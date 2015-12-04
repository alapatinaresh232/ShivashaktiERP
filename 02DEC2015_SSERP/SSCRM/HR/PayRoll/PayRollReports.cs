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
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using SSTrans;

namespace SSCRM
{
    public partial class PayRollReports : Form
    {
        private ExcelDB objExDb = null;
        private UtilityDB objUtilityDB = null;
        Security objSecurity = null;
        SQLDB objDB = null;
        private string strCmpData = "", strBranchData = "", strDeptData = "", strEmpData = "", sCompData = "", sBranCodes = "", sFormType = "";
        DateTime selectedMonth;
        DateTime FirstDayOfMonth;
        DateTime LastDayOfMonth;

        public PayRollReports()
        {
            InitializeComponent();
        }
        public PayRollReports(string sFrmType)
        {
            InitializeComponent();
            sFormType = sFrmType;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void PayRollReports_Load(object sender, EventArgs e)
        {
            //dtpWagePerioad.Value = DateTime.Today.AddDays(-30);
            //selectedMonth = dtpWagePerioad.Value;
            //FirstDayOfMonth = new DateTime(selectedMonth.Year, selectedMonth.Month, 01);
            //LastDayOfMonth = new DateTime(selectedMonth.Year, selectedMonth.Month, DateTime.DaysInMonth(dtpWagePerioad.Value.Year, dtpWagePerioad.Value.Month));

            if (sFormType == "EMP_LEAVE_RECONC_STMT")
            {
                lblWagePeriod.Visible = false;
                cbWagePeriod.Visible = false;
                grpEmployees.Enabled = false;
                cbReportType.Visible = false;
                lblrep.Visible = false;
                Text = "Emp Leave Reconciliation Statement";
                nmYear.Value = DateTime.Now.Year;
            }
            else
            {
                lblWagePeriod.Visible = true;
                cbWagePeriod.Visible = true;
                grpEmployees.Enabled = true;
                cbReportType.Visible = true;
                lblrep.Visible = true;
                lblYear.Visible = false;
                nmYear.Visible = false;
            }

            FillWagePeriod();

            FillCompanyData();
            FillDepartmentData();
            cbReportType.SelectedIndex = 0;
        }

        private void FillWagePeriod()
        {
            try
            {
                string strCMD = "SELECT HWP_WAGE_MONTH, HWP_WAGE_MONTH FROM HR_WAGE_PERIOD WHERE HWP_STATUS='PROCESSED' ORDER BY HWP_START_DATE DESC";
                objDB = new SQLDB();

                DataTable dt = objDB.ExecuteDataSet(strCMD).Tables[0];
                DataRow dr = dt.NewRow();
                dr[0] = "--Select--";
                dr[1] = "--Select--";
                dt.Rows.InsertAt(dr, 0);
                cbWagePeriod.DataSource = null;

                if (dt.Rows.Count > 1)
                {
                    cbWagePeriod.DataSource = dt;
                    cbWagePeriod.DisplayMember = "HWP_WAGE_MONTH";
                    cbWagePeriod.ValueMember = "HWP_WAGE_MONTH";
                }                        
                else
                {

                    MessageBox.Show(" You Have No Processed Wage Months!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void FillBranchData()
        {
            try
            {
                string strCmd = "";
                if (strCmpData.Length == 0)
                {
                    ((ListBox)clbBranch).DataSource = null;
                    clbBranch.Items.Clear();
                    chkBranchAll.Checked = false;
                }
                else
                {
                        
                        strCmd = "SELECT BRANCH_NAME,BRANCH_CODE FROM BRANCH_MAS where ACTIVE='T' and COMPANY_CODE in (" + strCmpData + ") and BRANCH_TYPE='HO'";
                        objDB = new SQLDB();
                        DataTable dt = objDB.ExecuteDataSet(strCmd).Tables[0];

                        ((ListBox)clbBranch).DataSource = dt;
                        ((ListBox)clbBranch).DisplayMember = "BRANCH_NAME";
                        ((ListBox)clbBranch).ValueMember = "BRANCH_CODE";
                       
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void FillDepartmentData()
        {
            try
            {
                string strCmd = "SELECT dept_name,dept_code FROM Dept_Mas";
                objDB = new SQLDB();
                DataTable dt = objDB.ExecuteDataSet(strCmd).Tables[0];

                ((ListBox)clbDepartment).DataSource = dt;
                ((ListBox)clbDepartment).DisplayMember = "dept_name";
                ((ListBox)clbDepartment).ValueMember = "dept_code";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //private void FillEmployees()
        //{
        //    try
        //    {
        //        if (strCmpData.Length > 0 && strBranchData.Length > 0 && strDeptData.Length > 0)
        //        {
        //            string strCMD = "SELECT ECODE,MEMBER_NAME+'-'+CAST(ECODE AS varchar) Name FROM EORA_MASTER WHERE company_code IN (" + strCmpData +
        //                            ") AND BRANCH_CODE IN (" + strBranchData + ") AND DEPT_ID IN (" + strDeptData + ") ORDER BY MEMBER_NAME ";
        //            objDB = new SQLDB();
        //            DataTable dt = objDB.ExecuteDataSet(strCMD).Tables[0];
        //            UtilityLibrary.AutoCompleteTextBox(txtDsearch, dt, "", "Name");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}


        private void FillCompanyData()
        {
            try
            {
                objSecurity = new Security();
                DataTable dtCpy = objSecurity.GetCompanyDataSet().Tables[0];

                ((ListBox)clbCompany).DataSource = dtCpy;
                ((ListBox)clbCompany).DisplayMember = "CM_Company_Name";
                ((ListBox)clbCompany).ValueMember = "CM_Company_Code";
                objSecurity = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
      
        private void chkCompAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCompAll.Checked == true)
            {
                for (int i = 0; i < clbCompany.Items.Count; i++)
                {
                    clbCompany.SetItemCheckState(i, CheckState.Checked);
                }
               // strCmpData = "'NFL','NKBPL','SATL','SBTLNPL','SHS','SLAF','SSBPL','VNF'";

                foreach (DataRowView view in clbCompany.CheckedItems)
                {
                    strCmpData += "'" + (view[clbCompany.ValueMember].ToString()) + "',";
                }
                if (strCmpData.Length > 0)
                {
                    strCmpData = strCmpData.Substring(0, strCmpData.Length - 1);

                }
                clbCompany.Enabled = false;
                FillBranchData();
                chkBranchAll.Checked = true;
                //chkDeptAll.Checked = true;
                //FillEmployeesData();
                //chkEmpAll.Checked = true;
            }
            else
            {
                for (int i = 0; i < clbCompany.Items.Count; i++)
                {
                    clbCompany.SetItemCheckState(i, CheckState.Unchecked);
                }
                strCmpData = "";
                clbCompany.Enabled = true;
                FillBranchData();
                chkBranchAll.Checked = false;
                //chkDeptAll.Checked = false;
                //FillEmployeesData();
                //chkEmpAll.Checked = false;
            }
        }

        private void chkBranchAll_CheckedChanged(object sender, EventArgs e)
        {
            strBranchData = "";
            if (chkBranchAll.Checked == true)
            {
                for (int i = 0; i < clbBranch.Items.Count; i++)
                {
                    clbBranch.SetItemCheckState(i, CheckState.Checked);
                    
                }
                clbBranch.Enabled = false;
               // strBranchData = "'NFLAPCHYD','NKBAPCHYD','SATAPCHYD','SHSAPCHYD','SSBAPCHYD','SSBNPCHYD','SSFAPCHYD','VNFAPCHYD'";

                foreach (DataRowView view in clbBranch.CheckedItems)
                {
                    strBranchData += "'" + (view[clbBranch.ValueMember].ToString()) + "',";
                }
                if (strBranchData.Length > 0)
                {
                    strBranchData = strBranchData.Substring(0, strBranchData.Length - 1);
                }

            }
            else
            {
                for (int i = 0; i < clbBranch.Items.Count; i++)
                {
                    clbBranch.SetItemCheckState(i, CheckState.Unchecked);
                }
                strBranchData = "";
                clbBranch.Enabled = true;
            }
        }

        private void chkDeptAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDeptAll.Checked == true)
            {
                for (int i = 0; i < clbDepartment.Items.Count; i++)
                {
                    clbDepartment.SetItemCheckState(i, CheckState.Checked);
                }

                strDeptData = "100000,200000,300000,400000,500000,600000,1500000,800000,900000,1000000,1100000,1200000,1300000,1400000";
                clbDepartment.Enabled = false;

            }
            else
            {
                for (int i = 0; i < clbDepartment.Items.Count; i++)
                {
                    clbDepartment.SetItemCheckState(i, CheckState.Unchecked);
                }
                strDeptData = "";
                clbDepartment.Enabled = true;
            }
        }

        private void chkEmpAll_CheckedChanged(object sender, EventArgs e)
        {
            strEmpData = "";
            //if (chkEmpAll.Checked == true)
            //{
            //    for (int i = 0; i < clbEmployees.Items.Count; i++)
            //    {
            //        clbEmployees.SetItemCheckState(i, CheckState.Checked);
            //    }

            //    foreach (DataRowView view in clbEmployees.CheckedItems)
            //    {
            //        strEmpData += (view[clbEmployees.ValueMember].ToString()) + ",";
            //    }

            //    clbEmployees.Enabled = false;
            //}
            //else
            //{
            //    for (int i = 0; i < clbEmployees.Items.Count; i++)
            //    {
            //        clbEmployees.SetItemCheckState(i, CheckState.Unchecked);
            //    }
            //    strEmpData = "";
            //    clbEmployees.Enabled = true;
            //}
            //if (strEmpData.Length > 0)
            //{
            //    strEmpData = strEmpData.TrimEnd(',');

            //}
        }
        private void clbCompany_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (chkCompAll.Checked == false)
            {
                //for (int i = 0; i < clbCompany.Items.Count; i++)
                //{
                //    if (e.Index != i && e.NewValue == CheckState.Checked)
                //    {
                //        clbCompany.SetItemChecked(i, false);
                //    }
                //}
                
            }            
           
        }

        private void clbBranch_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //if (chkBranchAll.Checked == false)
            //{
            //    for (int i = 0; i < clbBranch.Items.Count; i++)
            //    {
            //        if (e.Index != i && e.NewValue == CheckState.Checked)
            //        {
            //            clbBranch.SetItemChecked(i, false);
            //        }

            //    }
            //}
            //strBranchData = "";
            //foreach (DataRowView view in clbBranch.CheckedItems)
            //{
            //    strBranchData += "'"+(view[clbBranch.ValueMember].ToString()) + "',";
            //}
            //if (strBranchData.Length>0)
            //{
            //    strBranchData = strBranchData.Substring(0, strBranchData.Length - 1);
            //}
            //FillEmployeesData();
        }

        private void clbDepartment_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (chkDeptAll.Checked == false)
            {
                if (sFormType == "")
                {
                    for (int i = 0; i < clbDepartment.Items.Count; i++)
                    {
                        if (e.Index != i && e.NewValue == CheckState.Checked)
                        {
                            clbDepartment.SetItemChecked(i, false);
                        }

                    }
                }
            }
            strDeptData = "";
            foreach (DataRowView view in clbDepartment.CheckedItems)
            {
                strDeptData += (view[clbDepartment.ValueMember].ToString()) + ",";
            }
            if(strDeptData.Length>0)
            {
                strDeptData = strDeptData.Substring(0, strDeptData.Length - 1);
               
            }
           // FillEmployeesData();
            clbDepartment_SelectedIndexChanged(null, null);
        }

        private void clbEmployees_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //if (chkEmpAll.Checked == false)
            //{
            //    for (int i = 0; i < clbEmployees.Items.Count; i++)
            //    {
            //        if (e.Index != i && e.NewValue == CheckState.Checked)
            //        {
            //            clbEmployees.SetItemChecked(i, false);
            //        }

            //    }
            //}
            //strEmpData = "";
            //foreach (DataRowView view in clbEmployees.CheckedItems)
            //{
            //    strEmpData += (view[clbEmployees.ValueMember].ToString()) + ",";
            //}
        }  

        private void clbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            strCmpData = "";
            try
            {
                foreach (DataRowView view in clbCompany.CheckedItems)
                {
                    strCmpData += "'" + (view[clbCompany.ValueMember].ToString()) + "',";
                }
                if (strCmpData.Length > 0)
                {
                    strCmpData = strCmpData.Substring(0, strCmpData.Length - 1);

                }
            }
            catch
            {

            }
            chkBranchAll.Checked = true;           
            FillBranchData();
            chkBranchAll_CheckedChanged(null, null);
        }

        private void clbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            strDeptData = "";
            foreach (DataRowView view in clbDepartment.CheckedItems)
            {
                strDeptData += (view[clbDepartment.ValueMember].ToString()) +',';
            }
            if (strDeptData.Length> 0)
            {
                strDeptData = strDeptData.Substring(0, strDeptData.Length - 1);
                
            }
           // FillEmployeesData();
            FillEmployeeList();
           // FillEmployees();
        }

        private void clbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            strBranchData = "";
            foreach (DataRowView view in clbBranch.CheckedItems)
            {
                strBranchData+= "'" + (view[clbBranch.ValueMember].ToString()) + "',";
            }
            if (strBranchData.Length> 0)
            {
                strBranchData = strBranchData.Substring(0, strBranchData.Length - 1);
               
            }
            //FillEmployeesData();
            FillEmployeeList();
            //FillEmployees();
        }

        private DataSet Get_EmployeeList(string CompCode, string BranType, string DeptId, string WageMonth, string sType)
        {
            objDB = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objDB.CreateParameter("@sCompany", DbType.String, CompCode, ParameterDirection.Input);
                param[1] = objDB.CreateParameter("@sBranchType", DbType.String, BranType, ParameterDirection.Input);
                param[2] = objDB.CreateParameter("@sDeptId", DbType.String, DeptId, ParameterDirection.Input);
                param[3] = objDB.CreateParameter("@sWageMonth", DbType.String, WageMonth, ParameterDirection.Input);
                param[4] = objDB.CreateParameter("@sType", DbType.String, sType, ParameterDirection.Input);
                ds = objDB.ExecuteDataSet("Get_EmployeeListForPayrollReports", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objDB = null;
            }
            return ds;
        }

        private void FillEmployeeList()
        {
            tvEmployees.Nodes.Clear();
            objDB = new SQLDB();
            DataSet ds = new DataSet();
            GetSelectedValues();

            try
            {
                ds = Get_EmployeeList(sCompData, "", "", cbWagePeriod.SelectedValue.ToString(), "PARENT");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    tvEmployees.Nodes.Add("Employees", "Employees");

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        tvEmployees.Nodes[0].Nodes.Add(ds.Tables[0].Rows[i]["ValMember"].ToString(), ds.Tables[0].Rows[i]["DisMember"].ToString());

                        DataSet dsEmp = new DataSet();

                        dsEmp = Get_EmployeeList(sCompData, ds.Tables[0].Rows[i]["ValMember"].ToString(), strDeptData, cbWagePeriod.SelectedValue.ToString(), "CHILD");

                        if (dsEmp.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < dsEmp.Tables[0].Rows.Count; j++)
                            {
                                tvEmployees.Nodes[0].Nodes[i].Nodes.Add(dsEmp.Tables[0].Rows[j]["EmpCode"].ToString(), dsEmp.Tables[0].Rows[j]["EmpName"].ToString());
                            }
                        }

                    }

                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.tvEmployees.SelectedNode = tvEmployees.Nodes[0];
                    this.tvEmployees.SelectedNode.Expand();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

      
        private void GetSelectedValues()
        {
            strEmpData = "";
            strCmpData = "";
            strBranchData = "";
            sCompData = "";
            sBranCodes = "";

            foreach (DataRowView view in clbBranch.CheckedItems)
            {
                strBranchData += "'" + (view[clbBranch.ValueMember].ToString()) + "',";
            }
            if (strBranchData.Length > 0)
            {
                strBranchData = strBranchData.Substring(0, strBranchData.Length - 1);

            }

            if (tvEmployees.Nodes.Count > 0)
            {
               
                    for (int j = 0; j < tvEmployees.Nodes[0].Nodes.Count; j++)
                    {
                        for (int k = 0; k < tvEmployees.Nodes[0].Nodes[j].Nodes.Count; k++)
                        {
                            if (tvEmployees.Nodes[0].Nodes[j].Nodes[k].Checked == true)
                            {
                                strEmpData += tvEmployees.Nodes[0].Nodes[j].Nodes[k].Name.ToString()+',';
                            }
                        }
                    }
                
            }

            if (strEmpData.Length > 0)
            {
                strEmpData = strEmpData.TrimEnd(',');
            }

            foreach (DataRowView view in clbCompany.CheckedItems)
            {
                strCmpData += "'" + (view[clbCompany.ValueMember].ToString()) + "',";
            }
            if (strCmpData.Length > 0)
            {
                strCmpData = strCmpData.Substring(0, strCmpData.Length - 1);

            }

            foreach (DataRowView view in clbCompany.CheckedItems)
            {
                sCompData += (view[clbCompany.ValueMember].ToString())+',';
            }
            if (sCompData.Length > 0)
            {
                sCompData = sCompData.Substring(0, sCompData.Length - 1);

            }

            foreach (DataRowView view in clbBranch.CheckedItems)
            {
                sBranCodes += (view[clbBranch.ValueMember].ToString()) +',';
            }
            if (sBranCodes.Length > 0)
            {
                sBranCodes = sBranCodes.Substring(0, sBranCodes.Length - 1);

            }


        }


        private void btnReport_Click(object sender, EventArgs e)
        {
            GetSelectedValues();

            ReportViewer childReportViewer = null;
            if(CheckData())
            {
                if (sFormType == "EMP_LEAVE_RECONC_STMT")
                {
                    childReportViewer = new ReportViewer(sCompData, sBranCodes, nmYear.Value.ToString(), strDeptData, "ALL", "");
                    CommonData.ViewReport = "EMP_WISE_LEAVE_RECONCILIATION";
                    childReportViewer.Show();
                }
                else
                {
                    if (cbReportType.SelectedIndex == 1)
                    {
                        childReportViewer = new ReportViewer(cbWagePeriod.SelectedValue.ToString(), sCompData, strEmpData, "PAYSLIP");
                        //childReportViewer = new ReportViewer("JUN2014", "ALL", "ALL","26/JUN/2014", "WAGEATTD", "ALL");

                        CommonData.ViewReport = "HR_PAYROLL_PRINT_MONYY";
                        childReportViewer.Show();
                    }
                    else if (cbReportType.SelectedIndex == 2)
                    {

                        childReportViewer = new ReportViewer(cbWagePeriod.SelectedValue.ToString(), sCompData, strEmpData, "PAYREGBANK");
                        CommonData.ViewReport = "HR_PAYROLL_PAY_REG_BANK";
                        childReportViewer.Show();
                    }
                    else if (cbReportType.SelectedIndex == 3)
                    {

                        childReportViewer = new ReportViewer(cbWagePeriod.SelectedValue.ToString(), sCompData, strEmpData, "PAYREGCASH");
                        CommonData.ViewReport = "HR_PAYROLL_PAY_REG_CASH";
                        childReportViewer.Show();
                    }

                    else if (cbReportType.SelectedIndex == 4)
                    {

                        childReportViewer = new ReportViewer(cbWagePeriod.SelectedValue.ToString(), sCompData, strEmpData, "PAYREGDEDU");
                        CommonData.ViewReport = "HR_PAYROLL_PAY_REG_DEDU";
                        childReportViewer.Show();
                    }
                    else if (cbReportType.SelectedIndex == 5)
                    {

                        childReportViewer = new ReportViewer(cbWagePeriod.SelectedValue.ToString(), sCompData, strEmpData, "PAYREGESI");
                        CommonData.ViewReport = "HR_PAYROLL_PAY_REG_ESI";
                        childReportViewer.Show();
                    }
                    else if (cbReportType.SelectedIndex == 6)
                    {

                        childReportViewer = new ReportViewer(cbWagePeriod.SelectedValue.ToString(), sCompData, strEmpData, "PAYREGPTAX");
                        CommonData.ViewReport = "HR_PAYROLL_PAY_REG_PROFTAX";
                        childReportViewer.Show();
                    }
                    else if (cbReportType.SelectedIndex == 7)
                    {

                        childReportViewer = new ReportViewer(cbWagePeriod.SelectedValue.ToString(), sCompData, strEmpData, "BANKWISESALARY");
                        CommonData.ViewReport = "HR_PAYROLL_PAYLIST_BANKWISE";
                        childReportViewer.Show();
                    }
                }                                          
                
            }
        }

        private bool CheckData()
        {
            bool flag=true;
            GetSelectedValues();

            if (sFormType == "" || sFormType == "EMP_LEAVE_RECONC_STMT")
            {

                if (strCmpData.Length == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Select Company", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return flag;
                }
                if (strBranchData.Length == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Select Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return flag;
                }
                if (strDeptData.Length == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Select Department", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return flag;
                }
            }
            if (sFormType == "")
            {
                if (strEmpData.Length == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Select Employee", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return flag;
                }
                if (cbReportType.SelectedIndex == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Select ReportType", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return flag;
                }
                if (cbWagePeriod.SelectedIndex == -1)
                {
                    flag = false;
                    MessageBox.Show("We have no Processed Wage Months", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return flag;
                }
                if (cbWagePeriod.SelectedIndex == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Select WageMonth", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return flag;
                }
            }
            //try
            //{
            //    string strSQL = "select HWP_START_DATE,HWP_END_DATE,HWP_DAYS from HR_WAGE_PERIOD where HWP_WAGE_MONTH='" + cbWagePeriod.SelectedValue + "' and hwp_status='PROCESSED'";
            //    objDB = new SQLDB();
            //    DataTable dt = objDB.ExecuteDataSet(strSQL).Tables[0];
            //    strSQL = "SELECT * FROM HR_PAYROLL_ATTD_MTOD_TRAN WHERE HPAMT_DATE='" + LastDayOfMonth.ToString("dd/MMM/yyyy") + "'";
            //    DataTable dt1 = objDB.ExecuteDataSet(strSQL).Tables[0];
            //    if (dt.Rows.Count > 0 && dt1.Rows.Count>0)
            //    {
            //        //dtpFrom.Value = Convert.ToDateTime(dt.Rows[0]["HWP_START_DATE"].ToString());
            //        //dtpTo.Value = Convert.ToDateTime(dt.Rows[0]["HWP_END_DATE"].ToString());
            //        //txtNoofDays.Text = dt.Rows[0]["HWP_DAYS"].ToString();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Selected WagePeriod is Not Processed");
            //        //dtpWagePerioad.Value = DateTime.Today.AddDays(-30);
            //        flag = false;
            //        return flag;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            
            return flag;
        }

        private void dtpWagePerioad_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                //string strSQL = "select HWP_START_DATE,HWP_END_DATE,HWP_DAYS from HR_WAGE_PERIOD where HWP_WAGE_MONTH='" + (dtpWagePerioad.Value).ToString("MMMyyyy").ToUpper() + "' and hwp_status='RUNNING'";
                //objDB = new SQLDB();
                //DataTable dt = objDB.ExecuteDataSet(strSQL).Tables[0];
                //if (dt.Rows.Count > 0)
                //{
                //    //dtpFrom.Value = Convert.ToDateTime(dt.Rows[0]["HWP_START_DATE"].ToString());
                //    //dtpTo.Value = Convert.ToDateTime(dt.Rows[0]["HWP_END_DATE"].ToString());
                //    //txtNoofDays.Text = dt.Rows[0]["HWP_DAYS"].ToString();
                //}
                //else
                //{
                //    MessageBox.Show("Selected WagePeriod is Not Valid");
                //    dtpWagePerioad.Value = DateTime.Today.AddDays(-30);

                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void clbEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            strEmpData = "";
            //foreach (DataRowView view in clbEmployees.CheckedItems)
            //{
            //    strEmpData += "" + (view[clbEmployees.ValueMember].ToString()) + ",";
            //}
            if (strEmpData.Length > 0)
            {
                strEmpData = strEmpData.TrimEnd(',');

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            strEmpData = "";
            strBranchData = "";
            strCmpData = "";
            strDeptData = "";
            chkBranchAll.Checked = false;
            chkCompAll.Checked = false;
            chkDeptAll.Checked = false;
            sCompData = "";
            sBranCodes = "";
            
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            GetSelectedValues();

            if (CheckData())
            {
                if (sFormType == "EMP_LEAVE_RECONC_STMT")
                {
                    objExDb = new ExcelDB();

                    DataTable dtExcel = new DataTable();
                    objUtilityDB = new UtilityDB();
                    dtExcel = objExDb.Get_EmpLeaveReconciliationDetl(sCompData, sBranCodes, Convert.ToInt32(nmYear.Value), strDeptData, "ALL", "").Tables[0];

                    if (dtExcel.Rows.Count > 0)
                    {
                        try
                        {
                            Excel.Application oXL = new Excel.Application();
                            Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                            Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                            oXL.Visible = true;
                            int iTotColumns = 0;
                            iTotColumns = 5 + (3 * 14);
                            string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                            Excel.Range rg = worksheet.get_Range("A4", sLastColumn + "4");
                            Excel.Range rgData = worksheet.get_Range("A4", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["emp_tot"]) + 4).ToString());
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
                            rg.Cells.RowHeight = 25;
                            rgData = worksheet.get_Range("A5", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["emp_tot"]) + 4).ToString());
                            rgData.WrapText = false;
                            rg = worksheet.get_Range("A4", Type.Missing);
                            rg.Cells.ColumnWidth = 4;
                            rg = worksheet.get_Range("B4", Type.Missing);
                            rg.Cells.ColumnWidth = 8;
                            rg = worksheet.get_Range("C4", Type.Missing);
                            rg.Cells.ColumnWidth = 30;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("D4", Type.Missing);
                            rg.Cells.ColumnWidth = 25;
                            rg.WrapText = true;
                            rg = worksheet.get_Range("E4", Type.Missing);
                            rg.Cells.ColumnWidth = 25;
                            rg.WrapText = true;


                            int iColumn = 1;
                            worksheet.Cells[4, iColumn++] = "SlNo";
                            worksheet.Cells[4, iColumn++] = "Ecode";
                            worksheet.Cells[4, iColumn++] = "Emp Name";
                            worksheet.Cells[4, iColumn++] = "Dept";
                            worksheet.Cells[4, iColumn++] = "Desig";

                            Excel.Range rgHead;

                            int iStartColumn = 0;
                            for (int iCol = 0; iCol < 14; iCol++)
                            {
                                rgHead = worksheet.get_Range("A2", "E3");
                                rgHead.Merge(Type.Missing);
                                rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Cells.RowHeight = 20;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.Value2 = "EMP WISE LEAVE RECONCILIATION STATEMENT";


                                iStartColumn = (3 * iCol) + 6;
                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "2", objUtilityDB.GetColumnName(iStartColumn + 2) + "2");


                                rgHead.Merge(Type.Missing);
                                rgHead.Interior.ColorIndex = 34 + 1;
                                rgHead.Borders.Weight = 2;
                                rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
                                rgHead.Cells.RowHeight = 20;
                                rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "3", objUtilityDB.GetColumnName(iStartColumn + 2) + "3");


                                rgHead.Merge(Type.Missing);
                                rgHead.Interior.ColorIndex = 34 + 1;
                                rgHead.Borders.Weight = 2;
                                rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
                                rgHead.Cells.RowHeight = 20;
                                rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;

                                rgHead.Cells.ColumnWidth = 5;
                                worksheet.Cells[4, iStartColumn++] = "CL";
                                worksheet.Cells[4, iStartColumn++] = "EL";
                                worksheet.Cells[4, iStartColumn++] = "SL";


                            }


                            int iRowCounter = 5; int iColumnCounter = 1;
                            for (int i = 0; i < dtExcel.Rows.Count; i++)
                            {
                                if (i > 0)
                                {

                                    if (dtExcel.Rows[i]["emp_ecode"].ToString() == dtExcel.Rows[i - 1]["emp_ecode"].ToString())
                                    {
                                        int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["emp_slno"]);
                                        //int iStartColumn = 0;
                                        iColumnCounter = (3 * (iMonthNo - 1)) + 9;


                                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "3", objUtilityDB.GetColumnName(iColumnCounter + 2) + "3");
                                        rgHead.Cells.Value2 = dtExcel.Rows[i]["emp_month"];
                                        rgHead.WrapText = true;

                                        rgHead.Interior.ColorIndex = 31;
                                        rgHead.Font.ColorIndex = 2;
                                        rgHead.Font.Bold = true;
                                        rgHead.Borders.Weight = 2;
                                        //rgHead.Interior.ColorIndex = 31;
                                        //rgHead.Font.ColorIndex = 2;
                                        rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                        rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;

                                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 2) + "2");
                                        rgHead.Cells.Value2 = dtExcel.Rows[i]["emp_trntype"];
                                        rgHead.WrapText = true;

