using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSTrans;
using SSCRMDB;

namespace SSCRM
{
    public partial class ShortageStationaryDetails : Form
    {
        private SQLDB objSQLdb = null;
        private bool IsModifyInvoice = false;
        private bool blIsCellQty = true;
        Int32 intCurrentRow = 0;
        Int32 intCurrentCell = 0;
        public ShortageStationaryDetails()
        {
            InitializeComponent();
        }

        private void ShortageStationaryDetails_Load(object sender, EventArgs e)
        {
            txtDocMonth.Text = CommonData.DocMonth;
            generateId();
            cmbBoxTransType.SelectedIndex = 0;
            gvIndentDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                     System.Drawing.FontStyle.Regular);
            TrnDate.Value = Convert.ToDateTime(CommonData.DocMonth);
            txtCompanyName.Text = CommonData.CompanyName;
            txtBranchName.Text = CommonData.BranchName;
        }
        public void generateId()
        {
            string strCommand = null;
            SQLDB objSQLdb = new SQLDB();
            DataSet ds = new DataSet();
            DataTable dt;
            try
            {

                strCommand = "SELECT ISNULL(MAX(STSH_TRN_NO),0)+1 FROM STATIONARY_SHORTAGE_HEAD" +
                               " WHERE STSH_BRANCH_CODE='" + CommonData.BranchCode + "'";
                ds = objSQLdb.ExecuteDataSet(strCommand);
                dt = ds.Tables[0];
                txtTransaction.Text = Convert.ToInt32(dt.Rows[0][0]).ToString("0");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                ds = null;
                dt = null;
                objSQLdb = null;
            }
        }

        private void btnItemsSearch_Click(object sender, EventArgs e)
        {
            StationaryItemsSearch ItemSearch = new StationaryItemsSearch("StationaryShortageDetl");
            ItemSearch.objShortageStationaryDetails = this;
            ItemSearch.ShowDialog();
        }

