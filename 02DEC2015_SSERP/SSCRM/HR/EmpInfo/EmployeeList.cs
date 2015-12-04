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
    public partial class EmployeeList : Form
    {
        SQLDB objSQLdb = null;           
        public EmployeeList()
        {
            InitializeComponent();
        }

        private void EmployeeList_Load(object sender, EventArgs e)
        {
            dtpMonth.Value = DateTime.Today;
            FillCompanyData();
            FillBranchData();
            FillDepartment();
        }      
        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();

            try
            {
                string strCmd = " SELECT DISTINCT CM_COMPANY_NAME ,CM_COMPANY_CODE " +
                                   " FROM USER_BRANCH " +
                                   " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                   " INNER JOIN COMPANY_MAS CM ON CM_COMPANY_CODE = COMPANY_CODE " +
                                   " WHERE UB_USER_ID ='" + CommonData.LogUserId +
                                   "'ORDER BY CM_COMPANY_NAME";

                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

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

                cbCompany.SelectedValue = CommonData.CompanyCode;
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

            try
            {
                if (cbCompany.SelectedIndex > 0 && cbBranchType.SelectedIndex>0)
                {
                    string strCommand = " SELECT DISTINCT BRANCH_NAME,BRANCH_CODE " +
                                        " FROM USER_BRANCH " +
                                        " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                        " WHERE COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() + 
                                        "' AND BRANCH_TYPE ='" +cbBranchType.SelectedValue.ToString() +
                                        "' AND UB_USER_ID ='" + CommonData.LogUserId +
                                        "' ORDER BY BRANCH_NAME ASC";
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
                else
                {
                    cbBranches.DataSource = null; 
                }

                //cbBranches.SelectedValue = CommonData.BranchCode;
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

        private DataSet GetBranchType()
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[0];
            DataSet ds = new DataSet();
            try
            {

                ds = objSQLdb.ExecuteDataSet("GET_BRANCHTYPE", CommandType.StoredProcedure, param);

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
        private void FillBranchTypeData()
        {
            objSQLdb = new SQLDB();
            DataTable dtBranchType = new DataTable();
            cbBranchType.DataSource = null;

            try
            {
                if (cbCompany.SelectedIndex > 0)
                {
                    dtBranchType = GetBranchType().Tables[0];
                }


                if (dtBranchType.Rows.Count > 0)
                {
                    DataRow dr = dtBranchType.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dtBranchType.Rows.InsertAt(dr, 0);
                    cbBranchType.DataSource = dtBranchType;
                    cbBranchType.DisplayMember = "DisplayMember";
                    cbBranchType.ValueMember = "ValueMember";

                }
            

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
                dtBranchType = null;
            }
        }
        private void FillDepartment()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT dept_name,dept_code FROM Dept_Mas order by dept_name asc";

                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "-ALL-";
                    dr[1] = "000000";

                    dt.Rows.InsertAt(dr, 0);
                    cbDepatment.DataSource = dt;
                    cbDepatment.DisplayMember = "dept_name";
                    cbDepatment.ValueMember = "dept_code";
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

        private DataSet GetEmployeeDetails(string CompCode, string BranchType, string BranchCode, Int32 Dept, string EffDate)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCMPNY", DbType.String, CompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchType", DbType.String, BranchType, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xDeptId", DbType.Int32, Dept, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xEffDate", DbType.String, EffDate, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_EmployeeList_Details", CommandType.StoredProcedure, param);

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

            bool Chkflag = true;

            if (cbCompany.SelectedIndex == 0)
            {
                MessageBox.Show("Select Company", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Chkflag = false;
                cbCompany.Focus();
                return Chkflag;
            }
            if (cbBranchType.SelectedIndex == 0 || cbBranchType.SelectedIndex == -1)
            {
                MessageBox.Show("Select BranchType", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Chkflag = false;
                cbCompany.Focus();
                return Chkflag;
            }

            if (cbBranches.SelectedIndex == 0 || cbBranches.SelectedIndex==-1 )
            {
                MessageBox.Show("Select Branch", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Chkflag = false;
                cbCompany.Focus();
                return Chkflag;
            }
            //if (dtpMonth.Value>DateTime.Today)
            //{
            //    MessageBox.Show("Select valid Month", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    Chkflag = false;
            //    cbCompany.Focus();
            //    return Chkflag;
            //}
          
            return Chkflag;
        }



        private void GetEmployeeList()
        {
            objSQLdb = new SQLDB();
            DataTable dtEmpDel = new DataTable();
            gvEmployeeList.Rows.Clear();

            try
            {
                if (cbDepatment.SelectedIndex > 0)
                {
                    dtEmpDel = GetEmployeeDetails(cbCompany.SelectedValue.ToString(), cbBranchType.SelectedValue.ToString(), cbBranches.SelectedValue.ToString()
                        ,Convert.ToInt32(cbDepatment.SelectedValue.ToString()), Convert.ToDateTime(dtpMonth.Value).ToString("MMMyyyy").ToUpper()).Tables[0];
                }
                else
                {
                    dtEmpDel = GetEmployeeDetails(cbCompany.SelectedValue.ToString(), cbBranchType.SelectedValue.ToString(), cbBranches.SelectedValue.ToString(), 0, Convert.ToDateTime(dtpMonth.Value).ToString("MMMyyyy").ToUpper()).Tables[0];
                }
                if (dtEmpDel.Rows.Count > 0)
                {
                    for (int i = 0; i < dtEmpDel.Rows.Count; i++)
                    {
                        gvEmployeeList.Rows.Add();

                        gvEmployeeList.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                        gvEmployeeList.Rows[i].Cells["ECode"].Value = dtEmpDel.Rows[i]["Ecode"].ToString();
                        gvEmployeeList.Rows[i].Cells["EName"].Value = dtEmpDel.Rows[i]["MemberName"].ToString();
                        gvEmployeeList.Rows[i].Cells["Doj"].Value = Convert.ToDateTime(dtEmpDel.Rows[i]["DateOfJoim"].ToString()).ToShortDateString();
                        gvEmployeeList.Rows[i].Cells["Dept"].Value = dtEmpDel.Rows[i]["Deprtment"].ToString();
                        gvEmployeeList.Rows[i].Cells["Desig"].Value = dtEmpDel.Rows[i]["Designation"].ToString();
                       //gvEmployeeList.Rows[i].Cells["SalStruct"].Value =                         


                    }
                }
                else
                {
                    MessageBox.Show("No Data Found", "Major Cost Center Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            finally
            {
                objSQLdb = null;
                dtEmpDel = null;
            }

        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                FillBranchTypeData();
               

            }
            else
            {
                cbBranchType.DataSource = null;
                gvEmployeeList.Rows.Clear();
               
            }
        }

        private void cbBranchType_SelectedIndexChanged(object sender, EventArgs e)
        {
        
            if (cbBranchType.SelectedIndex > 0)
            {
                FillBranchData();               

            }
            else 
            {
                cbBranches.DataSource = null;
                cbDepatment.DataSource = null; 
                gvEmployeeList.Rows.Clear();                
            }

        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                GetEmployeeList();
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dtpMonth.Value = DateTime.Today;
            //cbCompany.SelectedIndex =0;
            cbBranchType.SelectedIndex = 0;
            cbBranches.SelectedIndex = -1;
            cbDepatment.SelectedIndex = -1;
            gvEmployeeList.Rows.Clear();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void cbBranches_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvEmployeeList.Rows.Clear();
               
        }

        private void cbDepatment_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvEmployeeList.Rows.Clear();
        }

        private void dtpMonth_ValueChanged(object sender, EventArgs e)
        {
            gvEmployeeList.Rows.Clear();

        }
            

    }
}
