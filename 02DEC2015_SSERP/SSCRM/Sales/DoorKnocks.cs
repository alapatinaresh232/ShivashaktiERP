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
using System.Collections;
namespace SSCRM
{
    public partial class DoorKnocks : Form
    {
        private SQLDB objSQLDB = null;
        private InvoiceDB objData = null;
        private DnkDetailsDB objDnkDetails = null;
        private string strFormerid = string.Empty;
        public string strStateCode = string.Empty;
        private string strVillage = string.Empty;
        private string strECode = string.Empty;
        private bool blCustomerSearch = false;
        public int iUpdateVal = 0;
        IFormatProvider ifrmt = new System.Globalization.CultureInfo("en-GB", true);
        public DoorKnocks()
        {
            InitializeComponent();
        }

        private void DoorKnocks_Load(object sender, EventArgs e)
        {
            ClearForm(this);
            FillEmployeeData();
            cbEcode.SelectedIndex = -1;
            cmbDemoDtls.SelectedIndex = 0;
            cmleadType.SelectedIndex = 0;
            cbRelation.SelectedIndex = 0;
            gvProductDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);

            objSQLDB = new SQLDB();
            string strSql = "SELECT ISNULL(MAX(SDH_TRN_NUMBER),0)+1 FROM SALES_DNK_HEAD WHERE SDH_BRANCH_CODE='" + CommonData.BranchCode + "' AND SDH_DOCUMENT_MONTH='" + CommonData.DocMonth + "'";
            txtTNRNo.Text = objSQLDB.ExecuteDataSet(strSql).Tables[0].Rows[0][0].ToString();
            objSQLDB = null;
            meTNRDate.Value = System.DateTime.Now;
        }

