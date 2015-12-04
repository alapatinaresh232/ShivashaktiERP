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
using SSTrans;
using System.IO;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace SSCRM
{
    public partial class BranchAttendence : Form
    {
        private InvoiceDB objInvDB = null;
        ReportViewer childReportViewer;
        SQLDB objSQLdb = null;
        ExcelDB objExcelDB = null;
        private UtilityDB objUtilityDB = null;



        private string Company = string.Empty;
        private string State = string.Empty;
        private string Branches = string.Empty;
        private string Category = string.Empty;
        private string ItemCompany = string.Empty;
        private string ItemState = string.Empty;
        private string ItemBranches = string.Empty;
        private string items = string.Empty;

        string FistCatId = "";
        string LastCatId = "";
        string FistBrncode = "";
        string LastBrancode = "";
        private int iFrmType = 0;
        int CatSlNo = 0;

        string strBranchType = "";
        string strDept = "";
        string company = "";
        string branches = "";
        public BranchAttendence()
        {
            InitializeComponent();
        }
        public BranchAttendence(int iForm)
        {
            iFrmType = iForm;
            InitializeComponent();
        }

        private void StationaryCategory_Load(object sender, EventArgs e)
        {
            FillBranchTypes();
            FillBranches();
            FillDepartments();
            cbReport.SelectedIndex = 0;
            dtpFromDate.Value = DateTime.Now;
            dtpToDate.Value = DateTime.Now;
            //cbLocationType.SelectedIndex = ;
        }

        private void FillDepartments()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
            clbDept.DataSource = null;
            try
            {
                strCommand = "SELECT dept_code,dept_name  FROM Dept_Mas";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    ((ListBox)clbDept).DataSource = dt;
                    ((ListBox)clbDept).DisplayMember = "dept_name";
                    ((ListBox)clbDept).ValueMember = "dept_code";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FillBranches()
        {
            strBranchType = "";
            foreach (DataRowView view in clbBrType.CheckedItems)
            {
                strBranchType += (view[clbBrType.ValueMember].ToString()) + ",";
            }
            if (strBranchType.Length > 0)
                strBranchType = strBranchType.Substring(0, strBranchType.Length - 1);
            tvCompBranches.Nodes.Clear();
            InvoiceDB objInv = new InvoiceDB();
            objUtilityDB = new UtilityDB();
            DataSet ds = new DataSet();
            if (CommonData.LogUserId.ToUpper() == "ADMIN")
                ds = objInv.AdminBranchCursor_Get("", "", "PARENT");
            else
                ds = objUtilityDB.UserBranchCursor_Get(CommonData.LogUserId, "", "", "PARENT");
            TreeNode tNode;
          
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    tvCompBranches.Nodes.Add(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), ds.Tables[0].Rows[i]["COMPANY_NAME"].ToString());
                    DataSet dschild = new DataSet();
                    if (CommonData.LogUserId.ToUpper() == "ADMIN")
                    {

                        dschild = objInv.AdminBranchCursor_Get(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), strBranchType, "CHILD");
                    }
                    
                    else
                    {


                        dschild = objUtilityDB.UserBranchCursor_Get(CommonData.LogUserId, ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), strBranchType, "CHILD");
                    }
                    //tvBranches.Nodes[i].Nodes.Add("BRANCHES" + "(" + dschild.Tables[0].Rows.Count + ")");
                    if (dschild.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                        {
                            tvCompBranches.Nodes[i].Nodes.Add(dschild.Tables[0].Rows[j]["BRANCH_CODE"].ToString(), dschild.Tables[0].Rows[j]["BRANCH_NAME"].ToString());
                        }
                    }
                }
            }

        }

        private DataSet Get_UserBranchStateFilterCursor(string sCompCode, string sStateCode, string sLogUserId, string sBranchtType, string sGetType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompany", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sStateCode", DbType.String, sStateCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sUser", DbType.String, sLogUserId, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sBranchType", DbType.String, sBranchtType, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sType", DbType.String, sGetType, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("Get_Stationary_UserBranchState", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }
        private void FillBranchTypes()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
            clbBrType.DataSource = null;
            try
            {
                strCommand = "SELECT Distinct(BRANCH_TYPE) BranchType " +
                             ", CASE  WHEN BRANCH_TYPE='BR' THEN 'BRANCH' " +
                             "  WHEN BRANCH_TYPE='SP' THEN 'STOCK POINT' " +
                             "  WHEN BRANCH_TYPE='PU' THEN 'PRODUCTION UNIT' " +
                             "  WHEN BRANCH_TYPE='TR' THEN 'TRANSPORT UNIT' " +
                             "  WHEN BRANCH_TYPE='HO' THEN 'HEAD OFFICE' " +
                             "  WHEN BRANCH_TYPE='ST' THEN 'STORE' " +
                             "  WHEN BRANCH_TYPE='OL' THEN 'OUTLETS' END BranchTypeName " +
                             "  FROM BRANCH_MAS WHERE BRANCH_TYPE NOT IN('PO') ORDER BY BranchTypeName";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    ((ListBox)clbBrType).DataSource = dt;
                    ((ListBox)clbBrType).DisplayMember = "BranchTypeName";
                    ((ListBox)clbBrType).ValueMember = "BranchType";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void lbBranchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillBranches();
        }

        private void chkCompAll_CheckedChanged(object sender, EventArgs e)
        {
            strBranchType = "";
            if (chkCompAll.Checked == true)
            {
                for (int i = 0; i < clbBrType.Items.Count; i++)
                {
                    clbBrType.SetItemCheckState(i, CheckState.Checked);
                }

                foreach (DataRowView view in clbBrType.CheckedItems)
                {
                    strBranchType += (view[clbBrType.ValueMember].ToString()) + ",";
                }
            }
            else
            {
                for (int i = 0; i < clbBrType.Items.Count; i++)
                {
                    clbBrType.SetItemCheckState(i, CheckState.Unchecked);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            strDept = "";
            if (checkBox1.Checked == true)
            {
                for (int i = 0; i < clbDept.Items.Count; i++)
                {
                    clbDept.SetItemCheckState(i, CheckState.Checked);
                }

                foreach (DataRowView view in clbDept.CheckedItems)
                {
                    strDept += (view[clbDept.ValueMember].ToString()) + ",";
                }
                strDept = strDept.Substring(0, strDept.Length - 1);
            }
            else
            {
                for (int i = 0; i < clbDept.Items.Count; i++)
                {
                    clbDept.SetItemCheckState(i, CheckState.Unchecked);
                }
            }
        }

        private void tvCompBranches_AfterCheck(object sender, TreeViewEventArgs e)
        {
            tvCompBranches.BeginUpdate();

            foreach (TreeNode Node in e.Node.Nodes)
            {
                Node.Checked = e.Node.Checked;
            }

            tvCompBranches.EndUpdate();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            GetSelectedControlsIDs();
            if (cbReport.Text == "MONTHWISE")
            {
                ReportViewer childReportViewer = new ReportViewer(company, branches,strDept, dtpFromDate.Value.ToString("dd-MMM-yyyy"), dtpToDate.Value.ToString("dd-MMM-yyyy"), "WAGEATTD");
                CommonData.ViewReport = "SSERP_REP_HR_BR_ATTD_REG";
                childReportViewer.Show();
            }
            if (cbReport.Text == "DAYWISE")
            {
                ReportViewer childReportViewer = new ReportViewer(company, branches, strDept, dtpFromDate.Value.ToString("dd-MMM-yyyy"), dtpFromDate.Value.ToString("dd-MMM-yyyy"), "DAYATTD");
                CommonData.ViewReport = "HR_HO_ATTD_DAYATTDy2";
                childReportViewer.Show();
            }
        }
        public void GetSelectedControlsIDs()
        {
            company = ""; branches = "";
            bool iscomp = false;
            for (int i = 0; i < tvCompBranches.Nodes.Count; i++)
            {
                for (int j = 0; j < tvCompBranches.Nodes[i].Nodes.Count; j++)
                {
                    if (tvCompBranches.Nodes[i].Nodes[j].Checked == true)
                    {
                        if (branches != string.Empty)
                            branches += ",";
                        branches += tvCompBranches.Nodes[i].Nodes[j].Name.ToString();
                        iscomp = true;
                    }
                }
                if (iscomp == true)
                {
                    if (company != string.Empty)
                        company += ",";
                    company += tvCompBranches.Nodes[i].Name.ToString();
                }
                iscomp = false;
            }

            if (strDept.Length == 0)
            {
                strDept = "ALL";
            }
        }

        private void clbDept_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //strDept = "";

            //foreach (DataRowView view in clbDept.CheckedItems)
            //{
            //    strDept += (view[clbDept.ValueMember].ToString()) + ",";
            //}

            //if (strDept.Length > 0)
            //{
            //    strDept = strDept.Substring(0, strDept.Length - 1);
            //}
        }

        private void clbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            strDept = "";

            foreach (DataRowView view in clbDept.CheckedItems)
            {
                strDept += (view[clbDept.ValueMember].ToString()) + ",";
            }

            if (strDept.Length > 0)
            {
                strDept = strDept.Substring(0, strDept.Length - 1);
            }

        }

        private void cbReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbReport.Text=="MONTHWISE")
            {
                lblToDate.Visible = true;
                dtpToDate.Visible = true;
                lblFromDate.Text = "From";
            }
            if (cbReport.Text == "DAYWISE")
            {
                lblFromDate.Text = "ToDay";
                lblToDate.Visible = false;
                dtpToDate.Visible = false;
            }
        }

        //private void FillStationaryItems()
        //{
        //    objInvDB = new InvoiceDB();
        //    DataSet ds = new DataSet();
        //    string strCatId = string.Empty;
        //    string strItem = string.Empty;
        //    string strCategoryId = string.Empty;
        //    string strCompanycode = string.Empty;

        //    string strEndCompanycode = string.Empty;
        //    string strStateCode = "";
        //    string strBranchCode = "";
        //    string strItemCode = "";
        //    //if (txtItemName.Text.ToString().Trim().Length > 2)
        //    ////    strItem = txtItemName.Text;
        //    tvItems.Nodes.Clear();
        //    ds = objInvDB.IndentStationaryList_Get(CommonData.CompanyCode, CommonData.BranchCode, strItem);
        //    TreeNode tNode;

        //    tNode = tvItems.Nodes.Add("Stationary");
        //    Int16 intNode = 0, intNode1 = 0, intNode2 = 0, intNode3 = 0;
        //    Int16 tComp = 0;

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {

        //            if (strCategoryId != ds.Tables[0].Rows[i]["sis_category_id"].ToString())
        //            {
        //                strCategoryId = ds.Tables[0].Rows[i]["sis_category_id"].ToString();
        //                tvItems.Nodes[0].Nodes.Add(ds.Tables[0].Rows[i]["sis_category_id"].ToString(), ds.Tables[0].Rows[i]["sis_category_name"].ToString());
        //                DataRow[] dr1 = ds.Tables[0].Select("sis_category_id='" + strCategoryId + "'");
        //                intNode1 = 0;
        //                strCompanycode = "";
        //                for (int j = 0; j < dr1.Length; j++)
        //                {

        //                    if (strCompanycode != dr1[j]["sis_company_code"].ToString())
        //                    {
        //                        strCompanycode = dr1[j]["sis_company_code"].ToString();
        //                        tvItems.Nodes[0].Nodes[intNode].Nodes.Add(dr1[j]["sis_company_code"].ToString(), dr1[j]["sis_company_name"].ToString());
        //                        DataRow[] dr2 = ds.Tables[0].Select("sis_category_id='" + strCategoryId + "' and sis_company_code='" + strCompanycode + "'");
        //                        //strStateCode="";
        //                        intNode2 = 0;
        //                        for (int k = 0; k < dr2.Length; k++)
        //                        {
        //                            if (strStateCode != dr2[k]["sis_state_code"].ToString())
        //                            {
        //                                strStateCode = dr2[k]["sis_state_code"].ToString();
        //                                tvItems.Nodes[0].Nodes[intNode].Nodes[intNode1].Nodes.Add(dr2[k]["sis_state_code"].ToString(), dr2[k]["sis_state_name"].ToString());
        //                                DataRow[] dr3 = ds.Tables[0].Select("sis_category_id='" + strCategoryId + "' and sis_company_code='" + strCompanycode +
        //                                              "' and sis_state_code='" + strStateCode + "'");
        //                                intNode3 = 0;
        //                                for (int l = 0; l < dr3.Length; l++)
        //                                {
        //                                    if (strBranchCode != dr3[l]["sis_branch_code"].ToString())
        //                                    {
        //                                        strBranchCode = dr3[l]["sis_branch_code"].ToString();
        //                                        tvItems.Nodes[0].Nodes[intNode].Nodes[intNode1].Nodes[intNode2].Nodes.Add(dr3[l]["sis_branch_code"].ToString(), dr3[l]["sis_branch_name"].ToString());
        //                                        DataRow[] dr4 = ds.Tables[0].Select("sis_category_id='" + strCategoryId + "' and sis_company_code='" + strCompanycode +
        //                                              "' and sis_state_code='" + strStateCode + "' and sis_branch_code='" + strBranchCode + "'");

        //                                        for (int m = 0; m < dr4.Length; m++)
        //                                        {
        //                                            tvItems.Nodes[0].Nodes[intNode].Nodes[intNode1].Nodes[intNode2].Nodes[intNode3].Nodes.Add(dr4[m]["sis_item_code"].ToString()
        //                                                                                              , dr4[m]["sis_item_name"].ToString());

        //                                        }
        //                                        intNode3++;
        //                                    }
        //                                }
        //                                intNode2++;
        //                                strBranchCode = "";
        //                            }
        //                        }
        //                        intNode1++;
        //                        strStateCode = "";
        //                    }
        //                }
        //                intNode++;


        //            }

        //        }
        //    }


        //    //DataRow[] dr1 = ds.Tables[0].Select("SIC_CATEGORY_ID='" + dr["SIC_CATEGORY_ID"].ToString() + "'");
        //    //if (dr1.Length > 0)
        //    //{
        //    //    tvItems.Nodes[0].Nodes[0].Nodes.Add(dr["SIM_ITEM_CODE"].ToString(), dr["SIM_ITEM_NAME"].ToString() + "  - Price: "  + dr["SIM_ITEM_PRICE"].ToString());
        //    //}

        //    //tvItems.Nodes[0].Nodes.Add("STATIONARY WITH COMPANY NAME");
        //    //tvItems.Nodes[0].Nodes.Add("STATIONARY WITH COMPANY NAME");
        //    //tvItems.Nodes[0].Nodes.Add("STATIONARY WITH-OUT COMPANY NAME");
        //    //if (ds.Tables[0].Rows.Count > 0)
        //    //{
        //    //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    //    {
        //    //        tvItems.Nodes[0].Nodes[0].Nodes.Add(ds.Tables[0].Rows[i]["ITEM_PRICE"].ToString(), ds.Tables[0].Rows[i]["ITEM_NAME"].ToString());
        //    //    }
        //    //}
        //    //if (ds.Tables[1].Rows.Count > 0)
        //    //{
        //    //    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
        //    //    {
        //    //        tvItems.Nodes[0].Nodes[1].Nodes.Add(ds.Tables[1].Rows[i]["ITEM_PRICE"].ToString(), ds.Tables[1].Rows[i]["ITEM_NAME"].ToString());
        //    //    }
        //    //}
        //    tvItems.Nodes[0].Expand();
        //    tvItems.Nodes[0].Nodes[0].Expand();
        //    //tvItems.Nodes[0].Nodes[1].Expand();
        //}

        //private void btnClose_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //    this.Dispose();
        //}
        //private void GetSelectedCompAndBranches()
        //{
        //    Company = "";
        //    Branches = "";
        //    State = "";

        //    bool iscomp = false;
        //    bool iSstate = false;
        //    for (int i = 0; i < tvBranches.Nodes.Count; i++)
        //    {
        //        for (int j = 0; j < tvBranches.Nodes[i].Nodes.Count; j++)
        //        {
        //            for (int k = 0; k < tvBranches.Nodes[i].Nodes[j].Nodes.Count; k++)
        //            {

        //                if (tvBranches.Nodes[i].Nodes[j].Nodes[k].Checked == true)
        //                {
        //                    if (Branches != string.Empty)
        //                        Branches += ",";
        //                    Branches += tvBranches.Nodes[i].Nodes[j].Nodes[k].Name.ToString();
        //                    iscomp = true;
        //                    iSstate = true;
        //                }

        //            }
        //            if (iSstate == true)
        //            {
        //                if (State != string.Empty)
        //                    State += ",";
        //                State += tvBranches.Nodes[i].Nodes[j].Name.ToString();
        //            }
        //            iSstate = false;
        //        }

        //        if (iscomp == true)
        //        {
        //            if (Company != string.Empty)
        //                Company += ",";
        //            Company += tvBranches.Nodes[i].Name.ToString();
        //        }
        //        iscomp = false;
        //    }
        //}
        //private void GetSelectedItems()
        //{
        //    Category = "";
        //    ItemCompany = "";
        //    ItemBranches = "";
        //    ItemState = "";
        //    items = "";
        //    bool iscategory = false;
        //    bool iscomp = false;
        //    bool iSstate = false;
        //    bool isbranch = false;
        //    for (int i = 0; i < tvItems.Nodes[0].Nodes.Count; i++)
        //    {
        //        for (int j = 0; j < tvItems.Nodes[0].Nodes[i].Nodes.Count; j++)
        //        {
        //            for (int k = 0; k < tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes.Count; k++)
        //            {
        //                for (int l = 0; l < tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Nodes.Count; l++)
        //                {
        //                    for (int m = 0; m < tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Nodes[l].Nodes.Count; m++)
        //                    {
        //                        if (tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Nodes[l].Nodes[m].Checked == true)
        //                        {

        //                            if (items != string.Empty)
        //                                items += ",";
        //                            items += Convert.ToInt32(tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Nodes[l].Nodes[m].Name.ToString());
        //                            iscomp = true;
        //                            iSstate = true;
        //                            iscategory = true;
        //                            isbranch = true;

        //                        }
        //                    }
        //                    if (tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Nodes[l].Checked == true)
        //                    {
        //                        if (ItemBranches != string.Empty)
        //                            ItemBranches += ",";
        //                        ItemBranches += tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Nodes[l].Name.ToString();
        //                        iscomp = true;
        //                        iSstate = true;
        //                        iscategory = true;
        //                        isbranch = true;
        //                    }

        //                }
        //                if (tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Checked == true)
        //                {
        //                    if (ItemState != string.Empty)
        //                        ItemState += ",";
        //                    ItemState += tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Name.ToString();
        //                    iscomp = true;
        //                    iSstate = true;
        //                    iscategory = true;
        //                    isbranch = true;
        //                }
        //            }
        //            if (iscomp == true)
        //            {
        //                if (ItemCompany != string.Empty)
        //                    ItemCompany += ",";
        //                ItemCompany += tvItems.Nodes[0].Nodes[i].Name.ToString();
        //            }
        //            iscomp = false;
        //        }

        //        if (iscategory == true)
        //        {
        //            if (Category != string.Empty)
        //                Category += ",";
        //            Category += tvItems.Nodes[0].Nodes[i].Name.ToString();
        //        }
        //        iscategory = false;
        //    }
        //}


        //private bool CheckData()
        //{
        //    bool flag = true;
        //    //if (cbLocationType.SelectedIndex== 0)
        //    //{
        //    //    MessageBox.Show("Select Atleast One Branch Type ", "SSCRM-Report", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //    //    return false;
        //    //}

        //    flag = false;

        //    for (int a = 0; a < tvBranches.Nodes.Count; a++)
        //    {
        //        for (int b = 0; b < tvBranches.Nodes[a].Nodes.Count; b++)
        //        {
        //            for (int c = 0; c < tvBranches.Nodes[a].Nodes[b].Nodes.Count; c++)
        //            {

        //                if (tvBranches.Nodes[a].Nodes[b].Nodes[c].Checked == true)
        //                {
        //                    flag = true;
        //                }
        //            }
        //        }
        //    }
        //    if (flag == false)
        //    {
        //        MessageBox.Show("Please Select Atleast One Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return flag;
        //    }

        //    flag = false;

        //    for (int i = 0; i < tvItems.Nodes[0].Nodes.Count; i++)
        //    {
        //        for (int j = 0; j < tvItems.Nodes[0].Nodes[i].Nodes.Count; j++)
        //        {
        //            for (int k = 0; k < tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes.Count; k++)
        //            {
        //                for (int l = 0; l < tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Nodes.Count; l++)
        //                {
        //                    for (int m = 0; m < tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Nodes[l].Nodes.Count; m++)
        //                    {
        //                        if (tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Nodes[l].Nodes[m].Checked == true)
        //                        {
        //                            flag = true;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    if (flag == false)
        //    {
        //        MessageBox.Show("Please Select Atleast One Item", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return flag;
        //    }


        //    if (cbTrnType.SelectedIndex == 0)
        //    {
        //        flag = false;
        //        MessageBox.Show("Please Select Report Type", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        cbTrnType.Focus();
        //        return flag;
        //    }

        //    return flag;

        //}


        //private void btnReport_Click(object sender, EventArgs e)
        //{
        //    GetSelectedCompAndBranches();
        //    GetSelectedItems();
        //    if (CheckData() == true)
        //    {
        //        if (cbRepType.SelectedIndex == 0)
        //        {
        //            if (cbTrnType.SelectedIndex == 1)
        //            {
        //                CommonData.ViewReport = "BRANCH WISE ITEMS SEARCH";
        //                childReportViewer = new ReportViewer(Company, Branches, dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), items, "INDENT", "");
        //                childReportViewer.Show();
        //            }
        //            if (cbTrnType.SelectedIndex == 2)
        //            {
        //                CommonData.ViewReport = "BRANCH WISE ITEMS SEARCH";
        //                childReportViewer = new ReportViewer(Company, Branches, dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), items, "DISPATCHES", "");
        //                childReportViewer.Show();
        //            }
        //            if (cbTrnType.SelectedIndex == 3)
        //            {
        //                CommonData.ViewReport = "BRANCH WISE ITEMS SEARCH";
        //                childReportViewer = new ReportViewer(Company, Branches, dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), items, "CLOSING", "");
        //                childReportViewer.Show();
        //            }
        //        }
        //        if (cbRepType.SelectedIndex == 1)
        //        {
        //            if (cbTrnType.SelectedIndex == 1)
        //            {
        //                CommonData.ViewReport = "CATEGORY WISE BRANCH ITEMS SEARCH";
        //                childReportViewer = new ReportViewer(Company, Branches, dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), items, "INDENT", "");
        //                childReportViewer.Show();
        //            }
        //            if (cbTrnType.SelectedIndex == 2)
        //            {
        //                CommonData.ViewReport = "CATEGORY WISE BRANCH ITEMS SEARCH";
        //                childReportViewer = new ReportViewer(Company, Branches, dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), items, "DISPATCHES", "");
        //                childReportViewer.Show();
        //            }
        //            if (cbTrnType.SelectedIndex == 3)
        //            {
        //                CommonData.ViewReport = "CATEGORY WISE BRANCH ITEMS SEARCH";
        //                childReportViewer = new ReportViewer(Company, Branches, dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), items, "CLOSING", "");
        //                childReportViewer.Show();
        //            }
        //        }
        //    }
        //}

        //private void cbLocationType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    FillBranches();
        //}
        //private DataSet Get_StationaryItemsSummary(string sCompCode, string sBranchCode, string sfromDate, string sToDate,
        //                                         string sItemId, string TrnType, string RepType)
        //{
        //    objSQLdb = new SQLDB();
        //    SqlParameter[] param = new SqlParameter[7];
        //    DataSet ds = new DataSet();
        //    try
        //    {

        //        param[0] = objSQLdb.CreateParameter("@xCMPNY", DbType.String, sCompCode, ParameterDirection.Input);
        //        param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
        //        param[2] = objSQLdb.CreateParameter("@xFrom", DbType.String, sfromDate, ParameterDirection.Input);
        //        param[3] = objSQLdb.CreateParameter("@xTo", DbType.String, sToDate, ParameterDirection.Input);
        //        param[4] = objSQLdb.CreateParameter("@xItemID", DbType.String, sItemId, ParameterDirection.Input);
        //        param[5] = objSQLdb.CreateParameter("@xTrnType", DbType.String, TrnType, ParameterDirection.Input);
        //        param[6] = objSQLdb.CreateParameter("@RepType", DbType.String, RepType, ParameterDirection.Input);

        //        ds = objSQLdb.ExecuteDataSet("SSCRM_REP_STATIONARY_REC_ALLBR_SUMM", CommandType.StoredProcedure, param);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.ToString());
        //    }
        //    finally
        //    {
        //        param = null;
        //        objSQLdb = null;
        //    }
        //    return ds;
        //}
        //private void btnDownload_Click(object sender, EventArgs e)
        //{
        //    DataTable dtExcel = new DataTable();
        //    objExcelDB = new ExcelDB();
        //    objUtilityDB = new UtilityDB();

        //    GetSelectedCompAndBranches();
        //    GetSelectedItems();

        //    if (CheckData() == true)
        //    {        

        //        #region " Report1 :: Stationary Summary"

        //        if (cbRepType.SelectedIndex == 0)
        //        {
        //            objExcelDB = new ExcelDB();
        //            objUtilityDB = new UtilityDB();

        //            dtExcel = Get_StationaryItemsSummary(Company, Branches, dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), items, cbTrnType.Text, "").Tables[0];
        //            objExcelDB = null;

        //                           if (dtExcel.Rows.Count > 0)
        //            {
        //                try
        //                {
        //                    Excel.Application oXL = new Excel.Application();
        //                    Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
        //                    Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
        //                    oXL.Visible = true;
        //                    int iTotColumns = 0;
        //                    iTotColumns = 4 + (1 * Convert.ToInt32(dtExcel.Rows[0]["rs_No_Of_items"])) + 1;
        //                    string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
        //                    Excel.Range rg = worksheet.get_Range("A4", sLastColumn + "4");
        //                    Excel.Range rgData = worksheet.get_Range("A4", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["rs_No_Of_Branchs"]) + 4).ToString());
        //                    rgData.Font.Size = 11;
        //                    rgData.WrapText = true;
        //                    rgData.VerticalAlignment = Excel.Constants.xlCenter;
        //                    rgData.Borders.Weight = 2;


        //                    rg.Font.Bold = true;
        //                    rg.Font.Name = "Times New Roman";
        //                    rg.Font.Size = 10;
        //                    rg.WrapText = true;
        //                    rg.Font.ColorIndex = 2; // White Color : 2 and Red = 3,30; Green = 10,43; 
        //                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
        //                    rg.Interior.ColorIndex = 31;
        //                    rg.Borders.Weight = 2;
        //                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
        //                  //  rg.Cells.RowHeight = 38;
        //                    rgData = worksheet.get_Range("A5", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["rs_No_Of_Branchs"]) + 5).ToString());
        //                    rgData.WrapText = false;
        //                    rg = worksheet.get_Range("A4", Type.Missing);
        //                    rg.Cells.ColumnWidth = 4;
        //                    rg = worksheet.get_Range("B4", Type.Missing);
        //                    rg.Cells.ColumnWidth = 30;
        //                    rg = worksheet.get_Range("C4", Type.Missing);
        //                    rg.Cells.ColumnWidth = 15;
        //                    rg = worksheet.get_Range("D4", Type.Missing);
        //                    rg.Cells.ColumnWidth = 45;
        //                    rg.WrapText = true;


        //                    int iColumn = 1;
        //                    worksheet.Cells[4, iColumn++] = "SlNo";
        //                    worksheet.Cells[4, iColumn++] = "Company";
        //                    worksheet.Cells[4, iColumn++] = "State";
        //                    worksheet.Cells[4, iColumn++] = "Branch Name";
        //                    worksheet.Cells.WrapText = true;

        //                    //for (int i = 0; i < dtExcel.Rows.Count; i++)
        //                    //{
        //                    //    if (i > 0)
        //                    //    {
        //                    //        if (dtExcel.Rows[i]["rs_category_id"].ToString() == dtExcel.Rows[i - 1]["rs_category_id"].ToString())
        //                    //        {
        //                    //            rg = worksheet.get_Range("E3:" + objUtilityDB.GetColumnName(Convert.ToInt32(dtExcel.Rows[0]["rs_category_count"]) + 5) + "3", Type.Missing);
        //                    //            rg.Merge(Type.Missing);
        //                    //            rg.Borders.Weight = 2;
        //                    //            rg.Cells.ColumnWidth = 10;
        //                    //            rg.Font.ColorIndex = 2;
        //                    //            rg.Interior.ColorIndex = 31;
        //                    //            rg.Cells.Value2 = dtExcel.Rows[i]["rs_category"];
        //                    //        }
        //                    //        else
        //                    //        {
        //                    //            rg = worksheet.get_Range("E3:" + objUtilityDB.GetColumnName(Convert.ToInt32(dtExcel.Rows[0]["rs_category_count"]) + 5) + "3", Type.Missing);
        //                    //            rg.Merge(Type.Missing);
        //                    //            rg.Borders.Weight = 2;
        //                    //            rg.Cells.ColumnWidth = 10;
        //                    //            rg.Font.ColorIndex = 2;
        //                    //            rg.Interior.ColorIndex = 31;
        //                    //            rg.Cells.Value2 = dtExcel.Rows[i]["rs_category"];
        //                    //        }
        //                    //    }
        //                    //    else
        //                    //    {
        //                    //        rg = worksheet.get_Range("E3:" + objUtilityDB.GetColumnName(Convert.ToInt32(dtExcel.Rows[0]["rs_category_count"]) + 5) + "3", Type.Missing);
        //                    //        rg.Merge(Type.Missing);
        //                    //        rg.Borders.Weight = 2;
        //                    //        rg.Cells.ColumnWidth = 10;
        //                    //        rg.Font.ColorIndex = 2;
        //                    //        rg.Interior.ColorIndex = 31;
        //                    //        rg.Cells.Value2 = dtExcel.Rows[i]["rs_category"];
        //                    //    }
        //                    //}


        //                    Excel.Range rgHead;
        //                    int iStartColumn = 0;
        //                    for (int iProd = 0; iProd < Convert.ToInt32(dtExcel.Rows[0]["rs_No_Of_items"]); iProd++)
        //                    {
        //                        rgHead = worksheet.get_Range("A1", "K1");
        //                        rgHead.Merge(Type.Missing);
        //                        rgHead.Font.Size = 14;
        //                        rgHead.Font.ColorIndex = 1;
        //                        rgHead.Font.Bold = true;
        //                        //rgHead.Cells.RowHeight = 30;
        //                        rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
        //                        rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
        //                        rgHead.Cells.Value2 = rgHead.Cells.Value2 = "STATIONARY" + cbTrnType.Text + "";



        //                        iStartColumn = (1 * iProd) + 5;

        //                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "4", objUtilityDB.GetColumnName(iStartColumn) + "4");


        //                        rgHead.Merge(Type.Missing);
        //                        rgHead.Interior.ColorIndex = 34 + 1;
        //                        rgHead.Borders.Weight = 2;
        //                        rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
        //                        //rgHead.Cells.RowHeight = 30;
        //                        rgHead.Font.Size = 14;
        //                        rgHead.Font.ColorIndex = 1;
        //                        rgHead.Font.Bold = true;
        //                        rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
        //                        rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;


        //                       // worksheet.Cells[4, iStartColumn++] = "Qty";
        //                        //worksheet.Cells[3, iStartColumn++] = "Damage";
        //                        //worksheet.Cells[3, iStartColumn++] = "Total";


        //                    }

        //                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn+1) + "4", objUtilityDB.GetColumnName(iStartColumn+1) + "4");
        //                    rgHead.Merge(Type.Missing);
        //                    rgHead.Interior.ColorIndex = 34 + 1;
        //                    rgHead.Borders.Weight = 2;
        //                    rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
        //                    //rgHead.Cells.RowHeight = 30;
        //                    rgHead.Font.Size = 10;
        //                    rgHead.Font.ColorIndex = 1;
        //                    rgHead.Font.Bold = true;
        //                    rgHead.Cells.Value2 = "TOTAL";
                            

                          
        //                    rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
        //                    rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;

        //                   // worksheet.Cells[4, iStartColumn++] = "Qty";
        //                    //worksheet.Cells[3, iStartColumn++] = "Damage";
        //                    //worksheet.Cells[3, iStartColumn++] = "Total";



        //                    int iRowCounter = 5; int iColumnCounter = 1;
        //                    for (int i = 0; i < dtExcel.Rows.Count; i++)
        //                    {
        //                        if (i > 0)
        //                        {

        //                            if (dtExcel.Rows[i]["rs_branch_code"].ToString() == dtExcel.Rows[i - 1]["rs_branch_code"].ToString())
        //                            {

        //                                int iMnthNo = Convert.ToInt32(dtExcel.Rows[i]["rs_Item_sl_no"]);
        //                                //int iStartColumn = 0;
        //                                iColumnCounter = (1 * (iMnthNo - 1)) + 5;
        //                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "3", objUtilityDB.GetColumnName(iColumnCounter) + "3");
        //                                rgHead.Cells.Value2 = dtExcel.Rows[i]["rs_category"];
        //                                rgHead.WrapText = true;
        //                                rgHead.Interior.ColorIndex = 34 + 1;
        //                                rgHead.Font.ColorIndex = 1;
        //                                rgHead.Font.Bold = true;
        //                                rgHead.Borders.Weight = 2;
        //                                rgHead.Font.Size = 10;                                      
        //                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
        //                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
        //                                if (dtExcel.Rows[i]["rs_category_id"].ToString() == dtExcel.Rows[i - 1]["rs_category_id"].ToString())
        //                                {
        //                                    int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["rs_Item_sl_no"]);
        //                                    //int iStartColumn = 0;
        //                                    iColumnCounter = (1 * (iMonthNo - 1)) + 5;
        //                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "4", objUtilityDB.GetColumnName(iColumnCounter) + "4");
        //                                    rgHead.Cells.Value2 = dtExcel.Rows[i]["rs_item_name"];
        //                                    rgHead.WrapText = true;
        //                                    rgHead.Interior.ColorIndex = 34 + 1;
        //                                    rgHead.Font.ColorIndex = 1;
        //                                    rgHead.Font.Bold = true;
        //                                    rgHead.Borders.Weight = 2;
        //                                    rgHead.Font.Size = 8;

        //                                    //rgHead.Interior.ColorIndex = 31;
        //                                    //rgHead.Font.ColorIndex = 2;
        //                                    rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
        //                                    rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
        //                                    //rgHead.Orientation = 90;

        //                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["Qty"];
        //                                    //worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Damage_qty"];
        //                                    //worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Qty"];
        //                                }
                                      
        //                            }

        //                            else
        //                            {

        //                                iRowCounter++;
        //                                worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 4;
        //                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["rs_company_name"];
        //                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["rs_state_Name"];
        //                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["rs_branch_name"];
        //                                int iMnthNo = Convert.ToInt32(dtExcel.Rows[i]["rs_Item_sl_no"]);
        //                                iColumnCounter = (1 * (iMnthNo - 1)) + 5;
        //                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "3", objUtilityDB.GetColumnName(iColumnCounter) + "3");
        //                                rgHead.Cells.Value2 = dtExcel.Rows[i]["rs_category"];
        //                                rgHead.WrapText = true;
        //                                rgHead.Interior.ColorIndex = 34 + 1;
        //                                rgHead.Font.ColorIndex = 1;
        //                                rgHead.Font.Bold = true;
        //                                rgHead.Borders.Weight = 2;
        //                                rgHead.Font.Size = 10;
        //                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
        //                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;

        //                                int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["rs_Item_sl_no"]);

        //                                iColumnCounter = (1 * (iMonthNo - 1)) + 5;
        //                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "4", objUtilityDB.GetColumnName(iColumnCounter) + "4");
        //                                rgHead.Cells.Value2 = dtExcel.Rows[i]["rs_item_name"];
        //                                rgHead.WrapText = true;
        //                               // rgHead.Orientation = 90;
        //                                 rgHead.Font.Size =8;
        //                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["Qty"];
        //                                iColumnCounter = iTotColumns;
        //                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["rs_tot_Qty"];
                                       

        //                            }
        //                        }
        //                        else
        //                        {

        //                            worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 4;
        //                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["rs_company_name"];
        //                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["rs_state_Name"];
        //                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["rs_branch_name"];
        //                            int iMnthNo = Convert.ToInt32(dtExcel.Rows[i]["rs_Item_sl_no"]);
        //                            iColumnCounter = (1 * (iMnthNo - 1)) + 5;
        //                            rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "3", objUtilityDB.GetColumnName(iColumnCounter) + "3");
        //                            rgHead.Cells.Value2 = dtExcel.Rows[i]["rs_category"];
        //                            rgHead.WrapText = true;
        //                            rgHead.Interior.ColorIndex = 34 + 1;
        //                            rgHead.Font.ColorIndex = 1;
        //                            rgHead.Font.Bold = true;
        //                            rgHead.Borders.Weight = 2;
        //                            rgHead.Font.Size = 10;
        //                            rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
        //                            rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;


        //                            int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["rs_Item_sl_no"]);
        //                            //int iStartColumn = 0;
        //                            iColumnCounter = (1 * (iMonthNo - 1)) + 5;
        //                            rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "4", objUtilityDB.GetColumnName(iColumnCounter) + "4");
        //                            rgHead.Cells.Value2 = dtExcel.Rows[i]["rs_item_name"];
        //                           // rgHead.Orientation = 90;
        //                            rgHead.WrapText = true;                                  
        //                            rgHead.Font.Size =8;
        //                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["Qty"];
        //                            //worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Damage_qty"];
        //                            //worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Qty"];

        //                            iColumnCounter = iTotColumns ;

        //                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["rs_tot_Qty"];
        //                            rg.Font.Bold = true;
        //                            //worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Total_Qty"];

        //                        }

        //                        iColumnCounter = 1;
        //                    }


        //                    iStartColumn = 4 + (1 * (Convert.ToInt32(dtExcel.Rows[0]["rs_No_Of_items"]))) + 1;
        //                    iColumnCounter = iStartColumn;
        //                    rgHead = worksheet.get_Range("E" + (Convert.ToInt32(dtExcel.Rows[0]["rs_No_Of_Branchs"]) + 5).ToString(),
        //                                           objUtilityDB.GetColumnName(iStartColumn) + (Convert.ToInt32(dtExcel.Rows[0]["rs_No_Of_Branchs"]) + 5).ToString());
        //                    rgHead.Borders.Weight = 2;
        //                    rgHead.Font.Size = 12; rgHead.Font.Bold = true;


        //                    rg = worksheet.get_Range("A" + (Convert.ToInt32(dtExcel.Rows[0]["rs_No_Of_Branchs"]) + 5).ToString(),
        //                                            "D" + (Convert.ToInt32(dtExcel.Rows[0]["rs_No_Of_Branchs"]) + 5).ToString());
        //                    rg.Merge(Type.Missing);
        //                    rg.Value2 = "Total";
        //                    rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 14;
        //                    rg.Font.ColorIndex = 30;
        //                    rg.VerticalAlignment = Excel.Constants.xlCenter;
        //                    rg.HorizontalAlignment = Excel.Constants.xlCenter;


        //                    for (int iProduct = 0; iProduct <= Convert.ToInt32(dtExcel.Rows[0]["rs_No_Of_items"]); iProduct++)
        //                    {
        //                        iStartColumn = (1 * iProduct) + 5; iColumnCounter = iStartColumn;
        //                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["rs_No_Of_Branchs"]) + 5, iColumnCounter] = "=SUM(" + objUtilityDB.GetColumnName(iColumnCounter) + "5:" + objUtilityDB.GetColumnName(iColumnCounter) + "" + (Convert.ToInt32(dtExcel.Rows[0]["rs_No_Of_Branchs"]) + 4).ToString() + ")";
        //                        iColumnCounter = iColumnCounter + 1;

        //                    }
        //                }

        //                catch (Exception ex)
        //                {
        //                    MessageBox.Show(ex.ToString());
        //                }
        //            }
        //        }

        //        #endregion
        //        #region " Report2 :: Stationary Summary"

        //        if (cbRepType.SelectedIndex == 1)
        //        {
        //            objExcelDB = new ExcelDB();
        //            objUtilityDB = new UtilityDB();

        //            dtExcel = Get_StationaryItemsSummary(Company, Branches, dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), items, cbTrnType.Text, "ITEMS").Tables[0];
        //            objExcelDB = null;

        //            if (dtExcel.Rows.Count > 0)
        //            {
        //                try
        //                {
        //                    Excel.Application oXL = new Excel.Application();
        //                    Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
        //                    Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
        //                    oXL.Visible = true;
        //                    int iTotColumns = 0;
        //                    iTotColumns = 3 + (1 * Convert.ToInt32(dtExcel.Rows[0]["rs_No_Of_Branchs"])) + 1;
        //                    string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
        //                    Excel.Range rg = worksheet.get_Range("A4", sLastColumn + "4");
        //                    Excel.Range rgData = worksheet.get_Range("A4", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["rs_No_Of_items"]) + 5).ToString());
        //                    rgData.Font.Size = 11;
        //                    rgData.WrapText = true;
        //                    rgData.VerticalAlignment = Excel.Constants.xlCenter;
        //                    rgData.Borders.Weight = 2;


        //                    rg.Font.Bold = true;
        //                    rg.Font.Name = "Times New Roman";
        //                    rg.Font.Size = 10;
        //                    rg.WrapText = true;
        //                    rg.Font.ColorIndex = 2; // White Color : 2 and Red = 3,30; Green = 10,43; 
        //                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
        //                    rg.Interior.ColorIndex = 31;
        //                    rg.Borders.Weight = 2;
        //                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
        //                    //  rg.Cells.RowHeight = 38;
        //                    rgData = worksheet.get_Range("A5", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["rs_No_Of_Branchs"]) + 5).ToString());
        //                    rgData.WrapText = false;
        //                    rg = worksheet.get_Range("A4", Type.Missing);
        //                    rg.Cells.ColumnWidth = 4;
        //                    rg = worksheet.get_Range("B4", Type.Missing);
        //                    rg.Cells.ColumnWidth = 30;
        //                    rg = worksheet.get_Range("C4", Type.Missing);
        //                    rg.Cells.ColumnWidth = 30;                        


        //                    int iColumn = 1;
        //                    worksheet.Cells[4, iColumn++] = "SlNo";
        //                    worksheet.Cells[4, iColumn++] = "Category";                           
        //                    worksheet.Cells[4, iColumn++] = "Item Name";
        //                    worksheet.Cells.WrapText = true;
        //                    Excel.Range rgHead;

        //                    //for (int i = 0; i < dtExcel.Rows.Count; i++)
        //                    //{
        //                    //    if (i > 0)
        //                    //    {
        //                    //        if (dtExcel.Rows[i]["rs_company_code"].ToString() == dtExcel.Rows[i - 1]["rs_company_code"].ToString())
        //                    //        {
        //                    //            rg = worksheet.get_Range(objUtilityDB.GetColumnName(Convert.ToInt32(dtExcel.Rows[0]["rs_No_Of_Branchs"]) + 5) + objUtilityDB.GetColumnName(Convert.ToInt32(dtExcel.Rows[0]["rs_comp_count"]) + 5) + "3", Type.Missing);
        //                    //            rg.Merge(Type.Missing);
        //                    //            rg.Borders.Weight = 2;
        //                    //            rg.Cells.ColumnWidth = 10;
        //                    //            rg.Font.ColorIndex = 2;
        //                    //            rg.Interior.ColorIndex = 31;
        //                    //            rg.Cells.Value2 = dtExcel.Rows[i]["rs_company_name"];
        //                    //        }
        //                    //        else
        //                    //        {
        //                    //            rg = worksheet.get_Range("D3:" + objUtilityDB.GetColumnName(Convert.ToInt32(dtExcel.Rows[0]["rs_comp_count"]) + 5) + "3", Type.Missing);
        //                    //            rg.Merge(Type.Missing);
        //                    //            rg.Borders.Weight = 2;
        //                    //            rg.Cells.ColumnWidth = 10;
        //                    //            rg.Font.ColorIndex = 2;
        //                    //            rg.Interior.ColorIndex = 31;
        //                    //            rg.Cells.Value2 = dtExcel.Rows[i]["rs_company_name"];
        //                    //        }
        //                    //    }
        //                    //    else
        //                    //    {
        //                    //        rg = worksheet.get_Range("D3:" + objUtilityDB.GetColumnName(Convert.ToInt32(dtExcel.Rows[0]["rs_comp_count"]) + 5) + "3", Type.Missing);
        //                    //        rg.Merge(Type.Missing);
        //                    //        rg.Borders.Weight = 2;
        //                    //        rg.Cells.ColumnWidth = 10;
        //                    //        rg.Font.ColorIndex = 2;
        //                    //        rg.Interior.ColorIndex = 31;
        //                    //        rg.Cells.Value2 = dtExcel.Rows[i]["rs_company_name"];
        //                    //    }
        //                    //}

        //                    int iStartColumn = 0;
        //                    for (int iProd = 0; iProd < Convert.ToInt32(dtExcel.Rows[0]["rs_No_Of_Branchs"]); iProd++)
        //                    {
        //                        rgHead = worksheet.get_Range("A1", "K1");
        //                        rgHead.Merge(Type.Missing);
        //                        rgHead.Font.Size = 14;
        //                        rgHead.Font.ColorIndex = 1;
        //                        rgHead.Font.Bold = true;
        //                        //rgHead.Cells.RowHeight = 30;
        //                        rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
        //                        rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
        //                        rgHead.Cells.Value2 = rgHead.Cells.Value2 = "STATIONARY" + cbTrnType.Text + "";



        //                        iStartColumn = (1 * iProd) + 4;

        //                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "4", objUtilityDB.GetColumnName(iStartColumn) + "4");


        //                        rgHead.Merge(Type.Missing);
        //                        rgHead.Interior.ColorIndex = 34 + 1;
        //                        rgHead.Borders.Weight = 2;
        //                        rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
        //                        //rgHead.Cells.RowHeight = 30;
        //                        rgHead.Font.Size = 14;
        //                        rgHead.Font.ColorIndex = 1;
        //                        rgHead.Font.Bold = true;
        //                        rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
        //                        rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;


        //                        // worksheet.Cells[4, iStartColumn++] = "Qty";
        //                        //worksheet.Cells[3, iStartColumn++] = "Damage";
        //                        //worksheet.Cells[3, iStartColumn++] = "Total";


        //                    }

        //                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn + 1) + "4", objUtilityDB.GetColumnName(iStartColumn + 1) + "4");
        //                    rgHead.Merge(Type.Missing);
        //                    rgHead.Interior.ColorIndex = 34 + 1;
        //                    rgHead.Borders.Weight = 2;
        //                    rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
        //                    //rgHead.Cells.RowHeight = 30;
        //                    rgHead.Font.Size = 10;
        //                    rgHead.Font.ColorIndex = 1;
        //                    rgHead.Font.Bold = true;
        //                    rgHead.Cells.Value2 = "TOTAL";



        //                    rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
        //                    rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;

        //                    // worksheet.Cells[4, iStartColumn++] = "Qty";
        //                    //worksheet.Cells[3, iStartColumn++] = "Damage";
        //                    //worksheet.Cells[3, iStartColumn++] = "Total";



        //                    int iRowCounter = 5; int iColumnCounter = 1;
        //                    for (int i = 0; i < dtExcel.Rows.Count; i++)
        //                    {
        //                        if (i > 0)
        //                        {

        //                            if (dtExcel.Rows[i]["rs_item_id"].ToString() == dtExcel.Rows[i - 1]["rs_item_id"].ToString())
        //                            {
        //                                int iMnthNo = Convert.ToInt32(dtExcel.Rows[i]["rs_Branch_sl_no"]);
        //                                //int iStartColumn = 0;
        //                                iColumnCounter = (1 * (iMnthNo - 1)) + 4;
        //                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "3", objUtilityDB.GetColumnName(iColumnCounter) + "3");
        //                                rgHead.Cells.Value2 = dtExcel.Rows[i]["rs_company_name"];
        //                                rgHead.WrapText = true;
        //                                rgHead.Interior.ColorIndex = 34 + 1;
        //                                rgHead.Font.ColorIndex = 1;
        //                                rgHead.Font.Bold = true;
        //                                rgHead.Borders.Weight = 2;
        //                                rg.Cells.ColumnWidth = 20;

        //                                rgHead.Merge(Type.Missing);
        //                                rgHead.Font.Size = 10;                                      
        //                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
        //                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
        //                                if (dtExcel.Rows[i]["rs_company_code"].ToString() == dtExcel.Rows[i - 1]["rs_company_code"].ToString())
        //                                {
        //                                    int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["rs_Branch_sl_no"]);
        //                                    //int iStartColumn = 0;
        //                                    iColumnCounter = (1 * (iMonthNo - 1)) + 4;
        //                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "4", objUtilityDB.GetColumnName(iColumnCounter) + "4");
        //                                    rgHead.Cells.Value2 = dtExcel.Rows[i]["rs_branch_name"];
        //                                    rgHead.WrapText = true;
        //                                    rgHead.Interior.ColorIndex = 34 + 1;
        //                                    rgHead.Font.ColorIndex = 1;
        //                                    rgHead.Font.Bold = true;
        //                                    rgHead.Borders.Weight = 2;
        //                                    rgHead.Font.Size = 8;

        //                                    //rgHead.Interior.ColorIndex = 31;
        //                                    //rgHead.Font.ColorIndex = 2;
        //                                    rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
        //                                    rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
        //                                    //rgHead.Orientation = 90;

        //                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["Qty"];
        //                                    //worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Damage_qty"];
        //                                    //worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Qty"];
        //                                }
        //                            }

        //                            else
        //                            {

        //                                iRowCounter++;
        //                                worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 4;
        //                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["rs_category"];
        //                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["rs_item_name"];
        //                                //worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["rs_branch_name"];

        //                                int iMnthNo = Convert.ToInt32(dtExcel.Rows[i]["rs_Branch_sl_no"]);
        //                                //int iStartColumn = 0;
        //                                iColumnCounter = (1 * (iMnthNo - 1)) + 4;
        //                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "3", objUtilityDB.GetColumnName(iColumnCounter) + "3");
        //                                rgHead.Cells.Value2 = dtExcel.Rows[i]["rs_company_name"];
        //                                rgHead.WrapText = true;
        //                                rgHead.Interior.ColorIndex = 34 + 1;
        //                                rgHead.Font.ColorIndex = 1;
        //                                rgHead.Font.Bold = true;
        //                                rgHead.Borders.Weight = 2;
        //                                rg.Cells.ColumnWidth = 20;
        //                                rgHead.Font.Size = 10;
        //                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
        //                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;

        //                                int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["rs_Branch_sl_no"]);
        //                                iColumnCounter = (1 * (iMonthNo - 1)) + 4;
        //                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "4", objUtilityDB.GetColumnName(iColumnCounter) + "4");
        //                                rgHead.Cells.Value2 = dtExcel.Rows[i]["rs_branch_name"];
        //                                rgHead.WrapText = true;
        //                                // rgHead.Orientation = 90;
        //                                rgHead.Font.Size = 8;
        //                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["Qty"];
        //                                iColumnCounter = iTotColumns ;                                     
        //                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["rs_tot_Qty"];


        //                            }
        //                        }
        //                        else
        //                        {

        //                            worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 4;
        //                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["rs_category"];
        //                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["rs_item_name"];
        //                            //worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["rs_branch_name"];

        //                            int iMnthNo = Convert.ToInt32(dtExcel.Rows[i]["rs_Branch_sl_no"]);
        //                            //int iStartColumn = 0;
        //                            iColumnCounter = (1 * (iMnthNo - 1)) + 4;
        //                            rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "3", objUtilityDB.GetColumnName(iColumnCounter) + "3");
        //                            rgHead.Cells.Value2 = dtExcel.Rows[i]["rs_company_name"];
        //                            rgHead.WrapText = true;
        //                            rgHead.Interior.ColorIndex = 34 + 1;
        //                            rgHead.Font.ColorIndex = 1;
        //                            rgHead.Font.Bold = true;
        //                            rgHead.Borders.Weight = 2;
        //                            rg.Cells.ColumnWidth = 20;
        //                            rgHead.Font.Size = 10;
        //                            rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
        //                            rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
        //                            int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["rs_Branch_sl_no"]);
        //                            //int iStartColumn = 0;
        //                            iColumnCounter = (1 * (iMonthNo - 1)) + 4;
        //                            rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "4", objUtilityDB.GetColumnName(iColumnCounter) + "4");
        //                            rgHead.Cells.Value2 = dtExcel.Rows[i]["rs_branch_name"];
        //                            // rgHead.Orientation = 90;
        //                            rgHead.WrapText = true;

        //                            rgHead.Font.Size = 8;
        //                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["Qty"];
        //                            //worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Damage_qty"];
        //                            //worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Qty"];

        //                            iColumnCounter = iTotColumns;

        //                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["rs_tot_Qty"];
        //                            //worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Total_Qty"];

        //                        }

        //                        iColumnCounter = 1;
        //                    }

        //                    iStartColumn = 3 + (1 * (Convert.ToInt32(dtExcel.Rows[0]["rs_No_Of_Branchs"]))) + 1;
        //                    iColumnCounter = iStartColumn;
        //                    rgHead = worksheet.get_Range("D" + (Convert.ToInt32(dtExcel.Rows[0]["rs_No_Of_items"]) + 5).ToString(),
        //                                           objUtilityDB.GetColumnName(iStartColumn) + (Convert.ToInt32(dtExcel.Rows[0]["rs_No_Of_items"]) + 5).ToString());
        //                    rgHead.Borders.Weight = 2;
        //                    rgHead.Font.Size = 12; rgHead.Font.Bold = true;


        //                    rg = worksheet.get_Range("A" + (Convert.ToInt32(dtExcel.Rows[0]["rs_No_Of_items"]) + 5).ToString(),
        //                                            "C" + (Convert.ToInt32(dtExcel.Rows[0]["rs_No_Of_items"]) + 5).ToString());
        //                    rg.Merge(Type.Missing);
        //                    rg.Value2 = "Total";
        //                    rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 14;
        //                    rg.Font.ColorIndex = 30;
        //                    rg.VerticalAlignment = Excel.Constants.xlCenter;
        //                    rg.HorizontalAlignment = Excel.Constants.xlCenter;


        //                    for (int iProduct = 0; iProduct <= Convert.ToInt32(dtExcel.Rows[0]["rs_No_Of_Branchs"]); iProduct++)
        //                    {
        //                        iStartColumn = (1 * iProduct) + 4; iColumnCounter = iStartColumn;
        //                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["rs_No_Of_items"]) + 5, iColumnCounter] = "=SUM(" + objUtilityDB.GetColumnName(iColumnCounter) + "5:" + objUtilityDB.GetColumnName(iColumnCounter) + "" + (Convert.ToInt32(dtExcel.Rows[0]["rs_No_Of_items"]) + 4).ToString() + ")";
        //                        iColumnCounter = iColumnCounter + 1;

        //                    }


                           
        //                }

        //                catch (Exception ex)
        //                {
        //                    MessageBox.Show(ex.ToString());
        //                }
        //            }
        //        }

        //        #endregion
              
                
                
        //    }
        //}
    }
}

        
    
    

                

      
    


