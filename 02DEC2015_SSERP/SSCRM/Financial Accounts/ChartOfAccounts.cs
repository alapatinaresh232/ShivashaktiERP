using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using System.Data.SqlClient;
using SSCRM.App_Code;

namespace SSCRM
{
    public partial class ChartOfAccounts : Form
    {
        SQLDB objDB = null;
        DateTime startDate, endDate;
        bool flagUpdate = false;
        string AccountFlag,DefFlag,PLBSFlag;
        public ChartOfAccounts()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void ChartOfAccounts_Load(object sender, EventArgs e)
        {
            txtCompany.Text = CommonData.CompanyName;
            txtBranch.Text = CommonData.BranchName;
            objDB = new SQLDB();
           

            FillAccounts();
            FillCostCentreFlag();
            cbType.SelectedIndex = 0;
            cmbCostCentFlag.SelectedIndex = 0;
            string FinYear =CommonData.FinancialYear;
             startDate = new DateTime(Convert.ToInt32(FinYear.Split('-')[0]), 4, 1);
             endDate = new DateTime(Convert.ToInt32(FinYear.Split('-')[0]), 3, 31);
             FillLedgerPrintFormat();
             cmbLedFormat.SelectedIndex = 0;
             txtAccountSearch.Focus();
             //FillGrid();
             FillAutoCompleteBox();
        }
        private void FillAutoCompleteBox()
        {
            objDB = new SQLDB();
            DataTable dtAccMas = new DataTable();
            dtAccMas = objDB.ExecuteDataSet(" SELECT AM_ACCOUNT_ID,AM_ACCOUNT_NAME AccName FROM FA_ACCOUNT_MASTER  WHERE AM_COMPANY_CODE='" + CommonData.CompanyCode + "' AND AM_ACCOUNT_GROUP_ID IS NULL ").Tables[0];
            UtilityLibrary.AutoCompleteTextBox(txtAccountSearch, dtAccMas, "AM_ACCOUNT_ID", "AM_ACCOUNT_ID");

            DataTable dtAccName = new DataTable();

            dtAccName = objDB.ExecuteDataSet(" SELECT AM_ACCOUNT_ID,AM_ACCOUNT_NAME AccName FROM FA_ACCOUNT_MASTER  WHERE AM_COMPANY_CODE='" + CommonData.CompanyCode + "' AND AM_ACCOUNT_GROUP_ID IS NULL ").Tables[0];
            UtilityLibrary.AutoCompleteTextBox(txtName, dtAccName, "", "AccName");
        }

        private void  FillCostCentreFlag()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("type", typeof(string));
            dt.Columns.Add("name", typeof(string));

            dt.Rows.Add("--Select--", "--Select--");
            dt.Rows.Add("Y", "ENABLE");
            dt.Rows.Add("N", "DISABLE");
            cmbCostCentFlag.DataSource = dt;
            cmbCostCentFlag.DisplayMember = "name";
            cmbCostCentFlag.ValueMember = "type";
        }
        private void FillLedgerPrintFormat()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("type", typeof(string));
            dt.Columns.Add("name", typeof(string));

            dt.Rows.Add("ALL", "ALL TRANSACTIONS");
            dt.Rows.Add("DATESUMM", "DATE WISE Dr. Cr. SUMMARY");
            dt.Rows.Add("DATETRNTYPESUMM", "DATE WISE TRN TYPE WISE SUMMARY");
            dt.Rows.Add("MONTHSUMM", "MONTH WISE Dr. Cr. SUMMARY");
            dt.Rows.Add("MONTHTRNTYPESUMM", "MONTH WISE TRN TYPE WISE SUMMARY");

