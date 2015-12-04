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
    public partial class PfMaster : Form
    {
        SQLDB objDB = null;
        string strDOJ = "";
        bool flagUpdate = false;
        int iID = 0;
        public PfMaster()
        {
            InitializeComponent();
        }

        private void PfMaster_Load(object sender, EventArgs e)
        {

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

        private void txtEcodeSearch_TextChanged(object sender, EventArgs e)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(CheckData())
            {
                try
                {
                    int iRes = 0;
                    string strCMD = "";
                    try
                    {
                        Convert.ToInt32(txtPfBasic.Text);
                    }
                    catch
                    {
                        txtPfBasic.Text = "0";
                    }
                    if (flagUpdate == false)
                    {
                        strCMD = "INSERT INTO HR_PF_ACC_MAS(HPAM_APPL_NO"+
                                        ",HPAM_EORA_CODE,HPAM_PF_ACC_NO"+
                                        ",HPAM_EFF_DATE,HPAM_CREATED_BY"+
                                        ",HPAM_CREATED_DATE,HPAM_PF_BASIC) VALUES" +
                                        "(" + txtName.Tag + "," + txtEcodeSearch.Text + 
                                        ",'" + txtPFNumber.Text.Trim().Replace(" ", "") + 
                                        "','" + dtpTo.Value.ToString("dd/MMM/yyyy") + 
                                        "','" + CommonData.LogUserId + "',getdate(),"+txtPfBasic.Text+")";

                    }
                    else
                    {
                        strCMD = " update HR_PF_ACC_MAS set  HPAM_APPL_NO =" + txtName.Tag + 
                            ",HPAM_EORA_CODE =" + txtEcodeSearch.Text + 
                            ", HPAM_PF_ACC_NO= '" + txtPFNumber.Text.Trim().Replace(" ", "") + 
                            "',HPAM_EFF_DATE='"+dtpTo.Value.ToString("dd/MMM/yyyy")+
                            "',HPAM_MODIFIED_BY='" + CommonData.LogUserId + 
                            "', HPAM_MODIFIED_DATE=getdate(), HPAM_PF_BASIC=" + txtPfBasic.Text + 
                            " where HPAM_ID=" + iID;
                    }
                    objDB = new SQLDB();
                    iRes = objDB.ExecuteSaveData(strCMD);
                    if (iRes > 0)
                    {
                        MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        strCMD = " UPDATE HR_PF_ACC_MAS SET HPAM_STATUS='RUNNING' WHERE HPAM_EFF_DATE= (SELECT MAX(HPAM_EFF_DATE) FROM HR_PF_ACC_MAS where HPAM_EORA_CODE=" + txtEcodeSearch.Text + ") and HPAM_EORA_CODE=" + txtEcodeSearch.Text + " " +
                                    " UPDATE HR_PF_ACC_MAS SET HPAM_STATUS='CLOSED' WHERE HPAM_EFF_DATE < (SELECT MAX(HPAM_EFF_DATE) FROM HR_PF_ACC_MAS where HPAM_EORA_CODE=" + txtEcodeSearch.Text + ") and HPAM_EORA_CODE=" + txtEcodeSearch.Text + " ";
                        objDB = new SQLDB();
                        objDB.ExecuteSaveData(strCMD);
                        txtPFNumber.Text = "AP/KKP/";
                        FillToGrid();
                        flagUpdate = false;
                    }

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        private void FillToGrid()
        {
            try
            {
                string strCMD = "select HR_PF_ACC_MAS.*,member_name from HR_PF_ACC_MAS  inner join eora_master on ecode=HPAM_EORA_CODE where HPAM_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) + " order by HPAM_EFF_DATE";
                 objDB = new SQLDB();
                 DataTable dt = objDB.ExecuteDataSet(strCMD).Tables[0];
                if(dt.Rows.Count>0)
                {
                    gvEmpDetails.Rows.Clear();
                    for (int iVar = 0; iVar < dt.Rows.Count; iVar++)
                    {
                        gvEmpDetails.Rows.Add();
                        gvEmpDetails.Rows[iVar].Cells["SLNO"].Value = (iVar + 1).ToString();
                        gvEmpDetails.Rows[iVar].Cells["id"].Value = dt.Rows[iVar]["HPAM_ID"].ToString();
                        gvEmpDetails.Rows[iVar].Cells["Ecode"].Value = dt.Rows[iVar]["HPAM_EORA_CODE"].ToString();
                        gvEmpDetails.Rows[iVar].Cells["Names"].Value = dt.Rows[iVar]["member_name"].ToString();
                        gvEmpDetails.Rows[iVar].Cells["PF_NUMBER"].Value = dt.Rows[iVar]["HPAM_PF_ACC_NO"].ToString();
                        gvEmpDetails.Rows[iVar].Cells["PFBasic"].Value = dt.Rows[iVar]["HPAM_PF_BASIC"].ToString();
                        gvEmpDetails.Rows[iVar].Cells["From"].Value = Convert.ToDateTime( dt.Rows[iVar]["HPAM_EFF_DATE"].ToString()).ToString("dd/MMM/yyyy");
                        gvEmpDetails.Rows[iVar].Cells["To"].Value = dt.Rows[iVar]["HPAM_STATUS"].ToString();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
            if(txtPFNumber.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Valid PFNumber", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPFNumber.Focus();
            }
            return flag;
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
                        iID = Convert.ToInt32( gvEmpDetails.Rows[e.RowIndex].Cells["id"].Value.ToString());
                        txtPFNumber.Text = gvEmpDetails.Rows[e.RowIndex].Cells["PF_NUMBER"].Value.ToString();
                        txtPfBasic.Text = gvEmpDetails.Rows[e.RowIndex].Cells["PFBasic"].Value.ToString();
                        dtpTo.Value = Convert.ToDateTime( gvEmpDetails.Rows[e.RowIndex].Cells["From"].Value.ToString());
                    }
                }
            }
        }

        private void txtPFNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text.Length > 4)
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

       
       


       
    }
}
