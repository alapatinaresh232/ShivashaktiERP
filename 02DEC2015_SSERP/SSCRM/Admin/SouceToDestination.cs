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
using DataBoundTreeView;

namespace SSCRM
{
    
    public partial class SouceToDestination : Form
    {
        private StaffLevel objData = null;
        private SQLDB objDA = null;
        private int intGridRow = -1;
        private string isModify = "NO";

        public SouceToDestination()
        {
            InitializeComponent();
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

        private void SouceToDestination_Load(object sender, EventArgs e)
        {
            lblDocMonth.Text = CommonData.DocMonth.ToString().ToUpper();
            FillStateComboBox();
            FillStafflevelComboBox();
            rdbGroup.Checked = true;
            FillMappList();
            fillGridMappedData();
            FillLogicalBranchComboBox();
            FillCampComboBox();
            if (CommonData.LogUserId.ToUpper() == "ADMIN" || CommonData.LogUserRole == "MANAGEMENT")
            {
                btnUnlock.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                btnUnlock.Enabled = false;
                btnDelete.Enabled = false;
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
                MessageBox.Show(ex.Message, "Source To Destination", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                objData = null;
            }
        }

        private void cbBranches_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbStates.SelectedIndex > 0 && cbBranches.SelectedIndex > -1)
            {
                clbSource.Items.Clear();
                clbDestination.Items.Clear();
            }
        }

