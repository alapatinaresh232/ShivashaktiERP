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
using SSCRM.App_Code;

namespace SSCRM
{
    public partial class StationaryBrGRN : Form
    {
        private SQLDB objSQLDB = null;
        StationaryDB objStationaryDB;
        private string ScreenType = "";
        

        public StationaryBrGRN()
        {
            InitializeComponent();
        }
        public StationaryBrGRN( string ScType)
        {
            InitializeComponent();
            ScreenType = ScType;

        }
        string BranchCode = "", CompanyCode = "", FinYear = CommonData.FinancialYear;
       
           

        private void StationaryIndent_Load(object sender, EventArgs e)
        {
            DataSet ds = null;
            lblCmpCode.Visible = false;
            lblBranchCode.Visible = false;
            txtAddress.Visible = false;
            dtGrnDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            txtItemsCount.Text = "0.00"; ;
            txtGrnAmt.Text = "0.00";
            Int32 Ecode =Convert.ToInt32(CommonData.LogUserEcode);
            gvIndentDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10,
                                                        System.Drawing.FontStyle.Regular);

            gvIndentDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10,
                                                        System.Drawing.FontStyle.Regular);
            cbGrnType.SelectedIndex = 0;

            if (CommonData.LogUserId == "admin" || CommonData.LogUserId == "ADMIN")
            {
                btnDelete.Enabled = true;
            }
            else
            {
                btnDelete.Enabled = false;
            }

            if (ScreenType == "SELF")
            {
                cbGrnType.Visible = false;
                label12.Visible = false;
            }
            else
            {
                cbGrnType.Visible = true;
                label12.Visible = true;
            }

            objSQLDB = new SQLDB();
            objStationaryDB = new StationaryDB();
            if (ScreenType == "SELF")
            {
                DataTable dt = new DataTable();
                string SqlCmd = "";
                try
                {
                    SqlCmd = "SELECT BM.COMPANY_CODE CompanyCode,CM_COMPANY_NAME CompanyName,BRANCH_NAME BranchName" +
                                               ",BM.BRANCH_CODE BranchCode,MEMBER_NAME " +
                                               " FROM EORA_MASTER EM " +
                                               " INNER JOIN BRANCH_MAS BM ON BM.BRANCH_CODE=EM.BRANCH_CODE " +
                                               " INNER JOIN COMPANY_MAS CM ON CM.CM_COMPANY_CODE=BM.COMPANY_CODE WHERE EM.ECODE='" + CommonData.LogUserEcode + "'";
                    dt = objSQLDB.ExecuteDataSet(SqlCmd).Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }              

                if (dt.Rows.Count > 0)
                {
                    CompanyCode = dt.Rows[0]["CompanyCode"].ToString();
                    BranchCode = dt.Rows[0]["BranchCode"].ToString();
                    txtCompanyName.Text = dt.Rows[0]["CompanyName"].ToString();
                    txtBranchName.Text = dt.Rows[0]["BranchName"].ToString();
                }
            }
            txtGrnNo.Text = GenerateGRNNo();
            txtIndentNo_Validated(null, null);
            if (ScreenType == "SELF")
            {
                try
                {
                    ds = objStationaryDB.GetSelfDCsList_ForBranchGRN(CompanyCode, BranchCode, CommonData.FinancialYear, Ecode);
                    UtilityLibrary.PopulateControl(cmbDC, ds.Tables[0].DefaultView, 0, 1, "--Please Select--", 0);
                    txtTotalFreight.Text = ds.Tables[0].Rows[0]["TotalFreight"].ToString();
                    txtAdvPaid.Text = ds.Tables[0].Rows[0]["AdvPaid"].ToString();
                    txtToPay.Text = ds.Tables[0].Rows[0]["ToPay"].ToString();
                   
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                objStationaryDB = null;
                }
            }
            else
            {

                txtCompanyName.Text = CommonData.CompanyName;
                txtBranchName.Text = CommonData.BranchName;
                try
                {
                    ds = objStationaryDB.GetDCsList_ForBranchGRN(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth);
                    UtilityLibrary.PopulateControl(cmbDC, ds.Tables[0].DefaultView, 0, 1, "--Please Select--", 0);
                    txtTotalFreight.Text = ds.Tables[0].Rows[0]["TotalFreight"].ToString();
                    txtAdvPaid.Text = ds.Tables[0].Rows[0]["AdvPaid"].ToString();
                    txtToPay.Text = ds.Tables[0].Rows[0]["ToPay"].ToString();
                  
                  
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                    
                finally
                {
                    objStationaryDB = null;
                }
            }
           
           
        }
                
