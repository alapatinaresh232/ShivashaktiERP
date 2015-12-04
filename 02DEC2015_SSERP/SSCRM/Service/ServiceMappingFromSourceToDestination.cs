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
using DataBoundTreeView;

namespace SSCRM
{
    public partial class ServiceMappingFromSourceToDestination : Form
    {
        private StaffLevel objData = null;
        private SQLDB objSQLdb = null;
        private ServiceDB objServicedb = null;
        private int intGridRow = -1;
        private string isModify = "NO";
        

        public ServiceMappingFromSourceToDestination()
        {
            InitializeComponent();
           
        }

        private void ServiceMappingFromSourceToDestination_Load(object sender, EventArgs e)
        {
           
            lblDocMonth.Text = CommonData.DocMonth.ToString().ToUpper();
            FillStates();
            FillServicelevel();
            FillCampComboBox();
            FillLogicalBranchComboBox();
            FillMappList();
            rdbGroup.Checked = true;
            FillMappedDataToGrid();

        }

        private void FillStates()
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
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message, "Source To Destination", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                strStateCode = cbStates.SelectedValue.ToString();
                DataTable dt = objData.GetStateBranchesDS(CommonData.CompanyCode, strStateCode, CommonData.BranchCode).Tables[0];
                
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "Select";
                    dr[1] = "Select";
                    dt.Rows.InsertAt(dr, 0);

