using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSAdmin;
using SSTrans;
using System.Data.SqlClient;
using System.Collections;
using System.Net.Mail;
using System.Net;
using System.Xml;
using System.IO;
using System.Threading;

namespace SDMS
{
    public partial class DealerInvoice : Form
    {
        IndentDB objIndent = null;
        UtilityDB objUtilData = null;
        SQLDB objSQLDB = null;
        General objGeneral = null;
        bool IsModify = false, flagText=false;
        string strFormType="";
        string sFinYear = "";
        private string strECode = string.Empty;
        private string strDCode = string.Empty;
        private string strDLMobileNo = "";
        public DealerInvoice(string sFmType)
        {
            InitializeComponent();
            strFormType = sFmType;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnClearProd_Click(object sender, EventArgs e)
        {
            gvProductDetails.Rows.Clear();
        }

        private void DeliveryChallan_Load(object sender, EventArgs e)
        {
            sFinYear = CommonData.FinancialYear;
            txtVatPers.Text = "5";
            
            FillVehcleType();
            cbVehicleType.SelectedIndex = 0;
            cbRefType.SelectedIndex = 0;
            cbDocType.SelectedIndex = 0;
            gvProductDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                      System.Drawing.FontStyle.Regular);
            meTransactionDate.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyyy"));
            
            FillDriverEmployeeData();
            txtTransactionNo.Text = NewTransactionNumber().ToString();
            CalculateTotals();
        }
        private string NewTransactionNumber()
        {
            string sTranNo = string.Empty;
            objIndent = new IndentDB();
            try
            {
                sTranNo = objIndent.GenerateNewDLDCTranNo(CommonData.CompanyCode, CommonData.BranchCode, strFormType);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delivery challan", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objIndent = null;
            }

            return sTranNo;
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
                MessageBox.Show(ex.Message, "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {

                objUtilData = null;
                Cursor.Current = Cursors.Default;
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
        private void CalculateTotals()
        {
            double netAmt = 0;
            double amt = 0;
            double dicAmt = 0, disPer=0;
            double vatAmt = 0, vatPer = 0;
            double totalProducts = 0;
            double totalQty = 0;
            int count = 0;
            if (gvProductDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {
                    if (gvProductDetails.Rows[i].Cells["IssQty"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["Rate"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["Amount"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["NetAmt"].Value != null)
                    {
                        netAmt += Convert.ToDouble(gvProductDetails.Rows[i].Cells["NetAmt"].Value);

                        totalQty += Convert.ToDouble(gvProductDetails.Rows[i].Cells["IssQty"].Value);
                    }
                    if (gvProductDetails.Rows[i].Cells["IssQty"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["Rate"].Value.ToString() != "")
                    {
                        amt += Convert.ToDouble(gvProductDetails.Rows[i].Cells["Amount"].Value);
                    }
                    if (gvProductDetails.Rows[i].Cells["IssQty"].Value.ToString() != "" && Convert.ToDouble(gvProductDetails.Rows[i].Cells["IssQty"].Value.ToString()) > 0 && gvProductDetails.Rows[i].Cells["Rate"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["Amount"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["Discper"].Value.ToString() != "")
                    {
                        disPer += Convert.ToDouble(gvProductDetails.Rows[i].Cells["Discper"].Value);
                    }
                    if (gvProductDetails.Rows[i].Cells["IssQty"].Value.ToString() != "" && Convert.ToDouble(gvProductDetails.Rows[i].Cells["IssQty"].Value.ToString()) > 0 && gvProductDetails.Rows[i].Cells["Rate"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["Amount"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["Vatper"].Value.ToString() != "")
                    {
                        vatPer += Convert.ToDouble(gvProductDetails.Rows[i].Cells["Vatper"].Value);
                    }
                    if (gvProductDetails.Rows[i].Cells["IssQty"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["Rate"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["NetAmt"].Value != null && gvProductDetails.Rows[i].Cells["Discper"].Value.ToString() != "")
                    {
                        dicAmt += Convert.ToDouble(gvProductDetails.Rows[i].Cells["DiscAmount"].Value);
                    }
                    if (gvProductDetails.Rows[i].Cells["IssQty"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["Rate"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["NetAmt"].Value != null && gvProductDetails.Rows[i].Cells["Vatper"].Value.ToString() != "")
                    {
                        vatAmt += Convert.ToDouble(gvProductDetails.Rows[i].Cells["VatAmount"].Value);
                    }
                    if (Convert.ToDouble(gvProductDetails.Rows[i].Cells["IssQty"].Value.ToString()) > 0 )
                    {
                        count++;
                    }
                }
                disPer /= count;
                vatPer /= count;
            }
            
            totalProducts = gvProductDetails.Rows.Count;
           
            txtAmt.Text = Convert.ToDouble(amt).ToString("f");
            txtDcAmt.Text = Convert.ToDouble(netAmt).ToString("f");
            txtProducts.Text = Convert.ToDouble(count).ToString("f");
            txtDiscPers.Text = Convert.ToDouble(disPer).ToString("f");
            txtDiscAmt.Text = Convert.ToDouble(dicAmt).ToString("f");
            txtVatPers.Text = Convert.ToDouble(vatPer).ToString("f");
            txtVatAmt.Text = Convert.ToDouble(vatAmt).ToString("f");
            txtQty.Text = Convert.ToDouble(totalQty).ToString("f");
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
            IsModify = false;
            meTransactionDate.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyyy"));
            gvProductDetails.Rows.Clear();
            txtTransactionNo.Text = NewTransactionNumber().ToString();
        }
        private void ClearForm()
        {
            IsModify = false;
            txtVatPers.Text = "5";
            txtReferenceNo.Text = "";
            txtTotalFreight.Text = "";
            txtToPay.Text = "";
            txtAdvancePaid.Text = "";
            txtLoadingCharges.Text = "";
            txtBuyOrdNo.Text = "";
            txtDealerCode.Text = "";
            txtEcodeSearch.Text = "";
            txtEname.Text = "";
            txtDipatchNo.Text = "";
            txtTripLRNo.Text = "";
            txtVehicleNo.Text = "";
            txtTransporter.Text = "";
            txtAddress.Text = "";
            txtRemarks.Text = "";
            txtQty.Text = "";
            txtProducts.Text = "";
            txtDcAmt.Text = "";
            gvProductDetails.Rows.Clear();
            cbVehicleType.SelectedIndex = 0;
            cbDocType.SelectedIndex = 0;
            strECode = "";
            strDCode = "";
            txtDueDays.Text = "";
            txtDeliveryPoint.Text = "";
        }
        private void gvProductDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex >= gvProductDetails.Columns["IssQty"].Index && e.ColumnIndex <= gvProductDetails.Columns["NetAmt"].Index)
            {
                if (gvProductDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                {
                    gvProductDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value="0";
                }
                CaluculateDisVat(e.RowIndex);
            }
            CalculateTotals();
        }
        private void CaluculateDisVat(int iRow)
        {
            double gQty = 0, dQty = 0, tQty = 0, disc = 0, vat = 0;

            if (gvProductDetails.Rows[iRow].Cells["IssQty"].Value.ToString() != "")
                gQty = Convert.ToDouble(gvProductDetails.Rows[iRow].Cells["IssQty"].Value);
            if (gQty >= 0 && Convert.ToDouble(gvProductDetails.Rows[iRow].Cells["Rate"].Value) >= 0)
            {
                gvProductDetails.Rows[iRow].Cells["Amount"].Value = gQty * (Convert.ToDouble(gvProductDetails.Rows[iRow].Cells["Rate"].Value));
                gvProductDetails.Rows[iRow].Cells["Amount"].Value = Convert.ToDouble(gvProductDetails.Rows[iRow].Cells["Amount"].Value).ToString("f");
                gvProductDetails.Rows[iRow].Cells["NetAmt"].Value = Convert.ToDouble(gvProductDetails.Rows[iRow].Cells["Amount"].Value).ToString("f");
            }
            if (Convert.ToDouble(gvProductDetails.Rows[iRow].Cells["Discper"].Value) >= 0)
            {
                double amt = gQty * (Convert.ToDouble(gvProductDetails.Rows[iRow].Cells["Rate"].Value));
                disc = (amt * Convert.ToDouble(gvProductDetails.Rows[iRow].Cells["Discper"].Value)) / 100;
                
                int iDisc = Convert.ToInt32(Convert.ToDouble(disc).ToString("0"));
                if (disc > iDisc)
                {
                    iDisc++;
                }
                gvProductDetails.Rows[iRow].Cells["DiscAmount"].Value = iDisc;
                gvProductDetails.Rows[iRow].Cells["NetAmt"].Value = amt - iDisc;
                gvProductDetails.Rows[iRow].Cells["NetAmt"].Value = Convert.ToDouble(gvProductDetails.Rows[iRow].Cells["NetAmt"].Value).ToString("f");
            }
            if (Convert.ToDouble(gvProductDetails.Rows[iRow].Cells["Vatper"].Value) >= 0)
            {
                double amt = gQty * (Convert.ToDouble(gvProductDetails.Rows[iRow].Cells["Rate"].Value));

                disc = (amt * Convert.ToDouble(gvProductDetails.Rows[iRow].Cells["Discper"].Value)) / 100;
                int iDisc = Convert.ToInt32(Convert.ToDouble(disc).ToString("0"));
                if (disc > iDisc)
                {
                    iDisc++;
                }
                double discAmt = amt - iDisc;
                gvProductDetails.Rows[iRow].Cells["DiscAmount"].Value = iDisc;

                vat = (discAmt * Convert.ToDouble(gvProductDetails.Rows[iRow].Cells["Vatper"].Value)) / 100;
                int iVat = Convert.ToInt32(Convert.ToDouble(vat).ToString("0"));
                if (vat > iVat)
                {
                    iVat++;
                }
                gvProductDetails.Rows[iRow].Cells["VatAmount"].Value = iVat;
                gvProductDetails.Rows[iRow].Cells["NetAmt"].Value = discAmt + iVat;
               
                gvProductDetails.Rows[iRow].Cells["NetAmt"].Value = Convert.ToDouble(gvProductDetails.Rows[iRow].Cells["NetAmt"].Value).ToString("f");
            }
        }
        private void txtEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (txtEcodeSearch.Text.ToString().Trim().Length > 0)
            //    EcodeSearch();
            //else
            //{
            //    cbEcode.SelectedIndex = -1;
            //}
        }
        private void EcodeSearch()
        {
            string sCompCode = CommonData.CompanyCode;
            string sDeptId = "";
            string sDesgId = "";
            objSQLDB = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                cbEcode.DataSource = null;
                cbEcode.Items.Clear();

                param[0] = objSQLDB.CreateParameter("@xCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLDB.CreateParameter("@xDeptID", DbType.String, sDeptId, ParameterDirection.Input);
                param[2] = objSQLDB.CreateParameter("@xDesigID", DbType.String, sDesgId, ParameterDirection.Input);
                param[3] = objSQLDB.CreateParameter("@xNameLike", DbType.String, txtEcodeSearch.Text, ParameterDirection.Input);
                ds = objSQLDB.ExecuteDataSet("DL_GetEmpSearchForSaleOrderBooking", CommandType.StoredProcedure, param);

                DataTable dtEmp = ds.Tables[0];

                if (dtEmp.Rows.Count > 0)
                {
                    cbEcode.DataSource = dtEmp;
                    cbEcode.DisplayMember = "EmpName";
                    cbEcode.ValueMember = "EmpCode";
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
                objSQLDB = null;
                Cursor.Current = Cursors.Default;
            }
        }
        private void RestrictToDigits(KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false)
                e.Handled = true;
            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void txtDealerCode_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }
        private void DealerSearch()
        {
            string sCompCode = CommonData.CompanyCode;
            string sFirmName = "";
            objSQLDB = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                cbDealer.DataSource = null;
                cbDealer.Items.Clear();
                txtEcodeSearch.Text = "";



                param[0] = objSQLDB.CreateParameter("@xCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLDB.CreateParameter("@xDealerName", DbType.String, txtDealerCode.Text, ParameterDirection.Input);
                param[2] = objSQLDB.CreateParameter("@xFirmName", DbType.String, sFirmName, ParameterDirection.Input);
                ds = objSQLDB.ExecuteDataSet("DL_GetDealersListSearch", CommandType.StoredProcedure, param);

                DataTable dtDealer = ds.Tables[0];

                if (dtDealer.Rows.Count > 0)
                {
                    cbDealer.DataSource = dtDealer;
                    cbDealer.DisplayMember = "FirmName";
                    cbDealer.ValueMember = "DealerCode";

                    //cbEcode.DataSource = dtDealer;
                    //cbEcode.DisplayMember = "SoName";
                    //cbEcode.ValueMember = "SoEcode";
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (cbDealer.SelectedIndex > -1)
                {
                    cbDealer.SelectedIndex = 0;
                    strDCode = ((System.Data.DataRowView)(cbDealer.SelectedItem)).Row.ItemArray[0].ToString();
                }
                objSQLDB = null;
                Cursor.Current = Cursors.Default;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            RestrictToDigits(e);
        }

        private void txtTotalFreight_KeyPress(object sender, KeyPressEventArgs e)
        {
            RestrictToDigits(e);
        }

        private void txtAdvancePaid_KeyPress(object sender, KeyPressEventArgs e)
        {
            RestrictToDigits(e);
        }

        private void txtLoadingCharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            RestrictToDigits(e);
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

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            ProductSearchAll PSearch = new ProductSearchAll("StockPoint_DLDC");
            PSearch.objDealerInvoice = this;
            PSearch.ShowDialog();
            CalculateTotals();
        }

        private void cbDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDealer.SelectedIndex > -1)
            {
                strDCode = ((System.Data.DataRowView)(cbDealer.SelectedItem)).Row.ItemArray[0].ToString();
                DealerInfo objDLInfo = new DealerInfo();

                DataTable dt = new DataTable();
                try
                {
                    dt = objDLInfo.DL_DealerSearch_Get(CommonData.CompanyCode, strDCode, "").Tables[0];
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    objDLInfo = null;
                }
                if (dt.Rows.Count > 0)
                {
                    txtAddress.Text = dt.Rows[0]["FirmName"].ToString() + ", ";
                    txtAddress.Text += dt.Rows[0]["FirmHNo"].ToString() + ", ";
                    txtAddress.Text += dt.Rows[0]["FirmVillage"].ToString() + ", ";
                    txtAddress.Text += dt.Rows[0]["FirmMandal"].ToString() + ", ";
                    txtAddress.Text += dt.Rows[0]["FrimDist"].ToString() + ", ";
                    txtAddress.Text += dt.Rows[0]["FirmState"].ToString();
                    txtAddress.Text += "-" + dt.Rows[0]["FirmPin"].ToString() + ", ";
                    txtAddress.Text += "PhNo:" + dt.Rows[0]["FirmMobile"].ToString();
                    strDLMobileNo = dt.Rows[0]["FirmMobile"].ToString();
                    txtEcodeSearch.Text = dt.Rows[0]["SoEcode"].ToString() + "";
                }
                else
                {
                    strDLMobileNo = "";
                    txtAddress.Text = "";
                    txtEcodeSearch.Text = "";
                }
                
            }
        }

        private void cbEcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEcode.SelectedIndex > -1)
            {
                strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
            }
        }

        private void cbRefType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRefType.SelectedIndex > 0)
            {
                lblEcode.Text = "Name";
                //txtEname.Text = "";
                cbEcode.Visible = false;
                txtEname.Visible = true;
                txtEcodeSearch.Visible = false;
                //txtEname.Enabled = false;
            }
            else
            {
                lblEcode.Text = "Ecode";
                //txtEname.Text = "";
                //txtEname.Enabled = true;
                cbEcode.Visible = true;
                txtEname.Visible = false;
                txtEcodeSearch.Visible = true;
            }
        }

        private void cbDocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDocType.SelectedIndex > 0)
            {
                txtDipatchNo.Text = txtTransactionNo.Text;
                dtpDispDate.Value = meTransactionDate.Value;
                

            }
            else
            {
                txtDipatchNo.Text = "";
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
            //if (Convert.ToString(txtReferenceNo.Text).Length == 0)
            //{
            //    MessageBox.Show("Enter Reference No", "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    blValue = false;
            //    txtReferenceNo.Focus();
            //    return blValue;
            //}

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
            if (Convert.ToString(txtTotalFreight.Text).Trim().Length == 0)
            {
                MessageBox.Show("Enter Freight !", "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtTotalFreight.Focus();
                return blValue;
            }
            if(strDCode.Length==0)
            {
                MessageBox.Show("Enter Dealer Code!", "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtDealerCode.Focus();
                return blValue;
            }
            if (txtDipatchNo.Text.Trim().Length == 0)
            {
                MessageBox.Show("Enter Invoice No !", "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtDipatchNo.Focus();
                return blValue;
            }
            if (cbRefType.SelectedItem.ToString() == "EMPLOYEE")
            {
                if (strECode.Length == 0)
                {
                    MessageBox.Show("Enter Employee Name!", "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    blValue = false;
                    txtEname.Focus();
                    return blValue;
                }
            }
            else
            {
                if (txtEname.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Enter Employee Name!", "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    blValue = false;
                    txtEname.Focus();
                    return blValue;
                }
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
            for (int i = 0; i < gvProductDetails.Rows.Count; i++)
            {
                if ((Convert.ToString(gvProductDetails.Rows[i].Cells["IssQty"].Value) != "" && Convert.ToString(gvProductDetails.Rows[i].Cells["CASE"].Value) != "" && Convert.ToString(gvProductDetails.Rows[i].Cells["NetAmt"].Value) != ""))
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
            if (txtDueDays.Text.Length==0)
            {
                blValue = false;
                MessageBox.Show("Enter Due Days", "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            return blValue;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            int intSave = 0;
            try
            {
                if (CheckData())
                {
                    if (SaveDcHeadData() > 0)
                    {
                        intSave = SaveDCDetlData();
                    }
                    else
                    {
                        string strSQL = "DELETE from DL_DC_HEAD" +
                                   " WHERE DLDH_COMPANY_CODE='" + CommonData.CompanyCode +
                                   "' AND DLDH_BRANCH_CODE='" + CommonData.BranchCode +
                                   "' AND DLDH_TRN_NUMBER='" + txtTransactionNo.Text +
                                   "' AND DLDH_FIN_YEAR='" + sFinYear + "'";
                        strSQL += " DELETE FROM FA_OUTSTANDING WHERE OU_BILL_NUMBER ='" + txtTransactionNo.Text + "';";

                        objSQLDB = new SQLDB();
                        int intDel = objSQLDB.ExecuteSaveData(strSQL);
                        objSQLDB = null;
                    }
                    if (intSave > 0)
                    {
                        ///=========Inserting Record into FA_OUTSTANDING Table=============///
                        DateTime DT = meTransactionDate.Value.AddDays(Convert.ToDouble(txtDueDays.Text));
                        string sDocMonth = meTransactionDate.Value.ToString("MMM").ToUpper() + "" + meTransactionDate.Value.ToString("yyyy");

                        string strSQL = " DELETE FROM FA_OUTSTANDING WHERE OU_BILL_NUMBER ='" + txtTransactionNo.Text + "';";
                        strSQL += " INSERT INTO FA_OUTSTANDING(OU_COMPANY_CODE" +
                                ",OU_STATE_CODE" +
                                ",OU_BRANCH_CODE" +
                                ",OU_FIN_YEAR" +
                                ",OU_ACCOUNT_ID" +
                                ",OU_BILL_TYPE" +
                                ",OU_BILL_NUMBER" +
                                ",OU_BILL_DATE" +
                                ",OU_DUE_DATE" +
                                ",OU_BILL_AMOUNT" +
                                ",OU_AMOUNT" +
                                ",OU_DR_CR_ID" +
                                ",OU_DOCUMENT_MONTH" +
                                ",OU_AMT_PAID_RCVD" +
                                ",OU_CREATED_BY" +
                                ",OU_CREATED_DATE) VALUES('" + CommonData.CompanyCode.ToString() +
                                "','" + CommonData.StateCode +
                                "','" + CommonData.BranchCode +
                                "','" + sFinYear +
                                "','" + strDCode +
                                "','" + strFormType + "'" +
                                ",'" + txtTransactionNo.Text +
                                "','" + Convert.ToDateTime(meTransactionDate.Value).ToString("dd/MMM/yyyy") +
                                "','" + Convert.ToDateTime(DT).ToString("dd/MMM/yyyy") +
                                "','" + txtDcAmt.Text +
                                "','" + txtDcAmt.Text +
                                "','D'" +
                                ",'" + sDocMonth +
                                "','0" +
                                "','" + CommonData.LogUserId +
                                "',GETDATE())";
                        if (strSQL.Length > 10)
                        {
                            objSQLDB = new SQLDB();
                            intSave = objSQLDB.ExecuteSaveData(strSQL);
                        }
                        ////////////////////////////////////////////////////////////////////////////////////////

                        IsModify = false;
                        //MessageBox.Show("DC data saved.\nTran No: " + txtTransactionNo.Text, "Delivery challan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         DialogResult result = MessageBox.Show("DC data saved.\nTran No: "+txtTransactionNo.Text+"   \n And Do You want to Print this Invoice",
                                               "SSCRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                         if (result == DialogResult.Yes)
                         {
                             PrintInvoice();
                         }
                         if (DateTime.Now.AddDays(-5) <= meTransactionDate.Value)
                         {
                             if (txtDeliveryPoint.Text.Replace(" ", "").Length > 0)
                             {
                                 DialogResult resultSms = MessageBox.Show("Do you want to send SMS to Dealer?", "SSERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                 if (resultSms == DialogResult.Yes)
                                 {

                                     string sqlText = "SELECT stdm_emp_code EmpCode,HECD_EMP_MOBILE_NO MobileNo FROM DLSourceToDest_Map " +
                                                        "LEFT JOIN HR_EMP_CONTACT_DETL ON HECD_EORA_CODE = stdm_emp_code " +
                                                        "WHERE stdm_dealer_code = " + ((System.Data.DataRowView)(cbDealer.SelectedItem)).Row.ItemArray[0].ToString();

                                     objSQLDB = new SQLDB();
                                     try
                                     {
                                         DataTable dtCont = objSQLDB.ExecuteDataSet(sqlText).Tables[0];
                                         if (dtCont.Rows.Count > 0)
                                         {
                                             for (int i = 0; i < dtCont.Rows.Count; i++)
                                             {
                                                 if (dtCont.Rows[i]["MobileNo"].ToString().Length > 0)
                                                 {
                                                     strDLMobileNo += "," + dtCont.Rows[i]["MobileNo"].ToString();
                                                 }
                                             }
                                         }
                                     }
                                     catch
                                     {

                                     }
                                     finally
                                     {
                                         objSQLDB = null;

                                     }
                                     string sMessage = "Dealer Name: " + cbDealer.Text.ToString();
                                     sMessage += ", Invoice No: " + txtReferenceNo.Text;
                                     sMessage += ", Invoice Date: " + Convert.ToDateTime(meTransactionDate.Value).ToString("dd-MMM-yyyy");
                                     sMessage += ", LRNo: " + txtTripLRNo.Text;
                                     sMessage += ", LRDate: " + Convert.ToDateTime(dtpDispDate.Value).ToString("dd-MMM-yyyy");
                                     sMessage += ", Transport Name: " + txtTransporter.Text;
                                     sMessage += ", BookingPoint: " + CommonData.BranchName;
                                     sMessage += ", DeliveryPoint: " + txtDeliveryPoint.Text;

                                     strDLMobileNo += ",08008004115";

                                     try
                                     {
                                         if (strDLMobileNo.Length > 0)
                                         {
                                             sMessage = sMessage.Replace("&", ",");
                                             HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://dnd.sendsmsindia.org/sendsms.jsp?" +
                                                     "user=SBTLAP&password=admin@66&mobiles=" + strDLMobileNo +
                                                     "&sms=" + sMessage + "&senderid=SHNMUK&version=3");
                                             //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://dnd.sendsmsindia.org/getundeliveredreasonanddescription.jsp?userid=SBTLAP&password=admin@66");
                                             HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                                             StreamReader reader = new StreamReader(response.GetResponseStream());
                                             string result1 = reader.ReadToEnd();
                                             sqlText = " INSERT INTO SMS_HISTORY(" +
                                                         "sms_sender_id," +
                                                         "sms_mobile_no," +
                                                         "sms_name," +
                                                         "sms_type," +
                                                         "sms_message," +
                                                         "sms_sent_by," +
                                                         "sms_api_result," +
                                                         "sms_sent_date)" +
                                                         " VALUES('SHNMUK" +
                                                         "','" + strDLMobileNo +
                                                         "','" + cbDealer.Text.ToString() +
                                                         "','" + "SATL_DL_INVOICE" +
                                                         "','" + sMessage +
                                                         "','ERP" +
                                                         "','" + result1 +
                                                         "',getdate());";


                                             //System.Threading.Thread.Sleep(3000);
                                             request = null;
                                             response = null;
                                             reader = null;
                                         }
                                     }
                                     catch (Exception ex)
                                     {

                                     }
                                     finally
                                     {

                                     }
                                     objSQLDB = new SQLDB();
                                     try
                                     {
                                         objSQLDB.ExecuteSaveData(sqlText);
                                     }
                                     catch (Exception ex)
                                     {

                                     }
                                     finally
                                     {
                                         objSQLDB = null;
                                     }
                                 }
                             }
                         }
                        ClearForm();
                        txtTransactionNo.Text = NewTransactionNumber().ToString();
                        for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                        {
                            gvProductDetails.Rows[i].Cells["IssQty"].Value = "0";
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
            finally
            {

            }
        }
        private int SaveDcHeadData()
        {
            int intSave = 0;
            string strSQL = string.Empty;
            objSQLDB = new SQLDB();
            string strDriver = string.Empty;
            string ecode = string.Empty;

            try
            {
                string sDocMonth = meTransactionDate.Value.ToString("MMM").ToUpper() + "" + meTransactionDate.Value.ToString("yyyy");
                //string sDocMonth = meTransactionDate.Value.ToString("MMMyyyy");
                if (cbVehicleType.Text == "OWN")
                    strDriver = cbDriverEcode.Text.ToString();
                else
                    strDriver = txtDriverNo.Text.ToString();
                string strOtherName = "";
                if (cbRefType.SelectedItem.ToString() == "EMPLOYEE")
                {
                    strECode = cbEcode.SelectedValue.ToString();
                    strOtherName = "";
                }
                else
                {
                    strECode = "0";
                    strOtherName = txtEname.Text;
                }
                if (txtReferenceNo.Text.Trim().Length == 0)
                    txtReferenceNo.Text = "0";
                if (txtBuyOrdNo.Text.Trim().Length==0)
                    txtBuyOrdNo.Text = "0";
                if (txtTotalFreight.Text.ToString().Trim().Length == 0)
                    txtTotalFreight.Text = "0.00";
                if (txtAdvancePaid.Text.ToString().Trim().Length == 0)
                    txtAdvancePaid.Text = "0.00";
                if (txtToPay.Text.ToString().Trim().Length == 0)
                    txtToPay.Text = "0.00";
                if (txtLoadingCharges.Text.ToString().Trim().Length == 0)
                    txtLoadingCharges.Text = "0.00";
                if (IsModify == false)
                {
                    txtTransactionNo.Text = NewTransactionNumber().ToString();
                    if (txtTransactionNo.Text.ToString().Trim().Length > 10)
                    {
                        strSQL = " INSERT INTO DL_DCINV_HEAD" +
                             "(DLDH_COMPANY_CODE" +
                             ", DLDH_STATE_CODE" +
                             ", DLDH_BRANCH_CODE" +
                             ", DLDH_FIN_YEAR" +
                             ", DLDH_DOCUMENT_MONTH" +
                             ", DLDH_TRN_TYPE" +
                             ", DLDH_TRN_NUMBER" +
                             ", DLDH_REFERENCE_NUMBER" +
                             ", DLDH_TRN_DATE" +
                             ", DLDH_DEALER_CODE" +
                             ", DLDH_REF_TYPE"+
                             ", DLDH_REF_ECODE" +
                             ", DLDH_OTHER_REF_NAME" +
                             ", DLDH_BUYERS_ORDER_NO" +
                             ", DLDH_DISPATCH_DOC_TYPE" +
                             ", DLDH_DISPATCH_DOC_NO" +
                             ", DLDH_DISPATCH_DOC_DATE" +
                             ", DLDH_TRIP_OR_LR_NUMBER" +
                             ", DLDH_WAYBILL_NO" +
                             ", DLDH_VEHICLE_SOURCE" +
                             ", DLDH_VEHICLE_NUMBER" +
                             ", DLDH_TRANSPORTER_NAME" +
                             ", DLDH_DRIVER_NAME" +
                             ", DLDH_TOTAL_FREIGHT" +
                             ", DLDH_ADVANCE_PAID" +
                             ", DLDH_TO_PAY_FREIGHT" +
                             ", DLDH_LOADING_CHARGES" +
                             ", DLDH_DESTINATION_ADDRESS" +
                             ", DLDH_REMARKS" +
                             ", DLDH_CREATED_BY" +
                             ", DLDH_CREATED_DATE "+
                             ", DLDH_NET_SALES" +
                             ", DLDH_VAT_AMOUNT" +
                             ", DLDH_DISCOUNT_AMOUNT" +
                             ", DLDH_INVOICE_AMOUNT" +
                             ", DLDH_DUE_DAYS"+
                             ",DLDH_DELIVERY_POINT) " +
                             " VALUES ('" + CommonData.CompanyCode +
                             "', '" + CommonData.StateCode +
                             "', '" + CommonData.BranchCode +
                             "', '" + sFinYear +
                             "', '" + sDocMonth +
                             "', '" + strFormType + "'" +
                             ", '" + txtTransactionNo.Text +
                             "', '" + txtReferenceNo.Text.Replace(" ","") +
                             "', '" + Convert.ToDateTime(meTransactionDate.Value).ToString("dd/MMM/yyyy") +
                             "', "+Convert.ToInt32(strDCode)+
                             ", '" +cbRefType.SelectedItem.ToString()+
                        //if (cbRefType.SelectedItem.ToString() == "EMPLOYEE")
                        //{
                        //    strSQL += "', " + Convert.ToInt32(strECode)+", '" + cbEcode.SelectedText.ToString() ;
                        //}
                        //else
                        //{
                        //    strSQL += "', " + Convert.ToInt32(txtEcodeSearch.Text)+", '" + txtEname.Text ;
                        //}
                        //   strSQL += "', " + Convert.ToInt32(txtBuyOrdNo.Text) +
                             "', "+Convert.ToInt32(strECode)+
                             ",'" + strOtherName +
                             "'," + Convert.ToInt32(txtBuyOrdNo.Text) +
                             ", '" + cbDocType.SelectedItem.ToString() +
                             "', '" + txtDipatchNo.Text +
                             "', '" + dtpDispDate.Value.ToString("dd/MMM/yyyy")+
                             "', '" + txtTripLRNo.Text +
                             "', '" + txtWayBillNo.Text.Replace(" ","") +
                             "', '" + cbVehicleType.Text +
                             "', '" + txtVehicleNo.Text +
                             "', '" + txtTransporter.Text +
                             "', '" + strDriver +
                             "', " + txtTotalFreight.Text +
                             ", " + txtAdvancePaid.Text +
                             ", " + txtToPay.Text +
                             ", " + txtLoadingCharges.Text +
                             ", '" + txtAddress.Text +
                             "', '" + txtRemarks.Text +
                             "', '" + CommonData.LogUserId +
                             "',getdate()"+ 
                             ", " + txtAmt.Text +
                             ", " + txtVatAmt.Text +
                             ", " + txtDiscAmt.Text +
                             ", "+txtDcAmt.Text+
                            ", "+Convert.ToInt32( txtDueDays.Text)+
                            ",'"+txtDeliveryPoint.Text+"')";
                    }

                }
                else
                {
                    strSQL = " DELETE from DL_DCINV_DETL" +
                                " WHERE DLDD_COMPANY_CODE='" + CommonData.CompanyCode +
                                    "' AND DLDD_BRANCH_CODE='" + CommonData.BranchCode +
                                    "' AND DLDD_TRN_NUMBER='" + txtTransactionNo.Text +
                                    "'  AND DLDD_FIN_YEAR='" + sFinYear + "'";

                     strSQL += " DELETE FROM FA_OUTSTANDING WHERE OU_BILL_NUMBER ='" + txtTransactionNo.Text + "';";
                    int intRec = objSQLDB.ExecuteSaveData(strSQL);

                    strSQL = " UPDATE DL_DCINV_HEAD set " +
                               " DLDH_TRN_DATE ='" + Convert.ToDateTime(meTransactionDate.Value).ToString("dd/MMM/yyyy") +
                               "', DLDH_REFERENCE_NUMBER='"+txtReferenceNo.Text.Replace(" ","")+
                                "', DLDH_DEALER_CODE=" + Convert.ToInt32(strDCode) +
                                ", DLDH_REF_TYPE='" + cbRefType.SelectedItem.ToString() +
                                "', DLDH_REF_ECODE=" + Convert.ToInt32(strECode) +
                                ", DLDH_OTHER_REF_NAME='" + strOtherName +
                                "', DLDH_BUYERS_ORDER_NO=" + Convert.ToInt32(txtBuyOrdNo.Text) +
                                ", DLDH_DISPATCH_DOC_TYPE='" + cbDocType.SelectedItem.ToString() +
                                "', DLDH_DISPATCH_DOC_NO='" + txtDipatchNo.Text +
                                "', DLDH_DISPATCH_DOC_DATE='" + dtpDispDate.Value.ToString("dd/MMM/yyyy") +
                                "', DLDH_TRIP_OR_LR_NUMBER='" + txtTripLRNo.Text +
                                "', DLDH_WAYBILL_NO='" + txtWayBillNo.Text.Replace(" ","") +
                                "', DLDH_VEHICLE_SOURCE='" + cbVehicleType.Text +
                                "', DLDH_VEHICLE_NUMBER='" + txtVehicleNo.Text.Replace(" ","") +
                                "', DLDH_TRANSPORTER_NAME='" + txtTransporter.Text +
                                "', DLDH_DRIVER_NAME='" + strDriver +
                                "', DLDH_TOTAL_FREIGHT=" + txtTotalFreight.Text +
                                ", DLDH_ADVANCE_PAID=" + txtAdvancePaid.Text +
                                ", DLDH_TO_PAY_FREIGHT=" + txtToPay.Text +
                                ", DLDH_LOADING_CHARGES=" + txtLoadingCharges.Text +
                                ", DLDH_DESTINATION_ADDRESS='" + txtAddress.Text +
                                "', DLDH_REMARKS='" + txtRemarks.Text +
                                "',DLDH_NET_SALES=" + txtAmt.Text +
                                ",DLDH_VAT_AMOUNT=" + txtVatAmt.Text +
                                ",DLDH_DISCOUNT_AMOUNT=" + txtDiscAmt.Text +
                                ",DLDH_INVOICE_AMOUNT=" + txtDcAmt.Text +
                                ",DLDH_DUE_DAYS="+Convert.ToInt32( txtDueDays.Text)+
                                ",DLDH_DELIVERY_POINT='" + txtDeliveryPoint.Text +
                             "', DLDH_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                             "', DLDH_LAST_MODIFIED_DATE='" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                             "' WHERE DLDH_TRN_NUMBER = '" + txtTransactionNo.Text +
                             "' AND DLDH_BRANCH_CODE='" + CommonData.BranchCode +
                             "' AND DLDH_FIN_YEAR='" + sFinYear +
                             "' AND DLDH_COMPANY_CODE='" + CommonData.CompanyCode.ToString() + "'";


                }
                if(strSQL.Length>10)
                {
                    intSave= objSQLDB.ExecuteSaveData(strSQL);
                }
               
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
            string sdocMonth = meTransactionDate.Value.ToString("MMM").ToUpper() + "" + meTransactionDate.Value.ToString("yyyy");
            try
            {
                strSQL = " DELETE from DL_DCINV_DETL" +
                                " WHERE DLDD_COMPANY_CODE='" + CommonData.CompanyCode +
                                    "' AND DLDD_BRANCH_CODE='" + CommonData.BranchCode +
                                    "' AND DLDD_TRN_NUMBER='" + txtTransactionNo.Text +
                                    "' AND DLDD_FIN_YEAR='" + sFinYear + "'";

                intSave = objSQLDB.ExecuteSaveData(strSQL);
                sbSQL = new StringBuilder();
                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {
                    if (gvProductDetails.Rows[i].Cells["IssQty"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["IssQty"].Value.ToString() != "0" && gvProductDetails.Rows[i].Cells["NetAmt"].Value.ToString() != "")
                    {
                        sbSQL.Append(" INSERT INTO DL_DCINV_DETL (DLDD_COMPANY_CODE"+
                                        ", DLDD_STATE_CODE, DLDD_BRANCH_CODE, DLDD_FIN_YEAR"+
                                        ", DLDD_DOCUMENT_MONTH, DLDD_TRN_TYPE, DLDD_TRN_NUMBER,  DLDD_SL_NO" +
                                        ",DLDD_PRODUCT_ID,DLDD_ISS_QTY,DLDD_ISS_CASE,DLDD_ISS_RATE,DLDD_ISS_AMT,DLDD_DISC_PER" +
                                        ",DLDD_DISC_AMOUNT,DLDD_VAT_PER,DLDD_VAT_AMOUNT,DLDD_NET_AMOUNT,DLDD_BATCH_NO)" +
                                        " VALUES ('" + CommonData.CompanyCode + "', '" + CommonData.StateCode + 
                                        "', '" + CommonData.BranchCode + "', '" + sFinYear + "', '" + sdocMonth +
                                        "', '" + strFormType + "', '" + txtTransactionNo.Text +
                                        "', " + gvProductDetails.Rows[i].Cells["SLNO"].Value +
                                        ", '" + gvProductDetails.Rows[i].Cells["ProductId"].Value + 
                                        "', " + gvProductDetails.Rows[i].Cells["IssQty"].Value +
                                        ", " + gvProductDetails.Rows[i].Cells["CASE"].Value +
                                        ", " + gvProductDetails.Rows[i].Cells["Rate"].Value + 
                                        ", " + gvProductDetails.Rows[i].Cells["Amount"].Value +
                                        ", " + gvProductDetails.Rows[i].Cells["Discper"].Value + 
                                        ", " + gvProductDetails.Rows[i].Cells["DiscAmount"].Value +
                                        "," + gvProductDetails.Rows[i].Cells["Vatper"].Value + 
                                        ", " + gvProductDetails.Rows[i].Cells["VatAmount"].Value +
                                        "," + gvProductDetails.Rows[i].Cells["NetAmt"].Value + 
                                        ",'" + gvProductDetails.Rows[i].Cells["BatchNo"].Value.ToString().Trim().Replace(" ","") + "'); ");

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
        private void FillTransactionData(string TranNO)
        {
            objIndent = new IndentDB();
            Hashtable ht = new Hashtable();
            try
            {

                ht = objIndent.GetDLDeliveryChallanData(TranNO);
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
                    IsModify = true;
                    //strTransactionNo = dtHead.Rows[0]["TrnNumber"].ToString();
                    txtTransactionNo.Text = dtHead.Rows[0]["TrnNumber"] + "";
                    txtReferenceNo.Text = dtHead.Rows[0]["ReferanceNumber"] + "";
                    meTransactionDate.Value = Convert.ToDateTime(Convert.ToDateTime(dtHead.Rows[0]["TrnDate"]).ToString("dd/MM/yyyy"));
                    txtTotalFreight.Text = dtHead.Rows[0]["TotalFreight"] + "";
                    txtAdvancePaid.Text = dtHead.Rows[0]["AdvancePaid"] + "";
                    txtToPay.Text = dtHead.Rows[0]["ToPayFreight"] + "";
                    txtLoadingCharges.Text = dtHead.Rows[0]["loading_charges"] + "";
                    //cbTransactionType.Text = dtHead.Rows[0]["TrnType"] + "";
                    //txtDocMonth.Text = dtHead.Rows[0]["DocMonth"] + "";
                    sFinYear = dtHead.Rows[0]["FinYear"] + "";
                    //FillBranchGroupData(0);
                    //strECode = dtHead.Rows[0]["ToEcode"] + "";
                    //cbEcode.SelectedIndex = objGeneral.GetComboBoxSelectedIndex(strECode, cbEcode);
                    cbRefType.SelectedItem = dtHead.Rows[0]["RefType"] + "";
                    //txtEname.Text = dtHead.Rows[0]["RefEcode"] + "";
                    
                    txtDealerCode.Text = dtHead.Rows[0]["DealerCode"] + "";
                    EcodeSearch();
                    DealerSearch();
                    if (dtHead.Rows[0]["RefType"].ToString() == "EMPLOYEE")
                    {
                        cbEcode.Visible = true;
                        cbEcode.SelectedValue = dtHead.Rows[0]["RefEcode"] + "";
                        txtEcodeSearch.Text = dtHead.Rows[0]["RefEcode"] + "";
                    }
                    else
                    {
                        cbEcode.Visible = false;
                        txtEname.Text = dtHead.Rows[0]["RefName"] + "";
                    }
                    cbDealer.SelectedValue = dtHead.Rows[0]["DealerCode"] + "";
                    txtBuyOrdNo.Text = dtHead.Rows[0]["BuyerOrNo"] + "";
                   
                    cbDocType.SelectedItem = dtHead.Rows[0]["DocType"] + "";
                    txtDipatchNo.Text = dtHead.Rows[0]["DocNo"] + "";
                    dtpDispDate.Value = Convert.ToDateTime(Convert.ToDateTime(dtHead.Rows[0]["DocDate"]).ToString("dd/MM/yyyy"));
                    
                    cbVehicleType.SelectedValue = dtHead.Rows[0]["VehicleSource"] + "";

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
                    txtAddress.Text = dtHead.Rows[0]["DestAddress"] + "";
                    txtRemarks.Text = dtHead.Rows[0]["Remarks"] + "";
                    txtAmt.Text = dtHead.Rows[0]["NetSales"] + "";
                    txtVatAmt.Text = dtHead.Rows[0]["VatAmt"] + "";
                    txtDiscAmt.Text = dtHead.Rows[0]["DiscAmt"] + "";
                    txtDcAmt.Text = dtHead.Rows[0]["InvoicAmt"] + "";
                    txtDueDays.Text = dtHead.Rows[0]["dueDays"] + "";
                    txtDeliveryPoint.Text = dtHead.Rows[0]["DeliveryPoint"] + "";
                    FillTransactionDetl(dtDetl);
                    isData = true;
                }
                else
                {
                    //txtTransactionNo.Text = NewTransactionNumber();
                    btnSave.Enabled = true;
                    btnDelete.Enabled = true;
                    IsModify = false;
                    sFinYear = CommonData.FinancialYear;
                    gvProductDetails.Rows.Clear();
                    txtVatPers.Text = "5";
                    txtTotalFreight.Text = "";
                    txtToPay.Text = "";
                    txtAdvancePaid.Text = "";
                    txtLoadingCharges.Text = "";
                    txtBuyOrdNo.Text = "";
                    txtDealerCode.Text = "";
                    txtEcodeSearch.Text = "";
                    txtEname.Text = "";
                    txtDipatchNo.Text = "";
                    txtTripLRNo.Text = "";
                    txtVehicleNo.Text = "";
                    txtTransporter.Text = "";
                    txtAddress.Text = "";
                    txtRemarks.Text = "";
                    txtQty.Text = "";
                    txtProducts.Text = "";
                    txtDcAmt.Text = "";
                    gvProductDetails.Rows.Clear();
                    cbVehicleType.SelectedIndex = 0;
                    cbDocType.SelectedIndex = 0;
                    strECode = "";
                    strDCode = "";
                    txtDueDays.Text = "";
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
            objIndent = new IndentDB();
            try
            {
                gvProductDetails.Rows.Clear();
                //cbIndents.SelectedIndex = -1;
                if (dt.Rows.Count > 0)
                {
                    for (int intRow = 0; intRow < dt.Rows.Count; intRow++)
                    {

                        gvProductDetails.Rows.Add();
                        gvProductDetails.Rows[intRow].Cells["SLNO"].Value = intRow + 1;
                        gvProductDetails.Rows[intRow].Cells["Category"].Value = dt.Rows[intRow]["category_name"].ToString();
                        gvProductDetails.Rows[intRow].Cells["Product"].Value = dt.Rows[intRow]["product_name"].ToString();
                        gvProductDetails.Rows[intRow].Cells["BatchNo"].Value = dt.Rows[intRow]["BatchNo"].ToString();
                        gvProductDetails.Rows[intRow].Cells["ProductId"].Value = dt.Rows[intRow]["ProductId"].ToString();
                        gvProductDetails.Rows[intRow].Cells["IssQty"].Value = dt.Rows[intRow]["IssQty"].ToString();
                        gvProductDetails.Rows[intRow].Cells["CASE"].Value = dt.Rows[intRow]["IssCase"].ToString();
                        gvProductDetails.Rows[intRow].Cells["RATE"].Value = dt.Rows[intRow]["IssRate"].ToString();
                        gvProductDetails.Rows[intRow].Cells["Amount"].Value = dt.Rows[intRow]["IssAmt"].ToString();
                        gvProductDetails.Rows[intRow].Cells["Discper"].Value = dt.Rows[intRow]["DisPer"].ToString();
                        gvProductDetails.Rows[intRow].Cells["DiscAmount"].Value = dt.Rows[intRow]["DisAmt"].ToString();
                        gvProductDetails.Rows[intRow].Cells["Vatper"].Value = dt.Rows[intRow]["VatPer"].ToString();
                        gvProductDetails.Rows[intRow].Cells["VatAmount"].Value = dt.Rows[intRow]["VatAmt"].ToString();
                        gvProductDetails.Rows[intRow].Cells["NetAmt"].Value = dt.Rows[intRow]["NetAmt"].ToString();

                    }
                }
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

        private void gvProductDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == gvProductDetails.Columns["Delete"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        DataGridViewRow dgvr = gvProductDetails.Rows[e.RowIndex];
                        gvProductDetails.Rows.Remove(dgvr);
                        OrderSlNo();
                        CalculateTotals();
                    }

                }
            }
        }
        private void OrderSlNo()
        {
            if (gvProductDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {
                    gvProductDetails.Rows[i].Cells["SLNO"].Value = i + 1;
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

                            string strDelete = " DELETE from DL_DC_DETL " +
                                                " WHERE DLDD_COMPANY_CODE='" + CommonData.CompanyCode +
                                                "' AND DLDD_BRANCH_CODE='" + CommonData.BranchCode +
                                                "' AND DLDD_TRN_NUMBER='" + txtTransactionNo.Text +
                                                "' AND DLDD_FIN_YEAR='" + CommonData.FinancialYear + "'";

                            strDelete += "DELETE FROM FA_OUTSTANDING WHERE OU_BILL_NUMBER=" + txtTransactionNo.Text;


                            strDelete += "DELETE from DL_DC_HEAD" +
                                                " WHERE DLDH_COMPANY_CODE='" + CommonData.CompanyCode +
                                                "' AND DLDH_BRANCH_CODE='" + CommonData.BranchCode +
                                                "' AND DLDH_TRN_NUMBER='" + txtTransactionNo.Text +
                                                "' AND DLDH_FIN_YEAR='" + CommonData.FinancialYear + "' ";


                            int intRec = objSQLDB.ExecuteSaveData(strDelete);
                            if (intRec > 0)
                            {
                                IsModify = false;
                                MessageBox.Show("Tran No: " + txtTransactionNo.Text + " Deleted!");
                                ClearForm();
                                gvProductDetails.Rows.Clear();
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

        private void cbDriverEcode_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void cbVehicleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbVehicleType.Text == "OWN")
            {
                cbDriverEcode.Visible = true;
                txtTripLRNo.Enabled = true;
                txtDriverNo.Enabled = true;
                txtVehicleNo.Enabled = true;
                txtTransporter.Enabled = true;
            }
            else if (cbVehicleType.Text == "BYHAND")
            {
                txtTripLRNo.Enabled = false;
                txtDriverNo.Enabled = false;
                txtVehicleNo.Enabled = false;
                txtTransporter.Enabled = false;
                cbDriverEcode.Visible = false;
            }
            else
            {
                cbDriverEcode.Visible = false;
                txtTripLRNo.Enabled = true;
                txtDriverNo.Enabled = true;
                txtVehicleNo.Enabled = true;
                txtTransporter.Enabled = true;
            }
        }

        private void txtBuyOrdNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //RestrictToDigits(e);
        }

        private void gvProductDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //blIsCellQty = true;
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 4)
            {
                TextBox txtQty1 = e.Control as TextBox;
                if (txtQty1 != null)
                {
                    flagText = true;
                    txtQty1.MaxLength = 30;
                    txtQty1.KeyPress += new KeyPressEventHandler(txt_KeyPress1);

                }
            }
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 5)
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    flagText = false;
                    txtQty.MaxLength = 30;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                    
                }
            }

            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 6)
            {
                TextBox txtRate = e.Control as TextBox;
                if (txtRate != null)
                {
                    flagText = false;
                    txtRate.MaxLength = 10;
                    txtRate.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 8)
            {

                TextBox txtCase = e.Control as TextBox;
                if (txtCase != null)
                {
                    flagText = false;
                    txtCase.MaxLength = 10;
                    txtCase.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 9)
            {

                TextBox txtDisc = e.Control as TextBox;
                if (txtDisc != null)
                {
                    flagText = false;
                    txtDisc.MaxLength = 10;
                    txtDisc.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 11)
            {
                TextBox txtVat = e.Control as TextBox;
                if (txtVat != null)
                {
                    flagText = false;
                    txtVat.MaxLength = 10;
                    txtVat.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
        }
        private void txt_KeyPress1(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);

        }
        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46) && flagText==false)
            {
                e.Handled = true;
                return;
            }

            // checks to make sure only 1 decimal is allowed
            if (e.KeyChar == 46 && flagText == false)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }

        }

        private void txtDiscPers_Validated(object sender, EventArgs e)
        {
            if (txtDiscPers.Text.Length > 0)
            {
                for (int iVar = 0; iVar < gvProductDetails.Rows.Count; iVar++)
                {
                    gvProductDetails.Rows[iVar].Cells["Discper"].Value = txtDiscPers.Text;
                    CaluculateDisVat(iVar);
                }
                CalculateTotals();
            }
        }

        private void txtVatPers_Validated(object sender, EventArgs e)
        {
            if (txtVatPers.Text.Length > 0)
            {
                for (int iVar = 0; iVar < gvProductDetails.Rows.Count; iVar++)
                {
                    gvProductDetails.Rows[iVar].Cells["Vatper"].Value = txtVatPers.Text;
                    CaluculateDisVat(iVar);
                }
                CalculateTotals();
            }
        }

        private void txtEname_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtDealerCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtDealerCode.Text.ToString().Trim().Length > 0)
                DealerSearch();
            else
            {
                cbDealer.SelectedIndex = -1;
                strDCode = "";
            }
        }

        private void txtEname_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEname.Text.ToString().Trim().Length > 0)
                EcodeSearch();
            else
            {
                cbEcode.SelectedIndex = -1;
                strECode = "";
            }
        }

        private void txtWayBillNo_KeyUp(object sender, KeyEventArgs e)
        {
            txtWayBillNo.Text = txtWayBillNo.Text.Replace(" ", "");
        }

        private void txtBuyOrdNo_KeyUp(object sender, KeyEventArgs e)
        {
            txtBuyOrdNo.Text = txtBuyOrdNo.Text.Replace(" ", "");
        }

        private void txtReferenceNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strCmd = "SELECT DLDH_TRN_NUMBER FROM DL_DCINV_HEAD WHERE DLDH_REFERENCE_NUMBER='" + txtReferenceNo.Text+"'";
                objSQLDB = new SQLDB();
                DataTable dt = objSQLDB.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtTransactionNo.Text = dt.Rows[0]["DLDH_TRN_NUMBER"].ToString();
                    //FillTransactionData(dt.Rows[0]["DLDH_TRN_NUMBER"].ToString());
                    //CalculateTotals();
                }
                else
                {
                     txtTransactionNo.Text = NewTransactionNumber();
                     IsModify = false;
                    txtVatPers.Text = "5";
                    txtTotalFreight.Text = "";
                    txtToPay.Text = "";
                    txtAdvancePaid.Text = "";
                    txtLoadingCharges.Text = "";
                    txtBuyOrdNo.Text = "";
                    txtDealerCode.Text = "";
                    txtEcodeSearch.Text = "";
                    txtEname.Text = "";
                    txtDipatchNo.Text = "";
                    txtTripLRNo.Text = "";
                    txtVehicleNo.Text = "";
                    txtTransporter.Text = "";
                    txtAddress.Text = "";
                    txtRemarks.Text = "";
                    txtQty.Text = "";
                    txtProducts.Text = "";
                    txtDcAmt.Text = "";
                    gvProductDetails.Rows.Clear();
                    cbVehicleType.SelectedIndex = 0;
                    cbDocType.SelectedIndex = 0;
                    strECode = "";
                    strDCode = "";
                    txtDueDays.Text = "";
                    txtDeliveryPoint.Text = "";
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

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            //if (txtEcodeSearch.Text.ToString().Trim().Length > 0)
            //    EcodeSearch();
            //else
            //{
            //    cbEcode.SelectedIndex = -1;
            //}
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (IsModify == true)
            {
                PrintInvoice();
            }
        }
        private void PrintInvoice()
        {
            try
            {

                int invAmount = Convert.ToInt32(txtDcAmt.Text.ToString().Substring(0, txtDcAmt.Text.Length - 3));

                objSQLDB = new SQLDB();
                SqlParameter[] param = new SqlParameter[1];
                DataSet ds = new DataSet();

                param[0] = objSQLDB.CreateParameter("@xNumber", DbType.String, invAmount, ParameterDirection.Input);
                ds = objSQLDB.ExecuteDataSet("Num2Wrd", CommandType.StoredProcedure, param);

                string strAmt = ds.Tables[0].Rows[0][0].ToString();
                CommonData.ViewReport = "DL_INV_REPORT";
                ReportViewer objReportview = new ReportViewer(txtTransactionNo.Text);
                objReportview.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtEcodeSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtEcodeSearch.Text.ToString().Trim().Length > 0)
                EcodeSearch();
            else
            {
                cbEcode.SelectedIndex = -1;
            }
        }

        private void meTransactionDate_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
