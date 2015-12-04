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
    public partial class NewAssetIssue : Form
    {
        SQLDB objSQLdb = null;
        FixedAssetsDB objFixedAssetDB = null;
        Int32 TrnNo = 0;
        bool flagUpdate = false;
        bool isDetlUpdate = false;
        Int32 AssetMovementId = 0;
        public NewAssetIssue()
        {
            InitializeComponent();
        }

        private void NewAssetIssue_Load(object sender, EventArgs e)
        {
            FillAssetType();
            FillCompanyData();
            cbTrnType.SelectedIndex = 0;
            cbStatus.SelectedIndex = 0;
            FillBranchData();
        }
        private void FillAssetType()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT  DISTINCT(FAM_ASSET_TYPE) FROM FIXED_ASSETS_MAS  WHERE FAM_ASSET_TYPE<>'CPU' ORDER BY FAM_ASSET_TYPE ASC";
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
            //cbAssetModel.DataSource = null;
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
        private void GenerateAssetSLNo()
        {
           
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                if (flagUpdate == false)
                {
                    if (cbAssetType.SelectedIndex > 0)
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
                flagUpdate = false;
                FillAssetMakeData();
                GenerateAssetSLNo();

            }
            else
            {
                cbAssetMake.DataSource = null;
                txtAssetSLNo.Text = "";
            }
        }

        private void cbAssetMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAssetMake.SelectedIndex > 0)
            {
                FillAssetModelData();
            }
            else
            {
                cbAssetModel.DataSource = null; 
            }
           
        }

     

        private void txtAssetSLNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == ' ';
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtAssetTagNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == ' ';
            e.KeyChar = char.ToUpper(e.KeyChar);
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
        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();

            try
            {
                string strCmd = "SELECT DISTINCT CM_COMPANY_NAME ,CM_COMPANY_CODE " +
                                   " FROM USER_BRANCH " +
                                   " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                   " INNER JOIN COMPANY_MAS ON CM_COMPANY_CODE = COMPANY_CODE " +
                                   " WHERE UB_USER_ID ='" + CommonData.LogUserId +
                                   "' ORDER BY CM_COMPANY_NAME";

                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbToComp.DataSource = dt;
                    cbToComp.DisplayMember = "CM_COMPANY_NAME";
                    cbToComp.ValueMember = "CM_COMPANY_CODE";
                }

                cbToComp.SelectedValue = CommonData.CompanyCode;
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


        private void FillBranchData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();

            try
            {
                if (cbToComp.SelectedIndex > 0)
                {
                    string strCommand = "SELECT DISTINCT BRANCH_NAME,BRANCH_CODE " +
                                        " FROM USER_BRANCH " +
                                        " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                        " WHERE COMPANY_CODE ='" + cbToComp.SelectedValue.ToString() +
                                        "' AND UB_USER_ID ='" + CommonData.LogUserId +
                                        "' ORDER BY BRANCH_NAME ASC";
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


                cbToBranch.SelectedValue = CommonData.BranchCode;
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

        private bool CheckData()
        {
            bool flag = true;
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();

            string strCommand = "SELECT FAMR_TRN_NO FROM FIXED_ASSETS_MOVEMENT_REG " +
                                " WHERE FAMR_ASSET_SL_NO='" + txtAssetSLNo.Text.ToString() + "'";

            dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

            if (dt.Rows.Count > 1)
            {
                flag = false;
                MessageBox.Show("This Data Can Not Be Manipulated", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return flag;              
               
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
            if (cbAssetModel.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Asset Model", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbAssetMake.Focus();
                return false;
            }


            if (cbTrnType.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Issue Type ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbTrnType.Focus();
                return flag;

            }
            if (cbTrnType.SelectedIndex == 2)
            {
                if (txtEName.Text.Length == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Enter  Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEcode.Focus();
                    return flag;
                }
            }
            if (cbStatus.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Asset Status ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbStatus.Focus();
                return flag;

            }

            if (cbToComp.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Company", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbToComp.Focus();
                return flag;

            }

            if (cbToBranch.SelectedIndex == 0 || cbToBranch.SelectedIndex == -1)
            {
                flag = false;
                MessageBox.Show("Please Select Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbToBranch.Focus();
                return flag;

            }


            return flag;
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb= new SQLDB();
            string strCmd = "";
            int iRec = 0;

            if (CheckData() == true)
            {

                if (SaveAssetHeadDetails() > 0)
                {
                    if (SaveAssetMovementDetails() > 0)
                    {
                     

                        MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(null, null);
                        GenerateAssetSLNo();
                        TrnNo = 0;
                        AssetMovementId = 0;
                        flagUpdate = false;
                        isDetlUpdate = false;
                    }

                    else
                    {
                        strCmd += "DELETE FROM FIXED_ASSETS_MOVEMENT_REG WHERE FAMR_ASSET_SL_NO='" + txtAssetSLNo.Text.ToString() + "'";

                        

                        strCmd += "DELETE FROM FIXED_ASSETS_HEAD WHERE FAH_ASSET_SL_NO='" + txtAssetSLNo.Text.ToString() + "'";


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
        #region "INSERT AND UPDATE DATA"
        private int SaveAssetHeadDetails()
        {
            objSQLdb= new SQLDB();
            int iRes = 0;
            string strCommand = "";
            try
            {
                if (txtAssetCost.Text == "")
                {
                    txtAssetCost.Text = "0";

                }
                if (txtAssetTagNo.Text == "")
                {
                    txtAssetTagNo.Text = "0";
                }
                if (flagUpdate == true)
                {
                    strCommand = "UPDATE FIXED_ASSETS_HEAD SET FAH_ASSET_TYPE='" + cbAssetType.SelectedValue.ToString()+
                                                              "',FAH_ASSET_MAKE='" + cbAssetMake.SelectedValue.ToString()+
                                                              "',FAH_MODEL='" + cbAssetModel.SelectedValue.ToString()+
                                                              "', FAH_ASSET_COST='" + Convert.ToDouble(txtAssetCost.Text).ToString("0.00") +
                                                              "', FAH_ASSET_TAG_NO='" + txtAssetTagNo.Text.ToString().Replace(" ", "").ToUpper() +
                                                              "',FAH_MODIFIED_BY='" + CommonData.LogUserId +
                                                              "',FAH_MODIFIED_DATE=getdate()" +
                                                              " WHERE FAH_ASSET_SL_NO='" + txtAssetSLNo.Text.ToString() + "' ";

                }
                else if (flagUpdate == false)
                {

                    strCommand = "INSERT INTO FIXED_ASSETS_HEAD(FAH_ASSET_TYPE " +
                                                             ", FAH_ASSET_MAKE " +
                                                             ", FAH_MODEL " +
                                                             ", FAH_ASSET_SL_NO " +
                        //", FAH_SUPPLIER_ID " +
                                                             ", FAH_REC_DATE " +
                                                             ", FAH_ASSET_COST " +
                                                             ", FAH_ASSET_TAG_NO " +
                                                             ", FAH_CREATED_BY " +
                                                             ", FAH_CREATED_DATE " +
                                                             ")VALUES " +
                                                             "('" + cbAssetType.SelectedValue.ToString() +
                                                             "','" + cbAssetMake.SelectedValue.ToString() +
                                                             "','" + cbAssetModel.SelectedValue.ToString()+
                                                             "','" + txtAssetSLNo.Text.ToString() +
                        //"','" + cbSupplier.SelectedValue.ToString() +
                                                             "','" + Convert.ToDateTime(dtpGivenDate.Value).ToString("dd/MMM/yyyy") +
                                                             "', '" + Convert.ToDouble(txtAssetCost.Text).ToString("0.00") +
                                                             "' , '" + txtAssetTagNo.Text.ToString().Replace(" ", "").ToUpper() +
                                                             "', '" + CommonData.LogUserId +
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

                if (txtEcode.Text.Length == 0)
                {
                    txtEcode.Text = "0";
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
                                                                 ", FAMR_ASSET_TAG_NO " +
                                                                 ", FAMR_CREATED_BY " +
                                                                 ", FAMR_CREATED_DATE " +
                                                                 ")VALUES " +
                                                                 "('" + txtAssetSLNo.Text.ToString() +
                                                                 "'," + TrnNo +
                                                                 ",'" + cbTrnType.Tag.ToString() +
                                                                 "','" + cbToBranch.SelectedValue.ToString() +
                                                                 "'," + Convert.ToInt32(txtEcode.Text) +
                                                                 ",'" + cbToBranch.SelectedValue.ToString() +
                                                                 "'," + Convert.ToInt32(txtEcode.Text) +
                                                                 ",'" + Convert.ToDateTime(dtpGivenDate.Value).ToString("dd/MMM/yyyy") +
                                                                 "', '" + cbStatus.SelectedItem.ToString() +
                                                                 "', '" + txtContactNo.Text.ToString().Replace(" ", "") +
                                                                 "','" + txtAssetTagNo.Text.ToString().Replace(" ", "").ToUpper() +
                                                                 "','" + CommonData.LogUserId +
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

        #endregion

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close(); 
            this.Dispose();
        }

        private void txtEcode_KeyUp(object sender, KeyEventArgs e)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();

            if (txtEcode.Text.Length > 4)
            {
                try
                {
                    string strCmd = "SELECT  MEMBER_NAME EmpName" +
                                              ",dept_name deptName" +
                                              ",desig_name DesigName" +
                                              ",HECD_EMP_MOBILE_NO ContactNo" +
                                              " FROM EORA_MASTER EM " +
                                              " INNER JOIN Dept_Mas DT ON  DT.dept_code= EM.DEPT_ID " +
                                              " INNER JOIN DESIG_MAS DS ON DS.desig_code=EM.DESG_ID " +
                                              " left join HR_EMP_CONTACT_DETL ON HECD_EORA_CODE=EM.ECODE " +
                                              " WHERE ECODE=" + Convert.ToInt32(txtEcode.Text) + "";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        txtEName.Text = dt.Rows[0]["EmpName"].ToString();
                        txtDept.Text = dt.Rows[0]["deptName"].ToString();
                        txtDesig.Text = dt.Rows[0]["DesigName"].ToString();
                        txtContactNo.Text = dt.Rows[0]["ContactNo"].ToString();
                    }
                    else
                    {

                        txtEName.Text = "";
                        txtDept.Text = "";
                        txtDesig.Text = "";
                        txtContactNo.Text = "";
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
                txtEName.Text = "";
                txtEName.Text = "";
                txtDept.Text = "";
                txtDesig.Text = "";
                txtContactNo.Text = "";
            }
        }
        private void FillAssetsDetails()
        {
            objFixedAssetDB = new FixedAssetsDB();
            DataTable dt = new DataTable();

            if (txtAssetSLNo.Text != "")
            {
                try
                {
                    dt = objFixedAssetDB.GetFixedAssetsData(txtAssetSLNo.Text.ToString()).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        flagUpdate = true;

                        //txtAssetTagNo.ReadOnly = true;
                        //txtAssetCost.ReadOnly = true;

                        //dtpGivenDate.Enabled = false;
                        cbToComp.Enabled = false;
                        cbToBranch.Enabled = false;
                        //txtEcode.ReadOnly = true;

                        txtAssetSLNo.Tag = dt.Rows[0]["AssetId"].ToString();

                        cbAssetType.SelectedValue = dt.Rows[0]["Asset_Type"].ToString();
                        cbAssetMake.SelectedValue = dt.Rows[0]["Asset_Make"].ToString();
                        cbAssetModel.SelectedValue = dt.Rows[0]["Asset_Model"].ToString();

                        dtpGivenDate.Value = Convert.ToDateTime(dt.Rows[0]["Given_Date"].ToString());
                        cbStatus.SelectedItem = dt.Rows[0]["Status"].ToString();

                        txtAssetTagNo.Text = dt.Rows[0]["TagNo"].ToString();
                        txtAssetCost.Text = dt.Rows[0]["AssetCost"].ToString();
                        AssetMovementId = Convert.ToInt32(dt.Rows[0]["Movement_Id"].ToString());

                        TrnNo = Convert.ToInt32(dt.Rows[0]["Trn_No"].ToString());
                        if (dt.Rows[0]["Trn_Type"].ToString().Equals("BR2GC"))
                        {
                            cbTrnType.Text = "BR2EMPLOYEE";
                        }
                        else
                        {
                            cbTrnType.Text = dt.Rows[0]["Trn_Type"].ToString();
                        }

                        cbToComp.SelectedValue = dt.Rows[0]["AtComp"].ToString();
                        cbToBranch.SelectedValue = dt.Rows[0]["AtBranch"].ToString();
                        txtDept.Text = dt.Rows[0]["DeptName"].ToString();
                        txtDesig.Text = dt.Rows[0]["DesigName"].ToString();
                        txtContactNo.Text = dt.Rows[0]["ContNo"].ToString();


                        string ToEcode = dt.Rows[0]["To_Ecode"].ToString();
                        txtEcode.Text = ToEcode;
                        txtEcode_KeyUp(null, null);

                        if (txtContactNo.Text.Length == 0)
                        {
                            txtContactNo.Text = dt.Rows[0]["ContNo"].ToString();
                        }




                    }
                    else if (dt.Rows.Count == 0)
                    {
                        GenerateAssetSLNo();

                        flagUpdate = false;
                        isDetlUpdate = false;

                        TrnNo = 0;
                        AssetMovementId = 0;
                        dtpGivenDate.Value = DateTime.Today;

                        cbToComp.SelectedIndex = 0;

                        cbToBranch.SelectedIndex = -1;
                        txtEcode.Text = "";
                        txtEName.Text = "";

                        cbAssetType.SelectedIndex = 0;
                        cbAssetModel.SelectedIndex = -1;
                        cbAssetMake.SelectedIndex = -1;


                        cbStatus.SelectedIndex = 0;
                        txtDesig.Text = "";
                        txtDept.Text = "";
                        dtpGivenDate.Enabled = true;

                        cbToComp.Enabled = true;
                        cbToBranch.Enabled = true;
                        txtEcode.ReadOnly = false;
                        txtAssetTagNo.ReadOnly = false;
                        txtAssetCost.ReadOnly = false;

                        txtAssetTagNo.Text = "";
                        txtAssetCost.Text = "";
                        cbTrnType.SelectedIndex = 0;

                        

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objFixedAssetDB = null;
                    dt = null;
                }
            }

        }

        private void txtAssetSLNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtAssetSLNo.Text != "")
            {
                FillAssetsDetails();
               
            }
            else
            {
                flagUpdate = false;
                isDetlUpdate = false;

                TrnNo = 0;
                AssetMovementId = 0;

                dtpGivenDate.Value = DateTime.Today;

                cbToComp.SelectedIndex = 0;

                cbToBranch.SelectedIndex = -1;
                txtEcode.Text = "";
                txtEName.Text = "";

                cbAssetType.SelectedIndex = 0;
                cbAssetModel.SelectedIndex = -1;
                cbAssetMake.SelectedIndex = -1;


                cbStatus.SelectedIndex = 0;

                txtDesig.Text = "";
                txtDept.Text = "";
                txtAssetCost.ReadOnly = false;
                txtAssetTagNo.ReadOnly = false;
                dtpGivenDate.Enabled = true;

                txtAssetTagNo.Text = "";
                txtAssetCost.Text = "";
            }

        }

        private void cbToComp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbToComp.SelectedIndex > 0)
            {
                FillBranchData();
            }
            else if (cbToComp.SelectedIndex == 0)
            {
                cbToBranch.DataSource = null;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GenerateAssetSLNo();
            flagUpdate = false;
            isDetlUpdate = false;
            txtAssetSLNo.Text = "";

            dtpGivenDate.Value = DateTime.Today;
            dtpGivenDate.Value = DateTime.Today;
            GenerateAssetSLNo();

            cbAssetType.SelectedIndex=0;
            cbAssetModel.SelectedIndex = -1;
            cbAssetMake.SelectedIndex = -1;


            cbToComp.SelectedIndex = 0;
            cbTrnType.SelectedIndex = 0;
            cbToBranch.SelectedIndex = -1;
            txtEcode.Text = "";
            txtEName.Text = "";

            txtAssetTagNo.Text = "";
            txtAssetCost.Text = "";

            dtpGivenDate.Enabled = true;
            cbToComp.Enabled = true;
            cbToBranch.Enabled = true;
            txtEcode.ReadOnly = false;
            cbTrnType.Enabled = true;
            cbStatus.SelectedIndex = 0;
            TrnNo = 0;
            txtDesig.Text = "";
            txtDept.Text = "";
            txtAssetCost.ReadOnly = false;
            txtAssetTagNo.ReadOnly = false;
          
            txtContactNo.Text = "";            
           
           

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
          objSQLdb= new SQLDB();
            int iRec = 0;
            DataTable dt = new DataTable();
            string strCmd = "";

            if (txtAssetSLNo.Text != "" && flagUpdate == true)
            {
                string strCommand = "SELECT FAMR_TRN_NO FROM FIXED_ASSETS_MOVEMENT_REG " +
                              " WHERE FAMR_ASSET_SL_NO='" + txtAssetSLNo.Text.ToString() + "'";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count == 1)
                {

                    DialogResult dlgResult = MessageBox.Show("Do you want Delete this Record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        try
                        {
                            //flagUpdate = true;
                            strCmd += "DELETE FROM FIXED_ASSETS_MOVEMENT_REG WHERE FAMR_ASSET_SL_NO='" + txtAssetSLNo.Text.ToString() + "'";


                        


                            strCmd += "DELETE FROM FIXED_ASSETS_HEAD WHERE FAH_ASSET_SL_NO='" + txtAssetSLNo.Text.ToString() + "'";
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


                        }
                        else
                        {
                            MessageBox.Show("Data Not Deleted ", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }


                    }
                }
                else
                {
                    MessageBox.Show("This Data Can Not Be Deleted", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            else
            {
                MessageBox.Show("Please Enter Asset SLNO ", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAssetSLNo.Focus();
            }
            


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

     
      
    }
}
