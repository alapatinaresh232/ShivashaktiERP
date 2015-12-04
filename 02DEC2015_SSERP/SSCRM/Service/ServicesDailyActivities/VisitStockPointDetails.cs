using System;
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
    public partial class VisitStockPointDetails : Form
    {
        SQLDB objSQLdb = null;
        public ServiceActivities objServiceActivities;
        DataRow[] drs;


        public VisitStockPointDetails()
        {
            InitializeComponent();
        }
        public VisitStockPointDetails(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }

        private void VisitStockPointDetails_Load(object sender, EventArgs e)
        {
            FillBranchData();

            if (drs != null)
            {               
                txtPurpose.Text = drs[0]["Purpose"].ToString();
                txtRemarks.Text = drs[0]["Remarks"].ToString();
                string BranName = drs[0]["BranchName"].ToString();
                for (int i = 0; i < clbStockPoints.Items.Count; i++)
                {
                    if (((NewCheckboxListItem)(clbStockPoints.Items[i])).Text.Equals(BranName))
                    {
                        clbStockPoints.SetSelected(i, true);
                        clbStockPoints.SetItemChecked(i, true);
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
                string strCommand = "SELECT BRANCH_NAME,BRANCH_CODE FROM BRANCH_MAS" +
                                    " WHERE COMPANY_CODE='" + CommonData.CompanyCode +                                   
                                    "' AND  BRANCH_TYPE IN('SP')  ORDER BY BRANCH_NAME ";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Text = dataRow["BRANCH_NAME"].ToString();
                        oclBox.Tag = dataRow["BRANCH_CODE"].ToString();

                        clbStockPoints.Items.Add(oclBox);
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
                MessageBox.Show("Please Enter Visit Purpose", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPurpose.Focus();
            }
            else if (clbStockPoints.CheckedItems.Count == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }

            else if (txtRemarks.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Remarks", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRemarks.Focus();
            }

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

                for (int i = 0; i < clbStockPoints.Items.Count; i++)
                {
                    if (clbStockPoints.GetItemCheckState(i) == CheckState.Checked)
                    {
                        ((ServiceActivities)objServiceActivities).dtActivityDetails.Rows.Add(new Object[] { "-1","SP VISITS",((NewCheckboxListItem)(clbStockPoints.Items[i])).Tag.ToString(),
                                ((NewCheckboxListItem)(clbStockPoints.Items[i])).Text.ToString(), txtPurpose.Text.ToString().Replace("'"," "),"","","","","","","STOCK POINT VISITS", 
                                ((NewCheckboxListItem)(clbStockPoints.Items[i])).Text.ToString(), txtRemarks.Text.ToString().Replace("'"," "),0,0,""});
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
            txtPurpose.Text = string.Empty;
            txtRemarks.Text = string.Empty;
           
        }

      
    }
}
