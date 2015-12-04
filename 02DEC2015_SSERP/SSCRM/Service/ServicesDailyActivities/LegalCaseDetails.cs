using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSAdmin;
using SSCRM.App_Code;

namespace SSCRM
{
    public partial class LegalCaseDetails : Form
    {
        SQLDB objSQLdb = null;
        public  ServiceActivities objServiceActivities = null;             
        
        DataRow[] drs;

        public LegalCaseDetails()
        {
            InitializeComponent();
        }
        public LegalCaseDetails(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }
              

        private void LegalCaseDetails_Load(object sender, EventArgs e)
        {
            cbCaseType.SelectedIndex = 0;
            FillLegalCaseNumbersData();
            if (drs != null)
            {              
                cbCaseType.Text = drs[0]["LegalCaseType"].ToString();
                cbCaseNo.Text = drs[0]["LegalCaseNo"].ToString();
                txtRemarks.Text = drs[0]["Remarks"].ToString();
            }

        }

        private void FillLegalCaseNumbersData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT DISTINCT(LCH_CASE_NUMBER) LegalCaseNo FROM LEGAL_CASE_HEARINGS ORDER BY LCH_CASE_NUMBER";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                   // UtilityLibrary.AutoCompleteComboBox(cbCaseNo, dt, "", "LegalCaseNo");
                    DataRow row = dt.NewRow();

                    row[0] = "--Select--";
                    //row[1] = "--Select--";


                    dt.Rows.InsertAt(row, 0);

                    cbCaseNo.DataSource = dt;
                    cbCaseNo.DisplayMember = "LegalCaseNo";
                    cbCaseNo.ValueMember = "LegalCaseNo";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
                dt = null;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }


        private bool CheckData()
        {
            bool flag = true;
            if (cbCaseNo.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Case Number", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCaseNo.Focus();
                return flag;
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
                    ((ServiceActivities)objServiceActivities).dtActivityDetails.Rows.Remove(drs[0]);
                }


                ((ServiceActivities)objServiceActivities).dtActivityDetails.Rows.Add(new Object[] { "-1","LEGAL","","","","","","","", cbCaseType.Text.ToString(),
                    cbCaseNo.Text.ToString(),"LEGAL CASE DETAILS", 
                               cbCaseType.Text.ToString()+'-'+cbCaseNo.Text.ToString(), txtRemarks.Text.ToString().Replace("\'",""),0,0,""});
                ((ServiceActivities)objServiceActivities).GetActivityDetails();

                this.Close();
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbCaseType.SelectedIndex = 0;
            cbCaseNo.SelectedIndex = 0;
            txtRemarks.Text = "";

        }

     
       

       
    }
}
