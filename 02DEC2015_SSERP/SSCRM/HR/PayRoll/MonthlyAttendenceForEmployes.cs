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
using SSAdmin;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
namespace SSCRM
{
    public partial class MonthlyAttendenceForEmployes : Form
    {
        Security objSecurity = null;
        UtilityDB objUtilityDB = null;
        SQLDB objSQLdb = null;
        HRInfo objHRdb = null;
        bool flagSave = false;
        string strCmpData = "";
        ExcelDB objExDb = null;

        public MonthlyAttendenceForEmployes()
        {
            InitializeComponent();
        }

        private void MonthlyAttendenceForEmployes_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            FillBranchData();
            dtpMonth.Value = DateTime.Today;

            gvEmpLopDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                    System.Drawing.FontStyle.Regular);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (flagSave == true)
            {
                this.Close();
                this.Dispose();
            }
            else
            {
                if (gvEmpLopDetails.Rows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("Do you want to close Data is Not Saved? ",
                                           "CRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        this.Close();
                        this.Dispose();
                    }
                }
                else
                {
                    this.Close();
                    this.Dispose();
                }

            }
        }

     

        private void FillBranchData()
        {
            DataTable table = new DataTable();
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("name", typeof(string));

            //table.Rows.Add("--Select--", "--Select--");
            table.Rows.Add("HO", "HEAD OFFICE");
            table.Rows.Add("OB", "OTHER BRANCH");

             ((ListBox)clbBranch).DataSource = table;
             ((ListBox)clbBranch).DisplayMember = "name";
             ((ListBox)clbBranch).ValueMember = "type";

        }

        private void FillCompanyData()
        {
           try
            {
                objSecurity = new Security();
                DataTable dtCpy = objSecurity.GetCompanyDataSet().Tables[0];

                ((ListBox)clbCompany).DataSource = dtCpy;
                ((ListBox)clbCompany).DisplayMember = "CM_Company_Name";
                ((ListBox)clbCompany).ValueMember = "CM_Company_Code";
                objSecurity = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dtpMonth_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string strSQL = "select HWP_START_DATE,HWP_END_DATE,HWP_DAYS "+
                                " from HR_WAGE_PERIOD "+
                                " where HWP_WAGE_MONTH='" + (dtpMonth.Value).ToString("MMMyyyy").ToUpper() + 
                                "' and hwp_status='RUNNING'";
                objSQLdb = new SQLDB();
                DataTable dt = objSQLdb.ExecuteDataSet(strSQL).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    dtpFrmDate.Value = Convert.ToDateTime(dt.Rows[0]["HWP_START_DATE"].ToString());
                    dtpToDate.Value = Convert.ToDateTime(dt.Rows[0]["HWP_END_DATE"].ToString());
                    txtTotDays.Text = dt.Rows[0]["HWP_DAYS"].ToString();
                }
                else
                {
                    MessageBox.Show("Selected WagePeriod is Not Valid");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //private void cbBranches_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cbBranches.SelectedValue == "HO")
        //    {
        //        cbCompany.Visible = true;
        //        lblCompany.Visible = true;
        //    }
        //    else
        //    {
        //        cbCompany.Visible = false;
        //        lblCompany.Visible = false;
        //    }
        //}

        private void GetEmployeeAttdDetails()
        {
            //string str
        }

        private void btnCollect_Click(object sender, EventArgs e)
        {
            //string strCMD = " SELECT HPAM_COMPANY_CODE,HPAM_BRANCH_CODE,HPAM_WAGEMONTH,'HO'AS HPMTM_LOCATION,HPAM_ECODE,HPAM_NET,HPAM_PRE,HPAM_ABSX,HPAM_WOF,HPAM_HDAY,"+
            //                " HPAM_ELS_AVAILED,HPAM_CLS_AVAILED,HPAM_SLS_AVAILED,HPAM_COFFS_AVAILED,HPAM_LTMNTS,HPAM_EARLYGOMNTS,HPAM_COMPANY_NAME,HPAM_BRANCH_NAME,"+
            //                " HPAM_DEPT_CODE,HPAM_DEPT_NAME,HPAM_ENAME,HPAM_DESIG_ID,HPAM_DESIGNATION,HPAM_DOJ from HR_PAYROLL_ATTD_MTOD WHERE HPAM_WAGEMONTH ='JUN2014' AND HPAM_ECODE NOT IN (SELECT HPOAM_ECODE FROM HR_PAYROLL_MANUAL_ATTD_MTOD)"+
            //                " UNION SELECT HPOAM_COMPANY_CODE,HPOAM_BRANCH_CODE,HPOAM_WAGEMONTH,HPMTM_LOCATION,HPOAM_ECODE,"+
            //                " HPOAM_NET,HPOAM_PRE,HPOAM_ABSX,HPOAM_WOF,HPOAM_HDAY,HPOAM_ELS_AVAILED,HPOAM_CLS_AVAILED,HPOAM_SLS_AVAILED,HPOAM_COFFS_AVAILED,HPOAM_LTMNTS,"+
            //                " HPOAM_EARLYGOMNTS,HPOAM_COMPANY_NAME,HPOAM_BRANCH_NAME,HPOAM_DEPT_CODE,HPOAM_DEPT_NAME,HPOAM_ENAME,HPOAM_DESIG_ID,HPOAM_DESIGNATION,HPOAM_DOJ FROM HR_PAYROLL_MANUAL_ATTD_MTOD"+
            //                " WHERE HPOAM_WAGEMONTH ='JUN2014'";
            //    objSQLdb = new SQLDB();
            //    DataTable dt = objSQLdb.ExecuteDataSet(strCMD).Tables[0];
            if (CheckData())
            {
                chkBranchAll.Checked = true;
                chkCompAll.Checked = true;
                DataTable dt = null,dt1=null;
                objHRdb = new HRInfo();
                try
                {
                    dt = objHRdb.EmployeeAttendDetails(dtpMonth.Value.ToString("MMMyyyy").ToUpper()).Tables[0];
                    //dt1 = objHRdb.EmployeeAttendDetails(dtpMonth.Value.ToString("MMMyyyy").ToUpper()).Tables[1];
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                gvEmpLopDetails.Rows.Clear();
                try
                {

                    if (dt.Rows.Count > 0)
                    {

                        for (int iVar = 0; iVar < dt.Rows.Count; iVar++)
                        {
                            gvEmpLopDetails.Rows.Add();
                            gvEmpLopDetails.Rows[iVar].Cells["SLNO"].Value = (iVar + 1).ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["Ecode"].Value = dt.Rows[iVar]["Ecode"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["EmpName"].Value = dt.Rows[iVar]["EName"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["Desig"].Value = dt.Rows[iVar]["DesingName"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["TotDays"].Value = txtTotDays.Text.ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["Lops"].Value = dt.Rows[iVar]["Abs"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["EmpCLs"].Value = dt.Rows[iVar]["Cls"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["EmpSLs"].Value = dt.Rows[iVar]["Sls"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["EmpELs"].Value = dt.Rows[iVar]["Els"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["CCode"].Value = dt.Rows[iVar]["CCode"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["BCode"].Value = dt.Rows[iVar]["BCode"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["Desig"].Value = dt.Rows[iVar]["DesingName"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["WageMonth"].Value = dt.Rows[iVar]["WageMonth"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["Location"].Value = dt.Rows[iVar]["Location"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["Woff"].Value = dt.Rows[iVar]["WOff"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["Holiday"].Value = dt.Rows[iVar]["Holiday"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["Lmnts"].Value = dt.Rows[iVar]["Lmnts"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["Emnts"].Value = dt.Rows[iVar]["Emnts"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["CName"].Value = dt.Rows[iVar]["CName"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["BName"].Value = dt.Rows[iVar]["BName"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["DCode"].Value = dt.Rows[iVar]["DCode"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["DName"].Value = dt.Rows[iVar]["DName"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["DesigId"].Value = dt.Rows[iVar]["DesigId"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["Doj"].Value = dt.Rows[iVar]["Doj"].ToString();

                            //gvEmpLopDetails.Rows[iVar].Cells["WorkedDays"].Value = dt.Rows[iVar]["Ecode"].ToString();
                            if (Convert.ToDouble(dt.Rows[iVar]["Pre"].ToString()) > Convert.ToDouble(txtTotDays.Text.ToString()))
                            {
                                gvEmpLopDetails.Rows[iVar].Cells["PaidDays"].Value = dt.Rows[iVar]["Pre"].ToString();
                            }
                            else
                            {
                                gvEmpLopDetails.Rows[iVar].Cells["PaidDays"].Value = dt.Rows[iVar]["Pre"].ToString();
                            }
                            //if (Convert.ToDouble(gvEmpLopDetails.Rows[iVar].Cells["PaidDays"].Value.ToString()) > Convert.ToDouble(txtTotDays.Text.ToString()))
                            //{
                            //    gvEmpLopDetails.Rows[iVar].Cells["PaidDays"].Value = txtTotDays.Text.ToString();
                            //}
                            //else
                            //{
                            //    gvEmpLopDetails.Rows[iVar].Cells["PaidDays"].Value = dt.Rows[iVar]["Pre"].ToString();
                            //}

                        }
                    }


                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        private bool CheckData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            bool flag = true;

            

            //if (dt.Rows.Count > 0)
            //{
            //    flag = false;
            //    MessageBox.Show("This Data Has Been Approved! Can Not Be Manipulated", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return flag;

            //}
            
            if (gvEmpLopDetails.Rows.Count > 0)
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
                    if (gvEmpLopDetails.Rows[i].Cells["Coff"].Value == null)
                    {
                        gvEmpLopDetails.Rows[i].Cells["Coff"].Value = "0";
                    }
                    
                    if (Convert.ToString(gvEmpLopDetails.Rows[i].Cells["PaidDays"].Value) == "")
                    {
                        gvEmpLopDetails.Rows[i].Cells["PaidDays"].Value = Convert.ToInt32(gvEmpLopDetails.Rows[i].Cells["TotDays"].Value) - Convert.ToInt32(gvEmpLopDetails.Rows[i].Cells["Lops"].Value);
                    }
                    if (Convert.ToDouble(Convert.ToString(gvEmpLopDetails.Rows[i].Cells["PaidDays"].Value)) > Convert.ToDouble( txtTotDays.Text))
                    {
                        flag = false;
                        MessageBox.Show("Paid Days is Exceeded than Month Days For Employee Ecode- "+gvEmpLopDetails.Rows[i].Cells["Ecode"].Value, "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return flag;
                    }


                }
            }
            return flag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(CheckData())
            {
                objSQLdb = new SQLDB();
                try
                {
                    string strCMD = "";
                    //for (int iVar = 0; iVar < gvEmpLopDetails.Rows.Count; iVar++)
                    //{
                    //    strCMD += " delete HR_PAYROLL_MANUAL_ATTD_MTOD where HPOAM_ECODE='" + gvEmpLopDetails.Rows[iVar].Cells["Ecode"].Value + "' and HPOAM_WAGEMONTH ='" + gvEmpLopDetails.Rows[iVar].Cells["WageMonth"].Value +"'";
                    //}
                    strCMD +="delete HR_PAYROLL_MANUAL_ATTD_MTOD where HPOAM_WAGEMONTH='"+dtpMonth.Value.ToString("MMMyyyy")+"'";
                    objSQLdb.ExecuteSaveData(strCMD);
                    strCMD = "";
                    for (int iVar = 0; iVar < gvEmpLopDetails.Rows.Count; iVar++)
                    {
                        

                        strCMD += " insert into HR_PAYROLL_MANUAL_ATTD_MTOD ( HPOAM_COMPANY_CODE,HPOAM_BRANCH_CODE ,HPOAM_WAGEMONTH,HPMTM_LOCATION ,HPOAM_ECODE ," +
                                        "HPOAM_NET,HPOAM_PRE,HPOAM_ABSX,HPOAM_WOF,HPOAM_HDAY,HPOAM_ELS_AVAILED,HPOAM_CLS_AVAILED,HPOAM_SLS_AVAILED,HPOAM_COFFS_AVAILED,HPOAM_LTMNTS," +
                                        "HPOAM_EARLYGOMNTS,HPOAM_COMPANY_NAME,HPOAM_BRANCH_NAME ,HPOAM_DEPT_CODE ,HPOAM_DEPT_NAME,HPOAM_ENAME,HPOAM_DESIG_ID,HPOAM_DESIGNATION,HPOAM_DOJ) values(" +
                                        "'" + gvEmpLopDetails.Rows[iVar].Cells["CCode"].Value + "','" + gvEmpLopDetails.Rows[iVar].Cells["BCode"].Value + "','" + dtpMonth.Value.ToString("MMMyyyy") +
                                        "','" + gvEmpLopDetails.Rows[iVar].Cells["Location"].Value + "','" + gvEmpLopDetails.Rows[iVar].Cells["Ecode"].Value + "','" + 0 + "','" + gvEmpLopDetails.Rows[iVar].Cells["PaidDays"].Value +
                                        "','" + gvEmpLopDetails.Rows[iVar].Cells["Lops"].Value + "','" + gvEmpLopDetails.Rows[iVar].Cells["Woff"].Value + "','" + gvEmpLopDetails.Rows[iVar].Cells["Holiday"].Value +
                                        "','" + gvEmpLopDetails.Rows[iVar].Cells["EmpELs"].Value + "','" + gvEmpLopDetails.Rows[iVar].Cells["EmpCLs"].Value + "','" + gvEmpLopDetails.Rows[iVar].Cells["EmpSLs"].Value + "','" + gvEmpLopDetails.Rows[iVar].Cells["Coff"].Value +
                                        "','" + gvEmpLopDetails.Rows[iVar].Cells["Lmnts"].Value + "','" + gvEmpLopDetails.Rows[iVar].Cells["Emnts"].Value + "','" + gvEmpLopDetails.Rows[iVar].Cells["CName"].Value + "','" + gvEmpLopDetails.Rows[iVar].Cells["BName"].Value +
                                        "','" + gvEmpLopDetails.Rows[iVar].Cells["DCode"].Value + "','" + gvEmpLopDetails.Rows[iVar].Cells["DName"].Value + "','" + gvEmpLopDetails.Rows[iVar].Cells["EmpName"].Value + "','" + gvEmpLopDetails.Rows[iVar].Cells["DesigId"].Value +
                                        "','" + gvEmpLopDetails.Rows[iVar].Cells["Desig"].Value + "','" + Convert.ToDateTime( gvEmpLopDetails.Rows[iVar].Cells["Doj"].Value).ToString("dd/MMM/yyyy") + "' )";
                    }
                    
                    int iRes = objSQLdb.ExecuteSaveData(strCMD);
                    if (iRes > 0)
                    {
                        MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flagSave = true;
                        //btnCancel_Click(null, null);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    flagSave = false;
                }

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            gvEmpLopDetails.Rows.Clear();
            chkCompAll.Checked = false;
            chkBranchAll.Checked = false;
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
                    gvEmpLopDetails.Rows[e.RowIndex].Cells["Lops"].Value = "0";
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
                    gvEmpLopDetails.Rows[e.RowIndex].Cells["EmpCLs"].Value = "0";
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
                    gvEmpLopDetails.Rows[e.RowIndex].Cells["EmpSLs"].Value = "0";

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
                    gvEmpLopDetails.Rows[e.RowIndex].Cells["EmpELs"].Value = "0";

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
            if (Convert.ToString(gvEmpLopDetails.Rows[e.RowIndex].Cells["PaidDays"].Value) != "" && txtTotDays.Text != "")
            {
                if (Convert.ToDouble(gvEmpLopDetails.Rows[e.RowIndex].Cells["PaidDays"].Value) > Convert.ToDouble(txtTotDays.Text))
                {
                    MessageBox.Show("Please Enter Valid PaidDays", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //else
                //{
                //    MessageBox.Show("Please Enter Valid LOPs", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    gvEmpLopDetails.Rows[e.RowIndex].Cells["Lops"].Value = "";
                //}

            }
        }

        private void gvEmpLopDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
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

        private void clbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            strCmpData = "";
            foreach (DataRowView view in clbCompany.CheckedItems)
            {
                strCmpData += "'" + (view[clbCompany.ValueMember].ToString()) + "',";
            }
            if (strCmpData.Length > 0)
            {
                strCmpData = strCmpData.Substring(0, strCmpData.Length - 1);
            }
            GridRefresh();
        }

        private void GridRefresh()
        {
            if(gvEmpLopDetails.Rows.Count>0)
            {
                for (int iVar = 0; iVar < gvEmpLopDetails.Rows.Count; iVar++)
                {
                    gvEmpLopDetails.Rows[iVar].Visible = false;
                }
                for (int iVar = 0; iVar < gvEmpLopDetails.Rows.Count;iVar++ )
                {
                    string[] sComp = strCmpData.Split(',');
                    //string[] sBranch = 
                    for (int jVar = 0; jVar < sComp.Length; jVar++)
                    {
                        if ("'" + gvEmpLopDetails.Rows[iVar].Cells["CCode"].Value.ToString() + "'" == sComp[jVar] )
                        {
                            gvEmpLopDetails.Rows[iVar].Visible = true;
                        }
                    }
                }
            }
        }

        private void chkCompAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCompAll.Checked == true)
            {
                for (int i = 0; i < clbCompany.Items.Count; i++)
                {
                    clbCompany.SetItemCheckState(i, CheckState.Checked);
                }
                clbCompany.Enabled = false;
                if (gvEmpLopDetails.Rows.Count > 0)
                {
                    for (int iVar = 0; iVar < gvEmpLopDetails.Rows.Count; iVar++)
                    {
                        gvEmpLopDetails.Rows[iVar].Visible = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < clbCompany.Items.Count; i++)
                {
                    clbCompany.SetItemCheckState(i, CheckState.Unchecked);
                }
                clbCompany.Enabled = true;
                clbBranch.Enabled = true;
                chkBranchAll.Checked = false;
                if (gvEmpLopDetails.Rows.Count > 0)
                {
                    for (int iVar = 0; iVar < gvEmpLopDetails.Rows.Count; iVar++)
                    {
                        gvEmpLopDetails.Rows[iVar].Visible = false;
                    }
                }
            }
            //GridRefresh();
            //strCmpData="ALL";
        }

        private void chkBranchAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBranchAll.Checked == true)
            {
                for (int i = 0; i < clbBranch.Items.Count; i++)
                {
                    clbBranch.SetItemCheckState(i, CheckState.Checked);
                }
                clbBranch.Enabled = false;
            }
            else
            {
                for (int i = 0; i < clbBranch.Items.Count; i++)
                {
                    clbBranch.SetItemCheckState(i, CheckState.Unchecked);
                }
                clbBranch.Enabled = true;
            }
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            gvEmpLopDetails.ClearSelection();
            int rowIndex = 0;
            foreach (DataGridViewRow row in gvEmpLopDetails.Rows)
            {
                if (row.Cells[15].Value.ToString().Contains(txtEcodeSearch.Text) == true)
                {
                    rowIndex = row.Index;
                    gvEmpLopDetails.CurrentCell = gvEmpLopDetails.Rows[rowIndex].Cells[15];
                    break;
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ReportViewer childReportViewer = new ReportViewer(dtpMonth.Value.ToString("MMMyyyy").ToUpper(), "ALL");
            CommonData.ViewReport = "HR_PAYROLL_MANUAL_ATTD_MTOD_CHECKLIST";
            childReportViewer.Show();
   
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                objExDb = new ExcelDB();
                DataTable dtExcel = objExDb.GetAttendanceChecklistData(dtpMonth.Value.ToString("MMMyyyy").ToUpper(), "ALL").Tables[0];
                objExDb = null;
                if (dtExcel.Rows.Count > 0)
                {
                    try
                    {
                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;

                        Excel.Range rg = worksheet.get_Range("A1", "M1");
                        Excel.Range rgData = worksheet.get_Range("A2", "M" + (dtExcel.Rows.Count + 1).ToString());
                        rgData.Font.Size = 11;
                        rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.Borders.Weight = 2;

                        rg.Font.Bold = true;
                        rg.Font.Name = "Times New Roman";
                        rg.Font.Size = 10;
                        rg.WrapText = true;
                        rg.Font.ColorIndex = 2; // White Color : 2 and Red = 3,30; Green = 10,43; 
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Interior.ColorIndex = 31;
                        rg.Borders.Weight = 2;
                        rg.Borders.LineStyle = Excel.Constants.xlSolid;
                        rg.Cells.RowHeight = 38;

                        rg = worksheet.get_Range("A1", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "Sl.No";

                        rg = worksheet.get_Range("B1", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg.Cells.Value2 = "Company";

                        rg = worksheet.get_Range("C1", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg.Cells.Value2 = "Branch";

                        rg = worksheet.get_Range("D1", Type.Missing);
                        rg.Cells.ColumnWidth = 6;
                        rg.Cells.Value2 = "Month";

                        rg = worksheet.get_Range("E1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Ecode";

                        rg = worksheet.get_Range("F1", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg.Cells.Value2 = "Name";

                        rg = worksheet.get_Range("G1", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg.Cells.Value2 = "Designation";

                        rg = worksheet.get_Range("H1", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg.Cells.Value2 = "Department";

                        rg = worksheet.get_Range("I1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Location";

                        rg = worksheet.get_Range("J1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "PaidDays";

                        rg = worksheet.get_Range("K1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Lops";

                        rg = worksheet.get_Range("L1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "ErrorType";

                        rg = worksheet.get_Range("M1", Type.Missing);
                        rg.Cells.ColumnWidth = 30;
                        rg.Cells.Value2 = "Error Description";


                        int RowCounter = 1;

                        foreach (DataRow dr in dtExcel.Rows)
                        {
                            int i = 1;
                            worksheet.Cells[RowCounter + 1, i++] = RowCounter;
                            worksheet.Cells[RowCounter + 1, i++] = dr["ATTDCHKLST_COMPANY_NAME"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["ATTDCHKLST_BRANCH_NAME"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["ATTDCHKLST_WAGEMONTH"].ToString().ToUpper();
                            worksheet.Cells[RowCounter + 1, i++] = dr["ATTDCHKLST_ECODE"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["ATTDCHKLST_NAME"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["ATTDCHKLST_DESIG"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["ATTDCHKLST_DEPT_NAME"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["ATTDCHKLST_LOCATION"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["ATTDCHKLST_PRE"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["ATTDCHKLST_LOP"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["ATTDCHKLST_ERROR_TYPE"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["ATTDCHKLST_ERROR_NAME"].ToString();


                            RowCounter++;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {

            //ReportViewer childReportViewer = new ReportViewer(dtpMonth.Value.ToString("MMMyyyy").ToUpper(), "ALL", "ALL", "PAYCHECKLIST");
            //CommonData.ViewReport = "HR_PAYROLL_CALC_CHECKLIST";
            //childReportViewer.Show();


            try
            {
                objExDb = new ExcelDB();
                objUtilityDB = new UtilityDB();
                DataTable dtExcel = new DataTable();
                dtExcel = objExDb.GetPayRollCheckListReportsData(dtpMonth.Value.ToString("MMMyyyy").ToUpper(), "ALL", "ALL", "PAYCHECKLIST").Tables[0];
                objExDb = null;

                if (dtExcel.Rows.Count > 0)
                {
                    Excel.Application oXL = new Excel.Application();
                    Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                    Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                    oXL.Visible = true;
                    string sLastColumn = objUtilityDB.GetColumnName(51);
                    Excel.Range rg = worksheet.get_Range("A3", sLastColumn + "3");
                    Excel.Range rgData = worksheet.get_Range("A3", sLastColumn + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString());
                    rgData.Font.Size = 11;
                    rgData.WrapText = true;
                    rgData.VerticalAlignment = Excel.Constants.xlCenter;
                    rgData.Borders.Weight = 2;



                    rg.Font.Bold = true;
                    rg.Font.Name = "Times New Roman";
                    rg.Font.Size = 10;
                    rg.WrapText = true;
                    rg.Font.ColorIndex = 2; // White Color : 2 and Red = 3,30; Green = 10,43; 
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.RowHeight = 38;

                    rg = worksheet.get_Range("A3", Type.Missing);
                    rg.Cells.ColumnWidth = 4;

                    rg = worksheet.get_Range("B3", Type.Missing);
                    rg.Cells.ColumnWidth = 10;

                    rg = worksheet.get_Range("C3", Type.Missing);
                    rg.Cells.ColumnWidth = 6;

                    rg = worksheet.get_Range("D3", Type.Missing);
                    rg.Cells.ColumnWidth = 30;

                    rg = worksheet.get_Range("E3", Type.Missing);
                    rg.Cells.ColumnWidth = 30;

                    rg = worksheet.get_Range("F3", Type.Missing);
                    rg.Cells.ColumnWidth = 30;

                    rg = worksheet.get_Range("G3", Type.Missing);
                    rg.Cells.ColumnWidth = 15;

                    rg = worksheet.get_Range("H3", Type.Missing);
                    rg.Cells.ColumnWidth = 7;

                    rg = worksheet.get_Range("I3", Type.Missing);
                    rg.Cells.ColumnWidth = 7;

                    //rg = worksheet.get_Range("I3", Type.Missing);
                    //rg.Cells.ColumnWidth = 10;
                    //rg.Interior.ColorIndex = 44;

                    rg = worksheet.get_Range(objUtilityDB.GetColumnName(47) + "3", Type.Missing);
                    rg.Cells.ColumnWidth = 10;
                    rg = worksheet.get_Range(objUtilityDB.GetColumnName(48) + "3", Type.Missing);
                    rg.Cells.ColumnWidth = 10;
                    rg = worksheet.get_Range(objUtilityDB.GetColumnName(51) + "3", Type.Missing);
                    rg.Cells.ColumnWidth = 15;
                    rg = worksheet.get_Range(objUtilityDB.GetColumnName(49) + "3", Type.Missing);
                    rg.Cells.ColumnWidth = 20;
                    rg = worksheet.get_Range(objUtilityDB.GetColumnName(50) + "3", Type.Missing);
                    rg.Cells.ColumnWidth = 20;

                    Excel.Range rgHead = null;
                    rgHead = worksheet.get_Range("A1", "I2");
                    rgHead.Merge(Type.Missing);
                    rgHead.Font.Size = 14;
                    rgHead.Font.ColorIndex = 1;
                    rgHead.Font.Bold = true;
                    rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                    rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                    
                    rgHead.Cells.Value2 = "PAY ROLL REGISTER FOR THE MONTH OF " + dtExcel.Rows[0]["HPCM_WAGEMONTH"].ToString() + "";

                    int iColumn = 1;
                    worksheet.Cells[3, iColumn++] = "SlNo";
                    worksheet.Cells[3, iColumn++] = "Company";
                    worksheet.Cells[3, iColumn++] = "Ecode";
                    worksheet.Cells[3, iColumn++] = "Name";
                    worksheet.Cells[3, iColumn++] = "Desig";
                    worksheet.Cells[3, iColumn++] = "Department";
                    worksheet.Cells[3, iColumn++] = "Doj";
                    worksheet.Cells[3, iColumn++] = "Present Daye";
                    worksheet.Cells[3, iColumn++] = "Lops";
                    
                    for (int i = 0; i < 2; i++)
                    {
                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumn) + "2", objUtilityDB.GetColumnName(iColumn + 13) + "2");
                        //rgHead.Cells.ColumnWidth = 5;
                        rgHead.Merge(Type.Missing);
                        if (i == 0)
                            rgHead.Value2 = "ACTUALS";
                        else
                            rgHead.Value2 = "EARNINGS";
                        rgHead.Interior.ColorIndex = 44 + i;
                        rgHead.Borders.Weight = 2;
                        rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
                        rgHead.Cells.RowHeight = 20;
                        rgHead.Font.Size = 14;
                        rgHead.Font.ColorIndex = 1;
                        rgHead.Font.Bold = true;
                        rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumn) + "3", objUtilityDB.GetColumnName(iColumn + 13) + "3");
                        rgHead.Interior.ColorIndex = 44 + i;
                        rgHead.Font.ColorIndex = 1;
                        rgHead.Cells.ColumnWidth = 5;

                        worksheet.Cells[3, iColumn++] = "PF Basic";
                        worksheet.Cells[3, iColumn++] = "Basic";
                        worksheet.Cells[3, iColumn++] = "HRA";
                        worksheet.Cells[3, iColumn++] = "Conv Allw";
                        worksheet.Cells[3, iColumn++] = "CCA Allw";
                        worksheet.Cells[3, iColumn++] = "LTA Allw";
                        worksheet.Cells[3, iColumn++] = "Special Allw";
                        worksheet.Cells[3, iColumn++] = "Books & Period.";
                        worksheet.Cells[3, iColumn++] = "Med Reimb";
                        worksheet.Cells[3, iColumn++] = "Children";
                        worksheet.Cells[3, iColumn++] = "Vehicle Allw";
                        worksheet.Cells[3, iColumn++] = "Petrol Allw";
                        worksheet.Cells[3, iColumn++] = "Uniform Allw";
                        worksheet.Cells[3, iColumn++] = "Earning Total";
                    }

                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumn) + "2", objUtilityDB.GetColumnName(iColumn + 7) + "2");
                    //rgHead.Cells.ColumnWidth = 5;
                    rgHead.Merge(Type.Missing);
                    rgHead.Value2 = "DEDUCTIONS";
                    rgHead.Interior.ColorIndex = 46;
                    rgHead.Borders.Weight = 2;
                    rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
                    rgHead.Cells.RowHeight = 20;
                    rgHead.Font.Size = 14;
                    rgHead.Font.ColorIndex = 1;
                    rgHead.Font.Bold = true;
                    rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                    rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumn) + "3", objUtilityDB.GetColumnName(iColumn + 7) + "3");
                    rgHead.Interior.ColorIndex = 46;
                    rgHead.Font.ColorIndex = 1;
                    rgHead.Cells.ColumnWidth = 5;

                    worksheet.Cells[3, iColumn++] = "PF";
                    worksheet.Cells[3, iColumn++] = "Proff Tax";
                    worksheet.Cells[3, iColumn++] = "ESI";
                    worksheet.Cells[3, iColumn++] = "Sal Adv";
                    worksheet.Cells[3, iColumn++] = "Pers Loan";
                    worksheet.Cells[3, iColumn++] = "TDS";
                    worksheet.Cells[3, iColumn++] = "Others";
                    worksheet.Cells[3, iColumn++] = "Total Deductions";

                    worksheet.Cells[3, iColumn++] = "Net Pay";
                    worksheet.Cells[3, iColumn++] = "Pay Mode";
                    worksheet.Cells[3, iColumn++] = "Bank Name";
                    worksheet.Cells[3, iColumn++] = "Account No";
                    worksheet.Cells[3, iColumn++] = "PF No";
                    worksheet.Cells[3, iColumn++] = "ESI No";

                    iColumn = 1;
                    for (int i = 0; i < dtExcel.Rows.Count; i++)
                    {
                        worksheet.Cells[i + 4, iColumn++] = i + 1;
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_COMPANY_CODE"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_EORA_CODE"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_NAME"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_DESIG"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_DEPT_NAME"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_DOJ"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PRE"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_LOP"].ToString();                        

                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PF_BASIC"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_BASIC"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_HRA"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_CONV_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_CCA"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_LTA_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_SPL_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_BNP_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_MED_REIMB"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_CH_ED_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_VEH_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PET_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_UNF_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = "=U" + (i + 4).ToString() + "+V" + (i + 4).ToString() +
                                                            "+K" + (i + 4).ToString() + "+L" + (i + 4).ToString() +
                                                            "+M" + (i + 4).ToString() + "+N" + (i + 4).ToString() +
                                                            "+O" + (i + 4).ToString() + "+P" + (i + 4).ToString() +
                                                            "+Q" + (i + 4).ToString() + "+R" + (i + 4).ToString() +
                                                            "+S" + (i + 4).ToString() + "+T" + (i + 4).ToString() + "";

                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_Pf_ERNG_BASIC"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_BASIC"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_HRA"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_CONV_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_CCA"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_LTA_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_SPL_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_BNP_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_MED_REIMB"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_CH_ED_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_VEH_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_PET_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_UNF_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_TOTAL"].ToString();
                        //worksheet.Cells[i + 4, iColumn++] = "=AG" + (i + 4).ToString() + "+AH" + (i + 4).ToString() +
                        //                                    "+AI" + (i + 4).ToString() + "+AJ" + (i + 4).ToString() +
                        //                                    "+Y" + (i + 4).ToString() + "+Z" + (i + 4).ToString() +
                        //                                    "+AA" + (i + 4).ToString() + "+AB" + (i + 4).ToString() +
                        //                                    "+AC" + (i + 4).ToString() + "+AD" + (i + 4).ToString() +
                        //                                    "+AE" + (i + 4).ToString() + "+AF" + (i + 4).ToString() + "";

                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_PF"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_PROFTAX"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_ESI"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_SAL_ADV"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_PERS_LOAN"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_TDS"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_OTHERS"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_TOTAL"].ToString();

                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_NET_PAY"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PAY_MODE"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_BANK_NAME"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_BANK_ACCOUNT_NO"].ToString() + "'";
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_PF_NUMBER"].ToString() + "";
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ESI_NUMBER"].ToString() + "";

                        iColumn = 1;
                    }
                }
                else
                {
                    MessageBox.Show("No Data Found!", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

    }
}
