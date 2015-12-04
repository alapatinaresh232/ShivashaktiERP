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
    public partial class InspectionDetails : Form
    {
        SQLDB objSQLDB;
        public InspectionDetails()
        {
            InitializeComponent();
        }
        private void InspectionDetails_Load(object sender, EventArgs e)
        {
            GetBindData();
            txtDocMonth.Text = CommonData.DocMonth;
            dtTrn.Value = Convert.ToDateTime(CommonData.CurrentDate);
        }
        public void GetBindData()
        {
            objSQLDB = new SQLDB();
            DataSet ds = objSQLDB.ExecuteDataSet("SELECT *FROM BRANCH_MAS WHERE BRANCH_TYPE='SP' AND ACTIVE='T'");
            UtilityLibrary.PopulateControl(cmbBranch, ds.Tables[0].DefaultView, 1, 0, "--PLEASE SELECT--", 0);
            objSQLDB = null;
        }
        public void GetMaxId()
        {
            if (cmbBranch.SelectedIndex > 0)
            {
                objSQLDB = new SQLDB();
                DataSet ds = objSQLDB.ExecuteDataSet("SELECT ISNULL(MAX(SPHT_TRN_NUMBER),0)+1 FROM SP_HEAD_TRN WHERE SPHT_CODE='" + cmbBranch.SelectedValue + "' AND SPHT_TRN_TYPE='INSPECTION'");
                objSQLDB = null;
                txtTrnNo_num.Text = ds.Tables[0].Rows[0][0].ToString();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBranch.SelectedIndex > 0)
            {
                objSQLDB = new SQLDB();
                DataSet ds = objSQLDB.ExecuteDataSet("SELECT COUNT(*) FROM SP_HEAD_MAS WHERE SPHM_CODE='" + cmbBranch.SelectedValue + "'");
                if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    MessageBox.Show("Please enter Stock point details", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                objSQLDB = null;
                GetMaxId();
                btnClear_Click(null, null);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string sqlStr = "";
            objSQLDB = new SQLDB();
            DataSet ds = objSQLDB.ExecuteDataSet("SELECT COUNT(*) FROM SP_HEAD_MAS WHERE SPHM_CODE='" + cmbBranch.SelectedValue + "'");
            if (ds.Tables[0].Rows[0][0].ToString() == "0")
            {
                MessageBox.Show("Please enter Stock point details", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (Check() == true)
            {
                DataSet dsExist = objSQLDB.ExecuteDataSet("SELECT COUNT(*) FROM SP_HEAD_TRN WHERE SPHT_CODE='" + cmbBranch.SelectedValue + "' AND SPHT_TRN_TYPE='INSPECTION' AND SPHT_TRN_NUMBER=" + txtTrnNo_num.Text);
                if (dsExist.Tables[0].Rows[0][0].ToString() == "0")
                {
                    GetMaxId();
                    sqlStr = "INSERT INTO SP_HEAD_TRN (SPHT_CODE,SPHT_TRN_DATE,SPHT_TRN_TYPE,SPHT_TRN_NUMBER,SPHT_SUBMITTED_BY_ECODE,SPHT_SUBMITTED_TO_NAME," +
                        "SPHT_SUBMITTED_TO_DESG,SPHT_SUBMITTED_TO_DEPT,SPHT_SUBMITTED_TO_ADDR1,SPHT_SUBMITTED_TO_ADDR2,SPHT_SUBMITTED_TO_CITY,SPHT_SUBMITTED_TO_STATE," +
                        "SPHT_REMARKS,SPHT_CREATED_BY,SPHT_AUTHORIZED_BY,SPHT_CREATED_DATE) VALUES ";
                    sqlStr += "('" + cmbBranch.SelectedValue + "','" + Convert.ToDateTime(dtTrn.Value).ToString("dd/MMM/yyyy") + "','INSPECTION'," + txtTrnNo_num.Text + "," + txtEcode_num.Text + ",'" + txtName.Text + "','" + txtDesigIn.Text +
                        "','" + txtDepartment.Text + "','" + txtAddress.Text + "','" + txtAddress1.Text + "','" + txtCity.Text + "','" + txtState.Text + "','" + txtRemarks.Text + "','" + CommonData.LogUserId +
                        "','" + CommonData.LogUserId + "','" + CommonData.CurrentDate + "')";
                }
                else
                {
                    sqlStr = " UPDATE SP_HEAD_TRN SET SPHT_TRN_DATE='" + Convert.ToDateTime(dtTrn.Value).ToString("dd/MMM/yyyy") + "',SPHT_SUBMITTED_BY_ECODE=" + txtEcode_num.Text +
                        ",SPHT_SUBMITTED_TO_NAME='" + txtName.Text + "',SPHT_SUBMITTED_TO_DESG='" + txtDesigIn.Text + "',SPHT_SUBMITTED_TO_DEPT='" + txtDepartment.Text +
                        "',SPHT_SUBMITTED_TO_ADDR1='" + txtAddress.Text + "',SPHT_SUBMITTED_TO_ADDR2='" + txtAddress1.Text + "',SPHT_SUBMITTED_TO_CITY='" + txtCity.Text +
                        "',SPHT_SUBMITTED_TO_STATE='" + txtState.Text + "',SPHT_REMARKS='" + txtRemarks.Text + "',SPHT_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                        "',SPHT_LAST_MODIFIED_DATE='" + CommonData.CurrentDate + "' WHERE SPHT_CODE='" + cmbBranch.SelectedValue + "' AND SPHT_TRN_TYPE='INSPECTION' AND SPHT_TRN_NUMBER=" + txtTrnNo_num.Text;
                }
                objSQLDB = new SQLDB();
                int iretval = objSQLDB.ExecuteSaveData(sqlStr);
                objSQLDB = null;
                if (iretval > 0)
                    MessageBox.Show("Inserted Data Successfully", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Data not Inserted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnClear_Click(null, null);
            }
        }
        public bool Check()
        {
            bool ibool = true;
            if (txtEcode_num.Text.Trim() == "")
            {
                MessageBox.Show("Please enter Ecode", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ibool = false;
            }
            else if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("Please enter Inspecter Name", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ibool = false;
            }
            else if (txtDesig.Text.Trim() == "")
            {
                MessageBox.Show("Please enter Designation", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ibool = false;
            }
            else if (txtDepartment.Text.Trim() == "")
            {
                MessageBox.Show("Please enter Department", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ibool = false;
            }
            else if ((txtAddress.Text.Trim() == "") && (txtAddress1.Text.Trim() == "") && (txtCity.Text.Trim() == "") && (txtState.Text.Trim() == ""))
            {
                MessageBox.Show("Please enter Address", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ibool = false;
            }
            else if (txtRemarks.Text.Trim() == "")
            {
                MessageBox.Show("Please enter Remarks", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ibool = false;
            }
            return ibool;
        }
        private void txtEcode_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcode_num.Text != "")
            {
                objSQLDB = new SQLDB();
                DataSet ds = objSQLDB.ExecuteDataSet("SELECT * FROM EORA_MASTER WHERE ECODE=" + txtEcode_num.Text);
                objSQLDB = null;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtEName.Text = ds.Tables[0].Rows[0]["MEMBER_NAME"].ToString();
                    txtDesig.Text = ds.Tables[0].Rows[0]["DESIG"].ToString();
                }
                else
                {
                    txtEName.Text = "";
                    txtDesig.Text = "";
                }
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            GetMaxId();
            txtEcode_num.Text = "";
            txtEName.Text = "";
            txtDesig.Text = "";
            txtDepartment.Text = "";
            txtAddress.Text = "";
            txtAddress1.Text = "";
            txtCity.Text = "";
            txtState.Text = "";
            txtRemarks.Text = "";
            txtName.Text = "";
            txtDesigIn.Text = "";
        }
        private void txtTrnNo_Validated(object sender, EventArgs e)
        {
            if (txtTrnNo_num.Text != "")
            {
                objSQLDB = new SQLDB();
                DataSet dsExist = objSQLDB.ExecuteDataSet("SELECT * FROM SP_HEAD_TRN WHERE SPHT_CODE='" + cmbBranch.SelectedValue + "' AND SPHT_TRN_TYPE='INSPECTION' AND SPHT_TRN_NUMBER=" + txtTrnNo_num.Text);
                objSQLDB = null;
                if (dsExist.Tables[0].Rows.Count > 0)
                {
                    dtTrn.Value = Convert.ToDateTime(dsExist.Tables[0].Rows[0]["SPHT_TRN_DATE"]);
                    txtEcode_num.Text = dsExist.Tables[0].Rows[0]["SPHT_SUBMITTED_BY_ECODE"].ToString();
                    txtEcode_KeyUp(null, null);
                    txtName.Text = dsExist.Tables[0].Rows[0]["SPHT_SUBMITTED_TO_NAME"].ToString();
                    txtDesigIn.Text = dsExist.Tables[0].Rows[0]["SPHT_SUBMITTED_TO_DESG"].ToString();
                    txtDepartment.Text = dsExist.Tables[0].Rows[0]["SPHT_SUBMITTED_TO_DEPT"].ToString();
                    txtAddress.Text = dsExist.Tables[0].Rows[0]["SPHT_SUBMITTED_TO_ADDR1"].ToString();
                    txtAddress1.Text = dsExist.Tables[0].Rows[0]["SPHT_SUBMITTED_TO_ADDR2"].ToString();
                    txtCity.Text = dsExist.Tables[0].Rows[0]["SPHT_SUBMITTED_TO_CITY"].ToString();
                    txtState.Text = dsExist.Tables[0].Rows[0]["SPHT_SUBMITTED_TO_STATE"].ToString();
                    txtRemarks.Text = dsExist.Tables[0].Rows[0]["SPHT_REMARKS"].ToString();
                }
                else
                {
                    txtName.Text = "";
                    txtDesigIn.Text = "";
                    txtEcode_num.Text = "";
                    txtEName.Text = "";
                    txtDesig.Text = "";
                    txtDepartment.Text = "";
                    txtAddress.Text = "";
                    txtAddress1.Text = "";
                    txtCity.Text = "";
                    txtState.Text = "";
                    txtRemarks.Text = "";
                }
            }
        }
        private void txtTrnNo_num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }
        private void txtEcode_num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }
    }
}