        private void txtConEcodeSearch_TextChanged(object sender, EventArgs e)
        {
             objSQLdb = new SQLDB();
             if (txtConEcodeSearch.Text.ToString().Trim().Length > 0)
             {
                 try
                 {
                     objSQLdb = new SQLDB();
                     DataTable dt = new DataTable();
                     dt = objSQLdb.ExecuteDataSet("SELECT MEMBER_NAME from EORA_MASTER WHERE ecode='" + txtConEcodeSearch.Text + "' ").Tables[0];

                     if (dt.Rows.Count > 0)
                         txtConcernName.Text = dt.Rows[0]["MEMBER_NAME"] + "";
                     else
                         txtConcernName.Text = "";

                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.ToString());
                 }
                 finally
                 {
                     objSQLdb = null;
                 }

             }
             else
             {

                 txtConEcodeSearch.Focus();
             }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtAppEcodeSearch_TextChanged(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            if (txtAppEcodeSearch. Text.ToString().Trim().Length > 0)
            {
                try
                {
                    objSQLdb = new SQLDB();
                    DataTable dt = new DataTable();
                    dt = objSQLdb.ExecuteDataSet("SELECT MEMBER_NAME from EORA_MASTER WHERE ecode='" + txtAppEcodeSearch.Text + "' ").Tables[0];

                    if (dt.Rows.Count > 0)
                       txtApproverEName.Text = dt.Rows[0]["MEMBER_NAME"] + "";
                    else
                        txtApproverEName.Text = "";

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSQLdb = null;
                }

            }
            else
            {

                txtAppEcodeSearch.Focus();
            }


        }
        private int SaveStationaryHeadData()
        {
            objSQLdb = new SQLDB();
            string sqlText = "";
            int intRec = 0;
           
            try
            {
                if (IsModifyInvoice == false)
                {
                    sqlText = " INSERT INTO STATIONARY_SHORTAGE_HEAD(STSH_COMPANY_CODE,STSH_STATE_CODE,STSH_BRANCH_CODE,STSH_FIN_YEAR,STSH_DOC_MONTH,STSH_TRN_TYPE," +
                             " STSH_TRN_NO,STSH_EORA_CODE,STSH_REMARKS,STSH_APPROVED_BY,STSH_DATE ," +
                             " STSH_CREATED_BY,STSH_CREATED_DATE) " +
                             " VALUES('" + CommonData.CompanyCode +
                             "','" + CommonData.StateCode +
                             "', '" + CommonData.BranchCode +
                             "','" + CommonData.FinancialYear +
                             "','" + txtDocMonth.Text.ToUpper() +
                             "','" + cmbBoxTransType.SelectedItem.ToString() +
                             "' ," + txtTransaction.Text +
                             "," + txtConEcodeSearch.Text +                             
                             ",'" + txtRemarks.Text.Replace("'", "") +
                             "', " + txtAppEcodeSearch.Text +
                             ",'" + TrnDate.Value.ToString("dd/MMM/yyyy") + 
                             "','" + CommonData.LogUserId +
                             "' ,getdate())";
                }
                else
                {
                    sqlText = " UPDATE STATIONARY_SHORTAGE_HEAD SET STSH_DOC_MONTH='" + txtDocMonth.Text.ToUpper() +
                        "',STSH_EORA_CODE=" + txtConEcodeSearch.Text +                       
                        ",STSH_REMARKS='" + txtRemarks.Text.Replace("'", "") +
                        "',STSH_APPROVED_BY=" + txtAppEcodeSearch.Text +
                        ",STSH_DATE='" + TrnDate.Value.ToString("dd/MMM/yyyy") +
                        "',STSH_MODIFIED_BY='" + CommonData.LogUserId +
                        "',STSH_TRN_TYPE='" + cmbBoxTransType.SelectedItem.ToString() +
                        "',STSH_MODIFIED_DATE=GETDATE()" +

                        " WHERE STSH_COMPANY_CODE='" + CommonData.CompanyCode +
                        "' AND STSH_BRANCH_CODE='" + CommonData.BranchCode +
                        //"' AND SSSH_TRN_TYPE='" + cmbBoxTransType.SelectedItem.ToString() +
                        "' AND STSH_TRN_NO=" + txtTransaction.Text + "";

                }
                if (sqlText.Length > 3)
                {
                    objSQLdb = new SQLDB();
                    intRec = objSQLdb.ExecuteSaveData(sqlText);
                }

            }

            catch (Exception ex)
            {
                intRec = 0;
                MessageBox.Show(ex.Message, "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objSQLdb = null;
            }
            return intRec;
        }
        private int SaveStationaryDeatailData()
        {
            objSQLdb = new SQLDB();
            string sqlText = "";
            string sqlDelete = "";
            int intRec = 0;
            try
            {
                sqlDelete = " DELETE FROM STATIONARY_SHORTAGE_DETL" +
                            " WHERE STSD_COMPANY_CODE='" + CommonData.CompanyCode +
                            "' AND STSD_BRANCH_CODE='" + CommonData.BranchCode +
                    //"' AND SSSD_TRN_TYPE='" + cmbBoxTransType.SelectedItem +
                            "' AND STSD_TRN_NO=" + txtTransaction.Text + "";

                intRec = objSQLdb.ExecuteSaveData(sqlDelete);

                for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
                {

                    if (gvIndentDetails.Rows[i].Cells["Qty"].Value.ToString() != "")
                    {
                        sqlText += " INSERT INTO STATIONARY_SHORTAGE_DETL(STSD_COMPANY_CODE,STSD_STATE_CODE,STSD_BRANCH_CODE,STSD_FIN_YEAR,STSD_DOC_MONTH, " +
                                       "STSD_TRN_TYPE,STSD_TRN_NO,STSD_SL_NO,STSD_ITEM_ID,STSD_ITEM_QTY)" +
                                       " VALUES('" + CommonData.CompanyCode + "', '" + CommonData.StateCode + "', '" + CommonData.BranchCode
                                              + "','" + CommonData.FinancialYear + "','" + txtDocMonth.Text.ToUpper()
                                              + "','" + cmbBoxTransType.SelectedItem.ToString() + "'," + txtTransaction.Text
                                              + "," + gvIndentDetails.Rows[i].Cells["SLNO"].Value
                                              + "," + gvIndentDetails.Rows[i].Cells["ItemID"].Value.ToString().Trim()
                                              + "," + gvIndentDetails.Rows[i].Cells["Qty"].Value + ") ";
                    }
                    

                }
                intRec = 0;
                if (sqlText.Length > 10)
                    intRec = objSQLdb.ExecuteSaveData(sqlText);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objSQLdb = null;
            }
            return intRec;

        }
        private bool CheckData()
        {
            bool blValue = true;
            if (gvIndentDetails.Rows.Count == 0)
            {
                MessageBox.Show("Select Atlease One item!", "Stationary", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
            //{
            //    if (gvIndentDetails.Columns["Qty"].ToString().Trim() == "")
            //    {
            //        MessageBox.Show("Please Enter Qty", "Stationary", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return false;
            //    }
            //    //if (gvIndentDetails.Rows[i].Cells["ReqQty"].Value.ToString().Trim() != "")
            //    //{
            //    //    return true;
            //    //}

            //}
            if (txtConcernName.Text.Length == 0)
            {
                MessageBox.Show("Enter Concern Ecode !", "Stationary",MessageBoxButtons.OK,MessageBoxIcon.Error);
                blValue = false;
               txtConEcodeSearch.Focus();
                return blValue;
            }

            if (txtTransaction.Text.Length == 0)
            {
                MessageBox.Show("Enter Transaction Number", "Stationary", MessageBoxButtons.OK, MessageBoxIcon.Error);
                blValue = false;
                txtTransaction.Focus();
                return blValue;
            }
            if (txtApproverEName.Text.Length == 0)
            {
                MessageBox.Show("Enter Approved Ecode!", "Stationary", MessageBoxButtons.OK, MessageBoxIcon.Error);
                blValue = false;
                txtAppEcodeSearch.Focus();
                return blValue;
            }
          
            if (txtRemarks.Text.Length == 0)
            {
                MessageBox.Show("Enter Remarks", "Stationary", MessageBoxButtons.OK, MessageBoxIcon.Error);
                blValue = false;
                txtRemarks.Focus();
                return blValue;
            }
            for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
            {
                if (gvIndentDetails.Rows[i].Cells["Qty"].Value.ToString().Trim() == "0")
                {
                    MessageBox.Show("Please Enter Shortage Qty", "Stationary", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            
            return blValue;

        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            int intSaved = 0;
            try
            {

                if (CheckData() == true)
                {
                    if (SaveStationaryHeadData() >= 1)
                        intSaved = SaveStationaryDeatailData();


                    if (intSaved > 0)
                    {
                        MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txtConEcodeSearch.Text = "";
                        txtConcernName.Text = "";
                        cmbBoxTransType.SelectedIndex = 0;
                        txtTransaction.Text = "";
                        txtAppEcodeSearch.Text = "";
                        txtApproverEName.Text = string.Empty;
                        gvIndentDetails.Rows.Clear();
                        txtRemarks.Text = "";
                        IsModifyInvoice = false;
                       

                        generateId();
                    }
                    else
                    {
                        //string strSQL = " delete from SP_STOCK_SHORTAGE_HEAD where SSSH_COMPANY_CODE ='"+CommonData.CompanyCode+"' and SSSH_BRANCH_CODE='"+CommonData.BranchCode+"' and SSSH_TRN_TYPE='"+cmbBoxTransType.SelectedItem.ToString+"' and SSSH_TRN_NO="+txtTransaction.Text+" ";

                        string strSQL = "DELETE from STATIONARY_SHORTAGE_HEAD" +
                                      " WHERE STSH_COMPANY_CODE='" + CommonData.CompanyCode +
                                      "' AND STSH_BRANCH_CODE='" + CommonData.BranchCode +                                      
                                      "' AND STSH_TRN_NO=" + txtTransaction.Text;
                        objSQLdb = new SQLDB();
                        objSQLdb.ExecuteSaveData(strSQL);
                        MessageBox.Show("Data not saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ShortageStationaryDetails", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {

                //cbBranch.Enabled = true;
                //cbBranch.SelectedIndex = 0;
            }
        }

        private void txtTransaction_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == false)
                e.Handled = true;

            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void txtConEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == false)
                e.Handled = true;

            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void txtAppEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == false)
                e.Handled = true;

            if (e.KeyChar == 8)
                e.Handled = false;
        }
        private void FillData()
        {
            FillHeadData();

        }
        private void FillHeadData()
        {
            string strSQL = "Select *,CM_COMPANY_NAME,BRANCH_NAME from STATIONARY_SHORTAGE_HEAD "+
                                  "INNER JOIN COMPANY_MAS ON STSH_COMPANY_CODE=CM_COMPANY_CODE " +
                                  " INNER JOIN BRANCH_MAS ON BRANCH_CODE=STSH_BRANCH_CODE " +
                                 "where STSH_COMPANY_CODE='" + CommonData.CompanyCode +
                                "' and STSH_BRANCH_CODE='" + CommonData.BranchCode +
                //"' and SSSH_TRN_TYPE='" + cmbBoxTransType.SelectedItem +
                                "' and STSH_TRN_NO=" + txtTransaction.Text;
            objSQLdb = new SQLDB();
            try
            {
                DataTable dt = objSQLdb.ExecuteDataSet(strSQL).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    IsModifyInvoice = true;
                    txtCompanyName.Text = dt.Rows[0]["CM_COMPANY_NAME"].ToString();
                    txtBranchName.Text = dt.Rows[0]["BRANCH_NAME"].ToString();
                    txtDocMonth.Text = dt.Rows[0]["STSH_DOC_MONTH"].ToString();
                    TrnDate.Value = Convert.ToDateTime(dt.Rows[0]["STSH_DATE"].ToString());
                    txtConEcodeSearch.Text = dt.Rows[0]["STSH_EORA_CODE"].ToString();
                    txtAppEcodeSearch.Text = dt.Rows[0]["STSH_APPROVED_BY"].ToString();                    
                    txtRemarks.Text = dt.Rows[0]["STSH_REMARKS"].ToString();
                    cmbBoxTransType.SelectedItem = dt.Rows[0]["STSH_TRN_TYPE"].ToString();
                    FillDetailData();
                }
                else
                {
                    txtDocMonth.Text = CommonData.DocMonth;
                    txtConEcodeSearch.Text = "";
                    txtConcernName.Text = "";
                    txtApproverEName.Text = "";
                    txtAppEcodeSearch.Text = "";
                    
                    
                    txtRemarks.Text = "";
                    gvIndentDetails.Rows.Clear();

                    generateId();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }

        private void FillDetailData()
        {
            string strSQL = "Select *,SIM_ITEM_NAME,SIC_CATEGORY_NAME FROM STATIONARY_SHORTAGE_HEAD "+
                             " INNER JOIN  STATIONARY_SHORTAGE_DETL ON STSD_COMPANY_CODE=STSH_COMPANY_CODE "+
                             " AND STSD_BRANCH_CODE=STSH_BRANCH_CODE AND STSD_TRN_NO=STSH_TRN_NO "+
                            " INNER JOIN STATIONARY_ITEMS_MASTER ON SIM_ITEM_CODE=STSD_ITEM_ID  " +
                            "LEFT JOIN STATIONARY_ITEMS_CATEGORY ON  SIM_ITEM_CODE=SIC_CATEGORY_ID " +
                            "where STSD_COMPANY_CODE='" + CommonData.CompanyCode +
                            "' and STSD_BRANCH_CODE='" + CommonData.BranchCode +
                //"' and SSSD_TRN_TYPE='" + cmbBoxTransType.SelectedItem +
                            "' and STSD_TRN_NO=" + txtTransaction.Text;
            try
            {
                objSQLdb = new SQLDB();
                DataTable dt = objSQLdb.ExecuteDataSet(strSQL).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    gvIndentDetails.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        gvIndentDetails.Rows.Add();
                        gvIndentDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                        gvIndentDetails.Rows[i].Cells["ItemID"].Value = dt.Rows[i]["STSD_ITEM_ID"].ToString();
                        gvIndentDetails.Rows[i].Cells["ItemName"].Value = dt.Rows[i]["SIM_ITEM_NAME"].ToString();
                        //gvProductDetails.Rows[i].Cells["Category"].Value = dt.Rows[i]["SIC_CATEGORY_NAME"].ToString();
                        gvIndentDetails.Rows[i].Cells["Qty"].Value = dt.Rows[i]["STSD_ITEM_QTY"].ToString();
                      

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDocMonth.Text = CommonData.DocMonth;
            txtConEcodeSearch.Text = "";
            //txtCompanyName.Text = "";
            //txtBranchName.Text = "";
            txtConcernName.Text = "";
            txtApproverEName.Text = "";
            gvIndentDetails.Rows.Clear();
            txtRemarks.Text = "";
            txtAppEcodeSearch.Text = "";
            txtApproverEName.Tag = "";
            TrnDate.Value = Convert.ToDateTime(CommonData.DocMonth);
            generateId();
           
        }

       
        private void btnClearItems_Click(object sender, EventArgs e)
        {
            gvIndentDetails.Rows.Clear();
        }

        private void txtTransaction_Validated(object sender, EventArgs e)
        {
            if (txtTransaction.Text.Length > 0)
            {
                FillData();
            }
        }

        private void gvIndentDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvIndentDetails.Columns["Del"].Index)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    string ProductName = gvIndentDetails.Rows[e.RowIndex].Cells[gvIndentDetails.Columns["ItemName"].Index].Value.ToString();
                    DataGridViewRow dgvr = gvIndentDetails.Rows[e.RowIndex];
                    gvIndentDetails.Rows.Remove(dgvr);
                }
                for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
                {
                    gvIndentDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                }
            }
        }



        private void gvIndentDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            blIsCellQty = true;
            intCurrentRow = (((System.Windows.Forms.DataGridView)(sender))).CurrentCell.RowIndex;
            intCurrentCell = (((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex;
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 3)
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    txtQty.MaxLength = 6;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
            else
                blIsCellQty = false;
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46) || (blIsCellQty == false))
            {
                e.Handled = true;
                return;
            }
            if (intCurrentCell == 5 && e.KeyChar == 46)
            {
                e.Handled = true;
                return;
            }
            // checks to make sure only 1 decimal is allowed
            else if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }
    }

}
