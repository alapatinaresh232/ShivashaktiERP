using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using System.IO;
using System.Data.SqlClient;
using System.Diagnostics;

namespace SSCRM
{
    public partial class ReceiptVoucher : Form
    {
        SQLDB objDB=null;
        public ReceiptVoucher()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           // ProcessStartInfo processInfo = new ProcessStartInfo();
           // //processInfo.WindowStyle = ProcessWindowStyle.Hidden;
           // processInfo.FileName = "cmd.exe";

           // processInfo.UseShellExecute = false;
           //// processInfo.WorkingDirectory = Path.GetDirectoryName("C:\\Users\\soft\\Desktop\\Link CRM.txt");
           // processInfo.Arguments = " START /MIN NOTEPAD /P C:\\Users\\soft\\Desktop\\Link CRM.txt";
           // Process P = new Process();
           // P.StartInfo = processInfo;
           // P.Start();
           // Process.Start("cmd.exe", "PRINT LinkCRM.txt");
           // string str = "START /MIN NOTEPAD /P LinkCRM.txt";
           //// string str = "/C copy /b Image1.jpg + Archive.rar Image2.jpg";
           //// Process.Start("cmd.exe", str);

          //  ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd", "/c START /MIN NOTEPAD /P C:\\Users\\soft\\Desktop\\LinkCRM.txt");
          ////  processStartInfo.RedirectStandardOutput = true;
          // // processStartInfo.RedirectStandardError = true;
          //  processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
          //  processStartInfo.UseShellExecute = false;

          //  Process process = Process.Start(processStartInfo);


            this.Close();
            this.Dispose();
        }

