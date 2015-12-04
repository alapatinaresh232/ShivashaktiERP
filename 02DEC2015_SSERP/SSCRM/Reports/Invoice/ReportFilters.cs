using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SSCRMDB;
using SSAdmin;
using SSTrans;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office;
using System.Xml;

namespace SSCRM
{
    public partial class ReportFilters : Form
    {
        ReportViewer childReportViewer;
        SQLDB objSQLdb = null;
        private string Company = string.Empty;
        private string State = string.Empty;
        private string Branches = string.Empty, strGrpRange = "";
         UtilityDB objUtilityDB = null;
        ExcelDB objExDb = null;
        string IformType = null;
       private string strReportType = "";


        public ReportFilters()
        {
            InitializeComponent();
        }
        public ReportFilters(string sRep)
        {
            InitializeComponent();
            IformType = sRep;
        }


        private void BranchReportFilter_Load(object sender, EventArgs e)
        {
            
            dtpFromDoc.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.DocMonth).ToString("MMMyyyy").ToUpper());
            dtpToDoc.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.DocMonth).ToString("MMMyyyy").ToUpper());
            lblFrmDesg.Visible = false;
            lblToDesgId.Visible = false;
            cbFrmDesg.Visible = false;
            cbToDesigId.Visible = false;
            lblLOS.Visible = false;
            dtpLOSAsOnDate.Visible = false;

            if (IformType == "Field Support")
            {
                FillReportType();
                lblGroup.Visible = true;
                chkGrpPerMnth.Visible = false;
                chkTotalGrps.Visible = false;
                txtFrmGrps.Visible = false;
                txtNoofRecords.Visible = false;
                txtToGrps.Visible = false;
                lblTo.Visible = false;
                lblFrm.Visible = false;
                lblNoOfRec.Visible = false;
                FillBranches();
                dtpFromDoc.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.DocMonth).ToString("MMMyyyy").ToUpper());
                dtpToDoc.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.DocMonth).ToString("MMMyyyy").ToUpper());
                cbSortBy.SelectedIndex = 0;

            }
            if (IformType == "RECRUITMENT_VS_RETAINED_SRS")
            {
                FillBranches();
                lblNoOfRec.Visible = false;
                lblSortBy.Visible = false;
                cbSortBy.Visible = false;
                txtFrmGrps.Visible = false;
                lblTo.Visible = false;
                lblFrm.Text = "Successed SRs Points";
                chkTotalGrps.Visible = false;
                txtNoofRecords.Visible = false;
                chkGrpPerMnth.Checked = true;
                chkGrpPerMnth.Visible = false;
                lblGroup.Visible = true;
                lblGroup.Text = "Groups Per Month";
            }
            if (IformType == "ALL_INDIA_TOP")
            {

                FillBranches();
                chkTotalGrps.Checked = true;
                FillReportType();
                dtpFromDoc.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.DocMonth).ToString("MMMyyyy").ToUpper());
                dtpToDoc.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.DocMonth).ToString("MMMyyyy").ToUpper());
                
            }
            if (IformType == "SALES_ANALYSIS")
            {
                FillBranches();
                //lblGroup.Visible = true;
                lblNoOfRec.Visible = false;
                lblSortBy.Visible = false;
                cbSortBy.Visible = false;
                txtFrmGrps.Visible = false;
                lblTo.Visible = false;
                lblFrm.Text = "Top Percentage";
                chkTotalGrps.Visible = false;
                txtNoofRecords.Visible = false;
               // chkGrpPerMnth.Text = "Groups";
                chkGrpPerMnth.Checked = false;
                chkGrpPerMnth.Visible = false;
                lblGroup.Visible = true;
                lblGroup.Text = "Groups Per Month";
                
            }
            if (IformType == "SR_ANALYSIS_BY_AGE")
            {
                FillBranches();
                lblNoOfRec.Visible = false;
                lblSortBy.Visible = false;
                cbSortBy.Visible = false;
                txtFrmGrps.Visible = false;
                lblTo.Visible = false;
                lblFrm.Text = "Successed SRs Points";
                chkTotalGrps.Visible = false;
                txtNoofRecords.Visible = false;
                chkGrpPerMnth.Checked = false;
                chkGrpPerMnth.Visible = false;
                txtFrmGrpPerMnth.Visible = false;
                txtToGrpPerMnth.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                lblGroup.Visible = false;
                btnDownload.Visible = false;
            }

            if (IformType == "SALES_EMP_LIST")
            {
                FillReportType();
                FillFromDesigComboBox();
                lblGroup.Visible = true;
                chkGrpPerMnth.Visible = false;
                chkTotalGrps.Visible = false;
                txtFrmGrps.Visible = false;
                txtNoofRecords.Visible = false;
                txtToGrps.Visible = false;
                lblTo.Visible = false;
                lblFrm.Visible = false;
                lblNoOfRec.Visible = false;
                FillBranches();
                dtpFromDoc.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.DocMonth).ToString("MMMyyyy").ToUpper());
                dtpToDoc.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.DocMonth).ToString("MMMyyyy").ToUpper());
                cbSortBy.SelectedIndex = 0;
                lblSortBy.Text = "Report Type";

                lblFrmDesg.Visible = true;
                lblToDesgId.Visible = true;
                cbFrmDesg.Visible = true;
                cbToDesigId.Visible = true;
                lblDocm.Visible = false;
                lblToDoc.Visible = false;
                dtpToDoc.Visible = false;

                lblLOS.Visible = true;
                dtpLOSAsOnDate.Visible = true;

            }

        }

        private void FillReportType()
        {
            DataTable dt = new DataTable();
            cbSortBy.Items.Clear();

            dt.Columns.Add("type", typeof(string));
            dt.Columns.Add("name", typeof(string));

            if (IformType == "ALL_INDIA_TOP")
            {
                dt.Rows.Add("GROUP", "GROUP");
                dt.Rows.Add("PERSONAL", "PERSONAL");
            }
            if (IformType == "Field Support")
            {               
                dt.Rows.Add("GC2ABM", "GC2ABM");
                dt.Rows.Add("DBM2SR.BM", "DBM2SR.BM");
            }
            if (IformType == "SALES_EMP_LIST")
            {
                dt.Rows.Add("DETAILED", "DETAILED");
                dt.Rows.Add("SUMMARY", "SUMMARY");
            }


            cbSortBy.DataSource = dt;
            cbSortBy.DisplayMember = "name";
            cbSortBy.ValueMember = "type";
        }
              

        private DataSet Get_UserBranchStateFilterCursor(string sCompCode, string sStateCode,string sLogUserId, string sBranchtType, string sGetType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompany", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sStateCode", DbType.String, sStateCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sUser", DbType.String, sLogUserId, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sBranchType", DbType.String, sBranchtType, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sType", DbType.String, sGetType, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("Get_UserBranchStateFilterCursor", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }

        private void FillBranches()
        {
            tvBranches.Nodes.Clear();
            objSQLdb = new SQLDB();
            DataSet ds = new DataSet();
            try
            {
                ds = Get_UserBranchStateFilterCursor("", "",CommonData.LogUserId,"","PARENT");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        tvBranches.Nodes.Add(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), ds.Tables[0].Rows[i]["COMPANY_NAME"].ToString());

                        DataSet dsState = new DataSet();

                        dsState = Get_UserBranchStateFilterCursor(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "",CommonData.LogUserId,"","STATE");

                        if (dsState.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < dsState.Tables[0].Rows.Count; j++)
                            {
                                tvBranches.Nodes[i].Nodes.Add(dsState.Tables[0].Rows[j]["StateCode"].ToString(), dsState.Tables[0].Rows[j]["StateName"].ToString());

                                DataSet dschild = new DataSet();
                                dschild = Get_UserBranchStateFilterCursor(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), dsState.Tables[0].Rows[j]["StateCode"].ToString(),CommonData.LogUserId,"BR", "CHILD");

                                if (dschild.Tables[0].Rows.Count > 0)
                                {
                                    for (int k = 0; k < dschild.Tables[0].Rows.Count; k++)
                                    {

                                        tvBranches.Nodes[i].Nodes[j].Nodes.Add(dschild.Tables[0].Rows[k]["BranCode"].ToString(), dschild.Tables[0].Rows[k]["BranchName"].ToString());
                                    }
                                }


                            }
                        }


                    }

                }

                for (int ivar = 0; ivar < tvBranches.Nodes.Count; ivar++)
                {
                    for (int j = 0; j < tvBranches.Nodes[ivar].Nodes.Count; j++)
                    {
                        if (tvBranches.Nodes[ivar].Nodes[j].Nodes.Count > 0)
                            tvBranches.Nodes[ivar].FirstNode.Expand();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
     
        private bool CheckData()
        {
            bool flag = false;

            
                for (int i = 0; i < tvBranches.Nodes.Count; i++)
                {
                    for (int j = 0; j < tvBranches.Nodes[i].Nodes.Count; j++)
                    {
                        for (int k = 0; k < tvBranches.Nodes[i].Nodes[j].Nodes.Count; k++)
                        {

                            if (tvBranches.Nodes[i].Nodes[j].Nodes[k].Checked == true)
                            {
                                flag = true;
                            }
                        }
                    }

                }

                if (flag == false)
                {
                    MessageBox.Show("Please Select Atleast One Branch", "SSCRM-Report", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return flag;
                }
                if (IformType == "Field Support")
                {
                    if (dtpFromDoc.Value > dtpToDoc.Value)
                    {
                        flag = false;
                        MessageBox.Show("Please Select Valid Document Months", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        dtpFromDoc.Focus();
                        return flag;
                    }
                    if (txtFrmGrpPerMnth.Text.Length == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Enter Groups FromRange ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtFrmGrpPerMnth.Focus();
                        return flag;
                    }
                    if (txtToGrpPerMnth.Text.Length == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Enter Groups  ToRange ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtToGrpPerMnth.Focus();
                        return flag;
                    }
                }
                if (IformType == "ALL_INDIA_TOP")
                {
                    
                    if (dtpFromDoc.Value > dtpToDoc.Value)
                    {
                        flag = false;
                        MessageBox.Show("Please Select Valid Document Months", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        dtpFromDoc.Focus();
                        return flag;
                    }
                    if (chkTotalGrps.Checked == false)
                    {
                        flag = false;
                        MessageBox.Show("Please Select Total Groups", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        chkTotalGrps.Focus();
                        return flag;
                    }

                    if (txtFrmGrps.Text.Length == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Enter From Groups ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtFrmGrps.Focus();
                        return flag;
                    }
                    if (txtToGrps.Text.Length == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Enter To Groups ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtToGrps.Focus();
                        return flag;
                    }
                    if (chkGrpPerMnth.Checked == true)
                    {
                        if (txtFrmGrpPerMnth.Text.Length == 0)
                        {
                            flag = false;
                            MessageBox.Show("Please Enter Groups Per Month From Value", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            txtFrmGrpPerMnth.Focus();
                            return flag;
                        }
                        if (txtToGrpPerMnth.Text.Length == 0)
                        {
                            flag = false;
                            MessageBox.Show("Please Enter Groups Per Month To Value", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            txtToGrpPerMnth.Focus();
                            return flag;
                        }

                    }
                    if (txtNoofRecords.Text.Length == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Enter No Of Records", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtNoofRecords.Focus();
                        return flag;
                    }
                }
                if (IformType == "RECRUITMENT_VS_RETAINED_SRS")
                {
                    if (chkGrpPerMnth.Checked == true)
                    {
                        if (txtFrmGrpPerMnth.Text.Length == 0)
                        {
                            flag = false;
                            MessageBox.Show("Please Enter Groups Per Month From Value", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            txtFrmGrpPerMnth.Focus();
                            return flag;
                        }
                        if (txtToGrpPerMnth.Text.Length == 0)
                        {
                            flag = false;
                            MessageBox.Show("Please Enter Groups Per Month To Value", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            txtToGrpPerMnth.Focus();
                            return flag;
                        }

                    }
                    if (txtToGrps.Text.Length == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Enter Successed SRs Points", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtToGrps.Focus();
                        return flag;
                    }
                }
                if (IformType == "SR_ANALYSIS_BY_AGE")
                {
                    if (txtToGrps.Text.Length == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Enter Successed SRs Points", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtToGrps.Focus();
                        return flag;
                    }
                }

            return flag;
        }
       
        private void GetSelectedCompAndBranches()
        {
            Company = "";
            Branches = "";
            State = "";

            bool iscomp = false;
            bool iSstate=false;
            for (int i = 0; i < tvBranches.Nodes.Count; i++)
            {
                for (int j = 0; j < tvBranches.Nodes[i].Nodes.Count; j++)
                {
                    for (int k = 0; k < tvBranches.Nodes[i].Nodes[j].Nodes.Count; k++)
                    {
                        
                        if (tvBranches.Nodes[i].Nodes[j].Nodes[k].Checked == true)
                        {
                            if (Branches != string.Empty)
                                Branches += ",";
                            Branches += tvBranches.Nodes[i].Nodes[j].Nodes[k].Name.ToString();
                            iscomp = true;
                            iSstate = true;
                        }
                       
                    }
                    if (iSstate == true)
                    {
                        if (State != string.Empty)
                            State += ",";
                        State += tvBranches.Nodes[i].Nodes[j].Name.ToString();
                    }
                    iSstate = false;  
                }
              
                if (iscomp == true)
                {
                    if (Company != string.Empty)
                        Company += ",";
                    Company += tvBranches.Nodes[i].Name.ToString();
                }
                iscomp = false;
            }

            if (txtFrmGrpPerMnth.Text.Length == 0)
            {
                txtFrmGrpPerMnth.Text = "0";
            }
            if (txtToGrpPerMnth.Text.Length == 0)
            {
                txtToGrpPerMnth.Text = "0";
            }
        }

        private void Get_GroupRangeValue()
        {
            Int32 nValue = 0, nGrpRange = 0; double sRange = 0.00;
            strGrpRange = "";

            if (txtToGrps.Text.Length == 0 || txtToGrps.Text.Equals("0"))
                txtToGrps.Text = "25";

            if (txtToGrps.Text.Length != 0 || Convert.ToDouble(txtToGrps.Text) != 0)
            {
                nGrpRange = Convert.ToInt32(txtToGrps.Text);
                nValue = 100 / nGrpRange;

                for (int i = 0; i < nValue; i++)
                {
                    sRange += Convert.ToDouble(txtToGrps.Text);

                    double frmValue = 0.00;
                    if (i == 0)
                        frmValue = 00.01;
                    else
                        frmValue = sRange - Convert.ToDouble(txtToGrps.Text);

                    strGrpRange += "" + frmValue.ToString("00.00") + "-" + sRange.ToString("0.00") + ",";
                    if (sRange != 100.0 && i == (nValue - 1))
                    {
                        strGrpRange += "" + sRange.ToString("00.00") + "-100.00,";
                    }

                }
            }
            strGrpRange = strGrpRange.TrimEnd(',');
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (IformType == "Field Support")
            {
                cbSortBy.SelectedIndex = 0;
            }
            tvBranches.Nodes.Clear();
            dtpFromDoc.Value = DateTime.Now;
            dtpToDoc.Value = DateTime.Now;
            txtNoofRecords.Text = "";
           
            txtFrmGrps.Text = "";
            txtToGrps.Text = "";
            txtToGrpPerMnth.Text = "";
            txtFrmGrpPerMnth.Text = "";
            chkGrpPerMnth.Checked = false;
            chkTotalGrps.Checked = true;

            FillBranches();
        }

        private void GetReportType()
        {
            strReportType = "";

            if (chkGrpPerMnth.Checked == true && chkTotalGrps.Checked == true && cbSortBy.SelectedIndex == 0)
            {
                strReportType = "PER_MONTH_TOTAL_GROUP_WISE";
            }
            if (chkGrpPerMnth.Checked == true && chkTotalGrps.Checked == true && cbSortBy.SelectedIndex == 1)
            {
                strReportType = "PER_MONTH_TOTAL_GROUP_PERSONAL_WISE";
            }
            if (chkGrpPerMnth.Checked == false && chkTotalGrps.Checked == true && cbSortBy.SelectedIndex == 1)
            {
                strReportType = "TOTAL_GROUP_PERSONAL_WISE";
            }
            if (chkGrpPerMnth.Checked == false && chkTotalGrps.Checked == true && cbSortBy.SelectedIndex == 0)
            {
                strReportType = "TOTAL_GROUP_WISE";
            }
            if (strReportType.Length == 0)
            {
                strReportType = "TOTAL_GROUP_WISE";
            }
            
        }

        private void btnReport_Click(object sender, EventArgs e)
        {

            if (CheckData() == true)
            {
                GetSelectedCompAndBranches();
                if (IformType == "Field Support")
                {
                    if (cbSortBy.SelectedIndex == 0)
                    {
                        CommonData.ViewReport = "SSERP_REP_FieldSupport_Deviations_GC2ABM";
                        childReportViewer = new ReportViewer(Company, Branches, dtpFromDoc.Value.ToString("MMMyyyy").ToUpper(), dtpToDoc.Value.ToString("MMMyyyy").ToUpper(), txtFrmGrpPerMnth.Text, txtToGrpPerMnth.Text, cbSortBy.Text.ToString());
                        childReportViewer.Show();
                    }
                    if (cbSortBy.SelectedIndex == 1)
                    {
                        CommonData.ViewReport = "SSERP_REP_FieldSupport_Deviations_DBM2SR.BM";
                        childReportViewer = new ReportViewer(Company, Branches, dtpFromDoc.Value.ToString("MMMyyyy").ToUpper(), dtpToDoc.Value.ToString("MMMyyyy").ToUpper(), txtFrmGrpPerMnth.Text, txtToGrpPerMnth.Text, cbSortBy.Text.ToString());
                        childReportViewer.Show();
                    }
                }
                if (IformType == "RECRUITMENT_VS_RETAINED_SRS")
                {
                    CommonData.ViewReport = "SSERP_REP_RECRUITMENT_VS_RETAINED_SRS";
                    childReportViewer = new ReportViewer(Company, Branches, dtpFromDoc.Value.ToString("MMMyyyy").ToUpper(), dtpToDoc.Value.ToString("MMMyyyy").ToUpper(), txtFrmGrpPerMnth.Text, txtToGrpPerMnth.Text, txtToGrps.Text, "");
                    childReportViewer.Show();
                }
                if (IformType == "ALL_INDIA_TOP")
                {
                    GetReportType();

                    if (strReportType == "PER_MONTH_TOTAL_GROUP_WISE")
                    {
                        CommonData.ViewReport = "SSCRM_REP_ALL_INDIA_TOPPERS_FOR_AWARDS";
                        childReportViewer = new ReportViewer(Company, Branches, "", "", "", Convert.ToInt32(txtFrmGrpPerMnth.Text), Convert.ToInt32(txtToGrpPerMnth.Text), Convert.ToInt32(txtFrmGrps.Text), Convert.ToInt32(txtToGrps.Text), Convert.ToInt32(txtNoofRecords.Text), dtpFromDoc.Value.ToString("MMMyyyy").ToUpper(), dtpToDoc.Value.ToString("MMMyyyy").ToUpper(), "PER_MONTH_TOTAL_GROUP_WISE");
                        childReportViewer.Show();
                    }
                    if (strReportType == "PER_MONTH_TOTAL_GROUP_PERSONAL_WISE")
                    {
                        CommonData.ViewReport = "SSCRM_REP_ALL_INDIA_TOPPERS_FOR_AWARDS";
                        childReportViewer = new ReportViewer(Company, Branches, "", "", "", Convert.ToInt32(txtFrmGrpPerMnth.Text), Convert.ToInt32(txtToGrpPerMnth.Text), Convert.ToInt32(txtFrmGrps.Text), Convert.ToInt32(txtToGrps.Text), Convert.ToInt32(txtNoofRecords.Text), dtpFromDoc.Value.ToString("MMMyyyy").ToUpper(), dtpToDoc.Value.ToString("MMMyyyy").ToUpper(), "PER_MONTH_TOTAL_GROUP_PERSONAL_WISE");
                        childReportViewer.Show();
                    }
                    if (strReportType == "TOTAL_GROUP_PERSONAL_WISE")
                    {
                        CommonData.ViewReport = "SSCRM_REP_ALL_INDIA_TOPPERS_FOR_AWARDS";
                        childReportViewer = new ReportViewer(Company, Branches, "", "", "", 0, 0, Convert.ToInt32(txtFrmGrps.Text), Convert.ToInt32(txtToGrps.Text), Convert.ToInt32(txtNoofRecords.Text), dtpFromDoc.Value.ToString("MMMyyyy").ToUpper(), dtpToDoc.Value.ToString("MMMyyyy").ToUpper(), "TOTAL_GROUP_PERSONAL_WISE");
                        childReportViewer.Show();
                    }
                    if (strReportType == "TOTAL_GROUP_WISE")
                    {
                        CommonData.ViewReport = "SSCRM_REP_ALL_INDIA_TOPPERS_FOR_AWARDS";
                        childReportViewer = new ReportViewer(Company, Branches, "", "", "", 0, 0, Convert.ToInt32(txtFrmGrps.Text), Convert.ToInt32(txtToGrps.Text), Convert.ToInt32(txtNoofRecords.Text), dtpFromDoc.Value.ToString("MMMyyyy").ToUpper(), dtpToDoc.Value.ToString("MMMyyyy").ToUpper(), "TOTAL_GROUP_WISE");
                        childReportViewer.Show();
                    }
                }

                if (IformType == "SALES_ANALYSIS")
                {
                    Get_GroupRangeValue();

                    CommonData.ViewReport = "SSERP_REP_GROUPS_SALES_ANALYSATION";
                    childReportViewer = new ReportViewer(Company, Branches, dtpFromDoc.Value.ToString("MMMyyyy").ToUpper(), dtpToDoc.Value.ToString("MMMyyyy").ToUpper(), strGrpRange, txtFrmGrpPerMnth.Text, txtToGrpPerMnth.Text, "");
                    childReportViewer.Show();
                }

                if (IformType == "SR_ANALYSIS_BY_AGE")
                {
                    CommonData.ViewReport = "SSERP_REP_AGE_WISE_SR_ANALYSIS";
                    childReportViewer = new ReportViewer(Company, Branches, dtpFromDoc.Value.ToString("MMMyyyy").ToUpper(), dtpToDoc.Value.ToString("MMMyyyy").ToUpper(), "0", Convert.ToInt32(txtToGrps.Text), "AGE_WISE_SR_ANALYSIS");
                    childReportViewer.Show();
                }
                if (IformType == "SALES_EMP_LIST")
                {
                    if (cbSortBy.SelectedIndex == 0)
                    {
                        CommonData.ViewReport = "SSERP_REP_SALES_EMP_LIST";
                        childReportViewer = new ReportViewer(Company, Branches, dtpFromDoc.Value.ToString("MMMyyyy").ToUpper(), Convert.ToInt32(txtFrmGrpPerMnth.Text),Convert.ToInt32(txtToGrpPerMnth.Text),Convert.ToInt32(cbFrmDesg.SelectedValue),Convert.ToInt32(cbToDesigId.SelectedValue),Convert.ToDateTime(dtpLOSAsOnDate.Value).ToString("dd/MMM/yyyy"),"DETAILED");
                        childReportViewer.Show();
                    }
                    else
                    {
                        CommonData.ViewReport = "SSERP_REP_SALES_EMP_SUM_BASED_ON_GRPS";
                        childReportViewer = new ReportViewer(Company, Branches, dtpFromDoc.Value.ToString("MMMyyyy").ToUpper(), Convert.ToInt32(txtFrmGrpPerMnth.Text), Convert.ToInt32(txtToGrpPerMnth.Text), Convert.ToInt32(cbFrmDesg.SelectedValue), Convert.ToInt32(cbToDesigId.SelectedValue),"","SUMMARY");
                        childReportViewer.Show();

                    }
                }
            }

        }
              

        private void tvBranches_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TriStateTreeView.getStatus(e);
            tvBranches.BeginUpdate();

            foreach (TreeNode Node in e.Node.Nodes)
            {
                Node.Checked = e.Node.Checked;
            }

            tvBranches.EndUpdate();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {
                GetSelectedCompAndBranches();

                #region "IformType :: Field Support Deviations"
                try
                {

                    objExDb = new ExcelDB();
                    objUtilityDB = new UtilityDB();
                    DataTable dtExcel = new DataTable();
                    if (IformType == "Field Support")
                    {
                        if (cbSortBy.SelectedIndex == 0)
                        {

                            dtExcel = objExDb.GetStmtofFieldSupportAndDeviations(Company, Branches, dtpFromDoc.Value.ToString("MMMyyyy").ToUpper(), dtpToDoc.Value.ToString("MMMyyyy").ToUpper(), Convert.ToInt32(txtFrmGrpPerMnth.Text), Convert.ToInt32(txtToGrpPerMnth.Text), cbSortBy.Text.ToString()).Tables[0];
                            objExDb = null;

                            if (dtExcel.Rows.Count > 0)
                            {
                                Excel.Application oXL = new Excel.Application();
                                Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                                Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                                oXL.Visible = true;

                                int iTotColumns = 0;
                                iTotColumns = 25;
                                string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                                Excel.Range rgHead = null;
                                Excel.Range rg = worksheet.get_Range("A4", sLastColumn + "4");
                                Excel.Range rgData = worksheet.get_Range("A5", sLastColumn + (dtExcel.Rows.Count + 4).ToString());
                                rgData.Font.Size = 11;
                                rgData.WrapText = true;
                                rgData.VerticalAlignment = Excel.Constants.xlCenter;
                                rgData.Borders.Weight = 2;

                                rgData = worksheet.get_Range("A1", "H2");
                                rgData.Merge(Type.Missing);
                                rgData.Font.Bold = true; rgData.Font.Size = 16;
                                rgData.Value2 = "STATEMENT OF FIELD SUPPORT AND REPORTING DEVIATIONS [GC-ABM 1-3 GROUPS]";
                                rgData.VerticalAlignment = Excel.Constants.xlCenter;
                                rgData.HorizontalAlignment = Excel.Constants.xlCenter;

                                rg.Font.Bold = true;
                                rg.Font.Name = "Times New Roman";
                                rg.Font.Size = 10;
                                rg.WrapText = true;
                                rg.Font.ColorIndex = 2; // White Color : 2 and Red = 3,30; Green = 10,43; 
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.Interior.ColorIndex = 31;
                                rg.Borders.Weight = 2;
                                rg.Borders.LineStyle = Excel.Constants.xlSolid;
                                rg.Cells.RowHeight = 38;

                                rg = worksheet.get_Range("A4", Type.Missing);
                                rg.Cells.ColumnWidth = 6;
                                rg = worksheet.get_Range("B4", Type.Missing);
                                rg.Cells.ColumnWidth = 8;
                                rg = worksheet.get_Range("C4", Type.Missing);
                                rg.Cells.ColumnWidth = 20;
                                rg = worksheet.get_Range("D4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("E4", Type.Missing);
                                rg.Cells.ColumnWidth = 30;
                                rg = worksheet.get_Range("F3", "H3");
                                rg.Merge(Type.Missing);
                                rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                                rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                                rg.Value2 = "ACTUAL DESIG&SALARY";
                                rg = worksheet.get_Range("F4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("G4", Type.Missing);
                                rg.Cells.ColumnWidth = 8;
                                rg = worksheet.get_Range("H4", Type.Missing);
                                rg.Cells.ColumnWidth = 8;
                                rg = worksheet.get_Range("I4", Type.Missing);
                                rg.Cells.ColumnWidth = 30;
                                rg = worksheet.get_Range("J4", Type.Missing);
                                rg.Cells.ColumnWidth = 8;
                                rg = worksheet.get_Range("K4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;

                                rg = worksheet.get_Range("L3", "N3");
                                rg.Merge(Type.Missing);
                                rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                                rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                                rg.Value2 = "PERSONAL POINTS";
                                rg = worksheet.get_Range("L4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("M4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("N4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;

                                rg = worksheet.get_Range("O4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("P4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;



                                rg = worksheet.get_Range("Q3", "R3");
                                rg.Merge(Type.Missing);
                                rg.Value2 = "VEHICLE STATUS";
                                rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                                rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;


                                rg = worksheet.get_Range("Q4", Type.Missing);
                                rg.Cells.ColumnWidth = 6;
                                rg = worksheet.get_Range("R4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;

                                rg = worksheet.get_Range("S4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("T4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;

                                rg = worksheet.get_Range("U4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;

                                rg = worksheet.get_Range("V4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;

                                rg = worksheet.get_Range("W4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;

                                rg = worksheet.get_Range("X3", "Y3");
                                rg.Merge(Type.Missing);
                                rg.Value2 = "FIELD SUPPORT";
                                rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                                rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;

                                rg = worksheet.get_Range("x4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;

                                rg = worksheet.get_Range("y4", Type.Missing);
                                rg.Cells.ColumnWidth = 8;




                                int iColumn = 1, iStartRow = 4;
                                worksheet.Cells[iStartRow, iColumn++] = "SlNo";
                                worksheet.Cells[iStartRow, iColumn++] = "Doc month";
                                worksheet.Cells[iStartRow, iColumn++] = "Branch";
                                worksheet.Cells[iStartRow, iColumn++] = "Ecode";
                                worksheet.Cells[iStartRow, iColumn++] = "Emp Name";
                                worksheet.Cells[iStartRow, iColumn++] = "Desig";
                                worksheet.Cells[iStartRow, iColumn++] = "PMD";
                                worksheet.Cells[iStartRow, iColumn++] = "DA Days";
                                worksheet.Cells[iStartRow, iColumn++] = "Details Of Reporting Deviations";
                                worksheet.Cells[iStartRow, iColumn++] = "Groups";
                                worksheet.Cells[iStartRow, iColumn++] = "Total PMD";
                                worksheet.Cells[iStartRow, iColumn++] = "Single";
                                worksheet.Cells[iStartRow, iColumn++] = "Disc&Bulk";
                                worksheet.Cells[iStartRow, iColumn++] = "Total";
                                worksheet.Cells[iStartRow, iColumn++] = "Group points Incl per";
                                worksheet.Cells[iStartRow, iColumn++] = "Revenue";
                                worksheet.Cells[iStartRow, iColumn++] = "Yes/No";
                                worksheet.Cells[iStartRow, iColumn++] = "Petrol Rate";
                                worksheet.Cells[iStartRow, iColumn++] = "Avg PMD (P/G)";
                                worksheet.Cells[iStartRow, iColumn++] = "Revenue(P/H)";
                                worksheet.Cells[iStartRow, iColumn++] = "Points(P/G)";
                                worksheet.Cells[iStartRow, iColumn++] = "Points(P/H)";
                                worksheet.Cells[iStartRow, iColumn++] = " Group Points exc per";
                                worksheet.Cells[iStartRow, iColumn++] = "Points";
                                worksheet.Cells[iStartRow, iColumn++] = "Cust";
                                iStartRow++; iColumn = 1;
                                for (int i = 0; i < dtExcel.Rows.Count; i++)
                                {

                                    worksheet.Cells[iStartRow, iColumn++] = (i + 1).ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_doc_month"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_branch_name"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_eora_code"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_eora_name"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_desg_name"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_pers_pmd"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_da_days"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_report_deviations"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_groups"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_group_pmd"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_single_pts"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_disc_bulk_pts"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_Totl_disc_bulk_pts"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_group_pts"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_revenue"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_vehicle_status"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_petrol_rate"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_avg_pmd_pg"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_revenue_ph"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_points_pg"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_points_ph"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_exclus_group_pts"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_fieldsupp_pts"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_fieldsupph"].ToString();

                                    iStartRow++; iColumn = 1;


                                }
                            }

                            else
                            {
                                MessageBox.Show("No Data Found!", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        if (cbSortBy.SelectedIndex == 1)
                        {
                            dtExcel = objExDb.GetStmtofFieldSupportAndDeviations(Company, Branches, dtpFromDoc.Value.ToString("MMMyyyy").ToUpper(), dtpToDoc.Value.ToString("MMMyyyy").ToUpper(), Convert.ToInt32(txtFrmGrpPerMnth.Text), Convert.ToInt32(txtToGrpPerMnth.Text), cbSortBy.Text.ToString()).Tables[0];
                            objExDb = null;

                            if (dtExcel.Rows.Count > 0)
                            {
                                Excel.Application oXL = new Excel.Application();
                                Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                                Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                                oXL.Visible = true;

                                int iTotColumns = 0;
                                iTotColumns = 20;
                                string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                                Excel.Range rgHead = null;
                                Excel.Range rg = worksheet.get_Range("A4", sLastColumn + "4");
                                Excel.Range rgData = worksheet.get_Range("A5", sLastColumn + (dtExcel.Rows.Count + 4).ToString());
                                rgData.Font.Size = 11;
                                rgData.WrapText = true;
                                rgData.VerticalAlignment = Excel.Constants.xlCenter;
                                rgData.Borders.Weight = 2;

                                rgData = worksheet.get_Range("A1", "H2");
                                rgData.Merge(Type.Missing);
                                rgData.Font.Bold = true; rgData.Font.Size = 16;
                                rgData.Value2 = "STATEMENT OF FIELD SUPPORT AND REPORTING DEVIATIONS [DBM_SR.BM 4-10 GROUPS]";

                                //rgData.Value2 = "STATEMENT OF FIELD SUPPORT AND REPORTING DEVIATIONS '+/n+' [GC-ABM 1-3 GROUPS]";
                                rgData.VerticalAlignment = Excel.Constants.xlCenter;
                                rgData.HorizontalAlignment = Excel.Constants.xlCenter;

                                rg.Font.Bold = true;
                                rg.Font.Name = "Times New Roman";
                                rg.Font.Size = 10;
                                rg.WrapText = true;
                                rg.Font.ColorIndex = 2; // White Color : 2 and Red = 3,30; Green = 10,43; 
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.Interior.ColorIndex = 31;
                                rg.Borders.Weight = 2;
                                rg.Borders.LineStyle = Excel.Constants.xlSolid;
                                rg.Cells.RowHeight = 38;

                                rg = worksheet.get_Range("A4", Type.Missing);
                                rg.Cells.ColumnWidth = 6;
                                rg = worksheet.get_Range("B4", Type.Missing);
                                rg.Cells.ColumnWidth = 8;
                                rg = worksheet.get_Range("C4", Type.Missing);
                                rg.Cells.ColumnWidth = 20;
                                rg = worksheet.get_Range("D4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("E4", Type.Missing);
                                rg.Cells.ColumnWidth = 30;
                                rg = worksheet.get_Range("F3", "H3");
                                rg.Merge(Type.Missing);
                                rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                                rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                                rg.Value2 = "ACTUAL DESIG&SALARY";
                                rg = worksheet.get_Range("F4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("G4", Type.Missing);
                                rg.Cells.ColumnWidth = 8;
                                rg = worksheet.get_Range("H4", Type.Missing);
                                rg.Cells.ColumnWidth = 8;
                                rg = worksheet.get_Range("I4", Type.Missing);
                                rg.Cells.ColumnWidth = 25;
                                rg = worksheet.get_Range("J4", Type.Missing);
                                rg.Cells.ColumnWidth = 8;
                                rg = worksheet.get_Range("K4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("L4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("M4", Type.Missing);
                                rg.Cells.ColumnWidth = 15;

                                rg = worksheet.get_Range("N3", "O3");
                                rg.Merge(Type.Missing);
                                rg.Value2 = "VEHICLE STATUS";
                                rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                                rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;

                                rg = worksheet.get_Range("N4", Type.Missing);
                                rg.Cells.ColumnWidth = 6;
                                rg = worksheet.get_Range("O4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;

                                rg = worksheet.get_Range("P4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("Q4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;

                                rg = worksheet.get_Range("R4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;



                                rg = worksheet.get_Range("S3", "T3");
                                rg.Merge(Type.Missing);
                                rg.Value2 = "FIELD SUPPORT";
                                rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                                rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;

                                rg = worksheet.get_Range("S4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("T4", Type.Missing);
                                rg.Cells.ColumnWidth = 8;


                                int iColumn = 1, iStartRow = 4;
                                worksheet.Cells[iStartRow, iColumn++] = "SlNo";
                                worksheet.Cells[iStartRow, iColumn++] = "Doc month";
                                worksheet.Cells[iStartRow, iColumn++] = "Branch";
                                worksheet.Cells[iStartRow, iColumn++] = "Ecode";
                                worksheet.Cells[iStartRow, iColumn++] = "Emp Name";
                                worksheet.Cells[iStartRow, iColumn++] = "Desig";
                                worksheet.Cells[iStartRow, iColumn++] = "PMD";
                                worksheet.Cells[iStartRow, iColumn++] = "DA Days";
                                worksheet.Cells[iStartRow, iColumn++] = "Details Of Reporting Deviations";
                                worksheet.Cells[iStartRow, iColumn++] = "Groups";
                                worksheet.Cells[iStartRow, iColumn++] = "Total PMD";
                                worksheet.Cells[iStartRow, iColumn++] = "Total Points";
                                worksheet.Cells[iStartRow, iColumn++] = "Revenue";
                                worksheet.Cells[iStartRow, iColumn++] = "Yes/No";
                                worksheet.Cells[iStartRow, iColumn++] = "Petrol Rate";
                                worksheet.Cells[iStartRow, iColumn++] = "Avg PMD (P/G)";
                                worksheet.Cells[iStartRow, iColumn++] = "Points(P/G)";
                                worksheet.Cells[iStartRow, iColumn++] = "Points(P/H)";
                                worksheet.Cells[iStartRow, iColumn++] = "Points";
                                worksheet.Cells[iStartRow, iColumn++] = "Cust";
                                iStartRow++; iColumn = 1;
                                for (int i = 0; i < dtExcel.Rows.Count; i++)
                                {

                                    worksheet.Cells[iStartRow, iColumn++] = (i + 1).ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_doc_month"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_branch_name"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_eora_code"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_eora_name"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_desg_name"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_pers_pmd"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_da_days"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_report_deviations"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_groups"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_group_pmd"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_group_pts"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_revenue"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_vehicle_status"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_petrol_rate"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_avg_pmd_pg"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_points_pg"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_points_ph"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_fieldsupp_pts"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["br_fieldsupph"].ToString();
                                    iStartRow++; iColumn = 1;


                                }
                            }

                            else
                            {
                                MessageBox.Show("No Data Found!", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                #endregion

                #region "IformType :: RecruitmentVSRetained_SRs"

                if (IformType == "RECRUITMENT_VS_RETAINED_SRS")
                {
                    try
                    {

                        objExDb = new ExcelDB();
                        objUtilityDB = new UtilityDB();
                        DataTable dtExcel = new DataTable();
                        dtExcel = objExDb.Get_Recruitment_vs_RetainedSRs(Company, Branches, dtpFromDoc.Value.ToString("MMMyyyy").ToUpper(), dtpToDoc.Value.ToString("MMMyyyy").ToUpper(), Convert.ToInt32(txtFrmGrpPerMnth.Text), Convert.ToInt32(txtToGrpPerMnth.Text), Convert.ToInt32(txtToGrps.Text), "").Tables[0];

                        objExDb = null;

                        if (dtExcel.Rows.Count > 0)
                        {
                            Excel.Application oXL = new Excel.Application();
                            Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                            Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                            oXL.Visible = true;

                            int iTotColumns = 0;
                            iTotColumns = 15;
                            string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                            Excel.Range rgHead = null;
                            Excel.Range rg = worksheet.get_Range("A4", sLastColumn + "4");
                            Excel.Range rgData = worksheet.get_Range("A5", sLastColumn + (dtExcel.Rows.Count + 4).ToString());
                            rgData.Font.Size = 11;
                            rgData.WrapText = true;
                            rgData.VerticalAlignment = Excel.Constants.xlCenter;
                            rgData.Borders.Weight = 2;

                            rgData = worksheet.get_Range("A1", "O1");
                            rgData.Merge(Type.Missing);
                            rgData.Font.Bold = true; rgData.Font.Size = 16;
                            rgData.Value2 = "RECRUITMENT ANALYSIS - " + dtpFromDoc.Value.ToString("MMMyyyy").ToUpper() + "  To \t " + dtpToDoc.Value.ToString("MMMyyyy").ToUpper() + " ";
                            rgData.VerticalAlignment = Excel.Constants.xlCenter;
                            rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                            rgData.Cells.RowHeight = 30;

                            rg.Font.Bold = true;
                            rg.Font.Name = "Times New Roman";
                            rg.Font.Size = 10;
                            rg.WrapText = true;
                            rg.Font.ColorIndex = 2; // White Color : 2 and Red = 3,30; Green = 10,43; 
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.Interior.ColorIndex = 31;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;
                            rg.Cells.RowHeight = 42;

                            rg = worksheet.get_Range("A4:A2", Type.Missing);
                            rg.Cells.ColumnWidth = 4; rg.Merge(Type.Missing);
                            rg.Cells.Value2 = "Sl.No";
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;

                            rg = worksheet.get_Range("B4:B2", Type.Missing);
                            rg.Cells.ColumnWidth = 8; rg.Merge(Type.Missing);
                            rg.Cells.Value2 = "Ecode";
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;

                            rg = worksheet.get_Range("C4:C2", Type.Missing);
                            rg.Cells.ColumnWidth = 30; rg.Merge(Type.Missing);
                            rg.Cells.Value2 = "Emp Name"; rg.WrapText = true;
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;

                            rg = worksheet.get_Range("D4:D2", Type.Missing);
                            rg.Cells.ColumnWidth = 20; rg.Merge(Type.Missing);
                            rg.Cells.Value2 = "Place"; rg.WrapText = true;

                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;

                            rg = worksheet.get_Range("E4:E2", Type.Missing);
                            rg.Cells.ColumnWidth = 6; rg.Merge(Type.Missing);
                            rg.Cells.Value2 = "Grps P/M"; rg.WrapText = true;
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;


                            rg = worksheet.get_Range("F2", "O2");
                            rg.Merge(Type.Missing);
                            rg.Value2 = "RETENTION";
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;

                            rg = worksheet.get_Range("F3", "J3");
                            rg.Merge(Type.Missing);
                            rg.Value2 = "Total";
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;

                            rg = worksheet.get_Range("F4", Type.Missing);
                            rg.Cells.ColumnWidth = 8;
                            rg.Cells.Value2 = "Recruited";
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;

                            rg = worksheet.get_Range("G4", Type.Missing);
                            rg.Cells.ColumnWidth = 8; rg.Cells.Value2 = "P/G";
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;

                            rg = worksheet.get_Range("H4", Type.Missing);
                            rg.Cells.ColumnWidth = 8; rg.Cells.Value2 = "Retain";
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;

                            rg = worksheet.get_Range("I4", Type.Missing);
                            rg.Cells.ColumnWidth = 8; rg.Cells.Value2 = "%";
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;

                            rg = worksheet.get_Range("J4", Type.Missing);
                            rg.Cells.ColumnWidth = 8; rg.Cells.Value2 = "Points P/SR";
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;

                            rg = worksheet.get_Range("K3", "O3");
                            rg.Merge(Type.Missing);
                            rg.Value2 = "Successed SRs >= " + txtToGrps.Text;
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg = worksheet.get_Range("K4", Type.Missing);
                            rg.Cells.ColumnWidth = 8; rg.Cells.Value2 = "No's";
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;

                            rg = worksheet.get_Range("L4", Type.Missing);
                            rg.Cells.ColumnWidth = 8; rg.Cells.Value2 = "%Suc. SRs";
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;

                            rg = worksheet.get_Range("M4", Type.Missing);
                            rg.Cells.ColumnWidth = 8; rg.Cells.Value2 = "Suc. SRs.Ret";
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;

                            rg = worksheet.get_Range("N4", Type.Missing);
                            rg.Cells.ColumnWidth = 8; rg.Cells.Value2 = "% Of Suc. SRs Ret on Total";
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;

                            rg = worksheet.get_Range("O4", Type.Missing);
                            rg.Cells.ColumnWidth = 8; rg.Cells.Value2 = "Points P/SR";
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;

                            Int32 iStartRow = 5, iColumn = 1;

                            for (int i = 0; i < dtExcel.Rows.Count; i++)
                            {

                                worksheet.Cells[iStartRow, iColumn++] = (i + 1).ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_GL_Ecode"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_GL_Name"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Branch_Name"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Grps_Per_Month"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Recruited_SRs"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Points_Per_Grp"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Retained_SRs"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Rec_Vs_Ret_Perc"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Points_Per_SR"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Succ_SRs"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Succ_SRs_Perc"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_SucSRs_Retain"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_SucSRs_Retain_Perc"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Retain_SR_Pnts_Per_SR"].ToString();

                                iStartRow++; iColumn = 1;

                            }
                        }

                        else
                        {
                            MessageBox.Show("No Data Found!", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                #endregion

                #region "IformType :: All India Toppers"

                if (IformType == "ALL_INDIA_TOP")
                {
                    try
                    {
                        DataTable dtExcel = new DataTable();
                        objExDb = new ExcelDB();
                        objUtilityDB = new UtilityDB();
                        GetReportType();

                        if (strReportType == "PER_MONTH_TOTAL_GROUP_WISE")
                        {
                            dtExcel = objExDb.GetAllIndiaToppersForAwards(Company, Branches, "", "", "", Convert.ToInt32(txtFrmGrpPerMnth.Text), Convert.ToInt32(txtToGrpPerMnth.Text), Convert.ToInt32(txtFrmGrps.Text), Convert.ToInt32(txtToGrps.Text), Convert.ToInt32(txtNoofRecords.Text), dtpFromDoc.Value.ToString("MMMyyyy").ToUpper(), dtpToDoc.Value.ToString("MMMyyyy").ToUpper(), "PER_MONTH_TOTAL_GROUP_WISE").Tables[0];
                        }
                        if (strReportType == "PER_MONTH_TOTAL_GROUP_PERSONAL_WISE")
                        {
                            dtExcel = objExDb.GetAllIndiaToppersForAwards(Company, Branches, "", "", "", Convert.ToInt32(txtFrmGrpPerMnth.Text), Convert.ToInt32(txtToGrpPerMnth.Text), Convert.ToInt32(txtFrmGrps.Text), Convert.ToInt32(txtToGrps.Text), Convert.ToInt32(txtNoofRecords.Text), dtpFromDoc.Value.ToString("MMMyyyy").ToUpper(), dtpToDoc.Value.ToString("MMMyyyy").ToUpper(), "PER_MONTH_TOTAL_GROUP_PERSONAL_WISE").Tables[0];

                        }
                        if (strReportType == "TOTAL_GROUP_PERSONAL_WISE")
                        {
                            dtExcel = objExDb.GetAllIndiaToppersForAwards(Company, Branches, "", "", "", 0, 0, Convert.ToInt32(txtFrmGrps.Text), Convert.ToInt32(txtToGrps.Text), Convert.ToInt32(txtNoofRecords.Text), dtpFromDoc.Value.ToString("MMMyyyy").ToUpper(), dtpToDoc.Value.ToString("MMMyyyy").ToUpper(), "TOTAL_GROUP_PERSONAL_WISE").Tables[0];
                        }
                        if (strReportType == "TOTAL_GROUP_WISE")
                        {
                            dtExcel = objExDb.GetAllIndiaToppersForAwards(Company, Branches, "", "", "", 0, 0, Convert.ToInt32(txtFrmGrps.Text), Convert.ToInt32(txtToGrps.Text), Convert.ToInt32(txtNoofRecords.Text), dtpFromDoc.Value.ToString("MMMyyyy").ToUpper(), dtpToDoc.Value.ToString("MMMyyyy").ToUpper(), "TOTAL_GROUP_WISE").Tables[0];
                        }

                        objExDb = null;

                        if (dtExcel.Rows.Count > 0)
                        {
                            Excel.Application oXL = new Excel.Application();
                            Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                            Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                            oXL.Visible = true;

                            int iTotColumns = 0;
                            iTotColumns = 16;
                            string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                            Excel.Range rgHead = null;
                            Excel.Range rg = worksheet.get_Range("A4", sLastColumn + "4");
                            Excel.Range rgData = worksheet.get_Range("A5", sLastColumn + (dtExcel.Rows.Count + 4).ToString());
                            rgData.Font.Size = 11;
                            rgData.WrapText = true;
                            rgData.VerticalAlignment = Excel.Constants.xlCenter;
                            rgData.Borders.Weight = 2;

                            rgData = worksheet.get_Range("A1", "H3");
                            rgData.Merge(Type.Missing);
                            rgData.Font.Bold = true; rgData.Font.Size = 16;
                            rgData.Value2 = "ALL INDIA TOP " + txtNoofRecords.Text.ToString() + "  MEMBERS ";
                            rgData.VerticalAlignment = Excel.Constants.xlCenter;
                            rgData.HorizontalAlignment = Excel.Constants.xlCenter;

                            rg.Font.Bold = true;
                            rg.Font.Name = "Times New Roman";
                            rg.Font.Size = 10;
                            rg.WrapText = true;
                            rg.Font.ColorIndex = 2; // White Color : 2 and Red = 3,30; Green = 10,43; 
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.Interior.ColorIndex = 31;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;
                            rg.Cells.RowHeight = 38;

                            rg = worksheet.get_Range("A4", Type.Missing);
                            rg.Cells.ColumnWidth = 4;
                            rg = worksheet.get_Range("B4", Type.Missing);
                            rg.Cells.ColumnWidth = 13;
                            rg = worksheet.get_Range("C4", Type.Missing);
                            rg.Cells.ColumnWidth = 8;
                            rg = worksheet.get_Range("D4", Type.Missing);
                            rg.Cells.ColumnWidth = 25;
                            rg = worksheet.get_Range("E4", Type.Missing);
                            rg.Cells.ColumnWidth = 12;
                            rg = worksheet.get_Range("F4", Type.Missing);
                            rg.Cells.ColumnWidth = 20;
                            rg = worksheet.get_Range("G4", Type.Missing);
                            rg.Cells.ColumnWidth = 15;
                            rg = worksheet.get_Range("H4", Type.Missing);
                            rg.Cells.ColumnWidth = 6;
                            rg = worksheet.get_Range("I3", "K3");
                            rg.Merge(Type.Missing);
                            rg.Value2 = "PERSONAL";
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg = worksheet.get_Range("I4", Type.Missing);
                            rg.Cells.ColumnWidth = 6;
                            rg = worksheet.get_Range("J4", Type.Missing);
                            rg.Cells.ColumnWidth = 6;
                            rg = worksheet.get_Range("K4", Type.Missing);
                            rg.Cells.ColumnWidth = 9;

                            rg = worksheet.get_Range("L3", "P3");
                            rg.Merge(Type.Missing);
                            rg.Value2 = "GROUP";
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg = worksheet.get_Range("L4", Type.Missing);
                            rg.Cells.ColumnWidth = 6;
                            rg = worksheet.get_Range("M4", Type.Missing);
                            rg.Cells.ColumnWidth = 6;
                            rg = worksheet.get_Range("N4", Type.Missing);
                            rg.Cells.ColumnWidth = 8;
                            rg = worksheet.get_Range("O4", Type.Missing);
                            rg.Cells.ColumnWidth = 6;
                            rg = worksheet.get_Range("P4", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            //rg = worksheet.get_Range("Q4", Type.Missing);
                            //rg.Cells.ColumnWidth = 20;


                            int iColumn = 1, iStartRow = 4;
                            worksheet.Cells[iStartRow, iColumn++] = "Rank";
                            worksheet.Cells[iStartRow, iColumn++] = "Emp Pic";
                            worksheet.Cells[iStartRow, iColumn++] = "Ecode";
                            worksheet.Cells[iStartRow, iColumn++] = "Emp Name";
                            worksheet.Cells[iStartRow, iColumn++] = "Position";
                            worksheet.Cells[iStartRow, iColumn++] = "State";
                            worksheet.Cells[iStartRow, iColumn++] = "Branch";
                            // worksheet.Cells[iStartRow, iColumn++] = "Region";                            
                            worksheet.Cells[iStartRow, iColumn++] = "Groups(P/M)";
                            worksheet.Cells[iStartRow, iColumn++] = "No.Of Months";
                            worksheet.Cells[iStartRow, iColumn++] = "PMD";
                            worksheet.Cells[iStartRow, iColumn++] = "Total Points";
                            worksheet.Cells[iStartRow, iColumn++] = "No.Of Months";
                            worksheet.Cells[iStartRow, iColumn++] = "PMD";
                            worksheet.Cells[iStartRow, iColumn++] = "Total Points";
                            worksheet.Cells[iStartRow, iColumn++] = "Groups";
                            worksheet.Cells[iStartRow, iColumn++] = "Total Points(P/G)";


                            iStartRow++; iColumn = 1;
                            for (int i = 0; i < dtExcel.Rows.Count; i++)
                            {
                                string strImgFile = "";

                                worksheet.Cells[iStartRow, iColumn++] = (i + 1).ToString();

                                if (dtExcel.Rows[i]["al_Photo"].ToString() != "")
                                {
                                    strImgFile = byteArrayToImage((byte[])dtExcel.Rows[i]["al_Photo"]);

                                    Excel.Range CellRange = (Excel.Range)worksheet.Cells[iStartRow, iColumn++];
                                    float Left = (float)((double)CellRange.Left);
                                    float Top = (float)((double)CellRange.Top);
                                    float ImageSize = 65;
                                    worksheet.Shapes.AddPicture(strImgFile, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left + 2, Top + 2, ImageSize, ImageSize);
                                    CellRange.RowHeight = ImageSize + 2;
                                }
                                else
                                {
                                    worksheet.Cells[iStartRow, iColumn++] = "";
                                }

                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["al_eora_code"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["al_eora_name"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["al_category"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["al_state_name"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["al_branch_Name"].ToString();
                                //worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["al_region_name"].ToString();                                
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["al_grps_per_month"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["al_pers_work_months"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["al_pers_pmd"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["al_pers_points"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["al_group_work_months"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["al_group_pmd"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["al_group_points"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["al_groups"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["al_avg_points"].ToString();


                                iStartRow++; iColumn = 1;

                                if (strImgFile.Length > 0)
                                {
                                    File.Delete(strImgFile);
                                }

                            }
                        }

                        else
                        {
                            MessageBox.Show("No Data Found!", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                #endregion

                #region " IformType :: Group Sales Analysation"

                if (IformType == "SALES_ANALYSIS")
                {
                    Get_GroupRangeValue();

                    DataTable dtExcel = new DataTable();
                    objExDb = new ExcelDB();
                    objUtilityDB = new UtilityDB();
                    dtExcel = objExDb.Get_GroupsSalesAnalysation(Company, Branches, dtpFromDoc.Value.ToString("MMMyyyy").ToUpper(), dtpToDoc.Value.ToString("MMMyyyy").ToUpper(), strGrpRange, Convert.ToInt32(txtFrmGrpPerMnth.Text), Convert.ToInt32(txtToGrpPerMnth.Text), "EXCEL_DOWNLOAD").Tables[0];
                    objExDb = null;
                    if (dtExcel.Rows.Count > 0)
                    {
                        try
                        {
                            Excel.Application oXL = new Excel.Application();
                            Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                            Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                            oXL.Visible = true;
                            int iTotColumns = 0;
                            iTotColumns = 3 + (8 * Convert.ToInt32(dtExcel.Rows[0]["sr_No_Of_Ranges"]));
                            string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                            Excel.Range rg = worksheet.get_Range("A3", sLastColumn + "3");
                            Excel.Range rgData = worksheet.get_Range("A3", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["sr_No_Of_Pers"]) + 3).ToString());
                            rgData.Font.Size = 11;
                            rgData.WrapText = true;
                            rgData.VerticalAlignment = Excel.Constants.xlCenter;
                            rgData.Borders.Weight = 2;


                            rg.Font.Bold = true;
                            rg.Font.Name = "Times New Roman";
                            rg.Font.Size = 10;
                            rg.WrapText = true;
                            rg.Font.ColorIndex = 2; // White Color : 2 and Red = 3,30; Green = 10,43; 
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Interior.ColorIndex = 31;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;
                            rg.Cells.RowHeight = 38;
                            rgData = worksheet.get_Range("A4", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["sr_No_Of_Pers"]) + 3).ToString());
                            rgData.WrapText = false;
                            rg = worksheet.get_Range("A3", Type.Missing);
                            rg.Cells.ColumnWidth = 4;
                            rg = worksheet.get_Range("B3", Type.Missing);
                            rg.Cells.ColumnWidth = 8;
                            rg = worksheet.get_Range("C3", Type.Missing);
                            rg.Cells.ColumnWidth = 35;
                            rg.WrapText = true;


                            int iColumn = 1;
                            worksheet.Cells[3, iColumn++] = "SlNo";
                            worksheet.Cells[3, iColumn++] = "Ecode";
                            worksheet.Cells[3, iColumn++] = "Employee Name";

                            Excel.Range rgHead;
                            int iStartColumn = 0;
                            for (int iProd = 0; iProd < Convert.ToInt32(dtExcel.Rows[0]["sr_No_Of_Ranges"]); iProd++)
                            {
                                rgHead = worksheet.get_Range("A1", "K1");
                                rgHead.Merge(Type.Missing);
                                rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Cells.RowHeight = 30;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.Value2 = "GROUPS SALES ANALYSATION \t FROM  " + (dtpFromDoc.Value).ToString("MMMyyyy").ToUpper() + " \t  TO  " + (dtpToDoc.Value).ToString("MMMyyyy").ToUpper() + " ";


                                iStartColumn = (8 * iProd) + 4;

                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "2", objUtilityDB.GetColumnName(iStartColumn + 7) + "2");


                                rgHead.Merge(Type.Missing);
                                rgHead.Interior.ColorIndex = 34 + 1;
                                rgHead.Borders.Weight = 2;
                                rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
                                rgHead.Cells.RowHeight = 30;
                                rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;


                                worksheet.Cells[3, iStartColumn++] = "Groups";
                                worksheet.Cells[3, iStartColumn++] = "Groups P/M";
                                worksheet.Cells[3, iStartColumn++] = "Pers PMD";
                                worksheet.Cells[3, iStartColumn++] = "Pers Points";
                                worksheet.Cells[3, iStartColumn++] = "Tot Avg PMD";
                                worksheet.Cells[3, iStartColumn++] = "P/H";
                                worksheet.Cells[3, iStartColumn++] = "P/G";
                                worksheet.Cells[3, iStartColumn++] = "% Of Business";


                            }


                            int iRowCounter = 4; int iColumnCounter = 1;
                            for (int i = 0; i < dtExcel.Rows.Count; i++)
                            {
                                if (i > 0)
                                {

                                    if (dtExcel.Rows[i]["sr_GL_Ecode"].ToString() == dtExcel.Rows[i - 1]["sr_GL_Ecode"].ToString())
                                    {
                                        int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sr_Sort_No"]);
                                        //int iStartColumn = 0;
                                        iColumnCounter = (8 * (iMonthNo - 1)) + 4;
                                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 7) + "2");
                                        rgHead.Cells.Value2 = dtExcel.Rows[i]["sr_Sort_Order"];
                                        rgHead.WrapText = true;

                                        rgHead.Interior.ColorIndex = 34 + iMonthNo;
                                        rgHead.Font.ColorIndex = 1;
                                        rgHead.Font.Bold = true;
                                        rgHead.Borders.Weight = 2;
                                        //rgHead.Interior.ColorIndex = 31;
                                        //rgHead.Font.ColorIndex = 2;
                                        rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                        rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;


                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_Groups"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToDouble(dtExcel.Rows[i]["sr_Grps_Per_Mnth"]).ToString("0");
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_Pers_PMD"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_Pers_Points"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_Avg_PMD_Per_Grp"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_Pnts_Per_Head"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_Pnts_Per_Grp"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_Pers_Of_Business"];
                                    }

                                    else
                                    {

                                        iRowCounter++;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 3;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_GL_Ecode"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_GL_Name"];

                                        int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sr_Sort_No"]);

                                        iColumnCounter = (8 * (iMonthNo - 1)) + 4;
                                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 7) + "2");
                                        rgHead.Cells.Value2 = dtExcel.Rows[i]["sr_Sort_Order"];
                                        rgHead.WrapText = true;

                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_Groups"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToDouble(dtExcel.Rows[i]["sr_Grps_Per_Mnth"]).ToString("0");
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_Pers_PMD"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_Pers_Points"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_Avg_PMD_Per_Grp"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_Pnts_Per_Head"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_Pnts_Per_Grp"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_Pers_Of_Business"];


                                    }
                                }
                                else
                                {

                                    worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 3;
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_GL_Ecode"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_GL_Name"];

                                    int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sr_Sort_No"]);
                                    //int iStartColumn = 0;
                                    iColumnCounter = (8 * (iMonthNo - 1)) + 4;
                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 7) + "2");
                                    rgHead.Cells.Value2 = dtExcel.Rows[i]["sr_Sort_Order"];
                                    rgHead.WrapText = true;
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_Groups"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToDouble(dtExcel.Rows[i]["sr_Grps_Per_Mnth"]).ToString("0");
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_Pers_PMD"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_Pers_Points"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_Avg_PMD_Per_Grp"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_Pnts_Per_Head"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_Pnts_Per_Grp"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_Pers_Of_Business"];

                                }

                                iColumnCounter = 1;
                            }




                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }

                #endregion


                #region "IformType :: Sales Emp LIst"

                if (IformType == "SALES_EMP_LIST")
                {
                    if (cbSortBy.SelectedIndex == 0)
                    {
                        try
                        {

                            objExDb = new ExcelDB();
                            objUtilityDB = new UtilityDB();
                            DataTable dtExcel = new DataTable();
                            dtExcel = objExDb.Get_SalesEmpList(Company, Branches, dtpFromDoc.Value.ToString("MMMyyyy").ToUpper(), Convert.ToInt32(txtFrmGrpPerMnth.Text), Convert.ToInt32(txtToGrpPerMnth.Text), Convert.ToInt32(cbFrmDesg.SelectedValue), Convert.ToInt32(cbToDesigId.SelectedValue), Convert.ToDateTime(dtpLOSAsOnDate.Value).ToString("dd/MMM/yyyy"), "DETAILED").Tables[0];

                            objExDb = null;

                            if (dtExcel.Rows.Count > 0)
                            {
                                Excel.Application oXL = new Excel.Application();
                                Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                                Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                                oXL.Visible = true;

                                int iTotColumns = 0;
                                iTotColumns = 11;
                                string sLastColumn = objUtilityDB.GetColumnName(iTotColumns+1);
                                Excel.Range rgHead = null;
                                Excel.Range rg = worksheet.get_Range("A3", sLastColumn + "3");
                                Excel.Range rgData = worksheet.get_Range("A4", sLastColumn + (dtExcel.Rows.Count + 3).ToString());
                                rgData.Font.Size = 11;
                                rgData.WrapText = true;
                                rgData.VerticalAlignment = Excel.Constants.xlCenter;
                                rgData.Borders.Weight = 2;

                                rgData = worksheet.get_Range("A1", "L1");
                                rgData.Merge(Type.Missing);
                                rgData.Font.Bold = true; rgData.Font.Size = 16;
                                rgData.Value2 = Company + " Sales Employees List - " + dtpFromDoc.Value.ToString("MMMyyyy").ToUpper() + "";
                                rgData.VerticalAlignment = Excel.Constants.xlCenter;
                                rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgData.Cells.RowHeight = 25;

                                rgData = worksheet.get_Range("A2", "L2");
                                rgData.Merge(Type.Missing);
                                rgData.Font.Bold = true; rgData.Font.Size = 16;
                                rgData.Value2 = "Groups Range " + txtFrmGrpPerMnth.Text + " To  " + txtToGrpPerMnth.Text + "";
                                rgData.VerticalAlignment = Excel.Constants.xlCenter;
                                rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgData.Cells.RowHeight = 25;

                                rg.Font.Bold = true;
                                rg.Font.Name = "Times New Roman";
                                rg.Font.Size = 10;
                                rg.WrapText = true;
                                rg.Font.ColorIndex = 2; // White Color : 2 and Red = 3,30; Green = 10,43; 
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.Interior.ColorIndex = 31;
                                rg.Borders.Weight = 2;
                                rg.Borders.LineStyle = Excel.Constants.xlSolid;
                                rg.Cells.RowHeight = 42;

                                rg.Font.Bold = true;
                                rg.Font.Name = "Times New Roman";
                                rg.Font.Size = 10;
                                rg.WrapText = true;
                                rg.Font.ColorIndex = 2; // White Color : 2 and Red = 3,30; Green = 10,43; 
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.Interior.ColorIndex = 31;
                                rg.Borders.Weight = 2;
                                rg.Borders.LineStyle = Excel.Constants.xlSolid;
                                rg.Cells.RowHeight = 42;

                                rg = worksheet.get_Range("A3", Type.Missing);
                                rg.Cells.ColumnWidth = 5;
                                rg = worksheet.get_Range("B3", Type.Missing);
                                rg.Cells.ColumnWidth = 8;
                                rg = worksheet.get_Range("C3", Type.Missing);
                                rg.Cells.ColumnWidth = 30;
                                rg = worksheet.get_Range("D3", Type.Missing);
                                rg.Cells.ColumnWidth = 12;
                                rg = worksheet.get_Range("E3", Type.Missing);
                                rg.Cells.ColumnWidth = 12;
                                rg = worksheet.get_Range("F3", Type.Missing);
                                rg.Cells.ColumnWidth = 12;
                                rg = worksheet.get_Range("G3", Type.Missing);
                                rg.Cells.ColumnWidth = 12;
                                rg = worksheet.get_Range("H3", Type.Missing);
                                rg.Cells.ColumnWidth = 12;
                                rg = worksheet.get_Range("I3", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("J3", Type.Missing);
                                rg.Cells.ColumnWidth = 15;
                                rg = worksheet.get_Range("K3", Type.Missing);
                                rg.Cells.ColumnWidth = 7;
                                rg = worksheet.get_Range("L3", Type.Missing);
                                rg.Cells.ColumnWidth = 10;

                                int iColumn = 1, iStartRow = 3;
                                worksheet.Cells[iStartRow, iColumn++] = "Sl.No";
                                worksheet.Cells[iStartRow, iColumn++] = "Ecode";
                                worksheet.Cells[iStartRow, iColumn++] = "Name";
                                worksheet.Cells[iStartRow, iColumn++] = "Desig";
                                worksheet.Cells[iStartRow, iColumn++] = "DOJ";
                                worksheet.Cells[iStartRow, iColumn++] = "Total Length Of Service";
                                worksheet.Cells[iStartRow, iColumn++] = "DOP";
                                worksheet.Cells[iStartRow, iColumn++] = "LOS In Present Desig";
                                worksheet.Cells[iStartRow, iColumn++] = "Company";
                                worksheet.Cells[iStartRow, iColumn++] = "Branch";
                                worksheet.Cells[iStartRow, iColumn++] = "Groups";
                                worksheet.Cells[iStartRow, iColumn++] = "ContactNo";

                                iColumn = 1; iStartRow = 4;
                                for (int i = 0; i < dtExcel.Rows.Count; i++)
                                {

                                    worksheet.Cells[iStartRow, iColumn++] = (i + 1).ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Emp_Code"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Emp_Name"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Desig"].ToString();
                                    if (dtExcel.Rows[i]["sr_DOJ"].ToString() != "")
                                        worksheet.Cells[iStartRow, iColumn++] = Convert.ToDateTime(dtExcel.Rows[i]["sr_DOJ"]).ToString("dd/MMM/yyyy");
                                    else
                                        worksheet.Cells[iStartRow, iColumn++] = "";
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Serv_Length"].ToString();
                                    if (dtExcel.Rows[i]["sr_Last_Prm_Date"].ToString() != "")
                                        worksheet.Cells[iStartRow, iColumn++] = Convert.ToDateTime(dtExcel.Rows[i]["sr_Last_Prm_Date"]).ToString("dd/MMM/yyyy");
                                    else
                                        worksheet.Cells[iStartRow, iColumn++] = "";

                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_LOS_In_PresDesig"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_comp_code"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_branch_name"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_groups"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Mobile_No"].ToString();
                                    iStartRow++; iColumn = 1;

                                }

                               
                            }

                            else
                            {
                                MessageBox.Show("No Data Found!", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    else
                    {
                        try
                        {

                            objExDb = new ExcelDB();
                            objUtilityDB = new UtilityDB();
                            DataTable dtExcel = new DataTable();
                            dtExcel = objExDb.Get_SalesEmpList(Company, Branches, dtpFromDoc.Value.ToString("MMMyyyy").ToUpper(), Convert.ToInt32(txtFrmGrpPerMnth.Text), Convert.ToInt32(txtToGrpPerMnth.Text), Convert.ToInt32(cbFrmDesg.SelectedValue), Convert.ToInt32(cbToDesigId.SelectedValue), "", "SUMMARY").Tables[0];

                            objExDb = null;

                            if (dtExcel.Rows.Count > 0)
                            {
                                Excel.Application oXL = new Excel.Application();
                                Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                                Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                                oXL.Visible = true;

                                int iTotColumns = 0;
                                iTotColumns = 19;
                                string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                                Excel.Range rgHead = null;
                                Excel.Range rg = worksheet.get_Range("A3", sLastColumn + "5");
                                Excel.Range rgData = worksheet.get_Range("A6", sLastColumn + (dtExcel.Rows.Count + 5).ToString());
                                rgData.Font.Size = 11;
                                rgData.WrapText = true;
                                rgData.VerticalAlignment = Excel.Constants.xlCenter;
                                rgData.Borders.Weight = 2;

                                rgData = worksheet.get_Range("A1", "S1");
                                rgData.Merge(Type.Missing);
                                rgData.Font.Bold = true; rgData.Font.Size = 16;
                                rgData.Value2 = Company ;
                                rgData.VerticalAlignment = Excel.Constants.xlCenter;
                                rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgData.Cells.RowHeight = 25;


                                rgData = worksheet.get_Range("A2", "S2");
                                rgData.Merge(Type.Missing);
                                rgData.Font.Bold = true; rgData.Font.Size = 16;
                                rgData.Value2 = "GROUPS & MANPOWER - " + dtpFromDoc.Value.ToString("MMMyyyy").ToUpper() + "";
                                rgData.VerticalAlignment = Excel.Constants.xlCenter;
                                rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgData.Cells.RowHeight = 25;

                               
                                rg.Font.Bold = true;
                                rg.Font.Name = "Times New Roman";
                                rg.Font.Size = 10;
                                rg.WrapText = true;
                                rg.Font.ColorIndex = 2; // White Color : 2 and Red = 3,30; Green = 10,43; 
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.Interior.ColorIndex = 31;
                                rg.Borders.Weight = 2;
                                rg.Borders.LineStyle = Excel.Constants.xlSolid;
                                rg.Cells.RowHeight = 42;

                                rg.Font.Bold = true;
                                rg.Font.Name = "Times New Roman";
                                rg.Font.Size = 10;
                                rg.WrapText = true;
                                rg.Font.ColorIndex = 2; // White Color : 2 and Red = 3,30; Green = 10,43; 
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.Interior.ColorIndex = 31;
                                rg.Borders.Weight = 2;
                                rg.Borders.LineStyle = Excel.Constants.xlSolid;
                                rg.Cells.RowHeight = 25;

                                rg = worksheet.get_Range("A5", "A3");
                                rg.Merge(Type.Missing);
                                rg.Value2 = "Sl.No";
                                rg.WrapText = true;
                                rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                                rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                                rg.Cells.ColumnWidth = 3;
                                rg = worksheet.get_Range("B5", "B3");
                                rg.Merge(Type.Missing);
                                rg.Value2 = "Branch";
                                rg.WrapText = true;
                                rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                                rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                                rg.Cells.ColumnWidth = 25;
                                rg = worksheet.get_Range("C5", "C3");
                                rg.Merge(Type.Missing);
                                rg.Value2 = "SR's P/G";
                                rg.WrapText = true;
                                rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                                rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                                rg.Cells.ColumnWidth = 7;

                                rg = worksheet.get_Range("D5", "D3");
                                rg.Merge(Type.Missing);
                                rg.Value2 = "SR's Per GL/MGR";
                                rg.WrapText = true;
                                rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                                rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                                rg.Cells.ColumnWidth = 7;
                                rg = worksheet.get_Range("E3", "S3");
                                rg.Merge(Type.Missing);
                                rg.Value2 = "Groups Range";
                                rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                                rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                                rg = worksheet.get_Range("E5","E4");
                                rg.Merge(Type.Missing);
                                rg.Value2 = "No.Of SR's";
                                rg.WrapText = true;
                                rg.Cells.ColumnWidth = 7;
                                rg = worksheet.get_Range("F4", Type.Missing);
                                rg.Value2 = "GC / GL's";
                                rg.WrapText = true;
                                rg.Cells.ColumnWidth = 7;

                                rg = worksheet.get_Range("F5", Type.Missing);
                                rg.Value2 = "1";
                                rg.WrapText = true;
                                rg.Cells.ColumnWidth = 6;

                                rg = worksheet.get_Range("G4", Type.Missing);
                                rg.Value2 = "TMs";
                                rg.WrapText = true;
                                rg.Cells.ColumnWidth = 6;

                                rg = worksheet.get_Range("G5", Type.Missing);
                                rg.Value2 = "2";
                                rg.WrapText = true;
                                rg.Cells.ColumnWidth = 6;

                                rg = worksheet.get_Range("H4", Type.Missing);
                                rg.Value2 = "ABMs";
                                rg.WrapText = true;
                                rg.Cells.ColumnWidth = 6;

                                rg = worksheet.get_Range("H5", Type.Missing);
                                rg.Value2 = "3";
                                rg.WrapText = true;
                                rg.Cells.ColumnWidth = 6;

                                rg = worksheet.get_Range("I4", Type.Missing);
                                rg.Value2 = "DBMs";
                                rg.WrapText = true;
                                rg.Cells.ColumnWidth = 6;

                                rg = worksheet.get_Range("I5", Type.Missing);
                                rg.Value2 = "4";
                                rg.WrapText = true;
                                rg.Cells.ColumnWidth = 6;

                                rg = worksheet.get_Range("J4", Type.Missing);
                                rg.Value2 = "BMs";
                                rg.WrapText = true;
                                rg.Cells.ColumnWidth = 6;

                                rg = worksheet.get_Range("J5", Type.Missing);
                                rg.Value2 = "5";
                                rg.WrapText = true;
                                rg.Cells.ColumnWidth = 6;

                                rg = worksheet.get_Range("K4", Type.Missing);
                                rg.Value2 = "BMs";
                                rg.WrapText = true;
                                rg.Cells.ColumnWidth = 6;

                                rg = worksheet.get_Range("K5", Type.Missing);
                                rg.Value2 = "6";
                                rg.WrapText = true;
                                rg.Cells.ColumnWidth = 6;

                                rg = worksheet.get_Range("L4", Type.Missing);
                                rg.Value2 = "Sr BM";
                                rg.WrapText = true;
                                rg.Cells.ColumnWidth = 6;

                                rg = worksheet.get_Range("L5", Type.Missing);
                                rg.Value2 = "7 To 10";
                                rg.WrapText = true;
                                rg.Cells.ColumnWidth = 7;

                                rg = worksheet.get_Range("M4", Type.Missing);
                                rg.Value2 = "Dy DSM";
                                rg.WrapText = true;
                                rg.Cells.ColumnWidth = 7;

                                rg = worksheet.get_Range("M5", Type.Missing);
                                rg.Value2 = "11 To 19";
                                rg.WrapText = true;
                                rg.Cells.ColumnWidth = 7;

                                rg = worksheet.get_Range("N4", Type.Missing);
                                rg.Value2 = "DSM";
                                rg.WrapText = true;
                                rg.Cells.ColumnWidth = 7;

                                rg = worksheet.get_Range("N5", Type.Missing);
                                rg.Value2 = "20 To 39";
                                rg.WrapText = true;
                                rg.Cells.ColumnWidth = 7;

                                rg = worksheet.get_Range("O4", Type.Missing);
                                rg.Value2 = "RSMs";
                                rg.WrapText = true;
                                rg.Cells.ColumnWidth = 7;

                                rg = worksheet.get_Range("O5", Type.Missing);
                                rg.Value2 = "40 To 49";
                                rg.WrapText = true;
                                rg.Cells.ColumnWidth = 7;

                                rg = worksheet.get_Range("P4", Type.Missing);
                                rg.Value2 = "RHs";
                                rg.WrapText = true;
                                rg.Cells.ColumnWidth = 7;

                                rg = worksheet.get_Range("P5", Type.Missing);
                                rg.Value2 = "50 To 79";
                                rg.WrapText = true;
                                rg.Cells.ColumnWidth = 10;

                                rg = worksheet.get_Range("Q4", Type.Missing);
                                rg.Value2 = "RHs & Above";
                                rg.WrapText = true;
                                rg.Cells.ColumnWidth = 11;

                                rg = worksheet.get_Range("Q5", Type.Missing);
                                rg.Value2 = "80 & Above";
                                rg.WrapText = true;
                                rg.Cells.ColumnWidth = 11;

                                rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                                rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                                rg = worksheet.get_Range("R5", "R4");
                                rg.Merge(Type.Missing);
                                rg.Value2 = "Total GC-VP";
                                rg.WrapText = true;
                                rg.Cells.ColumnWidth = 7;                               

                                rg = worksheet.get_Range("S5", "S4");
                                rg.Merge(Type.Missing);
                                rg.Value2 = "Grand Total";
                                rg.WrapText = true;
                                rg.Cells.ColumnWidth = 8;
                                                               

                                int iColumn = 1, iStartRow = 6;
                               
                                for (int i = 0; i < dtExcel.Rows.Count; i++)
                                {
                                  
                                    worksheet.Cells[iStartRow, iColumn++] = (i + 1).ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_branch_name"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_NoOf_SRsPer_GRP"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_NoOf_SRsPer_GL"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_NoOf_SRs"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Grps_1"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Grps_2"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Grps_3"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Grps_4"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Grps_5"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Grps_6"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Grps_7To10"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Grps_11To19"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Grps_20To39"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Grps_40To49"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Grps_50To79"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Grps_80_AND_BOVE"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Tot_GC_VP"].ToString();
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Grand_Tot"].ToString();                                    

                                    iStartRow++; iColumn = 1;

                                }

                                iStartRow = 3;
                                iColumn = iStartRow;
                                rgHead = worksheet.get_Range("C" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString(),
                                                        "S" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString());

                                rg = worksheet.get_Range("A" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString(),
                                                        "B" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString());
                                rg.Merge(Type.Missing);
                                rg.Value2 = "ALL INDIA TOTAL";
                                rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 14;
                                rg.Font.ColorIndex = 30;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;

                                rgHead.Borders.Weight = 2;
                                rgHead.Font.Size = 12; rgHead.Font.Bold = true;
                                double TotSRs = 0, TotGLs = 0, TotEmp = 0, TotGCtoVP = 0;

                                for (int j = 0; j < Convert.ToInt32(dtExcel.Rows.Count); j++)
                                {
                                    TotSRs += Convert.ToDouble(dtExcel.Rows[j]["sr_NoOf_SRs"]);
                                    TotGLs += Convert.ToDouble(dtExcel.Rows[j]["sr_Grps_1"]);
                                    TotEmp += Convert.ToDouble(dtExcel.Rows[j]["sr_Grand_Tot"]);
                                    TotGCtoVP += Convert.ToDouble(dtExcel.Rows[j]["sr_Tot_GC_VP"]);

                                    iStartRow = 5; iColumn =3;

                                    if (TotGLs != 0)
                                    {
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = ((TotGLs + TotSRs) / TotGLs).ToString("0.00");
                                    }
                                    iColumn = iColumn + 1;

                                    if (TotGCtoVP != 0)
                                    {
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = (TotSRs / TotGCtoVP).ToString("0.00");
                                    }
                                    iColumn = iColumn + 1;

                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "6:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                    iColumn = iColumn + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "6:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                    iColumn = iColumn + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "6:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                    iColumn = iColumn + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "6:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                    iColumn = iColumn + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "6:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                    iColumn = iColumn + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "6:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                    iColumn = iColumn + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "6:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                    iColumn = iColumn + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "6:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                    iColumn = iColumn + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "6:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                    iColumn = iColumn + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "6:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                    iColumn = iColumn + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "6:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                    iColumn = iColumn + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "6:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                    iColumn = iColumn + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "6:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                    iColumn = iColumn + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "6:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                    iColumn = iColumn + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "6:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";
                                    iColumn = iColumn + 1;
                                }

                            }

                            else
                            {
                                MessageBox.Show("No Data Found!", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }                    

                #endregion
            }
        
        }
       
        private string byteArrayToImage(byte[] byteArray)
        {
            System.Drawing.Image newImage;
            string strFileName ="D:\\EmpPhoto.jpg";
            if (byteArray != null)
            {
                using (MemoryStream stream = new MemoryStream(byteArray))
                {
                    newImage = System.Drawing.Image.FromStream(stream);
                    newImage.Save(strFileName);
                }
                return strFileName;
            }
            else
            {
                return "";
            }
        }

      

        private void txtNoofRecords_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

        }

        private void txtToGrpPerMnth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

        }

        private void txtFrmGrpPerMnth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

        }

        private void txtFrmGrps_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

        }

        private void txtToGrps_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

        }


        private void FillFromDesigComboBox()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            try
            {
                strCmd = "SELECT DISTINCT ldm_elevel_id LevelId,ldm_designations Desig FROM LevelsDesig_mas  "+
                         "INNER JOIN DESIG_MAS ON desig_code=LDM_DESIG_ID "+
                         "WHERE dept_code=1200000 "+
                         "ORDER BY ldm_elevel_id desc";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    cbFrmDesg.DataSource = dt;
                    cbFrmDesg.DisplayMember = "Desig";
                    cbFrmDesg.ValueMember = "LevelId";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void FillTODesigComboBox()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            cbToDesigId.DataSource = null;
            if (cbFrmDesg.SelectedIndex > -1)
            {
                try
                {
                    Int32 nDesgId = Convert.ToInt32(((System.Data.DataRowView)(cbFrmDesg.SelectedItem)).Row.ItemArray[0].ToString());
                    strCmd = "SELECT DISTINCT ldm_elevel_id LevelId,ldm_designations Desig FROM LevelsDesig_mas  " +
                             "INNER JOIN DESIG_MAS ON desig_code=LDM_DESIG_ID " +
                             "WHERE dept_code=1200000  and ldm_elevel_id < = " + nDesgId + "" +
                             "ORDER BY ldm_elevel_id desc";

                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        cbToDesigId.DataSource = dt;
                        cbToDesigId.DisplayMember = "Desig";
                        cbToDesigId.ValueMember = "LevelId";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void cbFrmDesg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFrmDesg.SelectedIndex > -1)
            {
                FillTODesigComboBox();
            }
        }

     
    }
}


        


