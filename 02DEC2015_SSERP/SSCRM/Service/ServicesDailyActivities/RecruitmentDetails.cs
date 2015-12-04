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
    public partial class RecruitmentDetails : Form
    {
        SQLDB objSQLdb = null;
        public ServiceActivities objServiceActivities = null;
        DataRow[] drs;
      

        public RecruitmentDetails()
        {
            InitializeComponent();
           
        }
        public RecruitmentDetails(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }

       

        private void RecruitmentDetails_Load(object sender, EventArgs e)
        {
            
            FillDepartmentData();
            if (drs != null)
            {               
                txtNoOfPersons.Text = drs[0]["NoOfrecruitPersons"].ToString();
                txtRemarks.Text = drs[0]["Remarks"].ToString();

                string DeptName = drs[0]["RecruitDeptName"].ToString();
                for (int i = 0; i < clbDepartments.Items.Count; i++)
                {
                    if (((NewCheckboxListItem)(clbDepartments.Items[i])).Text.Equals(DeptName))
                    {                      
                        clbDepartments.SetSelected(i, true);
                        clbDepartments.SetItemChecked(i, true);
                    }
                }                      
           
               
            }
        }

        private void FillDepartmentData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT DISTINCT dept_name,dept_code FROM Dept_Mas ORDER BY dept_name";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Text = dataRow["dept_name"].ToString();
                        oclBox.Tag = dataRow["dept_code"].ToString();

                        clbDepartments.Items.Add(oclBox);


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

            if (txtNoOfPersons.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter No. Of Recruit Persons", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNoOfPersons.Focus();
            }
            else if (clbDepartments.CheckedItems.Count == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Department", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }

            //else if (txtRemarks.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter Remarks", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtRemarks.Focus();
            //}

            return flag;

        }

     

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtNoOfPersons.Text = "";
            txtRemarks.Text = "";


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {

                if (drs != null)
                {
                    ((ServiceActivities)objServiceActivities).dtActivityDetails.Rows.Remove(drs[0]);
                }


                for (int i = 0; i < clbDepartments.Items.Count; i++)
                {
                    if (clbDepartments.GetItemCheckState(i) == CheckState.Checked)
                    {
                        ((ServiceActivities)objServiceActivities).dtActivityDetails.Rows.Add(new Object[] {"-1","RECRUITMENT", "","","","","",((NewCheckboxListItem)(clbDepartments.Items[i])).Text.ToString(),
                                txtNoOfPersons.Text.ToString(),"","","RECRUITMENT", 
                               ((NewCheckboxListItem)(clbDepartments.Items[i])).Text.ToString(),txtRemarks.Text.ToString().Replace("\'",""),0,0,""});
                        ((ServiceActivities)objServiceActivities).GetActivityDetails();
                    }
                }


                this.Close();
            }
        }

        private void txtNoOfPersons_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

       
    }
}
