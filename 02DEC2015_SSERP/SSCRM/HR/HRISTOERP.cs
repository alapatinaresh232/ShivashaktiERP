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
    public partial class HRISTOERP : Form
    {
        SQLDB objDb = null;
        public HRISTOERP()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void HRISTOERP_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            FillCompanyData();

        }

        

        private void txtDsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtDsearch_Validated(object sender, EventArgs e)
        {
            if (txtDsearch.Text.Length > 4)
            {
                objDb = new SQLDB();
                DataTable dt = new DataTable();
                string strCmd = "SELECT HAMH_NAME,HAMH_COMPANY_CODE,HAMH_BRANCH_CODE FROM HR_APPL_MASTER_HEAD WHERE HAMH_EORA_CODE=" + Convert.ToInt32(txtDsearch.Text);
                try
                {
                    dt = objDb.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtName.Text = dt.Rows[0]["HAMH_NAME"] + "";
                        cbCompany.SelectedValue = dt.Rows[0]["HAMH_COMPANY_CODE"] + "";
                        cbLocation.SelectedValue = dt.Rows[0]["HAMH_BRANCH_CODE"] + "";
                        MessageBox.Show("Ecode Already Exists", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        txtName.Text = "";
                        cbCompany.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objDb = null;
                    dt = null;
                }

            }
            else
            {
                btnSave.Enabled = false;
                txtName.Text = "";
                cbCompany.SelectedIndex = 0;
            }
        }
        private void FillCompanyData()
        {
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT CM_COMPANY_NAME,CM_COMPANY_CODE FROM COMPANY_MAS";

                dt = objDb.ExecuteDataSet(strCommand).Tables[0];
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objDb = null;
                dt = null;
            }
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLocationData();
        }
        private void FillLocationData()
        {
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            cbLocation.DataSource = null;
            try
            {
                if (cbCompany.SelectedIndex > 0)
                {
                    string strCommand = "SELECT BRANCH_CODE as branchCode,BRANCH_NAME FROM BRANCH_MAS WHERE COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + "' ORDER BY BRANCH_NAME ASC";
                    dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbLocation.DataSource = dt;
                    cbLocation.DisplayMember = "BRANCH_NAME";
                    cbLocation.ValueMember = "branchCode";
                    //cbLocation.ValueMember = "LOCATION";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objDb = null;
                dt = null;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDsearch.Text = "";
            txtName.Text = "";
            cbCompany.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                objDb = new SQLDB();
                SqlParameter[] param = new SqlParameter[3];
                DataTable dt=new DataTable();
                try
                {
                    param[0] = objDb.CreateParameter("@xEcode", DbType.Int32, txtDsearch.Text, ParameterDirection.Input);
                    param[1] = objDb.CreateParameter("@xCmp", DbType.String, cbCompany.SelectedValue.ToString(), ParameterDirection.Input);
                    param[2] = objDb.CreateParameter("@xBrn", DbType.String, cbLocation.SelectedValue.ToString(), ParameterDirection.Input);
                    dt = objDb.ExecuteDataSet("CONV_EMP_HRIS2SSCRM_APPL_ecode", CommandType.StoredProcedure, param).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString().Contains("INFORMATION HAS BEEN ADDED"))
                        {
                            MessageBox.Show("Data Saved Successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnSave.Enabled = false;
                            btnClear_Click(null, null);
                        }
                        if (dt.Rows[0][0].ToString().Contains("Already Exist"))
                        {
                            MessageBox.Show("Ecode Already Exists", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnSave.Enabled = false;
                            btnClear_Click(null, null);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Data Not Saved", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    param = null;
                    objDb = null;
                    //dt = null;
                }
            }
        }
        private bool CheckData()
        {
            bool bFlag = true;
            if (txtDsearch.Text.Length < 4)
            {
                bFlag = false;
                MessageBox.Show("Please Enter Valid Ecode");
            }
            if(cbCompany.SelectedIndex==0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Company");
            }
            if (cbLocation.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Branch");
            }
            return bFlag;
        }
    }
}
