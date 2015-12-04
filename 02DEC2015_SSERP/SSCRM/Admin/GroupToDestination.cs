using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using SSCRMDB;
using SSAdmin;
using SSCRM.App_Code;

namespace SSCRM
{
    
    public partial class GroupToDestination : Form
    {
        private StaffLevel objData = null;
        private SQLDB objDA = null;
        private int selectedRow = 0;
        public GroupToDestination()
        {
            InitializeComponent();
        }

        private void cbStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStates.SelectedIndex > 0)
            {
                FillBranchComboBox(cbStates.SelectedValue.ToString());
            }
        }

        private void GroupToDestination_Load(object sender, EventArgs e)
        {
            lblDocMonth.Text = CommonData.DocMonth.ToString().ToUpper();
            FillStateComboBox();
            FillLogicalBranchComboBox();
            fillGridMappedData();
            cbLogcalBranch.Enabled = true;
            clbDestination.ColumnWidth = 300;
        }

        private void cbBranches_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbStates.SelectedIndex > 0 && cbBranches.SelectedIndex > -1)
            {
                FillLogicalBranchComboBox();
                clbDestination.Items.Clear();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int intSave = 0;
            if (CheckCondition())
            {
                intSave = SaveMappedEmployee();
            }

            if (intSave > 0)
            {
                clbDestination.Items.Clear();
                selectedRow = 0;
                MessageBox.Show("Data saved", "Group to Destination", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            fillGridMappedData();
        }

        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            clbDestination.Items.Clear();
            gvMappedGroups.Rows.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int intSave = 0;
            if (CheckDeleteCondition())
            {
                DialogResult result = MessageBox.Show("Do you want to delete mapped data ?",
                                        "Source to Destination Data", MessageBoxButtons.YesNo,  MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    intSave = DeleteMappedData();
            }

            if (intSave > 0)
            {
                gvMappedGroups.Rows.Clear();
                selectedRow = 0;
            }
        }

        private void FillStateComboBox()
        {
            objData = new StaffLevel();
            try
            {
                DataTable dt = objData.GetStatesDS().Tables[0];

                if (dt.Rows.Count > 1)
                {
                    cbStates.DataSource = dt;
                    cbStates.DisplayMember = "State";
                    cbStates.ValueMember = "CDState";
                }
                cbStates.SelectedValue = CommonData.StateCode;
                cbStates.Enabled = false;
                dt = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Group To Destination", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                
                objData = null;
            }
        }

        private void FillBranchComboBox(string strStateCode)
        {
            objData = new StaffLevel();
            try
            {
                DataTable dt = objData.GetStateBranchesDS(CommonData.CompanyCode, strStateCode, CommonData.BranchCode).Tables[0];
                //DataRow dr = dt.NewRow();
                //dr[0] = "Select";
                //dr[1] = "Select";
                //dt.Rows.InsertAt(dr, 0);
                if (dt.Rows.Count > 0)
                {
                    cbBranches.DataSource = dt;
                    cbBranches.DisplayMember = "branch_name";
                    cbBranches.ValueMember = "branch_code";
                }
                cbBranches.SelectedValue = CommonData.BranchCode;
                dt = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Group To Destination", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
               
                objData = null;
            }
        }

        private void FillLogicalBranchComboBox()
        {
            objData = new StaffLevel();
            try
            {
                DataTable dt = objData.LevelsLogicalBranchList_Get(cbBranches.SelectedValue.ToString()).Tables[0];
                DataRow dr = dt.NewRow();
                dr[0] = "Select";
                dr[1] = "Select";
                dt.Rows.InsertAt(dr, 0);
                if (dt.Rows.Count > 1)
                {
                    cbLogcalBranch.DataSource = dt;
                    cbLogcalBranch.DisplayMember = "Lbranch_name";
                    cbLogcalBranch.ValueMember = "Lbranch_code";
                }
                dt = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Group To Destination", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                objData = null;
            }
        }

        
        private void FillGroupDetailMappedData(int intRow)
        {
            if (intRow >= 0)
            {
                objData = new StaffLevel();
                string strLogBcode = string.Empty;
                string[] strDestCode = gvMappedGroups.Rows[intRow].Cells["ecode"].Value.ToString().Split('~');
                try
                {
                    clbDestination.Items.Clear();
                    DataTable dt = objData.LevelGroupDetailEcodesMapped_Get(CommonData.CompanyCode, cbStates.SelectedValue.ToString(), cbBranches.SelectedValue.ToString(), Convert.ToInt32(strDestCode[1]), Convert.ToInt32(strDestCode[0]), strLogBcode).Tables[0];
                    if (dt.Rows.Count > 0)
                    {

                        foreach (DataRow dataRow in dt.Rows)
                        {
                            if (dataRow["mapped"].ToString() == "999999")
                            {
                                NewCheckboxListItem oclBox = new NewCheckboxListItem();
                                oclBox.Tag = dataRow["ECODE"].ToString();
                                oclBox.Text = dataRow["ENAME"].ToString();
                                clbDestination.Items.Add(oclBox, false);
                                oclBox = null;
                            }
                            else
                            {
                                NewCheckboxListItem oclBox = new NewCheckboxListItem();
                                oclBox.Tag = dataRow["ECODE"].ToString();
                                oclBox.Text = dataRow["ENAME"].ToString();
                                clbDestination.Items.Add(oclBox, true);
                                oclBox = null;

                            }


                        }

                    }
                    dt = null;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Group To Destination", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {

                    objData = null;
                }
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

       
        private int SaveMappedEmployee()
        {
            objDA = new SQLDB();
            int rec = 0;
            string strSQL = string.Empty;
            try
            {
                 strSQL=GetSelectedEcodes();

                if(strSQL.Length>20)
                    rec = objDA.ExecuteSaveData(strSQL);
               
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Group To Destination", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                objDA = null;
            }
            return rec;
        }

        private string GetSelectedEcodes()
        {
            string[] strArrGroupECode = null;
            StringBuilder sbSQL = new StringBuilder();
            string strSQL = string.Empty;
            if (gvMappedGroups.Rows.Count > 0)
            {
                strArrGroupECode = gvMappedGroups.Rows[selectedRow].Cells["ecode"].Value.ToString().Split('~');
                int intRec = DeleteMappedData();
                objDA = new SQLDB();
                string strLogBRCode = string.Empty;
                if (cbLogcalBranch.SelectedIndex > 0)
                    strLogBRCode = cbLogcalBranch.SelectedValue.ToString();

                for (int i = 0; i < clbDestination.Items.Count; i++)
                {
                    if (clbDestination.GetItemCheckState(i) == CheckState.Checked)
                    {
                        string[] strDestCode = ((NewCheckboxListItem)(clbDestination.Items[i])).Tag.Split('~');
                        sbSQL.Append("INSERT INTO LevelGroup_map_Detl(lgmd_company_code, lgmd_branch_code, lgmd_log_branch_code, lgmd_state_code, lgmd_doc_month, lgmd_group_ecode, lgmd_dest_ecode, lgm_user_id, lgm_created_date)" +
                                       " VALUES('" + CommonData.CompanyCode + "', '" + cbBranches.SelectedValue.ToString() + "', '" + strLogBRCode + "', '" + cbStates.SelectedValue.ToString() +
                                       "', '" + CommonData.DocMonth.ToUpper() + "', " + strArrGroupECode[0].ToString() +
                                       ", " + strDestCode[0] + ",'" + CommonData.LogUserId.ToString() + "', getdate());");
                    }
                }

            }
            
            strSQL = sbSQL.ToString().Substring(0, sbSQL.ToString().Length - 1);
            return strSQL;
        }

        private bool CheckCondition()
        {
            bool blCheck = true;
            if (cbStates.SelectedIndex <= 0)
            {
                MessageBox.Show("Select state", "Group To Destination", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blCheck = false;
                cbStates.Focus();
                return blCheck;
            }
            if (cbBranches.SelectedIndex == -1)
            {
                MessageBox.Show("Select branch", "Group To Destination", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blCheck = false;
                cbBranches.Focus();
                return blCheck;
            }
            
            if (gvMappedGroups.Rows.Count==0)
            {
                MessageBox.Show("Select group", "Group To Destination", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blCheck = false;
                //clbGroup.Focus();
                return blCheck;
            }

            bool blSource = false;
            for (int i = 0; i < clbDestination.Items.Count; i++)
            {
                if (clbDestination.GetItemCheckState(i) == CheckState.Checked)
                {
                    blSource = true;
                    break;
                }
            }

            if (clbDestination.Items.Count == 0 || blSource == false)
            {
                MessageBox.Show("Select Destination atleast one", "Group To Destination", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blCheck = false;
                clbDestination.Focus();
                return blCheck;
            }
            return blCheck;
        }

        private bool CheckDeleteCondition()
        {
            bool blCheck = true;
            if (cbStates.SelectedIndex == -1)
            {
                MessageBox.Show("Select state", "Group To Destination", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blCheck = false;
                cbStates.Focus();
                return blCheck;
            }
            if (cbBranches.SelectedIndex == -1)
            {
                MessageBox.Show("Select branch", "Group To Destination", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blCheck = false;
                cbBranches.Focus();
                return blCheck;
            }
            
            bool blSource = false;
            for (int i = 0; i < clbDestination.Items.Count; i++)
            {
                if (clbDestination.GetItemCheckState(i) == CheckState.Checked)
                {
                    blSource = true;
                    break;
                }
            }

            if (clbDestination.Items.Count == 0 || blSource == false)
            {
                MessageBox.Show("Select Destination atleast one", "Group To Destination", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blCheck = false;
                clbDestination.Focus();
                return blCheck;
            }
            return blCheck;
        }

        private int DeleteMappedData()
        {
           
            string[] strArrDestCode = null;
            int nDelete = 0;
            objDA = new SQLDB();
            string strDelete = string.Empty;
            try
            {
                if (clbDestination.Items.Count > 0)
                {
                    string strSCodes = gvMappedGroups.Rows[selectedRow].Cells["ecode"].Value.ToString();
                    strArrDestCode = strSCodes.Split('~');

                        strDelete = "Delete from LevelGroup_map_Detl " +
                           " WHERE lgmd_company_code='" + CommonData.CompanyCode.ToString() +
                           "' AND lgmd_branch_code='" + cbBranches.SelectedValue.ToString() +
                           "' AND lgmd_state_code='" + cbStates.SelectedValue.ToString() +
                           "' AND lgmd_doc_month='" + CommonData.DocMonth.ToString().ToUpper() +
                           "'  AND lgmd_group_ecode=" + strArrDestCode[0].ToString() + " and lgmd_map_type='BR'";
                    
                    nDelete = objDA.ExecuteSaveData(strDelete);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objDA = null;
            }
            return nDelete;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtDsearch_KeyUp(object sender, KeyEventArgs e)
        {
            SearchEcode(txtDsearch.Text.ToString(), clbDestination);
        }

        private bool CheckGroupMapped()
        {
            bool blOk = true;
            string strSQL = " SELECT  lgm_source_ecode FROM LevelGroup_map " +
                            " WHERE lgm_company_code='" + CommonData.CompanyCode +
                            "' AND lgm_branch_code='" + CommonData.BranchCode +
                            "' AND lgm_doc_month='" + CommonData.DocMonth + "'";
            objDA = new SQLDB();
            DataTable dt = objDA.ExecuteDataSet(strSQL).Tables[0];

            if (dt.Rows.Count == 0)
                blOk = false;

            dt = null;

            return blOk;
        }

        private void btnEcodeAdd_Click(object sender, EventArgs e)
        {
            //EcodeSearch Search = new EcodeSearch("GroupToDest");
            //Search.objFrmGroupToDestination = this;
            //Search.ShowDialog();
        }

        private void fillGridMappedData()
        {

            string strLogBcode = string.Empty;
            objData = new StaffLevel();
            gvMappedGroups.Rows.Clear();
            DataTable dt = objData.LevelMappedGroupList_Get(CommonData.CompanyCode, CommonData.StateCode, CommonData.BranchCode).Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataGridViewRow tempRow = new DataGridViewRow();

                    DataGridViewCell cellECODE = new DataGridViewTextBoxCell();
                    cellECODE.Value = dt.Rows[i]["ECODE"];
                    tempRow.Cells.Add(cellECODE);

                    DataGridViewCell cellGROUPName = new DataGridViewTextBoxCell();
                    cellGROUPName.Value = dt.Rows[i]["GroupName"];
                    tempRow.Cells.Add(cellGROUPName);

                    DataGridViewCell cellGroupEname = new DataGridViewTextBoxCell();
                    cellGroupEname.Value = dt.Rows[i]["ENAME"];
                    tempRow.Cells.Add(cellGroupEname);

                    DataGridViewCell celllogBranchCode = new DataGridViewTextBoxCell();
                    celllogBranchCode.Value = dt.Rows[i]["logBranchCode"];
                    tempRow.Cells.Add(celllogBranchCode);

                    DataGridViewCell cellLogicalBranch = new DataGridViewTextBoxCell();
                    cellLogicalBranch.Value = dt.Rows[i]["logBranchName"];
                    tempRow.Cells.Add(cellLogicalBranch);

                    gvMappedGroups.Rows.Add(tempRow);

                }
            }
        }

         private void gvMappedGroups_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (cbStates.SelectedIndex > 0 && cbBranches.SelectedIndex > -1)
                {
                    FillGroupDetailMappedData(e.RowIndex);
                    selectedRow = e.RowIndex;
                }
                if (cbLogcalBranch.Items.Count != 0)
                {
                    txtlogicalBranch.Text = "";
                    //string logBranchCode = gvMappedGroups.Rows[e.RowIndex].Cells["logBranchCode"].Value.ToString();
                    if (gvMappedGroups.Rows[e.RowIndex].Cells["logBranchCode"].Value.ToString() == "                    ")
                        cbLogcalBranch.SelectedIndex = 0;
                    else
                    {
                        cbLogcalBranch.SelectedValue = gvMappedGroups.Rows[e.RowIndex].Cells["logBranchCode"].Value.ToString();
                        txtlogicalBranch.Text = gvMappedGroups.Rows[e.RowIndex].Cells["LogicalBranch"].Value.ToString();
                    }
                }
            }
            
        }

        private void gvMappedGroups_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (cbStates.SelectedIndex > 0 && cbBranches.SelectedIndex > -1)
            {
                FillGroupDetailMappedData(e.RowIndex);
                selectedRow = e.RowIndex;
            }
            
        }

        private void txtDsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            SearchEcode(txtDsearch.Text.ToString(), clbDestination);
        }

        private void txtGroupSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Int32 rowindex = 0;
            //foreach (DataGridViewRow row in gvMappedGroups.Rows)
            //{
            //    if (UtilityFunctions.IsNumeric(txtGroupSearch.Text) == true)
            //    {
            //        if (row.Cells["ECode"].Value.ToString().Contains(txtGroupSearch.Text.ToUpper()))
            //        {
            //            rowindex = row.Index;
                        
            //        }
            //        else
            //            gvMappedGroups.Rows[row.Index].DefaultCellStyle.BackColor = Color.White;
            //    }
            //    else
            //    {
            //        if (row.Cells["GroupName"].Value.ToString().Contains(txtGroupSearch.Text.ToUpper()))
            //        {
            //            rowindex = row.Index;
            //        }
            //        else
            //            gvMappedGroups.Rows[row.Index].DefaultCellStyle.BackColor = Color.White;
            //    }
            //    if (txtGroupSearch.Text == "")
            //        gvMappedGroups.Rows[row.Index].DefaultCellStyle.BackColor = Color.White;
            //    gvMappedGroups.Rows[rowindex].Selected = true;
            //    gvMappedGroups.Rows[rowindex].DefaultCellStyle.BackColor = Color.Yellow;
            //}
        }
        
    }
}
