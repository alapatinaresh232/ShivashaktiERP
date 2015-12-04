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

namespace SDMS
{
    public partial class CashPaymentDetails : Form
    {
        SQLDB objSQLdb = null;
        public CashPayment objCashPayment;
        DataRow[] drs;
        DataRow[] drss;
        public bool isModifyFlag = false;
        DataGridView dgvBillDetails = null;
        public CashPaymentDetails()
        {
            InitializeComponent();
        }
        public CashPaymentDetails(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }
        private void CashPaymentDetails_Load(object sender, EventArgs e)
        {
            txtReceivedAmt.Text = "0";
            dtpBillDate.Value = DateTime.Today;
            cbPaymentMode.SelectedIndex = 0;
            DataGridView dgvBillDetails = ((CashPayment)objCashPayment).gvBillDetails;
            txtSlNo.Text = (dgvBillDetails.Rows.Count + 1) + "";


            if (drs != null)
            {
                FillHeadData();
                
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtAccountSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtAccountSearch.Text.ToString().Trim().Length > 0)
            {
                if (txtAccountSearch.Text.ToString().Trim().ToUpper() == ((CashPayment)objCashPayment).cbAccType.SelectedValue.ToString())
                {
                    MessageBox.Show("Entered Account is wrong");
                    txtAccountSearch.Text = "";
                    txtAccountSearch.Focus();
                }
                else
                {
                    EcodeSearch();
                }
            }
            else
            {
                cbAccountName.SelectedIndex = -1;
            }
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

                //DataRow[] dtRow = dtEmp.Select("AM_ACCOUNT_ID=" + ((CashPayment)objCashPayment).cbAccType.SelectedValue.ToString());
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
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dtpBillDate.Value = DateTime.Today;
            txtChqNo.Text = string.Empty;
            txtReceivedAmt.Text ="0";
            txtRemarks.Text = string.Empty;
            //cbAccountName.SelectedIndex = 0;
            cbPaymentMode.SelectedIndex = 0;
            txtAccountSearch.Text = string.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                string strSLNo = "";
                if (drs != null)
                {
                    strSLNo = drs[0][0].ToString();
                    //UpdatingOutstandingTableBeforeSaving();
                    ((CashPayment)objCashPayment).dtBillRecievedDetails.Rows.Remove(drs[0]);
                }
                else
                {
                    strSLNo = txtSlNo.Text;
                }



                dgvBillDetails = ((CashPayment)objCashPayment).gvBillDetails;

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
                    int i = objCashPayment.iSiNo + 1;
                    int count = 0;
                    if (((CashPayment)objCashPayment).dtBillRecievedDetails.Rows.Count == 0)
                        count = 1;
                    else
                        count = Convert.ToInt32(((CashPayment)objCashPayment).dtBillRecievedDetails.Rows[((CashPayment)objCashPayment).dtBillRecievedDetails.Rows.Count - 1]["SlNo"].ToString()) + 1;
                    ((CashPayment)objCashPayment).dtBillRecievedDetails.Rows.Add(new Object[] { count ,
                        i, cbAccountName.SelectedValue.ToString(), 
                    strAccNames, cbPaymentMode.SelectedItem.ToString(), txtChqNo.Text, Convert.ToDateTime(dtpBillDate.Value).ToString("dd/MM/yyyy"), 
                    txtRemarks.Text, Convert.ToDouble(txtReceivedAmt.Text),
                    objCashPayment.txtVoucherId.Text,objCashPayment.dtpVoucherDate.Value,"D","","",objCashPayment.txtVoucherNo.Text,
                    objCashPayment.cbAccType.SelectedValue.ToString()});

                    objCashPayment.iSiNo = i;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                ((CashPayment)objCashPayment).GetBillDetails();
                ((CashPayment)objCashPayment).CaluculatingTotalRcvdAmt();

                MessageBox.Show("Details Added Sucessfully", "Cash Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //btnCancel_Click(null, null);
                this.Close();

            }
        }

        private bool CheckData()
        {
            bool flag = true;
            if (txtAccountSearch.Text.Length == 0)
            {
                MessageBox.Show("Enter Account Number!", "Cash Payment", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                flag = false;
                txtAccountSearch.Focus();
                return flag;
            }
            if (cbPaymentMode.SelectedIndex == 0)
            {
                MessageBox.Show("Select PaymentMode!", "Cash Payment", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                flag = false;
                cbPaymentMode.Focus();
                return flag;
            }

            if (txtChqNo.Text.Length == 0)
            {
                MessageBox.Show("Enter Reference number!", "Cash Payment", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                flag = false;
                txtChqNo.Focus();
                return flag;
            }
            if (Convert.ToDouble(txtReceivedAmt.Text.ToString()) <= 0)
            {
                MessageBox.Show("Enter Received Amount!", "Cash Payment", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                flag = false;
                txtReceivedAmt.Focus();
                return flag;
            }
            return flag;
        }

       
    }
}
