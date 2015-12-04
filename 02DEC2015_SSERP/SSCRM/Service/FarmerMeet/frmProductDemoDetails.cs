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
    public partial class frmDemoDetails : Form
    {
        SQLDB objSQLdb = null;
        public FarmerMeetingForm objFarmerMeetingForm;

        string strfrmType = "";
        DataGridViewRow  gridRow;
        bool bFlag = false;


        public frmDemoDetails(string sType)
        {
            InitializeComponent();
            strfrmType = sType;
           
        }
        public frmDemoDetails(string sType,DataGridViewRow dgvrow)
        {
            bFlag = true;
            InitializeComponent();
            strfrmType = sType;
           
            gridRow = dgvrow;
        }
        private void frmDemoDetails_Load(object sender, EventArgs e)
        {
            gvProductDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                       System.Drawing.FontStyle.Regular);
            
        }

      

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            ProductSearchAll ProductsAdd = new ProductSearchAll("frmDemoDetails");
            ProductsAdd.objfrmDemoDetails = this;
            ProductsAdd.ShowDialog();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        //private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cbCompany.SelectedIndex > 0)
        //    {
        //        FillBranchData();
        //    }
        //}

        private bool CheckData()
        {
            bool flag = true;
            if (txtFarmersCnt.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter No.Of Farmers Attended","Product Demo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                txtFarmersCnt.Focus();
            }
           
            //else if (txtRemarks.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter Product Remarks", "Product Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtRemarks.Focus();
            //}
            else if (gvProductDetails.Rows.Count == 0)
            {
                flag = false;
                MessageBox.Show("Please Add  Product Details", "Product Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }
            return flag;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
            DataGridView dgvDemoDetails = null;
            if (bFlag == true)
            {
                if (strfrmType == "FarmerMeetingForm")
                {

                    ((FarmerMeetingForm)objFarmerMeetingForm).gvDemoDetails.Rows.Remove(gridRow);


                    dgvDemoDetails = ((FarmerMeetingForm)objFarmerMeetingForm).gvDemoDetails;
                    AddProductDemodetailsToGrid(dgvDemoDetails);
                    //MessageBox.Show("Product Demo Details Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCancel_Click(null, null);
                    this.Dispose();


                }
                bFlag = false;
            }
            else
            {
                if (CheckData() == true)
                {

                    if (strfrmType == "FarmerMeetingForm")
                    {
                        dgvDemoDetails = ((FarmerMeetingForm)objFarmerMeetingForm).gvDemoDetails;
                        AddProductDemodetailsToGrid(dgvDemoDetails);
                        //MessageBox.Show("Product Demo Details Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(null, null);
                        this.Dispose();


                    }
                    else
                    {
                        MessageBox.Show("Product Demo Details Not saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

        }

        private void AddProductDemodetailsToGrid(DataGridView dgvDemoDetails)
        {
            int intRow = 0;
            intRow = dgvDemoDetails.Rows.Count + 1;
           
               

                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {

                    DataGridViewRow tempRow = new DataGridViewRow();
                    DataGridViewCell cellSlNo = new DataGridViewTextBoxCell();
                    cellSlNo.Value = intRow;
                    tempRow.Cells.Add(cellSlNo);


                    DataGridViewCell cellProductId = new DataGridViewTextBoxCell();
                    cellProductId.Value = gvProductDetails.Rows[i].Cells["ProductId"].Value.ToString();
                    tempRow.Cells.Add(cellProductId);

                    DataGridViewCell cellProductName = new DataGridViewTextBoxCell();
                    cellProductName.Value = gvProductDetails.Rows[i].Cells["ProductName"].Value.ToString();
                    tempRow.Cells.Add(cellProductName);

                    DataGridViewCell cellCategoryName = new DataGridViewTextBoxCell();
                    cellCategoryName.Value = gvProductDetails.Rows[i].Cells["CategoryName"].Value.ToString();
                    tempRow.Cells.Add(cellCategoryName);
              
                    DataGridViewCell cellFarmers = new DataGridViewTextBoxCell();
                    cellFarmers.Value = Convert.ToInt32(txtFarmersCnt.Text);
                    tempRow.Cells.Add(cellFarmers);

                    DataGridViewCell cellDemos = new DataGridViewTextBoxCell();
                    cellDemos.Value = Convert.ToInt32(gvProductDetails.Rows.Count);
                    tempRow.Cells.Add(cellDemos);

                    DataGridViewCell cellRemarks = new DataGridViewTextBoxCell();
                    cellRemarks.Value = txtRemarks.Text.ToString().Replace("'"," ");
                    tempRow.Cells.Add(cellRemarks);

                    intRow = intRow + 1;
                    dgvDemoDetails.Rows.Add(tempRow);                   
              
          
                }
            
           
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
           
            txtFarmersCnt.Text = "";
            txtRemarks.Text = "";
            gvProductDetails.Rows.Clear();

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

        private void txtRemarks_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.KeyChar = Char.ToUpper(e.KeyChar);
            //if (e.KeyChar != '\b')
            //{
            //    if (!char.IsLetter(e.KeyChar))
            //    {
            //        e.Handled = true;
            //    }
            //}
        }

        private void txtFarmersCnt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
          

    }
}
