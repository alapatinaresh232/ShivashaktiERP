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

namespace SSCRM
{
    public partial class frmAttendedFarmerDetails : Form
    {
        public frmWrongCommitmentOrFinancialFrauds objWCORFF;
        SQLDB objSQLDB = null;
        ServiceDeptDB objServicedb = null;

        string sVillage = "";
        string sMandal = "";
        string sDistrict = "";
        string sState = "";

        DataRow[] drs;

        public frmAttendedFarmerDetails(string sVill,string stMandal,string stDist,string stState)
        {
            InitializeComponent();
            sVillage = sVill;
            sMandal = stMandal;
            sDistrict = stDist;
            sState = stState;
        }
        public frmAttendedFarmerDetails(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }

        private void frmAttendedFarmerDetails_Load(object sender, EventArgs e)
        {
            txtVillage.Text = sVillage;
            txtMandal.Text = sMandal;
            txtDistrict.Text = sDistrict;
            txtState.Text = sState;

            if (drs != null)
            {
                txtFarmerName.Text = drs[0]["FarmerName"].ToString();
                txtHouseNo.Text = drs[0]["HouseNo"].ToString();
                txtLandMark.Text = drs[0]["LandMark"].ToString();
                txtVillage.Text = drs[0]["VillageName"].ToString();
                txtMandal.Text = drs[0]["Mandal"].ToString();
                txtDistrict.Text = drs[0]["District"].ToString();
                txtState.Text = drs[0]["State"].ToString();
                txtPin.Text = drs[0]["Pin"].ToString();
                txtMobileNo.Text = drs[0]["MobileNo"].ToString();
                txtRemarks.Text = drs[0]["Remarks"].ToString();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private bool CheckData()
        {
            bool bFlag = true;
            if (txtFarmerName.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Enter Farmer Name", "Farmer Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFarmerName.Focus();

            }
            //else if (txtHouseNo.Text.Length == 0)
            //{
            //    bFlag = false;
            //    MessageBox.Show("Please Enter HouseNo", "Farmer Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtHouseNo.Focus();

            //}
           
            else if (txtVillage.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Add Village Name", "Farmer Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtVillSearch.Focus();

            }
            //else if (txtMobileNo.Text.Length == 0)
            //{
            //    bFlag = false;
            //    MessageBox.Show("Please Enter Mobile Number", "Farmer Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtMobileNo.Focus();
            //}
            ////else if (txtRemarks.Text.Length == 0)
            ////{
            ////    bFlag = false;
            ////    MessageBox.Show("Please Enter Crop Remarks", "Farmer Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////    txtRemarks.Focus();
            ////}
            return bFlag;


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {
                if (drs != null)
                {
                    ((frmWrongCommitmentOrFinancialFrauds)objWCORFF).dtFarmerDetails.Rows.Remove(drs[0]);
                }

                ((frmWrongCommitmentOrFinancialFrauds)objWCORFF).dtFarmerDetails.Rows.Add(new Object[] { "-1", txtFarmerName.Text, txtHouseNo.Text, txtLandMark.Text, txtVillage.Text, txtMandal.Text, txtDistrict.Text,
                    txtState.Text, txtPin.Text, txtMobileNo.Text, txtRemarks.Text.Replace("\'", "") });
                ((frmWrongCommitmentOrFinancialFrauds)objWCORFF).GetFarmerDetails();

                this.Close();
            }
        }

        private void btnVillageSearch_Click(object sender, EventArgs e)
        {
            VillageSearch vilsearch = new VillageSearch("frmAttendedFarmerDetails");
            vilsearch.objfrmAttendedFarmerDetails = this;
            vilsearch.ShowDialog();
        }

        private void FillAddressData(string svilsearch)
        {
            Hashtable htParam = null;
            objServicedb = new ServiceDeptDB();
            string strDist = string.Empty;
            DataSet dsVillage = null;
            DataTable dtVillage = new DataTable();
            if (txtVillSearch.Text != "")
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    if (svilsearch.Trim().Length >= 0)

                        htParam = new Hashtable();
                    htParam.Add("sVillage", svilsearch);
                    htParam.Add("sDistrict", strDist);



                    htParam.Add("sCDState", CommonData.StateCode);
                    dsVillage = new DataSet();
                    dsVillage = objServicedb.GetVillageDataSet(htParam);
                    dtVillage = dsVillage.Tables[0];
                    if (dtVillage.Rows.Count == 1)
                    {
                        txtVillage.Text = dtVillage.Rows[0]["PANCHAYAT"].ToString();
                        txtMandal.Text = dtVillage.Rows[0]["Mandal"].ToString();
                        txtDistrict.Text = dtVillage.Rows[0]["District"].ToString();
                        txtState.Text = dtVillage.Rows[0]["State"].ToString();
                        txtPin.Text = dtVillage.Rows[0]["PIN"].ToString();

                    }
                    else if (dtVillage.Rows.Count > 1)
                    {
                        txtVillage.Text = "";
                        txtMandal.Text = "";
                        txtDistrict.Text = "";
                        txtState.Text = "";
                        txtPin.Text = "";

                        FillAddressComboBox(dtVillage);
                    }

                    else
                    {
                        htParam = new Hashtable();
                        htParam.Add("sVillage", "%" + svilsearch);
                        htParam.Add("sDistrict", strDist);
                        dsVillage = new DataSet();
                        dsVillage = objServicedb.GetVillageDataSet(htParam);
                        dtVillage = dsVillage.Tables[0];
                        FillAddressComboBox(dtVillage);


                    }


                    Cursor.Current = Cursors.Default;
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    objServicedb = null;
                    Cursor.Current = Cursors.Default;

                }
            }
            else
            {

                txtVillage.Text = "";
                txtMandal.Text = "";
                txtDistrict.Text = "";
                txtState.Text = "";
                txtPin.Text = "";
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

            cbVillage.DataBindings.Clear();
            cbVillage.DataSource = dataTable;
            cbVillage.DisplayMember = "Panchayath";
            cbVillage.ValueMember = "StateID";

        }

        private void txtVillSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtVillSearch.Text.Length == 0)
                {
                    cbVillage.DataSource = null;
                    cbVillage.DataBindings.Clear();
                    cbVillage.Items.Clear();
                    //txtVillage.Text = "";
                    //txtState.Text = "";
                    //txtDistrict.Text = "";
                    //txtMandal.Text = "";
                    //txtPin.Text = "";

                }

