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
    public partial class SPPackingMtrlGRN : Form
    {
        private SQLDB objSQLDB = null;
        private UtilityDB objUtilData = null;
        private ProductUnitDB objPUDB = null;
        private IndentDB objIndent = null;
        private StockPointDB objStockPointDB = null;
        private string strFormerid = string.Empty;
        private string strTransactionNo = string.Empty;
        private string strBranchCode = string.Empty;
        private string strECode = string.Empty;
        private bool blIsCellQty = true;
        private bool IsModify = false;
        private string sFinYear = "";
        private string strFromBranchCode = string.Empty;
        private bool TranTypeLoad = false;
        private InvoiceDB objInvoiceDB = null;
        private string strToBranchCode = string.Empty;
        public SPPackingMtrlGRN()
        {
            InitializeComponent();
        }

        private void DeliveryChallanPU_Load(object sender, EventArgs e)
        {
            txtDocMonth.Text = CommonData.DocMonth;
            sFinYear = CommonData.FinancialYear;
            FillTransactionType();
            FillBranchData();
            FillVehcleType();
            cbVehicleType.SelectedIndex = 0;
            dtpTranDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            txtTransactionNo.Text = NewTransactionNumber().ToString();

            //FillDriverEmployeeData();
            cbTransactionType.SelectedIndex = 0;
            //cbBranches1.SelectedIndex = 0;
            dtpTranDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            gvGRNDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                      System.Drawing.FontStyle.Regular);

            gvGRNDetails.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8,
                                                        System.Drawing.FontStyle.Bold);
            CalculateTotals();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm(this);
            IsModify = false;
            dtpTranDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            gvGRNDetails.Rows.Clear();
            txtTransactionNo.Text = NewTransactionNumber().ToString();
            IsModify = false;
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
                DataTable dt = objUtilData.dtSPPKMGRNTranType();
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
                MessageBox.Show(ex.Message,"SP GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                MessageBox.Show(ex.Message,"SP GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {

                objUtilData = null;
                Cursor.Current = Cursors.Default;
            }

        }

        private void FillBranchData()
        {
            objPUDB = new  ProductUnitDB();
            try
            {
                cbBranches1.DataSource = null;
                string brType = cbTransactionType.Text.ToString();
                //if (brType.Trim() == "PU2SP")
                //    brType = "PU2PU";
                //cbBranches1.Items.Clear();
                DataTable dtBranch = objPUDB.BranchListForDC_Get(CommonData.CompanyCode, brType).Tables[0];
                if (dtBranch.Rows.Count > 0)
                {
                    //DataView dv1 = dtBranch.DefaultView;
                    //dv1.RowFilter = " active = 'T' ";
                    //DataTable dtBR = dv1.ToTable();
                    //if (dtBR.Rows.Count > 0)
                    //{
                    //foreach (DataRow dataRow in dtBR.Rows)
                    //{
                    cbBranches1.DataSource = dtBranch;
                    cbBranches1.ValueMember = "branch_code";
                    cbBranches1.DisplayMember = "branch_name";

                    //ComboboxItem objItem = new ComboboxItem();
                    //objItem.Value = dataRow["branch_code"].ToString();
                    //objItem.Text = dataRow["branch_name"].ToString();
                    //cbBranches.Items.Add(objItem);
                    //objItem = null;
                    //}
                    cbBranches1.SelectedIndex = 0;
                    //}
                }
                dtBranch = null;
                //dv1 = null;
                //dtBR = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SP GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {

                objPUDB = null;
                Cursor.Current = Cursors.Default;
            }

        }

        private void Fill_pudc_LIST()
        {
            objPUDB = new  ProductUnitDB();
            
            try
            {
                string[] strArrBranchCode = cbBranches1.SelectedValue.ToString().Split('@');
                    //((SSCRM.ComboboxItem)(cbBranches1.SelectedItem)).Value.ToString().Split('@');
                cbDCs.DataSource = null;
                cbDCs.Items.Clear();
                DataTable dt = objPUDB.GRN_DeliveryChallanListFromPU(CommonData.CompanyCode, strArrBranchCode[0]).Tables[0];
                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dt.Rows)
                    {
                        ComboboxItem objItem = new ComboboxItem();
                        objItem.Value = dataRow["TrnNo"].ToString();
                        objItem.Text = dataRow["TrnNoDt"].ToString();
                        cbDCs.Items.Add(objItem);
                        objItem = null;
                        
                    }

                }
                dt = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SP GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objPUDB = null;
            }

        }
        private void CalculateTotals()
        {
            double amt = 0;
            double totalProducts = 0;
            double totalQty = 0;
            if (gvGRNDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvGRNDetails.Rows.Count; i++)
                {
                    if (gvGRNDetails.Rows[i].Cells["RecdQty"].Value.ToString() != "" && gvGRNDetails.Rows[i].Cells["Rate"].Value.ToString() != "" && gvGRNDetails.Rows[i].Cells["Amount"].Value.ToString() != "")
                    {
                        amt += Convert.ToDouble(gvGRNDetails.Rows[i].Cells["Amount"].Value);
                        totalQty += Convert.ToDouble(gvGRNDetails.Rows[i].Cells["RecdQty"].Value);
                    }
                }

            }
            totalProducts = gvGRNDetails.Rows.Count;
            txtDcAmt.Text = Convert.ToDouble(amt).ToString("f");
            txtProducts.Text = Convert.ToDouble(totalProducts).ToString("f");
            txtQty.Text = Convert.ToDouble(totalQty).ToString("f");
        }
        private void Fill_pudc_Details()
        {
            objPUDB = new  ProductUnitDB();
            string strDCNo = string.Empty;
            try
            {
                string strArrBranchCode = cbBranches1.SelectedValue.ToString();
                    //((SSCRM.ComboboxItem)(cbBranches1.SelectedItem)).Value.ToString();

                if(cbDCs.SelectedIndex>-1)
                    strDCNo = ((SSCRM.ComboboxItem)(cbDCs.SelectedItem)).Value.ToString();

               // FillTransactionPUHeadData(strDCNo);
                gvGRNDetails.Rows.Clear();
                DataTable dt = objPUDB.DRN_PUDeliveryChallanDetl(CommonData.CompanyCode, strArrBranchCode, strDCNo).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int intRow = 0; intRow < dt.Rows.Count; intRow++)
                    {

                        gvGRNDetails.Rows.Add();
                        gvGRNDetails.Rows[intRow].Cells["SLNO"].Value = intRow + 1;
                        gvGRNDetails.Rows[intRow].Cells["ProductId"].Value = dt.Rows[intRow]["ProductId"].ToString();
                        gvGRNDetails.Rows[intRow].Cells["Category"].Value = dt.Rows[intRow]["category_name"].ToString();
                        gvGRNDetails.Rows[intRow].Cells["Product"].Value = dt.Rows[intRow]["product_name"].ToString();
                        gvGRNDetails.Rows[intRow].Cells["BatchNo"].Value = dt.Rows[intRow]["BatchNo"].ToString();
                        gvGRNDetails.Rows[intRow].Cells["DcQty"].Value = dt.Rows[intRow]["DcQty"].ToString();
                        //if (Convert.ToDouble(dt.Rows[intRow]["RecdQty"].ToString()) > 0)
                        gvGRNDetails.Rows[intRow].Cells["RecdQty"].Value = dt.Rows[intRow]["RecdQty"].ToString();
                        //else
                        gvGRNDetails.Rows[intRow].Cells["DamageQty"].Value = dt.Rows[intRow]["DmgQty"].ToString();
                        gvGRNDetails.Rows[intRow].Cells["RATE"].Value = dt.Rows[intRow]["dcRate"].ToString();
                        gvGRNDetails.Rows[intRow].Cells["Amount"].Value = dt.Rows[intRow]["dcAmt"].ToString();
                        gvGRNDetails.Rows[intRow].Cells["DCNo"].Value = dt.Rows[intRow]["TrnNumber"].ToString();
                        gvGRNDetails.Rows[intRow].Cells["DCSLNo"].Value = dt.Rows[intRow]["SlNo"].ToString();

                       
                    }
                    FillTransactionPUHeadData(strDCNo);
                    txtAdvancePaid.ReadOnly = true;
                    txtTotalFreight.ReadOnly = true;
                    txtTripLRNo.ReadOnly = true;
                    txtVehicleNo.ReadOnly = true;
                    txtDriverNo.ReadOnly = true;
                    txtTransporter.ReadOnly = true;
                    cbVehicleType.Enabled = false;

                }
                dt = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SP GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objPUDB = null;
            }

        }

        private void cbBranches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBranches1.SelectedIndex > -1)
            {                
                cbDCs.Items.Clear();
                Fill_pudc_LIST();
            }
        }      

        private void FillTransactionPUHeadData(string TranNO)
        {
            objPUDB = new ProductUnitDB();
            try
            {
                string[] strArrBranchCode = cbBranches1.SelectedValue.ToString().Split('@');
                    //((SSCRM.ComboboxItem)(cbBranches1.SelectedItem)).Value.ToString().Split('@');
                DataTable dt = objPUDB.GRN_DeliveryChallanPUHead_Get(CommonData.CompanyCode, strArrBranchCode[0], TranNO).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtTotalFreight.Text = dt.Rows[0]["TotalFreight"] + "";
                    txtAdvancePaid.Text = dt.Rows[0]["AdvancePaid"] + "";
                    txtToPay.Text = dt.Rows[0]["ToPayFreight"] + "";
                    cbVehicleType.SelectedValue = dt.Rows[0]["VehicleSource"] + "";
                    txtDriverNo.Text = dt.Rows[0]["DriverName"] + "";
                    txtTripLRNo.Text = Convert.ToString(dt.Rows[0]["TripLRNumber"]);
                    txtVehicleNo.Text = Convert.ToString(dt.Rows[0]["VehicleNumber"]);
                    txtTransporter.Text = dt.Rows[0]["TransporterName"] + "";
                }
                dt = null;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                objPUDB = null;
               

            }
        }
        //private void FillDriverEmployeeData()
        //{
        //    objPUDB = new  ProductUnitDB();

        //    try
        //    {
        //        cbDriverEcode.DataSource = null;
        //        cbDriverEcode.Items.Clear();
        //        DataTable dtEcode = objPUDB.DCOtherEmployeeList_Get().Tables[0];
        //        if (dtEcode.Rows.Count > 0)
        //        {

        //            foreach (DataRow dataRow in dtEcode.Rows)
        //            {
        //                ComboboxItem objItem = new ComboboxItem();
        //                objItem.Value = dataRow["ENAME"].ToString();
        //                objItem.Text = dataRow["ENAME"].ToString();
        //                cbDriverEcode.Items.Add(objItem);
        //                objItem = null;
        //            }

        //        }
        //        dtEcode = null;

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "SP GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //    }
        //    finally
        //    {
        //        objPUDB = null;
        //    }

        //}

        private string NewTransactionNumber()
        {
            string sTranNo = string.Empty;
            objPUDB = new ProductUnitDB();
            try
            {
                sTranNo = objPUDB.GenerateNewSPPKMGRNTranNo(CommonData.CompanyCode, CommonData.BranchCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SP GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objPUDB = null;
            }

            return sTranNo;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int intSave = 0;
            try
            {
                if (CheckData())
                {
                    if (SaveDCHeadData() > 0)
                        intSave = SaveDCDetlData();
                    else
                    {
                        string strSQL = "DELETE from SP_PKM_GRN_HEAD" +
                                    " WHERE SPGH_COMPANY_CODE='" + CommonData.CompanyCode +
                                    "' AND SPGH_BRANCH_CODE='" + CommonData.BranchCode +
                                    "' AND SPGH_GRN_NUMBER='" + txtTransactionNo.Text +
                                    "' AND SPGH_FIN_YEAR='" + sFinYear + "'";
                        objSQLDB = new SQLDB();
                        int intDel = objSQLDB.ExecuteSaveData(strSQL);
                        objSQLDB = null;
                    }

                    if (intSave > 0)
                    {
                        cbBranches1.Enabled = true;
                        cbDCs.Enabled = true;
                        IsModify = false;
                        MessageBox.Show("GRN data saved.\n Transaction No:" + txtTransactionNo.Text.ToString(), "SP GRN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm(this);
                        txtTransactionNo.Text = NewTransactionNumber().ToString();
                        //gvGRNDetails.Rows.Clear();
                        for (int i = 0; i < gvGRNDetails.Rows.Count; i++)
                        {
                            gvGRNDetails.Rows[i].Cells["RecdQty"].Value = "0";
                            
                        }
                    }
                    else
                    {
                        string strSQL = "DELETE from SP_PKM_GRN_HEAD" +
                                    " WHERE SPGH_COMPANY_CODE='" + CommonData.CompanyCode +
                                    "' AND SPGH_BRANCH_CODE='" + CommonData.BranchCode +
                                    "' AND SPGH_GRN_NUMBER='" + txtTransactionNo.Text +
                                    "' AND SPGH_FIN_YEAR='" + sFinYear + "'";
                        MessageBox.Show("GRN data not saved.", "SP GRN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }

            }
            catch (Exception ex)
            {
                if (IsModify == false)
                {
                    string strSQL = "DELETE from SP_PKM_GRN_HEAD" +
                                        " WHERE SPGH_COMPANY_CODE='" + CommonData.CompanyCode +
                                        "' AND SPGH_BRANCH_CODE='" + CommonData.BranchCode +
                                        "' AND SPGH_GRN_NUMBER='" + txtTransactionNo.Text +
                                        "' AND SPGH_FIN_YEAR='" + sFinYear + "'";
                    objSQLDB = new SQLDB();
                    int intDel = objSQLDB.ExecuteSaveData(strSQL);
                    objSQLDB = null;
                }
                MessageBox.Show(ex.Message,"DC", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
              
            }
        }

        private int SaveDCHeadData()
        {
            int intSave = 0;
            string strSQL = string.Empty;
            objSQLDB = new SQLDB();
            string strDriver = string.Empty;
            int ecode = 0;
            try
            {
                  strDriver = txtDriverNo.Text.ToString();

                if (txtTotalFreight.Text.ToString().Trim().Length == 0)
                    txtTotalFreight.Text = "0.00";
                if (txtAdvancePaid.Text.ToString().Trim().Length == 0)
                    txtAdvancePaid.Text = "0.00";
                if (txtToPay.Text.ToString().Trim().Length == 0)
                    txtToPay.Text = "0.00";
                if (txtUnLoadingCharges.Text.ToString().Trim().Length == 0)
                    txtUnLoadingCharges.Text = "0.00";               
                
                if (IsModify == false)
                {
                    txtTransactionNo.Text = NewTransactionNumber().ToString();

                    strSQL = " INSERT INTO SP_PKM_GRN_HEAD " +
                         "(SPGH_COMPANY_CODE" +
                         ", SPGH_STATE_CODE" +
                         ", SPGH_BRANCH_CODE" +
                         ", SPGH_FIN_YEAR" +
                         ", SPGH_DOCUMENT_MONTH" +
                         ", SPGH_GRN_TYPE" +
                         ", SPGH_GRN_NUMBER" +
                         ", SPGH_REFERENCE_NUMBER" +
                         ", SPGH_GRN_DATE" +
                         ", SPGH_FROM_BRANCH_CODE" +
                         ", SPGH_FROM_eCODE" +
                         ", SPGH_TRIP_OR_LR_NUMBER" +
                         ", SPGH_VEHICLE_SOURCE" +
                         ", SPGH_VEHICLE_NUMBER" +
                         ", SPGH_TRANSPORTER_NAME" +
                         ", SPGH_DRIVER_NAME" +
                         ", SPGH_TOTAL_FREIGHT" +
                         ", SPGH_ADVANCE_PAID" +
                         ", SPGH_TO_PAY_FREIGHT" +
                         ", SPGH_UNLOADING_CHARGES" +
                         ", SPGH_CREATED_BY" +
                         ", SPGH_CREATED_DATE) " +
                         "VALUES " +
                         "('" + CommonData.CompanyCode +
                         "', '" + CommonData.StateCode +
                         "', '" + CommonData.BranchCode +
                         "', '" + sFinYear +
                         "', '" + txtDocMonth.Text +
                         "', '" + cbTransactionType.Text + 
                         "', '" + txtTransactionNo.Text +
                         "', '" + txtReferenceNo.Text +
                         "', '" + Convert.ToDateTime(dtpTranDate.Value.ToString()).ToString("dd/MMM/yyy") +
                         "', '" + cbBranches1.SelectedValue.ToString() +
                         //"', '" + ((SSCRM.ComboboxItem)(cbBranches1.SelectedItem)).Value.ToString() +
                         "', " + ecode +
                         ", '" + txtTripLRNo.Text +
                         "', '" + cbVehicleType.Text +
                         "', '" + txtVehicleNo.Text +
                         "', '" + txtTransporter.Text +
                         "', '" + txtDriverNo.Text +
                         "', '" + txtTotalFreight.Text +
                         "', '" + txtAdvancePaid.Text +
                         "', '" + txtToPay.Text +
                         "', '" + txtUnLoadingCharges.Text +
                         "', '" + CommonData.LogUserId +
                         "', '" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyy") +
                         "')";
                }
                else
                {
                    strSQL = " DELETE from SP_PKM_GRN_DETL" +
                                " WHERE SPGD_company_code='" + CommonData.CompanyCode +
                                    "' AND SPGD_BRANCH_CODE='" + CommonData.BranchCode +
                                    "' AND SPGD_GRN_NUMBER='" + txtTransactionNo.Text +
                                    "' AND SPGD_FIN_YEAR='" + sFinYear + "'";

                    int intRec = objSQLDB.ExecuteSaveData(strSQL);

                    strSQL = " UPDATE SP_PKM_GRN_HEAD " +
                                "SET SPGH_GRN_DATE ='" + Convert.ToDateTime(dtpTranDate.Value).ToString("dd/MMM/yyy") +
                                "', SPGH_TRIP_OR_LR_NUMBER='" + txtTripLRNo.Text +
                                "', SPGH_VEHICLE_SOURCE='" + cbVehicleType.Text +
                                "', SPGH_VEHICLE_NUMBER='" + txtVehicleNo.Text +
                                "', SPGH_TRANSPORTER_NAME='" + txtTransporter.Text +
                                "', SPGH_DRIVER_NAME='" + txtDriverNo.Text +
                                "', SPGH_TOTAL_FREIGHT='" + txtTotalFreight.Text +
                                "', SPGH_ADVANCE_PAID='" + txtAdvancePaid.Text +
                                "', SPGH_TO_PAY_FREIGHT='" + txtToPay.Text +
                                "', SPGH_UNLOADING_CHARGES='" + txtUnLoadingCharges.Text +
                                "', SPGH_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                                "', SPGH_LAST_MODIFIED_DATE='" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyy") +
                             "' WHERE SPGH_GRN_NUMBER = '" + txtTransactionNo.Text +
                             "'  AND SPGH_BRANCH_CODE='" + CommonData.BranchCode +
                             "' AND SPGH_FIN_YEAR='" + sFinYear +
                             "' AND SPGH_COMPANY_CODE='" + CommonData.CompanyCode.ToString() + "'";


                }

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
                strSQL = "DELETE from SP_PKM_GRN_DETL" +
                                " WHERE SPGD_COMPANY_CODE='" + CommonData.CompanyCode +
                                    "' AND SPGD_BRANCH_CODE='" + CommonData.BranchCode +
                                    "' AND SPGD_GRN_NUMBER='" + txtTransactionNo.Text +
                                    "' AND SPGD_FIN_YEAR='" + sFinYear + "'";

                intSave = objSQLDB.ExecuteSaveData(strSQL);

                for (int i = 0; i < gvGRNDetails.Rows.Count; i++)
                {
                    if (gvGRNDetails.Rows[i].Cells["RecdQty"].Value.ToString() != "" && gvGRNDetails.Rows[i].Cells["RecdQty"].Value.ToString() != "0" && gvGRNDetails.Rows[i].Cells["Amount"].Value.ToString() != "")
                    {
                        sbSQL.Append(" INSERT INTO SP_PKM_GRN_DETL(SPGD_COMPANY_CODE, SPGD_STATE_CODE, SPGD_BRANCH_CODE, SPGD_FIN_YEAR, SPGD_DOCUMENT_MONTH, SPGD_GRN_TYPE, SPGD_GRN_NUMBER, SPGD_SL_NO, SPGD_PRODUCT_ID, SPGD_BATCH_NO, SPGD_GRN_QTY, SPGD_REC_RATE, " +
                                    "SPGD_REC_QTY, SPGD_DAMAGE_QTY, SPGD_REC_AMT, SPGD_AGNST_TRN_TYPE, SPGD_AGNST_TRN_NUMBER)" +
                                    " VALUES ('" + CommonData.CompanyCode + "', '" + CommonData.StateCode + "', '" + CommonData.BranchCode + "', '" + sFinYear + "', '" + txtDocMonth.Text +
                                    "', '" + cbTransactionType.Text + "', '" +  txtTransactionNo.Text + 
                                    "', " +  gvGRNDetails.Rows[i].Cells["SLNO"].Value +
                                    ", '" + gvGRNDetails.Rows[i].Cells["ProductId"].Value +
                                    "', '" + gvGRNDetails.Rows[i].Cells["BatchNo"].Value + 
                                    "', '" + gvGRNDetails.Rows[i].Cells["DCQty"].Value + 
                                    "', '" + gvGRNDetails.Rows[i].Cells["Rate"].Value +
                                    "', '" + gvGRNDetails.Rows[i].Cells["RecdQty"].Value +
                                    "', '" + gvGRNDetails.Rows[i].Cells["DamageQty"].Value +
                                    "', '" + gvGRNDetails.Rows[i].Cells["Amount"].Value +
                                    "', '" + cbTransactionType.Text + "', '" + gvGRNDetails.Rows[i].Cells["DCNo"].Value + "'); ");

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
            objPUDB = new  ProductUnitDB();
            try
            {
                gvGRNDetails.Rows.Clear();
                cbDCs.SelectedIndex = -1;
                if (dt.Rows.Count > 0)
                {
                    for (int intRow = 0; intRow < dt.Rows.Count; intRow++)
                    {
                       
                        gvGRNDetails.Rows.Add();
                        gvGRNDetails.Rows[intRow].Cells["SLNO"].Value = intRow + 1;
                        gvGRNDetails.Rows[intRow].Cells["Category"].Value = dt.Rows[intRow]["category_name"].ToString();
                        gvGRNDetails.Rows[intRow].Cells["Product"].Value = dt.Rows[intRow]["product_name"].ToString();
                        gvGRNDetails.Rows[intRow].Cells["ProductId"].Value = dt.Rows[intRow]["ProductId"].ToString();
                        gvGRNDetails.Rows[intRow].Cells["BatchNo"].Value = dt.Rows[intRow]["BatchNo"].ToString();
                        gvGRNDetails.Rows[intRow].Cells["DcQty"].Value = dt.Rows[intRow]["DCQty"].ToString();
                        gvGRNDetails.Rows[intRow].Cells["RecdQty"].Value = dt.Rows[intRow]["RecdQty"].ToString();
                        gvGRNDetails.Rows[intRow].Cells["DamageQty"].Value = dt.Rows[intRow]["DmgQty"].ToString();
                        gvGRNDetails.Rows[intRow].Cells["RATE"].Value = dt.Rows[intRow]["RecdRate"].ToString();
                        gvGRNDetails.Rows[intRow].Cells["Amount"].Value = dt.Rows[intRow]["RecdAmt"].ToString();
                        gvGRNDetails.Rows[intRow].Cells["DCNo"].Value = dt.Rows[intRow]["AgnTrnNumber"].ToString();
                        gvGRNDetails.Rows[intRow].Cells["DCSLNo"].Value = dt.Rows[intRow]["SlNo"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"SP GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objPUDB = null;
            }
        }

        private bool CheckData()
        {
            bool blValue = true;
           
            if (Convert.ToString(txtTransactionNo.Text).Length == 0)
            {
                MessageBox.Show("Transaction no is not generated!\n Contact to IT-Department", "SP GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                return blValue;
            }
            if (Convert.ToString(txtReferenceNo.Text).Length == 0)
            {
                MessageBox.Show("Eneter GRN Reference No","SP GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                return blValue;
            }
            if (Convert.ToString(txtTransactionNo.Text).Length < 21)
            {
                MessageBox.Show("Invalid Transaction Number!", "SP GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtTransactionNo.Focus();
                return blValue;
            }
            if (Convert.ToString(txtTransactionNo.Text).IndexOf('-') == -1)
            {
                MessageBox.Show("Invalid Transaction Number!", "SP GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtTransactionNo.Focus();
                return blValue;
            }            
            if (cbBranches1.SelectedIndex == -1)
            {
                MessageBox.Show("Select received from branch!", "SP GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                cbBranches1.Focus();
                return blValue;
            }

                       
            bool blInvDtl = false;
            for (int i = 0; i < gvGRNDetails.Rows.Count; i++)
            {
                if (Convert.ToString(gvGRNDetails.Rows[i].Cells["RecdQty"].Value) != "0")
                {
                    blInvDtl = true;
                }
            }

            if (blInvDtl == false)
            {
                blValue = false;
                MessageBox.Show("Enter product quantity", "SP GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            
            
            return blValue;

        }

        private void gvGRNDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 6 && e.ColumnIndex <=18)
            {
                if (Convert.ToString(gvGRNDetails.Rows[e.RowIndex].Cells["RecdQty"].Value) != "")
                {
                    if (Convert.ToDouble(gvGRNDetails.Rows[e.RowIndex].Cells["RecdQty"].Value) >= 0 && Convert.ToDouble(gvGRNDetails.Rows[e.RowIndex].Cells["Rate"].Value) >= 0)
                    {
                        gvGRNDetails.Rows[e.RowIndex].Cells["Amount"].Value = (Convert.ToDouble(gvGRNDetails.Rows[e.RowIndex].Cells["RecdQty"].Value) * Convert.ToDouble(gvGRNDetails.Rows[e.RowIndex].Cells["Rate"].Value));
                        gvGRNDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvGRNDetails.Rows[e.RowIndex].Cells["Amount"].Value).ToString("f");
                    }

                }
                else
                {
                    gvGRNDetails.Rows[e.RowIndex].Cells["Amount"].Value = string.Empty;
                    
                }
            }
            CalculateTotals();
        }
        
        private void txtTransactionNo_Validated(object sender, EventArgs e)
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
            DataSet Rs = new  DataSet();
            try
            {

                Rs = objPUDB.GRN_PKM_StockPoint("", "", TranNO);
                DataTable dtH = Rs.Tables[0];// (DataTable)ht["Head"];
                DataTable dtD =Rs.Tables[1]; // (DataTable)ht["Detail"];
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
                objPUDB = null;
                Rs = null;
               
            }
        }

        private bool FillTransactionHead(DataTable dtHead, DataTable dtDetl)
        {
            bool isData = false;
            try
            {
                if (dtHead.Rows.Count > 0)
                {
                    
                    IsModify = true;
                    txtDocMonth.Text = dtHead.Rows[0]["DocMonth"].ToString();
                    sFinYear = dtHead.Rows[0]["FinYear"].ToString();
                    strTransactionNo = dtHead.Rows[0]["TrnNumber"].ToString();
                    txtTransactionNo.Text = dtHead.Rows[0]["TrnNumber"] + "";
                    dtpTranDate.Value = Convert.ToDateTime(dtHead.Rows[0]["TrnDate"] + "");
                    cbTransactionType.Text = dtHead.Rows[0]["TrnType"] + "";
                    strFromBranchCode = dtHead.Rows[0]["FromBranchCode"]+"";
                    cbBranches1.SelectedValue = strFromBranchCode;
                    strECode = dtHead.Rows[0]["FromEcode"] + "";                    
                    cbVehicleType.SelectedValue = dtHead.Rows[0]["VehicleSource"] + "";
                    txtDriverNo.Text = dtHead.Rows[0]["DriverName"] + "";
                    txtTripLRNo.Text = Convert.ToString(dtHead.Rows[0]["TripLRNumber"]);
                    txtVehicleNo.Text = Convert.ToString(dtHead.Rows[0]["VehicleNumber"]);
                    txtTotalFreight.Text = Convert.ToString(dtHead.Rows[0]["TotalFreight"]);
                    txtAdvancePaid.Text = Convert.ToString(dtHead.Rows[0]["AdvancePaid"]);
                    txtToPay.Text = Convert.ToString(dtHead.Rows[0]["ToPayFreight"]);
                    txtUnLoadingCharges.Text = Convert.ToString(dtHead.Rows[0]["UnloadingCharges"]);
                    txtTransporter.Text = dtHead.Rows[0]["TransporterName"] + "";
                    txtReferenceNo.Text = dtHead.Rows[0]["ReferNumber"] + "";
                    FillTransactionDetl(dtDetl);
                    isData = true;
                    cbBranches1.Enabled = false;
                    cbDCs.Enabled = false;
                }
                else
                {
                    cbBranches1.Enabled = true;
                    txtDocMonth.Text = CommonData.DocMonth;
                    sFinYear = CommonData.FinancialYear;
                    cbDCs.Enabled = true;
                    btnSave.Enabled = true;
                    btnDelete.Enabled = true;
                    IsModify = false;
                    gvGRNDetails.Rows.Clear();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dtHead = null;
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
                    cbBranches1.Enabled = true;
                    cbDCs.Enabled = true;
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
            //if (txtTransactionNo.SelectionStart <= 21 && txtTransactionNo.SelectionStart >= 14)
            //{

            //    if (char.IsDigit(e.KeyChar) == false)
            //        e.Handled = true;

            //    if (e.KeyChar == 8)
            //        e.Handled = false;
            //}
            //else
            //    e.Handled = true;
            if (char.IsNumber(e.KeyChar) == false)
                e.Handled = true;
            if (txtTransactionNo.SelectionStart < 17)
                e.Handled = true;
            else if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void gvGRNDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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

        private void gvGRNDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex >= 0)
            {
                DataGridView dgv = (DataGridView)sender;
                if (e.ColumnIndex == 5 || e.ColumnIndex == 6)
                {
                    DataGridViewCell textBoxCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    if (textBoxCell != null)
                    {
                        gvGRNDetails.CurrentCell = textBoxCell;
                        dgv.BeginEdit(true);
                    }
                }
                if (e.ColumnIndex == gvGRNDetails.Columns["Delete"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        DataGridViewRow dgvr = gvGRNDetails.Rows[e.RowIndex];
                        gvGRNDetails.Rows.Remove(dgvr);
                        OrderSlNo();
                    }
                }
            }
        }

        private void OrderSlNo()
        {
            if (gvGRNDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvGRNDetails.Rows.Count; i++)
                {
                    gvGRNDetails.Rows[i].Cells["SLNO"].Value = i + 1;
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
                    if (objSecur.CanModifyDataUserAsPerBackDays(Convert.ToDateTime(dtpTranDate.Value)) == false)
                    {
                        MessageBox.Show("You cannot manipulate backdays data!\n If you want to modify, Contact to IT-Department", "DC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTransactionNo.Focus();
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Do you want to Delete " + txtTransactionNo.Text + " Transaction ?",
                                               "SP GRN", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            objSQLDB = new SQLDB();

                            string strDelete = " DELETE from SP_PKM_GRN_DETL " +
                                                " WHERE SPGD_COMPANY_CODE='" + CommonData.CompanyCode +
                                                "' AND SPGD_BRANCH_CODE='" + CommonData.BranchCode +
                                                "' AND SPGD_GRN_NUMBER='" + txtTransactionNo.Text +
                                                "'  AND SPGD_FIN_YEAR='" + CommonData.FinancialYear + "'";

                            strDelete += "DELETE from SP_PKM_GRN_HEAD" +
                                                " WHERE SPGH_COMPANY_CODE='" + CommonData.CompanyCode +
                                                "' AND SPGH_BRANCH_CODE='" + CommonData.BranchCode +
                                                "' AND SPGH_GRN_NUMBER='" + txtTransactionNo.Text +
                                                "'  AND SPGH_FIN_YEAR='" + CommonData.FinancialYear + "' ";

                            int intRec = objSQLDB.ExecuteSaveData(strDelete);
                            if (intRec > 0)
                            {
                                cbBranches1.Enabled = true;
                                cbDCs.Enabled = true;
                                IsModify = false;
                                MessageBox.Show("Tran No: " + txtTransactionNo.Text + " Deleted!", "CRM GRN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ClearForm(this);
                                gvGRNDetails.Rows.Clear();
                                txtTransactionNo.Text = NewTransactionNumber().ToString();
                            }

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Enter Tran Number.", "SP GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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

        private void gvGRNDetails_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                DataGridView dgv = (DataGridView)sender;
                dgv.EndEdit();
                if (e.ColumnIndex == 6)
                {
                    if (Convert.ToString(gvGRNDetails.Rows[e.RowIndex].Cells["RecdQty"].Value) != "" && Convert.ToString(gvGRNDetails.Rows[e.RowIndex].Cells["DCNo"].Value) != "0")
                    {
                        if (Convert.ToDouble(gvGRNDetails.Rows[e.RowIndex].Cells["RecdQty"].Value) > Convert.ToDouble(gvGRNDetails.Rows[e.RowIndex].Cells["DcQty"].Value))
                        {
                            //MessageBox.Show("Received quantity should be less than or equal to DC quantity!", "DC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //e.Cancel = true;
                        }
                    }
                }
            }
            catch
            {
            }

        }
        
        private double GetTotalAmount()
        {
            double dbInvAmt = 0;
            for (int i = 0; i < gvGRNDetails.Rows.Count; i++)
            {
                if (gvGRNDetails.Rows[i].Cells["Amount"].Value.ToString() != "")
                {  
                    if(Convert.ToDouble(gvGRNDetails.Rows[i].Cells["Amount"].Value.ToString())>=1)
                        dbInvAmt += Convert.ToDouble(gvGRNDetails.Rows[i].Cells["Amount"].Value);

                }

            }
            return dbInvAmt;
        }

        private void btnClearProd_Click(object sender, EventArgs e)
        {
            gvGRNDetails.Rows.Clear();
        }

        private void cbDCs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDCs.SelectedIndex > -1)
            {
                
                Fill_pudc_Details();
                cbVehicleType.Enabled = false;
            }
            else
                cbVehicleType.Enabled = true;
        }

        private void cbVehicleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbVehicleType.Text == "OWN")
            //    cbDriverEcode.Visible = true;
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
            //else
                //cbDriverEcode.Visible = false;
        }

        private void cbDriverEcode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void cbTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTransactionType.SelectedIndex > -1 && TranTypeLoad == false)
            {
                FillBranchData();
                if(cbBranches1.Items.Count!=0)
                    cbBranches1.SelectedIndex = 0;
                
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

        private void txtTotalFreight_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAdvancePaid_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtToPay_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }        
        
        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            if (cbTransactionType.SelectedValue.ToString() == "BR2SP")
            {
                if (cbBranches1.SelectedValue != null)
                {
                    //IndentProductSearch PSearch = new IndentProductSearch("SPGRN", cbBranches1.SelectedValue.ToString());
                    //PSearch.objStockPointGRN = this;
                    //PSearch.ShowDialog();
                }
            }
            else
            {
                //IndentProductSearch PSearch = new IndentProductSearch("SPGRN", CommonData.BranchCode);
                //PSearch.objStockPointGRN = this;
                //PSearch.ShowDialog();
            }
            CalculateTotals();
        }

        private void txtReferenceNo_Validated(object sender, EventArgs e)
        {
            if (txtReferenceNo.Text.Trim().Length > 0)
            {
                FillGRNDetails(txtReferenceNo.Text.ToString());
            }
        }

        private void FillGRNDetails(string grnRefNo)
        {
            objStockPointDB = new StockPointDB();
            DataSet sGrnInfo = null;
            try
            {
                sGrnInfo = objStockPointDB.SPPKMGRNHeadDetails_Get(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth, txtReferenceNo.Text.ToString());
                DataTable sGrnHead = sGrnInfo.Tables[0];
                if (sGrnHead.Rows.Count > 0)
                {
                    FillGRNHeadData(sGrnHead);
                    //FillGRNDetlDatatoGrid(sGrnDetl);
                }
                else
                {
                    sGrnHead = null;
                    //ClearForm(this);
                    txtToPay.Text = "";
                    txtTotalFreight.Text = "";
                    txtAdvancePaid.Text = "";
                    txtUnLoadingCharges.Text = "";
                    txtVehicleNo.Text = "";
                    txtDriverNo.Text = "";
                    txtTransporter.Text = "";
                    txtTripLRNo.Text = "";
                    IsModify = false;
                    dtpTranDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
                    //gvGRNDetails.Rows.Clear();
                    txtTransactionNo.Text = NewTransactionNumber().ToString();
                    IsModify = false;
                   
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillGRNDetlDatatoGrid(string sGrnNo)
        {
            objStockPointDB = new StockPointDB();
            DataTable sGrnDetl = null;
            sGrnDetl = objStockPointDB.SPPKMGRNDetlInfo_Get(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth, txtTransactionNo.Text.ToString()).Tables[0];
            gvGRNDetails.Rows.Clear();
            if (sGrnDetl.Rows.Count > 0)
            {
                foreach (DataRow dr in sGrnDetl.Rows)
                {
                    gvGRNDetails.Rows.Add();
                    int intCurRow = gvGRNDetails.Rows.Count - 1;
                    gvGRNDetails.Rows[intCurRow].Cells["SLNO"].Value = intCurRow + 1;
                    gvGRNDetails.Rows[intCurRow].Cells["ProductID"].Value = dr["PRODUCTID"] + "";
                    gvGRNDetails.Rows[intCurRow].Cells["Category"].Value = dr["CATEGORY"] + "";
                    gvGRNDetails.Rows[intCurRow].Cells["Product"].Value = dr["PRODUCT_NAME"] + "";
                    gvGRNDetails.Rows[intCurRow].Cells["BatchNO"].Value = dr["BATCHNO"] + "";
                    gvGRNDetails.Rows[intCurRow].Cells["DCQty"].Value = dr["DCQTY"] + "";
                    gvGRNDetails.Rows[intCurRow].Cells["RecdQty"].Value = dr["RECIEVED_QTY"] + "";
                    gvGRNDetails.Rows[intCurRow].Cells["DamageQty"].Value = dr["DAMAGE_QTY"] + "";
                    gvGRNDetails.Rows[intCurRow].Cells["Rate"].Value = dr["RATE"] + "";
                    gvGRNDetails.Rows[intCurRow].Cells["Amount"].Value = dr["AMOUNT"] + "";
                    gvGRNDetails.Rows[intCurRow].Cells["DcNo"].Value = dr["DCNO"] + "";
                    gvGRNDetails.Rows[intCurRow].Cells["DCSlno"].Value = dr["DCSLNO"] + "";
                }
            }
        }

        private void FillGRNHeadData(DataTable sGrnHead)
        {
            if (sGrnHead.Rows.Count > 0)
            {
                cbTransactionType.SelectedValue = sGrnHead.Rows[0]["TRANCTION_TYPE"] + "";
                txtTransactionNo.Text = sGrnHead.Rows[0]["GRN_NO"] + "";
                //dtpTranDate.Value = Convert.ToDateTime(Convert.ToDateTime(sGrnHead.Rows[0]["TRANSACTION_DATE"]+"").ToString("dd/MM/yyyy"));
                //txtTotalFreight.Text = sGrnHead.Rows[0]["TOTAL_FREIGHT"] + "";
                //txtAdvancePaid.Text = sGrnHead.Rows[0]["ADVANCE_PAID"] + "";
                //txtToPay.Text = sGrnHead.Rows[0]["TO_PAY"] + "";
                //txtUnLoadingCharges.Text = sGrnHead.Rows[0]["UNLOADING_CHARGES"] + "";
                //FillBranchData();
                //cbBranches.Enabled = true;
                //cbBranches.Text = sGrnHead.Rows[0]["FROM_BRANCHCODE"] + "";
                //string ecode = sGrnHead.Rows[0]["FROM_ECODE"] + "";
                //cbEcode.SelectedValue = ecode;
                ////cbDCs.SelectedValue = sGrnHead.Rows[0]["DCNO"] + "";
                //txtTripLRNo.Text = sGrnHead.Rows[0]["TRIP_NO"] + "";
                //cbVehicleType.SelectedValue = sGrnHead.Rows[0]["VEHICLE_TYPE"] + "";
                //txtVehicleNo.Text = sGrnHead.Rows[0]["VEHICLE_NO"] + "";
                //txtDriverNo.Text = sGrnHead.Rows[0]["DRIVER_NO"] + "";
                //txtTransporter.Text = sGrnHead.Rows[0]["TRANSPORTER"] + "";
            }
            //FillGRNDetlDatatoGrid(txtTransactionNo.Text.ToString());
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

        



   }
}
