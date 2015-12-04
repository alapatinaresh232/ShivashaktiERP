using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRM;
using SSAdmin;
using SSCRMDB;
using System.Collections;
using SSTrans;
using SSCRM.App_Code;
namespace SSCRM
{
    public partial class frmSrmonthlyInputes : Form
    {
        private StaffLevel objData = null;
        private InvoiceDB objInvoiceDB = null;
        private SQLDB objDA = null;
        public frmSrmonthlyInputes()
        {
            InitializeComponent();
        }

        private void frmSrmonthlyInputes_Load(object sender, EventArgs e)
        {
            lblDocMonth.Text = CommonData.DocMonth.ToString().ToUpper();
            GetSRBindData();
        }
        public void GetSRBindData()
        {
            gvProductDetails.Rows.Clear();
            Hashtable ht = new Hashtable();
            try
            {
                objInvoiceDB = new InvoiceDB();
                ht = objInvoiceDB.GetSalesDmSRData(CommonData.CompanyCode, CommonData.BranchCode, lblDocMonth.Text);
                int intRow = 1;
                DataTable dt = (DataTable)ht["SalseDM"];
                gvProductDetails.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataGridViewRow tempRow = new DataGridViewRow();
                    DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                    cellSLNO.Value = intRow;
                    tempRow.Cells.Add(cellSLNO);

                    DataGridViewCell cellsr_grp_name = new DataGridViewTextBoxCell();
                    cellsr_grp_name.Value = dt.Rows[i]["sr_grp_name"];
                    tempRow.Cells.Add(cellsr_grp_name);

                    DataGridViewCell cellsr_grp_eora_name = new DataGridViewTextBoxCell();
                    cellsr_grp_eora_name.Value = dt.Rows[i]["sr_grp_eora_name"];
                    tempRow.Cells.Add(cellsr_grp_eora_name);

                    DataGridViewCell cellsr_eora_code = new DataGridViewTextBoxCell();
                    cellsr_eora_code.Value = dt.Rows[i]["sr_eora_code"];
                    tempRow.Cells.Add(cellsr_eora_code);

                    DataGridViewCell cellsr_eora_name = new DataGridViewTextBoxCell();
                    cellsr_eora_name.Value = dt.Rows[i]["sr_eora_name"];
                    tempRow.Cells.Add(cellsr_eora_name);

                    DataGridViewCell cellSDSD_PMD = new DataGridViewTextBoxCell();
                    cellSDSD_PMD.Value = dt.Rows[i]["sr_PMD"];
                    tempRow.Cells.Add(cellSDSD_PMD);

                    DataGridViewCell cellSDSD_DA_DAYS = new DataGridViewTextBoxCell();
                    cellSDSD_DA_DAYS.Value = dt.Rows[i]["sr_DA_DAYS"];
                    tempRow.Cells.Add(cellSDSD_DA_DAYS);

                    DataGridViewCell cellSDSD_DEMOS = new DataGridViewTextBoxCell();
                    cellSDSD_DEMOS.Value = dt.Rows[i]["sr_DEMOS"];
                    tempRow.Cells.Add(cellSDSD_DEMOS);

                    DataGridViewCell cellsr_grp_eora_code = new DataGridViewTextBoxCell();
                    cellsr_grp_eora_code.Value = dt.Rows[i]["sr_grp_eora_code"];
                    tempRow.Cells.Add(cellsr_grp_eora_code);

                    intRow = intRow + 1;
                    gvProductDetails.Rows.Add(tempRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objInvoiceDB = null;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
         {
            objDA = new SQLDB();
            try
            {
                
                if (CheckData() == true)
                {
                    string strIns = "";
                    strIns = " DELETE FROM SALES_DM_SR_DATA WHERE sdsd_company_code='" + CommonData.CompanyCode + "' and sdsd_branch_code='" + CommonData.BranchCode + "' and sdsd_document_month='" + CommonData.DocMonth + "'";
                    //objDA.ExecuteSaveData(strSQL);
                    //strSQL = "SELECT SDSD_EORA_CODE FROM SALES_DM_SR_DATA WHERE sdsd_company_code='" + CommonData.CompanyCode + "' and sdsd_branch_code='" + CommonData.BranchCode + "' and sdsd_document_month='" + CommonData.DocMonth + "'";
                    //DataView dvFil = objDA.ExecuteDataSet(strSQL).Tables[0].DefaultView;

                    for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                    {
                        //dvFil.RowFilter = "SDSD_EORA_CODE=" + gvProductDetails.Rows[i].Cells["sr_eora_code"].Value;
                        //DataTable dt;
                        //dt = dvFil.ToTable();

                        string iPMD = gvProductDetails.Rows[i].Cells["sr_PMD"].Value.ToString() == "" ? "0" : gvProductDetails.Rows[i].Cells["sr_PMD"].Value.ToString();
                        string iDA = gvProductDetails.Rows[i].Cells["sr_da_days"].Value.ToString() == "" ? "0" : gvProductDetails.Rows[i].Cells["sr_da_days"].Value.ToString();
                        string iDemos = gvProductDetails.Rows[i].Cells["sr_Demos"].Value.ToString() == "" ? "0" : gvProductDetails.Rows[i].Cells["sr_Demos"].Value.ToString();

                        //if (dt.Rows.Count == 0)
                        //{
                            strIns += " INSERT INTO SALES_DM_SR_DATA(SDSD_COMPANY_CODE,SDSD_STATE_CODE,SDSD_BRANCH_CODE," +
                         " SDSD_FIN_YEAR,SDSD_DOCUMENT_MONTH,SDSD_GROUP_NAME,SDSD_GROUP_LEAD_ECODE,SDSD_EORA_CODE,SDSD_PMD," +
                         " SDSD_DA_DAYS,SDSD_DEMOS,SDSD_CREATED_BY,SDSD_AUTHORIZED_BY,SDSD_CREATED_DATE) VALUES(" +
                         "'" + CommonData.CompanyCode + "','" + CommonData.StateCode + "','" + CommonData.BranchCode + "','" + CommonData.FinancialYear + "','" +
                         CommonData.DocMonth + "','" + gvProductDetails.Rows[i].Cells["sr_grp_name"].Value + "','" + gvProductDetails.Rows[i].Cells["sr_grp_eora_code"].Value +
                         "','" + gvProductDetails.Rows[i].Cells["sr_eora_code"].Value + "'," + iPMD + "," + iDA + "," + iDemos + ",'" + CommonData.LogUserId + "','" +
                         CommonData.LogUserId + "','" + CommonData.CurrentDate + "');";
                        //}
                        //else
                        //{
                        //    strIns += " UPDATE SALES_DM_SR_DATA SET SDSD_PMD=" + iPMD + ",SDSD_DA_DAYS=" + iDA + ",SDSD_DEMOS=" + iDemos + " WHERE sdsd_company_code='" + CommonData.CompanyCode + "' and sdsd_branch_code='" + CommonData.BranchCode + "' and sdsd_document_month='" + CommonData.DocMonth + "' and sdsd_eora_code='" + gvProductDetails.Rows[i].Cells["sr_eora_code"].Value + "'; ";
                        //}
                    }
                    int iretval = objDA.ExecuteSaveData(strIns);
                    objDA = null;
                    MessageBox.Show("Data saved successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                GetSRBindData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("Please enter valid numbers", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool CheckData()
        {
            bool blValue = true;
            //bool blInvDtl = false;
            //for (int i = 0; i < gvProductDetails.Rows.Count; i++)
            //{
            //    if ((Convert.ToString(gvProductDetails.Rows[i].Cells["sr_PMD"].Value) != "") || (Convert.ToString(gvProductDetails.Rows[i].Cells["sr_da_days"].Value) != "") && (Convert.ToString(gvProductDetails.Rows[i].Cells["sr_Demos"].Value) != ""))
            //    {
            //        blInvDtl = true;
            //    }
            //}
            if (gvProductDetails.Rows.Count == 0)
            {
                MessageBox.Show("Enter Atleast One Record to Save details", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            //if (blInvDtl == false)
            //{
            //    blValue = false;
            //    MessageBox.Show("Enter PMD,DA and Demos details", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}
            return blValue;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GetSRBindData();
        }

        private void txtSearch_Validated(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in gvProductDetails.Rows)
            {
                if (UtilityFunctions.IsNumeric(txtSearch.Text) == true)
                {
                    if (row.Cells["sr_eora_code"].Value.ToString().Contains(txtSearch.Text.ToUpper()))
                    {
                        gvProductDetails.Rows[row.Index].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else
                        gvProductDetails.Rows[row.Index].DefaultCellStyle.BackColor = Color.White;
                }
                else
                {
                    if (row.Cells["sr_eora_name"].Value.ToString().Contains(txtSearch.Text.ToUpper()))
                    {
                        gvProductDetails.Rows[row.Index].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else
                        gvProductDetails.Rows[row.Index].DefaultCellStyle.BackColor = Color.White;
                }
                if (txtSearch.Text == "")
                    gvProductDetails.Rows[row.Index].DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void gvProductDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 0)
            {
                try
                {
                    if (UtilityFunctions.IsNumeric(gvProductDetails.Rows[e.RowIndex].Cells["sr_PMD"].Value.ToString()) == false)
                        gvProductDetails.Rows[e.RowIndex].Cells["sr_PMD"].Value = "0";
                    if (UtilityFunctions.IsNumeric(gvProductDetails.Rows[e.RowIndex].Cells["sr_DA_DAYS"].Value.ToString()) == false)
                        gvProductDetails.Rows[e.RowIndex].Cells["sr_DA_DAYS"].Value = "0";
                    if (UtilityFunctions.IsNumeric(gvProductDetails.Rows[e.RowIndex].Cells["sr_DEMOS"].Value.ToString()) == false)
                        gvProductDetails.Rows[e.RowIndex].Cells["sr_DEMOS"].Value = "0";

                    if (Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["sr_PMD"].Value) != "")
                    {
                        int daysIn = System.DateTime.DaysInMonth(Convert.ToDateTime(CommonData.DocMonth).Year, Convert.ToDateTime(CommonData.DocMonth).Month);
                        if (Convert.ToInt32(gvProductDetails.Rows[e.RowIndex].Cells["sr_PMD"].Value) > daysIn)
                        {
                            MessageBox.Show("Please enter number bellow month days", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            gvProductDetails.Rows[e.RowIndex].Cells["sr_PMD"].Value = "0";
                        }
                    }
                    if (Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["sr_DA_DAYS"].Value) != "")
                    {
                        if (Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["sr_PMD"].Value) == "")
                        {
                            MessageBox.Show("Please enter PMD value", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            gvProductDetails.Rows[e.RowIndex].Cells["sr_DA_DAYS"].Value = "0";
                        }
                        else
                        {
                            if (Convert.ToInt32(gvProductDetails.Rows[e.RowIndex].Cells["sr_PMD"].Value) < Convert.ToInt32(gvProductDetails.Rows[e.RowIndex].Cells["sr_DA_DAYS"].Value))
                            {
                                MessageBox.Show("Please enter number bellow PMD value", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                gvProductDetails.Rows[e.RowIndex].Cells["sr_DA_DAYS"].Value = "0";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please enter valid numbers", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
