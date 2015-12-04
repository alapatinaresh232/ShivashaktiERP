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
    public partial class DeliveryChallanPU : Form
    {
        private ReportViewer childReportViewer = null;
        private SQLDB objSQLDB = null;
        private UtilityDB objUtilData = null;
        private ProductUnitDB objPUDB = null;
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
        public DeliveryChallanPU()
        {
            InitializeComponent();
        }
        public DeliveryChallanPU(string sFrm)
        {
            sFrmType = sFrm;
            InitializeComponent();
            
        }
        private void DeliveryChallanPU_Load(object sender, EventArgs e)
        {
            txtDocMonth.Text = CommonData.DocMonth.ToUpper();
            txtDocMonth.CharacterCasing = CharacterCasing.Upper;
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
            
            if (sFrmType == "DCST")
                txtEcodeSearch.Enabled = false;
            else
                txtEcodeSearch.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm(this);
            IsModify = false;
            dtpTranDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            gvReqDetails.Rows.Clear();
            txtTransactionNo.Text = NewTransactionNumber().ToString();
            IsModify = false;
            txtDocMonth.Text = CommonData.DocMonth.ToUpper();
            chkCancelPuDC.Checked = false;
            txtRemarks.Text = "";
            lblKnocking.Visible = false;
            lblKnocking.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void FillTransactionType()
        {
            objUtilData = new UtilityDB();
            try
            {
                DataTable dt = objUtilData.dtDCPUTranType();
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
                MessageBox.Show(ex.Message, "DC PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {

                objUtilData = null;
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
                MessageBox.Show(ex.Message, "DC PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                if (cbTransactionType.Text == "PU2SP")
                    sTrnType = "SP2SP";
                else if (cbTransactionType.Text == "PU2PU")
                    sTrnType = "PU2PU";
                else if (cbTransactionType.Text == "PU2TU")
                    sTrnType = "PU2TU";
                else if (cbTransactionType.Text == "PU2BR")
                    sTrnType = "PU2BR";
                else if (cbTransactionType.Text == "PU2CU")
                    sTrnType = "PU2CU";
                else if (cbTransactionType.Text == "PU2OL")
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
                MessageBox.Show(ex.Message, "DC PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {

                objPUDB = null;
                Cursor.Current = Cursors.Default;
            }

        }

        //private void FillBranchIndent()
        //{
        //    objPUDB = new  ProductUnitDB();
        //    int intEcode = 0;

        //    try
        //    {
        //        string[] strArrBranchCode = ((SSCRM.ComboboxItem)(cbBranches.SelectedItem)).Value.ToString().Split('@');
        //        cbRequests.DataSource = null;
        //        cbRequests.Items.Clear();
        //        DataTable dt = objPUDB.DeliveryChallanIndentList_Get(CommonData.CompanyCode, CommonData.BranchCode, strArrBranchCode[0], intEcode).Tables[0];
        //        if (dt.Rows.Count > 0)
        //        {

        //            foreach (DataRow dataRow in dt.Rows)
        //            {
        //                ComboboxItem objItem = new ComboboxItem();
        //                objItem.Value = dataRow["IndentNumber"].ToString();
        //                objItem.Text = dataRow["Indent"].ToString();
        //                cbRequests.Items.Add(objItem);
        //                objItem = null;

        //            }

        //        }
        //        dt = null;

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "DC PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //    }
        //    finally
        //    {
        //        objPUDB = null;
        //    }

        //}

        //private void FillBranchIndentDetails()
        //{
        //    objPUDB = new  ProductUnitDB();
        //    string strIndentNo = "0";
        //    try
        //    {
        //        string[] strArrBranchCode = ((SSCRM.ComboboxItem)(cbBranches.SelectedItem)).Value.ToString().Split('@');
        //        if(cbRequests.SelectedIndex>-1)
        //            strIndentNo = ((SSCRM.ComboboxItem)(cbRequests.SelectedItem)).Value.ToString();
        //        gvReqDetails.Rows.Clear();
        //        DataTable dt = objPUDB.DeliveryChallanIndentDetails_Get(CommonData.CompanyCode, strArrBranchCode[0], Convert.ToInt32(strIndentNo)).Tables[0];
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int intRow = 0; intRow < dt.Rows.Count; intRow++)
        //            {

        //                gvReqDetails.Rows.Add();
        //                gvReqDetails.Rows[intRow].Cells["SLNO"].Value = intRow + 1;
        //                gvReqDetails.Rows[intRow].Cells["ProductId"].Value = dt.Rows[intRow]["product_id"].ToString();
        //                gvReqDetails.Rows[intRow].Cells["Category"].Value = dt.Rows[intRow]["category_name"].ToString();
        //                gvReqDetails.Rows[intRow].Cells["Product"].Value = dt.Rows[intRow]["product_name"].ToString();
        //                gvReqDetails.Rows[intRow].Cells["IndQty"].Value = dt.Rows[intRow]["IndQty"].ToString();
        //                gvReqDetails.Rows[intRow].Cells["IssQty"].Value = dt.Rows[intRow]["IssQty"].ToString();
        //                gvReqDetails.Rows[intRow].Cells["RATE"].Value = dt.Rows[intRow]["RATE"].ToString();
        //                gvReqDetails.Rows[intRow].Cells["Amount"].Value = dt.Rows[intRow]["TotProductValue"].ToString();
        //                gvReqDetails.Rows[intRow].Cells["IndentNo"].Value = dt.Rows[intRow]["indent_number"].ToString();
        //                gvReqDetails.Rows[intRow].Cells["IndentSLNo"].Value = dt.Rows[intRow]["indent_sl_no"].ToString();


        //            }
        //        }
        //        dt = null;

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "DC PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //    }
        //    finally
        //    {
        //        objPUDB = null;
        //    }

        //}

        //private void cbBranches_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cbBranches.SelectedIndex > -1)
        //    {
        //        cbRequests.Items.Clear();
        //        FillBranchIndent();
        //     }
        //}

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
                MessageBox.Show(ex.Message, "DC PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                sTranNo = objPUDB.GenerateNewDCPUTranNo(CommonData.CompanyCode, CommonData.BranchCode, sFrmType);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DC PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objPUDB = null;
            }

            return sTranNo;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            int intSave = 0;
            string strCommand = "";
            if (CheckData())
            {
                try
                {
                    if (chkCancelPuDC.Checked == true)
                    {
                        if (IsModify == false)
                        {
                            strCommand = " INSERT INTO PU_FG_DC_HEAD(PUFDH_COMPANY_CODE" +
                                                                  ", PUFDH_STATE_CODE" +
                                                                  ", PUFDH_BRANCH_CODE" +
                                                                  ", PUFDH_FIN_YEAR" +
                                                                  ", PUFDH_DOCUMENT_MONTH" +
                                                                  ", PUFDH_TRN_TYPE" +
                                                                  ", PUFDH_TRN_NUMBER" +
                                                                  ", PUFDH_REF_NO" +
                                                                  ", PUFDH_TRN_DATE" +
                                                                  ", PUFDH_VEHICLE_SOURCE" +
                                                                  ", PUFDH_CREATED_BY" +
                                                                  ", PUFDH_CREATED_DATE " +
                                                                  ", PUFDH_STATUS " +
                                                                  ", PUFDH_REMARKS " +
                                                                  ") VALUES" +
                                                                  "('" + CommonData.CompanyCode +
                                                                  "', '" + CommonData.StateCode +
                                                                  "', '" + CommonData.BranchCode +
                                                                  "', '" + CommonData.FinancialYear +
                                                                  "', '" + txtDocMonth.Text +
                                                                  "', '" + cbTransactionType.Text +
                                                                  "', '" + txtTransactionNo.Text +
                                                                  "', '" + txtRefNo.Text.Replace(" ", "").ToString() +
                                                                  "', '" + Convert.ToDateTime(dtpTranDate.Value.ToString()).ToString("dd/MMM/yyyy") +
                                                                  "', '" + cbVehicleType.Text +
                                                                  "', '" + CommonData.LogUserId +
                                                                  "', '" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                                                  "','CANCELLED','" + txtRemarks.Text.ToString() + "')";
                        }
                        else if (IsModify == true)
                        {
                            strCommand = "UPDATE PU_FG_DC_HEAD " +
                                         "SET PUFDH_TRN_TYPE ='" + cbTransactionType.Text +
                                         "', PUFDH_DOCUMENT_MONTH ='" + txtDocMonth.Text +
                                         "', PUFDH_TRN_DATE ='" + Convert.ToDateTime(dtpTranDate.Value).ToString("dd/MMM/yyyy") +
                                         "', PUFDH_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                                         "',PUFDH_LAST_MODIFIED_DATE='" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                         "',PUFDH_REMARKS='" + txtRemarks.Text.ToString() +
                                         "' WHERE PUFDH_TRN_NUMBER = '" + txtTransactionNo.Text +
                                         "' AND PUFDH_BRANCH_CODE='" + CommonData.BranchCode +
                                         "' AND PUFDH_FIN_YEAR='" + CommonData.FinancialYear.ToString() +
                                         "' AND PUFDH_COMPANY_CODE='" + CommonData.CompanyCode.ToString() + "'";

                        }

                        if (strCommand.Length > 10)
                        {
                            intSave = objSQLDB.ExecuteSaveData(strCommand);
                        }
                        if (intSave > 0)
                        {
                            IsModify = false;
                            MessageBox.Show("DC data saved.\n Transaction No:" + txtTransactionNo.Text.ToString(), "Delivery challan PU", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm(this);
                            txtTransactionNo.Text = NewTransactionNumber().ToString();
                            chkCancelPuDC.Checked = false;
                            txtRemarks.Text = "";

                        }
                        else
                        {
                            MessageBox.Show("DC data not saved.", "Delivery Challan PU", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                            string strSQL = "DELETE from PU_FG_DC_HEAD" +
                                        " WHERE PUFDH_COMPANY_CODE='" + CommonData.CompanyCode +
                                        "' AND PUFDH_BRANCH_CODE='" + CommonData.BranchCode +
                                        "' AND PUFDH_TRN_NUMBER='" + txtTransactionNo.Text +
                                        "' AND PUFDH_FIN_YEAR='" + CommonData.FinancialYear + "'";
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
                            MessageBox.Show("DC data saved.\n Transaction No:" + txtTransactionNo.Text.ToString(), "Delivery challan PU", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm(this);
                            txtTransactionNo.Text = NewTransactionNumber().ToString();
                            gvReqDetails.Rows.Clear();
                        }
                        else
                        {
                            MessageBox.Show("DC data not saved.", "Delivery Challan PU", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                }


                catch (Exception ex)
                {
                    string strSQL = "DELETE from PU_FG_DC_HEAD" +
                                       " WHERE PUFDH_COMPANY_CODE='" + CommonData.CompanyCode +
                                       "' AND PUFDH_BRANCH_CODE='" + CommonData.BranchCode +
                                       "' AND PUFDH_TRN_NUMBER='" + txtTransactionNo.Text +
                                       "'  AND PUFDH_FIN_YEAR='" + CommonData.FinancialYear + "'";
                    objSQLDB = new SQLDB();
                    int intDel = objSQLDB.ExecuteSaveData(strSQL);
                    objSQLDB = null;

                    MessageBox.Show(ex.Message, "DC", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally
                {

                }
            }
           
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
                if (txtDocMonth.Text.ToString().Trim().Length == 0)
                    txtDocMonth.Text = CommonData.DocMonth.ToUpper();
                if (txtTotalFreight.Text.ToString().Trim().Length == 0)
                    txtTotalFreight.Text = "0.00";
                if (txtAdvancePaid.Text.ToString().Trim().Length == 0)
                    txtAdvancePaid.Text = "0.00";
                if (txtToPay.Text.ToString().Trim().Length == 0)
                    txtToPay.Text = "0.00";
                if (txtLoadingCharges.Text.ToString().Trim().Length == 0)
                    txtLoadingCharges.Text = "0.00";
                if (txtEcodeSearch.Text.Length > 0)
                    sEcode = txtEcodeSearch.Text;
                txtVehicleNo.Text = txtVehicleNo.Text.Replace(" ", "");
                if (IsModify == false)
                {
                    txtTransactionNo.Text = NewTransactionNumber().ToString();
                    if (txtTransactionNo.Text.ToString().Length > 5)
                    {
                        strSQL = " INSERT INTO PU_FG_DC_HEAD " +
                             "(PUFDH_COMPANY_CODE" +
                             ", PUFDH_STATE_CODE" +
                             ", PUFDH_BRANCH_CODE" +
                             ", PUFDH_FIN_YEAR" +
                             ", PUFDH_DOCUMENT_MONTH" +
                             ", PUFDH_TRN_TYPE" +
                             ", PUFDH_TRN_NUMBER" +
                             ", PUFDH_REF_NO" +
                             ", PUFDH_TRN_DATE" +
                             ", PUFDH_TO_BRANCH_CODE" +
                             ", PUFDH_TO_ECODE" +
                             ", PUFDH_TRIP_OR_LR_NUMBER" +
                             ", PUFDH_WAY_BILL_NO" +
                             ", PUFDH_VEHICLE_SOURCE" +
                             ", PUFDH_VEHICLE_NUMBER" +
                             ", PUFDH_TRANSPORTER_NAME" +
                             ", PUFDH_DRIVER_NAME" +
                             ", PUFDH_TOTAL_FREIGHT" +
                             ", PUFDH_ADVANCE_PAID" +
                             ", PUFDH_TO_PAY_FREIGHT" +
                             ", PUFDH_LOADING_CHARGES" +
                             ", PUFDH_CREATED_BY" +
                             ", PUFDH_CREATED_DATE" + 
                             ", PUFDH_STATUS " +
                             ", PUFDH_REMARKS) " +
                             "VALUES" +
                             "('" + CommonData.CompanyCode +
                             "', '" + CommonData.StateCode +
                             "', '" + CommonData.BranchCode +
                             "', '" + CommonData.FinancialYear +
                             "', '" + txtDocMonth.Text +
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
                             ", '" + CommonData.LogUserId +
                             "', getdate(),'DOCUMENTED','"+ txtRemarks.Text.ToString() +"')";
                    }
                }
                else
                {
                    strSQL = "DELETE from PU_FG_DC_DETL" +
                                " WHERE PUFDD_company_code='" + CommonData.CompanyCode +
                                    "' AND PUFDD_BRANCH_CODE='" + CommonData.BranchCode +
                                    "' AND PUFDD_TRN_NUMBER='" + txtTransactionNo.Text +
                                    "' AND PUFDD_FIN_YEAR='" + CommonData.FinancialYear + "'";

                    int intRec = objSQLDB.ExecuteSaveData(strSQL);

                    strSQL = " UPDATE PU_FG_DC_HEAD " +
                                "SET PUFDH_TRN_TYPE ='" + cbTransactionType.Text +
                                "', PUFDH_DOCUMENT_MONTH ='" + txtDocMonth.Text +
                                "', PUFDH_TRN_DATE ='" + Convert.ToDateTime(dtpTranDate.Value).ToString("dd/MMM/yyyy") +
                                "', PUFDH_TO_BRANCH_CODE='" + ((SSCRM.ComboboxItem)(cbBranches.SelectedItem)).Value +
                                "', PUFDH_TO_ECODE=" + sEcode +
                                ", PUFDH_TRIP_OR_LR_NUMBER='" + txtTripLRNo.Text +
                                "', PUFDH_WAY_BILL_NO='" + txtWayBillNo.Text +
                                "', PUFDH_VEHICLE_SOURCE='" + cbVehicleType.Text +
                                "', PUFDH_VEHICLE_NUMBER='" + txtVehicleNo.Text +
                                "', PUFDH_TRANSPORTER_NAME='" + txtTransporter.Text +
                                "', PUFDH_DRIVER_NAME='" + cbDriverEcode.Text +
                                "', PUFDH_TOTAL_FREIGHT=" + txtTotalFreight.Text +
                                ", PUFDH_ADVANCE_PAID=" + txtAdvancePaid.Text +
                                ", PUFDH_TO_PAY_FREIGHT=" + txtToPay.Text +
                                ", PUFDH_LOADING_CHARGES=" + txtLoadingCharges.Text +
                                ", PUFDH_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                                "', PUFDH_LAST_MODIFIED_DATE=getdate()" + 
                             " WHERE PUFDH_TRN_NUMBER = '" + txtTransactionNo.Text +
                             "'  AND PUFDH_BRANCH_CODE='" + CommonData.BranchCode +
                             "' AND PUFDH_FIN_YEAR='" + CommonData.FinancialYear.ToString() +
                             "' AND PUFDH_COMPANY_CODE='" + CommonData.CompanyCode.ToString() + "'";

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
                strSQL = "DELETE from PU_FG_DC_DETL" +
                                " WHERE PUFDD_COMPANY_CODE='" + CommonData.CompanyCode +
                                    "' AND PUFDD_BRANCH_CODE='" + CommonData.BranchCode +
                                    "' AND PUFDD_TRN_NUMBER='" + txtTransactionNo.Text +
                                    "' AND PUFDD_FIN_YEAR='" + CommonData.FinancialYear + "'";

                intSave = objSQLDB.ExecuteSaveData(strSQL);
                int iSlNo = 0;
                for (int i = 0; i < gvReqDetails.Rows.Count; i++)
                {

                    if (gvReqDetails.Rows[i].Cells["IssDmgQty"].Value.ToString().Trim() == "")
                        gvReqDetails.Rows[i].Cells["IssDmgQty"].Value = "0";
                    if (gvReqDetails.Rows[i].Cells["IssQty"].Value.ToString().Trim() == "")
                        gvReqDetails.Rows[i].Cells["IssQty"].Value = "0";
                    
                    double iTotalQty = 0;
                    iTotalQty = Convert.ToDouble(gvReqDetails.Rows[i].Cells["IssQty"].Value) + Convert.ToDouble(gvReqDetails.Rows[i].Cells["IssDmgQty"].Value);
                    if (iTotalQty != 0 && gvReqDetails.Rows[i].Cells["Amount"].Value.ToString() != "") //&& gvReqDetails.Rows[i].Cells["Amount"].Value.ToString() != "0.00")
                    {
                        iSlNo++;
                        sbSQL.Append("INSERT INTO PU_FG_DC_DETL (PUFDD_COMPANY_CODE, PUFDD_STATE_CODE, PUFDD_BRANCH_CODE, PUFDD_FIN_YEAR"+
                                    ", PUFDD_TRN_TYPE, PUFDD_TRN_NUMBER, PUFDD_SL_NO, PUFDD_BATCH_NO, PUFDD_PRODUCT_ID, PUFDD_ISS_QTY"+
                                    ", PUFDD_BAD_QTY, PUFDD_ISS_RATE, " +
                                    "PUFDD_BED_PED_PERC, PUFDD_BED_PED_VAL, PUFDD_EDCESS_PED_PERC, PUFDD_EDCESS_PED_VAL, " +
                                    "PUFDD_SEC_EDCESS_PED_PERC, PUFDD_SEC_EDCESS_PED_VAL, PUFDD_AGNST_ST_FORM_TYPE, PUFDD_TAX_PERC, PUFDD_TAX_VAL, " +
                                    "PUFDD_ISS_AMT, PUFDD_AGNST_DOC_REQ_TYPE, PUFDD_AGNST_DOC_REQ_NUMBER, PUFDD_AGNST_DOC_REQ_SL_NO)" +
                                    " VALUES ('" + CommonData.CompanyCode + "', '" + CommonData.StateCode + "', '" + CommonData.BranchCode + "', '" + CommonData.FinancialYear +
                                    "', '" + cbTransactionType.Text + "', '" + txtTransactionNo.Text +
                                    "', " + iSlNo +
                                    ", '" + gvReqDetails.Rows[i].Cells["BatchNo"].Value.ToString().Replace(" ","") +
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
                throw new Exception(ex.ToString());
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
            try
            {
                strSQL = "DELETE from PU_PM_DC_DETL" +
                                " WHERE PUPDD_COMPANY_CODE='" + CommonData.CompanyCode +
                                    "' AND PUPDD_BRANCH_CODE='" + CommonData.BranchCode +
                                    "' AND PUPDD_TRN_NUMBER='" + txtTransactionNo.Text +
                                    "' AND PUPDD_FIN_YEAR='" + CommonData.FinancialYear + "'";

                intSave = objSQLDB.ExecuteSaveData(strSQL);
                int iSlNO = 0;
                for (int i = 0; i < gvPackingDetl.Rows.Count; i++)
                {

                    if (gvPackingDetl.Rows[i].Cells["IssDmgQty1"].Value.ToString().Trim() == "")
                        gvPackingDetl.Rows[i].Cells["IssDmgQty1"].Value = "0";
                    if (gvPackingDetl.Rows[i].Cells["IssQty1"].Value.ToString().Trim() == "")
                        gvPackingDetl.Rows[i].Cells["IssQty1"].Value = "0";

                    double iTotalQty = 0;
                    iTotalQty = Convert.ToDouble(gvPackingDetl.Rows[i].Cells["IssQty1"].Value) + Convert.ToDouble(gvPackingDetl.Rows[i].Cells["IssDmgQty1"].Value);
                    if (iTotalQty != 0 && gvPackingDetl.Rows[i].Cells["Amount1"].Value.ToString() != "") //&& gvReqDetails.Rows[i].Cells["Amount"].Value.ToString() != "0.00")
                    {
                        iSlNO++;
                        sbSQL.Append("INSERT INTO PU_PM_DC_DETL (PUPDD_COMPANY_CODE, PUPDD_STATE_CODE, PUPDD_BRANCH_CODE, PUPDD_FIN_YEAR" +
                                    ", PUPDD_TRN_TYPE, PUPDD_TRN_NUMBER, PUPDD_SL_NO, PUPDD_BATCH_NO, PUPDD_PRODUCT_ID, PUPDD_ISS_QTY" +
                                    ", PUPDD_BAD_QTY, PUPDD_ISS_RATE, " +
                                    "PUPDD_BED_PED_PERC, PUPDD_BED_PED_VAL, PUPDD_EDCESS_PED_PERC, PUPDD_EDCESS_PED_VAL, " +
                                    "PUPDD_SEC_EDCESS_PED_PERC, PUPDD_SEC_EDCESS_PED_VAL, PUPDD_AGNST_ST_FORM_TYPE, PUPDD_TAX_PERC, PUPDD_TAX_VAL, " +
                                    "PUPDD_ISS_AMT, PUPDD_AGNST_DOC_REQ_TYPE, PUPDD_AGNST_DOC_REQ_NUMBER, PUPDD_AGNST_DOC_REQ_SL_NO)" +
                                    " VALUES ('" + CommonData.CompanyCode + "', '" + CommonData.StateCode + "', '" + CommonData.BranchCode + "', '" + CommonData.FinancialYear +
                                    "', '" + cbTransactionType.Text + "', '" + txtTransactionNo.Text +
                                    "', " + iSlNO +
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
            return intSave;


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
                    CalculateTotals();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DC PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                MessageBox.Show(ex.Message, "DC PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objPUDB = null;
            }
        }

        private bool CheckData()
        {
            bool blValue = true;
            //if (Convert.ToString(txtRefNo.Text).Length == 0)
            //{
            //    MessageBox.Show("Enter DC Reference No !", "DC PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    blValue = false;
            //    return blValue;
            //}
            //if (Convert.ToString(txtTransactionNo.Text).Length == 0)
            //{
            //    MessageBox.Show("Transaction no is not generated!\n Contact to IT-Department", "DC PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    blValue = false;
            //    return blValue;
            //}
            //if (Convert.ToString(txtTransactionNo.Text).Length < 20)
            //{
            //    MessageBox.Show("Invalid Transaction Number!", "DC PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    blValue = false;
            //    txtTransactionNo.Focus();
            //    return blValue;
            //}
            //if (Convert.ToString(txtTransactionNo.Text).IndexOf('-') == -1)
            //{
            //    MessageBox.Show("Invalid Transaction Number!", "DC PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    blValue = false;
            //    txtTransactionNo.Focus();
            //    return blValue;
            //}

            //if (cbBranches.SelectedIndex == -1)
            //{
            //    MessageBox.Show("Select To Branch!", "DC PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    blValue = false;
            //    cbBranches.Focus();
            //    return blValue;
            //}
            ////if (cbTransactionType.SelectedValue == "PU2BR")
            ////{
            ////    if (txtEmpName.Text.Length == 0 || txtEcodeSearch.Text.Length == 0)
            ////    {
            ////        MessageBox.Show("Enter Valid Ecode!", "DC PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            ////        blValue = false;
            ////        txtEcodeSearch.Focus();
            ////        return blValue;
            ////    }
            ////}
            ////if (Convert.ToString(txtTotalFreight.Text).Trim().Length == 0)
            ////{
            ////    MessageBox.Show("Enter Freight !", "DC PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            ////    blValue = false;
            ////    txtTotalFreight.Focus();
            ////    return blValue;
            ////}

            ////if (Convert.ToString(txtTripLRNo.Text).Trim().Length == 0)
            ////{
            ////    MessageBox.Show("Enter Trip / LR Number !", "DC PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            ////    blValue = false;
            ////    txtTripLRNo.Focus();
            ////    return blValue;
            ////}

            ////if (Convert.ToString(txtVehicleNo.Text).Trim().Length == 0)
            ////{
            ////    MessageBox.Show("Enter VehicleNo !", "DC PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            ////    blValue = false;
            ////    txtVehicleNo.Focus();
            ////    return blValue;
            ////}



            //bool blInvDtl = false;
            //for (int i = 0; i < gvReqDetails.Rows.Count; i++)
            //{
            //    if (Convert.ToString(gvReqDetails.Rows[i].Cells["IssQty"].Value) != "0" && Convert.ToString(gvReqDetails.Rows[i].Cells["Amount"].Value) != "0.00")
            //    {
            //        blInvDtl = true;
            //    }

            //}

            //if (blInvDtl == false)
            //{
            //    blValue = false;
            //    MessageBox.Show("Enter product quantity", "DC PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}
            if (Convert.ToString(txtTransactionNo.Text).Length == 0)
            {
                MessageBox.Show("Enter Transaction number!", "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtTransactionNo.Focus();
                return blValue;
            }
            if (Convert.ToString(txtRefNo.Text).Length == 0)
            {
                MessageBox.Show("Enter Reference No", "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtRefNo.Focus();
                return blValue;
            }

            if (Convert.ToInt32(Convert.ToDateTime(dtpTranDate.Value).ToString("yyyy")) < 1950)
            {
                MessageBox.Show("Enter valid  Date !", "DC/DCST PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                dtpTranDate.CausesValidation = true;
                dtpTranDate.Focus();
                return blValue;
            }
            if (Convert.ToDateTime(Convert.ToDateTime(dtpTranDate.Value).ToString("dd/MM/yyyy")) > Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyyy")))
            {
                MessageBox.Show("Date should be less than to day", "DC/DCST PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                dtpTranDate.CausesValidation = true;
                blValue = false;
                dtpTranDate.Focus();
                return blValue;
            }

            if (chkCancelPuDC.Checked == true)
            {
                if (txtRemarks.Text.Length <= 20)
                {
                    MessageBox.Show("Enter Remarks", "DC/DCST PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    blValue = false;
                    txtRemarks.Focus();
                    return blValue;
                }
            }
            else
            {
                if (cbBranches.SelectedIndex == -1)
                {
                    MessageBox.Show("Select To Branch!", "DC/DCST PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    blValue = false;
                    cbBranches.Focus();
                    return blValue;
                }
                if (cbTransactionType.SelectedValue.ToString() == "PU2BR")
                {
                    if (txtEcodeSearch.Text.Length == 0)
                    {
                        MessageBox.Show("Enter GL/GC number!", "DC/DCST PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        blValue = false;
                        txtEcodeSearch.Focus();
                        return blValue;
                    }
                }
                //if (Convert.ToString(txtTotalFreight.Text).Trim().Length == 0)
                //{
                //    MessageBox.Show("Enter Freight !", "DC/DCST SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //    blValue = false;
                //    txtTotalFreight.Focus();
                //    return blValue;
                //}
                if (cbVehicleType.Text != "BYHAND")
                {
                    if (Convert.ToString(txtTripLRNo.Text).Trim().Length == 0 && cbVehicleType.SelectedText != "BYHAND")
                    {
                        MessageBox.Show("Enter Trip / LR Number !", "DC/DCST PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        blValue = false;
                        txtTripLRNo.Focus();
                        return blValue;
                    }

                    if (Convert.ToString(txtVehicleNo.Text).Trim().Length == 0)
                    {
                        MessageBox.Show("Enter VehicleNo !", "DC/DCST PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        blValue = false;
                        txtVehicleNo.Focus();
                        return blValue;
                    }
                }
                bool blInvDtl = false;
                for (int i = 0; i < gvReqDetails.Rows.Count; i++)
                {
                    if (Convert.ToString(gvReqDetails.Rows[i].Cells["IssQty"].Value) != "" && Convert.ToString(gvReqDetails.Rows[i].Cells["Amount"].Value) != "")
                    {
                        blInvDtl = true;
                    }

                }
                if (CheckingDocMonth() == false)
                {
                    MessageBox.Show("Enter Valid Document Month!", "DC/DCST PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    blValue = false;
                    txtDocMonth.Focus();
                    return blValue;
                }
                if (blInvDtl == false)
                {
                    blValue = false;
                    MessageBox.Show("Enter product quantity/Amount", "DC/DCST PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }

            return blValue;

        }
        private bool CheckingDocMonth()
        {
            bool flag = false;
            try
            {
                string strSQL = "SELECT * FROM document_month WHERE company_code='" + CommonData.CompanyCode + "' AND fin_year='" + CommonData.FinancialYear + "' and document_month='" + txtDocMonth.Text + "'";
                objSQLDB = new SQLDB();
                DataTable DT = objSQLDB.ExecuteDataSet(strSQL).Tables[0];
                if (DT.Rows.Count > 0)
                {
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return flag;
        }
        private void gvProductDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 6 && e.ColumnIndex < 18)
            {
                if (gvReqDetails.Rows[e.RowIndex].Cells["IssQty"].Value.ToString() == "")
                    gvReqDetails.Rows[e.RowIndex].Cells["IssQty"].Value = "0";
                if (gvReqDetails.Rows[e.RowIndex].Cells["IssDmgQty"].Value.ToString() == "")
                    gvReqDetails.Rows[e.RowIndex].Cells["IssDmgQty"].Value = "0";
                if (gvReqDetails.Rows[e.RowIndex].Cells["TotQty"].Value.ToString() == "")
                    gvReqDetails.Rows[e.RowIndex].Cells["TotQty"].Value = "0";
                gvReqDetails.Rows[e.RowIndex].Cells["TotQty"].Value = Convert.ToDouble(Convert.ToDouble(gvReqDetails.Rows[e.RowIndex].Cells["IssQty"].Value) + Convert.ToDouble(gvReqDetails.Rows[e.RowIndex].Cells["IssDmgQty"].Value)).ToString("f");
                if (Convert.ToString(gvReqDetails.Rows[e.RowIndex].Cells["IssQty"].Value) != "")
                {
                    if (gvReqDetails.Rows[e.RowIndex].Cells["IssQty"].Value.ToString() == "")
                        gvReqDetails.Rows[e.RowIndex].Cells["IssQty"].Value = "0";
                    if (Convert.ToDouble(gvReqDetails.Rows[e.RowIndex].Cells["IssQty"].Value) >= 0 && Convert.ToDouble(gvReqDetails.Rows[e.RowIndex].Cells["Rate"].Value) >= 0)
                    {
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
            CalculateTotals();
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

        private void CalculateTotals()
        {
            double totGood = 0, totDmg = 0, totQty = 0, amt = 0, totCrts = 0, iProducts = 0;
            for (int i = 0; i < gvPackingDetl.Rows.Count; i++)
            {
                double iGood = 0, iDmg = 0, iTot = 0;
                if (gvPackingDetl.Rows[i].Cells["IssQty"].Value.ToString().Replace(" ", "") != "")
                    iGood = Convert.ToDouble(gvPackingDetl.Rows[i].Cells["IssQty"].Value.ToString().Replace(" ", ""));
                if (gvPackingDetl.Rows[i].Cells["IssDmgQty"].Value.ToString().Replace(" ", "") != "")
                    iDmg = Convert.ToDouble(gvPackingDetl.Rows[i].Cells["IssDmgQty"].Value.ToString().Replace(" ", ""));
                iTot = iGood + iDmg;
                gvPackingDetl.Rows[i].Cells["TotQty"].Value = iTot.ToString("f");
            }
            for (int i = 0; i < gvReqDetails.Rows.Count; i++)
            {
                double iGood = 0, iDmg = 0, iTot = 0;
                try { Convert.ToDouble(gvReqDetails.Rows[i].Cells["IssQty"].Value.ToString()); }
                catch { gvReqDetails.Rows[i].Cells["IssQty"].Value = "0"; }
                try { Convert.ToDouble(gvReqDetails.Rows[i].Cells["IssDmgQty"].Value.ToString()); }
                catch { gvReqDetails.Rows[i].Cells["IssDmgQty"].Value = "0"; }
                try { Convert.ToDouble(gvReqDetails.Rows[i].Cells["Amount"].Value.ToString()); }
                catch { gvReqDetails.Rows[i].Cells["Amount"].Value = "0"; }
                if (gvReqDetails.Rows[i].Cells["Category"].Value.ToString() != "PACKING MATERIAL")
                {
                    totGood += Convert.ToDouble(gvReqDetails.Rows[i].Cells["IssQty"].Value.ToString());
                    totDmg += Convert.ToDouble(gvReqDetails.Rows[i].Cells["IssDmgQty"].Value.ToString());
                    amt += Convert.ToDouble(gvReqDetails.Rows[i].Cells["Amount"].Value.ToString());
                    iProducts++;
                }
                else
                {
                    totCrts += (Convert.ToDouble(gvReqDetails.Rows[i].Cells["IssQty"].Value.ToString()) +
                                Convert.ToDouble(gvReqDetails.Rows[i].Cells["IssDmgQty"].Value.ToString()));
                }

                //if (gvReqDetails.Rows[i].Cells["IssQty"].Value.ToString().Replace(" ", "") != "")
                //    iGood = Convert.ToDouble(gvReqDetails.Rows[i].Cells["IssQty"].Value.ToString().Replace(" ", ""));
                //if (gvReqDetails.Rows[i].Cells["IssDmgQty"].Value.ToString().Replace(" ", "") != "")
                //    iDmg = Convert.ToDouble(gvReqDetails.Rows[i].Cells["IssDmgQty"].Value.ToString().Replace(" ", ""));
                //iTot = iGood + iDmg;
                //gvReqDetails.Rows[i].Cells["TotQty"].Value = iTot.ToString("f");
                //if (gvReqDetails.Rows[i].Cells["IssQty"].Value.ToString() != "" && gvReqDetails.Rows[i].Cells["Rate"].Value.ToString() != "" && gvReqDetails.Rows[i].Cells["Amount"].Value.ToString() != "")
                //{
                //    amt += Convert.ToDouble(gvReqDetails.Rows[i].Cells["Amount"].Value);
                //}

                //totGood += iGood;
                //totDmg += iDmg;
                //totQty += iTot;
            }
            totQty = totGood + totDmg;
            txtGoodQty.Text = totGood.ToString("f");
            txtDmgQty.Text = totDmg.ToString("f");
            txtTotQty.Text = totQty.ToString("f");
            txtProducts.Text = iProducts.ToString("f");
            txtCrates.Text = totCrts.ToString("f");
            txtDcAmt.Text = Convert.ToDouble(amt).ToString("f");
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

        private void FillTransactionData(string TranNO)
        {
            objPUDB = new ProductUnitDB();
            Hashtable ht = new Hashtable();
            try
            {

                ht = objPUDB.GetDeliveryChallanData("", TranNO);
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
                    if (Convert.ToInt32(dtHead.Rows[0]["GRNCount"].ToString()) > 0)
                    {
                        btnSave.Enabled = false;
                        btnClearProd.Enabled = false;
                        btnProductSearch.Enabled = false;
                        btnDelete.Enabled = false;
                        lblKnocking.Visible = true;
                        lblKnocking.Text = "Cant Modify Allready Knocked With Knocked Trn No: " + dtHead.Rows[0]["KnockedGRNNo"].ToString() + " / Ref No: " + dtHead.Rows[0]["KnockedGRNRefNo"].ToString();
                    }
                    else
                    {
                        if (Convert.ToInt32(dtHead.Rows[0]["BackDays"]) > 5)
                        {
                            if (CommonData.LogUserId.ToUpper() != "ADMIN" && CommonData.LogUserRole != "MANAGEMENT")
                            {
                                btnSave.Enabled = false;
                                btnClearProd.Enabled = false;
                                btnProductSearch.Enabled = false;
                                btnDelete.Enabled = false;
                                lblKnocking.Visible = false;
                                lblKnocking.Text = "";
                            }
                            else
                            {
                                btnSave.Enabled = true;
                                btnClearProd.Enabled = true;
                                btnDelete.Enabled = true;
                                btnProductSearch.Enabled = true;
                                lblKnocking.Visible = false;
                                lblKnocking.Text = "";
                            }
                            IsModify = true;
                            strTransactionNo = dtHead.Rows[0]["TrnNumber"].ToString();
                            txtRefNo.Text = dtHead.Rows[0]["pufdh_ref_no"].ToString();
                            txtTransactionNo.Text = dtHead.Rows[0]["TrnNumber"] + "";
                            dtpTranDate.Value = Convert.ToDateTime(dtHead.Rows[0]["TrnDate"] + "");
                            txtTotalFreight.Text = dtHead.Rows[0]["TotalFreight"] + "";
                            txtAdvancePaid.Text = dtHead.Rows[0]["AdvancePaid"] + "";
                            txtToPay.Text = dtHead.Rows[0]["ToPayFreight"] + "";
                            txtLoadingCharges.Text = dtHead.Rows[0]["LoadingCharges"] + "";
                            txtWayBillNo.Text = dtHead.Rows[0]["WayBillNo"] + "";
                            cbTransactionType.Text = dtHead.Rows[0]["TrnType"] + "";
                            strToBranchCode = dtHead.Rows[0]["ToBranchCode"] + "";
                            cbBranches.SelectedIndex = objGeneral.GetComboBoxSelectedIndex(strToBranchCode, cbBranches);
                            // strECode = dtHead.Rows[0]["ToEcode"] + "";

                            if (dtHead.Rows[0]["Status"].ToString().Equals("CANCELLED"))
                            {
                                chkCancelPuDC.Checked = true;
                            }
                            else
                            {
                                chkCancelPuDC.Checked = false;
                            }

                            txtRemarks.Text = dtHead.Rows[0]["Remarks"].ToString();



                            cbVehicleType.SelectedValue = dtHead.Rows[0]["VehicleSource"] + "";
                            txtEcodeSearch.Text = dtHead.Rows[0]["EoraCode"] + "";
                            txtEmpName.Text = dtHead.Rows[0]["EmpName"] + "";
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
                            txtDocMonth.Text = dtHead.Rows[0]["DocMonth"].ToString();
                            txtTripLRNo.Text = Convert.ToString(dtHead.Rows[0]["TripLRNumber"]);
                            txtVehicleNo.Text = Convert.ToString(dtHead.Rows[0]["VehicleNumber"]);

                            txtTransporter.Text = dtHead.Rows[0]["TransporterName"] + "";

                            if (chkCancelPuDC.Checked == false)
                            {
                                FillTransactionDetl(dtDetl);
                                FillTransactionPMDetl(dtPMDetl);
                            }

                            isData = true;
                        }
                        else
                        {
                            lblKnocking.Visible = false;
                            lblKnocking.Text = "";

                            IsModify = true;
                            strTransactionNo = dtHead.Rows[0]["TrnNumber"].ToString();
                            txtRefNo.Text = dtHead.Rows[0]["pufdh_ref_no"].ToString();
                            txtTransactionNo.Text = dtHead.Rows[0]["TrnNumber"] + "";
                            dtpTranDate.Value = Convert.ToDateTime(dtHead.Rows[0]["TrnDate"] + "");
                            txtTotalFreight.Text = dtHead.Rows[0]["TotalFreight"] + "";
                            txtAdvancePaid.Text = dtHead.Rows[0]["AdvancePaid"] + "";
                            txtToPay.Text = dtHead.Rows[0]["ToPayFreight"] + "";
                            txtLoadingCharges.Text = dtHead.Rows[0]["LoadingCharges"] + "";
                            txtWayBillNo.Text = dtHead.Rows[0]["WayBillNo"] + "";
                            cbTransactionType.Text = dtHead.Rows[0]["TrnType"] + "";
                            strToBranchCode = dtHead.Rows[0]["ToBranchCode"] + "";
                            cbBranches.SelectedIndex = objGeneral.GetComboBoxSelectedIndex(strToBranchCode, cbBranches);
                            // strECode = dtHead.Rows[0]["ToEcode"] + "";

                            if (dtHead.Rows[0]["Status"].ToString().Equals("CANCELLED"))
                            {
                                chkCancelPuDC.Checked = true;
                            }
                            else
                            {
                                chkCancelPuDC.Checked = false;
                            }

                            txtRemarks.Text = dtHead.Rows[0]["Remarks"].ToString();


                            cbVehicleType.SelectedValue = dtHead.Rows[0]["VehicleSource"] + "";
                            txtEcodeSearch.Text = dtHead.Rows[0]["EoraCode"] + "";
                            txtEmpName.Text = dtHead.Rows[0]["EmpName"] + "";
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
                            txtDocMonth.Text = dtHead.Rows[0]["DocMonth"].ToString();
                            txtTripLRNo.Text = Convert.ToString(dtHead.Rows[0]["TripLRNumber"]);
                            txtVehicleNo.Text = Convert.ToString(dtHead.Rows[0]["VehicleNumber"]);

                            txtTransporter.Text = dtHead.Rows[0]["TransporterName"] + "";

                            if (chkCancelPuDC.Checked == false)
                            {
                                FillTransactionDetl(dtDetl);
                                FillTransactionPMDetl(dtPMDetl);
                            }

                            isData = true;
                        }
                    }
                }
                else
                {
                    txtDocMonth.Text = CommonData.DocMonth.ToUpper();
                    txtTransactionNo.Text = NewTransactionNumber();
                    btnSave.Enabled = true;
                    btnDelete.Enabled = true;
                    IsModify = false;
                    gvReqDetails.Rows.Clear();
                    txtGoodQty.Text = "0";
                    txtDmgQty.Text = "0";
                    txtTotQty.Text = "0";
                    txtProducts.Text = "0";
                    txtDcAmt.Text = "0";

                    chkCancelPuDC.Checked = false;
                    txtRemarks.Text = "";
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

        private void SaveDeleteEnableDisable()
        {
            try
            {
                if (Convert.ToDateTime(Convert.ToDateTime(dtpTranDate.Value).ToString("dd/MM/yyyy")) < Convert.ToDateTime(CommonData.DocFDate) ||
                        Convert.ToDateTime(Convert.ToDateTime(dtpTranDate.Value).ToString("dd/MM/yyyy")) > Convert.ToDateTime(CommonData.DocTDate))
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

        private void txtTransactionNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtTransactionNo.SelectionStart <= txtTransactionNo.Text.Length && txtTransactionNo.SelectionStart >= txtTransactionNo.Text.Length-6)
            {
                if (char.IsDigit(e.KeyChar) == false)
                    e.Handled = true;
                if (e.KeyChar == 8)
                    e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void gvProductDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            blIsCellQty = true;
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 7)
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    txtQty.MaxLength = 6;
                    txtQty.Tag = "Qty";
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }

            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 6)
            {
                TextBox txtRate = e.Control as TextBox;
                if (txtRate != null)
                {
                    txtRate.MaxLength = 10;
                    txtRate.Tag = "Rate";
                    txtRate.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
            else if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 4)
            {
                TextBox txtBatchNo = e.Control as TextBox;
                if (txtBatchNo != null)
                {
                    txtBatchNo.MaxLength = 30;
                    txtBatchNo.CharacterCasing = CharacterCasing.Upper;
                    txtBatchNo.Tag = "batchNo";
                    //txtBatchNo.KeyPress += new KeyPressEventHandler(txt_BathNoKeyPress);
                }
            }
        }
        
        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            TextBox tb=(TextBox)sender;
            if (tb.Tag != "batchNo")
            {
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
        }
        private void txt_BathNoKeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }
        private void txtDriverNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void gvIndentDetails_CellClick(object sender, DataGridViewCellEventArgs e)
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
                    CalculateTotals();
                }
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
                        MessageBox.Show("You cannot manipulate backdays data!\n If you want to modify, Contact to IT-Department", "DC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTransactionNo.Focus();
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Do you want to Delete " + txtTransactionNo.Text + " Transaction ?",
                                               "CRM DC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            objSQLDB = new SQLDB();

                            string strDelete = " DELETE from PU_FG_DC_DETL " +
                                                " WHERE PUFDD_COMPANY_CODE='" + CommonData.CompanyCode +
                                                "' AND PUFDD_BRANCH_CODE='" + CommonData.BranchCode +
                                                "' AND PUFDD_TRN_NUMBER='" + txtTransactionNo.Text +
                                                "'  AND PUFDD_FIN_YEAR='" + CommonData.FinancialYear + "';";

                            strDelete += " DELETE from PU_FG_DC_HEAD" +
                                                " WHERE PUFDH_COMPANY_CODE='" + CommonData.CompanyCode +
                                                "' AND PUFDH_BRANCH_CODE='" + CommonData.BranchCode +
                                                "' AND PUFDH_TRN_NUMBER='" + txtTransactionNo.Text +
                                                "'  AND PUFDH_FIN_YEAR='" + CommonData.FinancialYear + "'; ";

                            int intRec = objSQLDB.ExecuteSaveData(strDelete);
                            if (intRec > 0)
                            {
                                IsModify = false;
                                MessageBox.Show("Tran No: " + txtTransactionNo.Text + " Deleted!");
                                ClearForm(this);
                                gvReqDetails.Rows.Clear();
                                txtDocMonth.Text = CommonData.DocMonth;
                                txtTransactionNo.Text = NewTransactionNumber().ToString();
                            }

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Enter Tran Number.", "DC PU", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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

        private void gvIndentDetails_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            

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

        private void btnClearProd_Click(object sender, EventArgs e)
        {
            gvReqDetails.Rows.Clear();
        }

        //private void cbIndents_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cbRequests.SelectedIndex > -1)
        //    {
        //        FillBranchIndentDetails();
        //    }
        //}

        private void cbVehicleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbVehicleType.Text == "OWN")
                cbDriverEcode.Visible = true;
            else
                cbDriverEcode.Visible = false;
        }

        private void cbDriverEcode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            //BatchProductSearch PSearch = new BatchProductSearch("DCfromPU");
            //PSearch.objDeliveryChallanPU = this;
            //PSearch.ShowDialog();

            ProductSearchAll PSearch = new ProductSearchAll("DC_PU");
            PSearch.objDeliveryChallanPU = this;
            PSearch.ShowDialog();

            //CalculateTotals();
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

        private void txtTotalFreight_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtToPay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false)
                e.Handled = true;
            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void txtTotalFreight_KeyUp(object sender, KeyEventArgs e)
        {
            txtToPay.Text = txtTotalFreight.Text;
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

        private void txtTripLRNo_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtTransporter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetterOrDigit(e.KeyChar) == false)
                e.Handled = true;

            if (e.KeyChar == 8 || e.KeyChar == 32)
                e.Handled = false;

            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtTransactionNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 37 || e.KeyValue == 39)
                e.Handled = false;
            else if (txtTransactionNo.SelectionStart < 14)
                e.Handled = true;
        }

        private void txtTransactionNo_Enter(object sender, EventArgs e)
        {
            this.txtTransactionNo.Select(txtTransactionNo.Text.Length, 0);
        }

        private void txtDriverNo_TextChanged(object sender, EventArgs e)
        {

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

        private void txtRefNo_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtRefNo_Validated(object sender, EventArgs e)
        {
            if (txtRefNo.Text.Trim().Length > 0)
            {
                FillDCDetails(txtRefNo.Text.Replace("  ", "").ToString());
            }
            else
            {
                btnCancel_Click(null,null);
                txtRefNo.Focus();
            }
        }
        private void FillDCDetails(string sRefNo)
        {
            objSQLDB = new SQLDB();
            DataSet sDCInfo = new DataSet();
            string sDCType = "";

            if (sFrmType == "DCST")
            {
                sDCType = "DCST-";
            }
            else
            {
                sDCType = "DC-";
            }
            try
            {
                sDCInfo = objSQLDB.ExecuteDataSet("SELECT PUFDH_TRN_NUMBER FROM PU_FG_DC_HEAD WHERE PUFDH_BRANCH_CODE='" + CommonData.BranchCode +
                                                    "' AND PUFDH_REF_NO='" + sRefNo + "' AND PUFDH_FIN_YEAR = '" + CommonData.FinancialYear +
                                                    "' AND PUFDH_TRN_NUMBER LIKE '%" + sDCType + "%'");
                DataTable sDCHead = sDCInfo.Tables[0];
                if (sDCHead.Rows.Count > 0)
                {
                    txtTransactionNo.Text = "";
                    //btnCancel_Click(null,null);
                    txtTransactionNo.Text = sDCHead.Rows[0]["PUFDH_TRN_NUMBER"] + "";

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

        private void txtTransactionNo_TextChanged(object sender, EventArgs e)
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

        private void gvPackingDetl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0)
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

        private void gvPackingDetl_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>-1)
            {
                if (e.ColumnIndex >= 6 && e.ColumnIndex < 18)
                {
                    try
                    {
                        Convert.ToString(gvPackingDetl.Rows[e.RowIndex].Cells["IssQty1"].Value);
                    }
                    catch
                    {
                        gvPackingDetl.Rows[e.RowIndex].Cells["IssQty1"].Value = "0";
                    }
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
            else if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 6)
            {
                TextBox txtRate = e.Control as TextBox;
                if (txtRate != null)
                {
                    txtRate.MaxLength = 10;
                    txtRate.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
            
        }

        private void btnClearItems_Click(object sender, EventArgs e)
        {
            gvPackingDetl.Rows.Clear();
        }

        private void btnAddPMeterialItems_Click(object sender, EventArgs e)
        {
            ProductSearchAll PSearch = new ProductSearchAll("DC_PU_PM");
            PSearch.objDeliveryChallanPU = this;
            PSearch.ShowDialog();
        }

        private void btnPrintEmpList_Click(object sender, EventArgs e)
        {
            
            //string txtItem = view[cbBranches.SelectedItem].to;
                //cbBranches.Items[cbBranches.SelectedIndex].ToString();
            CommonData.ViewReport = "SSERP_REP_MONTH_SALES_EMP_LIST_BY_BRANCH";
            childReportViewer = new ReportViewer("", ((SSCRM.ComboboxItem)(cbBranches.SelectedItem)).Value.ToString(), "", txtDocMonth.Text.ToUpper(), "");
            childReportViewer.Show();

        }

      
    }
}