        private string GenerateGRNNo()
        {
            objSQLDB = new SQLDB();
            string sIndNo = string.Empty;
             string sqlText="";
             try
             {
                 //if (ScreenType == "SELF")
                 //{
                 //    sqlText = "SELECT ISNULL(MAX(STGH_GRN_NO),0)+1 FROM ST_GRN_HEAD WHERE STGH_COMPANY_CODE='" + CompanyCode + "' AND STGH_BRANCH_CODE='" + BranchCode + "' AND STGH_FIN_YEAR='" + CommonData.FinancialYear + "'";
                 //    sIndNo = objSQLDB.ExecuteDataSet(sqlText).Tables[0].Rows[0][0].ToString();
                 //}
                 //else
                 //{

                     sqlText = "SELECT ISNULL(MAX(STGH_GRN_NO),0)+1 FROM ST_GRN_HEAD WHERE STGH_COMPANY_CODE='" + CommonData.CompanyCode + "' AND STGH_BRANCH_CODE='" + CommonData.BranchCode + "' AND STGH_FIN_YEAR='" + CommonData.FinancialYear + "'";
                     sIndNo = objSQLDB.ExecuteDataSet(sqlText).Tables[0].Rows[0][0].ToString();
                 //}
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.ToString());
             }
            return sIndNo;
        }

        private void gvIndentDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4 || e.ColumnIndex == 6)
            {
                if (Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["Price"].Value) >= 0 && Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["GRNQty"].Value) >= 0)
                {
                    gvIndentDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["GRNQty"].Value) * (Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["Price"].Value));
                    gvIndentDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["Amount"].Value).ToString("f");
                    if (cbGrnType.SelectedIndex == 1)
                    {
                        gvIndentDetails.Rows[e.RowIndex].Cells["DCQty"].Value = Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["GRNQty"].Value);
                    }
                    gvIndentDetails.Rows[e.RowIndex].Cells["ExQty"].Value = Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["DCQty"].Value) - Convert.ToDouble(gvIndentDetails.Rows[e.RowIndex].Cells["GRNQty"].Value);

                }
            }
            CalculateTot();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        public void CalculateTot()
        {
            double iTotalAmt = 0;
            for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
            {
                if (gvIndentDetails.Rows[i].Cells["Amount"].Value.ToString() != "")
                    iTotalAmt += Convert.ToDouble(gvIndentDetails.Rows[i].Cells["Amount"].Value);
                else
                    iTotalAmt += 0;
            }
            txtGrnAmt.Text = iTotalAmt.ToString();
        }

        private bool CheckData()
        {
            bool flag = true;
            if (cbGrnType.SelectedIndex == 1)
            {
                if (txtDCNo.Text.Trim().Length == 0 || txtDCNo.Text.Trim().Length <= 4)
                {
                    flag = false;
                    MessageBox.Show("Please Enter Supplier Name","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    txtDCNo.Focus();
                    return flag;
                }
            }
            if (dtGrnDate.Value > DateTime.Now)
            {
                flag = false;
                MessageBox.Show("Please Select Valid GRN Date", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtGrnDate.Focus();
                return flag;
            }
           
            return flag;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            DataSet dsCnt = null;
            string strQryD = "", strQryF = "";
            int ivals = 0;
            int ivalsHead = 0;
            if (CheckData() == true)
            {
                if (gvIndentDetails.Rows.Count > 0)
                {
                    try
                    {
                        if (txtGrnAmt.Text.Length == 0)
                        {
                            txtGrnAmt.Text = "0.00";
                        }
                        string[] strDCValue = cmbDC.SelectedValue.ToString().Split('@');
                        if (ScreenType == "SELF")
                        {
                            dsCnt = objSQLDB.ExecuteDataSet("SELECT *FROM ST_GRN_HEAD WHERE STGH_COMPANY_CODE='" + CompanyCode + "' AND STGH_BRANCH_CODE='" + BranchCode + "' AND STGH_FIN_YEAR='" + CommonData.FinancialYear + "' AND STGH_GRN_NO='" + txtGrnNo.Text + "'");
                        }
                        else
                        {
                            dsCnt = objSQLDB.ExecuteDataSet("SELECT *FROM ST_GRN_HEAD WHERE STGH_COMPANY_CODE='" + CommonData.CompanyCode + "' AND STGH_BRANCH_CODE='" + CommonData.BranchCode + "' AND STGH_FIN_YEAR='" + CommonData.FinancialYear + "' AND STGH_GRN_NO='" + txtGrnNo.Text + "'");
                        }
                        if (dsCnt.Tables[0].Rows.Count == 0)
                        {
                            txtGrnNo.Text = GenerateGRNNo();
                            if (ScreenType == "SELF")
                            {
                                strQryD += " INSERT INTO ST_GRN_HEAD(STGH_COMPANY_CODE,STGH_STATE_CODE,STGH_BRANCH_CODE,STGH_FIN_YEAR,STGH_GRN_NO,STGH_AGAINEST_DC_NO,STGH_FROM_BRANCH_CODE,STGH_GRN_DATE,STGH_LR_POD_NO,STGH_DOOR_DELIVERY_FLAG,STGH_TRANSPORT_OR_COURIER,STGH_TRANSPORTER_OR_CORRIER_NAME,STGH_TOTAL_FRAIGHT,STGH_ADV_PAID," +
                                   "STGH_TO_PAY,STGH_BAGS_OR_PACKETS_COUNT,STGH_CREATED_BY,STGH_CREATED_DATE,STGH_GRN_AMOUNT) VALUES ('" + CompanyCode + "','" + BranchCode.Substring(3, 2) + "','" + BranchCode + "','" + CommonData.FinancialYear + "'," + txtGrnNo.Text +
                                     "," + strDCValue[2] + ",'" + strDCValue[0] + "','" + Convert.ToDateTime(dtGrnDate.Value).ToString("dd/MMM/yyyy") + "','" + txtTripLRNo.Text + "','" + txtDeliverFlg.Text +
                                     "','" + txtTransportorCourier.Text + "','" + txtTrnsorCourName.Text + "'," + txtTotalFreight.Text + ",'" + txtAdvPaid.Text + "','" + txtToPay.Text +
                                     "'," + txtBagsCount.Text + ",'" + CommonData.LogUserId + "','" + CommonData.CurrentDate + "'," + txtGrnAmt.Text + ")";
                            }
                            else
                            {

                                strQryD += " INSERT INTO ST_GRN_HEAD(STGH_COMPANY_CODE " +
                                                                   ",STGH_STATE_CODE " +
                                                                   ",STGH_BRANCH_CODE " +
                                                                   ",STGH_FIN_YEAR " +
                                                                   ",STGH_GRN_NO " +
                                                                   ",STGH_GRN_DATE " +
                                                                   ",STGH_AGAINEST_DC_NO " +
                                                                   ",STGH_FROM_BRANCH_CODE " +
                                                                   ",STGH_LR_POD_NO " +
                                                                   ",STGH_DOOR_DELIVERY_FLAG " +
                                                                   ",STGH_TRANSPORT_OR_COURIER " +
                                                                   ",STGH_TRANSPORTER_OR_CORRIER_NAME " +
                                                                   ",STGH_TOTAL_FRAIGHT,STGH_ADV_PAID " +
                                                                   ",STGH_TO_PAY " +
                                                                   ",STGH_BAGS_OR_PACKETS_COUNT " +
                                                                   ",STGH_SUPPLIER_ADDRESS " +
                                                                   ",STGH_CREATED_BY " +
                                                                   ",STGH_CREATED_DATE " +
                                                                   ",STGH_GRN_AMOUNT " +
                                                                   ",STGH_GRN_TYPE " +
                                                                   ") VALUES " +
                                                                   "('" + CommonData.CompanyCode +
                                                                   "','" + CommonData.StateCode +
                                                                   "','" + CommonData.BranchCode +
                                                                   "','" + CommonData.FinancialYear +
                                                                   "'," + txtGrnNo.Text +
                                                                   ",'" + Convert.ToDateTime(dtGrnDate.Value).ToString("dd/MMM/yyyy") + "'";
                                if (cbGrnType.SelectedIndex == 0)
                                {
                                    strQryD += "," + strDCValue[2] + ",'" + strDCValue[0] + "','" + txtTripLRNo.Text + "','" + txtDeliverFlg.Text +
                                              "','" + txtTransportorCourier.Text + "','" + txtTrnsorCourName.Text + "'," + txtTotalFreight.Text +
                                              ",'" + txtAdvPaid.Text + "','" + txtToPay.Text + "'," + txtBagsCount.Text + ",''";
                                }
                                if (cbGrnType.SelectedIndex == 1)
                                {
                                    strQryD += ",0,'" + txtDCNo.Text.ToString().Replace("'", "") +
                                                 "','" + txtTripLRNo.Text + "','NO','','',0,0,0,0,'" + txtAddress.Text.ToString().Replace("'", "") + "'";
                                }
                                strQryD += ",'" + CommonData.LogUserId + "','" + CommonData.CurrentDate + "'," + txtGrnAmt.Text +
                                            ",'" + cbGrnType.Text.ToString() + "')";
                            }
                        }
                        else
                        {
                            //strDCValue = txtDCNo.Text.ToString().Split('-');
                            if (ScreenType == "SELF")
                            {
                                strQryD += " UPDATE ST_GRN_HEAD SET STGH_GRN_DATE='" + Convert.ToDateTime(dtGrnDate.Value).ToString("dd/MMM/yyyy") +
                                 "',STGH_LR_POD_NO='" + txtTripLRNo.Text + "',STGH_TRANSPORT_OR_COURIER='" + txtTransportorCourier.Text +
                                 "',STGH_TRANSPORTER_OR_CORRIER_NAME='" + txtTrnsorCourName.Text + "',STGH_TOTAL_FRAIGHT=" + txtTotalFreight.Text + ",STGH_ADV_PAID=" + txtAdvPaid.Text +
                                 ",STGH_TO_PAY=" + txtToPay.Text + ",STGH_BAGS_OR_PACKETS_COUNT=" + txtBagsCount.Text + ",STGH_LAST_MODIFIED_BY='" + CommonData.LogUserId + "',STGH_LAST_MODIFIED_DATE='" + CommonData.CurrentDate +
                                 "',STGH_GRN_AMOUNT=" + txtGrnAmt.Text + " WHERE STGH_COMPANY_CODE='" + CompanyCode + "' AND STGH_BRANCH_CODE='" + BranchCode + "' AND STGH_FIN_YEAR='" + CommonData.FinancialYear + "' AND STGH_GRN_NO='" + txtGrnNo.Text + "'";

                                strQryD += " DELETE FROM ST_GRN_DETL WHERE STGD_COMPANY_CODE='" + CompanyCode + "' AND STGD_BRANCH_CODE='" + BranchCode + "' AND STGD_FIN_YEAR='" + CommonData.FinancialYear + "' AND STGD_GRN_NO='" + txtGrnNo.Text + "'";
                            }
                            else
                            {
                                if (cbGrnType.SelectedIndex == 1)
                                {
                                    txtDeliverFlg.Text = "NO";
                                    txtBagsCount.Text = "0";
                                    txtAdvPaid.Text = "0";
                                    txtTotalFreight.Text = "0";
                                    txtToPay.Text = "0";
                                }



                                strQryD += " UPDATE ST_GRN_HEAD SET STGH_GRN_DATE='" + Convert.ToDateTime(dtGrnDate.Value).ToString("dd/MMM/yyyy") +
                                    "',STGH_LR_POD_NO='" + txtTripLRNo.Text + "',STGH_DOOR_DELIVERY_FLAG='" + txtDeliverFlg.Text +
                                    "', STGH_TRANSPORT_OR_COURIER='" + txtTransportorCourier.Text +
                                    "',STGH_TRANSPORTER_OR_CORRIER_NAME='" + txtTrnsorCourName.Text + "',STGH_TOTAL_FRAIGHT=" + txtTotalFreight.Text +
                                    ",STGH_ADV_PAID=" + txtAdvPaid.Text + ",STGH_TO_PAY=" + txtToPay.Text + ",STGH_BAGS_OR_PACKETS_COUNT=" + txtBagsCount.Text +
                                    ",STGH_LAST_MODIFIED_BY='" + CommonData.LogUserId + "',STGH_LAST_MODIFIED_DATE='" + CommonData.CurrentDate +
                                    "',STGH_GRN_AMOUNT=" + txtGrnAmt.Text + ",STGH_GRN_TYPE='" + cbGrnType.Text.ToString() + "'";
                                if (cbGrnType.SelectedIndex == 1)
                                {
                                    strQryD += ",STGH_FROM_BRANCH_CODE='" + txtDCNo.Text.ToString().Replace("'", "") + "'";
                                }
                                strQryD += ",STGH_SUPPLIER_ADDRESS='" + txtAddress.Text.ToString().Replace("'", "") + "' WHERE STGH_COMPANY_CODE='" + CommonData.CompanyCode +
                                   "' AND STGH_BRANCH_CODE='" + CommonData.BranchCode + "' AND STGH_FIN_YEAR='" + CommonData.FinancialYear +
                                   "' AND STGH_GRN_NO='" + txtGrnNo.Text + "'";

                                strQryD += " DELETE FROM ST_GRN_DETL WHERE STGD_COMPANY_CODE='" + CommonData.CompanyCode + "' AND STGD_BRANCH_CODE='" + CommonData.BranchCode + "' AND STGD_FIN_YEAR='" + CommonData.FinancialYear + "' AND STGD_GRN_NO='" + txtGrnNo.Text + "'";
                            }
                        }
                        if (strQryD.Length > 10)
                        {
                            ivalsHead = objSQLDB.ExecuteSaveData(strQryD);
                        }

                        if (ivalsHead > 0)
                        {
                            for (int i = 0; i < gvIndentDetails.Rows.Count; i++)
                            {
                                string sFrom = gvIndentDetails.Rows[i].Cells["FrmNo"].Value.ToString() == "" ? "0" : gvIndentDetails.Rows[i].Cells["FrmNo"].Value.ToString();
                                string sTo = gvIndentDetails.Rows[i].Cells["ToNo"].Value.ToString() == "" ? "0" : gvIndentDetails.Rows[i].Cells["ToNo"].Value.ToString();
                                if (ScreenType == "SELF")
                                {
                                    strQryF += " INSERT INTO ST_GRN_DETL(STGD_COMPANY_CODE,STGD_STATE_CODE,STGD_BRANCH_CODE,STGD_GRN_NO,STGD_GRN_SL_NO,STGD_ITEM_ID,STGD_ITEM_DC_QTY" +
                                ",STGD_ITEM_GRN_QTY,STGD_SHORT_OR_EXCESS_QTY,STGD_PRICE,STGD_AMOUNT,STGD_FIN_YEAR) VALUES ('" + CompanyCode + "','" + BranchCode.Substring(3, 2) + "','" + BranchCode + "'," + txtGrnNo.Text +
                                    "," + Convert.ToInt32(i + 1) + "," + gvIndentDetails.Rows[i].Cells["ItemID"].Value + "," + gvIndentDetails.Rows[i].Cells["DCQty"].Value +
                                    "," + gvIndentDetails.Rows[i].Cells["GRNQty"].Value + "," + gvIndentDetails.Rows[i].Cells["ExQty"].Value + "," + gvIndentDetails.Rows[i].Cells["Price"].Value + "," + gvIndentDetails.Rows[i].Cells["Amount"].Value + ",'" + CommonData.FinancialYear + "')";
                                }
                                else
                                {
                                    strQryF += " INSERT INTO ST_GRN_DETL(STGD_COMPANY_CODE,STGD_STATE_CODE,STGD_BRANCH_CODE,STGD_GRN_NO " +
                                                ",STGD_GRN_SL_NO,STGD_ITEM_ID,STGD_ITEM_DC_QTY,STGD_ITEM_GRN_QTY,STGD_SHORT_OR_EXCESS_QTY " +
                                                ",STGD_PRICE,STGD_AMOUNT,STGD_FIN_YEAR) VALUES ('" + CommonData.CompanyCode +
                                                "','" + CommonData.StateCode +
                                                "','" + CommonData.BranchCode +
                                                "'," + txtGrnNo.Text +
                                                "," + Convert.ToInt32(i + 1) + "," + gvIndentDetails.Rows[i].Cells["ItemID"].Value +
                                                "," + gvIndentDetails.Rows[i].Cells["DCQty"].Value +
                                                "," + gvIndentDetails.Rows[i].Cells["GRNQty"].Value +
                                                "," + gvIndentDetails.Rows[i].Cells["ExQty"].Value +
                                                "," + gvIndentDetails.Rows[i].Cells["Price"].Value +
                                                "," + gvIndentDetails.Rows[i].Cells["Amount"].Value +
                                                ",'" + CommonData.FinancialYear + "')";

                                }
                            }
                            ivals = objSQLDB.ExecuteSaveData(strQryF);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        objSQLDB = null;
                    }
                    if (ivals > 0)
                    {
                        MessageBox.Show("Data saved successfully", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDCNo.Visible = false;
                        txtGrnNo.Text = GenerateGRNNo();
                        cmbDC.Visible = true;
                        txtDCDate.Text = "";
                        //txtBranchName.Text = "";
                        //txtCompanyName.Text = "";
                        txtBagsCount.Text = "";
                        txtDCNo.Text = "";
                        txtDeliverFlg.Text = "";
                        txtGrnAmt.Text = "";
                        txtItemsCount.Text = "";
                        txtToPay.Text = "";
                        txtTotalFreight.Text = "";
                        txtTransportorCourier.Text = "";
                        txtTripLRNo.Text = "";
                        txtTrnsorCourName.Text = "";
                        dtGrnDate.Value = DateTime.Now;
                        gvIndentDetails.Rows.Clear();
                    }
                    else
                        MessageBox.Show("Data Not saved ", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCancel_Click(null, null);
                }
            }
        }

        private void txtIndentNo_Validated(object sender, EventArgs e)
        {
            if (txtGrnNo.Text != "")
            {
                string sqlQry = "";
                objSQLDB = new SQLDB();
                try
                {
                    if (ScreenType == "SELF")
                    {
                        sqlQry = " SELECT *,SDN_DC_DATE,TM_TRANSPORT_NAME FROM ST_GRN_HEAD INNER JOIN BRANCH_MAS ON BRANCH_CODE = STGH_FROM_BRANCH_CODE " +
                                 " INNER JOIN  STATIONARY_DELIVERY_NOTE ON SDN_DELIVERY_NOTE_NO=STGH_AGAINEST_DC_NO  AND SDN_TO_COMP_CODE=STGH_COMPANY_CODE " +
                                 " AND  SDN_TO_BRANCH_CODE=STGH_BRANCH_CODE AND SDN_FIN_YEAR=STGH_FIN_YEAR " +
                                 " INNER JOIN TRANSPORT_MASTER ON TM_ID=SDN_TRANSPORTER_OR_CORRIER_NAME " +
                                 " WHERE STGH_COMPANY_CODE='" + CommonData.CompanyCode + "' AND STGH_BRANCH_CODE='" + CommonData.BranchCode +
                                 "' and STGH_FIN_YEAR = '" + CommonData.FinancialYear + "' AND STGH_GRN_NO=" + Convert.ToInt32(txtGrnNo.Text);
                        sqlQry += " SELECT SIM_ITEM_CODE AS ItemID,SIM_ITEM_NAME AS ItemName"+//,STDD_APPROVED_QTY AppQty " +
                                  ",STGD_ITEM_DC_QTY AS DispQty,STGD_ITEM_GRN_QTY AS GRNQty,STGD_SHORT_OR_EXCESS_QTY AS ExQty,STGD_PRICE AS Price,STGD_AMOUNT AS Amoun " +
                                  " FROM ST_GRN_DETL A  INNER JOIN STATIONARY_ITEMS_MASTER C ON A.STGD_ITEM_ID=C.SIM_ITEM_CODE " +
                                  " INNER JOIN ST_GRN_HEAD B ON B.STGH_GRN_NO=A.STGD_GRN_NO AND B.STGH_COMPANY_CODE=A.STGD_COMPANY_CODE AND B.STGH_BRANCH_CODE=A.STGD_BRANCH_CODE" +
                                  " AND A.STGD_FIN_YEAR=B.STGH_FIN_YEAR " +
                                  " INNER JOIN STATIONARY_DELIVERY_NOTE SDN ON SDN.SDN_DELIVERY_NOTE_NO=B.STGH_AGAINEST_DC_NO AND SDN.SDN_TO_COMP_CODE=B.STGH_COMPANY_CODE " +
                                  " AND SDN_TO_BRANCH_CODE=B.STGH_BRANCH_CODE AND SDN_FIN_YEAR=B.STGH_FIN_YEAR " +
                                 // " INNER JOIN STATIONARY_DC_DETL SDD ON SDD.STDD_DELIVERY_NOTE_NO=SDN.SDN_DELIVERY_NOTE_NO AND SDN.SDN_TO_COMP_CODE=SDD.STDD_TO_COMP_CODE " +
                                 // " AND SDD.STDD_TO_BRANCH_CODE=SDN.SDN_TO_BRANCH_CODE AND SDN.SDN_FIN_YEAR=SDD.STDD_FIN_YEAR " +
                                  " WHERE STGD_COMPANY_CODE='" + CommonData.CompanyCode +
                                  "' AND STGD_BRANCH_CODE='" + CommonData.BranchCode + "' and STGD_FIN_YEAR = '" + CommonData.FinancialYear + "' AND STGD_GRN_NO=" + txtGrnNo.Text;
                    }
                    else
                    {
                        sqlQry = " SELECT *,isnull(STGH_GRN_TYPE,'TRANSFER') GrnType,SDN_DC_DATE,TM_TRANSPORT_NAME FROM ST_GRN_HEAD "+
                                  " left JOIN BRANCH_MAS ON BRANCH_CODE = STGH_FROM_BRANCH_CODE " +
                                 " left join STATIONARY_DELIVERY_NOTE ON SDN_DELIVERY_NOTE_NO=STGH_AGAINEST_DC_NO  AND SDN_TO_COMP_CODE=STGH_COMPANY_CODE " +
                                 " AND  SDN_TO_BRANCH_CODE=STGH_BRANCH_CODE AND SDN_FIN_YEAR=STGH_FIN_YEAR " +
                                 " left JOIN TRANSPORT_MASTER ON TM_ID=SDN_TRANSPORTER_OR_CORRIER_NAME " +
                                 " WHERE STGH_COMPANY_CODE='" + CommonData.CompanyCode + "' AND STGH_BRANCH_CODE='" + CommonData.BranchCode +
                                 "' and STGH_FIN_YEAR = '" + CommonData.FinancialYear + "' AND STGH_GRN_NO=" + Convert.ToInt32(txtGrnNo.Text);
                        sqlQry += " SELECT SIM_ITEM_CODE AS ItemID,SIM_ITEM_NAME AS ItemName "+//,STDD_APPROVED_QTY AppQty " +
                                  ",STGD_ITEM_DC_QTY AS DispQty,STGD_ITEM_GRN_QTY AS GRNQty,STGD_SHORT_OR_EXCESS_QTY AS ExQty,STGD_PRICE AS Price,STGD_AMOUNT AS Amoun " +
                                  " FROM ST_GRN_DETL A  INNER JOIN STATIONARY_ITEMS_MASTER C ON A.STGD_ITEM_ID=C.SIM_ITEM_CODE " +
                                  " INNER JOIN ST_GRN_HEAD B ON B.STGH_GRN_NO=A.STGD_GRN_NO AND B.STGH_COMPANY_CODE=A.STGD_COMPANY_CODE AND B.STGH_BRANCH_CODE=A.STGD_BRANCH_CODE" +
                                  " AND A.STGD_FIN_YEAR=B.STGH_FIN_YEAR " +
                                  " left JOIN STATIONARY_DELIVERY_NOTE SDN ON SDN.SDN_DELIVERY_NOTE_NO=B.STGH_AGAINEST_DC_NO AND SDN.SDN_TO_COMP_CODE=B.STGH_COMPANY_CODE " +
                                  " AND SDN_TO_BRANCH_CODE=B.STGH_BRANCH_CODE AND SDN_FIN_YEAR=B.STGH_FIN_YEAR " +
                                 // " INNER JOIN STATIONARY_DC_DETL SDD ON SDD.STDD_DELIVERY_NOTE_NO=SDN.SDN_DELIVERY_NOTE_NO AND SDN.SDN_TO_COMP_CODE=SDD.STDD_TO_COMP_CODE " +
                                 // " AND SDD.STDD_TO_BRANCH_CODE=SDN.SDN_TO_BRANCH_CODE AND SDN.SDN_FIN_YEAR=SDD.STDD_FIN_YEAR " +
                                  " WHERE STGD_COMPANY_CODE='" + CommonData.CompanyCode +
                                  "' AND STGD_BRANCH_CODE='" + CommonData.BranchCode + "' and STGD_FIN_YEAR = '" + CommonData.FinancialYear + "' AND STGD_GRN_NO=" + txtGrnNo.Text;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                DataSet ds = objSQLDB.ExecuteDataSet(sqlQry);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["GrnType"].ToString() == "PURCHASE")
                    {                        
                        cbGrnType.SelectedIndex = 1;
                        cbGrnType_SelectedIndexChanged(null, null);                        
                    }
                    else
                    {
                        cbGrnType.SelectedIndex = 0;
                        cbGrnType_SelectedIndexChanged(null, null);
                    }
                    cbGrnType.Enabled = false;

                    // ds.Tables[0].Rows[0]["STGH_AGAINEST_DC_NO"].ToString();
                    //ds.Tables[0].Rows[0]["STGH_FROM_BRANCH_CODE"].ToString();
                    dtGrnDate.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["STGH_GRN_DATE"]);
                    if (ds.Tables[0].Rows[0]["SDN_DC_DATE"].ToString() != "")
                    {
                        txtDCDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["SDN_DC_DATE"]).ToString("dd-MM-yyyy");
                    }
                    txtTripLRNo.Text = ds.Tables[0].Rows[0]["STGH_LR_POD_NO"].ToString();
                    txtDeliverFlg.Text = ds.Tables[0].Rows[0]["STGH_DOOR_DELIVERY_FLAG"].ToString();
                    txtTransportorCourier.Text = ds.Tables[0].Rows[0]["STGH_TRANSPORT_OR_COURIER"].ToString();
                    txtTrnsorCourName.Text = ds.Tables[0].Rows[0]["TM_TRANSPORT_NAME"].ToString();
                    txtTotalFreight.Text = ds.Tables[0].Rows[0]["STGH_TOTAL_FRAIGHT"].ToString();
                    txtAdvPaid.Text = ds.Tables[0].Rows[0]["STGH_ADV_PAID"].ToString();
                    txtToPay.Text = ds.Tables[0].Rows[0]["STGH_TO_PAY"].ToString();
                    txtBagsCount.Text = ds.Tables[0].Rows[0]["STGH_BAGS_OR_PACKETS_COUNT"].ToString();
                    txtGrnAmt.Text = ds.Tables[0].Rows[0]["STGH_GRN_AMOUNT"].ToString();
                    if (ds.Tables[0].Rows[0]["GrnType"].ToString() == "TRANSFER")
                    {
                        txtDCNo.Text = ds.Tables[0].Rows[0]["BRANCH_NAME"].ToString() + "-DC-" + ds.Tables[0].Rows[0]["STGH_AGAINEST_DC_NO"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["GrnType"].ToString() == "PURCHASE")
                    {
                        txtDCNo.Text = ds.Tables[0].Rows[0]["STGH_FROM_BRANCH_CODE"].ToString();
                        txtAddress.Text = ds.Tables[0].Rows[0]["STGH_SUPPLIER_ADDRESS"].ToString();
                    }

                    txtDCNo.Visible = true;
                    cmbDC.Visible = false;
                    //dtIndentDate.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["SIH_INDENT_DATE"]);
                    //txtIndentAmt.Text = ds.Tables[0].Rows[0]["SIH_INDENT_AMOUNT"].ToString();
                    //txtItemsCount.Text = ds.Tables[1].Rows.Count.ToString();
                    //lblAprRefNo.Text = ds.Tables[0].Rows[0]["SIH_APPROVAL_REF_NO"].ToString();
                    //if (ds.Tables[1].Rows.Count > 0)
                    //{
                    gvIndentDetails.Rows.Clear();
                    GetBindData(ds.Tables[1], 0);

                    //txtTotalFreight.Text = ds.Tables[1].Rows[0]["SDN_TOTAL_FRAIGHT"].ToString();
                    //txtAdvPaid.Text = ds.Tables[1].Rows[0]["SDN_ADV_PAID"].ToString();
                    //txtToPay.Text = ds.Tables[1].Rows[0]["SDN_TO_PAY"].ToString();
                    //txtBagsCount.Text = ds.Tables[1].Rows[0]["SDN_BAGS_OR_PACKS_COUNT"].ToString();

                    //txtTripLRNo.Text = ds.Tables[1].Rows[0]["SDN_LR_OR_POD_NO"].ToString();
                    //cmbTransporterOrCorrierName.Text = ds.Tables[1].Rows[0]["SDN_TRANSPORTER_OR_CORRIER_NAME"].ToString();
                    //}
                }
                else
                {
                    cbGrnType.Enabled = true;
                    txtDCNo.Visible = false;
                    cmbDC.Visible = true;
                    txtGrnNo.Text = GenerateGRNNo();
                    gvIndentDetails.Rows.Clear();
                    txtToPay.Text = "";
                    txtTotalFreight.Text = "";
                    txtTransportorCourier.Text = "";
                    txtTripLRNo.Text = "";
                    txtTrnsorCourName.Text = "";
                    dtGrnDate.Value = DateTime.Now;
                    txtAdvPaid.Text = "";
                    txtBagsCount.Text = "";
                    txtDCDate.Text = "";
                    txtDeliverFlg.Text = "";
                    cbGrnType.SelectedIndex = 0;
                    txtCompanyName.Text = CommonData.CompanyName;
                    txtBranchName.Text = CommonData.BranchName;
                    txtAddress.Text = "";
                    cbGrnType_SelectedIndexChanged(null,null);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            DataSet dsCnt = null;
            //if (cbGrnType.SelectedIndex == 0)
            //{
            if (ScreenType == "SELF")
            {
                dsCnt = objSQLDB.ExecuteDataSet("SELECT *FROM ST_GRN_HEAD WHERE STGH_COMPANY_CODE='" + CompanyCode + "' AND STGH_BRANCH_CODE='" + BranchCode + "' AND STGH_FIN_YEAR='" + CommonData.FinancialYear + "' AND STGH_GRN_NO='" + txtGrnNo.Text + "'");
            }
            else
            {
                dsCnt = objSQLDB.ExecuteDataSet("SELECT *FROM ST_GRN_HEAD WHERE STGH_COMPANY_CODE='" + CommonData.CompanyCode + "' AND STGH_BRANCH_CODE='" + CommonData.BranchCode + "' AND STGH_FIN_YEAR='" + CommonData.FinancialYear + "' AND STGH_GRN_NO='" + txtGrnNo.Text + "'");
            }
            if (dsCnt.Tables[0].Rows.Count > 0)
            {
                if (txtGrnNo.Text.Length > 0)
                {
                    string strQryD = " DELETE FROM ST_GRN_DETL WHERE STGD_COMPANY_CODE='" + CommonData.CompanyCode + "' AND STGD_BRANCH_CODE='" + CommonData.BranchCode +
                        "' and STGD_FIN_YEAR = '" + CommonData.FinancialYear + "' AND STGD_GRN_NO=" + Convert.ToInt32(txtGrnNo.Text);
                    strQryD += " DELETE FROM ST_GRN_HEAD WHERE STGH_COMPANY_CODE='" + CommonData.CompanyCode + "' AND STGH_BRANCH_CODE='" + CommonData.BranchCode +
                        "' and STGH_FIN_YEAR = '" + CommonData.FinancialYear + "' AND STGH_GRN_NO=" + Convert.ToInt32(txtGrnNo.Text);
                    int ivals = objSQLDB.ExecuteSaveData(strQryD);
                    if (ivals > 0)
                    {
                        MessageBox.Show("Data deleted successfully", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        StationaryIndent_Load(null, null);
                        gvIndentDetails.Rows.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Data Not deleted", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            StationaryIndent_Load(null, null);
            cbGrnType.Enabled = true;
            txtDCNo.Visible = false;
            txtGrnNo.Text = GenerateGRNNo();
            cmbDC.Visible = true;
            txtDCDate.Text = "";
            //txtBranchName.Text = "";
            //txtCompanyName.Text = "";
            txtAdvPaid.Text = "";
            txtBagsCount.Text = "";
            txtDCNo.Text = "";
            txtDeliverFlg.Text = "";
            txtGrnAmt.Text = "";
            txtItemsCount.Text = "";
            txtToPay.Text = "";
            txtTotalFreight.Text = "";
            txtTransportorCourier.Text = "";
            txtTripLRNo.Text = "";
            txtTrnsorCourName.Text = "";
            dtGrnDate.Value = DateTime.Now;
            txtAddress.Text = "";
            gvIndentDetails.Rows.Clear();
            cbGrnType.SelectedIndex = 0;
            cbGrnType_SelectedIndexChanged(null,null);
            //objStationaryDB = new StationaryDB();
            //DataSet ds = objStationaryDB.GetDCsList_ForBranchGRN(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth);
            //UtilityLibrary.PopulateControl(cmbDC, ds.Tables[0].DefaultView, 0, 1, "--Please Select--", 0);
            //objStationaryDB = null;
        }

        private void txtAdvPaid_KeyUp(object sender, KeyEventArgs e)
        {
            GetCal();
        }

        private void txtTotalFreight_KeyUp(object sender, KeyEventArgs e)
        {
            GetCal();
        }

        public void GetCal()
        {
            txtTotalFreight.Text = txtTotalFreight.Text == "" ? "0" : txtTotalFreight.Text;
            txtAdvPaid.Text = txtAdvPaid.Text == "" ? "0" : txtAdvPaid.Text;
            int iCal = Convert.ToInt32(txtTotalFreight.Text) + Convert.ToInt32(txtAdvPaid.Text);
            txtToPay.Text = iCal.ToString();
        }

        private void txtTotalFreight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtAdvPaid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void cmbDC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDC.SelectedIndex > 0)
            {
                DataSet GetData = null;
                string[] strSval = cmbDC.SelectedValue.ToString().Split('@');
                objStationaryDB = new StationaryDB();
                if (ScreenType == "SELF")
                {
                    GetData = objStationaryDB.Get_ST_DC_Data(CommonData.BranchCode, CommonData.FinancialYear, strSval[2]);
                }
                else
                {
                    GetData = objStationaryDB.Get_ST_DC_Data( CommonData.BranchCode, CommonData.FinancialYear,strSval[2]);
                }
                objStationaryDB = null;
                if (GetData.Tables[0].Rows.Count > 0)
                {

                    //SDN_COMPANY_CODE,SDN_BRANCH_CODE,SDN_STATE_CODE,SDN_FIN_YEAR
                    lblBranchCode.Text = GetData.Tables[0].Rows[0]["SDN_BRANCH_CODE"].ToString();
                    lblCmpCode.Text = GetData.Tables[0].Rows[0]["SDN_COMPANY_CODE"].ToString();
                    txtBranchName.Text = GetData.Tables[0].Rows[0]["BRANCH_NAME"].ToString();
                    //txtCompanyName.Text = GetData.Tables[0].Rows[0]["CM_COMPANY_NAME"].ToString();
                    txtCompanyName.Text = "SHIVASHAKTI GROUP OF COMPANIES";
                    txtTripLRNo.Text = GetData.Tables[0].Rows[0]["SDN_LR_OR_POD_NO"].ToString();
                    if (GetData.Tables[0].Rows[0]["TransportName"] != "")
                    {
                        txtTrnsorCourName.Text = GetData.Tables[0].Rows[0]["TransportName"].ToString();
                    }
                    else
                    {
                        txtTrnsorCourName.Text = GetData.Tables[0].Rows[0]["CourrierName"].ToString();
                    }                   
                    txtBagsCount.Text = GetData.Tables[0].Rows[0]["SDN_BAGS_OR_PACKS_COUNT"].ToString();
                    txtTransportorCourier.Text = GetData.Tables[0].Rows[0]["SDN_TRANSPORT_OR_COURIER"].ToString();
                    txtTotalFreight.Text = GetData.Tables[0].Rows[0]["SDN_TOTAL_FRAIGHT"].ToString();
                    txtAdvPaid.Text = GetData.Tables[0].Rows[0]["SDN_ADV_PAID"].ToString();
                    txtToPay.Text = GetData.Tables[0].Rows[0]["SDN_TO_PAY"].ToString();
                    txtDeliverFlg.Text = GetData.Tables[0].Rows[0]["SDN_DOOR_DELIVERY_FLAG"].ToString();
                    txtDCDate.Text = Convert.ToDateTime(GetData.Tables[0].Rows[0]["SDN_DC_DATE"]).ToString("dd-MM-yyyy");
                    //SDN_REF_INDENT_NO,SDN_CREATED_BY,SDN_CREATED_DATE,SDN_DELIVERY_NOTE_NO,SDN_BAGS_OR_PACKETS,SDN_TO_BRANCH_CODE
                    gvIndentDetails.Rows.Clear();
                    GetBindData(GetData.Tables[1], 1);
                }
            }
        }

        public void GetBindData(DataTable dt, int ival)
        {
            gvIndentDetails.Rows.Clear();
            int intRow = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellItemID = new DataGridViewTextBoxCell();
                cellItemID.Value = dt.Rows[i]["ItemID"].ToString();
                tempRow.Cells.Add(cellItemID);

                DataGridViewCell cellItemName = new DataGridViewTextBoxCell();
                cellItemName.Value = dt.Rows[i]["ItemName"].ToString();
                tempRow.Cells.Add(cellItemName);

                //DataGridViewCell cellAvailQty = new DataGridViewTextBoxCell();
                //if (ival == 1)
                //    cellAvailQty.Value = dt.Rows[i]["DeliveryNo"].ToString();
                //else
                //    cellAvailQty.Value = dt.Rows[i]["ItemQty"].ToString();
                //tempRow.Cells.Add(cellAvailQty);


                //DataGridViewCell cellApprQty = new DataGridViewTextBoxCell();
                //cellApprQty.Value = Convert.ToDouble(dt.Rows[i]["AppQty"].ToString()).ToString("f");
                //tempRow.Cells.Add(cellApprQty);
               

                DataGridViewCell cellexQty = new DataGridViewTextBoxCell();
                cellexQty.Value = dt.Rows[i]["DispQty"].ToString();
                tempRow.Cells.Add(cellexQty);

                DataGridViewCell cellDisQty = new DataGridViewTextBoxCell();
                cellDisQty.Value = dt.Rows[i]["GRNQty"].ToString();
                tempRow.Cells.Add(cellDisQty);

                DataGridViewCell cellExesQty = new DataGridViewTextBoxCell();
                cellExesQty.Value = (Convert.ToDouble(dt.Rows[i]["DispQty"].ToString()) - Convert.ToDouble(dt.Rows[i]["GRNQty"].ToString())).ToString("f");
                tempRow.Cells.Add(cellExesQty);

                DataGridViewCell cellRate = new DataGridViewTextBoxCell();
                cellRate.Value = Convert.ToDouble(dt.Rows[i]["Price"]).ToString("f");
                tempRow.Cells.Add(cellRate);               

                DataGridViewCell cellAmount = new DataGridViewTextBoxCell();
                string iCalval = Convert.ToDouble(Convert.ToDouble(dt.Rows[i]["GRNQty"].ToString()) * Convert.ToDouble(dt.Rows[i]["Price"].ToString())).ToString("f");
                cellAmount.Value = iCalval;
                tempRow.Cells.Add(cellAmount);

                DataGridViewCell cellFrmNo = new DataGridViewTextBoxCell();
                if (ival == 1)
                    cellFrmNo.Value = dt.Rows[i]["frmNo"].ToString();
                else
                    cellFrmNo.Value = "";
                tempRow.Cells.Add(cellFrmNo);

                DataGridViewCell cellToNo = new DataGridViewTextBoxCell();
                if (ival == 1)
                    cellToNo.Value = dt.Rows[i]["ToNo"].ToString();
                else
                    cellToNo.Value = "";
                tempRow.Cells.Add(cellToNo);

                intRow = intRow + 1;
                gvIndentDetails.Rows.Add(tempRow);
                if (ival == 0)
                {
                    gvIndentDetails.Columns[8].Visible = false;
                    gvIndentDetails.Columns[9].Visible = false;
                }
            }
        }

        private void cbGrnType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbGrnType.SelectedIndex == 0)
            {
                lblAdvPaid.Visible = true;
                txtAdvPaid.Visible = true;
                txtDCDate.Visible = true;
                lblDoor.Visible = true;
                txtTotalFreight.Visible = true;
                lblTotFrght.Visible = true;
                lblPaidNow.Visible = true;
                txtToPay.Visible = true;
                lblNoOfBags.Visible = true;
                txtBagsCount.Visible = true;
                btnClearItems.Visible = false;
                btnItemsSearch.Visible = false;
                txtCompanyName.Visible = true;
                txtBranchName.ReadOnly = true;
                txtDeliverFlg.Visible = true;
                txtTransportorCourier.Visible = true;
                txtTrnsorCourName.Visible = true;
                lblCompany.Visible = true;
                txtDCNo.ReadOnly = true;
               
                txtDCNo.Visible = true;
                lblLRNo.Text = "LR/POD Number";
                txtTripLRNo.ReadOnly = true;
                txtDCNo.BackColor = System.Drawing.SystemColors.Info;
                txtTripLRNo.BackColor = System.Drawing.SystemColors.Control;
                cmbDC.Visible = true;
                txtDCNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                lblDc.Visible = true;
                lblDc.Text = "DC";
                //lblDcDate.Text = "DC Date";
                lblCompany.Text = "Company";
                txtAddress.Visible = false;
                lblBranch.Visible = true;
                txtBranchName.Visible = true;
                gvIndentDetails.Columns[3].Visible = true;
                gvIndentDetails.Columns[5].Visible = true;
                gvIndentDetails.Columns[8].Visible = true;
                gvIndentDetails.Columns[9].Visible = true;
                gvIndentDetails.Columns[6].ReadOnly = true;
                gvIndentDetails.Columns[2].Width = 350;
                lblDcDate.Visible = true;
                txtAdvPaid.Text = "";
                txtBagsCount.Text = "";
                txtDCNo.Text = "";
                txtDeliverFlg.Text = "";
                txtGrnAmt.Text = "";
                txtItemsCount.Text = "";
                txtToPay.Text = "";
                txtTotalFreight.Text = "";
                txtTransportorCourier.Text = "";
                txtTripLRNo.Text = "";
                txtTrnsorCourName.Text = "";
                dtGrnDate.Value = DateTime.Now;
                gvIndentDetails.Rows.Clear();
                txtAddress.Text = "";
                txtCompanyName.Text = CommonData.CompanyName;
                txtBranchName.Text = CommonData.BranchName;
            }
            else
            {
                txtAdvPaid.Text = "";
                txtBagsCount.Text = "";
                txtDCNo.Text = "";
                txtDeliverFlg.Text = "";
                txtGrnAmt.Text = "";
                txtItemsCount.Text = "";
                txtToPay.Text = "";
                txtTotalFreight.Text = "";
                txtTransportorCourier.Text = "";
                txtTripLRNo.Text = "";
                txtTrnsorCourName.Text = "";
                dtGrnDate.Value = DateTime.Now;
                gvIndentDetails.Rows.Clear();
                txtAddress.Text = "";
                
                    txtAddress.Visible = true;
                lblBranch.Visible = false;
                txtBranchName.Visible = false;
                lblAdvPaid.Visible = false;
                txtAdvPaid.Visible = false;
                lblDc.Visible = true;
                lblDc.Text = "Supplier";
                lblDoor.Visible = false;
                txtTotalFreight.Visible = false;
                lblTotFrght.Visible = false;
                lblPaidNow.Visible = false;
                txtToPay.Visible = false;
                lblNoOfBags.Visible = false;
                txtBagsCount.Visible = false;
                btnClearItems.Visible = true;
                btnItemsSearch.Visible = true;
                txtCompanyName.Visible = false;                
                txtDeliverFlg.Visible = false;
                txtTransportorCourier.Visible = false;
                txtTrnsorCourName.Visible = false;
                lblCompany.Visible = true;
                lblLRNo.Text = "        Invoice No";              
                lblDcDate.Visible =false;
                txtTripLRNo.ReadOnly = false;
                txtDCNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                txtDCNo.BackColor = Color.White;
                txtTripLRNo.BackColor = Color.White;
                txtDCNo.Visible = true;
                cmbDC.Visible = false;
                txtDCDate.Visible = false;
                txtDCNo.ReadOnly = false;
                lblCompany.Text = "Address";
                gvIndentDetails.Columns[2].Width = 550;
                gvIndentDetails.Columns[3].Visible = false;
                gvIndentDetails.Columns[5].Visible = false;
                gvIndentDetails.Columns[8].Visible = false;
                gvIndentDetails.Columns[9].Visible = false;
                gvIndentDetails.Columns[6].ReadOnly = false;                                                 
            }
        }

        private void btnItemsSearch_Click(object sender, EventArgs e)
        {
            StationaryItemsSearch ItemSearch = new StationaryItemsSearch("ST_PURCHASE_GRN");
            ItemSearch.objStationaryBrGRN = this;
            ItemSearch.ShowDialog();            
        }

        private void btnClearItems_Click(object sender, EventArgs e)
        {
            gvIndentDetails.Rows.Clear();
        }          
       
       
    }
}
