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
    public partial class frmBulletIns : Form
    {
        int iFormType = 0;
        ReportViewer childReportViewer;
        public frmBulletIns()
        {
            InitializeComponent();
        }
        public frmBulletIns(int iForm)
        {
            iFormType = iForm;
            InitializeComponent();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (iFormType == 1)
            {
                if (rbtnSummary.Checked == true)
                {
                    childReportViewer = new ReportViewer("SSBVERBAL", CommonData.BranchCode, dtpDate.Value.ToString("MMMyyyy"), "Summary");
                    CommonData.ViewReport = "SSCRM_REP_DOCMM_GROUPSX";
                    childReportViewer.Show();
                }
                else if (rbtnDetailed.Checked == true)
                {
                    childReportViewer = new ReportViewer("SSBVERBAL", CommonData.BranchCode, dtpDate.Value.ToString("MMMyyyy"), "Detailed");
                    CommonData.ViewReport = "SSCRM_REP_DOCMM_GROUPSX";
                    childReportViewer.Show();
                }
                else
                    MessageBox.Show("Please select one radio button", "SSCRM Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (rbtnSummary.Checked == true)
                {
                    childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, dtpDate.Value.ToString("MMMyyyy"), "Summary");
                    CommonData.ViewReport = "SSCRM_REP_DOCMM_GROUPSX";
                    childReportViewer.Show();
                }
                else if (rbtnDetailed.Checked == true)
                {
                    childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, dtpDate.Value.ToString("MMMyyyy"), "Detailed");
                    CommonData.ViewReport = "SSCRM_REP_DOCMM_GROUPSX";
                    childReportViewer.Show();
                }
                else
                    MessageBox.Show("Please select one radio button", "SSCRM Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbtnSummary_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnSummary.Checked == true)
                rbtnDetailed.Checked = false;
        }

        private void rbtnDetailed_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnDetailed.Checked == true)
                rbtnSummary.Checked = false;
        }

        private void frmBulletIns_Load(object sender, EventArgs e)
        {
            dtpDate.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
            rbtnDetailed.Checked = true;
        }
    }
}
