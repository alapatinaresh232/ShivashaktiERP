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
    public partial class AuditHierarchyMapping : Form
    {
        SQLDB objSQLdb = null;
        AuditDB objAuditdb = null;

        public AuditHierarchyMapping()
        {
            InitializeComponent();
        }

        private void AuditHierarchyMapping_Load(object sender, EventArgs e)
        {
            FillDesignationComboBox();
            FillMappList();

            dtpDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
        }

        private void FillDesignationComboBox()
        {
            objSQLdb = new SQLDB();
            string strCommand = "";
            DataTable dt = new DataTable();

            try
            {
                strCommand = "SELECT desig_code,desig_name FROM DESIG_MAS " +
                             " WHERE dept_code IN (400000) " +
                             " ORDER BY desig_name";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();

                    row[0] = "0";
                    row[1] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cbDesig.DataSource = dt;
                    cbDesig.DisplayMember = "desig_name";
                    cbDesig.ValueMember = "desig_code";
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
            objAuditdb = new AuditDB();
            DataTable dt = new DataTable();
            string strLoadedEcode = string.Empty;
            clbDestination.Items.Clear();
            clbSource.Items.Clear();

            if (cbDesig.SelectedIndex > 0)
            {
                try
                {
                    dt = objAuditdb.GetAuditEcodes(nLevelId).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dt.Rows)
                        {
                            NewCheckboxListItem oclBox = new NewCheckboxListItem();
                            oclBox.Tag = dataRow["ECODE"].ToString();
                            oclBox.Text = dataRow["ENAME"].ToString();

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
                    objSQLdb = null;
                }
            }
        }
        private void FillSourceComboBox()
        {
            objAuditdb = new AuditDB();
            DataTable dt = new DataTable();

            string[] strDestCode = ((NewCheckboxListItem)(clbDestination.SelectedItem)).Tag.Split('~');
            clbSource.Items.Clear();

            try
            {
                dt = objAuditdb.LevelGroupAuditEcodes_Get(Convert.ToInt32(strDestCode[0]), Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper()).Tables[0];

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
                objAuditdb = null;
            }
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

                strCommand = "SELECT DISTINCT ARS_DEST_ECODE FROM AUDIT_HIERARCHY_MAPPING "+
                            " WHERE ARS_DOC_MONTH='"+ Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper() +
                            "' AND ARS_DEST_ECODE=" + DestEcode[0] + " AND ARS_LOCK_STATUS='L' AND ARS_DEPT_ID=400000";

                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    bFlag = false;
                    MessageBox.Show("Mapping Locked","SSERP-Audit Mapping",MessageBoxButtons.OK,MessageBoxIcon.Warning);
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
                MessageBox.Show("Please Select Designation", "Audit Hierarchy Mapping", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clbSource.Focus();
                return flag;
            }
            if (cbDesig.SelectedIndex > 0)
            {
                if (clbDestination.Items.Count == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Select Valid Designation", "Audit Hierarchy Mapping", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Please Select Destination atleast one", "Audit Hierarchy Mapping", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Select source atleast one", "Audit Hierarchy Mapping", MessageBoxButtons.OK, MessageBoxIcon.Information);                
                clbSource.Focus();
                return flag;
            }
            return flag;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData() == true && CheckLockingStatus() == true)
            {
                if (SaveAuditHierarchyDetails() > 0)
                {
                    MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbDesig.SelectedIndex = 0;
                    dtpDocMonth.Value = DateTime.Today;
                    clbDestination.Items.Clear();
                    clbSource.Items.Clear();
                    txtDsearch.Text = "";
                    txtMsearch.Text = "";
                    txtSearch.Text = "";
                    FillMappList();
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            

        }
        private int SaveAuditHierarchyDetails()
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


                strCommand = "DELETE FROM AUDIT_HIERARCHY_MAPPING WHERE ARS_DEST_ECODE="+ strArrDestCode[0] +
                            " AND ARS_DOC_MONTH='"+ Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper() +
                            "'AND ARS_DEPT_ID=400000";

                iRes = objSQLdb.ExecuteSaveData(strCommand);

                strCommand = "";
                iRes = 0;
                
                
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
                                                                       "('"+ strArrSourceCode[2] +
                                                                       "','"+ strArrSourceCode[3] +
                                                                       "',400000,'"+ strArrSourceCode[1] +
                                                                       "','" + strArrSourceCode[0] + 
                                                                       "','"+ strArrDestCode[0] +
                                                                       "','" + strArrDestCode[1] +
                                                                       "','"+ Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper() +
                                                                       "','P','"+ CommonData.LogUserId +
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

        private void txtDsearch_KeyUp(object sender, KeyEventArgs e)
        {
            SearchEcode(txtDsearch.Text.ToString(), clbDestination);

        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            SearchEcode(txtSearch.Text.ToString(), clbSource);
        }


        private void FillMappList()
        {
            objAuditdb = new AuditDB();
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

                dt = objAuditdb.GetAuditMaporUnmappedEcodes(Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper(),cMapped).Tables[0];

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
                objAuditdb = null;
            }
        }

        private void chkMapp_CheckStateChanged(object sender, EventArgs e)
        {
            FillMappList();
        }

        private void txtMsearch_KeyUp(object sender, KeyEventArgs e)
        {
            SearchEcode(txtMsearch.Text.ToString(), lbMapp);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbDesig.SelectedIndex = 0;
            dtpDocMonth.Value = DateTime.Today;
            clbDestination.Items.Clear();
            clbSource.Items.Clear();
            txtDsearch.Text = "";
            txtMsearch.Text = "";
            txtSearch.Text = "";
            chkMapp.Checked = false;

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
                                           "SSERP-Audit Hierarchy Mapping", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {

                    try
                    {
                        if (clbDestination.SelectedIndex > -1)
                            sDestEcode = ((NewCheckboxListItem)(clbDestination.SelectedItem)).Tag;

                        stArrDestCode = sDestEcode.Split('~');



                        strCommand = "DELETE FROM AUDIT_HIERARCHY_MAPPING WHERE ARS_DEST_ECODE=" + stArrDestCode[0] +
                                    " AND ARS_DOC_MONTH='" + Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper() +
                                    "'AND ARS_DEPT_ID=400000";

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
                        dtpDocMonth.Value = DateTime.Today;
                        txtDsearch.Text = "";
                        txtMsearch.Text = "";
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

        private void dtpDocMonth_ValueChanged(object sender, EventArgs e)
        {
            FillMappList();

            for (int i = 0; i < clbDestination.Items.Count; i++)
            {
                clbDestination.SetItemCheckState(i, CheckState.Unchecked);
            }

            clbSource.Items.Clear();
            
        }

      

    }
}
