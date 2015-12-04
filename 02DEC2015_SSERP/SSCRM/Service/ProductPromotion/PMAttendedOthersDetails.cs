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
    public partial class PMAttendedOthersDetails : Form
    {
        public frmProductPromotion objfrmProductPromotion;
        ServiceDeptDB objServicedb = null;

        DataRow[] drs;

        public PMAttendedOthersDetails()
        {
            InitializeComponent();
        }

        public PMAttendedOthersDetails(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }

        
        private void PMAttendedOthersDetails_Load(object sender, EventArgs e)
        {
            cbRelation.SelectedIndex = 0;

            if (drs != null)
            {

                txtName.Text = drs[0]["OthersName"].ToString();
                txtDesig.Text=drs[0]["OthersDesig"].ToString();
                txtRelationName.Text = drs[0]["RelName"].ToString();
                cbRelation.Text = drs[0]["OthersRel"].ToString();
                txtVillage.Text = drs[0]["Village"].ToString();
                txtMandal.Text = drs[0]["Mandal"].ToString();
                txtDistrict.Text = drs[0]["District"].ToString();
                txtState.Text = drs[0]["State"].ToString();
                txtPin.Text = drs[0]["Pin"].ToString();
                txtMobileNo.Text = drs[0]["MobileNo"].ToString();
                txtHouseNo.Text = drs[0]["HouseNo"].ToString();
                txtLandMark.Text = drs[0]["LandMark"].ToString();
                txtRemarks.Text = drs[0]["Remarks"].ToString();
            }
        }

        private bool CheckData()
        {
            bool flag = true;
            if (txtName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
            }
            //else if (txtRelationName.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter  Relation Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtRelationName.Focus();
            //}
            else if (txtVillage.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Village Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtVillSearch.Focus();
            }
            //else if (txtRemarks.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter Remarks", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtRemarks.Focus();
            //}

            return flag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (CheckData() == true)
            //{
            //    if (drs != null)
            //    {
            //        ((frmProductPromotion)objfrmProductPromotion).dtOthersDetails.Rows.Remove(drs[0]);
            //    }

            //    ((frmProductPromotion)objfrmProductPromotion).dtOthersDetails.Rows.Add(new object[] {"-1",txtName.Text.ToString().Replace("'"," "),txtDesig.Text.ToString().Replace("'"," "),
            //        cbRelation.Text.ToString(),txtRelationName.Text.ToString().Replace("'"," "),txtHouseNo.Text.ToString().Replace("'"," "),txtLandMark.Text.ToString().Replace("'"," "),
            //        txtVillage.Text.ToString(),txtMandal.Text.ToString(),txtDistrict.Text.ToString(),txtState.Text.ToString(),
            //        txtPin.Text.ToString(),txtMobileNo.Text.ToString(),txtRemarks.Text.ToString().Replace("'"," ") });

            //    ((frmProductPromotion)objfrmProductPromotion).GetOthersDetails();

            //    this.Close();
            //}

        }

        private void btnVillageSearch_Click(object sender, EventArgs e)
        {
            VillageSearch VillSearch = new VillageSearch("PMAttendedOthersDetails");
            VillSearch.objPMAttendedOthersDetails = this;
            VillSearch.ShowDialog();
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
                    txtVillage.Text = "";
                    txtState.Text = "";
                    txtDistrict.Text = "";
                    txtMandal.Text = "";
                    txtPin.Text = "";

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

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtDesig.Text = "";
            txtRelationName.Text = "";
            txtVillage.Text = "";
            txtMandal.Text = "";
            txtState.Text = "";
            txtPin.Text = "";
            txtMobileNo.Text = "";
            txtRemarks.Text = "";
            cbRelation.SelectedIndex = 0;
            cbVillage.DataSource = null;
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);

            if (e.KeyChar != '\b' && !(char.IsWhiteSpace(e.KeyChar)))
            {
                if (!char.IsLetter(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtDesig_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);

            if (e.KeyChar != '\b' && !(char.IsWhiteSpace(e.KeyChar)))
            {
                if (!char.IsLetter(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtRelationName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);

            if (e.KeyChar != '\b' && !(char.IsWhiteSpace(e.KeyChar)))
            {
                if (!char.IsLetter(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtLandMark_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);

        }

        private void txtPin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

        }

        private void txtMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && e.KeyChar != ',')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox24_TextChanged(object sender, EventArgs e)
        {

        }

      

    }
}
