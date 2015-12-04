using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.UI.WebControls;
using SSCRMDB;
using SSAdmin;
using SSTrans;
namespace SSCRM
{
    public partial class ProductSearch : Form
    {
        private Master objCategory = null;
        private SQLDB objData = null;
        public Invoice objFrmInvoice;
        public InvoiceBultin objFrmInvoiceBultin;
        public InvoiceTemplateProducts objInvoiceTemplateProducts;
        private InvoiceDB objInv = null;
        private string strFromInvoice = string.Empty;
        public ProductSearch(string strFrom)
        {
            InitializeComponent();
            strFromInvoice = strFrom;
        }

        private void ProductSearch_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.X + 120, Screen.PrimaryScreen.WorkingArea.Y + 100);
            this.StartPosition = FormStartPosition.CenterScreen;
            FillCategoryComboBox();
            if (cbCategory.SelectedIndex > -1)
            {
                cbCategory.SelectedIndex = 0;
                FillProductData(cbCategory.SelectedValue.ToString());
            }
        }

        private void FillCategoryComboBox()
        {
            objCategory = new Master();
            try
            {
                DataTable dt = objCategory.GetProductCategory().Tables[0];
                if (dt.Rows.Count > 1)
                {
                    cbCategory.DataSource = dt;
                    cbCategory.DisplayMember = "CATEGORY_NAME";
                    cbCategory.ValueMember = "CATEGORY_ID";
                }
                dt = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                objCategory = null;
            }
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCategory.SelectedIndex > -1)
            {
                clbProduct.DataSource = null;
                clbProduct.DataBindings.Clear();
                clbProduct.Items.Clear();
                FillProductData(cbCategory.SelectedValue.ToString());
            }
        }

        private void FillProductData(string sCategoryCode)
        {
            objInv = new InvoiceDB();
            try
            {
                DataTable dt = objInv.GetProductMasterSearch(CommonData.CompanyCode, sCategoryCode).Tables[0];
                if (dt.Rows.Count > 1)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        if (Convert.ToInt16(dataRow["pm_single_mrp"] + "") > 0 || Convert.ToInt16(dataRow["pm_bulk_mrp"] + "") > 0)
                        {
                            NewCheckboxListItem oclBox = new NewCheckboxListItem();
                            oclBox.Tag = dataRow["pm_product_id"].ToString() +
                                        "~" + dataRow["pm_product_name"].ToString() +
                                        "~" + dataRow["category_name"].ToString() +
                                        "~" + dataRow["pm_single_mrp"].ToString() +
                                        "~" + dataRow["pm_bulk_mrp"].ToString() +
                                        "~" + dataRow["TIPPoints"].ToString();
                            oclBox.Text = dataRow["pm_product_name"].ToString();
                            clbProduct.Items.Add(oclBox);
                            oclBox = null;
                        }
                    }

                }
                dt = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                objInv = null;
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string[] strArrProduct = null;
            int intRow = ((Invoice)objFrmInvoice).gvProductDetails.Rows.Count + 1;
            bool isItemExisted = false;
            if (CheckData())
            {
                for (int i = 0; i < clbProduct.Items.Count; i++)
                {
                    if (clbProduct.GetItemCheckState(i) == CheckState.Checked)
                    {
                        string strProduct = ((NewCheckboxListItem)(clbProduct.Items[i])).Tag; 
                        strArrProduct = strProduct.Split('~');
                        for (int nRow = 0; nRow < ((Invoice)objFrmInvoice).gvProductDetails.Rows.Count; nRow++)
                        {
                            if (((Invoice)objFrmInvoice).gvProductDetails.Rows[nRow].Cells["ProductID"].Value.ToString().Trim() == strArrProduct[0].ToString().Trim())
                            {
                                isItemExisted = true;
                            }
                        }
                        if ((Convert.ToDouble(strArrProduct[3]) > 0 || (Convert.ToDouble(strArrProduct[4]) > 0)) && isItemExisted == false)  //dt.Rows[i]["pm_single_mrp"] or dt.Rows[i]["PM_BULK_MRP"]
                        {
                            DataGridViewRow tempRow = new DataGridViewRow();
                            DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                            cellSLNO.Value = intRow;
                            tempRow.Cells.Add(cellSLNO);

                            DataGridViewCell cellMainProductID = new DataGridViewTextBoxCell();
                            //cellMainProductID.Value = dt.Rows[i]["pm_product_id"];
                            cellMainProductID.Value = strArrProduct[0];
                            tempRow.Cells.Add(cellMainProductID);

                            DataGridViewCell cellMainProduct = new DataGridViewTextBoxCell();
                            //cellMainProduct.Value = dt.Rows[i]["pm_product_name"];
                            cellMainProduct.Value = strArrProduct[1];
                            tempRow.Cells.Add(cellMainProduct);

                            DataGridViewCell cellSubProduct = new DataGridViewTextBoxCell();
                            //cellSubProduct.Value = dt.Rows[i]["category_name"];
                            cellSubProduct.Value = strArrProduct[2];
                            tempRow.Cells.Add(cellSubProduct);

                            DataGridViewCell cellDessc = new DataGridViewTextBoxCell();
                            //cellDessc.Value = dt.Rows[i]["pm_product_name"];
                            cellDessc.Value = strArrProduct[1];
                            tempRow.Cells.Add(cellDessc);

                            DataGridViewCell cellQTY = new DataGridViewTextBoxCell();
                            cellQTY.Value = "";
                            tempRow.Cells.Add(cellQTY);

                            if (cbCategory.SelectedValue.ToString() == "009")
                            {
                                DataGridViewCell cellRate = new DataGridViewTextBoxCell();
                                //cellRate.Value = dt.Rows[i]["pm_bulk_mrp"];
                                cellRate.Value = strArrProduct[4];
                                tempRow.Cells.Add(cellRate);
                            }
                            else
                            {
                                DataGridViewCell cellRate = new DataGridViewTextBoxCell();
                                //cellRate.Value = dt.Rows[i]["pm_single_mrp"];
                                cellRate.Value = strArrProduct[3];
                                tempRow.Cells.Add(cellRate);
                            }
                            DataGridViewCell cellAmt = new DataGridViewTextBoxCell();
                            cellAmt.Value = "";
                            tempRow.Cells.Add(cellAmt);

                            DataGridViewCell cellPoints = new DataGridViewTextBoxCell();
                            //cellPoints.Value = dt.Rows[i]["TIPPoints"];
                            cellPoints.Value = strArrProduct[5];
                            tempRow.Cells.Add(cellPoints);

                            intRow = intRow + 1;
                            ((Invoice)objFrmInvoice).gvProductDetails.Rows.Add(tempRow);
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

        private bool CheckData()
        {
            bool blVil = false;
            for (int i = 0; i < clbProduct.Items.Count; i++)
            {
                if (clbProduct.GetItemCheckState(i) == CheckState.Checked)
                {
                    blVil = true;
                }
            }
            return blVil;
        }
    }
}
