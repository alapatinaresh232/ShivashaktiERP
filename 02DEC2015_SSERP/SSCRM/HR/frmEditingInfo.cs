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
using SSCRM.App_Code;

namespace SSCRM
{
    public partial class frmEditingInfo : Form
    {
        public SQLDB objSQLDB;
        public DataTable dt;
        public frmEditingInfo()
        {
            InitializeComponent();
        }
        private void frmEditingInfo_Load(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            rbtAgent.Checked = true;
        }


        private void txtNominee_num_TextChanged(object sender, EventArgs e)
        {
            if (!SSCRM.App_Code.UtilityLibrary.CustomValidate(grouper1, toolTip1))
                return;            
            if (txtEoraCode_num.Text != "")
            {
                objSQLDB = new SQLDB();
                string sCheckval = "";
                if (rbtEmployee.Checked == true)
                    sCheckval = "E";
                else
                    sCheckval = "A";
                dt = objSQLDB.ExecuteDataSet("SELECT *FROM HR_APPL_MASTER_HEAD WHERE HAMH_EORA_TYPE='" + sCheckval + "' AND HAMH_EORA_CODE=" + txtEoraCode_num.Text).Tables[0];
                if (dt.Rows.Count == 1)
                {
                    lblResult.ForeColor = Color.Green;
                    lblResult.Text = "Record Exist";
                    btnEdit.Enabled = true;
                }
                else
                {
                    lblResult.Text = "This record is not exist";
                    lblResult.ForeColor = Color.Red;
                    btnEdit.Enabled = false;
                }
            }
            else
            {
                lblResult.Text = "This record is not exist";
                btnEdit.Enabled = false;
            }
            objSQLDB = null;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!SSCRM.App_Code.UtilityLibrary.CustomValidate(grouper1, toolTip1))
                return;   
            if (dt.Rows.Count == 1)
            {
                //frmApplication frmApplication = new frmApplication(dt.Rows[0]["HAMH_COMPANY_CODE"].ToString(), dt.Rows[0]["HAMH_BRANCH_CODE"].ToString(), dt.Rows[0]["HAMH_APPL_NUMBER"].ToString(), dt.Rows[0]["HAMH_EORA_TYPE"].ToString());
                frmApplication frmApplication = new frmApplication(dt.Rows[0]["HAMH_COMPANY_CODE"].ToString(), dt.Rows[0]["HAMH_BRANCH_CODE"].ToString(), dt.Rows[0]["HAMH_APPL_NUMBER"].ToString(), "Edit");
                frmApplication.Show();
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbtEmployee_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtEmployee.Checked == true)
                rbtAgent.Checked = false;
            txtNominee_num_TextChanged(null, null);
        }

        private void rbtAgent_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtAgent.Checked == true)
                rbtEmployee.Checked = false;
            txtNominee_num_TextChanged(null, null);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            //objSQLDB = new SQLDB();
            //SqlParameter[] param = new SqlParameter[1];
            //DataSet ds = new DataSet();
            //try
            //{
            //    param[0] = objSQLDB.CreateParameter("@ApplicationNo", DbType.Int32, dt.Rows[0]["HAMH_APPL_NUMBER"].ToString(), ParameterDirection.Input);
            //    ds = objSQLDB.ExecuteDataSet("GetHR_Appl_Details", CommandType.StoredProcedure, param);
            //    ds.Tables[0].TableName = "HR_APPL_MASTER_HEAD";
            //    ds.Tables[1].TableName = "HR_APPL_CHECK";
            //    ds.Tables[2].TableName = "HR_APPL_ECA_DETL";
            //    ds.Tables[3].TableName = "HR_APPL_EDU_DETL";
            //    ds.Tables[4].TableName = "HR_APPL_EXP_DETL";
            //    ds.Tables[5].TableName = "HR_APPL_REF_DETL";
            //    ds.Tables[6].TableName = "HR_APPL_FAMILY_DETL";
            //    ds.Tables[7].TableName = "HR_APPL_SHORT_COURSE_DETL";
            //    ds.Tables[8].TableName = "HR_APPL_APPOINTMENT";
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.ToString());
            //}
            //finally
            //{
            //    param = null;
            //    objSQLDB = null;
            //}
            //ReportViewer chlRV = new ReportViewer(ds);
            //CommonData.ViewReport = "HRDetails";
            //chlRV.Show();           
        }
    }
}
