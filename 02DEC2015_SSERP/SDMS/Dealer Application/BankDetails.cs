using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using SSAdmin;
using SSTrans;
using SSCRMDB;
using SDMS.App_Code;
using System.Windows.Forms;

namespace SDMS
{
    public partial class BankDetails : Form
    {
        public DealarApplicationForm dealerApplication;
        DataRow[] drs;
        InvoiceDB objInvoiceData;
        string strStateCode = string.Empty;
        public BankDetails()
        {
            InitializeComponent();
        }
        public BankDetails(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //UtilityLibrary oUtility = new UtilityLibrary();
            //if (!SSCRM.App_Code.UtilityLibrary.CustomValidate(grouper1, toolTip1))
            //    return;

            //bellow line for delete the row in dtEducation table
            if (drs != null)
                ((DealarApplicationForm)dealerApplication).dtBankerDetails.Rows.Remove(drs[0]);
            if (txtBankPin.Text.Length == 0)
            {
                txtBankPin.Text = 0 + "";
            }
            if (txtPhoneNo.Text.Length == 0)
            {
                txtPhoneNo.Text = 0 + "";
            }
            //till here
            if (cbBankName.SelectedIndex > 0)
            {
                ((DealarApplicationForm)dealerApplication).dtBankerDetails.Rows.Add(new Object[] { "-1", cbBankName.SelectedValue.ToString().ToUpper(), txtBankHNo.Text, txtBankLandMark.Text.ToUpper(), txtBankVill.Text.ToUpper(), txtBankMandal.Text.ToUpper(), txtBankDistricit.Text.ToUpper(), txtBankState.Text.ToUpper(), txtBankPin.Text, txtPhoneNo.Text });
                ((DealarApplicationForm)dealerApplication).GetBankerDetails();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please Enter Details", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BankDetails_Load(object sender, EventArgs e)
        {
            SQLDB objectDB=new SQLDB();
            DataTable dt=new DataTable();
            try
            {
                 dt = objectDB.ExecuteDataSet("SELECT DBM_BANK_NAME FROM DL_BANK_MASTER").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    

                    dt.Rows.InsertAt(dr, 0);
                    cbBankName.DataSource = dt;
                    cbBankName.DisplayMember = "DBM_BANK_NAME";
                    cbBankName.ValueMember = "DBM_BANK_NAME";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objectDB = null;
                dt = null;
            }
            if (drs != null)
            {
                cbBankName.SelectedValue = drs[0][1].ToString();
              
                txtBankHNo.Text = drs[0][2].ToString();
                txtBankLandMark.Text = drs[0][3].ToString();
                txtBankVill.Text = drs[0][4].ToString();
                txtBankVillSearch.Text = drs[0][4].ToString();
                txtBankMandal.Text = drs[0][5].ToString();
                txtBankDistricit.Text = drs[0][6].ToString();
                txtBankState.Text = drs[0][7].ToString();
                txtBankPin.Text = drs[0][8].ToString();
                txtPhoneNo.Text = drs[0][9].ToString();

            }
        }

        private void txtBankVillSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtBankVillSearch.Text.Length == 0)
                {
                    cbBankVill.DataSource = null;
                    cbBankVill.DataBindings.Clear();
                    cbBankVill.Items.Clear();
                    //if (btnSave.Enabled == true)
                    //    ClearVillageDetails("Firm");
                }
                else if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                {
                    if (FindInputAddressSearch() == false)
                    {
                        FillAddressData(txtBankVillSearch.Text);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private bool FindInputAddressSearch()
        {
            bool blFind = false;
            try
            {
                
                    for (int i = 0; i < this.cbBankVill.Items.Count; i++)
                    {
                        string strItem = ((System.Data.DataRowView)(this.cbBankVill.Items[i])).Row.ItemArray[1] + "".ToString().Trim();
                        if (strItem.IndexOf(txtBankVillSearch.Text) > -1)
                        {
                            blFind = true;
                            cbBankVill.SelectedIndex = i;
                            txtBankVill.Text = ((System.Data.DataRowView)(this.cbBankVill.Items[i])).Row.ItemArray[1] + "";
                            txtBankMandal.Text = ((System.Data.DataRowView)(this.cbBankVill.Items[i])).Row.ItemArray[2] + "";
                            txtBankDistricit.Text = ((System.Data.DataRowView)(this.cbBankVill.Items[i])).Row.ItemArray[3] + "";
                            txtBankState.Text = ((System.Data.DataRowView)(this.cbBankVill.Items[i])).Row.ItemArray[4] + "";
                            txtBankPin.Text = ((System.Data.DataRowView)(this.cbBankVill.Items[i])).Row.ItemArray[5] + "";
                            strStateCode = ((System.Data.DataRowView)(this.cbBankVill.Items[i])).Row.ItemArray[0] + "";
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
                    
                        txtBankVill.Text = dtVillage.Rows[0]["PANCHAYAT"].ToString();  // ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[1] + "";
                        txtBankMandal.Text = dtVillage.Rows[0]["Mandal"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[2]+ ""; 
                        txtBankDistricit.Text = dtVillage.Rows[0]["District"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[3] + "";
                        txtBankState.Text = dtVillage.Rows[0]["State"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[4] + "";
                        txtBankPin.Text = dtVillage.Rows[0]["PIN"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[5] + "";
                        strStateCode = dtVillage.Rows[0]["CDState"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[0] + "";
                   
                    

                }
                else if (dtVillage.Rows.Count > 1)
                {
                    
                        txtBankVill.Text = "";
                        txtBankMandal.Text = "";
                        txtBankDistricit.Text = "";
                        txtBankState.Text = "";
                        txtBankPin.Text = "";
                        strStateCode = "";
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
                    txtBankVill.Text = "";
                    txtBankMandal.Text = "";
                    txtBankDistricit.Text = "";
                    txtBankState.Text = "";
                    txtBankPin.Text = "";
                }
                Cursor.Current = Cursors.Default;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objInvoiceData = null;
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

           
                cbBankVill.DataBindings.Clear();
                cbBankVill.DataSource = dataTable;
                cbBankVill.DisplayMember = "Panchayath";
                cbBankVill.ValueMember = "StateID";
           
        }

        private void cbBankVill_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBankVill.SelectedIndex > -1)
            {
                if (this.cbBankVill.Items[cbBankVill.SelectedIndex].ToString() != "")
                {
                    txtBankVill.Text = ((System.Data.DataRowView)(this.cbBankVill.Items[cbBankVill.SelectedIndex])).Row.ItemArray[1] + "";
                    txtBankMandal.Text = ((System.Data.DataRowView)(this.cbBankVill.Items[cbBankVill.SelectedIndex])).Row.ItemArray[2] + "";
                    txtBankDistricit.Text = ((System.Data.DataRowView)(this.cbBankVill.Items[cbBankVill.SelectedIndex])).Row.ItemArray[3] + "";
                    txtBankState.Text = ((System.Data.DataRowView)(this.cbBankVill.Items[cbBankVill.SelectedIndex])).Row.ItemArray[4] + "";
                    txtBankPin.Text = ((System.Data.DataRowView)(this.cbBankVill.Items[cbBankVill.SelectedIndex])).Row.ItemArray[5] + "";
                    strStateCode = ((System.Data.DataRowView)(this.cbBankVill.Items[cbBankVill.SelectedIndex])).Row.ItemArray[0] + "";
                }
            }
        }

        private void txtPhoneNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtBankVillSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtBankPin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }
    }
}