                                        rgHead.Interior.ColorIndex = 34 + 1;
                                        rgHead.Font.ColorIndex = 1;
                                        rgHead.Font.Bold = true;
                                        rgHead.Borders.Weight = 2;
                                        //rgHead.Interior.ColorIndex = 31;
                                        //rgHead.Font.ColorIndex = 2;
                                        rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                        rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;

                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["emp_cl"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["emp_el"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["emp_sl"];
                                    }

                                    else
                                    {

                                        iRowCounter++;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 4;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["emp_ecode"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["emp_name"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["emp_dept"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["emp_desig"];

                                        int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["emp_slno"]);

                                        iColumnCounter = (3 * (iMonthNo - 1)) + 9;

                                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "3", objUtilityDB.GetColumnName(iColumnCounter + 2) + "3");
                                        rgHead.Cells.Value2 = dtExcel.Rows[i]["emp_month"];
                                        rgHead.WrapText = true;

                                        rgHead.Interior.ColorIndex = 31;
                                        rgHead.Font.ColorIndex = 2;
                                        rgHead.Font.Bold = true;
                                        rgHead.Borders.Weight = 2;
                                        //rgHead.Interior.ColorIndex = 31;
                                        //rgHead.Font.ColorIndex = 2;
                                        rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                        rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;

                                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 2) + "2");
                                        rgHead.Cells.Value2 = dtExcel.Rows[i]["emp_trntype"];
                                        rgHead.WrapText = true;

                                        rgHead.Interior.ColorIndex = 34 + 1;
                                        rgHead.Font.ColorIndex = 1;
                                        rgHead.Font.Bold = true;
                                        rgHead.Borders.Weight = 2;
                                        //rgHead.Interior.ColorIndex = 31;
                                        //rgHead.Font.ColorIndex = 2;
                                        rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                        rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;



                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["emp_cl"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["emp_el"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["emp_sl"];
                                    }
                                }
                                else
                                {

                                    worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 4;
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["emp_ecode"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["emp_name"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["emp_dept"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["emp_desig"];

                                    int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["emp_slno"]);
                                    //int iStartColumn = 0;
                                    iColumnCounter = (3 * (iMonthNo - 1)) + 9;

                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "3", objUtilityDB.GetColumnName(iColumnCounter + 2) + "3");
                                    rgHead.Cells.Value2 = dtExcel.Rows[i]["emp_month"];
                                    rgHead.WrapText = true;

                                    rgHead.Interior.ColorIndex = 31;
                                    rgHead.Font.ColorIndex = 2;
                                    rgHead.Font.Bold = true;
                                    rgHead.Borders.Weight = 2;
                                    //rgHead.Interior.ColorIndex = 31;
                                    //rgHead.Font.ColorIndex = 2;
                                    rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                    rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;

                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 2) + "2");
                                    rgHead.Cells.Value2 = dtExcel.Rows[i]["emp_trntype"];
                                    rgHead.WrapText = true;

                                    rgHead.Interior.ColorIndex = 34 + 1;
                                    rgHead.Font.ColorIndex = 1;
                                    rgHead.Font.Bold = true;
                                    rgHead.Borders.Weight = 2;
                                    //rgHead.Interior.ColorIndex = 31;
                                    //rgHead.Font.ColorIndex = 2;
                                    rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                    rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;


                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["emp_cl"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["emp_el"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["emp_sl"];

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
                else
                {
                    if (cbReportType.SelectedIndex == 1)
                    {
                        //childReportViewer = new ReportViewer(cbWagePeriod.SelectedValue.ToString(), strCmpData, strEmpData, "PAYSLIP");
                        ////childReportViewer = new ReportViewer("JUN2014", "ALL", "ALL","26/JUN/2014", "WAGEATTD", "ALL");

                        //CommonData.ViewReport = "HR_PAYROLL_PRINT_MONYY";
                        //childReportViewer.Show();



                    }
                    else if (cbReportType.SelectedIndex == 2 || cbReportType.SelectedIndex == 3)
                    {
                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet;
                        int iSheetNo = 0;

                        //for (int ivar = 0; ivar < clbCompany.Items.Count; ivar++)
                        //{
                        //    if (clbCompany.GetItemCheckState(ivar) == CheckState.Checked)
                        //    {
                        foreach (DataRowView view in clbCompany.CheckedItems)
                        {
                            //strCmpData += "'" + (view[clbCompany.ValueMember].ToString()) + "',";

                            string strCompany = (view[clbCompany.ValueMember].ToString());


                            DataTable dtExcel = new DataTable();
                            objExDb = new ExcelDB();
                            objUtilityDB = new UtilityDB();

                            try
                            {

                                if (cbReportType.SelectedIndex == 2)
                                    dtExcel = objExDb.GetPayRollReportsData(cbWagePeriod.SelectedValue.ToString(), strCompany, strEmpData, "PAYREGBANK").Tables[0];
                                else if (cbReportType.SelectedIndex == 3)
                                    dtExcel = objExDb.GetPayRollReportsData(cbWagePeriod.SelectedValue.ToString(), strCompany, strEmpData, "PAYREGCASH").Tables[0];
                                objExDb = null;

                                if (dtExcel.Rows.Count > 0)
                                {
                                    //string strHead = "";
                                    Excel.Sheets worksheets = theWorkbook.Worksheets;
                                    worksheet = (Excel.Worksheet)worksheets.Add(worksheets[iSheetNo + 1], Type.Missing, Type.Missing, Type.Missing);
                                    worksheet.Name = strCompany;
                                    iSheetNo++;

                                    oXL.Visible = true;
                                    string sLastColumn = objUtilityDB.GetColumnName(45);
                                    Excel.Range rg = worksheet.get_Range("A3", sLastColumn + "3");
                                    Excel.Range rgData = worksheet.get_Range("A3", sLastColumn + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString());
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

                                    rg = worksheet.get_Range("A3", Type.Missing);
                                    rg.Cells.ColumnWidth = 4;

                                    rg = worksheet.get_Range("B3", Type.Missing);
                                    rg.Cells.ColumnWidth = 6;

                                    rg = worksheet.get_Range("C3", Type.Missing);
                                    rg.Cells.ColumnWidth = 30;

                                    rg = worksheet.get_Range("D3", Type.Missing);
                                    rg.Cells.ColumnWidth = 30;

                                    rg = worksheet.get_Range("E3", Type.Missing);
                                    rg.Cells.ColumnWidth = 30;

                                    rg = worksheet.get_Range("F3", Type.Missing);
                                    rg.Cells.ColumnWidth = 15;

                                    rg = worksheet.get_Range("G3", Type.Missing);
                                    rg.Cells.ColumnWidth = 7;

                                    rg = worksheet.get_Range("H3", Type.Missing);
                                    rg.Cells.ColumnWidth = 7;

                                    rg = worksheet.get_Range(objUtilityDB.GetColumnName(45) + "3", Type.Missing);
                                    rg.Cells.ColumnWidth = 20;
                                    rg = worksheet.get_Range(objUtilityDB.GetColumnName(44) + "3", Type.Missing);
                                    rg.Cells.ColumnWidth = 10;

                                    Excel.Range rgHead = null;
                                    rgHead = worksheet.get_Range("A1", "H2");
                                    rgHead.Merge(Type.Missing);
                                    rgHead.Font.Size = 14;
                                    rgHead.Font.ColorIndex = 1;
                                    rgHead.Font.Bold = true;
                                    rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                    rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                    if (cbReportType.SelectedIndex == 2)
                                        rgHead.Cells.Value2 = "PAY ROLL REGISTER FOR THE MONTH OF " + dtExcel.Rows[0]["HPCM_WAGEMONTH"].ToString() + " FOR BANK";
                                    else if (cbReportType.SelectedIndex == 3)
                                        rgHead.Cells.Value2 = "PAY ROLL REGISTER FOR THE MONTH OF " + dtExcel.Rows[0]["HPCM_WAGEMONTH"].ToString() + " FOR CASH";

                                    int iColumn = 1;
                                    worksheet.Cells[3, iColumn++] = "SlNo";
                                    worksheet.Cells[3, iColumn++] = "Ecode";
                                    worksheet.Cells[3, iColumn++] = "Name";
                                    worksheet.Cells[3, iColumn++] = "Desig";
                                    worksheet.Cells[3, iColumn++] = "Department";
                                    worksheet.Cells[3, iColumn++] = "Doj";
                                    worksheet.Cells[3, iColumn++] = "Present Daye";
                                    worksheet.Cells[3, iColumn++] = "Lops";

                                    for (int i = 0; i < 2; i++)
                                    {
                                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumn) + "2", objUtilityDB.GetColumnName(iColumn + 12) + "2");
                                        //rgHead.Cells.ColumnWidth = 5;
                                        rgHead.Merge(Type.Missing);
                                        if (i == 0)
                                            rgHead.Value2 = "ACTUAL EARNINGS";
                                        else
                                            rgHead.Value2 = "GAINED EARNINGS";
                                        rgHead.Interior.ColorIndex = 44 + i;
                                        rgHead.Borders.Weight = 2;
                                        rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
                                        rgHead.Cells.RowHeight = 20;
                                        rgHead.Font.Size = 14;
                                        rgHead.Font.ColorIndex = 1;
                                        rgHead.Font.Bold = true;
                                        rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                        rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumn) + "3", objUtilityDB.GetColumnName(iColumn + 12) + "3");
                                        rgHead.Interior.ColorIndex = 44 + i;
                                        rgHead.Font.ColorIndex = 1;
                                        rgHead.Cells.ColumnWidth = 8;

                                        worksheet.Cells[3, iColumn++] = "Basic";
                                        worksheet.Cells[3, iColumn++] = "HRA";
                                        worksheet.Cells[3, iColumn++] = "Conv Allw";
                                        worksheet.Cells[3, iColumn++] = "CCA Allw";
                                        worksheet.Cells[3, iColumn++] = "LTA Allw";
                                        worksheet.Cells[3, iColumn++] = "Special Allw";
                                        worksheet.Cells[3, iColumn++] = "Books & Period.";
                                        worksheet.Cells[3, iColumn++] = "Med Reimb";
                                        worksheet.Cells[3, iColumn++] = "Children";
                                        worksheet.Cells[3, iColumn++] = "Vehicle Allw";
                                        worksheet.Cells[3, iColumn++] = "Petrol Allw";
                                        worksheet.Cells[3, iColumn++] = "Uniform Allw";
                                        worksheet.Cells[3, iColumn++] = "Earning Total";
                                    }

                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumn) + "2", objUtilityDB.GetColumnName(iColumn + 7) + "2");
                                    //rgHead.Cells.ColumnWidth = 5;
                                    rgHead.Merge(Type.Missing);
                                    rgHead.Value2 = "DEDUCTIONS";
                                    rgHead.Interior.ColorIndex = 46;
                                    rgHead.Borders.Weight = 2;
                                    rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
                                    rgHead.Cells.RowHeight = 20;
                                    rgHead.Font.Size = 14;
                                    rgHead.Font.ColorIndex = 1;
                                    rgHead.Font.Bold = true;
                                    rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                    rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumn) + "3", objUtilityDB.GetColumnName(iColumn + 7) + "3");
                                    rgHead.Interior.ColorIndex = 46;
                                    rgHead.Font.ColorIndex = 1;
                                    rgHead.Cells.ColumnWidth = 7;

