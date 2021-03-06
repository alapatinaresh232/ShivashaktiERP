﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSTrans;
using SSAdmin;

namespace SSCRM
{
    public partial class VisitBranchDetails : Form
    {
        SQLDB objSQLdb = null;
        public ServiceActivities objServiceActivities;
        DataRow[] drs;
        

        public VisitBranchDetails()
        {
            InitializeComponent();
            
        }
        public VisitBranchDetails(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }

        private void VisitBranchDetails_Load(object sender, EventArgs e)
        {
            FillBranchData();
            if (drs != null)
            {
                        
                txtPurpose.Text = drs[0]["Purpose"].ToString();               
                txtRemarks.Text = drs[0]["Remarks"].ToString();       
                
                string BranName = drs[0]["BranchName"].ToString();

                for (int i = 0; i < clbBranches.Items.Count; i++)
                {
                    if (((NewCheckboxListItem)(clbBranches.Items[i])).Text.Equals(BranName))
                    {
                        clbBranches.SetSelected(i, true);
                        clbBranches.SetItemChecked(i, true);
                    }
                }

            }

        }

        private void FillBranchData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
           
            try
            {
                string strCommand = "SELECT BRANCH_NAME,BRANCH_CODE FROM BRANCH_MAS"+
                                    " WHERE COMPANY_CODE='" + CommonData.CompanyCode  +                                    
                                    "' AND BRANCH_TYPE IN('BR')  ORDER BY BRANCH_NAME ";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();                       
                        oclBox.Text = dataRow["BRANCH_NAME"].ToString();
                        oclBox.Tag = dataRow["BRANCH_CODE"].ToString();

                        clbBranches.Items.Add(oclBox);
                        

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

            if (txtPurpose.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Visit Purpose","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
                txtPurpose.Focus();
            }
            else if (clbBranches.CheckedItems.Count == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Atleast One Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                if (drs != null)
                {
                    ((ServiceActivities)objServiceActivities).dtActivityDetails.Rows.Remove(drs[0]);
                }



                for (int i = 0; i < clbBranches.Items.Count; i++)
                {
                    if (clbBranches.GetItemCheckState(i) == CheckState.Checked)
                    {
                        ((ServiceActivities)objServiceActivities).dtActivityDetails.Rows.Add(new Object[] { "-1","BR VISITS" ,((NewCheckboxListItem)(clbBranches.Items[i])).Tag.ToString(),
                            ((NewCheckboxListItem)(clbBranches.Items[i])).Text.ToString(), txtPurpose.Text.ToString().Replace("'"," "),"","","","","","","BRANCH VISITS", 
                                ((NewCheckboxListItem)(clbBranches.Items[i])).Text.ToString(), txtRemarks.Text.ToString().Replace("'"," "),0,0,""});
                        ((ServiceActivities)objServiceActivities).GetActivityDetails();
                    }
                }


                this.Close();
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtPurpose.Text = "";
            txtRemarks.Text = "";
        }    
       
       
    }
}
