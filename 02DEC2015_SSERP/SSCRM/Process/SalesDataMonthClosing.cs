using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSAdmin;
using SSTrans;
using SSCRM.App_Code;
namespace SSCRM
{
    public partial class SalesDataMonthClosing : Form
    {
        private SQLDB objDB = null;
        private Security objSecurity = null;
        private InvoiceDB objInvDB = null;
        private HRInfo objHrInfo = null;
        private string sForm = "";
        public SalesDataMonthClosing()
        {
            InitializeComponent();
        }

        public SalesDataMonthClosing(string sString)
        {
            sForm = sString;
            InitializeComponent();
        }

        private void SalesDataMonthClosing_Load(object sender, EventArgs e)
        {
            FillCompanyDropDownList();
            gvProductDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);
            dtpInvoiceDate.Value = Convert.ToDateTime(CommonData.DocMonth);
            if (sForm == "LOCK")
            {
                btnSave.Text = "Lock";
            }
            else
            {
                btnSave.Text = "UnLock";
            }
        }

        private void FillCompanyDropDownList()
        {
            objDB = new SQLDB();

            if (CommonData.LogUserId.ToUpper() != "ADMIN")
            {
                UtilityLibrary.PopulateControl(cbCompany, objDB.ExecuteDataSet("SELECT distinct CM_COMPANY_CODE,CM_COMPANY_NAME FROM branch_mas " +
                                                                            "INNER JOIN USER_BRANCH ON UB_BRANCH_CODE = BRANCH_CODE " +
                                                                            "INNER JOIN COMPANY_MAS ON CM_COMPANY_CODE = COMPANY_CODE " +
                                                                            "INNER JOIN USER_MASTER ON UM_USER_ID = UB_USER_ID " +
                                                                            "WHERE upper(UM_USER_ID) = Upper('" + CommonData.LogUserId + "')").Tables[0].DefaultView, 1, 0, "--PLEASE SELECT--", 0);
            }
            else
            {
                UtilityLibrary.PopulateControl(cbCompany, objDB.ExecuteDataSet("SELECT distinct CM_COMPANY_CODE,CM_COMPANY_NAME FROM COMPANY_MAS").Tables[0].DefaultView, 1, 0, "--PLEASE SELECT--", 0);
            }

            FillDataToGrid();
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                //if (CommonData.LogUserId.ToUpper() != "ADMIN")
                //{
                    objHrInfo = new HRInfo();
                    DataTable dtBranch = objHrInfo.GetUserBranchList(CommonData.LogUserId, cbCompany.SelectedValue.ToString(), "BR", "").Tables[0];
                    UtilityLibrary.PopulateControl(cbUserBranch, dtBranch.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
                    objHrInfo = null;
                //}
                //else
                //{
                //    objHrInfo = new HRInfo();
                //    DataTable dtBranch = objHrInfo.GetAllBranchList(cbCompany.SelectedValue.ToString(), "BR", "").Tables[0];
                //    UtilityLibrary.PopulateControl(cbUserBranch, dtBranch.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
                //    objHrInfo = null;
                //}
            }
            else
            {
                cbUserBranch.Items.Clear();
            }
            FillDataToGrid();
        }

