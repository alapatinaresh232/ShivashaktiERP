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
    public partial class VehicleIncentiveApproval : Form
    {
        private SQLDB objDB = null;
        private Security objData = null;
        private UtilityDB objUtil = null;
        private DataGridViewRow dgvrow = null;
        public VehicleIncentiveApproval()
        {
            InitializeComponent();
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
            if (cbUserBranch.SelectedIndex > -1)
            {
                gvEmpVehDetails.Rows.Clear();
                if (CommonData.LogUserId.ToUpper() != "ADMIN")
                {
                    string[] sBranch = cbUserBranch.SelectedValue.ToString().Split('@');
                    sqlText = "SELECT HVLH_EORA_CODE,CAST(HVLH_EORA_CODE AS VARCHAR)+'--'+MEMBER_NAME+' ('+DESIG+')' NAME,HVLH_VEHICLE_CLASS,VM_VEHICLE_MAKE," +
                                "HVLH_VEHICLE_MAKERS_CLASS,HVLH_VEHICLE_REG_NUMBER,HVLH_LOAN_AMT,HVLH_EMPLOYEE_DEPOSIT," +
                                "ISNULL(HVLH_DRIVING_LICENCE,HVLH_LEARNING_LICENCE) LICENCE,ISNULL(HVLH_DRIVING_VALID_FROM,HVLH_LEARNING_VALID_FROM) LICENCE_VALID_FROM," +
                                "HVLH_VEHICLE_REG_DATE,HVLH_PETROL_INCN_REQUEST_DATE,HVLH_PETROL_INCN_APPR_DATE,HVLH_INCN_REQUEST_AMT FROM HR_VEHICLE_LOAN_HEAD INNER JOIN VEHICLE_MASTER ON VM_VEHICLE_MODEL = HVLH_VEHICLE_MAKERS_CLASS" +
                                " INNER JOIN EORA_MASTER ON ECODE = HVLH_EORA_CODE WHERE HVLH_INCN_REQUEST_FLAG = 'Y' AND HVLH_BRANCH_CODE = '" + sBranch[0] + "'";
                }
                else
                {
                    sqlText = "SELECT HVLH_EORA_CODE,CAST(HVLH_EORA_CODE AS VARCHAR)+'--'+MEMBER_NAME+' ('+DESIG+')' NAME,HVLH_VEHICLE_CLASS,VM_VEHICLE_MAKE," +
                                "HVLH_VEHICLE_MAKERS_CLASS,HVLH_VEHICLE_REG_NUMBER,HVLH_LOAN_AMT,HVLH_EMPLOYEE_DEPOSIT," +
                                "ISNULL(HVLH_DRIVING_LICENCE,HVLH_LEARNING_LICENCE) LICENCE,ISNULL(HVLH_DRIVING_VALID_FROM,HVLH_LEARNING_VALID_FROM) LICENCE_VALID_FROM," +
                                "HVLH_VEHICLE_REG_DATE,HVLH_PETROL_INCN_REQUEST_DATE,HVLH_PETROL_INCN_APPR_DATE,HVLH_INCN_REQUEST_AMT FROM HR_VEHICLE_LOAN_HEAD INNER JOIN VEHICLE_MASTER ON VM_VEHICLE_MODEL = HVLH_VEHICLE_MAKERS_CLASS" +
                                " INNER JOIN EORA_MASTER ON ECODE = HVLH_EORA_CODE WHERE HVLH_INCN_REQUEST_FLAG = 'Y'";
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
                        if (dtEmpData.Rows[i]["HVLH_INCN_REQUEST_AMT"].ToString().Trim() != "")
                        {
                            DataGridViewCell cellWEF = new DataGridViewTextBoxCell();
                            cellWEF.Value = Convert.ToInt32(dtEmpData.Rows[i]["HVLH_INCN_REQUEST_AMT"]);
                            tempRow.Cells.Add(cellWEF);
                        }
                        gvEmpVehDetails.Rows.Add(tempRow);
                    }
                }
            }
        }

        private void gvEmpVehDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                try
                {

                    dgvrow = gvEmpVehDetails.Rows[e.RowIndex];
                    string ecode = "";
                    ecode = gvEmpVehDetails.Rows[gvEmpVehDetails.CurrentCell.RowIndex].Cells["ECODE"].Value.ToString();
                    GetEmpVehicleDetails(ecode);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
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
                             ",HVLH_VEHICLE_REG_DATE,HVLH_PETROL_INCN_REQUEST_DATE,HVLH_PETROL_INCN_APPR_DATE,HVLH_INCN_REQUEST_AMT,HVLH_INCN_APPROVED_AMT FROM HR_VEHICLE_LOAN_HEAD LEFT JOIN HR_VEHICLE_LOAN_RECOVERY ON HVLR_EORA_CODE = HVLH_EORA_CODE" +
                             " AND HVLR_VEHICLE_REG_NUMBER = HVLH_VEHICLE_REG_NUMBER INNER JOIN VEHICLE_MASTER ON VM_VEHICLE_MODEL = HVLH_VEHICLE_MAKERS_CLASS" +
                             " INNER JOIN EORA_MASTER ON ECODE = HVLH_EORA_CODE WHERE HVLH_EORA_CODE = '" + ecode + "' GROUP BY HVLH_EORA_CODE,HVLH_VEHICLE_CLASS,VM_VEHICLE_MAKE,HVLH_VEHICLE_MAKERS_CLASS" +
                             ",HVLH_VEHICLE_REG_NUMBER,HVLH_LOAN_AMT,HVLH_EMPLOYEE_DEPOSIT,HVLH_LOAN_RECOVERY_CUTOFFDATE,HVLH_DRIVING_LICENCE,HVLH_LEARNING_LICENCE,HVLH_INCN_REQUEST_AMT,HVLH_INCN_APPROVED_AMT" +
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
                txtReqAmt.Text = dtEmpVehData.Rows[0]["HVLH_INCN_REQUEST_AMT"] + "";
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
                txtReqAmt.Text = "";
            }
        }

        //private void dtpWEF_ValueChanged(object sender, EventArgs e)
        //{
        //    if (dgvrow != null)
        //    {
        //        string strWEF = Convert.ToDateTime(dtpWEF.Value).ToString("dd/MMM/yyyy").ToUpper();
        //        gvEmpVehDetails.Rows[dgvrow.Index].Cells["WEF"].Value = strWEF;
        //    }
            
            
        //}

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
                        int imax = 0;
                        //string imaxRefno = CommonData.CompanyCode + "/" + CommonData.FinancialYear.Substring(2, 2) + "-" + CommonData.FinancialYear.Substring(7, 2) + "/INCN/";
                        sqlText = "SELECT ISNULL(MAX(HVLH_INCN_APPR_REF_NO),0)+1 FROM HR_VEHICLE_LOAN_HEAD";
                        imax = Convert.ToInt32(objDB.ExecuteDataSet(sqlText).Tables[0].Rows[0][0]);
                        sqlText = "";
                        for (int i = 0; i < sValue.Length; i++)
                        {

                            sqlText += "UPDATE HR_VEHICLE_LOAN_HEAD SET HVLH_INCN_APPR_REF_NO='" + imax + "'" +
                                        ",HVLH_INCN_APPROVED_AMT='" + sWef[i] + "',HVLH_INCN_APPROVED_BY='" + CommonData.LogUserEcode + "'" +
                                        ",HVLH_INCN_APPROVED_DATE='" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") + "'" +
                                        ",HVLH_INCN_REQUEST_FLAG='A' WHERE HVLH_EORA_CODE = '" + sValue[i] + "'; ";
                            imax++;
                        }
                        iRes = objDB.ExecuteSaveData(sqlText);
                        if (iRes > 0)
                        {
                            MessageBox.Show("Data Saved Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearAll();
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
                MessageBox.Show("Enter Approved Amount for Selected", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return bFlag;
            }
            return bFlag;
        }

        private void ClearAll()
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
            txtReqAmt.Text = "";
            gvEmpVehDetails.Rows.Clear();
        }
    }
}
