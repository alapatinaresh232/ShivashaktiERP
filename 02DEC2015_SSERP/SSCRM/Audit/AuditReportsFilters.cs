using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SSCRMDB;
using SSTrans;
using SSAdmin;
using Excel = Microsoft.Office.Interop.Excel;

namespace SSCRM
{
    public partial class AuditReportsFilters : Form
    {
        private InvoiceDB objInv = null;
        private Master objMaster = null;
        Security objSecurity = null;
      
        private UtilityDB objUtilityDB = null;
        private ReportViewer childReportViewer = null;
        ExcelDB objExcelDB = null;
        HRInfo objHRdb = null;

        AuditDB objAuditDB = null;
        SQLDB objSQLdb = null;
        double dTotDays;
        ReportDocument cryRpt = new ReportDocument();
       
        private string  Company = "",Branches = "", DocumentMonth = "";
        private string DevType = "", SubDevType = "", strDept = "", strEcodes = "", strMisConduct = "", strPptPnt = "", strMgntPnt = "", strStatus = "", strBranTypes = "";
        private int iFrmType = 0;
        double nTotDays = 0;


        public AuditReportsFilters()
        {
            InitializeComponent();
        }

        public AuditReportsFilters(int iRepType)
        {
            InitializeComponent();
            iFrmType = iRepType;
        }
       

        private void AuditReportsFilters_Load(object sender, EventArgs e)
        {                   

            dtpFromDate.Value = DateTime.Today;
            dtpToDate.Value = DateTime.Today;
            btnSendMail.Visible = false;
            FillBranchTypes();
            FillBranches();
            FillDocumentMonths();
            FillReportType();
            FillDepartmentData();
            FillDeviationTypes();
            FillEmployeeData();
            cbMgntPoint.SelectedIndex = 0;
            cbMisconduct.SelectedIndex = 0;
            cbPptPoint.SelectedIndex = 0;
            chkWithExpl.Visible = false;
            chkWithOutExpl.Visible = false;

            chkComp.Visible = false;
            if (iFrmType == 0)
            {
                cbReportType.SelectedIndex = 0;
                //this.Text = "Audit Solved Points ::Solvation Summary";
                if (cbReportType.SelectedIndex == 0)
                {                                                          
                    clbDevType.Enabled = false;

                    cbPptPoint.Enabled = false;
                    cbMgntPoint.Enabled = false;
                    grpStatus.Enabled = false;
                    cbMisconduct.Enabled = false;
                }
                else if (cbReportType.SelectedIndex == 1 || cbReportType.SelectedIndex == 2)
                {
                    grpDates.Visible = false;                    
                }

                //btnDownload.Visible = false;
            }
            if (iFrmType == 1)
            {
                this.Text = "Audit Amount Recovery :: Details";

                grpDates.Visible = false;
                               
                clbDevType.Enabled = false;
                cbPptPoint.Enabled = false;
                cbMgntPoint.Enabled = false;
                grpStatus.Enabled = false;
                cbMisconduct.Enabled = false;
                clbDepartment.Enabled = false;
                chkWithExpl.Visible = true;
                chkWithOutExpl.Visible = true;
                chkWithExpl.Checked = true;

            }
            if (iFrmType == 2)
            {
                this.Text = "Audit Major Points :: Details";
                grpDates.Visible = false;               
            }
                       
        }


