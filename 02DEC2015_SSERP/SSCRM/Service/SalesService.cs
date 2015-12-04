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
using SSCRM.App_Code;
namespace SSCRM
{
    public partial class SalesService : Form
    {
        SQLDB objSQLdb = null;
        HRInfo objHRInfo = null;
        InvoiceDB objInvoiceDB = null;
        DataSet ds;
        string strChkDoc = "", strChkVill = "", strChkProd = "", strQty = "";
        public SalesService()
        {
            InitializeComponent();
        }

        private void SalesService_Load(object sender, EventArgs e)
        {
            GetPopupContrpls();
            cmbQty.SelectedIndex = 0;
            cmbQty.SelectedIndex = 0;
            //dtpActualDt.Value = System.DateTime.Now;
        }

        public void GetPopupContrpls()
        {
            objHRInfo = new HRInfo();
            DataTable dtBranch = objHRInfo.GetAllBranchList(CommonData.CompanyCode, "", "").Tables[0];
            UtilityLibrary.PopulateControl(cmbBranch, dtBranch.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
            objHRInfo = null;
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            Clear();
            treeInvView.Nodes.Clear();
            objSQLdb = null;
            objInvoiceDB = new InvoiceDB();
            ds = objInvoiceDB.GetInvocieInfoforService(cmbBranch.SelectedValue.ToString());
            objInvoiceDB = null;

            GetALLData();
        }

        private void treeInvView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = treeInvView.SelectedNode;
            Clear();
            DataView dvFil = ds.Tables[0].DefaultView;
            dvFil.RowFilter = "TNA_INVOICE_NUMBER=" + node.Text;
            DataTable dt;
            dt = dvFil.ToTable();
            if (dt.Rows.Count > 0)
            {
                txtVillage.Text = dt.Rows[0]["CM_VILLAGE"].ToString();
                txtMandal.Text = dt.Rows[0]["CM_MANDAL"].ToString();
                txtDistrict.Text = dt.Rows[0]["CM_DISTRICT"].ToString();
                txtState.Text = dt.Rows[0]["CM_STATE"].ToString();
                txtPin.Text = dt.Rows[0]["CM_PIN"].ToString();
                txtHouseNo.Text = dt.Rows[0]["CM_HOUSE_NO"].ToString();
                txtLandMark.Text = dt.Rows[0]["CM_LANDMARK"].ToString();
                txtMobileNo.Text = dt.Rows[0]["CM_MOBILE_NUMBER"].ToString();
                txtLanLineNo.Text = dt.Rows[0]["CM_LAND_LINE_NO"].ToString();
                txtCustomerName.Text = dt.Rows[0]["CM_FARMER_NAME"].ToString();
                txtRelationName.Text = dt.Rows[0]["CM_FORG_NAME"].ToString();
                cbRelation.Text = dt.Rows[0]["CM_SO_FO"].ToString();
                FillServiceDetail(dt);

                dgvActivity.Columns.Clear();
                dgvActivity.Rows.Clear();
                dgvActivity.ColumnCount = 1;
                dgvActivity.Columns[0].Name = "Activity";
                dgvActivity.Columns[0].Width = 150;
                foreach (DataRow dr in dt.Rows)
                {
                    dgvActivity.Rows.Add(dr["sam_activity_name"].ToString());
                }
                DataGridViewComboBoxColumn cmb = new DataGridViewComboBoxColumn();
                cmb.HeaderText = "Select Data";
                cmb.Name = "Ecode";
                cmb.MaxDropDownItems = 4;
                cmb.Width = 300;
                foreach (DataRow drC in ds.Tables[3].Rows)
                {
                    cmb.Items.Add(drC[1].ToString());
                }
                dgvActivity.Columns.Add(cmb);

                DataGridViewTextBoxColumn ActivityDt = new DataGridViewTextBoxColumn();
                ActivityDt.Name = "ActivityDt";
                ActivityDt.Width = 100;
                dgvActivity.Columns.Add(ActivityDt);

                DataGridViewTextBoxColumn ActRemarks = new DataGridViewTextBoxColumn();
                ActRemarks.Name = "Remarks";
                ActRemarks.Width = 200;
                dgvActivity.Columns.Add(ActRemarks);
            }
        }

        private void FillServiceDetail(DataTable dt)
        {
            try
            {
                int intRow = 1;
                gvProductDetails.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["TNA_PRODUCT_ID"].ToString().Length > 0)
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = intRow;
                        tempRow.Cells.Add(cellSLNO);

                        DataGridViewCell cellMainProductID = new DataGridViewTextBoxCell();
                        cellMainProductID.Value = dt.Rows[i]["TNA_PRODUCT_ID"];
                        tempRow.Cells.Add(cellMainProductID);

                        DataGridViewCell cellMainProduct = new DataGridViewTextBoxCell();
                        cellMainProduct.Value = dt.Rows[i]["pm_product_name"];// +"(Rs = " + Convert.ToDecimal(dt.Rows[i]["SID_PRICE"]).ToString("0") + ")";
                        tempRow.Cells.Add(cellMainProduct);

                        DataGridViewCell cellDessc = new DataGridViewTextBoxCell();
                        cellDessc.Value = dt.Rows[i]["category_name"];
                        tempRow.Cells.Add(cellDessc);

                        DataGridViewCell cellPoints = new DataGridViewTextBoxCell();
                        cellPoints.Value = Convert.ToDouble(dt.Rows[i]["TNA_QTY"]).ToString("f");
                        tempRow.Cells.Add(cellPoints);

                        intRow = intRow + 1;
                        gvProductDetails.Rows.Add(tempRow);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
        }

