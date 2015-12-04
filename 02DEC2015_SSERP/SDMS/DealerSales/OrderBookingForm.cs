using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSTrans;
namespace SDMS
{
    public partial class OrderBookingForm : Form
    {
        private SQLDB objSQLDB = null;
        private InvoiceDB objData = null;
        private  bool blIsCellQty=true;
        private int intCurrentRow = 0;
        private int intCurrentCell = 0;
        private bool IsModifyInvoice = false;
        private string strECode = string.Empty;
        private string strDCode= string.Empty;
        string strTrnNo = "";
        public OrderBookingForm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void OrderBookingForm_Load(object sender, EventArgs e)
        {
            txtDocMonth.Text = CommonData.DocMonth.ToString();
            cbEcode.SelectedIndex = -1;
            cbDealer.SelectedIndex = -1;
            gvProductDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);
            if (CommonData.LogUserId.ToUpper() == "ADMIN")
                txtDocMonth.ReadOnly = false;
            //FillEmployeeData();
            //FillDealerData();
        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            ProductSearchAll PSearch = new ProductSearchAll("OrderBooking");
            PSearch.objFrmOrderBooking = this;
            PSearch.ShowDialog();
        }

        private void btnClearProd_Click(object sender, EventArgs e)
        {
            gvProductDetails.Rows.Clear();
            txtBalAmt.Text = "";
            txtInvAmt.Text = "";
        }

