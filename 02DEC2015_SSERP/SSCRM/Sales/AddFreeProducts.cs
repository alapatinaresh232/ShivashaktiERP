using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSCRM.App_Code;
namespace SSCRM
{
    public partial class AddFreeProducts : Form
    {
        SQLDB objSQLDB;
        public Invoice objInvoice;
        public SPInvoice objSPInvoice;
        public InvoiceBultin objInvoiceBultin;
        DataGridViewRow dgvrs;
        DataGridView dgview;
        int iRowIndex = 0;
        private string sFrmType = "", strCompany = "";

        public AddFreeProducts()
        {
            InitializeComponent();
        }
        public AddFreeProducts(DataGridView dts)
        {
            InitializeComponent();
            dgview = dts;
        }
        public AddFreeProducts(DataGridView dts,string sType,string sCompCode)
        {
            InitializeComponent();
            dgview = dts;
            sFrmType = sType;
            strCompany = sCompCode;
            
        }
        public AddFreeProducts(int rowIndex, DataGridViewRow dgvr,string sType,string sCompCode)
        {
            InitializeComponent();
            iRowIndex = rowIndex;
            dgvrs = dgvr;
            sFrmType = sType;
            strCompany = sCompCode;
        }
        public AddFreeProducts(int rowIndex, DataGridViewRow dgvr)
        {
            InitializeComponent();
            iRowIndex = rowIndex;
            dgvrs = dgvr;
        }
        private void AddFreeProducts_Load(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            if (sFrmType == "SP_INVOICE")
            {
                if (dgview != null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ProductID");
                    dt.Columns.Add("Product");
                    string sCompare = "";
                    foreach (DataGridViewRow drv in dgview.Rows)
                    {
                        //if (!sCompare.Contains(drv.Cells[1].Value.ToString()))
                        dt.Rows.Add(new object[] { drv.Cells[1].Value.ToString(), drv.Cells[2].Value.ToString() });
                        sCompare += drv.Cells[2].Value.ToString();
                    }
                    UtilityLibrary.PopulateControl(cmbSoldProduct, dt.DefaultView, 1, 0, "-- Please Select --", 0);
                    lblSoldProduct.Visible = true;
                    cmbSoldProduct.Visible = true;
                }
                else
                {
                    lblSoldProduct.Visible = false;
                    cmbSoldProduct.Visible = false;
                }
                string sqlQry = " SELECT PM_PRODUCT_ID,PM_PRODUCT_NAME FROM dbo.PRODUCT_MAS a " +
                    "inner join product_mas_company b on a.pm_product_id=b.pmc_product_id where " +
                    "pmc_product_company='" + strCompany + "' and pm_product_type = 'SNGPK' ORDER BY PM_PRODUCT_NAME";
                UtilityLibrary.PopulateControl(cmbProducts, objSQLDB.ExecuteDataSet(sqlQry).Tables[0].DefaultView, 1, 0, "-- Please Select --", 0);
                objSQLDB = null;
                if (dgvrs != null)
                {
                    cmbProducts.SelectedValue = dgvrs.Cells[10].Value.ToString();
                    txtQty.Text = dgvrs.Cells[8].Value.ToString();
                }

            }
            else
            {
                if (dgview != null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ProductID");
                    dt.Columns.Add("Product");
                    string sCompare = "";
                    foreach (DataGridViewRow drv in dgview.Rows)
                    {
                        //if (!sCompare.Contains(drv.Cells[1].Value.ToString()))
                        dt.Rows.Add(new object[] { drv.Cells[1].Value.ToString(), drv.Cells[2].Value.ToString() });
                        sCompare += drv.Cells[2].Value.ToString();
                    }
                    UtilityLibrary.PopulateControl(cmbSoldProduct, dt.DefaultView, 1, 0, "-- Please Select --", 0);
                    lblSoldProduct.Visible = true;
                    cmbSoldProduct.Visible = true;
                }
                else
                {
                    lblSoldProduct.Visible = false;
                    cmbSoldProduct.Visible = false;
                }
                string sqlQry = " SELECT PM_PRODUCT_ID,PM_PRODUCT_NAME FROM dbo.PRODUCT_MAS a " +
                    "inner join product_mas_company b on a.pm_product_id=b.pmc_product_id where " +
                    "pmc_product_company='" + CommonData.CompanyCode + "' and pm_product_type = 'SNGPK' ORDER BY PM_PRODUCT_NAME";
                UtilityLibrary.PopulateControl(cmbProducts, objSQLDB.ExecuteDataSet(sqlQry).Tables[0].DefaultView, 1, 0, "-- Please Select --", 0);
                objSQLDB = null;
                if (dgvrs != null)
                {
                    cmbProducts.SelectedValue = dgvrs.Cells[9].Value.ToString();
                    txtQty.Text = dgvrs.Cells[8].Value.ToString();
                }
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;
                return;
            }
            //to allow decimals only teak plant
            if (cmbProducts.SelectedText.ToString().Contains("TEAK"))
            {
                if (e.KeyChar == 46)
                {
                    if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                        e.Handled = true;
                }
            }
            else if (e.KeyChar == 46)
            {
                e.Handled = true;
                return;
            }

            //if (e.KeyChar != '\b')
            //{
            //    if (!char.IsDigit((e.KeyChar)))
            //    {
            //        e.Handled = true;
            //    }
            //}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (sFrmType == "SP_INVOICE")
            {
                if (dgvrs != null)
                {
                    ((SPInvoice)objSPInvoice).dgvFreeProduct.Rows[iRowIndex].Cells["FreeProduct"].Value = cmbProducts.Text;
                    ((SPInvoice)objSPInvoice).dgvFreeProduct.Rows[iRowIndex].Cells["FreeQty"].Value = txtQty.Text;
                    ((SPInvoice)objSPInvoice).dgvFreeProduct.Rows[iRowIndex].Cells["FreeProductId"].Value = cmbProducts.SelectedValue.ToString();
                }
                else
                {
                    ((SPInvoice)objSPInvoice).dgvFreeProduct.Rows.Add();
                    int intCurRow = ((SPInvoice)objSPInvoice).dgvFreeProduct.Rows.Count - 1;
                    ((SPInvoice)objSPInvoice).dgvFreeProduct.Rows[intCurRow].Cells["FreeSlNo"].Value = Convert.ToInt32(intCurRow + 1) + "";
                    ((SPInvoice)objSPInvoice).dgvFreeProduct.Rows[intCurRow].Cells["MainProductIdSlNo"].Value = dgview.Rows[0].Cells["SLNO"].Value + "";
                    ((SPInvoice)objSPInvoice).dgvFreeProduct.Rows[intCurRow].Cells["MainProductId"].Value = cmbSoldProduct.SelectedValue.ToString() + "";
                    ((SPInvoice)objSPInvoice).dgvFreeProduct.Rows[intCurRow].Cells["FreeBrand"].Value = dgview.Rows[0].Cells[3].Value + "";
                    ((SPInvoice)objSPInvoice).dgvFreeProduct.Rows[intCurRow].Cells["FreeMainProduct"].Value = cmbSoldProduct.Text + "";
                    ((SPInvoice)objSPInvoice).dgvFreeProduct.Rows[intCurRow].Cells["OfferNumber"].Value = dgview.Rows[0].Cells[5].Value + "";
                    ((SPInvoice)objSPInvoice).dgvFreeProduct.Rows[intCurRow].Cells["FreeProduct"].Value = cmbProducts.Text + "";
                    ((SPInvoice)objSPInvoice).dgvFreeProduct.Rows[intCurRow].Cells["SoldQty"].Value = dgview.Rows[0].Cells[7].Value + "";
                    ((SPInvoice)objSPInvoice).dgvFreeProduct.Rows[intCurRow].Cells["FreeQty"].Value = txtQty.Text;
                    ((SPInvoice)objSPInvoice).dgvFreeProduct.Rows[intCurRow].Cells["FreeProductId"].Value = cmbProducts.SelectedValue.ToString();
                }
                ((SPInvoice)objSPInvoice).TotalFreeProducts();
            }
            else
            {
                if (objInvoice != null)
                {
                    if (dgvrs != null)
                    {
                        ((Invoice)objInvoice).dgvFreeProduct.Rows[iRowIndex].Cells["FreeProduct"].Value = cmbProducts.Text;
                        ((Invoice)objInvoice).dgvFreeProduct.Rows[iRowIndex].Cells["FreeQty"].Value = txtQty.Text;
                        ((Invoice)objInvoice).dgvFreeProduct.Rows[iRowIndex].Cells["FreeProductId"].Value = cmbProducts.SelectedValue.ToString();
                    }
                    else
                    {
                        ((Invoice)objInvoice).dgvFreeProduct.Rows.Add();
                        int intCurRow = ((Invoice)objInvoice).dgvFreeProduct.Rows.Count - 1;
                        ((Invoice)objInvoice).dgvFreeProduct.Rows[intCurRow].Cells["FreeSlNo"].Value = Convert.ToInt32(intCurRow + 1) + "";
                        ((Invoice)objInvoice).dgvFreeProduct.Rows[intCurRow].Cells["MainProductIdSlNo"].Value = dgview.Rows[0].Cells["SLNO"].Value + "";
                        ((Invoice)objInvoice).dgvFreeProduct.Rows[intCurRow].Cells["MainProductId"].Value = cmbSoldProduct.SelectedValue.ToString() + "";
                        ((Invoice)objInvoice).dgvFreeProduct.Rows[intCurRow].Cells["FreeBrand"].Value = dgview.Rows[0].Cells[3].Value + "";
                        ((Invoice)objInvoice).dgvFreeProduct.Rows[intCurRow].Cells["FreeMainProduct"].Value = cmbSoldProduct.Text + "";
                        ((Invoice)objInvoice).dgvFreeProduct.Rows[intCurRow].Cells["OfferNumber"].Value = dgview.Rows[0].Cells[5].Value + "";
                        ((Invoice)objInvoice).dgvFreeProduct.Rows[intCurRow].Cells["FreeProduct"].Value = cmbProducts.Text + "";
                        ((Invoice)objInvoice).dgvFreeProduct.Rows[intCurRow].Cells["SoldQty"].Value = dgview.Rows[0].Cells[7].Value + "";
                        ((Invoice)objInvoice).dgvFreeProduct.Rows[intCurRow].Cells["FreeQty"].Value = txtQty.Text;
                        ((Invoice)objInvoice).dgvFreeProduct.Rows[intCurRow].Cells["FreeProductId"].Value = cmbProducts.SelectedValue.ToString();
                    }
                    ((Invoice)objInvoice).TotalFreeProducts();

                }
                else
                {
                    if (dgvrs != null)
                    {
                        ((InvoiceBultin)objInvoiceBultin).dgvFreeProduct.Rows[iRowIndex].Cells["FreeProduct"].Value = cmbProducts.Text;
                        ((InvoiceBultin)objInvoiceBultin).dgvFreeProduct.Rows[iRowIndex].Cells["FreeQty"].Value = txtQty.Text;
                        ((InvoiceBultin)objInvoiceBultin).dgvFreeProduct.Rows[iRowIndex].Cells["FreeProductId"].Value = cmbProducts.SelectedValue.ToString();
                    }
                    else
                    {
                        ((InvoiceBultin)objInvoiceBultin).dgvFreeProduct.Rows.Add();
                        int intCurRow = ((InvoiceBultin)objInvoiceBultin).dgvFreeProduct.Rows.Count - 1;
                        ((InvoiceBultin)objInvoiceBultin).dgvFreeProduct.Rows[intCurRow].Cells["FreeSlNo"].Value = Convert.ToInt32(intCurRow + 1) + "";
                        ((InvoiceBultin)objInvoiceBultin).dgvFreeProduct.Rows[intCurRow].Cells["MainProductIdSlNo"].Value = dgview.Rows[0].Cells["SLNO"].Value + "";
                        ((InvoiceBultin)objInvoiceBultin).dgvFreeProduct.Rows[intCurRow].Cells["MainProductId"].Value = cmbSoldProduct.SelectedValue.ToString() + "";
                        ((InvoiceBultin)objInvoiceBultin).dgvFreeProduct.Rows[intCurRow].Cells["FreeBrand"].Value = dgview.Rows[0].Cells[4].Value + "";
                        ((InvoiceBultin)objInvoiceBultin).dgvFreeProduct.Rows[intCurRow].Cells["FreeMainProduct"].Value = cmbSoldProduct.Text + "";
                        ((InvoiceBultin)objInvoiceBultin).dgvFreeProduct.Rows[intCurRow].Cells["OfferNumber"].Value = dgview.Rows[0].Cells[5].Value + "";
                        ((InvoiceBultin)objInvoiceBultin).dgvFreeProduct.Rows[intCurRow].Cells["FreeProduct"].Value = cmbProducts.Text + "";
                        ((InvoiceBultin)objInvoiceBultin).dgvFreeProduct.Rows[intCurRow].Cells["SoldQty"].Value = dgview.Rows[0].Cells[7].Value + "";
                        ((InvoiceBultin)objInvoiceBultin).dgvFreeProduct.Rows[intCurRow].Cells["FreeQty"].Value = txtQty.Text;
                        ((InvoiceBultin)objInvoiceBultin).dgvFreeProduct.Rows[intCurRow].Cells["FreeProductId"].Value = cmbProducts.SelectedValue.ToString();
                    }
                    ((InvoiceBultin)objInvoiceBultin).TotalFreeProducts();
                }
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtQty.Text = "";
            cmbProducts.SelectedIndex = 0;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
