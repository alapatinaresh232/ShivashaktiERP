using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SSTrans;
using SSCRMDB;


namespace SSCRM
{
    public partial class ShortageStockDetails : Form
    {
        private SQLDB objSQLdb = null;
        private InvoiceDB objData = null;
        private bool IsModifyInvoice = false;
        public ShortageStockDetails()
        {
            InitializeComponent();
        }

        private void ShortageStockDetails_Load(object sender, EventArgs e)
        {
           // FillEmployeeData();
            txtDocMonth.Text = CommonData.DocMonth;
            generateId();
            cmbBoxTransType.SelectedIndex = 0;
            gvProductDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                     System.Drawing.FontStyle.Regular);
            TrnDate.Value = Convert.ToDateTime(CommonData.DocMonth);
        }
        public void generateId()
        {
            string strCommand = null;
            SQLDB objSQLdb = new SQLDB();
            DataSet ds = new DataSet();
            DataTable dt;
            try
            {

                strCommand = "SELECT ISNULL(MAX(SSSH_TRN_NO),0)+1 FROM SP_STOCK_SHORTAGE_HEAD" +
                                " WHERE SSSH_BRANCH_CODE='" + CommonData.BranchCode + "'";
                ds = objSQLdb.ExecuteDataSet(strCommand);
                dt = ds.Tables[0];
                txtTransaction.Text =  Convert.ToInt32(dt.Rows[0][0]).ToString("0");
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

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            objSQLdb = new SQLDB();
            if (txtEcodeSearch.Text.ToString().Trim().Length > 0)
            {
                try
                {
                    objSQLdb = new SQLDB();
                    DataTable dt = new DataTable();
                    dt = objSQLdb.ExecuteDataSet("SELECT MEMBER_NAME from EORA_MASTER WHERE ecode='" + txtEcodeSearch.Text + "' ").Tables[0];

                    if (dt.Rows.Count > 0)
                        txtEName.Text = dt.Rows[0]["MEMBER_NAME"] + "";
                    else
                        txtEName.Text = "";

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
               
                txtEcodeSearch.Focus();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDocMonth.Text = CommonData.DocMonth;
            txtEcodeSearch.Text = "";
            txtEName.Text = "";
       
            gvProductDetails.Rows.Clear();
            txtRemarks.Text = "";
            txtApprovedEcode.Text = "";
            txtApproverEName.Tag = "";
            txtActualFine.Text = "";
            txtFineCollAmount.Text = "";
            generateId();
            IsModifyInvoice = false;
        }
        private bool CheckData()
        {
            bool blValue = true;
            if (txtEName.Text.Length == 0)
            {
                MessageBox.Show("Enter Ecode !", "ShoratgeStock Details");
                blValue = false;
                txtEcodeSearch.Focus();
                return blValue;
            }
            
            if(txtTransaction.Text.Length==0){
                MessageBox.Show("Enter Transaction Number", "ShoratgeStock Details");
                blValue = false;
                txtTransaction.Focus();
                return blValue;
            }
            if (txtApproverEName.Text.Length == 0)
            {
                MessageBox.Show("Enter Ecode number!", "ShoratgeStock Details");
                blValue = false;
                txtApprovedEcode.Focus();
                return blValue;
            }
            if (txtFineCollAmount.Text.Length==0)
            {
                MessageBox.Show("Enter Fine Collected Amount!", "ShoratgeStock Details");
                blValue = false;
                txtFineCollAmount.Focus();
                return blValue;
            }
            if (txtRemarks.Text.Length == 0)
            {
                MessageBox.Show("Enter Remarks", "ShoratgeStock Details");
                blValue = false;
                txtRemarks.Focus();
                return blValue;
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
                    if (SaveStockHeadData() >= 1)
                        intSaved = SaveStockDeatailData();
                    
                    
                    if (intSaved > 0)
                    {
                        txtEcodeSearch.Text = "";
                        txtEName.Text = "";
                        cmbBoxTransType.SelectedIndex = 0;
                        txtTransaction.Text = "";
                        txtApprovedEcode.Text = "";
                        txtApproverEName.Text = string.Empty;
                        txtActualFine.Text = "";
                        txtFineCollAmount.Text = "";
                        gvProductDetails.Rows.Clear();
                        IsModifyInvoice = false;
                        MessageBox.Show("Data Saved Successfully", "ShortageStockDetails", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        generateId();
                    }
                    else
                       
                    {
                        //string strSQL = " delete from SP_STOCK_SHORTAGE_HEAD where SSSH_COMPANY_CODE ='"+CommonData.CompanyCode+"' and SSSH_BRANCH_CODE='"+CommonData.BranchCode+"' and SSSH_TRN_TYPE='"+cmbBoxTransType.SelectedItem.ToString+"' and SSSH_TRN_NO="+txtTransaction.Text+" ";
                        
                    string strSQL =    "DELETE from SP_STOCK_SHORTAGE_HEAD" +
                                  " WHERE SSSH_COMPANY_CODE='" + CommonData.CompanyCode +
                                  "' AND SSSH_BRANCH_CODE='" + CommonData.BranchCode +
                                  "' AND SSSH_TRN_TYPE='" + cmbBoxTransType.SelectedItem+
                                  "' AND SSSH_TRN_NO="+txtTransaction.Text ;
                    objSQLdb = new SQLDB();
                    objSQLdb.ExecuteSaveData(strSQL);
                    MessageBox.Show("Data not saved", "ShortageStockDetails", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ShortageStockDetails", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {

                //cbBranch.Enabled = true;
                //cbBranch.SelectedIndex = 0;
            }
        }
        private int SaveStockHeadData()
        {
            objSQLdb = new SQLDB();
            string sqlText = "";
            int intRec = 0;

            if (txtFineCollAmount.Text == "")
                txtFineCollAmount.Text = "0.00";
            try
            {
                if (IsModifyInvoice == false)
                {
                    sqlText = " INSERT INTO SP_STOCK_SHORTAGE_HEAD(SSSH_COMPANY_CODE,SSSH_STATE_CODE,SSSH_BRANCH_CODE,SSSH_FIN_YEAR,SSSH_DOC_MONTH,SSSH_TRN_TYPE, " +
                             " SSSH_TRN_NO,SSSH_EORA_CODE,SSSH_FINE_AMT,SSSH_COLLECTED_AMT,SSSH_REMARKS,SSSH_APPROVED_BY,SSSH_DATE," +
                             " SSSH_CREATED_BY,SSSH_CREATED_DATE,SSSH_ECODE) " +
                             " VALUES('" + CommonData.CompanyCode + 
                             "','" + CommonData.StateCode + 
                             "', '" + CommonData.BranchCode + 
                             "','" + CommonData.FinancialYear + 
                             "','" + txtDocMonth.Text.ToUpper() + 
                             "','" + cmbBoxTransType.SelectedItem.ToString() + 
                             "' ," + txtTransaction.Text + 
                             "," + txtEcodeSearch.Text + 
                             ", " + txtActualFine.Text + 
                             "," + txtFineCollAmount.Text + 
                             ",'" + txtRemarks.Text.Replace("'", "") + 
                             "', " + txtApprovedEcode.Text + 
                             ",'" + TrnDate.Value.ToString("dd/MMM/yyyy")
                             + "','" + CommonData.LogUserId + 
                             "' ,getdate()," + txtEcodeSearch.Text + ")";
                }
                else
                {
                    sqlText = " UPDATE SP_STOCK_SHORTAGE_HEAD SET SSSH_DOC_MONTH='" + txtDocMonth.Text.ToUpper() +
                        "',SSSH_EORA_CODE=" + txtEcodeSearch.Text +
                        ",SSSH_FINE_AMT=" + txtActualFine.Text +
                        ",SSSH_COLLECTED_AMT=" + txtFineCollAmount.Text +
                        ",SSSH_REMARKS='" + txtRemarks.Text.Replace("'", "") +
                        "',SSSH_APPROVED_BY=" + txtApprovedEcode.Text +
                        ",SSSH_DATE='" + TrnDate.Value.ToString("dd/MMM/yyyy") +
                        "',SSSH_MODIFIED_BY='" + CommonData.LogUserId +
                        "',SSSH_TRN_TYPE='" + cmbBoxTransType.SelectedItem.ToString() +
                        "',SSSH_MODIFIED_DATE=GETDATE()" +
                        ",SSSH_ECODE=" + txtEcodeSearch.Text +
                        " WHERE SSSH_COMPANY_CODE='" + CommonData.CompanyCode +
                        "' AND SSSH_BRANCH_CODE='" + CommonData.BranchCode +
                        //"' AND SSSH_TRN_TYPE='" + cmbBoxTransType.SelectedItem.ToString() +
                        "' AND SSSH_TRN_NO=" + txtTransaction.Text + "";
                        
                }
                intRec = 0;
                objSQLdb = new SQLDB();
                intRec = objSQLdb.ExecuteSaveData(sqlText);

            }
               
            catch (Exception ex)
            {
                intRec = 0;
                MessageBox.Show(ex.Message, "ShortageStockDetails", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objSQLdb = null;
            }
            return intRec;
        }
        private int SaveStockDeatailData()
        {
          objSQLdb = new SQLDB();
            string sqlText = "";
            string sqlDelete = "";
            int intRec = 0;
            try
            {
                sqlDelete = " DELETE from SP_STOCK_SHORTAGE_DETL" +
                            " WHERE SSSD_COMPANY_CODE='" + CommonData.CompanyCode +
                            "' AND SSSD_BRANCH_CODE='" + CommonData.BranchCode +
                            //"' AND SSSD_TRN_TYPE='" + cmbBoxTransType.SelectedItem +
                            "' AND SSSD_TRN_NO=" + txtTransaction.Text + "";

                intRec = objSQLdb.ExecuteSaveData(sqlDelete);

                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {
                    if (gvProductDetails.Rows[i].Cells["Fine"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["Qty"].Value.ToString() != "")
                    {
                         sqlText += " INSERT INTO SP_STOCK_SHORTAGE_DETL(SSSD_COMPANY_CODE,SSSD_STATE_CODE,SSSD_BRANCH_CODE,SSSD_FIN_YEAR,SSSD_DOC_MONTH, " +
                                    "SSSD_TRN_TYPE,SSSD_TRN_NO,SSSD_SL_NO,SSSD_PRODUCT_ID,SSSD_QTY,SSSD_PRICE, "+
                                    " SSSD_FINE_PER_QTY, SSSD_TOTAL_FINE)"+
                                    " VALUES('" + CommonData.CompanyCode + "', '" + CommonData.StateCode + "', '" + CommonData.BranchCode 
                                           +"','"+ CommonData.FinancialYear + "','" + txtDocMonth.Text.ToUpper()
                                           + "','" + cmbBoxTransType.SelectedItem.ToString() + "'," + txtTransaction.Text
                                           +","+gvProductDetails.Rows[i].Cells["SLNO"].Value
                                           +",'"+gvProductDetails.Rows[i].Cells["ProductID"].Value.ToString().Trim()
                                           +"',"+gvProductDetails.Rows[i].Cells["Qty"].Value
                                           +","+gvProductDetails.Rows[i].Cells["Rate"].Value
                                           +","+gvProductDetails.Rows[i].Cells["Fine"].Value
                                           + "," + gvProductDetails.Rows[i].Cells["Points"].Value + "); ";
                    }

                }
                intRec = 0;
                if (sqlText.Length > 10)
                    intRec = objSQLdb.ExecuteSaveData(sqlText);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ShortageStockDetails", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objSQLdb = null;
            }
            return intRec;

        }
        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            ProductSearchAll PSearch = new ProductSearchAll("ShortageStockDetails",cmbBoxTransType.SelectedItem.ToString());
            PSearch.objFrmShortageStock = this;
            PSearch.ShowDialog();
        }

        private void gvProductDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvProductDetails.Columns["Del"].Index)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    string ProductName = gvProductDetails.Rows[e.RowIndex].Cells[gvProductDetails.Columns["MainProduct"].Index].Value.ToString();
                    DataGridViewRow dgvr = gvProductDetails.Rows[e.RowIndex];
                    gvProductDetails.Rows.Remove(dgvr);
                }
                for (int i = 0; i < gvProductDetails.Rows.Count;i++ )
                {
                    gvProductDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                }
            }
        }

       

        private void txtApprovedEcode_Click(object sender, KeyEventArgs e)
        {
            objSQLdb = new SQLDB();
            if (txtApprovedEcode.Text.ToString().Trim().Length > 0)
            {
                try
                {
                    objSQLdb = new SQLDB();
                    DataTable dt = new DataTable();
                    dt = objSQLdb.ExecuteDataSet("SELECT MEMBER_NAME FROM EORA_MASTER WHERE ECODE='" + txtApprovedEcode.Text + "' ").Tables[0];

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
              txtApprovedEcode.Focus();
            }
        }

        void summition()
        {
            
            double sum = 0;
            foreach (DataGridViewRow row in gvProductDetails.Rows)
            {
                if (!row.IsNewRow)
                    sum += Convert.ToDouble(row.Cells[gvProductDetails.Columns["Points"].Index].Value.ToString());
            }
            txtActualFine.Text = sum.ToString();
            txtFineCollAmount.Text = sum.ToString();
        }
        private void gvProductDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (gvProductDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
            {
                gvProductDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
            }
            if (cmbBoxTransType.SelectedItem == "SHORTAGE")
            {
                if (e.ColumnIndex == gvProductDetails.Columns["Qty"].Index || e.ColumnIndex == gvProductDetails.Columns["Fine"].Index)
                {
                    if (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value) >= 0 && Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Fine"].Value) >= 0)
                    {
                        gvProductDetails.Rows[e.RowIndex].Cells["Points"].Value = (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value)) * (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Fine"].Value));
                        gvProductDetails.Rows[e.RowIndex].Cells["Points"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Points"].Value).ToString("f");
                    }
                }
            }
            else
            {
                gvProductDetails.Rows[e.RowIndex].Cells["Fine"].Value = "0";
            }
            //if (e.ColumnIndex == gvProductDetails.Columns["Points"].Index)
            //{
                summition();
            //}
        }

        private void txtTransaction_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == false)
                e.Handled = true;

            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void txtEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == false)
                e.Handled = true;

            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void txtApprovedEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == false)
                e.Handled = true;

            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void txtFineCollAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == false)
                e.Handled = true;

            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void txtTransaction_Validated(object sender, EventArgs e)
        {
            if (txtTransaction.Text.Length > 0)
            {
                FillData();
            }
        }

        private void FillData()
        {
            FillHeadData();
            
        }
        private void FillHeadData()
        {
            string strSQL = "Select SP_STOCK_SHORTAGE_HEAD.*, umc.UM_USER_NAME CreatedBy, umm.UM_USER_NAME ModifiedBy from SP_STOCK_SHORTAGE_HEAD"+
                                " left join user_master umc on umc.um_user_id = SSSH_CREATED_BY"+
                                " left join user_master umm on umm.um_user_id = SSSH_MODIFIED_BY"+
                                " where SSSH_COMPANY_CODE='" + CommonData.CompanyCode +
                                "' and SSSH_BRANCH_CODE='" + CommonData.BranchCode +
                                //"' and SSSH_TRN_TYPE='" + cmbBoxTransType.SelectedItem +
                                "' and SSSH_TRN_NO=" + txtTransaction.Text;
            objSQLdb = new SQLDB();
            try
            {
                DataTable dt = objSQLdb.ExecuteDataSet(strSQL).Tables[0];
                if (dt.Rows.Count > 0)
                {                    
                    IsModifyInvoice = true;
                    txtDocMonth.Text = dt.Rows[0]["SSSH_DOC_MONTH"].ToString();
                    TrnDate.Value = Convert.ToDateTime(dt.Rows[0]["SSSH_DATE"].ToString());
                    txtEcodeSearch.Text = dt.Rows[0]["SSSH_EORA_CODE"].ToString();
                    txtApprovedEcode.Text = dt.Rows[0]["SSSH_APPROVED_BY"].ToString();
                    txtFineCollAmount.Text = dt.Rows[0]["SSSH_COLLECTED_AMT"].ToString();
                    txtActualFine.Text = dt.Rows[0]["SSSH_FINE_AMT"].ToString();
                    txtRemarks.Text = dt.Rows[0]["SSSH_REMARKS"].ToString();
                    cmbBoxTransType.SelectedItem = dt.Rows[0]["SSSH_TRN_TYPE"].ToString();
                    txtCreatedBy.Text = dt.Rows[0]["SSSH_CREATED_BY"].ToString() + "-" + dt.Rows[0]["CreatedBy"].ToString();
                    txtModifiedBy.Text = dt.Rows[0]["SSSH_MODIFIED_BY"].ToString() + "-" + dt.Rows[0]["CreatedBy"].ToString();

                    if (dt.Rows[0]["SSSH_CREATED_DATE"].ToString() != "")
                        txtCreatedDate.Text = Convert.ToDateTime(dt.Rows[0]["SSSH_CREATED_DATE"].ToString()).ToString();
                    else
                        txtCreatedDate.Text = "";

                    if(dt.Rows[0]["SSSH_MODIFIED_DATE"].ToString()!="")
                        txtModifiedDate.Text = Convert.ToDateTime(dt.Rows[0]["SSSH_MODIFIED_DATE"].ToString()).ToString();
                    else
                        txtModifiedDate.Text = "";

                    FillDetailData();
                }
                else
                {
                    txtDocMonth.Text = CommonData.DocMonth;
                    txtEcodeSearch.Text = "";
                    txtEName.Text = "";
                    txtApproverEName.Text = "";
                    txtApprovedEcode.Text = "";
                    txtFineCollAmount.Text = "";
                    txtActualFine.Text = "";
                    txtRemarks.Text = "";
                    gvProductDetails.Rows.Clear();
                 
                    generateId();
                }
            }
            catch(Exception ex)
            {
                ex.ToString();
            }

        }

        private void FillDetailData()
        {
            string strSQL = "Select *,pm_product_name,category_name from SP_STOCK_SHORTAGE_DETL " +
                            "inner join product_mas on pm_product_id=SSSD_PRODUCT_ID " +
                            "inner join category_master on category_id=pm_category_id " +
                            "where SSSD_COMPANY_CODE='" + CommonData.CompanyCode +
                            "' and SSSD_BRANCH_CODE='" + CommonData.BranchCode +
                            //"' and SSSD_TRN_TYPE='" + cmbBoxTransType.SelectedItem +
                            "' and SSSD_TRN_NO=" + txtTransaction.Text;
            try
            {
                objSQLdb = new SQLDB();
                DataTable dt = objSQLdb.ExecuteDataSet(strSQL).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    gvProductDetails.Rows.Clear();
                    for(int i=0;i<dt.Rows.Count;i++)
                    {
                        gvProductDetails.Rows.Add();
                        gvProductDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                        gvProductDetails.Rows[i].Cells["ProductID"].Value = dt.Rows[i]["SSSD_PRODUCT_ID"].ToString();
                        gvProductDetails.Rows[i].Cells["MainProduct"].Value = dt.Rows[i]["pm_product_name"].ToString();
                        gvProductDetails.Rows[i].Cells["Category"].Value = dt.Rows[i]["category_name"].ToString();
                        gvProductDetails.Rows[i].Cells["Qty"].Value = dt.Rows[i]["SSSD_QTY"].ToString();
                        gvProductDetails.Rows[i].Cells["Rate"].Value = dt.Rows[i]["SSSD_PRICE"].ToString();
                        gvProductDetails.Rows[i].Cells["Fine"].Value = dt.Rows[i]["SSSD_FINE_PER_QTY"].ToString();
                        gvProductDetails.Rows[i].Cells["Points"].Value = dt.Rows[i]["SSSD_TOTAL_FINE"].ToString();

                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cmbBoxTransType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillData();
            if (cmbBoxTransType.SelectedItem != "SHORTAGE")
            {
                for (int i = 0; i < gvProductDetails.Rows.Count;i++ )
                {
                    gvProductDetails.Rows[i].Cells["Fine"].Value = "0";
                    gvProductDetails.Rows[i].Cells["Points"].Value = "0";
                }
                summition();
                txtFineCollAmount.Text = "0";
            }
        }

        private void gvProductDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == gvProductDetails.Columns["Qty"].Index || (((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == gvProductDetails.Columns["Rate"].Index
                                                    || (((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == gvProductDetails.Columns["Fine"].Index)
            {
                TextBox txtQty1 = e.Control as TextBox;
                if (txtQty1 != null)
                {
                    //flagText = true;
                    txtQty1.MaxLength = 30;
                    txtQty1.KeyPress += new KeyPressEventHandler(txt_KeyPress);

                }
            }

        }
        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            // checks to make sure only 1 decimal is allowed
            if (e.KeyChar == 46 )
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }

        }

        private void txtApprovedEcode_TextChanged(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            if (txtApprovedEcode.Text.ToString().Trim().Length > 0)
            {
                try
                {
                    objSQLdb = new SQLDB();
                    DataTable dt = new DataTable();
                    dt = objSQLdb.ExecuteDataSet("SELECT MEMBER_NAME from EORA_MASTER WHERE ecode='" + txtApprovedEcode.Text + "' ").Tables[0];

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
                txtApprovedEcode.Text = "";
                
            }
        }

        private void txtEcodeSearch_TextChanged(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            if (txtEcodeSearch.Text.ToString().Trim().Length > 0)
            {
                try
                {
                    objSQLdb = new SQLDB();
                    DataTable dt = new DataTable();
                    dt = objSQLdb.ExecuteDataSet("SELECT MEMBER_NAME from EORA_MASTER WHERE ecode='" + txtEcodeSearch.Text + "' ").Tables[0];

                    if (dt.Rows.Count > 0)
                        txtEName.Text = dt.Rows[0]["MEMBER_NAME"] + "";
                    else
                        txtEName.Text = "";

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
                txtEcodeSearch.Text = "";
                txtEcodeSearch.Focus();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string sqlText = "";
            sqlText += " DELETE FROM SP_STOCK_SHORTAGE_DETL WHERE SSSD_BRANCH_CODE = '" + CommonData.BranchCode +
                        "' AND SSSD_FIN_YEAR='" + CommonData.FinancialYear + "' AND SSSD_TRN_NO=" + txtTransaction.Text;
            sqlText += " DELETE FROM SSSH_BRANCH_CODE WHERE SSSH_BRANCH_CODE = '" + CommonData.BranchCode +
                        "' AND SSSH_FIN_YEAR='" + CommonData.FinancialYear + "' AND SSSH_TRN_NO=" + txtTransaction.Text;
            objSQLdb = new SQLDB();
            if (txtTransaction.Text.Length == 0)
            {
                MessageBox.Show("Invalid Transaction Number", "ShoratgeStock Details");                
                txtTransaction.Focus();
                return;
            }
            if (CommonData.LogUserRole.ToUpper() != "MANAGEMENT" && CommonData.LogUserId.ToUpper() != "ADMIN")
            {
                //Convert.ToDateTime(CommonData.CurrentDate) - Convert.ToDateTime(txtCreatedDate.Text);
            }
        }
      
    }
}
