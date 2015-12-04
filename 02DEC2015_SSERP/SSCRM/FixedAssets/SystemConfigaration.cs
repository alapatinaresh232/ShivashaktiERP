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
    public partial class SystemConfigaration : Form
    {
        SQLDB objDb = null;
        FixedAssetsDB objFixedAsssetDB = null;

        bool flagUpdate = false;

        public SystemConfigaration()
        {
            InitializeComponent();
        }

        private void SystemConfigaration_Load(object sender, EventArgs e)
        {
            FillCompanyData();
           

        }
        private void FillCompanyData()
        {
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT CM_COMPANY_CODE,CM_COMPANY_NAME FROM COMPANY_MAS WHERE ACTIVE='T' ORDER BY CM_COMPANY_NAME";
                dt = objDb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";
                    row[1] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cbCompany.DataSource = dt;
                    cbCompany.DisplayMember = "CM_COMPANY_NAME";
                    cbCompany.ValueMember = "CM_COMPANY_CODE";
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objDb = null;
                dt = null;
            }
        }

        private void FillBranchData()
        {

            objDb = new SQLDB();
            DataTable dt = new DataTable();

            try
            {
                string strCommand = "SELECT BRANCH_NAME ,BRANCH_CODE  FROM BRANCH_MAS " +
                                       " WHERE COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                            "' AND ACTIVE='T' Order by BRANCH_NAME ";
                dt = objDb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbBranch.DataSource = dt;
                    cbBranch.DisplayMember = "BRANCH_NAME";
                    cbBranch.ValueMember = "BRANCH_CODE";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objDb = null;
                dt = null;
            }

        }



        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                FillBranchData();
            }
            else
            {
                cbBranch.DataSource = null;
                cbSystemID.DataSource = null;
                cbMotherboard.DataSource = null;
                cbMoniter.DataSource = null;
                cbScanner.DataSource = null;
                cbProcesor.DataSource = null;
                cbHDD.DataSource = null;
                cbOperatingSys.DataSource = null;
                cbAntivirus.DataSource = null;
                cbRAM.DataSource = null;
                cbHDD.DataSource = null;
                cbDVDorCD.DataSource = null;
                cbProcesor.DataSource = null;
                cbPrinter.DataSource = null;
                cbWebCam.DataSource = null;
                cbBiometrcs.DataSource = null;

            }
           
        }

        private void cbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                if (cbBranch.SelectedIndex > 0)
                {

                    FillSystemID();
                }
                else
                {
                    cbSystemID.DataSource = null;
                    cbMotherboard.DataSource = null;
                    cbMoniter.DataSource = null;
                    cbScanner.DataSource = null;
                    cbProcesor.DataSource = null;
                    cbHDD.DataSource = null;
                    cbOperatingSys.DataSource = null;
                    cbAntivirus.DataSource = null;
                    cbRAM.DataSource = null;
                    cbHDD.DataSource = null;
                    cbDVDorCD.DataSource = null;
                    cbProcesor.DataSource = null;
                    cbPrinter.DataSource = null;
                    cbWebCam.DataSource = null;
                    cbBiometrcs.DataSource = null;
                    txtEmpName.Text = "";
                    txtMobileNo.Text = "";
                    txtAssetTagNo.Text = "";


                }
            }
        }
        private void FillSystemID()
       {
            objFixedAsssetDB = new FixedAssetsDB();
            DataTable dt = new DataTable();
            cbSystemID.DataSource = null;
            

            try
            {
                if (cbBranch.SelectedIndex > 0)
                {
                    dt = objFixedAsssetDB.GetAssetSlNos(cbBranch.SelectedValue.ToString(), "").Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = "--Select--";

                        dt.Rows.InsertAt(dr, 0);
                        cbSystemID.DataSource = dt;
                        cbSystemID.DisplayMember = "DisplayMember";
                        cbSystemID.ValueMember = "ValueMember";

                    }
                    else
                    {
                        cbSystemID.SelectedIndex = -1;
                        cbMoniter.SelectedIndex = -1;
                        cbMotherboard.SelectedIndex = -1;
                        cbPrinter.SelectedIndex = -1;
                        cbProcesor.SelectedIndex = -1;
                        cbRAM.SelectedIndex = -1;
                        cbScanner.SelectedIndex = -1;
                        cbHDD.SelectedIndex = -1;
                        cbDVDorCD.SelectedIndex = -1;
                        cbWebCam.SelectedIndex = -1;
                        cbBiometrcs.SelectedIndex = -1;
                        cbOperatingSys.SelectedIndex = -1;
                        cbAntivirus.SelectedIndex = -1;
                        txtEmpName.Text = "";
                        txtMobileNo.Text = "";
                        txtAssetTagNo.Text = "";
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objDb = null;
                dt = null;
            }
        }
        private void FillEmployeeDetails()
        {
            objFixedAsssetDB = new FixedAssetsDB();
            DataTable dt = new DataTable();
             try
            {
                if (cbSystemID.SelectedIndex>0)
                {
                    dt = objFixedAsssetDB.GetAssetSlNos("", cbSystemID.SelectedValue.ToString()).Tables[0];


                    if (dt.Rows.Count > 0)
                    {
                        txtEmpName.Text = dt.Rows[0]["EmpName"].ToString();
                        txtMobileNo.Text = dt.Rows[0]["ContactNo"].ToString();
                        txtAssetTagNo.Text = dt.Rows[0]["TagNo"].ToString();
                    }
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
      

        private void cbSystemID_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbSystemID.SelectedIndex > 0)
            {

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
                FillEmployeeDetails();

                FillSystemConfigurationDetails();
            }
            else
            {

                cbMotherboard.DataSource = null;
                cbMoniter.DataSource = null;
                cbScanner.DataSource = null;
                cbProcesor.DataSource = null;
                cbHDD.DataSource = null;
                cbOperatingSys.DataSource = null;
                cbAntivirus.DataSource = null;
                cbRAM.DataSource = null;
                cbHDD.DataSource = null;
                cbDVDorCD.DataSource = null;
                cbProcesor.DataSource = null;
                cbPrinter.DataSource = null;
                cbWebCam.DataSource = null;
                cbBiometrcs.DataSource = null;
                txtEmpName.Text = "";
                txtMobileNo.Text = "";
                txtAssetTagNo.Text = "";



              
            }
            
        }
        private void FillMotherBoardConfigDetl()
        {
            objDb = new SQLDB();
            DataTable dt = new DataTable();
           
            try
            {
                if (cbSystemID.SelectedIndex > 0)
                {

                    string[] strArr = cbSystemID.Text.ToString().Split('-');
                    string strCommand = "select FACM_ASSET_CONF_MODEL from FIXED_ASSETS_SYS_CONFID_MAS " +
                                           "where FACM_ASSET_CONF_TYPE='MOTHER BOARD' AND" +
                                           " FACM_ASSET_TYPE='" + strArr[0] + "' ORDER BY FACM_ASSET_CONF_MODEL";

                    dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                }


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
                objDb = null;
                dt = null;
            }

        }
        private void FillProcesorConfigDetl()
        {
            objDb = new SQLDB();
            DataTable dt = new DataTable();
          
            try
            {
                if (cbSystemID.SelectedIndex > 0)
                {
                    string[] strArr = cbSystemID.Text.ToString().Split('-');
                    string strCommand = "select FACM_ASSET_CONF_MODEL from FIXED_ASSETS_SYS_CONFID_MAS " +
                                           "where FACM_ASSET_CONF_TYPE='PROCESSOR' AND" +
                                           " FACM_ASSET_TYPE='" + strArr[0] + "' ORDER BY FACM_ASSET_CONF_MODEL";

                    dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                }

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
                objDb = null;
                dt = null;
            }

        }
        private void FillHDDConfigDetl()
        {
            objDb = new SQLDB();
            DataTable dt = new DataTable();
           
            try
            {
                if (cbSystemID.SelectedIndex > 0)
                {
                    string[] strArr = cbSystemID.Text.ToString().Split('-');
                    string strCommand = "select FACM_ASSET_CONF_MODEL from FIXED_ASSETS_SYS_CONFID_MAS " +
                                           "where FACM_ASSET_CONF_TYPE='HARD DISC' AND" +
                                           " FACM_ASSET_TYPE='" + strArr[0] + "' ORDER BY FACM_ASSET_CONF_MODEL";

                    dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                }

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
                objDb = null;
                dt = null;
            }

        }
        private void FillDVDorCDConfigDetl()
        {
            objDb = new SQLDB();
            DataTable dt = new DataTable();
           
            try
            {
                if (cbSystemID.SelectedIndex > 0)
                {
                    string[] strArr = cbSystemID.Text.ToString().Split('-');
                    string strCommand = "select FACM_ASSET_CONF_MODEL from FIXED_ASSETS_SYS_CONFID_MAS " +
                                           "where FACM_ASSET_CONF_TYPE='DVD OR CD-ROM' AND" +
                                           " FACM_ASSET_TYPE='" + strArr[0] + "' ORDER BY FACM_ASSET_CONF_MODEL";

                    dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                }

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
                objDb = null;
                dt = null;
            }

        }
        private void FillRAMConfigDetl()
        {

            objDb = new SQLDB();
            DataTable dt = new DataTable();
           
            try
            {
                if (cbSystemID.SelectedIndex > 0)
                {
                    string[] strArr = cbSystemID.Text.ToString().Split('-');
                    string strCommand = "select FACM_ASSET_CONF_MODEL from FIXED_ASSETS_SYS_CONFID_MAS " +
                                           "where FACM_ASSET_CONF_TYPE='RAM' AND" +
                                           " FACM_ASSET_TYPE='" + strArr[0] + "' ORDER BY FACM_ASSET_CONF_MODEL";

                    dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                }

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
                objDb = null;
                dt = null;
            }

        }
        private void FillMoniterConfigDetl()
        {

            objDb = new SQLDB();
            DataTable dt = new DataTable();
            
            try
            {
                if (cbSystemID.SelectedIndex > 0)
                {
                    string[] strArr = cbSystemID.Text.ToString().Split('-');
                    string strCommand = "select FACM_ASSET_CONF_MODEL from FIXED_ASSETS_SYS_CONFID_MAS " +
                                           "where FACM_ASSET_CONF_TYPE='LCD OR LED MONITOR' AND" +
                                           " FACM_ASSET_TYPE='" + strArr[0] + "' ORDER BY FACM_ASSET_CONF_MODEL";

                    dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                }

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
                objDb = null;
                dt = null;
            }

        }
        private void FillOperatingSysConfigDetl()
        {
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            
            try
            {
                if (cbSystemID.SelectedIndex > 0)
                {
                    string[] strArr = cbSystemID.Text.ToString().Split('-');
                    string strCommand = "SELECT FACM_ASSET_CONF_MODEL FROM FIXED_ASSETS_SYS_CONFID_MAS " +
                                           "where FACM_ASSET_CONF_TYPE='OPERATING SYSTEM' AND" +
                                           " FACM_ASSET_TYPE='" + strArr[0] + "' ORDER BY FACM_ASSET_CONF_MODEL";

                    dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                }

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
                objDb = null;
                dt = null;
            }

        }
        private void FillAntivirusSysConfigDetl()
        {
            objDb = new SQLDB();
            DataTable dt = new DataTable();
           
            try
            {
                if (cbSystemID.SelectedIndex > 0)
                {
                    string[] strArr = cbSystemID.Text.ToString().Split('-');
                    string strCommand = "SELECT FACM_ASSET_CONF_MODEL FROM FIXED_ASSETS_SYS_CONFID_MAS " +
                                           "where FACM_ASSET_CONF_TYPE='ANTIVIRUS' AND" +
                                           " FACM_ASSET_TYPE='" + strArr[0] + "' ORDER BY FACM_ASSET_CONF_MODEL";

                    dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                }

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
                objDb = null;
                dt = null;
            }

        }
        private void FillPrinterConfigDetl()
        {

            objFixedAsssetDB = new FixedAssetsDB();
            DataTable dt = new DataTable();
           

            try
            {
                if (cbSystemID.SelectedIndex > 0)
                {

                    dt = objFixedAsssetDB.GetPrinterConfigurationID(cbBranch.SelectedValue.ToString()).Tables[0];
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
                objDb = null;
                dt = null;
            }

        }
        private void FillScannerConfigDetl()
        {

            objFixedAsssetDB = new FixedAssetsDB();
            DataTable dt = new DataTable();  
            try
            {  
                if (cbSystemID.SelectedIndex > 0)
                {
                    
                        dt = objFixedAsssetDB.GetScannerConfigurationID(cbBranch.SelectedValue.ToString()).Tables[0];
                    
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
                objDb = null;
                dt = null;
            }

        }
        private void FillBiometrcsConfigDetl()
        {

            objDb = new SQLDB();
            DataTable dt = new DataTable();
            

            try
            {
                if (cbSystemID.SelectedIndex > 0)
                {

                    dt = objFixedAsssetDB.GetBiometricsConfigurationID(cbBranch.SelectedValue.ToString()).Tables[0];
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
                objDb = null;
                dt = null;
            }

        }
        private void FillWebcamConfigDetl()
        {

            objDb = new SQLDB();
            DataTable dt = new DataTable();
         
            try
            {
                if (cbSystemID.SelectedIndex > 0)
                {

                    dt = objFixedAsssetDB.GetWebcamConfigurationID(cbBranch.SelectedValue.ToString()).Tables[0];
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
                objDb = null;
                dt = null;
            }

        }

        private bool CheckData()
        {
            bool flag = true;
            if (cbCompany.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Company", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbCompany.Focus();
                return flag;

            }

            if (cbBranch.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbBranch.Focus();
                return flag;

            }
            if (cbSystemID.Items.Count != 0)
            {
                if (cbSystemID.SelectedIndex == 0)
                {

                    flag = false;
                    MessageBox.Show("Please Select System Id", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cbSystemID.Focus();
                }
            }
            else
            {
                flag = false;
                MessageBox.Show("Systems Are Not Issued To This Branch\n '"+ cbBranch.Text.ToString() +"'","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return flag;
            }

            return flag;
        }
        private void FillSystemConfigurationDetails()
        {

            objFixedAsssetDB = new FixedAssetsDB();
            DataTable dt = new DataTable();
           
            try
            {
                dt = objFixedAsssetDB.GetSystemConfigDtails(cbSystemID.SelectedValue.ToString()).Tables[0];


                if (dt.Rows.Count > 0)
                {
                    flagUpdate = true;

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
                    flagUpdate = false;
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objDb = null;
                dt = null;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objDb = new SQLDB();
            if (CheckData() == true)
            {

                if (SaveFixedAssetsConfigDetails() > 0)
                {
                    MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //btnCancel_Click(null, null);
                    cbSystemID.SelectedIndex = 0;
                    cbMotherboard.SelectedIndex = -1;
                    cbHDD.SelectedIndex = -1;
                    cbRAM.SelectedIndex = -1;
                    cbProcesor.SelectedIndex = -1;
                    cbDVDorCD.SelectedIndex = -1;
                    cbMoniter.SelectedIndex = -1;
                    cbScanner.SelectedIndex = -1;
                    cbPrinter.SelectedIndex = -1;
                    cbBiometrcs.SelectedIndex = -1;
                    cbWebCam.SelectedIndex = -1;
                    cbOperatingSys.SelectedIndex = -1;
                    cbAntivirus.SelectedIndex = -1;
                    txtEmpName.Text = "";
                    txtMobileNo.Text = "";
                    txtAssetTagNo.Text = "";
                    flagUpdate = false;
                }

                else
                {
                    MessageBox.Show("Data Not Saved ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private int SaveFixedAssetsConfigDetails()
        {
            int ival = 0;
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            string strMotherBoard = "", strProcessor = "", strHDD = "", strDVDorCD = "", strRam = "", strMonitor = "", strPrinter = "", 
                strScanner = "", strBiometrics = "", strWebcam = "",strOperatingSys="",strAntivirus="";
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
                    strAntivirus= cbAntivirus.SelectedValue.ToString();
                }


                if (flagUpdate == true)
                {
                    strCommand = "UPDATE FIXED_ASSETS_CONFIG_DETL SET FACD_RAM='" + strRam +
                                                                  "', FACD_HDD='" + strHDD +
                                                                  "', FACD_PROCESSOR='" + strProcessor +
                                                                  "',FACD_MOTHER_BOARD='" + strMotherBoard +
                                                                  "', FACD_MONITOR='" + strMonitor +
                                                                  "', FACD_CD_DVD='" + strDVDorCD +
                                                                  "', FACD_PRINTER_ID='" + strPrinter +
                                                                  "',FACD_SCANNER_ID='" + strScanner +
                                                                  "',FACD_BIOMETRICSS_ID='" + strBiometrics +
                                                                  "', FACD_WEB_CAM ='" + strWebcam +
                                                                  "', FACD_OPERATING_SYS ='" + strOperatingSys+
                                                                   "', FACD_ANTI_VIRUS ='" + strAntivirus+
                                                                   "',FACD_MODIFIED_BY='" + CommonData.LogUserId +
                                                                  "',FACD_MODOFIED_DATE= getdate()" +
                                                                  " WHERE FACD_ASSET_ID ='" + cbSystemID.SelectedValue.ToString() + "'";


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
                                                              "('" + cbSystemID.SelectedValue.ToString() +
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

                    ival = objDb.ExecuteSaveData(strCommand);
                }
                if (ival > 0)
                {
                    return ival;
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return ival;

        }




        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbCompany.SelectedIndex = 0;
            cbBranch.SelectedIndex = -1;
            cbSystemID.SelectedIndex = -1;
            cbMotherboard.SelectedIndex = -1;
            cbHDD.SelectedIndex = -1;
            cbRAM.SelectedIndex = -1;
            cbProcesor.SelectedIndex = -1;
            cbDVDorCD.SelectedIndex = -1;
            cbMoniter.SelectedIndex = -1;
            cbScanner.SelectedIndex = -1;
            cbPrinter.SelectedIndex = -1;
            cbBiometrcs.SelectedIndex = -1;
            cbWebCam.SelectedIndex = -1;
            cbOperatingSys.SelectedIndex = -1;
            cbAntivirus.SelectedIndex = -1;
            txtEmpName.Text = "";
            txtMobileNo.Text = "";
            txtAssetTagNo.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            objDb = new SQLDB();
            string strCommand = "";
            int ival = 0;

            if (cbSystemID.SelectedIndex > 0)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want Delete this Record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {

                    try
                    {
                        strCommand = "DELETE FROM FIXED_ASSETS_CONFIG_DETL WHERE FACD_ASSET_ID='" + cbSystemID.SelectedValue.ToString() + "'";
                        ival = objDb.ExecuteSaveData(strCommand);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }


                    if (ival > 0)
                    {
                        MessageBox.Show("Data deleted successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //btnCancel_Click(null, null);
                        FillSystemID();
                        cbMotherboard.SelectedIndex = -1;
                        cbHDD.SelectedIndex = -1;
                        cbRAM.SelectedIndex = -1;
                        cbProcesor.SelectedIndex = -1;
                        cbDVDorCD.SelectedIndex = -1;
                        cbMoniter.SelectedIndex = -1;
                        cbScanner.SelectedIndex = -1;
                        cbPrinter.SelectedIndex = -1;
                        cbBiometrcs.SelectedIndex = -1;
                        cbWebCam.SelectedIndex = -1;
                        cbOperatingSys.SelectedIndex = -1;
                        cbAntivirus.SelectedIndex = -1;
                        txtEmpName.Text = "";
                        txtMobileNo.Text = "";
                        txtAssetTagNo.Text = "";
                    }

                    else
                    {
                        MessageBox.Show("Data Not Deleted ", "Fixed Asset Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
            else
            {
                MessageBox.Show("Please Select SystemId ", "System Configuration Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            
           
        }

      


    }
}


    
