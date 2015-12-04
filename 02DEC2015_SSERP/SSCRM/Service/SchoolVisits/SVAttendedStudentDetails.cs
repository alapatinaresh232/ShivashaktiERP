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
    public partial class SVAttendedStudentDetails : Form
    {
        public frmSchoolVisits objfrmSchoolVisits;
        ServiceDeptDB objServicedb = null;
        DataRow[] drs;
        string strVillage;
        string strMandal;
        string strDistrict;
        string strState ;
        string strPin ;

        public SVAttendedStudentDetails()
        {
            InitializeComponent();
        }
        public SVAttendedStudentDetails(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }
        public SVAttendedStudentDetails(string sVil,string sMandal,string sDidtrict,string sState,string sPin)
        {
            InitializeComponent();
            strVillage = sVil;
            strMandal = sMandal;
            strDistrict = sDidtrict;
            strState = sState;
            strPin = sPin;
        }

        private void SVAttendedStudentDetails_Load(object sender, EventArgs e)
        {
            
            cbRelation.SelectedIndex = 0;
            txtVillage.Text = strVillage;
            txtMandal.Text = strMandal;
            txtDistrict.Text = strDistrict;
            txtState.Text = strState;
            txtPin.Text = strPin;

            if (drs != null)
            {               

                txtStudentName.Text = drs[0]["StudentName"].ToString();
                txtRelationName.Text = drs[0]["StudentRelName"].ToString();
                cbRelation.Text = drs[0]["StudentRel"].ToString();
                txtVillage.Text = drs[0]["StudentVillage"].ToString();
                txtMandal.Text = drs[0]["StudentMandal"].ToString();
                txtDistrict.Text = drs[0]["StudentDistrict"].ToString();
                txtState.Text = drs[0]["StudentState"].ToString();
                txtPin.Text = drs[0]["StudentPin"].ToString();
                txtMobileNo.Text = drs[0]["StudentMobileNo"].ToString();
                txtHouseNo.Text = drs[0]["StudentHNo"].ToString();
                txtLandMark.Text = drs[0]["StudentLandMark"].ToString();
                txtRemarks.Text = drs[0]["StudentRemarks"].ToString();
            }

        }

        private bool CheckData()
        {
            bool flag = true;
            if (txtStudentName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Student Name","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
                txtStudentName.Focus();
            }
            else if (txtRelationName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Student Relation Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRelationName.Focus();
            }
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
            if (CheckData() == true)
            {
                if (drs != null)
                {
                    ((frmSchoolVisits)objfrmSchoolVisits).dtStudentDetails.Rows.Remove(drs[0]);
                }

                ((frmSchoolVisits)objfrmSchoolVisits).dtStudentDetails.Rows.Add(new object[] {"-1",txtStudentName.Text.ToString(),cbRelation.Text.ToString(),
                 txtRelationName.Text.ToString(),txtHouseNo.Text.ToString(),txtLandMark.Text.ToString(),txtVillage.Text.ToString(),txtMandal.Text.ToString(),
                  txtDistrict.Text.ToString(),txtState.Text.ToString(),txtPin.Text.ToString(),txtMobileNo.Text.ToString(),txtRemarks.Text.ToString() });

                ((frmSchoolVisits)objfrmSchoolVisits).GetStudentDetails();

                this.Close();
            }

        }

        private void btnVillageSearch_Click(object sender, EventArgs e)
        {
            VillageSearch VillSearch = new VillageSearch("SVAttendedStudentDetails");
            VillSearch.objSVAttendedStudentDetails = this;
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
            txtStudentName.Text = "";
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();

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

        private void txtLandMark_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

      
    }
}
