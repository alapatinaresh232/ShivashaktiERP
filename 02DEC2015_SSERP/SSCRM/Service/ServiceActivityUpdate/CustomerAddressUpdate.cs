using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using SSCRMDB;
using SSTrans;
using SSAdmin;
using SSCRM.App_Code;

namespace SSCRM
{
    public partial class CustomerAddressUpdate : Form
    {

        private SQLDB objSQLDB = null;
        private InvoiceDB objData = null;
        public ActivityServiceUpdate objActivityServiceUpdate;
        private string strFormerid = string.Empty;
        
        private string strVillage = string.Empty;
        private string strDateOfBirth = string.Empty;
        private string strMarriageDate = string.Empty;
        public string strStateCode = string.Empty;
        private string strBranchCode = string.Empty;
        private string strECode = string.Empty;
        private bool blCustomerSearch = false;
        ServiceDB objServiceDB;
        DataSet ds;
        DataTable dtCustDetails = new DataTable();

        public CustomerAddressUpdate(DataTable dtDetails)
        {
            InitializeComponent();
            dtCustDetails = dtDetails;
        }

        private void CustomerAddressUpdate_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            objServiceDB = new ServiceDB();
            ds = objServiceDB.GetECodesforService("", "0");
            UtilityLibrary.PopulateControl(cmbBranch, ds.Tables[1].DefaultView, 1, 0, "-- Please Select --", 0);
            FillFinYear();
            strFormerid = "";
            txtCustomerid.Text = "";

            if (dtCustDetails.Rows.Count > 0)
            {
                cbCompany.SelectedValue = dtCustDetails.Rows[0]["CompCode"].ToString();
                cmbBranch.SelectedValue = dtCustDetails.Rows[0]["branch_code"].ToString();
                cmbFinYear.SelectedValue = dtCustDetails.Rows[0]["fin_year"].ToString();
                txtInvNo.Text = dtCustDetails.Rows[0]["invoice_number"].ToString();
                txtOrderNo.Text = dtCustDetails.Rows[0]["order_number"].ToString();
                dtpInvoiceDate.Value = Convert.ToDateTime(dtCustDetails.Rows[0]["invoice_date"].ToString());
                txtDocMonth.Text = dtCustDetails.Rows[0]["document_month"].ToString();
                txtVillage.Text = dtCustDetails.Rows[0]["cm_village"].ToString();
                txtMandal.Text = dtCustDetails.Rows[0]["cm_mandal"].ToString();
                txtDistrict.Text = dtCustDetails.Rows[0]["cm_district"].ToString();
                txtState.Text = dtCustDetails.Rows[0]["CM_STATE"].ToString();
                txtPin.Text = dtCustDetails.Rows[0]["cm_pin"].ToString();
                txtCustomerName.Text = dtCustDetails.Rows[0]["cm_farmer_name"].ToString();
                txtHouseNo.Text = dtCustDetails.Rows[0]["cm_house_no"].ToString();
                txtLandMark.Text = dtCustDetails.Rows[0]["cm_landmark"].ToString();
                txtRelationName.Text = dtCustDetails.Rows[0]["cm_forg_name"].ToString();
                txtMobileNo.Text = dtCustDetails.Rows[0]["cm_mobile_number"].ToString();
                txtLanLineNo.Text = dtCustDetails.Rows[0]["cm_land_line_no"].ToString();

                if (dtCustDetails.Rows[0]["cm_marriage_date"].ToString() != "")
                {
                    if (dtCustDetails.Rows[0]["cm_marriage_date"].ToString().Substring(0, 10) + "" == "01/01/1900" || dtCustDetails.Rows[0]["cm_marriage_date"].ToString().Substring(0, 10) + "" == "01-01-1900")
                        dtpMarrageDate.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyy"));
                    else
                        dtpMarrageDate.Value = Convert.ToDateTime(Convert.ToDateTime(dtCustDetails.Rows[0]["cm_marriage_date"] + "").ToString("dd/MM/yyy")); 
                }

                if (dtCustDetails.Rows[0]["cm_dob"].ToString() != "")
                {
                    if (dtCustDetails.Rows[0]["cm_dob"].ToString().Substring(0, 10) + "" == "01/01/1900" || dtCustDetails.Rows[0]["cm_dob"].ToString().Substring(0, 10) + "" == "01-01-1900")
                        dtpDateOfBirth.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyy"));
                    else
                        dtpDateOfBirth.Value = Convert.ToDateTime(Convert.ToDateTime(dtCustDetails.Rows[0]["cm_dob"] + "").ToString("dd/MM/yyy"));
                }
               
                txtAge.Text = dtCustDetails.Rows[0]["cm_age"].ToString();                
               // cbCustomer.SelectedValue = dtCustDetails.Rows[0]["farmer_ID"].ToString();
                cbRelation.Text = dtCustDetails.Rows[0]["cm_so_fo"].ToString();
                strFormerid = dtCustDetails.Rows[0]["farmer_ID"].ToString();
                txtCustomerid.Text = strFormerid;

               
            }
        }
    
     
       
