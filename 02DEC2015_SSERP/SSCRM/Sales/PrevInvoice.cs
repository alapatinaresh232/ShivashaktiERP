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
    public partial class PrevInvoice : Form
    {
        private SQLDB objSQLDB = null;
        private InvoiceDB objData = null;
        private string strFormerid = string.Empty;
        private int intInvoiceNo = 0;
        private string strVillage = string.Empty;
        private string strDateOfBirth = string.Empty;
        private string strMarriageDate = string.Empty;
        public string strStateCode = string.Empty;
        private string strBranchCode = string.Empty;
        private string strECode = string.Empty;
        private bool blCustomerSearch = false;
        private bool blIsCellQty = true;
        private string strCreditSale = "NO";
        private bool IsModifyInvoice = false;
        private bool IsBiltinInvoice = false;

        public PrevInvoice()
        {
            InitializeComponent();
        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            //this.Location = new Point(Screen.PrimaryScreen.WorkingArea.X + 30, Screen.PrimaryScreen.WorkingArea.Y + 30);
            //this.StartPosition = FormStartPosition.CenterScreen;
            ClearForm(this);
            FillBranchData();
            FillEmployeeData();
            cbEcode.SelectedIndex = -1;
            cbRelation.SelectedIndex = 0;
            gvProductDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);
            cbFinYear.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm(this);
            //cbBranch.Enabled = true;
            //cbBranch.Focus();
            txtOrderNo.Text = string.Empty;
            IsModifyInvoice = false;
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

                //string strSCode = ((System.Data.DataRowView)(cbBranch.SelectedItem)).Row.ItemArray[0].ToString();
                //string[] strArr = strSCode.Split('@');
                //strSCode = strArr[1].ToString();

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
                    //if (btnSave.Enabled == true)
                    //FillProductData();
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
                dsEmp = objData.GetEcodeList(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth);
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
                        txtCustomerName.Text = dt.Rows[0]["cm_farmer_name"] + "";
                        txtRelationName.Text = dt.Rows[0]["cm_forg_name"] + "";
                        cbRelation.Text = dt.Rows[0]["cm_so_fo"] + "";
                        txtHouseNo.Text = Convert.ToString(dt.Rows[0]["cm_house_no"]);
                        txtLandMark.Text = Convert.ToString(dt.Rows[0]["cm_landmark"]);
                        //txtVillage.Text = dt.Rows[0]["cm_village"] + "";
                        //txtMandal.Text = dt.Rows[0]["cm_mandal"] + "";
                        //txtDistrict.Text = dt.Rows[0]["cm_district"] + "";
                        //txtState.Text = dt.Rows[0]["cm_state"] + "";
                        //strStateCode = dt.Rows[0]["sihp_state_code"] + "";
                        //txtPin.Text = dt.Rows[0]["cm_pin"] + "";
                        txtMobileNo.Text = dt.Rows[0]["cm_mobile_number"] + "";
                        txtLanLineNo.Text = dt.Rows[0]["cm_land_line_no"] + "";

                        if (dt.Rows[0]["cm_marriage_date"].ToString().Substring(0, 10) == "01/01/1900" || dt.Rows[0]["cm_marriage_date"].ToString().Substring(0, 10) == "01-01-1900")
                            meMarriageDate.Text = "";
                        else
                            meMarriageDate.Text = dt.Rows[0]["cm_marriage_date"].ToString() ;

                        if (dt.Rows[0]["cm_dob"].ToString().Substring(0, 10) == "01/01/1900" || dt.Rows[0]["cm_dob"].ToString().Substring(0, 10) == "01-01-1900")
                            meDataofBirth.Text = "";
                        else
                            meDataofBirth.Text = dt.Rows[0]["cm_dob"].ToString() ;

                        txtAge.Text = dt.Rows[0]["cm_age"] + "";
                        //if (btnSave.Enabled == true)
                        //FillProductData();

                    }
                    else if (dt.Rows.Count > 1)
                    {

                        FillCustomerComboBox(dt);

                    }
                    else
                    {
                        cbCustomer.DataSource = null;
                        cbCustomer.DataBindings.Clear();
                        cbCustomer.Items.Clear();
                        txtCustomerName.Text = "";
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

        
        private void btnSave_Click(object sender, EventArgs e)
        {
            int intSaved = 0;
            try
            {
                if (meMarriageDate.Text.IndexOf(" ") > -1)
                    strMarriageDate = "";
                else
                    strMarriageDate = meMarriageDate.Text;

                if (meDataofBirth.Text.IndexOf(" ") > -1)
                    strDateOfBirth = "";
                else
                    strDateOfBirth = meDataofBirth.Text;


                if (CheckData() == true)
                {
                    if (SaveCustomerData() >= 1)
                    {
                        if (SaveInvoiceHeadData() >= 1)
                            intSaved = SaveInvoiceDeatailData();
                    }
                    if (intSaved > 0)
                    {
                        intInvoiceNo = 0;
                        strFormerid = "";
                        ClearForm(this);
                        txtOrderNo.Text = string.Empty;
                        cbVillage.DataBindings.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Data not saved");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                IsModifyInvoice = false;
                //cbBranch.Enabled = true;
                //cbBranch.SelectedIndex = 0;
            }

        }

        private bool CheckData()
        {
            bool blValue = true;
            strCreditSale = "NO";
            if (Convert.ToString(txtOrderNo.Text).Length == 0)
            {
                MessageBox.Show("Enter Order number!");
                blValue = false;
                txtOrderNo.Focus();
                return blValue;
            }

            if (Convert.ToString(txtInvoiceNo.Text).Length == 0)
            {
                MessageBox.Show("Enter Invoice number!");
                blValue = false;
                txtInvoiceNo.Focus();
                return blValue;
            }
            if ((meInvoiceDate.Text.IndexOf(" ") >= 0) || (meInvoiceDate.Text.Length < 10))
            {
                MessageBox.Show("Enter Invoice date!");
                blValue = false;
                meInvoiceDate.Focus();
                return blValue;
            }
            else
            {
                if (Convert.ToInt32(Convert.ToDateTime(meInvoiceDate.Text).ToString("yyyy")) < 1950)
                {
                    MessageBox.Show("Enter valid  Date !");
                    blValue = false;
                    meInvoiceDate.CausesValidation = true;
                    meInvoiceDate.Focus();
                    return blValue;
                }
                if (Convert.ToDateTime(Convert.ToDateTime(meInvoiceDate.Text).ToString("dd/MM/yyyy")) > Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy")))
                {
                    MessageBox.Show("Date should be less than to day");
                    meInvoiceDate.CausesValidation = true;
                    blValue = false;
                    meInvoiceDate.Focus();
                    return blValue;
                }
                else if (Convert.ToDateTime(Convert.ToDateTime(meInvoiceDate.Text).ToString("dd/MM/yyyy")) < Convert.ToDateTime(CommonData.DocFDate) ||
                    Convert.ToDateTime(Convert.ToDateTime(meInvoiceDate.Text).ToString("dd/MM/yyyy")) > Convert.ToDateTime(CommonData.DocTDate))
                {
                    MessageBox.Show("Invoice date should be between " + CommonData.DocFDate + " and " + CommonData.DocTDate);
                    meInvoiceDate.CausesValidation = true;
                    blValue = false;
                    meInvoiceDate.Focus();
                    return blValue;
                }
            }



            if (cbEcode.SelectedIndex == -1)
            {
                MessageBox.Show("Enter Employee number!");
                blValue = false;
                cbEcode.Focus();
                return blValue;
            }
            if (Convert.ToString(txtVillage.Text).Length == 0 || txtVillage.Text.ToString().Trim() == "NOVILLAGE")
            {
                MessageBox.Show("Enter Village!");
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
            if (Convert.ToString(txtCustomerName.Text).Length == 0 || txtCustomerName.Text.ToString().Trim() == "NOVILLAGE")
            {
                MessageBox.Show("Enter Customer name!");
                blValue = false;
                txtCustomerName.Focus();
                return blValue;
            }
            bool blInvDtl = false;
            for (int i = 0; i < gvProductDetails.Rows.Count; i++)
            {
                if (Convert.ToString(gvProductDetails.Rows[i].Cells["Rate"].Value) != "" && Convert.ToString(gvProductDetails.Rows[i].Cells["Qty"].Value) != "")
                {
                    blInvDtl = true;
                }

            }

            if (blInvDtl == false)
            {
                blValue = false;
                MessageBox.Show("Enter product details");
                return false;
            }
            if (txtInvAmt.Text.ToString() == "")
            {
                MessageBox.Show("Please check invoice amount!");
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
                                       "CRM", MessageBoxButtons.YesNo);
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

            return blValue;

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
            try
            {
                if (IsModifyInvoice == false)
                {
                    string sqlDelete = "DELETE from SALES_INV_DETL_PREV" +
                                 " WHERE sidp_company_code='" + CommonData.CompanyCode +
                                     "' AND sidp_branch_code='" + CommonData.BranchCode +
                                     "' AND sidp_invoice_number=" + txtInvoiceNo.Text +
                                     "  AND sidp_fin_year='" + CommonData.FinancialYear + "'";

                    intRec = objSQLDB.ExecuteSaveData(sqlDelete);

                    sqlText = "UPDATE SALES_INV_HEAD_PREV set sihp_invoice_date='" + Convert.ToDateTime(meInvoiceDate.Text).ToString("dd/MMM/yyy") +
                            "', sihp_farmer_ID='" + strFormerid +
                            "', sihp_eora_code='" + strECode +
                            "', sihp_prod_pattern='Ppattern'" +
                            ", sihp_Document_month='" + CommonData.DocMonth +
                            "', sihp_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                            "', sihp_LAST_MODIFIED_DATE='" + CommonData.CurrentDate +
                            "', sihp_order_number='" + txtOrderNo.Text +
                            "', sihp_INVOICE_AMOUNT =" + txtInvAmt.Text +
                            ", sihp_ADVANCE_AMOUNT = " + txtAdvanceAmt.Text +
                            ", sihp_RECEIVED_AMOUNT = " + txtReceivedAmt.Text +
                            ", sihp_CREDIT_SALE = '" + strCreditSale +
                        //"', sihp_order_date='" + Convert.ToDateTime(meOrderDate.Text).ToString("dd/MMM/yyyy") +
                            "' WHERE sihp_invoice_number = " + txtInvoiceNo.Text +
                            "  AND sihp_branch_code='" + CommonData.BranchCode +
                            "' AND sihp_fin_year='" + CommonData.FinancialYear.ToString() +
                            "' AND sihp_company_code='" + CommonData.CompanyCode.ToString() + "'";


                }
                intRec = 0;
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
            string sqlDelete = "";
            int intRec = 0;
            try
            {
                sqlDelete = "DELETE from SALES_INV_DETL_PREV" +
                                " WHERE sidp_company_code='" + CommonData.CompanyCode +
                                    "' AND sidp_branch_code='" + CommonData.BranchCode +
                                    "' AND sidp_invoice_number=" + txtInvoiceNo.Text +
                                    "  AND sidp_fin_year='" + CommonData.FinancialYear + "'";

                intRec = objSQLDB.ExecuteSaveData(sqlDelete);

                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {
                    if (gvProductDetails.Rows[i].Cells["Rate"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["Qty"].Value.ToString() != "")
                    {
                        sqlText += "INSERT INTO SALES_INV_DETL_PREV(sidp_company_code, sidp_state_code, sidp_branch_code, sidp_invoice_number,sidp_fin_year, sidp_invoice_sl_no, sidp_state, sidp_product_id, sidp_price, sidp_qty, sidp_amount, SID_PRODUCT_POINTS)" +
                                    " VALUES ('" + CommonData.CompanyCode + "', '" + CommonData.StateCode + "', '" + CommonData.BranchCode + "', " + txtInvoiceNo.Text + ", '" + CommonData.FinancialYear + "'," + gvProductDetails.Rows[i].Cells["SLNO"].Value + ", '" + CommonData.StateName +
                                    "', '" + gvProductDetails.Rows[i].Cells["ProductID"].Value.ToString().Trim() + "', " + gvProductDetails.Rows[i].Cells["Rate"].Value + ", " + gvProductDetails.Rows[i].Cells["Qty"].Value +
                                    ", " + gvProductDetails.Rows[i].Cells["Amount"].Value + ", " + gvProductDetails.Rows[i].Cells["Points"].Value + "); ";

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

        private int SaveCustomerData()
        {
            objSQLDB = new SQLDB();
            objData = new InvoiceDB();
            object strDOB = null;
            object strMarDate = string.Empty;
            object strAge = "null";
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

            try
            {
                intRec = Convert.ToInt32(objSQLDB.ExecuteDataSet("SELECT * from Customer_Mas where cm_farmer_id ='" + strFormerid + "'").Tables[0].Rows.Count);

                if (intRec == 0 || strFormerid == "" || strFormerid == "99999")
                {
                    strFormerid = objData.GetCustomerFarmerId(txtState.Text.ToString().Trim(), txtDistrict.Text.ToString().Trim(), txtMandal.Text.ToString().Trim(), txtVillage.Text.ToString().Trim()).ToString();

                    sqlText = "INSERT INTO Customer_Mas(cm_village, cm_farmer_ID" +
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

                    sqlText = "UPDATE Customer_Mas SET  " +
                       " cm_village='" + txtVillage.Text + "".Trim() +
                       "', cm_farmer_name ='" + txtCustomerName.Text + "".Trim() +
                       "', cm_so_fo='" + cbRelation.Text +
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
                intRec = objSQLDB.ExecuteSaveData(sqlText);
            }
            catch (Exception ex)
            {
                intRec = 0;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objData = null;
            }
            return intRec;
        }

        private void gvProductDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                if (Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value) != "")
                {
                    if (Convert.ToInt32(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value) >= 0 && Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value) >= 0)
                    {
                        gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToInt32(gvProductDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) * (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value));
                        gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value).ToString("f");
                        gvProductDetails.Rows[e.RowIndex].Cells["Points"].Value = Convert.ToInt32(gvProductDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) * (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["DBPoints"].Value));
                        gvProductDetails.Rows[e.RowIndex].Cells["Points"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Points"].Value).ToString("f");
                    }
                }
                else
                    gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value = string.Empty;
            }
            if (e.ColumnIndex == 6)
            {
                if (Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value) != "")
                {
                    if (Convert.ToInt32(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value) >= 0 && Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value) >= 0)
                    {
                        gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) * (Convert.ToInt32(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value));
                        gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value).ToString("f");
                        gvProductDetails.Rows[e.RowIndex].Cells["Points"].Value = Convert.ToInt32(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value) * (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["DBPoints"].Value));
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
            txtInvAmt.Text = GetInvoiceAmt().ToString("f");
            txtReceivedAmt.Text = txtInvAmt.Text;
        }

        private void txtCustomerSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtCustomerSearch.Text.Length > 0)
                {
                    
                    if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                    {
                        blCustomerSearch = true;
                        if (FindInputCustomerSearch() == false)
                        {
                            FillCustomerFarmerData(txtCustomerSearch.Text, "");
                        }
                    }



                }


            }
            catch (Exception ex)
            {

            }
            if (e.KeyValue == 8)
            {
                if (this.txtCustomerSearch.TextLength >= 2)
                    FillCustomerFarmerData(Convert.ToString(txtCustomerSearch.Text.Trim()), "");
                this.txtCustomerName.SelectionStart = this.txtCustomerSearch.TextLength;
            }

        }

        private void txtInvoiceNo_Validated(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Convert.ToString(txtInvoiceNo.Text + "0")) > 0)
            {
                int intInvNo = Convert.ToInt32(Convert.ToString(txtInvoiceNo.Text));
                ClearForm(this);
                txtInvoiceNo.Text = intInvNo.ToString();
                if (FillInvOrBultInData(Convert.ToInt32(txtInvoiceNo.Text)) == false)
                {
                    MessageBox.Show("No data for " + txtInvoiceNo.Text + " Invoice number");
                    txtInvoiceNo.Focus();
                }
            }
        }

        private bool FillInvOrBultInData(int nInvNo)
        {
            bool isData = false;
            Hashtable ht;
            DataTable InvH;
            DataTable InvD;
            try
            {
                objData = new InvoiceDB();
                ht = objData.GetPrevInvoiceData(CommonData.StateCode, CommonData.BranchCode, CommonData.FinancialYear, nInvNo);
               
                InvH = (DataTable)ht["InvHead"];
                InvD = (DataTable)ht["InvDetail"];
                isData = FillInvoiceData(InvH, InvD);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objData = null;
            }
            return isData;
        }

        private bool  FillInvoiceData(DataTable dtInvH, DataTable dtInvD)
        {
            bool isData = false;
            try
            {
                if (dtInvH.Rows.Count > 0)
                {
                     IsModifyInvoice = true;
                     isData = true;
                    intInvoiceNo = Convert.ToInt32(dtInvH.Rows[0]["invoice_number"]);
                    txtInvoiceNo.Text = dtInvH.Rows[0]["invoice_number"] + "";
                    strFormerid = Convert.ToString(dtInvH.Rows[0]["farmer_ID"] + "");
                    meInvoiceDate.Text = dtInvH.Rows[0]["invoice_date"] + "";
                    strECode = dtInvH.Rows[0]["eora_code"] + "";
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

                    if (dtInvH.Rows[0]["INVOICE_AMOUNT"] + "" == "")
                        txtInvAmt.Text = "0.00";
                    else
                        txtInvAmt.Text = dtInvH.Rows[0]["INVOICE_AMOUNT"] + "";

                    if (dtInvH.Rows[0]["ADVANCE_AMOUNT"] + "" == "")
                        txtInvAmt.Text = "0.00";
                    else
                        txtAdvanceAmt.Text = dtInvH.Rows[0]["ADVANCE_AMOUNT"] + "";

                    if (dtInvH.Rows[0]["RECEIVED_AMOUNT"] + "" == "")
                        txtInvAmt.Text = "0.00";
                    else
                        txtReceivedAmt.Text = dtInvH.Rows[0]["RECEIVED_AMOUNT"] + "";

                    txtBalAmt.Text = Convert.ToDouble(Convert.ToDouble(txtInvAmt.Text) - (Convert.ToDouble(txtAdvanceAmt.Text) + Convert.ToDouble(txtReceivedAmt.Text))).ToString("f"); ;

                    if (dtInvH.Rows[0]["CREDIT_SALE"] + "" == "YES")
                        lblCreditSale.Visible = true;
                    else
                        lblCreditSale.Visible = false;

                    //meOrderDate.Text = dtInvH.Rows[0]["order_date"] + "";
                    txtCustomerid.Text = strFormerid;
                    if (dtInvH.Rows[0]["cm_marriage_date"].ToString() != "")
                    {
                        if (dtInvH.Rows[0]["cm_marriage_date"].ToString().Substring(0, 10) + "" == "01/01/1900" || dtInvH.Rows[0]["cm_marriage_date"].ToString().Substring(0, 10) + "" == "01-01-1900")
                            meMarriageDate.Text = "";
                        else
                            meMarriageDate.Text = dtInvH.Rows[0]["cm_marriage_date"] + "";
                    }

                    if (dtInvH.Rows[0]["cm_dob"].ToString() != "")
                    {
                        if (dtInvH.Rows[0]["cm_dob"].ToString().Substring(0, 10) + "" == "01/01/1900" || dtInvH.Rows[0]["cm_dob"].ToString().Substring(0, 10) + "" == "01-01-1900")
                            meDataofBirth.Text = "";
                        else
                            meDataofBirth.Text = dtInvH.Rows[0]["cm_dob"] + "";
                    }
                    txtAge.Text = dtInvH.Rows[0]["cm_age"] + "";
                    //cbBranch.Enabled = false;
                    SaveDeleteEnableDisable();
                    if (strFormerid == "99999")
                    {
                        DataTable dtXLS = objData.GetXLSCustomerDetls(Convert.ToInt32(txtInvoiceNo.Text)).Tables[0];
                        if (dtXLS.Rows.Count > 0)
                        {
                            txtMobileNo.Text = Convert.ToString(dtXLS.Rows[0]["cm_mobile_number"]);
                            txtVillage.Text = Convert.ToString(dtXLS.Rows[0]["cm_village"]);
                            txtMandal.Text = Convert.ToString(dtXLS.Rows[0]["cm_mandal"]);
                            txtDistrict.Text = Convert.ToString(dtXLS.Rows[0]["cm_district"]);
                            txtCustomerName.Text = Convert.ToString(dtXLS.Rows[0]["cm_farmer_name"]);
                        }
                    }
                    FillInvoiceDetail(dtInvD);
                }
                else
                {
                    btnSave.Enabled = true;
                    btnDelete.Enabled = true;
                    //cbBranch.Enabled = true;
                    IsModifyInvoice = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dtInvH = null;
            }
            return isData;
        }

        private void SaveDeleteEnableDisable()
        {
            try
            {
                if (Convert.ToDateTime(Convert.ToDateTime(meInvoiceDate.Text).ToString("dd/MM/yyyy")) < Convert.ToDateTime(CommonData.DocFDate) ||
                        Convert.ToDateTime(Convert.ToDateTime(meInvoiceDate.Text).ToString("dd/MM/yyyy")) > Convert.ToDateTime(CommonData.DocTDate))
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
                        cellSLNO.Value = dt.Rows[i]["invoice_sl_no"];
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
                        if (Convert.ToInt32(dt.Rows[i]["qty"]) > 0)
                            cellQTY.Value = Convert.ToInt32(dt.Rows[i]["qty"]);
                        else
                            cellQTY.Value = "";

                        tempRow.Cells.Add(cellQTY);

                        DataGridViewCell cellRate = new DataGridViewTextBoxCell();
                        if (Convert.ToInt32(dt.Rows[i]["qty"]) > 0)
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

                        DataGridViewCell cellDBPoints = new DataGridViewTextBoxCell();
                        cellDBPoints.Value = cellPoints.Value;
                        tempRow.Cells.Add(cellDBPoints);

                        if (dt.Rows[i]["ProductType"].ToString() == "Combi")
                            tempRow.Cells[2].Style.BackColor = Color.FromArgb(192, 192, 255);
                        else if (dt.Rows[i]["ProductType"].ToString() == "Free")   //Free
                            tempRow.Cells[2].Style.BackColor = Color.MediumTurquoise;

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
            catch (Exception ex)
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
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 5)
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    txtQty.MaxLength = 3;
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

            // checks to make sure only 1 decimal is allowed
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }


            //if (char.IsNumber(e.KeyChar) == false || blIsCellQty == false)
            //    e.Handled = true;
            //if (e.KeyChar == 8)
            //    e.Handled = false;
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
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtInvoiceNo.Text.Length > 0)
                {
                    DialogResult result = MessageBox.Show("Do you want to Delete " + txtInvoiceNo.Text + " invoice?",
                                           "CRM", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        objSQLDB = new SQLDB();
                        string strDelete = " DELETE from SALES_INV_DETL_PREV " +
                                            " WHERE SID_COMPANY_CODE='" + CommonData.CompanyCode +
                                            "' AND  sidp_branch_code='" + CommonData.BranchCode +
                                            "' AND sidp_invoice_number=" + txtInvoiceNo.Text +
                                            "  AND sidp_fin_year='" + CommonData.FinancialYear + "'";

                        strDelete += " DELETE from SALES_INV_HEAD_PREV " +
                                            " WHERE sihp_COMPANY_CODE='" + CommonData.CompanyCode +
                                            "' AND sihp_branch_code='" + CommonData.BranchCode +
                                            "' AND sihp_invoice_number=" + txtInvoiceNo.Text +
                                            "  AND sihp_fin_year='" + CommonData.FinancialYear + "'";

                        int intRec = objSQLDB.ExecuteSaveData(strDelete);
                        if (intRec > 0)
                        {
                            MessageBox.Show("Invoice " + intInvoiceNo + " Deleted ");
                            ClearForm(this);
                        }
                        IsModifyInvoice = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            this.Close();
            StockIndentFRM childForm = new StockIndentFRM();
            childForm.Show();
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
                    if (strItem.IndexOf(txtCustomerSearch.Text) > -1)
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
                            txtCustomerName.Text = dt.Rows[0]["cm_farmer_name"] + "";
                            txtRelationName.Text = dt.Rows[0]["cm_forg_name"] + "";
                            cbRelation.Text = dt.Rows[0]["cm_so_fo"] + "";
                            txtHouseNo.Text = Convert.ToString(dt.Rows[0]["cm_house_no"]);
                            txtLandMark.Text = Convert.ToString(dt.Rows[0]["cm_landmark"]);
                            //txtVillage.Text = dt.Rows[0]["cm_village"] + "";
                            //txtMandal.Text = dt.Rows[0]["cm_mandal"] + "";
                            //txtDistrict.Text = dt.Rows[0]["cm_district"] + "";
                            //txtState.Text = dt.Rows[0]["cm_state"] + "";
                            //strStateCode = dt.Rows[0]["sihp_state_code"] + "";
                            //txtPin.Text = dt.Rows[0]["cm_pin"] + "";
                            txtMobileNo.Text = dt.Rows[0]["cm_mobile_number"] + "";
                            txtLanLineNo.Text = dt.Rows[0]["cm_land_line_no"] + "";

                            if (dt.Rows[0]["cm_marriage_date"].ToString().Substring(0, 10) + "" == "01/01/1900" || dt.Rows[0]["cm_marriage_date"].ToString().Substring(0, 10) + "" == "01-01-1900")
                                meMarriageDate.Text = "";
                            else
                                meMarriageDate.Text = dt.Rows[0]["cm_marriage_date"] + "";

                            if (dt.Rows[0]["cm_dob"].ToString().Substring(0, 10) + "" == "01/01/1900" || dt.Rows[0]["cm_dob"].ToString().Substring(0, 10) + "" == "01-01-1900")
                                meDataofBirth.Text = "";
                            else
                                meDataofBirth.Text = dt.Rows[0]["cm_dob"] + "";

                            txtAge.Text = dt.Rows[0]["cm_age"] + "";
                            //if (btnSave.Enabled == true)
                            //    FillProductData();

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
                    //if (btnSave.Enabled == true)
                    //FillProductData();
                }
            }

        }

        private void txtVillage_Validated(object sender, EventArgs e)
        {
            //txtVillage.Text = strVillage;
        }

        private void meDataofBirth_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtAge.Text = "";
        }

        private void txtAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            meDataofBirth.Text = "";
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
                            
                            txtCustomerName.Text = dt.Rows[0]["cm_farmer_name"] + "";
                            txtRelationName.Text = dt.Rows[0]["cm_forg_name"] + "";
                            cbRelation.Text = dt.Rows[0]["cm_so_fo"] + "";
                            txtHouseNo.Text = Convert.ToString(dt.Rows[0]["cm_house_no"]);
                            txtLandMark.Text = Convert.ToString(dt.Rows[0]["cm_landmark"]);
                            txtMobileNo.Text = dt.Rows[0]["cm_mobile_number"] + "";
                            txtLanLineNo.Text = dt.Rows[0]["cm_land_line_no"] + "";

                            if (dt.Rows[0]["cm_marriage_date"].ToString().Substring(0, 10) + "" == "01/01/1900" || dt.Rows[0]["cm_marriage_date"].ToString().Substring(0, 10) + "" == "01-01-1900")
                                meMarriageDate.Text = "";
                            else
                                meMarriageDate.Text = dt.Rows[0]["cm_marriage_date"] + "";

                            if (dt.Rows[0]["cm_dob"].ToString().Substring(0, 10) + "" == "01/01/1900" || dt.Rows[0]["cm_dob"].ToString().Substring(0, 10) + "" == "01-01-1900")
                                meDataofBirth.Text = "";
                            else
                                meDataofBirth.Text = dt.Rows[0]["cm_dob"] + "";

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
            //strStateCode = "";
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
            }
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
                    if (((System.Windows.Forms.TextBox)ctrControl).Name != "txtOrderNo")
                        ((System.Windows.Forms.TextBox)ctrControl).Text = string.Empty;

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
                        gvProductDetails.Rows.Clear();
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
                    ((System.Windows.Forms.DateTimePicker)ctrControl).Text = DateTime.Now.Date.ToString("dd/MM/yy");

                }
                if (ctrControl.Controls.Count > 0)
                {
                    ClearForm(ctrControl);
                }
            }
            lblCreditSale.Visible = false;
            txtCustomerid.Text = "";
            cbEcode.SelectedIndex = -1;
        }

       private void txtOrderNo_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (char.IsNumber(e.KeyChar) == false)
                e.Handled = true;

            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void txtOrderNo_Validated(object sender, EventArgs e)
        {
            //if (Convert.ToInt32(Convert.ToString(txtOrderNo.Text + "0")) > 0)
            //{
            //    int intOrdNo = Convert.ToInt32(Convert.ToString(txtOrderNo.Text));
            //    ClearForm(this);
            //    txtOrderNo.Text = intOrdNo.ToString();
            //    FillInvoiceData(Convert.ToInt32(txtOrderNo.Text));

            //}
        }

        private void meOrderDate_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void btnVSearch_Click(object sender, EventArgs e)
        {
            VillageSearch VSearch = new VillageSearch("PreInvoice");
            VSearch.objFrmPrevInvoice = this;
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

        private void SearchEcode(ComboBox acComboBox, ref KeyEventArgs e)
        {
            //int selectionStart = acComboBox.SelectionStart;
            //int selectionLength = acComboBox.SelectionLength;
            //int selectionEnd = selectionStart + selectionLength;
            //int index;
            //StringBuilder sb = new StringBuilder();

            //sb.Append(acComboBox.Text.Substring(0, selectionStart));
            //   //.Append(e.KE.ToString())
            //   //.Append(acComboBox.Text.Substring(selectionEnd));

            //if (sb.ToString().Trim().Length > 0)
            //{
            //    for (int i = 0; i < acComboBox.Items.Count; i++)
            //    {
            //        if (acComboBox.Items[i].ToString() == "System.Data.DataRowView")  // for listbox search
            //        {
            //            if (((System.Data.DataRowView)(acComboBox.Items[i])).Row.ItemArray[1].ToString().IndexOf(sb.ToString(), StringComparison.OrdinalIgnoreCase) >= 0)
            //            {
            //                index = ((System.Data.DataRowView)(acComboBox.Items[i])).Row.ItemArray[1].ToString().IndexOf(sb.ToString(), StringComparison.OrdinalIgnoreCase);
            //                acComboBox.SelectedIndex = i;
            //                acComboBox.Select(0, 0);
            //                acComboBox.SelectionStart = index;
            //                acComboBox.Select(index + 1, acComboBox.Text.Length - (index + 1));
            //                e.Handled = true;
            //                break;
            //            }
            //            //else
            //            //    e.Handled = true; 

            //        }
            //        //else  // for checkbox list search
            //        //{
            //        //    if (cbEcode.Items[i].ToString().IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0)
            //        //    {
            //        //        cbEcode.SetSelected(i, true);
            //        //        break;
            //        //    }
            //        //    else
            //        //        cbEcode.SetSelected(i, false);

            //        //}

            //    }
            //}
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
                    if (Convert.ToInt32(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value) >= 0 && Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value) > 0)
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
            ProductSearchAll PSearch = new ProductSearchAll("PreInvoice");
            PSearch.objFrmPrevInvoice = this;
            PSearch.ShowDialog();
        }

        private void cbEcode_KeyUp(object sender, KeyEventArgs e)
        {
            //int selectionStart = cbEcode.SelectionStart;
            //int selectionLength = cbEcode.SelectionLength;
            //int selectionEnd = selectionStart + selectionLength;
            //int index;
            //StringBuilder sb = new StringBuilder();

            //sb.Append(cbEcode.Text.Substring(0, selectionStart))
            //.Append(Convert.ToChar(e.KeyData))
            //.Append(cbEcode.Text.Substring(selectionEnd));

            //if (sb.ToString().Trim().Length > 0)
            //{
            //    for (int i = 0; i < cbEcode.Items.Count; i++)
            //    {
            //        if (cbEcode.Items[i].ToString() == "System.Data.DataRowView")  // for listbox search
            //        {
            //            if (((System.Data.DataRowView)(cbEcode.Items[i])).Row.ItemArray[1].ToString().IndexOf(sb.ToString(), StringComparison.OrdinalIgnoreCase) >= 0)
            //            {
            //                index = ((System.Data.DataRowView)(cbEcode.Items[i])).Row.ItemArray[1].ToString().IndexOf(sb.ToString(), StringComparison.OrdinalIgnoreCase);
            //                cbEcode.SelectedIndex = i;
            //                cbEcode.Select(0, 0);
            //                cbEcode.SelectionStart = index;
            //                cbEcode.Select(index + 1, cbEcode.Text.Length - (index + 1));
            //                break;
            //            }
            //            //else
            //            //    e.Handled = true; 

            //        }
            //        //else  // for checkbox list search
            //        //{
            //        //    if (cbEcode.Items[i].ToString().IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0)
            //        //    {
            //        //        cbEcode.SetSelected(i, true);
            //        //        break;
            //        //    }
            //        //    else
            //        //        cbEcode.SetSelected(i, false);

            //        //}

            //    }
            //}


            ////if (!e.KeyCode.Equals((char)8))
            ////{
            ////    SearchEcode(cbEcode, ref e);
            ////}
            ////else
            ////    e.Handled = false;
        }

        private double GetInvoiceAmt()
        {
            double dbInvAmt = 0;
            for (int i = 0; i < gvProductDetails.Rows.Count; i++)
            {
                if (gvProductDetails.Rows[i].Cells["Amount"].Value.ToString() != "")
                {  
                    if(Convert.ToDouble(gvProductDetails.Rows[i].Cells["Amount"].Value.ToString())>=1)
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
            if (txtEcodeSearch.Text.ToString().Trim().Length > 0)
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
                dsEmp = objData.InvLevelEcodeSearch_Get(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth, txtEcodeSearch.Text.ToString());
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

        private void lblBranch_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void meInvoiceDate_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
        
    }
}
