using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using System.Diagnostics;
using System.Configuration;
using SSCRM.App_Code;
using SSCRMDB;
using SSAdmin;
using SSTrans;

namespace SSCRM
{
    public partial class VehicleAudit : Form
    {
        private SQLDB objDB = null;
        private Security objData = null;
        private UtilityDB objUtil = null;
        private DataGridViewRow dgvrow = null;
        private ReportViewer childReportViewer = null;
        private string strForm = "";
        public VehicleAudit()
        {
            InitializeComponent();
        }

        public VehicleAudit(string sFormType)
        {
            InitializeComponent();
            strForm = sFormType;
        }

        private void Petrol_Allowance_Approval_Load(object sender, EventArgs e)
        {
            gvEmpVehDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);
            FillBranchData();
        }

        private void FillBranchData()
        {
            objUtil = new UtilityDB();
            DataTable dtUB = new DataTable();
            try
            {
                dtUB = objUtil.dtUserBranch(CommonData.LogUserId);
                //if (dtUB.Rows.Count == 0)
                //dtUB.Rows.Add(CommonData.BranchCode + '@' + CommonData.CompanyCode + '@' + CommonData.BranchName + '@' + CommonData.CompanyName, CommonData.BranchName);
                cbUserBranch.DisplayMember = "branch_name";
                cbUserBranch.ValueMember = "branch_Code";
                cbUserBranch.DataSource = dtUB;
                cbUserBranch.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objUtil = null;
                dtUB = null;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void cbUserBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbUserBranch.Items.Count > 0)
            {
                FillEmpVehicleinfoToGrid();
            }
        }