                else if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                {

                    FillAddressData(txtVillSearch.Text);


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
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

                }
            }

        }

        void RestrictingDigits(KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsLetter(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        void RestrictingCharacters(KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && e.KeyChar!=',')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }


        private void txtFarmerName_KeyPress(object sender, KeyPressEventArgs e)
        {
             e.KeyChar = Char.ToUpper(e.KeyChar);
            if (e.KeyChar != '\b' && !(char.IsWhiteSpace(e.KeyChar)))
            {
                if (!char.IsLetter(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            //RestrictingDigits(e);
            
        }

        private void txtVillSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
            //RestrictingDigits(e);
        }

        private void txtRemarks_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.KeyChar = char.ToUpper(e.KeyChar);
            //RestrictingDigits(e);
        }

        private void txtMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            RestrictingCharacters(e);
        }

        private void txtPin_KeyPress(object sender, KeyPressEventArgs e)
        {
            RestrictingCharacters(e);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtFarmerName.Text = "";
            txtDistrict.Text = "";           
         
            txtHouseNo.Text = "";
            txtLandMark.Text = "";
            txtMobileNo.Text = "";
            txtPin.Text = "";
            txtState.Text = "";
            txtVillage.Text = "";
            txtVillSearch.Text = "";
            cbVillage.SelectedIndex = -1;
            txtMandal.Text = "";
            txtRemarks.Text = "";

        }

    }
}