        private void ReceiptVoucher_Load(object sender, EventArgs e)
        {
            txtComanpanyName.Text = CommonData.CompanyName;
            txtBranch.Text = CommonData.BranchName;
            txtDocMonth.Text = CommonData.DocMonth;
            cbAcctType.SelectedIndex = 0;
            cbPaymentMode.SelectedIndex = 0;
            cmbCashAccount.SelectedIndex = 0;
            GenerateVoucherId();
            GenerateVoucherRefNo();
            cbEmpType.SelectedIndex = 0;
            dtpInvoiceDate.Value = DateTime.Today;
        }
        private void GenerateVoucherId()
        {
            try
            {
                string sDocType="";
                if(cbAcctType.SelectedItem == "CASH")
                {
                    sDocType="CR";
                }
                else
                {
                    sDocType="BR";
                }


                string strCMD = "SELECT ISNULL(MAX(VCO_VOUCHER_ID),0)+1 VoucherId FROM FA_VOUCHER_OTHERS WHERE vco_company_code='" + CommonData.CompanyCode +
                    "' AND VCO_BRANCH_CODE='" + CommonData.BranchCode + "' AND VCO_FIN_YEAR='" + CommonData.FinancialYear + "' and vco_doc_type='"+sDocType+"' ";

                objDB = new SQLDB();
                DataTable DT = objDB.ExecuteDataSet(strCMD).Tables[0];
                if (DT != null && DT.Rows.Count > 0)
                {
                    txtVoucherId.Text = DT.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void GenerateVoucherRefNo()
        {
            try
            {
                string sDocType = "";
                if (cbAcctType.SelectedItem == "CASH")
                {
                    sDocType = "CR";
                }
                else
                {
                    sDocType = "BR";
                }
                string strCMD = "SELECT ISNULL(MAX(VCO_REF_NO),0)+1 VoucherId FROM FA_VOUCHER_OTHERS WHERE vco_company_code='" + CommonData.CompanyCode +
                    "' AND VCO_BRANCH_CODE='" + CommonData.BranchCode + "' AND VCO_FIN_YEAR='" + CommonData.FinancialYear + "' and vco_doc_type='"+sDocType+"' ";

                objDB = new SQLDB();
                DataTable DT = objDB.ExecuteDataSet(strCMD).Tables[0];
                if (DT != null && DT.Rows.Count > 0)
                {
                    txtVouchRefNo.Text = DT.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void FillCashAcc()
        {
            try
            {
                string strCMD = "SELECT AM_ACCOUNT_ID,AM_ACCOUNT_NAME FROM FA_ACCOUNT_MASTER WHERE AM_ACCOUNT_TYPE_ID='"+cbAcctType.SelectedItem+
                "' AND AM_COMPANY_CODE='" + CommonData.CompanyCode + "'";
                objDB = new SQLDB();
                DataTable dt = objDB.ExecuteDataSet(strCMD).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    cmbCashAccount.DataSource = dt;
                    cmbCashAccount.ValueMember = "AM_ACCOUNT_ID";
                    cmbCashAccount.DisplayMember = "AM_ACCOUNT_NAME";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cbAcctType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCashAcc();
            DataTable table = new DataTable();
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("name", typeof(string));

          
            if (cbAcctType.Text == "CASH")
            {
                table.Rows.Add("CASH", "CASH");
             
                //cbPaymentMode.SelectedItem = "CASH";
                cbPaymentMode.Enabled = false;
                txtChqDDNo.Enabled = false;
            }
            else
            {
                table.Rows.Add("CHEQUE", "CHEQUE");
                table.Rows.Add("DD", "DD");
                cbPaymentMode.Enabled = true;
                txtChqDDNo.Enabled = true;
            }
            cbPaymentMode.DataSource = table;
            cbPaymentMode.DisplayMember = "type";
            cbPaymentMode.ValueMember = "name";
        }

        private void cmbEmpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbEmpType.SelectedIndex==0)
            {
                txtEcodeSearch.Visible = true;
                txtName.Visible = true;
                txtOthersName.Visible = false;
            }
            if (cbEmpType.SelectedIndex == 1)
            {
                txtEcodeSearch.Visible = false;
                txtName.Visible = false;
                txtOthersName.Visible =true;
            }


        }

        private void btnBillDetails_Click(object sender, EventArgs e)
        {
            if ((txtName.Text.Length > 0 && cbEmpType.SelectedIndex == 0) || txtOthersName.Text.Length > 3)
            {
                AddReceiptAccountDetails obj = new AddReceiptAccountDetails("RECEIPT");
                obj.objRecieptVoucher = this;
                obj.Show();
            }

            else
            {
                MessageBox.Show("Please Enter Valid Ecode OR Name");
                //btnSave.Enabled = false;
            }
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

        private void txtEcodeSearch_Validated(object sender, EventArgs e)
        {
            if (txtEcodeSearch.Text.Length > 4)
            {
                try
                {
                    string strSQL = "SELECT HAMH_APPL_NUMBER,HAMH_NAME,HAMH_MY_PHOTO, case WHEN hamh_working_status='W' THEN 'WORKING' " +
                                    " WHEN hamh_working_status='L' THEN 'LEFT'" +
                                    " WHEN hamh_working_status='R' THEN 'RESIGNED' " +
                                    " WHEN hamh_working_status='P' THEN 'PENDING'" +
                                    " END AS STATUS,hafp_finger_fp2,DESIG_SHORT_NAME desig " +
                                    " FROM HR_APPL_MASTER_HEAD left join HR_APPL_FINGER_PRINTS on HAMH_APPL_NUMBER=hafp_APPL_NUMBER " +
                                 //   " LEFT JOIN DESIG_MAS ON DESG_ID=desig_code " +
                    " inner join eora_master on ecode=HAMH_EORA_CODE LEFT JOIN DESIG_MAS ON DESG_ID=desig_code WHERE HAMH_EORA_CODE=" + txtEcodeSearch.Text + " ";
                    DataTable dt = objDB.ExecuteDataSet(strSQL).Tables[0];

                    //txtStatus.Text = dt.Rows[0]["STATUS"].ToString();
                    // DataSet dsPhoto = objDB.ExecuteDataSet("SELECT HAPS_PHOTO_SIG FROM HR_APPL_PHOTO_SIG WHERE HAPS_APPL_NUMBER = " + dt.Rows[0][0]);

                    if (dt.Rows.Count > 0)
                    {
                        txtName.Text = dt.Rows[0]["HAMH_NAME"].ToString();
                        txtApplNo.Text = dt.Rows[0]["HAMH_APPL_NUMBER"].ToString();
                        txtDesigName.Text = dt.Rows[0]["desig"].ToString();
                        if (dt.Rows[0]["HAMH_MY_PHOTO"].ToString().Length != 0)
                            GetImage((byte[])dt.Rows[0]["HAMH_MY_PHOTO"], "PHOTO");
                        
                        //else
                        //    picEmpPhoto.BackgroundImage = null;
                        
                        //DataSet dsSig = objDB.ExecuteDataSet("SELECT isnull(isnull(HAUM_SIG1,HAUM_SIG2),HAUM_SIG3) HAUM_SIG FROM HR_APPL_UAN_MAS WHERE HAUM_APPL_NO=" + txtApplNo.Text);
                        //if (dsSig.Tables[0].Rows.Count > 0)
                        //{
                        //    if (dsSig.Tables[0].Rows[0]["HAUM_SIG"].ToString().Length != 0)
                        //        GetImage((byte[])dsSig.Tables[0].Rows[0]["HAUM_SIG"], "SIG");
                        //    else
                        //        pbSig2.BackgroundImage = null;
                        //}
                        GenerateNarration();
                    }
                    else
                    {
                       
                        txtApplNo.Text = "";
                        txtName.Text = "";
                        //picEmpPhoto.BackgroundImage = null;
                     
                        txtDesigName.Text = "";
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                txtApplNo.Text = "";
                txtName.Text = "";
                //picEmpPhoto.BackgroundImage = null;
              
            }
        }
        public void GetImage(byte[] imageData, string TYPE)
        {
            try
            {
                Image newImage;
                using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                {
                    ms.Write(imageData, 0, imageData.Length);
                    newImage = Image.FromStream(ms, true);
                }
                //if (TYPE == "PHOTO")
                //{
                //    picEmpPhoto.BackgroundImage = newImage;
                //    this.picEmpPhoto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtEcodeSearch.Text = "";
            txtApplNo.Text = "";

            txtVouchRefNo.Text = "";
            txtEcodeSearch.Enabled = true;
            txtOthersName.Enabled = true;
            cbEmpType.Enabled = true;
           
            txtChqDDNo.Text = "";

            //picEmpPhoto.BackgroundImage = null;
          


            //dtPayVoucher.Rows.Clear();
            gvBillDetails.Rows.Clear();
            txtTotalAmt.Text = "0";
            txtTotBills.Text = "0";
            txtTotalCash.Text = "0";
            //btnSave.Enabled = false;
            btnPrint.Enabled = false;
            GenerateVoucherId();
            GenerateVoucherRefNo();
            txtOthersName.Text = "";
            txtNarration.Text = "";
            cbEmpType.SelectedIndex = 0;
            
            //string strSQL = "SELECT dflt_doc_type,dflt_sort_order_number,dflt_company_code,dflt_account_id VC_ACCOUNT_ID,am_account_name AccountName" +
            //    //",'' VC_MAIN_COST_CENTRE_ID,'' MajorCostCentreName"
            //           ",0 VC_AMOUNT FROM FA_PMTR_VOUACCTS INNER JOIN FA_ACCOUNT_MASTER ON dflt_company_code=am_company_code AND dflt_account_id=am_account_id WHERE dflt_doc_type='CP'" +
            //           "ORDER BY dflt_sort_order_number ASC";
            //objDB = new SQLDB();
            //DataTable dt1 = objDB.ExecuteDataSet(strSQL).Tables[0];
            //FillAccountDetails(dt1);
        }

        private void gvBillDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvBillDetails.Columns["Delete"].Index)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    //string SlNo = gvBillDetails.Rows[e.RowIndex].Cells[gvBillDetails.Columns["AccountId"].Index].Value.ToString();
                    //DataRow[] dr = dtPayVoucher.Select("ACC_ID='" + SlNo+"'");
                    //dtPayVoucher.Rows.Remove(dr[0]);
                    DataGridViewRow dgvr = gvBillDetails.Rows[e.RowIndex];
                    gvBillDetails.Rows.Remove(dgvr);
                    OrderSlNo();
                    //GetAccountDetials();
                    MessageBox.Show("Selected information Has Been Deleted", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GenerateNarration();
                    CalculateTotal();
                }
            }
        }
        private void OrderSlNo()
        {
            if (gvBillDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvBillDetails.Rows.Count; i++)
                {
                    gvBillDetails.Rows[i].Cells["SLNO"].Value = i + 1;
                }
            }
        }
        public void CalculateTotal()
        {
            double total = 0, totalCash = 0, totalBills = 0;
            for (int i = 0; i < gvBillDetails.Rows.Count; i++)
            {
                if (Convert.ToDouble(gvBillDetails.Rows[i].Cells["AmtReceived"].Value.ToString()) > 0 &&
                    (Convert.ToString(gvBillDetails.Rows[i].Cells["AmtReceived"].Value) != ""))
                    total += Convert.ToDouble(gvBillDetails.Rows[i].Cells["AmtReceived"].Value);
              //  gvBillDetails.Rows[i].Cells["Cash"].Value = gvBillDetails.Rows[i].Cells["AmtReceived"].Value;
                //if (Convert.ToDouble(gvBillDetails.Rows[i].Cells["Cash"].Value.ToString()) > 0 &&
                //    (Convert.ToString(gvBillDetails.Rows[i].Cells["Cash"].Value) != ""))
                //    totalCash += Convert.ToDouble(gvBillDetails.Rows[i].Cells["Cash"].Value);
                //if (Convert.ToDouble(gvBillDetails.Rows[i].Cells["Bills"].Value.ToString()) > 0 &&
                //    (Convert.ToString(gvBillDetails.Rows[i].Cells["Bills"].Value) != ""))
                //    totalBills += Convert.ToDouble(gvBillDetails.Rows[i].Cells["Bills"].Value);

            }
            txtTotalAmt.Text = total.ToString();
            txtTotalCash.Text = "0";
            txtTotBills.Text = "0";
            //txtTotalCash.Text = total.ToString();
            //txtTotBills.Text = totalBills.ToString();
        }
        public void GenerateNarration()
        {
            string strNarration = "BEING CASH ";
            string str = "";
            //for (int i = 0; i < gvBillDetails.Rows.Count; i++)
            //{
            //    if (Convert.ToDouble(gvBillDetails.Rows[i].Cells["AmtReceived"].Value.ToString()) > 0 &&
            //        (Convert.ToString(gvBillDetails.Rows[i].Cells["AmtReceived"].Value) != ""))
            //    {

            //        str += gvBillDetails.Rows[i].Cells["AccName"].Value.ToString().Split('(')[0] + ",";
            //    }
            //}
            //if (str.Length > 0)
            //    str = str.Remove(str.Length - 1);

            if (cbEmpType.SelectedItem == "EMPLOYEE")
                strNarration += str + " RECEIVED FROM " + txtName.Text + "(" + txtDesigName.Text + ") FOR THE MONTH OF " + txtDocMonth.Text + " ";
            else
                strNarration += str + " RECEIVED FROM " + txtOthersName.Text + " FOR THE MONTH OF " + txtDocMonth.Text + " ";
            txtNarration.Text = strNarration;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(CheckData())
            {
                if (SaveFaVoucherData() > 0)
                {
                    MessageBox.Show("Data Saved Successfully ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DialogResult resultSms = MessageBox.Show("Do you want to Print this Payment Voucher", "SSERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultSms == DialogResult.Yes)
                    {
                        CommonData.ViewReport = "SSCRM_REP_RECEIPT_VOUCHER_PRINT";
                        ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, CommonData.FinancialYear, txtVouchRefNo.Text);
                        objReportview.Show();
                    }
                    //flagUpdate = false;
                   




                    btnClear_Click(null, null);
                    txtVouchRefNo.Focus();
                }
                else
                {
                    string strTYPE="";
                    if (cbAcctType.SelectedItem == "CASH")
                    {
                        strTYPE = "CR";
                    }
                    else
                    {
                        strTYPE = "BR";
                    }

                  string strSQL = " DELETE FROM FA_VOUCHER WHERE VC_COMPANY_CODE='" + CommonData.CompanyCode +
                             "' AND VC_BRANCH_CODE='" + CommonData.BranchCode +
                             "' AND VC_FIN_YEAR='" + CommonData.FinancialYear +
                             "' AND VC_DOC_MONTH ='" + CommonData.DocMonth +
                             "' AND VC_DOC_TYPE='"+strTYPE+"'" +
                             " AND VC_VOUCHER_ID='" + txtVoucherId.Text + "'";

                    strSQL += " DELETE from FA_VOUCHER_OTHERS" +
                              " WHERE VCO_COMPANY_CODE='" + CommonData.CompanyCode +
                              "' AND VCO_BRANCH_CODE='" + CommonData.BranchCode +
                              "' AND VCO_VOUCHER_ID='" + txtVoucherId.Text +
                              "' AND VCO_DOC_TYPE='" + strTYPE + 
                              "' AND VCO_DOC_MONTH ='" + CommonData.DocMonth +
                              "'  AND VCO_FIN_YEAR='" + CommonData.FinancialYear + "'";

                    objDB = new SQLDB();
                    objDB.ExecuteSaveData(strSQL);
                }
            }
        }
        private int SaveFaVoucherData()
        {
            string strSQL = "";
            int iRes = 0;
            string ShortName = "";
            //if (txtOthersName.Text.Length > 15)
            //{
            //    ShortName = ShortName.Substring(0, 15);

            //}
            try
            {
                strSQL = "";
                //if (cmbEmpType.SelectedIndex == 0)
                //{
                //    for (int i = 0; i < dtPayVoucher.Rows.Count; i++)
                //    {
                //        strSQL += "INSERT INTO FA_COST_CENTRE(CC_COMPANY_CODE,CC_COST_CENTRE_ID,CC_COST_CENTRE_NAME,CC_MAJOR_COST_CENTRE_ID," +
                //            " CC_TYPE,CC_CREATED_BY,CC_CREATED_DATE)SELECT '" + CommonData.CompanyCode + "','" + txtEcodeSearch.Text + "','" + txtName.Text + "','" + dtPayVoucher.Rows[i]["MAJCOST_ID"] + "','C','" + CommonData.LogUserId + "',GETDATE() " +
                //            " WHERE NOT EXISTS(SELECT * FROM FA_COST_CENTRE WHERE CC_COMPANY_CODE='" + CommonData.CompanyCode + "' AND CC_COST_CENTRE_ID='" + txtEcodeSearch.Text + "' AND CC_MAJOR_COST_CENTRE_ID='" + dtPayVoucher.Rows[i]["MAJCOST_ID"] + "')";
                //    }
                //}
                string strType = "";
                if (cbAcctType.SelectedItem == "CASH")
                {
                    strType = "CR";
                }
                else
                {
                    strType = "BR";
                }
                strSQL += " DELETE FROM FA_VOUCHER WHERE VC_COMPANY_CODE='" + CommonData.CompanyCode +
                          "' AND VC_BRANCH_CODE='" + CommonData.BranchCode +
                          "' AND VC_FIN_YEAR='" + CommonData.FinancialYear +
                          "' AND VC_DOC_MONTH ='" + CommonData.DocMonth +
                          "' AND VC_DOC_TYPE='"+strType+"'" +
                          " AND VC_VOUCHER_ID='" + txtVoucherId.Text + "'";

                strSQL += " DELETE from FA_VOUCHER_OTHERS" +
                          " WHERE VCO_COMPANY_CODE='" + CommonData.CompanyCode +
                          "' AND VCO_BRANCH_CODE='" + CommonData.BranchCode +
                          "' AND VCO_VOUCHER_ID='" + txtVoucherId.Text +
                          "' AND VCO_DOC_MONTH ='" + CommonData.DocMonth +
                          "'  AND VCO_FIN_YEAR='" + CommonData.FinancialYear +
                          "' AND VCO_DOC_TYPE='"+strType+"'";
                strSQL += " INSERT INTO FA_VOUCHER_OTHERS(VCO_COMPANY_CODE" +
                                          ",VCO_BRANCH_CODE" +
                                          ",VCO_FIN_YEAR" +
                                          ",VCO_DOC_MONTH" +
                                          ",VCO_DOC_TYPE" +
                                          ",VCO_VOUCHER_ID" +
                                          ",VCO_VOUCHER_DATE" +
                                          ",VCO_NARRATION_1" +
                                          ",VCO_NARRATION_2" +
                                          ",VCO_EFFECT_NAME" +
                                          ",VCO_VOUCHER_NO" +
                                          ",VCO_REF_NO " +
                                          ",VCO_CREATED_BY" +
                                          ",VCO_CREATED_DATE" +
                                          ",VCO_STATUS) VALUES('" + CommonData.CompanyCode +
                                          "','" + CommonData.BranchCode +
                                          "','" + CommonData.FinancialYear +
                                          "','" + CommonData.DocMonth +
                                          "','" + strType + "'" +
                                          ",'" + txtVoucherId.Text +
                                          "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                          "','" + txtNarration.Text.Trim().Replace("'", "") +
                                          "','" +
                                          "','" +
                                          "','" +
                                          "','" + txtVouchRefNo.Text.Trim().Replace("'", "") +
                                          "','" + CommonData.LogUserId +
                                          "',getdate(),";
                 if (chkCancelDc.Checked == true)
                {
                    strSQL += "'CANCEL' )";
                }
                else
                {
                    strSQL += "'' )";
                }


                strSQL += " INSERT INTO FA_VOUCHER( VC_COMPANY_CODE" +
                       ",VC_BRANCH_CODE" +
                       ",VC_FIN_YEAR" +
                       ",VC_DOC_MONTH" +
                       ",VC_DOC_TYPE" +
                       ",VC_VOUCHER_ID" +
                       ",VC_VOUCHER_DATE" +
                       ",VC_SL_NO" +
                       ",VC_ACCOUNT_ID" +
                       ",VC_DEBIT_CREDIT" +
                       ",VC_AMOUNT" +
                       ",VC_MAIN_COST_CENTRE_ID" +
                       ",VC_SUB_COST_CENTRE_ID" +
                       ",VC_PAYMENT_MODE" +
                       ",VC_CHEQUE_NO" +
                       ",VC_CHEQUE_DATE" +
                       ",VC_REMARKS" +
                       ",VC_VOUCHER_NO" +
                       ",VC_CASH_BANK_ID" +
                       ",VC_VOUCHER_AMOUNT" +
                       ",VC_VOUCHER_TYPE" +
                       ",VC_APPROVED" +
                       ",VC_POSTED" +
                       ",VC_CREATED_BY" +
                       ",VC_CREATED_DATE,VC_CASH_AMT,VC_BILLS_AMT,VC_EMP_OTHR)" +
                       " VALUES('" + CommonData.CompanyCode +
                       "','" + CommonData.BranchCode +
                       "','" + CommonData.FinancialYear +
                       "','" + CommonData.DocMonth +
                       "','"+strType+"'" +
                        ",'" + txtVoucherId.Text +
                        "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                        "','0" +
                        "','" + cmbCashAccount.SelectedValue.ToString() +
                        "','D" +
                        "','" + txtTotalAmt.Text +
                        "','" +
                        "','" +
                        "','" +
                        "','" +
                        "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                        "','" +
                        "','" +
                        "','" +
                        "','" + txtTotalAmt.Text +
                        "','"+strType+"'" +
                        ",'N" +
                        "','N" +
                        "','" + CommonData.LogUserId +
                        "',GETDATE(),'" + txtTotalCash.Text + "','" + txtTotBills.Text;
                if (cbEmpType.Text == "EMPLOYEE")
                {
                    strSQL += "','EMPLOYEE')";
                }
                else
                {
                    strSQL += "','OTHERS')";
                }//+"')";
                int j = 0;
                for (int i = 0; i < gvBillDetails.Rows.Count; i++)
                {
                    if (Convert.ToDouble(gvBillDetails.Rows[i].Cells["AmtReceived"].Value.ToString()) > 0 &&
                    (Convert.ToString(gvBillDetails.Rows[i].Cells["AmtReceived"].Value) != ""))
                    {
                        j++;
                        strSQL += " INSERT INTO FA_VOUCHER( VC_COMPANY_CODE" +
                              ",VC_BRANCH_CODE" +
                              ",VC_FIN_YEAR" +
                              ",VC_DOC_MONTH" +
                              ",VC_DOC_TYPE" +
                              ",VC_VOUCHER_ID" +
                              ",VC_VOUCHER_DATE" +
                              ",VC_SL_NO" +
                              ",VC_ACCOUNT_ID" +
                              ",VC_DEBIT_CREDIT" +
                              ",VC_AMOUNT" +
                              ",VC_MAIN_COST_CENTRE_ID" +
                              ",VC_SUB_COST_CENTRE_ID" +
                              ",VC_EMP_OTHR" +
                              ",VC_PAYMENT_MODE" +
                              ",VC_CHEQUE_NO" +
                              ",VC_CHEQUE_DATE" +
                              ",VC_REMARKS" +
                              ",VC_VOUCHER_NO" +
                              ",VC_CASH_BANK_ID" +
                              ",VC_VOUCHER_AMOUNT" +
                              ",VC_VOUCHER_TYPE" +
                              ",VC_APPROVED" +
                              ",VC_POSTED" +
                              ",VC_CREATED_BY" +
                              ",VC_CREATED_DATE,VC_CASH_AMT,VC_BILLS_AMT)" +
                              " VALUES('" + CommonData.CompanyCode +
                              "','" + CommonData.BranchCode +
                              "','" + CommonData.FinancialYear +
                              "','" + CommonData.DocMonth +
                              "','"+strType+"'" +
                               ",'" + txtVoucherId.Text +
                               "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                               "','" + (j) +
                               "','" + gvBillDetails.Rows[i].Cells["AccountId"].Value +
                               "','C" +
                               "','" + gvBillDetails.Rows[i].Cells["AmtReceived"].Value +"','',";
                            //if (cmbEmpType.SelectedIndex == 0)
                            //{
                            //    strSQL += "','" + dtPayVoucher.Rows[i]["MAJCOST_ID"] +
                            //     "','" + txtEcodeSearch.Text;
                            //}
                            //else
                            //{
                            //    strSQL += "','','";
                            //}
                            //strSQL +="','" + cbPaymentMode.SelectedItem +
                        if (cbEmpType.Text == "EMPLOYEE")
                        {
                            strSQL += "'" + txtEcodeSearch.Text + "','EMPLOYEE'";
                        }
                        else
                        {
                            strSQL += "'" + txtOthersName.Text + "','OTHERS'";
                        }
                strSQL +=  ",'" + cbPaymentMode.Text +
                               "','" + txtChqDDNo.Text +
                               "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                               "','" +
                               "','" +
                               "','" + cmbCashAccount.SelectedValue +
                               "','" + txtTotalAmt.Text +
                               "','"+strType+"'" +
                               ",'N" +
                               "','N" +
                               "','" + CommonData.LogUserId +
                               "',GETDATE(),'" + txtTotalCash.Text + "','" + txtTotBills.Text + "')";
                    }
                }


                objDB = new SQLDB();
                iRes = objDB.ExecuteSaveData(strSQL);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iRes;
        }
        private bool CheckData()
        {
            bool flag = true;
            if (txtVouchRefNo.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Enter Voucher Referenc No", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtVouchRefNo.Focus();
                return flag;
            }
            if (cbEmpType.SelectedItem == "EMPLOYEE")
            {
                if (txtName.Text.Length == 0)
                {
                    flag = false;
                    MessageBox.Show("Enter valid Ecode", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtEcodeSearch.Focus();
                    return flag;
                }
            }
            else
            {
                if (txtOthersName.Text.Length == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Enter Others Name", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtEcodeSearch.Focus();
                    return flag;
                }
            }
            if (gvBillDetails.Rows.Count == 0)
            {
                flag = false;
                MessageBox.Show("Please Add Account ", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //txtVouchRefNo.Focus();
                return flag;
            }

            if (cbPaymentMode.SelectedIndex > 0 && txtChqDDNo.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Cheq/DD Ref No", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return flag;
            }
            //if (cbEmpType.SelectedIndex > 0 && txtOthersName.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter Others Name", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return flag;
            //}
            if (txtTotalAmt.Text.Length == 0 || txtTotalAmt.Text == "0" || txtTotalAmt.Text == "0.00")
            {
                flag = false;
                MessageBox.Show("Please Enter Amount", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return flag;
            }
            if ((txtTotalCash.Text.Length == 0 || txtTotalCash.Text == "0" || txtTotalCash.Text == "0.00")&&txtTotBills.Text=="0")
            {
                flag = false;
                MessageBox.Show("Please Enter Cash ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return flag;
            }
           
            //if(cmbAccounts.SelectedText == null)
            //{
            //    flag = false;
            //    MessageBox.Show("Select Accounts", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    cmbAccounts.Focus();
            //    return flag;
            //}
            //if (cmbMajorCost.SelectedValue == null)
            //{
            //    flag = false;
            //    MessageBox.Show("MajorCostCentre is not Exist Please create it in MajorCostCentre Master.", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    cmbMajorCost.Focus();
            //    return flag;
            //}
            //if (cmbAccounts.SelectedValue == null)
            //{
            //    flag = false;
            //    MessageBox.Show("Account is not Exist Please Create it in ChartOfAccounts Master.", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    cmbAccounts.Focus();
            //    return flag;
            //}
            //if (txtReceivedAmt.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Enter Amount", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    txtReceivedAmt.Focus();
            //    return flag;
            //}
            //if (txtRemarks.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Enter Remarks", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    txtRemarks.Focus();
            //    return flag;
            //}
            //if (txtPunchID.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Punch For Payment", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return flag;
            //}
            if (txtNarration.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Remarks", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return flag;
            }

            return flag;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
             DialogResult dlgResult = MessageBox.Show("Do you want delete ?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    string strTYPE = "";
                    if (cbAcctType.SelectedItem == "CASH")
                    {
                        strTYPE = "CR";
                    }
                    else
                    {
                        strTYPE = "BR";
                    }

                    string strSQL = " DELETE FROM FA_VOUCHER WHERE VC_COMPANY_CODE='" + CommonData.CompanyCode +
                               "' AND VC_BRANCH_CODE='" + CommonData.BranchCode +
                               "' AND VC_FIN_YEAR='" + CommonData.FinancialYear +
                               "' AND VC_DOC_MONTH ='" + CommonData.DocMonth +
                               "' AND VC_DOC_TYPE='" + strTYPE + "'" +
                               " AND VC_VOUCHER_ID='" + txtVoucherId.Text + "'";

                    strSQL += " DELETE from FA_VOUCHER_OTHERS" +
                              " WHERE VCO_COMPANY_CODE='" + CommonData.CompanyCode +
                              "' AND VCO_BRANCH_CODE='" + CommonData.BranchCode +
                              "' AND VCO_VOUCHER_ID='" + txtVoucherId.Text +
                              "' AND VCO_DOC_TYPE='" + strTYPE +
                              "' AND VCO_DOC_MONTH ='" + CommonData.DocMonth +
                              "'  AND VCO_FIN_YEAR='" + CommonData.FinancialYear + "'";

                    objDB = new SQLDB();
                    int i = objDB.ExecuteSaveData(strSQL);
                    if (i > 0)
                    {
                        MessageBox.Show("Data Deleted Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnClear_Click(null, null);
                        txtVouchRefNo.Focus();
                    }
               
            }
        }

        private void txtVouchRefNo_Validated(object sender, EventArgs e)
        {
            if(txtVouchRefNo.Text.Length>0)
            {
            objDB = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();

            try
            {
                param[0] = objDB.CreateParameter("@company_code", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objDB.CreateParameter("@branch_code", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objDB.CreateParameter("@Fin_year", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objDB.CreateParameter("@Voucher_Type", DbType.String, "CR", ParameterDirection.Input);
                param[4] = objDB.CreateParameter("@Voucher_No", DbType.String, txtVouchRefNo.Text, ParameterDirection.Input);
                ds = objDB.ExecuteDataSet("GetReceiptVoucherDetails", CommandType.StoredProcedure, param);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {

                    txtEcodeSearch.Text = dt.Rows[0]["VC_SUB_COST_CENTRE_ID"].ToString();
                    txtVoucherId.Text = dt.Rows[0]["VoucherId"].ToString();
                    txtApplNo.Text = dt.Rows[0]["HAMH_APPL_NUMBER"].ToString();
                    txtName.Text = dt.Rows[0]["EmpName"].ToString();
                    dtpInvoiceDate.Value = Convert.ToDateTime(dt.Rows[0]["VC_VOUCHER_DATE"].ToString());
                    if (dt.Rows[0]["HAMH_MY_PHOTO"].ToString().Length != 0)
                        GetImage((byte[])dt.Rows[0]["HAMH_MY_PHOTO"], "PHOTO");
                    //else
                    //    picEmpPhoto.BackgroundImage = null;

                    txtEcodeSearch.Enabled = false;
                    txtOthersName.Enabled = false;
                    cbEmpType.Enabled = false;
                    //if (DateTime.Now.AddDays(-3) <= Convert.ToDateTime(dt.Rows[0]["VC_VOUCHER_DATE"]) || CommonData.LogUserId.ToUpper() == "ADMIN")
                    //{
                    //    btnSave.Enabled = true;
                        
                    //}
                    //else
                    //{
                    //    btnSave.Enabled = false;
                        
                    //}


                   
                    
                    txtNarration.Text = dt.Rows[0]["Remarks"].ToString();
                    if (dt.Rows[0]["VC_EMP_OTHR"].ToString()!="OTHERS" )
                    {
                        cbEmpType.SelectedIndex = 0;
                    }
                    else
                    {
                        cbEmpType.SelectedIndex = 1;
                        txtOthersName.Text = dt.Rows[0]["EmpName"].ToString();
                    }
                    FillAccountDetails(dt);

                    txtTotBills.Text = dt.Rows[0]["VC_BILLS_AMT"].ToString();
                    txtTotalCash.Text = dt.Rows[0]["VC_CASH_AMT"].ToString();

                    //btnPrint.Enabled = true;
                    if (DateTime.Now.AddDays(-3) <= Convert.ToDateTime(dtpInvoiceDate.Value) || CommonData.LogUserId.ToUpper() == "ADMIN")
                    {
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        btnSave.Enabled = false;
                    }
                    if (CommonData.LogUserId.ToString().ToUpper() == "ADMIN")
                    {
                        btnPrint.Enabled = true;
                    }
                    btnPrints.Enabled = true;
                }
                else
                {
                    btnSave.Enabled = true;
                    btnPrint.Enabled = false;
                    txtEcodeSearch.Text = "";

                    txtEcodeSearch.Enabled = true;
                    txtOthersName.Enabled = true;
                    cbEmpType.Enabled = true;

                    //ClearAllAmount();
                    //gvBillDetails.Rows.Clear();


                    txtTotalAmt.Text = "0";
                    txtTotBills.Text = "0";
                    txtTotalCash.Text = "0";

                    txtOthersName.Text = "";
                    cbEmpType.SelectedIndex = 0;
                    GenerateVoucherId();
                    GenerateVoucherRefNo();
                    txtNarration.Text = "";
                    txtName.Text = "";
                    txtApplNo.Text = "";
                    //picEmpPhoto.BackgroundImage = null;
                   



                    //string strSQL = "SELECT dflt_doc_type,dflt_sort_order_number,dflt_company_code,dflt_account_id VC_ACCOUNT_ID,am_account_name AccountName" +
                    //    //",'' VC_MAIN_COST_CENTRE_ID,'' MajorCostCentreName"
                    //    ",0 VC_AMOUNT FROM FA_PMTR_VOUACCTS INNER JOIN FA_ACCOUNT_MASTER ON dflt_company_code=am_company_code AND dflt_account_id=am_account_id WHERE dflt_doc_type='CP'" +
                    //    "ORDER BY dflt_sort_order_number ASC";
                    //objDB = new SQLDB();
                    //DataTable dt1 = objDB.ExecuteDataSet(strSQL).Tables[0];
                    //FillAccountDetails(dt1);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            }
        }
        private void FillAccountDetails(DataTable dt)
        {
            //dtPayVoucher.Rows.Clear();

            int intRow = 1;
            gvBillDetails.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                //dtPayVoucher.Rows[i]["SL_NO"] = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellAccId = new DataGridViewTextBoxCell();
                cellAccId.Value = dt.Rows[i]["VC_ACCOUNT_ID"];
                tempRow.Cells.Add(cellAccId);

                DataGridViewCell cellAccName = new DataGridViewTextBoxCell();
                cellAccName.Value = dt.Rows[i]["AccountName"];
                tempRow.Cells.Add(cellAccName);

                //DataGridViewCell cellMajorCetreId = new DataGridViewTextBoxCell();
                //cellMajorCetreId.Value = dtPayVoucher.Rows[i]["MAJCOST_ID"];
                //tempRow.Cells.Add(cellMajorCetreId);

                //DataGridViewCell cellMajorCetreName = new DataGridViewTextBoxCell();
                //cellMajorCetreName.Value = dtPayVoucher.Rows[i]["MAJCOST_CENTRE_NAME"];
                //tempRow.Cells.Add(cellMajorCetreName);

                //DataGridViewCell cellEcode = new DataGridViewTextBoxCell();
                //cellEcode.Value = dtPayVoucher.Rows[i]["ECODE"];
                //tempRow.Cells.Add(cellEcode);


                //DataGridViewCell cellPayMode = new DataGridViewTextBoxCell();
                //cellPayMode.Value = dtPayVoucher.Rows[i]["PAYMENT_MODE"].ToString();
                //tempRow.Cells.Add(cellPayMode);

                //DataGridViewCell cellChqNO = new DataGridViewTextBoxCell();
                //cellChqNO.Value = dtPayVoucher.Rows[i]["CHQ_DD_NO"];
                //tempRow.Cells.Add(cellChqNO);

                //DataGridViewCell cellRemarks = new DataGridViewTextBoxCell();
                //cellRemarks.Value = dtPayVoucher.Rows[i]["REMARKS"];
                //tempRow.Cells.Add(cellRemarks);

                DataGridViewCell cellCash = new DataGridViewTextBoxCell();
                cellCash.Value = dt.Rows[i]["vc_cash_amt"]; ;
                tempRow.Cells.Add(cellCash);
                DataGridViewCell cellBill = new DataGridViewTextBoxCell();
                cellBill.Value = dt.Rows[i]["vc_bills_amt"]; ;
                tempRow.Cells.Add(cellBill);

                DataGridViewCell cellAmount = new DataGridViewTextBoxCell();
                cellAmount.Value = dt.Rows[i]["VC_AMOUNT"]; ;
                tempRow.Cells.Add(cellAmount);

                intRow = intRow + 1;
                gvBillDetails.Rows.Add(tempRow);
            }
            CalculateTotal();
        }

        private void cbPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbPaymentMode.SelectedItem == "CASH")
            //{
            //    txtChqDDNo.Enabled = false;
            //}
            //else
            //{
            //    txtChqDDNo.Enabled = true;
            //}
        }

        private void gvBillDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (gvBillDetails.Rows.Count > 0)
            {
                if (e.ColumnIndex == gvBillDetails.Columns["AmtReceived"].Index || e.ColumnIndex == gvBillDetails.Columns["Cash"].Index
                    || e.ColumnIndex == gvBillDetails.Columns["Bills"].Index)
                {
                    //if ((Convert.ToString(gvBillDetails.Rows[e.RowIndex].Cells["AmtReceived"].Value) != "") 
                    //    && (Convert.ToDouble(gvBillDetails.Rows[e.RowIndex].Cells["AmtReceived"].Value)>0))
                    //{

                    //}
                    if (gvBillDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                    {
                        gvBillDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";

                    }
                    GenerateNarration();
                    CalculateTotal();
                }
                //gvBillDetails.Rows[e.RowIndex].Cells["AmtReceived"].Value = Convert.ToInt32(gvBillDetails.Rows[e.RowIndex].Cells["Cash"].Value )+
                //    Convert.ToInt32(gvBillDetails.Rows[e.RowIndex].Cells["Bills"].Value);
                CalculateTotal();
            }
        }

        private void gvBillDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 3)
            {
                TextBox txtQty1 = e.Control as TextBox;
                if (txtQty1 != null)
                {

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
            //if (e.KeyChar == 46 )
            //{
            //    if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
            //        e.Handled = true;
            //}

        }

        private void btnClearProd_Click(object sender, EventArgs e)
        {
            gvBillDetails.Rows.Clear();
            //dtPayVoucher.Rows.Clear();
            CalculateTotal();
            txtNarration.Text = "";
        }

        private void txtTotalCash_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtTotalCash_TextChanged(object sender, EventArgs e)
        {
            if (txtTotalCash.Text == "")
            {
                txtTotalCash.Text = "0";
            }
            if (Convert.ToDouble(txtTotalCash.Text) <= Convert.ToDouble(txtTotalAmt.Text))
            {
                txtTotBills.Text = (Convert.ToDouble(txtTotalAmt.Text) - Convert.ToDouble(txtTotalCash.Text)).ToString();
            }
            else
            {
                txtTotalCash.Text = txtTotalAmt.Text;
                txtTotBills.Text = "0";
            }
        }

        private void btnPrints_Click(object sender, EventArgs e)
        {
            CommonData.ViewReport = "SSCRM_REP_RECEIPT_VOUCHER_PRINT";
            ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, CommonData.FinancialYear, txtVouchRefNo.Text);
            objReportview.Show();
        }

    }
}
