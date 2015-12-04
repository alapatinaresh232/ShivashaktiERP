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
using SSTrans;

namespace SSCRM
{
    public partial class ServiceLevelGroupToDestination : Form
    {
        StaffLevel objData = null;
        ServiceDB objServicedb = null;
        SQLDB objSQLdb = null;
        private int selectedRow = 0;
        

        public ServiceLevelGroupToDestination()
        {
            InitializeComponent();
        }

        private void ServiceLevelGroupToDestination_Load(object sender, EventArgs e)
        {
            lblDocMonth.Text = CommonData.DocMonth.ToString().ToUpper();
            FillStateComboBox();
            FillLogicalBranchComboBox();
            FillMappedGroupDataToGrid();
            cbLogcalBranch.Enabled = true;

            gvMappedGroups.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                       System.Drawing.FontStyle.Regular);

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
               
                if (dt.Rows.Count > 0)
                {
                    //DataRow dr = dt.NewRow();
                    //dr[0] = "--Select--";
                    //dr[1] = "--Select--";
                    //dt.Rows.InsertAt(dr, 0);

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
                dr[0] = "--Select--";
                dr[1] = "--Select--";
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

        private void cbStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStates.SelectedIndex > 0)
            {
                FillBranchComboBox(cbStates.SelectedValue.ToString());
            }

        }

