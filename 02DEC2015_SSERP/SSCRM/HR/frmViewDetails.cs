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
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
namespace SSCRM
{
    public partial class frmViewDetails : Form
    {
        public Security objSecurity;
        public HRInfo objHrInfo;

        public frmViewDetails()
        {
            InitializeComponent();
        }

        private void frmViewDetails_Load(object sender, EventArgs e)
        {
            gvPendingData.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);
            cmbType.SelectedIndex = 0;
            cmdStatus.SelectedIndex = 0;
            this.StartPosition = FormStartPosition.CenterScreen;
            objSecurity = new Security();
            DataTable dtCpy = objSecurity.GetCompanyDataSet().Tables[0];
            UtilityLibrary.PopulateControl(cmbCompany, dtCpy.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
            objSecurity = null;
            if (CommonData.LogUserId.ToUpper() != "ADMIN" && CommonData.LogUserRole != "MANAGEMENT")
            {
                cmbCompany.SelectedValue = CommonData.CompanyCode;
                cmbBranch_optional.SelectedValue = CommonData.BranchCode;
                cmbCompany.Enabled = false;
                cmbBranch_optional.Enabled = false;
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
                cmbType_SelectedIndexChanged(null, null);
            else
                gvPendingData.Rows.Clear();
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBranch_optional.SelectedIndex > 0)
                GetDataBuind();
        }
        
