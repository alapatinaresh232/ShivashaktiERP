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
    public partial class frmOsCollection : Form
    {
        SQLDB objSQLdb = null;
        public ServiceActivities objServiceActivities;
        DataRow[] drs;

        public frmOsCollection()
        {
            InitializeComponent();
        }
        public frmOsCollection(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }

        private void frmOsCollection_Load(object sender, EventArgs e)
        {
            FillCampsData();

            if (drs != null)
            {               
                cbCamps.Text = drs[0]["BranchName"].ToString();
                txtGcGlEcode.Text = drs[0]["GCGLEcode"].ToString();
                GetEmployeeName();
                txtRemarks.Text = drs[0]["Remarks"].ToString();
                txtOSAmt.Text = drs[0]["Amount"].ToString();
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


        private void txtOSAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

        }

        private void txtGcGlEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
       
        private void GetEmployeeName()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();

            if (txtGcGlEcode.Text.Length > 4)
            {
                try
                {
                    string strCmd = "SELECT MEMBER_NAME+'('+desig_name+')' EName FROM EORA_MASTER " +
                                    " INNER JOIN DESIG_MAS ON desig_code=DESG_ID " +
                                    " WHERE ECODE=" + txtGcGlEcode.Text + "";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtGcGlName.Text = dt.Rows[0]["EName"].ToString();
                    }
                    else
                    {
                        txtGcGlName.Text = "";
                        MessageBox.Show("Please Enter Valid Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtGcGlEcode.Focus();
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
                txtGcGlName.Text = "";
            }


        }

        private void txtGcGlEcode_Validated(object sender, EventArgs e)
        {
            GetEmployeeName();
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

            if (txtGcGlName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Gc Or GL Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtGcGlEcode.Focus();
                return flag;
            }

            if (txtOSAmt.Text.Length == 0 || txtOSAmt.Text.Equals("0"))
            {
                flag = false;
                MessageBox.Show("Please Enter Collected OutStanding Amount ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtOSAmt.Focus();
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
                
                ((ServiceActivities)objServiceActivities).dtActivityDetails.Rows.Add(new Object[] { "-1","OS COLLECTION",cbCamps.SelectedValue.ToString(),
                                cbCamps.Text.ToString(),"","","","","","","","OS COLLECTION", 
                                txtOSAmt.Text+' '+"Amount Collected From"+'-'+txtGcGlName.Text+'-'+txtGcGlEcode.Text, txtRemarks.Text.ToString().Replace("'"," "),
                                txtGcGlEcode.Text,txtOSAmt.Text,"" });
                ((ServiceActivities)objServiceActivities).GetActivityDetails();


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
            cbCamps.SelectedIndex = 0;
            txtGcGlEcode.Text = "";
            txtGcGlName.Text = "";
            txtRemarks.Text = "";
            txtOSAmt.Text = "";
        }

      
    }
}
