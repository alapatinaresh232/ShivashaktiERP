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
using SSCRM.App_Code;
using System.IO;
using SSTrans;
using System.Data.SqlClient;
using System.Configuration;
namespace SSCRM
{
    public partial class DummyAgentApplication : Form
    {
        private Security objSecurity = null;
        private HRInfo objHrInfo = null;
        private SQLDB objDB = null;
        public DummyAgentApplication()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DummyAgentApplication_Load(object sender, EventArgs e)
        {
            objSecurity = new Security();
            DataTable dtCpy = objSecurity.GetCompanyDataSet().Tables[0];
            UtilityLibrary.PopulateControl(cmbCompany, dtCpy.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
            objSecurity = null;

            objDB = new SQLDB();
            string strSQL1 = "SELECT HRSM_RECRUITMENT_SOURCE_CODE,HRSM_RECRUITMENT_SOURCE_NAME from HR_RECRUITMENT_SOURCE_MASTER";
            DataTable dt1 = objDB.ExecuteDataSet(strSQL1, CommandType.Text).Tables[0];
            UtilityLibrary.PopulateControl(cmbSourceCode, dt1.DefaultView, 1, 0, "--PLEASE SELECT--", 0);

            cmbCompany.SelectedIndex = 0;
            txtfName.Text = "";
            txtFullName.Text = "";
            txtNativePlace_optional.Text = "";
            cmbF_Flg_optional.SelectedIndex = 0;
            cmbMatrital_optional.SelectedIndex = 0;
            cmbNationality.SelectedIndex = 0;
            cmbSex_optional.SelectedIndex = 0;
            cmbTypeEdu.SelectedIndex = 0;
            dtpDOJ.Value = Convert.ToDateTime(CommonData.CurrentDate);
            dtpDOB.Value = Convert.ToDateTime(CommonData.CurrentDate);
            lblPath.Text = "";
            txtEcodeSearch.Text = "";
            txtRecruitedBy.Text = "";
            cmbReligion.SelectedIndex = 0;
            cmbSourceCode.SelectedIndex = 0;
            picEmpPhoto.BackgroundImage = null;

            //DataView dvDept = objDB.ExecuteDataSet("SELECT DEPT_CODE,DEPT_DESC FROM DEPT_MAS", CommandType.Text).Tables[0].DefaultView;
            //UtilityLibrary.PopulateControl(cmbDepartment, dvDept, 1, 0, "--PLEASE SELECT--", 0);
            //objDB = null;  
        }

        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCompany.SelectedIndex > 0)
            {
                objHrInfo = new HRInfo();
                DataTable dtBranch = objHrInfo.GetAllBranchList(cmbCompany.SelectedValue.ToString(), "", "").Tables[0];
                UtilityLibrary.PopulateControl(cmbBranch, dtBranch.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
                objHrInfo = null;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbCompany.SelectedIndex = 0;
            txtfName.Text = "";
            txtFullName.Text = "";
            txtNativePlace_optional.Text = "";
            cmbF_Flg_optional.SelectedIndex = 0;
            cmbMatrital_optional.SelectedIndex = 0;
            cmbNationality.SelectedIndex = 0;
            cmbSex_optional.SelectedIndex = 0;
            cmbTypeEdu.SelectedIndex = 0;
            dtpDOJ.Value = Convert.ToDateTime(CommonData.CurrentDate);
            dtpDOB.Value = Convert.ToDateTime(CommonData.CurrentDate);
            lblPath.Text = "";
            txtEcodeSearch.Text = "";
            txtRecruitedBy.Text = "";
            cmbReligion.SelectedIndex = 0;
            cmbSourceCode.SelectedIndex = 0;
            picEmpPhoto.BackgroundImage = null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                Int32 imaxApplNo = GetNewApplNo();
                Int32 iAcode = GetNewACode();
                string sqlText = "";
                int iRec = 0;
                objDB = new SQLDB();                
                try
                {
                    sqlText = "INSERT INTO HR_APPL_MASTER_HEAD(HAMH_COMPANY_CODE,HAMH_BRANCH_CODE,HAMH_APPL_NUMBER,HAMH_APPL_DATE,HAMH_RECRUITMENT_SOURCE_CODE,HAMH_RECRUITMENT_SOURCE_DELT_CODE,HAMH_RECRUITMENT_SOURCE_ECODE,"+
                              "HAMH_NAME,HAMH_FORH,HAMH_FORH_NAME,HAMH_DOJ,HAMH_DOB,HAMH_NATIVE_PLACE,HAMH_SEX,HAMH_NAIONALITY,HAMH_RELIGION,HAMH_MATRITAL_STATUS,HAMH_EORA_TYPE,HAMH_EORA_CODE,HAMH_CREATED_BY,HAMH_CREATED_DATE,"+
                              "HAMH_WORKING_STATUS) VALUES('" + cmbCompany.SelectedValue.ToString() + "','" + cmbBranch.SelectedValue.ToString() + "','" + imaxApplNo + "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") + "'" +
                              ",'" + cmbSourceCode.SelectedValue.ToString() + "','" + cmbRequtSourDetails.SelectedValue.ToString() + "','" + txtEcodeSearch.Text.ToString() + "','" + txtFullName.Text.ToString() + "'" +
                              ",'" + cmbF_Flg_optional.SelectedItem.ToString() + "','" + txtfName.Text.ToString() + "','" + Convert.ToDateTime(dtpDOJ.Value).ToString("dd/MMM/yyyy") + "','" + Convert.ToDateTime(dtpDOB.Value).ToString("dd/MMM/yyyy") + "'" +
                              ",'" + txtNativePlace_optional.Text + "','" + cmbSex_optional.SelectedItem.ToString() + "','" + cmbNationality.SelectedItem.ToString() + "','" + cmbReligion.SelectedItem.ToString() + "','" + cmbMatrital_optional.SelectedItem.ToString() + "'" +
                              ",'A','" + iAcode + "','"+CommonData.LogUserId+"','"+Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy")+"','L'); ";
                    sqlText += "INSERT INTO EORA_MASTER(BRANCH_CODE,DEPT_ID,ECODE,MEMBER_NAME,EORA,HRIS_EMP_NAME,DESG_ID,DESIG,EMP_DOJ,EMP_DOB,FATHER_NAME,elevel_id,company_code,edu_qualification) VALUES('"+cmbBranch.SelectedValue.ToString()+"'" +
                               ",'1200000','"+iAcode+"','"+txtFullName.Text.ToString()+"','A','"+txtFullName.Text.ToString()+"','987','ST','"+Convert.ToDateTime(dtpDOJ.Value).ToString("dd/MMM/yyyy")+"','"+Convert.ToDateTime(dtpDOB.Value).ToString("dd/MMM/yyyy")+"'"+
                               ",'"+txtfName.Text.ToString()+"','95','"+cmbCompany.SelectedValue.ToString()+"','"+cmbTypeEdu.SelectedItem.ToString()+"'); ";
                    iRec = objDB.ExecuteSaveData(sqlText);

                    if (iRec > 0)
                    {
                        MessageBox.Show("Acode Generated For " + txtFullName.Text + "\n ACODE : " + iAcode + "", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnClear_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Data not Saved", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objDB = null;
                }
            }
        }

        private int GetNewACode()
        {
            objDB = new SQLDB();
            Int32 imaxAcode = 0;
            try
            {
                string sqlText = "SELECT MAX(HAMH_EORA_CODE)+1 FROM HR_APPL_MASTER_HEAD WHERE HAMH_EORA_TYPE='A';";
                imaxAcode = Convert.ToInt32(objDB.ExecuteDataSet(sqlText).Tables[0].Rows[0][0]);
                return imaxAcode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return imaxAcode;
            }
            finally
            {
                objDB = null;
            }
        }

        private int GetNewApplNo()
        {
            objDB = new SQLDB();
            Int32 imaxAppl = 0;
            try
            {
                string sqlText = "SELECT MAX(HAMH_APPL_NUMBER)+1 FROM HR_APPL_MASTER_HEAD;";
                imaxAppl = Convert.ToInt32(objDB.ExecuteDataSet(sqlText).Tables[0].Rows[0][0]);
                return imaxAppl;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return imaxAppl;
            }
            finally
            {
                objDB = null;
            }
        }

        private bool CheckData()
        {
            bool rFlag = true;
            if (cmbCompany.SelectedIndex <= 0)
            {
                rFlag = false;
                MessageBox.Show("Select Company?", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return rFlag;
            }
            if (cmbBranch.SelectedIndex <= 0)
            {
                rFlag = false;
                MessageBox.Show("Select Branch?", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return rFlag;
            }
            if (cmbSourceCode.SelectedIndex <= 0)
            {
                rFlag = false;
                MessageBox.Show("Select Recruitement Source?", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return rFlag;
            }
            if (cmbReligion.SelectedIndex == -1)
            {
                rFlag = false;
                MessageBox.Show("Select Religion?", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return rFlag;
            }
            if (cmbRequtSourDetails.SelectedIndex == 0)
            {
                rFlag = false;
                MessageBox.Show("Select Recruitement Source Detail?", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return rFlag;
            }
            if (txtRecruitedBy.Text == "")
            {
                rFlag = false;
                MessageBox.Show("Enter Recruiter Ecode?", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return rFlag;
            }
            if (txtFullName.Text == "")
            {
                rFlag = false;
                MessageBox.Show("Enter Agent Name?", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return rFlag;
            }
            if (cmbF_Flg_optional.SelectedIndex == -1)
            {
                rFlag = false;
                MessageBox.Show("Select Father or Husband?", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return rFlag;
            }
            if (txtfName.Text == "")
            {
                rFlag = false;
                MessageBox.Show("Enter Agent Father Name?", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return rFlag;
            }
            
            if (txtNativePlace_optional.Text == "")
            {
                rFlag = false;
                MessageBox.Show("Enter Native Place Name?", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return rFlag;
            }
            if (cmbNationality.SelectedIndex == -1)
            {
                rFlag = false;
                MessageBox.Show("Select Nationality?", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return rFlag;
            }
            if (cmbMatrital_optional.SelectedIndex == -1)
            {
                rFlag = false;
                MessageBox.Show("Select Marital Status?", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return rFlag;
            }
            if (cmbSex_optional.SelectedIndex == -1)
            {
                rFlag = false;
                MessageBox.Show("Select Gender?", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return rFlag;
            }
            if (cmbTypeEdu.SelectedIndex == -1)
            {
                rFlag = false;
                MessageBox.Show("Select Qualification?", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return rFlag;
            }
            
            return rFlag;
        }

        private void txtEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
            
        }

        private void GetRecruiterName()
        {
            objDB = new SQLDB();
            string recruiterName = "";
            try
            {
                string sqlText = "SELECT MEMBER_NAME FROM EORA_MASTER WHERE ECODE LIKE '%" + txtEcodeSearch.Text + "%';";
                recruiterName = objDB.ExecuteDataSet(sqlText).Tables[0].Rows[0][0].ToString();
                if (recruiterName.Length > 5)
                {
                    txtRecruitedBy.Text = recruiterName;
                }
                else
                {
                    txtRecruitedBy.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                
            }
            finally
            {
                objDB = null;
            }
        }

        private void cmbSourceCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSourceCode.SelectedIndex > 0)
            {
                objDB = new SQLDB();
                string strReq = "SELECT HRSMD_RECRUITMENT_SOURCE_DETL_CODE,HRSMD_RECRUITMENT_SOURCE_DETL_NAME From HR_RECRUITMENT_SOURCE_MASTER_DETL WHERE HRSMD_RECRUITMENT_SOURCE_CODE='" + cmbSourceCode.SelectedValue + "'";
                DataTable dtRQT = objDB.ExecuteDataSet(strReq, CommandType.Text).Tables[0];
                UtilityLibrary.PopulateControl(cmbRequtSourDetails, dtRQT.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
                objDB = null;
            }
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text.Trim().Length > 4)
            {
                GetRecruiterName();
            }
            else
            {
                txtRecruitedBy.Text = "";
            }
        }
    }
}
