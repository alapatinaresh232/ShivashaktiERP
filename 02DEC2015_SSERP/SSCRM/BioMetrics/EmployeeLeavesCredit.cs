using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SSCRMDB;

namespace SSCRM
{
    public partial class EmployeeLeavesCredit : Form
    {
        private SQLDB objData = null;
        private bool flagUpdate = false;
        private string strFormType = "";

        public EmployeeLeavesCredit()
        {
            InitializeComponent();
        }

        public EmployeeLeavesCredit(string sFrmType)
        {
            InitializeComponent();
            strFormType = sFrmType;
        }

        private void EmployeeLeavesCredit_Load(object sender, EventArgs e)
        {
            //FillLeavesDetailsToGird();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        private void txtEcodeSearch_TextChanged(object sender, EventArgs e)
        {
            objData = new SQLDB();
            DataTable dt = new DataTable();
            String strCmd = "";
            if (txtEcodeSearch.Text.Length > 0)
            {
                try
                {
                    if (strFormType == "HO")
                    {
                         strCmd = "SELECT EM.BRANCH_CODE,EM.MEMBER_NAME,EM.DESIG,EM.COMPANY_CODE,"
                                        + "CM_COMPANY_NAME,BM.BRANCH_NAME,dept_name FROM EORA_MASTER EM "
                                        + "INNER JOIN COMPANY_MAS ON CM_COMPANY_CODE=COMPANY_CODE INNER JOIN "
                                        + "BRANCH_MAS BM ON BM.BRANCH_CODE=EM.BRANCH_CODE INNER JOIN Dept_Mas "
                                        + "ON dept_code=DEPT_ID WHERE ecode=" + Convert.ToInt32(txtEcodeSearch.Text);
                    }
                    else
                    {
                        strCmd = "SELECT EM.BRANCH_CODE,EM.MEMBER_NAME,EM.DESIG,EM.COMPANY_CODE,"
                                        + "CM_COMPANY_NAME,BM.BRANCH_NAME,dept_name FROM EORA_MASTER EM "
                                        + "INNER JOIN COMPANY_MAS ON CM_COMPANY_CODE=COMPANY_CODE INNER JOIN "
                                        + "BRANCH_MAS BM ON BM.BRANCH_CODE=EM.BRANCH_CODE INNER JOIN Dept_Mas "
                                        + "ON dept_code=DEPT_ID WHERE ecode=" + Convert.ToInt32(txtEcodeSearch.Text) +
                                        " and EM.BRANCH_CODE='"+ CommonData.BranchCode +"'";
                    }

                    dt = objData.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtName.Text = dt.Rows[0]["MEMBER_NAME"] + "";
                        txtCompany.Text = dt.Rows[0]["CM_COMPANY_NAME"] + "";
                        txtCompany.Tag = dt.Rows[0]["COMPANY_CODE"] + "";
                        txtBranch.Text = dt.Rows[0]["BRANCH_NAME"] + "";
                        txtBranch.Tag = dt.Rows[0]["BRANCH_CODE"] + "";
                        txtDept.Text = dt.Rows[0]["dept_name"] + "";
                        txtDesig.Text = dt.Rows[0]["DESIG"] + "";

                        strCmd = "SELECT * FROM HR_EMPLOYEE_LEAVE_CREDITS WHERE HELC_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text)+
                            " and helc_leave_year='"+Convert.ToDateTime(CommonData.CurrentDate).Year
                            +"' ORDER BY HELC_LEAVE_YEAR desc";
                        dt = objData.ExecuteDataSet(strCmd).Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            FillLeavesDetailsToGirdNextTimeOnwards(dt);
                        }
                        else if (dt.Rows.Count == 0)
                        {
                            FillLeavesDetailsToGirdFirstTime();
                        }
                    }
                    else
                    {
                        txtName.Text = string.Empty;
                        txtCompany.Text = string.Empty;
                        txtCompany.Tag = string.Empty;
                        txtBranch.Text = string.Empty;
                        txtBranch.Tag = string.Empty;
                        txtDept.Text = string.Empty;
                        txtDesig.Text = string.Empty;
                        gvLeaveDetl.Rows.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    dt = null;
                    objData = null;
                }
            }
        }
        
