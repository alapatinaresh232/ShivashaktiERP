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
    public partial class ProductLicence : Form
    {
        SQLDB objSQLdb = null;
        bool flagUpdate = false;
        int trnNo = 0;
        public ProductLicenceDetails objProductLicenceDetails = null;
        string ProdLicenceNo = "";
       

        public ProductLicence()
        {
            InitializeComponent();
        }
        public ProductLicence(string strLicenceNo)
        {
            ProdLicenceNo = strLicenceNo;
            flagUpdate = true;
            InitializeComponent();
            
        }


        private void ProductLicence_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            FillStates();
            dtpIssueDate.Value = DateTime.Today;
            dtpExpDate.Value = DateTime.Today;
            cmbWholeSale.SelectedIndex = 0;
            cmbRetail.SelectedIndex = 0;
            cbStatus.SelectedIndex = 0;
            if (flagUpdate == true)
            {
                FillProdLicenceDetails(ProdLicenceNo);
            }
            
           
        }
        
        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT CM_COMPANY_NAME ,CM_COMPANY_CODE FROM COMPANY_MAS WHERE ACTIVE='T' ORDER BY CM_COMPANY_NAME";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dt.Rows.InsertAt(dr, 0);

                    cbCompany.DataSource = dt;
                    cbCompany.DisplayMember = "CM_COMPANY_NAME";
                    cbCompany.ValueMember = "CM_COMPANY_CODE";
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

        private void FillStates()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT sm_state,sm_state_code FROM state_mas ORDER BY sm_state";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dt.Rows.InsertAt(dr, 0);

                    cbState.DataSource = dt;
                    cbState.DisplayMember = "sm_state";
                    cbState.ValueMember = "sm_state_code";
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
            if (cbCompany.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Company", "Product Licence ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCompany.Focus();
            }
            else if (cbState.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select State", "Product Licence ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbState.Focus();
            }
            else if (cbDistrict.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select District", "Product Licence ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbDistrict.Focus();
            }
            else if (txtName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Valid Ecode", "Product Licence ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEcodeHandleBy.Focus();
            }

            else if (txtLicenceNo.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter LicenceNo", "Product Licence ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLicenceNo.Focus();
            }
            else if (!(dtpExpDate.Value > dtpIssueDate.Value))
            {
                MessageBox.Show("Enter Valid Dates", "Product Licence ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                dtpExpDate.Focus();
                flag = false;
            }
            else if (Convert.ToInt32( txtValidMons.Text)==0)
            {
                MessageBox.Show("Enter Valid Dates", "Product Licence ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                dtpExpDate.Focus();
                flag = false;
            }
            else if (gvProductDetails.Rows.Count == 0)
            {
                MessageBox.Show("Please Add Product Details", "Product Licence ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                btnProductSearch.Focus();
                flag = false;
            }
            return flag;
        }
     
              

        private void dtpExpDate_ValueChanged(object sender, EventArgs e)
        {
            if ((dtpExpDate.Value < dtpIssueDate.Value))
            {
                dtpIssueDate.Value = dtpExpDate.Value;
            }
            else
            {
                int months = MonthDiff(dtpIssueDate.Value, dtpExpDate.Value);
                txtValidMons.Text = Convert.ToString(months);
            }
        }

        private void dtpIssueDate_ValueChanged(object sender, EventArgs e)
        {
            dtpExpDate.Value=dtpIssueDate.Value;
            //int months = MonthDiff(dtpIssueDate.Value,dtpExpDate.Value);
            txtValidMons.Text = Convert.ToString(0);
            
        }
        public static int MonthDiff(DateTime d2, DateTime d1)
        {
            //if (startDate > endDate)
            //{
            //    throw new Exception("Start Date is greater than the End Date");
            //}

            //int months = ((endDate.Year * 12) + endDate.Month) - ((startDate.Year * 12) + startDate.Month);

            //if (endDate.Day >= startDate.Day)
            //{
            //    months++;
            //}

            //return months;
            int retVal = 0;

            if (d1.Month < d2.Month)
            {
                retVal = (d1.Month + 12) - d2.Month;
                retVal += ((d1.Year - 1) - d2.Year) * 12;
            }
            else
            {
                retVal = d1.Month - d2.Month;
                retVal += (d1.Year - d2.Year) * 12;
            }
            return retVal;

        }
        private void txtEcodeHandleBy_TextChanged(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            if (txtEcodeHandleBy.Text != "")
            {
                try
                {
                    string strCommand = "SELECT MEMBER_NAME FROM EORA_MASTER WHERE ECODE =" + Convert.ToInt32(txtEcodeHandleBy.Text) + " ";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtName.Text = dt.Rows[0]["MEMBER_NAME"] + "";
                    }
                    else
                    {
                        txtName.Text = "";
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
        }

      

        private void cbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbDistrict.DataSource = null;
            try
            {
                if (cbState.SelectedIndex > 0)
                {
                    string strCmd = "SELECT DISTINCT(district) "+
                                    ",CDDistrict " +
                                    ",sm_state_code "+
                                    "FROM VILLAGEMASTERUKEY "+
                                    "INNER JOIN state_mas ON sm_state= state "+
                                    "WHERE sm_state_code='"+ cbState.SelectedValue.ToString() +
                                    "' ORDER BY district ";
                    dt=objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["district"] = "--Select--";
                    dr["district"] = "--Select--";
                    dt.Rows.InsertAt(dr, 0);

                    cbDistrict.DataSource = dt;
                    cbDistrict.DisplayMember = "district";
                    cbDistrict.ValueMember = "CDDistrict";
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

       

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                ProductSearchAll PSearch = new ProductSearchAll("ProductLicence", cbCompany.SelectedValue.ToString());
                PSearch.objProductLicence = this;
                PSearch.ShowDialog();
            }
            else
            {
                MessageBox.Show("Select Company", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            string strCmd = "";
            int iResult = 0;

            if (CheckData())
            {
                
                try
                {
                    if (flagUpdate == true)
                    {
                        strCmd = "UPDATE pr_licence_master SET plm_company_code='" + cbCompany.SelectedValue.ToString() +
                                       "', plm_state_code='" + cbState.SelectedValue.ToString() +
                                       "',plm_district= " + cbDistrict.SelectedValue.ToString() +
                                       ",plm_licence_issu_date='" + Convert.ToDateTime(dtpIssueDate.Value).ToString("dd/MMM/yyyy") +
                                       "',plm_licence_valid_months='" + txtValidMons.Text +
                                       "',plm_licence_valid_date='" + Convert.ToDateTime(dtpExpDate.Value).ToString("dd/MMM/yyyy") +
                                       "',plm_handled_by_ecode=" + txtEcodeHandleBy.Text +
                                       ",plm_wholesale_flag='" + cmbWholeSale.Tag.ToString() +
                                       "',plm_retail_flag='" + cmbRetail.Tag.ToString() +
                                       "',plm_outlet_addr='" + txtAddress.Text +
                                       "',plm_modified_by='" + CommonData.LogUserId +
                                       "',plm_modified_date='" + CommonData.CurrentDate +
                                       "',plm_status='" + cbStatus.Text.ToString() +
                                       "' WHERE plm_trn_no=" + trnNo;
                    }
                         
                    else
                    {
                        string strCmnd = "SELECT ISNULL(MAX(plm_trn_no), 0)+1 AS TrnNo FROM pr_licence_master ";
                        trnNo = Convert.ToInt32(objSQLdb.ExecuteDataSet(strCmnd).Tables[0].Rows[0][0].ToString());

                        strCmd = "";
                        strCmd = "INSERT INTO pr_licence_master(plm_company_code " +
                                 ", plm_state_code " +
                                 ", plm_district " +
                                 ", plm_licence_number " +
                                 ", plm_licence_issu_date " +
                                 ", plm_licence_valid_months " +
                                 ", plm_licence_valid_date " +
                                 ", plm_handled_by_ecode " +
                                 ", plm_wholesale_flag " +
                                 ", plm_retail_flag " +
                                 ", plm_outlet_addr " +
                                 ", plm_created_by " +
                                 ", plm_created_date " +
                                 ", plm_trn_no "+
                                 ", plm_status) VALUES('" + cbCompany.SelectedValue.ToString() +
                                 "','" + cbState.SelectedValue.ToString() +
                                 "'," + Convert.ToInt32(cbDistrict.SelectedValue.ToString()) +
                                 ",'" + txtLicenceNo.Text +
                                 "','" + Convert.ToDateTime(dtpIssueDate.Value).ToString("dd/MMM/yyyy") +
                                 "'," + txtValidMons.Text +
                                 ",'" + Convert.ToDateTime(dtpExpDate.Value).ToString("dd/MMM/yyyy") +
                                 "', " + Convert.ToInt32(txtEcodeHandleBy.Text) +
                                 ",'" + cmbWholeSale.Tag.ToString() +
                                 "','" + cmbRetail.Tag.ToString() +
                                 "','" + txtAddress.Text +
                                 "','" + CommonData.LogUserId +
                                 "','" + CommonData.CurrentDate +
                                 "'," + trnNo +
                                 ",'"+ cbStatus.Text.ToString() +"')";
                    }

                    iResult = objSQLdb.ExecuteSaveData(strCmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (iResult > 0)
                {
                    if (SavePrLicenceDetail() > 0)
                    {
                        MessageBox.Show("Data Saved Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flagUpdate = false;
                        btnCancel_Click(null, null);
                    }
                    else
                    {
                        string strDelete = "DELETE FROM pr_licence_detail WHERE pld_trn_no=" + trnNo +
                                       " DELETE FROM pr_licence_master WHERE plm_trn_no= " + trnNo + "";
                        iResult = objSQLdb.ExecuteSaveData(strDelete);
                        MessageBox.Show("Data Not Saved ", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Data Not Saved ", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private int SavePrLicenceDetail()
        {
            objSQLdb = new SQLDB();
            string strCommand = "";
            int iRes = 0;
            try
            {
                
                strCommand = " DELETE FROM pr_licence_detail WHERE pld_trn_no=" + trnNo;
                iRes = objSQLdb.ExecuteSaveData(strCommand);
                strCommand = "";


                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {
                   
                    strCommand += " INSERT INTO pr_licence_detail(pld_company_code " +
                                        ",pld_state_code " +
                                        ",pld_district " +
                                        ",pld_licence_number " +
                                        ",pld_category_id " +
                                        ",pld_brand_id " +
                                        ",pld_product_id "+
                                        ",pld_trn_no "+
                                        ",pld_sl_no) "+
                                        "VALUES('" + cbCompany.SelectedValue.ToString() +
                                        "','" + cbState.SelectedValue.ToString() +
                                        "'," + Convert.ToInt32(cbDistrict.SelectedValue.ToString()) +
                                        ",'" + txtLicenceNo.Text +
                                        "','" + gvProductDetails.Rows[i].Cells["CategoryId"].Value.ToString() +
                                        "','" + gvProductDetails.Rows[i].Cells["BrandId"].Value.ToString() +
                                        "','" + gvProductDetails.Rows[i].Cells["ProductId"].Value.ToString() + 
                                        "'," + trnNo +
                                        "," +Convert.ToInt32(gvProductDetails.Rows[i].Cells["SlNo"].Value.ToString()) + ") ";

                }
                iRes = objSQLdb.ExecuteSaveData(strCommand);
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;

            }

            return iRes;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            flagUpdate = false;
            cbCompany.SelectedIndex = 0;
            cbState.SelectedIndex = 0;
            txtLicenceNo.Text = string.Empty;
            txtEcodeHandleBy.Text = string.Empty;
            txtName.Text = string.Empty;
         
            cmbWholeSale.SelectedIndex = 0;
            cmbRetail.SelectedIndex = 0;
            dtpExpDate.Value = DateTime.Today;
            dtpIssueDate.Value = DateTime.Today;
            gvProductDetails.Rows.Clear();
                       

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (flagUpdate == true)
            {
                objSQLdb = new SQLDB();
                int iRes = 0;
                try
                {
                   

                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        string strDelete = "DELETE FROM pr_licence_detail WHERE pld_trn_no=" + trnNo +
                                       ";DELETE FROM pr_licence_master WHERE plm_trn_no= " + trnNo + "";
                        iRes = objSQLdb.ExecuteSaveData(strDelete);
                    }
                    if (iRes > 0)
                    {
                        MessageBox.Show("Data Deleted Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flagUpdate = false;
                        btnCancel_Click(null, null);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSQLdb = null;

                }
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();

        }

        private void btnClearProd_Click(object sender, EventArgs e)
        {
            gvProductDetails.Rows.Clear();

        }


        private void FillProdLicenceDetails(string ProdLicenceNo)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT PLD_COMPANY_CODE "+
                                ", PLD_STATE_CODE "+
                                ", PLD_DISTRICT "+
                                ", PLD_LICENCE_NUMBER "+
                                ", PLD_CATEGORY_ID "+
                                ", PLD_BRAND_ID "+
                                ", PLD_PRODUCT_ID "+
                                ", PLD_SL_NO "+
                                ", CM.CATEGORY_NAME CategoryName "+
                                ", PM.PM_PRODUCT_NAME ProductName "+
                                ", plm_wholesale_flag "+
                                ", plm_handled_by_ecode "+
                                ", plm_retail_flag "+
                                ", plm_outlet_addr " +
                                ", plm_licence_issu_date "+
                                ", plm_licence_valid_date "+
                                ", plm_licence_valid_months "+
                                ", plm_trn_no "+
                                ",plm_status Status FROM pr_licence_detail PLD " +
                                " INNER JOIN CATEGORY_MASTER CM ON PLD.PLD_CATEGORY_ID=CM.CATEGORY_ID "+
                                " INNER JOIN PRODUCT_MAS PM ON PM.PM_PRODUCT_ID=PLD_PRODUCT_ID "+
                                " INNER JOIN pr_licence_master ON plm_licence_number=pld_licence_number "+
                                "  WHERE pld_licence_number='" + ProdLicenceNo + " ' ";
                dt=objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    flagUpdate = true;
                    txtLicenceNo.ReadOnly = true;
                    cbCompany.SelectedValue = dt.Rows[0]["PLD_COMPANY_CODE"].ToString();
                    cbState.SelectedValue = dt.Rows[0]["PLD_STATE_CODE"].ToString();
                    cbDistrict.SelectedValue = dt.Rows[0]["PLD_DISTRICT"].ToString();
                    txtLicenceNo.Text = ProdLicenceNo;
                    dtpIssueDate.Value = Convert.ToDateTime(dt.Rows[0]["plm_licence_issu_date"].ToString());
                    txtValidMons.Text = Convert.ToInt32(dt.Rows[0]["plm_licence_valid_months"]).ToString();
                    dtpExpDate.Value = Convert.ToDateTime(dt.Rows[0]["plm_licence_valid_date"].ToString());
                    txtAddress.Text = dt.Rows[0]["plm_outlet_addr"].ToString(); ;
                    if (dt.Rows[0]["plm_wholesale_flag"].ToString() == "N")
                    {
                        cmbWholeSale.SelectedIndex = 1;
                    }
                    if (dt.Rows[0]["plm_retail_flag"].ToString() == "N")
                    {
                        cmbRetail.SelectedIndex = 1;
                    }
                    txtEcodeHandleBy.Text = Convert.ToInt32(dt.Rows[0]["plm_handled_by_ecode"]).ToString();
                    trnNo = Convert.ToInt32(dt.Rows[0]["plm_trn_no"] + "");

                    if (dt.Rows[0]["Status"].ToString() != null)
                        cbStatus.Text = dt.Rows[0]["Status"].ToString();
                    else
                        cbStatus.SelectedIndex = 0;

                    gvProductDetails.Rows.Clear();
                    for (int iVar = 0; iVar < dt.Rows.Count; iVar++)
                    {
                        gvProductDetails.Rows.Add();

                        gvProductDetails.Rows[iVar].Cells["SlNo"].Value = (iVar + 1).ToString();
                        gvProductDetails.Rows[iVar].Cells["CategoryId"].Value = dt.Rows[iVar]["PLD_CATEGORY_ID"].ToString();
                        gvProductDetails.Rows[iVar].Cells["CategoryName"].Value = dt.Rows[iVar]["CategoryName"].ToString();
                        gvProductDetails.Rows[iVar].Cells["ProductId"].Value = dt.Rows[iVar]["PLD_PRODUCT_ID"].ToString();
                        gvProductDetails.Rows[iVar].Cells["ProductName"].Value = dt.Rows[iVar]["ProductName"].ToString();
                        gvProductDetails.Rows[iVar].Cells["BrandId"].Value = dt.Rows[iVar]["PLD_BRAND_ID"].ToString();
                    }
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

       
        private void txtLicenceNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
            if (e.KeyChar != '\b')
            {
             
                e.Handled = (e.KeyChar == (char)Keys.Space);
            }
        }

        private void cmbWholeSale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbWholeSale.SelectedIndex == 0)
            {
                cmbWholeSale.Tag = "Y";
            }
            else if (cmbWholeSale.SelectedIndex == 1)
            {
                cmbWholeSale.Tag = "N";
            }
        }

        private void cmbRetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRetail.SelectedIndex == 0)
            {
                cmbRetail.Tag = "Y";
            }
            else if (cmbRetail.SelectedIndex == 1)
            {
                cmbRetail.Tag = "N";
            }
        }

        private void gvProductDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvProductDetails.Columns["delete"].Index)
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
