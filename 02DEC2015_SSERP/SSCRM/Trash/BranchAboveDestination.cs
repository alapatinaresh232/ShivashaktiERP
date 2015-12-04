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
using SSCRM.App_Code;

namespace SSCRM
{
    
    public partial class BranchAboveDestination : Form
    {

        private StaffLevel objData = null;
        private SQLDB objDA = null;
        private int selectedRow = 0;
  
        public BranchAboveDestination()
        {
            InitializeComponent();
        }

        private void BranchAboveDestination_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            lblDocMonth.Text = CommonData.DocMonth.ToString().ToUpper();
            FillCompanyData();
            fillGridMappedData();
           
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
                MessageBox.Show("Data saved", "Camp To BR-Head Above", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                        "Camp To BR-Head Above", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    intSave = DeleteMappedData();
            }

            if (intSave > 0)
            {
                gvMappedGroups.Rows.Clear();
                selectedRow = 0;
            }
        }
        private void FillBranchComboBox(string strStateCode)
        {
            objData = new StaffLevel();
            try
            {
                DataTable dt = objData.LevelCompanyBranches_Proc(cbCompany.SelectedValue.ToString(), "BR").Tables[0];
                cbBranches.DataSource = null;
                cbBranches.Items.Clear();
                DataRow dr = dt.NewRow();
                dr[0] = "Select";
                dr[1] = "Select";
                dt.Rows.InsertAt(dr, 0);
                if (dt.Rows.Count > 0)
                {
                    cbBranches.DataSource = dt;
                    cbBranches.DisplayMember = "branch_name";
                    cbBranches.ValueMember = "branch_code";
                    cbBranches.SelectedValue = CommonData.BranchCode;
                }
                
                dt = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Camp To BR-Head Above", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                objData = null;
            }
        }
        private void FillGroupDetailMappedData(int intRow)
        {
            objData = new StaffLevel();
            string strLogBcode = string.Empty;
            string[] strDestCode = gvMappedGroups.Rows[intRow].Cells["ecode"].Value.ToString().Split('~');
            try
            {
                clbDestination.Items.Clear();
                DataTable dt = objData.LevelCampBanchAboveEcodesMapped_Get(cbCompany.SelectedValue.ToString(), CommonData.StateCode, cbBranches.SelectedValue.ToString(), Convert.ToInt32(strDestCode[1]), Convert.ToInt32(strDestCode[0]), strLogBcode).Tables[0];
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
                MessageBox.Show(ex.Message, "Camp To BR-Head Above", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(ex.ToString(), "Camp To BR-Head Above", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                int intRec = DeleteMappedData();
                for (int row = 0; row < gvMappedGroups.Rows.Count; row++)
                {
                    strArrGroupECode = gvMappedGroups.Rows[row].Cells["ecode"].Value.ToString().Split('~');
                    
                    objDA = new SQLDB();
                    string strLogBRCode = gvMappedGroups.Rows[row].Cells["logBranchCode"].Value.ToString().Trim();

                    for (int i = 0; i < clbDestination.Items.Count; i++)
                    {
                        if (clbDestination.GetItemCheckState(i) == CheckState.Checked)
                        {
                            string[] strDestCode = ((NewCheckboxListItem)(clbDestination.Items[i])).Tag.Split('~');
                            sbSQL.Append("INSERT INTO LevelGroup_map_Detl(lgmd_company_code, lgmd_branch_code, lgmd_log_branch_code, lgmd_state_code, lgmd_doc_month, lgmd_group_ecode, lgmd_dest_ecode, lgm_user_id, lgmd_map_type)" +
                                           " VALUES('" + cbCompany.SelectedValue.ToString() + "', '" + cbBranches.SelectedValue.ToString() + "', '" + strLogBRCode + "', '" + cbBranches.SelectedValue.ToString().Substring(cbCompany.SelectedValue.ToString().Length, 2) +
                                           "', '" + CommonData.DocMonth.ToUpper() + "', " + strArrGroupECode[0].ToString() +
                                           ", " + strDestCode[0] + ",'" + CommonData.LogUserId.ToString() + "','HO');");
                        }
                    }
                }

            }
            
            strSQL = sbSQL.ToString();
            return strSQL;
        }

        private bool CheckCondition()
        {
            bool blCheck = true;
            if (gvMappedGroups.Rows.Count==0)
            {
                MessageBox.Show("Select Camp", "Camp To BR-Head Above", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                MessageBox.Show("Select Destination atleast one", "Camp To BR-Head Above", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blCheck = false;
                clbDestination.Focus();
                return blCheck;
            }
            return blCheck;
        }

        private bool CheckDeleteCondition()
        {
            bool blCheck = true;
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
                MessageBox.Show("Select Destination atleast one", "Camp To BR-Head Above", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
            objData = new StaffLevel();
            string strDelete = "";
            try
            {
                for (int row = 0; row < gvMappedGroups.Rows.Count; row++)
                {
                    if (clbDestination.Items.Count > 0)
                    {
                        string strSCodes = gvMappedGroups.Rows[row].Cells["ecode"].Value.ToString();
                        strArrDestCode = strSCodes.Split('~');

                        strDelete += " DELETE from LevelGroup_map_Detl " +
                           "WHERE lgmd_company_code='" + cbCompany.SelectedValue.ToString() +
                           "' AND lgmd_branch_code='" + cbBranches.SelectedValue.ToString() +
                           "' AND lgmd_doc_month='" + CommonData.DocMonth.ToString().ToUpper() +
                           "' AND lgmd_group_ecode=" + strArrDestCode[0] +
                           " AND lgmd_map_type='HO';";
      
                    }
                    
                }
                nDelete = objDA.ExecuteSaveData(strDelete);
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

        private void fillGridMappedData()
        {

            //string strBcode = string.Empty;
            //objData = new StaffLevel();
            //gvMappedGroups.Rows.Clear();
            //if (cbBranches.SelectedIndex>0)
            //strBcode = cbBranches.SelectedValue.ToString();
            //DataTable dt = objData.LevelMappedGroupList_Get(cbCompany.SelectedValue.ToString(), CommonData.StateCode, cbBranches.SelectedValue.ToString()).Tables[0];
            ////DataTable dt = objData.LevelGroupAboveBranchList(cbCompany.SelectedValue.ToString(), CommonData.StateCode, strBcode).Tables[0];
            ////DataTable dt = objData.LevelGroupAboveBranchListGet(cbCompany.SelectedValue.ToString(), CommonData.StateCode, strBcode, "").Tables[0];
            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        DataGridViewRow tempRow = new DataGridViewRow();

            //        DataGridViewCell cellECODE = new DataGridViewTextBoxCell();
            //        cellECODE.Value = dt.Rows[i]["ECODE"];
            //        tempRow.Cells.Add(cellECODE);

            //        DataGridViewCell cellBRANCHName = new DataGridViewTextBoxCell();
            //        cellBRANCHName.Value = dt.Rows[i]["BRANCH_NAME"];
            //        tempRow.Cells.Add(cellBRANCHName);

            //        DataGridViewCell cellHEADEname = new DataGridViewTextBoxCell();
            //        cellHEADEname.Value = dt.Rows[i]["ENAME"];
            //        tempRow.Cells.Add(cellHEADEname);

            //        DataGridViewCell cellBranchCode = new DataGridViewTextBoxCell();
            //        cellBranchCode.Value = dt.Rows[i]["BRANCH_CODE"];
            //        tempRow.Cells.Add(cellBranchCode);

            //        //DataGridViewCell cellLogicalBranch = new DataGridViewTextBoxCell();
            //        //cellLogicalBranch.Value = dt.Rows[i]["logBranchName"];
            //        //tempRow.Cells.Add(cellLogicalBranch);

            //        gvMappedGroups.Rows.Add(tempRow);

            //    }
            //}
            string strLogBcode = string.Empty;
            objData = new StaffLevel();
            gvMappedGroups.Rows.Clear();
            DataTable dt = objData.LevelMappedGroupList_Get(cbCompany.SelectedValue.ToString(), cbBranches.SelectedValue.ToString().Substring(3, 2), cbBranches.SelectedValue.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataGridViewRow tempRow = new DataGridViewRow();

                    DataGridViewCell cellECODE = new DataGridViewTextBoxCell();
                    cellECODE.Value = dt.Rows[i]["ECODE"];
                    tempRow.Cells.Add(cellECODE);

                    DataGridViewCell cellGROUPName = new DataGridViewTextBoxCell();
                    cellGROUPName.Value = dt.Rows[i]["GroupName"]+"";
                    tempRow.Cells.Add(cellGROUPName);

                    DataGridViewCell cellGroupEname = new DataGridViewTextBoxCell();
                    cellGroupEname.Value = dt.Rows[i]["ENAME"]+"";
                    tempRow.Cells.Add(cellGroupEname);

                    DataGridViewCell celllogBranchCode = new DataGridViewTextBoxCell();
                    celllogBranchCode.Value = dt.Rows[i]["logBranchCode"]+"";
                    tempRow.Cells.Add(celllogBranchCode);

                    DataGridViewCell cellLogicalBranch = new DataGridViewTextBoxCell();
                    cellLogicalBranch.Value = dt.Rows[i]["logBranchName"]+"";
                    tempRow.Cells.Add(cellLogicalBranch);

                    gvMappedGroups.Rows.Add(tempRow);

                }
            }
        }

        private void gvMappedGroups_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (cbCompany.SelectedIndex > 0 && e.RowIndex > -1)
            {
                FillGroupDetailMappedData(e.RowIndex);
                selectedRow = e.RowIndex;
            }
            Cursor.Current = Cursors.Default;
        }

         private void FillCompanyData()
         {
             DataSet ds = null;
             Security objComp = new Security();
             try
             {
                 ds = new DataSet();
                 ds = objComp.GetCompanyDataSet();
                 DataTable dtCompany = ds.Tables[0];
                 if (dtCompany.Rows.Count > 0)
                 {
                     cbCompany.DisplayMember = "CM_Company_Name";
                     cbCompany.ValueMember = "CM_Company_Code";
                     cbCompany.DataSource = dtCompany;
                 }
                 cbCompany.SelectedValue = CommonData.CompanyCode;
             }

             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message, "Above Branch", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
             finally
             {
                 objComp = null;
                 ds.Dispose();
             }

         }

        private void gvMappedGroups_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (gvMappedGroups.Rows.Count > 0 && e.RowIndex > -1)
            {
                FillGroupDetailMappedData(e.RowIndex);
                selectedRow = e.RowIndex;
            }
            Cursor.Current = Cursors.Default;
        }

        private void txtDsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            SearchEcode(txtDsearch.Text.ToString(), clbDestination);
        }

        private void cbBranches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBranches.SelectedIndex > -1)
            {
                fillGridMappedData();
                clbDestination.Items.Clear();
                
            }
           
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > -1)
            {
                FillBranchComboBox(CommonData.StateCode);
                gvMappedGroups.Rows.Clear();
                clbDestination.Items.Clear();
                cbBranches.SelectedIndex = 0;
            }
        }

