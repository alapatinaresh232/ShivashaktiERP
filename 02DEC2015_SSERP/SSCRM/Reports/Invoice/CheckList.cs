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
using SSTrans;
namespace SSCRM
{
    public partial class CheckList : Form
    {
        ReportViewer childReportViewer;
        Security objSecurity = null;
        SQLDB objSQLDB = null;
        string strData = "";
        public CheckList()
        {
            InitializeComponent();
        }

        private void CheckList_Load(object sender, EventArgs e)
        {
            dtpDate.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
                //System.DateTime.Now;
            GetChckListPopup();
        }

        public void GetChckListPopup()
        {
            objSecurity = new Security();
            DataTable dtCpy = objSecurity.GetCompanyDataSet().Tables[0];
            for (int i = 0; i < dtCpy.Rows.Count; i++)
            {
                chklCompany.Items.Add(dtCpy.Rows[i][1].ToString(), false);
            }
            objSecurity = null;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            childReportViewer = new ReportViewer("ALL", "ALL", dtpDate.Value.ToString("MMMyyyy"), 0);
            CommonData.ViewReport = "SSCRM_CHECKLIST";
            childReportViewer.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cgkCmp_CheckedChanged(object sender, EventArgs e)
        {
            if (cgkCmp.Checked == true)
            {
                for (int i = 0; i < chklCompany.Items.Count; i++)
                {
                    chklCompany.SetItemCheckState(i, CheckState.Checked);
                }
                for (int i = 0; i < chklCompany.Items.Count; i++)
                {
                    strData += "'" + chklCompany.Items[i].ToString() + "',";
                }
                GetBuindBranch(strData);
            }
            else
            {
                for (int i = 0; i < chklCompany.Items.Count; i++)
                {
                    chklCompany.SetItemCheckState(i, CheckState.Unchecked);
                }
                chkBranch.Checked = false;
                chklBranch.Items.Clear();
                lblCnt.Text = "0";
                strData = "";
            }
        }

        private void chkBranch_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBranch.Checked == true)
            {
                for (int i = 0; i < chklBranch.Items.Count; i++)
                {
                    chklBranch.SetItemCheckState(i, CheckState.Checked);
                }
            }
            else
            {
                for (int i = 0; i < chklBranch.Items.Count; i++)
                {
                    chklBranch.SetItemCheckState(i, CheckState.Unchecked);
                }
            }
        }

        private void chklCompany_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (chklCompany.SelectedItem != null)
            {
                if (e != null)
                {
                    if (e.NewValue.ToString() == "Checked")
                    {
                        strData += "'" + chklCompany.SelectedItem.ToString() + "'" + ",";
                    }
                    else if (e.NewValue.ToString() == "Unchecked")
                    {
                        strData = strData.Replace("'" + chklCompany.SelectedItem.ToString() + "'" + ",", "");
                    }
                    if (strData != "")
                    {
                        GetBuindBranch(strData);
                    }
                    else
                    {
                        chklBranch.Items.Clear();
                        lblCnt.Text = "0";
                    }
                }
                else
                {

                    for (int i = 0; i < chklCompany.Items.Count; i++)
                    {
                        strData += "'" + chklCompany.Items[i].ToString() + "',";
                    }
                }
            }
        }
        public void GetBuindBranch(string strData)
        {
            chklBranch.Items.Clear();
            objSQLDB = new SQLDB();
            string strQry = "SELECT BRANCH_CODE,BRANCH_NAME FROM BRANCH_MAS WHERE COMPANY_CODE IN (SELECT CM_COMPANY_CODE FROM DBO.COMPANY_MAS WHERE CM_COMPANY_NAME IN " +
                        "(" + strData.TrimEnd(',') + "))";
            DataSet ds = objSQLDB.ExecuteDataSet(strQry);
            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                chklBranch.Items.Add(ds.Tables[0].Rows[j][1].ToString(), false);
            }
            objSQLDB = null;
            lblCnt.Text = ds.Tables[0].Rows.Count.ToString();
            strData = "";
        }
    }
}
