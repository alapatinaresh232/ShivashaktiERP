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

namespace SSCRM
{
    public partial class FixedAssetsPrint : Form
    {
        SQLDB objSQLdb = null;
        InvoiceDB objInv = null;        
        string strAssets = "";
        string sRepType = "";
        private string Company = string.Empty;
        private string Branches = string.Empty;
        private int iFormType = 0;

        public FixedAssetsPrint()
        {
            InitializeComponent();
        }
        public FixedAssetsPrint(int iForm)
        {
            iFormType = iForm;
            InitializeComponent();
        }

        private void FixedAssetsPrint_Load(object sender, EventArgs e)
        {
            FillAssets();
            FillBranches();
           
            if (iFormType == 1)
            {
                chkAllAssets.Visible = false;
                cbRepType.SelectedIndex = 1;
                cbRepType.Enabled = false;
                this.Text = "Fixed Assets :: Detailed Report";
            }
            if (iFormType == 2)
            {
                chkAllAssets.Visible = false;
                cbRepType.SelectedIndex = 2;
                cbRepType.Enabled = false;
                this.Text = "Fixed Assets :: Summary Report";
            }
            if (iFormType == 3)
            {
                chkAllAssets.Visible = true;
                cbRepType.SelectedIndex = 1;
                cbRepType.Enabled = false;
                this.Text = "Fixed Assets :: Details";
            }
            //cbRepType.SelectedIndex = 0;

        }


        private void FillAssets()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
           
            try
            {
                string strCommand = "SELECT DISTINCT(FAH_ASSET_TYPE) FROM FIXED_ASSETS_HEAD ORDER BY FAH_ASSET_TYPE ASC;";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {                   
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = dataRow["FAH_ASSET_TYPE"].ToString();
                        oclBox.Text = dataRow["FAH_ASSET_TYPE"].ToString();
                        clbAssets.Items.Add(oclBox);
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

       

        private void FillBranches()
        {
            objInv = new InvoiceDB();
            DataSet ds = new DataSet();
            ds = objInv.UserBranchCursor_Get("", "", "PARENT");           
           
            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {                  
                    tvBranches.Nodes.Add(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), ds.Tables[0].Rows[i]["COMPANY_NAME"].ToString());

                    DataSet dschild = new DataSet();
                    dschild = objInv.UserBranchCursor_Get(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "BR", "CHILD");
                    
                    if (dschild.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                        {
                            
                            tvBranches.Nodes[i].Nodes.Add(dschild.Tables[0].Rows[j]["BRANCH_CODE"].ToString(), dschild.Tables[0].Rows[j]["BRANCH_NAME"].ToString());
                        }
                    }

                    dschild = objInv.UserBranchCursor_Get(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "SP", "CHILD");
                    
                    if (dschild.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                        {
                            
                            tvBranches.Nodes[i].Nodes.Add(dschild.Tables[0].Rows[j]["BRANCH_CODE"].ToString(), dschild.Tables[0].Rows[j]["BRANCH_NAME"].ToString());
                        }
                    }

                    dschild = objInv.UserBranchCursor_Get(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "PU", "CHILD");
                    
                    if (dschild.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                        {
                            tvBranches.Nodes[i].Nodes.Add(dschild.Tables[0].Rows[j]["BRANCH_CODE"].ToString(), dschild.Tables[0].Rows[j]["BRANCH_NAME"].ToString());
                        }
                    }

                    dschild = objInv.UserBranchCursor_Get(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "TR", "CHILD");
                    
                    if (dschild.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                        {
                            tvBranches.Nodes[i].Nodes.Add(dschild.Tables[0].Rows[j]["BRANCH_CODE"].ToString(), dschild.Tables[0].Rows[j]["BRANCH_NAME"].ToString());
                        }
                    }
                }
            }
          
               
        }
     

        private bool CheckData()
        {
            bool bFlag = true;
            if (cbRepType.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Report Type","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
                cbRepType.Focus();
                return bFlag;
            }
            if (clbAssets.CheckedItems.Count == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Atleast One AssetType", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return bFlag;
            }
                       
                    
           
            return bFlag;
        }


