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
    public partial class PMProductItemDetails : Form
    {
        public frmProductPromotion objfrmProductPromotion;
        SQLDB objSQLdb = null;
        DataRow[] drs;

        public PMProductItemDetails()
        {
            InitializeComponent();
        }
        public PMProductItemDetails(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }

      
        private void PMProductItemDetails_Load(object sender, EventArgs e)
        {
            FillItemDetails();

            if (drs != null)
            {
                cbItems.SelectedValue = drs[0]["ItemId"].ToString();
                txtQty.Text = drs[0]["Qty"].ToString();
                txtRemarks.Text = drs[0]["ItemRemarks"].ToString();
            }

        }

        private void FillItemDetails()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT IM_ITEM_ID,IM_ITEM_NAME  FROM ITEM_MAS "+
                                    " WHERE IM_CATEGORY_ID='0004' "+
                                    " ORDER BY IM_ITEM_NAME ASC";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();

                    row[0] = "--Select--";
                    row[1] = "--Select--";
                    dt.Rows.InsertAt(row, 0);

                    cbItems.DataSource = dt;
                    cbItems.DisplayMember = "IM_ITEM_NAME";
                    cbItems.ValueMember = "IM_ITEM_ID";

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
            if (cbItems.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Promotion Type","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Error);
                cbItems.Focus();
                return flag;
            }
            else if (txtQty.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Item Quantity", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtQty.Focus();
                return flag;
            }
            if (((frmProductPromotion)objfrmProductPromotion).gvItemDetails.Rows.Count > 0)
            {
                for (int i = 0; i < ((frmProductPromotion)objfrmProductPromotion).gvItemDetails.Rows.Count; i++)
                {
                    if (cbItems.SelectedValue.ToString().Equals(((frmProductPromotion)objfrmProductPromotion).gvItemDetails.Rows[i].Cells["ItemId"].Value.ToString()))
                    {
                        flag = false;
                        MessageBox.Show("Already Exists", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return flag;
                       
                    }
                }
            }
            //else if (txtRemarks.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter Remarks", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtRemarks.Focus();
            //}

            return flag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (drs != null)
            {
                ((frmProductPromotion)objfrmProductPromotion).dtItemDetails.Rows.Remove(drs[0]);
            }
            ((frmProductPromotion)objfrmProductPromotion).GetItemDetails();

            if (CheckData() == true)
            {

                ((frmProductPromotion)objfrmProductPromotion).dtItemDetails.Rows.Add(new object[] { "-1",cbItems.SelectedValue.ToString(),cbItems.Text.ToString(),
                                            txtQty.Text,txtRemarks.Text.ToString().Replace("'"," ")});

                ((frmProductPromotion)objfrmProductPromotion).GetItemDetails();

                this.Close();
            }

        }
              

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbItems.SelectedIndex = 0;
            txtQty.Text = "";
            txtRemarks.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
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
