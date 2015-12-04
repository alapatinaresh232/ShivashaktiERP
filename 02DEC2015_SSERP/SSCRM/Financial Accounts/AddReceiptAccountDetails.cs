using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSCRM.App_Code;

namespace SSCRM
{
    public partial class AddReceiptAccountDetails : Form
    {
        SQLDB objDB;
        public PaymentVouchers objPaymentVoucher;
        public ReceiptVoucher objRecieptVoucher;
        string sType;
        DataRow[] drs;
        public AddReceiptAccountDetails()
        {
            InitializeComponent();
        }
        public AddReceiptAccountDetails(string strType)
        {
            InitializeComponent();
            sType = strType;
        }
        public AddReceiptAccountDetails(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void AddAccountDetails_Load(object sender, EventArgs e)
        {
            objDB = new SQLDB();
            txtSiNo.Text = (((ReceiptVoucher)objRecieptVoucher).gvBillDetails.Rows.Count + 1).ToString();
            DataTable dtAccMas;

            //if (((ReceiptVoucher)objRecieptVoucher).cbEmpType.SelectedIndex == 1)
            //{
            //    //lblMajorCost.Visible = false;
            //    //cmbMajorCost.Visible = false;
            //    dtAccMas = objDB.ExecuteDataSet(" SELECT AM_ACCOUNT_ID,AM_ACCOUNT_NAME AccName FROM FA_ACCOUNT_MASTER  WHERE AM_COMPANY_CODE='" + CommonData.CompanyCode
            //                                            + "' and  AM_ACCOUNT_ID <> '" + ((ReceiptVoucher)objRecieptVoucher).cmbCashAccount.SelectedValue
            //                                            + "' AND AM_COSTCENTRE_FLAG IS NULL and AM_ACCOUNT_GROUP_ID IS NULL ").Tables[0];
            //}
            //else
            //{
            //    //lblMajorCost.Visible = true;
            //    //cmbMajorCost.Visible = true;
                dtAccMas = objDB.ExecuteDataSet(" SELECT AM_ACCOUNT_ID,AM_ACCOUNT_NAME AccName FROM FA_ACCOUNT_MASTER  WHERE AM_COMPANY_CODE='" + CommonData.CompanyCode + "' AND AM_COSTCENTRE_FLAG='Y' and AM_ACCOUNT_GROUP_ID IS NULL ").Tables[0];

            //}

            UtilityLibrary.AutoCompleteComboBox(cmbAccounts, dtAccMas, "AM_ACCOUNT_ID", "AccName");
            if (dtAccMas.Rows.Count > 0)
            {
                DataRow dr = dtAccMas.NewRow();


                cmbAccounts.DataSource = dtAccMas;
                cmbAccounts.DisplayMember = "AccName";
                cmbAccounts.ValueMember = "AM_ACCOUNT_ID";
            }
            if (drs != null)
            {
                txtSiNo.Text = drs[0]["SL_NO"].ToString();
                cmbAccounts.SelectedValue = drs[0]["ACC_ID"].ToString();
                //cmbMajorCost.SelectedValue = drs[0]["MAJCOST_ID"].ToString();
                //cbPaymentMode.SelectedItem = drs[0]["PAYMENT_MODE"].ToString();
                //txtChqDDNo.Text = drs[0]["CHQ_DD_NO"].ToString();
                txtReceivedAmt.Text = drs[0]["AMOUNT"].ToString();
                //txtRemarks.Text = drs[0]["REMARKS"].ToString();
            }
        }
       
      
        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if(cmbAccounts.Text.Length==0)
            {
                MessageBox.Show("Please Select Account Head", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //else if (cmbMajorCost.SelectedValue == null && ((PaymentVouchers)objPaymentVoucher).cmbEmpType.SelectedIndex==0)
            //{
            //    MessageBox.Show("MajorCostCentre is not Exist Please create it in MajorCostCentre Master.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}

            else if (txtReceivedAmt.Text.Length == 0)
            {
                MessageBox.Show("Please Enter Amount", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool flag = false;
            if (sType == "RECEIPT")
            {

                for (int i = 0; i < ((ReceiptVoucher)objRecieptVoucher).gvBillDetails.Rows.Count; i++)
                {
                    if (((ReceiptVoucher)objRecieptVoucher).gvBillDetails.Rows[i].Cells["AccountId"].Value.ToString() == cmbAccounts.SelectedValue.ToString())
                    {
                        MessageBox.Show("Account Already Added", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        flag = true;
                        return;

                    }
                }
            }
            else
            {
                for (int i = 0; i < ((PaymentVouchers)objPaymentVoucher).gvBillDetails.Rows.Count; i++)
                {
                    if (((PaymentVouchers)objPaymentVoucher).gvBillDetails.Rows[i].Cells["AccountId"].Value.ToString() == cmbAccounts.SelectedValue.ToString())
                    {
                        MessageBox.Show("Account Already Added", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        flag = true;
                        return;

                    }
                }
            }
            //else if (cbPaymentMode.SelectedIndex > 0 && txtChqDDNo.Text.Length == 0)
            //{
            //    MessageBox.Show("Please Enter Cheq/DD Ref No", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //else if (txtRemarks.Text.Length == 0)
            //{
            //    MessageBox.Show("Please Enter Remarks", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}

            //if (drs != null)
            //    ((PaymentVouchers)objPaymentVoucher).dtPayVoucher.Rows.Remove(drs[0]);
            //((PaymentVouchers)objPaymentVoucher).dtPayVoucher.Rows.Add(new Object[] { "-1", cmbAccounts.SelectedValue, cmbAccounts.Text, txtReceivedAmt.Text });
            //((PaymentVouchers)objPaymentVoucher).GetAccountDetials();
            if(flag==false&&sType=="RECEIPT")
            {
            ((ReceiptVoucher)objRecieptVoucher).gvBillDetails.Rows.Add();
            ((ReceiptVoucher)objRecieptVoucher).gvBillDetails.Rows[((ReceiptVoucher)objRecieptVoucher).gvBillDetails.Rows.Count - 1].Cells["SlNo"].Value
                = ((ReceiptVoucher)objRecieptVoucher).gvBillDetails.Rows.Count;
            ((ReceiptVoucher)objRecieptVoucher).gvBillDetails.Rows[((ReceiptVoucher)objRecieptVoucher).gvBillDetails.Rows.Count - 1].Cells["AccountId"].Value =
                cmbAccounts.SelectedValue;
            ((ReceiptVoucher)objRecieptVoucher).gvBillDetails.Rows[((ReceiptVoucher)objRecieptVoucher).gvBillDetails.Rows.Count - 1].Cells["AccName"].Value =
                cmbAccounts.Text;
            ((ReceiptVoucher)objRecieptVoucher).gvBillDetails.Rows[((ReceiptVoucher)objRecieptVoucher).gvBillDetails.Rows.Count - 1].Cells["Cash"].Value =
                txtReceivedAmt.Text;
            ((ReceiptVoucher)objRecieptVoucher).gvBillDetails.Rows[((ReceiptVoucher)objRecieptVoucher).gvBillDetails.Rows.Count - 1].Cells["Bills"].Value =
                "0";
            ((ReceiptVoucher)objRecieptVoucher).gvBillDetails.Rows[((ReceiptVoucher)objRecieptVoucher).gvBillDetails.Rows.Count - 1].Cells["AmtReceived"].Value =
                txtReceivedAmt.Text;
            ((ReceiptVoucher)objRecieptVoucher).CalculateTotal();
            ((ReceiptVoucher)objRecieptVoucher).GenerateNarration();
            
                this.Close();
            }
            if (flag == false && sType != "RECEIPT")
            {
                ((PaymentVouchers)objPaymentVoucher).gvBillDetails.Rows.Add();
                ((PaymentVouchers)objPaymentVoucher).gvBillDetails.Rows[((PaymentVouchers)objPaymentVoucher).gvBillDetails.Rows.Count - 1].Cells["SlNo"].Value
                    = ((PaymentVouchers)objPaymentVoucher).gvBillDetails.Rows.Count;
                ((PaymentVouchers)objPaymentVoucher).gvBillDetails.Rows[((PaymentVouchers)objPaymentVoucher).gvBillDetails.Rows.Count - 1].Cells["AccountId"].Value =
                    cmbAccounts.SelectedValue;
                ((PaymentVouchers)objPaymentVoucher).gvBillDetails.Rows[((PaymentVouchers)objPaymentVoucher).gvBillDetails.Rows.Count - 1].Cells["AccName"].Value =
                    cmbAccounts.Text;
                ((PaymentVouchers)objPaymentVoucher).gvBillDetails.Rows[((PaymentVouchers)objPaymentVoucher).gvBillDetails.Rows.Count - 1].Cells["AmtReceived"].Value =
                    txtReceivedAmt.Text;
                //((PaymentVouchers)objPaymentVoucher).CalculateTotal();
                //((PaymentVouchers)objPaymentVoucher).GenerateNarration();
                this.Close();
            }
            
        }

        private void txtReceivedAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false)
                e.Handled = true;
            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtReceivedAmt.Text = "";
            //txtRemarks.Text = "";
            if (sType != "RECEIPT")
                txtSiNo.Text = (((PaymentVouchers)objPaymentVoucher).gvBillDetails.Rows.Count + 1).ToString();
            else
                txtSiNo.Text = (((ReceiptVoucher)objRecieptVoucher).gvBillDetails.Rows.Count + 1).ToString();
            //txtChqDDNo.Text = "";
        }
    }
}
