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
    public partial class GCGLAdvances : Form
    {
        SQLDB objSQLdb = null;
        InvoiceDB objData = null;
        string strECode = "";
        public SPInvoice objInvoice;
        StockPointDB objSPDB = null;
       

        public GCGLAdvances()
        {
            InitializeComponent();
        }      

        private void GCGLAdvances_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            FillFinYear();
            cbFinYear.SelectedValue = CommonData.FinancialYear;            
            //dtpDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(string.Format("01"+ CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
            cbDocMonths.SelectedIndex = 0;

            //if (CommonData.LogUserId.ToUpper() == "ADMIN")
            //{
            //    cbCompany.Enabled = true;
            //    cbBranches.Enabled = true;
            //    cbFinYear.Enabled = true;
            //}
            //else
            //{
            //    cbCompany.Enabled = false;
            //    cbBranches.Enabled = false;
            //    cbFinYear.Enabled = false;
            //}
        }


        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT DISTINCT CM_COMPANY_NAME ,CM_COMPANY_CODE " +
                                   " FROM USER_BRANCH " +
                                   " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                   " INNER JOIN COMPANY_MAS ON CM_COMPANY_CODE = COMPANY_CODE " +
                                   " WHERE UB_USER_ID ='" + CommonData.LogUserId +
                                   "' ORDER BY CM_COMPANY_NAME";
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
                //cbCompany.SelectedValue = CommonData.CompanyCode;
                //dt = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
            }
        }

        private void FillBranchData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();

            try
            {
                if (cbCompany.SelectedIndex > 0)
                {
                    string strCommand = "SELECT DISTINCT BRANCH_NAME BranchName,BRANCH_CODE+'@'+STATE_CODE+'@'+sm_state BranCode " +
                                         " FROM USER_BRANCH " +
                                         " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                         " INNER JOIN state_mas ON sm_state_code=STATE_CODE "+
                                         " WHERE COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() +
                                         "' AND UB_USER_ID ='" + CommonData.LogUserId +
                                         "' AND BRANCH_TYPE IN ('BR') ORDER BY BRANCH_NAME ASC";

                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbBranches.DataSource = dt;
                    cbBranches.DisplayMember = "BranchName";
                    cbBranches.ValueMember = "BranCode";
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
            }
        }

        private void FillFinYear()
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

        private void EcodeSearch()
        {
            objData = new InvoiceDB();
            DataSet dsEmp = null;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                cbEcode.DataSource = null;
                cbEcode.Items.Clear();
                dsEmp = objData.InvLevelEcodeSearch_Get(cbCompany.SelectedValue.ToString(), cbBranches.SelectedValue.ToString().Split('@')[0],CommonData.DocMonth, txtEcodeSearch.Text);
                DataTable dtEmp = dsEmp.Tables[0];
                if (dtEmp.Rows.Count > 0)
                {
                    cbEcode.DataSource = dtEmp;
                    cbEcode.DisplayMember = "ENAME";
                    cbEcode.ValueMember = "ECODE";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (cbEcode.SelectedIndex > -1)
                {
                    cbEcode.SelectedIndex = 0;
                    strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
                }
                objData = null;
                Cursor.Current = Cursors.Default;
            }

        }


        private void FillEmployeeData()
        {
           objSPDB = new StockPointDB();
            DataTable dtEmp = null;          
            cbEcode.DataSource = null;
            cbEcode.Items.Clear();
            if (cbCompany.SelectedIndex > 0 && cbBranches.SelectedIndex > 0)
            {
                try
                {
                    dtEmp = objSPDB.Get_GCandAboveList(cbCompany.SelectedValue.ToString(), cbBranches.SelectedValue.ToString().Split('@')[0], CommonData.DocMonth,txtEcodeSearch.Text.ToString());

                    if (dtEmp.Rows.Count > 0)
                    {
                        cbEcode.DataSource = dtEmp;
                        cbEcode.DisplayMember = "ENAME";
                        cbEcode.ValueMember = "ECODE";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (cbEcode.SelectedIndex > -1)
                    {
                        cbEcode.SelectedIndex = 0;
                        strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
                    }
                    objData = null;
                    Cursor.Current = Cursors.Default;
                }
            }

        }
      

        private void cbBranches_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillEmployeeData();
            FillGCGLAdvances();
        }

        public DataSet Get_GCGLAdvances(string sCompCode, string sBranchCode,string sFinYear,string sDocMonth,Int32 nEcode,string sRepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xFinYear", DbType.String, sFinYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xEcode", DbType.Int32, nEcode, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xRep_Type", DbType.String, sRepType, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GCGLAdvances_Get", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }

      

        public void FillGCGLAdvances()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            gvAdvancesDetl.Rows.Clear();
            string sRepType = "";

            if (cbCompany.SelectedIndex > 0 && cbBranches.SelectedIndex > 0 && cbFinYear.SelectedIndex > 0 && cbEcode.SelectedIndex>-1)
            {
                try
                {
                    if (cbDocMonths.SelectedIndex == 0)
                    {
                        sRepType = "CURRENT";
                    }
                    else
                    {
                        sRepType = "PREVIOUS";
                    }
                    if (cbEcode.SelectedIndex > -1)
                    {
                        strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
                    }

                    dt = Get_GCGLAdvances(cbCompany.SelectedValue.ToString(), cbBranches.SelectedValue.ToString().Split('@')[0], cbFinYear.SelectedValue.ToString(), "", Convert.ToInt32(strECode), sRepType).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            gvAdvancesDetl.Rows.Add();
                            gvAdvancesDetl.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                            gvAdvancesDetl.Rows[i].Cells["OrderNo"].Value = dt.Rows[i]["OrderNo"].ToString();
                            gvAdvancesDetl.Rows[i].Cells["OrderDate"].Value = Convert.ToDateTime(dt.Rows[i]["OrderDate"].ToString()).ToString("dd/MMM/yyyy");
                            gvAdvancesDetl.Rows[i].Cells["DocMonth"].Value = dt.Rows[i]["DocMonth"].ToString();
                            gvAdvancesDetl.Rows[i].Cells["CampName"].Value = dt.Rows[i]["CampName"].ToString();
                            gvAdvancesDetl.Rows[i].Cells["SRName"].Value = dt.Rows[i]["EmpName"].ToString();
                            gvAdvancesDetl.Rows[i].Cells["CustomerName"].Value = dt.Rows[i]["CustomerName"].ToString();
                            gvAdvancesDetl.Rows[i].Cells["OrderQty"].Value = dt.Rows[i]["Qty"].ToString();
                            gvAdvancesDetl.Rows[i].Cells["OrderAmt"].Value = dt.Rows[i]["Orderamt"].ToString();
                            gvAdvancesDetl.Rows[i].Cells["Status"].Value = dt.Rows[i]["Status"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void cbFinYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0 && cbBranches.SelectedIndex > 0 && cbFinYear.SelectedIndex > 0)
            {
                FillGCGLAdvances();
            }

        }

        private void cbEcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEcode.SelectedIndex > -1)
            {
                FillGCGLAdvances();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbCompany.SelectedIndex = 0;
            cbBranches.SelectedIndex = -1;
            cbFinYear.SelectedValue = CommonData.FinancialYear;            
            gvAdvancesDetl.Rows.Clear();
            cbDocMonths.SelectedIndex = 0;
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillBranchData();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text.ToString().Trim().Length > 4)
                FillEmployeeData();
            //else
            //    FillEmployeeData();
        }

        private void gvAdvancesDetl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string strOrderNo = "";

            if (e.RowIndex >= 0)
            {
                strOrderNo = gvAdvancesDetl.Rows[e.RowIndex].Cells["OrderNo"].Value.ToString();

                SPInvoice objSPInvoice = new SPInvoice(cbCompany.SelectedValue.ToString(),cbBranches.SelectedValue.ToString(),cbFinYear.Text.ToString(),strOrderNo);
                objSPInvoice.objGCGLAdvances = this;
                objSPInvoice.Show();
            }
        }

        private void cbDocMonths_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGCGLAdvances();

        }

       
    }
}
