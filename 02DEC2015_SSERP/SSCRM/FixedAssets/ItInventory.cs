using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSTrans;

namespace SSCRM
{
    public partial class ItInventory : Form
    {
        SQLDB objSQLdb = null;
        FixedAssetsDB objAssetDB = null;
        Int32 TrnNo = 0;
        bool isHeadUpdate = false;
        bool isDetlUpdate = false;
        Int32 AssetMovementId = 0;
        private DataTable dtMovementDetl = new DataTable();

        string AssetType = "";
        

        public DataTable dtConfigDetl = new DataTable();

        public ItInventory()
        {
            InitializeComponent();
        }

        private void ItInventory_Load(object sender, EventArgs e)
        {
            FillAssetType();
            FillSuppliersData();
            FillToCompanyData();
            dtpPurchaseDate.Value = DateTime.Today;
            dtpGivenDate.Value = DateTime.Today;
            cbTrnType.SelectedIndex = 0;
            cbStatus.SelectedIndex = 0;

            gvAssetConfigDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                     System.Drawing.FontStyle.Regular);

            #region "CREATE CONFIG_DETL TABLE"
            dtConfigDetl.Columns.Add("SLNO");
            dtConfigDetl.Columns.Add("AssetConfigId");
            dtConfigDetl.Columns.Add("ConfigCapacity");
            dtConfigDetl.Columns.Add("ConfigType");
            dtConfigDetl.Columns.Add("ConfigMake");
            dtConfigDetl.Columns.Add("ConfigModel");
            dtConfigDetl.Columns.Add("ConfigQty");
            #endregion

           
        }

        private void FillAssetType()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "exec Get_AssetTypes";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";
                    dt.Rows.InsertAt(row, 0);

