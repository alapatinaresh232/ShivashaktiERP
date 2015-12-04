using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSAdmin;
using SSCRMDB;
using SSCRM.App_Code;
using SSTrans;
using System.Net.Mail;
using System.Net;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;

namespace SSCRM
{
    public partial class frmApprovedStatus : Form
    {
        public SQLDB objSQLDB;
        public HRInfo objHrInfo;
        public Security objSecurity;
        public frmApprovedStatus()
        {
            InitializeComponent();
        }

        private void frmApprovedStatus_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            cmbStatus.SelectedIndex = 0;
            cmbType.SelectedIndex = 0;
            objSecurity = new Security();
            DataTable dtCpy = objSecurity.GetCompanyDataSet().Tables[0];
            UtilityLibrary.PopulateControl(cmbCompany, dtCpy.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
            objSecurity = null;
        }

        public void GetPendingData()
        {
            objSQLDB = new SQLDB();
            DataTable dtPending = new DataTable();
            //if (cmbType.SelectedIndex == 1)
            //    dtPending = objSQLDB.ExecuteDataSet("SELECT HAMH_COMPANY_CODE,HAMH_BRANCH_CODE,HAMH_APPL_NUMBER,HAMH_EORA_CODE,HAED_SSC_NUMBER,HAMH_NAME,HAMH_FORH_NAME,HAMH_DOB,HAMH_DOJ,isnull(HAMH_APPL_PENDING_REASON,'') as HAMH_REASON FROM HR_APPL_MASTER_HEAD MD INNER JOIN HR_APPL_CHECK AC ON MD.HAMH_APPL_NUMBER=AC.HAED_APPL_NUMBER WHERE HAMH_EORA_TYPE='E' AND ISNULL(HAMH_LEFT_APPROVAL_FLAG,'NO')='NO' AND HAMH_LEFT_DATE IS NOT NULL AND HAMH_BRANCH_CODE='" + cmbBranch_optional.SelectedValue + "'", CommandType.Text).Tables[0];
            //if (cmbType.SelectedIndex == 0)
            //{
            //    if (cmbStatus.SelectedIndex == 0)
            //        dtPending = objSQLDB.ExecuteDataSet("SELECT HAMH_COMPANY_CODE,HAMH_BRANCH_CODE,HAMH_APPL_NUMBER,HAMH_EORA_CODE,HAED_SSC_NUMBER,HAMH_NAME,HAMH_FORH_NAME,HAMH_DOB,HAMH_DOJ,isnull(HAMH_APPL_PENDING_REASON,'') as HAMH_REASON FROM HR_APPL_MASTER_HEAD MD INNER JOIN HR_APPL_CHECK AC ON MD.HAMH_APPL_NUMBER=AC.HAED_APPL_NUMBER WHERE ISNULL(HAMH_APPROVAL_NO,0)=0 AND HAMH_EORA_TYPE='A' AND HAMH_LEFT_DATE IS NULL AND HAMH_BRANCH_CODE='" + cmbBranch_optional.SelectedValue + "'", CommandType.Text).Tables[0];
            //    else if (cmbStatus.SelectedIndex == 1)
            //        dtPending = objSQLDB.ExecuteDataSet("SELECT HAMH_COMPANY_CODE,HAMH_BRANCH_CODE,HAMH_APPL_NUMBER,HAMH_EORA_CODE,HAED_SSC_NUMBER,HAMH_NAME,HAMH_FORH_NAME,HAMH_DOB,HAMH_DOJ,isnull(HAMH_APPL_PENDING_REASON,'') as HAMH_REASON FROM HR_APPL_MASTER_HEAD MD INNER JOIN HR_APPL_CHECK AC ON MD.HAMH_APPL_NUMBER=AC.HAED_APPL_NUMBER WHERE ISNULL(HAMH_APPROVAL_NO,0)=-1 AND HAMH_EORA_TYPE='A' AND HAMH_LEFT_DATE IS NULL AND HAMH_BRANCH_CODE='" + cmbBranch_optional.SelectedValue + "'", CommandType.Text).Tables[0];
            //    else
            //        dtPending = objSQLDB.ExecuteDataSet("SELECT HAMH_COMPANY_CODE,HAMH_BRANCH_CODE,HAMH_APPL_NUMBER,HAMH_EORA_CODE,HAED_SSC_NUMBER,HAMH_NAME,HAMH_FORH_NAME,HAMH_DOB,HAMH_DOJ,isnull(HAMH_APPL_PENDING_REASON,'') as HAMH_REASON FROM HR_APPL_MASTER_HEAD MD INNER JOIN HR_APPL_CHECK AC ON MD.HAMH_APPL_NUMBER=AC.HAED_APPL_NUMBER WHERE HAMH_EORA_TYPE='A' AND ISNULL(HAMH_LEFT_APPROVAL_FLAG,'NO')='NO' AND HAMH_LEFT_DATE IS NOT NULL AND HAMH_BRANCH_CODE='" + cmbBranch_optional.SelectedValue + "'", CommandType.Text).Tables[0];
            //}
            if (cmbType.SelectedIndex == 1)//E
            {
                dtPending = objSQLDB.ExecuteDataSet("SELECT HAMH_COMPANY_CODE,HAMH_BRANCH_CODE,HAMH_APPL_NUMBER,HAMH_EORA_CODE"+
                    ",HAED_SSC_NUMBER,HAMH_NAME,HAMH_FORH_NAME,HAMH_DOB,HAMH_DOJ,isnull(HAMH_APPL_PENDING_REASON,'') as "+
                    "HAMH_REASON FROM HR_APPL_MASTER_HEAD MD INNER JOIN HR_APPL_CHECK AC ON MD.HAMH_APPL_NUMBER=AC.HAED_APPL_NUMBER "+
                    "INNER JOIN EORA_MASTER ON ECODE = HAMH_EORA_CODE WHERE HAMH_EORA_TYPE='E' AND ISNULL(HAMH_LEFT_APPROVAL_FLAG,'NO')='NO' AND HAMH_LEFT_DATE IS NOT NULL AND " +
                    "BRANCH_CODE='" + cmbBranch_optional.SelectedValue + "'", CommandType.Text).Tables[0];
            }
            else
            
            {
                if (cmbStatus.SelectedIndex == 0)
                    dtPending = objSQLDB.ExecuteDataSet("SELECT HAMH_COMPANY_CODE, HAMH_BRANCH_CODE, HAMH_APPL_NUMBER, HAMH_EORA_CODE"+
			                                            ", HAED_SSC_NUMBER, HAMH_NAME, HAMH_FORH_NAME, HAMH_DOB, HAMH_DOJ"+
			                                            ", isnull(HAMH_APPL_PENDING_REASON,'') as HAMH_REASON FROM HR_APPL_MASTER_HEAD MD "+
			                                            "INNER JOIN HR_APPL_CHECK AC ON MD.HAMH_APPL_NUMBER=AC.HAED_APPL_NUMBER "+
			                                            "INNER JOIN EORA_MASTER ON ECODE = HAMH_EORA_CODE "+
                                                        "WHERE HAMH_EORA_TYPE='A' AND HAMH_WORKING_STATUS ='P' AND BRANCH_CODE = '" + cmbBranch_optional.SelectedValue + "'" +
			                                            "AND NOT EXISTS (SELECT HARH_APPL_NUMBER FROM HR_APPL_REJOIN_HISTORY WHERE HARH_APPL_NUMBER = HAMH_APPL_NUMBER)", CommandType.Text).Tables[0];
                else if (cmbStatus.SelectedIndex == 1)
                    dtPending = objSQLDB.ExecuteDataSet("SELECT HAMH_COMPANY_CODE, HAMH_BRANCH_CODE, HAMH_APPL_NUMBER, HAMH_EORA_CODE" +
                                                        ", HAED_SSC_NUMBER, HAMH_NAME, HAMH_FORH_NAME, HAMH_DOB, HAMH_DOJ" +
                                                        ", isnull(HAMH_APPL_PENDING_REASON,'') as HAMH_REASON FROM HR_APPL_MASTER_HEAD MD " +
                                                        "LEFT JOIN HR_APPL_CHECK AC ON MD.HAMH_APPL_NUMBER=AC.HAED_APPL_NUMBER " +
                                                        "INNER JOIN EORA_MASTER ON ECODE = HAMH_EORA_CODE " +
                                                        "WHERE HAMH_EORA_TYPE='A' AND HAMH_WORKING_STATUS ='P' AND BRANCH_CODE = '" + cmbBranch_optional.SelectedValue + "'" +
                                                        "AND EXISTS (SELECT HARH_APPL_NUMBER FROM HR_APPL_REJOIN_HISTORY WHERE HARH_APPL_NUMBER = HAMH_APPL_NUMBER)", CommandType.Text).Tables[0];
                else if (cmbStatus.SelectedIndex == 2)
                    dtPending = objSQLDB.ExecuteDataSet("SELECT HAMH_COMPANY_CODE,HAMH_BRANCH_CODE,HAMH_APPL_NUMBER,HAMH_EORA_CODE" +
                        ",HAED_SSC_NUMBER,HAMH_NAME,HAMH_FORH_NAME,HAMH_DOB,HAMH_DOJ,isnull(HAMH_APPL_PENDING_REASON,'') as HAMH_REASON " +
                        "FROM HR_APPL_MASTER_HEAD MD INNER JOIN HR_APPL_CHECK AC ON MD.HAMH_APPL_NUMBER=AC.HAED_APPL_NUMBER INNER JOIN EORA_MASTER ON ECODE = HAMH_EORA_CODE WHERE HAMH_EORA_TYPE='A' " +
                        "AND HAMH_WORKING_STATUS ='R' AND BRANCH_CODE='" + cmbBranch_optional.SelectedValue + "'", CommandType.Text).Tables[0];
                else if (cmbStatus.SelectedIndex == 3)
                    dtPending = objSQLDB.ExecuteDataSet("SELECT HAMH_COMPANY_CODE,HAMH_BRANCH_CODE,HAMH_APPL_NUMBER,HAMH_EORA_CODE"+
                        ",HAED_SSC_NUMBER,HAMH_NAME,HAMH_FORH_NAME,HAMH_DOB,HAMH_DOJ,isnull(HAMH_APPL_PENDING_REASON,'') as HAMH_REASON FROM "+
                        "HR_APPL_MASTER_HEAD MD INNER JOIN HR_APPL_CHECK AC ON MD.HAMH_APPL_NUMBER=AC.HAED_APPL_NUMBER INNER JOIN EORA_MASTER ON ECODE = HAMH_EORA_CODE "+
                        "WHERE HAMH_EORA_TYPE='A' AND HAMH_WORKING_STATUS ='L' AND BRANCH_CODE='" + cmbBranch_optional.SelectedValue + "'", CommandType.Text).Tables[0];
            }
            objSQLDB = null;
            int intRow = 1;
            gvPendingData.Rows.Clear();

            for (int i = 0; i < dtPending.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellcCode = new DataGridViewTextBoxCell();
                cellcCode.Value = dtPending.Rows[i]["HAMH_COMPANY_CODE"];
                tempRow.Cells.Add(cellcCode);
                DataGridViewCell cellbcode = new DataGridViewTextBoxCell();
                cellbcode.Value = dtPending.Rows[i]["HAMH_BRANCH_CODE"];
                tempRow.Cells.Add(cellbcode);
                DataGridViewCell cellAppno = new DataGridViewTextBoxCell();
                cellAppno.Value = dtPending.Rows[i]["HAMH_APPL_NUMBER"];
                tempRow.Cells.Add(cellAppno);


                DataGridViewCell cellEoraCode = new DataGridViewTextBoxCell();
                cellEoraCode.Value = dtPending.Rows[i]["HAMH_EORA_CODE"];
                tempRow.Cells.Add(cellEoraCode);

                DataGridViewCell cellsSSC = new DataGridViewTextBoxCell();
                cellsSSC.Value = dtPending.Rows[i]["HAED_SSC_NUMBER"];
                tempRow.Cells.Add(cellsSSC);

                DataGridViewCell cellsName = new DataGridViewTextBoxCell();
                cellsName.Value = dtPending.Rows[i]["HAMH_NAME"];
                tempRow.Cells.Add(cellsName);

                DataGridViewCell cellFName = new DataGridViewTextBoxCell();
                cellFName.Value = dtPending.Rows[i]["HAMH_FORH_NAME"];
                tempRow.Cells.Add(cellFName);

                DataGridViewCell cellDateofBirth = new DataGridViewTextBoxCell();
                if (dtPending.Rows[i]["HAMH_DOB"].ToString() != "")
                    cellDateofBirth.Value = Convert.ToDateTime(dtPending.Rows[i]["HAMH_DOB"]).ToString("dd/MM/yyyy");
                else
                    cellDateofBirth.Value = "";
                tempRow.Cells.Add(cellDateofBirth);

                DataGridViewCell cellDateofjoin = new DataGridViewTextBoxCell();
                if (dtPending.Rows[i]["HAMH_DOJ"].ToString() != "")
                    cellDateofjoin.Value = Convert.ToDateTime(dtPending.Rows[i]["HAMH_DOJ"]).ToString("dd/MM/yyyy");
                else
                    cellDateofjoin.Value = "";
                tempRow.Cells.Add(cellDateofjoin);

                DataGridViewCell cellQual = new DataGridViewTextBoxCell();
                cellQual.Value = "";
                tempRow.Cells.Add(cellQual);

                DataGridViewCell cellLnk = new DataGridViewTextBoxCell();
                cellLnk.Value = dtPending.Rows[i]["HAMH_REASON"];
                tempRow.Cells.Add(cellLnk);

                DataGridViewCell cellFlag = new DataGridViewTextBoxCell();
                cellFlag.Value = "";
                tempRow.Cells.Add(cellFlag);

                intRow = intRow + 1;
                gvPendingData.Rows.Add(tempRow);
            }
        }