        private void FillEmpVehicleinfoToGrid()
        {
            objDB = new SQLDB();
            string sqlText = "";
            DataTable dtEmpData = new DataTable();
            gvEmpVehDetails.Rows.Clear();
            if (cbUserBranch.SelectedIndex > -1)
            {
                if (CommonData.LogUserId.ToUpper() != "ADMIN")
                {
                    sqlText = "SELECT HVLH_EORA_CODE,CAST(HVLH_EORA_CODE AS VARCHAR)+'--'+MEMBER_NAME+' ('+DESIG+')' NAME,HVLH_VEHICLE_CLASS,VM_VEHICLE_MAKE" +
                            ",HVLH_VEHICLE_MAKERS_CLASS,HVLH_VEHICLE_REG_NUMBER,HVLH_LOAN_AMT,HVLH_EMPLOYEE_DEPOSIT,HVLH_LOAN_RECOVERY_CUTOFFDATE+SUM(HVLR_RECD_AMT) RECOVERED" +
                            ",ISNULL(HVLH_DRIVING_LICENCE,HVLH_LEARNING_LICENCE) LICENCE,ISNULL(HVLH_DRIVING_VALID_DATE,HVLH_LEARNING_VALID_DATE) LICENCE_VALID_DATE" +
                            ",HVLH_VEHICLE_REG_DATE,HVLH_PETROL_INCENTIVE_FROM_DATE FROM HR_VEHICLE_LOAN_HEAD INNER JOIN HR_VEHICLE_LOAN_RECOVERY ON HVLR_EORA_CODE = HVLH_EORA_CODE " +
                            "AND HVLR_VEHICLE_REG_NUMBER = HVLH_VEHICLE_REG_NUMBER INNER JOIN VEHICLE_MASTER ON VM_VEHICLE_MODEL = HVLH_VEHICLE_MAKERS_CLASS " +
                            "INNER JOIN EORA_MASTER ON ECODE = HVLH_EORA_CODE WHERE HVLH_BRANCH_CODE = '" + cbUserBranch.SelectedValue.ToString() + "' AND HVLH_AUDIT_FLAG != 'A' " +
                            "GROUP BY HVLH_EORA_CODE,HVLH_VEHICLE_CLASS,VM_VEHICLE_MAKE,HVLH_VEHICLE_MAKERS_CLASS" +
                            ",HVLH_VEHICLE_REG_NUMBER,HVLH_LOAN_AMT,HVLH_EMPLOYEE_DEPOSIT,HVLH_LOAN_RECOVERY_CUTOFFDATE,HVLH_DRIVING_LICENCE,HVLH_LEARNING_LICENCE" +
                            ",HVLH_VEHICLE_REG_DATE,HVLH_DRIVING_VALID_DATE,HVLH_LEARNING_VALID_DATE,MEMBER_NAME,DESIG,HVLH_PETROL_INCENTIVE_FROM_DATE";
                }
                else
                {
                    sqlText = "SELECT HVLH_EORA_CODE,CAST(HVLH_EORA_CODE AS VARCHAR)+'--'+MEMBER_NAME+' ('+DESIG+')' NAME,HVLH_VEHICLE_CLASS,VM_VEHICLE_MAKE"+
                            ",HVLH_VEHICLE_MAKERS_CLASS,HVLH_VEHICLE_REG_NUMBER,HVLH_LOAN_AMT,HVLH_EMPLOYEE_DEPOSIT"+
                            ",ISNULL(HVLH_DRIVING_LICENCE,HVLH_LEARNING_LICENCE) LICENCE,ISNULL(HVLH_DRIVING_VALID_FROM,HVLH_LEARNING_VALID_FROM) LICENCE_VALID_FROM" +
                            ",HVLH_VEHICLE_REG_DATE,HVLH_PETROL_INCN_REQUEST_DATE,HVLH_PETROL_INCN_APPR_DATE FROM HR_VEHICLE_LOAN_HEAD INNER JOIN VEHICLE_MASTER ON VM_VEHICLE_MODEL = HVLH_VEHICLE_MAKERS_CLASS " +
                            "INNER JOIN EORA_MASTER ON ECODE = HVLH_EORA_CODE WHERE HVLH_AUDIT_FLAG = 'P';";
                }
                dtEmpData = objDB.ExecuteDataSet(sqlText).Tables[0];
                if (dtEmpData.Rows.Count > 0)
                {
                    for (int i = 0; i < dtEmpData.Rows.Count; i++)
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = i + 1;
                        tempRow.Cells.Add(cellSLNO);

                        DataGridViewCell cellEcode = new DataGridViewTextBoxCell();
                        cellEcode.Value = dtEmpData.Rows[i]["HVLH_EORA_CODE"];
                        tempRow.Cells.Add(cellEcode);

                        DataGridViewCell cellName = new DataGridViewTextBoxCell();
                        cellName.Value = dtEmpData.Rows[i]["NAME"];
                        tempRow.Cells.Add(cellName);

                        //DataGridViewCell cellDesig = new DataGridViewTextBoxCell();
                        //cellDesig.Value = dtEmpData.Rows[i]["HVLR_DOC_MONTH"];
                        //tempRow.Cells.Add(cellDesig);

                        DataGridViewCell cellModel = new DataGridViewTextBoxCell();
                        cellModel.Value = dtEmpData.Rows[i]["HVLH_VEHICLE_MAKERS_CLASS"];
                        tempRow.Cells.Add(cellModel);

                        DataGridViewCell cellVehNo = new DataGridViewTextBoxCell();
                        cellVehNo.Value = dtEmpData.Rows[i]["HVLH_VEHICLE_REG_NUMBER"];                        
                        tempRow.Cells.Add(cellVehNo);

                        DataGridViewCell cellWEF = new DataGridViewTextBoxCell();
                        cellWEF.Value = Convert.ToDateTime(dtEmpData.Rows[i]["HVLH_PETROL_INCN_REQUEST_DATE"]).ToString("dd/MMM/yyyy").ToUpper();
                        tempRow.Cells.Add(cellWEF);

                        gvEmpVehDetails.Rows.Add(tempRow);

                    }
                }
            }
        }

        private void gvEmpVehDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != 6)
                {
                    dgvrow = gvEmpVehDetails.Rows[e.RowIndex];
                    string ecode = "";
                    ecode = gvEmpVehDetails.Rows[gvEmpVehDetails.CurrentCell.RowIndex].Cells["ECODE"].Value.ToString();
                    GetEmpVehicleDetails(ecode);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void GetEmpVehicleDetails(string ecode)
        {
            objDB = new SQLDB();
            string sqlText = "";
            DataTable dtEmpVehData = new DataTable();
            if (cbUserBranch.SelectedIndex > -1)
            {
                sqlText = "SELECT HVLH_EORA_CODE,CAST(HVLH_EORA_CODE AS VARCHAR)+'--'+MEMBER_NAME+' ('+DESIG+')' NAME,DESIG,HVLH_VEHICLE_CLASS,VM_VEHICLE_MAKE,HVLH_VEHICLE_COST" +
                             ",HVLH_VEHICLE_MAKERS_CLASS,HVLH_VEHICLE_REG_NUMBER,HVLH_LOAN_AMT,HVLH_EMPLOYEE_DEPOSIT,HVLH_LOAN_RECOVERY_CUTOFFDATE+ISNULL(SUM(HVLR_RECD_AMT),0) RECOVERED" +
                             ",ISNULL(HVLH_DRIVING_LICENCE,HVLH_LEARNING_LICENCE) LICENCE,ISNULL(HVLH_DRIVING_VALID_FROM,HVLH_LEARNING_VALID_FROM) LICENCE_VALID_FROM,ISNULL(HVLH_DRIVING_VALID_TO,HVLH_LEARNING_VALID_TO) LICENCE_VALID_TO,HVLH_REGD_OWNER" +
                             ",HVLH_VEHICLE_REG_DATE,HVLH_PETROL_INCN_REQUEST_DATE,HVLH_PETROL_INCN_APPR_DATE FROM HR_VEHICLE_LOAN_HEAD LEFT JOIN HR_VEHICLE_LOAN_RECOVERY ON HVLR_EORA_CODE = HVLH_EORA_CODE" +
                             " AND HVLR_VEHICLE_REG_NUMBER = HVLH_VEHICLE_REG_NUMBER INNER JOIN VEHICLE_MASTER ON VM_VEHICLE_MODEL = HVLH_VEHICLE_MAKERS_CLASS" +
                             " INNER JOIN EORA_MASTER ON ECODE = HVLH_EORA_CODE WHERE HVLH_EORA_CODE = '" + ecode + "' GROUP BY HVLH_EORA_CODE,HVLH_VEHICLE_CLASS,VM_VEHICLE_MAKE,HVLH_VEHICLE_MAKERS_CLASS" +
                             ",HVLH_VEHICLE_REG_NUMBER,HVLH_LOAN_AMT,HVLH_EMPLOYEE_DEPOSIT,HVLH_LOAN_RECOVERY_CUTOFFDATE,HVLH_DRIVING_LICENCE,HVLH_LEARNING_LICENCE" +
                             ",HVLH_VEHICLE_REG_DATE,HVLH_DRIVING_VALID_FROM,HVLH_LEARNING_VALID_FROM,HVLH_DRIVING_VALID_TO,HVLH_LEARNING_VALID_TO,MEMBER_NAME,DESIG,HVLH_PETROL_INCN_REQUEST_DATE,HVLH_PETROL_INCN_APPR_DATE,HVLH_VEHICLE_COST,DESIG,HVLH_REGD_OWNER";
             
            }
            dtEmpVehData = objDB.ExecuteDataSet(sqlText).Tables[0];
            if (dtEmpVehData.Rows.Count > 0)
            {
                txtEmpName.Text = dtEmpVehData.Rows[0]["NAME"]+"";
                txtVehType.Text = dtEmpVehData.Rows[0]["HVLH_VEHICLE_CLASS"]+"";
                txtVehMake.Text = dtEmpVehData.Rows[0]["VM_VEHICLE_MAKE"]+"";
                txtModel.Text = dtEmpVehData.Rows[0]["HVLH_VEHICLE_MAKERS_CLASS"]+"";
                txtVehCost.Text = dtEmpVehData.Rows[0]["HVLH_VEHICLE_COST"]+"";
                txtDesig.Text = dtEmpVehData.Rows[0]["DESIG"]+"";
                txtRegNo.Text = dtEmpVehData.Rows[0]["HVLH_VEHICLE_REG_NUMBER"]+"";
                txtLoanSanctioned.Text = dtEmpVehData.Rows[0]["HVLH_LOAN_AMT"]+"";
                txtDeposit.Text = dtEmpVehData.Rows[0]["HVLH_EMPLOYEE_DEPOSIT"]+"";
                txtRecovered.Text = dtEmpVehData.Rows[0]["RECOVERED"]+"";
                txtRcName.Text = dtEmpVehData.Rows[0]["HVLH_REGD_OWNER"]+"";
                txtLicenceNo.Text = dtEmpVehData.Rows[0]["LICENCE"]+"";
                meDLValidTo.Text = Convert.ToDateTime(dtEmpVehData.Rows[0]["LICENCE_VALID_TO"]).ToString("dd/MM/yyyy");
                meRegDate.Text = Convert.ToDateTime(dtEmpVehData.Rows[0]["HVLH_VEHICLE_REG_DATE"]).ToString("dd/MM/yyyy");
                meDLValidFrom.Text = Convert.ToDateTime(dtEmpVehData.Rows[0]["LICENCE_VALID_FROM"]).ToString("dd/MM/yyyy");
                meAllowReqFrom.Text = Convert.ToDateTime(dtEmpVehData.Rows[0]["HVLH_PETROL_INCN_REQUEST_DATE"]).ToString("dd/MM/yyyy");
            }
            else
            {
                txtEmpName.Text = "";
                txtVehType.Text = "";
                txtVehMake.Text = "";
                txtModel.Text = "";
                txtVehCost.Text = "";
                txtDesig.Text = "";
                txtRegNo.Text = "";
                txtLoanSanctioned.Text = "";
                txtDeposit.Text = "";
                txtRecovered.Text = "";
                txtRcName.Text = "";
                txtLicenceNo.Text = "";
                meRegDate.Text = "";
                meDLValidTo.Text = "";
                meDLValidFrom.Text = "";
                meAllowReqFrom.Text = "";
            }
        }

        

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                
                //ListBox ChkedRow = new ListBox();
                string ecodes = "";
                string wef = "";
                for (int i = 0; i < gvEmpVehDetails.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(gvEmpVehDetails.Rows[i].Cells["Approve"].Value) == true)
                    {
                        if (ecodes != "")
                            ecodes += ",";
                        if(wef != "")
                            wef += ",";
                        ecodes += gvEmpVehDetails.Rows[i].Cells["ECODE"].Value.ToString();
                        wef += gvEmpVehDetails.Rows[i].Cells["WEF"].Value.ToString();
                    }
                }
                if (ecodes != "")
                {
                    int iRes = 0;
                    try
                    {
                        objDB = new SQLDB();
                        string[] sValue = ecodes.Split(',');
                        string[] sWef = wef.Split(',');
                        string sqlText = "";
                        //string CompCode = "";
                        //sqlText = "SELECT CM_ACTUAL_CODE FROM COMPANY_MAS WHERE CM_COMPANY_CODE = '" + CommonData.CompanyCode + "'";
                        //CompCode = Convert.ToString(objDB.ExecuteDataSet(sqlText).Tables[0].Rows[0][0]).ToString();
                        //string imaxRefno = CompCode + "/" + CommonData.FinancialYear.Substring(2, 2) + "-" + CommonData.FinancialYear.Substring(7, 2) + "/ALLW/";
                        //sqlText = "SELECT ISNULL(MAX(SUBSTRING(ISNULL(HVLH_PETROL_APPR_REF_NO,'" + imaxRefno + "'),18,22)),0)+1 FROM HR_VEHICLE_LOAN_HEAD WHERE HVLH_COMPANY_CODE = '" + CommonData.CompanyCode + "'";
                        //imaxRefno += Convert.ToInt32(objDB.ExecuteDataSet(sqlText).Tables[0].Rows[0][0]).ToString("0000");

                        sqlText = "";
                        for (int i = 0; i < sValue.Length; i++)
                        {
                            sqlText += "UPDATE HR_VEHICLE_LOAN_HEAD SET HVLH_PETROL_INCN_APPR_L3_ECODE='" + CommonData.LogUserEcode + "'" +
                                        ",HVLH_PETROL_INCN_APPR_L3_DATE='" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") + "'" +
                                        ",HVLH_AUDIT_FLAG='A' WHERE HVLH_EORA_CODE = '" + sValue[i] + "'; ";
                        }
                        iRes = objDB.ExecuteSaveData(sqlText);
                        if (iRes > 0)
                        {
                            MessageBox.Show("Data Saved Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //childReportViewer = new ReportViewer(imaxRefno);
                            //CommonData.ViewReport = "PETROL_ALLOWANCE_APPROVAL";
                            //childReportViewer.Show();

                            FillEmpVehicleinfoToGrid();
                        }
                        else
                            MessageBox.Show("Data Not Saved", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        }

        private bool CheckData()
        {
            bool bFlag = false;
            if (gvEmpVehDetails.Rows.Count == 0)
            {
                MessageBox.Show("There is No list to Approve", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return bFlag;
            }
            for (int i = 0; i < gvEmpVehDetails.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvEmpVehDetails.Rows[i].Cells["Approve"].Value) == true)
                {
                    bFlag = true;                    
                }
            }
            if (bFlag == false)
            {
                MessageBox.Show("Select Atleast One", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return bFlag;
            }
            else
            {
                bFlag = false;
            }
            for (int i = 0; i < gvEmpVehDetails.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvEmpVehDetails.Rows[i].Cells["Approve"].Value) == true && gvEmpVehDetails.Rows[i].Cells["WEF"].Value != null)
                {
                    bFlag = true;   
                }
            }
            if (bFlag == false)
            {
                MessageBox.Show("Enter Effect From Date", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return bFlag;
            }
            return bFlag;
        }
    }
}
