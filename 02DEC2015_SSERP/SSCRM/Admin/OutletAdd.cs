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
    public partial class OutletAdd : Form
    {
        private Master objMaster = null;
        private StaffLevel objState = null;
        private SQLDB objData = null;
        private UtilityDB objUtility = null;
        string strBCode = string.Empty;
        bool blFormLoad = true;
        public OutletAdd()
        {
            InitializeComponent();
        }
        private void OutletAdd_Load(object sender, EventArgs e)
        {
            chkActive.Checked = true;
            btnDelete.Enabled = false;
            FillBranchType();

            blFormLoad = false;
        }
        private void FillBranchType()
        {
            objMaster = new Master();
            objUtility = new UtilityDB();
            try
            {


                cbBranchType.DataSource = objUtility.dtBranchType();
                cbBranchType.ValueMember = "type";
                cbBranchType.DisplayMember = "name";
                cbBranchType.SelectedValue = CommonData.BranchType;
                if (cbBranchType.SelectedIndex > 0)
                    FillBranchComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Outlet", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                objMaster = null;
            }
        }

        private void FillBranchComboBox()
        {
            objMaster = new Master();
            try
            {
                DataTable dt = objMaster.GetBranchDataSet(CommonData.CompanyCode).Tables[0];
                DataView dvData = dt.DefaultView;
                dvData.RowFilter = " BRANCH_TYPE = '" + cbBranchType.SelectedValue.ToString() + "'";
                dt = dvData.ToTable();
                if (dt.Rows.Count > 1)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "0";
                    dr[1] = "Select";
                    dt.Rows.InsertAt(dr, 0);
                    cbBranch.DataSource = dt;
                    cbBranch.DisplayMember = "BRANCH_NAME";
                    cbBranch.ValueMember = "BRANCH_CODE";
                }
                dt = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Outlet", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                objMaster = null;
            }
        }

        private void FillOutletList()
        {
            objMaster = new Master();
            DataTable dt = null;
            string strActive = "T";
            try
            {
                if (blFormLoad == false)
                {

                    this.gvOutlet.Rows.Clear();

                    if (chkActive.CheckState == CheckState.Unchecked)
                        strActive = "F";

                    if (cbBranchType.SelectedIndex > 0 && cbBranch.SelectedIndex > 0)
                    {
                        string[] strBrCode = cbBranch.SelectedValue.ToString().Split('@');
                        dt = objMaster.BranchOutlet_Get(cbBranchType.SelectedValue.ToString(), strBrCode[0], strActive, strBrCode[0].Substring(0, 3)).Tables[0];
                    }
                    if (dt.Rows.Count > 0)
                    {
                        for (int intRow = 0; intRow < dt.Rows.Count; intRow++)
                        {
                            gvOutlet.Rows.Add();
                            gvOutlet.Rows[intRow].Cells["SLNo"].Value = intRow + 1;
                            gvOutlet.Rows[intRow].Cells["OutletCode"].Value = dt.Rows[intRow]["OUTLET_CODE"];
                            gvOutlet.Rows[intRow].Cells["OutletName"].Value = dt.Rows[intRow]["OUTLET_NAME"];
                            gvOutlet.Rows[intRow].Cells["Address"].Value = dt.Rows[intRow]["OUTLET_ADDRESS"];
                            gvOutlet.Rows[intRow].Cells["Branch"].Value = dt.Rows[intRow]["BRANCH_NAME"];
                            gvOutlet.Rows[intRow].Cells["BranchCode"].Value = dt.Rows[intRow]["OUTLET_BRANCH_CODE"];
                            gvOutlet.Rows[intRow].Cells["BranchType"].Value = dt.Rows[intRow]["OUTLET_BRANCH_TYPE"];
                            gvOutlet.Rows[intRow].Cells["Active"].Value = dt.Rows[intRow]["Active"];
                        }
                    }
                    else
                        MessageBox.Show("No Outlet(s)", "Add Outlet", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Outlet", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                objMaster = null;
            }
        }

        private void cbBranch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!e.KeyChar.Equals((char)8))
            {
                SearchItems(cbBranch, ref e);
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

        private void cbBranchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbBranchType.SelectedIndex > -1 && blFormLoad==false)
                    FillBranchComboBox();
            }
            catch
            {
            }
        }

        private void cbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            try
            {
                if (cbBranch.SelectedIndex > 0)
                    FillOutletList();
            }
            catch
            {
            }
        }
        private void gvOutlet_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                blFormLoad = true;
                cbBranchType.Text = gvOutlet.Rows[gvOutlet.CurrentCell.RowIndex].Cells["BranchType"].Value.ToString();
                for (int i = 0; i < cbBranchType.Items.Count; i++)
                {
                    if (gvOutlet.Rows[gvOutlet.CurrentCell.RowIndex].Cells["BranchType"].Value.ToString() == ((System.Data.DataRowView)(cbBranchType.Items[i])).Row.ItemArray[0].ToString()) ///.Items[i].ToString())
                    {
                        cbBranchType.SelectedIndex = i;
                        cbBranchType.CausesValidation = false;
                        break;
                    }
                }

                txtOutletCode.Text = gvOutlet.Rows[gvOutlet.CurrentCell.RowIndex].Cells["OutletCode"].Value.ToString();
                txtOutletName.Text = gvOutlet.Rows[gvOutlet.CurrentCell.RowIndex].Cells["OutletName"].Value.ToString();
                txtOutletAddress.Text = gvOutlet.Rows[gvOutlet.CurrentCell.RowIndex].Cells["Address"].Value.ToString();
                

                if (gvOutlet.Rows[gvOutlet.CurrentCell.RowIndex].Cells["Active"].Value.ToString() == "T")
                    chkActive.Checked = true;
                else
                    chkActive.Checked = false;

                //if (gvOutlet.Rows[gvOutlet.CurrentCell.RowIndex].Cells["BranchCode"].Value.ToString() != "")
                //    cbBranch.SelectedValue = gvOutlet.Rows[gvOutlet.CurrentCell.RowIndex].Cells["BranchCode"].Value.ToString();
                //else
                //    cbBranch.SelectedValue = 0;

                cbBranchType.Enabled = false;
                cbBranch.Enabled = false;
                txtOutletCode.Enabled = false;
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
            try
            {
                if (CheckData())
                {
                    string[] strBrCode = cbBranch.SelectedValue.ToString().Split('@');
                    if (chkActive.CheckState == CheckState.Unchecked)
                        strActive = "F";

                    if (btnDelete.Enabled == true)
                    { 
                        
                        strSQl = " UPDATE BRANCH_MAS_OUTLET" +
                                 " SET OUTLET_NAME='" + txtOutletName.Text +
                                 "', OUTLET_ADDRESS='" + txtOutletAddress.Text +
                                 "', OUTLET_BRANCH_TYPE='" + ((System.Data.DataRowView)(cbBranchType.SelectedItem)).Row.ItemArray[0].ToString() +
                                 "', Active='" + strActive +
                                 "' WHERE " +
                                 "  OUTLET_BRANCH_CODE='" + strBrCode[0] +
                                 "' AND OUTLET_CODE='" + txtOutletCode.Text + "'";

                    }
                    else
                    {
                        strSQl = " INSERT into BRANCH_MAS_OUTLET(OUTLET_CODE, OUTLET_NAME, OUTLET_ADDRESS, OUTLET_BRANCH_TYPE" +
                                 ", OUTLET_BRANCH_CODE, Active) VALUES('" + txtOutletCode.Text.ToString() +
                                 "', '" + txtOutletName.Text.ToString() +
                                 "', '" + Convert.ToString(txtOutletAddress.Text) +
                                 "', '" + ((System.Data.DataRowView)(cbBranchType.SelectedItem)).Row.ItemArray[0].ToString() +
                                 "', '" + strBrCode[0] +
                                 "','T')";

                        

                    }
                    int rec = objData.ExecuteSaveData(strSQl);
                    this.gvOutlet.Rows.Clear();
                    CleareEntryData();
                    FillOutletList();
                    btnDelete.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Outlet", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (cbBranch.SelectedIndex == 0)
            {
                MessageBox.Show("Select state", "Add Outlet", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                cbBranch.Focus();
                return blValue;
            }
            if (cbBranchType.SelectedIndex == 0)
            {
                MessageBox.Show("Select Branch Type", "Add Outlet", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                cbBranchType.Focus();
                return blValue;
            }

            if (txtOutletCode.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("Enter Outlet Code!", "Add Outlet", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtOutletCode.Focus();
                return blValue;
            }
            if (txtOutletName.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("Enter Outlet Name!", "Add Outlet", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtOutletName.Focus();
                return blValue;
            }
            if (txtOutletAddress.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("Enter Outlet Address!", "Add Outlet", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtOutletAddress.Focus();
                return blValue;
            }
            
            if (btnDelete.Enabled == false)
            {
                dt = objData.ExecuteDataSet("SELECT OUTLET_NAME, OUTLET_ADDRESS FROM BRANCH_MAS_OUTLET where  Upper(OUTLET_CODE) = '" + txtOutletCode.Text.ToString().ToUpper() + "' AND OUTLET_BRANCH_CODE='" + cbBranch.SelectedValue.ToString() + "'", CommandType.Text).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Given outlet code " + txtOutletCode.Text + " already existed!", "Add Outlet", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    blValue = false;
                    return blValue;
                }
            }

            dt = objData.ExecuteDataSet("SELECT OUTLET_NAME, OUTLET_ADDRESS FROM BRANCH_MAS_OUTLET where Upper(OUTLET_NAME)='" + txtOutletName.Text.ToString().ToUpper() + "' AND Upper(OUTLET_CODE) <> '" + txtOutletCode.Text.ToString().ToUpper() + "' AND OUTLET_BRANCH_CODE='" + cbBranch.SelectedValue.ToString() + "'" , CommandType.Text).Tables[0];
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Given outlet " + txtOutletName.Text + " already existed!", "Add Outlet", MessageBoxButtons.OK, MessageBoxIcon.Information);
                blValue = false;
                return blValue;
            }
            dt = null;
            return blValue;
        }
        private void CleareEntryData()
        {
            this.gvOutlet.Rows.Clear();
            cbBranch.Enabled = true;
            cbBranchType.Enabled = true;
            txtOutletCode.Enabled = true;
            txtOutletCode.Text = "";
            txtOutletName.Text = "";
            txtOutletAddress.Text = "";
            btnDelete.Enabled = false;
            

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strSQl = string.Empty;
            objData = new SQLDB();
            objMaster = new Master();
            try
            {
                DialogResult result = MessageBox.Show("Do you want to delete " + txtOutletName.Text.ToString() + " Outlet ?",
                                       "Outlet Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {

                    strSQl = " UPDATE BRANCH_MAS_OUTLET" +
                                " SET Active ='F' " +
                                " WHERE OUTLET_BRANCH_CODE='" + cbBranch.SelectedValue.ToString() +
                                "' AND OUTLET_CODE='" + txtOutletCode.Text + "'";

                    int rec = objData.ExecuteSaveData(strSQl);
                    this.gvOutlet.Rows.Clear();
                    CleareEntryData();
                    chkActive.Checked = true;
                    FillOutletList();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Outlet", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void txtOutletCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (char.IsLetter(e.KeyChar) == false)
                e.Handled = true;
            e.KeyChar = Char.ToUpper(e.KeyChar);

            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void txtOutletName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtOutletAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void chkActive_Click(object sender, EventArgs e)
        {
            if (txtOutletCode.Enabled == true)
                FillOutletList();
        }

        private void gvOutlet_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                blFormLoad = true;
                cbBranchType.Text = gvOutlet.Rows[e.RowIndex].Cells["BranchType"].Value.ToString();
                for (int i = 0; i < cbBranchType.Items.Count; i++)
                {
                    if (gvOutlet.Rows[e.RowIndex].Cells["BranchType"].Value.ToString() == ((System.Data.DataRowView)(cbBranchType.Items[i])).Row.ItemArray[0].ToString()) ///.Items[i].ToString())
                    {
                        cbBranchType.SelectedIndex = i;
                        cbBranchType.CausesValidation = false;
                        break;
                    }
                }

                txtOutletCode.Text = gvOutlet.Rows[e.RowIndex].Cells["OutletCode"].Value.ToString();
                txtOutletName.Text = gvOutlet.Rows[e.RowIndex].Cells["OutletName"].Value.ToString();
                txtOutletAddress.Text = gvOutlet.Rows[e.RowIndex].Cells["Address"].Value.ToString();


                if (gvOutlet.Rows[e.RowIndex].Cells["Active"].Value.ToString() == "T")
                    chkActive.Checked = true;
                else
                    chkActive.Checked = false;

                //if (gvOutlet.Rows[e.RowIndex].Cells["BranchCode"].Value.ToString() != "")
                //    cbBranch.SelectedValue = gvOutlet.Rows[e.RowIndex].Cells["BranchCode"].Value.ToString();
                //else
                //    cbBranch.SelectedValue = 0;

                cbBranchType.Enabled = false;
                cbBranch.Enabled = false;
                txtOutletCode.Enabled = false;
                btnDelete.Enabled = true;
                blFormLoad = false;
            }
            catch
            {
            }
        }

    }
}