        private void gvPendingData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (gvPendingData.Rows[e.RowIndex].Cells["Edit"].Value.ToString().Trim() != "")
                {
                    if (Convert.ToBoolean(gvPendingData.Rows[e.RowIndex].Cells["Edit"].Selected) == true)
                    {
                        string CompanyCode = gvPendingData.Rows[e.RowIndex].Cells[gvPendingData.Columns["HAMH_COMPANY_CODE"].Index].Value.ToString();
                        string BranchCode = gvPendingData.Rows[e.RowIndex].Cells[gvPendingData.Columns["HAMH_BRANCH_CODE"].Index].Value.ToString();
                        string ApplNo = gvPendingData.Rows[e.RowIndex].Cells[gvPendingData.Columns["HAMH_APPL_NUMBER"].Index].Value.ToString();
                        frmApplication frmAppli = new frmApplication(CompanyCode, BranchCode, ApplNo.ToString(), "Approved");
                        //this.Hide();
                        frmAppli.ShowDialog();
                    }
                }
                if (e.ColumnIndex == gvPendingData.Columns["lnkReason"].Index)
                {
                    string CompanyCode = gvPendingData.Rows[e.RowIndex].Cells[gvPendingData.Columns["HAMH_COMPANY_CODE"].Index].Value.ToString();
                    string BranchCode = gvPendingData.Rows[e.RowIndex].Cells[gvPendingData.Columns["HAMH_BRANCH_CODE"].Index].Value.ToString();
                    string ApplNo = gvPendingData.Rows[e.RowIndex].Cells[gvPendingData.Columns["HAMH_APPL_NUMBER"].Index].Value.ToString();
                    frmReason frmChldReason = new frmReason(CompanyCode, BranchCode, Convert.ToInt32(ApplNo));
                    frmChldReason.objApprovedStatus = this;
                    frmChldReason.ShowDialog();
                }
                if (e.ColumnIndex == gvPendingData.Columns["chkApproved"].Index)
                {
                    bool isExist = true;
                    bool cbchecked = (bool)gvPendingData.Rows[e.RowIndex].Cells["chkApproved"].EditedFormattedValue;
                    if (cbchecked == false)
                    {
                        objSQLDB = new SQLDB();
                        Int32 count = 1;//Convert.ToInt32(objSQLDB.ExecuteDataSet("SELECT COUNT(*) FROM HR_APPL_APPROVAL_DETL WHERE HAAD_APPL_NUMBER = '" + gvPendingData.Rows[e.RowIndex].Cells[gvPendingData.Columns["HAMH_APPL_NUMBER"].Index].Value.ToString() + "'").Tables[0].Rows[0][0].ToString());
                        if (count == 0)
                        {
                            //gvPendingData.Rows[e.RowIndex].Cells["chkApproved"].Value = true;
                            //((DataGridView)sender)[gvPendingData.Columns["chkApproved"].Index, e.RowIndex].Value = false;
                            //CheckBox cb = (CheckBox)((DataGridView)sender)[gvPendingData.Columns["chkApproved"].Index, e.RowIndex].Value;
                            //cb = new CheckBox();
                            //cb.Checked = false;
                            //MessageBox.Show("Incomplete Data For Agent : " + gvPendingData.Rows[e.RowIndex].Cells["HAMH_EORA_CODE"].Value, "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //DataGridViewCheckBoxCell Check = (DataGridViewCheckBoxCell)gvPendingData.Rows[e.RowIndex].Cells["chkApproved"];
                            isExist = false;
                            //gvPendingData.Rows[e.RowIndex].Cells["aFlag"].Value = "YES";                            
                            //GetPendingData();                            
                        }
                        //if (count != 0)
                        //    count = Convert.ToInt32(objSQLDB.ExecuteDataSet("SELECT COUNT(*) FROM HR_APPL_EDU_DETL WHERE HAED_APPL_NUMBER = '" + gvPendingData.Rows[e.RowIndex].Cells[gvPendingData.Columns["HAMH_APPL_NUMBER"].Index].Value.ToString() + "'").Tables[0].Rows[0][0].ToString());
                        //if (count == 0)
                        //{
                        //    //gvPendingData.Rows[e.RowIndex].Cells["chkApproved"].Value = true;
                        //    //((DataGridView)sender)[gvPendingData.Columns["chkApproved"].Index, e.RowIndex].Value = false;
                        //    //CheckBox cb = (CheckBox)((DataGridView)sender)[gvPendingData.Columns["chkApproved"].Index, e.RowIndex].Value;
                        //    //cb = new CheckBox();
                        //    //cb.Checked = false;
                        //    //MessageBox.Show("Incomplete Data For Agent : " + gvPendingData.Rows[e.RowIndex].Cells["HAMH_EORA_CODE"].Value, "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //    DataGridViewCheckBoxCell Check = (DataGridViewCheckBoxCell)gvPendingData.Rows[e.RowIndex].Cells["chkApproved"];
                        //    Check.Selected = false;
                        //    isExist = false;
                        //    //gvPendingData.Rows[e.RowIndex].Cells["aFlag"].Value = "YES";
                        //    //GetPendingData();
                        //}
                        if (gvPendingData.Rows[e.RowIndex].Cells[gvPendingData.Columns["Qualfication"].Index].Value.ToString().Trim() == "")
                        {
                            isExist = false;
                            //MessageBox.Show("Enter Qualification For Agent : " + gvPendingData.Rows[e.RowIndex].Cells["HAMH_EORA_CODE"].Value, "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            gvPendingData.Rows[e.RowIndex].Cells["aFlag"].Value = "YES";
                        }
                        if (isExist == false)
                        {
                            MessageBox.Show("Incomplete Data For Agent : " + gvPendingData.Rows[e.RowIndex].Cells["HAMH_EORA_CODE"].Value, "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            gvPendingData.Rows[e.RowIndex].Cells["aFlag"].Value = "YES";
                        }
                        else
                        {
                            gvPendingData.Rows[e.RowIndex].Cells["aFlag"].Value = "NO";
                        }
                    }
                }
                
            }

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int i = 0;
            ListBox ChkedRow = new ListBox();
            label1.Text = "";
            lblQulf.Text = "";
            lblEcode.Text = "";
            for (i = 0; i <= gvPendingData.RowCount - 1; i++)
            {
                if (Convert.ToBoolean(gvPendingData.Rows[i].Cells["chkApproved"].Value) == true)
                {
                    label1.Text += gvPendingData.Rows[i].Cells["HAMH_APPL_NUMBER"].Value.ToString() + ",";
                    lblQulf.Text += gvPendingData.Rows[i].Cells["Qualfication"].Value.ToString() + ",";
                    lblEcode.Text += gvPendingData.Rows[i].Cells["HAMH_EORA_CODE"].Value.ToString() + ",";
                }
            }
            foreach (DataGridViewRow row in gvPendingData.Rows)
            {
                if (row.Cells["aflag"].Value.ToString() == "YES")
                    row.Cells[13].Value = false;
            }
            if (label1.Text != "")
            {
                if (cmbStatus.SelectedIndex != 2)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want approved this records?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        try
                        {
                            objSQLDB = new SQLDB();
                            string[] sValue = label1.Text.TrimEnd(',').Split(',');
                            string[] sQValue = lblQulf.Text.TrimEnd(',').Split(',');
                            string[] sEValue = lblEcode.Text.TrimEnd(',').Split(',');
                            string SqlUpdate = "";
                            DataTable dtMaxApr = objSQLDB.ExecuteDataSet("SELECT  MAX(isnull(HAMH_APPROVAL_NO,0))+1 AS HAMH_APPROVAL_NO FROM HR_APPL_MASTER_HEAD", CommandType.Text).Tables[0];
                            int iMaxApr = Convert.ToInt32(dtMaxApr.Rows[0][0]);
                            for (int j = 0; j < sValue.Length; j++)
                            {
                                if (sQValue[j].Trim().ToString() != "")
                                {
                                    SqlUpdate += " UPDATE HR_APPL_MASTER_HEAD SET HAMH_APPROVAL_NO=" + iMaxApr + 
                                        ",HAMH_WORKING_STATUS='W',HAMH_APPROVAL_DATE='" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") + 
                                        "',HAMH_MODIFIED_BY='" + CommonData.LogUserId + "',HAMH_MODIFIED_DATE='" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") + 
                                        "' WHERE HAMH_APPL_NUMBER=" + sValue[j];
                                    //SqlUpdate += " update eora_master set edu_qualification=(select top 1 haed_examination_passed from dbo.HR_APPL_EDU_DETL where " +
                                    //    " haed_appl_number=a.HAMH_APPL_NUMBER order by haed_appl_sl_number desc)From HR_APPL_MASTER_HEAD a inner join eora_master b " +
                                    //    " on a.hamh_eora_code=b.ecode where HAMH_APPL_NUMBER=" + sValue[j] + ";";
                                    SqlUpdate += " update eora_master set edu_qualification='" + sQValue[j] + "' where ecode=" + sEValue[j] + ";";
                                    SqlUpdate += " INSERT into hr_appl_approval_history" +
                                                    "(haah_approval_no" +
                                                    ", haah_eora_code" +
                                                    ", haah_appl_no" +
                                                    ", haah_approval_date" +
                                                    ", haah_approval_by"+
                                                    ", haah_branch_code)" +
                                                    " Values(" +
                                                    "" + iMaxApr +
                                                    "," + sEValue[j] +
                                                    "," + sValue[j] +
                                                    ",getdate(),'" + CommonData.LogUserId +
                                                    "','" + cmbBranch_optional.SelectedValue.ToString() + "')";
                                }


                            }
                            int iretuval = objSQLDB.ExecuteSaveData(SqlUpdate);
                            //This is for Sending mail.
                            if (iretuval > 0)
                            {
                                DataSet dsBrnchEmail = objSQLDB.ExecuteDataSet("SELECT BRANCH_MAIL_ID"+
                                    ",BM_HR_EMAIL"+
                                    ",BM_REG_HR_MAIL"+
                                    ",BM_TRAINER_MAIL"+
                                    ",BM_INCH_MAIL"+
                                    " FROM BRANCH_MAS WHERE BRANCH_CODE='" + cmbBranch_optional.SelectedValue + "'");
                                if (dsBrnchEmail.Tables[0].Rows[0][0].ToString() != "")
                                {
                                    DataTable dtMailData = objSQLDB.ExecuteDataSet("SELECT ecode,member_name,father_name,emp_doj,emp_dob,DATEDIFF(YEAR, emp_dob, GETDATE()) Age,desig,edu_qualification " +
                                                        "FROM HR_APPL_MASTER_HEAD a inner join eora_master b on a.hamh_eora_code=b.ecode " +
                                                        "WHERE HAMH_APPL_NUMBER IN(" + label1.Text.TrimEnd(',') + ") ORDER BY DATEDIFF(YEAR, emp_dob, GETDATE()) DESC", CommandType.Text).Tables[0];

                                    string Mailbody = "<br /><br /><table padding='0' font-family= 'Segoe UI' cellpadding='5' cellspacing='0' border='1'>";
                                    Mailbody += "<tr><td colspan=\"9\"><a href=\"www.shivashakthigroup.com\">" +
                                                    "<img src=\"http://shivashakthigroup.com/wp-content/uploads/2013/01/logo.png\" alt=\"Shivashakthi Group of Companies\"/></a></td></tr>";
                                    Mailbody += "<tr style =\"background-color:#6FA1D2; color:#ffffff;\"><td>SlNo</td><td>ECode</td><td>Employee Name</td><td>Father Name</td><td>DOJ</td>" +
                                        "<td>DOB</td><td>Age</td><td>Desig</td><td>Qualification</td></tr>";
                                    int j = 1;
                                    foreach (DataRow dr in dtMailData.Rows)
                                    {
                                        Mailbody += "<tr><td>" + j.ToString() + "</td>" +
                                            "<td>" + dr["ecode"].ToString() + "</td>" +
                                            "<td>" + dr["member_name"].ToString().ToUpper() + "</td>" +
                                            "<td>" + dr["father_name"].ToString().ToUpper() + "</td>" +
                                            "<td>" + Convert.ToDateTime(dr["emp_doj"]).ToString("dd/MMM/yyyy").ToUpper() + "</td>" +
                                            "<td>" + Convert.ToDateTime(dr["emp_dob"]).ToString("dd/MMM/yyyy").ToUpper() + "</td>" +
                                            "<td>" + dr["Age"].ToString().ToUpper() + "</td>" +
                                            "<td>" + dr["desig"].ToString().ToUpper() + "</td>" +                                            
                                            "<td>" + dr["edu_qualification"].ToString().ToUpper() + "</td></tr>";
                                        j++;
                                    }
                                    Mailbody += "</table>";
                                    SendMail(dsBrnchEmail.Tables[0], iMaxApr, Mailbody);
                                }
                            }
                            MessageBox.Show("The records approved successfully", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            objSQLDB = null;
                            GetPendingData();
                            ReportViewer childForm = new ReportViewer(iMaxApr);
                            CommonData.ViewReport = "ApprovedDetails";
                            childForm.Show();


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want approved for left this records?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        objSQLDB = new SQLDB();
                        string LoginID = objSQLDB.ExecuteDataSet("SELECT UM_ECODE FROM USER_MASTER WHERE UM_USER_ID='" + CommonData.LogUserId + "'").Tables[0].Rows[0][0].ToString();
                        string SqlUpdate = " UPDATE HR_APPL_MASTER_HEAD SET HAMH_LEFT_APPROVAL_FLAG='YES',HAMH_WORKING_STATUS='L',HAMH_LEFT_APPROVAL_ECODE='" + LoginID + "' WHERE HAMH_APPL_NUMBER in(" + label1.Text.TrimEnd(',') + ") AND HAMH_COMPANY_CODE='" + cmbCompany.SelectedValue + "' AND HAMH_BRANCH_CODE='" + cmbBranch_optional.SelectedValue + "'";
                        int iretuval = objSQLDB.ExecuteSaveData(SqlUpdate);
                        MessageBox.Show("The records left approved successfully", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        objSQLDB = null;
                        GetPendingData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Select atleast one checkbox", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public string SendMail(DataTable dt, int RefNo, string body)
        {
            String[] addrCC = { "umahr@sivashakthi.net" };
            var fromAddress = new MailAddress("ssbplitho@gmail.com", "SSCRM-HR :: Agent Approvals");
            var toAddress = new MailAddress(dt.Rows[0]["BRANCH_MAIL_ID"].ToString());
            const string fromPassword = "ssbplitho5566";
            try
            {
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = "SSCRM Approved Agents List - Document Ref No:  " + RefNo,
                    Body = "This is to inform that the below agents are approved on " + CommonData.CurrentDate + 
                                ", you will receive approval letter soon, </br>The letter reference no is :" + RefNo + 
                                "</br></br></br>" + body + "</br></br><table width='80%'><tr><td align='center'>"+
                                "</td></tr><tr><td style='height:50px'></td></tr><tr><td> This is server generated mail please do not reply to this mail."+
                                "</td></tr></table>",
                    IsBodyHtml = true
                })
                {
                    if (dt.Rows[0]["BM_HR_EMAIL"].ToString() != "")
                        message.CC.Add(dt.Rows[0]["BM_HR_EMAIL"].ToString());
                    if (dt.Rows[0]["BM_REG_HR_MAIL"].ToString() != "")
                        message.CC.Add(dt.Rows[0]["BM_REG_HR_MAIL"].ToString());
                    if (dt.Rows[0]["BM_TRAINER_MAIL"].ToString() != "")
                        message.CC.Add(dt.Rows[0]["BM_TRAINER_MAIL"].ToString());
                    if (dt.Rows[0]["BM_INCH_MAIL"].ToString() != "")
                        message.CC.Add(dt.Rows[0]["BM_INCH_MAIL"].ToString());
                    for (int i = 0; i < addrCC.Length; i++)
                        message.CC.Add(addrCC[i]);
                    message.To.Add("nareshit@sivashakthi.net");
                    smtp.Send(message);
                    return "Yes";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            int i = 0;
            ListBox ChkedRow = new ListBox();
            label1.Text = "";
            for (i = 0; i <= gvPendingData.RowCount - 1; i++)
            {
                if (Convert.ToBoolean(gvPendingData.Rows[i].Cells["chkApproved"].Value) == true)
                {
                    label1.Text += gvPendingData.Rows[i].Cells["HAMH_APPL_NUMBER"].Value.ToString() + ",";
                }
            }
            if (label1.Text != "")
            {
                objSQLDB = new SQLDB();
                string[] sValue = label1.Text.TrimEnd(',').Split(',');
                string SqlUpdate = "";
                for (int j = 0; j < sValue.Length; j++)
                {
                    SqlUpdate += " UPDATE HR_APPL_MASTER_HEAD SET HAMH_WORKING_STATUS='R',HAMH_APPROVAL_DATE=GETDATE(),HAMH_MODIFIED_BY='" + CommonData.LogUserId + "',HAMH_MODIFIED_DATE=GETDATE() WHERE HAMH_APPL_NUMBER=" + sValue[j] + " AND HAMH_COMPANY_CODE='" + cmbCompany.SelectedValue + "' AND HAMH_BRANCH_CODE='" + cmbBranch_optional.SelectedValue + "'";
                }
                int iretuval = objSQLDB.ExecuteSaveData(SqlUpdate);
                MessageBox.Show("The records rejected successfully", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                objSQLDB = null;
                GetPendingData();
            }
            else
            {
                MessageBox.Show("Select atleast one checkbox", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCompany.SelectedIndex > 0)
            {
                objHrInfo = new HRInfo();
                DataTable dtBranch = objHrInfo.GetAllBranchList(cmbCompany.SelectedValue.ToString(), "", "").Tables[0];
                UtilityLibrary.PopulateControl(cmbBranch_optional, dtBranch.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
                objHrInfo = null;
            }
        }

        private void cmbBranch_optional_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBranch_optional.SelectedIndex > 0)
                GetPendingData();
            else
                gvPendingData.Rows.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbCompany.SelectedIndex = 0;
            cmbBranch_optional.SelectedIndex = 0;
            cmbCompany_SelectedIndexChanged(null, null);
            gvPendingData.DataSource = null;
            gvPendingData.Rows.Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPendingData();
            if (cmbStatus.SelectedIndex == 2)
                btnReject.Enabled = false;
            else
                btnReject.Enabled = true;
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbType.SelectedIndex == 1)
            {
                cmbStatus.SelectedIndex = 2;
                cmbStatus.Enabled = false;
                btnReject.Enabled = false;
            }
            else
            {
                cmbStatus.Enabled = true;
                btnReject.Enabled = true;
            }
            GetPendingData();
        }

        private void gvPendingData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 13)
            {
                if (e.RowIndex != -1)
                {
                    foreach (DataGridViewRow row in gvPendingData.Rows)
                    {
                        if (row.Cells["aflag"].Value.ToString() == "YES")
                            row.Cells[13].Value = false;
                    }
                }
            }
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            gvPendingData.ClearSelection();
            int rowIndex = 0;
            foreach (DataGridViewRow row in gvPendingData.Rows)
            {
                if (row.Cells[4].Value.ToString() == txtEcodeSearch.Text)
                {
                    rowIndex = row.Index;
                    gvPendingData.CurrentCell = gvPendingData.Rows[rowIndex].Cells[4];
                    break;
                }
            }
        }
    }
}