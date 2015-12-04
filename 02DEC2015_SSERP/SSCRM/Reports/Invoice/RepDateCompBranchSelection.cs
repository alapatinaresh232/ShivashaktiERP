using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRM.App_Code;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
using SSAdmin;
using SSCRMDB;
using SSTrans;


namespace SSCRM
{
    public partial class RepDateCompBranchSelection : Form
    {
        SQLDB objSQLdb = null;
        StockPointDB objSPdb = null;
        private ExcelDB objExDb = null;
        UtilityDB objUtilityDB = null;

        string Company = "", Branches = "", sFrmType = "", DocumentMonth = "", strTrnTypes = "";
        double dQty = 0;

        public RepDateCompBranchSelection()
        {
            InitializeComponent();
        }
        public RepDateCompBranchSelection(string sType)
        {
            InitializeComponent();
            sFrmType = sType;
        }

        private void RepDateCompBranchSelection_Load(object sender, EventArgs e)
        {
            FillBranches();
            FillTransactonTypesToList();

            lblTrnType.Visible = false;
            chkTrnType.Visible = false;
            btnDownload.Visible = false;
            dtpFrmDocMonth.Value = DateTime.Today;
            if (sFrmType == "STOCK_SUMMARY")
            {
                this.Text = "STOCK SUMMARY";
                lblQty.Visible = false;
                txtQty.Visible = false;
                lblToMonth.Visible = false;
                dtpToDocMonth.Visible = false;
               
            }
            if (sFrmType == "LOW_DISP_SP")
            {
                this.Text = "Low Dispatch SP's";
                lblName.Text = "Doc Month";
                dtpFrmDocMonth.CustomFormat = "MMM/yyyy";
                dtpFrmDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
                dtpToDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));                
                lblQty.Visible = true;
                txtQty.Visible = true;                


            }
            if (sFrmType == "STK_RET_GCGL")
            {
                this.Text = "% Stock Returns GC/GL Wise";
                lblName.Text = "Doc Month";
                dtpFrmDocMonth.CustomFormat = "MMM/yyyy";
                dtpToDocMonth.CustomFormat = "MMM/yyyy";
                dtpFrmDocMonth.Value = Convert.ToDateTime(CommonData.DocMonth);
                dtpToDocMonth.Value = Convert.ToDateTime(CommonData.DocMonth);
                lblQty.Visible = false;
                txtQty.Visible = false;

            }
            if (sFrmType == "SP_DCST_INTRSTATE_EQLCOMP")
            {
                this.Text = "interstate Stock Receipts";
                lblName.Text = "Doc Month";
                dtpFrmDocMonth.CustomFormat = "MMM/yyyy";
                dtpToDocMonth.CustomFormat = "MMM/yyyy";
                dtpFrmDocMonth.Value = Convert.ToDateTime(CommonData.DocMonth);
                dtpToDocMonth.Value = Convert.ToDateTime(CommonData.DocMonth);
                lblQty.Visible = false;
                txtQty.Visible = false;

            }
            
            if (sFrmType == "SP_DCST_INTRSTATE_DIFFCOMP")
            {
                this.Text = "interstate Stock Receipts";
                lblName.Text = "Doc Month";
                dtpFrmDocMonth.CustomFormat = "MMM/yyyy";
                dtpToDocMonth.CustomFormat = "MMM/yyyy";
                dtpFrmDocMonth.Value = Convert.ToDateTime(CommonData.DocMonth);
                dtpToDocMonth.Value = Convert.ToDateTime(CommonData.DocMonth);
                lblQty.Visible = false;
                txtQty.Visible = false;

            }

            if (sFrmType == "SP_DCST_WITH_IN_STATE")
            {
                this.Text = "With in State Stock Receipts";
                lblName.Text = "Doc Month";
                dtpFrmDocMonth.CustomFormat = "MMM/yyyy";
                dtpToDocMonth.CustomFormat = "MMM/yyyy";
                dtpFrmDocMonth.Value = Convert.ToDateTime(CommonData.DocMonth);
                dtpToDocMonth.Value = Convert.ToDateTime(CommonData.DocMonth);
                lblQty.Visible = false;
                txtQty.Visible = false;

            }

            if (sFrmType == "STK_RET_GCGL_BR_SUMM")
            {
                this.Text = "% Stock Returns GC/GL Wise";
                lblName.Text = "Doc Month";
                dtpFrmDocMonth.CustomFormat = "MMM/yyyy";
                dtpToDocMonth.CustomFormat = "MMM/yyyy";
                dtpFrmDocMonth.Value = Convert.ToDateTime(CommonData.DocMonth);
                dtpToDocMonth.Value = Convert.ToDateTime(CommonData.DocMonth);
                lblQty.Visible = false;
                txtQty.Visible = false;

            }
            if (sFrmType == "STK_RET_GCGL_PR")
            {
                this.Text = "% Stock Returns GC/GL Product Wise";
                lblName.Text = "Doc Month";
                dtpFrmDocMonth.CustomFormat = "MMM/yyyy";
                dtpToDocMonth.CustomFormat = "MMM/yyyy";
                dtpFrmDocMonth.Value = Convert.ToDateTime(CommonData.DocMonth);
                dtpToDocMonth.Value = Convert.ToDateTime(CommonData.DocMonth);
                lblQty.Visible = false;
                txtQty.Visible = false;

            }
            if (sFrmType == "RF_SUMMARY")
            {
                this.Text = "RF SUMMARY";
                lblName.Text = "From Month";
                dtpFrmDocMonth.CustomFormat = "MMM/yyyy";
                dtpFrmDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
                dtpToDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
                lblQty.Visible = false;
                txtQty.Visible = false;
            }
            if (sFrmType == "SP_PENDING_DC")
            {
                this.Text = "PENDING DC's";
                lblName.Text = "From Month";
                dtpFrmDocMonth.CustomFormat = "MMM/yyyy";
                dtpToDocMonth.CustomFormat = "MMM/yyyy";
                dtpFrmDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
                dtpToDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
                lblQty.Visible = false;
                txtQty.Visible = false;              
            }
            if (sFrmType == "STOCKPOINT_DC")
            {
                this.Text = "STOCKPOINT_DC's";
                lblName.Text = "From Month";
                dtpFrmDocMonth.CustomFormat = "MMM/yyyy";
                dtpToDocMonth.CustomFormat = "MMM/yyyy";
                dtpFrmDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
                dtpToDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
                lblQty.Visible = false;
                txtQty.Visible = false;
            }
            if (sFrmType == "STOCKPOINT_DCST")
            {
                this.Text = "STOCKPOINT_DCST's";
                lblName.Text = "From Month";
                dtpFrmDocMonth.CustomFormat = "MMM/yyyy";
                dtpToDocMonth.CustomFormat = "MMM/yyyy";
                dtpFrmDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
                dtpToDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
                lblQty.Visible = false;
                txtQty.Visible = false;
            }
            if (sFrmType == "STOCKPOINT_GRN")
            {
                this.Text = "STOCKPOINT_GRN's";
                lblName.Text = "From Month";
                dtpFrmDocMonth.CustomFormat = "MMM/yyyy";
                dtpToDocMonth.CustomFormat = "MMM/yyyy";
                dtpFrmDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
                dtpToDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
                lblQty.Visible = false;
                txtQty.Visible = false;
            }

            if (sFrmType == "SPECIAL_APPROVALS_SUMMARY")
            {
                this.Text = "SPECIAL APPROVALS SUMMARY";
                lblName.Text = "From Date";
                lblToMonth.Text = "To Date";
                dtpToDocMonth.CustomFormat = "dd/MM/yyyy";
                dtpFrmDocMonth.Value = DateTime.Today;
                dtpToDocMonth.Value = DateTime.Today;
                lblQty.Visible = false;
                txtQty.Visible = false;
            }
            if (sFrmType == "TOUR_EXPENSES")
            {
                this.Text = "EXPENSES STATEMENT";
                lblName.Text = "From Month";
                dtpFrmDocMonth.CustomFormat = "MMM/yyyy";
                dtpToDocMonth.CustomFormat = "MMM/yyyy";
                dtpFrmDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
                dtpToDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
                lblQty.Visible = false;
                txtQty.Visible = false;
            }         
            

            if (sFrmType == "RF_STATEMENT")
            {
                this.Text = "RF STATEMENT";
                lblName.Text = "From Month";
                dtpFrmDocMonth.CustomFormat = "MMM/yyyy";
                dtpToDocMonth.CustomFormat = "MMM/yyyy";
                dtpFrmDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
                dtpToDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
                lblQty.Visible = false;
                txtQty.Visible = false;
            }

            if (sFrmType == "SHORTAGE_WRITEOFF")
            {
                this.Text = "Shortage/Write off/Excess Register";
                lblName.Text = "From Month";
                dtpFrmDocMonth.CustomFormat = "MMM/yyyy";
                dtpToDocMonth.CustomFormat = "MMM/yyyy";
                dtpFrmDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
                dtpToDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
                lblQty.Visible = false;
                txtQty.Visible = false;
            }

