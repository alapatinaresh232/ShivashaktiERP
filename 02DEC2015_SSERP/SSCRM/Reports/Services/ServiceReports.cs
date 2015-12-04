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

namespace SSCRM
{
    public partial class ServiceReports : Form
    {
        SQLDB objSQLdb = null;
        InvoiceDB objInv = null;
        ReportViewer childReportViewer = null;

        private string Company = string.Empty;
        private string Branches = string.Empty;
        private string DocumentMonth = string.Empty;
        string sFrmType = "";

        public ServiceReports()
        {
            InitializeComponent();
        }
         public ServiceReports(string sType)
        {
            InitializeComponent();
            sFrmType = sType;
        }        

        private void ServiceReports_Load(object sender, EventArgs e)
        {
            
            tvDocMonth.Visible = true;
            cbReportType.SelectedIndex=2;
            dtpFromDate.Visible = false;
            dtpToDate.Visible = false;
            lblFromDate.Visible = false;
            lblToDate.Visible = false;
           

            FillBranches();
            FillDocumentMonths();
            if (sFrmType == "CHR")
            {
                lblRepName.Text = "CUSTOMER HISTORY RECORD";
                cbReportType.SelectedIndex = 2;
               
            }
            else if (sFrmType == "FARMER_MEET")
            {
                lblRepName.Text = "FARMER MEETINGS";
                cbReportType.SelectedIndex = 2;
            }
            else if (sFrmType == "DEMO_PLOT")
            {
                lblRepName.Text = "DEMO PLOTS";
                cbReportType.SelectedIndex = 2;
                cbReportType.Enabled = false;
            }
            else if (sFrmType == "PROD_PRM")
            {
                lblRepName.Text = "PRODUCT PROMOTIONS";
                cbReportType.SelectedIndex = 2;
                cbReportType.Enabled = false;
            }
            else
            {
                cbReportType.Enabled = true; 
            }
        }
        private void FillBranches()
        {
            objInv = new InvoiceDB();
            DataSet ds = new DataSet();
            try
            {
                ds = objInv.UserBranchCursor_Get("", "", "PARENT");

                if (ds.Tables[0].Rows.Count > 0)
                {
                   tvBranches.Nodes.Add("COMPANY AND BRANCHES", "COMPANY AND BRANCHES");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tvBranches.Nodes[0].Nodes.Add(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), ds.Tables[0].Rows[i]["COMPANY_NAME"].ToString());

                        DataSet dschild = new DataSet();
                        dschild = objInv.UserBranchCursor_Get(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "BR", "CHILD");

                        if (dschild.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                            {
                                tvBranches.Nodes[0].Nodes[i].Nodes.Add(dschild.Tables[0].Rows[j]["BRANCH_CODE"].ToString(), dschild.Tables[0].Rows[j]["BRANCH_NAME"].ToString());
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            tvBranches.Nodes[0].Expand();
        }

        private void FillDocumentMonths()
        {
            objSQLdb = new SQLDB();
            DataSet ds = new DataSet();
            DataSet dschild = new DataSet();
            string strSQL = "";
            try
            {
                strSQL = "SELECT DISTINCT FY_FIN_YEAR FROM FIN_YEAR";
                ds = objSQLdb.ExecuteDataSet(strSQL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tvDocMonth.Nodes.Add("Document Months", "Document Months");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tvDocMonth.Nodes[0].Nodes.Add(ds.Tables[0].Rows[i]["FY_FIN_YEAR"].ToString(), ds.Tables[0].Rows[i]["FY_FIN_YEAR"].ToString());
                        strSQL = "";
                        strSQL = "SELECT DISTINCT DOCUMENT_MONTH,start_date FROM DOCUMENT_MONTH WHERE FIN_YEAR = '" + ds.Tables[0].Rows[i]["FY_FIN_YEAR"].ToString() + "' ORDER BY START_DATE ASC";
                        dschild = objSQLdb.ExecuteDataSet(strSQL);
                        if (dschild.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                            {
                                tvDocMonth.Nodes[0].Nodes[i].Nodes.Add(dschild.Tables[0].Rows[j]["DOCUMENT_MONTH"].ToString(), dschild.Tables[0].Rows[j]["DOCUMENT_MONTH"].ToString());
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            tvDocMonth.Nodes[0].Expand();

        }

     
        private bool CheckData()
        {
            bool flag = false;

            for (int i = 0; i < tvBranches.Nodes.Count; i++)
            {
                for (int k = 0; k < tvBranches.Nodes[i].Nodes.Count; k++)
                {

                    for (int j = 0; j < tvBranches.Nodes[i].Nodes[k].Nodes.Count; j++)
                    {
                        if (tvBranches.Nodes[i].Nodes[k].Nodes[j].Checked == true)
                        {
                            flag = true;
                        }
                    }

                }
            }
            if (flag == false)
            {
                MessageBox.Show("Please Select Branches", "SSCRM-Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }
            flag = false;
            for (int i = 0; i < tvDocMonth.Nodes.Count; i++)
            {
                for (int k = 0; k < tvDocMonth.Nodes[i].Nodes.Count; k++)
                {
                    for (int j = 0; j < tvDocMonth.Nodes[i].Nodes[k].Nodes.Count; j++)
                    {
                        if (tvDocMonth.Nodes[i].Nodes[k].Nodes[j].Checked == true)
                        {
                            flag = true;
                        }
                    }

                }
            }
            if (flag == false)
            {
                MessageBox.Show("Please Select Document Month", "SSCRM-Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }         


            return flag;
        }
               

        private void GetSelectedCompAndBranchNodes()
        {
            Company = "";
            Branches = "";


            bool iscomp = false;
            for (int i = 0; i < tvBranches.Nodes.Count; i++)
            {
                for (int j = 0; j < tvBranches.Nodes[i].Nodes.Count; j++)
                {
                    for (int k = 0; k < tvBranches.Nodes[i].Nodes[j].Nodes.Count; k++)
                    {
                        if (tvBranches.Nodes[i].Nodes[j].Nodes[k].Checked == true)
                        {
                            if (Branches != string.Empty)
                                Branches += ",";
                            Branches += tvBranches.Nodes[i].Nodes[j].Nodes[k].Name.ToString();
                            iscomp = true;
                        }
                    }

                    if (iscomp == true)
                    {
                        if (Company != string.Empty)
                            Company += ",";
                        Company += tvBranches.Nodes[i].Nodes[j].Name.ToString();
                    }
                    iscomp = false;
                }
            }


        }
        private void GetSelectedDocmonths()
        {

            DocumentMonth = "";
            for (int i = 0; i < tvDocMonth.Nodes.Count; i++)
            {
                for (int j = 0; j < tvDocMonth.Nodes[i].Nodes.Count; j++)
                {
                    for (int k = 0; k < tvDocMonth.Nodes[i].Nodes[j].Nodes.Count; k++)
                    {
                        if (tvDocMonth.Nodes[i].Nodes[j].Nodes[k].Checked == true)
                        {
                            if (DocumentMonth != string.Empty)
                                DocumentMonth += ",";
                            DocumentMonth += tvDocMonth.Nodes[i].Nodes[j].Nodes[k].Name.ToString();

                        }
                    }
                }
            }

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                GetSelectedCompAndBranchNodes();
                GetSelectedDocmonths();

                if (sFrmType == "CHR")
                {
                    if (cbReportType.SelectedIndex == 2)
                    {
                        childReportViewer = new ReportViewer(Company, Branches, DocumentMonth, dtpFromDate.Value.ToString("dd/MM/yyyy"), dtpFromDate.Value.ToString("dd/MM/yyyy"), "DETAILED");
                        CommonData.ViewReport = "SSCRM_SERVICE_REP_CHR";
                        childReportViewer.Show();
                    }
                    else if (cbReportType.SelectedIndex == 1)
                    {
                        childReportViewer = new ReportViewer(Company, Branches, DocumentMonth,"ALL", "BRANCH_WISE");
                        CommonData.ViewReport = "SERVICES_COUNTING_AND_REPL_SUMMARY_BRANCH_WISE";
                        childReportViewer.Show();
                    }
                }
                else if (sFrmType == "FARMER_MEET")
                {
                    if (cbReportType.SelectedIndex == 2)
                    {
                        CommonData.ViewReport = "SSCRM_REP_FARMER_MEETINGS_DETL";
                        ReportViewer objReportview = new ReportViewer(Company, Branches, DocumentMonth, "DETAILED");
                        objReportview.Show();
                    }
                    else if (cbReportType.SelectedIndex == 1)
                    {
                        CommonData.ViewReport = "SSCRM_REP_FM_SUMMARY";
                        ReportViewer objReportview = new ReportViewer(Company, Branches, DocumentMonth, "DETAILED");
                        objReportview.Show();
                    }
                }
                else if (sFrmType == "DEMO_PLOT")
                {
                    CommonData.ViewReport = "SSCRM_REP_SERVICE_DEMO_PLOTS";
                    ReportViewer objReportview = new ReportViewer(Company, Branches,DocumentMonth, "DETAILED");
                    objReportview.Show();
                }
                else if (sFrmType == "PROD_PRM")
                {
                    CommonData.ViewReport = "SSCRM_SERVICE_REP_PRODUCT_PROMOTION_DETL";
                    ReportViewer objReportview = new ReportViewer(Company, Branches, DocumentMonth, "DETAILED");
                    objReportview.Show();
                }

            }
          
        }

        private void tvBranches_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TriStateTreeView.getStatus(e);
        }

        private void tvDocMonth_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TriStateTreeView.getStatus(e);

        }

    
       
     

      

       
                     
      
       
    }
}
