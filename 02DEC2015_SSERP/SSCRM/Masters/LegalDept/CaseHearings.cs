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
using SSTrans;
using SSAdmin;

namespace SSCRM
{
    public partial class CaseHearings : Form
    {
        SQLDB objDb = null;
        DataTable dtDtl=null;
        bool isModify = false, isPrevRecords=false;
        string strCompCode = "", strBranchCode = "", strCaseType = "", strCaseNumber = "", strCompName="",strBranchName="",strStateCode="";
        DateTime dtHearingDate = Convert.ToDateTime("01-JAN-1900");
        LeagalCaseDetails objLD=null;
        public CaseHearings()
        {
            InitializeComponent();
        }
        public CaseHearings(string sCompCode,string sBranchCode,string sCType,string sCaseNo,string sCompName,string sBranchName,string sStateCode,LeagalCaseDetails obj)
        {
            strCompCode = sCompCode;
            strBranchCode = sBranchCode;
            strCaseType = sCType;
            strCaseNumber = sCaseNo;
            strCompName = sCompName;
            strBranchName = sBranchName;
            strStateCode = sStateCode;
            objLD = obj;
            InitializeComponent();

        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnAddEcode_Click(object sender, EventArgs e)
        {
            AttendedByName objAttendedByName = new AttendedByName();
            objAttendedByName.objCaseHearings = this;
            objAttendedByName.ShowDialog();
            //Partners objPartners = new Partners();
            //objPartners.dealerApplication = this;
            //objPartners.ShowDialog();
        }

        private void CaseHearings_Load(object sender, EventArgs e)
        {
            //FillCompanyData();
            //FillCaseType();
            FillStatus();
            txtCompName.Tag = strCompCode;
            txtCompName.Text = strCompName;

            txtBranchName.Tag = strBranchCode;
            txtBranchName.Text = strBranchName;

            txtCaseType.Tag = strStateCode;
            txtCaseType.Text = strCaseType;

            txtCaseNo.Text = strCaseNumber;


        }
        private void FillStatus()
        {
            try
            {
                cbStatus.DataSource = dtStatus();
                cbStatus.DisplayMember = "name";
                cbStatus.ValueMember = "type";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private DataTable dtStatus()
        {
            DataTable table = new DataTable();
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("name", typeof(string));
            table.Rows.Add("--SELECT--", "--SELECT--");
            table.Rows.Add("CLOSED", "CLOSED");
            table.Rows.Add("TRIAL", "TRIAL");
            table.Rows.Add("PENDING", "PENDING");
            table.Rows.Add("APPEAL PENDING", "APPEAL PENDING");
            return table;
        }
        private void FillCaseType()
        {
            //try
            //{
            //    cbCaseType.DataSource = dtCaseTypes();
            //    cbCaseType.DisplayMember = "CaseType";
            //    cbCaseType.ValueMember = "CaseType";
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }
        private DataTable dtCaseTypes()
        {
            DataTable table = new DataTable();

            string strSql = "select distinct(LCM_CASE_TYPE) CaseType,LCM_CASE_TYPE from legal_case_master";
            objDb = new SQLDB();
            table = objDb.ExecuteDataSet(strSql).Tables[0];

            DataRow dr = table.NewRow();
            dr[0] = "--Select--";
            dr[1] = "--Select--";
            table.Rows.InsertAt(dr, 0);

            //DataRow dr1 = table.NewRow();
            //dr1[0] = "CRIMINAL";
            //dr1[1] = "CRIMINAL";
            //table.Rows.InsertAt(dr1, 1);

            //DataRow dr2 = table.NewRow();
            //dr2[0] = "CIVIL";
            //dr2[1] = "CIVIL";
            //table.Rows.InsertAt(dr2, 2);

            //DataRow dr3 = table.NewRow();
            //dr3[0] = "CONSUMER";
            //dr3[1] = "CONSUMER";
            //table.Rows.InsertAt(dr3, 3);


           
            //table.Columns.Add("type", typeof(string));
            //table.Columns.Add("name", typeof(string));

            //table.Rows.Add("CRIMINAL", "CRIMINAL");
            //table.Rows.Add("CIVIL", "CIVIL");
            //table.Rows.Add("CONSUMER", "CONSUMER");

            return table;
        }
        private void FillCompanyData()
        {
            //objDb = new SQLDB();
            //DataTable dt = new DataTable();
            //try
            //{
            //    string strCommand = "SELECT CM_COMPANY_NAME,CM_COMPANY_CODE FROM COMPANY_MAS where active='t'";

            //    dt = objDb.ExecuteDataSet(strCommand).Tables[0];
            //    if (dt.Rows.Count > 0)
            //    {
            //        DataRow dr = dt.NewRow();
            //        dr[0] = "--Select--";
            //        dr[1] = "--Select--";

            //        dt.Rows.InsertAt(dr, 0);
            //        cbCompany.DataSource = dt;
            //        cbCompany.DisplayMember = "CM_COMPANY_NAME";
            //        cbCompany.ValueMember = "CM_COMPANY_CODE";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            //finally
            //{
            //    objDb = null;
            //    dt = null;
            //}
        }
        private void FillLocationData()
        {
            //objDb = new SQLDB();
            //DataTable dt = new DataTable();
            //cbBranch.DataSource = null;
            //try
            //{
            //    if (cbCompany.SelectedIndex > 0)
            //    {
            //        string strCommand = "SELECT BRANCH_CODE+'@'+STATE_CODE as branchCode,BRANCH_NAME FROM BRANCH_MAS WHERE COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + "' and active='t' ORDER BY BRANCH_NAME ASC";
            //        dt = objDb.ExecuteDataSet(strCommand).Tables[0];
            //    }
            //    if (dt.Rows.Count > 0)
            //    {
            //        DataRow dr = dt.NewRow();
            //        dr[0] = "--Select--";
            //        dr[1] = "--Select--";

            //        dt.Rows.InsertAt(dr, 0);
            //        cbBranch.DataSource = dt;
            //        cbBranch.DisplayMember = "BRANCH_NAME";
            //        cbBranch.ValueMember = "branchCode";
            //        //cbLocation.ValueMember = "LOCATION";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            //finally
            //{
            //    objDb = null;
            //    dt = null;
            //}
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLocationData();

        }
        private void FillCaseNumbers()
        {
            //try
            //{
            //    objDb = new SQLDB();
            //    SqlParameter[] param = new SqlParameter[3];
            //    DataTable dt = new DataTable();
            //    if(cbCompany.SelectedIndex>0 && cbBranch.SelectedIndex>0 && cbCaseType.SelectedIndex>0)
            //    {
            //        param[0] = objDb.CreateParameter("@xCompanyCode", DbType.String, cbCompany.SelectedValue.ToString(), ParameterDirection.Input);
            //        param[1] = objDb.CreateParameter("@xBranchCode", DbType.String, cbBranch.SelectedValue.ToString().Split('@')[0], ParameterDirection.Input);
            //        param[2] = objDb.CreateParameter("@xCaseType", DbType.String, cbCaseType.SelectedValue.ToString(), ParameterDirection.Input);
            //        dt = objDb.ExecuteDataSet("getLegalCaseNumber", CommandType.StoredProcedure, param).Tables[0];
            //    }
            //    if (dt.Rows.Count > 0)
            //    {
            //        DataRow dr = dt.NewRow();
            //        dr[0] = "--Select--";
            //        dr[1] = "--Select--";
            //        dt.Rows.InsertAt(dr, 0);

            //        cbCaseNo.DataSource = dt;
            //        cbCaseNo.DisplayMember = "LCM_CASE_NUMBER";
            //        cbCaseNo.ValueMember = "LCM_CASE_NUMBER";

                    
            //    }
            //    else
            //    {
            //        cbCaseNo.DataSource = null;
            //    }
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }

        private void cbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCaseNumbers();
        }

        private void cbCaseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCaseNumbers();
        }

        private void gvAttendedBy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvAttendedBy.Columns["Delete"].Index)
            {
                DataGridViewRow row = gvAttendedBy.Rows[e.RowIndex];
                gvAttendedBy.Rows.Remove(row);
                OrderSINo();
            }
        }

