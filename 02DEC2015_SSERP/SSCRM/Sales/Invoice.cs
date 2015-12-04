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
    public partial class Invoice : Form
    {
        private SQLDB objSQLDB = null;
        private StockPointDB objSPDB = null;
        private InvoiceDB objData = null;
        private string strFormerid = string.Empty;
        private int intInvoiceNo = 0;
        private string strVillage = string.Empty;
        private string strDateOfBirth = string.Empty;
        private string strMarriageDate = string.Empty;
        public string strStateCode = string.Empty;
        private string strBranchCode = string.Empty;
        private string strECode = string.Empty;
        private string strFSECode = string.Empty;
        private bool blCustomerSearch = false;
        private bool IsInvoiceExist = true;
        private bool blIsCellQty = true;
        private string strCreditSale = "NO";
        private bool IsModifyInvoice = false;
        private bool IsBiltinInvoice = false;
        private bool IsSaleOrder = false;
        private bool IsModifyBulletIn = false;
        private double intMainProductRemainder = 0;
        private int intCurrentRow = 0;
        private int intCurrentCell = 0;
     

        public Invoice()
        {
            InitializeComponent();
        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            txtOrderNo.Focus();
            txtDocMonth.Text = CommonData.DocMonth.ToString();
            if (CommonData.LogUserId.ToUpper() == "ADMIN" || CommonData.LogUserRole.ToUpper() == "MANAGEMENT")
                txtDocMonth.ReadOnly = false;
            else
                txtDocMonth.ReadOnly = true;
            btnDelete.Enabled = false;
            ClearForm(this);
            FillEmployeeData();
            cbEcode.SelectedIndex = -1;
            cbRelation.SelectedIndex = 0;
            gvProductDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);

            dgvFreeProduct.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);

            dgvFreeProduct.DefaultCellStyle.BackColor = Color.LightGreen;
            dgvFreeProduct.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGreen;


            if (chkBullet.CheckState == CheckState.Unchecked)
                txtInvoiceNo.Text = GenerateNewInvoiceNo().ToString();

            dtpInvoiceDate.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyyy"));
            if (CommonData.LogUserId.ToUpper() == "ADMIN")
                txtDocMonth.ReadOnly = false;

            txtOrderNo.Focus();
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm(this);
            txtOrderNo.Text = string.Empty;
            if (chkBullet.CheckState == CheckState.Unchecked)
                txtInvoiceNo.Text = GenerateNewInvoiceNo().ToString();
            txtOrderNo.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {

            this.Close();
            this.Dispose();
        }

        private void FillAddressData(string sSearch)
        {
            Hashtable htParam = null;
            objData = new InvoiceDB();
            string strDist = string.Empty;
            DataSet dsVillage = null;
            DataTable dtVillage = new DataTable();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (sSearch.Trim().Length >= 0)
                    htParam = new Hashtable();
                htParam.Add("sVillage", sSearch);
                htParam.Add("sDistrict", strDist);



                htParam.Add("sCDState", CommonData.StateCode);
                dsVillage = new DataSet();
                dsVillage = objData.GetVillageDataSet(htParam);
                dtVillage = dsVillage.Tables[0];
                if (dtVillage.Rows.Count == 1)
                {
                    txtVillage.Text = dtVillage.Rows[0]["PANCHAYAT"].ToString();  // ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[1] + "";
                    txtMandal.Text = dtVillage.Rows[0]["Mandal"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[2]+ ""; 
                    txtDistrict.Text = dtVillage.Rows[0]["District"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[3] + "";
                    txtState.Text = dtVillage.Rows[0]["State"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[4] + "";
                    txtPin.Text = dtVillage.Rows[0]["PIN"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[5] + "";
                    strStateCode = dtVillage.Rows[0]["CDState"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[0] + "";
                }
                else if (dtVillage.Rows.Count > 1)
                {
                    txtVillage.Text = "";
                    txtMandal.Text = "";
                    txtDistrict.Text = "";
                    txtState.Text = "";
                    txtPin.Text = "";
                    strStateCode = "";
                    FillAddressComboBox(dtVillage);
                }

                else
                {
                    htParam = new Hashtable();
                    htParam.Add("sVillage", "%" + sSearch);
                    htParam.Add("sDistrict", strDist);
                    dsVillage = new DataSet();
                    dsVillage = objData.GetVillageDataSet(htParam);
                    dtVillage = dsVillage.Tables[0];
                    FillAddressComboBox(dtVillage);
                    ClearVillageDetails();
                }
                Cursor.Current = Cursors.Default;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objData = null;
                Cursor.Current = Cursors.Default;

            }

        }

        private void FillBulletIns()
        {
            objData = new InvoiceDB();
            try
            {
                cbInvoiceNo.Items.Clear();
                DataTable dt = objData.InvoiceBulitinListToCustomer_Get().Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        ComboboxItem objItem = new ComboboxItem();
                        objItem.Value = dataRow["invoice_number"].ToString();
                        objItem.Text = dataRow["invoice_number"].ToString() + "---" + Convert.ToDateTime(dataRow["invoice_date"].ToString()).ToString("dd/MM/yyyy");
                        cbInvoiceNo.Items.Add(objItem);
                        objItem = null;

                    }

                }
                dt = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                objData = null;
            }
        }

        private void FillEmployeeData()
        {
            objData = new InvoiceDB();
            DataSet dsEmp = null;
            try
            {
                dsEmp = objData.GetEcodeList(CommonData.CompanyCode, CommonData.BranchCode, txtDocMonth.Text);
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
                MessageBox.Show(ex.Message);
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

        private void FillCustomerFarmerData(string sSearch, string sCustId)
        {
            DataSet ds = null;
            objData = new InvoiceDB();
            try
            {
                if (sSearch.Trim() != "" || sCustId.Length > 0)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    ds = new DataSet();
                    ds = objData.InvCustomerSearch_Get(sSearch, txtVillage.Text.ToString(), txtMandal.Text.ToString(), txtState.Text.ToString());
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count == 1)
                    {
                        strFormerid = Convert.ToString(dt.Rows[0]["cm_farmer_ID"]);
                        txtCustomerid.Text = Convert.ToString(dt.Rows[0]["cm_farmer_ID"]);
                        //txtCustomerName.Text = dt.Rows[0]["cm_farmer_name"] + "";
                        txtRelationName.Text = dt.Rows[0]["cm_forg_name"] + "";
                        cbRelation.Text = dt.Rows[0]["cm_so_fo"] + "";
                        txtHouseNo.Text = Convert.ToString(dt.Rows[0]["cm_house_no"]);
                        txtLandMark.Text = Convert.ToString(dt.Rows[0]["cm_landmark"]);
                        txtMobileNo.Text = dt.Rows[0]["cm_mobile_number"] + "";
                        txtLanLineNo.Text = dt.Rows[0]["cm_land_line_no"] + "";

                        if (dt.Rows[0]["cm_marriage_date"].ToString().Substring(0, 10) == "01/01/1900" || dt.Rows[0]["cm_marriage_date"].ToString().Substring(0, 10) == "01-01-1900")
                            dtpMarrageDate.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyyy"));
                        else
                            dtpMarrageDate.Value = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0]["cm_marriage_date"]).ToString("dd/MM/yyyy"));

                        if (dt.Rows[0]["cm_dob"].ToString().Substring(0, 10) == "01/01/1900" || dt.Rows[0]["cm_dob"].ToString().Substring(0, 10) == "01-01-1900")
                            dtpDateOfBirth.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyyy"));
                        else
                            dtpDateOfBirth.Value = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0]["cm_dob"]).ToString("dd/MM/yyyy"));

                        txtAge.Text = dt.Rows[0]["cm_age"] + "";


                    }
                    if (dt.Rows.Count >= 1)
                    {

                        FillCustomerComboBox(dt);

                    }
                    else
                    {
                        cbCustomer.DataSource = null;
                        cbCustomer.DataBindings.Clear();
                        cbCustomer.Items.Clear();
                        ///txtCustomerName.Text = "";
                        txtRelationName.Text = "";
                        txtMobileNo.Text = "";
                        txtHouseNo.Text = "";
                        txtLandMark.Text = "";
                        txtLanLineNo.Text = "";
                        txtAge.Text = "";
                        strFormerid = "";
                        txtCustomerid.Text = "";
                    }
                }

            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objData = null;
                ds.Dispose();
                Cursor.Current = Cursors.Default;

            }

        }

        private void FillProductData()
        {
            DataSet ds = null;
            objData = new InvoiceDB();
            //string[] strBCode;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ds = new DataSet();
                ds = objData.GetProductDataSet(CommonData.CompanyCode, CommonData.BranchCode);
                DataTable dt = ds.Tables[0];
                int intRow = 1;
                this.gvProductDetails.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i]["pm_single_mrp"]) > 0)
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = intRow;
                        tempRow.Cells.Add(cellSLNO);

                        DataGridViewCell cellMainProductID = new DataGridViewTextBoxCell();
                        cellMainProductID.Value = dt.Rows[i]["pm_product_id"];
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
                        cellQTY.Value = "";
                        tempRow.Cells.Add(cellQTY);

                        DataGridViewCell cellRate = new DataGridViewTextBoxCell();
                        cellRate.Value = dt.Rows[i]["pm_single_mrp"];
                        tempRow.Cells.Add(cellRate);

                        DataGridViewCell cellAmt = new DataGridViewTextBoxCell();
                        cellAmt.Value = "";
                        tempRow.Cells.Add(cellAmt);

                        DataGridViewCell cellPoints = new DataGridViewTextBoxCell();
                        cellPoints.Value = Convert.ToDouble(dt.Rows[i]["TIPPoints"]).ToString("f");
                        tempRow.Cells.Add(cellPoints);

                        DataGridViewCell cellDBRate = new DataGridViewTextBoxCell();
                        cellDBRate.Value = dt.Rows[i]["pm_single_mrp"];
                        tempRow.Cells.Add(cellDBRate);

                        DataGridViewCell cellDBPoints = new DataGridViewTextBoxCell();
                        cellDBPoints.Value = Convert.ToDouble(dt.Rows[i]["TIPPoints"]).ToString("f");
                        tempRow.Cells.Add(cellDBPoints);

                        intRow = intRow + 1;
                        gvProductDetails.Rows.Add(tempRow);
                    }
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objData = null;
                ds.Dispose();
                Cursor.Current = Cursors.Default;

            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            int intSaved = 0;
            try
            {
                txtInvAmt.Text = GetInvoiceAmt().ToString("f");
                try { txtAdvanceAmt_KeyUp(null, null); }
                catch { }
                try
                { Convert.ToDouble(txtAdvanceAmt.Text); }
                catch
                { txtAdvanceAmt.Text = "0"; }

                txtBalAmt.Text = Convert.ToDouble(Convert.ToDouble(txtInvAmt.Text) - Convert.ToDouble(txtAdvanceAmt.Text)).ToString("f");

                if (dtpMarrageDate.Value.ToString("dd/MM/yyyy") == Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyyy"))
                    strMarriageDate = "";
                else
                    strMarriageDate = dtpMarrageDate.Value.ToString("dd/MM/yyyy");

                if (dtpDateOfBirth.Value.ToString("dd/MM/yyyy") == Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyyy"))
                    strDateOfBirth = "";
                else
                    strDateOfBirth = dtpDateOfBirth.Value.ToString("dd/MM/yyyy");

                if (strFSECode == "")
                    strFSECode = "0";


                if (CheckData() == true)
                {
                    if (SaveCustomerData() >= 1)
                    {
                        intSaved = SaveInvoiceHeadData();
                        //if (SaveInvoiceHeadData() >= 1)
                        //    intSaved = SaveInvoiceDeatailData();
                        //if (dgvFreeProduct.Rows.Count > 0)
                        //    intSaved = SaveFreeDeatailData();
                        //SaveServiceActivityRecords();
                    }
                    if (intSaved > 0)
                    {
                        if (intSaved > 0 ) //&& IsModifyBulletIn == true)
                        {
                            //SaveBulletInvoiceData();
                            MessageBox.Show("Data Saved Successfully","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        }
                        intInvoiceNo = 0;
                        strFormerid = "";
                        ClearForm(this);
                        txtOrderNo.Text = string.Empty;
                        cbVillage.DataBindings.Clear();
                        txtOrderNo.Focus();
                        if (chkBullet.CheckState == CheckState.Unchecked)
                            txtInvoiceNo.Text = GenerateNewInvoiceNo().ToString();

                    }
                    else
                    {

                        MessageBox.Show("Data not saved");
                        intInvoiceNo = 0;
                        strFormerid = "";
                        ClearForm(this);
                        txtOrderNo.Text = string.Empty;
                        cbVillage.DataBindings.Clear();
                        txtOrderNo.Focus();
                        if (chkBullet.CheckState == CheckState.Unchecked)
                            txtInvoiceNo.Text = GenerateNewInvoiceNo().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //if (chkBullet.CheckState == CheckState.Checked)
                //{
                //    FillBulletIns();
                //    cbInvoiceNo.SelectedIndex = -1;
                //}
            }

            this.Cursor = Cursors.Default;

            txtOrderNo.Focus();

        }

        private void SaveServiceActivityRecords()
        {
            objSQLDB = new SQLDB();
            string sqlText = " exec SSCRM_SERVICE_TNA_AUD '" + CommonData.CompanyCode +
                                "', '" + CommonData.BranchCode +
                                "', '" + txtDocMonth.Text.ToUpper() +
                                "', '" + txtOrderNo.Text + 
                                "', " + txtInvoiceNo.Text;

            try
            {
                objSQLDB.ExecuteSaveData(sqlText);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLDB = null;
            }
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
        private bool CheckData()
        {

            bool blValue = true;
            strCreditSale = "NO";
            if (Convert.ToString(txtOrderNo.Text).Length == 0)
            {
                MessageBox.Show("Enter Order number!", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtOrderNo.Focus();
                return blValue;
            }

            if (Convert.ToString(txtInvoiceNo.Text).Length == 0 || Convert.ToString(txtInvoiceNo.Text) == "0")
            {
                MessageBox.Show("Enter Invoice number!", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtInvoiceNo.Focus();
                return blValue;
            }

            if (Convert.ToDateTime(Convert.ToDateTime(dtpInvoiceDate.Value).ToString("dd/MM/yyyy")) > Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyyy")))
            {
                MessageBox.Show("Invoice date should be less than to day.", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                dtpInvoiceDate.Focus();
                return blValue;
            }
            //else if (Convert.ToDateTime(meInvoiceDate.Value) < Convert.ToDateTime(Convert.ToDateTime(CommonData.DocFDate).ToString("dd/MM/yyyy")) ||
            //Convert.ToDateTime(meInvoiceDate.Value) > Convert.ToDateTime(Convert.ToDateTime(CommonData.DocTDate).ToString("dd/MM/yyyy")))
            //{
            //    MessageBox.Show("Invoice date should be between " + CommonData.DocFDate + " and " + CommonData.DocTDate, "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    blValue = false;
            //    meInvoiceDate.Focus();
            //    return blValue;
            //}

            Security objSecur = new Security();
            if (objSecur.CanModifyDataUserAsPerBackDays(Convert.ToDateTime(dtpInvoiceDate.Value)) == false)
            {
                MessageBox.Show("You cannot manipulate backdays data!\n If you want to modify, Contact to IT-Department", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                blValue = false;
                txtOrderNo.Focus();
                return blValue;
            }
            objSecur = null;

            if (cbEcode.SelectedIndex == -1)
            {
                MessageBox.Show("Enter Employee Code!", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                cbEcode.Focus();
                return blValue;
            }
            else
            {
                string sEcode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
                string sLockStatus = GetLockingStatus(sEcode, txtDocMonth.Text);
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
            if (Convert.ToString(txtVillage.Text).Length == 0 || txtVillage.Text.ToString().Trim() == "NOVILLAGE")
            {
                MessageBox.Show("Enter Village!", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtVillageSearch.Focus();
                return blValue;
            }
            if (strStateCode.Length == 0)
            {
                MessageBox.Show("No State for " + txtVillage.Text + " Village!");
                blValue = false;
                txtVillageSearch.Focus();
                return blValue;
            }

            if (cbCustomer.Text.ToString().Trim().Length > 0)
                txtCustomerName.Text = cbCustomer.Text;

            if (Convert.ToString(txtCustomerName.Text).Length == 0 || txtCustomerName.Text.ToString().Trim() == "NOVILLAGE")
            {
                MessageBox.Show("Enter Customer name!", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtCustomerName.Focus();
                return blValue;
            }
            //if (Convert.ToInt32(Convert.ToDateTime(dtpDateOfBirth.Value).ToString("yyyy")) < 1950)
            //{
            //    MessageBox.Show("Enter valid  Date !", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    blValue = false;
            //    dtpDateOfBirth.Focus();
            //    return blValue;
            //}
            if (Convert.ToDateTime(Convert.ToDateTime(dtpDateOfBirth.Value).ToString("dd/MM/yyyy")) > Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyyy")))
            {
                MessageBox.Show("Date should be less than to day", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                dtpDateOfBirth.Focus();
                return blValue;
            }
            if (Convert.ToDateTime(Convert.ToDateTime(dtpMarrageDate.Value).ToString("dd/MM/yyyy")) > Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy")))
            {
                MessageBox.Show("Date should be less than to day", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                dtpMarrageDate.Focus();
                return blValue;
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
                MessageBox.Show("Enter product details", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (txtInvAmt.Text.ToString() == "")
            {
                MessageBox.Show("Please check invoice amount!", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                gvProductDetails.Focus();
                return blValue;
            }
            if (txtReceivedAmt.Text == "" || txtReceivedAmt.Text == "0" || txtReceivedAmt.Text == "0.00")
                txtReceivedAmt.Text = "0.00";
            if (txtBalAmt.Text == "" || txtBalAmt.Text == "0" || txtBalAmt.Text == "0.00")
                txtBalAmt.Text = "0.00";
            if (txtAdvanceAmt.Text == "" || txtAdvanceAmt.Text == "0" || txtAdvanceAmt.Text == "0.00")
                txtAdvanceAmt.Text = "0.00";
            if (Convert.ToDouble(txtInvAmt.Text) > Convert.ToDouble(txtReceivedAmt.Text) + Convert.ToDouble(txtAdvanceAmt.Text))
            {
                DialogResult result = MessageBox.Show("Do you want to save " + txtInvoiceNo.Text + " invoice as Credit Sale ?",
                                       "CRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                DialogResult result = MessageBox.Show("Invoice amount less than total collection amount, Do you want to save ?",
                                       "Invoice ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                MessageBox.Show("You cannot add new data", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                blValue = false;
                txtInvoiceNo.Focus();
                return blValue;
            }

            if (IsSaleOrder == false)
            {
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
                                    " Not Issued to any One in the Order Form Issue Screen"
                                    , "SSERP :: Invoice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

            }

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

            if (txtAdvanceAmt.Text == "")
                txtAdvanceAmt.Text = "0.00";
            if (txtReceivedAmt.Text == "")
                txtReceivedAmt.Text = "0.00";
            if (strFSECode == "")
                strFSECode = "0";

            try { Convert.ToInt32(txtOrderNo.Text.ToString()); }
            catch { txtOrderNo.Text = "0"; }
            try
            {
                if (IsModifyInvoice == false)
                {
                    if (chkBullet.CheckState == CheckState.Checked)
                        txtInvoiceNo.Text = ((SSCRM.ComboboxItem)(cbInvoiceNo.Items[cbInvoiceNo.SelectedIndex])).Value.ToString();
                    else if (IsBiltinInvoice == true)
                    {
                        if (CheckOrderInvoiceExist(0, Convert.ToInt32(txtOrderNo.Text.ToString()).ToString("00000")) == false)
                            txtInvoiceNo.Text = GenerateNewInvoiceNo().ToString();
                        //else if(CheckOrderInvoiceExist(Convert.ToInt32(txtInvoiceNo.Text.ToString()), txtOrderNo.Text.ToString()) == true)
                        //    txtInvoiceNo.Text = GenerateNewInvoiceNo().ToString();
                        //else
                        //    return intRec;
                    }
                    //else
                    //    txtInvoiceNo.Text = GenerateNewInvoiceNo().ToString();
                    objData = null;
                    //txtInvoiceNo.Text = GenerateNewInvoiceNo().ToString();
                    if (Convert.ToInt32(txtInvoiceNo.Text) != 0)
                    {
                        sqlText = " INSERT INTO Sales_Inv_Head(sih_company_code, sih_state_code, sih_branch_code, sih_fin_year" +
                                    ", sih_invoice_number, sih_invoice_date, sih_farmer_ID, sih_order_number, sih_eora_code, " +
                                    "sih_prod_pattern, sih_Document_month, sih_created_Date, SIH_CREATED_BY, SIH_INVOICE_AMOUNT" +
                                    ", SIH_ADVANCE_AMOUNT, SIH_RECEIVED_AMOUNT, SIH_CREDIT_SALE, SIH_FIELD_SUP_BY, SIH_ORDER_DATE)" +
                                    " VALUES ('" + CommonData.CompanyCode + "', '" + CommonData.StateCode + "', '" + CommonData.BranchCode +
                                    "' , '" + CommonData.FinancialYear + "'," + txtInvoiceNo.Text + ", '" + Convert.ToDateTime(dtpInvoiceDate.Value).ToString("dd/MMM/yyy") + "' " +
                                    ", '" + strFormerid + "', '" + Convert.ToInt32(txtOrderNo.Text).ToString("00000") + "', '" + strECode +
                                    "', 'Ppattern', '" + txtDocMonth.Text.ToUpper() + "', GETDATE(),'" + CommonData.LogUserId +
                                    "', " + txtInvAmt.Text + "," + txtAdvanceAmt.Text + ", " + txtReceivedAmt.Text +
                                    ", '" + strCreditSale + "'," + strFSECode + ", '" + Convert.ToDateTime(dtpOrderDate.Value).ToString("dd/MMM/yyy") + "')";

                        sqlText += " INSERT INTO SALES_INV_BULTIN_HEAD(sibh_company_code, sibh_state_code, sibh_branch_code, sibh_fin_year"+
                                    ", sibh_invoice_number, sibh_invoice_date, sibh_farmer_ID, sibh_order_number, sibh_eora_code"+
                                    ", sibh_prod_pattern, sibh_Document_month, sibh_created_Date, SIBH_CREATED_BY, SIBH_INVOICE_AMOUNT"+
                                    ", SIBH_ADVANCE_AMOUNT, SIBH_RECEIVED_AMOUNT, SIBH_CREDIT_SALE, SIBH_FIELD_SUP_BY, SIBH_ORDER_DATE)" +
                                     " SELECT '" + CommonData.CompanyCode + "', '" + CommonData.StateCode + "', '" + CommonData.BranchCode +
                                     "' , '" + CommonData.FinancialYear + "'," + txtInvoiceNo.Text + ", '" + Convert.ToDateTime(dtpInvoiceDate.Value).ToString("dd/MMM/yyy") + "' " +
                                     ", '99999', '" + Convert.ToInt32(txtOrderNo.Text).ToString("00000") + "', '" + strECode + "', 'Ppattern', '" + CommonData.DocMonth +
                                     "', '" + CommonData.CurrentDate + "','" + CommonData.LogUserId +
                                     "', " + txtInvAmt.Text + "," + txtAdvanceAmt.Text +
                                     ", " + txtReceivedAmt.Text + ", '" + strCreditSale +
                                     "'," + strFSECode + ",'" + Convert.ToDateTime(dtpOrderDate.Value).ToString("dd/MMM/yyy") + 
                                     "'  WHERE NOT EXISTS (SELECT sibh_order_number FROM SALES_INV_BULTIN_HEAD" +
                                     " WHERE sibh_branch_code='" + CommonData.BranchCode +
                                     "' AND sibh_fin_year='" + CommonData.FinancialYear +
                                     "' AND sibh_order_number='" + Convert.ToInt32(txtOrderNo.Text).ToString("00000") + "')";

                        sqlText += " DELETE from Sales_Inv_Detl_FREE" +
                                " WHERE sidf_company_code='" + CommonData.CompanyCode +
                                    "' AND sidf_branch_code='" + CommonData.BranchCode +
                                    "' AND sidf_invoice_number=" + txtInvoiceNo.Text +
                                    "  AND sidf_fin_year='" + CommonData.FinancialYear + "'";

                        sqlText += " DELETE from SALES_INV_BULTIN_DETL" +
                             " WHERE sibd_company_code='" + CommonData.CompanyCode +
                                 "' AND sibd_branch_code='" + CommonData.BranchCode +
                                 "' AND sibd_invoice_number=" + txtInvoiceNo.Text +
                                 "  AND sibd_fin_year='" + CommonData.FinancialYear + "'";

                        sqlText += " UPDATE SALES_INV_BULTIN_HEAD set sibh_invoice_date='" + Convert.ToDateTime(dtpInvoiceDate.Value).ToString("dd/MMM/yyy") +
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
                                   "', SIBH_FIELD_SUP_BY=" + strFSECode +
                                   ", SIBH_ORDER_DATE='" + Convert.ToDateTime(dtpOrderDate.Value).ToString("dd/MMM/yyy") +
                                   "' WHERE sibh_invoice_number = " + txtInvoiceNo.Text +
                                   "  AND sibh_branch_code='" + CommonData.BranchCode +
                                   "' AND sibh_fin_year='" + CommonData.FinancialYear.ToString() +
                                   "' AND sibh_company_code='" + CommonData.CompanyCode.ToString() +
                                   "' AND EXISTS (SELECT sibh_order_number FROM SALES_INV_BULTIN_HEAD" +
                                    " WHERE sibh_branch_code='" + CommonData.BranchCode +
                                    "' AND sibh_fin_year='" + CommonData.FinancialYear +
                                    "' AND sibh_order_number='" + Convert.ToInt32(txtOrderNo.Text).ToString("00000") + "')";

                    }
                }
                else
                {
                    //string sqlDelete = "";
                    sqlText = " DELETE FROM SERVICES_TNA WHERE TNA_COMPANY_CODE = '" + CommonData.CompanyCode +
                                    "' AND TNA_BRANCH_CODE='" + CommonData.BranchCode +
                                    "' AND TNA_FIN_YEAR='" + CommonData.FinancialYear +
                                    "' AND TNA_INVOICE_NUMBER =" + txtInvoiceNo.Text;
                    sqlText += " DELETE from Sales_Inv_Detl_FREE" +
                                " WHERE sidf_company_code='" + CommonData.CompanyCode +
                                    "' AND sidf_branch_code='" + CommonData.BranchCode +
                                    "' AND sidf_invoice_number=" + txtInvoiceNo.Text +
                                    "  AND sidf_fin_year='" + CommonData.FinancialYear + "'";
                    sqlText += " DELETE from SALES_INV_BULTIN_DETL" +
                             " WHERE sibd_company_code='" + CommonData.CompanyCode +
                                 "' AND sibd_branch_code='" + CommonData.BranchCode +
                                 "' AND sibd_invoice_number=" + txtInvoiceNo.Text +
                                 "  AND sibd_fin_year='" + CommonData.FinancialYear + "'";
                    sqlText += " DELETE from Sales_Inv_Detl" +
                                " WHERE sid_company_code='" + CommonData.CompanyCode +
                                "' AND sid_branch_code='" + CommonData.BranchCode +
                                "' AND sid_invoice_number=" + txtInvoiceNo.Text +
                                "  AND sid_fin_year='" + CommonData.FinancialYear + "'";
                    
                    //intRec = objSQLDB.ExecuteSaveData(sqlDelete);

                    sqlText += " UPDATE Sales_Inv_Head set sih_invoice_date='" + Convert.ToDateTime(dtpInvoiceDate.Value).ToString("dd/MMM/yyy") +
                            "', sih_farmer_ID='" + strFormerid +
                            "', sih_eora_code='" + strECode +
                            "', sih_prod_pattern='Ppattern'" +
                            ", sih_Document_month='" + txtDocMonth.Text +
                            "', SIH_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                            "', SIH_LAST_MODIFIED_DATE=GETDATE()" +
                            ", sih_order_number='" + Convert.ToInt32(txtOrderNo.Text).ToString("00000") +
                            "', SIH_INVOICE_AMOUNT =" + txtInvAmt.Text +
                            ", SIH_ADVANCE_AMOUNT = " + txtAdvanceAmt.Text +
                            ", SIH_RECEIVED_AMOUNT = " + txtReceivedAmt.Text +
                            ", SIH_CREDIT_SALE = '" + strCreditSale +
                            "', SIH_FIELD_SUP_BY=" + strFSECode +
                            ", SIH_ORDER_DATE='" + Convert.ToDateTime(dtpOrderDate.Value).ToString("dd/MMM/yyy") +
                            "' WHERE sih_invoice_number = " + txtInvoiceNo.Text +
                            "  AND sih_branch_code='" + CommonData.BranchCode +
                            "' AND sih_fin_year='" + CommonData.FinancialYear.ToString() +
                            "' AND sih_company_code='" + CommonData.CompanyCode.ToString() + "'";

                    sqlText += " UPDATE SALES_INV_BULTIN_HEAD set sibh_invoice_date='" + Convert.ToDateTime(dtpInvoiceDate.Value).ToString("dd/MMM/yyy") +
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
                                   "', SIBH_FIELD_SUP_BY=" + strFSECode +
                                   ", SIBH_ORDER_DATE='" + Convert.ToDateTime(dtpOrderDate.Value).ToString("dd/MMM/yyy") +
                                   "' WHERE sibh_invoice_number = " + txtInvoiceNo.Text +
                                   "  AND sibh_branch_code='" + CommonData.BranchCode +
                                   "' AND sibh_fin_year='" + CommonData.FinancialYear.ToString() +
                                   "' AND sibh_company_code='" + CommonData.CompanyCode.ToString() +
                                   "' AND EXISTS (SELECT sibh_order_number FROM SALES_INV_BULTIN_HEAD" +
                                    " WHERE sibh_branch_code='" + CommonData.BranchCode +
                                    "' AND sibh_fin_year='" + CommonData.FinancialYear +
                                    "' AND sibh_order_number='" + Convert.ToInt32(txtOrderNo.Text).ToString("00000") + "')";


                }

                sqlText += " DELETE FROM SERVICES_TNA WHERE TNA_COMPANY_CODE = '" + CommonData.CompanyCode +
                                    "' AND TNA_BRANCH_CODE='" + CommonData.BranchCode +
                                    "' AND TNA_FIN_YEAR='" + CommonData.FinancialYear +
                                    "' AND TNA_INVOICE_NUMBER =" + txtInvoiceNo.Text;
                sqlText += " DELETE from Sales_Inv_Detl_FREE" +
                                " WHERE sidf_company_code='" + CommonData.CompanyCode +
                                    "' AND sidf_branch_code='" + CommonData.BranchCode +
                                    "' AND sidf_invoice_number=" + txtInvoiceNo.Text +
                                    "  AND sidf_fin_year='" + CommonData.FinancialYear + "'";
                sqlText += " DELETE from SALES_INV_BULTIN_DETL" +
                         " WHERE sibd_company_code='" + CommonData.CompanyCode +
                             "' AND sibd_branch_code='" + CommonData.BranchCode +
                             "' AND sibd_invoice_number=" + txtInvoiceNo.Text +
                             "  AND sibd_fin_year='" + CommonData.FinancialYear + "'";
                sqlText += " DELETE from Sales_Inv_Detl" +
                            " WHERE sid_company_code='" + CommonData.CompanyCode +
                            "' AND sid_branch_code='" + CommonData.BranchCode +
                            "' AND sid_invoice_number=" + txtInvoiceNo.Text +
                            "  AND sid_fin_year='" + CommonData.FinancialYear + "'";

                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {
                    if (gvProductDetails.Rows[i].Cells["Rate"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["Qty"].Value.ToString() != "")
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

                        sqlText += " INSERT INTO Sales_Inv_Detl(sid_company_code, sid_state_code, sid_branch_code, sid_invoice_number,sid_fin_year, sid_invoice_sl_no, sid_state, sid_product_id, sid_price, sid_qty, sid_amount, SID_PRODUCT_POINTS, SID_UNIT_POINTS)" +
                                    " VALUES ('" + CommonData.CompanyCode + "', '" + CommonData.StateCode + "', '" + CommonData.BranchCode + "', " + txtInvoiceNo.Text + ", '" + CommonData.FinancialYear + "'," + gvProductDetails.Rows[i].Cells["SLNO"].Value + ", '" + CommonData.StateName +
                                    "', '" + gvProductDetails.Rows[i].Cells["ProductID"].Value.ToString().Trim() + "', " + gvProductDetails.Rows[i].Cells["Rate"].Value + ", " + gvProductDetails.Rows[i].Cells["Qty"].Value +
                                    ", " + gvProductDetails.Rows[i].Cells["Amount"].Value + ", " + gvProductDetails.Rows[i].Cells["Points"].Value + ",'" + gvProductDetails.Rows[i].Cells["DBPoints"].Value + "'); ";



                    }

                }

                for (int i = 0; i < dgvFreeProduct.Rows.Count; i++)
                {
                    //if (dgvFreeProduct.Rows[i].Cells["FreeQty"].Value.ToString() != "")
                    //{
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

                    //}

                }

                sqlText += " exec SSCRM_SERVICE_TNA_AUD '" + CommonData.CompanyCode +
                                "', '" + CommonData.BranchCode +
                                "', '" + txtDocMonth.Text.ToUpper() +
                                "', '" + txtOrderNo.Text +
                                "', " + txtInvoiceNo.Text;
                intRec = 0;
                if (sqlText.Length > 10)
                    intRec = objSQLDB.ExecuteSaveData(sqlText);

            }
            catch (Exception ex)
            {
                intRec = 0;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objSQLDB = null;

            }
            return intRec;
        }

        private int SaveInvoiceDeatailData()
        {
            objSQLDB = new SQLDB();
            string sqlText = "";
            //string sqlDelete = "";
            int intRec = 0;
            try
            {
                sqlText = " DELETE FROM SERVICES_TNA WHERE TNA_COMPANY_CODE = '" + CommonData.CompanyCode +
                                    "' AND TNA_BRANCH_CODE='" + CommonData.BranchCode +
                                    "' AND TNA_FIN_YEAR='" + CommonData.FinancialYear +
                                    "' AND TNA_INVOICE_NUMBER =" + txtInvoiceNo.Text;

                sqlText += " DELETE from Sales_Inv_Detl_FREE" +
                                " WHERE sidf_company_code='" + CommonData.CompanyCode +
                                    "' AND sidf_branch_code='" + CommonData.BranchCode +
                                    "' AND sidf_invoice_number=" + txtInvoiceNo.Text +
                                    "  AND sidf_fin_year='" + CommonData.FinancialYear + "'";

                sqlText += " DELETE from SALES_INV_BULTIN_DETL" +
                             " WHERE sibd_company_code='" + CommonData.CompanyCode +
                                 "' AND sibd_branch_code='" + CommonData.BranchCode +
                                 "' AND sibd_invoice_number=" + txtInvoiceNo.Text +
                                 "  AND sibd_fin_year='" + CommonData.FinancialYear + "'";

                sqlText += " DELETE from Sales_Inv_Detl" +
                                " WHERE sid_company_code='" + CommonData.CompanyCode +
                                    "' AND sid_branch_code='" + CommonData.BranchCode +
                                    "' AND sid_invoice_number=" + txtInvoiceNo.Text +
                                    "  AND sid_fin_year='" + CommonData.FinancialYear + "'";

                //intRec = objSQLDB.ExecuteSaveData(sqlDelete);

                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {
                    if (gvProductDetails.Rows[i].Cells["Rate"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["Qty"].Value.ToString() != "")
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

                        sqlText += " INSERT INTO Sales_Inv_Detl(sid_company_code, sid_state_code, sid_branch_code, sid_invoice_number,sid_fin_year, sid_invoice_sl_no, sid_state, sid_product_id, sid_price, sid_qty, sid_amount, SID_PRODUCT_POINTS, SID_UNIT_POINTS)" +
                                    " VALUES ('" + CommonData.CompanyCode + "', '" + CommonData.StateCode + "', '" + CommonData.BranchCode + "', " + txtInvoiceNo.Text + ", '" + CommonData.FinancialYear + "'," + gvProductDetails.Rows[i].Cells["SLNO"].Value + ", '" + CommonData.StateName +
                                    "', '" + gvProductDetails.Rows[i].Cells["ProductID"].Value.ToString().Trim() + "', " + gvProductDetails.Rows[i].Cells["Rate"].Value + ", " + gvProductDetails.Rows[i].Cells["Qty"].Value +
                                    ", " + gvProductDetails.Rows[i].Cells["Amount"].Value + ", " + gvProductDetails.Rows[i].Cells["Points"].Value + ",'" + gvProductDetails.Rows[i].Cells["DBPoints"].Value + "'); ";

                        

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
                objData = null;
            }
            return intRec;

        }

        private int SaveFreeDeatailData()
        {
            objSQLDB = new SQLDB();
            string sqlText = "";
            int intRec = 0;
            //MainProductId
            try
            {
                for (int i = 0; i < dgvFreeProduct.Rows.Count; i++)
                {
                    //if (dgvFreeProduct.Rows[i].Cells["FreeQty"].Value.ToString() != "")
                    //{
                        if (dgvFreeProduct.Rows[i].Cells["OfferNumber"].Value.ToString() == "")
                            dgvFreeProduct.Rows[i].Cells["OfferNumber"].Value = 0;

                        sqlText += " INSERT INTO Sales_Inv_Detl_FREE(sidf_company_code, sidf_state_code, sidf_branch_code, "+
                                    "sidf_invoice_number,sidf_fin_year, sidf_invoice_sl_no, sidf_product_id, "+
                                    "SIDF_INVOICE_PRODUCT_ID, SIDF_OFFER_NUMBER, sidf_given_qty1, sidf_given_ecode1, "+
                                    "sidf_given_date1, SIDF_SL_NO)" +
                                    " VALUES ('" + CommonData.CompanyCode + "', '" + CommonData.StateCode + 
                                    "', '" + CommonData.BranchCode + "', " + txtInvoiceNo.Text + 
                                    ", '" + CommonData.FinancialYear + "'," + dgvFreeProduct.Rows[i].Cells["MainProductIdSLno"].Value +
                                    ", '" + dgvFreeProduct.Rows[i].Cells["FreeProductID"].Value.ToString().Trim() + 
                                    "', '" + dgvFreeProduct.Rows[i].Cells["MainProductId"].Value.ToString().Trim() + 
                                    "'," + dgvFreeProduct.Rows[i].Cells["OfferNumber"].Value + ", " + dgvFreeProduct.Rows[i].Cells["FreeQty"].Value +
                                    ", " + strECode + ", getdate(), " + dgvFreeProduct.Rows[i].Cells["FreeSlNo"].Value + ") ";

                    //}

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
                objData = null;
            }
            return intRec;

        }

        private int SaveBulletInvoiceData()
        {
            objSQLDB = new SQLDB();
            string sqlText = "";
            int intRec = 0;

            if (txtAdvanceAmt.Text == "")
                txtAdvanceAmt.Text = "0.00";
            if (txtReceivedAmt.Text == "")
                txtReceivedAmt.Text = "0.00";
            try
            {
                sqlText = " DELETE from SALES_INV_BULTIN_DETL" +
                             " WHERE sibd_company_code='" + CommonData.CompanyCode +
                                 "' AND sibd_branch_code='" + CommonData.BranchCode +
                                 "' AND sibd_invoice_number=" + txtInvoiceNo.Text +
                                 "  AND sibd_fin_year='" + CommonData.FinancialYear + "'";

                //intRec = objSQLDB.ExecuteSaveData(sqlDelete);

                sqlText += " UPDATE SALES_INV_BULTIN_HEAD set sibh_invoice_date='" + Convert.ToDateTime(dtpInvoiceDate.Value).ToString("dd/MMM/yyy") +
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
                        "' WHERE sibh_invoice_number = " + txtInvoiceNo.Text +
                        "  AND sibh_branch_code='" + CommonData.BranchCode +
                        "' AND sibh_fin_year='" + CommonData.FinancialYear.ToString() +
                        "' AND sibh_company_code='" + CommonData.CompanyCode.ToString() +
                        "' AND EXISTS (SELECT sibh_order_number FROM SALES_INV_BULTIN_HEAD" +
                         " WHERE sibh_branch_code='" + CommonData.BranchCode +
                         "' AND sibh_fin_year='" + CommonData.FinancialYear +
                         "' AND sibh_order_number='" + Convert.ToInt32(txtOrderNo.Text).ToString("00000") + "')";


                //intRec = objSQLDB.ExecuteSaveData(sqlText);

                //if (intRec == 0)
                //{
                sqlText += " INSERT INTO SALES_INV_BULTIN_HEAD(sibh_company_code, sibh_state_code, sibh_branch_code, sibh_fin_year, sibh_invoice_number, sibh_invoice_date, sibh_farmer_ID, sibh_order_number, sibh_eora_code, sibh_prod_pattern, sibh_Document_month, sibh_created_Date, SIBH_CREATED_BY, SIBH_INVOICE_AMOUNT, SIBH_ADVANCE_AMOUNT, SIBH_RECEIVED_AMOUNT, SIBH_CREDIT_SALE)" +
                             " SELECT '" + CommonData.CompanyCode + "', '" + CommonData.StateCode + "', '" + CommonData.BranchCode +
                             "' , '" + CommonData.FinancialYear + "'," + txtInvoiceNo.Text + ", '" + Convert.ToDateTime(dtpInvoiceDate.Value).ToString("dd/MMM/yyy") + "' " +
                             ", '99999', '" + Convert.ToInt32(txtOrderNo.Text).ToString("00000") + "', '" + strECode + "', 'Ppattern', '" + CommonData.DocMonth +
                             "', '" + CommonData.CurrentDate + "','" + CommonData.LogUserId +
                             "', " + txtInvAmt.Text + "," + txtAdvanceAmt.Text +
                             ", " + txtReceivedAmt.Text + ", '" + strCreditSale +
                             "' WHERE NOT EXISTS (SELECT sibh_order_number FROM SALES_INV_BULTIN_HEAD" +
                             " WHERE sibh_branch_code='" + CommonData.BranchCode +
                             "' AND sibh_fin_year='" + CommonData.FinancialYear +
                             "' AND sibh_order_number='" + Convert.ToInt32(txtOrderNo.Text).ToString("00000") + "')";
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

                    if (gvProductDetails.Rows[i].Cells["Rate"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["Qty"].Value.ToString() != "")
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
                intRec = 0;
                MessageBox.Show(ex.Message, "Invoice ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objSQLDB = null;

            }
            return intRec;
        }

        private int SaveCustomerData()
        {
            objSQLDB = new SQLDB();
            objData = new InvoiceDB();
            object strDOB = string.Empty;
            object strMarDate = string.Empty;
            object strAge = null;
            string sqlText = string.Empty;
            int intRec = 0;
            if (strDateOfBirth.Length > 5)
                strDOB = Convert.ToDateTime(strDateOfBirth).ToString("dd/MMM/yyyy");
            else
                strDOB = "";

            if (strMarriageDate.Length > 5)
                strMarDate = Convert.ToDateTime(strMarriageDate).ToString("dd/MMM/yyyy");
            else
                strMarDate = "";
            if (txtAge.Text.Length > 0)
                strAge = Convert.ToInt16(txtAge.Text);
            if (strAge == null)
                strAge = "0";
            try
            {
                if (strFormerid != "" && strFormerid != "99999")
                {
                    try
                    {
                        intRec = Convert.ToInt32(objSQLDB.ExecuteDataSet("SELECT count(cm_farmer_id) from Customer_Mas where cm_farmer_id ='" + strFormerid + "'").Tables[0].Rows[0][0]);
                    }
                    catch { intRec = 0; }
                }
                else { intRec = 0; }

                if (intRec == 0 || strFormerid == "" || strFormerid == "99999")
                {
                    strFormerid = objData.GetCustomerFarmerId(txtState.Text.ToString().Trim(), txtDistrict.Text.ToString().Trim(), txtMandal.Text.ToString().Trim(), txtVillage.Text.ToString().Trim()).ToString();
                    if (strAge == null)
                        strAge = "0";
                    sqlText = " INSERT INTO Customer_Mas(cm_village, cm_farmer_ID" +
                       ", cm_farmer_name, cm_so_fo, cm_forg_name, cm_mobile_number" +
                       ", cm_land_line_no, cm_mandal, cm_district, cm_state, cm_pin, cm_dob, cm_marriage_date, cm_age" +
                       ", cm_house_no, cm_landmark, CM_CREATED_BY, CM_CREATED_DATE) VALUES ( '" + txtVillage.Text + "".Trim() + "', '" + strFormerid +
                       "', '" + txtCustomerName.Text + "".Trim() + "','" + cbRelation.Text + "','" + txtRelationName.Text + "'" +
                       ", '" + txtMobileNo.Text + "" + "','" + txtLanLineNo.Text + "'".Trim() +
                       ", '" + txtMandal.Text + "" + "', '" + txtDistrict.Text + "', '" + txtState.Text +
                       "', '" + txtPin.Text + "', '" + strDOB + "','" + strMarDate + "', " + strAge +
                       ", '" + txtHouseNo.Text + "".Trim() +
                       "', '" + txtLandMark.Text + "".Trim() + "', '" + CommonData.LogUserId + "', '" + CommonData.CurrentDate + "')";
                }
                else
                {

                    sqlText = " UPDATE Customer_Mas SET  " +
                       " cm_village='" + txtVillage.Text + "".Trim() +
                       "', cm_farmer_name ='" + txtCustomerName.Text + "".Trim() +
                       "', cm_so_fo='" + cbRelation.Text.ToString() +
                       "', cm_forg_name='" + txtRelationName.Text + "'" +
                       ", cm_mobile_number='" + txtMobileNo.Text + "" +
                       "', cm_land_line_no='" + txtLanLineNo.Text + "".Trim() +
                       "', cm_mandal='" + txtMandal.Text + "" +
                       "', cm_district='" + txtDistrict.Text +
                       "', cm_state='" + txtState.Text +
                       "', cm_pin='" + txtPin.Text +
                       "', cm_dob='" + strDOB +
                       "', cm_marriage_date='" + strMarDate +
                       "', cm_age=" + strAge +
                       ", cm_house_no='" + txtHouseNo.Text +
                       "', cm_landmark='" + txtLandMark.Text +
                       "', CM_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                       "', CM_LAST_MODIFIED_DATE='" + CommonData.CurrentDate +
                       "' WHERE cm_farmer_id='" + strFormerid + "'";
                }
                intRec = 0;
                //objSQLDB = new SQLDB();
                intRec = objSQLDB.ExecuteSaveData(sqlText);
                //objSQLDB = null;
            }
            catch (Exception ex)
            {
                intRec = 0;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objData = null;
                objSQLDB = null;
            }
            return intRec;
        }

        private void gvProductDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            objData = new InvoiceDB();
            if (e.ColumnIndex == 5)
            {
                if (Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value) != "")
                {
                    string[] strRatePoints = objData.getProductRatePoints(gvProductDetails.Rows[e.RowIndex].Cells["ProductId"].Value.ToString(), Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value.ToString()), Convert.ToDateTime(dtpInvoiceDate.Value.ToString("dd/MM/yyyy"))).Split('@');
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
                    FillFreeProductToGrid(gvProductDetails.Rows[e.RowIndex].Cells["Brand"].Value.ToString(), gvProductDetails.Rows[e.RowIndex].Cells["ProductId"].Value.ToString(), gvProductDetails.Rows[e.RowIndex].Cells["MainProduct"].Value.ToString(), Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value), Convert.ToInt32(gvProductDetails.Rows[e.RowIndex].Cells["SLNO"].Value));
                }
                else
                    gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value = string.Empty;
            }
            if (e.ColumnIndex == 6)
            {
                if (gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value != gvProductDetails.Rows[e.RowIndex].Cells["DBRate"].Value)
                {
                    try
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
                    catch
                    {

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
            txtReceivedAmt.Text = txtInvAmt.Text;
            TotalMainProducts();
        }

        private void txtCustomerName_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtCustomerName.Text.Length > 0)
                {
                    cbCustomer.DataSource = null;
                    cbCustomer.Items.Clear();
                    if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                    {
                        blCustomerSearch = true;
                        if (FindInputCustomerSearch() == false)
                        {
                            FillCustomerFarmerData(txtCustomerName.Text, "");
                        }
                    }

                }

            }
            catch //(Exception ex)
            {

            }
            if (e.KeyValue == 8)
            {
                if (this.txtCustomerName.TextLength >= 2)
                    FillCustomerFarmerData(Convert.ToString(txtCustomerName.Text.Trim()), "");
                this.txtCustomerName.SelectionStart = this.txtCustomerName.TextLength;
            }

        }
        //private void FillFreeProductToGrid(string sProdCat, string sProdId, string sMainProd, Int16 intQty, Int16 intInvDetlSlno)
        //{
        //    bool isItemExisted = false;
        //    objData = new InvoiceDB();
        //    DataTable dt = new DataTable();
        //    int totRows = dgvFreeProduct.Rows.Count;
        //    for (int nRow = 0; nRow < totRows; nRow++)
        //    {

        //        if (dgvFreeProduct.Rows[nRow].Cells["MainProductId"].Value.ToString().Trim() == sProdId.ToString().Trim())
        //        {
        //            dgvFreeProduct.Rows.RemoveAt(nRow);
        //            totRows = totRows - 1;
        //            nRow = nRow - 1;
        //        }

        //    }

        //    if (isItemExisted == false)
        //    {
        //        dt = objData.InvFreeProduct_Get(sProdId, intQty);
        //        if (dt.Rows.Count > 0)
        //        {
        //            if (Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0]["OFFER_FROM_DATE"]).ToString("dd/MM/yyyy")) <= Convert.ToDateTime(dtpInvoiceDate.Value.ToString("dd/MM/yyyy")) && dt.Rows[0]["OFFER_TO_DATE"].ToString() == "")
        //            {
        //                intMainProductRemainder = intQty;// This will take max sold units offer item
        //                foreach (DataRow dr in dt.Rows)
        //                {
        //                    if (Convert.ToInt16(dr["SOLD_UNITS"].ToString()) <= intQty && intMainProductRemainder >= 0)
        //                    {
        //                        dgvFreeProduct.Rows.Add();
        //                        int intCurRow = dgvFreeProduct.Rows.Count - 1;
        //                        dgvFreeProduct.Rows[intCurRow].Cells["FreeSlNo"].Value = intCurRow + 1;
        //                        dgvFreeProduct.Rows[intCurRow].Cells["MainProductIdSlNo"].Value = intInvDetlSlno;
        //                        dgvFreeProduct.Rows[intCurRow].Cells["MainProductId"].Value = sProdId;
        //                        dgvFreeProduct.Rows[intCurRow].Cells["FreeBrand"].Value = dr["CATEGORY_NAME"] + ""; ;
        //                        dgvFreeProduct.Rows[intCurRow].Cells["FreeMainProduct"].Value = sMainProd;
        //                        dgvFreeProduct.Rows[intCurRow].Cells["OfferNumber"].Value = dr["OFFER_NUMBER"] + "";
        //                        dgvFreeProduct.Rows[intCurRow].Cells["FreeProduct"].Value = dr["product_name"] + "";
        //                        dgvFreeProduct.Rows[intCurRow].Cells["SoldQty"].Value = dr["SOLD_UNITS"].ToString();
        //                        dgvFreeProduct.Rows[intCurRow].Cells["FreeQty"].Value = CaliculateFreeProduct(intQty, Convert.ToInt16(dr["FREE_UNITS"].ToString()), Convert.ToInt16(dr["SOLD_UNITS"].ToString()));
        //                        dgvFreeProduct.Rows[intCurRow].Cells["FreeProductId"].Value = dr["FREE_PRODUCTID"];
        //                    }

        //                }
        //            }
        //        }
        //    }
        //    SetFreeGridSlNo();
        //    TotalFreeProducts();
        //}
        private void FillFreeProductToGrid(string sProdCat, string sProdId, string sMainProd, double intQty, Int32 intInvDetlSlno)
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

            }

            if (isItemExisted == false)
            {
                double OfferSoldUnits = 0;
                dt = objData.InvFreeProduct_Get(sProdId, intQty);
                if (dt.Rows.Count > 0)
                {
                    string date = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0]["OFFER_FROM_DATE"]).ToString()).ToString();
                    string date1 = Convert.ToDateTime(dtpInvoiceDate.Value.ToString("dd/MM/yyyy")).ToString();
                    string date2 = dt.Rows[0]["OFFER_TO_DATE"].ToString();

                    if (Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0]["OFFER_FROM_DATE"]).ToString()) <= Convert.ToDateTime(dtpInvoiceDate.Value.ToString()) && dt.Rows[0]["OFFER_TO_DATE"].ToString() == "")
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
                remainder = Convert.ToInt16(intQty % OfferSoldUnits);
                while (remainder.ToString() != "0")
                {
                    dt.Clear();
                    //int OfferSoldUnits = 0;
                    //if (intQty % OfferSoldUnits != "0")
                    dt = objData.InvFreeProduct_Get(sProdId, remainder);
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0]["OFFER_FROM_DATE"]).ToString()) <= Convert.ToDateTime(dtpInvoiceDate.Value.ToString("dd/MM/yyyy")) && dt.Rows[0]["OFFER_TO_DATE"].ToString() == "")
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

                                        dgvFreeProduct.Rows[intCurRow].Cells["FreeQty"].Value = Convert.ToInt16(dgvFreeProduct.Rows[intCurRow].Cells["FreeQty"].Value) + CaliculateFreeProduct(remainder, Convert.ToInt16(dr["FREE_UNITS"].ToString()), Convert.ToInt16(dr["SOLD_UNITS"].ToString()));
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
            double totFree = 0;
            for (int nRow = 0; nRow < dgvFreeProduct.Rows.Count; nRow++)
            {

                if (dgvFreeProduct.Rows[nRow].Cells["FreeQty"].Value.ToString() != "")
                {
                    totFree += Convert.ToDouble(dgvFreeProduct.Rows[nRow].Cells["FreeQty"].Value.ToString());
                }

            }
            txtFreeProductQty.Text = totFree.ToString();
            if (txtMainProductQty.Text != "" && txtFreeProductQty.Text != "")
                txtTotalQty.Text = Convert.ToDouble(Convert.ToDouble(txtMainProductQty.Text.ToString()) + Convert.ToDouble(txtFreeProductQty.Text.ToString())).ToString("f");
        }

        private void FillInvoiceFreeProduct()
        {
            //bool isItemExisted = false;
            objData = new InvoiceDB();
            DataTable dt = new DataTable();
            dt = objData.InvoiceDetailFreeItems_Get(Convert.ToInt32(txtInvoiceNo.Text));
            if (dt.Rows.Count == 0)
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
            if (Convert.ToInt32(Convert.ToString(txtInvoiceNo.Text + "0")) > 0)
            {
                int intInvNo = Convert.ToInt32(Convert.ToString(txtInvoiceNo.Text));
                ClearForm(this);
                txtInvoiceNo.Text = intInvNo.ToString();
                FillInvOrBultInData(Convert.ToInt32(txtInvoiceNo.Text), "");
            }
        }

        private bool CheckOrderInvoiceExist(int nInvNo, string sOrderNo)
        {
            Hashtable ht;
            DataTable InvH;
            DataTable InvD;
            DataSet ds = new DataSet();
            try
            {
                IsModifyInvoice = false;
                IsModifyBulletIn = false;
                objData = new InvoiceDB();
                ht = objData.GetInvoiceData(CommonData.StateCode, CommonData.BranchCode, nInvNo, sOrderNo);

                InvH = (DataTable)ht["InvHead"];
                InvD = (DataTable)ht["InvDetail"];
                if (InvH.Rows.Count == 0)
                {
                    IsBiltinInvoice = true;
                    ht = objData.InvoiceBulitin_Get(CommonData.StateCode, CommonData.BranchCode, nInvNo, sOrderNo);
                    InvH = (DataTable)ht["InvHead"];
                    InvD = (DataTable)ht["InvDetail"];
                }

                if (InvH.Rows.Count == 0)
                    IsInvoiceExist = false;
                else
                    IsInvoiceExist = true;
                return IsInvoiceExist;
            }
            catch// (Exception ex)
            {
                objData = null;
                ht = null;
                InvH = null;
                InvD = null;
                return IsInvoiceExist;
            }
            finally
            {
                objData = null;
                ht = null;
                InvH = null;
                InvD = null;
                ds = null;
            }
        }

        private void FillInvOrBultInData(int nInvNo, string sOrderNo)
        {
            Hashtable ht;
            DataTable InvH;
            DataTable InvD;
            DataSet ds = new DataSet();
            objSPDB = new StockPointDB();
            DataTable dtSPHead = new DataTable();
            try
            {
                IsModifyInvoice = false;
                IsModifyBulletIn = false;
                IsSaleOrder = false;
                objData = new InvoiceDB();
                ht = objData.GetInvoiceData(CommonData.StateCode, CommonData.BranchCode, nInvNo, sOrderNo);

                InvH = (DataTable)ht["InvHead"];
                InvD = (DataTable)ht["InvDetail"];
                if (InvH.Rows.Count == 0)
                {
                    IsBiltinInvoice = true;
                    ht = objData.InvoiceBulitin_Get(CommonData.StateCode, CommonData.BranchCode, nInvNo, sOrderNo);
                    InvH = (DataTable)ht["InvHead"];
                    InvD = (DataTable)ht["InvDetail"];

                    if (InvH.Rows.Count == 0)
                    {
                        ds = objData.SalesOrderForBuiltInAndInvoice_Get(txtOrderNo.Text.ToString().Trim());
                        InvH = ds.Tables[0];
                        InvD = ds.Tables[1];
                        if (InvH.Rows.Count == 0)
                        { IsSaleOrder = false; }
                        else
                        {
                            IsSaleOrder = true;
                            if (ds.Tables[0].Rows[0]["DocMonth"].ToString().ToUpper() == CommonData.DocMonth.ToUpper())
                            {
                                MessageBox.Show("Allready Entered to Advance register in this month","SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                btnCancel_Click(null, null);
                                txtOrderNo.Text = "";
                                return;
                            }
                        }
                        ds = null;
                    }
                    else
                        IsModifyBulletIn = true;
                }
                else
                {
                    IsBiltinInvoice = false;
                }              

                FillInvoiceData(InvH, InvD);
                FillInvoiceFreeProduct();

                
                if (InvH.Rows.Count == 0)
                {
                    txtInvoiceNo.Text = GenerateNewInvoiceNo().ToString();
                    txtEcodeSearch.Enabled = true;
                    cbEcode.Enabled = true;
                    btnDelete.Enabled = true;
                    btnSave.Enabled = true;
                }
                else
                {
                    if (CommonData.LogUserId.ToUpper() != "ADMIN" && CommonData.LogUserRole.ToString() != "MANAGEMENT")
                    {
                        if (IsBiltinInvoice == false)
                        {
                            //txtEcodeSearch.Enabled = false;
                            //cbEcode.Enabled = false;
                            btnDelete.Enabled = false;
                            //btnSave.Enabled = false;
                        }
                        else if (IsModifyBulletIn == true)
                        {
                            txtEcodeSearch.Enabled = false;
                            cbEcode.Enabled = false;
                            btnSave.Enabled = true;
                        }
                    }
                    else
                    {
                        txtEcodeSearch.Enabled = true;
                        cbEcode.Enabled = true;
                        btnDelete.Enabled = true;
                        btnSave.Enabled = true;
                    }
                }

                if (txtOrderNo.Text.Length > 0)
                {
                    dtSPHead = objSPDB.Get_SPInvoiceHeadDetails(CommonData.CompanyCode, CommonData.BranchCode, "", 0, Convert.ToInt32(txtInvoiceNo.Text), txtOrderNo.Text).Tables[0];

                    if (dtSPHead.Rows.Count > 0)
                    {
                        btnSave.Enabled = false;
                        btnDelete.Enabled = false;
                        txtEcodeSearch.Enabled = false;
                        cbEcode.Enabled = false;
                        txtDocMonth.Text = CommonData.DocMonth.ToUpper();
                        MessageBox.Show("Invoice is Generated against OrderNo (" + txtOrderNo.Text + ")  at Stock Point. \n Please Contact " + dtSPHead.Rows[0]["SpName"].ToString() + " Stockpoint", "SSERP-Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }


            }
            catch (Exception ex)
            {
                objData = null;
                ht = null;
                InvH = null;
                InvD = null;
                MessageBox.Show(ex.Message, "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objData = null;
                ht = null;
                InvH = null;
                InvD = null;
                ds = null;
            }
        }

        private void FillInvoiceData(DataTable dtInvH, DataTable dtInvD)
        {
           
            try
            {
                if (dtInvH.Rows.Count > 0)
                {  
                    if (IsBiltinInvoice == false)
                        IsModifyInvoice = true;
                    if (dtInvH.Rows[0]["invoice_number"] + "" != "")
                    {
                        intInvoiceNo = Convert.ToInt32(dtInvH.Rows[0]["invoice_number"]);
                        txtInvoiceNo.Text = dtInvH.Rows[0]["invoice_number"] + "";
                        dtpInvoiceDate.Value = Convert.ToDateTime(dtInvH.Rows[0]["invoice_date"]);
                    }
                    else
                    {
                        txtInvoiceNo.Text = GenerateNewInvoiceNo().ToString();
                    }
                    if ((IsBiltinInvoice == true || IsModifyInvoice == true) && IsSaleOrder == false)
                        txtDocMonth.Text = dtInvH.Rows[0]["document_month"] + "";
                    else
                        txtDocMonth.Text = CommonData.DocMonth.ToUpper();
                    dtpOrderDate.Value = Convert.ToDateTime(dtInvH.Rows[0]["order_date"].ToString());
                    strFormerid = Convert.ToString(dtInvH.Rows[0]["farmer_ID"] + "");
                    txtEcodeSearch.Text = dtInvH.Rows[0]["eora_code"] + "";
                    txtEcodeSearch_KeyUp(null, null);
                    strECode = dtInvH.Rows[0]["eora_code"] + "";
                    if (dtInvH.Rows[0]["FieldSupBy"].ToString() == "0")
                        txtFSEcode.Text = "";
                    else
                        txtFSEcode.Text = dtInvH.Rows[0]["FieldSupBy"] + "";
                    txtFSEcode_KeyUp(null, null);
                    strFSECode = dtInvH.Rows[0]["FieldSupBy"] + "";
                    cbEcode.SelectedValue = strECode;
                    txtHouseNo.Text = Convert.ToString(dtInvH.Rows[0]["cm_house_no"]);
                    txtLandMark.Text = Convert.ToString(dtInvH.Rows[0]["cm_landmark"]);
                    txtVillage.Text = dtInvH.Rows[0]["cm_village"] + "";
                    txtMandal.Text = dtInvH.Rows[0]["cm_mandal"] + "";
                    txtDistrict.Text = dtInvH.Rows[0]["cm_district"] + "";
                    txtState.Text = dtInvH.Rows[0]["CM_STATE"] + "";
                    strStateCode = dtInvH.Rows[0]["state_code"] + "";
                    txtPin.Text = dtInvH.Rows[0]["cm_pin"] + "";
                    txtCustomerName.Text = dtInvH.Rows[0]["cm_farmer_name"] + "";
                    cbRelation.Text = dtInvH.Rows[0]["cm_so_fo"] + "";
                    txtRelationName.Text = dtInvH.Rows[0]["cm_forg_name"] + "";
                    txtMobileNo.Text = dtInvH.Rows[0]["cm_mobile_number"] + "";
                    txtLanLineNo.Text = dtInvH.Rows[0]["cm_land_line_no"] + "";
                    txtOrderNo.Text = dtInvH.Rows[0]["order_number"] + "";
                    if (IsSaleOrder == false)
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

                    dtpOrderDate.Value = Convert.ToDateTime(dtInvH.Rows[0]["order_date"].ToString());
                    txtCustomerid.Text = strFormerid;
                    if (dtInvH.Rows[0]["cm_marriage_date"].ToString() != "")
                    {
                        if (dtInvH.Rows[0]["cm_marriage_date"].ToString().Substring(0, 10) + "" == "01/01/1900" || dtInvH.Rows[0]["cm_marriage_date"].ToString().Substring(0, 10) + "" == "01-01-1900")
                            dtpMarrageDate.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyy"));
                        else
                            dtpMarrageDate.Value = Convert.ToDateTime(Convert.ToDateTime(dtInvH.Rows[0]["cm_marriage_date"] + "").ToString("dd/MM/yyy")); ;
                    }

                    if (dtInvH.Rows[0]["cm_dob"].ToString() != "")
                    {
                        if (dtInvH.Rows[0]["cm_dob"].ToString().Substring(0, 10) + "" == "01/01/1900" || dtInvH.Rows[0]["cm_dob"].ToString().Substring(0, 10) + "" == "01-01-1900")
                            dtpDateOfBirth.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyy"));
                        else
                            dtpDateOfBirth.Value = Convert.ToDateTime(Convert.ToDateTime(dtInvH.Rows[0]["cm_dob"] + "").ToString("dd/MM/yyy"));
                    }
                    txtAge.Text = dtInvH.Rows[0]["cm_age"] + "";

                    FillInvoiceDetail(dtInvD);

                }
                else
                {
                    btnSave.Enabled = true;
                    btnDelete.Enabled = true;
                    txtEcodeSearch.Enabled = true;
                    cbEcode.Enabled = true;
                    txtDocMonth.Text = CommonData.DocMonth.ToUpper();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                dtInvH = null;
            }
        }

        private void SaveDeleteEnableDisable()
        {
            try
            {
                if (Convert.ToDateTime(Convert.ToDateTime(dtpInvoiceDate.Value).ToString("dd/MM/yyyy")) < Convert.ToDateTime(CommonData.DocFDate) ||
                        Convert.ToDateTime(Convert.ToDateTime(dtpInvoiceDate.Value).ToString("dd/MM/yyyy")) > Convert.ToDateTime(CommonData.DocTDate))
                {
                    btnSave.Enabled = false;
                    btnDelete.Enabled = false;
                }
                else
                {
                    btnSave.Enabled = true;
                    btnDelete.Enabled = true;
                }
            }
            catch
            {
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

                        if (Convert.ToInt32(cellRate.Value) == 0)
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
                        if (Convert.ToInt32(dt.Rows[i]["SIDPoints"]) >= 0)
                            cellPoints.Value = Convert.ToDouble(dt.Rows[i]["SIDPoints"]).ToString("f");
                        else
                            cellPoints.Value = Convert.ToDouble(dt.Rows[i]["TIPPoints"]).ToString("f");

                        if (cellPoints.Value.ToString() == "")
                            cellPoints.Value = Convert.ToDouble(dt.Rows[i]["TIPPoints"]).ToString("f");

                        tempRow.Cells.Add(cellPoints);

                        DataGridViewCell cellDBRate = new DataGridViewTextBoxCell();
                        cellDBRate.Value = cellRate.Value;
                        tempRow.Cells.Add(cellDBRate);

                        if (dt.Rows[i]["ProductType"].ToString() == "Combi")
                            tempRow.Cells[2].Style.BackColor = Color.FromArgb(192, 192, 255);
                        else if (dt.Rows[i]["ProductType"].ToString() == "Free")   //Free
                            tempRow.Cells[2].Style.BackColor = Color.MediumTurquoise;

                        intRow = intRow + 1;
                        gvProductDetails.Rows.Add(tempRow);
                    }
                }
                TotalMainProducts();
                TotalFreeProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dt = null;
            }
        }

       

        private void txtVillage_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtVillageSearch.Text.Length == 0)
                {
                    cbVillage.DataSource = null;
                    cbVillage.DataBindings.Clear();
                    cbVillage.Items.Clear();
                    if (btnSave.Enabled == true)
                        ClearVillageDetails();
                }
                else if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                {
                    if (FindInputAddressSearch() == false)
                    {
                        FillAddressData(txtVillageSearch.Text);
                    }
                }
            }
            catch// (Exception ex)
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

        private void txtCustomerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtRelationName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
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

                
                TotalFreeProducts();
                TotalMainProducts();
                txtInvAmt.Text = GetInvoiceAmt().ToString("f");
                txtAdvanceAmt_KeyUp(null, null);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtInvoiceNo.Text.Length > 0)
                {
                    Security objSecur = new Security();
                    if (objSecur.CanModifyDataUserAsPerBackDays(Convert.ToDateTime(dtpInvoiceDate.Value)) == false)
                    {
                        MessageBox.Show("You cannot manipulate backdays data!\n If you want to modify, Contact to IT-Department", "Invoice");
                        txtOrderNo.Focus();
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Do you want to Delete " + txtInvoiceNo.Text + " invoice?",
                                               "CRM", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            objSQLDB = new SQLDB();
                            string strDelete = " DELETE FROM SERVICES_TNA WHERE TNA_COMPANY_CODE='" + CommonData.CompanyCode + "'" +
                                              " AND TNA_BRANCH_CODE='" + CommonData.BranchCode + "'" +
                                              " AND TNA_INVOICE_NUMBER=" + txtInvoiceNo.Text +
                                              " AND TNA_FIN_YEAR='" + CommonData.FinancialYear + "'";

                            strDelete += " DELETE from Sales_Inv_Detl_FREE" +
                                               " WHERE sidf_company_code='" + CommonData.CompanyCode +
                                               "' AND sidf_branch_code='" + CommonData.BranchCode +
                                               "' AND sidf_invoice_number=" + txtInvoiceNo.Text +
                                               "  AND sidf_fin_year='" + CommonData.FinancialYear + "'";

                            strDelete += " DELETE from Sales_Inv_Detl " +
                                               " WHERE SID_COMPANY_CODE='" + CommonData.CompanyCode +
                                               "' AND  sid_branch_code='" + CommonData.BranchCode +
                                               "' AND sid_invoice_number=" + txtInvoiceNo.Text +
                                               "  AND sid_fin_year='" + CommonData.FinancialYear + "'";

                            strDelete += " DELETE from Sales_Inv_Head " +
                                                " WHERE SIH_COMPANY_CODE='" + CommonData.CompanyCode +
                                                "' AND sih_branch_code='" + CommonData.BranchCode +
                                                "' AND sih_invoice_number=" + txtInvoiceNo.Text +
                                                "  AND sih_fin_year='" + CommonData.FinancialYear + "'";

                            //int intRec = objSQLDB.ExecuteSaveData(strDelete);

                            //if (intRec > 0)
                            //{
                                //MessageBox.Show("Invoice " + intInvoiceNo + " Deleted ");
                                strDelete += " DELETE from Sales_Inv_Detl_FREE" +
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

                                strDelete += " DELETE FROM SALES_INV_SP_HEAD " +
                                                " where SISH_BRANCH_CODE='" + CommonData.BranchCode +
                                                "' and SISH_INVOICE_NO=" + txtInvoiceNo.Text +
                                                " and SISH_FIN_YEAR='" + CommonData.FinancialYear + "'";
                                                

                                int intRec = objSQLDB.ExecuteSaveData(strDelete);

                                if (intRec > 0)
                                {
                                    IsModifyInvoice = false;
                                    MessageBox.Show("Invoice " + intInvoiceNo + " Deleted ", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //ClearForm(this);
                                }
                                ClearForm(this);
                            //}
                            if (chkBullet.CheckState == CheckState.Unchecked)
                                txtInvoiceNo.Text = GenerateNewInvoiceNo().ToString();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillAddressComboBox(DataTable dt)
        {
            DataTable dataTable = new DataTable("Village");

            dataTable.Columns.Add("StateID", typeof(String));
            dataTable.Columns.Add("Panchayath", typeof(String));
            dataTable.Columns.Add("Mandal", typeof(String));
            dataTable.Columns.Add("District", typeof(String));
            dataTable.Columns.Add("State", typeof(String));
            dataTable.Columns.Add("Pin", typeof(String));
            cbVillage.DataBindings.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
                dataTable.Rows.Add(new String[] { dt.Rows[i]["CDState"] + 
                     "", dt.Rows[i]["PANCHAYAT"] + 
                     "", dt.Rows[i]["MANDAL"] + 
                     "", dt.Rows[i]["DISTRICT"] + 
                     "", dt.Rows[i]["STATE"] + "", dt.Rows[i]["PIN"] + ""});


            cbVillage.DataSource = dataTable;
            cbVillage.DisplayMember = "Panchayath";
            cbVillage.ValueMember = "StateID";
        }

        private void FillCustomerComboBox(DataTable dt)
        {
            DataTable dataCustomer = new DataTable("Customer");
            dataCustomer.Columns.Add("farmer_ID", typeof(String));
            dataCustomer.Columns.Add("farmer_name", typeof(String));
            dataCustomer.Columns.Add("forg_name", typeof(String));
            dataCustomer.Columns.Add("village", typeof(String));

            cbCustomer.DataBindings.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
                dataCustomer.Rows.Add(new String[] { dt.Rows[i]["cm_farmer_ID"] + 
                     "", dt.Rows[i]["cm_farmer_name"] + "", dt.Rows[i]["cm_forg_name"] + 
                     "", dt.Rows[i]["cm_village"] + ""});

            cbCustomer.DataSource = dataCustomer;
            cbCustomer.DisplayMember = "farmer_name";
            cbCustomer.ValueMember = "farmer_ID";
        }

        private bool FindInputAddressSearch()
        {
            bool blFind = false;
            try
            {
                for (int i = 0; i < this.cbVillage.Items.Count; i++)
                {
                    string strItem = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[1] + "".ToString().Trim();
                    if (strItem.IndexOf(txtVillageSearch.Text) > -1)
                    {
                        blFind = true;
                        cbVillage.SelectedIndex = i;
                        txtVillage.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[1] + "";
                        txtMandal.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[2] + "";
                        txtDistrict.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[3] + "";
                        txtState.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[4] + "";
                        txtPin.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[5] + "";
                        strStateCode = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[0] + "";
                        break;
                    }
                    break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
            }
            return blFind;
        }

        private bool FindInputCustomerSearch()
        {
            bool blFind = false;
            objData = new InvoiceDB();
            try
            {
                for (int i = 0; i < this.cbCustomer.Items.Count; i++)
                {
                    string strItem = ((System.Data.DataRowView)(this.cbCustomer.Items[i])).Row.ItemArray[1] + "".ToString().Trim();
                    if (strItem.IndexOf(txtCustomerName.Text) > -1)
                    {
                        DataSet ds = new DataSet();
                        blFind = true;
                        cbCustomer.SelectedIndex = i;
                        ds = objData.InvCustomerSearch_Get(strItem, txtVillage.Text.ToString(), txtMandal.Text.ToString(), txtState.Text.ToString());
                        DataTable dt = ds.Tables[0];

                        if (dt.Rows.Count == 1)
                        {
                            strFormerid = Convert.ToString(dt.Rows[0]["cm_farmer_ID"]);
                            txtCustomerid.Text = strFormerid;
                            //txtCustomerName.Text = dt.Rows[0]["cm_farmer_name"] + "";
                            txtRelationName.Text = dt.Rows[0]["cm_forg_name"] + "";
                            cbRelation.Text = dt.Rows[0]["cm_so_fo"] + "";
                            txtHouseNo.Text = Convert.ToString(dt.Rows[0]["cm_house_no"]);
                            txtLandMark.Text = Convert.ToString(dt.Rows[0]["cm_landmark"]);
                            txtMobileNo.Text = dt.Rows[0]["cm_mobile_number"] + "";
                            txtLanLineNo.Text = dt.Rows[0]["cm_land_line_no"] + "";

                            if (dt.Rows[0]["cm_marriage_date"].ToString().Substring(0, 10) + "" == "01/01/1900" || dt.Rows[0]["cm_marriage_date"].ToString().Substring(0, 10) + "" == "01-01-1900")
                                dtpMarrageDate.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyy"));
                            else
                                dtpMarrageDate.Value = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0]["cm_marriage_date"] + "").ToString("dd/MM/yyy"));

                            if (dt.Rows[0]["cm_dob"].ToString().Substring(0, 10) + "" == "01/01/1900" || dt.Rows[0]["cm_dob"].ToString().Substring(0, 10) + "" == "01-01-1900")
                                dtpDateOfBirth.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyy"));
                            else
                                dtpDateOfBirth.Value = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0]["cm_dob"] + "").ToString("dd/MM/yyy"));

                            txtAge.Text = dt.Rows[0]["cm_age"] + "";

                            break;
                        }
                    }
                    break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                objData = null;
            }
            return blFind;
        }

        private void cbVillage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbVillage.SelectedIndex > -1)
            {
                if (this.cbVillage.Items[cbVillage.SelectedIndex].ToString() != "")
                {
                    txtVillage.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[1] + "";
                    txtMandal.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[2] + "";
                    txtDistrict.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[3] + "";
                    txtState.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[4] + "";
                    txtPin.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[5] + "";
                    strStateCode = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[0] + "";
                }
            }

        }


        private void txtAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            dtpDateOfBirth.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyy"));
        }

        private void cbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Master objMData = new Master();
            try
            {
                if (cbCustomer.SelectedIndex > -1)
                {
                    if (this.cbCustomer.Items[cbCustomer.SelectedIndex].ToString() != "")
                    {
                        DataSet ds = new DataSet();
                        ds = objMData.CustomerData_Get(((System.Data.DataRowView)(cbCustomer.Items[cbCustomer.SelectedIndex])).Row.ItemArray[0].ToString());
                        DataTable dt = ds.Tables[0];

                        if (dt.Rows.Count == 1)
                        {
                            strFormerid = Convert.ToString(dt.Rows[0]["cm_farmer_ID"]);
                            txtCustomerid.Text = strFormerid;

                            //txtCustomerName.Text = dt.Rows[0]["cm_farmer_name"] + "";
                            txtRelationName.Text = dt.Rows[0]["cm_forg_name"] + "";
                            cbRelation.Text = dt.Rows[0]["cm_so_fo"] + "";
                            txtHouseNo.Text = Convert.ToString(dt.Rows[0]["cm_house_no"]);
                            txtLandMark.Text = Convert.ToString(dt.Rows[0]["cm_landmark"]);
                            txtMobileNo.Text = dt.Rows[0]["cm_mobile_number"] + "";
                            txtLanLineNo.Text = dt.Rows[0]["cm_land_line_no"] + "";

                            if (dt.Rows[0]["cm_marriage_date"].ToString().Substring(0, 10) + "" == "01/01/1900" || dt.Rows[0]["cm_marriage_date"].ToString().Substring(0, 10) + "" == "01-01-1900")
                                dtpMarrageDate.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyy"));
                            else
                                dtpMarrageDate.Value = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0]["cm_marriage_date"] + "").ToString("dd/MM/yyy"));

                            if (dt.Rows[0]["cm_dob"].ToString().Substring(0, 10) + "" == "01/01/1900" || dt.Rows[0]["cm_dob"].ToString().Substring(0, 10) + "" == "01-01-1900")
                                dtpDateOfBirth.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyy"));
                            else
                                dtpDateOfBirth.Value = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0]["cm_dob"] + "").ToString("dd/MM/yyy"));

                            txtAge.Text = dt.Rows[0]["cm_age"] + "";
                        }
                        ds = null;
                        dt = null;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                objMData = null;
                cbCustomer.CausesValidation = false;
            }
        }

        private void ClearVillageDetails()
        {
            txtVillage.Text = "";
            txtMandal.Text = "";
            txtDistrict.Text = "";
            txtState.Text = "";
            txtPin.Text = "";
        }


        private void cbEcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEcode.SelectedIndex > -1)
            {
                strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
            }

            if (strECode != "")
                try { Convert.ToInt32(strECode); }
                catch { strECode = "0"; }
            else
                strECode = "0";
            FSEcodeSearch();
            FillFSEmployeeData();
        }

        private void ClearForm(System.Windows.Forms.Control parent)
        {
            intInvoiceNo = 0;
            strFormerid = "";
            strVillage = string.Empty;
            foreach (System.Windows.Forms.Control ctrControl in parent.Controls)
            {
                //Loop through all controls 
                if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.TextBox)))
                {
                    //Check to see if it's a textbox 
                    if ((((System.Windows.Forms.TextBox)ctrControl).Name != "txtEcodeSearch") &&
                        (((System.Windows.Forms.TextBox)ctrControl).Name != "txtOrderNo") &&
                        (((System.Windows.Forms.TextBox)ctrControl).Name != "txtInvoiceNo"))
                    {
                        ((System.Windows.Forms.TextBox)ctrControl).Text = string.Empty;
                        txtEcodeSearch_KeyUp(null, null);
                    }


                    //If it is then set the text to String.Empty (empty textbox) 
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.MaskedTextBox)))
                {

                    //Check to see if it's a textbox 
                    if (((System.Windows.Forms.MaskedTextBox)ctrControl).Name != "meOrderDate")
                        ((System.Windows.Forms.MaskedTextBox)ctrControl).Text = string.Empty;
                    //If it is then set the text to String.Empty (empty textbox) 
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.RichTextBox)))
                {
                    ((System.Windows.Forms.RichTextBox)ctrControl).Text = string.Empty;
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.ComboBox)))
                {
                    
                        cbRelation.SelectedIndex = 0;
                    
                    
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.CheckBox)))
                {
                    if (((System.Windows.Forms.CheckBox)ctrControl).Name == "chkBullet")
                    {
                        cbInvoiceNo.SelectedIndex = -1;
                        gvProductDetails.Rows.Clear();
                        dgvFreeProduct.Rows.Clear();
                    }
                    else
                        ((System.Windows.Forms.CheckBox)ctrControl).Checked = false;
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.RadioButton)))
                {
                    ((System.Windows.Forms.RadioButton)ctrControl).Checked = false;
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.DataGridView)))
                {
                    //((System.Windows.Forms.DataGridView)ctrControl).Rows.Clear();
                    //FillProductData();

                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.DateTimePicker)))
                {
                    ((System.Windows.Forms.DateTimePicker)ctrControl).Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

                }
                if (ctrControl.Controls.Count > 0)
                {
                    ClearForm(ctrControl);
                }
            }
            lblCreditSale.Visible = false;
            txtCustomerid.Text = "";
            //cbEcode.SelectedIndex = -1;
            txtDocMonth.Text = CommonData.DocMonth.ToUpper();

            //txtOrderNo.Focus();
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
                ClearForm(this);
                FillInvOrBultInData(0, Convert.ToInt32(txtOrderNo.Text).ToString("00000"));
                
            }
        }

        private void btnVSearch_Click(object sender, EventArgs e)
        {
            VillageSearch VSearch = new VillageSearch("Invoice");
            VSearch.objFrmInvoice = this;
            VSearch.ShowDialog();
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
                if (Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value) != "")
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
            ProductSearchAll PSearch = new ProductSearchAll("Invoice");
            PSearch.objFrmInvoice = this;
            PSearch.ShowDialog();
        }
        private double GetInvoiceAmt()
        {
            double dbInvAmt = 0;
            for (int i = 0; i < gvProductDetails.Rows.Count; i++)
            {
                if (gvProductDetails.Rows[i].Cells["Amount"].Value.ToString() != "")
                {
                    if (Convert.ToDouble(gvProductDetails.Rows[i].Cells["Amount"].Value.ToString()) >= 1)
                        dbInvAmt += Convert.ToDouble(gvProductDetails.Rows[i].Cells["Amount"].Value);

                }

            }
            if (txtAdvanceAmt.Text != "0.00" && txtAdvanceAmt.Text != "")
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
            cbFSEName.DataSource = null;
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
                MessageBox.Show(ex.Message);
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

        private void chkBullet_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBullet.CheckState == CheckState.Checked)
            {
                cbInvoiceNo.Visible = true;
                ClearForm(this);
                FillBulletIns();
                cbInvoiceNo.SelectedIndex = -1;
            }
            else
            {
                cbInvoiceNo.Visible = false;
                cbInvoiceNo.Items.Clear();
                ClearForm(this);

                txtInvoiceNo.Text = GenerateNewInvoiceNo().ToString();
            }
        }

        private void cbInvoiceNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbInvoiceNo.SelectedIndex > -1)
            {
                setToolTipComboBox(cbInvoiceNo);
                FillInvOrBultInData(Convert.ToInt32(((SSCRM.ComboboxItem)(cbInvoiceNo.Items[cbInvoiceNo.SelectedIndex])).Value), "");
            }
        }

        private void setToolTipComboBox(ComboBox cbBox)
        {
            if (cbBox.SelectedIndex > -1)
            {
                ToolTip ttInv = new ToolTip();
                string strTT = cbBox.SelectedItem.ToString();
                ttInv.SetToolTip(cbInvoiceNo, strTT);
                ttInv.AutoPopDelay = 5000;
                ttInv.InitialDelay = 100;
                ttInv.ReshowDelay = 100;
                ttInv.ShowAlways = true;
            }
        }

        private void btnFreeClear_Click(object sender, EventArgs e)
        {
            dgvFreeProduct.Rows.Clear();
        }

        private void btnAddFreeProduct_Click(object sender, EventArgs e)
        {
            if (gvProductDetails.Rows.Count > 0)
            {
                AddFreeProducts freeProduct = new AddFreeProducts(gvProductDetails);
                freeProduct.objInvoice = this;
                freeProduct.ShowDialog();
            }
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
                        oAddFreeProducts.objInvoice = this;
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

        private void txtMainProductQty_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFSEcode_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtFSEcode.Text.ToString().Trim().Length > 0)
                FSEcodeSearch();
            else
                cbFSEName.DataSource = null;
        }

        private void FillFSEmployeeData()
        {
            //throw new NotImplementedException();
        }

        private void FSEcodeSearch()
        {
            if (txtFSEcode.Text.Length > 0)
            {
                objData = new InvoiceDB();
                DataSet dsEmp = null;
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    cbFSEName.DataSource = null;
                    cbFSEName.Items.Clear();
                    dsEmp = objData.InvLevelFSEcodeSearch_Get(CommonData.CompanyCode, CommonData.BranchCode, txtDocMonth.Text, txtFSEcode.Text.ToString(), Convert.ToInt32(strECode));
                    DataTable dtEmp = dsEmp.Tables[0];
                    if (dtEmp.Rows.Count > 0)
                    {
                        cbFSEName.DataSource = dtEmp;
                        cbFSEName.DisplayMember = "ENAME";
                        cbFSEName.ValueMember = "ECODE";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (cbFSEName.SelectedIndex > -1)
                    {
                        cbFSEName.SelectedIndex = 0;
                        strFSECode = ((System.Data.DataRowView)(cbFSEName.SelectedItem)).Row.ItemArray[0].ToString();
                    }
                    objData = null;
                    Cursor.Current = Cursors.Default;
                }
            }
            else
            {
                strFSECode = "0";
            }

        }

        private void cbFSEName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFSEName.SelectedIndex > -1)
            {
                strFSECode = ((System.Data.DataRowView)(cbFSEName.SelectedItem)).Row.ItemArray[0].ToString();
            }
        }

        private void txtFSEcode_Validated(object sender, EventArgs e)
        {
            FSEcodeSearch();
        }

       

      


    }
}
