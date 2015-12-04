using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using SSCRMDB;
using SSAdmin;

namespace SSCRM
{
    public partial class StationaryIndentList : Form
    {
        Security objData = null;
        SQLDB objSQLDB = null;
        UtilityDB objUtil = null;
        private string strForm = "";
        public StationaryIndentList()
        {
            InitializeComponent();
        }

        public StationaryIndentList(string sfrom)
        {
            InitializeComponent();
            strForm = sfrom;
        }

        private void StationaryIndentList_Load(object sender, EventArgs e)
        {
            //Hashtable htComp = new Hashtable();
            cmbStatus.SelectedIndex = 0;
            //FillCompanyData();
            //FillBranchData();
            //FillUserBranch();
            GetGridBind();
            gvIndentDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);
            if (strForm == "MGR" || strForm == "SHEAD")
            {
                cmbStatus.SelectedIndex = 0;
            }
            else if (strForm == "HEAD" )
            {
                cmbStatus.SelectedIndex = 2;
            }
            //else if (strForm == "SELFHEAD")
            //{
            //    cmbStatus.SelectedIndex = 0;
            //}
            else
            {
                cmbStatus.SelectedIndex = 0;
            }
        }
        //private void FillCompanyData()
        //{
        //    DataSet ds = null;
        //    objData = new Security();
        //    try
        //    {
        //        ds = new DataSet();