        private void FillDataToGrid()
        {
            if (cbCompany.SelectedIndex > 0 && cbUserBranch.SelectedIndex > 0)
            {
                gvProductDetails.Rows.Clear();
                DataTable dtSales = new DataTable();
                objInvDB = new InvoiceDB();
                try
                {
                    dtSales = objInvDB.Get_SalesDifferenceChecklictForGCInBranches(cbCompany.SelectedValue.ToString(), cbUserBranch.SelectedValue.ToString(), dtpInvoiceDate.Value.ToString("MMMyyyy").ToUpper(), "GROUP SUMMARY").Tables[0];

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objInvDB = null;
                }
                DataView dtSalesView = new DataView();
                dtSalesView = dtSales.DefaultView;
                if (sForm == "LOCK")
                {
                    dtSalesView.RowFilter = " brgrp_lock_status not in ('Y')";
                }
                else
                {
                    dtSalesView.RowFilter = " brgrp_lock_status = 'Y'";
                }
                dtSales = dtSalesView.ToTable();
                if (dtSales.Rows.Count > 0)
                {
                    for (int i = 0; i < dtSales.Rows.Count; i++)
                    {
                        gvProductDetails.Rows.Add();
                        gvProductDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                        gvProductDetails.Rows[i].Cells["Ecode"].Value = dtSales.Rows[i]["brGrp_grp_ecode"].ToString();
                        gvProductDetails.Rows[i].Cells["GCName"].Value = dtSales.Rows[i]["brGrp_grp_ename"].ToString();
                        gvProductDetails.Rows[i].Cells["Camp"].Value = dtSales.Rows[i]["brGrp_grp_name"].ToString();
                        gvProductDetails.Rows[i].Cells["DiffCust"].Value = dtSales.Rows[i]["brGrp_diff_Noof_Invoices"].ToString();
                        gvProductDetails.Rows[i].Cells["DiffQty"].Value = dtSales.Rows[i]["brGrp_diff_qty"].ToString();
                        gvProductDetails.Rows[i].Cells["DiffRev"].Value = dtSales.Rows[i]["brGrp_diff_amt"].ToString();
                        gvProductDetails.Rows[i].Cells["DiffPoints"].Value = dtSales.Rows[i]["brGrp_diff_prod_points"].ToString();
                        gvProductDetails.Rows[i].Cells["DiffFreeUnits"].Value = dtSales.Rows[i]["brGrp_diff_free_units"].ToString();
                        gvProductDetails.Rows[i].Cells["SumCust"].Value = dtSales.Rows[i]["brGrp_summ_ssbh_cust"].ToString();
                        gvProductDetails.Rows[i].Cells["SumQty"].Value = dtSales.Rows[i]["brGrp_summ_ssbh_qty"].ToString();
                        gvProductDetails.Rows[i].Cells["SumRev"].Value = dtSales.Rows[i]["brGrp_summ_ssbh_rev"].ToString();
                        gvProductDetails.Rows[i].Cells["SumPoints"].Value = dtSales.Rows[i]["brGrp_summ_ssbh_points"].ToString();
                        gvProductDetails.Rows[i].Cells["InvCust"].Value = dtSales.Rows[i]["brGrp_Noof_Invoices"].ToString();
                        gvProductDetails.Rows[i].Cells["InvQty"].Value = dtSales.Rows[i]["brGrp_qty"].ToString();
                        gvProductDetails.Rows[i].Cells["InvRev"].Value = dtSales.Rows[i]["brGrp_amt"].ToString();
                        gvProductDetails.Rows[i].Cells["InvPoints"].Value = dtSales.Rows[i]["brGrp_prod_points"].ToString();
                        gvProductDetails.Rows[i].Cells["aFlag"].Value = "";
                        if (dtSales.Rows[i]["brgrp_lock_status"].ToString() == "Y")
                        {
                            gvProductDetails.Rows[i].Cells["Status"].Value = "LOCKED";
                        }
                        else
                        {
                            gvProductDetails.Rows[i].Cells["Status"].Value = "UNLOCKED";
                        }

                    }
                }
            }
            else
            {
                gvProductDetails.Rows.Clear();
            }

        }