        private void ClearForm(System.Windows.Forms.Control parent)
        {
            strFormerid = "";
            strVillage = string.Empty;
            foreach (System.Windows.Forms.Control ctrControl in parent.Controls)
            {
                //Loop through all controls 
                if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.TextBox)))
                {
                    //Check to see if it's a textbox 
                    if (((System.Windows.Forms.TextBox)ctrControl).Name != "txtTNRNo")
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
                    ((System.Windows.Forms.DataGridView)ctrControl).Rows.Clear();
                    //FillProductData();

                }
                //else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.DateTimePicker)))
                //{
                //    ((System.Windows.Forms.DateTimePicker)ctrControl).Text = DateTime.Now.Date.ToString("dd/MM/yy");
                //}
                if (ctrControl.Controls.Count > 0)
                {
                    ClearForm(ctrControl);
                }
            }
            //lblCreditSale.Visible = false;
            lblCustomerId.Text = "";
            cbEcode.SelectedIndex = -1;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            int intSaved = 0;
            try
            {
                if (CheckData() == true)
                {
                    if (iUpdateVal == 0)
                    {
                        objSQLDB = new SQLDB();
                        string strSql = "SELECT ISNULL(MAX(SDH_TRN_NUMBER),0)+1 FROM SALES_DNK_HEAD WHERE SDH_BRANCH_CODE='" + CommonData.BranchCode + "' AND SDH_DOCUMENT_MONTH='" + CommonData.DocMonth + "'";
                        txtTNRNo.Text = objSQLDB.ExecuteDataSet(strSql).Tables[0].Rows[0][0].ToString();
                        objSQLDB = null;
                    }
                    if (SaveDoorknocksHead() >= 1)
                        intSaved = SaveDoorKnockDeatailData();
                    if (intSaved > 0)
                    {
                        strFormerid = "";
                        ClearForm(this);
                        txtTNRNo.Text = string.Empty;
                        cbVillage.DataBindings.Clear();
                        btnCancel_Click(null, null);
                        MessageBox.Show("Data saved successfully", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Data not saved", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool CheckData()
        {
            bool blValue = true;
            if (Convert.ToString(txtTNRNo.Text).Length == 0)
            {
                MessageBox.Show("Enter TNR number!", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                blValue = false;
                txtTNRNo.Focus();
                return blValue;
            }

            if ((meTNRDate.Text.IndexOf(" ") >= 0) || (meTNRDate.Text.Length < 10))
            {
                MessageBox.Show("Enter TNR date!");
                blValue = false;
                meTNRDate.Focus();
                return blValue;
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
            //if (strStateCode.Length == 0)
            //{
            //    MessageBox.Show("No State for " + txtVillage.Text + " Village!");
            //    blValue = false;
            //    txtVillageSearch.Focus();
            //    return blValue;
            //}
            if (cmleadType.SelectedIndex == 0)
            {
                MessageBox.Show("Please Select Lead Type");
                blValue = false;
                cmleadType.Focus();
                return blValue;
            }
            if (cmbDemoDtls.SelectedIndex == 0)
            {
                MessageBox.Show("Please Select Demo Details");
                blValue = false;
                cmbDemoDtls.Focus();
                return blValue;
            }
            if (txtRemarks.Text.Length == 0)
            {
                MessageBox.Show("Please Enter Remarks");
                blValue = false;
                txtRemarks.Focus();
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
                if (Convert.ToString(gvProductDetails.Rows[i].Cells["DBPoints"].Value) != "")
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
            return blValue;
        }

        private int SaveDoorknocksHead()
        {
            objSQLDB = new SQLDB();
            objDnkDetails = new DnkDetailsDB();
            string sqlText = "";
            string sqlTextCust = "";
            int intRec = 0;
            try
            {
                DataSet dsCnt = objDnkDetails.DNKSearch_Get(txtCustomerName.Text, txtVillage.Text, txtMandal.Text, txtState.Text);
                if (dsCnt.Tables[0].Rows.Count == 0)
                {
                    sqlText = "INSERT INTO SALES_DNK_HEAD(SDH_COMPANY_CODE,SDH_STATE_CODE,SDH_BRANCH_CODE,SDH_FIN_YEAR,SDH_DOCUMENT_MONTH,SDH_EORA_CODE,SDH_TRN_DATE,SDH_TRN_NUMBER,	SDH_FARMER_ID,SDH_LEAD_TYPE,SDH_DEMO_DETL,SDH_DNH_REMARKS,SDH_CREATED_BY,SDH_AUTHORIZED_BY,SDH_CREATED_DATE)" +
                     " VALUES ('" + CommonData.CompanyCode + "', '" + CommonData.StateCode + "', '" + CommonData.BranchCode + "' , '" + CommonData.FinancialYear + "','" + CommonData.DocMonth + "'," + strECode + ", '" + Convert.ToDateTime(meTNRDate.Value).ToString("dd/MMM/yyyy") + "'" +
                     ",'" + txtTNRNo.Text + "',99999, '" + cmleadType.Text + "', '" + cmbDemoDtls.Text +
                     "','" + txtRemarks.Text + "','" + CommonData.LogUserId + "', '" + CommonData.LogUserId + "', '" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") + "')";

                    sqlTextCust = "INSERT INTO SALES_DNK_CUST_DETL (SDC_COMPANY_CODE,SDC_STATE_CODE,SDC_BRANCH_CODE,SDC_FIN_YEAR,SDC_DOCUMENT_MONTH,SDC_EORA_CODE,SDC_TRN_DATE,SDC_TRN_NUMBER,SDC_SL_NO,SDC_VILLAGE,SDC_FARMER_NAME,SDC_SO_FO,SDC_FORG_NAME,SDC_MOBILE_NUMBER,SDC_MANDAL,SDC_DISTRICT,SDC_STATE,SDC_PIN)" +
                        " VALUES ('" + CommonData.CompanyCode + "', '" + CommonData.StateCode + "', '" + CommonData.BranchCode + "' , '" + CommonData.FinancialYear + "','" + CommonData.DocMonth + "'," + strECode + ", '" + Convert.ToDateTime(meTNRDate.Value).ToString("dd/MMM/yyyy") +
                        "','" + txtTNRNo.Text + "',1,'" + txtVillage.Text + "','" + txtCustomerName.Text + "','" + cbRelation.Text + "','" + txtRelationName.Text + "','" + txtMobileNo.Text + "','" + txtMandal.Text + "','" + txtDistrict.Text + "','" + txtState.Text + "','" + txtPin.Text + "')";
                }
                else
                {
                    string sqlDelete = "DELETE from SALES_DNK_DETL" +
                                 " WHERE SDD_COMPANY_CODE='" + CommonData.CompanyCode +
                                     "' AND SDD_BRANCH_CODE='" + CommonData.BranchCode +
                                     "' AND SDD_TRN_NUMBER=" + txtTNRNo.Text +
                                     "  AND SDD_FIN_YEAR='" + CommonData.FinancialYear + "' AND SDD_DOCUMENT_MONTH='" + CommonData.DocMonth + "'";
                    intRec = objSQLDB.ExecuteSaveData(sqlDelete);

                    sqlText = "UPDATE SALES_DNK_HEAD SET SDH_COMPANY_CODE='" + CommonData.CompanyCode +
                        "',SDH_STATE_CODE='" + CommonData.StateCode + "',SDH_BRANCH_CODE='" + CommonData.BranchCode +
                        "',SDH_DOCUMENT_MONTH='" + CommonData.DocMonth + "',SDH_FIN_YEAR='" + CommonData.FinancialYear + "',SDH_TRN_DATE='" + Convert.ToDateTime(meTNRDate.Value).ToString("dd/MMM/yyyy") +
                        "',SDH_LEAD_TYPE='" + cmleadType.Text + "',SDH_DEMO_DETL='" + cmbDemoDtls.Text + "',SDH_DNH_REMARKS='" + txtRemarks.Text +
                        "',SDH_EORA_CODE='" + strECode + "',SDH_LAST_MODIFIED_BY='" + CommonData.LogUserId + "',SDH_LAST_MODIFIED_DATE='" + CommonData.CurrentDate +
                        "' WHERE ltrim(rtrim(SDH_TRN_NUMBER))='" + txtTNRNo.Text.Trim() + "'";

                    sqlTextCust = "UPDATE SALES_DNK_CUST_DETL SET SDC_COMPANY_CODE='" + CommonData.CompanyCode + "',SDC_STATE_CODE='" + CommonData.StateCode + "',SDC_BRANCH_CODE='" + CommonData.BranchCode + "',SDC_FIN_YEAR='" + CommonData.FinancialYear +
                        "',SDC_DOCUMENT_MONTH='" + CommonData.DocMonth + "',SDC_EORA_CODE='" + strECode + "',SDC_TRN_DATE='" + Convert.ToDateTime(meTNRDate.Value).ToString("dd/MMM/yyyy") + "',SDC_VILLAGE='" + txtVillage.Text + "',SDC_FARMER_NAME='" + txtCustomerName.Text + "',SDC_SO_FO='" + cbRelation.Text +
                        "',SDC_FORG_NAME='" + txtRelationName.Text + "',SDC_MOBILE_NUMBER='" + txtMobileNo.Text + "',SDC_MANDAL='" + txtMandal.Text + "',SDC_DISTRICT='" + txtDistrict.Text + "',SDC_STATE='" + txtState.Text + "',SDC_PIN='" + txtPin.Text +
                        "' WHERE ltrim(rtrim(SDC_TRN_NUMBER))=" + txtTNRNo.Text.Trim();
                }
                intRec = 0;
                intRec = objSQLDB.ExecuteSaveData(sqlText);
                intRec = objSQLDB.ExecuteSaveData(sqlTextCust);
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

        private int SaveDoorKnockDeatailData()
        {
            objSQLDB = new SQLDB();
            string sqlText = "";
            string sqlDelete = "";
            int intRec = 0;
            try
            {
                sqlDelete = "DELETE from SALES_DNK_DETL" +
                                " WHERE SDD_COMPANY_CODE='" + CommonData.CompanyCode +
                                    "' AND SDD_BRANCH_CODE='" + CommonData.BranchCode +
                                    "' AND SDD_TRN_NUMBER='" + txtTNRNo.Text +
                                    "' AND SDD_FIN_YEAR='" + CommonData.FinancialYear + "' AND  SDD_DOCUMENT_MONTH='" + CommonData.DocMonth + "'";
                intRec = objSQLDB.ExecuteSaveData(sqlDelete);
                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {
                    if (gvProductDetails.Rows[i].Cells["DBPoints"].Value.ToString() != "")
                    {
                        sqlText += "INSERT INTO SALES_DNK_DETL(SDD_COMPANY_CODE,SDD_STATE_CODE,SDD_BRANCH_CODE,SDD_FIN_YEAR,SDD_DOCUMENT_MONTH,SDD_EORA_CODE,SDD_TRN_DATE,SDD_TRN_NUMBER,SDD_PRODUCT_ID,SDD_SL_NO)" +
                                    " VALUES ('" + CommonData.CompanyCode + "', '" + CommonData.StateCode + "', '" + CommonData.BranchCode + "','" + CommonData.FinancialYear + "','" + CommonData.DocMonth + "','" + strECode + "','" + Convert.ToDateTime(meTNRDate.Value).ToString("dd/MMM/yyyy") + "'," + txtTNRNo.Text + ",'" +
                                     gvProductDetails.Rows[i].Cells["ProductID"].Value.ToString().Trim() + "'," + Convert.ToInt32(i + 1) + ")";
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm(this);
            txtTNRNo.Text = string.Empty;
            meTNRDate.Value = System.DateTime.Now;
            objDnkDetails = new DnkDetailsDB();
            objData = new InvoiceDB();
            objDnkDetails = null;
            objData = null;
            objSQLDB = new SQLDB();
            string strSql = "SELECT ISNULL(MAX(SDH_TRN_NUMBER),0)+1 FROM SALES_DNK_HEAD WHERE SDH_BRANCH_CODE='" + CommonData.BranchCode + "' AND SDH_DOCUMENT_MONTH='" + CommonData.DocMonth + "'";
            txtTNRNo.Text = objSQLDB.ExecuteDataSet(strSql).Tables[0].Rows[0][0].ToString();
            objSQLDB = null;
            btnSave.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtTNRNo.Text.Length > 0)
            {
                DialogResult result = MessageBox.Show("Do you want to Delete Salse Order Details?",
                                       "SSCRM Application", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    objSQLDB = new SQLDB();
                    string strDelete = " DELETE FROM SALES_DNK_HEAD " +
                                        " WHERE SDH_COMPANY_CODE='" + CommonData.CompanyCode +
                                        "' AND SDH_BRANCH_CODE='" + CommonData.BranchCode +
                                        "' AND SDH_TRN_NUMBER='" + txtTNRNo.Text +
                                        "' AND SDH_FIN_YEAR='" + CommonData.FinancialYear + "' AND SDH_DOCUMENT_MONTH='" + CommonData.DocMonth + "'";

                    strDelete += " DELETE FROM SALES_DNK_CUST_DETL " +
                                        " WHERE SDC_COMPANY_CODE='" + CommonData.CompanyCode +
                                        "' AND SDC_BRANCH_CODE='" + CommonData.BranchCode +
                                        "' AND SDC_TRN_NUMBER='" + txtTNRNo.Text +
                                        "' AND SDC_FIN_YEAR='" + CommonData.FinancialYear + "' AND SDC_DOCUMENT_MONTH='" + CommonData.DocMonth + "'";

                    strDelete += " DELETE FROM SALES_DNK_DETL " +
                                     " WHERE SDD_COMPANY_CODE='" + CommonData.CompanyCode +
                                     "' AND SDD_BRANCH_CODE='" + CommonData.BranchCode +
                                     "' AND SDD_TRN_NUMBER='" + txtTNRNo.Text +
                                     "' AND SDD_FIN_YEAR='" + CommonData.FinancialYear + "' AND SDD_DOCUMENT_MONTH='" + CommonData.DocMonth + "'";

                    int intRec = objSQLDB.ExecuteSaveData(strDelete);
                    if (intRec > 0)
                    {
                        MessageBox.Show("Salse Order " + txtTNRNo.Text + " Deleted ");
                        ClearForm(this);
                    }
                    objSQLDB = null;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
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

        private void txtVillageSearch_KeyUp(object sender, KeyEventArgs e)
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

        private void cbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            objDnkDetails = new DnkDetailsDB();
            try
            {
                if (cbCustomer.SelectedIndex > -1)
                {
                    if (this.cbCustomer.Items[cbCustomer.SelectedIndex].ToString() != "")
                    {
                        DataSet ds = new DataSet();
                        ds = objDnkDetails.DNKSearch_Get(((System.Data.DataRowView)(cbCustomer.Items[cbCustomer.SelectedIndex])).Row.ItemArray[1].ToString(), txtVillage.Text.ToString(), txtMandal.Text.ToString(), txtState.Text.ToString());
                        DataTable dt = ds.Tables[0];

                        if (dt.Rows.Count == 1)
                        {
                            strFormerid = Convert.ToString(dt.Rows[0]["SDH_FARMER_ID"] + "");
                            strECode = dt.Rows[0]["SDH_EORA_CODE"] + "";
                            cbEcode.SelectedValue = strECode;
                            meTNRDate.Value = Convert.ToDateTime(dt.Rows[0]["SDH_TRN_DATE"]);
                            txtVillage.Text = dt.Rows[0]["SDC_VILLAGE"] + "";
                            txtMandal.Text = dt.Rows[0]["SDC_MANDAL"] + "";
                            txtDistrict.Text = dt.Rows[0]["SDC_DISTRICT"] + "";
                            txtState.Text = dt.Rows[0]["SDC_STATE"] + "";
                            txtPin.Text = dt.Rows[0]["SDC_PIN"] + "";
                            txtCustomerName.Text = dt.Rows[0]["SDC_FARMER_NAME"] + "";
                            cbRelation.Text = dt.Rows[0]["SDC_SO_FO"] + "";
                            txtRelationName.Text = dt.Rows[0]["SDC_FORG_NAME"] + "";
                            txtMobileNo.Text = dt.Rows[0]["SDC_MOBILE_NUMBER"] + "";
                            txtTNRNo.Text = dt.Rows[0]["SDH_TRN_NUMBER"] + "";
                            cmleadType.Text = dt.Rows[0]["SDH_LEAD_TYPE"].ToString() + "";
                            cmbDemoDtls.Text = dt.Rows[0]["SDH_DEMO_DETL"].ToString() + "";
                            txtRemarks.Text = dt.Rows[0]["SDH_DNH_REMARKS"].ToString() + "";
                            lblCustomerId.Text = strFormerid;
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
                objDnkDetails = null;
            }
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

        private void ClearVillageDetails()
        {
            txtVillage.Text = "";
            txtMandal.Text = "";
            txtDistrict.Text = "";
            txtState.Text = "";
            txtPin.Text = "";
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

        private void btnVSearch_Click(object sender, EventArgs e)
        {
            VillageSearch VSearch = new VillageSearch("DoorKnocks");
            VSearch.objDoorKnocks = this;
            VSearch.ShowDialog();
        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            ProductSearchAll PSearch = new ProductSearchAll("DoorKnocks");
            PSearch.objFrmDoorKnocks = this;
            PSearch.ShowDialog();
        }

        private void txtTNRNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == false)
                e.Handled = true;

            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void txtTNRNo_Validated(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Convert.ToString(txtTNRNo.Text + "0")) > 0)
            {
                int intOrdNo = Convert.ToInt32(Convert.ToString(txtTNRNo.Text));
                ClearForm(this);
                txtTNRNo.Text = intOrdNo.ToString();
                FillDNKData(Convert.ToInt32(txtTNRNo.Text));
            }
        }

        private void FillDNKData(int nTnrNo)
        {
            Hashtable ht = new Hashtable();
            try
            {
                objDnkDetails = new DnkDetailsDB();
                ht = objDnkDetails.GetDNKHeadDetailsData(CommonData.StateCode, CommonData.BranchCode, nTnrNo, CommonData.DocMonth);
                DataTable dtDNKH = (DataTable)ht["DNKHead"];
                DataTable dtDNKD = (DataTable)ht["DNKDetail"];
                if (dtDNKH.Rows.Count > 0)
                {
                    nTnrNo = Convert.ToInt32(dtDNKH.Rows[0]["SDH_TRN_NUMBER"]);
                    strFormerid = Convert.ToString(dtDNKH.Rows[0]["SDH_FARMER_ID"] + "");
                    strECode = dtDNKH.Rows[0]["SDH_EORA_CODE"] + "";
                    txtEcodeSearch.Text = strECode;
                    txtEcodeSearch_KeyUp(null, null);
                    meTNRDate.Value = Convert.ToDateTime(dtDNKH.Rows[0]["SDH_TRN_DATE"]);
                    cbVillage.Text = dtDNKH.Rows[0]["SDC_VILLAGE"] + "";
                    txtVillage.Text = dtDNKH.Rows[0]["SDC_VILLAGE"] + "";
                    txtMandal.Text = dtDNKH.Rows[0]["SDC_MANDAL"] + "";
                    txtDistrict.Text = dtDNKH.Rows[0]["SDC_DISTRICT"] + "";
                    txtState.Text = dtDNKH.Rows[0]["SDC_STATE"] + "";
                    txtPin.Text = dtDNKH.Rows[0]["SDC_PIN"] + "";
                    txtCustomerName.Text = dtDNKH.Rows[0]["SDC_FARMER_NAME"] + "";
                    cbRelation.Text = dtDNKH.Rows[0]["SDC_SO_FO"] + "";
                    txtRelationName.Text = dtDNKH.Rows[0]["SDC_FORG_NAME"] + "";
                    txtMobileNo.Text = dtDNKH.Rows[0]["SDC_MOBILE_NUMBER"] + "";
                    txtTNRNo.Text = dtDNKH.Rows[0]["SDH_TRN_NUMBER"] + "";
                    cmleadType.Text = dtDNKH.Rows[0]["SDH_LEAD_TYPE"].ToString();
                    cmbDemoDtls.Text = dtDNKH.Rows[0]["SDH_DEMO_DETL"].ToString();
                    txtRemarks.Text = dtDNKH.Rows[0]["SDH_DNH_REMARKS"].ToString();
                    lblCustomerId.Text = strFormerid;
                    FillDNKDetail(dtDNKD);
                    iUpdateVal = 1;

                    if (Convert.ToDateTime(dtDNKH.Rows[0]["SDH_TRN_DATE"]) < Convert.ToDateTime(CommonData.DocFDate) || Convert.ToDateTime(dtDNKH.Rows[0]["SDH_TRN_DATE"]) > Convert.ToDateTime(CommonData.DocTDate))
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
                else
                {
                    iUpdateVal = 0;
                    btnSave.Enabled = true;
                    btnDelete.Enabled = true;
                    MessageBox.Show("This transaction number data not available.", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void FillDNKDetail(DataTable dt)
        {
            try
            {
                int intRow = 1;
                gvProductDetails.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["SDD_PRODUCT_ID"].ToString().Length > 0)
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = intRow;
                        tempRow.Cells.Add(cellSLNO);

                        DataGridViewCell cellMainProductID = new DataGridViewTextBoxCell();
                        cellMainProductID.Value = dt.Rows[i]["SDD_Product_id"];
                        tempRow.Cells.Add(cellMainProductID);

                        DataGridViewCell cellMainProduct = new DataGridViewTextBoxCell();
                        cellMainProduct.Value = dt.Rows[i]["pm_product_name"] + "(Rs = " + Convert.ToDecimal(dt.Rows[i]["SingleMRP"]).ToString("0") + ")";
                        tempRow.Cells.Add(cellMainProduct);

                        DataGridViewCell cellDessc = new DataGridViewTextBoxCell();
                        cellDessc.Value = dt.Rows[i]["category_name"];
                        tempRow.Cells.Add(cellDessc);

                        DataGridViewCell cellPoints = new DataGridViewTextBoxCell();
                        cellPoints.Value = Convert.ToDouble(dt.Rows[i]["Points"]).ToString("f");
                        tempRow.Cells.Add(cellPoints);

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

        private void txtCustomerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtRelationName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtCustomerSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtCustomerSearch.Text.Length > 0)
                {
                    blCustomerSearch = true;
                    if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                    {
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

        private bool FindInputCustomerSearch()
        {
            bool blFind = false;
            objDnkDetails = new DnkDetailsDB();
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
                        ds = objDnkDetails.DNKSearch_Get(strItem, txtVillage.Text, txtMandal.Text, txtState.Text);
                        DataTable dt = ds.Tables[0];

                        if (dt.Rows.Count == 1)
                        {
                            strFormerid = Convert.ToString(dt.Rows[0]["SDH_FARMER_ID"] + "");
                            strECode = dt.Rows[0]["SDH_EORA_CODE"] + "";
                            cbEcode.SelectedValue = strECode;
                            meTNRDate.Value = Convert.ToDateTime(dt.Rows[0]["SDH_TRN_DATE"]);
                            txtVillage.Text = dt.Rows[0]["SDC_VILLAGE"] + "";
                            txtMandal.Text = dt.Rows[0]["SDC_MANDAL"] + "";
                            txtDistrict.Text = dt.Rows[0]["SDC_DISTRICT"] + "";
                            txtState.Text = dt.Rows[0]["SDC_STATE"] + "";
                            txtPin.Text = dt.Rows[0]["SDC_PIN"] + "";
                            txtCustomerName.Text = dt.Rows[0]["SDC_FARMER_NAME"] + "";
                            cbRelation.Text = dt.Rows[0]["SDC_SO_FO"] + "";
                            txtRelationName.Text = dt.Rows[0]["SDC_FORG_NAME"] + "";
                            txtMobileNo.Text = dt.Rows[0]["SDC_MOBILE_NUMBER"].ToString();
                            txtTNRNo.Text = dt.Rows[0]["SDH_TRN_NUMBER"].ToString();
                            cmleadType.Text = dt.Rows[0]["SDH_LEAD_TYPE"].ToString();
                            cmbDemoDtls.Text = dt.Rows[0]["SDH_DEMO_DETL"].ToString();
                            txtRemarks.Text = dt.Rows[0]["SDH_DNH_REMARKS"].ToString();
                            lblCustomerId.Text = strFormerid;
                            break;
                        }
                    }
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

        private void FillCustomerFarmerData(string sSearch, string sCustId)
        {
            DataSet ds = null;
            objDnkDetails = new DnkDetailsDB();
            try
            {
                if (sSearch.Trim() != "" || sCustId.Length > 0)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    ds = new DataSet();
                    ds = objDnkDetails.DNKSearch_Get(sSearch, txtVillage.Text, txtMandal.Text, txtState.Text);
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count == 1)
                    {
                        strFormerid = Convert.ToString(dt.Rows[0]["SDH_FARMER_ID"] + "");
                        strECode = dt.Rows[0]["SDH_EORA_CODE"] + "";
                        cbEcode.SelectedValue = strECode;
                        meTNRDate.Value = Convert.ToDateTime(dt.Rows[0]["SDH_TRN_DATE"]);
                        txtVillage.Text = dt.Rows[0]["SDC_VILLAGE"] + "";
                        txtMandal.Text = dt.Rows[0]["SDC_MANDAL"] + "";
                        txtDistrict.Text = dt.Rows[0]["SDC_DISTRICT"] + "";
                        txtState.Text = dt.Rows[0]["SDC_STATE"] + "";
                        txtPin.Text = dt.Rows[0]["SDC_PIN"] + "";
                        txtCustomerName.Text = dt.Rows[0]["SDC_FARMER_NAME"] + "";
                        cbRelation.Text = dt.Rows[0]["SDC_SO_FO"] + "";
                        txtRelationName.Text = dt.Rows[0]["SDC_FORG_NAME"] + "";
                        txtMobileNo.Text = dt.Rows[0]["SDC_MOBILE_NUMBER"] + "";
                        txtTNRNo.Text = dt.Rows[0]["SDH_TRN_NUMBER"] + "";
                        cmleadType.Text = dt.Rows[0]["SDH_LEAD_TYPE"].ToString();
                        cmbDemoDtls.Text = dt.Rows[0]["SDH_DEMO_DETL"].ToString();
                        txtRemarks.Text = dt.Rows[0]["SDH_DNH_REMARKS"].ToString();
                        lblCustomerId.Text = strFormerid;
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
                        strFormerid = "";
                        //txtTNRNo.Text = "";
                        //meTNRDate.Text = "";
                        lblCustomerId.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objDnkDetails = null;
                ds.Dispose();
                Cursor.Current = Cursors.Default;
            }
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
                dataCustomer.Rows.Add(new String[] { dt.Rows[i]["SDH_FARMER_ID"] + 
                     "", dt.Rows[i]["SDC_FARMER_NAME"] + "", dt.Rows[i]["SDC_FORG_NAME"] + 
                     "", dt.Rows[i]["SDC_VILLAGE"] + ""});


            cbCustomer.DataSource = dataCustomer;
            cbCustomer.DisplayMember = "farmer_name";
            cbCustomer.ValueMember = "farmer_ID";
        }

        private void btnClearProd_Click(object sender, EventArgs e)
        {
            gvProductDetails.Rows.Clear();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void txtMobileNo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