        //        ds = objData.GetCompanyDataSet();
        //        DataTable dtCompany = ds.Tables[0];
        //        if (dtCompany.Rows.Count > 0)
        //        {
        //            cbCompany.DisplayMember = "CM_Company_Name";
        //            cbCompany.ValueMember = "CM_Company_Code";
        //            cbCompany.DataSource = dtCompany;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        objData = null;
        //        ds.Dispose();
        //    }
        //}
        //private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cbCompany.SelectedIndex > -1)
        //    {
        //        FillBranchData();
        //    }
        //}
        //private void FillUserBranch()
        //{
        //    objUtil = new UtilityDB();
        //    try
        //    {
        //        DataTable dtUB = objUtil.dtUserBranch(CommonData.LogUserId);
        //        cbUserBranch.DisplayMember = "branch_name";
        //        cbUserBranch.ValueMember = "branch_code";
        //        cbUserBranch.DataSource = dtUB;
        //        cbUserBranch.SelectedIndex = 0;
        //        //cbUserBranch.SelectedValue = CommonData.BranchCode;
        //        dtUB = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        objUtil = null;
        //    }
        //}
        //private void FillBranchData()
        //{
        //    DataSet ds = null;
        //    objData = new Security();
        //    try
        //    {
        //        ds = new DataSet();
        //        ds = objData.UserBranchList(cbCompany.SelectedValue.ToString());
        //        DataTable dtCompany = ds.Tables[0];
        //        DataView dv1 = dtCompany.DefaultView;
        //        dv1.RowFilter = " Active = 'T' ";
        //        DataTable dtBR = dv1.ToTable();
        //        if (dtBR.Rows.Count > 0)
        //        {
        //            cbBranch.DisplayMember = "branch_name";
        //            cbBranch.ValueMember = "branch_Code";
        //            cbBranch.DataSource = dtBR;
        //        }
        //        dtCompany = null;
        //        dv1 = null;
        //        dtBR = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        objData = null;
        //        ds.Dispose();
        //    }
        //}
        public void GetGridBind()
        {
            try
            {
                objSQLDB = new SQLDB();

                string sqlQry = "";
                if (CommonData.LogUserId.ToUpper() == "ADMIN")
                {
                    sqlQry = "SELECT distinct CAST( SIH_ECODE as VARCHAR) +'-' +MEMBER_NAME MemberName, sih_company_code,SIH_FIN_YEAR,sih_branch_code BranchCode,branch_name BranchName,sih_indent_number IndentNo, sih_indent_Amount IndentAmount,sih_indent_date IndentDate" +
                                ",case when sih_indent_status='P' then 'Pending' when  sih_indent_status='A' then 'Approved'  when  sih_indent_status='V' then 'Verified'" +
                                " when  sih_indent_status='R' then 'Reject' when  sih_indent_status='D' then 'Dispatched' else '' end Status,SIH_INDENT_TYPE IndentType ,SDN_DC_DATE" +
                                " FROM STATIONARY_INDENT_HEAD A  INNER JOIN BRANCH_MAS B ON " +
                                "A.SIH_BRANCH_CODE=B.BRANCH_CODE   LEFT JOIN STATIONARY_DELIVERY_NOTE ON SDN_REF_INDENT_NO=SIH_INDENT_NUMBER " +
                                "AND SDN_APPROVAL_REF_NO = SIH_APPROVAL_REF_NO " +
                                " AND SDN_COMPANY_CODE=SIH_COMPANY_CODE AND SDN_TO_BRANCH_CODE=SIH_BRANCH_CODE  AND SDN_FIN_YEAR=SIH_FIN_YEAR" +
                                  " LEFT JOIN EORA_MASTER ON ECODE=SIH_ECODE ";

                }
                else if (strForm == "SELF")
                {
                    sqlQry = "SELECT distinct CAST( SIH_ECODE as VARCHAR) +'-' + MEMBER_NAME MemberName, sih_company_code,SIH_FIN_YEAR,sih_branch_code BranchCode,branch_name BranchName,sih_indent_number IndentNo, sih_indent_Amount IndentAmount,sih_indent_date IndentDate" +
                                ",case when sih_indent_status='P' then 'Pending' when  sih_indent_status='A' then 'Approved'  when  sih_indent_status='V' then 'Verified'" +
                                " when  sih_indent_status='R' then 'Reject' when  sih_indent_status='D' then 'Dispatched' else '' end Status,SIH_INDENT_TYPE IndentType ,SDN_DC_DATE" +
                                " FROM STATIONARY_INDENT_HEAD A  INNER JOIN BRANCH_MAS B ON " +
                                "A.SIH_BRANCH_CODE=B.BRANCH_CODE INNER JOIN USER_BRANCH on UB_BRANCH_CODE = SIH_BRANCH_CODE " +
                               " LEFT JOIN STATIONARY_DELIVERY_NOTE ON SDN_REF_INDENT_NO=SIH_INDENT_NUMBER " +
                                "AND SDN_APPROVAL_REF_NO = SIH_APPROVAL_REF_NO " +
                                " AND SDN_COMPANY_CODE=SIH_COMPANY_CODE AND SDN_TO_BRANCH_CODE=SIH_BRANCH_CODE  AND SDN_FIN_YEAR=SIH_FIN_YEAR" +
                                  " LEFT JOIN EORA_MASTER ON ECODE=SIH_ECODE " +

                                " WHERE UB_USER_ID = '" + CommonData.LogUserId + "' AND SIH_INDENT_TYPE='SELF' ORDER BY BranchName,IndentNo";
                }
                else if (strForm == "SHEAD")
                {
                    sqlQry = "SELECT distinct CAST( SIH_ECODE as VARCHAR) +'-' + MEMBER_NAME MemberName, sih_company_code,SIH_FIN_YEAR,sih_branch_code BranchCode,branch_name BranchName,sih_indent_number IndentNo, sih_indent_Amount IndentAmount,sih_indent_date IndentDate" +
                            ",case when sih_indent_status='P' then 'Pending' when  sih_indent_status='A' then 'Approved'  when  sih_indent_status='V' then 'Verified'" +
                            " when  sih_indent_status='R' then 'Reject' when  sih_indent_status='D' then 'Dispatched' else '' end Status,SIH_INDENT_TYPE IndentType ,SDN_DC_DATE" +
                            " FROM STATIONARY_INDENT_HEAD A  INNER JOIN BRANCH_MAS B ON " +
                            "A.SIH_BRANCH_CODE=B.BRANCH_CODE INNER JOIN USER_BRANCH on UB_BRANCH_CODE = SIH_BRANCH_CODE " +
                           "LEFT JOIN STATIONARY_DELIVERY_NOTE ON SDN_REF_INDENT_NO=SIH_INDENT_NUMBER " +
                                "AND SDN_APPROVAL_REF_NO = SIH_APPROVAL_REF_NO " +
                                " AND SDN_COMPANY_CODE=SIH_COMPANY_CODE AND SDN_TO_BRANCH_CODE=SIH_BRANCH_CODE  AND SDN_FIN_YEAR=SIH_FIN_YEAR" +
                                  " LEFT JOIN EORA_MASTER ON ECODE=SIH_ECODE " +
                            " WHERE SIH_INDENT_APPROVED_BY_HEAD = '" + CommonData.LogUserId + "' AND SIH_INDENT_TYPE='SELF' ORDER BY BranchName,IndentNo";
                }
                else
                {
                    sqlQry = "SELECT distinct CAST( SIH_ECODE as VARCHAR) +'-' + MEMBER_NAME MemberName, sih_company_code,SIH_FIN_YEAR,sih_branch_code BranchCode,branch_name BranchName,sih_indent_number IndentNo, sih_indent_Amount IndentAmount,sih_indent_date IndentDate" +
                                ",case when sih_indent_status='P' then 'Pending' when  sih_indent_status='A' then 'Approved'  when  sih_indent_status='V' then 'Verified'" +
                                " when  sih_indent_status='R' then 'Reject' when  sih_indent_status='D' then 'Dispatched' else '' end Status,SIH_INDENT_TYPE IndentType ,SDN_DC_DATE" +
                                " FROM STATIONARY_INDENT_HEAD A  INNER JOIN BRANCH_MAS B ON " +
                                "A.SIH_BRANCH_CODE=B.BRANCH_CODE INNER JOIN USER_BRANCH on UB_BRANCH_CODE = SIH_BRANCH_CODE " +
                               " LEFT JOIN STATIONARY_DELIVERY_NOTE ON SDN_REF_INDENT_NO=SIH_INDENT_NUMBER " +
                                "AND SDN_APPROVAL_REF_NO = SIH_APPROVAL_REF_NO " +
                                " AND SDN_COMPANY_CODE=SIH_COMPANY_CODE AND SDN_TO_BRANCH_CODE=SIH_BRANCH_CODE  AND SDN_FIN_YEAR=SIH_FIN_YEAR " +
                                  " LEFT JOIN EORA_MASTER ON ECODE=SIH_ECODE " +
                                " WHERE UB_USER_ID = '" + CommonData.LogUserId + "' ORDER BY BranchName,IndentNo";

                }
                DataSet dsData = objSQLDB.ExecuteDataSet(sqlQry);

                gvIndentDetails.Rows.Clear();
                int intRow = 1;

                DataRow[] dr = dsData.Tables[0].Select("Status='" + cmbStatus.Text + "'");

                for (int i = 0; i < dr.Length; i++)
                {

                    DataGridViewRow tempRow = new DataGridViewRow();
                    DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                    cellSLNO.Value = intRow;
                    tempRow.Cells.Add(cellSLNO);

                    DataGridViewCell cellCmpCd = new DataGridViewTextBoxCell();
                    cellCmpCd.Value = dr[i]["sih_company_code"].ToString();
                    tempRow.Cells.Add(cellCmpCd);

                    DataGridViewCell cellFin = new DataGridViewTextBoxCell();
                    cellFin.Value = dr[i]["SIH_FIN_YEAR"].ToString();
                    tempRow.Cells.Add(cellFin);

                    DataGridViewCell cellItemID = new DataGridViewTextBoxCell();
                    cellItemID.Value = dr[i]["BranchCode"].ToString();
                    tempRow.Cells.Add(cellItemID);

                    DataGridViewCell cellItemName = new DataGridViewTextBoxCell();
                    cellItemName.Value = dr[i]["BranchName"].ToString();
                    tempRow.Cells.Add(cellItemName);

                    if (dr[i]["IndentType"].ToString() == "SELF")
                    {

                        DataGridViewCell cellItemSelfName = new DataGridViewTextBoxCell();
                        cellItemSelfName.Value = dr[i]["MemberName"].ToString();
                        tempRow.Cells.Add(cellItemSelfName);
                    }
                    else
                    {
                        DataGridViewCell cellItemSelfName = new DataGridViewTextBoxCell();
                        cellItemSelfName.Value = "";
                        tempRow.Cells.Add(cellItemSelfName);
                    }

                    DataGridViewCell cellAvailQty = new DataGridViewTextBoxCell();
                    cellAvailQty.Value = dr[i]["IndentNo"].ToString();
                    tempRow.Cells.Add(cellAvailQty);

                    DataGridViewCell cellReqQty = new DataGridViewTextBoxCell();
                    cellReqQty.Value = dr[i]["IndentAmount"].ToString();
                    tempRow.Cells.Add(cellReqQty);

                    DataGridViewCell cellApprQty = new DataGridViewTextBoxCell();
                    cellApprQty.Value = Convert.ToDateTime(dr[i]["IndentDate"]).ToString("dd/MMM/yyyy").ToUpper();
                    tempRow.Cells.Add(cellApprQty);

                    if (cmbStatus.Text == "DISPATCHED")
                    {
                        if (dr[i]["SDN_DC_DATE"].ToString().Trim() != "")
                        {
                            DataGridViewCell cellDiapstchDate = new DataGridViewTextBoxCell();
                            cellDiapstchDate.Value = Convert.ToDateTime(dr[i]["SDN_DC_DATE"]).ToString("dd/MMM/yyyy").ToUpper();
                            tempRow.Cells.Add(cellDiapstchDate);

                        }
                        else
                        {
                            DataGridViewCell cellDiapstchDate = new DataGridViewTextBoxCell();
                            cellDiapstchDate.Value = Convert.ToDateTime(dr[i]["IndentDate"]).ToString("dd/MMM/yyyy").ToUpper();
                            tempRow.Cells.Add(cellDiapstchDate);
                        }
                    }
                    else
                    {

                        DataGridViewCell cellDiapstchDate = new DataGridViewTextBoxCell();
                        cellDiapstchDate.Value = "";
                        tempRow.Cells.Add(cellDiapstchDate);
                    }

                    DataGridViewCell cellRate = new DataGridViewTextBoxCell();
                    cellRate.Value = dr[i]["Status"].ToString().ToUpper();
                    tempRow.Cells.Add(cellRate);

                    intRow++;
                    gvIndentDetails.Rows.Add(tempRow);
                }
                //if ((strForm == "MGR") || (strForm == "HEAD"))
                //    gvIndentDetails.Columns[9].Visible = true;
                //else
                //    gvIndentDetails.Columns[9].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLDB = null;

            }
        }

