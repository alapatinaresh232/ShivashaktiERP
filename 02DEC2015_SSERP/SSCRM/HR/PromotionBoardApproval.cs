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
namespace SSCRM
{
    public partial class PromotionBoardApproval : Form
    {
        private SQLDB objDB = null;
        private Master objMstr = null;
        private HRInfo objHRInfo = null;
        private PromotionBoard objPromotionBoard = null;
        private int iFormType = 0;
        public PromotionBoardApproval()
        {
            InitializeComponent();
        }
        public PromotionBoardApproval(int iForm)
        {
            iFormType = iForm;
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void PromotionBoardApproval_Load(object sender, EventArgs e)
        {
            if (iFormType == 1)
            {
                lblCat.Visible = false;
                cmbPBType.Visible = false;
                lblBranch.Visible = false;
                cmbRepToBranch.Visible = false;
                this.Text = "Agent Appointment Letter Approval";
            }
            else
            {
                lblCat.Visible = true;
                cmbPBType.Visible = true;
            }
            gvPromotiomDetl.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);
            GetPopupdropDown();
            FillPromotionsToGrid();
        }

        public void FillPromotionsToGrid()
        {
            string sqlText = "";
            DataTable dt = new DataTable();
            //objDB = new SQLDB();
            objHRInfo = new HRInfo();
            gvPromotiomDetl.Rows.Clear();
            string sBranch = "", sPBType = "", sDetlType = "";
            if (iFormType == 1)
            {
                sDetlType = "AGENT_APPROVAL";
            }
            else
            {
                if (cmbPBType.SelectedIndex == 0 && cmbRepToBranch.SelectedIndex == 0)
                {
                    //sqlText = "SELECT HESS_TRXN_NO,HESS_LTR_REF_NO,HESS_APPL_NUMBER,HESS_EORA_CODE,MEMBER_NAME,HESS_TO_BRANCH_CODE,HESS_PROMOTION_CATEGORY_CODE" +
                    //        ",HPCM_PROMOTION_CATEGORY_NAME,HESS_BASIC,HESS_HRA,HESS_CCA,HESS_CONV_ALW,HESS_LTA_ALW,HESS_SPL_ALW,HESS_TO_COMPANY_CODE,HESS_UNF_ALW" +
                    //        ",HESS_VEH_ALW,HESS_CH_ED_ALW,HESS_BNP_ALW,HESS_MED_REIMB,HESS_PET_ALW,(HESS_BASIC+HESS_HRA+HESS_CCA+HESS_CONV_ALW+" +
                    //        "HESS_LTA_ALW+HESS_SPL_ALW+HESS_UNF_ALW+HESS_VEH_ALW+HESS_CH_ED_ALW+HESS_BNP_ALW+HESS_MED_REIMB+HESS_PET_ALW) HESS_GROSS_SAL" +
                    //        ",HESS_EFF_DATE,ISNULL(elevel_id,0) ELEVEL_ID,HESS_DESIG_ID,HESS_TO_DEPT_ID,desig_name FROM HR_EMP_SAL_STRU INNER JOIN EORA_MASTER ON ECODE = HESS_EORA_CODE INNER JOIN HR_PROMOTION_CATEGORY_MASTER " +
                    //        "ON HPCM_PROMOTION_CATEGORY_CODE = HESS_PROMOTION_CATEGORY_CODE LEFT JOIN LevelsDesig_mas ON ldm_company_code = HESS_TO_COMPANY_CODE AND LDM_DESIG_ID = HESS_DESIG_ID INNER JOIN DESIG_MAS ON desig_code = HESS_DESIG_ID " +
                    //        "WHERE HESS_APPR_STATUS='P' ORDER BY HESS_EFF_DATE ASC";
                    sDetlType = "ALL";
                }
                else if (cmbPBType.SelectedIndex > 0 && cmbRepToBranch.SelectedIndex == 0)
                {
                    //sqlText = "SELECT HESS_TRXN_NO,HESS_LTR_REF_NO,HESS_APPL_NUMBER,HESS_EORA_CODE,MEMBER_NAME,HESS_TO_BRANCH_CODE,HESS_PROMOTION_CATEGORY_CODE" +
                    //        ",HPCM_PROMOTION_CATEGORY_NAME,HESS_BASIC,HESS_HRA,HESS_CCA,HESS_CONV_ALW,HESS_LTA_ALW,HESS_SPL_ALW,HESS_TO_COMPANY_CODE,HESS_UNF_ALW" +
                    //        ",HESS_VEH_ALW,HESS_CH_ED_ALW,HESS_BNP_ALW,HESS_MED_REIMB,HESS_PET_ALW,(HESS_BASIC+HESS_HRA+HESS_CCA+HESS_CONV_ALW+"+
                    //        "HESS_LTA_ALW+HESS_SPL_ALW+HESS_UNF_ALW+HESS_VEH_ALW+HESS_CH_ED_ALW+HESS_BNP_ALW+HESS_MED_REIMB+HESS_PET_ALW) HESS_GROSS_SAL"+
                    //        ",HESS_EFF_DATE,ISNULL(elevel_id,0) ELEVEL_ID,HESS_DESIG_ID,HESS_TO_DEPT_ID,desig_name FROM HR_EMP_SAL_STRU INNER JOIN EORA_MASTER ON ECODE = HESS_EORA_CODE INNER JOIN HR_PROMOTION_CATEGORY_MASTER " +
                    //        "ON HPCM_PROMOTION_CATEGORY_CODE = HESS_PROMOTION_CATEGORY_CODE LEFT JOIN LevelsDesig_mas ON ldm_company_code = HESS_TO_COMPANY_CODE AND LDM_DESIG_ID = HESS_DESIG_ID INNER JOIN DESIG_MAS ON desig_code = HESS_DESIG_ID WHERE HESS_APPR_STATUS='P' " +
                    //        "AND HESS_PROMOTION_CATEGORY_CODE='" + cmbPBType.SelectedValue + "' ORDER BY HESS_EFF_DATE ASC";
                    sPBType = cmbPBType.SelectedValue.ToString();
                    sDetlType = "BYCAT";
                }
                else if (cmbPBType.SelectedIndex > 0 && cmbRepToBranch.SelectedIndex > 0)
                {
                    //sqlText = "SELECT HESS_TRXN_NO,HESS_LTR_REF_NO,HESS_APPL_NUMBER,HESS_EORA_CODE,MEMBER_NAME,HESS_TO_BRANCH_CODE,HESS_PROMOTION_CATEGORY_CODE" +
                    //         ",HPCM_PROMOTION_CATEGORY_NAME,HESS_BASIC,HESS_HRA,HESS_CCA,HESS_CONV_ALW,HESS_LTA_ALW,HESS_SPL_ALW,HESS_TO_COMPANY_CODE,HESS_UNF_ALW" +
                    //         ",HESS_VEH_ALW,HESS_CH_ED_ALW,HESS_BNP_ALW,HESS_MED_REIMB,HESS_PET_ALW,(HESS_BASIC+HESS_HRA+HESS_CCA+HESS_CONV_ALW+" +
                    //         "HESS_LTA_ALW+HESS_SPL_ALW+HESS_UNF_ALW+HESS_VEH_ALW+HESS_CH_ED_ALW+HESS_BNP_ALW+HESS_MED_REIMB+HESS_PET_ALW) HESS_GROSS_SAL" +
                    //         ",HESS_EFF_DATE,ISNULL(elevel_id,0) ELEVEL_ID,HESS_DESIG_ID,HESS_TO_DEPT_ID,desig_name FROM HR_EMP_SAL_STRU INNER JOIN EORA_MASTER ON ECODE = HESS_EORA_CODE INNER JOIN HR_PROMOTION_CATEGORY_MASTER " +
                    //        "ON HPCM_PROMOTION_CATEGORY_CODE = HESS_PROMOTION_CATEGORY_CODE LEFT JOIN LevelsDesig_mas ON ldm_company_code = HESS_TO_COMPANY_CODE AND LDM_DESIG_ID = HESS_DESIG_ID INNER JOIN DESIG_MAS ON desig_code = HESS_DESIG_ID WHERE HESS_APPR_STATUS='P' " +
                    //        "AND HESS_PROMOTION_CATEGORY_CODE='" + cmbPBType.SelectedValue + "' AND HESS_FROM_BRANCH_CODE = '" + cmbRepToBranch.SelectedValue +
                    //        "' ORDER BY HESS_EFF_DATE ASC";
                    sPBType = cmbPBType.SelectedValue.ToString();
                    sBranch = cmbRepToBranch.SelectedValue.ToString();
                    sDetlType = "BYCATBR";
                }
                else if (cmbPBType.SelectedIndex == 0 && cmbRepToBranch.SelectedIndex > 0)
                {
                    //sqlText = "SELECT HESS_TRXN_NO,HESS_LTR_REF_NO,HESS_APPL_NUMBER,HESS_EORA_CODE,MEMBER_NAME,HESS_TO_BRANCH_CODE,HESS_PROMOTION_CATEGORY_CODE" +
                    //         ",HPCM_PROMOTION_CATEGORY_NAME,HESS_BASIC,HESS_HRA,HESS_CCA,HESS_CONV_ALW,HESS_LTA_ALW,HESS_SPL_ALW,HESS_TO_COMPANY_CODE,HESS_UNF_ALW" +
                    //         ",HESS_VEH_ALW,HESS_CH_ED_ALW,HESS_BNP_ALW,HESS_MED_REIMB,HESS_PET_ALW,(HESS_BASIC+HESS_HRA+HESS_CCA+HESS_CONV_ALW+" +
                    //         "HESS_LTA_ALW+HESS_SPL_ALW+HESS_UNF_ALW+HESS_VEH_ALW+HESS_CH_ED_ALW+HESS_BNP_ALW+HESS_MED_REIMB+HESS_PET_ALW) HESS_GROSS_SAL" +
                    //         ",HESS_EFF_DATE,ISNULL(elevel_id,0) ELEVEL_ID,HESS_DESIG_ID,HESS_TO_DEPT_ID,desig_name FROM HR_EMP_SAL_STRU INNER JOIN EORA_MASTER ON ECODE = HESS_EORA_CODE INNER JOIN HR_PROMOTION_CATEGORY_MASTER " +
                    //        "ON HPCM_PROMOTION_CATEGORY_CODE = HESS_PROMOTION_CATEGORY_CODE LEFT JOIN LevelsDesig_mas ON ldm_company_code = HESS_TO_COMPANY_CODE AND LDM_DESIG_ID = HESS_DESIG_ID INNER JOIN DESIG_MAS ON desig_code = HESS_DESIG_ID " +
                    //        "WHERE HESS_APPR_STATUS='P' AND HESS_FROM_BRANCH_CODE = '" + cmbRepToBranch.SelectedValue + "' ORDER BY HESS_EFF_DATE ASC";
                    sBranch = cmbRepToBranch.SelectedValue.ToString();
                    sDetlType = "BYBR";
                }
            }
            //if (sqlText.Length > 10)
            //    dt = objDB.ExecuteDataSet(sqlText).Tables[0];
            try
            {
                dt = objHRInfo.GetPromotionsDetlsForApproval("", sBranch, sPBType, sDetlType).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objHRInfo = null;
            }
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataGridViewRow tempRow = new DataGridViewRow();
                    DataGridViewCell tempSlNo = new DataGridViewTextBoxCell();
                    tempSlNo.Value = i + 1;
                    tempRow.Cells.Add(tempSlNo);

                    DataGridViewCell tempTrnNo = new DataGridViewTextBoxCell();
                    tempTrnNo.Value = dt.Rows[i]["HESS_TRXN_NO"].ToString();
                    tempRow.Cells.Add(tempTrnNo);

                    DataGridViewCell tempRefNo = new DataGridViewTextBoxCell();
                    tempRefNo.Value = dt.Rows[i]["HESS_LTR_REF_NO"].ToString();
                    tempRow.Cells.Add(tempRefNo);

                    DataGridViewCell tempApplNo = new DataGridViewTextBoxCell();
                    tempApplNo.Value = dt.Rows[i]["HESS_APPL_NUMBER"].ToString();
                    tempRow.Cells.Add(tempApplNo);

                    DataGridViewCell tempEcode = new DataGridViewTextBoxCell();
                    tempEcode.Value = dt.Rows[i]["HESS_EORA_CODE"].ToString();
                    tempRow.Cells.Add(tempEcode);

                    DataGridViewCell tempName = new DataGridViewTextBoxCell();
                    tempName.Value = dt.Rows[i]["MEMBER_NAME"].ToString();
                    tempRow.Cells.Add(tempName);

                    DataGridViewCell tempBCode = new DataGridViewTextBoxCell();
                    tempBCode.Value = dt.Rows[i]["HESS_TO_BRANCH_CODE"].ToString();
                    tempRow.Cells.Add(tempBCode);

                    DataGridViewCell tempCCode = new DataGridViewTextBoxCell();
                    tempCCode.Value = dt.Rows[i]["HESS_TO_COMPANY_CODE"].ToString();
                    tempRow.Cells.Add(tempCCode);

                    DataGridViewCell tempDesig = new DataGridViewTextBoxCell();
                    tempDesig.Value = dt.Rows[i]["desig_name"].ToString();
                    tempRow.Cells.Add(tempDesig);

                    DataGridViewCell tempDesigID = new DataGridViewTextBoxCell();
                    tempDesigID.Value = dt.Rows[i]["HESS_DESIG_ID"].ToString();
                    tempRow.Cells.Add(tempDesigID);

                    DataGridViewCell tempDeptID = new DataGridViewTextBoxCell();
                    tempDeptID.Value = dt.Rows[i]["HESS_TO_DEPT_ID"].ToString();
                    tempRow.Cells.Add(tempDeptID);

                    DataGridViewCell tempElevelID = new DataGridViewTextBoxCell();
                    tempElevelID.Value = dt.Rows[i]["ELEVEL_ID"].ToString();
                    tempRow.Cells.Add(tempElevelID);

                    DataGridViewCell tempPCat = new DataGridViewTextBoxCell();
                    tempPCat.Value = dt.Rows[i]["HPCM_PROMOTION_CATEGORY_NAME"].ToString();
                    tempRow.Cells.Add(tempPCat);

                    DataGridViewCell tempSal = new DataGridViewTextBoxCell();
                    tempSal.Value = dt.Rows[i]["HESS_GROSS_SAL"].ToString();
                    tempRow.Cells.Add(tempSal);

                    DataGridViewCell tempEff = new DataGridViewTextBoxCell();
                    tempEff.Value = Convert.ToDateTime(dt.Rows[i]["HESS_EFF_DATE"].ToString()).ToString("dd/MMM/yyyy").ToUpper();
                    tempRow.Cells.Add(tempEff);

                    gvPromotiomDetl.Rows.Add(tempRow);
                }
            }
            objDB = null;
            dt = null;
        }

        public void GetPopupdropDown()
        {
            objDB = new SQLDB();
            DataView dtRBranch = objDB.ExecuteDataSet("SELECT BRANCH_CODE,BRANCH_NAME FROM BRANCH_MAS WHERE ACTIVE='T' ORDER BY BRANCH_NAME ASC", CommandType.Text).Tables[0].DefaultView;
            UtilityLibrary.PopulateControl(cmbRepToBranch, dtRBranch, 1, 0, "--PLEASE SELECT--", 0);
            objDB = null;
            objDB = new SQLDB();
            DataView dtPCat = objDB.ExecuteDataSet("SELECT HPCM_PROMOTION_CATEGORY_CODE,HPCM_PROMOTION_CATEGORY_NAME FROM HR_PROMOTION_CATEGORY_MASTER ORDER BY HPCM_PROMOTION_CATEGORY_NAME ASC", CommandType.Text).Tables[0].DefaultView;
            UtilityLibrary.PopulateControl(cmbPBType, dtPCat, 1, 0, "--PLEASE SELECT--", 0);
            objDB = null;
        }

        private void cmbPBType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillPromotionsToGrid();
        }

        private void gvPromotiomDetl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex > -1)
                {
                    GetEmpSalDetails(gvPromotiomDetl.Rows[e.RowIndex].Cells["ApplNo"].Value.ToString(), gvPromotiomDetl.Rows[e.RowIndex].Cells["TrnNo"].Value.ToString());
                    if (e.ColumnIndex == gvPromotiomDetl.Columns["Select"].Index)
                    {
                        bool cbchecked = (bool)gvPromotiomDetl.Rows[e.RowIndex].Cells["Select"].EditedFormattedValue;
                        if (cbchecked == true)
                        {
                            gvPromotiomDetl.Rows[e.RowIndex].Cells["Appr"].Value = "YES";
                            //DataGridViewImageColumn tempCell = new DataGridViewImageColumn();
                            //tempCell.Image = Properties.Resources.actions_edit;
                            //gvPromotiomDetl.Rows[e.RowIndex].Cells["Sel"].Value = Properties.Resources.actions_edit;
                            GetEmpSalDetails(gvPromotiomDetl.Rows[e.RowIndex].Cells["ApplNo"].Value.ToString(), gvPromotiomDetl.Rows[e.RowIndex].Cells["TrnNo"].Value.ToString());
                        }
                        else
                        {
                            gvPromotiomDetl.Rows[e.RowIndex].Cells["Appr"].Value = "NO";
                            //DataGridViewImageColumn tempCell = new DataGridViewImageColumn();
                            //tempCell.Image = Properties.Resources.actions_delete;
                            //gvPromotiomDetl.Rows[e.RowIndex].Cells["Sel"].Value = Properties.Resources.actions_delete;
                            txtRefNo.Text = meWef.Text = txtDept.Text = txtDesig.Text = txtComp.Text = txtBranch.Text = txtRemarks.Text = "";
                            txtReportToName.Text = txtReportingBranch.Text = txtSalGross.Text = txtSalBasic.Text = txtSalHRA.Text = txtSalCCA.Text = "";
                            txtSalConvAllw.Text = txtSalLTAAllw.Text = txtSalSpecialAllw.Text = txtSalUniformAllw.Text = txtVehicleAllw.Text = "";
                            txtSalChildAllw.Text = txtSalBNPAllw.Text = txtSalMedAllw.Text = txtSalPetrAllw.Text = txtPromType.Text = "";
                        }
                    }
                    else if (e.ColumnIndex == gvPromotiomDetl.Columns["Print"].Index)
                    {
                        try
                        {
                            GetEmpSalDetails(gvPromotiomDetl.Rows[e.RowIndex].Cells["ApplNo"].Value.ToString(), gvPromotiomDetl.Rows[e.RowIndex].Cells["TrnNo"].Value.ToString());
                            string strCmd = " SELECT HLPH_LETTER_REF_NO,HLPH_APPL_NO FROM " +
                                     "HR_PB_LETTER_PRINT_HIST " +
                                     " WHERE HLPH_APPL_NO=" + Convert.ToInt32(gvPromotiomDetl.Rows[e.RowIndex].Cells["ApplNo"].Value.ToString()) +
                                     " AND HLPH_LETTER_REF_NO='" + txtRefNo.Text +
                                     "'";
                            objDB = new SQLDB();
                            DataTable dt = objDB.ExecuteDataSet(strCmd).Tables[0];
                            if (dt.Rows.Count == 0)
                            {
                                //INC
                                //PRM
                                //PRM&INC
                                //REDSG
                                //TRN
                                //TRN&INC
                                //TRN&INC&PRM
                                //TRN&PRM

                                if (cmbPBType.SelectedValue.ToString() == "TRN")
                                {
                                    ReportViewer cldReportViewer = new ReportViewer("", "", gvPromotiomDetl.Rows[e.RowIndex].Cells["ApplNo"].Value.ToString(), gvPromotiomDetl.Rows[e.RowIndex].Cells["ApplNo"].Value.ToString(), gvPromotiomDetl.Rows[e.RowIndex].Cells["ProductID"].Value.ToString(), "TRN");
                                    CommonData.ViewReport = "PROMOTION_BOARD_TRN_LETTER";
                                    cldReportViewer.Show();
                                }
                                else if (cmbPBType.SelectedValue.ToString() == "INC" ||
                                    cmbPBType.SelectedValue.ToString() == "PRM" ||
                                    cmbPBType.SelectedValue.ToString() == "PRM&INC" ||
                                    cmbPBType.SelectedValue.ToString() == "REDSG" ||
                                    cmbPBType.SelectedValue.ToString() == "TRN&INC" ||
                                    cmbPBType.SelectedValue.ToString() == "TRN&INC&PRM" ||
                                    cmbPBType.SelectedValue.ToString() == "TRN&PRM")
                                {

                                }
                                else
                                {
                                    CommonData.ViewReport = "PRINT_SALES_APPT_LETTER";
                                    ReportViewer childReportViewer = new ReportViewer(Convert.ToInt32(gvPromotiomDetl.Rows[e.RowIndex].Cells["ApplNo"].Value).ToString(), gvPromotiomDetl.Rows[e.RowIndex].Cells["ProductID"].Value.ToString());
                                    childReportViewer.Show();
                                }

                                string strInsert = "INSERT INTO HR_PB_LETTER_PRINT_HIST(HLPH_APPL_NO " +
                                                                                     ", HLPH_PB_TYPE " +
                                                                                     ", HLPH_LETTER_REF_NO " +
                                                                                     ", HLPH_PRINT_DATE " +
                                                                                     ")VALUES" +
                                                                                     "(" + Convert.ToInt32(gvPromotiomDetl.Rows[e.RowIndex].Cells["ApplNo"].Value.ToString()) +
                                                                                     ",'" + cmbPBType.SelectedValue.ToString() + "','" + txtRefNo.Text +
                                                                                     "',getdate())";

                                if (strInsert.Length > 10)
                                {
                                 int   iRes = objDB.ExecuteSaveData(strInsert);
                                }


                            }

                            else
                            {
                                MessageBox.Show("Letter Already Printed", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    else if (e.ColumnIndex == gvPromotiomDetl.Columns["Edit"].Index && iFormType != 1)
                    {
                        objPromotionBoard = new PromotionBoard(gvPromotiomDetl.Rows[e.RowIndex].Cells["ApplNo"].Value.ToString(), gvPromotiomDetl.Rows[e.RowIndex].Cells["TrnNo"].Value.ToString());
                        objPromotionBoard.objPromotionBoardApproval = this;
                        objPromotionBoard.ShowDialog();
                    }
                    else if (e.ColumnIndex == gvPromotiomDetl.Columns["Delete"].Index)
                    {
                        if (iFormType != 1)
                        {
                            DeletePromotionBoardData(gvPromotiomDetl.Rows[e.RowIndex].Cells["ApplNo"].Value.ToString(), gvPromotiomDetl.Rows[e.RowIndex].Cells["TrnNo"].Value.ToString());

                        }//gvPromotiomDetl.Rows[e.RowIndex].Cells["ApplNo"].Value.ToString();
                        //gvPromotiomDetl.Rows[e.RowIndex].Cells["TrnNo"].Value.ToString();
                    }
                }
            }
        }

        private void DeletePromotionBoardData(string sApplNo, string sTrnNo)
        {
            objDB = new SQLDB();
            string sqlText = "";
            int iRes = 0;
            if (sApplNo != "" && sTrnNo != "")
            {
                try
                {
                    sqlText = "DELETE FROM HR_EMP_SAL_STRU WHERE HESS_TRXN_NO=" + sTrnNo + " AND HESS_APPL_NUMBER=" + sApplNo;
                    iRes = objDB.ExecuteSaveData(sqlText);
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
                    MessageBox.Show("SSERP-HR", "Deleted Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillPromotionsToGrid();
                }
                else
                {
                    MessageBox.Show("SSERP-HR", "Record Not Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void GetEmpSalDetails(string sApplNo, string sTrnNO)
        {
            DataTable dt = new DataTable();
            objDB = new SQLDB();
            string sqlText = "SELECT HESS_APPL_NUMBER,HESS_TRXN_NO,HESS_EORA_CODE,HESS_PROMOTION_CATEGORY_CODE,isnull(HPCM_PROMOTION_CATEGORY_NAME,'APPOINTMENT LETTER') HPCM_PROMOTION_CATEGORY_NAME" +
                            ",HESS_TO_COMPANY_CODE,CM_COMPANY_NAME,HESS_TO_BRANCH_CODE,BM.BRANCH_NAME,HESS_TO_DEPT_ID,dept_name,HESS_DESIG_ID" +
                            ",desig_name,HESS_SALSTRU_TYPE,HESS_SALSTRU_CODE,HESS_LTR_REF_NO,HESS_EFF_DATE,HESS_REPO_TO_ECODE" +
                            ",CAST(HESS_REPO_TO_ECODE AS VARCHAR)+'-'+MEMBER_NAME REPORTING_PERSON,HESS_REPO_TO_BRANCH_CODE" +
                            ",BR.BRANCH_NAME REPORTING_BRANCH,HESS_REMARKS,HESS_BASIC,HESS_HRA,HESS_CCA,HESS_CONV_ALW,HESS_LTA_ALW,HESS_SPL_ALW" +
                            ",HESS_UNF_ALW,HESS_VEH_ALW,HESS_CH_ED_ALW,HESS_BNP_ALW,HESS_MED_REIMB,HESS_PET_ALW " +
                            ",(HESS_BASIC+HESS_HRA+HESS_CCA+HESS_CONV_ALW+HESS_LTA_ALW+HESS_SPL_ALW+HESS_UNF_ALW+HESS_VEH_ALW+HESS_CH_ED_ALW+" +
                            "HESS_BNP_ALW+HESS_MED_REIMB+HESS_PET_ALW) HESS_GROSS_SAL FROM HR_EMP_SAL_STRU INNER JOIN BRANCH_MAS BM " +
                            "ON BM.BRANCH_CODE = HESS_TO_BRANCH_CODE INNER JOIN COMPANY_MAS ON CM_COMPANY_CODE = HESS_TO_COMPANY_CODE " +
                            "LEFT JOIN HR_PROMOTION_CATEGORY_MASTER ON HPCM_PROMOTION_CATEGORY_CODE = HESS_PROMOTION_CATEGORY_CODE " +
                            "INNER JOIN Dept_Mas ON Dept_Mas.dept_code = HESS_TO_DEPT_ID LEFT JOIN EORA_MASTER ON ECODE = HESS_REPO_TO_ECODE " +
                            "INNER JOIN BRANCH_MAS BR ON BR.BRANCH_CODE = HESS_REPO_TO_BRANCH_CODE INNER JOIN DESIG_MAS ON desig_code = HESS_DESIG_ID " +
                            "AND DESIG_MAS.dept_code = Dept_Mas.dept_code WHERE HESS_APPL_NUMBER = " + sApplNo + " AND HESS_TRXN_NO = " + sTrnNO + "";
            dt = objDB.ExecuteDataSet(sqlText).Tables[0];
            if (dt.Rows.Count > 0)
            {
                txtPromType.Text = dt.Rows[0]["HPCM_PROMOTION_CATEGORY_NAME"].ToString();
                txtRefNo.Text = dt.Rows[0]["HESS_LTR_REF_NO"].ToString();
                meWef.Text = Convert.ToDateTime(dt.Rows[0]["HESS_EFF_DATE"].ToString()).ToString("dd/MM/yyyy");
                txtDept.Text = dt.Rows[0]["dept_name"].ToString();
                txtDesig.Text = dt.Rows[0]["desig_name"].ToString();
                txtComp.Text = dt.Rows[0]["CM_COMPANY_NAME"].ToString();
                txtBranch.Text = dt.Rows[0]["BRANCH_NAME"].ToString();
                txtRemarks.Text = dt.Rows[0]["HESS_REMARKS"].ToString();
                txtReportToName.Text = dt.Rows[0]["REPORTING_PERSON"].ToString();
                txtReportingBranch.Text = dt.Rows[0]["REPORTING_BRANCH"].ToString();
                txtSalGross.Text = dt.Rows[0]["HESS_GROSS_SAL"].ToString();
                txtSalBasic.Text = dt.Rows[0]["HESS_BASIC"].ToString();
                txtSalHRA.Text = dt.Rows[0]["HESS_HRA"].ToString();
                txtSalCCA.Text = dt.Rows[0]["HESS_CCA"].ToString();
                txtSalConvAllw.Text = dt.Rows[0]["HESS_CONV_ALW"].ToString();
                txtSalLTAAllw.Text = dt.Rows[0]["HESS_LTA_ALW"].ToString();
                txtSalSpecialAllw.Text = dt.Rows[0]["HESS_SPL_ALW"].ToString();
                txtSalUniformAllw.Text = dt.Rows[0]["HESS_UNF_ALW"].ToString();
                txtVehicleAllw.Text = dt.Rows[0]["HESS_VEH_ALW"].ToString();
                txtSalChildAllw.Text = dt.Rows[0]["HESS_CH_ED_ALW"].ToString();
                txtSalBNPAllw.Text = dt.Rows[0]["HESS_BNP_ALW"].ToString();
                txtSalMedAllw.Text = dt.Rows[0]["HESS_MED_REIMB"].ToString();
                txtSalPetrAllw.Text = dt.Rows[0]["HESS_PET_ALW"].ToString();
            }
            else
            {                
                txtRefNo.Text = meWef.Text = txtDept.Text = txtDesig.Text = txtComp.Text = txtBranch.Text = txtRemarks.Text = "";
                txtReportToName.Text = txtReportingBranch.Text = txtSalGross.Text = txtSalBasic.Text = txtSalHRA.Text = txtSalCCA.Text = "";
                txtSalConvAllw.Text = txtSalLTAAllw.Text = txtSalSpecialAllw.Text = txtSalUniformAllw.Text = txtVehicleAllw.Text = "";
                txtSalChildAllw.Text = txtSalBNPAllw.Text = txtSalMedAllw.Text = txtSalPetrAllw.Text = txtPromType.Text = "";
            }
            dt = null;
            objDB = null;

        }
        
        private void gvPromotiomDetl_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 10)
            {
                if (e.RowIndex != -1)
                {
                    foreach (DataGridViewRow row in gvPromotiomDetl.Rows)
                    {
                        if (row.Cells["Appr"].Value.ToString() == "NO")
                            row.Cells[13].Value = false;
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbPBType.SelectedIndex = cmbRepToBranch.SelectedIndex = 0;
            txtRefNo.Text = meWef.Text = txtDept.Text = txtDesig.Text = txtComp.Text = txtBranch.Text = txtRemarks.Text = "";
            txtReportToName.Text = txtReportingBranch.Text = txtSalGross.Text = txtSalBasic.Text = txtSalHRA.Text = txtSalCCA.Text = "";
            txtSalConvAllw.Text = txtSalLTAAllw.Text = txtSalSpecialAllw.Text = txtSalUniformAllw.Text = txtVehicleAllw.Text = "";
            txtSalChildAllw.Text = txtSalBNPAllw.Text = txtSalMedAllw.Text = txtSalPetrAllw.Text = txtPromType.Text = "";
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            string sqlText = "";
            int iRes = 0;
            objDB = new SQLDB();
            if (CheckData())
            {
                try
                {
                    for (int i = 0; i < gvPromotiomDetl.Rows.Count; i++)
                    {
                        if ((bool)gvPromotiomDetl.Rows[i].Cells["Select"].EditedFormattedValue == true)
                        {
                            sqlText += " UPDATE HR_EMP_SAL_STRU SET HESS_APPR_STATUS='A',HESS_APPR_BY='" + CommonData.LogUserEcode +
                                "',HESS_APPR_DATE='" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                "' WHERE HESS_APPL_NUMBER=" + gvPromotiomDetl.Rows[i].Cells["ApplNo"].Value +
                                " AND HESS_TRXN_NO=" + gvPromotiomDetl.Rows[i].Cells["TrnNo"].Value;
                            sqlText += " UPDATE EORA_MASTER SET BRANCH_CODE='" + gvPromotiomDetl.Rows[i].Cells["BranchCode"].Value + "'" +
                                        ",company_code='" + gvPromotiomDetl.Rows[i].Cells["CompCode"].Value + "'" +
                                        ",DEPT_ID='" + gvPromotiomDetl.Rows[i].Cells["Dept"].Value + "'" +
                                        ",DESG_ID='" + gvPromotiomDetl.Rows[i].Cells["DesigID"].Value + "'" +
                                        ",DESIG='" + gvPromotiomDetl.Rows[i].Cells["Desig"].Value + "'" +
                                        ",HRIS_DESIG='" + gvPromotiomDetl.Rows[i].Cells["Desig"].Value + "'" +
                                        ",HRIS_DESIG_ID='" + gvPromotiomDetl.Rows[i].Cells["DesigID"].Value + "'" +
                                        ",elevel_id='" + gvPromotiomDetl.Rows[i].Cells["ElevelId"].Value + "'" +
                                        "WHERE ECODE = " + gvPromotiomDetl.Rows[i].Cells["Ecode"].Value;
                        }
                    }
                    if (sqlText.Length > 10)
                        iRes = objDB.ExecuteSaveData(sqlText);
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
                    MessageBox.Show("Approved Successfully", "Promotion Board", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRefNo.Text = meWef.Text = txtDept.Text = txtDesig.Text = txtComp.Text = txtBranch.Text = txtRemarks.Text = "";
                    txtReportToName.Text = txtReportingBranch.Text = txtSalGross.Text = txtSalBasic.Text = txtSalHRA.Text = txtSalCCA.Text = "";
                    txtSalConvAllw.Text = txtSalLTAAllw.Text = txtSalSpecialAllw.Text = txtSalUniformAllw.Text = txtVehicleAllw.Text = "";
                    txtSalChildAllw.Text = txtSalBNPAllw.Text = txtSalMedAllw.Text = txtSalPetrAllw.Text = txtPromType.Text = "";
                    FillPromotionsToGrid();
                }
                else
                {
                    MessageBox.Show("Data not Saved", "Promotion Board", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool CheckData()
        {
            bool bFalg = true;
            if (gvPromotiomDetl.Rows.Count == 0)
            {
                MessageBox.Show("No Data to Save!","",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                return false;
            }
            if (gvPromotiomDetl.Rows.Count > 0)
            {
                bFalg = false;
                for (int i = 0; i < gvPromotiomDetl.Rows.Count; i++)
                {
                    if ((bool)gvPromotiomDetl.Rows[i].Cells["Select"].EditedFormattedValue == true)
                    {
                        bFalg = true;
                        return bFalg;
                    }
                }
                 
            }
            return bFalg;
        }
    }
}
