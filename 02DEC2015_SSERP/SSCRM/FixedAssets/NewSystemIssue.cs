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
    public partial class NewSystemIssue : Form
    {
        SQLDB objSQLdB = null;

        FixedAssetsDB objFixedAsssetDB = null;
        string strAssetType = "CPU";
        bool isDetlUpdate = false;
        Int32 AssetMovementId = 0;
        Int32 TrnNo = 0;
        bool flagUpdate = false;

        public NewSystemIssue()
        {
            InitializeComponent();
        }

        private void NewSystemIssue_Load(object sender, EventArgs e)
        {
            GenerateAssetSLNo();
            dtpGivenDate.Value = DateTime.Today;
            FillCompanyData();
            cbTrnType.SelectedIndex = 0;
            cbStatus.SelectedIndex = 0;
            FillBranchData();


            FillMotherBoardConfigDetl();
            FillProcesorConfigDetl();
            FillHDDConfigDetl();
            FillDVDorCDConfigDetl();
            FillRAMConfigDetl();
            FillMoniterConfigDetl();
            FillPrinterConfigDetl();
            FillScannerConfigDetl();
            FillBiometrcsConfigDetl();
            FillWebcamConfigDetl();
            FillOperatingSysConfigDetl();
            FillAntivirusSysConfigDetl();

        }

        private void GenerateAssetSLNo()
        {

            objSQLdB = new SQLDB();
            DataTable dt = new DataTable();
            strAssetType = "CPU";
            try
            {


                string strNewNo = "SSGC/" + strAssetType + '/';

                string strCmd = "SELECT ISNULL(MAX(CAST(SUBSTRING(ISNULL(FAH_ASSET_SL_NO,'" + strNewNo +
                                "')," + (strNewNo.Length + 1) + "," + (strNewNo.Length + 6) + ") AS NUMERIC)),0)+1 " +
                                " FROM FIXED_ASSETS_HEAD WHERE FAH_ASSET_SL_NO LIKE '%" + strAssetType + "%'";
                dt = objSQLdB.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    txtAssetSLNo.Text = strNewNo + Convert.ToInt32(dt.Rows[0][0].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdB = null;
                dt = null;
            }

        }


        private void FillMotherBoardConfigDetl()
        {
            objSQLdB = new SQLDB();
            DataTable dt = new DataTable();

            try
            {

                string strCommand = "select DISTINCT FACM_ASSET_CONF_MODEL from FIXED_ASSETS_SYS_CONFID_MAS " +
                                       "where FACM_ASSET_CONF_TYPE='MOTHER BOARD' AND" +
                                       " FACM_ASSET_TYPE='" + strAssetType + "' ORDER BY FACM_ASSET_CONF_MODEL";

                dt = objSQLdB.ExecuteDataSet(strCommand).Tables[0];



                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbMotherboard.DataSource = dt;
                    cbMotherboard.DisplayMember = "FACM_ASSET_CONF_MODEL";
                    cbMotherboard.ValueMember = "FACM_ASSET_CONF_MODEL";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdB = null;
                dt = null;
            }

        }
        private void FillProcesorConfigDetl()
        {
            objSQLdB = new SQLDB();
            DataTable dt = new DataTable();

            try
            {

                string strCommand = "select DISTINCT FACM_ASSET_CONF_MODEL from FIXED_ASSETS_SYS_CONFID_MAS " +
                                       "where FACM_ASSET_CONF_TYPE='PROCESSOR' AND" +
                                       " FACM_ASSET_TYPE='" + strAssetType + "' ORDER BY FACM_ASSET_CONF_MODEL";

                dt = objSQLdB.ExecuteDataSet(strCommand).Tables[0];


                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbProcesor.DataSource = dt;

                    cbProcesor.DisplayMember = "FACM_ASSET_CONF_MODEL";
                    cbProcesor.ValueMember = "FACM_ASSET_CONF_MODEL";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdB = null;
                dt = null;
            }

        }
        private void FillHDDConfigDetl()
        {
            objSQLdB = new SQLDB();
            DataTable dt = new DataTable();

            try
            {

                string strCommand = "select DISTINCT FACM_ASSET_CONF_MODEL from FIXED_ASSETS_SYS_CONFID_MAS " +
                                       "where FACM_ASSET_CONF_TYPE='HARD DISC' AND" +
                                       " FACM_ASSET_TYPE='" + strAssetType + "' ORDER BY FACM_ASSET_CONF_MODEL";

                dt = objSQLdB.ExecuteDataSet(strCommand).Tables[0];


                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbHDD.DataSource = dt;
                    cbHDD.DisplayMember = "FACM_ASSET_CONF_MODEL";
                    cbHDD.ValueMember = "FACM_ASSET_CONF_MODEL";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdB = null;
                dt = null;
            }

        }
        private void FillDVDorCDConfigDetl()
        {
            objSQLdB = new SQLDB();
            DataTable dt = new DataTable();

            try
            {


                string strCommand = "select DISTINCT FACM_ASSET_CONF_MODEL from FIXED_ASSETS_SYS_CONFID_MAS " +
                                       "where FACM_ASSET_CONF_TYPE='DVD OR CD-ROM' AND" +
                                       " FACM_ASSET_TYPE='" + strAssetType + "' ORDER BY FACM_ASSET_CONF_MODEL";

                dt = objSQLdB.ExecuteDataSet(strCommand).Tables[0];


                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbDVDorCD.DataSource = dt;
                    cbDVDorCD.DisplayMember = "FACM_ASSET_CONF_MODEL";
                    cbDVDorCD.ValueMember = "FACM_ASSET_CONF_MODEL";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdB = null;
                dt = null;
            }

        }
        private void FillRAMConfigDetl()
        {

            objSQLdB = new SQLDB();
            DataTable dt = new DataTable();

            try
            {

                string strCommand = "select DISTINCT FACM_ASSET_CONF_MODEL from FIXED_ASSETS_SYS_CONFID_MAS " +
                                       "where FACM_ASSET_CONF_TYPE='RAM' AND" +
                                       " FACM_ASSET_TYPE='" + strAssetType + "' ORDER BY FACM_ASSET_CONF_MODEL";

                dt = objSQLdB.ExecuteDataSet(strCommand).Tables[0];


                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbRAM.DataSource = dt;
                    cbRAM.DisplayMember = "FACM_ASSET_CONF_MODEL";
                    cbRAM.ValueMember = "FACM_ASSET_CONF_MODEL";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdB = null;
                dt = null;
            }

        }
        private void FillMoniterConfigDetl()
        {

            objSQLdB = new SQLDB();
            DataTable dt = new DataTable();

            try
            {


                string strCommand = "select DISTINCT FACM_ASSET_CONF_MODEL from FIXED_ASSETS_SYS_CONFID_MAS " +
                                       "where FACM_ASSET_CONF_TYPE='LCD OR LED MONITOR' AND" +
                                       " FACM_ASSET_TYPE='" + strAssetType + "' ORDER BY FACM_ASSET_CONF_MODEL";

                dt = objSQLdB.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbMoniter.DataSource = dt;
                    cbMoniter.DisplayMember = "FACM_ASSET_CONF_MODEL";
                    cbMoniter.ValueMember = "FACM_ASSET_CONF_MODEL";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdB = null;
                dt = null;
            }

        }
        private void FillOperatingSysConfigDetl()
        {
            objSQLdB = new SQLDB();
            DataTable dt = new DataTable();

            try
            {


                string strCommand = "SELECT DISTINCT FACM_ASSET_CONF_MODEL FROM FIXED_ASSETS_SYS_CONFID_MAS " +
                                       "where FACM_ASSET_CONF_TYPE='OPERATING SYSTEM' AND" +
                                       " FACM_ASSET_TYPE='" + strAssetType + "' ORDER BY FACM_ASSET_CONF_MODEL";

                dt = objSQLdB.ExecuteDataSet(strCommand).Tables[0];


                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbOperatingSys.DataSource = dt;
                    cbOperatingSys.DisplayMember = "FACM_ASSET_CONF_MODEL";
                    cbOperatingSys.ValueMember = "FACM_ASSET_CONF_MODEL";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdB = null;
                dt = null;
            }

        }
        private void FillAntivirusSysConfigDetl()
        {
            objSQLdB = new SQLDB();
            DataTable dt = new DataTable();

            try
            {


                string strCommand = "SELECT DISTINCT FACM_ASSET_CONF_MODEL FROM FIXED_ASSETS_SYS_CONFID_MAS " +
                                       "where FACM_ASSET_CONF_TYPE='ANTIVIRUS' AND" +
                                       " FACM_ASSET_TYPE='" + strAssetType + "' ORDER BY FACM_ASSET_CONF_MODEL";

                dt = objSQLdB.ExecuteDataSet(strCommand).Tables[0];


                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbAntivirus.DataSource = dt;
                    cbAntivirus.DisplayMember = "FACM_ASSET_CONF_MODEL";
                    cbAntivirus.ValueMember = "FACM_ASSET_CONF_MODEL";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdB = null;
                dt = null;
            }

        }
        private void FillPrinterConfigDetl()
        {
            objFixedAsssetDB = new FixedAssetsDB();
            DataTable dt = new DataTable();
            try
            {
                if (cbToBranch.SelectedIndex > 0)
                {

                    dt = objFixedAsssetDB.GetPrinterConfigurationID(cbToBranch.SelectedValue.ToString()).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbPrinter.DataSource = dt;
                    cbPrinter.DisplayMember = "DisplayMember";
                    cbPrinter.ValueMember = "ValueMember";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objFixedAsssetDB = null;
                dt = null;
            }

        }
        private void FillScannerConfigDetl()
        {

            objFixedAsssetDB = new FixedAssetsDB();
            DataTable dt = new DataTable();
            try
            {
                if (cbToBranch.SelectedIndex > 0)
                {

                    dt = objFixedAsssetDB.GetScannerConfigurationID(cbToBranch.SelectedValue.ToString()).Tables[0];

                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbScanner.DataSource = dt;
                    cbScanner.DisplayMember = "DisplayMember";
                    cbScanner.ValueMember = "ValueMember";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objFixedAsssetDB = null;
                dt = null;
            }

        }
        private void FillBiometrcsConfigDetl()
        {

            objFixedAsssetDB = new FixedAssetsDB();
            DataTable dt = new DataTable();


            try
            {
                if (cbToBranch.SelectedIndex > 0)
                {

                    dt = objFixedAsssetDB.GetBiometricsConfigurationID(cbToBranch.SelectedValue.ToString()).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbBiometrcs.DataSource = dt;
                    cbBiometrcs.DisplayMember = "DisplayMember";
                    cbBiometrcs.ValueMember = "ValueMember";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objFixedAsssetDB = null;
                dt = null;
            }

        }
        private void FillWebcamConfigDetl()
        {
            objFixedAsssetDB = new FixedAssetsDB();
            DataTable dt = new DataTable();

            try
            {
                if (cbToBranch.SelectedIndex > 0)
                {

                    dt = objFixedAsssetDB.GetWebcamConfigurationID(cbToBranch.SelectedValue.ToString()).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbWebCam.DataSource = dt;
                    cbWebCam.DisplayMember = "DisplayMember";
                    cbWebCam.ValueMember = "ValueMember";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objFixedAsssetDB = null;
                dt = null;
            }

        }



        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtEcode_KeyUp(object sender, KeyEventArgs e)
        {
            objSQLdB = new SQLDB();
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
                    dt = objSQLdB.ExecuteDataSet(strCmd).Tables[0];

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
                    objSQLdB = null;
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
        private void FillCompanyData()
        {
            objSQLdB = new SQLDB();
            DataTable dt = new DataTable();

            try
            {
                string strCmd = "SELECT DISTINCT CM_COMPANY_NAME ,CM_COMPANY_CODE " +
                                   " FROM USER_BRANCH " +
                                   " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                   " INNER JOIN COMPANY_MAS ON CM_COMPANY_CODE = COMPANY_CODE " +
                                   " WHERE UB_USER_ID ='" + CommonData.LogUserId +
                                   "' ORDER BY CM_COMPANY_NAME";

                dt = objSQLdB.ExecuteDataSet(strCmd).Tables[0];

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
                objSQLdB = null;
                dt = null;
            }
        }


        private void FillBranchData()
        {
            objSQLdB = new SQLDB();
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
                    dt = objSQLdB.ExecuteDataSet(strCommand).Tables[0];
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
                objSQLdB = null;
                dt = null;
            }
        }

        private bool CheckData()
        {
            bool flag = true;
            objSQLdB = new SQLDB();
            DataTable dt = new DataTable();

            
            if (CommonData.LogUserId.ToUpper() != "ADMIN")
            {
                string strCommand = "";
                strCommand = " SELECT FAMR_ASSET_SL_NO, FAMR_ID FROM FIXED_ASSETS_MOVEMENT_REG" +
                                " INNER JOIN USER_BRANCH ON UB_BRANCH_CODE = FAMR_TO_BRANCH_CODE" +
                                " WHERE EXISTS (SELECT * FROM (SELECT FAMR_ASSET_SL_NO AsstSlNo, max(FAMR_GIVEN_DATE) mEffdate" +
                                " FROM FIXED_ASSETS_MOVEMENT_REG GROUP BY FAMR_ASSET_SL_NO) effd WHERE AsstSlNo = FAMR_ASSET_SL_NO" +
                                " AND mEffdate = FAMR_GIVEN_DATE) AND UB_USER_ID='" + CommonData.LogUserId + "' AND FAMR_ASSET_SL_NO='" + txtAssetSLNo.Text.ToString() + "'";

                dt = objSQLdB.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 1)
                {
                    flag = false;
                    MessageBox.Show("This Data Can Not Be Manipulated", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return flag;
                }
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
            objSQLdB = new SQLDB();
            string strCmd = "";
            int iRec = 0;

            if (CheckData() == true)
            {

                if (SaveAssetHeadDetails() > 0)
                {
                    if (SaveAssetMovementDetails() > 0)
                    {
                        SaveFixedAssetsConfigDetails();

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

                        strCmd += "DELETE FROM FIXED_ASSETS_CONFIG_DETL WHERE FACD_ASSET_ID='" + txtAssetSLNo.Text.ToString() + "'";

                        strCmd += "DELETE FROM FIXED_ASSETS_HEAD WHERE FAH_ASSET_SL_NO='" + txtAssetSLNo.Text.ToString() + "'";


                        iRec = objSQLdB.ExecuteSaveData(strCmd);

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
            objSQLdB = new SQLDB();
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
                    strCommand = "UPDATE FIXED_ASSETS_HEAD SET FAH_ASSET_TYPE='" + strAssetType +
                                                              "',FAH_ASSET_MAKE='" + strAssetType +
                                                              "',FAH_MODEL='" + strAssetType +
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
                                                             "('" + strAssetType +
                                                             "','" + strAssetType +
                                                             "','" + strAssetType +
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
                    iRes = objSQLdB.ExecuteSaveData(strCommand);
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
            objSQLdB = new SQLDB();
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
                    dt = objSQLdB.ExecuteDataSet(strCommand).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        TrnNo = Convert.ToInt32(dt.Rows[0]["TrnNo"].ToString());

                    }
                }

                objSQLdB = new SQLDB();
                strCommand = "";

                strCommand = "DELETE FROM FIXED_ASSETS_MOVEMENT_REG WHERE FAMR_ID='" + AssetMovementId + "' ";
                iRes = objSQLdB.ExecuteSaveData(strCommand);


                strCommand = "";
                objSQLdB = new SQLDB();

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
                    iRes = objSQLdB.ExecuteSaveData(strCommand);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iRes;


        }

        private int SaveFixedAssetsConfigDetails()
        {
            int ival = 0;
            objSQLdB = new SQLDB();
            DataTable dt = new DataTable();
            string strMotherBoard = "", strProcessor = "", strHDD = "", strDVDorCD = "", strRam = "", strMonitor = "", strPrinter = "",
                strScanner = "", strBiometrics = "", strWebcam = "", strOperatingSys = "", strAntivirus = "";
            string strCommand = "";

            try
            {

                if (cbMotherboard.SelectedIndex > 0)
                {
                    strMotherBoard = cbMotherboard.SelectedValue.ToString();
                }

                if (cbProcesor.SelectedIndex > 0)
                {
                    strProcessor = cbProcesor.SelectedValue.ToString();
                }
                if (cbHDD.SelectedIndex > 0)
                {
                    strHDD = cbHDD.SelectedValue.ToString();
                }

                if (cbDVDorCD.SelectedIndex > 0)
                {
                    strDVDorCD = cbDVDorCD.SelectedValue.ToString();
                }
                if (cbRAM.SelectedIndex > 0)
                {
                    strRam = cbRAM.SelectedValue.ToString();
                }
                if (cbMoniter.SelectedIndex > 0)
                {
                    strMonitor = cbMoniter.SelectedValue.ToString();
                }

                if (cbPrinter.SelectedIndex > 0)
                {
                    strPrinter = cbPrinter.SelectedValue.ToString();
                }

                if (cbScanner.SelectedIndex > 0)
                {
                    strScanner = cbScanner.SelectedValue.ToString();
                }

                if (cbBiometrcs.SelectedIndex > 0)
                {
                    strBiometrics = cbBiometrcs.SelectedValue.ToString();
                }

                if (cbWebCam.SelectedIndex > 0)
                {
                    strWebcam = cbWebCam.SelectedValue.ToString();
                }
                if (cbOperatingSys.SelectedIndex > 0)
                {
                    strOperatingSys = cbOperatingSys.SelectedValue.ToString();
                }
                if (cbAntivirus.SelectedIndex > 0)
                {
                    strAntivirus = cbAntivirus.SelectedValue.ToString();
                }


                if (flagUpdate == true)
                {
                    strCommand = "UPDATE FIXED_ASSETS_CONFIG_DETL SET FACD_RAM='" + strRam +
                                                                  "',FACD_HDD='" + strHDD +
                                                                  "',FACD_PROCESSOR='" + strProcessor +
                                                                  "',FACD_MOTHER_BOARD='" + strMotherBoard +
                                                                  "',FACD_MONITOR='" + strMonitor +
                                                                  "',FACD_CD_DVD='" + strDVDorCD +
                                                                  "',FACD_PRINTER_ID='" + strPrinter +
                                                                  "',FACD_SCANNER_ID='" + strScanner +
                                                                  "',FACD_BIOMETRICSS_ID='" + strBiometrics +
                                                                  "',FACD_WEB_CAM ='" + strWebcam +
                                                                  "',FACD_OPERATING_SYS ='" + strOperatingSys +
                                                                  "',FACD_ANTI_VIRUS ='" + strAntivirus +
                                                                  "',FACD_MODIFIED_BY='" + CommonData.LogUserId +
                                                                  "',FACD_MODOFIED_DATE= getdate()" +
                                                                  " WHERE FACD_ASSET_ID ='" + txtAssetSLNo.Text.ToString() + "'";


                }
                else if (flagUpdate == false)
                {

                    strCommand = "INSERT INTO FIXED_ASSETS_CONFIG_DETL(FACD_ASSET_ID " +
                                                                  ",FACD_RAM" +
                                                                  ",FACD_HDD" +
                                                                  ",FACD_PROCESSOR" +
                                                                  ",FACD_MOTHER_BOARD" +
                                                                  ",FACD_MONITOR" +
                                                                  ",FACD_CD_DVD" +
                                                                  ",FACD_PRINTER_ID" +
                                                                  ",FACD_SCANNER_ID" +
                                                                  ",FACD_BIOMETRICSS_ID" +
                                                                  ",FACD_WEB_CAM " +
                                                                  ",FACD_OPERATING_SYS " +
                                                                  ",FACD_ANTI_VIRUS " +
                                                                  ",FACD_CREATED_BY " +
                                                                  ",FACD_CREATED_DATE " +
                                                                  ")VALUES " +
                                                                  "('" + txtAssetSLNo.Text.ToString() +
                                                                  "','" + strRam +
                                                                  "','" + strHDD +
                                                                  "','" + strProcessor +
                                                                  "','" + strMotherBoard +
                                                                  "','" + strMonitor +
                                                                  "','" + strDVDorCD +
                                                                  "','" + strPrinter +
                                                                  "','" + strScanner +
                                                                  "','" + strBiometrics +
                                                                  "','" + strWebcam +
                                                                  "','" + strOperatingSys +
                                                                  "','" + strAntivirus +
                                                                  "','" + CommonData.LogUserId + "',getdate())";


                }

                if (strCommand.Length > 0)
                {

                    ival = objSQLdB.ExecuteSaveData(strCommand);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return ival;

        }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            flagUpdate = false;
            isDetlUpdate = false;
            txtAssetSLNo.Text = "";

            dtpGivenDate.Value = DateTime.Today;
            dtpGivenDate.Value = DateTime.Today;
            GenerateAssetSLNo();

            cbToComp.SelectedIndex = 0;
            cbTrnType.SelectedIndex = 0;
            cbToBranch.SelectedIndex = 0;
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
            cbMotherboard.SelectedIndex = 0;
            //cbMoniter.DataSource = null;
            cbScanner.SelectedIndex = -1; ;
            cbProcesor.SelectedIndex = 0;
            cbHDD.SelectedIndex = 0;
            cbOperatingSys.SelectedIndex = 0;
            cbAntivirus.SelectedIndex = 0;
            cbRAM.SelectedIndex = 0;
            cbHDD.SelectedIndex = 0;
            cbDVDorCD.SelectedIndex = 0;
            cbProcesor.SelectedIndex = 0;
            cbPrinter.SelectedIndex = -1;
            cbWebCam.SelectedIndex = -1;
            cbBiometrcs.SelectedIndex = -1;
            cbMoniter.SelectedIndex = 0;
            // cbScanner.DataSource = null;
            //cbProcesor.DataSource = null;
            //cbHDD.DataSource = null;
            //cbOperatingSys.DataSource = null;
            //cbAntivirus.DataSource = null;
            //cbRAM.DataSource = null;
            //cbHDD.DataSource = null;
            //cbDVDorCD.DataSource = null;
            //cbProcesor.DataSource = null;
            //cbPrinter.DataSource = null;
            //cbWebCam.DataSource = null;
            //cbBiometrcs.DataSource = null;
            txtContactNo.Text = "";


        }

        private void cbToComp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbToComp.SelectedIndex > 0)
            {
                FillBranchData();
            }
        }

        private void FillAssetsDetails()
        {
            objFixedAsssetDB = new FixedAssetsDB();
            DataTable dt = new DataTable();

            if (txtAssetSLNo.Text != "")
            {
                try
                {
                    dt = objFixedAsssetDB.GetFixedAssetsData(txtAssetSLNo.Text.ToString()).Tables[0];
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

                        cbMotherboard.SelectedIndex = 0;

                        cbScanner.SelectedIndex = -1; ;
                        cbProcesor.SelectedIndex = 0;
                        cbHDD.SelectedIndex = 0;
                        cbOperatingSys.SelectedIndex = 0;
                        cbAntivirus.SelectedIndex = 0;
                        cbRAM.SelectedIndex = 0;
                        cbHDD.SelectedIndex = 0;
                        cbDVDorCD.SelectedIndex = 0;
                        cbProcesor.SelectedIndex = 0;
                        cbPrinter.SelectedIndex = -1;
                        cbWebCam.SelectedIndex = -1;
                        cbBiometrcs.SelectedIndex = -1;
                        cbMoniter.SelectedIndex = 0;

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objFixedAsssetDB = null;
                    dt = null;
                }
            }

        }

        private void FillSystemConfigurationDetails()
        {
            objFixedAsssetDB = new FixedAssetsDB();
            DataTable dt = new DataTable();

            try
            {
                dt = objFixedAsssetDB.GetSystemConfigDtails(txtAssetSLNo.Text.ToString()).Tables[0];


                if (dt.Rows.Count > 0)
                {
                    isDetlUpdate = true;

                    cbRAM.SelectedValue = dt.Rows[0]["Ram"].ToString();
                    cbHDD.SelectedValue = dt.Rows[0]["HDD"].ToString();
                    cbProcesor.SelectedValue = dt.Rows[0]["Processor"].ToString();
                    cbMotherboard.SelectedValue = dt.Rows[0]["MotherBoard"].ToString();
                    cbMoniter.SelectedValue = dt.Rows[0]["Moniter"].ToString();
                    cbDVDorCD.SelectedValue = dt.Rows[0]["CDorDvd"].ToString();
                    cbPrinter.SelectedValue = dt.Rows[0]["PrinterId"].ToString();
                    cbScanner.SelectedValue = dt.Rows[0]["ScannerId"].ToString();
                    cbBiometrcs.SelectedValue = dt.Rows[0]["BioMetricss"].ToString();
                    cbWebCam.SelectedValue = dt.Rows[0]["WebCam"].ToString();
                    cbOperatingSys.SelectedValue = dt.Rows[0]["OperatindSys"].ToString();
                    cbAntivirus.SelectedValue = dt.Rows[0]["Antivirus"].ToString();

                }
                else
                {
                    isDetlUpdate = false;

                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdB = null;
                dt = null;
            }
        }


        private void txtAssetSLNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtAssetSLNo.Text != "")
            {
                FillAssetsDetails();
                FillSystemConfigurationDetails();
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

                cbMotherboard.SelectedIndex = 0;

                cbTrnType.SelectedIndex = 0;
                cbScanner.SelectedIndex = -1; ;
                cbProcesor.SelectedIndex = 0;
                cbHDD.SelectedIndex = 0;
                cbOperatingSys.SelectedIndex = 0;
                cbAntivirus.SelectedIndex = 0;
                cbRAM.SelectedIndex = 0;
                cbHDD.SelectedIndex = 0;
                cbDVDorCD.SelectedIndex = 0;
                cbProcesor.SelectedIndex = 0;
                cbPrinter.SelectedIndex = -1;
                cbWebCam.SelectedIndex = -1;
                cbBiometrcs.SelectedIndex = -1;
                cbMoniter.SelectedIndex = 0;
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

        private void cbToBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbToBranch.SelectedIndex > 0)
            {
                FillWebcamConfigDetl();
                FillPrinterConfigDetl();
                FillScannerConfigDetl();
                FillBiometrcsConfigDetl();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSQLdB = new SQLDB();
            int iRec = 0;
            DataTable dt = new DataTable();
            string strCmd = "";

            if (txtAssetSLNo.Text != "" && flagUpdate == true)
            {
                string strCommand = "SELECT FAMR_TRN_NO FROM FIXED_ASSETS_MOVEMENT_REG " +
                              " WHERE FAMR_ASSET_SL_NO='" + txtAssetSLNo.Text.ToString() + "'";
                dt = objSQLdB.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count == 1)
                {

                    DialogResult dlgResult = MessageBox.Show("Do you want Delete this Record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        try
                        {
                            //flagUpdate = true;
                            strCmd += " DELETE FROM FIXED_ASSETS_MOVEMENT_REG WHERE FAMR_ASSET_SL_NO='" + txtAssetSLNo.Text.ToString() + "'";
                            strCmd += " DELETE FROM FIXED_ASSETS_CONFIG_DETL WHERE FACD_ASSET_ID='" + txtAssetSLNo.Text.ToString() + "'";
                            strCmd += " DELETE FROM FIXED_ASSETS_HEAD WHERE FAH_ASSET_SL_NO='" + txtAssetSLNo.Text.ToString() + "'";
                            iRec = objSQLdB.ExecuteSaveData(strCmd);
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

        private void txtAssetTagNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }

        private void txtEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
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

        private void txtContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

    }
}
