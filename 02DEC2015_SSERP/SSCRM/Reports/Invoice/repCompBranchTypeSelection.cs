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

using Excel = Microsoft.Office.Interop.Excel;

namespace SSCRM
{
    public partial class repCompBranchTypeSelection : Form
    {
        Security objSecurity = null;
        ExcelDB objExcelDB = null;
        private UtilityDB objUtilityDB = null;
        string sComp = "";
        string sBranch = "";
        public repCompBranchTypeSelection()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbCompany.Items.Count; i++)
            {
                clbCompany.SetItemCheckState(i, CheckState.Unchecked);

            }
            for (int i = 0; i < clbBranch.Items.Count; i++)
            {
                clbBranch.SetItemCheckState(i, CheckState.Unchecked);

            }
            cbStatus.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            FillBranchType();
            cbStatus.SelectedIndex = 0;

        }
        private void FillCompanyData()
        {
            try
            {
                objSecurity = new Security();
                DataTable dtCpy = objSecurity.GetCompanyDataSet().Tables[0];

                ((ListBox)clbCompany).DataSource = dtCpy;
                ((ListBox)clbCompany).DisplayMember = "CM_Company_Name";
                ((ListBox)clbCompany).ValueMember = "CM_Company_Code";
                objSecurity = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();
            this.Dispose();
        }

        private void FillBranchType()
        {
            string strBranch = " SELECT Distinct(BRANCH_TYPE),"
                               + " CASE "
                                + " WHEN BRANCH_TYPE='BR' THEN 'BRANCH'"
                                + " WHEN BRANCH_TYPE='SP' THEN 'STOCK POINT'"
                                + " WHEN BRANCH_TYPE='PU' THEN 'PRODUCTION UNIT'"
                                + " WHEN BRANCH_TYPE='TR' THEN 'TRANSPORT UNIT'"
                                + " WHEN BRANCH_TYPE='OL' THEN 'OUTLETS'"
                                + " WHEN BRANCH_TYPE='PO' THEN 'PARTY OFFICE'"
                                + " WHEN BRANCH_TYPE='HO' THEN 'CORPORATE OFFICE'"
                                + " WHEN BRANCH_TYPE='ST' THEN 'STATIONARY'"
                                + " WHEN BRANCH_TYPE='RS' THEN 'RETAIL STORE'"
                                + " WHEN BRANCH_TYPE='WH' THEN 'WARE HOUSE'"
                                + " ELSE ''"
                                + " END"
                                + " DISCRIPTION"
                                + " FROM BRANCH_MAS";

            try
            {
                SQLDB sql = new SQLDB();
                objSecurity = new Security();
                DataTable dt = sql.ExecuteDataSet(strBranch).Tables[0];

                ((ListBox)clbBranch).DataSource = dt;
                ((ListBox)clbBranch).DisplayMember = "DISCRIPTION";
                ((ListBox)clbBranch).ValueMember = "BRANCH_TYPE";
                objSecurity = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }



        private void btnPrint_Click(object sender, EventArgs e)
        {

            if (CheckData())
            {
                GetSelectedValues();             

                ReportViewer childReportViewer = new ReportViewer(sComp, sBranch, sBranch, cbStatus.SelectedItem.ToString());
                CommonData.ViewReport = "SSERP_REP_BRANCH_MASTER";
                childReportViewer.Show();



            }
        }


        private bool CheckData()
        {
            bool blVil = false;
            for (int i = 0; i < clbCompany.Items.Count; i++)
            {
                if (clbCompany.GetItemCheckState(i) == CheckState.Checked)
                    blVil = true;
            }
            if (blVil == false)
            {
                MessageBox.Show("Select clbCompany checkBox", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return blVil;
            }
            blVil = false;
            for (int i = 0; i < clbBranch.Items.Count; i++)
            {
                if (clbBranch.GetItemCheckState(i) == CheckState.Checked)
                    blVil = true;
            }
            if (blVil == false)
            {
                MessageBox.Show("Select Branch checkBox", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return blVil;
            }
            return blVil;
        }
        private void GetSelectedValues()
        {
            sComp = "";
            //lstEcodes.Items.Clear();
            for (int i = 0; i < clbCompany.Items.Count; i++)
            {
                if (clbCompany.GetItemCheckState(i) == CheckState.Checked)
                {
                    if (sComp != "")
                        sComp += "";
                    DataRowView view = (DataRowView)clbCompany.Items[i];
                    sComp += "" + (view[clbCompany.ValueMember].ToString()) + ",";
                }
            }
            sBranch = "";
            for (int i = 0; i < clbBranch.Items.Count; i++)
            {
                if (clbBranch.GetItemCheckState(i) == CheckState.Checked)
                {
                    if (sBranch != "")
                        sBranch += "";
                    DataRowView view = (DataRowView)clbBranch.Items[i];

                    sBranch += "" + (view[clbBranch.ValueMember].ToString()) + ",";
                }
            }
        }
        private void btnDownload_Click(object sender, EventArgs e)
        {
            DataTable dtExcel = new DataTable();
            objExcelDB = new ExcelDB();
            objUtilityDB = new UtilityDB();
            #region Branch Master"
            if (CheckData())
            {
                GetSelectedValues();
                try
                {

                    dtExcel = objExcelDB.GetBranchMaster(sComp, sBranch, sBranch, cbStatus.SelectedItem.ToString()).Tables[0];
                    objExcelDB = null;


                    if (dtExcel.Rows.Count > 0)
                    {
                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;
                        int iTotColumns = 0;
                        iTotColumns = 29;
                        string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                        Excel.Range rgHead = null;
                        Excel.Range rg = worksheet.get_Range("A4", sLastColumn + "4");
                        Excel.Range rgData = worksheet.get_Range("A5", sLastColumn + (dtExcel.Rows.Count + 4).ToString());
                        rgData.Font.Size = 11;
                        rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.Borders.Weight = 2;

                        rgData = worksheet.get_Range("A1", "K2");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 16;
                        rgData.Value2 = "BRANCH SP/PU/TU DETAILS";
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
                        rg.Cells.ColumnWidth = 12;
                        rg = worksheet.get_Range("C4", Type.Missing);
                        rg.Cells.ColumnWidth = 15;
                        rg = worksheet.get_Range("D4", Type.Missing);
                        rg.Cells.ColumnWidth = 50;
                        rg = worksheet.get_Range("E4", Type.Missing);
                        rg.Cells.ColumnWidth = 12;
                        rg = worksheet.get_Range("F4", Type.Missing);
                        rg.Cells.ColumnWidth = 12;
                        rg = worksheet.get_Range("G4", Type.Missing);
                        rg.Cells.ColumnWidth = 12;
                        rg = worksheet.get_Range("H4", Type.Missing);
                        rg.Cells.ColumnWidth = 12;
                        rg = worksheet.get_Range("I4", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg = worksheet.get_Range("J4", Type.Missing);
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("K4", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg = worksheet.get_Range("L4", Type.Missing);
                        rg.Cells.ColumnWidth = 15;
                        rg = worksheet.get_Range("M4", Type.Missing);
                        rg.Cells.ColumnWidth = 15;
                        rg = worksheet.get_Range("N4", Type.Missing);
                        rg.Cells.ColumnWidth = 15;
                        rg = worksheet.get_Range("O4", Type.Missing);
                        rg.Cells.ColumnWidth = 15;
                        rg = worksheet.get_Range("P4", Type.Missing);
                        rg.Cells.ColumnWidth = 15;
                        rg = worksheet.get_Range("Q4", Type.Missing);
                        rg.Cells.ColumnWidth = 15;
                        rg = worksheet.get_Range("R4", Type.Missing);
                        rg.Cells.ColumnWidth = 15;
                        rg = worksheet.get_Range("S4", Type.Missing);
                        rg.Cells.ColumnWidth = 15;
                        rg = worksheet.get_Range("T4", Type.Missing);
                        rg.Cells.ColumnWidth = 15;
                        rg = worksheet.get_Range("U4", Type.Missing);
                        rg.Cells.ColumnWidth = 15;
                        rg = worksheet.get_Range("V4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("W4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;            
                        rg = worksheet.get_Range("X4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("Y4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("Z4", Type.Missing);
                        rg.Cells.ColumnWidth = 9;
                        rg = worksheet.get_Range("AA4", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg = worksheet.get_Range("AB4", Type.Missing);
                        rg.Cells.ColumnWidth =30;
                        rg = worksheet.get_Range("AC4", Type.Missing);
                        rg.Cells.ColumnWidth = 8;




                        int iColumn = 1, iStartRow = 4;
                        worksheet.Cells[iStartRow, iColumn++] = "SlNo";
                        worksheet.Cells[iStartRow, iColumn++] = "Branch Code";
                        worksheet.Cells[iStartRow, iColumn++] = "Branch Name";
                        worksheet.Cells[iStartRow, iColumn++] = "Address";
                        worksheet.Cells[iStartRow, iColumn++] = "Sate";
                        worksheet.Cells[iStartRow, iColumn++] = "District";
                        worksheet.Cells[iStartRow, iColumn++] = "Mondal";
                        worksheet.Cells[iStartRow, iColumn++] = "Location";
                        worksheet.Cells[iStartRow, iColumn++] = "ESI";
                        worksheet.Cells[iStartRow, iColumn++] = " Head Ecode";
                        worksheet.Cells[iStartRow, iColumn++] = "Head Name";
                        worksheet.Cells[iStartRow, iColumn++] = "Desig";
                        worksheet.Cells[iStartRow, iColumn++] = "Br Contact NO";
                        worksheet.Cells[iStartRow, iColumn++] = "Branch E_mail";
                        worksheet.Cells[iStartRow, iColumn++] = "Hr E_mail";
                        worksheet.Cells[iStartRow, iColumn++] = "RHr E_mail";
                        worksheet.Cells[iStartRow, iColumn++] = "Inch E_mail";
                        worksheet.Cells[iStartRow, iColumn++] = "Trainer E_mail";
                        worksheet.Cells[iStartRow, iColumn++] = "TIN";
                        worksheet.Cells[iStartRow, iColumn++] = "CST";
                        worksheet.Cells[iStartRow, iColumn++] = "EXERC";
                        worksheet.Cells[iStartRow, iColumn++] = "Rent Type";
                        worksheet.Cells[iStartRow, iColumn++] = "From Date";
                        worksheet.Cells[iStartRow, iColumn++] = "To Date";
                        worksheet.Cells[iStartRow, iColumn++] = "Deposit";
                        worksheet.Cells[iStartRow, iColumn++] = "Rent EMI";
                        worksheet.Cells[iStartRow, iColumn++] = "Woner Name";
                        worksheet.Cells[iStartRow, iColumn++] = "Remarks";
                        worksheet.Cells[iStartRow, iColumn++] = "Status";
                      

                        iStartRow++; iColumn = 1;
                        for (int i = 0; i < dtExcel.Rows.Count; i++)
                        {
                            worksheet.Cells[iStartRow, iColumn++] = i + 1;
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["RS_BRANCH_CODE"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["RS_BRANCH_NAME"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["RS_BRANCH_ADDRESS"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["RS_STATE"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["RS_DISTRICT"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["RS_MANDAL"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["RS_LOCATION"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["RS_ESI_APPLICABLE"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["RS_HEAD_ECODE"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["RS_HEAD_NAME"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["RS_HEAD_DESIG"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["RS_BRANCH_CONTNO"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["RS_MAIL_ID"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["RS_HR_MAIL_ID"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["RS_REGHR_MAILID"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["RS_INCH_MAILID"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["RS_TRAINER_MAILID"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["RS_TIN_NO"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["RS_CST_NO"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["RS_EXREG_NO"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["RS_RENT_TYPE"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["RS_RENT_FROM"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["RS_RENT_TO"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["RS_SECURE_DEPOSIT"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["RS_RENT_EMI"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["RS_OWNER_NAME"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["RS_REMARKS"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["RS_STATUS"];

                            iStartRow++; iColumn = 1;
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            #endregion
        }

    }
        
    }

