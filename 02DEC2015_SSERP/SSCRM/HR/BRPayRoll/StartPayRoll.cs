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
    public partial class StartPayRoll : Form
    {
        SQLDB objSQLdb = null;

        public StartPayRoll()
        {
            InitializeComponent();
        }

        private void StartPayRoll_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            dtpDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));

            if (CommonData.BranchType.Equals("BR"))
            {
                FillBranchData();
            }
            else
            {
                FillBranchData();
                cbBranches.SelectedIndex = 0;
            }
        }



        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT DISTINCT CM_COMPANY_NAME ,CM_COMPANY_CODE " +
                                   " FROM USER_BRANCH " +
                                   " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                   " INNER JOIN COMPANY_MAS ON CM_COMPANY_CODE = COMPANY_CODE " +
                                   " WHERE UB_USER_ID ='" + CommonData.LogUserId +
                                   "' ORDER BY CM_COMPANY_NAME";

                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbCompany.DataSource = dt;
                    cbCompany.DisplayMember = "CM_COMPANY_NAME";
                    cbCompany.ValueMember = "CM_COMPANY_CODE";
                }

                cbCompany.SelectedValue = CommonData.CompanyCode;
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
                    string strCommand = "SELECT DISTINCT BRANCH_NAME,BRANCH_CODE+'@'+STATE_CODE as branchCode " +
                                        " FROM USER_BRANCH " +
                                        " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                        " WHERE COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() +
                                        "' AND UB_USER_ID ='" + CommonData.LogUserId +
                                        "' AND BRANCH_TYPE='BR' ORDER BY BRANCH_NAME ASC";
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
                    cbBranches.ValueMember = "branchCode";

                }

                string BranCode = CommonData.BranchCode + '@' + CommonData.StateCode;
                cbBranches.SelectedValue = BranCode;
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

       

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                FillBranchData();
                cbBranches.SelectedIndex = 0;
            }
            else
            {
                cbBranches.DataSource = null;
            }
        }

       

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbCompany.SelectedIndex = 0;
            cbBranches.SelectedIndex = 0;
            dtpDocMonth.Value = DateTime.Today;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private bool CheckData()
        {
            bool flag = true;

            objSQLdb = new SQLDB();           
            DataTable dt = new DataTable();
            string strCommand = "", Status = "";
                     
            
            if (cbCompany.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Company","SSERP-PAYROLL",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return flag;
            }

            if (cbBranches.SelectedIndex == 0 || cbBranches.SelectedIndex == -1)
            {
                flag = false;
                MessageBox.Show("Please Select Branch", "SSERP-PAYROLL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;
            }

            if (dtpDocMonth.Value > DateTime.Today)
            {
                flag = false;
                MessageBox.Show("Please Select Valid Month ", "SSERP-PAYROLL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;
            }

            if (cbCompany.SelectedIndex > 0 && cbBranches.SelectedIndex > 0)
            {
                strCommand = "SELECT BPWP_STATUS FROM BR_PAYROLL_WAGE_PERIOD " +
                             " WHERE BPWP_COMP_CODE='" + cbCompany.SelectedValue.ToString() +
                             "' AND BPWP_BRANCH_CODE='" + cbBranches.SelectedValue.ToString().Split('@')[0] +
                             "' AND BPWP_DOC_MONTH < '" + Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper() +"' ";

                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count> 0)
                {
                    Status = dt.Rows[0]["BPWP_STATUS"].ToString();

                    if (Status.Equals("CLOSED"))
                    {
                        flag = true;
                        return flag;
                    }
                    else
                    {
                        flag = false;
                        MessageBox.Show("Previous Month PAYROLL Was On The Process", "SSERP-PAYROLL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return flag;
                    }

                }

                strCommand = "";
                Status = "";

                strCommand = "SELECT BPWP_STATUS FROM BR_PAYROLL_WAGE_PERIOD "+
                             " WHERE BPWP_COMP_CODE='"+ cbCompany.SelectedValue.ToString() +
                             "' AND BPWP_BRANCH_CODE='"+ cbBranches.SelectedValue.ToString().Split('@')[0] +
                             "' AND BPWP_DOC_MONTH='"+ Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper() +"'";

                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    Status = dt.Rows[0]["BPWP_STATUS"].ToString();

                }

                if (Status.Equals("RUNNING"))
                {
                    flag = false;
                    MessageBox.Show("Already PAYROLL Was Running For The Month  "+ Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper() +"", "SSERP-PAYROLL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return flag;

                }
                else if (Status.Equals("PROCESSED"))
                {
                    flag = false;
                    MessageBox.Show("Already PAYROLL Was Processed For The Month " + Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper() + "", "SSERP-PAYROLL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return flag;

                }
                else if (Status.Equals("CLOSED"))
                {
                    flag = false;
                    MessageBox.Show("PAYROLL Was CLOSED For The Month " + Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper() + "", "SSERP-PAYROLL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return flag;

                }

            }

            return flag;

        }



        private void btnStart_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "";
            DateTime FirstDayOfMonth;
            DateTime LastDayOfMonth;

            if (CheckData() == true)
            {
                try
                {
                    FirstDayOfMonth = new DateTime(dtpDocMonth.Value.Year, dtpDocMonth.Value.Month, 01);
                    LastDayOfMonth = new DateTime(dtpDocMonth.Value.Year, dtpDocMonth.Value.Month, DateTime.DaysInMonth(dtpDocMonth.Value.Year, dtpDocMonth.Value.Month));

                    strCommand += "INSERT INTO BR_PAYROLL_WAGE_PERIOD(BPWP_COMP_CODE " +
                                                                  ", BPWP_STATE_CODE " +
                                                                  ", BPWP_BRANCH_CODE " +
                                                                  ", BPWP_FIN_YEAR " +
                                                                  ", BPWP_DOC_MONTH " +
                                                                  ", BPWP_PAYROLL_TYPE " +
                                                                  ", BPWP_FROM_DATE " +
                                                                  ", BPWP_TO_DATE " +
                                                                  ", BPWP_STATUS " +
                                                                  ", BPWP_CREATED_BY " +
                                                                  ", BPWP_CREATED_DATE " +
                                                                  ")VALUES " +
                                                                  "('" + cbCompany.SelectedValue.ToString() +
                                                                  "','" + cbBranches.SelectedValue.ToString().Split('@')[1] +
                                                                  "','" + cbBranches.SelectedValue.ToString().Split('@')[0] +
                                                                  "','" + CommonData.FinancialYear +
                                                                  "','" + Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper() +
                                                                  "','OFFICE','" + Convert.ToDateTime(FirstDayOfMonth).ToString("dd/MMM/yyyy") +
                                                                  "','" + Convert.ToDateTime(LastDayOfMonth).ToString("dd/MMM/yyyy") +
                                                                  "','RUNNING','" + CommonData.LogUserId +
                                                                  "',getdate())";

                    strCommand += "INSERT INTO BR_PAYROLL_WAGE_PERIOD(BPWP_COMP_CODE " +
                                                                  ", BPWP_STATE_CODE " +
                                                                  ", BPWP_BRANCH_CODE " +
                                                                  ", BPWP_FIN_YEAR " +
                                                                  ", BPWP_DOC_MONTH " +
                                                                  ", BPWP_PAYROLL_TYPE " +
                                                                  ", BPWP_FROM_DATE " +
                                                                  ", BPWP_TO_DATE " +
                                                                  ", BPWP_STATUS " +
                                                                  ", BPWP_CREATED_BY " +
                                                                  ", BPWP_CREATED_DATE " +
                                                                  ")VALUES " +
                                                                  "('" + cbCompany.SelectedValue.ToString() +
                                                                  "','" + cbBranches.SelectedValue.ToString().Split('@')[1] +
                                                                  "','" + cbBranches.SelectedValue.ToString().Split('@')[0] +
                                                                  "','" + CommonData.FinancialYear +
                                                                  "','" + Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper() +
                                                                  "','TM2DDSM','" + Convert.ToDateTime(FirstDayOfMonth).ToString("dd/MMM/yyyy") +
                                                                  "','" + Convert.ToDateTime(LastDayOfMonth).ToString("dd/MMM/yyyy") +
                                                                  "','RUNNING','" + CommonData.LogUserId +
                                                                  "',getdate())";

                    strCommand += "INSERT INTO BR_PAYROLL_WAGE_PERIOD(BPWP_COMP_CODE " +
                                                                  ", BPWP_STATE_CODE " +
                                                                  ", BPWP_BRANCH_CODE " +
                                                                  ", BPWP_FIN_YEAR " +
                                                                  ", BPWP_DOC_MONTH " +
                                                                  ", BPWP_PAYROLL_TYPE " +
                                                                  ", BPWP_FROM_DATE " +
                                                                  ", BPWP_TO_DATE " +
                                                                  ", BPWP_STATUS " +
                                                                  ", BPWP_CREATED_BY " +
                                                                  ", BPWP_CREATED_DATE " +
                                                                  ")VALUES " +
                                                                  "('" + cbCompany.SelectedValue.ToString() +
                                                                  "','" + cbBranches.SelectedValue.ToString().Split('@')[1] +
                                                                  "','" + cbBranches.SelectedValue.ToString().Split('@')[0] +
                                                                  "','" + CommonData.FinancialYear +
                                                                  "','" + Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper() +
                                                                  "','SR2GC','" + Convert.ToDateTime(FirstDayOfMonth).ToString("dd/MMM/yyyy") +
                                                                  "','" + Convert.ToDateTime(LastDayOfMonth).ToString("dd/MMM/yyyy") +
                                                                  "','RUNNING','" + CommonData.LogUserId +
                                                                  "',getdate())";



                    if (strCommand.Length > 10)
                    {
                        iRes = objSQLdb.ExecuteSaveData(strCommand);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                if (iRes > 0)
                {
                    MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbCompany.SelectedIndex = 0;
                    cbBranches.SelectedIndex = -1;              

                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        

    }
}
