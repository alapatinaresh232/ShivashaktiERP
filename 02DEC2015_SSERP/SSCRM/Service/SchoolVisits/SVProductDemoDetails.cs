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
    public partial class SVProductDemoDetails : Form
    {
         public frmSchoolVisits objfrmSchoolVisits;
         DataRow[] drs;

        public SVProductDemoDetails()
        {
            InitializeComponent();
        }
        public SVProductDemoDetails(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }

       
        private void SVProductDemoDetails_Load(object sender, EventArgs e)
        {
            gvProductDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                      System.Drawing.FontStyle.Regular);

            if (drs != null)
            {
              
                txtDemosCnt.Text = drs[0]["DemosCnt"].ToString();
                txtStudentsCnt.Text = drs[0]["StudentsCnt"].ToString();

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
                MessageBox.Show("Please Enter Product Demos Count","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
                txtDemosCnt.Focus();
            }
            else if (txtStudentsCnt.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Students Count","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
                txtStudentsCnt.Focus();
            }
            else if (gvProductDetails.Rows.Count == 0)
            {
                flag = false;
                MessageBox.Show("Please Add Atleast One Product", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }
            return flag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {
                if (drs != null)
                {
                    ((frmSchoolVisits)objfrmSchoolVisits).dtProdDemoDetails.Rows.Remove(drs[0]);
                }


                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {

                    ((frmSchoolVisits)objfrmSchoolVisits).dtProdDemoDetails.Rows.Add(new object[] { "-1" ,gvProductDetails.Rows[i].Cells["ProductId"].Value.ToString(),
                                gvProductDetails.Rows[i].Cells["ProductName"].Value.ToString(),gvProductDetails.Rows[i].Cells["CategoryName"].Value.ToString(),
                                txtStudentsCnt.Text,txtDemosCnt.Text,txtRemarks.Text.ToString()});
                    ((frmSchoolVisits)objfrmSchoolVisits).GetProductDetails();
                }
                this.Close();
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            gvProductDetails.Rows.Clear();
            txtDemosCnt.Text = "";
            txtRemarks.Text = "";
            txtStudentsCnt.Text = "";
        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            ProductSearchAll ProductsAdd = new ProductSearchAll("SVProductDemoDetails");
            ProductsAdd.objSVProductDemoDetails = this;
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