                                    worksheet.Cells[3, iColumn++] = "PF";
                                    worksheet.Cells[3, iColumn++] = "Proff Tax";
                                    worksheet.Cells[3, iColumn++] = "ESI";
                                    worksheet.Cells[3, iColumn++] = "Sal Adv";
                                    worksheet.Cells[3, iColumn++] = "Pers Loan";
                                    worksheet.Cells[3, iColumn++] = "TDS";
                                    worksheet.Cells[3, iColumn++] = "Others";
                                    worksheet.Cells[3, iColumn++] = "Total Deductions";

                                    worksheet.Cells[3, iColumn++] = "Net Pay";
                                    worksheet.Cells[3, iColumn++] = "Bank Name";
                                    worksheet.Cells[3, iColumn++] = "Account No";

                                    iColumn = 1;
                                    for (int i = 0; i < dtExcel.Rows.Count; i++)
                                    {
                                        worksheet.Cells[i + 4, iColumn++] = i + 1;
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_EORA_CODE"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_NAME"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_DESIG"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_DEPT_NAME"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_DOJ"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PRE"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_LOP"].ToString();

                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_BASIC"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_HRA"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_CONV_ALW"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_CCA"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_LTA_ALW"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_SPL_ALW"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_BNP_ALW"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_MED_REIMB"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_CH_ED_ALW"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_VEH_ALW"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PET_ALW"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_UNF_ALW"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = "=I" + (i + 4).ToString() + "+J" + (i + 4).ToString() +
                                                                            "+K" + (i + 4).ToString() + "+L" + (i + 4).ToString() +
                                                                            "+M" + (i + 4).ToString() + "+N" + (i + 4).ToString() +
                                                                            "+O" + (i + 4).ToString() + "+P" + (i + 4).ToString() +
                                                                            "+Q" + (i + 4).ToString() + "+R" + (i + 4).ToString() +
                                                                            "+S" + (i + 4).ToString() + "+T" + (i + 4).ToString() + "";
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_BASIC"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_HRA"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_CONV_ALW"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_CCA"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_LTA_ALW"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_SPL_ALW"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_BNP_ALW"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_MED_REIMB"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_CH_ED_ALW"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_VEH_ALW"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_PET_ALW"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_UNF_ALW"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = "=U" + (i + 4).ToString() + "+V" + (i + 4).ToString() +
                                                                            "+W" + (i + 4).ToString() + "+X" + (i + 4).ToString() +
                                                                            "+Y" + (i + 4).ToString() + "+Z" + (i + 4).ToString() +
                                                                            "+AA" + (i + 4).ToString() + "+AB" + (i + 4).ToString() +
                                                                            "+AC" + (i + 4).ToString() + "+AD" + (i + 4).ToString() +
                                                                            "+AE" + (i + 4).ToString() + "+AF" + (i + 4).ToString() + "";

                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_PF"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_PROFTAX"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_ESI"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_SAL_ADV"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_PERS_LOAN"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_TDS"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_OTHERS"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_TOTAL"].ToString();

                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_NET_PAY"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_BANK_NAME"].ToString();
                                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_BANK_ACCOUNT_NO"].ToString() + "'";

