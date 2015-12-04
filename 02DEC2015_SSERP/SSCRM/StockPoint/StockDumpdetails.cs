using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
namespace SSCRM
{
    public partial class StockDumpdetails : Form
    {
        SQLDB objSQLDB;
        delegate void SetComboBoxCellType(int iRowIndex);
        bool bIsComboBox = false;
        string sGRNNumber = "";
        string sGRNProductID = "";
        string sGRNProductQty = "";
        string sVtoG = "";
        public StockDumpdetails()
        {
            InitializeComponent();
        }
        private void StockDumpdetails_Load(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            string sqlQry = "SELECT COUNT(*) FROM SP_HAMELI_CHARGES WHERE SPHC_BRANCH_CODE='" + CommonData.BranchCode + "'";
            DataSet dsExists = objSQLDB.ExecuteDataSet(sqlQry);
            if (dsExists.Tables[0].Rows[0][0].ToString() == "0")
            {
                MessageBox.Show("Hamali/Loading Charges not available. please enter hameli/Loading charges detials.", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //return;
            }
            objSQLDB = null;
            txtCompany.Text = CommonData.CompanyName;
            txtStockPoint.Text = CommonData.BranchName;
            dtTrans.Value = System.DateTime.Now;
            dtFrom.Value = System.DateTime.Now;
            dtTodate.Value = System.DateTime.Now;
            txtBal_Qty.Text = "0";
            txtDC_Qty.Text = "0";
            txtGrn_Qty.Text = "0";
            txtVtoG.Text = "0";
            txtTrnNo.Text = GenerateNewTrnNo().ToString();
            gvDCDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);
            gvGRNDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private int GenerateNewTrnNo()
        {
            int iMax = 0;
            objSQLDB = new SQLDB();
            string sSqlText = "SELECT isnull(MAX(SPSDG_TRN_NUMBER),0)+1 FROM SP_ST_DUMP_GRN WHERE SPSDG_BRANCH_CODE = '" + CommonData.BranchCode + "'";
            iMax = Convert.ToInt32(objSQLDB.ExecuteDataSet(sSqlText).Tables[0].Rows[0][0].ToString());
            objSQLDB = null;
            return iMax;
        }
        private void btnDisplay_Click(object sender, EventArgs e)
        {
            string strSql = "SELECT SPGH_GRN_DATE,SPGD_COMPANY_CODE,SPGD_BRANCH_CODE,SPGD_FIN_YEAR,SPGD_DOCUMENT_MONTH,SPGD_GRN_NUMBER,SPGD_PRODUCT_ID," +
                "(SELECT B.SPGD_REC_QTY-ISNULL(SUM(SPDDC_ISS_QTY),0) FROM SP_ST_DUMP_DC WHERE SPDDC_GRN_NUMBER=A.SPGH_GRN_NUMBER) AS SPGD_REC_QTY,PM_PRODUCT_NAME,BRANCH_NAME FROM " +
                "SP_GRN_HEAD A INNER JOIN SP_GRN_DETL B ON A.SPGH_COMPANY_CODE=B.SPGD_COMPANY_CODE AND A.SPGH_BRANCH_CODE=B.SPGD_BRANCH_CODE AND A.SPGH_FIN_YEAR=B.SPGD_FIN_YEAR" +
                " AND A.SPGH_GRN_NUMBER=B.SPGD_GRN_NUMBER INNER JOIN PRODUCT_MAS C ON B.SPGD_PRODUCT_ID=C.PM_PRODUCT_ID INNER JOIN BRANCH_MAS D ON B.SPGD_BRANCH_CODE=D.BRANCH_CODE " +
                " WHERE SPGH_GRN_DATE BETWEEN '" + Convert.ToDateTime(dtFrom.Value).ToString("dd/MMM/yyyy") + "' AND '" + Convert.ToDateTime(dtTodate.Value).ToString("dd/MMM/yyyy") +
                "' AND SPGD_BRANCH_CODE='" + CommonData.BranchCode + "' AND (SELECT B.SPGD_REC_QTY-ISNULL(SUM(SPDDC_ISS_QTY),0) FROM SP_ST_DUMP_DC WHERE SPDDC_GRN_NUMBER=A.SPGH_GRN_NUMBER) > 0 ";
            objSQLDB = new SQLDB();
            DataSet ds = objSQLDB.ExecuteDataSet(strSql);
            objSQLDB = null;
            GetGRNData(ds.Tables[0]);
        }
        public void GetGRNData(DataTable dt)
        {
            int intRow = 1;
            gvGRNDetails.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellBranch = new DataGridViewTextBoxCell();
                cellBranch.Value = dt.Rows[i]["BRANCH_NAME"];
                tempRow.Cells.Add(cellBranch);

                DataGridViewCell cellGrnNo = new DataGridViewTextBoxCell();
                cellGrnNo.Value = dt.Rows[i]["SPGD_GRN_NUMBER"];
                tempRow.Cells.Add(cellGrnNo);

                DataGridViewCell cellGrnDt = new DataGridViewTextBoxCell();
                cellGrnDt.Value = Convert.ToDateTime(dt.Rows[i]["SPGH_GRN_DATE"]).ToString("dd/MM/yyyy");
                tempRow.Cells.Add(cellGrnDt);

                DataGridViewCell cellProduct = new DataGridViewTextBoxCell();
                cellProduct.Value = dt.Rows[i]["PM_PRODUCT_NAME"];
                tempRow.Cells.Add(cellProduct);

                DataGridViewCell cellQty = new DataGridViewTextBoxCell();
                cellQty.Value = dt.Rows[i]["SPGD_REC_QTY"];
                tempRow.Cells.Add(cellQty);

                DataGridViewCell cellProductID = new DataGridViewTextBoxCell();
                cellProductID.Value = dt.Rows[i]["SPGD_PRODUCT_ID"];
                tempRow.Cells.Add(cellProductID);

                DataGridViewCell cellBranchCode = new DataGridViewTextBoxCell();
                cellBranchCode.Value = dt.Rows[i]["SPGD_BRANCH_CODE"];
                tempRow.Cells.Add(cellBranchCode);

                intRow = intRow + 1;
                gvGRNDetails.Rows.Add(tempRow);
            }
        }
        private void gvGRNDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == gvGRNDetails.Columns["Check"].Index)
                {
                    bool cbchecked = (bool)gvGRNDetails.Rows[e.RowIndex].Cells["Check"].EditedFormattedValue;
                    if (cbchecked == false)
                    {
                        sGRNNumber = gvGRNDetails.Rows[e.RowIndex].Cells[2].Value.ToString();
                        sGRNProductID = gvGRNDetails.Rows[e.RowIndex].Cells[6].Value.ToString();
                        sGRNProductQty = gvGRNDetails.Rows[e.RowIndex].Cells[5].Value.ToString();
                        DataTable dt = new DataTable();
                        string strHArta = "SELECT ISNULL(SPHC_VtoG,0) FROM SP_HAMELI_CHARGES WHERE SPHC_BRANCH_CODE='" + CommonData.BranchCode + "' AND SPHC_PRODUCT_ID='" + sGRNProductID + "'";
                        objSQLDB = new SQLDB();
                        dt = objSQLDB.ExecuteDataSet(strHArta).Tables[0];
                        if (dt.Rows.Count > 0)
                            sVtoG = dt.Rows[0][0].ToString();
                        else
                            sVtoG = "0";
                        dt = null;
                        gvDCDetails.Rows.Clear();
                        GetDCData(sGRNProductID, sGRNNumber);
                    }
                }
            }
            if (e.ColumnIndex == 8)
            {
                if (e.RowIndex != -1)
                {
                    foreach (DataGridViewRow row in gvGRNDetails.Rows)
                    {
                        row.Cells[e.ColumnIndex].Value = false;
                    }
                }
            }
        }
        public void GetDCData(string ProductID, string GRNNumber)
        {
            string strSql = "SELECT SPDH_TRN_DATE,SPDD_COMPANY_CODE,SPDD_BRANCH_CODE,SPDD_FIN_YEAR,SPDD_TRN_NUMBER,SPDD_PRODUCT_ID," +
                " SPDD_ISS_QTY-ISNULL((SELECT ISNULL(SPDDC_ISS_QTY,0) FROM SP_ST_DUMP_DC WHERE SPDDC_TRN_NUMBER=A.SPDH_TRN_NUMBER AND " +
                " SPDDC_GRN_NUMBER='" + GRNNumber + "'),0) AS SPDD_ISS_QTY,PM_PRODUCT_NAME,BRANCH_NAME FROM SP_DC_HEAD A INNER JOIN SP_DC_DETL B ON A.SPDH_COMPANY_CODE=B.SPDD_COMPANY_CODE AND " +
                " A.SPDH_BRANCH_CODE=B.SPDD_BRANCH_CODE AND A.SPDH_FIN_YEAR=B.SPDD_FIN_YEAR AND A.SPDH_TRN_NUMBER=B.SPDD_TRN_NUMBER INNER JOIN PRODUCT_MAS C ON B.SPDD_PRODUCT_ID=C.PM_PRODUCT_ID " +
                " INNER JOIN BRANCH_MAS D ON B.SPDD_BRANCH_CODE=D.BRANCH_CODE WHERE SPDD_BRANCH_CODE='" + CommonData.BranchCode + "' AND SPDD_PRODUCT_ID='" + ProductID + "' AND SPDH_TRN_DATE BETWEEN '" + Convert.ToDateTime(dtFrom.Value).ToString("dd/MMM/yyyy") + "' AND '" + Convert.ToDateTime(dtTodate.Value).ToString("dd/MMM/yyyy") + "'";
            objSQLDB = new SQLDB();
            DataSet ds = objSQLDB.ExecuteDataSet(strSql);
            objSQLDB = null;
            GetDCData(ds.Tables[0]);
        }
        public void GetDCData(DataTable dt)
        {
            int intRow = 1;
            gvDCDetails.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellType = new DataGridViewTextBoxCell();
                cellType.Value = "";
                tempRow.Cells.Add(cellType);

                DataGridViewCell cellPName = new DataGridViewTextBoxCell();
                cellPName.Value = dt.Rows[i]["PM_PRODUCT_NAME"];
                tempRow.Cells.Add(cellPName);

                DataGridViewCell cellQty = new DataGridViewTextBoxCell();
                cellQty.Value = dt.Rows[i]["SPDD_ISS_QTY"];
                tempRow.Cells.Add(cellQty);

                DataGridViewCell cellGrnNo = new DataGridViewTextBoxCell();
                cellGrnNo.Value = "";
                tempRow.Cells.Add(cellGrnNo);
                DataGridViewCell celleQty = new DataGridViewTextBoxCell();
                celleQty.Value = "";
                tempRow.Cells.Add(celleQty);

                DataGridViewCell cellProdcut = new DataGridViewTextBoxCell();
                cellProdcut.Value = dt.Rows[i]["SPDD_PRODUCT_ID"];
                tempRow.Cells.Add(cellProdcut);

                DataGridViewCell cellTrnNo = new DataGridViewTextBoxCell();
                cellTrnNo.Value = dt.Rows[i]["SPDD_TRN_NUMBER"];
                tempRow.Cells.Add(cellTrnNo);

                intRow = intRow + 1;
                gvDCDetails.Rows.Add(tempRow);
            }
        }
        private void gvDCDetails_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            SetComboBoxCellType objChangeCellType = new SetComboBoxCellType(ChangeCellToComboBox);
            if (e.ColumnIndex == this.gvDCDetails.Columns[1].Index)
            {
                this.gvDCDetails.BeginInvoke(objChangeCellType, e.RowIndex);
                bIsComboBox = false;
            }
        }
        private void ChangeCellToComboBox(int iRowIndex)
        {
            if (bIsComboBox == false)
            {
                int cmdSelectIndex = 0;
                DataGridViewComboBoxCell dgComboCell = new DataGridViewComboBoxCell();
                dgComboCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                DataTable dt = new DataTable();
                dt.Columns.Add("Value", typeof(string));
                dt.Columns.Add("Text", typeof(string));
                dt.Rows.Add(new Object[] { "1", "V to G" });
                dt.Rows.Add(new Object[] { "2", "G to V" });
                dt.Rows.Add(new Object[] { "3", "V to V" });
                if (gvDCDetails.Rows[iRowIndex].Cells[gvDCDetails.CurrentCell.ColumnIndex].Value.ToString() != "")
                    cmdSelectIndex = Convert.ToInt32(gvDCDetails.Rows[iRowIndex].Cells[gvDCDetails.CurrentCell.ColumnIndex].Value);
                //else
                //    cmdSelectIndex = 0;
                dgComboCell.DataSource = dt;
                dgComboCell.ValueMember = "Value";
                dgComboCell.DisplayMember = "Text";
                
                gvDCDetails.Rows[iRowIndex].Cells[gvDCDetails.CurrentCell.ColumnIndex] = dgComboCell;
                //((DataGridViewComboBoxCell)gvDCDetails.Rows[iRowIndex].Cells[gvDCDetails.CurrentCell.ColumnIndex]). = true;
                bIsComboBox = true;
            }
        }
        private void gvDCDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 0)
            {
                if (gvDCDetails.Rows[e.RowIndex].Cells[1].Value == "")
                {
                    gvDCDetails.Rows[e.RowIndex].Cells[5].Value = "";
                    gvDCDetails.Rows[e.RowIndex].Cells[8].Value = "";
                    MessageBox.Show("Please select Dump Type.", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (gvDCDetails.Rows[e.RowIndex].Cells[1].Value != "2")
                    gvDCDetails.Rows[e.RowIndex].Cells[4].Value = sGRNNumber;
                else
                    gvDCDetails.Rows[e.RowIndex].Cells[4].Value = "";
                if (gvDCDetails.Rows[e.RowIndex].Cells[5].Value != "")
                {
                    if (gvDCDetails.Rows[e.RowIndex].Cells[1].Value != "2")
                    {
                        if (Convert.ToDecimal(gvDCDetails.Rows[e.RowIndex].Cells[5].Value) > Convert.ToDecimal(gvDCDetails.Rows[e.RowIndex].Cells[3].Value))
                        {
                            gvDCDetails.Rows[e.RowIndex].Cells[5].Value = "";
                            gvDCDetails.Rows[e.RowIndex].Cells[8].Value = "";
                            return;
                        }
                    }
                    objSQLDB = new SQLDB();
                    string strHArta = "";
                    if (gvDCDetails.Rows[e.RowIndex].Cells[1].Value == "1")
                        strHArta = "SELECT SPHC_VtoG FROM SP_HAMELI_CHARGES WHERE SPHC_BRANCH_CODE='" + CommonData.BranchCode + "' AND SPHC_PRODUCT_ID='" + gvDCDetails.Rows[e.RowIndex].Cells[6].Value + "'";
                    else if (gvDCDetails.Rows[e.RowIndex].Cells[1].Value == "2")
                        strHArta = "SELECT SPHC_GtoV FROM SP_HAMELI_CHARGES WHERE SPHC_BRANCH_CODE='" + CommonData.BranchCode + "' AND SPHC_PRODUCT_ID='" + gvDCDetails.Rows[e.RowIndex].Cells[6].Value + "'";
                    else
                        strHArta = "SELECT SPHC_VtoV FROM SP_HAMELI_CHARGES WHERE SPHC_BRANCH_CODE='" + CommonData.BranchCode + "' AND SPHC_PRODUCT_ID='" + gvDCDetails.Rows[e.RowIndex].Cells[6].Value + "'";
                    DataSet dsHLrt = objSQLDB.ExecuteDataSet(strHArta);
                    objSQLDB = null;
                    if (dsHLrt.Tables[0].Rows.Count > 0)
                    {
                        gvDCDetails.Rows[e.RowIndex].Cells[8].Value = dsHLrt.Tables[0].Rows[0][0].ToString();
                        decimal dAmnt = Convert.ToDecimal(gvDCDetails.Rows[e.RowIndex].Cells[8].Value) * Convert.ToDecimal(gvDCDetails.Rows[e.RowIndex].Cells[5].Value);
                        gvDCDetails.Rows[e.RowIndex].Cells[9].Value = dAmnt.ToString("0.00");
                    }
                    GetTotalQuantity();
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            string sqlQry1 = "SELECT COUNT(*) FROM SP_HAMELI_CHARGES WHERE SPHC_BRANCH_CODE='" + CommonData.BranchCode + "'";
            DataSet dsExists = objSQLDB.ExecuteDataSet(sqlQry1);
            if (dsExists.Tables[0].Rows[0][0].ToString() == "0")
            {
                MessageBox.Show("Hamali/Loading Charges not available. please enter hameli/Loading charges detials.", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (Convert.ToInt32(txtGrn_Qty.Text) < Convert.ToInt32(txtDC_Qty.Text))
            {
                MessageBox.Show("Please Enter Quantity is bellow GRN Quanitity", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string sqlQry = " INSERT INTO SP_ST_DUMP_GRN(SPSDG_COMPANY_CODE,SPSDG_BRANCH_CODE,SPSDG_FIN_YEAR,SPSDG_TRN_NUMBER,SPSDG_TRN_DATE,SPSDG_GRN_NUMBER,SPSDG_PRODUCT_ID,SPSDG_REC_QTY) VALUES (" +
                "'" + CommonData.CompanyCode + "','" + CommonData.BranchCode + "','" + CommonData.FinancialYear + "'," + txtTrnNo.Text + ",'" + Convert.ToDateTime(dtTrans.Value).ToString("dd/MMM/yyyy") + "','" + sGRNNumber + "','" + sGRNProductID + "'," + sGRNProductQty + ")";
            for (int i = 0; i < gvDCDetails.Rows.Count; i++)
            {
                if (gvDCDetails.Rows[i].Cells[5].Value.ToString() == "")
                    gvDCDetails.Rows[i].Cells[5].Value = "0";
                sqlQry += "INSERT INTO SP_ST_DUMP_DC(SPSDC_COMPANY_CODE,SPDDC_BRANCH_CODE,SPDDC_FIN_YEAR,SPDDC_TRN_NO,SPDDC_TRN_NUMBER,SPDDC_TRN_DATE,SPDDC_GRN_NUMBER,SPDDC_DUMP_TYPE,SPDDC_PRODUCT_ID,SPDDC_ISS_QTY,SPDDC_HORL_RT)VALUES (" +
                "'" + CommonData.CompanyCode + "','" + CommonData.BranchCode + "','" + CommonData.FinancialYear + "'," + txtTrnNo.Text + ",'" + gvDCDetails.Rows[i].Cells[7].Value.ToString() + "','" + Convert.ToDateTime(dtTrans.Value).ToString("dd/MMM/yyyy") + "','" + gvDCDetails.Rows[i].Cells[4].Value.ToString() + "','" + gvDCDetails.Rows[i].Cells[1].FormattedValue.ToString() + "','" + gvDCDetails.Rows[i].Cells[6].Value.ToString() + "'," + gvDCDetails.Rows[i].Cells[5].Value.ToString() + "," + gvDCDetails.Rows[i].Cells[9].Value.ToString() + ")";
            }
            DialogResult dlgResult = MessageBox.Show("Do you want Save to DC data?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgResult == DialogResult.Yes)
            {
                sqlQry += "INSERT INTO SP_ST_DUMP_DC(SPSDC_COMPANY_CODE,SPDDC_BRANCH_CODE,SPDDC_FIN_YEAR,SPDDC_TRN_NO,SPDDC_TRN_NUMBER,SPDDC_TRN_DATE,SPDDC_GRN_NUMBER,SPDDC_DUMP_TYPE,SPDDC_PRODUCT_ID,SPDDC_ISS_QTY,SPDDC_HORL_RT)VALUES (" +
                "'" + CommonData.CompanyCode + "','" + CommonData.BranchCode + "','" + CommonData.FinancialYear + "'," + txtTrnNo.Text + ",'','" + Convert.ToDateTime(dtTrans.Value).ToString("dd/MMM/yyyy") + "','" + sGRNNumber + "','V to G','" + sGRNProductID + "'," + txtVtoG.Text + "," + txtAmt.Text + ")";
            }

            int iRetVal = objSQLDB.ExecuteSaveData(sqlQry);
            objSQLDB = null;
            if (iRetVal > 0)
                MessageBox.Show("Inserted Data Successfully", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Data not Inserted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnCancel_Click(null, null);
        }
        private void gvGRNDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 8)
            //{
            //    if (e.RowIndex != -1)
            //    {
            //        foreach (DataGridViewRow row in gvGRNDetails.Rows)
            //        {
            //            row.Cells[e.ColumnIndex].Value = false;
            //        }
            //    }
            //}
        }
        private void GetTotalQuantity()
        {
            int dbInvAmt = 0;
            for (int i = 0; i < gvDCDetails.Rows.Count; i++)
            {
                if (gvDCDetails.Rows[i].Cells[5].Value != "")
                {
                    if (Convert.ToInt32(gvDCDetails.Rows[i].Cells[5].Value.ToString()) >= 1)
                    {
                        if (gvDCDetails.Rows[i].Cells[4].Value.ToString() != "")
                            dbInvAmt += Convert.ToInt32(gvDCDetails.Rows[i].Cells[5].Value);
                    }
                }
            }
            txtDC_Qty.Text = dbInvAmt.ToString();
            txtGrn_Qty.Text = sGRNProductQty;
            int ival = Convert.ToInt32(txtGrn_Qty.Text) - Convert.ToInt32(txtDC_Qty.Text);
            txtBal_Qty.Text = ival.ToString();
            txtVtoG.Text = "0";
            txtBalance.Text = txtBal_Qty.Text;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            gvDCDetails.Rows.Clear();
            gvGRNDetails.Rows.Clear();
            txtCompany.Text = CommonData.CompanyName;
            txtStockPoint.Text = CommonData.BranchName;
            dtTrans.Value = System.DateTime.Now;
            dtFrom.Value = System.DateTime.Now;
            dtTodate.Value = System.DateTime.Now;
            txtTrnNo.Text = GenerateNewTrnNo().ToString();
            txtBal_Qty.Text = "0";
            txtDC_Qty.Text = "0";
            txtGrn_Qty.Text = "0";
            txtVtoG.Text = "0";
            txtBalance.Text = "0";
        }
        private void dtFrom_ValueChanged(object sender, EventArgs e)
        {
            dtTodate.Value = dtFrom.Value.AddDays(1);
        }
        private void dtTodate_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan TS = Convert.ToDateTime(dtTodate.Value) - Convert.ToDateTime(dtFrom.Value);
            if (TS.Days > 1)
            {
                dtTodate.Value = dtFrom.Value.AddDays(1);
                MessageBox.Show("selected dates should be 2 days only!", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void txtVtoG_TextChanged(object sender, EventArgs e)
        {
            if (sVtoG != "")
            {
                txtVtoG.Text = txtVtoG.Text == "" ? "0" : txtVtoG.Text;
                int ivals = Convert.ToInt32(txtBal_Qty.Text) - Convert.ToInt32(txtVtoG.Text);
                if (ivals < 0)
                {
                    txtVtoG.Text = "0";
                    MessageBox.Show("Please Enter Valid Quantity.", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                decimal ivalus = Convert.ToDecimal(sVtoG) * Convert.ToDecimal(txtVtoG.Text);
                txtAmt.Text = ivalus.ToString();
                txtBalance.Text = ivals.ToString();
            }
        }
    }
}
