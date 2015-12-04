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

namespace SSCRM
{
    public partial class StationaryItemsSearch : Form
    {
        private string IndentType = "";
        SQLDB objSQLdb = null;
        private InvoiceDB objInvDB = null;
        public StationaryIndent objStationaryIndent;
        public StationaryBulkIndent objStationaryBulkIndent;
        public StationaryGRN objStationaryGRN;
        public BranchStationaryIssue objBranchStationaryIssue;
        public StationaryOpeningStock objStationaryOpeningStock;
        public StationaryDCByHand objStationaryDCByHand;
        public StationaryBrocher objStationaryBroucher;
        public ShortageStationaryDetails objShortageStationaryDetails;
        public StationaryBrGRN objStationaryBrGRN;
      
        public StationaryItemsSearch(string sIndentType)
        {
            InitializeComponent();
            IndentType = sIndentType;
        }
        public StationaryItemsSearch()
        {
            InitializeComponent();

        }
        private void StationaryItemsSearch_Load(object sender, EventArgs e)
        {
            if (IndentType == "StationaryBroucher" || IndentType == "StationaryDC" || IndentType == "StationaryGRN")
            {
                FillStationaryItems();
            }
            else
            {
                FillBranchStationaryItems();
            }
        }
        private void FillStationaryItems()
        {
            objInvDB = new InvoiceDB();
            DataSet ds = new DataSet();
            string strCatId = string.Empty;
            string strItem = string.Empty;
            string strCategoryId = string.Empty;
            string strCompanycode = string.Empty;

            string strEndCompanycode = string.Empty;
            string strStateCode = "";
            string strBranchCode = "";
            string strItemCode = "";
            if (txtItemName.Text.ToString().Trim().Length > 2)
                strItem = txtItemName.Text;
            tvItems.Nodes.Clear();
            ds = objInvDB.IndentStationaryList_Get(CommonData.CompanyCode, CommonData.BranchCode, strItem);
            TreeNode tNode;

            tNode = tvItems.Nodes.Add("Stationary");
            Int16 intNode = 0, intNode1 = 0, intNode2 = 0, intNode3 = 0;
            Int16 tComp = 0;

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    if (strCategoryId != ds.Tables[0].Rows[i]["sis_category_id"].ToString())
                    {
                        strCategoryId = ds.Tables[0].Rows[i]["sis_category_id"].ToString();
                        tvItems.Nodes[0].Nodes.Add(ds.Tables[0].Rows[i]["sis_category_id"].ToString(), ds.Tables[0].Rows[i]["sis_category_name"].ToString());
                        DataRow[] dr1 = ds.Tables[0].Select("sis_category_id='" + strCategoryId + "'");
                        intNode1 = 0;
                        strCompanycode = "";
                        for (int j = 0; j < dr1.Length; j++)
                        {

                            if (strCompanycode != dr1[j]["sis_company_code"].ToString())
                            {
                                strCompanycode = dr1[j]["sis_company_code"].ToString();
                                tvItems.Nodes[0].Nodes[intNode].Nodes.Add(dr1[j]["sis_company_code"].ToString(), dr1[j]["sis_company_name"].ToString());
                                DataRow[] dr2 = ds.Tables[0].Select("sis_category_id='" + strCategoryId + "' and sis_company_code='" + strCompanycode + "'");
                                //strStateCode="";
                                intNode2 = 0;
                                for (int k = 0; k < dr2.Length; k++)
                                {
                                    if (strStateCode != dr2[k]["sis_state_code"].ToString())
                                    {
                                        strStateCode = dr2[k]["sis_state_code"].ToString();
                                        tvItems.Nodes[0].Nodes[intNode].Nodes[intNode1].Nodes.Add(dr2[k]["sis_state_code"].ToString(), dr2[k]["sis_state_name"].ToString());
                                        DataRow[] dr3 = ds.Tables[0].Select("sis_category_id='" + strCategoryId + "' and sis_company_code='" + strCompanycode +
                                                      "' and sis_state_code='" + strStateCode + "'");
                                        intNode3 = 0;
                                        for (int l = 0; l < dr3.Length; l++)
                                        {
                                            if (strBranchCode != dr3[l]["sis_branch_code"].ToString())
                                            {
                                                strBranchCode = dr3[l]["sis_branch_code"].ToString();
                                                tvItems.Nodes[0].Nodes[intNode].Nodes[intNode1].Nodes[intNode2].Nodes.Add(dr3[l]["sis_branch_code"].ToString(), dr3[l]["sis_branch_name"].ToString());
                                                DataRow[] dr4 = ds.Tables[0].Select("sis_category_id='" + strCategoryId + "' and sis_company_code='" + strCompanycode +
                                                      "' and sis_state_code='" + strStateCode + "' and sis_branch_code='" + strBranchCode + "'");

                                                for (int m = 0; m < dr4.Length; m++)
                                                {
                                                    tvItems.Nodes[0].Nodes[intNode].Nodes[intNode1].Nodes[intNode2].Nodes[intNode3].Nodes.Add(dr4[m]["sis_item_code"].ToString() +
                                                                                                      "~" + dr4[m]["sis_item_price"].ToString() +
                                                                                                      "~" + dr4[m]["sis_closing_stock"].ToString()
                                                                                                      , dr4[m]["sis_item_name"].ToString() +
                                                                                                      "  - Price: " + dr4[m]["sis_item_price"].ToString());
                                                }
                                                intNode3++;
                                            }
                                        }
                                        intNode2++;
                                        strBranchCode = "";
                                    }
                                }
                                intNode1++;
                                strStateCode = "";
                            }
                        }
                        intNode++;


                    }

                }
            }


