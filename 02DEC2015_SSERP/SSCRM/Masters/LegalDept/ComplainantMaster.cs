using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using System.Collections;
using SSTrans;
using SSAdmin;

namespace SSCRM
{
    public partial class ComplainantMaster : Form
    {
        InvoiceDB objInvoiceData = null;
        Master objMstr = null;
        LeagalCaseDetails lcd = null;
        string strName="";
        bool flagUpdate = false;
        public ComplainantMaster(LeagalCaseDetails lc,string sName)
        {
            lcd = lc;
            strName = sName;
            InitializeComponent();
        }
        public ComplainantMaster()
        {
            InitializeComponent();
        }

        private void ComplainantMaster_Load(object sender, EventArgs e)
        {
            GenerateNewComplainantMaster();
            txtName.Text = strName;
        }
       
        private void GenerateNewComplainantMaster()
        {
            SQLDB objDb = new SQLDB();
            DataSet ds = new DataSet();
            DataTable dt;
            try
            {
                string strCommand = "SELECT ISNULL(MAX(LCM_COMPLAINANT_ID), 0)+1 from LEGAL_COMPLAINANT_MASTER";
                ds = objDb.ExecuteDataSet(strCommand);
                dt = ds.Tables[0];
                txtComplainantId.Text = Convert.ToInt32(dt.Rows[0][0]).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                ds = null;
                dt = null;
                objDb = null;
            }        
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            flagUpdate = false;
            GenerateNewComplainantMaster();
            txtName.Text = "";
            txtHNo.Text = "";
            txtLandMark.Text = "";
            txtVill.Text = "";
            txtMandal.Text = "";
            txtDistricit.Text = "";
            txtState.Text = "";
            txtPin.Text = "";
            txtPhone.Text = "";
            txtDesig.Text = "";
            txtVillSearch.Text = "";
            cbVill.DataSource = null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(CheckData())
            {
                if(SaveComplainanatMaster()>0)
                {
                    MessageBox.Show("Data Added Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //lcd.txtComplnAddress.Text = txtName.Text+"\n"+txtHNo.Text+","+txtMandal.Text+","+txtLandMark.Text+","+txtDistricit.Text+","+txtState.Text+","+txtPin.Text+","+txtPhone.Text;
                    //lcd.txtRemarks.Tag = txtComplainantId.Text;
                    if(lcd!=null)
                    {
                    lcd.FillComplainantData("0");
                    lcd.cbComplainantName.SelectedValue = txtComplainantId.Text;
                    }
                    flagUpdate = false;
                    btnClear_Click(null, null);

                    this.Close();
                }
            }
        }

        private int SaveComplainanatMaster()
        {
            int iSave=0;
            string strSQL = "";
            if (txtPin.Text == "")
            {
                txtPin.Text = "0";
            }
            try
            {
                if (flagUpdate == false)
                {
                    strSQL = " INSERT INTO LEGAL_COMPLAINANT_MASTER(LCM_COMPLAINANT_ID" +
                                                                    ",LCM_COMPLAINANT_NAME" +
                                                                    ",LCM_COMPLAINANT_DESIGNATION" +
                                                                    ",LCM_ADD_PRES_ADDR_HNO" +
                                                                    ",LCM_ADD_PRES_ADDR_LANDMARK" +
                                                                    ",LCM_ADD_PRES_ADDR_VILL_OR_TOWN" +
                                                                    ",LCM_ADD_PRES_ADDR_MANDAL" +
                                                                    ",LCM_ADD_PRES_ADDR_DISTRICT" +
                                                                    ",LCM_ADD_PRES_ADDR_STATE" +
                                                                    ",LCM_ADD_PRES_ADDR_PIN" +
                                                                    ",LCM_ADD_PRES_ADDR_PHONE" +
                                                                    ",LCM_CREATED_BY" +
                                                                    ",LCM_CREATED_DATE)" +
                                                                    " values(" + txtComplainantId.Text.Trim() +
                                                                    ",'" + txtName.Text.Trim() +
                                                                    "','" + txtDesig.Text +
                                                                    "','" + txtHNo.Text +
                                                                    "','" + txtLandMark.Text +
                                                                    "','" + txtVill.Text +
                                                                    "','" + txtMandal.Text +
                                                                    "','" + txtDistricit.Text +
                                                                    "','" + txtState.Text +
                                                                    "','" + txtPin.Text +
                                                                    "','" + txtPhone.Text +
                                                                    "','" + CommonData.LogUserId +
                                                                    "',getdate())";
                }
                else
                {
                    strSQL = " UPDATE LEGAL_COMPLAINANT_MASTER SET LCM_COMPLAINANT_NAME = '" + txtName.Text.Trim() +
                                                                   "',LCM_COMPLAINANT_DESIGNATION= '" + txtDesig.Text +
                                                                   "',LCM_ADD_PRES_ADDR_HNO= '" + txtHNo.Text +
                                                                   "',LCM_ADD_PRES_ADDR_LANDMARK= '" + txtLandMark.Text +
                                                                   "',LCM_ADD_PRES_ADDR_VILL_OR_TOWN= '" + txtVill.Text +
                                                                   "',LCM_ADD_PRES_ADDR_MANDAL= '" + txtMandal.Text +
                                                                   "',LCM_ADD_PRES_ADDR_DISTRICT= '" + txtDistricit.Text +
                                                                   "',LCM_ADD_PRES_ADDR_STATE= '" + txtState.Text +
                                                                   "',LCM_ADD_PRES_ADDR_PIN= '" + txtPin.Text +
                                                                   "',LCM_ADD_PRES_ADDR_PHONE= '" + txtPhone.Text +
                                                                   "',LCM_MODIFIED_BY= '" + CommonData.LogUserId +
                                                                   "',LCM_MODIFIED_DATE=getdate() "  +
                                                                   " WHERE LCM_COMPLAINANT_ID = " + txtComplainantId.Text.Trim();
                }
                SQLDB objSqlDb = new SQLDB();
                iSave = objSqlDb.ExecuteSaveData(strSQL);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iSave;
        }

        private bool CheckData()
        {
            bool flag = true;
            if(txtName.Text.Length==0)
            {
                flag = false;
                MessageBox.Show("Please Enter Name", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return flag;
            }
            //if (txtHNo.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter H No.", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtHNo.Focus();
            //    return flag;
            //}
            //if (txtMandal.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter Village", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtVillSearch.Focus();
            //    return flag;
            //}
            if (txtPhone.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Phone", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return flag;
            }

            return flag;
        }

        private void txtVillSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtVillSearch.Text.Length == 0)
                {
                    cbVill.DataSource = null;
                    cbVill.DataBindings.Clear();
                    cbVill.Items.Clear();
                    //if (btnSave.Enabled == true)
                    ClearVillageDetails();
                }
                else if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                {
                    if (FindInputAddressSearch() == false)
                    {
                        FillAddressData(txtVillSearch.Text);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void FillAddressData(string sSearch)
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
                        txtVill.Text = dtVillage.Rows[0]["PANCHAYAT"].ToString();  // ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[1] + "";
                        txtMandal.Text = dtVillage.Rows[0]["Mandal"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[2]+ ""; 
                        txtDistricit.Text = dtVillage.Rows[0]["District"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[3] + "";
                        txtState.Text = dtVillage.Rows[0]["State"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[4] + "";
                        txtPin.Text = dtVillage.Rows[0]["PIN"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[5] + "";
                }
                else if (dtVillage.Rows.Count > 1)
                {
                    ClearVillageDetails();
                    FillAddressComboBox(dtVillage);
                }

                else
                {
                    htParam = new Hashtable();
                    htParam.Add("sVillage", "%" + sSearch);
                    htParam.Add("sDistrict", strDist);
                    dsVillage = new DataSet();
                    dsVillage = objInvoiceData.GetVillageDataSet(htParam);
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


            for (int i = 0; i < dt.Rows.Count; i++)
                dataTable.Rows.Add(new String[] { dt.Rows[i]["CDState"] + 
                     "", dt.Rows[i]["PANCHAYAT"] + 
                     "", dt.Rows[i]["MANDAL"] + 
                     "", dt.Rows[i]["DISTRICT"] + 
                     "", dt.Rows[i]["STATE"] + "", dt.Rows[i]["PIN"] + ""});

           
                cbVill.DataBindings.Clear();
                cbVill.DataSource = dataTable;
                cbVill.DisplayMember = "Panchayath";
                cbVill.ValueMember = "StateID";
           
        }

        private bool FindInputAddressSearch()
        {
            bool blFind = false;
            try
            {
                    for (int i = 0; i < this.cbVill.Items.Count; i++)
                    {
                        string strItem = ((System.Data.DataRowView)(this.cbVill.Items[i])).Row.ItemArray[1] + "".ToString().Trim();
                        if (strItem.IndexOf(txtVillSearch.Text) > -1)
                        {
                            blFind = true;
                            cbVill.SelectedIndex = i;
                            txtVill.Text = ((System.Data.DataRowView)(this.cbVill.Items[i])).Row.ItemArray[1] + "";
                            txtMandal.Text = ((System.Data.DataRowView)(this.cbVill.Items[i])).Row.ItemArray[2] + "";
                            txtDistricit.Text = ((System.Data.DataRowView)(this.cbVill.Items[i])).Row.ItemArray[3] + "";
                            txtState.Text = ((System.Data.DataRowView)(this.cbVill.Items[i])).Row.ItemArray[4] + "";
                            txtPin.Text = ((System.Data.DataRowView)(this.cbVill.Items[i])).Row.ItemArray[5] + "";
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

        private void ClearVillageDetails()
        {
            txtVill.Text = "";
            txtMandal.Text = "";
            txtDistricit.Text = "";
            txtState.Text = "";
            txtPin.Text = "";
        }

        private void btnVSearch_Click(object sender, EventArgs e)
        {
            VillageSearch VSearch = new VillageSearch("ComplainantMaster");
            VSearch.objComplainantMaster = this;
            VSearch.ShowDialog();
        }

        private void cbVill_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbVill.SelectedIndex > -1)
            {
                if (this.cbVill.Items[cbVill.SelectedIndex].ToString() != "")
                {
                    txtVill.Text = ((System.Data.DataRowView)(this.cbVill.Items[cbVill.SelectedIndex])).Row.ItemArray[1] + "";
                    txtMandal.Text = ((System.Data.DataRowView)(this.cbVill.Items[cbVill.SelectedIndex])).Row.ItemArray[2] + "";
                    txtDistricit.Text = ((System.Data.DataRowView)(this.cbVill.Items[cbVill.SelectedIndex])).Row.ItemArray[3] + "";
                    txtState.Text = ((System.Data.DataRowView)(this.cbVill.Items[cbVill.SelectedIndex])).Row.ItemArray[4] + "";
                    txtPin.Text = ((System.Data.DataRowView)(this.cbVill.Items[cbVill.SelectedIndex])).Row.ItemArray[5] + "";
                }
            }
        }

        private void txtName_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtName.Text.Length > 0)
            {

                objMstr = new Master();
                DataSet ds = new DataSet();
                try
                {
                    ds = objMstr.GetComplainantMasterDetl(txtName.Text.Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objMstr = null;
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    flagUpdate = true;
                    txtComplainantId.Text = ds.Tables[0].Rows[0]["ComplainantId"].ToString();
                    txtHNo.Text = ds.Tables[0].Rows[0]["LCM_ADD_PRES_ADDR_HNO"].ToString();
                    txtLandMark.Text = ds.Tables[0].Rows[0]["LCM_ADD_PRES_ADDR_LANDMARK"].ToString();
                    txtVill.Text = ds.Tables[0].Rows[0]["LCM_ADD_PRES_ADDR_VILL_OR_TOWN"].ToString();
                    txtMandal.Text = ds.Tables[0].Rows[0]["LCM_ADD_PRES_ADDR_MANDAL"].ToString();
                    txtDistricit.Text = ds.Tables[0].Rows[0]["LCM_ADD_PRES_ADDR_DISTRICT"].ToString();
                    txtState.Text = ds.Tables[0].Rows[0]["LCM_ADD_PRES_ADDR_STATE"].ToString();
                    txtPin.Text = ds.Tables[0].Rows[0]["LCM_ADD_PRES_ADDR_PIN"].ToString();
                    txtPhone.Text = ds.Tables[0].Rows[0]["LCM_ADD_PRES_ADDR_PHONE"].ToString();
                    
                }
                else
                {
                    flagUpdate = false;
                    GenerateNewComplainantMaster();
                    //btnClear_Click(null,null);
                    txtHNo.Text = "";
                    txtLandMark.Text = "";
                    txtVill.Text = "";
                    txtMandal.Text = "";
                    txtDistricit.Text = "";
                    txtState.Text = "";
                    txtPin.Text = "";
                    txtPhone.Text = "";
                    txtDesig.Text = "";
                    txtVillSearch.Text = "";
                }
                ds = null;

            }
            else
            {
                flagUpdate = false;
                GenerateNewComplainantMaster();
                //btnClear_Click(null,null);
                txtHNo.Text = "";
                txtLandMark.Text = "";
                txtVill.Text = "";
                txtMandal.Text = "";
                txtDistricit.Text = "";
                txtState.Text = "";
                txtPin.Text = "";
                txtPhone.Text = "";
                txtDesig.Text = "";
                txtVillSearch.Text = "";
            }
        }       
    }
}
