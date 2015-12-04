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
using ZkFingerDemo;
using System.Data.SqlClient;

namespace SSCRM
{
    public partial class PaymentVouchers : Form
    {
        SQLDB objDB;
        //public DataTable dtPayVoucher = new DataTable();
        int iNotMatchedCount = 0;
        bool flagUpdate = false;
        DataTable dtVerify = null;
        public PaymentVouchers()
        {
            InitializeComponent();
        }

        private void PaymentVouchers_Load(object sender, EventArgs e)
        {
            txtBranch.Text = CommonData.BranchName;
            txtDocMonth.Text = CommonData.DocMonth;
            txtComanpanyName.Text = CommonData.CompanyName;
            FillCashAcc();
            GenerateVoucherId();
            GenerateVoucherRefNo();
            cbPaymentMode.SelectedIndex = 0;
            cmbEmpType.SelectedIndex = 0;


            //#region CREATING TABLE FOR PayVoucher
            //dtPayVoucher.Columns.Add("SL_NO");
            //dtPayVoucher.Columns.Add("ACC_ID");
            //dtPayVoucher.Columns.Add("ACC_NAME");
            ////dtPayVoucher.Columns.Add("MAJCOST_ID");
            ////dtPayVoucher.Columns.Add("MAJCOST_CENTRE_NAME");
            ////dtPayVoucher.Columns.Add("ECODE");
            ////dtPayVoucher.Columns.Add("PAYMENT_MODE");
            ////dtPayVoucher.Columns.Add("CHQ_DD_NO");
          
            ////dtPayVoucher.Columns.Add("REMARKS");
            //dtPayVoucher.Columns.Add("AMOUNT");
            //#endregion
            try
            {
                  objDB = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();


            param[0] = objDB.CreateParameter("@xCompCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                ds = objDB.ExecuteDataSet("GetAccountsinSortOrder", CommandType.StoredProcedure, param);
                DataTable dt = ds.Tables[0];
                FillAccountDetails(dt);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        private void FillCashAcc()
        {
            try
            {
                string strCMD = "SELECT AM_ACCOUNT_ID,AM_ACCOUNT_NAME FROM FA_ACCOUNT_MASTER WHERE AM_ACCOUNT_TYPE_ID='CASH' AND AM_COMPANY_CODE='" + CommonData.CompanyCode + "'";
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
        private void GenerateVoucherId()
        {
            try
            {
                string strCMD = "SELECT ISNULL(MAX(VCO_VOUCHER_ID),0)+1 VoucherId FROM FA_VOUCHER_OTHERS WHERE vco_company_code='" + CommonData.CompanyCode +
                    "' AND VCO_BRANCH_CODE='" + CommonData.BranchCode + "' AND VCO_FIN_YEAR='" + CommonData.FinancialYear + "' ";

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
                string strCMD = "SELECT ISNULL(MAX(VCO_REF_NO),0)+1 VoucherId FROM FA_VOUCHER_OTHERS WHERE vco_company_code='" + CommonData.CompanyCode +
                    "' AND VCO_BRANCH_CODE='" + CommonData.BranchCode + "' AND VCO_FIN_YEAR='" + CommonData.FinancialYear + "' ";

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

        private void txtEcodeSearch_TextChanged(object sender, EventArgs e)
        {
            
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
                if (TYPE == "SIG")
                {
                    pbSig2.BackgroundImage = newImage;
                    this.pbSig2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                }
                if (TYPE == "FINGER")
                {
                    picFigerPrint.BackgroundImage = newImage;
                    this.picFigerPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnBillDetails_Click(object sender, EventArgs e)
        {
            if ((txtName.Text.Length > 0 && cmbEmpType.SelectedIndex==0)||txtOthersName.Text.Length>3)
            {
                AddAccountDetails obj = new AddAccountDetails();
                obj.objPaymentVoucher = this;
                obj.Show();
            }
            else
            {
                MessageBox.Show("Please Enter Valid Ecode OR Name");
                //btnSave.Enabled = false;
            }
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (txtApplNo.Text.Length > 0)
            {
                Verify frm = new Verify();
                frm.emp_appl_number = txtApplNo.Text;
                frm.dtVerif = dtVerify;
                
                frm.ShowDialog();
                if (frm.fpstatus == true)
                {
                    MessageBox.Show("Employee Verified");

                    if (flagUpdate == true)
                    {
                        if (DateTime.Now.AddDays(-3) <= Convert.ToDateTime(txtPunchDate.Text) || CommonData.LogUserId.ToUpper() == "ADMIN")
                        {
                            btnSave.Enabled = true;
                            picFigerPrint.BackgroundImage = frm.PictureBox1.Image;
                            txtPunchDate.Text = CommonData.CurrentDate;
                            txtPunchTime.Text = DateTime.Now.ToLongTimeString();
                        }
                        else
                            btnSave.Enabled = false;


                    }
                    else
                    {

                        picFigerPrint.BackgroundImage = frm.PictureBox1.Image;
                        txtPunchDate.Text = CommonData.CurrentDate;
                        txtPunchTime.Text = DateTime.Now.ToLongTimeString();

                        btnSave.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("FingerPrint Not Matched", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    btnSave.Enabled = false;

                    iNotMatchedCount++;
                    if (iNotMatchedCount > 3)
                    {
                        gpVerification.Enabled = true;
                        gpVerification.Visible = true;
                        rbWith.Checked = true;
                    }
                    else
                    {
                        gpVerification.Enabled = false;
                        gpVerification.Visible = false;

                    }
                }

            }
            else
            {
                MessageBox.Show("Please Enter Valid Ecode");
                btnSave.Enabled = false;
            }

        }

        private void gvBillDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (gvBillDetails.Rows[e.RowIndex].Cells["Edit"].Value.ToString().Trim() != "")
            //{
            //    if (Convert.ToBoolean(gvBillDetails.Rows[e.RowIndex].Cells["Edit"].Selected) == true)
            //    {
            //        int SlNo = Convert.ToInt32(gvBillDetails.Rows[e.RowIndex].Cells[gvBillDetails.Columns["SlNo"].Index].Value);
            //        DataRow[] dr = dtPayVoucher.Select("SL_NO=" + SlNo);
            //        AddAccountDetails obj= new AddAccountDetails(dr);
            //        obj.objPaymentVoucher = this;
            //        obj.ShowDialog();
            //    }
            //}
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
        //public void GetAccountDetials()
        //{
        //    int intRow = 1;
        //    gvBillDetails.Rows.Clear();
        //    for (int i = 0; i < dtPayVoucher.Rows.Count; i++)
        //    {
        //        DataGridViewRow tempRow = new DataGridViewRow();
        //        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
        //        cellSLNO.Value = intRow;
        //        dtPayVoucher.Rows[i]["SL_NO"] = intRow;
        //        tempRow.Cells.Add(cellSLNO);

        //        DataGridViewCell cellAccId = new DataGridViewTextBoxCell();
        //        cellAccId.Value = dtPayVoucher.Rows[i]["ACC_ID"];
        //        tempRow.Cells.Add(cellAccId);

        //        DataGridViewCell cellAccName = new DataGridViewTextBoxCell();
        //        cellAccName.Value = dtPayVoucher.Rows[i]["ACC_NAME"];
        //        tempRow.Cells.Add(cellAccName);

        //        //DataGridViewCell cellMajorCetreId = new DataGridViewTextBoxCell();
        //        //cellMajorCetreId.Value = dtPayVoucher.Rows[i]["MAJCOST_ID"];
        //        //tempRow.Cells.Add(cellMajorCetreId);

        //        //DataGridViewCell cellMajorCetreName = new DataGridViewTextBoxCell();
        //        //cellMajorCetreName.Value = dtPayVoucher.Rows[i]["MAJCOST_CENTRE_NAME"];
        //        //tempRow.Cells.Add(cellMajorCetreName);

        //        //DataGridViewCell cellEcode = new DataGridViewTextBoxCell();
        //        //cellEcode.Value = dtPayVoucher.Rows[i]["ECODE"];
        //        //tempRow.Cells.Add(cellEcode);


        //        //DataGridViewCell cellPayMode = new DataGridViewTextBoxCell();
        //        //cellPayMode.Value = dtPayVoucher.Rows[i]["PAYMENT_MODE"].ToString();
        //        //tempRow.Cells.Add(cellPayMode);

        //        //DataGridViewCell cellChqNO = new DataGridViewTextBoxCell();
        //        //cellChqNO.Value = dtPayVoucher.Rows[i]["CHQ_DD_NO"];
        //        //tempRow.Cells.Add(cellChqNO);

        //        //DataGridViewCell cellRemarks = new DataGridViewTextBoxCell();
        //        //cellRemarks.Value = dtPayVoucher.Rows[i]["REMARKS"];
        //        //tempRow.Cells.Add(cellRemarks);

        //        DataGridViewCell cellAmount = new DataGridViewTextBoxCell();
        //        cellAmount.Value = dtPayVoucher.Rows[i]["AMOUNT"].ToString();
        //        tempRow.Cells.Add(cellAmount);

        //        intRow = intRow + 1;
        //        gvBillDetails.Rows.Add(tempRow);
        //    }
        //    CalculateTotal();
        //}
        public void CalculateTotal()
        {
            double total = 0,totalCash=0,totalBills=0;
            for (int i = 0; i < gvBillDetails.Rows.Count;i++ )
            {
                if(Convert.ToDouble( gvBillDetails.Rows[i].Cells["AmtReceived"].Value.ToString())>0 &&
                    (Convert.ToString(gvBillDetails.Rows[i].Cells["AmtReceived"].Value) != "") )
                    total += Convert.ToDouble(gvBillDetails.Rows[i].Cells["AmtReceived"].Value);
                //gvBillDetails.Rows[i].Cells["Cash"].Value = gvBillDetails.Rows[i].Cells["AmtReceived"].Value;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                try
                {
                    if (SavePunchVoucherData() > 0)
                    {
                        if (SaveFaVoucherData() > 0)
                        {
                            txtEcodeSearch.Enabled = true;
                            MessageBox.Show("Data Saved Successfully ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DialogResult resultSms = MessageBox.Show("Do you want to Print this Payment Voucher", "SSERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (resultSms == DialogResult.Yes)
                            {
                                CommonData.ViewReport = "SSCRM_REP_CASH_VOUCHER_PRINT";
                                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, CommonData.FinancialYear, txtVouchRefNo.Text);
                                objReportview.Show();
                            }
                            flagUpdate = false;
                            btnClear_Click(null, null);
                            txtVouchRefNo.Focus();
                        }
                        else
                        {
                            string strSQL = " DELETE FROM PAYMENT_VOUCHER_PUNCHES WHERE PVP_COMP_CODE='" + CommonData.CompanyCode + "' and " +
                             " PVP_BRANCH_CODE='" + CommonData.BranchCode + "' and " +
                             " PVP_FIN_YEAR='" + CommonData.FinancialYear + "' and " +
                       //      " PVP_DOC_MONTH='" + CommonData.DocMonth + "' and " +
                             " PVP_VOUCHER_NO='" + txtVoucherId.Text + "'";
                            strSQL += " DELETE FROM FA_VOUCHER WHERE VC_COMPANY_CODE='" + CommonData.CompanyCode +
                              "' AND VC_BRANCH_CODE='" + CommonData.BranchCode +
                              "' AND VC_FIN_YEAR='" + CommonData.FinancialYear +
                             // "' AND VC_DOC_MONTH ='" + CommonData.DocMonth +
                              "' AND VC_DOC_TYPE='CP'" +
                              " AND VC_VOUCHER_ID='" + txtVoucherId.Text + "'";

                            strSQL += " DELETE from FA_VOUCHER_OTHERS" +
                                      " WHERE VCO_COMPANY_CODE='" + CommonData.CompanyCode +
                                      "' AND VCO_BRANCH_CODE='" + CommonData.BranchCode +
                                      "' AND VCO_VOUCHER_ID='" + txtVoucherId.Text +
                              //        "' AND VCO_DOC_MONTH ='" + CommonData.DocMonth +
                                      "' AND VCO_DOC_TYPE='CP'" +
                                      "  AND VCO_FIN_YEAR='" + CommonData.FinancialYear + "'";

                            objDB = new SQLDB();
                            objDB.ExecuteSaveData(strSQL);
                        }
                    }
                    else
                    {
                        string str = " DELETE FROM PAYMENT_VOUCHER_PUNCHES WHERE PVP_COMP_CODE='" + CommonData.CompanyCode + "' and " +
                        " PVP_BRANCH_CODE='" + CommonData.BranchCode + "' and " +
                        " PVP_FIN_YEAR='" + CommonData.FinancialYear + "' and " +
                     //   " PVP_DOC_MONTH='" + CommonData.DocMonth + "' and " +
                        " PVP_VOUCHER_NO='" + txtVoucherId.Text + "'";
                        objDB = new SQLDB();
                        objDB.ExecuteSaveData(str);
                    }
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
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

                strSQL += " DELETE FROM FA_VOUCHER WHERE VC_COMPANY_CODE='" + CommonData.CompanyCode +
                          "' AND VC_BRANCH_CODE='" + CommonData.BranchCode +
                          "' AND VC_FIN_YEAR='" + CommonData.FinancialYear +
                        //  "' AND VC_DOC_MONTH ='" + CommonData.DocMonth +
                          "' AND VC_DOC_TYPE='CP'" +
                          " AND VC_VOUCHER_ID='" + txtVoucherId.Text + "'";

                strSQL += " DELETE from FA_VOUCHER_OTHERS" +
                          " WHERE VCO_COMPANY_CODE='" + CommonData.CompanyCode +
                          "' AND VCO_BRANCH_CODE='" + CommonData.BranchCode +
                          "' AND VCO_VOUCHER_ID='" + txtVoucherId.Text +
                         // "' AND VCO_DOC_MONTH ='" + CommonData.DocMonth +
                          "' AND VCO_DOC_TYPE='CP'" +
                          "  AND VCO_FIN_YEAR='" + CommonData.FinancialYear + "'";
                strSQL += " INSERT INTO FA_VOUCHER_OTHERS(VCO_COMPANY_CODE" +
                                          ",VCO_BRANCH_CODE" +
                                          ",VCO_FIN_YEAR" +
                                          ",VCO_DOC_MONTH"+
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
                                          ",VCO_VERIFICATION" +
                                          ",VCO_NOT_VERIFY_REMARKS" +
                                          ",VCO_STATUS"+                                        
                                          ") VALUES('" + CommonData.CompanyCode +
                                          "','" + CommonData.BranchCode +
                                          "','" + CommonData.FinancialYear +
                                          "','" + CommonData.DocMonth +
                                          "','CP'" +
                                          ",'" + txtVoucherId.Text +
                                          "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                          "','" + txtNarration.Text.Trim().Replace("'", "") +
                                          "','" +
                                          "','" +
                                          "','" +
                                          "','" + txtVouchRefNo.Text.Trim().Replace("'", "") +
                                          "','" + CommonData.LogUserId +
                                          "',getdate(),";
                if (rbWithout.Checked == true || cmbEmpType.Text == "OTHERS")
                {
                    strSQL += "'BIOVERIFY NOT DONE','"+txtRemarks.Text+"',";
                }
                else
                {
                    strSQL += "'BIOVERIFY DONE','" + txtRemarks.Text + "',";
                }
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
                       ",VC_DOC_MONTH"+
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
                       "','CP'" +
                        ",'" + txtVoucherId.Text +
                        "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                        "','0" +
                        "','" + cmbCashAccount.SelectedValue.ToString() +
                        "','C" +
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
                        "','CP'" +
                        ",'N" +
                        "','N" +
                        "','" + CommonData.LogUserId +
                        "',GETDATE(),'" + txtTotalCash.Text + "','" + txtTotBills.Text ;
                if (cmbEmpType.Text == "EMPLOYEE")
                {
                    strSQL += "','EMPLOYEE')";
                }
                else
                {
                    strSQL += "','OTHERS')";
                }
                int j = 0;
                for (int i = 0; i < gvBillDetails.Rows.Count;i++ )
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
                              "','CP'" +
                               ",'" + txtVoucherId.Text +
                               "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                               "','" + (j) +
                               "','" + gvBillDetails.Rows[i].Cells["AccountId"].Value +
                               "','D" +
                               "','" + gvBillDetails.Rows[i].Cells["AmtReceived"].Value+"','',";
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
                        if (cmbEmpType.Text == "EMPLOYEE")
                        {
                            strSQL += "'" + txtEcodeSearch.Text + "','EMPLOYEE'";
                        }
                        else
                        {
                            strSQL += "'"+txtOthersName.Text+"','OTHERS'";
                        }
                strSQL +=  ",'" + cbPaymentMode.Text +
                           "','" + txtChqDDNo.Text +
                           "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                           "','" + 
                           "','" +
                           "','" + cmbCashAccount.SelectedValue +
                           "','" + txtTotalAmt.Text +
                           "','CP'" +
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

        private int SavePunchVoucherData()
        {
            int iRes = 0;
            try
            {
                
                string strEMPCode = "0";
                if (cmbEmpType.SelectedIndex == 0)
                {
                    strEMPCode = txtEcodeSearch.Text;
                }
                else
                {
                    strEMPCode = "0";
                }

                string strCMD = "";
                if (flagUpdate == false)
                {

                    GenerateVoucherId();
                    GenerateVoucherRefNo();

                    strCMD = " insert into PAYMENT_VOUCHER_PUNCHES(PVP_COMP_CODE,PVP_BRANCH_CODE,PVP_FIN_YEAR,PVP_DOC_MONTH,PVP_EORA_CODE,PVP_PUNCH_DATE,PVP_PUNCH_TIME,PVP_VOUCHER_NO" +
                                       ",PVP_REF_VOUCH_NO,PVP_VOUCHER_TYPE,PVP_VOUCHER_AMT,PVP_REMARKS,PVP_CREATED_BY,PVP_CREATED_DATE,PVP_TYPE,PVP_OTHERS_NAME)" +
                                       " values('" + CommonData.CompanyCode + "','" + CommonData.BranchCode + "','" + CommonData.FinancialYear + "','" + CommonData.DocMonth +
                                       "'," + strEMPCode + ",'" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") + "','" + txtPunchTime.Text +
                                       "','" + txtVoucherId.Text + "','" + txtVouchRefNo.Text.Trim().Replace("'", "") + "','CP','" + txtTotalAmt.Text.Trim() +
                                       "','" + txtNarration.Text.Trim().Replace("'", "") + "','" + CommonData.LogUserId + "',getdate(),'" + cmbEmpType.SelectedItem + "','" + txtOthersName.Text.ToUpper() + "')";
                }
                else
                {
                    strCMD = " UPDATE PAYMENT_VOUCHER_PUNCHES SET PVP_VOUCHER_STATUS='PAID'"+
                    ",PVP_VOUCHER_TYPE='CP',PVP_VOUCHER_AMT='" + txtTotalAmt.Text.Trim() + "',PVP_REMARKS='" + txtNarration.Text.Trim().Replace("'", "") + "',PVP_MODIFIED_BY='" + CommonData.LogUserId + "',PVP_MODIFIED_DATE=GETDATE()"+
                        " WHERE PVP_VOUCHER_NO='" + txtVoucherId.Text + "' and PVP_BRANCH_CODE= '" + CommonData.BranchCode + "' and PVP_FIN_YEAR='"+ CommonData.FinancialYear +"'";// and PVP_DOC_MONTH='" + CommonData.DocMonth + "'";
                }
                objDB = new SQLDB();
                iRes = objDB.ExecuteSaveData(strCMD);

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
            if (cmbEmpType.SelectedItem == "EMPLOYEE")
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
            //if(dtPayVoucher.Rows.Count==0)
            //{
            //    flag = false;
            //    MessageBox.Show("Enter Amount Details", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    //txtVouchRefNo.Focus();
            //    return flag;
            //}
            

            if (cbPaymentMode.SelectedIndex > 0 && txtChqDDNo.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Cheq/DD Ref No", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return flag;
            }
            //if(cmbEmpType.SelectedIndex>0 && txtOthersName.Text.Length==0)
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
            if ((txtTotalCash.Text.Length == 0 || txtTotalCash.Text == "0" || txtTotalCash.Text == "0.00") && txtTotBills.Text == "0")
            {
                flag = false;
                MessageBox.Show("Please Enter Cash", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return flag;
            }

            //if (Convert.ToInt32(txtTotalCash.Text) + Convert.ToInt32(txtTotBills.Text) == Convert.ToInt32(txtTotalCash.Text))
            //{
            //    flag = false;
            //    MessageBox.Show("Please Tally", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return flag;
            //}


            if (cmbCashAccount.SelectedItem == null)
            {
                flag = false;
                MessageBox.Show("Select Cash Accounts", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbCashAccount.Focus();
                return flag;
            }
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
                MessageBox.Show("Please Enter Narration", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return flag;
            }

            bool isTrue = false;
            if(gvBillDetails.Rows.Count>0)
            {
                for (int i = 0; i < gvBillDetails.Rows.Count;i++ )
                {
                    if (gvBillDetails.Rows[i].Cells["AccountId"].Value.ToString() == "L020902016" && gvBillDetails.Rows[i].Cells["AmtReceived"].Value.ToString()!="0")
                    {
                        isTrue = true;
                    }
                }
            }
            if(isTrue == false)
            if (Convert.ToDouble(txtTotalAmt.Text) >= 20000)
            {
                flag = false;
                MessageBox.Show("Amount Should be Less Than 20,000 Rs/-", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return flag;
            }
            if(rbWithout.Checked == true && txtRemarks.Text.Trim().Length<3)
            {
                flag = false;
                MessageBox.Show("Please Enter Remarks", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return flag;
            }

            return flag;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //txtStatus.Text = "";
            txtName.Text = "";
            txtEcodeSearch.Text = "";
            txtApplNo.Text = "";

         
            txtVouchRefNo.Text = "";
           
            txtPunchDate.Text = "";
            txtPunchTime.Text = "";
            txtChqDDNo.Text = "";

            picEmpPhoto.BackgroundImage = null;
            pbSig2.BackgroundImage = null;
            picFigerPrint.BackgroundImage = null;

            txtEcodeSearch.Enabled = true;
            txtOthersName.Enabled = true;
            cmbEmpType.Enabled = true;
            //dtPayVoucher.Rows.Clear();
            //gvBillDetails.Rows.Clear();
            txtTotalAmt.Text = "0";
            txtTotBills.Text = "0";
            txtTotalCash.Text = "0";
            chkCancelDc.Checked = false;

            btnVerify.Enabled = true;
            btnSave.Enabled = false;
            btnPrint.Enabled = false;
            //btnCancel.Enabled = false;
            GenerateVoucherId();
            GenerateVoucherRefNo();
            txtOthersName.Text = "";
            txtNarration.Text = "";
            cmbEmpType.SelectedIndex = 0;

            flagUpdate = false;

            objDB = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();


            param[0] = objDB.CreateParameter("@xCompCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
            ds = objDB.ExecuteDataSet("GetAccountsinSortOrder", CommandType.StoredProcedure, param);
            DataTable dt = ds.Tables[0];
            FillAccountDetails(dt);

            iNotMatchedCount = 0;
            gpVerification.Visible = false;
            rbWith.Checked = true;
            txtRemarks.Text = "";
           
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            CommonData.ViewReport = "SSCRM_REP_CASH_VOUCHER_PRINT";
            ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode,CommonData.BranchCode,CommonData.FinancialYear,txtVouchRefNo.Text);
            objReportview.Show();
        }

        private void txtVouchRefNo_KeyUp(object sender, KeyEventArgs e)
        {
            
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

                    DataGridViewCell cellAmount = new DataGridViewTextBoxCell();
                    cellAmount.Value = dt.Rows[i]["VC_AMOUNT"]; ;
                    tempRow.Cells.Add(cellAmount);

                    DataGridViewCell cellAmount1 = new DataGridViewTextBoxCell();
                    cellAmount1.Value = dt.Rows[i]["VC_AMOUNT"]; ;
                    tempRow.Cells.Add(cellAmount1);

                    DataGridViewCell cellAmount2 = new DataGridViewTextBoxCell();
                    cellAmount2.Value = dt.Rows[i]["VC_AMOUNT"]; ;
                    tempRow.Cells.Add(cellAmount2);

                    intRow = intRow + 1;
                    gvBillDetails.Rows.Add(tempRow);
                }
                CalculateTotal();

            
        }

        private void cbPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
             if (cbPaymentMode.SelectedItem == "CASH")
             {
                 txtChqDDNo.Enabled = false;
             }
             else
             {
                 txtChqDDNo.Enabled = true;
             }
        }

        private void cmbEmpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEmpType.SelectedItem == "EMPLOYEE")
            {
                txtOthersName.Visible = false;
                txtEcodeSearch.Visible = true;
                txtName.Visible = true;
                lblPunchDate.Visible = true;
                txtPunchDate.Visible = true;
                //lblTime.Visible = true;
                txtPunchTime.Visible = true;
                btnVerify.Visible = true;
                gpPhotos.Visible = true;
                btnSave.Enabled = false;
            }
            else
            {
                txtRemarks.Text = "OTHERS";

                txtEcodeSearch.Visible = false;
                txtName.Visible = false;
                lblPunchDate.Visible = false;
                txtPunchDate.Visible = false;
                //lblTime.Visible = false;
                txtPunchTime.Visible = false;
                btnVerify.Visible = false;
                gpPhotos.Visible = false;
                txtOthersName.Visible = true;

                btnSave.Enabled = true;
            }
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
                param[3] = objDB.CreateParameter("@Voucher_Type", DbType.String, "CP", ParameterDirection.Input);
                param[4] = objDB.CreateParameter("@Voucher_No", DbType.String, txtVouchRefNo.Text, ParameterDirection.Input);
                ds = objDB.ExecuteDataSet("GetVoucherDetails", CommandType.StoredProcedure, param);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {

                    txtEcodeSearch.Text = dt.Rows[0]["VC_SUB_COST_CENTRE_ID"].ToString();
                    txtVoucherId.Text = dt.Rows[0]["VoucherId"].ToString();
                    txtApplNo.Text = dt.Rows[0]["HAMH_APPL_NUMBER"].ToString();
                    txtName.Text = dt.Rows[0]["EmpName"].ToString();

                    flagUpdate = true;
                    txtEcodeSearch.Enabled = false;
                    txtOthersName.Enabled = false;
                    cmbEmpType.Enabled = false;

                    if (dt.Rows[0]["VCO_VERIFICATION"].ToString() == "BIOVERIFY DONE" || dt.Rows[0]["VCO_VERIFICATION"].ToString().Length==0)
                    {
                        rbWith.Checked = true;
                        txtRemarks.Text = dt.Rows[0]["VCO_NOT_VERIFY_REMARKS"].ToString();
                    }
                    else
                    {
                        rbWithout.Checked = true;
                        txtRemarks.Text = dt.Rows[0]["VCO_NOT_VERIFY_REMARKS"].ToString();
                    }


 //                   if (DateTime.Now.AddDays(-3) <= Convert.ToDateTime(dt.Rows[0]["VC_VOUCHER_DATE"]) || CommonData.LogUserId.ToUpper() == "ADMIN")
                    if ((DateTime.Now.AddDays(-3) <= Convert.ToDateTime(dt.Rows[0]["VC_VOUCHER_DATE"]) && cmbEmpType.Text =="OTHERS") || CommonData.LogUserId.ToUpper() == "ADMIN")
                    {
                        btnSave.Enabled = true;
                    }
                    else
                        btnSave.Enabled = false;


                    if (CommonData.LogUserId.ToUpper() == "ADMIN")
                    {
                        btnVerify.Enabled = false;
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        btnVerify.Enabled = true;
                        btnSave.Enabled = false;
                    }
                   
                    txtNarration.Text = dt.Rows[0]["Remarks"].ToString();

                   
                    if (dt.Rows[0]["Type"].ToString() == "EMPLOYEE")
                    {
                        cmbEmpType.SelectedIndex = 0;
                        if (dt.Rows[0]["HAMH_MY_PHOTO"].ToString().Length != 0)
                            GetImage((byte[])dt.Rows[0]["HAMH_MY_PHOTO"], "PHOTO");
                        else
                            picEmpPhoto.BackgroundImage = null;
                        if (ds.Tables[0].Rows[0]["FingerPrint"].ToString().Length != 0)
                        {
                            //picFigerPrint.BackgroundImage = null;
                            GetImage(((byte[])ds.Tables[0].Rows[0]["FingerPrint"]), "FINGER");
                            //lblMessage.Visible = false;
                        }
                        else
                        {
                            picFigerPrint.BackgroundImage = null;
                        }
                        DataSet dsSig = objDB.ExecuteDataSet("SELECT isnull(isnull(HAUM_SIG1,HAUM_SIG2),HAUM_SIG3) HAUM_SIG FROM HR_APPL_UAN_MAS WHERE HAUM_APPL_NO=" + txtApplNo.Text);
                        if (dsSig.Tables[0].Rows.Count > 0)
                        {
                            if (dsSig.Tables[0].Rows[0]["HAUM_SIG"].ToString().Length != 0)
                                GetImage((byte[])dsSig.Tables[0].Rows[0]["HAUM_SIG"], "SIG");
                            else
                                pbSig2.BackgroundImage = null;
                        }
                        else
                        {
                            pbSig2.BackgroundImage = null;
                        }
                        txtPunchDate.Text = dt.Rows[0]["PVP_PUNCH_DATE"].ToString();
                        txtPunchTime.Text = dt.Rows[0]["PVP_PUNCH_TIME"].ToString();
                    }
                    else
                    {
                        cmbEmpType.SelectedIndex = 1;
                        txtOthersName.Text = dt.Rows[0]["EmpName"].ToString();
                    }
                    if (dt.Rows[0]["VCO_STATUS"].ToString() == "CANCEL")
                    {
                        chkCancelDc.Checked = true;
                    }
                    else
                    {
                        chkCancelDc.Checked = false;
                    }
                    FillAccountDetails(dt);
                   // btnVerify.Enabled = false;
                    btnPrint.Enabled = true;
                    
                    if (CommonData.LogUserId.ToString().ToUpper() == "ADMIN")
                    {
                        btnDelete.Enabled = true;
                    }
                    txtTotBills.Text = dt.Rows[0]["VC_BILLS_AMT"].ToString();
                    txtTotalCash.Text = dt.Rows[0]["VC_CASH_AMT"].ToString();

                    gpVerification.Visible = true;
                    gpVerification.Enabled = false;


                   

                    //btnCancel.Enabled = true;

                    //if(DateTime.Now.AddDays(-3) <=  Convert.ToDateTime( txtPunchDate.Text) || CommonData.LogUserId.ToUpper()=="ADMIN")
                    //    btnSave.Enabled = true;
                    //else
                    //    btnSave.Enabled = false;
                }
                else
                {
                    flagUpdate = false;
                    txtEcodeSearch.Enabled = true;
                    txtOthersName.Enabled = true;
                    cmbEmpType.Enabled = true;
                    btnVerify.Enabled = true;
                    btnPrint.Enabled = false;
                    txtEcodeSearch.Text = "";
                    picFigerPrint.BackgroundImage = null;
                    //ClearAllAmount();
                    //gvBillDetails.Rows.Clear();
                   
                    txtPunchDate.Text = "";
                    txtPunchTime.Text = "";
                    txtTotalAmt.Text = "0";
                    txtTotBills.Text = "0";
                    txtTotalCash.Text = "0";
                    txtOthersName.Text = "";
                    cmbEmpType.SelectedIndex = 0;
                    GenerateVoucherId();
                    txtNarration.Text = "";
                    txtName.Text = "";
                    txtApplNo.Text = "";
                    picEmpPhoto.BackgroundImage = null;
                    pbSig2.BackgroundImage = null;



                    objDB = new SQLDB();
                    param = new SqlParameter[1];
                     ds = new DataSet();


                    param[0] = objDB.CreateParameter("@xCompCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                    ds = objDB.ExecuteDataSet("GetAccountsinSortOrder", CommandType.StoredProcedure, param);
                    DataTable dt1 = ds.Tables[0];
                    FillAccountDetails(dt1);

                    iNotMatchedCount = 0;

                    gpVerification.Enabled = false;
                    gpVerification.Visible = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ClearAllAmount()
        {
            for (int i = 0; i < gvBillDetails.Rows.Count;i++ )
            {
                gvBillDetails.Rows[i].Cells["AmtReceived"].Value = "0";
            }
            //for (int i = 0; i < dtPayVoucher.Rows.Count;i++ )
            //{
            //    dtPayVoucher.Rows[i]["AMOUNT"] = 0;
            //}
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
                                    " END AS STATUS,hafp_finger_fp2,HAFP_FINGER_FP1,HAFP_FINGER_FP4,DESIG_SHORT_NAME desig " +
                                    " FROM HR_APPL_MASTER_HEAD left join HR_APPL_FINGER_PRINTS on HAMH_APPL_NUMBER=hafp_APPL_NUMBER "+
                                   // " LEFT JOIN DESIG_MAS ON DESG_ID=desig_code "+
                    " inner join eora_master on ecode=HAMH_EORA_CODE LEFT JOIN DESIG_MAS ON DESG_ID=desig_code WHERE HAMH_EORA_CODE=" + txtEcodeSearch.Text + " ";
                    DataTable dt = objDB.ExecuteDataSet(strSQL).Tables[0];
                    dtVerify = dt;
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
                        if (dt.Rows[0]["hafp_finger_fp2"].ToString().Length == 0)
                        {
                            MessageBox.Show("Employee Enrollment Not Done \n Please Enroll First", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            btnVerify.Enabled = false;
                        }
                        else
                        {
                            btnVerify.Enabled = true;
                        }
                        DataSet dsSig = objDB.ExecuteDataSet("SELECT isnull(isnull(HAUM_SIG1,HAUM_SIG2),HAUM_SIG3) HAUM_SIG FROM HR_APPL_UAN_MAS WHERE HAUM_APPL_NO=" + txtApplNo.Text);
                        if (dsSig.Tables[0].Rows.Count > 0)
                        {
                            if (dsSig.Tables[0].Rows[0]["HAUM_SIG"].ToString().Length != 0)
                                GetImage((byte[])dsSig.Tables[0].Rows[0]["HAUM_SIG"], "SIG");
                            else
                                pbSig2.BackgroundImage = null;
                        }
                        GenerateNarration();
                    }
                    else
                    {
                        btnVerify.Enabled = true;
                        txtApplNo.Text = "";
                        txtName.Text = "";
                        picEmpPhoto.BackgroundImage = null;
                        pbSig2.BackgroundImage = null;
                        picFigerPrint.BackgroundImage = null;
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
                picFigerPrint.BackgroundImage = null;
                pbSig2.BackgroundImage = null;
                //txtStatus.Text = "";
                btnVerify.Enabled = false;
            }
        }

        private void btnAddMultiple_Click(object sender, EventArgs e)
        {
            //AccountsSearch ASearch = new AccountsSearch();
            //ASearch.objFrmPaymentVoucher = this;
            //ASearch.ShowDialog();
        }

        private void btnClearProd_Click(object sender, EventArgs e)
        {
            gvBillDetails.Rows.Clear();
            //dtPayVoucher.Rows.Clear();
            CalculateTotal();
            txtNarration.Text = "";
        }
        public void GenerateNarration()
        {
            string strNarration = "BEING CASH ";
            string str = "";
            //for (int i = 0; i < gvBillDetails.Rows.Count;i++ )
            //{
            //    if ( Convert.ToDouble( gvBillDetails.Rows[i].Cells["AmtReceived"].Value.ToString())>0 &&
            //        (Convert.ToString(gvBillDetails.Rows[i].Cells["AmtReceived"].Value) != "") )
            //    {

            //         str += gvBillDetails.Rows[i].Cells["AccName"].Value.ToString().Split('(')[0] + ",";
            //    }
            //}
            //if (str.Length > 0)
            //   str= str.Remove(str.Length - 1);

            if(cmbEmpType.Text=="EMPLOYEE")
                strNarration += str + " PAID TO " + txtName.Text + "(" + txtDesigName.Text + ") FOR THE MONTH OF " + txtDocMonth.Text + " ";
            else
                strNarration += str + " PAID TO " + txtOthersName.Text + " FOR THE MONTH OF " + txtDocMonth.Text + " ";
            txtNarration.Text = strNarration;


            //string strNarration = "BEING CASH PAID TOWARDS ";
            //string str = "";
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

            //strNarration += str + " PAID TO " + txtName.Text + "(" + txtDesigName.Text + ") FOR THE MONTH OF " + txtDocMonth.Text + " ";
            //txtNarration.Text = strNarration;
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
                   // gvBillDetails.Rows[e.RowIndex].Cells["AmtReceived"].Value = Convert.ToInt32(gvBillDetails.Rows[e.RowIndex].Cells["Cash"].Value) +
                   //Convert.ToInt32(gvBillDetails.Rows[e.RowIndex].Cells["Bills"].Value);
                    GenerateNarration();
                    CalculateTotal();
                }
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
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 ) )
            {
                e.Handled = true;
                return;
            }

             //checks to make sure only 1 decimal is allowed
            if (e.KeyChar == 46 )
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }

        }

        private void txtVouchRefNo_KeyPress(object sender, KeyPressEventArgs e)
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
            if (txtTotalCash.Text=="")
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Do you want delete ?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgResult == DialogResult.Yes)
            {
                string strTYPE = "CP";
                //if (cbAcctType.SelectedItem == "CASH")
                //{
                //    strTYPE = "CR";
                //}
                //else
                //{
                //    strTYPE = "BR";
                //}

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


                strSQL += " DELETE FROM PAYMENT_VOUCHER_PUNCHES where PVP_COMP_CODE='" + CommonData.CompanyCode
                         + "' AND PVP_BRANCH_CODE='" + CommonData.BranchCode +
                         "' AND PVP_FIN_YEAR='" + CommonData.FinancialYear +
                         "' AND PVP_VOUCHER_NO= '"+ txtVoucherId.Text +
                         "' AND PVP_VOUCHER_TYPE='"+strTYPE+"'";


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

        private void rbWith_CheckedChanged(object sender, EventArgs e)
        {
            txtRemarks.Enabled = false;
            btnVerify.Enabled = true;
            btnSave.Enabled = false;
        }

        private void rbWithout_CheckedChanged(object sender, EventArgs e)
        {
            txtRemarks.Enabled = true;
            btnVerify.Enabled = false;
            btnSave.Enabled = true;
        }

        private void txtOthersName_Validated(object sender, EventArgs e)
        {
            GenerateNarration();
        }


        //private void button1_Click(object sender, EventArgs e)
        //{
        //    CheckData();
        //}

       
      

      
    }
}
