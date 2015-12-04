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
    public partial class PayRollProcess : Form
    {
         SQLDB objData =null;
         DateTime selectedMonth;
         DateTime FirstDayOfMonth;
         DateTime LastDayOfMonth;
        public PayRollProcess()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void PayRollProcess_Load(object sender, EventArgs e)
        {
            FillWagePeriod();
            btnReport.Enabled = false;
            //dtpWagePerioad.Value = DateTime.Today.AddDays(-30);
            //selectedMonth = dtpWagePerioad.Value;
            //FirstDayOfMonth = new DateTime(selectedMonth.Year, selectedMonth.Month, 01);
            //LastDayOfMonth = new DateTime(selectedMonth.Year, selectedMonth.Month, DateTime.DaysInMonth(dtpWagePerioad.Value.Year, dtpWagePerioad.Value.Month));
        }
        private void FillWagePeriod()
        {
            try
            {
                string strCMD = "SELECT HWP_WAGE_MONTH,HWP_WAGE_MONTH FROM HR_WAGE_PERIOD WHERE HWP_STATUS='RUNNING' ";
                objData = new SQLDB();


              //  LastDayOfMonth = Convert.ToDateTime(cbWagePeriod.SelectedValue.ToString());
              //  LastDayOfMonth = new DateTime(Convert.ToInt32(cbWagePeriod.SelectedValue.ToString().Substring(3)), Convert.ToInt32(cbWagePeriod.SelectedValue.ToString().Substring(0, 2)), DateTime.DaysInMonth(Convert.ToInt32(cbWagePeriod.SelectedValue.ToString().Substring(3)), LastDayOfMonth.Month));
              //string   strSQL = "SELECT * FROM HR_PAYROLL_ATTD_MTOD_TRAN WHERE HPAMT_DATE='" + LastDayOfMonth.ToString("dd/MMM/yyyy") + "'";
              //  DataTable dt1 = objData.ExecuteDataSet(strSQL).Tables[0];




                DataTable dt = objData.ExecuteDataSet(strCMD).Tables[0];


                DataRow dr = dt.NewRow();
                dr[0] = "--Select--";
                dr[1] = "--Select--";
                dt.Rows.InsertAt(dr, 0);
                cbWagePeriod.DataSource = null;

                if (dt.Rows.Count > 1 )
                {
                    cbWagePeriod.DataSource = dt;
                    cbWagePeriod.DisplayMember = "HWP_WAGE_MONTH";
                    cbWagePeriod.ValueMember = "HWP_WAGE_MONTH";
                }
                else
                {

                    MessageBox.Show(" You Have No Processed Wage Months!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnReport_Click(object sender, EventArgs e)
        {

            if(CheckData())
            {
            DataTable dt = new DataTable();
            objData = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            try
            {
                param[0] = objData.CreateParameter("@xWagePeriod", DbType.String, cbWagePeriod.SelectedValue.ToString(), ParameterDirection.Input);
                param[1] = objData.CreateParameter("@xCmp_cd", DbType.String, "ALL", ParameterDirection.Input);
                param[2] = objData.CreateParameter("@xECode", DbType.String, "ALL", ParameterDirection.Input);
                param[3] = objData.CreateParameter("@xPROCTYPE", DbType.String, "PROCESS", ParameterDirection.Input);
                dt = objData.ExecuteDataSet("HR_PAYROLL_CALC_PROCESS", CommandType.StoredProcedure, param).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objData = null;
                param = null;
            }
            if (dt.Rows.Count>0)
            {
                MessageBox.Show(dt.Rows.Count+" Employees Details Updated");
                FillWagePeriod();
            }


            }
        }

        private void dtpWagePerioad_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                //string strSQL = "select HWP_START_DATE,HWP_END_DATE,HWP_DAYS from HR_WAGE_PERIOD where HWP_WAGE_MONTH='" + (dtpWagePerioad.Value).ToString("MMMyyyy").ToUpper() + "' and hwp_status='RUNNING'";
                //objData = new SQLDB();
                //DataTable dt = objData.ExecuteDataSet(strSQL).Tables[0];

                
                //if (dt.Rows.Count > 0)
                //{
                //    //dtpFrom.Value = Convert.ToDateTime(dt.Rows[0]["HWP_START_DATE"].ToString());
                //    //dtpTo.Value = Convert.ToDateTime(dt.Rows[0]["HWP_END_DATE"].ToString());
                //    //txtNoofDays.Text = dt.Rows[0]["HWP_DAYS"].ToString();
                //}
                //else
                //{
                //    MessageBox.Show("Selected WagePeriod is Not Valid");
                //    //dtpWagePerioad.Value = DateTime.Today.AddDays(-30);

                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CheckData()
        {
            bool flag = true;
            if (cbWagePeriod.SelectedIndex == 0)
            {
                MessageBox.Show("Please Select WagePeriod");
                flag = false;
            }
            if (cbWagePeriod.SelectedIndex == -1)
            {
                MessageBox.Show("You Have No Running WagePeriod");
                flag = false;
            }
            if (cbWagePeriod.SelectedIndex > 0)
            {
                try
                {
                    string strSQL = "select HWP_START_DATE,HWP_END_DATE,HWP_DAYS from HR_WAGE_PERIOD where HWP_WAGE_MONTH='" + cbWagePeriod.SelectedValue.ToString() + "' and hwp_status='RUNNING'";
                    objData = new SQLDB();
                    DataTable dt = objData.ExecuteDataSet(strSQL).Tables[0];

                    LastDayOfMonth = Convert.ToDateTime(cbWagePeriod.SelectedValue.ToString());
                    LastDayOfMonth = new DateTime(LastDayOfMonth.Year, LastDayOfMonth.Month, DateTime.DaysInMonth(LastDayOfMonth.Year, LastDayOfMonth.Month));
                    strSQL = "SELECT * FROM HR_PAYROLL_ATTD_MTOD_TRAN WHERE HPAMT_DATE='" + LastDayOfMonth.ToString("dd/MMM/yyyy") + "'";
                    DataTable dt1 = objData.ExecuteDataSet(strSQL).Tables[0];
                    if (dt.Rows.Count > 0 && dt1.Rows.Count > 0)
                    {
                        //dtpFrom.Value = Convert.ToDateTime(dt.Rows[0]["HWP_START_DATE"].ToString());
                        //dtpTo.Value = Convert.ToDateTime(dt.Rows[0]["HWP_END_DATE"].ToString());
                        //txtNoofDays.Text = dt.Rows[0]["HWP_DAYS"].ToString();
                    }
                    else
                    {
                        flag = false;
                        MessageBox.Show("Selected WagePeriod is Not Completed");
                        //dtpWagePerioad.Value = DateTime.Today.AddDays(-30);
                        return flag;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return flag;
        }

        private void cbWagePeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvEmpLopDetails.Rows.Clear();
            gvEmpSal.Rows.Clear();
            btnReport.Enabled = false;
            if (cbWagePeriod.SelectedIndex > 0)
            {
                try
                {
                     string strCMD1 = "select * from HR_WAGE_PERIOD where HWP_WAGE_MONTH='"+cbWagePeriod.SelectedValue.ToString()+"'";
                     objData = new SQLDB();
                     DataTable dt1 = objData.ExecuteDataSet(strCMD1).Tables[0];
                     string strCmd = " SELECT HPOAM_COMPANY_CODE CCode,HPOAM_BRANCH_CODE BCode,HPOAM_WAGEMONTH WageMonth,HPMTM_LOCATION ,HPOAM_ECODE Ecode,HPOAM_NET Net," +
                                     " HPOAM_PRE Pre,HPOAM_ABSX Abs, HPOAM_WOF WOff,HPOAM_HDAY Holiday,HPOAM_ELS_AVAILED Els,HPOAM_CLS_AVAILED Cls,HPOAM_SLS_AVAILED Sls,HPOAM_COFFS_AVAILED Coff, " +
                                     " HPOAM_LTMNTS Lmnts,HPOAM_EARLYGOMNTS Emnts,HPOAM_COMPANY_NAME CName,HPOAM_BRANCH_NAME BName,HPOAM_DEPT_CODE DCode,HPOAM_DEPT_NAME DName," +
                                     " HPOAM_ENAME EName,HPOAM_DESIG_ID DesigId,HPOAM_DESIGNATION DesingName,HPOAM_DOJ Doj from HR_PAYROLL_MANUAL_ATTD_MTOD WHERE HPOAM_WAGEMONTH ='" + cbWagePeriod.SelectedValue.ToString() + "' and (HPOAM_PRE+HPOAM_ABSX)<> (" + dt1.Rows[0]["HWP_DAYS"] + ")";

                    DataTable dt2 = objData.ExecuteDataSet(strCmd).Tables[0];
                    
                    if (dt2.Rows.Count > 0)
                    {
                        gvEmpLopDetails.Rows.Clear();
                        for (int iVar = 0; iVar < dt2.Rows.Count; iVar++)
                        {
                            gvEmpLopDetails.Rows.Add();
                            gvEmpLopDetails.Rows[iVar].Cells["SLNO"].Value = (iVar + 1).ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["Ecode"].Value = dt2.Rows[iVar]["Ecode"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["EmpName"].Value = dt2.Rows[iVar]["EName"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["Desig"].Value = dt2.Rows[iVar]["DesingName"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["TotDays"].Value = dt1.Rows[0]["HWP_DAYS"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["Lops"].Value = dt2.Rows[iVar]["Abs"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["EmpCLs"].Value = dt2.Rows[iVar]["Cls"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["EmpSLs"].Value = dt2.Rows[iVar]["Sls"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["EmpELs"].Value = dt2.Rows[iVar]["Els"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["CCode"].Value = dt2.Rows[iVar]["CCode"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["BCode"].Value = dt2.Rows[iVar]["BCode"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["Desig"].Value = dt2.Rows[iVar]["DesingName"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["WageMonth"].Value = dt2.Rows[iVar]["WageMonth"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["Location"].Value = dt2.Rows[iVar]["HPMTM_LOCATION"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["Woff"].Value = dt2.Rows[iVar]["WOff"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["Holiday"].Value = dt2.Rows[iVar]["Holiday"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["Lmnts"].Value = dt2.Rows[iVar]["Lmnts"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["Emnts"].Value = dt2.Rows[iVar]["Emnts"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["CName"].Value = dt2.Rows[iVar]["CName"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["BName"].Value = dt2.Rows[iVar]["BName"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["DCode"].Value = dt2.Rows[iVar]["DCode"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["DName"].Value = dt2.Rows[iVar]["DName"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["DesigId"].Value = dt2.Rows[iVar]["DesigId"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["Doj"].Value = dt2.Rows[iVar]["Doj"].ToString();

                            //gvEmpLopDetails.Rows[iVar].Cells["WorkedDays"].Value = dt.Rows[iVar]["Ecode"].ToString();
                            gvEmpLopDetails.Rows[iVar].Cells["PaidDays"].Value = dt2.Rows[iVar]["Pre"].ToString(); ;

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
                    else
                    {
                        btnReport.Enabled = true;
                    }
                    string strCmd3="SELECT * FROM HR_PAYROLL_MANUAL_ATTD_MTOD WHERE HPOAM_WAGEMONTH ='" + cbWagePeriod.SelectedValue.ToString() + "'  ";

                   

                    string strCMD = "SELECT * FROM HR_PAYROLL_MANUAL_ATTD_MTOD WHERE HPOAM_ECODE  not IN (SELECT HESS_EORA_CODE FROM HR_EMP_SAL_STRU )AND HPOAM_WAGEMONTH='" + cbWagePeriod.SelectedValue.ToString() + "'";
                   
                    objData = new SQLDB();
                    DataTable dt = objData.ExecuteDataSet(strCMD).Tables[0];
                    DataTable dt3 = objData.ExecuteDataSet(strCmd3).Tables[0];
                    if(dt3.Rows.Count==0)
                    {
                        btnReport.Enabled = false;
                    }
                    //string strcount= dt1.Rows[iVar]["HWP_DAYS"].ToString();
                    if (dt.Rows.Count > 0)
                    {
                        gvEmpSal.Rows.Clear();
                        for (int iVar = 0; iVar < dt.Rows.Count; iVar++)
                        {
                            gvEmpSal.Rows.Add();
                            gvEmpSal.Rows[iVar].Cells["SLNO1"].Value = (iVar + 1).ToString();
                            gvEmpSal.Rows[iVar].Cells["Ecode1"].Value = dt.Rows[iVar]["HPOAM_ECODE"].ToString();
                            gvEmpSal.Rows[iVar].Cells["EmpName1"].Value = dt.Rows[iVar]["HPOAM_ENAME"].ToString();
                            gvEmpSal.Rows[iVar].Cells["Desig1"].Value = dt.Rows[iVar]["HPOAM_DESIGNATION"].ToString();
                            gvEmpSal.Rows[iVar].Cells["TotDays1"].Value = dt1.Rows[0]["HWP_DAYS"].ToString();
                            gvEmpSal.Rows[iVar].Cells["Lops1"].Value = dt.Rows[iVar]["HPOAM_ABSX"].ToString();
                            gvEmpSal.Rows[iVar].Cells["EmpCLs1"].Value = dt.Rows[iVar]["HPOAM_CLS_AVAILED"].ToString();
                            gvEmpSal.Rows[iVar].Cells["EmpSLs1"].Value = dt.Rows[iVar]["HPOAM_SLS_AVAILED"].ToString();
                            gvEmpSal.Rows[iVar].Cells["EmpELs1"].Value = dt.Rows[iVar]["HPOAM_ELS_AVAILED"].ToString();
                            gvEmpSal.Rows[iVar].Cells["CCode1"].Value = dt.Rows[iVar]["HPOAM_COMPANY_CODE"].ToString();
                            gvEmpSal.Rows[iVar].Cells["BCode1"].Value = dt.Rows[iVar]["HPOAM_BRANCH_CODE"].ToString();
                            gvEmpSal.Rows[iVar].Cells["Desig1"].Value = dt.Rows[iVar]["HPOAM_DESIGNATION"].ToString();
                            gvEmpSal.Rows[iVar].Cells["WageMonth1"].Value = dt.Rows[iVar]["HPOAM_WAGEMONTH"].ToString();
                            gvEmpSal.Rows[iVar].Cells["Location1"].Value = dt.Rows[iVar]["HPMTM_LOCATION"].ToString();
                            gvEmpSal.Rows[iVar].Cells["Woff1"].Value = dt.Rows[iVar]["HPOAM_WOF"].ToString();
                            gvEmpSal.Rows[iVar].Cells["Holiday1"].Value = dt.Rows[iVar]["HPOAM_HDAY"].ToString();
                            gvEmpSal.Rows[iVar].Cells["Lmnts1"].Value = dt.Rows[iVar]["HPOAM_LTMNTS"].ToString();
                            gvEmpSal.Rows[iVar].Cells["Emnts1"].Value = dt.Rows[iVar]["HPOAM_EARLYGOMNTS"].ToString();
                            gvEmpSal.Rows[iVar].Cells["CName1"].Value = dt.Rows[iVar]["HPOAM_COMPANY_NAME"].ToString();
                            gvEmpSal.Rows[iVar].Cells["BName1"].Value = dt.Rows[iVar]["HPOAM_BRANCH_NAME"].ToString();
                            gvEmpSal.Rows[iVar].Cells["DCode1"].Value = dt.Rows[iVar]["HPOAM_DEPT_CODE"].ToString();
                            gvEmpSal.Rows[iVar].Cells["DName1"].Value = dt.Rows[iVar]["HPOAM_DEPT_NAME"].ToString();
                            gvEmpSal.Rows[iVar].Cells["DesigId1"].Value = dt.Rows[iVar]["HPOAM_DESIG_ID"].ToString();
                            gvEmpSal.Rows[iVar].Cells["Doj1"].Value = dt.Rows[iVar]["HPOAM_DOJ"].ToString();

                            //gvEmpLopDetails.Rows[iVar].Cells["WorkedDays"].Value = dt.Rows[iVar]["Ecode"].ToString();

                            gvEmpSal.Rows[iVar].Cells["PaidDays1"].Value = dt.Rows[iVar]["HPOAM_PRE"].ToString(); ;
                           
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

      
    }
}
