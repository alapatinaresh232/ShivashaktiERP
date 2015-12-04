using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using SSCRMDB;
using SSAdmin;
using SSTrans;

namespace SSCRM
{
    public partial class SPGRNInputBranch : Form
    {
        string sFinYear = string.Empty;
        string sTranNo = "";
        ProductUnitDB objPUDB = null;
        IndentDB objIndent = null;
        SQLDB objSQLDB = null;
        bool IsModify = false;
        public SPGRNInputBranch()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void SPGRNInputBranch_Load(object sender, EventArgs e)
        {
            gvGRNDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                       System.Drawing.FontStyle.Regular);
            txtDocMonth.Text = CommonData.DocMonth;
            sFinYear = CommonData.FinancialYear;
            txtTrnType.Visible = false;
            FillDcDcstList();
            txtGRNNo.Text = NewTransactionNumber().ToString();
           
        }
        private void FillDcDcstList()
        {
            objPUDB = new ProductUnitDB();
            try
            {
                cbDCDCSTNo.DataSource = null;
                
                DataTable dt = objPUDB.DcDCSTList_Get(CommonData.CompanyCode, CommonData.BranchCode,CommonData.FinancialYear,CommonData.DocMonth).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";
                    row[1] = "--Select--";
                    dt.Rows.InsertAt(row,0);
                    cbDCDCSTNo.DataSource = dt;
                    cbDCDCSTNo.ValueMember = "VALUE_MEMBER";
                    cbDCDCSTNo.DisplayMember = "DISPLAY_MEMBER";
                    cbDCDCSTNo.SelectedIndex = 0;
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
                Cursor.Current = Cursors.Default;
            }
        }
        private string NewTransactionNumber()
        {
            string sTranNo = string.Empty;
            objPUDB = new ProductUnitDB();
            try
            {
                sTranNo = objPUDB.GenerateNewSPGRNTranNo(CommonData.CompanyCode, CommonData.BranchCode);
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

        private void txtGRNNo_Enter(object sender, EventArgs e)
        {
            this.txtGRNNo.Select(txtGRNNo.Text.Length, 0);
        }

        private void txtGRNNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 37 || e.KeyValue == 39)
                e.Handled = false;
            else if (txtGRNNo.SelectionStart < 14)
                e.Handled = true;
        }