        public void Clear()
        {
            txtVillage.Text = "";
            txtMandal.Text = "";
            txtDistrict.Text = "";
            txtState.Text = "";
            txtPin.Text = "";
            txtHouseNo.Text = "";
            txtLandMark.Text = "";
            txtMobileNo.Text = "";
            txtLanLineNo.Text = "";
            txtCustomerName.Text = "";
            txtRelationName.Text = "";
            cbRelation.SelectedIndex = 0;
        }

        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBranch.SelectedIndex > 0)
            {
                chkDocMonth.Items.Clear();
                chkVillage.Items.Clear();
                chkProducts.Items.Clear();
                objInvoiceDB = new InvoiceDB();
                ds = objInvoiceDB.GetInvocieInfoforService(cmbBranch.SelectedValue.ToString());
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (!chkDocMonth.Items.Contains(dr["TNA_DOC_MONTH"].ToString()))
                        chkDocMonth.Items.Add(dr["TNA_DOC_MONTH"].ToString());
                }
                foreach (DataRow drV in ds.Tables[1].Rows)
                {
                    if (!chkVillage.Items.Contains(drV["CM_VILLAGE"].ToString()))
                        chkVillage.Items.Add(drV["CM_VILLAGE"].ToString());
                }
                foreach (DataRow drP in ds.Tables[2].Rows)
                {
                    if (!chkProducts.Items.Contains(drP["pm_product_name"].ToString()))
                        chkProducts.Items.Add(drP["pm_product_name"].ToString());
                }
                objSQLdb = null;
            }
        }

        public void GetALLData()
        {
            string strMain = "";
            if (strChkDoc != "")
                strMain += " AND TNA_DOC_MONTH in (" + strChkDoc.TrimEnd(',') + ") ";

            if (strChkVill != "")
                strMain += " AND CM_VILLAGE in (" + strChkVill.TrimEnd(',') + ")";

            if (strChkProd != "")
                strMain += " AND pm_product_name in (" + strChkProd.TrimEnd(',') + ")";

            if (cmbQty.SelectedIndex == 4)
                txtTo_Validated(null, null);
            else
                txtFrm_Validated(null, null);

            if (strQty != "")
                strMain += strQty;

            objSQLdb = new SQLDB();
            string strSq = "SELECT DISTINCT TNA_INVOICE_NUMBER FROM SERVICES_TNA, SALES_INV_HEAD, CUSTOMER_MAS, SERVICES_ACTIVITIES_MAS, PRODUCT_MAS, CATEGORY_MASTER " +
                " WHERE TNA_company_code = siH_company_code and TNA_invoice_number = siH_invoice_number and TNA_product_id = pm_product_id and " +
                "SIH_FARMER_ID = CM_FARMER_ID AND TNA_TARGET_DATE <= CONVERT(NVARCHAR(10),GETDATE(),102) AND TNA_BRANCH_CODE='" + cmbBranch.SelectedValue + "'";
            DataView dvMain = objSQLdb.ExecuteDataSet(strSq + strMain).Tables[0].DefaultView;
            treeInvView.Nodes.Clear();
            lblCnt.Text = dvMain.Table.Rows.Count.ToString();
            for (int i = 0; i < dvMain.Table.Rows.Count; i++)
            {
                TreeNode treeNode = new TreeNode(dvMain.Table.Rows[i][0].ToString());
                treeInvView.Nodes.Add(treeNode);
            }
            objSQLdb = null;
        }

        private void chkDocMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            strChkDoc = "";
            for (int i = 0; i < chkDocMonth.CheckedItems.Count; i++)
            {
                strChkDoc += "'" + chkDocMonth.CheckedItems[i].ToString() + "',";
            }
        }

        private void chkVillage_SelectedIndexChanged(object sender, EventArgs e)
        {
            strChkVill = "";
            for (int i = 0; i < chkVillage.CheckedItems.Count; i++)
            {
                strChkVill += "'" + chkVillage.CheckedItems[i].ToString() + "',";
            }
        }

        private void chkProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            strChkProd = "";
            for (int i = 0; i < chkProducts.CheckedItems.Count; i++)
            {
                strChkProd += "'" + chkProducts.CheckedItems[i].ToString() + "',";
            }
        }

        private void txtFrm_Validated(object sender, EventArgs e)
        {
            strQty = "";
            if (cmbQty.SelectedIndex == 0)
                strQty = "";
            else if (cmbQty.SelectedIndex == 1)
                strQty += " AND TNA_QTY <= " + txtFrm.Text;
            else if (cmbQty.SelectedIndex == 2)
                strQty += " AND TNA_QTY >= " + txtFrm.Text;
            else if (cmbQty.SelectedIndex == 3)
                strQty += " AND TNA_QTY = " + txtFrm.Text;
        }

        private void txtTo_Validated(object sender, EventArgs e)
        {
            strQty = "";
            if (cmbQty.SelectedIndex == 4)
                strQty += " AND TNA_QTY BETWEEN " + txtFrm.Text + " AND " + txtTo.Text;
        }

        private void cmbQty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbQty.SelectedIndex != 4)
            {
                lblTo.Visible = false;
                txtTo.Visible = false;
                lblFrm.Text = "No.";
            }
            else
            {
                lblTo.Visible = true;
                txtTo.Visible = true;
                lblFrm.Text = "From";
            }
        }

        private void txtFrm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void chkDocM_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDocM.Checked == true)
            {
                for (int i = 0; i < chkDocMonth.Items.Count; i++)
                {
                    chkDocMonth.SetItemCheckState(i, CheckState.Checked);
                }
                strChkDoc = "";
                for (int i = 0; i < chkDocMonth.CheckedItems.Count; i++)
                {
                    strChkDoc += "'" + chkDocMonth.CheckedItems[i].ToString() + "',";
                }
            }
            else
            {
                for (int i = 0; i < chkDocMonth.Items.Count; i++)
                {
                    chkDocMonth.SetItemCheckState(i, CheckState.Unchecked);
                }
                strChkDoc = "";
            }
        }

        private void chkVill_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVill.Checked == true)
            {
                for (int i = 0; i < chkVillage.Items.Count; i++)
                {
                    chkVillage.SetItemCheckState(i, CheckState.Checked);
                }
                strChkVill = "";
                for (int i = 0; i < chkVillage.CheckedItems.Count; i++)
                {
                    strChkVill += "'" + chkVillage.CheckedItems[i].ToString() + "',";
                }
            }
            else
            {
                for (int i = 0; i < chkVillage.Items.Count; i++)
                {
                    chkVillage.SetItemCheckState(i, CheckState.Unchecked);
                }
                strChkVill = "";
            }
        }

        private void chkProduct_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProduct.Checked == true)
            {
                for (int i = 0; i < chkProducts.Items.Count; i++)
                {
                    chkProducts.SetItemCheckState(i, CheckState.Checked);
                }
                strChkProd = "";
                for (int i = 0; i < chkProducts.CheckedItems.Count; i++)
                {
                    strChkProd += "'" + chkProducts.CheckedItems[i].ToString() + "',";
                }
            }
            else
            {
                for (int i = 0; i < chkProducts.Items.Count; i++)
                {
                    chkProducts.SetItemCheckState(i, CheckState.Unchecked);
                }
                strChkProd = "";
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            TreeNode currentnode = new TreeNode();
            treeInvView.BackColor = System.Drawing.Color.White;
            foreach (TreeNode node1 in treeInvView.Nodes)
            {
                node1.BackColor = System.Drawing.Color.White;
                if (node1.Text.Contains(txtSearch.Text))
                //if (node1.Text == this.txtSearch.Text)
                {
                    currentnode = node1;
                    treeInvView.SelectedNode = currentnode;
                    treeInvView.SelectedNode.BackColor = System.Drawing.Color.Blue;
                    break;
                }
            }
        }

        private void txtECode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvActivity.Rows.Count; i++)
                {
                    if (Convert.ToString(dgvActivity.Rows[i].Cells["Ecode"].Value) != "")
                    {
                        //ActivityDt,Remarks                        
                    }
                    if (Convert.ToString(dgvActivity.Rows[i].Cells["ActivityDt"].Value) != "")
                    {
                        //ActivityDt,Remarks                        
                    }
                    if (Convert.ToString(dgvActivity.Rows[i].Cells["Remarks"].Value) != "")
                    {
                        //ActivityDt,Remarks                        
                    }
                }

                //string sqlUpdate = "UPDATE SERVICES_TNA SET TNA_ACTUAL_DATE='" + dtpActualDt.Value.ToString("dd/MMM/yyyy") + "',TNA_ATTEND_BY_ECODE=" + cmbEcodes.SelectedValue + ",TNA_FARMER_REMARKS='" + txtRemarks.Text +
                //    "' WHERE TNA_BRANCH_CODE='" + cmbBranch.SelectedValue + "' AND TNA_INVOICE_NUMBER=" + treeInvView.SelectedNode.Text;
                //objSQLdb = new SQLDB();
                //int intRec = objSQLdb.ExecuteSaveData(sqlUpdate);
                objSQLdb = null;
                MessageBox.Show("Data saved successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvActivity_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvActivity.Columns["ActivityDt"].Index)
            {                
                DateTimePicker dtp = new DateTimePicker();
                dtp.Value = DateTime.Now;
                dtp.Format = DateTimePickerFormat.Custom;
                dtp.Location = dgvActivity.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                dtp.Size = dgvActivity.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Size;
                dgvActivity.Controls.Add(dtp);
            }
        }
    }
}