            if (sFrmType == "PU_DAMAGE_STOCK_DETL")
            {
                this.Text = "% Of PU Damaged Stock";
                lblName.Text = "From Date";
                lblToMonth.Text = "To Date";
                dtpToDocMonth.CustomFormat = "dd/MM/yyyy";
                dtpFrmDocMonth.Value = DateTime.Today;
                dtpToDocMonth.Value = DateTime.Today;
                lblQty.Visible = false;
                txtQty.Visible = false;
            }
            if (sFrmType == "PU_DAMAGE_STOCK_SUMMARY")
            {
                this.Text = "% Of PU Damaged Stock";
                lblName.Text = "From Date";
                lblToMonth.Text = "To Date";
                dtpToDocMonth.CustomFormat = "dd/MM/yyyy";
                dtpFrmDocMonth.Value = DateTime.Today;
                dtpToDocMonth.Value = DateTime.Today;
                lblQty.Visible = false;
                txtQty.Visible = false;
            }
            if (sFrmType == "SP_TRANSPORT_COST")
            {
                btnDownload.Visible = true;
                this.Text = "Transport Cost Summary";
                lblName.Text = "Doc Month";
                dtpFrmDocMonth.CustomFormat = "MMM/yyyy";
                dtpFrmDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
                dtpToDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));                
                lblQty.Visible = false;
                txtQty.Visible = false;
                lblToMonth.Visible = false;
                dtpToDocMonth.Visible = false;
            }
            if (sFrmType == "STOCK_TRANSACTIONS_SUM")
            {
                this.Text = "Stock Transactions Summary";
                lblName.Text = "From Month";
                dtpFrmDocMonth.CustomFormat = "MMM/yyyy";
                dtpToDocMonth.CustomFormat = "MMM/yyyy";
                dtpFrmDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
                dtpToDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
                lblQty.Visible = false;
                txtQty.Visible = false;
                lblTrnType.Visible = true;
                chkTrnType.Visible = true;
                btnDownload.Visible = true;
            }

            if (sFrmType == "SP_TRNSP_COST_TRIP_WISE")
            {
                btnDownload.Visible = true;
                this.Text = "Trip Wise Transport Cost";
                lblName.Text = "Doc Month";
                dtpFrmDocMonth.CustomFormat = "MMM/yyyy";
                dtpFrmDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
                dtpToDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
                lblQty.Visible = false;
                txtQty.Visible = false;
                lblToMonth.Visible = false;
                dtpToDocMonth.Visible = false;
            }
            if (sFrmType == "BRANCH_MEETING")
            {
                this.Text = "Branch Meeting Details";
                lblName.Text = "From Date";
                lblToMonth.Text = "To Date";
                dtpToDocMonth.CustomFormat = "dd/MM/yyyy";
                dtpFrmDocMonth.Value = DateTime.Today;
                dtpToDocMonth.Value = DateTime.Today;
                lblQty.Visible = false;
                txtQty.Visible = false;
            }
            if (sFrmType == "AWARDS_DETL")
            {
                this.Text = "Emp Awards Details";
                lblName.Text = "From Date";
                lblToMonth.Text = "To Date";
                dtpToDocMonth.CustomFormat = "dd/MM/yyyy";
                dtpFrmDocMonth.Value = DateTime.Today;
                dtpToDocMonth.Value = DateTime.Today;
                lblQty.Text = "Ecode";
                lblQty.Visible = true;
                txtQty.Visible = true;
                lblTrnType.Visible = true;
                lblTrnType.Text = "Events";
                chkTrnType.Visible = true;
                btnDownload.Visible = false;
                FillEvents();
            }

            if (sFrmType == "STATE_VAT_SUM")
            {
                this.Text = "State Vat Summary";
                lblName.Text = "Doc Month";
                dtpFrmDocMonth.CustomFormat = "MMM/yyyy";
                dtpToDocMonth.CustomFormat = "MMM/yyyy";
                dtpFrmDocMonth.Value = Convert.ToDateTime(CommonData.DocMonth);
                dtpToDocMonth.Value = Convert.ToDateTime(CommonData.DocMonth);
                lblQty.Visible = false;
                txtQty.Visible = false;

            }


        }

        private void FillTransactonTypesToList()
        {

            NewCheckboxListItem oclBox = new NewCheckboxListItem();
            oclBox.Tag = "DC";
            oclBox.Text = "DC";
            chkTrnType.Items.Add(oclBox);
            oclBox = null;
            NewCheckboxListItem oclBox1 = new NewCheckboxListItem();
            oclBox1.Tag = "DCST";
            oclBox1.Text = "DCST";
            chkTrnType.Items.Add(oclBox1);
            oclBox1 = null;
            NewCheckboxListItem oclBox2 = new NewCheckboxListItem();
            oclBox2.Tag = "DCR";
            oclBox2.Text = "DCR";
            chkTrnType.Items.Add(oclBox2);
            oclBox = null;
            NewCheckboxListItem oclBox3 = new NewCheckboxListItem();
            oclBox3.Tag = "GRN";
            oclBox3.Text = "GRN";
            chkTrnType.Items.Add(oclBox3);
            oclBox = null;
        }

        private void FillEvents()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            chkTrnType.Items.Clear();
            try
            {
                strCmd = "SELECT DISTINCT HEA_EVENT_ID,HEA_EVENT_NAME " +
                         " FROM HR_APPL_EMP_AWARDS_REWARDS " +
                         " ORDER BY HEA_EVENT_NAME asc";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = dataRow["HEA_EVENT_ID"].ToString();
                        oclBox.Text = dataRow["HEA_EVENT_NAME"].ToString();
                        chkTrnType.Items.Add(oclBox);
                        oclBox = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
      
        private void FillBranches()
        {
            tvBranches.Nodes.Clear();
            objSPdb = new StockPointDB();
            DataSet ds = new DataSet();
            try
            {
                if (sFrmType == "STK_RET_GCGL_BR_SUMM")
                {
                    ds = objSPdb.Get_UserBranchesWithStateFilter("", "", "admin", "", "PARENT");
                }
                else
                {
                    ds = objSPdb.Get_UserBranchesWithStateFilter("", "", CommonData.LogUserId, "", "PARENT");
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        tvBranches.Nodes.Add(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), ds.Tables[0].Rows[i]["COMPANY_NAME"].ToString());

                        DataSet dsState = new DataSet();

                        if (sFrmType == "STK_RET_GCGL_BR_SUMM")
                        {
                            dsState = objSPdb.Get_UserBranchesWithStateFilter(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "", "admin", "", "STATE");
                        }
                        else
                        {
                            dsState = objSPdb.Get_UserBranchesWithStateFilter(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "", CommonData.LogUserId, "", "STATE");
                        }
                        if (dsState.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < dsState.Tables[0].Rows.Count; j++)
                            {
                                tvBranches.Nodes[i].Nodes.Add(dsState.Tables[0].Rows[j]["StateCode"].ToString(), dsState.Tables[0].Rows[j]["StateName"].ToString());

                                DataSet dschild = new DataSet();
                                if (sFrmType == "STOCK_SUMMARY" || sFrmType == "STK_RET_GCGL_PR" || sFrmType == "LOW_DISP_SP" 
                                    || sFrmType == "RF_SUMMARY" || sFrmType == "SP_PENDING_DC" || sFrmType == "STK_RET_GCGL"
                                    || sFrmType == "STOCKPOINT_DC" || sFrmType == "STOCKPOINT_DCST" || sFrmType == "STOCKPOINT_GRN" 
                                    || sFrmType == "SP_DCST_INTRSTATE_EQLCOMP" || sFrmType == "SP_DCST_INTRSTATE_DIFFCOMP"
                                    || sFrmType == "RF_STATEMENT" || sFrmType == "SHORTAGE_WRITEOFF" || sFrmType == "PU_DAMAGE_STOCK_DETL"
                                    || sFrmType == "PU_DAMAGE_STOCK_SUMMARY" || sFrmType == "SP_DCST_WITH_IN_STATE" || sFrmType == "SP_TRANSPORT_COST"
                                    || sFrmType == "STOCK_TRANSACTIONS_SUM" || sFrmType == "SP_TRNSP_COST_TRIP_WISE")
                                {
                                    dschild = objSPdb.Get_UserBranchesWithStateFilter(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), dsState.Tables[0].Rows[j]["StateCode"].ToString(), CommonData.LogUserId, "SP", "CHILD");
                                    tvBranches.Nodes[i].Nodes[j].Nodes.Add("STOCK POINTS" + "(" + dschild.Tables[0].Rows.Count + ")");
                                    if (dschild.Tables[0].Rows.Count > 0)
                                    {
                                        for (int k = 0; k < dschild.Tables[0].Rows.Count; k++)
                                        {
                                            tvBranches.Nodes[i].Nodes[j].Nodes[0].Nodes.Add(dschild.Tables[0].Rows[k]["BranchCode"].ToString(), dschild.Tables[0].Rows[k]["BranchName"].ToString());
                                        }
                                    }
                                }

                                if (sFrmType == "SPECIAL_APPROVALS_SUMMARY" || sFrmType == "TOUR_EXPENSES" || sFrmType == "STATE_VAT_SUM")
                                {                             

                                    dschild = objSPdb.Get_UserBranchesWithStateFilter(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), dsState.Tables[0].Rows[j]["StateCode"].ToString(), CommonData.LogUserId, "BR", "CHILD");
                                    tvBranches.Nodes[i].Nodes[j].Nodes.Add("BRANCHES" + "(" + dschild.Tables[0].Rows.Count + ")");
                                    if (dschild.Tables[0].Rows.Count > 0)
                                    {
                                        for (int k = 0; k < dschild.Tables[0].Rows.Count; k++)
                                        {
                                            tvBranches.Nodes[i].Nodes[j].Nodes[0].Nodes.Add(dschild.Tables[0].Rows[k]["BranchCode"].ToString(), dschild.Tables[0].Rows[k]["BranchName"].ToString());
                                        }
                                    }

                                   
                                }
                                if (sFrmType == "STK_RET_GCGL_BR_SUMM")
                                {

                                    dschild = objSPdb.Get_UserBranchesWithStateFilter(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), dsState.Tables[0].Rows[j]["StateCode"].ToString(), "", "BR", "CHILD");
                                    tvBranches.Nodes[i].Nodes[j].Nodes.Add("BRANCHES" + "(" + dschild.Tables[0].Rows.Count + ")");
                                    if (dschild.Tables[0].Rows.Count > 0)
                                    {
                                        for (int k = 0; k < dschild.Tables[0].Rows.Count; k++)
                                        {
                                            tvBranches.Nodes[i].Nodes[j].Nodes[0].Nodes.Add(dschild.Tables[0].Rows[k]["BranchCode"].ToString(), dschild.Tables[0].Rows[k]["BranchName"].ToString());
                                        }
                                    }


                                }

                                if (sFrmType == "BRANCH_MEETING" || sFrmType == "AWARDS_DETL")
                                {
                                    dschild = objSPdb.Get_UserBranchesWithStateFilter(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), dsState.Tables[0].Rows[j]["StateCode"].ToString(), CommonData.LogUserId, "SP", "CHILD");
                                    tvBranches.Nodes[i].Nodes[j].Nodes.Add("STOCK POINTS" + "(" + dschild.Tables[0].Rows.Count + ")");
                                    if (dschild.Tables[0].Rows.Count > 0)
                                    {
                                        for (int k = 0; k < dschild.Tables[0].Rows.Count; k++)
                                        {
                                            tvBranches.Nodes[i].Nodes[j].Nodes[0].Nodes.Add(dschild.Tables[0].Rows[k]["BranchCode"].ToString(), dschild.Tables[0].Rows[k]["BranchName"].ToString());
                                        }
                                    }

                                    dschild = objSPdb.Get_UserBranchesWithStateFilter(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), dsState.Tables[0].Rows[j]["StateCode"].ToString(), CommonData.LogUserId, "BR", "CHILD");
                                    tvBranches.Nodes[i].Nodes[j].Nodes.Add("BRANCHES" + "(" + dschild.Tables[0].Rows.Count + ")");
                                    if (dschild.Tables[0].Rows.Count > 0)
                                    {
                                        for (int k = 0; k < dschild.Tables[0].Rows.Count; k++)
                                        {
                                            tvBranches.Nodes[i].Nodes[j].Nodes[1].Nodes.Add(dschild.Tables[0].Rows[k]["BranchCode"].ToString(), dschild.Tables[0].Rows[k]["BranchName"].ToString());
                                        }
                                    }

                                    dschild = objSPdb.Get_UserBranchesWithStateFilter(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), dsState.Tables[0].Rows[j]["StateCode"].ToString(), CommonData.LogUserId, "PU", "CHILD");
                                    tvBranches.Nodes[i].Nodes[j].Nodes.Add("PRODUCTION UNITS" + "(" + dschild.Tables[0].Rows.Count + ")");
                                    if (dschild.Tables[0].Rows.Count > 0)
                                    {
                                        for (int k = 0; k < dschild.Tables[0].Rows.Count; k++)
                                        {
                                            tvBranches.Nodes[i].Nodes[j].Nodes[2].Nodes.Add(dschild.Tables[0].Rows[k]["BranchCode"].ToString(), dschild.Tables[0].Rows[k]["BranchName"].ToString());
                                        }
                                    }

                                    dschild = objSPdb.Get_UserBranchesWithStateFilter(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), dsState.Tables[0].Rows[j]["StateCode"].ToString(), CommonData.LogUserId, "TR", "CHILD");
                                    tvBranches.Nodes[i].Nodes[j].Nodes.Add("TRANSPORT UNITS" + "(" + dschild.Tables[0].Rows.Count + ")");
                                    if (dschild.Tables[0].Rows.Count > 0)
                                    {
                                        for (int k = 0; k < dschild.Tables[0].Rows.Count; k++)
                                        {
                                            tvBranches.Nodes[i].Nodes[j].Nodes[3].Nodes.Add(dschild.Tables[0].Rows[k]["BranchCode"].ToString(), dschild.Tables[0].Rows[k]["BranchName"].ToString());
                                        }
                                    }
                                }


                                if (sFrmType == "")
                                {
                                    dschild = objSPdb.Get_UserBranchesWithStateFilter(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), dsState.Tables[0].Rows[j]["StateCode"].ToString(), CommonData.LogUserId, "SP", "CHILD");
                                    tvBranches.Nodes[i].Nodes[j].Nodes.Add("STOCK POINTS" + "(" + dschild.Tables[0].Rows.Count + ")");
                                    if (dschild.Tables[0].Rows.Count > 0)
                                    {
                                        for (int k = 0; k < dschild.Tables[0].Rows.Count; k++)
                                        {
                                            tvBranches.Nodes[i].Nodes[j].Nodes[0].Nodes.Add(dschild.Tables[0].Rows[k]["BranchCode"].ToString(), dschild.Tables[0].Rows[k]["BranchName"].ToString());
                                        }
                                    }

                                    dschild = objSPdb.Get_UserBranchesWithStateFilter(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), dsState.Tables[0].Rows[j]["StateCode"].ToString(), CommonData.LogUserId, "BR", "CHILD");
                                    tvBranches.Nodes[i].Nodes[j].Nodes.Add("BRANCHES" + "(" + dschild.Tables[0].Rows.Count + ")");
                                    if (dschild.Tables[0].Rows.Count > 0)
                                    {
                                        for (int k = 0; k < dschild.Tables[0].Rows.Count; k++)
                                        {
                                            tvBranches.Nodes[i].Nodes[j].Nodes[1].Nodes.Add(dschild.Tables[0].Rows[k]["BranchCode"].ToString(), dschild.Tables[0].Rows[k]["BranchName"].ToString());
                                        }
                                    }

                                    dschild = objSPdb.Get_UserBranchesWithStateFilter(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), dsState.Tables[0].Rows[j]["StateCode"].ToString(), CommonData.LogUserId, "PU", "CHILD");
                                    tvBranches.Nodes[i].Nodes[j].Nodes.Add("PRODUCTION UNITS" + "(" + dschild.Tables[0].Rows.Count + ")");
                                    if (dschild.Tables[0].Rows.Count > 0)
                                    {
                                        for (int k = 0; k < dschild.Tables[0].Rows.Count; k++)
                                        {
                                            tvBranches.Nodes[i].Nodes[j].Nodes[2].Nodes.Add(dschild.Tables[0].Rows[k]["BranchCode"].ToString(), dschild.Tables[0].Rows[k]["BranchName"].ToString());
                                        }
                                    }

                                    dschild = objSPdb.Get_UserBranchesWithStateFilter(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), dsState.Tables[0].Rows[j]["StateCode"].ToString(), CommonData.LogUserId, "TR", "CHILD");
                                    tvBranches.Nodes[i].Nodes[j].Nodes.Add("TRANSPORT UNITS" + "(" + dschild.Tables[0].Rows.Count + ")");
                                    if (dschild.Tables[0].Rows.Count > 0)
                                    {
                                        for (int k = 0; k < dschild.Tables[0].Rows.Count; k++)
                                        {
                                            tvBranches.Nodes[i].Nodes[j].Nodes[3].Nodes.Add(dschild.Tables[0].Rows[k]["BranchCode"].ToString(), dschild.Tables[0].Rows[k]["BranchName"].ToString());
                                        }
                                    }
                                    dschild = objSPdb.Get_UserBranchesWithStateFilter(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), dsState.Tables[0].Rows[j]["StateCode"].ToString(), CommonData.LogUserId, "OL", "CHILD");
                                    tvBranches.Nodes[i].Nodes[j].Nodes.Add("OUT LETS" + "(" + dschild.Tables[0].Rows.Count + ")");
                                    if (dschild.Tables[0].Rows.Count > 0)
                                    {
                                        for (int k = 0; k < dschild.Tables[0].Rows.Count; k++)
                                        {
                                            tvBranches.Nodes[i].Nodes[j].Nodes[4].Nodes.Add(dschild.Tables[0].Rows[k]["BranchCode"].ToString(), dschild.Tables[0].Rows[k]["BranchName"].ToString());
                                        }
                                    }
                                }
                               
                            }
                        }


                    }

                }

                for (int ivar = 0; ivar < tvBranches.Nodes.Count; ivar++)
                {
                    for (int j = 0; j < tvBranches.Nodes[ivar].Nodes.Count; j++)
                    {
                        if (tvBranches.Nodes[ivar].Nodes[j].Nodes.Count > 0)
                            tvBranches.Nodes[ivar].FirstNode.Expand();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }



        private bool CheckData()
        {
            bool flag = true;
            GetSelectedTrnTypes();

            if (sFrmType == "AWARDS_DETL" && txtQty.Text.Length > 3)
            {
                flag = true;

            }
            else
            {
                flag = false;
                for (int k = 0; k < tvBranches.Nodes.Count; k++)
                {
                    for (int i = 0; i < tvBranches.Nodes[k].Nodes.Count; i++)
                    {
                        for (int j = 0; j < tvBranches.Nodes[k].Nodes[i].Nodes.Count; j++)
                        {
                            for (int ival = 0; ival < tvBranches.Nodes[k].Nodes[i].Nodes[j].Nodes.Count; ival++)
                            {
                                if (tvBranches.Nodes[k].Nodes[i].Nodes[j].Nodes[ival].Checked == true)
                                {
                                    flag = true;
                                }
                            }
                        }
                    }
                }
            }

            if (flag == false)
            {
                MessageBox.Show("Select Atleast One Branch", "SSCRM-Report", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return flag;
            }
            if (sFrmType == "AWARDS_DETL")
            {
                if (strTrnTypes.Length == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Select Event", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return flag;
                }
            }
            if (sFrmType == "STOCK_TRANSACTIONS_SUM")
            {
                if (strTrnTypes.Length == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Select Transaction Type", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return flag;
                }
            }

            return flag;
        }

        private void GetSelectedValues()
        {
            
            bool iscomp = false;
            Company = "";
            Branches = "";

            for (int k = 0; k < tvBranches.Nodes.Count; k++)
            {
                for (int i = 0; i < tvBranches.Nodes[k].Nodes.Count; i++)
                {
                    for (int j = 0; j < tvBranches.Nodes[k].Nodes[i].Nodes.Count; j++)
                    {
                        for (int ival = 0; ival < tvBranches.Nodes[k].Nodes[i].Nodes[j].Nodes.Count; ival++)
                        {
                            if (tvBranches.Nodes[k].Nodes[i].Nodes[j].Nodes[ival].Checked == true)
                            {
                                if (Branches != string.Empty)
                                    Branches += ",";
                                Branches += tvBranches.Nodes[k].Nodes[i].Nodes[j].Nodes[ival].Name.ToString();
                                iscomp = true;
                            }
                        }
                    }
                }


                if (iscomp == true)
                {
                    if (Company != string.Empty)
                        Company += ",";
                    Company += tvBranches.Nodes[k].Name.ToString();
                }
                iscomp = false;
            }
            
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
            if (sFrmType != "STOCK_SUMMARY")
            {
                DocumentMonth = "";
                if ((dtpFrmDocMonth.Value > dtpToDocMonth.Value))
                {
                    dtpFrmDocMonth.Value = dtpToDocMonth.Value;
                }
                else
                {
                    int months = MonthDiff(dtpFrmDocMonth.Value, dtpToDocMonth.Value);
                    months = months + 1;

                    for (int i = 0; i < months; i++)
                    {
                        DocumentMonth += Convert.ToDateTime(dtpFrmDocMonth.Value).AddMonths(i).ToString("MMMyyyy").ToUpper() + ',';
                    }

                    DocumentMonth = DocumentMonth.TrimEnd(',');
                }
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {
                GetSelectedValues();
                GetSelectedTrnTypes();
                GetDocumentMonths();

                if (DocumentMonth.Length == 0)
                {
                    DocumentMonth = Convert.ToDateTime(dtpFrmDocMonth.Value).ToString("MMMyyyy").ToUpper();
                }
                
                if (sFrmType == "STOCK_SUMMARY")
                {
                    CommonData.ViewReport = "COMPANY_BRANCH_PRODUCT_WISE_STOCK_SUMMARY";
                    ReportViewer objReportview = new ReportViewer(Convert.ToDateTime(dtpFrmDocMonth.Value).ToString("dd/MMM/yyyy"), Branches, "OUTSTANDING", Convert.ToDateTime(dtpFrmDocMonth.Value.AddMonths(-3)).ToString("MMMyyyy").ToUpper(), Convert.ToDateTime(dtpFrmDocMonth.Value.AddMonths(+1)).ToString("MMMyyyy").ToUpper());
                    objReportview.Show();
                }
                if (sFrmType == "LOW_DISP_SP")
                {                    
                    if(txtQty.Text!="") 
                    dQty = Convert.ToDouble(txtQty.Text);

                    if (txtQty.Text.Length > 0 && dQty!=0)
                    {
                        CommonData.ViewReport = "LOW_DISP_STOCK_POINT_DETAILS";
                        ReportViewer objReportview = new ReportViewer(Company, Branches, "", Convert.ToDateTime(dtpFrmDocMonth.Value).ToString("MMMyyyy").ToUpper(), Convert.ToDateTime(dtpToDocMonth.Value).ToString("MMMyyyy").ToUpper(), Convert.ToInt32(txtQty.Text), "");
                        objReportview.Show();
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Minimum Qty","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        txtQty.Focus();
                    }
                }
                if (sFrmType == "STK_RET_GCGL")
                {

                    CommonData.ViewReport = "SSERP_STK_RETURN_SPVGL";
                    ReportViewer objReportview = new ReportViewer(Company, Branches, Convert.ToDateTime(dtpFrmDocMonth.Value).ToString("MMMyyyy").ToUpper(), Convert.ToDateTime(dtpToDocMonth.Value).ToString("MMMyyyy").ToUpper(), "SPVSGC");
                    objReportview.Show();
                    
                }
                if (sFrmType == "SP_DCST_INTRSTATE_EQLCOMP")
                {

                    CommonData.ViewReport = "SP_DCST_INTRSTATE_EQLCOMP";
                    ReportViewer objReportview = new ReportViewer(Company, Branches, Convert.ToDateTime(dtpFrmDocMonth.Value).ToString("MMMyyyy").ToUpper(), Convert.ToDateTime(dtpToDocMonth.Value).ToString("MMMyyyy").ToUpper(), "INTRSTATE_EQLCOMP");
                    objReportview.Show();

                }
                if (sFrmType == "SP_DCST_INTRSTATE_DIFFCOMP")
                {                    
                    CommonData.ViewReport = "SP_DCST_INTRSTATE_EQLCOMP";
                    ReportViewer objReportview = new ReportViewer(Company, Branches, Convert.ToDateTime(dtpFrmDocMonth.Value).ToString("MMMyyyy").ToUpper(), Convert.ToDateTime(dtpToDocMonth.Value).ToString("MMMyyyy").ToUpper(), "INTRSTATE_DIFFCOMP");
                    objReportview.Show();

                }
                if (sFrmType == "STK_RET_GCGL_PR")
                {

                    CommonData.ViewReport = "SSERP_STK_RETURN_SPVGL_PRWISE";
                    ReportViewer objReportview = new ReportViewer(Company, Branches, Convert.ToDateTime(dtpFrmDocMonth.Value).ToString("MMMyyyy").ToUpper(), Convert.ToDateTime(dtpToDocMonth.Value).ToString("MMMyyyy").ToUpper(), "GC_PR_WISE");
                    objReportview.Show();

                }
                if (sFrmType == "STK_RET_GCGL_BR_SUMM")
                {

                    CommonData.ViewReport = "SSERP_STK_RETURN_SPVGL_BRANCH_WISE";
                    ReportViewer objReportview = new ReportViewer(Company, Branches, Convert.ToDateTime(dtpFrmDocMonth.Value).ToString("MMMyyyy").ToUpper(), Convert.ToDateTime(dtpToDocMonth.Value).ToString("MMMyyyy").ToUpper(), "BR_GC_WISE");
                    objReportview.Show();

                }
                if (sFrmType == "RF_SUMMARY")
                {
                    CommonData.ViewReport = "SP_RF_SUMMARY";
                    ReportViewer objReportview = new ReportViewer(Company, Branches, Convert.ToDateTime(dtpFrmDocMonth.Value).ToString("MMMyyyy"), Convert.ToDateTime(dtpToDocMonth.Value).ToString("MMMyyyy"),"","","");
                    objReportview.Show();
                }
                if (sFrmType == "SP_PENDING_DC")
                {                   
                    CommonData.ViewReport = "STOCKPOINT_DC";
                    ReportViewer objReportview = new ReportViewer(Company, Branches,DocumentMonth, "PENDING");
                    objReportview.Show();
                }
                if (sFrmType == "SPECIAL_APPROVALS_SUMMARY")
                {
                    CommonData.ViewReport = "SSCRM_REP_SPECIAL_APPROVALS_SUMMARY";
                    ReportViewer objReportview = new ReportViewer(Company, Branches, Convert.ToDateTime(dtpFrmDocMonth.Value).ToString("dd/MMM/yyyy"), Convert.ToDateTime(dtpToDocMonth.Value).ToString("dd/MMM/yyyy"), "");
                    objReportview.Show();
                }
                if (sFrmType == "TOUR_EXPENSES")
                {
                    CommonData.ViewReport = "SSCRM_REP_TOUR_EXPENSES_BRANCH_WISE";
                    ReportViewer objReportview = new ReportViewer(Company, Branches, dtpFrmDocMonth.Value.ToString("MMMyyyy").ToUpper(), dtpToDocMonth.Value.ToString("MMMyyyy").ToUpper(), "ALL", "");
                    objReportview.Show();
                }
                if (sFrmType == "RF_STATEMENT")
                {
                    CommonData.ViewReport = "SP_REFILL_STATEMENT_DETLS";
                    ReportViewer objReportview = new ReportViewer(Company, Branches, dtpFrmDocMonth.Value.ToString("MMMyyyy").ToUpper(), dtpToDocMonth.Value.ToString("MMMyyyy").ToUpper(), "", "", "");
                    objReportview.Show();
                }
                if (sFrmType == "SHORTAGE_WRITEOFF")
                {
                    CommonData.ViewReport = "SHORTAGE_WRITEOFF_EXCESS_REG";
                    ReportViewer objReportview = new ReportViewer(Company, Branches, CommonData.FinancialYear, dtpFrmDocMonth.Value.ToString("MMMyyyy").ToUpper(), dtpToDocMonth.Value.ToString("MMMyyyy").ToUpper(), "DETAILED");
                    objReportview.Show();
                }
                if (sFrmType == "PU_DAMAGE_STOCK_DETL")
                {
                    CommonData.ViewReport = "SSERP_REP_SP_VS_PU_DAMAGE_STOCK_DETL";
                    ReportViewer objReportview = new ReportViewer(Company, Branches, Convert.ToDateTime(dtpFrmDocMonth.Value).ToString("dd/MMM/yyyy"), Convert.ToDateTime(dtpToDocMonth.Value).ToString("dd/MMM/yyyy"), "DETAILED");
                    objReportview.Show();
                }
                if (sFrmType == "PU_DAMAGE_STOCK_SUMMARY")
                {
                    CommonData.ViewReport = "SSERP_REP_SP_VS_PU_DAMAGE_STOCK_SUMMARY";
                    ReportViewer objReportview = new ReportViewer(Company, Branches, Convert.ToDateTime(dtpFrmDocMonth.Value).ToString("dd/MMM/yyyy"), Convert.ToDateTime(dtpToDocMonth.Value).ToString("dd/MMM/yyyy"), "SUMMARY");
                    objReportview.Show();
                }
                if (sFrmType == "SP_DCST_WITH_IN_STATE")
                {                  
                    CommonData.ViewReport = "SP_DCST_INTRSTATE_EQLCOMP";
                    ReportViewer objReportview = new ReportViewer(Company, Branches, Convert.ToDateTime(dtpFrmDocMonth.Value).ToString("MMMyyyy").ToUpper(), Convert.ToDateTime(dtpToDocMonth.Value).ToString("MMMyyyy").ToUpper(), "WITH_IN_STATE");
                    objReportview.Show();
                }
                if (sFrmType == "SP_TRANSPORT_COST")
                {
                    CommonData.ViewReport = "SSERP_REP_SP_TRANSPORT_COST_MONTH_WISE";
                    ReportViewer objReportview = new ReportViewer(Company, Branches, Convert.ToDateTime(dtpFrmDocMonth.Value).ToString("MMMyyyy").ToUpper(), Convert.ToDateTime(dtpToDocMonth.Value).ToString("MMMyyyy").ToUpper(), "");
                    objReportview.Show();
                }
                if (sFrmType == "STOCK_TRANSACTIONS_SUM")
                {
                    CommonData.ViewReport = "SSERP_REP_SP_STOCK_TRANSACTIONS_SUMMARY";
                    ReportViewer objReportview = new ReportViewer(Company, Branches, Convert.ToDateTime(dtpFrmDocMonth.Value).ToString("MMMyyyy").ToUpper(), Convert.ToDateTime(dtpToDocMonth.Value).ToString("MMMyyyy").ToUpper(),strTrnTypes, "");
                    objReportview.Show();
                }
                if (sFrmType == "SP_TRNSP_COST_TRIP_WISE")
                {
                    CommonData.ViewReport = "SSERP_REP_SP_TRANSPORT_COST_TRIP_WISE";
                    ReportViewer objReportview = new ReportViewer(Company, Branches, Convert.ToDateTime(dtpFrmDocMonth.Value).ToString("MMMyyyy").ToUpper(), Convert.ToDateTime(dtpToDocMonth.Value).ToString("MMMyyyy").ToUpper(), "");
                    objReportview.Show();
                }
                if (sFrmType == "BRANCH_MEETING")
                {
                    CommonData.ViewReport = "SSERP_REP_BRANCH_MEETING_DETAILS";
                    ReportViewer objReportview = new ReportViewer(Company, Branches, Convert.ToDateTime(dtpFrmDocMonth.Value).ToString("dd/MMM/yyyy").ToUpper(), Convert.ToDateTime(dtpToDocMonth.Value).ToString("dd/MMM/yyyy").ToUpper(), "");
                    objReportview.Show();
                }
                if (sFrmType == "AWARDS_DETL")
                {
                    CommonData.ViewReport = "SSERP_REP_EMP_AWARD_DETL";
                    ReportViewer objReportview = new ReportViewer(Company, Branches, txtQty.Text, Convert.ToDateTime(dtpFrmDocMonth.Value).ToString("dd/MMM/yyyy").ToUpper(), Convert.ToDateTime(dtpToDocMonth.Value).ToString("dd/MMM/yyyy").ToUpper(), strTrnTypes, "");
                    objReportview.Show();
                }
                if (sFrmType == "STOCKPOINT_DC")
                {
                    CommonData.ViewReport = "STOCKPOINT_DC";
                    ReportViewer objReportview = new ReportViewer(Company, Branches, DocumentMonth, "");
                    objReportview.Show();
                }
                if (sFrmType == "STOCKPOINT_DCST")
                {
                    CommonData.ViewReport = "STOCKPOINT_DCST";
                    ReportViewer objReportview = new ReportViewer(Company, Branches, DocumentMonth);
                    objReportview.Show();
                }

                if (sFrmType == "STOCKPOINT_GRN")
                {
                    CommonData.ViewReport = "STOCKPOINT_GRN";
                    ReportViewer objReportview = new ReportViewer(Company, Branches, DocumentMonth);
                    objReportview.Show();
                }
                if (sFrmType == "STATE_VAT_SUM")
                {
                    CommonData.ViewReport = "SSCRM_REP_SALES_REG_VAT_CAL_SUM";
                    ReportViewer objReportview = new ReportViewer(Company, Branches, DocumentMonth, "", "", "VAT_SUM");
                    objReportview.Show();
                }
     
     
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

     

        private void tvBranches_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TriStateTreeView.getStatus(e);

            tvBranches.BeginUpdate();

            foreach (TreeNode Node in e.Node.Nodes)
            {
                Node.Checked = e.Node.Checked;
            }

            tvBranches.EndUpdate();  
           
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FillBranches();
            dtpFrmDocMonth.Value = DateTime.Today;
            txtQty.Text = "";
            dtpToDocMonth.Value = DateTime.Today;
        }


        private void GetSelectedTrnTypes()
        {
            strTrnTypes = "";

            if (chkTrnType.Items.Count > 0)
            {
                for (int i = 0; i < chkTrnType.Items.Count; i++)
                {
                    if (chkTrnType.GetItemCheckState(i) == CheckState.Checked)
                    {                  
                        strTrnTypes+=((SSAdmin.NewCheckboxListItem)(chkTrnType.Items[i])).Tag.ToString() + ',';                        
                    }
                }

                strTrnTypes = strTrnTypes.TrimEnd(',');                
            }
           
        }
      

        private void btnDownload_Click(object sender, EventArgs e)
        {
            GetSelectedValues();
            GetSelectedTrnTypes();

            if (CheckData() == true)
            {

                #region " sFrmType :: Stock Point Transport Cost Summary"

                if (sFrmType == "SP_TRANSPORT_COST")
                {
                    DataTable dtExcel = new DataTable();
                    objExDb = new ExcelDB();
                    objUtilityDB = new UtilityDB();
                    dtExcel = objExDb.Get_Sp_TransportCost_Summary(Company, Branches, Convert.ToDateTime(dtpFrmDocMonth.Value).ToString("MMMyyyy").ToUpper(), Convert.ToDateTime(dtpToDocMonth.Value).ToString("MMMyyyy").ToUpper(), "").Tables[0];
                    objExDb = null;
                    if (dtExcel.Rows.Count > 0)
                    {
                        try
                        {
                            Excel.Application oXL = new Excel.Application();
                            Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                            Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                            oXL.Visible = true;
                            int iTotColumns = 0;
                            iTotColumns = 33;
                            string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                            Excel.Range rgHead = null;
                            Excel.Range rg = worksheet.get_Range("A4", sLastColumn + "4");
                            Excel.Range rgData = worksheet.get_Range("A5", sLastColumn + (dtExcel.Rows.Count + 5).ToString());
                            rgData.Font.Size = 11;
                            rgData.WrapText = true;
                            rgData.VerticalAlignment = Excel.Constants.xlCenter;
                            rgData.Borders.Weight = 2;

                            rgData = worksheet.get_Range("A1", "V1");
                            rgData.Merge(Type.Missing);
                            rgData.Font.Bold = true; rgData.Font.Size = 14;
                            rgData.Value2 = " Transport Cost For the Month Of  " + dtExcel.Rows[0]["sp_Document_Month"].ToString() + " ";
                            rgData.VerticalAlignment = Excel.Constants.xlCenter;
                            rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                            rgData.RowHeight = 25;
                            rgData.Font.ColorIndex = 9;
                            rgData = worksheet.get_Range("A5", "A3");
                            rgData.Merge(Type.Missing);
                            rgData.ColumnWidth = 80;
                            rgData.RowHeight = 20;

                            rg = worksheet.get_Range("A5", "A3");
                            rg.Merge(Type.Missing);
                            rg.Value2 = "Sl.No";
                            rg.WrapText = true;
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Cells.ColumnWidth = 5;
                            rg = worksheet.get_Range("B5", "B3");
                            rg.Merge(Type.Missing);
                            rg.Value2 = "SP Name";
                            rg.WrapText = true;
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Cells.ColumnWidth = 30;
                            rg = worksheet.get_Range("C3", "Q3");
                            rg.Merge(Type.Missing);
                            rg.Value2 = "TRANSPORT COST";
                            rg.WrapText = true;
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg = worksheet.get_Range("C4", "E4");
                            rg.Merge(Type.Missing);
                            rg.Value2 = "Gromin";
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg = worksheet.get_Range("C5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Value2 = "Units";
                            rg.WrapText = true;
                            rg.Cells.ColumnWidth = 8;
                            rg = worksheet.get_Range("D5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Value2 = "Exp";
                            rg.WrapText = true;
                            rg.Cells.ColumnWidth = 8;
                            rg = worksheet.get_Range("E5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Value2 = "CPU";
                            rg.WrapText = true;
                            rg.Cells.ColumnWidth = 8;
                            rg = worksheet.get_Range("F4", "H4");
                            rg.Merge(Type.Missing);
                            rg.Value2 = "Other than Gromin";
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg = worksheet.get_Range("F5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Value2 = "Units";
                            rg.WrapText = true;
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("G5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Value2 = "Exp";
                            rg.WrapText = true;
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("H5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Value2 = "CPU";
                            rg.WrapText = true;
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("I4", "Q4");
                            rg.Merge(Type.Missing);
                            rg.Value2 = "Total";
                            rg.WrapText = true;
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Cells.ColumnWidth = 6;
                            rg = worksheet.get_Range("I5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Merge(Type.Missing);
                            rg.Value2 = "Total Units";
                            rg.WrapText = true;
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("j5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Value2 = "GSP Exp";
                            rg.WrapText = true;
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("K5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Value2 = "BR Exp";
                            rg.WrapText = true;
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("L5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Value2 = "Total Exp";
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("M5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Value2 = "Total Tons";
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("N5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Value2 = "Total KM's";
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("O5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Value2 = "CPU";
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("P5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Value2 = "CPT";
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("Q5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Value2 = "CPK";
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("R4", "U3");
                            rg.Merge(Type.Missing);
                            rg.Value2 = "EXPENSES";
                            rg.WrapText = true;
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Cells.ColumnWidth = 8;
                            rg = worksheet.get_Range("R5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Merge(Type.Missing);
                            rg.Value2 = "Gromin";
                            rg.WrapText = true;
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("S5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Value2 = "Granuals";
                            rg.WrapText = true;
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("T5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Value2 = "Liquids";
                            rg.WrapText = true;
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("U5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Value2 = "Total";
                            rg.Cells.ColumnWidth = 10;

                            rg = worksheet.get_Range("V4", "Y3");
                            rg.Merge(Type.Missing);
                            rg.Value2 = "UNITS";
                            rg.WrapText = true;
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Cells.ColumnWidth = 8;
                            rg = worksheet.get_Range("V5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Merge(Type.Missing);
                            rg.Value2 = "Gromin";
                            rg.WrapText = true;
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("W5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Value2 = "Granuals";
                            rg.WrapText = true;
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("X5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Value2 = "Liquids";
                            rg.WrapText = true;
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("Y5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Value2 = "Total";
                            rg.Cells.ColumnWidth = 10;

                            rg = worksheet.get_Range("Z4", "AC3");
                            rg.Merge(Type.Missing);
                            rg.Value2 = "Kgs / Ltrs";
                            rg.WrapText = true;
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Cells.ColumnWidth = 8;
                            rg = worksheet.get_Range("Z5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Merge(Type.Missing);
                            rg.Value2 = "Gromin";
                            rg.WrapText = true;
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("AA5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Value2 = "Granuals";
                            rg.WrapText = true;
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("AB5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Value2 = "Liquids";
                            rg.WrapText = true;
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("AC5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Value2 = "Total";
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("AD4", "AG3");
                            rg.Merge(Type.Missing);
                            rg.Value2 = "COST PER UNIT";
                            rg.WrapText = true;
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Cells.ColumnWidth = 8;
                            rg = worksheet.get_Range("AD5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Merge(Type.Missing);
                            rg.Value2 = "Gromin";
                            rg.WrapText = true;
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("AE5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Value2 = "Granuals";
                            rg.WrapText = true;
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("AF5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Value2 = "Liquids";
                            rg.WrapText = true;
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("AG5", Type.Missing);
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Value2 = "Total";
                            rg.Cells.ColumnWidth = 10;

                            int iColumn = 1, iStartRow = 6;


                            for (int i = 0; i < dtExcel.Rows.Count; i++)
                            {
                                worksheet.Cells[iStartRow, iColumn++] = i + 1;
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_branch_name"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Gromin_Prod_Qty"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Tot_Gromin_Exp"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Gromin_Prod_CPU"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Other_Prod_Units"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Other_Prod_Exp"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Other_Prod_CPU"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Total_Despatch_Units"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_GSP_Exp"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Branch_Exp"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Tot_Expenses"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Total_Tons"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Total_Kms"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Tot_Cost_Per_Unit"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Tot_Cost_Per_Ton"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Tot_Cost_Per_KM"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Tot_Gromin_Exp"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Tot_Granual_Exp"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Tot_Liquids_Exp"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Tot_Expenses"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Gromin_Prod_Qty"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Other_Prod_Qty"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["SP_Liquids_Qty"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Total_Despatch_Units"].ToString();

                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Gromin_Tot_in_Kgs"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Granuals_Tot_in_Kgs"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Liquids_Tot_in_Ltrs"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Tot_Qty_in_Kgs"].ToString();

                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Gromin_Cost_Per_Unit"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Granuals_Cost_Per_Unit"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Liquids_Cost_Per_Unit"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sp_Cost_Per_Unit"].ToString();

                                iStartRow++; iColumn = 1;
                            }

                            iStartRow = 3;
                            iColumn = iStartRow;
                            rgHead = worksheet.get_Range("C" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString(),
                                                    "AG" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString());

                            rg = worksheet.get_Range("A" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString(),
                                                    "B" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString());
                            rg.Merge(Type.Missing);
                            rg.Value2 = "Total";
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 14;
                            rg.Font.ColorIndex = 30;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;

                            rgHead.Borders.Weight = 2;
                            rgHead.Font.Size = 12; rgHead.Font.Bold = true;
                            double TotGromin_Units = 0, TotGranuals_Units = 0, TotLiquid_Units = 0, TotKms = 0, TotTons = 0,
                                TotExp = 0, TotKgs = 0, TotUnits = 0, TotGromin_Exp = 0, TotGranuals_Exp = 0, TotLiquid_Exp = 0,
                                OtherProd_Tot_Units = 0, OtherProd_Tot_Exp = 0;

                            for (int j = 0; j < Convert.ToInt32(dtExcel.Rows.Count); j++)
                            {
                                TotGromin_Units += Convert.ToDouble(dtExcel.Rows[j]["sp_Gromin_Prod_Qty"]);
                                TotGranuals_Units += Convert.ToDouble(dtExcel.Rows[j]["sp_Other_Prod_Qty"]);
                                TotLiquid_Units += Convert.ToDouble(dtExcel.Rows[j]["SP_Liquids_Qty"]);
                                TotUnits += Convert.ToDouble(dtExcel.Rows[j]["sp_Total_Despatch_Units"]);
                                TotGromin_Exp += Convert.ToDouble(dtExcel.Rows[j]["sp_Tot_Gromin_Exp"]);
                                TotGranuals_Exp += Convert.ToDouble(dtExcel.Rows[j]["sp_Tot_Granual_Exp"]);
                                TotLiquid_Exp += Convert.ToDouble(dtExcel.Rows[j]["sp_Tot_Liquids_Exp"]);
                                TotExp += Convert.ToDouble(dtExcel.Rows[j]["sp_Tot_Expenses"]);
                                TotTons += Convert.ToDouble(dtExcel.Rows[j]["sp_Total_Tons"]);
                                TotKms += Convert.ToDouble(dtExcel.Rows[j]["sp_Total_Kms"]);
                                OtherProd_Tot_Units += Convert.ToDouble(dtExcel.Rows[j]["sp_Other_Prod_Units"]);
                                OtherProd_Tot_Exp += Convert.ToDouble(dtExcel.Rows[j]["sp_Other_Prod_Exp"]);

                                iStartRow = 3; iColumn = iStartRow;
                                worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                iColumn = iColumn + 1;
                                worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                iColumn = iColumn + 1;
                                if (TotGromin_Units != 0)
                                {
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = (TotGromin_Exp / TotGromin_Units).ToString("0.00");
                                }
                                else
                                {
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "0";
                                }
                                iColumn = iColumn + 1;
                                worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                iColumn = iColumn + 1;
                                worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                iColumn = iColumn + 1;
                                if (OtherProd_Tot_Units != 0)
                                {
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = (OtherProd_Tot_Exp / OtherProd_Tot_Units).ToString("0.00");
                                }
                                else
                                {
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "0";
                                }
                                iColumn = iColumn + 1;
                                worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                iColumn = iColumn + 1;
                                worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                iColumn = iColumn + 1;
                                worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                iColumn = iColumn + 1;
                                worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                iColumn = iColumn + 1;
                                worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                iColumn = iColumn + 1;
                                worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                iColumn = iColumn + 1;
                                //Cost Per UNit Calculation
                                if (TotUnits != 0)
                                {
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = (TotExp / TotUnits).ToString("0.00");
                                }
                                else
                                {
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "0";
                                }
                                iColumn = iColumn + 1;
                                // Cost Per Ton Calculation
                                if (TotTons != 0)
                                {
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = (TotExp / TotTons).ToString("0.00");
                                }
                                else
                                {
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "0";
                                }
                                iColumn = iColumn + 1;
                                // Cost Per KM Calculation
                                if (TotKms != 0)
                                {
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = (TotExp / TotKms).ToString("0.00");
                                }
                                else
                                {
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "0";
                                }
                                iColumn = iColumn + 1;
                                worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                iColumn = iColumn + 1;
                                worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                iColumn = iColumn + 1;
                                worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                iColumn = iColumn + 1;
                                worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                iColumn = iColumn + 1;
                                worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                iColumn = iColumn + 1;
                                worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                iColumn = iColumn + 1;
                                worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                iColumn = iColumn + 1;
                                worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                iColumn = iColumn + 1;
                                worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                iColumn = iColumn + 1;
                                worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                iColumn = iColumn + 1;
                                worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                iColumn = iColumn + 1;
                                worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                iColumn = iColumn + 1;
                                //Cost Per Units Calculation
                                if (TotGromin_Units != 0)
                                {
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = (TotGromin_Exp / TotGromin_Units).ToString("0.00");
                                }
                                else
                                {
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "0";
                                }
                                iColumn = iColumn + 1;

                                if (TotGranuals_Units != 0)
                                {
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = (TotGranuals_Exp / TotGranuals_Units).ToString("0.00");
                                }
                                else
                                {
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "0";
                                }
                                iColumn = iColumn + 1;


                                if (TotLiquid_Units != 0)
                                {
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = (TotLiquid_Exp / TotLiquid_Units).ToString("0.00");
                                }
                                else
                                {
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "0";
                                }
                                iColumn = iColumn + 1;
                                if (TotUnits != 0)
                                {
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = (TotExp / TotUnits).ToString("0.00");
                                }
                                else
                                {
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "0";
                                }
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

                #region " sFrmType :: Stock Transactions Summary"

                if (sFrmType == "STOCK_TRANSACTIONS_SUM")
                {
                    DataTable dtExcel = new DataTable();
                    objExDb = new ExcelDB();
                    objUtilityDB = new UtilityDB();
                    dtExcel = objExDb.Get_SP_Stock_Transactions_Sum(Company, Branches, Convert.ToDateTime(dtpFrmDocMonth.Value).ToString("MMMyyyy").ToUpper(), Convert.ToDateTime(dtpToDocMonth.Value).ToString("MMMyyyy").ToUpper(), strTrnTypes, "").Tables[0];
                    objExDb = null;
                    if (dtExcel.Rows.Count > 0)
                    {
                        try
                        {
                            Excel.Application oXL = new Excel.Application();
                            Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                            Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                            oXL.Visible = true;
                            int iTotColumns = 0;
                            iTotColumns = 3 + (3 * Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_Products"])) + 3;
                            string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                            Excel.Range rg = worksheet.get_Range("A3", sLastColumn + "3");
                            Excel.Range rgData = worksheet.get_Range("A3", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_SPs"]) + 3).ToString());
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
                            rgData = worksheet.get_Range("A4", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_SPs"]) + 3).ToString());
                            rgData.WrapText = false;
                            rg = worksheet.get_Range("A3", Type.Missing);
                            rg.Cells.ColumnWidth = 4;
                            rg = worksheet.get_Range("B3", Type.Missing);
                            rg.Cells.ColumnWidth = 8;
                            rg = worksheet.get_Range("C3", Type.Missing);
                            rg.Cells.ColumnWidth = 45;
                            rg.WrapText = true;


                            int iColumn = 1;
                            worksheet.Cells[3, iColumn++] = "SlNo";
                            worksheet.Cells[3, iColumn++] = "State";
                            worksheet.Cells[3, iColumn++] = "Stock Point Name";

                            Excel.Range rgHead;
                            int iStartColumn = 0;
                            for (int iProd = 0; iProd < Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_Products"]); iProd++)
                            {
                                rgHead = worksheet.get_Range("A1", "K1");
                                rgHead.Merge(Type.Missing);
                                rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Cells.RowHeight = 30;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.Value2 = "STOCK TRANSACTIONS SUMMARY \t  FROM  " + (dtpFrmDocMonth.Value).ToString("MMMyyyy").ToUpper() + " \t  TO  " + (dtpToDocMonth.Value).ToString("MMMyyyy").ToUpper() + " ";


                                iStartColumn = (3 * iProd) + 4;

                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "2", objUtilityDB.GetColumnName(iStartColumn + 2) + "2");


                                rgHead.Merge(Type.Missing);
                                rgHead.Interior.ColorIndex = 34 + 1;
                                rgHead.Borders.Weight = 2;
                                rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
                                rgHead.Cells.RowHeight = 30;
                                rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;


                                worksheet.Cells[3, iStartColumn++] = "Good";
                                worksheet.Cells[3, iStartColumn++] = "Damage";
                                worksheet.Cells[3, iStartColumn++] = "Total";


                            }

                            rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "2", objUtilityDB.GetColumnName(iStartColumn + 2) + "2");
                            rgHead.Merge(Type.Missing);
                            rgHead.Interior.ColorIndex = 34 + 1;
                            rgHead.Borders.Weight = 2;
                            rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
                            rgHead.Cells.RowHeight = 30;
                            rgHead.Font.Size = 14;
                            rgHead.Font.ColorIndex = 1;
                            rgHead.Font.Bold = true;
                            rgHead.Value2 = "TOTAL";
                            rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                            rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;

                            worksheet.Cells[3, iStartColumn++] = "Good";
                            worksheet.Cells[3, iStartColumn++] = "Damage";
                            worksheet.Cells[3, iStartColumn++] = "Total";



                            int iRowCounter = 4; int iColumnCounter = 1;
                            for (int i = 0; i < dtExcel.Rows.Count; i++)
                            {
                                if (i > 0)
                                {

                                    if (dtExcel.Rows[i]["sp_branch_code"].ToString() == dtExcel.Rows[i - 1]["sp_branch_code"].ToString())
                                    {
                                        int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sp_Prod_sl_no"]);
                                        //int iStartColumn = 0;
                                        iColumnCounter = (3 * (iMonthNo - 1)) + 4;
                                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 2) + "2");
                                        rgHead.Cells.Value2 = dtExcel.Rows[i]["sp_product_Name"];
                                        rgHead.WrapText = true;

                                        rgHead.Interior.ColorIndex = 14 + iMonthNo;
                                        rgHead.Font.ColorIndex = 1;
                                        rgHead.Font.Bold = true;
                                        rgHead.Borders.Weight = 2;
                                        //rgHead.Interior.ColorIndex = 31;
                                        //rgHead.Font.ColorIndex = 2;
                                        rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                        rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;


                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Good_qty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Damage_qty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Qty"];
                                    }

                                    else
                                    {

                                        iRowCounter++;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 3;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_state_code"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_branch_name"];

                                        int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sp_Prod_sl_no"]);

                                        iColumnCounter = (3 * (iMonthNo - 1)) + 4;
                                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 2) + "2");
                                        rgHead.Cells.Value2 = dtExcel.Rows[i]["sp_product_Name"];
                                        rgHead.WrapText = true;

                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Good_qty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Damage_qty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Qty"];

                                        iColumnCounter = iTotColumns - 2;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_tot_Good_Qty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_tot_Damage_Qty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Total_Qty"];
                                    }
                                }
                                else
                                {

                                    worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 3;
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_state_code"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_branch_name"];

                                    int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sp_Prod_sl_no"]);
                                    //int iStartColumn = 0;
                                    iColumnCounter = (3 * (iMonthNo - 1)) + 4;
                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 2) + "2");
                                    rgHead.Cells.Value2 = dtExcel.Rows[i]["sp_product_Name"];
                                    rgHead.WrapText = true;
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Good_qty"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Damage_qty"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Qty"];

                                    iColumnCounter = iTotColumns - 2;
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_tot_Good_Qty"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_tot_Damage_Qty"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Total_Qty"];

                                }

                                iColumnCounter = 1;
                            }


                            iStartColumn = 3 + (3 * (Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_Products"]))) + 3;
                            iColumnCounter = iStartColumn;
                            rgHead = worksheet.get_Range("D" + (Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_SPs"]) + 4).ToString(),
                                                   objUtilityDB.GetColumnName(iStartColumn) + (Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_SPs"]) + 4).ToString());
                            rgHead.Borders.Weight = 2;
                            rgHead.Font.Size = 12; rgHead.Font.Bold = true;


                            rg = worksheet.get_Range("A" + (Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_SPs"]) + 4).ToString(),
                                                    "C" + (Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_SPs"]) + 4).ToString());
                            rg.Merge(Type.Missing);
                            rg.Value2 = "Total";
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 14;
                            rg.Font.ColorIndex = 30;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;


                            for (int iProduct = 0; iProduct <= Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_Products"]); iProduct++)
                            {
                                iStartColumn = (3 * iProduct) + 4; iColumnCounter = iStartColumn;
                                worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_SPs"]) + 4, iColumnCounter] = "=SUM(" + objUtilityDB.GetColumnName(iColumnCounter) + "3:" + objUtilityDB.GetColumnName(iColumnCounter) + "" + (Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_SPs"]) + 3).ToString() + ")";
                                iColumnCounter = iColumnCounter + 1;
                                worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_SPs"]) + 4, iColumnCounter] = "=SUM(" + objUtilityDB.GetColumnName(iColumnCounter) + "3:" + objUtilityDB.GetColumnName(iColumnCounter) + "" + (Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_SPs"]) + 3).ToString() + ")";
                                iColumnCounter = iColumnCounter + 1;
                                worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_SPs"]) + 4, iColumnCounter] = "=SUM(" + objUtilityDB.GetColumnName(iColumnCounter) + "3:" + objUtilityDB.GetColumnName(iColumnCounter) + "" + (Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_SPs"]) + 3).ToString() + ")";
                                iColumnCounter = iColumnCounter + 1;

                            }

                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }

                #endregion

                #region " sFrmType :: SP Transport Cost Trip Wise"

                if (sFrmType == "SP_TRNSP_COST_TRIP_WISE")
                {
                    DataTable dtExcel = new DataTable();
                    objExDb = new ExcelDB();
                    objUtilityDB = new UtilityDB();
                    dtExcel = objExDb.Get_Sp_Trip_Wise_TransportCost(Company, Branches, Convert.ToDateTime(dtpFrmDocMonth.Value).ToString("MMMyyyy").ToUpper(), Convert.ToDateTime(dtpToDocMonth.Value).ToString("MMMyyyy").ToUpper(), "EXCEL_DOWNLOAD").Tables[0];
                    objExDb = null;
                    if (dtExcel.Rows.Count > 0)
                    {
                        try
                        {
                            Excel.Application oXL = new Excel.Application();
                            Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                            Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                            oXL.Visible = true;
                            int iTotColumns = 0;
                            iTotColumns = 32 + (2 * Convert.ToInt32(dtExcel.Rows[0]["sp_NoOf_Products"])) + 2;
                            string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                            Excel.Range rg = worksheet.get_Range("A3", sLastColumn + "3");
                            Excel.Range rgData = worksheet.get_Range("A3", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_Trips"]) + 3).ToString());
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
                            rgData = worksheet.get_Range("A4", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_Trips"]) + 3).ToString());
                            rgData.WrapText = false;
                            rg = worksheet.get_Range("A3", Type.Missing);
                            rg.Cells.ColumnWidth = 4;
                            rg = worksheet.get_Range("B3", Type.Missing);
                            rg.Cells.ColumnWidth = 45;
                            rg = worksheet.get_Range("C3", Type.Missing);
                            rg.Cells.ColumnWidth = 8;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("D3", Type.Missing);
                            rg.Cells.ColumnWidth = 20;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("E3", Type.Missing);
                            rg.Cells.ColumnWidth = 35;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("F3", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("G3", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("H3", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("I3", Type.Missing);
                            rg.Cells.ColumnWidth = 15;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("J3", Type.Missing);
                            rg.Cells.ColumnWidth = 20;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("K3", Type.Missing);
                            rg.Cells.ColumnWidth = 12;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("L3", Type.Missing);
                            rg.Cells.ColumnWidth = 12;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("M3", Type.Missing);
                            rg.Cells.ColumnWidth = 12;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("N3", Type.Missing);
                            rg.Cells.ColumnWidth = 12;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("O3", Type.Missing);
                            rg.Cells.ColumnWidth = 12;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("P3", Type.Missing);
                            rg.Cells.ColumnWidth = 12;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("Q3", Type.Missing);
                            rg.Cells.ColumnWidth = 12;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("R3", Type.Missing);
                            rg.Cells.ColumnWidth = 30;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("S3", Type.Missing);
                            rg.Cells.ColumnWidth = 30;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("T3", Type.Missing);
                            rg.Cells.ColumnWidth = 8;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("U3", Type.Missing);
                            rg.Cells.ColumnWidth = 25;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("V3", Type.Missing);
                            rg.Cells.ColumnWidth = 30;
                            rg = worksheet.get_Range("W3", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("X3", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("Y3", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("Z3", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("AA3", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("AB3", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("AC3", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("AD3", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("AE3", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("AF3", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.WrapText = true;




                            int iColumn = 1;
                            worksheet.Cells[3, iColumn++] = "SlNo";
                            worksheet.Cells[3, iColumn++] = "Stock Point Name";
                            worksheet.Cells[3, iColumn++] = "State";
                            worksheet.Cells[3, iColumn++] = "Trn Type";
                            worksheet.Cells[3, iColumn++] = "Supplied To (BR/SP/PU)";
                            worksheet.Cells[3, iColumn++] = "No.Of DC/DCST'S";
                            worksheet.Cells[3, iColumn++] = "Trip/LRNo";
                            worksheet.Cells[3, iColumn++] = "Trip Start Date";
                            worksheet.Cells[3, iColumn++] = "Vehicle No";
                            worksheet.Cells[3, iColumn++] = "Vehicle Type";
                            worksheet.Cells[3, iColumn++] = "Starting Metre Reading";
                            worksheet.Cells[3, iColumn++] = "Ending Metre Reading";
                            worksheet.Cells[3, iColumn++] = "No Of KM's Veh Running";
                            worksheet.Cells[3, iColumn++] = "Hire Charges";
                            worksheet.Cells[3, iColumn++] = "Diesel Charges";
                            worksheet.Cells[3, iColumn++] = "Other Exp";
                            worksheet.Cells[3, iColumn++] = "Total Exp";
                            worksheet.Cells[3, iColumn++] = "Transporter Name";
                            worksheet.Cells[3, iColumn++] = "Driver Name";
                            worksheet.Cells[3, iColumn++] = "GC/GL Ecode";
                            worksheet.Cells[3, iColumn++] = "GC/GL Name";
                            worksheet.Cells[3, iColumn++] = "DC/DCST No";
                            worksheet.Cells[3, iColumn++] = "DC/DCST Ref No";
                            worksheet.Cells[3, iColumn++] = "Total Units";
                            worksheet.Cells[3, iColumn++] = "Gromin Qty";
                            worksheet.Cells[3, iColumn++] = "Granuals Qty";
                            worksheet.Cells[3, iColumn++] = "Liquids Qty";
                            worksheet.Cells[3, iColumn++] = "Total Kg's";
                            worksheet.Cells[3, iColumn++] = "Total Tons";
                            worksheet.Cells[3, iColumn++] = "Cost Per Unit";
                            worksheet.Cells[3, iColumn++] = "Cost Per Ton";
                            worksheet.Cells[3, iColumn++] = "Cost Per KM";


                            Excel.Range rgHead;
                            int iStartColumn = 0;
                            for (int iProd = 0; iProd < Convert.ToInt32(dtExcel.Rows[0]["sp_NoOf_Products"]); iProd++)
                            {
                                rgHead = worksheet.get_Range("A1", "K1");
                                rgHead.Merge(Type.Missing);
                                rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Cells.RowHeight = 30;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.Value2 = "TRIP WISE TRANSPORT COST FOR THE MONTH OF  " + (dtpFrmDocMonth.Value).ToString("MMMyyyy").ToUpper();


                                iStartColumn = (2 * iProd) + 33;

                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "2", objUtilityDB.GetColumnName(iStartColumn + 1) + "2");


                                rgHead.Merge(Type.Missing);
                                rgHead.Interior.ColorIndex = 34 + 1;
                                rgHead.Borders.Weight = 2;
                                rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
                                rgHead.Cells.RowHeight = 30;
                                rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;

                                worksheet.Cells[3, iStartColumn++] = "Good";
                                worksheet.Cells[3, iStartColumn++] = "Damage";


                            }

                            rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "2", objUtilityDB.GetColumnName(iStartColumn + 1) + "2");
                            rgHead.Merge(Type.Missing);
                            rgHead.Interior.ColorIndex = 34 + 1;
                            rgHead.Borders.Weight = 2;
                            rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
                            rgHead.Cells.RowHeight = 30;
                            rgHead.Font.Size = 14;
                            rgHead.Font.ColorIndex = 1;
                            rgHead.Font.Bold = true;
                            rgHead.Value2 = "TOTAL";
                            rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                            rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;

                            worksheet.Cells[3, iStartColumn++] = "Good";
                            worksheet.Cells[3, iStartColumn++] = "Damage";




                            int iRowCounter = 4; int iColumnCounter = 1;
                            for (int i = 0; i < dtExcel.Rows.Count; i++)
                            {
                                if (i > 0)
                                {

                                    if (dtExcel.Rows[i]["sp_Trn_No"].ToString() == dtExcel.Rows[i - 1]["sp_Trn_No"].ToString())
                                    {
                                        int iProd = Convert.ToInt32(dtExcel.Rows[i]["sp_Prod_SlNo"]);
                                        //int iStartColumn = 0;
                                        iColumnCounter = (2 * (iProd - 1)) + 33;
                                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 1) + "2");
                                        rgHead.Cells.Value2 = dtExcel.Rows[i]["sp_product_name"];
                                        rgHead.WrapText = true;
                                        rgHead.Cells.RowHeight = 50;
                                        rgHead.Interior.ColorIndex = 35;
                                        rgHead.Font.ColorIndex = 1;
                                        rgHead.Font.Bold = true;
                                        rgHead.Borders.Weight = 2;
                                        //rgHead.Interior.ColorIndex = 31;
                                        //rgHead.Font.ColorIndex = 2;
                                        rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                        rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;


                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Good_Qty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Damage_Qty"];
                                    }

                                    else
                                    {
                                        worksheet.Cells.WrapText = true;

                                        iRowCounter++;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 3;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_branch_name"];

                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_state_code"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Vehicle_Purpose"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_To_Branch_Name"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_No_Of_DCDCST"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_TripLRNo"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToDateTime(dtExcel.Rows[i]["sp_trip_start_date"]).ToString("dd/MMM/yyyy");
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Vehicle_No"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Vehicle_Model"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_start_metre_reading"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_end_metre_reading"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Total_Kms"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_hire_Exp"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Diesel_Exp"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Other_Exp"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Transport_Cost"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Transporter_Name"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Driver_Name"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_GL_Ecode"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_GL_Name"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_DCDCST_No"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_DCDCST_Ref_No"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Total_Despatch_Units"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Gromin_Prod_Qty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Other_Prod_Qty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["SP_Liquids_Qty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Qty_in_Kgs"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Total_Tons"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Cost_Per_Unit"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Cost_Per_Ton"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Cost_Per_KM"];


                                        int iProd = Convert.ToInt32(dtExcel.Rows[i]["sp_Prod_SlNo"]);

                                        iColumnCounter = (2 * (iProd - 1)) + 33;
                                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 1) + "2");
                                        rgHead.Cells.Value2 = dtExcel.Rows[i]["sp_product_name"];
                                        rgHead.WrapText = true;
                                        rgHead.Cells.RowHeight = 50;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Good_Qty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Damage_Qty"];

                                        iColumnCounter = iTotColumns - 1;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Good_Qty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Damage_Qty"];

                                    }
                                }
                                else
                                {
                                    worksheet.Cells.WrapText = true;
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 3;
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_branch_name"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_state_code"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Vehicle_Purpose"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_To_Branch_Name"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_No_Of_DCDCST"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_TripLRNo"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToDateTime(dtExcel.Rows[i]["sp_trip_start_date"]).ToString("dd/MMM/yyyy");
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Vehicle_No"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Vehicle_Model"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_start_metre_reading"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_end_metre_reading"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Total_Kms"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_hire_Exp"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Diesel_Exp"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Other_Exp"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Transport_Cost"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Transporter_Name"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Driver_Name"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_GL_Ecode"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_GL_Name"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_DCDCST_No"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_DCDCST_Ref_No"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Total_Despatch_Units"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Gromin_Prod_Qty"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Other_Prod_Qty"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["SP_Liquids_Qty"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Qty_in_Kgs"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Total_Tons"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Cost_Per_Unit"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Cost_Per_Ton"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Cost_Per_KM"];

                                    int iProd = Convert.ToInt32(dtExcel.Rows[i]["sp_Prod_SlNo"]);
                                    //int iStartColumn = 0;
                                    iColumnCounter = (2 * (iProd - 1)) + 33;
                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 1) + "2");
                                    rgHead.Cells.Value2 = dtExcel.Rows[i]["sp_product_name"];
                                    rgHead.WrapText = true;
                                    rgHead.Cells.RowHeight = 50;
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Good_Qty"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Damage_Qty"];

                                    iColumnCounter = iTotColumns - 1;
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Good_Qty"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Damage_Qty"];

                                }

                                iColumnCounter = 1;
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
            
        }

        private void chkTrnType_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (sFrmType == "AWARDS_DETL")
            {
                for (int i = 0; i < chkTrnType.Items.Count; i++)
                {
                    if (e.Index != i)
                        chkTrnType.SetItemCheckState(i, CheckState.Unchecked);
                }
            }
            else
            {
            }
        }
      
   
      
    }
}
