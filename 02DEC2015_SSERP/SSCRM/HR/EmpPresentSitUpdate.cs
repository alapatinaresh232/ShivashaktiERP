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
using System.IO;
using SSCRM.App_Code;
using SSTrans;
namespace SSCRM
{
    public partial class EmpPresentSitUpdate : Form
    {
        private SQLDB objDB = null;
        private Master objMstr = null;
        private DataTable dtEmpInfo = null;
        private DataTable dtEmpSal = null;
        private Security objSecurity = null;
        private HRInfo objHrInfo = null;
        private bool isUpDate = false;
        public EmpPresentSitUpdate()
        {
            InitializeComponent();
        }

        private void PromotionBoard_Load(object sender, EventArgs e)
        {            
            GetPopupdropDown();
        }
        
        public void GetPopupdropDown()
        {
            objSecurity = new Security();
            objDB = new SQLDB();
            DataTable dtCpy = objDB.ExecuteDataSet("SELECT CM_Company_Code, CM_Company_Name FROM Company_Mas" +
                                                    " ORDER BY CM_Company_Name", CommandType.Text).Tables[0];
            UtilityLibrary.PopulateControl(cmbCompany, dtCpy.DefaultView, 1, 0, "--PLEASE SELECT--", 0);             
            objDB = null;    

            objDB = new SQLDB();
            DataView dvDept = objDB.ExecuteDataSet("SELECT dept_code,dept_name FROM Dept_Mas order by dept_name asc", CommandType.Text).Tables[0].DefaultView;
            UtilityLibrary.PopulateControl(cmbDepartMent, dvDept, 1, 0, "--PLEASE SELECT--", 0);

            DataView dtRBranch = objDB.ExecuteDataSet("SELECT BRANCH_CODE,BRANCH_NAME FROM BRANCH_MAS ORDER BY BRANCH_NAME ASC", CommandType.Text).Tables[0].DefaultView;
            UtilityLibrary.PopulateControl(cmbRepToBranch, dtRBranch, 1, 0, "--PLEASE SELECT--", 0);
            objDB = null;

            objMstr = new Master();
            DataTable dtSalTypes = objMstr.GetHRSalStructureTypes().Tables[0];
            UtilityLibrary.PopulateControl(cmbSalStrType, dtSalTypes.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
            objMstr = null;
            
        }
        private DataTable dtAuthSigns()
        {
            DataTable table = new DataTable();
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("name", typeof(string));

            table.Rows.Add("11137", "BIJOY FRANCIS (GM)");
            table.Rows.Add("40003", "N SREENIVAS RAO (MD)");
            //table.Rows.Add("SP2PU", "SP2PU");
            //table.Rows.Add("SP2OL", "SP2OL");


            return table;
        }
        

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text.Length > 4)
            {
                FillEmployeeMasterData();
                FillSalStuctureData();
            }
            else
            {
                
                txtSalBasic.Text = "";
                txtSalBNPAllw.Text = "";
                txtSalHRA.Text = "";
                txtSalCCA.Text = "";
                txtSalChildAllw.Text = "";
                txtSalConvAllw.Text = "";
                txtSalGross.Text = "";
                txtSalLTAAllw.Text = "";
                txtSalMedAllw.Text = "";
                txtSalPetrAllw.Text = "";
                txtSalSpecialAllw.Text = "";
                txtSalUniformAllw.Text = "";
                txtVehicleAllw.Text = "";
                txtRecruiterName.Text = "";
                txtReportingEcode.Text = "";
                txtRemarks.Text = "";
                txtMemberName.Text = "";
                txtFatherName.Text = "";
                txtHRISDesig.Text = "";
                txtDept.Text = "";
                txtBranch.Text = "";
                txtComp.Text = "";
                meHRISDataofBirth.Text = "";
                meHRISDateOfJoin.Text = "";
                picEmp.BackgroundImage = null;                
                cmbCompany.SelectedIndex = 0;
                if (cmbBranch.Items.Count > 0)
                    cmbBranch.SelectedIndex = 0;
                if (cmbDesignation.Items.Count > 0)
                    cmbDesignation.SelectedIndex = 0;
                cmbDepartMent.SelectedIndex = 0;
                cmbRepToBranch.SelectedIndex = 0;
                
                dtpEffDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            }
        }

