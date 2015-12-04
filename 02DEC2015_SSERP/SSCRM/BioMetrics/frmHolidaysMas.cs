using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using SSCRMDB;
using SSTrans;

namespace SSCRM
{
    public partial class frmHolidaysMas : Form
    {
        SQLDB objSQLdb = null;
        bool flagUpdate = false;

        public frmHolidaysMas()
        {
            InitializeComponent();
        }

        private void frmHolidaysMas_Load(object sender, EventArgs e)
        {
            cbTrnType.SelectedIndex = 0;
            cbHolidayType.SelectedIndex = 0;
            txtMaxHolidays.Text = "15";
            GenerateHolidayId();
            dtpHolidayDate.Value = DateTime.Today;
            FillCompanyData();
            nmYear.Value = DateTime.Today.Year;
        }

        private void GenerateHolidayId()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = " SELECT ISNULL(MAX(HLHM_HOLIDAY_ID),0)+1 AS HolidayId FROM HR_LOCATION_HOLIDAY_MASTER";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtHolId.Text = dt.Rows[0]["HolidayId"] + "";
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

        private void FillStates()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            cbBranch.DataSource = null;
            try
            {
                strCmd = " SELECT DISTINCT STATE_CODE,sm_state FROM BRANCH_MAS " +
                         " INNER JOIN state_mas ON sm_state_code=STATE_CODE " +
                         " WHERE COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                         "' ORDER BY sm_state ASC";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];


                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";
                    row[1] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cbBranch.DataSource = dt;
                    cbBranch.DisplayMember = "sm_state";
                    cbBranch.ValueMember = "STATE_CODE";
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

        private void FillBranches()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            try
            {
                strCmd = "SELECT DISTINCT BRANCH_CODE,BRANCH_NAME FROM BRANCH_MAS "+
                         "WHERE COMPANY_CODE='"+ cbCompany.SelectedValue.ToString() +
                         "' and ACTIVE='T' AND BRANCH_TYPE IN ('BR','HO') ORDER BY BRANCH_NAME ASC";

                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];


                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";
                    row[1] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cbBranch.DataSource = dt;
                    cbBranch.DisplayMember = "BRANCH_NAME";
                    cbBranch.ValueMember = "BRANCH_CODE";
                   
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


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void cbTrnType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                if (cbTrnType.SelectedIndex == 0)
                {
                    FillStates();
                }
                else if (cbTrnType.SelectedIndex == 1)
                {
                    FillBranches();
                }

                if (cbBranch.SelectedIndex > 0)
                    FillHolidaysToGrid(cbCompany.SelectedValue.ToString(),cbBranch.SelectedValue.ToString(),cbHolidayType.Text);
            }


        }

      
        private DataSet GetHolidaysDetails(string CompCode, string BranCode, string HolidayType,Int32 Year)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, CompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, BranCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sHolidayType", DbType.String, HolidayType, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sYear", DbType.String, Year, ParameterDirection.Input);                
                ds = objSQLdb.ExecuteDataSet("Get_BranchHolidaysList", CommandType.StoredProcedure, param);
                param = null;
                objSQLdb = null;
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
            if (cbCompany.SelectedIndex > 0)
            {
                if (cbTrnType.SelectedIndex == 0)
                {
                    FillStates();
                    FillHolidaysToGrid(cbCompany.SelectedValue.ToString(),cbBranch.SelectedValue.ToString(),cbHolidayType.Text.ToString());
                }
                else if (cbTrnType.SelectedIndex == 1)
                {
                    FillBranches();
                    FillHolidaysToGrid(cbCompany.SelectedValue.ToString(), cbBranch.SelectedValue.ToString(), cbHolidayType.Text.ToString());
                }
            }
        }

        private void cbHolidayType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbHolidayType.SelectedIndex == 1)
            {
                txtHolName.ReadOnly = true;
                txtHolName.Text = "WEEK OFF";
                if(cbCompany.SelectedIndex>0 && cbBranch.SelectedIndex>0)
                FillHolidaysToGrid(cbCompany.SelectedValue.ToString(), cbBranch.SelectedValue.ToString(), cbHolidayType.Text.ToString());
            }
            else
            {
                txtHolName.ReadOnly = false;
                txtHolName.Text = "";
                if(cbCompany.SelectedIndex>0 && cbBranch.SelectedIndex>0)
                FillHolidaysToGrid(cbCompany.SelectedValue.ToString(), cbBranch.SelectedValue.ToString(), cbHolidayType.Text.ToString());
            }
        }

        private bool CheckData()
        {
            bool flag = true;

            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
           
            if (cbCompany.SelectedIndex <= 0)
            {
                flag = false;
                MessageBox.Show("Please Select Company", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbCompany.Focus();
                return flag;
            }
            if (cbBranch.SelectedIndex <= 0)
            {
                flag = false;
                MessageBox.Show("Please Select Company", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbCompany.Focus();
                return flag;
            }
            if (dtpHolidayDate.Value.Year > nmYear.Value)
            {
                flag = false;
                MessageBox.Show("Please Select Valid Holiday Date", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpHolidayDate.Focus();
                return flag;
            }

            if (txtHolName.Text.Length <= 3)
            {
                flag = false;
                MessageBox.Show("Please Enter Holiday Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHolName.Focus();
                return flag;            
            }
            if (flagUpdate == false)
            {
                strCmd = " SELECT HLHM_HOLIDAY_NAME from HR_LOCATION_HOLIDAY_MASTER " +
                         " WHERE HLHM_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                         "' and HLHM_BRANCH_CODE='" + cbBranch.SelectedValue.ToString() +
                         "' and HLHM_HOLIDAY_DATE='" + Convert.ToDateTime(dtpHolidayDate.Value).ToString("dd/MMM/yyyy") + "'";

                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    flag = false;
                    MessageBox.Show("Already Holiday Name Exist on this Date", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return flag;
                }
            }

            if (gvHolidaysDetails.Rows.Count > 15)
            {
                flag = false;
                MessageBox.Show("Year Holidays Count Don't Exceed Max Holidays \n Check Once Holidays List", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;
            }

            return flag;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            string strCmd = "";
            int iRes = 0;

            if (CheckData() == true)
            {
                try
                {
                    if (flagUpdate == true)
                    {
                        strCmd = "UPDATE HR_LOCATION_HOLIDAY_MASTER set HLHM_TRN_TYPE='" + cbTrnType.Text.ToString() +
                                  "',HLHM_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                  "',HLHM_BRANCH_CODE='" + cbBranch.SelectedValue.ToString() +
                                  "',HLHM_HOLIDAY_DATE='" + Convert.ToDateTime(dtpHolidayDate.Value).ToString("dd/MMM/yyyy") +
                                  "',HLHM_HOLIDAY_NAME='" + txtHolName.Text.ToString().Replace("'", " ") +
                                  "',HLHM_HOLIDAY_DESCRIPTION='" + txtHolDesc.Text.ToString().Replace("'", " ") +
                                  "',HLHM_HOLIDAY_TYPE='" + cbHolidayType.Text.ToString() +
                                  "',HLHM_MAX_HOLIDAYS=" + Convert.ToInt32(txtMaxHolidays.Text) +
                                  ", HLHM_MODIFIED_BY='" + CommonData.LogUserId +
                                  "',HLHM_MODIFIED_DATE=getdate() WHERE HLHM_HOLIDAY_ID=" + Convert.ToInt32(txtHolId.Text) + "";
                    }
                    else
                    {
                        GenerateHolidayId();

                        strCmd = "INSERT INTO HR_LOCATION_HOLIDAY_MASTER(HLHM_TRN_TYPE " +
                                                                       ", HLHM_COMPANY_CODE " +
                                                                       ", HLHM_BRANCH_CODE " +
                                                                       ", HLHM_HOLIDAY_DATE " +
                                                                       ", HLHM_HOLIDAY_NAME " +
                                                                       ", HLHM_HOLIDAY_DESCRIPTION " +
                                                                       ", HLHM_HOLIDAY_TYPE " +
                                                                       ", HLHM_MAX_HOLIDAYS " +
                                                                       ", HLHM_CREATED_BY " +
                                                                       ", HLHM_CREATED_DATE " +
                                                                       ")VALUES " +
                                                                       "('" + cbTrnType.Text.ToString() +
                                                                       "','" + cbCompany.SelectedValue.ToString() +
                                                                       "','" + cbBranch.SelectedValue.ToString() +
                                                                       "','" + Convert.ToDateTime(dtpHolidayDate.Value).ToString("dd/MMM/yyyy") +
                                                                       "','" + txtHolName.Text.ToString().Replace("'", " ") +
                                                                       "','" + txtHolDesc.Text.ToString().Replace("'", " ") +
                                                                       "','" + cbHolidayType.Text.ToString() +
                                                                       "'," + Convert.ToInt32(txtMaxHolidays.Text) +
                                                                       ",'" + CommonData.LogUserId +
                                                                       "',getdate())";

                    }

                    if (strCmd.Length > 5)
                    {
                        objSQLdb = new SQLDB();
                        iRes = objSQLdb.ExecuteSaveData(strCmd);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                if (iRes > 0)
                {
                    MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillHolidaysToGrid(cbCompany.SelectedValue.ToString(), cbBranch.SelectedValue.ToString(), cbHolidayType.Text);
                    flagUpdate = false;
                    if(cbHolidayType.SelectedIndex==0)
                        txtHolName.Text = "";
                    txtHolDesc.Text = "";
//                    cbHolidayType.SelectedIndex = 0;
                    GenerateHolidayId();
                    dtpHolidayDate.Focus();
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FillHolidaysToGrid(string sCompCode,string sBranCode,string sHolidayType)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();           
            gvHolidaysDetails.Rows.Clear();
            if (cbCompany.SelectedIndex > 0 && cbBranch.SelectedIndex > 0)
            {
                try
                {
                    dt = GetHolidaysDetails(sCompCode, sBranCode, sHolidayType,Convert.ToInt32(nmYear.Value)).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        txtMaxHolidays.Text = dt.Rows[0]["MaxHolidays"].ToString();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            gvHolidaysDetails.Rows.Add();
                            gvHolidaysDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                            gvHolidaysDetails.Rows[i].Cells["HolidayId"].Value = dt.Rows[i]["HolidayId"].ToString();
                            gvHolidaysDetails.Rows[i].Cells["StateCode"].Value = dt.Rows[i]["BranCode"].ToString();
                            gvHolidaysDetails.Rows[i].Cells["CompCode"].Value = dt.Rows[i]["CompCode"].ToString();
                            gvHolidaysDetails.Rows[i].Cells["TrnType"].Value = dt.Rows[i]["TrnType"].ToString();
                            gvHolidaysDetails.Rows[i].Cells["HolidayDate"].Value = Convert.ToDateTime(dt.Rows[i]["HolidayDate"].ToString()).ToString("dd/MMM/yyyy");
                            gvHolidaysDetails.Rows[i].Cells["HolidayType"].Value = dt.Rows[i]["HolidayType"].ToString();
                            gvHolidaysDetails.Rows[i].Cells["HolidayName"].Value = dt.Rows[i]["HolidayName"].ToString();
                            gvHolidaysDetails.Rows[i].Cells["HolidayDesc"].Value = dt.Rows[i]["HolidayDesc"].ToString();
                                                      
                        }
                    }

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
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtMaxHolidays.Text = "15";
            txtHolName.Text = "";
            txtHolDesc.Text = "";
            dtpHolidayDate.Value = DateTime.Today;
            cbTrnType.SelectedIndex = 0;
            //cbBranch.SelectedIndex = 0;
            cbHolidayType.SelectedIndex = 0;
            gvHolidaysDetails.Rows.Clear();
            GenerateHolidayId();
            flagUpdate = false;
            //cbCompany.SelectedIndex = 0;
        }

        private void cbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0 && cbBranch.SelectedIndex > 0)
            {
                FillHolidaysToGrid(cbCompany.SelectedValue.ToString(), cbBranch.SelectedValue.ToString(), cbHolidayType.Text.ToString());
            }
        }

        private void gvHolidaysDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 iResult = 0;
            if (e.ColumnIndex == gvHolidaysDetails.Columns["Edit"].Index)
            {
                flagUpdate = true;
                txtHolId.Text = gvHolidaysDetails.Rows[e.RowIndex].Cells["HolidayId"].Value.ToString();
                txtHolName.Text = gvHolidaysDetails.Rows[e.RowIndex].Cells["HolidayName"].Value.ToString();
                txtHolDesc.Text = gvHolidaysDetails.Rows[e.RowIndex].Cells["HolidayDesc"].Value.ToString();
                dtpHolidayDate.Value = Convert.ToDateTime(gvHolidaysDetails.Rows[e.RowIndex].Cells["HolidayDate"].Value.ToString());
            }

            if (e.ColumnIndex == gvHolidaysDetails.Columns["Delete"].Index)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want Delete this Record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    objSQLdb = new SQLDB();
                    try
                    {
                        string strCommand = "DELETE FROM HR_LOCATION_HOLIDAY_MASTER "+
                                            " WHERE HLHM_HOLIDAY_ID =" + gvHolidaysDetails.Rows[e.RowIndex].Cells["HolidayId"].Value + " ";
                        iResult = objSQLdb.ExecuteSaveData(strCommand);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        objSQLdb = null;
                    }

                    if (iResult > 0)
                    {                       
                        MessageBox.Show("Data Deleted Successfully", "Holidays Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GenerateHolidayId();
                        flagUpdate = false;
                        FillHolidaysToGrid(cbCompany.SelectedValue.ToString(),cbBranch.SelectedValue.ToString(),cbHolidayType.Text);
                        dtpHolidayDate.Focus();
                        
                    }
                }
                else
                {
                    MessageBox.Show("Data not Deleted", "Holidays Master", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void nmYear_ValueChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0 && cbBranch.SelectedIndex > 0)
            {
                FillHolidaysToGrid(cbCompany.SelectedValue.ToString(), cbBranch.SelectedValue.ToString(), cbHolidayType.Text.ToString());
            }
        }
    }
}