                    cbAssetType.DataSource = dt;
                    cbAssetType.DisplayMember = "FAM_ASSET_TYPE";
                    cbAssetType.ValueMember = "FAM_ASSET_TYPE";
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
        private void FillAssetMakeData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbAssetMake.DataSource = null;
            cbAssetModel.DataSource = null;
            try
            {
                if (cbAssetType.SelectedIndex > 0)
                {
                    string strCommand = "SELECT  DISTINCT(FAM_ASSET_MAKE) FROM FIXED_ASSETS_MAS " +
                                        " WHERE FAM_ASSET_TYPE='" + cbAssetType.SelectedValue.ToString() +
                                        "' ORDER BY FAM_ASSET_MAKE ASC";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";
                   
                    dt.Rows.InsertAt(row, 0);

                    cbAssetMake.DataSource = dt;
                    cbAssetMake.DisplayMember = "FAM_ASSET_MAKE";
                    cbAssetMake.ValueMember = "FAM_ASSET_MAKE";
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

        private void FillAssetModelData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbAssetModel.DataSource = null;
            try
            {
                if (cbAssetMake.SelectedIndex > 0)
                {
                    string strCommand = "SELECT  DISTINCT(FAM_ASSET_MODEL) FROM FIXED_ASSETS_MAS " +
                                        " WHERE FAM_ASSET_MAKE='" + cbAssetMake.SelectedValue.ToString() +
                                        "' ORDER BY FAM_ASSET_MODEL ASC";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";
                  
                    dt.Rows.InsertAt(row, 0);

                    cbAssetModel.DataSource = dt;
                    cbAssetModel.DisplayMember = "FAM_ASSET_MODEL";
                    cbAssetModel.ValueMember = "FAM_ASSET_MODEL";
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

        private void FillSuppliersData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT SM_SUPPLIER_CODE,SM_SUPPLIER_NAME FROM SUPPLIER_MASTER " +
                                    " ORDER BY SM_SUPPLIER_NAME ASC";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row["SM_SUPPLIER_NAME"] = "--Select--";
                    dt.Rows.InsertAt(row, 0);

                    cbSupplier.DataSource = dt;
                    cbSupplier.DisplayMember = "SM_SUPPLIER_NAME";
                    cbSupplier.ValueMember = "SM_SUPPLIER_CODE";
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

        private void FillToCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT CM_COMPANY_CODE,CM_COMPANY_NAME FROM COMPANY_MAS WHERE ACTIVE='T' ORDER BY CM_COMPANY_NAME";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";
                    row[1] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cbToComp.DataSource = dt;
                    cbToComp.DisplayMember = "CM_COMPANY_NAME";
                    cbToComp.ValueMember = "CM_COMPANY_CODE";
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


        private void FillToBranches()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbToBranch.DataSource = null;
            try
            {
                if (cbToComp.SelectedIndex > 0)
                {

                    string strCommand = "SELECT BRANCH_NAME ,BRANCH_CODE  FROM BRANCH_MAS " +
                                        " WHERE COMPANY_CODE='" + cbToComp.SelectedValue.ToString() +
                                        "' Order by BRANCH_NAME ";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbToBranch.DataSource = dt;
                    cbToBranch.DisplayMember = "BRANCH_NAME";
                    cbToBranch.ValueMember = "BRANCH_CODE";
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
        private void GenerateAssetSLNo()
        {

            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {

                if (isHeadUpdate == false)
                {

                    string strNewNo = "SSGC/" + cbAssetType.SelectedValue.ToString() + '/';

                    string strCmd = "SELECT ISNULL(MAX(CAST(SUBSTRING(ISNULL(FAH_ASSET_SL_NO,'" + strNewNo +
                                    "')," + (strNewNo.Length + 1) + "," + (strNewNo.Length + 6) + ") AS NUMERIC)),0)+1 " +
                                    " FROM FIXED_ASSETS_HEAD WHERE FAH_ASSET_SL_NO LIKE '%" + strNewNo + "%'";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        txtAssetSLNo.Text = strNewNo + Convert.ToInt32(dt.Rows[0][0].ToString());
                    }
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

        private void cbAssetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAssetType.SelectedIndex > 0)
            {              
                FillAssetMakeData();
                GenerateAssetSLNo();
            }
        }
        private void cbAssetMake_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbAssetMake.SelectedIndex > 0)
            {
                FillAssetModelData();
            }
        }
        private void cbToComp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbToComp.SelectedIndex > 0)
            {
                FillToBranches();
            }
        }

        private void txtToEcode_TextChanged(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            if (txtToEcode.Text.Length > 4)
            {
                try
                {
                    string strCmd = "SELECT MEMBER_NAME EName FROM EORA_MASTER WHERE ECODE=" + Convert.ToInt32(txtToEcode.Text) + "";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtToEName.Text = dt.Rows[0]["EName"].ToString();
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
            else
            {
                txtToEName.Text = "";
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();



        }

        private void btnAddAssetConfigDetails_Click(object sender, EventArgs e)
        {

            if (cbAssetType.SelectedIndex > 0)
            {
                AssetType = cbAssetType.SelectedValue.ToString();


                AssetConfigurationDetl ConfigDetl = new AssetConfigurationDetl(AssetType);
                ConfigDetl.objItInventory = this;
                ConfigDetl.Show();

            }
            else
            {
                MessageBox.Show("Please Select Asset Type", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool CheckData()
        {
            bool flag = true;

            //objSQLdb = new SQLDB();
            //DataTable dt = new DataTable();

            //string strCmd = "SELECT COUNT(*) FROM FIXED_ASSETS_MOVEMENT_REG "+
            //                " WHERE FAMR_ASSET_SL_NO='"+ txtAssetSLNo.Text.ToString() +"'";
            //dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
            //if (dt.Rows.Count > 0)
            //{
            //    dtMovementDetl = dt;               
                //flag = false;
                //MessageBox.Show("This Data Can Not Be Manipulated", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //return false;
            //}


            //if (txtAssetSLNo.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter Asset SlNo", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtAssetSLNo.Focus();
            //    return false;
            //}
            if (cbAssetType.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Asset Type", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbAssetType.Focus();
                return false;
            }
            if (cbAssetMake.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Asset Make", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbAssetMake.Focus();
                return false;
            }
            if (cbSupplier.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Supplier Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbSupplier.Focus();
                return false;
                
            }
            if (cbStatus.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Asset Working Status", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbStatus.Focus();
                return false;
            }

            //if (gvAssetConfigDetails.Rows.Count == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Add Asset Configuration Details", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);                
            //    return false;
            //}
            if (dtpGivenDate.Value < dtpPurchaseDate.Value)
            {
                flag = false;
                MessageBox.Show("Please Select Valid Issue Date", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpGivenDate.Focus();
                return false;
            }
            if (cbTrnType.SelectedIndex > 0)
            {
                if (cbTrnType.SelectedItem.ToString() == "BR2BR")
                {                                    

                   if (cbToComp.SelectedIndex == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Select To Company", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbToComp.Focus();
                        return false;
                    }
                    else if (cbToBranch.SelectedIndex == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Select To Branch ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbToBranch.Focus();
                        return false;
                    }
                }

               
              
                else if (cbTrnType.SelectedItem.ToString() == "BR2EMPLOYEE")
                {
                   
                    if (txtToEName.Text.Length == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Enter To Ecode ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtToEcode.Focus();
                        return false;
                    }
                    else if (cbToComp.SelectedIndex == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Select To Company ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbToComp.Focus();
                        return false;
                    }
                    else if (cbToBranch.SelectedIndex == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Select ToBranch ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbToBranch.Focus();
                        return false;
                    }

                }
               
            }
            else
            {
                flag = false;
                MessageBox.Show("Please Select Transaction Type ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbTrnType.Focus();
                return false;

            }


            return flag;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            string strCmd = "";
            int iRec = 0;

            if (CheckData() == true)
            {

                if (SaveAssetHeadDetails() > 0)
                {
                    if (SaveAssetMovementDetails() > 0)
                    {
                        SaveAssetConfigDetails();

                        MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(null, null);
                        isHeadUpdate = false;
                        isDetlUpdate = false;
                    }


                    else
                    {
                        //strCmd = "DELETE FROM FIXED_ASSETS_MOVEMENT_REG WHERE FAMR_ASSET_SL_NO='" + txtAssetSLNo.Text.ToString() + "'";
                        //iRec = objSQLdb.ExecuteSaveData(strCmd);

                        strCmd = "DELETE FROM FIXED_ASSETS_HEAD WHERE FAH_ASSET_SL_NO='" + txtAssetSLNo.Text.ToString() + "'";
                        iRec = objSQLdb.ExecuteSaveData(strCmd);

                        MessageBox.Show("Data Not Saved ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {                 

                    MessageBox.Show("Data Not Saved ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               
            }
        }

        private int SaveAssetHeadDetails()
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "";
            try
            {
                if (txtAssetCost.Text == "")
                {
                    txtAssetCost.Text = "0";
                }

                if (isHeadUpdate == true)
                {
                    strCommand = "UPDATE FIXED_ASSETS_HEAD SET FAH_ASSET_TYPE='" + cbAssetType.SelectedValue.ToString() +
                                    "',FAH_ASSET_MAKE='" + cbAssetMake.SelectedValue.ToString() +
                                    "',FAH_MODEL='" + cbAssetModel.SelectedValue.ToString() +
                                    "', FAH_ASSET_COST='"+ Convert.ToDouble(txtAssetCost.Text).ToString("0.00") +
                                    "', FAH_ASSET_TAG_NO='" + txtAssetTagNo.Text.ToString() +
                                    "', FAH_MODIFIED_BY='" + CommonData.LogUserId +
                                    "',FAH_MODIFIED_DATE=getdate()" +
                                    " WHERE FAH_ID=" + txtAssetSLNo.Tag + " ";

                }
                else if (isHeadUpdate == false)
                {

                    strCommand = "INSERT INTO FIXED_ASSETS_HEAD(FAH_ASSET_TYPE " +
                                                             ", FAH_ASSET_MAKE " +
                                                             ", FAH_MODEL " +
                                                             ", FAH_ASSET_SL_NO " +
                                                             ", FAH_SUPPLIER_ID " +
                                                             ", FAH_REC_DATE " +
                                                             ", FAH_ASSET_COST "+
                                                             ", FAH_ASSET_TAG_NO "+
                                                             ", FAH_CREATED_BY " +
                                                             ", FAH_CREATED_DATE " +
                                                             ")VALUES " +
                                                             "('" + cbAssetType.SelectedValue.ToString() +
                                                             "','" + cbAssetMake.SelectedValue.ToString() +
                                                             "','" + cbAssetModel.SelectedValue.ToString() +
                                                             "','" + txtAssetSLNo.Text.ToString() +
                                                             "','" + cbSupplier.SelectedValue.ToString() +
                                                             "','" + Convert.ToDateTime(dtpPurchaseDate.Value).ToString("dd/MMM/yyyy") +
                                                             "', '"+ Convert.ToDouble(txtAssetCost.Text).ToString("0.00") +
                                                             "' , '"+ txtAssetTagNo.Text.ToString() +"', '" + CommonData.LogUserId +
                                                             "',getdate())";

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

        private int SaveAssetMovementDetails()
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "";
            DataTable dt = new DataTable();
            try
            {
               
                if (txtToEcode.Text.Length == 0)
                {
                    txtToEcode.Text = "0";
                }


                if (TrnNo.Equals(0))
                {
                    strCommand = "SELECT ISNULL(MAX(FAMR_TRN_NO),0)+1 TrnNo FROM FIXED_ASSETS_MOVEMENT_REG " +
                                " WHERE FAMR_ASSET_SL_NO='" + txtAssetSLNo.Text.ToString() + "' ";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        TrnNo = Convert.ToInt32(dt.Rows[0]["TrnNo"].ToString());

                    }
                }

                objSQLdb = new SQLDB();
                strCommand = "";

                strCommand = "DELETE FROM FIXED_ASSETS_MOVEMENT_REG WHERE FAMR_ID='" + AssetMovementId + "' ";
                iRes = objSQLdb.ExecuteSaveData(strCommand);


                strCommand = "";

                objSQLdb = new SQLDB();

                strCommand = "INSERT INTO FIXED_ASSETS_MOVEMENT_REG(FAMR_ASSET_SL_NO " +
                                                                 ", FAMR_TRN_NO " +
                                                                 ", FAMR_TRN_TYPE " +
                                                                 ", FAMR_FROM_BRANCH_CODE " +
                                                                 ", FAMR_FROM_ECODE " +
                                                                 ", FAMR_TO_BRANCH_CODE " +
                                                                 ", FAMR_TO_ECODE " +
                                                                 ", FAMR_GIVEN_DATE " +
                                                                 ", FAMR_STATUS " +
                                                                 ", FAH_CONT_NO " +
                                                                 ", FAMR_ASSET_TAG_NO "+
                                                                 ", FAMR_CREATED_BY " +
                                                                 ", FAMR_CREATED_DATE " +
                                                                 ")VALUES " +
                                                                 "('" + txtAssetSLNo.Text.ToString() +
                                                                 "'," + TrnNo +
                                                                 ",'" + cbTrnType.Tag.ToString() +
                                                                 "','SSBAPCHYD',0,'" + cbToBranch.SelectedValue.ToString() +
                                                                 "'," + Convert.ToInt32(txtToEcode.Text) +
                                                                 ",'" + Convert.ToDateTime(dtpGivenDate.Value).ToString("dd/MMM/yyyy") +
                                                                 "', '" + cbStatus.SelectedItem.ToString() +
                                                                 "', '" + txtContactNo.Text.ToString().Replace(" ", "") +
                                                                 "', '"+ txtAssetTagNo.Text.ToString() +"', '" + CommonData.LogUserId +
                                                                 "',getdate())";


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

        private int SaveAssetConfigDetails()
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "";
            try
            {
                if (gvAssetConfigDetails.Rows.Count > 0)
                {
                    strCommand = "DELETE FROM FIXED_ASSETS_SYSTEM_CONFIG WHERE FASC_ASSET_ID='" + txtAssetSLNo.Text.ToString() + "' ";
                    iRes = objSQLdb.ExecuteSaveData(strCommand);


                    strCommand = "";
                    objSQLdb = new SQLDB();


                    for (int i = 0; i < gvAssetConfigDetails.Rows.Count; i++)
                    {
                        strCommand += "INSERT INTO FIXED_ASSETS_SYSTEM_CONFIG(FASC_ASSET_ID " +
                                                                          ", FASC_SL_NO " +
                                                                          ", FASC_CONFIG_ASSET_ID " +
                                                                          ", FASC_CONFIG_TYPE " +
                                                                          ", FASC_CONFIG_MAKE " +
                                                                          ", FASC_CONFIG_MODEL " +
                                                                          ", FASC_CONFIG_CAPACITY " +
                                                                          ", FASC_CONFIG_QTY " +
                                                                          ", FASC_CREATED_BY " +
                                                                          ", FASC_CREATED_DATE " +
                                                                          ")VALUES('" + txtAssetSLNo.Text.ToString() +
                                                                          "'," + Convert.ToInt32(gvAssetConfigDetails.Rows[i].Cells["SLNO"].Value) +
                                                                          ",'"+ gvAssetConfigDetails.Rows[i].Cells["AssetConfigId"].Value.ToString() +
                                                                          "','"+ gvAssetConfigDetails.Rows[i].Cells["ConfigType"].Value.ToString() +
                                                                          "','"+ gvAssetConfigDetails.Rows[i].Cells["ConfigMake"].Value.ToString() +
                                                                          "','"+ gvAssetConfigDetails.Rows[i].Cells["ConfigModel"].Value.ToString() +
                                                                          "','"+ gvAssetConfigDetails.Rows[i].Cells["ConfigCapacity"].Value.ToString() +
                                                                          "',"+ Convert.ToInt32(gvAssetConfigDetails.Rows[i].Cells["ConfigQty"].Value) +
                                                                          ",'"+ CommonData.LogUserId +
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


        private void btnCancel_Click(object sender, EventArgs e)
        {                      
            cbAssetType.SelectedIndex = 0;
            cbAssetMake.SelectedIndex = -1;
            cbAssetModel.SelectedIndex = -1;
            cbSupplier.SelectedIndex = 0;
            cbStatus.SelectedIndex = 0;
            cbTrnType.SelectedIndex = 0;
            dtpGivenDate.Value = DateTime.Today;
            dtpPurchaseDate.Value = DateTime.Today;
            cbToComp.SelectedIndex = 0;
            cbToBranch.SelectedIndex = -1;
            txtToEcode.Text = "";
            txtToEName.Text = "";
            txtContactNo.Text = "";
            dtConfigDetl.Rows.Clear();
            gvAssetConfigDetails.Rows.Clear();
            cbAssetType.Enabled = true;
            cbAssetMake.Enabled = true;
            cbAssetModel.Enabled = true;
            txtAssetSLNo.Text = "";
            isDetlUpdate = false;
            isHeadUpdate = false;
            dtpGivenDate.Enabled = true;
            cbToComp.Enabled = true;
            cbToBranch.Enabled = true;
            txtToEcode.ReadOnly = false;
            cbTrnType.Enabled = true;
            txtAssetTagNo.Text = "";
            txtAssetCost.Text = "";
            txtAssetCost.ReadOnly = false;
            txtAssetTagNo.ReadOnly = false;
            cbSupplier.Enabled = true;
            dtpPurchaseDate.Enabled = true;

        }

        public void GetAssetConfigDetails()
        {
            int intRow = 1;
            gvAssetConfigDetails.Rows.Clear();
            
            try
            {

                for (int i = 0; i < dtConfigDetl.Rows.Count; i++)
                {
                    DataGridViewRow tempRow = new DataGridViewRow();
                    DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                    cellSLNO.Value = intRow;
                    dtConfigDetl.Rows[i]["SLNO"] = intRow;
                    tempRow.Cells.Add(cellSLNO);


                    DataGridViewCell cellAssetConfigId = new DataGridViewTextBoxCell();
                    cellAssetConfigId.Value = dtConfigDetl.Rows[i]["AssetConfigId"].ToString();
                    tempRow.Cells.Add(cellAssetConfigId);

                    DataGridViewCell cellConfigCapacity = new DataGridViewTextBoxCell();
                    cellConfigCapacity.Value = dtConfigDetl.Rows[i]["ConfigCapacity"].ToString();
                    tempRow.Cells.Add(cellConfigCapacity);

                    DataGridViewCell cellConfigType = new DataGridViewTextBoxCell();
                    cellConfigType.Value = dtConfigDetl.Rows[i]["ConfigType"].ToString();
                    tempRow.Cells.Add(cellConfigType);

                    DataGridViewCell cellConfigMake = new DataGridViewTextBoxCell();
                    cellConfigMake.Value = dtConfigDetl.Rows[i]["ConfigMake"].ToString();
                    tempRow.Cells.Add(cellConfigMake);

                    DataGridViewCell cellConfigModel = new DataGridViewTextBoxCell();
                    cellConfigModel.Value = dtConfigDetl.Rows[i]["ConfigModel"].ToString();
                    tempRow.Cells.Add(cellConfigModel);

                    DataGridViewCell cellConfigQty = new DataGridViewTextBoxCell();
                    cellConfigQty.Value = dtConfigDetl.Rows[i]["ConfigQty"].ToString();
                    tempRow.Cells.Add(cellConfigQty);


                    intRow = intRow + 1;
                    gvAssetConfigDetails.Rows.Add(tempRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtToEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

        }

        private void txtContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar != '\b' && e.KeyChar!=',')
            //{
            //    if (!char.IsDigit(e.KeyChar))
            //    {
            //        e.Handled = true;
            //    }
            //}
        }

        private void cbTrnType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTrnType.Text == "BR2BR")
            {
                cbTrnType.Tag = "BR2BR";
            }
            else if (cbTrnType.Text == "BR2EMPLOYEE")
            {
                cbTrnType.Tag = "BR2GC";

            }

        }

        private void FillAssetsDetails()
        {
            objAssetDB = new FixedAssetsDB();
            DataTable dt = new DataTable();
           
            if (txtAssetSLNo.Text != "")
            {
                try
                {
                    dt = objAssetDB.GetFixedAssetsData(txtAssetSLNo.Text.ToString()).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        isHeadUpdate = true;

                        cbAssetType.Enabled = false;
                        cbAssetMake.Enabled = false;
                        cbAssetModel.Enabled = false;
                        cbSupplier.Enabled = false;
                        dtpPurchaseDate.Enabled = false;
                        txtAssetTagNo.ReadOnly = true;
                        txtAssetCost.ReadOnly = true;

                        dtpGivenDate.Enabled = false;
                        cbToComp.Enabled = false;
                        cbToBranch.Enabled = false;
                        txtToEcode.ReadOnly = true;
                        cbTrnType.Enabled = false;

                        txtAssetSLNo.Tag = dt.Rows[0]["AssetId"].ToString();
                        cbAssetType.SelectedValue = dt.Rows[0]["Asset_Type"].ToString();
                        cbAssetMake.SelectedValue = dt.Rows[0]["Asset_Make"].ToString();
                        cbAssetModel.SelectedValue = dt.Rows[0]["Asset_Model"].ToString();
                        cbSupplier.SelectedValue = dt.Rows[0]["Supplier_Id"].ToString();

                        if (dt.Rows[0]["Purchase_Date"].ToString().Length >0)
                        {
                            dtpPurchaseDate.Value = Convert.ToDateTime(dt.Rows[0]["Purchase_Date"]);
                        }
                        dtpGivenDate.Value = Convert.ToDateTime(dt.Rows[0]["Given_Date"].ToString());
                        cbStatus.SelectedItem = dt.Rows[0]["Status"].ToString();
                        AssetMovementId = Convert.ToInt32(dt.Rows[0]["Movement_Id"].ToString());
                        txtAssetTagNo.Text = dt.Rows[0]["TagNo"].ToString();
                        txtAssetCost.Text = dt.Rows[0]["AssetCost"].ToString();

                        TrnNo = Convert.ToInt32(dt.Rows[0]["Trn_No"].ToString());
                        if (dt.Rows[0]["Trn_Type"].ToString().Equals("BR2GC"))
                        {
                            cbTrnType.Text = "BR2EMPLOYEE";
                        }
                        else
                        {
                            cbTrnType.Text = dt.Rows[0]["Trn_Type"].ToString();
                        }
                        cbToComp.Text = dt.Rows[0]["To_Comp_Name"].ToString();
                        cbToBranch.SelectedValue = dt.Rows[0]["To_Branch"].ToString();

                        string ToEcode = dt.Rows[0]["To_Ecode"].ToString();
                        if (ToEcode.Length > 3)
                        {
                            txtToEcode.Text = ToEcode;
                        }
                                               
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        isHeadUpdate = false;
                        isDetlUpdate = false;
                        //cbAssetType.SelectedIndex = 0;
                        GenerateAssetSLNo();
                        //cbAssetMake.SelectedIndex = -1;
                        //cbAssetModel.SelectedIndex = -1;
                        cbSupplier.SelectedIndex = 0;
                        dtpPurchaseDate.Value = DateTime.Today;
                        dtpGivenDate.Value = DateTime.Today;
                        cbTrnType.SelectedIndex = 0;
                        cbToComp.SelectedIndex = 0;

                        cbToBranch.SelectedIndex = -1;
                        txtToEcode.Text = "";
                        txtToEName.Text = "";
                        dtConfigDetl.Rows.Clear();
                        gvAssetConfigDetails.Rows.Clear();
                        cbStatus.SelectedIndex = 0;

                        cbAssetType.Enabled = true;
                        cbAssetMake.Enabled = true;
                        cbAssetModel.Enabled = true;
                        cbSupplier.Enabled = true;
                        dtpPurchaseDate.Enabled = true;
                        cbTrnType.Enabled = true;

                        dtpGivenDate.Enabled = true;

                        cbToComp.Enabled = true;
                        cbToBranch.Enabled = true;
                        txtToEcode.ReadOnly = false;
                        txtAssetTagNo.ReadOnly = false;
                        txtAssetCost.ReadOnly = false;

                        txtAssetTagNo.Text = "";
                        txtAssetCost.Text = "";


                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objAssetDB = null;
                    dt = null;
                }
            }
            
        }
        private void FillAssetConfigDetails()
        {
            objAssetDB = new FixedAssetsDB();
            DataTable dt = new DataTable();
            dtConfigDetl.Rows.Clear();
            try
            {
                  dt = objAssetDB.GetFixedAssetsConfigurationDetails(txtAssetSLNo.Text.ToString()).Tables[0];
                  if (dt.Rows.Count > 0)
                  {

                      for (int i = 0; i < dt.Rows.Count; i++)
                      {
                          dtConfigDetl.Rows.Add(new object[]{"-1",dt.Rows[i]["Config_AssetId"].ToString(),
                                                                    dt.Rows[i]["Config_Capacity"].ToString(),
                                                                    dt.Rows[i]["Config_Type"].ToString(),
                                                                    dt.Rows[i]["Config_Make"].ToString(),
                                                                    dt.Rows[i]["Config_Model"].ToString(),
                                                                    dt.Rows[i]["Config_Qty"].ToString()});
                          GetAssetConfigDetails();

                      }
                  }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void gvAssetConfigDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             objSQLdb = new SQLDB();
            DataTable dt = new DataTable();

            string strCmd = "SELECT COUNT(*) FROM FIXED_ASSETS_MOVEMENT_REG "+
                            " WHERE FAMR_ASSET_SL_NO='"+ txtAssetSLNo.Text.ToString() +"'";
            dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
            if (dt.Rows.Count > 1)
            {
                dtMovementDetl = dt;
            }

            if (e.ColumnIndex == gvAssetConfigDetails.Columns["Edit"].Index)
            {

                if (Convert.ToBoolean(gvAssetConfigDetails.Rows[e.RowIndex].Cells["Edit"].Selected) == true)
                {
                    isDetlUpdate = true;
                    if (dtMovementDetl.Rows.Count == 1)
                    {
                        int SlNo = Convert.ToInt32(gvAssetConfigDetails.Rows[e.RowIndex].Cells[gvAssetConfigDetails.Columns["SlNo"].Index].Value);
                        DataRow[] dr = dtConfigDetl.Select("SlNo=" + SlNo);

                        AssetType = cbAssetType.SelectedValue.ToString();

                        AssetConfigurationDetl ConfigDetl = new AssetConfigurationDetl(AssetType, dr);
                        ConfigDetl.objItInventory = this;
                        ConfigDetl.Show();
                    }
                    else
                    {
                        MessageBox.Show("This Data Can Not Be Manipulated", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }             
                             

            if (e.ColumnIndex == gvAssetConfigDetails.Columns["Delete"].Index)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    int SlNo = Convert.ToInt32(gvAssetConfigDetails.Rows[e.RowIndex].Cells[gvAssetConfigDetails.Columns["SlNo"].Index].Value);
                    DataRow[] dr = dtConfigDetl.Select("SlNo=" + SlNo);
                    dtConfigDetl.Rows.Remove(dr[0]);
                    GetAssetConfigDetails();
                    MessageBox.Show("Selected Data Deleted Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }


        }

        private void txtAssetSLNo_KeyUp(object sender, KeyEventArgs e)
        {
            //if (txtAssetSLNo.Text != "")
            //{
            //    isHeadUpdate = true;
            //    FillAssetsDetails();
            //    FillAssetConfigDetails();
            //}
            //else
            //{
            //    isHeadUpdate = false;
            //    isDetlUpdate = false;
            //    cbAssetType.SelectedIndex = 0;
            //    cbAssetMake.SelectedIndex = -1;
            //    cbAssetModel.SelectedIndex = -1;
            //    cbSupplier.SelectedIndex = 0;
            //    dtpPurchaseDate.Value = DateTime.Today;
            //    dtpGivenDate.Value = DateTime.Today;
            //    cbTrnType.SelectedIndex = 0;
            //    cbToComp.SelectedIndex = 0;

            //    cbToBranch.SelectedIndex = -1;
            //    txtToEcode.Text = "";
            //    txtToEName.Text = "";
            //    dtConfigDetl.Rows.Clear();
            //    gvAssetConfigDetails.Rows.Clear();
            //    cbStatus.SelectedIndex = 0;

            //    cbAssetType.Enabled = true;
            //    cbAssetMake.Enabled = true;
            //    cbAssetModel.Enabled = true;
            //    cbSupplier.Enabled = true;
            //    dtpPurchaseDate.Enabled = true;
            //    txtAssetCost.ReadOnly = false;
            //    txtAssetTagNo.ReadOnly = false;
            //    dtpGivenDate.Enabled = true;
            //    cbTrnType.Enabled = true;
            //    txtAssetTagNo.Text = "";
            //    txtAssetCost.Text = "";
            //}
        }

        private void txtAssetSLNo_Validated(object sender, EventArgs e)
        {
            if (txtAssetSLNo.Text != "")
            {
                isHeadUpdate = true;
                FillAssetsDetails();
                FillAssetConfigDetails();
            }
            else
            {
                isHeadUpdate = false;
                isDetlUpdate = false;
                cbAssetType.SelectedIndex = 0;
                cbAssetMake.SelectedIndex = -1;
                cbAssetModel.SelectedIndex = -1;
                cbSupplier.SelectedIndex = 0;
                dtpPurchaseDate.Value = DateTime.Today;
                dtpGivenDate.Value = DateTime.Today;
                cbTrnType.SelectedIndex = 0;
                cbToComp.SelectedIndex = 0;

                cbToBranch.SelectedIndex = -1;
                txtToEcode.Text = "";
                txtToEName.Text = "";
                dtConfigDetl.Rows.Clear();
                gvAssetConfigDetails.Rows.Clear();
                cbStatus.SelectedIndex = 0;

                cbAssetType.Enabled = true;
                cbAssetMake.Enabled = true;
                cbAssetModel.Enabled = true;
                cbSupplier.Enabled = true;
                dtpPurchaseDate.Enabled = true;
                txtAssetCost.ReadOnly = false;
                txtAssetTagNo.ReadOnly = false;
                dtpGivenDate.Enabled = true;
                cbTrnType.Enabled = true;
                txtAssetTagNo.Text = "";
                txtAssetCost.Text = "";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            int iRec = 0;
            string strCmd = "";

            if (txtAssetSLNo.Text != "")
            {
                DialogResult dlgResult = MessageBox.Show("Do you want Delete this Record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    try
                    {
                        strCmd = "DELETE FROM FIXED_ASSETS_SYSTEM_CONFIG WHERE FASC_ASSET_ID='" + txtAssetSLNo.Text.ToString() + "' ";
                        iRec = objSQLdb.ExecuteSaveData(strCmd);

                        strCmd = "DELETE FROM FIXED_ASSETS_MOVEMENT_REG WHERE FAMR_ASSET_SL_NO='" + txtAssetSLNo.Text.ToString() + "'";
                        iRec = objSQLdb.ExecuteSaveData(strCmd);

                        strCmd = "DELETE FROM FIXED_ASSETS_HEAD WHERE FAH_ASSET_SL_NO='" + txtAssetSLNo.Text.ToString() + "'";
                        iRec = objSQLdb.ExecuteSaveData(strCmd);



                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                    if (iRec > 0)
                    {
                        MessageBox.Show("Data Deleted Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(null, null);
                       
                    }
                    else
                    {
                        MessageBox.Show("Data Not Deleted", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                }
            }
            else
            {
                MessageBox.Show("Please Enter Asset SLNO ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAssetSLNo.Focus();
            }
        }

        private void txtAssetCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && e.KeyChar != '.')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

        }

        private void txtAssetTagNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
            e.Handled = (e.KeyChar == (char)Keys.Space);           
        }

      
      

        
       
       
    }
}