            //DataRow[] dr1 = ds.Tables[0].Select("SIC_CATEGORY_ID='" + dr["SIC_CATEGORY_ID"].ToString() + "'");
            //if (dr1.Length > 0)
            //{
            //    tvItems.Nodes[0].Nodes[0].Nodes.Add(dr["SIM_ITEM_CODE"].ToString(), dr["SIM_ITEM_NAME"].ToString() + "  - Price: "  + dr["SIM_ITEM_PRICE"].ToString());
            //}

            //tvItems.Nodes[0].Nodes.Add("STATIONARY WITH COMPANY NAME");
            //tvItems.Nodes[0].Nodes.Add("STATIONARY WITH COMPANY NAME");
            //tvItems.Nodes[0].Nodes.Add("STATIONARY WITH-OUT COMPANY NAME");
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            //        tvItems.Nodes[0].Nodes[0].Nodes.Add(ds.Tables[0].Rows[i]["ITEM_PRICE"].ToString(), ds.Tables[0].Rows[i]["ITEM_NAME"].ToString());
            //    }
            //}
            //if (ds.Tables[1].Rows.Count > 0)
            //{
            //    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            //    {
            //        tvItems.Nodes[0].Nodes[1].Nodes.Add(ds.Tables[1].Rows[i]["ITEM_PRICE"].ToString(), ds.Tables[1].Rows[i]["ITEM_NAME"].ToString());
            //    }
            //}
            tvItems.Nodes[0].Expand();
            tvItems.Nodes[0].Nodes[0].Expand();
            //tvItems.Nodes[0].Nodes[1].Expand();
        }


        private DataSet Get_Items(string sCategory, string sCompCode, string sStateCode, string sBranch, string sGetType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCateroy", DbType.String, sCategory, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sCompany", DbType.String, sCompCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sState", DbType.String, sStateCode, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sBranch", DbType.String, sBranch, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sType", DbType.String, sGetType, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("StationaryItemsSearch_Get_search", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }
      
        private void FillBranchStationaryItems()
        {
            objInvDB = new InvoiceDB();
            DataSet ds = new DataSet();
            string strCatId = string.Empty;
            string strItem = string.Empty;
            if (txtItemName.Text.ToString().Trim().Length > 2)
                strItem = txtItemName.Text;
            tvItems.Nodes.Clear();
            ds = objInvDB.BrIndentStationaryList_Get(CommonData.CompanyCode, CommonData.BranchCode, strItem);
            TreeNode tNode;
            tNode = tvItems.Nodes.Add("Stationary");
            Int16 intNode = 0;
            string sNodes = "";
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (sNodes != dr["sis_category_name"].ToString())
                {
                    tvItems.Nodes[0].Nodes.Add(dr["sis_category_name"].ToString());
                    sNodes = dr["sis_category_name"].ToString();
                    DataRow[] dr1 = ds.Tables[0].Select("sis_category_id='" + dr["sis_category_id"].ToString() + "'");
                    for (int i = 0; i < dr1.Length; i++)
                    {
                        tvItems.Nodes[0].Nodes[intNode].Nodes.Add(dr1[i]["sis_item_code"].ToString() + 
                                                                    "~" + dr1[i]["sis_item_price"].ToString() + 
                                                                    "~" + dr1[i]["sis_closing_stock"].ToString()
                                                                    , dr1[i]["sis_item_name"].ToString() + 
                                                                    "  - Price: " + dr1[i]["sis_item_price"].ToString());
                    }
                    intNode++;
                }
            }

            //DataRow[] dr1 = ds.Tables[0].Select("SIC_CATEGORY_ID='" + dr["SIC_CATEGORY_ID"].ToString() + "'");
            //if (dr1.Length > 0)
            //{
            //    tvItems.Nodes[0].Nodes[0].Nodes.Add(dr["SIM_ITEM_CODE"].ToString(), dr["SIM_ITEM_NAME"].ToString() + "  - Price: "  + dr["SIM_ITEM_PRICE"].ToString());
            //}

            //tvItems.Nodes[0].Nodes.Add("STATIONARY WITH COMPANY NAME");
            //tvItems.Nodes[0].Nodes.Add("STATIONARY WITH COMPANY NAME");
            //tvItems.Nodes[0].Nodes.Add("STATIONARY WITH-OUT COMPANY NAME");
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            //        tvItems.Nodes[0].Nodes[0].Nodes.Add(ds.Tables[0].Rows[i]["ITEM_PRICE"].ToString(), ds.Tables[0].Rows[i]["ITEM_NAME"].ToString());
            //    }
            //}
            //if (ds.Tables[1].Rows.Count > 0)
            //{
            //    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            //    {
            //        tvItems.Nodes[0].Nodes[1].Nodes.Add(ds.Tables[1].Rows[i]["ITEM_PRICE"].ToString(), ds.Tables[1].Rows[i]["ITEM_NAME"].ToString());
            //    }
            //}
            tvItems.Nodes[0].Expand();
            tvItems.Nodes[0].Nodes[0].Expand();
            //tvItems.Nodes[0].Nodes[1].Expand();
        }

        private void tvItems_AfterCheck(object sender, TreeViewEventArgs e)
        {
            tvItems.BeginUpdate();
            foreach (TreeNode Node in e.Node.Nodes)
            {
                Node.Checked = e.Node.Checked;
            }
            tvItems.EndUpdate();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            AddItemsToGrid(tvItems.Nodes[0]);
        }

        private void AddItemsToGrid(TreeNode StartNode)
        {
            DataGridView dgvStationaryItems = null;
            if (CheckData())
            {
                if (IndentType == "StationaryIndentBR")
                {
                    dgvStationaryItems = ((StationaryIndent)objStationaryIndent).gvIndentDetails;
                    AddItemsToGridStationaryIndent(dgvStationaryItems);
                }
                else if (IndentType == "StationaryIndentBulk")
                {
                    dgvStationaryItems = ((StationaryBulkIndent)objStationaryBulkIndent).gvIndentDetails;
                    AddItemsToGridStationaryIndent(dgvStationaryItems);
                }
                else if (IndentType == "StationaryShortageDetl")
                {
                    dgvStationaryItems = ((ShortageStationaryDetails)objShortageStationaryDetails).gvIndentDetails;
                    AddItemsToGridStationaryShortageDetl(dgvStationaryItems);
                    
                }
                else if (IndentType == "StationaryGRN")
                {
                    dgvStationaryItems = ((StationaryGRN)objStationaryGRN).gvIndentDetails;
                    AddItemsToGridStationaryGRN(dgvStationaryItems);
                }
                else if (IndentType == "BrStationaryItemsIssue")
                {
                    dgvStationaryItems = ((BranchStationaryIssue)objBranchStationaryIssue).gvStatItemsDetails;
                    AddItemsToGridBranchStationaryIssue(dgvStationaryItems);
                }
                else if (IndentType == "StationaryOpeningStock")
                {
                    dgvStationaryItems = ((StationaryOpeningStock)objStationaryOpeningStock).gvItemsDetails;
                    AddItemsToGridStationaryOpeningStock(dgvStationaryItems);
                }
                else if (IndentType == "StationaryDC")
                {
                    dgvStationaryItems = ((StationaryDCByHand)objStationaryDCByHand).gvIndentDetails;
                    AddItemsToGridStationaryDCByHand(dgvStationaryItems);
                }
                else if (IndentType == "StationaryBroucher")
                {
                    dgvStationaryItems = ((StationaryBrocher)objStationaryBroucher).gvIndentDetails;
                    AddItemsToGridStationaryBroucher(dgvStationaryItems);
                }
                else if (IndentType == "ST_PURCHASE_GRN")
                {
                    dgvStationaryItems = ((StationaryBrGRN)objStationaryBrGRN).gvIndentDetails;
                    AddStationaryItemsToGridForPurchaseGRN(dgvStationaryItems);
                }
            }
            else
            {
                MessageBox.Show("Select Items!", "StationaryItems Search");
            }
        }

        private void AddItemsToGridStationaryShortageDetl(DataGridView dgvItems)
        {
            string[] strArrItems = null;
            bool isItemExisted = false;
            int intRow = 0;

            intRow = dgvItems.Rows.Count + 1;

            if (CheckData())
            {

                for (int i = 0; i < tvItems.Nodes[0].Nodes.Count; i++)
                {
                    for (int j = 0; j < tvItems.Nodes[0].Nodes[i].Nodes.Count; j++)
                    {
                        if (tvItems.Nodes[0].Nodes[i].Nodes[j].Checked == true)
                        {
                            string strItemId = tvItems.Nodes[0].Nodes[i].Nodes[j].Name.ToString();//((NewCheckboxListItem)(clbProduct.Items[i])).Tag;
                            strArrItems = strItemId.Split('~');
                            for (int nRow = 0; nRow < dgvItems.Rows.Count; nRow++)
                            {
                                if (dgvItems.Rows[nRow].Cells["ItemID"].Value.ToString().Trim() == strArrItems[0].ToString().Trim())
                                {
                                    isItemExisted = true;

                                }
                            }
                            if ((Convert.ToDouble(strArrItems[1]) > 0 || (Convert.ToDouble(strArrItems[1]) > 0)) && isItemExisted == false)
                            {
                                DataGridViewRow tempRow = new DataGridViewRow();
                                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                                cellSLNO.Value = intRow;
                                tempRow.Cells.Add(cellSLNO);

                                DataGridViewCell cellItemID = new DataGridViewTextBoxCell();
                                cellItemID.Value = strArrItems[0];
                                tempRow.Cells.Add(cellItemID);

                                DataGridViewCell cellItemName = new DataGridViewTextBoxCell();
                                cellItemName.Value = tvItems.Nodes[0].Nodes[i].Nodes[j].Text;
                                tempRow.Cells.Add(cellItemName);

                                //DataGridViewCell cellAvaiQty = new DataGridViewTextBoxCell();
                                //cellAvaiQty.Value = strArrItems[2];
                                //tempRow.Cells.Add(cellAvaiQty);

                                DataGridViewCell cellItemQty = new DataGridViewTextBoxCell();
                                cellItemQty.Value = "";
                                tempRow.Cells.Add(cellItemQty);

                                intRow = intRow + 1;
                                dgvItems.Rows.Add(tempRow);
                            }
                        }
                        isItemExisted = false;
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Select Product!", "Product Search");
            }
        }


        private void AddStationaryItemsToGridForPurchaseGRN(DataGridView dgvItems)
        {
            string[] strArrItems = null;
            bool isItemExisted = false;
            int intRow = 0;

            intRow = dgvItems.Rows.Count + 1;

            if (CheckData())
            {

                for (int i = 0; i < tvItems.Nodes[0].Nodes.Count; i++)
                {
                    for (int j = 0; j < tvItems.Nodes[0].Nodes[i].Nodes.Count; j++)
                    {
                        if (tvItems.Nodes[0].Nodes[i].Nodes[j].Checked == true)
                        {
                            string strItemId = tvItems.Nodes[0].Nodes[i].Nodes[j].Name.ToString();//((NewCheckboxListItem)(clbProduct.Items[i])).Tag;
                            strArrItems = strItemId.Split('~');
                            for (int nRow = 0; nRow < dgvItems.Rows.Count; nRow++)
                            {
                                if (dgvItems.Rows[nRow].Cells["ItemID"].Value.ToString().Trim() == strArrItems[0].ToString().Trim())
                                {
                                    isItemExisted = true;

                                }
                            }
                            if ((Convert.ToDouble(strArrItems[1]) > 0 || (Convert.ToDouble(strArrItems[1]) > 0)) && isItemExisted == false)
                            {
                                DataGridViewRow tempRow = new DataGridViewRow();
                                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                                cellSLNO.Value = intRow;
                                tempRow.Cells.Add(cellSLNO);

                                DataGridViewCell cellItemID = new DataGridViewTextBoxCell();
                                cellItemID.Value = strArrItems[0];
                                tempRow.Cells.Add(cellItemID);

                                DataGridViewCell cellItemName = new DataGridViewTextBoxCell();
                                cellItemName.Value = tvItems.Nodes[0].Nodes[i].Nodes[j].Text;
                                tempRow.Cells.Add(cellItemName);

                                DataGridViewCell cellIQty1 = new DataGridViewTextBoxCell();
                                cellIQty1.Value = "0";
                                tempRow.Cells.Add(cellIQty1);

                                DataGridViewCell cellIQty2 = new DataGridViewTextBoxCell();
                                cellIQty2.Value = "0";
                                tempRow.Cells.Add(cellIQty2);

                                DataGridViewCell cellIQty3 = new DataGridViewTextBoxCell();
                                cellIQty3.Value = "0";
                                tempRow.Cells.Add(cellIQty3);

                                DataGridViewCell cellRate = new DataGridViewTextBoxCell();
                                cellRate.Value = Convert.ToDouble(strArrItems[1]).ToString("f");
                                tempRow.Cells.Add(cellRate);

                                DataGridViewCell cellAmount = new DataGridViewTextBoxCell();
                                cellAmount.Value = "0";
                                tempRow.Cells.Add(cellAmount);

                                DataGridViewCell cellFrom = new DataGridViewTextBoxCell();
                                cellFrom.Value = "";
                                tempRow.Cells.Add(cellFrom);

                                DataGridViewCell cellTo = new DataGridViewTextBoxCell();
                                cellTo.Value = "";
                                tempRow.Cells.Add(cellTo);


                                intRow = intRow + 1;
                                dgvItems.Rows.Add(tempRow);
                            }
                        }
                        isItemExisted = false;

                    }

                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Select Product!", "Product Search");
            }
        }



        private void AddItemsToGridStationaryBroucher(DataGridView dgvItems)
        {
            string[] strArrItems = null;
            bool isItemExisted = false;
            int intRow = 0;

            intRow = dgvItems.Rows.Count + 1;

            if (CheckData())
            {

                for (int i = 0; i < tvItems.Nodes[0].Nodes.Count; i++)
                {
                    for (int j = 0; j < tvItems.Nodes[0].Nodes[i].Nodes.Count; j++)
                    {
                        for (int k = 0; k < tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes.Count; k++)
                        {
                            for (int l = 0; l < tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Nodes.Count; l++)
                            {
                                for (int m = 0; m < tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Nodes[l].Nodes.Count; m++)
                                {
                                    if (tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Nodes[l].Nodes[m].Checked == true)
                                    {
                                        string strItemId = tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Nodes[l].Nodes[m].Name.ToString();//((NewCheckboxListItem)(clbProduct.Items[i])).Tag;
                                        strArrItems = strItemId.Split('~');
                                        for (int nRow = 0; nRow < dgvItems.Rows.Count; nRow++)
                                        {
                                            if (dgvItems.Rows[nRow].Cells["ItemID"].Value.ToString().Trim() == strArrItems[0].ToString().Trim())
                                            {
                                                isItemExisted = true;
                                                break;
                                            }
                                        }
                                        if ((Convert.ToDouble(strArrItems[1]) > 0 || (Convert.ToDouble(strArrItems[1]) > 0)) && isItemExisted == false)
                                        {
                                            DataGridViewRow tempRow = new DataGridViewRow();
                                            DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                                            cellSLNO.Value = intRow;
                                            tempRow.Cells.Add(cellSLNO);

                                            DataGridViewCell cellItemID = new DataGridViewTextBoxCell();
                                            cellItemID.Value = strArrItems[0];
                                            tempRow.Cells.Add(cellItemID);

                                            DataGridViewCell cellItemName = new DataGridViewTextBoxCell();
                                            cellItemName.Value = tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Nodes[l].Nodes[m].Text;
                                            tempRow.Cells.Add(cellItemName);

                                            DataGridViewCell cellIQty1 = new DataGridViewTextBoxCell();
                                            cellIQty1.Value = "0";
                                            tempRow.Cells.Add(cellIQty1);

                                            DataGridViewCell cellIQty2 = new DataGridViewTextBoxCell();
                                            cellIQty2.Value = "0";
                                            tempRow.Cells.Add(cellIQty2);

                                            DataGridViewCell cellIQty3 = new DataGridViewTextBoxCell();
                                            cellIQty3.Value = "";
                                            tempRow.Cells.Add(cellIQty3);

                                            DataGridViewCell cellRate = new DataGridViewTextBoxCell();
                                            cellRate.Value = Convert.ToDouble(strArrItems[1]).ToString("f");
                                            tempRow.Cells.Add(cellRate);

                                            DataGridViewCell cellAmount = new DataGridViewTextBoxCell();
                                            cellAmount.Value = "";
                                            tempRow.Cells.Add(cellAmount);

                                            DataGridViewCell cellFrom = new DataGridViewTextBoxCell();
                                            cellFrom.Value = "";
                                            tempRow.Cells.Add(cellFrom);

                                            DataGridViewCell cellTo = new DataGridViewTextBoxCell();
                                            cellTo.Value = "";
                                            tempRow.Cells.Add(cellTo);

                                            DataGridViewCell cellStatus = new DataGridViewTextBoxCell();
                                            cellStatus.Value = "NEW";
                                            tempRow.Cells.Add(cellStatus);

                                            intRow = intRow + 1;
                                            dgvItems.Rows.Add(tempRow);
                                        }
                                    }
                                    isItemExisted = false;


                                }
                            }
                        }
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Select Product!", "Product Search");
            }
        }


        private void AddItemsToGridStationaryDCByHand(DataGridView dgvItems)
        {
            string[] strArrItems = null;
            bool isItemExisted = false;
            int intRow = 0;

            intRow = dgvItems.Rows.Count + 1;

            if (CheckData())
            {

                for (int i = 0; i < tvItems.Nodes[0].Nodes.Count; i++)
                {
                    for (int j = 0; j < tvItems.Nodes[0].Nodes[i].Nodes.Count; j++)
                    {
                        for (int k = 0; k < tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes.Count; k++)
                        {
                            for (int l = 0; l < tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Nodes.Count; l++)
                            {
                                for (int m = 0; m < tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Nodes[l].Nodes.Count; m++)
                                {
                                    if (tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Nodes[l].Nodes[m].Checked == true)
                                    {
                                        string strItemId = tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Nodes[l].Nodes[m].Name.ToString();//((NewCheckboxListItem)(clbProduct.Items[i])).Tag;
                                        strArrItems = strItemId.Split('~');
                                        for (int nRow = 0; nRow < dgvItems.Rows.Count; nRow++)
                                        {
                                            if (dgvItems.Rows[nRow].Cells["ItemID"].Value.ToString().Trim() == strArrItems[0].ToString().Trim())
                                            {
                                                isItemExisted = true;
                                                break;
                                            }
                                        }
                                        if ((Convert.ToDouble(strArrItems[1]) > 0 || (Convert.ToDouble(strArrItems[1]) > 0)) && isItemExisted == false)
                                        {
                                            DataGridViewRow tempRow = new DataGridViewRow();
                                            DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                                            cellSLNO.Value = intRow;
                                            tempRow.Cells.Add(cellSLNO);

                                            DataGridViewCell cellItemID = new DataGridViewTextBoxCell();
                                            cellItemID.Value = strArrItems[0];
                                            tempRow.Cells.Add(cellItemID);

                                            DataGridViewCell cellItemName = new DataGridViewTextBoxCell();
                                            cellItemName.Value = tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Nodes[l].Nodes[m].Text;
                                            tempRow.Cells.Add(cellItemName);

                                            //DataGridViewCell cellIQty1 = new DataGridViewTextBoxCell();
                                            //cellIQty1.Value = "0";
                                            //tempRow.Cells.Add(cellIQty1);

                                            DataGridViewCell cellIQty2 = new DataGridViewTextBoxCell();
                                            cellIQty2.Value = "";
                                            tempRow.Cells.Add(cellIQty2);

                                            DataGridViewCell cellIQty3 = new DataGridViewTextBoxCell();
                                            cellIQty3.Value = "";
                                            tempRow.Cells.Add(cellIQty3);

                                            DataGridViewCell cellRate = new DataGridViewTextBoxCell();
                                            cellRate.Value = Convert.ToDouble(strArrItems[1]).ToString("f");
                                            tempRow.Cells.Add(cellRate);

                                            DataGridViewCell cellFromNo = new DataGridViewTextBoxCell();
                                            cellFromNo.Value = "0";
                                            tempRow.Cells.Add(cellFromNo);

                                            DataGridViewCell cellToNo = new DataGridViewTextBoxCell();
                                            cellToNo.Value = "0";
                                            tempRow.Cells.Add(cellToNo);

                                            //DataGridViewCell cellAmount = new DataGridViewTextBoxCell();
                                            //cellAmount.Value = "";
                                            //tempRow.Cells.Add(cellAmount);

                                            intRow = intRow + 1;
                                            dgvItems.Rows.Add(tempRow);
                                        }

                                    }
                                    isItemExisted = false;


                                }
                            }
                        }
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Select Product!", "Product Search");
            }
        }

        private void AddItemsToGridStationaryOpeningStock(DataGridView dgvItems)
        {
            string[] strArrItems = null;
            bool isItemExisted = false;
            int intRow = 0;

            intRow = dgvItems.Rows.Count + 1;

            if (CheckData())
            {

                for (int i = 0; i < tvItems.Nodes[0].Nodes.Count; i++)
                {
                    for (int j = 0; j < tvItems.Nodes[0].Nodes[i].Nodes.Count; j++)
                    {
                        if (tvItems.Nodes[0].Nodes[i].Nodes[j].Checked == true)
                        {
                            string strItemId = tvItems.Nodes[0].Nodes[i].Nodes[j].Name.ToString();//((NewCheckboxListItem)(clbProduct.Items[i])).Tag;
                            strArrItems = strItemId.Split('~');
                            for (int nRow = 0; nRow < dgvItems.Rows.Count; nRow++)
                            {
                                if (dgvItems.Rows[nRow].Cells["ItemID"].Value.ToString().Trim() == strArrItems[0].ToString().Trim())
                                {
                                    isItemExisted = true;

                                }
                            }
                            if ((Convert.ToDouble(strArrItems[1]) > 0 || (Convert.ToDouble(strArrItems[1]) > 0)) && isItemExisted == false)
                            {
                                DataGridViewRow tempRow = new DataGridViewRow();
                                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                                cellSLNO.Value = intRow;
                                tempRow.Cells.Add(cellSLNO);

                                DataGridViewCell cellItemID = new DataGridViewTextBoxCell();
                                cellItemID.Value = strArrItems[0];
                                tempRow.Cells.Add(cellItemID);

                                DataGridViewCell cellItemName = new DataGridViewTextBoxCell();
                                cellItemName.Value = tvItems.Nodes[0].Nodes[i].Nodes[j].Text;
                                tempRow.Cells.Add(cellItemName);
                                intRow = intRow + 1;
                                dgvItems.Rows.Add(tempRow);
                            }
                        }
                        isItemExisted = false;
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Select Product!", "Product Search");
            }
        }

        private void AddItemsToGridBranchStationaryIssue(DataGridView dgvItems)
        {
            string[] strArrItems = null;
            bool isItemExisted = false;
            int intRow = 0;

            intRow = dgvItems.Rows.Count + 1;

            if (CheckData())
            {

                for (int i = 0; i < tvItems.Nodes[0].Nodes.Count; i++)
                {
                    for (int j = 0; j < tvItems.Nodes[0].Nodes[i].Nodes.Count; j++)
                    {
                        if (tvItems.Nodes[0].Nodes[i].Nodes[j].Checked == true)
                        {
                            string strItemId = tvItems.Nodes[0].Nodes[i].Nodes[j].Name.ToString();//((NewCheckboxListItem)(clbProduct.Items[i])).Tag;
                            strArrItems = strItemId.Split('~');
                            for (int nRow = 0; nRow < dgvItems.Rows.Count; nRow++)
                            {
                                if (dgvItems.Rows[nRow].Cells["ItemID"].Value.ToString().Trim() == strArrItems[0].ToString().Trim())
                                {
                                    isItemExisted = true;
                                    
                                }
                            }
                            if ((Convert.ToDouble(strArrItems[1]) > 0 || (Convert.ToDouble(strArrItems[1]) > 0)) && isItemExisted == false)
                            {
                                DataGridViewRow tempRow = new DataGridViewRow();
                                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                                cellSLNO.Value = intRow;
                                tempRow.Cells.Add(cellSLNO);

                                DataGridViewCell cellItemID = new DataGridViewTextBoxCell();
                                cellItemID.Value = strArrItems[0];
                                tempRow.Cells.Add(cellItemID);

                                DataGridViewCell cellItemName = new DataGridViewTextBoxCell();
                                cellItemName.Value = tvItems.Nodes[0].Nodes[i].Nodes[j].Text;
                                tempRow.Cells.Add(cellItemName);

                                DataGridViewCell cellAvaiQty = new DataGridViewTextBoxCell();
                                cellAvaiQty.Value = strArrItems[2];
                                tempRow.Cells.Add(cellAvaiQty);

                                DataGridViewCell cellItemQty = new DataGridViewTextBoxCell();
                                cellItemQty.Value = "";
                                tempRow.Cells.Add(cellItemQty);

                                intRow = intRow + 1;
                                dgvItems.Rows.Add(tempRow);
                            }
                        }
                        isItemExisted = false;
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Select Product!", "Product Search");
            }
        }

        private bool CheckData()
        {
            //throw new NotImplementedException();
            return true;
        }
        private void AddItemsToGridStationaryGRN(DataGridView dgvItems)
        {
            string[] strArrItems = null;
            bool isItemExisted = false;
            int intRow = 0;

            intRow = dgvItems.Rows.Count + 1;

            if (CheckData())
            {

                for (int i = 0; i < tvItems.Nodes[0].Nodes.Count; i++)
                {
                    for (int j = 0; j < tvItems.Nodes[0].Nodes[i].Nodes.Count; j++)
                    {
                        for (int k = 0; k < tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes.Count; k++)
                        {
                            for (int l = 0; l < tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Nodes.Count; l++)
                            {
                                for (int m = 0; m < tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Nodes[l].Nodes.Count; m++)
                                {
                                    if (tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Nodes[l].Nodes[m].Checked == true)
                                    {
                                        string strItemId = tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Nodes[l].Nodes[m].Name.ToString();
                                        strArrItems = strItemId.Split('~');
                                        for (int nRow = 0; nRow < dgvItems.Rows.Count; nRow++)
                                        {
                                            if (dgvItems.Rows[nRow].Cells["ItemID"].Value.ToString().Trim() == strArrItems[0].ToString().Trim())
                                            {
                                                isItemExisted = true;
                                                break;
                                            }
                                        }
                                        if ((Convert.ToDouble(strArrItems[1]) > 0 || (Convert.ToDouble(strArrItems[1]) > 0)) && isItemExisted == false)
                                        {
                                            DataGridViewRow tempRow = new DataGridViewRow();
                                            DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                                            cellSLNO.Value = intRow;
                                            tempRow.Cells.Add(cellSLNO);

                                            DataGridViewCell cellItemID = new DataGridViewTextBoxCell();
                                            cellItemID.Value = strArrItems[0];
                                            tempRow.Cells.Add(cellItemID);

                                            DataGridViewCell cellItemName = new DataGridViewTextBoxCell();
                                            cellItemName.Value = tvItems.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Nodes[l].Nodes[m].Text;
                                            tempRow.Cells.Add(cellItemName);

                                            DataGridViewCell cellItemQty = new DataGridViewTextBoxCell();
                                            cellItemQty.Value = "0";
                                            tempRow.Cells.Add(cellItemQty);

                                            DataGridViewCell cellItemQty1 = new DataGridViewTextBoxCell();
                                            cellItemQty1.Value = "";
                                            tempRow.Cells.Add(cellItemQty1);

                                            DataGridViewCell cellItemQty2 = new DataGridViewTextBoxCell();
                                            cellItemQty2.Value = "";
                                            tempRow.Cells.Add(cellItemQty2);

                                            DataGridViewCell cellFromNo = new DataGridViewTextBoxCell();
                                            cellFromNo.Value = "0";
                                            tempRow.Cells.Add(cellFromNo);

                                            DataGridViewCell cellToNo = new DataGridViewTextBoxCell();
                                            cellToNo.Value = "0";
                                            tempRow.Cells.Add(cellToNo);

                                         
                                            intRow = intRow + 1;
                                            dgvItems.Rows.Add(tempRow);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Select Product!", "Product Search");
            }
        }

        private void AddItemsToGridStationaryIndent(DataGridView dgvItems)
        {
            string[] strArrItems = null;
            bool isItemExisted = false;
            int intRow = 0;

            intRow = dgvItems.Rows.Count + 1;

            if (CheckData())
            {

                for (int i = 0; i < tvItems.Nodes[0].Nodes.Count; i++)
                {
                    for (int j = 0; j < tvItems.Nodes[0].Nodes[i].Nodes.Count; j++)
                    {
                        if (tvItems.Nodes[0].Nodes[i].Nodes[j].Checked == true)
                        {
                            string strItemId = tvItems.Nodes[0].Nodes[i].Nodes[j].Name.ToString();//((NewCheckboxListItem)(clbProduct.Items[i])).Tag;
                            strArrItems = strItemId.Split('~');
                            for (int nRow = 0; nRow < dgvItems.Rows.Count; nRow++)
                            {
                                if (dgvItems.Rows[nRow].Cells["ItemID"].Value.ToString().Trim() == strArrItems[0].ToString().Trim())
                                {
                                    isItemExisted = true;
                                    break;
                                }
                            }
                            if ((Convert.ToDouble(strArrItems[1]) > 0 || (Convert.ToDouble(strArrItems[1]) > 0)) && isItemExisted == false)
                            {
                                DataGridViewRow tempRow = new DataGridViewRow();
                                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                                cellSLNO.Value = intRow;
                                tempRow.Cells.Add(cellSLNO);

                                DataGridViewCell cellItemID = new DataGridViewTextBoxCell();
                                cellItemID.Value = strArrItems[0];
                                tempRow.Cells.Add(cellItemID);

                                DataGridViewCell cellItemName = new DataGridViewTextBoxCell();
                                cellItemName.Value = tvItems.Nodes[0].Nodes[i].Nodes[j].Text;
                                tempRow.Cells.Add(cellItemName);

                                DataGridViewCell cellAvailQty = new DataGridViewTextBoxCell();
                                cellAvailQty.Value = strArrItems[2];
                                tempRow.Cells.Add(cellAvailQty);

                                DataGridViewCell cellReqQty = new DataGridViewTextBoxCell();
                                cellReqQty.Value = "";
                                tempRow.Cells.Add(cellReqQty);

                                DataGridViewCell cellApprQty = new DataGridViewTextBoxCell();
                                cellApprQty.Value = "";
                                tempRow.Cells.Add(cellApprQty);

                                DataGridViewCell cellRate = new DataGridViewTextBoxCell();
                                cellRate.Value = Convert.ToDouble(strArrItems[1]).ToString("f");
                                tempRow.Cells.Add(cellRate);

                                DataGridViewCell cellAmount = new DataGridViewTextBoxCell();
                                cellAmount.Value = "";
                                tempRow.Cells.Add(cellAmount);

                                DataGridViewCell cellDBAvailQty = new DataGridViewTextBoxCell();
                                cellDBAvailQty.Value = strArrItems[2];
                                tempRow.Cells.Add(cellDBAvailQty);

                                intRow = intRow + 1;
                                dgvItems.Rows.Add(tempRow);

                                dgvItems.Columns[5].ReadOnly = true;
                                dgvItems.Columns[3].ReadOnly = false;
                                dgvItems.Columns[4].ReadOnly = false;
                            }
                        }
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Select Product!", "Product Search");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
