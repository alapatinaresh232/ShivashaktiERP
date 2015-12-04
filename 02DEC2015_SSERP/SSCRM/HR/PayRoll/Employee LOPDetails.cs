using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSCRM;
using SSTrans;

namespace SSCRM
{
    public partial class EmployeeLOPDetails : Form
    {
        SQLDB objSQLdb = null;
        HRInfo objHRdb = null;

        bool flagUpdate = false;

        DateTime selectedMonth;
        DateTime FirstDayOfMonth;
        DateTime LastDayOfMonth;
        public EmployeeLOPDetails()
        {
            InitializeComponent();
        }

        private void EmployeeLOPDetails_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            FillDepartmentData();


            FirstDayOfMonth = new DateTime(dtpMonth.Value.Year, dtpMonth.Value.Month, 01);
            LastDayOfMonth = new DateTime(dtpMonth.Value.Year, dtpMonth.Value.Month, DateTime.DaysInMonth(dtpMonth.Value.Year, dtpMonth.Value.Month));

            dtpFrmDate.Value = FirstDayOfMonth;
            dtpToDate.Value = LastDayOfMonth;

           

            //Int32 daysInMonth = DateTime.DaysInMonth(dtpMonth.Value.Year, dtpMonth.Value.Month);
            txtTotDays.Text = Convert.ToString(DateTime.DaysInMonth(dtpMonth.Value.Year, dtpMonth.Value.Month));

            gvEmpLopDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                       System.Drawing.FontStyle.Regular);


