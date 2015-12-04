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
    public partial class GoodsReceiptNotePU : Form
    {
        private SQLDB objSQLDB = null;
        private UtilityDB  objUtilData = null;
        private ProductUnitDB objPUDB = null;
        private ReportViewer childReportViewer = null;
        private General objGeneral = null;
        private string strFormerid = string.Empty;
        private string strTransactionNo = string.Empty;
        private string strBranchCode = string.Empty;
        private string strECode = string.Empty;
        private bool blIsCellQty = true;
        private bool IsModify = false;
        private string strToBranchCode = string.Empty;
        private bool TranTypeLoad = false;
        private string sFrmType = "";
        public GoodsReceiptNotePU()
        {
            InitializeComponent();
        }
        public GoodsReceiptNotePU(string sFrm)
        {
            sFrmType = sFrm;
            InitializeComponent();
        }
        private void GoodsReceiptNote_Load(object sender, EventArgs e)
        {
            txtDocMonth.Text = CommonData.DocMonth;
            FillTransactionType();
            FillBranchData();
            FillVehcleType();
            cbVehicleType.SelectedIndex = 0;
            dtpTranDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            txtTransactionNo.Text = NewTransactionNumber().ToString();

            FillDriverEmployeeData();
            cbTransactionType.SelectedIndex = 0;

            if (cbBranches.Items.Count != 0)
                cbBranches.SelectedIndex = 0;
            dtpTranDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            gvReqDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                      System.Drawing.FontStyle.Regular);
            gvPackingDetl.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                      System.Drawing.FontStyle.Regular);

            gvReqDetails.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Bold);
         
        }
        


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void cbTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTransactionType.SelectedIndex > -1 && TranTypeLoad == false)
            {
                FillBranchData();
                if (cbBranches.Items.Count != 0)
                    cbBranches.SelectedIndex = 0;
            }
            if (IsModify != true)
                txtTransactionNo.Text = NewTransactionNumber();
        }
        private void FillTransactionType()
        {
            objUtilData = new UtilityDB();
            try
            {
                DataTable dt = objUtilData.dtGRNPUTranType();
                if (dt.Rows.Count > 0)
                {
                    TranTypeLoad = true;
                    cbTransactionType.DataSource = dt;
                    cbTransactionType.DisplayMember = "type";
                    cbTransactionType.ValueMember = "name";
                }
                TranTypeLoad = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {

                objUtilData = null;
                Cursor.Current = Cursors.Default;
            }
        }
        private void FillBranchData()
        {
            objPUDB = new ProductUnitDB();
            try
            {
                string sTrnType = "";
                if (cbTransactionType.Text == "SP2PU")
                    sTrnType = "SP2SP";
                else if (cbTransactionType.Text == "PU2PU")
                    sTrnType = "PU2PU";
                else if (cbTransactionType.Text == "BR2PU")
                    sTrnType = "PU2BR";
                else if (cbTransactionType.Text == "TU2PU")
                    sTrnType = "PU2TU";
                //else if (cbTransactionType.Text == "PU2CU")
                //    sTrnType = "PU2CU";
                else if (cbTransactionType.Text == "OL2PU")
                    sTrnType = "PU2OL";
                cbBranches.Items.Clear();
                DataTable dtBranch = objPUDB.BranchListForDC_Get(CommonData.CompanyCode, sTrnType).Tables[0];
                DataView dv1 = dtBranch.DefaultView;
                dv1.RowFilter = " active = 'T' ";
                DataTable dtBR = dv1.ToTable();
                if (dtBR.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dtBR.Rows)
                    {
                        ComboboxItem objItem = new ComboboxItem();
                        objItem.Value = dataRow["branch_code"].ToString();
                        objItem.Text = dataRow["branch_name"].ToString();
                        cbBranches.Items.Add(objItem);
                        objItem = null;
                    }

                }
                dtBranch = null;
                dv1 = null;
                dtBR = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {

                objPUDB = null;
                Cursor.Current = Cursors.Default;
            }
        }
        private void FillVehcleType()
        {
            objUtilData = new UtilityDB();
            try
            {
                DataTable dt = objUtilData.dtVehicleType();
                if (dt.Rows.Count > 0)
                {
                    cbVehicleType.DataSource = dt;
                    cbVehicleType.DisplayMember = "type";
                    cbVehicleType.ValueMember = "name";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {

                objUtilData = null;
                Cursor.Current = Cursors.Default;
            }
        }
        private void FillDriverEmployeeData()
        {
            objPUDB = new ProductUnitDB();

            try
            {
                cbDriverEcode.DataSource = null;
                cbDriverEcode.Items.Clear();
                DataTable dtEcode = objPUDB.DCOtherEmployeeList_Get().Tables[0];
                if (dtEcode.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dtEcode.Rows)
                    {

                        ComboboxItem objItem = new ComboboxItem();
                        objItem.Value = dataRow["ENAME"].ToString();
                        objItem.Text = dataRow["ENAME"].ToString();
                        cbDriverEcode.Items.Add(objItem);
                        objItem = null;
                    }

                }
                dtEcode = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objPUDB = null;
            }

        }
        private string NewTransactionNumber()
        {
            string sTranNo = string.Empty;
            objPUDB = new ProductUnitDB();
            try
            {
                sTranNo = objPUDB.GenerateNewGRNTranNo(CommonData.CompanyCode, CommonData.BranchCode, sFrmType);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objPUDB = null;
            }

            return sTranNo;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm(this);
            cmbDCType.SelectedText = "DC";
            txtDCNo.Text = "";
            IsModify = false;
            dtpTranDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            gvReqDetails.Rows.Clear();
            gvPackingDetl.Rows.Clear();
            txtDocMonth.Text = CommonData.DocMonth;
            txtTransactionNo.Text = NewTransactionNumber().ToString();
            IsModify = false;
            chkCancelSPGRN.Checked = false;
            txtRemarks.Text = "";
            txtDocMonth.Text = CommonData.DocMonth.ToUpper();
        }
        private void ClearForm(System.Windows.Forms.Control parent)
        {
            strTransactionNo = string.Empty;
            foreach (System.Windows.Forms.Control ctrControl in parent.Controls)
            {
                //Loop through all controls 
                if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.TextBox)))
                {
                    //Check to see if it's a textbox 
                    ((System.Windows.Forms.TextBox)ctrControl).Text = string.Empty;

                    //If it is then set the text to String.Empty (empty textbox) 
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.MaskedTextBox)))
                {

                    //Check to see if it's a textbox 
                    ((System.Windows.Forms.MaskedTextBox)ctrControl).Text = string.Empty;
                    //If it is then set the text to String.Empty (empty textbox) 
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.RichTextBox)))
                {
                    ((System.Windows.Forms.RichTextBox)ctrControl).Text = string.Empty;
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.ComboBox)))
                {
                    //cbVehicleType.SelectedIndex = 0;
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
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int intSave = 0;
            objSQLDB = new SQLDB();
            string strCommand = "";
            
            if (CheckData())
            {
                try
                {
                    if (chkCancelSPGRN.Checked == true)
                    {
                        if (IsModify == false)
                        {

                            strCommand = " INSERT INTO PU_GRN_HEAD(PUGH_COMPANY_CODE" +
                                                                ", PUGH_STATE_CODE" +
                                                                ", PUGH_BRANCH_CODE" +
                                                                ", PUGH_FIN_YEAR" +
                                                                ", PUGH_DOCUMENT_MONTH" +
                                                                ", PUGH_TRN_TYPE" +
                                                                ", PUGH_TRN_NUMBER" +
                                                                ", PUGH_REF_NO" +
                                                                ", PUGH_TRN_DATE" +
                                                                ", PUGH_VEHICLE_SOURCE" +
                                                                ", PUGH_CREATED_BY" +
                                                                ", PUGH_CREATED_DATE" +
                                                                ", PUGH_STATUS" +
                                                                ", PUGH_REMARKS)" +
                                                                "VALUES " +
                                                                "('" + CommonData.CompanyCode +
                                                                "', '" + CommonData.StateCode +
                                                                "', '" + CommonData.BranchCode +
                                                                "', '" + CommonData.FinancialYear +
                                                                "', '" + Convert.ToDateTime(dtpTranDate.Value.ToString()).ToString("MMMyyyy").ToUpper() +
                                                                "', '" + cbTransactionType.Text +
                                                                "', '" + txtTransactionNo.Text +
                                                                "', '" + txtRefNo.Text.Replace(" ","").ToString() +
                                                                "', '" + Convert.ToDateTime(dtpTranDate.Value.ToString()).ToString("dd/MMM/yyyy") +
                                                                "', '" + cbVehicleType.Text +
                                                                "', '" + CommonData.LogUserId +
                                                                "', '" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                                                "','CANCELLED','" + txtRemarks.Text.ToString() + "')";
                        }
                        else if (IsModify == true)
                        {
                            strCommand = " UPDATE PU_GRN_HEAD " +
                                         "SET PUGH_TRN_TYPE ='" + cbTransactionType.Text +
                                         "', PUGH_DOCUMENT_MONTH ='" + txtDocMonth.Text.ToUpper() +
                                         "', PUGH_TRN_DATE ='" + Convert.ToDateTime(dtpTranDate.Value).ToString("dd/MMM/yyyy") +
                                         "', PUGH_VEHICLE_SOURCE='" + cbVehicleType.Text +
                                         "', PUGH_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                                         "', PUGH_LAST_MODIFIED_DATE='" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                         "',PUGH_REMARKS='"+ txtRemarks.Text.ToString() +
                                         "' WHERE PUGH_TRN_NUMBER= '" + txtTransactionNo.Text +
                                         "'  AND PUGH_BRANCH_CODE='" + CommonData.BranchCode +
                                         "' AND PUGH_FIN_YEAR='" + CommonData.FinancialYear.ToString() +
                                         "' AND PUGH_COMPANY_CODE='" + CommonData.CompanyCode.ToString() + "'";

                        }
                        if (strCommand.Length > 10)
                        {
                            intSave = objSQLDB.ExecuteSaveData(strCommand);
                        }
                        if (intSave > 0)
                        {
                            IsModify = false;
                            MessageBox.Show("GRN data saved.\n Transaction No:" + txtTransactionNo.Text.ToString(), "Goods Receipt Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm(this);
                            txtTransactionNo.Text = NewTransactionNumber().ToString();
                            chkCancelSPGRN.Checked = false;
                            txtRemarks.Text = "";
                            gvReqDetails.Rows.Clear();
                            gvPackingDetl.Rows.Clear();
                            txtDocMonth.Text = CommonData.DocMonth.ToUpper();
                        }
                        else
                        {
                            MessageBox.Show("GRN data not saved.", "Goods Receipt Note", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        if (SaveDCHeadData() > 0)
                        {
                            intSave = SaveDCDetlData();
                            //if (gvPackingDetl.Rows.Count > 0)
                            SaveDCPMDetlData();
                        }
                        else
                        {
                            string strSQL = "DELETE from PU_GRN_HEAD" +
                                        " WHERE PUGH_COMPANY_CODE='" + CommonData.CompanyCode +
                                        "' AND PUGH_BRANCH_CODE='" + CommonData.BranchCode +
                                        "' AND PUGH_TRN_NUMBER='" + txtTransactionNo.Text +
                                        "' AND PUGH_FIN_YEAR='" + CommonData.FinancialYear + "'";
                            objSQLDB = new SQLDB();
                            try
                            {
                                int intDel = objSQLDB.ExecuteSaveData(strSQL);
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

                        if (intSave > 0)
                        {
                            IsModify = false;
                            MessageBox.Show("GRN data saved.\n Transaction No:" + txtTransactionNo.Text.ToString(), "Goods Receipt Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm(this);
                            txtTransactionNo.Text = NewTransactionNumber().ToString();
                            chkCancelSPGRN.Checked = false;
                            txtRemarks.Text = "";
                            gvReqDetails.Rows.Clear();
                            gvPackingDetl.Rows.Clear();
                            txtDocMonth.Text = CommonData.DocMonth.ToUpper();
                        }
                        else
                        {
                            MessageBox.Show("GRN data not saved.", "Goods Receipt Note", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                }
                catch (Exception ex)
                {
                    string strSQL = "DELETE from PU_GRN_HEAD" +
                                       " WHERE PUGH_COMPANY_CODE='" + CommonData.CompanyCode +
                                       "' AND PUGH_BRANCH_CODE='" + CommonData.BranchCode +
                                       "' AND PUGH_TRN_NUMBER='" + txtTransactionNo.Text +
                                       "'  AND PUGH_FIN_YEAR='" + CommonData.FinancialYear + "'";
                    objSQLDB = new SQLDB();
                    int intDel = objSQLDB.ExecuteSaveData(strSQL);
                    objSQLDB = null;

                    MessageBox.Show(ex.Message, "GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally
                {

                }
            }
        }
        private bool CheckData()
        {
            bool blValue = true;
            if (Convert.ToString(txtRefNo.Text).Length == 0)
            {
                MessageBox.Show("Enter DC Reference No !", "GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                return blValue;
            }
            if (Convert.ToString(txtTransactionNo.Text).Length == 0)
            {
                MessageBox.Show("Transaction no is not generated!\n Contact to IT-Department", "GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                return blValue;
            }
            if (Convert.ToString(txtTransactionNo.Text).Length < 20)
            {
                MessageBox.Show("Invalid Transaction Number!", "GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtTransactionNo.Focus();
                return blValue;
            }
            if (Convert.ToString(txtTransactionNo.Text).IndexOf('-') == -1)
            {
                MessageBox.Show("Invalid Transaction Number!", "GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtTransactionNo.Focus();
                return blValue;
            }

            if (chkCancelSPGRN.Checked == true)
            {
                if (txtRemarks.Text.Length <= 20)
                {
                    MessageBox.Show("Enter Remarks", "DC/DCST PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    blValue = false;
                    txtRemarks.Focus();
                    return blValue;
                }
            }
            else if (chkCancelSPGRN.Checked == false)
            {

                if (cbBranches.SelectedIndex == -1)
                {
                    MessageBox.Show("Select To Branch!", "GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    blValue = false;
                    cbBranches.Focus();
                    return blValue;
                }
                
                //if (txtEmpName.Text.Length == 0 || txtEcodeSearch.Text.Length == 0)
                //{
                //    MessageBox.Show("Enter Valid Ecode!", "GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //    blValue = false;
                //    txtEcodeSearch.Focus();
                //    return blValue;
                //}

                //if (Convert.ToString(txtTotalFreight.Text).Trim().Length == 0)
                //{
                //    MessageBox.Show("Enter Freight !", "GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //    blValue = false;
                //    txtTotalFreight.Focus();
                //    return blValue;
                //}

                //if (Convert.ToString(txtTripLRNo.Text).Trim().Length == 0)
                //{
                //    MessageBox.Show("Enter Trip / LR Number !", "GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //    blValue = false;
                //    txtTripLRNo.Focus();
                //    return blValue;
                //}

                //if (Convert.ToString(txtVehicleNo.Text).Trim().Length == 0)
                //{
                //    MessageBox.Show("Enter VehicleNo !", "GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //    blValue = false;
                //    txtVehicleNo.Focus();
                //    return blValue;
                //}
                
                bool blInvDtl = false;
                for (int i = 0; i < gvReqDetails.Rows.Count; i++)
                {
                    try
                    { Convert.ToDouble(Convert.ToString(gvReqDetails.Rows[i].Cells["IssQty"].Value)); }
                    catch
                    { gvReqDetails.Rows[i].Cells["IssQty"].Value = "0"; }
                    try
                    { Convert.ToDouble(Convert.ToString(gvReqDetails.Rows[i].Cells["IssDmgQty"].Value)); }
                    catch
                    { gvReqDetails.Rows[i].Cells["IssDmgQty"].Value = "0"; }

                    if ((Convert.ToDouble(Convert.ToString(gvReqDetails.Rows[i].Cells["IssDmgQty"].Value)) + Convert.ToDouble(Convert.ToString(gvReqDetails.Rows[i].Cells["IssQty"].Value))) > 0)
                    {
                        blInvDtl = true;
                    }

                }

                if (blInvDtl == false)
                {
                    blValue = false;
                    MessageBox.Show("Enter product quantity", "GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }


            return blValue;
        }

        private int SaveDCHeadData()
        {
            int intSave = 0;
            string strSQL = string.Empty;
            objSQLDB = new SQLDB();
            string strDriver = string.Empty;
            string sEcode = "0";
            try
            {
                if (cbVehicleType.Text == "OWN")
                    strDriver = cbDriverEcode.Text.ToString();
                else
                    strDriver = txtDriverNo.Text.ToString();

                try { Convert.ToDouble(txtTotalFreight.Text); }
                catch { txtTotalFreight.Text = "0"; }
                try { Convert.ToDouble(txtAdvancePaid.Text); }
                catch { txtAdvancePaid.Text = "0"; }
                try { Convert.ToDouble(txtToPay.Text); }
                catch { txtToPay.Text = "0"; }
                try { Convert.ToDouble(txtLoadingCharges.Text); }
                catch { txtLoadingCharges.Text = "0"; }
                try { Convert.ToInt32(txtEcodeSearch.Text); }
                catch { txtEcodeSearch.Text = "0"; }

                if (txtTotalFreight.Text.Replace(" ","").ToString().Trim().Length == 0)
                    txtTotalFreight.Text = "0.00";
                if (txtAdvancePaid.Text.ToString().Trim().Length == 0)
                    txtAdvancePaid.Text = "0.00";
                if (txtToPay.Text.ToString().Trim().Length == 0)
                    txtToPay.Text = "0.00";
                if (txtLoadingCharges.Text.ToString().Trim().Length == 0)
                    txtLoadingCharges.Text = "0.00";
                if (txtEcodeSearch.Text.Length > 0)
                    sEcode = txtEcodeSearch.Text;
                else
                    sEcode = "0";
                if (IsModify == false)
                {
                    txtTransactionNo.Text = NewTransactionNumber().ToString();
                    if (txtTransactionNo.Text.ToString().Length > 5)
                    {
                        strSQL = " INSERT INTO PU_GRN_HEAD " +
                             "(PUGH_COMPANY_CODE" +
                             ", PUGH_STATE_CODE" +
                             ", PUGH_BRANCH_CODE" +
                             ", PUGH_FIN_YEAR" +
                             ", PUGH_DOCUMENT_MONTH" +
                             ", PUGH_TRN_TYPE" +
                             ", PUGH_TRN_NUMBER" +
                             ", PUGH_REF_NO" +
                             ", PUGH_TRN_DATE" +
                             ", PUGH_TO_BRANCH_CODE" +
                             ", PUGH_TO_ECODE" +
                             ", PUGH_TRIP_OR_LR_NUMBER" +
                             ", PUGH_WAY_BILL_NO" +
                             ", PUGH_VEHICLE_SOURCE" +
                             ", PUGH_VEHICLE_NUMBER" +
                             ", PUGH_TRANSPORTER_NAME" +
                             ", PUGH_DRIVER_NAME" +
                             ", PUGH_TOTAL_FREIGHT" +
                             ", PUGH_ADVANCE_PAID" +
                             ", PUGH_TO_PAY_FREIGHT" +
                             ", PUGH_LOADING_CHARGES" +
                             ", PUGH_DC_TYPE" +
                             ", PUGH_DC_NO" +
                             ", PUGH_CREATED_BY" +
                             ", PUGH_CREATED_DATE)" +
                             "VALUES " +
                             "('" + CommonData.CompanyCode +
                             "', '" + CommonData.StateCode +
                             "', '" + CommonData.BranchCode +
                             "', '" + CommonData.FinancialYear +
                             "', '" + txtDocMonth.Text.ToUpper() +
                             "', '" + cbTransactionType.Text +
                             "', '" + txtTransactionNo.Text +
                             "', '" + txtRefNo.Text.Replace(" ", "").ToString() +
                             "', '" + Convert.ToDateTime(dtpTranDate.Value.ToString()).ToString("dd/MMM/yyyy") +
                             "', '" + ((SSCRM.ComboboxItem)(cbBranches.SelectedItem)).Value +
                             "', " + sEcode +
                             ", '" + txtTripLRNo.Text.Replace(" ", "") +
                             "', '" + txtWayBillNo.Text.Replace(" ", "") +
                             "', '" + cbVehicleType.Text +
                             "', '" + txtVehicleNo.Text.Replace(" ", "") +
                             "', '" + txtTransporter.Text +
                             "', '" + cbDriverEcode.Text +
                             "', " + txtTotalFreight.Text +
                             ", " + txtAdvancePaid.Text +
                             ", " + txtToPay.Text +
                             ", " + txtLoadingCharges.Text +
                             ", '" + cmbDCType.Text +
                             "', '" + txtDCNo.Text.Replace(" ", "") +
                             "', '" + CommonData.LogUserId +
                             "', getdate())";
                    }
                }
                else
                {
                    strSQL = "DELETE from PU_GRN_DETL" +
                                " WHERE PUGD_COMPANY_CODE='" + CommonData.CompanyCode +
                                    "' AND PUGD_BRANCH_CODE='" + CommonData.BranchCode +
                                    "' AND PUGD_TRN_NUMBER='" + txtTransactionNo.Text +
                                    "' AND PUGD_FIN_YEAR='" + CommonData.FinancialYear + "'";

                    int intRec = objSQLDB.ExecuteSaveData(strSQL);

                    strSQL = " UPDATE PU_GRN_HEAD " +
                                "SET PUGH_TRN_TYPE ='" + cbTransactionType.Text +
                                "', PUGH_DOCUMENT_MONTH ='" + txtDocMonth.Text.ToUpper() +
                                "', PUGH_TRN_DATE ='" + Convert.ToDateTime(dtpTranDate.Value).ToString("dd/MMM/yyyy") +
                                "', PUGH_TO_BRANCH_CODE='" + ((SSCRM.ComboboxItem)(cbBranches.SelectedItem)).Value +
                                "', PUGH_TO_ECODE=" + sEcode +
                                ", PUGH_TRIP_OR_LR_NUMBER='" + txtTripLRNo.Text +
                                "', PUGH_WAY_BILL_NO='" + txtWayBillNo.Text +
                                "', PUGH_VEHICLE_SOURCE='" + cbVehicleType.Text +
                                "', PUGH_VEHICLE_NUMBER='" + txtVehicleNo.Text +
                                "', PUGH_TRANSPORTER_NAME='" + txtTransporter.Text +
                                "', PUGH_DRIVER_NAME='" + cbDriverEcode.Text +
                                "', PUGH_TOTAL_FREIGHT=" + txtTotalFreight.Text +
                                ", PUGH_ADVANCE_PAID=" + txtAdvancePaid.Text +
                                ", PUGH_TO_PAY_FREIGHT=" + txtToPay.Text +
                                ", PUGH_LOADING_CHARGES=" + txtLoadingCharges.Text +
                                ", PUGH_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                                "', PUGH_LAST_MODIFIED_DATE=getdate()" + 
                                ", PUGH_DC_TYPE='" + cmbDCType.Text +
                                "', PUGH_DC_NO='" + txtDCNo.Text.Replace(" ","") +
                             "' WHERE PUGH_TRN_NUMBER='" + txtTransactionNo.Text +
                             "'  AND PUGH_BRANCH_CODE='" + CommonData.BranchCode +
                             "' AND PUGH_FIN_YEAR='" + CommonData.FinancialYear.ToString() +
                             "' AND PUGH_COMPANY_CODE='" + CommonData.CompanyCode.ToString() + "'";

                }
                if (strSQL.Length > 10)
                    intSave = objSQLDB.ExecuteSaveData(strSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                objSQLDB = null;
            }

            return intSave;
        }
        private int SaveDCDetlData()
        {
            int intSave = 0;
            string strSQL = string.Empty;
            StringBuilder sbSQL = new StringBuilder();
            objSQLDB = new SQLDB();
            try
            {
                strSQL = "DELETE from PU_GRN_DETL" +
                                " WHERE PUGD_COMPANY_CODE='" + CommonData.CompanyCode +
                                    "' AND PUGD_BRANCH_CODE='" + CommonData.BranchCode +
                                    "' AND PUGD_TRN_NUMBER='" + txtTransactionNo.Text +
                                    "' AND PUGD_FIN_YEAR='" + CommonData.FinancialYear + "'";

                intSave = objSQLDB.ExecuteSaveData(strSQL);
                int islno = 0;
                for (int i = 0; i < gvReqDetails.Rows.Count; i++)
                {
                    try
                    { Convert.ToDouble(gvReqDetails.Rows[i].Cells["Amount"].Value.ToString()); }
                    catch
                    { gvReqDetails.Rows[i].Cells["Amount"].Value = "0"; }

                    if (gvReqDetails.Rows[i].Cells["IssDmgQty"].Value.ToString().Trim() == "")
                        gvReqDetails.Rows[i].Cells["IssDmgQty"].Value = "0";
                    if (gvReqDetails.Rows[i].Cells["IssQty"].Value.ToString().Trim() == "")
                        gvReqDetails.Rows[i].Cells["IssQty"].Value = "0";

                    try { Convert.ToDouble(gvReqDetails.Rows[i].Cells["IssDmgQty"].Value.ToString()); }
                    catch { gvReqDetails.Rows[i].Cells["IssDmgQty"].Value = "0"; }
                    try { Convert.ToDouble(gvReqDetails.Rows[i].Cells["IssQty"].Value.ToString()); }
                    catch { gvReqDetails.Rows[i].Cells["IssQty"].Value = "0"; }
                    try { Convert.ToDouble(gvReqDetails.Rows[i].Cells["Rate"].Value.ToString()); }
                    catch { gvReqDetails.Rows[i].Cells["Rate"].Value = "0"; }
                    try { Convert.ToDouble(gvReqDetails.Rows[i].Cells["BedPer"].Value.ToString()); }
                    catch { gvReqDetails.Rows[i].Cells["BedPer"].Value = "0"; }
                    try { Convert.ToDouble(gvReqDetails.Rows[i].Cells["BedVal"].Value.ToString()); }
                    catch { gvReqDetails.Rows[i].Cells["BedVal"].Value = "0"; }
                    try { Convert.ToDouble(gvReqDetails.Rows[i].Cells["EDCessPer"].Value.ToString()); }
                    catch { gvReqDetails.Rows[i].Cells["EDCessPer"].Value = "0"; }
                    try { Convert.ToDouble(gvReqDetails.Rows[i].Cells["EDCessVal"].Value.ToString()); }
                    catch { gvReqDetails.Rows[i].Cells["EDCessVal"].Value = "0"; }
                    try { Convert.ToDouble(gvReqDetails.Rows[i].Cells["SecEDCessPer"].Value.ToString()); }
                    catch { gvReqDetails.Rows[i].Cells["SecEDCessPer"].Value = "0"; }
                    try { Convert.ToDouble(gvReqDetails.Rows[i].Cells["SecEDCessVal"].Value.ToString()); }
                    catch { gvReqDetails.Rows[i].Cells["SecEDCessVal"].Value = "0"; }
                    try { Convert.ToDouble(gvReqDetails.Rows[i].Cells["TaxPer"].Value.ToString()); }
                    catch { gvReqDetails.Rows[i].Cells["TaxPer"].Value = "0"; }
                    try { Convert.ToDouble(gvReqDetails.Rows[i].Cells["TaxVal"].Value.ToString()); }
                    catch { gvReqDetails.Rows[i].Cells["TaxVal"].Value = "0"; }
                    try { Convert.ToDouble(gvReqDetails.Rows[i].Cells["IssAmt"].Value.ToString()); }
                    catch { gvReqDetails.Rows[i].Cells["IssAmt"].Value = "0"; }
                    try { Convert.ToDouble(gvReqDetails.Rows[i].Cells["ReqNo"].Value.ToString()); }
                    catch { gvReqDetails.Rows[i].Cells["ReqNo"].Value = "0"; }
                    try { Convert.ToDouble(gvReqDetails.Rows[i].Cells["ReqSlno"].Value.ToString()); }
                    catch { gvReqDetails.Rows[i].Cells["ReqSlno"].Value = "0"; }

                    double iTotalQty = 0;
                    iTotalQty = Convert.ToDouble(gvReqDetails.Rows[i].Cells["IssQty"].Value) + Convert.ToDouble(gvReqDetails.Rows[i].Cells["IssDmgQty"].Value);
                    if (iTotalQty != 0 && gvReqDetails.Rows[i].Cells["Amount"].Value.ToString() != "") //&& gvReqDetails.Rows[i].Cells["Amount"].Value.ToString() != "0.00")
                    {
                        islno++;
                        sbSQL.Append("INSERT INTO PU_GRN_DETL (PUGD_COMPANY_CODE, PUGD_STATE_CODE, PUGD_BRANCH_CODE, PUGD_FIN_YEAR" +
                                    ", PUGD_TRN_TYPE, PUGD_TRN_NUMBER, PUGD_SL_NO, PUGD_BATCH_NO, PUGD_PRODUCT_ID, PUGD_GOOD_QTY" +
                                    ", PUGD_BAD_QTY, PUGD_ISS_RATE, " +
                                    "PUGD_BED_PED_PERC, PUGD_BED_PED_VAL, PUGD_EDCESS_PED_PERC, PUGD_EDCESS_PED_VAL, " +
                                    "PUGD_SEC_EDCESS_PED_PERC,PUGD_SEC_EDCESS_PED_VAL, PUGD_AGNST_ST_FORM_TYPE, PUGD_TAX_PERC, PUGD_TAX_VAL, " +
                                    "PUGD_ISS_AMT, PUGD_AGNST_DOC_REQ_TYPE, PUGD_AGNST_DOC_REQ_NUMBER, PUGD_AGNST_DOC_REQ_SL_NO)" +
                                    " VALUES ('" + CommonData.CompanyCode + "', '" + CommonData.StateCode + "', '" + CommonData.BranchCode + "', '" + CommonData.FinancialYear +
                                    "', '" + cbTransactionType.Text + "', '" + txtTransactionNo.Text +
                                    "', " + islno +
                                    ", '" + gvReqDetails.Rows[i].Cells["BatchNo"].Value +
                                    "', '" + gvReqDetails.Rows[i].Cells["ProductId"].Value + "', " + gvReqDetails.Rows[i].Cells["IssQty"].Value +
                                    ", " + gvReqDetails.Rows[i].Cells["IssDmgQty"].Value +
                                    ", " + gvReqDetails.Rows[i].Cells["Rate"].Value +
                                    ", " + gvReqDetails.Rows[i].Cells["BedPer"].Value +
                                    ", " + gvReqDetails.Rows[i].Cells["BedVal"].Value +
                                    ", " + gvReqDetails.Rows[i].Cells["EDCessPer"].Value +
                                    ", " + gvReqDetails.Rows[i].Cells["EDCessVal"].Value +
                                    ", " + gvReqDetails.Rows[i].Cells["SecEDCessPer"].Value +
                                    ", " + gvReqDetails.Rows[i].Cells["SecEDCessVal"].Value +
                                    ", '" + Convert.ToString(gvReqDetails.Rows[i].Cells["STaxType"].Value) +
                                    "', " + gvReqDetails.Rows[i].Cells["TaxPer"].Value +
                                    ", " + gvReqDetails.Rows[i].Cells["TaxVal"].Value +
                                    ", " + gvReqDetails.Rows[i].Cells["IssAmt"].Value +
                                    ",'', " + Convert.ToString(gvReqDetails.Rows[i].Cells["ReqNo"].Value) + ", " + Convert.ToString(gvReqDetails.Rows[i].Cells["ReqSlno"].Value) + "); ");

                    }

                }
                intSave = 0;
                if (sbSQL.ToString().Length > 10)
                    intSave = objSQLDB.ExecuteSaveData(sbSQL.ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLDB = null;
            }
            return intSave;


        }
        private int SaveDCPMDetlData()
        {
            int intSave = 0;
            string strSQL = string.Empty;
            StringBuilder sbSQL = new StringBuilder();
            objSQLDB = new SQLDB();
            if (gvPackingDetl.Rows.Count > 0)
            {
                try
                {
                    strSQL = "DELETE from PU_GRN_PM_DETL" +
                                    " WHERE PUGPD_COMPANY_CODE='" + CommonData.CompanyCode +
                                        "' AND PUGPD_BRANCH_CODE='" + CommonData.BranchCode +
                                        "' AND PUGPD_TRN_NUMBER='" + txtTransactionNo.Text +
                                        "' AND PUGPD_FIN_YEAR='" + CommonData.FinancialYear + "'";

                    intSave = objSQLDB.ExecuteSaveData(strSQL);
                    int iSlno = 0;
                    for (int i = 0; i < gvPackingDetl.Rows.Count; i++)
                    {

                        if (gvPackingDetl.Rows[i].Cells["IssDmgQty1"].Value.ToString().Trim() == "")
                            gvPackingDetl.Rows[i].Cells["IssDmgQty1"].Value = "0";
                        if (gvPackingDetl.Rows[i].Cells["IssQty1"].Value.ToString().Trim() == "")
                            gvPackingDetl.Rows[i].Cells["IssQty1"].Value = "0";
                        try { Convert.ToDouble(gvPackingDetl.Rows[i].Cells["IssDmgQty1"].Value.ToString()); }
                        catch { gvPackingDetl.Rows[i].Cells["IssDmgQty1"].Value = "0"; }
                        try { Convert.ToDouble(gvPackingDetl.Rows[i].Cells["IssQty1"].Value.ToString()); }
                        catch { gvPackingDetl.Rows[i].Cells["IssQty1"].Value = "0"; }
                        try { Convert.ToDouble(gvPackingDetl.Rows[i].Cells["Rate1"].Value.ToString()); }
                        catch { gvPackingDetl.Rows[i].Cells["Rate1"].Value = "0"; }
                        try { Convert.ToDouble(gvPackingDetl.Rows[i].Cells["BedPer1"].Value.ToString()); }
                        catch { gvPackingDetl.Rows[i].Cells["BedPer1"].Value = "0"; }
                        try { Convert.ToDouble(gvPackingDetl.Rows[i].Cells["BedVal1"].Value.ToString()); }
                        catch { gvPackingDetl.Rows[i].Cells["BedVal1"].Value = "0"; }
                        try { Convert.ToDouble(gvPackingDetl.Rows[i].Cells["EDCessPer1"].Value.ToString()); }
                        catch { gvPackingDetl.Rows[i].Cells["EDCessPer1"].Value = "0"; }
                        try { Convert.ToDouble(gvPackingDetl.Rows[i].Cells["EDCessVal1"].Value.ToString()); }
                        catch { gvPackingDetl.Rows[i].Cells["EDCessVal1"].Value = "0"; }
                        try { Convert.ToDouble(gvPackingDetl.Rows[i].Cells["SecEDCessPer1"].Value.ToString()); }
                        catch { gvPackingDetl.Rows[i].Cells["SecEDCessPer1"].Value = "0"; }
                        try { Convert.ToDouble(gvPackingDetl.Rows[i].Cells["SecEDCessVal1"].Value.ToString()); }
                        catch { gvPackingDetl.Rows[i].Cells["SecEDCessVal1"].Value = "0"; }
                        try { Convert.ToDouble(gvPackingDetl.Rows[i].Cells["TaxPer1"].Value.ToString()); }
                        catch { gvPackingDetl.Rows[i].Cells["TaxPer1"].Value = "0"; }
                        try { Convert.ToDouble(gvPackingDetl.Rows[i].Cells["TaxVal1"].Value.ToString()); }
                        catch { gvPackingDetl.Rows[i].Cells["TaxVal1"].Value = "0"; }
                        try { Convert.ToDouble(gvPackingDetl.Rows[i].Cells["IssAmt1"].Value.ToString()); }
                        catch { gvPackingDetl.Rows[i].Cells["IssAmt1"].Value = "0"; }
                        try { Convert.ToDouble(gvPackingDetl.Rows[i].Cells["ReqNo1"].Value.ToString()); }
                        catch { gvPackingDetl.Rows[i].Cells["ReqNo1"].Value = "0"; }
                        try { Convert.ToDouble(gvPackingDetl.Rows[i].Cells["ReqSlno1"].Value.ToString()); }
                        catch { gvPackingDetl.Rows[i].Cells["ReqSlno1"].Value = "0"; }

                        double iTotalQty = 0;
                        iTotalQty = Convert.ToDouble(gvPackingDetl.Rows[i].Cells["IssQty1"].Value) + Convert.ToDouble(gvPackingDetl.Rows[i].Cells["IssDmgQty1"].Value);
                        if (iTotalQty != 0 && gvPackingDetl.Rows[i].Cells["Amount1"].Value.ToString() != "") //&& gvReqDetails.Rows[i].Cells["Amount"].Value.ToString() != "0.00")
                        {
                            iSlno++;
                            sbSQL.Append("INSERT INTO PU_GRN_PM_DETL (PUGPD_COMPANY_CODE, PUGPD_STATE_CODE, PUGPD_BRANCH_CODE, PUGPD_FIN_YEAR" +
                                        ", PUGPD_TRN_TYPE, PUGPD_TRN_NUMBER, PUGPD_SL_NO, PUGPD_BATCH_NO, PUGPD_PRODUCT_ID, PUGPD_GOOD_QTY" +
                                        ", PUGPD_BAD_QTY, PUGPD_ISS_RATE, " +
                                        "PUGPD_BED_PED_PERC, PUGPD_BED_PED_VAL, PUGPD_EDCESS_PED_PERC, PUGPD_EDCESS_PED_VAL, " +
                                        "PUGPD_SEC_EDCESS_PED_PERC, PUGPD_SEC_EDCESS_PED_VAL, PUGPD_AGNST_ST_FORM_TYPE, PUGPD_TAX_PERC, PUGPD_TAX_VAL, " +
                                        "PUGPD_ISS_AMT, PUGPD_AGNST_DOC_REQ_TYPE, PUGPD_AGNST_DOC_REQ_NUMBER, PUGPD_AGNST_DOC_REQ_SL_NO)" +
                                        " VALUES ('" + CommonData.CompanyCode + "', '" + CommonData.StateCode + "', '" + CommonData.BranchCode + "', '" + CommonData.FinancialYear +
                                        "', '" + cbTransactionType.Text + "', '" + txtTransactionNo.Text +
                                        "', " + iSlno +
                                        ", '" + gvPackingDetl.Rows[i].Cells["BatchNo1"].Value +
                                        "', '" + gvPackingDetl.Rows[i].Cells["ProductId1"].Value + "', " + gvPackingDetl.Rows[i].Cells["IssQty1"].Value +
                                        ", " + gvPackingDetl.Rows[i].Cells["IssDmgQty1"].Value +
                                        ", " + gvPackingDetl.Rows[i].Cells["Rate1"].Value +
                                        ", " + gvPackingDetl.Rows[i].Cells["BedPer1"].Value +
                                        ", " + gvPackingDetl.Rows[i].Cells["BedVal1"].Value +
                                        ", " + gvPackingDetl.Rows[i].Cells["EDCessPer1"].Value +
                                        ", " + gvPackingDetl.Rows[i].Cells["EDCessVal1"].Value +
                                        ", " + gvPackingDetl.Rows[i].Cells["SecEDCessPer1"].Value +
                                        ", " + gvPackingDetl.Rows[i].Cells["SecEDCessVal1"].Value +
                                        ", '" + Convert.ToString(gvPackingDetl.Rows[i].Cells["STaxType1"].Value) +
                                        "', " + gvPackingDetl.Rows[i].Cells["TaxPer1"].Value +
                                        ", " + gvPackingDetl.Rows[i].Cells["TaxVal1"].Value +
                                        ", " + gvPackingDetl.Rows[i].Cells["IssAmt1"].Value +
                                        ",'', " + Convert.ToString(gvPackingDetl.Rows[i].Cells["ReqNo1"].Value) +
                                        ", " + Convert.ToString(gvPackingDetl.Rows[i].Cells["ReqSlno1"].Value) + "); ");

                        }

                    }
                    intSave = 0;
                    if (sbSQL.ToString().Length > 10)
                        intSave = objSQLDB.ExecuteSaveData(sbSQL.ToString());

                }
                catch (Exception ex)
                {
                    //throw new Exception(ex.ToString());
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSQLDB = null;
                }
            }
            else
            {
                intSave = 1;
            }
            return intSave;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTransactionNo.Text.Length > 0 && IsModify == true)
                {
                    Security objSecur = new Security();
                    if (objSecur.CanModifyDataUserAsPerBackDays(Convert.ToDateTime(dtpTranDate.Value)) == false)
                    {
                        MessageBox.Show("You cannot manipulate backdays data!\n If you want to modify, Contact to IT-Department", "GRN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTransactionNo.Focus();
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Do you want to Delete " + txtTransactionNo.Text + " Transaction ?",
                                               "CRM GRN", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            objSQLDB = new SQLDB();

                            string strDelete = " DELETE from PU_GRN_DETL " +
                                                " WHERE PUGD_COMPANY_CODE='" + CommonData.CompanyCode +
                                                "' AND PUGD_BRANCH_CODE='" + CommonData.BranchCode +
                                                "' AND PUGD_TRN_NUMBER='" + txtTransactionNo.Text +
                                                "'  AND PUGD_FIN_YEAR='" + CommonData.FinancialYear + "';";

                            strDelete += " DELETE from PU_GRN_PM_DETL" +
                                               " WHERE PUGPD_COMPANY_CODE='" + CommonData.CompanyCode +
                                               "' AND PUGPD_BRANCH_CODE='" + CommonData.BranchCode +
                                               "' AND PUGPD_TRN_NUMBER='" + txtTransactionNo.Text +
                                               "'  AND PUGPD_FIN_YEAR='" + CommonData.FinancialYear + "'; ";


                            strDelete += " DELETE from PU_GRN_HEAD" +
                                                " WHERE PUGH_COMPANY_CODE='" + CommonData.CompanyCode +
                                                "' AND PUGH_BRANCH_CODE='" + CommonData.BranchCode +
                                                "' AND PUGH_TRN_NUMBER='" + txtTransactionNo.Text +
                                                "'  AND PUGH_FIN_YEAR='" + CommonData.FinancialYear + "'; ";

                           

                            int intRec = objSQLDB.ExecuteSaveData(strDelete);
                            if (intRec > 0)
                            {
                                IsModify = false;
                                MessageBox.Show("Tran No: " + txtTransactionNo.Text + " Deleted!");
                                ClearForm(this);
                                gvReqDetails.Rows.Clear();
                                gvPackingDetl.Rows.Clear();
                                txtTransactionNo.Text = NewTransactionNumber().ToString();
                            }

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Enter Tran Number.", "GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtTransactionNo.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objSQLDB = null;
            }
        }

        private void txtTransactionNo_TextChanged(object sender, EventArgs e)
        {

            //try
            //{
            //    if (Convert.ToString(txtTransactionNo.Text).Length > 15)
            //    {

            //        FillTransactionData(txtTransactionNo.Text.ToString());

            //    }
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message);
            //}
        }
        private void FillTransactionData(string TranNO)
        {
            objPUDB = new ProductUnitDB();
            Hashtable ht = new Hashtable();
            try
            {

                ht = objPUDB.GetGoodsReceiptNoteData("", TranNO);
                DataTable dtH = (DataTable)ht["Head"];
                DataTable dtD = (DataTable)ht["Detail"];
                DataTable dtP = (DataTable)ht["PMDetl"];
                FillTransactionHead(dtH, dtD, dtP);
                dtH = null;
                dtD = null;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                objPUDB = null;
                ht = null;

            }
        }

        private bool FillTransactionHead(DataTable dtHead, DataTable dtDetl, DataTable dtPMDetl)
        {
            bool isData = false;
            objGeneral = new General();
            try
            {
                if (dtHead.Rows.Count > 0)
                {
                    IsModify = true;
                    txtDocMonth.Text = dtHead.Rows[0]["DocMonth"].ToString();
                    strTransactionNo = dtHead.Rows[0]["TrnNumber"].ToString();
                    txtRefNo.Text = dtHead.Rows[0]["pugh_ref_no"].ToString();
                    txtTransactionNo.Text = dtHead.Rows[0]["TrnNumber"] + "";
                    dtpTranDate.Value = Convert.ToDateTime(dtHead.Rows[0]["TrnDate"] + "");
                    txtTotalFreight.Text = dtHead.Rows[0]["TotalFreight"] + "";
                    txtAdvancePaid.Text = dtHead.Rows[0]["AdvancePaid"] + "";
                    txtToPay.Text = dtHead.Rows[0]["ToPayFreight"] + "";
                    txtLoadingCharges.Text = dtHead.Rows[0]["LoadingCharges"] + "";
                    txtWayBillNo.Text = dtHead.Rows[0]["WayBillNo"] + "";
                    cbTransactionType.Text = dtHead.Rows[0]["TrnType"] + "";
                    strToBranchCode = dtHead.Rows[0]["ToBranchCode"] + "";
                    cmbDCType.SelectedText = dtHead.Rows[0]["DcType"] + "";
                    txtDCNo.Text = dtHead.Rows[0]["DcNo"] + "";
                    cbBranches.SelectedIndex = objGeneral.GetComboBoxSelectedIndex(strToBranchCode, cbBranches);
                     strECode = dtHead.Rows[0]["ToEcode"] + "";
                     txtEcodeSearch.Text = strECode;
                     txtEmpName.Text = dtHead.Rows[0]["EName"] + "";
                    cbVehicleType.SelectedValue = dtHead.Rows[0]["VehicleSource"] + "";

                    if (dtHead.Rows[0]["Status"].ToString().Equals("CANCELLED"))
                    {
                        chkCancelSPGRN.Checked = true;
                    }
                    else
                    {
                        chkCancelSPGRN.Checked = false;
                    }

                    txtRemarks.Text = dtHead.Rows[0]["Remarks"].ToString();


                    if (cbVehicleType.Text == "OWN")
                    {
                        cbDriverEcode.Visible = true;
                        cbDriverEcode.Text = dtHead.Rows[0]["DriverName"] + "";
                    }
                    else
                    {
                        cbDriverEcode.Visible = false;
                        txtDriverNo.Text = dtHead.Rows[0]["DriverName"] + "";
                    }

                    txtTripLRNo.Text = Convert.ToString(dtHead.Rows[0]["TripLRNumber"]);
                    txtVehicleNo.Text = Convert.ToString(dtHead.Rows[0]["VehicleNumber"]);

                    txtTransporter.Text = dtHead.Rows[0]["TransporterName"] + "";

                    if (chkCancelSPGRN.Checked == false)
                    {
                        FillTransactionDetl(dtDetl);
                        FillTransactionPMDetl(dtPMDetl);
                    }

                    isData = true;
                }
                else
                {
                    txtTransactionNo.Text = NewTransactionNumber();
                    txtDocMonth.Text = CommonData.DocMonth;
                    btnSave.Enabled = true;
                    btnDelete.Enabled = true;
                    IsModify = false;
                    cmbDCType.SelectedText = "DC";
                    txtDCNo.Text = "";
                    gvReqDetails.Rows.Clear();
                    chkCancelSPGRN.Checked = false;
                    txtRemarks.Text = "";
                    txtDocMonth.Text = CommonData.DocMonth.ToUpper();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dtHead = null;
                objGeneral = null;
            }
            return isData;
        }

        private void FillTransactionDetl(DataTable dt)
        {
            objPUDB = new ProductUnitDB();
            try
            {
                gvReqDetails.Rows.Clear();
                cbRequests.SelectedIndex = -1;
                if (dt.Rows.Count > 0)
                {
                    for (int intRow = 0; intRow < dt.Rows.Count; intRow++)
                    {

                        gvReqDetails.Rows.Add();
                        gvReqDetails.Rows[intRow].Cells["SLNO"].Value = dt.Rows[intRow]["SlNo"].ToString();
                        gvReqDetails.Rows[intRow].Cells["Category"].Value = dt.Rows[intRow]["category_name"].ToString();
                        gvReqDetails.Rows[intRow].Cells["Product"].Value = dt.Rows[intRow]["product_name"].ToString();
                        gvReqDetails.Rows[intRow].Cells["BatchNo"].Value = dt.Rows[intRow]["BatchNo"].ToString();
                        gvReqDetails.Rows[intRow].Cells["ProductId"].Value = dt.Rows[intRow]["ProductId"].ToString();
                        gvReqDetails.Rows[intRow].Cells["ReqQty"].Value = dt.Rows[intRow]["IndQty"].ToString();
                        gvReqDetails.Rows[intRow].Cells["IssQty"].Value = dt.Rows[intRow]["IssQty"].ToString();
                        gvReqDetails.Rows[intRow].Cells["IssDmgQty"].Value = dt.Rows[intRow]["IssDmgQty"].ToString();
                        gvReqDetails.Rows[intRow].Cells["RATE"].Value = dt.Rows[intRow]["IssRate"].ToString();
                        gvReqDetails.Rows[intRow].Cells["Amount"].Value = Convert.ToDouble(Convert.ToDouble(dt.Rows[intRow]["IssQty"].ToString()) * Convert.ToDouble(dt.Rows[intRow]["IssRate"].ToString())).ToString("f");
                        gvReqDetails.Rows[intRow].Cells["BedPer"].Value = dt.Rows[intRow]["BedPer"].ToString();
                        gvReqDetails.Rows[intRow].Cells["BedVal"].Value = dt.Rows[intRow]["BedVal"].ToString();
                        gvReqDetails.Rows[intRow].Cells["EDCessPer"].Value = dt.Rows[intRow]["EDCessPer"].ToString();
                        gvReqDetails.Rows[intRow].Cells["EDCessVal"].Value = dt.Rows[intRow]["EDCessVal"].ToString();
                        gvReqDetails.Rows[intRow].Cells["SecEDCessPer"].Value = dt.Rows[intRow]["SecEDCessPer"].ToString();
                        gvReqDetails.Rows[intRow].Cells["SecEDCessVal"].Value = dt.Rows[intRow]["SecEDCessVal"].ToString();
                        gvReqDetails.Rows[intRow].Cells["STaxType"].Value = dt.Rows[intRow]["STaxType"].ToString();
                        gvReqDetails.Rows[intRow].Cells["TaxPer"].Value = dt.Rows[intRow]["TaxPer"].ToString();
                        gvReqDetails.Rows[intRow].Cells["TaxVal"].Value = dt.Rows[intRow]["TaxVal"].ToString();
                        gvReqDetails.Rows[intRow].Cells["IssAmt"].Value = dt.Rows[intRow]["IssAmt"].ToString();
                        gvReqDetails.Rows[intRow].Cells["ReqType"].Value = dt.Rows[intRow]["IndentType"].ToString();
                        gvReqDetails.Rows[intRow].Cells["ReqNo"].Value = dt.Rows[intRow]["IndentNo"].ToString();
                        gvReqDetails.Rows[intRow].Cells["ReqSLNo"].Value = dt.Rows[intRow]["IndentSLNo"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objPUDB = null;
            }
        }


        private void FillTransactionPMDetl(DataTable dt)
        {
            objPUDB = new ProductUnitDB();
            try
            {
                gvPackingDetl.Rows.Clear();
                //cbRequests.SelectedIndex = -1;
                if (dt.Rows.Count > 0)
                {
                    for (int intRow = 0; intRow < dt.Rows.Count; intRow++)
                    {

                        gvPackingDetl.Rows.Add();
                        gvPackingDetl.Rows[intRow].Cells["SLNO1"].Value = dt.Rows[intRow]["SlNo"].ToString();
                        gvPackingDetl.Rows[intRow].Cells["Category1"].Value = dt.Rows[intRow]["category_name"].ToString();
                        gvPackingDetl.Rows[intRow].Cells["Product1"].Value = dt.Rows[intRow]["product_name"].ToString();
                        gvPackingDetl.Rows[intRow].Cells["BatchNo1"].Value = dt.Rows[intRow]["BatchNo"].ToString();
                        gvPackingDetl.Rows[intRow].Cells["ProductId1"].Value = dt.Rows[intRow]["ProductId"].ToString();
                        gvPackingDetl.Rows[intRow].Cells["ReqQty1"].Value = dt.Rows[intRow]["IndQty"].ToString();
                        gvPackingDetl.Rows[intRow].Cells["IssQty1"].Value = dt.Rows[intRow]["IssQty"].ToString();
                        gvPackingDetl.Rows[intRow].Cells["IssDmgQty1"].Value = dt.Rows[intRow]["IssDmgQty"].ToString();
                        gvPackingDetl.Rows[intRow].Cells["RATE1"].Value = dt.Rows[intRow]["IssRate"].ToString();
                        gvPackingDetl.Rows[intRow].Cells["Amount1"].Value = Convert.ToDouble(Convert.ToDouble(dt.Rows[intRow]["IssQty"].ToString()) * Convert.ToDouble(dt.Rows[intRow]["IssRate"].ToString())).ToString("f");
                        gvPackingDetl.Rows[intRow].Cells["BedPer1"].Value = dt.Rows[intRow]["BedPer"].ToString();
                        gvPackingDetl.Rows[intRow].Cells["BedVal1"].Value = dt.Rows[intRow]["BedVal"].ToString();
                        gvPackingDetl.Rows[intRow].Cells["EDCessPer1"].Value = dt.Rows[intRow]["EDCessPer"].ToString();
                        gvPackingDetl.Rows[intRow].Cells["EDCessVal1"].Value = dt.Rows[intRow]["EDCessVal"].ToString();
                        gvPackingDetl.Rows[intRow].Cells["SecEDCessPer1"].Value = dt.Rows[intRow]["SecEDCessPer"].ToString();
                        gvPackingDetl.Rows[intRow].Cells["SecEDCessVal1"].Value = dt.Rows[intRow]["SecEDCessVal"].ToString();
                        gvPackingDetl.Rows[intRow].Cells["STaxType1"].Value = dt.Rows[intRow]["STaxType"].ToString();
                        gvPackingDetl.Rows[intRow].Cells["TaxPer1"].Value = dt.Rows[intRow]["TaxPer"].ToString();
                        gvPackingDetl.Rows[intRow].Cells["TaxVal1"].Value = dt.Rows[intRow]["TaxVal"].ToString();
                        gvPackingDetl.Rows[intRow].Cells["IssAmt1"].Value = dt.Rows[intRow]["IssAmt"].ToString();
                        gvPackingDetl.Rows[intRow].Cells["ReqType1"].Value = dt.Rows[intRow]["IndentType"].ToString();
                        gvPackingDetl.Rows[intRow].Cells["ReqNo1"].Value = dt.Rows[intRow]["IndentNo"].ToString();
                        gvPackingDetl.Rows[intRow].Cells["ReqSLNo1"].Value = dt.Rows[intRow]["IndentSLNo"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objPUDB = null;
            }
        }
        private void btnAddPMeterialItems_Click(object sender, EventArgs e)
        {
            ProductSearchAll PSearch = new ProductSearchAll("GRN_PM");
            PSearch.objGoodsReceiptNotePU = this;
            PSearch.ShowDialog();
        }

        private void btnClearItems_Click(object sender, EventArgs e)
        {
            gvPackingDetl.Rows.Clear();
        }

        private void gvPackingDetl_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            blIsCellQty = true;
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

        }


        private void gvPackingDetl_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 6 && e.ColumnIndex < 18)
            {
                if (Convert.ToString(gvPackingDetl.Rows[e.RowIndex].Cells["IssQty1"].Value) != "")
                {
                    if (Convert.ToDouble(gvPackingDetl.Rows[e.RowIndex].Cells["IssQty1"].Value) >= 0 && Convert.ToDouble(gvPackingDetl.Rows[e.RowIndex].Cells["Rate1"].Value) >= 0)
                    {
                        gvPackingDetl.Rows[e.RowIndex].Cells["Amount1"].Value = Convert.ToDouble(gvPackingDetl.Rows[e.RowIndex].Cells["IssQty1"].Value) * (Convert.ToDouble(gvPackingDetl.Rows[e.RowIndex].Cells["Rate1"].Value));
                        gvPackingDetl.Rows[e.RowIndex].Cells["Amount1"].Value = Convert.ToDouble(gvPackingDetl.Rows[e.RowIndex].Cells["Amount1"].Value).ToString("f");
                        double dAmt = Convert.ToDouble(gvPackingDetl.Rows[e.RowIndex].Cells["Amount1"].Value.ToString());
                        double dBper = 0;
                        double dECP = 0;
                        double dSECP = 0;
                        double dTaxP = 0;
                        if (Convert.ToString(gvPackingDetl.Rows[e.RowIndex].Cells["BedPer1"].Value) != "")
                            dBper = Convert.ToDouble(gvPackingDetl.Rows[e.RowIndex].Cells["BedPer1"].Value.ToString());
                        if (Convert.ToString(gvPackingDetl.Rows[e.RowIndex].Cells["EDCessPer1"].Value) != "")
                            dECP = Convert.ToDouble(gvPackingDetl.Rows[e.RowIndex].Cells["EDCessPer1"].Value.ToString());
                        if (Convert.ToString(gvPackingDetl.Rows[e.RowIndex].Cells["SecEDCessPer1"].Value) != "")
                            dSECP = Convert.ToDouble(gvPackingDetl.Rows[e.RowIndex].Cells["SecEDCessPer1"].Value.ToString());
                        if (Convert.ToString(gvPackingDetl.Rows[e.RowIndex].Cells["TaxPer1"].Value) != "")
                            dTaxP = Convert.ToDouble(gvPackingDetl.Rows[e.RowIndex].Cells["TaxPer1"].Value.ToString());
                        gvPackingDetl.Rows[e.RowIndex].Cells["IssAmt1"].Value = CaliculateIssueAmountForPm(e.RowIndex, dAmt, dBper, dECP, dSECP, dTaxP).ToString("f"); ;
                    }

                }
                else
                {
                    gvPackingDetl.Rows[e.RowIndex].Cells["Rate1"].Value = "0";
                    gvPackingDetl.Rows[e.RowIndex].Cells["Amount1"].Value = "0";
                    gvPackingDetl.Rows[e.RowIndex].Cells["BedPer1"].Value = "0";
                    gvPackingDetl.Rows[e.RowIndex].Cells["EDCessPer1"].Value = "0";
                    gvPackingDetl.Rows[e.RowIndex].Cells["SecEDCessPer1"].Value = "0";
                    gvPackingDetl.Rows[e.RowIndex].Cells["TaxPer1"].Value = "0";
                    gvPackingDetl.Rows[e.RowIndex].Cells["BedVal1"].Value = "0";
                    gvPackingDetl.Rows[e.RowIndex].Cells["EDCessVal1"].Value = "0";
                    gvPackingDetl.Rows[e.RowIndex].Cells["SecEDCessVal1"].Value = "0";
                    gvPackingDetl.Rows[e.RowIndex].Cells["TaxVal1"].Value = "0";
                    gvPackingDetl.Rows[e.RowIndex].Cells["IssAmt1"].Value = "0";
                }
            }
            //try
            //{
            //    DataGridView dgv = (DataGridView)sender;
            //    dgv.EndEdit();
            //    if (e.ColumnIndex == 5)
            //    {
            //        if (Convert.ToString(gvReqDetails.Rows[e.RowIndex].Cells["IssQty"].Value) != "" && Convert.ToString(gvReqDetails.Rows[e.RowIndex].Cells["IndentNo"].Value) != "0")
            //        {
            //            if (Convert.ToDouble(gvReqDetails.Rows[e.RowIndex].Cells["IssQty"].Value) > Convert.ToDouble(gvReqDetails.Rows[e.RowIndex].Cells["IndQty"].Value))
            //            {
            //                MessageBox.Show("Issue quantity should be less than or equal to indent quantity!", "DC", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                e.Cancel = true;
            //            }
            //        }
            //    }
            //}
            //catch
            //{
            //}
        }

        private void gvPackingDetl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.RowIndex >= 0)
                {
                    DataGridView dgv = (DataGridView)sender;
                    if (e.ColumnIndex == 5 || e.ColumnIndex == 6)
                    {
                        DataGridViewCell textBoxCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        if (textBoxCell != null)
                        {
                            gvPackingDetl.CurrentCell = textBoxCell;
                            dgv.BeginEdit(true);
                        }
                    }
                }
                if (e.ColumnIndex == gvPackingDetl.Rows[e.RowIndex].Cells["Delete1"].ColumnIndex)
                {
                    DataGridViewRow tempRow = new DataGridViewRow();
                    tempRow = gvPackingDetl.Rows[e.RowIndex];
                    gvPackingDetl.Rows.Remove(tempRow);
                    for (int i = 0; i < gvPackingDetl.Rows.Count; i++)
                    {
                        gvPackingDetl.Rows[i].Cells["SLNO1"].Value = i + 1;
                    }
                }
            }
        }

        private void txtRefNo_Validated(object sender, EventArgs e)
        {
            if (txtRefNo.Text.Trim().Length > 0)
            {
                FillGRNDetails(txtRefNo.Text.Replace(" ","").ToString());
            }
        }
        private void FillGRNDetails(string sRefNo)
        {
            objSQLDB = new SQLDB();
            DataSet sDCInfo = new DataSet();
            string sDCType = "GRN";
           
            try
            {
                sDCInfo = objSQLDB.ExecuteDataSet("SELECT PUGH_TRN_NUMBER FROM PU_GRN_HEAD WHERE PUGH_BRANCH_CODE='" + CommonData.BranchCode +
                    "' AND PUGH_REF_NO='" + sRefNo + "' AND pugh_fin_year='" + CommonData.FinancialYear + "' AND PUGH_TRN_NUMBER LIKE '%" + sDCType + "%'");
                DataTable sDCHead = sDCInfo.Tables[0];
                if (sDCHead.Rows.Count > 0)
                {
                    txtTransactionNo.Text = sDCHead.Rows[0]["PUGH_TRN_NUMBER"] + "";
                    txtTransactionNo_KeyUp(null, null);

                }
                else
                {
                    txtTransactionNo.Text = NewTransactionNumber();
                }
                //CalculateTotals();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void txtRefNo_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtRefNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                //if (!char.IsDigit((e.KeyChar)))
                //{
                //    e.Handled = true;
                //}
            }
        }

        private void txtEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            objSQLDB = new SQLDB();
            if (txtEcodeSearch.Text.Length > 4)
            {
                DataTable dt = objSQLDB.ExecuteDataSet("SELECT MEMBER_NAME+' ('+DESIG+')' AS DATA FROM EORA_MASTER WHERE ECODE=" + txtEcodeSearch.Text).Tables[0];
                if (dt.Rows.Count > 0)
                    txtEmpName.Text = dt.Rows[0][0].ToString();
                else
                    txtEmpName.Text = "";
            }
            else
                txtEmpName.Text = "";
            objSQLDB = null;
        }

        private void txtTransactionNo_Enter(object sender, EventArgs e)
        {
            this.txtTransactionNo.Select(txtTransactionNo.Text.Length, 0);
        }

        private void txtTransactionNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 37 || e.KeyValue == 39)
                e.Handled = false;
            else if (txtTransactionNo.SelectionStart < 14)
                e.Handled = true;
        }

        private void txtTransactionNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetterOrDigit(e.KeyChar) == false)
                e.Handled = true;

            if (e.KeyChar == 8 || e.KeyChar == 32)
                e.Handled = false;

            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtVehicleNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetterOrDigit(e.KeyChar) == false)
                e.Handled = true;

            if (e.KeyChar == 8 || e.KeyChar == 32)
                e.Handled = false;

            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtTripLRNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetterOrDigit(e.KeyChar) == false)
                e.Handled = true;

            if (e.KeyChar == 8 || e.KeyChar == 32)
                e.Handled = false;

            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtAdvancePaid_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtTotalFreight.Text.ToString().Length > 0 && txtAdvancePaid.Text.ToString().Length > 0)
            {
                if (Convert.ToDouble(txtTotalFreight.Text.ToString()) - Convert.ToDouble(txtAdvancePaid.Text.ToString()) < 0)
                    e.Handled = false;
                else
                    txtToPay.Text = Convert.ToString(Convert.ToDouble(txtTotalFreight.Text.ToString()) - Convert.ToDouble(txtAdvancePaid.Text.ToString()));
            }
            else if (txtTotalFreight.Text.ToString().Length > 0 && txtAdvancePaid.Text.ToString().Length == 0)
            {
                txtToPay.Text = txtTotalFreight.Text.ToString();
            }
        }

        private void txtTotalFreight_KeyUp(object sender, KeyEventArgs e)
        {
            txtToPay.Text = txtTotalFreight.Text;
        }

        private void txtToPay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false)
                e.Handled = true;
            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void txtAdvancePaid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false)
                e.Handled = true;
            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void txtTotalFreight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false)
                e.Handled = true;
            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            //BatchProductSearch PSearch = new BatchProductSearch("DCfromPU");
            //PSearch.objDeliveryChallanPU = this;
            //PSearch.ShowDialog();

            ProductSearchAll PSearch = new ProductSearchAll("GRN");
            PSearch.objGoodsReceiptNotePU = this;
            PSearch.ShowDialog();
        }

        private void cbVehicleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbVehicleType.Text == "OWN")
                cbDriverEcode.Visible = true;
            else
                cbDriverEcode.Visible = false;
        }

        private void btnClearProd_Click(object sender, EventArgs e)
        {
            gvReqDetails.Rows.Clear();
        }

        private void gvReqDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 6 && e.ColumnIndex < 18)
            {
                if (Convert.ToString(gvReqDetails.Rows[e.RowIndex].Cells["IssQty"].Value) != "")
                {
                    if (Convert.ToDouble(gvReqDetails.Rows[e.RowIndex].Cells["IssQty"].Value) >= 0 && Convert.ToDouble(gvReqDetails.Rows[e.RowIndex].Cells["Rate"].Value) >= 0)
                    {

                        try { Convert.ToDouble(gvReqDetails.Rows[e.RowIndex].Cells["IssDmgQty"].Value.ToString()); }
                        catch { gvReqDetails.Rows[e.RowIndex].Cells["IssDmgQty"].Value = "0"; }
                        try { Convert.ToDouble(gvReqDetails.Rows[e.RowIndex].Cells["IssQty"].Value.ToString()); }
                        catch { gvReqDetails.Rows[e.RowIndex].Cells["IssQty"].Value = "0"; }                            

                        double iTotalQty = 0;
                        iTotalQty = Convert.ToDouble(gvReqDetails.Rows[e.RowIndex].Cells["IssQty"].Value) + Convert.ToDouble(gvReqDetails.Rows[e.RowIndex].Cells["IssDmgQty"].Value);
                        gvReqDetails.Rows[e.RowIndex].Cells["TotQty"].Value = iTotalQty.ToString("f");

                        gvReqDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvReqDetails.Rows[e.RowIndex].Cells["IssQty"].Value) * (Convert.ToDouble(gvReqDetails.Rows[e.RowIndex].Cells["Rate"].Value));
                        gvReqDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvReqDetails.Rows[e.RowIndex].Cells["Amount"].Value).ToString("f");
                        double dAmt = Convert.ToDouble(gvReqDetails.Rows[e.RowIndex].Cells["Amount"].Value.ToString());
                        double dBper = 0;
                        double dECP = 0;
                        double dSECP = 0;
                        double dTaxP = 0;
                        if (Convert.ToString(gvReqDetails.Rows[e.RowIndex].Cells["BedPer"].Value) != "")
                            dBper = Convert.ToDouble(gvReqDetails.Rows[e.RowIndex].Cells["BedPer"].Value.ToString());
                        if (Convert.ToString(gvReqDetails.Rows[e.RowIndex].Cells["EDCessPer"].Value) != "")
                            dECP = Convert.ToDouble(gvReqDetails.Rows[e.RowIndex].Cells["EDCessPer"].Value.ToString());
                        if (Convert.ToString(gvReqDetails.Rows[e.RowIndex].Cells["SecEDCessPer"].Value) != "")
                            dSECP = Convert.ToDouble(gvReqDetails.Rows[e.RowIndex].Cells["SecEDCessPer"].Value.ToString());
                        if (Convert.ToString(gvReqDetails.Rows[e.RowIndex].Cells["TaxPer"].Value) != "")
                            dTaxP = Convert.ToDouble(gvReqDetails.Rows[e.RowIndex].Cells["TaxPer"].Value.ToString());
                        gvReqDetails.Rows[e.RowIndex].Cells["IssAmt"].Value = CaliculateIssueAmount(e.RowIndex, dAmt, dBper, dECP, dSECP, dTaxP).ToString("f"); ;
                    }

                }
                else
                {
                    gvReqDetails.Rows[e.RowIndex].Cells["Amount"].Value = string.Empty;
                    gvReqDetails.Rows[e.RowIndex].Cells["BedPer"].Value = "";
                    gvReqDetails.Rows[e.RowIndex].Cells["EDCessPer"].Value = "";
                    gvReqDetails.Rows[e.RowIndex].Cells["SecEDCessPer"].Value = "";
                    gvReqDetails.Rows[e.RowIndex].Cells["TaxPer"].Value = "";
                    gvReqDetails.Rows[e.RowIndex].Cells["BedVal"].Value = "";
                    gvReqDetails.Rows[e.RowIndex].Cells["EDCessVal"].Value = "";
                    gvReqDetails.Rows[e.RowIndex].Cells["SecEDCessVal"].Value = "";
                    gvReqDetails.Rows[e.RowIndex].Cells["TaxVal"].Value = "";
                    gvReqDetails.Rows[e.RowIndex].Cells["IssAmt"].Value = "";
                }
            }
            //try
            //{
            //    DataGridView dgv = (DataGridView)sender;
            //    dgv.EndEdit();
            //    if (e.ColumnIndex == 5)
            //    {
            //        if (Convert.ToString(gvReqDetails.Rows[e.RowIndex].Cells["IssQty"].Value) != "" && Convert.ToString(gvReqDetails.Rows[e.RowIndex].Cells["IndentNo"].Value) != "0")
            //        {
            //            if (Convert.ToDouble(gvReqDetails.Rows[e.RowIndex].Cells["IssQty"].Value) > Convert.ToDouble(gvReqDetails.Rows[e.RowIndex].Cells["IndQty"].Value))
            //            {
            //                MessageBox.Show("Issue quantity should be less than or equal to indent quantity!", "DC", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                e.Cancel = true;
            //            }
            //        }
            //    }
            //}
            //catch
            //{
            //}
            CalculateGrandTotals();
        }

        private void CalculateGrandTotals()
        {
            double dTotQty = 0, dTotAmt = 0, dTotGood = 0, dTotDmg = 0, dTotCrts = 0;
            for (int i = 0; i < gvReqDetails.Rows.Count; i++)
            {
                try { Convert.ToDouble(gvReqDetails.Rows[i].Cells["IssDmgQty"].Value.ToString()); }
                catch { gvReqDetails.Rows[i].Cells["IssDmgQty"].Value = "0"; }
                try { Convert.ToDouble(gvReqDetails.Rows[i].Cells["IssQty"].Value.ToString()); }
                catch { gvReqDetails.Rows[i].Cells["IssQty"].Value = "0"; }
                try { Convert.ToDouble(gvReqDetails.Rows[i].Cells["Amount"].Value.ToString()); }
                catch { gvReqDetails.Rows[i].Cells["Amount"].Value = "0"; }
                if (gvReqDetails.Rows[i].Cells["Category"].Value.ToString() != "PACKING MATERIAL")
                {
                    dTotGood += Convert.ToDouble(gvReqDetails.Rows[i].Cells["IssQty"].Value.ToString());
                    dTotDmg += Convert.ToDouble(gvReqDetails.Rows[i].Cells["IssDmgQty"].Value.ToString());
                    dTotAmt += Convert.ToDouble(gvReqDetails.Rows[i].Cells["Amount"].Value.ToString());
                }
                else
                {
                    dTotCrts += (Convert.ToDouble(gvReqDetails.Rows[i].Cells["IssQty"].Value.ToString()) + 
                                Convert.ToDouble(gvReqDetails.Rows[i].Cells["IssDmgQty"].Value.ToString()));
                }
                
            }
            dTotQty = dTotGood + dTotDmg;
            txtTotQty.Text = dTotQty.ToString("f");
            txtTotAmount.Text = dTotAmt.ToString("f");
            txtTotGood.Text = dTotGood.ToString("f");
            txtTotDamage.Text = dTotDmg.ToString("f");
            txtTotCrates.Text = dTotCrts.ToString("f");
        }

        private double CaliculateIssueAmount(int nRow, double dAmt, double dBEDPer, double EDCessPer, double SecEDCessPer, double TaxPer)
        {
            double dIssAmt = 0.00;
            double dBVal = 0;
            double dECPVal = 0;
            double dSECPVal = 0;
            double dTaxPVal = 0;

            dBVal = Math.Round((dAmt * dBEDPer) / 100, 0);
            dECPVal = Math.Round((dAmt * EDCessPer) / 100, 0);
            dSECPVal = Math.Round((dAmt * SecEDCessPer) / 100, 0);
            dTaxPVal = Math.Round((dAmt * TaxPer) / 100, 0);
            dIssAmt = dAmt + dBVal + dECPVal + dSECPVal + dTaxPVal;

            gvReqDetails.Rows[nRow].Cells["BedVal"].Value = dBVal.ToString("f");
            gvReqDetails.Rows[nRow].Cells["EDCessVal"].Value = dECPVal.ToString("f");
            gvReqDetails.Rows[nRow].Cells["SecEDCessVal"].Value = dSECPVal.ToString("f");
            gvReqDetails.Rows[nRow].Cells["TaxVal"].Value = dTaxPVal.ToString("f");
            if (dBVal == 0)
                gvReqDetails.Rows[nRow].Cells["BedPer"].Value = "0";
            if (dECPVal == 0)
                gvReqDetails.Rows[nRow].Cells["EDCessPer"].Value = "0";
            if (dSECPVal == 0)
                gvReqDetails.Rows[nRow].Cells["SecEDCessPer"].Value = "0";
            if (dTaxPVal == 0)
                gvReqDetails.Rows[nRow].Cells["TaxPer"].Value = "0";
            if (Convert.ToString(gvReqDetails.Rows[nRow].Cells["ReqNo"].Value) == "")
                gvReqDetails.Rows[nRow].Cells["ReqNo"].Value = "0";
            if (Convert.ToString(gvReqDetails.Rows[nRow].Cells["ReqSLNo"].Value) == "")
                gvReqDetails.Rows[nRow].Cells["ReqSLNo"].Value = "0";

            return dIssAmt;
        }

        private double CaliculateIssueAmountForPm(int nRow, double dAmt, double dBEDPer, double EDCessPer, double SecEDCessPer, double TaxPer)
        {
            double dIssAmt = 0.00;
            double dBVal = 0;
            double dECPVal = 0;
            double dSECPVal = 0;
            double dTaxPVal = 0;

            dBVal = Math.Round((dAmt * dBEDPer) / 100, 0);
            dECPVal = Math.Round((dAmt * EDCessPer) / 100, 0);
            dSECPVal = Math.Round((dAmt * SecEDCessPer) / 100, 0);
            dTaxPVal = Math.Round((dAmt * TaxPer) / 100, 0);
            dIssAmt = dAmt + dBVal + dECPVal + dSECPVal + dTaxPVal;

            gvPackingDetl.Rows[nRow].Cells["BedVal1"].Value = dBVal.ToString("f");
            gvPackingDetl.Rows[nRow].Cells["EDCessVal1"].Value = dECPVal.ToString("f");
            gvPackingDetl.Rows[nRow].Cells["SecEDCessVal1"].Value = dSECPVal.ToString("f");
            gvPackingDetl.Rows[nRow].Cells["TaxVal1"].Value = dTaxPVal.ToString("f");
            if (dBVal == 0)
                gvPackingDetl.Rows[nRow].Cells["BedPer1"].Value = "0";
            if (dECPVal == 0)
                gvPackingDetl.Rows[nRow].Cells["EDCessPer1"].Value = "0";
            if (dSECPVal == 0)
                gvPackingDetl.Rows[nRow].Cells["SecEDCessPer1"].Value = "0";
            if (dTaxPVal == 0)
                gvPackingDetl.Rows[nRow].Cells["TaxPer1"].Value = "0";
            if (Convert.ToString(gvPackingDetl.Rows[nRow].Cells["ReqNo1"].Value) == "")
                gvPackingDetl.Rows[nRow].Cells["ReqNo1"].Value = "0";
            if (Convert.ToString(gvPackingDetl.Rows[nRow].Cells["ReqSLNo1"].Value) == "")
                gvPackingDetl.Rows[nRow].Cells["ReqSLNo1"].Value = "0";

            return dIssAmt;
        }

        private double GetTotalAmount()
        {
            double dbInvAmt = 0;
            for (int i = 0; i < gvReqDetails.Rows.Count; i++)
            {
                if (gvReqDetails.Rows[i].Cells["Amount"].Value.ToString() != "")
                {
                    if (Convert.ToDouble(gvReqDetails.Rows[i].Cells["Amount"].Value.ToString()) >= 1)
                        dbInvAmt += Convert.ToDouble(gvReqDetails.Rows[i].Cells["Amount"].Value);

                }

            }
            return dbInvAmt;
        }



        private void txtTransactionNo_Validated(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(txtTransactionNo.Text).Length > 15)
                {

                    FillTransactionData(txtTransactionNo.Text.ToString());

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void gvReqDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.RowIndex >= 0)
                {
                    DataGridView dgv = (DataGridView)sender;
                    if (e.ColumnIndex == 5 || e.ColumnIndex == 6)
                    {
                        DataGridViewCell textBoxCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        if (textBoxCell != null)
                        {
                            gvReqDetails.CurrentCell = textBoxCell;
                            dgv.BeginEdit(true);
                        }
                    }
                }
                if (e.ColumnIndex == gvReqDetails.Rows[e.RowIndex].Cells["Delete"].ColumnIndex)
                {
                    DataGridViewRow tempRow = new DataGridViewRow();
                    tempRow = gvReqDetails.Rows[e.RowIndex];
                    gvReqDetails.Rows.Remove(tempRow);
                    for (int i = 0; i < gvReqDetails.Rows.Count; i++)
                    {
                        gvReqDetails.Rows[i].Cells["SLNO"].Value = i + 1;
                    }
                }
            }
        }

        private void gvReqDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            blIsCellQty = true;
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
        }

        private void txtDriverNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

      
        private void txtLoadingCharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false)
                e.Handled = true;
            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void txtLoadingCharges_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtTransactionNo_KeyUp(object sender, KeyEventArgs e)
        {

            try
            {
                if (Convert.ToString(txtTransactionNo.Text).Length > 15)
                {

                    FillTransactionData(txtTransactionNo.Text.ToString());

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnPrintEmpList_Click(object sender, EventArgs e)
        {
            CommonData.ViewReport = "SSERP_REP_MONTH_SALES_EMP_LIST_BY_BRANCH";
            childReportViewer = new ReportViewer("", ((SSCRM.ComboboxItem)(cbBranches.SelectedItem)).Value.ToString(), "", txtDocMonth.Text.ToUpper(), "");
            childReportViewer.Show();
        }

     
    
        
    }
}
