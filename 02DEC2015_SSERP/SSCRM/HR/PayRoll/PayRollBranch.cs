using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSAdmin;
using SSTrans;
using SSCRM.App_Code;
using System.IO;
using System.Data.SqlClient;
using System.Collections;


namespace SSCRM
{
   
    public partial class PayRollBranch : Form
    {
        SQLDB objSQLDB = null;
        bool flagUpdate = false;
        DataTable dt,dt1;
        public PayRollBranch()
        {
            InitializeComponent();
        }

        private void txtEcodeSearch_TextChanged(object sender, EventArgs e)
        {
           
        }
        public void GetImage(byte[] imageData)
        {
            try
            {
                Image newImage;
                using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                {
                    ms.Write(imageData, 0, imageData.Length);
                    newImage = Image.FromStream(ms, true);
                }
                picEmpPhoto.BackgroundImage = newImage;
                this.picEmpPhoto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
            }
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void PayRollBranch_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            
        }
        private void FillCompanyData()
        {
            objSQLDB = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT CM_COMPANY_NAME,CM_COMPANY_CODE FROM COMPANY_MAS where active='T'";

                dt = objSQLDB.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbCompany.DataSource = dt;
                    cbCompany.DisplayMember = "CM_COMPANY_NAME";
                    cbCompany.ValueMember = "CM_COMPANY_CODE";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLDB = null;
                dt = null;
            }
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                FillBranchData();
            
        }
        private void FillBranchData()
        {
            if (cbCompany.SelectedIndex > 0)
            {
                objSQLDB = new SQLDB();
                DataTable dt = new DataTable();
                cbLocation.DataSource = null;
                try
                {
                    if (cbCompany.SelectedIndex > 0)
                    {
                        string strCommand = "SELECT BRANCH_CODE as branchCode,BRANCH_NAME FROM BRANCH_MAS WHERE active='T' and COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + "' ORDER BY BRANCH_NAME ASC ";
                        dt = objSQLDB.ExecuteDataSet(strCommand).Tables[0];
                    }
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = "--Select--";
                        dr[1] = "--Select--";

                        dt.Rows.InsertAt(dr, 0);
                        cbLocation.DataSource = dt;
                        cbLocation.DisplayMember = "BRANCH_NAME";
                        cbLocation.ValueMember = "branchCode";
                        //cbLocation.ValueMember = "LOCATION";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSQLDB = null;
                    dt = null;
                }
            }
            else
            {
                cbLocation.DataSource = null;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEcodeSearch.Text = "";
            //txtEcodeSearch_TextChanged(null, null);
            cbCompany.SelectedIndex = 0;
            flagUpdate = false;
            gvEmpDetails.Rows.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(checkData())
            {
                if (SavePayRollBranch()>0)
                {
                    MessageBox.Show("Data saved successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    flagUpdate = false;
                    btnClear_Click(null,null);
                }
            }
        }

        private int SavePayRollBranch()
        {
            int iRes = 0;
            try
            {
                if (flagUpdate == false)
                {
                    string strCMD = "insert into HR_PAYROLL_OTHBRN_ECODE(HPOE_ECODE,HPOE_COMPANY_CODE,HPOE_BRANCH_CODE,HPOE_WAGEMONTH,HPOE_PROLL_COMPANY_CODE" +
                                        ",HPOE_PROLL_BRANCH_CODE,HPOE_PROLL_FROM_WAGEPERIOD,HPOE_CREATED_BY,HPOE_CREATED_DATE)" +
                                        " values(" + txtEcodeSearch.Text + ",'" + txtCompany.Tag + "','" + txtBranch.Tag + "','" + dtpEffectedFrom.Value.ToString("MMMyyyy").ToUpper() +
                                        "','" + cbCompany.SelectedValue + "','" + cbLocation.SelectedValue + "','" + dtpEffectedFrom.Value.ToString("MMMyyyy").ToUpper() + "','" + CommonData.LogUserId + "',getdate())";

                    objSQLDB = new SQLDB();
                    iRes = objSQLDB.ExecuteSaveData(strCMD);
                }
                                        
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iRes;
        }
        private void FillDataToGrid()
        {
            try
            {
                
                

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            gvEmpDetails.Rows.Clear();
            for (int i = 0; i < dt1.Rows.Count;i++ )
            {
                gvEmpDetails.Rows.Add();
                gvEmpDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                gvEmpDetails.Rows[i].Cells["Ecode"].Value = dt.Rows[i]["ID"] + "";
                gvEmpDetails.Rows[i].Cells["Names"].Value = dt.Rows[i]["MEMBER_NAME"] + "";
                gvEmpDetails.Rows[i].Cells["PCompany"].Value = dt.Rows[i]["COMPANY_CODE"] + "";
                gvEmpDetails.Rows[i].Cells["PBranch"].Value = dt.Rows[i]["BRANCH_NAME"] + "";
                gvEmpDetails.Rows[i].Cells["RComapny"].Value = dt1.Rows[i]["HPOE_PROLL_COMPANY_CODE"] + "";
                gvEmpDetails.Rows[i].Cells["RBranch"].Value = dt1.Rows[i]["branchName"] + "";
                gvEmpDetails.Rows[i].Cells["From"].Value = dt1.Rows[i]["HPOE_PROLL_FROM_WAGEPERIOD"] + "";
                gvEmpDetails.Rows[i].Cells["To"].Value = dt1.Rows[i]["HPOE_PROLL_TO_WAGEPERIOD"] + "";

            }
        }

        private bool checkData()
        {
            bool flag=true;
            if(txtName.Text.Length==0)
            {
                MessageBox.Show("Enter Valid Ecode", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                flag = false;
                txtEcodeSearch.Focus();
                return flag;
            }
            if(cbCompany.SelectedIndex<=0)
            {
                MessageBox.Show("Select Company", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                flag = false;
                cbCompany.Focus();
                return flag;
            }
            if (cbLocation.SelectedIndex <= 0)
            {
                MessageBox.Show("Select Branch", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                flag = false;
                cbLocation.Focus();
                return flag;
            }
            return flag;
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            gvEmpDetails.Rows.Clear();
            if (txtEcodeSearch.Text != "")
            {
                dt = new DataTable();
                objSQLDB = new SQLDB();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = objSQLDB.CreateParameter("@EORACODE", DbType.String, txtEcodeSearch.Text.ToString(), ParameterDirection.Input);
                try
                {
                    dt = objSQLDB.ExecuteDataSet("GetEmpDataforEORAMas", CommandType.StoredProcedure, param).Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (dt.Rows.Count > 0)
                {

                    txtName.Text = dt.Rows[0]["MEMBER_NAME"].ToString();
                    txtDesig.Text = dt.Rows[0]["DESIG_NAME"].ToString();
                    txtDesig.Tag = dt.Rows[0]["DESIG_ID"].ToString();
                    txtCompany.Text = dt.Rows[0]["COMPANY_NAME"].ToString();
                    txtCompany.Tag = dt.Rows[0]["COMPANY_CODE"].ToString();
                    txtBranch.Text = dt.Rows[0]["BRANCH_NAME"].ToString();
                    txtBranch.Tag = dt.Rows[0]["BRANCH_CODE"].ToString();
                    txtDept.Text = dt.Rows[0]["Dept_Name"].ToString();
                    txtDept.Tag = dt.Rows[0]["Dept_Code"].ToString();
                    string strCMD = "select HR_PAYROLL_OTHBRN_ECODE.*,bm.branch_name branchName from HR_PAYROLL_OTHBRN_ECODE inner join branch_mas bm on bm.branch_code=HPOE_PROLL_BRANCH_CODE" +
                                                " where HPOE_ECODE=" + txtEcodeSearch.Text;
                    objSQLDB = new SQLDB();
                    dt1 = objSQLDB.ExecuteDataSet(strCMD).Tables[0];
                    if (dt1.Rows.Count > 0)
                    {
                        FillDataToGrid();
                        flagUpdate = true;
                        cbCompany.SelectedValue = dt1.Rows[0]["HPOE_PROLL_COMPANY_CODE"].ToString();
                        cbLocation.SelectedValue = dt1.Rows[0]["HPOE_PROLL_BRANCH_CODE"].ToString();
                        dtpEffectedFrom.Value = Convert.ToDateTime(dt1.Rows[0]["HPOE_PROLL_FROM_WAGEPERIOD"]);
                    }

                    objSQLDB = new SQLDB();
                    DataSet dsPhoto = objSQLDB.ExecuteDataSet("SELECT HAPS_PHOTO_SIG FROM HR_APPL_PHOTO_SIG WHERE HAPS_APPL_NUMBER=" + txtEcodeSearch.Text);

                    if (dsPhoto.Tables[0].Rows.Count > 0)
                        GetImage((byte[])dsPhoto.Tables[0].Rows[0]["HAPS_PHOTO_SIG"]);
                    else
                        picEmpPhoto.BackgroundImage = global::SSCRM.Properties.Resources.nomale;



                    objSQLDB = null;


                }
                else
                {
                    txtEcodeSearch.Focus();
                    txtName.Text = "";
                    txtDesig.Text = "";
                    txtDept.Text = "";
                    txtCompany.Text = "";
                    txtBranch.Text = "";
                    cbCompany.SelectedIndex = 0;
                    picEmpPhoto.BackgroundImage = global::SSCRM.Properties.Resources.nomale;

                }
                objSQLDB = null;

            }
            else
            {
                txtEcodeSearch.Focus();
                txtName.Text = "";
                txtDesig.Text = "";
                txtDept.Text = "";
                txtCompany.Text = "";
                txtBranch.Text = "";
                cbCompany.SelectedIndex = 0;
                picEmpPhoto.BackgroundImage = global::SSCRM.Properties.Resources.nomale;
            }
        }
       
    }
}
