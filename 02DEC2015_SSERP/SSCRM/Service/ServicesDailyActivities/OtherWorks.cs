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
    public partial class OtherWorks : Form
    {
        SQLDB objSQLdb = null;
        public ServiceActivities objServiceActivities = null;
        DataRow[] drs;
        string sFrmType = "", sActivity = "";

        public OtherWorks(string frmType,string strActId)
        {
            InitializeComponent();
            sFrmType = frmType;
            sActivity = strActId;
        }
        public OtherWorks(DataRow[] dr,string frmType,string sActId)
        {
            InitializeComponent();
            drs = dr;
            sFrmType = frmType;
            sActivity = sActId;
        }

        private void OtherWorks_Load(object sender, EventArgs e)
        {

            if (sFrmType == "OTHERS")
            {
                lblActivity.Visible = true;
                txtActivityName.Visible = true;
            }
            else
            {
                lblActivity.Visible = false;
                txtActivityName.Visible = false;
            }
            if (drs != null)
            {
                txtDesc.Text = drs[0]["Purpose"].ToString();
                txtRemarks.Text = drs[0]["Remarks"].ToString();
            }
        }

        private bool CheckData()
        {
            bool flag = true;

            if (sFrmType == "OTHERS")
            {
                if (txtActivityName.Text.Length < 4)
                {
                    flag = false;
                    MessageBox.Show("Please Enter Activity Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtActivityName.Focus();
                    return flag;
                }
                if (txtActivityName.Text.Length > 30)
                {
                    flag = false;
                    MessageBox.Show("Activity Name Should Be less than 30 Characters", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtActivityName.Focus();
                    return flag;
                }
            }
            if (txtDesc.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Description", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDesc.Focus();
                return flag;
            }

            //if (txtRemarks.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter Remarks", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtRemarks.Focus();
            //    return flag;
           // }

            return flag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            string strCmd = "", ActivityName = "";
            DataTable dt=new DataTable();           
            int iRes = 0;
            if (CheckData() == true)
            {
                try
                {
                    if (drs != null)
                    {
                        ((ServiceActivities)objServiceActivities).dtActivityDetails.Rows.Remove(drs[0]);
                    }
                    if (sFrmType == "")
                    {
                        ((ServiceActivities)objServiceActivities).dtActivityDetails.Rows.Add(new Object[] { "-1",sActivity,
                        "","",txtDesc.Text.ToString().Replace("'"," "),"","","","","","",sActivity,
                        txtDesc.Text.ToString().Replace("'"," "), txtRemarks.Text.ToString().Replace("'",""),0,0,""});
                        ((ServiceActivities)objServiceActivities).GetActivityDetails();
                    }
                    else if (sFrmType == "OTHERS")
                    {
                        strCmd = "SELECT SOAM_ACTIVITY_DESC Activity FROM SERVICES_OTHER_ACTIVITY_MASTER " +
                                  " WHERE SOAM_ACTIVITY_DESC LIKE '%" + txtActivityName.Text + "%'";
                        dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                        strCmd = "";
                        if (dt.Rows.Count == 0)
                        {
                            strCmd = "INSERT INTO SERVICES_OTHER_ACTIVITY_MASTER(SOAM_ACTIVITY_NAME " +
                                                                              ", SOAM_ACTIVITY_DESC " +
                                                                              ")VALUES('" + txtActivityName.Text.ToString().Replace("'", "") +
                                                                              "','" + txtActivityName.Text.ToString().Replace("'", "") + "')";
                            if (strCmd.Length > 3)
                            {
                                iRes = objSQLdb.ExecuteSaveData(strCmd);
                            }


                            ((ServiceActivities)objServiceActivities).dtActivityDetails.Rows.Add(new Object[] { "-1",txtActivityName.Text.ToString().Replace("'", ""),
                        "","",txtDesc.Text.ToString().Replace("'"," "),"","","","","","",txtActivityName.Text.ToString().Replace("'", ""),
                        txtDesc.Text.ToString().Replace("'"," "), txtRemarks.Text.ToString().Replace("'",""),0,0,""});
                            ((ServiceActivities)objServiceActivities).GetActivityDetails();
                            ((ServiceActivities)objServiceActivities).FillActivityTypes();
                        }
                        else
                        {
                            MessageBox.Show("Activity Name Already Existed","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else if (sFrmType == "DOCUMENTATION")
                    {
                        ((ServiceActivities)objServiceActivities).dtActivityDetails.Rows.Add(new Object[] { "-1","DOCUMENTATION","","",txtDesc.Text.ToString().Replace("'"," ")
                    ,"","","","","","","DOCUMENTATION", "DOCUMENTATION"+'-'+ txtDesc.Text.ToString().Replace("'"," "),txtRemarks.Text.ToString().Replace("'",""),0,0,""});
                        ((ServiceActivities)objServiceActivities).GetActivityDetails();
                    }
                    else if (sFrmType == "SEMINORS")
                    {
                        ((ServiceActivities)objServiceActivities).dtActivityDetails.Rows.Add(new Object[] { "-1","SEMINORS AND EXHIBITIONS","","",txtDesc.Text.ToString().Replace("'"," ")
                    ,"","","","","","","SEMINORS AND EXHIBITIONS", "SEMINORS AND EXHIBITIONS"+'-'+ txtDesc.Text.ToString().Replace("'"," "),txtRemarks.Text.ToString().Replace("'",""),0,0,""});
                        ((ServiceActivities)objServiceActivities).GetActivityDetails();
                    }
                    else if (sFrmType == "ADV_VERIFICATION")
                    {
                        ((ServiceActivities)objServiceActivities).dtActivityDetails.Rows.Add(new Object[] { "-1","ADVANCE VERIFICATION","","",txtDesc.Text.ToString().Replace("'"," ")
                    ,"","","","","","","ADVANCE VERIFICATION", "ADVANCE VERIFICATION"+'-'+ txtDesc.Text.ToString().Replace("'"," "),txtRemarks.Text.ToString().Replace("'",""),0,0,""});
                        ((ServiceActivities)objServiceActivities).GetActivityDetails();
                    }

                    this.Close();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtRemarks.Text = "";
            txtDesc.Text = "";
            txtActivityName.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

   
      
    }
}
