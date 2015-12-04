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
    public partial class BranchStationaryIssue : Form
    {
        private SQLDB objSQLDB = null;
        private bool isModify = false;
        private StationaryDB objStationaryDB = null;
        public BranchStationaryIssue()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            StationaryItemsSearch ItemSearch = new StationaryItemsSearch("BrStationaryItemsIssue");
            ItemSearch.objBranchStationaryIssue = this;
            ItemSearch.ShowDialog();
        }

        private void BranchStationaryIssue_Load(object sender, EventArgs e)
        {
            txtTrnNo.Text = GenerateNewTrnNo().ToString();
            dtpTrnDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            gvStatItemsDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);
        }

        private int GenerateNewTrnNo()
        {
            int iMax = 0;
            try
            {
                objSQLDB = new SQLDB();
                string strSql = "SELECT ISNULL(MAX(BSIH_TRN_NUMBER),0)+1 FROM STBR_ITEMS_ISSUE_HEAD WHERE BSIH_BRANCH_CODE = '" + CommonData.BranchCode + "' AND BSIH_FIN_YEAR = '" + CommonData.FinancialYear + "'";                
                iMax = Convert.ToInt32(objSQLDB.ExecuteDataSet(strSql).Tables[0].Rows[0][0].ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLDB = null;
            }
            return iMax;

        }

        private void btnClearProd_Click(object sender, EventArgs e)
        {
            gvStatItemsDetails.Rows.Clear();
        }

        private void txtEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtEcode_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcode.Text.Length > 4)
            {
                txtEmpName.Text = GetEmpName(txtEcode.Text);
            }
            else
                txtEmpName.Text = "";
        }

        private string GetEmpName(string strEcode)
        {
            string strName = "";
            try
            {
                objSQLDB = new SQLDB();
                string strSql = "SELECT CAST(ECODE AS VARCHAR)+'-'+MEMBER_NAME FROM EORA_MASTER WHERE ECODE = " + strEcode;
                strName = objSQLDB.ExecuteDataSet(strSql).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLDB = null;
            }
            return strName;
        }

        private void txtTrnNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {              
                string strSql = "";
                int iRes = 0;
                if (SaveHeadData() > 0)
                    iRes = SaveDetlData();
                if (iRes > 0)
                {
                    MessageBox.Show("Data Saved Successfully", "SSCRM-Stationary", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCancel_Click(null, null);
                }
                else
                {
                    try
                    {
                        objSQLDB = new SQLDB();
                        strSql += " DELETE FROM STBR_ITEMS_ISSUE_DETL WHERE BSID_BRANCH_CODE='" + CommonData.BranchCode + "' AND BSID_FIN_YEAR = '" + CommonData.FinancialYear + "' AND BSID_TRN_NUMBER =" + txtTrnNo.Text;
                        strSql += " DELETE FROM STBR_ITEMS_ISSUE_HEAD WHERE BSIH_BRANCH_CODE='" + CommonData.BranchCode + "' AND BSIH_FIN_YEAR = '" + CommonData.FinancialYear + "' AND BSIH_TRN_NUMBER =" + txtTrnNo.Text;
                        objSQLDB.ExecuteSaveData(strSql);
                        MessageBox.Show("Data Not Saved", "SSCRM-Stationary", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Data Not Saved\n" + ex.ToString(), "SSCRM-Stationary", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    finally
                    {
                        objSQLDB = null;
                    }
                }

            }
        }

        private int SaveDetlData()
        {
            objSQLDB = new SQLDB();
            int iRes = 0;
            string strSql = "";
            strSql = " DELETE FROM STBR_ITEMS_ISSUE_DETL WHERE BSID_BRANCH_CODE='" + CommonData.BranchCode + "' AND BSID_FIN_YEAR = '" + CommonData.FinancialYear + "' AND BSID_TRN_NUMBER =" + txtTrnNo.Text;
            objSQLDB.ExecuteSaveData(strSql);
            strSql = "";
            for (int i = 0; i < gvStatItemsDetails.Rows.Count; i++)
            {
                if (Convert.ToDouble(gvStatItemsDetails.Rows[i].Cells["Qty"].Value).ToString() != "0")
                {
                    strSql += "INSERT INTO STBR_ITEMS_ISSUE_DETL" +
                                "(BSID_COMPANY_CODE" +
                                ",BSID_BRANCH_CODE" +
                                ",BSID_FIN_YEAR" +
                                ",BSID_TRN_NUMBER" +
                                ",BSID_TRN_SL_NO" +
                                ",BSID_ITEM_ID" +
                                ",BSID_ITEM_QTY" +
                                ") " +
                                "VALUES" +
                                "('" + CommonData.CompanyCode +
                                "','" + CommonData.BranchCode +
                                "','" + CommonData.FinancialYear +
                                "'," + txtTrnNo.Text +
                                "," + (i + 1) +
                                "," + gvStatItemsDetails.Rows[i].Cells["ItemID"].Value +
                                "," + gvStatItemsDetails.Rows[i].Cells["Qty"].Value +
                                "); ";
                }
            }

          
                try
                {
                    iRes = objSQLDB.ExecuteSaveData(strSql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSQLDB = null;
                }
            
            
            
            return iRes;
        }

        private int SaveHeadData()
        {
            int iRes = 0;
            string strSql = "";
            if (gvStatItemsDetails.Rows.Count > 0)
            {
                if (isModify == false)
                {

                    txtTrnNo.Text = GenerateNewTrnNo().ToString();
                    strSql = "INSERT INTO STBR_ITEMS_ISSUE_HEAD" +
                                "(BSIH_COMPANY_CODE" +
                                ",BSIH_BRANCH_CODE" +
                                ",BSIH_FIN_YEAR" +
                                ",BSIH_TRN_NUMBER" +
                                ",BSIH_TRN_DATE" +
                                ",BSIH_EORA_CODE" +
                                ",BSIH_REMARKS" +
                                ",BSIH_CREATED_BY" +
                                ",BSIH_CREATED_DATE) " +
                                "VALUES" +
                                "('" + CommonData.CompanyCode +
                                "','" + CommonData.BranchCode +
                                "','" + CommonData.FinancialYear +
                                "'," + txtTrnNo.Text +
                                ",'" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") +
                                "'," + txtEcode.Text +
                                ",'" + txtRemarks.Text.Replace("/'", "") +
                                "','" + CommonData.LogUserId +
                                "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                "'); ";

                }

                else
                {
                    strSql = "UPDATE STBR_ITEMS_ISSUE_HEAD SET " +
                                "BSIH_TRN_DATE = '" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") +
                                "',BSIH_EORA_CODE = " + txtEcode.Text +
                                ",BSIH_REMARKS = '" + txtRemarks.Text +
                                "',BSIH_LAST_MODIFIED_BY = '" + CommonData.LogUserId +
                                "',BSIH_LAST_MODIFIED_DATE = '" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                "' WHERE " +
                                "BSIH_COMPANY_CODE = '" + CommonData.CompanyCode +
                                "' AND BSIH_BRANCH_CODE = '" + CommonData.BranchCode +
                                "' AND BSIH_FIN_YEAR = '" + CommonData.FinancialYear +
                                "' AND BSIH_TRN_NUMBER = " + txtTrnNo.Text;
                }
            }
                objSQLDB = new SQLDB();
                try
                {
                    iRes = objSQLDB.ExecuteSaveData(strSql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSQLDB = null;
                }
            

            return iRes;
        }
        

        private bool CheckData()
        {
            bool bFlag = true;
            if (txtEmpName.Text.Trim().ToString() == "")
            {
                bFlag = false;
                MessageBox.Show("Invalid Ecode!","SSCRM-Stationary",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                return bFlag;
            }
            if (gvStatItemsDetails.Rows.Count > 0)
            {
              
                for (int i = 0; i < gvStatItemsDetails.Rows.Count; i++)
                {
                    if (isModify == false)
                    {
                        if (Convert.ToString(gvStatItemsDetails.Rows[i].Cells["Qty"].Value).ToString() != "")
                        {
                            double Issuedqty = Convert.ToDouble(gvStatItemsDetails.Rows[i].Cells["Qty"].Value.ToString());
                            double AvilableQty = Convert.ToDouble(gvStatItemsDetails.Rows[i].Cells["AvailableQty"].Value.ToString());

                            if (Issuedqty > AvilableQty)
                            {
                                bFlag = false;
                                MessageBox.Show("Invalid Item Qty!", "SSCRM-Stationary", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                return bFlag;
                            }

                        }
                        else
                        {
                            bFlag = false;
                            MessageBox.Show("Enter Issued Qty!", "SSCRM-Stationary", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return bFlag;
                        }
                    }
                    else
                    {
                        if (gvStatItemsDetails.Rows[i].Cells["Qty"].Value.ToString() == "" || gvStatItemsDetails.Rows[i].Cells["Qty"].Value.ToString() == "0")
                        {
                            bFlag = false;
                            MessageBox.Show("Invalid Item Qty!", "SSCRM-Stationary", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return bFlag;
                        }

                    }
                }
            }
            else
            {
                bFlag = false;
                MessageBox.Show("Enter Atleast One Item!", "SSCRM-Stationary", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return bFlag;
            }
            return bFlag;
        }

        private void txtTrnNo_Validated(object sender, EventArgs e)
        {
            if (txtTrnNo.Text.Trim().Length > 0)
            {
                FillTransactionData();
            }
            else
                ClearData();
        }

        private void FillTransactionData()
        {
            DataSet ds = new DataSet();            
            DataTable dtHead = new DataTable();
            DataTable dtDetl = new DataTable();
            objStationaryDB = new StationaryDB();
            gvStatItemsDetails.Rows.Clear();
            ds = objStationaryDB.Get_BR_StationaryIssueData_ByTrnNo(CommonData.CompanyCode, CommonData.BranchCode, CommonData.FinancialYear, txtTrnNo.Text);
            dtHead = ds.Tables[0];
            dtDetl = ds.Tables[1];            
            if (dtHead.Rows.Count > 0)
            {
                dtpTrnDate.Value = Convert.ToDateTime(dtHead.Rows[0]["BSIH_TRN_DATE"]);
                txtEcode.Text = dtHead.Rows[0]["BSIH_EORA_CODE"].ToString();
                txtEmpName.Text = dtHead.Rows[0]["ENAME"].ToString();
                txtRemarks.Text = dtHead.Rows[0]["BSIH_REMARKS"].ToString();
                isModify = true;
                if (dtDetl.Rows.Count > 0)
                {
                    for (int i = 0; i < dtDetl.Rows.Count; i++)
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSlNo = new DataGridViewTextBoxCell();
                        cellSlNo.Value = i + 1;
                        tempRow.Cells.Add(cellSlNo);

                        DataGridViewCell cellItemId = new DataGridViewTextBoxCell();
                        cellItemId.Value = dtDetl.Rows[i]["BSID_ITEM_ID"].ToString();
                        tempRow.Cells.Add(cellItemId);

                        DataGridViewCell cellItemName = new DataGridViewTextBoxCell();
                        cellItemName.Value = dtDetl.Rows[i]["SIM_ITEM_NAME"].ToString();
                        tempRow.Cells.Add(cellItemName);

                        DataGridViewCell cellAvaiQty = new DataGridViewTextBoxCell();
                        cellAvaiQty.Value = "";
                        tempRow.Cells.Add(cellAvaiQty);

                        DataGridViewCell cellItemQty = new DataGridViewTextBoxCell();
                        cellItemQty.Value = dtDetl.Rows[i]["BSID_ITEM_QTY"].ToString();
                        tempRow.Cells.Add(cellItemQty);

                        gvStatItemsDetails.Rows.Add(tempRow);
                    }
                }

                if (CommonData.LogUserId.ToUpper() == "ADMIN" || CommonData.LogUserRole == "MANAGEMENT")
                {
                    btnSave.Enabled = true;
                    btnDelete.Enabled = true;
                }
                else
                {
                    btnSave.Enabled = false;
                    btnDelete.Enabled = false;
                }

            }
            else
            {
                ClearData();
            }
        }

        private void ClearData()
        {
            isModify = false;
            txtTrnNo.Text = GenerateNewTrnNo().ToString();
            dtpTrnDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            txtEcode.Text = "";
            txtEmpName.Text = "";
            gvStatItemsDetails.Rows.Clear();
            btnSave.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            string strSql = "";
            int iRes = 0;
            try
            {
                strSql += " DELETE FROM STBR_ITEMS_ISSUE_DETL WHERE BSID_BRANCH_CODE='" + CommonData.BranchCode + "' AND BSID_FIN_YEAR = '" + CommonData.FinancialYear + "' AND BSID_TRN_NUMBER =" + txtTrnNo.Text;
                strSql += " DELETE FROM STBR_ITEMS_ISSUE_HEAD WHERE BSIH_BRANCH_CODE='" + CommonData.BranchCode + "' AND BSIH_FIN_YEAR = '" + CommonData.FinancialYear + "' AND BSIH_TRN_NUMBER =" + txtTrnNo.Text;

                iRes = objSQLDB.ExecuteSaveData(strSql);
                if (iRes > 0)
                {
                    MessageBox.Show("Data Deleted Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearData();
                }
                else
                {
                    MessageBox.Show("Data Not Deleted", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLDB = null;
            }
        }

        private void gvStatItemsDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvStatItemsDetails.Columns["Delete"].Index)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    //string ProductId = gvProductDetails.Rows[e.RowIndex].Cells[gvProductDetails.Columns["ProductId"].Index].Value.ToString();
                    DataGridViewRow dgvr = gvStatItemsDetails.Rows[e.RowIndex];
                    gvStatItemsDetails.Rows.Remove(dgvr);
                    OrderSlNo();
                }
            }
        }

        private void OrderSlNo()
        {
            if (gvStatItemsDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvStatItemsDetails.Rows.Count; i++)
                {
                    gvStatItemsDetails.Rows[i].Cells["SLNO"].Value = i + 1;
                }
            }
        }
    }
}
