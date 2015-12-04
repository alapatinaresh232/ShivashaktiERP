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

namespace SSCRM
{
    public partial class JournalVoucher : Form
    {
        SQLDB objDB = null;
        public JournalVoucher()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void JournalVoucher_Load(object sender, EventArgs e)
        {
            txtComanpanyName.Text = CommonData.CompanyName;
            txtBranch.Text = CommonData.BranchName;
            txtDocMonth.Text = CommonData.DocMonth;
            GenerateVoucherId();
            cbEmpType.SelectedIndex = 0;
            dtpInvoiceDate.Value = DateTime.Today;

        }
        private void GenerateVoucherId()
        {
            try
            {
                string strCMD = "SELECT ISNULL(MAX(VCO_VOUCHER_ID),0)+1 VoucherId FROM FA_VOUCHER_OTHERS WHERE vco_company_code='" + CommonData.CompanyCode +
                    "' AND VCO_BRANCH_CODE='" + CommonData.BranchCode + "' AND VCO_FIN_YEAR='" + CommonData.FinancialYear + "' and VCO_DOC_TYPE='JV' ";

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
                                    " END AS STATUS,hafp_finger_fp2,desig " +
                                    " FROM HR_APPL_MASTER_HEAD left join HR_APPL_FINGER_PRINTS on HAMH_APPL_NUMBER=hafp_APPL_NUMBER " +
                    " inner join eora_master on ecode=HAMH_EORA_CODE WHERE HAMH_EORA_CODE=" + txtEcodeSearch.Text + " ";
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
                        else
                            picEmpPhoto.BackgroundImage = null;

                        //DataSet dsSig = objDB.ExecuteDataSet("SELECT isnull(isnull(HAUM_SIG1,HAUM_SIG2),HAUM_SIG3) HAUM_SIG FROM HR_APPL_UAN_MAS WHERE HAUM_APPL_NO=" + txtApplNo.Text);
                        //if (dsSig.Tables[0].Rows.Count > 0)
                        //{
                        //    if (dsSig.Tables[0].Rows[0]["HAUM_SIG"].ToString().Length != 0)
                        //        GetImage((byte[])dsSig.Tables[0].Rows[0]["HAUM_SIG"], "SIG");
                        //    else
                        //        pbSig2.BackgroundImage = null;
                        //}
                        //GenerateNarration();
                    }
                    else
                    {

                        txtApplNo.Text = "";
                        txtName.Text = "";
                        picEmpPhoto.BackgroundImage = null;

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
                picEmpPhoto.BackgroundImage = null;

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
                if (TYPE == "PHOTO")
                {
                    picEmpPhoto.BackgroundImage = newImage;
                    this.picEmpPhoto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cbEmpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEmpType.SelectedIndex == 0)
            {
                txtEcodeSearch.Visible = true;
                txtName.Visible = true;
                txtOthersName.Visible = false;
            }
            if (cbEmpType.SelectedIndex == 1)
            {
                txtEcodeSearch.Visible = false;
                txtName.Visible = false;
                txtOthersName.Visible = true;
            }
        }

        private void btnBillDetails_Click(object sender, EventArgs e)
        {
            if ((txtName.Text.Length > 0 && cbEmpType.SelectedIndex == 0) || txtOthersName.Text.Length > 3)
            {
                AddJVAccountDetails obj = new AddJVAccountDetails();
                obj.objJournalVoucher = this;
                obj.Show();
            }
            else
            {
                MessageBox.Show("Please Enter Valid Ecode OR Name");
                //btnSave.Enabled = false;
            }
        }
        public void CalculateTotal()
        {
            double creditTotal = 0,debitTotal=0;
            for (int i = 0; i < gvBillDetails.Rows.Count; i++)
            {
                if (gvBillDetails.Rows[i].Cells["CrDr"].Value == "CREDIT")
                {
                    if (Convert.ToDouble(gvBillDetails.Rows[i].Cells["AmtReceived"].Value.ToString()) > 0 &&
                        (Convert.ToString(gvBillDetails.Rows[i].Cells["AmtReceived"].Value) != ""))
                        creditTotal += Convert.ToDouble(gvBillDetails.Rows[i].Cells["AmtReceived"].Value);
                }
                else
                {
                    if (Convert.ToDouble(gvBillDetails.Rows[i].Cells["AmtReceived"].Value.ToString()) > 0 &&
                     (Convert.ToString(gvBillDetails.Rows[i].Cells["AmtReceived"].Value) != ""))
                        debitTotal += Convert.ToDouble(gvBillDetails.Rows[i].Cells["AmtReceived"].Value);
                }


            }
            txtCreditlAmt.Text = creditTotal.ToString();
            txtDebitAmt.Text = debitTotal.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtEcodeSearch.Text = "";
            txtApplNo.Text = "";

            txtVouchRefNo.Text = "";


           

            picEmpPhoto.BackgroundImage = null;



            //dtPayVoucher.Rows.Clear();
            gvBillDetails.Rows.Clear();
            txtDebitAmt.Text = "";
            txtCreditlAmt.Text = "";
            //btnSave.Enabled = false;
            btnPrint.Enabled = false;
            GenerateVoucherId();
            txtOthersName.Text = "";
            txtNarration.Text = "";
            cbEmpType.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                if (SaveFaVoucherData() > 0)
                {
                    MessageBox.Show("Data Saved Successfully ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnClear_Click(null, null);
                    txtVouchRefNo.Focus();
                }
                else
                {
                    

                    string strSQL = " DELETE FROM FA_VOUCHER WHERE VC_COMPANY_CODE='" + CommonData.CompanyCode +
                               "' AND VC_BRANCH_CODE='" + CommonData.BranchCode +
                               "' AND VC_FIN_YEAR='" + CommonData.FinancialYear +
                               "' AND VC_DOC_MONTH ='" + CommonData.DocMonth +
                               "' AND VC_DOC_TYPE='JV'" +
                               " AND VC_VOUCHER_ID='" + txtVoucherId.Text + "'";

                    strSQL += " DELETE from FA_VOUCHER_OTHERS" +
                              " WHERE VCO_COMPANY_CODE='" + CommonData.CompanyCode +
                              "' AND VCO_BRANCH_CODE='" + CommonData.BranchCode +
                              "' AND VCO_VOUCHER_ID='" + txtVoucherId.Text +
                              "' AND VCO_DOC_TYPE='JV"+
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
            if (txtOthersName.Text.Length > 15)
            {
                ShortName = ShortName.Substring(0, 15);

            }
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
               
                strSQL += " DELETE FROM FA_VOUCHER WHERE VC_COMPANY_CODE='" + CommonData.CompanyCode +
                          "' AND VC_BRANCH_CODE='" + CommonData.BranchCode +
                          "' AND VC_FIN_YEAR='" + CommonData.FinancialYear +
                          "' AND VC_DOC_MONTH ='" + CommonData.DocMonth +
                          "' AND VC_DOC_TYPE='JV'" +
                          " AND VC_VOUCHER_ID='" + txtVoucherId.Text + "'";

                strSQL += " DELETE from FA_VOUCHER_OTHERS" +
                          " WHERE VCO_COMPANY_CODE='" + CommonData.CompanyCode +
                          "' AND VCO_BRANCH_CODE='" + CommonData.BranchCode +
                          "' AND VCO_VOUCHER_ID='" + txtVoucherId.Text +
                          "' AND VCO_DOC_MONTH ='" + CommonData.DocMonth +
                          "'  AND VCO_FIN_YEAR='" + CommonData.FinancialYear +
                          "' AND VCO_DOC_TYPE='JV'";
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
                                          ") VALUES('" + CommonData.CompanyCode +
                                          "','" + CommonData.BranchCode +
                                          "','" + CommonData.FinancialYear +
                                          "','" + CommonData.DocMonth +
                                          "','JV'" +
                                          ",'" + txtVoucherId.Text +
                                          "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                          "','" + txtNarration.Text.Trim().Replace("'", "") +
                                          "','" +
                                          "','" +
                                          "','" +
                                          "','" + txtVouchRefNo.Text.Trim().Replace("'", "") +
                                          "','" + CommonData.LogUserId +
                                          "',getdate())";

               
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
                              ",VC_CREATED_DATE)" +
                              " VALUES('" + CommonData.CompanyCode +
                              "','" + CommonData.BranchCode +
                              "','" + CommonData.FinancialYear +
                              "','" + CommonData.DocMonth +
                              "','JV'" +
                               ",'" + txtVoucherId.Text +
                               "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                               "','" + (j) +
                               "','" + gvBillDetails.Rows[i].Cells["AccountId"].Value ;
                        if (gvBillDetails.Rows[i].Cells["CrDr"].Value == "CREDIT")
                        {
                            strSQL += "','C";
                        }
                        else
                        {
                            strSQL += "','D";
                        }
                               
                             strSQL+=  "','" + gvBillDetails.Rows[i].Cells["AmtReceived"].Value +
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
                               "','','" + txtEcodeSearch.Text + "','" +
                               "','" +
                               "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                               "','" +
                               "','" +
                               "','" +
                               "','" + txtDebitAmt.Text +
                               "','JV'" +
                               ",'N" +
                               "','N" +
                               "','" + CommonData.LogUserId +
                               "',GETDATE())";
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
            if (txtName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Enter valid Ecode", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtEcodeSearch.Focus();
                return flag;
            }
            //if(dtPayVoucher.Rows.Count==0)
            //{
            //    flag = false;
            //    MessageBox.Show("Enter Amount Details", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    //txtVouchRefNo.Focus();
            //    return flag;
            //}

          
            if (cbEmpType.SelectedIndex > 0 && txtOthersName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Others Name", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return flag;
            }
            if(gvBillDetails.Rows.Count==0)
            {
                flag = false;
                MessageBox.Show("Please Add Accounts", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return flag;
            }
            if (txtCreditlAmt.Text != txtDebitAmt.Text )
            {
                flag = false;
                MessageBox.Show("Credit and Debit Amount Should be Equal", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return flag;
            }
            else
            {

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
             string strSQL = "";
             if (dlgResult == DialogResult.Yes)
             {
                  strSQL = " DELETE FROM FA_VOUCHER WHERE VC_COMPANY_CODE='" + CommonData.CompanyCode +
                             "' AND VC_BRANCH_CODE='" + CommonData.BranchCode +
                             "' AND VC_FIN_YEAR='" + CommonData.FinancialYear +
                             "' AND VC_DOC_MONTH ='" + CommonData.DocMonth +
                             "' AND VC_DOC_TYPE='JV'" +
                             " AND VC_VOUCHER_ID='" + txtVoucherId.Text + "'";

                 strSQL += " DELETE from FA_VOUCHER_OTHERS" +
                           " WHERE VCO_COMPANY_CODE='" + CommonData.CompanyCode +
                           "' AND VCO_BRANCH_CODE='" + CommonData.BranchCode +
                           "' AND VCO_VOUCHER_ID='" + txtVoucherId.Text +
                           "' AND VCO_DOC_MONTH ='" + CommonData.DocMonth +
                           "'  AND VCO_FIN_YEAR='" + CommonData.FinancialYear +
                           "' AND VCO_DOC_TYPE='JV'";

             }
             objDB = new SQLDB();
             int i = objDB.ExecuteSaveData(strSQL);
             if (i > 0)
             {
                 MessageBox.Show("Data Deleted Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 btnClear_Click(null, null);
                 txtVouchRefNo.Focus();
             }
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
                    //GenerateNarration();
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

        private void gvBillDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (gvBillDetails.Rows.Count > 0)
            {
                if (e.ColumnIndex == gvBillDetails.Columns["AmtReceived"].Index)
                {
                    //if ((Convert.ToString(gvBillDetails.Rows[e.RowIndex].Cells["AmtReceived"].Value) != "") 
                    //    && (Convert.ToDouble(gvBillDetails.Rows[e.RowIndex].Cells["AmtReceived"].Value)>0))
                    //{

                    //}
                    if (gvBillDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                    {
                        gvBillDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";

                    }
                    //GenerateNarration();
                    CalculateTotal();
                }
            }
        }

        private void gvBillDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 4)
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

        private void txtVouchRefNo_Validated(object sender, EventArgs e)
        {
            objDB = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();

            try
            {
                param[0] = objDB.CreateParameter("@company_code", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objDB.CreateParameter("@branch_code", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objDB.CreateParameter("@Fin_year", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objDB.CreateParameter("@Voucher_Type", DbType.String, "JV", ParameterDirection.Input);
                param[4] = objDB.CreateParameter("@Voucher_No", DbType.String, txtVouchRefNo.Text, ParameterDirection.Input);
                ds = objDB.ExecuteDataSet("GetJVVoucherDetails", CommandType.StoredProcedure, param);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {

                    txtEcodeSearch.Text = dt.Rows[0]["VC_SUB_COST_CENTRE_ID"].ToString();
                    txtVoucherId.Text = dt.Rows[0]["VoucherId"].ToString();
                    txtApplNo.Text = dt.Rows[0]["HAMH_APPL_NUMBER"].ToString();
                    txtName.Text = dt.Rows[0]["EmpName"].ToString();

                    if (dt.Rows[0]["HAMH_MY_PHOTO"].ToString().Length != 0)
                        GetImage((byte[])dt.Rows[0]["HAMH_MY_PHOTO"], "PHOTO");
                    else
                        picEmpPhoto.BackgroundImage = null;




                    txtNarration.Text = dt.Rows[0]["Remarks"].ToString();
                    //if (dt.Rows[0]["Type"].ToString() == "EMPLOYEE")
                    //{
                    //    cbEmpType.SelectedIndex = 0;
                    //}
                    //else
                    //{
                    //    cbEmpType.SelectedIndex = 1;
                    //    txtOthersName.Text = dt.Rows[0]["EmpName"].ToString();
                    //}
                    FillAccountDetails(dt);

                    btnPrint.Enabled = true;
                    btnSave.Enabled = false;
                }
                else
                {
                    btnSave.Enabled = true;
                    btnPrint.Enabled = false;
                    txtEcodeSearch.Text = "";

                    //ClearAllAmount();
                    //gvBillDetails.Rows.Clear();


                    txtDebitAmt.Text = "";
                    txtCreditlAmt.Text = "";
                    txtOthersName.Text = "";
                    cbEmpType.SelectedIndex = 0;
                    GenerateVoucherId();
                    txtNarration.Text = "";
                    txtName.Text = "";
                    txtApplNo.Text = "";
                    picEmpPhoto.BackgroundImage = null;




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

                if (dt.Rows[i]["VC_DEBIT_CREDIT"].ToString() == "C")
                {
                    DataGridViewCell cellDebitCredit = new DataGridViewTextBoxCell();
                    cellDebitCredit.Value = "CREDIT";
                    tempRow.Cells.Add(cellDebitCredit);
                }
                else
                {
                    DataGridViewCell cellDebitCredit = new DataGridViewTextBoxCell();
                    cellDebitCredit.Value = "DEBIT";
                    tempRow.Cells.Add(cellDebitCredit);
                }
                DataGridViewCell cellAmount = new DataGridViewTextBoxCell();
                cellAmount.Value = dt.Rows[i]["VC_AMOUNT"]; ;
                tempRow.Cells.Add(cellAmount);

                intRow = intRow + 1;
                gvBillDetails.Rows.Add(tempRow);
            }
            CalculateTotal();
        }

    }
}
