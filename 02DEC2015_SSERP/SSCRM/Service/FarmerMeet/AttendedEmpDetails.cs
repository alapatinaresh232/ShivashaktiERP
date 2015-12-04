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
    public partial class AttendedEmpDetails : Form
    {
        public FarmerMeetingForm objFarmerMeetingForm = null;
        SQLDB objSQLdb = null;
        string strfrmType = "", strEcode = "";


        public AttendedEmpDetails(string sType,string Ecode)
        {
            InitializeComponent();
            strfrmType = sType;
            strEcode = Ecode;
        }

        private void AttendedEmpDetails_Load(object sender, EventArgs e)
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

        private void AddEmpDetailsToGrid(DataGridView dgvEmpdetails)
        {
            int intRow = 0;
            intRow = dgvEmpdetails.Rows.Count + 1;
            bool isItemExisted = false;

            if (objFarmerMeetingForm.gvAttendedEmpDetails.Rows.Count > 0)
            {
                for (int i = 0; i < objFarmerMeetingForm.gvAttendedEmpDetails.Rows.Count; i++)
                {
                    if (txtEcodeSearch.Text.Equals(objFarmerMeetingForm.gvAttendedEmpDetails.Rows[i].Cells["Ecode"].Value))
                    {
                        isItemExisted = true;
                    }
                }
            }
            if (isItemExisted == false)
            {
                DataGridViewRow tempRow = new DataGridViewRow();


                DataGridViewCell cellSlNo = new DataGridViewTextBoxCell();
                cellSlNo.Value = intRow;
                tempRow.Cells.Add(cellSlNo);

                DataGridViewCell cellEmpEcode = new DataGridViewTextBoxCell();
                cellEmpEcode.Value = Convert.ToInt32(txtEcodeSearch.Text);
                tempRow.Cells.Add(cellEmpEcode);

                DataGridViewCell cellEmpName = new DataGridViewTextBoxCell();
                cellEmpName.Value = txtEName.Text.ToString();
                tempRow.Cells.Add(cellEmpName);

                DataGridViewCell cellEmpDesig = new DataGridViewTextBoxCell();
                cellEmpDesig.Value = txtHRISDesig.Text.ToString();
                tempRow.Cells.Add(cellEmpDesig);

                DataGridViewCell cellEmpDept = new DataGridViewTextBoxCell();
                cellEmpDept.Value = txtDept.Text.ToString();
                tempRow.Cells.Add(cellEmpDept);

                intRow = intRow + 1;
                dgvEmpdetails.Rows.Add(tempRow);
            }
        }


      

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataGridView dgvEmpdetails = null;
            if (txtEcodeSearch.Text != "")
            {
                if (strfrmType == "FarmerMeetingForm")
                {
                    dgvEmpdetails = ((FarmerMeetingForm)objFarmerMeetingForm).gvAttendedEmpDetails;
                    AddEmpDetailsToGrid(dgvEmpdetails);
                    //MessageBox.Show("Emp Details Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCancel_Click(null, null);
                    this.Close();


                }
                else
                {
                    MessageBox.Show("Emp Details Not saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please Enter Ecode", "Employee Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEcodeSearch.Focus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
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


        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text != "")
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

       
    }
}
