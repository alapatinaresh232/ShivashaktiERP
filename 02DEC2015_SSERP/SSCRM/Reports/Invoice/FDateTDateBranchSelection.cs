using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Web;
using System.IO;
using SSCRMDB;
using SSAdmin;
using SSTrans;


namespace SSCRM
{
    public partial class FDateTDateBranchSelection : Form
    {
        ReportViewer childReportViewer;
        SQLDB objData = null;
        private int iForm = 0;
        string strCheckBranches = "";
        string strComp = "", strBranch = "";
      
        public FDateTDateBranchSelection()
        {
            InitializeComponent();
        }
        public FDateTDateBranchSelection(int iFormType)
        {
            InitializeComponent();
            iForm = iFormType;          
        }
        private void FillUserBranchData()
        {

            objData = new SQLDB();
            //string sqlText = "";
            DataTable dt = new DataTable();            
            try
            {
                string strCommand = "SELECT UB_BRANCH_CODE ,BRANCH_NAME  FROM USER_BRANCH " +
                    " INNER JOIN BRANCH_MAS ON UB_BRANCH_CODE=BRANCH_CODE WHERE UB_USER_ID='" + CommonData.LogUserId+ "'";
                dt = objData.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dt.Rows)
                    {
                        if (dataRow["BRANCH_NAME"] + "" != "")
                        {
                            NewCheckboxListItem oclBox = new NewCheckboxListItem();
                            oclBox.Tag = dataRow["UB_BRANCH_CODE"].ToString();
                            oclBox.Text = dataRow["BRANCH_NAME"].ToString();
                            cblBranchesList.Items.Add(oclBox);
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

            //objData = new SQLDB();
            //string sqlText = "";
            //DataTable dt = new DataTable();

            ////sqlText = "SELECT DISTINCT COMPANY_CODE FROM USER_BRANCH INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE WHERE UB_USER_ID = '" + CommonData.LogUserId + "'";
            ////dt = objData.ExecuteDataSet(sqlText).Tables[0];
            ////if (dt.Rows.Count > 0)
            ////{
            ////    for (int i = 0; i < dt.Rows.Count; i++)
            ////    {
            ////        if (strComp != "")
            ////            strComp += ",";
            ////        strComp += dt.Rows[i]["COMPANY_CODE"].ToString();
            ////    }
            ////}
            ////else
            ////{
            ////    strComp += CommonData.CompanyCode.ToString();
            ////}
        }
        private void FillBranchData()
        {
            objData = new SQLDB();
            DataTable dt = new DataTable();
            //string strBranchData = "";
            try
            {
                string strCommand = "select BRANCH_NAME,BRANCH_CODE from BRANCH_MAS where ACTIVE='T' AND BRANCH_TYPE IN ('SP','BR','PU','TR','OL')";
                dt = objData.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dt.Rows)
                    {
                        if (dataRow["BRANCH_NAME"] + "" != "")
                        {
                            NewCheckboxListItem oclBox = new NewCheckboxListItem();
                            oclBox.Tag = dataRow["BRANCH_CODE"].ToString();
                            oclBox.Text = dataRow["BRANCH_NAME"].ToString();
                            cblBranchesList.Items.Add(oclBox);
                            oclBox = null;
                        }
                    }
                }
            }


            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objData = null;
                dt = null;
            }
        }

        private void FDateTDateBranchSelection_Load(object sender, EventArgs e)
        {
         
            if (iForm == 1 || iForm == 2)
            {
                FillBranchData();
                this.Text = "Reconsilationn By Destination";
                dtpFromDate.Value = Convert.ToDateTime(CommonData.DocMonth);
                dtpToDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            }
            if (iForm == 3)
            {
                this.Text = "Calibration Certificate Details";
                lblFromDate.Visible = false;
                lblToDate.Visible = false;
                dtpFromDate.Visible = false;
                dtpToDate.Visible = false;
                FillUserBranchData();
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            int index = cblBranchesList.FindString(this.txtSearch.Text);
            if (0 <= index)
            {
                cblBranchesList.SelectedIndex = index;
            }
            if (this.txtSearch.Text.Trim() == "")
            {
                cblBranchesList.SelectedIndex = -1;
            }
         }

        private void btnReport_Click(object sender, EventArgs e)
       {
            if (CheckData())
            {
                if (iForm == 1)
                {

                    string sDestBranch = string.Empty;
                    for (int i = 0; i < cblBranchesList.Items.Count; i++)
                    {
                        if (cblBranchesList.GetItemCheckState(i) == CheckState.Checked)
                        {
                            sDestBranch += "" + ((NewCheckboxListItem)cblBranchesList.Items[i]).Tag + ",";

                        }
                        sDestBranch = sDestBranch.TrimEnd(',');
                    }
                    ReportViewer childForm = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, sDestBranch, Convert.ToDateTime(dtpFromDate.Value).ToString("dd/MMM/yyyy"), Convert.ToDateTime(dtpToDate.Value).ToString("dd/MMM/yyyy"), "");
                    CommonData.ViewReport = "Reconsilation_By_Destination_Branch";
                    childForm.Show();

                }
                if (iForm == 2)
                {
                 
                    string sDestBranch =string.Empty;
                    for (int i = 0; i < cblBranchesList.Items.Count; i++)
                    {
                        if (cblBranchesList.GetItemCheckState(i) == CheckState.Checked)
                        {
                           sDestBranch += "" + ((NewCheckboxListItem)cblBranchesList.Items[i]).Tag + ",";

                        }
                        //sDestBranch = sDestBranch.TrimEnd(',');
                    }
                    ReportViewer childForm = new ReportViewer(CommonData.CompanyCode, sDestBranch, Convert.ToDateTime(dtpFromDate.Value).ToString("dd/MMM/yyyy"), Convert.ToDateTime(dtpToDate.Value).ToString("dd/MMM/yyyy"), "SELECTION");
                    CommonData.ViewReport = "SSERP_REP_STATIONARY_DELIVERY_CHALLAN_REGISTER";
                    childForm.Show();
                  
                }
                if (iForm == 3)
                {

                    string sDestBranch = string.Empty;
                    for (int i = 0; i < cblBranchesList.Items.Count; i++)
                    {
                        if (cblBranchesList.GetItemCheckState(i) == CheckState.Checked)
                        {
                            sDestBranch += "" + ((NewCheckboxListItem)cblBranchesList.Items[i]).Tag + ",";

                        }
                        //sDestBranch = sDestBranch.TrimEnd(',');
                    }
                    CommonData.ViewReport = "SSERP_REP_SP_CALIBRATION_CERTIFICATE";
                    childReportViewer = new ReportViewer(CommonData.CompanyCode, sDestBranch, "", "", "", "");
                    childReportViewer.Show();
                }
               
            }
                        
        }

        private bool CheckData()
        {
            if (cblBranchesList.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please Select Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (Convert.ToDateTime(dtpFromDate.Value) > Convert.ToDateTime(dtpToDate.Value))
            {
                MessageBox.Show("Invalid DateTime Selection", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }       

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void cblBranchesList_SelectedIndexChanged(object sender, EventArgs e)
        {
             strCheckBranches = "";
            for (int i = 0; i < cblBranchesList.CheckedItems.Count;i++)
            {
                strCheckBranches += " " + cblBranchesList.CheckedItems[i].ToString() + " \n";
            }
        }

       

              
    }
}
