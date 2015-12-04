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
    public partial class DeliveryChallan : Form
    {
        private SQLDB objSQLDB = null;
        private InvoiceDB objData = null;
        private Master objMasterData = null;
        private UtilityDB objUtilData = null;
        private IndentDB objIndent = null;
        private General objGeneral = null;
        private StockPointDB objStockPointDB = null;
        private ReportViewer childReportViewer = null;
        private string strFormerid = string.Empty;
        private string strTransactionNo = string.Empty;
        private string strBranchCode = string.Empty;
        private string strECode = string.Empty;
        private bool blIsCellQty = true;
        private bool IsModify = false;
        private string sFinYear = "";
        private string strToBranchCode = string.Empty;
        private bool TranTypeLoad = false;
        private string sForm = string.Empty;

        public DeliveryChallan()
        {
            InitializeComponent();
        }

        public DeliveryChallan(string sFmType)
        {
            sForm = sFmType;
            InitializeComponent();
        }

        private void DeliveryChallan_Load(object sender, EventArgs e)
        {
            if (sForm == "DC")
                this.Text = "Delivery Chalan :: DC";
            else if (sForm == "DCST")
                this.Text = "Delivery Chalan :: Stock Transfer";

            txtDocMonth.Text = CommonData.DocMonth;
            sFinYear = CommonData.FinancialYear;
            FillTransactionType();
            FillBranchData();
            FillBranchGroupData(0);
            FillVehcleType();
            cbVehicleType.SelectedIndex = 0;
            gvIndentDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                       System.Drawing.FontStyle.Regular);
            meTransactionDate.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyyy"));
            txtTransactionNo.Text = NewTransactionNumber().ToString();

            FillDriverEmployeeData();
            cbTransactionType.SelectedIndex = 0;
            //cbBranches.SelectedIndex = 0;
            CalculateTotals();
            if (CommonData.LogUserId == "ADMIN" || CommonData.LogUserRole == "MANAGEMENT")
            {
                txtDocMonth.ReadOnly = false;
            }
            else
            {
                txtDocMonth.ReadOnly = true;
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm(this);
            IsModify = false;
            meTransactionDate.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyyy"));
            gvIndentDetails.Rows.Clear();
            txtTransactionNo.Text = NewTransactionNumber().ToString();
            IsModify = false;
            btnSave.Enabled = true;
            btnClearProd.Enabled = true;
            btnDelete.Enabled = true;
            btnProductSearch.Enabled = true;
            lblKnocking.Visible = false;
            lblKnocking.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            
            this.Close();
            this.Dispose();
        }

        private void FillBranchGroupData(Int32 ecode)
        {
            
            objIndent = new IndentDB();
            string strStockPointCode = string.Empty;
            try
            {
                cbEcode.DataSource = null;
                cbEcode.Items.Clear();
                if (cbBranches.SelectedIndex > -1)
                {
                    string[] strArrBranchCode = cbBranches.SelectedValue.ToString().Split('@');
                        //((SSCRM.ComboboxItem)(cbBranches.SelectedItem)).Value.ToString().Split('@');
                    strToBranchCode = strArrBranchCode[0];
                }
                DataTable dtGroup = objIndent.IndentGroupEcodeList_Get(CommonData.CompanyCode.ToString(), strToBranchCode, CommonData.DocMonth, ecode).Tables[0];
                if (ecode == 0)
                {
                    DataRow dr = dtGroup.NewRow();
                    dr[0] = "0";
                    dr[1] = "Select";
                    dtGroup.Rows.InsertAt(dr, 0);
                    dr = null;
                }

                if (dtGroup.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dtGroup.Rows)
                    {
                        ComboboxItem objItem = new ComboboxItem();
                        objItem.Value = dataRow["ECODE"].ToString();
                        objItem.Text = dataRow["ENAME"].ToString();
                        cbEcode.Items.Add(objItem);
                        objItem = null;
                    }
                    cbEcode.SelectedIndex = 0;
                }
                else
                {
                    DataRow dr = dtGroup.NewRow();
                    dr[0] = "0";
                    dr[1] = "Select";
                    dtGroup.Rows.InsertAt(dr, 0);
                    dr = null;
                    foreach (DataRow dataRow in dtGroup.Rows)
                    {
                        ComboboxItem objItem = new ComboboxItem();
                        objItem.Value = dataRow["ECODE"].ToString();
                        objItem.Text = dataRow["ENAME"].ToString();
                        cbEcode.Items.Add(objItem);
                        objItem = null;
                    }
                    cbEcode.SelectedIndex = 0;
                }
                dtGroup = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objIndent = null;
            }

       }

        private void FillTransactionType()
        {
            objUtilData = new UtilityDB();
            try
            {
                DataTable dt = objUtilData.dtDCTranType();
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
                MessageBox.Show(ex.Message,"DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                MessageBox.Show(ex.Message,"DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {

                objUtilData = null;
                Cursor.Current = Cursors.Default;
            }

        }

        private void FillBranchData()
        {
            objIndent = new IndentDB();
            try
            {
                cbBranches.DataSource = null;
                string transType = string.Empty;
                if (cbTransactionType.Text.ToString() == "SP2PU")
                    transType = "PU2PU";
                else
                    transType = cbTransactionType.Text.ToString();
                DataTable dtBranch = objIndent.BranchListForDC_Get(CommonData.CompanyCode, transType).Tables[0];
                if (dtBranch.Rows.Count > 0)
                {
                    //DataView dv1 = dtBranch.DefaultView;
                    //dv1.RowFilter = " active = 'T' ";
                    //DataTable dtBR = dv1.ToTable();
                    //if (dtBR.Rows.Count > 0)
                    //{
                    //foreach (DataRow dataRow in dtBR.Rows)
                    //{
                    cbBranches.DataSource = dtBranch;
                    cbBranches.ValueMember = "branch_code";
                    cbBranches.DisplayMember = "branch_name";
                    
                    //ComboboxItem objItem = new ComboboxItem();
                    //objItem.Value = dataRow["branch_code"].ToString();
                    //objItem.Text = dataRow["branch_name"].ToString();
                    //cbBranches.Items.Add(objItem);
                    //objItem = null;
                    //}
                    cbBranches.SelectedIndex = 0;
                    //}
                }
                dtBranch = null;
                //dv1 = null;
                //dtBR = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {

                objData = null;
                Cursor.Current = Cursors.Default;
            }

        }

        private void FillBranchIndent()
        {
            objIndent = new IndentDB();
            int intEcode = 0;
            
            try
            {
                string[] strArrBranchCode = cbBranches.SelectedValue.ToString().Split('@');
                    //((SSCRM.ComboboxItem)(cbBranches.SelectedItem)).Value.ToString().Split('@');
               
                if (cbEcode.SelectedIndex>0)
                    intEcode = Convert.ToInt32(((SSCRM.ComboboxItem)(cbEcode.SelectedItem)).Value.ToString());
                cbIndents.DataSource = null;
                cbIndents.Items.Clear();
                DataTable dt = objIndent.DeliveryChallanIndentList_Get(CommonData.CompanyCode, CommonData.BranchCode, strArrBranchCode[0], intEcode).Tables[0];
                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dt.Rows)
                    {
                        ComboboxItem objItem = new ComboboxItem();
                        objItem.Value = dataRow["IndentNumber"].ToString();
                        objItem.Text = dataRow["Indent"].ToString();
                        cbIndents.Items.Add(objItem);
                        objItem = null;
                        
                    }

                }
                dt = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objIndent = null;
            }

        }

        private void FillBranchIndentDetails()
        {
            objIndent = new IndentDB();
            string strIndentNo = "0";
            int intEcode = 0;
            try
            {
                string[] strArrBranchCode = cbBranches.SelectedValue.ToString().Split('@');
                    //((SSCRM.ComboboxItem)(cbBranches.SelectedItem)).Value.ToString().Split('@');
                if(cbIndents.SelectedIndex>-1)
                    strIndentNo = ((SSCRM.ComboboxItem)(cbIndents.SelectedItem)).Value.ToString();

                if (cbEcode.SelectedIndex > 0)
                 intEcode = Convert.ToInt32(((SSCRM.ComboboxItem)(cbEcode.SelectedItem)).Value.ToString());

                gvIndentDetails.Rows.Clear();
                DataTable dt = objIndent.DeliveryChallanIndentDetails_Get(CommonData.CompanyCode, strArrBranchCode[0], Convert.ToInt32(strIndentNo),intEcode).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int intRow = 0; intRow < dt.Rows.Count; intRow++)
                    {

                        gvIndentDetails.Rows.Add();
                        gvIndentDetails.Rows[intRow].Cells["SLNO"].Value = intRow + 1;
                        gvIndentDetails.Rows[intRow].Cells["ProductId"].Value = dt.Rows[intRow]["product_id"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["Category"].Value = dt.Rows[intRow]["category_name"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["Product"].Value = dt.Rows[intRow]["product_name"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["BatchNo"].Value = dt.Rows[intRow]["BatchNo"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["IndQty"].Value = dt.Rows[intRow]["IndQty"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["IssQty"].Value = dt.Rows[intRow]["IssQty"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["RATE"].Value = dt.Rows[intRow]["RATE"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["Amount"].Value = dt.Rows[intRow]["TotProductValue"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["IndentNo"].Value = dt.Rows[intRow]["indent_number"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["IndentSLNo"].Value = dt.Rows[intRow]["indent_sl_no"].ToString();
                       
                       
                    }
                }
                dt = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objIndent = null;
            }

        }

        private void cbBranches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBranches.SelectedIndex > -1)
            {
                cbEcode.Items.Clear();
                //cbIndents.Items.Clear();
                FillBranchGroupData(0);
                //FillBranchIndent();
                //FillBranchIndentDetails();
            }
        }

        private void FillDriverEmployeeData()
        {
            objIndent = new IndentDB();

            try
            {
                cbDriverEcode.DataSource = null;
                cbDriverEcode.Items.Clear();
                DataTable dtEcode = objIndent.DCOtherEmployeeList_Get().Tables[0];
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
                MessageBox.Show(ex.Message, "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objIndent = null;
            }

        }

        private string NewTransactionNumber()
        {
            string sTranNo = string.Empty;
            objIndent = new IndentDB();
            try
            {
                sTranNo = objIndent.GenerateNewDCTranNo(CommonData.CompanyCode, CommonData.BranchCode, sForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delivery challan", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objData = null;
            }

            return sTranNo;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int intSave = 0;
            string strCommand = "";
            objSQLDB = new SQLDB();

            if (CheckData())
            {
                try
                {
                    if (chkCancelDc.Checked == true)
                    {
                        if (IsModify == false)
                        {
                            strCommand = "INSERT INTO SP_DC_HEAD(SPDH_COMPANY_CODE " +
                                                              ", SPDH_STATE_CODE " +
                                                              ", SPDH_BRANCH_CODE " +
                                                              ", SPDH_DOCUMENT_MONTH " +
                                                              ", SPDH_REFERENCE_NUMBER " +
                                                              ", SPDH_FIN_YEAR " +
                                                              ", SPDH_TRN_TYPE " +
                                                              ", SPDH_TRN_NUMBER " +
                                                              ", SPDH_TRN_DATE " +
                                                              ", SPDH_VEHICLE_SOURCE " +
                                                              ", SPDH_STATUS " +
                                                              ", SPDH_REMARKS " +
                                                              ",SPDH_CREATED_BY " +
                                                              ",SPDH_CREATED_DATE " +
                                                              ")VALUES " +
                                                              "('" + CommonData.CompanyCode +
                                                              "','" + CommonData.StateCode +
                                                              "','" + CommonData.BranchCode +
                                                              "','" + CommonData.DocMonth +
                                                              "','" + txtReferenceNo.Text.ToString() +
                                                              "','" + sFinYear +
                                                              "','" + cbTransactionType.Text.ToString() +
                                                              "','" + txtTransactionNo.Text.ToString() +
                                                              "','" + Convert.ToDateTime(meTransactionDate.Value).ToString("dd/MMM/yyy") +
                                                              "','" + cbVehicleType.Text.ToString() +
                                                              "','CANCELLED','" + txtRemarks.Text.ToString() +
                                                              "','" + CommonData.LogUserId +
                                                              "',getdate())";
                        }
                        else if (IsModify == true)
                        {
                            strCommand = " UPDATE SP_DC_HEAD SET SPDH_DOCUMENT_MONTH='" + txtDocMonth.Text.ToString() +
                                        "', SPDH_FIN_YEAR='" + sFinYear +
                                        "', SPDH_TRN_TYPE='" + cbTransactionType.Text.ToString() +
                                        "', SPDH_TRN_DATE='" + Convert.ToDateTime(meTransactionDate.Value).ToString("dd/MMM/yyy") +
                                        "', SPDH_STATUS='CANCELLED" +
                                        "', SPDH_STATE_CODE='" + CommonData.StateCode +
                                //"', SPDH_DOCUMENT_MONTH='" + txtDocMonth.Text.ToUpper() +
                                        "', SPDH_REMARKS='" + txtRemarks.Text.ToString() +
                                        "', SPDH_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                                        "', SPDH_REFERENCE_NUMBER='" + txtReferenceNo.Text.ToString() +
                                        "', SPDH_LAST_MODIFIED_DATE=getdate() " +
                                        " WHERE SPDH_COMPANY_CODE='" + CommonData.CompanyCode +
                                        "' AND SPDH_BRANCH_CODE='" + CommonData.BranchCode +
                                        "'AND SPDH_FIN_YEAR='" + sFinYear +
                                        "' AND SPDH_TRN_NUMBER='" + txtTransactionNo.Text.ToString() + "'";

                            strCommand += " DELETE from SP_DC_DETL" +
                                            " WHERE spdd_company_code='" + CommonData.CompanyCode +
                                            "' AND spdd_branch_code='" + CommonData.BranchCode +
                                            "' AND spdd_trn_number='" + txtTransactionNo.Text +
                                            "' AND spdd_fin_year='" + sFinYear + "'";
                        }


                        if (strCommand.Length > 10)
                        {
                            intSave = objSQLDB.ExecuteSaveData(strCommand);
                        }

                        if (intSave > 0)
                        {
                            IsModify = false;
                            MessageBox.Show("DC data saved.\nTran No: " + txtTransactionNo.Text, "Delivery challan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm(this);
                            txtTransactionNo.Text = NewTransactionNumber().ToString();
                            chkCancelDc.Checked = false;
                        }
                        else
                        {
                            MessageBox.Show("DC data not saved.", "Delivery Challan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                    }
                    else
                    {

                        if (SaveDCHeadData() > 0)
                            intSave = SaveDCDetlData();
                        else
                        {
                            string strSQL = "DELETE from SP_DC_HEAD" +
                                        " WHERE SPIH_COMPANY_CODE='" + CommonData.CompanyCode +
                                        "' AND SPIH_BRANCH_CODE='" + CommonData.BranchCode +
                                        "' AND SPDH_TRN_NUMBER='" + txtTransactionNo.Text +
                                        "' AND SPIH_FIN_YEAR='" + sFinYear + "'";
                            objSQLDB = new SQLDB();
                            int intDel = objSQLDB.ExecuteSaveData(strSQL);
                            objSQLDB = null;
                        }


                        if (intSave > 0)
                        {
                            IsModify = false;
                            MessageBox.Show("DC data saved.\nTran No: " + txtTransactionNo.Text, "Delivery challan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm(this);
                            txtTransactionNo.Text = NewTransactionNumber().ToString();
                            for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
                            {
                                gvIndentDetails.Rows[i].Cells["IssQty"].Value = "0";
                                //gvIndentDetails.Rows[i].Cells["IssQty"].Value = "";
                            }
                            //gvIndentDetails.Rows.Clear();

                        }
                        else
                        {
                            MessageBox.Show("DC data not saved.", "Delivery Challan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                    }
                }



                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

            }


            //int intSave = 0;
            //try
            //{
            //    if (CheckData())
            //    {
            //        if (SaveDCHeadData() > 0)
            //            intSave = SaveDCDetlData();
            //        else
            //        {
            //            string strSQL = "DELETE from SP_DC_HEAD" +
            //                        " WHERE SPIH_COMPANY_CODE='" + CommonData.CompanyCode +
            //                        "' AND SPIH_BRANCH_CODE='" + CommonData.BranchCode +
            //                        "' AND SPDH_TRN_NUMBER='" + txtTransactionNo.Text +
            //                        "' AND SPIH_FIN_YEAR='" + sFinYear + "'";
            //            objSQLDB = new SQLDB();
            //            int intDel = objSQLDB.ExecuteSaveData(strSQL);
            //            objSQLDB = null;
            //        }

            //        if (intSave > 0)
            //        {
            //            IsModify = false;
            //            MessageBox.Show("DC data saved.\nTran No: " + txtTransactionNo.Text, "Delivery challan", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            ClearForm(this);
            //            txtTransactionNo.Text = NewTransactionNumber().ToString();
            //            for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
            //            {
            //                gvIndentDetails.Rows[i].Cells["IssQty"].Value = "0";
            //                //gvIndentDetails.Rows[i].Cells["IssQty"].Value = "";
            //            }
            //            //gvIndentDetails.Rows.Clear();

            //        }
            //        else
            //        {
            //            MessageBox.Show("DC data not saved.", "Delivery Challan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        }

            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message,"DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //}
            //finally
            //{
              
            //}
        }

        private int SaveDCHeadData()
        {
            int intSave = 0;
            string strSQL = string.Empty;
            objSQLDB = new SQLDB();
            string strDriver = string.Empty;
            string ecode = string.Empty;

            try
            {
                if (cbVehicleType.Text == "OWN")
                    strDriver = cbDriverEcode.Text.ToString();
                else
                    strDriver = txtDriverNo.Text.ToString();
                txtTripLRNo.Text = txtTripLRNo.Text.Replace(" ", "");
                txtVehicleNo.Text = txtVehicleNo.Text.Replace(" ", "");
                
                if (txtTotalFreight.Text.ToString().Trim().Length == 0)
                    txtTotalFreight.Text = "0.00";
                if (txtAdvancePaid.Text.ToString().Trim().Length == 0)
                    txtAdvancePaid.Text = "0.00";
                if (txtToPay.Text.ToString().Trim().Length == 0)
                    txtToPay.Text = "0.00";
                if (txtLoadingCharges.Text.ToString().Trim().Length == 0)
                    txtLoadingCharges.Text = "0.00";
                if (cbTransactionType.SelectedValue.ToString() != "SP2BR")
                    ecode = "0";
                else
                    ecode = ((SSCRM.ComboboxItem)(cbEcode.SelectedItem)).Value.ToString();
                if (IsModify == false)
                {
                    txtTransactionNo.Text = NewTransactionNumber().ToString();
                    if (txtTransactionNo.Text.ToString().Replace(" ","").Length > 10)
                    {
                        strSQL = "INSERT INTO SP_DC_HEAD " +
                             "(SPDH_COMPANY_CODE" +
                             ", SPDH_STATE_CODE" +
                             ", SPDH_BRANCH_CODE" +
                             ", SPDH_FIN_YEAR" +
                             ", SPDH_DOCUMENT_MONTH" +
                             ", SPDH_TRN_TYPE" +
                             ", SPDH_TRN_NUMBER" +
                             ", SPDH_REFERENCE_NUMBER" +
                             ", SPDH_TRN_DATE" +
                             ", SPDH_TO_BRANCH_CODE" +
                             ", SPDH_TO_eCODE" +
                             ", SPDH_TRIP_OR_LR_NUMBER" +
                             ", SPDH_VEHICLE_SOURCE" +
                             ", SPDH_VEHICLE_NUMBER" +
                             ", SPDH_TRANSPORTER_NAME" +
                             ", SPDH_DRIVER_NAME" +
                             ", SPDH_TOTAL_FREIGHT" +
                             ", SPDH_ADVANCE_PAID" +
                             ", SPDH_TO_PAY_FREIGHT" +
                             ", SPDH_LOADING_CHARGES" +
                             ", SPDH_CREATED_BY" +
                             ", SPDH_CREATED_DATE" +
                             ", SPDH_STATUS" +
                             ", SPDH_REMARKS) " +
                             "VALUES" +
                             "('" + CommonData.CompanyCode +
                             "', '" + CommonData.StateCode +
                             "', '" + CommonData.BranchCode +
                             "', '" + sFinYear +
                             "', '" + txtDocMonth.Text +
                             "', '" + cbTransactionType.Text +
                             "', '" + txtTransactionNo.Text +
                             "', '" + txtReferenceNo.Text +
                             "', '" + Convert.ToDateTime(Convert.ToDateTime(meTransactionDate.Value).ToString("dd/MM/yyyy")).ToString("dd/MMM/yyy") +
                            //"', '" + ((SSCRM.ComboboxItem)(cbBranches.SelectedItem)).Value +
                             "', '" + cbBranches.SelectedValue.ToString() +
                             "', '" + ecode +
                             "', '" + txtTripLRNo.Text +
                             "', '" + cbVehicleType.Text +
                             "', '" + txtVehicleNo.Text +
                             "', '" + txtTransporter.Text +
                             "', '" + cbDriverEcode.Text +
                             "', " + txtTotalFreight.Text +
                             ", " + txtAdvancePaid.Text +
                             ", " + txtToPay.Text +
                             ", " + txtLoadingCharges.Text +
                             ", '" + CommonData.LogUserId +
                             "', '" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyy") +
                             "','DOCUMENTED','"+ txtRemarks.Text.ToString() +"')";
                    }

                }
                else
                {
                    if (txtTransactionNo.Text.ToString().Replace(" ", "").Length > 10)
                    {
                        strSQL = "DELETE from SP_DC_DETL" +
                                    " WHERE spdd_company_code='" + CommonData.CompanyCode +
                                        "' AND spdd_branch_code='" + CommonData.BranchCode +
                                        "' AND spdd_trn_number='" + txtTransactionNo.Text +
                                        "'  AND spdd_fin_year='" + sFinYear + "'";

                        int intRec = objSQLDB.ExecuteSaveData(strSQL);

                        strSQL = "UPDATE SP_DC_HEAD " +
                                    "SET SPDH_TRN_TYPE ='" + cbTransactionType.Text +
                                    "', SPDH_TRN_DATE ='" + Convert.ToDateTime(meTransactionDate.Value).ToString("dd/MMM/yyy") +
                                    "', SPDH_TO_BRANCH_CODE='" + cbBranches.SelectedValue.ToString() +
                                    "', SPDH_TO_eCODE=" + ecode +
                                    ", SPDH_TRIP_OR_LR_NUMBER='" + txtTripLRNo.Text +
                                    "', SPDH_DOCUMENT_MONTH='" + txtDocMonth.Text.ToUpper() +
                                    "', SPDH_STATUS='DOCUMENTED" +
                                    "', SPDH_STATE_CODE='" + CommonData.StateCode +
                                    "', SPDH_VEHICLE_SOURCE='" + cbVehicleType.Text +
                                    "', SPDH_VEHICLE_NUMBER='" + txtVehicleNo.Text +
                                    "', SPDH_TRANSPORTER_NAME='" + txtTransporter.Text +
                                    "', SPDH_DRIVER_NAME='" + cbDriverEcode.Text +
                                    "', SPDH_TOTAL_FREIGHT=" + txtTotalFreight.Text +
                                    ", SPDH_ADVANCE_PAID=" + txtAdvancePaid.Text +
                                    ", SPDH_TO_PAY_FREIGHT=" + txtToPay.Text +
                                    ", SPDH_LOADING_CHARGES=" + txtLoadingCharges.Text +
                                 ", SPDH_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                                 "', SPDH_LAST_MODIFIED_DATE='" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyy") +
                                 "' WHERE SPDH_TRN_NUMBER = '" + txtTransactionNo.Text +
                                 "' AND SPDH_BRANCH_CODE='" + CommonData.BranchCode +
                                 "' AND SPDH_FIN_YEAR='" + sFinYear +
                                 "' AND SPDH_COMPANY_CODE='" + CommonData.CompanyCode.ToString() + "'";
                    }

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

        private int SaveDCDetlData()
        {
            int intSave = 0;
            string strSQL = string.Empty;
            StringBuilder sbSQL = new StringBuilder();
            objSQLDB = new SQLDB();
            try
            {
                strSQL = "DELETE from SP_DC_DETL" +
                                " WHERE spdd_company_code='" + CommonData.CompanyCode +
                                    "' AND spdd_branch_code='" + CommonData.BranchCode +
                                    "' AND spdd_trn_number='" + txtTransactionNo.Text +
                                    "' AND spdd_fin_year='" + sFinYear + "'";

                intSave = objSQLDB.ExecuteSaveData(strSQL);
                int iSLNO = 0;
                for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
                {
                    if (gvIndentDetails.Rows[i].Cells["TotalQty"].Value.ToString() != "" 
                        && gvIndentDetails.Rows[i].Cells["TotalQty"].Value.ToString() != "0" 
                        && gvIndentDetails.Rows[i].Cells["Amount"].Value.ToString() != "")
                    {
                        iSLNO++;
                        sbSQL.Append("INSERT INTO SP_DC_DETL(SPDD_COMPANY_CODE, SPDD_STATE_CODE, "+
                                    "SPDD_BRANCH_CODE, SPDD_FIN_YEAR, SPDD_DOCUMENT_MONTH"+
                                    ", SPDD_TRN_TYPE, SPDD_TRN_NUMBER, SPDD_BATCH_NO, SPDD_GRNISS_NUMBER"+
                                    ", SPDD_SL_NO, SPDD_PRODUCT_ID, SPDD_ISS_QTY, SPDD_ISS_DAMG_QTY" +
                                    ", SPDD_ISS_RATE, SPDD_ISS_AMT, SPDD_INDENT_NUMBER, SPDD_INDENT_SLNO)" +
                                    " VALUES ('" + CommonData.CompanyCode + "', '" + CommonData.StateCode + 
                                    "', '" + CommonData.BranchCode + 
                                    "', '" + sFinYear + "', '" + txtDocMonth.Text +
                                    "', '" + cbTransactionType.Text + "', '" +  txtTransactionNo.Text +
                                    "', '" + gvIndentDetails.Rows[i].Cells["BatchNo"].Value +
                                    "', 'GRNNO', " + iSLNO +
                                    ", '" + gvIndentDetails.Rows[i].Cells["ProductId"].Value + 
                                    "', " + gvIndentDetails.Rows[i].Cells["TotalQty"].Value + 
                                    ", " + gvIndentDetails.Rows[i].Cells["DmgQty"].Value + 
                                    ", " + gvIndentDetails.Rows[i].Cells["Rate"].Value + 
                                    ", " + gvIndentDetails.Rows[i].Cells["Amount"].Value +
                                    ", " + gvIndentDetails.Rows[i].Cells["IndentNo"].Value + 
                                    ", " + gvIndentDetails.Rows[i].Cells["IndentSLNo"].Value + "); ");

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

        private void FillTransactionDetl(DataTable dt)
        {
            objIndent = new IndentDB();
            try
            {
                gvIndentDetails.Rows.Clear();
                cbIndents.SelectedIndex = -1;
                if (dt.Rows.Count > 0)
                {
                    for (int intRow = 0; intRow < dt.Rows.Count; intRow++)
                    {
                       // if(cbIndents.SelectedIndex==-1 && dt.Rows[intRow]["IndentNo"].ToString()!="0")
                       //     cbIndents.SelectedIndex = objGeneral.GetComboBoxSelectedIndex(dt.Rows[intRow]["IndentNo"].ToString(), cbIndents);

                        gvIndentDetails.Rows.Add();
                        gvIndentDetails.Rows[intRow].Cells["SLNO"].Value = intRow + 1;
                        gvIndentDetails.Rows[intRow].Cells["Category"].Value = dt.Rows[intRow]["category_name"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["Product"].Value = dt.Rows[intRow]["product_name"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["BatchNo"].Value = dt.Rows[intRow]["BatchNo"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["ProductId"].Value = dt.Rows[intRow]["ProductId"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["IndQty"].Value = dt.Rows[intRow]["IndQty"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["IssQty"].Value = dt.Rows[intRow]["IssQty"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["DmgQty"].Value = dt.Rows[intRow]["DmgQty"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["TotalQty"].Value = dt.Rows[intRow]["TotalQty"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["RATE"].Value = dt.Rows[intRow]["IssRate"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["Amount"].Value = dt.Rows[intRow]["IssAmt"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["IndentNo"].Value = dt.Rows[intRow]["IndentNo"].ToString();
                        gvIndentDetails.Rows[intRow].Cells["IndentSLNo"].Value = dt.Rows[intRow]["IndentSLNo"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objIndent = null;
            }
        }

        private bool CheckData()
        {
            bool blValue = true;

            if (Convert.ToString(txtTransactionNo.Text).Length == 0)
            {
                MessageBox.Show("Enter Transaction number!", "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtTransactionNo.Focus();
                return blValue;
            }
            if (Convert.ToString(txtReferenceNo.Text).Length == 0)
            {
                MessageBox.Show("Enter Reference No", "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtReferenceNo.Focus();
                return blValue;
            }

            if (Convert.ToInt32(Convert.ToDateTime(meTransactionDate.Value).ToString("yyyy")) < 1950)
            {
                MessageBox.Show("Enter valid  Date !", "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                meTransactionDate.CausesValidation = true;
                meTransactionDate.Focus();
                return blValue;
            }
            if (Convert.ToDateTime(Convert.ToDateTime(meTransactionDate.Value).ToString("dd/MM/yyyy")) > Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyyy")))
            {
                MessageBox.Show("Date should be less than to day", "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                meTransactionDate.CausesValidation = true;
                blValue = false;
                meTransactionDate.Focus();
                return blValue;
            }

            if (chkCancelDc.Checked == true)
            {
                if (txtRemarks.Text.Length <= 20)
                {
                    MessageBox.Show("Please Enter Remarks", "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    blValue = false;
                    txtRemarks.Focus();
                    return blValue;
                }
            }
            else
            {

                if (cbBranches.SelectedIndex == -1)
                {
                    MessageBox.Show("Select To Branch!", "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    blValue = false;
                    cbBranches.Focus();
                    return blValue;
                }
                if (cbTransactionType.SelectedValue.ToString() == "SP2BR")
                {
                    if (cbEcode.SelectedIndex == -1)
                    {
                        MessageBox.Show("Enter GL/GC number!", "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        blValue = false;
                        cbEcode.Focus();
                        return blValue;
                    }
                }
                if (Convert.ToString(txtTotalFreight.Text).Trim().Length == 0)
                {
                    MessageBox.Show("Enter Freight !", "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    blValue = false;
                    txtTotalFreight.Focus();
                    return blValue;
                }
                if (cbVehicleType.Text != "BYHAND")
                {
                    if (Convert.ToString(txtTripLRNo.Text).Trim().Length == 0 && cbVehicleType.SelectedText != "BYHAND")
                    {
                        MessageBox.Show("Enter Trip / LR Number !", "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        blValue = false;
                        txtTripLRNo.Focus();
                        return blValue;
                    }

                    if (Convert.ToString(txtVehicleNo.Text).Trim().Length == 0)
                    {
                        MessageBox.Show("Enter VehicleNo !", "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        blValue = false;
                        txtVehicleNo.Focus();
                        return blValue;
                    }
                }
                bool blInvDtl = false;
                for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
                {
                    if (Convert.ToString(gvIndentDetails.Rows[i].Cells["IssQty"].Value) != "" && Convert.ToString(gvIndentDetails.Rows[i].Cells["Amount"].Value) != "")
                    {
                        blInvDtl = true;
                    }

                }

                if (blInvDtl == false)
                {
                    blValue = false;
                    MessageBox.Show("Enter product quantity/Amount", "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }


            return blValue;

        }

        private void gvProductDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex >= 5 && e.ColumnIndex <= 8)
                {
                    double gQty = 0, dQty = 0, tQty = 0;
                    if (gvIndentDetails.Rows[e.RowIndex].Cells["IssQty"].Value.ToString() != "")
                        gQty = Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["IssQty"].Value);
                    if (gvIndentDetails.Rows[e.RowIndex].Cells["DmgQty"].Value.ToString() != "")
                        dQty = Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["DmgQty"].Value);
                    tQty = gQty + dQty;
                    gvIndentDetails.Rows[e.RowIndex].Cells["TotalQty"].Value = tQty.ToString("0.0");
                    //if (Convert.ToString(gvIndentDetails.Rows[e.RowIndex].Cells["IssQty"].Value) != "")
                    //{
                    if (tQty >= 0 && Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["Rate"].Value) >= 0)
                    {
                        gvIndentDetails.Rows[e.RowIndex].Cells["Amount"].Value = gQty * (Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["Rate"].Value));
                        gvIndentDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["Amount"].Value).ToString("f");

                    }
                    //}
                    //else
                    //    gvIndentDetails.Rows[e.RowIndex].Cells["Amount"].Value = string.Empty;
                    CalculateTotals();
                }
                try
                {
                    DataGridView dgv = (DataGridView)sender;
                    dgv.EndEdit();
                    if (e.ColumnIndex == 5)
                    {
                        if (Convert.ToString(gvIndentDetails.Rows[e.RowIndex].Cells["IssQty"].Value) != "" && Convert.ToString(gvIndentDetails.Rows[e.RowIndex].Cells["IndentNo"].Value) != "0")
                        {
                            if (Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["IssQty"].Value) > Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["IndQty"].Value))
                            {
                                //MessageBox.Show("Issue quantity should be less than or equal to indent quantity!", "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //e.Cancel = true;
                            }
                        }
                    }
                }
                catch
                {
                }
            }
           
        }

        private void CalculateTotals()
        {
            double amt = 0;
            double totalProducts = 0;
            double totalQty = 0;
            if (gvIndentDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
                {
                    if (gvIndentDetails.Rows[i].Cells["TotalQty"].Value.ToString() != ""
                        && gvIndentDetails.Rows[i].Cells["Rate"].Value.ToString() != ""
                        && gvIndentDetails.Rows[i].Cells["Amount"].Value.ToString() != ""
                        && gvIndentDetails.Rows[i].Cells["Category"].Value.ToString() != "PACKING MATERIAL")
                    {
                        amt += Convert.ToDouble(gvIndentDetails.Rows[i].Cells["Amount"].Value);
                        totalQty += Convert.ToDouble(gvIndentDetails.Rows[i].Cells["TotalQty"].Value);
                    }
                }
                
            }
            totalProducts = gvIndentDetails.Rows.Count;
            txtDcAmt.Text = Convert.ToDouble(amt).ToString("f");
            txtProducts.Text = Convert.ToDouble(totalProducts).ToString("f");
            txtQty.Text = Convert.ToDouble(totalQty).ToString("f");
        }
               
        private void FillTransactionData(string TranNO)
        {
            objIndent = new IndentDB();
            Hashtable ht = new Hashtable();
            try
            {

                ht = objIndent.GetDeliveryChallanData("", TranNO);
                DataTable dtH = (DataTable)ht["Head"];
                DataTable dtD = (DataTable)ht["Detail"];
                FillTransactionHead(dtH, dtD);
                dtH = null;
                dtD = null;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                objIndent = null;
                ht = null;
               
            }
        }

        private bool FillTransactionHead(DataTable dtHead, DataTable dtDetl)
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
                        
                    }
                    IsModify = true;
                    strTransactionNo = dtHead.Rows[0]["TrnNumber"].ToString();
                    txtTransactionNo.Text = dtHead.Rows[0]["TrnNumber"] + "";
                    txtReferenceNo.Text = dtHead.Rows[0]["ReferanceNumber"] + "";
                    meTransactionDate.Value = Convert.ToDateTime(Convert.ToDateTime(dtHead.Rows[0]["TrnDate"]).ToString("dd/MM/yyyy"));
                    txtTotalFreight.Text = dtHead.Rows[0]["TotalFreight"] + "";
                    txtAdvancePaid.Text = dtHead.Rows[0]["AdvancePaid"] + "";
                    txtToPay.Text = dtHead.Rows[0]["ToPayFreight"] + "";
                    txtLoadingCharges.Text = dtHead.Rows[0]["loading_charges"] + "";
                    cbTransactionType.Text = dtHead.Rows[0]["TrnType"] + "";
                    if (dtHead.Rows[0]["ToBranchCode"].ToString() != "")
                    {
                        strToBranchCode = dtHead.Rows[0]["ToBranchCode"] + "";
                        cbBranches.SelectedValue = strToBranchCode;
                    }
                    txtDocMonth.Text = dtHead.Rows[0]["DocMonth"] + "";
                    sFinYear = dtHead.Rows[0]["FinYear"] + "";
                    FillBranchGroupData(0);
                    strECode = dtHead.Rows[0]["ToEcode"] + "";
                    cbEcode.SelectedIndex = objGeneral.GetComboBoxSelectedIndex(strECode, cbEcode);

                    if (dtHead.Rows[0]["Status"].ToString().Equals("CANCELLED"))
                    {
                        chkCancelDc.Checked = true;
                    }
                    else
                    {
                        chkCancelDc.Checked = false;
                    }

                    txtRemarks.Text = dtHead.Rows[0]["Remarks"].ToString();

                    cbVehicleType.SelectedValue = dtHead.Rows[0]["VehicleSource"] + "";

                    if (cbVehicleType.Text == "OWN")
                    {
                        cbDriverEcode.Visible = true;
                        cbDriverEcode.Text = dtHead.Rows[0]["DriverName"] + "";
                    }
                    else
                    {
                        cbDriverEcode.Visible =false;
                        txtDriverNo.Text = dtHead.Rows[0]["DriverName"] + "";
                    }

                    if (chkCancelDc.Checked == false)
                    {

                        txtTripLRNo.Text = Convert.ToString(dtHead.Rows[0]["TripLRNumber"]);
                        txtVehicleNo.Text = Convert.ToString(dtHead.Rows[0]["VehicleNumber"]);

                        txtTransporter.Text = dtHead.Rows[0]["TransporterName"] + "";
                        FillTransactionDetl(dtDetl);
                    }
                    
                    isData = true;
                }
                else
                {
                    btnSave.Enabled = true;
                    btnDelete.Enabled = true;
                    IsModify = false;
                    txtDocMonth.Text = CommonData.DocMonth;                    
                    sFinYear = CommonData.FinancialYear;
                    gvIndentDetails.Rows.Clear();
                    chkCancelDc.Checked = false;
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
                if (Convert.ToDateTime(Convert.ToDateTime(meTransactionDate.Value).ToString("dd/MM/yyyy")) < Convert.ToDateTime(CommonData.DocFDate) ||
                        Convert.ToDateTime(Convert.ToDateTime(meTransactionDate.Value).ToString("dd/MM/yyyy")) > Convert.ToDateTime(CommonData.DocTDate))
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
            if (char.IsNumber(e.KeyChar) == false)
                e.Handled = true;
            if (cbTransactionType.SelectedValue.ToString() == "SP2BR" && txtTransactionNo.SelectionStart < 16)
                e.Handled = true;
            else if (cbTransactionType.SelectedValue.ToString() != "SP2BR" && txtTransactionNo.SelectionStart < 18)
                e.Handled = true;
            else if (e.KeyChar == 8)
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
                        gvIndentDetails.CurrentCell = textBoxCell;
                        dgv.BeginEdit(true);
                    }
                }
                if (e.ColumnIndex == gvIndentDetails.Columns["Delete"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        DataGridViewRow dgvr = gvIndentDetails.Rows[e.RowIndex];
                        gvIndentDetails.Rows.Remove(dgvr);
                        OrderSlNo();
                    }
                }
            }
        }

        private void OrderSlNo()
        {
            if (gvIndentDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
                {
                    gvIndentDetails.Rows[i].Cells["SLNO"].Value = i + 1;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTransactionNo.Text.Length > 0 && IsModify == true)
                {
                    Security objSecur = new Security();
                    if (objSecur.CanModifyDataUserAsPerBackDays(Convert.ToDateTime(meTransactionDate.Value)) == false)
                    {
                        MessageBox.Show("You cannot manipulate backdays data!\n If you want to modify, Contact to IT-Department", "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTransactionNo.Focus();
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Do you want to Delete " + txtTransactionNo.Text + " Transaction ?",
                                               "CRM DC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            objSQLDB = new SQLDB();

                            string strDelete = " DELETE from SP_DC_DETL " +
                                                " WHERE SPDD_COMPANY_CODE='" + CommonData.CompanyCode +
                                                "' AND SPDD_BRANCH_CODE='" + CommonData.BranchCode +
                                                "' AND SPDD_TRN_NUMBER='" + txtTransactionNo.Text +
                                                "' AND SPDD_FIN_YEAR='" + CommonData.FinancialYear + "'";

                            strDelete += "DELETE from SP_DC_HEAD" +
                                                " WHERE SPDH_COMPANY_CODE='" + CommonData.CompanyCode +
                                                "' AND SPDH_BRANCH_CODE='" + CommonData.BranchCode +
                                                "' AND SPDH_TRN_NUMBER='" + txtTransactionNo.Text +
                                                "' AND SPDH_FIN_YEAR='" + CommonData.FinancialYear + "' ";

                            int intRec = objSQLDB.ExecuteSaveData(strDelete);
                            if (intRec > 0)
                            {
                                IsModify = false;
                                MessageBox.Show("Tran No: " + txtTransactionNo.Text + " Deleted!");
                                ClearForm(this);
                                gvIndentDetails.Rows.Clear();
                                txtTransactionNo.Text = NewTransactionNumber().ToString();
                            }

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Enter Tran Number.", "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
              
        private void cbEcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEcode.SelectedIndex > -1)
            {
                cbIndents.Items.Clear();
                FillBranchIndent();
               // FillBranchIndentDetails();
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
            for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
            {
                if (gvIndentDetails.Rows[i].Cells["Amount"].Value.ToString() != "")
                {  
                    if(Convert.ToDouble(gvIndentDetails.Rows[i].Cells["Amount"].Value.ToString())>=1)
                        dbInvAmt += Convert.ToDouble(gvIndentDetails.Rows[i].Cells["Amount"].Value);

                }

            }
            return dbInvAmt;
        }

        private void btnClearProd_Click(object sender, EventArgs e)
        {
            gvIndentDetails.Rows.Clear();
        }

        private void cbIndents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbIndents.SelectedIndex > -1)
            {
                FillBranchIndentDetails();
            }
        }

        private void cbVehicleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbVehicleType.Text == "OWN")
            //    cbDriverEcode.Visible = true;
            //else if (cbVehicleType.Text == "BYHAND")
            //{
            //    txtTripLRNo.Enabled = false;
            //    txtDriverNo.Enabled = false;
            //    txtVehicleNo.Enabled = false;
            //    txtTransporter.Enabled = false;
            //    cbDriverEcode.Visible = false;
            //}
            //else
            //    cbDriverEcode.Visible = false;
            if (cbVehicleType.Text == "BYHAND")
            {
                txtTripLRNo.Enabled = false;
                txtDriverNo.Enabled = false;
                txtVehicleNo.Enabled = false;
                txtTransporter.Enabled = false;
                //cbDriverEcode.Visible = false;
            }
            else
            {
                txtTripLRNo.Enabled = true;
                txtDriverNo.Enabled = true;
                txtVehicleNo.Enabled = true;
                txtTransporter.Enabled = true;
            }
        }

        private void cbDriverEcode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            if (cbTransactionType.SelectedValue.ToString() == "SP2BR")
            {
                if (cbBranches.SelectedValue != null)
                {
                    IndentProductSearch PSearch = new IndentProductSearch("SPDC", cbBranches.SelectedValue.ToString());
                    PSearch.objDeliveryChallan = this;
                    PSearch.ShowDialog();
                }
                    CalculateTotals();
                
            }
            else
            {
                IndentProductSearch PSearch = new IndentProductSearch("SPDCST",CommonData.BranchCode);
                PSearch.objDeliveryChallan = this;
                PSearch.ShowDialog();
                CalculateTotals();
            }
            
        }

        private void cbTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTransactionType.SelectedIndex > -1 && TranTypeLoad == false)
            {
                if (cbTransactionType.SelectedValue.ToString() == "SP2BR")
                {
                    cbBranches.Enabled = true;
                    cbIndents.Enabled = true;
                    cbEcode.Enabled = true;
                    txtEcodeSearch.Enabled = true;
                    txtTransactionNo.Text = NewTransactionNumber().ToString();
                    FillBranchData();
                    //cbBranches.SelectedIndex = 0;
                }
                else if (cbTransactionType.SelectedValue.ToString() == "SP2SP")
                {
                    cbBranches.Enabled = true;
                    cbIndents.Enabled = true;
                    cbEcode.Enabled = false;
                    txtEcodeSearch.Enabled = false;
                    txtTransactionNo.Text = NewTransactionNumber().ToString();
                    FillBranchData();
                    //cbBranches.SelectedIndex = 0;
                }
                else if (cbTransactionType.SelectedValue.ToString() == "SP2PU")
                {
                    cbBranches.Enabled = true;
                    cbIndents.Enabled = true;
                    cbEcode.Enabled = false;
                    txtEcodeSearch.Enabled = false;
                    txtTransactionNo.Text = NewTransactionNumber().ToString();
                    FillBranchData();
                    //cbBranches.SelectedIndex = 0;
                }
                else if (cbTransactionType.SelectedValue.ToString() == "SP2OL")
                {
                    cbBranches.Enabled = true;
                    cbIndents.Enabled = true;
                    cbEcode.Enabled = false;
                    txtEcodeSearch.Enabled = false;
                    txtTransactionNo.Text = NewTransactionNumber().ToString();
                    FillBranchData();
                    if(cbBranches.Items.Count != 0)
                        cbBranches.SelectedIndex = 0;
                }
                else
                {
                    cbBranches.Enabled = false;
                    cbIndents.Enabled = false;
                    cbEcode.Enabled = false;
                    txtTransactionNo.Text = NewTransactionNumber().ToString();
                }
            }
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

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text.Trim().Length > 4)
            {

                FillBranchGroupData(Convert.ToInt32(txtEcodeSearch.Text.ToString()));
            }
            else
                FillBranchGroupData(0);
        }

        private void txtTransactionNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(txtTransactionNo.Text).Length > 15)
                {

                    FillTransactionData(txtTransactionNo.Text.ToString());
                    CalculateTotals();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void txtReferenceNo_Validated(object sender, EventArgs e)
        {
            if (txtReferenceNo.Text.Trim().Length > 0)
            {
                FillDCDetails(txtReferenceNo.Text.ToString());
            }
        }

        private void FillDCDetails(string p)
        {
            objStockPointDB = new StockPointDB();
            DataSet sDCInfo = null;
            try
            {
                sDCInfo = objStockPointDB.SPDCHeadDetails_Get(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth, txtReferenceNo.Text.ToString(),cbTransactionType.SelectedValue.ToString());
                DataTable sDCHead = sDCInfo.Tables[0];
                if (sDCHead.Rows.Count > 0)
                {
                    FillDCHeadData(sDCHead);
                    
                }
                else
                {
                    sDCHead = null;
                    //ClearForm(this);
                    txtToPay.Text = "";
                    txtTotalFreight.Text = "";
                    txtAdvancePaid.Text = "";
                    txtLoadingCharges.Text = "";
                    txtVehicleNo.Text = "";
                    txtDriverNo.Text = "";
                    txtTransporter.Text = "";
                    txtTripLRNo.Text = "";
                    IsModify = false;
                    meTransactionDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
                    //gvIndentDetails.Rows.Clear();
                    txtTransactionNo.Text = NewTransactionNumber().ToString();
                    IsModify = false;

                }
                CalculateTotals();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillDCHeadData(DataTable sDCHead)
        {
            if (sDCHead.Rows.Count > 0)
            {
                cbTransactionType.SelectedValue = sDCHead.Rows[0]["SPDH_TRN_TYPE"] + "";
                txtTransactionNo.Text = sDCHead.Rows[0]["SPDH_TRN_NUMBER"] + "";
            }
        }

        private void txtTripLRNo_Validated(object sender, EventArgs e)
        {
            txtTripLRNo.Text = txtTripLRNo.Text.Replace(" ", "");
            if (txtTripLRNo.Text.Length > 0)
            {
                string sqlText = "SELECT top 1 SPDH_TRIP_OR_LR_NUMBER TripLRNo,SPDH_VEHICLE_SOURCE VehcSource" +
                                    ",SPDH_VEHICLE_NUMBER VehNo,SPDH_DRIVER_NAME DriverName,SPDH_TRANSPORTER_NAME Transport " +
                                    "FROM SP_DC_HEAD WHERE SPDH_COMPANY_CODE = '"+CommonData.CompanyCode+
                                    "' and SPDH_BRANCH_CODE = '"+CommonData.BranchCode+"' " +
                                    "AND SPDH_DOCUMENT_MONTH = '"+txtDocMonth.Text+"' AND SPDH_TRIP_OR_LR_NUMBER = '"+txtTripLRNo.Text+"'";
                objSQLDB = new SQLDB();
                DataTable dt = new DataTable();
                try
                {
                    dt = objSQLDB.ExecuteDataSet(sqlText).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtTripLRNo.Text = dt.Rows[0]["TripLRNo"].ToString();
                        cbVehicleType.Text = dt.Rows[0]["VehcSource"].ToString();
                        txtVehicleNo.Text = dt.Rows[0]["VehNo"].ToString();
                        txtDriverNo.Text = dt.Rows[0]["DriverName"].ToString();
                        cbDriverEcode.Text = dt.Rows[0]["DriverName"].ToString();
                        txtTransporter.Text = dt.Rows[0]["Transport"].ToString();
                    }
                    else
                    {
                        txtVehicleNo.Text = "";
                        txtDriverNo.Text = "";
                        cbDriverEcode.Text = "";
                        txtTransporter.Text = "";
                    }
                }
                catch
                {

                }
                finally
                {

                }
            }
        }

        private void btnPrintEmpList_Click(object sender, EventArgs e)
        {
            CommonData.ViewReport = "SSERP_REP_MONTH_SALES_EMP_LIST_BY_BRANCH";
            childReportViewer = new ReportViewer("", cbBranches.SelectedValue.ToString(), "", txtDocMonth.Text.ToUpper(), "");
            childReportViewer.Show();
        }

      
        
    }
}
