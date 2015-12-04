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
using SSCRM.App_Code;
using System.Net.Mail;
using System.Net;

namespace SSCRM
{
    public partial class StationaryBrocher : Form
    {
        private SQLDB objSQLDB = null;
        private bool blIsCellQty = true;
        private int intCurrentRow = 0;
        private int intCurrentCell = 0;
        public StationaryBrochureList chldStationaryBrochureList = null;
        bool flagText = false;
        DataSet ds = null;
        DateTime dtpGRNDate;
        string Mailbody = "";
        string BranchMailId = "";
        string BranchCCMailId = "";
        string SelfMailId = "";
        int DCNo = 0;
        int AppRefNo = 0;
        int IndentNo = 0;
        string IndentStatus = "";
        string IndentbySelf = "";
        string IndentbyBranch = "";
        int SlNo = 0;
        string[] EmpCode;
        string MailBranchCode = "";
        string CorrierOrTransport = "";
        DataTable DtSendMail = new DataTable(); 
        public StationaryBrocher()
        {
            InitializeComponent();
        }

        string ScreenType = "", CompanyCode = "", BranchCode = "",Ecode="", StateCode = "", FinYear = "";
        int IndentNumber = 0;

        public StationaryBrocher(string ScType, string CmpyCode, string BrCode,string StECode ,string StCode, string Fin_Year, int IndetntNo)
        {
            InitializeComponent();
            ScreenType = ScType;
            BranchCode = BrCode;
            Ecode = StECode;
            IndentNumber = IndetntNo;
            CompanyCode = CmpyCode;
            FinYear = Fin_Year;
            StateCode = StCode;
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
        }

        private void StationaryIndent_Load(object sender, EventArgs e)
        {
            txtDispatchNo.ReadOnly = true;
            cmdDoorDeliveryFlag.SelectedIndex = 0;
            txtDispatchNo.ReadOnly = true;
            dtIndentDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            txtItemsCount.Text = "0.00";
            txtIndentAmt.Text = "0.00";
            txtIndentNo.Text = IndentNumber.ToString();
            txtDispatchNo.Text = GenerateIndentNo();
            txtIndentNo_Validated(null, null);
            gvIndentDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10,
                                                        System.Drawing.FontStyle.Regular);

            gvIndentDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10,
                                                        System.Drawing.FontStyle.Regular);
            objSQLDB = new SQLDB();
            cmdTransportOrCorrier.SelectedIndex = 0;
        }

        private string GenerateIndentNo()
        {
            objSQLDB = new SQLDB();
             string sqlText="";
             DataTable dt = new DataTable();
            string sIndNo = string.Empty;
            try
            {
                if (BranchCode != "")
                {
                    string SqlCmd = "  SELECT DISTINCT COMPANY_CODE FROM  BRANCH_MAS " +
                            " WHERE BRANCH_CODE='" + BranchCode + "'";
                    if (SqlCmd.Length > 0)
                    {
                        dt = objSQLDB.ExecuteDataSet(SqlCmd).Tables[0];
                    }
                    if (dt.Rows.Count > 0)
                    {
                        CompanyCode = dt.Rows[0]["COMPANY_CODE"].ToString();
                    }


                    sqlText = "SELECT ISNULL(MAX(SDN_DELIVERY_NOTE_NO),0)+1 FROM   BRANCH_MAS" +
                                      " INNER JOIN  STATIONARY_DELIVERY_NOTE  ON SDN_TO_BRANCH_CODE = BRANCH_CODE " +
                                      "WHERE COMPANY_CODE = '" + CompanyCode + "'   AND SDN_FIN_YEAR='" + CommonData.FinancialYear + "'";

                  
                    //sqlText = "SELECT ISNULL(MAX(SDN_DELIVERY_NOTE_NO),0)+1 FROM STATIONARY_DELIVERY_NOTE WHERE SDN_COMPANY_CODE='" + CommonData.CompanyCode + "' AND SDN_BRANCH_CODE='" + CommonData.BranchCode + "' AND SDN_FIN_YEAR='" + CommonData.FinancialYear + "'";
                    sIndNo = objSQLDB.ExecuteDataSet(sqlText).Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return sIndNo;
        }

        private void gvIndentDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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
           if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 4)
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
           if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 8)
           {
               TextBox txtQty = e.Control as TextBox;
               if (txtQty != null)
               {
                   flagText = false;
                   txtQty.MaxLength = 6;
                   txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
               }
           }
           if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 9)
           {
               TextBox txtQty = e.Control as TextBox;
               if (txtQty != null)
               {
                   flagText = false;
                   txtQty.MaxLength = 7;
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
           if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 11)
           {
               TextBox txtQty = e.Control as TextBox;
               if (txtQty != null)
               {
                   flagText = true;
                   txtQty.MaxLength = 200;
                   txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
               }
           }
           
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
            // checks to make sure only 1 decimal is allowed
            else if (e.KeyChar == 46 && flagText == false)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void gvIndentDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                if (Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["Price"].Value) >= 0 && Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["DispatchQty"].Value) >= 0)
                {
                    gvIndentDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["DispatchQty"].Value) * (Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["Price"].Value));
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
        private bool Checkdata()
        {
            if (Convert.ToDateTime(CommonData.CurrentDate) > dtpGRNDate.AddDays(Convert.ToInt32(CommonData.LogUserBackDays)))
            {
                MessageBox.Show("You are not allowed to Enter/Update for backdays Data", "Stationary :: GRN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (Convert.ToDateTime(CommonData.CurrentDate) > dtIndentDate.Value.AddDays(Convert.ToInt32(CommonData.LogUserBackDays)))
            {
                MessageBox.Show("You are not allowed to Enter/Update for backdays Data", "Stationary :: GRN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
            {
                if (gvIndentDetails.Rows[i].Cells["DispatchQty"].Value.ToString().Trim() == "")
                {
                    MessageBox.Show("Enter DispatchQty", "Stationary", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Checkdata())
            {
                objSQLDB = new SQLDB();
                int ivals = 0;
                string sqlText = "";
                DataTable dt = new DataTable();
                string strQry = "", strQryD = "", strQryF = "";
                strQry = " UPDATE STATIONARY_INDENT_HEAD" +
                        " SET SIH_INDENT_STATUS='D'" +
                        " WHERE SIH_BRANCH_CODE='" + BranchCode +
                        "' AND SIH_INDENT_NUMBER=" + IndentNumber;

                string sTransportName = "";
                if (cmdTransportOrCorrier.SelectedIndex == 2)
                    sTransportName = txtTransportName.Text;
                else
                {
                    if (cmbTransporterOrCorrierName.Items.Count > 0)
                        sTransportName = cmbTransporterOrCorrierName.SelectedValue.ToString();
                    else
                        sTransportName = "";
                }
                int ival = 0;
                try
                {
                    ival = objSQLDB.ExecuteSaveData(strQry);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (ival > 0)
                {
                    if (txtEcode.Text != "Branch Indent" && txtEcode.Text != "")
                    {
                        EmpCode = txtEcode.Text.ToString().Split('-');

                    }

                    if (gvIndentDetails.Rows.Count > 0)
                    {
                        if (txtBagsCount.Text.Length == 0)
                            txtBagsCount.Text = "0";
                        if (txtTotalFreight.Text.Length == 0)
                            txtTotalFreight.Text = "0";
                        if (txtAdvPaid.Text.Length == 0)
                            txtAdvPaid.Text = "0";
                        if (txtToPay.Text.Length == 0)
                            txtToPay.Text = "0";
                        DataSet dsCnt = objSQLDB.ExecuteDataSet("SELECT COUNT(*) FROM STATIONARY_DELIVERY_NOTE WHERE SDN_TO_BRANCH_CODE = '" + BranchCode + "' AND SDN_REF_INDENT_NO = " + IndentNumber);
                        if (Convert.ToInt32(dsCnt.Tables[0].Rows[0][0]) == 0)
                        {
                            txtDispatchNo.Text = GenerateIndentNo();
                            strQryD += " INSERT INTO STATIONARY_DELIVERY_NOTE(SDN_COMPANY_CODE " +
                                                                             ",SDN_BRANCH_CODE " +
                                                                             ",SDN_STATE_CODE " +
                                                                             ",SDN_FIN_YEAR " +
                                                                             ",SDN_TO_COMP_CODE " +
                                                                             ",SDN_TO_BRANCH_CODE " +
                                                                             ",SDN_APPROVAL_REF_NO " +
                                                                             ",SDN_LR_OR_POD_NO" +
                                                                             ",SDN_TRANSPORTER_OR_CORRIER_NAME " +
                                                                             ",SDN_BAGS_OR_PACKETS " +
                                                                             ",SDN_REF_INDENT_NO " +
                                                                             ",SDN_CREATED_BY " +
                                                                             ",SDN_CREATED_DATE " +
                                                                             ",SDN_DELIVERY_NOTE_NO " +
                                                                             ",SDN_DC_DATE " +
                                                                             ",SDN_DOOR_DELIVERY_FLAG," +
                                                                             "SDN_TRANSPORT_OR_COURIER" +
                                                                             ",SDN_TOTAL_FRAIGHT " +
                                                                             ",SDN_ADV_PAID " +
                                                                             ",SDN_TO_PAY " +
                                                                             ",SDN_BAGS_OR_PACKS_COUNT " +
                                                                             ",SDN_ECODE) VALUES" +
                                                                             " ('" + CommonData.CompanyCode +
                                                                             "','" + CommonData.BranchCode +
                                                                             "','" + CommonData.StateCode +
                                                                             "','" + CommonData.FinancialYear +
                                                                             "','" + CompanyCode +
                                                                             "','" + BranchCode +
                                                                             "'," + lblAprRefNo.Text +
                                                                             ",'" + txtTripLRNo.Text +
                                                                             "','" + sTransportName +
                                                                             "'," + txtBagsCount.Text +
                                                                             "," + IndentNumber +
                                                                             ",'" + CommonData.LogUserId +
                                                                             "','" + CommonData.CurrentDate +
                                                                             "'," + txtDispatchNo.Text +
                                                                             ",'" + Convert.ToDateTime(dtIndentDate.Value).ToString("dd/MMM/yyyy") +
                                                                             "','" + cmdDoorDeliveryFlag.Text + "','" + cmdTransportOrCorrier.Text +
                                                                             "'," + txtTotalFreight.Text +
                                                                             "," + txtAdvPaid.Text +
                                                                             "," + txtToPay.Text +
                                                                             "," + txtBagsCount.Text;
                            if (Ecode != "Branch Indent" && Ecode != "")
                            {

                                strQryD += "," + Convert.ToInt32(EmpCode[0]) + ")";
                            }
                            else
                            {
                                strQryD += ",0)";
                            }
                        }
                        else
                        {

                            strQryD += " UPDATE STATIONARY_DELIVERY_NOTE SET SDN_APPROVAL_REF_NO=" + lblAprRefNo.Text +
                                                                           ",SDN_LR_OR_POD_NO='" + txtTripLRNo.Text +
                                                                           "',SDN_DC_DATE='" + Convert.ToDateTime(dtIndentDate.Value).ToString("dd/MMM/yyyy") +
                                                                           "',SDN_TRANSPORTER_OR_CORRIER_NAME='" + sTransportName +
                                                                           "',SDN_BAGS_OR_PACKETS=" + txtBagsCount.Text +
                                                                           ",SDN_REF_INDENT_NO=" + IndentNumber +
                                                                           ",SDN_CREATED_BY='" + CommonData.LogUserId +
                                                                           "',SDN_CREATED_DATE='" + CommonData.CurrentDate +
                                                                           "',SDN_DELIVERY_NOTE_NO=" + txtDispatchNo.Text +
                                                                           ",SDN_DOOR_DELIVERY_FLAG='" + cmdDoorDeliveryFlag.Text +
                                                                           "',SDN_TRANSPORT_OR_COURIER='" + cmdTransportOrCorrier.Text +
                                                                           "',SDN_TOTAL_FRAIGHT=" + txtTotalFreight.Text +
                                                                           ",SDN_ADV_PAID=" + txtAdvPaid.Text +
                                                                           ",SDN_TO_PAY=" + txtToPay.Text +
                                                                           ",SDN_BAGS_OR_PACKS_COUNT=" + txtBagsCount.Text;
                            if (Ecode != "Branch Indent" && Ecode != "")
                            {

                                strQryD += ",SDN_ECODE=" + Convert.ToInt32(EmpCode[0]);
                            }
                            else
                            {
                                strQryD += ",SDN_ECODE=0";
                            }


                            strQryD += " WHERE  SDN_TO_COMP_CODE='" + CompanyCode +
                                              "' AND SDN_TO_BRANCH_CODE='" + BranchCode +
                                              "' AND SDN_FIN_YEAR='" + CommonData.FinancialYear +
                                              "' AND SDN_DELIVERY_NOTE_NO=" + txtDispatchNo.Text;


                            strQryD += " DELETE FROM STATIONARY_DC_DETL WHERE STDD_TO_COMP_CODE = '" + CompanyCode + "' AND STDD_TO_BRANCH_CODE='" + BranchCode +
                                       "'  AND STDD_FIN_YEAR='" + CommonData.FinancialYear + "' AND STDD_DELIVERY_NOTE_NO=" + txtDispatchNo.Text + "";
                        }
                        int ivalsHead = 0;
                        try
                        {
                            ivalsHead = objSQLDB.ExecuteSaveData(strQryD);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        if (ivalsHead > 0)
                        {
                            for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
                            {
                                if (gvIndentDetails.Rows[i].Cells["AddStatus"].Value == null)
                                    gvIndentDetails.Rows[i].Cells["AddStatus"].Value = "";
                                if (gvIndentDetails.Rows[i].Cells["AddStatus"].Value.ToString() == "NEW")
                                {
                                    strQryF += " INSERT INTO STATIONARY_INDENT_DETL(" +
                                              "SID_COMPANY_CODE," +
                                              "SID_STATE_CODE," +
                                              "SID_BRANCH_CODE," +
                                              "SID_FIN_YEAR," +
                                              "SID_INDENT_NUMBER," +
                                              "SID_INDENT_SL_NO," +
                                              "SID_ITEM_ID," +
                                              "SID_ITEM_AVAILABLE_QTY," +
                                              "SID_ITEM_REQ_QTY," +
                                              "SID_ITEM_HO_RECON_QTY," +
                                              "SID_ITEM_PRICE," +
                                              "SID_ITEM_AMOUNT,SID_ITEM_REMARKS) " +
                                              "SELECT SIH_COMPANY_CODE," +
                                              "SIH_STATE_CODE,SIH_BRANCH_CODE," +
                                              "SIH_FIN_YEAR,SIH_INDENT_NUMBER," +
                                              "(SELECT ISNULL(MAX(SID_INDENT_SL_NO),0)+1 " +
                                              "FROM STATIONARY_INDENT_DETL WHERE " +
                                              "SID_BRANCH_CODE = SIH_BRANCH_CODE " +
                                              "AND SID_FIN_YEAR = SIH_FIN_YEAR AND " +
                                              "SID_INDENT_NUMBER = SIH_INDENT_NUMBER) SlNo" +
                                              "," + gvIndentDetails.Rows[i].Cells["ItemID"].Value.ToString() +
                                              ",0," + gvIndentDetails.Rows[i].Cells["ApprovedQty"].Value +
                                              "," + gvIndentDetails.Rows[i].Cells["ApprovedQty"].Value +
                                              ",0,0,'Added by Stationary'" +
                                              " FROM STATIONARY_INDENT_HEAD WHERE SIH_BRANCH_CODE='" + BranchCode +
                                              "' AND SIH_INDENT_NUMBER=" + IndentNumber;
                                }

                               

                                string sFrom = gvIndentDetails.Rows[i].Cells["FrmNo"].Value.ToString() == "" ? "0" : gvIndentDetails.Rows[i].Cells["FrmNo"].Value.ToString();
                                string sTo = gvIndentDetails.Rows[i].Cells["ToNo"].Value.ToString() == "" ? "0" : gvIndentDetails.Rows[i].Cells["ToNo"].Value.ToString();

                                strQryF += " INSERT INTO STATIONARY_DC_DETL(STDD_COMPANY_CODE " +
                                                                            ",STDD_BRANCH_CODE " +
                                                                            ",STDD_STATE_CODE " +
                                                                            ",STDD_FIN_YEAR " +
                                                                            ",STDD_DELIVERY_NOTE_NO " +
                                                                            ",STDD_SL_NO " +
                                                                            ",STDD_ITEM_ID " +
                                                                            ",STDD_APPROVED_QTY" +
                                                                            ",STDD_DISPATCHED_QTY " +
                                                                            ",STDD_NUMBERING_FROM " +
                                                                            ",STDD_NUMBERING_TO " +
                                                                            ",STDD_REMARKS" +
                                                                            ",STDD_TO_COMP_CODE" +
                                                                            ",STDD_TO_BRANCH_CODE) VALUES " +
                                                                            "('" + CommonData.CompanyCode +
                                                                            "','" + CommonData.BranchCode +
                                                                            "','" + CommonData.StateCode +
                                                                            "','" + CommonData.FinancialYear +
                                                                            "'," + txtDispatchNo.Text +
                                                                            "," + Convert.ToInt32(i + 1) +
                                                                            "," + gvIndentDetails.Rows[i].Cells["ItemID"].Value +
                                                                            ",'" + gvIndentDetails.Rows[i].Cells["ApprovedQty"].Value +
                                                                            "','" + gvIndentDetails.Rows[i].Cells["DispatchQty"].Value +
                                                                            "'," + sFrom +
                                                                            "," + sTo +
                                                                            ",'" + gvIndentDetails.Rows[i].Cells["Remarks"].Value +
                                                                            "','" + CompanyCode +
                                                                            "','" + BranchCode + "')";
                            }
                            try
                            {
                                ivals = objSQLDB.ExecuteSaveData(strQryF);

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                            finally
                            {
                                objSQLDB = null;
                            }

                        }

                        if (ivals > 0)
                        {
                            MessageBox.Show("Data saved successfully", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SendStationaryIndentDispatchDetails();
                        }

                        else
                        {
                            MessageBox.Show("Data Not saved ", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                gvIndentDetails.Rows.Clear();
                chldStationaryBrochureList.GetGridBind();
                this.Close();
            }

        }




        private void SendStationaryIndentDispatchDetails()
        {
            objSQLDB = new SQLDB();
            string strCommand = "";
           
            try
            {
                strCommand = " EXEC STATIONARY_INDENT_DISPATCH_MAIL '" + CompanyCode + "','" + BranchCode + "'," + IndentNumber + "";
            }
            catch (Exception ex)
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
                    DCNo = Convert.ToInt32(dt.Rows[i]["rs_deliv_note_no"].ToString());
                    IndentNo = Convert.ToInt32(dt.Rows[i]["rs_indent_no"].ToString());
                    AppRefNo = Convert.ToInt32(dt.Rows[i]["rs_appr_ref_no"].ToString());
                    IndentStatus = dt.Rows[i]["rs_indent_status"].ToString();
                    SlNo = Convert.ToInt32(dt.Rows[i]["rs_indent_sl_no1"].ToString());
                    MailBranchCode = dt.Rows[i]["rs_branch_code"].ToString();
                    CorrierOrTransport = dt.Rows[i]["rs_corrier_trnspt"].ToString();
                    BranchCCMailId = "";
                    if (dt.Rows[i]["rs_indent_type"].Equals("SELF"))
                    {
                        SelfMailId = dt.Rows[i]["rs_self_mailid"].ToString();
                        IndentbySelf = dt.Rows[i]["rs_indentbySelf"].ToString();
                        BranchCCMailId = dt.Rows[i]["rs_branch_mailid"].ToString();
                        if (dt.Rows[i]["rs_br_verify_mail_id"].ToString().Length > 5)
                            BranchCCMailId += "," + dt.Rows[i]["rs_br_verify_mail_id"].ToString();
                        if (dt.Rows[i]["rs_br_approval_mail_id"].ToString().Length > 5)
                            BranchCCMailId += "," + dt.Rows[i]["rs_br_approval_mail_id"].ToString();
                    }
                    else
                    {
                        BranchMailId = dt.Rows[i]["rs_branch_mailid"].ToString();
                        BranchCCMailId = dt.Rows[i]["rs_branch_mailid"].ToString();
                        if (dt.Rows[i]["rs_br_verify_mail_id"].ToString().Length > 5)
                            BranchCCMailId += "," + dt.Rows[i]["rs_br_verify_mail_id"].ToString();
                        if (dt.Rows[i]["rs_br_approval_mail_id"].ToString().Length > 5)
                            BranchCCMailId += "," + dt.Rows[i]["rs_br_approval_mail_id"].ToString();
                        if (dt.Rows[i]["rs_br_inch_mailid"].ToString().Length > 5)
                            BranchCCMailId += "," + dt.Rows[i]["rs_br_inch_mailid"].ToString();
                        IndentbyBranch = dt.Rows[i]["rs_indentbyBranch"].ToString();
                    }
                    
                    if ((SlNo < 2) && (SlNo != 0))
                    {
                        Mailbody = "<br /><br /><table padding='0' font-family= 'Segoe UI' cellpadding='5' cellspacing='0' border='1'>";

                        Mailbody += "<tr><td colspan=2;> IndentNo:" + Convert.ToInt32(dt.Rows[i]["rs_indent_no"]) +
                            //" Indent Status:" + dt.Rows[i]["rs_indent_status"] +
                             "<br/><br/>   Indent Date : " + Convert.ToDateTime(dt.Rows[i]["rs_indent_date"]).ToString("dd/MMM/yyyy") +
                             "<br/><br/> ApprovedDate : " + Convert.ToDateTime(dt.Rows[i]["rs_Apprby_date"]).ToString("dd/MMM/yyyy");
                        if (IndentbySelf != "")
                        {
                            Mailbody += "<br/><br/> Dispatched To:" + dt.Rows[i]["rs_indentbySelf"];
                        }
                        if (IndentbyBranch != "")
                        {
                            Mailbody += "<br/><br/> Dispatched To:" + dt.Rows[i]["rs_indentbyBranch"];
                        }
                        Mailbody += "<td> Dc NO :" + Convert.ToInt32(dt.Rows[i]["rs_deliv_note_no"]) +
                             "<br/><br/> DcDate :" + Convert.ToDateTime(dt.Rows[i]["rs_date"]).ToString("dd/MMM/yyyy") +
                             " <br/><br/>Approval RefNO:" + dt.Rows[i]["rs_appr_ref_no"] + "</td>" +
                            "<td colspan=3; >LR|POD No:" + dt.Rows[i]["rs_lr_or_pod"];

                        if (CorrierOrTransport.Equals("TRANSPORT"))
                        {
                            Mailbody += "<br/><br/> TranportName:" + dt.Rows[i]["rs_trnsporter_name"];
                        }
                        if (CorrierOrTransport.Equals("COURIER"))
                        {
                            Mailbody += " <br/><br/> CourierName :" + dt.Rows[i]["rs_corrier_name"];
                        }
                        Mailbody += "<br/><br/> Bags Count:" + dt.Rows[i]["rs_bags_packets_count"] +
                       " <td > TotalFright:" + dt.Rows[i]["rs_total_freight"] +
                       "  <br/><br/> Adv Paid:" + dt.Rows[i]["rs_paid"] +
                       "  <br/><br/> To Pay : " + dt.Rows[i]["rs_topay"] + "</td></tr>";


                    }

                    if ((SlNo == 1))
                    {
                        //Mailbody += "<br /><br /><table padding='0' font-family= 'Segoe UI' cellpadding='5' cellspacing='0' border='1'>";
                        Mailbody += "<tr><td colspan=\"7\"><a href=\"www.shivashakthigroup.com\">" +
                              "<img src=\"http://shivashakthigroup.com/wp-content/uploads/2013/01/logo.png\" alt=\"Shivashakthi Group of Companies\"/></a></td></tr>";


                        Mailbody += "<tr style =\"background-color:#6FA1D2; color:#ffffff;\"><td>Sl.No</td><td colspan=3>Item Name</td> <td>Avilabel Qty</td>" +
                            "  <td >Apporved Qty</td><td>Dispatched Qty</td></tr>";

                    }


                    if (SlNo == 1)
                    {
                        Mailbody += "<tr><td>" + dt.Rows[i]["rs_indent_sl_no1"].ToString() + "</td>" +
                                "<td colspan=3>" + dt.Rows[i]["rs_item_name1"].ToString() + "</td>" +
                            //"<td>" + dt.Rows[i]["rs_item_req_qty1"].ToString() + "</td>" +
                                "<td>" + dt.Rows[i]["rs_avilable_qty1"].ToString() + "</td>" +
                                "<td>" + dt.Rows[i]["rs_appr_qty1"].ToString() + "</td>" +
                                 "<td>" + dt.Rows[i]["rs_item_disp_qty1"].ToString() + "</td></tr>";
                    }
                    else
                    {
                        Mailbody += "<tr><td>" + dt.Rows[i]["rs_indent_sl_no1"].ToString() + "</td>" +
                                   "<td colspan=3>" + dt.Rows[i]["rs_item_name1"].ToString() + "</td>" +
                            //"<td>" + dt.Rows[i]["rs_item_req_qty1"].ToString() + "</td>" +
                                    "<td>" + dt.Rows[i]["rs_avilable_qty1"].ToString() + "</td>" +
                                   "<td>" + dt.Rows[i]["rs_appr_qty1"].ToString() + "</td>" +
                                    "<td>" + dt.Rows[i]["rs_item_disp_qty1"].ToString() + "</td></tr>";
                    }
                }

                Mailbody += "</table>";
                if ((BranchMailId.Length > 0))
                {
                    SendStationaryIndentDispatchDetailsMail(DCNo, AppRefNo);

                    BranchMailId = "";
                    SelfMailId = "";

                }
                else if ((SelfMailId.Length > 0))
                {
                    BranchMailId = SelfMailId;
                    SendStationaryIndentDispatchDetailsMail(DCNo, AppRefNo);
                    BranchMailId = "";
                    SelfMailId = "";

                }
                Mailbody = "";
            }

        }


        public string SendStationaryIndentDispatchDetailsMail(int sDiapatchNo, int sAppRefNo)
        {
            //String[] addrCC = { "laki.lakshmi99@gmail.com" };

            MailAddress fromAddress = new MailAddress("ssbplitho@gmail.com", "SSCRM ::Stationary ");
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
                Subject = "SSERP Stationary Indent Dispatch Info :: DC No " + sDiapatchNo + "    " + "Approval Ref No  " + sAppRefNo,
                Body = "</br></br></br>" + Mailbody + "</br></br><table width='100%'><tr><td align='center'></td></tr><tr><td style='height:50px'></td></tr><tr><td></td></tr></table>  This is server generated mail please do not reply to this mail",

                IsBodyHtml = true
            })
            {
                string[] ssBRCC = BranchCCMailId.Split(',');
                foreach (string sCC in ssBRCC)
                    if (sCC.Length > 5)
                        message.CC.Add(sCC);
                //for (int i = 0; i < addrCC.Length; i++)
                //    message.CC.Add(addrCC[i]);
                //message.To.Add("lakshminallagatla@gmail.com");
                message.To.Add(BranchMailId);
                message.CC.Add("audittripsheet@gmail.com");
                message.CC.Add("auditdata@sivashakthi.net");
                message.CC.Add("ssbplstore9@gmail.com");

                smtp.Send(message);
                return "Yes";
            }
            //}
            //catch (Exception ex)
            //{
            //    return ex.ToString();
            //}
        } 


        private void txtIndentNo_Validated(object sender, EventArgs e)
        {
            dtpGRNDate = DateTime.Now;
            if (txtIndentNo.Text != "")
            {
                objSQLDB = new SQLDB();
                string sqlQry = " SELECT * FROM STATIONARY_INDENT_HEAD INNER JOIN COMPANY_MAS ON CM_COMPANY_CODE = SIH_COMPANY_CODE INNER JOIN BRANCH_MAS ON BRANCH_CODE = SIH_BRANCH_CODE WHERE SIH_COMPANY_CODE='" + CompanyCode + "' AND SIH_BRANCH_CODE='" + BranchCode + "' AND SIH_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text);
                sqlQry += " SELECT * FROM STATIONARY_INDENT_HEAD A INNER JOIN STATIONARY_INDENT_DETL B ON A.SIH_COMPANY_CODE=B.SID_COMPANY_CODE" +
                            " AND A.SIH_BRANCH_CODE=B.SID_BRANCH_CODE AND A.SIH_INDENT_NUMBER=B.SID_INDENT_NUMBER AND SIH_INDENT_STATUS IN('A','D')" +
                            " LEFT OUTER JOIN STATIONARY_DELIVERY_NOTE C ON SDN_APPROVAL_REF_NO = SIH_APPROVAL_REF_NO AND SDN_TO_BRANCH_CODE = SIH_BRANCH_CODE" +
                            " LEFT JOIN STATIONARY_DC_DETL D ON C.SDN_TO_COMP_CODE=D.STDD_TO_COMP_CODE AND C.SDN_TO_BRANCH_CODE=D.STDD_TO_BRANCH_CODE AND C.SDN_FIN_YEAR =D.STDD_FIN_YEAR AND C.SDN_DELIVERY_NOTE_NO=D.STDD_DELIVERY_NOTE_NO AND STDD_ITEM_ID = SID_ITEM_ID" +
                            " INNER JOIN STATIONARY_ITEMS_MASTER E ON B.SID_ITEM_ID=E.SIM_ITEM_CODE" +
                            " WHERE ( SID_ITEM_HO_RECON_QTY >0 OR STDD_DISPATCHED_QTY>0 ) AND SIH_COMPANY_CODE='" + CompanyCode + "' AND SIH_BRANCH_CODE='" + BranchCode + "' AND SIH_FIN_YEAR='" + FinYear + "' AND SIH_INDENT_NUMBER='" + IndentNumber + "'";
                DataSet ds = objSQLDB.ExecuteDataSet(sqlQry);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtIndentDate.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["SIH_INDENT_DATE"]);
                    txtIndentAmt.Text = ds.Tables[0].Rows[0]["SIH_INDENT_AMOUNT"].ToString();
                    txtItemsCount.Text = ds.Tables[1].Rows.Count.ToString();
                    lblAprRefNo.Text = ds.Tables[0].Rows[0]["SIH_APPROVAL_REF_NO"].ToString();
                    txtCompanyName.Text = ds.Tables[0].Rows[0]["CM_COMPANY_NAME"].ToString();
                    txtBranchName.Text = ds.Tables[0].Rows[0]["BRANCH_NAME"].ToString();
                    txtEcode.Text = Ecode;
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        gvIndentDetails.Rows.Clear();
                        GetBindData(ds.Tables[1]);
                        if (ds.Tables[1].Rows[0]["SDN_DELIVERY_NOTE_NO"].ToString().Trim() != "")
                            txtDispatchNo.Text = ds.Tables[1].Rows[0]["SDN_DELIVERY_NOTE_NO"].ToString();
                        if (ds.Tables[1].Rows[0]["SDN_TRANSPORT_OR_COURIER"].ToString().Trim() != "")
                            cmdTransportOrCorrier.Text = ds.Tables[1].Rows[0]["SDN_TRANSPORT_OR_COURIER"].ToString();
                        if (ds.Tables[1].Rows[0]["SDN_TRANSPORTER_OR_CORRIER_NAME"].ToString().Trim() != "")
                        {
                            if (cmdTransportOrCorrier.SelectedIndex == 0)
                            {
                                cmbTransporterOrCorrierName.SelectedValue = ds.Tables[1].Rows[0]["SDN_TRANSPORTER_OR_CORRIER_NAME"].ToString();
                            }
                            else
                            {
                                cmbTransporterOrCorrierName.Text = ds.Tables[1].Rows[0]["SDN_TRANSPORTER_OR_CORRIER_NAME"].ToString();
                            }
                        }
                        if (ds.Tables[1].Rows[0]["SDN_DC_DATE"].ToString().Trim() != "")
                        {
                            dtIndentDate.Value = Convert.ToDateTime(ds.Tables[1].Rows[0]["SDN_DC_DATE"].ToString());
                            dtpGRNDate = Convert.ToDateTime(ds.Tables[1].Rows[0]["SDN_DC_DATE"].ToString());
                        }
                        if (ds.Tables[1].Rows[0]["SDN_DOOR_DELIVERY_FLAG"].ToString().Trim() != "")
                            cmdDoorDeliveryFlag.SelectedItem = ds.Tables[1].Rows[0]["SDN_DOOR_DELIVERY_FLAG"].ToString();
                        txtTotalFreight.Text = ds.Tables[1].Rows[0]["SDN_TOTAL_FRAIGHT"].ToString();
                        txtAdvPaid.Text = ds.Tables[1].Rows[0]["SDN_ADV_PAID"].ToString();
                        txtToPay.Text = ds.Tables[1].Rows[0]["SDN_TO_PAY"].ToString();
                        txtBagsCount.Text = ds.Tables[1].Rows[0]["SDN_BAGS_OR_PACKS_COUNT"].ToString();

                        txtTripLRNo.Text = ds.Tables[1].Rows[0]["SDN_LR_OR_POD_NO"].ToString();
                        //cmbTransporterOrCorrierName.Text = ds.Tables[1].Rows[0]["SDN_TRANSPORTER_OR_CORRIER_NAME"].ToString();
                    }
                }
                else
                {
                    txtIndentAmt.Text = "";
                    txtItemsCount.Text = "";
                    gvIndentDetails.Rows.Clear();
                }
            }
        }

        public void GetBindData(DataTable dt)
        {
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

                DataGridViewCell cellApprQty = new DataGridViewTextBoxCell();
                cellApprQty.Value = dt.Rows[i]["SID_ITEM_HO_RECON_QTY"].ToString();
                tempRow.Cells.Add(cellApprQty);

                DataGridViewCell cellReqQty = new DataGridViewTextBoxCell();
                if (dt.Rows[i]["STDD_DISPATCHED_QTY"].ToString() == "")
                    cellReqQty.Value = dt.Rows[i]["SID_ITEM_HO_RECON_QTY"].ToString();
                else
                    cellReqQty.Value = dt.Rows[i]["STDD_DISPATCHED_QTY"].ToString();
                tempRow.Cells.Add(cellReqQty);                

                DataGridViewCell cellRate = new DataGridViewTextBoxCell();
                cellRate.Value = Convert.ToDouble(dt.Rows[i]["SID_ITEM_PRICE"]).ToString("f");
                tempRow.Cells.Add(cellRate);

                DataGridViewCell cellAmount = new DataGridViewTextBoxCell();
                cellAmount.Value = dt.Rows[i]["SID_ITEM_AMOUNT"].ToString();
                tempRow.Cells.Add(cellAmount);

                DataGridViewCell cellFrmNo = new DataGridViewTextBoxCell();
                cellFrmNo.Value = dt.Rows[i]["STDD_NUMBERING_FROM"].ToString();
                tempRow.Cells.Add(cellFrmNo);

                DataGridViewCell cellToNo = new DataGridViewTextBoxCell();
                cellToNo.Value = dt.Rows[i]["STDD_NUMBERING_TO"].ToString();
                tempRow.Cells.Add(cellToNo);

                DataGridViewCell cellAddStatus = new DataGridViewTextBoxCell();
                cellAddStatus.Value = "";
                tempRow.Cells.Add(cellAddStatus);


                DataGridViewCell cellRemarks = new DataGridViewTextBoxCell();
                cellRemarks.Value = dt.Rows[i]["SID_INDENT_REMARKS"].ToString();
                tempRow.Cells.Add(cellRemarks);

                intRow = intRow + 1;
                gvIndentDetails.Rows.Add(tempRow);
                gvIndentDetails.Columns[5].ReadOnly = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //objSQLDB = new SQLDB();
            //string strQryD = " DELETE FROM STATIONARY_INDENT_DETL WHERE SID_COMPANY_CODE='" + CompanyCode + "' AND SID_BRANCH_CODE='" + BranchCode + "' AND SID_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text);
            //strQryD += " DELETE FROM STATIONARY_INDENT_HEAD WHERE SIH_COMPANY_CODE='" + CompanyCode + "' AND SIH_BRANCH_CODE='" + BranchCode + "' AND SIH_INDENT_NUMBER=" + Convert.ToInt32(txtIndentNo.Text);
            //int ivals = objSQLDB.ExecuteSaveData(strQryD);
            //if (ivals > 0)
            //    MessageBox.Show("Data deleted successfully", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //else
            //    MessageBox.Show("Data Not deleted", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //StationaryIndent_Load(null, null);
            //gvIndentDetails.Rows.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            StationaryIndent_Load(null, null);
            gvIndentDetails.Rows.Clear();
        }

        private void cmdTransportOrCorrier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmdTransportOrCorrier.SelectedIndex == 2)
            {
                cmbTransporterOrCorrierName.Visible = false;
                txtTransportName.Visible = true;
                txtTransportName.Text = "";
            }
            else
            {
                cmbTransporterOrCorrierName.Visible = true;
                txtTransportName.Visible = false;
                txtTransportName.Text = "";
            }
            if (cmdTransportOrCorrier.SelectedIndex == 0)
                UtilityLibrary.PopulateControl(cmbTransporterOrCorrierName, objSQLDB.ExecuteDataSet("SELECT TM_TRANSPORT_NAME,TM_ID FROM TRANSPORT_MASTER").Tables[0].DefaultView, 0, 1, "-- Please Select --", 0);
            else if (cmdTransportOrCorrier.SelectedIndex == 1)
                UtilityLibrary.PopulateControl(cmbTransporterOrCorrierName, objSQLDB.ExecuteDataSet("SELECT CM_CORRIER_NAME,CM_ID FROM CORRIER_MASTER").Tables[0].DefaultView, 0, 1, "-- Please Select --", 0);
            else
                cmbTransporterOrCorrierName.Items.Clear();
        }

        private void txtAdvPaid_KeyUp(object sender, KeyEventArgs e)
        {
            GetCal();
        }

        private void txtTotalFreight_KeyUp(object sender, KeyEventArgs e)
        {
            GetCal();
        }

        public void GetCal()
        {
            txtTotalFreight.Text = txtTotalFreight.Text == "" ? "0" : txtTotalFreight.Text;
            txtAdvPaid.Text = txtAdvPaid.Text == "" ? "0" : txtAdvPaid.Text;
            double iCal = Convert.ToDouble(Convert.ToDouble(txtTotalFreight.Text) - Convert.ToDouble(txtAdvPaid.Text));
            txtToPay.Text = iCal.ToString("0.00");
        }

        private void txtTotalFreight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtAdvPaid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnItemsSearch_Click(object sender, EventArgs e)
        {
            StationaryItemsSearch ItemSearch = new StationaryItemsSearch("StationaryBroucher");
            ItemSearch.objStationaryBroucher = this;
            ItemSearch.ShowDialog();
        }
    }
}