            //if (CommonData.LogUserId != "admin")
            //{
            //    dtpFrmDate.Enabled = true;
            //    dtpToDate.Enabled = true;
            //}
        }

        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbBranches.DataSource = null;
            try
            {
                string strCmd = "SELECT CM_COMPANY_CODE,CM_COMPANY_NAME FROM COMPANY_MAS WHERE ACTIVE='T' ORDER BY CM_COMPANY_NAME";
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
            try
            {
                string strCmd = "SELECT dept_code,dept_desc FROM Dept_Mas";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "0";
                    row[1] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cbDept.DataSource = dt;
                    cbDept.DisplayMember = "dept_desc";
                    cbDept.ValueMember = "dept_code";
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

        private void FillEmpDetails(string sMonth)
        {
            objSQLdb = new SQLDB();
            objHRdb = new HRInfo();
            DataTable dt = new DataTable();
            gvEmpLopDetails.Rows.Clear();
            char cStatus = 'P';
            string strCommand = "";
            string CompCode = cbCompany.SelectedValue.ToString();

            if (cbDept.SelectedIndex > 0)
            {
                string BranCode = cbBranches.SelectedValue.ToString();
                try
                {
                    strCommand = "SELECT * FROM HR_EMP_LOPS WHERE HEL_COMPANY_CODE='" + CompCode +
                                 "' AND HEL_BRANCH_CODE='" + BranCode +
                                 "' AND HEL_DEPT_CODE=" + Convert.ToInt32(cbDept.SelectedValue) +
                                 "  AND HEL_DOC_MONTH='" + Convert.ToDateTime(dtpMonth.Value).ToString("MMM/yyyy") +"'";
                    //"' AND HEL_APPR_STATUS='p' ";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        strCommand = "";
                        dt = null;

                        dt = objHRdb.EmployeeLOPDetails_Get(CompCode, BranCode, Convert.ToInt32(cbDept.SelectedValue), sMonth).Tables[0];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            flagUpdate = true;
                            dtpFrmDate.Value = Convert.ToDateTime(dt.Rows[0]["FromDate"].ToString());
                            dtpToDate.Value = Convert.ToDateTime(dt.Rows[0]["ToDate"].ToString());

                            gvEmpLopDetails.Rows.Add();
                            gvEmpLopDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                            gvEmpLopDetails.Rows[i].Cells["ApplNo"].Value = dt.Rows[i]["ApplNo"].ToString();
                            gvEmpLopDetails.Rows[i].Cells["TotLeaves"].Value = dt.Rows[i]["EmpLeaves"].ToString();
                            gvEmpLopDetails.Rows[i].Cells["Ecode"].Value = dt.Rows[i]["Ecode"].ToString();
                            gvEmpLopDetails.Rows[i].Cells["EmpName"].Value = dt.Rows[i]["EmpName"].ToString();
                            gvEmpLopDetails.Rows[i].Cells["Desig"].Value = dt.Rows[i]["EmpDesig"].ToString();
                            gvEmpLopDetails.Rows[i].Cells["TotDays"].Value = dt.Rows[i]["WorkingDays"].ToString();
                            gvEmpLopDetails.Rows[i].Cells["Lops"].Value = dt.Rows[i]["EmpLops"].ToString();
                            gvEmpLopDetails.Rows[i].Cells["EmpCLs"].Value = dt.Rows[i]["EmpCLs"].ToString();
                            gvEmpLopDetails.Rows[i].Cells["EmpSLs"].Value = dt.Rows[i]["EmpSLs"].ToString();
                            gvEmpLopDetails.Rows[i].Cells["EmpELs"].Value = dt.Rows[i]["EmpELs"].ToString();
                            gvEmpLopDetails.Rows[i].Cells["WorkedDays"].Value = dt.Rows[i]["WorkedDays"].ToString();
                            double Workingdays = Convert.ToDouble(dt.Rows[i]["WorkingDays"] + "");
                            double Lops = Convert.ToDouble(dt.Rows[i]["EmpLops"].ToString());
                            double paidDays = Workingdays - Lops;
                            gvEmpLopDetails.Rows[i].Cells["PaidDays"].Value = Convert.ToString(paidDays);
                        }
                    }
                    else
                    {
                        strCommand = "";
                        dt = null;

                        strCommand = "SELECT ECODE,HRIS_EMP_NAME,HAMH_APPL_NUMBER,desig_desc " +
                                      " FROM EORA_MASTER " +
                                      " INNER JOIN HR_APPL_MASTER_HEAD ON HAMH_EORA_CODE=ECODE " +
                                      " INNER JOIN DESIG_MAS ON desig_code=DESG_ID " +
                                      " WHERE DEPT_ID=" + Convert.ToInt32(cbDept.SelectedValue) +
                                      " AND company_code='" + cbCompany.SelectedValue.ToString() +
                                      "' AND BRANCH_CODE='" + cbBranches.SelectedValue.ToString() + "'";

                        dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                gvEmpLopDetails.Rows.Add();
                                gvEmpLopDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                                gvEmpLopDetails.Rows[i].Cells["ApplNo"].Value = dt.Rows[i]["HAMH_APPL_NUMBER"].ToString();
                                gvEmpLopDetails.Rows[i].Cells["TotLeaves"].Value = 0;
                                gvEmpLopDetails.Rows[i].Cells["Ecode"].Value = dt.Rows[i]["ECODE"].ToString();
                                gvEmpLopDetails.Rows[i].Cells["EmpName"].Value = dt.Rows[i]["HRIS_EMP_NAME"].ToString();
                                gvEmpLopDetails.Rows[i].Cells["Desig"].Value = dt.Rows[i]["desig_desc"].ToString();
                                gvEmpLopDetails.Rows[i].Cells["TotDays"].Value = txtTotDays.Text;
                            }

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
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                FillBranches();
                
            }
            gvEmpLopDetails.Rows.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void dtpMonth_ValueChanged(object sender, EventArgs e)
        {
            selectedMonth = dtpMonth.Value;
            FirstDayOfMonth = new DateTime(selectedMonth.Year, selectedMonth.Month, 01);
            LastDayOfMonth = new DateTime(selectedMonth.Year, selectedMonth.Month, DateTime.DaysInMonth(dtpMonth.Value.Year, dtpMonth.Value.Month));
           
            dtpFrmDate.Value = FirstDayOfMonth;
            dtpToDate.Value = LastDayOfMonth;

         

            Int32 daysInMonth = DateTime.DaysInMonth(dtpMonth.Value.Year, dtpMonth.Value.Month);
            txtTotDays.Text = Convert.ToString(daysInMonth);

            FillEmpDetails(Convert.ToDateTime(dtpMonth.Value).ToString("MMM/yyyy"));

           
        }

        private void cbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDept.SelectedIndex > 0)
            {
                FillEmpDetails(Convert.ToDateTime(dtpMonth.Value).ToString("MMM/yyyy"));
            }
        }

        private bool CheckData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            bool flag = true;

            string strCommand = "SELECT * FROM HR_EMP_LOPS WHERE HEL_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                 "' AND HEL_BRANCH_CODE='" + cbBranches.SelectedValue.ToString() +
                                 "' AND HEL_DEPT_CODE=" + Convert.ToInt32(cbDept.SelectedValue) +
                                 "  AND HEL_DOC_MONTH='" + Convert.ToDateTime(dtpMonth.Value).ToString("MMM/yyyy") +
                                 "' AND HEL_APPR_STATUS='A' ";
            dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

            if (dt.Rows.Count > 0)
            {
                flag = false;
                MessageBox.Show("This Data Has Been Approved! Can Not Be Manipulated", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;

            }
            else if (cbCompany.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Company", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCompany.Focus();
            }
            else if (cbBranches.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbBranches.Focus();
            }
            else if (cbDept.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Department", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbDept.Focus();
            }
            else if (dtpMonth.Value > DateTime.Today.AddDays(1.0))
            {
                flag = false;
                MessageBox.Show("Please Select Valid Month", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpMonth.Focus();
            }
           

            else if (gvEmpLopDetails.Rows.Count > 0)
            {

                for (int i = 0; i < gvEmpLopDetails.Rows.Count; i++)
                {
                    if (Convert.ToString(gvEmpLopDetails.Rows[i].Cells["Lops"].Value) == "")
                    {

                        gvEmpLopDetails.Rows[i].Cells["Lops"].Value = "0";
                    }
                    if (Convert.ToString(gvEmpLopDetails.Rows[i].Cells["EmpELs"].Value) == "")
                    {
                        gvEmpLopDetails.Rows[i].Cells["EmpELs"].Value = "0";
                    }
                    if (Convert.ToString(gvEmpLopDetails.Rows[i].Cells["EmpSLs"].Value) == "")
                    {
                        gvEmpLopDetails.Rows[i].Cells["EmpSLs"].Value = "0";
                    }
                    if (Convert.ToString(gvEmpLopDetails.Rows[i].Cells["EmpCLs"].Value) == "")
                    {
                        gvEmpLopDetails.Rows[i].Cells["EmpCLs"].Value = "0";
                    }
                    if (Convert.ToString(gvEmpLopDetails.Rows[i].Cells["WorkedDays"].Value) == "")
                    {
                        gvEmpLopDetails.Rows[i].Cells["WorkedDays"].Value = Convert.ToInt32(txtTotDays.Text);
                    }
                    if (gvEmpLopDetails.Rows[i].Cells["TotLeaves"].Value.ToString() == "")
                    {
                        gvEmpLopDetails.Rows[i].Cells["TotLeaves"].Value = "0";
                    }
                    if (Convert.ToString(gvEmpLopDetails.Rows[i].Cells["PaidDays"].Value) == "")
                    {
                        gvEmpLopDetails.Rows[i].Cells["PaidDays"].Value = Convert.ToInt32(gvEmpLopDetails.Rows[i].Cells["TotDays"].Value) - Convert.ToInt32(gvEmpLopDetails.Rows[i].Cells["Lops"].Value);
                    }
                }
            }
            return flag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            int iResult = 0;
            string strCmd = "";
            

            if (CheckData())
            {
                if (gvEmpLopDetails.Rows.Count > 0)
                {
                    try
                    {
                        

                            //for (int i = 0; i < gvEmpLopDetails.Rows.Count; i++)
                            //{

                            //    strCmd += "DELETE FROM HR_EMP_LOPS WHERE HEL_APPL_NO=" + gvEmpLopDetails.Rows[i].Cells["ApplNo"].Value.ToString() +
                            //             " AND HEL_DOC_MONTH='" + Convert.ToDateTime(dtpMonth.Value).ToString("MMM/yyyy") + "'";
                            //}
                            //iResult = objSQLdb.ExecuteSaveData(strCmd);
                            //strCmd = "";
                        if (flagUpdate == true)
                        {
                            for (int ivar = 0; ivar < gvEmpLopDetails.Rows.Count; ivar++)
                            {
                                strCmd += "UPDATE HR_EMP_LOPS SET HEL_EORA_CODE=" + Convert.ToInt32(gvEmpLopDetails.Rows[ivar].Cells["Ecode"].Value) +
                                        ", HEL_YEAR=" + Convert.ToDateTime(dtpMonth.Value).ToString("yyyy") +
                                        ",HEL_FROM='" + Convert.ToDateTime(dtpFrmDate.Value).ToString("dd/MMM/yyyy") +
                                        "',HEL_TO='" + Convert.ToDateTime(dtpToDate.Value).ToString("dd/MMM/yyyy") +
                                        "',HEL_TOT_WORKING_DAYS=" + txtTotDays.Text +
                                        ", HEL_LOPS=" + gvEmpLopDetails.Rows[ivar].Cells["Lops"].Value +
                                        ",HEL_PRESENT_DAYS=" + gvEmpLopDetails.Rows[ivar].Cells["WorkedDays"].Value +
                                        ",HEL_ABSNT_DAYS=" + gvEmpLopDetails.Rows[ivar].Cells["TotLeaves"].Value +
                                        ",HEL_CLS=" + gvEmpLopDetails.Rows[ivar].Cells["EmpCLs"].Value +
                                        ",HEL_SLS=" + gvEmpLopDetails.Rows[ivar].Cells["EmpSLs"].Value +
                                        ",HEL_ELS=" + gvEmpLopDetails.Rows[ivar].Cells["EmpELs"].Value +
                                        ",HEL_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                        "',HEL_BRANCH_CODE='" + cbBranches.SelectedValue.ToString() +
                                        "',HEL_DEPT_CODE=" + Convert.ToInt32(cbDept.SelectedValue) +
                                        ",HEL_APPR_STATUS='P',HEL_MODIFIED_BY='" + CommonData.LogUserId +
                                        "',HEL_MODIFIED_DATE='" + CommonData.CurrentDate +
                                        "' WHERE HEL_APPL_NO=" + gvEmpLopDetails.Rows[ivar].Cells["ApplNo"].Value.ToString() +
                                        " AND HEL_DOC_MONTH='" + Convert.ToDateTime(dtpMonth.Value).ToString("MMM/yyyy").ToUpper() + "'";
                            }
                            flagUpdate = false;
                        }
                        else
                        {

                            for (int ivar = 0; ivar < gvEmpLopDetails.Rows.Count; ivar++)
                            {
                                strCmd += "INSERT INTO HR_EMP_LOPS(HEL_APPL_NO " +
                                                               ", HEL_EORA_CODE " +
                                                               ", HEL_YEAR " +
                                                               ", HEL_DOC_MONTH " +
                                                               ", HEL_FROM " +
                                                               ", HEL_TO " +
                                                               ", HEL_TOT_WORKING_DAYS " +
                                                               ", HEL_LOPS " +
                                                               ", HEL_PRESENT_DAYS " +
                                                               ", HEL_ABSNT_DAYS " +
                                                               ", HEL_CLS " +
                                                               ", HEL_SLS " +
                                                               ", HEL_ELS " +
                                                               ", HEL_COMPANY_CODE " +
                                                               ", HEL_BRANCH_CODE " +
                                                               ", HEL_DEPT_CODE " +
                                                               ", HEL_APPR_STATUS " +
                                                               ", HEL_CREATED_BY " +
                                                               ", HEL_CREATED_DATE " +
                                                               ")VALUES " +
                                                               "(" + gvEmpLopDetails.Rows[ivar].Cells["ApplNo"].Value.ToString() +
                                                               "," + Convert.ToInt32(gvEmpLopDetails.Rows[ivar].Cells["Ecode"].Value) +
                                                               "," + Convert.ToDateTime(dtpMonth.Value).ToString("yyyy") +
                                                               ",'" + Convert.ToDateTime(dtpMonth.Value).ToString("MMM/yyyy").ToUpper() +
                                                               "','" + Convert.ToDateTime(dtpFrmDate.Value).ToString("dd/MMM/yyyy") +
                                                               "','" + Convert.ToDateTime(dtpToDate.Value).ToString("dd/MMM/yyyy") +
                                                               "'," + txtTotDays.Text +
                                                               "," + gvEmpLopDetails.Rows[ivar].Cells["Lops"].Value +
                                                               "," + gvEmpLopDetails.Rows[ivar].Cells["WorkedDays"].Value +
                                                               "," + gvEmpLopDetails.Rows[ivar].Cells["TotLeaves"].Value +
                                                               "," + gvEmpLopDetails.Rows[ivar].Cells["EmpCLs"].Value +
                                                               "," + gvEmpLopDetails.Rows[ivar].Cells["EmpSLs"].Value +
                                                               "," + gvEmpLopDetails.Rows[ivar].Cells["EmpELs"].Value +
                                                               ",'" + cbCompany.SelectedValue.ToString() +
                                                               "','" + cbBranches.SelectedValue.ToString() +
                                                               "'," + Convert.ToInt32(cbDept.SelectedValue) +
                                                               ", 'P' ,'" + CommonData.LogUserId +
                                                               "','" + CommonData.CurrentDate + "')";
                            }
                        }                       
                        
                        if (strCmd.Length > 10)
                        {
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
                        btnCancel_Click(null, null);
                        flagUpdate = false;
                    }
                    else
                    {
                        MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }            

        }
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') != -1)
            {
                e.Handled = true;
            }
        }

        private void gvEmpLopDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            if (Convert.ToString(gvEmpLopDetails.Rows[e.RowIndex].Cells["Lops"].Value) != "" && txtTotDays.Text != "")
            {
                if (Convert.ToDouble(gvEmpLopDetails.Rows[e.RowIndex].Cells["Lops"].Value) <= Convert.ToDouble(txtTotDays.Text))
                {
                    gvEmpLopDetails.Rows[e.RowIndex].Cells["PaidDays"].Value = Convert.ToInt32(txtTotDays.Text) - Convert.ToDouble(gvEmpLopDetails.Rows[e.RowIndex].Cells["Lops"].Value);

                }
                else
                {
                    MessageBox.Show("Please Enter Valid LOPs", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    gvEmpLopDetails.Rows[e.RowIndex].Cells["Lops"].Value = "";
                }

            }
            else
            {
                gvEmpLopDetails.Rows[e.RowIndex].Cells["Lops"].Value = "0";
            }
            if (Convert.ToString(gvEmpLopDetails.Rows[e.RowIndex].Cells["EmpCLs"].Value) != "" && txtTotDays.Text != "")
            {
                if (Convert.ToDouble(gvEmpLopDetails.Rows[e.RowIndex].Cells["EmpCLs"].Value) > Convert.ToDouble(txtTotDays.Text))
                {
                    MessageBox.Show("Please Enter Valid CLs", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    gvEmpLopDetails.Rows[e.RowIndex].Cells["EmpCLs"].Value = "";
                    //gvEmpLopDetails.Rows[e.RowIndex].Cells["EmpCLs"].Value = Convert.ToDouble(gvEmpLopDetails.Rows[e.RowIndex].Cells["EmpCLs"].Value);
                }
            }
            else
            {
                gvEmpLopDetails.Rows[e.RowIndex].Cells["EmpCLs"].Value = "0";
            }
            if (Convert.ToString(gvEmpLopDetails.Rows[e.RowIndex].Cells["EmpSLs"].Value) != "" && txtTotDays.Text != "")
            {
                if (Convert.ToDouble(gvEmpLopDetails.Rows[e.RowIndex].Cells["EmpSLs"].Value) > Convert.ToDouble(txtTotDays.Text))
                {
                    MessageBox.Show("Please Enter Valid SLs", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    gvEmpLopDetails.Rows[e.RowIndex].Cells["EmpSLs"].Value = " ";

                    //gvEmpLopDetails.Rows[e.RowIndex].Cells["EmpSLs"].Value = Convert.ToDouble(gvEmpLopDetails.Rows[e.RowIndex].Cells["EmpSLs"].Value);
                }
            }
            else
            {
                gvEmpLopDetails.Rows[e.RowIndex].Cells["EmpSLs"].Value = "0";
            }

            if (Convert.ToString(gvEmpLopDetails.Rows[e.RowIndex].Cells["EmpELs"].Value) != "" && txtTotDays.Text != "")
            {
                if (Convert.ToDouble(gvEmpLopDetails.Rows[e.RowIndex].Cells["EmpELs"].Value) > Convert.ToDouble(txtTotDays.Text))
                {
                    MessageBox.Show("Please Enter Valid ELs", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    gvEmpLopDetails.Rows[e.RowIndex].Cells["EmpELs"].Value = "";

                    //gvEmpLopDetails.Rows[e.RowIndex].Cells["EmpELs"].Value = Convert.ToDouble(gvEmpLopDetails.Rows[e.RowIndex].Cells["EmpELs"].Value);
                }
            }
            else
            {
                gvEmpLopDetails.Rows[e.RowIndex].Cells["EmpELs"].Value = "0";
            }


            if (Convert.ToString(gvEmpLopDetails.Rows[e.RowIndex].Cells["EmpCLs"].Value) != "" && Convert.ToString(gvEmpLopDetails.Rows[e.RowIndex].Cells["EmpSLs"].Value) != "")
            {
                if (Convert.ToString(gvEmpLopDetails.Rows[e.RowIndex].Cells["EmpELs"].Value) != "" && gvEmpLopDetails.Rows[e.RowIndex].Cells["Lops"].Value.ToString() != "")
                {
                    double Leaves = Convert.ToDouble(gvEmpLopDetails.Rows[e.RowIndex].Cells["EmpCLs"].Value.ToString()) + Convert.ToDouble(gvEmpLopDetails.Rows[e.RowIndex].Cells["EmpSLs"].Value.ToString()) + Convert.ToDouble(gvEmpLopDetails.Rows[e.RowIndex].Cells["EmpELs"].Value.ToString());
                    Leaves = Leaves + Convert.ToDouble(gvEmpLopDetails.Rows[e.RowIndex].Cells["Lops"].Value);
                    if (Leaves <= Convert.ToInt32(txtTotDays.Text))
                    {
                        double workedDays = Convert.ToDouble(txtTotDays.Text) - Leaves;
                        gvEmpLopDetails.Rows[e.RowIndex].Cells["WorkedDays"].Value = workedDays;
                        gvEmpLopDetails.Rows[e.RowIndex].Cells["TotLeaves"].Value = Leaves;

                    }
                    //else if (Leaves > Convert.ToInt32(txtTotDays.Text))
                    //{
                    //    MessageBox.Show("Please Enter Valid Leave Days","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    //}

                }

            }
        }

        private void gvEmpLopDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
        }

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {

            //txtTotDays.Text = Convert.ToString((dtpToDate.Value-dtpFrmDate.Value).TotalDays);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbCompany.SelectedIndex = 0;
            cbBranches.SelectedIndex = -1;
            cbDept.SelectedIndex = 0;
            dtpMonth.Value = DateTime.Today;
            gvEmpLopDetails.Rows.Clear();


        }

        private void gvEmpLopDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            if (e.ColumnIndex == gvEmpLopDetails.Columns["Delete"].Index)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    try
                    {
                        string strCmd = "DELETE FROM HR_EMP_LOPS WHERE HEL_APPL_NO=" + gvEmpLopDetails.Rows[e.RowIndex].Cells["ApplNo"].Value +
                                        " AND HEL_DOC_MONTH='" + Convert.ToDateTime(dtpMonth.Value).ToString("MMM/yyyy").ToUpper() + "'";
                        iRes = objSQLdb.ExecuteSaveData(strCmd);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    if (iRes > 0)
                    {
                        MessageBox.Show("Data Deleted Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillEmpDetails(Convert.ToDateTime(dtpMonth.Value).ToString("MMM/yyyy").ToUpper());

                    }

                    //DataGridViewRow dgvr = gvEmpLopDetails.Rows[e.RowIndex];
                    //gvEmpLopDetails.Rows.Remove(dgvr);
                }
                //if (gvEmpLopDetails.Rows.Count > 0)
                //{
                //    for (int i = 0; i < gvEmpLopDetails.Rows.Count; i++)
                //    {
                //        gvEmpLopDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                //    }
                //}
            }
        }

        private void cbBranches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBranches.SelectedIndex > 0 && cbDept.SelectedIndex > 0)
            {
                FillEmpDetails(Convert.ToDateTime(dtpMonth.Value).ToString("MMM/yyyy"));
            }

        }

    }
}
