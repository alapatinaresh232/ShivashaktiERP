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
using SSAdmin;
using SSTrans;

namespace SSCRM
{
    public partial class ServiceActivitiesReport : Form
    {
        SQLDB objSQLdb = null;
        StockPointDB objSPdb = null;

        private string Company = "", Branches = "", strEcode = "", sFrmType = "";

        public ServiceActivitiesReport(string strType)
        {
            InitializeComponent();
            sFrmType = strType;
        }


        private void ServiceActivitiesReport_Load(object sender, EventArgs e)
        {
            FillBranches();
            dtpFrmDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
            dtpToDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
        }

        private void FillBranches()
        {
            tvBranches.Nodes.Clear();
            objSPdb = new StockPointDB();
            DataSet ds = new DataSet();
            try
            {
                ds = objSPdb.Get_UserBranchesWithStateFilter("", "", CommonData.LogUserId, "", "PARENT");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        tvBranches.Nodes.Add(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), ds.Tables[0].Rows[i]["COMPANY_NAME"].ToString());

                        DataSet dsState = new DataSet();

                        dsState = objSPdb.Get_UserBranchesWithStateFilter(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "", CommonData.LogUserId, "", "STATE");

                        if (dsState.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < dsState.Tables[0].Rows.Count; j++)
                            {
                                tvBranches.Nodes[i].Nodes.Add(dsState.Tables[0].Rows[j]["StateCode"].ToString(), dsState.Tables[0].Rows[j]["StateName"].ToString());

                                DataSet dschild = new DataSet();

                                // Filling Branches
                                if (sFrmType == "TOUR_EXPENSES")
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
                                    dschild = objSPdb.Get_UserBranchesWithStateFilter(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), dsState.Tables[0].Rows[j]["StateCode"].ToString(), CommonData.LogUserId, "HO", "CHILD");
                                    tvBranches.Nodes[i].Nodes[j].Nodes.Add("HO" + "(" + dschild.Tables[0].Rows.Count + ")");
                                    if (dschild.Tables[0].Rows.Count > 0)
                                    {
                                        for (int k = 0; k < dschild.Tables[0].Rows.Count; k++)
                                        {
                                            tvBranches.Nodes[i].Nodes[j].Nodes[1].Nodes.Add(dschild.Tables[0].Rows[k]["BranchCode"].ToString(), dschild.Tables[0].Rows[k]["BranchName"].ToString());
                                        }
                                    }

                                }

                            }

                        }
                    }
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.tvBranches.SelectedNode = tvBranches.Nodes[0];
                    this.tvBranches.SelectedNode.Expand();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }


        private void FillEmployeeList()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            clbEmployees.Items.Clear();
            GetSelectedValues();
            if (Company.Length > 0 && Branches.Length > 3)
            {
                try
                {
                    strCmd = "exec Get_ServiceEmployeeList '" + Company + "','" + Branches + "', '" + dtpFrmDocMonth.Value.ToString("MMMyyyy").ToUpper() +
                              "', '" + dtpToDocMonth.Value.ToString("MMMyyyy").ToUpper() + "'";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            NewCheckboxListItem oclBox = new NewCheckboxListItem();
                            oclBox.Tag = item["Ecode"].ToString();
                            oclBox.Text = item["EmpName"].ToString();
                            clbEmployees.Items.Add(oclBox);
                            oclBox = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

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


                strEcode = "";

                if (chkEmployees.Checked == true)
                {
                    strEcode = "ALL";
                }
                else
                {
                    for (int iVar = 0; iVar < clbEmployees.Items.Count; iVar++)
                    {
                        if (clbEmployees.GetItemCheckState(iVar) == CheckState.Checked)
                        {
                            strEcode += ((NewCheckboxListItem)clbEmployees.Items[iVar]).Tag + ',';
                        }
                    }
                    strEcode = strEcode.TrimEnd(',');
                }

            }

        }

        private void dtpFrmDocMonth_ValueChanged(object sender, EventArgs e)
        {
            FillEmployeeList();
        }

        private void dtpToDocMonth_ValueChanged(object sender, EventArgs e)
        {
            FillEmployeeList();
        }

        private bool CheckData()
        {
            bool flag = true;

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



            if (flag == false)
            {
                MessageBox.Show("Select Atleast One Branch", "SSCRM-Report", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return flag;
            }

            return flag;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {
                GetSelectedValues();
                if (strEcode.Length == 0)
                    strEcode = "ALL";

                if (sFrmType == "TOUR_EXPENSES")
                {
                    CommonData.ViewReport = "SSCRM_REP_TOUR_EXPENSES_DETL";
                    ReportViewer objReportview = new ReportViewer(Company, Branches, dtpFrmDocMonth.Value.ToString("MMMyyyy").ToUpper(), dtpToDocMonth.Value.ToString("MMMyyyy").ToUpper(), strEcode, "");
                    objReportview.Show();
                }
            }
        }

        private void chkEmployees_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEmployees.Checked == true)
            {
                for (int iVar = 0; iVar < clbEmployees.Items.Count; iVar++)
                {
                    clbEmployees.SetItemCheckState(iVar, CheckState.Checked);
                }
            }
            else
            {
                for (int iVar = 0; iVar < clbEmployees.Items.Count; iVar++)
                {
                    clbEmployees.SetItemCheckState(iVar, CheckState.Unchecked);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtDsearch_KeyUp(object sender, KeyEventArgs e)
        {
            SearchEcode(txtDsearch.Text.ToString(), clbEmployees);
        }

        private void SearchEcode(string searchString, ListBox cbEcodes)
        {
            if (searchString.Trim().Length > 0)
            {
                for (int i = 0; i < cbEcodes.Items.Count; i++)
                {
                    if (cbEcodes.Items[i].ToString() == "System.Data.DataRowView")  // for listbox search
                    {
                        if (((System.Data.DataRowView)(cbEcodes.Items[i])).Row.ItemArray[1].ToString().IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            cbEcodes.SetSelected(i, true);
                            break;
                        }
                        else
                            cbEcodes.SetSelected(i, false);

                    }
                    else  // for checkbox list search
                    {
                        if (cbEcodes.Items[i].ToString().IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            cbEcodes.SetSelected(i, true);
                            break;
                        }
                        else
                            cbEcodes.SetSelected(i, false);

                    }

                }
            }
        }

       

        private void tvBranches_AfterCheck(object sender, TreeViewEventArgs e)
        {
            GetSelectedValues();
            FillEmployeeList();
        }
      
    }
         
}
