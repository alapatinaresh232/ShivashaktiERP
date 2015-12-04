using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using System.Data.SqlClient;
using SSCRM.App_Code;
using System.IO;
using ZkFingerDemo;

namespace SSCRM
{
    public partial class PaymentVoucher : Form
    {
        SQLDB objDB = null;
        bool flagUpdate = false;
        public PaymentVoucher()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void PaymentVoucher_Load(object sender, EventArgs e)
        {
            txtBranch.Text = CommonData.BranchName;
            txtDocMonth.Text = CommonData.DocMonth;
            cbPaymentMode.SelectedIndex = 0;
            //FillMachineDetails();
            //FillPunchDetails();
            FillCashAcc();
            DataTable dtAccMas = objDB.ExecuteDataSet(" SELECT AM_ACCOUNT_ID,AM_ACCOUNT_NAME AccName FROM FA_ACCOUNT_MASTER  WHERE AM_COMPANY_CODE='"+CommonData.CompanyCode+"' AND AM_ACCOUNT_GROUP_ID IS NULL ").Tables[0];
        
            UtilityLibrary.AutoCompleteComboBox(cmbAccounts, dtAccMas, "AM_ACCOUNT_ID", "AccName");
            if (dtAccMas.Rows.Count > 0)
            {
                DataRow dr = dtAccMas.NewRow();
              

                cmbAccounts.DataSource = dtAccMas;
                cmbAccounts.DisplayMember = "AccName";
                cmbAccounts.ValueMember = "AM_ACCOUNT_ID";
            }
           

            GenerateVoucherId();
        }
        private void FillMajorCostDetails()
        {
            DataTable dtMajorCost = objDB.ExecuteDataSet(" SELECT MCC_MAJOR_COST_CENTRE_ID,MCC_MAJOR_COST_CENTRE_NAME+' ('+MCC_MAJOR_COST_CENTRE_ID+')' MajorCost FROM FA_MAJOR_COST_CENTRE " +
                                                " WHERE  MCC_COMPANY_CODE='" + CommonData.CompanyCode + "'").Tables[0];
            UtilityLibrary.AutoCompleteComboBox(cmbMajorCost, dtMajorCost, "MCC_MAJOR_COST_CENTRE_ID", "MajorCost");
            if (dtMajorCost.Rows.Count > 0)
            {
                DataRow dr = dtMajorCost.NewRow();


                cmbMajorCost.DataSource = dtMajorCost;
                cmbMajorCost.DisplayMember = "MajorCost";
                cmbMajorCost.ValueMember = "MCC_MAJOR_COST_CENTRE_ID";
            }
        }
        private void GenerateVoucherId()
        {
            try
            {
                string strCMD = "SELECT ISNULL(MAX(VCO_VOUCHER_ID),0)+1 VoucherId FROM FA_VOUCHER_OTHERS WHERE vco_company_code='" + CommonData.CompanyCode +
                    "' AND VCO_BRANCH_CODE='"+CommonData.BranchCode+"' AND VCO_FIN_YEAR='" + CommonData.FinancialYear + "'";

                objDB = new SQLDB();
                DataTable DT = objDB.ExecuteDataSet(strCMD).Tables[0];
                if(DT!=null && DT.Rows.Count>0)
                {
                    txtVoucherId.Text = DT.Rows[0][0].ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void FillCashAcc()
        {
            try
            {
                string strCMD = "SELECT AM_ACCOUNT_ID,AM_ACCOUNT_NAME FROM FA_ACCOUNT_MASTER WHERE AM_ACCOUNT_TYPE_ID='CASH' AND AM_COMPANY_CODE='"+CommonData.CompanyCode+"'";
                objDB = new SQLDB();
                DataTable dt = objDB.ExecuteDataSet(strCMD).Tables[0];
                if(dt.Rows.Count>0)
                {
                    cmbCashAccount.DataSource = dt;
                    cmbCashAccount.ValueMember = "AM_ACCOUNT_ID";
                    cmbCashAccount.DisplayMember = "AM_ACCOUNT_NAME";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void FillPunchDetails()
        {
            DataSet ds = new DataSet();
            DataTable dt=new DataTable();
            objDB = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            try
            {

                //param[0] = objDB.CreateParameter("@xMachineId", DbType.String, txtMachineId.Text, ParameterDirection.Input);
                ds = objDB.ExecuteDataSet("Get_Punch_Details", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                
            }
            dt=ds.Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                //txtPunchID.Text = dt.Rows[0]["PVP_ID"].ToString();
                txtPunchDate.Text = Convert.ToDateTime(dt.Rows[0]["PVP_PUNCH_DATE"].ToString()).ToString("dd-MMM-yyyy");
                txtPunchTime.Text = dt.Rows[0]["PVP_PUNCH_TIME"].ToString().Substring(0, dt.Rows[0]["PVP_PUNCH_TIME"].ToString().Length - 8);
                txtEcodeSearch.Text = dt.Rows[0]["MemberNames"].ToString();
                txtApplNo.Text = dt.Rows[0]["PVP_EORA_CODE"].ToString();//
                txtApplNo.Tag = dt.Rows[0]["MEMBER_NAME"].ToString();
                //txtName.Text = dt.Rows[0]["member_name"].ToString();
                //txtDept.Text = dt.Rows[0]["dept_name"].ToString();
                //txtDesig.Text = dt.Rows[0]["desig_name"].ToString();
                objDB = new SQLDB();

                DataSet dsPhoto = objDB.ExecuteDataSet("SELECT HAPS_PHOTO_SIG FROM HR_APPL_PHOTO_SIG WHERE HAPS_EORA_CODE = " + dt.Rows[0]["PVP_EORA_CODE"]);

                if (dsPhoto.Tables[0].Rows.Count > 0)
                    GetImage((byte[])dsPhoto.Tables[0].Rows[0]["HAPS_PHOTO_SIG"], "PHOTO");
                else
                    picEmpPhoto.BackgroundImage = null;

                DataSet dsSig = objDB.ExecuteDataSet("SELECT isnull(isnull(HAUM_SIG1,HAUM_SIG2),HAUM_SIG3) HAUM_SIG FROM HR_APPL_UAN_MAS WHERE HAUM_EORA_CODE=" + dt.Rows[0]["PVP_EORA_CODE"]);
                if (dsSig.Tables[0].Rows[0]["HAUM_SIG"].ToString().Length != 0)
                    GetImage((byte[])dsSig.Tables[0].Rows[0]["HAUM_SIG"], "SIG");
                else
                    pbSig2.BackgroundImage = null;


            }
            else
            {
                txtPunchDate.Text = "";
                //txtPunchID.Text = "";
                //txtMachineId.Text = "";
                txtPunchTime.Text = "";
                txtApplNo.Text = "";
                txtApplNo.Tag = "";
                picEmpPhoto.BackgroundImage = null;
                pbSig2.BackgroundImage = null;
            }
        }
        public void GetImage(byte[] imageData,string TYPE)
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
                if(TYPE=="SIG")
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
        private void FillMachineDetails()
        {
            try
            {
                string strCMD = "SELECT FAMR_ASSET_TAG_NO FROM FIXED_ASSETS_MOVEMENT_REG WHERE FAMR_TO_BRANCH_CODE='"+CommonData.BranchCode+"' ";
                objDB = new SQLDB();
                 DataTable dt = objDB.ExecuteDataSet(strCMD).Tables[0];
                if(dt!=null && dt.Rows.Count>0)
                {
                    //txtMachineId.Text = dt.Rows[0][0].ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(CheckData())
            {
            if(SavePunchVoucherData()>0)
            {
                if(SaveFaVoucherData()>0)
                {
                    MessageBox.Show("Data Saved Successfully ", "SDMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClear_Click(null,null);
                    txtVouchRefNo.Focus();
                }
            }

            }

        }

        private int SavePunchVoucherData()
        {
            int iRes = 0;
            try
            {
                GenerateVoucherId();
                string strCMD = " insert into PAYMENT_VOUCHER_PUNCHES(PVP_COMP_CODE,PVP_BRANCH_CODE,PVP_FIN_YEAR,PVP_DOC_MONTH,PVP_EORA_CODE,PVP_PUNCH_DATE,PVP_PUNCH_TIME,PVP_VOUCHER_NO" +
                                    ",PVP_REF_VOUCH_NO,PVP_VOUCHER_TYPE,PVP_VOUCHER_AMT,PVP_REMARKS,PVP_CREATED_BY,PVP_CREATED_DATE)" +
                                    " values('"+CommonData.CompanyCode+"','"+CommonData.BranchCode+"','"+CommonData.FinancialYear+"','"+CommonData.DocMonth+
                                    "',"+txtEcodeSearch.Text+",'"+Convert.ToDateTime(txtPunchDate.Text).ToString("dd/MMM/yyyy")+"','"+txtPunchTime.Text+
                                    "','" + txtVoucherId.Text + "','" + txtVouchRefNo.Text.Trim().Replace("'", "") + "','CP','" + txtReceivedAmt.Text.Trim() +
                                    "','" + txtRemarks.Text.Trim().Replace("'", "") + "','" + CommonData.LogUserId + "',getdate())";
                //string  strCMD = " UPDATE PAYMENT_VOUCHER_PUNCHES SET PVP_VOUCHER_STATUS='PAID',PVP_VOUCHER_NO='"+txtVoucherId.Text+"',PVP_REF_VOUCH_NO='"+txtVouchRefNo.Text.Trim().Replace("'","");
                //                "',PVP_VOUCHER_TYPE='CP',PVP_VOUCHER_AMT='" + txtReceivedAmt.Text.Trim() + "',PVP_REMARKS='" + txtRemarks.Text.Trim().Replace("'", "") + "',PVP_MODIFIED_BY='" + CommonData.LogUserId + "',PVP_MODIFIED_DATE=GETDATE()" +
                //                " WHERE PVP_ID='"+txtPunchID.Text+"' ";
                objDB=new SQLDB();
                iRes = objDB.ExecuteSaveData(strCMD);
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iRes;
        }
        private int SaveFaVoucherData()
        {
            string strSQL="";
            int iRes = 0;
            string ShortName = "";
            if(txtName.Text.Length>15)
            {
                ShortName = ShortName.Substring(0, 15);

            }
            try
            {
                strSQL = "INSERT INTO FA_COST_CENTRE(CC_COMPANY_CODE,CC_COST_CENTRE_ID,CC_COST_CENTRE_NAME,CC_MAJOR_COST_CENTRE_ID,"+
                    " CC_TYPE,CC_CREATED_BY,CC_CREATED_DATE)SELECT '" + CommonData.CompanyCode + "','" + txtEcodeSearch.Text + "','" + txtName.Text + "','" + cmbMajorCost.SelectedValue + "','C','" + CommonData.LogUserId + "',GETDATE() " +
                    " WHERE NOT EXISTS(SELECT * FROM FA_COST_CENTRE WHERE CC_COMPANY_CODE='" + CommonData.CompanyCode + "' AND CC_COST_CENTRE_ID='" + txtEcodeSearch.Text + "' AND CC_MAJOR_COST_CENTRE_ID='" + cmbMajorCost.SelectedValue + "')";

                strSQL +=  " DELETE FROM FA_VOUCHER WHERE VC_COMPANY_CODE='" + CommonData.CompanyCode +
                          "' AND VC_BRANCH_CODE='" + CommonData.BranchCode +
                          "' AND VC_FIN_YEAR='" + CommonData.FinancialYear +
                          "' AND VC_DOC_TYPE='CP'" +
                          " AND VC_VOUCHER_ID='" + txtVoucherId.Text + "'";

                strSQL += " DELETE from FA_VOUCHER_OTHERS" +
                          " WHERE VCO_COMPANY_CODE='" + CommonData.CompanyCode +
                          "' AND VCO_BRANCH_CODE='" + CommonData.BranchCode +
                          "' AND VCO_VOUCHER_ID='" + txtVoucherId.Text +
                          "'  AND VCO_FIN_YEAR='" + CommonData.FinancialYear + "'";
                strSQL += " INSERT INTO FA_VOUCHER_OTHERS(VCO_COMPANY_CODE" +
                                          ",VCO_BRANCH_CODE" +
                                          ",VCO_FIN_YEAR" +
                                          ",VCO_DOC_TYPE" +
                                          ",VCO_VOUCHER_ID" +
                                          ",VCO_VOUCHER_DATE" +
                                          ",VCO_NARRATION_1" +
                                          ",VCO_NARRATION_2" +
                                          ",VCO_EFFECT_NAME" +
                                          ",VCO_VOUCHER_NO" +
                                          ",VCO_REF_NO "+
                                          ",VCO_CREATED_BY" +
                                          ",VCO_CREATED_DATE" +
                                          ") VALUES('" + CommonData.CompanyCode +
                                          "','" + CommonData.BranchCode +
                                          "','" + CommonData.FinancialYear +
                                          "','CP'" +
                                          ",'" + txtVoucherId.Text +
                                          "','" + Convert.ToDateTime(txtPunchDate.Text).ToString("dd/MMM/yyyy") +
                                          "','" + txtRemarks.Text.Trim().Replace("'", "") +
                                          "','" + 
                                          "','" + 
                                          "','" +
                                          "','" + txtVouchRefNo.Text.Trim().Replace("'", "") +
                                          "','" + CommonData.LogUserId +
                                          "',getdate())";

                strSQL += " INSERT INTO FA_VOUCHER( VC_COMPANY_CODE" +
                       ",VC_BRANCH_CODE" +
                       ",VC_FIN_YEAR" +
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
                       "','CP'" +
                        ",'" + txtVoucherId.Text +
                        "','" + Convert.ToDateTime(txtPunchDate.Text).ToString("dd/MMM/yyyy") +
                        "','0"+
                        "','" + cmbCashAccount.SelectedValue.ToString() +
                        "','C"+
                        "','" + txtReceivedAmt.Text +
                        "','" + 
                        "','" + 
                        "','" + cbPaymentMode.SelectedItem.ToString()+
                        "','" + 
                        "','" + Convert.ToDateTime(txtPunchDate.Text).ToString("dd/MMM/yyyy") +
                        "','" + txtRemarks.Text.Trim().Replace("'", "") +
                        "','"+
                        "','" +
                        "','" + txtReceivedAmt.Text +
                        "','CP'" +
                        ",'N" +
                        "','N" +
                        "','" + CommonData.LogUserId +
                        "',GETDATE())";
                strSQL += " INSERT INTO FA_VOUCHER( VC_COMPANY_CODE" +
                      ",VC_BRANCH_CODE" +
                      ",VC_FIN_YEAR" +
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
                      "','CP'" +
                       ",'" + txtVoucherId.Text +
                       "','" + Convert.ToDateTime(txtPunchDate.Text).ToString("dd/MMM/yyyy") +
                       "','1" +
                       "','" + cmbAccounts.SelectedValue.ToString()+
                       "','D" +
                       "','" + txtReceivedAmt.Text +
                       "','" + cmbMajorCost.SelectedValue.ToString() +
                       "','" + txtEcodeSearch.Text +
                       "','" + cbPaymentMode.SelectedItem.ToString() +
                       "','" + txtChqDDNo.Text +
                       "','" + Convert.ToDateTime(txtPunchDate.Text).ToString("dd/MMM/yyyy") +
                       "','" + txtRemarks.Text.Trim().Replace("'", "") +
                       "','" +
                       "','" + cmbCashAccount.SelectedValue +
                       "','" + txtReceivedAmt.Text +
                       "','CP'" +
                       ",'N" +
                       "','N" +
                       "','" + CommonData.LogUserId +
                       "',GETDATE())";


                objDB = new SQLDB();
                iRes = objDB.ExecuteSaveData(strSQL);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iRes;
        }
        private bool CheckData()
        {
            bool flag = true;
            if(txtVouchRefNo.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Enter Voucher Referenc No", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtVouchRefNo.Focus();
                return flag;
            }
            //if(cmbAccounts.SelectedText == null)
            //{
            //    flag = false;
            //    MessageBox.Show("Select Accounts", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    cmbAccounts.Focus();
            //    return flag;
            //}
            if (cmbMajorCost.SelectedValue == null)
            {
                flag = false;
                MessageBox.Show("MajorCostCentre is not Exist Please create it in MajorCostCentre Master.", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbMajorCost.Focus();
                return flag;
            }
            if (cmbAccounts.SelectedValue == null)
            {
                flag = false;
                MessageBox.Show("Account is not Exist Please Create it in ChartOfAccounts Master.", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbAccounts.Focus();
                return flag;
            }
            if(txtReceivedAmt.Text.Length==0)
            {
                flag = false;
                MessageBox.Show("Enter Amount", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtReceivedAmt.Focus();
                return flag;
            }
            if (txtRemarks.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Enter Remarks", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtRemarks.Focus();
                return flag;
            }
            //if (txtPunchID.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Punch For Payment", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return flag;
            //}
            

            return flag;
        }

        private void txtReceivedAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false)
                e.Handled = true;
            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
           
            flagUpdate = false;
            txtVouchRefNo.Text = "";
            GenerateVoucherId();
            //FillPunchDetails();
            txtChqDDNo.Text = "";
            txtReceivedAmt.Text = "";
            cmbAccounts.SelectedText = "";
            cmbMajorCost.SelectedText = "";
            txtRemarks.Text = "";


            //txtVouchRefNo.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
             DialogResult result = MessageBox.Show("Do you want to Cancel this Punch ?",
                                               "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
             if (result == DialogResult.Yes)
             {
                 try
                 {
                     //string strCMD = " UPDATE PAYMENT_VOUCHER_PUNCHES SET PVP_VOUCHER_STATUS='CANCEL'"+
                     //                " WHERE PVP_ID='" + txtPunchID.Text + "' ";
                     //objDB = new SQLDB();
                     //objDB.ExecuteSaveData(strCMD);
                     //flagUpdate = false;
                     //btnRefresh_Click(null, null);
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.ToString());
                 }
             }
        }

        private void txtVouchRefNo_Validated(object sender, EventArgs e)
        {
            try
            {
                string strCMD = "select pvp_voucher_no from PAYMENT_VOUCHER_PUNCHES where PVP_COMP_CODE='" + CommonData.CompanyCode + "' and PVP_BRANCH_CODE="+
                                "'" + CommonData.BranchCode + "' and PVP_FIN_YEAR='" + CommonData.FinancialYear + "' and PVP_REF_VOUCH_NO='"+txtVouchRefNo.Text+"' ";
                objDB = new SQLDB();
                DataTable dt = objDB.ExecuteDataSet(strCMD).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtVoucherId.Text = dt.Rows[0]["pvp_voucher_no"].ToString();
                }
                else
                {
                    txtName.Text = "";
                    txtEcodeSearch.Text = "";
                    txtApplNo.Text = "";
                    txtChqDDNo.Text = "";
                    txtReceivedAmt.Text = "";
                    txtRemarks.Text = "";
                    txtPunchDate.Text = "";
                    txtPunchTime.Text = "";
                    picEmpPhoto.BackgroundImage = null;
                    pbSig2.BackgroundImage = null;
                    picFigerPrint.BackgroundImage = null;
                    btnSave.Enabled = false;
                    GenerateVoucherId();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtVoucherId_TextChanged(object sender, EventArgs e)
        {
            FillPaymentVoucherDetails();
        }

        private void FillPaymentVoucherDetails()
        {
             DataSet ds = new DataSet();
            DataTable dt=new DataTable();
            objDB = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            try
            {

                param[0] = objDB.CreateParameter("@xCompanyCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objDB.CreateParameter("@xBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objDB.CreateParameter("@xFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objDB.CreateParameter("@xVoucherId", DbType.String, txtVoucherId.Text, ParameterDirection.Input);
                ds = objDB.ExecuteDataSet("GetPaymentVoucherDetails", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                
            }
            dt=ds.Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                flagUpdate = true;
                btnSave.Enabled = false;
                //btnCancel.Enabled = false;
                txtRemarks.Text = dt.Rows[0]["PVP_REMARKS"].ToString();
                txtReceivedAmt.Text = dt.Rows[0]["PVP_VOUCHER_AMT"].ToString();
                cbPaymentMode.SelectedItem = dt.Rows[0]["VC_PAYMENT_MODE"].ToString();
                txtChqDDNo.Text = dt.Rows[0]["VC_CHEQUE_NO"].ToString();
                cmbAccounts.SelectedValue = dt.Rows[0]["VC_ACCOUNT_ID"].ToString();
                cmbCashAccount.SelectedValue = dt.Rows[0]["VC_CASH_BANK_ID"].ToString();
                cmbMajorCost.SelectedValue = dt.Rows[0]["VC_MAIN_COST_CENTRE_ID"].ToString();
                //txtApplNo.Text = dt.Rows[0]["PVP_EORA_CODE"].ToString();
                txtEcodeSearch.Text = dt.Rows[0]["MemberNames"].ToString();
                //txtMachineId.Text = dt.Rows[0]["PVP_MACHINE_ID"].ToString();
                //txtPunchID.Text = dt.Rows[0]["PVP_ID"].ToString();
                txtPunchDate.Text = Convert.ToDateTime(dt.Rows[0]["PVP_PUNCH_DATE"].ToString()).ToString("dd-MMM-yyyy"); ;
                txtPunchTime.Text = Convert.ToDateTime( dt.Rows[0]["PVP_PUNCH_TIME"].ToString()).ToShortTimeString();


                DataSet dsPhoto = objDB.ExecuteDataSet("SELECT HAMH_MY_PHOTO from HR_APPL_MASTER_HEAD WHERE HAMH_APPL_NUMBER= " + dt.Rows[0]["HAMH_APPL_NUMBER"] +
                                        " SELECT hafp_finger_fp2 FROM HR_APPL_FINGER_PRINTS WHERE hafp_appl_number= " + dt.Rows[0]["HAMH_APPL_NUMBER"]);

                if (dsPhoto.Tables[0].Rows.Count > 0)
                {
                    if (dsPhoto.Tables[0].Rows[0]["HAMH_MY_PHOTO"].ToString().Length != 0)
                        GetImage((byte[])dsPhoto.Tables[0].Rows[0]["HAMH_MY_PHOTO"], "PHOTO");
                    else
                        picEmpPhoto.BackgroundImage = null;
                }

                if (dsPhoto.Tables[1].Rows.Count > 0)
                {
                    if (dsPhoto.Tables[1].Rows[0]["hafp_finger_fp2"].ToString().Length != 0)
                        GetImage((byte[])dsPhoto.Tables[1].Rows[0]["hafp_finger_fp2"], "FINGER");
                    else
                        picFigerPrint.BackgroundImage = null;
                }

                DataSet dsSig = objDB.ExecuteDataSet("SELECT isnull(isnull(HAUM_SIG1,HAUM_SIG2),HAUM_SIG3) HAUM_SIG FROM HR_APPL_UAN_MAS WHERE HAUM_EORA_CODE=" + dt.Rows[0]["PVP_EORA_CODE"]);
                if (dsSig.Tables[0].Rows.Count>0)
                {
                if (dsSig.Tables[0].Rows[0]["HAUM_SIG"].ToString().Length != 0)
                    GetImage((byte[])dsSig.Tables[0].Rows[0]["HAUM_SIG"], "SIG");
                else
                    pbSig2.BackgroundImage = null;
                }

            }
        }

        private void cmbAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbAccounts.SelectedIndex > -1)
                {
                    string strCMD = "SELECT AM_COSTCENTRE_FLAG FROM FA_ACCOUNT_MASTER WHERE AM_ACCOUNT_ID='" + cmbAccounts.SelectedValue + "' and am_company_code='"+CommonData.CompanyCode+"'";
                    objDB = new SQLDB();
                    DataTable dt = objDB.ExecuteDataSet(strCMD).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["AM_COSTCENTRE_FLAG"].ToString().Trim() == "Y")
                        {
                            cmbMajorCost.Enabled = true;
                            //txtCostCentre.Enabled = true;
                            FillMajorCostDetails();
                        }
                        else
                        {
                            cmbMajorCost.Enabled = false;
                            cmbMajorCost.DataSource = null;
                        }
                    }
                    else
                    {
                        cmbMajorCost.Enabled = false;

                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cmbMajorCost_Validated(object sender, EventArgs e)
        {
            string str="";
            //str = cmbMajorCost.SelectedValue.ToString();
            if (cmbMajorCost.SelectedValue == null)
            {
                MessageBox.Show("Major Cost Centre is not Exist Please Create it in MajorCostCentre Master.");
            }
        }

        private void cmbAccounts_Validated(object sender, EventArgs e)
        {
            if (cmbAccounts.SelectedValue == null)
            {
                MessageBox.Show("Account is not Exist Please Create it in ChartOfAccounts Master.");
            }
        }

        private void txtEcodeSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtEcodeSearch.Text.Length > 4)
            {
                try
                {
                    DataTable dt = objDB.ExecuteDataSet("SELECT HAMH_APPL_NUMBER,HAMH_NAME,HAMH_MY_PHOTO, case WHEN hamh_working_status='W' THEN 'WORKING' "+
                                    " WHEN hamh_working_status='L' THEN 'LEFT'"+
                                    " WHEN hamh_working_status='R' THEN 'RESIGNED' "+
                                    " WHEN hamh_working_status='P' THEN 'PENDING'"+
                                    " END AS STATUS,hafp_finger_fp2" +
                                    " FROM HR_APPL_MASTER_HEAD left join HR_APPL_FINGER_PRINTS on HAMH_APPL_NUMBER=hafp_APPL_NUMBER WHERE HAMH_EORA_CODE=" + txtEcodeSearch.Text + "").Tables[0];
                    txtName.Text = dt.Rows[0]["HAMH_NAME"].ToString();
                    txtApplNo.Text = dt.Rows[0]["HAMH_APPL_NUMBER"].ToString();
                    txtStatus.Text = dt.Rows[0]["STATUS"].ToString();
                    // DataSet dsPhoto = objDB.ExecuteDataSet("SELECT HAPS_PHOTO_SIG FROM HR_APPL_PHOTO_SIG WHERE HAPS_APPL_NUMBER = " + dt.Rows[0][0]);

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["HAMH_MY_PHOTO"].ToString().Length != 0)
                            GetImage((byte[])dt.Rows[0]["HAMH_MY_PHOTO"], "PHOTO");
                        else
                            picEmpPhoto.BackgroundImage = null;
                        if (dt.Rows[0]["hafp_finger_fp2"].ToString().Length == 0)
                        {
                            MessageBox.Show("Employee Enrollment Not Done \n Please Enroll First","",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                            btnVerify.Enabled = false;
                        }
                        else
                        {
                            btnVerify.Enabled = true;
                        }
                    }
                    
                    DataSet dsSig = objDB.ExecuteDataSet("SELECT isnull(isnull(HAUM_SIG1,HAUM_SIG2),HAUM_SIG3) HAUM_SIG FROM HR_APPL_UAN_MAS WHERE HAUM_APPL_NO=" + txtApplNo.Text);
                    if (dsSig.Tables[0].Rows.Count > 0)
                    {
                        if (dsSig.Tables[0].Rows[0]["HAUM_SIG"].ToString().Length != 0)
                            GetImage((byte[])dsSig.Tables[0].Rows[0]["HAUM_SIG"], "SIG");
                        else
                            pbSig2.BackgroundImage = null;
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
                pbSig2.BackgroundImage = null;
                txtStatus.Text = "";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtStatus.Text = "";
            txtName.Text = "";
            txtEcodeSearch.Text = "";
            txtApplNo.Text = "";
            txtChqDDNo.Text = "";
            txtVouchRefNo.Text = "";
            txtReceivedAmt.Text = "";
            txtRemarks.Text = "";
            txtPunchDate.Text = "";
            txtPunchTime.Text = "";
            cmbAccounts.SelectedIndex = 0;
            picEmpPhoto.BackgroundImage = null;
            pbSig2.BackgroundImage = null;
            picFigerPrint.BackgroundImage = null;
            cmbMajorCost.DataSource = null;
            btnSave.Enabled = false;
            GenerateVoucherId();

        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (txtApplNo.Text.Length > 0)
            {
                Verify frm = new Verify();
                frm.emp_appl_number = txtApplNo.Text;
                frm.ShowDialog();
                if (frm.fpstatus == true)
                {
                    MessageBox.Show("Employee Verified");
                    picFigerPrint.BackgroundImage = frm.PictureBox1.Image;
                    txtPunchDate.Text = CommonData.CurrentDate;
                    txtPunchTime.Text = DateTime.Now.ToLongTimeString();
                    btnSave.Enabled = true;
                }
                else
                {
                    MessageBox.Show("FingerPrint Not Matched","",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                    btnSave.Enabled = false;
                }

            }
            else
            {
                MessageBox.Show("Please Enter Valid Ecode");
                btnSave.Enabled = false;
            }

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

        private void txtEcodeSearch_Validated(object sender, EventArgs e)
        {

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

        

        
    }
}
