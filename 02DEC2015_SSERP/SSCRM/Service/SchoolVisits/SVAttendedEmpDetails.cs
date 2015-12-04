using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;


namespace SSCRM
{
    public partial class SVAttendedEmpDetails : Form
    {
        SQLDB objSQLdb = null;
        public frmSchoolVisits objfrmSchoolVisits;

        public SVAttendedEmpDetails()
        {
            InitializeComponent();
        }

        private void SVAttendedEmpDetails_Load(object sender, EventArgs e)
        {

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
                                  ", ndesig_name EmpDesig " +
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtEcodeSearch.Text != "")
            {

                ((frmSchoolVisits)objfrmSchoolVisits).dtAttEmpDetails.Rows.Add(new object[] {"-1",txtEcodeSearch.Text,txtEName.Text.ToString(),
                                                                                txtHRISDesig.Text.ToString(),txtDept.Text.ToString()});
                ((frmSchoolVisits)objfrmSchoolVisits).GetEmpDetails();

                this.Close();
            }
            else
            {
                MessageBox.Show("Please Enter Employee Ecode","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
                txtEcodeSearch.Focus();
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtEcodeSearch.Text = "";
            txtEName.Text = "";
            txtHRISDesig.Text = "";
            txtDept.Text = "";
            txtComp.Text = "";
            txtBranch.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text != "")
            {
                GetEmpDetails();
            }
           
        }
    }
}
