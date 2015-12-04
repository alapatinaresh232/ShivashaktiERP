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
    public partial class AboveBranchLevelUserBranches : Form
    {
        private InvoiceDB objInv = null;
        private HRInfo objHRdb = null;
        private SQLDB objDA = null;

        public AboveBranchLevelUserBranches()
        {
            InitializeComponent();
        }

        private void AboveBranchLevelUserBranches_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            FillBranches();
        }

        private void FillCompanyData()
        {
            objDA = new SQLDB();
            DataTable dt = new DataTable();

            try
            {
                string strCmd = "SELECT DISTINCT CM_COMPANY_NAME ,CM_COMPANY_CODE " +
                                   " FROM USER_BRANCH " +
                                   " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                   " INNER JOIN COMPANY_MAS ON CM_COMPANY_CODE = COMPANY_CODE " +
                                   " WHERE UB_USER_ID ='" + CommonData.LogUserId +
                                   "' ORDER BY CM_COMPANY_NAME";

                dt = objDA.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbCompany.DataSource = dt;
                    cbCompany.DisplayMember = "CM_COMPANY_NAME";
                    cbCompany.ValueMember = "CM_COMPANY_CODE";
                }

                //cbCompany.SelectedValue = CommonData.CompanyCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objDA = null;
                dt = null;
            }
        }


        private void FillBranchData()
        {
            objDA = new SQLDB();
            DataTable dt = new DataTable();
           
            try
            {
                if (cbCompany.SelectedIndex > 0)
                {
                    string strCommand = "SELECT DISTINCT BRANCH_NAME,BRANCH_CODE " +
                                        " FROM USER_BRANCH " +
                                        " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                        " WHERE COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() +
                                        "' AND UB_USER_ID ='" + CommonData.LogUserId +
                                        "' ORDER BY BRANCH_NAME ASC";
                    dt = objDA.ExecuteDataSet(strCommand).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbLocation.DataSource = dt;
                    cbLocation.DisplayMember = "BRANCH_NAME";
                    cbLocation.ValueMember = "BRANCH_CODE";

                }
                else
                {
                    cbLocation.DataSource = null;
                }


                //cbLocation.SelectedValue = CommonData.BranchCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objDA = null;
                dt = null;
            }
        }

        private void FillBranches()
        {
            tvBranches.Nodes.Clear();

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
                    tvBranches.Nodes.Add(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), ds.Tables[0].Rows[i]["COMPANY_NAME"].ToString());
                    DataSet dschild = new DataSet();
                    dschild = objInv.AdminBranchCursor_Get(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "BR", "CHILD");
                    tvBranches.Nodes[i].Nodes.Add("BRANCHES" + "(" + dschild.Tables[0].Rows.Count + ")");
                    if (dschild.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                        {
                            tvBranches.Nodes[i].Nodes[0].Nodes.Add(dschild.Tables[0].Rows[j]["BRANCH_CODE"].ToString(), dschild.Tables[0].Rows[j]["BRANCH_NAME"].ToString());
                        }
                    }

                    dschild = objInv.AdminBranchCursor_Get(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "SP", "CHILD");
                    tvBranches.Nodes[i].Nodes.Add("STOCK POINTS" + "(" + dschild.Tables[0].Rows.Count + ")");
                    if (dschild.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                        {
                            tvBranches.Nodes[i].Nodes[1].Nodes.Add(dschild.Tables[0].Rows[j]["BRANCH_CODE"].ToString(), dschild.Tables[0].Rows[j]["BRANCH_NAME"].ToString());
                        }
                    }

                    dschild = objInv.AdminBranchCursor_Get(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "PU", "CHILD");
                    tvBranches.Nodes[i].Nodes.Add("PRODUCTION UNITS" + "(" + dschild.Tables[0].Rows.Count + ")");
                    if (dschild.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                        {
                            tvBranches.Nodes[i].Nodes[2].Nodes.Add(dschild.Tables[0].Rows[j]["BRANCH_CODE"].ToString(), dschild.Tables[0].Rows[j]["BRANCH_NAME"].ToString());
                        }
                    }

                    dschild = objInv.AdminBranchCursor_Get(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "TR", "CHILD");
                    tvBranches.Nodes[i].Nodes.Add("TRANSPORT UNITS" + "(" + dschild.Tables[0].Rows.Count + ")");
                    if (dschild.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                        {
                            tvBranches.Nodes[i].Nodes[3].Nodes.Add(dschild.Tables[0].Rows[j]["BRANCH_CODE"].ToString(), dschild.Tables[0].Rows[j]["BRANCH_NAME"].ToString());
                        }
                    }
                }
            }

            //this.tvBranches.SelectedNode = tNode.Nodes[1];
            //this.tvBranches.SelectedNode.Expand();
        }

        private void FillUsers()
        {
            objHRdb = new HRInfo();
            DataTable dt = new DataTable();
            clbUsers.Items.Clear();

            if (cbCompany.SelectedIndex > 0 && cbLocation.SelectedIndex > 0)
            {
                try
                {
                    dt = objHRdb.BranchUsersList_Get(cbCompany.SelectedValue.ToString(), cbLocation.SelectedValue.ToString()).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
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
                    else
                    {
                        clbUsers.Items.Clear();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    objHRdb = null;
                }
            }
            else
            {
                clbUsers.Items.Clear();
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




        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                FillBranchData();
            }
            else
            {
                cbLocation.DataSource = null;               
            }
            clbUsers.Items.Clear();
            FillBranches();
        }

        private void cbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLocation.SelectedIndex > 0)
            {
                FillUsers();
                FillBranches();
            }
        }

        private void txtDsearch_KeyUp(object sender, KeyEventArgs e)
        {
            SearchEcode(txtDsearch.Text.ToString(), clbUsers);
        }

        private void txtDsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            SearchEcode(txtDsearch.Text.ToString(), clbUsers);
        }

        public void GetMappedData()
        {
            if (clbUsers.Items.Count > 0)
            {

                FillUserBranches(((SSAdmin.NewCheckboxListItem)(clbUsers.SelectedItem)).Tag.ToString());
            }
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
                
                foreach (DataRow row in dt.Rows)
                {
                    for (int k = 0; k < tvBranches.Nodes.Count; k++)
                    {
                        for (int i = 0; i < tvBranches.Nodes[k].Nodes.Count; i++)
                        {
                            for (int j = 0; j < tvBranches.Nodes[k].Nodes[i].Nodes.Count; j++)
                            {
                                if (tvBranches.Nodes[k].Nodes[i].Nodes[j].Name.ToString() == row.ItemArray[0].ToString())
                                {

                                    tvBranches.Nodes[k].Nodes[i].Nodes[j].Checked = true;
                                }
                               
                            }
                        }
                    }
                }
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            //cbCompany.SelectedIndex = 0;
            //cbLocation.SelectedIndex = -1;

            txtDsearch.Text = string.Empty;
            for (int i = 0; i < clbUsers.Items.Count; i++)
            {
                clbUsers.SetItemCheckState(i, CheckState.Unchecked);
            }
            for (int k = 0; k < tvBranches.Nodes.Count; k++)
            {
                for (int i = 0; i < tvBranches.Nodes[k].Nodes.Count; i++)
                {
                    for (int j = 0; j < tvBranches.Nodes[k].Nodes[i].Nodes.Count; j++)
                    {
                        tvBranches.Nodes[k].Nodes[i].Nodes[j].Checked = false;
                    }
                    tvBranches.Nodes[k].Nodes[i].Checked = false;
                }
                tvBranches.Nodes[k].Checked = false;
            }
        }

        private bool CheckData()
        {
            bool blVil = false;

            if (cbCompany.SelectedIndex == 0)
            {
                blVil = false;
                MessageBox.Show("Please Select Company", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbCompany.Focus();
                return blVil;
            }

            if (cbLocation.SelectedIndex == 0 || cbLocation.SelectedIndex == -1)
            {
                blVil = false;
                MessageBox.Show("Please Select Location", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbLocation.Focus();
                return blVil;
            }

            for (int i = 0; i < clbUsers.Items.Count; i++)
            {
                if (clbUsers.GetItemCheckState(i) == CheckState.Checked)
                    blVil = true;
            }
            if (blVil == false)
            {
                MessageBox.Show("Select User", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return blVil;
            }
            else
                blVil = false;
            for (int k = 0; k < tvBranches.Nodes.Count; k++)
            {
                for (int i = 0; i < tvBranches.Nodes[k].Nodes.Count; i++)
                {
                    for (int j = 0; j < tvBranches.Nodes[k].Nodes[i].Nodes.Count; j++)
                    {
                        if (tvBranches.Nodes[k].Nodes[i].Nodes[j].Checked == true)
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
                  
                    strSQL = string.Empty;
                    for (int k = 0; k < tvBranches.Nodes.Count; k++)
                    {
                        for (int i = 0; i < tvBranches.Nodes[k].Nodes.Count; i++)
                        {
                            for (int j = 0; j < tvBranches.Nodes[k].Nodes[i].Nodes.Count; j++)
                            {
                                if (tvBranches.Nodes[k].Nodes[i].Nodes[j].Checked == true)
                                {
                                    strSQL += "INSERT INTO USER_BRANCH(UB_USER_ID,UB_BRANCH_CODE,UB_STATUS,UB_CREATED_BY,UB_CREATED_DATE)" +
                                        " VALUES('" + ((SSAdmin.NewCheckboxListItem)(clbUsers.SelectedItem)).Tag.ToString() + "','" + tvBranches.Nodes[k].Nodes[i].Nodes[j].Name.ToString() + "'" +
                                        ",'R','" + CommonData.LogUserId + "',GETDATE()); ";
                                }
                            }
                        }
                    }

                    if (strSQL.Length > 5)
                    {
                        rec = objDA.ExecuteSaveData(strSQL);
                    }

                    if (rec > 0)
                    {
                        MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                   

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                GetMappedData();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

       


    }
}
