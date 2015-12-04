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

namespace SDMS
{
    public partial class SaleRegister : Form
    {
        private Master objData = null;
        ReportViewer childReportViewer = null;
        public SaleRegister()
        {
            InitializeComponent();
        }
        private void SaleRegister_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.X + 170, Screen.PrimaryScreen.WorkingArea.Y + 160);
            this.StartPosition = FormStartPosition.CenterScreen;
            dtpInvoiceFromDate.Value = DateTime.Today;
            dtpInvoiceToDate.Value =dtpInvoiceFromDate.Value.AddDays(1);
            FillBranchData();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        private bool CheckData()
        {
            bool flag = true;
            if(cbBranches.SelectedIndex<=0)
            {
                MessageBox.Show("Select SP/PU");
                flag = false;
            }
            else if (rbtnDetailed.Checked == false && rbtnSummary.Checked == false)
            {
                MessageBox.Show("Select Detailed/Summary");
                flag = false;
            }
            return flag;
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                if (rbtnDetailed.Checked == true)
                {
                    CommonData.ViewReport = "SATL_REP_SALES_REG_DETL";
                    childReportViewer = new ReportViewer(CommonData.CompanyCode, cbBranches.SelectedValue.ToString(), "APR2013", dtpInvoiceFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpInvoiceToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), "DETAILED");
                    childReportViewer.Show();
                    
                }
                if (rbtnSummary.Checked == true)
                {
                    CommonData.ViewReport = "SATL_SALREG_SUMM_REP";
                    childReportViewer = new ReportViewer(CommonData.CompanyCode, cbBranches.SelectedValue.ToString(), "APR2013", dtpInvoiceFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpInvoiceToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), "SUMMARIZED");
                    childReportViewer.Show();
                    
                }
               
            }
        }

        

        private void FillBranchData()
        {
            objData = new Master();
            DataSet dsBranch = new DataSet();
            try
            {
                dsBranch = objData.GetSPPUBranchDataSet(CommonData.CompanyCode.ToString());
                DataTable dtBranch = dsBranch.Tables[0];
                if (dtBranch.Rows.Count > 0)
                {
                    DataRow dr = dtBranch.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";
                    dtBranch.Rows.InsertAt(dr, 0);

                    cbBranches.DataSource = dtBranch;
                    cbBranches.DisplayMember = "branch_name";
                    cbBranches.ValueMember = "branch_code";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objData = null;
                Cursor.Current = Cursors.Default;
            }
        }

        private void dtpInvoiceFromDate_ValueChanged(object sender, EventArgs e)
        {
            dtpInvoiceToDate.Value = dtpInvoiceFromDate.Value;
        }

        private void dtpInvoiceToDate_ValueChanged(object sender, EventArgs e)
        {
            if ((dtpInvoiceToDate.Value < dtpInvoiceFromDate.Value))
            {
                dtpInvoiceFromDate.Value = dtpInvoiceToDate.Value;
            }            
        }
    }
}
