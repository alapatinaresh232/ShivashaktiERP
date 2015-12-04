using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRM;
using SSAdmin;
using SSCRMDB;
using System.Collections;
using SSTrans;
using SSCRM.App_Code;

namespace SSCRM
{
    public partial class frmTMAndAbovePMD : Form
    {
        SQLDB objSQLdb = null;
        private InvoiceDB objInvoiceDB = null;
        private int intCurrentRow = 0;
        private int intCurrentCell = 0;
        bool flagText = false;
        //bool flagUpdate = false;
        public frmTMAndAbovePMD()
        {
            InitializeComponent();
        }

        private void frmTMAndAbovePMD_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            dtpDocmonth.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.DocMonth).ToString("MMMyyyy").ToUpper());
        }
        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();

            try
            {
                string strCmd = "SELECT DISTINCT CM_COMPANY_NAME ,CM_COMPANY_CODE " +
                                   " FROM USER_BRANCH " +
                                   " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                   " INNER JOIN COMPANY_MAS ON CM_COMPANY_CODE = COMPANY_CODE " +
                                   " WHERE UB_USER_ID ='" + CommonData.LogUserId +
                                   "' ORDER BY CM_COMPANY_NAME";

                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbComp.DataSource = dt;
                    cbComp.DisplayMember = "CM_COMPANY_NAME";
                    cbComp.ValueMember = "CM_COMPANY_CODE";
                }

                cbComp.SelectedValue = CommonData.CompanyCode;
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


        private void FillBranchData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();

            try
            {
                if (cbComp.SelectedIndex > 0)
                {
                    string strCommand = "SELECT DISTINCT BRANCH_NAME,BRANCH_CODE " +
                                        " FROM USER_BRANCH " +
                                        " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                        " WHERE COMPANY_CODE ='" + cbComp.SelectedValue.ToString() +
                                        "' AND UB_USER_ID ='" + CommonData.LogUserId +
                                        "' ORDER BY BRANCH_NAME ASC";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbBranch.DataSource = dt;
                    cbBranch.DisplayMember = "BRANCH_NAME";
                    cbBranch.ValueMember = "BRANCH_CODE";

                }

                //cbBranch.SelectedValue = CommonData.BranchCode;
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

        private void cbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbComp.SelectedIndex>0 && cbBranch.SelectedIndex > 0)
            {
                GetTMBindData();
            }
            else
            {
                cbBranch.DataSource = null;
                gvTMDetails.Rows.Clear();
            }
        }
        public void GetTMBindData()
        {
            gvTMDetails.Rows.Clear();
            Hashtable ht = new Hashtable();
            try
            {
                objInvoiceDB = new InvoiceDB();
                ht = objInvoiceDB.GetSalesDmTMAndAboveData(cbComp.SelectedValue.ToString(),cbBranch.SelectedValue.ToString(),dtpDocmonth.Value.ToString("MMMyyyy"));
                int intRow = 1;
                DataTable dt = (DataTable)ht["SalseDM"];
                gvTMDetails.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataGridViewRow tempRow = new DataGridViewRow();
                    DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                    cellSLNO.Value = intRow;
                    tempRow.Cells.Add(cellSLNO);

                    DataGridViewCell cellEoraEcode = new DataGridViewTextBoxCell();
                    cellEoraEcode.Value = dt.Rows[i]["sr_eora_code"];
                    tempRow.Cells.Add(cellEoraEcode);

                    DataGridViewCell cellEmpName = new DataGridViewTextBoxCell();
                    cellEmpName.Value = dt.Rows[i]["sr_emp_name"];
                    tempRow.Cells.Add(cellEmpName);

                    DataGridViewCell cellDesigName = new DataGridViewTextBoxCell();
                    cellDesigName.Value = dt.Rows[i]["sr_desig_name"];
                    tempRow.Cells.Add(cellDesigName);

                    DataGridViewCell cellGroups = new DataGridViewTextBoxCell();
                    cellGroups.Value = dt.Rows[i]["sr_groups"];
                    tempRow.Cells.Add(cellGroups);


                    DataGridViewCell cellSDSD_PMD = new DataGridViewTextBoxCell();
                    cellSDSD_PMD.Value = dt.Rows[i]["sr_PMD"];
                    tempRow.Cells.Add(cellSDSD_PMD);

                    DataGridViewCell cellSDSD_DA_DAYS = new DataGridViewTextBoxCell();
                    cellSDSD_DA_DAYS.Value = dt.Rows[i]["sr_DA_DAYS"];
                    tempRow.Cells.Add(cellSDSD_DA_DAYS);

                    DataGridViewCell cellSDSD_DEMOS = new DataGridViewTextBoxCell();
                    cellSDSD_DEMOS.Value = dt.Rows[i]["sr_DEMOS"];
                    tempRow.Cells.Add(cellSDSD_DEMOS);


                    DataGridViewCell cellTm_groupName = new DataGridViewTextBoxCell();
                    cellTm_groupName.Value = dt.Rows[i]["sr_group_name"];
                    tempRow.Cells.Add(cellTm_groupName);


                    DataGridViewCell cellTm_state_code = new DataGridViewTextBoxCell();
                    cellTm_state_code.Value = dt.Rows[i]["sr_state_code"];
                    tempRow.Cells.Add(cellTm_state_code);

                    DataGridViewCell cellfinyear = new DataGridViewTextBoxCell();
                    cellfinyear.Value = dt.Rows[i]["sr_fin_year"];
                    tempRow.Cells.Add(cellfinyear);

                    DataGridViewCell cellFalgeSatus = new DataGridViewTextBoxCell();
                    cellFalgeSatus.Value = dt.Rows[i]["sr_flag_status"];
                    tempRow.Cells.Add(cellFalgeSatus);

                    intRow = intRow + 1;
                    gvTMDetails.Rows.Add(tempRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objInvoiceDB = null;
            }
        }
        private bool CheckData()
        {
            bool flag = true;
            if (cbComp.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Company", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbComp.Focus();
                return flag;

            }
            if (cbBranch.SelectedIndex == 0 || cbBranch.SelectedIndex == -1)
            {
                flag = false;
                MessageBox.Show("Please Select Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbBranch.Focus();
                return flag;

            }           
            if (gvTMDetails.Rows.Count == 0)
            {
                MessageBox.Show("Enter Atleast One Record to Save details", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }          
          
            return flag;
        }
        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46) && (flagText == false))
            {
                e.Handled = true;
                return;
            }

            //to allow decimals only teak plant
            if (intCurrentCell == 3 && e.KeyChar == 46)
            {
                e.Handled = true;
                return;
            }
             //checks to make sure only 1 decimal is allowed
            else if (e.KeyChar == 46 && flagText == false)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
         {
            objSQLdb = new SQLDB();

            int iretval = 0;
            try
            {
                if (CheckData() == true)
                {

                    string strIns = "";
                  
                    //objDA.ExecuteSaveData(strSQL);
                    //strSQL = "SELECT SDSD_EORA_CODE FROM SALES_DM_SR_DATA WHERE sdsd_company_code='" + CommonData.CompanyCode + "' and sdsd_branch_code='" + CommonData.BranchCode + "' and sdsd_document_month='" + CommonData.DocMonth + "'";
                    //DataView dvFil = objDA.ExecuteDataSet(strSQL).Tables[0].DefaultView;

                    for (int i = 0; i < gvTMDetails.Rows.Count; i++)
                    {
                        string iPMD = gvTMDetails.Rows[i].Cells["PMD"].Value.ToString() == "" ? "0" : gvTMDetails.Rows[i].Cells["PMD"].Value.ToString();
                        string iDA = gvTMDetails.Rows[i].Cells["DAdays"].Value.ToString() == "" ? "0" : gvTMDetails.Rows[i].Cells["DAdays"].Value.ToString();
                        string iDemos = gvTMDetails.Rows[i].Cells["Demos"].Value.ToString() == "" ? "0" : gvTMDetails.Rows[i].Cells["Demos"].Value.ToString();

                        if (gvTMDetails.Rows[i].Cells["FlgStatus"].Value.ToString() == "FALSE")
                        {
                             strIns += " INSERT INTO SALES_DM_SR_DATA(SDSD_COMPANY_CODE " +
                                                                    ",SDSD_STATE_CODE " +
                                                                    ",SDSD_BRANCH_CODE," +
                                                                    " SDSD_FIN_YEAR " +
                                                                    ",SDSD_DOCUMENT_MONTH " +
                                                                    ",SDSD_GROUP_NAME " +
                                                                    ",SDSD_GROUP_LEAD_ECODE " +
                                                                    ",SDSD_EORA_CODE " +
                                                                    ",SDSD_PMD" +
                                                                    ",SDSD_DA_DAYS " +
                                                                    ",SDSD_DEMOS " +
                                                                    ",SDSD_CREATED_BY " +
                                                                    ",SDSD_AUTHORIZED_BY " +
                                                                    ",SDSD_CREATED_DATE) " +
                                                                    "VALUES('" + cbComp.SelectedValue.ToString() +
                                                                    "','" + gvTMDetails.Rows[i].Cells["StateCode"].Value +
                                                                    "','" + cbBranch.SelectedValue.ToString() +
                                                                    "','" + CommonData.FinancialYear +
                                                                    "','" + dtpDocmonth.Value.ToString("MMMyyyy") +
                                                                    "','" + gvTMDetails.Rows[i].Cells["GroupName"].Value +
                                                                    "'," + gvTMDetails.Rows[i].Cells["Ecode"].Value +
                                                                    "," + gvTMDetails.Rows[i].Cells["Ecode"].Value +
                                                                    "," + iPMD +
                                                                    "," + iDA +
                                                                    "," + iDemos +
                                                                    ",'" + CommonData.LogUserId +
                                                                    "','" + CommonData.LogUserId +
                                                                    "','" + CommonData.CurrentDate + "')";
                       
                        }

                        else
                        {
                            strIns += "UPDATE SALES_DM_SR_DATA SET SDSD_PMD='" + iPMD +
                                                                "',SDSD_DA_DAYS='" + iDA +
                                                                "',SDSD_DEMOS='" + iDemos +
                                                                "',SDSD_GROUP_NAME='" + gvTMDetails.Rows[i].Cells["GroupName"].Value +
                                                                "',SDSD_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                                                                "',SDSD_LAST_MODIFIED_DATE='" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                                                "' WHERE SDSD_COMPANY_CODE ='" + cbComp.SelectedValue.ToString() +
                                                                "' AND SDSD_STATE_CODE ='" + gvTMDetails.Rows[i].Cells["StateCode"].Value.ToString() +
                                                                "' AND SDSD_BRANCH_CODE= '" + cbBranch.SelectedValue.ToString() +
                                                                "' AND SDSD_FIN_YEAR='" + gvTMDetails.Rows[i].Cells["FinYear"].Value.ToString() +
                                                                "' AND SDSD_GROUP_NAME='" + gvTMDetails.Rows[i].Cells["GroupName"].Value.ToString() +
                                                                "' AND SDSD_GROUP_LEAD_ECODE='" + gvTMDetails.Rows[i].Cells["Ecode"].Value.ToString() +
                                                                "' AND SDSD_EORA_CODE='" + gvTMDetails.Rows[i].Cells["Ecode"].Value.ToString() +
                                                                "' AND SDSD_DOCUMENT_MONTH= '" + Convert.ToDateTime(dtpDocmonth.Value).ToString("MMMyyyy") + "'";

                        }
                    }

                    iretval = objSQLdb.ExecuteSaveData(strIns);                 
                }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("Please enter valid numbers", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (iretval > 0)
            {  
                objSQLdb = null;
                    MessageBox.Show("Data saved successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  btnCancel_Click(null, null);
                  GetTMBindData();                             
            }
            
        }

    
        private void cbComp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbComp.SelectedIndex > 0)
            {
                FillBranchData();
            }
            else
            {
                cbBranch.DataSource = null;
                gvTMDetails.Rows.Clear();
            }
        }

        private void dtpDocmonth_ValueChanged(object sender, EventArgs e)
        {
            if (cbComp.SelectedIndex > 0 && cbBranch.SelectedIndex > 0)
            {
                GetTMBindData();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //cbComp.SelectedIndex = 0;
            //cbBranch.SelectedIndex = -1;
            cbComp.SelectedValue = CommonData.CompanyCode;
            cbBranch.SelectedValue = CommonData.BranchCode;
            //flagUpdate = false;
            //dtpDocmonth.Value = DateTime.Now;
            dtpDocmonth.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.DocMonth).ToString("MMMyyyy").ToUpper());
            if (cbComp.SelectedIndex > 0 && cbBranch.SelectedIndex > 0)
            {
                GetTMBindData();
            }
            else
            {
                gvTMDetails.Rows.Clear();
            }
        }

        private void gvProductDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //blIsCellQty = true;
            intCurrentRow = (((System.Windows.Forms.DataGridView)(sender))).CurrentCell.RowIndex;
            intCurrentCell = (((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex;
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 3)
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    flagText = false;
                    txtQty.MaxLength = 6;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
           
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 5)
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    flagText = false;
                    txtQty.MaxLength = 6;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 6)
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    flagText = false;
                    txtQty.MaxLength = 6;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }

            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 7)
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    flagText = false;
                    txtQty.MaxLength = 10;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            int iResult = 0;
            string strIns = "";

            if (cbComp.SelectedIndex > 0 && cbBranch.SelectedIndex > 0)
            {
                DialogResult result = MessageBox.Show("Do you want to delete This Record ?",
                                    "Tm And Above PMD", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        for (int i = 0; i < gvTMDetails.Rows.Count; i++)
                        {
                            if (gvTMDetails.Rows[i].Cells["FlgStatus"].Value.ToString() == "TRUE")
                            {
                                strIns += " DELETE FROM SALES_DM_SR_DATA WHERE SDSD_COMPANY_CODE='" + cbComp.SelectedValue.ToString() +
                                                                         "' AND SDSD_STATE_CODE='" + gvTMDetails.Rows[i].Cells["StateCode"].Value.ToString() +
                                                                         "' AND SDSD_BRANCH_CODE='" + cbBranch.SelectedValue.ToString() +
                                                                        // "' AND SDSD_FIN_YEAR='" + gvTMDetails.Rows[i].Cells["FinYear"].Value.ToString() +
                                                                        // "' AND SDSD_GROUP_NAME='" + gvTMDetails.Rows[i].Cells["GroupName"].Value.ToString() +
                                                                         "' AND SDSD_GROUP_LEAD_ECODE='" + gvTMDetails.Rows[i].Cells["Ecode"].Value.ToString() +
                                                                         "' AND SDSD_EORA_CODE='" + gvTMDetails.Rows[i].Cells["Ecode"].Value.ToString() +
                                                                         "' AND SDSD_DOCUMENT_MONTH='" + dtpDocmonth.Value.ToString("MMMyyyy") + "'";
                            }
                          
                        }
                        iResult = objSQLdb.ExecuteSaveData(strIns);

                        if (iResult > 0)
                        {
                            MessageBox.Show("Data Deleted Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //flagUpdate = false;
                            btnCancel_Click(null, null);
                            GetTMBindData();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
                else
                {
                    MessageBox.Show(" Data Not Deleted", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void gvTMDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 0)
            {
                try
                {
                    if (UtilityFunctions.IsNumeric(gvTMDetails.Rows[e.RowIndex].Cells["PMD"].Value.ToString()) == false)
                        gvTMDetails.Rows[e.RowIndex].Cells["PMD"].Value = "0";
                    if (UtilityFunctions.IsNumeric(gvTMDetails.Rows[e.RowIndex].Cells["DAdays"].Value.ToString()) == false)
                        gvTMDetails.Rows[e.RowIndex].Cells["DAdays"].Value = "0";
                    if (UtilityFunctions.IsNumeric(gvTMDetails.Rows[e.RowIndex].Cells["Demos"].Value.ToString()) == false)
                        gvTMDetails.Rows[e.RowIndex].Cells["Demos"].Value = "0";

                    if (Convert.ToString(gvTMDetails.Rows[e.RowIndex].Cells["PMD"].Value) != "")
                    {
                        int daysIn = System.DateTime.DaysInMonth(Convert.ToDateTime(CommonData.DocMonth).Year, Convert.ToDateTime(CommonData.DocMonth).Month);
                        if (Convert.ToInt32(gvTMDetails.Rows[e.RowIndex].Cells["PMD"].Value) > daysIn)
                        {
                            MessageBox.Show("Please enter number bellow month days", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            gvTMDetails.Rows[e.RowIndex].Cells["PMD"].Value = "0";
                        }
                    }
                    if (Convert.ToString(gvTMDetails.Rows[e.RowIndex].Cells["DAdays"].Value) != "")
                    {
                        if (Convert.ToString(gvTMDetails.Rows[e.RowIndex].Cells["PMD"].Value) == "")
                        {
                            MessageBox.Show("Please enter PMD value", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            gvTMDetails.Rows[e.RowIndex].Cells["DAdays"].Value = "0";
                        }
                        else
                        {
                            if (Convert.ToDouble(gvTMDetails.Rows[e.RowIndex].Cells["PMD"].Value) < Convert.ToDouble(gvTMDetails.Rows[e.RowIndex].Cells["DAdays"].Value))
                            {
                                MessageBox.Show("Please enter number bellow PMD value", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                gvTMDetails.Rows[e.RowIndex].Cells["DAdays"].Value = "0";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please enter valid numbers", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
