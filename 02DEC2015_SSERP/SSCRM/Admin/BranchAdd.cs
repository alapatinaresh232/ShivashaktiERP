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
    public partial class BranchAdd : Form
    {
        private Master objMaster = null;
        private StaffLevel objState = null;
        private SQLDB objData = null;
        private UtilityDB objUtility = null;
        string strBCode = string.Empty;
        bool blFormLoad = true;
        public BranchAdd()
        {
            InitializeComponent();
        }

        private void BranchAdd_Load(object sender, EventArgs e)
        {
           // this.Location = new Point(Screen.PrimaryScreen.WorkingArea.X + 120, Screen.PrimaryScreen.WorkingArea.Y + 40);
           // this.StartPosition = FormStartPosition.CenterScreen;
            gvBranch.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8,
                                                        System.Drawing.FontStyle.Regular);

            FillCompanyData();
            FillStateComboBox();
            chkActive.Checked = true;
            cbESI.SelectedIndex = 0;
            btnDelete.Enabled = false;
            FillECodeComboBox();
            FillBranchData();
            blFormLoad = false;
            FillBranchList();
        }
        private void FillBranchData()
        {
            objMaster = new Master();
            objUtility = new UtilityDB();
            try
            {


                cbBranchType.DataSource = objUtility.dtBranchType();
                cbBranchType.ValueMember = "type";
                cbBranchType.DisplayMember = "name";
                cbStates.SelectedIndex = 0;
                cbBranchType.SelectedIndex = 0;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Branch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                objMaster = null;
            }
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
                MessageBox.Show(ex.Message, "Add Branch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                objComp = null;
                ds.Dispose();
            }

        }
        private void FillStateComboBox()
        {
            objState = new StaffLevel();
            try
            {
                DataTable dt = objState.GetStatesDS().Tables[0];
                if (dt.Rows.Count > 1)
                {
                    cbStates.DataSource = dt;
                    cbStates.DisplayMember = "State";
                    cbStates.ValueMember = "CDState";
                }
                dt = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Branch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                objState = null;
            }
        }
        private void FillBranchList()
        {
            objMaster = new Master();
            DataTable dt=null;
            string strActive = "T";
            try
            {
                if (blFormLoad == false)
                {

                    this.gvBranch.Rows.Clear();

                    if (chkActive.CheckState == CheckState.Unchecked)
                        strActive = "F";

                    if (cbStates.SelectedIndex == 0 && cbBranchType.SelectedIndex == 0)
                        dt = objMaster.GetBranchesDS("", "", "", strActive, cbCompany.SelectedValue.ToString()).Tables[0];
                    else if (cbStates.SelectedIndex > 0 && cbBranchType.SelectedIndex == 0)
                        dt = objMaster.GetBranchesDS(cbStates.SelectedValue.ToString(), "", "", strActive, cbCompany.SelectedValue.ToString()).Tables[0];
                    else if (cbStates.SelectedIndex == 0 && cbBranchType.SelectedIndex > 0)
                        dt = objMaster.GetBranchesDS("", cbBranchType.SelectedValue.ToString(), "", strActive, cbCompany.SelectedValue.ToString()).Tables[0];
                    else if (cbStates.SelectedIndex > 0 && cbBranchType.SelectedIndex > 0)
                        dt = objMaster.GetBranchesDS(cbStates.SelectedValue.ToString(), cbBranchType.SelectedValue.ToString(), "", strActive, cbCompany.SelectedValue.ToString()).Tables[0];
                    else if (cbStates.SelectedIndex > 0 && cbBranchType.SelectedIndex > 0 && strBCode.Length > 1)
                        dt = objMaster.GetBranchesDS(cbStates.SelectedValue.ToString(), cbBranchType.SelectedValue.ToString(), "", strActive, cbCompany.SelectedValue.ToString()).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataGridViewRow tempRow = new DataGridViewRow();
                            DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                            cellSLNO.Value = i + 1;
                            tempRow.Cells.Add(cellSLNO);

                            DataGridViewCell cellbranch_code = new DataGridViewTextBoxCell();
                            cellbranch_code.Value = dt.Rows[i]["branch_code"];
                            tempRow.Cells.Add(cellbranch_code);

                            DataGridViewCell cellbranch_name = new DataGridViewTextBoxCell();
                            cellbranch_name.Value = dt.Rows[i]["branch_name"];
                            tempRow.Cells.Add(cellbranch_name);

                            DataGridViewCell cellbranch_address = new DataGridViewTextBoxCell();
                            cellbranch_address.Value = dt.Rows[i]["branch_address"];
                            tempRow.Cells.Add(cellbranch_address);

                            DataGridViewCell cellbranchHead = new DataGridViewTextBoxCell();
                            cellbranchHead.Value = dt.Rows[i]["ENAME"];
                            tempRow.Cells.Add(cellbranchHead);

                            DataGridViewCell cellesi_applicable = new DataGridViewTextBoxCell();
                            cellesi_applicable.Value = dt.Rows[i]["esi_applicable"];
                            tempRow.Cells.Add(cellesi_applicable);

                            DataGridViewCell cellbranch_type = new DataGridViewTextBoxCell();
                            cellbranch_type.Value = dt.Rows[i]["branch_type"];
                            tempRow.Cells.Add(cellbranch_type);

                            DataGridViewCell cellsm_state = new DataGridViewTextBoxCell();
                            cellsm_state.Value = dt.Rows[i]["sm_state"];
                            tempRow.Cells.Add(cellsm_state);

                            DataGridViewCell cellstate_code = new DataGridViewTextBoxCell();
                            cellstate_code.Value = dt.Rows[i]["state_code"];
                            tempRow.Cells.Add(cellstate_code);

                            DataGridViewCell cellEcode = new DataGridViewTextBoxCell();
                            cellEcode.Value = dt.Rows[i]["ECODE"];
                            tempRow.Cells.Add(cellEcode);

                            DataGridViewCell cellActive = new DataGridViewTextBoxCell();
                            cellActive.Value = dt.Rows[i]["Active"];
                            tempRow.Cells.Add(cellActive);

                            gvBranch.Rows.Add(tempRow);

                        }
                    }
                    else
                        MessageBox.Show("No Branch(s)", "Add Branch", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Branch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                objMaster = null;
            }
        }

        private void FillECodeComboBox()
        {
            objMaster = new Master();
            try
            {
                DataTable dt = objMaster.GetEmployeeDataSet().Tables[0];
                if (dt.Rows.Count > 1)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "0";
                    dr[1] = "Select";
                    dt.Rows.InsertAt(dr, 0);
                    cbBranchHead.DataSource = dt;
                    cbBranchHead.DisplayMember = "ENAME";
                    cbBranchHead.ValueMember = "ECODE";
                }
                dt = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Branch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                objMaster = null;
            }
        }

        private void cbBranchHead_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!e.KeyChar.Equals((char)8))
            {
                SearchItems(cbBranchHead, ref e);
            }
            else
                e.Handled = false;


        }

        private void SearchItems(ComboBox acComboBox, ref KeyPressEventArgs e)
        {
            int selectionStart = acComboBox.SelectionStart;
            int selectionLength = acComboBox.SelectionLength;
            int selectionEnd = selectionStart + selectionLength;
            int index;
            StringBuilder sb = new StringBuilder();

            sb.Append(acComboBox.Text.Substring(0, selectionStart))
                .Append(e.KeyChar.ToString())
                .Append(acComboBox.Text.Substring(selectionEnd));
            index = acComboBox.FindString(sb.ToString());

            if (index == -1)
                e.Handled = false;
            else
            {
                acComboBox.SelectedIndex = index;
                acComboBox.Select(selectionStart + 1, acComboBox.Text.Length - (selectionStart + 1));
                e.Handled = true;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }



        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbCompany.SelectedIndex > -1)
                    FillBranchList();
            }
            catch
            {
            }
        }

        private void cbBranchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbBranchType.SelectedIndex > -1 )
                    FillBranchList();
            }
            catch
            {
            }
        }

        private void cbStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbStates.SelectedIndex > 0)
                    FillBranchList();
            }
            catch
            {
            }
        }
        private void gvBranch_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                blFormLoad = true;
                cbStates.Text = gvBranch.Rows[gvBranch.CurrentCell.RowIndex].Cells["StateCode"].Value.ToString();
                for (int counter = 0; counter < cbStates.Items.Count; counter++)
                {
                    if (gvBranch.Rows[gvBranch.CurrentCell.RowIndex].Cells["StateCode"].Value.ToString() == ((System.Data.DataRowView)(cbStates.Items[counter])).Row.ItemArray[1].ToString())
                    {
                        cbStates.SelectedIndex = counter;
                        cbStates.CausesValidation = false;
                        break;
                    }
                }
                cbBranchType.Text = gvBranch.Rows[gvBranch.CurrentCell.RowIndex].Cells["BranchType"].Value.ToString();
                for (int i = 0; i < cbBranchType.Items.Count; i++)
                {
                    if (gvBranch.Rows[gvBranch.CurrentCell.RowIndex].Cells["BranchType"].Value.ToString() == ((System.Data.DataRowView)(cbBranchType.Items[i])).Row.ItemArray[0].ToString()) ///.Items[i].ToString())
                    {
                        cbBranchType.SelectedIndex = i;
                        cbBranchType.CausesValidation = false;
                        break;
                    }
                }
                txtBranchCode.Text = gvBranch.Rows[gvBranch.CurrentCell.RowIndex].Cells["BranchCode"].Value.ToString();
                txtBranchName.Text = gvBranch.Rows[gvBranch.CurrentCell.RowIndex].Cells["BranchName"].Value.ToString();
                txtBranchAddress.Text = gvBranch.Rows[gvBranch.CurrentCell.RowIndex].Cells["Address"].Value.ToString();
                for (int counter = 0; counter < cbESI.Items.Count; counter++)
                {
                    if (gvBranch.Rows[gvBranch.CurrentCell.RowIndex].Cells["ESI"].Value.ToString() == cbESI.Items[counter].ToString())
                    {
                        cbESI.SelectedIndex = counter;
                        cbESI.CausesValidation = false;
                        break;
                    }
                }

                if (gvBranch.Rows[gvBranch.CurrentCell.RowIndex].Cells["Active"].Value.ToString() == "T")
                    chkActive.Checked = true;
                else
                    chkActive.Checked = false;

                if (gvBranch.Rows[gvBranch.CurrentCell.RowIndex].Cells["ECODE"].Value.ToString() != "")
                    cbBranchHead.SelectedValue = gvBranch.Rows[gvBranch.CurrentCell.RowIndex].Cells["ECODE"].Value.ToString();
                else
                    cbBranchHead.SelectedValue = 0;

                cbCompany.Enabled = false;
                cbStates.Enabled = false;
                txtBranchCode.Enabled = false;
                btnDelete.Enabled = true;
                blFormLoad = false;
            }
            catch
            {
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string strSQl = string.Empty;
            objData = new SQLDB();
            objMaster = new Master();
            string strActive = "T";
            int iEcode = 0;
            try
            {
                if (CheckData())
                {
                    if (chkActive.CheckState == CheckState.Unchecked)
                        strActive = "F";
                    if (cbBranchHead.SelectedIndex > 0)
                        iEcode = Convert.ToInt32(cbBranchHead.SelectedValue.ToString());
                    else
                        iEcode = 0;
                    if (btnDelete.Enabled == true)
                    {
                            strSQl = " UPDATE branch_mas" +
                                     " SET branch_name='" + txtBranchName.Text +
                                     "', branch_address='" + txtBranchAddress.Text +
                                     "', branch_type='" + ((System.Data.DataRowView)(cbBranchType.SelectedItem)).Row.ItemArray[0].ToString() +
                                     "', esi_applicable='" + cbESI.SelectedItem.ToString() +
                                     "', Active='" + strActive +
                                     "', BRANCH_HEAD_ECODE=" + iEcode +
                                     ", LAST_MODIFIED_BY='" + CommonData.LogUserId +
                                     "', LAST_MODIFIED_DATE='" + CommonData.CurrentDate +
                                     "' WHERE " +
                                     "  company_code='" + cbCompany.SelectedValue.ToString() +
                                     "' AND state_code='" + cbStates.SelectedValue.ToString() +
                                     "' AND branch_code='" + txtBranchCode.Text + "'";
                            int rec = objData.ExecuteSaveData(strSQl);
                    
                    }
                    else
                    {
                        strSQl = " INSERT into branch_mas(branch_code, branch_name, branch_address, branch_type" +
                                 ", esi_applicable, state_code, company_code, BRANCH_HEAD_ECODE, CREATED_BY, CREATED_DATE, Active) VALUES('" + txtBranchCode.Text.ToString() +
                                 "', '" + txtBranchName.Text.ToString() +
                                 "', '" + Convert.ToString(txtBranchAddress.Text) +
                                 "', '" + ((System.Data.DataRowView)(cbBranchType.SelectedItem)).Row.ItemArray[0].ToString() +
                                 "', '" + cbESI.SelectedItem.ToString() +
                                 "', '" + cbStates.SelectedValue.ToString() +
                                 "', '" + cbCompany.SelectedValue.ToString() +
                                 "', " + iEcode +
                                 ", '" + CommonData.LogUserId +
                                 "', '" + CommonData.CurrentDate + "','" + strActive + "')";
                        int rec = objData.ExecuteSaveData(strSQl);
                    
                    }

                    this.gvBranch.Rows.Clear();
                    CleareEntryData();
                    FillBranchList();
                    btnDelete.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Branch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                objData = null;
                objMaster = null;
            }
        }
        private bool CheckData()
        {
            bool blValue = true;
            DataTable dt = new DataTable();
            
            if (cbStates.SelectedIndex == 0)
            {
                MessageBox.Show("Select state", "Add Branch", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                cbStates.Focus();
                return blValue;
            }
            if (cbBranchType.SelectedIndex == 0)
            {
                MessageBox.Show("Select Branch Type", "Add Branch", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                cbBranchType.Focus();
                return blValue;
            }
            if (txtBranchCode.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("Enter Branch Code!", "Add Branch", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtBranchCode.Focus();
                return blValue;
            }
            if (txtBranchName.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("Enter Branch Name!", "Add Branch", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtBranchName.Focus();
                return blValue;
            }
            if (txtBranchAddress.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("Enter Branch Address!", "Add Branch", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtBranchAddress.Focus();
                return blValue;
            }
            //if (cbBranchHead.SelectedIndex <= 0)
            //{
            //    MessageBox.Show("Select Branch Head", "Add Branch", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    blValue = false;
            //    cbBranchHead.Focus();
            //    return blValue;
            //}
            if (btnDelete.Enabled == false)
            {
                dt = objData.ExecuteDataSet("SELECT branch_name, branch_address FROM branch_mas where  Upper(branch_code) = '" + txtBranchCode.Text.ToString().ToUpper() + "'", CommandType.Text).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Given branh code " + txtBranchCode.Text + " already existed!", "Add Branch", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    blValue = false;
                    return blValue;
                }
            }

            dt = objData.ExecuteDataSet("SELECT branch_name, branch_address FROM branch_mas where Upper(branch_name)='" + txtBranchName.Text.ToString().ToUpper() + "' AND Upper(branch_code) <> '" + txtBranchCode.Text.ToString().ToUpper() + "'", CommandType.Text).Tables[0];
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Given branh " + txtBranchName.Text + " already existed!", "Add Branch", MessageBoxButtons.OK, MessageBoxIcon.Information);
                blValue = false;
                return blValue;
            }
            dt = null;
            return blValue;
        }
        private void CleareEntryData()
        {
            this.gvBranch.Rows.Clear();
            cbStates.Enabled = true;
            txtBranchCode.Enabled = true;
            txtBranchCode.Text = "";
            txtBranchName.Text = "";
            txtBranchAddress.Text = "";
            btnDelete.Enabled = false;
            cbCompany.Enabled = true;
            cbStates.Enabled = true;
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strSQl = string.Empty;
            objData = new SQLDB();
            objMaster = new Master();
            try
            {
                DialogResult result = MessageBox.Show("Do you want to delete " + txtBranchName.Text.ToString()+ " branch ?",
                                       "Branch Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {

                    strSQl = " UPDATE branch_mas" +
                                " SET Active ='F' " +
                                " WHERE company_code='" + cbCompany.SelectedValue.ToString() +
                                "' AND state_code='" + cbStates.SelectedValue.ToString() +
                                "' AND branch_code='" + txtBranchCode.Text + "'";

                    int rec = objData.ExecuteSaveData(strSQl);
                    this.gvBranch.Rows.Clear();
                    CleareEntryData();
                    chkActive.Checked = true;
                    FillBranchList();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Branch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                objData = null;
                objMaster = null;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CleareEntryData();
            cbStates.SelectedIndex = 0;
            cbBranchType.SelectedIndex = 0;
            cbBranchHead.SelectedIndex = 0;
            cbCompany.Enabled = true;
            cbStates.Enabled = true;
        }

        private void txtBranchCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == false)
                e.Handled = true;
            e.KeyChar = Char.ToUpper(e.KeyChar);

            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void txtBranchName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtBranchAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void chkActive_Click(object sender, EventArgs e)
        {
            if(txtBranchCode.Enabled==true)
                FillBranchList();
        }

       

    }
}
