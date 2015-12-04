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
    public partial class MonthlyAttendanceForOtherBranches : Form
    {
        SQLDB objDB = null;
        string strDOJ = "";
        bool flagUpdate = false;
        public MonthlyAttendanceForOtherBranches()
        {
            InitializeComponent();
        }

        private void MonthlyAttendanceForOtherBranches_Load(object sender, EventArgs e)
        {
            dtpEffectedFrom.Value = DateTime.Today;
            txtPre.Text = "0";
            txtPH.Text = "0";
            txtWoff.Text = "0";
            txtELs.Text = "0";
            txtCl.Text = "0";
            txtSl.Text = "0";
            txtELs.Text = "0";
            txtCoffDays.Text = "0";
            txtLop.Text = "0";

            txtNetDays.Text = "0";

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void dtpEffectedFrom_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string strSQL = "select HWP_START_DATE,HWP_END_DATE,HWP_DAYS from HR_WAGE_PERIOD where HWP_WAGE_MONTH='" + (dtpEffectedFrom.Value).ToString("MMMyyyy").ToUpper()+"'";
                objDB = new SQLDB();
                DataTable dt = objDB.ExecuteDataSet(strSQL).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    dtpFrom.Value = Convert.ToDateTime(dt.Rows[0]["HWP_START_DATE"].ToString());
                    dtpTo.Value = Convert.ToDateTime(dt.Rows[0]["HWP_END_DATE"].ToString());
                    txtNoofDays.Text = dt.Rows[0]["HWP_DAYS"].ToString();
                }
                else
                {
                    MessageBox.Show("Selected WagePeriod is Not Valid");

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtEcodeSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtEcodeSearch.Text.Length>0)
            {

                try
                {
                    objDB = new SQLDB();
                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = objDB.CreateParameter("@EORACODE", DbType.String, txtEcodeSearch.Text.ToString(), ParameterDirection.Input);

                    DataTable  dt = objDB.ExecuteDataSet("GetEmpDataforEORAMas", CommandType.StoredProcedure, param).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtName.Text = dt.Rows[0]["MEMBER_NAME"].ToString();
                        txtDesig.Text = dt.Rows[0]["DESIG_NAME"].ToString();
                        txtDesig.Tag = dt.Rows[0]["DESIG_ID"].ToString();
                        txtCompany.Text = dt.Rows[0]["COMPANY_NAME"].ToString();
                        txtCompany.Tag = dt.Rows[0]["COMPANY_CODE"].ToString();
                        txtBranch.Text = dt.Rows[0]["BRANCH_NAME"].ToString();
                        txtBranch.Tag = dt.Rows[0]["BRANCH_CODE"].ToString();
                        txtDept.Text = dt.Rows[0]["Dept_Name"].ToString();
                        txtDept.Tag = dt.Rows[0]["Dept_Code"].ToString();
                        strDOJ = dt.Rows[0]["DOJ"].ToString();
                        string strSQL = "SELECT * FROM HR_PAYROLL_MANUAL_ATTD_MTOD WHERE HPOAM_ECODE=" + txtEcodeSearch.Text + " and HPOAM_WAGEMONTH='" + (dtpEffectedFrom.Value).ToString("MMMyyyy").ToUpper() + "'";

                        objDB = new SQLDB();
                        DataTable dtT = objDB.ExecuteDataSet(strSQL).Tables[0];
                        if (dtT.Rows.Count > 0)
                        {
                            flagUpdate = true;
                            txtPre.Text = dtT.Rows[0]["HPOAM_PRE"].ToString();
                            txtPH.Text = dtT.Rows[0]["HPOAM_HDAY"].ToString();
                            txtWoff.Text = dtT.Rows[0]["HPOAM_WOF"].ToString();
                            txtSl.Text = dtT.Rows[0]["HPOAM_SLS_AVAILED"].ToString();
                            txtELs.Text = dtT.Rows[0]["HPOAM_ELS_AVAILED"].ToString();
                            txtCl.Text = dtT.Rows[0]["HPOAM_CLS_AVAILED"].ToString();
                            txtCoffDays.Text = dtT.Rows[0]["HPOAM_COFFS_AVAILED"].ToString();
                            txtLop.Text = dtT.Rows[0]["HPOAM_ABSX"].ToString();
                            txtNetDays.Text = dtT.Rows[0]["HPOAM_NET"].ToString();
                        }
                    }
                    else
                    {
                        txtName.Text = "";
                        txtDesig.Text = "";
                        txtDesig.Tag = "";
                        txtCompany.Text = "";
                        txtCompany.Tag = "";
                        txtBranch.Text = "";
                        txtBranch.Tag = "";
                        txtDept.Text = "";
                        txtDept.Tag = "";
                        txtPre.Text = "0";
                        txtPH.Text = "0";
                        txtCl.Text = "0";
                        txtSl.Text = "0";
                        txtELs.Text = "0";
                        txtWoff.Text = "0";
                        txtCoffDays.Text = "0";
                        txtLop.Text = "0";
                        txtNetDays.Text = "0";
                        strDOJ = "";
                    }
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                txtName.Text = "";
                txtDesig.Text = "";
                txtDesig.Tag = "";
                txtCompany.Text = "";
                txtCompany.Tag = "";
                txtBranch.Text = "";
                txtBranch.Tag = "";
                txtDept.Text = "";
                txtDept.Tag = "";
                txtPre.Text = "0";
                txtPH.Text = "0";
                txtCl.Text = "0";
                txtSl.Text = "0";
                txtELs.Text = "0";
                txtWoff.Text = "0";
                txtCoffDays.Text = "0";
                txtLop.Text = "0";
                txtNetDays.Text = "0";
                strDOJ = "";
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

        private void btnSave_Click(object sender, EventArgs e)
        {
           if(CheckData())
           {
               try
               {
                   int iRes = 0;
                   string strCMD = "select * from HR_PAYROLL_OTHBRN_ECODE where HPOE_ECODE=" + Convert.ToInt32(txtEcodeSearch.Text);
                   objDB = new SQLDB();
                   DataTable dt  = objDB.ExecuteDataSet(strCMD).Tables[0];
                   if (flagUpdate==false)
                   {
                       strCMD = "insert into HR_PAYROLL_MANUAL_ATTD_MTOD(HPOAM_COMPANY_CODE,HPOAM_BRANCH_CODE,HPOAM_WAGEMONTH,HPOAM_ECODE,HPMTM_LOCATION,HPOAM_NET,HPOAM_PRE," +
                                "HPOAM_ABSX,HPOAM_WOF,HPOAM_HDAY,HPOAM_ELS_AVAILED,HPOAM_CLS_AVAILED,HPOAM_SLS_AVAILED,HPOAM_COFFS_AVAILED," +
                                "HPOAM_COMPANY_NAME,HPOAM_BRANCH_NAME,HPOAM_DEPT_CODE,HPOAM_DEPT_NAME,HPOAM_ENAME,HPOAM_DESIG_ID,HPOAM_DESIGNATION,HPOAM_DOJ) " +
                                "values('" + txtCompany.Tag + "','" + txtBranch.Tag + "','" + dtpEffectedFrom.Value.ToString("MMMyyyy").ToUpper() + "'," + Convert.ToInt32(txtEcodeSearch.Text);
                       if (dt.Rows.Count > 0)
                       {
                           strCMD += ",'OB'";
                       }
                       else
                       {
                           strCMD += ",'HO'";
                       }
                       strCMD+=","+txtNetDays.Text+","+txtPre.Text+","+txtLop.Text+","+txtWoff.Text+","+txtPH.Text+","+txtELs.Text+","+txtCl.Text+","+txtSl.Text+
                                ","+txtCoffDays.Text+",'"+txtCompany.Text+"','"+txtBranch.Text+"','"+txtDept.Tag+"','"+txtDept.Text+"','"+txtName.Text+"','"+txtDesig.Tag+
                                "','"+txtDesig.Text+"','"+ Convert.ToDateTime( strDOJ).ToString("dd/MMM/yyyy")+"')";
                   }
                   else
                   {
                       strCMD = "update HR_PAYROLL_MANUAL_ATTD_MTOD set HPOAM_COMPANY_CODE='" + txtCompany.Tag + "',HPOAM_BRANCH_CODE='" + txtBranch.Tag + "',HPOAM_WAGEMONTH='" + dtpEffectedFrom.Value.ToString("MMMyyyy").ToUpper();
                       if (dt.Rows.Count > 0)
                       {
                           strCMD += "',HPMTM_LOCATION='OB'";
                       }
                        else
                       {
                           strCMD += "',HPMTM_LOCATION='HO'";
                       }
                       
                       strCMD+=",HPOAM_NET=" + txtNetDays.Text + ",HPOAM_PRE=" + txtPre.Text + ",HPOAM_ABSX=" + txtLop.Text + ",HPOAM_WOF=" + txtWoff.Text + ",HPOAM_HDAY=" + txtPH.Text +
                                ",HPOAM_ELS_AVAILED=" + txtELs.Text + ",HPOAM_CLS_AVAILED=" + txtCl.Text + ",HPOAM_SLS_AVAILED=" + txtSl.Text + ",HPOAM_COFFS_AVAILED=" + txtCoffDays.Text +
                                ",HPOAM_COMPANY_NAME='" + txtCompany.Text + "',HPOAM_BRANCH_NAME='" + txtBranch.Text + "',HPOAM_DEPT_CODE='" + txtDept.Tag + "',HPOAM_DEPT_NAME='" + txtDept.Text +
                                "',HPOAM_ENAME='" + txtName.Text + "',HPOAM_DESIG_ID='" + txtDesig.Tag + "',HPOAM_DESIGNATION='" + txtDesig.Text + "',HPOAM_DOJ='" + Convert.ToDateTime(strDOJ).ToString("dd/MMM/yyyy") + "' where HPOAM_ECODE=" + Convert.ToInt32(txtEcodeSearch.Text);
                   }
                   objDB = new SQLDB();
                   iRes = objDB.ExecuteSaveData(strCMD);

                   if(iRes>0)
                   {
                       MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       btnCancel_Click(null, null);
                       //flagUpdate = false;
                   }
                   else
                   {
                       MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   }
               }
               catch(Exception ex)
               {
                   MessageBox.Show(ex.ToString());
               }
           }
        }

        private bool CheckData()
        {
            bool flag=true;
            if (txtName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Valid Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEcodeSearch.Focus();
            }
            if(Convert.ToDouble(txtNetDays.Text)>Convert.ToDouble( txtNoofDays.Text))
            {
                flag = false;
                MessageBox.Show("Net Days Should not be exceeded Month Days(30/31) ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEcodeSearch.Focus();
            }
            return flag;
        }
        private void CalculatingTotalNoofDays()
        {
            double dPre = Convert.ToDouble(txtPre.Text);
            double dPh = Convert.ToDouble(txtPH.Text);
            double dWoff = Convert.ToDouble(txtWoff.Text);
            double dSls = Convert.ToDouble(txtSl.Text);
            double dCLs = Convert.ToDouble(txtCl.Text);
            double dELs = Convert.ToDouble(txtELs.Text);
            double dCoff = Convert.ToDouble(txtCoffDays.Text);
            double dLop = Convert.ToDouble(txtLop.Text);
            double dNet= Convert.ToDouble(txtNetDays.Text);

            dNet = dPre + dPh + dWoff + dSls + dELs + dCLs + dCoff + dLop;
            txtNetDays.Text = dNet.ToString();
            if (dNet > Convert.ToDouble(txtNoofDays.Text))
            {
                MessageBox.Show("Net Days Should not be exceeded Month Days(30/31)");
                txtNetDays.Text = txtNoofDays.Text;
            }
        }

        private void txtPre_Validated(object sender, EventArgs e)
        {
            CalculatingTotalNoofDays();
        }

        private void txtPH_Validated(object sender, EventArgs e)
        {
            CalculatingTotalNoofDays();
        }

        private void txtWoff_Validated(object sender, EventArgs e)
        {
            CalculatingTotalNoofDays();
        }

        private void txtELs_Validated(object sender, EventArgs e)
        {
            CalculatingTotalNoofDays();
        }

        private void txtCl_Validated(object sender, EventArgs e)
        {
            CalculatingTotalNoofDays();
        }

        private void txtSl_Validated(object sender, EventArgs e)
        {
            CalculatingTotalNoofDays();
        }

        private void txtCoffDays_Validated(object sender, EventArgs e)
        {
            CalculatingTotalNoofDays();
        }

        private void txtLop_Validated(object sender, EventArgs e)
        {
            CalculatingTotalNoofDays();
        }

        private void txtNetDays_Validated(object sender, EventArgs e)
        {
            CalculatingTotalNoofDays();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtEcodeSearch.Text = "";
            txtPre.Text = "0";
            txtPH.Text = "0";
            txtCl.Text = "0";
            txtSl.Text = "0";
            txtELs.Text = "0";
            txtWoff.Text = "0";
            txtCoffDays.Text = "0";
            txtLop.Text = "0";
            txtNetDays.Text = "0";
            strDOJ = "";
        }

        private void txtPre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
        }

        private void txtELs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
        }

        private void txtCoffDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
        }

        private void txtPH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
        }

        private void txtCl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
        }

        private void txtLop_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
        }

        private void txtWoff_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
        }

        private void txtSl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
        }

    }
}
