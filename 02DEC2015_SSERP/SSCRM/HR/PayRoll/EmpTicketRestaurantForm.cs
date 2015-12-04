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
    public partial class EmpTicketRestaurantForm : Form
    {
        SQLDB objSQLdb = null;
        Int32 TrnNo = 0;
        Int32 nSlNo = 0;
        Int32 EmpApplNo = 0;
        bool flagUpdate = false;
        double TotAmount = 0;
        public EmpTicketRestaurantForm()
        {
            InitializeComponent();
        }

        private void EmpTicketRestaurantForm_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            FillFinYearData();
            GenerateTrnNo();

            cbPaymentMode.SelectedIndex = 0;
            cbFinYear.SelectedValue = CommonData.FinancialYear;

            gvEmpTktRestaurantDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                     System.Drawing.FontStyle.Regular);
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
                                        "' AND BRANCH_TYPE IN('BR','HO') AND ACTIVE='T' Order by BRANCH_NAME ASC";
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

        private void FillFinYearData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
            try
            {
                strCommand = "SELECT DISTINCT(FY_FIN_YEAR) FROM FIN_YEAR";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();

                    row[0] = "--Select--";
                    dt.Rows.InsertAt(row, 0);

                    cbFinYear.DataSource = dt;

                    cbFinYear.ValueMember = "FY_FIN_YEAR";
                    cbFinYear.DisplayMember = "FY_FIN_YEAR";
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


        private void FillDocMonthsData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                if (cbFinYear.SelectedIndex > 0)
                {
                    string strCmd = "SELECT DISTINCT(document_month),start_date ,end_date " +
                                    " FROM document_month " +
                                    " WHERE  fin_year='" + cbFinYear.SelectedValue.ToString() +
                                    "' ORDER BY start_date ,end_date";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                }


                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row["document_month"] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cbWagePeriod.DataSource = dt;
                    cbWagePeriod.DisplayMember = "document_month";
                    cbWagePeriod.ValueMember = "document_month";
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

        private Int32 GenerateTrnNo()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
            try
            {
                strCommand = "SELECT isnull(MAX(HPTH_TRN_NO),0)+1 TrnNo FROM HR_PAYROLL_TDS_HEAD ";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    //if (TrnNo == 0)
                    //{
                        TrnNo = Convert.ToInt32(dt.Rows[0]["TrnNo"].ToString());
                    //}

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
            return TrnNo;
        }

        private DataSet GetEmpTRCDetails(Int32 Ecode, string strFinYear)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xEcode", DbType.Int32, Ecode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xFinYear", DbType.String, strFinYear, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_EmpTicketRestaurantDetails", CommandType.StoredProcedure, param);

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

        private bool CheckData()
        {
            bool flag = true;

            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();          

            if (txtEName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Valid Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEcodeSearch.Focus();
            }
            else if (cbFinYear.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select FinYear", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbFinYear.Focus();
            }

            else if (cbCompany.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Company", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCompany.Focus();
            }
            else if (cbBranches.SelectedIndex == 0 || cbBranches.SelectedIndex == -1)
            {
                flag = false;
                MessageBox.Show("Please Select Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbBranches.Focus();
            }
          
            else if (txtAmount.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Amount", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAmount.Focus();
            }
            else if (cbWagePeriod.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Wage Period", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbWagePeriod.Focus();
            }

            return flag;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();

            if (CheckData() == true)
            {
                GenerateTrnNo();

                if (SaveEmpTRCHead() > 0)
                {
                    if (SaveEmpTRCSchedule() > 0)
                    {
                        MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        btnCancel_Click(null, null);
                        nSlNo = 0;
                        TrnNo = 0;
                        TotAmount = 0;
                        txtEcodeSearch.Focus();
                        cbFinYear.SelectedIndex = 3;
                    }
                    else
                    {
                        MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #region "SAVE AND UPDATE DATA"

        private int SaveEmpTRCHead()
        {
            objSQLdb = new SQLDB();
            string strCommand = "";
            int iRes = 0;
            Int32 EmiMonths = 1;
            DataTable dt = new DataTable();
            double AnnualAmt = 0;

            try
            {
                GenerateTrnNo();

                objSQLdb = new SQLDB();
                strCommand = "SELECT HPTH_EORA_CODE FROM HR_PAYROLL_TDS_HEAD "+
                             " WHERE HPTH_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) +
                            //" AND  HPTH_FIN_YEAR='" + cbFinYear.SelectedValue.ToString() +
                            "AND HPTH_TRN_TYPE='COUPONS' AND HPTH_STARTING_WAGE_PERIOD='" + cbWagePeriod.SelectedValue.ToString() + "'";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    AnnualAmt = TotAmount + Convert.ToDouble(txtAmount.Text);

                    strCommand = "";
                    objSQLdb = new SQLDB();
                    DataTable dtCount = new DataTable();

                    strCommand = "SELECT COUNT(*) as NoOfMonths FROM HR_PAYROLL_TDS_SCHEDULE "+
                                " WHERE HPTS_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) +
                               " AND  HPTS_FIN_YEAR='" + cbFinYear.SelectedValue.ToString() +
                               "'AND HPTS_TRN_TYPE='COUPONS'";
                    dtCount = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                    EmiMonths = Convert.ToInt32(dtCount.Rows[0][0].ToString());

                    if (flagUpdate == false)
                    {
                        EmiMonths = EmiMonths + 1;
                    }

                    strCommand = "UPDATE HR_PAYROLL_TDS_HEAD SET HPTH_APPL_NO=" + EmpApplNo +
                                  ", HPTH_COMP_CODE='" + cbCompany.SelectedValue.ToString() +
                                  "',HPTH_BRANCH_CODE='" + cbBranches.SelectedValue.ToString() +
                                  "',HPTH_ANNUAL_CUTTING=" + AnnualAmt + ", HPTH_NO_MONTH=" + EmiMonths + 
                                  ",HPTH_MONTHLY_DEDUCTION=" + txtAmount.Text +
                                  ",HPTH_MODIFIED_BY='"+ CommonData.LogUserId +
                                  "',HPTH_MODIFIED_DATE=getdate() "+
                                  " WHERE HPTH_FIN_YEAR='" + cbFinYear.SelectedValue.ToString() +
                                  "' AND HPTH_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) +
                                  " AND HPTH_TRN_TYPE='COUPONS'";


                }
                else
                {

                    strCommand = "INSERT INTO HR_PAYROLL_TDS_HEAD(HPTH_APPL_NO " +
                                                               ", HPTH_COMP_CODE " +
                                                               ", HPTH_BRANCH_CODE " +
                                                               ", HPTH_EORA_CODE " +
                                                               ", HPTH_TRN_TYPE " +
                                                               ", HPTH_TRN_NO " +
                                                               ", HPTH_FIN_YEAR " +
                                                               ", HPTH_ANNUAL_CUTTING " +
                                                               ", HPTH_NO_MONTH " +
                                                               ", HPTH_MONTHLY_DEDUCTION " +
                                                               ", HPTH_STARTING_WAGE_PERIOD " +                                                             
                                                               ", HPTH_CREATED_BY " +
                                                               ", HPTH_CREATED_DATE " +
                                                               ")VALUES(" + EmpApplNo +
                                                               ",'" + cbCompany.SelectedValue.ToString() +
                                                               "','" + cbBranches.SelectedValue.ToString() +
                                                               "'," + Convert.ToInt32(txtEcodeSearch.Text) +
                                                               ",'COUPONS', " + TrnNo + ",'" + cbFinYear.SelectedValue.ToString() +
                                                               "',"+ Convert.ToInt32(txtAmount.Text) +
                                                               "," + EmiMonths + "," + Convert.ToInt32(txtAmount.Text) +
                                                               ",'" + cbWagePeriod.SelectedValue.ToString() +                                                              
                                                               "','" + CommonData.LogUserId +
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
            return iRes;
        }

        private int SaveEmpTRCSchedule()
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "";
            DataTable dt = new DataTable();

            try
            {
                //GenerateTrnNo();

                objSQLdb = new SQLDB();

                strCommand = "SELECT isnull(MAX(HPTS_SL_NO),0)+1 FROM HR_PAYROLL_TDS_SCHEDULE "+
                            " WHERE HPTS_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) +
                             " AND HPTS_MONTH='" + cbWagePeriod.SelectedValue.ToString() +
                             "' AND HPTS_TRN_TYPE='COUPONS'";

                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    if (nSlNo == 0)
                    {
                        nSlNo = Convert.ToInt32(dt.Rows[0][0].ToString());
                    }
                }

                strCommand = "";
                objSQLdb = new SQLDB();


                if (flagUpdate == true)
                {
                    strCommand = "UPDATE HR_PAYROLL_TDS_SCHEDULE SET HPTS_MONTH='" + cbWagePeriod.SelectedValue.ToString() +
                                "',HPTS_DEDUCTED_AMT=" + txtAmount.Text +
                                ",HPTS_TDS_EMI=" + txtAmount.Text +
                                ",HPTS_DEDUCT_TO='" + cbPaymentMode.SelectedItem.ToString() +
                                "',HPTS_MODIFIED_BY='"+ CommonData.LogUserId +
                                "',HPTS_MODIFIED_DATE=getdate() "+
                                " WHERE HPTS_FIN_YEAR='" + cbFinYear.SelectedValue.ToString() +
                                "'AND HPTS_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) +
                                " AND HPTS_SL_NO=" + Convert.ToInt32(nSlNo) +
                                " AND HPTS_TRN_TYPE='COUPONS'";

                    flagUpdate = false;
                }
                else
                {

                    strCommand = "INSERT INTO HR_PAYROLL_TDS_SCHEDULE(HPTS_APPL_NO " +
                                                                   ", HPTS_COMP_CODE " +
                                                                   ", HPTS_BRANCH_CODE " +
                                                                   ", HPTS_EORA_CODE " +
                                                                   ", HPTS_FIN_YEAR " +
                                                                   ", HPTS_TRN_TYPE " +
                                                                   ", HPTS_TRN_NO " +
                                                                   ", HPTS_SL_NO " +
                                                                   ", HPTS_MONTH " +
                                                                   ", HPTS_TDS_EMI " +
                                                                   ", HPTS_DEDUCTED_AMT " +
                                                                   ", HPTS_STATUS " +
                                                                   ", HPTS_CREATED_BY " +
                                                                   ", HPTS_CREATED_DATE " +
                                                                   ", HPTS_DEDUCT_TO " +
                                                                   ")VALUES(" + EmpApplNo +
                                                                   ",'" + cbCompany.SelectedValue.ToString() +
                                                                   "','" + cbBranches.SelectedValue.ToString() +
                                                                   "'," + Convert.ToInt32(txtEcodeSearch.Text) +
                                                                   ",'" + cbFinYear.SelectedValue.ToString() +
                                                                   "','COUPONS'," + TrnNo +
                                                                   "," + nSlNo +
                                                                   ",'" + cbWagePeriod.SelectedValue.ToString() +
                                                                   "'," + Convert.ToDouble(txtAmount.Text) +
                                                                   "," + Convert.ToDouble(txtAmount.Text) +
                                                                   ",'RECOVERED','" + CommonData.LogUserId +
                                                                   "',getdate(),'" + cbPaymentMode.Text.ToString() + "')";
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

            return iRes;
        }

        #endregion


      

        private void txtEcodeSearch_Validated(object sender, EventArgs e)
        {
            if (txtEcodeSearch.Text != "")
            {
                FillEmpTicketRestaurantDetails();
            }

        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillBranchData();
        }

        private void cbFinYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDocMonthsData();
        }

        private void FillEmpTicketRestaurantDetails()
        {

            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            TotAmount = 0;
            gvEmpTktRestaurantDetails.Rows.Clear();
            if (txtEcodeSearch.Text != "")
            {
                try
                {

                    dt = GetEmpTRCDetails(Convert.ToInt32(txtEcodeSearch.Text), cbFinYear.SelectedValue.ToString()).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtEName.Text = dt.Rows[0]["EmpName"].ToString();
                        cbCompany.SelectedValue = dt.Rows[0]["CompCode"].ToString();
                        cbBranches.SelectedValue = dt.Rows[0]["BranCode"].ToString();
                        EmpApplNo = Convert.ToInt32(dt.Rows[0]["ApplNo"]);
                        TotAmount = Convert.ToDouble(dt.Rows[0]["RecoveredAmt"].ToString());

                        if (dt.Rows[0]["TrnNo"].ToString() != "")
                        {
                            TrnNo = Convert.ToInt32(dt.Rows[0]["TrnNo"]);
                        }

                        if (cbFinYear.SelectedIndex > 0 && dt.Rows[0]["TrnNo"].ToString() != "")
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                gvEmpTktRestaurantDetails.Rows.Add();

                                gvEmpTktRestaurantDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                                gvEmpTktRestaurantDetails.Rows[i].Cells["MonthSlNO"].Value = dt.Rows[i]["SlNo"].ToString();
                                gvEmpTktRestaurantDetails.Rows[i].Cells["Ecode"].Value = dt.Rows[i]["EmpCode"].ToString();
                                gvEmpTktRestaurantDetails.Rows[i].Cells["EmpName"].Value = dt.Rows[i]["EmpName"].ToString();                               
                                gvEmpTktRestaurantDetails.Rows[i].Cells["FinYear"].Value = dt.Rows[i]["FinYear"].ToString();
                                gvEmpTktRestaurantDetails.Rows[i].Cells["TotAmt"].Value = dt.Rows[i]["TdsTotAmt"].ToString();                                
                                gvEmpTktRestaurantDetails.Rows[i].Cells["WageMonth"].Value = dt.Rows[i]["WageMonth"].ToString();
                                gvEmpTktRestaurantDetails.Rows[i].Cells["Amount"].Value = dt.Rows[i]["EmiAmt"].ToString();                              

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
                    objSQLdb = null;
                    dt = null;
                }
            }
            else
            {
                txtEName.Text = "";
                cbCompany.SelectedIndex = 0;
                cbBranches.SelectedIndex = -1;
                gvEmpTktRestaurantDetails.Rows.Clear();
                txtAmount.Text = "";
            }
        }

      

        private void txtEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            flagUpdate = false;

            GenerateTrnNo();
            txtEcodeSearch.Text = "";
            txtEName.Text = "";
            cbCompany.SelectedIndex = 0;
            cbBranches.SelectedIndex = -1;
            txtAmount.Text = "";
            cbWagePeriod.SelectedIndex = 0;
            gvEmpTktRestaurantDetails.Rows.Clear();
            cbFinYear.SelectedIndex = 0;
            cbPaymentMode.SelectedIndex = 0;
        }

        private void cbWagePeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            double RecoveredAmt = 0;

            if (txtEcodeSearch.Text.Length > 0)
            {
                try
                {
                    RecoveredAmt = TotAmount;

                    strCmd = "SELECT HPTS_TDS_EMI Amount,HPTS_SL_NO SlNo " +
                             " FROM HR_PAYROLL_TDS_SCHEDULE " +
                             " WHERE HPTS_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) +
                             " AND HPTS_MONTH='" + cbWagePeriod.SelectedValue.ToString() +
                             "' AND HPTS_FIN_YEAR='" + cbFinYear.SelectedValue.ToString() +
                             "' AND HPTS_TRN_TYPE='COUPONS'";

                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        flagUpdate = true;
                        TotAmount = TotAmount - Convert.ToDouble(dt.Rows[0]["Amount"].ToString());
                        txtAmount.Text = dt.Rows[0]["Amount"].ToString();
                        nSlNo = Convert.ToInt32(dt.Rows[0]["SlNo"]);

                    }
                    else
                    {
                        TotAmount = RecoveredAmt;

                        flagUpdate = false;
                        txtAmount.Text = "";
                        nSlNo = 0;
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

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

     
    }
}
