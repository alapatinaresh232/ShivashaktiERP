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
    public partial class UserMaster : Form
    {
        private Master objMaster = null;
        private StaffLevel objState = null;
        private SQLDB objData = null;
        string strBCode = string.Empty;
        private General objGeneral = new General();
        private UtilityDB objUtilDB = null;
        public UserMaster()
        {
            InitializeComponent();
        }

        private void UserMaster_Load(object sender, EventArgs e)
        {
            chkActive.Checked = true;
            btnDelete.Enabled = false;
            FillCompanyData();
            cbCompany.SelectedIndex = objGeneral.GetComboBoxSelectedIndex(CommonData.CompanyCode, cbCompany);
            FillBranchComboBox(CommonData.CompanyCode);
            cbBranches.SelectedIndex = objGeneral.GetComboBoxSelectedIndex(CommonData.BranchCode, cbBranches);
            FillEcodeComboBox(CommonData.CompanyCode, CommonData.BranchCode);
            FillUSERRole();
            FillUserList();
            
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
                    foreach (DataRow dataRow in dtCompany.Rows)
                    {
                        ComboboxItem objItem = new ComboboxItem();
                        objItem.Value = dataRow["CM_Company_Code"].ToString();
                        objItem.Text = dataRow["CM_Company_Name"].ToString();
                        cbCompany.Items.Add(objItem);
                        objItem = null;

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objComp = null;
                ds.Dispose();
            }

        }

        private void FillBranchComboBox(string sCompCode)
        {
            objMaster = new Master();
            string strBCode = string.Empty;
            
            try
            {
                cbBranches.DataSource = null;
                cbBranches.Items.Clear();
                DataTable dt = objMaster.UserBranchList_Get(sCompCode).Tables[0];
                DataRow dr = dt.NewRow();
                dr[0] = "Select";
                dr[1] = "Select";
                dt.Rows.InsertAt(dr, 0);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        ComboboxItem objItem = new ComboboxItem();
                        objItem.Value = dataRow["branch_code"].ToString();
                        objItem.Text = dataRow["branch_name"].ToString();
                        cbBranches.Items.Add(objItem);
                        objItem = null;

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

                objMaster = null;
            }
        }

        private void FillEcodeComboBox(string sCompCode, string sBranchCode)
        {
            objMaster = new Master();
            DataSet dsEmp = null;
            try
            {
                cbEcode.DataSource = null;
                cbEcode.Items.Clear();
                dsEmp = objMaster.UserEcodeList_Get(sCompCode, sBranchCode);
                DataTable dtEmp = dsEmp.Tables[0];
                if (dtEmp.Rows.Count > 0)
                {
                    cbEcode.DataSource = dtEmp;
                    cbEcode.DisplayMember = "ENAME";
                    cbEcode.ValueMember = "ECODE";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objData = null;
                Cursor.Current = Cursors.Default;
            }

        }

        private void FillUSERRole()
        {
            objUtilDB = new UtilityDB();
            try
            {
                DataTable dt = objUtilDB.dtUserRole();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        ComboboxItem objItem = new ComboboxItem();
                        objItem.Value = dataRow["name"].ToString();
                        objItem.Text = dataRow["name"].ToString();
                        cbRole.Items.Add(objItem);
                        objItem = null;

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
                objUtilDB = null;
                
            }

        }

        private void FillUserList()
        {
            objMaster = new Master();
            Security objSecure = new Security();
            DataTable dt=null;
            char strActive = 'T';
            try
            {
                if(cbBranches.SelectedIndex!=-1)
                {

                    this.gvUsers.Rows.Clear();

                    if (chkActive.CheckState == CheckState.Unchecked)
                        strActive = 'F';

                    dt = objMaster.UserMasterList_Get(((SSCRM.ComboboxItem)(cbCompany.Items[cbCompany.SelectedIndex])).Value.ToString(), ((SSCRM.ComboboxItem)(cbBranches.Items[cbBranches.SelectedIndex])).Value.ToString()).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataGridViewRow tempRow = new DataGridViewRow();
                            DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                            cellSLNO.Value = i + 1;
                            tempRow.Cells.Add(cellSLNO);

                            DataGridViewCell cellUM_USER_ID = new DataGridViewTextBoxCell();
                            cellUM_USER_ID.Value = dt.Rows[i]["UM_USER_ID"];
                            tempRow.Cells.Add(cellUM_USER_ID);

                            DataGridViewCell cellUM_USER_NAME = new DataGridViewTextBoxCell();
                            cellUM_USER_NAME.Value = dt.Rows[i]["UM_USER_NAME"];
                            tempRow.Cells.Add(cellUM_USER_NAME);

                            DataGridViewCell cellUM_LOCATION = new DataGridViewTextBoxCell();
                            cellUM_LOCATION.Value = dt.Rows[i]["UM_LOCATION"];
                            tempRow.Cells.Add(cellUM_LOCATION);

                            DataGridViewCell cellUM_PHONE_NO = new DataGridViewTextBoxCell();
                            cellUM_PHONE_NO.Value = dt.Rows[i]["UM_PHONE_NO"];
                            tempRow.Cells.Add(cellUM_PHONE_NO);

                            DataGridViewCell cellUM_PASSWORD = new DataGridViewTextBoxCell();
                            cellUM_PASSWORD.Value = objSecure.GetDecodeString(dt.Rows[i]["UM_PASSWORD"].ToString());
                            tempRow.Cells.Add(cellUM_PASSWORD);

                            DataGridViewCell cellUM_ECODE = new DataGridViewTextBoxCell();
                            cellUM_ECODE.Value = dt.Rows[i]["UM_ECODE"];
                            tempRow.Cells.Add(cellUM_ECODE);

                            DataGridViewCell cellActive = new DataGridViewTextBoxCell();
                            cellActive.Value = dt.Rows[i]["Active"];
                            tempRow.Cells.Add(cellActive);

                            DataGridViewCell cellRole = new DataGridViewTextBoxCell();
                            cellRole.Value = dt.Rows[i]["UserRole"];
                            tempRow.Cells.Add(cellRole);

                            DataGridViewCell cellBackDays = new DataGridViewTextBoxCell();
                            cellBackDays.Value = dt.Rows[i]["BackDays"];
                            tempRow.Cells.Add(cellBackDays);

                            gvUsers.Rows.Add(tempRow);

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
                objMaster = null;
            }
        }

        
        private void btnClose_Click(object sender, EventArgs e)
        {
            objGeneral = null;
            this.Close();
            this.Dispose();
        }

        private void gvUsers_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                txtUserId.Text = gvUsers.Rows[gvUsers.CurrentCell.RowIndex].Cells["UserID"].Value.ToString();
                txtUserName.Text = gvUsers.Rows[gvUsers.CurrentCell.RowIndex].Cells["UserName"].Value.ToString();
                txtLocation.Text = gvUsers.Rows[gvUsers.CurrentCell.RowIndex].Cells["UserLocation"].Value.ToString();
                txtPhone.Text = gvUsers.Rows[gvUsers.CurrentCell.RowIndex].Cells["Phone"].Value.ToString();
                txtPassword.Text = gvUsers.Rows[gvUsers.CurrentCell.RowIndex].Cells["Password"].Value.ToString();
                cbEcode.SelectedValue = gvUsers.Rows[gvUsers.CurrentCell.RowIndex].Cells["ECODE"].Value.ToString();

                cbRole.Text = gvUsers.Rows[gvUsers.CurrentCell.RowIndex].Cells["Role"].Value.ToString();
                txtBackDays.Text = gvUsers.Rows[gvUsers.CurrentCell.RowIndex].Cells["BackDays"].Value.ToString();

                if (gvUsers.Rows[gvUsers.CurrentCell.RowIndex].Cells["Active"].Value.ToString() == "R")
                    chkActive.Checked = true;
                else
                    chkActive.Checked = false;

                txtUserId.Enabled = false;
                btnDelete.Enabled = true;
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
            Security objSecur = new Security();
            char strActive = 'R';
            try
            {
                if (CheckData())
                {
                    if (chkActive.CheckState == CheckState.Unchecked)
                        strActive = 'C';

                    DataTable dt = objMaster.UserUserIdCheck_Get(txtUserId.Text.ToString(), txtUserId.Text.ToString()).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        strSQl = " UPDATE USER_MASTER " +
                                 " SET UM_USER_NAME='" + txtUserName.Text +
                                 "', UM_LOCATION = '" + txtLocation.Text +
                                 "', UM_PASSWORD = '" + objSecur.GetEncodeString(txtPassword.Text) +
                                 "', UM_PHONE_NO = '" + txtPhone.Text +
                                 "', UM_USER_STATUS = '" + strActive +
                                 "', UM_ROLE = '" + cbRole.Text +
                                 "', UM_BACK_DAYS = " + txtBackDays.Text +
                                 " WHERE " +
                                 "  UM_USER_ID='" + txtUserId.Text + "'";
                                
                    }
                    else
                    {
                        strSQl = " INSERT into USER_MASTER(UM_USER_ID, UM_PASSWORD, UM_ECODE, UM_USER_NAME, UM_LOCATION, UM_PHONE_NO, UM_SUPERIOR_ID, UM_CREATED_BY, UM_CREATE_DATE, UM_USER_STATUS, UM_ROLE, UM_BACK_DAYS)" +
                                 " VALUES('" + txtUserId.Text +
                                 "', '" + objSecur.GetEncodeString(txtPassword.Text) +
                                 "', '" + txtUserId.Text +
                                 "', '" + txtUserName.Text +
                                 "', '" + txtLocation.Text +
                                 "', '" + txtPhone.Text +
                                 "', 'SA'" +
                                 ", '" + CommonData.LogUserId +
                                 "', '" + CommonData.CurrentDate +
                                 "','R'"+
                                 ",'" + cbRole.Text + "', "+ txtBackDays.Text+")";
                    }

                    int rec = objData.ExecuteSaveData(strSQl);
                    this.gvUsers.Rows.Clear();
                    CleareEntryData();
                    FillUserList();
                    btnDelete.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objData = null;
                objMaster = null;
                objSecur = null;
            }
        }
        private bool CheckData()
        {
            bool blValue = true;
            
            if (txtUserId.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("Enter user id!", "USER CREATE");
                blValue = false;
                txtUserId.Focus();
                return blValue;
            }
            if (txtUserName.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("Enter user Name!", "USER CREATE");
                blValue = false;
                txtUserName.Focus();
                return blValue;
            }
            if (txtLocation.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("Enter location Address!", "USER CREATE");
                blValue = false;
                txtLocation.Focus();
                return blValue;
            }
            if (txtPassword.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("Enter password Address!", "USER CREATE");
                blValue = false;
                txtLocation.Focus();
                return blValue;
            }

            if (cbRole.SelectedIndex == -1)
            {
                MessageBox.Show("Select Role!", "USER CREATE");
                blValue = false;
                cbRole.Focus();
                return blValue;
            }

            if (txtBackDays.Text.ToString().Trim().Length == 0)
                txtBackDays.Text = "0";

            return blValue;
        }
        private void CleareEntryData()
        {
            this.gvUsers.Rows.Clear();
            txtUserId.Enabled = true;
            txtUserId.Text = "";
            txtUserName.Text = "";
            txtLocation.Text = "";
            txtPhone.Text = "";
            txtPassword.Text = "";
            cbRole.SelectedIndex = -1;
            txtBackDays.Text = "";
            btnDelete.Enabled = false;
            FillUserList();
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strSQl = string.Empty;
            objData = new SQLDB();
            objMaster = new Master();
            try
            {
                DialogResult result = MessageBox.Show("Do you want to delete " + txtUserName.Text.ToString()+ " branch ?",
                                        "USER CREATE", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {

                    strSQl = " UPDATE USER_MASTER" +
                                " SET Active ='F' " +
                                " WHERE UM_USER_ID='" + CommonData.BranchCode +
                                "' AND UM_PASSWORD='" + txtUserId.Text + "'";

                    int rec = objData.ExecuteSaveData(strSQl);
                    this.gvUsers.Rows.Clear();
                    CleareEntryData();
                    chkActive.Checked = true;
                    FillUserList();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            
        }

        private void txtLBranchCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == false)
                e.Handled = true;
            e.KeyChar = Char.ToUpper(e.KeyChar);

            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void txtLBranchName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtBranchAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void chkActive_Click(object sender, EventArgs e)
        {
            if(txtUserId.Enabled==true)
                FillUserList();
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > -1)
                FillBranchComboBox(((SSCRM.ComboboxItem)(cbCompany.Items[cbCompany.SelectedIndex])).Value.ToString());
        }

        private void cbBranches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBranches.SelectedIndex > 0)
            {
                FillEcodeComboBox(((SSCRM.ComboboxItem)(cbCompany.Items[cbCompany.SelectedIndex])).Value.ToString(), ((SSCRM.ComboboxItem)(cbBranches.Items[cbBranches.SelectedIndex])).Value.ToString());
                FillUserList();
            }
        }

        private void txtUserId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetterOrDigit(e.KeyChar) == false)
                e.Handled = true;

            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void txtLocation_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void cbEcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CleareEntryData();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txtBackDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == false)
                e.Handled = true;

            if (e.KeyChar == 8)
                e.Handled = false;
        }
    }
}
