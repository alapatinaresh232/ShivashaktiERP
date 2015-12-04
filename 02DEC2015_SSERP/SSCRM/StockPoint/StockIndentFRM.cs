using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using System.Diagnostics;
using System.Configuration;
using SSCRMDB;
using SSAdmin;
using SSTrans;
namespace SSCRM
{
    public partial class StockIndentFRM : Form
    {
        private SQLDB objSQLDB = null;
        private IndentDB objData = null;
        private bool IsModify = false;

        public StockIndentFRM()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void StockIndentFRM_Load(object sender, EventArgs e)
        {
            FillBranchData();
            FillBranchGroupData();
            meIndentDate.Text = Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyyy");
            txtIndentNo.Text = NewIndentNumber().ToString();
            cbGroup.SelectedIndex = 0;
            cbBranches.SelectedIndex = 0;
        }

        private void gvIndentDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 7)
            {
                   TextBox txtQty = e.Control as TextBox;
                   if (txtQty != null)
                    {
                        txtQty.MaxLength = 4;
                        txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                    }
                
            }

        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (char.IsNumber(e.KeyChar) == false)
                e.Handled = true;
            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            IndentProductSearch PSearch = new IndentProductSearch("IndentToBranch");
            PSearch.objStockIndentFRM = this;
            PSearch.ShowDialog();
        }

        private Int32 NewIndentNumber()
        {
            Int32 nIndNo = 0;
            objData = new IndentDB();
            try
            {
                nIndNo =objData.GenerateNewIndentNo(CommonData.CompanyCode,CommonData.BranchCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Indent product search");
            }
            finally
            {
                objData = null;
            }

            return nIndNo;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int intSave = 0;
            try
            {
                if (CheackData())
                {
                    if (SaveIndentHeadData() > 0)
                        intSave = SaveIndentDetlData();
                    else
                    {
                        string strSQL = "DELETE from SP_INDENT_HEAD" +
                                    " WHERE SPIH_COMPANY_CODE='" + CommonData.CompanyCode +
                                    "' AND SPIH_BRANCH_CODE='" + CommonData.BranchCode +
                                    "' AND SPIH_INDENT_NUMBER=" + txtIndentNo.Text +
                                    "  AND SPIH_FIN_YEAR='" + CommonData.FinancialYear + "'";
                        objSQLDB = new SQLDB();
                        int intDel = objSQLDB.ExecuteSaveData(strSQL);
                        objSQLDB = null;
                    }

                    if (intSave > 0)
                    {
                        MessageBox.Show("Indent data saved.", "Indent");
                        txtIndentNo.Text = NewIndentNumber().ToString();
                        gvIndentDetails.Rows.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Indent data not saved.", "Indent");
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                IsModify = false;
            }
        }

        private int SaveIndentHeadData()
        {
            int intSave = 0;
            string strSQL = string.Empty;
            objSQLDB = new SQLDB();
            try
            {
                if (IsModify == false)
                {
                    txtIndentNo.Text = NewIndentNumber().ToString();

                    strSQL = "INSERT INTO SP_INDENT_HEAD " +
                         "(SPIH_COMPANY_CODE " +
                         ", SPIH_STATE_CODE " +
                         ", SPIH_BRANCH_CODE " +
                         ", SPIH_FIN_YEAR " +
                         ", SPIH_INDENT_NUMBER " +
                         ", SPIH_INDENT_DATE " +
                         ", SPIH_CREATED_BY " +
                         ", SPIH_CREATED_DATE) " +
                         "VALUES " +
                         "('" + CommonData.CompanyCode +
                         "', '" + CommonData.StateCode +
                         "', '" + CommonData.BranchCode +
                         "', '" + CommonData.FinancialYear +
                         "', " + txtIndentNo.Text +
                         ", '" + Convert.ToDateTime(meIndentDate.Text).ToString("dd/MMM/yyy") +
                         "', '" + CommonData.LogUserId +
                         "', '" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyy") +
                         "')";
                }
                else
                {
                    strSQL = "DELETE from SP_INDENT_DETL" +
                                " WHERE spid_company_code='" + CommonData.CompanyCode +
                                    "' AND spid_branch_code='" + CommonData.BranchCode +
                                    "' AND spid_indent_number=" + txtIndentNo.Text +
                                    "  AND spid_fin_year='" + CommonData.FinancialYear + "'";

                   int intRec = objSQLDB.ExecuteSaveData(strSQL);

                   strSQL = "UPDATE SP_INDENT_HEAD set SPIH_INDENT_DATE='" + Convert.ToDateTime(meIndentDate.Text).ToString("dd/MMM/yyy") +
                            "', SPIH_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                            "', SPIH_LAST_MODIFIED_DATE='" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyy") +
                            "' WHERE SPIH_INDENT_NUMBER = " + txtIndentNo.Text +
                            "  AND SPIH_BRANCH_CODE='" + CommonData.BranchCode +
                            "' AND SPIH_FIN_YEAR='" + CommonData.FinancialYear.ToString() +
                            "' AND SPIH_COMPANY_CODE='" + CommonData.CompanyCode.ToString() + "'";


                }
                
               intSave = objSQLDB.ExecuteSaveData(strSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                objSQLDB = null;
            }

            return intSave;
        }

        private int SaveIndentDetlData()
        {
            int intSave = 0;
            string strSQL = string.Empty;
            StringBuilder sbSQL = new StringBuilder();
            objSQLDB = new SQLDB();
            try
            {
                strSQL = "DELETE from SP_INDENT_DETL" +
                                " WHERE spid_company_code='" + CommonData.CompanyCode +
                                    "' AND spid_branch_code='" + CommonData.BranchCode +
                                    "' AND spid_indent_number=" + txtIndentNo.Text +
                                    "  AND spid_fin_year='" + CommonData.FinancialYear + "'";

                intSave = objSQLDB.ExecuteSaveData(strSQL);

                for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
                {
                    if (gvIndentDetails.Rows[i].Cells["Qty"].Value.ToString() != "" && gvIndentDetails.Rows[i].Cells["Qty"].Value.ToString() != "0")
                    {
                        sbSQL.Append("INSERT INTO SP_INDENT_DETL(spid_company_code, spid_state_code, spid_branch_code, spid_indent_number,spid_fin_year, spid_sl_no, SPID_GROUP, SPID_GLECODE, SPID_STOCK_POINT, SPID_PRODUCT_ID, SPID_REQ_QTY)" +
                                    " VALUES ('" + CommonData.CompanyCode + "', '" + CommonData.StateCode + "', '" + CommonData.BranchCode + "', " + txtIndentNo.Text + ", '" + CommonData.FinancialYear + "'," + gvIndentDetails.Rows[i].Cells["SLNO"].Value +
                                    ", '" + gvIndentDetails.Rows[i].Cells["Group"].Value.ToString().Trim() + "', '" + gvIndentDetails.Rows[i].Cells["GLCode"].Value + "', '" + gvIndentDetails.Rows[i].Cells["BranchCode"].Value +
                                    "', '" + gvIndentDetails.Rows[i].Cells["ProductId"].Value + "', " + gvIndentDetails.Rows[i].Cells["Qty"].Value + "); ");

                    }

                }
                intSave = 0;
                if (sbSQL.ToString().Length > 10)
                    intSave = objSQLDB.ExecuteSaveData(sbSQL.ToString());

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                objSQLDB = null;
            }
            return intSave;


        }

        private void FillIndentData(string strIndentNo)
        {
            objData = new IndentDB();
            try
            {
                gvIndentDetails.Rows.Clear();
                txtIndentNo.Text = "";
                meIndentDate.Text = "";
               
                DataTable dt = objData.IndentProductDetail_Get(CommonData.CompanyCode, CommonData.BranchCode, Convert.ToInt32(strIndentNo)).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    IsModify = true;
                    txtIndentNo.Text = strIndentNo;
                    meIndentDate.Text = Convert.ToDateTime(dt.Rows[0]["indent_date"].ToString()).ToString("dd/MM/yyyy");
                    for (int intRow = 0; intRow < dt.Rows.Count; intRow++)
                    {

                        gvIndentDetails.Rows.Add();
                        gvIndentDetails.Rows[intRow].Cells["SLNO"].Value = dt.Rows[intRow]["indent_sl_no"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["Group"].Value = dt.Rows[intRow]["SPID_GROUP"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["GLCode"].Value = dt.Rows[intRow]["SPID_GLECODE"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["EcodeName"].Value = dt.Rows[intRow]["ENAME"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["StockPoint"].Value = dt.Rows[intRow]["branch_name"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["Category"].Value = dt.Rows[intRow]["category_name"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["Product"].Value = dt.Rows[intRow]["pm_product_name"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["Qty"].Value = dt.Rows[intRow]["qty"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["ProductId"].Value = dt.Rows[intRow]["product_id"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["BranchCode"].Value = dt.Rows[intRow]["SPID_STOCK_POINT"].ToString();

                    }
                }
                else
                {
                    IsModify = false;
                    meIndentDate.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objData = null;
            }
        }

        private void FillIndentList()
        {
            objData = new IndentDB();
            string strGlCode="0";
            string strStockPointCode=string.Empty;
            try
            {

                gvIndentList.Rows.Clear();
                if (cbGroup.SelectedIndex > -1)
                {
                    string[] strArrGlCode = ((NewCheckboxListItem)(cbGroup.SelectedItem)).Tag.ToString().Split('~');
                    strGlCode = strArrGlCode[0];
                }
                if (cbBranches.SelectedIndex > -1)
                    strStockPointCode = ((NewCheckboxListItem)(cbBranches.SelectedItem)).Tag.ToString();
                if (strGlCode != "" && strStockPointCode.Length >= 1)
                {
                    DataTable dt = objData.IndentList_Get(CommonData.CompanyCode, CommonData.BranchCode, Convert.ToInt32(strGlCode), strStockPointCode).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        for (int intRow = 0; intRow < dt.Rows.Count; intRow++)
                        {

                            gvIndentList.Rows.Add();
                            gvIndentList.Rows[intRow].Cells["SLNOList"].Value = intRow + 1;
                            gvIndentList.Rows[intRow].Cells["IndentNo"].Value = dt.Rows[intRow]["indent_number"].ToString();
                            gvIndentList.Rows[intRow].Cells["IndentDate"].Value = Convert.ToDateTime(dt.Rows[intRow]["indent_date"]).ToString("dd/MM/yyyy");
                            gvIndentList.Rows[intRow].Cells["TotalProducts"].Value = dt.Rows[intRow]["TotalProducts"].ToString();
                            gvIndentList.Rows[intRow].Cells["TotalReqQty"].Value = dt.Rows[intRow]["TotalReqQty"].ToString();

                        }
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objData = null;
            }
        }

        private void cbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbGroup.SelectedIndex > -1)
            {
                gvIndentDetails.Rows.Clear();
                string[] strArrGlCode = ((NewCheckboxListItem)(cbGroup.SelectedItem)).Tag.ToString().Split('~');
                if(cbGroup.SelectedIndex >0)
                    lblGroupEcode.Text = strArrGlCode[1].ToString();
                FillIndentList();
            }
        }

        private bool CheackData()
        {
            bool blData = true;

            if (txtIndentNo.Text == "")
            {
                MessageBox.Show("Enter Indent No.", "Indent");
                blData = false;
                txtIndentNo.Focus();
                return blData;
            }
            objData = new IndentDB();
            int Trano=objData.CheckIndentNoDeliver(Convert.ToInt32(txtIndentNo.Text.ToString()));
            if (Trano > 0)
            {
                MessageBox.Show("Indent No already delivered. So you cannot modify.", "Indent");
                blData = false;
                txtIndentNo.Focus();
                return blData;
            }
            objData = null;
            if ((meIndentDate.Text.IndexOf(" ") >= 0) || (meIndentDate.Text.Length < 10))
            {
                MessageBox.Show("Enter Indent date!");
                blData = false;
                meIndentDate.Focus();
                return blData;
            }
            else
            {
                if (General.IsDateTime(meIndentDate.Text.ToString()))
                {
                    if (Convert.ToInt32(Convert.ToDateTime(meIndentDate.Text).ToString("yyyy")) < 1950)
                    {
                        MessageBox.Show("Enter valid  Date !");
                        blData = false;
                        meIndentDate.CausesValidation = true;
                        meIndentDate.Focus();
                        return blData;
                    }
                    if (Convert.ToDateTime(Convert.ToDateTime(meIndentDate.Text).ToString("dd/MM/yyyy")) > Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyyy")))
                    {
                        MessageBox.Show("Date should be less than to day");
                        meIndentDate.CausesValidation = true;
                        blData = false;
                        meIndentDate.Focus();
                        return blData;
                    }
                    
                }
                else
                {
                    MessageBox.Show("Enter valid Indent Date!");
                    meIndentDate.CausesValidation = true;
                    blData = false;
                    meIndentDate.Focus();
                    return blData;
                }
            }
            bool isItemExisted = false;
            for (int nRow = 0; nRow < gvIndentDetails.Rows.Count; nRow++)
            {
                if (gvIndentDetails.Rows[nRow].Cells["Qty"].Value.ToString().Trim().Length > 0)
                {
                    if (Convert.ToDouble(gvIndentDetails.Rows[nRow].Cells["Qty"].Value.ToString().Trim()) > 0)
                    {
                        isItemExisted = true;
                        break;
                    }
                }
            }
            if (isItemExisted == false)
            {
                blData = false;
                MessageBox.Show("Enter product quantity");
                return blData;
            }

            return blData;
        }

        private void txtIndentNo_Validating(object sender, CancelEventArgs e)
        {
            if (txtIndentNo.Text != "")
            {
                Int32 intIndNo = Convert.ToInt32(Convert.ToString(txtIndentNo.Text));
                txtIndentNo.Text = intIndNo.ToString();
                FillIndentData(intIndNo.ToString());
            }
        }

        private void txtIndentNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == false)
                e.Handled = true;
            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIndentNo.Text.Length > 0)
                {
                    Security objSecur = new Security();
                    if (objSecur.CanModifyDataUserAsPerBackDays(Convert.ToDateTime(meIndentDate.Text)) == false)
                    {
                        objSecur = null;
                        MessageBox.Show("You cannot manipulate backdays data!\n If you want to modify, Contact to IT-Department", "Indent");
                        txtIndentNo.Focus();
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Do you want to Delete " + txtIndentNo.Text + " Indent?",
                                               "CRM", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            objSQLDB = new SQLDB();
                            string strDelete = " DELETE from SP_INDENT_DETL " +
                                                " WHERE SPID_COMPANY_CODE='" + CommonData.CompanyCode +
                                                "' AND SPID_BRANCH_CODE='" + CommonData.BranchCode +
                                                "' AND SPID_INDENT_NUMBER=" + txtIndentNo.Text +
                                                "  AND SPID_FIN_YEAR='" + CommonData.FinancialYear + "'";

                            strDelete += " DELETE from SP_INDENT_HEAD " +
                                                " WHERE SPIH_COMPANY_CODE='" + CommonData.CompanyCode +
                                                "' AND SPIH_BRANCH_CODE='" + CommonData.BranchCode +
                                                "' AND SPIH_INDENT_NUMBER=" + txtIndentNo.Text +
                                                "  AND SPIH_FIN_YEAR='" + CommonData.FinancialYear + "'";

                            int intRec = objSQLDB.ExecuteSaveData(strDelete);
                            if (intRec > 0)
                            {
                                MessageBox.Show("Indent " + txtIndentNo.Text + " Deleted!");
                                txtIndentNo.Text = NewIndentNumber().ToString();
                            }

                        }
                       
                    }
                    objSecur = null;
                }
                else
                {
                    MessageBox.Show("Enter Indent Number.", "Indent");
                    txtIndentNo.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
               
                IsModify = false;
                objSQLDB = null;
            }
        }

        private void gvIndentDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridView dgv = (DataGridView)sender;
                if (e.ColumnIndex == 7)
                {
                    DataGridViewCell textBoxCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    if (textBoxCell != null)
                    {
                        gvIndentDetails.CurrentCell = textBoxCell;
                        dgv.BeginEdit(true);
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            meIndentDate.Text = "";
            gvIndentDetails.Rows.Clear();
            txtIndentNo.Text = NewIndentNumber().ToString();
            IsModify = false;
        }

        private void btnClearProd_Click(object sender, EventArgs e)
        {
            gvIndentDetails.Rows.Clear();
        }

        private void FillBranchGroupData()
        {
            IndentDB objIndent = new IndentDB();
            try
            {
                cbGroup.DataSource = null;
                cbGroup.Items.Clear();
                DataTable dtGroup = objIndent.IndentGroupList_Get(CommonData.CompanyCode.ToString(), CommonData.BranchCode, CommonData.DocMonth).Tables[0];
                DataRow dr = dtGroup.NewRow();
                dr[0] = "0";
                dr[1] = "Select";
                dtGroup.Rows.InsertAt(dr, 0);
                dr = null;
                if (dtGroup.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dtGroup.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = dataRow["ENAME"].ToString();
                        oclBox.Text = dataRow["GroupName"].ToString();
                        cbGroup.Items.Add(oclBox);
                        oclBox = null;
                    }

                }
                dtGroup = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objIndent = null;
            }

        }

        private void FillBranchData()
        {
            IndentDB objIndent = new IndentDB();
            try
            {
                DataTable dtBranch = objIndent.IndentStockPointList_Get(CommonData.CompanyCode.ToString()).Tables[0];
                DataRow dr = dtBranch.NewRow();
                dr[0] = "0";
                dr[1] = "Select";
                dtBranch.Rows.InsertAt(dr, 0);
                dr = null;
                if (dtBranch.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dtBranch.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = dataRow["branch_code"].ToString();
                        oclBox.Text = dataRow["branch_name"].ToString();
                        cbBranches.Items.Add(oclBox);
                        oclBox = null;
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objIndent = null;
            }

        }

        private void cbBranches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBranches.SelectedIndex > -1)
            {
                gvIndentDetails.Rows.Clear();
                FillIndentList();
            }
        }

        private void gvIndentList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvIndentList.Rows.Count > 0)
            {
                if (gvIndentList.Rows[e.RowIndex].Cells["IndentNo"].Value.ToString().Length >0)
                {
                    FillIndentData(gvIndentList.Rows[e.RowIndex].Cells["IndentNo"].Value.ToString());
                }
            }
        }

        private void gvIndentDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtIndentNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void meIndentDate_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
   }
}
