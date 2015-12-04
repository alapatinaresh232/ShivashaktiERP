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

namespace SSCRM
{
    public partial class ESIMaster : Form
    {
        string strDOJ="";
        SQLDB objDB = null;
        bool flagUpdate = false;
        int iID = 0;
        public ESIMaster()
        {
            InitializeComponent();
        }

        private void ESIMaster_Load(object sender, EventArgs e)
        {

        }

        private void txtEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtEcodeSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtEcodeSearch.Text.Length > 0)
            {

                try
                {
                    objDB = new SQLDB();
                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = objDB.CreateParameter("@EORACODE", DbType.String, txtEcodeSearch.Text.ToString(), ParameterDirection.Input);

                    DataTable dt = objDB.ExecuteDataSet("GetEmpDataforEORAMas", CommandType.StoredProcedure, param).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtName.Text = dt.Rows[0]["MEMBER_NAME"].ToString();
                        txtName.Tag = dt.Rows[0]["ApplNo"].ToString();
                        txtDesig.Text = dt.Rows[0]["DESIG_NAME"].ToString();
                        txtDesig.Tag = dt.Rows[0]["DESIG_ID"].ToString();
                        txtCompany.Text = dt.Rows[0]["COMPANY_NAME"].ToString();
                        txtCompany.Tag = dt.Rows[0]["COMPANY_CODE"].ToString();
                        txtBranch.Text = dt.Rows[0]["BRANCH_NAME"].ToString();
                        txtBranch.Tag = dt.Rows[0]["BRANCH_CODE"].ToString();
                        txtDept.Text = dt.Rows[0]["Dept_Name"].ToString();
                        txtDept.Tag = dt.Rows[0]["Dept_Code"].ToString();
                        strDOJ = dt.Rows[0]["DOJ"].ToString();
                        FillToGrid();

                    }
                    else
                    {
                        txtName.Text = "";
                        txtDesig.Text = "";
                        txtDesig.Tag = "";
                        txtCompany.Text = "";
                        txtCompany.Tag = "";
                        txtBranch.Text = "";
                        txtBranch.Tag = "";
                        txtDept.Text = "";
                        txtDept.Tag = "";
                        strDOJ = "";
                        gvEmpDetails.Rows.Clear();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                txtName.Text = "";
                txtDesig.Text = "";
                txtDesig.Tag = "";
                txtCompany.Text = "";
                txtCompany.Tag = "";
                txtBranch.Text = "";
                txtBranch.Tag = "";
                txtDept.Text = "";
                txtDept.Tag = "";
                strDOJ = "";
                gvEmpDetails.Rows.Clear();
            }
        }
        private void FillToGrid()
        {
            try
            {
                string strCMD = "select HR_ESI_ACC_MAS.*,member_name from HR_ESI_ACC_MAS  inner join eora_master on ecode=HEAM_EORA_CODE where HEAM_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) + " order by HEAM_EFF_DATE";
                objDB = new SQLDB();
                DataTable dt = objDB.ExecuteDataSet(strCMD).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    gvEmpDetails.Rows.Clear();
                    for (int iVar = 0; iVar < dt.Rows.Count; iVar++)
                    {
                        gvEmpDetails.Rows.Add();
                        gvEmpDetails.Rows[iVar].Cells["SLNO"].Value = (iVar + 1).ToString();
                        gvEmpDetails.Rows[iVar].Cells["id"].Value = dt.Rows[iVar]["HEAM_ID"].ToString();
                        gvEmpDetails.Rows[iVar].Cells["Ecode"].Value = dt.Rows[iVar]["HEAM_EORA_CODE"].ToString();
                        gvEmpDetails.Rows[iVar].Cells["Names"].Value = dt.Rows[iVar]["member_name"].ToString();
                        gvEmpDetails.Rows[iVar].Cells["PF_NUMBER"].Value = dt.Rows[iVar]["HEAM_ESI_ACC_NO"].ToString();
                        gvEmpDetails.Rows[iVar].Cells["From"].Value = Convert.ToDateTime(dt.Rows[iVar]["HEAM_EFF_DATE"].ToString()).ToString("dd/MMM/yyyy");
                        gvEmpDetails.Rows[iVar].Cells["To"].Value = dt.Rows[iVar]["HEAM_STATUS"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEcodeSearch.Text = "";
            txtPFNumber.Text = "";
            gvEmpDetails.Rows.Clear();
        }

        private void gvEmpDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (gvEmpDetails.Rows[e.RowIndex].Cells["Edit"].Value.ToString().Trim() != "")
                {
                    if (Convert.ToBoolean(gvEmpDetails.Rows[e.RowIndex].Cells["Edit"].Selected) == true)
                    {
                        flagUpdate = true;
                        txtEcodeSearch.Text = gvEmpDetails.Rows[e.RowIndex].Cells["Ecode"].Value.ToString();
                        iID = Convert.ToInt32(gvEmpDetails.Rows[e.RowIndex].Cells["id"].Value.ToString());
                        txtPFNumber.Text = gvEmpDetails.Rows[e.RowIndex].Cells["PF_NUMBER"].Value.ToString();
                        dtpTo.Value = Convert.ToDateTime(gvEmpDetails.Rows[e.RowIndex].Cells["From"].Value.ToString());
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                try
                {
                    int iRes = 0;
                    string strCMD = "";
                    if (flagUpdate == false)
                    {
                        strCMD = "INSERT INTO HR_ESI_ACC_MAS(HEAM_APPL_NO,HEAM_EORA_CODE,HEAM_ESI_ACC_NO,HEAM_EFF_DATE,HEAM_CREATED_BY,HEAM_CREATED_DATE) VALUES" +
                                        "(" + txtName.Tag + "," + txtEcodeSearch.Text + ",'" + txtPFNumber.Text.Trim().Replace(" ", "") + "','" + dtpTo.Value.ToString("dd/MMM/yyyy") + "','" + CommonData.LogUserId + "',getdate())";

                    }
                    else
                    {
                        strCMD = " update HR_ESI_ACC_MAS set  HEAM_APPL_NO =" + txtName.Tag + ",HEAM_EORA_CODE=" + txtEcodeSearch.Text + ", HEAM_ESI_ACC_NO= '" + txtPFNumber.Text.Trim().Replace(" ", "") + "',HPAM_EFF_DATE='" + dtpTo.Value.ToString("dd/MMM/yyyy") +
                            "',HEAM_MODIFIED_BY='" + CommonData.LogUserId + "', HEAM_MODIFIED_DATE=getdate() where HEAM_ID=" + iID;
                    }
                    objDB = new SQLDB();
                    iRes = objDB.ExecuteSaveData(strCMD);
                    if (iRes > 0)
                    {
                        MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        strCMD = " UPDATE HR_ESI_ACC_MAS SET HEAM_STATUS='RUNNING' WHERE HEAM_EFF_DATE= (SELECT MAX(HEAM_EFF_DATE) FROM HR_ESI_ACC_MAS where HEAM_EORA_CODE=" + txtEcodeSearch.Text + ") and HEAM_EORA_CODE=" + txtEcodeSearch.Text + " " +
                                    " UPDATE HR_ESI_ACC_MAS SET HEAM_STATUS='CLOSED' WHERE HEAM_EFF_DATE < (SELECT MAX(HEAM_EFF_DATE) FROM HR_ESI_ACC_MAS where HEAM_EORA_CODE=" + txtEcodeSearch.Text + ")and HEAM_EORA_CODE=" + txtEcodeSearch.Text + " ";
                        objDB = new SQLDB();
                        objDB.ExecuteSaveData(strCMD);
                        txtPFNumber.Text = "";
                        FillToGrid();
                        flagUpdate = false;
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
            bool flag = true;
            if (txtName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Valid Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEcodeSearch.Focus();
            }
            if (txtPFNumber.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Valid ESINumber", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPFNumber.Focus();
            }
            return flag;
        }

        private void txtPFNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
            }
        }

      
    }
}
