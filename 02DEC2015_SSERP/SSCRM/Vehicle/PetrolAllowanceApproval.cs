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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Security;
//using System.Collections;
using System.Net;
using System.Xml;
using System.IO;

namespace SSCRM
{
    public partial class PetrolAllowanceApproval : Form
    {
        private SQLDB objDB = null;
        private Security objData = null;
        private UtilityDB objUtil = null;
        private DataGridViewRow dgvrow = null;
        private ReportViewer childReportViewer = null;
        private string strForm = "";
        private DataTable dtUB = null;
        public PetrolAllowanceApproval()
        {
            InitializeComponent();
        }

        public PetrolAllowanceApproval(string sFormType)
        {
            InitializeComponent();
            strForm = sFormType;
        }

        private void Petrol_Allowance_Approval_Load(object sender, EventArgs e)
        {
            gvEmpVehDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,System.Drawing.FontStyle.Regular);
            //if (strForm == "FINAL")
            //{
                //txtBranchName.Visible = true;
                //cbUserBranch.Visible = false;
            //}
            //else
            //{
                //txtBranchName.Visible = false;
            cbUserBranch.Visible = false;
            //}
            FillBranchData();

        }

        private void FillBranchData()
        {
            objUtil = new UtilityDB();
            dtUB = new DataTable();
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
                //dtUB = null;
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
            if (dtUB.Rows.Count>0)
            {
                string strBranches = "";
                for (int i = 0; i < dtUB.Rows.Count; i++)
                {
                    string[] sBranch = dtUB.Rows[i]["branch_Code"].ToString().Split('@');
                    if (strBranches != "")
                        strBranches += ",";
                    strBranches += "'" + sBranch[0] + "'";
                }
                if (strForm == "FINAL")
                {
                    sqlText = "SELECT distinct HVLH_EORA_CODE,BRANCH_NAME,CAST(HVLH_EORA_CODE AS VARCHAR)+'--'+MEMBER_NAME+' ('+DESIG+')' NAME,HVLH_VEHICLE_CLASS,VM_VEHICLE_MAKE" +
                                ",HVLH_VEHICLE_MAKERS_CLASS,HVLH_VEHICLE_REG_NUMBER,HVLH_LOAN_AMT,HVLH_EMPLOYEE_DEPOSIT" +
                                ",ISNULL(HVLH_DRIVING_LICENCE,HVLH_LEARNING_LICENCE) LICENCE,ISNULL(HVLH_DRIVING_VALID_FROM,"+
                                "HVLH_LEARNING_VALID_FROM) LICENCE_VALID_FROM" +
                                ",HVLH_VEHICLE_REG_DATE,HVLH_PETROL_INCN_REQUEST_DATE,HVLH_PETROL_INCN_APPR_DATE,"+
                                "HVLH_PETROL_INCN_APPR_UPTO_DATE,isnull(HECD_EMP_MOBILE_NO,HVLH_MOBILE_NO) HECD_EMP_MOBILE_NO FROM HR_VEHICLE_LOAN_HEAD INNER JOIN VEHICLE_MASTER " +
                                "ON VM_VEHICLE_MODEL = HVLH_VEHICLE_MAKERS_CLASS " +
                                "INNER JOIN EORA_MASTER EM ON ECODE = HVLH_EORA_CODE "+
                                "INNER JOIN BRANCH_MAS BM ON BM.BRANCH_CODE = EM.BRANCH_CODE " +
                                "LEFT JOIN HR_APPL_A2E_MIGRATION ON HAAM_AGENT_CODE = HVLH_EORA_CODE "+
                                "LEFT JOIN HR_APPL_MASTER_HEAD ON HAMH_EORA_CODE = ISNULL(HAAM_EMP_CODE,HVLH_EORA_CODE) "+
                                "LEFT JOIN HR_EMP_CONTACT_DETL ON HECD_APPL_NO = HAMH_APPL_NUMBER WHERE HVLH_PETROL_REQUEST_FLAG = 'V' "+
                                "AND EM.BRANCH_CODE IN (" + strBranches + ");";

                }
                else
                {

                    //sqlText = "SELECT HVLH_EORA_CODE,CAST(HVLH_EORA_CODE AS VARCHAR)+'--'+MEMBER_NAME+' ('+DESIG+')' NAME,HVLH_VEHICLE_CLASS,VM_VEHICLE_MAKE" +
                    //        ",HVLH_VEHICLE_MAKERS_CLASS,HVLH_VEHICLE_REG_NUMBER,HVLH_LOAN_AMT,HVLH_EMPLOYEE_DEPOSIT,HVLH_LOAN_RECOVERY_CUTOFFDATE+SUM(HVLR_RECD_AMT) RECOVERED" +
                    //        ",ISNULL(HVLH_DRIVING_LICENCE,HVLH_LEARNING_LICENCE) LICENCE,ISNULL(HVLH_DRIVING_VALID_DATE,HVLH_LEARNING_VALID_DATE) LICENCE_VALID_DATE" +
                    //        ",HVLH_VEHICLE_REG_DATE,HVLH_PETROL_INCENTIVE_FROM_DATE FROM HR_VEHICLE_LOAN_HEAD INNER JOIN HR_VEHICLE_LOAN_RECOVERY ON HVLR_EORA_CODE = HVLH_EORA_CODE " +
                    //        "AND HVLR_VEHICLE_REG_NUMBER = HVLH_VEHICLE_REG_NUMBER INNER JOIN VEHICLE_MASTER ON VM_VEHICLE_MODEL = HVLH_VEHICLE_MAKERS_CLASS " +
                    //        "INNER JOIN EORA_MASTER ON ECODE = HVLH_EORA_CODE WHERE HVLH_BRANCH_CODE = '" + cbUserBranch.SelectedValue.ToString() + "' AND HVLH_PETROL_REQUEST_FLAG = 'Y' " +
                    //        "GROUP BY HVLH_EORA_CODE,HVLH_VEHICLE_CLASS,VM_VEHICLE_MAKE,HVLH_VEHICLE_MAKERS_CLASS" +
                    //        ",HVLH_VEHICLE_REG_NUMBER,HVLH_LOAN_AMT,HVLH_EMPLOYEE_DEPOSIT,HVLH_LOAN_RECOVERY_CUTOFFDATE,HVLH_DRIVING_LICENCE,HVLH_LEARNING_LICENCE" +

                    sqlText = "SELECT distinct HVLH_EORA_CODE,BRANCH_NAME,CAST(HVLH_EORA_CODE AS VARCHAR)+'--'+MEMBER_NAME+' ('+DESIG+')' NAME,HVLH_VEHICLE_CLASS,VM_VEHICLE_MAKE" +
                            ",HVLH_VEHICLE_MAKERS_CLASS,HVLH_VEHICLE_REG_NUMBER,HVLH_LOAN_AMT,HVLH_EMPLOYEE_DEPOSIT" +
                            ",ISNULL(HVLH_DRIVING_LICENCE,HVLH_LEARNING_LICENCE) LICENCE,ISNULL(HVLH_DRIVING_VALID_FROM,"+
                            "HVLH_LEARNING_VALID_FROM) LICENCE_VALID_FROM" +
                            ",HVLH_VEHICLE_REG_DATE,HVLH_PETROL_INCN_REQUEST_DATE,HVLH_PETROL_INCN_APPR_DATE,"+
                            "HVLH_PETROL_INCN_APPR_UPTO_DATE,isnull(HECD_EMP_MOBILE_NO,HVLH_MOBILE_NO) HECD_EMP_MOBILE_NO FROM HR_VEHICLE_LOAN_HEAD INNER JOIN VEHICLE_MASTER " +
                            "ON VM_VEHICLE_MODEL = HVLH_VEHICLE_MAKERS_CLASS " +
                            "INNER JOIN EORA_MASTER EM ON ECODE = HVLH_EORA_CODE " +
                            "INNER JOIN BRANCH_MAS BM ON BM.BRANCH_CODE = EM.BRANCH_CODE " +
                            "LEFT JOIN HR_APPL_A2E_MIGRATION ON HAAM_AGENT_CODE = HVLH_EORA_CODE "+
                            "LEFT JOIN HR_APPL_MASTER_HEAD ON HAMH_EORA_CODE = ISNULL(HAAM_EMP_CODE,HVLH_EORA_CODE) "+
                            "LEFT JOIN HR_EMP_CONTACT_DETL ON HECD_APPL_NO = HAMH_APPL_NUMBER WHERE HVLH_PETROL_REQUEST_FLAG = 'Y' "+
                            "AND EM.BRANCH_CODE IN (" + strBranches + ");";

                }
                if (sqlText.Length > 10)
                {
                    try
                    {
                        dtEmpData = objDB.ExecuteDataSet(sqlText).Tables[0];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                if (dtEmpData.Rows.Count > 0)
                {
                    for (int i = 0; i < dtEmpData.Rows.Count; i++)
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = i + 1;
                        tempRow.Cells.Add(cellSLNO);

                        DataGridViewCell cellBranch = new DataGridViewTextBoxCell();
                        cellBranch.Value = dtEmpData.Rows[i]["BRANCH_NAME"];
                        tempRow.Cells.Add(cellBranch);

                        DataGridViewCell cellEcode = new DataGridViewTextBoxCell();
                        cellEcode.Value = dtEmpData.Rows[i]["HVLH_EORA_CODE"];
                        tempRow.Cells.Add(cellEcode);

                        DataGridViewCell cellName = new DataGridViewTextBoxCell();
                        cellName.Value = dtEmpData.Rows[i]["NAME"];
                        tempRow.Cells.Add(cellName);

                        DataGridViewCell cellDesig = new DataGridViewTextBoxCell();
                        cellDesig.Value = dtEmpData.Rows[i]["HECD_EMP_MOBILE_NO"];
                        tempRow.Cells.Add(cellDesig);

                        DataGridViewCell cellModel = new DataGridViewTextBoxCell();
                        cellModel.Value = dtEmpData.Rows[i]["HVLH_VEHICLE_MAKERS_CLASS"];
                        tempRow.Cells.Add(cellModel);

                        DataGridViewCell cellVehNo = new DataGridViewTextBoxCell();
                        cellVehNo.Value = dtEmpData.Rows[i]["HVLH_VEHICLE_REG_NUMBER"];                        
                        tempRow.Cells.Add(cellVehNo);

                        DataGridViewCell cellWEF = new DataGridViewTextBoxCell();
                        if (dtEmpData.Rows[i]["HVLH_PETROL_INCN_REQUEST_DATE"].ToString() == "")
                            cellWEF.Value = "";
                        else
                            cellWEF.Value = Convert.ToDateTime(dtEmpData.Rows[i]["HVLH_PETROL_INCN_REQUEST_DATE"]).ToString("dd/MMM/yyyy").ToUpper();
                        tempRow.Cells.Add(cellWEF);
                        if (dtEmpData.Rows[i]["HVLH_PETROL_INCN_APPR_DATE"].ToString().Trim() != "")
                        {
                            DataGridViewCell cellWEFAppr = new DataGridViewTextBoxCell();
                            cellWEFAppr.Value = Convert.ToDateTime(dtEmpData.Rows[i]["HVLH_PETROL_INCN_APPR_DATE"]).ToString("dd/MMM/yyyy").ToUpper();
                            tempRow.Cells.Add(cellWEFAppr);
                        }
                        string strWETAppr = dtEmpData.Rows[i]["HVLH_PETROL_INCN_APPR_UPTO_DATE"].ToString().Trim();
                        if (dtEmpData.Rows[i]["HVLH_PETROL_INCN_APPR_UPTO_DATE"].ToString().Trim() != "")
                        {
                            DataGridViewCell cellWETAppr = new DataGridViewTextBoxCell();
                            cellWETAppr.Value = Convert.ToDateTime(dtEmpData.Rows[i]["HVLH_PETROL_INCN_APPR_UPTO_DATE"]).ToString("dd/MMM/yyyy").ToUpper();
                            tempRow.Cells.Add(cellWETAppr);
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
        }

        private void GetEmpVehicleDetails(string ecode)
        {
            objDB = new SQLDB();
            string sqlText = "";
            DataTable dtEmpVehData = new DataTable();
            if (cbUserBranch.SelectedIndex > -1)
            {
                sqlText = "SELECT BRANCH_NAME,HVLH_EORA_CODE,CAST(HVLH_EORA_CODE AS VARCHAR)+'--'+MEMBER_NAME+' ('+DESIG+')' NAME,DESIG,HVLH_VEHICLE_CLASS,VM_VEHICLE_MAKE,HVLH_VEHICLE_COST" +
                             ",HVLH_VEHICLE_MAKERS_CLASS,HVLH_VEHICLE_REG_NUMBER,HVLH_LOAN_AMT,HVLH_EMPLOYEE_DEPOSIT,HVLH_LOAN_RECOVERY_CUTOFFDATE+ISNULL(SUM(HVLR_RECD_AMT),0) RECOVERED" +
                             ",ISNULL(HVLH_DRIVING_LICENCE,HVLH_LEARNING_LICENCE) LICENCE,ISNULL(HVLH_DRIVING_VALID_FROM,HVLH_LEARNING_VALID_FROM) LICENCE_VALID_FROM,ISNULL(HVLH_DRIVING_VALID_TO,HVLH_LEARNING_VALID_TO) LICENCE_VALID_TO,HVLH_REGD_OWNER" +
                             ",HVLH_VEHICLE_REG_DATE,HVLH_PETROL_INCN_REQUEST_DATE,HVLH_PETROL_INCN_APPR_DATE,HVLH_PETROL_INCN_APPR_UPTO_DATE FROM HR_VEHICLE_LOAN_HEAD LEFT JOIN HR_VEHICLE_LOAN_RECOVERY ON HVLR_EORA_CODE = HVLH_EORA_CODE" +
                             " AND HVLR_VEHICLE_REG_NUMBER = HVLH_VEHICLE_REG_NUMBER INNER JOIN VEHICLE_MASTER ON VM_VEHICLE_MODEL = HVLH_VEHICLE_MAKERS_CLASS" +
                             " INNER JOIN BRANCH_MAS ON BRANCH_CODE = HVLH_BRANCH_CODE"+
                             " INNER JOIN EORA_MASTER ON ECODE = HVLH_EORA_CODE WHERE HVLH_EORA_CODE = '" + ecode + "' GROUP BY HVLH_EORA_CODE,HVLH_VEHICLE_CLASS,VM_VEHICLE_MAKE,HVLH_VEHICLE_MAKERS_CLASS" +
                             ",HVLH_VEHICLE_REG_NUMBER,HVLH_LOAN_AMT,HVLH_EMPLOYEE_DEPOSIT,HVLH_LOAN_RECOVERY_CUTOFFDATE,HVLH_DRIVING_LICENCE,HVLH_LEARNING_LICENCE,BRANCH_NAME,HVLH_PETROL_INCN_APPR_UPTO_DATE" +
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
                txtBranchName.Text = dtEmpVehData.Rows[0]["BRANCH_NAME"] + "";
                if (strForm == "FINAL")
                {
                    dtpWEF.Value = Convert.ToDateTime(dtEmpVehData.Rows[0]["HVLH_PETROL_INCN_APPR_DATE"]);
                    dtpWET.Value = Convert.ToDateTime(dtEmpVehData.Rows[0]["HVLH_PETROL_INCN_APPR_UPTO_DATE"]);
                }
                else
                {
                    dtpWEF.Value = Convert.ToDateTime(dtEmpVehData.Rows[0]["HVLH_PETROL_INCN_REQUEST_DATE"]);
                    dtpWET.Value = Convert.ToDateTime(dtEmpVehData.Rows[0]["LICENCE_VALID_TO"]);
                }
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

        private void dtpWEF_ValueChanged(object sender, EventArgs e)
        {
            if (dgvrow != null)
            {
                string strWEF = Convert.ToDateTime(dtpWEF.Value).ToString("dd/MMM/yyyy").ToUpper();
                gvEmpVehDetails.Rows[dgvrow.Index].Cells["WEF"].Value = strWEF;
            }
            
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                
                //ListBox ChkedRow = new ListBox();
                string ecodes = "";
                string wef = "";
                string wet = "";
                string Mobile = "";
                string Name = "";
                string[] sBranch = cbUserBranch.SelectedValue.ToString().Split('@');
                for (int i = 0; i < gvEmpVehDetails.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(gvEmpVehDetails.Rows[i].Cells["Approve"].Value) == true)
                    {
                        if (ecodes != "")
                            ecodes += ",";
                        if(wef != "")
                            wef += ",";
                        if (wet != "")
                            wet += ",";
                        if(Mobile!="")
                            Mobile+=",";
                        if (Name != "")
                            Name += ",";
                        ecodes += gvEmpVehDetails.Rows[i].Cells["ECODE"].Value.ToString();
                        wef += Convert.ToDateTime(gvEmpVehDetails.Rows[i].Cells["WEF"].Value.ToString()).ToString("dd-MMM-yyyy");
                        wet += Convert.ToDateTime(gvEmpVehDetails.Rows[i].Cells["WET"].Value.ToString()).ToString("dd-MMM-yyyy");
                        Mobile += gvEmpVehDetails.Rows[i].Cells["Mobile"].Value.ToString();
                        Name += gvEmpVehDetails.Rows[i].Cells["EmpName"].Value.ToString();
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
                        string[] sWet = wet.Split(',');
                        string[] sMobile = Mobile.Split(',');
                        string[] sName = Name.Split(',');
                        string sqlText = "";
                        string CompCode = "";
                        string[] strBranch = cbUserBranch.SelectedValue.ToString().Split('@');
                        if (strForm == "FINAL")
                        {
                            sqlText = "SELECT CM_ACTUAL_CODE FROM COMPANY_MAS WHERE CM_COMPANY_CODE = '" + strBranch[1] + "'";
                            CompCode = Convert.ToString(objDB.ExecuteDataSet(sqlText).Tables[0].Rows[0][0]).ToString();
                            
                            int imax = 0;
                            sqlText = "SELECT ISNULL(MAX(HVLH_PETROL_APPR_REF_NO),0)+1 FROM HR_VEHICLE_LOAN_HEAD";
                            imax = Convert.ToInt32(objDB.ExecuteDataSet(sqlText).Tables[0].Rows[0][0]);
                            sqlText = "";
                            
                            for (int i = 0; i < sValue.Length; i++)
                            {

                                sqlText += "UPDATE HR_VEHICLE_LOAN_HEAD SET HVLH_PETROL_APPR_REF_NO='" + imax + "'" +
                                            ",HVLH_PETROL_INCN_APPR_DATE='" + sWef[i] + "',HVLH_PETROL_INCN_APPR_UPTO_DATE='" + sWet[i] +
                                            "',HVLH_PETROL_INCN_APPR_L1_ECODE='" + CommonData.LogUserEcode + "'" +
                                            ",HVLH_MOBILE_NO='" + sMobile[i] + "'" +
                                            ",HVLH_PETROL_INCN_APPR_L1_DATE=GETDATE()" +
                                            ",HVLH_PETROL_REQUEST_FLAG='A' WHERE HVLH_EORA_CODE = '" + sValue[i] + "'; ";
                                sqlText += " INSERT INTO HR_VEHICLE_APPROVAL_HISTORY(" +
                                            "HVAH_EORA_CODE," +
                                            "HVAH_PETROL_INCN_APPR_DATE," +
                                            "HVAH_PETROL_INCN_APPR_UPTO_DATE," +
                                            "HVAH_PETROL_APPR_REF_NO," +
                                            "HVAH_CREATED_BY," +
                                            "HVAH_CREATED_DATE)" +
                                            " VALUES" +
                                            "(" + sValue[i] + "" +
                                            ",'" + sWef[i] + "'" +
                                            ",'" + sWet[i] + "'" +
                                            ",'" + imax + "'" +
                                            ",'" + CommonData.LogUserId + "'" +
                                            ",GETDATE()); ";

                                imax++;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < sValue.Length; i++)
                            {
                                sqlText += "UPDATE HR_VEHICLE_LOAN_HEAD SET HVLH_PETROL_INCN_APPR_DATE='" + sWef[i] + "',HVLH_PETROL_INCN_APPR_UPTO_DATE='" + sWet[i] + "',HVLH_PETROL_INCN_APPR_L2_ECODE='" + CommonData.LogUserEcode + "'" +
                                            ",HVLH_PETROL_INCN_APPR_L2_DATE='" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") + "',HVLH_MOBILE_NO='" + sMobile[i] + "'" +
                                            ",HVLH_PETROL_REQUEST_FLAG='V' WHERE HVLH_EORA_CODE = '" + sValue[i] + "'; ";
                            }
                        }
                        iRes = objDB.ExecuteSaveData(sqlText);
                        if (iRes > 0)
                        {
                            if (strForm == "FINAL")
                            {
                                for (int i = 0; i < sValue.Length; i++)
                                {
                                    //string[] sValue = ecodes.Split(',');
                                    //string[] sWef = wef.Split(',');
                                    //string[] sWet = wet.Split(',');
                                    //string[] sMobile = Mobile.Split(',');
                                    //string[] sName = Name.Split(',');
                                    try
                                    {
                                        Convert.ToInt64(sMobile[i]);
                                    }
                                    catch
                                    {
                                        sMobile[i] = "0";
                                    }
                                    if (Convert.ToInt64(sMobile[i]) != 0)
                                    {
                                        string smsMessage = "";
                                        string smsResult = "";
                                        smsMessage = "Dear " + sName[i] + ",your Petrol amount has been approved and is valid from " +
                                                        "" + sWef[i] + " to " + sWet[i];
                                        try
                                        {
                                            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://dnd.sendsmsindia.org/sendsms.jsp?" +
                                            //                            "user=SBTLAP&password=admin@66&mobiles=" + sMobile[i] +
                                            //                            "&sms=" + smsMessage + "&senderid=SSGCHO&version=3");

                                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.smsjust.com/blank/sms/user/urlsms.php?username=shivashakti&pass=1234567&senderid=SSGCHO&dest_mobileno="
                                                + sMobile[i] + "&msgtype=UNI&message=" + smsMessage + "&response=Y");

                                            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://shiva.bulksmshyderabad.co.in/api/smsapi.aspx?" +
                                            //                            "username=shivashakti&password=shiva123&to=" + sMobile[i] +
                                            //                            "&from=SSGCHO&message=" + smsMessage + "");

                                            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://dnd.sendsmsindia.org/getundeliveredreasonanddescription.jsp?userid=SBTLAP&password=admin@66");



                                            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                                            StreamReader reader = new StreamReader(response.GetResponseStream());
                                            smsResult = reader.ReadToEnd();
                                            //DataTable dataTable = new DataTable();
                                            //dataSet.ReadXml(response.GetResponseStream());
                                            //dataTable.ReadXml(reader);
                                            //result = reader.ReadToEnd();
                                        }
                                        catch
                                        {

                                        }
                                        string sqlSmsText = " INSERT INTO SMS_HISTORY(" +
                                                                "sms_sender_id," +
                                                                "sms_mobile_no," +
                                                                "sms_name," +
                                                                "sms_type," +
                                                                "sms_message," +
                                                                "sms_sent_by," +
                                                                "sms_api_result," +
                                                                "sms_sent_date)" +
                                                                " VALUES('SSGCHO" +
                                                                "','" + sMobile[i] +
                                                                "','" + sName[i] +
                                                                "','" + "PETROL APPROVAL" +
                                                                "','" + smsMessage +
                                                                "','ERP" +
                                                                "','" + smsResult +
                                                                "',getdate());";
                                        try
                                        {
                                            objDB.ExecuteSaveData(sqlSmsText);
                                        }
                                        catch
                                        {

                                        }
                                    }


                                }
                            }
                            MessageBox.Show("Data Saved Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearAll();
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
                    if (gvEmpVehDetails.Rows[i].Cells["WEF"].Value == null)
                    {
                        MessageBox.Show("Enter With effect from", "SSCRm", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        bFlag = false;
                        return bFlag;
                    }
                    if (gvEmpVehDetails.Rows[i].Cells["WET"].Value == null)
                    {
                        MessageBox.Show("Enter With effect To", "SSCRm", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        bFlag = false;
                        return bFlag;
                    }
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
            //for (int i = 0; i < gvEmpVehDetails.Rows.Count; i++)
            //{
            //    if (Convert.ToBoolean(gvEmpVehDetails.Rows[i].Cells["Approve"].Value) == true && gvEmpVehDetails.Rows[i].Cells["WEF"].Value != null)
            //    {
            //        bFlag = true;
            //    }
            //}
            //if (bFlag == false)
            //{
            //    MessageBox.Show("Enter Effect Upto Date", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return bFlag;
            //}
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
        }

        private void dtpWET_ValueChanged(object sender, EventArgs e)
        {
            if (dgvrow != null)
            {
                string strWET = Convert.ToDateTime(dtpWET.Value).ToString("dd/MMM/yyyy").ToUpper();
                gvEmpVehDetails.Rows[dgvrow.Index].Cells["WET"].Value = strWET;
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            bool bFlag = false;
            if (gvEmpVehDetails.Rows.Count == 0)
            {
                MessageBox.Show("There is No list to Approve", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
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
                return;
            }
            string ecodes = "";            
            string Mobile = "";
            string Name = "";
            string[] sBranch = cbUserBranch.SelectedValue.ToString().Split('@');
            for (int i = 0; i < gvEmpVehDetails.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvEmpVehDetails.Rows[i].Cells["Approve"].Value) == true)
                {
                    if (ecodes != "")
                        ecodes += ",";                    
                    if (Mobile != "")
                        Mobile += ",";
                    if (Name != "")
                        Name += ",";
                    ecodes += gvEmpVehDetails.Rows[i].Cells["ECODE"].Value.ToString();
                    Mobile += gvEmpVehDetails.Rows[i].Cells["Mobile"].Value.ToString();
                    Name += gvEmpVehDetails.Rows[i].Cells["EmpName"].Value.ToString();
                }
            }
            if (ecodes != "")
            {
                int iRes = 0;
                try
                {
                    objDB = new SQLDB();
                    string[] sValue = ecodes.Split(',');                    
                    string[] sMobile = Mobile.Split(',');
                    string[] sName = Name.Split(',');
                    string sqlText = "";
                    //string CompCode = "";
                    string[] strBranch = cbUserBranch.SelectedValue.ToString().Split('@');
                    
                        //sqlText = "SELECT CM_ACTUAL_CODE FROM COMPANY_MAS WHERE CM_COMPANY_CODE = '" + strBranch[1] + "'";
                        //CompCode = Convert.ToString(objDB.ExecuteDataSet(sqlText).Tables[0].Rows[0][0]).ToString();

                        //int imax = 0;
                        //sqlText = "SELECT ISNULL(MAX(HVLH_PETROL_APPR_REF_NO),0)+1 FROM HR_VEHICLE_LOAN_HEAD";
                        //imax = Convert.ToInt32(objDB.ExecuteDataSet(sqlText).Tables[0].Rows[0][0]);
                    sqlText = "";

                    for (int i = 0; i < sValue.Length; i++)
                    {

                        sqlText += " UPDATE HR_VEHICLE_LOAN_HEAD SET HVLH_PETROL_REQUEST_FLAG = 'R' WHERE HVLH_EORA_CODE = " + sValue[i];
                        //imax++;
                    }
                    
                    iRes = objDB.ExecuteSaveData(sqlText);
                    if (iRes > 0)
                    {
                        
                        MessageBox.Show("Data Saved Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearAll();
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
                    MessageBox.Show("Data Not Saved", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    objDB = null;
                }
            }
        }
    }
}
