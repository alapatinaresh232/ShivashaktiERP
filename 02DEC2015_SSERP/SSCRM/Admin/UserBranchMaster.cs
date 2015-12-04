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
    public partial class UserBranchMaster : Form
    {
        private InvoiceDB objInv = null;
        private Master objMaster = null;
        private SQLDB objDA = null;
        public UserBranchMaster()
        {
            InitializeComponent();
        }

        private void UserBranchMaster_Load(object sender, EventArgs e)
        {
            FillBranches();
            FillUsers();
        }

        private void FillBranches()
        {
            try
            {
                objInv = new InvoiceDB();
                DataSet ds = new DataSet();
                ds = objInv.AdminBranchCursor_Get("", "", "PARENT");
                TreeNode tNode;
                //tNode = tvProducts.Nodes.Add("Products");
                //tvBranches.Nodes[0].Nodes.Add("Single Product");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        treeView1.Nodes.Add(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), ds.Tables[0].Rows[i]["COMPANY_NAME"].ToString());
                        DataSet dschild = new DataSet();

                        dschild = objInv.AdminBranchCursor_Get(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "HO", "CHILD");
                        if (dschild.Tables[0].Rows.Count > 0)
                        {
                            treeView1.Nodes[i].Nodes.Add("CORPORATE OFFICE" + " (" + dschild.Tables[0].Rows.Count + ")");
                            for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                            {
                                treeView1.Nodes[i].Nodes[treeView1.Nodes[i].Nodes.Count - 1].Nodes.Add(dschild.Tables[0].Rows[j]["BRANCH_CODE"].ToString(), dschild.Tables[0].Rows[j]["BRANCH_NAME"].ToString());
                            }
                        }

                        dschild = objInv.AdminBranchCursor_Get(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "BR", "CHILD");                        
                        if (dschild.Tables[0].Rows.Count > 0)
                        {
                            treeView1.Nodes[i].Nodes.Add("BRANCHES" + " (" + dschild.Tables[0].Rows.Count + ")");
                            for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                            {
                                treeView1.Nodes[i].Nodes[treeView1.Nodes[i].Nodes.Count-1].Nodes.Add(dschild.Tables[0].Rows[j]["BRANCH_CODE"].ToString(), dschild.Tables[0].Rows[j]["BRANCH_NAME"].ToString());
                            }
                        }

                        dschild = objInv.AdminBranchCursor_Get(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "SP", "CHILD");
                        if (dschild.Tables[0].Rows.Count > 0)
                        {
                            treeView1.Nodes[i].Nodes.Add("STOCK POINTS" + " (" + dschild.Tables[0].Rows.Count + ")");
                            for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                            {
                                treeView1.Nodes[i].Nodes[treeView1.Nodes[i].Nodes.Count - 1].Nodes.Add(dschild.Tables[0].Rows[j]["BRANCH_CODE"].ToString(), dschild.Tables[0].Rows[j]["BRANCH_NAME"].ToString());
                            }
                        }

                        dschild = objInv.AdminBranchCursor_Get(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "OL", "CHILD");
                        if (dschild.Tables[0].Rows.Count > 0)
                        {
                            treeView1.Nodes[i].Nodes.Add("OUTLETS" + " (" + dschild.Tables[0].Rows.Count + ")");
                            for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                            {
                                treeView1.Nodes[i].Nodes[treeView1.Nodes[i].Nodes.Count - 1].Nodes.Add(dschild.Tables[0].Rows[j]["BRANCH_CODE"].ToString(), dschild.Tables[0].Rows[j]["BRANCH_NAME"].ToString());
                            }
                        }

                        dschild = objInv.AdminBranchCursor_Get(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "PU", "CHILD");
                        if (dschild.Tables[0].Rows.Count > 0)
                        {
                            treeView1.Nodes[i].Nodes.Add("PRODUCTION UNITS" + " (" + dschild.Tables[0].Rows.Count + ")");
                            for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                            {
                                treeView1.Nodes[i].Nodes[treeView1.Nodes[i].Nodes.Count - 1].Nodes.Add(dschild.Tables[0].Rows[j]["BRANCH_CODE"].ToString(), dschild.Tables[0].Rows[j]["BRANCH_NAME"].ToString());
                            }
                        }

                        dschild = objInv.AdminBranchCursor_Get(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "TR", "CHILD");
                        if (dschild.Tables[0].Rows.Count > 0)
                        {
                            treeView1.Nodes[i].Nodes.Add("TRANSPORT UNITS" + " (" + dschild.Tables[0].Rows.Count + ")");
                            for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                            {
                                treeView1.Nodes[i].Nodes[treeView1.Nodes[i].Nodes.Count - 1].Nodes.Add(dschild.Tables[0].Rows[j]["BRANCH_CODE"].ToString(), dschild.Tables[0].Rows[j]["BRANCH_NAME"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //this.tvBranches.SelectedNode = tNode.Nodes[1];
            //this.tvBranches.SelectedNode.Expand();
        }
        private void FillUsers()
        {
            objMaster = new Master();
            DataTable dt = null;
            try
            {

                dt = objMaster.UserMasterList_Get(CommonData.CompanyCode, CommonData.BranchCode).Tables[0];

                foreach (DataRow dataRow in dt.Rows)
                {
                    if (dataRow["UM_USER_ID"] + "" != "")
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = dataRow["UM_USER_ID"].ToString();
                        oclBox.Text = dataRow["UM_USER_NAME"].ToString() + " ( " + dataRow["UM_USER_ID"].ToString() + " )";
                        clbUsers.Items.Add(oclBox);
                        oclBox = null;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objMaster = null;
            }
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
        //private void tvBranches_AfterCheck(object sender, TreeViewEventArgs e)
        //{
        //    treeView1.BeginUpdate();

        //    foreach (TreeNode Node in e.Node.Nodes)
        //    {
        //        Node.Checked = e.Node.Checked;
        //    }

        //    treeView1.EndUpdate();
        //}

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtDsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            SearchEcode(txtDsearch.Text.ToString(), clbUsers);
        }

        private void txtDsearch_KeyUp(object sender, KeyEventArgs e)
        {
            SearchEcode(txtDsearch.Text.ToString(), clbUsers);
        }

        private void clbUsers_ItemCheck(object sender, ItemCheckEventArgs e)
        {
           
            for (int i = 0; i < clbUsers.Items.Count; i++)
            {
                if (e.Index != i)
                    clbUsers.SetItemCheckState(i, CheckState.Unchecked);
            }
            if (e.NewValue == CheckState.Checked)
            {
                GetMappedData();
            }
            else
            {
                lstMappedBranches.Items.Clear();
                for (int k = 0; k < treeView1.Nodes.Count; k++)
                {
                    for (int i = 0; i < treeView1.Nodes[k].Nodes.Count; i++)
                    {
                        for (int j = 0; j < treeView1.Nodes[k].Nodes[i].Nodes.Count; j++)
                        {


                            treeView1.Nodes[k].Nodes[i].Nodes[j].Checked = false;

                            //else
                            //    treeView1.Nodes[k].Nodes[i].Nodes[j].Checked = false;
                        }
                    }
                }
            }


            //treeView1.Nodes.Clear();
        }

        private void FillUserBranches(string sUserId)
        {
            objDA = new SQLDB();
            int rec = 0;
            string strSQL = string.Empty;
            DataTable dt = new DataTable();
            dt = null;
            strSQL = "SELECT UB_BRANCH_CODE FROM USER_BRANCH WHERE UB_USER_ID = '" + sUserId + "'";
            dt = objDA.ExecuteDataSet(strSQL).Tables[0];

            if (dt.Rows.Count > 0)
            {
                for (int k = 0; k < treeView1.Nodes.Count; k++)
                {
                    for (int i = 0; i < treeView1.Nodes[k].Nodes.Count; i++)
                    {
                        for (int j = 0; j < treeView1.Nodes[k].Nodes[i].Nodes.Count; j++)
                        {
                            

                                treeView1.Nodes[k].Nodes[i].Nodes[j].Checked = false;
                            
                            //else
                            //    treeView1.Nodes[k].Nodes[i].Nodes[j].Checked = false;
                        }
                    }
                }




                foreach (DataRow row in dt.Rows)
                {
                    for (int k = 0; k < treeView1.Nodes.Count; k++)
                    {
                        for (int i = 0; i < treeView1.Nodes[k].Nodes.Count; i++)
                        {
                            for (int j = 0; j < treeView1.Nodes[k].Nodes[i].Nodes.Count; j++)
                            {
                                if (treeView1.Nodes[k].Nodes[i].Nodes[j].Name.ToString() == row.ItemArray[0].ToString())
                                {

                                    treeView1.Nodes[k].Nodes[i].Nodes[j].Checked = true;
                                }
                                //else
                                //    treeView1.Nodes[k].Nodes[i].Nodes[j].Checked = false;
                            }
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objDA = new SQLDB();
            int rec = 0;
            string strSQL = string.Empty;
            if (CheckData())
            {
                try
                {
                    strSQL = "DELETE FROM USER_BRANCH WHERE UB_USER_ID = '" + ((SSAdmin.NewCheckboxListItem)(clbUsers.SelectedItem)).Tag.ToString() + "'";
                    rec = objDA.ExecuteSaveData(strSQL);
                    //if (rec > 0)
                    //{
                    strSQL = string.Empty;
                    for (int k = 0; k < treeView1.Nodes.Count; k++)
                        {
                            for (int i = 0; i < treeView1.Nodes[k].Nodes.Count; i++)
                            {
                                for (int j = 0; j < treeView1.Nodes[k].Nodes[i].Nodes.Count; j++)
                                {
                                    if (treeView1.Nodes[k].Nodes[i].Nodes[j].Checked == true)
                                    {
                                        strSQL += "INSERT INTO USER_BRANCH(UB_USER_ID,UB_BRANCH_CODE,UB_STATUS,UB_CREATED_BY,UB_CREATED_DATE)" +
                                            " VALUES('" + ((SSAdmin.NewCheckboxListItem)(clbUsers.SelectedItem)).Tag.ToString() + "','" + treeView1.Nodes[k].Nodes[i].Nodes[j].Name.ToString() + "'" +
                                            ",'R','" + CommonData.LogUserId + "',GETDATE()); ";
                                    }
                                }
                            }
                        }
                        rec = objDA.ExecuteSaveData(strSQL);
                        if (rec > 0)
                        {
                            MessageBox.Show("Data Saved Successfully", "SSCRM-Admin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Data Not Saved", "SSCRM-Admin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    //}
                    //else
                    //{
                      //  MessageBox.Show("Data Not Saved", "SSCRM-Admin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                GetMappedData();
            }
            
        }
        private bool CheckData()
        {
            bool blVil = false;
            for (int i = 0; i < clbUsers.Items.Count; i++)
            {
                if (clbUsers.GetItemCheckState(i) == CheckState.Checked)
                    blVil = true;
            }
            if (blVil == false)
            {
                MessageBox.Show("Select User", "SSCRM-Admin", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return blVil;
            }
            else
                blVil = false;
            for (int k = 0; k < treeView1.Nodes.Count; k++)
            {
                for (int i = 0; i < treeView1.Nodes[k].Nodes.Count; i++)
                {
                    for (int j = 0; j < treeView1.Nodes[k].Nodes[i].Nodes.Count; j++)
                    {
                        if (treeView1.Nodes[k].Nodes[i].Nodes[j].Checked == true)
                        {
                            blVil = true;
                        }
                    }
                }
            }
            if (blVil == false)
            {
                MessageBox.Show("Select Branches For User", "SSCRM-Admin", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return blVil;
            }
            return blVil;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtDsearch.Text = string.Empty;
            for (int i = 0; i < clbUsers.Items.Count; i++)
            {
                clbUsers.SetItemCheckState(i, CheckState.Unchecked);
            }
            for (int k = 0; k < treeView1.Nodes.Count; k++)
            {
                for (int i = 0; i < treeView1.Nodes[k].Nodes.Count; i++)
                {
                    for (int j = 0; j < treeView1.Nodes[k].Nodes[i].Nodes.Count; j++)
                    {
                        treeView1.Nodes[k].Nodes[i].Nodes[j].Checked = false;
                    }
                    treeView1.Nodes[k].Nodes[i].Checked = false;
                }
                treeView1.Nodes[k].Checked = false;
            }
        }

        public void GetMappedData()
        {
            string SqlQry = "SELECT BRANCH_NAME FROM USER_BRANCH INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE WHERE UB_USER_ID = '" + ((SSAdmin.NewCheckboxListItem)(clbUsers.SelectedItem)).Tag.ToString() + "';";
            objDA = new SQLDB();
            DataSet dsOld = null;
            dsOld = objDA.ExecuteDataSet(SqlQry);
            objDA = null;
            lstMappedBranches.Items.Clear();
            foreach (DataRow dr in dsOld.Tables[0].Rows)
            {
                lstMappedBranches.Items.Add(dr[0].ToString());
            }

            FillUserBranches(((SSAdmin.NewCheckboxListItem)(clbUsers.SelectedItem)).Tag.ToString());
        }
    }
}
