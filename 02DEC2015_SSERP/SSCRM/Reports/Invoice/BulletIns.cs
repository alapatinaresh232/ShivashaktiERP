using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSCRM.App_Code;

namespace SSCRM
{
    public partial class BulletIns : Form
    {
        ReportViewer childReportViewer;
        SQLDB objSQLDB;
        public BulletIns()
        {
            InitializeComponent();
        }

        private void BulletIns_Load(object sender, EventArgs e)
        {
            GetPopupdropDown();
            cmbBranchtype.SelectedIndex = 0;
            cmbSumary.SelectedIndex = 0;
            rbtnBranch.Checked = true;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            childReportViewer = new ReportViewer();
            CommonData.ViewReport = "SSCRM_REP_DOCMM_GROUPSX";
            childReportViewer.Show();
        }

        public void GetPopupdropDown()
        {
            objSQLDB = new SQLDB();
            string strSQLCpy = "SELECT CM_COMPANY_NAME,CM_COMPANY_CODE FROM COMPANY_MAS";
            DataTable dtCpy = objSQLDB.ExecuteDataSet(strSQLCpy, CommandType.Text).Tables[0];
            UtilityLibrary.PopulateControl(cmbCompany, dtCpy.DefaultView, 0, 1, "--PLEASE SELECT--", 0);
            objSQLDB = null;
        }

        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCompany.SelectedIndex > 0)
            {
                objSQLDB = new SQLDB();
                DataTable dtBranch = objSQLDB.ExecuteDataSet("SELECT BRANCH_CODE,BRANCH_NAME FROM BRANCH_MAS WHERE COMPANY_CODE='" + cmbCompany.SelectedValue + "'").Tables[0];
                UtilityLibrary.PopulateControl(cmbBranch, dtBranch.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
                objSQLDB = null;
            }
        }

        private void rbtnBranch_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnBranch.Checked == true)
            {
                rbtnGroups.Checked = false;
                groupBox2.Enabled = true;
                groupBox4.Enabled = false;
            }
            else
            {
                rbtnGroups.Checked = true;
                groupBox2.Enabled = false;
                groupBox4.Enabled = true;
            }
        }

        private void rbtnGroups_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnGroups.Checked == true)
            {
                rbtnBranch.Checked = false;
                groupBox2.Enabled = false;
                groupBox4.Enabled = true;
            }
            else
            {
                groupBox2.Enabled = true;
                groupBox4.Enabled = false;
            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            if ((txtFrom.Text != "") && (txtTo.Text != ""))
            {
                if ((UtilityFunctions.IsNumeric(txtFrom.Text) == false) || (UtilityFunctions.IsNumeric(txtTo.Text) == false))
                {
                    MessageBox.Show("Please enter valid numbers", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (Convert.ToInt32(txtFrom.Text) > Convert.ToInt32(txtTo.Text))
                {
                    MessageBox.Show("To No. must be greater than from No.", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            GetLevels();
        }
        public void GetLevels()
        {
            chkLevles.Items.Clear();
            objSQLDB = new SQLDB();
            string SqlQry = "";
            if ((txtFrom.Text == "") || (txtTo.Text == ""))
                SqlQry = "SELECT LDM_DESIGNATIONS FROM LEVELSDESIG_MAS WHERE LDM_COMPANY_CODE='" + cmbCompany.SelectedValue + "' ORDER BY LDM_DESIGNATIONS";
            else
                SqlQry = "SELECT LDM_DESIGNATIONS FROM LEVELSDESIG_MAS WHERE LDM_COMPANY_CODE='" + cmbCompany.SelectedValue + "' AND LDM_ELEVEL_ID BETWEEN " + txtFrom.Text + " AND " + txtTo.Text + " ORDER BY LDM_DESIGNATIONS";
            DataTable dtLevelDesig = objSQLDB.ExecuteDataSet(SqlQry).Tables[0];
            foreach (DataRow dr in dtLevelDesig.Rows)
            {
                chkLevles.Items.Add(dr[0].ToString());
            }
            objSQLDB = null;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
