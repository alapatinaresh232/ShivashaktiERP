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
    public partial class InvoiceBultinCustomer : Form
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
        private bool blCustomerSearch = false;
        private bool blIsCellQty = true;
        private string strCreditSale = "NO";

        public InvoiceBultinCustomer()
        {
            InitializeComponent();
        }

        private void InvoiceBultin_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.X + 30, Screen.PrimaryScreen.WorkingArea.Y + 30);
            this.StartPosition = FormStartPosition.CenterScreen;
            ClearForm(this);
            FillBranchData();
            FillEmployeeData();
            cbEcode.SelectedIndex = -1;
            cbRelation.SelectedIndex = 0;
            gvProductDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);
            txtCustomerSearch.Text = "NOVILLAGE";
            strFormerid = "99999";
            txtCustomerid.Text = "99999";
            txtCustomerName.Text = "NOVILLAGE";

            objData = new InvoiceDB();
            txtInvoiceNo.Text = objData.GenerateInvoiceNo(CommonData.CompanyCode, CommonData.BranchCode).ToString();
            objData = null;
            lblBranch.Text = CommonData.BranchName;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm(this);
            //cbBranch.Enabled = true;
            //cbBranch.Focus();
            txtOrderNo.Text = string.Empty;
            objData = new InvoiceDB();
            txtInvoiceNo.Text = objData.GenerateInvoiceNo(CommonData.CompanyCode, CommonData.BranchCode).ToString();
            objData = null;
            strFormerid = "99999";
            txtCustomerid.Text = "99999";
            txtCustomerName.Text = "NOVILLAGE";


        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
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
            Master objMData = new Master();
            try
            {
                if (sSearch.Trim() != "" || sCustId.Length > 0)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    ds = new DataSet();
                    ds = objMData.CustomerData_Get("", sSearch);
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
                        txtMobileNo.Text = dt.Rows[0]["cm_mobile_number"] + "";
                        txtLanLineNo.Text = dt.Rows[0]["cm_land_line_no"] + "";

                        if (dt.Rows[0]["cm_marriage_date"].ToString().Substring(0, 10) + "" == "01/01/1900")
                            meMarriageDate.Text = "";
                        else
                            meMarriageDate.Text = dt.Rows[0]["cm_marriage_date"] + "";

                        if (dt.Rows[0]["cm_dob"].ToString().Substring(0, 10) + "" == "01/01/1900")
                            meDataofBirth.Text = "";
                        else
                            meDataofBirth.Text = dt.Rows[0]["cm_dob"] + "";

                        txtAge.Text = dt.Rows[0]["cm_age"] + "";
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
                        txtRelationName.Text = "";
                        txtMobileNo.Text = "";
                        txtHouseNo.Text = "";
                        txtLandMark.Text = "";
                        txtLanLineNo.Text = "";
                        txtAge.Text = "";
                        strFormerid = "99999";
                        txtCustomerid.Text = "99999";
                        txtCustomerName.Text = "NOVILLAGE";

                    }
                }

            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objMData = null;
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
                        objData = new InvoiceDB();
                        txtInvoiceNo.Text = objData.GenerateInvoiceNo(CommonData.CompanyCode, CommonData.BranchCode).ToString();
                        objData = null;
                        strFormerid = "99999";
                        txtCustomerid.Text = "99999";
                        txtCustomerName.Text = "NOVILLAGE";

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

            //if ((meOrderDate.Text.IndexOf(" ") >= 0) || (meOrderDate.Text.Length < 10))
            //{
            //    MessageBox.Show("Enter Order date!");
            //    blValue = false;
            //    meOrderDate.Focus();
            //    return blValue;
            //}
            //else
            //{
            //    if (Convert.ToInt32(Convert.ToDateTime(meOrderDate.Text).ToString("yyyy")) < 1950)
            //    {
            //        MessageBox.Show("Enter valid  Date !");
            //        blValue = false;
            //        meOrderDate.CausesValidation = true;
            //        meOrderDate.Focus();
            //    }
            //    if (Convert.ToDateTime(Convert.ToDateTime(meOrderDate.Text).ToString("dd/MM/yyyy")) > Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy")))
            //    {
            //        MessageBox.Show("Date should be less than to day");
            //        meOrderDate.CausesValidation = true;
            //        blValue = false;
            //        meOrderDate.Focus();
            //    }
            //}

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
                if (General.IsDateTime(meInvoiceDate.Text.ToString()))
                {
                    if (Convert.ToInt32(Convert.ToDateTime(meInvoiceDate.Text).ToString("yyyy")) < 1950)
                    {
                        MessageBox.Show("Enter valid  Date !");
                        blValue = false;
                        meInvoiceDate.CausesValidation = true;
                        meInvoiceDate.Focus();
                        return blValue;
                    }
                    if (Convert.ToDateTime(Convert.ToDateTime(meInvoiceDate.Text).ToString("dd/MM/yyyy")) > Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyyy")))
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
                else
                {
                    MessageBox.Show("Enter valid Invoice Date!");
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
            if (Convert.ToString(txtCustomerName.Text).Length == 0)
            {
                MessageBox.Show("Enter Customer name!");
                blValue = false;
                txtCustomerName.Focus();
                return blValue;
            }
            if (General.IsDateTime(meDataofBirth.Text.ToString()))
            {
                //if ((meDataofBirth.Text.IndexOf(" ") > -1) && (meDataofBirth.Text.Length < 10))
                //{
                //    MessageBox.Show("Enter valid  Date !");
                //    meDataofBirth.Focus();
                //    meDataofBirth.CausesValidation = false;
                //}
                //else
                //{
                //    if (Convert.ToInt32(Convert.ToDateTime(meDataofBirth.Text).ToString("yyyy")) < 1950)
                //    {
                //        MessageBox.Show("Enter valid  Date !");
                //        meDataofBirth.CausesValidation = true;
                //        meDataofBirth.Focus();
                //    }
                //    if (Convert.ToDateTime(Convert.ToDateTime(meDataofBirth.Text).ToString("dd/MM/yyyy")) > Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy")))
                //    {
                //        MessageBox.Show("Date should be less than to day");
                //        meDataofBirth.CausesValidation = true;
                //        meDataofBirth.Focus();
                //    }
                //}
                //if ((meMarriageDate.Text.IndexOf(" ") > -1) && (meMarriageDate.Text.Length < 10))
                //{
                //    MessageBox.Show("Enter valid  Date !");
                //    meMarriageDate.Focus();
                //    meMarriageDate.CausesValidation = false;
                //}
                //else
                //{
                //    if (Convert.ToInt32(Convert.ToDateTime(meMarriageDate.Text).ToString("yyyy")) < 1950)
                //    {
                //        MessageBox.Show("Enter valid  Date !");
                //        meMarriageDate.CausesValidation = true;
                //        meMarriageDate.Focus();
                //    }

                //    if (Convert.ToDateTime(Convert.ToDateTime(meMarriageDate.Text).ToString("dd/MM/yyyy")) > Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy")))
                //    {
                //        MessageBox.Show("Date should be less than to day");
                //        meMarriageDate.CausesValidation = true;
                //        meMarriageDate.Focus();
                //    }
                //}
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
                MessageBox.Show("Enter product details");
                return false;
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
                intRec = objSQLDB.ExecuteSaveData("UPDATE SALES_INV_BULTIN_HEAD set SIBH_CREDIT_SALE='" + strCreditSale + "' WHERE sibh_invoice_number = " + txtInvoiceNo.Text + " AND sibh_branch_code='" + CommonData.BranchCode + "' AND sibh_fin_year='" + CommonData.FinancialYear.ToString() + "'");

                if (intRec == 0)
                    sqlText = "INSERT INTO SALES_INV_BULTIN_HEAD(sibh_company_code, sibh_state_code, sibh_branch_code, sibh_fin_year, sibh_invoice_number, sibh_invoice_date, sibh_farmer_ID, sibh_order_number, sibh_eora_code, sibh_prod_pattern, sibh_Document_month, sibh_created_Date, SIBH_CREATED_BY, SIBH_INVOICE_AMOUNT, SIBH_ADVANCE_AMOUNT, SIBH_RECEIVED_AMOUNT, SIBH_CREDIT_SALE)" +
                     " VALUES ('" + CommonData.CompanyCode + "', '" + CommonData.StateCode + "', '" + CommonData.BranchCode + "' , '" + CommonData.FinancialYear + "'," + txtInvoiceNo.Text + ", '" + Convert.ToDateTime(meInvoiceDate.Text).ToString("dd/MMM/yyy") + "' " +
                     ", '" + strFormerid + "', '" + txtOrderNo.Text + "', '" + strECode + "', 'Ppattern', '" + CommonData.DocMonth +
                     "', '" + CommonData.CurrentDate + "','" + CommonData.LogUserId +
                     "', " + txtInvAmt.Text + "," + txtAdvanceAmt.Text +
                     ", " + txtReceivedAmt.Text + ", '" + strCreditSale + "' )";
                else
                {
                    string sqlDelete = "DELETE from SALES_INV_BULTIN_DETL" +
                                 " WHERE sibd_company_code='" + CommonData.CompanyCode +
                                     "' AND sibd_branch_code='" + CommonData.BranchCode +
                                     "' AND sibd_invoice_number=" + txtInvoiceNo.Text +
                                     "  AND sibd_fin_year='" + CommonData.FinancialYear + "'";

                    intRec = objSQLDB.ExecuteSaveData(sqlDelete);

                    sqlText = "UPDATE SALES_INV_BULTIN_HEAD set sibh_invoice_date='" + Convert.ToDateTime(meInvoiceDate.Text).ToString("dd/MMM/yyy") +
                            "', sibh_farmer_ID='" + strFormerid +
                            "', sibh_eora_code='" + strECode +
                            "', sibh_prod_pattern='Ppattern'" +
                            ", sibh_Document_month='" + CommonData.DocMonth +
                            "', SIBH_LAST_MODIFIED_BY='" + CommonData.LogUserId + 
                            "', SIBH_LAST_MODIFIED_DATE='" + CommonData.CurrentDate +
                            "', sibh_order_number='" + txtOrderNo.Text +
                            "', SIBH_INVOICE_AMOUNT =" + txtInvAmt.Text +
                            ", SIBH_ADVANCE_AMOUNT = " + txtAdvanceAmt.Text +
                            ", SIBH_RECEIVED_AMOUNT = " + txtReceivedAmt.Text +
                            ", SIBH_CREDIT_SALE = '" + strCreditSale +
                        //"', sibh_order_date='" + Convert.ToDateTime(meOrderDate.Text).ToString("dd/MMM/yyyy") +
                            "' WHERE sibh_invoice_number = " + txtInvoiceNo.Text +
                            "  AND sibh_branch_code='" + CommonData.BranchCode +
                            "' AND sibh_fin_year='" + CommonData.FinancialYear.ToString() +
                            "' AND sibh_company_code='" + CommonData.CompanyCode.ToString() + "'";


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
                sqlDelete = "DELETE from SALES_INV_BULTIN_DETL" +
                                " WHERE sibd_company_code='" + CommonData.CompanyCode +
                                    "' AND sibd_branch_code='" + CommonData.BranchCode +
                                    "' AND sibd_invoice_number=" + txtInvoiceNo.Text +
                                    "  AND sibd_fin_year='" + CommonData.FinancialYear + "'";

                intRec = objSQLDB.ExecuteSaveData(sqlDelete);

                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {
                    if (gvProductDetails.Rows[i].Cells["Rate"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["Qty"].Value.ToString() != "")
                    {
                        sqlText += "INSERT INTO SALES_INV_BULTIN_DETL(sibd_company_code, sibd_state_code, sibd_branch_code, sibd_invoice_number,sibd_fin_year, sibd_invoice_sl_no, sibd_state, sibd_product_id, sibd_price, sibd_qty, sibd_amount, SIBD_PRODUCT_POINTS)" +
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
            //objSQLDB = new SQLDB();
            //objData = new InvoiceDB();
            //object strDOB = null;
            //object strMarDate = string.Empty;
            //object strAge = "null";
            //string sqlText = string.Empty;
            int intRec = 1;
            //if (strDateOfBirth.Length > 5)
            //    strDOB = Convert.ToDateTime(strDateOfBirth).ToString("dd/MMM/yyyy");
            //else
            //    strDOB = "";

            //if (strMarriageDate.Length > 5)
            //    strMarDate = Convert.ToDateTime(strMarriageDate).ToString("dd/MMM/yyyy");
            //else
            //    strMarDate = "";
            //if (txtAge.Text.Length > 0)
            //    strAge = Convert.ToInt16(txtAge.Text);

            //try
            //{
            //    intRec = Convert.ToInt32(objSQLDB.ExecuteDataSet("SELECT * from Customer_Mas where cm_farmer_id ='" + strFormerid + "'").Tables[0].Rows.Count);

            //    if (intRec == 0 || strFormerid == "" || strFormerid == "99999")
            //    {
            //        strFormerid = objData.GetCustomerFarmerId(txtState.Text.ToString().Trim(), txtDistrict.Text.ToString().Trim(), txtMandal.Text.ToString().Trim(), txtVillage.Text.ToString().Trim()).ToString();

            //        sqlText = "INSERT INTO Customer_Mas(cm_village, cm_farmer_ID" +
            //           ", cm_farmer_name, cm_so_fo, cm_forg_name, cm_mobile_number" +
            //           ", cm_land_line_no, cm_mandal, cm_district, cm_state, cm_pin, cm_dob, cm_marriage_date, cm_age" +
            //           ", cm_house_no, cm_landmark, CM_CREATED_BY, CM_CREATED_DATE) VALUES ( '" + txtVillage.Text + "".Trim() + "', '" + strFormerid +
            //           "', '" + txtCustomerName.Text + "".Trim() + "','" + cbRelation.Text + "','" + txtRelationName.Text + "'" +
            //           ", '" + txtMobileNo.Text + "" + "','" + txtLanLineNo.Text + "'".Trim() +
            //           ", '" + txtMandal.Text + "" + "', '" + txtDistrict.Text + "', '" + txtState.Text +
            //           "', '" + txtPin.Text + "', '" + strDOB + "','" + strMarDate + "', " + strAge +
            //           ", '" + txtHouseNo.Text + "".Trim() +
            //           "', '" + txtLandMark.Text + "".Trim() + "', '" + CommonData.LogUserId + "', '" + CommonData.CurrentDate + "')";
            //    }
            //    else
            //    {

            //        sqlText = "UPDATE Customer_Mas SET  " +
            //           " cm_village='" + txtVillage.Text + "".Trim() +
            //           "', cm_farmer_name ='" + txtCustomerName.Text + "".Trim() +
            //           "', cm_so_fo='" + cbRelation.Text +
            //           "', cm_forg_name='" + txtRelationName.Text + "'" +
            //           ", cm_mobile_number='" + txtMobileNo.Text + "" +
            //           "', cm_land_line_no='" + txtLanLineNo.Text + "".Trim() +
            //           "', cm_mandal='" + txtMandal.Text + "" +
            //           "', cm_district='" + txtDistrict.Text +
            //           "', cm_state='" + txtState.Text +
            //           "', cm_pin='" + txtPin.Text +
            //           "', cm_dob='" + strDOB +
            //           "', cm_marriage_date='" + strMarDate +
            //           "', cm_age=" + strAge +
            //           ", cm_house_no='" + txtHouseNo.Text +
            //           "', cm_landmark='" + txtLandMark.Text +
            //           "', CM_LAST_MODIFIED_BY='" + CommonData.LogUserId +
            //           "', CM_LAST_MODIFIED_DATE='" + CommonData.CurrentDate +
            //           "' WHERE cm_farmer_id='" + strFormerid + "'";
            //    }
            //    intRec = 0;
            //    intRec = objSQLDB.ExecuteSaveData(sqlText);
            //}
            //catch (Exception ex)
            //{
            //    intRec = 0;
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    objData = null;
            //}
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
                FillInvoiceData(Convert.ToInt32(txtInvoiceNo.Text));

            }
        }

        private void FillInvoiceData(int nInvNo)
        {
            Hashtable ht = new Hashtable();
            try
            {
                objData = new InvoiceDB();
                ht = objData.InvoiceBulitin_Get(CommonData.StateCode, CommonData.BranchCode, nInvNo,"");
                DataTable dtInvH = (DataTable)ht["InvHead"];
                DataTable dtInvD = (DataTable)ht["InvDetail"];
                if (dtInvH.Rows.Count > 0)
                {
                    intInvoiceNo = Convert.ToInt32(dtInvH.Rows[0]["sibh_invoice_number"]);
                    txtInvoiceNo.Text = dtInvH.Rows[0]["sibh_invoice_number"] + "";
                    strFormerid = Convert.ToString(dtInvH.Rows[0]["sibh_farmer_ID"] + "");
                    meInvoiceDate.Text = dtInvH.Rows[0]["sibh_invoice_date"] + "";
                    strECode = dtInvH.Rows[0]["sibh_eora_code"] + "";
                    cbEcode.SelectedValue = strECode;
                    txtHouseNo.Text = Convert.ToString(dtInvH.Rows[0]["cm_house_no"]);
                    txtLandMark.Text = Convert.ToString(dtInvH.Rows[0]["cm_landmark"]);
                    txtCustomerName.Text = dtInvH.Rows[0]["cm_farmer_name"] + "";
                    cbRelation.Text = dtInvH.Rows[0]["cm_so_fo"] + "";
                    txtRelationName.Text = dtInvH.Rows[0]["cm_forg_name"] + "";
                    txtMobileNo.Text = dtInvH.Rows[0]["cm_mobile_number"] + "";
                    txtLanLineNo.Text = dtInvH.Rows[0]["cm_land_line_no"] + "";
                    txtOrderNo.Text = dtInvH.Rows[0]["sibh_order_number"] + "";

                    if (dtInvH.Rows[0]["SIBH_INVOICE_AMOUNT"] + "" == "")
                        txtInvAmt.Text = "0.00";
                    else
                        txtInvAmt.Text = dtInvH.Rows[0]["SIBH_INVOICE_AMOUNT"] + "";

                    if (dtInvH.Rows[0]["SIBH_ADVANCE_AMOUNT"] + "" == "")
                        txtInvAmt.Text = "0.00";
                    else
                        txtAdvanceAmt.Text = dtInvH.Rows[0]["SIBH_ADVANCE_AMOUNT"] + "";

                    if (dtInvH.Rows[0]["SIBH_RECEIVED_AMOUNT"] + "" == "")
                        txtInvAmt.Text = "0.00";
                    else
                        txtReceivedAmt.Text = dtInvH.Rows[0]["SIBH_RECEIVED_AMOUNT"] + "";

                    txtBalAmt.Text = Convert.ToDouble(Convert.ToDouble(txtInvAmt.Text) - (Convert.ToDouble(txtAdvanceAmt.Text) + Convert.ToDouble(txtReceivedAmt.Text))).ToString("f"); ;

                    if (dtInvH.Rows[0]["SIBH_CREDIT_SALE"] + "" == "YES")
                        lblCreditSale.Visible = true;
                    else
                        lblCreditSale.Visible = false;

                    //meOrderDate.Text = dtInvH.Rows[0]["sibh_order_date"] + "";
                    txtCustomerid.Text = strFormerid;
                    if (dtInvH.Rows[0]["cm_marriage_date"].ToString() != "")
                    {
                        if (dtInvH.Rows[0]["cm_marriage_date"].ToString().Substring(0, 10) + "" == "01/01/1900")
                            meMarriageDate.Text = "";
                        else
                            meMarriageDate.Text = dtInvH.Rows[0]["cm_marriage_date"] + "";
                    }

                    if (dtInvH.Rows[0]["cm_dob"].ToString() != "")
                    {
                        if (dtInvH.Rows[0]["cm_dob"].ToString().Substring(0, 10) + "" == "01/01/1900")
                            meDataofBirth.Text = "";
                        else
                            meDataofBirth.Text = dtInvH.Rows[0]["cm_dob"] + "";
                    }
                    txtAge.Text = dtInvH.Rows[0]["cm_age"] + "";
                    //cbBranch.Enabled = false;
                    
                    SaveDeleteEnableDisable();
                    if (strFormerid == "99999")
                    {
                        //DataTable dtXLS = objData.GetXLSCustomerDetls(Convert.ToInt32(txtInvoiceNo.Text)).Tables[0];
                        //if (dtXLS.Rows.Count > 0)
                        //{
                        //    gbFarmerDetls.Visible = true;
                        //    txtMobileNo.Text = Convert.ToString(dtXLS.Rows[0]["cm_mobile_number"]);
                        //    txtVillageXLS.Text = Convert.ToString(dtXLS.Rows[0]["cm_village"]);
                        //    txtMandalXls.Text = Convert.ToString(dtXLS.Rows[0]["cm_mandal"]);
                        //    txtDistrictXls.Text = Convert.ToString(dtXLS.Rows[0]["cm_district"]);
                        //    txtFarmerDetls.Text = Convert.ToString(dtXLS.Rows[0]["cm_farmer_name"]);
                        //    //FillAddressData(txtVillageXLS.Text.Trim());
                        //}
                        //else
                        //    gbFarmerDetls.Visible = false;
                    }
                    FillInvoiceDetail(dtInvD);
                }
                else
                {
                    btnSave.Enabled = true;
                    btnDelete.Enabled = true;
                    //cbBranch.Enabled = true;
                    //FillProductData();
                }

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
                    if (Convert.ToDouble(dt.Rows[i]["sibd_amount"]) > 0)
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = dt.Rows[i]["sibd_invoice_sl_no"];
                        tempRow.Cells.Add(cellSLNO);

                        DataGridViewCell cellMainProductID = new DataGridViewTextBoxCell();
                        cellMainProductID.Value = dt.Rows[i]["sibd_product_id"];
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
                        if (Convert.ToInt32(dt.Rows[i]["sibd_qty"]) > 0)
                            cellQTY.Value = Convert.ToInt32(dt.Rows[i]["sibd_qty"]);
                        else
                            cellQTY.Value = "";

                        tempRow.Cells.Add(cellQTY);

                        DataGridViewCell cellRate = new DataGridViewTextBoxCell();
                        if (Convert.ToInt32(dt.Rows[i]["sibd_qty"]) > 0)
                            cellRate.Value = dt.Rows[i]["sibd_price"];
                        else
                            cellRate.Value = dt.Rows[i]["TIP_Rate"];

                        if (Convert.ToInt32(cellRate.Value) == 0)
                            cellRate.Value = dt.Rows[i]["TIP_Rate"];

                        tempRow.Cells.Add(cellRate);

                        DataGridViewCell cellAmt = new DataGridViewTextBoxCell();
                        if (Convert.ToDouble(dt.Rows[i]["sibd_amount"]) > 0)
                            cellAmt.Value = dt.Rows[i]["sibd_amount"];
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
            if (txtInvoiceNo.Text.Length > 0)
            {
                DialogResult result = MessageBox.Show("Do you want to Delete " + txtInvoiceNo.Text + " invoice?",
                                       "CRM", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    objSQLDB = new SQLDB();
                    string strDelete = " DELETE from SALES_INV_BULTIN_DETL " +
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
                        MessageBox.Show("Invoice " + intInvoiceNo + " Deleted ");
                        ClearForm(this);
                    }
                    objData = new InvoiceDB();
                    txtInvoiceNo.Text = objData.GenerateInvoiceNo(CommonData.CompanyCode, CommonData.BranchCode).ToString();
                    objData = null;
                    strFormerid = "99999";
                    txtCustomerid.Text = "99999";
                    txtCustomerName.Text = "NOVILLAGE";

                }
            }
        }
        
        private void btnView_Click(object sender, EventArgs e)
        {
            this.Close();
            StockIndentFRM childForm = new StockIndentFRM();
            childForm.Show();
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

        private bool FindInputCustomerSearch()
        {
            bool blFind = false;
            Master objMData = new Master();
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
                        ds = objMData.CustomerData_Get(strItem);
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
                            //strStateCode = dt.Rows[0]["sibh_state_code"] + "";
                            //txtPin.Text = dt.Rows[0]["cm_pin"] + "";
                            txtMobileNo.Text = dt.Rows[0]["cm_mobile_number"] + "";
                            txtLanLineNo.Text = dt.Rows[0]["cm_land_line_no"] + "";

                            if (dt.Rows[0]["cm_marriage_date"].ToString().Substring(0, 10) + "" == "01/01/1900")
                                meMarriageDate.Text = "";
                            else
                                meMarriageDate.Text = dt.Rows[0]["cm_marriage_date"] + "";

                            if (dt.Rows[0]["cm_dob"].ToString().Substring(0, 10) + "" == "01/01/1900")
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
                objMData = null;
            }
            return blFind;
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

                            if (dt.Rows[0]["cm_marriage_date"].ToString().Substring(0, 10) + "" == "01/01/1900")
                                meMarriageDate.Text = "";
                            else
                                meMarriageDate.Text = dt.Rows[0]["cm_marriage_date"] + "";

                            if (dt.Rows[0]["cm_dob"].ToString().Substring(0, 10) + "" == "01/01/1900")
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
            //ProductSearchAll PSearch = new ProductSearchAll("InvoiceBultinCustomer");
            //PSearch.objFrmInvoiceBultin = this;
            //PSearch.ShowDialog();
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

            return dbInvAmt;
        }

        private void txtAdvanceAmt_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtAdvanceAmt.Text == "")
                txtAdvanceAmt.Text = "0.00";
            if (txtReceivedAmt.Text == "")
                txtReceivedAmt.Text = "0.00";
            txtReceivedAmt.Text = Convert.ToDouble(Convert.ToDouble(txtInvAmt.Text) - Convert.ToDouble(txtAdvanceAmt.Text)).ToString("f");
            txtBalAmt.Text = Convert.ToDouble(Convert.ToDouble(txtInvAmt.Text) - (Convert.ToDouble(txtAdvanceAmt.Text) + Convert.ToDouble(txtReceivedAmt.Text))).ToString("f");

        }

        private void txtReceivedAmt_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtAdvanceAmt.Text == "")
                txtAdvanceAmt.Text = "0.00";
            if (txtReceivedAmt.Text == "")
                txtReceivedAmt.Text = "0.00";
            txtBalAmt.Text = Convert.ToDouble(Convert.ToDouble(txtInvAmt.Text) - (Convert.ToDouble(txtAdvanceAmt.Text) + Convert.ToDouble(txtReceivedAmt.Text))).ToString("f");

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

        private void gbFarmerDetls_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

       
    }
}
