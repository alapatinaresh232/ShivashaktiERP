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
using SSTrans;
namespace SSCRM
{
    public partial class SRReconciliation : Form
    {
        SQLDB objSQLDB = null;
        HRInfo objHRInfo = null;
        Security objSecurity = null;
        string strFrmType = "";
        public SRReconciliation()
        {
            InitializeComponent();
        }

        public SRReconciliation(string sFormType)
        {
            InitializeComponent();
            strFrmType = sFormType;
        }

        private void SRReconciliation_Load(object sender, EventArgs e)
        {
            if (strFrmType == "SERVICE_ORG_CHART")
            {
                this.Text = "service Organization Chart";
            }
        }



        private void btnReport_Click(object sender, EventArgs e)
        {
            if (strFrmType == "")
            {
                if (!SSCRM.App_Code.UtilityLibrary.CustomValidate(groupBox1, toolTip1))
                    return;
                string ReqECode = "";
                if (txtECode.Text.Contains('-'))
                    ReqECode = txtECode.Text.Split('-')[1];
                else
                    ReqECode = txtECode.Text;
                CommonData.ViewReport = "SSCRM_REP_SALES_SR_DOCMM_ACCOUNTABILITY";
                ReportViewer frmReport = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.CompanyCode.ToString(), dtpDate.Value.ToString("MMMyyyy"), Convert.ToInt32(ReqECode));
                frmReport.Show();
            }
            if (strFrmType == "SERVICE_ORG_CHART")
            {
                if (txtECode.Text.Length > 4)
                {
                    CommonData.ViewReport = "SSCRM_REP_SERVICE_ORG_CHART";
                    ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, "800000", dtpDate.Value.ToString("MMMyyyy").ToUpper(),Convert.ToInt32(txtECode.Text),"DETAILED");
                    objReportview.Show();
                }
                else
                {
                    MessageBox.Show("Please Enter Ecode","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }

            }
        }

        //public void GetPopupdropDown()
        //{           
        //    objSecurity = new Security();
        //    DataTable dtCpy = objSecurity.GetCompanyDataSet().Tables[0];
        //    UtilityLibrary.PopulateControl(cmbCompany, dtCpy.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
        //    objSecurity = null;

        //}
       
        public void TextAutoMembers()
        {
            objHRInfo = new HRInfo();
            DataTable dt = objHRInfo.GetNameandEcode().Tables[0];
            objHRInfo = null;

            AutoCompleteStringCollection local = new AutoCompleteStringCollection();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                local.Add(dt.Rows[i]["Data"].ToString());
            }
            txtECode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtECode.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtECode.AutoCompleteCustomSource = local;
            objSQLDB = null;
        }

        //private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cmbCompany.SelectedIndex > 0)
        //    {
        //        objHRInfo = new HRInfo();
        //        DataTable dtBranch = objHRInfo.GetAllBranchList(cmbCompany.SelectedValue.ToString(), "", "").Tables[0];
        //        UtilityLibrary.PopulateControl(cmbBranch, dtBranch.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
        //        objHRInfo = null;
        //    }
        //}

        //private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cmbBranch.SelectedIndex > 0)
        //        TextAutoMembers();
        //}

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtECode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
    }
}
