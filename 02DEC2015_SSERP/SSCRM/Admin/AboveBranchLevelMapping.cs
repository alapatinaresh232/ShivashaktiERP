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
namespace SSCRM
{
    public partial class AboveBranchLevelMapping : Form
    {
        SQLDB objDB = null;
        public AboveBranchLevelMapping()
        {
            InitializeComponent();
        }

        private void AboveBranchLevelMapping_Load(object sender, EventArgs e)
        {
            lblDocMonth.Text = CommonData.DocMonth;
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
                dt = objDB.ExecuteDataSet("exec GetGroupsListForAboveMapping '" + CommonData.DocMonth + "'").Tables[0];
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
                    if (dt.Rows[0]["rs_gl_ecode"].ToString() != "")
                    {
                        tNode = new TreeNode();
                        tNode.Text = dt.Rows[0]["rs_gl_name"].ToString();
                        tNode.Tag = dt.Rows[0]["rs_gl_ecode"].ToString();
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
                                if (dt.Rows[i]["rs_gl_ecode"].ToString() != "")
                                {
                                    tNode1 = new TreeNode();
                                    tNode1.Text = dt.Rows[i]["rs_gl_name"].ToString();
                                    tNode1.Tag = dt.Rows[i]["rs_gl_ecode"].ToString();
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
                                if (dt.Rows[i]["rs_gl_ecode"].ToString() != "")
                                {
                                    tNode1 = new TreeNode();
                                    tNode1.Text = dt.Rows[i]["rs_gl_name"].ToString();
                                    tNode1.Tag = dt.Rows[i]["rs_gl_ecode"].ToString();
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
                            if (dt.Rows[i]["rs_gl_ecode"].ToString() != "")
                            {
                                tNode1 = new TreeNode();
                                tNode1.Text = dt.Rows[i]["rs_gl_name"].ToString();
                                tNode1.Tag = dt.Rows[i]["rs_gl_ecode"].ToString();
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
            clbLeaders.Items.Clear();
            try
            {
                dt = objDB.ExecuteDataSet("SELECT lbl_fin_year,lbl_doc_month,lbl_state_code,lbl_leader_ecode," +
                        "CAST(lbl_leader_ecode as varchar)+'--'+MEMBER_NAME+'--('+ISNULL(ldm_designations,'')+')' LeaderName,COUNT(*) Groups,ldm_elevel_id" +
                        " FROM LevelBranchAbove_Leaders INNER JOIN EORA_MASTER ON ECODE = lbl_leader_ecode" +
                        " LEFT JOIN LevelGroup_map_Detl ON lgmd_doc_month = lbl_doc_month AND lgmd_dest_ecode = lbl_leader_ecode" +
                        " LEFT JOIN LevelsDesig_mas ON ldm_company_code = company_code AND LDM_DESIG_ID = DESG_ID" +
                        " WHERE lbl_doc_month = '" + CommonData.DocMonth + "'" +
                        " GROUP BY lbl_fin_year,lbl_doc_month,lbl_state_code," +
                        "lbl_leader_ecode,MEMBER_NAME,ldm_designations,ldm_elevel_id ORDER BY ldm_elevel_id ASC,Groups DESC").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = dataRow["lbl_leader_ecode"].ToString();
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
                dt = null;                
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
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
            FillGroupsList();
            try
            {
                dt = objDB.ExecuteDataSet("select distinct lgmd_group_ecode from LevelGroup_map_Detl where lgmd_doc_month = '" + CommonData.DocMonth + "' and lgmd_dest_ecode = " + ((NewCheckboxListItem)(clbLeaders.SelectedItem)).Tag.ToString()).Tables[0];
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
                dt = null;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                objDB = new SQLDB();
                string sGroups = "", sqlText = "";
                int iRes = 0;
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
                    sqlText = "DELETE FROM LevelGroup_map_Detl WHERE lgmd_doc_month = '" + CommonData.DocMonth +
                            "' AND lgmd_dest_ecode=" + ((NewCheckboxListItem)(clbLeaders.SelectedItem)).Tag.ToString() + " AND lgmd_map_type!='BR'";
                    objDB.ExecuteSaveData(sqlText);
                    sqlText = "";
                    if (sGroups.Length > 5)
                    {
                        sqlText = "INSERT INTO LevelGroup_map_Detl(lgmd_company_code,lgmd_branch_code,lgmd_log_branch_code,lgmd_state_code" +
                                ",lgmd_doc_month,lgmd_group_ecode,lgmd_dest_ecode,lgm_user_id,lgm_created_date,lgmd_map_type)" +
                                " SELECT DISTINCT lgm_company_code,lgm_branch_code,lgm_log_branch_code,lgm_state_code,lgm_doc_month," +
                                "lgm_group_ecode,'" + ((NewCheckboxListItem)(clbLeaders.SelectedItem)).Tag.ToString() +
                                "','" + CommonData.LogUserId + "',getdate(),'AB' from LevelGroup_map where lgm_doc_month = '" + CommonData.DocMonth +
                                "' AND lgm_group_ecode IN (" + sGroups + ") AND lgm_group_ecode NOT IN (SELECT lgmd_group_ecode from LevelGroup_map_Detl " +
                                "where lgmd_doc_month = '" + CommonData.DocMonth + "' and lgmd_dest_ecode = " + ((NewCheckboxListItem)(clbLeaders.SelectedItem)).Tag.ToString() +
                                " AND lgmd_map_type = 'BR')";
                    }
                    iRes = objDB.ExecuteSaveData(sqlText);
                    MessageBox.Show("Data Saved Successfully", "SSERP-AboveBranchMapping", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GetMappedGroups();
                    FillLeadersList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objDB = null;
                }
            }
        }

        private bool CheckData()
        {
            if (clbLeaders.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select Atleast one leader", "SSERP-AboveBranchMapping", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbLeaders.Items.Count; i++)
            {                
                clbLeaders.SetItemCheckState(i, CheckState.Unchecked);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtEmpName.Text != "")
            {
                objDB = new SQLDB();
                int iRes = 0;
                string sqlText = "select * from LevelBranchAbove_Leaders where lbl_doc_month = '" + CommonData.DocMonth + "' and lbl_leader_ecode =" + txtDsearch.Text;
                DataTable dt = new DataTable();
                try
                {
                    dt = objDB.ExecuteDataSet(sqlText).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show(txtDsearch.Text + " Allready Added for document month " + CommonData.DocMonth, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        sqlText = "insert into LevelBranchAbove_Leaders(lbl_fin_year,lbl_doc_month,lbl_leader_ecode,lbl_state_code) " +
                                "values('" + CommonData.FinancialYear + "','" + CommonData.DocMonth + "','" + txtDsearch.Text + "','');";
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
                    MessageBox.Show(txtDsearch.Text + " not added for month " + CommonData.DocMonth, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Enter Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDsearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtDsearch.Text.Length > 4)
            {
                GetEmpName();
            }
            else
                txtEmpName.Text = "";
            SearchEcode(txtDsearch.Text.ToString(), clbLeaders);
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
            string sqlText = "select MEMBER_NAME from EORA_MASTER WHERE ecode = " + txtDsearch.Text;
            DataTable dt = new DataTable();
            try
            {
                dt = objDB.ExecuteDataSet(sqlText).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtEmpName.Text = dt.Rows[0]["MEMBER_NAME"].ToString();
                }
                else
                    txtEmpName.Text = "";
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

        private void btnDeleteLeader_Click(object sender, EventArgs e)
        {
            if (txtEmpName.Text != "")
            {
                objDB = new SQLDB();
                int iRes = 0;
                string sqlText = "select * from LevelBranchAbove_Leaders where lbl_doc_month = '" + CommonData.DocMonth + "' and lbl_leader_ecode =" + txtDsearch.Text;
                DataTable dt = new DataTable();
                try
                {
                    dt = objDB.ExecuteDataSet(sqlText).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        sqlText = "delete from LevelGroup_map_Detl where lgmd_dest_ecode = " + txtDsearch.Text + " and lgmd_doc_month = '" + CommonData.DocMonth + "' and lgmd_map_type = 'AB'";
                        sqlText += " delete from LevelBranchAbove_Leaders where lbl_doc_month='" + CommonData.DocMonth + "' and lbl_leader_ecode=" + txtDsearch.Text;
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
                    MessageBox.Show(txtDsearch.Text + " not deleted for month " + CommonData.DocMonth, "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Enter Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
