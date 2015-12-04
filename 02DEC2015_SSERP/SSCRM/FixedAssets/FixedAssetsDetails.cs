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
    public partial class FixedAssetsDetails : Form
    {
        SQLDB objSQLdb = null;
        FixedAssetsDB objAssetDB = null;
        Int32 TrnNo = 0;
        bool isHeadUpdate = false;
        bool isDetlUpdate = false;
        Int32 AssetMovementId = 0;
        Int32 AssetLastMovementId = 0;


        public FixedAssetsDetails()
        {
            InitializeComponent();
        }

        private void FixedAssetsDetails_Load(object sender, EventArgs e)
        {
            FillAssetType();
            FillSuppliersData();
            FillToCompanyData();
            FillFromCompanyData();
            cbTrnType.SelectedIndex = 0;
            dtpGivenDate.Value = DateTime.Today;
            dtpPurchaseDate.Value = DateTime.Today;
            cbStatus.SelectedIndex = 0;

            gvAssetMovementDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                   System.Drawing.FontStyle.Regular);
            cbFrmComp.SelectedValue = CommonData.CompanyCode;
            cbFrmBranch.SelectedValue = CommonData.BranchCode;
        }

        private void FillAssetType()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT  DISTINCT(FAM_ASSET_TYPE) FROM FIXED_ASSETS_MAS ORDER BY FAM_ASSET_TYPE ASC";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";
                    dt.Rows.InsertAt(row,0);
                   
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
                    //row[0] = "--Select--";
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
                    string strCommand = "SELECT  DISTINCT(FAM_ASSET_MODEL) FROM FIXED_ASSETS_MAS "+
                                        " WHERE FAM_ASSET_MAKE='"+ cbAssetMake.SelectedValue.ToString() +
                                        "' ORDER BY FAM_ASSET_MODEL ASC";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";
                    //row[0] = "--Select--";
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
                string strCommand = "SELECT SM_SUPPLIER_CODE,SM_SUPPLIER_NAME FROM SUPPLIER_MASTER "+
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

        private void FillFromCompanyData()
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

                    cbFrmComp.DataSource = dt;
                    cbFrmComp.DisplayMember = "CM_COMPANY_NAME";
                    cbFrmComp.ValueMember = "CM_COMPANY_CODE";
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


        private void FillFromBranches()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbFrmBranch.DataSource = null;
            try
            {
                if (cbFrmComp.SelectedIndex > 0)
                {

                    string strCommand = "SELECT BRANCH_NAME ,BRANCH_CODE  FROM BRANCH_MAS " +
                                        " WHERE COMPANY_CODE='" + cbFrmComp.SelectedValue.ToString() +
                                        "' Order by BRANCH_NAME ";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbFrmBranch.DataSource = dt;
                    cbFrmBranch.DisplayMember = "BRANCH_NAME";
                    cbFrmBranch.ValueMember = "BRANCH_CODE";
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
            //if (cbAssetType.SelectedIndex > 0)
            //{
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
            //}
            //else
            //{
            //    txtAssetSLNo.Text = "";
            //}
        }

        private bool CheckData()
        {
            bool flag = true;
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();

            string strCommand = "SELECT  FAMR_GIVEN_DATE FROM FIXED_ASSETS_MOVEMENT_REG "+
                                " WHERE FAMR_ASSET_SL_NO='"+ txtAssetSLNo.Text.ToString() +
                                "' AND FAMR_RETURN_DATE IS NULL";
            dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
            if (dt.Rows.Count > 0)
            {

                if (Convert.ToDateTime(dt.Rows[0]["FAMR_GIVEN_DATE"].ToString()) > dtpGivenDate.Value)
                {
                    flag = false;
                    MessageBox.Show("Please Select Valid Issue Date", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpGivenDate.Focus();
                    return false;
                }
            }
            if (txtAssetSLNo.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Asset SlNo", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAssetSLNo.Focus();
                return false;
            }
            if (cbAssetType.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Asset Type", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbAssetType.Focus();
                return false;
            }
            if (cbAssetMake.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Asset Make", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbAssetMake.Focus();
                return false;
            }
            //if (cbSupplier.SelectedIndex == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Select Supplier Name", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    cbSupplier.Focus();
            //return false;
            //}
            if (cbStatus.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Asset Status", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbStatus.Focus();
                return false;
            }
            if (cbTrnType.SelectedIndex > 0)
            {
                if (cbTrnType.SelectedItem.ToString() == "BR2BR")
                {
                    if (cbFrmComp.SelectedIndex == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Select From Company", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbFrmComp.Focus();
                        return false;
                    }
                    else if (cbFrmBranch.SelectedIndex == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Select From Branch", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbFrmBranch.Focus();
                        return false;
                    }


                    else if (cbToComp.SelectedIndex == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Select To Company", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbToComp.Focus();
                        return false;
                    }
                    else if (cbToBranch.SelectedIndex == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Select To Branch ", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbToBranch.Focus();
                        return false;
                    }
                }

                else if (cbTrnType.SelectedItem.ToString() == "GC2BR")
                {
                    if (txtfrmEName.Text.Length == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Enter From Ecode ", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtFrmEcode.Focus();
                        return false;
                    }
                    else if (cbToComp.SelectedIndex == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Select To Company ", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbToComp.Focus();
                        return false;
                    }
                    else if (cbToBranch.SelectedIndex == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Select ToBranch ", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbToBranch.Focus();
                        return false;
                    }


                    else if (cbFrmComp.SelectedIndex == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Select From Company ", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbFrmComp.Focus();
                        return false;
                    }
                    else if (cbFrmBranch.SelectedIndex == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Select From Branch ", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbFrmBranch.Focus();
                        return false;
                    }


                }
                else if (cbTrnType.SelectedItem.ToString() == "BR2GC")
                {
                    if (cbFrmComp.SelectedIndex == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Select From Company ", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbFrmComp.Focus();
                        return false;
                    }
                    else if (cbFrmBranch.SelectedIndex == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Select From Branch ", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbFrmBranch.Focus();
                        return false;
                    }


                    else if (txtToEName.Text.Length == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Enter To Ecode ", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtToEcode.Focus();
                        return false;
                    }
                    else if (cbToComp.SelectedIndex == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Select To Company ", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbToComp.Focus();
                        return false;
                    }
                    else if (cbToBranch.SelectedIndex == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Select ToBranch ", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbToBranch.Focus();
                        return false;
                    }

                }
                else if (cbTrnType.SelectedItem.ToString() == "GC2GC")
                {
                    if (txtfrmEName.Text.Length == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Enter From Ecode ", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtFrmEcode.Focus();
                        return false;
                    }
                    else if (txtToEName.Text.Length == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Enter To Ecode ", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtToEcode.Focus();
                        return false;
                    }
                    else if (cbFrmComp.SelectedIndex == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Select From Company ", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbFrmComp.Focus();
                        return false;
                    }

                    else if (cbFrmBranch.SelectedIndex == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Select From Branch ", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbFrmBranch.Focus();
                        return false;
                    }

                    else if (cbToComp.SelectedIndex == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Select To Company ", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbToComp.Focus();
                        return false;
                    }

                    else if (cbToBranch.SelectedIndex == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Select ToBranch ", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbToBranch.Focus();
                        return false;
                    }


                }
            }
            else
            {
                flag = false;
                MessageBox.Show("Please Select Transaction Type ", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbTrnType.Focus();
                return false;

            }
            return flag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            if (CheckData() == true)
            {
                if (SaveAssetHeadDetails() > 0)
                {
                    if (SaveAssetMovementDetails() > 0)
                    {
                        MessageBox.Show("Data Saved Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(null, null);
                        isHeadUpdate = false;
                        isDetlUpdate = false;
                        TrnNo = 0;
                       
                    }
                    else
                    {
                        string strCmd = "DELETE FROM FIXED_ASSETS_HEAD WHERE FAH_ASSET_SL_NO='" + txtAssetSLNo.Text.ToString() + "'";
                        int iRec = objSQLdb.ExecuteSaveData(strCmd);

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
                                    "',FAH_ASSET_MAKE='"+ cbAssetMake.SelectedValue.ToString() +
                                    "',FAH_MODEL='"+ cbAssetModel.SelectedValue.ToString() +
                                    "', FAH_ASSET_COST='" + Convert.ToDouble(txtAssetCost.Text).ToString("0.00") +
                                    "', FAH_ASSET_TAG_NO='" + txtAssetTagNo.Text.ToString().Replace(" ","").ToUpper() +
                                    "',FAH_MODIFIED_BY='"+ CommonData.LogUserId +
                                    "',FAH_MODIFIED_DATE=getdate()"+
                                    " WHERE FAH_ID="+ txtAssetSLNo.Tag +" ";
                   
                }
                else if(isHeadUpdate == false)
                {

                    strCommand = "INSERT INTO FIXED_ASSETS_HEAD(FAH_ASSET_TYPE " +
                                                             ", FAH_ASSET_MAKE " +
                                                             ", FAH_MODEL " +
                                                             ", FAH_ASSET_SL_NO " +
                                                             //", FAH_SUPPLIER_ID " +
                                                             //", FAH_REC_DATE " +
                                                             ", FAH_ASSET_COST " +
                                                             ", FAH_ASSET_TAG_NO " +
                                                             ", FAH_CREATED_BY " +
                                                             ", FAH_CREATED_DATE " +
                                                             ")VALUES " +
                                                             "('" + cbAssetType.SelectedValue.ToString() +
                                                             "','" + cbAssetMake.SelectedValue.ToString() +
                                                             "','" + cbAssetModel.SelectedValue.ToString() +
                                                             "','" + txtAssetSLNo.Text.ToString() +
                                                             //"','" + cbSupplier.SelectedValue.ToString() +
                                                             //"','" + Convert.ToDateTime(dtpPurchaseDate.Value).ToString("dd/MMM/yyyy") +
                                                             "', '" + Convert.ToDouble(txtAssetCost.Text).ToString("0.00") +
                                                             "' , '" + txtAssetTagNo.Text.ToString().Replace(" ", "").ToUpper() + 
                                                             "', '" + CommonData.LogUserId +
                                                             "',getdate())";

                }

                iRes = objSQLdb.ExecuteSaveData(strCommand);
                if (iRes > 0)
                {
                    return iRes;
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


                if (txtFrmEcode.Text.Length == 0)
                {
                    txtFrmEcode.Text = "0";
                }
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
                                                                 ",'" + cbTrnType.SelectedItem.ToString() +
                                                                 "','" + cbFrmBranch.SelectedValue.ToString() +
                                                                 "'," + Convert.ToInt32(txtFrmEcode.Text) +
                                                                 ",'" + cbToBranch.SelectedValue.ToString() +
                                                                 "'," + Convert.ToInt32(txtToEcode.Text) +
                                                                 ",'" + Convert.ToDateTime(dtpGivenDate.Value).ToString("dd/MMM/yyyy") +
                                                                 "', '" + cbStatus.SelectedItem.ToString() +
                                                                 "', '" + txtContactNo.Text.ToString().Replace(" ", "") +
                                                                 "','" + txtAssetTagNo.Text.ToString().Replace(" ", "").ToUpper() +
                                                                 "','" + CommonData.LogUserId +
                                                                 "',getdate())";

                iRes = objSQLdb.ExecuteSaveData(strCommand);
                if (iRes > 0)
                {
                    return iRes;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iRes;


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

        private void cbFrmComp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFrmComp.SelectedIndex > 0)
            {
                FillFromBranches();

                if (isHeadUpdate == false)
                {
                    cbToComp.SelectedValue = cbFrmComp.SelectedValue.ToString();
                }
            }          
                      
        }

        private void cbToComp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbToComp.SelectedIndex > 0)
            {
                FillToBranches();
            }
        }

        private void txtFrmEcode_TextChanged(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            if (txtFrmEcode.Text.Length > 4)
            {
                try
                {
                    string strCmd = "SELECT MEMBER_NAME EName FROM EORA_MASTER WHERE ECODE=" + Convert.ToInt32(txtFrmEcode.Text) + "";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtfrmEName.Text = dt.Rows[0]["EName"].ToString();
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
                txtfrmEName.Text = "";
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

        private void FillAssetsDetails(string AssetSlNo)
        {
            objAssetDB = new FixedAssetsDB();
            DataTable dt = new DataTable();
            gvAssetMovementDetails.Rows.Clear();
            //if (txtAssetSLNo.Text != "")
            //{
                try
                {
                    dt = objAssetDB.GetFixedAssetsData(AssetSlNo).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        isHeadUpdate = true;

                        cbAssetType.Enabled = false;
                        cbAssetMake.Enabled = false;
                        cbAssetModel.Enabled = false;
                        cbSupplier.Enabled = false;
                        dtpPurchaseDate.Enabled = false;

                        txtAssetCost.ReadOnly = true;
                        txtAssetTagNo.ReadOnly = true;

                        if (dt.Rows[0]["StatusAt"].ToString() == "GC")
                        {
                            cbTrnType.Items.Clear();
                            cbTrnType.Items.Add("--Select--");
                            cbTrnType.Items.Add("GC2GC");
                            cbTrnType.Items.Add("GC2BR");
                            cbTrnType.SelectedItem = "--Select--";


                        }

                        else if (dt.Rows[0]["StatusAt"].ToString() == "BR")
                        {
                            cbTrnType.Items.Clear();
                            cbTrnType.Items.Add("--Select--");
                            cbTrnType.Items.Add("BR2BR");
                            cbTrnType.Items.Add("BR2GC");
                            cbTrnType.SelectedItem = "--Select--";


                        }

                        if (dt.Rows[0]["AtComp"].ToString() != "")
                        {
                            cbFrmComp.Enabled = false;
                            cbFrmComp.SelectedValue = dt.Rows[0]["AtComp"].ToString();
                        }
                        if (dt.Rows[0]["AtBranch"].ToString() != "")
                        {
                            cbFrmBranch.Enabled = false;
                            cbFrmBranch.SelectedValue = dt.Rows[0]["AtBranch"].ToString();
                        }
                        if (dt.Rows[0]["AtEmp"].ToString() != "")
                        {
                            txtFrmEcode.ReadOnly = true;
                            txtFrmEcode.Text = dt.Rows[0]["AtEmp"].ToString();
                        }

                        AssetLastMovementId = Convert.ToInt32(dt.Rows[0]["AtTrnID"].ToString());

                        txtAssetSLNo.Tag = dt.Rows[0]["AssetId"].ToString();
                        cbAssetType.SelectedValue = dt.Rows[0]["Asset_Type"].ToString();
                        cbAssetMake.SelectedValue = dt.Rows[0]["Asset_Make"].ToString();
                        cbAssetModel.SelectedValue = dt.Rows[0]["Asset_Model"].ToString();
                        //cbSupplier.SelectedValue = dt.Rows[0]["Supplier_Id"].ToString();
                        //dtpPurchaseDate.Value = Convert.ToDateTime(dt.Rows[0]["Purchase_Date"]);
                        cbStatus.SelectedItem = dt.Rows[0]["Status"].ToString();
                        cbTrnType.Text = dt.Rows[0]["Trn_Type"].ToString();

                        dtpGivenDate.Value = Convert.ToDateTime(dt.Rows[0]["Given_Date"].ToString());

                        txtAssetCost.Text = dt.Rows[0]["AssetCost"].ToString();
                        txtAssetTagNo.Text = dt.Rows[0]["TagNo"].ToString();

                        for (int ivar = 0; ivar < dt.Rows.Count; ivar++)
                        {
                            gvAssetMovementDetails.Rows.Add();

                            gvAssetMovementDetails.Rows[ivar].Cells["SLNO"].Value = (ivar + 1).ToString();
                            gvAssetMovementDetails.Rows[ivar].Cells["transactionNo"].Value = dt.Rows[ivar]["Trn_No"].ToString();
                            gvAssetMovementDetails.Rows[ivar].Cells["AssetId"].Value = dt.Rows[ivar]["Movement_Id"].ToString();
                            gvAssetMovementDetails.Rows[ivar].Cells["GivenDate"].Value = Convert.ToDateTime(dt.Rows[ivar]["Given_Date"].ToString()).ToString("dd/MMM/yyyy");
                            gvAssetMovementDetails.Rows[ivar].Cells["TrnType"].Value = dt.Rows[ivar]["Trn_Type"].ToString();
                            gvAssetMovementDetails.Rows[ivar].Cells["fromComp"].Value = dt.Rows[ivar]["From_Comp_Name"].ToString();
                            gvAssetMovementDetails.Rows[ivar].Cells["fromBranch"].Value = dt.Rows[ivar]["From_Branch_Name"].ToString();
                            gvAssetMovementDetails.Rows[ivar].Cells["FromName"].Value = dt.Rows[ivar]["From_Name"].ToString();
                            gvAssetMovementDetails.Rows[ivar].Cells["FromEcode"].Value = dt.Rows[ivar]["From_Ecode"].ToString();
                            gvAssetMovementDetails.Rows[ivar].Cells["ToComp"].Value = dt.Rows[ivar]["To_Comp_Name"].ToString();
                            gvAssetMovementDetails.Rows[ivar].Cells["ToBranch"].Value = dt.Rows[ivar]["To_Branch_Name"].ToString();
                            gvAssetMovementDetails.Rows[ivar].Cells["ToName"].Value = dt.Rows[ivar]["To_Name"].ToString();
                            gvAssetMovementDetails.Rows[ivar].Cells["ToEcode"].Value = dt.Rows[ivar]["To_Ecode"].ToString();
                            gvAssetMovementDetails.Rows[ivar].Cells["ContactNo"].Value = dt.Rows[ivar]["ContNo"].ToString();
                        }
                    }
                    else
                    {
                        isHeadUpdate = false;
                        isDetlUpdate = false;
                        //GenerateAssetSLNo();
                        cbAssetType.SelectedIndex = 0;
                        cbAssetMake.SelectedIndex = -1;
                        cbAssetModel.SelectedIndex = -1;
                        cbSupplier.SelectedIndex = 0;
                        dtpPurchaseDate.Value = DateTime.Today;
                        dtpGivenDate.Value = DateTime.Today;
                        cbTrnType.SelectedIndex = 0;
                        cbToComp.SelectedIndex = 0;
                        cbFrmComp.SelectedIndex = 0;
                        cbFrmBranch.SelectedIndex = -1;
                        cbToBranch.SelectedIndex = -1;
                        txtFrmEcode.Text = "";
                        txtfrmEName.Text = "";
                        txtToEcode.Text = "";
                        txtToEName.Text = "";
                        gvAssetMovementDetails.Rows.Clear();
                        cbStatus.SelectedIndex = 0;
                        txtAssetTagNo.Text = "";
                        txtAssetCost.Text = "";

                        cbAssetType.Enabled = true;
                        cbAssetMake.Enabled = true;
                        cbAssetModel.Enabled = true;
                        cbSupplier.Enabled = true;
                        dtpPurchaseDate.Enabled = true;
                        cbFrmBranch.Enabled = true;
                        cbFrmComp.Enabled = true;
                        txtFrmEcode.ReadOnly = false;
                        txtAssetCost.ReadOnly = false;
                        txtAssetTagNo.ReadOnly = false;

                        cbTrnType.Items.Clear();
                        cbTrnType.Items.Add("--Select--");
                        cbTrnType.Items.Add("BR2BR");
                        cbTrnType.Items.Add("BR2GC");
                        cbTrnType.Items.Add("GC2BR");
                        cbTrnType.Items.Add("GC2GC");
                        cbTrnType.SelectedItem = "--Select--";
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
            //}
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtAssetSLNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == ' ';
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            isHeadUpdate = false;
            isDetlUpdate = false;
            txtAssetSLNo.Text = "";
            cbAssetType.SelectedIndex = 0;
            cbAssetMake.SelectedIndex = -1;
            cbAssetModel.SelectedIndex = -1;
            cbSupplier.SelectedIndex = 0;
            dtpPurchaseDate.Value = DateTime.Today;
            dtpGivenDate.Value = DateTime.Today;
            cbTrnType.SelectedIndex = 0;
            cbToComp.SelectedIndex = 0;
            cbFrmComp.SelectedIndex = 0;
            cbFrmBranch.SelectedIndex = -1;
            cbToBranch.SelectedIndex = -1;
            txtFrmEcode.Text = "";
            txtfrmEName.Text = "";
            txtToEcode.Text = "";
            txtToEName.Text = "";
            txtAssetTagNo.Text = "";
            txtAssetCost.Text = "";
            gvAssetMovementDetails.Rows.Clear();
            cbAssetType.Enabled = true;
            cbAssetMake.Enabled = true;
            cbAssetModel.Enabled = true;
            cbSupplier.Enabled = true;
            dtpPurchaseDate.Enabled = true;
            cbStatus.SelectedIndex = 0;
            TrnNo = 0;
            cbFrmBranch.Enabled = true;
            cbFrmComp.Enabled = true;
            txtFrmEcode.ReadOnly = false;
            txtAssetCost.ReadOnly = false;
            txtAssetTagNo.ReadOnly = false;
            cbTrnType.Items.Clear();
            cbTrnType.Items.Add("--Select--");
            cbTrnType.Items.Add("BR2BR");
            cbTrnType.Items.Add("BR2GC");
            cbTrnType.Items.Add("GC2BR");
            cbTrnType.Items.Add("GC2GC");
            cbTrnType.SelectedItem = "--Select--";
 }

        private void txtAssetSLNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtAssetSLNo.Text != "")
            {
                FillAssetsDetails(txtAssetSLNo.Text.ToString());
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
                cbFrmComp.SelectedIndex = 0;
                cbFrmBranch.SelectedIndex = -1;
                cbToBranch.SelectedIndex = -1;
                txtFrmEcode.Text = "";
                txtfrmEName.Text = "";
                txtToEcode.Text = "";
                txtToEName.Text = "";
                gvAssetMovementDetails.Rows.Clear();
                cbStatus.SelectedIndex = 0;
                
                cbAssetType.Enabled = true;
                cbAssetMake.Enabled = true;
                cbAssetModel.Enabled = true;
                cbSupplier.Enabled = true;
                dtpPurchaseDate.Enabled = true;
                cbFrmBranch.Enabled = true;
                cbFrmComp.Enabled = true;
                txtFrmEcode.ReadOnly = false;

                txtAssetTagNo.Text = "";
                txtAssetCost.Text = "";

                txtAssetCost.ReadOnly = false;
                txtAssetTagNo.ReadOnly = false;

                cbTrnType.Items.Clear();
                cbTrnType.Items.Add("--Select--");
                cbTrnType.Items.Add("BR2BR");
                cbTrnType.Items.Add("BR2GC");
                cbTrnType.Items.Add("GC2BR");
                cbTrnType.Items.Add("GC2GC");
                cbTrnType.SelectedItem = "--Select--";

                
            }
        }

        private void gvAssetMovementDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvAssetMovementDetails.Columns["Edit"].Index)
            {
                isDetlUpdate = true;

                AssetMovementId = Convert.ToInt32(gvAssetMovementDetails.Rows[e.RowIndex].Cells["AssetId"].Value.ToString());
                if (AssetMovementId.Equals(AssetLastMovementId))
                {
                    if (gvAssetMovementDetails.Rows[e.RowIndex].Cells["TrnType"].Value.ToString().Substring(0, 2) == "GC")
                    {
                        cbTrnType.Items.Clear();
                        cbTrnType.Items.Add("--Select--");
                        cbTrnType.Items.Add("GC2GC");
                        cbTrnType.Items.Add("GC2BR");
                        cbTrnType.SelectedItem = "--Select--";


                    }
                    else
                    {
                        cbTrnType.Items.Clear();
                        cbTrnType.Items.Add("--Select--");
                        cbTrnType.Items.Add("BR2BR");
                        cbTrnType.Items.Add("BR2GC");
                        cbTrnType.SelectedItem = "--Select--";


                    }
                    TrnNo = Convert.ToInt32(gvAssetMovementDetails.Rows[e.RowIndex].Cells["transactionNo"].Value.ToString());
                    //dtpGivenDate.Value = Convert.ToDateTime(gvAssetMovementDetails.Rows[e.RowIndex].Cells["GivenDate"].Value);
                    cbTrnType.Text = gvAssetMovementDetails.Rows[e.RowIndex].Cells["TrnType"].Value.ToString();
                    cbFrmComp.Text = gvAssetMovementDetails.Rows[e.RowIndex].Cells["fromComp"].Value.ToString();
                    cbFrmBranch.Text = gvAssetMovementDetails.Rows[e.RowIndex].Cells["fromBranch"].Value.ToString();

                    cbToComp.Text = gvAssetMovementDetails.Rows[e.RowIndex].Cells["ToComp"].Value.ToString();
                    cbToBranch.Text = gvAssetMovementDetails.Rows[e.RowIndex].Cells["ToBranch"].Value.ToString();
                    txtFrmEcode.Text = gvAssetMovementDetails.Rows[e.RowIndex].Cells["FromEcode"].Value.ToString();
                    txtToEcode.Text = gvAssetMovementDetails.Rows[e.RowIndex].Cells["ToEcode"].Value.ToString();

                    
                }
                else
                {
                    MessageBox.Show("This Data Can Not Be Manipulated","Fixed Asset Movement Details",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
               
            }

            if (e.ColumnIndex == gvAssetMovementDetails.Columns["Delete"].Index)
            {
                DialogResult dlgResult;
                AssetMovementId = Convert.ToInt32(gvAssetMovementDetails.Rows[e.RowIndex].Cells["AssetId"].Value.ToString());
                //if (AssetMovementId.Equals(AssetLastMovementId))
                //{
                // dlgResult = MessageBox.Show("This is Asset Last Movement Record?(Do you want Delete this Record?)", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //}
                //else
                //{
                dlgResult = MessageBox.Show("Do you want Delete this Record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //}
                if (dlgResult == DialogResult.Yes)
                {
                    DataGridViewRow dgvr = gvAssetMovementDetails.Rows[e.RowIndex];
                    gvAssetMovementDetails.Rows.Remove(dgvr);
                    MessageBox.Show("Data Deleted Successfully", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (gvAssetMovementDetails.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvAssetMovementDetails.Rows.Count; i++)
                        {
                            gvAssetMovementDetails.Rows[i].Cells["SlNo"].Value = (i + 1).ToString();
                        }
                    }
                    
                    //objSQLdb = new SQLDB();
                    //int iRes = 0;
                    //try
                    //{

                    //    string strCommand = "DELETE FROM FIXED_ASSETS_MOVEMENT_REG WHERE FAMR_ID='" + AssetMovementId + "' ";
                    //    iRes = objSQLdb.ExecuteSaveData(strCommand);
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show(ex.ToString());
                    //}
                    //if (iRes > 0)
                    //{
                    //    MessageBox.Show("Selected Data Deleted Successfully", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    FillAssetsDetails(txtAssetSLNo.Text.ToString());
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Data Not Deleted", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}

                }
                
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
                        //flagUpdate = true;
                        strCmd = "DELETE FROM FIXED_ASSETS_MOVEMENT_REG WHERE FAMR_ASSET_SL_NO='" + txtAssetSLNo.Text.ToString() + "'";
                        iRec = objSQLdb.ExecuteSaveData(strCmd);

                        strCmd = "";

                        strCmd = "DELETE FROM FIXED_ASSETS_HEAD WHERE FAH_ASSET_SL_NO='" + txtAssetSLNo.Text.ToString() + "'";
                        iRec = objSQLdb.ExecuteSaveData(strCmd);



                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                    if (iRec > 0)
                    {
                        MessageBox.Show("Data Deleted SucessFully", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(null, null);
                        //flagUpdate = false;
                        
                    }
                    else
                    {
                        MessageBox.Show("Data Not Deleted ", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                }
            }
            else
            {
                MessageBox.Show("Please Enter Asset SLNO ", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAssetSLNo.Focus();
            }
            
        }

        private void txtFrmEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
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

        private void cbFrmBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFrmBranch.SelectedIndex > 0)
            {
                if (isHeadUpdate == false)
                {
                    cbToBranch.SelectedValue = cbFrmBranch.SelectedValue.ToString();
                }
            }
        }

        private void txtAssetSLNo_Validated(object sender, EventArgs e)
        {
            if (isHeadUpdate == false)
            {
                isHeadUpdate = false;
                isDetlUpdate = false;
                txtAssetSLNo.Text = "";
                GenerateAssetSLNo();
                //cbAssetType.SelectedIndex = 0;
                //cbAssetMake.SelectedIndex = -1;
                //cbAssetModel.SelectedIndex = -1;
                //cbSupplier.SelectedIndex = 0;
                //dtpPurchaseDate.Value = DateTime.Today;
                //dtpGivenDate.Value = DateTime.Today;
                //cbTrnType.SelectedIndex = 0;
                //cbToComp.SelectedIndex = 0;
                //cbFrmComp.SelectedIndex = 0;
                //cbFrmBranch.SelectedIndex = -1;
                //cbToBranch.SelectedIndex = -1;
                //txtFrmEcode.Text = "";
                //txtfrmEName.Text = "";
                //txtToEcode.Text = "";
                //txtToEName.Text = "";
                //gvAssetMovementDetails.Rows.Clear();
                //cbAssetType.Enabled = true;
                //cbAssetMake.Enabled = true;
                //cbAssetModel.Enabled = true;
                //cbSupplier.Enabled = true;
                //dtpPurchaseDate.Enabled = true;
                //cbStatus.SelectedIndex = 0;
                //TrnNo = 0;
                //cbFrmBranch.Enabled = true;
                //cbFrmComp.Enabled = true;
                //txtFrmEcode.ReadOnly = false;
                //cbTrnType.Items.Clear();
                //cbTrnType.Items.Add("--Select--");
                //cbTrnType.Items.Add("BR2BR");
                //cbTrnType.Items.Add("BR2GC");
                //cbTrnType.Items.Add("GC2BR");
                //cbTrnType.Items.Add("GC2GC");
                //cbTrnType.SelectedItem = "--Select--";
                //txtAssetCost.ReadOnly = false;
                //txtAssetTagNo.ReadOnly = false;
                //txtAssetTagNo.Text = "";
                //txtAssetCost.Text = "";
            }
        }

        private void txtAssetTagNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
            e.Handled = (e.KeyChar == (char)Keys.Space);
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
              
      

      
    }
}
