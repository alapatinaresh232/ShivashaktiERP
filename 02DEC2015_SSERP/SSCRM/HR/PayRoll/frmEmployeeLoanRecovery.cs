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
    public partial class frmEmployeeLoanRecovery : Form
    {
        SQLDB objSQLdb = null;
        double nActualEmi = 0;
        double CurrentEmi = 0;
        double nDiffEmi = 0;
        double TotEmi = 0;
        int nLoanSlNo = 0;
        int nLoanLastSlNo = 0;
        string strEmpName = "";
        string strLoanType = "";
        double dBalAmt = 0;
        double NextEmi = 0;
        public frmEmployeeLoanRecovery()
        {
            InitializeComponent();
        }

        private void frmEmployeeLoanRecovery_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            dtpMonth.Value = DateTime.Today;

            gvEmpLoanRecoveryDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                     System.Drawing.FontStyle.Regular);


            //if (CommonData.LogUserId == "admin")
            //{
            //    dtpMonth.Enabled = true;
            //}
            //else
            //{
            //    dtpMonth.Enabled = false;
            //}

        }

        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT CM_COMPANY_CODE,CM_COMPANY_NAME " +
                                " FROM COMPANY_MAS " +
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
                                        " FROM BRANCH_MAS " +
                                        " where COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                        "' AND BRANCH_TYPE IN('HO') Order by BRANCH_NAME ASC";
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

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                FillBranchData();
            }
        }

        private DataSet GetEmpLoanRecoveryDetails(string CompCode, string BranchCode, string DocMonth)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, CompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, DocMonth, ParameterDirection.Input);


                ds = objSQLdb.ExecuteDataSet("Get_LoanRepayScheduleDetails", CommandType.StoredProcedure, param);

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


        private void FillEmpLoanRecoveryDetailsToGrid()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            gvEmpLoanRecoveryDetails.Rows.Clear();           
            double nBalanceAmt = 0;
            if (cbCompany.SelectedIndex > 0)
            {
                try
                {

                    dt = GetEmpLoanRecoveryDetails(cbCompany.SelectedValue.ToString(), cbBranches.SelectedValue.ToString(),Convert.ToDateTime(dtpMonth.Value).ToString("MMMyyyy").ToUpper()).Tables[0];

                  


                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            gvEmpLoanRecoveryDetails.Rows.Add();
                            
                            gvEmpLoanRecoveryDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                            gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanNo"].Value = dt.Rows[i]["LoanNo"].ToString();
                            gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanSlNo"].Value = dt.Rows[i]["LoanSlNo"].ToString();
                            gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanLastSlno"].Value = dt.Rows[i]["LoanlastSlno"].ToString();
                            gvEmpLoanRecoveryDetails.Rows[i].Cells["ActualEmi"].Value = dt.Rows[i]["LoanEmi"].ToString();                           
                            gvEmpLoanRecoveryDetails.Rows[i].Cells["Ecode"].Value = dt.Rows[i]["Ecode"].ToString();
                            gvEmpLoanRecoveryDetails.Rows[i].Cells["EmpName"].Value = dt.Rows[i]["EmpName"].ToString();
                            gvEmpLoanRecoveryDetails.Rows[i].Cells["Desig"].Value = dt.Rows[i]["EmpDesig"].ToString();
                            gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanType"].Value = dt.Rows[i]["LoanType"].ToString();
                            gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanAmt"].Value = dt.Rows[i]["LoanAmt"].ToString();
                            gvEmpLoanRecoveryDetails.Rows[i].Cells["RecoveredAmt"].Value = dt.Rows[i]["RecoveredAmt"].ToString();

                            nBalanceAmt = Convert.ToDouble(dt.Rows[i]["LoanAmt"].ToString()) - Convert.ToDouble(dt.Rows[i]["RecoveredAmt"].ToString());

                            gvEmpLoanRecoveryDetails.Rows[i].Cells["BalanceAmt"].Value = Convert.ToDouble(nBalanceAmt).ToString("0.00");
                            gvEmpLoanRecoveryDetails.Rows[i].Cells["Emi"].Value = dt.Rows[i]["LoanEmi"].ToString();
                            gvEmpLoanRecoveryDetails.Rows[i].Cells["Remarks"].Value = dt.Rows[i]["LoanRemarks"].ToString();


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


        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void cbBranches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBranches.SelectedIndex > 0)
            {
                FillEmpLoanRecoveryDetailsToGrid();
            }
        }

        private void dtpMonth_ValueChanged(object sender, EventArgs e)
        {
            FillEmpLoanRecoveryDetailsToGrid();
        }

        private bool CheckData()
        {
            bool flag = true;
            if (cbCompany.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Company","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            string strCommand = "";
            string strCheckStatus = "";           
            string strSelect = "";
            DataTable dtEmi = new DataTable();
            DataTable dt = new DataTable();
            int iRes = 0;

            if (CheckData() == true)
            {
                try
                {
                    if (gvEmpLoanRecoveryDetails.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvEmpLoanRecoveryDetails.Rows.Count; i++)
                        {
                            strCheckStatus = "";
                            dBalAmt = 0;
                            NextEmi = 0;
                            strEmpName = gvEmpLoanRecoveryDetails.Rows[i].Cells["EmpName"].Value.ToString();
                            strLoanType = gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanType"].Value.ToString();


                            strCheckStatus = "SELECT DISTINCT HPLRS_LOAN_EMI_STATUS " +
                                            " FROM HR_PAYROLL_LOAN_REPAY_SCHEDULE " +
                                            " WHERE HPLRS_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                            "'AND HPLRS_BRANCH_CODE='" + cbBranches.SelectedValue.ToString() +
                                            "'AND HPLRS_LOAN_NUMBER='" + gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanNo"].Value.ToString() +
                                            "' AND HPLRS_LOAN_TYPE='" + gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanType"].Value.ToString() +
                                            "' AND HPLRS_EORA_CODE=" + Convert.ToInt32(gvEmpLoanRecoveryDetails.Rows[i].Cells["Ecode"].Value) +
                                            " AND HPLRS_LOAN_SLNO< " + Convert.ToInt32(gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanSlNo"].Value) +
                                            " AND HPLRS_LOAN_EMI_STATUS NOT IN ('RECOVERED','STOP REQUEST')";

                            dt = objSQLdb.ExecuteDataSet(strCheckStatus).Tables[0];

                            if (dt.Rows.Count == 0)
                            {
                                dBalAmt = Convert.ToDouble(gvEmpLoanRecoveryDetails.Rows[i].Cells["BalanceAmt"].Value);
                                nActualEmi = Convert.ToDouble(gvEmpLoanRecoveryDetails.Rows[i].Cells["ActualEmi"].Value);
                                CurrentEmi = Convert.ToDouble(gvEmpLoanRecoveryDetails.Rows[i].Cells["Emi"].Value);
                                nLoanSlNo = Convert.ToInt32(gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanSlNo"].Value);
                                nLoanLastSlNo = Convert.ToInt32(gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanLastSlno"].Value);

                                strSelect = "SELECT HPLRS_LOAN_EMI NextLoanEmi " +
                                                " FROM HR_PAYROLL_LOAN_REPAY_SCHEDULE HPLR " +
                                                " WHERE HPLRS_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                                "'AND HPLRS_BRANCH_CODE='" + cbBranches.SelectedValue.ToString() +
                                                "'AND HPLRS_WAGE_MONTH='" + Convert.ToDateTime(dtpMonth.Value.AddMonths(1)).ToString("MMMyyyy").ToUpper() +
                                                "'AND HPLRS_EORA_CODE=" + Convert.ToInt32(gvEmpLoanRecoveryDetails.Rows[i].Cells["Ecode"].Value) +
                                                " AND HPLRS_LOAN_NUMBER='" + gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanNo"].Value.ToString() +
                                                "' AND HPLRS_LOAN_EMI_STATUS='FUTURE EMI'";

                                objSQLdb = new SQLDB();
                                if (strSelect.Length > 5)
                                {
                                    dtEmi = objSQLdb.ExecuteDataSet(strSelect).Tables[0];
                                }
                                if (dtEmi.Rows.Count > 0)
                                {
                                    NextEmi = Convert.ToDouble(dtEmi.Rows[0]["NextLoanEmi"].ToString());

                                }

                                if (dBalAmt >= CurrentEmi)
                                {

                                    if (dBalAmt != CurrentEmi)
                                    {
                                        if (nActualEmi > CurrentEmi)
                                        {
                                            nDiffEmi = nActualEmi - CurrentEmi;
                                            TotEmi = NextEmi + nDiffEmi;

                                        }
                                        else if (nActualEmi < CurrentEmi)
                                        {
                                            nDiffEmi = CurrentEmi - nActualEmi;
                                            if (nDiffEmi > nActualEmi)
                                            {
                                                TotEmi = nDiffEmi - NextEmi;

                                            }
                                            else
                                            {
                                                TotEmi = NextEmi - nDiffEmi;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        TotEmi = CurrentEmi;
                                    }

                                    if ((nActualEmi > CurrentEmi) || (nActualEmi < CurrentEmi))
                                    {
                                        if (TotEmi != dBalAmt)
                                        {
                                            objSQLdb = new SQLDB();

                                            if (nLoanSlNo.Equals(nLoanLastSlNo))
                                            {
                                                nLoanLastSlNo = (nLoanLastSlNo + 1);
                                                strCommand += "INSERT INTO HR_PAYROLL_LOAN_REPAY_SCHEDULE(HPLRS_COMPANY_CODE " +
                                                                                         ", HPLRS_BRANCH_CODE " +
                                                                                         ", HPLRS_EORA_CODE " +
                                                                                         ", HPLRS_LOAN_SLNO " +
                                                                                         ", HPLRS_LOAN_TYPE " +
                                                                                         ", HPLRS_LOAN_NUMBER " +
                                                                                         ", HPLRS_WAGE_MONTH " +
                                                                                         ", HPLRS_LOAN_EMI " +
                                                                                         ", HPLRS_LOAN_EMI_STATUS " +
                                                                                         ", HPLRS_LOAN_REMARKS " +
                                                                                         ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                                         "','" + cbBranches.SelectedValue.ToString() +
                                                                                         "'," + Convert.ToInt32(gvEmpLoanRecoveryDetails.Rows[i].Cells["Ecode"].Value) +
                                                                                         "," + (nLoanLastSlNo++) +
                                                                                         ",'" + gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanType"].Value.ToString() +
                                                                                         "','" + gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanNo"].Value.ToString() +
                                                                                         "','" + Convert.ToDateTime(dtpMonth.Value.AddMonths(1)).ToString("MMMyyyy").ToUpper() +
                                                                                         "'," + TotEmi +
                                                                                         ",'FUTURE EMI','" + gvEmpLoanRecoveryDetails.Rows[i].Cells["Remarks"].Value.ToString() + "')";
                                            }
                                            else
                                            {
                                                nLoanSlNo = (nLoanSlNo + 1);
                                                strCommand += "UPDATE HR_PAYROLL_LOAN_REPAY_SCHEDULE SET HPLRS_LOAN_EMI=" + TotEmi +
                                                       ",HPLRS_LOAN_REMARKS='" + gvEmpLoanRecoveryDetails.Rows[i].Cells["Remarks"].Value.ToString() +
                                                      "' WHERE HPLRS_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                                      "' AND HPLRS_BRANCH_CODE='" + cbBranches.SelectedValue.ToString() +
                                                      "' AND HPLRS_EORA_CODE=" + Convert.ToInt32(gvEmpLoanRecoveryDetails.Rows[i].Cells["Ecode"].Value) +
                                                      " AND HPLRS_LOAN_NUMBER='" + gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanNo"].Value.ToString() +
                                                      "' AND HPLRS_LOAN_SLNO=" + (nLoanSlNo) +
                                                      " AND HPLRS_LOAN_TYPE='" + gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanType"].Value.ToString() + "'";

                                            }


                                            nActualEmi = 0;
                                            CurrentEmi = 0;
                                            nDiffEmi = 0;
                                            TotEmi = 0;
                                            nLoanSlNo = 0;
                                            nLoanLastSlNo = 0;
                                        }
                                        else
                                        {
                                            objSQLdb = new SQLDB();
                                            string strDelete = "DELETE FROM HR_PAYROLL_LOAN_REPAY_SCHEDULE" +
                                                                " WHERE HPLRS_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                                                "'AND HPLRS_BRANCH_CODE='" + cbBranches.SelectedValue.ToString() +
                                                                "'AND HPLRS_LOAN_NUMBER='" + gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanNo"].Value.ToString() +
                                                                "' AND HPLRS_LOAN_TYPE='" + gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanType"].Value.ToString() +
                                                                "' AND HPLRS_EORA_CODE=" + Convert.ToInt32(gvEmpLoanRecoveryDetails.Rows[i].Cells["Ecode"].Value) +
                                                                " AND HPLRS_LOAN_SLNO> " + Convert.ToInt32(gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanSlNo"].Value) +
                                                                " AND HPLRS_LOAN_EMI_STATUS NOT IN ('RECOVERED')";

                                            if (strDelete.Length > 5)
                                            {
                                                int iDel = objSQLdb.ExecuteSaveData(strDelete);
                                            }

                                            strDelete = "";

                                        }

                                    }

                                    strCommand += "UPDATE HR_PAYROLL_LOAN_REPAY_SCHEDULE SET HPLRS_LOAN_EMI=" + Convert.ToDouble(gvEmpLoanRecoveryDetails.Rows[i].Cells["Emi"].Value) +
                                                ",HPLRS_LOAN_EMI_STATUS='RECOVERED' ,HPLRS_LOAN_REMARKS='" + gvEmpLoanRecoveryDetails.Rows[i].Cells["Remarks"].Value.ToString() +
                                                "' WHERE HPLRS_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                                "' AND HPLRS_BRANCH_CODE='" + cbBranches.SelectedValue.ToString() +
                                                "' AND HPLRS_EORA_CODE=" + Convert.ToInt32(gvEmpLoanRecoveryDetails.Rows[i].Cells["Ecode"].Value) +
                                                " AND HPLRS_LOAN_NUMBER='" + gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanNo"].Value.ToString() +
                                                "' AND HPLRS_LOAN_SLNO=" + Convert.ToInt32(gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanSlNo"].Value) +
                                                " AND HPLRS_LOAN_TYPE='" + gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanType"].Value.ToString() + "'";
                                }
                                else
                                {
                                    MessageBox.Show("Please Enter Correct Emi Amount For This Month", "LOAN RECOVERY", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                            else
                            {
                                MessageBox.Show("Please Recover Previous Month Loan Emi Amount for\n " + strEmpName + '(' + strLoanType + ')' + " ", "LOAN RECOVERY", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                strLoanType = "";
                                strEmpName = "";
                            }
                        }
                    }
                    objSQLdb = new SQLDB();

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
                    MessageBox.Show("Loan Amount Recovered Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillEmpLoanRecoveryDetailsToGrid();
                }
                else
                {
                    MessageBox.Show("Loan Amount Not Recovered ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbCompany.SelectedIndex = 0;
            cbBranches.SelectedIndex = -1;
            dtpMonth.Value = DateTime.Today;
            gvEmpLoanRecoveryDetails.Rows.Clear();
        }

        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') != -1)
            {
                e.Handled = true;
            }
        }


        private void gvEmpLoanRecoveryDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (gvEmpLoanRecoveryDetails.CurrentCell.ColumnIndex == 12)
            {
                e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
            }
        }

        private void btnStopReq_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            string strCommand = "";
            string strUpdate = "";
            string strSelect = "";
            string strCheckStatus = "";
            DataTable dt = new DataTable();
            DataTable dtEmi = new DataTable();
            int iRes = 0;
            if (CheckData() == true)
            {
                try
                {
                    if (gvEmpLoanRecoveryDetails.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvEmpLoanRecoveryDetails.Rows.Count; i++)
                        {
                            if (Convert.ToBoolean(gvEmpLoanRecoveryDetails.Rows[i].Cells["chkStopReq"].Value) == true)
                            {
                                strCheckStatus = "";
                                strEmpName = gvEmpLoanRecoveryDetails.Rows[i].Cells["EmpName"].Value.ToString();
                                strLoanType = gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanType"].Value.ToString();

                                strCheckStatus = "SELECT DISTINCT HPLRS_LOAN_EMI_STATUS " +
                                                " FROM HR_PAYROLL_LOAN_REPAY_SCHEDULE " +
                                                " WHERE HPLRS_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                                "'AND HPLRS_BRANCH_CODE='" + cbBranches.SelectedValue.ToString() +
                                                "'AND HPLRS_LOAN_NUMBER='" + gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanNo"].Value.ToString() +
                                                "' AND HPLRS_LOAN_TYPE='" + gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanType"].Value.ToString() +
                                                "' AND HPLRS_EORA_CODE=" + Convert.ToInt32(gvEmpLoanRecoveryDetails.Rows[i].Cells["Ecode"].Value) +
                                                " AND HPLRS_LOAN_SLNO< " + Convert.ToInt32(gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanSlNo"].Value) +
                                                " AND HPLRS_LOAN_EMI_STATUS NOT IN ('RECOVERED','STOP REQUEST')";

                                dt = objSQLdb.ExecuteDataSet(strCheckStatus).Tables[0];

                                if (dt.Rows.Count == 0)
                                {
                                    dBalAmt = Convert.ToDouble(gvEmpLoanRecoveryDetails.Rows[i].Cells["BalanceAmt"].Value);
                                    nActualEmi = Convert.ToDouble(gvEmpLoanRecoveryDetails.Rows[i].Cells["ActualEmi"].Value.ToString());
                                    CurrentEmi = Convert.ToDouble(gvEmpLoanRecoveryDetails.Rows[i].Cells["Emi"].Value.ToString());
                                    nLoanSlNo = Convert.ToInt32(gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanSlNo"].Value.ToString());
                                    nLoanLastSlNo = Convert.ToInt32(gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanLastSlno"].Value.ToString());

                                    strSelect = "SELECT HPLRS_LOAN_EMI NextLoanEmi " +
                                                " FROM HR_PAYROLL_LOAN_REPAY_SCHEDULE HPLR " +
                                                " WHERE HPLRS_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                                "'AND HPLRS_BRANCH_CODE='" + cbBranches.SelectedValue.ToString() +
                                                "'AND HPLRS_WAGE_MONTH='" + Convert.ToDateTime(dtpMonth.Value.AddMonths(1)).ToString("MMMyyyy").ToUpper() +
                                                "'AND HPLRS_EORA_CODE=" + Convert.ToInt32(gvEmpLoanRecoveryDetails.Rows[i].Cells["Ecode"].Value) +
                                                " AND HPLRS_LOAN_NUMBER='" + gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanNo"].Value.ToString() +
                                                "' AND HPLRS_LOAN_EMI_STATUS='FUTURE EMI'";

                                    objSQLdb = new SQLDB();
                                    if (strSelect.Length > 5)
                                        dtEmi = objSQLdb.ExecuteDataSet(strSelect).Tables[0];
                                    if (dtEmi.Rows.Count > 0)
                                    {
                                        NextEmi = Convert.ToDouble(dtEmi.Rows[0]["NextLoanEmi"].ToString());

                                    }
                                    //if (nActualEmi > CurrentEmi)
                                    //{
                                    //    nDiffEmi = nActualEmi - CurrentEmi;
                                    //    TotEmi = nActualEmi + nDiffEmi;

                                    //}
                                    //else if (nActualEmi < CurrentEmi)
                                    //{
                                    //    nDiffEmi = CurrentEmi - nActualEmi;
                                    //    TotEmi = nActualEmi - nDiffEmi;
                                    //}
                                    TotEmi = nActualEmi + NextEmi;
                                    //if ((nActualEmi > CurrentEmi) || (nActualEmi < CurrentEmi))
                                    //{
                                    if (nLoanSlNo.Equals(nLoanLastSlNo))
                                    {
                                        nLoanLastSlNo = (nLoanLastSlNo + 1);
                                        strUpdate += "INSERT INTO HR_PAYROLL_LOAN_REPAY_SCHEDULE(HPLRS_COMPANY_CODE " +
                                                                                 ", HPLRS_BRANCH_CODE " +
                                                                                 ", HPLRS_EORA_CODE " +
                                                                                 ", HPLRS_LOAN_SLNO " +
                                                                                 ", HPLRS_LOAN_TYPE " +
                                                                                 ", HPLRS_LOAN_NUMBER " +
                                                                                 ", HPLRS_WAGE_MONTH " +
                                                                                 ", HPLRS_LOAN_EMI " +
                                                                                 ", HPLRS_LOAN_EMI_STATUS " +
                                                                                 ", HPLRS_LOAN_REMARKS " +
                                                                                 ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                                 "','" + cbBranches.SelectedValue.ToString() +
                                                                                 "'," + Convert.ToInt32(gvEmpLoanRecoveryDetails.Rows[i].Cells["Ecode"].Value) +
                                                                                 "," + (nLoanLastSlNo++) +
                                                                                 ",'" + gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanType"].Value.ToString() +
                                                                                 "','" + gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanNo"].Value.ToString() +
                                                                                 "','" + Convert.ToDateTime(dtpMonth.Value.AddMonths(1)).ToString("MMMyyyy").ToUpper() +
                                                                                 "'," + TotEmi +
                                                                                 ",'FUTURE EMI','" + gvEmpLoanRecoveryDetails.Rows[i].Cells["Remarks"].Value.ToString() + "')";
                                    }
                                    else
                                    {
                                        nLoanSlNo = (nLoanSlNo + 1);
                                        strUpdate += "UPDATE HR_PAYROLL_LOAN_REPAY_SCHEDULE SET HPLRS_LOAN_EMI=" + TotEmi +
                                               ",HPLRS_LOAN_REMARKS='" + gvEmpLoanRecoveryDetails.Rows[i].Cells["Remarks"].Value.ToString() +
                                              "' WHERE HPLRS_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                              "' AND HPLRS_BRANCH_CODE='" + cbBranches.SelectedValue.ToString() +
                                              "' AND HPLRS_EORA_CODE=" + Convert.ToInt32(gvEmpLoanRecoveryDetails.Rows[i].Cells["Ecode"].Value) +
                                              " AND HPLRS_LOAN_NUMBER='" + gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanNo"].Value.ToString() +
                                              "' AND HPLRS_LOAN_SLNO=" + (nLoanSlNo) +
                                              " AND HPLRS_LOAN_TYPE='" + gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanType"].Value.ToString() + "'";
                                    }

                                    nActualEmi = 0;
                                    CurrentEmi = 0;
                                    nDiffEmi = 0;
                                    TotEmi = 0;
                                    nLoanSlNo = 0;
                                    nLoanLastSlNo = 0;

                                    //}

                                    strCommand += "UPDATE HR_PAYROLL_LOAN_REPAY_SCHEDULE SET HPLRS_LOAN_EMI=0 ,HPLRS_LOAN_EMI_STATUS='STOP REQUEST' " +
                                                ",HPLRS_LOAN_REMARKS='" + gvEmpLoanRecoveryDetails.Rows[i].Cells["Remarks"].Value.ToString() +
                                               "' WHERE HPLRS_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                               "' AND HPLRS_BRANCH_CODE='" + cbBranches.SelectedValue.ToString() +
                                               "' AND HPLRS_EORA_CODE=" + Convert.ToInt32(gvEmpLoanRecoveryDetails.Rows[i].Cells["Ecode"].Value) +
                                               " AND HPLRS_LOAN_NUMBER='" + gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanNo"].Value.ToString() +
                                               "' AND HPLRS_LOAN_SLNO=" + Convert.ToInt32(gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanSlNo"].Value) +
                                               " AND HPLRS_LOAN_TYPE='" + gvEmpLoanRecoveryDetails.Rows[i].Cells["LoanType"].Value.ToString() + "'";

                                }

                                else
                                {
                                    MessageBox.Show("Previous Month Loan Emi Amount is Not Recovered \n " + strEmpName + '(' + strLoanType + ')' + " ", "LOAN RECOVERY", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    strLoanType = "";
                                    strEmpName = "";
                                }
                            }
                        }
                    }
                    if (strUpdate.Length > 5)
                    {
                        objSQLdb = new SQLDB();
                        int iRec = objSQLdb.ExecuteSaveData(strUpdate);
                    }
                    if (strCommand.Length > 10)
                    {
                        objSQLdb = new SQLDB();
                        iRes = objSQLdb.ExecuteSaveData(strCommand);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (iRes > 0)
                {
                    MessageBox.Show("Loan Emi Amount Recovered Is Stopped For This Month", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    FillEmpLoanRecoveryDetailsToGrid();
                }
                else
                {
                    MessageBox.Show("Amount Recovered Is Stopped", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

       

        private void gvEmpLoanRecoveryDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            //nActualEmi = Convert.ToDouble(gvEmpLoanRecoveryDetails.Rows[e.RowIndex].Cells["ActualEmi"].Value);
            //CurrentEmi = Convert.ToDouble(gvEmpLoanRecoveryDetails.Rows[e.RowIndex].Cells["Emi"].Value);
        
            //if (nActualEmi > CurrentEmi)
            //{
            //    MessageBox.Show("Please Enter Remarks For Emi Amount Is Low Recovered","LOAN RECOVERY",MessageBoxButtons.OK,MessageBoxIcon.Information);

            //}
            //else if (nActualEmi < CurrentEmi)
            //{
            //    MessageBox.Show("Please Enter Remarks For Emi Amount Is Excess Recovered", "LOAN RECOVERY", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
           
        }

       
    }
}
