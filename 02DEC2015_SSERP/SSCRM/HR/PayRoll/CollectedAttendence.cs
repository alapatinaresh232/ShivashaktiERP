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
    public partial class CollectedAttendence : Form
    {
        public CollectedAttendence()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dtpCollectedDate.Value > DateTime.Today)
            {
                MessageBox.Show("Date should not exceed than Today Date", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else
            {
                //HR_D2D_PUNCH_INSERT_FROM_BIO
                try
                {
                    SQLDB objSQLdb = new SQLDB();
                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = objSQLdb.CreateParameter("@zBDH_DOWNLOAD_BY", DbType.Int32, Convert.ToInt32(CommonData.LogUserEcode), ParameterDirection.Input);
                    DataSet ds =objSQLdb.ExecuteDataSet("HR_D2D_PUNCH_INSERT_FROM_BIO", CommandType.StoredProcedure,param);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                string strSQL = "select BDH_DOWNLOAD_DATE,BDH_DOWNLOAD_BY,BDH_UPDATED_RECORDS,MEMBER_NAME  from BIOMETRICS_DOWNLOAD_HIST INNER JOIN EORA_MASTER ON ECODE=BDH_DOWNLOAD_BY where BDH_DOWNLOAD_DATE  between  '" + Convert.ToDateTime(DateTime.Today.AddDays(-1)).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(DateTime.Today.AddDays(1)).ToString("dd/MMM/yyyy") + "'";
                SQLDB objSQLDB = new SQLDB();
                DataTable dt = objSQLDB.ExecuteDataSet(strSQL).Tables[0];
                for (int iVar = 0; iVar < dt.Rows.Count;iVar++ )
                {
                    gvBillDetails.Rows.Add();
                    gvBillDetails.Rows[iVar].Cells["CollectedDate"].Value = Convert.ToDateTime( dt.Rows[iVar]["BDH_DOWNLOAD_DATE"].ToString()).ToShortDateString();
                    gvBillDetails.Rows[iVar].Cells["CollectedBy"].Value = dt.Rows[iVar]["BDH_DOWNLOAD_BY"].ToString() + "-" + dt.Rows[iVar]["MEMBER_NAME"].ToString();
                    gvBillDetails.Rows[iVar].Cells["Records"].Value = dt.Rows[iVar]["BDH_UPDATED_RECORDS"].ToString();
                }
            }
        }

        private void CollectedAttendence_Load(object sender, EventArgs e)
        {
            dtpCollectedDate.Value = DateTime.Today;
        }
    }
}