                    cbBranches.DataSource = dt;
                    cbBranches.DisplayMember = "branch_name";
                    cbBranches.ValueMember = "branch_code";
                }
                cbBranches.SelectedValue = CommonData.BranchCode;
                dt = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                objData = null;
            }
        }

        private void FillCampComboBox()
        {
            objData = new StaffLevel();
            try
            {
                DataTable dt = objData.LevelCampList_Get("T").Tables[0];
                DataRow dr = dt.NewRow();
                dr[0] = "--Select--";
                dr[1] = "--Select--";
                dt.Rows.InsertAt(dr, 0);
                if (dt.Rows.Count > 1)
                {
                    cbGroupCamp.DataSource = dt;
                    cbGroupCamp.DisplayMember = "CAMP_NAME";
                    cbGroupCamp.ValueMember = "CAMP_CODE";
                }
                dt = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Source To Destination", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                objData = null;
            }
        }

        private void FillServicelevel()
        {
            //objSQLdb = new SQLDB();
            objServicedb = new ServiceDB();
            DataTable dt = new DataTable();
            cbLevels.DataSource = null;
            try
            {
                dt = objServicedb.GetServiceLevels(CommonData.CompanyCode, CommonData.BranchType).Tables[0];
                //string strCmd = "SELECT desig_code,desig_name,ndesig_name FROM DESIG_MAS "+
                //                " WHERE dept_code IN(800000) ORDER BY ndesig_name ";
                //dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 1)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "0";
                    dr[1] = "--Select--";
                    dt.Rows.InsertAt(dr, 0);

                    cbLevels.DataSource = dt;
                    cbLevels.DisplayMember = "desig_name";
                    cbLevels.ValueMember = "desig_code";

                   
                    
                }
                dt = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private void cbStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStates.SelectedIndex > 0)
            {
                clbSource.Items.Clear();
                clbSource.Items.Clear();
                FillBranchComboBox(cbStates.SelectedValue.ToString());
            }
        }
        private void FillMappList()
        {
            objServicedb = new ServiceDB();
            char cMapped = 'T';
            Hashtable ht = new Hashtable();
            DataSet ds = new DataSet();
            lbMapp.DataSource = null;
            try
            {
                if (chkMapp.CheckState == CheckState.Unchecked)
                {
                    cMapped = 'F';
                    lblMap.Text = "Unmapped Source";
                }
                else
                {
                    lblMap.Text = "Mapped Source";
                }

                DataTable dt = objServicedb.GetMapOrUnmappedEcodes(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth, cMapped).Tables[0];
                if (dt.Rows.Count > 1)
                {
                    
                    lbMapp.DisplayMember = "ENAME";
                    lbMapp.ValueMember = "ECODE";
                    lbMapp.DataSource = dt;
                }
                dt = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                objServicedb = null;
            }
        }

        private void chkMapp_CheckStateChanged(object sender, EventArgs e)
        {
            FillMappList();

        }

        private void cbGroupCamp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbGroupCamp.SelectedIndex > 0)
            {
                string[] strCamp = cbGroupCamp.SelectedValue.ToString().Split('^');
                if (strCamp.Length > 2)
                {
                    txtAddress.Text = strCamp[1].ToString();
                    txtPhone.Text = strCamp[2].ToString();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbLevels.SelectedIndex = 0;
            if (cbGroupCamp.Items.Count != 0)
                cbGroupCamp.SelectedIndex = 0;
            txtAddress.Text = "";
            txtPhone.Text = "";
            clbSource.Items.Clear();
            clbDestination.Items.Clear();
            FillMappedDataToGrid();
        }

        private void FillDestinationData(string BranchCode,Int32 DesigId)
        {
            objServicedb=new ServiceDB();
            DataTable dt = new DataTable();
            string strLoadedEcode = string.Empty;
            
            DesigId=Convert.ToInt32(cbLevels.SelectedValue.ToString());
            clbDestination.Items.Clear();
            try
            {
                dt = objServicedb.EcodesForService_Get(cbBranches.SelectedValue.ToString(),DesigId,CommonData.DocMonth).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = dataRow["ECODE"].ToString();
                        oclBox.Text = dataRow["Ename"].ToString();
                        if (!strLoadedEcode.Contains(dataRow["ECODE"].ToString()))
                            clbDestination.Items.Add(oclBox);

                        strLoadedEcode += dataRow["ECODE"].ToString() + ",";
                        oclBox = null;
                    }


                }
                dt = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objServicedb = null;
            }
        }

        private void cbLevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStates.SelectedIndex > 0 && cbBranches.SelectedIndex > -1 && cbLevels.SelectedIndex > 0)
            {
                clbSource.Items.Clear();
                clbDestination.Items.Clear();
                FillDestinationData(cbBranches.SelectedValue.ToString(), Convert.ToInt32(cbLevels.SelectedValue.ToString()));
                removeExistedGroupEcode();
                if (cbGroupCamp.Items.Count != 0)
                    cbGroupCamp.SelectedIndex = 0;
                txtAddress.Text = "";
                txtPhone.Text = "";
                cbGroupCamp.Focus();
               
            }
        }

        private void removeExistedGroupEcode()
        {
            try
            {
                if (clbDestination.Items.Count > 0 && gvMappedGroups.Rows.Count > 0)
                {
                    for (int j = 0; j < gvMappedGroups.Rows.Count; j++)
                    {
                        string strMEcode = gvMappedGroups.Rows[j].Cells["Ecode"].Value.ToString();
                        for (int i = 0; i < clbDestination.Items.Count; i++)
                        {
                            string strDCodes = ((NewCheckboxListItem)(clbDestination.Items[i])).Tag;
                            if (strMEcode == strDCodes)
                            {
                                clbDestination.Items.RemoveAt(i);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

      
        private void FillGroupMappedSourceData(string strBCode,string strSCode)
        {
            objServicedb = new ServiceDB();
            DataTable dt = new DataTable();
                      
            string strLogBcode = string.Empty;
            string strLoadedEcode = string.Empty;
            string[] strDestCode = ((NewCheckboxListItem)(clbDestination.SelectedItem)).Tag.Split('~');
            
            clbSource.Items.Clear();
            try
            {
                dt = objServicedb.LevelGroupServiceEcodeMapped_Get(CommonData.CompanyCode, strBCode, strSCode, Convert.ToInt32(strDestCode[0]), Convert.ToInt32(cbLevels.SelectedValue.ToString()), strLogBcode).Tables[0];
                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dt.Rows)
                    {
                        if (dataRow["mapped"].ToString() == "999999")
                        {
                            cbGroupCamp.Text = dataRow["GroupName"].ToString();
                            cbLogcalBranch.Text = dataRow["logBranchName"].ToString();
                            NewCheckboxListItem oclBox = new NewCheckboxListItem();
                            oclBox.Tag = dataRow["ECODE"].ToString();
                            oclBox.Text = dataRow["ENAME"].ToString();
                            clbSource.Items.Add(oclBox);
                            oclBox = null;
                        }
                        else
                        {
                            if (strDestCode[0].IndexOf(dataRow["ECODE"].ToString().Substring(0, dataRow["ECODE"].ToString().IndexOf("~"))) < 0)
                            {
                                cbGroupCamp.Text = dataRow["GroupName"].ToString();
                                 cbLogcalBranch.Text = dataRow["logBranchName"].ToString();
                                NewCheckboxListItem oclBox = new NewCheckboxListItem();
                                oclBox.Tag = dataRow["ECODE"].ToString();
                                oclBox.Text = dataRow["ENAME"].ToString();
                                if (!strLoadedEcode.Contains(dataRow["ECODE"].ToString()))
                                    clbSource.Items.Add(oclBox, CheckState.Checked);

                                strLoadedEcode += dataRow["ECODE"].ToString() + ",";
                                //clbSource.Items.Add(oclBox, CheckState.Checked);
                                oclBox = null;
                            }
                        }

                        
                    }

                }
                dt = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objServicedb = null;
                
            }
        }

        private void clbDestination_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            clbSource.Items.Clear();
            if (cbStates.SelectedIndex > 0 && cbBranches.SelectedIndex > -1 && cbLevels.SelectedIndex > 0 && e.NewValue == CheckState.Checked)
            {
                isModify = "NO";
                for (int i = 0; i < clbDestination.Items.Count; i++)
                {
                    if (e.Index != i)
                        clbDestination.SetItemCheckState(i, CheckState.Unchecked);
                }
                if (rdbGroup.Checked == true)
                {
                    FillGroupMappedSourceData(cbBranches.SelectedValue.ToString(), cbStates.SelectedValue.ToString());
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

        private void txtMsearch_KeyUp(object sender, KeyEventArgs e)
        {
            SearchEcode(txtMsearch.Text.ToString(), lbMapp);
        }

        private void txtDsearch_KeyUp(object sender, KeyEventArgs e)
        {
            SearchEcode(txtDsearch.Text.ToString(), clbDestination);
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            SearchEcode(txtSearch.Text.ToString(), clbSource);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int intSave = 0;
            try
            {
                if (CheckCondition()==true)
                {
                    intSave = SaveMappedEmployee();
                }

                if (intSave > 0)
                {
                    MessageBox.Show("Mapped Data Saved Sucessfully","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    clbSource.Items.Clear();
                    clbDestination.Items.Clear();
                    //cbLevels.SelectedIndex = 0;
                    if (cbGroupCamp.SelectedIndex >= 0)
                        cbGroupCamp.SelectedIndex = 0;

                    txtAddress.Text = "";
                    txtPhone.Text = "";
                    chkMapp.Checked = false;
                    isModify = "NO";
                    FillMappList();
                    if (cbLogcalBranch.SelectedIndex >= 0)
                        cbLogcalBranch.SelectedIndex = 0;
                }
                FillServicelevel();
            }
            catch (Exception ex)
            {
                isModify = "NO";
                MessageBox.Show(ex.Message);
            }
        }

        private bool CheckCondition()
        {
            bool blCheck = true;
            if (cbStates.SelectedIndex <= 0)
            {
                MessageBox.Show("Select state");
                blCheck = false;
                cbStates.Focus();
                return blCheck;
            }
            if (cbBranches.SelectedIndex == -1)
            {
                MessageBox.Show("Select branch");
                blCheck = false;
                cbBranches.Focus();
                return blCheck;
            }
            if (cbLevels.SelectedIndex <= 0 && intGridRow == -1)
            {
                MessageBox.Show("Select level");
                blCheck = false;
                cbLevels.Focus();
                return blCheck;
            }
            if (rdbGroup.Checked == true)
            {
                if (cbGroupCamp.SelectedIndex <= 0)
                {
                    MessageBox.Show("Select camp.");
                    blCheck = false;
                    cbGroupCamp.Focus();
                    return blCheck;
                }
            }
            if (rdbGroup.Checked == true)
            {
                if (CheckDuplicateGroupName())
                {
                    MessageBox.Show("Given camp name already exist.", "Group Mapping", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    blCheck = false;
                    cbGroupCamp.Focus();
                    return blCheck;
                }
            }
            bool blSource = true;
            for (int i = 0; i < clbSource.Items.Count; i++)
            {
                if (clbSource.GetItemCheckState(i) == CheckState.Checked)
                {
                    blSource = true;
                    break;
                }
            }

            if (clbSource.Items.Count == 0 || blSource == false)
            {
                MessageBox.Show("Select source atleast one", "Group Mapping", MessageBoxButtons.OK, MessageBoxIcon.Information);
                blCheck = false;
                clbSource.Focus();
                return blCheck;
            }
            return blCheck;
        }

        private int SaveMappedEmployee()
        {
            objSQLdb = new SQLDB();
            int rec = 0;
            string strCommand = string.Empty;
            try
            {
                strCommand = GetSelectedEcodes();

                if (strCommand.Length > 20)
                    rec = objSQLdb.ExecuteSaveData(strCommand);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                isModify = "NO";
                FillMappedDataToGrid();
                intGridRow = -1;
                //FillMappList();
                objSQLdb = null;
            }
            return rec;
        }

        private string GetSelectedEcodes()
        {
            objSQLdb = new SQLDB();
            StringBuilder sbSQL = new StringBuilder();
            string strCommand = string.Empty;
            string[] strArrDestCode = null;
            string[] strArrSourceCode = null;
            string strCamp = "";
            if (clbSource.Items.Count > 0)
            {
                string strSCodes = string.Empty;
                if (clbDestination.SelectedIndex > -1)
                    strSCodes = ((NewCheckboxListItem)(clbDestination.SelectedItem)).Tag;
                else
                    strSCodes = gvMappedGroups.Rows[intGridRow].Cells["ecode"].Value.ToString();

                strArrDestCode = strSCodes.Split('~');
                string strDelele = "DELETE FROM ServiceLevelGroup_map " +
                        " WHERE lgm_company_code='" + CommonData.CompanyCode.ToString() +
                        "' AND lgm_branch_code='" + cbBranches.SelectedValue.ToString() +
                        "' AND lgm_state_code='" + cbStates.SelectedValue.ToString() +
                        "' AND Upper(lgm_doc_month)='" + CommonData.DocMonth.ToString().ToUpper() +
                        "' AND lgm_group_ecode=" + strArrDestCode[0].ToString();

                int intRec = objSQLdb.ExecuteSaveData(strDelele);

                string strLogBRCode = string.Empty;
                if (cbLogcalBranch.SelectedIndex > 0)
                    strLogBRCode = cbLogcalBranch.SelectedValue.ToString();


                strCamp = cbGroupCamp.Text.ToString();

                for (int i = 0; i < clbSource.Items.Count; i++)
                {
                    if (clbSource.GetItemCheckState(i) == CheckState.Checked)
                    {
                        string strDCodes = ((NewCheckboxListItem)(clbSource.Items[i])).Tag;
                        strArrSourceCode = strDCodes.Split('~');
                        strCommand += "INSERT INTO ServiceLevelGroup_map(lgm_company_code " +
                                                                 ", lgm_branch_code " +
                                                                ", lgm_log_branch_code " +
                                                                ", lgm_state_code " +
                                                                ", lgm_doc_month " +
                                                                ", lgm_group_ecode " +
                                                                ", lgm_source_ecode " +
                                                                ", lgm_group_name " +
                                                                ", lgm_user_id " +
                                                                ", lgm_phone " +
                                                                ", lgm_address)" +
                                                                " VALUES " +
                                                                "('" + CommonData.CompanyCode +
                                                                "', '" + cbBranches.SelectedValue.ToString() +
                                                                "', '" + strLogBRCode +
                                                                "', '" + cbStates.SelectedValue.ToString() +
                                                                "', '" + CommonData.DocMonth.ToString().ToUpper() +
                                                                "', " + strArrDestCode[0].ToString() +
                                                                ", " + strArrSourceCode[0].ToString() +
                                                                ",'" + strCamp +
                                                                "','" + CommonData.LogUserId.ToString() +
                                                                "', '" + txtPhone.Text.ToString().Trim() +
                                                                "','" + txtAddress.Text.ToString().Trim() + "') ";
                    }
                }
            }


            return strCommand;
        }


        private bool CheckDuplicateGroupName()
        {
            bool blOk = true;
            string strSQL = string.Empty;
            string[] strGroupECode;
            if (clbDestination.SelectedIndex > -1)
                strGroupECode = ((NewCheckboxListItem)(clbDestination.SelectedItem)).Tag.ToString().Split('~');
            else
                strGroupECode = gvMappedGroups.Rows[intGridRow].Cells["ecode"].Value.ToString().Split('~');

            if (isModify == "NO")
            {
                strSQL = " SELECT  lgm_source_ecode FROM ServiceLevelGroup_map " +
                            " WHERE lgm_company_code='" + CommonData.CompanyCode +
                            "' AND lgm_branch_code='" + CommonData.BranchCode +
                            "' AND lgm_doc_month='" + CommonData.DocMonth +
                            "' AND upper(lgm_group_name)='" + cbGroupCamp.Text.ToString() + "'";
            }

            if (isModify == "YES")
            {
                strSQL = " SELECT  lgm_source_ecode FROM ServiceLevelGroup_map " +
                            " WHERE lgm_company_code='" + CommonData.CompanyCode +
                            "' AND lgm_branch_code='" + CommonData.BranchCode +
                            "' AND lgm_doc_month='" + CommonData.DocMonth +
                            "' AND upper(lgm_group_name)='" + cbGroupCamp.Text.ToString() +
                            "' AND lgm_group_ecode <> " + strGroupECode[0];
            }

            objSQLdb = new SQLDB();
            cbLevels.DataSource = null;
            cbLevels.Items.Clear();
            DataTable dt = objSQLdb.ExecuteDataSet(strSQL).Tables[0];

            if (dt.Rows.Count == 0)
                blOk = false;

            return blOk;
        }

        private void FillMappedDataToGrid()
        {
            objServicedb = new ServiceDB();
            string strLogBcode = string.Empty;
            gvMappedGroups.Rows.Clear();
            DataTable dt = objServicedb.LevelServiceMappedGroupList_Get(CommonData.CompanyCode, CommonData.BranchCode, CommonData.StateCode).Tables[0];
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

                    DataGridViewCell cellGroupPhone = new DataGridViewTextBoxCell();
                    cellGroupPhone.Value = dt.Rows[i]["Phone"];
                    tempRow.Cells.Add(cellGroupPhone);

                    DataGridViewCell cellGroupAddress = new DataGridViewTextBoxCell();
                    cellGroupAddress.Value = dt.Rows[i]["Address"];
                    tempRow.Cells.Add(cellGroupAddress);

                    gvMappedGroups.Rows.Add(tempRow);

                }
            }
        

        }

        private void FillGroupMappedSourceDataGrid(int intRow)
        {
            objServicedb = new ServiceDB();
            DataTable dt = new DataTable();
            string strLogBcode = string.Empty;
            string strLoadedEcode = string.Empty;
            string[] strLvlEcode = gvMappedGroups.Rows[intRow].Cells["ecode"].Value.ToString().Split('~');
            cbGroupCamp.Text = gvMappedGroups.Rows[intRow].Cells["GroupName"].Value.ToString();
            clbSource.Items.Clear();

            try
            {
                dt = objServicedb.ServiceLevelGroupEcodeMapped_Get(CommonData.CompanyCode, cbBranches.SelectedValue.ToString(), cbStates.SelectedValue.ToString(), Convert.ToInt32(strLvlEcode[1]), Convert.ToInt32(strLvlEcode[0]), strLogBcode).Tables[0];
                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dt.Rows)
                    {
                        if (dataRow["ENAME"].ToString().Trim().Length > 0)
                        {
                            if (dataRow["mapped"].ToString() == "999999")
                            {
                               
                                NewCheckboxListItem oclBox = new NewCheckboxListItem();
                                oclBox.Tag = dataRow["ECODE"].ToString();
                                oclBox.Text = dataRow["ENAME"].ToString();
                                clbSource.Items.Add(oclBox);
                                oclBox = null;
                            }
                            else
                            {
                                if (strLvlEcode[0].IndexOf(dataRow["ECODE"].ToString().Substring(0, dataRow["ECODE"].ToString().IndexOf("~"))) < 0)
                                {
                                    
                                    NewCheckboxListItem oclBox = new NewCheckboxListItem();
                                    oclBox.Tag = dataRow["ECODE"].ToString();
                                    oclBox.Text = dataRow["ENAME"].ToString();
                                    if (!strLoadedEcode.Contains(dataRow["ECODE"].ToString()))
                                        clbSource.Items.Add(oclBox, CheckState.Checked);

                                    strLoadedEcode += dataRow["ECODE"].ToString() + ",";

                                    //clbSource.Items.Add(oclBox, CheckState.Checked);
                                    oclBox = null;
                                }
                            }
                        }
                       
                    }

                }
                dt = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                objServicedb = null;
            }
        }

        private void gvMappedGroups_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string[] strLvlEcode = gvMappedGroups.Rows[e.RowIndex].Cells["ecode"].Value.ToString().Split('~');

                if (cbStates.SelectedIndex > -1 && cbBranches.SelectedIndex > -1)
                {
                    intGridRow = e.RowIndex;
                    clbSource.Items.Clear();
                    clbDestination.Items.Clear();


                    cbGroupCamp.Text = gvMappedGroups.Rows[e.RowIndex].Cells["GroupName"].Value.ToString();
                    isModify = "YES";
                    FillGroupMappedSourceDataGrid(e.RowIndex);
                    rdbGroup.Checked = true;

                }
                if (cbLogcalBranch.Items.Count != 0)
                {
                    
                    if (gvMappedGroups.Rows[e.RowIndex].Cells["logBranchCode"].Value.ToString() == "                    ")
                        cbLogcalBranch.SelectedIndex = 0;
                    else
                    {
                        cbLogcalBranch.SelectedValue = gvMappedGroups.Rows[e.RowIndex].Cells["logBranchCode"].Value.ToString();
                       
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //int intSave = 0;
            //if (CheckDeleteCondition())
            //{
            //    DialogResult result = MessageBox.Show("Do you want to delete mapped data ?",
            //                            "Source to Destination Data", MessageBoxButtons.YesNo);
            //    if (result == DialogResult.Yes)
            //        intSave = DeleteMappedData();
            //    MessageBox.Show("Mapped Data Deleted Sucessfully","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
            //}

            //if (intSave > 0)
            //{
            //    txtAddress.Text = "";
            //    txtPhone.Text = "";
            //    cbGroupCamp.SelectedIndex = 0;
            //    clbSource.Items.Clear();
            //    clbDestination.Items.Clear();
            //    cbLevels.SelectedIndex = 0;
            //    FillMappedDataToGrid();
            //}
            //intGridRow = -1;
        }

        private bool CheckDeleteCondition()
        {
            bool blCheck = true;
            if (cbStates.SelectedIndex == -1)
            {
                MessageBox.Show("Select state", "Group Mapping", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blCheck = false;
                cbStates.Focus();
                return blCheck;
            }
            if (cbBranches.SelectedIndex == -1)
            {
                MessageBox.Show("Select branch", "Group Mapping", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blCheck = false;
                cbBranches.Focus();
                return blCheck;
            }
            if (cbLevels.SelectedIndex <= 0 && intGridRow == -1)
            {
                MessageBox.Show("Select level", "Group Mapping", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blCheck = false;
                cbLevels.Focus();
                return blCheck;
            }
            bool blSource = false;
            for (int i = 0; i < clbDestination.Items.Count; i++)
            {
                if (clbDestination.GetItemCheckState(i) == CheckState.Checked)
                {
                    blSource = true;
                }
            }
            if (intGridRow == -1)
            {
                if (clbDestination.Items.Count == 0 || blSource == false)
                {
                    MessageBox.Show("Select Destination atleast one", "Group Mapping", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    blCheck = false;
                    clbDestination.Focus();
                    return blCheck;
                }
            }
            return blCheck;
        }

        private string GetSelectedEcodesToCheck()
        {
            string strEcodes = string.Empty;
            if (clbSource.Items.Count > 0)
            {


                for (int i = 0; i < clbSource.Items.Count; i++)
                {
                    if (clbSource.GetItemCheckState(i) == CheckState.Checked)
                    {
                        string[] strCodes = ((NewCheckboxListItem)(clbSource.Items[i])).Tag.ToString().Split('~');

                        strEcodes += strCodes[0].ToString() + ",";

                    }
                }
            }
            if (strEcodes.Length > 2)
                strEcodes = strEcodes.Substring(0, strEcodes.Length - 1);
            return strEcodes;
        }

        private int DeleteMappedData()
        {

            string[] strArrDestCode = null;
            int nDelete = 0;
            objSQLdb = new SQLDB();
            try
            {
                string strSCodes = string.Empty;
                if (clbDestination.SelectedIndex > -1)
                    strSCodes = ((NewCheckboxListItem)(clbDestination.SelectedItem)).Tag;
                else
                    strSCodes = gvMappedGroups.Rows[intGridRow].Cells["ecode"].Value.ToString();

                strArrDestCode = strSCodes.Split('~');
                string strMapEcodes = GetSelectedEcodesToCheck();
                strMapEcodes += strMapEcodes + "," + strArrDestCode[0];

                //strMapEcodes = ChecckDeleteCondition(strMapEcodes);
                if (strMapEcodes.Length > 0)
                {
                    string strDelete = " DELETE FROM ServiceLevelGroup_map " +
                           " WHERE lgm_company_code='" + CommonData.CompanyCode +
                           "' AND lgm_branch_code='" + CommonData.BranchCode +
                           "' AND lgm_doc_month='" + CommonData.DocMonth +
                           "' AND lgm_group_ecode=" + strArrDestCode[0].ToString();

                    nDelete = objSQLdb.ExecuteSaveData(strDelete);
                }
                else
                {
                    MessageBox.Show("Invoice made against " + strMapEcodes + " Ecode.", "Group mapping", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Data is not deleted.");
            }
            finally
            {
                objSQLdb = null;
            }
            return nDelete;
        }

        private void txtAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
            if (char.IsLetterOrDigit(e.KeyChar) == false)
                e.Handled = true;
            e.KeyChar = Char.ToUpper(e.KeyChar);

            if (e.KeyChar == 8)
                e.Handled = false;
            if (e.KeyChar == 32)
                e.Handled = false;
        }

        private void lbMapp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
       
    }
}
