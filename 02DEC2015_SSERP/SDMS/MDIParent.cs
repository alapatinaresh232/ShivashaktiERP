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
using SSCRM;
namespace SDMS
{
    public partial class MDIParent : Form
    {

        private Boolean terminating;
        private UserTaskDB objTask = null;
        private DataTable dt = null;
        private SQLDB objDB = null;
        
        #region Form Object Variables
        private ReportViewer childReportViewer = null;
        private InvoiceTemplateProducts childInvoiceTemplateProducts = null;
        private VillageMaster childVillageMaster = null;
        private DealarApplicationForm childDealarApplicationForm = null;
        private FinishedGoods childFinishedGoods = null;
        private DeliveryChallanPU childDeliveryChallanPU = null;
        private OpeningStock childOpeningStock = null;
        private GoodsReceiptNotePU childGoodsReceiptNotePU = null;
        private InternalDamage childInternalDamage = null;
        private SPRefill childSPRefill = null;
        private EmpFromToSelection childEmpFromToSelection = null;
        private FDateTDateBranchSelection childFDateTDateBranchSelection = null;
        private OrderBookingForm childOrderBookingForm = null;
        private AgingIntervals childAgingIntervals = null;
        private DeliveryChallan childDeliveryChallanForm = null;
        private StockPointGRN childStockPointGRN = null;
        private DealerInvoice childDealerInvoice = null;
        private ReportGLSelection chldReportGLSelection = null;
        private frmDoorknocks chldfrmDoorknocks = null;
        private ResetPasswordFRM childResetPasswordFRMForm = null;
        private FeedBack childFeedBack = null;
        private frmChangeToBranch childfrmChangeToBranch = null;
        private StationaryIndent chldStationaryIndent = null;
        private SaleRegister childSaleRegister = null;
        private ReceiptVoucher childReceiptVoucher = null;
        private ReceiptVoucherList childReceiptVoucherList = null;
        private BankRecieptVoucher childBankRecieptVoucher = null;
        private FAOutstanding childFAOutstanding = null;
        private CashPayment childCashPayment = null;
        private BankPayment childBankPayment = null;
        private JournalVoucher childJournalVoucher = null;
        private TaskMaster childTaskMaster = null;
        private UserTask childUserTaskForm = null;
        private SalesCreditNote childSalesCreditNote = null;
        private DealerMapping childDealerMapping = null;
        private EmployeeContactDetails childEmployeeContactDetails = null;
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
                  
                if (CommonData.LogUserId.ToUpper() != "ADMIN")
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
                        mbAdmin.Visible = false;
                        //msb_Transaction_Transport.Visible = false;
                        mbUser.Visible = true;

                        if (CommonData.BranchType == "HO")
                        {
                            //msbTrans_CRM.Visible = true;
                            //msbTrans_HR.Visible = true;
                            //msbTrans_StockPoints.Visible = true;
                            //msbTrans_ProductionUnits.Visible = true;
                            //msbReport_CRM.Visible = true;
                            //msbReport_HR.Visible = true;
                            //msbReport_StockPoint.Visible = true;
                            //msb_Transaction_Transport.Visible = true;

                        }

                        if (CommonData.BranchType == "BR")
                        {
                            //msbTrans_CRM.Visible = true;
                            //msbTrans_HR.Visible = true;
                            //msbReport_CRM.Visible = true;
                            //msbReport_HR.Visible = true;
                        }

                        if (CommonData.BranchType == "SP")
                        {
                            //msbTrans_StockPoints.Visible = true;
                            //msbReport_StockPoint.Visible = true;
                        }