        private void FillReportType()
        {
            if (iFrmType == 0)
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("type", typeof(string));
                dt.Columns.Add("name", typeof(string));                 

                dt.Rows.Add("AUDIT TOUR SCHEDULE", "AUDIT TOUR SCHEDULE");
                dt.Rows.Add("AUDIT REGIONAL SUMMARY", "AUDIT REGIONAL SUMMARY");
                dt.Rows.Add("AUDIT SOLVATION SUMMARY", "AUDIT SOLVATION SUMMARY");
                dt.Rows.Add("AUDIT TOUR SCHEDULE SUMMARY", "AUDIT TOUR SCHEDULE SUMMARY");
                dt.Rows.Add("REGIONAL SUMMARY ZONAL HEAD WISE", "REGIONAL SUMMARY ZONAL HEAD WISE");

                cbReportType.DataSource = dt;
                cbReportType.DisplayMember = "name";
                cbReportType.ValueMember = "type";
                

            }
            if (iFrmType == 1)
            {
               
                DataTable dt = new DataTable();
                dt.Columns.Add("type", typeof(string));
                dt.Columns.Add("name", typeof(string));

                dt.Rows.Add("AUDIT RECOVERY DETAILS", "AUDIT RECOVERY DETAILS");
                dt.Rows.Add("AUDITOR WISE RECOVERY DETAILS", "AUDITOR WISE RECOVERY DETAILS");

                cbReportType.DataSource = dt;
                cbReportType.DisplayMember = "name";
                cbReportType.ValueMember = "type";


            }
            if (iFrmType == 2)
            {             

                DataTable dt = new DataTable();
                dt.Columns.Add("type", typeof(string));
                dt.Columns.Add("name", typeof(string));

                dt.Rows.Add("AUDIT MAJOR POINTS", "AUDIT MAJOR POINTS");
                dt.Rows.Add("ZONAL HEAD WISE AUDIT POINTS", "ZONAL HEAD WISE AUDIT POINTS");
                dt.Rows.Add("AUDITOR WISE AUDIT POINTS", "AUDITOR WISE AUDIT POINTS");

                cbReportType.DataSource = dt;
                cbReportType.DisplayMember = "name";
                cbReportType.ValueMember = "type";

            }
           
        }
        private void FillBranchTypes()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
            clbBranchTypes.Items.Clear();
            try
            {
                strCommand = "SELECT Distinct(BRANCH_TYPE) BranchType "+
                             ", CASE  WHEN BRANCH_TYPE='BR' THEN 'BRANCH' "+
                             "  WHEN BRANCH_TYPE='SP' THEN 'STOCK POINT' "+
                             "  WHEN BRANCH_TYPE='PU' THEN 'PRODUCTION UNIT' "+
                             "  WHEN BRANCH_TYPE='TR' THEN 'TRANSPORT UNIT' "+
                             "  WHEN BRANCH_TYPE='OL' THEN 'OUTLETS'  "+
                             "  WHEN BRANCH_TYPE='HO' THEN 'CORPORATE OFFICE' "+
                             "  WHEN BRANCH_TYPE='ST' THEN 'STATIONARY' "+
                             "  WHEN BRANCH_TYPE='RS' THEN 'RETAIL STORE' "+
                             "  WHEN BRANCH_TYPE='WH' THEN 'WARE HOUSE' "+
                             "  ELSE '' END BranchTypeName "+
                             "  FROM BRANCH_MAS WHERE BRANCH_TYPE not in ('PO')";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = item["BranchType"].ToString();
                        oclBox.Text = item["BranchTypeName"].ToString();
                        clbBranchTypes.Items.Add(oclBox);
                        oclBox = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
              

        private void FillBranchandRegions()
        {
            tvBranches.Nodes.Clear();

            objAuditDB = new AuditDB();
            DataSet ds = new DataSet();
            ds = objAuditDB.GetAuditBranchRegions("","", "", "",CommonData.LogUserId, "PARENT");
            TreeNode tNode;
            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tvBranches.Nodes.Add(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), ds.Tables[0].Rows[i]["COMPANY_NAME"].ToString());
                        DataSet dschild = new DataSet();
                        dschild = objAuditDB.GetAuditBranchRegions(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "", "", "", CommonData.LogUserId, "ZONES");
                        if (dschild.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                            {
                                tvBranches.Nodes[i].Nodes.Add(dschild.Tables[0].Rows[j]["ABM_STATE"].ToString(), dschild.Tables[0].Rows[j]["ABM_STATE"].ToString());

                                DataSet dschild1 = new DataSet();
                                dschild1 = objAuditDB.GetAuditBranchRegions(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "", dschild.Tables[0].Rows[j]["ABM_STATE"].ToString(), "", CommonData.LogUserId, "REGIONS");
                                if (dschild1.Tables[0].Rows.Count > 0)
                                {
                                    for (int k = 0; k < dschild1.Tables[0].Rows.Count; k++)
                                    {
                                        tvBranches.Nodes[i].Nodes[j].Nodes.Add(dschild1.Tables[0].Rows[k]["ABM_REGION"].ToString(), dschild1.Tables[0].Rows[k]["ABM_REGION"].ToString());

                                        DataSet dschild2 = new DataSet();
                                        dschild2 = objAuditDB.GetAuditBranchRegions(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "HO", dschild.Tables[0].Rows[j]["ABM_STATE"].ToString(), dschild1.Tables[0].Rows[k]["ABM_REGION"].ToString(), CommonData.LogUserId, "BRANCHES");


                                        tvBranches.Nodes[i].Nodes[j].Nodes[k].Nodes.Add("HO" + "(" + dschild2.Tables[0].Rows.Count + ")");
                                        if (dschild2.Tables[0].Rows.Count > 0)
                                        {
                                            for (int ivar = 0; ivar < dschild2.Tables[0].Rows.Count; ivar++)
                                            {
                                                tvBranches.Nodes[i].Nodes[j].Nodes[k].Nodes[0].Nodes.Add(dschild2.Tables[0].Rows[ivar]["BranchCode"].ToString(), dschild2.Tables[0].Rows[ivar]["BranchName"].ToString());
                                            }
                                        }

                                        dschild2 = objAuditDB.GetAuditBranchRegions(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "BR", dschild.Tables[0].Rows[j]["ABM_STATE"].ToString(), dschild1.Tables[0].Rows[k]["ABM_REGION"].ToString(), CommonData.LogUserId, "BRANCHES");


                                        tvBranches.Nodes[i].Nodes[j].Nodes[k].Nodes.Add("BRANCHES" + "(" + dschild2.Tables[0].Rows.Count + ")");
                                        if (dschild2.Tables[0].Rows.Count > 0)
                                        {
                                            for (int ivar = 0; ivar < dschild2.Tables[0].Rows.Count; ivar++)
                                            {
                                                tvBranches.Nodes[i].Nodes[j].Nodes[k].Nodes[1].Nodes.Add(dschild2.Tables[0].Rows[ivar]["BranchCode"].ToString(), dschild2.Tables[0].Rows[ivar]["BranchName"].ToString());
                                            }
                                        }

                                        dschild2 = objAuditDB.GetAuditBranchRegions(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "SP", dschild.Tables[0].Rows[j]["ABM_STATE"].ToString(), dschild1.Tables[0].Rows[k]["ABM_REGION"].ToString(), CommonData.LogUserId, "BRANCHES");


                                        tvBranches.Nodes[i].Nodes[j].Nodes[k].Nodes.Add("STOCK POINTS" + "(" + dschild2.Tables[0].Rows.Count + ")");
                                        if (dschild2.Tables[0].Rows.Count > 0)
                                        {
                                            for (int ivar = 0; ivar < dschild2.Tables[0].Rows.Count; ivar++)
                                            {
                                                tvBranches.Nodes[i].Nodes[j].Nodes[k].Nodes[2].Nodes.Add(dschild2.Tables[0].Rows[ivar]["BranchCode"].ToString(), dschild2.Tables[0].Rows[ivar]["BranchName"].ToString());
                                            }
                                        }

                                        dschild2 = objAuditDB.GetAuditBranchRegions(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "PU", dschild.Tables[0].Rows[j]["ABM_STATE"].ToString(), dschild1.Tables[0].Rows[k]["ABM_REGION"].ToString(), CommonData.LogUserId, "BRANCHES");


                                        tvBranches.Nodes[i].Nodes[j].Nodes[k].Nodes.Add("PRODUCTION UNITS" + "(" + dschild2.Tables[0].Rows.Count + ")");
                                        if (dschild2.Tables[0].Rows.Count > 0)
                                        {
                                            for (int ivar = 0; ivar < dschild2.Tables[0].Rows.Count; ivar++)
                                            {
                                                tvBranches.Nodes[i].Nodes[j].Nodes[k].Nodes[3].Nodes.Add(dschild2.Tables[0].Rows[ivar]["BranchCode"].ToString(), dschild2.Tables[0].Rows[ivar]["BranchName"].ToString());
                                            }
                                        }

                                        dschild2 = objAuditDB.GetAuditBranchRegions(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "TU", dschild.Tables[0].Rows[j]["ABM_STATE"].ToString(), dschild1.Tables[0].Rows[k]["ABM_REGION"].ToString(), CommonData.LogUserId, "BRANCHES");


                                        tvBranches.Nodes[i].Nodes[j].Nodes[k].Nodes.Add("TRANSPORT UNITS" + "(" + dschild2.Tables[0].Rows.Count + ")");
                                        if (dschild2.Tables[0].Rows.Count > 0)
                                        {
                                            for (int ivar = 0; ivar < dschild2.Tables[0].Rows.Count; ivar++)
                                            {
                                                tvBranches.Nodes[i].Nodes[j].Nodes[k].Nodes[4].Nodes.Add(dschild2.Tables[0].Rows[ivar]["BranchCode"].ToString(), dschild2.Tables[0].Rows[ivar]["BranchName"].ToString());
                                            }
                                        }

                                        dschild2 = objAuditDB.GetAuditBranchRegions(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "OL", dschild.Tables[0].Rows[j]["ABM_STATE"].ToString(), dschild1.Tables[0].Rows[k]["ABM_REGION"].ToString(), CommonData.LogUserId, "BRANCHES");


                                        tvBranches.Nodes[i].Nodes[j].Nodes[k].Nodes.Add("OUTLETS" + "(" + dschild2.Tables[0].Rows.Count + ")");
                                        if (dschild2.Tables[0].Rows.Count > 0)
                                        {
                                            for (int ivar = 0; ivar < dschild2.Tables[0].Rows.Count; ivar++)
                                            {
                                                tvBranches.Nodes[i].Nodes[j].Nodes[k].Nodes[5].Nodes.Add(dschild2.Tables[0].Rows[ivar]["BranchCode"].ToString(), dschild2.Tables[0].Rows[ivar]["BranchName"].ToString());
                                            }
                                        }

                                    }
                                }
                            }
                        }

                    }
                }

                for (int i = 0; i < tvBranches.Nodes.Count; i++)
                {
                    for (int j = 0; j < tvBranches.Nodes[i].Nodes.Count; j++)
                    {
                        if (tvBranches.Nodes[i].Nodes[j].Nodes.Count > 0)
                            tvBranches.Nodes[i].FirstNode.Expand();
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
            GetSelectedBranchTypes();
            objAuditDB = new AuditDB();
            DataSet ds = new DataSet();
            chkComp.Visible = false;
            ds = objAuditDB.GetAuditBranchRegions("", "", "", "",CommonData.LogUserId ,"PARENT");
            TreeNode tNode;
            if (strBranTypes.Length >= 2)
            {
                try
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        chkComp.Visible = true;

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            tvBranches.Nodes.Add(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), ds.Tables[0].Rows[i]["COMPANY_NAME"].ToString());
                            DataSet dschild = new DataSet();
                            dschild = objAuditDB.GetAuditBranchRegions(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), strBranTypes, "", "", "", "ZONES");
                            if (dschild.Tables[0].Rows.Count > 0)
                            {
                                for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                                {
                                    tvBranches.Nodes[i].Nodes.Add(dschild.Tables[0].Rows[j]["ABM_STATE"].ToString(), dschild.Tables[0].Rows[j]["ABM_STATE"].ToString());

                                    DataSet dschild1 = new DataSet();
                                    dschild1 = objAuditDB.GetAuditBranchRegions(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), strBranTypes, dschild.Tables[0].Rows[j]["ABM_STATE"].ToString(), "", "", "REGIONS");
                                    if (dschild1.Tables[0].Rows.Count > 0)
                                    {
                                        for (int k = 0; k < dschild1.Tables[0].Rows.Count; k++)
                                        {
                                            tvBranches.Nodes[i].Nodes[j].Nodes.Add(dschild1.Tables[0].Rows[k]["ABM_REGION"].ToString(), dschild1.Tables[0].Rows[k]["ABM_REGION"].ToString());

                                            DataSet dschild2 = new DataSet();


                                            dschild2 = objAuditDB.GetAuditBranchRegions(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), strBranTypes, dschild.Tables[0].Rows[j]["ABM_STATE"].ToString(), dschild1.Tables[0].Rows[k]["ABM_REGION"].ToString(), CommonData.LogUserId, "BRANCHES");

                                            if (dschild2.Tables[0].Rows.Count > 0)
                                            {
                                                for (int ivar = 0; ivar < dschild2.Tables[0].Rows.Count; ivar++)
                                                {
                                                    tvBranches.Nodes[i].Nodes[j].Nodes[k].Nodes.Add(dschild2.Tables[0].Rows[ivar]["BranchCode"].ToString(), dschild2.Tables[0].Rows[ivar]["BranchName"].ToString());
                                                }
                                            }

                                        }
                                    }
                                }
                            }

                        }
                    }

                    for (int i = 0; i < tvBranches.Nodes.Count; i++)
                    {
                        for (int j = 0; j < tvBranches.Nodes[i].Nodes.Count; j++)
                        {
                            if (tvBranches.Nodes[i].Nodes[j].Nodes.Count > 0)
                                tvBranches.Nodes[i].FirstNode.Expand();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }


        private bool CheckData()
        {
            bool blVil = true;

            GetSelectedValues();


            if (iFrmType == 0 && cbReportType.SelectedIndex == 0 || iFrmType == 0 && cbReportType.SelectedIndex == 4 ||
                iFrmType==2 && cbReportType.SelectedIndex==1 || iFrmType==1 && cbReportType.SelectedIndex==1 ||
                iFrmType==2 && cbReportType.SelectedIndex==2 )
            {
            }
            else
            {
                if (strBranTypes.Length == 0)
                {
                    MessageBox.Show("Select Location Type ", "SSCRM-Report", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }



                blVil = false;
                for (int ivar = 0; ivar < tvBranches.Nodes.Count; ivar++)
                {
                    for (int k = 0; k < tvBranches.Nodes[ivar].Nodes.Count; k++)
                    {
                        for (int i = 0; i < tvBranches.Nodes[ivar].Nodes[k].Nodes.Count; i++)
                        {
                            for (int j = 0; j < tvBranches.Nodes[ivar].Nodes[k].Nodes[i].Nodes.Count; j++)
                            {
                                if (tvBranches.Nodes[ivar].Nodes[k].Nodes[i].Nodes[j].Checked == true)
                                {
                                    blVil = true;
                                }
                            }
                        }
                    }
                }

                if (blVil == false)
                {
                    MessageBox.Show("Select Atleast One Branch", "SSCRM-Report", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return blVil;
                }
            }

            if (iFrmType == 0 && cbReportType.SelectedIndex == 3)
            {
            }
            else
            {
                blVil = false;
                for (int k = 0; k < tvDocMonth.Nodes.Count; k++)
                {
                    for (int j = 0; j < tvDocMonth.Nodes[k].Nodes.Count; j++)
                    {
                        if (tvDocMonth.Nodes[k].Nodes[j].Checked == true)
                        {
                            blVil = true;
                        }
                    }
                }

                if (blVil == false)
                {
                    MessageBox.Show("Select Document Month", "SSCRM-Report", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return blVil;
                }
            }


            return blVil;
        }

        private void FillDocumentMonths()
        {
            objSQLdb = new SQLDB();
            DataSet ds = new DataSet();
            DataSet dschild = new DataSet();
            string strSQL = "";
            tvDocMonth.Nodes.Clear();

            try
            {
                strSQL = "SELECT DISTINCT HMH_FIN_YEAR FROM HR_MISCONDUCT_HEAD ORDER BY HMH_FIN_YEAR ASC";
                ds = objSQLdb.ExecuteDataSet(strSQL);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tvDocMonth.Nodes.Add(ds.Tables[0].Rows[i]["HMH_FIN_YEAR"].ToString(), ds.Tables[0].Rows[i]["HMH_FIN_YEAR"].ToString());

                        strSQL = "SELECT DISTINCT HMH_VISIT_MONTH,start_date FROM HR_MISCONDUCT_HEAD " +
                                    " INNER JOIN document_month ON document_month=HMH_VISIT_MONTH " +
                                    "  WHERE FIN_YEAR = '" + ds.Tables[0].Rows[i]["HMH_FIN_YEAR"].ToString() + "' ORDER BY START_DATE ASC";
                        dschild = objSQLdb.ExecuteDataSet(strSQL);

                        if (dschild.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                            {
                                tvDocMonth.Nodes[i].Nodes.Add(dschild.Tables[0].Rows[j]["HMH_VISIT_MONTH"].ToString(), dschild.Tables[0].Rows[j]["HMH_VISIT_MONTH"].ToString());
                            }
                        }
                    }

                }

                //strSQL = " SELECT DISTINCT HMH_VISIT_MONTH,start_date FROM HR_MISCONDUCT_HEAD "+
                //         " INNER JOIN document_month ON document_month=HMH_VISIT_MONTH "+
                //         " ORDER BY start_date ASC";
                //ds = objSQLdb.ExecuteDataSet(strSQL);

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //    {
                //        tvDocMonth.Nodes.Add(ds.Tables[0].Rows[i]["HMH_VISIT_MONTH"].ToString(), ds.Tables[0].Rows[i]["HMH_VISIT_MONTH"].ToString());
                //    }
                //}

                tvDocMonth.Nodes[0].Expand();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
                ds = null;
                dschild = null;
            }

          
        }

        //private void FillAuditRecEmpDetl()
        //{
        //    objSQLdb = new SQLDB();
        //    DataTable dt = new DataTable();
        //    string strCommand = "";
        //    clbEmployees.Items.Clear();

        //    try
        //    {
        //        strCommand = "exec Get_Audit_Recovery_Emp_Detl";

        //        dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

        //        if (dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow item in dt.Rows)
        //            {
        //                NewCheckboxListItem oclBox = new NewCheckboxListItem();
        //                oclBox.Tag = item["Ecode"].ToString();
        //                oclBox.Text = item["EmpName"].ToString();
        //                clbEmployees.Items.Add(oclBox);
        //                oclBox = null;
        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
          
        //}
      


        private void FillDeviationTypes()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
            clbDevType.Items.Clear();

            try
            {
                strCommand = "SELECT HRMC_MISCONDUCT_CODE,HRMC_MISCONDUCT_HEAD FROM HR_MISCONDUCT_MAS ORDER BY HRMC_MISCONDUCT_HEAD ASC";

                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = item["HRMC_MISCONDUCT_CODE"].ToString();
                        oclBox.Text = item["HRMC_MISCONDUCT_HEAD"].ToString();
                        clbDevType.Items.Add(oclBox);
                        oclBox = null;
                    }
                }

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
          
        }

        private void FillDepartmentData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            clbDepartment.Items.Clear();
            try
            {
                string strCommand = "SELECT dept_code,dept_name FROM Dept_Mas ORDER BY dept_code,dept_name";

                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = item["dept_code"].ToString();
                        oclBox.Text = item["dept_name"].ToString();
                        clbDepartment.Items.Add(oclBox);
                        oclBox = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
                dt = null;
            }
        }
             

        //private void FillAuditEmployeeDetails()
        //{
        //    objSQLdb = new SQLDB();
        //    DataTable dt = new DataTable();
        //    clbEmployees.Items.Clear();
        //    string strCommand = "";
        //    try
        //    {
        //        strCommand = "exec LevelDeptHead "+ 40307 +","+ CommonData.DocMonth +"";
        //        dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

        //        if (dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow item in dt.Rows)
        //            {
        //                NewCheckboxListItem oclBox = new NewCheckboxListItem();
        //                oclBox.Tag = item["Ecode"].ToString();
        //                oclBox.Text = item["EmpName"].ToString();
        //                clbEmployees.Items.Add(oclBox);
        //                oclBox = null;
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        objSQLdb = null;
        //        dt = null;
        //    }

        //}


        private void GetSelectedValues()
        {
            Company = ""; Branches = ""; DocumentMonth = ""; DevType = ""; SubDevType = "";
            strDept = ""; strMgntPnt = ""; strPptPnt = ""; strStatus = ""; strMisConduct = ""; 


           //Selected Companies and Branches            

            bool iscomp = false;

            for (int ivar = 0; ivar < tvBranches.Nodes.Count; ivar++)
            {
                for (int k = 0; k < tvBranches.Nodes[ivar].Nodes.Count; k++)
                {
                    for (int i = 0; i < tvBranches.Nodes[ivar].Nodes[k].Nodes.Count; i++)
                    {
                        for (int j = 0; j < tvBranches.Nodes[ivar].Nodes[k].Nodes[i].Nodes.Count; j++)
                        {
                            if (tvBranches.Nodes[ivar].Nodes[k].Nodes[i].Nodes[j].Checked == true)
                            {
                                if (Branches != string.Empty)
                                    Branches += ",";
                                Branches += tvBranches.Nodes[ivar].Nodes[k].Nodes[i].Nodes[j].Name.ToString();
                                iscomp = true;
                            }
                        }
                    }
                }

                if (iscomp == true)
                {
                    if (Company != string.Empty)
                        Company += ",";
                    Company += tvBranches.Nodes[ivar].Name.ToString();
                }
                iscomp = false;
            }
            


            //Selected DocMonths


            for (int i = 0; i < tvDocMonth.Nodes.Count; i++)
            {
                for (int j = 0; j < tvDocMonth.Nodes[i].Nodes.Count; j++)
                {
                    if (tvDocMonth.Nodes[i].Nodes[j].Checked == true)
                    {
                        if (DocumentMonth != string.Empty)
                            DocumentMonth += ",";
                        DocumentMonth += tvDocMonth.Nodes[i].Nodes[j].Name.ToString();

                    }
                }
            }


            //Selected DeviationTypes            
                for (int i = 0; i < clbDevType.Items.Count; i++)
                {
                    if (clbDevType.GetItemCheckState(i) == CheckState.Checked)
                    {
                        DevType += ((SSAdmin.NewCheckboxListItem)(clbDevType.Items[i])).Text.ToString() + ",";

                    }
                }        
            
            //Selected Departments           

                for (int i = 0; i < clbDepartment.Items.Count; i++)
                {
                    if (clbDepartment.GetItemCheckState(i) == CheckState.Checked)
                    {
                        strDept += ((SSAdmin.NewCheckboxListItem)(clbDepartment.Items[i])).Tag.ToString() + ",";

                    }
                }                       

            if (strDept.Length != 0)
                strDept = strDept.TrimEnd(',');
          

          //Status
            if (ChkStatusAll.Checked == true)
            {
                strStatus = "ALL";
            }
            else
            {
                if (chkSolved.Checked == true)
                {
                    strStatus = "SOLVED";
                }
                if (chkUnsolved.Checked == true)
                {
                    strStatus = "UNSOLVED";
                }
                if (chkSolvedFollUp.Checked == true)
                {
                    strStatus = "SOLVED-FOLLOWUP";
                }
                if (chkSolved.Checked == true && chkUnsolved.Checked == true)
                {
                    strStatus = "SOLVED,UNSOLVED";
                }
                if (chkSolved.Checked == true && chkSolvedFollUp.Checked == true)
                {
                    strStatus = "SOLVED,SOLVED-FOLLOWUP";
                }
                if (chkSolved.Checked == true && chkUnsolved.Checked == true && chkSolvedFollUp.Checked == true)
                {
                    strStatus = "SOLVED,SOLVED-FOLLOWUP,UNSOLVED";
                }
            }   
            
        }            
             

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
       
        //private void GetManagementEmployeeName()
        //{
        //    objSQLdb = new SQLDB();
        //    DataTable dt = new DataTable();

        //    if (txtEcode.Text != "")
        //    {
        //        try
        //        {
        //            string strCmd = "SELECT MEMBER_NAME+'('+DESIG+')' EName FROM EORA_MASTER " +
        //                            " WHERE ECODE=" + txtEcode.Text + "";
        //            dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
        //            if (dt.Rows.Count > 0)
        //            {
        //                txtEName.Text = dt.Rows[0]["EName"].ToString();
        //            }
        //            else
        //            {
        //                txtEName.Text = "";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.ToString());
        //        }
        //        finally
        //        {
        //            objSQLdb = null;
        //            dt = null;
        //        }
        //    }
        //    else
        //    {
        //        txtEName.Text = "";
        //    }

        //}          
            


        private void btnReport_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {               
                GetSelectedValues();

                if (strEcodes == "")
                {
                    strEcodes = "0";
                }
                if (cbEmployees.SelectedIndex >= -1)
                {
                    strEcodes = cbEmployees.Text.ToString().Split('-')[0];
                }

               
                if (DevType == "")
                {
                    DevType = "ALL";
                }

                //if (strDept == "")
                //{
                //    //strDept = "100000,200000,300000,400000,500000,600000,800000,900000,1000000,1100000,1200000,1300000,1400000,1600000";
                //}

                if (strStatus == "")
                {
                    strStatus = "ALL";
                }
                              

                if (iFrmType == 0)
                {
                    if (cbReportType.SelectedIndex == 2)
                    {
                        CommonData.ViewReport = "SSCRM_REP_AUDIT_POINTS_SUMMARY";
                        childReportViewer = new ReportViewer(Company, Branches, "", DocumentMonth, "SUMMARY");
                        childReportViewer.Show();
                    }
                    if (cbReportType.SelectedIndex == 3)
                    {
                        CommonData.ViewReport = "SSERP_REP_AUDIT_TOUR_SCHEDULE_SUMMARY";
                        childReportViewer = new ReportViewer(Company, Branches, "", "",dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(),dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), "");
                        childReportViewer.Show();
                    }
                }
                if (iFrmType == 1)
                {
                    if (cbReportType.SelectedIndex == 0)
                    {
                        CommonData.ViewReport = "SSERP_REP_AUDIT_RECOVERY_DETLS";
                        childReportViewer = new ReportViewer(Company, Branches, "", "", "", DocumentMonth, "DETAILED", "0");
                        childReportViewer.Show();
                    }
                    else if (cbReportType.SelectedIndex == 1)
                    {
                        CommonData.ViewReport = "SSERP_REP_AUDIT_RECOVERY_DETLS";
                        childReportViewer = new ReportViewer(Company, Branches, "", "", "", DocumentMonth, "DETAILED", strEcodes);
                        childReportViewer.Show();
                    }
                }
                if (iFrmType == 2)
                {
                    if (cbReportType.SelectedIndex == 0)
                    {
                        CommonData.ViewReport = "SSCRM_REP_AUDIT_QUERY_REGISTER";
                        childReportViewer = new ReportViewer(Company, Branches, "", DocumentMonth, strEcodes, DevType, strDept, cbMisconduct.Text.ToString(), cbMgntPoint.Text.ToString(), cbPptPoint.Text.ToString(), strStatus, "AUDIT_QUERY_REGISTER");
                        childReportViewer.Show();
                    }
                    else if (cbReportType.SelectedIndex == 1)
                    {
                        CommonData.ViewReport = "SSCRM_REP_AUDIT_QUERY_REGISTER";
                        childReportViewer = new ReportViewer(Company, Branches, "", DocumentMonth, strEcodes, DevType, strDept, cbMisconduct.Text.ToString(), cbMgntPoint.Text.ToString(), cbPptPoint.Text.ToString(), strStatus, "ZONAL_HEAD");
                        childReportViewer.Show();
                    }
                    else if (cbReportType.SelectedIndex == 2)
                    {
                        CommonData.ViewReport = "SSCRM_REP_AUDIT_QUERY_REGISTER";
                        childReportViewer = new ReportViewer(Company, Branches, "", DocumentMonth, strEcodes, DevType, strDept, cbMisconduct.Text.ToString(), cbMgntPoint.Text.ToString(), cbPptPoint.Text.ToString(), strStatus, "AUDITOR_WISE");
                        childReportViewer.Show();
                    }

                }
                if (iFrmType == 0)
                {
                    if (cbReportType.SelectedIndex == 1)
                    {
                        CommonData.ViewReport = "AUDIT_DEVIATION_TYPES_SUMMARY";
                        childReportViewer = new ReportViewer(Company, Branches, "", DocumentMonth, "SUMMARY", DevType,"0");
                        childReportViewer.Show();
                    }
                    if (cbReportType.SelectedIndex == 4)
                    {
                        CommonData.ViewReport = "AUDIT_DEVIATION_TYPES_SUMMARY";
                        childReportViewer = new ReportViewer(Company, Branches, "", DocumentMonth, "SALES_HEAD_WISE", DevType,strEcodes);
                        childReportViewer.Show();
                    }

                }
                if (iFrmType == 0)
                {
                    if (cbReportType.SelectedIndex == 0)
                    {
                        if (dTotDays > 31)
                        {
                            MessageBox.Show("Please Select Dates B/W One Month", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            childReportViewer = new ReportViewer(Company,Branches,DocumentMonth, strEcodes, Convert.ToDateTime(dtpFromDate.Value).ToString("dd/MMM/yyyy"), Convert.ToDateTime(dtpToDate.Value).ToString("dd/MMM/yyyy"), "");
                            CommonData.ViewReport = "SSCRM_AUDIT_PLAN_CRTAB_REP";
                            childReportViewer.Show();
                        }
                    }
                }              
                
            }

        }       

    
        private void chkComp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkComp.Checked == true)
            {
                for (int k = 0; k < tvBranches.Nodes.Count; k++)
                {
                    for (int i = 0; i < tvBranches.Nodes[k].Nodes.Count; i++)
                    {
                        for (int j = 0; j < tvBranches.Nodes[k].Nodes[i].Nodes.Count; j++)
                        {
                            tvBranches.Nodes[k].Nodes[i].Nodes[j].Checked = true;

                        }
                    }

                }
            }
            else
            {
                for (int k = 0; k < tvBranches.Nodes.Count; k++)
                {
                    for (int i = 0; i < tvBranches.Nodes[k].Nodes.Count; i++)
                    {
                        for (int j = 0; j < tvBranches.Nodes[k].Nodes[i].Nodes.Count; j++)
                        {
                            tvBranches.Nodes[k].Nodes[i].Nodes[j].Checked = false;

                        }
                    }

                }

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            FillBranches();
            //FillAuditEmployeeDetails();
            FillDepartmentData();
            FillDocumentMonths();
            FillDeviationTypes();
            cbMgntPoint.SelectedIndex = 0;
            cbPptPoint.SelectedIndex = 0;
            cbMisconduct.SelectedIndex = 0;
            ChkStatusAll.Checked = false;
            chkSolved.Checked = false;
            chkUnsolved.Checked = false;
            chkSolvedFollUp.Checked = false;           

        }



        private void btnDownload_Click(object sender, EventArgs e)
        {
            DataTable dtExcel = new DataTable();
            objExcelDB = new ExcelDB();
            objUtilityDB = new UtilityDB();

            if (CheckData() == true)
            {
                GetSelectedValues();
                if (DevType == "")
                {
                    DevType = "ALL";
                }
                if (strEcodes == "")
                {
                    strEcodes = "0";
                }
                if (cbEmployees.SelectedIndex >= -1)
                {
                    strEcodes = cbEmployees.Text.ToString().Split('-')[0];
                }

                if (strDept == "")
                {
                    //strDept = "100000,200000,300000,400000,500000,600000,800000,900000,1000000,1100000,1200000,1300000,1400000,1600000";
                }
                if (strStatus == "")
                {
                    strStatus = "ALL";
                }

                #region "iFrmType == 2 :: Audit Query Register"
                if (iFrmType == 2)
                {

                    try
                    {
                        if (cbReportType.SelectedIndex == 0)
                            dtExcel = objExcelDB.GetAuditQueryReg(Company, Branches, "", DocumentMonth, strEcodes, DevType, strDept, cbMisconduct.Text.ToString(), "ALL", "ALL", strStatus, "AUDIT_QUERY_REGISTER").Tables[0];
                        if (cbReportType.SelectedIndex == 1)
                            dtExcel = objExcelDB.GetAuditQueryReg(Company, Branches, "", DocumentMonth, strEcodes, DevType, strDept, cbMisconduct.Text.ToString(), "ALL", "ALL", strStatus, "ZONAL_HEAD").Tables[0];
                        if (cbReportType.SelectedIndex == 2)
                            dtExcel = objExcelDB.GetAuditQueryReg(Company, Branches, "", DocumentMonth, strEcodes, DevType, strDept, cbMisconduct.Text.ToString(), "ALL", "ALL", strStatus, "AUDITOR_WISE").Tables[0];
                        objExcelDB = null;


                        if (dtExcel.Rows.Count > 0)
                        {
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

                #region "iFrmType==1 :: Audit Recovery"

                if (iFrmType == 1)
                {
                    objExcelDB = new ExcelDB();
                    if (cbReportType.SelectedIndex == 0)
                        dtExcel = objExcelDB.GetAuditRecDetails(Company, Branches, "", DocumentMonth, "", "", "0", "").Tables[0];
                    if (cbReportType.SelectedIndex == 1)
                        dtExcel = objExcelDB.GetAuditRecDetails(Company, Branches, "", DocumentMonth, "", "", strEcodes, "").Tables[0];
                    objExcelDB = null;
                    if (dtExcel.Rows.Count > 0)
                    {
                        try
                        {

                            Excel.Application oXL = new Excel.Application();
                            Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                            Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                            oXL.Visible = true;
                            int iTotColumns = 0;
                            if (chkWithOutExpl.Checked == true)
                                iTotColumns = 13;
                            if (chkWithExpl.Checked == true)
                                iTotColumns = 14;
                            string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                            Excel.Range rgHead = null;
                            Excel.Range rg = worksheet.get_Range("A4", sLastColumn + "4");
                            Excel.Range rgData = worksheet.get_Range("A5", sLastColumn + (dtExcel.Rows.Count + 4).ToString());
                            rgData.Font.Size = 11;
                            rgData.WrapText = true;
                            rgData.VerticalAlignment = Excel.Constants.xlCenter;
                            rgData.Borders.Weight = 2;
                            if (chkWithExpl.Checked == true)
                                rgData = worksheet.get_Range("A1", "N2");
                            else
                                rgData = worksheet.get_Range("A1", "M2");

                            rgData.Merge(Type.Missing);
                            rgData.Font.Bold = true; rgData.Font.Size = 16;
                            rgData.Value2 = "AUDIT RECOVERY DETAILS";
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
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("D4", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("E4", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("F4", Type.Missing);
                            rg.Cells.ColumnWidth = 20;
                            rg = worksheet.get_Range("G4", Type.Missing);
                            rg.Cells.ColumnWidth = 30;
                            rg = worksheet.get_Range("H4", Type.Missing);
                            rg.Cells.ColumnWidth = 50;
                            if (chkWithExpl.Checked == true)
                            {
                                rg = worksheet.get_Range("I4", Type.Missing);
                                rg.Cells.ColumnWidth = 50;
                                rg = worksheet.get_Range("J4", Type.Missing);
                                rg.Cells.ColumnWidth = 30;
                                rg = worksheet.get_Range("K4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("L4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("M4", Type.Missing);
                                rg.Cells.ColumnWidth = 20;
                                rg = worksheet.get_Range("N4", Type.Missing);
                                rg.Cells.ColumnWidth = 15;
                            }
                            else
                            {
                                rg = worksheet.get_Range("I4", Type.Missing);
                                rg.Cells.ColumnWidth = 25;
                                rg = worksheet.get_Range("J4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("K4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("L4", Type.Missing);
                                rg.Cells.ColumnWidth = 20;
                                rg = worksheet.get_Range("M4", Type.Missing);
                                rg.Cells.ColumnWidth = 15;
                            }



                            int iColumn = 1, iStartRow = 4;
                            worksheet.Cells[iStartRow, iColumn++] = "SlNo";
                            worksheet.Cells[iStartRow, iColumn++] = "Query ID";
                            worksheet.Cells[iStartRow, iColumn++] = "Visit Month";
                            worksheet.Cells[iStartRow, iColumn++] = "Doc Month";
                            worksheet.Cells[iStartRow, iColumn++] = "Zone";
                            worksheet.Cells[iStartRow, iColumn++] = "Region";
                            worksheet.Cells[iStartRow, iColumn++] = "Branch";
                            worksheet.Cells[iStartRow, iColumn++] = "Particulars";
                            if (chkWithExpl.Checked == true)
                            {
                                worksheet.Cells[iStartRow, iColumn++] = "Explanation";
                            }
                            worksheet.Cells[iStartRow, iColumn++] = "Recovery Mode";
                            worksheet.Cells[iStartRow, iColumn++] = "Rec Amt";
                            worksheet.Cells[iStartRow, iColumn++] = "Actual Rec Amt";
                            worksheet.Cells[iStartRow, iColumn++] = "Details Of Recovery";
                            worksheet.Cells[iStartRow, iColumn++] = "Auditor";


                            iStartRow++; iColumn = 1;

                            for (int i = 0; i < dtExcel.Rows.Count; i++)
                            {

                                worksheet.Cells[iStartRow, iColumn++] = i + 1;
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_misc_id"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_visit_month"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_doc_month"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_zone"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_region"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_branch_name"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_audit_quiry"].ToString();

                                if (chkWithExpl.Checked == true)
                                {
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Branch_Head_Explanation"].ToString();
                                }
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_recov_mode"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_recv_amt"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_actrecv_amt"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_act_recv_detls"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_auditor_name"].ToString();

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

                #region "iFrmType==0 :: Audit Regional  Summary"

                if (iFrmType == 0)
                {
                    if (cbReportType.SelectedIndex == 1 || cbReportType.SelectedIndex == 4)
                    {
                        try
                        {
                            if (cbReportType.SelectedIndex == 1)
                                dtExcel = objExcelDB.GetAuditDeviationDetails(Company, Branches, "", DocumentMonth, DevType, "EXCEL_REPORT", "0").Tables[0];
                            if (cbReportType.SelectedIndex == 4)
                                dtExcel = objExcelDB.GetAuditDeviationDetails(Company, Branches, "", DocumentMonth, DevType, "EXCEL_REPORT", strEcodes).Tables[0];
                            objExcelDB = null;
                            if (dtExcel.Rows.Count > 0)
                            {

                                Excel.Application oXL = new Excel.Application();
                                Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                                Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                                oXL.Visible = true;
                                int iTotColumns = 0;
                                iTotColumns = 4 + (3 * Convert.ToInt32(dtExcel.Rows[0]["no_of_devs"])) + 4;
                                string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                                Excel.Range rg = worksheet.get_Range("A3", sLastColumn + "3");
                                Excel.Range rgData = worksheet.get_Range("A3", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["no_Of_Branches"]) + 3).ToString());
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
                                rgData = worksheet.get_Range("A4", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["no_Of_Branches"]) + 3).ToString());
                                rgData.WrapText = false;
                                rg = worksheet.get_Range("A3", Type.Missing);
                                rg.Cells.ColumnWidth = 4;
                                rg = worksheet.get_Range("B3", Type.Missing);
                                rg.Cells.ColumnWidth = 8;
                                rg = worksheet.get_Range("C3", Type.Missing);
                                rg.Cells.ColumnWidth = 20;
                                rg = worksheet.get_Range("D3", Type.Missing);
                                rg.Cells.ColumnWidth = 40;

                                rg = worksheet.get_Range(objUtilityDB.GetColumnName(iTotColumns - 5) + "3", objUtilityDB.GetColumnName(iTotColumns - 5) + "3");
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range(objUtilityDB.GetColumnName(iTotColumns - 2) + "3", objUtilityDB.GetColumnName(iTotColumns - 2) + "3");
                                rg.Cells.ColumnWidth = 10;

                                int iColumn = 1;
                                worksheet.Cells[3, iColumn++] = "SlNo";
                                worksheet.Cells[3, iColumn++] = "Zone";
                                worksheet.Cells[3, iColumn++] = "Region";
                                worksheet.Cells[3, iColumn++] = "Branch";
                                worksheet.Cells[3, iColumn++] = "Visit Month";

                                Excel.Range rgHead;
                                int iStartColumn = 0;
                                for (int iDev = 0; iDev < Convert.ToInt32(dtExcel.Rows[0]["no_of_devs"]); iDev++)
                                {
                                    rgHead = worksheet.get_Range("A1", "E1");
                                    rgHead.Merge(Type.Missing);
                                    rgHead.Font.Size = 14;
                                    rgHead.Font.ColorIndex = 1;
                                    rgHead.Font.Bold = true;
                                    rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                    rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                    rgHead.Cells.Value2 = "REGIONAL SUMMARY";


                                    iStartColumn = (3 * iDev) + 6;

                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "2", objUtilityDB.GetColumnName(iStartColumn + 2) + "2");

                                    //worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "2", objUtilityDB.GetColumnName(iStartColumn + 6) + "2").Merge(Type.Missing);


                                    rgHead.Merge(Type.Missing);
                                    rgHead.Interior.ColorIndex = 34 + 1;
                                    rgHead.Borders.Weight = 2;
                                    rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
                                    rgHead.Cells.RowHeight = 20;
                                    rgHead.Font.Size = 14;
                                    rgHead.Font.ColorIndex = 1;
                                    rgHead.Font.Bold = true;
                                    rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                    rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;


                                    worksheet.Cells[3, iStartColumn++] = "No.Of Cases";
                                    worksheet.Cells[3, iStartColumn++] = "Dev Amount";
                                    worksheet.Cells[3, iStartColumn++] = "Rec Amount";


                                }

                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "2", objUtilityDB.GetColumnName(iStartColumn + 2) + "2");
                                rgHead.Merge(Type.Missing);
                                rgHead.Interior.ColorIndex = 34 + Convert.ToInt32(dtExcel.Rows[0]["no_of_devs"]) + 1;
                                rgHead.Borders.Weight = 2;
                                rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
                                rgHead.Cells.RowHeight = 20;
                                rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Value2 = "TOTAL";
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;

                                worksheet.Cells[3, iStartColumn++] = "No.Of Cases";
                                worksheet.Cells[3, iStartColumn++] = "Dev Amount";
                                worksheet.Cells[3, iStartColumn++] = "Rec Amount";



                                int iRowCounter = 4; int iColumnCounter = 1;
                                for (int i = 0; i < dtExcel.Rows.Count; i++)
                                {
                                    if (i > 0)
                                    {

                                        if (dtExcel.Rows[i]["Branch_Code"].ToString() == dtExcel.Rows[i - 1]["Branch_Code"].ToString())
                                        {
                                            int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["dev_slno"]);
                                            //int iStartColumn = 0;
                                            iColumnCounter = (3 * (iMonthNo - 1)) + 6;
                                            rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 2) + "2");
                                            rgHead.Cells.Value2 = dtExcel.Rows[i]["Dev_Type"];
                                            rgHead.WrapText = true;

                                            rgHead.Interior.ColorIndex = 34 + iMonthNo;
                                            rgHead.Font.ColorIndex = 1;
                                            rgHead.Font.Bold = true;
                                            rgHead.Borders.Weight = 2;
                                            //rgHead.Interior.ColorIndex = 31;
                                            //rgHead.Font.ColorIndex = 2;
                                            rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                            rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;


                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["no_of_Dev_Cases"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["dev_Amt"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["rec_Amt"];
                                        }

                                        else
                                        {

                                            iRowCounter++;
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 3;
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["State"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["Region"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["Branch_Name"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["visit_month"];


                                            int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["dev_slno"]);

                                            iColumnCounter = (3 * (iMonthNo - 1)) + 6;
                                            rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 2) + "2");
                                            rgHead.Cells.Value2 = dtExcel.Rows[i]["Dev_Type"];
                                            rgHead.WrapText = true;

                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["no_of_Dev_Cases"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["dev_Amt"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["rec_Amt"];

                                            iColumnCounter = iTotColumns - 2;
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["tot_dev_cases"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["tot_dev_amt"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["tot_rec_amt"];
                                        }
                                    }
                                    else
                                    {

                                        worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 3;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["State"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["Region"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["Branch_Name"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["visit_month"];


                                        int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["dev_slno"]);
                                        //int iStartColumn = 0;
                                        iColumnCounter = (3 * (iMonthNo - 1)) + 6;
                                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 2) + "2");
                                        rgHead.Cells.Value2 = dtExcel.Rows[i]["Dev_Type"];
                                        rgHead.WrapText = true;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["no_of_Dev_Cases"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["dev_Amt"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["rec_Amt"];

                                        iColumnCounter = iTotColumns - 2;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["tot_dev_cases"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["tot_dev_amt"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["tot_rec_amt"];

                                    }

                                    iColumnCounter = 1;
                                }


                                iStartColumn = (3 * (Convert.ToInt32(dtExcel.Rows[0]["no_of_devs"]))) + 8;
                                iColumnCounter = iStartColumn;
                                rgHead = worksheet.get_Range("F" + (Convert.ToInt32(dtExcel.Rows[0]["no_Of_Branches"]) + 4).ToString(),
                                                       objUtilityDB.GetColumnName(iStartColumn) + (Convert.ToInt32(dtExcel.Rows[0]["no_Of_Branches"]) + 4).ToString());
                                rgHead.Borders.Weight = 2;
                                rgHead.Font.Size = 12; rgHead.Font.Bold = true;

                                for (int iMonths = 0; iMonths <= Convert.ToInt32(dtExcel.Rows[0]["no_of_devs"]); iMonths++)
                                {
                                    iStartColumn = (3 * iMonths) + 6; iColumnCounter = iStartColumn;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["no_Of_Branches"]) + 4, iColumnCounter] = "=SUM(" + objUtilityDB.GetColumnName(iColumnCounter) + "3:" + objUtilityDB.GetColumnName(iColumnCounter) + "" + (Convert.ToInt32(dtExcel.Rows[0]["no_Of_Branches"]) + 3).ToString() + ")";
                                    iColumnCounter = iColumnCounter + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["no_Of_Branches"]) + 4, iColumnCounter] = "=SUM(" + objUtilityDB.GetColumnName(iColumnCounter) + "3:" + objUtilityDB.GetColumnName(iColumnCounter) + "" + (Convert.ToInt32(dtExcel.Rows[0]["no_Of_Branches"]) + 3).ToString() + ")";
                                    iColumnCounter = iColumnCounter + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["no_Of_Branches"]) + 4, iColumnCounter] = "=SUM(" + objUtilityDB.GetColumnName(iColumnCounter) + "3:" + objUtilityDB.GetColumnName(iColumnCounter) + "" + (Convert.ToInt32(dtExcel.Rows[0]["no_Of_Branches"]) + 3).ToString() + ")";
                                    iColumnCounter = iColumnCounter + 1;

                                }



                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }

                if (cbReportType.SelectedIndex == 3)
                {
                    objExcelDB = new ExcelDB();
                    dtExcel = objExcelDB.Get_AuditTourScheduleSummary(Company, Branches, DocumentMonth, "", dtpFromDate.Value.ToString("dd/MMM/yyyy"), dtpToDate.Value.ToString("dd/MMM/yyyy"), "").Tables[0];
                    objExcelDB = null;
                    if (dtExcel.Rows.Count > 0)
                    {
                        try
                        {

                            Excel.Application oXL = new Excel.Application();
                            Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                            Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                            oXL.Visible = true;
                            int iTotColumns = 0;
                            iTotColumns = 10;
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
                            rgData.Value2 = "AUDIT TOUR SCHEDULE SUMMARY   From : " + dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper() + " \t\t  To : " + dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper() + " ";
                            rgData.VerticalAlignment = Excel.Constants.xlCenter;
                            rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                            rgData.Font.ColorIndex = 30;

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
                            rg.Cells.ColumnWidth = 30;
                            rg = worksheet.get_Range("D4", Type.Missing);
                            rg.Cells.ColumnWidth = 20;
                            rg = worksheet.get_Range("E4", Type.Missing);
                            rg.Cells.ColumnWidth = 8;
                            rg = worksheet.get_Range("F4", Type.Missing);
                            rg.Cells.ColumnWidth = 8;
                            rg = worksheet.get_Range("G4", Type.Missing);
                            rg.Cells.ColumnWidth = 8;
                            rg = worksheet.get_Range("H4", Type.Missing);
                            rg.Cells.ColumnWidth = 8;
                            rg = worksheet.get_Range("I4", Type.Missing);
                            rg.Cells.ColumnWidth = 8;

                            int iColumn = 1, iStartRow = 4;
                            worksheet.Cells[iStartRow, iColumn++] = "SlNo";
                            worksheet.Cells[iStartRow, iColumn++] = "Company";
                            worksheet.Cells[iStartRow, iColumn++] = "Emp Name";
                            worksheet.Cells[iStartRow, iColumn++] = "Desig";
                            worksheet.Cells[iStartRow, iColumn++] = "Camp Days";
                            worksheet.Cells[iStartRow, iColumn++] = "HO Days";
                            worksheet.Cells[iStartRow, iColumn++] = "Holidays";
                            worksheet.Cells[iStartRow, iColumn++] = "Journey Days";
                            worksheet.Cells[iStartRow, iColumn++] = "Leaves";
                            worksheet.Cells[iStartRow, iColumn++] = "Total Days";

                            iStartRow++; iColumn = 1;

                            for (int i = 0; i < dtExcel.Rows.Count; i++)
                            {
                                worksheet.Cells[iStartRow, iColumn++] = i + 1;
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ts_company_name"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ts_Emp_Name"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ts_Emp_Desig"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ts_camp_days"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ts_HO_days"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ts_holidays"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ts_journey_days"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ts_leave_days"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ts_total_days"].ToString();

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

                #region "iFrmType==0 :: Audit DR Planning"

                if (iFrmType == 0)
                {
                    if (cbReportType.SelectedIndex == 0)
                    {
                        try
                        {
                            dtExcel = objExcelDB.Get_AuditDRPlanning(Company, Branches, DocumentMonth, strEcodes, dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), "").Tables[0];
                            objExcelDB = null;
                            if (dtExcel.Rows.Count > 0)
                            {
                                Excel.Application oXL = new Excel.Application();
                                Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                                Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                                oXL.Visible = true;
                                int iTotColumns = 0;
                                iTotColumns = 4 + Convert.ToInt32(dtExcel.Rows[0]["AuPnA_No_of_Days"]);
                                string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                                Excel.Range rg = worksheet.get_Range("A3", sLastColumn + 3);
                                Excel.Range rgData = worksheet.get_Range("A3", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["AuPnA_No_Of_Emp"]) + 3).ToString());
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
                                rg.Cells.RowHeight = 20;
                                rgData = worksheet.get_Range("A4", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["AuPnA_No_Of_Emp"]) + 3).ToString());
                                rgData.WrapText = true;
                                rg = worksheet.get_Range("A3", Type.Missing);
                                rg.Cells.ColumnWidth = 4;
                                rg = worksheet.get_Range("B2", "C2");
                                rg.Merge(Type.Missing);
                                rg.Value2 = "REPORTING STRUCTURE";
                                rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                                rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                                rg = worksheet.get_Range("B3", Type.Missing);
                                rg.Cells.ColumnWidth = 40;
                                rg = worksheet.get_Range("C3", Type.Missing);
                                rg.Cells.ColumnWidth = 40;
                                rg = worksheet.get_Range("D3", Type.Missing);
                                rg.Cells.ColumnWidth = 50;
                                
                                int iColumn = 1;
                                worksheet.Cells[3, iColumn++] = "SlNo";
                                worksheet.Cells[3, iColumn++] = "Level-1";
                                worksheet.Cells[3, iColumn++] = "Level-2";
                                worksheet.Cells[3, iColumn++] = "Emp Name";

                                Excel.Range rgHead;
                                int iStartColumn = 0;
                                for (int iDev = 0; iDev < Convert.ToInt32(dtExcel.Rows[0]["AuPnA_No_of_Days"]); iDev++)
                                {
                                    rgHead = worksheet.get_Range("A1", "D1");
                                    rgHead.Merge(Type.Missing);
                                    rgHead.Font.Size = 14;
                                    rgHead.Font.ColorIndex = 30;
                                    rgHead.Font.Bold = true;
                                    rgHead.Cells.RowHeight = 30;
                                    rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                    rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                    rgHead.Cells.Value2 = "AUDIT TOUR SCHEDULE \t From : " + dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper() + " \t\t  To : " + dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper() + " ";

                                    iStartColumn = (iDev) + 5;
                                }


                                int iRowCounter = 4; int iColumnCounter = 1;
                                for (int i = 0; i < dtExcel.Rows.Count; i++)
                                {
                                    if (i > 0)
                                    {

                                        if (dtExcel.Rows[i]["AuPnA_Ecode"].ToString() == dtExcel.Rows[i - 1]["AuPnA_Ecode"].ToString())
                                        {
                                            int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["AuPnA_SL_NO"]);
                                            //int iStartColumn = 0;
                                            iColumnCounter = (1 * (iMonthNo - 1)) + 5;
                                            rg = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "3", objUtilityDB.GetColumnName(iColumnCounter) + "3");
                                            rg.Cells.Value2 = Convert.ToDateTime(dtExcel.Rows[i]["AuPnA_Plan_Date"].ToString()).ToString("dd/MMM/yyyy");
                                            rg.WrapText = true;

                                            rg.Font.Bold = true;
                                            rg.Borders.Weight = 2;
                                            //rgHead.Interior.ColorIndex = 31;
                                            //rgHead.Font.ColorIndex = 2;
                                            rg.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                            rg.Cells.VerticalAlignment = Excel.Constants.xlCenter;

                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["AuPnA_Plan_Location"].ToString();
                                        }

                                        else
                                        {

                                            iRowCounter++;
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 3;

                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["AuPnA_Report_Ecode2"].ToString() + '-' + dtExcel.Rows[i]["AuPnA_Report_EName2"].ToString();
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["AuPnA_Report_Ecode1"].ToString() + '-' + dtExcel.Rows[i]["AuPnA_Report_EName1"].ToString();
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["AuPnA_Ecode"].ToString() + '-' + dtExcel.Rows[i]["AuPnA_EName"].ToString();



                                            int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["AuPnA_SL_NO"]);

                                            iColumnCounter = (1 * (iMonthNo - 1)) + 5;
                                            rg = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "3", objUtilityDB.GetColumnName(iColumnCounter) + "3");
                                            rg.Cells.Value2 = Convert.ToDateTime(dtExcel.Rows[i]["AuPnA_Plan_Date"].ToString()).ToString("dd/MMM/yyyy");
                                            rg.WrapText = true;

                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["AuPnA_Plan_Location"];

                                        }
                                    }
                                    else
                                    {

                                        worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 3;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["AuPnA_Report_Ecode2"].ToString() + '-' + dtExcel.Rows[i]["AuPnA_Report_EName2"].ToString();
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["AuPnA_Report_Ecode1"].ToString() + '-' + dtExcel.Rows[i]["AuPnA_Report_EName1"].ToString();
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["AuPnA_Ecode"].ToString() + '-' + dtExcel.Rows[i]["AuPnA_EName"].ToString();
                                        
                                        int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["AuPnA_SL_NO"]);
                                        //int iStartColumn = 0;
                                        iColumnCounter = (1 * (iMonthNo - 1)) + 5;
                                        rg = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "3", objUtilityDB.GetColumnName(iColumnCounter) + "3");
                                        rg.Cells.Value2 = Convert.ToDateTime(dtExcel.Rows[i]["AuPnA_Plan_Date"].ToString()).ToString("dd/MMM/yyyy");
                                        rg.WrapText = true;

                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["AuPnA_Plan_Location"];

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
                }

                #endregion

                #region "iFrmType==0 :: Audit Solvation Summary"

                if (iFrmType == 0 && cbReportType.SelectedIndex == 2)
                {
                    objExcelDB = new ExcelDB();

                    dtExcel = objExcelDB.Get_AuditSolvationSummary(Company, Branches, "", DocumentMonth, "SUMMARY").Tables[0];
                    objExcelDB = null;
                    if (dtExcel.Rows.Count > 0)
                    {
                        try
                        {

                            Excel.Application oXL = new Excel.Application();
                            Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                            Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                            oXL.Visible = true;
                            int iTotColumns = 0;
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
                            rgData.Value2 = "AUDIT SOLVATION SUMMARY";
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
                            rg.Cells.ColumnWidth = 40;
                            rg = worksheet.get_Range("C4", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("D4", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("E4", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg = worksheet.get_Range("F4", Type.Missing);
                            rg.Cells.ColumnWidth = 8;
                            rg = worksheet.get_Range("G4", Type.Missing);
                            rg.Cells.ColumnWidth = 8;
                            rg = worksheet.get_Range("H4", Type.Missing);
                            rg.Cells.ColumnWidth = 8;
                            rg = worksheet.get_Range("I4", Type.Missing);
                            rg.Cells.ColumnWidth = 8;


                            int iColumn = 1, iStartRow = 4;
                            worksheet.Cells[iStartRow, iColumn++] = "SlNo";
                            worksheet.Cells[iStartRow, iColumn++] = "Branch Name";
                            worksheet.Cells[iStartRow, iColumn++] = "Visit Month";
                            worksheet.Cells[iStartRow, iColumn++] = "Doc Month";
                            worksheet.Cells[iStartRow, iColumn++] = "Total Points";
                            worksheet.Cells[iStartRow, iColumn++] = "Solved Points";
                            worksheet.Cells[iStartRow, iColumn++] = "Solved-FollowUp";
                            worksheet.Cells[iStartRow, iColumn++] = "Unsolved Points";
                            worksheet.Cells[iStartRow, iColumn++] = "% Of Solvation";

                            iStartRow++; iColumn = 1;

                            for (int i = 0; i < dtExcel.Rows.Count; i++)
                            {
                                worksheet.Cells[iStartRow, iColumn++] = i + 1;
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["aq_branch_name"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["aq_vis_month"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["aq_doc_month"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["aq_tot_points"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["aq_solved_points"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["aq_solved_pointsFUP"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["aq_unsolved_points"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["aq_Percentage_of_Sol"].ToString();

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

        }
       
        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFromDate.Value < dtpToDate.Value)
            {
                double TotDays = (dtpToDate.Value - dtpFromDate.Value).TotalDays;
                dTotDays = TotDays;
            }
            else
            {
                dTotDays = 0;
            }
        }

        private void cbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (iFrmType == 0)
            {
                if (cbReportType.SelectedIndex == 0)
                {
                    grpDates.Visible = true;                  
                    clbDevType.Enabled = false;
                    clbDepartment.Enabled = false;                  
                    cbPptPoint.Enabled = false;
                    cbMgntPoint.Enabled = false;
                    grpStatus.Enabled = false;
                    cbMisconduct.Enabled = false;
                    clbBranchTypes.Enabled = true;
                    tvBranches.Enabled = true;
                    tvDocMonth.Enabled = true;
                    //btnDownload.Visible = false;
                   
                }
                else if(cbReportType.SelectedIndex==1)
                {
                    
                    clbDevType.Enabled = true;
                   
                    clbDepartment.Enabled = false;                   
                    cbPptPoint.Enabled = false;
                    cbMgntPoint.Enabled = false;
                    grpStatus.Enabled = false;
                    cbMisconduct.Enabled = false;
                   
                    grpDates.Visible = false;
                    clbBranchTypes.Enabled = true;
                    tvBranches.Enabled = true;
                    tvDocMonth.Enabled = true;
                    btnDownload.Visible = true;
                }
                else if (cbReportType.SelectedIndex == 2)
                {
                            
                    clbDevType.Enabled = false;
                    clbDepartment.Enabled = false;
                    cbPptPoint.Enabled = false;
                    cbMgntPoint.Enabled = false;
                    grpStatus.Enabled = true;
                    cbMisconduct.Enabled = false;                   
                    grpStatus.Enabled = false;                                     
                    grpDates.Visible = false;
                    clbBranchTypes.Enabled = true;
                    tvBranches.Enabled = true;
                    tvDocMonth.Enabled = true;
                   
                }
                else if (cbReportType.SelectedIndex == 3)
                {
                   
                    clbDevType.Enabled = false;
                    clbDepartment.Enabled = false;
                    cbPptPoint.Enabled = false;
                    cbMgntPoint.Enabled = false;
                    grpStatus.Enabled = false;
                    cbMisconduct.Enabled = false;
                    grpStatus.Enabled = false;
                    grpDates.Visible = true;                    
                    tvDocMonth.Enabled = false;
                    btnDownload.Visible = true;
                }
                else if (cbReportType.SelectedIndex == 4)
                {

                    clbDevType.Enabled = true;

                    clbDepartment.Enabled = false;
                    cbPptPoint.Enabled = false;
                    cbMgntPoint.Enabled = false;
                    grpStatus.Enabled = false;
                    cbMisconduct.Enabled = false;

                    grpDates.Visible = false;
                    clbBranchTypes.Enabled = true;
                    tvBranches.Enabled = true;
                    tvDocMonth.Enabled = true;
                    btnDownload.Visible = true;
                }

            }
            if (iFrmType == 2 && cbReportType.SelectedIndex == 1)
            {
                if (CommonData.LogUserRole == "MANAGEMENT" || CommonData.LogUserId.ToUpper()=="ADMIN"
                    || CommonData.LogUserId=="44139" || CommonData.LogUserId=="40896" || CommonData.LogUserId=="41013")
                {
                    btnSendMail.Visible = true;
                }
                else
                {
                    btnSendMail.Visible = false;
                }
            }
            else
            {
                btnSendMail.Visible = false;
            }

        }

       

        private void ChkStatusAll_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkStatusAll.Checked == true)
            {
                chkSolved.Checked = true;
                chkUnsolved.Checked = true;
                chkSolvedFollUp.Checked = true;
            }
            else
            {
                chkSolved.Checked = false;
                chkUnsolved.Checked = false;
                chkSolvedFollUp.Checked = false;
            }

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

        private void tvDocMonth_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TriStateTreeView.getStatus(e);

            tvDocMonth.BeginUpdate();

            foreach (TreeNode Node in e.Node.Nodes)
            {
                Node.Checked = e.Node.Checked;
            }

            tvDocMonth.EndUpdate();

        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFromDate.Value < dtpToDate.Value)
            {
                double TotDays = (dtpToDate.Value - dtpFromDate.Value).TotalDays;
                dTotDays = TotDays;
            }
            else
            {
                dTotDays = 0;
            }
        }

        private void GetSelectedBranchTypes()
        {
            strBranTypes = "";

            for (int i = 0; i < clbBranchTypes.Items.Count; i++)
            {
                if (clbBranchTypes.GetItemCheckState(i) == CheckState.Checked)
                {
                    strBranTypes += "" + ((SSAdmin.NewCheckboxListItem)(clbBranchTypes.Items[i])).Tag.ToString() + ",";
                }
            }

            if (strBranTypes.Length >= 2)
            {
                strBranTypes = strBranTypes.TrimEnd(',');               
            }
        }

        private void clbBranchTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            tvBranches.Nodes.Clear();
            chkComp.Visible = false;

            GetSelectedBranchTypes();
            if (strBranTypes.Length >= 2)
            {                
                FillBranches();
            }
        }

        private void FillEmployeeData()
        {
            objHRdb = new HRInfo();
            DataTable dt = new DataTable();
            cbEmployees.DataBindings.Clear();
            try
            {

                dt = objHRdb.GetEmployeesForMisconduct("", "", "", txtEnameSearch.Text.ToString()).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    //dr[1] = "--Select--";
                    //dr[3] = "--Select--";

                    //dt.Rows.InsertAt(dr, 0);

                    cbEmployees.DataSource = dt;
                    cbEmployees.DisplayMember = "ENAME";
                    cbEmployees.ValueMember = "EmpDetl";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objHRdb = null;
                dt = null;
            }
        }

        private void txtEnameSearch_KeyUp(object sender, KeyEventArgs e)
        {
            FillEmployeeData();
        }

        private void chkWithExpl_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWithExpl.Checked == true)
            {
                chkWithOutExpl.Checked = false;
            }
            else
            {
                chkWithOutExpl.Checked = true;
            }
        }

        private void chkWithOutExpl_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWithOutExpl.Checked == true)
            {
                chkWithExpl.Checked = false;
            }
            else
            {
                chkWithExpl.Checked = true;
            }
        }

        private void btnSendMail_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "", strMailId = "";
            strEcodes = "";

            if (iFrmType == 2 && cbReportType.SelectedIndex == 1)
            {
                if (CheckData() == true)
                {
                    if (cbEmployees.SelectedIndex >= -1)
                    {
                        strEcodes = cbEmployees.Text.ToString().Split('-')[0];
                    }
                    objExcelDB = new ExcelDB();
                    DataTable dtExcel = objExcelDB.GetAuditQueryReg(Company, Branches, "", DocumentMonth, strEcodes, DevType, strDept, cbMisconduct.Text.ToString(), "ALL", "ALL", "UNSOLVED", "ZONAL_HEAD").Tables[0];

                    if (dtExcel.Rows.Count > 0)
                    {
                        objExcelDB = null;
                        GetSelectedValues();

                        if (DevType == "")
                        {
                            DevType = "ALL";
                        }
                       
                        try
                        {
                            strCmd = "SELECT  HECD_EMP_EMAIL_ID EmpMailId FROM HR_EMP_CONTACT_DETL WHERE HECD_EORA_CODE=" + strEcodes + "";
                            dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                            if (dt.Rows[0]["EmpMailId"].ToString().Length>3)
                            {
                                strMailId = dt.Rows[0]["EmpMailId"].ToString();
                            }
                            if (strMailId.Length > 6)
                            {                              

                                DialogResult dlgResult = MessageBox.Show("Do you want Send Mail to Selected Person?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (dlgResult == DialogResult.Yes)
                                {
                                    string strPath = Application.StartupPath;
                                    strPath = strPath.Replace("\\bin\\Debug", "");
                                    cryRpt.Load(strPath + "\\Reports\\Audit\\SSCRM_REP_AUDIT_MAJOR_POINTS.rpt");
                                    cryRpt.Refresh();
                                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                                    paramCompCode.Value = Company;
                                    cryRpt.ParameterFields["@cmp_cd"].CurrentValues.Add(paramCompCode);

                                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                                    paramBranCode.Value = Branches;
                                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                                    ParameterDiscreteValue paramFinYear = new ParameterDiscreteValue();
                                    paramFinYear.Value = "";
                                    cryRpt.ParameterFields["@xFinYear"].CurrentValues.Add(paramFinYear);

                                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                                    paramDocMon.Value = DocumentMonth;
                                    cryRpt.ParameterFields["@xDoc_month"].CurrentValues.Add(paramDocMon);

                                    ParameterDiscreteValue paramAuditEcode = new ParameterDiscreteValue();
                                    paramAuditEcode.Value = strEcodes;
                                    cryRpt.ParameterFields["@xAuditEcode"].CurrentValues.Add(paramAuditEcode);

                                    ParameterDiscreteValue paramDevType = new ParameterDiscreteValue();
                                    paramDevType.Value = DevType;
                                    cryRpt.ParameterFields["@xDevType"].CurrentValues.Add(paramDevType);

                                    ParameterDiscreteValue paramDept = new ParameterDiscreteValue();
                                    paramDept.Value = strDept;
                                    cryRpt.ParameterFields["@xDept"].CurrentValues.Add(paramDept);

                                    ParameterDiscreteValue paramIsMiscond = new ParameterDiscreteValue();
                                    paramIsMiscond.Value = cbMisconduct.Text.ToString();
                                    cryRpt.ParameterFields["@xIsMisCon"].CurrentValues.Add(paramIsMiscond);

                                    ParameterDiscreteValue paramMgntPnt = new ParameterDiscreteValue();
                                    paramMgntPnt.Value = cbMgntPoint.Text.ToString();
                                    cryRpt.ParameterFields["@xMgntPoint"].CurrentValues.Add(paramMgntPnt);

                                    ParameterDiscreteValue paramPPTPnt = new ParameterDiscreteValue();
                                    paramPPTPnt.Value = cbPptPoint.Text.ToString();
                                    cryRpt.ParameterFields["@xPptPoint"].CurrentValues.Add(paramPPTPnt);

                                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                                    paramStatus.Value = "UNSOLVED";
                                    cryRpt.ParameterFields["@xStatus"].CurrentValues.Add(paramStatus);

                                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                                    paramRepType.Value = "ZONAL_HEAD";
                                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);


                                    SetDataSourceConnectionToReport(ref cryRpt);

                                    //rptViewer.ReportSource = cryRpt;

                                    MailMessage Msg = new MailMessage();
                                    ExportOptions CrExportOptions;
                                    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                                    PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                                    CrDiskFileDestinationOptions.DiskFileName = @"D:\\UnsolvedAuditPoints.pdf";

                                    CrExportOptions = cryRpt.ExportOptions;
                                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                                    cryRpt.Export();


                                    String[] addrCC = { "satyaprasad@sivashakthi.net" };
                                    var fromAddress = new MailAddress("ssbplitho@gmail.com", "SSERP :: Unsolved Audit Points");
                                    var toAddress = new MailAddress("ssgcitho@gmail.com");
                                    const string fromPassword = "ssbplitho5566";
                                                                      
                                   
                                    var smtp = new SmtpClient
                                    {
                                        Host = "smtp.gmail.com",
                                        Port = 587,
                                        EnableSsl = true,
                                        DeliveryMethod = SmtpDeliveryMethod.Network,
                                        UseDefaultCredentials = true,
                                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                                    };

                                    Msg.CC.Add("satyaprasad@sivashakthi.net");
                                    Msg.CC.Add(toAddress);
                                    Msg.To.Add(strMailId);
                                    Msg.CC.Add("vijayssgc@gmail.com");
                                    Msg.CC.Add("auditssbpl@gmail.com");
                                    Msg.CC.Add("auditsbtl@gmail.com");
                                    Msg.CC.Add("bijayia@gmail.com");
                                    Msg.Bcc.Add("mundruvijaya@gmail.com");

                                   
                                    Msg.From = fromAddress;
                                    Msg.Subject = "Unsolved Audit Points as on Date";
                                    Msg.Body = " Dear Sir , \n\n\t    Please find the following Attached Unsolved Audit points Of Your Zone/Region.";
                                    Msg.Attachments.Add(new Attachment("D:\\UnsolvedAuditPoints.pdf"));

                                    // System.Web.Mail.SmtpMail.Send(Msg);                                
                                   
                                    smtp.Send(Msg);
                                    Msg.Dispose();
                                    Cursor.Current = Cursors.Default;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Employee Mail-Id Was Not Updated in ERP", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                Cursor.Current = Cursors.Default;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("No Attachment Send to Mail", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Cursor.Current = Cursors.Default;
                    }    
                }
                            
            }
        }

        #region SetDataSourceConnectionToReport

        private void SetDataSourceConnectionToReport(ref ReportDocument InvoiceRepDocument)
        {
            try
            {
                ConnectionInfo SQLConnection;
                System.Data.SqlClient.SqlConnectionStringBuilder connectionParser;
                TableLogOnInfo RepTableLogOnInfo = null;
                SQLConnection = new ConnectionInfo();
                objSecurity = new Security();
                //connectionParser = new SqlConnectionStringBuilder(objSecurity.GetDecodeString(ConfigurationManager.AppSettings["DBCon"].ToString()));
                SQLConnection.ServerName = "202.63.115.34\\sbpl";
                SQLConnection.DatabaseName = "SIVASHAKTHI";
                SQLConnection.IntegratedSecurity = false;
                SQLConnection.UserID = "sbpl";
                SQLConnection.Password = "sbpl123";
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
                        InvoiceRepDocument.Subreports[CntSr].Database.Tables[Cnt].Location = InvoiceRepDocument.Subreports[CntSr].Database.Tables[Cnt].Location.Replace(";1", "");
                        //InvoiceRepDocument.Subreports[CntSr].Database.Tables[Cnt].Location = SQLConnection.DatabaseName + ".dbo." + InvoiceRepDocument.Subreports[CntSr].Database.Tables[Cnt].Location.Replace(";1", "");
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
                    InvoiceRepDocument.Database.Tables[Cnt].Location = InvoiceRepDocument.Database.Tables[Cnt].Location.Replace(";1", "");
                    //InvoiceRepDocument.Database.Tables[Cnt].Location = SQLConnection.DatabaseName + ".dbo." + InvoiceRepDocument.Database.Tables[Cnt].Location.Replace(";1", "");

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