        private void FillSalStuctureData()
        {
            objDB = new SQLDB();
            
            string sqlText = "";
            try
            {
                sqlText = "select HESS_APPL_NUMBER,HESS_EORA_CODE,HESS_PROMOTION_CATEGORY_CODE,HESS_FROM_COMPANY_CODE" +
                        ",HESS_FROM_BRANCH_CODE,HESS_TO_COMPANY_CODE,HESS_TO_BRANCH_CODE,HESS_TRXN_NO,HESS_TRXN_DATE" +
                        ",HESS_SALSTRU_TYPE,HESS_SALSTRU_CODE,HESS_LTR_REF_NO,HESS_LTR_REF_DATE,HESS_EFF_DATE" +
                        ",HESS_FROM_DEPT_ID,HESS_FROM_DESIG_ID,HESS_TO_DEPT_ID,HESS_DESIG_ID,HESS_REPO_TO_ECODE" +
                        ",HESS_REPO_TO_BRANCH_CODE,HESS_REMARKS,HESS_BASIC,HESS_HRA,HESS_CCA,HESS_CONV_ALW,HESS_LTA_ALW" +
                        ",HESS_SPL_ALW,HESS_UNF_ALW,HESS_VEH_ALW,HESS_CH_ED_ALW,HESS_BNP_ALW,HESS_MED_REIMB,HESS_PET_ALW" +
                        ",HESS_AUTH_SIGN from HR_EMP_SAL_STRU LEFT JOIN HR_APPL_A2E_MIGRATION ON HAAM_AGENT_CODE = HESS_EORA_CODE " +
                        "where (ISNULL(HAAM_EMP_CODE,HESS_EORA_CODE) = " + txtEcodeSearch.Text + " OR " +
                        "ISNULL(HAAM_AGENT_CODE,HESS_EORA_CODE) = " + txtEcodeSearch.Text + ") AND HESS_PROMOTION_CATEGORY_CODE ='GEN'";
                dtEmpSal = objDB.ExecuteDataSet(sqlText).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objMstr = null;
            }
            if (dtEmpSal.Rows.Count > 0)
            {
                isUpDate = true;
                cmbSalStrType.SelectedValue = dtEmpSal.Rows[0]["HESS_SALSTRU_TYPE"].ToString();
                cmbSalStrCode.SelectedValue = dtEmpSal.Rows[0]["HESS_SALSTRU_CODE"].ToString();
                txtSalBasic.Text = dtEmpSal.Rows[0]["HESS_BASIC"].ToString();
                txtSalBNPAllw.Text = dtEmpSal.Rows[0]["HESS_BNP_ALW"].ToString();
                txtSalHRA.Text = dtEmpSal.Rows[0]["HESS_HRA"].ToString();
                txtSalCCA.Text = dtEmpSal.Rows[0]["HESS_CCA"].ToString();
                txtSalChildAllw.Text = dtEmpSal.Rows[0]["HESS_CH_ED_ALW"].ToString();
                txtSalConvAllw.Text = dtEmpSal.Rows[0]["HESS_CONV_ALW"].ToString();
                //txtSalGross.Text = dtEmpSal.Rows[0]["HESS_BASIC"].ToString();
                txtSalLTAAllw.Text = dtEmpSal.Rows[0]["HESS_LTA_ALW"].ToString();
                txtSalMedAllw.Text = dtEmpSal.Rows[0]["HESS_MED_REIMB"].ToString();
                txtSalPetrAllw.Text = dtEmpSal.Rows[0]["HESS_PET_ALW"].ToString();
                txtSalSpecialAllw.Text = dtEmpSal.Rows[0]["HESS_SPL_ALW"].ToString();
                txtSalUniformAllw.Text = dtEmpSal.Rows[0]["HESS_UNF_ALW"].ToString();
                txtVehicleAllw.Text = dtEmpSal.Rows[0]["HESS_VEH_ALW"].ToString();
                //txtRecruiterName.Text = dtEmpSal.Rows[0]["HESS_BASIC"].ToString();
                txtReportingEcode.Text = dtEmpSal.Rows[0]["HESS_REPO_TO_ECODE"].ToString();                
                txtReportingEcode_KeyUp(null, null);
                txtSalBasic_KeyUp(null, null);
                cmbRepToBranch.SelectedValue = dtEmpSal.Rows[0]["HESS_REPO_TO_BRANCH_CODE"].ToString();
                txtRemarks.Text = dtEmpSal.Rows[0]["HESS_REMARKS"].ToString();

            }
            else
            {
                txtSalBasic.Text = "";
                txtSalBNPAllw.Text = "";
                txtSalHRA.Text = "";
                txtSalCCA.Text = "";
                txtSalChildAllw.Text = "";
                txtSalConvAllw.Text = "";
                txtSalGross.Text = "";
                txtSalLTAAllw.Text = "";
                txtSalMedAllw.Text = "";
                txtSalPetrAllw.Text = "";
                txtSalSpecialAllw.Text = "";
                txtSalUniformAllw.Text = "";
                txtVehicleAllw.Text = "";
                txtRecruiterName.Text = "";
                txtReportingEcode.Text = "";
                cmbRepToBranch.SelectedIndex = 0;
                txtRemarks.Text = "";
                isUpDate = false;
            }
        }

