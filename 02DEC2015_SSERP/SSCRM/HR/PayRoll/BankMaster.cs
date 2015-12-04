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
    public partial class BankMaster : Form
    {
        string strDOJ = "";
        SQLDB objDB = null;
        bool flagUpdate = false;
        int iID = 0;
        
        public BankMaster()
        {
            InitializeComponent();
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
                string strCMD = "select HR_BANK_ACC_MAS.*,member_name,BANK_IFSC_MICR_MAS.* from HR_BANK_ACC_MAS  " +
                                    "inner join eora_master on ecode=HBAM_EORA_CODE "+
                                    "left join BANK_IFSC_MICR_MAS on BIM_BANK_IFSC_CODE=HBAM_IFSC_CODE " +
                                    "where HBAM_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) + " order by HBAM_EFF_DATE";
                objDB = new SQLDB();
                DataTable dt = objDB.ExecuteDataSet(strCMD).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    gvEmpDetails.Rows.Clear();
                    for (int iVar = 0; iVar < dt.Rows.Count; iVar++)
                    {
                        gvEmpDetails.Rows.Add();
                        gvEmpDetails.Rows[iVar].Cells["SLNO"].Value = (iVar + 1).ToString();
                        gvEmpDetails.Rows[iVar].Cells["id"].Value = dt.Rows[iVar]["HBAM_ID"].ToString();
                        gvEmpDetails.Rows[iVar].Cells["Ecode"].Value = dt.Rows[iVar]["HBAM_EORA_CODE"].ToString();
                        gvEmpDetails.Rows[iVar].Cells["Names"].Value = dt.Rows[iVar]["member_name"].ToString();
                        gvEmpDetails.Rows[iVar].Cells["PF_NUMBER"].Value = dt.Rows[iVar]["HBAM_BANK_ACC_NO"].ToString();
                        gvEmpDetails.Rows[iVar].Cells["IFSC"].Value = dt.Rows[iVar]["HBAM_IFSC_CODE"].ToString();
                        gvEmpDetails.Rows[iVar].Cells["BankName"].Value = dt.Rows[iVar]["HBAM_BANK_NAME"].ToString();
                        gvEmpDetails.Rows[iVar].Cells["From"].Value = Convert.ToDateTime(dt.Rows[iVar]["HBAM_EFF_DATE"].ToString()).ToString("dd/MMM/yyyy");
                        gvEmpDetails.Rows[iVar].Cells["To"].Value = dt.Rows[iVar]["HBAM_STATUS"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEcodeSearch.Text = "";
            txtPFNumber.Text = "";
            txtBankName.Text = "";
            gvEmpDetails.Rows.Clear();
            txtBankName.Text = "";
            txtState.Text = "";
            txtDistrict.Text = "";
            txtCity.Text = "";
            txtBankBranch.Text = "";
            txtAddress.Text = "";
            txtIFSC.Text = "";
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
                        txtBankName.Text = gvEmpDetails.Rows[e.RowIndex].Cells["BankName"].Value.ToString();
                        dtpTo.Value = Convert.ToDateTime(gvEmpDetails.Rows[e.RowIndex].Cells["From"].Value.ToString());
                        txtIFSC.Text = gvEmpDetails.Rows[e.RowIndex].Cells["IFSC"].Value.ToString();
                        txtIFSC_Validated(null, null);
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
                        strCMD = "INSERT INTO HR_BANK_ACC_MAS(HBAM_APPL_NO, HBAM_EORA_CODE, HBAM_BANK_ACC_NO, "+
                                    "HBAM_BANK_NAME, HBAM_EFF_DATE, HBAM_CREATED_BY, HBAM_CREATED_DATE, HBAM_IFSC_CODE) VALUES" +
                                    "(" + txtName.Tag + "," + txtEcodeSearch.Text + 
                                    ",'" + txtPFNumber.Text.Trim().Replace(" ", "") + "','"+txtBankName.Text.Trim()+
                                    "','" + dtpTo.Value.ToString("dd/MMM/yyyy") + "','" + CommonData.LogUserId + 
                                    "',getdate(),'"+txtIFSC.Text.Replace(" ","").ToUpper()+"')";

                    }
                    else
                    {
                        strCMD = " update HR_BANK_ACC_MAS set  HBAM_APPL_NO =" + txtName.Tag + 
                                    ",HBAM_EORA_CODE =" + txtEcodeSearch.Text + 
                                    ", HBAM_BANK_ACC_NO= '" + txtPFNumber.Text.Trim().Replace(" ", "") +
                                    "',HBAM_BANK_NAME='"+txtBankName.Text.Trim()+
                                    "',HBAM_EFF_DATE='" + dtpTo.Value.ToString("dd/MMM/yyyy") +
                                    "',HBAM_MODIFIED_BY='" + CommonData.LogUserId +
                                    "',HBAM_IFSC_CODE='" + txtIFSC.Text.Replace(" ", "").ToUpper() + 
                                    "', HBAM_MODIFIED_DATE=getdate() where HBAM_ID=" + iID;
                    }
                    objDB = new SQLDB();
                    iRes = objDB.ExecuteSaveData(strCMD);
                    if (iRes > 0)
                    {
                        MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        strCMD = " UPDATE HR_BANK_ACC_MAS SET HBAM_STATUS='RUNNING' WHERE HBAM_EFF_DATE= (SELECT MAX(HBAM_EFF_DATE) FROM HR_BANK_ACC_MAS where HBAM_EORA_CODE=" + txtEcodeSearch.Text + ") and HBAM_EORA_CODE=" + txtEcodeSearch.Text + " " +
                                    " UPDATE HR_BANK_ACC_MAS SET HBAM_STATUS='CLOSED' WHERE HBAM_EFF_DATE < (SELECT MAX(HBAM_EFF_DATE) FROM HR_BANK_ACC_MAS where HBAM_EORA_CODE=" + txtEcodeSearch.Text + ") and HBAM_EORA_CODE=" + txtEcodeSearch.Text + "";
                        objDB = new SQLDB();
                        objDB.ExecuteSaveData(strCMD);
                        txtPFNumber.Text = "";
                        txtBankName.Text = "";
                        txtBankName.Text = "";
                        txtState.Text = "";
                        txtDistrict.Text = "";
                        txtCity.Text = "";
                        txtBankBranch.Text = "";
                        txtAddress.Text = "";
                        txtIFSC.Text = "";
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
                MessageBox.Show("Please Enter Valid Bank A/c Number", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPFNumber.Focus();
            }
            return flag;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void BankMaster_Load(object sender, EventArgs e)
        {
            try
            {
                //txtBankName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                //txtBankName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //AutoCompleteStringCollection daColl = new AutoCompleteStringCollection();
                objDB = new SQLDB();
                DataTable dtBankMas = objDB.ExecuteDataSet("SELECT DISTINCT BIM_BANK_NAME FROM BANK_IFSC_MICR_MAS ORDER BY BIM_BANK_NAME").Tables[0];
                DataTable dtStateMas = objDB.ExecuteDataSet("SELECT DISTINCT BIM_STATE FROM BANK_IFSC_MICR_MAS ORDER BY BIM_STATE").Tables[0];
                DataTable dtIFSC = objDB.ExecuteDataSet("SELECT DISTINCT BIM_BANK_IFSC_CODE FROM BANK_IFSC_MICR_MAS").Tables[0];
                //string[] postSource = dt
                //        .AsEnumerable()
                //        .Select<System.Data.DataRow, String>(x => x.Field<String>("BIM_BANK_NAME"))
                //        .ToArray();
                //var source = new AutoCompleteStringCollection();
                //source.AddRange(postSource);
                //txtBankName.AutoCompleteCustomSource = source;
                //txtBankName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //txtBankName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                UtilityLibrary.AutoCompleteTextBox(txtBankName, dtBankMas, "", "BIM_BANK_NAME");
                UtilityLibrary.AutoCompleteTextBox(txtState, dtStateMas, "", "BIM_STATE");
                UtilityLibrary.AutoCompleteTextBox(txtIFSC, dtIFSC, "", "BIM_BANK_IFSC_CODE");

            }
            catch { }
            finally { objDB = null; }
            dtpTo.Value = DateTime.Now;
        }

        private void txtState_Validated(object sender, EventArgs e)
        {
            //try
            //{
            //    objDB = new SQLDB();
            //    DataTable dtDistMas = objDB.ExecuteDataSet("SELECT DISTINCT BIM_DISTRICT FROM BANK_IFSC_MICR_MAS " +
            //                                                "WHERE BIM_STATE = '" + txtState.Text.ToUpper() +
            //                                                "' ORDER BY BIM_BANK_NAME").Tables[0];
            //    UtilityLibrary.AutoCompleteTextBox(txtDistrict, dtDistMas, "", "BIM_DISTRICT");
            //    objDB = null;
            //}
            //catch { }
            //finally { objDB = null; }
        }

        private void txtDistrict_Validated(object sender, EventArgs e)
        {
            //try
            //{
            //    objDB = new SQLDB();
            //    DataTable dtCityMas = objDB.ExecuteDataSet("SELECT DISTINCT BIM_CITY FROM BANK_IFSC_MICR_MAS " +
            //                                                "WHERE BIM_STATE = '" + txtState.Text.ToUpper() +
            //                                                "' AND BIM_DISTRICT='" + txtDistrict.Text.ToUpper() +
            //                                                "' ORDER BY BIM_BANK_NAME").Tables[0];
            //    UtilityLibrary.AutoCompleteTextBox(txtCity, dtCityMas, "", "BIM_CITY");
            //    objDB = null;
            //}
            //catch { }
            //finally { objDB = null; }
        }

        private void txtCity_Validated(object sender, EventArgs e)
        {
            //try
            //{
            //    objDB = new SQLDB();
            //    DataTable dtBranchMas = objDB.ExecuteDataSet("SELECT DISTINCT BIM_BRANCH_NAME FROM BANK_IFSC_MICR_MAS " +
            //                                                "WHERE BIM_STATE = '" + txtState.Text.ToUpper() +
            //                                                "' AND BIM_DISTRICT='" + txtDistrict.Text.ToUpper() +
            //                                                "' AND BIM_CITY='" + txtCity.Text.ToUpper() +
            //                                                "' ORDER BY BIM_BANK_NAME").Tables[0];
            //    UtilityLibrary.AutoCompleteTextBox(txtBankBranch, dtBranchMas, "", "BIM_BRANCH_NAME");
            //    objDB = null;
            //}
            //catch { }
            //finally { objDB = null; }
        }

        private void txtBankBranch_Validated(object sender, EventArgs e)
        {
            try
            {
                objDB = new SQLDB();
                DataTable dtBranchMas = objDB.ExecuteDataSet("SELECT DISTINCT BIM_BANK_IFSC_CODE FROM BANK_IFSC_MICR_MAS " +
                                                            "WHERE BIM_BANK_NAME = '" + txtBankName.Text +                                                            
                                                            "' AND BIM_BRANCH_NAME='" + txtBankBranch.Text.ToUpper() +
                                                            "' ORDER BY BIM_BANK_IFSC_CODE").Tables[0];
                txtIFSC.Text = dtBranchMas.Rows[0][0].ToString();
                //UtilityLibrary.AutoCompleteTextBox(txtIFSC, dtBranchMas, "", "BIM_BANK_IFSC_CODE");
                objDB = null;
            }
            catch { }
            finally { objDB = null; }
        }

        private void txtIFSC_Validated(object sender, EventArgs e)
        {
            try
            {
                objDB = new SQLDB();
                DataTable dtBranchMas = objDB.ExecuteDataSet("SELECT * FROM BANK_IFSC_MICR_MAS " +
                                                            "WHERE BIM_BANK_IFSC_CODE='"+txtIFSC.Text.ToUpper()+"'").Tables[0];
                if (dtBranchMas.Rows.Count > 0)
                {
                    txtBankName.Text = dtBranchMas.Rows[0]["BIM_BANK_NAME"].ToString();
                    txtState.Text = dtBranchMas.Rows[0]["BIM_STATE"].ToString();
                    txtDistrict.Text = dtBranchMas.Rows[0]["BIM_DISTRICT"].ToString();
                    txtCity.Text = dtBranchMas.Rows[0]["BIM_CITY"].ToString();
                    txtBankBranch.Text = dtBranchMas.Rows[0]["BIM_BRANCH_NAME"].ToString();
                    txtAddress.Text = dtBranchMas.Rows[0]["BIM_ADDRESS"].ToString();
                }
                else
                {
                    //txtBankName.Text = "";
                    //txtState.Text = "";
                    //txtDistrict.Text = "";
                    //txtCity.Text = "";
                    //txtBankBranch.Text = "";
                    //txtAddress.Text = "";
                }
                objDB = null;
            }
            catch { }
            finally { objDB = null; }
        }

        private void txtBankName_Validated(object sender, EventArgs e)
        {
            try
            {
                objDB = new SQLDB();
                DataTable dtBranchMas = objDB.ExecuteDataSet("SELECT DISTINCT BIM_BRANCH_NAME FROM BANK_IFSC_MICR_MAS " +
                                                            "WHERE BIM_BANK_NAME = '" + txtBankName.Text +
                                                            "' ORDER BY BIM_BRANCH_NAME").Tables[0];
                UtilityLibrary.AutoCompleteTextBox(txtBankBranch, dtBranchMas, "", "BIM_BRANCH_NAME");
                objDB = null;
            }
            catch { }
            finally { objDB = null; }
        }
    }
}