            cmbLedFormat.DataSource = dt;
            cmbLedFormat.DisplayMember = "name";
            cmbLedFormat.ValueMember = "type";
        }
        private void FillAccounts()
        {
            try
            {
                //objDB = new SQLDB();
                //string strCMD = "SELECT AT_ACCOUNT_TYPE_ID,AT_ACCOUNT_TYPE_DESC FROM FA_ACCOUNT_TYPE";
                //DataTable DT = objDB.ExecuteDataSet(strCMD).Tables[0];

                DataTable dt = new DataTable();
                dt.Columns.Add("AT_ACCOUNT_TYPE_ID", typeof(string));
                dt.Columns.Add("AT_ACCOUNT_TYPE_DESC", typeof(string));

                dt.Rows.Add("--Select---", "---Select---");
                dt.Rows.Add("BANK", "BANK ACCOUNTS");
                dt.Rows.Add("CASH", "CASH ACCOUNT");
                dt.Rows.Add("PRCH", "PURCHASE ACCOUNTS");
                dt.Rows.Add("SALE", "SALES ACCOUNTS");
                dt.Rows.Add("STAX", "SALES TAX");
                dt.Rows.Add("SUCR", "SUNDRY CREDITORS");
                dt.Rows.Add("SUDR", "SUNDRY DEBTORS");
                if (dt.Rows.Count > 0)
                {
                    cmbCntrlAcc.DataSource = dt;
                    cmbCntrlAcc.DisplayMember = "AT_ACCOUNT_TYPE_DESC";
                    cmbCntrlAcc.ValueMember = "AT_ACCOUNT_TYPE_ID";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void FillGrid()
        {
            //gvProductDetails.Rows.Clear();
            //DateTime dt = startDate;
            //for (int i = 0; i < 12;i++ )
            //{
            //    gvProductDetails.Rows.Add();
            //    gvProductDetails.Rows[i].Cells["SLNO"].Value = (i + 1);
            //    gvProductDetails.Rows[i].Cells["MonYear"].Value = dt.ToString("MMMyyyy").ToUpper();
            //    dt = dt.AddMonths(1);
            //}
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            flagUpdate = false;
            
            txtAccountSearch.Text = "";
            txtName.Text = "";
            txtOpenigBalance.Text = "";
            txtShrtName.Text = "";
            rbAsset.Checked = false;
            rbBalanceSheet.Checked = false;
            rbCredit.Checked = false;
            rbDebit.Checked = false;
            rbExpenditure.Checked = false;
            rbIncome.Checked = false;
            rbLiability.Checked = false;
            rbPL.Checked = false;
            
        }

        private void txtOpenigBalance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false)
                e.Handled = true;
            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(CheckData())
            {
                if (SaveData() > 0)
                {

                    objDB = new SQLDB();
                    SqlParameter[] param = new SqlParameter[8];
                    DataSet ds = new DataSet();

                    try
                    {
                        param[0] = objDB.CreateParameter("@xCmpny", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                        param[1] = objDB.CreateParameter("@xAcctCd", DbType.String, txtAccountSearch.Text, ParameterDirection.Input);
                        param[2] = objDB.CreateParameter("@xDocMonth", DbType.String, CommonData.DocMonth, ParameterDirection.Input);
                        param[3] = objDB.CreateParameter("@xUsrID", DbType.String, CommonData.LogUserId, ParameterDirection.Input);
                        param[4] = objDB.CreateParameter("@xOpBalUpdtFlag", DbType.String,'Y', ParameterDirection.Input);
                        param[5] = objDB.CreateParameter("@xOpBal", DbType.String, txtOpenigBalance.Text, ParameterDirection.Input);
                        param[6] = objDB.CreateParameter("@xOpFlag", DbType.String, cbType.SelectedItem.ToString().Substring(0,1), ParameterDirection.Input);
                        param[7] = objDB.CreateParameter("@xFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);

                        ds = objDB.ExecuteDataSet("SSCRM_PROCESS_FA_ADU", CommandType.StoredProcedure, param);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }






                    MessageBox.Show("Data saved Successfully. " , "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCancel_Click(null,null);
                    FillAutoCompleteBox();
                    flagUpdate = false;
                }

                else
                {
                    MessageBox.Show("Data Not saved."  , "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }
        private int SaveData()
        {
            int iRes =0;
            string strCMD = "";
            string strCntrlAc = "";
            if (cmbCntrlAcc.SelectedIndex == 0)
            {
                strCntrlAc = "";
            }
            else
            {
                strCntrlAc = cmbCntrlAcc.SelectedValue.ToString();
            }
            try
            {
                if (flagUpdate == false)
                {
                    strCMD = " INSERT INTO FA_ACCOUNT_MASTER(AM_COMPANY_CODE,AM_ACCOUNT_ID,AM_ACCOUNT_NAME,AM_SHORT_NAME,"+//AM_ACCOUNT_GROUP_ID," +
                        "AM_ALIE_ID,AM_ACCOUNT_TYPE_ID,AM_DEFAULT_DEBIT_CREDIT_ID,AM_BUDGET_REQUIRED,AM_BUDGET_LOCK,AM_PL_BS," +//AM_SCHEDULE_ID,AM_SCHEDULE_GROUP_ID,AM_DUMMY_ACCOUNT," +
                        "AM_CREATED_BY,AM_CREATED_DATE,AM_LDGR_PRN_FLAG,AM_COSTCENTRE_FLAG) VALUES('" + CommonData.CompanyCode + "','" + txtAccountSearch.Text + "','" + txtName.Text + "','" + txtShrtName.Text +//"',''
                        "','" + AccountFlag + "','" + strCntrlAc + "','" + DefFlag + "','N','N','" + PLBSFlag + 
                    //    "','','',''
                    "','" + CommonData.LogUserId + "',GETDATE(),'"+cmbLedFormat.SelectedValue.ToString()+"','"+cmbCostCentFlag.SelectedValue.ToString()+"')";
                }
                else
                {
                    strCMD = " UPDATE FA_ACCOUNT_MASTER SET AM_COMPANY_CODE='" + CommonData.CompanyCode + "',AM_ACCOUNT_NAME='" + txtName.Text + "',AM_SHORT_NAME='" + txtShrtName.Text + "',"+//AM_ACCOUNT_GROUP_ID=''," +
                        "AM_ALIE_ID='" + AccountFlag + "',AM_DEFAULT_DEBIT_CREDIT_ID='" + DefFlag + "',AM_BUDGET_REQUIRED='N',AM_BUDGET_LOCK='N',AM_PL_BS='" + PLBSFlag +
                        //"',AM_SCHEDULE_ID='',AM_SCHEDULE_GROUP_ID='',AM_DUMMY_ACCOUNT=''," +
                        "',AM_LAST_MODIFIED_BY='" + CommonData.LogUserId + "',AM_LAST_MODIFIED_DATE=GETDATE(),AM_LDGR_PRN_FLAG='" + cmbLedFormat.SelectedValue.ToString() + "',AM_COSTCENTRE_FLAG='"+cmbCostCentFlag.SelectedValue.ToString()+
                        "' WHERE AM_ACCOUNT_ID='" + txtAccountSearch.Text + "' AND AM_COMPANY_CODE='" + CommonData.CompanyCode + "' ";
                }

                objDB = new SQLDB();
                iRes= objDB.ExecuteSaveData(strCMD);








            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iRes;
        }
        private bool CheckData()
        {
            bool flag = true;
            if(txtAccountSearch.Text.Length==0)
            {
                MessageBox.Show("Enter Account ID", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                flag = false;
                txtAccountSearch.Focus();
                return flag;
            }
            if (txtName.Text.Length == 0)
            {
                MessageBox.Show("Enter Account Name", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtName.Focus();
                flag = false;
                return flag;
            }
            if (txtShrtName.Text.Length == 0)
            {
                MessageBox.Show("Enter Account Short Name", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtShrtName.Focus();
                flag = false;
                return flag;
            }
            if (rbAsset.Checked == false && rbLiability.Checked && false && rbExpenditure.Checked == false && rbIncome.Checked == false)
            {
                MessageBox.Show("Check Account Flag", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                rbAsset.Focus();
                flag = false;
                return flag;
            }
            if (rbCredit.Checked == false && rbDebit.Checked == false)
            {
                MessageBox.Show("Check Default A/c Balance Flag", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                rbCredit.Focus();
                flag = false;
                return flag;
            }
            if (rbPL.Checked == false && rbBalanceSheet.Checked == false)
            {
                MessageBox.Show("Check Profit&Loss or Balance Sheet Flag", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                rbPL.Focus();
                flag = false;
                return flag;
            }
            if (txtOpenigBalance.Text.Length == 0)
            {
                MessageBox.Show("Enter Opening Balance", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtOpenigBalance.Focus();
                flag = false;
                return flag;
            }
            //if(cmbCntrlAcc.SelectedIndex == 0)
            //{
            //    MessageBox.Show("Select Control Account Type", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    cmbCntrlAcc.Focus();
            //    flag = false;
            //    return flag;
            //}
           
            return flag;
        }

        private void rbAsset_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAsset.Checked == true)
            {
                rbDebit.Checked = true;
                rbBalanceSheet.Checked = true;
                AccountFlag = "A";
            }
            else
            {
                rbDebit.Checked = false;
                rbBalanceSheet.Checked = false;
            }
        }

        private void rbLiability_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLiability.Checked == true)
            {
                rbCredit.Checked = true;
                rbBalanceSheet.Checked = true;
                AccountFlag = "L";
            }
            else
            {
                rbCredit.Checked = false;
                rbBalanceSheet.Checked = false;
            }
        }

        private void rbIncome_CheckedChanged(object sender, EventArgs e)
        {
            if (rbIncome.Checked == true)
            {
                rbCredit.Checked = true;
                rbPL.Checked = true;
                AccountFlag = "I";
            }
            else
            {
                rbCredit.Checked = false;
                rbPL.Checked = false;
            }
        }

        private void rbExpenditure_CheckedChanged(object sender, EventArgs e)
        {
            if (rbExpenditure.Checked == true)
            {
                rbDebit.Checked = true;
                rbPL.Checked = true;
                AccountFlag = "E";
            }
            else
            {
                rbDebit.Checked = false;
                rbPL.Checked = false;
            }
        }

        private void rbDebit_CheckedChanged(object sender, EventArgs e)
        {
            if(rbDebit.Checked==true)
            {
                DefFlag = "D";
            }
        }

        private void rbCredit_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCredit.Checked == true)
            {
                DefFlag = "C";
            }
        }

        private void rbPL_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPL.Checked == true)
            {
                PLBSFlag= "PL";
            }
        }

        private void rbBalanceSheet_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBalanceSheet.Checked == true)
            {
                PLBSFlag = "BS";
            }
        }

        private void txtAccountSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtAccountSearch.Text.Length > 4)
            {
                FillDetails();
            }
            else
            {
                flagUpdate = false;

                txtName.Text = "";
                txtShrtName.Text = "";
                rbAsset.Checked = false;
                rbExpenditure.Checked = false;
                rbIncome.Checked = false;
                rbLiability.Checked = false;
                rbPL.Checked = false;
                rbBalanceSheet.Checked = false;
                rbDebit.Checked = false;
                rbCredit.Checked = false;
            }
        }
        private void FillDetails()
        {
            objDB= new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataTable ds = new DataTable();
           
            try
            {
                param[0] = objDB.CreateParameter("@sAccountID", DbType.String,txtAccountSearch.Text, ParameterDirection.Input);
                param[1] = objDB.CreateParameter("@xCompCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                ds = objDB.ExecuteDataSet("GetChartAccountDetails", CommandType.StoredProcedure, param).Tables[0];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (ds.Rows.Count > 0)
            {
                flagUpdate = true;

                txtName.Text = ds.Rows[0]["AM_ACCOUNT_NAME"].ToString();
                txtShrtName.Text = ds.Rows[0]["AM_SHORT_NAME"].ToString();
                if (ds.Rows[0]["AM_COSTCENTRE_FLAG"].ToString().Length>0)
                {
                    cmbCostCentFlag.SelectedValue = ds.Rows[0]["AM_COSTCENTRE_FLAG"].ToString().Trim();
                }
                else
                {
                    cmbCostCentFlag.SelectedIndex = 0;
                }
                if (ds.Rows[0]["AM_ALIE_ID"].ToString() == "A")
                {
                    rbAsset.Checked = true;
                }
                if (ds.Rows[0]["AM_ALIE_ID"].ToString() == "L")
                {
                    rbLiability.Checked = true;
                }

                if (ds.Rows[0]["AM_ALIE_ID"].ToString() == "E")
                {
                    rbExpenditure.Checked = true;
                }
                if (ds.Rows[0]["AM_ALIE_ID"].ToString() == "I")
                {
                    rbIncome.Checked = true;
                }
                if (ds.Rows[0]["AM_DEFAULT_DEBIT_CREDIT_ID"].ToString() == "D")
                {
                    rbDebit.Checked = true;
                }
                if (ds.Rows[0]["AM_DEFAULT_DEBIT_CREDIT_ID"].ToString() == "C")
                {
                    rbCredit.Checked = true;
                }
                if (ds.Rows[0]["AM_PL_BS"].ToString() == "PL")
                {
                    rbPL.Checked = true;
                }
                if (ds.Rows[0]["AM_PL_BS"].ToString() == "BS")
                {
                    rbBalanceSheet.Checked = true;
                }
                if (ds.Rows[0]["AM_ACCOUNT_TYPE_ID"].ToString().Length > 0)
                {
                    cmbCntrlAcc.SelectedValue = ds.Rows[0]["AM_ACCOUNT_TYPE_ID"].ToString();
                }
                else
                {
                    cmbCntrlAcc.SelectedIndex = 0;
                }

            }
            else
            {
                flagUpdate = false;

                txtName.Text = "";
                txtShrtName.Text = "";
                rbAsset.Checked = false;
                rbExpenditure.Checked = false;
                rbIncome.Checked = false;
                rbLiability.Checked = false;
                rbPL.Checked = false;
                rbBalanceSheet.Checked = false;
                rbDebit.Checked = false;
                rbCredit.Checked = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(flagUpdate==true)
            {
                try
                {
                    string strCMD = " delete from FA_ACCOUNT_MASTER where AM_ACCOUNT_ID='" + txtAccountSearch.Text + "' and AM_COMPANY_CODE='"+CommonData.CompanyCode+"'";
                    objDB = new SQLDB();
                    int iRes = objDB.ExecuteSaveData(strCMD);
                    if (iRes > 0)
                    {
                        MessageBox.Show("Data Deleted Successfully. ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(null, null);
                        flagUpdate = false;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnBalance_Click(object sender, EventArgs e)
        {
            OpeningBalances btnBalance = new OpeningBalances(txtAccountSearch.Text, CommonData.FinancialYear, CommonData.CompanyCode, "ACCOUNTS");
            btnBalance.ShowDialog();
        }

        private void txtName_Validated(object sender, EventArgs e)
        {
            if (txtName.Text.Length > 0)
            {
                try
                {
                    string strCMD = " SELECT AM_ACCOUNT_ID,AM_ACCOUNT_NAME AccName FROM FA_ACCOUNT_MASTER  WHERE AM_COMPANY_CODE='" + CommonData.CompanyCode + 
                        "' AND AM_ACCOUNT_GROUP_ID IS NULL and AM_ACCOUNT_NAME like '%"+txtName.Text+"%'";
                    objDB = new SQLDB();
                    DataTable dt= objDB.ExecuteDataSet(strCMD).Tables[0];
                    if(dt.Rows.Count>0)
                    {
                        txtAccountSearch.Text="";
                        txtAccountSearch.Text = dt.Rows[0]["AM_ACCOUNT_ID"].ToString();
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
