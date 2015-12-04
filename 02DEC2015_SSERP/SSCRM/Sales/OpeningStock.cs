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
    public partial class OpeningStock : Form
    {
        private SQLDB objSQLDB = null;
        private InvoiceDB objData = null;
        private string strFormerid = string.Empty;        
        private string strVillage = string.Empty;
        private string strDateOfBirth = string.Empty;
        private string strMarriageDate = string.Empty;
        public string strStateCode = string.Empty;
        private string strBranchCode = string.Empty;
        private string strECode = string.Empty;
        private bool blIsCellQty = true;
        private int intCurrentRow = 0;
        private int intCurrentCell = 0;
        private int strForm = 0;
        public OpeningStock()
        {
            InitializeComponent();
        }
        public OpeningStock(int sForm)
        {
            InitializeComponent();
            strForm = sForm;
        }

        private void OpeningStock_Load(object sender, EventArgs e)
        {
            lblDocMonth.Text = CommonData.DocMonth.ToString();
            if (strForm == 1 || strForm == 2)
            {
                cbEcode.Visible = false;
                txtEcodeSearch.Visible = false;
                strECode = "0";
                gvProductDetails.Columns[4].HeaderText = "Good";
                gvProductDetails.Columns[5].HeaderText = "Damage";
            }
            else
            {
                cbEcode.Visible = true;
                txtEcodeSearch.Visible = true;
                FillEmployeeData();
                cbEcode.SelectedIndex = -1;
            }
            gvProductDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);
            FillProductsDataToGrid();
        }

        private void FillEmployeeData()
        {
            objData = new InvoiceDB();
            DataSet dsEmp = null;
            cbEcode.DataSource = null;
            cbEcode.Items.Clear();
            try
            {
                dsEmp = objData.GetAllEcodeList(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth);
                DataTable dtEmp = dsEmp.Tables[0];
                if (dtEmp.Rows.Count > 0)
                {
                    cbEcode.DataSource = dtEmp;
                    cbEcode.DisplayMember = "ENAME";
                    cbEcode.ValueMember = "ECODE";
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
                //objData = null;
                Cursor.Current = Cursors.Default;
            }

        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text.ToString().Trim().Length > 4)
                EcodeSearch();
            else
                FillEmployeeData();
        }

        private void EcodeSearch()
        {
            objData = new InvoiceDB();
            DataSet dsEmp = new DataSet();
            objSQLDB = new SQLDB();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                cbEcode.DataSource = null;
                cbEcode.Items.Clear();
                dsEmp = objSQLDB.ExecuteDataSet("select * from (SELECT ECODE, cast(ECODE as varchar)+'-'+MEMBER_NAME ENAME, company_code Company "+
                                                "FROM EORA_MASTER) Ecodes WHERE upper(ENAME) like upper('%" + txtEcodeSearch.Text.ToString() + "%') Order by ENAME");
                //dsEmp = objData.InvLevelAllEcodeSearch_Get(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth, txtEcodeSearch.Text.ToString());
                DataTable dtEmp = dsEmp.Tables[0];
                if (dtEmp.Rows.Count > 0)
                {
                    cbEcode.DataSource = dtEmp;
                    cbEcode.DisplayMember = "ENAME";
                    cbEcode.ValueMember = "ECODE";
                    FillProductsDataToGrid();
                }
                else
                {
                    strECode = "";
                    gvProductDetails.Rows.Clear();
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
                objData = null;
                Cursor.Current = Cursors.Default;
            }

        }

        private void btnClearProd_Click(object sender, EventArgs e)
        {
            gvProductDetails.Rows.Clear();
        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            ProductSearchAll PSearch = new ProductSearchAll("OpeningStock");
            PSearch.objFrmOpenStock = this;
            PSearch.ShowDialog();
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

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46) || (blIsCellQty == false))
            {
                e.Handled = true;
                return;
            }

            //to allow decimals only teak plant
            if (intCurrentCell == 5 && gvProductDetails.Rows[intCurrentRow].Cells["MainProduct"].Value.ToString().Contains("TEAK"))
            {
                if (e.KeyChar == 46)
                {
                    if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                        e.Handled = true;
                }
            }
            else if (intCurrentCell == 5 && e.KeyChar == 46)
            {
                e.Handled = true;
                return;
            }
            // checks to make sure only 1 decimal is allowed
            else if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void gvProductDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridView dgv = (DataGridView)sender;
                if (e.ColumnIndex == 5 || e.ColumnIndex == 7 || e.ColumnIndex == 8)
                {
                    DataGridViewCell textBoxCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    if (textBoxCell != null)
                    {
                        gvProductDetails.CurrentCell = textBoxCell;
                        dgv.BeginEdit(true);
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int intRec = 0;
            if (Checkdata())
            {
                intRec = SaveOpeningStock();
                if (intRec > 0)
                {
                    MessageBox.Show("Data Saved Successfully", "Opening Stock", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Data not Saved", "Opening Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private int SaveOpeningStock()
        {
            objSQLDB = new SQLDB();
            string sqlText = "";
            string sqlDelete = "";
            int intRec = 0;
            try
            {
                if (strForm == 1 || strForm == 2)
                {
                    sqlDelete = "DELETE FROM OPENING_STOCK" +
                                    " WHERE OS_COMPANY_CODE='" + CommonData.CompanyCode +
                                        "' AND OS_BRANCH_CODE='" + CommonData.BranchCode +
                                        "' AND OS_DOCUMENT_MONTH='" + CommonData.DocMonth + "'";
                                        
                }
                else
                {
                    sqlDelete = "DELETE FROM OPENING_STOCK" +
                                    " WHERE OS_COMPANY_CODE='" + CommonData.CompanyCode +
                                        "' AND OS_BRANCH_CODE='" + CommonData.BranchCode +
                                        "' AND OS_DOCUMENT_MONTH='" + CommonData.DocMonth +
                                        "'  AND OS_EORA_CODE='" + strECode + "'";
                }

                intRec = objSQLDB.ExecuteSaveData(sqlDelete);
                sqlText = "";
                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {
                    if ((gvProductDetails.Rows[i].Cells["Qty"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["Qty"].Value.ToString() != "0") || (gvProductDetails.Rows[i].Cells["Rate"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["Rate"].Value.ToString() != "0"))
                    {
                        sqlText += "INSERT INTO OPENING_STOCK(OS_COMPANY_CODE, OS_BRANCH_CODE, OS_EORA_CODE, OS_FIN_YEAR, OS_DOCUMENT_MONTH, OS_PRODUCT_ID, OS_OPENING_STOCK, OS_SHORTAGE, OS_REMARKS, OS_CREATED_BY, OS_CREATED_DATE)" +
                                    " VALUES ('" + CommonData.CompanyCode + "', '" + CommonData.BranchCode + "', " + strECode + ", '" + CommonData.FinancialYear + "','" + CommonData.DocMonth +
                                    "', '" + gvProductDetails.Rows[i].Cells["ProductID"].Value.ToString().Trim() + "','" + gvProductDetails.Rows[i].Cells["Qty"].Value +
                                    "','" + gvProductDetails.Rows[i].Cells["Rate"].Value + "','" + gvProductDetails.Rows[i].Cells["Remarks"].Value + 
                                    "','" + CommonData.LogUserId + "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") + "'); ";

                    }

                }
                intRec = 0;
                if (sqlText.Length > 10)
                    intRec = objSQLDB.ExecuteSaveData(sqlText);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objSQLDB = null;
            }
            return intRec;

        }

        private bool Checkdata()
        {
            bool blValue = true;

            if (cbEcode.SelectedIndex == -1 && strForm == 0)
            {
                MessageBox.Show("Enter Employee number!", "Opening Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                cbEcode.Focus();
                return blValue;
            }
            if (gvProductDetails.Rows.Count == 0)
            {
                MessageBox.Show("Enter Product Details!", "Opening Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                cbEcode.Focus();
                return blValue;
            }
            return blValue;
        }

        private void cbEcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEcode.SelectedIndex > -1)
            {
                strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
            }

            if (strECode != "")
                try { Convert.ToInt32(strECode); }
                catch { strECode = "0"; }
            else
                strECode = "0";

            FillProductsDataToGrid();
        }

        private void FillProductsDataToGrid()
        {
            objSQLDB = new SQLDB();
            string sqlText = "";
            DataTable dt = null;
            gvProductDetails.Rows.Clear();
            if (strECode != "")
            {
                try
                {
                    sqlText = "SELECT OS_COMPANY_CODE,OS_BRANCH_CODE,OS_EORA_CODE,OS_FIN_YEAR,OS_DOCUMENT_MONTH,OS_PRODUCT_ID,PM_PRODUCT_NAME,CATEGORY_NAME,isnull(OS_OPENING_STOCK,0) OS_OPENING_STOCK,isnull(OS_SHORTAGE,0) OS_SHORTAGE,OS_REMARKS" +
                        " FROM OPENING_STOCK  INNER JOIN PRODUCT_MAS ON PM_PRODUCT_ID = OS_PRODUCT_ID INNER JOIN CATEGORY_MASTER ON CATEGORY_ID = PM_CATEGORY_ID" +
                        " WHERE OS_DOCUMENT_MONTH = '" + CommonData.DocMonth + "' AND OS_EORA_CODE = '" + strECode +
                        "' AND OS_BRANCH_CODE = '" + CommonData.BranchCode + "' AND OS_COMPANY_CODE = '" + CommonData.CompanyCode + "'; ";

                    dt = objSQLDB.ExecuteDataSet(sqlText).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataGridViewRow tempRow = new DataGridViewRow();
                            DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                            cellSLNO.Value = i + 1;
                            tempRow.Cells.Add(cellSLNO);

                            DataGridViewCell cellProductID = new DataGridViewTextBoxCell();
                            cellProductID.Value = dt.Rows[i]["OS_PRODUCT_ID"];
                            tempRow.Cells.Add(cellProductID);

                            DataGridViewCell cellProduct = new DataGridViewTextBoxCell();
                            cellProduct.Value = dt.Rows[i]["PM_PRODUCT_NAME"];
                            tempRow.Cells.Add(cellProduct);

                            DataGridViewCell cellCategory = new DataGridViewTextBoxCell();
                            cellCategory.Value = dt.Rows[i]["CATEGORY_NAME"];
                            tempRow.Cells.Add(cellCategory);

                            DataGridViewCell cellQty = new DataGridViewTextBoxCell();
                            cellQty.Value = dt.Rows[i]["OS_OPENING_STOCK"]+"";
                            tempRow.Cells.Add(cellQty);

                            DataGridViewCell cellShortage = new DataGridViewTextBoxCell();
                            cellShortage.Value = dt.Rows[i]["OS_SHORTAGE"] + "";
                            tempRow.Cells.Add(cellShortage);

                            DataGridViewCell cellRemarks = new DataGridViewTextBoxCell();
                            cellRemarks.Value = dt.Rows[i]["OS_REMARKS"] + "";
                            tempRow.Cells.Add(cellRemarks);

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
                    //objData = null;
                }
            }
            else if (strForm == 1 || strForm == 2)
            {
                try
                {
                    sqlText = "SELECT OS_COMPANY_CODE,OS_BRANCH_CODE,OS_EORA_CODE,OS_FIN_YEAR,OS_DOCUMENT_MONTH,OS_PRODUCT_ID,PM_PRODUCT_NAME,CATEGORY_NAME,isnull(OS_OPENING_STOCK,0) OS_OPENING_STOCK,isnull(OS_SHORTAGE,0) OS_SHORTAGE,OS_REMARKS" +
                        " FROM OPENING_STOCK  INNER JOIN PRODUCT_MAS ON PM_PRODUCT_ID = OS_PRODUCT_ID INNER JOIN CATEGORY_MASTER ON CATEGORY_ID = PM_CATEGORY_ID" +
                        " WHERE OS_DOCUMENT_MONTH = '" + CommonData.DocMonth + //"' AND OS_EORA_CODE = '" + strECode +
                        "' AND OS_BRANCH_CODE = '" + CommonData.BranchCode + "' AND OS_COMPANY_CODE = '" + CommonData.CompanyCode + "'; ";

                    dt = objSQLDB.ExecuteDataSet(sqlText).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataGridViewRow tempRow = new DataGridViewRow();
                            DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                            cellSLNO.Value = i + 1;
                            tempRow.Cells.Add(cellSLNO);

                            DataGridViewCell cellProductID = new DataGridViewTextBoxCell();
                            cellProductID.Value = dt.Rows[i]["OS_PRODUCT_ID"];
                            tempRow.Cells.Add(cellProductID);

                            DataGridViewCell cellProduct = new DataGridViewTextBoxCell();
                            cellProduct.Value = dt.Rows[i]["PM_PRODUCT_NAME"];
                            tempRow.Cells.Add(cellProduct);

                            DataGridViewCell cellCategory = new DataGridViewTextBoxCell();
                            cellCategory.Value = dt.Rows[i]["CATEGORY_NAME"];
                            tempRow.Cells.Add(cellCategory);

                            DataGridViewCell cellQty = new DataGridViewTextBoxCell();
                            cellQty.Value = dt.Rows[i]["OS_OPENING_STOCK"] + "";
                            tempRow.Cells.Add(cellQty);

                            DataGridViewCell cellShortage = new DataGridViewTextBoxCell();
                            cellShortage.Value = dt.Rows[i]["OS_SHORTAGE"] + "";
                            tempRow.Cells.Add(cellShortage);

                            DataGridViewCell cellRemarks = new DataGridViewTextBoxCell();
                            cellRemarks.Value = dt.Rows[i]["OS_REMARKS"] + "";
                            tempRow.Cells.Add(cellRemarks);

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
                    //objData = null;
                }
            }
                
        }
    }
}