        private void FillEmployeeMasterData()
        {
            objMstr = new Master();
            DataSet ds = new DataSet();
            try
            {
                ds = objMstr.GetEmployeeMasterDetl(txtEcodeSearch.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objMstr = null;                
            }
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtEmpInfo = ds.Tables[0];
                    txtMemberName.Text = ds.Tables[0].Rows[0]["EmpName"].ToString();
                    txtFatherName.Text = ds.Tables[0].Rows[0]["FatherName"].ToString();
                    txtHRISDesig.Text = ds.Tables[0].Rows[0]["DesigName"].ToString();
                    txtDept.Text = ds.Tables[0].Rows[0]["DeptName"].ToString();
                    txtComp.Text = ds.Tables[0].Rows[0]["CompName"].ToString();
                    txtBranch.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
                    meHRISDataofBirth.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["EmpDob"]).ToString("dd/MM/yyyy");
                    meHRISDateOfJoin.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["EmpDoj"]).ToString("dd/MM/yyyy");
                    cmbCompany.SelectedValue = ds.Tables[0].Rows[0]["CompCode"].ToString();
                    cmbBranch.SelectedValue = ds.Tables[0].Rows[0]["BranchCode"].ToString();
                    cmbDepartMent.SelectedValue = ds.Tables[0].Rows[0]["DeptCode"].ToString();
                    cmbDesignation.SelectedValue = ds.Tables[0].Rows[0]["DesgID"].ToString() + '@' + ds.Tables[0].Rows[0]["ElevelID"].ToString();

