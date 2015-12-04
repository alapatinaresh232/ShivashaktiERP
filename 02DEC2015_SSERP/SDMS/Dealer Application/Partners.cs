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
    public partial class Partners : Form
    {
        public DealarApplicationForm dealerApplication;
        DataRow[] drs;
        public string strStateCode = string.Empty;
        private InvoiceDB objInvoiceData = null;
        public Partners()
        {
            InitializeComponent();
        }
        public Partners(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }
        private void Partners_Load(object sender, EventArgs e)
        {
            if (drs != null)
            {
                txtPartnerName.Text = drs[0][1].ToString();
                txtPartnerAge.Text = drs[0][2].ToString();
                txtHNo.Text = drs[0][3].ToString();
                txtLandMark.Text = drs[0][4].ToString();
                txtVill.Text = drs[0][5].ToString();
                txtVillSearch.Text = drs[0][5].ToString();
                txtMandal.Text = drs[0][6].ToString();
                txtDistricit.Text = drs[0][7].ToString();
                txtState.Text = drs[0][8].ToString();
                txtPin.Text = drs[0][9].ToString();
                txtPhone.Text = drs[0][10].ToString();
            }
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
                ((DealarApplicationForm)dealerApplication).dtPartners.Rows.Remove(drs[0]);
            if (txtPin.Text.Length == 0)
            {
                txtPin.Text = 0+"";
            }
            if (txtPartnerAge.Text.Length == 0)
            {
                txtPartnerAge.Text = 0 + "";
            }
            //till here
            if (txtPartnerName.Text.Length > 0)
            {

                ((DealarApplicationForm)dealerApplication).dtPartners.Rows.Add(new Object[] { "-1", txtPartnerName.Text.ToUpper(), txtPartnerAge.Text, txtHNo.Text, txtLandMark.Text.ToUpper(), txtVill.Text.ToUpper(), txtMandal.Text.ToUpper(), txtDistricit.Text.ToUpper(), txtState.Text.ToUpper(), txtPin.Text, txtPhone.Text });
                ((DealarApplicationForm)dealerApplication).GetPartnerDetails();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please Enter PartnerName", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    //    ClearVillageDetails("Firm");
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
                        strStateCode = ((System.Data.DataRowView)(this.cbVill.Items[i])).Row.ItemArray[0] + "";
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
                   
                        txtVill.Text = dtVillage.Rows[0]["PANCHAYAT"].ToString();  // ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[1] + "";
                        txtMandal.Text = dtVillage.Rows[0]["Mandal"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[2]+ ""; 
                        txtDistricit.Text = dtVillage.Rows[0]["District"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[3] + "";
                        txtState.Text = dtVillage.Rows[0]["State"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[4] + "";
                        txtPin.Text = dtVillage.Rows[0]["PIN"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[5] + "";
                        strStateCode = dtVillage.Rows[0]["CDState"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[0] + "";
                   
                    

                }
                else if (dtVillage.Rows.Count > 1)
                {
                   
                        txtVill.Text = "";
                        txtMandal.Text = "";
                        txtDistricit.Text = "";
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

                cbVill.DataBindings.Clear();
                cbVill.DataSource = dataTable;
                cbVill.DisplayMember = "Panchayath";
                cbVill.ValueMember = "StateID";
           
        }
        private void ClearVillageDetails()
        {
            txtVill.Text = "";
            txtMandal.Text = "";
            txtDistricit.Text = "";
            txtState.Text = "";
            txtPin.Text = "";
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
                    strStateCode = ((System.Data.DataRowView)(this.cbVill.Items[cbVill.SelectedIndex])).Row.ItemArray[0] + "";
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

        

        private void txtPartnerAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e); 
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e); 
        }

        private void txtPartnerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtVillSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        
    }
}
