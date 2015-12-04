using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SSCRMDB;
using SSAdmin;
using SSTrans;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office;
using System.Xml;


namespace SSCRM
{
    public partial class BranchReportFilter : Form
    {
        ReportViewer childReportViewer;
        SQLDB objSQLdb = null;
        private string Company = string.Empty;
        private string State = string.Empty;
        private string Branches = string.Empty;
         UtilityDB objUtilityDB = null;
        ExcelDB objExDb = null;
        private int iFrmType = 0;

        public BranchReportFilter()
        {
            InitializeComponent();
        }
        public BranchReportFilter(int iForm)
        {
            iFrmType = iForm;
            InitializeComponent();
        } 

        private void BranchReportFilter_Load(object sender, EventArgs e)
        {
            if (iFrmType == 1)
            {
                lblDocm.Text = "DOC MONTH";
                dtpToDoc.Visible = false;
                label2.Visible = false;
                txtNoofRecords.Visible = false;
                label3.Visible = false;
                label6.Visible = false;
                cbDesig.Visible = false;
                cbRepType.Visible = false;
                //btnDownload.Visible = false;
                lblEcode.Visible = false;
                FillBranches();
            }
            else
            {
                FillReportType();
                FillBranches();
                dtpFromDoc.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.DocMonth).ToString("MMMyyyy").ToUpper());
                dtpToDoc.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.DocMonth).ToString("MMMyyyy").ToUpper());
            }

        }
        private void FillReportType()
        {
            try
            {
                cbRepType.DataSource = dtReportType();
                cbRepType.DisplayMember = "name";
                cbRepType.ValueMember = "type";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FillDesigComboBox()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbDesig.DataSource = null;
            try
            {
                if (cbRepType.SelectedIndex > 0)
                {
                    dt = Get_AllIndiaFilter(cbRepType.SelectedValue.ToString()).Tables[0];


                    if (dt.Rows.Count > 0)
                    {
                        //DataRow dr = dt.NewRow();
                        //dr[0] = 0;
                        //dr[1] = "--Select--";

                        //dt.Rows.InsertAt(dr, 0);

                        cbDesig.DataSource = dt;
                        cbDesig.DisplayMember = "DisplayMember";
                        cbDesig.ValueMember = "ValueMember";
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
                dt = null;

            }
        }

     
        #region Report Type Table
        public  DataTable dtReportType()
        {
            DataTable table = new DataTable();
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("name", typeof(string));

            table.Rows.Add("--SELECT--", "--SELECT--");
            table.Rows.Add("SR", "SR");
            table.Rows.Add("GL", "GL");
            //table.Rows.Add("GC", "GC");
            table.Rows.Add("TM & ABOVE", "TM & ABOVE"); 

            return table;
        }
        #endregion
        private DataSet Get_UserBranchStateFilterCursor(string sCompCode, string sStateCode,string sLogUserId, string sBranchtType, string sGetType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompany", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sStateCode", DbType.String, sStateCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sUser", DbType.String, sLogUserId, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sBranchType", DbType.String, sBranchtType, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sType", DbType.String, sGetType, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("Get_UserBranchStateFilterCursor", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }
        private DataSet Get_AllIndiaFilter(string sReportType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xRepType", DbType.String, sReportType, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SSCRM_REP_ALL_INDIA_FILTER", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }
        private void FillBranches()
        {
            tvBranches.Nodes.Clear();
            objSQLdb = new SQLDB();
            DataSet ds = new DataSet();
            try
            {
                ds = Get_UserBranchStateFilterCursor("", "",CommonData.LogUserId,"","PARENT");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        tvBranches.Nodes.Add(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), ds.Tables[0].Rows[i]["COMPANY_NAME"].ToString());

                        DataSet dsState = new DataSet();
                        if (iFrmType == 1)
                        {
                            dsState = Get_UserBranchStateFilterCursor(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "", CommonData.LogUserId, "", "SPSTATE");
                        }
                        else
                        {
                            dsState = Get_UserBranchStateFilterCursor(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "", CommonData.LogUserId, "", "STATE");
                        }

                        if (dsState.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < dsState.Tables[0].Rows.Count; j++)
                            {
                                tvBranches.Nodes[i].Nodes.Add(dsState.Tables[0].Rows[j]["StateCode"].ToString(), dsState.Tables[0].Rows[j]["StateName"].ToString());

                                DataSet dschild = new DataSet();
                                if (iFrmType == 1)
                                {
                                    dschild = Get_UserBranchStateFilterCursor(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), dsState.Tables[0].Rows[j]["StateCode"].ToString(), CommonData.LogUserId, "SP", "SPCHILD");
                                }
                                else
                                {
                                    dschild = Get_UserBranchStateFilterCursor(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), dsState.Tables[0].Rows[j]["StateCode"].ToString(), CommonData.LogUserId, "BR", "CHILD");
                                }
                                if (dschild.Tables[0].Rows.Count > 0)
                                {
                                    for (int k = 0; k < dschild.Tables[0].Rows.Count; k++)
                                    {

                                        tvBranches.Nodes[i].Nodes[j].Nodes.Add(dschild.Tables[0].Rows[k]["BranCode"].ToString(), dschild.Tables[0].Rows[k]["BranchName"].ToString());
                                    }
                                }


                            }
                        }


                    }

                }

                for (int ivar = 0; ivar < tvBranches.Nodes.Count; ivar++)
                {
                    for (int j = 0; j < tvBranches.Nodes[ivar].Nodes.Count; j++)
                    {
                        if (tvBranches.Nodes[ivar].Nodes[j].Nodes.Count > 0)
                            tvBranches.Nodes[ivar].FirstNode.Expand();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private bool CheckData()
        {
            bool flag = false;
            if (iFrmType == 1)
            {
                for (int i = 0; i < tvBranches.Nodes.Count; i++)
                {
                    for (int j = 0; j < tvBranches.Nodes[i].Nodes.Count; j++)
                    {
                        for (int k = 0; k < tvBranches.Nodes[i].Nodes[j].Nodes.Count; k++)
                        {

                            if (tvBranches.Nodes[i].Nodes[j].Nodes[k].Checked == true)
                            {
                                flag = true;
                            }
                        }
                    }

                }
            }
            else
            {
                for (int i = 0; i < tvBranches.Nodes.Count; i++)
                {
                    for (int j = 0; j < tvBranches.Nodes[i].Nodes.Count; j++)
                    {
                        for (int k = 0; k < tvBranches.Nodes[i].Nodes[j].Nodes.Count; k++)
                        {

                            if (tvBranches.Nodes[i].Nodes[j].Nodes[k].Checked == true)
                            {
                                flag = true;
                            }
                        }
                    }

                }

                if (flag == false)
                {
                    MessageBox.Show("Please Select Atleast One Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return flag;
                }
                if (txtNoofRecords.Text.Length == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Enter No Of Records to Display", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNoofRecords.Focus();
                    return flag;
                }
                //if (cbDesig.SelectedIndex == 0)
                //{
                //    flag = false;
                //    MessageBox.Show("Please Select Designation", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    cbDesig.Focus();
                //    return flag;
                //}
                if (cbRepType.SelectedIndex == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Select Report Type", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbRepType.Focus();
                    return flag;
                }
            }

            return flag;
        }
        
       
        private void GetSelectedCompAndBranches()
        {
            Company = "";
            Branches = "";
            State = "";

            bool iscomp = false;
            bool iSstate=false;
            for (int i = 0; i < tvBranches.Nodes.Count; i++)
            {
                for (int j = 0; j < tvBranches.Nodes[i].Nodes.Count; j++)
                {
                    for (int k = 0; k < tvBranches.Nodes[i].Nodes[j].Nodes.Count; k++)
                    {
                        
                        if (tvBranches.Nodes[i].Nodes[j].Nodes[k].Checked == true)
                        {
                            if (Branches != string.Empty)
                                Branches += ",";
                            Branches += tvBranches.Nodes[i].Nodes[j].Nodes[k].Name.ToString();
                            iscomp = true;
                            iSstate = true;
                        }
                       
                    }
                    if (iSstate == true)
                    {
                        if (State != string.Empty)
                            State += ",";
                        State += tvBranches.Nodes[i].Nodes[j].Name.ToString();
                    }
                    iSstate = false;  
                }
              
                if (iscomp == true)
                {
                    if (Company != string.Empty)
                        Company += ",";
                    Company += tvBranches.Nodes[i].Name.ToString();
                }
                iscomp = false;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbRepType.SelectedIndex = 0;
            tvBranches.Nodes.Clear();
            dtpFromDoc.Value = DateTime.Now;
            dtpToDoc.Value = DateTime.Now;
            txtNoofRecords.Text = "";
            cbDesig.SelectedIndex = -1;

            FillBranches();
        }



        private void btnReport_Click(object sender, EventArgs e)
        {

            if (CheckData() == true)
            {
                GetSelectedCompAndBranches();              
              
                if (cbDesig.Text =="SR")
                {
                    CommonData.ViewReport = "SSCRM_REP_ALL_INDIA_TOP_SRS";
                    childReportViewer = new ReportViewer(Company, Branches, txtNoofRecords.Text.ToString(), dtpFromDoc.Value.ToString("MMMyyyy").ToUpper(), dtpToDoc.Value.ToString("MMMyyyy").ToUpper(), "");
                    childReportViewer.Show();
                }
                if (cbDesig.Text == "GL" || (cbDesig.Text == "GC"))
                {
                    CommonData.ViewReport = "SSCRM_REP_ALL_INDIA_TOP_GL";
                    childReportViewer = new ReportViewer(Company, Branches, txtNoofRecords.Text.ToString(), dtpFromDoc.Value.ToString("MMMyyyy").ToUpper(), dtpToDoc.Value.ToString("MMMyyyy").ToUpper(), "");
                    childReportViewer.Show();
                }
                else if (iFrmType == 1)
                {
                    CommonData.ViewReport = "MIS_SP_STOCK_PROCESS";
                    childReportViewer = new ReportViewer(Company, Branches, dtpFromDoc.Value.ToString("MMMyyyy").ToUpper(), "");
                    childReportViewer.Show();
                }
            }

        }
              

        private void tvBranches_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TriStateTreeView.getStatus(e);
            tvBranches.BeginUpdate();

            foreach (TreeNode Node in e.Node.Nodes)
            {
                Node.Checked = e.Node.Checked;
            }

            tvBranches.EndUpdate();
        }       

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                GetSelectedCompAndBranches();              

                try
                {
                    if (cbDesig.Text == "SR")
                    {
                        //GetSelectedCompAndBranches();

                        objExDb = new ExcelDB();
                        objUtilityDB = new UtilityDB();
                        DataTable dtExcel = new DataTable();
                        dtExcel = objExDb.GetAllIndiaTopSrDownloadExcel(Company, Branches, txtNoofRecords.Text.ToString(), dtpFromDoc.Value.ToString("MMMyyyy"), dtpToDoc.Value.ToString("MMMyyyy"), "").Tables[0];
                        objExDb = null;

                        if (dtExcel.Rows.Count > 0)
                        {
                            Excel.Application oXL = new Excel.Application();
                            Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                            Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                            oXL.Visible = true;
                            int iTotColumns = 0;
                            iTotColumns = 7;
                            string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);                          
                            Excel.Range rg = worksheet.get_Range("A3", sLastColumn + "3");
                            Excel.Range rgData = worksheet.get_Range("A3", sLastColumn + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString());
                            rgData.Font.Size = 11;
                            rgData.WrapText = true;
                            rgData.VerticalAlignment = Excel.Constants.xlCenter;
                            rgData.Borders.Weight = 2;



                            rg.Font.Bold = true;
                            rg.Font.Name = "Times New Roman";
                            rg.Font.Size = 10;
                            rg.WrapText = true;
                            rg.Font.ColorIndex = 2; // White Color : 2 and Red = 3,30; Green = 10,43; 
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Interior.ColorIndex = 31;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;
                            rg.Cells.RowHeight = 38;

                            Excel.Range rgHead = null;
                            rgHead = worksheet.get_Range("A1", "G2");
                            rgHead.Merge(Type.Missing);
                            rgHead.Font.Size = 14;
                            rgHead.Font.ColorIndex = 1;
                            rgHead.Font.Bold = true;
                            rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                            rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                            string NoOfRecords = txtNoofRecords.Text.ToString();
                            rgHead.Cells.Value2 = "ALL INDIA TOP " + NoOfRecords + " SR's";


                            rg = worksheet.get_Range("A3", Type.Missing);
                            rg.Cells.ColumnWidth = 7;

                            rg = worksheet.get_Range("B3", Type.Missing);
                            rg.Cells.ColumnWidth = 15;

                            rg = worksheet.get_Range("C3", Type.Missing);
                            rg.Cells.ColumnWidth = 8;

                            rg = worksheet.get_Range("D3", Type.Missing);
                            rg.Cells.ColumnWidth = 30;

                            rg = worksheet.get_Range("E3", Type.Missing);
                            rg.Cells.ColumnWidth = 35;

                            rg = worksheet.get_Range("F3", Type.Missing);
                            rg.Cells.ColumnWidth = 10;

                            rg = worksheet.get_Range("G3", Type.Missing);
                            rg.Cells.ColumnWidth = 10;

                            rg = worksheet.get_Range("H3", Type.Missing);
                            rg.Cells.ColumnWidth = 10;

                            
                            int iColumn = 1;
                            worksheet.Cells[3, iColumn++] = "Rank";
                            worksheet.Cells[3, iColumn++] = "Emp Pic";
                            worksheet.Cells[3, iColumn++] = "Ecode";
                            worksheet.Cells[3, iColumn++] = "Emp Name ";
                            worksheet.Cells[3, iColumn++] = "Brance Name";
                            worksheet.Cells[3, iColumn++] = "Region";
                            worksheet.Cells[3, iColumn++] = "Total Points";
                            
                            iColumn = 1;
                            for (int i = 0; i < dtExcel.Rows.Count; i++)
                            {
                                string strImgFile = "";

                                worksheet.Cells[i+4, iColumn++] = (i + 1).ToString();

                                if (dtExcel.Rows[i]["sr_Photo"].ToString() != "")
                                {
                                    strImgFile = byteArrayToImage((byte[])dtExcel.Rows[i]["sr_Photo"]);

                                    Excel.Range CellRange = (Excel.Range)worksheet.Cells[i+4, iColumn++];
                                    float Left = (float)((double)CellRange.Left);
                                    float Top = (float)((double)CellRange.Top);
                                    float ImageSize = 65;
                                    worksheet.Shapes.AddPicture(strImgFile, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left + 2, Top + 2, ImageSize, ImageSize);
                                    CellRange.RowHeight = ImageSize + 2;
                                }
                                else
                                {
                                    worksheet.Cells[i+4, iColumn++] = "";
                                }
                                                               
                                worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["sr_eora_code"].ToString();
                                worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["sr_eora_name"].ToString();

                                worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["sr_branch_Name"].ToString();
                                worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["sr_region_name"].ToString();
                                worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["sr_total_points"].ToString();

                                iColumn = 1;
                            }
                        }


                        else
                        {
                            MessageBox.Show("No Data Found!", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    if (cbDesig.Text == "GC" || cbDesig.Text == "GL")
                    {
                        //GetSelectedCompAndBranches();

                        objExDb = new ExcelDB();
                        objUtilityDB = new UtilityDB();
                        DataTable dtExcel = new DataTable();
                        dtExcel = objExDb.GetAllIndiaTopGLSDownloadExcel(Company, Branches, txtNoofRecords.Text.ToString(), dtpFromDoc.Value.ToString("MMMyyyy"), dtpToDoc.Value.ToString("MMMyyyy"), "").Tables[0];
                        objExDb = null;

                        if (dtExcel.Rows.Count > 0)
                        {
                            Excel.Application oXL = new Excel.Application();
                            Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                            Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                            oXL.Visible = true;
                            int iTotColumns = 0;
                            iTotColumns = 9;
                            string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                            Excel.Range rgHead = null;
                            Excel.Range rg = worksheet.get_Range("A4", sLastColumn + "4");
                            Excel.Range rgData = worksheet.get_Range("A5", sLastColumn + (dtExcel.Rows.Count + 4).ToString());
                            rgData.Font.Size = 11;
                            rgData.WrapText = true;
                            rgData.VerticalAlignment = Excel.Constants.xlCenter;
                            rgData.Borders.Weight = 2;

                            rgData = worksheet.get_Range("A1", "G3");
                            rgData.Merge(Type.Missing);
                            rgData.Font.Bold = true; rgData.Font.Size = 16;
                            rgData.Value2 = "ALL INDIA TOP " + txtNoofRecords.Text.ToString() + " GC/GL'S ";
                            rgData.VerticalAlignment = Excel.Constants.xlCenter;
                            rgData.HorizontalAlignment = Excel.Constants.xlCenter;


                            rg.Font.Bold = true;
                            rg.Font.Name = "Times New Roman";
                            rg.Font.Size = 10;
                            rg.WrapText = true;
                            rg.Font.ColorIndex = 2; // White Color : 2 and Red = 3,30; Green = 10,43; 
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.Interior.ColorIndex = 31;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;
                            rg.Cells.RowHeight = 38;

                            rg = worksheet.get_Range("A4", Type.Missing);
                            rg.Cells.ColumnWidth = 4;
                            rg = worksheet.get_Range("B4", Type.Missing);
                            rg.Cells.ColumnWidth = 15;
                            rg = worksheet.get_Range("C4", Type.Missing);
                            rg.Cells.ColumnWidth = 8;
                            rg = worksheet.get_Range("D4", Type.Missing);
                            rg.Cells.ColumnWidth = 30;
                            rg = worksheet.get_Range("E4", Type.Missing);
                            rg.Cells.ColumnWidth = 35;
                            rg = worksheet.get_Range("F4", Type.Missing);
                            rg.Cells.ColumnWidth =15;
                            rg = worksheet.get_Range("G3", Type.Missing);
                            rg.Cells.ColumnWidth = 8;


                            rg = worksheet.get_Range("H3", "I3");
                            rg.Merge(Type.Missing);
                            rg.Value2 = "TOTAL";
                            rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                            rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg = worksheet.get_Range("H4", Type.Missing);
                            rg.Cells.ColumnWidth = 6;
                            rg = worksheet.get_Range("I4", Type.Missing);
                            rg.Cells.ColumnWidth = 6;
                            rg.WrapText = true;

                            int iColumn = 1, iStartRow = 4;
                            worksheet.Cells[iStartRow, iColumn++] = "SlNo";
                            worksheet.Cells[iStartRow, iColumn++] = "Emp Pic";
                            worksheet.Cells[iStartRow, iColumn++] = "Ecode";
                            worksheet.Cells[iStartRow, iColumn++] = "Emp Name";
                            worksheet.Cells[iStartRow, iColumn++] = "Branch Name";
                            worksheet.Cells[iStartRow, iColumn++] = "Region";
                            worksheet.Cells[iStartRow, iColumn++] = "Category";
                            worksheet.Cells[iStartRow, iColumn++] = "Groups";
                            worksheet.Cells[iStartRow, iColumn++] = "Points P/G";


                            iStartRow++; iColumn = 1;
                            for (int i = 0; i < dtExcel.Rows.Count; i++)
                            {
                                string strImgFile = "";

                                worksheet.Cells[iStartRow, iColumn++] = (i + 1).ToString();

                                if (dtExcel.Rows[i]["gl_Photo"].ToString() != "")
                                {
                                    strImgFile = byteArrayToImage((byte[])dtExcel.Rows[i]["gl_Photo"]);

                                    Excel.Range CellRange = (Excel.Range)worksheet.Cells[iStartRow, iColumn++];
                                    float Left = (float)((double)CellRange.Left);
                                    float Top = (float)((double)CellRange.Top);
                                    float ImageSize = 65;
                                    worksheet.Shapes.AddPicture(strImgFile, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left + 2, Top + 2, ImageSize, ImageSize);
                                    CellRange.RowHeight = ImageSize + 2;
                                }
                                else
                                {
                                    worksheet.Cells[iStartRow, iColumn++] = "";
                                }
                                
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["gl_eora_code"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["gl_eora_name"].ToString();

                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["gl_branch_Name"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["gl_region_name"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["gl_category"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["gl_groups"].ToString();
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["gl_points_avg"].ToString();
                                iStartRow++; iColumn = 1;
                            }
                        }

                        else
                        {
                            MessageBox.Show("No Data Found!", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            if (iFrmType == 1)
            {  
                #region STATE WISE STOCK POINT RECONCILATION
                try
                {                

                    objExDb = new ExcelDB();
                    DataTable dtExcel = objExDb.StateWiseStockReconsilation(Company, Branches, dtpFromDoc.Value.ToString("MMMyyyy").ToUpper(), "").Tables[0];
                    objExDb = null;

                    if (dtExcel.Rows.Count > 0)
                    {
                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;

                        Excel.Range rg = worksheet.get_Range("A4", "Y4");

                        Excel.Range rgData = worksheet.get_Range("A4", "Y" + (dtExcel.Rows.Count + 4).ToString());
                        rgData.Font.Size = 11;
                        rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.Borders.Weight = 2;

                        rg.Font.Bold = true;
                        rg.Font.Name = "Times New Roman";
                        rg.Font.Size = 10;
                        rg.WrapText = true;
                        rg.Font.ColorIndex = 2; // White Color : 2 and Red = 3,30; Green = 10,43; 
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Interior.ColorIndex = 31;
                        rg.Borders.Weight = 2;
                        rg.Borders.LineStyle = Excel.Constants.xlSolid;
                        rg.Cells.RowHeight = 38;


                        rg = worksheet.get_Range("A1", "U1");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "STOCK RECONSILATION";
                        rg.Font.Bold = true; rg.Font.Size = 16;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;


                        rgData = worksheet.get_Range("W1", "Y1");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 14;
                        rgData.Value2 = "DOC MONTH : " + dtpFromDoc.Value.ToString("MMMyyyy");
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.HorizontalAlignment = Excel.Constants.xlCenter;



                        rg = worksheet.get_Range("A4", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "Sl.No";

                        rg = worksheet.get_Range("B4", Type.Missing);
                        rg.Cells.ColumnWidth = 30;
                        rg.Value2 = " SP NAME";


                        rg = worksheet.get_Range("C4", Type.Missing);
                        rg.Cells.ColumnWidth = 30;
                        rg.Value2 = " BRANCH NAME";

                        rg = worksheet.get_Range("D2", "F3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "OPENING STOCK";
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 21;
                        rg.Font.Bold = true; rg.Font.Size = 12;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;

                        rg = worksheet.get_Range("D4", Type.Missing);
                        rg.Cells.ColumnWidth =10;  
                        rg.Value2 = "GOOD";

                        rg = worksheet.get_Range("E4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;  
                        rg.Value2 = "DAMAGE";

                        rg = worksheet.get_Range("F4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;  
                        rg.Value2 = "TOTAL";



                        rg = worksheet.get_Range("G2", "I3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "RECEIPTS(GRN)";
                        rg.Font.ColorIndex = 3;
                        rg.Interior.ColorIndex = 8;
                        rg.Font.Bold = true; rg.Font.Size = 12;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;

                        rg = worksheet.get_Range("G4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "GOOD";

                        rg = worksheet.get_Range("H4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "DAMAGE";

                        rg = worksheet.get_Range("I4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "TOTAL";


                        rg = worksheet.get_Range("J2", "P2");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "ISSUES";
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 5;
                        rg.Font.Bold = true; rg.Font.Size = 12;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;

                     

                        rg = worksheet.get_Range("J3", "J4");
                        rg.Merge(Type.Missing);
                        rg.Interior.ColorIndex = 31;
                        rg.Font.ColorIndex = 2;
                        rg.WrapText = true;
                        rg.Value2 = " DC GOOD";

                        rg = worksheet.get_Range("K3", "M3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "DCST";
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex =9 ;
                        rg.Font.Bold = true; rg.Font.Size = 12;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;                    


                        rg = worksheet.get_Range("K4",Type.Missing);
                        rg.Cells.ColumnWidth = 10;                       
                        rg.Value2 = "GOOD";

                        rg = worksheet.get_Range("L4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "DAMAGE";

                        rg = worksheet.get_Range("M4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "TOTAL";

                        rg = worksheet.get_Range("N3","P3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "TOTAL";
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 21;
                        rg.Font.Bold = true; rg.Font.Size = 12;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;

                        rg = worksheet.get_Range("N4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "GOOD";

                        rg = worksheet.get_Range("O4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "DAMAGE";

                        rg = worksheet.get_Range("P4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "TOTAL";


                        //rg = worksheet.get_Range("Q2", "Q3");
                        //rg.Merge(Type.Missing);
                        //rg.Value2 = "";
                        //rg.Font.ColorIndex = 2;



                        //rg = worksheet.get_Range("Q4", Type.Missing);
                       
                        rg = worksheet.get_Range("Q2", "Q4");
                        rg.Merge(Type.Missing);
                        rg.Interior.ColorIndex = 31;                       
                        rg.Font.ColorIndex = 2;
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Font.Size = 12;
                        rg.Value2 = "DSPU_ISSUE DAMAGE";
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;                    


                        //rg = worksheet.get_Range("R2", "R3");
                        //rg.Merge(Type.Missing);
                        //rg.Value2 = "";
                        //rg.Font.ColorIndex = 2;


                        //rg = worksheet.get_Range("R4", Type.Missing);
                        //rg.Cells.ColumnWidth = 15;
                        rg = worksheet.get_Range("R2", "R4");
                        rg.Merge(Type.Missing);
                        rg.Interior.ColorIndex = 21;
                        rg.Font.ColorIndex = 2;
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Font.Size = 12;
                        rg.Value2 = "GRPU RECEIPT GOOD";
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;                    


                        rg = worksheet.get_Range("S2", "T3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "INTERNAL CONVERSION";
                        rg.Font.ColorIndex = 3;
                        rg.Interior.ColorIndex = 8;
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Font.Size = 12;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;

                        rg = worksheet.get_Range("S4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "G2D";

                        rg = worksheet.get_Range("T4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "D2G";


                        //rg = worksheet.get_Range("U2", "U3");
                        //rg.Merge(Type.Missing);
                        //rg.Value2 = "";


                        //rg = worksheet.get_Range("U4", Type.Missing);
                        //rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("U2", "U4");
                        rg.Merge(Type.Missing);
                        rg.Interior.ColorIndex = 21;
                        rg.Font.ColorIndex = 2;
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Font.Size = 12;
                        rg.Value2 = "SHORTAGE/WRITE OFF";
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;                    


                        //rg = worksheet.get_Range("V2", "V3");
                        //rg.Merge(Type.Missing);
                        //rg.Value2 = "";


                        //rg = worksheet.get_Range("V4", Type.Missing);
                        //rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("V2", "V4");
                        rg.Merge(Type.Missing);
                        rg.Interior.ColorIndex = 31;
                        rg.Font.ColorIndex = 2;
                        rg.Font.Bold = true; rg.Font.Size = 12;
                        rg.Value2 = "EXCESS";
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;                    


                        

                        rg = worksheet.get_Range("W2", "Y3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "CLOSING STOCK";
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex =21;
                        rg.Font.Bold = true; rg.Font.Size = 12;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;

                        rg = worksheet.get_Range("W4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "GOOD";

                        rg = worksheet.get_Range("X4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "DAMAGE";

                        rg = worksheet.get_Range("Y4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "TOTAL";
                        
                        int RowCounter = 1;

                        foreach (DataRow dr in dtExcel.Rows)
                        {
                            int i = 1;
                            worksheet.Cells[RowCounter + 4, i++] = RowCounter;
                            worksheet.Cells[RowCounter + 4, i++] = dr["sm_state"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["MSMS_BRANCH_NAME"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["MSMS_OPST_GOOD"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["MSMS_OPST_DMG"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["MSMS_OPST_TOT"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["MSMS_GRN_GOOD"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["MSMS_GRN_DMG"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["MSMS_GRN_TOT"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["MSMS_DC_GOOD"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["MSMS_DCST_GOOD"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["MSMS_DCST_DMG"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["MSMS_DCST_TOT"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["MSMS_ISSUE_GOOD"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["MSMS_ISSUE_DMG"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["MSMS_ISSUE_TOT"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["MSMS_REFILL_DC"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["MSMS_REFILL_GRN"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["MSMS_INTCNV_G2D"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["SMS_INTCNV_D2G"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["MSMS_SHORTAGE_TOT"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["MSMS_EXCESS_TOT"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["MSMS_CLOSING_GOOD"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["MSMS_CLOSING_DMG"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["MSMS_CLOSING_TOT"].ToString();                  

                            RowCounter++;
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                #endregion


            }
        }

        private string byteArrayToImage(byte[] byteArray)
        {
            System.Drawing.Image newImage;
            string strFileName = "D:\\EmpPhoto.jpg";
            if (byteArray != null)
            {
                using (MemoryStream stream = new MemoryStream(byteArray))
                {
                    newImage = System.Drawing.Image.FromStream(stream);
                    newImage.Save(strFileName);
                }
                return strFileName;
            }
            else
            {
                return "";
            }
        }

        private void cbRepType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRepType.SelectedIndex > 0)
            {
                FillDesigComboBox();
            }
            else
            {
                cbDesig.DataSource = null; 
            }
        }
        

    }
}


        


