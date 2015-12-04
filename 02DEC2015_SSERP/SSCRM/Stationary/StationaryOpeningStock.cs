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
    public partial class StationaryOpeningStock : Form
    {
        private SQLDB objDB = null;
        public StationaryOpeningStock()
        {
            InitializeComponent();
        }

        private void StationaryOpeningStock_Load(object sender, EventArgs e)
        {
            gvItemsDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10,
                                                        System.Drawing.FontStyle.Regular);
            dtpDate.Value = Convert.ToDateTime(CommonData.CurrentDate);

            FillOpeningStockData();
        }

        private void FillOpeningStockData()
        {
            objDB = new SQLDB();
            DataTable dt = new DataTable();
            string sqlText = " SELECT SOS_COMPANY_CODE,SOS_STATE_CODE,SOS_BRANCH_CODE,SOS_FIN_YEAR,SOS_ON_DATE,SOS_ITEM_ID,SIM_ITEM_NAME" +
                             ",SOS_QTY,SOS_CREATED_BY,SOS_CREATED_DATE FROM STATIONARY_OPENING_STOCK INNER JOIN STATIONARY_ITEMS_MASTER " +
                             "ON SIM_ITEM_CODE = SOS_ITEM_ID WHERE SOS_COMPANY_CODE = '" + CommonData.CompanyCode + 
                             "' AND SOS_BRANCH_CODE = '" + CommonData.BranchCode + "'";
            try
            {
                dt = objDB.ExecuteDataSet(sqlText).Tables[0];
                gvItemsDetails.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    dtpDate.Value = Convert.ToDateTime(dt.Rows[0]["SOS_ON_DATE"].ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = i+1;
                        tempRow.Cells.Add(cellSLNO);

                        DataGridViewCell cellItemID = new DataGridViewTextBoxCell();
                        cellItemID.Value = dt.Rows[i]["SOS_ITEM_ID"].ToString();
                        tempRow.Cells.Add(cellItemID);

                        DataGridViewCell cellItemName = new DataGridViewTextBoxCell();
                        cellItemName.Value = dt.Rows[i]["SIM_ITEM_NAME"].ToString();
                        tempRow.Cells.Add(cellItemName);

                        DataGridViewCell cellItemQty = new DataGridViewTextBoxCell();
                        cellItemQty.Value = dt.Rows[i]["SOS_QTY"].ToString();
                        tempRow.Cells.Add(cellItemQty);
                        
                        gvItemsDetails.Rows.Add(tempRow);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objDB = null;
                dt = null;
            }

        }

        private void btnItemsSearch_Click(object sender, EventArgs e)
        {
            StationaryItemsSearch ItemSearch = new StationaryItemsSearch("StationaryOpeningStock");
            ItemSearch.objStationaryOpeningStock = this;
            ItemSearch.ShowDialog();
        }

        private void btnClearItems_Click(object sender, EventArgs e)
        {
            gvItemsDetails.Rows.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                objDB = new SQLDB();
                try
                {                    
                    string sqlText = "";
                    int iRes = 0;
                    sqlText += " DELETE FROM STATIONARY_OPENING_STOCK WHERE SOS_COMPANY_CODE='" + CommonData.CompanyCode + "' AND SOS_BRANCH_CODE='" + CommonData.BranchCode + "'";
                    for (int i = 0; i < gvItemsDetails.Rows.Count; i++)
                    {
                        if (gvItemsDetails.Rows[i].Cells["Qty"].Value != null)
                        {
                            if (Convert.ToDouble(gvItemsDetails.Rows[i].Cells["Qty"].Value) != 0)
                                sqlText += " INSERT INTO STATIONARY_OPENING_STOCK(SOS_COMPANY_CODE,SOS_STATE_CODE,SOS_BRANCH_CODE" +
                                            ",SOS_FIN_YEAR,SOS_ON_DATE,SOS_ITEM_ID,SOS_QTY,SOS_CREATED_BY,SOS_CREATED_DATE) VALUES" +
                                            "('" + CommonData.CompanyCode + "','" + CommonData.StateCode + "','" + CommonData.BranchCode + "'" +
                                            ",'" + CommonData.FinancialYear + "','" + Convert.ToDateTime(dtpDate.Value).ToString("dd/MMM/yyyy") + "'" +
                                            "," + gvItemsDetails.Rows[i].Cells["ItemID"].Value + "," + gvItemsDetails.Rows[i].Cells["Qty"].Value +
                                            ",'" + CommonData.LogUserId + "','" + CommonData.CurrentDate + "'); ";
                        }
                    }
                    if (sqlText.Length > 10)
                        iRes = objDB.ExecuteSaveData(sqlText);
                    if (iRes > 0)
                    {
                        MessageBox.Show("Data Saved Successfully", "Stationary :: Opening Stock", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillOpeningStockData();
                    }
                    else
                    {
                        MessageBox.Show("Data Not Saved", "Stationary :: Opening Stock", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objDB = null;
                }

            }
        }

        private bool CheckData()
        {
            bool bFlag = true;
            if (gvItemsDetails.Rows.Count == 0)
            {
                MessageBox.Show("Enter Atleast One Item", "SSCRM :: Stationary Opening Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return bFlag;
        }

        private void gvItemsDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvItemsDetails.Columns["Delete"].Index)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    //string ProductId = gvProductDetails.Rows[e.RowIndex].Cells[gvProductDetails.Columns["ProductId"].Index].Value.ToString();
                    DataGridViewRow dgvr = gvItemsDetails.Rows[e.RowIndex];
                    gvItemsDetails.Rows.Remove(dgvr);
                    OrderSlNo();
                }
            }
        }

        private void OrderSlNo()
        {
            if (gvItemsDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvItemsDetails.Rows.Count; i++)
                {
                    gvItemsDetails.Rows[i].Cells["SLNO"].Value = i + 1;
                }
            }
        }
    }
}
