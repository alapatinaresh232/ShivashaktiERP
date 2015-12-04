using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSTrans;
namespace SSCRM
{
    public partial class PMProductDemoDetails : Form
    {
        public frmProductPromotion objfrmProductPromotion;
        SQLDB objSQLdb = null;
        DataRow[] drs;

        public PMProductDemoDetails()
        {
            InitializeComponent();
        }
        public PMProductDemoDetails(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;

        }

        private void PMProductDemoDetails_Load(object sender, EventArgs e)
        {
            gvProductDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                     System.Drawing.FontStyle.Regular);

            if (drs != null)
            {

                txtDemosCnt.Text = drs[0]["DemosCnt"].ToString();
                txtVisitorsCnt.Text = drs[0]["VisitorsCnt"].ToString();

                gvProductDetails.Rows.Add();

                gvProductDetails.Rows[0].Cells["SLNO"].Value = 1;
                gvProductDetails.Rows[0].Cells["ProductId"].Value = drs[0]["ProductID"].ToString();
                gvProductDetails.Rows[0].Cells["ProductName"].Value = drs[0]["prodName"].ToString();
                gvProductDetails.Rows[0].Cells["CategoryName"].Value = drs[0]["Category"].ToString();
                txtRemarks.Text = drs[0]["Remarks_Demo"].ToString();

            }

        }

        private bool CheckData()
        {
            bool flag = true;
            if (txtDemosCnt.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Product Demos Count", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDemosCnt.Focus();
            }
            else if (txtVisitorsCnt.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Visitors Count", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtVisitorsCnt.Focus();
            }
            else if (gvProductDetails.Rows.Count == 0)
            {
                flag = false;
                MessageBox.Show("Please Add  Product Details", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }
            return flag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (CheckData() == true)
            //{
            //    if (drs != null)
            //    {
            //        ((frmProductPromotion)objfrmProductPromotion).dtProdDemoDetails.Rows.Remove(drs[0]);
            //    }


            //    for (int i = 0; i < gvProductDetails.Rows.Count; i++)
            //    {

            //        ((frmProductPromotion)objfrmProductPromotion).dtProdDemoDetails.Rows.Add(new object[] { "-1" ,gvProductDetails.Rows[i].Cells["ProductId"].Value.ToString(),
            //                    gvProductDetails.Rows[i].Cells["ProductName"].Value.ToString(),gvProductDetails.Rows[i].Cells["CategoryName"].Value.ToString(),
            //                    txtVisitorsCnt.Text,txtDemosCnt.Text,txtRemarks.Text.ToString().Replace("'"," ").ToString()});
            //        ((frmProductPromotion)objfrmProductPromotion).GetProductDetails();
            //    }
            //    this.Close();
            //}

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtDemosCnt.Text = "";
            txtVisitorsCnt.Text = "";
            txtRemarks.Text = "";
            gvProductDetails.Rows.Clear();
        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            ProductSearchAll ProductsAdd = new ProductSearchAll("PMProductDemoDetails");
            ProductsAdd.objPMProductDemoDetails = this;
            ProductsAdd.ShowDialog();

        }

        private void txtStudentsCnt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtDemosCnt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void gvProductDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvProductDetails.Columns["Delete"].Index)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    DataGridViewRow dgvr = gvProductDetails.Rows[e.RowIndex];
                    gvProductDetails.Rows.Remove(dgvr);
                }
                if (gvProductDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                    {
                        gvProductDetails.Rows[i].Cells["SlNo"].Value = (i + 1).ToString();
                    }
                }
            }

        }
    }
}
