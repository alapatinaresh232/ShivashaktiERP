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

namespace SSCRM
{
    public partial class ServiceReportByTrnNo : Form
    {
        SQLDB objSQLdb = null;
        ServiceDeptDB objServiceDB = null;
        private int sForm = 0;
        public ServiceReportByTrnNo()
        {
            InitializeComponent();
        }
        public ServiceReportByTrnNo(int ScreenNo)
        {
            InitializeComponent();
            sForm = ScreenNo;
        }

        private void ServiceReportByTrnNo_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            FillBranchData();
            //FillDocMonthsData();
            //cbReportType.SelectedIndex = 0;
            lblReport.Visible = false;
            cbReportType.Visible = false;
            dtpDocMonth.Value = DateTime.Today;

            gvTransactionDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                 System.Drawing.FontStyle.Regular);

            if (sForm == 1)
            {
                Text = "Farmer Meet";
                cbReportType.SelectedIndex = 2;
            }
            if (sForm == 2)
            {
                Text = "Demo Plots";
                //Demo Plots
                cbReportType.SelectedIndex = 1;
            }
            if (sForm == 3)
            {
                Text = "School Visits";
                //School Visits
                cbReportType.SelectedIndex = 3;
            }

        }

        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd ="SELECT DISTINCT CM_COMPANY_NAME ,CM_COMPANY_CODE " +
                                   " FROM USER_BRANCH " +
                                   " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                   " INNER JOIN COMPANY_MAS ON CM_COMPANY_CODE = COMPANY_CODE " +
                                   " WHERE UB_USER_ID ='" + CommonData.LogUserId +
                                   "' ORDER BY CM_COMPANY_NAME";

                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";
                    row[1] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cbCompany.DataSource = dt;
                    cbCompany.DisplayMember = "CM_COMPANY_NAME";
                    cbCompany.ValueMember = "CM_COMPANY_CODE";
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

        private void FillBranchData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbBranches.DataSource = null;
            try
            {
                if (cbCompany.SelectedIndex > 0)
                {

                    string strCommand = "SELECT DISTINCT BRANCH_NAME,BRANCH_CODE " +
                                         " FROM USER_BRANCH " +
                                         " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                         " WHERE COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() +
                                         "' AND UB_USER_ID ='" + CommonData.LogUserId +
                                         "'and BRANCH_TYPE IN('BR','HO') ORDER BY BRANCH_NAME ASC";

                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbBranches.DataSource = dt;
                    cbBranches.DisplayMember = "BRANCH_NAME";
                    cbBranches.ValueMember = "BRANCH_CODE";
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

        //private void FillDocMonthsData()
        //{
        //    objSQLdb = new SQLDB();
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        if (cbCompany.SelectedIndex > 0)
        //        {
        //            string strCmd = "SELECT document_month FROM document_month " +
        //                            " WHERE company_code='" + cbCompany.SelectedValue.ToString() +
        //                            "' ORDER BY start_date ,end_date";
        //            dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
        //        }

        //        if (dt.Rows.Count > 0)
        //        {
        //            DataRow row = dt.NewRow();
        //            row[0] = "--Select--";                  

        //            dt.Rows.InsertAt(row, 0);

        //            cbDocMonth.DataSource = dt;
        //            cbDocMonth.DisplayMember = "document_month";
        //            cbDocMonth.ValueMember = "document_month";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        objSQLdb = null;
        //        dt = null;
        //    }
        //}

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                FillBranchData();               
            }
        }

        private void FillDemoPlotDetailsToGrid()
        {
            objServiceDB = new ServiceDeptDB();
            DataTable dt = new DataTable();
            string strCompCode = cbCompany.SelectedValue.ToString();
            string strBranchCode = cbBranches.SelectedValue.ToString();
            gvTransactionDetails.Rows.Clear();
            try
            {
                dt = objServiceDB.ServiceDemoPlotsDetails(strCompCode, strBranchCode, Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper(), "SUMMARY").Tables[0];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        gvTransactionDetails.Rows.Add();
                        gvTransactionDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                        gvTransactionDetails.Rows[i].Cells["TrnNo"].Value = dt.Rows[i]["SDPH_TRN_NUMBER"].ToString();
                        gvTransactionDetails.Rows[i].Cells["ConductedBy"].Value = dt.Rows[i]["EmpName"].ToString();
                        gvTransactionDetails.Rows[i].Cells["CampName"].Value = dt.Rows[i]["SDPH_CAMP_NAME"].ToString();
                        gvTransactionDetails.Rows[i].Cells["ProductsCnt"].Value = dt.Rows[i]["ProdCount"].ToString();
                        gvTransactionDetails.Rows[i].Cells["DemosCnt"].Value = "0";
                        gvTransactionDetails.Rows[i].Cells["OthersCnt"].Value = dt.Rows[i]["Farmers"].ToString();
                        gvTransactionDetails.Rows[i].Cells["CompanyStaffCnt"].Value = dt.Rows[i]["CompanyStaff"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objServiceDB = null;
                dt = null;
            }
        }


        private void FillSchoolVisitDetailsToGrid()
        {
            objServiceDB = new ServiceDeptDB();
            DataTable dt = new DataTable();
            string strCompCode = cbCompany.SelectedValue.ToString();
            string strBranchCode = cbBranches.SelectedValue.ToString();
            gvTransactionDetails.Rows.Clear();
            try
            {
                dt = objServiceDB.ServiceSchoolVisitDetails(strCompCode, strBranchCode, Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper(), "SUMMARY").Tables[0];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        gvTransactionDetails.Rows.Add();
                        gvTransactionDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                        gvTransactionDetails.Rows[i].Cells["TrnNo"].Value = dt.Rows[i]["SSVH_TRN_NUMBER"].ToString();
                        gvTransactionDetails.Rows[i].Cells["ConductedBy"].Value = dt.Rows[i]["Conducted_By"].ToString();
                        gvTransactionDetails.Rows[i].Cells["CampName"].Value = dt.Rows[i]["SSVH_SCHOOL_NAME"].ToString();
                        gvTransactionDetails.Rows[i].Cells["ProductsCnt"].Value = dt.Rows[i]["ProdsCount"].ToString();
                        gvTransactionDetails.Rows[i].Cells["DemosCnt"].Value = dt.Rows[i]["SSVP_DEMOS_COUNT"].ToString();
                        gvTransactionDetails.Rows[i].Cells["OthersCnt"].Value = dt.Rows[i]["SSVP_STUDENTS_COUNT"].ToString();
                        gvTransactionDetails.Rows[i].Cells["CompanyStaffCnt"].Value = dt.Rows[i]["CompStaffCnt"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objServiceDB = null;
                dt = null;
            }
        }

        private void FillFaremerMeetingDetailsToGrid()
        {
            objServiceDB = new ServiceDeptDB();
            DataTable dt = new DataTable();
            string strCompCode = cbCompany.SelectedValue.ToString();
            string strBranchCode = cbBranches.SelectedValue.ToString();
            gvTransactionDetails.Rows.Clear();
            try
            {
                dt = objServiceDB.ServiceFarmerMeetingDetails(strCompCode, strBranchCode, Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper(), "SUMMARY").Tables[0];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        gvTransactionDetails.Rows.Add();
                        gvTransactionDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                        gvTransactionDetails.Rows[i].Cells["TrnNo"].Value = dt.Rows[i]["SFMH_TRN_NUMBER"].ToString();
                        gvTransactionDetails.Rows[i].Cells["ConductedBy"].Value = dt.Rows[i]["EmpName"].ToString();
                        gvTransactionDetails.Rows[i].Cells["CampName"].Value = dt.Rows[i]["SFMH_CAMP_NAME"].ToString();
                        gvTransactionDetails.Rows[i].Cells["ProductsCnt"].Value = dt.Rows[i]["ProdCount"].ToString();
                        gvTransactionDetails.Rows[i].Cells["DemosCnt"].Value = dt.Rows[i]["SFMP_DEMOS_COUNT"].ToString();
                        gvTransactionDetails.Rows[i].Cells["OthersCnt"].Value = dt.Rows[i]["SFMP_FARMERS_COUNT"].ToString();
                        gvTransactionDetails.Rows[i].Cells["CompanyStaffCnt"].Value = dt.Rows[i]["CompanyStaff"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objServiceDB = null;
                dt = null;
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

    

        private void gvTransactionDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == gvTransactionDetails.Columns["Print"].Index)
                {
                    if (cbReportType.SelectedIndex == 1)
                    {
                        CommonData.ViewReport = "SSCRM_REP_DEMO_PLOTS_SUMMARY";
                        ReportViewer childReportViewer = new ReportViewer(gvTransactionDetails.Rows[e.RowIndex].Cells["TrnNo"].Value.ToString());
                        childReportViewer.Show();
                    }
                    else if (cbReportType.SelectedIndex == 2)
                    {

                        CommonData.ViewReport = "SSCRM_REP_FARMER_MEETINGS_SUMMARY";
                        ReportViewer childReportViewer = new ReportViewer(gvTransactionDetails.Rows[e.RowIndex].Cells["TrnNo"].Value.ToString());
                        childReportViewer.Show();

                    }
                    else if (cbReportType.SelectedIndex == 3)
                    {

                        CommonData.ViewReport = "SSCRM_REP_SCHOOL_VISITS_SUMMARY";
                        ReportViewer childReportViewer = new ReportViewer(gvTransactionDetails.Rows[e.RowIndex].Cells["TrnNo"].Value.ToString());
                        childReportViewer.Show();

                    }
                }
            }
        }

        private bool CheckData()
        {
            bool flag = true;
            //if (cbReportType.SelectedIndex == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Select Report Type", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    cbReportType.Focus();
            //}
           if (cbCompany.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Company", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCompany.Focus();
            }
            else if (cbBranches.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbBranches.Focus();
            }
           
            return flag;
        }

        private void btnDisplayPromotionDetails_Click(object sender, EventArgs e)
        {
            gvTransactionDetails.Rows.Clear();
            if (CheckData() == true)
            {
                if (cbReportType.SelectedIndex == 1)
                {
                    FillDemoPlotDetailsToGrid();
                }
                else if (cbReportType.SelectedIndex == 2)
                {
                    FillFaremerMeetingDetailsToGrid();
                }
                else if (cbReportType.SelectedIndex == 3)
                {
                    FillSchoolVisitDetailsToGrid();
                }
            }
        }

        private void cbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvTransactionDetails.Rows.Clear();
            //if (cbReportType.SelectedIndex == 1)
            //{
            //    lblReportName.Text = "Demo Plots Details";
            //}
            //else if (cbReportType.SelectedIndex == 2)
            //{
            //    lblReportName.Text = "Farmer Meeting Details";
            //}
            //else if (cbReportType.SelectedIndex == 3)
            //{
            //    lblReportName.Text = "School Visit Details";
            //}
        }

    
     
      
    }
}