        public void GetDataBuind()
        {
            string sType = "", sStatus = "";
            if (cmbType.SelectedIndex == 1)
                sType = "A";
            else if (cmbType.SelectedIndex == 2)
                sType = "E";
            if (cmdStatus.SelectedIndex == 1)
                sStatus = "P";
            else if (cmdStatus.SelectedIndex == 2)
                sStatus = "W";
            else if (cmdStatus.SelectedIndex == 3)
                sStatus = "L";
            else if (cmdStatus.SelectedIndex == 4)
                sStatus = "R";
            else
                sStatus = "";
            DataTable dt = new DataTable();
            try
            {                
                objHrInfo = new HRInfo();
                dt = objHrInfo.HR_EmpViewDetails(cmbCompany.SelectedValue.ToString(), cmbBranch_optional.SelectedValue.ToString(), sType, sStatus).Tables[0];                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objHrInfo = null;
            }
            
            //string sFilter = "";
            //if (cmbType.SelectedIndex == 0)
            //    sFilter += " HAMH_EORA_TYPE in('E','A') ";
            //else if (cmbType.SelectedIndex == 1)
            //    sFilter += " HAMH_EORA_TYPE in('A')";
            //else
            //    sFilter += " HAMH_EORA_TYPE in('E')";
            //dvlist.RowFilter = sFilter;
            //DataTable dt = dvlist.ToTable();

            int intRow = 1;
            gvPendingData.Rows.Clear();
            lblTotal.Text = "Total Records: " + dt.Rows.Count;
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataGridViewRow tempRow = new DataGridViewRow();
                    DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                    cellSLNO.Value = intRow;
                    tempRow.Cells.Add(cellSLNO);

                    DataGridViewCell cellcApplNo = new DataGridViewTextBoxCell();
                    cellcApplNo.Value = dt.Rows[i]["HAMH_APPL_NUMBER"];
                    tempRow.Cells.Add(cellcApplNo);

                    DataGridViewCell cellcEoraNo = new DataGridViewTextBoxCell();
                    cellcEoraNo.Value = dt.Rows[i]["HAMH_EORA_CODE"];
                    tempRow.Cells.Add(cellcEoraNo);


                    DataGridViewCell cellName = new DataGridViewTextBoxCell();
                    cellName.Value = dt.Rows[i]["HAMH_NAME"];
                    tempRow.Cells.Add(cellName);

                    DataGridViewCell cellDoj = new DataGridViewTextBoxCell();
                    if (dt.Rows[i]["HAMH_DOJ"].ToString() != "")
                        cellDoj.Value = Convert.ToDateTime(dt.Rows[i]["HAMH_DOJ"]).ToString("dd-MM-yyyy");
                    else
                        cellDoj.Value = "";
                    tempRow.Cells.Add(cellDoj);

                    DataGridViewCell cellFName = new DataGridViewTextBoxCell();
                    cellFName.Value = dt.Rows[i]["HAMH_FORH_NAME"];
                    tempRow.Cells.Add(cellFName);

                    DataGridViewCell cellDesc = new DataGridViewTextBoxCell();
                    cellDesc.Value = dt.Rows[i]["DESIG"];
                    tempRow.Cells.Add(cellDesc);

                    DataGridViewCell cellsDept = new DataGridViewTextBoxCell();
                    cellsDept.Value = dt.Rows[i]["DEPT_DESC"];
                    tempRow.Cells.Add(cellsDept);

                    DataGridViewCell cellStatus = new DataGridViewTextBoxCell();
                    cellStatus.Value = dt.Rows[i]["Status"];
                    tempRow.Cells.Add(cellStatus);

                    DataGridViewCell cellBCode = new DataGridViewTextBoxCell();
                    cellBCode.Value = dt.Rows[i]["HAMH_BRANCH_CODE"];
                    tempRow.Cells.Add(cellBCode);

                    DataGridViewCell cellCCode = new DataGridViewTextBoxCell();
                    cellCCode.Value = dt.Rows[i]["HAMH_COMPANY_CODE"];
                    tempRow.Cells.Add(cellCCode);

                    intRow = intRow + 1;
                    gvPendingData.Rows.Add(tempRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvPendingData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (gvPendingData.Rows[e.RowIndex].Cells["Left"].Value.ToString().Trim() != "")
                {
                    if (gvPendingData.Rows[e.RowIndex].Cells["Left"].Value.ToString() == "No Data")
                        return;
                    else if (Convert.ToBoolean(gvPendingData.Rows[e.RowIndex].Cells["Left"].Selected) == true)
                    {
                        string CompanyCode = gvPendingData.Rows[e.RowIndex].Cells[gvPendingData.Columns["HAMH_COMPANY_CODE"].Index].Value.ToString();
                        string BranchCode = gvPendingData.Rows[e.RowIndex].Cells[gvPendingData.Columns["HAMH_BRANCH_CODE"].Index].Value.ToString();
                        string ApplNo = gvPendingData.Rows[e.RowIndex].Cells[gvPendingData.Columns["HAMH_APPL_NUMBER"].Index].Value.ToString();
                        //if (GetExistMain(Convert.ToInt32(ApplNo)) == true)
                        //{
                        frmLeftInfo chldLeftInfo = new frmLeftInfo(CompanyCode, BranchCode, Convert.ToInt32(ApplNo), gvPendingData.Rows[e.RowIndex].Cells[gvPendingData.Columns["HAMH_EORA_CODE"].Index].Value.ToString());
                        chldLeftInfo.objfrmViewDetails = this;
                        chldLeftInfo.ShowDialog();
                        //}
                    }
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string sType = "", sStatus = "";
                if (cmbType.SelectedIndex == 1)
                    sType = "A";
                else if (cmbType.SelectedIndex == 2)
                    sType = "E";
                if (cmdStatus.SelectedIndex == 1)
                    sStatus = "P";
                else if (cmdStatus.SelectedIndex == 2)
                    sStatus = "W";
                else if (cmdStatus.SelectedIndex == 3)
                    sStatus = "L";
                else if (cmdStatus.SelectedIndex == 4)
                    sStatus = "R";
                DataTable dt = new DataTable();
                try
                {
                    objHrInfo = new HRInfo();
                    dt = objHrInfo.HR_EmpViewDetails(cmbCompany.SelectedValue.ToString(), cmbBranch_optional.SelectedValue.ToString(), sType, sStatus).Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objHrInfo = null;
                }
                //folderBrowserDialog1.ShowDialog();
                //string s = folderBrowserDialog1.SelectedPath;
                Excel.Application oXL = new Excel.Application();
                Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                oXL.Visible = true;

                //Excel.Workbook theWorkbook;// = new Excel.Workbook();
                //theWorkbook = oXL.Workbooks.Open("D:\\Template.xlsx", 0, false, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", true, false, 0, true, true, true);
                //Excel.Worksheet worksheet;// = (Microsoft.Office.Interop.Excel.Worksheet)Excel.Sheets;// = new Excel.Worksheet();

                //worksheet.Name = "Employees";

                //DataRow[] rpt = dtv.Table.Rows;//ACTable.Select("Ticker <> ''", "Ticker ASC");
                Excel.Range rg = worksheet.get_Range("A1", "L1");
                Excel.Range rgData = worksheet.get_Range("A2", "L" + dt.Rows.Count.ToString());
                rgData.Font.Size = 10;
                rgData.WrapText = true;
                rgData.VerticalAlignment = Excel.Constants.xlCenter;
                rgData.Borders.Weight = 2;
                //rg.Select();
                rg.Font.Bold = true;
                rg.Font.Name = "Arial";
                rg.Font.Size = 10;
                rg.WrapText = true;
                rg.Font.ColorIndex = 2; // White Color : 2 and Red = 3,30; Green = 10,43; 
                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                rg.Interior.ColorIndex = 31;
                rg.Borders.Weight = 2;
                rg.Borders.LineStyle = Excel.Constants.xlSolid;
                rg.Cells.RowHeight = 38;

                rg = worksheet.get_Range("A1", Type.Missing);
                rg.Cells.ColumnWidth = 5;
                rg.Cells.Value2 = "Sl.No";

                rg = worksheet.get_Range("B1", Type.Missing);
                rg.Cells.ColumnWidth = 10;
                rg.Cells.Value2 = "ECODE";

                rg = worksheet.get_Range("C1", Type.Missing);
                rg.Cells.ColumnWidth = 10;
                rg.Cells.Value2 = "Employee/Agent";

                rg = worksheet.get_Range("D1", Type.Missing);
                rg.Cells.ColumnWidth = 30;
                rg.Cells.Value2 = "Name";

                rg = worksheet.get_Range("E1", Type.Missing);
                rg.Cells.ColumnWidth = 30;
                rg.Cells.Value2 = "Father/Husband";

                rg = worksheet.get_Range("F1", Type.Missing);
                rg.Cells.ColumnWidth = 30;
                rg.Cells.Value2 = "Designation";

                rg = worksheet.get_Range("G1", Type.Missing);
                rg.Cells.ColumnWidth = 30;
                rg.Cells.Value2 = "Department";

                rg = worksheet.get_Range("H1", Type.Missing);
                rg.Cells.ColumnWidth = 15;
                rg.Cells.Value2 = "Date of Birth";

                rg = worksheet.get_Range("I1", Type.Missing);
                rg.Cells.ColumnWidth = 15;
                rg.Cells.Value2 = "Date of Join";

                rg = worksheet.get_Range("J1", Type.Missing);
                rg.Cells.ColumnWidth = 20;
                rg.Cells.Value2 = "Working Status";

                rg = worksheet.get_Range("K1", Type.Missing);
                rg.Cells.ColumnWidth = 20;
                rg.Cells.Value2 = "Address";

                rg = worksheet.get_Range("L1", Type.Missing);
                rg.Cells.ColumnWidth = 20;
                rg.Cells.Value2 = "ContactNo";

                int RowCounter = 1;

                foreach (DataRow dr in dt.Rows)
                {
                    worksheet.Cells[RowCounter + 1, 1] = RowCounter;
                    worksheet.Cells[RowCounter + 1, 2] = dr["HAMH_EORA_CODE"].ToString();
                    if (dr["HAMH_EORA_TYPE"].ToString() == "E")
                        worksheet.Cells[RowCounter + 1, 3] = "EMPLOYEE";
                    else
                        worksheet.Cells[RowCounter + 1, 3] = "AGENT";
                    worksheet.Cells[RowCounter + 1, 4] = dr["HAMH_NAME"].ToString();
                    worksheet.Cells[RowCounter + 1, 5] = dr["HAMH_FORH_NAME"].ToString();
                    worksheet.Cells[RowCounter + 1, 6] = dr["DESIG"].ToString();
                    worksheet.Cells[RowCounter + 1, 7] = dr["DEPT_DESC"].ToString();
                    worksheet.Cells[RowCounter + 1, 8] = Convert.ToDateTime(dr["HAMH_DOB"]).ToString("dd/MMM/yyyy");
                    worksheet.Cells[RowCounter + 1, 9] = Convert.ToDateTime(dr["HAMH_DOJ"]).ToString("dd/MMM/yyyy");
                    worksheet.Cells[RowCounter + 1, 10] = dr["Status"].ToString();
                    worksheet.Cells[RowCounter + 1, 11] = dr["Address"].ToString();
                    worksheet.Cells[RowCounter + 1, 12] = dr["ContactNO"].ToString();
                    //worksheet.Cells[RowCounter, 5] = dr["Total"].ToString();
                    RowCounter++;
                }
                //theWorkbook.SaveAs("C:\\temp\\EmployeeDetails.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                //false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                //TextWriter twDesigncs = new StreamWriter(s + "\\EmployeeViewDetails.xlsx");
                //twDesigncs.Write(wr.ToString());
                //twDesigncs.Close();
                //string wr = "";
                //try
                //{
                //    for (int i = 0; i < dtv.Table.Columns.Count; i++)
                //    {
                //        wr += dtv.Table.Columns[i].ToString().ToUpper() + "\t";
                //    }
                //    wr += "\n";
                //    //write rows to excel file
                //    for (int i = 0; i < (dtv.Table.Rows.Count); i++)
                //    {
                //        for (int j = 0; j < dtv.Table.Columns.Count; j++)
                //        {
                //            if (dtv.Table.Rows[i][j] != null)
                //            {
                //                wr += Convert.ToString(dtv.Table.Rows[i][j]) + "\t";
                //            }
                //            else
                //            {
                //                wr += "\t";
                //            }
                //        }
                //        //go to next line
                //        wr += "\n";
                //    }
                //    //close file                
                //    TextWriter twDesigncs = new StreamWriter(s + "\\EmployeeViewDetails.xlsx");
                //    twDesigncs.Write(wr.ToString());
                //    twDesigncs.Close();
                //    MessageBox.Show("Exported successfully Path" + s + "\\EmployeeViewDetails.xlsx", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //catch (Exception ex)
                //{
                //    throw ex;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            gvPendingData.ClearSelection();
            int rowIndex = 0;
            foreach (DataGridViewRow row in gvPendingData.Rows)
            {
                if (row.Cells[2].Value.ToString().Contains(txtEcodeSearch.Text) == true)
                {
                    rowIndex = row.Index;
                    gvPendingData.CurrentCell = gvPendingData.Rows[rowIndex].Cells[2];
                    break;
                }
            }
        }

        private void cmdStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBranch_optional.SelectedIndex > 0)
                GetDataBuind();
        }
    }
}
