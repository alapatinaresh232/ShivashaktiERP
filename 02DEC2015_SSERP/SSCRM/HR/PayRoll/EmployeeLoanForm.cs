using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SSCRMDB;

namespace SSCRM
{
    public partial class EmployeeLoanForm : Form
    {
        SQLDB objSQLdb = null;
        private bool flagUpdate = false;

        public EmployeeLoanForm()
        {
            InitializeComponent();
        }

      
        private void EmployeeLoanForm_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            GenerateLoanNo();
            FillLoanTypes();
            dtpWagePeriod.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
            dtpSanctionedDate.Value = DateTime.Today;

            gvEmpLoanDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                       System.Drawing.FontStyle.Regular);

        }

        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT CM_COMPANY_CODE,CM_COMPANY_NAME "+
                                " FROM COMPANY_MAS "+
                                " WHERE ACTIVE='T' ORDER BY CM_COMPANY_NAME";
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

                    string strCommand = "SELECT BRANCH_NAME ,BRANCH_CODE " +
                                        " FROM BRANCH_MAS "+
                                        " where COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                        "' AND BRANCH_TYPE IN('BR','HO','SP','PU') Order by BRANCH_NAME ASC";
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

        private void FillLoanTypes()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT HPLTM_LOAN_TYPE  FROM HR_PARYOLL_LOAN_TYPE_MASTER "+
                                " ORDER BY HPLTM_LOAN_TYPE";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";                   

                    dt.Rows.InsertAt(row, 0);

                    cbLoanType.DataSource = dt;
                    cbLoanType.DisplayMember = "HPLTM_LOAN_TYPE";
                    cbLoanType.ValueMember = "HPLTM_LOAN_TYPE";
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

        private void GenerateLoanNo()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                String strCommand = "SELECT ISNULL(MAX(CAST(HPLTH_LOAN_NUMBER AS NUMERIC)),0)+1 TranNo FROM HR_PAYROLL_LOAN_HEAD";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtLoanNo.Text = dt.Rows[0]["TranNo"] + "";
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


        private bool CheckData()
        {
            
            bool flag = true;
            if (txtEName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Emp Ecode","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
                txtEcodeSearch.Focus();
            }
            else if (cbCompany.SelectedIndex == 0)
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
            else if (cbLoanType.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Loan Type", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbLoanType.Focus();
            }
            else if (txtLoanNo.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Loan Number", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLoanNo.Focus();
            }
            else if (txtLoanAmt.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Loan Amount", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLoanAmt.Focus();
            }
            else if (txtLoanSanctionedMonths.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Loan Sanctioned Months", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLoanSanctionedMonths.Focus();
            }
            //else if (cbPaymentMode.SelectedIndex == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Select Payment Mode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    cbPaymentMode.Focus();
            //}
            //else if (txtReqMonths.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter Loan Requested Months", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtReqMonths.Focus();
            //}
            //else if (txtEmi.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter EMI", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtEmi.Focus();
            //}
            //else if (txtIntRate.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter Loan Interest Rate", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtIntRate.Focus();
            //}
            else if (txtLoanPurpose.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Loan Purpose", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLoanPurpose.Focus();
            }
            else if (txtApprovedName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Approved Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtApprovedByEcode.Focus();
            }
            else if (txtRecEmpName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Recommended Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRecommendedEcode.Focus();
            }
            //else if (txtCoEmpName.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter Co-Applicant Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtCoEmpEcode.Focus();
            //}

            objSQLdb = new SQLDB();
            string strCommand = "";
            DataTable dt = new DataTable();


            strCommand = "SELECT * FROM HR_PAYROLL_LOAN_REPAY_SCHEDULE " +
                              " WHERE HPLRS_LOAN_EMI_STATUS='RECOVERED' AND HPLRS_LOAN_NUMBER='" + txtLoanNo.Text.ToString() + "'";
            dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
            if (dt.Rows.Count > 0)
            {
                flag = false;
                MessageBox.Show("Loan Amt Is Recovered! Data Can Not Be Manipulated","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return flag;
            }

            return flag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "";
            DataTable dt = new DataTable();

            if (CheckData() == true)
            {
                try
                {
                    //if (txtEmi.Text.Length == 0)
                    //{
                    //    txtEmi.Text = "0";
                    //}
                    if (txtIntRate.Text.Length == 0)
                    {
                        txtIntRate.Text = "0";
                    }
                    if (txtCoEmpEcode.Text.Length == 0)
                    {
                        txtCoEmpEcode.Text = "0";
                    }
                    if (txtReqMonths.Text.Length == 0)
                    {
                        txtReqMonths.Text = "0";
                    }

                    strCommand = "DELETE FROM HR_PAYROLL_LOAN_REPAY_SCHEDULE WHERE HPLRS_LOAN_NUMBER='" + txtLoanNo.Text + "'";
                    int iDel = objSQLdb.ExecuteSaveData(strCommand);

                    strCommand = "";
                    if (flagUpdate == true)
                    {

                        strCommand = "UPDATE HR_PAYROLL_LOAN_HEAD SET HPLTH_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                    "',HPLTH_BRANCH_CODE ='" + cbBranches.SelectedValue.ToString() +
                                    "',HPLTH_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) +
                                    ", HPLTH_LOAN_TYPE ='" + cbLoanType.SelectedValue.ToString() + "',HPLTH_LOAN_AMOUNT=" + txtLoanAmt.Text +
                                    ", HPLTH_LOAN_SANCTION_DATE ='" + Convert.ToDateTime(dtpSanctionedDate.Value).ToString("dd/MMM/yyyy") +
                                    "',HPLTH_LOAN_STARTING_WAGE_PERIOD ='" + Convert.ToDateTime(dtpWagePeriod.Value).ToString("MMMyyyy").ToUpper() +
                                    "',HPLTH_EMI =" + txtEmi.Text + ", HPLTH_MONTHS =" + txtLoanSanctionedMonths.Text +
                                    ", HPLTH_INTR_RATE=" + txtIntRate.Text + ", HPLTH_LOAN_PURPOSE='" + txtLoanPurpose.Text.ToString() +
                                    "',HPLTH_LOAN_AMOUNT_REQUIRED =" + txtLoanAmt.Text +
                                    ", HPLTH_REPAYMENT_PERIOD=" + Convert.ToInt32(txtReqMonths.Text) + ",HPLTH_CO_ECODE=" + Convert.ToInt32(txtCoEmpEcode.Text) +
                                    ", HPLTH_RECO_ECODE =" + Convert.ToInt32(txtRecommendedEcode.Text) + ",HPLTH_APPR_ECODE=" + Convert.ToInt32(txtApprovedByEcode.Text) +
                                    " WHERE HPLTH_LOAN_NUMBER='" + txtLoanNo.Text.ToString() + "'";


                        flagUpdate = false;
                    }

                    else
                    {
                        strCommand = "INSERT INTO HR_PAYROLL_LOAN_HEAD(HPLTH_COMPANY_CODE " +
                                                                    ", HPLTH_BRANCH_CODE " +
                                                                    ", HPLTH_EORA_CODE " +
                                                                    ", HPLTH_LOAN_TYPE " +
                                                                    ", HPLTH_LOAN_NUMBER " +
                                                                    ", HPLTH_LOAN_AMOUNT " +
                                                                    ", HPLTH_LOAN_SANCTION_DATE " +
                                                                    ", HPLTH_LOAN_STARTING_WAGE_PERIOD " +
                                                                    ", HPLTH_EMI " +
                                                                    ", HPLTH_MONTHS " +
                                                                    ", HPLTH_INTR_RATE " +
                                                                    ", HPLTH_LOAN_PURPOSE " +
                                                                    ", HPLTH_LOAN_AMOUNT_REQUIRED " +
                                                                    ", HPLTH_REPAYMENT_PERIOD " +
                                                                    ", HPLTH_CO_ECODE " +
                                                                    ", HPLTH_RECO_ECODE " +
                                                                    ", HPLTH_APPR_ECODE " +
                                                                    ", HPLTH_CREATED_BY " +
                                                                    ", HPLTH_CREATED_DATE " +
                                                                    ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                    "','" + cbBranches.SelectedValue.ToString() +
                                                                    "'," + Convert.ToInt32(txtEcodeSearch.Text) +
                                                                    ",'" + cbLoanType.SelectedValue.ToString() +
                                                                    "','" + txtLoanNo.Text.ToString() +
                                                                    "'," + Convert.ToInt32(txtLoanAmt.Text) +
                                                                    ",'" + Convert.ToDateTime(dtpSanctionedDate.Value).ToString("dd/MMM/yyyy") +
                                                                    "','" + Convert.ToDateTime(dtpWagePeriod.Value).ToString("MMMyyyy").ToUpper() +
                                                                    "'," + Convert.ToInt32(txtEmi.Text) +
                                                                    "," + Convert.ToInt32(txtLoanSanctionedMonths.Text) +
                                                                    "," + Convert.ToInt32(txtIntRate.Text) +
                                                                    ",'" + txtLoanPurpose.Text.ToString() +
                                                                    "'," + Convert.ToInt32(txtLoanAmt.Text) +
                                                                    "," + Convert.ToInt32(txtReqMonths.Text) +
                                                                    "," + Convert.ToInt32(txtCoEmpEcode.Text) +
                                                                    "," + Convert.ToInt32(txtRecommendedEcode.Text) +
                                                                    "," + Convert.ToInt32(txtApprovedByEcode.Text) +
                                                                    ",'" + CommonData.LogUserId +
                                                                    "',getdate())";
                    }
                    
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
                    if (SaveLoanRepayMonthsDetails() > 0)
                    {
                        MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flagUpdate = false;
                        btnCancel_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        strCommand = "DELETE FROM HR_PAYROLL_LOAN_HEAD WHERE HPLTH_LOAN_NUMBER='" + txtLoanNo.Text + "'";
                        int iDel = objSQLdb.ExecuteSaveData(strCommand);
                        GenerateLoanNo();

                    }

                }
                else
                {
                   
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

        }

        private int SaveLoanRepayMonthsDetails()
        {
            objSQLdb = new SQLDB();
            int iRec = 0;
            string strCommand = "";
            int nSlNo = 1;
            double EmiMonths = 0;

            try
            {

                EmiMonths = Convert.ToInt32(txtLoanSanctionedMonths.Text);

                //strCommand = "DELETE FROM HR_PAYROLL_LOAN_REPAY_SCHEDULE WHERE HPLRS_LOAN_NUMBER='" + txtLoanNo.Text + "'";
                //int iDel = objSQLdb.ExecuteSaveData(strCommand);
                strCommand = "";

                for (int i = 0; i < EmiMonths; i++)
                {
                    strCommand += "INSERT INTO HR_PAYROLL_LOAN_REPAY_SCHEDULE(HPLRS_COMPANY_CODE " +
                                                                          ", HPLRS_BRANCH_CODE " +
                                                                          ", HPLRS_EORA_CODE " +
                                                                          ", HPLRS_LOAN_SLNO " +
                                                                          ", HPLRS_LOAN_TYPE " +
                                                                          ", HPLRS_LOAN_NUMBER " +
                                                                          ", HPLRS_WAGE_MONTH " +
                                                                          ", HPLRS_LOAN_EMI " +
                                                                          ", HPLRS_LOAN_EMI_STATUS " +                                                                         
                                                                          ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                          "','" + cbBranches.SelectedValue.ToString() +
                                                                          "'," + Convert.ToInt32(txtEcodeSearch.Text) +
                                                                          "," + (nSlNo++) +
                                                                          ",'" + cbLoanType.SelectedValue.ToString() +
                                                                          "','" + txtLoanNo.Text.ToString() +
                                                                          "','" + Convert.ToDateTime(dtpWagePeriod.Value.AddMonths(i)).ToString("MMMyyyy").ToUpper() +
                                                                          "'," + Convert.ToInt32(txtEmi.Text) +
                                                                          ",'FUTURE EMI')";
                }

                if (strCommand.Length > 10)
                {
                    iRec = objSQLdb.ExecuteSaveData(strCommand);
                }
                if (iRec > 0)
                {
                    return iRec;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iRec;
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                FillBranchData();
            }
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text.Length == 0)
            {
                btnCancel_Click(null,null);
            }
        }

        private void txtCoEmpEcode_KeyUp(object sender, KeyEventArgs e)
        {
            objSQLdb = new SQLDB();
            string strCommand = "";
            DataTable dt = new DataTable();
            if (txtCoEmpEcode.Text!="")
            {
                try
                {
                    strCommand = "SELECT MEMBER_NAME,company_code,BRANCH_CODE " +
                                " FROM EORA_MASTER " +
                                " WHERE ECODE=" + Convert.ToInt32(txtCoEmpEcode.Text) + "";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        txtCoEmpName.Text = dt.Rows[0]["MEMBER_NAME"].ToString();

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
            else
            {
                txtCoEmpName.Text = "";
            }
        }

        private void txtRecommendedEcode_KeyUp(object sender, KeyEventArgs e)
        {
            objSQLdb = new SQLDB();
            string strCommand = "";
            DataTable dt = new DataTable();
            if (txtRecommendedEcode.Text!="")
            {
                try
                {
                    strCommand = "SELECT MEMBER_NAME,company_code,BRANCH_CODE " +
                                " FROM EORA_MASTER " +
                                " WHERE ECODE=" + Convert.ToInt32(txtRecommendedEcode.Text) + "";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        txtRecEmpName.Text = dt.Rows[0]["MEMBER_NAME"].ToString();

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
            else
            {
                txtRecEmpName.Text = "";
            }

        }

        private void txtApprovedByEcode_KeyUp(object sender, KeyEventArgs e)
        {
            objSQLdb = new SQLDB();
            string strCommand = "";
            DataTable dt = new DataTable();
            if (txtApprovedByEcode.Text!="")
            {
                try
                {
                    strCommand = "SELECT MEMBER_NAME,company_code,BRANCH_CODE " +
                                " FROM EORA_MASTER " +
                                " WHERE ECODE=" + Convert.ToInt32(txtApprovedByEcode.Text) + "";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        txtApprovedName.Text = dt.Rows[0]["MEMBER_NAME"].ToString();

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
            else
            {
                txtApprovedName.Text = "";
            }


        }

        private void txtEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtCoEmpEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtRecommendedEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtApprovedByEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtLoanAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

    

        private void txtIntRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtLoanSanctionedMonths_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtLoanNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            flagUpdate = false;

            txtEcodeSearch.Text = "";
            txtEName.Text = "";
            //txtLoanNo.Text = "";
            txtLoanPurpose.Text = "";
            txtLoanAmt.Text = "";
            txtEmi.Text = "";
            txtReqMonths.Text = "";
            txtLoanSanctionedMonths.Text = "";
            cbCompany.SelectedIndex = 0;
            cbBranches.SelectedIndex = -1;
            dtpSanctionedDate.Value = DateTime.Today;
            dtpWagePeriod.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
            txtIntRate.Text = "";
            txtRecEmpName.Text = "";
            txtRecommendedEcode.Text = "";
            txtApprovedByEcode.Text = "";
            txtApprovedName.Text = "";
            txtCoEmpEcode.Text = "";
            txtCoEmpName.Text = "";
            cbLoanType.SelectedIndex = 0;
            gvEmpLoanDetails.Rows.Clear();

            GenerateLoanNo();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtEcodeSearch_Validated(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            string strCommand = "";
            DataTable dt = new DataTable();
            if (txtEcodeSearch.Text != "")
            {
                try
                {
                    strCommand = "SELECT MEMBER_NAME,company_code,BRANCH_CODE " +
                                " FROM EORA_MASTER " +
                                " WHERE ECODE=" + Convert.ToInt32(txtEcodeSearch.Text) + "";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        txtEName.Text = dt.Rows[0]["MEMBER_NAME"].ToString();
                        cbCompany.SelectedValue = dt.Rows[0]["company_code"].ToString();
                        cbBranches.SelectedValue = dt.Rows[0]["BRANCH_CODE"].ToString();

                        FillEmpPrevoiusLoanDetails();
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
            else
            {
                txtEName.Text = "";
                cbCompany.SelectedIndex = 0;
                cbBranches.SelectedIndex = -1;
            }

        }

        private DataSet GetEmpPreviousLoanDetails(Int32 Ecode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xEcode", DbType.Int32, Ecode, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_EmpPreviousLoanDetails", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }


        private void FillEmpPrevoiusLoanDetails()
        {
            DataTable dt = new DataTable();
            gvEmpLoanDetails.Rows.Clear();
            double BalAmount = 0;
           
           
            try
            {
                dt = GetEmpPreviousLoanDetails(Convert.ToInt32(txtEcodeSearch.Text)).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        gvEmpLoanDetails.Rows.Add();
                        
                        gvEmpLoanDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                        gvEmpLoanDetails.Rows[i].Cells["LoanNo"].Value = dt.Rows[i]["LoanNumber"].ToString();
                        gvEmpLoanDetails.Rows[i].Cells["LoanType"].Value = dt.Rows[i]["LoanType"].ToString();
                        gvEmpLoanDetails.Rows[i].Cells["LoanAmt"].Value = dt.Rows[i]["LoanAmt"].ToString();
                        gvEmpLoanDetails.Rows[i].Cells["AmtRecovered"].Value = dt.Rows[i]["RecoveredAmt"].ToString();
                        BalAmount = Convert.ToDouble(dt.Rows[i]["LoanAmt"].ToString()) - Convert.ToDouble(dt.Rows[i]["RecoveredAmt"].ToString());
                        gvEmpLoanDetails.Rows[i].Cells["BalanceAmt"].Value = BalAmount.ToString("0.00");
                    }
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

        private DataSet GetEmpLoanDetails(string strLoanNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xLoanNumber", DbType.String, strLoanNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_EmpLoanDetails", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;

        }

        private void txtLoanNo_Validated(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (txtLoanNo.Text != "")
            {
                try
                {
                    dt = GetEmpLoanDetails(txtLoanNo.Text.ToString()).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        flagUpdate = true;


                        txtEcodeSearch.Text = dt.Rows[0]["Ecode"].ToString();
                        cbCompany.SelectedValue = dt.Rows[0]["CompanyCode"].ToString();
                        cbBranches.SelectedValue = dt.Rows[0]["BranchCode"].ToString();
                        cbLoanType.SelectedValue = dt.Rows[0]["LoanType"].ToString();
                        dtpSanctionedDate.Value = Convert.ToDateTime(dt.Rows[0]["SanctionedDate"].ToString());
                        dtpWagePeriod.Value = Convert.ToDateTime(dt.Rows[0]["WagePeriod"].ToString());
                        txtEmi.Text = Convert.ToDouble(dt.Rows[0]["Emi"]).ToString("0");
                        txtLoanAmt.Text = Convert.ToDouble(dt.Rows[0]["LoanAmt"]).ToString("0");
                        txtIntRate.Text = Convert.ToDouble(dt.Rows[0]["IntRate"]).ToString("0");
                        txtLoanPurpose.Text = dt.Rows[0]["LoanPupose"].ToString();
                        txtRecommendedEcode.Text = dt.Rows[0]["Recomended_Ecode"].ToString();
                        txtReqMonths.Text = dt.Rows[0]["RepaymentPeriod"].ToString();
                        txtCoEmpEcode.Text = dt.Rows[0]["Co_Emp_Ecode"].ToString();
                        txtApprovedByEcode.Text = dt.Rows[0]["Appr_Ecode"].ToString();
                        txtEName.Text = dt.Rows[0]["ApplicantName"].ToString();
                        txtRecEmpName.Text = dt.Rows[0]["RecommendedName"].ToString();
                        txtCoEmpName.Text = dt.Rows[0]["Co_AppName"].ToString();
                        txtApprovedName.Text = dt.Rows[0]["ApprovName"].ToString();
                        txtLoanSanctionedMonths.Text = Convert.ToDouble(dt.Rows[0]["SanctionedMonths"]).ToString("0");
                        FillEmpPrevoiusLoanDetails();
                    }
                    else
                    {
                        flagUpdate = false;
                        GenerateLoanNo();
                        txtLoanPurpose.Text = "";
                        txtLoanAmt.Text = "";
                        txtEmi.Text = "";
                        txtReqMonths.Text = "";
                        txtLoanSanctionedMonths.Text = "";
                        //cbCompany.SelectedIndex = 0;
                        //cbBranches.SelectedIndex = -1;
                        dtpSanctionedDate.Value = DateTime.Today;
                        dtpWagePeriod.Value = DateTime.Today; 
                        txtIntRate.Text = "";
                        txtRecEmpName.Text = "";
                        txtRecommendedEcode.Text = "";
                        txtApprovedByEcode.Text = "";
                        txtApprovedName.Text = "";
                        txtCoEmpEcode.Text = "";
                        txtCoEmpName.Text = "";
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    dt = null;
                }
            }
            else
            {
                flagUpdate = false;
                //txtEcodeSearch.Text = "";
                //txtEName.Text = "";
                //txtLoanNo.Text = "";
                txtLoanPurpose.Text = "";
                txtLoanAmt.Text = "";
                txtEmi.Text = "";
                txtReqMonths.Text = "";
                txtLoanSanctionedMonths.Text = "";
                //cbCompany.SelectedIndex = 0;
                //cbBranches.SelectedIndex = -1;
                dtpSanctionedDate.Value = DateTime.Today;
                dtpWagePeriod.Value = DateTime.Today;
                txtIntRate.Text = "";
                txtRecEmpName.Text = "";
                txtRecommendedEcode.Text = "";
                txtApprovedByEcode.Text = "";
                txtApprovedName.Text = "";
                txtCoEmpEcode.Text = "";
                txtCoEmpName.Text = "";
                cbLoanType.SelectedIndex = 0;
                //gvEmpLoanDetails.Rows.Clear();

                GenerateLoanNo();
               
            }
          
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strDelete = "";
            if (txtLoanNo.Text != "")
            {
                DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    try
                    {
                        strDelete += "DELETE FROM HR_PAYROLL_LOAN_REPAY_SCHEDULE WHERE HPLRS_LOAN_NUMBER='" + txtLoanNo.Text.ToString() + "'";

                        strDelete += "DELETE FROM HR_PAYROLL_LOAN_HEAD WHERE HPLTH_LOAN_NUMBER='" + txtLoanNo.Text.ToString() + "'";
                        
                        iRes = objSQLdb.ExecuteSaveData(strDelete);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    if (iRes > 0)
                    {
                        MessageBox.Show("Data Deleted Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Data Not Deleted", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
              
            }

        }

        private void txtEmi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtReqMonths_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtLoanNo_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void txtReqMonths_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateEmi();
           
        }      

        private void CalculateEmi()
        {
             int nEmi = 0;
             int LoanAmt = 0;
             int nMonths = 0;
            //if (txtLoanAmt.Text != "" && txtReqMonths.Text != "")
            //{
            //    if (txtReqMonths.Text != "0")
            //    {
            //        int nEmi = 0;
            //        int LoanAmt = Convert.ToInt32(txtLoanAmt.Text);
            //        int nMonths = Convert.ToInt32(txtReqMonths.Text);
            //        //int intRate = Convert.ToInt32(txtIntRate.Text);
            //        nEmi = (LoanAmt / nMonths);

            //        txtEmi.Text = Convert.ToString(nEmi);
            //    }

            //}

            if (txtLoanAmt.Text != "" && txtLoanSanctionedMonths.Text != "")
            {
                if (txtLoanSanctionedMonths.Text != "0")
                {
                   
                     LoanAmt = Convert.ToInt32(txtLoanAmt.Text);
                     nMonths = Convert.ToInt32(txtLoanSanctionedMonths.Text);
                    //int intRate = Convert.ToInt32(txtIntRate.Text);
                    nEmi = (LoanAmt / nMonths);

                    txtEmi.Text = Convert.ToString(nEmi);
                }

            }
        }

        private void txtIntRate_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void txtLoanAmt_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateEmi();
        }

        private void txtLoanSanctionedMonths_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtLoanSanctionedMonths.Text != "")
            {
                CalculateEmi();
            }
            else
            {
                txtEmi.Text = "";
            }
        }

      

      
    }
}
