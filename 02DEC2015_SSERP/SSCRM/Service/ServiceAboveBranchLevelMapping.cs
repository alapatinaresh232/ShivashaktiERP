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
using SSAdmin;

namespace SSCRM
{
    public partial class ServiceAboveBranchLevelMapping : Form
    {
        SQLDB objDB = null;

        public ServiceAboveBranchLevelMapping()
        {
            InitializeComponent();
        }

        private void ServiceAboveBranchLevelMapping_Load(object sender, EventArgs e)
        {
            txtDocMonth.Text = CommonData.DocMonth;
            FillLeadersList();
            FillGroupsList();
        }

        private void FillGroupsList()
        {
            objDB = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                tstvGroups.Nodes.Clear();
                tstvGroups.Nodes.Add("Groups");

                dt = objDB.ExecuteDataSet("exec Get_ServiceGroupsListForAboveMapping '" + CommonData.DocMonth + "'").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    int iComp = 0, iBranch = 0, iGroup = 0;
                    TreeNode tNode = new TreeNode();
                    tNode.Text = dt.Rows[0]["rs_comp_name"].ToString() + "-(" + dt.Rows[0]["rs_comp_groups"].ToString() + ")";
                    tNode.Tag = dt.Rows[0]["rs_comp_code"].ToString();
                    tstvGroups.Nodes[0].Nodes.Add(tNode);
                    tNode = null;
                    tNode = new TreeNode();
                    tNode.Text = dt.Rows[0]["rs_branch_name"].ToString() + "-(" + dt.Rows[0]["rs_groups"].ToString() + ")";
                    tNode.Tag = dt.Rows[0]["rs_branch_code"].ToString();
                    tstvGroups.Nodes[0].Nodes[0].Nodes.Add(tNode);
                    tNode = null;
                    if (dt.Rows[0]["rs_Emp_Ecode"].ToString() != "")
                    {
                        tNode = new TreeNode();
                        tNode.Text = dt.Rows[0]["rs_Emp_Name"].ToString();
                        tNode.Tag = dt.Rows[0]["rs_Emp_Ecode"].ToString();
                        tstvGroups.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(tNode);
                        tNode = null;
                    }


                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        TreeNode tNode1 = null;
                        if (dt.Rows[i]["rs_comp_code"].ToString() == dt.Rows[i - 1]["rs_comp_code"].ToString())
                        {
                            if (dt.Rows[i]["rs_branch_code"].ToString() == dt.Rows[i - 1]["rs_branch_code"].ToString())
                            {
                                iGroup++;
                                if (dt.Rows[i]["rs_Emp_Ecode"].ToString() != "")
                                {
                                    tNode1 = new TreeNode();
                                    tNode1.Text = dt.Rows[i]["rs_Emp_Name"].ToString();
                                    tNode1.Tag = dt.Rows[i]["rs_Emp_Ecode"].ToString();
                                    tstvGroups.Nodes[0].Nodes[iComp].Nodes[iBranch].Nodes.Add(tNode1);
                                    tNode1 = null;
                                }
                            }
                            else
                            {
                                iGroup = 0;
                                iBranch++;
                                tNode1 = new TreeNode();
                                tNode1.Text = dt.Rows[i]["rs_branch_name"].ToString() + "-(" + dt.Rows[i]["rs_groups"].ToString() + ")";
                                tNode1.Tag = dt.Rows[i]["rs_branch_code"].ToString();
                                tstvGroups.Nodes[0].Nodes[iComp].Nodes.Add(tNode1);
                                tNode1 = null;
                                if (dt.Rows[i]["rs_Emp_Ecode"].ToString() != "")
                                {
                                    tNode1 = new TreeNode();
                                    tNode1.Text = dt.Rows[i]["rs_Emp_Name"].ToString();
                                    tNode1.Tag = dt.Rows[i]["rs_Emp_Ecode"].ToString();
                                    tstvGroups.Nodes[0].Nodes[iComp].Nodes[iBranch].Nodes.Add(tNode1);
                                    tNode1 = null;
                                }
                            }
                        }
                        else
                        {
                            iComp++;
                            iGroup = iBranch = 0;
                            tNode1 = new TreeNode();
                            tNode1.Text = dt.Rows[i]["rs_comp_name"].ToString() + "-(" + dt.Rows[i]["rs_comp_groups"].ToString() + ")";
                            tNode1.Tag = dt.Rows[i]["rs_comp_code"].ToString();
                            tstvGroups.Nodes[0].Nodes.Add(tNode1);
                            tNode1 = null;
                            tNode1 = new TreeNode();
                            tNode1.Text = dt.Rows[i]["rs_branch_name"].ToString() + "-(" + dt.Rows[i]["rs_groups"].ToString() + ")";
                            tNode1.Tag = dt.Rows[i]["rs_branch_code"].ToString();
                            tstvGroups.Nodes[0].Nodes[iComp].Nodes.Add(tNode1);
                            tNode1 = null;
                            if (dt.Rows[i]["rs_Emp_Ecode"].ToString() != "")
                            {
                                tNode1 = new TreeNode();
                                tNode1.Text = dt.Rows[i]["rs_Emp_Name"].ToString();
                                tNode1.Tag = dt.Rows[i]["rs_Emp_Ecode"].ToString();
                                tstvGroups.Nodes[0].Nodes[iComp].Nodes[iBranch].Nodes.Add(tNode1);
                                tNode1 = null;
                            }

                        }
                    }
                }
                tstvGroups.Nodes[0].Expand();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                dt = null;
            }
        }

        private void FillLeadersList()
        {
            objDB = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
            clbLeaders.Items.Clear();
            try
            {
                strCommand = "SELECT slbl_fin_year,slbl_doc_month,slbl_state_code,slbl_leader_ecode," +
                        "CAST(slbl_leader_ecode as varchar)+'-'+MEMBER_NAME+'('+DESIG+')' LeaderName,COUNT(*) Groups" +
                        " FROM ServiceLevelBranchAbove_Leaders INNER JOIN EORA_MASTER ON ECODE = slbl_leader_ecode" +
                        " LEFT JOIN ServiceLevelGroup_map_Detl ON lgmd_doc_month = slbl_doc_month" +
                        " AND lgmd_dest_ecode = slbl_leader_ecode WHERE slbl_doc_month = '" + CommonData.DocMonth + "'" +
                        " GROUP BY slbl_fin_year,slbl_doc_month,slbl_state_code," +
                        "slbl_leader_ecode,MEMBER_NAME,DESIG ORDER BY Groups DESC";

                dt = objDB.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = dataRow["slbl_leader_ecode"].ToString();
                        oclBox.Text = dataRow["LeaderName"].ToString() + "-(" + dataRow["Groups"].ToString() + ")";
                        clbLeaders.Items.Add(oclBox);
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
                objDB = null;
                dt = null;
            }
        }

        private void clbLeaders_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int i = 0; i < clbLeaders.Items.Count; i++)
            {
                if (e.Index != i)
                    clbLeaders.SetItemCheckState(i, CheckState.Unchecked);
            }
            if (e.NewValue == CheckState.Checked)
                GetMappedGroups();
            else
                FillGroupsList();
        }

        private void GetMappedGroups()
        {
            objDB = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
            FillGroupsList();
            try
            {
                strCommand = "SELECT distinct lgmd_group_ecode FROM ServiceLevelGroup_map_Detl"+
                                        " WHERE lgmd_doc_month = '" + CommonData.DocMonth +
                                        "' and lgmd_dest_ecode = " + ((NewCheckboxListItem)(clbLeaders.SelectedItem)).Tag.ToString();

                dt = objDB.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        for (int i = 0; i < tstvGroups.Nodes[0].Nodes.Count; i++)
                        {
                            for (int j = 0; j < tstvGroups.Nodes[0].Nodes[i].Nodes.Count; j++)
                            {
                                for (int k = 0; k < tstvGroups.Nodes[0].Nodes[i].Nodes[j].Nodes.Count; k++)
                                {
                                    if (tstvGroups.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Tag.ToString() == dr["lgmd_group_ecode"].ToString())
                                    {
                                        tstvGroups.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Checked = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objDB = null;
                dt = null;
            }
        }

        private bool CheckData()
        {
            bool flag = true;
            if (clbLeaders.CheckedItems.Count == 0)
            {
                flag = false;
                MessageBox.Show("Select Atleast one leader", "SSERP-AboveBranchMapping", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objDB = new SQLDB();
            string sGroups = "", sqlText = "";
            int iRes = 0;

            if (CheckData())
            {
                try
                {
                    for (int i = 0; i < tstvGroups.Nodes[0].Nodes.Count; i++)
                    {
                        for (int j = 0; j < tstvGroups.Nodes[0].Nodes[i].Nodes.Count; j++)
                        {
                            for (int k = 0; k < tstvGroups.Nodes[0].Nodes[i].Nodes[j].Nodes.Count; k++)
                            {
                                if (tstvGroups.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Checked == true)
                                {
                                    sGroups += "'" + tstvGroups.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Tag.ToString() + "'";
                                    sGroups += ",";
                                }
                            }
                            
                        }
                    }
                    sGroups = sGroups.TrimEnd(',');

                    sqlText = "DELETE FROM ServiceLevelGroup_map_Detl WHERE lgmd_doc_month = '" + CommonData.DocMonth +
                            "' AND lgmd_dest_ecode=" + ((NewCheckboxListItem)(clbLeaders.SelectedItem)).Tag.ToString() + " AND lgmd_map_type!='BR'";
                    objDB.ExecuteSaveData(sqlText);

                    sqlText = "";

                    if (sGroups.Length > 5)
                    {
                        sqlText = "INSERT INTO ServiceLevelGroup_map_Detl(lgmd_company_code,lgmd_branch_code,lgmd_log_branch_code,lgmd_state_code" +
                                ",lgmd_doc_month,lgmd_group_ecode,lgmd_dest_ecode,lgmd_user_id,lgmd_created_date,lgmd_map_type)" +
                                " SELECT DISTINCT lgm_company_code,lgm_branch_code,lgm_log_branch_code,lgm_state_code,lgm_doc_month," +
                                "lgm_group_ecode,'" + ((NewCheckboxListItem)(clbLeaders.SelectedItem)).Tag.ToString() +
                                "','" + CommonData.LogUserId + "',getdate(),'AB' FROM ServiceLevelGroup_map WHERE lgm_doc_month = '" + CommonData.DocMonth +
                                "' AND lgm_group_ecode IN (" + sGroups + ") AND lgm_group_ecode NOT IN (SELECT lgmd_group_ecode FROM ServiceLevelGroup_map_Detl " +
                                "where lgmd_doc_month = '" + CommonData.DocMonth + "' and lgmd_dest_ecode = " + ((NewCheckboxListItem)(clbLeaders.SelectedItem)).Tag.ToString() +
                                " AND lgmd_map_type = 'BR')";

                    }

                    if (sqlText.Length > 10)
                    {
                        iRes = objDB.ExecuteSaveData(sqlText);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (iRes > 0)
                {
                    MessageBox.Show("Data Saved Successfully", "SSERP-AboveBranchMapping", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GetMappedGroups();
                    FillLeadersList();
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SSERP-AboveBranchMapping", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbLeaders.Items.Count; i++)
            {
                clbLeaders.SetItemCheckState(i, CheckState.Unchecked);
            }
        }

    
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            objDB = new SQLDB();
            int iRes = 0;
            string sqlText = "";
            DataTable dt = new DataTable();
            if (txtEmpName.Text != "")
            {
                try
                {
                    sqlText = "SELECT * FROM ServiceLevelBranchAbove_Leaders " +
                                " WHERE slbl_doc_month = '" + CommonData.DocMonth +
                                "' AND slbl_leader_ecode =" + txtDsearch.Text;

                    dt = objDB.ExecuteDataSet(sqlText).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show(txtDsearch.Text + " Allready Added for document month " + CommonData.DocMonth, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        sqlText = "INSERT INTO ServiceLevelBranchAbove_Leaders(slbl_fin_year,slbl_doc_month,slbl_leader_ecode,slbl_state_code) " +
                                "VALUES('" + CommonData.FinancialYear + "','" + CommonData.DocMonth + "','" + txtDsearch.Text + "','');";
                        iRes = objDB.ExecuteSaveData(sqlText);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objDB = null;
                    dt = null;
                }
                if (iRes > 0)
                {
                    MessageBox.Show(txtDsearch.Text + " Added Successfully for month " + CommonData.DocMonth, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillLeadersList();
                    txtDsearch.Text = "";
                    txtEmpName.Text = "";
                }
                else
                {
                    MessageBox.Show(txtDsearch.Text + " not added for month " + CommonData.DocMonth, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please Enter Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void GetEmpName()
        {
            objDB = new SQLDB();
            string strCmd = "";
            DataTable dt = new DataTable();
            try
            {
                strCmd ="SELECT MEMBER_NAME from EORA_MASTER WHERE ECODE = " + txtDsearch.Text ;
                dt = objDB.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    txtEmpName.Text = dt.Rows[0]["MEMBER_NAME"].ToString();
                }
                else
                {
                    txtEmpName.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                txtEmpName.Text = "";
            }
            finally
            {
                objDB = null;
                dt = null;
            }
        }

        private void txtDsearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtDsearch.Text.Length > 4)
            {
                GetEmpName();
            }
            else
            {
                txtEmpName.Text = "";
            }
             
            SearchEcode(txtDsearch.Text.ToString(), clbLeaders);
        }

        private void btnDeleteLeader_Click(object sender, EventArgs e)
        {
            objDB = new SQLDB();
            int iRes = 0;
            string sqlText = "";
            DataTable dt = new DataTable();

            if (txtEmpName.Text != "")
            {
                try
                {
                    sqlText = "select * from ServiceLevelBranchAbove_Leaders "+
                               " where slbl_doc_month = '" + CommonData.DocMonth + "' and slbl_leader_ecode =" + txtDsearch.Text;
                    dt = objDB.ExecuteDataSet(sqlText).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        sqlText = "DELETE FROM ServiceLevelGroup_map_Detl where lgmd_dest_ecode = " + txtDsearch.Text + 
                                    " and lgmd_doc_month = '" + CommonData.DocMonth + "' and lgmd_map_type = 'AB'";
                        sqlText += " DELETE FROM ServiceLevelBranchAbove_Leaders where slbl_doc_month='" + CommonData.DocMonth +
                                    "' and slbl_leader_ecode=" + txtDsearch.Text;

                        iRes = objDB.ExecuteSaveData(sqlText);
                    }
                    else
                    {
                        MessageBox.Show(txtDsearch.Text + " not in document month " + CommonData.DocMonth, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objDB = null;
                    dt = null;
                }
                if (iRes > 0)
                {
                    MessageBox.Show(txtDsearch.Text + " deleted Successfully for month " + CommonData.DocMonth, "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillLeadersList();
                    txtDsearch.Text = "";
                    txtEmpName.Text = "";
                }
                else
                {
                    MessageBox.Show(txtDsearch.Text + " not deleted for month " + CommonData.DocMonth, "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please Enter Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

    }
}
