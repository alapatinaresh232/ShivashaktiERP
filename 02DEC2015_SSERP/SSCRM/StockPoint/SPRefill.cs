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
    public partial class SPRefill : Form
    {
        private SQLDB objSQLDB = null;
        private double strSourceKGVolu = 0;
        private double strSourceLtrVolu = 0;
        private double strDestKGVolu = 0;
        private double strDestLtrVolu = 0;
        public SPRefill()
        {
            InitializeComponent();
        }
        private void SPRefill_Load(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            lblDocMonth.Text = CommonData.DocMonth.ToString();

            dgvSourceProd.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);

            dgvDistProd.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);

            //dgvDistProd.DefaultCellStyle.BackColor = Color.LightGreen;
            //dgvDistProd.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGreen;
            dttrnDate.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyyy"));
            DataSet dsMax = objSQLDB.ExecuteDataSet("SELECT isnull(max(SPRH_TRN_NO),0)+1 FROM SP_REFILL_HEAD WHERE SPRH_COMPANY_CODE='" + CommonData.CompanyCode + "' AND SPRH_BRANCH_CODE='" + CommonData.BranchCode + "' AND SPRH_FIN_YEAR='" + CommonData.FinancialYear + "'");
            txtTranNo.Text = dsMax.Tables[0].Rows[0][0].ToString();
        }

        private void CalculateTotal()
        {
            double iLabCharg = 0, iStictCharg = 0, iWeighRent = 0, iOthExp = 0, iSourceVolm = 0, iDestVolm = 0;            
            if (txtLaburChr.Text.Length > 0)
                iLabCharg = Convert.ToDouble(txtLaburChr.Text);
            else
                txtLaburChr.Text = "0";
            if(txtStichingChr.Text.Length>0)
                iStictCharg = Convert.ToDouble(txtStichingChr.Text);
            else
                txtStichingChr.Text = "0";
            if(txtWeightRent.Text.Length>0)
                iWeighRent = Convert.ToDouble(txtWeightRent.Text);
            else
                txtWeightRent.Text = "0";
            if(txtOtherExp.Text.Length>0)
                iOthExp = Convert.ToDouble(txtOtherExp.Text);
            else
                txtOtherExp.Text = "0";
            txtTotalCharges.Text = Convert.ToDouble(iLabCharg + iStictCharg + iWeighRent + iOthExp).ToString("f");


            txtExessOrShortage.Text = Convert.ToDouble(strSourceKGVolu - strDestKGVolu).ToString("f") + "kg, " + Convert.ToDouble(strSourceLtrVolu - strDestLtrVolu).ToString("f") + "ltrs";
                    
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        //private void gvProductDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex >= 0)
        //    {
        //        DataGridView dgv = (DataGridView)sender;
        //        if (e.ColumnIndex == 5 || e.ColumnIndex == 7 || e.ColumnIndex == 8)
        //        {
        //            DataGridViewCell textBoxCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
        //            if (textBoxCell != null)
        //            {
        //                dgvSourceProd.CurrentCell = textBoxCell;
        //                dgv.BeginEdit(true);
        //            }
        //        }
        //    }
        //}

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            ProductSearchAll PSearch = new ProductSearchAll("SPRefill_Sours");
            PSearch.objSPRefill = this;
            PSearch.ShowDialog();
        }
        private void btnClearProd_Click(object sender, EventArgs e)
        {
            dgvSourceProd.Rows.Clear();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            ProductSearchAll PSearch = new ProductSearchAll("SPRefill_dist");
            PSearch.objSPRefill = this;
            PSearch.ShowDialog();
        }

        private void btnDistClear_Click(object sender, EventArgs e)
        {
            dgvDistProd.Rows.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            int iretVal = 0, iretVals = 0;
            string sqlSource = "", sqlDistnation = "";
            try
            {
                CalculateTotal();             
                
                DataSet dsCnt = objSQLDB.ExecuteDataSet("SELECT Count(*) FROM SP_REFILL_HEAD WHERE SPRH_COMPANY_CODE='" + CommonData.CompanyCode + "' AND SPRH_BRANCH_CODE='" + CommonData.BranchCode + "' AND SPRH_FIN_YEAR='" + CommonData.FinancialYear + "' AND SPRH_TRN_NO=" + txtTranNo.Text);
                if (dsCnt.Tables[0].Rows[0][0].ToString() == "0")
                {
                    DataSet dsMax = objSQLDB.ExecuteDataSet("SELECT isnull(max(SPRH_TRN_NO),0)+1 FROM SP_REFILL_HEAD WHERE SPRH_COMPANY_CODE='" + CommonData.CompanyCode + "' AND SPRH_BRANCH_CODE='" + CommonData.BranchCode + "' AND SPRH_FIN_YEAR='" + CommonData.FinancialYear + "'");
                    txtTranNo.Text = dsMax.Tables[0].Rows[0][0].ToString();
                    string strSql = " INSERT INTO SP_REFILL_HEAD(SPRH_COMPANY_CODE,SPRH_STATE_CODE,SPRH_BRANCH_CODE,SPRH_FIN_YEAR,SPRH_DOCUMENT_MONTH,SPRH_TRN_NO,SPRH_LABOUR_CHARGES," +
                        "SPRH_STICHING_CHARGES,SPRH_WEIGHING_RENT,SPRH_OTHER_EXP,SPRH_CREATED_BY,SPRH_CREATED_DATE,SPRH_TRN_DATE,SPRH_REMARKS) VALUES('" + CommonData.CompanyCode + "','" + CommonData.StateCode +
                        "','" + CommonData.BranchCode + "','" + CommonData.FinancialYear + "','" + lblDocMonth.Text + "'," + txtTranNo.Text + "," + txtLaburChr.Text + "," + txtStichingChr.Text +
                        "," + txtWeightRent.Text + "," + txtOtherExp.Text + ",'" + CommonData.LogUserId + "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") + 
                        "','" + Convert.ToDateTime(dttrnDate.Value).ToString("dd/MMM/yyyy") + "','" + txtRemarks.Text + "')";
                    iretVal = objSQLDB.ExecuteSaveData(strSql);
                }
                else
                {
                    string strSql = " UPDATE SP_REFILL_HEAD SET SPRH_LABOUR_CHARGES=" + txtLaburChr.Text + ",SPRH_STICHING_CHARGES=" + txtStichingChr.Text + 
                        ",SPRH_WEIGHING_RENT=" + txtWeightRent.Text + ",SPRH_OTHER_EXP=" + txtOtherExp.Text
                        + ",SPRH_TRN_DATE='" + Convert.ToDateTime(dttrnDate.Value).ToString("dd/MMM/yyyy") + 
                        "',SPRH_LAST_MODIFIED_BY='" + CommonData.LogUserId + "',SPRH_LAST_MODIFIED_DATE='" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") + 
                        "',SPRH_REMARKS='" + txtRemarks.Text + "' WHERE SPRH_COMPANY_CODE='" + CommonData.CompanyCode + 
                        "' AND SPRH_BRANCH_CODE='" + CommonData.BranchCode + "' AND SPRH_FIN_YEAR='" + CommonData.FinancialYear + 
                        "' AND SPRH_TRN_NO=" + txtTranNo.Text;

                    strSql += " DELETE FROM SP_REFILL_SOURCE_DETL WHERE SPSD_COMPANY_CODE='" + CommonData.CompanyCode + "' AND SPSD_BRANCH_CODE='" + CommonData.BranchCode + "' AND SPSD_FIN_YEAR='" + CommonData.FinancialYear + "' AND SPSD_TRN_NO=" + txtTranNo.Text;
                    strSql += " DELETE FROM SP_REFILL_DESTIN_DETL WHERE SPDD_COMPANY_CODE='" + CommonData.CompanyCode + "' AND SPDD_BRANCH_CODE='" + CommonData.BranchCode + "' AND SPDD_FIN_YEAR='" + CommonData.FinancialYear + "' AND SPDD_TRN_NO=" + txtTranNo.Text;
                    iretVal = objSQLDB.ExecuteSaveData(strSql);
                }
                if (iretVal > 0)
                {
                    for (int i = 0; i < dgvSourceProd.Rows.Count; i++)
                    {
                        sqlSource += " INSERT INTO SP_REFILL_SOURCE_DETL(SPSD_COMPANY_CODE,SPSD_STATE_CODE,SPSD_BRANCH_CODE,SPSD_FIN_YEAR,SPSD_TRN_NO,SPSD_TRN_SL_NO,SPSD_PRODUCT_ID,SPSD_DAMAGE_QTY) VALUES ('" + CommonData.CompanyCode +
                            "','" + CommonData.StateCode + "','" + CommonData.BranchCode + "','" + CommonData.FinancialYear + "'," + txtTranNo.Text + "," + Convert.ToInt32(i + 1) + ",'" + dgvSourceProd.Rows[i].Cells["ProductID"].Value + "'," + dgvSourceProd.Rows[i].Cells["Qty"].Value + ")";
                    }

                    for (int i = 0; i < dgvDistProd.Rows.Count; i++)
                    {
                        sqlDistnation += " INSERT INTO SP_REFILL_DESTIN_DETL(SPDD_COMPANY_CODE,SPDD_STATE_CODE,SPDD_BRANCH_CODE,SPDD_FIN_YEAR,SPDD_TRN_NO,SPDD_TRN_SL_NO,SPDD_PRODUCT_ID,SPDD_GOOD_QTY,SPDD_WEIGHT_PER_UNIT) VALUES ('" + CommonData.CompanyCode +
                            "','" + CommonData.StateCode + "','" + CommonData.BranchCode + "','" + CommonData.FinancialYear + "'," + txtTranNo.Text + "," + Convert.ToInt32(i + 1) + ",'" + dgvDistProd.Rows[i].Cells["DistProductID"].Value + "'," + dgvDistProd.Rows[i].Cells["distQty"].Value + "," + dgvDistProd.Rows[i].Cells["UnitWeight"].Value + ")";
                    }
                    iretVals = objSQLDB.ExecuteSaveData(sqlSource + sqlDistnation);
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
            if (iretVals > 0)
                MessageBox.Show("Inserted data successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Data is not Inserted", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            btnCancel_Click(null, null);
        }

        private void txtTranNo_Validated(object sender, EventArgs e)
        {
            if (txtTranNo.Text != "")
            {
                objSQLDB = new SQLDB();
                string SqlQry = " SELECT * FROM SP_REFILL_HEAD WHERE SPRH_COMPANY_CODE='" + CommonData.CompanyCode + "' AND SPRH_BRANCH_CODE='" + CommonData.BranchCode + "' AND SPRH_FIN_YEAR='" + CommonData.FinancialYear + "' AND SPRH_TRN_NO=" + txtTranNo.Text;
                SqlQry += " SELECT A.*,PM_PRODUCT_NAME,CASE WHEN PM_UOM IN ('LITRES','ML') THEN 'LTRS' WHEN PM_UOM IN ('KG','GRMS') THEN 'KGS' ELSE '' END AS PM_UOM,PM_UOM_QTY,CATEGORY_NAME FROM SP_REFILL_SOURCE_DETL A INNER JOIN PRODUCT_MAS B ON A.SPSD_PRODUCT_ID=B.PM_PRODUCT_ID INNER JOIN CATEGORY_MASTER C ON B.PM_CATEGORY_ID=C.CATEGORY_ID " +
                    " WHERE SPSD_COMPANY_CODE='" + CommonData.CompanyCode + "' AND SPSD_BRANCH_CODE='" + CommonData.BranchCode + "' AND SPSD_FIN_YEAR='" + CommonData.FinancialYear + "' AND SPSD_TRN_NO=" + txtTranNo.Text;
                SqlQry += " SELECT A.*,PM_PRODUCT_NAME,CASE WHEN PM_UOM IN ('LITRES','ML') THEN 'LTRS' WHEN PM_UOM IN ('KG','GRMS') THEN 'KGS' ELSE '' END AS PM_UOM,PM_UOM_QTY,CATEGORY_NAME FROM SP_REFILL_DESTIN_DETL A INNER JOIN PRODUCT_MAS B ON A.SPDD_PRODUCT_ID=B.PM_PRODUCT_ID INNER JOIN CATEGORY_MASTER C ON B.PM_CATEGORY_ID=C.CATEGORY_ID " +
                    " WHERE SPDD_COMPANY_CODE='" + CommonData.CompanyCode + "' AND SPDD_BRANCH_CODE='" + CommonData.BranchCode + "' AND SPDD_FIN_YEAR='" + CommonData.FinancialYear + "' AND SPDD_TRN_NO=" + txtTranNo.Text;
                DataSet ds = objSQLDB.ExecuteDataSet(SqlQry);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblDocMonth.Text = ds.Tables[0].Rows[0]["SPRH_DOCUMENT_MONTH"].ToString();
                    txtLaburChr.Text = ds.Tables[0].Rows[0]["SPRH_LABOUR_CHARGES"].ToString();
                    txtStichingChr.Text = ds.Tables[0].Rows[0]["SPRH_STICHING_CHARGES"].ToString();
                    txtWeightRent.Text = ds.Tables[0].Rows[0]["SPRH_WEIGHING_RENT"].ToString();
                    txtOtherExp.Text = ds.Tables[0].Rows[0]["SPRH_OTHER_EXP"].ToString();
                    txtRemarks.Text = ds.Tables[0].Rows[0]["SPRH_REMARKS"].ToString();                    
                    GetBindDataSource(ds.Tables[1]);
                    GetBindDataDist(ds.Tables[2]);
                    strSourceKGVolu = 0;
                    strSourceLtrVolu = 0;
                    for (int i = 0; i < dgvSourceProd.Rows.Count; i++)
                    {
                        if (dgvSourceProd.Rows[i].Cells["VolumeType"].Value.ToString() == "KGS")
                        {
                            if (dgvSourceProd.Rows[i].Cells["Qty"].Value.ToString() != "")
                                strSourceKGVolu += Convert.ToDouble(Convert.ToDouble(dgvSourceProd.Rows[i].Cells["Qty"].Value) * Convert.ToDouble(dgvSourceProd.Rows[i].Cells["UnitVolume"].Value));
                        }
                        if (dgvSourceProd.Rows[i].Cells["VolumeType"].Value.ToString() == "LTRS")
                        {
                            if (dgvSourceProd.Rows[i].Cells["Qty"].Value.ToString() != "")
                                strSourceLtrVolu += Convert.ToDouble(Convert.ToDouble(dgvSourceProd.Rows[i].Cells["Qty"].Value) * Convert.ToDouble(dgvSourceProd.Rows[i].Cells["UnitVolume"].Value));
                        }
                    }
                    txtSourceVolume.Text = strSourceKGVolu.ToString("f") + "kg, " + strSourceLtrVolu.ToString("f") + "ltrs";

                    strDestKGVolu = 0;
                    strDestLtrVolu = 0;
                    for (int i = 0; i < dgvDistProd.Rows.Count; i++)
                    {
                        if (dgvDistProd.Rows[i].Cells["RVolumeType"].Value.ToString() == "KGS")
                        {
                            if (dgvDistProd.Rows[i].Cells["distQty"].Value.ToString() != "")
                                strDestKGVolu += Convert.ToDouble(Convert.ToDouble(dgvDistProd.Rows[i].Cells["distQty"].Value) * Convert.ToDouble(dgvDistProd.Rows[i].Cells["UnitWeight"].Value));
                        }
                        if (dgvDistProd.Rows[i].Cells["RVolumeType"].Value.ToString() == "LTRS")
                        {
                            if (dgvDistProd.Rows[i].Cells["distQty"].Value.ToString() != "")
                                strDestLtrVolu += Convert.ToDouble(Convert.ToDouble(dgvDistProd.Rows[i].Cells["distQty"].Value) * Convert.ToDouble(dgvDistProd.Rows[i].Cells["UnitWeight"].Value));
                        }
                    }

                    //for (int i = 0; i < dgvDistProd.Rows.Count; i++)
                    //{
                    //    if (dgvDistProd.Rows[i].Cells["UnitWeight"].Value.ToString().Trim() == "")
                    //        dgvDistProd.Rows[i].Cells["UnitWeight"].Value = dgvDistProd.Rows[i].Cells["RUnitVolume"].Value.ToString();
                    //    if (dgvDistProd.Rows[i].Cells["distQty"].Value.ToString().Trim() != "")
                    //        totalSourceVolume += Convert.ToDouble(Convert.ToDouble(dgvDistProd.Rows[i].Cells["distQty"].Value) * Convert.ToDouble(dgvDistProd.Rows[i].Cells["UnitWeight"].Value));
                    //}
                    txtRecievedVolume.Text = strDestKGVolu.ToString("f") + "kg, " + strDestLtrVolu.ToString("f") + "ltrs";

                    CalculateTotal();

                }
                else
                {
                    btnCancel_Click(null, null);
                    lblDocMonth.Text = CommonData.DocMonth.ToString();
                }
                objSQLDB = null;
            }
        }
        public void GetBindDataSource(DataTable dt)
        {
            int intRow = 1;
            dgvSourceProd.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellItemID = new DataGridViewTextBoxCell();
                cellItemID.Value = dt.Rows[i]["SPSD_PRODUCT_ID"].ToString();
                tempRow.Cells.Add(cellItemID);

                DataGridViewCell cellItem = new DataGridViewTextBoxCell();
                cellItem.Value = dt.Rows[i]["PM_PRODUCT_NAME"].ToString();
                tempRow.Cells.Add(cellItem);

                DataGridViewCell cellItemName = new DataGridViewTextBoxCell();
                cellItemName.Value = dt.Rows[i]["CATEGORY_NAME"].ToString();
                tempRow.Cells.Add(cellItemName);

                DataGridViewCell cellUnitVolume = new DataGridViewTextBoxCell();
                cellUnitVolume.Value = dt.Rows[i]["PM_UOM_QTY"].ToString();
                tempRow.Cells.Add(cellUnitVolume);

                DataGridViewCell cellAvailQty = new DataGridViewTextBoxCell();
                cellAvailQty.Value = dt.Rows[i]["SPSD_DAMAGE_QTY"].ToString();
                tempRow.Cells.Add(cellAvailQty);

                DataGridViewCell cellUnitVol = new DataGridViewTextBoxCell();
                cellUnitVol.Value = dt.Rows[i]["PM_UOM_QTY"].ToString();
                tempRow.Cells.Add(cellUnitVol);

                DataGridViewCell cellVolType = new DataGridViewTextBoxCell();
                cellVolType.Value = dt.Rows[i]["PM_UOM"].ToString();
                tempRow.Cells.Add(cellVolType);

                intRow = intRow + 1;
                dgvSourceProd.Rows.Add(tempRow);
            }
        }
        public void GetBindDataDist(DataTable dt)
        {
            int intRow = 1;
            dgvDistProd.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellItemID = new DataGridViewTextBoxCell();
                cellItemID.Value = dt.Rows[i]["SPDD_PRODUCT_ID"].ToString();
                tempRow.Cells.Add(cellItemID);

                DataGridViewCell cellItem = new DataGridViewTextBoxCell();
                cellItem.Value = dt.Rows[i]["PM_PRODUCT_NAME"].ToString();
                tempRow.Cells.Add(cellItem);

                DataGridViewCell cellItemName = new DataGridViewTextBoxCell();
                cellItemName.Value = dt.Rows[i]["CATEGORY_NAME"].ToString();
                tempRow.Cells.Add(cellItemName);


                DataGridViewCell cellUnitVolume = new DataGridViewTextBoxCell();
                cellUnitVolume.Value = dt.Rows[i]["SPDD_WEIGHT_PER_UNIT"].ToString();
                tempRow.Cells.Add(cellUnitVolume);

                DataGridViewCell cellAvailQty = new DataGridViewTextBoxCell();
                cellAvailQty.Value = dt.Rows[i]["SPDD_GOOD_QTY"].ToString();
                tempRow.Cells.Add(cellAvailQty);

                DataGridViewCell cellUnitVol = new DataGridViewTextBoxCell();
                cellUnitVol.Value = dt.Rows[i]["PM_UOM_QTY"].ToString();
                tempRow.Cells.Add(cellUnitVol);

                DataGridViewCell cellVolType = new DataGridViewTextBoxCell();
                cellVolType.Value = dt.Rows[i]["PM_UOM"].ToString();
                tempRow.Cells.Add(cellVolType);

                intRow = intRow + 1;
                dgvDistProd.Rows.Add(tempRow);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            lblDocMonth.Text = CommonData.DocMonth.ToString();
            txtLaburChr.Text = "";
            txtOtherExp.Text = "";
            txtStichingChr.Text = "";
            txtTranNo.Text = "";
            txtWeightRent.Text = "";
            txtTotalCharges.Text = "0.00";
            txtRemarks.Text = "";
            dgvSourceProd.Rows.Clear();
            dgvDistProd.Rows.Clear();
            txtSourceVolume.Text = "";
            txtRecievedVolume.Text = "";
            txtExessOrShortage.Text = "";
            try
            {
                objSQLDB = new SQLDB();
                DataSet dsMax = objSQLDB.ExecuteDataSet("SELECT isnull(max(SPRH_TRN_NO),0)+1 FROM SP_REFILL_HEAD WHERE SPRH_COMPANY_CODE='" + CommonData.CompanyCode + "' AND SPRH_BRANCH_CODE='" + CommonData.BranchCode + "' AND SPRH_FIN_YEAR='" + CommonData.FinancialYear + "'");
                txtTranNo.Text = dsMax.Tables[0].Rows[0][0].ToString();
                dsMax = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLDB = null;
            }
            
        }

        private void txtLaburChr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtStichingChr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtWeightRent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtOtherExp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtLaburChr_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotal();
        }

        private void txtStichingChr_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotal();
        }

        private void txtWeightRent_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotal();
        }

        private void txtOtherExp_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotal();
        }

        private void dgvSourceProd_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                strSourceKGVolu = 0;
                strSourceLtrVolu = 0;
                for (int i = 0; i < dgvSourceProd.Rows.Count; i++)
                {
                    if (dgvSourceProd.Rows[i].Cells["VolumeType"].Value.ToString() == "KGS")
                    {
                        if (dgvSourceProd.Rows[i].Cells["Qty"].Value.ToString() != "")
                            strSourceKGVolu += Convert.ToDouble(Convert.ToDouble(dgvSourceProd.Rows[i].Cells["Qty"].Value) * Convert.ToDouble(dgvSourceProd.Rows[i].Cells["UnitVolume"].Value));
                    }
                    if (dgvSourceProd.Rows[i].Cells["VolumeType"].Value.ToString() == "LTRS")
                    {
                        if (dgvSourceProd.Rows[i].Cells["Qty"].Value.ToString() != "")
                            strSourceLtrVolu += Convert.ToDouble(Convert.ToDouble(dgvSourceProd.Rows[i].Cells["Qty"].Value) * Convert.ToDouble(dgvSourceProd.Rows[i].Cells["UnitVolume"].Value));
                    }
                }
                txtSourceVolume.Text = strSourceKGVolu.ToString("f") + "kg, " + strSourceLtrVolu.ToString("f") + "ltrs";
            }
            CalculateTotal();
        }

        private void dgvDistProd_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4 || e.ColumnIndex == 5)
            {
                strDestKGVolu = 0;
                strDestLtrVolu = 0;
                for (int i = 0; i < dgvDistProd.Rows.Count; i++)
                {
                    if (dgvDistProd.Rows[i].Cells["RVolumeType"].Value.ToString() == "KGS")
                    {
                        if (dgvDistProd.Rows[i].Cells["distQty"].Value.ToString() != "")
                            strDestKGVolu += Convert.ToDouble(Convert.ToDouble(dgvDistProd.Rows[i].Cells["distQty"].Value) * Convert.ToDouble(dgvDistProd.Rows[i].Cells["UnitWeight"].Value));
                    }
                    if (dgvDistProd.Rows[i].Cells["RVolumeType"].Value.ToString() == "LTRS")
                    {
                        if (dgvDistProd.Rows[i].Cells["distQty"].Value.ToString() != "")
                            strDestLtrVolu += Convert.ToDouble(Convert.ToDouble(dgvDistProd.Rows[i].Cells["distQty"].Value) * Convert.ToDouble(dgvDistProd.Rows[i].Cells["UnitWeight"].Value));
                    }
                }
                
                //for (int i = 0; i < dgvDistProd.Rows.Count; i++)
                //{
                //    if (dgvDistProd.Rows[i].Cells["UnitWeight"].Value.ToString().Trim() == "")
                //        dgvDistProd.Rows[i].Cells["UnitWeight"].Value = dgvDistProd.Rows[i].Cells["RUnitVolume"].Value.ToString();
                //    if (dgvDistProd.Rows[i].Cells["distQty"].Value.ToString().Trim() != "")
                //        totalSourceVolume += Convert.ToDouble(Convert.ToDouble(dgvDistProd.Rows[i].Cells["distQty"].Value) * Convert.ToDouble(dgvDistProd.Rows[i].Cells["UnitWeight"].Value));
                //}
                txtRecievedVolume.Text = strDestKGVolu.ToString("f") + "kg, " + strDestLtrVolu.ToString("f") + "ltrs";
            }
            CalculateTotal();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            int iRes = 0;
            string sqlText = "";
            sqlText = "";
            try
            {
                DataSet dsCnt = objSQLDB.ExecuteDataSet("SELECT Count(*) FROM SP_REFILL_HEAD WHERE SPRH_COMPANY_CODE='" + CommonData.CompanyCode + 
                    "' AND SPRH_BRANCH_CODE='" + CommonData.BranchCode + "' AND SPRH_FIN_YEAR='" + CommonData.FinancialYear + 
                    "' AND SPRH_TRN_NO=" + txtTranNo.Text);
                if (Convert.ToInt32(dsCnt.Tables[0].Rows[0][0].ToString())>0)
                {
                    sqlText = " DELETE FROM SP_REFILL_DESTIN_DETL WHERE SPDD_COMPANY_CODE='" + CommonData.CompanyCode + "' AND " +
                            "SPDD_BRANCH_CODE='" + CommonData.BranchCode + "' AND SPDD_FIN_YEAR='" + CommonData.FinancialYear + 
                            "' AND SPDD_TRN_NO=" + txtTranNo.Text;
                    sqlText += " DELETE FROM SP_REFILL_SOURCE_DETL WHERE SPSD_COMPANY_CODE='" + CommonData.CompanyCode + "' AND " +
                            "SPSD_BRANCH_CODE='" + CommonData.BranchCode + "' AND SPSD_FIN_YEAR='" + CommonData.FinancialYear +
                            "' AND SPSD_TRN_NO=" + txtTranNo.Text;
                    sqlText += " DELETE FROM SP_REFILL_HEAD WHERE SPRH_COMPANY_CODE='" + CommonData.CompanyCode + "' AND " +
                            "SPRH_BRANCH_CODE='" + CommonData.BranchCode + "' AND SPRH_FIN_YEAR='" + CommonData.FinancialYear +
                            "' AND SPRH_TRN_NO=" + txtTranNo.Text;
                    iRes = objSQLDB.ExecuteSaveData(sqlText);
                    if (iRes > 0)
                    {
                        MessageBox.Show("Record Deleted Successfully", "SP Refill", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Record not Deleted", "SP Refill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
                else
                {
                    
                }
                
                dsCnt = null;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLDB = null;
            }
        }
    }
}