        private void gvLeaveDetl_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvLeaveDetl.Columns["Opening"].Index || e.ColumnIndex == gvLeaveDetl.Columns["Availed"].Index)
            {
                if (Convert.ToString(gvLeaveDetl.Rows[e.RowIndex].Cells["Opening"].Value) != "" && Convert.ToString(gvLeaveDetl.Rows[e.RowIndex].Cells["Availed"].Value) != "")
                {
                    if (Convert.ToDouble(gvLeaveDetl.Rows[e.RowIndex].Cells["Opening"].Value) >= 0 && Convert.ToDouble(gvLeaveDetl.Rows[e.RowIndex].Cells["Availed"].Value) >= 0)
                    {
                        gvLeaveDetl.Rows[e.RowIndex].Cells["Balance"].Value = (Convert.ToDouble(gvLeaveDetl.Rows[e.RowIndex].Cells["Opening"].Value) - Convert.ToDouble(gvLeaveDetl.Rows[e.RowIndex].Cells["Availed"].Value));
                    }
                }
                if (Convert.ToString(gvLeaveDetl.Rows[e.RowIndex].Cells["Opening"].Value) != "" && Convert.ToString(gvLeaveDetl.Rows[e.RowIndex].Cells["Availed"].Value) != "")
                {
                    if ((Convert.ToDouble(gvLeaveDetl.Rows[e.RowIndex].Cells["Opening"].Value)) < (Convert.ToDouble(gvLeaveDetl.Rows[e.RowIndex].Cells["Availed"].Value)))
                    {
                        gvLeaveDetl.Rows[e.RowIndex].Cells["Availed"].Value = gvLeaveDetl.Rows[e.RowIndex].Cells["Opening"].Value;
                        gvLeaveDetl.Rows[e.RowIndex].Cells["Balance"].Value = (Convert.ToDouble(gvLeaveDetl.Rows[e.RowIndex].Cells["Opening"].Value) - Convert.ToDouble(gvLeaveDetl.Rows[e.RowIndex].Cells["Availed"].Value));
                    
                    }
                }
                else if (Convert.ToString(gvLeaveDetl.Rows[e.RowIndex].Cells["Opening"].Value) == "")
                {
                    gvLeaveDetl.Rows[e.RowIndex].Cells["Opening"].Value = "";
                }
                else if (Convert.ToString(gvLeaveDetl.Rows[e.RowIndex].Cells["Availed"].Value) == "")
                {
                    gvLeaveDetl.Rows[e.RowIndex].Cells["Availed"].Value = "";
                }

            }
            else if (e.ColumnIndex == gvLeaveDetl.Columns["Nullified"].Index)
            {
                if (Convert.ToString(gvLeaveDetl.Rows[e.RowIndex].Cells["Nullified"].Value) == "")
                {
                    gvLeaveDetl.Rows[e.RowIndex].Cells["Nullified"].Value = "0.0";
                }
            }
            else if (e.ColumnIndex == gvLeaveDetl.Columns["Year"].Index)
            {
                if (Convert.ToString(gvLeaveDetl.Rows[e.RowIndex].Cells["Year"].Value) != "")
                {
                    int year = Convert.ToInt32(gvLeaveDetl.Rows[e.RowIndex].Cells["Year"].Value.ToString());
                    if (year < DateTime.Today.Year)
                    {
                        MessageBox.Show("Enter Valid Year", "Employee Leaves Credit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        for (int ivar = 0; ivar < gvLeaveDetl.Rows.Count; ivar++)
                        {
                            gvLeaveDetl.Rows[ivar].Cells["Year"].Value = "";
                        }
                    }
                    else
                        for (int ivar = 0; ivar < gvLeaveDetl.Rows.Count; ivar++)
                        {
                            gvLeaveDetl.Rows[ivar].Cells["Year"].Value = gvLeaveDetl.CurrentCell.Value;
                        }
                }
                else
                {
                    for (int ivar = 0; ivar < gvLeaveDetl.Rows.Count; ivar++)
                    {
                        gvLeaveDetl.Rows[ivar].Cells["Year"].Value = "";
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtEcodeSearch.Text = string.Empty;
            //txtEcodeSearch.TextChanged += new System.EventHandler(this.txtEcodeSearch_TextChanged);
            txtName.Text = string.Empty;
            txtCompany.Text = string.Empty;
            txtBranch.Text = string.Empty;
            txtDept.Text = string.Empty;
            txtDesig.Text = string.Empty;
            gvLeaveDetl.Rows.Clear();
            flagUpdate = false;
        }

        private void txtEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }
        private void FillLeavesDetailsToGirdFirstTime()
        {
            DataTable dt = new DataTable();
            try
            {
                String strCommand = "SELECT HLTM_LEAVE_TYPE,HLTM_LEAVE_TYPE_DESC FROM HR_LEAVE_TYPE_MASTER order by HLTM_LEAVE_TYPE asc";
                objData = new SQLDB();

                dt = objData.ExecuteDataSet(strCommand).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            gvLeaveDetl.Rows.Clear();
            int j = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                    //DataGridViewRow tempRow = new DataGridViewRow();
                    //DataGridViewCell cellSNo = new DataGridViewTextBoxCell();
                    //cellSNo.Value = (i + 1).ToString();
                    //tempRow.Cells.Add(cellSNo);

                    //DataGridViewCell cellDate = new DataGridViewTextBoxCell();
                    //cellDate.Value = "";
                    //tempRow.Cells.Add(cellDate);
                  
                    //DataGridViewCell cellLeveType = new DataGridViewTextBoxCell();
                    //cellLeveType.Value = dt.Rows[i]["HLTM_LEAVE_TYPE"].ToString();
                    //tempRow.Cells.Add(cellLeveType);

                    //DataGridViewCell cellOpening = new DataGridViewTextBoxCell();
                    //cellOpening.Value = "";
                    //tempRow.Cells.Add(cellOpening);

                    //DataGridViewCell cellAvailed = new DataGridViewTextBoxCell();
                    //cellAvailed.Value = "";
                    //tempRow.Cells.Add(cellAvailed);

                    //DataGridViewCell cellOBalance = new DataGridViewTextBoxCell();
                    //cellOBalance.Value = "";
                    //tempRow.Cells.Add(cellOBalance);

                    //DataGridViewCell cellNullified = new DataGridViewTextBoxCell();
                    //cellNullified.Value = "0";
                    //tempRow.Cells.Add(cellNullified);
                    //gvLeaveDetl.Rows.Add(tempRow);
                    string strLeaveType = dt.Rows[i]["HLTM_LEAVE_TYPE"].ToString();
               
                    if (strLeaveType.Length.Equals(2))
                    {
                        

                        gvLeaveDetl.Rows.Add();

                        gvLeaveDetl.Rows[j].Cells["SLNO"].Value = (j+1).ToString();
                       
                        gvLeaveDetl.Rows[j].Cells["Year"].Value = DateTime.Today.Year;


                        gvLeaveDetl.Rows[j].Cells["LeaveType"].Value = strLeaveType;
                        gvLeaveDetl.Rows[j].Cells["Nullified"].Value = "0";

                        gvLeaveDetl.Rows[j].Cells["Opening"].Value = "";
                        gvLeaveDetl.Rows[j].Cells["Availed"].Value = "";
                        gvLeaveDetl.Rows[j].Cells["Balance"].Value = "";
                        j = j + 1;
                        
                        
                    }
                        
                    
                }

          
            

        }

        private void FillLeavesDetailsToGirdNextTimeOnwards(DataTable dt)
        {
            if (dt.Rows.Count >0)
            {
                flagUpdate = true;
            }
            
            try
            {
               
                gvLeaveDetl.Rows.Clear();
                for (int iVar = 0; iVar < dt.Rows.Count; iVar++)
                {
                    gvLeaveDetl.Rows.Add();
                    
                    gvLeaveDetl.Rows[iVar].Cells["SLNO"].Value = (iVar + 1).ToString();
                    gvLeaveDetl.Rows[iVar].Cells["Year"].Value = dt.Rows[iVar]["HELC_LEAVE_YEAR"].ToString();
                    gvLeaveDetl.Rows[iVar].Cells["LeaveType"].Value = dt.Rows[iVar]["HELC_LEAVE_TYPE"].ToString();
                    gvLeaveDetl.Rows[iVar].Cells["Opening"].Value = dt.Rows[iVar]["HELC_OPENING_LEAVES"].ToString();
                    gvLeaveDetl.Rows[iVar].Cells["Availed"].Value = dt.Rows[iVar]["HELC_AVAILED_LEAVES"].ToString();
                    gvLeaveDetl.Rows[iVar].Cells["Balance"].Value = dt.Rows[iVar]["HELC_BALANCE_LEAVES"].ToString();
                    gvLeaveDetl.Rows[iVar].Cells["Nullified"].Value = dt.Rows[iVar]["HELC_ALLOWED_LEAVES"].ToString();
                }
              
                    foreach (DataGridViewRow row in gvLeaveDetl.Rows)
                    {
                        row.ReadOnly = true;
                    }
                
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                dt = null;
                objData = null;
            }
        }

        private void gvLeaveDetl_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox tBox = e.Control as TextBox;
            if (gvLeaveDetl.CurrentCell.ColumnIndex == gvLeaveDetl.Columns["Year"].Index)
            {
                if (tBox == null)
                {
                    return;
                }
                tBox.TextChanged += tBox_TextChanged;
                tBox.KeyPress += tBox_KeyPress;
            }
            else if (gvLeaveDetl.CurrentCell.ColumnIndex == gvLeaveDetl.Columns["Opening"].Index || gvLeaveDetl.CurrentCell.ColumnIndex == gvLeaveDetl.Columns["Availed"].Index)
            {
                if (tBox == null)
                {
                    return;
                }
                tBox.KeyPress += tBox_KeyPress;
            }
        }
        void tBox_TextChanged(object sender, EventArgs e)
        {
            TextBox tBox = (TextBox)sender;
            if (tBox.Text.Length > 4)
            {
                tBox.Text = tBox.Text.Remove(tBox.Text.Length - 1, 1);
            }
        }
        void tBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46) )
            {
                e.Handled = true;
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int iRes = 0;

                if (CheckData())
                {
                    try
                    {
                        objData = new SQLDB();
                        string strCmd = "";

                        if (flagUpdate)
                        {
                            for (int iVar = 0; iVar < gvLeaveDetl.Rows.Count; iVar++)
                            {
                                strCmd += "update HR_EMPLOYEE_LEAVE_CREDITS set HELC_LEAVE_YEAR=" + Convert.ToInt32(gvLeaveDetl.Rows[iVar].Cells["Year"].Value.ToString())
                                                   + ",HELC_OPENING_LEAVES=" + Convert.ToDouble(gvLeaveDetl.Rows[iVar].Cells["Opening"].Value.ToString())
                                                   + ",HELC_AVAILED_LEAVES=" + Convert.ToDouble(gvLeaveDetl.Rows[iVar].Cells["Availed"].Value.ToString())
                                                   + ",HELC_BALANCE_LEAVES=" + Convert.ToDouble(gvLeaveDetl.Rows[iVar].Cells["Balance"].Value.ToString())
                                                   + ",HELC_ALLOWED_LEAVES=" + Convert.ToDouble(gvLeaveDetl.Rows[iVar].Cells["Nullified"].Value.ToString())
                                                   + ",HELC_MODIFIED_BY='" + CommonData.LogUserId
                                                   + "',HELC_MODIFIED_DATE='" + CommonData.CurrentDate + "' where HELC_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) + " and HELC_LEAVE_TYPE='" + gvLeaveDetl.Rows[iVar].Cells["LeaveType"].Value + "'";
                            }
                            flagUpdate = false;
                        }
                        else
                        {
                            for (int iVar = 0; iVar < gvLeaveDetl.Rows.Count; iVar++)
                            {
                                strCmd += "INSERT INTO HR_EMPLOYEE_LEAVE_CREDITS(HELC_COMPANY_CODE,"
                                                    + "HELC_BRANCH_CODE,"
                                                    + "HELC_EORA_CODE,"
                                                    + "HELC_LEAVE_TYPE,"
                                                    + "HELC_LEAVE_YEAR,"
                                                    + "HELC_OPENING_LEAVES,"
                                                    + "HELC_AVAILED_LEAVES,"
                                                    + "HELC_BALANCE_LEAVES,"
                                                    + "HELC_ALLOWED_LEAVES,"
                                                    + "HELC_CREATED_BY,"
                                                    + "HELC_CREATED_DATE) "
                                                    + "VALUES('" + txtCompany.Tag
                                                    + "','" + txtBranch.Tag
                                                    + "'," + Convert.ToInt32(txtEcodeSearch.Text)
                                                   + ",'" + gvLeaveDetl.Rows[iVar].Cells["LeaveType"].Value.ToString()
                                                   + "'," + Convert.ToInt32(gvLeaveDetl.Rows[iVar].Cells["Year"].Value.ToString())
                                                   + "," + Convert.ToDouble(gvLeaveDetl.Rows[iVar].Cells["Opening"].Value.ToString())
                                                   + "," + Convert.ToDouble(gvLeaveDetl.Rows[iVar].Cells["Availed"].Value.ToString())
                                                   + "," + Convert.ToDouble(gvLeaveDetl.Rows[iVar].Cells["Balance"].Value.ToString())
                                                   + "," + Convert.ToDouble(gvLeaveDetl.Rows[iVar].Cells["Nullified"].Value.ToString())
                                                   + ",'" + CommonData.LogUserId
                                                   + "',getdate())";
                            }
                        }


                        iRes = objData.ExecuteSaveData(strCmd);
                       
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {

                        objData = null;
                    }
                    if (iRes > 0)
                    {
                        MessageBox.Show("Data Saved Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Data not Saved", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnCancel_Click(null, null);
                    }
                }
           
        }
        private bool CheckData()
        {
            bool flag = true;
            if (txtName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Enter Valid Ecode", "Employee Leaves Credit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEcodeSearch.Focus();
            }
            else if (gvLeaveDetl.Rows.Count > 0)
            {
                for (int iVar = 0; iVar < gvLeaveDetl.Rows.Count; iVar++)
                {
                   
                        if (gvLeaveDetl.Rows[iVar].Cells["Year"].Value.ToString().Length == 0)
                        {
                            flag = false;
                            MessageBox.Show("Enter the Year ", "Employee Leaves Credit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return flag;
                        }
                        else if (gvLeaveDetl.Rows[iVar].Cells["Opening"].Value.ToString().Length == 0)
                        {
                            flag = false;
                            MessageBox.Show("Enter the Opening Leaves ", "Employee Leaves Credit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return flag;
                        }
                        else if (gvLeaveDetl.Rows[iVar].Cells["Availed"].Value.ToString().Length == 0)
                        {
                            flag = false;
                            MessageBox.Show("Enter the Availed Leaves ", "Employee Leaves Credit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return flag;
                        }
                    
                }
            }
            return flag;
        }
    }
}