        private void gvProductDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            objData = new InvoiceDB();
            if (e.ColumnIndex == 5)
            {
                if (Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value) != "")
                {
                    string[] strRatePoints = objData.getProductRatePoints(gvProductDetails.Rows[e.RowIndex].Cells["ProductId"].Value.ToString(), Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value.ToString()), Convert.ToDateTime(dtpInvoiceDate.Value.ToString("dd/MM/yyyy"))).Split('@');
                    if (strRatePoints[0] != "")
                    {
                        if (Convert.ToDouble(strRatePoints[0]) > 0)
                            gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value = Convert.ToDouble(strRatePoints[0]).ToString("f");

                        if (Convert.ToDouble(strRatePoints[1]) > 0)
                            gvProductDetails.Rows[e.RowIndex].Cells["DBPoints"].Value = Convert.ToDouble(strRatePoints[1]).ToString("f");
                    }
                    objData = null;
                    if (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value) >= 0 && Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value) >= 0)
                    {
                        gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) * (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value));
                        gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value).ToString("f");
                        gvProductDetails.Rows[e.RowIndex].Cells["Points"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) * (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["DBPoints"].Value));
                        gvProductDetails.Rows[e.RowIndex].Cells["Points"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Points"].Value).ToString("f");
                    }
                }
                else
                    gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value = string.Empty;
            }
            if (e.ColumnIndex == 6)
            {
                if (gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value != gvProductDetails.Rows[e.RowIndex].Cells["DBRate"].Value)
                {
                    if (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value) < Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["DBRate"].Value))
                    {
                        DialogResult result = MessageBox.Show("Rate is Below MRP " + gvProductDetails.Rows[e.RowIndex].Cells["DBRate"].Value + " Rs/-\nDo You Want to Continue with " + gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value + " Rs/-?",
                                               "Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.No)
                        {
                            gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value = gvProductDetails.Rows[e.RowIndex].Cells["DBRate"].Value;
                            return;
                        }
                    }
                    else if (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value) > Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["DBRate"].Value))
                    {
                        DialogResult result = MessageBox.Show("Rate is Above MRP " + gvProductDetails.Rows[e.RowIndex].Cells["DBRate"].Value + " Rs/-\nDo You Want to Continue with " + gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value + " Rs/-?",
                                               "Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.No)
                        {
                            gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value = gvProductDetails.Rows[e.RowIndex].Cells["DBRate"].Value;
                            return;
                        }
                    }
                }

                if (Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value) != "")
                {
                    if (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value) >= 0 && Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value) >= 0)
                    {
                        gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) * (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value));
                        gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value).ToString("f");
                        gvProductDetails.Rows[e.RowIndex].Cells["Points"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value) * (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["DBPoints"].Value));
                        gvProductDetails.Rows[e.RowIndex].Cells["Points"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Points"].Value).ToString("f");
                    }
                }
                else
                    gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value = "0.00";
            }
            if (e.ColumnIndex == 7)
            {
                if ((Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value) == "") || (Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value) == "0"))
                {
                    gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value = string.Empty;
                }


            }
            txtInvAmt.Text = GetInvoiceAmt().ToString("f");
            txtBalAmt.Text = txtInvAmt.Text;
        }

        private void txtAdvanceAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            txt_KeyPress(sender, e);
        }

        private void txtAdvanceAmt_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtInvAmt.Text != "")
            {
                if (txtAdvanceAmt.Text == "")
                    txtAdvanceAmt.Text = "0.00";
                //if (txtReceivedAmt.Text == "")
                //    txtReceivedAmt.Text = "0.00";
                //txtReceivedAmt.Text = Convert.ToDouble(Convert.ToDouble(txtInvAmt.Text) - Convert.ToDouble(txtAdvanceAmt.Text)).ToString("f");
                txtBalAmt.Text = Convert.ToDouble(Convert.ToDouble(txtInvAmt.Text) - (Convert.ToDouble(txtAdvanceAmt.Text))).ToString("f");
            }
            else
            {
                txtInvAmt.Text = GetInvoiceAmt().ToString("f");
            }
        }
        private double GetInvoiceAmt()
        {
            double dbInvAmt = 0;
            for (int i = 0; i < gvProductDetails.Rows.Count; i++)
            {
                if (gvProductDetails.Rows[i].Cells["Amount"].Value.ToString() != "")
                {
                    if (Convert.ToDouble(gvProductDetails.Rows[i].Cells["Amount"].Value.ToString()) >= 1)
                        dbInvAmt += Convert.ToDouble(gvProductDetails.Rows[i].Cells["Amount"].Value);

                }

            }
            //if (txtAdvanceAmt.Text != "0.00" && txtAdvanceAmt.Text != "")
            //    txtReceivedAmt.Text = Convert.ToDouble(Convert.ToDouble(txtInvAmt.Text) - Convert.ToDouble(txtAdvanceAmt.Text)).ToString("f");

            return dbInvAmt;
        }

        private void gvProductDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            blIsCellQty = true;
            intCurrentRow = (((System.Windows.Forms.DataGridView)(sender))).CurrentCell.RowIndex;
            intCurrentCell = (((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex;
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

            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 7)
            {
                if (((System.Windows.Forms.DataGridView)(sender)).CurrentRow.Cells[5].Value + "" != "")
                {
                    TextBox txtAmt = e.Control as TextBox;
                    if (txtAmt != null)
                    {
                        txtAmt.MaxLength = 10;
                        txtAmt.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                    }
                }
                else
                    blIsCellQty = false;
            }

            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 8)
            {
                TextBox txtPoints = e.Control as TextBox;
                if (txtPoints != null)
                {
                    txtPoints.MaxLength = 4;
                    txtPoints.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int intSaved = 0;
            if (CheckData())
            {
                if (SaveOrderBookingHeadData() > 0)
                    intSaved = SaveOrderBookingDeatailData();
                if (intSaved > 0)
                {
                    MessageBox.Show("Data Saved Successfully", "Order Booking", MessageBoxButtons.OK,MessageBoxIcon.Information);
                    IsModifyInvoice = false;
                    txtOrderNo.Text = string.Empty;
                    ClearForm();
                    txtOrderNo.Focus();
                }
                else
                {
                    MessageBox.Show("Data not saved","Order Booking",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
        }
        private void ClearForm()
        {
            txtDocMonth.Text = CommonData.DocMonth.ToUpper();
            dtpInvoiceDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            txtEcodeSearch.Text = "";
            cbEcode.SelectedIndex = -1;
            txtDealerSearch.Text = "";
            cbDealer.SelectedIndex = -1;
            txtInvAmt.Text = "";
            txtAdvanceAmt.Text = "";
            txtBalAmt.Text = "";
            strTrnNo = "";
            gvProductDetails.Rows.Clear();
        }
        private bool CheckData()
        {
            bool blValue = true;
            if (txtAdvanceAmt.Text == "")
                txtAdvanceAmt.Text = "0.00";
            //strCreditSale = "NO";
            if (Convert.ToString(txtOrderNo.Text).Length == 0)
            {
                MessageBox.Show("Enter Order number!", "Order Booking", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtOrderNo.Focus();
                return blValue;
            }
            if (Convert.ToDateTime(Convert.ToDateTime(dtpInvoiceDate.Value).ToString("dd/MM/yyyy")) > Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyyy")))
            {
                MessageBox.Show("Invoice date should be less than to day.", "Order Booking", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                dtpInvoiceDate.Focus();
                return blValue;
            }

            if (cbEcode.SelectedIndex == -1)
            {
                MessageBox.Show("Enter Employee number!", "Order Booking", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtEcodeSearch.Focus();
                return blValue;
            }

            if (cbDealer.SelectedIndex == -1)
            {
                MessageBox.Show("Enter Dealer number!", "Order Booking", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                txtDealerSearch.Focus();
                return blValue;
            }
            bool blInvDtl = false;
            for (int i = 0; i < gvProductDetails.Rows.Count; i++)
            {
                if (Convert.ToString(gvProductDetails.Rows[i].Cells["Rate"].Value) != "" && Convert.ToString(gvProductDetails.Rows[i].Cells["Qty"].Value) != "")
                {
                    blInvDtl = true;
                    break;
                }

            }

            if (blInvDtl == false)
            {
                blValue = false;
                MessageBox.Show("Enter product details", "Order Booking", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (txtInvAmt.Text.ToString() == "")
            {
                MessageBox.Show("Please check Order amount!", "Order Booking", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                gvProductDetails.Focus();
                return blValue;
            }
            if (Convert.ToDouble(txtInvAmt.Text) <  Convert.ToDouble(txtAdvanceAmt.Text))
            {
                DialogResult result = MessageBox.Show("Order amount less than total collection amount, Do you want to save ?",
                                       "Order Booking ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    blValue = true;
                }
                else
                {
                    blValue = false;
                }
            }
            return blValue;
        }

        private int SaveOrderBookingHeadData()
        {
            
            string sqlText = "";

            
            int iRes = 0;
            if (txtAdvanceAmt.Text == "")
                txtAdvanceAmt.Text = "0.00";
            try
            {
                objSQLDB = new SQLDB();
                if (IsModifyInvoice == false)
                {
                    strTrnNo = GenerateTrnNo().ToString();
                    sqlText = "INSERT INTO DL_ORDER_BOOKING_HEAD(DLOH_COMPANY_CODE,DLOH_STATE_CODE,DLOH_BRANCH_CODE,DLOH_FIN_YEAR,DLOH_DOC_MONTH,DLOH_ORDER_NO,DLOH_TRN_NO"
                        + ",DLOH_EORA_CODE,DLOH_ORDER_DATE,DLOH_DEALER_CODE,DLOH_ORDER_AMT,DLOH_ADV_AMT,DLOH_CREATED_BY,DLOH_CREATED_DATE) VALUES('" + CommonData.CompanyCode+"','"+CommonData.StateCode
                        + "','" + CommonData.BranchCode + "','" + CommonData.FinancialYear + "','" + Convert.ToDateTime(dtpInvoiceDate.Value).ToString("MMMyyyy").ToUpper() + "'," + Convert.ToInt32(txtOrderNo.Text) + "," + Convert.ToInt32(strTrnNo)
                        + ","+Convert.ToInt32(strECode)+",'" + Convert.ToDateTime(dtpInvoiceDate.Value).ToString("dd/MMM/yyyy") + "',"+Convert.ToInt32(strDCode)+",'" + txtInvAmt.Text + "','" + txtAdvanceAmt.Text 
                        + "','"+CommonData.LogUserId+"','"+CommonData.CurrentDate+"')";
                }
                else
                {
                    string strDelete = "DELETE DL_ORDER_BOOKING_DETL WHERE DLOD_TRN_NO="+Convert.ToInt32(strTrnNo);
                    //txtBalAmt.Text = "0.00";
                    //txtAdvanceAmt.Text = "0.00";
                    //txtInvAmt.Text = "0.00";
                    objSQLDB.ExecuteSaveData(strDelete);

                    sqlText = "UPDATE DL_ORDER_BOOKING_HEAD SET DLOH_COMPANY_CODE='" + CommonData.CompanyCode + "', DLOH_STATE_CODE='" + CommonData.StateCode
                        + "',DLOH_BRANCH_CODE='" + CommonData.BranchCode + "',DLOH_FIN_YEAR='" + CommonData.FinancialYear
                        + "',DLOH_DOC_MONTH='" + Convert.ToDateTime(dtpInvoiceDate.Value).ToString("MMMyyyy").ToUpper() + "',DLOH_EORA_CODE=" + Convert.ToInt32(strECode) + ",DLOH_ORDER_DATE='" + Convert.ToDateTime(dtpInvoiceDate.Value).ToString("dd/MMM/yyy")
                        + "',DLOH_DEALER_CODE=" + Convert.ToInt32(strDCode) + ",DLOH_ORDER_AMT='" + txtInvAmt.Text + "',DLOH_ADV_AMT='" + txtAdvanceAmt.Text + "',DLOH_MODIFIED_BY='" + CommonData.LogUserId
                        + "',DLOH_MODIFIED_DATE='" + CommonData.CurrentDate + "' WHERE DLOH_TRN_NO="+Convert.ToInt32(strTrnNo);
                }
                objSQLDB = new SQLDB();
                iRes = objSQLDB.ExecuteSaveData(sqlText);
            }
            catch (Exception ex)
            {
                iRes = 0;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objSQLDB = null;

            }
            return iRes;
        }
        private int SaveOrderBookingDeatailData()
        {
            objSQLDB = new SQLDB();
            string sqlText = "";
            string sqlDelete = "";
            int iRes = 0;
            try
            {
                sqlDelete = "DELETE from DL_ORDER_BOOKING_DETL WHERE DLOD_TRN_NO="+Convert.ToInt32(strTrnNo);
                iRes = objSQLDB.ExecuteSaveData(sqlDelete);
                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {
                    if (gvProductDetails.Rows[i].Cells["Rate"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["Qty"].Value.ToString() != "")
                    {
                        sqlText += "INSERT INTO DL_ORDER_BOOKING_DETL(DLOD_TRN_NO,DLOD_SL_NO,DLOD_PRODUCT_ID,DLOD_PRICE,DLOD_QTY,DLOD_AMT) VALUES("+Convert.ToInt32(strTrnNo)
                            + "," + gvProductDetails.Rows[i].Cells["SLNO"].Value + ", '" + gvProductDetails.Rows[i].Cells["ProductID"].Value.ToString().Trim()
                            + "', " + gvProductDetails.Rows[i].Cells["Rate"].Value + ", " + gvProductDetails.Rows[i].Cells["Qty"].Value + ", " + gvProductDetails.Rows[i].Cells["Amount"].Value + ")";
                    }
                }
                iRes = objSQLDB.ExecuteSaveData(sqlText);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objData = null;
            }
            return iRes;
        }
        private int GenerateTrnNo()
        {
            int iTrnNo = 0;
            objSQLDB = new SQLDB();
            DataTable dt = new DataTable();
            string sqlText = "SELECT isNull(MAX(DLOH_TRN_NO)+1,'1')  AS TRNNO FROM DL_ORDER_BOOKING_HEAD ";
            try
            {
                dt = objSQLDB.ExecuteDataSet(sqlText).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    iTrnNo = Convert.ToInt32( dt.Rows[0]["TRNNO"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                dt = null;
                objSQLDB = null;
            }
            return iTrnNo;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsModifyInvoice==true)
                {
                    DialogResult result = MessageBox.Show("Do you want to Delete " + txtOrderNo.Text + " order?",
                                                   "CRM", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        objSQLDB = new SQLDB();
                        string sqlDelete = "DELETE from DL_ORDER_BOOKING_DETL WHERE DLOD_TRN_NO=" + Convert.ToInt32(strTrnNo);
                        int iRes = objSQLDB.ExecuteSaveData(sqlDelete);
                        if (iRes > 0)
                        {
                            sqlDelete = "DELETE from DL_ORDER_BOOKING_HEAD WHERE DLOH_TRN_NO=" + Convert.ToInt32(strTrnNo);
                            iRes = objSQLDB.ExecuteSaveData(sqlDelete);
                            if (iRes > 0)
                            {
                                IsModifyInvoice = false;
                                MessageBox.Show("Order " + txtOrderNo.Text + " Deleted ", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ClearForm();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
            txtOrderNo.Text = "";
        }

        private void txtOrderNo_Validated(object sender, EventArgs e)
        {
            if (Convert.ToString(txtOrderNo.Text).Trim().Length > 0)
            {
                ClearForm();
                //FillInvOrBultInData(0, Convert.ToInt32(txtOrderNo.Text).ToString("00000"));
                FillOrderBookingData(Convert.ToInt32(txtOrderNo.Text));
            }
            else
            {
                ClearForm();
            }
        }
        private void FillOrderBookingData(int iOrderNo)
        {
            Hashtable ht;
            DataTable OrderH;
            DataTable OrderD;
            DataSet ds = new DataSet();
            try
            {
                IsModifyInvoice = false;
                objData = new InvoiceDB();
                ht = objData.GetDLOrderBookingData(CommonData.CompanyCode, CommonData.BranchCode, iOrderNo);

                OrderH = (DataTable)ht["OrderHead"];
                OrderD = (DataTable)ht["OrderDetail"];
                if(OrderH.Rows.Count>0)
                {
                    IsModifyInvoice = true;
                    FillOrderBookingHead(OrderH, OrderD);
                }
            }
            catch (Exception ex)
            {
                objData = null;
                ht = null;
                OrderH = null;
                OrderD = null;
                MessageBox.Show(ex.Message, "Order Booking", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objData = null;
                ht = null;
                OrderH = null;
                OrderD = null;
                ds = null;
            }
        }
        private void FillOrderBookingHead(DataTable dtOrderH,DataTable dtOrderD)
        {
            if(dtOrderH.Rows.Count>0)
            {
                strTrnNo = dtOrderH.Rows[0]["DLOH_TRN_NO"] + "";
                txtEcodeSearch.Text = dtOrderH.Rows[0]["DLOH_EORA_CODE"] + "";
                txtDealerSearch.Text = dtOrderH.Rows[0]["DLOH_DEALER_CODE"] + "";
                EcodeSearch();
                DealerSearch();
                txtDocMonth.Text = dtOrderH.Rows[0]["DLOH_DOC_MONTH"] + "";
                dtpInvoiceDate.Value = Convert.ToDateTime(Convert.ToDateTime(dtOrderH.Rows[0]["DLOH_ORDER_DATE"] + "").ToString("dd/MMM/yyyy"));
                cbEcode.SelectedValue = dtOrderH.Rows[0]["DLOH_EORA_CODE"] + "";
                cbDealer.SelectedValue = dtOrderH.Rows[0]["DLOH_DEALER_CODE"] + "";
                txtInvAmt.Text = dtOrderH.Rows[0]["DLOH_ORDER_AMT"] + "";
                txtAdvanceAmt.Text = dtOrderH.Rows[0]["DLOH_ADV_AMT"] + "";
                txtBalAmt.Text = (Convert.ToDouble(txtInvAmt.Text) - Convert.ToDouble(txtAdvanceAmt.Text)).ToString();
                FillOrderBookingDetl(dtOrderD);
            }
        }
        private void FillOrderBookingDetl(DataTable dtOrderD)
        {
            if (dtOrderD.Rows.Count > 0)
            {
                try
                {
                    gvProductDetails.Rows.Clear();
                    for (int i = 0; i < dtOrderD.Rows.Count; i++)
                    {
                        if (Convert.ToDouble(dtOrderD.Rows[i]["DLOD_AMT"]) > 0)
                        {
                            DataGridViewRow tempRow = new DataGridViewRow();
                            DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                            cellSLNO.Value = dtOrderD.Rows[i]["DLOD_SL_NO"];
                            tempRow.Cells.Add(cellSLNO);



                            DataGridViewCell cellProductId = new DataGridViewTextBoxCell();
                            //cellProductId.Value = dt.Rows[i]["cellProductId"];
                            cellProductId.Value = dtOrderD.Rows[i]["DLOD_PRODUCT_ID"];
                            tempRow.Cells.Add(cellProductId);

                            DataGridViewCell cellProductName = new DataGridViewTextBoxCell();
                            //cellProductName.Value = dt.Rows[i]["ProductName"];
                            cellProductName.Value = dtOrderD.Rows[i]["ProductName"];
                            tempRow.Cells.Add(cellProductName);


                            DataGridViewCell cellCategoryName = new DataGridViewTextBoxCell();
                            //cellCategoryName.Value = dt.Rows[i]["CategoryName"];
                            cellCategoryName.Value = dtOrderD.Rows[i]["categoryName"];
                            tempRow.Cells.Add(cellCategoryName);

                            DataGridViewCell cellDesc = new DataGridViewTextBoxCell();
                            //cellCategoryName.Value = dt.Rows[i]["CategoryName"];
                            cellDesc.Value = dtOrderD.Rows[i]["categoryName"];
                            tempRow.Cells.Add(cellDesc);

                            DataGridViewCell cellQTY = new DataGridViewTextBoxCell();
                            cellQTY.Value = dtOrderD.Rows[i]["DLOD_QTY"];
                            tempRow.Cells.Add(cellQTY);

                            DataGridViewCell cellprodMrp = new DataGridViewTextBoxCell();
                            cellprodMrp.Value = Convert.ToDouble(dtOrderD.Rows[i]["DLOD_PRICE"]).ToString("f");
                            //cellprodMrp.Value = dt.Rows[i]["BulkMRP"];
                            tempRow.Cells.Add(cellprodMrp);

                            DataGridViewCell cellAmt = new DataGridViewTextBoxCell();
                            cellAmt.Value = Convert.ToDouble(dtOrderD.Rows[i]["DLOD_AMT"]).ToString("f");
                            tempRow.Cells.Add(cellAmt);

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
                    dtOrderD = null;
                }
            }
        }
        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text.ToString().Trim().Length > 0)
                EcodeSearch();
            else
            {
                cbEcode.SelectedIndex = -1;
            }
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

        private void txtDealerSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtDealerSearch.Text.ToString().Trim().Length > 0)
                DealerSearch();
            else
            {
                cbDealer.SelectedIndex = -1;
            }
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

                param[0] = objSQLDB.CreateParameter("@xCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLDB.CreateParameter("@xDealerName", DbType.String, txtDealerSearch.Text, ParameterDirection.Input);
                param[2] = objSQLDB.CreateParameter("@xFirmName", DbType.String, sFirmName, ParameterDirection.Input);
                ds = objSQLDB.ExecuteDataSet("DL_GetDealersListSearch", CommandType.StoredProcedure, param);

                DataTable dtDealer = ds.Tables[0];

                if (dtDealer.Rows.Count > 0)
                {
                    cbDealer.DataSource = dtDealer;
                    cbDealer.DisplayMember = "DealerName";
                    cbDealer.ValueMember = "DealerCode";
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

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == false)
                e.Handled = true;

            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void txtOrderNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            txt_KeyPress(sender,e);
        }

        private void cbEcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEcode.SelectedIndex > -1)
            {
                strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
            }
        }

        private void cbDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDealer.SelectedIndex > -1)
            {
                strDCode = ((System.Data.DataRowView)(cbDealer.SelectedItem)).Row.ItemArray[0].ToString();
            }
        }
    }
}
