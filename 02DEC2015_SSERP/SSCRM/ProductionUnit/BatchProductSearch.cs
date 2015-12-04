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
namespace SSCRM
{
    public partial class BatchProductSearch : Form
    {
        private ProductUnitDB objPU = null;
        private string strStateCode = string.Empty;
        private string strBranchCode = string.Empty;
        public DeliveryChallanPU objDeliveryChallanPU;
        private string strDcType = string.Empty;
        private Master objMaster = null;
        public BatchProductSearch(string sDcType)
        {
            InitializeComponent();
            strDcType = sDcType;
        }

        private void ProductSearch_Load(object sender, EventArgs e)
        {

          
            FillProducts();

        }
        
        private void FillProducts()
        {
            objPU = new  ProductUnitDB();
            DataSet ds = new DataSet();
            string strCatId = string.Empty;
            string strProduct = string.Empty;
            if (txtProductName.Text.ToString().Trim().Length > 2)
                strProduct = txtProductName.Text;
            tvProducts.Nodes.Clear();
            ds = objPU.BatchProductList_Get(CommonData.CompanyCode, CommonData.BranchCode, strProduct);
            TreeNode tNode;
            tNode = tvProducts.Nodes.Add("Products");
            Int16 intNode = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (strCatId != ds.Tables[0].Rows[i]["CategoryID"].ToString())
                    {
                        strCatId = ds.Tables[0].Rows[i]["CategoryID"].ToString();
                        tvProducts.Nodes[0].Nodes.Add(ds.Tables[0].Rows[i]["CategoryID"].ToString(), ds.Tables[0].Rows[i]["CategoryName"].ToString());
                        tvProducts.Nodes[0].Nodes[intNode].Nodes.Add(ds.Tables[0].Rows[i]["ProductID"].ToString() + '@' + ds.Tables[0].Rows[i]["BATCH_NO"].ToString() + '@' + ds.Tables[0].Rows[i]["RATE"].ToString() + '@' + ds.Tables[0].Rows[i]["BATCH_DATE"].ToString() + '@' + ds.Tables[0].Rows[i]["BATCH_EXP_DATE"].ToString(), ds.Tables[0].Rows[i]["ProductName"].ToString());
                        intNode += 1;
                        
                    }
                    else
                        tvProducts.Nodes[0].Nodes[intNode - 1].Nodes.Add(ds.Tables[0].Rows[i]["ProductID"].ToString() + '@' + ds.Tables[0].Rows[i]["BATCH_NO"].ToString() + '@' + ds.Tables[0].Rows[i]["RATE"].ToString() + '@' + ds.Tables[0].Rows[i]["BATCH_DATE"].ToString() + '@' + ds.Tables[0].Rows[i]["BATCH_EXP_DATE"].ToString(), ds.Tables[0].Rows[i]["ProductName"].ToString());
                    
                }


            }
            if (this.tvProducts.Nodes[0].Nodes.Count > 0)
            {
                if (this.tvProducts.Nodes[0].Nodes[0].Nodes.Count > 1)
                {
                    this.tvProducts.SelectedNode = tNode.Nodes[0];
                    this.tvProducts.SelectedNode.Expand();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DataGridView dgvProducts;
            if (CheckData())
            {
                if (strDcType == "DCfromPU")
                {
                    dgvProducts = ((DeliveryChallanPU)objDeliveryChallanPU).gvReqDetails;
                    AddDCItemsToGrid(dgvProducts);
                }
                
                this.Close();
            }
            else
            {
                MessageBox.Show("Select Product!", "Product Search");
            }
        }
        private void AddIndentItemsToGrid(DataGridView dgvProducts)
        {
            bool isItemExisted = false;
            int intRow = 0;

            intRow = dgvProducts.Rows.Count;

            for (int i = 0; i < tvProducts.Nodes[0].Nodes.Count; i++)
            {
                for (int j = 0; j < tvProducts.Nodes[0].Nodes[i].Nodes.Count; j++)
                {
                    if (tvProducts.Nodes[0].Nodes[i].Nodes[j].Checked == true)
                    {
                        string strProduct = tvProducts.Nodes[0].Nodes[i].Nodes[j].Name.ToString();
                        isItemExisted = false;
                        for (int nRow = 0; nRow < dgvProducts.Rows.Count; nRow++)
                        {
                            if (dgvProducts.Rows[nRow].Cells["ProductID"].Value.ToString().Trim() == strProduct.Trim())
                            {
                                isItemExisted = true;
                                break;
                            }
                        }
                        if (isItemExisted == false)
                        {
                            dgvProducts.Rows.Add();
                            dgvProducts.Rows[intRow].Cells["SLNO"].Value = intRow + 1;
                            dgvProducts.Rows[intRow].Cells["Category"].Value = tvProducts.Nodes[0].Nodes[i].Text;
                            dgvProducts.Rows[intRow].Cells["Product"].Value = tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;
                            dgvProducts.Rows[intRow].Cells["Qty"].Value = "";
                            string[] strArrProduct = tvProducts.Nodes[0].Nodes[i].Nodes[j].Name.Split('@');
                            dgvProducts.Rows[intRow].Cells["ProductId"].Value = strArrProduct[0].ToString();
                            dgvProducts.Rows[intRow].Cells["BatchNo"].Value = strArrProduct[1].ToString();
                            dgvProducts.Rows[intRow].Cells["Rate"].Value = strArrProduct[2].ToString();
                            intRow = intRow + 1;
                        }

                    }
                }
            }
           this.Close();
        }

        private void AddDCItemsToGrid(DataGridView dgvProducts)
        {
            bool isItemExisted = false;
            int intRow = 0;

            intRow = dgvProducts.Rows.Count;

            for (int i = 0; i < tvProducts.Nodes[0].Nodes.Count; i++)
            {
                for (int j = 0; j < tvProducts.Nodes[0].Nodes[i].Nodes.Count; j++)
                {
                    if (tvProducts.Nodes[0].Nodes[i].Nodes[j].Checked == true)
                    {
                        string strProduct = tvProducts.Nodes[0].Nodes[i].Nodes[j].Name.ToString();
                        isItemExisted = false;
                        for (int nRow = 0; nRow < dgvProducts.Rows.Count; nRow++)
                        {
                            if (dgvProducts.Rows[nRow].Cells["ProductID"].Value.ToString().Trim() == strProduct.Trim())
                            {
                                isItemExisted = true;
                                break;
                            }
                        }
                        if (isItemExisted == false)
                        {
                            dgvProducts.Rows.Add();
                            dgvProducts.Rows[intRow].Cells["SLNO"].Value = intRow + 1;
                            dgvProducts.Rows[intRow].Cells["Category"].Value = tvProducts.Nodes[0].Nodes[i].Text;
                            dgvProducts.Rows[intRow].Cells["Product"].Value = tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;
                            dgvProducts.Rows[intRow].Cells["IssQty"].Value = "";
                            string[] strArrProduct = tvProducts.Nodes[0].Nodes[i].Nodes[j].Name.Split('@');
                            dgvProducts.Rows[intRow].Cells["ProductId"].Value = strArrProduct[0].ToString();
                            dgvProducts.Rows[intRow].Cells["BatchNo"].Value = strArrProduct[1].ToString();
                            dgvProducts.Rows[intRow].Cells["Rate"].Value = strArrProduct[2].ToString();
                                                        
                            intRow = intRow + 1;
                        }

                    }
                }
            }
            this.Close();
        }

        private bool CheckData()
        {
            bool blVil = false;
           
            for (int i = 0; i < tvProducts.Nodes[0].Nodes.Count; i++)
            {
                for (int j = 0; j < tvProducts.Nodes[0].Nodes[i].Nodes.Count; j++)
                {
                    if (tvProducts.Nodes[0].Nodes[i].Nodes[j].Checked == true)
                    {
                        blVil = true;
                        break;
                    }
                }
                if (blVil == true)
                    break;
            }
            return blVil;
        }

        private void txtProductName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtProductName.Text.ToString().Trim().Length > 2)
            {
                FillProducts();
            }
        }
    }
}
