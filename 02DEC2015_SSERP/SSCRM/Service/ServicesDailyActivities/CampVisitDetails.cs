using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSTrans;
using SSAdmin;

namespace SSCRM
{
    public partial class CampVisitDetails : Form
    {
        SQLDB objSQLdb = null;
        public ServiceActivities objServiceActivities;
        DataRow[] drs;


        public CampVisitDetails()
        {
            InitializeComponent();
        }
        public CampVisitDetails(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }
       

        private void CampVisitDetails_Load(object sender, EventArgs e)
        {
            FillCampsData();

            if (drs != null)
            {
                txtPurpose.Text = drs[0]["Purpose"].ToString();
                txtRemarks.Text = drs[0]["Remarks"].ToString();
                cbCamps.Text = drs[0]["BranchName"].ToString();

            }
        }

        private void FillCampsData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT DISTINCT(CM_CAMP_NAME) FROM CAMP_MAS ORDER BY CM_CAMP_NAME";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();

                    row[0] = "--Select--";
                    //row[1] = "--Select--";
                   

                    dt.Rows.InsertAt(row, 0);

                    cbCamps.DataSource = dt;
                    cbCamps.DisplayMember = "CM_CAMP_NAME";
                    cbCamps.ValueMember = "CM_CAMP_NAME";
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

        private bool CheckData()
        {
            bool flag = true;

            if (txtPurpose.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Visit Purpose", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPurpose.Focus();
            }
            else if (cbCamps.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Camp Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCamps.Focus();
            }

            //else if (txtRemarks.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter Remarks", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtRemarks.Focus();
            //}

            return flag;

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                if (drs != null)
                {
                    ((ServiceActivities)objServiceActivities).dtActivityDetails.Rows.Remove(drs[0]);
                }


                ((ServiceActivities)objServiceActivities).dtActivityDetails.Rows.Add(new Object[] { "-1","CAMP VISITS",cbCamps.SelectedValue.ToString(),
                                cbCamps.Text.ToString(), txtPurpose.Text.ToString().Replace("\'",""),"","","","","","","CAMP VISITS", 
                                "Camp Name"+'-'+cbCamps.Text.ToString(), txtRemarks.Text.ToString().Replace("\'",""),0,0,"" });
                ((ServiceActivities)objServiceActivities).GetActivityDetails();


                this.Close();
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtPurpose.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            cbCamps.SelectedIndex = 0;
        }

    }
}
