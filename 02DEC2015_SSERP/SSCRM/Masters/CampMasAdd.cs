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
    public partial class CampMasAdd : Form
    {
        private Master objMaster = null;
        private SQLDB objData = null;
        bool blFormLoad = true;
        bool isUpdate = false;
        public CampMasAdd()
        {
            InitializeComponent();
        }
        private void CampMasAdd_Load(object sender, EventArgs e)
        {
            chkActive.Checked = true;
            btnDelete.Enabled = false;
            blFormLoad = false;
            FillCampList();
           
        }
       
        private void FillCampList()
        {
            objMaster = new Master();
            DataTable dt = null;
            string strActive = "T";
            try
            {
                if (blFormLoad == false)
                {

                    this.gvCamp.Rows.Clear();

                    if (chkActive.CheckState == CheckState.Unchecked)
                        strActive = "F";
                    dt = objMaster.BranchCamp_Get(strActive).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        for (int intRow = 0; intRow < dt.Rows.Count; intRow++)
                        {
                            gvCamp.Rows.Add();
                            gvCamp.Rows[intRow].Cells["SLNo"].Value = intRow + 1;
                            gvCamp.Rows[intRow].Cells["CampCode"].Value = dt.Rows[intRow]["CM_CAMP_CODE"];
                            gvCamp.Rows[intRow].Cells["CampName"].Value = dt.Rows[intRow]["CM_CAMP_NAME"];
                            gvCamp.Rows[intRow].Cells["Phone"].Value = dt.Rows[intRow]["CM_PHONE"];
                            gvCamp.Rows[intRow].Cells["State"].Value = dt.Rows[intRow]["CM_STATE"];
                            gvCamp.Rows[intRow].Cells["District"].Value = dt.Rows[intRow]["CM_DISTRICT"];
                            gvCamp.Rows[intRow].Cells["Mandal"].Value = dt.Rows[intRow]["CM_MANDAL"];
                            gvCamp.Rows[intRow].Cells["Village"].Value = dt.Rows[intRow]["CM_VILLAGE"];
                            gvCamp.Rows[intRow].Cells["LandMark"].Value = dt.Rows[intRow]["CM_LANDMARK"];
                            gvCamp.Rows[intRow].Cells["Address"].Value = dt.Rows[intRow]["CM_ADDRESS"];                            
                            gvCamp.Rows[intRow].Cells["Active"].Value = dt.Rows[intRow]["Active"];
                        }
                    }
                    else
                        MessageBox.Show("No Camp(s)", "Add Camp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Camp", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void gvCamp_DoubleClick(object sender, EventArgs e)
        {
            //try
            //{
            //    blFormLoad = true;
                
            //    txtCampCode.Text = gvCamp.Rows[gvCamp.CurrentCell.RowIndex].Cells["CampCode"].Value.ToString();
            //    txtCampName.Text = gvCamp.Rows[gvCamp.CurrentCell.RowIndex].Cells["CampName"].Value.ToString();
            //    txtCampAddress.Text = gvCamp.Rows[gvCamp.CurrentCell.RowIndex].Cells["Address"].Value.ToString();
            //    txtPhone.Text = gvCamp.Rows[gvCamp.CurrentCell.RowIndex].Cells["Phone"].Value.ToString();

            //    if (gvCamp.Rows[gvCamp.CurrentCell.RowIndex].Cells["Active"].Value.ToString() == "T")
            //        chkActive.Checked = true;
            //    else
            //        chkActive.Checked = false;
                               
            //    txtCampCode.Enabled = false;
            //    btnDelete.Enabled = true;
            //    blFormLoad = false;
            //}
            //catch
            //{
            //}
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
                    
                    if (chkActive.CheckState == CheckState.Unchecked)
                        strActive = "F";

                    if (isUpdate == true)
                    {

                        strSQl = " UPDATE CAMP_MAS" +
                                 " SET CM_CAMP_NAME='" + txtCampName.Text +
                                 "', CM_ADDRESS='" + txtCampAddress.Text +
                                 "', CM_PHONE='" + txtPhone.Text +
                                 "', CM_STATE='" + txtState.Text +
                                 "', CM_DISTRICT='" + txtDistrict.Text +
                                 "', CM_MANDAL='" + txtMandal.Text +
                                 "', CM_VILLAGE='" + txtVillage.Text +
                                 "', CM_LANDMARK='" + txtLandMark.Text +
                                 "', ACTIVE='" + strActive +
                                 "', CM_MODIFIED_DATE='" + CommonData.CurrentDate+
                                 "', CM_MODIFIED_BY='" + CommonData.LogUserId +
                                 "' WHERE" +
                                 " CM_COMPANY_CODE='" + CommonData.CompanyCode +
                                 "' AND CM_BRANCH_CODE='" + CommonData.BranchCode +
                                 "' AND CM_CAMP_CODE='" + txtCampCode.Text + "'";

                    }
                    else
                    {
                        strSQl = "INSERT into CAMP_MAS(CM_CAMP_CODE, CM_COMPANY_CODE, CM_BRANCH_CODE, CM_CAMP_NAME, CM_ADDRESS" +
                                 ", CM_PHONE,CM_STATE,CM_DISTRICT,CM_MANDAL,CM_VILLAGE,CM_LANDMARK, CM_CREATED_BY) VALUES('" + txtCampCode.Text.ToString() +
                                 "', '" + CommonData.CompanyCode +
                                 "', '" + CommonData.BranchCode +
                                 "', '" + txtCampName.Text +
                                 "', '" + txtCampAddress.Text +
                                 "', '" + txtPhone.Text +
                                 "', '" + txtState.Text +
                                 "', '" + txtDistrict.Text +
                                 "', '" + txtMandal.Text +
                                 "', '" + txtVillage.Text +
                                 "', '" + txtLandMark.Text +
                                 "', '" + CommonData.LogUserId + "')";

                        

                    }
                    int rec = objData.ExecuteSaveData(strSQl);
                    this.gvCamp.Rows.Clear();
                    CleareEntryData();
                    FillCampList();
                    btnDelete.Enabled = false;
                    isUpdate = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Camp", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //isUpdate = false;
                objData = null;
                objMaster = null;
            }
        }

        private bool CheckData()
        {
            bool blValue = true;
            DataTable dt = new DataTable();

            if (txtCampCode.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("Enter Camp Code!", "Add Camp", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtCampCode.Focus();
                return blValue;
            }
            if (txtCampName.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("Enter camp Name!", "Add Camp", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtCampName.Focus();
                return blValue;
            }
            if (txtVillage.Text.ToString().Trim().Length == 0 || txtMandal.Text.ToString().Trim().Length == 0 || txtDistrict.Text.ToString().Trim().Length == 0 || txtState.Text.ToString().Trim().Length == 0 || txtLandMark.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("Enter Complete Address!", "Add Camp", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                //txtCampName.Focus();
                return blValue;
            }
            if (isUpdate == false)
            {
                if (isUpdate == false)
                {
                    dt = objData.ExecuteDataSet("SELECT CM_CAMP_NAME, CM_ADDRESS FROM CAMP_MAS where  Upper(CM_CAMP_CODE) = '" + txtCampCode.Text.ToString().ToUpper() + "' AND CM_BRANCH_CODE='" + CommonData.BranchCode + "'", CommandType.Text).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Given Camp code " + txtCampCode.Text + " already existed!", "Add Camp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        blValue = false;
                        return blValue;
                    }
                }
            }

            dt = objData.ExecuteDataSet("SELECT CM_CAMP_NAME, CM_ADDRESS FROM CAMP_MAS where Upper(CM_CAMP_CODE) <> '" + txtCampCode.Text.ToString().ToUpper() + "' AND Upper(CM_CAMP_NAME)='" + txtCampName.Text.ToString().ToUpper() + "' AND Upper(CM_COMPANY_CODE) = '" + CommonData.CompanyCode + "' AND CM_BRANCH_CODE='" + CommonData.BranchCode + "'", CommandType.Text).Tables[0];
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Given Camp " + txtCampName.Text + " already existed!", "Add Camp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                blValue = false;
                return blValue;
            }
            dt = null;
            return blValue;
        }

        private void CleareEntryData()
        {
            isUpdate = false;
            txtCampCode.Enabled = true;
            txtCampCode.Text = "";
            txtCampName.Text = "";
            txtCampAddress.Text = "";
            txtPhone.Text = "";
            txtLandMark.Text = "";
            txtMandal.Text = "";
            txtVillage.Text = "";
            txtDistrict.Text = "";
            txtState.Text = "";
            btnDelete.Enabled = false;
            

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strSQl = string.Empty;
            objData = new SQLDB();
            objMaster = new Master();
            try
            {
                DialogResult result = MessageBox.Show("Do you want to delete " + txtCampName.Text.ToString() + " Camp ?",
                                       "Camp Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {

                    strSQl = " UPDATE CAMP_MAS" +
                                " SET Active ='F' " +
                                " WHERE CM_BRANCH_CODE='" + CommonData.BranchCode +
                                "' AND CM_CAMP_CODE='" + txtCampCode.Text + "'";

                    int rec = objData.ExecuteSaveData(strSQl);
                    this.gvCamp.Rows.Clear();
                    CleareEntryData();
                    chkActive.Checked = true;
                    FillCampList();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Camp", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void txtCampCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (char.IsLetterOrDigit(e.KeyChar) == false)
                e.Handled = true;
            e.KeyChar = Char.ToUpper(e.KeyChar);

            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void txtCampName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtCampAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void chkActive_Click(object sender, EventArgs e)
        {
            if (txtCampCode.Enabled == true)
                FillCampList();
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetterOrDigit(e.KeyChar) == false)
                e.Handled = true;
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void gvCamp_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    blFormLoad = true;

            //    txtCampCode.Text = gvCamp.Rows[e.RowIndex].Cells["CampCode"].Value.ToString();
            //    txtCampName.Text = gvCamp.Rows[e.RowIndex].Cells["CampName"].Value.ToString();
            //    txtCampAddress.Text = gvCamp.Rows[e.RowIndex].Cells["Address"].Value.ToString();
            //    txtPhone.Text = gvCamp.Rows[e.RowIndex].Cells["Phone"].Value.ToString();

            //    if (gvCamp.Rows[e.RowIndex].Cells["Active"].Value.ToString() == "T")
            //        chkActive.Checked = true;
            //    else
            //        chkActive.Checked = false;

            //    txtCampCode.Enabled = false;
            //    btnDelete.Enabled = true;
            //    blFormLoad = false;
            //}
            //catch
            //{
            //}
        }

        private void gvCamp_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 14)
            {
                try
                {
                    blFormLoad = true;
                    isUpdate = true;
                    txtCampCode.Text = gvCamp.Rows[e.RowIndex].Cells["CampCode"].Value.ToString();
                    txtCampName.Text = gvCamp.Rows[e.RowIndex].Cells["CampName"].Value.ToString();
                    txtCampAddress.Text = gvCamp.Rows[e.RowIndex].Cells["Address"].Value.ToString();
                    txtPhone.Text = gvCamp.Rows[e.RowIndex].Cells["Phone"].Value.ToString();
                    txtState.Text = gvCamp.Rows[e.RowIndex].Cells["State"].Value.ToString();
                    txtDistrict.Text = gvCamp.Rows[e.RowIndex].Cells["District"].Value.ToString();
                    txtMandal.Text = gvCamp.Rows[e.RowIndex].Cells["Mandal"].Value.ToString();
                    txtVillage.Text = gvCamp.Rows[e.RowIndex].Cells["Village"].Value.ToString();
                    txtLandMark.Text = gvCamp.Rows[e.RowIndex].Cells["LandMark"].Value.ToString();

                    if (gvCamp.Rows[e.RowIndex].Cells["Active"].Value.ToString() == "T")
                        chkActive.Checked = true;
                    else
                        chkActive.Checked = false;

                    txtCampCode.Enabled = false;
                    if (CommonData.LogUserId.ToUpper() == "ADMIN")
                    {
                        btnDelete.Enabled = true;
                    }
                    else
                    {
                        btnDelete.Enabled = false;
                    }

                    blFormLoad = false;
                }
                catch
                {
                    
                }
            }
        }

        private void btnVSearch_Click(object sender, EventArgs e)
        {
            VillageSearch VSearch = new VillageSearch("CampMas");
            VSearch.objFrmCampMas = this;
            VSearch.ShowDialog();
        }

    }
}
