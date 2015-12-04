using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using SSAdmin;
using SSTrans;
using SSCRMDB;
using SDMS.App_Code;
namespace SDMS
{
    public partial class DealarApplicationForm : Form
    {
        
        private SQLDB objData = null;
        private InvoiceDB objInvoiceData = null;
        private DealerInfo objDealerInfo = null;
        private HRInfo objHrInfo = null;
        
        public bool updateFlag = false;

        //public Partners objPartners;
        public DataTable dtPartners = new DataTable();
        public DataTable dtPrevTurnOver = new DataTable();
        public DataTable dtBussinessDetails = new DataTable();
        public DataTable dtOtherDealerShips = new DataTable();
        public DataTable dtBussnessVehicles = new DataTable();
        public DataTable dtTerritoryDetl = new DataTable();
        public DataTable dtFixedAssets = new DataTable();
        public DataTable dtSecurityCheqs = new DataTable();
        public DataTable dtBankerDetails = new DataTable();
        public string strStateCode = string.Empty;
        
        public DealarApplicationForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }
        private void pg3Close_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void DealarApplicationForm_Load(object sender, EventArgs e)
        {
            txtDealerCode.Focus();
            grpPage1.Visible = true;
            grpPage2.Visible = false;
            grpPage3.Visible = false;
            
            GenerateApplicationNo();
            cbRelation.SelectedIndex = 0;
            //cbPest.SelectedIndex = 0;
            cbFirmType.SelectedIndex = 0;
            cbBussType.SelectedIndex = 0;
            cbMORF.SelectedIndex = 0;
            cbFinArrangType.SelectedIndex = 0;
            FillCommanyData();



            DateTime dtimeDefault = Convert.ToDateTime("01-01-1900");
            dtAppDate.Value = dtimeDefault;
            dtpDealerDoj.Value = dtimeDefault;
            dtpDealerDob.Value = dtimeDefault;
            dpPestLicDate.Value = dtimeDefault;
            dpPestLicValid.Value = dtimeDefault;
            dpFertLicDate.Value = dtimeDefault;
            dpFertLicValid.Value = dtimeDefault;



            #region "CREATE PARTNER TABLE"
            dtPartners.Columns.Add("SLNO");
            dtPartners.Columns.Add("PartnerName");
            dtPartners.Columns.Add("Age");
            dtPartners.Columns.Add("Hno");
            dtPartners.Columns.Add("LandMark");
            dtPartners.Columns.Add("Vill");
            dtPartners.Columns.Add("Mandal");
            dtPartners.Columns.Add("District");
            dtPartners.Columns.Add("State");
            dtPartners.Columns.Add("Pin");
            dtPartners.Columns.Add("Phone");
            #endregion

            #region "CREATE PREVTURNOVER TABLE"
            dtPrevTurnOver.Columns.Add("SLNO");
            dtPrevTurnOver.Columns.Add("Yearpt");
            dtPrevTurnOver.Columns.Add("Product");
            dtPrevTurnOver.Columns.Add("TurnOver");
            #endregion

            #region "CREATE BUSSINESSDETAILS TABLE"
            dtBussinessDetails.Columns.Add("SLNO");
            dtBussinessDetails.Columns.Add("CompanyName");
            dtBussinessDetails.Columns.Add("YearBD");
            dtBussinessDetails.Columns.Add("Product");
            dtBussinessDetails.Columns.Add("Last2YearTurnOver");
            dtBussinessDetails.Columns.Add("TurnOver");
            dtBussinessDetails.Columns.Add("TotalTurnOver");
            #endregion


            #region "CREATE DEALERSHIPDETAILS TABLE"
            dtOtherDealerShips.Columns.Add("SLNO");
            dtOtherDealerShips.Columns.Add("CompanyName");
            dtOtherDealerShips.Columns.Add("FromYear");
            dtOtherDealerShips.Columns.Add("Remarks");
            #endregion


            #region "CREATE BUSSINESSVEHICLES TABLE"
            dtBussnessVehicles.Columns.Add("SLNO");
            dtBussnessVehicles.Columns.Add("VehType");
            dtBussnessVehicles.Columns.Add("NoOfVeh");
            #endregion

            #region "CREATE TERRITORYDETAILS TABLE"
            dtTerritoryDetl.Columns.Add("SLNO");
            dtTerritoryDetl.Columns.Add("TerritoryType");
            dtTerritoryDetl.Columns.Add("TerritoryName");
            #endregion

            #region "CREATE FIXEDASSETS TABLE"
            dtFixedAssets.Columns.Add("SLNO");
            dtFixedAssets.Columns.Add("Details1");
            dtFixedAssets.Columns.Add("Details2");
            dtFixedAssets.Columns.Add("Details3");
            #endregion

            #region "CREATE SECURITYCHEQS TABLE"
            dtSecurityCheqs.Columns.Add("SLNO");
            dtSecurityCheqs.Columns.Add("CheqNo");
            dtSecurityCheqs.Columns.Add("BankName");
            dtSecurityCheqs.Columns.Add("BranchName");
            #endregion

            #region "CREATE BANKERDETAILS TABLE"
            dtBankerDetails.Columns.Add("SLNO");
            dtBankerDetails.Columns.Add("BankerName");
            dtBankerDetails.Columns.Add("Hno");
            dtBankerDetails.Columns.Add("LandMark");
            dtBankerDetails.Columns.Add("Vill");
            dtBankerDetails.Columns.Add("Mandal");
            dtBankerDetails.Columns.Add("District");
            dtBankerDetails.Columns.Add("State");
            dtBankerDetails.Columns.Add("Pin");
            dtBankerDetails.Columns.Add("Phone");
            #endregion

        }
        private void GenerateApplicationNo()
        {
            objData = new SQLDB();
            DataTable dt = new DataTable();
            int iMax = 0;
            try
            {
                String strCmd = "SELECT ISNULL(MAX(DAMH_APPL_NUMBER),0)+1 TRNNO FROM DL_APPL_MASTER_HEAD ";
                iMax = Convert.ToInt32(objData.ExecuteDataSet(strCmd).Tables[0].Rows[0][0].ToString()); ;
                if (iMax > 0)
                {
                    txtApplNo.Text = iMax.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objData = null;
            }
        }
        private void FillCommanyData()
        {
            objData = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT CM_COMPANY_NAME,CM_COMPANY_CODE FROM COMPANY_MAS WHERE CM_COMPANY_CODE in ('SHS','SATL')";

                dt = objData.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
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
                objData = null;
                dt = null;
            }
        }
        private void cmCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLocationData();
        }
        private void FillLocationData()
        {
            objData = new SQLDB();
            DataTable dt = new DataTable();
            cbBranch.DataSource = null;
            try
            {
                if (cbCompany.SelectedIndex > 0)
                {
                    string strCommand = "SELECT BRANCH_CODE as branchCode,BRANCH_NAME FROM BRANCH_MAS WHERE COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + "'";
                    dt = objData.ExecuteDataSet(strCommand).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbBranch.DataSource = dt;
                    cbBranch.DisplayMember = "BRANCH_NAME";
                    cbBranch.ValueMember = "branchCode";
                    //cbLocation.ValueMember = "LOCATION";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objData = null;
                dt = null;
            }
        }
        private void btnFirmVSearch_Click(object sender, EventArgs e)
        {
            VillageSearch VSearch = new VillageSearch("Firm");
            VSearch.objDealarApplicationForm = this;
            VSearch.ShowDialog();
            //AddVillage objAddVillage = new AddVillage("Firm");
            //objAddVillage.objDealarApplicationForm = this;
            //objAddVillage.ShowDialog();
        }

        private void btnDealerVSearch_Click(object sender, EventArgs e)
        {
            VillageSearch VSearch = new VillageSearch("Dealer");
            VSearch.objDealarApplicationForm = this;
            VSearch.ShowDialog();
        }
        private void btnDelPresSearch_Click(object sender, EventArgs e)
        {
            VillageSearch VSearch = new VillageSearch("DealerOperation");
            VSearch.objDealarApplicationForm = this;
            VSearch.ShowDialog();
        }
        private void txtFirmVillageSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtFirmVillageSearch.Text.Length == 0)
                {
                    cbFirmVillage_optional.DataSource = null;
                    cbFirmVillage_optional.DataBindings.Clear();
                    cbFirmVillage_optional.Items.Clear();
                    //if (btnSave.Enabled == true)
                        ClearVillageDetails("Firm");
                }
                else if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                {
                    if (FindInputAddressSearch("Firm") == false)
                    {
                        FillAddressData(txtFirmVillageSearch.Text,"Firm");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void txtDealerVillSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtDealerVillSearch.Text.Length == 0)
                {
                    cbDealerVill_optional.DataSource = null;
                    cbDealerVill_optional.DataBindings.Clear();
                    cbDealerVill_optional.Items.Clear();
                    //if (btnSave.Enabled == true)
                        ClearVillageDetails("Dealer");
                }
                else if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                {
                    if (FindInputAddressSearch("Dealer") == false)
                    {
                        FillAddressData(txtDealerVillSearch.Text, "Dealer");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void txtDelOperPresVillSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtDelOperPresVillSearch.Text.Length == 0)
                {
                    cbDealerOperVill_optional.DataSource = null;
                    cbDealerOperVill_optional.DataBindings.Clear();
                    cbDealerOperVill_optional.Items.Clear();
                    //if (btnSave.Enabled == true)
                    ClearVillageDetails("DealerOperation");
                }
                else if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                {
                    if (FindInputAddressSearch("DealerOperation") == false)
                    {
                        FillAddressData(txtDelOperPresVillSearch.Text, "DealerOperation");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void ClearVillageDetails(string sAddressType)
        {
            if (sAddressType == "Firm")
            {
                txtFirmVillage.Text = "";
                txtFirmMandal.Text = "";
                txtFirmDistrict.Text = "";
                txtFirmState.Text = "";
                txtFirmPin.Text = "";
            }
            else if (sAddressType == "Dealer")
            {
                txtDealerVill.Text = "";
                txtDealerMandal.Text = "";
                txtDealerDistricit.Text = "";
                txtDealerState.Text = "";
                txtDealerPin.Text = "";
            }
            else 
            {
                txtDelOperTown.Text = "";
                txtDelOperMandal.Text = "";
                txtDelOperDist.Text = "";
                txtDelOperState.Text = "";
            }
        }
        private bool FindInputAddressSearch(string sAddressType)
        {
            bool blFind = false;
            try
            {
                if(sAddressType=="Firm")
                for (int i = 0; i < this.cbFirmVillage_optional.Items.Count; i++)
                {
                    string strItem = ((System.Data.DataRowView)(this.cbFirmVillage_optional.Items[i])).Row.ItemArray[1] + "".ToString().Trim();
                    if (strItem.IndexOf(txtFirmVillageSearch.Text) > -1)
                    {
                        blFind = true;
                        cbFirmVillage_optional.SelectedIndex = i;
                        txtFirmVillage.Text = ((System.Data.DataRowView)(this.cbFirmVillage_optional.Items[i])).Row.ItemArray[1] + "";
                        txtFirmMandal.Text = ((System.Data.DataRowView)(this.cbFirmVillage_optional.Items[i])).Row.ItemArray[2] + "";
                        txtFirmDistrict.Text = ((System.Data.DataRowView)(this.cbFirmVillage_optional.Items[i])).Row.ItemArray[3] + "";
                        txtFirmState.Text = ((System.Data.DataRowView)(this.cbFirmVillage_optional.Items[i])).Row.ItemArray[4] + "";
                        txtFirmPin.Text = ((System.Data.DataRowView)(this.cbFirmVillage_optional.Items[i])).Row.ItemArray[5] + "";
                        strStateCode = ((System.Data.DataRowView)(this.cbFirmVillage_optional.Items[i])).Row.ItemArray[0] + "";
                        break;
                    }
                    break;
                }
                else if (sAddressType == "Dealer")
                    for (int i = 0; i < this.cbDealerVill_optional.Items.Count; i++)
                    {
                        string strItem = ((System.Data.DataRowView)(this.cbDealerVill_optional.Items[i])).Row.ItemArray[1] + "".ToString().Trim();
                        if (strItem.IndexOf(txtDealerVillSearch.Text) > -1)
                        {
                            blFind = true;
                            cbDealerVill_optional.SelectedIndex = i;
                            txtDealerVill.Text = ((System.Data.DataRowView)(this.cbFirmVillage_optional.Items[i])).Row.ItemArray[1] + "";
                            txtDealerMandal.Text = ((System.Data.DataRowView)(this.cbFirmVillage_optional.Items[i])).Row.ItemArray[2] + "";
                            txtDealerDistricit.Text = ((System.Data.DataRowView)(this.cbFirmVillage_optional.Items[i])).Row.ItemArray[3] + "";
                            txtDealerState.Text = ((System.Data.DataRowView)(this.cbFirmVillage_optional.Items[i])).Row.ItemArray[4] + "";
                            txtDealerPin.Text = ((System.Data.DataRowView)(this.cbFirmVillage_optional.Items[i])).Row.ItemArray[5] + "";
                            strStateCode = ((System.Data.DataRowView)(this.cbFirmVillage_optional.Items[i])).Row.ItemArray[0] + "";
                            break;
                        }
                        break;
                    }
                else 
                    for (int i = 0; i < this.cbDealerOperVill_optional.Items.Count; i++)
                    {
                        string strItem = ((System.Data.DataRowView)(this.cbDealerOperVill_optional.Items[i])).Row.ItemArray[1] + "".ToString().Trim();
                        if (strItem.IndexOf(txtDelOperPresVillSearch.Text) > -1)
                        {
                            blFind = true;
                            cbDealerOperVill_optional.SelectedIndex = i;
                            txtDelOperTown.Text = ((System.Data.DataRowView)(this.cbDealerOperVill_optional.Items[i])).Row.ItemArray[1] + "";
                            txtDelOperMandal.Text = ((System.Data.DataRowView)(this.cbDealerOperVill_optional.Items[i])).Row.ItemArray[2] + "";
                            txtDelOperDist.Text = ((System.Data.DataRowView)(this.cbDealerOperVill_optional.Items[i])).Row.ItemArray[3] + "";
                            txtDelOperState.Text = ((System.Data.DataRowView)(this.cbDealerOperVill_optional.Items[i])).Row.ItemArray[4] + "";

                            strStateCode = ((System.Data.DataRowView)(this.cbDealerOperVill_optional.Items[i])).Row.ItemArray[0] + "";
                            break;
                        }
                        break;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
            }
            return blFind;
        }
        private void FillAddressData(string sSearch,string sAddressType)
        {
            Hashtable htParam = null;
            objInvoiceData = new InvoiceDB();
            string strDist = string.Empty;
            DataSet dsVillage = null;
            DataTable dtVillage = new DataTable();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (sSearch.Trim().Length >= 0)
                    htParam = new Hashtable();
                htParam.Add("sVillage", sSearch);
                htParam.Add("sDistrict", strDist);



                htParam.Add("sCDState", CommonData.StateCode);
                dsVillage = new DataSet();
                dsVillage = objInvoiceData.GetVillageDataSet(htParam);
                dtVillage = dsVillage.Tables[0];
                if (dtVillage.Rows.Count == 1)
                {
                    if (sAddressType == "Firm")
                    {
                        txtFirmVillage.Text = dtVillage.Rows[0]["PANCHAYAT"].ToString();  // ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[1] + "";
                        txtFirmMandal.Text = dtVillage.Rows[0]["Mandal"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[2]+ ""; 
                        txtFirmDistrict.Text = dtVillage.Rows[0]["District"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[3] + "";
                        txtFirmState.Text = dtVillage.Rows[0]["State"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[4] + "";
                        txtFirmPin.Text = dtVillage.Rows[0]["PIN"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[5] + "";
                        strStateCode = dtVillage.Rows[0]["CDState"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[0] + "";
                    }
                    else if (sAddressType == "Dealer")
                    {
                        txtDealerVill.Text = dtVillage.Rows[0]["PANCHAYAT"].ToString();  
                        txtDealerMandal.Text = dtVillage.Rows[0]["Mandal"].ToString();  
                        txtDealerDistricit.Text = dtVillage.Rows[0]["District"].ToString();
                        txtDealerState.Text = dtVillage.Rows[0]["State"].ToString();  
                        txtDealerPin.Text = dtVillage.Rows[0]["PIN"].ToString();  
                        strStateCode = dtVillage.Rows[0]["CDState"].ToString(); 
                    }
                    else
                    {
                        txtDelOperTown.Text = dtVillage.Rows[0]["PANCHAYAT"].ToString();
                        txtDelOperMandal.Text = dtVillage.Rows[0]["Mandal"].ToString();
                        txtDelOperDist.Text = dtVillage.Rows[0]["District"].ToString();
                        txtDelOperState.Text = dtVillage.Rows[0]["State"].ToString();
                        strStateCode = dtVillage.Rows[0]["CDState"].ToString();
                    }

                }
                else if (dtVillage.Rows.Count > 1)
                {
                    if (sAddressType == "Firm")
                    {
                        txtFirmVillage.Text = "";
                        txtFirmMandal.Text = "";
                        txtFirmDistrict.Text = "";
                        txtFirmState.Text = "";
                        txtFirmPin.Text = "";
                        strStateCode = "";
                        FillAddressComboBox(dtVillage,sAddressType);
                    }
                   else if (sAddressType == "Dealer")
                    {
                        txtDealerVill.Text = "";
                        txtDealerMandal.Text = "";
                        txtDealerDistricit.Text = "";
                        txtDealerState.Text = "";
                        txtDealerPin.Text = "";
                        strStateCode = "";
                        FillAddressComboBox(dtVillage, sAddressType);
                    }
                    else
                    {
                        txtDelOperTown.Text = "";
                        txtDelOperMandal.Text = "";
                        txtDelOperDist.Text = "";
                        txtDelOperState.Text = "";
                        strStateCode = "";
                        FillAddressComboBox(dtVillage, sAddressType);
                    }
                }

                else
                {
                        htParam = new Hashtable();
                        htParam.Add("sVillage", "%" + sSearch);
                        htParam.Add("sDistrict", strDist);
                        dsVillage = new DataSet();
                        dsVillage = objInvoiceData.GetVillageDataSet(htParam);
                        dtVillage = dsVillage.Tables[0];
                        FillAddressComboBox(dtVillage,sAddressType);
                        ClearVillageDetails(sAddressType);
                }
                Cursor.Current = Cursors.Default;
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
        private void FillAddressComboBox(DataTable dt,string sAddressType)
        {
            DataTable dataTable = new DataTable("Village");

            dataTable.Columns.Add("StateID", typeof(String));
            dataTable.Columns.Add("Panchayath", typeof(String));
            dataTable.Columns.Add("Mandal", typeof(String));
            dataTable.Columns.Add("District", typeof(String));
            dataTable.Columns.Add("State", typeof(String));
            dataTable.Columns.Add("Pin", typeof(String));
            

            for (int i = 0; i < dt.Rows.Count; i++)
                dataTable.Rows.Add(new String[] { dt.Rows[i]["CDState"] + 
                     "", dt.Rows[i]["PANCHAYAT"] + 
                     "", dt.Rows[i]["MANDAL"] + 
                     "", dt.Rows[i]["DISTRICT"] + 
                     "", dt.Rows[i]["STATE"] + "", dt.Rows[i]["PIN"] + ""});

            if (sAddressType == "Firm")
            {
                cbFirmVillage_optional.DataBindings.Clear();
                cbFirmVillage_optional.DataSource = dataTable;
                cbFirmVillage_optional.DisplayMember = "Panchayath";
                cbFirmVillage_optional.ValueMember = "StateID";
            }
            else if (sAddressType == "Dealer")
            {
                cbDealerVill_optional.DataBindings.Clear();
                cbDealerVill_optional.DataSource = dataTable;
                cbDealerVill_optional.DisplayMember = "Panchayath";
                cbDealerVill_optional.ValueMember = "StateID";
            }
            else 
            {
                cbDealerOperVill_optional.DataBindings.Clear();
                cbDealerOperVill_optional.DataSource = dataTable;
                cbDealerOperVill_optional.DisplayMember = "Panchayath";
                cbDealerOperVill_optional.ValueMember = "StateID";
            }
        }

        private void cbFirmVillage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFirmVillage_optional.SelectedIndex > -1)
            {
                if (this.cbFirmVillage_optional.Items[cbFirmVillage_optional.SelectedIndex].ToString() != "")
                {
                    txtFirmVillage.Text = ((System.Data.DataRowView)(this.cbFirmVillage_optional.Items[cbFirmVillage_optional.SelectedIndex])).Row.ItemArray[1] + "";
                    txtFirmMandal.Text = ((System.Data.DataRowView)(this.cbFirmVillage_optional.Items[cbFirmVillage_optional.SelectedIndex])).Row.ItemArray[2] + "";
                    txtFirmDistrict.Text = ((System.Data.DataRowView)(this.cbFirmVillage_optional.Items[cbFirmVillage_optional.SelectedIndex])).Row.ItemArray[3] + "";
                    txtFirmState.Text = ((System.Data.DataRowView)(this.cbFirmVillage_optional.Items[cbFirmVillage_optional.SelectedIndex])).Row.ItemArray[4] + "";
                    txtFirmPin.Text = ((System.Data.DataRowView)(this.cbFirmVillage_optional.Items[cbFirmVillage_optional.SelectedIndex])).Row.ItemArray[5] + "";
                    strStateCode = ((System.Data.DataRowView)(this.cbFirmVillage_optional.Items[cbFirmVillage_optional.SelectedIndex])).Row.ItemArray[0] + "";
                }
            }
        }

        private void cbDealerVill_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDealerVill_optional.SelectedIndex > -1)
            {
                if (this.cbDealerVill_optional.Items[cbDealerVill_optional.SelectedIndex].ToString() != "")
                {
                    txtDealerVill.Text = ((System.Data.DataRowView)(this.cbDealerVill_optional.Items[cbDealerVill_optional.SelectedIndex])).Row.ItemArray[1] + "";
                    txtDealerMandal.Text = ((System.Data.DataRowView)(this.cbDealerVill_optional.Items[cbDealerVill_optional.SelectedIndex])).Row.ItemArray[2] + "";
                    txtDealerDistricit.Text = ((System.Data.DataRowView)(this.cbDealerVill_optional.Items[cbDealerVill_optional.SelectedIndex])).Row.ItemArray[3] + "";
                    txtDealerState.Text = ((System.Data.DataRowView)(this.cbDealerVill_optional.Items[cbDealerVill_optional.SelectedIndex])).Row.ItemArray[4] + "";
                    txtDealerPin.Text = ((System.Data.DataRowView)(this.cbDealerVill_optional.Items[cbDealerVill_optional.SelectedIndex])).Row.ItemArray[5] + "";
                    strStateCode = ((System.Data.DataRowView)(this.cbDealerVill_optional.Items[cbDealerVill_optional.SelectedIndex])).Row.ItemArray[0] + "";
                }
            }

        }
        private void cbDealerOperVill_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDealerOperVill_optional.SelectedIndex > -1)
            {
                if (this.cbDealerOperVill_optional.Items[cbDealerOperVill_optional.SelectedIndex].ToString() != "")
                {
                    txtDelOperTown.Text = ((System.Data.DataRowView)(this.cbDealerOperVill_optional.Items[cbDealerOperVill_optional.SelectedIndex])).Row.ItemArray[1] + "";
                    txtDelOperMandal.Text = ((System.Data.DataRowView)(this.cbDealerOperVill_optional.Items[cbDealerOperVill_optional.SelectedIndex])).Row.ItemArray[2] + "";
                    txtDelOperDist.Text = ((System.Data.DataRowView)(this.cbDealerOperVill_optional.Items[cbDealerOperVill_optional.SelectedIndex])).Row.ItemArray[3] + "";
                    txtDelOperState.Text = ((System.Data.DataRowView)(this.cbDealerOperVill_optional.Items[cbDealerOperVill_optional.SelectedIndex])).Row.ItemArray[4] + "";
                    strStateCode = ((System.Data.DataRowView)(this.cbDealerOperVill_optional.Items[cbDealerOperVill_optional.SelectedIndex])).Row.ItemArray[0] + "";
                }
            }
        }
        private void txtFirmEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToLower(e.KeyChar);
        }
        private void txtDealerEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToLower(e.KeyChar);
        }
        private void txtEcodeApprovBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }
        private void txtEcodeCreditAprroved_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }
        private void txtFirmMob_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }
        private void txtDealerMob_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }
        private void txtRepresntCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }
        private void txtNoofDealers_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }
        private void txtNoofEmp_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }
        private void txtGodownSpace_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }
        private void txtExpetTurnOverProd_KeyDown(object sender, KeyEventArgs e)
        {
            //restrctingToDigits(e);
        }

        private void txtExpetCapitalProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtExpetCapitalProd_optional.TextLength >= 0 && (e.KeyChar == (char)Keys.OemPeriod ))
            {
                //tests 
            }
            else
            {
                if (!char.IsControl(e.KeyChar)
                    && !char.IsDigit(e.KeyChar)
                    && e.KeyChar != '.' && e.KeyChar != ',')
                {
                    e.Handled = true;
                }
                // only allow one decimal point
                if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                {
                    e.Handled = true;
                }

               
            }
        }

        private void txtPrvTurnOverWhSale_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtPrvTurnOverWhSale_optional.TextLength >= 0 && (e.KeyChar == (char)Keys.OemPeriod))
            {
                //tests 
            }
            else
            {
                if (!char.IsControl(e.KeyChar)
                    && !char.IsDigit(e.KeyChar)
                    && e.KeyChar != '.' && e.KeyChar != ',')
                {
                    e.Handled = true;
                }
                // only allow one decimal point
                if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                {
                    e.Handled = true;
                }

               
            }
        }

        private void txtPrvTurnOverRetl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtPrvTurnOverRetl_optional.TextLength >= 0 && (e.KeyChar == (char)Keys.OemPeriod))
            {
                //tests 
            }
            else
            {
                if (!char.IsControl(e.KeyChar)
                    && !char.IsDigit(e.KeyChar)
                    && e.KeyChar != '.' && e.KeyChar != ',')
                {
                    e.Handled = true;
                }
                // only allow one decimal point
                if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                {
                    e.Handled = true;
                }

                
            }
        }


        private void restrctingToDigits(KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtFirmPhNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }

        private void txtFirmOffPh_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }

        private void txtFirmResPh_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }

        private void txtFirmFax_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }

        private void txtDealerPh_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }

        private void txtDealerOffPh_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }

        private void txtDealerResPh_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }

        private void txtDealerFax_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }
        private void txtDealerCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(CheckData(grpPage3))
            {
                SaveData();
            }
        }



        #region "GRIDVIEW DETAILS"
        public void GetPartnerDetails()
        {
            int intRow = 1;
            gvPartners.Rows.Clear();
            for (int i = 0; i < dtPartners.Rows.Count; i++)
            {
                string addres = "";
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                dtPartners.Rows[i]["SLNO"] = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellName = new DataGridViewTextBoxCell();
                cellName.Value = dtPartners.Rows[i]["PartnerName"];
                tempRow.Cells.Add(cellName);

                DataGridViewCell cellAge = new DataGridViewTextBoxCell();
                cellAge.Value = dtPartners.Rows[i]["Age"];
                tempRow.Cells.Add(cellAge);

               

               

                DataGridViewCell cellHNo = new DataGridViewTextBoxCell();
                cellHNo.Value = dtPartners.Rows[i]["Hno"];
                if(cellHNo.Value.ToString().Length>0)
                {
                    addres += cellHNo.Value;
                }
                    tempRow.Cells.Add(cellHNo);
                DataGridViewCell cellLandMark = new DataGridViewTextBoxCell();
                cellLandMark.Value = dtPartners.Rows[i]["LandMark"];
                if (cellLandMark.Value.ToString().Length > 0)
                {
                    addres += ","+cellLandMark.Value ;
                }
                tempRow.Cells.Add(cellLandMark);
                DataGridViewCell cellVill = new DataGridViewTextBoxCell();
                cellVill.Value = dtPartners.Rows[i]["Vill"];
                if (cellVill.Value.ToString().Length > 0)
                {
                    addres += "," + cellVill.Value;
                }
                tempRow.Cells.Add(cellVill);
                DataGridViewCell cellMandal= new DataGridViewTextBoxCell();
                cellMandal.Value = dtPartners.Rows[i]["Mandal"];
                if (cellMandal.Value.ToString().Length > 0)
                {
                    addres += "," + cellMandal.Value;
                }
                tempRow.Cells.Add(cellMandal);
                DataGridViewCell cellDist= new DataGridViewTextBoxCell();
                cellDist.Value = dtPartners.Rows[i]["District"];
                if (cellDist.Value.ToString().Length > 0)
                {
                    addres += "," + cellDist.Value;
                }
                tempRow.Cells.Add(cellDist);
                DataGridViewCell cellState= new DataGridViewTextBoxCell();
                cellState.Value = dtPartners.Rows[i]["State"];
                if (cellState.Value.ToString().Length > 0)
                {
                    addres += "," + cellState.Value;
                }
                tempRow.Cells.Add(cellState);
                DataGridViewCell cellPIn= new DataGridViewTextBoxCell();
                cellPIn.Value = dtPartners.Rows[i]["Pin"];
                if (cellPIn.Value.ToString().Length > 0)
                {
                    addres += "," + cellPIn.Value;
                }
                tempRow.Cells.Add(cellPIn);
                DataGridViewCell cellAddress = new DataGridViewTextBoxCell();
                cellAddress.Value = addres;
                //cellAddress.Value = dtPartners.Rows[i]["Hno"] + "," + dtPartners.Rows[i]["LandMark"] + "," + dtPartners.Rows[i]["Vill"] + "," + dtPartners.Rows[i]["Mandal"] + "," + dtPartners.Rows[i]["District"] + "," + dtPartners.Rows[i]["State"] + "," + dtPartners.Rows[i]["Pin"];
                tempRow.Cells.Add(cellAddress);
                DataGridViewCell cellPhone = new DataGridViewTextBoxCell();
                cellPhone.Value = dtPartners.Rows[i]["Phone"];
                tempRow.Cells.Add(cellPhone);


                intRow = intRow + 1;
                gvPartners.Rows.Add(tempRow);
            }
        }
        public void GetBankerDetails()
        {
            int intRow = 1;
            
            optionalgvBankerDetails.Rows.Clear();
            for (int i = 0; i < dtBankerDetails.Rows.Count; i++)
            {
                string addres = "";
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                dtBankerDetails.Rows[i]["SLNO"] = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellName = new DataGridViewTextBoxCell();
                cellName.Value = dtBankerDetails.Rows[i]["BankerName"];
                tempRow.Cells.Add(cellName);


                DataGridViewCell cellHNo = new DataGridViewTextBoxCell();
                cellHNo.Value = dtBankerDetails.Rows[i]["Hno"];
                if (cellHNo.Value.ToString().Length > 0)
                {
                    addres += cellHNo.Value;
                }
                tempRow.Cells.Add(cellHNo);
                DataGridViewCell cellLandMark = new DataGridViewTextBoxCell();
                cellLandMark.Value = dtBankerDetails.Rows[i]["LandMark"];
                if (cellLandMark.Value.ToString().Length > 0)
                {
                    addres += "," + cellLandMark.Value;
                }
                tempRow.Cells.Add(cellLandMark);
                DataGridViewCell cellVill = new DataGridViewTextBoxCell();
                cellVill.Value = dtBankerDetails.Rows[i]["Vill"];
                if (cellVill.Value.ToString().Length > 0)
                {
                    addres += "," + cellVill.Value;
                }
                tempRow.Cells.Add(cellVill);
                DataGridViewCell cellMandal = new DataGridViewTextBoxCell();
                cellMandal.Value = dtBankerDetails.Rows[i]["Mandal"];
                if (cellMandal.Value.ToString().Length > 0)
                {
                    addres += "," + cellMandal.Value;
                }
                tempRow.Cells.Add(cellMandal);
                DataGridViewCell cellDist = new DataGridViewTextBoxCell();
                cellDist.Value = dtBankerDetails.Rows[i]["District"];
                if (cellDist.Value.ToString().Length > 0)
                {
                    addres += "," + cellDist.Value;
                }
                tempRow.Cells.Add(cellDist);
                DataGridViewCell cellState = new DataGridViewTextBoxCell();
                cellState.Value = dtBankerDetails.Rows[i]["State"];
                if (cellState.Value.ToString().Length > 0)
                {
                    addres += "," + cellState.Value;
                }
                tempRow.Cells.Add(cellState);
                DataGridViewCell cellPIn = new DataGridViewTextBoxCell();
                cellPIn.Value = dtBankerDetails.Rows[i]["Pin"];
                if (cellPIn.Value.ToString().Length > 0)
                {
                    addres += "," + cellPIn.Value;
                }
                tempRow.Cells.Add(cellPIn);

                DataGridViewCell cellPhone = new DataGridViewTextBoxCell();
                cellPhone.Value = dtBankerDetails.Rows[i]["Phone"];
                //tempRow.Cells.Add(cellPhone);
                DataGridViewCell cellAddress = new DataGridViewTextBoxCell();
                cellAddress.Value = addres;
                //cellAddress.Value = dtPartners.Rows[i]["Hno"] + "," + dtPartners.Rows[i]["LandMark"] + "," + dtPartners.Rows[i]["Vill"] + "," + dtPartners.Rows[i]["Mandal"] + "," + dtPartners.Rows[i]["District"] + "," + dtPartners.Rows[i]["State"] + "," + dtPartners.Rows[i]["Pin"];
                tempRow.Cells.Add(cellAddress);

                

                intRow = intRow + 1;
                optionalgvBankerDetails.Rows.Add(tempRow);
            }
        }
        public void GetPrevTurnOverDetails()
        {
            int intRow = 1;
            gvPreviousTurnOvers.Rows.Clear();
            for (int i = 0; i < dtPrevTurnOver.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                dtPrevTurnOver.Rows[i]["SLNO"] = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellYear = new DataGridViewTextBoxCell();
                cellYear.Value = dtPrevTurnOver.Rows[i]["Yearpt"];
                tempRow.Cells.Add(cellYear);

                DataGridViewCell cellProd = new DataGridViewTextBoxCell();
                cellProd.Value = dtPrevTurnOver.Rows[i]["Product"];
                tempRow.Cells.Add(cellProd);

                DataGridViewCell cellTurnOver = new DataGridViewTextBoxCell();
                cellTurnOver.Value = dtPrevTurnOver.Rows[i]["TurnOver"].ToString();
                tempRow.Cells.Add(cellTurnOver);

                intRow = intRow + 1;
                gvPreviousTurnOvers.Rows.Add(tempRow);
            }
        }
        public void GetBussinessDetails()
        {
            int intRow = 1;
            gvBd.Rows.Clear();
            for (int i = 0; i < dtBussinessDetails.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                dtBussinessDetails.Rows[i]["SLNO"] = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellCompanyName = new DataGridViewTextBoxCell();
                cellCompanyName.Value = dtBussinessDetails.Rows[i]["CompanyName"];
                tempRow.Cells.Add(cellCompanyName);

                DataGridViewCell cellYear = new DataGridViewTextBoxCell();
                cellYear.Value = dtBussinessDetails.Rows[i]["YearBD"];
                tempRow.Cells.Add(cellYear);

                DataGridViewCell cellProd = new DataGridViewTextBoxCell();
                cellProd.Value = dtBussinessDetails.Rows[i]["Product"];
                tempRow.Cells.Add(cellProd);

                DataGridViewCell cellLastTurnOver = new DataGridViewTextBoxCell();
                cellLastTurnOver.Value = dtBussinessDetails.Rows[i]["Last2YearTurnOver"].ToString();
                tempRow.Cells.Add(cellLastTurnOver);

                DataGridViewCell cellTurnOver = new DataGridViewTextBoxCell();
                cellTurnOver.Value = dtBussinessDetails.Rows[i]["TurnOver"].ToString();
                tempRow.Cells.Add(cellTurnOver);

                DataGridViewCell cellTotalTurnOver = new DataGridViewTextBoxCell();
                cellTotalTurnOver.Value = dtBussinessDetails.Rows[i]["TotalTurnOver"].ToString();
                tempRow.Cells.Add(cellTotalTurnOver);

                intRow = intRow + 1;
                gvBd.Rows.Add(tempRow);
            }
        }
        public void GetOtherDealerShips()
        {
            int intRow = 1;
            gvDealerships.Rows.Clear();
            for (int i = 0; i < dtOtherDealerShips.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                dtOtherDealerShips.Rows[i]["SLNO"] = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellCompanyName = new DataGridViewTextBoxCell();
                cellCompanyName.Value = dtOtherDealerShips.Rows[i]["CompanyName"];
                tempRow.Cells.Add(cellCompanyName);

                DataGridViewCell cellFromYear = new DataGridViewTextBoxCell();
                cellFromYear.Value = dtOtherDealerShips.Rows[i]["FromYear"];
                tempRow.Cells.Add(cellFromYear);

                DataGridViewCell cellRemarks = new DataGridViewTextBoxCell();
                cellRemarks.Value = dtOtherDealerShips.Rows[i]["Remarks"];
                tempRow.Cells.Add(cellRemarks);

               
                intRow = intRow + 1;
                gvDealerships.Rows.Add(tempRow);
            }
        }
        public void GetBusinessVehicles()
        {
            int intRow = 1;
            gvBussinessVehl.Rows.Clear();
            for (int i = 0; i < dtBussnessVehicles.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                dtBussnessVehicles.Rows[i]["SLNO"] = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellVehType = new DataGridViewTextBoxCell();
                cellVehType.Value = dtBussnessVehicles.Rows[i]["VehType"];
                tempRow.Cells.Add(cellVehType);

                DataGridViewCell cellNoOfVeh = new DataGridViewTextBoxCell();
                cellNoOfVeh.Value = dtBussnessVehicles.Rows[i]["NoOfVeh"];
                tempRow.Cells.Add(cellNoOfVeh);

                intRow = intRow + 1;
                gvBussinessVehl.Rows.Add(tempRow);
            }
        }
        public void GetTerritoryDetl()
        {
            int intRow = 1;
            optionalgvTerritoryDetl.Rows.Clear();
            for (int i = 0; i < dtTerritoryDetl.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                dtTerritoryDetl.Rows[i]["SLNO"] = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellTerritoryTyep = new DataGridViewTextBoxCell();
                cellTerritoryTyep.Value = dtTerritoryDetl.Rows[i]["TerritoryType"];
                tempRow.Cells.Add(cellTerritoryTyep);

                DataGridViewCell cellTerritoryName = new DataGridViewTextBoxCell();
                cellTerritoryName.Value = dtTerritoryDetl.Rows[i]["TerritoryName"];
                tempRow.Cells.Add(cellTerritoryName);

                intRow = intRow + 1;
                optionalgvTerritoryDetl.Rows.Add(tempRow);
            }
        }
        public void GetFixedAssets()
        {
            int intRow = 1;
            gvFixedAsset.Rows.Clear();
            for (int i = 0; i < dtFixedAssets.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                dtFixedAssets.Rows[i]["SLNO"] = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellDetails1 = new DataGridViewTextBoxCell();
                cellDetails1.Value = dtFixedAssets.Rows[i]["Details1"];
                tempRow.Cells.Add(cellDetails1);

                DataGridViewCell cellDetails2 = new DataGridViewTextBoxCell();
                cellDetails2.Value = dtFixedAssets.Rows[i]["Details2"];
                tempRow.Cells.Add(cellDetails2);

                DataGridViewCell cellDetails3 = new DataGridViewTextBoxCell();
                cellDetails3.Value = dtFixedAssets.Rows[i]["Details3"];
                tempRow.Cells.Add(cellDetails3);

                intRow = intRow + 1;
                gvFixedAsset.Rows.Add(tempRow);
            }

        }
        #endregion
        
        private bool CheckData(GroupBox gp)
        {
            bool flag = true;
            UtilityLibrary oUtility = new UtilityLibrary();
            if (!SDMS.App_Code.UtilityLibrary.CustomValidate(gp,toolTip1))
            {
                return flag = false;
            }
            return flag;
        }



        private void SaveData()
        {
            
            foreach (Control oCntl in gpFirmAddress.Controls)
            {
                if (oCntl.Name.ToLower().IndexOf("_optional", 0) >= 0)
                {
                    if (oCntl.Text.Length == 0)
                    {
                        oCntl.Text = "";
                    }
                }
            }
            foreach (Control oCntl in gpDealerAddress.Controls)
            {
                if (oCntl.Name.ToLower().IndexOf("_optional", 0) >= 0)
                {
                    if (oCntl.Text.Length == 0)
                    {
                        oCntl.Text = "";
                    }
                }
            }
            foreach (Control oCntl in gpEnclosures.Controls)
            {
                CheckBox oCheck=null;
                if (oCntl.Name.IndexOf("chk", 0) >= 0)
                {
                    oCheck = (CheckBox)oCntl;
                    if (oCheck.Checked)
                        oCheck.Tag = "YES";
                    else
                        oCheck.Tag = "NO";
                }
                if (oCntl.Name.IndexOf("chkPar", 0) >= 0)
                {
                    if (oCheck.Checked == true)
                    {
                        oCntl.Tag = cbARTCL.SelectedItem.ToString();
                    }
                    else
                    {
                        oCntl.Tag = "NO";
                    }
                    
                }
            }

            if (chkSecCheq.Checked && dtSecurityCheqs.Rows.Count == 0)
            {
                MessageBox.Show("Enter SecurityCheque Details", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnAddSecurityCheqs.Focus();
                return;
            }
            if (chkPar.Checked && cbARTCL.SelectedIndex==0)
            {
                MessageBox.Show("Enter  Details", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbARTCL.Focus();
                return;
            }
            if (chkAnyOthers.Checked && txtAnyOthers.Text.Length == 0)
            {
                MessageBox.Show("Enter  Details", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAnyOthers.Focus();
                return;
            }

            if (txtRepresntCode_optional.Text.Length == 0)
            {
                txtRepresntCode_optional.Text = 0 + "";
            }
            if (txtNoofDealers_optional.Text.Length == 0)
            {
                txtNoofDealers_optional.Text = 0 + "";
            }
            if (txtGodownSpace_optional.Text.Length == 0)
            {
                txtGodownSpace_optional.Text = 0 + "";
            }
            if (txtNoofEmp_optional.Text.Length == 0)
            {
                txtNoofEmp_optional.Text = 0 + "";
            }
            if (txtExpetTurnOverProd_optional.Text.Length == 0)
            {
                txtExpetTurnOverProd_optional.Text = 0 + "";
            }
            if (txtExpetCapitalProd_optional.Text.Length == 0)
            {
                txtExpetCapitalProd_optional.Text = 0 + "";
            }
            if (txtPrvTurnOverWhSale_optional.Text.Length == 0)
            {
                txtPrvTurnOverWhSale_optional.Text = 0 + "";
            }
            if (txtPrvTurnOverRetl_optional.Text.Length == 0)
            {
                txtPrvTurnOverRetl_optional.Text = 0 + "";
            }
            if (txtFirmPin.Text.Length < 6)
            {
                txtFirmPin.Text = 0 + "";
            }
            if (txtDealerPin.Text.Length < 6)
            {
                txtDealerPin.Text = 0 + "";
            }
            //if (txtEcodeApprovBy.Text.Length == 0)
            //{
            //    txtEcodeApprovBy.Text = 0 + "";
            //}
            //if (txtCredLimAmount.Text.Length == 0)
            //{
            //    txtCredLimAmount.Text = 0 + "";
            //}
            //if (txtEcodeCreditAprroved.Text.Length == 0)
            //{
            //    txtEcodeCreditAprroved.Text = 0 + "";
            //}

            if (txtNameApprovBy.Text.Length == 0)
            {
                MessageBox.Show(" Enter Valid Code", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEcodeRecruitedBy.Focus();
            }
           else if (txtRecomndedName.Text.Length == 0)
            {
                MessageBox.Show(" Enter Valid Code", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDcodeRecommendedBy.Focus();
            }
            else if (txtCreditApprovedName.Text.Length == 0)
            {
                MessageBox.Show(" Enter Valid Code", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEcodeCreditAprroved.Focus();
            }

            else
            {
                byte[] imageData = { 0 };
                if (lblPath.Text != "")
                    imageData = ReadFile(lblPath.Text);

                if (updateFlag == false)
                {
                    GenerateApplicationNo();
                }

                var MainHeadrow = new[] {cbCompany.SelectedValue.ToString(),cbBranch.SelectedValue.ToString(),txtApplNo.Text.Trim(),dtAppDate.Value.ToString("dd/MMM/yyyy"), txtFirmName.Text.ToUpper().Trim(),txtFirmHouseNo.Text,
            txtFirmLandMark_optional.Text.ToUpper().Trim(),txtFirmVillage.Text.ToUpper().Trim(),txtFirmMandal.Text.ToUpper().Trim(),txtFirmDistrict.Text.ToUpper(),txtFirmState.Text.ToUpper(),
            txtFirmPin.Text,_optionaltxtFirmPhNo.Text,_optionaltxtFirmOffPh.Text,_optionaltxtFirmResPh.Text,_optionaltxtFirmFax.Text,txtFirmMob.Text,txtFirmEmail_optional.Text,txtDealerName.Text.ToUpper(),cbMORF.SelectedItem.ToString(),dtpDealerDob.Value.ToString("dd/MMM/yyyy"),cbRelation.SelectedItem.ToString(),txtDealerFatherName.Text.ToUpper(),
            txtDealerHNo.Text,txtDealerLandMark_optional.Text.ToUpper(),txtDealerVill.Text.ToUpper(),txtDealerMandal.Text.ToUpper(),txtDealerDistricit.Text.ToUpper(),txtDealerState.Text.ToUpper(),
            txtDealerPin.Text,_optionaltxtDealerPh.Text,_optionaltxtDealerOffPh.Text,_optionaltxtDealerResPh.Text,_optionaltxtDealerFax.Text,txtDealerMob.Text,txtDealerEmail_optional.Text,dtpDealerDoj.Value.ToString("dd/MMM/yyyy"),cbFirmType.SelectedItem.ToString(),txtVATNo_optional.Text,txtCSTNo_optional.Text,txtPANNo_optional.Text,txtPestLicNo_optional.Text,dpPestLicDate.Value.ToString("dd/MMM/yyyy"),dpPestLicValid.Value.ToString("dd/MMM/yyyy"),
            txtPestLIcIssuedBy_optional.Text, txtFertLicNo_optional.Text,dpFertLicDate.Value.ToString("dd/MMM/yyyy"),dpFertLicValid.Value.ToString("dd/MMM/yyyy"),txtFertLIcIssuedBy_optional.Text,txtPresentBus_optional.Text.ToUpper(),cbBussType.SelectedItem.ToString(),txtPrvTurnOverWhSale_optional.Text,txtPrvTurnOverRetl_optional.Text,
            txtNoofDealers_optional.Text,txtDelOperTown.Text.ToUpper(),txtDelOperMandal.Text.ToUpper(),txtDelOperDist.Text.ToUpper(),txtDelOperState.Text.ToUpper(),txtGodownSpace_optional.Text,txtNoofEmp_optional.Text,txtExpetTurnOverProd_optional.Text,txtExpetCapitalProd_optional.Text,cbFinArrangType.SelectedItem.ToString(),txtRepresntCode_optional.Text,
            chkSecCheq.Tag,chkLetterHeads.Tag,chkPar.Tag,chkVATRegCopy.Tag,chkPestLicCopy.Tag,chkPANCardCopy.Tag,chkIndemBond.Tag,txtEcodeRecruitedBy.Text,txtCredLimAmount.Text,txtEcodeCreditAprroved.Text, CommonData.LogUserId,System.DateTime.Now.ToString("dd/MMM/yyyy"),txtDealerCode.Text,chkFertCopy.Tag,txtAnyOthers.Text,txtDcodeRecommendedBy.Text,txtRemarks.Text};


                objDealerInfo = new DealerInfo();

                string sResult = objDealerInfo.DealerHeadSave(updateFlag, MainHeadrow, dtPartners, dtPrevTurnOver, dtBussinessDetails, dtOtherDealerShips, dtBussnessVehicles, dtTerritoryDetl, dtFixedAssets, dtSecurityCheqs, dtBankerDetails);
                if (sResult.Contains("saved"))
                {

                    updateFlag = false;

                    #region "This is used for Image Update"
                    if (imageData.Length > 1)
                    {
                        objDealerInfo = new DealerInfo();
                        objDealerInfo.UpdatePhoto(Convert.ToInt32(MainHeadrow[2]), imageData);
                    }
                    #endregion

                    try
                    {
                        objData = new SQLDB();
                        string strSql = "delete from FA_ACCOUNT_MASTER where AM_ACCOUNT_ID='" + Convert.ToInt32(txtDealerCode.Text) + "'";
                        strSql += " INSERT INTO FA_ACCOUNT_MASTER" +
                                       "(AM_COMPANY_CODE, AM_ACCOUNT_ID, AM_ACCOUNT_NAME, AM_SHORT_NAME," +
                                       "AM_ACCOUNT_GROUP_ID, AM_ALIE_ID, AM_ACCOUNT_TYPE_ID, AM_DEFAULT_DEBIT_CREDIT_ID, AM_BUDGET_REQUIRED, AM_BUDGET_LOCK," +
                                       "AM_PL_BS, AM_CREATED_BY, AM_CREATED_DATE)" +
                                       "select DAMH_COMPANY_CODE, DAMH_DEALER_CODE," +
                                       "DAMH_FIRM_NAME+' - '+STR(DAMH_DEALER_CODE,10), LEFT(DAMH_FIRM_NAME,15)," +
                                       "'A020201002', 'A', NULL, 'D', 'N', 'N', 'PL', 'ADMIN', GETDATE() from DL_APPL_MASTER_HEAD WHERE DAMH_DEALER_CODE=" + Convert.ToInt32(txtDealerCode.Text);

                        int iRes = objData.ExecuteSaveData(strSql);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    MessageBox.Show("Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                    txtDealerCode.Focus();
                    txtDealerCode.Text = "";
                    grpPage1.Visible = true;
                    grpPage2.Visible = false;
                    grpPage3.Visible = false;
                }
            }
            //}
            //else
            //{
            //    MessageBox.Show("Please Enter Details", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (CheckData(grpPage1))
            {
                grpPage1.Visible = false;
                grpPage2.Visible = true;
                grpPage3.Visible = false;
            }
             
        }
        byte[] ReadFile(string sPath)
        {
            //Initialize byte array with a null value initially.           
            byte[] data = null;

            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);

            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader(fStream);

            //When you use BinaryReader, you need to supply number of bytes to read from file.
            //In this case we want to read entire file. So supplying total number of bytes.
            data = br.ReadBytes((int)numBytes);
            return data;
        }

        private void txtEcodeApprovBy_TextChanged(object sender, EventArgs e)
        {
            //GetNameForEcode(txtNameApprovBy,txtEcodeApprovBy);
            txtNameApprovBy.Text = GetNameForEcode(txtNameApprovBy.Text, txtEcodeRecruitedBy.Text);
        }
        private string GetNameForEcode(string sNameAppointAprBy, string txtEcodeApprovBy)
        {
            objData = new SQLDB();
            DataTable dt = new DataTable();
            if (txtEcodeApprovBy.Length > 0)
            {
                try
                {
                    string strCmd = "SELECT  MEMBER_NAME FROM EORA_MASTER WHERE ECODE=" + Convert.ToInt32(txtEcodeApprovBy);
                    dt = objData.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        return dt.Rows[0][0].ToString();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {

                }
            }
            return "";
        }

        private void pg2Back_Click(object sender, EventArgs e)
        {
            grpPage1.Visible = true;
            grpPage2.Visible = false;
            grpPage3.Visible = false;
        }

        private void pg3Back_Click(object sender, EventArgs e)
        {
            grpPage1.Visible = false;
            grpPage2.Visible = true;
            grpPage3.Visible = false;
        }

        private void pg2Next_Click(object sender, EventArgs e)
        {
            //if(dtPartners.Rows.Count==0)
            //{
            //    MessageBox.Show("Please enter Partners details.", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //else if (dtPrevTurnOver.Rows.Count == 0)
            //{
            //    MessageBox.Show("Please enter Previous TurnOver details.", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //else if (dtBussinessDetails.Rows.Count == 0)
            //{
            //    MessageBox.Show("Please enter Bussiness details.", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //else if (dtOtherDealerShips.Rows.Count == 0)
            //{
            //    MessageBox.Show("Please enter DealerShip details.", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            grpPage1.Visible = false;
            grpPage2.Visible = false;
            grpPage3.Visible = true;


        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //if (dtBussnessVehicles.Rows.Count == 0)
            //{
            //    MessageBox.Show("Please enter Bussiness Vehicles details.", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //else if (dtTerritoryDetl.Rows.Count == 0)
            //{
            //    MessageBox.Show("Please enter Territory details.", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //else if (dtFixedAssets.Rows.Count == 0)
            //{
            //    MessageBox.Show("Please enter FixedAssets details.", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //else if (dtBankerDetails.Rows.Count == 0)
            //{
            //    MessageBox.Show("Please enter Banker details.", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //if (!SSCRM.App_Code.UtilityLibrary.CustomValidate(grpPage3, toolTip1))
            //{
            //    return  ;
            //}
            //if (dtSecurityCheqs.Rows.Count == 0)
            //{
            //    MessageBox.Show("Please enter SecurityCheqs details.", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    btnAddSecurityCheqs.Focus();
            //    return;
            //}
            //if (CheckData(grpPage3))
            //{
                SaveData();
            //}
        }
        private void btnPartners_Click(object sender, EventArgs e)
        {
            Partners objPartners = new Partners();
            objPartners.dealerApplication = this;
            objPartners.ShowDialog();
        }

        private void btnPrevTurnOver_Click(object sender, EventArgs e)
        {
            PrevTurnOver objPrevTurnOver = new PrevTurnOver();
            objPrevTurnOver.dealerApplication = this;
            objPrevTurnOver.ShowDialog();
        }

        private void btnBussinessDetails_Click(object sender, EventArgs e)
        {
            BussinessDetail objBussinessDetail = new BussinessDetail();
            objBussinessDetail.dealerApplication = this;
            objBussinessDetail.ShowDialog();
        }
        private void btnBankerDetl_Click(object sender, EventArgs e)
        {
            BankDetails objBankDetails = new BankDetails();
            objBankDetails.dealerApplication = this;
            objBankDetails.ShowDialog();
        }


        private void btnFixedAsset_Click(object sender, EventArgs e)
        {
            FixedAssets objFixedAssets = new FixedAssets();
            objFixedAssets.dealerApplication = this;
            objFixedAssets.ShowDialog();
        }
        private void btnAddSecurityCheqs_Click(object sender, EventArgs e)
        {
            SecurityCheq objSecurityCheq = new SecurityCheq();
            objSecurityCheq.dealerApplication = this;
            objSecurityCheq.ShowDialog();
        }

        private void btnTerritoryDetl_Click(object sender, EventArgs e)
        {
            Territory_Details objTerritory_Details = new Territory_Details();
            objTerritory_Details.dealerApplication = this;
            objTerritory_Details.ShowDialog();
        }
        private void btnDD_Click(object sender, EventArgs e)
        {
            OtherDealerships objOtherDealerships = new OtherDealerships();
            objOtherDealerships.dealerApplication = this;
            objOtherDealerships.ShowDialog();
        }
        private void btnBusVeh_Click(object sender, EventArgs e)
        {
            BussinessVehicles objBussinessVehicles = new BussinessVehicles();
            objBussinessVehicles.dealerApplication = this;
            objBussinessVehicles.ShowDialog();
        }

        #region "EDITING AND DELETING GRID DETAILS"
        private void gvPartner_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (gvPartners.Rows[e.RowIndex].Cells["Edit_Partners"].Value.ToString().Trim() != "")
                {
                    if (Convert.ToBoolean(gvPartners.Rows[e.RowIndex].Cells["Edit_Partners"].Selected) == true)
                    {
                        int SlNo = Convert.ToInt32(gvPartners.Rows[e.RowIndex].Cells[gvPartners.Columns["SLNO_Partners"].Index].Value);
                        DataRow[] dr = dtPartners.Select("SlNo=" + SlNo);
                        Partners objPartners = new Partners(dr);
                        objPartners.dealerApplication = this;
                        objPartners.ShowDialog();
                    }
                }
                if (e.ColumnIndex == gvPartners.Columns["Delete_Partners"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvPartners.Rows[e.RowIndex].Cells[gvPartners.Columns["SLNO_Partners"].Index].Value);
                        DataRow[] dr = dtPartners.Select("SlNo=" + SlNo);
                        dtPartners.Rows.Remove(dr[0]);
                        GetPartnerDetails();
                        MessageBox.Show("Selected information Has Been Deleted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void gvPreviousTurnOver_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (gvPreviousTurnOvers.Rows[e.RowIndex].Cells["Edit_PTOs"].Value.ToString().Trim() != "")
                {
                    if (Convert.ToBoolean(gvPreviousTurnOvers.Rows[e.RowIndex].Cells["Edit_PTOs"].Selected) == true)
                    {
                        int SlNo = Convert.ToInt32(gvPreviousTurnOvers.Rows[e.RowIndex].Cells[gvPreviousTurnOvers.Columns["SLNo_PTOs"].Index].Value);
                        DataRow[] dr = dtPrevTurnOver.Select("SlNo=" + SlNo);
                        PrevTurnOver objPartners = new PrevTurnOver(dr);
                        objPartners.dealerApplication = this;
                        objPartners.ShowDialog();
                    }
                }
                if (e.ColumnIndex == gvPreviousTurnOvers.Columns["Delete_PTOs"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvPreviousTurnOvers.Rows[e.RowIndex].Cells[gvPreviousTurnOvers.Columns["SLNo_PTOs"].Index].Value);
                        DataRow[] dr = dtPrevTurnOver.Select("SlNo=" + SlNo);
                        dtPrevTurnOver.Rows.Remove(dr[0]);
                        GetPrevTurnOverDetails();
                        MessageBox.Show("Selected information Has Been Deleted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void gvBd_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (gvBd.Rows[e.RowIndex].Cells["Edit_BDS"].Value.ToString().Trim() != "")
                {
                    if (Convert.ToBoolean(gvBd.Rows[e.RowIndex].Cells["Edit_BDS"].Selected) == true)
                    {
                        int SlNo = Convert.ToInt32(gvBd.Rows[e.RowIndex].Cells[gvBd.Columns["SLNO_BDS"].Index].Value);
                        DataRow[] dr = dtBussinessDetails.Select("SlNo=" + SlNo);
                        BussinessDetail objBussinessDetail = new BussinessDetail(dr);
                        objBussinessDetail.dealerApplication = this;
                        objBussinessDetail.ShowDialog();
                    }
                }
                if (e.ColumnIndex == gvBd.Columns["Delete_BDS"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvBd.Rows[e.RowIndex].Cells[gvBd.Columns["SLNO_BDS"].Index].Value);
                        DataRow[] dr = dtBussinessDetails.Select("SlNo=" + SlNo);
                        dtBussinessDetails.Rows.Remove(dr[0]);
                        GetBussinessDetails();
                        MessageBox.Show("Selected information Has Been Deleted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        
        private void gvDealerships_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (gvDealerships.Rows[e.RowIndex].Cells["Edit_Dealer"].Value.ToString().Trim() != "")
                {
                    if (Convert.ToBoolean(gvDealerships.Rows[e.RowIndex].Cells["Edit_Dealer"].Selected) == true)
                    {
                        int SlNo = Convert.ToInt32(gvDealerships.Rows[e.RowIndex].Cells[gvDealerships.Columns["SLNO_Dealer"].Index].Value);
                        DataRow[] dr = dtOtherDealerShips.Select("SlNo=" + SlNo);
                        OtherDealerships objOtherDealerships = new OtherDealerships(dr);
                        objOtherDealerships.dealerApplication = this;
                        objOtherDealerships.ShowDialog();
                    }
                }
                if (e.ColumnIndex == gvDealerships.Columns["Delete_Dealer"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvDealerships.Rows[e.RowIndex].Cells[gvDealerships.Columns["SLNO_Dealer"].Index].Value);
                        DataRow[] dr = dtOtherDealerShips.Select("SlNo=" + SlNo);
                        dtOtherDealerShips.Rows.Remove(dr[0]);
                        GetOtherDealerShips();
                        MessageBox.Show("Selected information Has Been Deleted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

       

        private void gvBussinessVehl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (gvBussinessVehl.Rows[e.RowIndex].Cells["Edit_BusVeh"].Value.ToString().Trim() != "")
                {
                    if (Convert.ToBoolean(gvBussinessVehl.Rows[e.RowIndex].Cells["Edit_BusVeh"].Selected) == true)
                    {
                        int SlNo = Convert.ToInt32(gvBussinessVehl.Rows[e.RowIndex].Cells[gvBussinessVehl.Columns["SLNO_BusVeh"].Index].Value);
                        DataRow[] dr = dtBussnessVehicles.Select("SlNo=" + SlNo);
                        BussinessVehicles objBussinessVehicles = new BussinessVehicles(dr);
                        objBussinessVehicles.dealerApplication = this;
                        objBussinessVehicles.ShowDialog();
                    }
                }
                if (e.ColumnIndex == gvBussinessVehl.Columns["Delete_BusVeh"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvBussinessVehl.Rows[e.RowIndex].Cells[gvBussinessVehl.Columns["SLNO_BusVeh"].Index].Value);
                        DataRow[] dr = dtBussnessVehicles.Select("SlNo=" + SlNo);
                        dtBussnessVehicles.Rows.Remove(dr[0]);
                        GetBusinessVehicles();
                        MessageBox.Show("Selected information Has Been Deleted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

      

        private void gvTerritoryDetl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (optionalgvTerritoryDetl.Rows[e.RowIndex].Cells["Edit_TerrDetl"].Value.ToString().Trim() != "")
                {
                    if (Convert.ToBoolean(optionalgvTerritoryDetl.Rows[e.RowIndex].Cells["Edit_TerrDetl"].Selected) == true)
                    {
                        int SlNo = Convert.ToInt32(optionalgvTerritoryDetl.Rows[e.RowIndex].Cells[optionalgvTerritoryDetl.Columns["SLNO_TerrDetl"].Index].Value);
                        DataRow[] dr = dtTerritoryDetl.Select("SlNo=" + SlNo);
                        Territory_Details objTerritory_Details = new Territory_Details(dr);
                        objTerritory_Details.dealerApplication = this;
                        objTerritory_Details.ShowDialog();
                    }
                }
                if (e.ColumnIndex == optionalgvTerritoryDetl.Columns["Delete_TerrDetl"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(optionalgvTerritoryDetl.Rows[e.RowIndex].Cells[optionalgvTerritoryDetl.Columns["SLNO_TerrDetl"].Index].Value);
                        DataRow[] dr = dtTerritoryDetl.Select("SlNo=" + SlNo);
                        dtTerritoryDetl.Rows.Remove(dr[0]);
                        GetTerritoryDetl();
                        MessageBox.Show("Selected information Has Been Deleted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        

        private void gvFixedAsset_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (gvFixedAsset.Rows[e.RowIndex].Cells["Edit_FixedDetl"].Value.ToString().Trim() != "")
                {
                if (Convert.ToBoolean(gvFixedAsset.Rows[e.RowIndex].Cells["Edit_FixedDetl"].Selected) == true)
                    {
                        int SlNo = Convert.ToInt32(gvFixedAsset.Rows[e.RowIndex].Cells[gvFixedAsset.Columns["SLNO_FixedDetl"].Index].Value);
                        DataRow[] dr = dtFixedAssets.Select("SlNo=" + SlNo);
                        FixedAssets objFixedAssets = new FixedAssets(dr);
                        objFixedAssets.dealerApplication = this;
                        objFixedAssets.ShowDialog();
                    }
                }
                if (e.ColumnIndex == gvFixedAsset.Columns["Delete_FixedDetl"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvFixedAsset.Rows[e.RowIndex].Cells[gvFixedAsset.Columns["SLNO_FixedDetl"].Index].Value);
                        DataRow[] dr = dtFixedAssets.Select("SlNo=" + SlNo);
                        dtFixedAssets.Rows.Remove(dr[0]);
                        GetFixedAssets();
                        MessageBox.Show("Selected information Has Been Deleted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void gvBankerDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (optionalgvBankerDetails.Rows[e.RowIndex].Cells["Edit_BankerDetl"].Value.ToString().Trim() != "")
                {
                    if (Convert.ToBoolean(optionalgvBankerDetails.Rows[e.RowIndex].Cells["Edit_BankerDetl"].Selected) == true)
                    {
                        int SlNo = Convert.ToInt32(optionalgvBankerDetails.Rows[e.RowIndex].Cells[optionalgvBankerDetails.Columns["SLNO_BankerDetl"].Index].Value);
                        DataRow[] dr = dtBankerDetails.Select("SlNo=" + SlNo);
                        BankDetails objBankDetails = new BankDetails(dr);
                        objBankDetails.dealerApplication = this;
                        objBankDetails.ShowDialog();
                    }
                }
                if (e.ColumnIndex == optionalgvBankerDetails.Columns["Delete_BankerDetl"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(optionalgvBankerDetails.Rows[e.RowIndex].Cells[optionalgvBankerDetails.Columns["SLNO_BankerDetl"].Index].Value);
                        DataRow[] dr = dtBankerDetails.Select("SlNo=" + SlNo);
                        dtBankerDetails.Rows.Remove(dr[0]);
                        GetBankerDetails();
                        MessageBox.Show("Selected information Has Been Deleted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        #endregion
       
        
        private void txtDealerCode_Validated(object sender, EventArgs e)
        {
            ClearFields();
            objData = new SQLDB();
            objDealerInfo = new DealerInfo();
            if (txtDealerCode.Text.Length > 2)
            {
                string sqlCmd = "select * from DL_APPL_MASTER_HEAD where DAMH_DEALER_CODE=" + Convert.ToInt32(txtDealerCode.Text);
                DataTable dt = objData.ExecuteDataSet(sqlCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    FillMainHeadData(dt);
                    dtPartners = objDealerInfo.GetDealerInformation(102, cbCompany.SelectedItem.ToString(), cbBranch.SelectedItem.ToString(), Convert.ToInt32(txtApplNo.Text)).Tables[0];
                    dtPrevTurnOver = objDealerInfo.GetDealerInformation(103, cbCompany.SelectedItem.ToString(), cbBranch.SelectedItem.ToString(), Convert.ToInt32(txtApplNo.Text)).Tables[0];
                    dtBussinessDetails = objDealerInfo.GetDealerInformation(104, cbCompany.SelectedItem.ToString(), cbBranch.SelectedItem.ToString(), Convert.ToInt32(txtApplNo.Text)).Tables[0];
                    dtOtherDealerShips = objDealerInfo.GetDealerInformation(105, cbCompany.SelectedItem.ToString(), cbBranch.SelectedItem.ToString(), Convert.ToInt32(txtApplNo.Text)).Tables[0];
                    dtBussnessVehicles = objDealerInfo.GetDealerInformation(106, cbCompany.SelectedItem.ToString(), cbBranch.SelectedItem.ToString(), Convert.ToInt32(txtApplNo.Text)).Tables[0];
                    dtTerritoryDetl = objDealerInfo.GetDealerInformation(107, cbCompany.SelectedItem.ToString(), cbBranch.SelectedItem.ToString(), Convert.ToInt32(txtApplNo.Text)).Tables[0];
                    dtFixedAssets = objDealerInfo.GetDealerInformation(108, cbCompany.SelectedItem.ToString(), cbBranch.SelectedItem.ToString(), Convert.ToInt32(txtApplNo.Text)).Tables[0];
                    dtSecurityCheqs = objDealerInfo.GetDealerInformation(109, cbCompany.SelectedItem.ToString(), cbBranch.SelectedItem.ToString(), Convert.ToInt32(txtApplNo.Text)).Tables[0];
                    dtBankerDetails = objDealerInfo.GetDealerInformation(110, cbCompany.SelectedItem.ToString(), cbBranch.SelectedItem.ToString(), Convert.ToInt32(txtApplNo.Text)).Tables[0];

                    GetPartnerDetails();
                    GetPrevTurnOverDetails();
                    GetBussinessDetails();
                    GetOtherDealerShips();
                    GetBusinessVehicles();
                    GetTerritoryDetl();
                    GetFixedAssets();
                    GetBankerDetails();
                    
                    
                        DataTable  dtPhoto = objDealerInfo.GetDealerInformation(111, cbCompany.SelectedItem.ToString(), cbBranch.SelectedItem.ToString(), Convert.ToInt32(txtApplNo.Text)).Tables[0];
                        if (dtPhoto.Rows.Count > 0)
                        {
                            if(DBNull.Value != dtPhoto.Rows[0]["DAMH_PHOTO"])
                                GetImage((byte[])dtPhoto.Rows[0]["DAMH_PHOTO"]);
                            else
                                pictureBox1.BackgroundImage = SDMS.Properties.Resources.nomale;
                        }

                        
                }
                //else
                //{
                //    ClearFields();
                //}
            }
            //else
            //{
            //    ClearFields();
            //}
        }
        private void ClearFields()
        {
            updateFlag = false;
           GenerateApplicationNo();
            cbCompany.SelectedIndex =0;
            //cbBranch.SelectedIndex = 0;
            txtFirmName.Text = "";
            dtAppDate.Value = DateTime.Now;
            txtFirmHouseNo.Text =  "";
            txtFirmLandMark_optional.Text =  "";
            txtFirmVillage.Text = "";
            txtFirmMandal.Text =  "";
            txtFirmDistrict.Text = "";
            txtFirmState.Text = "";
            txtFirmPin.Text =  "";
            _optionaltxtFirmPhNo.Text =  "";
            _optionaltxtFirmOffPh.Text = "";
            _optionaltxtFirmResPh.Text = "";
            _optionaltxtFirmFax.Text = "";
            txtFirmMob.Text = "";
            txtFirmEmail_optional.Text =  "";
            txtFirmVillageSearch.Text = "";

            txtDealerName.Text =  "";
            cbMORF.SelectedIndex = 0;
            dtpDealerDob.Value = DateTime.Now;
            cbRelation.SelectedIndex = 0;
            txtDealerFatherName.Text =  "";
            txtDealerHNo.Text = "";
            txtDealerLandMark_optional.Text ="";
            txtDealerVill.Text =  "";
            txtDealerMandal.Text ="";
            txtDealerDistricit.Text =  "";
            txtDealerState.Text = "";
            txtDealerPin.Text = "";
            _optionaltxtDealerPh.Text = "";
            _optionaltxtDealerOffPh.Text = "";
            _optionaltxtDealerResPh.Text =  "";
            _optionaltxtDealerFax.Text =  "";
            txtDealerMob.Text =  "";
            txtDealerEmail_optional.Text = "";
            dtpDealerDoj.Value = DateTime.Now;
            cbFirmType.SelectedIndex = 0;
            txtDealerVillSearch.Text = "";
            txtDelOperPresVillSearch.Text = "";

            txtVATNo_optional.Text =  "";
            txtCSTNo_optional.Text =  "";
            txtPANNo_optional.Text =  "";
            txtPestLicNo_optional.Text = "";
            dpPestLicDate.Value = DateTime.Now;
            dpPestLicValid.Value = DateTime.Now;
            txtPestLIcIssuedBy_optional.Text =  "";
            txtFertLicNo_optional.Text = "";
            dpFertLicDate.Value = DateTime.Now;
            dpFertLicValid.Value = DateTime.Now;
            txtFertLIcIssuedBy_optional.Text = "";
            txtPresentBus_optional.Text = "";
            cbBussType.SelectedIndex = 0;
            txtPrvTurnOverWhSale_optional.Text =  "";
            txtPrvTurnOverRetl_optional.Text =  "";
            txtNoofDealers_optional.Text = "";
            txtDelOperTown.Text =  "";
            txtDelOperMandal.Text = "";
            txtDelOperDist.Text =  "";
            txtDelOperState.Text =  "";
            txtGodownSpace_optional.Text = "";
            txtNoofEmp_optional.Text = "";
            txtExpetTurnOverProd_optional.Text ="";
            txtExpetCapitalProd_optional.Text =  "";
            cbFinArrangType.SelectedIndex = 0;
            txtRepresntCode_optional.Text = "";
            txtEcodeRecruitedBy.Text =  "";
            txtCredLimAmount.Text = "";
            txtEcodeCreditAprroved.Text = "";
                chkSecCheq.Checked = false;
                chkLetterHeads.Checked = false;
                cbARTCL.SelectedIndex = 0;
            chkPar.Checked = false;
            _optionalcheckBox1.Checked = false;
            _optionalcheckBox2.Checked = false;
            //if (dt.Rows[0]["DAMH_ENCL_PD_OR_ARTCL"] == "YES")
            //{
            //    CHK.Checked = true;
            //}
            chkVATRegCopy.Checked = false;
            chkPestLicCopy.Checked = false;
            chkPANCardCopy.Checked = false;
            chkIndemBond.Checked = false;
            chkAnyOthers.Checked = false;
            chkFertCopy.Checked = false;
            txtDcodeRecommendedBy.Text = "";
            txtRemarks.Text = "";

            cbFirmVillage_optional.DataSource = null;
            cbDealerVill_optional.DataSource = null;
            cbDealerOperVill_optional.DataSource = null;
            pictureBox1.BackgroundImage = SDMS.Properties.Resources.nomale;



            dtPartners.Rows.Clear();
            dtPrevTurnOver.Rows.Clear();
            dtBussinessDetails.Rows.Clear();
            dtBussnessVehicles.Rows.Clear();
            dtFixedAssets.Rows.Clear();
            dtOtherDealerShips.Rows.Clear();
            dtSecurityCheqs.Rows.Clear();
            dtTerritoryDetl.Rows.Clear();
            dtBankerDetails.Rows.Clear();
            GetPartnerDetails();
            GetPrevTurnOverDetails();
            GetBussinessDetails();
            GetOtherDealerShips();
            GetBusinessVehicles();
            GetTerritoryDetl();
            GetFixedAssets();
            GetBankerDetails();


            DateTime dtimeDefault = Convert.ToDateTime("01-01-1900");
            dtAppDate.Value = dtimeDefault;
            dtpDealerDoj.Value = dtimeDefault;
            dtpDealerDob.Value = dtimeDefault;
            dpPestLicDate.Value = dtimeDefault;
            dpPestLicValid.Value = dtimeDefault;
            dpFertLicDate.Value = dtimeDefault;
            dpFertLicValid.Value = dtimeDefault;
        }
        private void FillMainHeadData(DataTable dt)
        {
            updateFlag = true;

            txtApplNo.Text = dt.Rows[0]["DAMH_APPL_NUMBER"]+"";
            cbCompany.SelectedValue = dt.Rows[0]["DAMH_COMPANY_CODE"];
            cbBranch.SelectedValue = dt.Rows[0]["DAMH_BRANCH_CODE"];
            txtFirmName.Text = dt.Rows[0]["DAMH_FIRM_NAME"] + "";
            dtAppDate.Value = Convert.ToDateTime( dt.Rows[0]["DAMH_APPL_DATE"] );
            txtFirmHouseNo.Text = dt.Rows[0]["DAMH_FIRM_ADDR_HNO"] + "";
            txtFirmLandMark_optional.Text = dt.Rows[0]["DAMH_FIRM_ADDR_LANDMARK"] + "";
            txtFirmVillageSearch.Text = dt.Rows[0]["DAMH_FIRM_ADDR_VILL_OR_TOWN"] + "";
            txtFirmVillage.Text = dt.Rows[0]["DAMH_FIRM_ADDR_VILL_OR_TOWN"] + "";
            txtFirmMandal.Text = dt.Rows[0]["DAMH_FIRM_ADDR_MANDAL"] + "";
            txtFirmDistrict.Text = dt.Rows[0]["DAMH_FIRM_ADDR_DISTRICT"] + "";
            txtFirmState.Text = dt.Rows[0]["DAMH_FIRM_ADDR_STATE"] + "";
            txtFirmPin.Text = dt.Rows[0]["DAMH_FIRM_ADDR_PIN"] + "";
            _optionaltxtFirmPhNo.Text = dt.Rows[0]["DAMH_FIRM_ADDR_PHONE"] + "";
            _optionaltxtFirmOffPh.Text = dt.Rows[0]["DAMH_FIRM_ADDR_OFF_PHONE"] + "";
            _optionaltxtFirmResPh.Text = dt.Rows[0]["DAMH_FIRM_ADDR_RES_PHONE"] + "";
            _optionaltxtFirmFax.Text = dt.Rows[0]["DAMH_FIRM_ADDR_FAX"] + "";
            txtFirmMob.Text = dt.Rows[0]["DAMH_FIRM_ADDR_MOBILE"] + "";
            txtFirmEmail_optional.Text = dt.Rows[0]["DAMH_FIRM_ADDR_EMAIL"] + "";

            txtDealerName.Text = dt.Rows[0]["DAMH_DEALER_NAME"] + "";
            cbMORF.SelectedItem = dt.Rows[0]["DAMH_SEX"].ToString();
            dtpDealerDob.Value = Convert.ToDateTime( dt.Rows[0]["DAMH_DOB"]);
            cbRelation.SelectedItem = dt.Rows[0]["DAMH_FORH"] + "";
            txtDealerFatherName.Text = dt.Rows[0]["DAMH_FORH_NAME"] + "";
            txtDealerHNo.Text = dt.Rows[0]["DAMH_FIRM_HEAD_HNO"] + "";
            txtDealerLandMark_optional.Text = dt.Rows[0]["DAMH_FIRM_HEAD_LANDMARK"] + "";
            txtDealerVillSearch.Text = dt.Rows[0]["DAMH_FIRM_HEAD_VILL_OR_TOWN"] + "";
            txtDealerVill.Text = dt.Rows[0]["DAMH_FIRM_HEAD_VILL_OR_TOWN"] + "";
            txtDealerMandal.Text = dt.Rows[0]["DAMH_FIRM_HEAD_MANDAL"] + "";
            txtDealerDistricit.Text = dt.Rows[0]["DAMH_FIRM_HEAD_DISTRICT"] + "";
            txtDealerState.Text = dt.Rows[0]["DAMH_FIRM_HEAD_STATE"] + "";
            txtDealerPin.Text = dt.Rows[0]["DAMH_FIRM_HEAD_PIN"] + "";
            _optionaltxtDealerPh.Text = dt.Rows[0]["DAMH_FIRM_HEAD_PHONE"] + "";
            _optionaltxtDealerOffPh.Text = dt.Rows[0]["DAMH_FIRM_HEAD_OFF_PHONE"] + "";
            _optionaltxtDealerResPh.Text = dt.Rows[0]["DAMH_FIRM_HEAD_RES_PHONE"] + "";
            _optionaltxtDealerFax.Text = dt.Rows[0]["DAMH_FIRM_HEAD_FAX"] + "";
            txtDealerMob.Text = dt.Rows[0]["DAMH_FIRM_HEAD_MOBILE"] + "";
            txtDealerEmail_optional.Text = dt.Rows[0]["DAMH_FIRM_HEAD_EMAIL"] + "";
            dtpDealerDoj.Value =Convert.ToDateTime(  dt.Rows[0]["DAMH_DOJ"]);
            cbFirmType.SelectedItem = dt.Rows[0]["DAMH_FIRM_TYPE"] + "";


            txtVATNo_optional.Text = dt.Rows[0]["DAMH_VAT_NUMBER"] + "";
            txtCSTNo_optional.Text = dt.Rows[0]["DAMH_CST_NUMBER"] + "";
            txtPANNo_optional.Text = dt.Rows[0]["DAMH_IT_PAN_NUMBER"] + "";
            txtPestLicNo_optional.Text = dt.Rows[0]["DAMH_PESTICIDE_LICENSE_NUMBER"] + "";
            dpPestLicDate.Value = Convert.ToDateTime( dt.Rows[0]["DAMH_PESTICIDE_LICENSE_DATE"] );
            dpPestLicValid.Value = Convert.ToDateTime( dt.Rows[0]["DAMH_PESTICIDE_LICENSE_VALID_UPTO"] );
            txtPestLIcIssuedBy_optional.Text = dt.Rows[0]["DAMH_PESTICIDE_LICENSE_ISSUED_BY"] + "";
            txtFertLicNo_optional.Text = dt.Rows[0]["DAMH_FERTILIZR_LICENSE_NUMBER"] + "";
            dpFertLicDate.Value =Convert.ToDateTime(  dt.Rows[0]["DAMH_FERTILIZR_LICENSE_DATE"] );
            dpFertLicValid.Value = Convert.ToDateTime( dt.Rows[0]["DAMH_FERTILIZR_LICENSE_VALID_UPTO"]);
            txtFertLIcIssuedBy_optional.Text = dt.Rows[0]["DAMH_FERTILIZR_LICENSE_ISSUED_BY"] + "";
            txtPresentBus_optional.Text = dt.Rows[0]["DAMH_PRESENT_BUSINESS"] + "";
            cbBussType.SelectedItem = dt.Rows[0]["DAMH_BUSINESS_TYPE"] + "";
            txtPrvTurnOverWhSale_optional.Text = dt.Rows[0]["DAMH_PREV_YR_PEST_TURNOVER_WHSALE"] + "";
            txtPrvTurnOverRetl_optional.Text = dt.Rows[0]["DAMH_PREV_YR_PEST_TURNOVER_RETAIL"] + "";
            txtNoofDealers_optional.Text = dt.Rows[0]["DAMH_NOOF_DEALERS"] + "";
            txtDelOperPresVillSearch.Text = dt.Rows[0]["DAMH_PRESENT_AREA_VILL_OR_TOWN"] + "";
            txtDelOperTown.Text = dt.Rows[0]["DAMH_PRESENT_AREA_VILL_OR_TOWN"] + "";
            txtDelOperMandal.Text = dt.Rows[0]["DAMH_PRESENT_AREA_MANDAL"] + "";
            txtDelOperDist.Text = dt.Rows[0]["DAMH_PRESENT_AREA_DISTRICT"] + "";
            txtDelOperState.SelectedText = dt.Rows[0]["DAMH_PRESENT_AREA_STATE"] + "";
            txtGodownSpace_optional.Text = dt.Rows[0]["DAMH_GODOWN_SPACE_SFT"] + "";
            txtNoofEmp_optional.Text = dt.Rows[0]["DAMH_NOOF_EMPLOYEES"] + "";
            txtExpetTurnOverProd_optional.Text = dt.Rows[0]["DAMH_EXPECTED_TURNOVER_OUR_PROD"] + "";
            txtExpetCapitalProd_optional.Text = dt.Rows[0]["DAMH_EXPECTED_CAPITAL_OUR_PROD"] + "";
            cbFinArrangType.SelectedItem = dt.Rows[0]["DAMH_FINANCE_ARRANGE_TYPE"] + "";
            txtRepresntCode_optional.Text = dt.Rows[0]["DAMH_REPRESENT_CODE"] + "";
            txtEcodeRecruitedBy.Text = dt.Rows[0]["DAMH_APPOINTMENT_APPPROVED"] + "";
            txtCredLimAmount.Text = dt.Rows[0]["DAMH_CREDIT_LIMIT_AMOUNT"] + "";
            txtEcodeCreditAprroved.Text = dt.Rows[0]["DAMH_CREDIT_LIMIT_APPROVED_BY"] + "";

            txtDcodeRecommendedBy.Text = dt.Rows[0]["DAMH_APPOINTMENT_RECOMMENDED_BY"] + "";
            txtRemarks.Text = dt.Rows[0]["DAMH_REMARKS"] + "";

            if (dt.Rows[0]["DAMH_ENCL_SECU_CHQS"].ToString()=="YES")
            {
                chkSecCheq.Checked = true;
            }
            if (dt.Rows[0]["DAMH_ENCL_LTR_HEADS"].ToString() == "YES")
            {
                chkLetterHeads.Checked = true;
            }
            cbARTCL.SelectedItem = dt.Rows[0]["DAMH_ENCL_PD_OR_ARTCL"];
            if (cbARTCL.SelectedIndex > 0)
            {
                chkPar.Checked = true;
            }
            //if (dt.Rows[0]["DAMH_ENCL_PD_OR_ARTCL"] == "YES")
            //{
            //    CHK.Checked = true;
            //}
            if (dt.Rows[0]["DAMH_ENCL_VAT_REG_COPY"].ToString() == "YES")
            {
                chkVATRegCopy.Checked = true;
            }
            if (dt.Rows[0]["DAMH_ENCL_PEST_LICE_COPY"].ToString() == "YES")
            {
                chkPestLicCopy.Checked = true;
            }
            if (dt.Rows[0]["DAMH_ENCL_FERT_LICE_COPY"].ToString() == "YES")
            {
                chkFertCopy.Checked = true;
            }
            if (dt.Rows[0]["DAMH_ENCL_IT_PAN_CARD_COPY"].ToString() == "YES")
            {
                chkPANCardCopy.Checked = true;
            }
            if (dt.Rows[0]["DAMH_ENCL_INDBOND_COPY"].ToString() == "YES")
            {
                chkIndemBond.Checked = true;
            }
            if (dt.Rows[0]["DAMH_ANY_OTHER_AGREEMENTS"].ToString().Length >0)
            {
                chkAnyOthers.Checked = true;
            }

        }

        private void txtExpetTurnOverProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtExpetTurnOverProd_optional.TextLength >= 0 && (e.KeyChar == (char)Keys.OemPeriod))
            {
                //tests 
            }
            else
            {
                if (!char.IsControl(e.KeyChar)
                    && !char.IsDigit(e.KeyChar)
                    && e.KeyChar != '.' && e.KeyChar != ',')
                {
                    e.Handled = true;
                }
                // only allow one decimal point
                if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                {
                    e.Handled = true;
                }

               
            }
        }

        private void txtEcodeCreditAprroved_TextChanged(object sender, EventArgs e)
        {
            txtCreditApprovedName.Text = GetNameForEcode(txtNameApprovBy.Text, txtEcodeCreditAprroved.Text);
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "Images (*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|" + "All files (*.*)|*.*";
            od.Multiselect = true;
            if (od.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lblPath.Text = od.FileNames[0].ToString();
                Image loadedImage = Image.FromFile(lblPath.Text);
                //if (loadedImage.Height > 600 || loadedImage.Width > 800)
                //{
                //    lblPath.Text = "";
                //    MessageBox.Show("Please select image between size(600 W * 800 H)", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                pictureBox1.BackgroundImage = loadedImage;
            }
        }
        public void GetImage(byte[] imageData)
        {
            try
            {
                Image newImage;
                using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                {
                    ms.Write(imageData, 0, imageData.Length);
                    newImage = Image.FromStream(ms, true);
                }
                pictureBox1.BackgroundImage = newImage;
                this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
            }
        }

        private void txtDecodeRecommendedBy_TextChanged(object sender, EventArgs e)
        {
         txtRecomndedName.Text = GetNameForEcode("", txtDcodeRecommendedBy.Text);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            if (chk.Checked)
            {
                txtDealerVillSearch.Text = txtFirmVillageSearch.Text;

                txtDealerHNo.Text = txtFirmHouseNo.Text;
                txtDealerLandMark_optional.Text = txtFirmLandMark_optional.Text;
                txtDealerVill.Text = txtFirmVillage.Text;
                txtDealerMandal.Text = txtFirmMandal.Text;
                txtDealerDistricit.Text = txtFirmDistrict.Text;
                txtDealerState.Text = txtFirmState.Text;
                txtDealerPin.Text = txtFirmPin.Text;
            }
            else
            {
                txtDealerVillSearch.Text = "";
                txtDealerHNo.Text = "";
                txtDealerLandMark_optional.Text = "";
                txtDealerVill.Text = "";
                txtDealerMandal.Text = "";
                txtDealerDistricit.Text = "";
                txtDealerState.Text = "";
                txtDealerPin.Text = "";
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            if (chk.Checked)
            {
                txtDelOperPresVillSearch.Text = txtDealerVillSearch.Text;
                txtDelOperTown.Text = txtDealerVill.Text;
                txtDelOperMandal.Text = txtDealerMandal.Text;
                txtDelOperDist.Text = txtDealerDistricit.Text;
                txtDelOperState.Text = txtDealerState.Text;
            }
            else
            {
                txtDelOperPresVillSearch.Text = "";
                txtDelOperTown.Text ="";
                txtDelOperMandal.Text = "";
                txtDelOperDist.Text = "";
                txtDelOperState.Text = "";
            }
        }

        private void txtFirmName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtDealerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtDealerFatherName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtPestLIcIssuedBy_optional_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtFertLIcIssuedBy_optional_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtPANNo_optional_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtFirmVillageSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtDealerVillSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtDelOperPresVillSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtAnyOthers_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtRemarks_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtFirmLandMark_optional_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtDealerLandMark_optional_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtDcodeRecommendedBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }

        private void txtCredLimAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtExpetTurnOverProd_optional.TextLength >= 0 && (e.KeyChar == (char)Keys.OemPeriod))
            {
                //tests 
            }
            else
            {
                if (!char.IsControl(e.KeyChar)
                    && !char.IsDigit(e.KeyChar)
                    && e.KeyChar != '.' && e.KeyChar != ',')
                {
                    e.Handled = true;
                }
                // only allow one decimal point
                if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                {
                    e.Handled = true;
                }


            }
        }

        private void txtFirmPin_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }

        private void txtDealerPin_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }

       
       
        
       
       
    }
}
