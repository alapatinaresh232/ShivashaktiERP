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

namespace SSCRM
{
    public partial class SVGiftDetails : Form
    {
        SQLDB objSQLdb = null;
        ServiceDeptDB objServicedb = null;
        public frmSchoolVisits objfrmSchoolVisits;
        DataRow[] drs;
        string strVillage;
        string strMandal;
        string strDistrict;
        string strState;
        string strPin;

        public SVGiftDetails()
        {
            InitializeComponent();
        }
        public SVGiftDetails(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }
        public SVGiftDetails(string sVil, string sMandal, string sDidtrict, string sState, string sPin)
        {
            InitializeComponent();
            strVillage = sVil;
            strMandal = sMandal;
            strDistrict = sDidtrict;
            strState = sState;
            strPin = sPin;
        }

        private void SVGiftDetails_Load(object sender, EventArgs e)
        {
            cbRelation.SelectedIndex = 0;

            txtVillage.Text = strVillage;
            txtMandal.Text = strMandal;
            txtDistrict.Text = strDistrict;
            txtState.Text = strState;
            txtPin.Text = strPin;

            if (drs != null)
            {                

                txtStudentName.Text = drs[0]["GiftStudentname"].ToString();
                txtDesig.Text = drs[0]["StDesig"].ToString();
                txtRelationName.Text = drs[0]["GiftStRelName"].ToString();
                cbRelation.Text = drs[0]["GiftStRel"].ToString();
                txtHouseNo.Text = drs[0]["GiftStudentHNo"].ToString();
                txtLandMark.Text = drs[0]["GiftStLandMark"].ToString();
                txtVillage.Text = drs[0]["GiftStVillage"].ToString();
                txtMandal.Text = drs[0]["GiftStMandal"].ToString();
                txtDistrict.Text = drs[0]["GiftStDistrict"].ToString();
                txtState.Text = drs[0]["GiftStState"].ToString();
                txtPin.Text = drs[0]["GiftStPin"].ToString();
                txtMobileNo.Text = drs[0]["GiftStMobileNo"].ToString();
                txtQuizName.Text = drs[0]["Quiz"].ToString(); ;
                txtRank.Text = drs[0]["Rank"].ToString();
                txtGiftName.Text = drs[0]["GiftName"].ToString();
                txtGiftRemarks.Text = drs[0]["GiftRemarks"].ToString();


            }

        }

        private bool CheckData()
        {
            bool flag = true;
            if (txtStudentName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Student Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtStudentName.Focus();
            }
            //else if (txtRelationName.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter Student Relation Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtRelationName.Focus();
            //}
            else if (txtVillage.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Village Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtVillSearch.Focus();
            }
            else if (txtQuizName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Quiz Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtQuizName.Focus();
            }
            else if (txtGiftName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Given Gift Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGiftName.Focus();
            }
            //else if (txtMobileNo.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter MobileNo", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtMobileNo.Focus();
            //}

            //else if (txtGiftRemarks.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter Remarks", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtGiftRemarks.Focus();
            //}

            return flag;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {
                if (drs != null)
                {
                    ((frmSchoolVisits)objfrmSchoolVisits).dtGiftDetails.Rows.Remove(drs[0]);
                }


                ((frmSchoolVisits)objfrmSchoolVisits).dtGiftDetails.Rows.Add(new object[] {"-1",txtStudentName.Text.ToString(),txtDesig.Text.ToString(),
                    cbRelation.Text.ToString(),txtRelationName.Text.ToString(),txtHouseNo.Text.ToString(),txtLandMark.Text.ToString(),
                    txtVillage.Text.ToString(),txtMandal.Text.ToString(),txtDistrict.Text.ToString(),txtState.Text.ToString(),
                    txtPin.Text.ToString(),txtQuizName.Text.ToString(),txtRank.Text,txtGiftName.Text.ToString(),
                    txtMobileNo.Text.ToString(),txtGiftRemarks.Text.ToString() });

                ((frmSchoolVisits)objfrmSchoolVisits).GetGiftDetails();

                this.Close();
            }
        }

        private void btnVillageSearch_Click(object sender, EventArgs e)
        {
            VillageSearch VillSearch = new VillageSearch("SVGiftDetails");
            VillSearch.objSVGiftDetails = this;
            VillSearch.ShowDialog();
        }

        private void txtVillSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtVillSearch.Text.Length == 0)
                {
                    //cbVillage.DataSource = null;
                    //cbVillage.DataBindings.Clear();
                    //cbVillage.Items.Clear();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtRank_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtGiftName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtStudentName_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtQuizName_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtStudentName.Text = "";
            txtRelationName.Text = "";
            cbRelation.SelectedIndex = 0;
            txtDesig.Text = "";
            txtVillage.Text = "";
            txtMandal.Text = "";
            txtVillSearch.Text = "";
            txtDistrict.Text = "";
            txtPin.Text = "";
            txtState.Text = "";
            txtMobileNo.Text = "";
            txtQuizName.Text = "";
            txtRank.Text = "";
            txtGiftName.Text = "";
            txtGiftRemarks.Text = "";
            cbVillage.DataSource = null;
        }

        private void txtDesig_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);

        }

      

       
    }
}
