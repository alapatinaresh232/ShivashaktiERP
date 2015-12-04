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
using SSAdmin;
namespace SSCRM
{
    public partial class InternalDamage : Form
    {
        SQLDB objSQLDB = new SQLDB();
        private UtilityDB objUtilData;
        private bool TranTypeLoad = false;
        public InternalDamage()
        {
            InitializeComponent();
        }
        private void InternalDamage_Load(object sender, EventArgs e)
        {
            GetMaxTRNNo();
            FillTransactionType();
            txtDocMonth.Text = CommonData.DocMonth;
            dtTRNDate.Value = Convert.ToDateTime(System.DateTime.Now);
            if (CommonData.LogUserId.ToUpper() == "ADMIN" || CommonData.LogUserRole.ToUpper() == "MANAGEMENT")
                txtDocMonth.ReadOnly = false;
            else
                txtDocMonth.ReadOnly = true;
        }

        private void FillTransactionType()
        {
            objUtilData = new UtilityDB();
            try
            {
                DataTable dt = objUtilData.dtSPIntConvTranType();
                if (dt.Rows.Count > 0)
                {
                    TranTypeLoad = true;
                    cmbTrnType.DataSource = dt;
                    cmbTrnType.DisplayMember = "type";
                    cmbTrnType.ValueMember = "name";
                }
                TranTypeLoad = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {

                objUtilData = null;
                Cursor.Current = Cursors.Default;
            }

        }

        public void GetMaxTRNNo()
        {
            objSQLDB = new SQLDB();
            txtTrnNo.Text = objSQLDB.ExecuteDataSet("SELECT isnull(max(SIDH_TRN_NO),0)+1 FROM SP_INTERNAL_DAMAGE_HEAD WHERE SIDH_BRANCH_CODE='" + CommonData.BranchCode + "'").Tables[0].Rows[0][0].ToString();
            objSQLDB = null;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }        
        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            ProductSearchAll PSearch = new ProductSearchAll("INTERNALDAMAGE");
            PSearch.objInternalDamage = this;
            PSearch.ShowDialog();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            GetMaxTRNNo();
            txtRemarks.Text = "";
            txtDocMonth.Text = CommonData.DocMonth;
            gvProductDetails.Rows.Clear();
            dtTRNDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {

                objSQLDB = new SQLDB();
                string sqlQry = "";
                DataSet dsExist = objSQLDB.ExecuteDataSet("SELECT COUNT(*) FROM SP_INTERNAL_DAMAGE_HEAD WHERE SIDH_BRANCH_CODE='" + CommonData.BranchCode + "' AND SIDH_TRN_NO=" + txtTrnNo.Text);
                if (dsExist.Tables[0].Rows[0][0].ToString() == "0")
                {
                    GetMaxTRNNo();
                    sqlQry = " INSERT INTO SP_INTERNAL_DAMAGE_HEAD"+
                                " (SIDH_COMPANY_CODE,SIDH_STATE_CODE"+
                                ",SIDH_BRANCH_CODE,SIDH_FIN_YEAR"+
                                ",SIDH_DOCUMENT_MONTH,SIDH_TRN_TYPE"+
                                ",SIDH_TRN_NO,SIDH_TRN_DATE,SIDH_REMARKS"+
                                ",SIDH_CREATED_BY,SIDH_CREATED_DATE) VALUES(" +
                                "'" + CommonData.CompanyCode + "','" + CommonData.StateCode + 
                                "','" + CommonData.BranchCode + "','" + CommonData.FinancialYear + 
                                "','" + txtDocMonth.Text.ToUpper() + "','" + cmbTrnType.SelectedValue.ToString() + 
                                "'," + txtTrnNo.Text + ",'" + Convert.ToDateTime(dtTRNDate.Value).ToString("dd/MMM/yyyy") +
                                "','" + txtRemarks.Text.Replace("'", "") + "','" + CommonData.LogUserId + 
                                "',getdate())";
                }
                else
                {
                    sqlQry = " UPDATE SP_INTERNAL_DAMAGE_HEAD SET SIDH_TRN_TYPE='" + cmbTrnType.SelectedValue.ToString() +
                                "',SIDH_TRN_DATE='" + Convert.ToDateTime(dtTRNDate.Value).ToString("dd/MMM/yyyy") +
                                "',SIDH_REMARKS='" + txtRemarks.Text.Replace("'","") +
                                "',SIDH_DOCUMENT_MONTH='" + txtDocMonth.Text.ToUpper() +
                                "',SIDH_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                                "',SIDH_LAST_MODIFIED_DATE=GETDATE() WHERE " +
                                "SIDH_BRANCH_CODE='" + CommonData.BranchCode +
                                "' AND SIDH_TRN_NO=" + txtTrnNo.Text;

                    sqlQry += " DELETE FROM SP_INTERNAL_DAMAGE_DETL "+
                                "WHERE SIDD_BRANCH_CODE='" + CommonData.BranchCode + 
                                "' AND SIDD_TRN_NO=" + txtTrnNo.Text;
                }
                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {
                    sqlQry += " INSERT INTO SP_INTERNAL_DAMAGE_DETL (SIDD_COMPANY_CODE,SIDD_STATE_CODE,SIDD_BRANCH_CODE,SIDD_FIN_YEAR,SIDD_TRN_NO,SIDD_TRN_SL_NO,SIDD_PRODUCT_ID,SIDD_BATCH_NO,SIDD_DMG_QTY)VALUES(" +
                        "'" + CommonData.CompanyCode + "','" + CommonData.StateCode + "','" + CommonData.BranchCode + "','" + CommonData.FinancialYear + "'," + txtTrnNo.Text + "," + Convert.ToInt32(i + 1) +
                        ",'" + gvProductDetails.Rows[i].Cells["ProductID"].Value + "','" + gvProductDetails.Rows[i].Cells["BatchNo"].Value + "','" + gvProductDetails.Rows[i].Cells["Qty"].Value + "')";
                }
                objSQLDB = new SQLDB();
                int iRetVal = objSQLDB.ExecuteSaveData(sqlQry);
                if (iRetVal > 0)
                    MessageBox.Show("Inserted data successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Data is not Inserted", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                objSQLDB = null;
                btnCancel_Click(null, null);
            }
        }

        private bool CheckData()
        {
            if (gvProductDetails.Rows.Count == 0)
            {
                MessageBox.Show("Select Atleast One Product", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void txtTrnNo_Validated(object sender, EventArgs e)
        {
            if (txtTrnNo.Text != "")
            {
                gvProductDetails.Rows.Clear();
                string strSql = " SELECT * FROM SP_INTERNAL_DAMAGE_HEAD WHERE SIDH_BRANCH_CODE='" + CommonData.BranchCode + "' AND SIDH_TRN_NO=" + txtTrnNo.Text;
                strSql += " SELECT A.*,PM_PRODUCT_NAME,CATEGORY_NAME FROM SP_INTERNAL_DAMAGE_DETL A INNER JOIN PRODUCT_MAS B ON A.SIDD_PRODUCT_ID=B.PM_PRODUCT_ID INNER JOIN " +
                    "CATEGORY_MASTER C ON B.PM_CATEGORY_ID=C.CATEGORY_ID WHERE SIDD_BRANCH_CODE='" + CommonData.BranchCode + "' AND SIDD_TRN_NO=" + txtTrnNo.Text;
                objSQLDB = new SQLDB();
                DataSet ds = objSQLDB.ExecuteDataSet(strSql);
                objSQLDB = null;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtRemarks.Text = ds.Tables[0].Rows[0]["SIDH_REMARKS"].ToString();
                    dtTRNDate.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["SIDH_TRN_DATE"]);
                    txtDocMonth.Text = ds.Tables[0].Rows[0]["SIDH_DOCUMENT_MONTH"].ToString();
                    cmbTrnType.SelectedValue = ds.Tables[0].Rows[0]["SIDH_TRN_TYPE"].ToString();

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        GetBindData(ds.Tables[1]);
                    }
                    else
                        gvProductDetails.Rows.Clear();
                }
                else
                {
                    txtDocMonth.Text = CommonData.DocMonth;
                    gvProductDetails.Rows.Clear();
                    btnCancel_Click(null, null);                    
                }
            }
        }
        public void GetBindData(DataTable dt)
        {
            int intRow = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellItemID = new DataGridViewTextBoxCell();
                cellItemID.Value = dt.Rows[i]["SIDD_PRODUCT_ID"].ToString();
                tempRow.Cells.Add(cellItemID);

                DataGridViewCell cellItem = new DataGridViewTextBoxCell();
                cellItem.Value = dt.Rows[i]["PM_PRODUCT_NAME"].ToString();
                tempRow.Cells.Add(cellItem);

                DataGridViewCell cellCategoty = new DataGridViewTextBoxCell();
                cellCategoty.Value = dt.Rows[i]["CATEGORY_NAME"].ToString();
                tempRow.Cells.Add(cellCategoty);
                
                DataGridViewCell cellItemName = new DataGridViewTextBoxCell();
                cellItemName.Value = dt.Rows[i]["SIDD_BATCH_NO"].ToString();
                tempRow.Cells.Add(cellItemName);

                DataGridViewCell cellAvailQty = new DataGridViewTextBoxCell();
                cellAvailQty.Value = dt.Rows[i]["SIDD_DMG_QTY"].ToString();
                tempRow.Cells.Add(cellAvailQty);
                intRow = intRow + 1;
                gvProductDetails.Rows.Add(tempRow);
            }
        }
        private void btnClearProd_Click(object sender, EventArgs e)
        {
            gvProductDetails.Rows.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (CommonData.LogUserId.ToUpper() == "ADMIN" || CommonData.LogUserRole.ToUpper() == "MANAGEMENT")
            {
                DialogResult result = MessageBox.Show("Do you want to Delete " + txtTrnNo.Text + " Transaction Permanently from Database?",
                                                   "SSERP", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    int iRetVal = 0;
                    objSQLDB = new SQLDB();
                    string sqlQry = " DELETE FROM SP_INTERNAL_DAMAGE_DETL " +
                                    "WHERE SIDD_BRANCH_CODE='" + CommonData.BranchCode +
                                    "' AND SIDD_TRN_NO=" + txtTrnNo.Text;

                    sqlQry += " DELETE FROM SP_INTERNAL_DAMAGE_HEAD " +
                                    "WHERE SIDH_BRANCH_CODE='" + CommonData.BranchCode +
                                    "' AND SIDH_TRN_NO=" + txtTrnNo.Text;
                    try { iRetVal = objSQLDB.ExecuteSaveData(sqlQry); }
                    catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                    if (iRetVal > 0)
                        MessageBox.Show("Data Deleted successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Data not Deleted", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    objSQLDB = null;
                    btnCancel_Click(null, null);
                }
            }
            else
                MessageBox.Show("You dont have permission to delete this Data", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
