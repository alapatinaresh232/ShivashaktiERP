using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSTrans;
using System.Data.SqlClient;
namespace SDMS
{
    public partial class BankBillRecievedDetails : Form
    {
        SQLDB objSQLdb;
        InvoiceDB objInvdb = null;
        DataRow[] drs;
        DataRow[] drss;
        DataTable dtTemp = new DataTable();

        public BankRecieptVoucher objBankReceiptVoucher;
        string strType = "";
        DataGridView dgvBillDetails = null;
        private DataTable dt = new DataTable();
        private string sMobileNo = "";
        public bool isModifyFlag = false;
        public BankBillRecievedDetails()
        {
            InitializeComponent();
        }
        public BankBillRecievedDetails(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }

        private void BankBillRecievedDetails_Load(object sender, EventArgs e)
        {

            dt = ((BankRecieptVoucher)objBankReceiptVoucher).dtOutStanding.Clone();

            txtReceivedAmt.Text = "0";
            dtpBillDate.Value = DateTime.Today;
            //cbAdjustmentType.SelectedIndex = 0;
            cbPaymentMode.SelectedIndex = 0;
            DataGridView dgvBillDetails = ((BankRecieptVoucher)objBankReceiptVoucher).gvBillDetails;
            txtSlNo.Text = (dgvBillDetails.Rows.Count + 1) + "";
            gvOutstandigDetails.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold);
            if (drs != null)
            {
                FillHeadData();
                FillDetailData();
            }

        }
        private void FillHeadData()
        {
            if (drs != null)
            {
                txtSlNo.Text = drs[0][0].ToString();
                dtpBillDate.Value = Convert.ToDateTime(drs[0][6].ToString());
                txtAccountSearch.Text = drs[0][2].ToString();
                cbPaymentMode.SelectedItem = drs[0][4].ToString();
                txtChqNo.Text = drs[0][5].ToString();
                txtReceivedAmt.Text = drs[0][8].ToString();
                //cbAdjustmentType.SelectedItem = drs[0][9].ToString();
                txtRemarks.Text = drs[0][7].ToString();
               
            }
        }
        private void FillDetailData()
        {
            //DataRow[] dr = dtBillRecievedDetails.Select("SlNo=" + SlNo);
            drss = ((BankRecieptVoucher)objBankReceiptVoucher).dtAgnstVoucherBill.Select("isinos='" + drs[0][1] + "'");
            if (drss.Length > 0)
            {
                for (int i = 0; i < drss.Length; i++)
                {
                    DataRow[] tempRow = ((BankRecieptVoucher)objBankReceiptVoucher).dtOutStanding.Select("OU_BILL_NUMBER='" + drss[i][11] + "'");
                    if (tempRow.Length > 0)
                    {
                        drss[i][13] = tempRow[0][10];
                        drss[i][14] = tempRow[0][11];
                        drss[i][14] = Convert.ToDouble(drss[i][14]) + Convert.ToDouble(drss[i][15]);
                    }
                }
                gvOutstandigDetails.Rows.Clear();
                bool isExistOnAc = false;
                for (int i = 0; i < drss.Length; i++)
                {
                    gvOutstandigDetails.Rows.Add();
                    gvOutstandigDetails.Rows[i].Cells["SlNo"].Value = i + 1;

                    gvOutstandigDetails.Rows[i].Cells["AgCompCode"].Value = drss[i][6].ToString();
                    gvOutstandigDetails.Rows[i].Cells["AgStateCode"].Value = drss[i][7].ToString();
                    gvOutstandigDetails.Rows[i].Cells["AgBranchCode"].Value = drss[i][8].ToString();
                    gvOutstandigDetails.Rows[i].Cells["AgFinYear"].Value = drss[i][9].ToString();
                    gvOutstandigDetails.Rows[i].Cells["AgBillType"].Value = drss[i][10].ToString();

                    if (drss[i][5].ToString() == "A")
                    {
                        gvOutstandigDetails.Rows[i].Cells["Against"].Value = "BILL";
                        gvOutstandigDetails.Rows[i].Cells["InvoiceDate"].Value = Convert.ToDateTime(drss[i][12].ToString()).ToShortDateString();
                    }
                    if (drss[i][5].ToString() == "O")
                    {
                        isExistOnAc = true;
                        gvOutstandigDetails.Rows[i].Cells["Against"].Value = "ON A/C";

                    }
                    gvOutstandigDetails.Rows[i].Cells["InvoiceNumber"].Value = drss[i][11].ToString();

                    gvOutstandigDetails.Rows[i].Cells["InvoiceAmt"].Value = drss[i][13].ToString();
                    gvOutstandigDetails.Rows[i].Cells["OutstandingAmt"].Value = drss[i][14].ToString();
                    gvOutstandigDetails.Rows[i].Cells["ReceivedAmt"].Value = drss[i][15].ToString();
                }
                if (isExistOnAc == false)
                {
                    gvOutstandigDetails.Rows.Add();
                    gvOutstandigDetails.Rows[gvOutstandigDetails.Rows.Count - 1].Cells["SlNo"].Value = gvOutstandigDetails.Rows.Count;
                    gvOutstandigDetails.Rows[gvOutstandigDetails.Rows.Count - 1].Cells["InvoiceNumber"].Value = "";
                    gvOutstandigDetails.Rows[gvOutstandigDetails.Rows.Count - 1].Cells["Against"].Value = "ON A/C";
                    gvOutstandigDetails.Rows[gvOutstandigDetails.Rows.Count - 1].Cells["ReceivedAmt"].Value = "0";
                }
            }
            txtAgnstBillRecvdAmt.Text = txtReceivedAmt.Text;
            txtAgstBilBalAmt.Text = txtReceivedAmt.Text;
            CaluculatAdjutAmtAgainstReceived();
        }
        private void GetAccountNames()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT AM_ACCOUNT_ID AccountId, AM_ACCOUNT_NAME AccountName FROM FA_ACCOUNT_MASTER  ORDER BY AM_ACCOUNT_NAME";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";
                    row[1] = "--Select--";

