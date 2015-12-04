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
    public partial class PMAttendedEmpDetails : Form
    {
        SQLDB objSQLdb = null;
        public frmProductPromotion objfrmProductPromotion;
        string strEcode = "";

        public PMAttendedEmpDetails()
        {
            InitializeComponent();
        }
        public PMAttendedEmpDetails(string EmpCode)
        {
            InitializeComponent();
            strEcode = EmpCode;
        }

        private void PMAttendedEmpDetails_Load(object sender, EventArgs e)
        {
            if (strEcode.Length > 0)
            {
                txtEcodeSearch.Text = strEcode;
                txtEcodeSearch_KeyUp(null, null);
            }
        }

        private void GetEmpDetails()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            if (txtEcodeSearch.Text != "")
            {
                try
                {

                    strCmd = "SELECT HRIS_EMP_NAME EmpName " +
                                  ", desig_name EmpDesig " +
                                  ", dept_name DeptName " +
                                  ", CM_COMPANY_NAME CompName " +
                                  ", BRANCH_NAME	BranchName " +
                                  " FROM EORA_MASTER EM " +
                                  " INNER JOIN BRANCH_MAS BM ON BM.BRANCH_CODE = EM.BRANCH_CODE " +
                                  " INNER JOIN COMPANY_MAS ON CM_COMPANY_CODE = EM.company_code " +
                                  " INNER JOIN Dept_Mas ON dept_code = DEPT_ID " +
                                  " INNER JOIN DESIG_MAS ON desig_code = DESG_ID " +
                                  " WHERE ECODE = " + Convert.ToInt32(txtEcodeSearch.Text) + "";

                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        txtEName.Text = dt.Rows[0]["EmpName"].ToString();
                        txtComp.Text = dt.Rows[0]["CompName"].ToString();
                        txtBranch.Text = dt.Rows[0]["BranchName"].ToString();
                        txtDept.Text = dt.Rows[0]["DeptName"].ToString();
                        txtHRISDesig.Text = dt.Rows[0]["EmpDesig"].ToString();
                    }
                    else
                    {
                        txtEName.Text = "";
                        txtComp.Text = "";
                        txtBranch.Text = "";
                        txtDept.Text = "";
                        txtHRISDesig.Text = "";
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
                txtComp.Text = "";
                txtBranch.Text = "";
                txtDept.Text = "";
                txtHRISDesig.Text = "";

            }
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text.Length > 4)
            {
                GetEmpDetails();
            }
            else
            {
                txtEName.Text = "";
                txtComp.Text = "";
                txtBranch.Text = "";
                txtDept.Text = "";
                txtHRISDesig.Text = "";
            }
        }

        private bool CheckData()
        {
            bool flag = true;

            if (txtEName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEcodeSearch.Focus();
            }

            if (((frmProductPromotion)objfrmProductPromotion).gvAttendedEmpDetails.Rows.Count > 0)
            {
                for (int i = 0; i < ((frmProductPromotion)objfrmProductPromotion).gvAttendedEmpDetails.Rows.Count; i++)
                {
                    if (txtEcodeSearch.Text.Equals(((frmProductPromotion)objfrmProductPromotion).gvAttendedEmpDetails.Rows[i].Cells["Ecode"].Value.ToString()))
                    {
                        flag = false;
                        //MessageBox.Show("Already Exists", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //return flag;
                        this.Close();
                    }
                }
            }
           

            return flag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {
                ((frmProductPromotion)objfrmProductPromotion).dtAttEmpDetails.Rows.Add(new object[] {"-1",txtEcodeSearch.Text,txtEName.Text.ToString(),
                                                                                txtHRISDesig.Text.ToString(),txtDept.Text.ToString()});
                ((frmProductPromotion)objfrmProductPromotion).GetEmpDetails();

                this.Close();
            }          

        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtEcodeSearch.Text = "";
            txtBranch.Text = "";
            txtComp.Text = "";
            txtDept.Text = "";
            txtEName.Text = "";
            txtHRISDesig.Text = "";

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();

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
    }
}
