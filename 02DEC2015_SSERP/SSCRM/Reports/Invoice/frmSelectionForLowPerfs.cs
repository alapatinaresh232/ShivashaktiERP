using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office;
using System.Xml;
using SSAdmin;
using SSCRMDB;
using SSTrans;
using SSCRMDB;
using SSCRM.App_Code;

namespace SSCRM
{
    public partial class frmSelectionForLowPerfs : Form
    {
        SQLDB objSQLdb = null;
        ExcelDB objExcelDB = null;
        private InvoiceDB objData = null;
       
        UtilityDB objUtilityDB = null;
        private string strECode = string.Empty;
        private string sMonthsFlag = "", sGrpsFlag = "", sPntsFlag = "", sPntsPerGrpFlag = "", sPntsPerHeadFlag = "", sPntsGrpHeadFlag = "";
        Int32 frmMnths = 0, ToMnths = 0, frmGrps = 0, ToGrps = 0, frmPersPoints = 0, ToPersPoints = 0, frmPntsPerGrp = 0, ToPntsPerGrp = 0, frmPntsPerHead = 0, ToPntsPerHead = 0;

        public frmSelectionForLowPerfs()
        {
            InitializeComponent();
        }

        private void frmSelectionForLowPerfs_Load(object sender, EventArgs e)
        {
            dtpFromDoc.Value = DateTime.Now;
            dtpToDoc.Value = DateTime.Now;
            FillEmployeeData("");
            cbSortBy.SelectedIndex = 0;
            chkGrps.Checked = true;
            chkMonths.Checked = true;
            cbSelection.Visible = false;
            chkGrps.Visible = false;
            chkMonths.Visible = false;

        }
    
        private void txFrmMnths_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtToMnths_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtFrmGrps_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtToGrps_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtFrmPersPts_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtToPersPts_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }


