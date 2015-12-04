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
    public partial class frmProductDetails : Form
    {
        SQLDB objSQLdb = null;
        public frmDemoPlots objfrmDemoPlots = null;
        DataRow[] drs;


        public frmProductDetails()
        {
            InitializeComponent();
        }
        public frmProductDetails(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }

        private void frmProductDetails_Load(object sender, EventArgs e)
        {
            gvProductDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                       System.Drawing.FontStyle.Regular);

            FillCropDetails();

            if (drs != null)
            {              

                cbCrops.SelectedValue = drs[0]["CropId"].ToString();
                txtCropArea.Text = drs[0]["CropArea"].ToString();
                txtCropStage.Text = drs[0]["CropStage"].ToString();
                txtRemarks.Text = drs[0]["Remarks"].ToString();
                txtTreatedArea.Text = drs[0]["TreatedArea"].ToString();
                gvProductDetails.Rows.Add();
                gvProductDetails.Rows[0].Cells["SLNO"].Value = 1;
                gvProductDetails.Rows[0].Cells["ProductId"].Value = drs[0]["ProductID"].ToString();
                gvProductDetails.Rows[0].Cells["ProductName"].Value = drs[0]["prodName"].ToString();
                gvProductDetails.Rows[0].Cells["CategoryName"].Value = drs[0]["Category"].ToString();
                gvProductDetails.Rows[0].Cells["Qty"].Value = drs[0]["Qty"].ToString();

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void FillCropDetails()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbCrops.DataSource = null;
            try
            {
                string strCmd = "SELECT CROP_ID,CROP_NAME FROM CROP_MASTER ORDER BY CROP_NAME";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "0";
                    row[1] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cbCrops.DataSource = dt;
                    cbCrops.DisplayMember = "CROP_NAME";
                    cbCrops.ValueMember = "CROP_ID";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
                dt = null;
            }
        }
       
        private bool CheckData()
        {
            bool flag = true;
            if (txtCropArea.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Crop Area", "Demo Plot Product Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCropArea.Focus();
            }
            else if (cbCrops.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Crop Name", "Demo Plot Product Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbCrops.Focus();
            }

            else if (txtCropStage.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Crop Stage", "Demo Plot Product Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCropStage.Focus();
            }
            //else if (txtRemarks.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter Crop Remarks", "Demo Plot Product Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtRemarks.Focus();
            //}
            else if (gvProductDetails.Rows.Count == 0)
            {
                flag = false;
                MessageBox.Show("Please Add Product Details", "Demo Plot Product Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;

            }
            return flag;

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

        private void txtCropStage_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtCropArea.Text = "";
            txtCropStage.Text = "";
            txtRemarks.Text = "";
            cbCrops.SelectedIndex = 0;
            gvProductDetails.Rows.Clear();
        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            ProductSearchAll ProductsAdd = new ProductSearchAll("frmProductDetails");
            ProductsAdd.objfrmProductDetails = this;
            ProductsAdd.ShowDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {

                if (drs != null)
                {
                    ((frmDemoPlots)objfrmDemoPlots).dtProductDetails.Rows.Remove(drs[0]);
                }

                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {
                    if (Convert.ToString(gvProductDetails.Rows[i].Cells["Qty"].Value) == "")
                    {
                        gvProductDetails.Rows[i].Cells["Qty"].Value = "0";
                    }

                    ((frmDemoPlots)objfrmDemoPlots).dtProductDetails.Rows.Add(new Object[] { "-1",gvProductDetails.Rows[i].Cells["ProductId"].Value.ToString(),
                        Convert.ToInt32(cbCrops.SelectedValue),cbCrops.Text.ToString().Replace("'",""),txtCropArea.Text.ToString().Replace("'",""),txtCropStage.Text.ToString().Replace("\'","")
                        ,txtTreatedArea.Text.ToString().Replace("'",""),gvProductDetails.Rows[i].Cells["ProductName"].Value.ToString(),
                         gvProductDetails.Rows[i].Cells["Qty"].Value.ToString(),gvProductDetails.Rows[i].Cells["CategoryName"].Value.ToString(),txtRemarks.Text.ToString().Replace("\'","")});
                    ((frmDemoPlots)objfrmDemoPlots).GetProductDetails();
                }

                this.Close();
            }
        }

        void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }
       
        private void gvProductDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);

        }

      

     

       
    }
}