        private void FillCompanyData()
        {
            objSQLDB = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT CM_COMPANY_CODE,CM_COMPANY_NAME FROM COMPANY_MAS WHERE ACTIVE='T' ORDER BY CM_COMPANY_NAME";
                dt = objSQLDB.ExecuteDataSet(strCmd).Tables[0];

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
                cbCompany.SelectedValue = CommonData.CompanyCode;
                dt = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLDB = null;

            }
        }

        private void FillFinYear()
        {
            objSQLDB = new SQLDB();
            DataTable dt = new DataTable();
            try
            {

                string strCmd = "SELECT DISTINCT FY_FIN_YEAR FinYear FROM FIN_YEAR";
                dt = objSQLDB.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cmbFinYear.DataSource = dt;
                    cmbFinYear.DisplayMember = "FinYear";
                    cmbFinYear.ValueMember = "FinYear";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLDB = null;
                dt = null;

            }
        }
    
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtHouseNo.Text = "";
            txtLanLineNo.Text = "";
            txtMobileNo.Text = "";
            txtRelationName.Text = "";
            txtAge.Text = "";
           
        }
                
          

     

        private void FillCustomerFarmerData(string sSearch, string sCustId)
        {
            DataSet ds = null;
            objData = new InvoiceDB();
            try
            {
                if (sSearch.Trim() != "" || sCustId.Length > 0)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    ds = new DataSet();
                    ds = objData.InvCustomerSearch_Get(sSearch, txtVillage.Text.ToString(), txtMandal.Text.ToString(), txtState.Text.ToString());
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count == 1)
                    {
                        strFormerid = Convert.ToString(dt.Rows[0]["cm_farmer_ID"]);
                        txtCustomerid.Text = Convert.ToString(dt.Rows[0]["cm_farmer_ID"]);
                        //txtCustomerName.Text = dt.Rows[0]["cm_farmer_name"] + "";
                        txtRelationName.Text = dt.Rows[0]["cm_forg_name"] + "";
                        cbRelation.Text = dt.Rows[0]["cm_so_fo"] + "";
                        txtHouseNo.Text = Convert.ToString(dt.Rows[0]["cm_house_no"]);
                        txtLandMark.Text = Convert.ToString(dt.Rows[0]["cm_landmark"]);
                        txtMobileNo.Text = dt.Rows[0]["cm_mobile_number"] + "";
                        txtLanLineNo.Text = dt.Rows[0]["cm_land_line_no"] + "";

                        if (dt.Rows[0]["cm_marriage_date"].ToString().Substring(0, 10) == "01/01/1900" || dt.Rows[0]["cm_marriage_date"].ToString().Substring(0, 10) == "01-01-1900")
                            dtpMarrageDate.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyyy"));
                        else
                            dtpMarrageDate.Value = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0]["cm_marriage_date"]).ToString("dd/MM/yyyy"));

                        if (dt.Rows[0]["cm_dob"].ToString().Substring(0, 10) == "01/01/1900" || dt.Rows[0]["cm_dob"].ToString().Substring(0, 10) == "01-01-1900")
                            dtpDateOfBirth.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyyy"));
                        else
                            dtpDateOfBirth.Value = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0]["cm_dob"]).ToString("dd/MM/yyyy"));

                        txtAge.Text = dt.Rows[0]["cm_age"] + "";


                    }
                    if (dt.Rows.Count >= 1)
                    {

                        FillCustomerComboBox(dt);

                    }
                    else
                    {
                        cbCustomer.DataSource = null;
                        cbCustomer.DataBindings.Clear();
                        cbCustomer.Items.Clear();
                        ///txtCustomerName.Text = "";
                        txtRelationName.Text = "";
                        txtMobileNo.Text = "";
                        txtHouseNo.Text = "";
                        txtLandMark.Text = "";
                        txtLanLineNo.Text = "";
                        txtAge.Text = "";
                        strFormerid = "";
                        txtCustomerid.Text = "";
                    }
                }

            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objData = null;
                ds.Dispose();
                Cursor.Current = Cursors.Default;

            }

        }

        private void FillAddressComboBox(DataTable dt)
        {
            DataTable dataTable = new DataTable("Village");

            dataTable.Columns.Add("StateID", typeof(String));
            dataTable.Columns.Add("Panchayath", typeof(String));
            dataTable.Columns.Add("Mandal", typeof(String));
            dataTable.Columns.Add("District", typeof(String));
            dataTable.Columns.Add("State", typeof(String));
            dataTable.Columns.Add("Pin", typeof(String));
            cbVillage.DataBindings.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
                dataTable.Rows.Add(new String[] { dt.Rows[i]["CDState"] + 
                     "", dt.Rows[i]["PANCHAYAT"] + 
                     "", dt.Rows[i]["MANDAL"] + 
                     "", dt.Rows[i]["DISTRICT"] + 
                     "", dt.Rows[i]["STATE"] + "", dt.Rows[i]["PIN"] + ""});


            cbVillage.DataSource = dataTable;
            cbVillage.DisplayMember = "Panchayath";
            cbVillage.ValueMember = "StateID";
        }

        private void FillCustomerComboBox(DataTable dt)
        {
            DataTable dataCustomer = new DataTable("Customer");
            dataCustomer.Columns.Add("farmer_ID", typeof(String));
            dataCustomer.Columns.Add("farmer_name", typeof(String));
            dataCustomer.Columns.Add("forg_name", typeof(String));
            dataCustomer.Columns.Add("village", typeof(String));

            cbCustomer.DataBindings.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
                dataCustomer.Rows.Add(new String[] { dt.Rows[i]["cm_farmer_ID"] + 
                     "", dt.Rows[i]["cm_farmer_name"] + "", dt.Rows[i]["cm_forg_name"] + 
                     "", dt.Rows[i]["cm_village"] + ""});

            cbCustomer.DataSource = dataCustomer;
            cbCustomer.DisplayMember = "farmer_name";
            cbCustomer.ValueMember = "farmer_ID";
        }

        private bool FindInputAddressSearch()
        {
            bool blFind = false;
            try
            {
                for (int i = 0; i < this.cbVillage.Items.Count; i++)
                {
                    string strItem = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[1] + "".ToString().Trim();
                    if (strItem.IndexOf(txtVillageSearch.Text) > -1)
                    {
                        blFind = true;
                        cbVillage.SelectedIndex = i;
                        txtVillage.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[1] + "";
                        txtMandal.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[2] + "";
                        txtDistrict.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[3] + "";
                        txtState.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[4] + "";
                        txtPin.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[5] + "";
                        strStateCode = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[0] + "";
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

        private bool FindInputCustomerSearch()
        {
            bool blFind = false;
            objData = new InvoiceDB();
            try
            {
                for (int i = 0; i < this.cbCustomer.Items.Count; i++)
                {
                    string strItem = ((System.Data.DataRowView)(this.cbCustomer.Items[i])).Row.ItemArray[1] + "".ToString().Trim();
                    if (strItem.IndexOf(txtCustomerName.Text) > -1)
                    {
                        DataSet ds = new DataSet();
                        blFind = true;
                        cbCustomer.SelectedIndex = i;
                        ds = objData.InvCustomerSearch_Get(strItem, txtVillage.Text.ToString(), txtMandal.Text.ToString(), txtState.Text.ToString());
                        DataTable dt = ds.Tables[0];

                        if (dt.Rows.Count == 1)
                        {
                            strFormerid = Convert.ToString(dt.Rows[0]["cm_farmer_ID"]);
                            txtCustomerid.Text = strFormerid;
                            //txtCustomerName.Text = dt.Rows[0]["cm_farmer_name"] + "";
                            txtRelationName.Text = dt.Rows[0]["cm_forg_name"] + "";
                            cbRelation.Text = dt.Rows[0]["cm_so_fo"] + "";
                            txtHouseNo.Text = Convert.ToString(dt.Rows[0]["cm_house_no"]);
                            txtLandMark.Text = Convert.ToString(dt.Rows[0]["cm_landmark"]);
                            txtMobileNo.Text = dt.Rows[0]["cm_mobile_number"] + "";
                            txtLanLineNo.Text = dt.Rows[0]["cm_land_line_no"] + "";

                            if (dt.Rows[0]["cm_marriage_date"].ToString().Substring(0, 10) + "" == "01/01/1900" || dt.Rows[0]["cm_marriage_date"].ToString().Substring(0, 10) + "" == "01-01-1900")
                                dtpMarrageDate.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyy"));
                            else
                                dtpMarrageDate.Value = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0]["cm_marriage_date"] + "").ToString("dd/MM/yyy"));

                            if (dt.Rows[0]["cm_dob"].ToString().Substring(0, 10) + "" == "01/01/1900" || dt.Rows[0]["cm_dob"].ToString().Substring(0, 10) + "" == "01-01-1900")
                                dtpDateOfBirth.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyy"));
                            else
                                dtpDateOfBirth.Value = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0]["cm_dob"] + "").ToString("dd/MM/yyy"));

                            txtAge.Text = dt.Rows[0]["cm_age"] + "";

                            break;
                        }
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
                objData = null;
            }
            return blFind;
        }

       

        private void btnVSearch_Click(object sender, EventArgs e)
        {
            VillageSearch VSearch = new VillageSearch("CustomerAddressUpdate");
            VSearch.objCustomerAddressUpdate = this;
            VSearch.ShowDialog();
        }

        private void cbVillage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbVillage.SelectedIndex > -1)
            {
                if (this.cbVillage.Items[cbVillage.SelectedIndex].ToString() != "")
                {
                    txtVillage.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[1] + "";
                    txtMandal.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[2] + "";
                    txtDistrict.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[3] + "";
                    txtState.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[4] + "";
                    txtPin.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[5] + "";
                    strStateCode = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[0] + "";
                }
            }
        }

        private void FillAddressData(string sSearch)
        {
            Hashtable htParam = null;
            objData = new InvoiceDB();
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
                dsVillage = objData.GetVillageDataSet(htParam);
                dtVillage = dsVillage.Tables[0];
                if (dtVillage.Rows.Count == 1)
                {
                    txtVillage.Text = dtVillage.Rows[0]["PANCHAYAT"].ToString();  // ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[1] + "";
                    txtMandal.Text = dtVillage.Rows[0]["Mandal"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[2]+ ""; 
                    txtDistrict.Text = dtVillage.Rows[0]["District"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[3] + "";
                    txtState.Text = dtVillage.Rows[0]["State"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[4] + "";
                    txtPin.Text = dtVillage.Rows[0]["PIN"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[5] + "";
                    strStateCode = dtVillage.Rows[0]["CDState"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[0] + "";
                }
                else if (dtVillage.Rows.Count > 1)
                {
                    txtVillage.Text = "";
                    txtMandal.Text = "";
                    txtDistrict.Text = "";
                    txtState.Text = "";
                    txtPin.Text = "";
                    strStateCode = "";
                    FillAddressComboBox(dtVillage);
                }

                else
                {
                    htParam = new Hashtable();
                    htParam.Add("sVillage", "%" + sSearch);
                    htParam.Add("sDistrict", strDist);
                    dsVillage = new DataSet();
                    dsVillage = objData.GetVillageDataSet(htParam);
                    dtVillage = dsVillage.Tables[0];
                    FillAddressComboBox(dtVillage);
                    ClearVillageDetails();
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

        private void txtVillageSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtVillageSearch.Text.Length == 0)
                {
                    cbVillage.DataSource = null;
                    cbVillage.DataBindings.Clear();
                    cbVillage.Items.Clear();

                   // ClearVillageDetails();
                }
                else if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                {
                    if (FindInputAddressSearch() == false)
                    {
                        FillAddressData(txtVillageSearch.Text);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void ClearVillageDetails()
        {
            txtVillage.Text = "";
            txtMandal.Text = "";
            txtDistrict.Text = "";
            txtState.Text = "";
            txtPin.Text = "";
        }

     
    
        private bool CheckData()
        {
            bool blValue = true;

            if (Convert.ToString(txtVillage.Text).Length == 0 || txtVillage.Text.ToString().Trim() == "NOVILLAGE")
            {
                MessageBox.Show("Enter Village!", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtVillageSearch.Focus();
                return blValue;
            }
            if (txtState.Text.Length == 0)
            {
                MessageBox.Show("No State for " + txtVillage.Text + " Village!");
                blValue = false;
                txtVillageSearch.Focus();
                return blValue;
            }

            if (cbCustomer.Text.ToString().Trim().Length > 0)
                txtCustomerName.Text = cbCustomer.Text;

            if (Convert.ToString(txtCustomerName.Text).Length == 0 || txtCustomerName.Text.ToString().Trim() == "NOVILLAGE")
            {
                MessageBox.Show("Enter Customer name!", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtCustomerName.Focus();
                return blValue;
            }
            if (Convert.ToInt32(Convert.ToDateTime(dtpDateOfBirth.Value).ToString("yyyy")) < 1950)
            {
                MessageBox.Show("Enter valid  Date !", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                dtpDateOfBirth.Focus();
                return blValue;
            }
            if (Convert.ToDateTime(Convert.ToDateTime(dtpDateOfBirth.Value).ToString("dd/MM/yyyy")) > Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyyy")))
            {
                MessageBox.Show("Date should be less than to day", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                dtpDateOfBirth.Focus();
                return blValue;
            }
            if (Convert.ToDateTime(Convert.ToDateTime(dtpMarrageDate.Value).ToString("dd/MM/yyyy")) > Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy")))
            {
                MessageBox.Show("Date should be less than to day", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                dtpMarrageDate.Focus();
                return blValue;
            }
            return blValue;

        }




        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {
                if (SaveCustomerData() >= 1)
                {
                    SaveInvoiceHead();
                    SaveSaleBultinHead();
                    MessageBox.Show("Address Saved SuccessFully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    ((ActivityServiceUpdate)objActivityServiceUpdate).GetAllInvoicdeData(txtOrderNo.Text.ToString(),txtInvNo.Text);
                }
                else
                {
                    MessageBox.Show("Address Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private int SaveCustomerData()
        {
            objSQLDB = new SQLDB();
            objData = new InvoiceDB();
            object strDOB = string.Empty;
            object strMarDate = string.Empty;
            object strAge = null;
            string sqlText = string.Empty;
            int intRec = 0;
            if (strDateOfBirth.Length > 5)
                strDOB = Convert.ToDateTime(strDateOfBirth).ToString("dd/MMM/yyyy");
            else
                strDOB = "";

            if (strMarriageDate.Length > 5)
                strMarDate = Convert.ToDateTime(strMarriageDate).ToString("dd/MMM/yyyy");
            else
                strMarDate = "";
            if (txtAge.Text.Length > 0)
                strAge = Convert.ToInt16(txtAge.Text);
            if (strAge == null)
                strAge = "0";
            try
            {
                if (strFormerid != "" && strFormerid != "99999")
                {
                    try
                    {
                        intRec = Convert.ToInt32(objSQLDB.ExecuteDataSet("SELECT count(cm_farmer_id) from Customer_Mas where cm_farmer_id ='" + strFormerid + "'").Tables[0].Rows[0][0]);
                    }
                    catch { intRec = 0; }
                }
                else { intRec = 0; }

                if (intRec == 0 || strFormerid == "" || strFormerid == "99999")
                {
                    strFormerid = objData.GetCustomerFarmerId(txtState.Text.ToString().Trim(), txtDistrict.Text.ToString().Trim(), txtMandal.Text.ToString().Trim(), txtVillage.Text.ToString().Trim()).ToString();
                    if (strAge == null)
                        strAge = "0";
                    sqlText = " INSERT INTO Customer_Mas(cm_village, cm_farmer_ID" +
                       ", cm_farmer_name, cm_so_fo, cm_forg_name, cm_mobile_number" +
                       ", cm_land_line_no, cm_mandal, cm_district, cm_state, cm_pin, cm_dob, cm_marriage_date, cm_age" +
                       ", cm_house_no, cm_landmark, CM_CREATED_BY, CM_CREATED_DATE) VALUES ( '" + txtVillage.Text + "".Trim() + "', '" + strFormerid +
                       "', '" + txtCustomerName.Text + "".Trim() + "','" + cbRelation.Text + "','" + txtRelationName.Text + "'" +
                       ", '" + txtMobileNo.Text + "" + "','" + txtLanLineNo.Text + "'".Trim() +
                       ", '" + txtMandal.Text + "" + "', '" + txtDistrict.Text + "', '" + txtState.Text +
                       "', '" + txtPin.Text + "', '" + strDOB + "','" + strMarDate + "', " + strAge +
                       ", '" + txtHouseNo.Text + "".Trim() +
                       "', '" + txtLandMark.Text + "".Trim() + "', '" + CommonData.LogUserId + "', '" + CommonData.CurrentDate + "')";
                }
                else
                {

                    sqlText = " UPDATE Customer_Mas SET  " +
                       " cm_village='" + txtVillage.Text + "".Trim() +
                       "', cm_farmer_name ='" + txtCustomerName.Text + "".Trim() +
                       "', cm_so_fo='" + cbRelation.Text.ToString() +
                       "', cm_forg_name='" + txtRelationName.Text + "'" +
                       ", cm_mobile_number='" + txtMobileNo.Text + "" +
                       "', cm_land_line_no='" + txtLanLineNo.Text + "".Trim() +
                       "', cm_mandal='" + txtMandal.Text + "" +
                       "', cm_district='" + txtDistrict.Text +
                       "', cm_state='" + txtState.Text +
                       "', cm_pin='" + txtPin.Text +
                       "', cm_dob='" + strDOB +
                       "', cm_marriage_date='" + strMarDate +
                       "', cm_age=" + strAge +
                       ", cm_house_no='" + txtHouseNo.Text +
                       "', cm_landmark='" + txtLandMark.Text +
                       "', CM_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                       "', CM_LAST_MODIFIED_DATE=getdate() " +
                       "  WHERE cm_farmer_id='" + strFormerid + "'";
                }
                intRec = 0;

                if (sqlText.Length > 10)
                {
                    intRec = objSQLDB.ExecuteSaveData(sqlText);
                }

            }
            catch (Exception ex)
            {
                intRec = 0;
                MessageBox.Show(ex.Message);
            }
           
            return intRec;
        }

        private int SaveInvoiceHead()
        {
            objSQLDB = new SQLDB();
            int iRes = 0;
            string strCmd = "";
            try
            {
                strCmd = "UPDATE SALES_INV_HEAD SET SIH_FARMER_ID='"+ strFormerid +
                        "',SIH_LAST_MODIFIED_BY='"+ CommonData.LogUserId +
                        "',SIH_LAST_MODIFIED_DATE=getdate() "+
                        "  WHERE SIH_COMPANY_CODE='"+ cbCompany.SelectedValue.ToString() +
                        "' and SIH_BRANCH_CODE='"+ cmbBranch.SelectedValue.ToString() +
                        "' and SIH_DOCUMENT_MONTH='"+ txtDocMonth.Text.ToString() +
                        "' and SIH_FIN_YEAR='"+ cmbFinYear.SelectedValue.ToString() +
                        "' AND SIH_ORDER_NUMBER='"+ txtOrderNo.Text.ToString() +
                        "' and SIH_INVOICE_NUMBER="+ txtInvNo.Text +"";

                if (strCmd.Length > 10)
                {
                    iRes = objSQLDB.ExecuteSaveData(strCmd);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iRes;
        }

        private int SaveSaleBultinHead()
        {
            objSQLDB = new SQLDB();
            int iRes = 0;
            string strCmd = "";
            try
            {
                
                strCmd = "UPDATE SALES_INV_BULTIN_HEAD SET SIBH_FARMER_ID='" + strFormerid + 
                        "',SIBH_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                        "',SIBH_LAST_MODIFIED_DATE=getdate() "+
                        "  WHERE SIBH_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                        "' and SIBH_BRANCH_CODE='" + cmbBranch.SelectedValue.ToString() +
                        "' and SIBH_DOCUMENT_MONTH='"+ txtDocMonth.Text.ToString() +
                        "' and SIBH_FIN_YEAR='" + cmbFinYear.SelectedValue.ToString() +
                        "' AND SIBH_ORDER_NUMBER='" + txtOrderNo.Text.ToString() +
                        "' and SIBH_INVOICE_NUMBER=" + txtInvNo.Text + "";

                if (strCmd.Length > 10)
                {
                   iRes = objSQLDB.ExecuteSaveData(strCmd);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iRes;
        }

       

        private void cbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Master objMData = new Master();
            try
            {
                if (cbCustomer.SelectedIndex > -1)
                {
                    if (this.cbCustomer.Items[cbCustomer.SelectedIndex].ToString() != "")
                    {
                        DataSet ds = new DataSet();
                        ds = objMData.CustomerData_Get(((System.Data.DataRowView)(cbCustomer.Items[cbCustomer.SelectedIndex])).Row.ItemArray[0].ToString());
                        DataTable dt = ds.Tables[0];

                        if (dt.Rows.Count == 1)
                        {
                            strFormerid = Convert.ToString(dt.Rows[0]["cm_farmer_ID"]);
                            txtCustomerid.Text = strFormerid;

                            //txtCustomerName.Text = dt.Rows[0]["cm_farmer_name"] + "";
                            txtRelationName.Text = dt.Rows[0]["cm_forg_name"] + "";
                            cbRelation.Text = dt.Rows[0]["cm_so_fo"] + "";
                            txtHouseNo.Text = Convert.ToString(dt.Rows[0]["cm_house_no"]);
                            txtLandMark.Text = Convert.ToString(dt.Rows[0]["cm_landmark"]);
                            txtMobileNo.Text = dt.Rows[0]["cm_mobile_number"] + "";
                            txtLanLineNo.Text = dt.Rows[0]["cm_land_line_no"] + "";

                            if (dt.Rows[0]["cm_marriage_date"].ToString().Substring(0, 10) + "" == "01/01/1900" || dt.Rows[0]["cm_marriage_date"].ToString().Substring(0, 10) + "" == "01-01-1900")
                                dtpMarrageDate.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyy"));
                            else
                                dtpMarrageDate.Value = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0]["cm_marriage_date"] + "").ToString("dd/MM/yyy"));

                            if (dt.Rows[0]["cm_dob"].ToString().Substring(0, 10) + "" == "01/01/1900" || dt.Rows[0]["cm_dob"].ToString().Substring(0, 10) + "" == "01-01-1900")
                                dtpDateOfBirth.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyy"));
                            else
                                dtpDateOfBirth.Value = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0]["cm_dob"] + "").ToString("dd/MM/yyy"));

                            txtAge.Text = dt.Rows[0]["cm_age"] + "";
                        }
                        ds = null;
                        dt = null;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                objMData = null;
                cbCustomer.CausesValidation = false;
            }
        }

        private void txtCustomerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtRelationName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtCustomerName_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtCustomerName.Text.Length > 0)
                {
                    cbCustomer.DataSource = null;
                    cbCustomer.Items.Clear();
                    if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                    {
                        blCustomerSearch = true;
                        if (FindInputCustomerSearch() == false)
                        {
                            FillCustomerFarmerData(txtCustomerName.Text, "");
                        }
                    }

                }

            }
            catch (Exception ex)
            {

            }
            if (e.KeyValue == 8)
            {
                if (this.txtCustomerName.TextLength >= 2)
                    FillCustomerFarmerData(Convert.ToString(txtCustomerName.Text.Trim()), "");
                this.txtCustomerName.SelectionStart = this.txtCustomerName.TextLength;
            }
        }

        private void txtAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            dtpDateOfBirth.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyy"));
        }

      

      
                     
    }
}