        private void dtpInvoiceDate_ValueChanged(object sender, EventArgs e)
        {
            FillDataToGrid();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void gvProductDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvProductDetails.Columns["chkApprove"].Index)
            {
                if (sForm == "LOCK")
                {
                    //bool isExist = true;
                    bool cbchecked = (bool)gvProductDetails.Rows[e.RowIndex].Cells["chkApprove"].EditedFormattedValue;
                    if (cbchecked == false)
                    {
                        if (cbchecked == false)
                        {
                            if (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["DiffCust"].Value.ToString()) == 0
                                && Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["DiffQty"].Value.ToString()) == 0
                                && Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["DiffRev"].Value.ToString()) == 0
                                && Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["DiffFreeUnits"].Value.ToString()) == 0
                                && Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["DiffPoints"].Value.ToString()) == 0)
                            {
                                //MessageBox.Show("Incomplete Data For Agent : " + gvProductDetails.Rows[e.RowIndex].Cells["HAMH_EORA_CODE"].Value, "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                gvProductDetails.Rows[e.RowIndex].Cells["aFlag"].Value = "YES";
                            }
                            else
                            {
                                gvProductDetails.Rows[e.RowIndex].Cells["aFlag"].Value = "NO";
                            }
                        }
                        else
                        {
                            gvProductDetails.Rows[e.RowIndex].Cells["aFlag"].Value = "NO";
                        }
                    }
                }
                else
                {
                    //bool isExist = true;
                    bool cbchecked = (bool)gvProductDetails.Rows[e.RowIndex].Cells["chkApprove"].EditedFormattedValue;
                    if (cbchecked == false)
                    {
                        if (cbchecked == false)
                        {
                            gvProductDetails.Rows[e.RowIndex].Cells["aFlag"].Value = "YES";
                        }
                        else
                        {
                            gvProductDetails.Rows[e.RowIndex].Cells["aFlag"].Value = "NO";
                        }
                    }
                }

            }
        }

        private void gvProductDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 13)
            {
                if (e.RowIndex != -1)
                {
                    foreach (DataGridViewRow row in gvProductDetails.Rows)
                    {
                        if (row.Cells["aflag"].Value.ToString() == "YES")
                            row.Cells[13].Value = false;
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string sqlText = "";
            objDB = new SQLDB();
            int iRes = 0;
            if (sForm == "LOCK")
            {
                //for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                //{
                //    try
                //    {
                //        gvProductDetails.Rows[i].Cells["aFlag"].Value.ToString();
                //    }
                //    catch
                //    {
                //        gvProductDetails.Rows[i].Cells["aFlag"].Value = "";
                //    }
                //}
                try
                {
                    for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                    {
                        if (gvProductDetails.Rows[i].Cells["aFlag"].Value.ToString() == "YES")
                        {
                            sqlText += "ALTER TABLE SALES_SUMMARY_BULLETINS DISABLE TRIGGER TR_SSBH_ECODE_UPDATE ";
                            sqlText += "UPDATE SALES_SUMMARY_BULLETINS SET SSBH_LOCKING = 'Y'" +
                                        ",SSBH_LAST_LOCKING_DATE=GETDATE(),SSBH_LOCKED_BY='" + CommonData.LogUserId + "' " +
                                        "WHERE SSBH_EORA_CODE IN (SELECT lgm_source_ecode " +
                                        "FROM LevelGroup_map WHERE lgm_branch_code = '" + cbUserBranch.SelectedValue.ToString() + "' " +
                                        "AND lgm_doc_month = '" + dtpInvoiceDate.Value.ToString("MMMyyyy").ToUpper() +
                                        "' AND lgm_group_ecode = " + gvProductDetails.Rows[i].Cells["Ecode"].Value.ToString() +
                                        " UNION SELECT lgm_group_ecode FROM LevelGroup_map WHERE lgm_branch_code = '" + cbUserBranch.SelectedValue.ToString() +
                                        "' AND lgm_doc_month = '" + dtpInvoiceDate.Value.ToString("MMMyyyy").ToUpper() +
                                        "' AND lgm_group_ecode = " + gvProductDetails.Rows[i].Cells["Ecode"].Value.ToString() + ") " +
                                        "AND SSBH_BRANCH_CODE = '" + cbUserBranch.SelectedValue.ToString() +
                                        "' AND SSBH_DOC_MONTH = '" + dtpInvoiceDate.Value.ToString("MMMyyyy").ToUpper() + "' ";
                            sqlText += "ALTER TABLE SALES_SUMMARY_BULLETINS ENABLE TRIGGER TR_SSBH_ECODE_UPDATE ";
                        }
                    }
                    if (sqlText.Length > 10)
                    {
                        iRes = objDB.ExecuteSaveData(sqlText);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objDB = null;
                }
                if (iRes > 0)
                {
                    MessageBox.Show("Locked Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillDataToGrid();
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                    {
                        if (gvProductDetails.Rows[i].Cells["aFlag"].Value.ToString() == "YES")
                        {

                            sqlText += "ALTER TABLE SALES_SUMMARY_BULLETINS DISABLE TRIGGER TR_SSBH_ECODE_UPDATE ";
                            sqlText += "UPDATE SALES_SUMMARY_BULLETINS SET SSBH_LOCKING = 'N'" +
                                        ",SSBH_UNLOCK_DATE=GETDATE(),SSBH_UNLOCKED_BY='" + CommonData.LogUserId + "' " +
                                        "WHERE SSBH_EORA_CODE IN (SELECT lgm_source_ecode " +
                                        "FROM LevelGroup_map WHERE lgm_branch_code = '" + cbUserBranch.SelectedValue.ToString() + "' " +
                                        "AND lgm_doc_month = '" + dtpInvoiceDate.Value.ToString("MMMyyyy").ToUpper() +
                                        "' AND lgm_group_ecode = " + gvProductDetails.Rows[i].Cells["Ecode"].Value.ToString() +
                                        " UNION SELECT lgm_group_ecode FROM LevelGroup_map WHERE lgm_branch_code = '" + cbUserBranch.SelectedValue.ToString() +
                                        "' AND lgm_doc_month = '" + dtpInvoiceDate.Value.ToString("MMMyyyy").ToUpper() +
                                        "' AND lgm_group_ecode = " + gvProductDetails.Rows[i].Cells["Ecode"].Value.ToString() + ") " +
                                        "AND SSBH_BRANCH_CODE = '" + cbUserBranch.SelectedValue.ToString() +
                                        "' AND SSBH_DOC_MONTH = '" + dtpInvoiceDate.Value.ToString("MMMyyyy").ToUpper() + "' ";
                            sqlText += "ALTER TABLE SALES_SUMMARY_BULLETINS ENABLE TRIGGER TR_SSBH_ECODE_UPDATE ";


                        }
                    }
                    if (sqlText.Length > 10)
                    {
                        iRes = objDB.ExecuteSaveData(sqlText);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objDB = null;
                }
                if (iRes > 0)
                {
                    MessageBox.Show("UnLocked Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillDataToGrid();
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private void cbUserBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDataToGrid();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbCompany.SelectedIndex = 0;
            gvProductDetails.Rows.Clear();
        }


    }
}
