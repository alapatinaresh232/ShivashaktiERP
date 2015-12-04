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
using SSTrans;

namespace SSCRM
{
    public partial class EmployeeTDSForm : Form
    {
        SQLDB objSQLdb = null;
        Int32 EmpApplNo = 0;
        bool flagUpdate = false;
        Int32 TrnNo = 0;
        int nSlNo = 0;   

        public EmployeeTDSForm()
        {
            InitializeComponent();
        }

        private void EmployeeTDSForm_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            FillFinYearData();
            GenerateTrnNo();
           
            cbPaymentMode.SelectedIndex = 0;
            cbFinYear.SelectedValue = CommonData.FinancialYear;

            gvEmpTdsDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
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
                    if (TrnNo == 0)
                    {
                        TrnNo = Convert.ToInt32(dt.Rows[0]["TrnNo"].ToString());
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
            return TrnNo;
        }
      

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillBranchData();
        }

        private bool CheckData()
        {
            bool flag = true;

            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();

            //string strCommand = "SELECT * FROM HR_PAYROLL_TDS_SCHEDULE WHERE HPTS_STATUS='RECOVERED' AND HPTS_TRN_NO="+ Convert.ToInt32(txtTrnNo.Text) +" ";
            //dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

            //if (dt.Rows.Count > 0)
            //{
            //    flag = false;
            //    MessageBox.Show("TDS Amount Is Recovered! Data Can Not Be Manipulated","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
            //    return flag;
            //}
          
            if (txtEName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Ecode","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
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
            else if (cbBranches.SelectedIndex == 0 || cbBranches.SelectedIndex==-1)
            {
                flag = false;
                MessageBox.Show("Please Select Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbBranches.Focus();
            }
            else if (txtTotTdsAmt.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Total TDS Amount", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTotTdsAmt.Focus();
            }
            else if (txtEmi.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter TDS EMI Amount", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmi.Focus();
            }
            else if (txtPanNo.Text.Length < 4)
            {
                flag = false;
                MessageBox.Show("Please Enter Employee PanCard Number", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPanNo.Focus();
            }
            else if (cbWagePeriod.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Wage Period Month", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                if (SaveEmpTDSHead() > 0)
                {
                    if (SaveEmpTDSSchedule() > 0)
                    {
                        MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);                        

                        btnCancel_Click(null, null);
                        nSlNo = 0;
                        TrnNo = 0;
                        txtEcodeSearch.Focus();
                        //cbFinYear.SelectedIndex = 3;
                    }
                    else
                    {
                        MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        TrnNo = 0;
                    }
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TrnNo = 0;
                }
            }
        }
        #region "SAVE AND UPDATE DATA"

        private int SaveEmpTDSHead()
        {
            objSQLdb = new SQLDB();
            string strCommand = "";
            int iRes = 0;
            Int32 EmiMonths = 1;
            DataTable dt = new DataTable();

            try
            {
                GenerateTrnNo();

                objSQLdb = new SQLDB();
                strCommand = "SELECT HPTH_EORA_CODE FROM HR_PAYROLL_TDS_HEAD "+
                             " WHERE HPTH_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) +
                            " AND  HPTH_FIN_YEAR='" + cbFinYear.SelectedValue.ToString() +
                            "' AND HPTH_TRN_TYPE='TDS' ";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    strCommand = "";
                    objSQLdb = new SQLDB();
                    DataTable dtCount = new DataTable();

                    strCommand = "SELECT COUNT(*) as NoOfMonths FROM HR_PAYROLL_TDS_SCHEDULE "+
                                " WHERE HPTS_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) +
                                " AND  HPTS_FIN_YEAR='" + cbFinYear.SelectedValue.ToString() +
                                "'AND HPTS_TRN_TYPE='TDS'";
                    dtCount = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                    EmiMonths = Convert.ToInt32(dtCount.Rows[0][0].ToString());
                    if (flagUpdate == false)
                    {
                        EmiMonths = EmiMonths + 1;
                    }
                    strCommand = "";

                    strCommand = "UPDATE HR_PAYROLL_TDS_HEAD SET HPTH_APPL_NO=" + EmpApplNo +
                                  ", HPTH_COMP_CODE='" + cbCompany.SelectedValue.ToString() +
                                  "',HPTH_BRANCH_CODE='" + cbBranches.SelectedValue.ToString() +
                                  "',HPTH_ANNUAL_CUTTING=" + txtTotTdsAmt.Text +
                                  ", HPTH_NO_MONTH="+ EmiMonths +",HPTH_PAN_NO='" + txtPanNo.Text.ToString() +                                  
                                  "',HPTH_MONTHLY_DEDUCTION=" + txtEmi.Text +
                                  ",HPTH_MODIFIED_BY='"+ CommonData.LogUserId +
                                  "',HPTH_MODIFIED_DATE=getdate() "+
                                  " WHERE HPTH_FIN_YEAR='" + cbFinYear.SelectedValue.ToString() +
                                  "' AND HPTH_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) +
                                  " AND HPTH_TRN_TYPE='TDS' ";

                    strCommand += " UPDATE HR_APPL_MASTER_HEAD set HAMH_VD_PAN_CARD_NUMBER='" + txtPanNo.Text.ToString() +
                                    "' WHERE HAMH_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) + "";

                    
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
                                                               ", HPTH_PAN_NO " +
                                                               ", HPTH_CREATED_BY " +
                                                               ", HPTH_CREATED_DATE " +
                                                               ")VALUES(" + EmpApplNo +
                                                               ",'" + cbCompany.SelectedValue.ToString() +
                                                               "','" + cbBranches.SelectedValue.ToString() +
                                                               "'," + Convert.ToInt32(txtEcodeSearch.Text) +
                                                               ",'TDS', "+ TrnNo +",'" + cbFinYear.SelectedValue.ToString() +
                                                               "'," + Convert.ToInt32(txtTotTdsAmt.Text) +
                                                               "," + EmiMonths + "," + Convert.ToInt32(txtEmi.Text) +
                                                               ",'" + cbWagePeriod.SelectedValue.ToString() +
                                                               "','" + txtPanNo.Text.ToString() +
                                                               "','" + CommonData.LogUserId +
                                                               "',getdate())";
                    strCommand += "UPDATE HR_APPL_MASTER_HEAD set HAMH_VD_PAN_CARD_NUMBER='"+ txtPanNo.Text.ToString() +
                                    "' WHERE HAMH_EORA_CODE="+ Convert.ToInt32(txtEcodeSearch.Text)+"";
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

        private int SaveEmpTDSSchedule()
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
                             " WHERE HPTS_EORA_CODE="+ Convert.ToInt32(txtEcodeSearch.Text)+
                             " AND HPTS_FIN_YEAR='"+ cbFinYear.SelectedValue.ToString() +
                             "' AND HPTS_TRN_TYPE='TDS' ";

                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    if (nSlNo==0)
                    {
                        nSlNo = Convert.ToInt32(dt.Rows[0][0].ToString());
                    }
                }

                strCommand = "";
                objSQLdb = new SQLDB();
                strCommand = "SELECT HPTS_EORA_CODE FROM HR_PAYROLL_TDS_SCHEDULE " +
                            " WHERE HPTS_FIN_YEAR='" + cbFinYear.SelectedValue.ToString() +
                                "' AND HPTS_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) +
                                " AND HPTS_MONTH='" + cbWagePeriod.SelectedValue.ToString() +
                                "' AND HPTS_TRN_TYPE='TDS'";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                strCommand = "";

                if (dt.Rows.Count > 0)
                {                
                    strCommand = "UPDATE HR_PAYROLL_TDS_SCHEDULE SET HPTS_MONTH='" + cbWagePeriod.SelectedValue.ToString() +
                                "',HPTS_DEDUCTED_AMT="+ txtEmi.Text +
                                ",HPTS_TDS_EMI="+ txtEmi.Text +
                                ",HPTS_DEDUCT_TO='" + cbPaymentMode.SelectedItem.ToString() +
                                "',HPTS_MODIFIED_BY='"+ CommonData.LogUserId +
                                "',HPTS_MODIFIED_DATE=getdate() "+
                                " WHERE HPTS_FIN_YEAR='" + cbFinYear.SelectedValue.ToString() +
                                "' AND HPTS_EORA_CODE="+ Convert.ToInt32(txtEcodeSearch.Text) +
                                " AND HPTS_MONTH='" + cbWagePeriod.SelectedValue.ToString() +
                                "' AND HPTS_TRN_TYPE='TDS'";

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
                                                                   "','TDS'," + TrnNo +
                                                                   "," + nSlNo +
                                                                   ",'" + cbWagePeriod.SelectedValue.ToString() +
                                                                   "'," + Convert.ToDouble(txtEmi.Text) +
                                                                   "," + Convert.ToDouble(txtEmi.Text) +
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

        private void FillEmployeeTDSDetails()
        {

            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            gvEmpTdsDetails.Rows.Clear();
            if (txtEcodeSearch.Text != "")
            {
                try
                {


                    dt = GetEmpTDSDetails(Convert.ToInt32(txtEcodeSearch.Text), cbFinYear.SelectedValue.ToString()).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtEName.Text = dt.Rows[0]["EmpName"].ToString();
                        cbCompany.SelectedValue = dt.Rows[0]["CompCode"].ToString();
                        cbBranches.SelectedValue = dt.Rows[0]["BranCode"].ToString();
                        EmpApplNo = Convert.ToInt32(dt.Rows[0]["ApplNo"]);
                        txtDeductedAmt.Text = dt.Rows[0]["RecoveredAmt"].ToString();
                        txtPanNo.Text = dt.Rows[0]["PanNo"].ToString();
                        txtTotTdsAmt.Text = dt.Rows[0]["TdsTotAmt"].ToString();
                        if (dt.Rows[0]["TrnNo"].ToString() != "")
                        {
                            TrnNo = Convert.ToInt32(dt.Rows[0]["TrnNo"]);
                        }

                        if (cbFinYear.SelectedIndex > 0 && dt.Rows[0]["TrnNo"].ToString() != "")
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                gvEmpTdsDetails.Rows.Add();

                                gvEmpTdsDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                                gvEmpTdsDetails.Rows[i].Cells["MonthSlNO"].Value = dt.Rows[i]["SlNo"].ToString();
                                gvEmpTdsDetails.Rows[i].Cells["TdsNo"].Value = dt.Rows[i]["TrnNo"].ToString();
                                gvEmpTdsDetails.Rows[i].Cells["FinYear"].Value = dt.Rows[i]["FinYear"].ToString();
                                gvEmpTdsDetails.Rows[i].Cells["TotAmt"].Value = dt.Rows[i]["TdsTotAmt"].ToString();
                                gvEmpTdsDetails.Rows[i].Cells["Recovered"].Value = dt.Rows[i]["RecoveredAmt"].ToString();
                                gvEmpTdsDetails.Rows[i].Cells["WageMonth"].Value = dt.Rows[i]["WageMonth"].ToString();
                                gvEmpTdsDetails.Rows[i].Cells["EmiAmt"].Value = dt.Rows[i]["EmiAmt"].ToString();
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
                gvEmpTdsDetails.Rows.Clear();
            }
        }

      
        private void txtEcodeSearch_Validated(object sender, EventArgs e)
        {
            FillEmployeeTDSDetails();
        }
       

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            flagUpdate = false;

            txtDeductedAmt.Text = "";
            GenerateTrnNo();
            txtEcodeSearch.Text = "";
            txtEName.Text = "";
            cbCompany.SelectedIndex = 0;
            cbBranches.SelectedIndex = -1;
            txtPanNo.Text = "";           
            txtTotTdsAmt.Text = "";
            txtEmi.Text = "";
            //cbWagePeriod.SelectedIndex = 0;
            gvEmpTdsDetails.Rows.Clear();
            cbFinYear.SelectedValue = CommonData.FinancialYear;
            cbPaymentMode.SelectedIndex = 0;
            TrnNo = 0;

        }

        //private void CalculateEmi()
        //{
        //    double nEmi = 0;
        //    double TdsAmt = 0;
        //    double nMonths = 0;
          

        //    if (txtTotTdsAmt.Text != "" && txtTDSMonths.Text != "")
        //    {
        //        if (txtTDSMonths.Text != "0")
        //        {

        //            TdsAmt = Convert.ToDouble(txtTotTdsAmt.Text);
        //            nMonths = Convert.ToDouble(txtTDSMonths.Text);
        //            nEmi = (TdsAmt / nMonths);

        //            txtEmi.Text = Convert.ToString(nEmi);
        //        }

        //    }
        //}

       

        private void txtTotTdsAmt_KeyUp(object sender, KeyEventArgs e)
        {
            //if (txtTotTdsAmt.Text.Length > 0)
            //{
            //    CalculateEmi();
            //}
            //else
            //{
            //    txtEmi.Text = "";
            //}
        }

      

        private void txtTotTdsAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }
            }
        }
      

        private void txtPanNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            e.KeyChar = char.ToUpper(e.KeyChar);

           
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && (!char.IsLetter(e.KeyChar)))
                {
                    e.Handled = true;
                }
            }

        }

        private DataSet GetEmpTDSDetails(Int32 Ecode, string strFinYear)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xEcode", DbType.Int32, Ecode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xFinYear", DbType.String, strFinYear, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("GetEmpTDSDetails", CommandType.StoredProcedure, param);

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
            
        

        private void txtEmi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }
            }
        }

        private void gvEmpTdsDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == gvEmpTdsDetails.Columns["Edit"].Index)
                {
                    flagUpdate = true;
                    cbWagePeriod.Text = gvEmpTdsDetails.Rows[e.RowIndex].Cells["WageMonth"].Value.ToString();
                    txtEmi.Text = gvEmpTdsDetails.Rows[e.RowIndex].Cells["EmiAmt"].Value.ToString();
                    nSlNo = Convert.ToInt32(gvEmpTdsDetails.Rows[e.RowIndex].Cells["MonthSlNO"].Value);
                    TrnNo = Convert.ToInt32(gvEmpTdsDetails.Rows[e.RowIndex].Cells["TdsNo"].Value);
                }
            }

        }

        private void cbFinYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillEmployeeTDSDetails();
            FillDocMonthsData();
        }

        private void cbWagePeriod_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

      

      

    }
}
