using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSAdmin;
using SSTrans;
using SSCRMDB;

namespace SSCRM
{
    public partial class ServiceHierarchyMapping : Form
    {
        SQLDB objSQLdb = null;
        ServiceDB objServicedb = null;
        ServiceDeptDB objServdb = null;
        private string strBranch = "";

        public ServiceHierarchyMapping()
        {
            InitializeComponent();
        }

        private void ServiceHierarchyMapping_Load(object sender, EventArgs e)
        {
            FillDesignationComboBox();
            FillMappList();
            chkPreviousMnth.Visible = false;
            chkPreviousMnth.Checked = false;
            GetUserBranches();
            dtpDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
            if (CommonData.LogUserId.ToUpper() == "ADMIN")
                strBranch += ",SSBAPCHYD,NFLAPCHYD,VNFAPCHYD,NKBAPCHYD,SSBNPCHYD";
        }
        private void GetUserBranches()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            try
            {
                strCmd = "SELECT UB_BRANCH_CODE FROM USER_BRANCH WHERE UB_USER_ID = '" + CommonData.LogUserId + "'";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (strBranch != "")
                            strBranch += ",";
                        strBranch += dt.Rows[i]["UB_BRANCH_CODE"].ToString();
                    }
                }
                else
                {
                    strBranch += CommonData.BranchCode.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FillDesignationComboBox()
        {
            objSQLdb = new SQLDB();
            string strCommand = "";
            DataTable dt = new DataTable();

            try
            {
                strCommand = "exec Get_ServiceDesignations";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();

                    row[0] = "0";
                    row[1] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cbDesig.DataSource = dt;
                    cbDesig.DisplayMember = "DesigName";
                    cbDesig.ValueMember = "DesigCode";
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

        private void FillDestinationComboBox(int nLevelId)
        {
            objServicedb = new ServiceDB();
            DataTable dt = new DataTable();                   
            clbDestination.Items.Clear();
            GetUserBranches();
            try
            {
                dt = objServicedb.EcodesForService_Get(strBranch,nLevelId, "").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = dataRow["ECODE"].ToString();
                        oclBox.Text = dataRow["Ename"].ToString();

                        clbDestination.Items.Add(oclBox);

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

        private void FillSourceComboBox()
        {
            objServdb = new ServiceDeptDB();
            DataTable dt = new DataTable();

            string[] strDestCode = ((NewCheckboxListItem)(clbDestination.SelectedItem)).Tag.Split('~');
            clbSource.Items.Clear();

            try
            {
                dt = objServdb.LevelGroupServiceEcodes_Get(strBranch,Convert.ToInt32(strDestCode[0]), Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper()).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        if (dataRow["Mapped"].ToString() == "99999")
                        {
                            NewCheckboxListItem oclBox = new NewCheckboxListItem();
                            oclBox.Tag = dataRow["ECODE"].ToString();
                            oclBox.Text = dataRow["ENAME"].ToString();
                            clbSource.Items.Add(oclBox);
                            oclBox = null;
                        }
                        else
                        {

                            NewCheckboxListItem oclBox = new NewCheckboxListItem();
                            oclBox.Tag = dataRow["ECODE"].ToString();
                            oclBox.Text = dataRow["ENAME"].ToString();

                            clbSource.Items.Add(oclBox, CheckState.Checked);
                            oclBox = null;

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
                objServdb = null;
            }
        }

        private void FillMappList()
        {
            objServdb = new ServiceDeptDB();
            char cMapped = 'T';
            DataTable dt = new DataTable();
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

                dt = objServdb.Get_ServiceMaporUnmappedEcodes(strBranch,Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper(), cMapped).Tables[0];

                if (dt.Rows.Count > 0)
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
                objServdb = null;
            }
        }


        private bool CheckLockingStatus()
        {
            bool bFlag = true;
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
            string[] DestEcode = null;

            try
            {
                if (clbDestination.SelectedIndex > -1)
                    DestEcode = ((NewCheckboxListItem)(clbDestination.SelectedItem)).Tag.ToString().Split('~');

                strCommand = "SELECT DISTINCT ARS_DEST_ECODE FROM AUDIT_HIERARCHY_MAPPING " +
                            " WHERE ARS_DOC_MONTH='" + Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper() +
                            "' AND ARS_DEST_ECODE=" + DestEcode[0] + " AND ARS_LOCK_STATUS='L' AND ARS_DEPT_ID=800000";

                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    bFlag = false;
                    MessageBox.Show("Mapping Locked", "SSERP-Hierarchy Mapping", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return bFlag;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return bFlag;
        }

        private bool CheckData()
        {
            bool flag = true;
            bool blSource = false;
            bool blDestination = false;

            if (cbDesig.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Designation", "Hierarchy Mapping", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                clbSource.Focus();
                return flag;
            }
            if (cbDesig.SelectedIndex > 0)
            {
                if (clbDestination.Items.Count == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Select Valid Designation", "Hierarchy Mapping", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    clbSource.Focus();
                    return flag;
                }

            }
            if (clbDestination.Items.Count > 0)
            {
                for (int i = 0; i < clbDestination.Items.Count; i++)
                {
                    if (clbDestination.GetItemCheckState(i) == CheckState.Checked)
                    {
                        blDestination = true;
                        break;
                    }
                }
            }

            if (clbDestination.Items.Count == 0 || blDestination == false)
            {
                flag = false;
                MessageBox.Show("Please Select Destination atleast one", "Hierarchy Mapping", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                clbDestination.Focus();
                return flag;
            }

            if (clbSource.Items.Count > 0)
            {
                for (int i = 0; i < clbSource.Items.Count; i++)
                {
                    if (clbSource.GetItemCheckState(i) == CheckState.Checked)
                    {
                        blSource = true;
                        break;
                    }
                }
            }

            if (clbSource.Items.Count == 0 || blSource == false)
            {
                flag = false;
                MessageBox.Show("Select source atleast one", "Hierarchy Mapping", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                clbSource.Focus();
                return flag;
            }
            if (dtpDocMonth.Value > DateTime.Today)
            {
                flag = false;
                MessageBox.Show("Please Select Valid Document Month", "Hierarchy Mapping", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                dtpDocMonth.Focus();
                return flag;

            }

            return flag;
        }

        
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckData() == true && CheckLockingStatus() == true)
                {
                    if (SaveHierarchyDetails() > 0)
                    {
                        MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbDesig.SelectedIndex = 0;                       
                        clbDestination.Items.Clear();
                        clbSource.Items.Clear();
                        txtMsearch.Text = "";
                        txtDsearch.Text = "";
                        txtSearch.Text = "";
                        FillMappList();
                    }
                    else
                    {
                        MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }       

        private int SaveHierarchyDetails()
        {
            objSQLdb = new SQLDB();
            string strCommand = "";
            int iRes = 0;
            string[] strArrDestCode = null;
            string[] strArrSourceCode = null;
            string strDestEcode = "";

            try
            {

                if (clbDestination.SelectedIndex > -1)
                    strDestEcode = ((NewCheckboxListItem)(clbDestination.SelectedItem)).Tag;

                strArrDestCode = strDestEcode.Split('~');

                strCommand = "DELETE FROM AUDIT_HIERARCHY_MAPPING WHERE ARS_DEST_ECODE=" + strArrDestCode[0] +
                            " AND ARS_DOC_MONTH='" + Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper() +
                            "'AND ARS_DEPT_ID=800000";
              
                for (int i = 0; i < clbSource.Items.Count; i++)
                {
                    strArrSourceCode = null;

                    if (clbSource.GetItemCheckState(i) == CheckState.Checked)
                    {
                        strArrSourceCode = ((NewCheckboxListItem)(clbSource.Items[i])).Tag.Split('~');

                        strCommand += "INSERT INTO AUDIT_HIERARCHY_MAPPING(ARS_COMP_CODE " +
                                                                       ", ARS_BRANCH_CODE " +
                                                                       ", ARS_DEPT_ID " +
                                                                       ", ARS_DESIG_ID " +
                                                                       ", ARS_SOURCE_ECODE " +
                                                                       ", ARS_DEST_ECODE " +
                                                                       ", ARS_DEST_DESG_ID " +
                                                                       ", ARS_DOC_MONTH " +
                                                                       ", ARS_LOCK_STATUS " +
                                                                       ", ARS_CREATED_BY " +
                                                                       ", ARS_CREATED_DATE " +
                                                                       ")VALUES " +
                                                                       "('" + strArrSourceCode[2] +
                                                                       "','" + strArrSourceCode[3] +
                                                                       "',800000,'" + strArrSourceCode[1] +
                                                                       "','" + strArrSourceCode[0] +
                                                                       "','" + strArrDestCode[0] +
                                                                       "','" + strArrDestCode[1] +
                                                                       "','" + Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper() +
                                                                       "','P','" + CommonData.LogUserId +
                                                                       "',getdate())";
                    }
                }


                if (strCommand.Length > 10)
                {
                    iRes = objSQLdb.ExecuteSaveData(strCommand);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return iRes;


        }

        
        private void cbDesig_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDesig.SelectedIndex > 0)
            {
                FillDestinationComboBox(Convert.ToInt32(cbDesig.SelectedValue.ToString()));
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            clbDestination.Items.Clear();
            clbSource.Items.Clear();
            cbDesig.SelectedIndex = 0;
            //dtpDocMonth.Value = DateTime.Today;
            lbMapp.DataBindings.Clear();
            txtMsearch.Text = "";
            txtDsearch.Text = "";
            txtSearch.Text = "";
            chkPreviousMnth.Checked = false;
            chkPreviousMnth.Visible = false;
        }

        private void clbDestination_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            clbSource.Items.Clear();

            for (int i = 0; i < clbDestination.Items.Count; i++)
            {
                if (e.Index != i)
                    clbDestination.SetItemCheckState(i, CheckState.Unchecked);

            }

            FillSourceComboBox();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            string strCommand = "";
            int iRes = 0;
            string[] stArrDestCode = null;
            string sDestEcode = "";

            if (CheckData() == true && CheckLockingStatus() == true)
            {
                DialogResult result = MessageBox.Show("Do you want to delete mapped data ?",
                                           "SSERP-Hierarchy Mapping", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {

                    try
                    {
                        if (clbDestination.SelectedIndex > -1)
                            sDestEcode = ((NewCheckboxListItem)(clbDestination.SelectedItem)).Tag;

                        stArrDestCode = sDestEcode.Split('~');
                        
                        strCommand = "DELETE FROM AUDIT_HIERARCHY_MAPPING WHERE ARS_DEST_ECODE=" + stArrDestCode[0] +
                                    " AND ARS_DOC_MONTH='" + Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper() +
                                    "'AND ARS_DEPT_ID=800000 ";

                        iRes = objSQLdb.ExecuteSaveData(strCommand);


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    if (iRes > 0)
                    {
                        MessageBox.Show("Mapped Data Deleted Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clbDestination.Items.Clear();
                        clbSource.Items.Clear();
                        cbDesig.SelectedIndex = 0;
                        //dtpDocMonth.Value = DateTime.Today;
                        txtMsearch.Text = "";
                        txtDsearch.Text = "";
                        txtSearch.Text = "";
                        FillMappList();
                    }
                    else
                    {
                        MessageBox.Show("Mapped Data Not Deleted", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
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

        private void CheckingPreviousMonth()
        {
            //objSQLdb = new SQLDB();
            //DataTable dt = new DataTable();
            //string strCmd = "";

            //try
            //{
            //    strCmd = "SELECT * FROM AUDIT_HIERARCHY_MAPPING "+
            //              " WHERE ARS_DEPT_ID='800000' "+
            //              " AND ARS_DOC_MONTH='"+ Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper() +"'";
            //    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

            //    if (dt.Rows.Count == 0)
            //    {
            //        strCmd = "SELECT * FROM AUDIT_HIERARCHY_MAPPING " +
            //             " WHERE ARS_DEPT_ID='800000' " +
            //             " AND ARS_DOC_MONTH='" + Convert.ToDateTime(dtpDocMonth.Value).AddMonths(-1).ToString("MMMyyyy").ToUpper() + "'";
            //        dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

            //        if (dt.Rows.Count > 0)
            //        {
            //            chkPreviousMnth.Visible = true;
            //        }
            //        else
            //        {
            //            chkPreviousMnth.Visible = false;
            //            chkPreviousMnth.Checked = false;
            //        }
            //    }
            //    else
            //    {
            //        chkPreviousMnth.Visible = false;
            //        chkPreviousMnth.Checked = false;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            //finally
            //{
            //    objSQLdb = null;
            //    dt = null;
            //}
        }


        private void txtDsearch_KeyUp(object sender, KeyEventArgs e)
        {
            SearchEcode(txtDsearch.Text.ToString(), clbDestination);
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            SearchEcode(txtSearch.Text.ToString(), clbSource);
        }

        private void txtMsearch_KeyUp(object sender, KeyEventArgs e)
        {
            SearchEcode(txtMsearch.Text.ToString(), lbMapp);
        }     

        private void chkMapp_CheckStateChanged(object sender, EventArgs e)
        {
            FillMappList();
        }

        private void dtpDocMonth_ValueChanged(object sender, EventArgs e)
        {
            FillMappList();

            for (int i = 0; i < clbDestination.Items.Count; i++)
            {
                clbDestination.SetItemCheckState(i, CheckState.Unchecked);
            }

            clbSource.Items.Clear();
            CheckingPreviousMonth();
        }
             
        private void lbMapp_DoubleClick(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            try
            {
                strCmd = "select cast(ARS_DEST_ECODE as varchar)+'-'+MEMBER_NAME +'('+desig_name+')' Ename " +
                          " from AUDIT_HIERARCHY_MAPPING " +
                          " INNER JOIN EORA_MASTER ON ECODE=ARS_DEST_ECODE " +
                          " INNER JOIN DESIG_MAS ON desig_code=ARS_DEST_DESG_ID WHERE ARS_SOURCE_ECODE='" + lbMapp.SelectedValue.ToString() +
                          "' and ARS_DOC_MONTH='"+ Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper() +"' ";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("" + lbMapp.Text.ToString() + " \n  Mapped To  \n  " + dt.Rows[0]["Ename"] + "");

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

      
    }
}