                        if (CommonData.BranchType == "PU")
                        {
                            //msbTrans_ProductionUnits.Visible = true;
                        }
                        if (CommonData.BranchType == "TR")
                        {
                            //msb_Transaction_Transport.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        
        
        //private void msbTrans_CRM_Market_DoorKnock_Click(object sender, EventArgs e)
        //{
        //    if (childDoorKnocksForm == null || childDoorKnocksForm.Text == "")
        //    {
        //        childDoorKnocksForm = new DoorKnocks();
        //        childDoorKnocksForm.MdiParent = this;
        //        childDoorKnocksForm.Show();
        //    }
        //}

        private void msbTrans_CRM_Market_SaleOrder_Click(object sender, EventArgs e)
        {
            //if (childSalseORderForm == null || childSalseORderForm.Text == "")
            //{
            //    childSalseORderForm = new SalseORder();
            //    childSalseORderForm.MdiParent = this;
            //    childSalseORderForm.Show();
            //}
        }
        
        private void msbTrans_CRM_Sale_HierarMap_GroupMap_Click(object sender, EventArgs e)
        {
            //if (childSouceToDestinationForm == null || childSouceToDestinationForm.Text == "")
            //{
            //    childSouceToDestinationForm = new SouceToDestination();
            //    childSouceToDestinationForm.MdiParent = this;
            //    childSouceToDestinationForm.Show();
            //}
        }

        //private void msbTrans_CRM_Sale_HierarMap_GroupAbove_Click(object sender, EventArgs e)
        //{
        //    //if (childGroupToDestinationForm == null || childGroupToDestinationForm.Text == "")
        //    //{
        //    //    childGroupToDestinationForm = new GroupToDestination();
        //    //    childGroupToDestinationForm.MdiParent = this;
        //    //    childGroupToDestinationForm.Show();
        //    //}
        //}

        //private void msbTrans_CRM_Sale_Reconcil_OrdSheetIssue_Click(object sender, EventArgs e)
        //{
        //    if (childOrderSheetIssueForm == null || childOrderSheetIssueForm.Text == "")
        //    {
        //        childOrderSheetIssueForm = new frmOrderSheetIssue();
        //        childOrderSheetIssueForm.MdiParent = this;
        //        childOrderSheetIssueForm.Show();
        //    }
        //}

        //private void msbTrans_CRM_Sale_Reconcil_OrdSheetReturn_Click(object sender, EventArgs e)
        //{
        //    if (childSROrderSheetReturn == null || childSROrderSheetReturn.Text == "")
        //    {
        //        childSROrderSheetReturn = new frmOrderSheetReturn();
        //        childSROrderSheetReturn.MdiParent = this;
        //        childSROrderSheetReturn.Show();
        //    }
        //}

        //private void msbTrans_CRM_Sale_Reconcil_MonthInputs_Click(object sender, EventArgs e)
        //{
        //    if (childSrmonthlyInputesForm == null || childSrmonthlyInputesForm.Text == "")
        //    {
        //        childSrmonthlyInputesForm = new frmSrmonthlyInputes();
        //        childSrmonthlyInputesForm.MdiParent = this;
        //        childSrmonthlyInputesForm.Show();
        //    }
        //}

        //private void msbTrans_CRM_Sale_Builtin_Click(object sender, EventArgs e)
        //{
        //    if (childInvoiceBultinForm == null || childInvoiceBultinForm.Text == "")
        //    {
        //        childInvoiceBultinForm = new InvoiceBultin();
        //        childInvoiceBultinForm.MdiParent = this;
        //        childInvoiceBultinForm.Show();
        //    }
        //}

        //private void msbTrans_CRM_Sale_Invoices_Click(object sender, EventArgs e)
        //{
        //    if (childInvoiceForm == null || childInvoiceForm.Text == "")
        //    {
        //        childInvoiceForm = new Invoice();
        //        childInvoiceForm.MdiParent = this;
        //        childInvoiceForm.Show();
        //    }
        //}

        //private void msbTrans_CRM_Sale_PrevInvoice_Click(object sender, EventArgs e)
        //{
        //    if (childPrevInvoiceForm == null || childPrevInvoiceForm.Text == "")
        //    {
        //        childPrevInvoiceForm = new PrevInvoice();
        //        childPrevInvoiceForm.MdiParent = this;
        //        childPrevInvoiceForm.Show();
        //    }
        //}

        //private void msbTrans_CRM_Service_AOvsSales_Click(object sender, EventArgs e)
        //{

        //}

        //private void msbTrans_CRM_Service_FormVisit_Click(object sender, EventArgs e)
        //{

        //}

        //private void msbTrans_CRM_Service_CountActves_Click(object sender, EventArgs e)
        //{

        //}

        //private void msbTrans_CRM_Service_Replace_Click(object sender, EventArgs e)
        //{

        //}

        //private void msbMaster_General_Customer_Click(object sender, EventArgs e)
        //{
        //    if (childCustomerMaster == null || childCustomerMaster.Text == "")
        //    {
        //        childCustomerMaster = new CustomerMaster();
        //        childCustomerMaster.MdiParent = this;
        //        childCustomerMaster.Show();
        //    }
        //}

        //private void msbMaster_General_PhysicalBranch_Click(object sender, EventArgs e)
        //{
        //    if (childBranchAddForm == null || childBranchAddForm.Text == "")
        //    {
        //        childBranchAddForm = new BranchAdd();
        //        childBranchAddForm.MdiParent = this;
        //        childBranchAddForm.Show();
        //    }
        //}

        //private void msbMaster_General_LogicalBranch_Click(object sender, EventArgs e)
        //{
        //    if (childLogicalBranchForm == null || childLogicalBranchForm.Text == "")
        //    {
        //        childLogicalBranchForm = new LogicalBranch();
        //        childLogicalBranchForm.MdiParent = this;
        //        childLogicalBranchForm.Show();
        //    }
        //}

        //private void msbMaster_General_Product_Click(object sender, EventArgs e)
        //{

        //}

        //private void mbViews_Invoice_Click(object sender, EventArgs e)
        //{
        //    if (childInvoiceViewForm == null || childInvoiceViewForm.Text == "")
        //    {
        //        childInvoiceViewForm = new InvoiceView();
        //        childInvoiceViewForm.MdiParent = this;
        //        childInvoiceViewForm.Show();
        //    }
        //}

        //private void msbReport_SalesRegister_Click(object sender, EventArgs e)
        //{

        //}

        //private void msbAdmin_User_CreateUser_Click(object sender, EventArgs e)
        //{
        //    if (childUserMasterForm == null || childUserMasterForm.Text == "")
        //    {
        //        childUserMasterForm = new UserMaster();
        //        childUserMasterForm.MdiParent = this;
        //        childUserMasterForm.Show();
        //    }
        //}

        //private void msbAdmin_User_Task_Click(object sender, EventArgs e)
        //{
        //    if (childUserTaskForm == null || childUserTaskForm.Text == "")
        //    {
        //        childUserTaskForm = new UserTask();
        //        childUserTaskForm.MdiParent = this;
        //        childUserTaskForm.Show();
        //    }
        //}

        //private void msbAdmin_User_ResetPWD_Click(object sender, EventArgs e)
        //{

        //    if (childResetPasswordFRMForm == null || childResetPasswordFRMForm.Text == "")
        //    {
        //        childResetPasswordFRMForm = new ResetPasswordFRM();
        //        childResetPasswordFRMForm.MdiParent = this;
        //        childResetPasswordFRMForm.Show();
        //    }
        //}

        private void tsmExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to close ?",
                                       "CRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                Application.Exit();

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

                    dt = objTask.Get_DMSUserTasks(CommonData.LogUserId.ToString(),CommonData.CompanyCode.ToString()).Tables[0];

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
       
        private void msbMasters_VillageMaster_Click(object sender, EventArgs e)
        {
            if (childVillageMaster == null || childVillageMaster.Text == "")
            {
                childVillageMaster = new VillageMaster();
                //childVillageMaster.MdiParent = this;
                childVillageMaster.Show();
            }
        }


        private void msbTrans_CRM_Market_DealerAppl_Click(object sender, EventArgs e)
        {
            if (childDealarApplicationForm == null || childDealarApplicationForm.Text == "")
            {
                childDealarApplicationForm = new DealarApplicationForm();
                childDealarApplicationForm.MdiParent = this;
                childDealarApplicationForm.Show();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
        }

        private void msbTransactions_Dealers_ApplicationForm_Click(object sender, EventArgs e)
        {
            if (childDealarApplicationForm == null || childDealarApplicationForm.Text == "")
            {
                childDealarApplicationForm = new DealarApplicationForm();
                childDealarApplicationForm.MdiParent = this;
                childDealarApplicationForm.Show();
            }
        }

        private void msbTransactions_Production_FinishedGoods_Click(object sender, EventArgs e)
        {
            if (childFinishedGoods == null || childFinishedGoods.Text == "")
            {
                childFinishedGoods = new FinishedGoods();
                childFinishedGoods.MdiParent = this;
                childFinishedGoods.Show();
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

        private void msbTrans_ProdUnit_DCST_Click(object sender, EventArgs e)
        {
            if (childDeliveryChallanPU == null || childDeliveryChallanPU.Text == "")
            {
                childDeliveryChallanPU = new DeliveryChallanPU("DCST");
                childDeliveryChallanPU.MdiParent = this;
                childDeliveryChallanPU.Show();
            }
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

        private void msbTrans_ProdUnit_GRN_Click(object sender, EventArgs e)
        {
            if (childGoodsReceiptNotePU == null || childGoodsReceiptNotePU.Text == "")
            {
                childGoodsReceiptNotePU = new GoodsReceiptNotePU();
                childGoodsReceiptNotePU.MdiParent = this;
                childGoodsReceiptNotePU.Show();
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

        private void msbReport_Production_FinishedGoods_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(9);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
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

        private void msbReport_Production__RecByDestBranch_Click(object sender, EventArgs e)
        {
            if (childFDateTDateBranchSelection == null || childFDateTDateBranchSelection.Text == "")
            {
                childFDateTDateBranchSelection = new FDateTDateBranchSelection(1);
                childFDateTDateBranchSelection.MdiParent = this;
                childFDateTDateBranchSelection.Show();
            }
        }

        private void msbTransactions_Sales_OrderBooking_Click(object sender, EventArgs e)
        {
            if (childOrderBookingForm == null || childOrderBookingForm.Text == "")
            {
                childOrderBookingForm = new OrderBookingForm();
                childOrderBookingForm.MdiParent = this;
                childOrderBookingForm.Show();
            }
        }

        private void msbMasters_DealerMarketing_AgingIntervals_Click(object sender, EventArgs e)
        {
            if (childAgingIntervals == null || childAgingIntervals.Text == "")
            {
                childAgingIntervals = new AgingIntervals();
                childAgingIntervals.MdiParent = this;
                childAgingIntervals.Show();
            }
        }

        private void msbReport_Sales_OrderBookingsReg_Click(object sender, EventArgs e)
        {
            if (childEmpFromToSelection == null || childEmpFromToSelection.Text == "")
            {
                childEmpFromToSelection = new EmpFromToSelection(17);
                childEmpFromToSelection.MdiParent = this;
                childEmpFromToSelection.Show();
            }
        }

        private void msbTransactions_SP_DC_Click(object sender, EventArgs e)
        {
            if (childDeliveryChallanForm == null || childDeliveryChallanForm.Text == "")
            {
                childDeliveryChallanForm = new DeliveryChallan();
                childDeliveryChallanForm.MdiParent = this;
                childDeliveryChallanForm.Show();
            }
        }

        private void msbTransactions_SP_DCST_Click(object sender, EventArgs e)
        {
            if (childDeliveryChallanForm == null || childDeliveryChallanForm.Text == "")
            {
                childDeliveryChallanForm = new DeliveryChallan();
                childDeliveryChallanForm.MdiParent = this;
                childDeliveryChallanForm.Show();
            }
        }

        private void msbTransactions_SP_DealerInv_Click(object sender, EventArgs e)
        {
            if (childDealerInvoice == null || childDealerInvoice.Text == "")
            {
                childDealerInvoice = new DealerInvoice("SP2DLSB");
                childDealerInvoice.MdiParent = this;
                childDealerInvoice.Show();
            }
        }

        private void msbTransactions_SP_GRN_Click(object sender, EventArgs e)
        {
            if (childStockPointGRN == null || childStockPointGRN.Text == "")
            {
                childStockPointGRN = new StockPointGRN();
                childStockPointGRN.MdiParent = this;
                childStockPointGRN.Show();
            }
        }

        private void msbTrans_Production_DealerInv_Click(object sender, EventArgs e)
        {
            if (childDealerInvoice == null || childDealerInvoice.Text == "")
            {
                childDealerInvoice = new DealerInvoice("PU2DLSB");
                childDealerInvoice.MdiParent = this;
                childDealerInvoice.Show();
            }
        }

        private void msbReport_StockPoint_DC_Click(object sender, EventArgs e)
        {
            if (CommonData.LogUserRole == "MANAGEMENT" || CommonData.LogUserId.ToUpper() == "ADMIN")
            {
                chldReportGLSelection = new ReportGLSelection("STOCKPOINT_DC");
                chldReportGLSelection.MdiParent = this;
                chldReportGLSelection.Show();
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
            if (CommonData.LogUserRole == "MANAGEMENT" || CommonData.LogUserId.ToUpper() == "ADMIN")
            {
                chldReportGLSelection = new ReportGLSelection("STOCKPOINT_DCST");
                chldReportGLSelection.MdiParent = this;
                chldReportGLSelection.Show();
            }
            else
            {
                chldfrmDoorknocks = new frmDoorknocks("STOCKPOINT_DCST");
                chldfrmDoorknocks.MdiParent = this;
                chldfrmDoorknocks.Show();
            }
        }

        private void msbReport_StockPoint_GRNs_Click(object sender, EventArgs e)
        {
            if (CommonData.LogUserRole == "MANAGEMENT" || CommonData.LogUserId.ToUpper() == "ADMIN")
            {
                chldReportGLSelection = new ReportGLSelection("STOCKPOINT_GRN");
                chldReportGLSelection.MdiParent = this;
                chldReportGLSelection.Show();
            }
            else
            {
                chldfrmDoorknocks = new frmDoorknocks("STOCKPOINT_GRN");
                chldfrmDoorknocks.MdiParent = this;
                chldfrmDoorknocks.Show();
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

        private void mbUser_Feedback_Click(object sender, EventArgs e)
        {
            if (childFeedBack == null || childFeedBack.Text == "")
            {
                childFeedBack = new FeedBack();
                childFeedBack.MdiParent = this;
                childFeedBack.Show();
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

        private void msbTrans_Stationary_Indent_Click(object sender, EventArgs e)
        {
            if (chldStationaryIndent == null || chldStationaryIndent.Text == "")
            {
                chldStationaryIndent = new StationaryIndent();
                chldStationaryIndent.MdiParent = this;
                chldStationaryIndent.Show();
            }
        }

        private void msbReports_StockPoint_DealerInv_Click(object sender, EventArgs e)
        {

            if (childSaleRegister == null || childSaleRegister.Text == "")
            {
                childSaleRegister = new SaleRegister();
                childSaleRegister.MdiParent = this;
                childSaleRegister.Show();
            }
        }

        private void msbReport_Production__DealerInvReg_Click(object sender, EventArgs e)
        {
            if (childSaleRegister == null || childSaleRegister.Text == "")
            {
                childSaleRegister = new SaleRegister();
                childSaleRegister.MdiParent = this;
                childSaleRegister.Show();
            }
        }

        private void msbTrans_FA_DLRecieptVoucher_Click(object sender, EventArgs e)
        {
            if (childReceiptVoucher == null || childReceiptVoucher.Text == "")
            {
                childReceiptVoucher = new ReceiptVoucher();
                childReceiptVoucher.MdiParent = this;
                childReceiptVoucher.Show();
            }
        }

        private void msbTrans_FA_DLRecieptVoucherList_Click(object sender, EventArgs e)
        {
            if (childReceiptVoucherList == null || childReceiptVoucherList.Text == "")
            {
                childReceiptVoucherList = new ReceiptVoucherList();
                childReceiptVoucherList.MdiParent = this;
                childReceiptVoucherList.Show();
            }
        }

        private void msbTrans_FA_DLBankRecieptVoucher_Click(object sender, EventArgs e)
        {
            if (childBankRecieptVoucher == null || childBankRecieptVoucher.Text == "")
            {
                childBankRecieptVoucher = new BankRecieptVoucher();
                childBankRecieptVoucher.MdiParent = this;
                childBankRecieptVoucher.Show();
            }
        }

        private void msbReport_FinancialAccounts_Outstanding_Click(object sender, EventArgs e)
        {
            if (childFAOutstanding == null || childFAOutstanding.Text == "")
            {
                childFAOutstanding = new FAOutstanding();
                childFAOutstanding.MdiParent = this;
                childFAOutstanding.Show();
            }
        }

        private void msbTrans_FA_CashPayment_Click(object sender, EventArgs e)
        {
            if (childCashPayment == null || childCashPayment.Text == "")
            {
                childCashPayment = new CashPayment();
                childCashPayment.MdiParent = this;
                childCashPayment.Show();
            }
        }

        private void msbTrans_FA_BankPayment_Click(object sender, EventArgs e)
        {
            if (childBankPayment == null || childBankPayment.Text == "")
            {
                childBankPayment = new BankPayment();
                childBankPayment.MdiParent = this;
                childBankPayment.Show();
            }
        }

        private void journalVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (childJournalVoucher == null || childJournalVoucher.Text == "")
            {
                childJournalVoucher = new JournalVoucher();
                childJournalVoucher.MdiParent = this;
                childJournalVoucher.Show();
            }
        }


        private void mbAdmin_TaskMaster_Click(object sender, EventArgs e)
        {
            if (childTaskMaster == null || childTaskMaster.Text == "")
            {
                childTaskMaster = new TaskMaster();
                childTaskMaster.MdiParent = this;
                childTaskMaster.Show();
            }
        }

        private void mbAdmin_Users_Tasks_Click(object sender, EventArgs e)
        {
            if (childUserTaskForm == null || childUserTaskForm.Text == "")
            {
                childUserTaskForm = new UserTask();
                childUserTaskForm.MdiParent = this;
                childUserTaskForm.Show();
            }
        }

        private void msbReport_DealerMarketing_DealerDetails_Click(object sender, EventArgs e)
        {
            DealersSearch childDealersSearch = new DealersSearch();
            childDealersSearch.MdiParent = this;
            childDealersSearch.Show();
        }

        private void msbTransactions_SP_CreditNote_Click(object sender, EventArgs e)
        {
            if (childSalesCreditNote == null || childSalesCreditNote.Text == "")
            {
                childSalesCreditNote = new SalesCreditNote();
                childSalesCreditNote.MdiParent = this;
                childSalesCreditNote.Show();
            }
        }

        private void msbTrans_Production_Packing_Click(object sender, EventArgs e)
        {
            if (childFinishedGoods == null || childFinishedGoods.Text == "")
            {
                childFinishedGoods = new FinishedGoods();
                childFinishedGoods.MdiParent = this;
                childFinishedGoods.Show();
            }
        }

        private void msbTransactions_Sales_DealerMapping_Click(object sender, EventArgs e)
        {
            if (childDealerMapping == null || childDealerMapping.Text == "")
            {
                childDealerMapping = new DealerMapping();
                childDealerMapping.MdiParent = this;
                childDealerMapping.Show();
            }
        }

        private void msbMasters_HR_EmpContactDetl_Click(object sender, EventArgs e)
        {
            if (childEmployeeContactDetails == null || childEmployeeContactDetails.Text == "")
            {
                childEmployeeContactDetails = new EmployeeContactDetails();
                childEmployeeContactDetails.MdiParent = this;
                childEmployeeContactDetails.Show();
            }
        }        
    }
}
