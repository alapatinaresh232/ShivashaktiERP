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
    public partial class MonthlyAttdProcess : Form
    {
        private SQLDB objSQLdb = null;
        bool flagSave = false;
        private bool CellLOP = true;
        private int intCurrentRow = 0;
        private int intCurrentCell = 0;


        public MonthlyAttdProcess()
        {
            InitializeComponent();
        }
        private void MonthlyPayRoll_Load(object sender, EventArgs e)
        {
            getDocMonth();

            //this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
            //              (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            //txtWagePeriod.Text = DateTime.Today.ToString("MMMyyyy");
        }
        private void getDocMonth()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT BPWP_DOC_MONTH FROM BR_PAYROLL_WAGE_PERIOD WHERE BPWP_STATUS='RUNNING' AND BPWP_BRANCH_CODE= '" + CommonData.BranchCode + "' ";

                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtWagePeriod.Text = dt.Rows[0][0].ToString();
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
        private void btnCancel_Click(object sender, EventArgs e)
        {
            gvEmpDetails.Rows.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        private bool CheckData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            bool flagSave = true;
            string status = "";
            try
            {
                status = objSQLdb.ExecuteDataSet("SELECT BPWP_STATUS FROM BR_PAYROLL_WAGE_PERIOD " +
                                                    " WHERE BPWP_BRANCH_CODE='" + CommonData.BranchCode +
                                                    "' AND BPWP_DOC_MONTH='" + txtWagePeriod.Text +
                                                    "' AND BPWP_PAYROLL_TYPE='" + cbPayRollType.SelectedItem.ToString() +
                                                    "'").Tables[0].Rows[0][0].ToString();
            }
            catch
            {
                status = "";
            }
            finally
            {
                objSQLdb = null;
            }
            if (status != "RUNNING")
            {
                flagSave = false;
                MessageBox.Show("Payroll Status is not in Running Mode for Current Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return flagSave;
            }            
            if (gvEmpDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvEmpDetails.Rows.Count; i++)
                {
                    if (Convert.ToString(gvEmpDetails.Rows[i].Cells["LOP"].Value) == "")
                    {
                        gvEmpDetails.Rows[i].Cells["LOP"].Value = "0";
                    }
                    if (Convert.ToString(gvEmpDetails.Rows[i].Cells["Paid"].Value) == "")
                    {
                        gvEmpDetails.Rows[i].Cells["Paid"].Value = Convert.ToInt32(gvEmpDetails.Rows[i].Cells["Total"].Value) - Convert.ToInt32(gvEmpDetails.Rows[i].Cells["LOP"].Value);
                    }
                    if (Convert.ToDouble(Convert.ToString(gvEmpDetails.Rows[i].Cells["Paid"].Value)) > Convert.ToDouble(gvEmpDetails.Rows[i].Cells["Total"].Value))
                    {
                        flagSave = false;
                        MessageBox.Show("Paid Days is Exceeded than Month Days For Employee Ecode- " + gvEmpDetails.Rows[i].Cells["Ecode"].Value, "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        gvEmpDetails.ClearSelection();
                        gvEmpDetails.CurrentCell = gvEmpDetails.Rows[i].Cells["Ecode"];
                        return flagSave;
                    }
                }
            }
            return flagSave;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                objSQLdb = new SQLDB();
                int iRes = 0;
                try
                {
                    string strCMD = "";

                    strCMD += " DELETE HR_PAYROLL_MANUAL_ATTD_MTOD WHERE HPOAM_WAGEMONTH='" + txtWagePeriod.Text +
                       "' AND HPOAM_COMPANY_CODE='" + CommonData.CompanyCode + "' AND HPOAM_BRANCH_CODE='" + CommonData.BranchCode + "' AND HPOAM_PAYROLL_TYPE='" + cbPayRollType.SelectedItem.ToString() + "'";
                    int res = objSQLdb.ExecuteSaveData(strCMD);
                    strCMD = "";

                    for (int iVar = 0; iVar < gvEmpDetails.Rows.Count; iVar++)
                    {
                        strCMD += " INSERT INTO HR_PAYROLL_MANUAL_ATTD_MTOD ("+
                                    "HPOAM_COMPANY_CODE, HPOAM_BRANCH_CODE, HPOAM_WAGEMONTH, HPOAM_ECODE"+
                                    ", HPMTM_LOCATION, HPOAM_PRE, HPOAM_ABSX, HPOAM_COMPANY_NAME, HPOAM_BRANCH_NAME"+
                                    ", HPOAM_DEPT_CODE, HPOAM_DEPT_NAME, HPOAM_ENAME, HPOAM_DESIG_ID, HPOAM_DESIGNATION"+
                                    ",HPOAM_DOJ,HPOAM_PAYROLL_TYPE)" +
                                    " SELECT '" + CommonData.CompanyCode + "','" + CommonData.BranchCode + 
                                    "','" + txtWagePeriod.Text + "','" + gvEmpDetails.Rows[iVar].Cells["Ecode"].Value + 
                                    "','BR','" + gvEmpDetails.Rows[iVar].Cells["Paid"].Value + 
                                    "','" + gvEmpDetails.Rows[iVar].Cells["LOP"].Value +
                                    "','" + CommonData.CompanyName + "','" + CommonData.BranchName + 
                                    "','" + gvEmpDetails.Rows[iVar].Cells["deptcode"].Value +
                                    "','" + gvEmpDetails.Rows[iVar].Cells["Dept"].Value + 
                                    "','" + gvEmpDetails.Rows[iVar].Cells["Names"].Value + 
                                    "','" + gvEmpDetails.Rows[iVar].Cells["HpDesigID"].Value +
                                    "','" + gvEmpDetails.Rows[iVar].Cells["Desig"].Value + 
                                    "','" + Convert.ToDateTime(gvEmpDetails.Rows[iVar].Cells["HpDOJ"].Value).ToString("dd/MMM/yyyy") + 
                                    "','" + cbPayRollType.SelectedItem.ToString() +
                                    "' WHERE NOT EXISTS (SELECT HPOAM_ECODE FROM HR_PAYROLL_MANUAL_ATTD_MTOD"+
                                    " WHERE HPOAM_WAGEMONTH='" + txtWagePeriod.Text +
                                    "' AND HPOAM_ECODE='" + gvEmpDetails.Rows[iVar].Cells["Ecode"].Value + "') ";


                        strCMD += " UPDATE HR_PAYROLL_MANUAL_ATTD_MTOD SET HPOAM_PRE='" + gvEmpDetails.Rows[iVar].Cells["Paid"].Value + 
                                    "',HPOAM_ABSX='" + gvEmpDetails.Rows[iVar].Cells["LOP"].Value +
                                    "' WHERE HPOAM_WAGEMONTH='" + txtWagePeriod.Text + 
                                    "' AND HPOAM_ECODE='" + gvEmpDetails.Rows[iVar].Cells["Ecode"].Value +
                                    "' AND HPOAM_BRANCH_CODE ='" + CommonData.BranchCode + 
                                    "' AND HPOAM_PAYROLL_TYPE='" + cbPayRollType.SelectedItem.ToString() +
                                    "' AND EXISTS (SELECT HPOAM_ECODE FROM HR_PAYROLL_MANUAL_ATTD_MTOD"+
                                    " WHERE HPOAM_WAGEMONTH='" + txtWagePeriod.Text +
                                    "' AND HPOAM_ECODE='" + gvEmpDetails.Rows[iVar].Cells["Ecode"].Value + "') ";
                    }
                    iRes = objSQLdb.ExecuteSaveData(strCMD);
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    flagSave = false;
                }
                if (iRes > 0)
                {
                    MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    flagSave = true;
                    //btnCancel_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public DataSet Get_BRMonthlyEmpListForPmd(string sWagePeriod, string sCompanyCode, string sBranchCode, string sPayRoll)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xWagePeriod", DbType.String, sWagePeriod, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xCompanyCode", DbType.String, sCompanyCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xPayRollType", DbType.String, sPayRoll, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("Get_BRMonthlyEmpListForPmd", CommandType.StoredProcedure, param);
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

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = Get_BRMonthlyEmpListForPmd(txtWagePeriod.Text, CommonData.CompanyCode, CommonData.BranchCode, cbPayRollType.SelectedItem.ToString());
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                gvEmpDetails.Rows.Clear();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    gvEmpDetails.Rows.Add();
                    gvEmpDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                    gvEmpDetails.Rows[i].Cells["Ecode"].Value = dt.Rows[i]["Ecode"].ToString();
                    gvEmpDetails.Rows[i].Cells["Names"].Value = dt.Rows[i]["EName"].ToString();
                    gvEmpDetails.Rows[i].Cells["Dept"].Value = dt.Rows[i]["deptname"].ToString();
                    gvEmpDetails.Rows[i].Cells["DeptCode"].Value = dt.Rows[i]["deptcode"].ToString();
                    gvEmpDetails.Rows[i].Cells["Desig"].Value = dt.Rows[i]["design"].ToString();
                    gvEmpDetails.Rows[i].Cells["Total"].Value = dt.Rows[i]["TotDays"].ToString();
                    gvEmpDetails.Rows[i].Cells["Paid"].Value = dt.Rows[i]["PresDays"].ToString();
                    if ((Convert.ToDouble(dt.Rows[i]["PresDays"].ToString()) + Convert.ToDouble(dt.Rows[i]["LopDays"].ToString())) > Convert.ToDouble(dt.Rows[i]["TotDays"].ToString()))
                    {
                        DataGridViewCellStyle style = new DataGridViewCellStyle();
                        style.BackColor = Color.Yellow;
                        style.ForeColor = Color.Black;                        
                        gvEmpDetails.Rows[i].Cells["Paid"].Style = style;
                    }
                    gvEmpDetails.Rows[i].Cells["LOP"].Value = dt.Rows[i]["LopDays"].ToString();
                    gvEmpDetails.Rows[i].Cells["HpComName"].Value = dt.Rows[i]["HpComName"].ToString();
                    gvEmpDetails.Rows[i].Cells["HpBrName"].Value = dt.Rows[i]["HpBrName"].ToString();
                    gvEmpDetails.Rows[i].Cells["HpdeptName"].Value = dt.Rows[i]["deptname"].ToString();
                    gvEmpDetails.Rows[i].Cells["HpDeptCode"].Value = dt.Rows[i]["deptcode"].ToString();
                    gvEmpDetails.Rows[i].Cells["HpEname"].Value = dt.Rows[i]["HpEname"].ToString();
                    gvEmpDetails.Rows[i].Cells["HpDesigID"].Value = dt.Rows[i]["desigid"].ToString();
                    gvEmpDetails.Rows[i].Cells["HpDesg"].Value = dt.Rows[i]["design"].ToString();
                    gvEmpDetails.Rows[i].Cells["HpDOJ"].Value = dt.Rows[i]["empdoj"].ToString();
                }
            }
        }
        private void gvEmpDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (Convert.ToString(gvEmpDetails.Rows[e.RowIndex].Cells["LOP"].Value) != "")
                {
                    if (Convert.ToDouble(gvEmpDetails.Rows[e.RowIndex].Cells["LOP"].Value) < Convert.ToDouble(gvEmpDetails.Rows[e.RowIndex].Cells["Total"].Value))
                    {
                        gvEmpDetails.Rows[e.RowIndex].Cells["Paid"].Value = Convert.ToDouble(gvEmpDetails.Rows[e.RowIndex].Cells["Total"].Value) - Convert.ToDouble(gvEmpDetails.Rows[e.RowIndex].Cells["LOP"].Value);
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Valid LOP", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        gvEmpDetails.Rows[e.RowIndex].Cells["LOP"].Value = "";
                    }
                }
                else
                {
                    gvEmpDetails.Rows[e.RowIndex].Cells["LOP"].Value = "0";
                }
            }
        }

        private void gvEmpDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            CellLOP = true;
            intCurrentRow = (((System.Windows.Forms.DataGridView)(sender))).CurrentCell.RowIndex;
            intCurrentCell = (((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex;
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == gvEmpDetails.Columns["LOP"].Index)
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    txtQty.MaxLength = 6;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
        }
        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46) )
            {
                e.Handled = true;
                return;
            }

            //to allow decimals only teak plant
            
                if (e.KeyChar == 46)
                {
                    if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                        e.Handled = true;
                }
            
            
        }
    }
}
