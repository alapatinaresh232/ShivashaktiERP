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
    public partial class IndentProductSearch : Form
    {
        private IndentDB objIndent = null;
        private InvoiceDB objInv = null;
        private string strFromIndent = string.Empty;
        private string strStateCode = string.Empty;
        private string strBranchCode = string.Empty;
        private string sBranchCode = string.Empty;
        public StockIndentFRM objStockIndentFRM;
        public DeliveryChallan objDeliveryChallan;
        public StockPointGRN objStockPointGRN;
        private string strIndType = string.Empty;
        private Master objMaster = null;
        public StockPointDCR objStockPointDCR;

        public IndentProductSearch(string sIndType)
        {
            InitializeComponent();
            strIndType = sIndType;
        }

        public IndentProductSearch(string sIndType, string sBranch)
        {
            InitializeComponent();
            strIndType = sIndType;
            sBranchCode = sBranch;
        }

        private void ProductSearch_Load(object sender, EventArgs e)
        {
            FillBranchGroupData();
            FillBranchData();
            FillCategoryComboBox();
            //cbGroup.SelectedIndex = 0;
            cbBranches.SelectedIndex = 0;
            if (cbCategory.SelectedIndex > -1)
            {
                cbCategory.SelectedIndex = 0;
                //FillProducts();
            }
            FillProducts();
            cbGroup.Focus();
        }

        private void FillBranchData()
        {
            IndentDB objIndent = new IndentDB();
            try
            {
                DataTable dtBranch = objIndent.IndentStockPointList_Get(CommonData.CompanyCode.ToString()).Tables[0];
                if (dtBranch.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dtBranch.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = dataRow["branch_code"].ToString();
                        oclBox.Text = dataRow["branch_name"].ToString();
                        cbBranches.Items.Add(oclBox);
                        oclBox = null;
                    }

                 }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (cbBranches.SelectedIndex > -1)
                {
                    strBranchCode = ((System.Data.DataRowView)(cbBranches.SelectedItem)).Row.ItemArray[0].ToString();
                    FillBranchGroupData();
                }
                objIndent = null;
            }

        }

        private void FillBranchGroupData()
        {
            IndentDB objIndent = new IndentDB();
            try
            {
                cbGroup.DataSource=null;
                cbGroup.Items.Clear();
                DataTable dtGroup = objIndent.IndentGroupList_Get(CommonData.CompanyCode.ToString(),CommonData.BranchCode,CommonData.DocMonth).Tables[0];
                if (dtGroup.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dtGroup.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        string[] ename = dataRow["ENAME"].ToString().Split('~');
                        oclBox.Tag = dataRow["ENAME"].ToString();
                        oclBox.Text = dataRow["GroupName"].ToString();
                        cbGroup.Items.Add(oclBox);
                        oclBox = null;
                    }

                }
                dtGroup = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objIndent = null;
            }

        }

        //private void FillBranchGroupEcodeData()
        //{
        //    IndentDB objIndent = new IndentDB();
        //    try
        //    {
        //        cbEcode.DataSource = null;
        //        cbEcode.Items.Clear();
        //        DataTable dtEcode = objIndent.IndentGroupList_Get(CommonData.CompanyCode, cbBranches.SelectedValue.ToString(), cbGroup.SelectedValue.ToString()).Tables[0];
        //        if (dtEcode.Rows.Count > 0)
        //        {
        //            cbEcode.DataSource = dtEcode;
        //            cbEcode.DisplayMember = "ECODE";
        //            cbEcode.ValueMember = "ENAME";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        objIndent = null;
        //    }

        //}

        private void FillCategoryComboBox()
        {
            objMaster = new Master();
            try
            {
                cbCategory.DataBindings.Clear();

                cbCategory.DataSource = null;
                cbCategory.Items.Clear();
                DataTable dt = objMaster.GetProductCategory().Tables[0];
                DataRow dr = dt.NewRow();
                dr[0] = "0";
                dr[1] = "All Products";
                dt.Rows.InsertAt(dr, 0);
                dr = null;
                if (dt.Rows.Count > 1)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    { 
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = dataRow["CATEGORY_ID"].ToString();
                        oclBox.Text = dataRow["CATEGORY_NAME"].ToString();
                        cbCategory.Items.Add(oclBox);
                        oclBox = null;
                    }
                }

                cbCategory.SelectedIndex = 0;
                dt = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                objMaster = null;
            }
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCategory.SelectedIndex > -1)
            {
                FillProducts();
            }
        }

        private void FillProducts()
        {
            objIndent = new IndentDB();
            DataSet ds = new DataSet();
            string strCatId = string.Empty;
            string strSelectedCatgId = string.Empty;
            tvProducts.Nodes.Clear();
            if (cbCategory.SelectedIndex > -1)
            {
                strSelectedCatgId = ((NewCheckboxListItem)(cbCategory.SelectedItem)).Tag.ToString();
                if (strSelectedCatgId == "0")
                    strSelectedCatgId = string.Empty;
            }
            //if (strIndType == "SPDC")
            //    ds = objIndent.IndentProductList_Get(CommonData.CompanyCode, sBranchCode, strSelectedCatgId);
            //else if(strIndType == "SPDCST")
                ds = objIndent.IndentDCSTProductList_Get(CommonData.CompanyCode, sBranchCode, strSelectedCatgId);
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
                        tvProducts.Nodes[0].Nodes[intNode].Nodes.Add(ds.Tables[0].Rows[i]["ProductID"].ToString() + '@' + ds.Tables[0].Rows[i]["RATE"].ToString() , ds.Tables[0].Rows[i]["ProductName"].ToString());
                        intNode += 1;
                        
                    }
                    else
                        tvProducts.Nodes[0].Nodes[intNode - 1].Nodes.Add(ds.Tables[0].Rows[i]["ProductID"].ToString() + '@' + ds.Tables[0].Rows[i]["RATE"].ToString(), ds.Tables[0].Rows[i]["ProductName"].ToString());
                    
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
                if (strIndType == "IndentToBranch")
                {
                    dgvProducts = ((StockIndentFRM)objStockIndentFRM).gvIndentDetails;
                    AddIndentItemsToGrid(dgvProducts);
                }
                if (strIndType == "SPDC")
                {
                    dgvProducts = ((DeliveryChallan)objDeliveryChallan).gvIndentDetails;
                    AddDCItemsToGrid(dgvProducts);
                }
                if (strIndType == "SPDCST")
                {
                    dgvProducts = ((DeliveryChallan)objDeliveryChallan).gvIndentDetails;
                    AddDCItemsToGrid(dgvProducts);
                }
                if (strIndType == "SPGRN")
                {
                    dgvProducts = ((StockPointGRN)objStockPointGRN).gvGRNDetails;
                    AddGRNItemsToGrid(dgvProducts);
                }
                if (strIndType == "SPDCR")
                {
                    dgvProducts = ((StockPointDCR)objStockPointDCR).gvIndentDetails;
                    AddDCItemsToGrid(dgvProducts);
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Select Product!", "Product Search");
            }
        }

        private void AddGRNItemsToGrid(DataGridView dgvProducts)
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
                            if (dgvProducts.Rows[nRow].Cells["ProductID"].Value.ToString().Trim() == strProduct.Substring(0,10))
                            {
                                isItemExisted = true;
                                break;
                            }
                        }
                        if (isItemExisted == false)
                        {
                            dgvProducts.Rows.Add();
                            dgvProducts.Rows[intRow].Cells["SLNO"].Value = intRow + 1;
                            string[] strArrProduct = tvProducts.Nodes[0].Nodes[i].Nodes[j].Name.Split('@');
                            dgvProducts.Rows[intRow].Cells["ProductId"].Value = strArrProduct[0].ToString();
                            dgvProducts.Rows[intRow].Cells["Category"].Value = tvProducts.Nodes[0].Nodes[i].Text;
                            dgvProducts.Rows[intRow].Cells["Product"].Value = tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;
                            dgvProducts.Rows[intRow].Cells["BatchNo"].Value = "GENERAL";
                            dgvProducts.Rows[intRow].Cells["DCQty"].Value = "0";
                            dgvProducts.Rows[intRow].Cells["RecdQty"].Value = "";
                            dgvProducts.Rows[intRow].Cells["DamageQty"].Value = "0";
                            dgvProducts.Rows[intRow].Cells["RATE"].Value = strArrProduct[1].ToString();
                            dgvProducts.Rows[intRow].Cells["Amount"].Value = "0";
                            dgvProducts.Rows[intRow].Cells["DCNo"].Value = "";
                            dgvProducts.Rows[intRow].Cells["DCSLNo"].Value = intRow + 1;

                            intRow = intRow + 1;
                        }

                    }
                }
            }
            this.Close();
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
                            if (dgvProducts.Rows[nRow].Cells["ProductID"].Value.ToString().Trim() == strProduct.Substring(0,10))
                            {
                                isItemExisted = true;
                                break;
                            }
                        }
                        if (isItemExisted == false)
                        {
                            dgvProducts.Rows.Add();
                            dgvProducts.Rows[intRow].Cells["SLNO"].Value = intRow + 1;
                            dgvProducts.Rows[intRow].Cells["Group"].Value = ((NewCheckboxListItem)(cbGroup.SelectedItem)).Text.ToString();
                            string[] strArrGlCode = ((NewCheckboxListItem)(cbGroup.SelectedItem)).Tag.ToString().Split('~');
                            dgvProducts.Rows[intRow].Cells["GLCode"].Value = strArrGlCode[0];
                            dgvProducts.Rows[intRow].Cells["EcodeName"].Value = strArrGlCode[1];
                            dgvProducts.Rows[intRow].Cells["StockPoint"].Value = ((NewCheckboxListItem)(cbBranches.SelectedItem)).Text.ToString();
                            dgvProducts.Rows[intRow].Cells["Category"].Value = tvProducts.Nodes[0].Nodes[i].Text;
                            dgvProducts.Rows[intRow].Cells["Product"].Value = tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;
                            dgvProducts.Rows[intRow].Cells["Qty"].Value = "";
                            string[] strArrProduct = tvProducts.Nodes[0].Nodes[i].Nodes[j].Name.Split('@');
                            dgvProducts.Rows[intRow].Cells["ProductId"].Value = strArrProduct[0].ToString();
                            dgvProducts.Rows[intRow].Cells["BranchCode"].Value = ((NewCheckboxListItem)(cbBranches.SelectedItem)).Tag.ToString(); 

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
                            if (dgvProducts.Rows[nRow].Cells["ProductID"].Value.ToString().Trim() == strProduct.Substring(0,10))
                            {
                                isItemExisted = true;
                                break;
                            }
                        }
                        if (isItemExisted == false)
                        {
                            dgvProducts.Rows.Add();
                            dgvProducts.Rows[intRow].Cells["SLNO"].Value = intRow + 1;
                            string[] strArrProduct = tvProducts.Nodes[0].Nodes[i].Nodes[j].Name.Split('@');
                            dgvProducts.Rows[intRow].Cells["ProductId"].Value = strArrProduct[0].ToString();
                            dgvProducts.Rows[intRow].Cells["Category"].Value = tvProducts.Nodes[0].Nodes[i].Text;
                            dgvProducts.Rows[intRow].Cells["Product"].Value = tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;
                            dgvProducts.Rows[intRow].Cells["BatchNo"].Value = "GEN";
                            dgvProducts.Rows[intRow].Cells["IndQty"].Value = "0";
                            dgvProducts.Rows[intRow].Cells["IssQty"].Value = "";
                            dgvProducts.Rows[intRow].Cells["DmgQty"].Value = "0";
                            dgvProducts.Rows[intRow].Cells["TotalQty"].Value = "0";
                            dgvProducts.Rows[intRow].Cells["RATE"].Value = strArrProduct[1].ToString();
                            dgvProducts.Rows[intRow].Cells["Amount"].Value = "0";
                            dgvProducts.Rows[intRow].Cells["IndentNo"].Value = "0";
                            dgvProducts.Rows[intRow].Cells["IndentSLNo"].Value = intRow+1;
                            
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
            if (strIndType != "SPDC" && strIndType != "SPGRN" && strIndType != "SPDCST" && strIndType != "SPDCR")
            {
                if (cbGroup.SelectedIndex == -1)
                {
                    MessageBox.Show("Select group", "Indent Products");
                    cbGroup.Focus();
                    return false;
                }
            }
            if (cbBranches.SelectedIndex == -1)
            {
                MessageBox.Show("Select Stack point", "Indent Products");
                cbBranches.Focus();
                return false;
            }
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

        private void cbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbGroup.SelectedIndex > -1)
            {
                string[] strArrGlCode = ((NewCheckboxListItem)(cbGroup.SelectedItem)).Tag.ToString().Split('~');
                lblGroupEcode.Text = strArrGlCode[1].ToString();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void IndentProductSearch_Activated(object sender, EventArgs e)
        {
            if (strIndType == "IndentToBranch")
            {
                gbIndent.Enabled = true;
            }
            else
            {
                gbIndent.Enabled = false;
            }
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