        private void OrderSINo()
        {
            if (gvAttendedBy.Rows.Count > 0)
            {
                for (int i = 0; i < gvAttendedBy.Rows.Count; i++)
                {
                    gvAttendedBy.Rows[i].Cells["SiNO"].Value = i + 1;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(CheckData())
            {
                if (SaveCaseHearings() > 0)
                {
                    MessageBox.Show("Data Saved Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClear_Click(null, null);
                    dtHearingDate = Convert.ToDateTime("01-JAN-1900");
                    isModify = false;
                    isPrevRecords = false;
                    dtDtl = null;
                    objLD.txtCaseNo_KeyUp(null,null);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnClear_Click(null, null);
                    dtHearingDate = Convert.ToDateTime("01-JAN-1900");
                    isModify = false;
                    isPrevRecords = false;
                    dtDtl = null;
                }
            }
        }

        private int SaveCaseHearings()
        {
            int iSave = 0;
            try
            {
                if (txtLwrFee.Text.ToString().Trim().Length == 0)
                    txtLwrFee.Text = "0.00";
                if (txtStmpFees.Text.ToString().Trim().Length == 0)
                    txtStmpFees.Text = "0.00";
                if (txtMiscAmt.Text.ToString().Trim().Length == 0)
                    txtMiscAmt.Text = "0.00";
                string strSql = "";

                strSql = "SELECT max(LCH_DATE) FROM LEGAL_CASE_HEARINGS WHERE LCH_CASE_TYPE='"+txtCaseType.Text
                                                                    +"' AND LCH_CASE_NUMBER='"+txtCaseNo.Text
                                                                    + "' and LCH_COMPANY_CODE='"+txtCompName.Tag
                                                                    +"' and LCH_STATE_CODE='"+ txtCaseType.Tag 
                                                                    +"' and LCH_BRANCH_CODE='" + txtBranchName.Tag  +"'";

                objDb = new SQLDB();
                string strLastHeringDate = objDb.ExecuteDataSet(strSql).Tables[0].Rows[0][0].ToString();
                DateTime dtLastHeringDate=Convert.ToDateTime("01-JAN-1800");
                if(strLastHeringDate.Length!=0)
                 dtLastHeringDate = Convert.ToDateTime(strLastHeringDate);
                if (isPrevRecords == true )
                {
                    if (dtLastHeringDate == dtHearingDate)
                    {
                        strSql = "";

                        strSql += " DELETE FROM LEGAL_CASE_HEARINGS WHERE LCH_COMPANY_CODE='" + txtCompName.Tag +
                                                                   "' AND  LCH_STATE_CODE='" + txtCaseType.Tag +
                                                                   "' AND LCH_BRANCH_CODE='" + txtBranchName.Tag +
                                                                   "' AND LCH_CASE_TYPE='" + txtCaseType.Text +
                                                                   "' AND LCH_CASE_NUMBER='" + txtCaseNo.Text +
                                                                   "' AND LCH_DATE='" + Convert.ToDateTime(dtHearingDate).ToString("dd/MMM/yyyy") + "'";
                        objDb = new SQLDB();
                        iSave = objDb.ExecuteSaveData(strSql);
                    }
                    else if (isModify == true && dtLastHeringDate != dtHearingDate)
                    {
                        MessageBox.Show("You can't Change Previous dates");
                        return 0;
                    }
                }
                strSql = "";
                iSave = 0;
                string strNextDate = "";
                if (cbStatus.Text == "CLOSED")
                {
                    strNextDate = dtpHearingDate.Value.ToShortDateString();
                }
                else
                    strNextDate = dtpNextHearingDate.Value.ToString("dd/MMM/yyyy");

                    for (int iRow = 0; iRow < gvAttendedBy.Rows.Count; iRow++)
                    {
                        strSql += " insert into LEGAL_CASE_HEARINGS(LCH_COMPANY_CODE" +
                                                                ",LCH_STATE_CODE" +
                                                                ",LCH_BRANCH_CODE" +
                                                                ",LCH_CASE_TYPE" +
                                                                ",LCH_CASE_NUMBER" +
                                                                ",LCH_DATE" +
                                                                ",LCH_ATTENDED_FLAG" +
                                                                ",LCH_ATTENDED_SL_NO" +
                                                                ",LCH_ATTENDED_TYPE"+
                                                                ",LCH_ATTENDED_BY" +
                                                                ",LCH_REMARKS" +
                                                                ",LCH_STATUS" +
                                                                ",LCH_NEXT_DATE" +
                                                                ",LCH_CREATED_BY" +
                                                                ",LCH_CREATED_DATE" +
                                                                ",LCH_LAWYER_FEES" +
                                                                ",LCH_STAMP_FEES" +
                                                                ",LCH_MISCELLANEOUS_AMT" +
                                                                ") values('" + txtCompName.Tag +
                                                                "','" + txtCaseType.Tag +
                                                                "','" + txtBranchName.Tag +
                                                                "','" + txtCaseType.Text +
                                                                "','" + txtCaseNo.Text +
                                                                "','" + dtpHearingDate.Value.ToString("dd/MMM/yyyy") +
                                                                "','Y" +
                                                                "','" + gvAttendedBy.Rows[iRow].Cells["SiNO"].Value +
                                                                "','" + gvAttendedBy.Rows[iRow].Cells["type"].Value +
                                                                "'," + gvAttendedBy.Rows[iRow].Cells["Ecode"].Value +
                                                                ",'" + txtRemarks.Text +
                                                                "','" + cbStatus.SelectedValue.ToString() +
                                                                "','" + strNextDate +
                                                                "','" + CommonData.LogUserId +
                                                                "',getdate()"+
                                                                ",'"+txtLwrFee.Text+
                                                                "','"+txtStmpFees.Text+
                                                                "','"+txtMiscAmt.Text+
                                                                "')";
                    }
                    strSql += " insert into LEGAL_CASE_HEARINGS(LCH_COMPANY_CODE" +
                                                                  ",LCH_STATE_CODE" +
                                                                  ",LCH_BRANCH_CODE" +
                                                                  ",LCH_CASE_TYPE" +
                                                                  ",LCH_CASE_NUMBER" +
                                                                  ",LCH_DATE" +
                                                                  ",LCH_ATTENDED_FLAG" +
                                                                  ",LCH_ATTENDED_SL_NO" +
                                                                  ",LCH_ATTENDED_BY" +
                                                                  ",LCH_REMARKS" +
                                                                  ",LCH_STATUS" +
                                                                  ",LCH_NEXT_DATE" +
                                                                  ",LCH_CREATED_BY" +
                                                                  ",LCH_CREATED_DATE" +
                                                                  ") values('" +txtCompName.Tag +
                                                                  "','" + txtCaseType.Tag +
                                                                  "','" + txtBranchName.Tag +
                                                                  "','" + txtCaseType.Text +
                                                                  "','" + txtCaseNo.Text +
                                                                  "','" + dtpHearingDate.Value.ToString("dd/MMM/yyyy") +
                                                                  "','F" +
                                                                  "','0" +
                                                                  "',0" +
                                                                  ",' " +
                                                                  "','" + cbStatus.SelectedValue.ToString() +
                                                                  "','" +  strNextDate+
                                                                  "','" + CommonData.LogUserId +
                                                                  "',getdate())";
                    #region FOR UPDATING PREVIOUS RECORD
                    if (gvAttendedBy.Rows.Count==0 && isPrevRecords==true && isModify==false)
                    {
                        strSql += " UPDATE LEGAL_CASE_HEARINGS SET LCH_REMARKS='"+txtRemarks.Text+
                                                                  "',LCH_STATUS='"+cbStatus.SelectedValue.ToString()+
                                                                  "' WHERE LCH_COMPANY_CODE='" + dtDtl.Rows[dtDtl.Rows.Count - 1]["LCH_COMPANY_CODE"]+
                                                                  "' AND LCH_STATE_CODE='" + dtDtl.Rows[dtDtl.Rows.Count - 1]["LCH_STATE_CODE"]+
                                                                  "' AND LCH_BRANCH_CODE='" + dtDtl.Rows[dtDtl.Rows.Count - 1]["LCH_BRANCH_CODE"] +
                                                                  "' AND LCH_CASE_TYPE='" + dtDtl.Rows[dtDtl.Rows.Count - 1]["LCH_CASE_TYPE"]+
                                                                  "' AND LCH_CASE_NUMBER='" + dtDtl.Rows[dtDtl.Rows.Count - 1]["LCH_CASE_NUMBER"]+
                                                                  "' AND LCH_DATE='" +Convert.ToDateTime( dtDtl.Rows[dtDtl.Rows.Count - 1]["LCH_DATE"]).ToString("dd/MMM/yyyy")+"'";
                    }
                    if (gvAttendedBy.Rows.Count == 0 && isPrevRecords == true && isModify ==true)
                    {
                        strSql += " UPDATE LEGAL_CASE_HEARINGS SET LCH_REMARKS='" + txtRemarks.Text +
                                                                  "',LCH_STATUS='" + cbStatus.SelectedValue.ToString() +
                                                                  "' WHERE LCH_COMPANY_CODE='" + dtDtl.Rows[dtDtl.Rows.Count - 2]["LCH_COMPANY_CODE"] +
                                                                  "' AND LCH_STATE_CODE='" + dtDtl.Rows[dtDtl.Rows.Count - 2]["LCH_STATE_CODE"] +
                                                                  "' AND LCH_BRANCH_CODE='" + dtDtl.Rows[dtDtl.Rows.Count - 2]["LCH_BRANCH_CODE"] +
                                                                  "' AND LCH_CASE_TYPE='" + dtDtl.Rows[dtDtl.Rows.Count - 2]["LCH_CASE_TYPE"] +
                                                                  "' AND LCH_CASE_NUMBER='" + dtDtl.Rows[dtDtl.Rows.Count - 2]["LCH_CASE_NUMBER"] +
                                                                  "' AND LCH_DATE='" + Convert.ToDateTime(dtDtl.Rows[dtDtl.Rows.Count - 2]["LCH_DATE"]).ToString("dd/MMM/yyyy") + "'";
                    }
                    #endregion


                objDb = new SQLDB();
                iSave = objDb.ExecuteSaveData(strSql);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iSave;
        }
        private bool CheckData()
        {
            bool flag = true;
            //if (cbCompany.SelectedIndex == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Select Company", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    cbCompany.Focus();
            //    return flag;
            //}
            //if (cbBranch.SelectedIndex == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Select Branch", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    cbBranch.Focus();
            //    return flag;
            //}
            //if (cbCaseType.SelectedIndex == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Select CaseType", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    cbCaseType.Focus();
            //    return flag;
            //}
            //if (cbCaseNo.SelectedIndex == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Select CaseNumber", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    cbCaseNo.Focus();
            //    return flag;
            //}
            if (gvAttendedBy.Rows.Count == 0)
            {
                flag = false;
                MessageBox.Show("Please Add AttendedBy Name", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnAddEcode.Focus();
                return flag;
            }
            if (txtRemarks.Text.Length== 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Remarks", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRemarks.Focus();
                return flag;
            }
            if (cbStatus.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Status", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbStatus.Focus();
                return flag;
            }
            if(cbStatus.Text!="CLOSED")
            if(dtpHearingDate.Value >= dtpNextHearingDate.Value)
            {
                flag = false;
                MessageBox.Show("Next Hearing Date Must be Greater than Hearing Date", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbStatus.Focus();
                return flag;
            }
            return flag;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //cbCompany.SelectedIndex = 0;
            //cbCaseType.SelectedIndex = 0;
            //cbCaseNo.SelectedIndex = 0;
            gvAttendedBy.Rows.Clear();
            txtRemarks.Text = "";
            cbStatus.SelectedIndex = 0;

            dtHearingDate = Convert.ToDateTime("01-JAN-1900");
            isModify = false;
            isPrevRecords = false;
            dtDtl = null;
        }

        private void cbCaseNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LegalInfoDB objLegal = new LegalInfoDB();
            ////if (cbCaseNo.SelectedIndex > 0 && cbCompany.SelectedIndex > 0 && cbBranch.SelectedIndex > 0 && cbCaseType.SelectedIndex > 0)
            ////{
            //  DataTable   dtDtl = objLegal.getCaseHearingDetails(txtCompName.Tag.ToString(), txtBranchName.Tag.ToString(),txtCaseType.Text, "01-JAN-1900", txtCaseNo.Text);
            //     if(dtDtl.Rows.Count>0)
            //     {
            //         dtpHearingDate.Value = Convert.ToDateTime(dtDtl.Rows[dtDtl.Rows.Count - 1]["LCH_NEXT_DATE"].ToString());
            //         isPrevRecords = true;
            //     }
            ////}
            ////if (cbCaseNo.SelectedIndex > 0 && cbCompany.SelectedIndex>0 && cbBranch.SelectedIndex>0 && cbCaseType.SelectedIndex>0)
            ////{
                
            //     dtDtl = new DataTable();
            //    try
            //    {
            //        dtDtl = objLegal.getCaseHearingDetails(txtCompName.Tag.ToString(), txtBranchName.Tag.ToString(), txtCaseType.Text, dtpHearingDate.Value.ToString("dd/MMM/yyyy"), txtCaseNo.Text);
            //        FillCaseHearingDetails(dtDtl);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString());
            //    }
            //    finally
            //    {
            //        dtDtl = null;
            //    }
            ////}           
        }

        private void FillCaseHearingDetails(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                isModify = true;
                dtHearingDate = Convert.ToDateTime(dt.Rows[0]["LCH_DATE"].ToString());
                //txtCompName.Tag = dt.Rows[0]["LCH_COMPANY_CODE"].ToString();
                //cbBranch.SelectedValue = dt.Rows[0]["LCH_BRANCH_CODE"].ToString() + "@" + dt.Rows[0]["LCH_STATE_CODE"].ToString();
                //cbCaseType.SelectedValue = dt.Rows[0]["LCH_CASE_TYPE"].ToString();
                //cbCaseNo.SelectedValue = dt.Rows[0]["LCH_CASE_NUMBER"].ToString();
                dtpHearingDate.Value = Convert.ToDateTime(dt.Rows[0]["LCH_DATE"].ToString());
                gvAttendedBy.Rows.Clear();
                for (int iRow = 1; iRow < dt.Rows.Count; iRow++)
                {
                    gvAttendedBy.Rows.Add();
                    gvAttendedBy.Rows[iRow-1].Cells["SiNO"].Value = dt.Rows[iRow]["LCH_ATTENDED_SL_NO"].ToString();
                    gvAttendedBy.Rows[iRow - 1].Cells["type"].Value = dt.Rows[iRow]["LCH_ATTENDED_TYPE"].ToString();

                    gvAttendedBy.Rows[iRow-1].Cells["Ecode"].Value = dt.Rows[iRow]["LCH_ATTENDED_BY"].ToString();

                    Master objMstr = new Master();
                    DataSet ds = new DataSet();
                    try
                    {
                        ds = objMstr.GetEmployeeMasterDetl(gvAttendedBy.Rows[iRow-1].Cells["Ecode"].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    gvAttendedBy.Rows[iRow-1].Cells["EmpName"].Value = ds.Tables[0].Rows[0]["EmpName"].ToString();
                    ds = null;
                }
                txtRemarks.Text = dt.Rows[0]["LCH_REMARKS"].ToString();
                cbStatus.SelectedValue = dt.Rows[0]["LCH_STATUS"].ToString();
                dtpNextHearingDate.Value = Convert.ToDateTime(dt.Rows[0]["LCH_NEXT_DATE"].ToString());
            }
            else
            {
                isModify = false;
                //dtHearingDate = Convert.ToDateTime("01-JAN-1900");
                gvAttendedBy.Rows.Clear();
                txtRemarks.Text = "";
                cbStatus.SelectedIndex = 0;
                dtpNextHearingDate.Value = DateTime.Today;
            }
        }

        private void txtCaseNo_TextChanged(object sender, EventArgs e)
        {
            LegalInfoDB objLegal = new LegalInfoDB();
            //if (cbCaseNo.SelectedIndex > 0 && cbCompany.SelectedIndex > 0 && cbBranch.SelectedIndex > 0 && cbCaseType.SelectedIndex > 0)
            //{
            DataTable dtDtl = objLegal.getCaseHearingDetails(txtCompName.Tag.ToString(), txtBranchName.Tag.ToString(), txtCaseType.Text, "01-JAN-1900", txtCaseNo.Text);
            if (dtDtl.Rows.Count > 0)
            {
                dtpHearingDate.Value = Convert.ToDateTime(dtDtl.Rows[dtDtl.Rows.Count - 1]["LCH_NEXT_DATE"].ToString());
                isPrevRecords = true;
            }
            //}
            //if (cbCaseNo.SelectedIndex > 0 && cbCompany.SelectedIndex>0 && cbBranch.SelectedIndex>0 && cbCaseType.SelectedIndex>0)
            //{

            dtDtl = new DataTable();
            try
            {
                dtDtl = objLegal.getCaseHearingDetails(txtCompName.Tag.ToString(), txtBranchName.Tag.ToString(), txtCaseType.Text, dtpHearingDate.Value.ToString("dd/MMM/yyyy"), txtCaseNo.Text);
                FillCaseHearingDetails(dtDtl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                dtDtl = null;
            }
            //}          
        }

        private void btnLawyer_Click(object sender, EventArgs e)
        {
            bool flagLawyer = false;
            for (int i = 0; i < gvAttendedBy.Rows.Count;i++ )
            {
                if (gvAttendedBy.Rows[i].Cells["type"].Value == "LAWYER")
                {
                    flagLawyer = true;
                    return;
                }
            }
            if (flagLawyer == false)
            {
                gvAttendedBy.Rows.Add();
                gvAttendedBy.Rows[gvAttendedBy.Rows.Count - 1].Cells["SiNO"].Value = gvAttendedBy.Rows.Count;
                gvAttendedBy.Rows[gvAttendedBy.Rows.Count - 1].Cells["type"].Value = "LAWYER";
                gvAttendedBy.Rows[gvAttendedBy.Rows.Count - 1].Cells["Ecode"].Value = (objLD).cbLawyerName.SelectedValue;
                gvAttendedBy.Rows[gvAttendedBy.Rows.Count - 1].Cells["EmpName"].Value = ((System.Data.DataRowView)((objLD).cbLawyerName.Items[(objLD).cbLawyerName.SelectedIndex])).Row.ItemArray[0];
            }
        }

        private void txtLwrFee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtStmpFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtMiscAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStatus.Text == "CLOSED")
            {
                dtpNextHearingDate.Visible = false;
                label7.Visible = false;
            }
            else
            {
                dtpNextHearingDate.Visible = true;
                label7.Visible = true;
            }
        }

      

      

        //private void dtpHearingDate_ValueChanged(object sender, EventArgs e)
        //{
        //    if (cbCaseNo.SelectedIndex > 0 && cbCompany.SelectedIndex > 0 && cbBranch.SelectedIndex > 0 && cbCaseType.SelectedIndex > 0)
        //    {
        //        LegalInfoDB objLegal = new LegalInfoDB();
        //        DataTable dtDtl = new DataTable();
        //        try
        //        {
        //            dtDtl = objLegal.getCaseHearingDetails(cbCompany.SelectedValue.ToString(), cbBranch.SelectedValue.ToString().Split('@')[0], cbCaseType.SelectedValue.ToString(), dtpHearingDate.Value.ToString("dd/MMM/yyyy"), cbCaseNo.SelectedValue.ToString());
        //            FillCaseHearingDetails(dtDtl);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.ToString());
        //        }
        //        finally
        //        {
        //            dtDtl = null;
        //        }
        //    }       
        //}
    }
}
