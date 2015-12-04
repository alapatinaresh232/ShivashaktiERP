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
using SSCRM;
using SSTrans;

namespace SSCRM
{
    public partial class ProductsAdd : Form
    {
        private IndentDB objIndent = null;
        public ProductMasterCompany objProductMasterCompany;
        private string sCompany = CommonData.CompanyCode;
        //private SQLDB objSQLData = null;
               
        public ProductsAdd()
        {
            InitializeComponent();
        }
        public ProductsAdd(string sComp)
        {
            sCompany = sComp;
            InitializeComponent();
        }
                    

        private void AddProducts_Load(object sender, EventArgs e)
        {
            FillProducts();
            
        }
        private void FillProducts()
        {
            objIndent = new IndentDB();
            DataSet ds = new DataSet();
            string strCategoryId = string.Empty;
            tvProducts.Nodes.Clear();
            TreeNode tNode;
            tNode = tvProducts.Nodes.Add("Products");
            try
            {
                ds = objIndent.ProductList_Get(sCompany, strCategoryId);
                
                
                Int16 intNode = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (strCategoryId != ds.Tables[0].Rows[i]["CategoryID"].ToString())
                        {
                            strCategoryId = ds.Tables[0].Rows[i]["CategoryID"].ToString();
                            tvProducts.Nodes[0].Nodes.Add(ds.Tables[0].Rows[i]["CategoryID"].ToString(), ds.Tables[0].Rows[i]["CategoryName"].ToString());
                            tvProducts.Nodes[0].Nodes[intNode].Nodes.Add(ds.Tables[0].Rows[i]["ProductID"].ToString() + '@' + ds.Tables[0].Rows[i]["RATE"].ToString(), ds.Tables[0].Rows[i]["ProductName"].ToString());
                            intNode += 1;

                        }
                        else
                            tvProducts.Nodes[0].Nodes[intNode - 1].Nodes.Add(ds.Tables[0].Rows[i]["ProductID"].ToString() + '@' + ds.Tables[0].Rows[i]["RATE"].ToString(), ds.Tables[0].Rows[i]["ProductName"].ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objIndent = null;
                ds = null;
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
                               
                    dgvProducts = ((ProductMasterCompany)objProductMasterCompany).gvProductDetails;
                    AddProductsToGrid(dgvProducts);
               
               
                this.Close();
            }
            else
            {
                MessageBox.Show("Select Product!", "Add Products ");
            }

        }

        private void AddProductsToGrid(DataGridView dgvProducts)
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
                            if (dgvProducts.Rows[nRow].Cells["ProductName"].Value.ToString().Trim() == strProduct.Substring(0, 10))
                            {
                                dgvProducts.Rows.Add();
                                isItemExisted = true;
                                break;
                            }
                        }
                        if (isItemExisted == false)
                        {
                            dgvProducts.Rows.Add();
                            dgvProducts.Rows[intRow].Cells["SLNO"].Value = intRow + 1;
                            string[] strArrProduct = tvProducts.Nodes[0].Nodes[i].Nodes[j].Name.Split('@');
                            dgvProducts.Rows[intRow].Cells["Category"].Value = tvProducts.Nodes[0].Nodes[i].Text;
                            dgvProducts.Rows[intRow].Cells["ProductName"].Value = tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;
                            dgvProducts.Rows[intRow].Cells["ProductId"].Value = strArrProduct[0].ToString();
                            dgvProducts.Rows[intRow].Cells["CategoryId"].Value = tvProducts.Nodes[0].Nodes[i].Text;                            

                            intRow = intRow + 1;
                        }

                    }
                }
            }
            this.Close();
        }

        private bool CheckData()
        {
            bool Flag = false;
            for (int i = 0; i < tvProducts.Nodes[0].Nodes.Count; i++)
            {
                for (int j = 0; j < tvProducts.Nodes[0].Nodes[i].Nodes.Count; j++)
                {
                    if (tvProducts.Nodes[0].Nodes[i].Nodes[j].Checked == true)
                    {
                        Flag = true;
                        break;
                    }
                }
                if (Flag == true)
                    break;
            }
            return Flag;
        }

        private void tvProducts_AfterCheck(object sender, TreeViewEventArgs e)
        {
            tvProducts.BeginUpdate();

            foreach (TreeNode Node in e.Node.Nodes)
            {
                Node.Checked = e.Node.Checked;
            }

            tvProducts.EndUpdate();

        }

       

    }
}
