using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSAdmin;

namespace SSCRM
{
    public partial class AddEmployeesFromBiometric : Form
    {
        SQLDB objSQLdb = null;
        string strBranCode = "", strFrmDate = "", strToDate = "", strBioMetricSlNo = "";
        public TrainingProgramDetails objTrainingProgramDetails;
        

        public AddEmployeesFromBiometric()
        {
            InitializeComponent();
        }
        public AddEmployeesFromBiometric(string BranCode,string frmDate,string ToDate)
        {
            InitializeComponent();
            strBranCode = BranCode;
            strFrmDate = frmDate;
            strToDate = ToDate;
        }

        private void AddEmployeesFromBiometric_Load(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "SELECT DISTINCT HHP_DEVICE_SLNO DeviceSlNo FROM HR_HO_PUNCHES "+
                             " WHERE HHP_BRANCH_CODE='" + strBranCode + "' and HHP_PUN_DATE BETWEEN '" + strFrmDate + "' and '" + strToDate + "' ";
            dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strBioMetricSlNo = dt.Rows[0]["DeviceSlNo"].ToString();
               
                if (strFrmDate.Length > 5 && strToDate.Length > 5)
                {
                    FillEmployeeDetails(strBioMetricSlNo);
                }
            }
        }

        private DataSet GetEmployeeDetails(string sCompCode,string sBranchCode,string BioSlNo,string sfrmDate,string sToDate,string Type)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xComp_Code", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xBiometric_SlNo", DbType.String, BioSlNo, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xFrom_Date", DbType.String, sfrmDate, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xTo_Date", DbType.String, sToDate, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xType", DbType.String, Type, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("Get_EmployeeListFromBioMetric", CommandType.StoredProcedure, param);
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


        private void FillEmployeeDetails(string strDevSlNo)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            
            try
            {
                dt = GetEmployeeDetails("", strBranCode, strDevSlNo, strFrmDate, strToDate, "").Tables[0];
                
                if(dt.Rows.Count>0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Text = dataRow["DisplayMenber"].ToString();
                        oclBox.Tag = dataRow["ValueMember"].ToString();

                        clbEmployees.Items.Add(oclBox);


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataGridView dgvEmpDetl = null;
            if (clbEmployees.Items.Count > 0)
            {
                if (clbEmployees.CheckedItems.Count > 0)
                {
                    dgvEmpDetl = ((TrainingProgramDetails)objTrainingProgramDetails).gvEmpDetails;
                    AddEmpDetailsToGrid(dgvEmpDetl);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please Select Atleast One Employee","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                
            }
            else
            {
                MessageBox.Show("Please Select Valid Dates and Program Location","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }


        public void AddEmpDetailsToGrid(DataGridView dgvEmpDetl)
        {
            int intRow = 0;
            intRow = dgvEmpDetl.Rows.Count + 1;
            bool IsItemExisted = false;

            try
            {

                for (int i = 0; i < clbEmployees.Items.Count; i++)
                {
                    if (clbEmployees.GetItemCheckState(i) == CheckState.Checked)
                    {
                        IsItemExisted = false;
                        if (((TrainingProgramDetails)objTrainingProgramDetails).gvEmpDetails.Rows.Count > 0)
                        {
                            for (int j = 0; j < ((TrainingProgramDetails)objTrainingProgramDetails).gvEmpDetails.Rows.Count; j++)
                            {
                                if (((NewCheckboxListItem)(clbEmployees.Items[i])).Tag.Split('~')[0].Equals(((TrainingProgramDetails)objTrainingProgramDetails).gvEmpDetails.Rows[j].Cells["Ecode"].Value.ToString()))
                                {
                                    IsItemExisted = true;
                                }


                            }
                        }
                        if (IsItemExisted == false)
                        {
                           
                            DataGridViewRow tempRow = new DataGridViewRow();

                            DataGridViewCell cellSlNo = new DataGridViewTextBoxCell();
                            cellSlNo.Value = intRow;
                            tempRow.Cells.Add(cellSlNo);

                            DataGridViewCell cellEcode = new DataGridViewTextBoxCell();
                            cellEcode.Value = ((NewCheckboxListItem)(clbEmployees.Items[i])).Tag.Split('~')[0].ToString();
                            tempRow.Cells.Add(cellEcode);

                            DataGridViewCell cellCompanyCode = new DataGridViewTextBoxCell();
                            cellCompanyCode.Value = ((NewCheckboxListItem)(clbEmployees.Items[i])).Tag.Split('~')[3].ToString();
                            tempRow.Cells.Add(cellCompanyCode);

                            DataGridViewCell cellBranchCode = new DataGridViewTextBoxCell();
                            cellBranchCode.Value = ((NewCheckboxListItem)(clbEmployees.Items[i])).Tag.Split('~')[5].ToString();
                            tempRow.Cells.Add(cellBranchCode);

                            DataGridViewCell cellEmpName = new DataGridViewTextBoxCell();
                            cellEmpName.Value = ((NewCheckboxListItem)(clbEmployees.Items[i])).Text.ToString();
                            tempRow.Cells.Add(cellEmpName);

                            DataGridViewCell cellDesig = new DataGridViewTextBoxCell();
                            cellDesig.Value = ((NewCheckboxListItem)(clbEmployees.Items[i])).Tag.Split('~')[2].ToString();
                            tempRow.Cells.Add(cellDesig);

                            DataGridViewCell cellDept = new DataGridViewTextBoxCell();
                            cellDept.Value = ((NewCheckboxListItem)(clbEmployees.Items[i])).Tag.Split('~')[7].ToString();
                            tempRow.Cells.Add(cellDept);

                            DataGridViewCell cellCompany = new DataGridViewTextBoxCell();
                            cellCompany.Value = ((NewCheckboxListItem)(clbEmployees.Items[i])).Tag.Split('~')[4].ToString();
                            tempRow.Cells.Add(cellCompany);

                            DataGridViewCell cellBranch = new DataGridViewTextBoxCell();
                            cellBranch.Value = ((NewCheckboxListItem)(clbEmployees.Items[i])).Tag.Split('~')[6].ToString();
                            tempRow.Cells.Add(cellBranch);


                            intRow = intRow + 1;
                            dgvEmpDetl.Rows.Add(tempRow);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void chkAllEmps_CheckedChanged(object sender, EventArgs e)
        {
            if (clbEmployees.Items.Count > 0)
            {

                if (chkAllEmps.Checked == true)
                {
                    for (int i = 0; i < clbEmployees.Items.Count; i++)
                    {
                        clbEmployees.SetItemCheckState(i, CheckState.Checked);

                    }
                }
                else if (chkAllEmps.Checked == false)
                {
                    for (int i = 0; i < clbEmployees.Items.Count; i++)
                    {
                        clbEmployees.SetItemCheckState(i, CheckState.Unchecked);

                    }
                }
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            int index = clbEmployees.FindString(this.txtSearch.Text);
            if (0 <= index)
            {
                clbEmployees.SelectedIndex = index;
            }
            if (this.txtSearch.Text.Trim() == "")
            {
                clbEmployees.SelectedIndex = -1;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            clbEmployees.Items.Clear();
            chkAllEmps.Checked = false;
        }
    }
}
