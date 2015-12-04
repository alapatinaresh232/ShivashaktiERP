using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;

namespace SSCRM
{
    public partial class LicenceRelatedDetails : Form
    {
        SQLDB objSQLdb = null;
       public ServiceActivities objServiceActivities = null;
       DataRow[] drs;
       string strActivityType = "";

        public LicenceRelatedDetails(string sActivityType)
        {
            InitializeComponent();
            strActivityType = sActivityType;
        }
        public LicenceRelatedDetails(DataRow[] dr,string sActType)
        {
            InitializeComponent();
            drs = dr;
            strActivityType = sActType;
        }

        private void LicenceRelatedDetails_Load(object sender, EventArgs e)
        {
            cbLicenceType.SelectedIndex = 0;
            if (drs != null)
            {               
                cbLicenceType.Text = drs[0]["LicenceType"].ToString();
                txtLicenceNo.Text = drs[0]["LicenceNo"].ToString();
                txtRemarks.Text = drs[0]["Remarks"].ToString();
            }
        }

        //private void FillLicenceNumbersData()
        //{
        //    objSQLdb = new SQLDB();
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        string strCmd = "SELECT DISTINCT plm_licence_number+' ('+CASE WHEN plm_wholesale_flag='Y' AND plm_retail_flag='N' THEN 'WholeSale' "+
        //                        " WHEN plm_retail_flag='Y' AND plm_wholesale_flag='N' THEN 'Retail' "+
        //                        " WHEN plm_wholesale_flag='Y' AND plm_retail_flag='y' THEN 'WholeSale'+' And '+'Retail' "+
        //                        " WHEN plm_wholesale_flag='N' AND plm_retail_flag='N' THEN 'No'  END+')' DisMember,plm_licence_number ValueMember  " +
        //                        " FROM pr_licence_master ";
        //        dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
        //        if (dt.Rows.Count > 0)
        //        {
        //            DataRow row = dt.NewRow();

        //            row[0] = "--Select--";
                  
        //            dt.Rows.InsertAt(row, 0);

        //            cbLicenceNo.DataSource = dt;
        //            cbLicenceNo.DisplayMember = "DisMember";
        //            cbLicenceNo.ValueMember = "ValueMember";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        objSQLdb = null;
        //        dt = null;
        //    }
        //}

        private bool CheckData()
        {
            bool flag = true;
            if (strActivityType == "LICENCE")
            {
                if (cbLicenceType.Text == "RENEWAL")
                {
                    if (txtLicenceNo.Text.Length == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Enter Licence Number", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtLicenceNo.Focus();
                        return flag;
                    }
                }
            }
            else
            {
                if (txtLicenceNo.Text.Length == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Enter Licence Number", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtLicenceNo.Focus();
                    return flag;
                }
            }
            if (txtRemarks.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Remarks", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRemarks.Focus();
                return flag;
            }
            
            return flag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string sLicenceNo = "";

            if (CheckData() == true)
            {
                if (drs != null)
                {
                    ((ServiceActivities)objServiceActivities).dtActivityDetails.Rows.Remove(drs[0]);
                }
                if (cbLicenceType.Text == "NEW" && txtLicenceNo.Text.Length == 0)
                {
                    sLicenceNo = txtRemarks.Text.ToString().Replace("'", " ");
                }
                else
                {
                    sLicenceNo = txtLicenceNo.Text.ToString().Replace("'"," ");
                }

                if (strActivityType == "STOCK REPORT SUBMISSION")
                {
                    ((ServiceActivities)objServiceActivities).dtActivityDetails.Rows.Add(new Object[] {"-1","STOCK REPORT SUBMISSION","","","",cbLicenceType.Text.ToString(),
                    txtLicenceNo.Text.ToString().Replace("'",""),"","","","","STOCK REPORT SUBMISSION", 
                               "Stock Report Submission Of "+'-'+txtLicenceNo.Text.ToString().Replace("'",""), txtRemarks.Text.ToString().Replace("'",""),0,0,""});
                    ((ServiceActivities)objServiceActivities).GetActivityDetails();
                }
                if (strActivityType == "LICENCE")
                {
                    ((ServiceActivities)objServiceActivities).dtActivityDetails.Rows.Add(new Object[] {"-1","LICENCE","","","",cbLicenceType.Text.ToString(),
                    txtLicenceNo.Text.ToString().Replace("'",""),"","","","","LICENCE RELATED", 
                               cbLicenceType.Text.ToString()+'-'+sLicenceNo, txtRemarks.Text.ToString().Replace("'",""),0,0,""});
                    ((ServiceActivities)objServiceActivities).GetActivityDetails();
                }


                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtLicenceNo.Text = "";
            txtRemarks.Text = "";
            cbLicenceType.SelectedIndex = 0;

        }

        private void txtLicenceNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

     
    }
}
