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
using SSTrans;
using Excel = Microsoft.Office.Interop.Excel;
namespace SSCRM
{
    public partial class StationaryBrochureList : Form
    {
        SQLDB objSQLDB = null;
        UtilityDB objUtilityDB = null;
        ExcelDB objExDb = null;
        Security objSecurity = null;
        private string strForm = "";
        public StationaryBrochureList()
        {
            InitializeComponent();
        }

        public StationaryBrochureList(string sfrom)
        {
            InitializeComponent();
            strForm = sfrom;
        }
        private void StationaryIndentList_Load(object sender, EventArgs e)
        {
            cmbStatus.SelectedIndex = 0;
            GetGridBind();
            gvIndentDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);
            dtpFromDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            dtpToDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
        }

        public void GetGridBind()
        {
            try
            {
                objSQLDB = new SQLDB();
                string sqlQry = " SELECT CAST( SIH_ECODE as VARCHAR) +'-' + MEMBER_NAME MemberName,sih_company_code,SIH_STATE_CODE,SIH_FIN_YEAR,sih_branch_code BranchCode,branch_name BranchName,sih_indent_number IndentNo, sih_indent_Amount IndentAmount,sih_indent_date IndentDate," +
                " case when sih_indent_status='P' then 'PENDING' when  sih_indent_status='A' then 'APPROVED'  when  sih_indent_status='V' then 'VERIFIED'" +
                " when  sih_indent_status='R' then 'REJECT' else 'DISPATCHED' end Status,SIH_INDENT_TYPE IndentType FROM STATIONARY_INDENT_HEAD A  INNER JOIN BRANCH_MAS B ON " +
                " A.SIH_BRANCH_CODE=B.BRANCH_CODE LEFT JOIN EORA_MASTER ON ECODE=SIH_ECODE";
                if (cmbStatus.SelectedIndex == 0)
                {
                    sqlQry = "EXEC GetBroucherListForStationaryDispatch 'APPROVED','" + dtpFromDate.Value.ToString("dd/MMM/yyyy") + "','" + dtpToDate.Value.ToString("dd/MMM/yyyy") + "','GRID'";
                }
                else if (cmbStatus.SelectedIndex == 1)
                {
                    sqlQry = "EXEC GetBroucherListForStationaryDispatch 'DISPATCHED','" + dtpFromDate.Value.ToString("dd/MMM/yyyy") + "','" + dtpToDate.Value.ToString("dd/MMM/yyyy") + "','GRID'";
                }
                else if (cmbStatus.SelectedIndex == 2)
                {
                    sqlQry = "EXEC GetBroucherListForStationaryDispatch 'DELIVERED','" + dtpFromDate.Value.ToString("dd/MMM/yyyy") + "','" + dtpToDate.Value.ToString("dd/MMM/yyyy") + "','GRID'";
                }
                DataSet dsData = objSQLDB.ExecuteDataSet(sqlQry);
                gvIndentDetails.Rows.Clear();
                DataRow[] dr = dsData.Tables[0].Select("Status='" + cmbStatus.Text + "'");
                int intRow = 1;
                for (int i = 0; i < dr.Length; i++)
                {
                    DataGridViewRow tempRow = new DataGridViewRow();
                    DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                    cellSLNO.Value = intRow;
                    tempRow.Cells.Add(cellSLNO);

                    DataGridViewCell cellCmpCd = new DataGridViewTextBoxCell();
                    cellCmpCd.Value = dr[i]["sih_company_code"].ToString();
                    tempRow.Cells.Add(cellCmpCd);

                    DataGridViewCell cellState = new DataGridViewTextBoxCell();
                    cellState.Value = dr[i]["SIH_STATE_CODE"].ToString();
                    tempRow.Cells.Add(cellState);

                    DataGridViewCell cellFin = new DataGridViewTextBoxCell();
                    cellFin.Value = dr[i]["SIH_FIN_YEAR"].ToString();
                    tempRow.Cells.Add(cellFin);

                    DataGridViewCell cellItemID = new DataGridViewTextBoxCell();
                    cellItemID.Value = dr[i]["BranchCode"].ToString();
                    tempRow.Cells.Add(cellItemID);
                    
                    DataGridViewCell cellItemName = new DataGridViewTextBoxCell();
                    cellItemName.Value = dr[i]["BranchName"].ToString();
                    tempRow.Cells.Add(cellItemName);
                    if (dr[i]["MemberName"].ToString() != "")
                    {

                        DataGridViewCell cellItemSelfName = new DataGridViewTextBoxCell();
                        cellItemSelfName.Value = dr[i]["MemberName"].ToString();
                        tempRow.Cells.Add(cellItemSelfName);
                    }
                    else
                    {
                        DataGridViewCell cellItemSelfName = new DataGridViewTextBoxCell();
                        cellItemSelfName.Value = dr[i]["MemberName"].ToString();
                        tempRow.Cells.Add(cellItemSelfName);
                    }

                    DataGridViewCell cellAvailQty = new DataGridViewTextBoxCell();
                    cellAvailQty.Value = dr[i]["IndentNo"].ToString();
                    tempRow.Cells.Add(cellAvailQty);
                   

                    DataGridViewCell cellReqQty = new DataGridViewTextBoxCell();
                    cellReqQty.Value = dr[i]["IndentAmount"].ToString();
                    tempRow.Cells.Add(cellReqQty);

                    DataGridViewCell cellApprDate = new DataGridViewTextBoxCell();
                    cellApprDate.Value = Convert.ToDateTime(dr[i]["ApprovedDate"]).ToString("dd/MMM/yyyy");
                    tempRow.Cells.Add(cellApprDate);

                    DataGridViewCell cellDispDate = new DataGridViewTextBoxCell();
                    if (dr[i]["DispatchDate"].ToString().Trim() != "")
                        cellDispDate.Value = Convert.ToDateTime(dr[i]["DispatchDate"]).ToString("dd/MMM/yyyy");
                    else
                        cellDispDate.Value = "";
                    tempRow.Cells.Add(cellDispDate);

                    DataGridViewCell cellDelDate = new DataGridViewTextBoxCell();
                    if (dr[i]["DeliveredDate"].ToString().Trim() != "")
                        cellDelDate.Value = Convert.ToDateTime(dr[i]["DeliveredDate"]).ToString("dd/MMM/yyyy");
                    else
                        cellDelDate.Value = "";
                    tempRow.Cells.Add(cellDelDate);

                    DataGridViewCell cellRate = new DataGridViewTextBoxCell();
                    cellRate.Value = dr[i]["Status"].ToString();
                    tempRow.Cells.Add(cellRate);

                    intRow++;
                    gvIndentDetails.Rows.Add(tempRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void gvIndentDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >=0)
            {
                if (e.ColumnIndex == gvIndentDetails.Columns["lnkDetails"].Index)
                {
                    int indentNo = Convert.ToInt32(gvIndentDetails.Rows[e.RowIndex].Cells[gvIndentDetails.Columns["IndentNo"].Index].Value);
                    string CompanyCode = gvIndentDetails.Rows[e.RowIndex].Cells[gvIndentDetails.Columns["sih_company_code"].Index].Value.ToString();
                    string BranchCode = gvIndentDetails.Rows[e.RowIndex].Cells[gvIndentDetails.Columns["BranchCode"].Index].Value.ToString();
                    string Ecode =  gvIndentDetails.Rows[e.RowIndex].Cells[gvIndentDetails.Columns["MemberName"].Index].Value.ToString();
                    string StateCode = gvIndentDetails.Rows[e.RowIndex].Cells[gvIndentDetails.Columns["SIH_STATE_CODE"].Index].Value.ToString();
                    string Finyear = gvIndentDetails.Rows[e.RowIndex].Cells[gvIndentDetails.Columns["SIH_FIN_YEAR"].Index].Value.ToString();
                    
                   
                    StationaryBrocher frmSStationaryBrocher = new StationaryBrocher(strForm, CompanyCode, BranchCode,Ecode, StateCode,Finyear, indentNo);
                    frmSStationaryBrocher.chldStationaryBrochureList = this;
                    frmSStationaryBrocher.ShowDialog();
                }
                if (e.ColumnIndex == gvIndentDetails.Columns["ImgPrint"].Index)
                {
                    int indentNo = Convert.ToInt32(gvIndentDetails.Rows[e.RowIndex].Cells[gvIndentDetails.Columns["IndentNo"].Index].Value);
                    string CompanyCode = gvIndentDetails.Rows[e.RowIndex].Cells[gvIndentDetails.Columns["sih_company_code"].Index].Value.ToString();
                    string BranchCode = gvIndentDetails.Rows[e.RowIndex].Cells[gvIndentDetails.Columns["BranchCode"].Index].Value.ToString();
                    string Finyear = gvIndentDetails.Rows[e.RowIndex].Cells[gvIndentDetails.Columns["SIH_FIN_YEAR"].Index].Value.ToString();
                    if (cmbStatus.SelectedIndex == 0)
                    {
                        CommonData.ViewReport = "STATIONARYINDENT_FOR_DC";
                        ReportViewer oReportViewer = new ReportViewer(CompanyCode, BranchCode, Finyear, indentNo);
                        oReportViewer.Show();
                    }
                    else
                    {
                        CommonData.ViewReport = "STATIONARYDISPATCH";
                        ReportViewer oReportViewer = new ReportViewer(CompanyCode, BranchCode, Finyear, indentNo);
                        oReportViewer.Show();
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetGridBind();
        }

        private void gvIndentDetails_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                objSQLDB = new SQLDB();
                string sqlQry = " SELECT distinct CAST( SIH_ECODE as VARCHAR) +'-' + MEMBER_NAME MemberName, sih_company_code,SIH_STATE_CODE,SIH_FIN_YEAR,sih_branch_code BranchCode,branch_name BranchName,sih_indent_number IndentNo, sih_indent_Amount IndentAmount,sih_indent_date IndentDate," +
                " case when sih_indent_status='P' then 'PENDING' when  sih_indent_status='A' then 'APPROVED'  when  sih_indent_status='V' then 'VERIFIED'" +
                " when  sih_indent_status='R' then 'REJECT' else 'DISPATCHED' end Status,SIH_INDENT_TYPE IndentType FROM STATIONARY_INDENT_HEAD A  INNER JOIN BRANCH_MAS B ON " +
                " A.SIH_BRANCH_CODE=B.BRANCH_CODE LEFT JOIN EORA_MASTER ON ECODE=SIH_ECODE";
                if (cmbStatus.SelectedIndex == 0)
                {
                    sqlQry = "EXEC GetBroucherListForStationaryDispatch 'APPROVED','" + dtpFromDate.Value.ToString("dd/MMM/yyyy") + "','" + dtpToDate.Value.ToString("dd/MMM/yyyy") + "','GRID'";
                }
                else if (cmbStatus.SelectedIndex == 1)
                {
                    sqlQry = "EXEC GetBroucherListForStationaryDispatch 'DISPATCHED','" + dtpFromDate.Value.ToString("dd/MMM/yyyy") + "','" + dtpToDate.Value.ToString("dd/MMM/yyyy") + "','GRID'";
                }
                else if (cmbStatus.SelectedIndex == 2)
                {
                    sqlQry = "EXEC GetBroucherListForStationaryDispatch 'DELIVERED','" + dtpFromDate.Value.ToString("dd/MMM/yyyy") + "','" + dtpToDate.Value.ToString("dd/MMM/yyyy") + "','GRID'";
                }
                DataSet dsData = objSQLDB.ExecuteDataSet(sqlQry);
                gvIndentDetails.Rows.Clear();
                DataRow[] dr = dsData.Tables[0].Select("Status='" + cmbStatus.Text + "'");
                int intRow = 1;
                for (int i = 0; i < dr.Length; i++)
                {
                    DataGridViewRow tempRow = new DataGridViewRow();
                    DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                    cellSLNO.Value = intRow;
                    tempRow.Cells.Add(cellSLNO);

                    DataGridViewCell cellCmpCd = new DataGridViewTextBoxCell();
                    cellCmpCd.Value = dr[i]["sih_company_code"].ToString();
                    tempRow.Cells.Add(cellCmpCd);

                    DataGridViewCell cellState = new DataGridViewTextBoxCell();
                    cellState.Value = dr[i]["SIH_STATE_CODE"].ToString();
                    tempRow.Cells.Add(cellState);

                    DataGridViewCell cellFin = new DataGridViewTextBoxCell();
                    cellFin.Value = dr[i]["SIH_FIN_YEAR"].ToString();
                    tempRow.Cells.Add(cellFin);

                    DataGridViewCell cellItemID = new DataGridViewTextBoxCell();
                    cellItemID.Value = dr[i]["BranchCode"].ToString();
                    tempRow.Cells.Add(cellItemID);

                    DataGridViewCell cellItemName = new DataGridViewTextBoxCell();
                    cellItemName.Value = dr[i]["BranchName"].ToString();
                    tempRow.Cells.Add(cellItemName);
                    if (dr[i]["MemberName"].ToString() != "")
                    {

                        DataGridViewCell cellItemSelfName = new DataGridViewTextBoxCell();
                        cellItemSelfName.Value = dr[i]["MemberName"].ToString();
                        tempRow.Cells.Add(cellItemSelfName);
                    }
                   else
                    {

                        DataGridViewCell cellItemSelfName = new DataGridViewTextBoxCell();
                        cellItemSelfName.Value = "Branch Indent";
                        tempRow.Cells.Add(cellItemSelfName);
                    }              
                    DataGridViewCell cellAvailQty = new DataGridViewTextBoxCell();
                    cellAvailQty.Value = dr[i]["IndentNo"].ToString();
                    tempRow.Cells.Add(cellAvailQty);

                    DataGridViewCell cellReqQty = new DataGridViewTextBoxCell();
                    cellReqQty.Value = dr[i]["IndentAmount"].ToString();
                    tempRow.Cells.Add(cellReqQty);

                    DataGridViewCell cellApprDate = new DataGridViewTextBoxCell();
                    cellApprDate.Value = Convert.ToDateTime(dr[i]["ApprovedDate"]).ToString("dd/MMM/yyyy");
                    tempRow.Cells.Add(cellApprDate);

                    DataGridViewCell cellDispDate = new DataGridViewTextBoxCell();
                    if (dr[i]["DispatchDate"].ToString().Trim() != "")
                        cellDispDate.Value = Convert.ToDateTime(dr[i]["DispatchDate"]).ToString("dd/MMM/yyyy");
                    else
                        cellDispDate.Value = "";
                    tempRow.Cells.Add(cellDispDate);

                    DataGridViewCell cellDelDate = new DataGridViewTextBoxCell();
                    if (dr[i]["DeliveredDate"].ToString().Trim() != "")
                        cellDelDate.Value = Convert.ToDateTime(dr[i]["DeliveredDate"]).ToString("dd/MMM/yyyy");
                    else
                        cellDelDate.Value = "";
                    tempRow.Cells.Add(cellDelDate);

                    DataGridViewCell cellRate = new DataGridViewTextBoxCell();
                    cellRate.Value = dr[i]["Status"].ToString();
                    tempRow.Cells.Add(cellRate);

                    intRow++;
                    gvIndentDetails.Rows.Add(tempRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            
            objSQLDB = new SQLDB();
            string sqlQry = "";
            DataSet dsData = new DataSet();
            if (cmbStatus.SelectedIndex == 0)
            {
                sqlQry = "EXEC GetBroucherListForStationaryDispatch 'APPROVED','" + dtpFromDate.Value.ToString("dd/MMM/yyyy") + "','" + dtpToDate.Value.ToString("dd/MMM/yyyy") + "','EXCEL'";
            }
            else if (cmbStatus.SelectedIndex == 1)
            {
                sqlQry = "EXEC GetBroucherListForStationaryDispatch 'DISPATCHED','" + dtpFromDate.Value.ToString("dd/MMM/yyyy") + "','" + dtpToDate.Value.ToString("dd/MMM/yyyy") + "','EXCEL'";
            }
            else if (cmbStatus.SelectedIndex == 2)
            {
                sqlQry = "EXEC GetBroucherListForStationaryDispatch 'DELIVERED','" + dtpFromDate.Value.ToString("dd/MMM/yyyy") + "','" + dtpToDate.Value.ToString("dd/MMM/yyyy") + "','EXCEL'";
            }
            try
            {
                dsData = objSQLDB.ExecuteDataSet(sqlQry);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            try
            {
                
                objUtilityDB = new UtilityDB();
                DataTable dtExcel = new DataTable();
                Excel.Application oXL = new Excel.Application();

                dtExcel = dsData.Tables[0];
                Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                oXL.Visible = true;
                string sLastColumn = objUtilityDB.GetColumnName(21);
                Excel.Range rg = worksheet.get_Range("A3", sLastColumn + "3");
                Excel.Range rgData = worksheet.get_Range("A3", sLastColumn + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString());
                rgData.Font.Size = 11;
                rgData.WrapText = true;
                rgData.VerticalAlignment = Excel.Constants.xlCenter;
                rgData.Borders.Weight = 2;



                rg.Font.Bold = true;
                //rg.Font.Name = "Times New Roman";
                rg.Font.Size = 10;
                rg.WrapText = true;
                rg.Font.ColorIndex = 2; // White Color : 2 and Red = 3,30; Green = 10,43; 
                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                rg.Interior.ColorIndex = 31;
                rg.Borders.Weight = 2;
                rg.Borders.LineStyle = Excel.Constants.xlSolid;
                rg.Cells.RowHeight = 38;

                rg = worksheet.get_Range("A3", Type.Missing);
                rg.Cells.ColumnWidth = 4;

                rg = worksheet.get_Range("B3", "C3");
                rg.Cells.ColumnWidth = 50;
                rg = worksheet.get_Range("D3", Type.Missing);
                rg.Cells.ColumnWidth = 40;
                rg = worksheet.get_Range("E3", Type.Missing);
                rg.Cells.ColumnWidth = 10;
                rg = worksheet.get_Range("I3", "M3");
                rg.Cells.ColumnWidth = 13;

                rg = worksheet.get_Range("N3", Type.Missing);
                rg.Cells.ColumnWidth = 5;

                rg = worksheet.get_Range("O3", "P3");
                rg.Cells.ColumnWidth = 40;

                Excel.Range rgHead = null;
                rgHead = worksheet.get_Range("A1", "I2");
                rgHead.Merge(Type.Missing);
                rgHead.Font.Size = 14;
                rgHead.Font.ColorIndex = 1;
                rgHead.Font.Bold = true;
                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;

                rgHead.Cells.Value2 = "STATIONARY INDENT STATUS REGISTER FROM " + dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper() + 
                                            " TO " + dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper() + "";

                int iColumn = 1;
                worksheet.Cells[3, iColumn++] = "SlNo";
                worksheet.Cells[3, iColumn++] = "Company";
                worksheet.Cells[3, iColumn++] = "Branch";
                worksheet.Cells[3, iColumn++] = "Self";
                worksheet.Cells[3, iColumn++] = "FinYear";
                worksheet.Cells[3, iColumn++] = "Indent NO";
                worksheet.Cells[3, iColumn++] = "Dispatch No";
                worksheet.Cells[3, iColumn++] = "GRNNo at Branch";
                worksheet.Cells[3, iColumn++] = "Indent Date";
                worksheet.Cells[3, iColumn++] = "Indent Status";
                worksheet.Cells[3, iColumn++] = "Approved Date";
                worksheet.Cells[3, iColumn++] = "Dispatch Date";
                worksheet.Cells[3, iColumn++] = "Delivered Date";
                worksheet.Cells[3, iColumn++] = "Item SlNo";
                worksheet.Cells[3, iColumn++] = "Item Name";
                worksheet.Cells[3, iColumn++] = "Item Category";
                worksheet.Cells[3, iColumn++] = "Available Qty";
                worksheet.Cells[3, iColumn++] = "Request Qty";
                worksheet.Cells[3, iColumn++] = "Approved Qty";
                worksheet.Cells[3, iColumn++] = "Dispatch Qty";
                worksheet.Cells[3, iColumn++] = "Delivered Qty";

                int iRow = 4; iColumn = 1;
                for (int i = 0; i < dtExcel.Rows.Count; i++)
                {
                    worksheet.Cells[iRow, iColumn++] = i + 1;
                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["st_comp_name"];
                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["st_branch_name"];
                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["st_member_name"];
                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["st_fin_year"];
                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["st_indent_no"];
                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["st_disp_no"];
                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["st_grn_no"];
                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["st_indent_date"];
                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["st_indent_status"];
                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["st_approved_date"];
                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["st_disp_date"];
                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["st_delivered_date"];                    
                    worksheet.Cells[iRow, iColumn++] = "";
                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["st_item_name"];
                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["st_category"];
                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["st_available_qty"];
                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["st_req_qty"];
                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["st_ho_qty"];
                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["st_disp_qty"];
                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["st_delv_qty"];
                    iColumn = 1;
                    iRow++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