        private void txtGRNNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == false)
                e.Handled = true;
            if (txtGRNNo.SelectionStart < 17)
                e.Handled = true;
            else if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void txtGRNNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(txtGRNNo.Text).Length > 15 && cbDCDCSTNo.SelectedIndex > 0)
                {
                    string[] strDcDcstNo = cbDCDCSTNo.SelectedValue.ToString().Split('@');
                    FillTransactionData(strDcDcstNo[2], strDcDcstNo[1], strDcDcstNo[3], strDcDcstNo[0], strDcDcstNo[4]);
                    CalculateTotals();
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void cbDCDCSTNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDCDCSTNo.SelectedIndex > 0)
            {
                string[] strDcDcstNo = cbDCDCSTNo.SelectedValue.ToString().Split('@');
                FillTransactionData(strDcDcstNo[2], strDcDcstNo[1], strDcDcstNo[3], strDcDcstNo[0], strDcDcstNo[4]);
                CalculateTotals();
            }
            else
            {
                ClearForm();
                //txtReferenceNo.Text = string.Empty;
            }
        }
        private void FillTransactionData(string strCmpCode,string strBrCode,string strFinYear,string strTranNo, string sFrom)
        {
            objIndent = new IndentDB();
            Hashtable ht = new Hashtable();
            try
            {

                ht = objIndent.GetGRNInputBranchData(strCmpCode, strBrCode, strFinYear, strTranNo, "");
                
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
            try
            {
                if (dtHead.Rows.Count > 0)
                {

                    //IsModify = true;
                   
                    txtDcDate.Text = Convert.ToDateTime( dtHead.Rows[0]["TrnDate"]).ToShortDateString();
                    txtLocation.Text = dtHead.Rows[0]["BranchName"] + "";
                    //cbBranches1.SelectedValue = strFromBranchCode;
                    txtGcName.Text = dtHead.Rows[0]["ename"] + " - (" + dtHead.Rows[0]["ToEcode"]+")";
                    //txtEcodeSearch.Text = dtHead.Rows[0]["FromEcode"] + "";
                    txtVehType.Text = dtHead.Rows[0]["VehicleSource"] + "";
                    txtDriverNo.Text = dtHead.Rows[0]["DriverName"] + "";
                    txtTripLRNo.Text = Convert.ToString(dtHead.Rows[0]["TripLRNumber"]);
                    txtVehicleNo.Text = Convert.ToString(dtHead.Rows[0]["VehicleNumber"]);
                    txtTotalFreight.Text = Convert.ToString(dtHead.Rows[0]["TotalFreight"]);
                    txtAdvancePaid.Text = Convert.ToString(dtHead.Rows[0]["AdvancePaid"]);
                    txtToPay.Text = Convert.ToString(dtHead.Rows[0]["ToPayFreight"]);
                    //txtUnLoadChrg.Text = Convert.ToString(dtHead.Rows[0]["loading_charges"]);
                    txtTransporter.Text = dtHead.Rows[0]["TransporterName"] + "";
                    txtDcRefNo.Text = dtHead.Rows[0]["ReferanceNumber"] + "";
                    txtTrnType.Text = dtHead.Rows[0]["TrnType"] + "";
                    
                    FillTransactionDetl(dtDetl);
                    isData = true;
                    
                }
                else
                {
                   
                    txtDocMonth.Text = CommonData.DocMonth;
                    sFinYear = CommonData.FinancialYear;
                   
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

        private void FillTransactionDetl(DataTable dt)
        {
            objPUDB = new ProductUnitDB();
            try
            {
                gvGRNDetails.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    for (int intRow = 0; intRow < dt.Rows.Count; intRow++)
                    {
                        gvGRNDetails.Rows.Add();
                        gvGRNDetails.Rows[intRow].Cells["SLNO"].Value = intRow + 1;
                        gvGRNDetails.Rows[intRow].Cells["Category"].Value = dt.Rows[intRow]["category_name"].ToString();
                        gvGRNDetails.Rows[intRow].Cells["Product"].Value = dt.Rows[intRow]["product_name"].ToString();
                        gvGRNDetails.Rows[intRow].Cells["ProductId"].Value = dt.Rows[intRow]["ProductId"].ToString();
                        //gvGRNDetails.Rows[intRow].Cells["GoodQty"].Value = dt.Rows[intRow]["IssQty"].ToString();
                        gvGRNDetails.Rows[intRow].Cells["DispQty"].Value = dt.Rows[intRow]["TotalQty"].ToString();
                        gvGRNDetails.Rows[intRow].Cells["TotalQty"].Value = dt.Rows[intRow]["RecevedQty"].ToString();
                        gvGRNDetails.Rows[intRow].Cells["DamageQty"].Value = dt.Rows[intRow]["ExcessOrShortageQty"].ToString();
                        gvGRNDetails.Rows[intRow].Cells["RATE"].Value = dt.Rows[intRow]["IssRate"].ToString();
                        gvGRNDetails.Rows[intRow].Cells["Amount"].Value = dt.Rows[intRow]["IssAmt"].ToString();                       
                    }
                }
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
                    if (gvGRNDetails.Rows[i].Cells["TotalQty"].Value.ToString() != "" && gvGRNDetails.Rows[i].Cells["Rate"].Value.ToString() != "" && gvGRNDetails.Rows[i].Cells["Amount"].Value.ToString() != "")
                    {
                        amt += Convert.ToDouble(gvGRNDetails.Rows[i].Cells["Amount"].Value);
                        totalQty += Convert.ToDouble(gvGRNDetails.Rows[i].Cells["TotalQty"].Value);
                    }
                }

            }
            totalProducts = gvGRNDetails.Rows.Count;
            txtDcAmt.Text = Convert.ToDouble(amt).ToString("f");
            txtProducts.Text = Convert.ToDouble(totalProducts).ToString("f");
            txtQty.Text = Convert.ToDouble(totalQty).ToString("f");
        }

        private void gvGRNDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= gvGRNDetails.Columns["DispQty"].Index && e.ColumnIndex <= gvGRNDetails.Columns["Rate"].Index)
            {
                double dQty = 0, rQty = 0, eQty = 0;
                if (gvGRNDetails.Rows[e.RowIndex].Cells["DispQty"].Value.ToString() != "")
                    dQty = Convert.ToDouble(gvGRNDetails.Rows[e.RowIndex].Cells["DispQty"].Value);
                if (gvGRNDetails.Rows[e.RowIndex].Cells["TotalQty"].Value.ToString() != "")
                    rQty = Convert.ToDouble(gvGRNDetails.Rows[e.RowIndex].Cells["TotalQty"].Value);
                eQty = rQty - dQty;
                gvGRNDetails.Rows[e.RowIndex].Cells["DamageQty"].Value = eQty.ToString("0.0");
                //if (Convert.ToString(gvIndentDetails.Rows[e.RowIndex].Cells["IssQty"].Value) != "")
                //{
                if (rQty >= 0 && Convert.ToDouble(gvGRNDetails.Rows[e.RowIndex].Cells["Rate"].Value) >= 0)
                {
                    gvGRNDetails.Rows[e.RowIndex].Cells["Amount"].Value = rQty * (Convert.ToDouble(gvGRNDetails.Rows[e.RowIndex].Cells["Rate"].Value));
                    gvGRNDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvGRNDetails.Rows[e.RowIndex].Cells["Amount"].Value).ToString("f");

                }
                //}
                //else
                //    gvIndentDetails.Rows[e.RowIndex].Cells["Amount"].Value = string.Empty;
                CalculateTotals();
            }
            try
            {
                //DataGridView dgv = (DataGridView)sender;
                //dgv.EndEdit();
                //if (e.ColumnIndex == gvGRNDetails.Columns["GoodQty"].Index)
                //{
                //    if (Convert.ToString(gvGRNDetails.Rows[e.RowIndex].Cells["GoodQty"].Value) != "" && Convert.ToString(gvGRNDetails.Rows[e.RowIndex].Cells["IndentNo"].Value) != "0")
                //    {
                //        if (Convert.ToDouble(gvGRNDetails.Rows[e.RowIndex].Cells["GoodQty"].Value) > Convert.ToDouble(gvGRNDetails.Rows[e.RowIndex].Cells["IndQty"].Value))
                //        {
                //            //MessageBox.Show("Issue quantity should be less than or equal to indent quantity!", "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //            //e.Cancel = true;
                //        }
                //    }
                //}
            }
            catch
            {
            }
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
                        CalculateTotals();
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

        private int SaveDCHeadData()
        {
            int intSave = 0;
            string strSQL = string.Empty;
            objSQLDB = new SQLDB();
            string strDriver = string.Empty;
            string ecode = string.Empty;
            try
            {               

                strDriver = txtDriverNo.Text.ToString();

                if (txtTotalFreight.Text.ToString().Trim().Length == 0)
                    txtTotalFreight.Text = "0.00";
                if (txtAdvancePaid.Text.ToString().Trim().Length == 0)
                    txtAdvancePaid.Text = "0.00";
                if (txtToPay.Text.ToString().Trim().Length == 0)
                    txtToPay.Text = "0.00";
                if (txtUnLoadChrg.Text.ToString().Trim().Length == 0)
                    txtUnLoadChrg.Text = "0.00";
               
                  ecode = txtGcName.Text.Split('(')[1].Substring(0, txtGcName.Text.Split('(')[1].Length-1);
                if (IsModify == false)
                {
                    string[] strDcDcstNo = cbDCDCSTNo.SelectedValue.ToString().Split('@');                    

                    strSQL = " INSERT INTO SP_GRN_HEAD " +
                         "(SPGH_COMPANY_CODE" +
                         ", SPGH_STATE_CODE" +
                         ", SPGH_BRANCH_CODE" +
                         ", SPGH_FIN_YEAR" +
                         ", SPGH_DOCUMENT_MONTH" +
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
                         ", SPGH_CREATED_DATE" +
                         ",SPGH_AGNST_DCDCST_NO" +
                         ",SPGH_GRN_TYPE) " +

                         "VALUES " +
                         "('" + CommonData.CompanyCode +
                         "', '" + CommonData.StateCode +
                         "', '" + CommonData.BranchCode +
                         "', '" + CommonData.FinancialYear +
                         "', '" + CommonData.DocMonth +
                         "', '" + txtGRNNo.Text +
                         "', '" + txtReferenceNo.Text +
                         "', '" + Convert.ToDateTime(dtpGRNdate.Value.ToString()).ToString("dd/MMM/yyy") +
                         "', '" + strDcDcstNo[1] +
                         "', '" + ecode +
                         "', '" + txtTripLRNo.Text +
                         "', '" + txtVehType.Text +
                         "', '" + txtVehicleNo.Text.Replace(" ", "") +
                         "', '" + txtTransporter.Text +
                         "', '" + txtDriverNo.Text +
                         "', '" + txtTotalFreight.Text +
                         "', '" + txtAdvancePaid.Text +
                         "', '" + txtToPay.Text +
                         "', '" + txtUnLoadChrg.Text +
                         "', '" + CommonData.LogUserId +
                         "', '" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyy") +
                         "','" + strDcDcstNo[0] +
                         "','" + txtTrnType.Text + "')";
                }
                else
                {
                    strSQL = " DELETE from SP_GRN_DETL" +
                                " WHERE SPGD_company_code='" + CommonData.CompanyCode +
                                    "' AND SPGD_BRANCH_CODE='" + CommonData.BranchCode +
                                    "' AND SPGD_GRN_NUMBER='" + txtGRNNo.Text +
                                    "' AND SPGD_FIN_YEAR='" + CommonData.FinancialYear + "'";

                    int intRec = objSQLDB.ExecuteSaveData(strSQL);

                    strSQL = " UPDATE SP_GRN_HEAD " +
                                "SET SPGH_TO_PAY_FREIGHT='" + txtToPay.Text +
                                "', SPGH_UNLOADING_CHARGES='" + txtUnLoadChrg.Text +
                                "', SPGH_GRN_DATE='"+ Convert.ToDateTime(dtpGRNdate.Value.ToString()).ToString("dd/MMM/yyy") +
                                "', SPGH_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                                "', SPGH_LAST_MODIFIED_DATE='" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyy") +
                             "' WHERE SPGH_GRN_NUMBER = '" + txtGRNNo.Text +
                             "'  AND SPGH_BRANCH_CODE='" + CommonData.BranchCode +
                             "' AND SPGH_FIN_YEAR='" + CommonData.FinancialYear +
                             "' AND SPGH_COMPANY_CODE='" + CommonData.CompanyCode + "'";

                    
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
            string sDcDcstNo = string.Empty;
            objSQLDB = new SQLDB();
            try
            {


                strSQL = "DELETE from SP_GRN_DETL" +
                           " WHERE SPGD_COMPANY_CODE='" + CommonData.CompanyCode +
                               "' AND SPGD_BRANCH_CODE='" + CommonData.BranchCode +
                               "' AND SPGD_GRN_NUMBER='" + txtGRNNo.Text +
                               "' AND SPGD_FIN_YEAR='" + CommonData.FinancialYear + "'";

                intSave = objSQLDB.ExecuteSaveData(strSQL);

                if (IsModify == true)
                {
                    sDcDcstNo = txtDCDCSTNo.Text;
                }
                else
                {
                    string[] strDcDcstNo = cbDCDCSTNo.SelectedValue.ToString().Split('@');
                    sDcDcstNo = strDcDcstNo[0];
                }


                for (int i = 0; i < gvGRNDetails.Rows.Count; i++)
                {
                    if (gvGRNDetails.Rows[i].Cells["TotalQty"].Value.ToString() != "" && gvGRNDetails.Rows[i].Cells["TotalQty"].Value.ToString() != "0" && gvGRNDetails.Rows[i].Cells["Amount"].Value.ToString() != "")
                    {
                        sbSQL.Append(" INSERT INTO SP_GRN_DETL (SPGD_COMPANY_CODE" +
                                    ", SPGD_STATE_CODE, SPGD_BRANCH_CODE, SPGD_FIN_YEAR" +
                                    ", SPGD_DOCUMENT_MONTH, SPGD_GRN_NUMBER, SPGD_SL_NO" +
                                    ", SPGD_PRODUCT_ID, SPGD_REC_RATE" +
                                    ", SPGD_REC_QTY, SPGD_DAMAGE_QTY, SPGD_REC_AMT" +
                                    ", SPGD_GRN_TYPE, SPGD_AGNST_TRN_NUMBER)" +
                                    " VALUES ('" + CommonData.CompanyCode + "', '" + CommonData.StateCode + "', '" + CommonData.BranchCode + "', '" + CommonData.FinancialYear + "', '" + txtDocMonth.Text +
                                    "', '" + txtGRNNo.Text +
                                    "', " + gvGRNDetails.Rows[i].Cells["SLNO"].Value +
                                    ", '" + gvGRNDetails.Rows[i].Cells["ProductID"].Value +
                                    "', '" + gvGRNDetails.Rows[i].Cells["Rate"].Value +
                                    "', '" + gvGRNDetails.Rows[i].Cells["TotalQty"].Value +
                                    "', '0" +
                                    "', '" + gvGRNDetails.Rows[i].Cells["Amount"].Value +
                                    "','" + txtTrnType.Text + "','" + sDcDcstNo + "'); ");

                    }
                }
                cbDCDCSTNo.Visible = true;
                if (cbDCDCSTNo.Items.Count != 0)
                {
                    cbDCDCSTNo.SelectedIndex = 0;
                }
                txtDCDCSTNo.Visible = false;
                txtDCDCSTNo.Text = "";
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



        private void btnSave_Click(object sender, EventArgs e)
        {
            int intSave = 0;
            
            try
            {
                if (CheckData())
                {
                   // string[] strDcDcstNo = cbDCDCSTNo.SelectedValue.ToString().Split('@');
                    if (SaveDCHeadData() > 0)
                        intSave = SaveDCDetlData();
                    else
                    {
                        string strSQL = "DELETE from SP_GRN_HEAD" +
                                        "' WHERE SPGH_GRN_NUMBER = '" + txtGRNNo.Text +
                                       "'  AND SPGH_BRANCH_CODE='" + CommonData.BranchCode +
                                      "' AND SPGH_FIN_YEAR='" + CommonData.FinancialYear +
                                    "' AND SPGH_COMPANY_CODE='" + CommonData.CompanyCode + "'";
                        objSQLDB = new SQLDB();
                        int intDel = objSQLDB.ExecuteSaveData(strSQL);
                        objSQLDB = null;
                        ClearForm();
                    }

                    if (intSave > 0)
                    {
                        IsModify = false;
                        MessageBox.Show("GRN data saved.\nTran No: " + txtGRNNo.Text, "SPGRNInputBranch", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                        txtReferenceNo.Text = string.Empty;
                        txtGRNNo.Text = NewTransactionNumber().ToString();
                        FillDcDcstList();
                        //for (int i = 0; i < gvGRNDetails.Rows.Count; i++)
                        //{
                        //    gvGRNDetails.Rows[i].Cells["GoodQty"].Value = "0";
                        //    //gvIndentDetails.Rows[i].Cells["IssQty"].Value = "";
                        //}
                        gvGRNDetails.Rows.Clear();

                    }
                    else
                    {
                        MessageBox.Show("DC data not saved.", "SPGRNInputBranch", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {

            }
        }
        private bool CheckData()
        {
            bool blValue = true;
            if (Convert.ToString(txtReferenceNo.Text).Length == 0)
            {
                MessageBox.Show("Enter Reference No", "Grn SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtReferenceNo.Focus();
                return blValue;
            }
            if(cbDCDCSTNo.SelectedIndex == 0 && IsModify == false)
            {
                MessageBox.Show("Select DC/DCST", "Grn SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                return blValue;
            }
            return blValue;
        }

        private void ClearForm()
        {
            //txtReferenceNo.Text = string.Empty;
            txtLocation.Text = string.Empty;
            txtToPay.Text = string.Empty;
            txtTotalFreight.Text = string.Empty;
            txtTransporter.Text = string.Empty;
            txtTripLRNo.Text = string.Empty;
            txtUnLoadChrg.Text = string.Empty;
            txtVehicleNo.Text = string.Empty;
            txtVehType.Text = string.Empty;
            txtAdvancePaid.Text = string.Empty;
            txtVehicleNo.Text = string.Empty;
            txtDriverNo.Text = string.Empty;
            txtDcRefNo.Text = string.Empty;
            cbDCDCSTNo.Visible = true;
            if (cbDCDCSTNo.Items.Count > 0)
            {
                cbDCDCSTNo.SelectedIndex = 0;
            }
            
            txtDcDate.Text = string.Empty;
            dtpGRNdate.Value = DateTime.Now;
            txtGcName.Text = string.Empty;
            txtProducts.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtDcAmt.Text = string.Empty;
            gvGRNDetails.Rows.Clear();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbDCDCSTNo.Visible = true;
            txtDCDCSTNo.Visible = false;
            txtDCDCSTNo.Text = "";
            cbDCDCSTNo.SelectedIndex = 0;
            txtGRNNo.Text = NewTransactionNumber().ToString();
            gvGRNDetails.Rows.Clear();
            ClearForm();
            txtReferenceNo.Text = string.Empty;
            IsModify = false;
        }

        private void txtReferenceNo_Validated(object sender, EventArgs e)
        {
            if (txtReferenceNo.Text.Length > 0)//&& txtLocation.Text.Length == 0
            {
                objSQLDB = new SQLDB();
                DataTable dtH = new DataTable();
                DataTable dtT = new DataTable();
                try
                {
                    string strCommand = "SELECT SPGH_GRN_NUMBER,SPGH_AGNST_DCDCST_NO,SPDH_TRN_DATE TrnDate,SPDH_REFERENCE_NUMBER ReferanceNumber,SPGH_GRN_DATE" +
                                    ", BRANCH_NAME BranchName,SPGH_FROM_eCODE ToEcode, (SELECT MEMBER_NAME FROM EORA_MASTER WHERE ECODE=SPGH_FROM_eCODE) ename" +
                                    ",SPGH_VEHICLE_SOURCE VehicleSource,SPGH_DRIVER_NAME DriverName,SPGH_TRIP_OR_LR_NUMBER TripLRNumber" +
                                    ",SPGH_VEHICLE_NUMBER VehicleNumber,SPGH_TOTAL_FREIGHT TotalFreight,SPGH_ADVANCE_PAID AdvancePaid,SPGH_GRN_TYPE TrnType" +
                                    ",SPGH_TO_PAY_FREIGHT ToPayFreight,SPGH_UNLOADING_CHARGES,SPGH_TO_PAY_FREIGHT,SPGH_TRANSPORTER_NAME TransporterName " +
                                    "FROM SP_GRN_HEAD INNER JOIN BRANCH_MAS ON BRANCH_CODE=SPGH_FROM_BRANCH_CODE INNER JOIN SP_DC_HEAD " +
                                    "ON SPDH_TRN_NUMBER=SPGH_AGNST_DCDCST_NO WHERE SPGH_REFERENCE_NUMBER='" + txtReferenceNo.Text +
                                    "' AND SPGH_COMPANY_CODE='" + CommonData.CompanyCode + "' AND SPGH_BRANCH_CODE='" + CommonData.BranchCode +
                                    "' AND SPGH_FIN_YEAR='" + CommonData.FinancialYear + 
                                    "' union  "+
                                    "  SELECT SPGH_GRN_NUMBER,SPGH_AGNST_DCDCST_NO,PUFDH_TRN_DATE TrnDate,PUFDH_REF_NO ReferanceNumber,SPGH_GRN_DATE" +
                                    ", BRANCH_NAME BranchName,SPGH_FROM_eCODE ToEcode, (SELECT MEMBER_NAME FROM EORA_MASTER WHERE ECODE=SPGH_FROM_eCODE) ename" +
                                    ",SPGH_VEHICLE_SOURCE VehicleSource,SPGH_DRIVER_NAME DriverName,SPGH_TRIP_OR_LR_NUMBER TripLRNumber" +
                                    ",SPGH_VEHICLE_NUMBER VehicleNumber,SPGH_TOTAL_FREIGHT TotalFreight,SPGH_ADVANCE_PAID AdvancePaid,SPGH_GRN_TYPE TrnType" +
                                    ",SPGH_TO_PAY_FREIGHT ToPayFreight,SPGH_UNLOADING_CHARGES,SPGH_TO_PAY_FREIGHT,SPGH_TRANSPORTER_NAME TransporterName " +
                                    "FROM SP_GRN_HEAD INNER JOIN BRANCH_MAS ON BRANCH_CODE=SPGH_FROM_BRANCH_CODE " +
                                    "INNER JOIN PU_FG_DC_HEAD ON PUFDH_TRN_NUMBER=SPGH_AGNST_DCDCST_NO WHERE SPGH_REFERENCE_NUMBER='" + txtReferenceNo.Text +
                                    "' AND SPGH_COMPANY_CODE='" + CommonData.CompanyCode + "' AND SPGH_BRANCH_CODE='" + CommonData.BranchCode +
                                    "' AND SPGH_FIN_YEAR='" + CommonData.FinancialYear + "' ";
                    dtH = objSQLDB.ExecuteDataSet(strCommand).Tables[0];
                    if (dtH.Rows.Count > 0)
                    {
                        IsModify = true;
                        txtUnLoadChrg.Text = dtH.Rows[0]["SPGH_UNLOADING_CHARGES"].ToString();
                        string strDCDCSTNo = dtH.Rows[0]["SPGH_AGNST_DCDCST_NO"].ToString();
                        txtGRNNo.Text = dtH.Rows[0]["SPGH_GRN_NUMBER"].ToString();
                        dtpGRNdate.Value = Convert.ToDateTime(dtH.Rows[0]["SPGH_GRN_DATE"]);
                        //cbDCDCSTNo.SelectedItem = strDCDCSTNo;
                        txtDCDCSTNo.Visible = true;
                        txtDCDCSTNo.Text = strDCDCSTNo;
                        cbDCDCSTNo.Visible = false;
                        string sCommand = "SELECT  SPGD_PRODUCT_ID ProductId" +
                                            ",CATEGORY_NAME category_name" +
                                            ",PM_PRODUCT_NAME product_name" +
                                            ",SPGD_REC_QTY IssQty" +
                                            ",SPGD_DAMAGE_QTY DmgQty" +
                                            ",SPDD_ISS_QTY+SPDD_ISS_DAMG_QTY TotalQty" +
                                            ",SPGD_REC_QTY+SPGD_DAMAGE_QTY RecevedQty" +
                                            ",(SPDD_ISS_QTY+SPDD_ISS_DAMG_QTY)-(SPGD_REC_QTY+SPGD_DAMAGE_QTY) ExcessOrShortageQty" +
                                            ",SPGD_REC_RATE IssRate" +
                                            ",SPGD_REC_AMT IssAmt " +
                                            "FROM SP_GRN_DETL " +
                                            "INNER JOIN PRODUCT_MAS PM ON PM.PM_PRODUCT_ID=SPGD_PRODUCT_ID " +
                                            "INNER JOIN CATEGORY_MASTER ON CATEGORY_ID=PM.PM_CATEGORY_ID " +
                                            "INNER JOIN SP_GRN_HEAD ON SPGH_BRANCH_CODE = SPGD_BRANCH_CODE "+
                                            "AND SPGH_FIN_YEAR = SPGD_FIN_YEAR AND SPGH_GRN_NUMBER = SPGD_GRN_NUMBER "+
                                            "INNER JOIN SP_DC_HEAD ON SPDH_TO_BRANCH_CODE = SPGH_BRANCH_CODE "+
                                            "AND SPDH_TRN_NUMBER = SPGH_AGNST_DCDCST_NO "+
                                            "INNER JOIN SP_DC_DETL ON SPDD_BRANCH_CODE = SPDH_BRANCH_CODE "+
                                            "AND SPDD_FIN_YEAR = SPDH_FIN_YEAR AND SPDD_TRN_NUMBER = SPDH_TRN_NUMBER "+
                                            "AND SPDD_PRODUCT_ID = SPGD_PRODUCT_ID "+
                                            "WHERE SPGD_GRN_NUMBER='" + dtH.Rows[0]["SPGH_GRN_NUMBER"] + "' " +
                                            "AND SPGD_COMPANY_CODE='" + CommonData.CompanyCode + "' " +
                                            "AND SPGD_BRANCH_CODE='" + CommonData.BranchCode + "' " +
                                            "AND SPGD_FIN_YEAR='" + CommonData.FinancialYear + 
                                            "' union "+
                                            " SELECT  SPGD_PRODUCT_ID ProductId" +
                                            ",CATEGORY_NAME category_name" +
                                            ",PM_PRODUCT_NAME product_name" +
                                            ",SPGD_REC_QTY IssQty" +
                                            ",SPGD_DAMAGE_QTY DmgQty" +
                                            ",PUFDD_ISS_QTY+PUFDD_BAD_QTY TotalQty" +
                                            ",SPGD_REC_QTY+SPGD_DAMAGE_QTY RecevedQty" +
                                            ",(PUFDD_ISS_QTY+PUFDD_BAD_QTY)-(SPGD_REC_QTY+SPGD_DAMAGE_QTY) ExcessOrShortageQty" +
                                            ",SPGD_REC_RATE IssRate" +
                                            ",SPGD_REC_AMT IssAmt " +
                                            "FROM SP_GRN_DETL " +
                                            "INNER JOIN PRODUCT_MAS PM ON PM.PM_PRODUCT_ID=SPGD_PRODUCT_ID " +
                                            "INNER JOIN CATEGORY_MASTER ON CATEGORY_ID=PM.PM_CATEGORY_ID " +
                                            "INNER JOIN SP_GRN_HEAD ON SPGH_BRANCH_CODE = SPGD_BRANCH_CODE "+
                                            "AND SPGH_FIN_YEAR = SPGD_FIN_YEAR AND SPGH_GRN_NUMBER = SPGD_GRN_NUMBER "+
                                            "INNER JOIN PU_FG_DC_HEAD ON PUFDH_TO_BRANCH_CODE = SPGH_BRANCH_CODE "+
                                            "AND PUFDH_TRN_NUMBER = SPGH_AGNST_DCDCST_NO "+
                                            "INNER JOIN PU_FG_DC_DETL ON PUFDD_BRANCH_CODE = PUFDH_BRANCH_CODE "+
                                            "AND PUFDD_FIN_YEAR = PUFDH_FIN_YEAR AND PUFDD_TRN_NUMBER = PUFDH_TRN_NUMBER "+
                                            "AND PUFDD_PRODUCT_ID = SPGD_PRODUCT_ID "+
                                            "WHERE SPGD_GRN_NUMBER='" + dtH.Rows[0]["SPGH_GRN_NUMBER"] + "' " +
                                            "AND SPGD_COMPANY_CODE='" + CommonData.CompanyCode + "' " +
                                            "AND SPGD_BRANCH_CODE='" + CommonData.BranchCode + "' " +
                                            "AND SPGD_FIN_YEAR='" + CommonData.FinancialYear + "' ";
                        dtT = objSQLDB.ExecuteDataSet(sCommand).Tables[0];
                        if (dtT.Rows.Count > 0)
                        {
                            FillTransactionHead(dtH, dtT);
                            CalculateTotals();
                        }

                    }
                    else
                    {
                        cbDCDCSTNo.Visible = true;
                        txtDCDCSTNo.Visible = false;
                        txtDCDCSTNo.Text = "";
                        //cbDCDCSTNo.SelectedIndex = 0;
                        txtGRNNo.Text = NewTransactionNumber().ToString();
                        //gvGRNDetails.Rows.Clear();
                        //ClearForm();
                        IsModify = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    dtH = null;
                    dtT = null;
                    objSQLDB = null;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtGRNNo.Text.Length > 0 && IsModify == true)
                {
                    //Security objSecur = new Security();
                    //if (objSecur.CanModifyDataUserAsPerBackDays(Convert.ToDateTime(dtpGRNdate.Value)) == false)
                    //{
                    //    MessageBox.Show("You cannot manipulate backdays data!\n If you want to modify, Contact to IT-Department", "GRN SP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtGRNNo.Focus();
                    //}
                    //else
                    //{
                        DialogResult result = MessageBox.Show("Do you want to Delete " + txtGRNNo.Text + " Transaction ?",
                                               "CRM DC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            objSQLDB = new SQLDB();

                            string strDelete = " DELETE from SP_GRN_DETL " +
                                                " WHERE SPGD_COMPANY_CODE='" + CommonData.CompanyCode +
                                                "' AND SPGD_BRANCH_CODE='" + CommonData.BranchCode +
                                                "' AND SPGD_GRN_NUMBER='" + txtGRNNo.Text +
                                                "' AND SPGD_FIN_YEAR='" + CommonData.FinancialYear + "'";

                            strDelete += "DELETE from SP_GRN_HEAD" +
                                                " WHERE SPGH_COMPANY_CODE='" + CommonData.CompanyCode +
                                                "' AND SPGH_BRANCH_CODE='" + CommonData.BranchCode +
                                                "' AND SPGH_GRN_NUMBER='" + txtGRNNo.Text +
                                                "' AND SPGH_FIN_YEAR='" + CommonData.FinancialYear + "' ";

                            int intRec = objSQLDB.ExecuteSaveData(strDelete);
                            if (intRec > 0)
                            {
                                IsModify = false;
                                MessageBox.Show("Tran No: " + txtGRNNo.Text + " Deleted!");
                                cbDCDCSTNo.Visible = true;
                                txtDCDCSTNo.Visible = false;
                                txtDCDCSTNo.Text = "";
                                cbDCDCSTNo.SelectedIndex = 0;
                                txtGRNNo.Text = NewTransactionNumber().ToString();
                                gvGRNDetails.Rows.Clear();
                                ClearForm();
                                txtReferenceNo.Text = string.Empty;
                            }

                        }
                    //}
                }
                else
                {
                    MessageBox.Show("Enter GRN Number.", "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtGRNNo.Focus();
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

        private void txtGRNNo_Validated(object sender, EventArgs e)
        {
            
            if (Convert.ToString(txtGRNNo.Text).Length > 15)
            {
                objSQLDB = new SQLDB();
                DataTable dtH = new DataTable();
                DataTable dtT = new DataTable();
                try
                {
                    string strCommand = "SELECT SPGH_GRN_NUMBER,SPGH_REFERENCE_NUMBER,SPGH_AGNST_DCDCST_NO,SPDH_TRN_DATE TrnDate,SPGH_GRN_DATE"+
                                        ",SPDH_REFERENCE_NUMBER ReferanceNumber, BRANCH_NAME BranchName,SPGH_FROM_eCODE ToEcode"+
                                        ", (SELECT MEMBER_NAME FROM EORA_MASTER WHERE ECODE=SPGH_FROM_eCODE) ename"+
                                        ",SPGH_VEHICLE_SOURCE VehicleSource,SPGH_DRIVER_NAME DriverName,SPGH_TRIP_OR_LR_NUMBER TripLRNumber,"+
                                        "SPGH_VEHICLE_NUMBER VehicleNumber,SPGH_TOTAL_FREIGHT TotalFreight,SPGH_ADVANCE_PAID AdvancePaid,SPGH_GRN_TYPE TrnType," +
                                        "SPGH_TO_PAY_FREIGHT ToPayFreight,SPGH_UNLOADING_CHARGES,SPGH_TO_PAY_FREIGHT,SPGH_TRANSPORTER_NAME TransporterName "+
                                        "FROM SP_GRN_HEAD INNER JOIN BRANCH_MAS ON BRANCH_CODE=SPGH_FROM_BRANCH_CODE INNER JOIN SP_DC_HEAD ON SPDH_TRN_NUMBER=SPGH_AGNST_DCDCST_NO "+
                                        "WHERE SPGH_GRN_NUMBER='" + txtGRNNo.Text + "' AND SPGH_COMPANY_CODE ='" + CommonData.CompanyCode + 
                                        "' AND SPGH_BRANCH_CODE ='" + CommonData.BranchCode + "' AND SPGH_FIN_YEAR ='" + CommonData.FinancialYear + "'";
                    dtH = objSQLDB.ExecuteDataSet(strCommand).Tables[0];
                    if (dtH.Rows.Count > 0)
                    {
                        IsModify = true;
                        txtUnLoadChrg.Text = dtH.Rows[0]["SPGH_UNLOADING_CHARGES"].ToString();
                        string strDCDCSTNo = dtH.Rows[0]["SPGH_AGNST_DCDCST_NO"].ToString();
                        
                        txtReferenceNo.Text = dtH.Rows[0]["SPGH_REFERENCE_NUMBER"].ToString();
                        dtpGRNdate.Value = Convert.ToDateTime(dtH.Rows[0]["SPGH_GRN_DATE"]);
                        //cbDCDCSTNo.SelectedItem = strDCDCSTNo;
                        txtDCDCSTNo.Visible = true;
                        txtDCDCSTNo.Text = strDCDCSTNo;
                        cbDCDCSTNo.Visible = false;
                        string sCommand = "SELECT SPGD_PRODUCT_ID ProductId" +
                                            ",CATEGORY_NAME category_name" +
                                            ",PM_PRODUCT_NAME product_name" +
                                            ",SPGD_REC_QTY IssQty" +
                                            ",SPGD_DAMAGE_QTY DmgQty" +
                                            ",SPDD_ISS_QTY+SPDD_ISS_DAMG_QTY TotalQty" +
                                            ",SPGD_REC_QTY+SPGD_DAMAGE_QTY RecevedQty" +
                                            ",(SPDD_ISS_QTY+SPDD_ISS_DAMG_QTY)-(SPGD_REC_QTY+SPGD_DAMAGE_QTY) ExcessOrShortageQty" +
                                            ",SPGD_REC_RATE IssRate" +
                                            ",SPGD_REC_AMT IssAmt " +
                                            "FROM SP_GRN_DETL " +
                                            "INNER JOIN PRODUCT_MAS PM ON PM.PM_PRODUCT_ID=SPGD_PRODUCT_ID " +
                                            "INNER JOIN CATEGORY_MASTER ON CATEGORY_ID=PM.PM_CATEGORY_ID " +
                                            "INNER JOIN SP_GRN_HEAD ON SPGH_BRANCH_CODE = SPGD_BRANCH_CODE " +
                                            "AND SPGH_GRN_NUMBER = SPGD_GRN_NUMBER AND SPGH_FIN_YEAR = SPGD_FIN_YEAR " +
                                            "INNER JOIN SP_DC_HEAD ON SPDH_BRANCH_CODE = SPGH_FROM_BRANCH_CODE " +
                                            "AND SPDH_TRN_NUMBER = SPGH_AGNST_DCDCST_NO " +
                                            "INNER JOIN SP_DC_DETL ON SPDD_BRANCH_CODE = SPDH_BRANCH_CODE " +
                                            "AND SPDD_FIN_YEAR = SPDH_FIN_YEAR AND SPDD_TRN_NUMBER = SPDH_TRN_NUMBER " +
                                            "AND SPDD_PRODUCT_ID = SPGD_PRODUCT_ID " +
                                            "WHERE SPGD_GRN_NUMBER='" + dtH.Rows[0]["SPGH_GRN_NUMBER"] + "' " +
                                            "AND SPGD_COMPANY_CODE='" + CommonData.CompanyCode + "' " +
                                            "AND SPGD_BRANCH_CODE='" + CommonData.BranchCode + "' " +
                                            "AND SPGD_FIN_YEAR='" + CommonData.FinancialYear + "'";
                        
                        dtT = objSQLDB.ExecuteDataSet(sCommand).Tables[0];
                        if (dtT.Rows.Count > 0)
                        {
                            FillTransactionHead(dtH, dtT);
                            CalculateTotals();
                        }

                    }
                    else
                    {
                        cbDCDCSTNo.Visible = true;
                        txtDCDCSTNo.Visible = false;
                        txtDCDCSTNo.Text = "";
                        cbDCDCSTNo.SelectedIndex = 0;
                        txtGRNNo.Text = NewTransactionNumber().ToString();
                        gvGRNDetails.Rows.Clear();
                        ClearForm();
                        txtReferenceNo.Text = string.Empty;
                        IsModify = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    dtH = null;
                    dtT = null;
                    objSQLDB = null;
                }
                

            }
      
        }

        private void txtReferenceNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtUnLoadChrg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtToPay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtDcRefNo_KeyUp(object sender, KeyEventArgs e)
        {
            //if (txtDCDCSTNo.Text.Replace(" ", "").Length > 0)
            //{
            //    objPUDB = new ProductUnitDB();
            //    try
            //    {
            //        cbDCDCSTNo.DataSource = null;

            //        DataTable dt = objPUDB.DcDCSTListByRefDCDCSTNo_Get(CommonData.CompanyCode, CommonData.BranchCode, CommonData.FinancialYear, CommonData.DocMonth,txtDCDCSTNo.Text).Tables[0];
            //        if (dt.Rows.Count > 0)
            //        {
            //            DataRow row = dt.NewRow();
            //            row[0] = "--Select--";
            //            row[1] = "--Select--";
            //            dt.Rows.InsertAt(row, 0);
            //            cbDCDCSTNo.DataSource = dt;
            //            cbDCDCSTNo.ValueMember = "VALUE_MEMBER";
            //            cbDCDCSTNo.DisplayMember = "DISPLAY_MEMBER";
            //            cbDCDCSTNo.SelectedIndex = 0;
            //        }
            //        dt = null;
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "SP GRN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    }
            //    finally
            //    {

            //        objPUDB = null;
            //        Cursor.Current = Cursors.Default;
            //    }
            //}
            //else
            //{
            //    FillDcDcstList();
            //}
        }

      
    }
}