        private void FillEmployeeData(string strSearch)
        {
            objData = new InvoiceDB();
            DataSet dsEmp = null;
            try
            {
                dsEmp = objData.InvLevelEmpNameSearch(strSearch);
                DataTable dtEmp = dsEmp.Tables[0];
                if (dtEmp.Rows.Count > 0)
                {
                    UtilityLibrary.AutoCompleteComboBox(cbEcode, dtEmp, "", "ENAME");

                    cbEcode.DataSource = dtEmp;
                    cbEcode.DisplayMember = "ENAME";
                    cbEcode.ValueMember = "ECODE";

                }
                else
                {
                    cbEcode.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (cbEcode.SelectedIndex > -1)
                {
                    cbEcode.SelectedIndex = 0;
                    strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
                }
                objData = null;
                Cursor.Current = Cursors.Default;
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtEcode.Text = "";
            //cbEcode.DataSource = null;
            txFrmMnths.Text = "";
            txtToMnths.Text = "";
            txtFrmGrps.Text = "";
            txtToGrps.Text = "";
            txtFrmPersPts.Text = "";
            txtToPersPts.Text = "";
            txtFrmPntsPerGrp.Text = "";
            txtFrmPntsPerHead.Text = "";
            txtToPntsPerGrp.Text = "";
            txtToPntsPerHead.Text = "";
            cbSortBy.SelectedIndex = 0;

            if (chkPntsPerGrp.CheckState == CheckState.Unchecked || chkPntsPerHead.CheckState == CheckState.Unchecked)
            {
                txtFrmPntsPerGrp.Text = "";
                txtFrmPntsPerHead.Text = "";
                txtToPntsPerGrp.Text = "";
                txtToPntsPerHead.Text = "";
                cbSelection.Visible = false;
            }


        }
        private void SearchItems(ComboBox acComboBox, ref KeyPressEventArgs e)
        {
            int selectionStart = acComboBox.SelectionStart;
            int selectionLength = acComboBox.SelectionLength;
            int selectionEnd = selectionStart + selectionLength;
            int index;
            StringBuilder sb = new StringBuilder();

            sb.Append(acComboBox.Text.Substring(0, selectionStart))
                .Append(e.KeyChar.ToString())
                .Append(acComboBox.Text.Substring(selectionEnd));

            index = acComboBox.FindString(sb.ToString());

            if (index == -1)
                e.Handled = false;
            else
            {
                acComboBox.SelectedIndex = index;
                acComboBox.Select(selectionStart + 1, acComboBox.Text.Length - (selectionStart + 1));
                e.Handled = true;
            }
        }

        private void cbEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!e.KeyChar.Equals((char)8))
            //{
            //    SearchItems(cbEcode, ref e);
            //}
            //else
            //    e.Handled = false;           
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtEcode_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcode.Text.ToString().Trim().Length > 4)
            {
                FillEmployeeData(txtEcode.Text);
            }
            else
            {
                FillEmployeeData("");
            }
        }

        private void GetSelectedValues()
        {
            if (chkPntsPerGrp.Checked == true)
            {
                sPntsPerGrpFlag = "YES";
            }
            else
            {
                sPntsPerGrpFlag = "";
            }
            if (chkPntsPerHead.Checked == true)
            {
                sPntsPerHeadFlag = "YES";
            }
            else
            {
                sPntsPerHeadFlag = "";
            }
            if (chkGrps.Checked == true)
            {
                sGrpsFlag = "YES";
            }
            else
            {
                sGrpsFlag = "";
            }
            if (chkMonths.Checked == true)
            {
                sMonthsFlag = "YES";
            }
            else
            {
                sMonthsFlag = "";
            }
            if (chkPersPnts.Checked == true)
            {
                sPntsFlag = "YES";
            }
            else
            {
                sPntsFlag = "";
            }
            if (chkPntsPerGrp.Checked == true & chkPntsPerHead.Checked == true)
            {
                if (cbSelection.SelectedIndex == 0)
                {
                    sPntsGrpHeadFlag = "AND";
                }
                else
                {
                    sPntsGrpHeadFlag = "OR";
                }
            }
            else
            {
                sPntsGrpHeadFlag = "";
            }
            if (txFrmMnths.Text != "")
                frmMnths = Convert.ToInt32(txFrmMnths.Text);
            else
                frmMnths = 0;
            if (txtToMnths.Text != "")
                ToMnths = Convert.ToInt32(txtToMnths.Text);
            else
                ToMnths = 0;
            if (txtFrmGrps.Text != "")
                frmGrps = Convert.ToInt32(txtFrmGrps.Text);
            else
                frmGrps = 0;
            if (txtToGrps.Text != "")
                ToGrps = Convert.ToInt32(txtToGrps.Text);
            else
                ToGrps = 0;

            if (txtFrmPersPts.Text != "")
                frmPersPoints = Convert.ToInt32(txtFrmPersPts.Text);
            else
                frmPersPoints = 0;

            if (txtToPersPts.Text != "")
                ToPersPoints = Convert.ToInt32(txtToPersPts.Text);
            else
                ToPersPoints = 0;

            if (txtFrmPntsPerGrp.Text != "")
                frmPntsPerGrp = Convert.ToInt32(txtFrmPntsPerGrp.Text);
            else
                frmPntsPerGrp = 0;

            if (txtToPntsPerGrp.Text != "")
                ToPntsPerGrp = Convert.ToInt32(txtToPntsPerGrp.Text);
            else
                ToPntsPerGrp = 0;

            if (txtFrmPntsPerHead.Text != "")
                frmPntsPerHead = Convert.ToInt32(txtFrmPntsPerHead.Text);
            else
                frmPntsPerHead = 0;

            if (txtToPntsPerHead.Text != "")
                ToPntsPerHead = Convert.ToInt32(txtToPntsPerHead.Text);
            else
                ToPntsPerHead = 0;

        }


        private void btnDownload_Click(object sender, EventArgs e)
        {
            DataTable dtExcel = new DataTable();
            objExcelDB = new ExcelDB();
            objUtilityDB = new UtilityDB();
            int iTotColumns = 0;

            if (CheckData() == true)
            {
                GetSelectedValues();

                if (txtFrmPersPts.Text.Length == 0)
                    txtFrmPersPts.Text = "0";
                if (txtToPersPts.Text.Length == 0)
                    txtToPersPts.Text = "0";
                

                #region "Low Performers List"
                try
                {
                    dtExcel = objExcelDB.Get_LowPerformersList(CommonData.CompanyCode, CommonData.BranchCode, Convert.ToInt32(cbEcode.SelectedValue), dtpFromDoc.Value.ToString("MMMyyyy").ToUpper(),
                                           dtpToDoc.Value.ToString("MMMyyyy").ToUpper(), sMonthsFlag, frmMnths, ToMnths, sGrpsFlag, frmGrps, ToGrps, sPntsFlag, frmPersPoints,
                                           ToPersPoints, sPntsPerGrpFlag, frmPntsPerGrp, ToPntsPerGrp, sPntsPerHeadFlag, frmPntsPerHead, ToPntsPerHead, cbSortBy.Text, "EXCEL", 
                                           sPntsGrpHeadFlag,Convert.ToDateTime(dtpLOSAsOnDate.Value).ToString("dd/MMM/yyyy")).Tables[0];
                    objExcelDB = null;
                    if (dtExcel.Rows.Count > 0)
                    {
                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;
                        iTotColumns = 13 + (7 * Convert.ToInt32(dtExcel.Rows[0]["al_NoOf_Months"])) + 12;
                        string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                        Excel.Range rg = worksheet.get_Range("A3", sLastColumn + "3");
                        Excel.Range rgData = worksheet.get_Range("A3", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["al_No_Of_Emp"]) + 3).ToString());
                        rgData.Font.Size = 11;
                        rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.Borders.Weight = 2;

                        string strSelection = "";

                        strSelection = "Months Worked From "+ txFrmMnths.Text +" To "+ txtToMnths.Text +", Groups From "+ txtFrmGrps.Text +" To "+ txtToGrps.Text +" ";

                        if (chkPersPnts.Checked == true)
                        {
                            strSelection += " , Personal Points From "+ frmPersPoints +" To "+ ToPersPoints +"";
                        }
                        if (chkPntsPerGrp.Checked == true)
                        {
                            strSelection += " , Points P/G  From "+ frmPntsPerGrp +" To "+ ToPntsPerGrp +"";
                        }
                        if (chkPntsPerHead.Checked == true && chkPntsPerHead.Checked == true)
                        {
                            strSelection += " " +cbSelection.Text.ToString();
                        }
                        else if (chkPntsPerHead.Checked == true || chkPntsPerHead.Checked == true)
                        {
                            strSelection += " ,";
                        }

                        if (chkPntsPerHead.Checked == true)
                        {
                            strSelection += "Points P/H  From "+ frmPntsPerHead +" To "+ ToPntsPerHead +"";
                        }

                        strSelection += " , Length Of Service As On Date "+ Convert.ToDateTime(dtpLOSAsOnDate.Value).ToString("dd/MMM/yyyy") +"";
                        

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

                        rg = worksheet.get_Range("A3", Type.Missing);
                        rg.Cells.ColumnWidth = 5;

                        rg = worksheet.get_Range("B3", Type.Missing);
                        rg.Cells.ColumnWidth = 6;

                        rg = worksheet.get_Range("C3", Type.Missing);
                        rg.Cells.ColumnWidth = 25;

                        rg = worksheet.get_Range("D3", Type.Missing);
                        rg.Cells.ColumnWidth = 7;

                        rg = worksheet.get_Range("E3", Type.Missing);
                        rg.Cells.ColumnWidth = 12;

                        rg = worksheet.get_Range("F3", Type.Missing);
                        rg.Cells.ColumnWidth = 10;

                        rg = worksheet.get_Range("G3", Type.Missing);
                        rg.Cells.ColumnWidth = 12;

                        rg = worksheet.get_Range("H3", Type.Missing);
                        rg.Cells.ColumnWidth = 10;

                        rg = worksheet.get_Range("I3", Type.Missing);
                        rg.Cells.ColumnWidth = 10;

                        rg = worksheet.get_Range("J3", Type.Missing);
                        rg.Cells.ColumnWidth = 20;

                        rg = worksheet.get_Range("K3", Type.Missing);
                        rg.Cells.ColumnWidth = 30;

                        rg = worksheet.get_Range("L3", Type.Missing);
                        rg.Cells.ColumnWidth = 30;

                        rg = worksheet.get_Range("M3", Type.Missing);
                        rg.Cells.ColumnWidth = 30;                      

                        worksheet.Cells[3, 1] = "Sl.No";
                        worksheet.Cells[3, 2] = "Ecode";
                        worksheet.Cells[3, 3] = "Emp Name";
                        worksheet.Cells[3, 4] = "Desig";
                        worksheet.Cells[3, 5] = "DOJ";
                        worksheet.Cells[3, 6] = "Tot Length Of Service";
                        worksheet.Cells[3, 7] = "DOP";
                        worksheet.Cells[3, 8] = "LOS In Pres. Desig";                        
                        worksheet.Cells[3, 9] = "State";
                        worksheet.Cells[3, 10] = "Branch";
                        worksheet.Cells[3, 11] = "Level-1";
                        worksheet.Cells[3, 12] = "Level-2";
                        worksheet.Cells[3, 13] = "Level-3";
                      
                        int iStartColumn = 0;
                        Excel.Range rgHead;
                        for (int iMonths = 1; iMonths <= Convert.ToInt32(dtExcel.Rows[0]["al_NoOf_Months"]); iMonths++)
                        {
                            rgHead = worksheet.get_Range("A1", "M1");
                            rgHead.Merge(Type.Missing);
                            rgHead.Font.Size = 14;
                            rgHead.Font.ColorIndex = 1;
                            rgHead.Font.Bold = true;
                            rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                            rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead.Cells.Value2 = "LIST OF LOW PERFORMERS \n "+ cbEcode.Text +" ";
                            rgHead.Cells.RowHeight = 35;

                            rgHead = worksheet.get_Range("A2", "M2");
                            rgHead.Merge(Type.Missing);
                            rgHead.Font.Size = 12;
                            rgHead.Font.ColorIndex = 30;
                            rgHead.Font.Bold = true;
                            rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                            rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead.Cells.Value2 = "" + strSelection + "";
                            rgHead.Cells.RowHeight = 15;

                            iStartColumn = (7 * iMonths) +7;

                            rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "2", objUtilityDB.GetColumnName(iStartColumn + 6) + "2");
                            //rgHead.Cells.ColumnWidth = 5;
                            rgHead.Merge(Type.Missing);
                            rgHead.Interior.ColorIndex = 32 + iMonths;
                            rgHead.Borders.Weight = 2;
                            rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
                            rgHead.Cells.RowHeight = 25;
                            rgHead.Font.Size = 14;
                            rgHead.Font.ColorIndex = 1;
                            rgHead.Font.Bold = true;
                            rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                            rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "3", objUtilityDB.GetColumnName(iStartColumn + 6) + "3");
                            rgHead.Interior.ColorIndex = 32 + iMonths;
                            rgHead.Font.ColorIndex = 1;
                            rgHead.Cells.ColumnWidth = 5;

                            worksheet.Cells[3, iStartColumn] = "Pers Pmd";
                            worksheet.Cells[3, iStartColumn + 1] = "Pers Points";
                            worksheet.Cells[3, iStartColumn + 2] = "Pers Cust";
                            worksheet.Cells[3, iStartColumn + 3] = "Groups";
                            worksheet.Cells[3, iStartColumn + 4] = "Group Pmd";
                            worksheet.Cells[3, iStartColumn + 5] = "Group Points";                            
                            worksheet.Cells[3, iStartColumn + 6] = "Group Cust";

                        }
                        iStartColumn = 14 + (7 * Convert.ToInt32(dtExcel.Rows[0]["al_NoOf_Months"]));
                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "2", objUtilityDB.GetColumnName(iStartColumn + 11) + "2");
                        //rgHead.Cells.ColumnWidth = 5;
                        rgHead.Merge(Type.Missing);
                        rgHead.Interior.ColorIndex = 34 + Convert.ToInt32(dtExcel.Rows[0]["al_NoOf_Months"]) + 1;
                        rgHead.Borders.Weight = 2;
                        rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
                        
                        rgHead.Font.Size = 14;
                        rgHead.Font.ColorIndex = 1;
                        rgHead.Font.Bold = true;
                        rgHead.Cells.Value2 = "TOTAL";
                        rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "3", objUtilityDB.GetColumnName(iStartColumn + 11) + "3");
                        rgHead.Interior.ColorIndex = 34 + Convert.ToInt32(dtExcel.Rows[0]["al_NoOf_Months"]) + 1;
                        rgHead.Font.ColorIndex = 1;
                        rgHead.Cells.ColumnWidth = 5;

                        worksheet.Cells[3, iStartColumn] = "Pers Work Months";
                        worksheet.Cells[3, iStartColumn + 1] = "Pers PMD";
                        worksheet.Cells[3, iStartColumn + 2] = "Pers Points";
                        worksheet.Cells[3, iStartColumn + 3] = "Pers Cust";
                        worksheet.Cells[3, iStartColumn + 4] = "Group Work Months";
                        worksheet.Cells[3, iStartColumn + 5] = "Groups";
                        worksheet.Cells[3, iStartColumn + 6] = "Group Tot Points";
                        worksheet.Cells[3, iStartColumn + 7] = "Group Tot Pmd";
                        worksheet.Cells[3, iStartColumn + 8] = "Group Avg Pmd P/G";
                        worksheet.Cells[3, iStartColumn + 9] = "Group Points P/G";
                        worksheet.Cells[3, iStartColumn + 10] = "Group Points P/H";
                        worksheet.Cells[3, iStartColumn + 11] = "Group Cust P/H";


                        int iRowCounter = 4; int iColumnCounter = 1;
                        for (int i = 0; i < dtExcel.Rows.Count; i++)
                        {
                            if (i > 0)
                            {
                                if (dtExcel.Rows[i]["al_eora_code"].ToString() == dtExcel.Rows[i - 1]["al_eora_code"].ToString())
                                {
                                    int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["al_Month_SlNo"]);
                                    
                                    iColumnCounter = (7 * iMonthNo) +7;
                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 6) + "2");
                                    rgHead.Cells.Value2 = dtExcel.Rows[i]["al_doc_month"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_pers_pmd"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_pers_points"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_Pers_Cust"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_groups"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_group_pmd"];                                    
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_group_points"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_group_Cust"];
                                }
                                else
                                {
                                    iRowCounter++;
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 3;
                                  
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_eora_code"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_eora_name"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_Pres_Desig"];
                                    if (dtExcel.Rows[i]["al_DOJ"].ToString() != "")
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToDateTime(dtExcel.Rows[i]["al_DOJ"]).ToString("dd/MMM/yyyy");
                                    else
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = "";
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_Serv_Length"];
                                    if (dtExcel.Rows[i]["al_Last_Prm_Date"].ToString() != "")
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToDateTime(dtExcel.Rows[i]["al_Last_Prm_Date"]).ToString("dd/MMM/yyyy");
                                    else
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = "";                                    
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_LOS_In_PresDesig"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_state_code"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_branch_Name"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_Lvl1_Name"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_Lvl2_Name"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_Lvl3_Name"];
                                    int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["al_Month_SlNo"]);
                                   
                                    iColumnCounter = (7 * iMonthNo) +7;
                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 6) + "2");
                                    rgHead.Cells.Value2 = dtExcel.Rows[i]["al_doc_month"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_pers_pmd"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_pers_points"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_Pers_Cust"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_groups"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_group_pmd"];                                    
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_group_points"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_group_Cust"];

                                    iColumnCounter = iTotColumns - 11;
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_pers_work_months"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_pers_Tot_pmd"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_pers_Tot_points"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_Pers_Tot_Cust"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_group_work_months"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_Tot_Groups"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_group_Tot_points"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_group_Tot_pmd"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_Avg_PMD"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_Avg_Pnts"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_Pnts_Per_Head"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_Avg_Cust"];

                                }
                            }
                            else
                            {
                                worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 3;
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_eora_code"];
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_eora_name"];
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_Pres_Desig"];
                                if (dtExcel.Rows[i]["al_DOJ"].ToString() != "")
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToDateTime(dtExcel.Rows[i]["al_DOJ"]).ToString("dd/MMM/yyyy");
                                else
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = "";
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_Serv_Length"];
                                if (dtExcel.Rows[i]["al_Last_Prm_Date"].ToString() != "")
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToDateTime(dtExcel.Rows[i]["al_Last_Prm_Date"]).ToString("dd/MMM/yyyy");
                                else
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = "";
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_LOS_In_PresDesig"];
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_state_code"];
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_branch_Name"];                                
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_Lvl1_Name"];
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_Lvl2_Name"];
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_Lvl3_Name"];

                                int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["al_Month_SlNo"]);
                                //int iStartColumn = 0;
                                iColumnCounter = (7 * iMonthNo) +7;
                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 6) + "2");
                                rgHead.Cells.Value2 = dtExcel.Rows[i]["al_doc_month"];
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_pers_pmd"];
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_pers_points"];
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_Pers_Cust"];
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_groups"];
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_group_pmd"];                                
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_group_points"];
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_group_Cust"];

                                iColumnCounter = iTotColumns - 11;
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_pers_work_months"];
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_pers_Tot_pmd"];
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_pers_Tot_points"];
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_Pers_Tot_Cust"];
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_group_work_months"];
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_Tot_Groups"];
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_group_Tot_points"];
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_group_Tot_pmd"];
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_Avg_PMD"];
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_Avg_Pnts"];
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_Pnts_Per_Head"];
                                worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["al_Avg_Cust"];
                            }

                            iColumnCounter = 1;
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

        private void txtFrmPntsPerGrp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtToPntsPerGrp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtFrmPntsPerHead_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtToPntsPerHead_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private bool CheckData()
        {
            bool flag = true;
            if (chkMonths.Checked == true)
            {
                if (txFrmMnths.Text == ""  || txtToMnths.Text == "" )
                {
                    flag = false;
                    MessageBox.Show("Please Enter Months Range","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    txFrmMnths.Focus();
                    return flag;
                }
            }
            if (chkGrps.Checked == true)
            {
                if (txtFrmGrps.Text == ""  || txtToGrps.Text == "")
                {
                    flag = false;
                    MessageBox.Show("Please Enter Groups Range", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtFrmGrps.Focus();
                    return flag;
                }
            }
            return flag;
        }


        private void btnReport_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {
                GetSelectedValues();

                CommonData.ViewReport = "SSERP_REP_LIST_OF_LOW_PERFORMERS";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, Convert.ToInt32(cbEcode.SelectedValue), dtpFromDoc.Value.ToString("MMMyyyy").ToUpper(),
                                           dtpToDoc.Value.ToString("MMMyyyy").ToUpper(), sMonthsFlag, frmMnths, ToMnths, sGrpsFlag, frmGrps, ToGrps, sPntsFlag, frmPersPoints,
                                           ToPersPoints, sPntsPerGrpFlag, frmPntsPerGrp, ToPntsPerGrp, sPntsPerHeadFlag, frmPntsPerHead, ToPntsPerHead, cbSortBy.Text, "REPORT",
                                           sPntsGrpHeadFlag,Convert.ToDateTime(dtpLOSAsOnDate.Value).ToString("dd/MMM/yyyy"));
                objReportview.Show();
            }


        }


        private void chkPntsPerHead_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPntsPerGrp.Checked == true & chkPntsPerHead.Checked==true)
            {
                cbSelection.Visible = true;
                cbSelection.SelectedIndex = 0;
            }
           else if (chkPntsPerGrp.CheckState == CheckState.Unchecked || chkPntsPerHead.CheckState == CheckState.Unchecked)
            {               
                txtFrmPntsPerHead.Text = "";
                txtToPntsPerHead.Text = "";
                cbSelection.Visible = false;
            }

            else
            {
                cbSelection.Visible = false;
            }

        }

        

        private void chkPntsPerGrp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPntsPerGrp.Checked == true & chkPntsPerHead.Checked==true)
            {
                cbSelection.Visible = true;
                cbSelection.SelectedIndex = 0;

            }
           else if (chkPntsPerGrp.CheckState == CheckState.Unchecked || chkPntsPerHead.CheckState == CheckState.Unchecked)
            {
                txtFrmPntsPerGrp.Text = "";                
                txtToPntsPerGrp.Text = "";
                cbSelection.Visible = false;
                
            }

            else
            {
                cbSelection.Visible = false; 
            }


        }

        private void cbSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSelection.SelectedIndex == 0)
            {
                sPntsGrpHeadFlag = "AND";
            }
            else
            {
                sPntsGrpHeadFlag = "OR";
            }
        }

      

      
    }
}
