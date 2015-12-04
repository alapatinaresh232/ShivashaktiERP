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
using System.Data.Sql;
using SSAdmin;
using SSCRMDB;
using SSTrans;
using SSCRM.App_Code;

namespace SSCRM
{
    public partial class TripSheet : Form
    {
        SQLDB objSQLdb = null;
        StockPointDB objStockDB = null;
        private string sCompCode = "", sBranchCode = "", sFinYear = "", sUpDownType = "";
        private string strAltration = "", strFillUp = "", strReadingDiff = "", strMileage = "", strUpDownDiff = "", StockFlag = "";

        private bool flagUpdate = false;        
        public DataTable dtProductDetl = new DataTable();
        public DataTable dtDeliveryDetl = new DataTable();

        public TripSheet()
        {
            InitializeComponent();
        }

        private void TripSheet_Load(object sender, EventArgs e)
        {
            if (CommonData.LogUserId != "ADMIN")
                txtDocMonth.ReadOnly = false;
            else
                txtDocMonth.ReadOnly = true;

            cbTripCode.Visible = false;
            txtTrnNo.Visible = false;
            lblTripCode.Visible = false;
            lblTrnNo.Visible = false;

            gvDeliverProducts.Visible = false;
            grouper1.Visible = true;
            grouper2.Visible = false;
            txtDocMonth.Text = CommonData.DocMonth;
            cbVehType.SelectedIndex = 0;
           
            cbVehPurpose.SelectedIndex = 0;
            rdbSTNo.Checked = true;
            cbTripCode.SelectedIndex = 1;
            FillCompanyData();
            FillBranchData();

            sCompCode = CommonData.CompanyCode;
            sBranchCode = CommonData.BranchCode;
            sFinYear = CommonData.FinancialYear;
            btnSave.Enabled = false;
            btnClear.Enabled = false;

            cbUpDownType.SelectedIndex = 0;

            #region "CREATE TRIP_DELIVERY_DETAILS TABLE"
            dtDeliveryDetl.Columns.Add("SlNo_Delv");
            dtDeliveryDetl.Columns.Add("GLEcode");
            dtDeliveryDetl.Columns.Add("STDateTime");
            dtDeliveryDetl.Columns.Add("EndDateTime");
            dtDeliveryDetl.Columns.Add("StartDate");
            dtDeliveryDetl.Columns.Add("StartTime");
            dtDeliveryDetl.Columns.Add("StartPlace");
            dtDeliveryDetl.Columns.Add("StartReading");
            dtDeliveryDetl.Columns.Add("EndDate");
            dtDeliveryDetl.Columns.Add("EndTime");
            dtDeliveryDetl.Columns.Add("EndPlace");
            dtDeliveryDetl.Columns.Add("EndReading");
            dtDeliveryDetl.Columns.Add("NoOfKM");
            dtDeliveryDetl.Columns.Add("NoOfUnitsDel");
            dtDeliveryDetl.Columns.Add("NoOfCustCov");
            dtDeliveryDetl.Columns.Add("TotDays");
            dtDeliveryDetl.Columns.Add("DriverDA");
            dtDeliveryDetl.Columns.Add("CleanerDA");
            dtDeliveryDetl.Columns.Add("GLName");
            #endregion

            #region "CREATE PRODUCT_DETAILS TABLE"
            dtProductDetl.Columns.Add("SlNo_Prod");
            dtProductDetl.Columns.Add("ProductId");
            dtProductDetl.Columns.Add("StartReading");
            dtProductDetl.Columns.Add("ProdName");
            dtProductDetl.Columns.Add("CategoryName");
            dtProductDetl.Columns.Add("DespatchQty");
            #endregion

            
        }

        private void GenerateTrnNo()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
            try
            {
                if (cbTripCode.SelectedIndex > 0)
                {
                    string strNewNo = cbTripCode.Text.ToString();

                    strCommand = "SELECT ISNULL(MAX(SUBSTRING(ISNULL(TSH_TRN_NO, '" + strNewNo + "'),2,4)),0) + 1 " +
                                       " FROM TRIP_SHEET_HEAD WHERE TSH_TRIPSHEET_CHAR ='" + cbTripCode.Text.ToString() + "'";

                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtTrnNo.Text = strNewNo + Convert.ToInt32(dt.Rows[0][0]).ToString("0000");
                    }
                }
                else
                {
                    txtTrnNo.Text = "";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            try
            {
                strCmd = "SELECT CM_COMPANY_CODE,CM_COMPANY_NAME "+
                                " FROM COMPANY_MAS "+
                                " WHERE ACTIVE='T' ORDER BY CM_COMPANY_NAME asc";

                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbCompany.DataSource = dt;
                    cbCompany.DisplayMember = "CM_COMPANY_NAME";
                    cbCompany.ValueMember = "CM_COMPANY_CODE";
                }
                                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
                dt = null;
            }
        }

        private void FillBranchData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbBranches.DataSource = null;
            string strCommand = "";

