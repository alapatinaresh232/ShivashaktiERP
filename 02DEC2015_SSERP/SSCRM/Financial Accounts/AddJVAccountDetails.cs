﻿using System;
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
    public partial class AddJVAccountDetails : Form
    {
         SQLDB objDB;
         string sType;
        DataRow[] drs;
        public JournalVoucher objJournalVoucher;
        public AddJVAccountDetails()
        {
            InitializeComponent();
        }
        public AddJVAccountDetails(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
         }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void AddJVAccountDetails_Load(object sender, EventArgs e)
        {
            txtSiNo.Text = (((JournalVoucher)objJournalVoucher).gvBillDetails.Rows.Count + 1).ToString();
            DataTable dtAccMas;
            objDB = new SQLDB();
           
                //lblMajorCost.Visible = true;
                //cmbMajorCost.Visible = true;
            dtAccMas = objDB.ExecuteDataSet(" SELECT AM_ACCOUNT_ID,AM_ACCOUNT_NAME AccName FROM FA_ACCOUNT_MASTER  WHERE AM_COMPANY_CODE='" + CommonData.CompanyCode + "' AND AM_COSTCENTRE_FLAG='Y' and AM_ACCOUNT_GROUP_ID IS NULL ").Tables[0];

           

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
            txtSiNo.Text = (((JournalVoucher)objJournalVoucher).gvBillDetails.Rows.Count + 1).ToString();
            //txtChqDDNo.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbAccounts.Text.Length == 0)
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


            for (int i = 0; i < ((JournalVoucher)objJournalVoucher).gvBillDetails.Rows.Count; i++)
            {
                if (((JournalVoucher)objJournalVoucher).gvBillDetails.Rows[i].Cells["AccountId"].Value.ToString() == cmbAccounts.SelectedValue.ToString())
                {
                    MessageBox.Show("Account Already Added", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    flag = true;
                    return;

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
           
                ((JournalVoucher)objJournalVoucher).gvBillDetails.Rows.Add();
                ((JournalVoucher)objJournalVoucher).gvBillDetails.Rows[((JournalVoucher)objJournalVoucher).gvBillDetails.Rows.Count - 1].Cells["SlNo"].Value
                    = ((JournalVoucher)objJournalVoucher).gvBillDetails.Rows.Count;
                ((JournalVoucher)objJournalVoucher).gvBillDetails.Rows[((JournalVoucher)objJournalVoucher).gvBillDetails.Rows.Count - 1].Cells["AccountId"].Value =
                    cmbAccounts.SelectedValue;
                ((JournalVoucher)objJournalVoucher).gvBillDetails.Rows[((JournalVoucher)objJournalVoucher).gvBillDetails.Rows.Count - 1].Cells["AccName"].Value =
                    cmbAccounts.Text;
                ((JournalVoucher)objJournalVoucher).gvBillDetails.Rows[((JournalVoucher)objJournalVoucher).gvBillDetails.Rows.Count - 1].Cells["CrDr"].Value =
                        cbType.SelectedItem ;
                ((JournalVoucher)objJournalVoucher).gvBillDetails.Rows[((JournalVoucher)objJournalVoucher).gvBillDetails.Rows.Count - 1].Cells["AmtReceived"].Value =
                    txtReceivedAmt.Text;
                ((JournalVoucher)objJournalVoucher).CalculateTotal();
                this.Close();
           
        }
        
    }
}