                                        iColumn = 1;
                                    }

                                    Int32 iStartRow = 9;
                                    iColumn = iStartRow;
                                    rgHead = worksheet.get_Range("I" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString(),
                                                            "AQ" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString());

                                    rg = worksheet.get_Range("A" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString(),
                                                            "H" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString());
                                    rg.Merge(Type.Missing);
                                    rg.Value2 = "TOTALS";
                                    rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 14;
                                    rg.Font.ColorIndex = 30;
                                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                                    rg.HorizontalAlignment = Excel.Constants.xlCenter;

                                    rgHead.Borders.Weight = 2;
                                    rgHead.Font.Size = 12; rgHead.Font.Bold = true;

                                    for (int j = 0; j < Convert.ToInt32(dtExcel.Rows.Count); j++)
                                    {
                                        iStartRow = 9; iColumn = iStartRow;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;
                                        worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 4, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString() + ")";
                                        iColumn = iColumn + 1;

                                    }
                                }
                                //else
                                //{
                                //    MessageBox.Show("No Data Found!", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //}
                            }

                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }

                        }
                        //}

                        //    }

                    }

                    else if (cbReportType.SelectedIndex == 4)
                    {

                        ////childReportViewer = new ReportViewer(cbWagePeriod.SelectedValue.ToString(), strCmpData, strEmpData, "PAYREGDEDU");
                        ////CommonData.ViewReport = "HR_PAYROLL_PAY_REG_DEDU";
                        ////childReportViewer.Show();

                        try
                        {
                            objExDb = new ExcelDB();
                            objUtilityDB = new UtilityDB();
                            DataTable dtExcel = new DataTable();
                            dtExcel = objExDb.GetPayRollReportsData(cbWagePeriod.SelectedValue.ToString(), sCompData, strEmpData, "PAYREGDEDU").Tables[0];
                            objExDb = null;

                            if (dtExcel.Rows.Count > 0)
                            {
                                Excel.Application oXL = new Excel.Application();
                                Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                                Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                                oXL.Visible = true;
                                string sLastColumn = objUtilityDB.GetColumnName(15);
                                Excel.Range rg = worksheet.get_Range("A3", sLastColumn + "3");
                                Excel.Range rgData = worksheet.get_Range("A3", sLastColumn + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString());
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

                                rg = worksheet.get_Range("A3", Type.Missing);
                                rg.Cells.ColumnWidth = 4;

                                rg = worksheet.get_Range("B3", Type.Missing);
                                rg.Cells.ColumnWidth = 6;

                                rg = worksheet.get_Range("C3", Type.Missing);
                                rg.Cells.ColumnWidth = 30;

                                rg = worksheet.get_Range("D3", Type.Missing);
                                rg.Cells.ColumnWidth = 30;

                                rg = worksheet.get_Range("E3", Type.Missing);
                                rg.Cells.ColumnWidth = 30;

                                rg = worksheet.get_Range("F3", Type.Missing);
                                rg.Cells.ColumnWidth = 15;

                                rg = worksheet.get_Range("G3", Type.Missing);
                                rg.Cells.ColumnWidth = 10;



                                Excel.Range rgHead = null;
                                rgHead = worksheet.get_Range("A1", "H2");
                                rgHead.Merge(Type.Missing);
                                rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.Value2 = "DEDUCTIONS FOR THE MONTH OF " + dtExcel.Rows[0]["HPCM_WAGEMONTH"].ToString() + "";

                                int iColumn = 1;
                                worksheet.Cells[3, iColumn++] = "SlNo";
                                worksheet.Cells[3, iColumn++] = "Ecode";
                                worksheet.Cells[3, iColumn++] = "Name";
                                worksheet.Cells[3, iColumn++] = "Desig";
                                worksheet.Cells[3, iColumn++] = "Department";
                                worksheet.Cells[3, iColumn++] = "Doj";
                                worksheet.Cells[3, iColumn++] = "Company";
                                //worksheet.Cells[3, iColumn++] = "Branch";

                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumn) + "2", objUtilityDB.GetColumnName(iColumn + 7) + "2");
                                //rgHead.Cells.ColumnWidth = 5;
                                rgHead.Merge(Type.Missing);
                                rgHead.Value2 = "DEDUCTIONS";
                                rgHead.Interior.ColorIndex = 46;
                                rgHead.Borders.Weight = 2;
                                rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
                                rgHead.Cells.RowHeight = 20;
                                rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumn) + "3", objUtilityDB.GetColumnName(iColumn + 7) + "3");
                                rgHead.Interior.ColorIndex = 46;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Cells.ColumnWidth = 5;

                                worksheet.Cells[3, iColumn++] = "PF";
                                worksheet.Cells[3, iColumn++] = "Proff Tax";
                                worksheet.Cells[3, iColumn++] = "ESI";
                                worksheet.Cells[3, iColumn++] = "Sal Adv";
                                worksheet.Cells[3, iColumn++] = "Pers Loan";
                                worksheet.Cells[3, iColumn++] = "TDS";
                                worksheet.Cells[3, iColumn++] = "Others";
                                worksheet.Cells[3, iColumn++] = "Total Deductions";

                                iColumn = 1;
                                for (int i = 0; i < dtExcel.Rows.Count; i++)
                                {
                                    worksheet.Cells[i + 4, iColumn++] = i + 1;
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_EORA_CODE"].ToString();
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_NAME"].ToString();
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_DESIG"].ToString();
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_DEPT_NAME"].ToString();
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_DOJ"].ToString();
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_COMPANY_CODE"].ToString();
                                    //worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_LOP"].ToString();

                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_PF"].ToString();
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_PROFTAX"].ToString();
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_ESI"].ToString();
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_SAL_ADV"].ToString();
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_PERS_LOAN"].ToString();
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_TDS"].ToString();
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_OTHERS"].ToString();
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_TOTAL"].ToString();


                                    iColumn = 1;
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
                    else if (cbReportType.SelectedIndex == 5)
                    {

                        //childReportViewer = new ReportViewer(cbWagePeriod.SelectedValue.ToString(), strCmpData, strEmpData, "PAYREGESI");
                        //CommonData.ViewReport = "HR_PAYROLL_PAY_REG_ESI";
                        //childReportViewer.Show();

                        try
                        {
                            objExDb = new ExcelDB();
                            objUtilityDB = new UtilityDB();
                            DataTable dtExcel = new DataTable();
                            dtExcel = objExDb.GetPayRollReportsData(cbWagePeriod.SelectedValue.ToString(), sCompData, strEmpData, "PAYREGESI").Tables[0];
                            objExDb = null;

                            if (dtExcel.Rows.Count > 0)
                            {
                                Excel.Application oXL = new Excel.Application();
                                Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                                Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                                oXL.Visible = true;
                                string sLastColumn = objUtilityDB.GetColumnName(11);
                                Excel.Range rg = worksheet.get_Range("A3", sLastColumn + "3");
                                Excel.Range rgData = worksheet.get_Range("A3", sLastColumn + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString());
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

                                rg = worksheet.get_Range("A3", Type.Missing);
                                rg.Cells.ColumnWidth = 4;

                                rg = worksheet.get_Range("B3", Type.Missing);
                                rg.Cells.ColumnWidth = 6;

                                rg = worksheet.get_Range("C3", Type.Missing);
                                rg.Cells.ColumnWidth = 30;

                                rg = worksheet.get_Range("D3", Type.Missing);
                                rg.Cells.ColumnWidth = 30;

                                rg = worksheet.get_Range("E3", Type.Missing);
                                rg.Cells.ColumnWidth = 30;

                                rg = worksheet.get_Range("F3", Type.Missing);
                                rg.Cells.ColumnWidth = 15;

                                rg = worksheet.get_Range("G3", Type.Missing);
                                rg.Cells.ColumnWidth = 10;

                                rg = worksheet.get_Range("I3", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("J3", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("K3", Type.Missing);
                                rg.Cells.ColumnWidth = 10;

                                Excel.Range rgHead = null;
                                rgHead = worksheet.get_Range("A1", "H2");
                                rgHead.Merge(Type.Missing);
                                rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.Value2 = "ESI DEDUCTIONS FOR THE MONTH OF " + dtExcel.Rows[0]["HPCM_WAGEMONTH"].ToString() + "";

                                int iColumn = 1;
                                worksheet.Cells[3, iColumn++] = "SlNo";
                                worksheet.Cells[3, iColumn++] = "Ecode";
                                worksheet.Cells[3, iColumn++] = "Name";
                                worksheet.Cells[3, iColumn++] = "Desig";
                                worksheet.Cells[3, iColumn++] = "Department";
                                worksheet.Cells[3, iColumn++] = "Doj";
                                worksheet.Cells[3, iColumn++] = "Company";
                                //worksheet.Cells[3, iColumn++] = "Branch";

                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumn) + "2", objUtilityDB.GetColumnName(iColumn + 7) + "2");
                                //rgHead.Cells.ColumnWidth = 5;
                                rgHead.Merge(Type.Missing);
                                rgHead.Value2 = "DEDUCTIONS";
                                rgHead.Interior.ColorIndex = 46;
                                rgHead.Borders.Weight = 2;
                                rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
                                rgHead.Cells.RowHeight = 20;
                                rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumn) + "3", objUtilityDB.GetColumnName(iColumn + 3) + "3");
                                rgHead.Interior.ColorIndex = 46;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Cells.ColumnWidth = 5;

                                worksheet.Cells[3, iColumn++] = "ESI Number";
                                worksheet.Cells[3, iColumn++] = "Present Days";
                                worksheet.Cells[3, iColumn++] = "Earned Gross Sal";
                                worksheet.Cells[3, iColumn++] = "ESI 1.75%";

                                rg = worksheet.get_Range("H3", Type.Missing);
                                rg.Cells.ColumnWidth = 20;

                                iColumn = 1;
                                for (int i = 0; i < dtExcel.Rows.Count; i++)
                                {
                                    worksheet.Cells[i + 4, iColumn++] = i + 1;
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_EORA_CODE"].ToString();
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_NAME"].ToString();
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_DESIG"].ToString();
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_DEPT_NAME"].ToString();
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_DOJ"].ToString();
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_COMPANY_CODE"].ToString();
                                    //worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_LOP"].ToString();

                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ESI_NUMBER"].ToString() + "'";
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PRE"].ToString();
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_TOTAL"].ToString();
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_ESI"].ToString();


                                    iColumn = 1;
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
                    else if (cbReportType.SelectedIndex == 6)
                    {

                        //childReportViewer = new ReportViewer(cbWagePeriod.SelectedValue.ToString(), strCmpData, strEmpData, "PAYREGPTAX");
                        //CommonData.ViewReport = "HR_PAYROLL_PAY_REG_PROFTAX";
                        //childReportViewer.Show();
                        try
                        {
                            objExDb = new ExcelDB();
                            objUtilityDB = new UtilityDB();
                            DataTable dtExcel = new DataTable();
                            dtExcel = objExDb.GetPayRollReportsData(cbWagePeriod.SelectedValue.ToString(), sCompData, strEmpData, "PAYREGPTAX").Tables[0];
                            objExDb = null;

                            if (dtExcel.Rows.Count > 0)
                            {
                                Excel.Application oXL = new Excel.Application();
                                Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                                Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                                oXL.Visible = true;
                                string sLastColumn = objUtilityDB.GetColumnName(10);
                                Excel.Range rg = worksheet.get_Range("A3", sLastColumn + "3");
                                Excel.Range rgData = worksheet.get_Range("A3", sLastColumn + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString());
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

                                rg = worksheet.get_Range("A3", Type.Missing);
                                rg.Cells.ColumnWidth = 4;

                                rg = worksheet.get_Range("B3", Type.Missing);
                                rg.Cells.ColumnWidth = 6;

                                rg = worksheet.get_Range("C3", Type.Missing);
                                rg.Cells.ColumnWidth = 30;

                                rg = worksheet.get_Range("D3", Type.Missing);
                                rg.Cells.ColumnWidth = 30;

                                rg = worksheet.get_Range("E3", Type.Missing);
                                rg.Cells.ColumnWidth = 30;

                                rg = worksheet.get_Range("F3", Type.Missing);
                                rg.Cells.ColumnWidth = 15;

                                rg = worksheet.get_Range("G3", Type.Missing);
                                rg.Cells.ColumnWidth = 10;

                                rg = worksheet.get_Range("I3", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("J3", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("K3", Type.Missing);
                                rg.Cells.ColumnWidth = 10;

                                Excel.Range rgHead = null;
                                rgHead = worksheet.get_Range("A1", "J2");
                                rgHead.Merge(Type.Missing);
                                rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.Value2 = "PROFFETIONAL TAX FOR THE MONTH OF " + dtExcel.Rows[0]["HPCM_WAGEMONTH"].ToString() + "";

                                int iColumn = 1;
                                worksheet.Cells[3, iColumn++] = "SlNo";
                                worksheet.Cells[3, iColumn++] = "Ecode";
                                worksheet.Cells[3, iColumn++] = "Name";
                                worksheet.Cells[3, iColumn++] = "Desig";
                                worksheet.Cells[3, iColumn++] = "Department";
                                worksheet.Cells[3, iColumn++] = "Doj";
                                worksheet.Cells[3, iColumn++] = "Company";
                                //worksheet.Cells[3, iColumn++] = "Branch";


                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumn) + "3", objUtilityDB.GetColumnName(iColumn + 2) + "3");
                                rgHead.Interior.ColorIndex = 46;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Cells.ColumnWidth = 5;

                                //worksheet.Cells[3, iColumn++] = "ESI Number";
                                worksheet.Cells[3, iColumn++] = "Present Days";
                                worksheet.Cells[3, iColumn++] = "Earned Gross Sal";
                                worksheet.Cells[3, iColumn++] = "Proff Tax";



                                iColumn = 1;
                                for (int i = 0; i < dtExcel.Rows.Count; i++)
                                {
                                    worksheet.Cells[i + 4, iColumn++] = i + 1;
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_EORA_CODE"].ToString();
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_NAME"].ToString();
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_DESIG"].ToString();
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_DEPT_NAME"].ToString();
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_DOJ"].ToString();
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_COMPANY_CODE"].ToString();
                                    //worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_LOP"].ToString();

                                    //worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ESI_NUMBER"].ToString() + "'";
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PRE"].ToString();
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_TOTAL"].ToString();
                                    worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_PROFTAX"].ToString();


                                    iColumn = 1;
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
                    else if (cbReportType.SelectedIndex == 7)
                    {

                        //childReportViewer = new ReportViewer(cbWagePeriod.SelectedValue.ToString(), strCmpData, strEmpData, "BANKWISESALARY");
                        //CommonData.ViewReport = "HR_PAYROLL_PAYLIST_BANKWISE";
                        //childReportViewer.Show();
                        try
                        {
                            //foreach (DataRowView view in clbCompany.CheckedItems)
                            //{
                            //    //strCmpData += "'" + (view[clbCompany.ValueMember].ToString()) + "',";

                            //    string strCompany = (view[clbCompany.ValueMember].ToString());
                            objExDb = new ExcelDB();
                            objUtilityDB = new UtilityDB();
                            DataTable dtExcel = new DataTable();

                            dtExcel = objExDb.GetPayRollReportsData(cbWagePeriod.SelectedValue.ToString(), sCompData, strEmpData, "BANKWISESALARY").Tables[0];
                            objExDb = null;

                            if (dtExcel.Rows.Count > 0)
                            {
                                Excel.Application oXL = new Excel.Application();
                                Excel.Sheets xlSheets = null;
                                Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                                xlSheets = theWorkbook.Sheets;
                                Excel.Worksheet worksheet = (Excel.Worksheet)xlSheets.Add(xlSheets[1], Type.Missing, Type.Missing, Type.Missing);
                                //(Excel.Worksheet)oXL.ActiveSheet;
                                worksheet.Name = dtExcel.Rows[0]["HPCM_COMPANY_CODE"].ToString() + "-" + dtExcel.Rows[0]["HPCM_PS_BANK_NAME"].ToString() + "-" + dtExcel.Rows[0]["HPCM_WAGEMONTH"].ToString();
                                oXL.Visible = true;
                                string sLastColumn = objUtilityDB.GetColumnName(6);
                                Excel.Range rg = worksheet.get_Range("A3", sLastColumn + "3");
                                Excel.Range rgData = worksheet.get_Range("A3", sLastColumn + "3");
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

                                rg = worksheet.get_Range("A3", Type.Missing);
                                rg.Cells.ColumnWidth = 4;

                                rg = worksheet.get_Range("B3", Type.Missing);
                                rg.Cells.ColumnWidth = 6;

                                rg = worksheet.get_Range("C3", Type.Missing);
                                rg.Cells.ColumnWidth = 40;

                                rg = worksheet.get_Range("D3", Type.Missing);
                                rg.Cells.ColumnWidth = 30;

                                rg = worksheet.get_Range("E3", Type.Missing);
                                rg.Cells.ColumnWidth = 15;

                                rg = worksheet.get_Range("F3", Type.Missing);
                                rg.Cells.ColumnWidth = 60;


                                Excel.Range rgHead = null;
                                rgHead = worksheet.get_Range("A1", "F2");
                                rgHead.Merge(Type.Missing);
                                rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.Value2 = dtExcel.Rows[0]["HPCM_COMPANY_CODE"].ToString() + "-" + dtExcel.Rows[0]["HPCM_PS_BANK_NAME"].ToString() +
                                                        " PAY SHEET FOR THE MONTH OF " + dtExcel.Rows[0]["HPCM_WAGEMONTH"].ToString() + "";

                                int iColumn = 1;
                                worksheet.Cells[3, iColumn++] = "SlNo";
                                worksheet.Cells[3, iColumn++] = "Ecode";
                                worksheet.Cells[3, iColumn++] = "Name";
                                worksheet.Cells[3, iColumn++] = "Account No";
                                worksheet.Cells[3, iColumn++] = "Salary";
                                worksheet.Cells[3, iColumn++] = "Salary in Words";

                                iColumn = 1;
                                int iRow = 0, iSheet = 1;
                                for (int i = 0; i < dtExcel.Rows.Count; i++)
                                {
                                    if (i > 0)
                                    {
                                        if (dtExcel.Rows[i]["HPCM_COMPANY_CODE"].ToString() != dtExcel.Rows[i - 1]["HPCM_COMPANY_CODE"].ToString()
                                            || dtExcel.Rows[i]["HPCM_PS_BANK_NAME"].ToString() != dtExcel.Rows[i - 1]["HPCM_PS_BANK_NAME"].ToString())
                                        {

                                            rgHead = worksheet.get_Range("E" + (worksheet.UsedRange.Rows.Count + 1).ToString(),
                                                                    "E" + (worksheet.UsedRange.Rows.Count + 1).ToString());

                                            rg = worksheet.get_Range("A" + (worksheet.UsedRange.Rows.Count + 1).ToString(),
                                                                    "D" + (worksheet.UsedRange.Rows.Count + 1).ToString());
                                            rg.Merge(Type.Missing);
                                            rg.Value2 = "TOTALS";
                                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 14;
                                            rg.Font.ColorIndex = 30;
                                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                                            rg.HorizontalAlignment = Excel.Constants.xlCenter;

                                            rgHead.Borders.Weight = 2;
                                            rgHead.Font.Size = 12; rgHead.Font.Bold = true;

                                            iColumn = 5;
                                            worksheet.Cells[worksheet.UsedRange.Rows.Count, 5] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (worksheet.UsedRange.Rows.Count - 1).ToString() + ")";


                                            iRow = 0;
                                            iSheet++;
                                            //theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                                            worksheet = worksheet = (Excel.Worksheet)xlSheets.Add(xlSheets[iSheet], Type.Missing, Type.Missing, Type.Missing);
                                            //worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                                            worksheet.Name = dtExcel.Rows[i]["HPCM_COMPANY_CODE"].ToString() + "-" + dtExcel.Rows[i]["HPCM_PS_BANK_NAME"].ToString() + "-" + dtExcel.Rows[i]["HPCM_WAGEMONTH"].ToString();
                                            //oXL.Visible = true;                                        
                                            rg = worksheet.get_Range("A3", sLastColumn + "3");
                                            rgData = worksheet.get_Range("A3", sLastColumn + "3");
                                            rgData.Font.Size = 11;
                                            rgData.WrapText = true;
                                            rgData.VerticalAlignment = Excel.Constants.xlCenter;
                                            rgData.Borders.Weight = 2;

                                            iColumn = 1;

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

                                            rg = worksheet.get_Range("A3", Type.Missing);
                                            rg.Cells.ColumnWidth = 4;

                                            rg = worksheet.get_Range("B3", Type.Missing);
                                            rg.Cells.ColumnWidth = 6;

                                            rg = worksheet.get_Range("C3", Type.Missing);
                                            rg.Cells.ColumnWidth = 40;

                                            rg = worksheet.get_Range("D3", Type.Missing);
                                            rg.Cells.ColumnWidth = 30;

                                            rg = worksheet.get_Range("E3", Type.Missing);
                                            rg.Cells.ColumnWidth = 15;

                                            rg = worksheet.get_Range("F3", Type.Missing);
                                            rg.Cells.ColumnWidth = 60;


                                            rgHead = null;
                                            rgHead = worksheet.get_Range("A1", "F2");
                                            rgHead.Merge(Type.Missing);
                                            rgHead.Font.Size = 14;
                                            rgHead.Font.ColorIndex = 1;
                                            rgHead.Font.Bold = true;
                                            rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                            rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                            rgHead.Cells.Value2 = dtExcel.Rows[i]["HPCM_COMPANY_CODE"].ToString() + "-" + dtExcel.Rows[i]["HPCM_PS_BANK_NAME"].ToString() +
                                                        " PAY SHEET FOR THE MONTH OF " + dtExcel.Rows[i]["HPCM_WAGEMONTH"].ToString() + "";


                                            worksheet.Cells[3, iColumn++] = "SlNo";
                                            worksheet.Cells[3, iColumn++] = "Ecode";
                                            worksheet.Cells[3, iColumn++] = "Name";
                                            worksheet.Cells[3, iColumn++] = "Account No";
                                            worksheet.Cells[3, iColumn++] = "Salary";
                                            worksheet.Cells[3, iColumn++] = "Salary in Words";

                                            iColumn = 1;
                                        }
                                    }


                                    rgData = worksheet.get_Range("A" + (iRow + 4).ToString(), sLastColumn + (iRow + 4).ToString());
                                    rgData.Font.Size = 11;
                                    rgData.WrapText = true;
                                    rgData.VerticalAlignment = Excel.Constants.xlCenter;
                                    rgData.Borders.Weight = 2;

                                    worksheet.Cells[iRow + 4, iColumn++] = iRow + 1;
                                    worksheet.Cells[iRow + 4, iColumn++] = dtExcel.Rows[i]["HPCM_EORA_CODE"].ToString();
                                    worksheet.Cells[iRow + 4, iColumn++] = dtExcel.Rows[i]["HPCM_NAME"].ToString();
                                    worksheet.Cells[iRow + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_BANK_NAME"].ToString() + "-" + dtExcel.Rows[i]["HPCM_PS_BANK_ACCOUNT_NO"].ToString();
                                    worksheet.Cells[iRow + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_NET_PAY"].ToString();
                                    worksheet.Cells[iRow + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_NET_PAY_NTOW"].ToString();

                                    iRow++;
                                    iColumn = 1;
                                }
                                rgHead = worksheet.get_Range("E" + (worksheet.UsedRange.Rows.Count + 1).ToString(),
                                                                    "E" + (worksheet.UsedRange.Rows.Count + 1).ToString());

                                rg = worksheet.get_Range("A" + (worksheet.UsedRange.Rows.Count + 1).ToString(),
                                                        "D" + (worksheet.UsedRange.Rows.Count + 1).ToString());
                                rg.Merge(Type.Missing);
                                rg.Value2 = "TOTALS";
                                rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 14;
                                rg.Font.ColorIndex = 30;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;

                                rgHead.Borders.Weight = 2;
                                rgHead.Font.Size = 12; rgHead.Font.Bold = true;

                                iColumn = 5;

                                worksheet.Cells[worksheet.UsedRange.Rows.Count, 5] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (worksheet.UsedRange.Rows.Count - 1).ToString() + ")";
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
            }
            
        }
       

        private void SearchEcode(string searchString, ListBox cbEcodes)
        {
            if (searchString.Trim().Length > 0)
            {
                for (int i = 0; i < cbEcodes.Items.Count; i++)
                {
                    if (cbEcodes.Items[i].ToString() == "System.Data.DataRowView")  // for listbox search
                    {
                        if (((System.Data.DataRowView)(cbEcodes.Items[i])).Row.ItemArray[1].ToString().IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            cbEcodes.SetSelected(i, true);
                            break;
                        }
                        else
                            cbEcodes.SetSelected(i, false);

                    }
                    else  // for checkbox list search
                    {
                        if (cbEcodes.Items[i].ToString().IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            cbEcodes.SetSelected(i, true);
                            break;
                        }
                        else
                            cbEcodes.SetSelected(i, false);
                    }

                }
            }
        }

        private void cbWagePeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSelectedValues();
            if (sCompData.Length > 0)
            {
                FillEmployeeList();
            }
        }
      
       
    }
}