        private void cbBranches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStates.SelectedIndex > 0 && cbBranches.SelectedIndex > -1)
            {
                FillLogicalBranchComboBox();
                clbDestination.Items.Clear();
                FillMappedGroupDataToGrid();
            }
        }

        private void FillMappedGroupDataToGrid()
        {
            objServicedb = new ServiceDB();
            DataTable dt = new DataTable();
            string strLogBcode = string.Empty;
            gvMappedGroups.Rows.Clear();
            try
            {
                dt = objServicedb.LevelServiceMappedGroupList_Get(CommonData.CompanyCode, CommonData.BranchCode, CommonData.StateCode).Tables[0];
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objServicedb = null;
                dt = null;
            }
        }

        private void FillGroupDetailMappedData(int intRow)
        {
            objServicedb = new ServiceDB();
            DataTable dt = new DataTable();
            string strLogBcode = string.Empty;
            string[] strDestCode = gvMappedGroups.Rows[intRow].Cells["ecode"].Value.ToString().Split('~');
            try
            {
                clbDestination.Items.Clear();
                dt = objServicedb.ServiceLevelGroupDetailEcodesMapped_Get(CommonData.CompanyCode, cbBranches.SelectedValue.ToString(), cbStates.SelectedValue.ToString(), Convert.ToInt32(strDestCode[1]), Convert.ToInt32(strDestCode[0]),CommonData.DocMonth ,strLogBcode).Tables[0];

                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dt.Rows)
                    {
                        if (dataRow["mapped"].ToString() == "999999")
                        {
                            NewCheckboxListItem oclBox = new NewCheckboxListItem();
                            oclBox.Tag = dataRow["ECODE"].ToString();
                            oclBox.Text = dataRow["ENAME"].ToString();
                            clbDestination.Items.Add(oclBox,false);
                            oclBox = null;
                        }
                        else
                        {
                            NewCheckboxListItem oclBox = new NewCheckboxListItem();
                            oclBox.Tag = dataRow["ECODE"].ToString();
                            oclBox.Text = dataRow["ENAME"].ToString();
                            clbDestination.Items.Add(oclBox,true);
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

                objServicedb = null;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void gvMappedGroups_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (cbStates.SelectedIndex > 0 && cbBranches.SelectedIndex > -1)
            {
                FillGroupDetailMappedData(e.RowIndex);
                selectedRow = e.RowIndex;
                cbLogcalBranch.SelectedValue = gvMappedGroups.Rows[e.RowIndex].Cells["logBranchCode"].Value.ToString();
                txtlogicalBranch.Text = gvMappedGroups.Rows[e.RowIndex].Cells["LogicalBranch"].Value.ToString();
            }
            
        }

        private bool CheckData()
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

            if (gvMappedGroups.Rows.Count == 0)
            {
                MessageBox.Show("Select group", "Group To Destination", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blCheck = false;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            int intSave = 0;

            if (CheckData() == true)
            {
                try
                {
                    intSave = SaveMappedEmployee();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }


                if (intSave > 0)
                {
                    MessageBox.Show("Data saved Sucessfully", "Group to Destination", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clbDestination.Items.Clear();
                    selectedRow = 0;
                    FillMappedGroupDataToGrid();
                }
                else
                {
                    MessageBox.Show("Data Not saved", "Group to Destination", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }

        }

        private int SaveMappedEmployee()
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = string.Empty;
            try
            {
                strCommand = GetSelectedEcodes();

                if (strCommand.Length > 20)
                {
                    iRes = objSQLdb.ExecuteSaveData(strCommand);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Group To Destination", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                objSQLdb = null;
            }
            return iRes;
        }

        private string GetSelectedEcodes()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string[] strArrGroupECode = null;
            string strInsert = string.Empty;
            try
            {
                if (gvMappedGroups.Rows.Count > 0)
                {
                    strArrGroupECode = gvMappedGroups.Rows[selectedRow].Cells["ecode"].Value.ToString().Split('~');
                    int intRec = DeleteMappedData();
                    objSQLdb = new SQLDB();
                    string strLogBRCode = string.Empty;
                    if (cbLogcalBranch.SelectedIndex > 0)
                        strLogBRCode = cbLogcalBranch.SelectedValue.ToString();

                    for (int i = 0; i < clbDestination.Items.Count; i++)
                    {
                        if (clbDestination.GetItemCheckState(i) == CheckState.Checked)
                        {
                            string[] strDestCode = ((NewCheckboxListItem)(clbDestination.Items[i])).Tag.Split('~');


                            strInsert += "INSERT INTO ServiceLevelGroup_map_Detl(lgmd_company_code "+
                                                                              ", lgmd_branch_code "+
                                                                              ", lgmd_log_branch_code "+
                                                                              ", lgmd_state_code "+
                                                                              ", lgmd_doc_month "+
                                                                              ", lgmd_group_ecode "+
                                                                              ", lgmd_dest_ecode "+
                                                                              ", lgmd_user_id "+ 
                                                                              ", lgmd_created_date "+
                                                                              ", lgmd_map_type)" +
                                                                              " VALUES('" + CommonData.CompanyCode + 
                                                                              "', '" + cbBranches.SelectedValue.ToString() + 
                                                                              "', '" + strLogBRCode +
                                                                              "', '" + cbStates.SelectedValue.ToString() +
                                                                              "', '" + CommonData.DocMonth.ToUpper() + 
                                                                              "', " + strArrGroupECode[0].ToString() +
                                                                              ", " + strDestCode[0] +
                                                                              ",'" + CommonData.LogUserId.ToString() +
                                                                              "',' " + CommonData.CurrentDate +
                                                                              "','" + CommonData.BranchType + "')";
                        }
                    }

                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return strInsert;
        }

        private int DeleteMappedData()
        {
            objSQLdb = new SQLDB();
            string strDelete = string.Empty;
            string[] strArrDestCode = null;
            int nDelete = 0;
            try
            {
                if (clbDestination.Items.Count > 0)
                {
                    string strSCodes = gvMappedGroups.Rows[selectedRow].Cells["ecode"].Value.ToString();
                    strArrDestCode = strSCodes.Split('~');

                    strDelete = "DELETE FROM ServiceLevelGroup_map_Detl " +
                               " WHERE lgmd_company_code='" + CommonData.CompanyCode.ToString() +
                             "' AND lgmd_branch_code='" + cbBranches.SelectedValue.ToString() +
                              "' AND lgmd_state_code='" + cbStates.SelectedValue.ToString() +
                              "' AND lgmd_doc_month='" + CommonData.DocMonth.ToString().ToUpper() +
                              "'  AND lgmd_group_ecode=" + strArrDestCode[0].ToString();

                    nDelete = objSQLdb.ExecuteSaveData(strDelete);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objSQLdb = null;
            }
            return nDelete;
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

        private void txtDsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            SearchEcode(txtDsearch.Text.ToString(), clbDestination);
        }

        private void gvMappedGroups_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (cbStates.SelectedIndex > 0 && cbBranches.SelectedIndex > -1)
            {
                FillGroupDetailMappedData(e.RowIndex);
                selectedRow = e.RowIndex;
            }
            if (cbLogcalBranch.Items.Count != 0)
            {
                txtlogicalBranch.Text = "";
                if (gvMappedGroups.Rows[e.RowIndex].Cells["logBranchCode"].Value.ToString() == " ")
                {
                    cbLogcalBranch.SelectedIndex = 0;
                }
                else
                {
                    cbLogcalBranch.SelectedValue = gvMappedGroups.Rows[e.RowIndex].Cells["logBranchCode"].Value.ToString();
                    txtlogicalBranch.Text = gvMappedGroups.Rows[e.RowIndex].Cells["LogicalBranch"].Value.ToString();
                }
            }
        }

        private bool CheckDeleteCondition()
        {
            bool flag = true;
            if (cbStates.SelectedIndex == -1)
            {
                MessageBox.Show("Select state", "Group To Destination", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                flag = false;
                cbStates.Focus();
                return flag;
            }
            if (cbBranches.SelectedIndex == -1)
            {
                MessageBox.Show("Select branch", "Group To Destination", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                flag = false;
                cbBranches.Focus();
                return flag;
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
                flag = false;
                clbDestination.Focus();
                return flag;
            }
            return flag;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int iRes = 0;

            if (CheckDeleteCondition())
            {
                DialogResult result = MessageBox.Show("Do you want to delete mapped data ?",
                                        "Source to Destination Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    iRes = DeleteMappedData();
                }
                else
                {
                    MessageBox.Show("Mapped Data Not Deleted");
                }
            }

            if (iRes > 0)
            {
                gvMappedGroups.Rows.Clear();
                selectedRow = 0;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            clbDestination.Items.Clear();
            gvMappedGroups.Rows.Clear();
            cbLogcalBranch.SelectedIndex = 0;


        }
        
    }
}