                    if (ds.Tables[0].Rows[0]["PhotoSig"].ToString() != "")
                    {
                        GetImage((byte[])ds.Tables[0].Rows[0]["PhotoSig"]);
                    }
                }
                else
                {
                    dtEmpInfo = null;
                }
            }
            ds = null;
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
                picEmp.BackgroundImage = newImage;
                this.picEmp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
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

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtSalBasic_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtBoxCtrlAcceptNumericOnly(sender, e);
        }
        private void txtBoxCtrlAcceptNumericOnly(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtSalHRA_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtBoxCtrlAcceptNumericOnly(sender, e);
        }

        private void txtSalCCA_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtBoxCtrlAcceptNumericOnly(sender, e);
        }

        private void txtSalConvAllw_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtBoxCtrlAcceptNumericOnly(sender, e);
        }

        private void txtSalLTAAllw_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtBoxCtrlAcceptNumericOnly(sender, e);
        }

        private void txtSalSpecialAllw_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtBoxCtrlAcceptNumericOnly(sender, e);
        }

        private void txtSalUniformAllw_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtBoxCtrlAcceptNumericOnly(sender, e);
        }

        private void txtVehicleAllw_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtBoxCtrlAcceptNumericOnly(sender, e);
        }

        private void txtSalChildAllw_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtBoxCtrlAcceptNumericOnly(sender, e);
        }

        private void txtBNPAllw_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtBoxCtrlAcceptNumericOnly(sender, e);
        }

        private void txtSalMedAllw_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtBoxCtrlAcceptNumericOnly(sender, e);
        }

        private void txtSalPetrAllw_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtBoxCtrlAcceptNumericOnly(sender, e);
        }

        private void txtReportingEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtBoxCtrlAcceptNumericOnly(sender, e);
        }

        private void txtReportingEcode_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtReportingEcode.Text.Length > 4)
            {
                objMstr = new Master();
                DataSet ds = new DataSet();
                try
                {
                    ds = objMstr.GetEmployeeMasterDetl(txtReportingEcode.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objMstr = null;
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtRecruiterName.Text = ds.Tables[0].Rows[0]["EmpName"].ToString();
                    cmbRepToBranch.SelectedValue = ds.Tables[0].Rows[0]["BranchCode"].ToString();
                }
                else
                {
                    txtRecruiterName.Text = "";
                }
                ds = null;
            }
            else
            {
                txtRecruiterName.Text = "";
            }
        }

        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCompany.SelectedIndex > 0)
            {
                //objHrInfo = new HRInfo();
                //DataTable dtBranch = objHrInfo.GetAllBranchList(cmbCompany.SelectedValue.ToString(), "", "").Tables[0];
                //UtilityLibrary.PopulateControl(cmbBranch, dtBranch.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
                //objHrInfo = null;
                objDB = new SQLDB();
                DataTable dtBranch = objDB.ExecuteDataSet(" SELECT branch_code as branch_code, branch_name  as branch_name,branch_Type,ACTIVE FROM branch_mas " +
                                                        "WHERE branch_name<>'''' AND upper(company_code)=Upper('" + cmbCompany.SelectedValue.ToString() + "') Order by branch_name").Tables[0];
                UtilityLibrary.PopulateControl(cmbBranch, dtBranch.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
                objDB = null;
                
            }
        }

        private void cmbDepartMent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDepartMent.SelectedIndex > 0)
            {
                objDB = new SQLDB();
                DataView dvDesg = objDB.ExecuteDataSet(" SELECT DISTINCT CAST(DESIG_CODE AS VARCHAR)+'@'+ISNULL(CAST(ldm_elevel_id AS VARCHAR),'0') DESIG_CODE,Desig_Name From DESIG_MAS"+
                                    " LEFT JOIN LevelsDesig_mas ON LDM_DESIG_ID = desig_code WHERE DEPT_CODE=" + cmbDepartMent.SelectedValue + " order by Desig_Name asc", CommandType.Text).Tables[0].DefaultView;
                UtilityLibrary.PopulateControl(cmbDesignation, dvDesg, 1, 0, "--PLEASE SELECT--", 0);
            }
        }
        
       
        private string GenNewRefNo(string sComp,string sBranch,string sFin,string sPType)
        {
            string imax = "";
            //string sqlText = "";
            objDB = new SQLDB();
            DataTable dt = new DataTable();
            imax = "SSGC/" + sFin.Substring(2, 2) + "-" + sFin.Substring(7, 2) + "/APPT/";
            if (sPType != "")
            {
                dt = objDB.ExecuteDataSet("SELECT ISNULL(Max(Substring(ISNULL(HESS_LTR_REF_NO,'" + (imax) + "')," + (imax.Length+1) + "," + (imax.Length + 5) + ")),0)+1 FROM HR_EMP_SAL_STRU WHERE HESS_PROMOTION_CATEGORY_CODE='GEN' AND HESS_LTR_REF_NO LIKE '" + imax + "%'").Tables[0];
            }
            if (dt.Rows.Count > 0)
            {
                imax += Convert.ToInt32(dt.Rows[0][0]).ToString("00000");
            }
            objDB = null;
            return imax;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string sqlText = "";
            int iMax = 0;
            objDB = new SQLDB();
            int iRes = 0;            
            if (CheckData())
            {
                string sRefNo = "";
                if (isUpDate == false)
                    sRefNo = GenNewRefNo(cmbCompany.SelectedValue.ToString(), cmbBranch.SelectedValue.ToString(), CommonData.FinancialYear, "APPT");
                string[] sDesg = cmbDesignation.SelectedValue.ToString().Split('@');
                Int32 iBasic, iHra, iCca, iConv, iLta, iSpec, iUnif, iVeh, iChild, iBnp, iMed, iPet, iGross;
                iBasic = iHra = iCca = iConv = iLta = iSpec = iUnif = iVeh = iChild = iBnp = iMed = iPet = iGross = 0;
                if (txtSalBasic.Text.Length > 0)
                    iBasic = Convert.ToInt32(txtSalBasic.Text);
                if (txtSalHRA.Text.Length > 0)
                    iHra = Convert.ToInt32(txtSalHRA.Text);
                if (txtSalCCA.Text.Length > 0)
                    iCca = Convert.ToInt32(txtSalCCA.Text);
                if (txtSalConvAllw.Text.Length > 0)
                    iConv = Convert.ToInt32(txtSalConvAllw.Text);
                if (txtSalLTAAllw.Text.Length > 0)
                    iLta = Convert.ToInt32(txtSalLTAAllw.Text);
                if (txtSalSpecialAllw.Text.Length > 0)
                    iSpec = Convert.ToInt32(txtSalSpecialAllw.Text);
                if (txtSalUniformAllw.Text.Length > 0)
                    iUnif = Convert.ToInt32(txtSalUniformAllw.Text);
                if (txtVehicleAllw.Text.Length > 0)
                    iVeh = Convert.ToInt32(txtVehicleAllw.Text);
                if (txtSalChildAllw.Text.Length > 0)
                    iChild = Convert.ToInt32(txtSalChildAllw.Text);
                if (txtSalBNPAllw.Text.Length > 0)
                    iBnp = Convert.ToInt32(txtSalBNPAllw.Text);
                if (txtSalMedAllw.Text.Length > 0)
                    iMed = Convert.ToInt32(txtSalMedAllw.Text);
                if (txtSalPetrAllw.Text.Length > 0)
                    iPet = Convert.ToInt32(txtSalPetrAllw.Text);
                iGross = iBasic + iHra + iCca + iConv + iLta + iSpec + iUnif + iVeh + iChild + iBnp + iMed + iPet;
                try
                {
                    objDB = new SQLDB();
                    iMax = Convert.ToInt32(objDB.ExecuteDataSet(" select IsNull(Max(HESS_TRXN_NO),0)+1 from HR_EMP_SAL_STRU").Tables[0].Rows[0][0]);
                    if (isUpDate == false && iMax > 0)
                    {
                        sqlText = " INSERT INTO HR_EMP_SAL_STRU(HESS_FROM_COMPANY_CODE,HESS_FROM_BRANCH_CODE,HESS_TO_COMPANY_CODE,HESS_TO_BRANCH_CODE,HESS_APPL_NUMBER" +
                                ",HESS_EORA_CODE,HESS_PROMOTION_CATEGORY_CODE,HESS_TRXN_NO,HESS_TRXN_DATE,HESS_SALSTRU_TYPE,HESS_SALSTRU_CODE,HESS_LTR_REF_NO" +
                                ",HESS_LTR_REF_DATE,HESS_EFF_DATE,HESS_FROM_DESIG_ID,HESS_DESIG_ID,HESS_FROM_DEPT_ID,HESS_TO_DEPT_ID,HESS_REPO_TO_ECODE" +
                                ",HESS_REPO_TO_BRANCH_CODE,HESS_REMARKS,HESS_BASIC,HESS_HRA,HESS_CCA,HESS_CONV_ALW,HESS_LTA_ALW,HESS_SPL_ALW,HESS_UNF_ALW" +
                                ",HESS_VEH_ALW,HESS_CH_ED_ALW,HESS_BNP_ALW,HESS_MED_REIMB,HESS_PET_ALW,HESS_AUTH_SIGN,HESS_APPR_STATUS,HESS_CREATED_BY,HESS_CREATED_DATE) VALUES(" +
                                "'" + dtEmpInfo.Rows[0]["CompCode"] + "','" + dtEmpInfo.Rows[0]["BranchCode"] + "','" + cmbCompany.SelectedValue + 
                                "','" + cmbBranch.SelectedValue + "','" + dtEmpInfo.Rows[0]["ApplNumber"] + "'," + dtEmpInfo.Rows[0]["Ecode"] + 
                                ",'GEN'," + iMax + ",'" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                "','" + cmbSalStrType.SelectedValue + "','" + cmbSalStrCode.SelectedValue + "','" + sRefNo + "','" + Convert.ToDateTime(dtpEffDate.Value).ToString("dd/MMM/yyyy") + 
                                "','" + Convert.ToDateTime(dtpEffDate.Value).ToString("dd/MMM/yyyy") + "'," + dtEmpInfo.Rows[0]["DesgID"] + "," + sDesg[0] + 
                                "," + dtEmpInfo.Rows[0]["DeptCode"] + "," + cmbDepartMent.SelectedValue + "," + txtReportingEcode.Text + ",'" + cmbRepToBranch.SelectedValue + 
                                "','" + txtRemarks.Text + "'," + iBasic + "," + iHra + "," + iCca + "," + iConv + "," + iLta + "," + iSpec + "," + iUnif + "," + iVeh + 
                                "," + iChild + "," + iBnp + "," + iMed + "," + iPet + "," + 0 + ",'A','" + CommonData.LogUserId + "',getdate())";
                    }
                    else
                    {
                        sqlText = " UPDATE HR_EMP_SAL_STRU SET HESS_TO_COMPANY_CODE='" + cmbCompany.SelectedValue + "',HESS_TO_BRANCH_CODE='" + cmbBranch.SelectedValue + "'" +
                                ",HESS_SALSTRU_TYPE='" + cmbSalStrType.SelectedValue + "',HESS_SALSTRU_CODE='" + cmbSalStrCode.SelectedValue +
                                "',HESS_EFF_DATE='" + Convert.ToDateTime(dtpEffDate.Value).ToString("dd/MMM/yyyy") + "',HESS_TO_DEPT_ID='" + cmbDepartMent.SelectedValue + "'" +
                                ",HESS_DESIG_ID=" + sDesg[0] + ",HESS_REPO_TO_ECODE=" + txtReportingEcode.Text + ",HESS_REPO_TO_BRANCH_CODE='" + cmbRepToBranch.SelectedValue + "',HESS_REMARKS='" + txtRemarks.Text + "'" +
                                ",HESS_BASIC=" + iBasic + ",HESS_HRA=" + iHra + ",HESS_CCA=" + iCca + ",HESS_CONV_ALW=" + iConv + ",HESS_LTA_ALW=" + iLta + "" +
                                ",HESS_SPL_ALW=" + iSpec + ",HESS_UNF_ALW=" + iUnif + ",HESS_VEH_ALW=" + iVeh + ",HESS_CH_ED_ALW=" + iChild + ",HESS_BNP_ALW=" + iBnp +
                                ",HESS_MED_REIMB=" + iMed + ",HESS_PET_ALW='" + iPet + "' WHERE HESS_EORA_CODE='" + dtEmpSal.Rows[0]["HESS_EORA_CODE"].ToString() + "' AND HESS_TRXN_NO='" + dtEmpSal.Rows[0]["HESS_TRXN_NO"].ToString() + "'";
                    }
                    if (sqlText.Length > 10)
                        iRes = objDB.ExecuteSaveData(sqlText);
                    if (iRes > 0)
                    {
                        sqlText = " UPDATE EORA_MASTER SET BRANCH_CODE='" + cmbBranch.SelectedValue.ToString() + "'" +
                                    ",company_code='" + cmbCompany.SelectedValue.ToString() + "'" +
                                    ",DEPT_ID='" + cmbDepartMent.SelectedValue.ToString() + "'" +
                                    ",DESG_ID='" + sDesg[0] + "'" +
                                    ",DESIG='" + cmbDesignation.SelectedText.ToString() + "'" +
                                    ",HRIS_DESIG='" + cmbDesignation.SelectedText.ToString() + "'" +
                                    ",HRIS_DESIG_ID='" + sDesg[0] + "'" +
                                    ",elevel_id='" + sDesg[1] + "'" +
                                    "WHERE ECODE = '" + txtEcodeSearch.Text + "'";
                        iRes = objDB.ExecuteSaveData(sqlText);
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
                if (iRes > 0)
                {
                    MessageBox.Show("Data Saved Successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClear_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool CheckData()
        {
            bool bFlag = true;
            if (txtMemberName.Text == "")
            {
                MessageBox.Show("Enter Valid Ecode", "Promotion Board", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }            
            if (cmbBranch.SelectedIndex <= 0)
            {
                MessageBox.Show("Select Valid Transfer To Branch", "Promotion Board", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (cmbDesignation.SelectedIndex <= 0)
            {
                MessageBox.Show("Select Valid Designation", "Promotion Board", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (txtRecruiterName.Text == "")
            {
                MessageBox.Show("Enter Valid Reporting Ecode", "Promotion Board", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (cmbRepToBranch.SelectedIndex <= 0)
            {
                MessageBox.Show("Select Valid Reporting To Branch", "Promotion Board", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }            
            return bFlag;
        }

        private void txtSalBasic_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotals();
        }

        private void CalculateTotals()
        {
            Int32 iBasic, iHra, iCca, iConv, iLta, iSpec, iUnif, iVeh, iChild, iBnp, iMed, iPet, iGross;
            iBasic = iHra = iCca = iConv = iLta = iSpec = iUnif = iVeh = iChild = iBnp = iMed = iPet = iGross = 0;
            if (txtSalBasic.Text.Length > 0)
                iBasic = Convert.ToInt32(txtSalBasic.Text);
            if (txtSalHRA.Text.Length > 0)
                iHra = Convert.ToInt32(txtSalHRA.Text);
            if (txtSalCCA.Text.Length > 0)
                iCca = Convert.ToInt32(txtSalCCA.Text);
            if (txtSalConvAllw.Text.Length > 0)
                iConv = Convert.ToInt32(txtSalConvAllw.Text);
            if (txtSalLTAAllw.Text.Length > 0)
                iLta = Convert.ToInt32(txtSalLTAAllw.Text);
            if (txtSalSpecialAllw.Text.Length > 0)
                iSpec = Convert.ToInt32(txtSalSpecialAllw.Text);
            if (txtSalUniformAllw.Text.Length > 0)
                iUnif = Convert.ToInt32(txtSalUniformAllw.Text);
            if (txtVehicleAllw.Text.Length > 0)
                iVeh = Convert.ToInt32(txtVehicleAllw.Text);
            if (txtSalChildAllw.Text.Length > 0)
                iChild = Convert.ToInt32(txtSalChildAllw.Text);
            if (txtSalBNPAllw.Text.Length > 0)
                iBnp = Convert.ToInt32(txtSalBNPAllw.Text);
            if (txtSalMedAllw.Text.Length > 0)
                iMed = Convert.ToInt32(txtSalMedAllw.Text);
            if (txtSalPetrAllw.Text.Length > 0)
                iPet = Convert.ToInt32(txtSalPetrAllw.Text);
            iGross = Convert.ToInt32(iBasic + iHra + iCca + iConv + iLta + iSpec + iUnif + iVeh + iChild + iBnp + iMed + iPet);
            txtSalGross.Text = iGross.ToString("0");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {            
            txtSalBasic.Text = "";
            txtSalBNPAllw.Text = "";
            txtSalHRA.Text = "";
            txtSalCCA.Text = "";
            txtSalChildAllw.Text = "";
            txtSalConvAllw.Text = "";
            txtSalGross.Text = "";
            txtSalLTAAllw.Text = "";
            txtSalMedAllw.Text = "";
            txtSalPetrAllw.Text = "";
            txtSalSpecialAllw.Text = "";
            txtSalUniformAllw.Text = "";
            txtVehicleAllw.Text = "";
            txtRecruiterName.Text = "";
            txtReportingEcode.Text = "";
            txtRemarks.Text = "";
            txtMemberName.Text = "";
            txtFatherName.Text = "";
            txtHRISDesig.Text = "";
            txtDept.Text = "";
            txtBranch.Text = "";
            txtComp.Text = "";
            meHRISDataofBirth.Text = "";
            meHRISDateOfJoin.Text = "";
            picEmp.BackgroundImage = null;
            cmbBranch.SelectedIndex = 0;
            cmbCompany.SelectedIndex = 0;
            cmbDesignation.SelectedIndex = 0;
            cmbDepartMent.SelectedIndex = 0;
            cmbRepToBranch.SelectedIndex = 0;            
            dtpEffDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            txtEcodeSearch.Text = "";
        }

        private void cmbSalStrType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSalStrType.SelectedIndex > 0)
            {
                objMstr = new Master();
                DataTable dtSalCode = objMstr.GetHRSalStructureTypes(cmbSalStrType.SelectedValue.ToString(), "").Tables[0];
                UtilityLibrary.PopulateControl(cmbSalStrCode, dtSalCode.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
                objMstr = null;
                
            }
        }

        private void cmbSalStrCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSalStrCode.SelectedIndex > 0)
            {
                objMstr = new Master();
                DataTable dtSalStr = objMstr.GetHRSalStructureTypes(cmbSalStrType.SelectedValue.ToString(), cmbSalStrCode.SelectedValue.ToString()).Tables[0];                
                objMstr = null;
                if (dtSalStr.Rows.Count > 0)
                {
                    txtSalBasic.Text = dtSalStr.Rows[0]["HSSM_BASIC"].ToString();
                    txtSalBNPAllw.Text = dtSalStr.Rows[0]["HSSM_BNP_ALW"].ToString();
                    txtSalCCA.Text = dtSalStr.Rows[0]["HSSM_CCA"].ToString();
                    txtSalChildAllw.Text = dtSalStr.Rows[0]["HSSM_CH_ED_ALW"].ToString();
                    txtSalConvAllw.Text = dtSalStr.Rows[0]["HSSM_CONV_ALW"].ToString();
                    txtSalGross.Text = dtSalStr.Rows[0]["HSSM_GROSS"].ToString();
                    txtSalHRA.Text = dtSalStr.Rows[0]["HSSM_HRA"].ToString();
                    txtSalLTAAllw.Text = dtSalStr.Rows[0]["HSSM_LTA_ALW"].ToString();
                    txtSalMedAllw.Text = dtSalStr.Rows[0]["HSSM_MED_REIMB"].ToString();
                    txtSalPetrAllw.Text = dtSalStr.Rows[0]["HSSM_PET_ALW"].ToString();
                    txtSalSpecialAllw.Text = dtSalStr.Rows[0]["HSSM_SPL_ALW"].ToString();
                    txtSalUniformAllw.Text = dtSalStr.Rows[0]["HSSM_UNF_ALW"].ToString();
                    txtVehicleAllw.Text = dtSalStr.Rows[0]["HSSM_VEH_ALW"].ToString();                    
                }

            }
        }

        
    }
}
