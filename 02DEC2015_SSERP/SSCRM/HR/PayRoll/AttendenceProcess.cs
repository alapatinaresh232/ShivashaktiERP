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
    public partial class AttendenceProcess : Form
    {
        SQLDB objData = null;
        DateTime selectedMonth;
        DateTime FirstDayOfMonth;
        DateTime LastDayOfMonth;
        public AttendenceProcess()
        {
            InitializeComponent();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                DataTable dt = new DataTable();
                objData = new SQLDB();
                SqlParameter[] param = new SqlParameter[6];
                try
                {
                    param[0] = objData.CreateParameter("@xWagePeriod", DbType.String, dtpWagePerioad.Value.ToString("MMMyyyy").ToUpper(), ParameterDirection.Input);
                    param[1] = objData.CreateParameter("@xCmp_cd", DbType.String, "ALL", ParameterDirection.Input);
                    param[2] = objData.CreateParameter("@xDeptCode", DbType.String, "ALL", ParameterDirection.Input);
                    param[3] = objData.CreateParameter("@xECode", DbType.String, "ALL", ParameterDirection.Input);
                    param[4] = objData.CreateParameter("@xToday", DbType.String, dtpWagePerioad.Value.ToString("dd/MMM/yyyy"), ParameterDirection.Input);
                    param[5] = objData.CreateParameter("@xPROCTYPE", DbType.String, "WAGEATTD", ParameterDirection.Input);
                    dt = objData.ExecuteDataSet("HR_PAYROLL_ATTD_MTOD_PROCESS", CommandType.StoredProcedure, param).Tables[0];

                    string strCMD = "SELECT * FROM HR_PAYROLL_ATTD_MTOD WHERE HPAM_WAGEMONTH='" + dtpWagePerioad.Value.ToString("MMMyyyy").ToUpper() + "'";
                    objData = new SQLDB();
                    dt = objData.ExecuteDataSet(strCMD).Tables[0];
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
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(dt.Rows.Count + "- Employees Data Updated");
                }

            }
        }
        private bool CheckData()
        {
            bool flag = true;
            try
            {
                string strSQL = "select HWP_START_DATE,HWP_END_DATE,HWP_DAYS from HR_WAGE_PERIOD where HWP_WAGE_MONTH='" + (dtpWagePerioad.Value).ToString("MMMyyyy").ToUpper() + "' and hwp_status='RUNNING'";
                objData = new SQLDB();
                DataTable dt = objData.ExecuteDataSet(strSQL).Tables[0];

                strSQL = "SELECT * FROM HR_PAYROLL_ATTD_MTOD_TRAN WHERE HPAMT_DATE='" + LastDayOfMonth.ToString("dd/MMM/yyyy") + "'";
                DataTable dt1 = objData.ExecuteDataSet(strSQL).Tables[0];
                if (dt.Rows.Count > 0 && dt1.Rows.Count>0)
                {
                    //dtpFrom.Value = Convert.ToDateTime(dt.Rows[0]["HWP_START_DATE"].ToString());
                    //dtpTo.Value = Convert.ToDateTime(dt.Rows[0]["HWP_END_DATE"].ToString());
                    //txtNoofDays.Text = dt.Rows[0]["HWP_DAYS"].ToString();
                }
                else
                {
                    flag = false;
                    MessageBox.Show("Selected WagePeriod is Not Valid");
                    //dtpWagePerioad.Value = DateTime.Today.AddDays(-30);
                    return flag;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return flag;
        }
        private void AttendenceProcess_Load(object sender, EventArgs e)
        {
            //dtpWagePerioad.Value = DateTime.Today.AddDays(-30);
            selectedMonth = dtpWagePerioad.Value;
            FirstDayOfMonth = new DateTime(selectedMonth.Year, selectedMonth.Month, 01);
            LastDayOfMonth = new DateTime(selectedMonth.Year, selectedMonth.Month, DateTime.DaysInMonth(dtpWagePerioad.Value.Year, dtpWagePerioad.Value.Month));

        }

        private void dtpWagePerioad_ValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    string strSQL = "select HWP_START_DATE,HWP_END_DATE,HWP_DAYS from HR_WAGE_PERIOD where HWP_WAGE_MONTH='" + (dtpWagePerioad.Value).ToString("MMMyyyy").ToUpper() + "' and hwp_status='RUNNING'";
            //    objData = new SQLDB();
            //    DataTable dt = objData.ExecuteDataSet(strSQL).Tables[0];
            //    if (dt.Rows.Count > 0)
            //    {
            //        //dtpFrom.Value = Convert.ToDateTime(dt.Rows[0]["HWP_START_DATE"].ToString());
            //        //dtpTo.Value = Convert.ToDateTime(dt.Rows[0]["HWP_END_DATE"].ToString());
            //        //txtNoofDays.Text = dt.Rows[0]["HWP_DAYS"].ToString();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Selected WagePeriod is Not Valid");
            //        //dtpWagePerioad.Value = DateTime.Today.AddDays(-30);

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