                    dt.Rows.InsertAt(row, 0);
                    cbAccountName.DataSource = dt;
                    cbAccountName.DisplayMember = "AccountName";
                    cbAccountName.ValueMember = "AccountId";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            finally
            {
                objSQLdb = null;
                dt = null;
            }
        }


        private bool CheckData()
        {
            bool flag = true;
            if (txtAccountSearch.Text.Length == 0)
            {
                MessageBox.Show("Enter Account Number!", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                flag = false;
                txtAccountSearch.Focus();
                return flag;
            }
            if (cbPaymentMode.SelectedIndex == 0)
            {
                MessageBox.Show("Select PaymentMode!", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                flag = false;
                cbPaymentMode.Focus();
                return flag;
            }

            if (txtChqNo.Text.Length == 0)
            {
                MessageBox.Show("Enter Reference number!", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                flag = false;
                txtChqNo.Focus();
                return flag;
            }
            if (Convert.ToDouble(txtReceivedAmt.Text.ToString()) <= 0)
            {
                MessageBox.Show("Enter Received Amount!", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                MessageBox.Show("Enter Amount!", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            return flag;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                string strSLNo = "";
                if (drs != null)
                {

                    strSLNo = drs[0][0].ToString();

                    UpdatingOutstandingTableBeforeSaving();
                    ((BankRecieptVoucher)objBankReceiptVoucher).dtBillRecievedDetails.Rows.Remove(drs[0]);
                    for (int i = 0; i < drss.Length; i++)
                    {
                        ((BankRecieptVoucher)objBankReceiptVoucher).dtAgnstVoucherBill.Rows.Remove(drss[i]);
                    }
                }
                else
                {
                    strSLNo = txtSlNo.Text;
                }



                dgvBillDetails = ((BankRecieptVoucher)objBankReceiptVoucher).gvBillDetails;

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
                    
                    int count = 0;
                    int i = 0;
                    if (((BankRecieptVoucher)objBankReceiptVoucher).dtBillRecievedDetails.Rows.Count == 0)
                    {
                        count = 1;
                        i = 1;
                    }
                    else
                    {
                        i = Convert.ToInt32(((BankRecieptVoucher)objBankReceiptVoucher).dtBillRecievedDetails.Rows[((BankRecieptVoucher)objBankReceiptVoucher).dtBillRecievedDetails.Rows.Count - 1]["isinos"].ToString()) + 1;

                        count = Convert.ToInt32(((BankRecieptVoucher)objBankReceiptVoucher).dtBillRecievedDetails.Rows[((BankRecieptVoucher)objBankReceiptVoucher).dtBillRecievedDetails.Rows.Count - 1]["SlNo"].ToString()) + 1;
                    }
                    ((BankRecieptVoucher)objBankReceiptVoucher).dtBillRecievedDetails.Rows.Add(new Object[] { count ,
                        i, cbAccountName.SelectedValue.ToString(), 
                    strAccNames, cbPaymentMode.SelectedItem.ToString(), txtChqNo.Text, Convert.ToDateTime(dtpBillDate.Value).ToString("dd/MM/yyyy"), 
                    txtRemarks.Text, Convert.ToDouble(txtReceivedAmt.Text),
                    objBankReceiptVoucher.txtVoucherId.Text,objBankReceiptVoucher.dtpVoucherDate.Value,"C","","",objBankReceiptVoucher.txtVoucherNo.Text,
                    objBankReceiptVoucher.cbAccType.SelectedValue.ToString(),sMobileNo});
                    

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
                        ((BankRecieptVoucher)objBankReceiptVoucher).dtAgnstVoucherBill.Rows.Add(new Object[] {i, objBankReceiptVoucher.txtVoucherId.Text, 
                                Convert.ToDateTime(objBankReceiptVoucher.dtpVoucherDate.Value).ToShortDateString(), count ,
                                gvOutstandigDetails.Rows[iRow].Cells["SlNo"].Value,
                                strAdujstType, gvOutstandigDetails.Rows[iRow].Cells["AgCompCode"].Value, gvOutstandigDetails.Rows[iRow].Cells["AgStateCode"].Value,
                                gvOutstandigDetails.Rows[iRow].Cells["AgBranchCode"].Value, gvOutstandigDetails.Rows[iRow].Cells["AgFinYear"].Value,
                                gvOutstandigDetails.Rows[iRow].Cells["AgBillType"].Value, gvOutstandigDetails.Rows[iRow].Cells["InvoiceNumber"].Value, 
                                gvOutstandigDetails.Rows[iRow].Cells["InvoiceDate"].Value,   gvOutstandigDetails.Rows[iRow].Cells["InvoiceAmt"].Value,
                                gvOutstandigDetails.Rows[iRow].Cells["OutstandingAmt"].Value,gvOutstandigDetails.Rows[iRow].Cells["ReceivedAmt"].Value,txtChqNo.Text });
                        //}
                    }
                    objBankReceiptVoucher.iSiNo = i;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                ((BankRecieptVoucher)objBankReceiptVoucher).GetBillDetails();
                ((BankRecieptVoucher)objBankReceiptVoucher).CaluculatingTotalRcvdAmt();

                UpdatingOutstandingTable();


                MessageBox.Show("Details Added Sucessfully", "Cash Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //btnCancel_Click(null, null);
                this.Close();



            }
            //if (strType == "ReceiptVoucher")
            //{
            //    dgvBillDetails = ((ReceiptVoucher)objReceiptVoucher).gvBillDetails;
            //    AddBillDetailsToGrid(dgvBillDetails);
            //    MessageBox.Show("Bill dDetails saved Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    btnCancel_Click(null,null);

            //}
            //else
            //{
            //    MessageBox.Show("Bill Details Not saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}

        }
        private void UpdatingOutstandingTableBeforeSaving()
        {
            for (int i = 0; i < drss.Length; i++)
            {
                if (Convert.ToDouble(drss[i][15].ToString()) > 0)
                {
                    DataRow[] dr = ((BankRecieptVoucher)objBankReceiptVoucher).dtOutStanding.Select("OU_BILL_NUMBER='" + drss[i][11] + "'");
                    if (dr.Length > 0)
                    {
                        if (Convert.ToDouble(dr[0][11].ToString()) > 0)
                        {
                            int iSNo = Convert.ToInt32(dr[0][0].ToString());
                            double OutStdgAmt = Convert.ToDouble(((BankRecieptVoucher)objBankReceiptVoucher).dtOutStanding.Rows[iSNo - 1]["OU_AMOUNT"].ToString());
                            double PaidAmt = Convert.ToDouble(((BankRecieptVoucher)objBankReceiptVoucher).dtOutStanding.Rows[iSNo - 1]["OU_AMT_PAID_RCVD"].ToString());
                            OutStdgAmt += Convert.ToDouble(drss[i][15]);

                            PaidAmt -= Convert.ToDouble(drss[i][15]);
                            ((BankRecieptVoucher)objBankReceiptVoucher).dtOutStanding.Rows[iSNo - 1]["OU_AMOUNT"] = OutStdgAmt;
                            ((BankRecieptVoucher)objBankReceiptVoucher).dtOutStanding.Rows[iSNo - 1]["OU_AMT_PAID_RCVD"] = PaidAmt;
                        }

                    }
                }
            }
        }
        private void UpdatingOutstandingTable()
        {

            for (int i = 0; i < gvOutstandigDetails.Rows.Count; i++)
            {
                if (Convert.ToDouble(gvOutstandigDetails.Rows[i].Cells["ReceivedAmt"].Value.ToString()) > 0)
                {
                    //drss = ((ReceiptVoucher)objReceiptVoucher).dtAgnstVoucherBill.Select("MASTER_SL_NO=" + txtSlNo.Text);
                    DataRow[] dr = ((BankRecieptVoucher)objBankReceiptVoucher).dtOutStanding.Select("OU_BILL_NUMBER='" + gvOutstandigDetails.Rows[i].Cells["InvoiceNumber"].Value + "'");
                    if (dr.Length > 0)
                    {
                        if (Convert.ToDouble(dr[0][11].ToString()) > 0)
                        {
                            int iSNo = Convert.ToInt32(dr[0][0].ToString());
                            double OutStdgAmt = Convert.ToDouble(((BankRecieptVoucher)objBankReceiptVoucher).dtOutStanding.Rows[iSNo - 1]["OU_AMOUNT"].ToString());
                            double PaidAmt = Convert.ToDouble(((BankRecieptVoucher)objBankReceiptVoucher).dtOutStanding.Rows[iSNo - 1]["OU_AMT_PAID_RCVD"].ToString());
                            OutStdgAmt -= Convert.ToDouble(gvOutstandigDetails.Rows[i].Cells["ReceivedAmt"].Value);

                            PaidAmt += Convert.ToDouble(gvOutstandigDetails.Rows[i].Cells["ReceivedAmt"].Value);
                            ((BankRecieptVoucher)objBankReceiptVoucher).dtOutStanding.Rows[iSNo - 1]["OU_AMOUNT"] = OutStdgAmt;
                            ((BankRecieptVoucher)objBankReceiptVoucher).dtOutStanding.Rows[iSNo - 1]["OU_AMT_PAID_RCVD"] = PaidAmt;
                        }

                    }
                }
            }

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            gvOutstandigDetails.Rows.Clear();
            dtpBillDate.Value = DateTime.Today;
            txtChqNo.Text = string.Empty;
            txtReceivedAmt.Text = "0";
            txtRemarks.Text = string.Empty;
            //cbAdjustmentType.SelectedIndex = 0;
            //cbAccountName.SelectedIndex = 0;
            cbPaymentMode.SelectedIndex = 0;
            txtAccountSearch.Text = string.Empty;
          
        }

        private void txtAccountSearch_KeyUp(object sender, KeyEventArgs e)
        {
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
                //param[1] = objSQLDB.CreateParameter("@xDeptID", DbType.String, sDeptId, ParameterDirection.Input);
                //param[2] = objSQLDB.CreateParameter("@xDesigID", DbType.String, sDesgId, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xNameLike", DbType.String, txtAccountSearch.Text, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("DL_GetAcctNameSearch", CommandType.StoredProcedure, param);

                DataTable dtEmp = ds.Tables[0];

                //DataRow[] dtRow = dtEmp.Select("AM_ACCOUNT_ID=" + ((BankRecieptVoucher)objBankReceiptVoucher).cbAccType.SelectedValue.ToString());
                //if (dtRow.Length > 0)
                //{
                //    dtEmp.Rows.Remove(dtRow[0]);
                //}


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

        private void cbAccountName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAccountName.SelectedIndex > -1)
            {
                FillOutstandingAmount(cbAccountName.SelectedValue.ToString());
                string sqlText = "SELECT DAMH_FIRM_ADDR_MOBILE MobileNo FROM DL_APPL_MASTER_HEAD " +
                                    "WHERE DAMH_DEALER_CODE = " + cbAccountName.SelectedValue.ToString();
                objSQLdb = new SQLDB();
                try
                {
                    sMobileNo = objSQLdb.ExecuteDataSet(sqlText).Tables[0].Rows[0][0].ToString();
                }
                catch
                {
                    sMobileNo = "";
                }
                finally
                {
                    objSQLdb = null;
                }
            }
            else
            {
                gvOutstandigDetails.Rows.Clear();
            }
        }

        private void txtReceivedAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
       && !char.IsDigit(e.KeyChar)
       && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void cbAdjustmentType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void txtAccountSearch_TextChanged(object sender, EventArgs e)
        {

            if (txtAccountSearch.Text.ToString().Trim().Length > 0)
            {
                if (txtAccountSearch.Text.ToString().Trim().ToUpper() == ((BankRecieptVoucher)objBankReceiptVoucher).cbAccType.SelectedValue.ToString())
                {
                    MessageBox.Show("Entered Account is wrong");
                    txtAccountSearch.Text = "";
                    txtAccountSearch.Focus();
                }
                else
                {
                    EcodeSearch();

                    if (cbAccountName.SelectedIndex > -1)
                    {
                        FillOutstandingAmount(cbAccountName.SelectedValue.ToString());
                    }
                    else
                    {
                        gvOutstandigDetails.Rows.Clear();
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
                    dts.Rows[i]["SL_NO"] = ((BankRecieptVoucher)objBankReceiptVoucher).dtOutStanding.Rows.Count + 1;
                    i++;
                    ((BankRecieptVoucher)objBankReceiptVoucher).dtOutStanding.ImportRow(dr);

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
            if (((BankRecieptVoucher)objBankReceiptVoucher).dtOutStanding.Rows.Count > 0 && sAccountId.Length > 0)
            {
                DataRow[] dr = ((BankRecieptVoucher)objBankReceiptVoucher).dtOutStanding.Select("OU_ACCOUNT_ID='" + sAccountId + "'");
                dtTemp = ((BankRecieptVoucher)objBankReceiptVoucher).dtOutStanding.Clone();
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

        }
        private void gvOutstandigDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (txtReceivedAmt.Text.Length > 0)
            {
                if (e.ColumnIndex == 0)
                {
                    //if (Convert.ToBoolean(gvOutstandigDetails.Rows[e.RowIndex].Cells["chkRow"].FormattedValue) == false)
                    //{
                    //    gvOutstandigDetails.Rows[e.RowIndex].Cells["ReceivedAmt"].Value = 0;
                    //}
                    CaluculatAdjutAmtAgainstReceived();
                }
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

        private void txtReceivedAmt_TextChanged(object sender, EventArgs e)
        {
            //if (txtReceivedAmt.Text.Length == 0)
            //{
            //    txtReceivedAmt.Text = "0";
            //}

            //txtAgnstBillRecvdAmt.Text = txtReceivedAmt.Text;
            //txtAgstBilBalAmt.Text = txtReceivedAmt.Text;

            //if (gvOutstandigDetails.Rows.Count > 0)
            //{
            //    for (int i = 0; i < gvOutstandigDetails.Rows.Count; i++)
            //    {
            //        gvOutstandigDetails.Rows[i].Cells["ReceivedAmt"].Value = 0;
            //    }
            //    gvOutstandigDetails.Rows[gvOutstandigDetails.Rows.Count - 1].Cells["ReceivedAmt"].Value = txtReceivedAmt.Text;
            //}
        }

        private void gvOutstandigDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 12)
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    txtQty.MaxLength = 6;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
        }
        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtReceivedAmt_Validated(object sender, EventArgs e)
        {
            if (txtReceivedAmt.Text.Length == 0)
            {
                txtReceivedAmt.Text = "0";
            }

            txtAgnstBillRecvdAmt.Text = txtReceivedAmt.Text;
            txtAgstBilBalAmt.Text = txtReceivedAmt.Text;

            if (gvOutstandigDetails.Rows.Count > 0)
            {

                double ReceivedAmt = Convert.ToDouble(txtReceivedAmt.Text);
                for (int i = 0; i < gvOutstandigDetails.Rows.Count-1; i++)
                {
                    if(ReceivedAmt > Convert.ToDouble(gvOutstandigDetails.Rows[i].Cells["OutstandingAmt"].Value) )
                    {
                        gvOutstandigDetails.Rows[i].Cells["ReceivedAmt"].Value = gvOutstandigDetails.Rows[i].Cells["OutstandingAmt"].Value;
                        ReceivedAmt -= Convert.ToDouble(gvOutstandigDetails.Rows[i].Cells["OutstandingAmt"].Value);
                    }
                    else if (ReceivedAmt < Convert.ToDouble(gvOutstandigDetails.Rows[i].Cells["OutstandingAmt"].Value) && ReceivedAmt>0)
                    {
                        gvOutstandigDetails.Rows[i].Cells["ReceivedAmt"].Value = ReceivedAmt;
                        ReceivedAmt -= ReceivedAmt;
                    }
                    else if (ReceivedAmt == 0)
                    {
                        gvOutstandigDetails.Rows[i].Cells["ReceivedAmt"].Value = 0;
                    }

                }
                gvOutstandigDetails.Rows[gvOutstandigDetails.Rows.Count - 1].Cells["ReceivedAmt"].Value = ReceivedAmt;
            }
            CaluculatAdjutAmtAgainstReceived();
        }
    }
}