        private void txtGroupSearch_KeyUp(object sender, KeyEventArgs e)
        {
            Int32 rowindex = 0;
            bool isTrue = false;
            foreach (DataGridViewRow row in gvMappedGroups.Rows)
            {
                if (txtGroupSearch.Text.Trim() != "")
                {
                    if (UtilityFunctions.IsNumeric(txtGroupSearch.Text) == true)
                    {
                        if (row.Cells["ECode"].Value.ToString().Contains(txtGroupSearch.Text.ToUpper()))
                        {
                            rowindex = row.Index;
                            isTrue = true;
                        }
                        else
                            gvMappedGroups.Rows[row.Index].DefaultCellStyle.BackColor = Color.White;
                    }
                    else
                    {
                        if (row.Cells["GroupName"].Value.ToString().Trim().Contains(txtGroupSearch.Text.ToUpper()))
                        {
                            rowindex = row.Index;
                            isTrue = true;
                        }
                        else
                            gvMappedGroups.Rows[row.Index].DefaultCellStyle.BackColor = Color.White;
                    }
                    if (isTrue == true)
                    {
                        gvMappedGroups.Rows[rowindex].Selected = true;
                        break;
                    }
                }
                else
                {
                    isTrue = false;
                    gvMappedGroups.Rows[row.Index].Selected = false;
                }

            }
        }

    }
}