        //private void cbUserBranch_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cbUserBranch.SelectedValue != null)
        //    {
        //        string[] sval = cbUserBranch.SelectedValue.ToString().Split('@');
        //        GetGridBind(sval[0].ToString());
        //    }
        //}

        //private void cbBranch_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    if (cbBranch.SelectedValue != null)
        //        GetGridBind(cbBranch.SelectedValue.ToString());
        //}

        private void gvIndentDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == gvIndentDetails.Columns["lnkDetails"].Index)
                {
                    int indentNo = Convert.ToInt32(gvIndentDetails.Rows[e.RowIndex].Cells[gvIndentDetails.Columns["IndentNo"].Index].Value);
                    string sBranchCode = "", sCompanyCode = "";
                    sBranchCode = gvIndentDetails.Rows[e.RowIndex].Cells[gvIndentDetails.Columns["BranchCode"].Index].Value.ToString();
                    sCompanyCode = gvIndentDetails.Rows[e.RowIndex].Cells[gvIndentDetails.Columns["sih_company_code"].Index].Value.ToString();
                   
                    if (strForm == "BR" && cmbStatus.SelectedIndex == 0)
                    {
                        StationaryIndent frmStationaryIndent = new StationaryIndent(strForm, sCompanyCode, sBranchCode, indentNo);
                        frmStationaryIndent.chldStationaryIndentList = this;
                        frmStationaryIndent.ShowDialog();
                    }
                    if (strForm == "SELF" && cmbStatus.SelectedIndex == 0)
                    {
                        StationaryIndent frmStationaryIndent = new StationaryIndent(strForm, sCompanyCode, sBranchCode, indentNo);
                        frmStationaryIndent.chldStationaryIndentList = this;
                        frmStationaryIndent.ShowDialog();
                    }
                  
                    if (strForm == "MGR" && (cmbStatus.SelectedIndex == 0 || cmbStatus.SelectedIndex == 2 || cmbStatus.SelectedIndex == 3))
                    {
                        StationaryIndent frmStationaryIndent = new StationaryIndent(strForm, sCompanyCode, sBranchCode, indentNo);
                        frmStationaryIndent.chldStationaryIndentList = this;
                        frmStationaryIndent.ShowDialog();
                    }
                    if (strForm == "HEAD" && (cmbStatus.SelectedIndex == 0 || cmbStatus.SelectedIndex == 1 || cmbStatus.SelectedIndex == 2 || cmbStatus.SelectedIndex == 3))
                    {
                        StationaryIndent frmStationaryIndent = new StationaryIndent(strForm, sCompanyCode, sBranchCode, indentNo);
                        frmStationaryIndent.chldStationaryIndentList = this;
                        frmStationaryIndent.ShowDialog();
                    }
                    if (strForm == "SHEAD" && (cmbStatus.SelectedIndex == 0 || cmbStatus.SelectedIndex == 1 || cmbStatus.SelectedIndex == 2 || cmbStatus.SelectedIndex == 3))
                    {
                        StationaryIndent frmStationaryIndent = new StationaryIndent(strForm, sCompanyCode, sBranchCode, indentNo);
                        frmStationaryIndent.chldStationaryIndentList = this;
                        frmStationaryIndent.ShowDialog();
                    }
                }
                if (e.ColumnIndex == gvIndentDetails.Columns["ImgPrint"].Index)
                {
                    int indentNo = Convert.ToInt32(gvIndentDetails.Rows[e.RowIndex].Cells[gvIndentDetails.Columns["IndentNo"].Index].Value);
                    string CompanyCode = gvIndentDetails.Rows[e.RowIndex].Cells[gvIndentDetails.Columns["sih_company_code"].Index].Value.ToString();
                    string BranchCode = gvIndentDetails.Rows[e.RowIndex].Cells[gvIndentDetails.Columns["BranchCode"].Index].Value.ToString();
                    string Finyear = gvIndentDetails.Rows[e.RowIndex].Cells[gvIndentDetails.Columns["SIH_FIN_YEAR"].Index].Value.ToString();
                    //string Ecod = gvIndentDetails.Rows[e.RowIndex].Cells[gvIndentDetails.Columns["MemberName"].Index].Value.ToString();
                    
                        CommonData.ViewReport = "STATIONARYINDENT";
                        ReportViewer oReportViewer = new ReportViewer(CompanyCode, BranchCode, Finyear, indentNo);
                        oReportViewer.Show();
                    
                   
                }
            }
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (CommonData.LogUserId.ToUpper() == "ADMIN")
            //{
            //    if (cbBranch.SelectedValue != null)
            //        GetGridBind(cbBranch.SelectedValue.ToString());
            //}
            //else if (cbUserBranch.SelectedValue != null)
            //{
            //    string[] sval = cbUserBranch.SelectedValue.ToString().Split('@');
            //    GetGridBind(sval[0].ToString());
            //}
            GetGridBind();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
