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

namespace SSCRM
{
    public partial class HRLateComing : Form
    {
        private SQLDB objData = null;
        private string strChkCmp = "",strBranch="";
        public HRLateComing()
        {
            InitializeComponent();
        }

        private void HRLateComing_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            cbReportType.SelectedIndex = 0;
            dtpRepFrDate.Value = DateTime.Today;
            dtpRepToDate.Value = dtpRepFrDate.Value.AddDays(1);
        }
        private void FillCompanyData()
        {
            objData = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT DISTINCT(CM_COMPANY_NAME),CM_COMPANY_CODE FROM COMPANY_MAS WHERE ACTIVE='T'";

                dt = objData.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = item["CM_COMPANY_CODE"].ToString();
                        oclBox.Text = item["CM_COMPANY_NAME"].ToString();
                        clbCompany.Items.Add(oclBox);
                        oclBox = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objData = null;
                dt = null;
            }
        }
        private void FillBranchData(string sCmp)
        {
            objData = new SQLDB();
            DataTable dt = new DataTable();
            clbBranch.Items.Clear();
            string strCommand = "";
            try
            {
                if (chkCompanyAll.Checked == true ||clbCompany.CheckedItems.Count>0)
                {
                    if (strChkCmp == "ALL")
                    {
                        strCommand = "SELECT BRANCH_CODE as branchCode,BRANCH_NAME FROM BRANCH_MAS WHERE active='T' ";
                    }
                    else
                    {
                        strCommand = "SELECT BRANCH_CODE as branchCode,BRANCH_NAME FROM BRANCH_MAS WHERE active='T' and COMPANY_CODE in (" + sCmp + ")";
                    }

                    dt = objData.ExecuteDataSet(strCommand).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            NewCheckboxListItem oclBox = new NewCheckboxListItem();
                            oclBox.Tag = item["branchCode"].ToString();
                            oclBox.Text = item["BRANCH_NAME"].ToString();
                            clbBranch.Items.Add(oclBox);
                            oclBox = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objData = null;
                dt = null;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void chkCompanyAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCompanyAll.Checked == true)
            {
                for (int iVar = 0; iVar < clbCompany.Items.Count; iVar++)
                {
                    clbCompany.SetItemCheckState(iVar, CheckState.Checked);
                }
                strChkCmp = "";
                //for (int iVar = 0; iVar < clbCompany.Items.Count; iVar++)
                //{
                //    strChkCmp += "'"+((NewCheckboxListItem)clbCompany.Items[iVar]).Tag+"',";
                //}
                strChkCmp = "ALL";

            }
            else
            {
                for (int iVar = 0; iVar < clbCompany.Items.Count; iVar++)
                {
                    clbCompany.SetItemCheckState(iVar, CheckState.Unchecked);
                }
                strChkCmp = "";
            }
            FillBranchData(strChkCmp);
        }

        private void ChkBranch_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkBranch.Checked == true)
            {
                for (int iVar = 0; iVar < clbBranch.Items.Count; iVar++)
                {
                    clbBranch.SetItemCheckState(iVar, CheckState.Checked);
                }
                strBranch = "";
                //for (int iVar = 0; iVar < clbCompany.Items.Count; iVar++)
                //{
                //    strChkCmp += "'"+((NewCheckboxListItem)clbCompany.Items[iVar]).Tag+"',";
                //}
                strBranch = "ALL";

            }
            else
            {
                for (int iVar = 0; iVar < clbBranch.Items.Count; iVar++)
                {
                    clbBranch.SetItemCheckState(iVar, CheckState.Unchecked);
                }
                strBranch = "";
            }
        }

        private void clbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            strChkCmp = "";
            for (int iVar = 0; iVar < clbCompany.CheckedItems.Count; iVar++)
            {
                strChkCmp += "'" + ((NewCheckboxListItem)clbCompany.CheckedItems[iVar]).Tag + "',";
            }
            if (strChkCmp.Length > 0)
            {
                strChkCmp = strChkCmp.Remove(strChkCmp.Length-1);
                FillBranchData(strChkCmp);
            }
            else
            {
                clbBranch.Items.Clear();
            }
        }

        private void dtpRepToDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpRepFrDate.Value >= dtpRepToDate.Value)
            {
                dtpRepFrDate.Value = dtpRepToDate.Value.AddDays(-1);
            }
            
            
                DateTime dt1 = dtpRepFrDate.Value;
                DateTime dt2 = dtpRepToDate.Value;
                double totalDays = (dt2 - dt1).TotalDays;
                if (totalDays > 30)
                {
                    dtpRepToDate.Value = dtpRepFrDate.Value.AddDays(30);
                    MessageBox.Show("You get 31 days data Only");
                }
            
        }

        private void dtpRepFrDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpRepFrDate.Value >= dtpRepToDate.Value)
            {
                dtpRepToDate.Value = dtpRepFrDate.Value.AddDays(1);
            }
                DateTime dt1 = dtpRepFrDate.Value;
                DateTime dt2 = dtpRepToDate.Value;
                double totalDays = (dt2 - dt1).TotalDays;
                if (totalDays > 30)
                {
                    dtpRepToDate.Value = dtpRepFrDate.Value.AddDays(30);
                    MessageBox.Show("You get 31 days data Only");
                }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chkCompanyAll.Checked = false;
            ChkBranch.Checked = false;
            clbBranch.Items.Clear();
            for (int iVar = 0; iVar < clbCompany.Items.Count; iVar++)
            {
                clbCompany.SetItemCheckState(iVar, CheckState.Unchecked);
            }
            for (int iVar = 0; iVar < clbBranch.Items.Count; iVar++)
            {
                clbBranch.SetItemCheckState(iVar, CheckState.Unchecked);
            }

            dtpRepFrDate.Value = DateTime.Today;
            dtpRepToDate.Value = DateTime.Today.AddDays(1);
            txtLateComing.Text = "";
            txtEarlyGo.Text = "";
            cbReportType.SelectedIndex = 0;
            strChkCmp = "";
            strBranch = "";
        }

        private bool CheckData()
        {
            bool flag = true;
            if(strChkCmp.Length == 0)
            {
                MessageBox.Show("Select Company", "HR LateComing", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                flag = false;
            }
            else if (strBranch.Length == 0)
            {
                MessageBox.Show("Select Branch", "HR LateComing", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                flag = false;
            }
            return flag;

        }
        private void btnDisplay_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                if (txtEarlyGo.Text.Trim().Length == 0)
                {
                    txtEarlyGo.Text = "0";
                }
                if (txtLateComing.Text.Trim().Length == 0)
                {
                    txtLateComing.Text = "0";
                }
                crReportParams.FromDate = dtpRepFrDate.Value.ToString("dd/MMM/yyyy");
                crReportParams.ToDate = dtpRepToDate.Value.ToString("dd/MMM/yyyy");
                ReportViewer childLateComing = new ReportViewer(strChkCmp, strBranch, "ALL", txtEarlyGo.Text, txtLateComing.Text, cbReportType.SelectedItem.ToString());
                if (cbReportType.SelectedIndex == 1)
                {
                    CommonData.ViewReport = "SSBPLHO_ATTD_LateComing";
                }
                else
                {
                    CommonData.ViewReport = "SSBPLHO_ATTD";
                }
                childLateComing.Show();
            }
        }

        private void txtLateComing_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtEarlyGo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void clbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            strBranch = "";
            for (int iVar = 0; iVar < clbBranch.CheckedItems.Count; iVar++)
            {
                strBranch += "'" + ((NewCheckboxListItem)clbBranch.CheckedItems[iVar]).Tag + "',";
            }
            if (strBranch.Length > 0)
            {
                strBranch = strBranch.Remove(strBranch.Length - 1);
                //FillBranchData(strChkCmp);
            }
            //else
            //{
            //    clbBranch.Items.Clear();
            //}
        }
    }
}
