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



    public partial class StationaryDCByHand : Form
    {
        private SQLDB objSQLDB = null;
        Security objSecurity = null;
        HRInfo objHrInfo = null;
        bool blIsCellQty = true, flagText = false;


        private int intCurrentRow = 0;
        private int intCurrentCell = 0;
        public StationaryBrochureList chldStationaryBrochureList = null;
        bool flage = false;

        string Mailbody = "";
        string BranchMailId = "";
        string SelfMailId = "";
        string IndentbySelf = "";
        string IndentbyBranch = "";
        int DCNo = 0;
        int AppRefNo = 0;
        int SlNo = 0;
        int Ecode = 0;
        //string strEcode = "";
        string MailBranchCode = "";
        string CorrierOrTransport = "";
        DataTable DtSendMail = new DataTable();
        string BranchCode = "";
        string CompanyCode = "";
        DataSet dsCnt = new DataSet();


        public StationaryDCByHand()
        {
            InitializeComponent();
        }




        public StationaryDCByHand(string ScType, string CmpyCode, string BrCode, string StCode, string Fin_Year)
        {
            InitializeComponent();
            //ScreenType = ScType;
            //BranchCode = BrCode;           
            //CompanyCode = CmpyCode;
            //FinYear = Fin_Year;
            //StateCode = StCode;
        }
        //private void CalculateTotals()
        //{
        //    txtItemsCount.Text = gvIndentDetails.Rows.Count.ToString();
        //    if (gvIndentDetails.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
        //        {
        //            if (gvIndentDetails.Rows[i].Cells["ApprovedQty"].Value.ToString() != "" && gvIndentDetails.Rows[i].Cells["Amount"].Value.ToString() != "")
        //            {
        //                txtIndentAmt.Text = Convert.ToDouble(Convert.ToDouble(txtIndentAmt.Text) + Convert.ToDouble(gvIndentDetails.Rows[i].Cells["Amount"].Value)).ToString("f");
        //            }
        //        }
        //    }
        //    else
        //    {
        //        txtIndentAmt.Text = "0.00";
        //    }
        //}

        private void StationaryIndent_Load(object sender, EventArgs e)
        {
            objSecurity = new Security();
            DataTable dtCpy = objSecurity.GetCompanyDataSet().Tables[0];
            UtilityLibrary.PopulateControl(cmbCompany, dtCpy.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
            objSecurity = null;

            dtDispatchDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            txtItemsCount.Text = "0.00";
            txtIndentAmt.Text = "0.00";
            //txtDispatchNo.Text = GenerateIndentNo();
            cmdDoorDeliveryFlag.SelectedIndex = 0;
            gvIndentDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);

            gvIndentDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);
            objSQLDB = new SQLDB();
            cmdTransportOrCorrier.SelectedIndex = 0;
          
                cmbBoxTransType.SelectedIndex = 0;

            if (txtDispatchNo.Text != "" && cmbCompany.SelectedIndex > 0 && cmbBranch_optional.SelectedIndex > 0 && flage == true)
            {
                btnPrint.Enabled = true;
            }
            else
            {
                btnPrint.Enabled = false;
            }

        }

        private string GenerateIndentNo()
        {
            objSQLDB = new SQLDB();
            string sDcNo = string.Empty;
            DataTable dt = new DataTable();
            string CompanyCode = "";
            
            try
            {
                if (cmbBranch_optional.SelectedIndex > 0)
                {
                    //string ToBranCode = cmbBranch_optional.SelectedValue.ToString().Substring(0, 3);

                    string SqlCmd = "  SELECT DISTINCT COMPANY_CODE FROM  BRANCH_MAS " +
                         " WHERE BRANCH_CODE='" + cmbBranch_optional.SelectedValue.ToString() + "'";
                    if (SqlCmd.Length > 0)
                    {
                        dt = objSQLDB.ExecuteDataSet(SqlCmd).Tables[0];
                    }
                    if (dt.Rows.Count > 0)
                    {
                        CompanyCode = dt.Rows[0]["COMPANY_CODE"].ToString();
                    }
                    //string sqlText = "SELECT ISNULL(MAX(SDN_DELIVERY_NOTE_NO),0)+1 FROM STATIONARY_DELIVERY_NOTE WHERE SDN_COMPANY_CODE='" + CommonData.CompanyCode +
                    //                "' AND SDN_BRANCH_CODE='" + CommonData.BranchCode + "' AND SDN_FIN_YEAR='" + CommonData.FinancialYear + "'";


                    string sqlText = "SELECT ISNULL(MAX(SDN_DELIVERY_NOTE_NO),0)+1 FROM   BRANCH_MAS"+
                                      " INNER JOIN  STATIONARY_DELIVERY_NOTE  ON SDN_TO_BRANCH_CODE = BRANCH_CODE "+
                                      "WHERE COMPANY_CODE = '" + CompanyCode + "'   AND SDN_FIN_YEAR='" + CommonData.FinancialYear + "'";
                    sDcNo = objSQLDB.ExecuteDataSet(sqlText).Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return sDcNo;
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

      

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        public void CalculateTot()
        {
            double iTotalAmt = 0;
            double iTotalQty = 0;
            for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
            {
                if (gvIndentDetails.Rows[i].Cells["Amount"].Value.ToString() != "")
                    iTotalAmt += Convert.ToDouble(gvIndentDetails.Rows[i].Cells["Amount"].Value);
                else
                    iTotalAmt += 0;
                if (gvIndentDetails.Rows[i].Cells["DispatchQty"].Value.ToString() != "")
                    iTotalQty += Convert.ToDouble(gvIndentDetails.Rows[i].Cells["DispatchQty"].Value);
                else
                    iTotalQty += 0;

            }
            txtIndentAmt.Text = iTotalAmt.ToString();
            txtItemsCount.Text = iTotalQty.ToString();
        }
        private bool Checkdata()
        {
            if (Convert.ToDateTime(CommonData.CurrentDate) > dtDispatchDate.Value.AddDays(Convert.ToInt32(CommonData.LogUserBackDays)))
            {
                MessageBox.Show("You are not allowed to Enter/Update for backdays Data", "Stationary :: GRN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cmbBoxTransType.SelectedIndex == 1)
            {
                if (txtIndentByEcode.Text == "")
                {
                    MessageBox.Show(" Pls Enter Ecode ", "Stationary", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if (cmbCompany.SelectedIndex == 0)
            {
                MessageBox.Show(" Pls Select CompanyName", "Stationary", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cmbBranch_optional.SelectedIndex == 0 || cmbBranch_optional.SelectedIndex == -1)
            {
                MessageBox.Show(" Pls Select BranchName", "Stationary", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (gvIndentDetails.Rows.Count == 0)
            {
                MessageBox.Show("Select Atlease One item!", "Stationary", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                BranchCode = cmbBranch_optional.SelectedValue.ToString();
                CompanyCode = cmbCompany.SelectedValue.ToString();
                objSQLDB = new SQLDB();
                int ivals = 0;
                string sqlText = "";
                string strQry = "", strQryD = "", strQryF = "";
                //strQry = " UPDATE STATIONARY_INDENT_HEAD" +
                //        " SET SIH_INDENT_STATUS='D'" +
                //        " WHERE SIH_BRANCH_CODE='" + BranchCode + "'";
                //"' AND SDN_APPROVAL_REF_NO=" + IndentNumber;

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
                //try
                //{
                //    ival = objSQLDB.ExecuteSaveData(strQry);
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.ToString());
                //}
                //if (ival > 0)
                {
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

                        try
                        {
                            if (flage == false)
                            {
                                txtDispatchNo.Text = GenerateIndentNo();
                                strQryD += " INSERT INTO STATIONARY_DELIVERY_NOTE(SDN_COMPANY_CODE " +
                                                                                  ",SDN_BRANCH_CODE " +
                                                                                  ",SDN_STATE_CODE " +
                                                                                  ",SDN_FIN_YEAR " +
                                                                                  ",SDN_TO_COMP_CODE " +
                                                                                  ",SDN_TO_BRANCH_CODE " +
                                                                                  ",SDN_APPROVAL_REF_NO " +
                                                                                  ",SDN_LR_OR_POD_NO," +
                                                                                  "SDN_TRANSPORTER_OR_CORRIER_NAME" +
                                                                                  ",SDN_BAGS_OR_PACKETS,SDN_REF_INDENT_NO" +
                                                                                  ",SDN_CREATED_BY,SDN_CREATED_DATE " +
                                                                                  ",SDN_DELIVERY_NOTE_NO,SDN_DC_DATE " +
                                                                                  ",SDN_DOOR_DELIVERY_FLAG," +
                                                                                  "SDN_TRANSPORT_OR_COURIER " +
                                                                                  ",SDN_TOTAL_FRAIGHT " +
                                                                                  ",SDN_ADV_PAID " +
                                                                                  ",SDN_TO_PAY " +
                                                                                  ",SDN_BAGS_OR_PACKS_COUNT " +
                                                                                  ",SDN_ECODE) VALUES " +
                                                                                  "('" + CommonData.CompanyCode +
                                                                                  "','" + CommonData.BranchCode +
                                                                                  "','" + CommonData.StateCode +
                                                                                  "','" + CommonData.FinancialYear +
                                                                                  "','" + cmbCompany.SelectedValue.ToString() +
                                                                                  "','" + cmbBranch_optional.SelectedValue.ToString() +
                                                                                  "',0,'" + txtTripLRNo.Text +
                                                                                  "','" + sTransportName +
                                                                                  "'," + txtBagsCount.Text +
                                                                                  ",0,'" + CommonData.LogUserId +
                                                                                  "','" + CommonData.CurrentDate +
                                                                                  "'," + txtDispatchNo.Text +
                                                                                  ",'" + Convert.ToDateTime(dtDispatchDate.Value).ToString("dd/MMM/yyyy") +
                                                                                  "','" + cmdDoorDeliveryFlag.Text +
                                                                                  "','" + cmdTransportOrCorrier.Text +
                                                                                  "', " + txtTotalFreight.Text +
                                                                                  "," + txtAdvPaid.Text +
                                                                                  "," + txtToPay.Text +
                                                                                  "," + txtBagsCount.Text + " ";
                                if (cmbBoxTransType.SelectedIndex == 1)
                                {
                                    strQryD += "," + txtIndentByEcode.Text + ")";
                                }
                                else
                                {
                                    strQryD += ",0)";
                                }
                            }
                            else
                            {
                                strQryD += " UPDATE STATIONARY_DELIVERY_NOTE SET SDN_APPROVAL_REF_NO=0" +
                                                                                 ",SDN_LR_OR_POD_NO='" + txtTripLRNo.Text +
                                                                                 "',SDN_DC_DATE='" + Convert.ToDateTime(dtDispatchDate.Value).ToString("dd/MMM/yyyy") +
                                                                                 "',SDN_TRANSPORTER_OR_CORRIER_NAME='" + sTransportName +
                                                                                 "',SDN_BAGS_OR_PACKETS=" + txtBagsCount.Text +
                                                                                 ",SDN_REF_INDENT_NO=0,SDN_CREATED_BY='" + CommonData.LogUserId +
                                                                                 "',SDN_CREATED_DATE='" + CommonData.CurrentDate +
                                                                                 "',SDN_DELIVERY_NOTE_NO=" + txtDispatchNo.Text +
                                                                                 ",SDN_DOOR_DELIVERY_FLAG='" + cmdDoorDeliveryFlag.Text +
                                                                                 "',SDN_TRANSPORT_OR_COURIER='" + cmdTransportOrCorrier.Text +
                                                                                 "',SDN_TOTAL_FRAIGHT=" + txtTotalFreight.Text +
                                                                                 ",SDN_ADV_PAID=" + txtAdvPaid.Text +
                                                                                 ",SDN_TO_PAY=" + txtToPay.Text +
                                                                                 ",SDN_BAGS_OR_PACKS_COUNT=" + txtBagsCount.Text;
                                if (cmbBoxTransType.SelectedIndex == 1)
                                {
                                    strQryD += ",SDN_ECODE=" + txtIndentByEcode.Text + "";
                                }
                                else
                                {
                                    strQryD += ",SDN_ECODE=0";
                                }
                                strQryD += " WHERE  SDN_TO_COMP_CODE='" + cmbCompany.SelectedValue.ToString() +
                                                  "' AND SDN_TO_BRANCH_CODE ='" + cmbBranch_optional.SelectedValue.ToString() +
                                                  "' AND SDN_FIN_YEAR='" + CommonData.FinancialYear +
                                                  "' AND SDN_DELIVERY_NOTE_NO='" + txtDispatchNo.Text + "'";

                                strQryD += " DELETE FROM STATIONARY_DC_DETL WHERE STDD_TO_COMP_CODE = '" + CompanyCode + 
                                                                           "' AND STDD_TO_BRANCH_CODE='" + BranchCode +
                                                                           "' AND STDD_FIN_YEAR='" + CommonData.FinancialYear + 
                                                                           "' AND STDD_DELIVERY_NOTE_NO=" + txtDispatchNo.Text + "";
                            }
                            int ivalsHead = objSQLDB.ExecuteSaveData(strQryD);
                            if (ivalsHead > 0)
                            {
                                for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
                                {
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
                                                                                ",STDD_REMARKS " +
                                                                                ",STDD_TO_COMP_CODE " +
                                                                                ",STDD_TO_BRANCH_CODE) VALUES " +
                                                                                "('" + CommonData.CompanyCode +
                                                                                "','" + CommonData.BranchCode +
                                                                                "','" + CommonData.StateCode +
                                                                                "','" + CommonData.FinancialYear +
                                                                                "'," + txtDispatchNo.Text +
                                                                                "," + Convert.ToInt32(i + 1) +
                                                                                "," + gvIndentDetails.Rows[i].Cells["ItemId"].Value +
                                                                                ",'" + gvIndentDetails.Rows[i].Cells["ApprovedQty"].Value +
                                                                                "','" + gvIndentDetails.Rows[i].Cells["DispatchQty"].Value +
                                                                                "'," + sFrom +
                                                                                "," + sTo +
                                                                                ",'" + gvIndentDetails.Rows[i].Cells["Remarks"].Value +
                                                                                "','" + cmbCompany.SelectedValue.ToString() +
                                                                                "','" + cmbBranch_optional.SelectedValue.ToString() + "')";
                                }
                                ivals = objSQLDB.ExecuteSaveData(strQryF);
                            }
                            objSQLDB = null;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        if (ivals > 0)
                        {
                            MessageBox.Show("Data saved successfully", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (flage == false)
                            {
                                SendStationaryIndentDispatchDetails();
                            }

                            btnCancel_Click(null, null);

                        }

                        else
                        {
                            MessageBox.Show("Data Not saved ", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                }


            }

        }
        private void SendStationaryIndentDispatchDetails()
        {

            objSQLDB = new SQLDB();
            string strCommand = "";           
            try
            {
                strCommand = " EXEC STATIONARY_WITH_OUT_INDENT_DISPATCH_MAIL '" + CompanyCode + "','" + BranchCode + "'," + txtDispatchNo.Text + "";
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

                    //AppRefNo = Convert.ToInt32(dt.Rows[i]["rs_appr_ref_no"].ToString());
                    //IndentStatus = dt.Rows[i]["rs_indent_status"].ToString();
                    SlNo = Convert.ToInt32(dt.Rows[i]["rs_dispatch_sl_no1"].ToString());
                    MailBranchCode = dt.Rows[i]["rs_branch_code"].ToString();
                    CorrierOrTransport = dt.Rows[i]["rs_corrier_trnspt"].ToString();
                     Ecode = Convert.ToInt32(dt.Rows[0]["rs_ecode"].ToString());

                    if (Ecode == 0)
                    {
                        BranchMailId = dt.Rows[i]["rs_branch_mailid"].ToString();
                        IndentbyBranch = dt.Rows[i]["rs_indentbyBranch"].ToString();
                    }
                    else
                    {
                        SelfMailId = dt.Rows[i]["rs_self_mailid"].ToString();
                        IndentbySelf = dt.Rows[i]["rs_indentbySelf"].ToString();

                    }
                    if ((SlNo < 2) && (SlNo != 0))
                    {
                        Mailbody = "<br /><br /><table padding='0' font-family= 'Segoe UI' cellpadding='5' cellspacing='0' border='1'>";

                        Mailbody += "<tr><td colspan=2> Dc NO :" + Convert.ToInt32(dt.Rows[i]["rs_deliv_note_no"]) +
                         "<br/><br/> DcDate :" + Convert.ToDateTime(dt.Rows[i]["rs_date"]).ToString("dd/MMM/yyyy");
                            //"<td> Approval RefNO:" + dt.Rows[i]["rs_appr_ref_no"]+
                         if (IndentbySelf !="")
                        {
                            Mailbody += "<br/><br/> Dispatched To:" + dt.Rows[i]["rs_indentbySelf"];
                        }
                        if (IndentbyBranch != "")
                        {
                            Mailbody += "<br/><br/> Dispatched To:" + dt.Rows[i]["rs_indentbyBranch"] + "</td>"; 
                        }
                        Mailbody += "<td colspan=2> LR|POD No:" + dt.Rows[i]["rs_lr_or_pod"];

                        if (CorrierOrTransport.Equals("TRANSPORT"))
                        {
                            Mailbody += "<br/><br/> TranportName:" + dt.Rows[i]["rs_trnsporter_name"];
                        }
                        if (CorrierOrTransport.Equals("COURIER"))
                        {
                            Mailbody += " <br/><br/>CourierName :" + dt.Rows[i]["rs_corrier_name"];
                        }

                        Mailbody += "<br/><br/> Bags Count:" + dt.Rows[i]["rs_bags_packets_count"] + "</td>" +
                       " <td> TotalFright:" + dt.Rows[i]["rs_total_freight"] +
                       "  <br/><br/> Adv Paid:" + dt.Rows[i]["rs_paid"] +
                       "  <br/><br/> To Pay : " + dt.Rows[i]["rs_topay"] + "</td></tr>";


                    }

                    if ((SlNo == 1))
                    {
                        //Mailbody += "<br /><br /><table padding='0' font-family= 'Segoe UI' cellpadding='5' cellspacing='0' border='1'>";
                        Mailbody += "<tr><td colspan=\"6\"><a href=\"www.shivashakthigroup.com\">" +
                              "<img src=\"http://shivashakthigroup.com/wp-content/uploads/2013/01/logo.png\" alt=\"Shivashakthi Group of Companies\"/></a></td></tr>";


                        Mailbody += "<tr style =\"background-color:#6FA1D2; color:#ffffff;\"><td>Sl.No</td><td colspan=2>Item Name</td><td >Apporved Qty</td><td>Dispatched Qty</td></tr>";

                    }


                    if (SlNo == 1)
                    {
                        Mailbody += "<tr><td>" + dt.Rows[i]["rs_dispatch_sl_no1"].ToString() + "</td>" +
                                "<td colspan=2>" + dt.Rows[i]["rs_item_name1"].ToString() + "</td>" +
                            //"<td>" + dt.Rows[i]["rs_item_req_qty1"].ToString() + "</td>" +
                                "<td>" + dt.Rows[i]["rs_appr_qty1"].ToString() + "</td>" +
                                 "<td>" + dt.Rows[i]["rs_item_disp_qty1"].ToString() + "</td></tr>";
                    }
                    else
                    {
                        Mailbody += "<tr><td>" + dt.Rows[i]["rs_dispatch_sl_no1"].ToString() + "</td>" +
                                   "<td colspan=2>" + dt.Rows[i]["rs_item_name1"].ToString() + "</td>" +
                            //"<td>" + dt.Rows[i]["rs_item_req_qty1"].ToString() + "</td>" +
                                   "<td>" + dt.Rows[i]["rs_appr_qty1"].ToString() + "</td>" +
                                    "<td>" + dt.Rows[i]["rs_item_disp_qty1"].ToString() + "</td></tr>";
                    }
                }

                Mailbody += "</table>";
                if ((BranchMailId.Length > 0))
                {
                    SendStationaryIndentDispatchDetailsMail(DCNo);

                    BranchMailId = "";
                    SelfMailId = "";

                }
                else if ((SelfMailId.Length > 0))
                {
                    BranchMailId = SelfMailId;
                    SendStationaryIndentDispatchDetailsMail(DCNo);
                    BranchMailId = "";
                    SelfMailId = "";

                }
                Mailbody = "";
            }

        }




        public string SendStationaryIndentDispatchDetailsMail(int sDiapatchNo)
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
                Subject = "SSERP Stationary :: DC NO " + sDiapatchNo + " ",
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

                smtp.Send(message);
                return "Yes";
            }
            //}
            //catch (Exception ex)
            //{
            //    return ex.ToString();
            //}
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            DataTable dt = new DataTable();
            if (txtDispatchNo.Text != "" && cmbCompany.SelectedIndex>0 && cmbBranch_optional.SelectedIndex>0 && flage == true)
            {
                try
                {
                    string SqlCmd = "  SELECT DISTINCT COMPANY_CODE,BRANCH_CODE FROM  BRANCH_MAS " +
                             " WHERE BRANCH_CODE='" + cmbBranch_optional.SelectedValue.ToString() + "'";
                    if (SqlCmd.Length > 0)
                    {
                        dt = objSQLDB.ExecuteDataSet(SqlCmd).Tables[0];
                    }
                    if (dt.Rows.Count > 0)
                    {
                        CompanyCode = dt.Rows[0]["COMPANY_CODE"].ToString();
                        BranchCode = dt.Rows[0]["BRANCH_CODE"].ToString();
                    }

                    string strQryD = " DELETE FROM STATIONARY_DELIVERY_NOTE WHERE SDN_TO_COMP_CODE='" + CompanyCode +
                                                                                "'AND SDN_TO_BRANCH_CODE='" + BranchCode +
                                                                                "'AND SDN_FIN_YEAR='" + CommonData.FinancialYear +
                                                                                "'AND SDN_DELIVERY_NOTE_NO=" + txtDispatchNo.Text + "";

                    strQryD += " DELETE FROM STATIONARY_DC_DETL WHERE STDD_TO_COMP_CODE = '" + CompanyCode + "' AND STDD_TO_BRANCH_CODE='" + BranchCode +
                                "'  AND STDD_FIN_YEAR='" + CommonData.FinancialYear + "' AND STDD_DELIVERY_NOTE_NO=" + txtDispatchNo.Text + "";

                    int ivals = objSQLDB.ExecuteSaveData(strQryD);
                    if (ivals > 0)
                        MessageBox.Show("Data deleted successfully", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Data Not deleted", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    StationaryIndent_Load(null, null);
                    btnCancel_Click(null, null);
                    gvIndentDetails.Rows.Clear();
                    flage = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            flage = false;
            StationaryIndent_Load(null, null);
            flage = false;
            cmbBranch_optional.SelectedIndex = -1;
            //GenerateIndentNo();
            txtDispatchNo.Text = "";
            dtDispatchDate.Value = DateTime.Today;
            txtAdvPaid.Text = "";
            txtToPay.Text = "";
            txtTotalFreight.Text = "";
            txtBagsCount.Text = "";
            txtTripLRNo.Text = "";
            txtTransportName.Text = "";
            cmdDoorDeliveryFlag.SelectedIndex = 0;
            txtEcode.Text = "";
            cmbTransporterOrCorrierName.SelectedIndex = 0;
            cmdTransportOrCorrier.SelectedIndex = 0;
            txtIndentByEcode.Text = "";
            txtEmpName.Text = "";
            btnPrint.Enabled = false;

            if (cmbBoxTransType.SelectedIndex == 0)
            {
                txtIndentByEcode.Visible = false;
                txtEmpName.Visible = false;
                lblIndentBy.Visible = false;
            }
            else
            {
                txtIndentByEcode.Visible = true;
                txtEmpName.Visible = true;
                lblIndentBy.Visible = true;

            }
            gvIndentDetails.Rows.Clear();
        }

        private void cmdTransportOrCorrier_SelectedIndexChanged(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            if (cmdTransportOrCorrier.SelectedIndex == 0)
            {
                txtEcode.Visible = false;
                txtTransportName.Visible = false;
                cmbTransporterOrCorrierName.Visible = true;
                UtilityLibrary.PopulateControl(cmbTransporterOrCorrierName, objSQLDB.ExecuteDataSet("SELECT TM_TRANSPORT_NAME,TM_ID FROM TRANSPORT_MASTER").Tables[0].DefaultView, 0, 1, "-- Please Select --", 0);

            }
            else if (cmdTransportOrCorrier.SelectedIndex == 1)
            {
                UtilityLibrary.PopulateControl(cmbTransporterOrCorrierName, objSQLDB.ExecuteDataSet("SELECT CM_CORRIER_NAME,CM_ID FROM CORRIER_MASTER").Tables[0].DefaultView, 0, 1, "-- Please Select --", 0);
                txtEcode.Visible = false;
                txtTransportName.Visible = false;
                cmbTransporterOrCorrierName.Visible = true;
            }
            else
            {
                txtEcode.Visible = true;
                txtTransportName.Visible = true;
                cmbTransporterOrCorrierName.Visible = false;
            }
            objSQLDB = null;
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

        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCompany.SelectedIndex > 0)
            {
                objHrInfo = new HRInfo();
                DataTable dtBranch = objHrInfo.GetAllBranchList(cmbCompany.SelectedValue.ToString(), "", "").Tables[0];
                UtilityLibrary.PopulateControl(cmbBranch_optional, dtBranch.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
                objHrInfo = null;
            }
            else
            {
               
                //StationaryIndent_Load(null, null);
                flage = false;
                cmbBranch_optional.SelectedIndex = -1;
                //GenerateIndentNo();
                txtDispatchNo.Text = "";
                dtDispatchDate.Value = DateTime.Today;
                txtAdvPaid.Text = "";
                txtToPay.Text = "";
                txtTotalFreight.Text = "";
                txtBagsCount.Text = "";
                txtTripLRNo.Text = "";
                txtTransportName.Text = "";
                cmdDoorDeliveryFlag.SelectedIndex = 0;
                txtEcode.Text = "";
                cmbTransporterOrCorrierName.SelectedIndex = 0;
                cmdTransportOrCorrier.SelectedIndex = 0;
                txtIndentByEcode.Text = "";
                txtEmpName.Text = "";

                if (cmbBoxTransType.SelectedIndex == 0)
                {
                    txtIndentByEcode.Visible = false;
                    txtEmpName.Visible = false;
                    lblIndentBy.Visible = false;
                }
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
            if (txtEcode.Text.Length > 4)
            {
                GetEmpName(txtEcode.Text);
            }
            else
            {
                txtTransportName.Text = "";
            }
        }

        private void GetEmpName(string strEcode)
        {
            DataTable EmpData = new DataTable();
            try
            {
                objSQLDB = new SQLDB();
                string strSql = "SELECT CAST(ECODE AS VARCHAR)+'-'+MEMBER_NAME NAME,DESIG,EMP_DOJ,FATHER_NAME FROM EORA_MASTER WHERE ECODE = " + strEcode;
                EmpData = objSQLDB.ExecuteDataSet(strSql).Tables[0];
                if (EmpData.Rows.Count > 0)
                    txtTransportName.Text = EmpData.Rows[0]["NAME"].ToString();
                else
                    txtTransportName.Text = "";
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


        private void btnItemsSearch_Click(object sender, EventArgs e)
        {
            StationaryItemsSearch ItemSearch = new StationaryItemsSearch("StationaryDC");
            ItemSearch.objStationaryDCByHand = this;
            ItemSearch.ShowDialog();
        }


        private bool Checkdetails()
        {
            if (cmbCompany.SelectedIndex == 0)
            {
                MessageBox.Show(" Pls Select CompanyName", "Stationary", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cmbBranch_optional.SelectedIndex == 0)
            {
                MessageBox.Show(" Pls Select BranchName", "Stationary", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private void txtDispatchNo_Validated(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string sqlQry = "";
            //cmbBoxTransType.SelectedIndex = 0;
            if (txtDispatchNo.Text != "" && cmbCompany.SelectedIndex>0 && cmbBranch_optional.SelectedIndex>0)
            {
                try
                {
                    objSQLDB = new SQLDB();

                     sqlQry = " SELECT *,BM.COMPANY_CODE FROM STATIONARY_DELIVERY_NOTE " +
                                    " INNER JOIN BRANCH_MAS BM ON BM.BRANCH_CODE = SDN_TO_BRANCH_CODE " +
                                    "  WHERE  COMPANY_CODE='" + cmbCompany.SelectedValue.ToString() +
                                   "' AND SDN_TO_BRANCH_CODE='" + cmbBranch_optional.SelectedValue.ToString() + 
                                    "' AND  SDN_FIN_YEAR='" + CommonData.FinancialYear +
                                    "' AND SDN_DELIVERY_NOTE_NO='" + Convert.ToInt32(txtDispatchNo.Text) + "'";

                    sqlQry += " SELECT *,BM.COMPANY_CODE FROM STATIONARY_DELIVERY_NOTE " +
                              " INNER JOIN STATIONARY_DC_DETL ON  SDN_TO_COMP_CODE=STDD_TO_COMP_CODE AND SDN_TO_BRANCH_CODE=STDD_TO_BRANCH_CODE  " +
                              " AND STDD_FIN_YEAR=SDN_FIN_YEAR AND SDN_DELIVERY_NOTE_NO=STDD_DELIVERY_NOTE_NO " +
                              " INNER JOIN STATIONARY_ITEMS_MASTER E ON STDD_ITEM_ID=E.SIM_ITEM_CODE " +
                              " INNER JOIN BRANCH_MAS BM ON BM.BRANCH_CODE = SDN_TO_BRANCH_CODE " +
                              " WHERE COMPANY_CODE='" + cmbCompany.SelectedValue.ToString() +
                              "' AND SDN_TO_BRANCH_CODE='" + cmbBranch_optional.SelectedValue.ToString() +

                              "' AND SDN_FIN_YEAR='" + CommonData.FinancialYear +
                                    "' AND SDN_DELIVERY_NOTE_NO='" + Convert.ToInt32(txtDispatchNo.Text) + "'";

                    ds = objSQLDB.ExecuteDataSet(sqlQry);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnPrint.Enabled = true;
                    AppRefNo = Convert.ToInt32(ds.Tables[0].Rows[0]["SDN_APPROVAL_REF_NO"].ToString());
                    if (AppRefNo <= 0)
                    {
                        flage = true;
                        cmbCompany.SelectedValue = ds.Tables[0].Rows[0]["COMPANY_CODE"].ToString();
                        cmbBranch_optional.SelectedValue = ds.Tables[0].Rows[0]["SDN_TO_BRANCH_CODE"].ToString();
                        dtDispatchDate.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["SDN_DC_DATE"]);
                        txtDispatchNo.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["SDN_DELIVERY_NOTE_NO"]).ToString();

                        txtIndentByEcode.Text = Convert.ToInt32(ds.Tables[0].Rows[0]["SDN_ECODE"]).ToString();
                        if (txtIndentByEcode.Text.Length <= 1)
                        {

                            txtIndentByEcode.Visible = false;
                            txtEmpName.Visible = false;
                            lblIndentBy.Visible = false;

                        }


                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            gvIndentDetails.Rows.Clear();
                            GetBindData(ds.Tables[1]);

                            txtTotalFreight.Text = ds.Tables[1].Rows[0]["SDN_TOTAL_FRAIGHT"].ToString();
                            txtAdvPaid.Text = ds.Tables[1].Rows[0]["SDN_ADV_PAID"].ToString();
                            txtToPay.Text = ds.Tables[1].Rows[0]["SDN_TO_PAY"].ToString();
                            txtBagsCount.Text = ds.Tables[1].Rows[0]["SDN_BAGS_OR_PACKS_COUNT"].ToString();

                            txtTripLRNo.Text = ds.Tables[1].Rows[0]["SDN_LR_OR_POD_NO"].ToString();
                            cmdTransportOrCorrier.Text = ds.Tables[1].Rows[0]["SDN_TRANSPORT_OR_COURIER"].ToString();
                            if (cmbTransporterOrCorrierName.SelectedIndex >= 0)
                            {
                                cmbTransporterOrCorrierName.SelectedValue = ds.Tables[1].Rows[0]["SDN_TRANSPORTER_OR_CORRIER_NAME"].ToString();
                            }
                            else
                            {
                                cmbTransporterOrCorrierName.Text = ds.Tables[1].Rows[0]["SDN_TRANSPORTER_OR_CORRIER_NAME"].ToString();
                            }

                        }

                    }
                }
                else
                {
                    gvIndentDetails.Rows.Clear();
                    cmbBoxTransType.SelectedIndex = 0;
                    btnCancel_Click(null, null);
                    btnPrint.Enabled = false;
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
                cellItemID.Value = dt.Rows[i]["STDD_ITEM_ID"].ToString();
                tempRow.Cells.Add(cellItemID);

                DataGridViewCell cellItemName = new DataGridViewTextBoxCell();
                cellItemName.Value = dt.Rows[i]["SIM_ITEM_NAME"].ToString();
                tempRow.Cells.Add(cellItemName);

                //DataGridViewCell cellAvailQty = new DataGridViewTextBoxCell();
                //cellAvailQty.Value = "0";
                //tempRow.Cells.Add(cellAvailQty);

                DataGridViewCell cellApprQty = new DataGridViewTextBoxCell();
                cellApprQty.Value = dt.Rows[i]["STDD_APPROVED_QTY"].ToString();
                tempRow.Cells.Add(cellApprQty);

                DataGridViewCell cellDispatchQty = new DataGridViewTextBoxCell();
                cellDispatchQty.Value = dt.Rows[i]["STDD_DISPATCHED_QTY"].ToString();
                tempRow.Cells.Add(cellDispatchQty);

                DataGridViewCell cellRate = new DataGridViewTextBoxCell();
                cellRate.Value = Convert.ToDouble(dt.Rows[i]["SIM_ITEM_PRICE"]).ToString("f");
                tempRow.Cells.Add(cellRate);

                //DataGridViewCell cellAmount = new DataGridViewTextBoxCell();
                //cellAmount.Value = dt.Rows[i]["SID_ITEM_AMOUNT"].ToString();
                //tempRow.Cells.Add(cellAmount);

                DataGridViewCell cellFrmNo = new DataGridViewTextBoxCell();
                cellFrmNo.Value = dt.Rows[i]["STDD_NUMBERING_FROM"].ToString();
                tempRow.Cells.Add(cellFrmNo);

                DataGridViewCell cellToNo = new DataGridViewTextBoxCell();
                cellToNo.Value = dt.Rows[i]["STDD_NUMBERING_TO"].ToString();
                tempRow.Cells.Add(cellToNo);

                DataGridViewCell cellRemarks = new DataGridViewTextBoxCell();
                cellRemarks.Value = dt.Rows[i]["STDD_REMARKS"].ToString();
                tempRow.Cells.Add(cellRemarks);

                intRow = intRow + 1;
                gvIndentDetails.Rows.Add(tempRow);
                //gvIndentDetails.Columns[5].ReadOnly = false;

            }
        }

        private void btnClearItems_Click(object sender, EventArgs e)
        {
            gvIndentDetails.Rows.Clear();

        }






        private void txtIndentByEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtIndentByEcode_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtIndentByEcode.Text.Length > 4)
            {
                GetEmpDetaill();
            }
            //else
            //    txtEmpName.Text = "";
            //    cmbBranch_optional.SelectedIndex = -1;
            //    cmbCompany.SelectedIndex = 0;
        }

        private void GetEmpDetaill()
        {

            objSQLDB = new SQLDB();
            DataTable dt = new DataTable();
            string strSql = "";
            try
            {
                objSQLDB = new SQLDB();
                strSql = "SELECT ECODE,MEMBER_NAME,CM_COMPANY_NAME,BRANCH_NAME,BM.BRANCH_CODE,CM_COMPANY_CODE FROM EORA_MASTER EM " +
                    " INNER JOIN BRANCH_MAS BM ON BM.BRANCH_CODE = EM.BRANCH_CODE " +
                    " INNER JOIN COMPANY_MAS CM ON CM.CM_COMPANY_CODE = BM.COMPANY_CODE  WHERE ECODE ='" + txtIndentByEcode.Text.ToString() + "'";
                dt = objSQLDB.ExecuteDataSet(strSql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtIndentByEcode.Visible = true;
                    txtEmpName.Visible = true;
                    lblIndentBy.Visible = true;
                    txtEmpName.Text = dt.Rows[0]["MEMBER_NAME"].ToString();
                    cmbCompany.SelectedValue = dt.Rows[0]["CM_COMPANY_CODE"].ToString();
                    cmbBranch_optional.SelectedValue = dt.Rows[0]["BRANCH_CODE"].ToString();
                }
                else
                {
                    txtEmpName.Text = "";
                    cmbBranch_optional.SelectedIndex = -1;
                    cmbCompany.SelectedIndex = 0;
                    txtIndentByEcode.Visible = false;
                    txtEmpName.Visible = false;
                    lblIndentBy.Visible = false;


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

        }

        private void cmbBoxTransType_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (cmbBoxTransType.SelectedIndex == 0)
            {
                txtIndentByEcode.Visible = false;
                txtEmpName.Visible = false;
                lblIndentBy.Visible = false;

                objSecurity = new Security();
                DataTable dtCpy = objSecurity.GetCompanyDataSet().Tables[0];
                UtilityLibrary.PopulateControl(cmbCompany, dtCpy.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
                objSecurity = null;

                dtDispatchDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
                txtItemsCount.Text = "0.00";
                txtIndentAmt.Text = "0.00";
                //txtDispatchNo.Text = GenerateIndentNo();
                cmdDoorDeliveryFlag.SelectedIndex = 0;      
                cmdTransportOrCorrier.SelectedIndex = 0;
              
                if (txtDispatchNo.Text != "" && cmbCompany.SelectedIndex > 0 && cmbBranch_optional.SelectedIndex > 0 && flage == true)
                {
                    btnPrint.Enabled = true;
                }
                else
                {
                    btnPrint.Enabled = false;
                }
                           
                flage = false;
                cmbBranch_optional.SelectedIndex = -1;
                //GenerateIndentNo();
                txtDispatchNo.Text = "";
                dtDispatchDate.Value = DateTime.Today;
                txtAdvPaid.Text = "";
                txtToPay.Text = "";
                txtTotalFreight.Text = "";
                txtBagsCount.Text = "";
                txtTripLRNo.Text = "";
                txtTransportName.Text = "";
                cmdDoorDeliveryFlag.SelectedIndex = 0;
                txtEcode.Text = "";
                cmbTransporterOrCorrierName.SelectedIndex = 0;
                cmdTransportOrCorrier.SelectedIndex = 0;
                txtIndentByEcode.Text = "";
                txtEmpName.Text = "";
                btnPrint.Enabled = false;

                if (cmbBoxTransType.SelectedIndex == 0)
                {
                    txtIndentByEcode.Visible = false;
                    txtEmpName.Visible = false;
                    lblIndentBy.Visible = false;
                }
                else
                {
                    txtIndentByEcode.Visible = true;
                    txtEmpName.Visible = true;
                    lblIndentBy.Visible = true;

                }
                gvIndentDetails.Rows.Clear();
               
            }
            if (cmbBoxTransType.SelectedIndex == 1)
            {
                txtIndentByEcode.Visible = true;
                txtEmpName.Visible = true;
                lblIndentBy.Visible = true;

                objSecurity = new Security();
                DataTable dtCpy = objSecurity.GetCompanyDataSet().Tables[0];
                UtilityLibrary.PopulateControl(cmbCompany, dtCpy.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
                objSecurity = null;

                dtDispatchDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
                txtItemsCount.Text = "0.00";
                txtIndentAmt.Text = "0.00";
                //txtDispatchNo.Text = GenerateIndentNo();
                cmdDoorDeliveryFlag.SelectedIndex = 0;
                cmdTransportOrCorrier.SelectedIndex = 0;

                if (txtDispatchNo.Text != "" && cmbCompany.SelectedIndex > 0 && cmbBranch_optional.SelectedIndex > 0 && flage == true)
                {
                    btnPrint.Enabled = true;
                }
                else
                {
                    btnPrint.Enabled = false;
                }

                flage = false;
                cmbBranch_optional.SelectedIndex = -1;
                //GenerateIndentNo();
                txtDispatchNo.Text = "";
                dtDispatchDate.Value = DateTime.Today;
                txtAdvPaid.Text = "";
                txtToPay.Text = "";
                txtTotalFreight.Text = "";
                txtBagsCount.Text = "";
                txtTripLRNo.Text = "";
                txtTransportName.Text = "";
                cmdDoorDeliveryFlag.SelectedIndex = 0;
                txtEcode.Text = "";
                cmbTransporterOrCorrierName.SelectedIndex = 0;
                cmdTransportOrCorrier.SelectedIndex = 0;
                txtIndentByEcode.Text = "";
                txtEmpName.Text = "";
                btnPrint.Enabled = false;

                if (cmbBoxTransType.SelectedIndex == 0)
                {
                    txtIndentByEcode.Visible = false;
                    txtEmpName.Visible = false;
                    lblIndentBy.Visible = false;
                }
                else
                {
                    txtIndentByEcode.Visible = true;
                    txtEmpName.Visible = true;
                    lblIndentBy.Visible = true;

                }
                gvIndentDetails.Rows.Clear();
            }
        }

        private void txtIndentByEcode_TextChanged(object sender, EventArgs e)
        {
            if (txtIndentByEcode.Text.Length > 4)
            {
                GetEmpDetaill();
            }
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
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 8)
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

        private void gvIndentDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //if (e.ColumnIndex == gvIndentDetails.Columns["ImgPrint"].Index)
                //{
                //    CommonData.ViewReport = "STATIONARY_WITH_OUT_INDENT_FOR_DC";
                //    ReportViewer oReportViewer = new ReportViewer(cmbCompany.SelectedValue.ToString(),cmbBranch_optional.SelectedValue.ToString(),CommonData.FinancialYear,Convert.ToInt32(txtDispatchNo.Text.ToString()));
                //    oReportViewer.Show();
                //}

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
        }
        private void OrderSlNo()
        {
            if (gvIndentDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
                {
                    gvIndentDetails.Rows[i].Cells["SlNo_ref"].Value = i + 1;
                }
            }
        }

        private void gvIndentDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                if (Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["Price"].Value) >= 0 && Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["DispatchQty"].Value) >= 0)
                {
                    gvIndentDetails.Rows[e.RowIndex].Cells["ApprovedQty"].Value = gvIndentDetails.Rows[e.RowIndex].Cells["DispatchQty"].Value;
                    //gvIndentDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["DispatchQty"].Value) * (Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["Price"].Value));
                    //gvIndentDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["Amount"].Value).ToString("f");
                }
            }
            //CalculateTot
        }

        private void cmbBranch_optional_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBranch_optional.SelectedIndex > 0)
            {
                txtDispatchNo.Text = GenerateIndentNo();
            }
            else
            {             
                flage = false;
                //cmbBranch_optional.SelectedIndex = -1;
                //GenerateIndentNo();
                txtDispatchNo.Text = "";
                dtDispatchDate.Value = DateTime.Today;
                txtAdvPaid.Text = "";
                txtToPay.Text = "";
                txtTotalFreight.Text = "";
                txtBagsCount.Text = "";
                txtTripLRNo.Text = "";
                txtTransportName.Text = "";
                cmdDoorDeliveryFlag.SelectedIndex = 0;
                txtEcode.Text = "";
                cmbTransporterOrCorrierName.SelectedIndex = 0;
                cmdTransportOrCorrier.SelectedIndex = 0;
                txtIndentByEcode.Text = "";
                txtEmpName.Text = "";

                if (cmbBoxTransType.SelectedIndex == 0)
                {
                    txtIndentByEcode.Visible = false;
                    txtEmpName.Visible = false;
                    lblIndentBy.Visible = false;
                }
                gvIndentDetails.Rows.Clear();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (txtDispatchNo.Text != "" && cmbCompany.SelectedIndex > 0 && cmbBranch_optional.SelectedIndex > 0 && flage == true)
            {
                CommonData.ViewReport = "STATIONARY_WITH_OUT_INDENT_FOR_DC";
                ReportViewer oReportViewer = new ReportViewer(cmbCompany.SelectedValue.ToString(), cmbBranch_optional.SelectedValue.ToString(), CommonData.FinancialYear, Convert.ToInt32(txtDispatchNo.Text.ToString()));
                oReportViewer.Show();
            }
        }

    
      

      
    }

}

     
    
    


