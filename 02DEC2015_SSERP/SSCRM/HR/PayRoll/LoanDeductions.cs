using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using SSCRMDB;
using System.Text;
using SSAdmin;
using SSTrans;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace SSCRM
{
    public partial class LoanDeductions : Form
    {
        //Security objSecurity = null;
        //UtilityDB objUtilityDB = null;
        SQLDB objSQLdb = null;
        HRInfo objHRdb = null;
        bool flagSave = false;
        
        public LoanDeductions()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            gvEmpLoanDetails.Rows.Clear();
            txtEcodeSearch.Text = "";
            txtPerLoan.Text = "";
            txtCompLoan.Text = "";
            txtVehiLoan.Text = "";
            txtSalAdv.Text = "";
            txtOther.Text = "";
            txtTotal.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void dtpMonth_ValueChanged(object sender, EventArgs e)
        {
            gvEmpLoanDetails.Rows.Clear();
        }

        private void btnCollect_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            objHRdb = new HRInfo();
            try
            {
                dt = objHRdb.EmployeeDeductionsDetails(dtpMonth.Value.ToString("MMMyyyy").ToUpper()).Tables[0];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            gvEmpLoanDetails.Rows.Clear();
           
            try
            {
                if (dt.Rows.Count > 0)
                {
                    for (int iVar = 0; iVar < dt.Rows.Count; iVar++)
                    {
                        gvEmpLoanDetails.Rows.Add();
                        gvEmpLoanDetails.Rows[iVar].Cells["SLNO"].Value = (iVar + 1).ToString();
                        gvEmpLoanDetails.Rows[iVar].Cells["CompCode"].Value = dt.Rows[iVar]["ld_Comp_Code"].ToString();
                        gvEmpLoanDetails.Rows[iVar].Cells["BranchCode"].Value = dt.Rows[iVar]["ld_Bran_Code"].ToString();
                        gvEmpLoanDetails.Rows[iVar].Cells["Ecode"].Value = dt.Rows[iVar]["ld_eora_code"].ToString();
                        gvEmpLoanDetails.Rows[iVar].Cells["EmpName"].Value = dt.Rows[iVar]["ld_emp_name"].ToString();
                        gvEmpLoanDetails.Rows[iVar].Cells["Desig"].Value = dt.Rows[iVar]["ld_desig_name"].ToString();
                        gvEmpLoanDetails.Rows[iVar].Cells["Department"].Value = dt.Rows[iVar]["ld_dept_name"].ToString();
                        gvEmpLoanDetails.Rows[iVar].Cells["PersonalLoan"].Value = dt.Rows[iVar]["ld_Pers_Loan"].ToString();
                        gvEmpLoanDetails.Rows[iVar].Cells["ComputerLoan"].Value = dt.Rows[iVar]["ld_laptop_Loan"].ToString();
                        gvEmpLoanDetails.Rows[iVar].Cells["VehicleLoan"].Value = dt.Rows[iVar]["ld_veh_loan"].ToString();
                        gvEmpLoanDetails.Rows[iVar].Cells["SalaryAdvance"].Value = dt.Rows[iVar]["ld_sal_Adv"].ToString();
                        gvEmpLoanDetails.Rows[iVar].Cells["Other"].Value = dt.Rows[iVar]["ld_Other_dedu"].ToString();
                        gvEmpLoanDetails.Rows[iVar].Cells["Total"].Value = dt.Rows[iVar]["ld_tot_dedu"].ToString();
                    }
                    
                    foreach (DataGridViewRow oItem in gvEmpLoanDetails.Rows)
                    {
                        if (Convert.ToDouble(oItem.Cells["PersonalLoan"].Value.ToString()) == 0)
                        {
                            oItem.Cells["PersonalLoan"].Value = "";
                        }
                        if (Convert.ToDouble(oItem.Cells["ComputerLoan"].Value.ToString()) == 0)
                        {
                            oItem.Cells["ComputerLoan"].Value = "";
                        }
                        if (Convert.ToDouble(oItem.Cells["VehicleLoan"].Value.ToString()) == 0)
                        {
                            oItem.Cells["VehicleLoan"].Value = "";
                        }
                        if (Convert.ToDouble(oItem.Cells["SalaryAdvance"].Value.ToString()) == 0)
                        {
                            oItem.Cells["SalaryAdvance"].Value = "";
                        }
                        if (Convert.ToDouble(oItem.Cells["Other"].Value.ToString()) == 0)
                        {
                            oItem.Cells["Other"].Value = "";
                        }
                        if (Convert.ToDouble(oItem.Cells["Total"].Value.ToString()) == 0)
                        {
                            oItem.Cells["Total"].Value = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            calculateTotal();
           
        }
        private void calculateTotal()
        {
            double totPerLoan = 0;
            double totComputerLoan = 0;
            double totvehLoan = 0;
            double totSalAdv = 0;
            double totOther = 0;
            double Total = 0;
            for (int nRow = 0; nRow < gvEmpLoanDetails.Rows.Count; nRow++)
            {

                if (gvEmpLoanDetails.Rows[nRow].Cells["PersonalLoan"].Value.ToString() != "")
                {
                    totPerLoan += Convert.ToDouble(gvEmpLoanDetails.Rows[nRow].Cells["PersonalLoan"].Value.ToString());
                }
                if (gvEmpLoanDetails.Rows[nRow].Cells["ComputerLoan"].Value.ToString() != "")
                {
                    totComputerLoan += Convert.ToDouble(gvEmpLoanDetails.Rows[nRow].Cells["ComputerLoan"].Value.ToString());
                }
                if (gvEmpLoanDetails.Rows[nRow].Cells["VehicleLoan"].Value.ToString() != "")
                {
                    totvehLoan += Convert.ToDouble(gvEmpLoanDetails.Rows[nRow].Cells["VehicleLoan"].Value.ToString());
                }
                if (gvEmpLoanDetails.Rows[nRow].Cells["SalaryAdvance"].Value.ToString() != "")
                {
                    totSalAdv += Convert.ToDouble(gvEmpLoanDetails.Rows[nRow].Cells["SalaryAdvance"].Value.ToString());
                }
                if (gvEmpLoanDetails.Rows[nRow].Cells["Other"].Value.ToString() != "")
                {
                    totOther += Convert.ToDouble(gvEmpLoanDetails.Rows[nRow].Cells["Other"].Value.ToString());
                }
            }
                txtPerLoan.Text = totPerLoan.ToString();
                txtCompLoan.Text = totComputerLoan.ToString();
                txtVehiLoan.Text = totvehLoan.ToString();
                txtSalAdv.Text = totSalAdv.ToString();
                txtOther.Text = totOther.ToString();

                Total = totPerLoan + totComputerLoan + totvehLoan + totSalAdv + totOther;
                txtTotal.Text = Total.ToString();
            
        }
        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            gvEmpLoanDetails.ClearSelection();
            int rowIndex = 0;
            foreach (DataGridViewRow row in gvEmpLoanDetails.Rows)
            {
                if (row.Cells["Ecode"].Value.ToString().Contains(txtEcodeSearch.Text) == true)
                {
                    rowIndex = row.Index;
                    gvEmpLoanDetails.CurrentCell = gvEmpLoanDetails.Rows[rowIndex].Cells["Ecode"];
                    break;
                }
            }
        }
        private bool getMonth()
        {
            string strGet = "";
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                strGet = "SELECT HWP_STATUS FROM HR_WAGE_PERIOD WHERE HWP_WAGE_MONTH = '" + Convert.ToDateTime(dtpMonth.Value).ToString("MMMyyyy") + "' ";
                dt = objSQLdb.ExecuteDataSet(strGet).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(" Please Select Valid WagePeriod ");
                    return false;
                }
                else if (dt.Rows[0][0].ToString() == "RUNNING")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            finally
            {
                objSQLdb = null;
                dt = null;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (getMonth())
            {
                try
                {
                    string strCMD = "";
                    objSQLdb = new SQLDB();
                    strCMD += " Delete HR_PAYROLL_DEDUCTIONS WHERE HPLD_WAGE_MONTH='" + dtpMonth.Value.ToString("MMMyyyy") + "'";
                    objSQLdb.ExecuteSaveData(strCMD);
                    strCMD = "";
                    for (int iVar = 0; iVar < gvEmpLoanDetails.Rows.Count; iVar++)
                    {
                        double prLoan = 0;
                        double compLoan = 0;
                        double vehLoan = 0;
                        double salLoan = 0;
                        double other = 0;
                        // double Total = 0;

                        try { prLoan = Convert.ToDouble(gvEmpLoanDetails.Rows[iVar].Cells["PersonalLoan"].Value); }
                        catch { prLoan = 0; gvEmpLoanDetails.Rows[iVar].Cells["PersonalLoan"].Value = ""; }

                        try { compLoan = Convert.ToDouble(gvEmpLoanDetails.Rows[iVar].Cells["ComputerLoan"].Value); }
                        catch { compLoan = 0; gvEmpLoanDetails.Rows[iVar].Cells["ComputerLoan"].Value = ""; }

                        try { vehLoan = Convert.ToDouble(gvEmpLoanDetails.Rows[iVar].Cells["VehicleLoan"].Value); }
                        catch { vehLoan = 0; gvEmpLoanDetails.Rows[iVar].Cells["VehicleLoan"].Value = ""; }

                        try { salLoan = Convert.ToDouble(gvEmpLoanDetails.Rows[iVar].Cells["SalaryAdvance"].Value); }
                        catch { salLoan = 0; gvEmpLoanDetails.Rows[iVar].Cells["SalaryAdvance"].Value = ""; }

                        try { other = Convert.ToDouble(gvEmpLoanDetails.Rows[iVar].Cells["Other"].Value); }
                        catch { other = 0; gvEmpLoanDetails.Rows[iVar].Cells["Other"].Value = ""; }

                        strCMD += " Insert into HR_PAYROLL_DEDUCTIONS(HPLD_COMP_CODE,HPLD_BRANCH_CODE ,HPLD_EORA_CODE,HPLD_WAGE_MONTH ,HPLD_PERS_LOAN ," +
                                        " HPLD_COMP_LOAN,HPLD_VEH_LOAN,HPLD_SAL_ADV,HPLD_OTHER,HPLD_CREATED_BY,HPLD_CREATED_DATE)VALUES(" +
                                        "'" + gvEmpLoanDetails.Rows[iVar].Cells["CompCode"].Value +
                                        "','" + gvEmpLoanDetails.Rows[iVar].Cells["BranchCode"].Value +
                                        "','" + gvEmpLoanDetails.Rows[iVar].Cells["Ecode"].Value +
                                        "','" + dtpMonth.Value.ToString("MMMyyyy") +
                                        "','" + prLoan + "','" + compLoan +
                                        "','" + vehLoan + "','" + salLoan +
                                        "','" + other + "','" + CommonData.LogUserId + "',getdate())";
                    }
                    int iRes = objSQLdb.ExecuteSaveData(strCMD);
                    if (iRes > 0)
                    {
                        MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //flagSave = true;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    //MessageBox.Show("Please Select Running WagePeriod", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //flagSave = false;
                }
                finally
                {
                    objSQLdb = null;
                }
            }
            else
            {
                MessageBox.Show("Please Select Running WagePeriod", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvEmpLoanDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            double prLoan = 0;
            double compLoan = 0;
            double vehLoan = 0;
            double salLoan = 0;
            double other = 0;
            double Total = 0;

            try { prLoan = Convert.ToDouble(gvEmpLoanDetails.Rows[e.RowIndex].Cells["PersonalLoan"].Value); }
            catch { prLoan = 0; gvEmpLoanDetails.Rows[e.RowIndex].Cells["PersonalLoan"].Value = ""; }

            try { compLoan = Convert.ToDouble(gvEmpLoanDetails.Rows[e.RowIndex].Cells["ComputerLoan"].Value); }
            catch { compLoan = 0; gvEmpLoanDetails.Rows[e.RowIndex].Cells["ComputerLoan"].Value = ""; }

            try { vehLoan = Convert.ToDouble(gvEmpLoanDetails.Rows[e.RowIndex].Cells["VehicleLoan"].Value); }
            catch { vehLoan = 0; gvEmpLoanDetails.Rows[e.RowIndex].Cells["VehicleLoan"].Value = ""; }

            try { salLoan = Convert.ToDouble(gvEmpLoanDetails.Rows[e.RowIndex].Cells["SalaryAdvance"].Value); }
            catch { salLoan = 0; gvEmpLoanDetails.Rows[e.RowIndex].Cells["SalaryAdvance"].Value=""; }

            try { other = Convert.ToDouble(gvEmpLoanDetails.Rows[e.RowIndex].Cells["Other"].Value); }
            catch { other = 0; gvEmpLoanDetails.Rows[e.RowIndex].Cells["Other"].Value=""; }

            Total = prLoan + compLoan + vehLoan + salLoan + other;
            gvEmpLoanDetails.Rows[e.RowIndex].Cells["Total"].Value = Total;
            
            calculateTotal();
        }

        private void LoanDeductions_Load(object sender, EventArgs e)
        {
            //getMonth();
            dtpMonth.Value = DateTime.Now;
            gvEmpLoanDetails.Columns["PersonalLoan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gvEmpLoanDetails.Columns["ComputerLoan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gvEmpLoanDetails.Columns["VehicleLoan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gvEmpLoanDetails.Columns["SalaryAdvance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gvEmpLoanDetails.Columns["Other"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gvEmpLoanDetails.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
    }
}
