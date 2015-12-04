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

namespace SSCRM
{
    public partial class frmFieldSupport : Form
    {
        SQLDB objSQLdb = null;
        public ServiceActivities objServiceActivities;
        DataRow[] drs;

        public frmFieldSupport()
        {
            InitializeComponent();
        }

        public frmFieldSupport(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }
       
        private void frmFieldSupport_Load(object sender, EventArgs e)
        {
            FillCampsData();

            if (drs != null)
            {
                cbCamps.Text = drs[0]["BranchName"].ToString();
                txtExecutiveEcode.Text = drs[0]["GCGLEcode"].ToString();
                GetEmployeeName();
                txtRemarks.Text = drs[0]["Remarks"].ToString();                
            }
        }


        private void FillCampsData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT DISTINCT(CM_CAMP_NAME) FROM CAMP_MAS ORDER BY CM_CAMP_NAME asc";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();

                    row[0] = "--Select--";
                    //row[1] = "--Select--";


                    dt.Rows.InsertAt(row, 0);

                    cbCamps.DataSource = dt;
                    cbCamps.DisplayMember = "CM_CAMP_NAME";
                    cbCamps.ValueMember = "CM_CAMP_NAME";
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

     
        private void GetEmployeeName()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();

            if (txtExecutiveEcode.Text.Length > 4)
            {
                try
                {
                    string strCmd = "SELECT MEMBER_NAME+'('+desig_name+')' EName FROM EORA_MASTER " +
                                    " INNER JOIN DESIG_MAS ON desig_code=DESG_ID " +
                                    " WHERE ECODE=" + txtExecutiveEcode.Text + "";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtExecutiveName.Text = dt.Rows[0]["EName"].ToString();
                    }
                    else
                    {
                        txtExecutiveName.Text = "";
                        MessageBox.Show("Please Enter Valid Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtExecutiveEcode.Focus();
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
            else
            {
                txtExecutiveName.Text = "";
            }


        }

      
        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbCamps.SelectedIndex = 0;
            txtExecutiveEcode.Text = "";
            txtRemarks.Text = "";
            txtExecutiveName.Text = "";

        }

        private void txtExecutiveEcode_Validated(object sender, EventArgs e)
        {
            GetEmployeeName();
        }

        private void txtExecutiveEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

        }

        private bool CheckData()
        {
            bool flag = true;

            if (cbCamps.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Camp Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbCamps.Focus();
                return flag;
            }

            if (txtExecutiveName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Executive Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtExecutiveEcode.Focus();
                return flag;
            }
                      

            return flag;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData()==true)
            {
                if (drs != null)
                {
                    ((ServiceActivities)objServiceActivities).dtActivityDetails.Rows.Remove(drs[0]);
                }

                ((ServiceActivities)objServiceActivities).dtActivityDetails.Rows.Add(new Object[] { "-1","FIELD SUPPORT",cbCamps.SelectedValue.ToString(),
                                cbCamps.Text.ToString(),"","","","","","","","FIELD SUPPORT", 
                                "Field Support Given To"+'-'+txtExecutiveName.Text+'('+txtExecutiveEcode.Text+')', txtRemarks.Text.ToString().Replace("'"," "),
                                txtExecutiveEcode.Text,0,"" });
                ((ServiceActivities)objServiceActivities).GetActivityDetails();


                this.Close();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }
    }
}