            try
            {
                if (cbCompany.SelectedIndex > 0)
                {
                    strCommand = "SELECT BRANCH_NAME,BRANCH_CODE " +
                                 " FROM BRANCH_MAS where COMPANY_CODE='"+ cbCompany.SelectedValue.ToString() +
                                 "' and active='T' " +
                                 " and BRANCH_TYPE in ('BR','PU','SP','TU','OL') " +                                       
                                 " ORDER BY BRANCH_NAME ASC";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbBranches.DataSource = dt;
                    cbBranches.DisplayMember = "BRANCH_NAME";
                    cbBranches.ValueMember = "BRANCH_CODE";

                }
                                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
                dt = null;
            }
        }



        private void btnBack_Click(object sender, EventArgs e)
        {
            AutoValidate = AutoValidate.EnableAllowFocusChange;

            grouper1.Visible = true;
            grouper2.Visible = false;

           

        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            AutoValidate = AutoValidate.Disable;

            if (CheckData() == true)
            {
                grouper1.Visible = false;
                grouper2.Visible = true;
                
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            AutoValidate = AutoValidate.EnableAllowFocusChange;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            AutoValidate = AutoValidate.EnableAllowFocusChange;

        }

        private void btnAddTripDelvDetl_Click(object sender, EventArgs e)
        {
            if (txtTripLRNo.Text != "")
            {
                frmTripDeliveryDetails TripDetl = new frmTripDeliveryDetails();
                TripDetl.objTripSheet = this;
                TripDetl.ShowDialog();
                
            }
            else
            {
                MessageBox.Show("Please Enter TripLR Number","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }

        }

        private void btnAddDieselBillDetl_Click(object sender, EventArgs e)
        {
            
            if (txtTripLRNo.Text != "")
            {               
                frmDieselBillDetails DieselDetl = new frmDieselBillDetails();
                DieselDetl.objTripSheet = this;
                DieselDetl.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please Enter TripLR Number", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnAddDCDetails_Click(object sender, EventArgs e)
        {
            frmDCandDCRDetails DCDetail = new frmDCandDCRDetails();
            DCDetail.objTripSheet = this;
            DCDetail.ShowDialog();
            btnSave.Enabled = true;
        }

     
     
        private void cbTripCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTripCode.SelectedIndex > 0)
                GenerateTrnNo();
            else
                txtTrnNo.Text = "";
        }

        private void txtHireCharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtOtherExp_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender, e);
        }


        public void EnableTab(TabPage page, bool enable)
        {
            EnableControls(page.Controls, enable);
        }
        private void EnableControls(Control.ControlCollection ctls, bool enable)
        {
            foreach (Control ctl in ctls)
            {
                ctl.Enabled = enable;
                EnableControls(ctl.Controls, enable);
            }
        }
 

        private void cbVehType_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbVehType.Text.Equals("COMPANY"))
            {
                EnableTab(UpDownExp, true);
               

                FillVehicleNumbers();

            }
            else if (cbVehType.Text.Equals("HIRE"))
            {
                EnableTab(UpDownExp, false);
                txtVehNo.AutoCompleteCustomSource = null;

                //((Control)this.UpDownExp).Enabled = false;
                //(this.UpDownExp).Visible = false;
                //UpDownExp.Hide();               
                //TabExpDetl.Refresh();         
              
                              
            }
        }

        private void FillVehicleNumbers()
        {
            try
            {
                objSQLdb = new SQLDB();
                DataTable dt = objSQLdb.ExecuteDataSet("SELECT TVM_VEHICLE_NUMBER FROM TR_VEHICLE_MAS "+
                                                        " WHERE TVM_VEHICLE_NUMBER LIKE 'A%' "+
                                                        " ORDER BY TVM_VEHICLE_NUMBER ASC").Tables[0];
                UtilityLibrary.AutoCompleteTextBox(txtVehNo, dt, "", "TVM_VEHICLE_NUMBER");
                objSQLdb = null;
            }
            catch { }
            finally { objSQLdb = null; }
        }


        private void isDigitsCheck(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && e.KeyChar != '.')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') != -1)
            {
                e.Handled = true;
            }
        }

        private bool CheckData()
        {
            bool bFlag = true;
            double dEcode = 0;
            
            if (cbVehType.SelectedIndex == -1)
            {
                bFlag = false;
                MessageBox.Show("Please Select Vehilce Type","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
                cbVehType.Focus();
                return bFlag;
            }
            if (txtTrnNo.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Contact IT department", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTrnNo.Focus();
                return bFlag;
            }
            if (txtTripLRNo.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Enter Trip LRNumber ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTripLRNo.Focus();
                return bFlag;
            }
            if (txtVehNo.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Enter Vehicle Number", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtVehNo.Focus();
                return bFlag;
            }
            if (txtVehModel.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Enter Vehicle Model", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtVehModel.Focus();
                return bFlag;
            }
            if (cbVehPurpose.SelectedIndex == -1)
            {
                bFlag = false;
                MessageBox.Show("Please Select Vehicle Purpose", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbVehPurpose.Focus();
                return bFlag;
            }

            if (cbCompany.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Stock Deliver To Company ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCompany.Focus();
                return bFlag;
            }
            if (cbBranches.SelectedIndex == 0 || cbBranches.SelectedIndex==-1)
            {
                bFlag = false;
                MessageBox.Show("Please Select Stock Deliver To Branch ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbBranches.Focus();
                return bFlag;
            }
            if(txtEcodeSearch.Text.Length>0)
            dEcode = Convert.ToDouble(txtEcodeSearch.Text.ToString());

            if (dEcode != 0 && txtEcodeSearch.Text.Length > 0)
            {
                if (txtName.Text.Length == 0)
                {
                    bFlag = false;
                    MessageBox.Show("Please Enter Valid Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEcodeSearch.Focus();
                    return bFlag;
                }
            }

            if (gvDeliveryDetails.Rows.Count == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Add Trip Delivery Details", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnAddTripDelvDetl.Focus();
                return bFlag;
            }

            //if (cbVehType.SelectedIndex == 0)
            //{
            //    if (gvDieselDetails.Rows.Count == 0)
            //    {
            //        bFlag = false;
            //        MessageBox.Show("Please Add Diesel Expenses Details", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        btnAddDieselBillDetl.Focus();
            //        return bFlag;
            //    }
            //}

            btnSave.Enabled = true;
            btnClear.Enabled = true;
          
            return bFlag;
        }

        private bool CheckDetails()
        {
            bool flag = true;
           
            if (gvDCorDCSTDetl.Rows.Count == 0)
            {
                flag = false;
                MessageBox.Show("Please Add Deliver Product DC/DCST Reference Numbers", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnAddDCDetails.Focus();
                return flag;
            }
            if (cbVehType.SelectedIndex == 0 || cbVehType.SelectedIndex==1)
            {
                if (TabExpDetl.SelectedIndex==0)
                {
                    if (txtTotExp.Text.Length == 0 || txtTotExp.Text.Equals("0"))
                    {
                        flag = false;
                        MessageBox.Show("Please Enter Trip Expenses Details", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtOtherExp.Focus();
                        return flag;

                    }
                }

            }
            if (cbVehType.SelectedIndex == 1)
            {
                if (txtHirePerDay.Text.Length == 0 && txtHirePerKM.Text.Length == 0)
                {
                    if (txtKmPerDiesel.Text.Length == 0 && txtMileage.Text.Length == 0)
                    {
                        flag = false;
                        MessageBox.Show("Please Enter Agreement Amount Details", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtHirePerDay.Focus();
                        return flag;
                    }
                }
            }

           

            if (cbVehType.SelectedIndex == 0)
            {
                //if (TabExpDetl.SelectedIndex==1)
                //{
                //    if (cbUpDownType.SelectedIndex == 0)
                //    {
                //        flag = false;
                //        MessageBox.Show("Please Select HireType", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        cbUpDownType.Focus();
                //        return flag;
                //    }

                //    if (txtUDTotExp.Text.Length == 0 || txtUDTotExp.Text.Equals("0"))
                //    {
                //        flag = false;
                //        MessageBox.Show("Please Enter Expenses Details", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        txtUDTotExp.Focus();
                //        return flag;

                //    }
                //}

            }
            //if (txtRemarks.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter Remarks", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtRemarks.Focus();
            //    return flag;
            //}

            return flag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCmd = "";
            
            if (CheckDetails() == true)
            {
                try
                {

                    if (SaveTripSheetHeadDetails() > 0)
                    {
                        if (SaveDCorDCRDetails() > 0)
                        {
                            if (SaveTripDeliveryDetl() > 0)
                            {
                                SaveTripDieselExpDetails();
                                SaveProductDetails();
                                MessageBox.Show("Trip Sheet Data Saved \nTrip LRNo: " + txtTripLRNo.Text.ToString() + "", "TRIP SHEET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                flagUpdate = false;
                                GenerateTrnNo();
                                btnCancel_Click(null,null);
                                btnClear_Click(null, null);
                                grouper1.Visible = true;
                                grouper2.Visible = false;
                                AutoValidate = AutoValidate.EnableAllowFocusChange;
                            }
                            else
                            {
                                strCmd += "DELETE FROM TRIP_SHEET_DIESEL_EXP WHERE TSDD_COMP_CODE='" + sCompCode +
                                       "' AND TSDD_BRANCH_CODE='" + sBranchCode + "' AND TSDD_FIN_YEAR='" + sFinYear +
                                       "' AND TSDD_TRN_NO='" + txtTrnNo.Text.ToString() + "'";
                                strCmd += "DELETE FROM TRIP_SHEET_DC_DETL WHERE TSDCD_COMP_CODE='" + sCompCode +
                                                "' AND TSDCD_BRANCH_CODE='" + sBranchCode + "' AND TSDCD_FIN_YEAR='" + sFinYear +
                                                "' AND TSDCD_TRN_NO='" + txtTrnNo.Text.ToString() + "'";
                                strCmd += "DELETE FROM TRIP_SHEET_PRODUCT_DETL WHERE TSPD_COMP_CODE='" + sCompCode +
                                             "' AND TSPD_BRANCH_CODE='" + sBranchCode +
                                             "' AND TSPD_FIN_YEAR='" + sFinYear +
                                             "' AND TSPD_TRN_NO='" + txtTrnNo.Text.ToString() + "'";
                                strCmd += "DELETE FROM TRIP_SHEET_DELIVERY_DETL WHERE TSD_COMP_CODE='" + sCompCode +
                                              "' AND TSD_BRANCH_CODE='" + sBranchCode + "' AND TSD_FIN_YEAR='" + sFinYear +
                                              "' AND TSD_TRN_NO='" + txtTrnNo.Text.ToString() + "'";
                                strCmd += "DELETE FROM TRIP_SHEET_HEAD WHERE TSH_COMP_CODE='" + sCompCode +
                                              "' AND TSH_BRANCH_CODE='" + sBranchCode + "' AND TSH_FIN_YEAR='" + sFinYear +
                                              "' and TSH_TRN_NO='" + txtTrnNo.Text.ToString() + "'";
                                iRes = objSQLdb.ExecuteSaveData(strCmd);
                                flagUpdate = false;
                                MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        else
                        {
                            strCmd = "DELETE FROM TRIP_SHEET_HEAD WHERE TSH_COMP_CODE='" + sCompCode +
                                    "' AND TSH_BRANCH_CODE='" + sBranchCode + "' AND TSH_FIN_YEAR='" + sFinYear +
                                    "' AND TSH_TRN_NO='" + txtTrnNo.Text.ToString() + "'";
                            iRes = objSQLdb.ExecuteSaveData(strCmd);

                            flagUpdate = false;

                            MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }

        }

        private void GetTextBoxValues()
        {
            try
            {
                if (txtHireCharges.Text.Length == 0)
                    txtHireCharges.Text = "0";
                if (txtDieselCharges.Text.Length == 0)
                    txtDieselCharges.Text = "0";
                if (txtPhoneCharges.Text.Length == 0)
                    txtPhoneCharges.Text = "0";
                if (txtTollGates.Text.Length == 0)
                    txtTollGates.Text = "0";
                if (txtOtherExp.Text.Length == 0)
                    txtOtherExp.Text = "0";
                if (txtOtherExp.Text.Length == 0)
                    txtOtherExp.Text = "0";
                if (txtRepandMain.Text.Length == 0)
                    txtRepandMain.Text = "0";
                if (txtRTOBills.Text.Length == 0)
                    txtRTOBills.Text = "0";
                if (txtRTOWithout.Text.Length == 0)
                    txtRTOWithout.Text = "0";
                if (txtPCTCExp.Text.Length == 0)
                    txtPCTCExp.Text = "0";
                if (txtCleanerDa.Text.Length == 0)
                    txtCleanerDa.Text = "0";
                if (txtDriverDA.Text.Length == 0)
                    txtDriverDA.Text = "0";
                if (txtCampAdv.Text.Length == 0)
                    txtCampAdv.Text = "0";
                if (txtUnitAdv.Text.Length == 0)
                    txtUnitAdv.Text = "0";

                if (txtHirePerDay.Text.Length == 0)
                    txtHirePerDay.Text = "0";
                if (txtHirePerKM.Text.Length == 0)
                    txtHirePerKM.Text = "0";
                if (txtKmPerDiesel.Text.Length == 0)
                    txtKmPerDiesel.Text = "0";
                if (txtMileage.Text.Length == 0)
                    txtMileage.Text = "0";

                if (txtLoadingCharAmt.Text.Length == 0)
                    txtLoadingCharAmt.Text = "0";
                if (txtUnloadChargeAmt.Text.Length == 0)
                    txtUnloadChargeAmt.Text = "0";
                if (txtComm.Text.Length == 0)
                    txtComm.Text = "0";
                if (txtRta.Text.Length == 0)
                    txtRta.Text = "0";
                if (txtPcTcAmt.Text.Length == 0)
                    txtPcTcAmt.Text = "0";
                if (txtServTax.Text.Length == 0)
                    txtServTax.Text = "0";
                if (txtRepAmt.Text.Length == 0)
                    txtRepAmt.Text = "0";

                if (txtDespatchUnits.Text.Length == 0)
                    txtDespatchUnits.Text = "0";
                if (txtRetUnits.Text.Length == 0)
                    txtRetUnits.Text = "0";
                if (txtRetStockPers.Text.Length == 0)
                    txtRetStockPers.Text = "0";
                if (txtNetRecUnit.Text.Length == 0)
                    txtNetRecUnit.Text = "0";

                if (txtAvgKms.Text.Length == 0)
                    txtAvgKms.Text = "0";
                if (txtPerKMCost.Text.Length == 0)
                    txtPerKMCost.Text = "0";
                if (txtPerUnitCost.Text.Length == 0)
                    txtPerUnitCost.Text = "0";
                if (txtTotWorkDays.Text.Length == 0)
                    txtTotWorkDays.Text = "0";
                if (txtTotKM.Text.Length == 0)
                    txtTotKM.Text = "0";
                if (txtBalAmt.Text.Length == 0)
                    txtBalAmt.Text = "0";
                if (txtOthers.Text.Length == 0)
                    txtOthers.Text = "0";
                if (txtUDTotExp.Text.Length == 0)
                    txtUDTotExp.Text = "0";
                if (txtOslrAmt.Text.Length == 0)
                    txtOslrAmt.Text = "0";
                if (txtPersOfOslr.Text.Length == 0)
                    txtPersOfOslr.Text = "0";
                if (txtNetHire.Text.Length == 0)
                    txtNetHire.Text = "0";
                if (txtGrosHire.Text.Length == 0)
                    txtGrosHire.Text = "0";
                if (txtUDPerKM.Text.Length == 0)
                    txtUDPerKM.Text = "0";
                if (txtUDHirePerDay.Text.Length == 0)
                    txtUDHirePerDay.Text = "0";
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private int SaveTripSheetHeadDetails()
        {
            objSQLdb = new SQLDB();
            string strCommand = "";
            int iRes = 0;

          
            try
            {
                GetTextBoxValues();

               
                if (txtEcodeSearch.Text.Length == 0)
                    txtEcodeSearch.Text = "0";
                if (chkAllColumns.Checked == true)
                    strFillUp = "YES";
                else
                    strFillUp = "NO";
                if (chkAnyAlteration.Checked == true)
                    strAltration = "YES";
                else
                    strAltration = "NO";
                if (chkAnyReadingVar.Checked == true)
                    strReadingDiff = "YES";
                else
                    strReadingDiff = "NO";
                if (chkDiffInKms.Checked == true)
                    strUpDownDiff = "YES";
                else
                    strUpDownDiff = "NO";
                if (chkMileageAchieved.Checked == true)
                    strMileage = "YES";
                else
                    strMileage = "NO";
                if (rdbSTYes.Checked == true)
                    StockFlag = "YES";
                else if (rdbSTNo.Checked == true)
                    StockFlag = "NO";

                if (cbUpDownType.SelectedIndex == 0)
                    sUpDownType = "";
                else if (cbUpDownType.SelectedIndex == 1)
                    sUpDownType = cbUpDownType.Text.ToString();
                else if (cbUpDownType.SelectedIndex == 2)
                    sUpDownType = cbUpDownType.Text.ToString();


                strCommand = "DELETE FROM TRIP_SHEET_PRODUCT_DETL WHERE TSPD_COMP_CODE='"+ sCompCode +
                             "' AND TSPD_BRANCH_CODE='"+ sBranchCode +
                             "' AND TSPD_FIN_YEAR='"+ sFinYear +
                             "' AND TSPD_TRN_NO='"+ txtTrnNo.Text.ToString() +"'";
                strCommand += " DELETE FROM TRIP_SHEET_DELIVERY_DETL WHERE TSD_COMP_CODE='"+ sCompCode +
                              "' AND TSD_BRANCH_CODE='"+ sBranchCode +"' AND TSD_FIN_YEAR='"+ sFinYear +
                              "' AND TSD_TRN_NO='"+ txtTrnNo.Text.ToString() +"'";
                strCommand += " DELETE FROM TRIP_SHEET_DIESEL_EXP WHERE TSDD_COMP_CODE='"+ sCompCode +
                               "' AND TSDD_BRANCH_CODE='"+ sBranchCode +"' AND TSDD_FIN_YEAR='"+ sFinYear +
                               "' AND TSDD_TRN_NO='"+ txtTrnNo.Text.ToString() +"'";
                strCommand += " DELETE FROM TRIP_SHEET_DC_DETL WHERE TSDCD_COMP_CODE='"+ sCompCode +
                                "' AND TSDCD_BRANCH_CODE='"+ sBranchCode +"' AND TSDCD_FIN_YEAR='"+ sFinYear +
                                "' AND TSDCD_TRN_NO='"+ txtTrnNo.Text.ToString() +"'";

                if (flagUpdate == true)
                {
                    strCommand += " UPDATE TRIP_SHEET_HEAD SET TSH_COMP_CODE='"+ sCompCode +
                                                        "', TSH_BRANCH_CODE ='"+ sBranchCode +
                                                        "', TSH_FIN_YEAR ='"+ sFinYear +
                                                        "', TSH_DOC_MONTH='"+ txtDocMonth.Text.ToString().ToUpper() +
                                                        "', TSH_TRIP_TYPE ='"+ cbVehType.Text.ToString() +
                                                        "', TSH_TRIPSHEET_CHAR='"+ cbTripCode.Text.ToString() +
                                                        "', TSH_TRN_NO='"+ txtTrnNo.Text.ToString() +
                                                        "', TSH_TRIP_LRNO='"+ txtTripLRNo.Text.ToString().ToUpper() +
                                                        "', TSH_VEHICLE_NO ='"+ txtVehNo.Text.ToString() +
                                                        "', TSH_VEHICLE_TYPE ='"+ txtVehModel.Text.ToString() +
                                                        "', TSH_VEHICLE_PURPOUSE='"+ cbVehPurpose.Text.ToString() +
                                                        "', TSH_TRANS_NAME='"+ txtTransporterName.Text.ToString() +
                                                        "', TSH_DRIVER_NAME ='"+ txtDriverName.Text.ToString() +
                                                        "', TSH_CLEANER_NAME ='"+ txtCleanerName.Text.ToString() +
                                                        "', TSH_DELIVERY_TO_BRANCH ='"+ cbBranches.SelectedValue.ToString() +
                                                        "', TSH_DELIVERY_TO_ECODE ="+ txtEcodeSearch.Text +"";
                //if (cbVehType.SelectedIndex == 1)
                //{
                    strCommand += ", TSH_AGR_HIRE_DAY="+ txtHirePerDay.Text +
                                   ", TSH_AGR_HIRE_KM ="+ txtHirePerKM.Text +
                                   ", TSH_AGR_HIRE_KMLDIESEL="+ txtKmPerDiesel.Text +
                                   ", TSH_AGR_MILEAGE ="+ txtMileage.Text +"";
                //}

                strCommand += ", TSH_CHK_ALTRATION ='" + strAltration +
                              "',TSH_CHK_FILLUP ='" + strFillUp +
                              "',TSH_CHK_READING ='" + strReadingDiff +
                              "',TSH_CHK_MILEAGE ='" + strMileage +
                              "',TSH_CHK_UPDWN_DIFF='" + strUpDownDiff + "'";

                if (TabExpDetl.SelectedIndex == 0)
                {
                    strCommand += ", TSH_DA_DRIVER="+ txtDriverDA.Text +
                                  ", TSH_DA_CLEANER =" + txtCleanerDa.Text +
                                  ", TSH_EXP_TOT_HIRE ="+ txtHireCharges.Text +
                                  ", TSH_EXP_DIESEL ="+ txtDieselCharges.Text +
                                  ", TSH_EXP_PHONE_BILLS="+ txtPhoneCharges.Text +
                                  ", TSH_EXP_TOLL_GATES="+ txtTollGates.Text +
                                  ", TSH_EXP_OTHERS ="+ txtOtherExp.Text +
                                  ", TSH_EXP_TOTAL ="+ txtTotExp.Text +
                                  ", TSH_EXP_ADV_PAID_UNIT ="+ txtUnitAdv.Text +
                                  ", TSH_EXP_ADV_PAID_CAMP ="+ txtCampAdv.Text +
                                  ", TSH_EXP_BALANCE ="+ txtBalAmt.Text +
                                  ", TSH_DISPATCHED_UNITS ="+ txtDespatchUnits.Text +
                                  ", TSH_RETURNED_UNITS ="+ txtRetUnits.Text +
                                  ", TSH_NET_RECVED_UNITS ="+ txtNetRecUnit.Text +
                                  ", TSH_PERS_STOCK_RETURN ="+ txtRetStockPers.Text +
                                  ", TSH_TRIP_DAYS ="+ txtTotWorkDays.Text +
                                  ", TSH_KM_AVG ="+ txtAvgKms.Text +
                                  ", TSH_COST_PER_KM ="+ txtPerKMCost.Text +
                                  ", TSH_COST_PER_UNIT ="+ txtPerUnitCost.Text +
                                  ", TSH_EXP_RTO_BILLS ="+ txtRTOBills.Text +
                                  ", TSH_EXP_RTO_NOBILLS ="+ txtRTOWithout.Text +
                                  ", TSH_EXP_PCTC ="+ txtPCTCExp.Text +
                                  ", TSH_EXP_DA="+ txtRepandMain.Text +" ";
                }
                if (cbVehType.SelectedIndex == 0 && TabExpDetl.SelectedIndex == 1)
                {
                    strCommand += ", TSH_UPDOWN_TYPE ='"+ sUpDownType +
                                  "', TSH_UD_EXP_LOADING ="+ txtLoadingCharAmt.Text +
                                  ", TSH_UD_EXP_UNLOAD ="+ txtUnloadChargeAmt.Text +
                                  ", TSH_UD_EXP_COMM ="+ txtComm.Text +
                                  ", TSH_UD_EXP_REPAIRS ="+ txtRepAmt.Text +
                                  ", TSH_UD_EXP_RTA ="+ txtRta.Text +
                                  ", TSH_UD_EXP_PCTC ="+ txtPcTcAmt.Text +
                                  ", TSH_UD_EXP_SERVTAX ="+ txtServTax.Text +
                                  ", TSH_UD_EXP_OTHERS ="+ txtOthers.Text +
                                  ", TSH_UD_EXP_TOTALEXP ="+ txtUDTotExp.Text +
                                  ", TSH_UD_GRS_HIRE ="+ txtGrosHire.Text +
                                  ", TSH_UD_NET_HIRE ="+ txtNetHire.Text +
                                  ", TSH_UD_OSLR="+ txtOslrAmt.Text +"";
                }
                strCommand += ",TSH_STOCKTRANSFER_FLAG ='"+ StockFlag +
                               "',TSH_REMARKS='"+ txtRemarks.Text.ToString().Replace("\'"," ") +
                               "', TSH_MODIFIED_BY ='"+ CommonData.LogUserId +
                               "',TSH_MODIFIED_DATE=getdate() "+
                               "  WHERE TSH_COMP_CODE='" + sCompCode +
                               "' AND TSH_BRANCH_CODE='" + sBranchCode + "' AND TSH_FIN_YEAR='" + sFinYear +
                               "' AND TSH_TRN_NO='" + txtTrnNo.Text.ToString() + "' ";
                    
                }

                else if (flagUpdate == false)
                {

                    strCommand = "INSERT INTO TRIP_SHEET_HEAD (TSH_COMP_CODE " +
                                                           ", TSH_BRANCH_CODE " +
                                                           ", TSH_FIN_YEAR " +
                                                           ", TSH_DOC_MONTH " +
                                                           ", TSH_TRIP_TYPE " +
                                                           ", TSH_TRIPSHEET_CHAR " +
                                                           ", TSH_TRN_NO " +
                                                           ", TSH_TRIP_LRNO " +
                                                           ", TSH_VEHICLE_NO " +
                                                           ", TSH_VEHICLE_TYPE " +
                                                           ", TSH_VEHICLE_PURPOUSE " +
                                                           ", TSH_TRANS_NAME " +
                                                           ", TSH_DRIVER_NAME " +
                                                           ", TSH_CLEANER_NAME " +
                                                           ", TSH_DELIVERY_TO_BRANCH " +
                                                          ", TSH_DELIVERY_TO_ECODE ";
                    //if (cbVehType.SelectedIndex == 1)
                    //{
                           strCommand += ", TSH_AGR_HIRE_DAY " +
                                                        ", TSH_AGR_HIRE_KM " +
                                                        ", TSH_AGR_HIRE_KMLDIESEL " +
                                                        ", TSH_AGR_MILEAGE ";
                    //}

                    strCommand += ", TSH_CHK_ALTRATION " +
                                                 ", TSH_CHK_FILLUP " +
                                                 ", TSH_CHK_READING " +
                                                 ", TSH_CHK_MILEAGE " +
                                                 ", TSH_CHK_UPDWN_DIFF";
                    if (TabExpDetl.SelectedIndex == 0)
                    {
                        strCommand += ", TSH_DA_DRIVER " +
                                                            ", TSH_DA_CLEANER " +
                                                            ", TSH_EXP_TOT_HIRE " +
                                                            ", TSH_EXP_DIESEL " +
                                                            ", TSH_EXP_PHONE_BILLS " +
                                                            ", TSH_EXP_TOLL_GATES " +
                                                            ", TSH_EXP_OTHERS " +
                                                            ", TSH_EXP_TOTAL " +
                                                            ", TSH_EXP_ADV_PAID_UNIT " +
                                                            ", TSH_EXP_ADV_PAID_CAMP " +
                                                            ", TSH_EXP_BALANCE " +
                                                            ", TSH_DISPATCHED_UNITS " +
                                                            ", TSH_RETURNED_UNITS " +
                                                            ", TSH_NET_RECVED_UNITS " +
                                                            ", TSH_PERS_STOCK_RETURN " +
                                                            ", TSH_TRIP_DAYS " +
                                                            ", TSH_KM_AVG " +
                                                            ", TSH_COST_PER_KM " +
                                                            ", TSH_COST_PER_UNIT " +
                                                            ", TSH_EXP_RTO_BILLS " +
                                                            ", TSH_EXP_RTO_NOBILLS " +
                                                            ", TSH_EXP_PCTC " +
                                                            ", TSH_EXP_DA ";
                    }
                    if (cbVehType.SelectedIndex == 0 && TabExpDetl.SelectedIndex == 1)
                    {
                        strCommand += ", TSH_UPDOWN_TYPE " +
                                                            ", TSH_UD_EXP_LOADING " +
                                                            ", TSH_UD_EXP_UNLOAD " +
                                                            ", TSH_UD_EXP_COMM " +
                                                            ", TSH_UD_EXP_REPAIRS " +
                                                            ", TSH_UD_EXP_RTA " +
                                                            ", TSH_UD_EXP_PCTC " +
                                                            ", TSH_UD_EXP_SERVTAX " +
                                                            ", TSH_UD_EXP_OTHERS " +
                                                            ", TSH_UD_EXP_TOTALEXP " +
                                                            ", TSH_UD_GRS_HIRE " +
                                                            ", TSH_UD_NET_HIRE " +
                                                            ", TSH_UD_OSLR";
                    }
                    strCommand += ",TSH_STOCKTRANSFER_FLAG " +
                                                        ", TSH_REMARKS " +
                                                        ", TSH_CREATED_BY " +
                                                        ", TSH_CREATED_DATE)" +
                                                        " VALUES " +
                                                        "('" + sCompCode +
                                                        "','" + sBranchCode +
                                                        "','" + sFinYear +
                                                        "','" + txtDocMonth.Text.ToString() +
                                                        "','" + cbVehType.Text.ToString() +
                                                        "','" + cbTripCode.Text.ToString() +
                                                        "','" + txtTrnNo.Text.ToString() +
                                                        "','" + txtTripLRNo.Text.ToString() +
                                                        "','" + txtVehNo.Text.ToString() +
                                                        "','" + txtVehModel.Text.ToString() +
                                                        "','" + cbVehPurpose.Text.ToString() +
                                                        "','" + txtTransporterName.Text.ToString() +
                                                        "','" + txtDriverName.Text.ToString() +
                                                        "','" + txtCleanerName.Text.ToString() +
                                                        "','" + cbBranches.SelectedValue.ToString() +
                                                        "'," + Convert.ToInt32(txtEcodeSearch.Text) + "";
                    //if (cbVehType.SelectedIndex == 1)
                    //{
                    strCommand += "," + txtHirePerDay.Text +
                                                        "," + txtHirePerKM.Text +
                                                        "," + txtKmPerDiesel.Text +
                                                        "," + txtMileage.Text + "";
                    //}
                    strCommand += ",'" + strAltration + "','" + strFillUp +
                                    "','" + strReadingDiff + "','" + strMileage + "','" + strUpDownDiff + "'";
                    if (TabExpDetl.SelectedIndex == 0)
                    {

                        strCommand += "," + txtDriverDA.Text +
                                        "," + txtCleanerDa.Text +
                                        "," + txtHireCharges.Text +
                                        "," + txtDieselCharges.Text +
                                        "," + txtPhoneCharges.Text +
                                        ", " + txtTollGates.Text +
                                        "," + txtOtherExp.Text +
                                        "," + txtTotExp.Text +
                                        "," + txtUnitAdv.Text +
                                        "," + txtCampAdv.Text +
                                        "," + txtBalAmt.Text +
                                        "," + txtDespatchUnits.Text +
                                        "," + txtRetUnits.Text +
                                        "," + txtNetRecUnit.Text +
                                        "," + txtRetStockPers.Text +
                                        "," + Convert.ToDouble(txtTotWorkDays.Text) +
                                        "," + txtAvgKms.Text +
                                        "," + txtPerKMCost.Text +
                                        "," + txtPerUnitCost.Text +
                                        "," + txtRTOBills.Text +
                                        "," + txtRTOWithout.Text +
                                        "," + txtPCTCExp.Text +
                                        "," + txtRepandMain.Text + " ";
                    }

                    if (cbVehType.SelectedIndex == 0 && TabExpDetl.SelectedIndex==1)
                    {
                        strCommand += ",'" + sUpDownType +
                                        "'," + txtLoadingCharAmt.Text +
                                         "," + txtUnloadChargeAmt.Text +
                                         "," + txtComm.Text +
                                         "," + txtRepAmt.Text +
                                         "," + txtRta.Text +
                                         "," + txtPcTcAmt.Text +
                                         "," + txtServTax.Text +
                                         "," + txtOthers.Text +
                                         "," + txtUDTotExp.Text +
                                         "," + txtGrosHire.Text +
                                         "," + txtNetHire.Text +
                                         "," + txtOslrAmt.Text + " ";

                    }


                    strCommand += ",'" + StockFlag + "','" + txtRemarks.Text.ToString().Replace("'", " ") +
                                    "','" + CommonData.LogUserId + "',getdate())";
                }

                if (strCommand.Length > 10)
                {
                    iRes = objSQLdb.ExecuteSaveData(strCommand);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iRes;
        }


        private int SaveTripDieselExpDetails()
        {
            objSQLdb = new SQLDB();
            string strCmd = "";
            int iRes = 0;

            try
            {
                if (gvDieselDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < gvDieselDetails.Rows.Count; i++)
                    {
                        strCmd += "INSERT INTO TRIP_SHEET_DIESEL_EXP(TSDD_COMP_CODE " +
                                                                 ", TSDD_BRANCH_CODE " +
                                                                 ", TSDD_FIN_YEAR " +
                                                                 ", TSDD_TRIP_TYPE " +
                                                                 ", TSDD_TRN_NO " +
                                                                 ", TSDD_SL_NO " +
                                                                 ", TSDD_DATE " +
                                                                 ", TSDD_BILL_NO " +
                                                                 ", TSDD_PRICE_LTR " +
                                                                 ", TSDD_TOT_LTRS " +
                                                                 ", TSDD_AMOUNT " +
                                                                 ")VALUES "+
                                                                 "('"+ sCompCode +
                                                                 "','"+ sBranchCode +
                                                                 "','"+ sFinYear +
                                                                 "','"+ cbVehType.Text.ToString() +
                                                                 "','"+ txtTrnNo.Text.ToString() +
                                                                 "',"+ Convert.ToInt32(gvDieselDetails.Rows[i].Cells["SlNo_Diesel"].Value) +
                                                                 ",'"+ Convert.ToDateTime(gvDieselDetails.Rows[i].Cells["BillDate"].Value).ToString("dd/MMM/yyyy") +
                                                                 "','"+ gvDieselDetails.Rows[i].Cells["BillNo"].Value.ToString() +
                                                                 "',"+ Convert.ToDouble(gvDieselDetails.Rows[i].Cells["CostPerLtr"].Value) +
                                                                 "," + Convert.ToDouble(gvDieselDetails.Rows[i].Cells["NoOfLtrs"].Value) + 
                                                                 ","+ Convert.ToDouble(gvDieselDetails.Rows[i].Cells["BillTotAmt"].Value) +")";
                    }
                }

                if (strCmd.Length > 10)
                {
                    iRes = objSQLdb.ExecuteSaveData(strCmd);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iRes;
        }


        private int SaveDCorDCRDetails()
        {
            objSQLdb = new SQLDB();
            string strCmd = "";
            int iRes = 0;

            try
            {

                if (gvDCorDCSTDetl.Rows.Count > 0)
                {
                    for (int i = 0; i < gvDCorDCSTDetl.Rows.Count; i++)
                    {

                        strCmd += "INSERT INTO TRIP_SHEET_DC_DETL(TSDCD_COMP_CODE " +
                                                              ", TSDCD_BRANCH_CODE " +
                                                              ", TSDCD_FIN_YEAR " +
                                                              ", TSDCD_TRIP_TYPE " +
                                                              ", TSDCD_TRN_NO " +
                                                              ", TSDCD_SLNO " +
                                                              ", TSDCD_DCDCST_NO " +
                                                              ", TSDCD_DCREF_NO " +
                                                              ")VALUES " +
                                                              "('" + sCompCode +
                                                              "','" + sBranchCode +
                                                              "','" + sFinYear +
                                                              "','" + cbVehType.Text.ToString() +
                                                              "','" + txtTrnNo.Text.ToString() +
                                                              "'," + Convert.ToInt32(gvDCorDCSTDetl.Rows[i].Cells["SlNo_DC"].Value) + 
                                                              ",'"+ gvDCorDCSTDetl.Rows[i].Cells["DCorDCSTNo"].Value.ToString() +
                                                              "','"+ gvDCorDCSTDetl.Rows[i].Cells["DCRefNo"].Value.ToString() +"')";
                    }
                }

                if (strCmd.Length > 10)
                {
                    iRes = objSQLdb.ExecuteSaveData(strCmd);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return iRes;
        }

        private int SaveTripDeliveryDetl()
        {
            objSQLdb = new SQLDB();
            string strCmd = "";
            int iRes = 0;
            try
            {
                if (gvDeliveryDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < gvDeliveryDetails.Rows.Count; i++)
                    {

                        strCmd += "INSERT INTO TRIP_SHEET_DELIVERY_DETL(TSD_COMP_CODE " +
                                                                    ", TSD_BRANCH_CODE " +
                                                                    ", TSD_FIN_YEAR " +
                                                                    ", TSD_TRIP_TYPE " +
                                                                    ", TSD_TRN_NO " +
                                                                    ", TSD_SL_NO " +
                                                                    ", TSD_START_DATE " +
                                                                    ", TSD_START_TIME " +
                                                                    ", TSD_START_DATETIME " +
                                                                    ", TSD_START_READING " +
                                                                    ", TSD_START_PLACE " +
                                                                    ", TSD_END_DATE " +
                                                                    ", TSD_END_TIME " +
                                                                    ", TSD_END_DATETIME " +
                                                                    ", TSD_END_READING " +
                                                                    ", TSD_END_PLACE " +
                                                                    ", TSD_KMS_TRLVD " +
                                                                    ", TSD_TOT_DAYS " +
                                                                    ", TSD_TOT_DELUNITS " +
                                                                    ", TSD_TOT_CUST " +
                                                                    ", TSD_DA_DRIVER " +
                                                                    ", TSD_DA_CLEANER " +
                                                                    ", TSD_EORA_CODE " +
                                                                    ")VALUES " +
                                                                    "('"+ sCompCode +
                                                                    "','"+ sBranchCode +
                                                                    "','"+ sFinYear +
                                                                    "','"+ cbVehType.Text.ToString() +
                                                                    "','"+ txtTrnNo.Text.ToString() +
                                                                    "',"+ Convert.ToInt32(gvDeliveryDetails.Rows[i].Cells["SlNo_Delv"].Value) +
                                                                    ",'"+ Convert.ToDateTime(gvDeliveryDetails.Rows[i].Cells["StartDate"].Value).ToString("dd/MMM/yyyy") +
                                                                    "'," + gvDeliveryDetails.Rows[i].Cells["StartTime"].Value.ToString() +
                                                                    ",'" + gvDeliveryDetails.Rows[i].Cells["STDateTime"].Value +
                                                                    "'," + Convert.ToInt32(gvDeliveryDetails.Rows[i].Cells["StartReading"].Value) +
                                                                    ",'"+ gvDeliveryDetails.Rows[i].Cells["StartPlace"].Value.ToString() +
                                                                    "','" + Convert.ToDateTime(gvDeliveryDetails.Rows[i].Cells["EndDate"].Value).ToString("dd/MMM/yyyy") +
                                                                    "'," + gvDeliveryDetails.Rows[i].Cells["EndTime"].Value.ToString() +
                                                                    ",'" + gvDeliveryDetails.Rows[i].Cells["EndDateTime"].Value +
                                                                    "'," + Convert.ToInt32(gvDeliveryDetails.Rows[i].Cells["EndReading"].Value) +
                                                                    ",'" + gvDeliveryDetails.Rows[i].Cells["EndPlace"].Value.ToString() +
                                                                    "'," + Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["NoOfKM"].Value) +
                                                                    "," + Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["TotDays"].Value) +
                                                                    "," + Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["NoOfUnitsDel"].Value) +
                                                                    "," + Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["NoOfCustCov"].Value) +
                                                                    "," + Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["DriverDA"].Value) +
                                                                    "," + Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["CleanerDA"].Value) +
                                                                    "," + Convert.ToInt32(gvDeliveryDetails.Rows[i].Cells["GLEcode"].Value) + ")";
                    }
                }

                if (strCmd.Length > 10)
                {
                    iRes = objSQLdb.ExecuteSaveData(strCmd);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return iRes;
        }

        private int SaveProductDetails()
        {
            objSQLdb = new SQLDB();
            string strCmd = "";
            int iRes = 0;
            try
            {
                if (gvDeliverProducts.Rows.Count > 0)
                {
                    for (int i = 0; i < gvDeliverProducts.Rows.Count; i++)
                    {
                        strCmd += "INSERT INTO TRIP_SHEET_PRODUCT_DETL(TSPD_COMP_CODE " +
                                                                   ", TSPD_BRANCH_CODE " +
                                                                   ", TSPD_FIN_YEAR " +
                                                                   ", TSPD_TRIP_TYPE " +
                                                                   ", TSPD_TRN_NO " +
                                                                   ", TSPD_SL_NO " +
                                                                   ", TSPD_START_READING_KEY " +
                                                                   ", TSPD_PRODUCT_ID " +
                                                                   ", TSPD_DISP_QTY " +
                                                                   ")VALUES " +
                                                                   "('" + sCompCode +
                                                                   "','" + sBranchCode +
                                                                   "','" + sFinYear +
                                                                   "','" + cbVehType.Text.ToString() +
                                                                   "','" + txtTrnNo.Text.ToString() +
                                                                   "'," + Convert.ToInt32(gvDeliverProducts.Rows[i].Cells["SlNo_Prod"].Value) +
                                                                   "," + Convert.ToInt32(gvDeliverProducts.Rows[i].Cells["StartMetreReading"].Value) +
                                                                   ",'" + gvDeliverProducts.Rows[i].Cells["ProductId"].Value.ToString() +
                                                                   "'," + Convert.ToDouble(gvDeliverProducts.Rows[i].Cells["DespatchQty"].Value).ToString("0.00") +")";
                    }
                }

                if (strCmd.Length > 10)
                {
                    iRes = objSQLdb.ExecuteSaveData(strCmd);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return iRes;
        }

        private void txtRepandMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtTotExp_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtDespatchUnits_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtDieselCharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtDriverDA_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtRTOBills_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtUnitAdv_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtRetUnits_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtPhoneCharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }
             
        private void txtRTOWithout_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);

        }

        private void txtCampAdv_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtNetRecUnit_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtTollGates_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtPCTCExp_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtBalAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtRetStockPers_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtHirePerDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtHirePerKM_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtKmPerDiesel_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtMileage_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtTotWorkDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtAvgKms_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtPerKMCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtPerUnitCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtRta_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtOthers_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtOslrAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtLoadingCharAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtPcTcAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtUDTotExp_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender,e);
        }

        private void txtUDHirePerDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender, e);
        }

        private void txtUnloadChargeAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender, e);
        }

        private void txtServTax_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender, e);
        }

        private void txtGrosHire_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender, e);
        }

        private void txtUDPerKM_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender, e);
        }

        private void txtComm_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender, e);
        }

        private void txtRepAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender, e);
        }

        private void txtNetHire_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender, e);
        }

        private void txtPersOfOslr_KeyPress(object sender, KeyPressEventArgs e)
        {
            isDigitsCheck(sender, e);
        }

        #region  "GRIDVIEW DETAILS"
        public void GetTripDeliveryDetails()
        {
            int intRow = 1;
            gvDeliveryDetails.Rows.Clear();           
           
            
            try
            {
                if (dtDeliveryDetl.Rows.Count > 0)
                {
                    for (int i = 0; i < dtDeliveryDetl.Rows.Count; i++)
                    {
                                             
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = intRow;
                        dtDeliveryDetl.Rows[i]["SlNo_Delv"] = intRow;
                        tempRow.Cells.Add(cellSLNO);

                        DataGridViewCell cellGlEcode = new DataGridViewTextBoxCell();
                        cellGlEcode.Value = dtDeliveryDetl.Rows[i]["GLEcode"].ToString(); 
                        tempRow.Cells.Add(cellGlEcode);

                        DataGridViewCell cellStDateTime = new DataGridViewTextBoxCell();
                        cellStDateTime.Value = dtDeliveryDetl.Rows[i]["STDateTime"];
                        tempRow.Cells.Add(cellStDateTime);

                        DataGridViewCell cellEndDateTime = new DataGridViewTextBoxCell();
                        cellEndDateTime.Value = dtDeliveryDetl.Rows[i]["EndDateTime"];
                        tempRow.Cells.Add(cellEndDateTime);


                        DataGridViewCell cellStartingDate = new DataGridViewTextBoxCell();
                        cellStartingDate.Value = Convert.ToDateTime(dtDeliveryDetl.Rows[i]["StartDate"]).ToString("dd/MMM/yyyy");
                        tempRow.Cells.Add(cellStartingDate);

                        DataGridViewCell cellStartTime = new DataGridViewTextBoxCell();
                        cellStartTime.Value = dtDeliveryDetl.Rows[i]["StartTime"].ToString(); 
                        tempRow.Cells.Add(cellStartTime);

                        DataGridViewCell cellStartPlace = new DataGridViewTextBoxCell();
                        cellStartPlace.Value = dtDeliveryDetl.Rows[i]["StartPlace"].ToString(); 
                        tempRow.Cells.Add(cellStartPlace);

                        DataGridViewCell cellStartReading = new DataGridViewTextBoxCell();
                        cellStartReading.Value = dtDeliveryDetl.Rows[i]["StartReading"].ToString(); 
                        tempRow.Cells.Add(cellStartReading);

                        DataGridViewCell cellClosingDate = new DataGridViewTextBoxCell();
                        cellClosingDate.Value = Convert.ToDateTime(dtDeliveryDetl.Rows[i]["EndDate"]).ToString("dd/MMM/yyyy");
                        tempRow.Cells.Add(cellClosingDate);

                        DataGridViewCell cellEndTime = new DataGridViewTextBoxCell();
                        cellEndTime.Value = dtDeliveryDetl.Rows[i]["EndTime"].ToString(); ;
                        tempRow.Cells.Add(cellEndTime);

                        DataGridViewCell cellClosingPlace = new DataGridViewTextBoxCell();
                        cellClosingPlace.Value = dtDeliveryDetl.Rows[i]["EndPlace"].ToString(); 
                        tempRow.Cells.Add(cellClosingPlace);

                        DataGridViewCell cellEndReading = new DataGridViewTextBoxCell();
                        cellEndReading.Value = dtDeliveryDetl.Rows[i]["EndReading"].ToString(); 
                        tempRow.Cells.Add(cellEndReading);

                        DataGridViewCell cellNoOfKm = new DataGridViewTextBoxCell();
                        cellNoOfKm.Value = dtDeliveryDetl.Rows[i]["NoOfKM"].ToString(); 
                        tempRow.Cells.Add(cellNoOfKm);

                        DataGridViewCell cellTotUnits = new DataGridViewTextBoxCell();
                        cellTotUnits.Value = dtDeliveryDetl.Rows[i]["NoOfUnitsDel"].ToString(); 
                        tempRow.Cells.Add(cellTotUnits);

                        DataGridViewCell cellTotCust = new DataGridViewTextBoxCell();
                        cellTotCust.Value = dtDeliveryDetl.Rows[i]["NoOfCustCov"].ToString(); 
                        tempRow.Cells.Add(cellTotCust);

                        DataGridViewCell cellTotDays = new DataGridViewTextBoxCell();
                        cellTotDays.Value = dtDeliveryDetl.Rows[i]["TotDays"].ToString(); 
                        tempRow.Cells.Add(cellTotDays);

                        DataGridViewCell cellDriverDA = new DataGridViewTextBoxCell();
                        cellDriverDA.Value = dtDeliveryDetl.Rows[i]["DriverDA"].ToString(); 
                        tempRow.Cells.Add(cellDriverDA);

                        DataGridViewCell cellCleanerDA = new DataGridViewTextBoxCell();
                        cellCleanerDA.Value = dtDeliveryDetl.Rows[i]["CleanerDA"].ToString(); 
                        tempRow.Cells.Add(cellCleanerDA);

                        DataGridViewCell cellGLName = new DataGridViewTextBoxCell();
                        cellGLName.Value = dtDeliveryDetl.Rows[i]["GLName"].ToString(); 
                        tempRow.Cells.Add(cellGLName);

                        intRow = intRow + 1;
                        gvDeliveryDetails.Rows.Add(tempRow);
                    }
                }
                CalulateTotalTripDays();
                CalculateTotalKMs();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        } 
        
        public void GetDeliverProductDetails()
        {
            int intRow = 1;
            gvDeliverProducts.Rows.Clear();

            try
            {
                if (dtProductDetl.Rows.Count > 0)
                {

                    for (int i = 0; i < dtProductDetl.Rows.Count; i++)
                    {
                       
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = intRow;
                        dtProductDetl.Rows[i]["SlNo_Prod"] = intRow;
                        tempRow.Cells.Add(cellSLNO);


                        DataGridViewCell cellProdId = new DataGridViewTextBoxCell();
                        cellProdId.Value = dtProductDetl.Rows[i]["ProductId"].ToString();
                        tempRow.Cells.Add(cellProdId);

                        DataGridViewCell cellStReading = new DataGridViewTextBoxCell();
                        cellStReading.Value = dtProductDetl.Rows[i]["StartReading"].ToString();
                        tempRow.Cells.Add(cellStReading);

                        DataGridViewCell cellProdName = new DataGridViewTextBoxCell();
                        cellProdName.Value = dtProductDetl.Rows[i]["ProdName"].ToString();
                        tempRow.Cells.Add(cellProdName);

                        DataGridViewCell cellCategoryName = new DataGridViewTextBoxCell();
                        cellCategoryName.Value = dtProductDetl.Rows[i]["CategoryName"].ToString();
                        tempRow.Cells.Add(cellCategoryName);

                        DataGridViewCell cellDispatchQty = new DataGridViewTextBoxCell();
                        cellDispatchQty.Value = dtProductDetl.Rows[i]["DespatchQty"].ToString();
                        tempRow.Cells.Add(cellDispatchQty);                                         
                                           
                        intRow = intRow + 1;
                        gvDeliverProducts.Rows.Add(tempRow);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
                      
        }

        #endregion
                
        #region "CALCULATIONS"

        private void CalculateUpDownTotals()
        {
            double LoadChargesAmt, UnLoadChargesAmt, Commission, RTAAmt, PcTcAmount, ServiceTaxAmt, RepairsAmt, TotAmt, OtherAmt;
            LoadChargesAmt = UnLoadChargesAmt = Commission = RTAAmt = PcTcAmount = ServiceTaxAmt = RepairsAmt = TotAmt = OtherAmt = 0;

            if (txtLoadingCharAmt.Text.Length > 0)
                LoadChargesAmt = Convert.ToDouble(txtLoadingCharAmt.Text);
            if (txtUnloadChargeAmt.Text.Length > 0)
                UnLoadChargesAmt = Convert.ToDouble(txtUnloadChargeAmt.Text);
            if (txtComm.Text.Length > 0)
                Commission = Convert.ToDouble(txtComm.Text);
            if (txtRta.Text.Length > 0)
                RTAAmt = Convert.ToDouble(txtRta.Text);
            if (txtPcTcAmt.Text.Length > 0)
                PcTcAmount = Convert.ToDouble(txtPcTcAmt.Text);
            if (txtServTax.Text.Length > 0)
                ServiceTaxAmt = Convert.ToDouble(txtServTax.Text);
            if (txtRepAmt.Text.Length > 0)
                RepairsAmt = Convert.ToDouble(txtRepAmt.Text);
            if (txtOthers.Text.Length > 0)
                OtherAmt = Convert.ToDouble(txtOthers.Text);


            TotAmt = Convert.ToDouble(LoadChargesAmt + UnLoadChargesAmt + Commission + RTAAmt + PcTcAmount + ServiceTaxAmt + RepairsAmt + OtherAmt);
            txtUDTotExp.Text = Convert.ToString(TotAmt);


        }

        public void CalculationForQTY()
        {
            double TotDespatchUnits, ReturnUnits, NetUnits, PercOfStockReturn;
            TotDespatchUnits = ReturnUnits = NetUnits = PercOfStockReturn = 0;

            if (txtDespatchUnits.Text.Length > 0)
                TotDespatchUnits = Convert.ToDouble(txtDespatchUnits.Text);
            if (txtRetUnits.Text.Length > 0)
                ReturnUnits = Convert.ToDouble(txtRetUnits.Text);
            if (txtRetUnits.Text.Length > 0)
                NetUnits = Convert.ToDouble(Convert.ToInt32(TotDespatchUnits) - Convert.ToInt32(ReturnUnits));
            else
                NetUnits = TotDespatchUnits;
            txtNetRecUnit.Text = NetUnits.ToString("0");

            if (ReturnUnits != 0)
            {
                PercOfStockReturn = Convert.ToDouble((ReturnUnits / TotDespatchUnits) * 100);
            }

            txtRetStockPers.Text = Convert.ToDouble(PercOfStockReturn).ToString("0.00");

        }

        private void CalculateTotalandBalanceAmount()
        {
            double HireChargesAmt, DieselChargesAmt, PhoneChargesAmt, TollGateCharges, OtherCharges, RepandMainCharges, RtoChargesAmt,
                RtoWOutCharges, PCTCAmt, CleanerDAAmt, DriverDAAmt, AdvAtBranch, AdvAtCamp, BalanceAmt, TotAmount, TotAdvAmt;
            HireChargesAmt = DieselChargesAmt = PhoneChargesAmt = TollGateCharges = OtherCharges = RepandMainCharges = RtoChargesAmt =
                RtoWOutCharges = PCTCAmt = CleanerDAAmt = DriverDAAmt = AdvAtBranch = AdvAtCamp = BalanceAmt = TotAmount = TotAdvAmt = 0;

            if (txtHireCharges.Text.Length > 0)
                HireChargesAmt = Convert.ToDouble(txtHireCharges.Text);
            if (txtDieselCharges.Text.Length > 0)
                DieselChargesAmt = Convert.ToDouble(txtDieselCharges.Text);
            if (txtPhoneCharges.Text.Length > 0)
                PhoneChargesAmt = Convert.ToDouble(txtPhoneCharges.Text);
            if (txtTollGates.Text.Length > 0)
                TollGateCharges = Convert.ToDouble(txtTollGates.Text);
            if (txtOtherExp.Text.Length > 0)
                OtherCharges = Convert.ToDouble(txtOtherExp.Text);
            if (txtOtherExp.Text.Length > 0)
                OtherCharges = Convert.ToDouble(txtOtherExp.Text);
            if (txtRepandMain.Text.Length > 0)
                RepandMainCharges = Convert.ToDouble(txtRepandMain.Text);
            if (txtRTOBills.Text.Length > 0)
                RtoChargesAmt = Convert.ToDouble(txtRTOBills.Text);
            if (txtRTOWithout.Text.Length > 0)
                RtoWOutCharges = Convert.ToDouble(txtRTOWithout.Text);
            if (txtPCTCExp.Text.Length > 0)
                PCTCAmt = Convert.ToDouble(txtPCTCExp.Text);
            if (txtCleanerDa.Text.Length > 0)
                CleanerDAAmt = Convert.ToDouble(txtCleanerDa.Text);
            if (txtDriverDA.Text.Length > 0)
                DriverDAAmt = Convert.ToDouble(txtDriverDA.Text);

            if (txtCampAdv.Text.Length > 0)
                AdvAtCamp = Convert.ToDouble(txtCampAdv.Text);
            if (txtUnitAdv.Text.Length > 0)
                AdvAtBranch = Convert.ToDouble(txtUnitAdv.Text);

            TotAdvAmt = (AdvAtCamp + AdvAtBranch);

            TotAmount = Convert.ToDouble(HireChargesAmt + DieselChargesAmt + PhoneChargesAmt + TollGateCharges + OtherCharges + RepandMainCharges + RtoChargesAmt + RtoWOutCharges + PCTCAmt + CleanerDAAmt + DriverDAAmt);
            txtTotExp.Text = Convert.ToString(TotAmount);

            if (TotAdvAmt != 0)
            {
                if (TotAmount > TotAdvAmt)
                    BalanceAmt = Convert.ToDouble(Convert.ToInt32(TotAmount) - Convert.ToInt32(TotAdvAmt));
                else
                    BalanceAmt = Convert.ToDouble(Convert.ToInt32(TotAdvAmt) - Convert.ToInt32(TotAmount));
            }

            txtBalAmt.Text = (BalanceAmt).ToString("0.00");
            CalculatePerKMCost();
        }

        public void CalculateTotalUnits()
        {
            double TotDcUnits = 0;

            try
            {
                if (gvDCorDCSTDetl.Rows.Count > 0)
                {
                    for (int i = 0; i < gvDCorDCSTDetl.Rows.Count; i++)
                    {
                        if (Convert.ToString(gvDCorDCSTDetl.Rows[i].Cells["DCQty"].Value) != "")
                        {
                            TotDcUnits += Convert.ToDouble(gvDCorDCSTDetl.Rows[i].Cells["DCQty"].Value);
                        }
                    }
                }

                txtDespatchUnits.Text = (TotDcUnits).ToString("0");
                txtNetRecUnit.Text = (TotDcUnits).ToString("0");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void CalulateTotalTripDays()
        {           
            Int32 dTotDays = 0;
            DateTime dtpStartDate, dtpEndDate;
            Int32 nRowCount = 0;

            try
            {
                if (gvDeliveryDetails.Rows.Count > 0)
                {

                    nRowCount = gvDeliveryDetails.Rows.Count - 1;
                    dtpStartDate = Convert.ToDateTime(gvDeliveryDetails.Rows[0].Cells["StartDate"].Value);
                    dtpEndDate = Convert.ToDateTime(gvDeliveryDetails.Rows[nRowCount].Cells["EndDate"].Value);

                    if ((dtpStartDate > dtpEndDate))
                    {
                        dtpStartDate = dtpEndDate;
                    }
                    else
                    {
                        double TotDays = (dtpEndDate - dtpStartDate).TotalDays;
                        dTotDays = Convert.ToInt32(TotDays);
                    }

                    if (dTotDays != 0)
                        txtTotWorkDays.Text = dTotDays.ToString("f");
                    else
                        txtTotWorkDays.Text = "1";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        public void CalculateTotalKMs()
        {
            double dTotKMs, dAvgKms, dTotdays;
            dTotKMs = dAvgKms = dTotdays = 0.00;

            try
            {
                if (gvDeliveryDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < gvDeliveryDetails.Rows.Count; i++)
                    {
                        if (Convert.ToString(gvDeliveryDetails.Rows[i].Cells["NoOfKM"].Value) != "")
                        {
                            dTotKMs += Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["NoOfKM"].Value);
                        }
                    }
                }

                txtTotKM.Text = dTotKMs.ToString("0");
                if (txtTotWorkDays.Text != "")
                    dTotdays = Convert.ToDouble(txtTotWorkDays.Text);

                if (dTotKMs != 0 && dTotdays != 0)
                {
                    dAvgKms = Convert.ToDouble(Convert.ToInt32(dTotKMs / dTotdays));
                    txtAvgKms.Text = Convert.ToDouble(dAvgKms).ToString("0.00");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void CalculatePerKMCost()
        {
            double dKMCost, dTotAmt, dTotKm;
            dKMCost = dTotAmt = dTotKm = 0;

            try
            {
                if (txtTotExp.Text.Length > 0)
                    dTotAmt = Convert.ToDouble(txtTotExp.Text);
                if (txtTotKM.Text.Length > 0)
                    dTotKm = Convert.ToDouble(txtTotKM.Text);

                if (dTotAmt != 0 && dTotKm!=0)
                    dKMCost = Convert.ToDouble((dTotAmt) /(dTotKm));
                txtPerKMCost.Text = Convert.ToDouble(dKMCost).ToString("0.00");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        public void CalculateDieselTotalCost()
        {
            double DieselTotCost = 0;

            try
            {
                if (gvDieselDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < gvDieselDetails.Rows.Count; i++)
                    {
                        if (Convert.ToString(gvDieselDetails.Rows[i].Cells["BillTotAmt"].Value) != "")
                        {
                            DieselTotCost += Convert.ToDouble(gvDieselDetails.Rows[i].Cells["BillTotAmt"].Value);
                        }
                    }
                }

                txtDieselCharges.Text = (DieselTotCost).ToString("0");               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void CalculatePerUnitCost()
        {
            double dUnitCost, dTotAmt, dRecUnits;
            dUnitCost = dTotAmt = dRecUnits = 0;

            if (txtTotExp.Text.Length > 0)
                dTotAmt = Convert.ToDouble(txtTotExp.Text);

            if (txtNetRecUnit.Text != "")
                dRecUnits = Convert.ToDouble(txtNetRecUnit.Text);

            if (txtTotExp.Text != "")
                dTotAmt = Convert.ToDouble(txtTotExp.Text);
            if (dTotAmt != 0 && dRecUnits != 0)
                dUnitCost = Convert.ToDouble(Convert.ToInt32(dTotAmt / dRecUnits));

            txtPerUnitCost.Text = Convert.ToDouble(dUnitCost).ToString("0.00");
        }


        #endregion


        private void txtHireCharges_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalandBalanceAmount();
        }

        private void txtOtherExp_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalandBalanceAmount();
        }

        private void txtRepandMain_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalandBalanceAmount();
        }

        private void txtDieselCharges_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalandBalanceAmount();
        }

        private void txtDriverDA_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalandBalanceAmount();
        }

        private void txtRTOBills_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalandBalanceAmount();
        }

        private void txtPhoneCharges_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalandBalanceAmount();
        }

        private void txtCleanerDa_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalandBalanceAmount();
        }

        private void txtRTOWithout_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalandBalanceAmount();
        }

        private void txtTollGates_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalandBalanceAmount();
        }

        private void txtPCTCExp_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalandBalanceAmount();
        }

        private void txtUnitAdv_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalandBalanceAmount();
        }

        private void txtCampAdv_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalandBalanceAmount();
        }

        private void txtRta_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateUpDownTotals();
        }

        private void txtOthers_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateUpDownTotals();
        }

        private void txtOslrAmt_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateUpDownTotals();
        }

        private void txtLoadingCharAmt_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateUpDownTotals();
        }

        private void txtPcTcAmt_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateUpDownTotals();
        }

        private void txtUDTotExp_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateUpDownTotals();

        }

        private void txtUDHirePerDay_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtUnloadChargeAmt_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateUpDownTotals();
        }

        private void txtServTax_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateUpDownTotals();
        }

        private void txtGrosHire_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtUDPerKM_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtComm_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateUpDownTotals();
        }

        private void txtRepAmt_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateUpDownTotals();
        }

        private void txtNetHire_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtRetUnits_KeyUp(object sender, KeyEventArgs e)
        {
            CalculationForQTY();
            CalculatePerUnitCost();

        }

        private void txtDespatchUnits_KeyUp(object sender, KeyEventArgs e)
        {
            CalculationForQTY();
        }

        #region "EDITING AND DELETING GRIDVIEW DETAILS"

        private void gvDCorDCSTDetl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == gvDCorDCSTDetl.Columns["Del_Dc"].Index)
                {
                    if (Convert.ToBoolean(gvDCorDCSTDetl.Rows[e.RowIndex].Cells["Del_Dc"].Selected) == true)
                    {
                        DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dlgResult == DialogResult.Yes)
                        {
                            DataGridViewRow dgvr = gvDCorDCSTDetl.Rows[e.RowIndex];
                            gvDCorDCSTDetl.Rows.Remove(dgvr);
                            CalculateTotalUnits();
                            CalculatePerUnitCost();
                        }


                        if (gvDCorDCSTDetl.Rows.Count > 0)
                        {
                            for (int i = 0; i < gvDCorDCSTDetl.Rows.Count; i++)
                            {
                                gvDCorDCSTDetl.Rows[i].Cells["SlNo_DC"].Value = (i + 1).ToString();
                            }
                        }
                    }
                }

            }
        }


        private void gvDieselDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string strBillNo, strLtrCost, strTotLtr, strTotAmt;
                strBillNo = strLtrCost = strTotLtr = strTotAmt = "";
                DateTime dtpBillDate;
                DataGridViewRow rowIndex;

                if (e.ColumnIndex == gvDieselDetails.Columns["Edit_Bill"].Index)
                {
                    if (Convert.ToBoolean(gvDieselDetails.Rows[e.RowIndex].Cells["Edit_Bill"].Selected) == true)
                    {
                        rowIndex = gvDieselDetails.Rows[e.RowIndex];

                        strBillNo = gvDieselDetails.Rows[e.RowIndex].Cells["BillNo"].Value.ToString();
                        strLtrCost = gvDieselDetails.Rows[e.RowIndex].Cells["CostPerLtr"].Value.ToString();
                        strTotLtr = gvDieselDetails.Rows[e.RowIndex].Cells["NoOfLtrs"].Value.ToString();
                        strTotAmt = gvDieselDetails.Rows[e.RowIndex].Cells["BillTotAmt"].Value.ToString();
                        dtpBillDate = Convert.ToDateTime(gvDieselDetails.Rows[e.RowIndex].Cells["BillDate"].Value.ToString());

                        frmDieselBillDetails DieselDetl = new frmDieselBillDetails(rowIndex, dtpBillDate, strBillNo, strLtrCost, strTotLtr, strTotAmt);
                        DieselDetl.objTripSheet = this;
                        DieselDetl.ShowDialog();

                        if (gvDieselDetails.Rows.Count > 0)
                        {
                            for (int i = 0; i < gvDieselDetails.Rows.Count; i++)
                            {
                                gvDieselDetails.Rows[i].Cells["SlNo_Diesel"].Value = (i + 1).ToString();
                            }
                        }
                    }
                }
                if (e.ColumnIndex == gvDieselDetails.Columns["Del_Bill"].Index)
                {
                    if (Convert.ToBoolean(gvDieselDetails.Rows[e.RowIndex].Cells["Del_Bill"].Selected) == true)
                    {
                        DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dlgResult == DialogResult.Yes)
                        {
                            DataGridViewRow dgvr = gvDieselDetails.Rows[e.RowIndex];
                            gvDieselDetails.Rows.Remove(dgvr);
                        }


                        if (gvDieselDetails.Rows.Count > 0)
                        {
                            for (int i = 0; i < gvDieselDetails.Rows.Count; i++)
                            {
                                gvDieselDetails.Rows[i].Cells["SlNo_Diesel"].Value = (i + 1).ToString();
                            }
                        }
                    }
                }

            }
        }

        private void gvDeliveryDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == gvDeliveryDetails.Columns["Edit_Delv"].Index)
                {
                    if (Convert.ToBoolean(gvDeliveryDetails.Rows[e.RowIndex].Cells["Edit_Delv"].Selected) == true)
                    {


                        Int32 SlNo = Convert.ToInt32(gvDeliveryDetails.Rows[e.RowIndex].Cells["SlNo_Delv"].Value);
                        DataRow[] dr = dtDeliveryDetl.Select("SlNo_Delv="+ SlNo);
                       
                        frmTripDeliveryDetails TripDetl = new frmTripDeliveryDetails(dtProductDetl,dr);
                        TripDetl.objTripSheet = this;
                        TripDetl.ShowDialog();                       
                        
                    }
                }
                if (e.ColumnIndex == gvDeliveryDetails.Columns["Del_Delv"].Index)
                {
                    if (Convert.ToBoolean(gvDeliveryDetails.Rows[e.RowIndex].Cells["Del_Delv"].Selected) == true)
                    {
                        DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dlgResult == DialogResult.Yes)
                        {

                            Int32 SlNo = Convert.ToInt32(gvDeliveryDetails.Rows[e.RowIndex].Cells["SlNo_Delv"].Value);
                            DataRow[] dr = dtDeliveryDetl.Select("SlNo_Delv=" + SlNo);

                            string MetreReading = gvDeliveryDetails.Rows[e.RowIndex].Cells["StartReading"].Value.ToString();


                            for (int i = dtProductDetl.Rows.Count - 1; i >= 0; i--)
                            {
                                string stMetreRead = "";
                                stMetreRead = dtProductDetl.Rows[i][2].ToString();
                                if (stMetreRead.Equals(MetreReading))
                                {
                                    dtProductDetl.Rows[i].Delete();
                                }

                            }
                           
                            dtDeliveryDetl.Rows.Remove(dr[0]);
                           
                            GetDeliverProductDetails();
                            GetTripDeliveryDetails();
                            
                        }


                    }
                }

            }
        }

        #endregion

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                FillBranchData();
            }
            else
            {
                cbBranches.DataSource = null;
            }
        }

        private void txtEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            objSQLdb = new SQLDB();
            string strCmd = "";
            DataTable dt = new DataTable();
            if (txtEcodeSearch.Text != "")
            {
                try
                {
                    strCmd = "SELECT MEMBER_NAME EName,DESIG EmpDesig  FROM EORA_MASTER " +
                                       " WHERE ECODE= " + Convert.ToInt32(txtEcodeSearch.Text) + "";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtName.Text = dt.Rows[0]["EName"].ToString();                        
                    }
                    else
                    {
                        txtName.Text = "";                       
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSQLdb = null;
                    dt = null;
                }
            }
        }

        private void txtVehNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtVehModel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtTripLRNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtTransporterName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);

            if (e.KeyChar != '\b')
            {
                if (!char.IsLetter(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            
        }

        private void txtDriverName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);

            if (e.KeyChar != '\b')
            {
                if (!char.IsLetter(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtCleanerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);

            if (e.KeyChar != '\b')
            {
                if (!char.IsLetter(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtTotWorkDays_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalKMs();
        }

        private void txtTotKM_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalKMs();
            CalculatePerKMCost();
        }

        private void txtTotExp_TextChanged(object sender, EventArgs e)
        {
            CalculatePerKMCost();
            CalculatePerUnitCost();
        }

        private void GetTripSheetDetails(string strTrnNo,string strRefNo)
        {
            objStockDB = new StockPointDB();
            DataTable dtTSHead = new DataTable();

            if (strTrnNo!="" || strRefNo!= "")
            {
                try
                {
                    dtTSHead = objStockDB.Get_TripSheetHeadDetails(strTrnNo, strRefNo).Tables[0];


                    if (dtTSHead.Rows.Count > 0)
                    {
                        flagUpdate = true;
                        FillTripSheetHeadDetails(dtTSHead);
                    }
                    else
                    {
                        GenerateTrnNo();

                        txtDocMonth.Text = CommonData.DocMonth;
                        cbVehType.SelectedIndex = 0;
                        cbVehPurpose.SelectedIndex = 0;
                        cbTripCode.SelectedIndex = 1;
                        //txtTripLRNo.Text = "";
                        txtTransporterName.Text = "";
                        txtVehNo.Text = "";
                        txtVehModel.Text = "";
                        txtDriverName.Text = "";
                        txtCleanerName.Text = "";
                        gvDieselDetails.Rows.Clear();
                        sCompCode = CommonData.CompanyCode;
                        sBranchCode = CommonData.BranchCode;
                        sFinYear = CommonData.FinancialYear;

                        dtDeliveryDetl.Rows.Clear();
                        gvDeliveryDetails.Rows.Clear();
                        gvDCorDCSTDetl.Rows.Clear();
                        dtProductDetl.Rows.Clear();
                        gvDeliverProducts.Rows.Clear();
                        TabExpDetl.SelectedIndex = 0;
                        txtHirePerKM.Text = "";
                        txtKmPerDiesel.Text = "";
                        txtMileage.Text = "";
                        txtHirePerDay.Text = "";
                        chkAllColumns.Checked = false;
                        chkAnyAlteration.Checked = false;
                        chkAnyReadingVar.Checked = false;
                        chkDiffInKms.Checked = false;
                        chkMileageAchieved.Checked = false;
                        rdbSTNo.Checked = true;
                        rdbSTYes.Checked = false;
                        txtRemarks.Text = "";

                        txtEcodeSearch.Text = "";
                        txtName.Text = "";
                        cbCompany.SelectedIndex = 0;
                        cbBranches.SelectedIndex = -1;

                        txtTotWorkDays.Text = "0";
                        txtAvgKms.Text = "";
                        txtPerKMCost.Text = "";
                        txtPerUnitCost.Text = "";
                        txtHireCharges.Text = "";
                        txtDieselCharges.Text = "";
                        txtPhoneCharges.Text = "";
                        txtOtherExp.Text = "";
                        txtPCTCExp.Text = "";
                        txtTollGates.Text = "";
                        txtRepandMain.Text = "";
                        txtTotExp.Text = "";
                        txtRTOBills.Text = "";
                        txtRTOWithout.Text = "";
                        txtUnitAdv.Text = "";
                        txtCampAdv.Text = "";
                        txtCleanerDa.Text = "";
                        txtDriverDA.Text = "";
                        txtDespatchUnits.Text = "";
                        txtRetUnits.Text = "";
                        txtRetStockPers.Text = "";
                        txtNetRecUnit.Text = "";
                        txtBalAmt.Text = "";

                        txtLoadingCharAmt.Text = "";
                        txtUnloadChargeAmt.Text = "";
                        txtComm.Text = "";
                        txtRepAmt.Text = "";
                        txtRta.Text = "";
                        txtPcTcAmt.Text = "";
                        txtServTax.Text = "";
                        txtOthers.Text = "";
                        txtUDTotExp.Text = "";
                        txtNetHire.Text = "";
                        txtOslrAmt.Text = "";
                        txtPersOfOslr.Text = "";
                        txtUDHirePerDay.Text = "";
                        txtUDPerKM.Text = "";
                        txtGrosHire.Text = "";
                        cbVehType.Enabled = true;
                        cbTripCode.Enabled = true;

                        flagUpdate = false;

                        grouper1.Visible = true;
                        grouper2.Visible = false;


                    }
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objStockDB = null;
                    dtTSHead = null;
                }
            }
        }

        #region "GET DATA FOR UPDATE"

        private void FillTripSheetHeadDetails(DataTable dtHead)
        {
            try
            {
                if (dtHead.Rows.Count > 0)
                {

                    flagUpdate = true;

                    cbVehType.Enabled = false;
                    cbTripCode.Enabled = false;

                    sCompCode = dtHead.Rows[0]["CompCode"].ToString();
                    sBranchCode = dtHead.Rows[0]["BranCode"].ToString();
                    sFinYear = dtHead.Rows[0]["FinYear"].ToString();
                    txtDocMonth.Text = dtHead.Rows[0]["DocMonth"].ToString();
                    cbVehPurpose.Text = dtHead.Rows[0]["VehPurpose"].ToString();
                    cbVehType.Text = dtHead.Rows[0]["TripType"].ToString();
                    txtTripLRNo.Text = dtHead.Rows[0]["TripLRNo"].ToString();
                    txtTrnNo.Text = dtHead.Rows[0]["TrnNo"].ToString();
                    txtVehNo.Text = dtHead.Rows[0]["VehNo"].ToString();
                    txtVehModel.Text = dtHead.Rows[0]["VehModel"].ToString();
                    cbTripCode.Text = dtHead.Rows[0]["TripCode"].ToString();
                    txtTransporterName.Text = dtHead.Rows[0]["TransName"].ToString();
                    txtDriverName.Text = dtHead.Rows[0]["DriverName"].ToString();
                    txtCleanerName.Text = dtHead.Rows[0]["CleanerName"].ToString();
                    cbCompany.SelectedValue = dtHead.Rows[0]["DeliveryToCompany"].ToString();
                    cbBranches.SelectedValue = dtHead.Rows[0]["DeliverToBranch"].ToString();
                    txtEcodeSearch.Text = dtHead.Rows[0]["ToEcode"].ToString();
                    txtName.Text = dtHead.Rows[0]["GLName"].ToString();

                    txtHirePerDay.Text = dtHead.Rows[0]["AgrPerDay"].ToString();
                    txtHirePerKM.Text = dtHead.Rows[0]["AgrHireKM"].ToString();
                    txtKmPerDiesel.Text = dtHead.Rows[0]["AgrHirePerKmDiesel"].ToString();
                    txtMileage.Text = dtHead.Rows[0]["AgrMileage"].ToString();

                    strAltration = dtHead.Rows[0]["Altration"].ToString();
                    strFillUp = dtHead.Rows[0]["AllFillUp"].ToString();
                    strUpDownDiff = dtHead.Rows[0]["UpDownDiff"].ToString();
                    strReadingDiff = dtHead.Rows[0]["ChkReading"].ToString();
                    strMileage = dtHead.Rows[0]["chkMileage"].ToString();

                    if (strAltration.Equals("YES"))
                        chkAnyAlteration.Checked = true;
                    else
                        chkAnyAlteration.Checked = false;
                    if (strFillUp.Equals("YES"))
                        chkAllColumns.Checked = true;
                    else
                        chkAllColumns.Checked = false;
                    if (strUpDownDiff.Equals("YES"))
                        chkDiffInKms.Checked = true;
                    else
                        chkDiffInKms.Checked = false;
                    if (strReadingDiff.Equals("YES"))
                        chkAnyReadingVar.Checked = true;
                    else
                        chkAnyReadingVar.Checked = false;
                    if (strMileage.Equals("YES"))
                        chkMileageAchieved.Checked = true;
                    else
                        chkMileageAchieved.Checked = false;


                    txtHireCharges.Text = dtHead.Rows[0]["TotHireAmt"].ToString();
                    txtDriverDA.Text = dtHead.Rows[0]["DriverDA"].ToString();
                    txtCleanerDa.Text = dtHead.Rows[0]["CleanerDA"].ToString();
                    txtDieselCharges.Text = dtHead.Rows[0]["DieselAmt"].ToString();
                    txtPhoneCharges.Text = dtHead.Rows[0]["PhoneBills"].ToString();
                    txtTollGates.Text = dtHead.Rows[0]["TollGateAmt"].ToString();
                    txtOtherExp.Text = dtHead.Rows[0]["OtherAmt"].ToString();
                    txtTotExp.Text = dtHead.Rows[0]["TotAmount"].ToString();
                    txtUnitAdv.Text = dtHead.Rows[0]["UnitAdv"].ToString();
                    txtCampAdv.Text = dtHead.Rows[0]["CampAdv"].ToString();
                    txtRepandMain.Text = dtHead.Rows[0]["RepandMainAmt"].ToString();
                    txtBalAmt.Text = dtHead.Rows[0]["BalanceAmt"].ToString();

                    txtDespatchUnits.Text = dtHead.Rows[0]["TotUnits"].ToString();
                    txtRetUnits.Text = dtHead.Rows[0]["ReturnUnits"].ToString();
                    txtNetRecUnit.Text = dtHead.Rows[0]["ReceivedUnits"].ToString();
                    txtRetStockPers.Text = dtHead.Rows[0]["StockReturnPers"].ToString();
                    txtTotWorkDays.Text = dtHead.Rows[0]["TripDays"].ToString();
                    txtPerKMCost.Text = dtHead.Rows[0]["PerKmCost"].ToString();


                    txtAvgKms.Text = dtHead.Rows[0]["AvgKM"].ToString();
                    txtPerUnitCost.Text = dtHead.Rows[0]["PerUnitCost"].ToString();
                    if (dtHead.Rows[0]["UDTotAmt"].ToString()!="")
                    {
                        TabExpDetl.SelectTab(UpDownExp);
                        cbUpDownType.Text = dtHead.Rows[0]["UpDownType"].ToString();
                        txtLoadingCharAmt.Text = dtHead.Rows[0]["UDLoadingAmt"].ToString();
                        txtUnloadChargeAmt.Text = dtHead.Rows[0]["UNloadChargeAmt"].ToString();
                        txtComm.Text = dtHead.Rows[0]["CommAmt"].ToString();
                        txtRepAmt.Text = dtHead.Rows[0]["UDRepAmt"].ToString();
                        txtRta.Text = dtHead.Rows[0]["UDRtaAmt"].ToString();
                        txtPcTcAmt.Text = dtHead.Rows[0]["UDPcTcAmt"].ToString();
                        txtServTax.Text = dtHead.Rows[0]["UDServTaxAmt"].ToString();
                        txtOthers.Text = dtHead.Rows[0]["UDOtherAmt"].ToString();
                        txtUDTotExp.Text = dtHead.Rows[0]["UDTotAmt"].ToString();
                        txtGrosHire.Text = dtHead.Rows[0]["UDGrossHireAmt"].ToString();
                        txtNetHire.Text = dtHead.Rows[0]["UDNetHireAmt"].ToString();
                        txtOslrAmt.Text = dtHead.Rows[0]["UDOslrAmt"].ToString();
                    }
                    else
                    {
                        TabExpDetl.SelectTab(tabExpenses);
                    }
                    if (dtHead.Rows[0]["StockFlag"].ToString().Equals("YES"))
                        rdbSTYes.Checked = true;
                    else
                        rdbSTNo.Checked = true;

                    txtPCTCExp.Text = dtHead.Rows[0]["PCTCAmt"].ToString();
                    txtRTOBills.Text = dtHead.Rows[0]["RtoBillsAmt"].ToString();
                    txtRTOWithout.Text = dtHead.Rows[0]["RtoNoBillsAmt"].ToString();


                    txtRemarks.Text = dtHead.Rows[0]["Remarks"].ToString();



                    if (dtHead.Rows[0]["TrnNo"].ToString().Length > 4)
                    {
                        FillTripDeliveryDetails(txtTrnNo.Text.ToString());
                    }

                }
                else
                {

                    GenerateTrnNo();
                    cbVehType.Enabled = true;
                    cbTripCode.Enabled = true;
                    txtDocMonth.Text = CommonData.DocMonth;
                    cbVehType.SelectedIndex = 0;
                    cbVehPurpose.SelectedIndex = 0;
                    cbTripCode.SelectedIndex = 1;
                    //txtTripLRNo.Text = "";
                    txtTransporterName.Text = "";
                    txtVehNo.Text = "";
                    txtVehModel.Text = "";
                    txtDriverName.Text = "";
                    txtCleanerName.Text = "";
                    gvDieselDetails.Rows.Clear();
                    sCompCode = CommonData.CompanyCode;
                    sBranchCode = CommonData.BranchCode;
                    sFinYear = CommonData.FinancialYear;

                    dtDeliveryDetl.Rows.Clear();
                    gvDeliveryDetails.Rows.Clear();
                    gvDCorDCSTDetl.Rows.Clear();
                    dtProductDetl.Rows.Clear();
                    gvDeliverProducts.Rows.Clear();
                    TabExpDetl.SelectedIndex = 0;
                    txtHirePerKM.Text = "";
                    txtKmPerDiesel.Text = "";
                    txtMileage.Text = "";
                    txtHirePerDay.Text = "";
                    chkAllColumns.Checked = false;
                    chkAnyAlteration.Checked = false;
                    chkAnyReadingVar.Checked = false;
                    chkDiffInKms.Checked = false;
                    chkMileageAchieved.Checked = false;
                    rdbSTNo.Checked = true;
                    rdbSTYes.Checked = false;
                    txtRemarks.Text = "";

                    txtEcodeSearch.Text = "";
                    txtName.Text = "";
                    cbCompany.SelectedIndex = 0;
                    cbBranches.SelectedIndex = -1;

                    txtTotWorkDays.Text = "0";
                    txtAvgKms.Text = "";
                    txtPerKMCost.Text = "";
                    txtPerUnitCost.Text = "";
                    txtHireCharges.Text = "";
                    txtDieselCharges.Text = "";
                    txtPhoneCharges.Text = "";
                    txtOtherExp.Text = "";
                    txtPCTCExp.Text = "";
                    txtTollGates.Text = "";
                    txtRepandMain.Text = "";
                    txtTotExp.Text = "";
                    txtRTOBills.Text = "";
                    txtRTOWithout.Text = "";
                    txtUnitAdv.Text = "";
                    txtCampAdv.Text = "";
                    txtCleanerDa.Text = "";
                    txtDriverDA.Text = "";
                    txtDespatchUnits.Text = "";
                    txtRetUnits.Text = "";
                    txtRetStockPers.Text = "";
                    txtNetRecUnit.Text = "";
                    txtBalAmt.Text = "";

                    txtLoadingCharAmt.Text = "";
                    txtUnloadChargeAmt.Text = "";
                    txtComm.Text = "";
                    txtRepAmt.Text = "";
                    txtRta.Text = "";
                    txtPcTcAmt.Text = "";
                    txtServTax.Text = "";
                    txtOthers.Text = "";
                    txtUDTotExp.Text = "";
                    txtNetHire.Text = "";
                    txtOslrAmt.Text = "";
                    txtPersOfOslr.Text = "";
                    txtUDHirePerDay.Text = "";
                    txtUDPerKM.Text = "";
                    txtGrosHire.Text = "";

                    flagUpdate = false;

                    grouper1.Visible = true;
                    grouper2.Visible = false;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FillTripDeliveryDetails(string strTrnNo)
        {
            objStockDB = new StockPointDB();
            Hashtable ht = new Hashtable();
            gvDeliveryDetails.Rows.Clear();
            dtDeliveryDetl.Rows.Clear();
            DataTable dtTripDeliveryDetl = new DataTable();
            DataTable dtProductDetl = new DataTable();
            DataTable dtDCDetl = new DataTable();
            DataTable dtDieselDetl = new DataTable();           
            try
            {
                ht = objStockDB.Get_TripSheetDetails(strTrnNo);
                dtTripDeliveryDetl = (DataTable)ht["TripDeliveryDetails"];
                dtProductDetl = (DataTable)ht["TripProductDetails"];
                dtDCDetl = (DataTable)ht["TripDCorDCSTDetl"];
                dtDieselDetl = (DataTable)ht["TripDieselDetl"];

                if (dtTripDeliveryDetl.Rows.Count > 0)
                {

                    for (int i = 0; i < dtTripDeliveryDetl.Rows.Count; i++)
                    {
                        //gvDeliveryDetails.Rows.Add();

                        //gvDeliveryDetails.Rows[i].Cells["SlNo_Delv"].Value = (i + 1).ToString();
                        //gvDeliveryDetails.Rows[i].Cells["GLEcode"].Value = dtTripDeliveryDetl.Rows[i]["GLEcode"].ToString();
                        //gvDeliveryDetails.Rows[i].Cells["StartDate"].Value = Convert.ToDateTime(dtTripDeliveryDetl.Rows[i]["StartDate"]).ToString("dd/MMM/yyyy");
                        //gvDeliveryDetails.Rows[i].Cells["StartTime"].Value = dtTripDeliveryDetl.Rows[i]["StartTime"].ToString();
                        //gvDeliveryDetails.Rows[i].Cells["StartPlace"].Value = dtTripDeliveryDetl.Rows[i]["StartPlace"].ToString();
                        //gvDeliveryDetails.Rows[i].Cells["StartReading"].Value = dtTripDeliveryDetl.Rows[i]["StartReading"].ToString();
                        //gvDeliveryDetails.Rows[i].Cells["EndDate"].Value = Convert.ToDateTime(dtTripDeliveryDetl.Rows[i]["EndDate"]).ToString("dd/MMM/yyyy");
                        //gvDeliveryDetails.Rows[i].Cells["EndTime"].Value = dtTripDeliveryDetl.Rows[i]["EndTime"].ToString();
                        //gvDeliveryDetails.Rows[i].Cells["EndPlace"].Value = dtTripDeliveryDetl.Rows[i]["EndPlace"].ToString();
                        //gvDeliveryDetails.Rows[i].Cells["EndReading"].Value = dtTripDeliveryDetl.Rows[i]["EndReading"].ToString();
                        //gvDeliveryDetails.Rows[i].Cells["NoOfKM"].Value = dtTripDeliveryDetl.Rows[i]["NoOfKms"].ToString();
                        //gvDeliveryDetails.Rows[i].Cells["NoOfUnitsDel"].Value = dtTripDeliveryDetl.Rows[i]["TotDelvUnits"].ToString();
                        //gvDeliveryDetails.Rows[i].Cells["NoOfCustCov"].Value = dtTripDeliveryDetl.Rows[i]["TotCusts"].ToString();
                        //gvDeliveryDetails.Rows[i].Cells["TotDays"].Value = dtTripDeliveryDetl.Rows[i]["TotDays"].ToString();
                        //gvDeliveryDetails.Rows[i].Cells["DriverDA"].Value = dtTripDeliveryDetl.Rows[i]["DriverDAAmt"].ToString();
                        //gvDeliveryDetails.Rows[i].Cells["CleanerDA"].Value = dtTripDeliveryDetl.Rows[i]["CleanerDAAmt"].ToString();
                        //gvDeliveryDetails.Rows[i].Cells["GLName"].Value = dtTripDeliveryDetl.Rows[i]["EmpName"].ToString();

                        dtDeliveryDetl.Rows.Add(new Object[] {"-1", dtTripDeliveryDetl.Rows[i]["GLEcode"].ToString(),
                                                                    Convert.ToDateTime(dtTripDeliveryDetl.Rows[i]["StartDateTime"]).ToString("dd/MMM/yyyy hh:mm:ss"),
                                                                    Convert.ToDateTime(dtTripDeliveryDetl.Rows[i]["EndDateTime"]).ToString("dd/MMM/yyyy hh:mm:ss"),
                                                                   Convert.ToDateTime(dtTripDeliveryDetl.Rows[i]["StartDate"]).ToString("dd/MMM/yyyy"),
                                                                   dtTripDeliveryDetl.Rows[i]["StartTime"].ToString(),
                                                                   dtTripDeliveryDetl.Rows[i]["StartPlace"].ToString(),
                                                                   dtTripDeliveryDetl.Rows[i]["StartReading"].ToString(),
                                                                   Convert.ToDateTime(dtTripDeliveryDetl.Rows[i]["EndDate"]).ToString("dd/MMM/yyyy"),
                                                                   dtTripDeliveryDetl.Rows[i]["EndTime"].ToString(),
                                                                   dtTripDeliveryDetl.Rows[i]["EndPlace"].ToString(),
                                                                   dtTripDeliveryDetl.Rows[i]["EndReading"].ToString(),
                                                                   dtTripDeliveryDetl.Rows[i]["NoOfKms"].ToString(),
                                                                   dtTripDeliveryDetl.Rows[i]["TotDelvUnits"].ToString(),
                                                                   dtTripDeliveryDetl.Rows[i]["TotCusts"].ToString(),
                                                                   dtTripDeliveryDetl.Rows[i]["TotDays"].ToString(),
                                                                   dtTripDeliveryDetl.Rows[i]["DriverDAAmt"].ToString(),
                                                                   dtTripDeliveryDetl.Rows[i]["CleanerDAAmt"].ToString(),
                                                                   dtTripDeliveryDetl.Rows[i]["EmpName"].ToString()});

                        GetTripDeliveryDetails();

                       
                       
                    }

                    CalculateTotalKMs();
                    CalulateTotalTripDays();
                    CalculatePerKMCost();
                }

                FillTripDieselDetails(dtDieselDetl);
                FillDCorDCSTDetails(dtDCDetl);
                FillTripDeliverProductDetl(dtProductDetl);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objStockDB = null;
                ht = null;
            }

        }

        private void FillTripDieselDetails(DataTable dtDieselDetl)
        {
            gvDieselDetails.Rows.Clear();
            try
            {
                if (dtDieselDetl.Rows.Count > 0)
                {
                    for (int i = 0; i < dtDieselDetl.Rows.Count; i++)
                    {
                        gvDieselDetails.Rows.Add();

                        gvDieselDetails.Rows[i].Cells["SlNo_Diesel"].Value = (i + 1).ToString();
                        gvDieselDetails.Rows[i].Cells["BillDate"].Value = Convert.ToDateTime(dtDieselDetl.Rows[i]["BillDate"]).ToString("dd/MMM/yyyy");
                        gvDieselDetails.Rows[i].Cells["BillNo"].Value = dtDieselDetl.Rows[i]["BilNo"].ToString();
                        gvDieselDetails.Rows[i].Cells["CostPerLtr"].Value = dtDieselDetl.Rows[i]["PerLtrCost"].ToString();
                        gvDieselDetails.Rows[i].Cells["NoOfLtrs"].Value = dtDieselDetl.Rows[i]["TotLtrs"].ToString();
                        gvDieselDetails.Rows[i].Cells["BillTotAmt"].Value = dtDieselDetl.Rows[i]["TotalAmount"].ToString();
                        
                    }
                    CalculateDieselTotalCost();
                }

                dtDieselDetl = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void FillDCorDCSTDetails(DataTable dtDCSTDetl)
        {
            gvDCorDCSTDetl.Rows.Clear();
            try
            {
                if (dtDCSTDetl.Rows.Count > 0)
                {
                    for (int i = 0; i < dtDCSTDetl.Rows.Count; i++)
                    {
                        gvDCorDCSTDetl.Rows.Add();

                        gvDCorDCSTDetl.Rows[i].Cells["SlNo_DC"].Value = (i + 1).ToString();
                        gvDCorDCSTDetl.Rows[i].Cells["DCorDCSTNo"].Value = dtDCSTDetl.Rows[i]["DCorDCSTNo"].ToString();
                        gvDCorDCSTDetl.Rows[i].Cells["DCRefNo"].Value = dtDCSTDetl.Rows[i]["RefNo"].ToString();
                        gvDCorDCSTDetl.Rows[i].Cells["DCQty"].Value = dtDCSTDetl.Rows[i]["TotQty"].ToString();
                        
                    }
                }
                CalculateTotalUnits();
                CalculatePerUnitCost();

                dtDCSTDetl = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private void FillTripDeliverProductDetl(DataTable dtProducts)
        {
            dtProductDetl.Rows.Clear();
            gvDeliverProducts.Rows.Clear();
            try
            {
                if (dtProducts.Rows.Count > 0)
                {
                    for (int i = 0; i < dtProducts.Rows.Count; i++)
                    {

                        dtProductDetl.Rows.Add(new Object[] {"-1", dtProducts.Rows[i]["ProductId"].ToString(),
                                                                       dtProducts.Rows[i]["MetreReading"].ToString(),
                                                                       dtProducts.Rows[i]["ProductName"].ToString(),
                                                                       dtProducts.Rows[i]["CategoryName"].ToString(),
                                                                       dtProducts.Rows[i]["DespatchQty"].ToString()});

                        GetDeliverProductDetails();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        #endregion

        

        private void TabExpDetl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (cbVehType.Text.Equals("HIRE"))
            {
                if (e.TabPage == TabExpDetl.TabPages[1])
                {
                    e.Cancel = true;
                    UpDownExp.Hide();
                }
            }
            else
            {
                if (e.TabPage == TabExpDetl.TabPages[1])
                {
                    e.Cancel = false;
                    UpDownExp.Show();
                }

            }
        }

            

        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            int iRes = 0;
            string strCommand = "";

            if (txtTrnNo.Text != "" && flagUpdate == true)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    try
                    {
                       
                        strCommand += "DELETE FROM TRIP_SHEET_DIESEL_EXP WHERE TSDD_COMP_CODE='" + sCompCode +
                                       "' AND TSDD_BRANCH_CODE='" + sBranchCode + "' AND TSDD_FIN_YEAR='" + sFinYear +
                                       "' AND TSDD_TRN_NO='" + txtTrnNo.Text.ToString() + "'";
                        strCommand += "DELETE FROM TRIP_SHEET_DC_DETL WHERE TSDCD_COMP_CODE='" + sCompCode +
                                        "' AND TSDCD_BRANCH_CODE='" + sBranchCode + "' AND TSDCD_FIN_YEAR='" + sFinYear +
                                        "' AND TSDCD_TRN_NO='" + txtTrnNo.Text.ToString() + "'";
                        strCommand += "DELETE FROM TRIP_SHEET_PRODUCT_DETL WHERE TSPD_COMP_CODE='" + sCompCode +
                                     "' AND TSPD_BRANCH_CODE='" + sBranchCode +
                                     "' AND TSPD_FIN_YEAR='" + sFinYear +
                                     "' AND TSPD_TRN_NO='" + txtTrnNo.Text.ToString() + "'";
                        strCommand += "DELETE FROM TRIP_SHEET_DELIVERY_DETL WHERE TSD_COMP_CODE='" + sCompCode +
                                      "' AND TSD_BRANCH_CODE='" + sBranchCode + "' AND TSD_FIN_YEAR='" + sFinYear +
                                      "' AND TSD_TRN_NO='" + txtTrnNo.Text.ToString() + "'";
                        strCommand += "DELETE FROM TRIP_SHEET_HEAD WHERE TSH_COMP_CODE='" + sCompCode +
                                      "' AND TSH_BRANCH_CODE='" + sBranchCode + "' AND TSH_FIN_YEAR='" + sFinYear +
                                      "' and TSH_TRN_NO='" + txtTrnNo.Text.ToString() + "'";


                        if (strCommand.Length > 10)
                        {
                            iRes = objSQLdb.ExecuteSaveData(strCommand);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        objSQLdb = null;

                    }
                    if (iRes > 0)
                    {
                        MessageBox.Show("Data Deleted Successfully","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        btnClear_Click(null,null);
                        btnCancel_Click(null, null);
                        flagUpdate = false;
                        grouper1.Visible = true;
                        grouper2.Visible = false;
                        GenerateTrnNo();
                        AutoValidate = AutoValidate.EnableAllowFocusChange;
                        
                    }

                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
           
            gvDCorDCSTDetl.Rows.Clear();
            dtProductDetl.Rows.Clear();
            gvDeliverProducts.Rows.Clear();
            TabExpDetl.SelectedIndex = 0;
            txtHirePerKM.Text = "";
            txtKmPerDiesel.Text = "";
            txtMileage.Text = "";
            txtHirePerDay.Text = "";
            chkAllColumns.Checked = false;
            chkAnyAlteration.Checked = false;
            chkAnyReadingVar.Checked = false;
            chkDiffInKms.Checked = false;
            chkMileageAchieved.Checked = false;
            rdbSTNo.Checked = true;
            rdbSTYes.Checked = false;
            txtRemarks.Text = "";
                     
            txtTotWorkDays.Text = "0";
            txtAvgKms.Text = "";
            txtPerKMCost.Text = "";
            txtPerUnitCost.Text = "";
            txtHireCharges.Text = "";
            txtDieselCharges.Text = "";
            txtPhoneCharges.Text = "";
            txtOtherExp.Text = "";
            txtPCTCExp.Text = "";
            txtTollGates.Text = "";
            txtRepandMain.Text = "";
            txtTotExp.Text = "";
            txtRTOBills.Text = "";
            txtRTOWithout.Text = "";
            txtUnitAdv.Text = "";
            txtCampAdv.Text = "";
            txtCleanerDa.Text = "";
            txtDriverDA.Text = "";
            txtDespatchUnits.Text = "";
            txtRetUnits.Text = "";
            txtRetStockPers.Text = "";
            txtNetRecUnit.Text = "";
            txtBalAmt.Text = "";

            txtLoadingCharAmt.Text = "";
            txtUnloadChargeAmt.Text = "";
            txtComm.Text = "";
            txtRepAmt.Text = "";
            txtRta.Text = "";
            txtPcTcAmt.Text = "";
            txtServTax.Text = "";
            txtOthers.Text = "";
            txtUDTotExp.Text = "";
            txtNetHire.Text = "";
            txtOslrAmt.Text = "";
            txtPersOfOslr.Text = "";
            txtUDHirePerDay.Text = "";
            txtUDPerKM.Text = "";
            txtGrosHire.Text = "";

            ////flagUpdate = false;

            //grouper1.Visible = true;
            //grouper2.Visible = false;

            //GenerateTrnNo();
        }

        private void txtTripLRNo_Validated(object sender, EventArgs e)
        {
            if (txtTripLRNo.Text != "")
            {
                GetTripSheetDetails("", txtTripLRNo.Text.ToString());
            }
            else
            {
                txtDocMonth.Text = CommonData.DocMonth;
                cbVehType.SelectedIndex = 0;
                cbVehPurpose.SelectedIndex = 0;
                cbTripCode.SelectedIndex = 1;
                //txtTripLRNo.Text = "";
                txtTransporterName.Text = "";
                txtVehNo.Text = "";
                txtVehModel.Text = "";
                txtDriverName.Text = "";
                txtCleanerName.Text = "";
                gvDieselDetails.Rows.Clear();
                sCompCode = CommonData.CompanyCode;
                sBranchCode = CommonData.BranchCode;
                sFinYear = CommonData.FinancialYear;

                dtDeliveryDetl.Rows.Clear();
                gvDeliveryDetails.Rows.Clear();
                gvDCorDCSTDetl.Rows.Clear();
                dtProductDetl.Rows.Clear();
                gvDeliverProducts.Rows.Clear();
                TabExpDetl.SelectedIndex = 0;
                txtHirePerKM.Text = "";
                txtKmPerDiesel.Text = "";
                txtMileage.Text = "";
                txtHirePerDay.Text = "";
                chkAllColumns.Checked = false;
                chkAnyAlteration.Checked = false;
                chkAnyReadingVar.Checked = false;
                chkDiffInKms.Checked = false;
                chkMileageAchieved.Checked = false;
                rdbSTNo.Checked = true;
                rdbSTYes.Checked = false;
                txtRemarks.Text = "";

                txtEcodeSearch.Text = "";
                txtName.Text = "";
                cbCompany.SelectedIndex = 0;
                cbBranches.SelectedIndex = -1;

                txtTotWorkDays.Text = "0";
                txtAvgKms.Text = "";
                txtPerKMCost.Text = "";
                txtPerUnitCost.Text = "";
                txtHireCharges.Text = "";
                txtDieselCharges.Text = "";
                txtPhoneCharges.Text = "";
                txtOtherExp.Text = "";
                txtPCTCExp.Text = "";
                txtTollGates.Text = "";
                txtRepandMain.Text = "";
                txtTotExp.Text = "";
                txtRTOBills.Text = "";
                txtRTOWithout.Text = "";
                txtUnitAdv.Text = "";
                txtCampAdv.Text = "";
                txtCleanerDa.Text = "";
                txtDriverDA.Text = "";
                txtDespatchUnits.Text = "";
                txtRetUnits.Text = "";
                txtRetStockPers.Text = "";
                txtNetRecUnit.Text = "";
                txtBalAmt.Text = "";

                txtLoadingCharAmt.Text = "";
                txtUnloadChargeAmt.Text = "";
                txtComm.Text = "";
                txtRepAmt.Text = "";
                txtRta.Text = "";
                txtPcTcAmt.Text = "";
                txtServTax.Text = "";
                txtOthers.Text = "";
                txtUDTotExp.Text = "";
                txtNetHire.Text = "";
                txtOslrAmt.Text = "";
                txtPersOfOslr.Text = "";
                txtUDHirePerDay.Text = "";
                txtUDPerKM.Text = "";
                txtGrosHire.Text = "";

                flagUpdate = false;

                grouper1.Visible = true;
                grouper2.Visible = false;

                GenerateTrnNo();
            }
           
        }

   

        private void txtTrnNo_Validated(object sender, EventArgs e)
        {
            try
            {
                if (txtTrnNo.Text != "")
                {
                    GetTripSheetDetails(txtTrnNo.Text.ToString(), "");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
                
        }


        //private void ClearForm(System.Windows.Forms.Control parent)
        //{
           
        //    foreach (System.Windows.Forms.Control ctrControl in parent.Controls)
        //    {
        //        //Loop through all controls 
        //        if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.TextBox)))
        //        {
        //            //Check to see if it's a textbox 
        //            ((System.Windows.Forms.TextBox)ctrControl).Text = string.Empty;

        //            //If it is then set the text to String.Empty (empty textbox) 
        //        }
        //        else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.MaskedTextBox)))
        //        {

        //            //Check to see if it's a textbox 
        //            ((System.Windows.Forms.MaskedTextBox)ctrControl).Text = string.Empty;
        //            //If it is then set the text to String.Empty (empty textbox) 
        //        }
        //        else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.RichTextBox)))
        //        {
        //            ((System.Windows.Forms.RichTextBox)ctrControl).Text = string.Empty;
        //        }
        //        else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.ComboBox)))
        //        {
        //            //cbVehicleType.SelectedIndex = 0;
        //        }
        //        else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.CheckBox)))
        //        {
        //            ((System.Windows.Forms.CheckBox)ctrControl).Checked = false;
        //        }
        //        else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.RadioButton)))
        //        {
        //            ((System.Windows.Forms.RadioButton)ctrControl).Checked = false;
        //        }
        //        else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.DataGridView)))
        //        {
        //            //((System.Windows.Forms.DataGridView)ctrControl).Rows.Clear();
        //            //FillProductData();

        //        }
        //        else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.DateTimePicker)))
        //        {
        //            ((System.Windows.Forms.DateTimePicker)ctrControl).Text = DateTime.Now.Date.ToString("dd/MM/yy");

        //        }
        //        if (ctrControl.Controls.Count > 0)
        //        {
        //            ClearForm(ctrControl);
        //        }
        //    }
        //}

       
        private void txtTotExp_KeyUp(object sender, KeyEventArgs e)
        {
            CalculatePerUnitCost();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AutoValidate = AutoValidate.EnableAllowFocusChange;
            txtDocMonth.Text = CommonData.DocMonth;
            cbVehType.SelectedIndex = 0;
            cbVehPurpose.SelectedIndex = 0;
            cbTripCode.SelectedIndex = 1;
            txtTripLRNo.Text = "";
            txtTransporterName.Text = "";
            txtVehNo.Text = "";
            txtVehModel.Text = "";
            txtDriverName.Text = "";
            txtCleanerName.Text = "";

            txtEcodeSearch.Text = "";
            txtName.Text = "";
            cbCompany.SelectedIndex = 0;
            cbBranches.SelectedIndex = -1;

            gvDieselDetails.Rows.Clear();
            sCompCode = CommonData.CompanyCode;
            sBranchCode = CommonData.BranchCode;
            sFinYear = CommonData.FinancialYear;

            dtDeliveryDetl.Rows.Clear();
            gvDeliveryDetails.Rows.Clear();
            cbVehType.Enabled = true;
            cbTripCode.Enabled = true;

            flagUpdate = false;
            GenerateTrnNo();
        }

        private void txtNetRecUnit_TextChanged(object sender, EventArgs e)
        {
            CalculatePerUnitCost();
        }

        private void txtDespatchUnits_TextChanged(object sender, EventArgs e)
        {
            CalculatePerUnitCost();
        }

        private void btnBack_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
     
      
    }
      
}
