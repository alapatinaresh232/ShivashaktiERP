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
using System.Data.SqlClient;
using System.Collections;


namespace SSCRM
{
    public partial class StockDumping : Form
    {
        SQLDB objSQLdb = null;
        StockPointDB objSPDB = null;
        private bool flagUpdate = false;
        Int32 intCurrentRow = 0, intCurrentCell = 0;
        bool flagText = true;

        public StockDumping()
        {
            InitializeComponent();
        }
             

        private void StockDumping_Load(object sender, EventArgs e)
        {
            if (CommonData.LogUserId.ToUpper() == "ADMIN" || CommonData.LogUserRole=="MANAGEMENT")
                txtDocMonth.ReadOnly = false;
            else
                txtDocMonth.ReadOnly = true;

            txtDocMonth.Text = CommonData.DocMonth.ToUpper();
            FillCompanyData();
            GenerateTrnNo();
            chkCancel.Checked = false;
        }


        private void GenerateTrnNo()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";

            try
            {
                
                strCmd = " SELECT IsNull(Max(cast(SDH_TRN_NO as numeric)),0)+1 as TrnNo FROM STOCK_DUMP_HEAD "+
                          " WHERE SDH_BRANCH_CODE='"+ CommonData.BranchCode +
                          "' and SDH_FIN_YEAR='"+ CommonData.FinancialYear +"'";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    txtTrnNo.Text = dt.Rows[0]["TrnNo"].ToString();
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
                strCmd = "SELECT CM_COMPANY_CODE,CM_COMPANY_NAME " +
                                " FROM COMPANY_MAS " +
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
                    strCommand = "SELECT BRANCH_NAME BranchName,BRANCH_CODE BranchCode " +
                                 " FROM BRANCH_MAS where COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
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
                    cbBranches.DisplayMember = "BranchName";
                    cbBranches.ValueMember = "BranchCode";

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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

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



        private Int32 SaveStockDumpingDetails()
        {
            objSQLdb = new SQLDB();
            string strCmd = "";
            int iRes = 0;
            string sStatus = "";

            try
            {
                if (chkCancel.Checked == true)
                {
                    sStatus = "CANCELLED";
                }
                else
                {
                    sStatus = "DOCUMENTED";
                }

                if (txtEcodeSearch.Text.Length == 0)
                {
                    txtEcodeSearch.Text = "0";
                }

                if (flagUpdate == true)
                {
                    strCmd = "DELETE FROM STOCK_DUMP_DETL WHERE SDD_COMP_CODE='" + CommonData.CompanyCode +
                                 "' and SDD_BRANCH_CODE='" + CommonData.BranchCode + "' and SDD_FIN_YEAR ='" + CommonData.FinancialYear +
                                 "' and SDD_TRN_NO='" + txtTrnNo.Text + "'";
                    strCmd +=" UPDATE STOCK_DUMP_HEAD SET SDH_DOC_MONTH='"+ txtDocMonth.Text.ToUpper() +
                              "' ,SDH_LR_NO='"+ txtTripLRNo.Text  +"',SDH_VEHICLE_NO='"+ txtVehNo.Text.ToString().Replace("'","") +
                              "',SDH_VEHICLE_TYPE='" + txtVehModel.Text.ToString().Replace("'","")+
                              "',SDH_TRANS_NAME='"+ txtTransporterName.Text.ToString().Replace("'","") +
                              "',SDH_DRIVER_NAME='"+ txtDriverName.Text.ToString().Replace("'","") +
                              "',SDH_CLEANER_NAME='"+ txtCleanerName.Text.ToString().Replace("'","") +
                              "',SDH_TO_BRANCH_CODE='"+ cbBranches.SelectedValue.ToString() +
                              "',SDH_TOT_FREIGHT="+ Convert.ToDouble(txtTotalFreight.Text) +
                              ",SDH_ADV_PAID_AT_UNIT="+ Convert.ToDouble(txtAdvancePaid.Text) +
                              ",SDH_TO_PAY_AT_CAMP="+ Convert.ToDouble(txtToPay.Text) +
                              ",SDH_LOAD_AMT="+ Convert.ToDouble(txtLoadingCharges.Text) +
                              ",SDH_TOTAL_COST="+ Convert.ToDouble(txtTotDcAmt.Text) +
                              ",SDH_COST_PER_TON="+ Convert.ToDouble(txtCostPerTon.Text) +
                              ",SDH_REMARKS='"+ txtRemarks.Text.ToString().Replace("'","") +
                              "',SDH_LAST_MODIFIED_BY='"+ CommonData.LogUserId +
                              "',SDH_LAST_MODIFIED_DATE=getdate() "+
                             ", SDH_STATUS='" + sStatus + "',SDH_COST_PER_KM="+ Convert.ToDouble(txtCostPerKM.Text) +
                             ", SDH_TO_ECODE=" + Convert.ToInt32(txtEcodeSearch.Text) +
                             ", SDH_CONTACT_NO='"+txtDriverContactNo.Text+
                             "' WHERE SDH_COMP_CODE='" + CommonData.CompanyCode +
                             "' and SDH_BRANCH_CODE='"+ CommonData.BranchCode +
                             "' and SDH_FIN_YEAR='"+ CommonData.FinancialYear +
                             "' and SDH_TRN_NO='"+ txtTrnNo.Text +"'";
                }
                else
                {
                    GenerateTrnNo();
                    strCmd = "INSERT INTO STOCK_DUMP_HEAD(SDH_COMP_CODE " +
                                                       ", SDH_BRANCH_CODE " +
                                                       ", SDH_FIN_YEAR " +
                                                       ", SDH_DOC_MONTH " +
                                                       ", SDH_TRN_NO " +
                                                       ", SDH_LR_NO " +
                                                       ", SDH_VEHICLE_NO " +
                                                       ", SDH_VEHICLE_TYPE " +
                                                       ", SDH_TRANS_NAME " +
                                                       ", SDH_DRIVER_NAME " +
                                                       ", SDH_CLEANER_NAME " +
                                                       ", SDH_TO_BRANCH_CODE " +
                                                       ", SDH_TOT_FREIGHT " +
                                                       ", SDH_ADV_PAID_AT_UNIT " +
                                                       ", SDH_TO_PAY_AT_CAMP " +
                                                       ", SDH_LOAD_AMT " +
                                                       ", SDH_TOTAL_COST " +
                                                       ", SDH_COST_PER_TON " +
                                                       ", SDH_REMARKS " +
                                                       ", SDH_STATUS "+
                                                       ", SDH_CREATED_BY " +
                                                       ", SDH_CREATED_DATE " +
                                                       ",SDH_COST_PER_KM "+
                                                       ",SDH_TO_ECODE "+
                                                       ",SDH_CONTACT_NO "+
                                                       ") VALUES('" + CommonData.CompanyCode +
                                                       "','" + CommonData.BranchCode +
                                                       "','" + CommonData.FinancialYear +
                                                       "','" + txtDocMonth.Text +
                                                       "','" + txtTrnNo.Text.ToString() +
                                                       "','" + txtTripLRNo.Text.ToString() +
                                                       "','" + txtVehNo.Text.ToString().Replace("'", " ") +
                                                       "','" + txtVehModel.Text.ToString().Replace("'", " ") +
                                                       "','" + txtTransporterName.Text.ToString().Replace("'", " ") +
                                                       "','" + txtDriverName.Text.ToString().Replace("'", " ") +
                                                       "','" + txtCleanerName.Text.ToString().Replace("'", " ") +
                                                       "','" + cbBranches.SelectedValue.ToString() +
                                                       "'," + Convert.ToDouble(txtTotalFreight.Text) +
                                                       "," + Convert.ToDouble(txtAdvancePaid.Text) +
                                                       "," + Convert.ToDouble(txtToPay.Text) +
                                                       "," + Convert.ToDouble(txtLoadingCharges.Text) +
                                                       "," + Convert.ToDouble(txtTotQty.Text) +
                                                       "," + Convert.ToDouble(txtCostPerTon.Text) +
                                                       ",'" + txtRemarks.Text.ToString().Replace("'", " ") +
                                                       "','"+ sStatus +
                                                       "','" + CommonData.LogUserId +
                                                       "',getdate(),"+ Convert.ToDouble(txtCostPerKM.Text) +
                                                       "," + Convert.ToInt32(txtEcodeSearch.Text) + ",'" + txtDriverContactNo.Text + "')";
                }

                if (gvDeliveryDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < gvDeliveryDetails.Rows.Count; i++)
                    {
                        strCmd += "INSERT INTO STOCK_DUMP_DETL(SDD_COMP_CODE " +
                                                           ", SDD_BRANCH_CODE " +
                                                           ", SDD_FIN_YEAR " +
                                                           ", SDD_TRN_NO " +
                                                           ", SDD_SL_NO " +
                                                           ", SDD_DC_DATE " +
                                                           ", SDD_DCDCST_NO " +
                                                           ", SDD_TO_BRANCH_CODE " +
                                                           ", SDD_TO_ECODE " +
                                                           ", SDD_FROM_LOC " +
                                                           ", SDD_TO_LOC " +
                                                           ", SDD_NO_OF_KM " +
                                                           ", SDD_TOT_TONNES " +
                                                           ", SDD_TOT_GRMN_QTY " +
                                                           ", SDD_TOT_GRANUALS_QTY " +
                                                           ", SDD_TOT_LIQUIDS_QTY " +
                                                           ", SDD_TOTAL_QTY " +
                                                           ", SDD_TOT_DC_EXP " +
                                                           ", SDD_CREATED_BY " +
                                                           ", SDD_CREATED_DATE " +
                                                           ")VALUES('" + CommonData.CompanyCode +
                                                           "','" + CommonData.BranchCode +
                                                           "','" + CommonData.FinancialYear +
                                                           "','" + txtTrnNo.Text +
                                                           "'," + Convert.ToInt32(gvDeliveryDetails.Rows[i].Cells["SlNo"].Value) +
                                                           ",'" + Convert.ToDateTime(gvDeliveryDetails.Rows[i].Cells["DCDate"].Value).ToString("dd/MMM/yyyy") +
                                                           "','" + gvDeliveryDetails.Rows[i].Cells["DCDCSTNo"].Value.ToString() +
                                                           "','" + gvDeliveryDetails.Rows[i].Cells["ToBranCode"].Value.ToString() +
                                                           "'," + Convert.ToInt32(gvDeliveryDetails.Rows[i].Cells["GCEcode"].Value) +
                                                           ",'" + gvDeliveryDetails.Rows[i].Cells["FromLoc"].Value.ToString().Replace("'", " ") +
                                                           "','" + gvDeliveryDetails.Rows[i].Cells["ToLoc"].Value.ToString().Replace("'", " ") +
                                                           "'," + Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["NoOfKM"].Value) +
                                                           "," + Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["TotalTonnes"].Value) +
                                                           "," + Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["GrominBags"].Value) +
                                                           "," + Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["Granuals"].Value) +
                                                           "," + Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["Liquids"].Value) +
                                                           "," + Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["TotUnits"].Value) +
                                                           "," + Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["DCExp"].Value) +
                                                           ",'" + CommonData.LogUserId +
                                                           "',getdate())";
                    }
                }

                if (strCmd.Length > 10)
                {
                    objSQLdb = new SQLDB();
                    iRes = objSQLdb.ExecuteSaveData(strCmd);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return iRes;
        }      

        //private Int32 SaveStockDumpingDetl()
        //{
        //    objSQLdb = new SQLDB();
        //    int iRes = 0;
        //    string strCmd = "";

        //    try
        //    {
        //        if (gvDeliveryDetails.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < gvDeliveryDetails.Rows.Count; i++)
        //            {
        //                strCmd += "INSERT INTO STOCK_DUMP_DETL(SDD_COMP_CODE " +
        //                                                   ", SDD_BRANCH_CODE " +
        //                                                   ", SDD_FIN_YEAR " +
        //                                                   ", SDD_TRN_NO " +
        //                                                   ", SDD_SL_NO " +
        //                                                   ", SDD_DC_DATE " +
        //                                                   ", SDD_DCDCST_NO " +
        //                                                   ", SDD_TO_BRANCH_CODE " +
        //                                                   ", SDD_TO_ECODE " +
        //                                                   ", SDD_FROM_LOC " +
        //                                                   ", SDD_TO_LOC " +
        //                                                   ", SDD_NO_OF_KM " +
        //                                                   ", SDD_TOT_TONNES " +
        //                                                   ", SDD_TOT_GRMN_QTY " +
        //                                                   ", SDD_TOT_GRANUALS_QTY " +
        //                                                   ", SDD_TOT_LIQUIDS_QTY " +
        //                                                   ", SDD_TOTAL_QTY " +
        //                                                   ", SDD_TOT_DC_EXP " +
        //                                                   ", SDD_CREATED_BY " +
        //                                                   ", SDD_CREATED_DATE " +
        //                                                   ")VALUES('" + CommonData.CompanyCode +
        //                                                   "','" + CommonData.BranchCode +
        //                                                   "','" + CommonData.FinancialYear +
        //                                                   "','" + txtTrnNo.Text +
        //                                                   "'," + Convert.ToInt32s(gvDeliveryDetails.Rows[0].Cells["SlNo"].Value) +
        //                                                   ",'"+ Convert.ToDateTime(gvDeliveryDetails.Rows[i].Cells["DCDate"].Value).ToString("dd/MMM/yyyy") +
        //                                                   "','"+ gvDeliveryDetails.Rows[i].Cells["DCDCSTNo"].Value.ToString() +
        //                                                   "','"+ gvDeliveryDetails.Rows[i].Cells["ToBranCode"].Value.ToString() +
        //                                                   "',"+ Convert.ToInt32(gvDeliveryDetails.Rows[i].Cells["GCEcode"].Value) +
        //                                                   ",'"+ gvDeliveryDetails.Rows[i].Cells["FromLoc"].Value.ToString().Replace("'"," ") +
        //                                                   "','"+ gvDeliveryDetails.Rows[i].Cells["ToLoc"].Value.ToString().Replace("'"," ") +
        //                                                   "',"+ Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["NoOfKM"].Value) +
        //                                                   ","+ Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["TotalTonnes"].Value) +
        //                                                   ","+ Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["GrominBags"].Value) +
        //                                                   ","+ Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["Granuals"].Value) +
        //                                                   ","+ Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["Liquids"].Value) +
        //                                                   ","+ Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["TotUnits"].Value) +
        //                                                   ","+ Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["DCExp"].Value) +
        //                                                   ",'"+ CommonData.LogUserId +
        //                                                   "',getdate())";
        //            }
        //        }

        //        if (strCmd.Length > 5)
        //        {
        //            iRes = objSQLdb.ExecuteSaveData(strCmd);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }

        //    return iRes;
        //}

        private bool CheckData()
        {
            bool flag = true;
            if (cbCompany.SelectedIndex == 0 || cbCompany.SelectedIndex == -1)
            {
                flag = false;
                MessageBox.Show("Please Select Company","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return flag;
            }
            if (cbBranches.SelectedIndex == 0 || cbBranches.SelectedIndex == -1)
            {
                flag = false;
                MessageBox.Show("Please Select Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;
            }
            if (cbBranches.SelectedIndex == 0 || cbBranches.SelectedIndex == -1)
            {
                flag = false;
                MessageBox.Show("Please Select Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;
            }
            if (gvDeliveryDetails.Rows.Count == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Valid Trip/LR No", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTripLRNo.Focus();
                return flag;               
            }
            if (txtVehModel.Text.Trim().Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Vehical Type", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtVehModel.Focus();
                return flag;                                    
            }
            if (txtVehNo.Text.Trim().Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Vehical Number", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtVehNo.Focus();
                return flag;                
            }
            double dTotFreight = 0, dAdvPaid = 0;

            if (txtTotalFreight.Text.Length != 0)
            {
                dTotFreight = Convert.ToDouble(txtTotalFreight.Text);
            }
            if (txtAdvancePaid.Text.Length != 0)
            {
                dAdvPaid = Convert.ToDouble(txtAdvancePaid.Text);
            }

            if (dAdvPaid > dTotFreight)
            {
                flag = false;
                MessageBox.Show("Advance Amount is Greater than or Equal To Total Freight", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAdvancePaid.Focus();
                return flag; 
            }
            if (gvDeliveryDetails.Rows.Count == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Valid Trip/LR Number", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTripLRNo.Focus();
                return flag; 
            }
            if (txtTransporterName.Text.Length == 0 || txtDriverName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Transporter Name or Driver Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTransporterName.Focus();
                return flag; 
            }
            if (chkCancel.Checked == true)
            {
                if (txtRemarks.Text.TrimEnd().Length <= 10)
                {
                    flag = false;
                    MessageBox.Show("Please Enter Remarks", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtRemarks.Focus();
                    return flag;
                }
            }

            return flag;
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {
                try
                {
                    if (SaveStockDumpingDetails() > 0)
                    {
                        MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(null, null);
                        flagUpdate = false;
                        GenerateTrnNo();
                        txtTripLRNo.Focus();
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
            else
            {
                txtName.Text = "";
            }
        }

        private void txtTripLRNo_Validated(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            try
            {
                if (txtTripLRNo.Text.Length > 0)
                {
                    strCmd = "SELECT SDH_BRANCH_CODE,SDH_TRN_NO TrnNo FROM STOCK_DUMP_HEAD " +
                             " WHERE SDH_BRANCH_CODE='" + CommonData.BranchCode +
                             "' and SDH_FIN_YEAR='" + CommonData.FinancialYear +
                             "' and SDH_LR_NO='" + txtTripLRNo.Text +
                             "' and SDH_DOC_MONTH ='" + CommonData.DocMonth + "'";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtTrnNo.Text = dt.Rows[0]["TrnNo"].ToString();
                        txtTrnNo_Validated(null, null);
                    }
                    else
                    {
                        FillDcDetails();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FillDcDetails()
        {
            objSPDB = new StockPointDB();
            DataTable dt = new DataTable();
            gvDeliveryDetails.Rows.Clear();
            try
            {
                dt = objSPDB.Get_DcDcSTNo_BY_LRNO_StockDumping(CommonData.BranchCode,"",CommonData.DocMonth,"",txtTripLRNo.Text);

                if (dt.Rows.Count > 0)
                {
                    txtTotalFreight.Text = dt.Rows[0]["sp_Tot_Freight"].ToString();
                    txtToPay.Text = dt.Rows[0]["sp_To_Pay"].ToString();
                    txtLoadingCharges.Text = dt.Rows[0]["sp_Load_Charges"].ToString();
                    txtAdvancePaid.Text = dt.Rows[0]["sp_Adv_Paid"].ToString();
                    cbCompany.SelectedValue = CommonData.CompanyCode;
                    cbBranches.SelectedValue = dt.Rows[0]["sp_to_branch_code"].ToString();

                    txtTransporterName.Text=dt.Rows[0]["sp_transporter_name"].ToString();
                    txtDriverName.Text=dt.Rows[0]["sp_driver_name"].ToString();
                    txtCleanerName.Text=dt.Rows[0]["sp_cleaner_name"].ToString();
                    txtDriverContactNo.Text=dt.Rows[0]["sp_mobile_no"].ToString();
                    txtTinNo.Text = dt.Rows[0]["sp_TinNo"].ToString();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {                       
                        gvDeliveryDetails.Rows.Add();
                        gvDeliveryDetails.Rows[i].Cells["SlNo"].Value = (i + 1).ToString();
                        gvDeliveryDetails.Rows[i].Cells["DCDate"].Value = Convert.ToDateTime(dt.Rows[i]["sp_DC_Date"].ToString()).ToString("dd/MMM/yyyy");
                        gvDeliveryDetails.Rows[i].Cells["ToBranCode"].Value = dt.Rows[i]["sp_to_branch_code"].ToString();
                        gvDeliveryDetails.Rows[i].Cells["GCEcode"].Value = dt.Rows[i]["sp_to_ecode"].ToString();
                        gvDeliveryDetails.Rows[i].Cells["DCDCSTNo"].Value = dt.Rows[i]["sp_DC_No"].ToString();
                        gvDeliveryDetails.Rows[i].Cells["GcName"].Value = dt.Rows[i]["sp_emp_name"].ToString();
                        gvDeliveryDetails.Rows[i].Cells["FromLoc"].Value = "";
                        gvDeliveryDetails.Rows[i].Cells["ToLoc"].Value = "";
                        gvDeliveryDetails.Rows[i].Cells["NoOfKM"].Value = "0";
                        gvDeliveryDetails.Rows[i].Cells["TotalTonnes"].Value = dt.Rows[i]["sp_DC_Tot_Tons"].ToString();
                        gvDeliveryDetails.Rows[i].Cells["ExpAsPerTon"].Value = dt.Rows[i]["sp_ExpPerTon"].ToString(); 
                        gvDeliveryDetails.Rows[i].Cells["DCExp"].Value = dt.Rows[i]["sp_Dc_Exp"].ToString();
                        gvDeliveryDetails.Rows[i].Cells["GrominBags"].Value = dt.Rows[i]["sp_Gromin_Qty"].ToString();
                        gvDeliveryDetails.Rows[i].Cells["Granuals"].Value = dt.Rows[i]["sp_Granuals_Qty"].ToString();
                        gvDeliveryDetails.Rows[i].Cells["Liquids"].Value = dt.Rows[i]["sp_Liquids_Qty"].ToString();
                        gvDeliveryDetails.Rows[i].Cells["TotUnits"].Value = dt.Rows[i]["sp_Tot_Qty"].ToString();                      

                    }
                    CalculateTotals();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
          



        private void GetStockDumpingDetails()
        {
            objSPDB = new StockPointDB();
            DataTable dtHead = new DataTable();
            DataTable dtDetl;
            Hashtable ht = new Hashtable(); 
            gvDeliveryDetails.Rows.Clear();
            if (txtTrnNo.Text.Length > 0)
            {
                try
                {
                    ht = objSPDB.Get_StockDumpingDetails(CommonData.CompanyCode, CommonData.BranchCode, CommonData.FinancialYear, CommonData.DocMonth, txtTrnNo.Text);

                    dtHead = (DataTable)ht["StockDumpHead"];
                    dtDetl = (DataTable)ht["StockDumpDetl"];

                    if (dtHead.Rows.Count > 0)
                    {
                        flagUpdate = true;
                        txtDocMonth.Text = dtHead.Rows[0]["DocMonth"].ToString();
                        txtTotalFreight.Text = dtHead.Rows[0]["TotFreight"].ToString();
                        txtToPay.Text = dtHead.Rows[0]["AdvPayAtCamp"].ToString();
                        txtLoadingCharges.Text = dtHead.Rows[0]["LoadAmt"].ToString();
                        txtAdvancePaid.Text = dtHead.Rows[0]["AdvPaidAtUnit"].ToString();
                        cbCompany.SelectedValue = CommonData.CompanyCode;
                        cbBranches.SelectedValue = dtHead.Rows[0]["ToBranchCode"].ToString();
                        txtTripLRNo.Text = dtHead.Rows[0]["LrNo"].ToString();
                        txtTransporterName.Text = dtHead.Rows[0]["TransporterName"].ToString();
                        txtDriverName.Text = dtHead.Rows[0]["DriverName"].ToString();
                        txtCleanerName.Text = dtHead.Rows[0]["CleanerName"].ToString();
                        txtDriverContactNo.Text = dtHead.Rows[0]["ContactNo"].ToString();
                        txtVehModel.Text = dtHead.Rows[0]["VehType"].ToString();
                        txtVehNo.Text = dtHead.Rows[0]["VehNo"].ToString();
                        txtRemarks.Text = dtHead.Rows[0]["Remarks"].ToString();
                        txtEcodeSearch.Text = dtHead.Rows[0]["GCEcode"].ToString();
                        txtName.Text = dtHead.Rows[0]["GCName"].ToString();
                        txtTinNo.Text = dtHead.Rows[0]["BranchTinNo"].ToString();
                        txtCostPerKM.Text = dtHead.Rows[0]["CostPerKM"].ToString();
                        txtCostPerTon.Text = dtHead.Rows[0]["CostPerTon"].ToString();
                        txtTotDcAmt.Text = dtHead.Rows[0]["TotCost"].ToString();

                        if (dtHead.Rows[0]["Status"].ToString() == "CANCELLED")
                        {
                            chkCancel.Checked = true;
                        }
                        else
                        {
                            chkCancel.Checked = false;
                        }

                        for (int i = 0; i < dtDetl.Rows.Count; i++)
                        {

                            gvDeliveryDetails.Rows.Add();
                            gvDeliveryDetails.Rows[i].Cells["SlNo"].Value = (i + 1).ToString();
                            gvDeliveryDetails.Rows[i].Cells["DCDate"].Value = Convert.ToDateTime(dtDetl.Rows[i]["DCDate"].ToString()).ToString("dd/MMM/yyyy");
                            gvDeliveryDetails.Rows[i].Cells["ToBranCode"].Value = dtDetl.Rows[i]["ToBranchCode"].ToString();
                            gvDeliveryDetails.Rows[i].Cells["GCEcode"].Value = dtDetl.Rows[i]["ToEcode"].ToString();
                            gvDeliveryDetails.Rows[i].Cells["DCDCSTNo"].Value = dtDetl.Rows[i]["DCDcstNo"].ToString();
                            gvDeliveryDetails.Rows[i].Cells["GcName"].Value = dtDetl.Rows[i]["GCName"].ToString();
                            gvDeliveryDetails.Rows[i].Cells["FromLoc"].Value = dtDetl.Rows[i]["FromLoc"].ToString();
                            gvDeliveryDetails.Rows[i].Cells["ToLoc"].Value = dtDetl.Rows[i]["ToLoc"].ToString();
                            gvDeliveryDetails.Rows[i].Cells["NoOfKM"].Value = dtDetl.Rows[i]["NoOfKm"].ToString();
                            gvDeliveryDetails.Rows[i].Cells["TotalTonnes"].Value = dtDetl.Rows[i]["TotTons"].ToString();
                            gvDeliveryDetails.Rows[i].Cells["ExpAsPerTon"].Value = (Convert.ToDouble(txtCostPerTon.Text)*Convert.ToDouble(dtDetl.Rows[i]["TotTons"]));                              gvDeliveryDetails.Rows[i].Cells["DCExp"].Value = dtDetl.Rows[i]["DcExp"].ToString();
                            gvDeliveryDetails.Rows[i].Cells["GrominBags"].Value = dtDetl.Rows[i]["GrmnQty"].ToString();
                            gvDeliveryDetails.Rows[i].Cells["Granuals"].Value = dtDetl.Rows[i]["GranualsQty"].ToString();
                            gvDeliveryDetails.Rows[i].Cells["Liquids"].Value = dtDetl.Rows[i]["LiquidsQty"].ToString();
                            gvDeliveryDetails.Rows[i].Cells["TotUnits"].Value = dtDetl.Rows[i]["TotQty"].ToString();

                        }
                        CalculateTotals();
                        CalculateDCWiseExpenditure();

                    }
                    else
                    {
                        btnCancel_Click(null,null);
                        flagUpdate = false;
                        GenerateTrnNo();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }

        private void gvDeliveryDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            double dGrmnBags = 0, dGranuals = 0, dLiquids = 0;

            if (Convert.ToString(gvDeliveryDetails.Rows[e.RowIndex].Cells["GrominBags"].Value) != "")
            {
                dGrmnBags = Convert.ToDouble(gvDeliveryDetails.Rows[e.RowIndex].Cells["GrominBags"].Value);                
               
            }
            if (Convert.ToString(gvDeliveryDetails.Rows[e.RowIndex].Cells["Granuals"].Value) != "")
            {
                dGranuals = Convert.ToDouble(gvDeliveryDetails.Rows[e.RowIndex].Cells["Granuals"].Value);               
                
            }
            if (Convert.ToString(gvDeliveryDetails.Rows[e.RowIndex].Cells["Liquids"].Value) != "")
            {
                dLiquids = Convert.ToDouble(gvDeliveryDetails.Rows[e.RowIndex].Cells["Liquids"].Value);
            }

            gvDeliveryDetails.Rows[e.RowIndex].Cells["TotUnits"].Value = Convert.ToDouble(gvDeliveryDetails.Rows[e.RowIndex].Cells["GrominBags"].Value) + Convert.ToDouble(gvDeliveryDetails.Rows[e.RowIndex].Cells["Granuals"].Value) + Convert.ToDouble(gvDeliveryDetails.Rows[e.RowIndex].Cells["Liquids"].Value);          
           

            CalculateTotals();
            CalculateDCWiseExpenditure();
        }

        private void CalculateTotals()
        {
            double dTotUnits = 0, dTotExp = 0, dTotTons = 0, dTotKM = 0, dTotFrght = 0, dAdvPaid = 0, dLoadingCharges = 0, dCostPerTon = 0, dTotAmount = 0;
            try
            {
                
                if (gvDeliveryDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < gvDeliveryDetails.Rows.Count; i++)
                    {
                        if (Convert.ToString(gvDeliveryDetails.Rows[i].Cells["TotUnits"].Value) != "")
                        {
                            dTotUnits += Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["TotUnits"].Value);
                        }

                        if (Convert.ToString(gvDeliveryDetails.Rows[i].Cells["ExpAsPerTon"].Value) != "")
                        {
                            dTotExp += Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["ExpAsPerTon"].Value);
                        }

                        if (Convert.ToString(gvDeliveryDetails.Rows[i].Cells["TotalTonnes"].Value) != "")
                        {
                            dTotTons += Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["TotalTonnes"].Value);
                        }

                        if (Convert.ToString(gvDeliveryDetails.Rows[i].Cells["NoOfKM"].Value) != "")
                        {
                            dTotKM += Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["NoOfKM"].Value);
                        }
                    }

                    if (txtTotalFreight.Text.Length > 0)
                    {
                        dTotFrght = Convert.ToDouble(txtTotalFreight.Text);
                    }

                    if (txtAdvancePaid.Text.Length > 0)
                    {
                        dAdvPaid = Convert.ToDouble(txtAdvancePaid.Text);
                    }

                    if (txtLoadingCharges.Text.Length > 0)
                    {
                        dLoadingCharges = Convert.ToDouble(txtLoadingCharges.Text);
                    }
                   

                    if (dAdvPaid < dTotFrght)
                        txtToPay.Text = (dTotFrght - dAdvPaid).ToString("0.00");
                    else
                        txtToPay.Text = (dAdvPaid - dTotFrght).ToString("0.00");

                    txtTotAmt.Text = (dTotFrght + dLoadingCharges).ToString("0.00");
                                                          

                    txtTotDcAmt.Text = dTotExp.ToString("f");
                    txtTotQty.Text = dTotUnits.ToString("f");
                    txtTotTons.Text = dTotTons.ToString("f");
                    if (txtTotAmt.Text.Length != 0)
                    {
                        dTotExp = Convert.ToDouble(txtTotAmt.Text);
                        txtCostPerTon.Text = (dTotExp / dTotTons).ToString("0.00");
                    }

                    if (txtCostPerTon.Text.Length != 0)
                        dCostPerTon = Convert.ToDouble(txtCostPerTon.Text);

                    if (gvDeliveryDetails.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvDeliveryDetails.Rows.Count; i++)
                        {
                            dTotTons = Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["TotalTonnes"].Value);
                            gvDeliveryDetails.Rows[i].Cells["ExpAsPerTon"].Value = (dCostPerTon * dTotTons).ToString("0.00");
                            dTotAmount += Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["ExpAsPerTon"].Value);
                        }

                        txtTotDcAmt.Text = dTotAmount.ToString("0.00");
                    }

                    if (dTotKM != 0)
                        txtCostPerKM.Text = (dTotExp / dTotKM).ToString("0.00");
                    else
                        txtCostPerKM.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            gvDeliveryDetails.Rows.Clear();
            GenerateTrnNo();
            txtVehModel.Text = "";
            txtVehNo.Text ="";
            //txtTripLRNo.Text = "";
            txtAdvancePaid.Text = "";
            txtCleanerName.Text = "";
            txtDriverContactNo.Text = "";
            txtDriverName.Text = "";
            txtEcodeSearch.Text = "";
            txtCostPerTon.Text = "";
            txtTotQty.Text = "";
            txtTotDcAmt.Text = "";
            txtToPay.Text = "";
            txtTinNo.Text = "";
            txtRemarks.Text = "";
            txtName.Text = "";
            txtTotalFreight.Text = "";
            flagUpdate = false;
            txtTransporterName.Text = "";
            txtLoadingCharges.Text = "";
            chkCancel.Checked = false;
            txtCostPerKM.Text = "";
            txtTotAmt.Text = "";
            txtTripLRNo.Text = "";
            txtTotTons.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int iRes = 0;
            objSQLdb = new SQLDB();
            string strCmd = "";

            if (txtTrnNo.Text.Length>0 && flagUpdate == true)
            {
                DialogResult result = MessageBox.Show("Do you want to delete This Record ?",
                                        "Stock Dumping", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {

                        strCmd = "DELETE FROM STOCK_DUMP_DETL WHERE SDD_COMP_CODE='"+CommonData.CompanyCode +
                                 "' and SDD_BRANCH_CODE='"+ CommonData.BranchCode +"' and SDD_FIN_YEAR ='"+ CommonData.FinancialYear +
                                 "' and SDD_TRN_NO='"+  txtTrnNo.Text+"'";

                        strCmd += " DELETE FROM STOCK_DUMP_HEAD WHERE SDH_COMP_CODE='"+ CommonData.CompanyCode +
                                 "' and SDH_BRANCH_CODE='"+ CommonData.BranchCode +
                                 "' and SDH_FIN_YEAR='"+ CommonData.FinancialYear +"' and SDH_TRN_NO='"+ txtTrnNo.Text +"'";
                        

                        if (strCmd.Length > 10)
                        {
                            iRes = objSQLdb.ExecuteSaveData(strCmd);
                        }
                        if (iRes > 0)
                        {
                            MessageBox.Show("Data Deleted Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);                            
                            btnCancel_Click(null, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
                else
                {
                    MessageBox.Show("Data Not Deleted", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void txtTrnNo_Validated(object sender, EventArgs e)
        {
            if (txtTrnNo.Text.Length > 0)
            {
                GetStockDumpingDetails();
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

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46) && (flagText == false))
            {
                e.Handled = true;
                return;
            }

            // checks to make sure only 1 decimal is allowed
            else if (e.KeyChar == 46 && flagText == false)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void gvDeliveryDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            intCurrentRow = (((System.Windows.Forms.DataGridView)(sender))).CurrentCell.RowIndex;
            intCurrentCell = (((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex;
            flagText = true;

            if (this.gvDeliveryDetails.CurrentCell.ColumnIndex == gvDeliveryDetails.Columns["NoOfKM"].Index & (e.Control != null))
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    flagText = false;
                    txtQty.MaxLength = 12;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }          
                    
        }

        private void txtTotalFreight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }

        }

        private void txtAdvancePaid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void txtLoadingCharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void CalculateDCWiseExpenditure()
        {
            //double dTotFrght = 0, dAdvPaid = 0, dLoadingCharges = 0, dCostPerTon = 0, dTotTons = 0, dTotAmount = 0;


            //try
            //{
            //    if (txtTotalFreight.Text.Length > 0)
            //    {
            //        dTotFrght = Convert.ToDouble(txtTotalFreight.Text);
            //    }

            //    if (txtAdvancePaid.Text.Length > 0)
            //    {
            //        dAdvPaid = Convert.ToDouble(txtAdvancePaid.Text);
            //    }

            //    if (txtLoadingCharges.Text.Length > 0)
            //    {
            //        dLoadingCharges = Convert.ToDouble(txtLoadingCharges.Text);
            //    }

            //    CalculateTotals();

            //    if (dAdvPaid < dTotFrght)
            //        txtToPay.Text = (dTotFrght - dAdvPaid).ToString("0.00");
            //    else
            //        txtToPay.Text = (dAdvPaid - dTotFrght).ToString("0.00");

            //    txtTotAmt.Text = (dTotFrght + dLoadingCharges).ToString("0.00");

            //    if(txtCostPerTon.Text.Length!=0)
            //    dCostPerTon = Convert.ToDouble(txtCostPerTon.Text);


            //    if (gvDeliveryDetails.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < gvDeliveryDetails.Rows.Count; i++)
            //        {
            //            dTotTons = Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["TotalTonnes"].Value);
            //            gvDeliveryDetails.Rows[i].Cells["ExpAsPerTon"].Value = (dCostPerTon * dTotTons).ToString("0.00");
            //            dTotAmount += Convert.ToDouble(gvDeliveryDetails.Rows[i].Cells["ExpAsPerTon"].Value);
            //        }
            //        txtTotDcAmt.Text = dTotAmount.ToString("0.00");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            

        }

        private void txtDriverContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtTotalFreight_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateDCWiseExpenditure();
        }

        private void txtAdvancePaid_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateDCWiseExpenditure();
        }

        private void txtLoadingCharges_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateDCWiseExpenditure();
        }

        

    }
}
