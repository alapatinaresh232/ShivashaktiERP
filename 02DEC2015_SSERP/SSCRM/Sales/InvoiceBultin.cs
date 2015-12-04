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

namespace SSCRM
{
    public partial class InvoiceBultin : Form
    {
        private SQLDB objSQLDB = null;
        private InvoiceDB objData = null;
        private string strFormerid = string.Empty;
        private int intInvoiceNo = 0;
        private string strDateOfBirth = string.Empty;
        private string strMarriageDate = string.Empty;
        public string strStateCode = string.Empty;
        private string strBranchCode = string.Empty;
        private string strECode = string.Empty;
        private bool blIsCellQty = true;
        private string strCreditSale = "NO";
        private bool IsModifyInvoice = false;
        private bool blEcodeLoad = false;
        private double intMainProductRemainder = 0;
        private int intCurrentRow = 0;
        private int intCurrentCell = 0;
        public InvoiceBultin()
        {
            InitializeComponent();
        }

        private void InvoiceBultin_Load(object sender, EventArgs e)
        {
            
            ClearForm();
            FillBranchData();
            FillEmployeeData();
            cbEcode.SelectedIndex = -1;
            gvProductDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);
            gvInvoice.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8,
                                                        System.Drawing.FontStyle.Bold);
            gvInvoice.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);
            dgvFreeProduct.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);

            dgvFreeProduct.DefaultCellStyle.BackColor = Color.LightGreen;
            dgvFreeProduct.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGreen;
            strFormerid = "99999";

            objData = new InvoiceDB();
            txtDocMonth.Text = CommonData.DocMonth;
            txtInvoiceNo.Text = objData.GenerateInvoiceNo(CommonData.CompanyCode, CommonData.BranchCode).ToString();
            objData = null;

            meInvoiceDate.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
            //cbBranch.Enabled = true;
            //cbBranch.Focus();
            txtOrderNo.Text = string.Empty;
            txtInvoiceNo.Text = GenerateNewInvoiceNo().ToString();
            strFormerid = "99999";
            FillEmployeeData();
            IsModifyInvoice = false;
            btnSave.Enabled = true;
            btnDelete.Enabled = false;

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        private Int64 GenerateNewInvoiceNo()
        {

            Int64 intNewInvNo = 0;
            try
            {

                objData = new InvoiceDB();
                intNewInvNo = objData.GenerateInvoiceNo(CommonData.CompanyCode, CommonData.BranchCode);
                objData = null;
            }
            catch (Exception ex)
            {
                intNewInvNo = 0;
                MessageBox.Show(ex.Message, "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return intNewInvNo;
        }
        private void FillBranchData()
        {
            //Master objAdmin = new Master();
            //DataSet dsBranch = null;
            //try
            //{
            //    dsBranch = objAdmin.GetBranchDataSet(CommonData.CompanyCode.ToString());
            //    DataTable dtBranch = dsBranch.Tables[0];
            //    if (dtBranch.Rows.Count > 0)
            //    {
            //        cbBranch.DataSource = dtBranch;
            //        cbBranch.DisplayMember = "branch_name";
            //        cbBranch.ValueMember = "branch_code";
            //    }

            //    cbBranch.SelectedValue = CommonData.BranchCode+'@'+CommonData.StateCode;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    if (cbBranch.SelectedIndex > -1)
            //    {
            //        cbBranch.SelectedIndex = 0;
            //        strBranchCode = ((System.Data.DataRowView)(cbBranch.SelectedItem)).Row.ItemArray[0].ToString();
            //        string[] strArr = strBranchCode.Split('@');
            //        strStateCode = strArr[1].ToString();
            //        strBranchCode = strArr[0].ToString();
            //    }
            //    cbBranch.SelectedValue = CommonData.BranchCode + '@' + CommonData.StateCode;
            //    objData = null;
            //    Cursor.Current = Cursors.Default;
            //}

        }

        private void FillEmployeeData()
        {
            objData = new InvoiceDB();
            DataSet dsEmp = null;
            try
            {
                dsEmp = objData.GetEcodeList(CommonData.CompanyCode, CommonData.BranchCode, txtDocMonth.Text);
                DataTable dtEmp = dsEmp.Tables[0];
                DataRow dr = dtEmp.NewRow();
                dr[0] = "0";
                dr[1] = "Select";
                dtEmp.Rows.InsertAt(dr, 0);
                dr = null;
                if (dtEmp.Rows.Count > 0)
                {
                    blEcodeLoad = true;
                    cbEcode.DataSource = dtEmp;
                    cbEcode.DisplayMember = "ENAME";
                    cbEcode.ValueMember = "ECODE";
                    blEcodeLoad = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bullet Ins", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                if (cbEcode.SelectedIndex > -1)
                {
                    cbEcode.SelectedIndex = 0;
                    strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
                }
                objData = null;
                Cursor.Current = Cursors.Default;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int intSaved = 0;
            try
            {

                if (CheckData() == true)
                {
                    if (SaveInvoiceHeadData() > 0)
                        intSaved = SaveInvoiceDeatailData();
                    if (dgvFreeProduct.Rows.Count > 0)
                        intSaved = SaveFreeDeatailData();
                    //SaveServiceActivityRecords();

                    if (intSaved > 0)
                    {
                        intInvoiceNo = 0;
                        txtInvAmt.Text = "0.00";
                        txtTotalPoints.Text = "0.00";
                        txtAdvanceAmt.Text = "0.00";
                        txtReceivedAmt.Text = "0.00";
                        txtBalAmt.Text = "0.00";
                        txtOrderNo.Text = string.Empty;
                        txtInvoiceNo.Text = GenerateNewInvoiceNo().ToString();
                        strFormerid = "99999";
                        FillInvoiceList();
                        IsModifyInvoice = false;
                        dgvFreeProduct.Rows.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Data not saved", "Bullet Ins", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bullet Ins", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {

                //cbBranch.Enabled = true;
                //cbBranch.SelectedIndex = 0;
            }

        }

        private void SaveServiceActivityRecords()
        {
            objSQLDB = new SQLDB();
            string sqlText = " exec SSCRM_SERVICE_TNA_AUD '" + CommonData.CompanyCode +
                                "', '" + CommonData.BranchCode +
                                "', '" + txtDocMonth.Text +
                                "', '" + txtOrderNo.Text +
                                "', " + txtInvoiceNo.Text;

            try
            {
                objSQLDB.ExecuteSaveData(sqlText);
            }
            catch //(Exception ex)
            {

            }
            finally
            {
                objSQLDB = null;
            }
        }
        private bool CheckData()
        {
            bool blValue = true;
            strCreditSale = "NO";

            if (Convert.ToString(txtOrderNo.Text).Length == 0)
            {
                MessageBox.Show("Enter Order number!", "Bullet Ins", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtOrderNo.Focus();
                return blValue;
            }
            
            if (Convert.ToString(txtInvoiceNo.Text).Length == 0 || Convert.ToString(txtInvoiceNo.Text) == "0")
            {
                MessageBox.Show("Enter Invoice number!", "Bullet Ins", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtInvoiceNo.Focus();
                return blValue;
            }

            if (Convert.ToDateTime(Convert.ToDateTime(meInvoiceDate.Value).ToString("dd/MM/yyyy")) > Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyyy")))
            {
                MessageBox.Show("Invoice date should be less than to day.");
                blValue = false;
                meInvoiceDate.Focus();
                return blValue;
            }
            //else if (Convert.ToDateTime(meInvoiceDate.Value) < Convert.ToDateTime(Convert.ToDateTime(CommonData.DocFDate).ToString("dd/MM/yyyy")) ||
            // Convert.ToDateTime(meInvoiceDate.Value) > Convert.ToDateTime(Convert.ToDateTime(CommonData.DocTDate).ToString("dd/MM/yyyy")))
            //{
            //    MessageBox.Show("Invoice date should be between " + CommonData.DocFDate + " and " + CommonData.DocTDate);
            //    blValue = false;
            //    meInvoiceDate.Focus();
            //    return blValue;
            //}

            Security objSecur = new Security();
            if (objSecur.CanModifyDataUserAsPerBackDays(Convert.ToDateTime(meInvoiceDate.Value)) == false)
            {
                MessageBox.Show("You cannot manipulate backdays data!\n If you want to modify, Contact to IT-Department", "Bullet Ins", MessageBoxButtons.OK, MessageBoxIcon.Information);
                blValue = false;
                txtOrderNo.Focus();
                return blValue;
            }
            objSecur = null;

            //if (cbEcode.SelectedIndex == -1)
            if ((strECode == "") || (strECode == "0"))
            {
                MessageBox.Show("Select Ecode!", "Bullet Ins", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                cbEcode.Focus();
                return blValue;
            }
            else
            {
                string sEcode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
                string sLockStatus = GetLockingStatus(sEcode, CommonData.DocMonth);
                if (sLockStatus == "Y")
                {
                    MessageBox.Show("Sales Entry for this GC/Gl is Locked, \nPlease Contact you manager for Information/Unlocking", "SSERP-SummaryBultn", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
                else if (sLockStatus == "")
                {
                    MessageBox.Show("First Enter Sales Summary Bulletin for this SR", "SSERP-SummaryBultn", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }

            }
            bool blInvDtl = false;
            for (int i = 0; i < gvProductDetails.Rows.Count; i++)
            {
                if (Convert.ToString(gvProductDetails.Rows[i].Cells["Rate"].Value) != "" && Convert.ToString(gvProductDetails.Rows[i].Cells["Qty"].Value) != "")
                {
                    blInvDtl = true;
                    break;
                }

            }

            if (blInvDtl == false)
            {
                blValue = false;
                MessageBox.Show("Enter product details", "Bullet Ins", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (txtInvAmt.Text.ToString() == "" || txtInvAmt.Text.ToString() == "0" || txtInvAmt.Text.ToString() == "0.00")
            {
                MessageBox.Show("Please check invoice amount!", "Bullet Ins", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                gvProductDetails.Focus();
                return blValue;
            }
            if (txtReceivedAmt.Text == "" || txtReceivedAmt.Text == "0" || txtReceivedAmt.Text == "0.00")
                txtReceivedAmt.Text = "0.00";
            if (txtBalAmt.Text == "" || txtBalAmt.Text == "0" || txtBalAmt.Text == "0.00")
                txtBalAmt.Text = "0.00";
            if (Convert.ToDouble(txtReceivedAmt.Text) == 0 || Convert.ToDouble(txtBalAmt.Text) > 0)
            {
                DialogResult result = MessageBox.Show("Do you want to save " + txtInvoiceNo.Text + " invoice as Credit Sale ?",
                                       "Bullet Ins", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    strCreditSale = "NO";
                    blValue = false;
                }
                else
                {
                    strCreditSale = "YES";
                    blValue = true;
                }

                return blValue;
            }


            if (txtAdvanceAmt.Text == "")
                txtAdvanceAmt.Text = "0";
            if (txtReceivedAmt.Text == "")
                txtReceivedAmt.Text = "0";

            if (Convert.ToDouble(txtInvAmt.Text) < Convert.ToDouble(txtReceivedAmt.Text) + Convert.ToDouble(txtAdvanceAmt.Text))
            {
                DialogResult result = MessageBox.Show("Invoice amount less than total recieved amount, Do you want to save ?",
                                       "Bullet Ins", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    blValue = true;
                }
                else
                {
                    blValue = false;
                }
            }

            if (CommonData.IsExistedPostMonthData == true && IsModifyInvoice == false)
            {
                MessageBox.Show("You cannot add new data", "Built Ins", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                blValue = false;
                txtInvoiceNo.Focus();
                return blValue;
            }

            //if (IsSaleOrder == false)
            //{
                objData = new InvoiceDB();
                DataTable dtOrderStatus = new DataTable();
                try
                {
                    dtOrderStatus = objData.Get_OrderSheetIssueDetails_ByOrderNo(CommonData.CompanyCode, CommonData.BranchCode, txtDocMonth.Text.ToUpper(), Convert.ToInt32(txtOrderNo.Text).ToString("00000")).Tables[0];
                }
                catch { return false; }
                if (dtOrderStatus.Rows.Count > 0)
                {
                    if (dtOrderStatus.Rows[0]["Status"].ToString() != "DOCUMENTED")
                    {
                        MessageBox.Show("Order Form " + txtOrderNo.Text.ToString() +
                                    " Status updated as " + dtOrderStatus.Rows[0]["Status"].ToString() +
                                    " in the Order Sheet Returns Screen", "SSERP :: Invoice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Order Form " + txtOrderNo.Text.ToString() +
                                    " Not Issued to any One in the Order Form Issue Screen", "SSERP :: Invoice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            //}

            return blValue;

        }
        private string GetLockingStatus(string sEcode, string sDocMonth)
        {
            string sStatus = "";
            objSQLDB = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                dt = objSQLDB.ExecuteDataSet("EXEC GetSRSummaryBulletinHeadData '','','" + sEcode + "','" + sDocMonth + "'").Tables[0];
                sStatus = dt.Rows[0]["LockStatus"].ToString();
            }
            catch
            {
                sStatus = "";
            }
            finally
            {
                objSQLDB = null;
                dt = null;
            }
            return sStatus;
        }
        private int SaveInvoiceHeadData()
        {
            objSQLDB = new SQLDB();
            string sqlText = "";
            int intRec = 0;
            txtAdvanceAmt_KeyUp(null, null);
            if (txtAdvanceAmt.Text == "")
                txtAdvanceAmt.Text = "0.00";
            if (txtReceivedAmt.Text == "")
                txtReceivedAmt.Text = "0.00";
            try
            {
                if (IsModifyInvoice == false)
                {
                    txtInvoiceNo.Text = GenerateNewInvoiceNo().ToString();
                    if (Convert.ToInt32(txtInvoiceNo.Text) != 0)
                    {
                        sqlText = " INSERT INTO SALES_INV_BULTIN_HEAD(sibh_company_code, sibh_state_code, sibh_branch_code"+
                            ", sibh_fin_year, sibh_invoice_number, sibh_invoice_date, sibh_farmer_ID, sibh_order_number"+
                            ", sibh_eora_code, sibh_prod_pattern, sibh_Document_month, sibh_created_Date, SIBH_CREATED_BY"+
                            ", SIBH_INVOICE_AMOUNT, SIBH_ADVANCE_AMOUNT, SIBH_RECEIVED_AMOUNT, SIBH_CREDIT_SALE, SIBH_ORDER_DATE)" +
                             " SELECT '" + CommonData.CompanyCode + "', '" + CommonData.StateCode + "', '" + CommonData.BranchCode + 
                             "' , '" + CommonData.FinancialYear + "'," + txtInvoiceNo.Text + ", '" + Convert.ToDateTime(meInvoiceDate.Value).ToString("dd/MMM/yyy") + "' " +
                             ", '99999', '" + Convert.ToInt32(txtOrderNo.Text).ToString("00000") + "', '" + strECode + "', 'Ppattern', '" + CommonData.DocMonth +
                             "', '" + CommonData.CurrentDate + "','" + CommonData.LogUserId +
                             "', " + txtInvAmt.Text + "," + txtAdvanceAmt.Text +
                             ", " + txtReceivedAmt.Text + ", '" + strCreditSale +
                             "','" + Convert.ToDateTime(dtpOrderDate.Value).ToString("dd/MMM/yyy") + 
                             "' WHERE NOT EXISTS (SELECT sibh_order_number FROM SALES_INV_BULTIN_HEAD" +
                             " WHERE sibh_branch_code='" + CommonData.BranchCode +
                             "' AND sibh_fin_year='" + CommonData.FinancialYear +
                             "' AND sibh_order_number='" + Convert.ToInt32(txtOrderNo.Text).ToString("00000") + "')";
                    }
                }
                else
                {
                    sqlText = " DELETE from Sales_Inv_Detl_FREE" +
                                   " WHERE sidf_company_code='" + CommonData.CompanyCode +
                                   "' AND sidf_branch_code='" + CommonData.BranchCode +
                                   "' AND sidf_invoice_number=" + txtInvoiceNo.Text +
                                   "  AND sidf_fin_year='" + CommonData.FinancialYear + "'";

                    sqlText += " DELETE from SALES_INV_BULTIN_DETL" +
                                 " WHERE sibd_company_code='" + CommonData.CompanyCode +
                                     "' AND sibd_branch_code='" + CommonData.BranchCode +
                                     "' AND sibd_invoice_number=" + txtInvoiceNo.Text +
                                     "  AND sibd_fin_year='" + CommonData.FinancialYear + "'";

                    //intRec = objSQLDB.ExecuteSaveData(sqlDelete);

                    sqlText += " UPDATE SALES_INV_BULTIN_HEAD set sibh_invoice_date='" + Convert.ToDateTime(meInvoiceDate.Value).ToString("dd/MMM/yyy") +
                                "', sibh_farmer_ID='99999'" +
                                ", sibh_eora_code='" + strECode +
                                "', sibh_prod_pattern='Ppattern'" +
                                ", sibh_Document_month='" + txtDocMonth.Text +
                                "', SIBH_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                                "', SIBH_LAST_MODIFIED_DATE='" + CommonData.CurrentDate +
                                "', sibh_order_number='" + Convert.ToInt32(txtOrderNo.Text).ToString("00000") +
                                "', SIBH_INVOICE_AMOUNT =" + txtInvAmt.Text +
                                ", SIBH_ADVANCE_AMOUNT = " + txtAdvanceAmt.Text +
                                ", SIBH_RECEIVED_AMOUNT = " + txtReceivedAmt.Text +
                                ", SIBH_CREDIT_SALE = '" + strCreditSale +
                                "', SIBH_ORDER_DATE = '" + Convert.ToDateTime(dtpOrderDate.Value).ToString("dd/MMM/yyy") +
                                "' WHERE sibh_invoice_number = " + txtInvoiceNo.Text +
                                "  AND sibh_branch_code='" + CommonData.BranchCode +
                                "' AND sibh_fin_year='" + CommonData.FinancialYear.ToString() +
                                "' AND sibh_company_code='" + CommonData.CompanyCode.ToString() + "'";


                }
                intRec = 0;
                if (sqlText.Length > 10)
                {
                    objSQLDB = new SQLDB();
                    intRec = objSQLDB.ExecuteSaveData(sqlText);
                 
                }

            }
            catch (Exception ex)
            {
                intRec = 0;
                MessageBox.Show(ex.Message, "Bullet Ins", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objSQLDB = null;

            }
            return intRec;
        }

        private int SaveInvoiceDeatailData()
        {
            
            string sqlText = "";
            //string sqlDelete = "";
            int intRec = 0;
            objSQLDB = new SQLDB();
            try
            {

                sqlText = " DELETE from SALES_INV_BULTIN_DETL" +
                                " WHERE sibd_company_code='" + CommonData.CompanyCode +
                                    "' AND sibd_branch_code='" + CommonData.BranchCode +
                                    "' AND sibd_invoice_number=" + txtInvoiceNo.Text +
                                    "  AND sibd_fin_year='" + CommonData.FinancialYear + "'";
                //try
                //{
                //    intRec = objSQLDB.ExecuteSaveData(sqlDelete);
                //}
                //catch
                //{

                //}
                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {
                    try { Convert.ToDouble(gvProductDetails.Rows[i].Cells["Qty"].Value.ToString()); }
                    catch { gvProductDetails.Rows[i].Cells["Qty"].Value = "0"; }
                    try { Convert.ToDouble(gvProductDetails.Rows[i].Cells["Rate"].Value.ToString()); }
                    catch { gvProductDetails.Rows[i].Cells["Rate"].Value = "0"; }
                    try { Convert.ToDouble(gvProductDetails.Rows[i].Cells["Qty"].Value.ToString()); }
                    catch { gvProductDetails.Rows[i].Cells["Qty"].Value = "0"; }
                    try { Convert.ToDouble(gvProductDetails.Rows[i].Cells["Amount"].Value.ToString()); }
                    catch { gvProductDetails.Rows[i].Cells["Amount"].Value = "0"; }
                    try { Convert.ToDouble(gvProductDetails.Rows[i].Cells["Points"].Value.ToString()); }
                    catch { gvProductDetails.Rows[i].Cells["Points"].Value = "0"; }
                    try { Convert.ToDouble(gvProductDetails.Rows[i].Cells["DBPoints"].Value.ToString()); }
                    catch { gvProductDetails.Rows[i].Cells["DBPoints"].Value = "0"; }

                    if (gvProductDetails.Rows[i].Cells["Rate"].Value.ToString() != "" && Convert.ToDouble(gvProductDetails.Rows[i].Cells["Qty"].Value.ToString()) >0)
                    {
                        sqlText += " INSERT INTO SALES_INV_BULTIN_DETL(sibd_company_code, sibd_state_code, sibd_branch_code" +
                                    ", sibd_invoice_number,sibd_fin_year, sibd_invoice_sl_no, sibd_state, sibd_product_id" +
                                    ", sibd_price, sibd_qty, sibd_amount, SIBD_PRODUCT_POINTS, SIBD_UNIT_POINTS)" +
                                    " VALUES('" + CommonData.CompanyCode + "', '" + CommonData.StateCode + "', '" + CommonData.BranchCode +
                                    "', " + txtInvoiceNo.Text + ", '" + CommonData.FinancialYear + "'," + gvProductDetails.Rows[i].Cells["SLNO"].Value.ToString() +
                                    ", '" + CommonData.StateName + "', '" + gvProductDetails.Rows[i].Cells["ProductID"].Value.ToString().Trim() +
                                    "', " + Convert.ToDouble(gvProductDetails.Rows[i].Cells["Rate"].Value.ToString()) +
                                    ", " + Convert.ToDouble(gvProductDetails.Rows[i].Cells["Qty"].Value.ToString()) +
                                    ", " + Convert.ToDouble(gvProductDetails.Rows[i].Cells["Amount"].Value.ToString()) +
                                    ", " + Convert.ToDouble(gvProductDetails.Rows[i].Cells["Points"].Value.ToString()) +
                                    ", " + Convert.ToDouble(gvProductDetails.Rows[i].Cells["DBPoints"].Value.ToString()) + 
                                    "); ";

                    }

                }

                intRec = 0;
                if (sqlText.Length > 10)
                    intRec = objSQLDB.ExecuteSaveData(sqlText);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bullet Ins", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objSQLDB = null;
            }
            return intRec;

        }

        private void gvProductDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            objData = new InvoiceDB();
            if (e.ColumnIndex == 5)
            {
                try { Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value.ToString()); }
                catch { gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value = "0"; }
                try { Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value.ToString()); }
                catch { gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value = "0"; }
                try { Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["DBPoints"].Value.ToString()); }
                catch { gvProductDetails.Rows[e.RowIndex].Cells["DBPoints"].Value = "0"; }

                if (Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value.ToString()) != "")
                {
                    string[] strRatePoints = objData.getProductRatePoints(gvProductDetails.Rows[e.RowIndex].Cells["ProductId"].Value.ToString(), Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value.ToString()), Convert.ToDateTime(meInvoiceDate.Value.ToString())).Split('@');
                    if (strRatePoints[0] != "")
                    {
                        if (Convert.ToDouble(strRatePoints[0]) > 0)
                            gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value = Convert.ToDouble(strRatePoints[0]).ToString("f");

                        if (Convert.ToDouble(strRatePoints[1]) > 0)
                            gvProductDetails.Rows[e.RowIndex].Cells["DBPoints"].Value = Convert.ToDouble(strRatePoints[1]).ToString("f");
                    }
                    objData = null;

                    if (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value) >= 0 && Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value) >= 0)
                    {
                        gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) * (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value));
                        gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value).ToString("f");
                        gvProductDetails.Rows[e.RowIndex].Cells["Points"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) * (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["DBPoints"].Value));
                        gvProductDetails.Rows[e.RowIndex].Cells["Points"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Points"].Value).ToString("f");
                    }
                    FillFreeProductToGrid(gvProductDetails.Rows[e.RowIndex].Cells["Brand"].Value.ToString(), gvProductDetails.Rows[e.RowIndex].Cells["ProductId"].Value.ToString(), gvProductDetails.Rows[e.RowIndex].Cells["MainProduct"].Value.ToString(), Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value), Convert.ToInt16(gvProductDetails.Rows[e.RowIndex].Cells["Slno"].Value));
                }
                else
                    gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value = "0";
            }
            if (e.ColumnIndex == 6)
            {
                if (gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value != gvProductDetails.Rows[e.RowIndex].Cells["DBRate"].Value)
                {
                    if (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value) < Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["DBRate"].Value))
                    {
                        DialogResult result = MessageBox.Show("Rate is Below MRP " + gvProductDetails.Rows[e.RowIndex].Cells["DBRate"].Value + " Rs/-\nDo You Want to Continue with " + gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value + " Rs/-?",
                                               "Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.No)
                        {
                            gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value = gvProductDetails.Rows[e.RowIndex].Cells["DBRate"].Value;
                            return;
                        }
                    }
                    else if (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value) > Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["DBRate"].Value))
                    {
                        DialogResult result = MessageBox.Show("Rate is Above MRP " + gvProductDetails.Rows[e.RowIndex].Cells["DBRate"].Value + " Rs/-\nDo You Want to Continue with " + gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value + " Rs/-?",
                                               "Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.No)
                        {
                            gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value = gvProductDetails.Rows[e.RowIndex].Cells["DBRate"].Value;
                            return;
                        }
                    }
                }
                if (Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value) != "")
                {
                    if (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value) >= 0 && Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value) >= 0)
                    {
                        gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) * (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value));
                        gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value).ToString("f");
                        gvProductDetails.Rows[e.RowIndex].Cells["Points"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value) * (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["DBPoints"].Value));
                        gvProductDetails.Rows[e.RowIndex].Cells["Points"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Points"].Value).ToString("f");
                    }
                }
                else
                    gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value = "0.00";
            }
            if (e.ColumnIndex == 7)
            {
                if ((Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value) == "") || (Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value) == "0"))
                {
                    gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value = string.Empty;
                }


            }
            if (e.ColumnIndex == 8)
            {
                if ((Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value) != "") && Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["DBPoints"].Value) != "")
                {
                    gvProductDetails.Rows[e.RowIndex].Cells["Points"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value) * (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["DBPoints"].Value));
                    gvProductDetails.Rows[e.RowIndex].Cells["Points"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Points"].Value).ToString("f");
                }


            }
            txtInvAmt.Text = GetInvoiceAmt().ToString("f");

            if (txtAdvanceAmt.Text != "0.00" || txtAdvanceAmt.Text != "")
                txtReceivedAmt.Text = Convert.ToDouble(Convert.ToDouble(txtInvAmt.Text) - Convert.ToDouble(txtAdvanceAmt.Text)).ToString("f");
            else
                txtReceivedAmt.Text = txtInvAmt.Text;

            TotalMainProducts();
        }

        private int SaveFreeDeatailData()
        {
            objSQLDB = new SQLDB();
            string sqlText = "";
            int intRec = 0;
            try
            {
                for (int i = 0; i < dgvFreeProduct.Rows.Count; i++)
                {
                    if (dgvFreeProduct.Rows[i].Cells["FreeQty"].Value.ToString() != "")
                    {
                        if (dgvFreeProduct.Rows[i].Cells["OfferNumber"].Value.ToString() == "")
                            dgvFreeProduct.Rows[i].Cells["OfferNumber"].Value = 0;

                        sqlText += " INSERT INTO Sales_Inv_Detl_FREE(sidf_company_code, sidf_state_code, sidf_branch_code, " +
                                    "sidf_invoice_number,sidf_fin_year, sidf_invoice_sl_no, sidf_product_id, " +
                                    "SIDF_INVOICE_PRODUCT_ID, SIDF_OFFER_NUMBER, sidf_given_qty1, sidf_given_ecode1, " +
                                    "sidf_given_date1, SIDF_SL_NO)" +
                                    " VALUES ('" + CommonData.CompanyCode + "', '" + CommonData.StateCode +
                                    "', '" + CommonData.BranchCode + "', " + txtInvoiceNo.Text +
                                    ", '" + CommonData.FinancialYear + "'," + dgvFreeProduct.Rows[i].Cells["MainProductIdSLno"].Value +
                                    ", '" + dgvFreeProduct.Rows[i].Cells["FreeProductID"].Value.ToString().Trim() +
                                    "', '" + dgvFreeProduct.Rows[i].Cells["MainProductId"].Value.ToString().Trim() +
                                    "'," + dgvFreeProduct.Rows[i].Cells["OfferNumber"].Value + ", " + dgvFreeProduct.Rows[i].Cells["FreeQty"].Value +
                                    ", " + strECode + ", getdate(), " + dgvFreeProduct.Rows[i].Cells["FreeSlNo"].Value + ") ";

                    }

                }
                intRec = 0;
                if (sqlText.Length > 10)
                    intRec = objSQLDB.ExecuteSaveData(sqlText);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objSQLDB = null;
            }
            return intRec;

        }
        private void FillFreeProductToGrid(string sProdCat, string sProdId, string sMainProd, double intQty, Int16 intInvDetlSlno)
        {
            short remainder = 0;
            bool isItemExisted = false;
            objData = new InvoiceDB();
            DataTable dt = new DataTable();
            int totRows = dgvFreeProduct.Rows.Count;
            for (int nRow = 0; nRow < totRows; nRow++)
            {

                if (dgvFreeProduct.Rows[nRow].Cells["MainProductId"].Value.ToString().Trim() == sProdId.ToString().Trim())
                {
                    dgvFreeProduct.Rows.RemoveAt(nRow);
                    totRows = totRows - 1;
                    nRow = nRow - 1;
                }
                else
                {

                }

            }

            if (isItemExisted == false)
            {
                double OfferSoldUnits = 0;
                dt = objData.InvFreeProduct_Get(sProdId, intQty);
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0]["OFFER_FROM_DATE"]).ToString()) <= Convert.ToDateTime(meInvoiceDate.Value.ToString()) && dt.Rows[0]["OFFER_TO_DATE"].ToString() == "")
                    {
                        intMainProductRemainder = intQty;
                        
                        foreach (DataRow dr in dt.Rows)
                        {
                            OfferSoldUnits = Convert.ToDouble(dr["SOLD_UNITS"].ToString());
                            if (Convert.ToDouble(dr["SOLD_UNITS"].ToString()) <= intQty && intMainProductRemainder >= 0)
                            {
                                dgvFreeProduct.Rows.Add();
                                int intCurRow = dgvFreeProduct.Rows.Count - 1;
                                dgvFreeProduct.Rows[intCurRow].Cells["FreeSlNo"].Value = intCurRow + 1;
                                dgvFreeProduct.Rows[intCurRow].Cells["MainProductIdSlNo"].Value = intInvDetlSlno;
                                dgvFreeProduct.Rows[intCurRow].Cells["MainProductId"].Value = sProdId;
                                dgvFreeProduct.Rows[intCurRow].Cells["FreeBrand"].Value = dr["CATEGORY_NAME"] + ""; ;
                                dgvFreeProduct.Rows[intCurRow].Cells["FreeMainProduct"].Value = sMainProd;
                                dgvFreeProduct.Rows[intCurRow].Cells["OfferNumber"].Value = dr["OFFER_NUMBER"] + "";
                                dgvFreeProduct.Rows[intCurRow].Cells["FreeProduct"].Value = dr["product_name"] + "";
                                dgvFreeProduct.Rows[intCurRow].Cells["SoldQty"].Value = dr["SOLD_UNITS"].ToString();
                                dgvFreeProduct.Rows[intCurRow].Cells["FreeQty"].Value = CaliculateFreeProduct(intQty, Convert.ToDouble(dr["FREE_UNITS"].ToString()), Convert.ToDouble(dr["SOLD_UNITS"].ToString()));
                                dgvFreeProduct.Rows[intCurRow].Cells["FreeProductId"].Value = dr["FREE_PRODUCTID"];
                            }
                        } 
                    }
                }                
                else
                {
                    OfferSoldUnits = intQty;
                }
                if (OfferSoldUnits == 0)
                    remainder = 0;
                else
                    remainder = Convert.ToInt16(intQty % OfferSoldUnits);
                while (remainder.ToString() != "0")
                {
                    dt.Clear();
                    //int OfferSoldUnits = 0;
                    //if (intQty % OfferSoldUnits != "0")
                        dt = objData.InvFreeProduct_Get(sProdId, remainder);
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0]["OFFER_FROM_DATE"]).ToString()) <= Convert.ToDateTime(meInvoiceDate.Value.ToString()) && dt.Rows[0]["OFFER_TO_DATE"].ToString() == "")
                        {
                            intMainProductRemainder = remainder;
                            Boolean FreeProductExist = false;
                            foreach (DataRow dr in dt.Rows)
                            {
                                int dgvFreeProductCount = dgvFreeProduct.Rows.Count;
                                for (int intCurRow = 0; intCurRow < dgvFreeProductCount; intCurRow++)
                                {
                                    if (Convert.ToString(dgvFreeProduct.Rows[intCurRow].Cells["FreeProductId"].Value) == Convert.ToString(dr["FREE_PRODUCTID"]) && Convert.ToInt16(dgvFreeProduct.Rows[intCurRow].Cells["MainProductIdSlNo"].Value) == Convert.ToInt16(intInvDetlSlno))
                                    {

                                        //dgvFreeProduct.Rows[intCurRow].Cells["FreeQty"].Value = Convert.ToInt16(dgvFreeProduct.Rows[intCurRow].Cells["FreeQty"].Value) + CaliculateFreeProduct(remainder, Convert.ToInt16(dr["FREE_UNITS"].ToString()), Convert.ToInt16(dr["SOLD_UNITS"].ToString()));
                                        OfferSoldUnits = Convert.ToInt16(dr["SOLD_UNITS"]);
                                        FreeProductExist = true;
                                    }
                                }
                                if (FreeProductExist == false)
                                {
                                    OfferSoldUnits = Convert.ToInt16(dr["SOLD_UNITS"]);
                                        if (Convert.ToInt16(dr["SOLD_UNITS"].ToString()) <= remainder && intMainProductRemainder >= 0)
                                        {
                                            dgvFreeProduct.Rows.Add();
                                            int intCurRow = dgvFreeProduct.Rows.Count - 1;
                                            dgvFreeProduct.Rows[intCurRow].Cells["FreeSlNo"].Value = intCurRow + 1;
                                            dgvFreeProduct.Rows[intCurRow].Cells["MainProductIdSlNo"].Value = intInvDetlSlno;
                                            dgvFreeProduct.Rows[intCurRow].Cells["MainProductId"].Value = sProdId;
                                            dgvFreeProduct.Rows[intCurRow].Cells["FreeBrand"].Value = dr["CATEGORY_NAME"] + ""; ;
                                            dgvFreeProduct.Rows[intCurRow].Cells["FreeMainProduct"].Value = sMainProd;
                                            dgvFreeProduct.Rows[intCurRow].Cells["OfferNumber"].Value = dr["OFFER_NUMBER"] + "";
                                            dgvFreeProduct.Rows[intCurRow].Cells["FreeProduct"].Value = dr["product_name"] + "";
                                            dgvFreeProduct.Rows[intCurRow].Cells["SoldQty"].Value = dr["SOLD_UNITS"].ToString();
                                            dgvFreeProduct.Rows[intCurRow].Cells["FreeQty"].Value = CaliculateFreeProduct(remainder, Convert.ToInt16(dr["FREE_UNITS"].ToString()), Convert.ToInt16(dr["SOLD_UNITS"].ToString()));
                                            dgvFreeProduct.Rows[intCurRow].Cells["FreeProductId"].Value = dr["FREE_PRODUCTID"];
                                        
                                    }
                                }
                                FreeProductExist = false;
                            }
                        }
                    }
                    else
                    {
                        OfferSoldUnits = remainder;
                    }
                    remainder = Convert.ToInt16(remainder % OfferSoldUnits);
                }
            }
            SetFreeGridSlNo();
            TotalFreeProducts();
        }

        private void FillInvoiceFreeProduct()
        {
            //bool isItemExisted = false;
            objData = new InvoiceDB();
            DataTable dt = new DataTable();
            dt = objData.InvoiceBltnDetailFreeItems_Get(Convert.ToInt32(txtInvoiceNo.Text));
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dgvFreeProduct.Rows.Add();
                    int intCurRow = dgvFreeProduct.Rows.Count - 1;
                    dgvFreeProduct.Rows[intCurRow].Cells["FreeSlNo"].Value = dr["free_sl_no"] + "";
                    dgvFreeProduct.Rows[intCurRow].Cells["MainProductIdSlNo"].Value = dr["InvDetlSlno"] + "";
                    dgvFreeProduct.Rows[intCurRow].Cells["MainProductId"].Value = dr["main_product_id"] + "";
                    dgvFreeProduct.Rows[intCurRow].Cells["FreeBrand"].Value = dr["category_name"] + "";
                    dgvFreeProduct.Rows[intCurRow].Cells["FreeMainProduct"].Value = dr["MainProduct"] + "";
                    dgvFreeProduct.Rows[intCurRow].Cells["OfferNumber"].Value = dr["OFFER_NUMBER"] + "";
                    dgvFreeProduct.Rows[intCurRow].Cells["FreeProduct"].Value = dr["Free_product_name"] + "";
                    dgvFreeProduct.Rows[intCurRow].Cells["SoldQty"].Value = dr["SOLD_UNITS"] + "";
                    dgvFreeProduct.Rows[intCurRow].Cells["FreeQty"].Value = dr["qty"] + "";
                    dgvFreeProduct.Rows[intCurRow].Cells["FreeProductId"].Value = dr["free_product_id"];
                }

            }
            TotalFreeProducts();
        }

        private void SetFreeGridSlNo()
        {
            for (int nRow = 0; nRow < dgvFreeProduct.Rows.Count; nRow++)
            {
                dgvFreeProduct.Rows[nRow].Cells["FreeSlNo"].Value = nRow + 1;
            }
        }

        private void txtInvoiceNo_Validated(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(Convert.ToString(txtInvoiceNo.Text + "0")) > 0)
                {
                    int intInvNo = Convert.ToInt32(Convert.ToString(txtInvoiceNo.Text));
                    ClearForm();
                    txtInvoiceNo.Text = intInvNo.ToString();
                    FillInvoiceData(Convert.ToInt32(txtInvoiceNo.Text), "");

                    objData = new InvoiceDB();
                    Int64 invNo = objData.CheckInvoiceNoForBuiltIn(CommonData.StateCode, CommonData.BranchCode, Convert.ToInt32(txtInvoiceNo.Text), "");
                    if (invNo > 0)
                    {
                        btnSave.Enabled = false;
                        btnDelete.Enabled = false;
                        MessageBox.Show(txtInvoiceNo.Text.ToString() + " number already in Invoices");
                    }
                    objData = null;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Bullet Ins", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            if (blEcodeLoad == false)
                FillInvoiceList();
        }

        private void FillInvoiceData(int nInvNo, string sOrdNo)
        {
            Hashtable ht = new Hashtable();
            DataTable dtInvH;
            DataTable dtInvD;
            DataSet ds = new DataSet();
            try
            {
                objData = new InvoiceDB();
                ht = objData.InvoiceBulitin_Get(CommonData.StateCode, CommonData.BranchCode, nInvNo, sOrdNo);
                dtInvH = (DataTable)ht["InvHead"];
                dtInvD = (DataTable)ht["InvDetail"];
                if (dtInvH.Rows.Count == 0)
                {
                    IsModifyInvoice = false;
                    ds = objData.SalesOrderForBuiltInAndInvoice_Get(txtOrderNo.Text.ToString().Trim());
                    dtInvH = ds.Tables[0];
                    dtInvD = ds.Tables[1];
                }
                else
                {
                    IsModifyInvoice = true;
                    //btnSave.Enabled = true;
                    //btnDelete.Enabled = true;

                }
                ds = null;

                if (dtInvH.Rows.Count > 0)
                {

                    if (dtInvH.Rows[0]["invoice_number"] + "" != "")
                    {
                        intInvoiceNo = Convert.ToInt32(dtInvH.Rows[0]["invoice_number"]);
                        txtInvoiceNo.Text = dtInvH.Rows[0]["invoice_number"] + "";
                        meInvoiceDate.Value = Convert.ToDateTime(dtInvH.Rows[0]["invoice_date"]);
                    }
                    if (IsModifyInvoice == true)
                        txtDocMonth.Text = dtInvH.Rows[0]["document_month"] + "";
                    else
                        txtDocMonth.Text = CommonData.DocMonth.ToUpper();
                    strFormerid = Convert.ToString(dtInvH.Rows[0]["farmer_ID"] + "");
                    dtpOrderDate.Value = Convert.ToDateTime(dtInvH.Rows[0]["order_date"]);
                    txtEcodeSearch.Text = dtInvH.Rows[0]["eora_code"] + "";
                    txtEcodeSearch_KeyUp(null, null);
                    strECode = dtInvH.Rows[0]["eora_code"] + "";
                    cbEcode.SelectedValue = strECode;
                    txtOrderNo.Text = dtInvH.Rows[0]["order_number"] + "";
                    if (IsModifyInvoice == true)
                        txtDocMonth.Text = dtInvH.Rows[0]["document_month"] + "";
                    else
                        txtDocMonth.Text = CommonData.DocMonth.ToUpper();
                    if (dtInvH.Rows[0]["INVOICE_AMOUNT"] + "" == "")
                        txtInvAmt.Text = "0.00";
                    else
                        txtInvAmt.Text = dtInvH.Rows[0]["INVOICE_AMOUNT"] + "";

                    if (dtInvH.Rows[0]["ADVANCE_AMOUNT"] + "" == "")
                        txtAdvanceAmt.Text = "0.00";
                    else
                        txtAdvanceAmt.Text = dtInvH.Rows[0]["ADVANCE_AMOUNT"] + "";

                    if (dtInvH.Rows[0]["RECEIVED_AMOUNT"] + "" == "")
                        txtReceivedAmt.Text = "0.00";
                    else
                        txtReceivedAmt.Text = dtInvH.Rows[0]["RECEIVED_AMOUNT"] + "";

                    txtBalAmt.Text = Convert.ToDouble(Convert.ToDouble(txtInvAmt.Text) - (Convert.ToDouble(txtAdvanceAmt.Text) + Convert.ToDouble(txtReceivedAmt.Text))).ToString("f"); ;

                    if (dtInvH.Rows[0]["CREDIT_SALE"] + "" == "YES")
                        lblCreditSale.Visible = true;
                    else
                        lblCreditSale.Visible = false;

                    //SaveDeleteEnableDisable();
                    FillInvoiceDetail(dtInvD);
                    FillInvoiceFreeProduct();
                }
                else
                {
                    txtInvoiceNo.Text = GenerateNewInvoiceNo().ToString();
                    txtDocMonth.Text = CommonData.DocMonth.ToUpper();
                }


            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message, "Bullet Ins", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objData = null;
            }
        }

        private void SaveDeleteEnableDisable()
        {
            try
            {
                btnSave.Enabled = true;
                //btnDelete.Enabled = true;
                if (Convert.ToDateTime(Convert.ToDateTime(meInvoiceDate.Value).ToString("dd/MM/yyyy")) < Convert.ToDateTime(CommonData.DocFDate) ||
                        Convert.ToDateTime(Convert.ToDateTime(meInvoiceDate.Value).ToString("dd/MM/yyyy")) > Convert.ToDateTime(CommonData.DocTDate))
                {
                    btnSave.Enabled = false;
                    btnDelete.Enabled = false;
                }
                else
                {
                    btnSave.Enabled = true;
                    //btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bullet Ins", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objData = null;
            }

        }

        private void FillInvoiceDetail(DataTable dt)
        {
            try
            {
                int intRow = 1;
                gvProductDetails.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToDouble(dt.Rows[i]["amount"]) > 0)
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        if (dt.Rows[i]["invoice_sl_no"] + "" != "0")
                            cellSLNO.Value = dt.Rows[i]["invoice_sl_no"];
                        else
                            cellSLNO.Value = intRow;
                        tempRow.Cells.Add(cellSLNO);

                        DataGridViewCell cellMainProductID = new DataGridViewTextBoxCell();
                        cellMainProductID.Value = dt.Rows[i]["product_id"];
                        tempRow.Cells.Add(cellMainProductID);

                        DataGridViewCell cellMainProduct = new DataGridViewTextBoxCell();
                        cellMainProduct.Value = dt.Rows[i]["pm_product_name"];
                        tempRow.Cells.Add(cellMainProduct);

                        DataGridViewCell cellSubProduct = new DataGridViewTextBoxCell();
                        cellSubProduct.Value = dt.Rows[i]["category_name"];
                        tempRow.Cells.Add(cellSubProduct);

                        DataGridViewCell cellDessc = new DataGridViewTextBoxCell();
                        cellDessc.Value = dt.Rows[i]["pm_product_name"];
                        tempRow.Cells.Add(cellDessc);

                        DataGridViewCell cellQTY = new DataGridViewTextBoxCell();
                        if (Convert.ToDouble(dt.Rows[i]["qty"]) > 0)
                            cellQTY.Value = Convert.ToDouble(dt.Rows[i]["qty"]).ToString("f");
                        else
                            cellQTY.Value = "";

                        tempRow.Cells.Add(cellQTY);

                        DataGridViewCell cellRate = new DataGridViewTextBoxCell();
                        if (Convert.ToDouble(dt.Rows[i]["qty"]) > 0)
                            cellRate.Value = dt.Rows[i]["price"];
                        else
                            cellRate.Value = dt.Rows[i]["TIP_Rate"];

                        if (Convert.ToDouble(cellRate.Value) == 0)
                            cellRate.Value = dt.Rows[i]["TIP_Rate"];

                        tempRow.Cells.Add(cellRate);

                        DataGridViewCell cellAmt = new DataGridViewTextBoxCell();
                        if (Convert.ToDouble(dt.Rows[i]["amount"]) > 0)
                            cellAmt.Value = dt.Rows[i]["amount"];
                        else
                            cellAmt.Value = "";

                        tempRow.Cells.Add(cellAmt);

                        DataGridViewCell cellDBPoints = new DataGridViewTextBoxCell();
                        cellDBPoints.Value = Convert.ToDouble(dt.Rows[i]["TIPPoints"]).ToString("f");
                        tempRow.Cells.Add(cellDBPoints);

                        DataGridViewCell cellPoints = new DataGridViewTextBoxCell();
                        if (Convert.ToInt32(dt.Rows[i]["SIDPoints"]) > 0)
                            cellPoints.Value = Convert.ToDouble(dt.Rows[i]["SIDPoints"]).ToString("f");
                        else
                            cellPoints.Value = Convert.ToDouble(dt.Rows[i]["TIPPoints"]).ToString("f");

                        if (cellPoints.Value.ToString() == "")
                            cellPoints.Value = Convert.ToDouble(dt.Rows[i]["TIPPoints"]).ToString("f");

                        tempRow.Cells.Add(cellPoints);

                        DataGridViewCell cellDBRate = new DataGridViewTextBoxCell();
                        cellDBRate.Value = cellRate;
                        tempRow.Cells.Add(cellDBRate);

                        

                        if (dt.Rows[i]["ProductType"].ToString() == "Combi")
                            tempRow.Cells[2].Style.BackColor = Color.FromArgb(192, 192, 255);
                        else if (dt.Rows[i]["ProductType"].ToString() == "Free")   //Free
                            tempRow.Cells[2].Style.BackColor = Color.MediumTurquoise;

                        intRow = intRow + 1;
                        gvProductDetails.Rows.Add(tempRow);
                    }
                    GetTotalPoints();
                    TotalMainProducts();
                    TotalFreeProducts();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bullet Ins", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
            }
        }

        private void FillInvoiceList()
        {
            objData = new InvoiceDB();

            try
            {
                gvInvoice.Rows.Clear();
                txtInvTotAmt.Text = "0.00";
                txtRecTotAmt.Text = "0.00";
                txtAdvTotAmt.Text = "0.00";
                txtBalTotAmt.Text = "0.00";
                txtInvTotPoints.Text = "0.00";
                if (cbEcode.SelectedIndex > -1)
                {
                    DataTable dt = objData.InvoiceBuiltinList_Get(Convert.ToInt32(strECode),txtDocMonth.Text).Tables[0];
                    txtNumberOFInvoice.Text = dt.Rows.Count.ToString();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataGridViewRow tempRow = new DataGridViewRow();
                            DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                            cellSLNO.Value = gvInvoice.Rows.Count + 1;
                            tempRow.Cells.Add(cellSLNO);

                            DataGridViewCell cellOrdNo = new DataGridViewTextBoxCell();
                            cellOrdNo.Value = dt.Rows[i]["sibh_order_number"];
                            tempRow.Cells.Add(cellOrdNo);

                            DataGridViewCell cellInvNo = new DataGridViewTextBoxCell();
                            cellInvNo.Value = dt.Rows[i]["sibh_invoice_number"];
                            tempRow.Cells.Add(cellInvNo);

                            DataGridViewCell cellInvDate = new DataGridViewTextBoxCell();
                            cellInvDate.Value = Convert.ToDateTime(dt.Rows[i]["sibh_invoice_date"]).ToString("dd/MMM/yyyy");
                            tempRow.Cells.Add(cellInvDate);

                            DataGridViewCell cellInvAmt = new DataGridViewTextBoxCell();
                            cellInvAmt.Value = dt.Rows[i]["sibh_invoice_amount"];
                            txtInvTotAmt.Text = Convert.ToDouble(Convert.ToDouble(txtInvTotAmt.Text) + Convert.ToDouble(cellInvAmt.Value)).ToString("f");
                            tempRow.Cells.Add(cellInvAmt);

                            DataGridViewCell cellInvAdvAmt = new DataGridViewTextBoxCell();
                            cellInvAdvAmt.Value = dt.Rows[i]["sibh_advance_amount"];
                            txtAdvTotAmt.Text = Convert.ToDouble(Convert.ToDouble(txtAdvTotAmt.Text) + Convert.ToDouble(cellInvAdvAmt.Value)).ToString("f");
                            tempRow.Cells.Add(cellInvAdvAmt);

                            DataGridViewCell cellInvRecAmt = new DataGridViewTextBoxCell();
                            cellInvRecAmt.Value = dt.Rows[i]["sibh_received_amount"];
                            txtRecTotAmt.Text = Convert.ToDouble(Convert.ToDouble(txtRecTotAmt.Text) + Convert.ToDouble(cellInvRecAmt.Value)).ToString("f");
                            tempRow.Cells.Add(cellInvRecAmt);

                            DataGridViewCell cellInvBalAmt = new DataGridViewTextBoxCell();
                            cellInvBalAmt.Value = Convert.ToDouble(Convert.ToDouble(cellInvAmt.Value) - (Convert.ToDouble(cellInvAdvAmt.Value) + Convert.ToDouble(cellInvRecAmt.Value))).ToString("f");
                            txtBalTotAmt.Text = Convert.ToDouble(Convert.ToDouble(txtBalTotAmt.Text) + Convert.ToDouble(cellInvBalAmt.Value)).ToString("f");
                            tempRow.Cells.Add(cellInvBalAmt);

                            DataGridViewCell cellInvTotPoints = new DataGridViewTextBoxCell();
                            cellInvTotPoints.Value = Convert.ToDouble(dt.Rows[i]["InvPoints"]).ToString("f");
                            txtInvTotPoints.Text = Convert.ToDouble(Convert.ToDouble(txtInvTotPoints.Text) + Convert.ToDouble(cellInvTotPoints.Value)).ToString("f");
                            tempRow.Cells.Add(cellInvTotPoints);

                            DataGridViewCell cellCredit = new DataGridViewTextBoxCell();
                            cellCredit.Value = dt.Rows[i]["sibh_credit_sale"];
                            tempRow.Cells.Add(cellCredit);

                            gvInvoice.Rows.Add(tempRow);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bullet Ins", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
            }
        }

        private void txtInvoiceNo_KeyPress(object sender, KeyPressEventArgs e)
        {


            if (char.IsNumber(e.KeyChar) == false)
                e.Handled = true;

            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void gvProductDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            blIsCellQty = true;
            intCurrentRow = (((System.Windows.Forms.DataGridView)(sender))).CurrentCell.RowIndex;
            intCurrentCell = (((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex;
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 5)
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    txtQty.MaxLength = 6;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }

            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 6)
            {
                TextBox txtRate = e.Control as TextBox;
                if (txtRate != null)
                {
                    txtRate.MaxLength = 10;
                    txtRate.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }

            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 7)
            {
                if (((System.Windows.Forms.DataGridView)(sender)).CurrentRow.Cells[5].Value + "" != "")
                {
                    TextBox txtAmt = e.Control as TextBox;
                    if (txtAmt != null)
                    {
                        txtAmt.MaxLength = 10;
                        txtAmt.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                    }
                }
                else
                    blIsCellQty = false;
            }

            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 8)
            {
                TextBox txtPoints = e.Control as TextBox;
                if (txtPoints != null)
                {
                    txtPoints.MaxLength = 4;
                    txtPoints.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }

            }
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46) || (blIsCellQty == false))
            {
                e.Handled = true;
                return;
            }
            //to allow decimals only teak plant
            if (intCurrentCell == 5 && gvProductDetails.Rows[intCurrentRow].Cells["Brand"].Value.ToString().Contains("TEAK"))
            {
                if (e.KeyChar == 46)
                {
                    if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                        e.Handled = true;
                }
            }
            else if (intCurrentCell == 5 && e.KeyChar == 46)
            {
                e.Handled = true;
                return;
            }
            // checks to make sure only 1 decimal is allowed
            else if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }

        }

        private void gvProductDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridView dgv = (DataGridView)sender;
                if (e.ColumnIndex == 5 || e.ColumnIndex == 7 || e.ColumnIndex == 8)
                {
                    DataGridViewCell textBoxCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    if (textBoxCell != null)
                    {
                        gvProductDetails.CurrentCell = textBoxCell;
                        dgv.BeginEdit(true);
                    }
                }
            }

            if (e.ColumnIndex == gvProductDetails.Columns["Del"].Index)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    string sSoldProd = "";
                    DataGridViewRow dgvr = gvProductDetails.Rows[e.RowIndex];
                    sSoldProd = dgvr.Cells[1].Value.ToString();
                    gvProductDetails.Rows.Remove(dgvr);
                    if (gvProductDetails.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                        {
                            gvProductDetails.Rows[i].Cells["slNo"].Value = i + 1;
                        }
                    }
                    bool bflag = false;
                    do
                    {
                        bflag = false;
                        if (dgvFreeProduct.Rows.Count > 0)
                        {
                            for (int i = 0; i < dgvFreeProduct.Rows.Count; i++)
                            {
                                if (sSoldProd == dgvFreeProduct.Rows[i].Cells["MainProductId"].Value.ToString())
                                {
                                    DataGridViewRow dgvrFree = dgvFreeProduct.Rows[i];
                                    dgvFreeProduct.Rows.Remove(dgvrFree);
                                    bflag = true;
                                }
                            }
                        }
                    } while (bflag);

                    for (int i = 0; i < dgvFreeProduct.Rows.Count; i++)
                    {
                        dgvFreeProduct.Rows[i].Cells["FreeSlNo"].Value = i + 1;
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtInvoiceNo.Text.Length > 0)
                {
                    Security objSecur = new Security();
                    if (objSecur.CanModifyDataUserAsPerBackDays(Convert.ToDateTime(meInvoiceDate.Value)) == false)
                    {
                        MessageBox.Show("You cannot manipulate backdays data!\n If you want to modify, Contact to IT-Department", "Bullet Ins", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtOrderNo.Focus();
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Do you want to Delete " + txtInvoiceNo.Text + " invoice?",
                                               "Bullet Ins", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            objSQLDB = new SQLDB();

                            string strDelete = "DELETE from Sales_Inv_Detl_FREE" +
                                                " WHERE sidf_company_code='" + CommonData.CompanyCode +
                                                "' AND sidf_branch_code='" + CommonData.BranchCode +
                                                "' AND sidf_invoice_number=" + txtInvoiceNo.Text +
                                                "  AND sidf_fin_year='" + CommonData.FinancialYear + "'";

                            strDelete += " DELETE from SALES_INV_BULTIN_DETL " +
                                                " WHERE SIBD_COMPANY_CODE='" + CommonData.CompanyCode +
                                                "' AND  sibd_branch_code='" + CommonData.BranchCode +
                                                "' AND sibd_invoice_number=" + txtInvoiceNo.Text +
                                                "  AND sibd_fin_year='" + CommonData.FinancialYear + "'";

                            strDelete += " DELETE from SALES_INV_BULTIN_HEAD " +
                                                " WHERE SIBH_COMPANY_CODE='" + CommonData.CompanyCode +
                                                "' AND sibh_branch_code='" + CommonData.BranchCode +
                                                "' AND sibh_invoice_number=" + txtInvoiceNo.Text +
                                                "  AND sibh_fin_year='" + CommonData.FinancialYear + "'";

                            int intRec = objSQLDB.ExecuteSaveData(strDelete);
                            if (intRec > 0)
                            {
                                IsModifyInvoice = false;
                                MessageBox.Show("Invoice " + intInvoiceNo + " Deleted ", "Bullet Ins", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ClearForm();
                            }
                            txtInvoiceNo.Text = GenerateNewInvoiceNo().ToString();
                            strFormerid = "99999";

                        }
                    }
                    objSecur = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bullet Ins", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                dgvFreeProduct.Rows.Clear();
                FillInvoiceList();
            }
        }

        private void cbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbBranch.SelectedIndex > -1)
            //{
            //    strBranchCode = ((System.Data.DataRowView)(cbBranch.SelectedItem)).Row.ItemArray[0].ToString();
            //    string[] strArr = strBranchCode.Split('@');
            //    strStateCode = strArr[1].ToString();
            //    strBranchCode = strArr[0].ToString();
            //    ClearForm(this);
            //    //FillProductData();
            //}
        }

        private void cbEcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEcode.SelectedIndex > -1)
            {
                strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
                //if (blEcodeLoad == false)
                //    FillInvoiceList();
            }
        }

        private void ClearForm()
        {
            intInvoiceNo = 0;
            strFormerid = "";
            txtInvAmt.Text = "0.00";
            txtTotalPoints.Text = "0.00";
            txtAdvanceAmt.Text = "0.00";
            txtReceivedAmt.Text = "0.00";
            txtBalAmt.Text = "0.00";
            lblCreditSale.Visible = false;
            dgvFreeProduct.Rows.Clear();
            //cbEcode.SelectedIndex = -1;
        }

        private void txtOrderNo_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (char.IsLetterOrDigit(e.KeyChar) == false)
                e.Handled = true;

            if (e.KeyChar == 8)
                e.Handled = false;

            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtOrderNo_Validated(object sender, EventArgs e)
        {

            if (Convert.ToString(txtOrderNo.Text).Trim().Length > 0)
            {

                ClearForm();
                FillInvoiceData(0, Convert.ToInt32(txtOrderNo.Text).ToString("00000"));

                objData = new InvoiceDB();
                Int64 invNo = objData.CheckInvoiceNoForBuiltIn(CommonData.StateCode, CommonData.BranchCode, 0, txtOrderNo.Text.ToString());
                if (invNo > 0)
                {
                    btnSave.Enabled = false;
                    btnDelete.Enabled = false;
                    MessageBox.Show(txtOrderNo.Text.ToString() + " number already in Invoices" + invNo);
                }
                objData = null;
            }

            if (blEcodeLoad == false)
                FillInvoiceList();
        }

        private void cbEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!e.KeyChar.Equals((char)8))
            {
                SearchItems(cbEcode, ref e);
            }
            else
                e.Handled = false;


        }

        private void SearchItems(ComboBox acComboBox, ref KeyPressEventArgs e)
        {
            int selectionStart = acComboBox.SelectionStart;
            int selectionLength = acComboBox.SelectionLength;
            int selectionEnd = selectionStart + selectionLength;
            int index;
            StringBuilder sb = new StringBuilder();

            sb.Append(acComboBox.Text.Substring(0, selectionStart))
                .Append(e.KeyChar.ToString())
                .Append(acComboBox.Text.Substring(selectionEnd));

            index = acComboBox.FindString(sb.ToString());

            if (index == -1)
                e.Handled = false;
            else
            {
                acComboBox.SelectedIndex = index;
                acComboBox.Select(selectionStart + 1, acComboBox.Text.Length - (selectionStart + 1));
                e.Handled = true;
            }
        }

        private void gvProductDetails_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            double actualPrice;
            double plusAmt;
            double lessAmt;
            if (e.ColumnIndex == 7)
            {
                if (Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value) != "" && gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value !="")
                {
                    if (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value) >= 0 && Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value) > 0)
                    {
                        actualPrice = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells[5].Value) * (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value));
                        plusAmt = actualPrice + (actualPrice * 10) / 100;
                        lessAmt = actualPrice - (actualPrice * 10) / 100;
                        if (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Amount"].EditedFormattedValue) > plusAmt || Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Amount"].EditedFormattedValue) < lessAmt)
                        {
                            MessageBox.Show("Please check amount !", "Product amount");
                            e.Cancel = true;
                        }
                    }
                }
                txtInvAmt.Text = GetInvoiceAmt().ToString("f");
            }

        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            ProductSearchAll PSearch = new ProductSearchAll("InvoiceBultin");
            PSearch.objFrmInvoiceBultin = this;
            PSearch.ShowDialog();
        }

        private void GetTotalPoints()
        {

            double dbTotPoints = 0;
            for (int i = 0; i < gvProductDetails.Rows.Count; i++)
            {
                if (gvProductDetails.Rows[i].Cells["Amount"].Value.ToString() != "")
                {
                    if (Convert.ToDouble(gvProductDetails.Rows[i].Cells["Amount"].Value.ToString()) >= 1)
                        dbTotPoints += Convert.ToDouble(gvProductDetails.Rows[i].Cells["Points"].Value);

                }

            }
            txtTotalPoints.Text = dbTotPoints.ToString("f");

        }

        private double GetInvoiceAmt()
        {
            double dbInvAmt = 0;
            double dbTotPoints = 0;
            for (int i = 0; i < gvProductDetails.Rows.Count; i++)
            {
                if (gvProductDetails.Rows[i].Cells["Amount"].Value.ToString() != "")
                {
                    if (Convert.ToDouble(gvProductDetails.Rows[i].Cells["Amount"].Value.ToString()) >= 1)
                    {
                        dbTotPoints += Convert.ToDouble(gvProductDetails.Rows[i].Cells["Points"].Value);
                        dbInvAmt += Convert.ToDouble(gvProductDetails.Rows[i].Cells["Amount"].Value);
                    }

                }

            }
            txtTotalPoints.Text = dbTotPoints.ToString("f");
            if (txtAdvanceAmt.Text != "0.00" || txtAdvanceAmt.Text != "")
                txtReceivedAmt.Text = Convert.ToDouble(Convert.ToDouble(txtInvAmt.Text) - Convert.ToDouble(txtAdvanceAmt.Text)).ToString("f");
            return dbInvAmt;
        }

        private void txtAdvanceAmt_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtInvAmt.Text != "")
            {
                if (txtAdvanceAmt.Text == "")
                    txtAdvanceAmt.Text = "0.00";
                if (txtReceivedAmt.Text == "")
                    txtReceivedAmt.Text = "0.00";
                txtReceivedAmt.Text = Convert.ToDouble(Convert.ToDouble(txtInvAmt.Text) - Convert.ToDouble(txtAdvanceAmt.Text)).ToString("f");
                txtBalAmt.Text = Convert.ToDouble(Convert.ToDouble(txtInvAmt.Text) - (Convert.ToDouble(txtAdvanceAmt.Text) + Convert.ToDouble(txtReceivedAmt.Text))).ToString("f");
            }
            else
            {
                txtInvAmt.Text = GetInvoiceAmt().ToString("f");
            }
        }

        private void txtReceivedAmt_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtInvAmt.Text != "")
            {
                if (txtAdvanceAmt.Text == "")
                    txtAdvanceAmt.Text = "0.00";
                if (txtReceivedAmt.Text == "")
                    txtReceivedAmt.Text = "0.00";
                txtBalAmt.Text = Convert.ToDouble(Convert.ToDouble(txtInvAmt.Text) - (Convert.ToDouble(txtAdvanceAmt.Text) + Convert.ToDouble(txtReceivedAmt.Text))).ToString("f");
            }
            else
            {
                txtInvAmt.Text = GetInvoiceAmt().ToString("f");
            }
        }

        private void txtAdvanceAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            txt_KeyPress(sender, e);
        }

        private void txtReceivedAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            txt_KeyPress(sender, e);
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text.ToString().Trim().Length > 4)
                EcodeSearch();
            else
                FillEmployeeData();
        }

        private void EcodeSearch()
        {
            objData = new InvoiceDB();
            DataSet dsEmp = null;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                cbEcode.DataSource = null;
                cbEcode.Items.Clear();
                dsEmp = objData.InvLevelEcodeSearch_Get(CommonData.CompanyCode, CommonData.BranchCode, txtDocMonth.Text, txtEcodeSearch.Text.ToString());
                DataTable dtEmp = dsEmp.Tables[0];
                if (dtEmp.Rows.Count > 0)
                {
                    cbEcode.DataSource = dtEmp;
                    cbEcode.DisplayMember = "ENAME";
                    cbEcode.ValueMember = "ECODE";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bullet Ins", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                if (cbEcode.SelectedIndex > -1)
                {
                    cbEcode.SelectedIndex = 0;
                    strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
                }
                objData = null;
                Cursor.Current = Cursors.Default;
            }

        }

        private void btnClearProd_Click(object sender, EventArgs e)
        {
            gvProductDetails.Rows.Clear();
        }

        private void btnFreeClear_Click(object sender, EventArgs e)
        {
            dgvFreeProduct.Rows.Clear();
        }
        private void dgvFreeProduct_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 8)
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    txtQty.MaxLength = 4;
                    txtQty.KeyPress += new KeyPressEventHandler(txtFreeQty_KeyPress);
                }
            }



        }

        private void txtFreeQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (char.IsNumber(e.KeyChar) == false)
            //    e.Handled = true;

            //if (e.KeyChar == 8)
            //    e.Handled = false;

            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46) || (blIsCellQty == false))
            {
                e.Handled = true;
                return;
            }
            //to allow decimals only teak plant
            if (intCurrentCell == 8 && gvProductDetails.Rows[intCurrentRow].Cells["FreeProduct"].Value.ToString().Contains("TEAK"))
            {
                if (e.KeyChar == 46)
                {
                    if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                        e.Handled = true;
                }
            }
            else if (intCurrentCell == 8 && e.KeyChar == 46)
            {
                e.Handled = true;
                return;
            }
            // checks to make sure only 1 decimal is allowed
            else if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }

        }

        private void dgvFreeProduct_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            TotalFreeProducts();
        }

        private void dgvFreeProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridView dgv = (DataGridView)sender;
                if (e.ColumnIndex == 8)
                {
                    DataGridViewCell textBoxCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    if (textBoxCell != null)
                    {
                        dgvFreeProduct.CurrentCell = textBoxCell;
                        dgv.BeginEdit(true);
                    }
                }

                if (dgvFreeProduct.Rows[e.RowIndex].Cells["Edit"].Value.ToString().Trim() != "")
                {
                    if (Convert.ToBoolean(dgvFreeProduct.Rows[e.RowIndex].Cells["Edit"].Selected) == true)
                    {
                        string FreeProductId = dgvFreeProduct.Rows[e.RowIndex].Cells[dgvFreeProduct.Columns["FreeProductId"].Index].Value.ToString();
                        int FreeQty = Convert.ToInt32(dgvFreeProduct.Rows[e.RowIndex].Cells[dgvFreeProduct.Columns["FreeQty"].Index].Value);
                        DataGridViewRow dgvr = dgvFreeProduct.Rows[e.RowIndex];
                        AddFreeProducts oAddFreeProducts = new AddFreeProducts(e.RowIndex, dgvr);
                        oAddFreeProducts.objInvoiceBultin = this;
                        oAddFreeProducts.ShowDialog();
                    }
                }
                if (e.ColumnIndex == dgvFreeProduct.Columns["Delete"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        string FreeProductId = dgvFreeProduct.Rows[e.RowIndex].Cells[dgvFreeProduct.Columns["FreeProductId"].Index].Value.ToString();
                        DataGridViewRow dgvr = dgvFreeProduct.Rows[e.RowIndex];
                        dgvFreeProduct.Rows.Remove(dgvr);
                    }
                }
                TotalFreeProducts();
            }
        }
        private double CaliculateFreeProduct(double nMainQty, double nFreeQty, double nSoldQty)
        {
            double intQty = 0;
            //if (nFreeQty <= nMainQty && intMainProductRemainder != 0)
            //    nMainQty = intMainProductRemainder;
            if (nMainQty >= nSoldQty)
            {
                intQty = Convert.ToDouble(Convert.ToInt32(nMainQty / nSoldQty));
                intMainProductRemainder = Convert.ToSByte(nMainQty % nSoldQty);
                intQty = Convert.ToDouble(intQty * nFreeQty);
            }
            return intQty;
        }

        private void TotalMainProducts()
        {
            double totQty = 0;
            for (int nRow = 0; nRow < gvProductDetails.Rows.Count; nRow++)
            {

                if (gvProductDetails.Rows[nRow].Cells["Qty"].Value.ToString() != "")
                {
                    totQty += Convert.ToDouble(gvProductDetails.Rows[nRow].Cells["Qty"].Value.ToString());
                }

            }
            txtMainProductQty.Text = totQty.ToString("f");
            if (txtMainProductQty.Text != "" && txtFreeProductQty.Text != "")
                txtTotalQty.Text = Convert.ToDouble(Convert.ToDouble(txtMainProductQty.Text.ToString()) + Convert.ToDouble(txtFreeProductQty.Text.ToString())).ToString("f");
        }

        public void TotalFreeProducts()
        {
            Int32 totFree = 0;
            for (int nRow = 0; nRow < dgvFreeProduct.Rows.Count; nRow++)
            {

                if (dgvFreeProduct.Rows[nRow].Cells["FreeQty"].Value.ToString() != "")
                {
                    totFree += Convert.ToInt32(dgvFreeProduct.Rows[nRow].Cells["FreeQty"].Value);
                }

            }
            txtFreeProductQty.Text = totFree.ToString();
            if (txtMainProductQty.Text != "" && txtFreeProductQty.Text != "")
                txtTotalQty.Text = Convert.ToDouble(Convert.ToDouble(txtMainProductQty.Text.ToString()) + Convert.ToDouble(txtFreeProductQty.Text.ToString())).ToString("f");
        }

        private void btnAddFreeProduct_Click(object sender, EventArgs e)
        {
            if (gvProductDetails.Rows.Count > 0)
            {
                AddFreeProducts freeProduct = new AddFreeProducts(gvProductDetails);
                freeProduct.objInvoiceBultin = this;
                freeProduct.ShowDialog();
            }
        }

        private void txtEcodeSearch_Validated(object sender, EventArgs e)
        {
            if (blEcodeLoad == false)
                FillInvoiceList();
        }

        private void cbEcode_Validated(object sender, EventArgs e)
        {
            if (blEcodeLoad == false)
                FillInvoiceList();
        }
    }
}
