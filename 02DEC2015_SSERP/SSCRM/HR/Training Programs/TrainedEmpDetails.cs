using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSAdmin;

namespace SSCRM
{
    public partial class TrainedEmpDetails : Form
    {
        SQLDB objSQLdb = null;
        public  TrainingProgramDetails objTrainingProgramDetails;

        public TrainedEmpDetails()
        {
            InitializeComponent();
        }

        private void TrainedEmpDetails_Load(object sender, EventArgs e)
        {
            FillCompanyData();
        }

        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbBranches.DataSource = null;
            try
            {
                string strCmd = "SELECT CM_COMPANY_CODE,CM_COMPANY_NAME FROM COMPANY_MAS " +
                                " WHERE ACTIVE='T' ORDER BY CM_COMPANY_NAME";
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
        private void FillBranches()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbBranches.DataSource = null;
            try
            {
                if (cbCompany.SelectedIndex > 0)
                {

                    string strCommand = "SELECT BRANCH_NAME ,BRANCH_CODE  FROM BRANCH_MAS " +
                                        " WHERE COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                        "'AND BRANCH_TYPE IN('BR','HO')  Order by BRANCH_NAME ";
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

        private void FillDepartmentData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbDept.DataSource = null;

            try
            {
                if (cbBranches.SelectedIndex > 0)
                {
                    string strCmd = "SELECT DISTINCT(DEPT_ID),dept_name FROM EORA_MASTER " +
                                    "INNER JOIN Dept_Mas ON dept_code=DEPT_ID " +
                                    " WHERE company_code='" + cbCompany.SelectedValue.ToString() +
                                    "'AND BRANCH_CODE='" + cbBranches.SelectedValue.ToString() +
                                    "'ORDER BY dept_name";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "0";
                    row[1] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cbDept.DataSource = dt;
                    cbDept.DisplayMember = "dept_name";
                    cbDept.ValueMember = "DEPT_ID";
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

        private void FillDesignationsData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbDesig.DataSource = null;
            try
            {
                if (cbDept.SelectedIndex > 0)
                {
                    string strCmd = "SELECT DISTINCT(DESG_ID),desig_name FROM EORA_MASTER " +
                                    "INNER JOIN DESIG_MAS ON desig_code=DESG_ID  " +
                                    " WHERE DEPT_ID=" + Convert.ToInt32(cbDept.SelectedValue) +
                                    " AND company_code='" + cbCompany.SelectedValue.ToString() +
                                    "' AND BRANCH_CODE='" + cbBranches.SelectedValue.ToString() +
                                    "' ORDER BY desig_name";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "0";
                    row[1] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cbDesig.DataSource = dt;
                    cbDesig.DisplayMember = "desig_name";
                    cbDesig.ValueMember = "DESG_ID";

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

        private void FillEmployeeData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
            clbEmployees.Items.Clear();

            try
            {
                if (cbDesig.SelectedIndex > 0)
                {
                    strCommand = "SELECT  ECODE,CAST(ECODE as varchar)+'-'+MEMBER_NAME EmpName " +
                                " FROM EORA_MASTER " +
                                " INNER JOIN HR_APPL_MASTER_HEAD ON HAMH_EORA_CODE=ECODE " +
                                " WHERE company_code='" + cbCompany.SelectedValue.ToString() +
                                "'AND BRANCH_CODE='" + cbBranches.SelectedValue.ToString() +
                                "'AND DEPT_ID=" + Convert.ToInt32(cbDept.SelectedValue.ToString()) +
                                " AND DESG_ID=" + Convert.ToInt32(cbDesig.SelectedValue) +
                                " AND HAMH_WORKING_STATUS IN('W','E')";

                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Text = dataRow["EmpName"].ToString();
                        oclBox.Tag = dataRow["ECODE"].ToString();

                        clbEmployees.Items.Add(oclBox);


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
                dt = null;
            }
        }

        private bool CheckData()
        {
            bool flag = true;

            if (cbCompany.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Company", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }
            else if (cbBranches.SelectedIndex == 0 || cbBranches.SelectedIndex == -1)
            {
                flag = false;
                MessageBox.Show("Please Select Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }
            else if (cbDept.SelectedIndex == 0 || cbDept.SelectedIndex == -1)
            {
                flag = false;
                MessageBox.Show("Please Select Department", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }
            else if (cbDesig.SelectedIndex == 0 || cbDesig.SelectedIndex == -1)
            {
                flag = false;
                MessageBox.Show("Please Select Designation", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }
            else if (clbEmployees.CheckedItems.Count == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Atleast One Employee", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }

            return flag;

        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            DataGridView dgvEmpDetl = null;
            if (CheckData())
            {
                dgvEmpDetl = ((TrainingProgramDetails)objTrainingProgramDetails).gvEmpDetails;
                AddEmpDetailsToGrid(dgvEmpDetl);
                
                this.Close();               
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
                                if (((NewCheckboxListItem)(clbEmployees.Items[i])).Tag.Equals(((TrainingProgramDetails)objTrainingProgramDetails).gvEmpDetails.Rows[j].Cells["Ecode"].Value.ToString()))
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
                            cellEcode.Value = ((NewCheckboxListItem)(clbEmployees.Items[i])).Tag.ToString();
                            tempRow.Cells.Add(cellEcode);

                            DataGridViewCell cellCompanyCode = new DataGridViewTextBoxCell();
                            cellCompanyCode.Value = cbCompany.SelectedValue.ToString();
                            tempRow.Cells.Add(cellCompanyCode);

                            DataGridViewCell cellBranchCode = new DataGridViewTextBoxCell();
                            cellBranchCode.Value = cbBranches.SelectedValue.ToString();
                            tempRow.Cells.Add(cellBranchCode);

                            DataGridViewCell cellEmpName = new DataGridViewTextBoxCell();
                            cellEmpName.Value = ((NewCheckboxListItem)(clbEmployees.Items[i])).Text.ToString();
                            tempRow.Cells.Add(cellEmpName);

                            DataGridViewCell cellDesig = new DataGridViewTextBoxCell();
                            cellDesig.Value = cbDesig.Text.ToString();
                            tempRow.Cells.Add(cellDesig);

                            DataGridViewCell cellDept = new DataGridViewTextBoxCell();
                            cellDept.Value = cbDept.Text.ToString();
                            tempRow.Cells.Add(cellDept);

                            DataGridViewCell cellCompany = new DataGridViewTextBoxCell();
                            cellCompany.Value = cbCompany.Text.ToString();
                            tempRow.Cells.Add(cellCompany);

                            DataGridViewCell cellBranch = new DataGridViewTextBoxCell();
                            cellBranch.Value = cbBranches.Text.ToString();
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


        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                FillBranches();
            }
            else
            {
                cbBranches.DataSource = null;
                cbDept.DataSource = null;
                cbDesig.DataSource = null;
            }
        }

        private void cbBranches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBranches.SelectedIndex > 0)
            {
                FillDepartmentData();
            }
            else
            {
                cbDept.DataSource = null;
                cbDesig.DataSource = null;
            }
        }

        private void cbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDept.SelectedIndex > 0)
            {
                FillDesignationsData();
            }
            else
            {
                cbDesig.DataSource = null;
            }

        }

        private void cbDesig_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDesig.SelectedIndex > 0)
            {
                FillEmployeeData();
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
            cbCompany.SelectedIndex = 0;
            cbBranches.SelectedIndex = -1;
            cbDept.SelectedIndex = -1;
            cbDesig.SelectedIndex = -1;

            chkAllEmps.Checked = false;

            for (int i = 0; i < clbEmployees.Items.Count; i++)
            {
                clbEmployees.SetItemCheckState(i, CheckState.Unchecked);

            }

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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

    }
}
