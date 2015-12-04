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
    public partial class RollBackProcess : Form
    {
        SQLDB objDB = null;
        public RollBackProcess()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void RollBackProcess_Load(object sender, EventArgs e)
        {
            FillWagePeriod();
        }

        private void FillWagePeriod()
        {
            try
            {
                string strCMD = "SELECT HWP_WAGE_MONTH,HWP_WAGE_MONTH FROM HR_WAGE_PERIOD WHERE HWP_STATUS='PROCESSED' ";
                objDB = new SQLDB();

                DataTable dt = objDB.ExecuteDataSet(strCMD).Tables[0];
                DataRow dr = dt.NewRow();
                dr[0] = "--Select--";
                dr[1] = "--Select--";
                dt.Rows.InsertAt(dr, 0);
                cbReportType.DataSource = null;

                if (dt.Rows.Count > 1)
                {
                    cbReportType.DataSource = dt;
                    cbReportType.DisplayMember = "HWP_WAGE_MONTH";
                    cbReportType.ValueMember = "HWP_WAGE_MONTH";
                }
                else
                {

                    MessageBox.Show(" You Have No Processed Wage Months!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                try
                {
                    string strCMD = " DELETE HR_PAYROLL_CALC_MONYY WHERE  HPCM_WAGEMONTH='" + cbReportType.SelectedValue + "'";
                    strCMD += "UPDATE HR_WAGE_PERIOD SET HWP_STATUS='RUNNING' WHERE  HWP_WAGE_MONTH='" + cbReportType.SelectedValue + "' AND HWP_STATUS='PROCESSED' ";
                    objDB = new SQLDB();
                    int iRes = objDB.ExecuteSaveData(strCMD);
                    if (iRes > 0)
                    {
                        MessageBox.Show("Employee Data is Deleted Which has been Processed for the " + cbReportType.SelectedValue + "- WagePeriod");

                        strCMD = " insert into HR_PAYROLL_PROC_ROLLBACK_HIST (HPRH_WAGE_PERIOD,HPRH_DONE_BY,HPRH_DATE,HPRH_REMARKS)values ('" + cbReportType.SelectedValue + "','"+CommonData.LogUserId+"',getdate(),'" + txtReason.Text + "')";

                         objDB.ExecuteSaveData(strCMD);
                        FillWagePeriod();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private bool CheckData()
        {
            bool flag = false;

            if (cbReportType.SelectedIndex == 0 || cbReportType.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select WagePeriod");
                flag = false;
                return flag;
            }
          
            if (cbReportType.SelectedIndex > 0)
            {

                DialogResult result = MessageBox.Show("Employee Data will be DELETED for the " + cbReportType.SelectedValue + " - WagePeriod. You can Do PayRoll Process Again with PayRoll Process Screen", "SSCRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    flag = true;
                    return flag;
                }
            }
            if (txtReason.Text.Length == 0)
            {
                MessageBox.Show("Please Enter Reason");
                flag = false;
                return flag;
            }

            return flag;
        }

        private void txtReason_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = char.IsLetter(e.KeyChar);
        }
    }
}
