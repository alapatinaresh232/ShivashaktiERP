using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SSCRMDB;
using SSTrans;
using SSAdmin;
using Excel = Microsoft.Office.Interop.Excel;

namespace SSCRM
{
    public partial class frmBranchAuditReportsFilters : Form
    {
        private InvoiceDB objInv = null;
        private Master objMaster = null;
      
        private UtilityDB objUtilityDB = null;
        private ReportViewer childReportViewer = null;
        ExcelDB objExcelDB = null;

        AuditDB objAuditDB = null;
        SQLDB objSQLdb = null;
        double dTotDays;
  
       
        private string  Company = "",Branches = "", DocumentMonth = "";
        private string DevType = "", SubDevType = "", strDept = "", strEcodes = "", strMisConduct = "", strPptPnt = "", strMgntPnt = "", strStatus = "", strBranTypes = "";
        private int iFrmType = 0;
        double nTotDays = 0;


        public frmBranchAuditReportsFilters()
        {
            InitializeComponent();
        }

        public frmBranchAuditReportsFilters(int iRepType)
        {
            InitializeComponent();
            iFrmType = iRepType;
        }
       

        private void AuditReportsFilters_Load(object sender, EventArgs e)
        {                   

            FillBranchTypes();
            FillBranches();
            FillDocumentMonths();
            FillReportType();
            FillDepartmentData();
           
            chkComp.Visible = false;
           
        }


        private void FillReportType()
        {
            if (iFrmType == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("type", typeof(string));
                dt.Columns.Add("name", typeof(string));                 

                dt.Rows.Add("AUDIT MAJOR POINTS", "AUDIT MAJOR POINTS");               
                dt.Rows.Add("AUDIT SOLVATION SUMMARY", "AUDIT SOLVATION SUMMARY");

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

            if (strBranTypes.Length == 0)
            {
                MessageBox.Show("Select Atleast One Branch Type ", "SSCRM-Report", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                for (int i = 0; i < tvDocMonth.Nodes.Count; i++)
                {
                    for (int j = 0; j < tvDocMonth.Nodes[i].Nodes.Count; j++)
                    {
                        if (tvDocMonth.Nodes[i].Nodes[j].Nodes.Count > 0)
                            tvDocMonth.Nodes[i].Nodes[j].FirstNode.Expand();
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
            strDept = ""; strMgntPnt = ""; strPptPnt = ""; strStatus = ""; strMisConduct = ""; strEcodes = "";


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
                if (chkUnsolved.Checked == true && chkSolvedFollUp.Checked == true)
                {
                    strStatus = "SOLVED-FOLLOWUP,UNSOLVED";
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
       
      
        private void btnReport_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {

                GetSelectedValues();


                if (strStatus == "")
                {
                    strStatus = "ALL";
                }

                if (iFrmType == 0)
                {
                    if (cbReportType.SelectedIndex == 0)
                    {
                        CommonData.ViewReport = "SSCRM_REP_AUDIT_MAJOR_POINTS";
                        childReportViewer = new ReportViewer(Company, Branches, "", DocumentMonth, strEcodes, "ALL", strDept, "ALL", "ALL", "ALL", strStatus, "AUDIT_QUERY_REGISTER");
                        childReportViewer.Show();
                        
                    }
                    else if (cbReportType.SelectedIndex == 1)
                    {
                        CommonData.ViewReport = "SSCRM_REP_AUDIT_POINTS_SUMMARY";
                        childReportViewer = new ReportViewer(Company, Branches, "", DocumentMonth, "SUMMARY");
                        childReportViewer.Show();
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
            FillDepartmentData();
            FillDocumentMonths();
           
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
                
                if (strStatus == "")
                {
                    strStatus = "ALL";
                }

                #region "iFrmType 0 :: Query Register"
                if (iFrmType == 0)
                {
                    if (cbReportType.SelectedIndex == 0)
                    {
                        try
                        {
                            dtExcel = objExcelDB.GetAuditQueryReg(Company, Branches, "", DocumentMonth, strEcodes, "ALL", strDept, "ALL", "ALL", "ALL", strStatus, "AUDIT_QUERY_REGISTER").Tables[0];
                            objExcelDB = null;

                            if (dtExcel.Rows.Count > 0)
                            {
                                Excel.Application oXL = new Excel.Application();
                                Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                                Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                                oXL.Visible = true;
                                int iTotColumns = 0;
                                iTotColumns = 17;
                                string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                                Excel.Range rgHead = null;
                                Excel.Range rg = worksheet.get_Range("A4", sLastColumn + "4");
                                Excel.Range rgData = worksheet.get_Range("A5", sLastColumn + (dtExcel.Rows.Count + 4).ToString());
                                rgData.Font.Size = 11;
                                rgData.WrapText = true;
                                rgData.VerticalAlignment = Excel.Constants.xlCenter;
                                rgData.Borders.Weight = 2;

                                rgData = worksheet.get_Range("A1", "Q2");
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
                                rg.Cells.ColumnWidth = 8;
                                rg = worksheet.get_Range("C4", Type.Missing);
                                rg.Cells.ColumnWidth = 8;
                                rg = worksheet.get_Range("D4", Type.Missing);
                                rg.Cells.ColumnWidth = 8;
                                rg = worksheet.get_Range("E4", Type.Missing);
                                rg.Cells.ColumnWidth = 25;
                                rg = worksheet.get_Range("F4", Type.Missing);
                                rg.Cells.ColumnWidth = 15;
                                rg = worksheet.get_Range("G4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("H4", Type.Missing);
                                rg.Cells.ColumnWidth = 20;
                                rg = worksheet.get_Range("I4", Type.Missing);
                                rg.Cells.ColumnWidth = 40;
                                rg = worksheet.get_Range("K4", Type.Missing);
                                rg.Cells.ColumnWidth = 40;
                                rg = worksheet.get_Range("L4", Type.Missing);
                                rg.Cells.ColumnWidth = 40;
                                rg = worksheet.get_Range("M4", Type.Missing);
                                rg.Cells.ColumnWidth = 40;
                                rg = worksheet.get_Range("N4", Type.Missing);
                                rg.Cells.ColumnWidth = 40;
                                rg = worksheet.get_Range("O4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("P4", Type.Missing);
                                rg.Cells.ColumnWidth = 20;
                                rg = worksheet.get_Range("Q4", Type.Missing);
                                rg.Cells.ColumnWidth = 20;
                                
                                int iColumn = 1, iStartRow = 4;
                                worksheet.Cells[iStartRow, iColumn++] = "SlNo";
                                worksheet.Cells[iStartRow, iColumn++] = "QueryID";
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
                                worksheet.Cells[iStartRow, iColumn++] = "AuditBy";


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
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_audit_ecode"] + "-" + dtExcel.Rows[i]["ad_audit_name"];

                                    iStartRow++; iColumn = 1;
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

                if (iFrmType == 0 && cbReportType.SelectedIndex == 1)
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
        


    
        private void cbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbReportType.SelectedIndex == 0)
            {
                grpStatus.Enabled = true;
            }
            else
            {
                grpStatus.Enabled = false;
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

            GetSelectedBranchTypes();
            if (strBranTypes.Length >= 2)
            {                
                FillBranches();
            }
        }

    
    }
}