        private void cbLevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStates.SelectedIndex > 0 && cbBranches.SelectedIndex > -1 && cbLevels.SelectedIndex > 0)
            {
                clbSource.Items.Clear();
                clbDestination.Items.Clear();
                FillDestinationData(cbStates.SelectedValue.ToString(), cbBranches.SelectedValue.ToString(), Convert.ToInt16(cbLevels.SelectedValue.ToString()));
                removeExistedGroupEcode();
                if(cbGroupCamp.Items.Count != 0)
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
            finally
            {
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int intSave = 0;
            try
            {
                if (CheckCondition())
                {
                    intSave = SaveMappedEmployee();
                }

                if (intSave > 0)
                {
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
                    if(cbLogcalBranch.SelectedIndex>=0)
                        cbLogcalBranch.SelectedIndex = 0;
                }
                FillStafflevelComboBox();
            }
            catch (Exception ex)
            {
                isModify = "NO";
                MessageBox.Show(ex.Message);
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
                    FillGroupMappedSourceData(cbStates.SelectedValue.ToString(), cbBranches.SelectedValue.ToString());
                else if (rdbOthers.Checked == true)
                    FillGroupMappedOtherSourceData(cbStates.SelectedValue.ToString(), cbBranches.SelectedValue.ToString());
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbLevels.SelectedIndex = 0;
            if(cbGroupCamp.Items.Count != 0)
                cbGroupCamp.SelectedIndex = 0;
            txtAddress.Text = "";
            txtPhone.Text = "";
            clbSource.Items.Clear();
            clbDestination.Items.Clear();
            fillGridMappedData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int intSave = 0;
            if (CheckDeleteCondition())
            {
                DialogResult result = MessageBox.Show("Do you want to delete mapped data ?",
                                        "Source to Destination Data", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                    intSave = DeleteMappedData();
            }

            if (intSave > 0)
            {
                txtAddress.Text = "";
                txtPhone.Text = "";
                cbGroupCamp.SelectedIndex = 0;
                clbSource.Items.Clear();
                clbDestination.Items.Clear();
                cbLevels.SelectedIndex = 0;
                fillGridMappedData();
            }
            intGridRow = -1;
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
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
            }
            finally
            {
               
                objData = null;
            }
        }

        private void FillStafflevelComboBox()
        {
            objData = new StaffLevel();
            DataTable dt = new DataTable();
            try
            {
                dt = objData.GetStaffLevelsDS(CommonData.CompanyCode,CommonData.BranchType).Tables[0];
                if (dt.Rows.Count > 1)
                {
                    cbLevels.DataSource = dt;
                    cbLevels.DisplayMember = "ldm_Designations";
                    cbLevels.ValueMember = "ldm_elevel_id";
                }
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
                dr[0] = "Select";
                dr[1] = "Select";
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

        private void FillGroupMappedOtherSourceDataGrid(int intRow)
        {
            objData = new StaffLevel();
            string strLogBcode = string.Empty;
            string strLoadedEcode = string.Empty;
            string[] strLvlEcode = gvMappedGroups.Rows[intRow].Cells["ecode"].Value.ToString().Split('~');
            cbGroupCamp.Text = gvMappedGroups.Rows[intRow].Cells["GroupName"].Value.ToString();
            try
            {
                DataTable dt = objData.LevelGroupOtherEcodeMapped_Get(CommonData.CompanyCode, cbStates.SelectedValue.ToString(), cbBranches.SelectedValue.ToString(), Convert.ToInt32(strLvlEcode[1]), Convert.ToInt32(strLvlEcode[0]), strLogBcode).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        if (dataRow["ENAME"].ToString().Trim().Length > 0)
                        {
                            if (dataRow["mapped"].ToString() == "999999")
                            {
                                if (dataRow["EORA"].ToString() == "E" || dataRow["EORA"].ToString() == "A")
                                {
                                    NewCheckboxListItem oclBox = new NewCheckboxListItem();
                                    oclBox.Tag = dataRow["ECODE"].ToString();
                                    oclBox.Text = dataRow["ENAME"].ToString();
                                    clbSource.Items.Add(oclBox);
                                    oclBox = null;
                                }
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
                        //if (idx == 116)
                        //    idx = 116;
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

                objData = null;
            }
        }
        
        private void FillGroupMappedOtherSourceData(string strSCode, string strBCode)
        {
            objData = new StaffLevel();
            string strLogBcode = string.Empty;
            string strLoadedEcode = string.Empty;
            string[] strDestCode = ((NewCheckboxListItem)(clbDestination.SelectedItem)).Tag.Split('~');
            try
            {
                DataTable dt = objData.LevelGroupOtherEcodeMapped_Get(CommonData.CompanyCode, strSCode, strBCode, Convert.ToInt32(cbLevels.SelectedValue.ToString()), Convert.ToInt32(strDestCode[0]), strLogBcode).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    cbGroupCamp.Text = dt.Rows[0]["GroupName"] + "";
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        if (dataRow["ENAME"].ToString().Trim().Length > 0)
                        {
                            if (dataRow["mapped"].ToString() == "999999")
                            {
                                if (dataRow["EORA"].ToString() == "E" || dataRow["EORA"].ToString() == "A")
                                {
                                    NewCheckboxListItem oclBox = new NewCheckboxListItem();
                                    oclBox.Tag = dataRow["ECODE"].ToString();
                                    oclBox.Text = dataRow["ENAME"].ToString();
                                    clbSource.Items.Add(oclBox);
                                    oclBox = null;
                                }
                            }
                            else
                            {
                                if (strDestCode[0].IndexOf(dataRow["ECODE"].ToString().Substring(0, dataRow["ECODE"].ToString().IndexOf("~"))) < 0)
                                {
                                    NewCheckboxListItem oclBox = new NewCheckboxListItem();
                                    oclBox.Tag = dataRow["ECODE"].ToString();
                                    oclBox.Text = dataRow["ENAME"].ToString();
                                    if (!strLoadedEcode.Contains(dataRow["ECODE"].ToString()))
                                        clbSource.Items.Add(oclBox, CheckState.Checked);

                                    strLoadedEcode += dataRow["ECODE"].ToString() + ",";
                                    oclBox = null;
                                }
                            }
                        }
                        //if (idx == 116)
                        //    idx = 116;
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

                objData = null;
            }
        }

        private void FillGroupMappedSourceDataGrid(int intRow)
        {
            objData = new StaffLevel();
            string strLogBcode = string.Empty;
            string strLoadedEcode = string.Empty;
            string[] strLvlEcode = gvMappedGroups.Rows[intRow].Cells["ecode"].Value.ToString().Split('~');
            cbGroupCamp.Text = gvMappedGroups.Rows[intRow].Cells["GroupName"].Value.ToString();
            txtPhone.Text = gvMappedGroups.Rows[intRow].Cells["Phone"].Value.ToString();
            txtAddress.Text = gvMappedGroups.Rows[intRow].Cells["Address"].Value.ToString();
            try
            {
                DataTable dt = objData.LevelGroupEcodeMapped_Get(CommonData.CompanyCode, cbStates.SelectedValue.ToString(), cbBranches.SelectedValue.ToString(), Convert.ToInt32(strLvlEcode[1]), Convert.ToInt32(strLvlEcode[0]), strLogBcode).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        if (dataRow["mapped"].ToString() == "999999")
                        {
                            if (dataRow["EORA"].ToString() == "E" || dataRow["EORA"].ToString() == "A")
                            {
                                NewCheckboxListItem oclBox = new NewCheckboxListItem();
                                oclBox.Tag = dataRow["ECODE"].ToString();
                                oclBox.Text = dataRow["ENAME"].ToString();
                                clbSource.Items.Add(oclBox);
                                oclBox = null;
                            }
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
                                oclBox = null;
                            }
                        }

                        //if (idx == 116)
                        //    idx = 116;
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

                objData = null;
            }
        }

        private void FillGroupMappedSourceData(string strSCode, string strBCode)
        {
            objData = new StaffLevel();
            string strLogBcode = string.Empty;
            string strLoadedEcode = string.Empty;
            string[] strDestCode = ((NewCheckboxListItem)(clbDestination.SelectedItem)).Tag.Split('~');
            try
            {
                DataTable dt = objData.LevelGroupEcodeMapped_Get(CommonData.CompanyCode, strSCode, strBCode, Convert.ToInt32(cbLevels.SelectedValue.ToString()), Convert.ToInt32(strDestCode[0]), strLogBcode).Tables[0];
                if (dt.Rows.Count > 0)
                {
                   
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        if (dataRow["mapped"].ToString() == "999999")
                        {
                            if (dataRow["EORA"].ToString() == "E" || dataRow["EORA"].ToString() == "A")
                            {
                                NewCheckboxListItem oclBox = new NewCheckboxListItem();
                                oclBox.Tag = dataRow["ECODE"].ToString();
                                oclBox.Text = dataRow["ENAME"].ToString();
                                clbSource.Items.Add(oclBox);
                                oclBox = null;
                            }
                        }
                        else
                        {
                            if (strDestCode[0].IndexOf(dataRow["ECODE"].ToString().Substring(0, dataRow["ECODE"].ToString().IndexOf("~"))) < 0)
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

                        //if (idx == 116)
                        //    idx = 116;
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

                objData = null;
            }
        }

        private void FillAboveGroupMapped(string strSCode, string strBCode)
        {
            objData = new StaffLevel();
            string strLogBcode = string.Empty;
            string[] strDestCode = ((NewCheckboxListItem)(clbDestination.SelectedItem)).Tag.Split('~');
            try
            {
                DataTable dt = objData.LevelAboveEcodeGroupMapped_Get(CommonData.CompanyCode, strSCode, strBCode, Convert.ToInt32(cbLevels.SelectedValue.ToString()), Convert.ToInt32(strDestCode[0]), strLogBcode).Tables[0];
                if (dt.Rows.Count > 1)
                {
                    foreach (DataRow dataRow in dt.Rows)
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
                            if (strDestCode[0].IndexOf(dataRow["ECODE"].ToString().Substring(0, dataRow["ECODE"].ToString().IndexOf("~"))) < 0)
                            {
                                NewCheckboxListItem oclBox = new NewCheckboxListItem();
                                oclBox.Tag = dataRow["ECODE"].ToString();
                                oclBox.Text = dataRow["ENAME"].ToString();
                                clbSource.Items.Add(oclBox, CheckState.Checked);
                                oclBox = null;
                            }
                        }

                        //if (idx == 116)
                        //    idx = 116;
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

                objData = null;
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

        private void FillSourceData(string strSCode, string strBCode)
        {
            objData = new StaffLevel();
            string strLogBcode = string.Empty;
            string[] strDestCode = ((NewCheckboxListItem)(clbDestination.SelectedItem)).Tag.Split('~'); 
            try
            {
                DataTable dt = objData.GetMappedSourceDS(CommonData.CompanyCode, strSCode, strBCode, Convert.ToInt32(cbLevels.SelectedValue.ToString()), Convert.ToInt32(strDestCode[0]), strLogBcode).Tables[0];
                if (dt.Rows.Count > 1)
                {
                    foreach (DataRow dataRow in dt.Rows)
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
                            if (strDestCode[0].IndexOf(dataRow["ECODE"].ToString().Substring(0, dataRow["ECODE"].ToString().IndexOf("~"))) < 0)
                            {
                                NewCheckboxListItem oclBox = new NewCheckboxListItem();
                                oclBox.Tag = dataRow["ECODE"].ToString();
                                oclBox.Text = dataRow["ENAME"].ToString();
                                clbSource.Items.Add(oclBox, CheckState.Checked);
                                oclBox = null;
                            }
                         }
                      
                        //if (idx == 116)
                        //    idx = 116;
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

                objData = null;
            }
        }

        private void FillMappList()
        {
            objData = new StaffLevel();
            char cMapped = 'T';
            Hashtable ht = new Hashtable();
            DataSet ds = new DataSet();
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

                DataTable dt = objData.GetMappedUnmappedListDS(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth, cMapped).Tables[0];
                if (dt.Rows.Count > 1)
                {
                    lbMapp.DataSource = dt;
                    lbMapp.DisplayMember = "ENAME";
                    lbMapp.ValueMember = "ECODE";
                }
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

        private void FillGroupSourceData(string strSCode, string strBCode, int nLevelId)
        {
            objData = new StaffLevel();
            DataTable dt = new DataTable();
            try
            {
                dt = objData.LevelGroupEcode_Get(CommonData.CompanyCode, CommonData.StateCode, CommonData.BranchCode, nLevelId).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = dataRow["ECODE"].ToString();
                        oclBox.Text = dataRow["ENAME"].ToString();
                        clbSource.Items.Add(oclBox);
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
                objData = null;
            }
        }

        private void FillDestinationData(string strSCode, string strBCode,int nLevelId)
        {
            objData = new StaffLevel();
            DataTable dt = new DataTable();
            string strLoadedEcode = string.Empty;
            try
            {
                dt = objData.GetLevelEcodes(CommonData.CompanyCode, CommonData.StateCode, CommonData.BranchCode, nLevelId).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = dataRow["ECODE"].ToString();
                        oclBox.Text = dataRow["ENAME"].ToString();
                        string tempStr = "#"+dataRow["ECODE"].ToString()+"@";
                        if (!strLoadedEcode.Contains(tempStr))
                            clbDestination.Items.Add(oclBox);

                        strLoadedEcode += tempStr;
                        //clbDestination.Items.Add(oclBox);
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
                objData = null;
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
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                isModify = "NO";
                fillGridMappedData();
                intGridRow = -1;
                objDA = null;
            }
            return rec;
        }

        private string GetSelectedEcodes()
        {
            string[] strArrDestCode = null;
            string[] strArrSourceCode = null;
            string strCamp = "";
            StringBuilder sbSQL = new StringBuilder();
            string strSQL = string.Empty;
            objDA = new SQLDB();
            if (clbSource.Items.Count > 0)
            {
                string strSCodes =string.Empty;
                if (clbDestination.SelectedIndex>-1)
                    strSCodes = ((NewCheckboxListItem)(clbDestination.SelectedItem)).Tag;
                else
                    strSCodes = gvMappedGroups.Rows[intGridRow].Cells["ecode"].Value.ToString();

                strArrDestCode = strSCodes.Split('~');
                string strDelele = " DELETE FROM LevelGroup_map " +
                        " WHERE lgm_company_code='" + CommonData.CompanyCode.ToString() +
                        "' AND lgm_branch_code='" + cbBranches.SelectedValue.ToString() +
                        //"' AND lgm_state_code='" + cbStates.SelectedValue.ToString() +
                        "' AND Upper(lgm_doc_month)='" + CommonData.DocMonth.ToString().ToUpper() +
                        "' AND lgm_group_ecode=" + strArrDestCode[0].ToString();

                    int intRec = objDA.ExecuteSaveData(strDelele);

                    string strLogBRCode = string.Empty;
                    if (cbLogcalBranch.SelectedIndex > 0)
                        strLogBRCode = cbLogcalBranch.SelectedValue.ToString();


                    if (rdbOthers.Checked == true)
                        strCamp = "OFFICE SALES";
                    else
                        strCamp = cbGroupCamp.Text.ToString();

                    for (int i = 0; i < clbSource.Items.Count; i++)
                    {
                        if (clbSource.GetItemCheckState(i) == CheckState.Checked)
                        {
                            string strDCodes = ((NewCheckboxListItem)(clbSource.Items[i])).Tag;
                            strArrSourceCode = strDCodes.Split('~');


                            sbSQL.Append(" INSERT INTO LevelGroup_map(lgm_company_code, lgm_branch_code, lgm_log_branch_code, lgm_state_code, lgm_doc_month" +
                                        ", lgm_group_ecode, lgm_source_ecode, lgm_gl_ecode, lgm_sr_ecode, lgm_group_name, lgm_user_id, lgm_phone, lgm_address, lgm_created_date)" +
                                        " VALUES('" + CommonData.CompanyCode + "', '" + cbBranches.SelectedValue.ToString() +
                                        "', '" + strLogBRCode + "', '" + cbStates.SelectedValue.ToString() +
                                        "', '" + CommonData.DocMonth.ToString().ToUpper() + "', " + strArrDestCode[0].ToString() +
                                        ", " + strArrSourceCode[0].ToString() + "," + strArrDestCode[0].ToString() +
                                        ", " + strArrSourceCode[0].ToString() + ",'" + strCamp + "','" + CommonData.LogUserId.ToString() +
                                        "', '" + txtPhone.Text.ToString().Trim() + "','" + txtAddress.Text.ToString().Trim() + 
                                        "', getdate());");

                        }
                        
                    }
                    if (sbSQL.ToString() == "")
                    {
                        sbSQL.Append(" INSERT INTO LevelGroup_map(lgm_company_code, lgm_branch_code, lgm_log_branch_code, lgm_state_code, lgm_doc_month" +
                                        ", lgm_group_ecode, lgm_source_ecode, lgm_gl_ecode, lgm_sr_ecode, lgm_group_name, lgm_user_id, lgm_phone, lgm_address, lgm_created_date)" +
                                        " VALUES('" + CommonData.CompanyCode + "', '" + cbBranches.SelectedValue.ToString() +
                                        "', '" + strLogBRCode + "', '" + cbStates.SelectedValue.ToString() +
                                        "', '" + CommonData.DocMonth.ToString().ToUpper() + "', " + strArrDestCode[0].ToString() +
                                        ", " + strArrDestCode[0].ToString() + "," + strArrDestCode[0].ToString() +
                                        ", " + strArrDestCode[0].ToString() + ",'" + strCamp + "','" + CommonData.LogUserId.ToString() +
                                        "', '" + txtPhone.Text.ToString().Trim() + "','" + txtAddress.Text.ToString().Trim() +
                                        "', getdate());");
                    }

                    sbSQL.Append(" UPDATE LevelGroup_map SET lgm_sr_ecode = HAAM_EMP_CODE "+
                                    "FROM HR_APPL_A2E_MIGRATION WHERE HAAM_AGENT_CODE = lgm_source_ecode "+
                                    "AND lgm_doc_month =  '" + CommonData.DocMonth.ToString().ToUpper() + "' " +
                                    "AND lgm_branch_code = '" + cbBranches.SelectedValue.ToString() + "' " +
                                    "AND lgm_group_ecode = '" + strArrDestCode[0].ToString() + "' ");

                    sbSQL.Append(" UPDATE LevelGroup_map SET lgm_gl_ecode  = HAAM_EMP_CODE " +
                                    "FROM HR_APPL_A2E_MIGRATION WHERE HAAM_AGENT_CODE = lgm_group_ecode " +
                                    "AND lgm_doc_month =  '" + CommonData.DocMonth.ToString().ToUpper() + "' " +
                                    "AND lgm_branch_code = '" + cbBranches.SelectedValue.ToString() + "' " +
                                    "AND lgm_group_ecode = '" + strArrDestCode[0].ToString() + "' ");

                
            }
            if (strSQL == "")
            {
                strSQL = sbSQL.ToString().Substring(0, sbSQL.ToString().Length - 1);
            }
            return strSQL;
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

                        strEcodes += strCodes[0].ToString()+",";

                    }
                }
            }
            if (strEcodes.Length>2)
                strEcodes = strEcodes.Substring(0, strEcodes.Length - 1);
            return strEcodes;
        }

        private bool CheckCondition()
        {
            bool blCheck = true;
            string strSqlText = "select count(*) FROM LevelGroup_map WHERE lgm_Locking='L' and lgm_branch_code = '" + CommonData.BranchCode + "' and lgm_doc_month = '" + CommonData.DocMonth + "'";
            string sStatus = "";
            SQLDB ObjDB = new SQLDB();
            //try
            //{
            //    sStatus = ObjDB.ExecuteDataSet(strSqlText).Tables[0].Rows[0][0].ToString();
            //    if (Convert.ToInt32(sStatus) > 0)
            //    {
            //        MessageBox.Show("Camp to SR Mapping Locked \n Please Contact your Manager to Unlock", "Group Mapping", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        blCheck = false;
            //        //clbSource.Focus();
            //        return blCheck;
            //    }
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //    return false;
                
            //}
            //finally
            //{
            //    ObjDB = null;
            //}
            
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
            if (cbLevels.SelectedIndex <= 0 && intGridRow ==-1)
            {
                MessageBox.Show("Select level");
                blCheck = false;
                cbLevels.Focus();
                return blCheck;
            }
            if (rdbGroup.Checked == true)
            {
                if (cbGroupCamp.SelectedIndex<=0)
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
            for(int i=0;i<clbSource.Items.Count;i++)
            {
                if (clbSource.GetItemCheckState(i) == CheckState.Checked)
                {
                    blSource = true;
                    break;
                }
            }

            if (clbSource.Items.Count == 0 || blSource == false)
            {
                MessageBox.Show("Select source atleast one","Group Mapping", MessageBoxButtons.OK, MessageBoxIcon.Information);
                blCheck = false;
                clbSource.Focus();
                return blCheck;
            }
            string strSCodes = string.Empty;
            try
            {
                if (clbDestination.SelectedIndex > -1)
                    strSCodes = ((NewCheckboxListItem)(clbDestination.SelectedItem)).Tag;
                else
                    strSCodes = gvMappedGroups.Rows[intGridRow].Cells["ecode"].Value.ToString();
            }
            catch { strSCodes = "0"; }
            string[] sGCodes = null;
            sGCodes = strSCodes.Split('~');
            strSqlText = " SELECT SSBH_EORA_CODE, SSBH_DOC_MONTH, SSBH_LOCKING " +
                                "FROM SALES_SUMMARY_BULLETINS WHERE SSBH_EORA_CODE = " + sGCodes[0] +
                                " AND SSBH_DOC_MONTH='" + CommonData.DocMonth + "'";
            sStatus = "";
            ObjDB = new SQLDB();
            try
            {
                sStatus = ObjDB.ExecuteDataSet(strSqlText).Tables[0].Rows[0]["SSBH_LOCKING"].ToString();
            }
            catch
            {
                sStatus = "N";
            }
            finally
            {
                ObjDB = null;
            }
            if (sStatus == "Y")
            {
                MessageBox.Show(" Selected Group is Locked \n Please Contact your Manager to Unlock", "Group Mapping", MessageBoxButtons.OK, MessageBoxIcon.Information);
                blCheck = false;
                //clbSource.Focus();
                return blCheck;
            }
            
            return blCheck;
        }

        private bool CheckDeleteCondition()
        {
            bool blCheck = true;

            string strSqlText = "select count(*) FROM LevelGroup_map WHERE lgm_Locking='L' and lgm_branch_code = '" + CommonData.BranchCode + "' and lgm_doc_month = '" + CommonData.DocMonth + "'";
            string sStatus = "";
            SQLDB ObjDB = new SQLDB();
            try
            {
                sStatus = ObjDB.ExecuteDataSet(strSqlText).Tables[0].Rows[0][0].ToString();
            }
            catch
            {
                sStatus = "0";
            }
            finally
            {
                ObjDB = null;
            }
            if (Convert.ToInt32(sStatus) > 0)
            {
                MessageBox.Show("Camp to SR Mapping Locked \n Please Contact your Manager to Unlock", "Group Mapping", MessageBoxButtons.OK, MessageBoxIcon.Information);
                blCheck = false;
                //clbSource.Focus();
                return blCheck;
            }

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
            if (cbLevels.SelectedIndex <= 0 && intGridRow ==-1)
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

        private int DeleteMappedData()
        {
           
            string[] strArrDestCode = null;
            int nDelete = 0;
            objDA = new SQLDB();
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
                if (strMapEcodes.Length>0)
                {
                    string strDelete = " DELETE FROM LevelGroup_map " +
                           " WHERE lgm_company_code='" + CommonData.CompanyCode +
                           "' AND lgm_branch_code='" + CommonData.BranchCode +
                           "' AND lgm_doc_month='" + CommonData.DocMonth +
                           "' AND lgm_group_ecode=" + strArrDestCode[0].ToString();
                   
                    nDelete = objDA.ExecuteSaveData(strDelete);
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
                objDA = null;
            }
            return nDelete;
        }

        private string ChecckDeleteCondition(string sEcode)
        {
           string strVal = string.Empty;
            objData = new StaffLevel();
            strVal = objData.CheckEcodeUsedInInvoice(sEcode);
            objData = null;
            return strVal; 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void chkMapp_CheckStateChanged(object sender, EventArgs e)
        {
            FillMappList();
        }

        private void rdbGroup_Click(object sender, EventArgs e)
        {
            clbSource.Items.Clear();
            clbDestination.Items.Clear();
            cbGroupCamp.SelectedIndex = 0;
            txtAddress.Text = "";
            txtPhone.Text = "";
            cbGroupCamp.Enabled = true;
            cbGroupCamp.Focus();
            FillStafflevelComboBox();
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            SearchEcode(txtSearch.Text.ToString(), clbSource);
        }

        private void txtDsearch_KeyUp(object sender, KeyEventArgs e)
        {
            SearchEcode(txtDsearch.Text.ToString(), clbDestination);
        }

        private void txtMsearch_KeyUp(object sender, KeyEventArgs e)
        {
            SearchEcode(txtMsearch.Text.ToString(), lbMapp);
        }

        private bool CheckGroupMapped()
        {
            bool blOk = true;
            string strSQL = " SELECT  lgm_source_ecode FROM LevelGroup_map " +
                            " WHERE lgm_company_code='" + CommonData.CompanyCode +
                            "' AND lgm_branch_code='" + CommonData.BranchCode +
                            "' AND lgm_doc_month='" + CommonData.DocMonth + "'";
            objDA = new SQLDB();
            cbLevels.DataSource = null;
            cbLevels.Items.Clear();
            DataTable dt = objDA.ExecuteDataSet(strSQL).Tables[0];

            if (dt.Rows.Count == 0)
                blOk = false;

            return blOk;
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
                strSQL = " SELECT  lgm_source_ecode FROM LevelGroup_map " +
                            " WHERE lgm_company_code='" + CommonData.CompanyCode +
                            "' AND lgm_branch_code='" + CommonData.BranchCode +
                            "' AND lgm_doc_month='"+ CommonData.DocMonth +
                            "' AND upper(lgm_group_name)='" + cbGroupCamp.Text.ToString() + "'";
            }

            if (isModify == "YES")
            {
                strSQL = " SELECT  lgm_source_ecode FROM LevelGroup_map " +
                            " WHERE lgm_company_code='" + CommonData.CompanyCode +
                            "' AND lgm_branch_code='" + CommonData.BranchCode +
                            "' AND lgm_doc_month='" + CommonData.DocMonth +
                            "' AND upper(lgm_group_name)='" + cbGroupCamp.Text.ToString() +
                            "' AND lgm_group_ecode <> " + strGroupECode[0];
            }

            objDA = new SQLDB();
            cbLevels.DataSource = null;
            cbLevels.Items.Clear();
            DataTable dt = objDA.ExecuteDataSet(strSQL).Tables[0];

            if (dt.Rows.Count == 0)
                blOk = false;

            return blOk;
        }

        private void rdbOthers_Click(object sender, EventArgs e)
        {
            clbSource.Items.Clear();
            clbDestination.Items.Clear();
            cbGroupCamp.SelectedIndex = 0;
            txtAddress.Text = "";
            txtPhone.Text = "";
            cbGroupCamp.Enabled = false;
            if (CheckGroupMapped())
                FillStafflevelComboBox();
            else
                MessageBox.Show("No groups. Please create groups.");
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

                    DataGridViewCell cellSlNo = new DataGridViewTextBoxCell();
                    cellSlNo.Value = i + 1;
                    tempRow.Cells.Add(cellSlNo);

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
                    if (gvMappedGroups.Rows[e.RowIndex].Cells["GroupName"].Value.ToString() == "OFFICE SALES")
                    {
                        FillGroupMappedOtherSourceDataGrid(e.RowIndex);
                        rdbOthers.Checked = true;
                    }
                    else
                    {
                        cbGroupCamp.Text = gvMappedGroups.Rows[e.RowIndex].Cells["GroupName"].Value.ToString();
                        isModify = "YES";
                        FillGroupMappedSourceDataGrid(e.RowIndex);
                        rdbGroup.Checked = true;
                    }
                }
                if (cbLogcalBranch.Items.Count != 0)
                {
                    //txtlogicalBranch.Text = "";
                    //string logBranchCode = gvMappedGroups.Rows[e.RowIndex].Cells["logBranchCode"].Value.ToString();
                    if (gvMappedGroups.Rows[e.RowIndex].Cells["logBranchCode"].Value.ToString() == "                    ")
                        cbLogcalBranch.SelectedIndex = 0;
                    else
                    {
                        cbLogcalBranch.SelectedValue = gvMappedGroups.Rows[e.RowIndex].Cells["logBranchCode"].Value.ToString();
                        //txtlogicalBranch.Text = gvMappedGroups.Rows[e.RowIndex].Cells["LogicalBranch"].Value.ToString();
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void clbSource_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Unchecked)
            {
                string[] sEcode = ((SSAdmin.NewCheckboxListItem)(clbSource.Items[e.Index])).Tag.ToString().Split('~');
                if (CommonData.LogUserId.ToUpper() != "ADMIN")
                {
                    if (ChecckDeleteCondition(sEcode[0]).Length > 2)
                    {
                        MessageBox.Show("Invoice made against " + sEcode[0] + " Ecode.", "Group mapping", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        e.NewValue = CheckState.Checked;
                    }
                }
            }
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

        private void label3_Click(object sender, EventArgs e)
        {

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

        private void btnLock_Click(object sender, EventArgs e)
        {
            objDA = new SQLDB();
            int rec = 0;
            string strSQL = string.Empty;
            try
            {
                strSQL = "UPDATE LevelGroup_map SET lgm_Locking = 'L'" +
                            ", lgm_Locked_By='" + CommonData.LogUserId +
                            "', lgm_locked_date=GETDATE() " +
                            "WHERE lgm_branch_code = '" + CommonData.BranchCode +
                            "' AND lgm_doc_month = '" + CommonData.DocMonth + "'";
                if(strSQL.Length>20)
                    rec = objDA.ExecuteSaveData(strSQL);
               
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            { 
                objDA = null;
            }
            if (rec > 0)
            {
                MessageBox.Show("Locked Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            objDA = new SQLDB();
            int rec = 0;
            string strSQL = string.Empty;
            try
            {
                strSQL = "UPDATE LevelGroup_map SET lgm_Locking = 'U'" +
                            ", lgm_unlocked_by='" + CommonData.LogUserId +
                            "', lgm_unlocked_date=GETDATE() " +
                            "WHERE lgm_branch_code = '" + CommonData.BranchCode +
                            "' AND lgm_doc_month = '" + CommonData.DocMonth + "'";
                if (strSQL.Length > 20)
                    rec = objDA.ExecuteSaveData(strSQL);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objDA = null;
            }
            if (rec > 0)
            {
                MessageBox.Show("UnLocked Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        
    }
}
