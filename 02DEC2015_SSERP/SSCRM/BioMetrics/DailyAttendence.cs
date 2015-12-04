using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSCRM.App_Code;

namespace SSCRM
{
    public partial class DailyAttendence : Form
    {
        ReportViewer childReportViewer = null;
        Security objSecurity = null;
        SQLDB objDB = null;
        string strCmpData = "",strDeptData="";
        public DailyAttendence()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DailyAttendence_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            FillDeptData();
            dtpFDate.Value = DateTime.Today;
            dtpWagePerioad.Value = DateTime.Today;
            cbReportType.SelectedIndex = 0;
        }

        private void FillDeptData()
        {
            try
            {
                string strCmd = "SELECT dept_name,dept_code FROM Dept_Mas";
                objDB = new SQLDB();
                DataTable dt = objDB.ExecuteDataSet(strCmd).Tables[0];
              
                ((ListBox)clbDepartment).DataSource = dt;
                ((ListBox)clbDepartment).DisplayMember = "dept_name";
                ((ListBox)clbDepartment).ValueMember = "dept_code";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
              
                strCmpData = "ALL";
                
            }
            else
            {
                for (int i = 0; i < clbCompany.Items.Count; i++)
                {
                    clbCompany.SetItemCheckState(i, CheckState.Unchecked);
                }
                strCmpData = "";
            }
        }

        private void cbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbReportType.SelectedIndex == 2)
            {
                lblDate.Visible = true;
                dtpFDate.Visible = true;
            }
            else
            {
                lblDate.Visible = false;
                dtpFDate.Visible = false;
            }
        }

        private void chkDeptAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDeptAll.Checked == true)
            {
                for (int i = 0; i < clbDepartment.Items.Count; i++)
                {
                    clbDepartment.SetItemCheckState(i, CheckState.Checked);
                }
             
                strDeptData = "ALL";
               
            }
            else
            {
                for (int i = 0; i < clbDepartment.Items.Count; i++)
                {
                    clbDepartment.SetItemCheckState(i, CheckState.Unchecked);
                }
                strDeptData = "";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            strDeptData = "";
            strCmpData = "";
            for (int i = 0; i < clbDepartment.Items.Count; i++)
            {
                clbDepartment.SetItemCheckState(i, CheckState.Unchecked);
            }
            for (int i = 0; i < clbCompany.Items.Count; i++)
            {
                clbCompany.SetItemCheckState(i, CheckState.Unchecked);
            }
            chkCompAll.Checked = false;
            chkDeptAll.Checked = false;
            //((ListBox)clbCompany).DataSource = null;
            //chkCompAll.CheckState = CheckState.Unchecked;
            //((ListBox)clbDepartment).DataSource = null;
            //chkDeptAll.CheckState = CheckState.Unchecked;
            cbReportType.SelectedIndex = 0;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if(CheckData())
            {
            if (chkCompAll.Checked == false)
            {
                strCmpData = "";
                foreach (DataRowView view in clbCompany.CheckedItems)
                {
                    strCmpData += (view[clbCompany.ValueMember].ToString()) + ",";
                }
                strCmpData = strCmpData.Substring(0, strCmpData.Length - 1);
            }
            if (chkDeptAll.Checked == false)
            {
                strDeptData = "";
                foreach (DataRowView view in clbDepartment.CheckedItems)
                {
                    strDeptData += (view[clbDepartment.ValueMember].ToString()) + ",";
                }
                strDeptData= strDeptData.Substring(0, strDeptData.Length - 1);
            }

            if (cbReportType.SelectedIndex == 1)
            {
                childReportViewer = new ReportViewer(dtpWagePerioad.Value.ToString("MMMyyyy"), strCmpData, "ALL", DateTime.Today.ToString("dd/MMM/yyyy"), "WAGEATTD", strDeptData);
                //childReportViewer = new ReportViewer("JUN2014", "ALL", "ALL","26/JUN/2014", "WAGEATTD", "ALL");
                CommonData.ViewReport = "HR_HO_ATTD_MTODY";
                childReportViewer.Show();
            }
            if (cbReportType.SelectedIndex == 2)
            {
                childReportViewer = new ReportViewer(dtpWagePerioad.Value.ToString("MMMyyyy"), strCmpData, "ALL", dtpFDate.Value.ToString("dd/MMM/yyyy"), "DAYATTD", strDeptData);
                //childReportViewer = new ReportViewer("JUN2014", "ALL", "ALL","26/JUN/2014", "WAGEATTD", "ALL");
                CommonData.ViewReport = "HR_HO_ATTD_DAYATTDy";
                childReportViewer.Show();
            }
        }
        }
        private bool CheckData()
        {
            bool flag=true;
            if(cbReportType.SelectedIndex==0)
            {
                MessageBox.Show("Select ReportType", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                flag = false;
                return flag;
            }
            if(strCmpData.Length==0)
            {
                MessageBox.Show("Select Company", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                flag = false;
                return flag;
            }
            if (strDeptData.Length == 0)
            {
                MessageBox.Show("Select Department", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                flag = false;
                return flag;
            }
            return flag;
        }

        private void clbCompany_ItemCheck(object sender, ItemCheckEventArgs e)
        {
          
        }

        private void clbDepartment_ItemCheck(object sender, ItemCheckEventArgs e)
        {
           
        }

        private void clbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            strCmpData = "";
            foreach (DataRowView view in clbCompany.CheckedItems)
            {
                strCmpData += (view[clbCompany.ValueMember].ToString()) + ",";
            }
        }

        private void clbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            strDeptData = "";
            foreach (DataRowView view in clbDepartment.CheckedItems)
            {
                strDeptData += (view[clbDepartment.ValueMember].ToString()) + ",";
            }
        }
    }
}
