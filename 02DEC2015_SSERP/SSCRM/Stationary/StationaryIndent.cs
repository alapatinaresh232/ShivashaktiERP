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
using System.Net.Mail;
using System.Net;


namespace SSCRM
{
    public partial class StationaryIndent : Form
    {
        private SQLDB objSQLDB = null;
        private bool blIsCellQty = true;
        private int intCurrentRow = 0;
        private int intCurrentCell = 0;
        private string strIndentType = "";
        private StationaryItemsSearch childStationaryItemsSearch = null;
        public StationaryIndentList chldStationaryIndentList = null;

        string Mailbody = "";
        string BranchMailId = "";
        string SelfMailId = "";
        string ApprovedByMailId = "";
        int AppRefNo = 0;
        int IndentNo = 0;
        string IndentStatus = "";
        string IndentbySelf = "";
        string IndentbyBranch = "";
        int SlNo = 0;
        //string strEcode = "";
        string MailBranchCode = "";
        DataTable DtSendMail = new DataTable();  

        public StationaryIndent()
        {
            InitializeComponent();
        }
        public StationaryIndent(string sIndentType)
        {
            InitializeComponent();
            ScreenType = sIndentType;

        }
        string ScreenType = "", BranchCode = CommonData.BranchCode, CompanyCode = CommonData.CompanyCode, FinYear = CommonData.FinancialYear;
        
        int IndentNumber = 0;

        public StationaryIndent(string ScType, string CmpCode, string BrCode, int IndetntNo)
        {
            InitializeComponent();
            ScreenType = ScType;
            BranchCode = BrCode;
            CompanyCode = CmpCode;
            IndentNumber = IndetntNo;
        }

        private void btnItemsSearch_Click(object sender, EventArgs e)
        {
            StationaryItemsSearch ItemSearch = new StationaryItemsSearch("StationaryIndentBR");
            ItemSearch.objStationaryIndent = this;
            ItemSearch.ShowDialog();
        }

        private void CalculateTotals()
        {
            txtItemsCount.Text = gvIndentDetails.Rows.Count.ToString();
            if (gvIndentDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
                {
                    if (gvIndentDetails.Rows[i].Cells["ApprovedQty"].Value.ToString() != "" && gvIndentDetails.Rows[i].Cells["Amount"].Value.ToString() != "")
                    {
                        txtIndentAmt.Text = Convert.ToDouble(Convert.ToDouble(txtIndentAmt.Text) + Convert.ToDouble(gvIndentDetails.Rows[i].Cells["Amount"].Value)).ToString("f");
                    }
                }
            }
            else
            {
                txtIndentAmt.Text = "0.00";
            }
        }

        private void btnClearItems_Click(object sender, EventArgs e)
        {
            gvIndentDetails.Rows.Clear();
            CalculateTot();
        }

        private void StationaryIndent_Load(object sender, EventArgs e)
        {
            //if ((ScreenType != "MGR") && (ScreenType != "HEAD"))
            //{
            //    BranchCode = CommonData.BranchCode;
            //lblApp.Visible = false;
            //txtApprovedByEcode.Visible = false;
            //txtEmpName.Visible = false;
            //}
            if ((ScreenType == "BR") || (ScreenType == "MGR") || (ScreenType == "HEAD") || (ScreenType == ""))
            {
                lblApp.Visible = false;
                txtApprovedByEcode.Visible = false;
                txtEmpName.Visible = false;
                TxtIndentBy.Visible = false;
                lblIndentBy.Visible = false;
            }         
           
            else
            {
                if ((ScreenType == "SHEAD"))
                {
                    TxtIndentBy.Visible = false;
                    lblIndentBy.Visible = false;
                    lblApp.Visible = true;
                    txtApprovedByEcode.Visible = true;
                    txtEmpName.Visible = true;
                    gvIndentDetails.Columns["AvailableQty"].Visible = false;
                }
                else
                {
                    TxtIndentBy.Visible = true;
                    lblIndentBy.Visible = true;
                    lblApp.Visible = true;
                    txtApprovedByEcode.Visible = true;
                    txtEmpName.Visible = true;
                    gvIndentDetails.Columns["AvailableQty"].Visible = false;
                }
            }

            
            if (IndentNumber == 0)
                txtIndentNo.Text = GenerateIndentNo();
            else
            {
                txtIndentNo.Text = IndentNumber.ToString();
                txtIndentNo_Validated(null, null);
                if ((ScreenType != "MGR") && (ScreenType != "HEAD") && (ScreenType != "SHEAD"))
                {
                    btnSave.Text = "Save";
                    btnCancel.Text = "Cancel";
                }
                else if ((ScreenType != ""))
                {
                    btnSave.Text = "Approve";
                    btnCancel.Text = "Reject";

                }
                //btnDelete.Enabled = false;
                //btnClearItems.Enabled = false;
                //btnItemsSearch.Enabled = false;
            }
            dtIndentDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            TxtIndentBy.Text = CommonData.LogUserName;
            txtItemsCount.Text = "0.00";
            txtIndentAmt.Text = "0.00";
            gvIndentDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);

            gvIndentDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);

            //gvIndentDetails.DefaultCellStyle.BackColor = Color.LightGreen;
            //gvIndentDetails.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGreen;            
        }

        private string GenerateIndentNo()
        {
            objSQLDB = new SQLDB();
            string sIndNo = string.Empty;
            try
            {
                
                string sqlText = "SELECT ISNULL(MAX(SIH_INDENT_NUMBER),0)+1 FROM STATIONARY_INDENT_HEAD";
                sIndNo = objSQLDB.ExecuteDataSet(sqlText).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return sIndNo;
        }

        private void gvIndentDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            blIsCellQty = true;
            intCurrentRow = (((System.Windows.Forms.DataGridView)(sender))).CurrentCell.RowIndex;
            intCurrentCell = (((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex;
            //if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 3)
            //{
            //    TextBox txtQty = e.Control as TextBox;
            //    if (txtQty != null)
            //    {
            //        txtQty.MaxLength = 6;
            //        txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
            //    }
            //}
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 4)
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    txtQty.MaxLength = 6;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 5)
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    txtQty.MaxLength = 6;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 6)
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    txtQty.MaxLength = 6;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 7)
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    txtQty.MaxLength = 10;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 9)
            {
                blIsCellQty = false;
            }
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46 && blIsCellQty == true)
            {
                e.Handled = true;
                return;
            }

            //to allow decimals only teak plant
            if (intCurrentCell == 3 && e.KeyChar == 46 && blIsCellQty == true)
            {
                e.Handled = true;
                return;
            }
            // checks to make sure only 1 decimal is allowed
            else if (e.KeyChar == 46 && blIsCellQty == true)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }
        private void gvIndentDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridView dgv = (DataGridView)sender;
                if (e.ColumnIndex == 4)
                {
                    DataGridViewCell textBoxCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    if (textBoxCell != null)
                    {
                        gvIndentDetails.CurrentCell = textBoxCell;
                        dgv.BeginEdit(true);
                    }
                }
                //CalculateTotals();
            }
            if (e.ColumnIndex == gvIndentDetails.Columns["Delete"].Index)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    //string ProductId = gvProductDetails.Rows[e.RowIndex].Cells[gvProductDetails.Columns["ProductId"].Index].Value.ToString();
                    DataGridViewRow dgvr = gvIndentDetails.Rows[e.RowIndex];
                    gvIndentDetails.Rows.Remove(dgvr);
                    OrderSlNo();
                }
            }
        }

        private void OrderSlNo()
        {
            if (gvIndentDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
                {
                    gvIndentDetails.Rows[i].Cells["SLNO"].Value = i + 1;
                }
            }
        }

        private void gvIndentDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                if (Convert.ToString(gvIndentDetails.Rows[e.RowIndex].Cells["ReqQty"].Value) != "")
                {
                    gvIndentDetails.Rows[e.RowIndex].Cells["ApprovedQty"].Value = gvIndentDetails.Rows[e.RowIndex].Cells["ReqQty"].Value.ToString();
                    if (Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["Price"].Value) >= 0 && Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["ApprovedQty"].Value) >= 0)
                    {
                        gvIndentDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["ApprovedQty"].Value) * (Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["Price"].Value));
                        gvIndentDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["Amount"].Value).ToString("f");

                    }
                }
                else
                    gvIndentDetails.Rows[e.RowIndex].Cells["Amount"].Value = string.Empty;
            }
            else if (e.ColumnIndex == 3)
            {
                gvIndentDetails.Rows[e.RowIndex].Cells["AvailableQty"].Value = gvIndentDetails.Rows[e.RowIndex].Cells["DBAvailableQty"].Value;
            }
            else if (e.ColumnIndex == 5)
            {
                if (Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["Price"].Value) >= 0 && Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["ApprovedQty"].Value) >= 0)
                {
                    gvIndentDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["ApprovedQty"].Value) * (Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["Price"].Value));
                    gvIndentDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["Amount"].Value).ToString("f");
                }
            }
            CalculateTot();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        public void CalculateTot()
        {
            double iTotalAmt = 0;
            for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
            {
                if (gvIndentDetails.Rows[i].Cells["Amount"].Value.ToString() != "")
                    iTotalAmt += Convert.ToDouble(gvIndentDetails.Rows[i].Cells["Amount"].Value);
                else
                    iTotalAmt += 0;
            }
            txtIndentAmt.Text = iTotalAmt.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
           
            string strQry = "", strQryD = "";
            if (Checkdata())
            {
              if(txtIndentAmt.Text.Length==0)
              {
                  txtIndentAmt.Text = "0.00";
              }
            
                try 
                { 
                    if (ScreenType == "")
                {
                    DataSet dsExist = objSQLDB.ExecuteDataSet("SELECT SIH_INDENT_NUMBER FROM STATIONARY_INDENT_HEAD WHERE " +
                        //"SIH_COMPANY_CODE='" + CommonData.CompanyCode + "' AND SIH_BRANCH_CODE='" + BranchCode + "' AND "+
                        "SIH_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text));
                    if (dsExist.Tables[0].Rows.Count == 0)
                    {
                        txtIndentNo.Text = GenerateIndentNo();
                        strQry = " INSERT INTO STATIONARY_INDENT_HEAD" +
                                    "(SIH_COMPANY_CODE" +
                                    ",SIH_BRANCH_CODE" +
                                    ",SIH_STATE_CODE" +
                                    ",SIH_FIN_YEAR" +
                                    ",SIH_INDENT_NUMBER" +
                                    ",SIH_INDENT_DATE" +
                                    ",SIH_INDENT_AMOUNT" +
                                    ",SIH_INDENT_STATUS" +
                                    ",SIH_INDENT_TYPE" +
                                    ",SIH_CREATED_BY" +
                                    ",SIH_CREATED_DATE)" +
                                    " VALUES " +
                                    "('" + CompanyCode +
                                    "','" + BranchCode +
                                    "','" + BranchCode.Substring(3, 2) +
                                    "','" + FinYear +
                                    "','" + Convert.ToInt32(txtIndentNo.Text) +
                                    "','" + Convert.ToDateTime(dtIndentDate.Value).ToString("dd/MMM/yyyy") +
                                    "','" + Convert.ToDouble(txtIndentAmt.Text) +
                                    "','P','BR','" + CommonData.LogUserId +
                                    "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                    "')";
                    }
                    else
                    {
                        strQry = " UPDATE STATIONARY_INDENT_HEAD" +
                                    " SET SIH_INDENT_DATE='" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                    "',SIH_INDENT_AMOUNT=" + Convert.ToDouble(txtIndentAmt.Text) +
                                    " WHERE SIH_BRANCH_CODE='" + BranchCode +
                                    "' AND SIH_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text);
                        strQry += " DELETE FROM STATIONARY_INDENT_DETL" +
                                    " WHERE SID_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text) +
                                    " AND SID_BRANCH_CODE='" + BranchCode + "'";
                    }
                }
                if (ScreenType == "BR")
                {
                    strQry = " UPDATE STATIONARY_INDENT_HEAD" +
                                   " SET SIH_INDENT_DATE='" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                   "',SIH_INDENT_AMOUNT=" + Convert.ToDouble(txtIndentAmt.Text) +
                                   " WHERE SIH_BRANCH_CODE='" + BranchCode +
                                   "' AND SIH_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text);
                    strQry += " DELETE FROM STATIONARY_INDENT_DETL" +
                                " WHERE SID_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text) +
                                " AND SID_BRANCH_CODE='" + BranchCode + "'";
                }
                if (ScreenType == "SELF")
                {
                    string SqlCmd = null;
                    DataTable dt = new DataTable();
                    try
                    {
                        SqlCmd = "SELECT BM.COMPANY_CODE CompanyCode" +
                                               ",BM.BRANCH_CODE BranchCode" +
                                               ",MEMBER_NAME " +
                                               " FROM EORA_MASTER EM " +
                                               " INNER JOIN BRANCH_MAS BM ON BM.BRANCH_CODE=EM.BRANCH_CODE " +
                                               " INNER JOIN COMPANY_MAS CM ON CM.CM_COMPANY_CODE=BM.COMPANY_CODE WHERE EM.ECODE='" + CommonData.LogUserEcode + "'";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    dt = objSQLDB.ExecuteDataSet(SqlCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        CompanyCode = dt.Rows[0]["CompanyCode"].ToString();
                        BranchCode = dt.Rows[0]["BranchCode"].ToString();
                        
                    }
                    DataSet dsExist = objSQLDB.ExecuteDataSet(" SELECT * FROM STATIONARY_INDENT_HEAD WHERE SIH_COMPANY_CODE='" + CompanyCode + "' AND SIH_BRANCH_CODE='" + BranchCode
                                                              + "' AND SIH_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text) + "  AND  SIH_ECODE=" + CommonData.LogUserEcode);
                    if (dsExist.Tables[0].Rows.Count == 0)
                    {
                        strQry = " INSERT INTO STATIONARY_INDENT_HEAD" +
                                    "(SIH_COMPANY_CODE" +
                                    ",SIH_BRANCH_CODE" +
                                    ",SIH_STATE_CODE" +
                                    ",SIH_FIN_YEAR" +
                                     ",SIH_ECODE" +
                                    ",SIH_INDENT_NUMBER" +
                                    ",SIH_INDENT_DATE" +
                                    ",SIH_INDENT_AMOUNT" +
                                    ",SIH_INDENT_STATUS" +
                                    ",SIH_INDENT_TYPE" +
                                    ",SIH_INDENT_APPROVED_BY_HEAD" +
                                    ",SIH_CREATED_BY" +
                                    ",SIH_CREATED_DATE)" +
                                    " VALUES " +
                                    "('" + CompanyCode +
                                    "','" + BranchCode +
                                    "','" + BranchCode.Substring(3, 2) +
                                    "','" + FinYear +
                                    "','" + CommonData.LogUserEcode +
                                    "','" + Convert.ToInt32(txtIndentNo.Text) +
                                    "','" + Convert.ToDateTime(dtIndentDate.Value).ToString("dd/MMM/yyyy") +
                                    "','" + Convert.ToDouble(txtIndentAmt.Text) +
                                    "','P','SELF', '" + Convert.ToInt32(txtApprovedByEcode.Text) +"','" + CommonData.LogUserId +
                                    "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                    "')";
                    }
                    else
                    {
                        strQry = " UPDATE STATIONARY_INDENT_HEAD" +
                                    " SET SIH_INDENT_DATE='" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                    "',SIH_INDENT_AMOUNT=" + Convert.ToDouble(txtIndentAmt.Text) +
                                    " WHERE SIH_BRANCH_CODE='" + BranchCode +
                                    "' AND SIH_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text);
                        strQry += " DELETE FROM STATIONARY_INDENT_DETL" +
                                    " WHERE SID_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text) +
                                    " AND SID_BRANCH_CODE='" + BranchCode + "'";
                    }
                }
                else if (ScreenType == "MGR")
                {
                    strQry = " UPDATE STATIONARY_INDENT_HEAD" +
                                " SET SIH_INDENT_APPROVED_BY_MGR='" + CommonData.LogUserEcode +
                                "',SIH_APPROVED_BY_MGR_DATE='" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                "',SIH_INDENT_STATUS='V'" +
                                " WHERE SIH_BRANCH_CODE='" + BranchCode +
                                "' AND SIH_INDENT_NUMBER=" + IndentNumber;
                    strQry += " DELETE FROM STATIONARY_INDENT_DETL" +
                                " WHERE SID_BRANCH_CODE='" + BranchCode +
                                "' AND SID_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text);
                }
                else if (ScreenType == "HEAD")
                {
                    strQry = " SELECT ISNULL(MAX(SIH_APPROVAL_REF_NO),0)+1 FROM STATIONARY_INDENT_HEAD";
                    int imax = Convert.ToInt32(objSQLDB.ExecuteDataSet(strQry).Tables[0].Rows[0][0]);
                    strQry = " UPDATE STATIONARY_INDENT_HEAD SET " +
                                "SIH_INDENT_APPROVED_BY_HEAD='" + CommonData.LogUserEcode +
                                "',SIH_APPROVED_BY_HEAD_DATE='" + CommonData.CurrentDate +
                                "',SIH_INDENT_STATUS='A',SIH_APPROVAL_REF_NO='" + imax +
                                "' WHERE SIH_BRANCH_CODE='" + BranchCode +
                                "' AND SIH_INDENT_NUMBER=" + IndentNumber;
                    strQry += " DELETE FROM STATIONARY_INDENT_DETL " +
                                " WHERE SID_BRANCH_CODE='" + BranchCode +
                                "' AND SID_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text);
                }
                else if (ScreenType == "SHEAD")
                {
                    strQry = " SELECT ISNULL(MAX(SIH_APPROVAL_REF_NO),0)+1 FROM STATIONARY_INDENT_HEAD";
                    int imax = Convert.ToInt32(objSQLDB.ExecuteDataSet(strQry).Tables[0].Rows[0][0]);
                    strQry = " UPDATE STATIONARY_INDENT_HEAD SET " +
                                "SIH_INDENT_APPROVED_BY_HEAD='" + CommonData.LogUserEcode +
                                "',SIH_APPROVED_BY_HEAD_DATE='" + CommonData.CurrentDate +
                                "',SIH_INDENT_STATUS='A',SIH_APPROVAL_REF_NO='" + imax +
                                "' WHERE SIH_BRANCH_CODE='" + BranchCode +
                                "' AND SIH_INDENT_NUMBER=" + IndentNumber;
                    strQry += " DELETE FROM STATIONARY_INDENT_DETL " +
                                " WHERE SID_BRANCH_CODE='" + BranchCode +
                                "' AND SID_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text);
                }
                int ival = objSQLDB.ExecuteSaveData(strQry);
                if (ival > 0)
                {
                    if (gvIndentDetails.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
                        {
                            if (gvIndentDetails.Rows[i].Cells["ReqQty"].Value.ToString() == "")
                                gvIndentDetails.Rows[i].Cells["ReqQty"].Value = "0";
                            if (gvIndentDetails.Rows[i].Cells["Amount"].Value.ToString() == "")
                                gvIndentDetails.Rows[i].Cells["Amount"].Value = "0.00";
                            if (gvIndentDetails.Rows[i].Cells["AvailableQty"].Value.ToString() == "")
                                gvIndentDetails.Rows[i].Cells["AvailableQty"].Value = "0";
                            if (gvIndentDetails.Rows[i].Cells["ApprovedQty"].Value.ToString() == "")
                                gvIndentDetails.Rows[i].Cells["ApprovedQty"].Value = "0";
                            if (gvIndentDetails.Rows[i].Cells["ReqQty"].Value.ToString().Trim() != "0"
                                || gvIndentDetails.Rows[i].Cells["ApprovedQty"].Value.ToString().Trim() != "0")
                            {
                                strQryD += " INSERT INTO STATIONARY_INDENT_DETL" +
                                            "(SID_COMPANY_CODE" +
                                            ",SID_STATE_CODE" +
                                            ",SID_BRANCH_CODE" +
                                            ",SID_FIN_YEAR" +
                                            ",SID_INDENT_NUMBER" +
                                            ",SID_INDENT_SL_NO" +
                                            ",SID_ITEM_ID" +
                                            ",SID_ITEM_AVAILABLE_QTY" +
                                            ",SID_ITEM_REQ_QTY" +
                                            ",SID_ITEM_HO_RECON_QTY" +
                                            ",SID_ITEM_PRICE" +
                                            ",SID_ITEM_AMOUNT" +
                                            ",SID_INDENT_REMARKS)" +
                                            " VALUES(" +
                                            "'" + CompanyCode +
                                            "','" + BranchCode.Substring(3, 2) +
                                            "','" + BranchCode +
                                            "','" + FinYear +
                                            "','" + Convert.ToInt32(txtIndentNo.Text) +
                                            "','" + Convert.ToInt32(i + 1) +
                                            "','" + gvIndentDetails.Rows[i].Cells["ItemID"].Value +
                                            "','" + gvIndentDetails.Rows[i].Cells["AvailableQty"].Value +
                                            "','" + gvIndentDetails.Rows[i].Cells["ReqQty"].Value +
                                            "','" + gvIndentDetails.Rows[i].Cells["ApprovedQty"].Value +
                                            "','" + gvIndentDetails.Rows[i].Cells["Price"].Value +
                                            "'," + Convert.ToDouble(gvIndentDetails.Rows[i].Cells["Amount"].Value) +
                                            ",'" + gvIndentDetails.Rows[i].Cells["Remarks"].Value +
                                            "')";
                            }
                        }
                        int ivals = objSQLDB.ExecuteSaveData(strQryD);
                        objSQLDB = null;
                        if (ivals > 0)
                        {
                            MessageBox.Show("Data saved successfully", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (ScreenType != "" && ScreenType!="SELF")
                            {
                                ((StationaryIndentList)chldStationaryIndentList).GetGridBind();
                                this.Close();
                                this.Dispose();
                            }
                            else
                            {
                                gvIndentDetails.Rows.Clear();
                                txtEmpName.Text = "";
                                txtApprovedByEcode.Text = "";
                                StationaryIndent_Load(null, null);
                            }
                            if (ScreenType == "HEAD" || (ScreenType == "SHEAD"))
                            {
                                SendStationaryIndentApprovalDetails();
                            }
                        }
                        else
                            //strQry = " DELETE FROM STATIONARY_INDENT_HEAD" +
                            //        " WHERE SIH_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text) +
                            //        " AND SIH_BRANCH_CODE='" + BranchCode + "'";
                          
                            MessageBox.Show("Data Not saved ", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSQLDB = null;
                }
               
            }
            
        }

        private bool Checkdata()
        {
            if (ScreenType == "SELF")
            {
                if (txtEmpName.Text.Length == 0)
                {
                    MessageBox.Show("Please Enter Approved By Ecode", "Stationary", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
             if (gvIndentDetails.Rows.Count == 0)
            {
                MessageBox.Show("Select Atlease One item!", "Stationary", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
             
            //bool bflag = true;
            for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
            {
                if (gvIndentDetails.Rows[0].Cells["ReqQty"].Value.ToString().Trim()=="")
                {
                    MessageBox.Show("Please Enter ReqQty", "Stationary", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                //if (gvIndentDetails.Rows[i].Cells["ReqQty"].Value.ToString().Trim() != "")
                //{
                //    return true;
                //}
              
            }
            return true;
        }
        private void SendStationaryIndentApprovalDetails()
        {

           objSQLDB = new SQLDB();

            //DataTable dt = new DataTable();
           string strCommand = "";
           try
           {

                strCommand = " EXEC STATIONARY_INDENT_APPROVAL_MAIL '" + CompanyCode + "','" + BranchCode + "'," + IndentNumber + "";
           }
           catch(Exception ex)
           {
               MessageBox.Show(ex.ToString());
           }

            DtSendMail = objSQLDB.ExecuteDataSet(strCommand).Tables[0];

            if (DtSendMail.Rows.Count > 0)
            {
                BuildStationaryIndentStatusMailBody(DtSendMail);               
            }

        }
        private void BuildStationaryIndentStatusMailBody(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IndentNo = Convert.ToInt32(dt.Rows[i]["rs_indent_no"].ToString());
                    AppRefNo = Convert.ToInt32(dt.Rows[i]["rs_appr_ref_no"].ToString());
                    IndentStatus = dt.Rows[i]["rs_indent_status"].ToString();
                    SlNo = Convert.ToInt32(dt.Rows[i]["rs_indent_sl_no1"].ToString());
                    MailBranchCode = dt.Rows[i]["rs_branch_code"].ToString();


                    if (dt.Rows[i]["rs_indent_type"].Equals("SELF"))
                    {
                        SelfMailId = dt.Rows[i]["rs_self_mailid"].ToString();
                        IndentbySelf = dt.Rows[i]["rs_indentbySelf"].ToString();
                        ApprovedByMailId = dt.Rows[i]["rs_ApprovedBy_mailid"].ToString();
                    }
                    else
                    {
                        BranchMailId = dt.Rows[i]["rs_branch_mailid"].ToString();
                        IndentbyBranch = dt.Rows[i]["rs_branch_name"].ToString();
                    }
                    if ((SlNo < 2) && (SlNo != 0))
                    {
                        Mailbody = "<br /><br /><table padding='0' font-family= 'Segoe UI' cellpadding='5' cellspacing='0' border='1'>";

                        Mailbody += "<tr><td  colspan=3;> IndentNo:" + Convert.ToInt32(dt.Rows[i]["rs_indent_no"]) +
                            "<br/><br/> Indent Date : " + Convert.ToDateTime(dt.Rows[i]["rs_indent_date"]).ToString("dd/MMM/yyyy");
                        if (IndentbySelf != "")
                        {
                            Mailbody += "<br/><br/> Indent BY:" + dt.Rows[i]["rs_indentbySelf"];
                        }
                        if (IndentbyBranch != "")
                        {
                            Mailbody += "<br/><br/> Indent BY:" + dt.Rows[i]["rs_branch_name"];
                        }

                        Mailbody += "<td colspan=3> Approval Date:" + Convert.ToDateTime(dt.Rows[i]["rs_Apprby_date"]).ToString("dd/MMM/yyyy") +
                            " <br/><br/> Approval RefNO:" + dt.Rows[i]["rs_appr_ref_no"] +
                             "<br/><br/> Approved BY : " + dt.Rows[i]["rs_apprby_hod_name"] + "</td></tr>";
                    }

                    if ((SlNo == 1))
                    {
                        //Mailbody += "<br /><br /><table padding='0' font-family= 'Segoe UI' cellpadding='5' cellspacing='0' border='1'>";
                        Mailbody += "<tr ><td colspan=\"6\"><a href=\"www.shivashakthigroup.com\">" +
                              "<img src=\"http://shivashakthigroup.com/wp-content/uploads/2013/01/logo.png\" alt=\"Shivashakthi Group of Companies\"/></a></td></tr>";

                        Mailbody += "<tr style =\"background-color:#6FA1D2; color:#ffffff;\"><td>Sl.No</td><td colspan=2>Item Name</td> <td>Avilabel Qty</td><td>Indent Qty</td><td >Aapporved Qty</td></tr>";


                    }


                    if (SlNo == 1)
                    {
                        Mailbody += "<tr><td>" + dt.Rows[i]["rs_indent_sl_no1"].ToString() + "</td>" +
                      "<td colspan=2>" + dt.Rows[i]["rs_item_name1"].ToString() + "</td>" +
                      "<td>" + dt.Rows[i]["rs_avilable_qty1"].ToString() + "</td>" +
                      "<td>" + dt.Rows[i]["rs_item_req_qty1"].ToString() + "</td>" +
                      "<td>" + dt.Rows[i]["rs_appr_qty1"].ToString() + "</td></tr>";
                    }
                    else
                    {
                        Mailbody += "<tr><td>" + dt.Rows[i]["rs_indent_sl_no1"].ToString() + "</td>" +
                            "<td colspan=2>" + dt.Rows[i]["rs_item_name1"].ToString() + "</td>" +
                            "<td>" + dt.Rows[i]["rs_avilable_qty1"].ToString() + "</td>" +
                            "<td>" + dt.Rows[i]["rs_item_req_qty1"].ToString() + "</td>" +
                            "<td>" + dt.Rows[i]["rs_appr_qty1"].ToString() + "</td></tr>";
                    }
                }

                Mailbody += "</table>";
                if ((BranchMailId.Length > 0))
                {
                    SendStationaryIndentapprovalDetailsMail(IndentNo, AppRefNo);

                    BranchMailId = "";
                    SelfMailId = "";

                }
                else if ((SelfMailId.Length > 0))
                {
                    BranchMailId = SelfMailId;
                    //SendStationaryIndentapprovalDetailsMail(IndentNo, AppRefNo);

                    BranchMailId = "";
                    SelfMailId = "";

                }
                Mailbody = "";
            }

        }
        



        
        public string SendStationaryIndentapprovalDetailsMail(int sIndentNo, int sAppRefNo)
        {
          
            //String[] addrCC = { "laki.lakshmi99@gmail.com" };

            MailAddress fromAddress = new MailAddress("ssbplitho@gmail.com", "SSERP ::Stationary ");

            MailAddress toAddress = new MailAddress(BranchMailId);
            const string fromPassword = "ssbplitho5566";
            //try
            //{
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = "SSERP Stationary Indent "+IndentNo+" Approval :: " + AppRefNo,
                Body = "</br></br></br>" + Mailbody + "</br></br><table width='100%'><tr><td align='center'></td></tr><tr><td style='height:50px'></td></tr><tr><td></td></tr></table>  This is server generated mail please do not reply to this mail",

                IsBodyHtml = true
            })
            {
                //for (int i = 0; i < addrCC.Length; i++)
                //    message.CC.Add(addrCC[i]);
                //message.To.Add("lakshminallagatla@gmail.com");
                message.To.Add(BranchMailId);
                message.CC.Add("audittripsheet@gmail.com");
                message.CC.Add("auditdata@sivashakthi.net");
                message.CC.Add("ssbplstore9@gmail.com");
                if (ApprovedByMailId.Length > 6)
                {
                    message.CC.Add(ApprovedByMailId);
                }
                smtp.Send(message);
                return "Yes";
            }
            //}
            //catch (Exception ex)
            //{
            //    return ex.ToString();
            //}
        }


        //private bool Checkdata()
        //{
        //    if (ScreenType == "SELF")
        //    {
        //        if (txtEmpName.Text.Length == 0)
        //        {
        //            MessageBox.Show("Please Enter Approved By Ecode", "Stationary", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return false;
        //        }
        //    }
        //    if (gvIndentDetails.Rows.Count == 0)
        //    {
        //        MessageBox.Show("Select Atlease One item!", "Stationary", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //    //bool bflag = true;
        //    for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
        //    {
        //        if (gvIndentDetails.Rows[i].Cells["ReqQty"].Value.ToString().Trim() == "")
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}


        private void txtIndentNo_Validated(object sender, EventArgs e)
        {
            if (txtIndentNo.Text != "")
            {
                objSQLDB = new SQLDB();
                string sqlQry = " SELECT * FROM STATIONARY_INDENT_HEAD  WHERE SIH_COMPANY_CODE='" + CompanyCode + 
                    "' AND SIH_BRANCH_CODE='" + BranchCode + 
                    "' AND SIH_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text)+";";
                sqlQry += " SELECT * FROM STATIONARY_INDENT_DETL A "+
                    "INNER JOIN STATIONARY_ITEMS_MASTER B ON A.SID_ITEM_ID=B.SIM_ITEM_CODE "+
                    "WHERE SID_COMPANY_CODE='" + CompanyCode + 
                    "' AND SID_BRANCH_CODE='" + BranchCode + 
                    "' AND SID_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text);
                DataSet ds = objSQLDB.ExecuteDataSet(sqlQry);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtIndentDate.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["SIH_INDENT_DATE"]);
                    txtIndentAmt.Text = ds.Tables[0].Rows[0]["SIH_INDENT_AMOUNT"].ToString();
                    FinYear = ds.Tables[0].Rows[0]["SIH_FIN_YEAR"].ToString();
                    txtApprovedByEcode.Text = ds.Tables[0].Rows[0]["SIH_INDENT_APPROVED_BY_HEAD"].ToString();
                    if (ds.Tables[0].Rows[0]["SIH_INDENT_STATUS"].ToString() != "P" && ScreenType == "")
                    {
                        btnSave.Enabled = false;
                        btnDelete.Enabled = false;
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        btnDelete.Enabled = true;
                    }
                    txtItemsCount.Text = ds.Tables[1].Rows.Count.ToString();
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        gvIndentDetails.Rows.Clear();
                        GetBindData(ds.Tables[1]);
                    }
                }
                else
                {
                    //txtIndentDate.Text = "";
                    txtIndentAmt.Text = "";
                    txtItemsCount.Text = "";
                    txtApprovedByEcode.Text = "";
                    txtEmpName.Text = "";
                    btnSave.Enabled = true;
                    btnDelete.Enabled = true;
                    FinYear = CommonData.FinancialYear;
                    gvIndentDetails.Rows.Clear();

                }
            }
        }

        public void GetBindData(DataTable dt)
        {
            gvIndentDetails.Rows.Clear();
            int intRow = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellItemID = new DataGridViewTextBoxCell();
                cellItemID.Value = dt.Rows[i]["SID_ITEM_ID"].ToString();
                tempRow.Cells.Add(cellItemID);

                DataGridViewCell cellItemName = new DataGridViewTextBoxCell();
                cellItemName.Value = dt.Rows[i]["SIM_ITEM_NAME"].ToString();
                tempRow.Cells.Add(cellItemName);
             
                 DataGridViewCell cellAvailQty = new DataGridViewTextBoxCell();
                 cellAvailQty.Value = dt.Rows[i]["SID_ITEM_AVAILABLE_QTY"].ToString();
                 tempRow.Cells.Add(cellAvailQty);
         
                DataGridViewCell cellReqQty = new DataGridViewTextBoxCell();
                cellReqQty.Value = dt.Rows[i]["SID_ITEM_REQ_QTY"].ToString();
                tempRow.Cells.Add(cellReqQty);

                DataGridViewCell cellApprQty = new DataGridViewTextBoxCell();
                cellApprQty.Value = dt.Rows[i]["SID_ITEM_HO_RECON_QTY"].ToString();
                tempRow.Cells.Add(cellApprQty);

                DataGridViewCell cellRate = new DataGridViewTextBoxCell();
                cellRate.Value = Convert.ToDouble(dt.Rows[i]["SID_ITEM_PRICE"]).ToString("f");
                tempRow.Cells.Add(cellRate);

                DataGridViewCell cellAmount = new DataGridViewTextBoxCell();
                cellAmount.Value = dt.Rows[i]["SID_ITEM_AMOUNT"].ToString();
                tempRow.Cells.Add(cellAmount);

                DataGridViewCell cellAvailQty1 = new DataGridViewTextBoxCell();
                cellAvailQty1.Value = dt.Rows[i]["SID_ITEM_AVAILABLE_QTY"].ToString();
                tempRow.Cells.Add(cellAvailQty1);

                DataGridViewCell cellRem = new DataGridViewTextBoxCell();
                cellRem.Value = dt.Rows[i]["SID_INDENT_REMARKS"].ToString();
                tempRow.Cells.Add(cellRem);
                intRow = intRow + 1;
                gvIndentDetails.Rows.Add(tempRow);

                if ((ScreenType == "MGR") || (ScreenType == "HEAD" || ScreenType == "SHEAD"))
                    gvIndentDetails.Columns[5].ReadOnly = false;
                else
                {
                    gvIndentDetails.Columns[3].ReadOnly = false;
                    gvIndentDetails.Columns[4].ReadOnly = false;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            string strQryD = " DELETE FROM STATIONARY_INDENT_DETL WHERE SID_COMPANY_CODE='" + CompanyCode + "' AND SID_BRANCH_CODE='" + BranchCode + "' AND SID_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text);
            strQryD += " DELETE FROM STATIONARY_INDENT_HEAD WHERE SIH_COMPANY_CODE='" + CompanyCode + "' AND SIH_BRANCH_CODE='" + BranchCode + "' AND SIH_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text);
            int ivals = objSQLDB.ExecuteSaveData(strQryD);
            if (ivals > 0)
                MessageBox.Show("Data deleted successfully", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Data Not deleted", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            StationaryIndent_Load(null, null);
            txtEmpName.Text = "";
            txtApprovedByEcode.Text = "";
            gvIndentDetails.Rows.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if ((ScreenType == "MGR") || (ScreenType == "HEAD") || ScreenType == "SHEAD")
            {
                DialogResult dlgResult = MessageBox.Show("Do you want reject this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    objSQLDB = new SQLDB();
                    string strQry = "UPDATE STATIONARY_INDENT_HEAD SET SIH_INDENT_STATUS='R' WHERE SIH_COMPANY_CODE='" + CompanyCode + "' AND SIH_BRANCH_CODE='" + BranchCode + "' AND SIH_INDENT_NUMBER=" + IndentNumber;
                    int ivals = objSQLDB.ExecuteSaveData(strQry);
                    if (ivals > 0)
                        MessageBox.Show("Rejected this record.", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("This Not Reject.", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Close();
                this.Dispose();
                chldStationaryIndentList.GetGridBind();
            }
            else
            {
                StationaryIndent_Load(null, null);
                gvIndentDetails.Rows.Clear();
            }
        }

        private void txtEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtEcode_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtApprovedByEcode.Text.Length > 4)
            {
                txtEmpName.Text = GetEmpName(txtApprovedByEcode.Text);
            }
            else
                txtEmpName.Text = "";

        }
        private string GetEmpName(string strEcode)
        {
           string strName = "";
            objSQLDB=new SQLDB();
            DataTable dt = new DataTable();
            string strSql="";
            try
            {
                objSQLDB = new SQLDB();
                 strSql = "SELECT CAST(ECODE AS VARCHAR)+'-'+MEMBER_NAME , MEMBER_NAME FROM EORA_MASTER WHERE ECODE = " + strEcode;
                 dt = objSQLDB.ExecuteDataSet(strSql).Tables[0];
                 if (dt.Rows.Count > 0)
                 {
                     strName = dt.Rows[0]["MEMBER_NAME"].ToString();
                 }
                 else
                 {
                     strName = "";
                 }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }                
        
            finally
            {
                objSQLDB = null;
            }
            return strName;
        }

        private void txtApprovedByEcode_TextChanged(object sender, EventArgs e)
        {
            if (txtApprovedByEcode.Text.Length > 4)
            {
                txtEmpName.Text = GetEmpName(txtApprovedByEcode.Text);
            }
            else
                txtEmpName.Text = "";
        }

   

       
    }
}