        private bool CheckBranches()
        {
            bool flag = false;

            for (int k = 0; k < tvBranches.Nodes.Count; k++)
            {

                for (int j = 0; j < tvBranches.Nodes[k].Nodes.Count; j++)
                {
                    if (tvBranches.Nodes[k].Nodes[j].Checked == true)
                    {
                        flag = true;
                    }
                }

            }
            if (flag == false)
            {
                MessageBox.Show("Please Select Atleast One Branch", "SSCRM-Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }


            return flag;
        }

        private void GetSelectedItems()
        {
            strAssets = "";
            if (CheckData())
            {
                for (int i = 0; i < clbAssets.Items.Count; i++)
                {
                    if (clbAssets.GetItemCheckState(i) == CheckState.Checked)
                    {
                        strAssets += ((SSAdmin.NewCheckboxListItem)(clbAssets.Items[i])).Tag.ToString() + ",";                        
                    }
                }                                   
                            
            }
        }


        private void GetSelectedCompAndBranches()
        {
            Company = "";
            Branches = "";


            bool iscomp = false;
            for (int i = 0; i < tvBranches.Nodes.Count; i++)
            {
                for (int j = 0; j < tvBranches.Nodes[i].Nodes.Count; j++)
                {
                    if (tvBranches.Nodes[i].Nodes[j].Checked == true)
                    {
                        if (Branches != string.Empty)
                            Branches += ",";
                        Branches += tvBranches.Nodes[i].Nodes[j].Name.ToString();
                        iscomp = true;
                    }
                }
                if (iscomp == true)
                {
                    if (Company != string.Empty)
                        Company += ",";
                    Company += tvBranches.Nodes[i].Name.ToString();
                }
                iscomp = false;
            }


        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

       
      
        private void btnReport_Click(object sender, EventArgs e)
        {
            if (CheckData()==true)
            {
                if (CheckBranches() == true)
                {

                    GetSelectedItems();
                    GetSelectedCompAndBranches();
                    Branches = Branches.TrimEnd(',');
                    strAssets = strAssets.TrimEnd(',');                  

               
                    if (iFormType == 1)
                    {
                        CommonData.ViewReport = "REP_FIXED_ASSETS_DETAILS";
                        ReportViewer objReportview = new ReportViewer("", Branches, strAssets, "DETAILED");
                        objReportview.Show();
                    }
                    if (iFormType == 2)
                    {
                        CommonData.ViewReport = "REP_FIXED_ASSETS_DETAILS_SUMMARY";
                        ReportViewer objReportview = new ReportViewer("", Branches, strAssets, "SUMMARY");
                        objReportview.Show();
                    }
                    if (iFormType == 3)
                    {
                        CommonData.ViewReport = "FIXED_ASSETS_DETAILS";
                        ReportViewer objReportview = new ReportViewer("", Branches, strAssets, "DETAILED");
                        objReportview.Show();
                    }
                }
            }
           
           
       
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbAssets.Items.Count; i++)
            {
                clbAssets.SetItemCheckState(i,CheckState.Unchecked);
            }
          
            tvBranches.Nodes.Clear();
            FillBranches();
            txtDsearch.Text = "";
            chkAllAssets.Checked = false;

        }

       

        private void clbAssets_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (iFormType != 3)
            {
                for (int i = 0; i < clbAssets.Items.Count; i++)
                {
                    if (e.Index != i)
                        clbAssets.SetItemCheckState(i, CheckState.Unchecked);
                }
            }

            
        }

        private void tvBranches_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TriStateTreeView.getStatus(e);
           
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

        private void tvBranches_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (iFormType == 3)
            {
                UnCheckItems();
                
                if (tvBranches.Nodes.Count > 0)
                {                     
 
                    for (int i = 0; i < tvBranches.Nodes.Count; i++)
                    {
                        tvBranches.Nodes[i].StateImageIndex = 0;

                        for (int j = 0; j < tvBranches.Nodes[i].Nodes.Count; j++)
                        {
                            if (tvBranches.Nodes[i].Nodes[j].Checked == true)
                            {
                                if (j != e.Node.Index)
                                {                                   
                                    tvBranches.Nodes[i].Checked = false;
                                    tvBranches.Nodes[i].Nodes[j].Checked = false;
                                }
                            }

                        }

                    }
                }

            }

        }


        private void UnCheckItems()
        {
            foreach (TreeNode node in tvBranches.Nodes)
            {
                node.Checked = true;              
                UnCheckChildren(node, false);              
                                            
            }
        }

        private void UnCheckChildren(TreeNode rootNode, bool isChecked)
        {
            foreach (TreeNode node in rootNode.Nodes)
            {
                UnCheckChildren(node, isChecked);
                node.Checked = isChecked;
            }
        }

        private void txtDsearch_KeyUp(object sender, KeyEventArgs e)
        {
            SearchEcode(txtDsearch.Text.ToString(), clbAssets);
        }

        private void chkAllAssets_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllAssets.Checked == true)
            {
                for (int iVar = 0; iVar < clbAssets.Items.Count; iVar++)
                {
                    clbAssets.SetItemCheckState(iVar, CheckState.Checked);
                }

            }
            else
            {
                for (int iVar = 0; iVar < clbAssets.Items.Count; iVar++)
                {
                    clbAssets.SetItemCheckState(iVar, CheckState.Unchecked);
                }
            }
        }
          
      

    }
}
