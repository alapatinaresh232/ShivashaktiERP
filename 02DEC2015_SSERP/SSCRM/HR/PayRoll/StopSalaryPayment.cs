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


namespace SSCRM
{
    public partial class StopSalaryPayment : Form
    {
        SQLDB objDB = null;
        string strDOJ = "";
        int id = 0;
        bool flagUpdate = false;
        public StopSalaryPayment()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEcodeSearch.Text = "";
            txtRemarks.Text = "";
            cbStopType.SelectedIndex = 0;
            flagUpdate = false;
        }

        private void txtEcodeSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtEcodeSearch.Text.Length > 0)
            {

                try
                {
                    objDB = new SQLDB();
                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = objDB.CreateParameter("@EORACODE", DbType.String, txtEcodeSearch.Text.ToString(), ParameterDirection.Input);

                    DataTable dt = objDB.ExecuteDataSet("GetEmpDataforEORAMas", CommandType.StoredProcedure, param).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtName.Text = dt.Rows[0]["MEMBER_NAME"].ToString();
                        txtName.Tag = dt.Rows[0]["ApplNo"].ToString();
                        txtDesig.Text = dt.Rows[0]["DESIG_NAME"].ToString();
                        txtDesig.Tag = dt.Rows[0]["DESIG_ID"].ToString();
                        txtCompany.Text = dt.Rows[0]["COMPANY_NAME"].ToString();
                        txtCompany.Tag = dt.Rows[0]["COMPANY_CODE"].ToString();
                        txtBranch.Text = dt.Rows[0]["BRANCH_NAME"].ToString();
                        txtBranch.Tag = dt.Rows[0]["BRANCH_CODE"].ToString();
                        txtDept.Text = dt.Rows[0]["Dept_Name"].ToString();
                        txtDept.Tag = dt.Rows[0]["Dept_Code"].ToString();
                        strDOJ = dt.Rows[0]["DOJ"].ToString();

                        string strCMD = "select * from HR_PAYROLL_STOP_PAYMENT where HPSP_EORA_CODE="+Convert.ToInt32( txtEcodeSearch.Text);
                        objDB = new SQLDB();
                        DataTable dt1 = objDB.ExecuteDataSet(strCMD).Tables[0];
                        if(dt1.Rows.Count>0)
                        {

                            flagUpdate = true;
                            id = Convert.ToInt32( dt1.Rows[0]["HPSP_ID"].ToString());
                            txtRemarks.Text = dt1.Rows[0]["HPSP_REMARKS"].ToString();
                            dtpWagePerioad.Value = Convert.ToDateTime( dt1.Rows[0]["HPSP_WAGE_PERIOD"].ToString());
                            cbStopType.SelectedItem = dt1.Rows[0]["HPSP_STOP_TYPE"];
                        }
                    }
                    else
                    {
                        txtName.Text = "";
                        txtName.Tag = "";
                        txtDesig.Text = "";
                        txtDesig.Tag = "";
                        txtCompany.Text = "";
                        txtCompany.Tag = "";
                        txtBranch.Text = "";
                        txtBranch.Tag = "";
                        txtDept.Text = "";
                        txtDept.Tag = "";
                        strDOJ = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                txtName.Text = "";
                txtName.Tag = "";
                txtDesig.Text = "";
                txtDesig.Tag = "";
                txtCompany.Text = "";
                txtCompany.Tag = "";
                txtBranch.Text = "";
                txtBranch.Tag = "";
                txtDept.Text = "";
                txtDept.Tag = "";
                strDOJ = "";
            }

                
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(CheckData())
            {
                if (SaveData() > 0)
                {
                    MessageBox.Show("Data Saved Successfully");
                    flagUpdate = false;
                }
                else
                {
                    MessageBox.Show("Data Not Saved ");
                }
            }
        }

        private int SaveData()
        {
            int iRes = 0;
            string strCMD = "";
            try
            {
                if (flagUpdate == false)
                {
                    strCMD = " insert into HR_PAYROLL_STOP_PAYMENT(HPSP_APPL_NO,HPSP_EORA_CODE,HPSP_WAGE_PERIOD,HPSP_REMARKS,HPSP_STOP_TYPE,HPSP_CREATED_BY,HPSP_CREATED_DATE)" +
                                " values(" + txtName.Tag + "," + txtEcodeSearch.Text + ",'" + dtpWagePerioad.Value.ToString("dd/MMM/yyyy") +
                                "','" + txtRemarks.Text + "','" + cbStopType.SelectedItem.ToString() + "','" + CommonData.LogUserId + "',getdate())";
                }
                else
                {
                    strCMD = " update HR_PAYROLL_STOP_PAYMENT set HPSP_EORA_CODE=" + txtEcodeSearch.Text + ", HPSP_WAGE_PERIOD='" + dtpWagePerioad.Value.ToString("dd/MMM/yyyy") +
                            "',HPSP_REMARKS='" + txtRemarks.Text + "', HPSP_STOP_TYPE='" + cbStopType.SelectedItem.ToString() + "', HPSP_MODIFIED_BY='" + CommonData.LogUserId +
                            "',HPSP_MODIFIED_DATE=getdate() where HPSP_ID="+id;
                }
                objDB = new SQLDB();

                iRes = objDB.ExecuteSaveData(strCMD);

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
            if (txtName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Valid Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEcodeSearch.Focus();
            }
            if(txtRemarks.Text.Length==0)
            {
                flag = false;
                MessageBox.Show("Please Enter Remarks", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRemarks.Focus();
            }
            if(cbStopType.SelectedIndex==0)
            {

                flag = false;
                MessageBox.Show("Select Stop Type", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbStopType.Focus();
            }
            return flag;
        }

        private void StopSalaryPayment_Load(object sender, EventArgs e)
        {
            cbStopType.SelectedIndex = 0;
        }

       
    }
}
