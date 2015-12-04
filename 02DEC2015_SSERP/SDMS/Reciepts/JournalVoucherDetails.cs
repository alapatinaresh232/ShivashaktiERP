using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SSCRMDB;
using SSTrans;


namespace SDMS
{
    public partial class JournalVoucherDetails : Form
    {
        SQLDB objSQLdb = null;
        InvoiceDB objInvdb = null;
        DataRow[] drs;
        DataRow[] drss;
        public JournalVoucher objJournalVoucher;
        DataGridView dgvBillDetails = null;
        DataTable dtTemp = new DataTable();
        public JournalVoucherDetails()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtAccountSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtAccountSearch.Text.ToString().Trim().Length > 0)
            {
                EcodeSearch();
                if (CheckingForDuplicate())
                {
                    if (cbAccountName.SelectedIndex > -1 && cbDrOrCr.SelectedItem.ToString() == "CREDIT")
                    {
                        FillOutstandingAmount(cbAccountName.SelectedValue.ToString());
                    }
                    else
                    {
                        DataTable dt = new DataTable();
                        AddDataToGrid(dt);
                    }
                    //AddDataToGrid(dts);
                    //else
                    //{
                    //    AddDataToGrid(((ReceiptVoucher)objReceiptVoucher).dtOutStanding);
                    //}
                }
            }
            else
            {
                cbAccountName.SelectedIndex = -1;
            }
        }

        private bool CheckingForDuplicate()
        {
            bool flag = true;
            for (int i = 0; i < ((JournalVoucher)objJournalVoucher).gvBillDetails.Rows.Count;i++ )
            {
                if (((JournalVoucher)objJournalVoucher).gvBillDetails.Rows[i].Cells["AccountId"].Value==cbAccountName.SelectedValue.ToString())
                {
                    MessageBox.Show("Account Already Exists", "Journal Voucher", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    flag = false;
                    //cbDrOrCr.Focus();
                    return flag;
                }
            }
            if (txtAccountSearch.Tag == "CASH" || txtAccountSearch.Tag == "BANK")
            {
                MessageBox.Show("You Cant Enter this Account", "Journal Voucher", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                flag = false;
                return flag;
            }

            return flag;
        }
        public void FillOutstandingAmount(string sAccountId)
        {
            DataTable dts = new DataTable();
            if (!CheckingForOutStandingAmtInTempTable(sAccountId))
            {
                try
                {
                    //string strSQL = "";
                    //objSQLdb = new SQLDB();
                    //dts = objSQLdb.ExecuteDataSet(strSQL).Tables[0];

                    objInvdb = new InvoiceDB();
                    dts = objInvdb.GetAdjustingAmount(sAccountId).Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {

                }

                dts.Columns.Add("AG_BILL_AMOUNT");
                dts.Columns.Add("SL_NO");
                //ADDING TO TEMP OUTSTANDING TABLE
                int i = 0;
                foreach (DataRow dr in dts.Rows)
                {

                    dts.Rows[i]["AG_BILL_AMOUNT"] = 0;
                    dts.Rows[i]["SL_NO"] = ((JournalVoucher)objJournalVoucher).dtOutStanding.Rows.Count + 1;
                    i++;
                    ((JournalVoucher)objJournalVoucher).dtOutStanding.ImportRow(dr);

                }

                AddDataToGrid(dts);
            }
            else
            {
                AddDataToGrid(dtTemp);
            }
        }
        private bool CheckingForOutStandingAmtInTempTable(string sAccountId)
        {
            bool flag = false;
            if (((JournalVoucher)objJournalVoucher).dtOutStanding.Rows.Count > 0 && sAccountId.Length > 0)
            {
                DataRow[] dr = ((JournalVoucher)objJournalVoucher).dtOutStanding.Select("OU_ACCOUNT_ID='" + sAccountId + "'");
                dtTemp = ((JournalVoucher)objJournalVoucher).dtOutStanding.Clone();
                if (dr.Length > 0)
                {
                    flag = true;
                    for (int i = 0; i < dr.Length; i++)
                    {
                        if (Convert.ToDouble(dr[i][10].ToString()) > 0)
                        {
                            dtTemp.ImportRow(dr[i]);
                        }
                    }

                }
                return flag;
            }
            return flag;
        }
        private void AddDataToGrid(DataTable dt)
        {
            int i = 0;
            gvOutstandigDetails.Rows.Clear();
            if (dt.Rows.Count > 0)
            {

                for (int iRow = 0; iRow < dt.Rows.Count; iRow++)
                {
                    if (Convert.ToDouble(dt.Rows[iRow]["OU_AMOUNT"].ToString()) > 0)
                    {
                        gvOutstandigDetails.Rows.Add();
                        gvOutstandigDetails.Rows[i].Cells["SlNo"].Value = i + 1;

                        gvOutstandigDetails.Rows[i].Cells["AgCompCode"].Value = dt.Rows[iRow]["OU_COMPANY_CODE"].ToString();
                        gvOutstandigDetails.Rows[i].Cells["AgStateCode"].Value = dt.Rows[iRow]["OU_STATE_CODE"].ToString();
                        gvOutstandigDetails.Rows[i].Cells["AgBranchCode"].Value = dt.Rows[iRow]["OU_BRANCH_CODE"].ToString();
                        gvOutstandigDetails.Rows[i].Cells["AgFinYear"].Value = dt.Rows[iRow]["OU_FIN_YEAR"].ToString();
                        gvOutstandigDetails.Rows[i].Cells["AgBillType"].Value = dt.Rows[iRow]["OU_BILL_TYPE"].ToString();


                        gvOutstandigDetails.Rows[i].Cells["Against"].Value = "BILL";
                        gvOutstandigDetails.Rows[i].Cells["InvoiceNumber"].Value = dt.Rows[iRow]["OU_BILL_NUMBER"].ToString();
                        gvOutstandigDetails.Rows[i].Cells["InvoiceDate"].Value = Convert.ToDateTime(dt.Rows[iRow]["OU_BILL_DATE"].ToString()).ToShortDateString();
                        gvOutstandigDetails.Rows[i].Cells["InvoiceAmt"].Value = dt.Rows[iRow]["OU_BILL_AMOUNT"].ToString();
                        gvOutstandigDetails.Rows[i].Cells["OutstandingAmt"].Value = dt.Rows[iRow]["OU_AMOUNT"].ToString();
                        gvOutstandigDetails.Rows[i].Cells["ReceivedAmt"].Value = dt.Rows[iRow]["AG_BILL_AMOUNT"];
                        i++;
                    }
                }
            }
            gvOutstandigDetails.Rows.Add();
            //Convert.ToBoolean(gvOutstandigDetails.Rows[i].Cells["chkRow"].FormattedValue) = true;
            gvOutstandigDetails.Rows[i].Cells["SlNo"].Value = i + 1;
            gvOutstandigDetails.Rows[i].Cells["InvoiceNumber"].Value = "";
            gvOutstandigDetails.Rows[i].Cells["Against"].Value = "ON A/C";
            gvOutstandigDetails.Rows[i].Cells["ReceivedAmt"].Value = txtReceivedAmt.Text;

        }
        private void EcodeSearch()
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                cbAccountName.DataSource = null;
                cbAccountName.Items.Clear();

                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xNameLike", DbType.String, txtAccountSearch.Text, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("DL_GetJVAcctNameSearch", CommandType.StoredProcedure, param);

                DataTable dtEmp = ds.Tables[0];

                if (dtEmp.Rows.Count > 0)
                {
                    cbAccountName.DataSource = dtEmp;
                    cbAccountName.DisplayMember = "AccountName";
                    cbAccountName.ValueMember = "AM_ACCOUNT_ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (cbAccountName.SelectedIndex > -1)
                {
                    cbAccountName.SelectedIndex = 0;
                    txtAccountSearch.Tag = ds.Tables[0].Rows[0]["AccountType"].ToString();
                    //txtAccountSearch.Text = ((System.Data.DataRowView)(cbAccountName.SelectedItem)).Row.ItemArray[0].ToString();
                }
                objSQLdb = null;
                Cursor.Current = Cursors.Default;
            }
            if (ds.Tables == null)
            {
                gvOutstandigDetails.Rows.Clear();
            }
        }

        private void JournalVoucherDetails_Load(object sender, EventArgs e)
        {
            txtReceivedAmt.Text = "0";
            dtpBillDate.Value = DateTime.Today;
            cbDrOrCr.SelectedIndex = 0;
            DataGridView dgvBillDetails = ((JournalVoucher)objJournalVoucher).gvBillDetails;
            txtSlNo.Text = (dgvBillDetails.Rows.Count + 1) + "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dtpBillDate.Value = DateTime.Today;
            txtChqNo.Text = string.Empty;
            txtReceivedAmt.Text ="0";
            txtRemarks.Text = string.Empty;
            //cbAccountName.SelectedIndex = 0;
            cbDrOrCr.SelectedIndex = 0;
            txtAccountSearch.Text = string.Empty;
            gvOutstandigDetails.Rows.Clear();
        }

        private void cbDrOrCr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAccountName.SelectedIndex > -1 && cbDrOrCr.SelectedItem.ToString() == "CREDIT")
            {
                FillOutstandingAmount(cbAccountName.SelectedValue.ToString());
            }
            else
            {
                DataTable dt = new DataTable();
                AddDataToGrid(dt);
            }
        }

        private void txtReceivedAmt_TextChanged(object sender, EventArgs e)
        {
            if (txtReceivedAmt.Text.Length == 0)
            {
                txtReceivedAmt.Text = "0";
            }

            txtAgnstBillRecvdAmt.Text = txtReceivedAmt.Text;
            txtAgstBilBalAmt.Text = txtReceivedAmt.Text;

            if (gvOutstandigDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvOutstandigDetails.Rows.Count; i++)
                {
                    gvOutstandigDetails.Rows[i].Cells["ReceivedAmt"].Value = 0;
                }
                gvOutstandigDetails.Rows[gvOutstandigDetails.Rows.Count - 1].Cells["ReceivedAmt"].Value = txtReceivedAmt.Text;
            }
        }

        private void gvOutstandigDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 12)
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    txtQty.MaxLength = 10;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
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
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }

        }

        private void gvOutstandigDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 12)
            {
                if (Convert.ToDouble(txtReceivedAmt.Text) > 0)
                {
                    if (gvOutstandigDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                    {
                        gvOutstandigDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
                    }
                    //if ( Convert.ToInt32(gvOutstandigDetails.Rows[e.RowIndex].Cells["ReceivedAmt"].Value.ToString()) > 0)//&&Convert.ToBoolean(gvOutstandigDetails.Rows[e.RowIndex].Cells["chkRow"].FormattedValue) == false 
                    //{
                    //    MessageBox.Show("Please First Check it", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    gvOutstandigDetails.Rows[e.RowIndex].Cells["ReceivedAmt"].Value = "0";
                    //}
                    if (gvOutstandigDetails.Rows[e.RowIndex].Cells["Against"].Value == "BILL")
                        if (Convert.ToInt32(gvOutstandigDetails.Rows[e.RowIndex].Cells["ReceivedAmt"].Value) > Convert.ToDouble(gvOutstandigDetails.Rows[e.RowIndex].Cells["OutstandingAmt"].Value))
                        {

                            MessageBox.Show("Entered Amount is Exceded than Outstanding Amount", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            gvOutstandigDetails.Rows[e.RowIndex].Cells["ReceivedAmt"].Value = "0";
                        }
                    if (CaluculatAdjutAmtAgainstReceived() > Convert.ToDouble(txtAgnstBillRecvdAmt.Text))
                    {
                        MessageBox.Show("Entered Amount is Exceded than Received Amount", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        gvOutstandigDetails.Rows[e.RowIndex].Cells["ReceivedAmt"].Value = "0";
                    }
                    if (txtReceivedAmt.Text.Length > 0)
                    {
                        CaluculatAdjutAmtAgainstReceived();
                    }
                }
                else
                {
                    gvOutstandigDetails.Rows[e.RowIndex].Cells["ReceivedAmt"].Value = "0";
                    MessageBox.Show("Enter Received Amount First!", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtReceivedAmt.Focus();
                }
            }
        }

        private double CaluculatAdjutAmtAgainstReceived()
        {
            double totalAdjustAmt = 0;
            double balAmt = 0;
            double recivedAmt = Convert.ToDouble(txtAgnstBillRecvdAmt.Text);
            for (int iRow = 0; iRow < gvOutstandigDetails.Rows.Count - 1; iRow++)
            {
                if (gvOutstandigDetails.Rows[iRow].Cells["ReceivedAmt"].Value == null)
                {
                    gvOutstandigDetails.Rows[iRow].Cells["ReceivedAmt"].Value = "0";
                }
                if (Convert.ToDouble(gvOutstandigDetails.Rows[iRow].Cells["ReceivedAmt"].Value.ToString()) > 0)//&& Convert.ToBoolean(gvOutstandigDetails.Rows[iRow].Cells["chkRow"].FormattedValue) == true
                {
                    totalAdjustAmt += Convert.ToDouble(gvOutstandigDetails.Rows[iRow].Cells["ReceivedAmt"].Value);
                }
            }
            balAmt = recivedAmt - totalAdjustAmt;
            txtAgstBilAdjAmt.Text = totalAdjustAmt.ToString("0.00");
            txtAgstBilBalAmt.Text = balAmt.ToString("0.00");
            gvOutstandigDetails.Rows[gvOutstandigDetails.Rows.Count - 1].Cells["ReceivedAmt"].Value = balAmt.ToString("0.00");
            return totalAdjustAmt;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(CheckData())
            {
                string strSLNo = "";


                if (drs != null)
                {

                    strSLNo = drs[0][0].ToString();

                   // UpdatingOutstandingTableBeforeSaving();
                    ((JournalVoucher)objJournalVoucher).dtBillRecievedDetails.Rows.Remove(drs[0]);
                    for (int i = 0; i < drss.Length; i++)
                    {
                        ((JournalVoucher)objJournalVoucher).dtAgnstVoucherBill.Rows.Remove(drss[i]);
                    }
                }
                else
                {
                    strSLNo = txtSlNo.Text;
                }

                dgvBillDetails = ((JournalVoucher)objJournalVoucher).gvBillDetails;

                string[] strAccName = cbAccountName.Text.Split('-');
                string strAccNames = "";
                if (strAccName.Length > 2)
                {
                    strAccNames = strAccName[0];
                }
                else
                {
                    strAccNames = cbAccountName.Text;
                }
                try
                {
                    int i = objJournalVoucher.iSiNo + 1;
                    int count = 0;
                    string strCRorDr = "";
                    if (((JournalVoucher)objJournalVoucher).dtBillRecievedDetails.Rows.Count == 0)
                        count = 1;
                    else
                        count = Convert.ToInt32(((JournalVoucher)objJournalVoucher).dtBillRecievedDetails.Rows[((JournalVoucher)objJournalVoucher).dtBillRecievedDetails.Rows.Count - 1]["SlNo"].ToString()) + 1;
                    if (cbDrOrCr.SelectedItem.ToString() == "CREDIT")
                    {
                        strCRorDr = "C";
                    }
                    else if (cbDrOrCr.SelectedItem.ToString() == "DEBIT")
                    {
                        strCRorDr = "D";
                    }
                    ((JournalVoucher)objJournalVoucher).dtBillRecievedDetails.Rows.Add(new Object[] { count ,
                        i, cbAccountName.SelectedValue.ToString(), 
                    strAccNames,"" , txtChqNo.Text, Convert.ToDateTime(dtpBillDate.Value).ToString("dd/MM/yyyy"), 
                    txtRemarks.Text, Convert.ToDouble(txtReceivedAmt.Text),
                    objJournalVoucher.txtVoucherId.Text,objJournalVoucher.dtpVoucherDate.Value,strCRorDr,"","",objJournalVoucher.txtReferenceNo.Text,
                    ""});

                    for (int iRow = 0; iRow < gvOutstandigDetails.Rows.Count; iRow++)
                    {
                        //if ( Convert.ToDouble(gvOutstandigDetails.Rows[iRow].Cells["ReceivedAmt"].Value)>0 )//&& Convert.ToBoolean(gvOutstandigDetails.Rows[iRow].Cells["chkRow"].FormattedValue) == true 
                        //{
                        string strAdujstType = "";
                        if (gvOutstandigDetails.Rows[iRow].Cells["Against"].Value == "BILL")
                        {
                            strAdujstType = "A";
                        }
                        if (gvOutstandigDetails.Rows[iRow].Cells["Against"].Value == "ON A/C")
                        {
                            strAdujstType = "O";
                        }
                        ((JournalVoucher)objJournalVoucher).dtAgnstVoucherBill.Rows.Add(new Object[] {i, objJournalVoucher.txtVoucherId.Text, 
                                Convert.ToDateTime(objJournalVoucher.dtpVoucherDate.Value).ToShortDateString(), count ,
                                gvOutstandigDetails.Rows[iRow].Cells["SlNo"].Value,
                                strAdujstType, gvOutstandigDetails.Rows[iRow].Cells["AgCompCode"].Value, gvOutstandigDetails.Rows[iRow].Cells["AgStateCode"].Value,
                                gvOutstandigDetails.Rows[iRow].Cells["AgBranchCode"].Value, gvOutstandigDetails.Rows[iRow].Cells["AgFinYear"].Value,
                                gvOutstandigDetails.Rows[iRow].Cells["AgBillType"].Value, gvOutstandigDetails.Rows[iRow].Cells["InvoiceNumber"].Value, 
                                gvOutstandigDetails.Rows[iRow].Cells["InvoiceDate"].Value,   gvOutstandigDetails.Rows[iRow].Cells["InvoiceAmt"].Value,
                                gvOutstandigDetails.Rows[iRow].Cells["OutstandingAmt"].Value,gvOutstandigDetails.Rows[iRow].Cells["ReceivedAmt"].Value,txtChqNo.Text });
                        //}
                    }
                    objJournalVoucher.iSiNo = i;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                ((JournalVoucher)objJournalVoucher).GetBillDetails();
                ((JournalVoucher)objJournalVoucher).CaluculatingTotalRcvdAmt();

                //UpdatingOutstandingTable();


                MessageBox.Show("Details Added Sucessfully", "Cash Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //btnCancel_Click(null, null);
                this.Close();


            }
        }
        private bool CheckData()
        {
            bool flag = true;
            if (txtAccountSearch.Text.Length == 0)
            {
                MessageBox.Show("Enter Account Number!", "Journal Voucher", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                flag = false;
                txtAccountSearch.Focus();
                return flag;
            }
            if (cbDrOrCr.SelectedIndex == 0)
            {
                MessageBox.Show("Select Debit Or Credit!", "Journal Voucher", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                flag = false;
                cbDrOrCr.Focus();
                return flag;
            }

            if (txtChqNo.Text.Length == 0)
            {
                MessageBox.Show("Enter Reference number!", "Journal Voucher", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                flag = false;
                txtChqNo.Focus();
                return flag;
            }
            if (Convert.ToDouble(txtReceivedAmt.Text.ToString()) <= 0)
            {
                MessageBox.Show("Enter Received Amount!", "Journal Voucher", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                flag = false;
                txtReceivedAmt.Focus();
                return flag;
            }
            bool bReceivedAmt = false;
            for (int i = 0; i < gvOutstandigDetails.Rows.Count; i++)
            {
                if (Convert.ToDouble(gvOutstandigDetails.Rows[i].Cells["ReceivedAmt"].Value.ToString()) > 0)
                {
                    bReceivedAmt = true;
                }
            }
            if (bReceivedAmt == false && Convert.ToDouble(gvOutstandigDetails.Rows[gvOutstandigDetails.Rows.Count - 1].Cells["ReceivedAmt"].Value.ToString()) == 0)
            {
                MessageBox.Show("Enter Amount!", "Journal Voucher", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }


            return flag;
        }

        private void cbAccountName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
