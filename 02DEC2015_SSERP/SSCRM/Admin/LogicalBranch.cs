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
    public partial class LogicalBranch : Form
    {
        private Master objMaster = null;
        private StaffLevel objState = null;
        private SQLDB objData = null;
        string strBCode = string.Empty;
        bool blFormLoad = true;
        public LogicalBranch()
        {
            InitializeComponent();
        }

        private void BranchAdd_Load(object sender, EventArgs e)
        {
            //this.Location = new Point(Screen.PrimaryScreen.WorkingArea.X + 120, Screen.PrimaryScreen.WorkingArea.Y + 40);
            //this.StartPosition = FormStartPosition.CenterScreen;
            chkActive.Checked = true;
            btnDelete.Enabled = false;
            blFormLoad = false;
            lblPhysicalBranch.Text = CommonData.BranchCode + ":" + CommonData.BranchName;
            FillBranchList();
            
        }
        private void FillBranchList()
        {
            objMaster = new Master();
            DataTable dt=null;
            char strActive = 'T';
            try
            {
                if (blFormLoad == false)
                {

                    this.gvBranch.Rows.Clear();

                    if (chkActive.CheckState == CheckState.Unchecked)
                        strActive = 'F';

                    dt = objMaster.GetLogicalBranchData("", strActive).Tables[0];

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

                            DataGridViewCell cellActive = new DataGridViewTextBoxCell();
                            cellActive.Value = dt.Rows[i]["Active"];
                            tempRow.Cells.Add(cellActive);

                            gvBranch.Rows.Add(tempRow);

                        }
                    }
                    else
                        MessageBox.Show("No Branch(s)", "Logical Branch", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Logical Branch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                objMaster = null;
            }
        }

        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void gvBranch_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                txtLBranchCode.Text = gvBranch.Rows[gvBranch.CurrentCell.RowIndex].Cells["BranchCode"].Value.ToString();
                txtLBranchName.Text = gvBranch.Rows[gvBranch.CurrentCell.RowIndex].Cells["BranchName"].Value.ToString();
                txtBranchAddress.Text = gvBranch.Rows[gvBranch.CurrentCell.RowIndex].Cells["Address"].Value.ToString();
                if (gvBranch.Rows[gvBranch.CurrentCell.RowIndex].Cells["Active"].Value.ToString() == "T")
                    chkActive.Checked = true;
                else
                    chkActive.Checked = false;
                txtLBranchCode.Enabled = false;
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
            char strActive = 'T';
            try
            {
                if (CheckData())
                {
                    if (chkActive.CheckState == CheckState.Unchecked)
                        strActive = 'F';

                    DataTable dt = objMaster.GetLogicalBranchData(txtLBranchCode.Text.ToString(), strActive).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        strSQl = " UPDATE BRANCH_MAS_LOGICAL " +
                                 " SET LOG_BRANCH_NAME='" + txtLBranchName.Text +
                                 "', LOG_BRANCH_ADDRESS='" + txtBranchAddress.Text +
                                 "', Active='" + strActive +
                                 "' WHERE " +
                                 "  BRANCH_CODE='" + CommonData.BranchCode +
                                 "' AND log_branch_code='" + txtLBranchCode.Text + "'";
                    }
                    else
                    {
                        strSQl = " INSERT into BRANCH_MAS_LOGICAL(branch_code, log_branch_code, log_branch_name, log_branch_address)" +
                                 " VALUES('" + CommonData.BranchCode +
                                 "', '" + txtLBranchCode.Text.ToString() +
                                 "', '" + Convert.ToString(txtLBranchName.Text) +
                                 "', '" + Convert.ToString(txtBranchAddress.Text) +
                                 "' )";
                    }

                    int rec = objData.ExecuteSaveData(strSQl);
                    this.gvBranch.Rows.Clear();
                    CleareEntryData();
                    FillBranchList();
                    btnDelete.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Logical Branch", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            
            if (txtLBranchCode.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("Enter Branch Code!", "Logical Branch", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtLBranchCode.Focus();
                return blValue;
            }
            if (txtLBranchName.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("Enter Branch Name!", "Logical Branch", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtLBranchName.Focus();
                return blValue;
            }
            if (txtBranchAddress.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("Enter Branch Address!", "Logical Branch", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtBranchAddress.Focus();
                return blValue;
            }
       
            return blValue;
        }
        private void CleareEntryData()
        {
            this.gvBranch.Rows.Clear();
            txtLBranchCode.Enabled = true;
            txtLBranchCode.Text = "";
            txtLBranchName.Text = "";
            txtBranchAddress.Text = "";
            btnDelete.Enabled = false;
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strSQl = string.Empty;
            objData = new SQLDB();
            objMaster = new Master();
            try
            {
                DialogResult result = MessageBox.Show("Do you want to delete " + txtLBranchName.Text.ToString()+ " branch ?",
                                       "Branch Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {

                    strSQl = " UPDATE BRANCH_MAS_LOGICAL" +
                                " SET Active ='F' " +
                                " WHERE branch_code='" + CommonData.BranchCode +
                                "' AND log_branch_code='" + txtLBranchCode.Text + "'";

                    int rec = objData.ExecuteSaveData(strSQl);
                    this.gvBranch.Rows.Clear();
                    CleareEntryData();
                    chkActive.Checked = true;
                    FillBranchList();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Logical Branch", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            FillBranchList();
        }

        private void txtLBranchCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetterOrDigit(e.KeyChar) == false)
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
            if(txtLBranchCode.Enabled==true)
                FillBranchList();
        }
    }
}
