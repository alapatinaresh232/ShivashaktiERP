using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSTrans;
using SSAdmin;
using SSCRMDB;
using System.Collections;

namespace SSCRM
{
    public partial class frmOrderSheetIssue : Form
    {

        private StaffLevel objData = null;
        private InvoiceDB objInvoiceDB = null;
        private SQLDB objDA = null;
        private string strECode = string.Empty;
        Int32 nTotOrders = 0;

        public frmOrderSheetIssue()
        {
            InitializeComponent();
        }

        private void frmOrderSheetIssue_Load(object sender, EventArgs e)
        {
            meInvoiceDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            lblDocMonth.Text = CommonData.DocMonth.ToString().ToUpper();
            chklNumbers.Items.Clear();
            FillEmployeeData();
            cbEcode.SelectedIndex = -1;
        }

        private void EcodeSearch()
        {

            objInvoiceDB = new InvoiceDB();
            DataSet dsEmp = null;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                cbEcode.DataSource = null;
                cbEcode.Items.Clear();
                dsEmp = objInvoiceDB.OrderSheetIssueEcodeSearch_Get(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth, txtEcodeSearch.Text.ToString());
                DataTable dtEmp = dsEmp.Tables[0];
                if (dtEmp.Rows.Count > 0)
                {
                    cbEcode.DataSource = dtEmp;
                    cbEcode.DisplayMember = "ENAME";
                    cbEcode.ValueMember = "ECODE";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (cbEcode.SelectedIndex > -1)
                {
                    cbEcode.SelectedIndex = 0;
                    strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
                }
                objData = null;
                Cursor.Current = Cursors.Default;
            }

            //objInvoiceDB = new InvoiceDB();
            //objDA = new SQLDB();
            //DataSet dsEmp = null;
            //string sqlText = "";
            //try
            //{
            //    sqlText = "SELECT ECODE,MEMBER_NAME ENAME FROM EORA_MASTER WHERE ECODE LIKE '%"+txtEcodeSearch.Text.ToString()+"%' AND BRANCH_CODE = '"+CommonData.BranchCode+"';";
            //    Cursor.Current = Cursors.WaitCursor;
            //    cbEcode.DataSource = null;
            //    cbEcode.Items.Clear();
            //    //dsEmp = objInvoiceDB.InvLevelEcodeSearch_Get(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth, txtEcodeSearch.Text.ToString());
            //    dsEmp = objDA.ExecuteDataSet(sqlText);
            //    DataTable dtEmp = dsEmp.Tables[0];
            //    if (dtEmp.Rows.Count > 0)
            //    {
            //        cbEcode.DataSource = dtEmp;
            //        cbEcode.DisplayMember = "ENAME";
            //        cbEcode.ValueMember = "ECODE";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    if (cbEcode.SelectedIndex > -1)
            //    {
            //        cbEcode.SelectedIndex = 0;
            //        strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
            //    }
            //    objInvoiceDB = null;
            //    Cursor.Current = Cursors.Default;
            //}
        }
        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text.ToString().Trim().Length > 4)
            {
                if (txtEcodeSearch.Text.ToString().Trim().Length > 2)
                    EcodeSearch();
                else
                    FillEmployeeData();
            }
        }

        private void cbEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!e.KeyChar.Equals((char)8))
            {
                SearchItems(cbEcode, ref e);
            }
            else
                e.Handled = false;
        }

        private void cbEcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEcode.SelectedIndex > -1)
            {
                strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
                GetDNKNumbers();
            }
            else
                chklNumbers.Items.Clear();
        }

        private void SearchItems(ComboBox acComboBox, ref KeyPressEventArgs e)
        {
            int selectionStart = acComboBox.SelectionStart;
            int selectionLength = acComboBox.SelectionLength;
            int selectionEnd = selectionStart + selectionLength;
            int index;
            StringBuilder sb = new StringBuilder();

            sb.Append(acComboBox.Text.Substring(0, selectionStart))
                .Append(e.KeyChar.ToString())
                .Append(acComboBox.Text.Substring(selectionEnd));

            index = acComboBox.FindString(sb.ToString());

            if (index == -1)
                e.Handled = false;
            else
            {
                acComboBox.SelectedIndex = index;
                acComboBox.Select(selectionStart + 1, acComboBox.Text.Length - (selectionStart + 1));
                e.Handled = true;
            }
        }

        public void FillEmployeeData()
        {
            objInvoiceDB = new InvoiceDB();
            DataSet dsEmp = null;
            try
            {
                dsEmp = objInvoiceDB.GetEcodeList(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth);
                DataTable dtEmp = dsEmp.Tables[0];
                DataRow dr = dtEmp.NewRow();
                dr[0] = "0";
                dr[1] = "Select";
                dtEmp.Rows.InsertAt(dr, 0);
                dr = null;
                if (dtEmp.Rows.Count > 0)
                {
                    cbEcode.DataSource = dtEmp;
                    cbEcode.DisplayMember = "ENAME";
                    cbEcode.ValueMember = "ECODE";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (cbEcode.SelectedIndex > -1)
                {
                    cbEcode.SelectedIndex = 0;
                    strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
                }
                objInvoiceDB = null;
                Cursor.Current = Cursors.Default;
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private bool IsNumeric(string strData)
        {
            int i = 0;
            bool blnIsNumber = true;
            strData = strData.Trim();
            if (strData == "")
                blnIsNumber = false;
            for (i = 0; i < strData.Length; i++)
            {
                blnIsNumber = char.IsDigit(strData[i]);
                if (blnIsNumber == false)
                {
                    break;
                }
            }
            return blnIsNumber;
        }

        private void btnCan_Click(object sender, EventArgs e)
        {
            txtEcodeSearch.Text = "";
            cbEcode.SelectedIndex = -1;
            chklNumbers.Items.Clear();
            txtFrNo.Text = "";
            txtToNo.Text = "";
            chkAll.Checked = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckData() == false)
                    return;
                string strIns = "";
                string strExists = "";
                objDA = new SQLDB();
                objInvoiceDB = new InvoiceDB();
                string sLoginBy = objDA.ExecuteDataSet("SELECT UM_ECODE From user_master where um_user_id='" + CommonData.LogUserId + "'", CommandType.Text).Tables[0].Rows[0][0].ToString();

                DataSet dsDNKGroups = objInvoiceDB.GetDNKEcodeInfo(CommonData.CompanyCode, CommonData.BranchCode, lblDocMonth.Text, Convert.ToInt32(strECode));
                if (dsDNKGroups.Tables[0].Rows.Count > 0)
                {
                    string sTmpEocde = "";
                    DataSet dsExist = objDA.ExecuteDataSet("SELECT SDSOI_ORDER_NUMBER, SDSOI_EORA_CODE FROM SALES_DM_SR_ORDSHT_ISSU"+
                                                        " WHERE SDSOI_COMPANY_CODE='" + CommonData.CompanyCode + 
                                                        "' AND SDSOI_BRANCH_CODE='" + CommonData.BranchCode + 
                                                        "' AND SDSOI_DOCUMENT_MONTH='" + CommonData.DocMonth + 
                                                        "' AND cast(SDSOI_ORDER_NUMBER as numeric) between " + Convert.ToInt32(txtFrNo.Text) + 
                                                        " AND " + Convert.ToInt32(txtToNo.Text) + 
                                                        " ORDER BY SDSOI_EORA_CODE");

                    for (int i = Convert.ToInt32(txtFrNo.Text); i <= Convert.ToInt32(txtToNo.Text); i++)
                    {
                        DataView dvFilter = dsExist.Tables[0].DefaultView;
                        dvFilter.RowFilter = "SDSOI_ORDER_NUMBER=" + i.ToString("00000");
                        DataTable dt;
                        dt = dvFilter.ToTable();
                        if (dt.Rows.Count == 0)
                        {
                            strIns += " INSERT INTO SALES_DM_SR_ORDSHT_ISSU(SDSOI_COMPANY_CODE, SDSOI_STATE_CODE, SDSOI_BRANCH_CODE, " +
                                        "SDSOI_FIN_YEAR, SDSOI_DOCUMENT_MONTH, SDSOI_GROUP_NAME, SDSOI_GROUP_LEAD_ECODE, SDSOI_EORA_CODE, " +
                                        "SDSOI_ORDER_NUMBER, SDSOI_ISSUED_BY_ECODE, SDSOI_ISSUED_DATETIME, SDSOI_CREATED_BY, SDSOI_AUTHORIZED_BY, " +
                                        "SDSOI_CREATED_DATE) VALUES ('" + CommonData.CompanyCode + "','" + CommonData.StateCode +
                                        "','" + CommonData.BranchCode + "','" + CommonData.FinancialYear + "','" + CommonData.DocMonth +
                                        "','" + dsDNKGroups.Tables[0].Rows[0]["lgm_group_name"].ToString() + "'," + dsDNKGroups.Tables[0].Rows[0]["lgm_group_ecode"].ToString() +
                                        "," + strECode + ",'" + i.ToString("00000") + "'," + sLoginBy + ",'" + Convert.ToDateTime(meInvoiceDate.Value).ToString("dd/MMM/yyyy") +
                                        "','" + CommonData.LogUserId + "','" + CommonData.LogUserId + "',getdate())";
                        }
                        else
                        {
                            if (sTmpEocde != dt.Rows[0]["SDSOI_EORA_CODE"].ToString())
                                strExists = strExists.TrimEnd(',') + "\n" + dt.Rows[0]["SDSOI_EORA_CODE"].ToString() + " - ";
                            sTmpEocde = dt.Rows[0]["SDSOI_EORA_CODE"].ToString();
                            strExists += dt.Rows[0]["SDSOI_ORDER_NUMBER"].ToString() + ",";
                        }
                    }
                }
                else
                {
                    string sTmpEocde = "";
                    DataSet dsExist = objDA.ExecuteDataSet("SELECT SDSOI_ORDER_NUMBER, SDSOI_EORA_CODE FROM SALES_DM_SR_ORDSHT_ISSU "+
                                                            "WHERE SDSOI_COMPANY_CODE='" + CommonData.CompanyCode + 
                                                            "' AND SDSOI_BRANCH_CODE='" + CommonData.BranchCode + 
                                                            "' AND SDSOI_DOCUMENT_MONTH='" + CommonData.DocMonth + 
                                                            "' AND cast(SDSOI_ORDER_NUMBER as numeric) between " + Convert.ToInt32(txtFrNo.Text) + 
                                                            " AND " + Convert.ToInt32(txtToNo.Text) + " ORDER BY SDSOI_EORA_CODE");

                    for (int i = Convert.ToInt32(txtFrNo.Text); i <= Convert.ToInt32(txtToNo.Text); i++)
                    {
                        DataView dvFilter = dsExist.Tables[0].DefaultView;
                        dvFilter.RowFilter = "SDSOI_ORDER_NUMBER=" + i.ToString("00000");
                        DataTable dt;
                        dt = dvFilter.ToTable();
                        if (dt.Rows.Count == 0)
                        {
                            strIns += " INSERT INTO SALES_DM_SR_ORDSHT_ISSU(SDSOI_COMPANY_CODE, SDSOI_STATE_CODE, " +
                                        "SDSOI_BRANCH_CODE, SDSOI_FIN_YEAR, SDSOI_DOCUMENT_MONTH, " +
                                        "SDSOI_GROUP_NAME, SDSOI_GROUP_LEAD_ECODE, SDSOI_EORA_CODE, " +
                                        "SDSOI_ORDER_NUMBER, SDSOI_ISSUED_BY_ECODE, SDSOI_ISSUED_DATETIME, " +
                                        "SDSOI_CREATED_BY, SDSOI_AUTHORIZED_BY, SDSOI_CREATED_DATE) VALUES('" + CommonData.CompanyCode +
                                        "','" + CommonData.StateCode + "','" + CommonData.BranchCode + "','" + CommonData.FinancialYear +
                                        "','" + CommonData.DocMonth + "','','0'," + strECode + ",'" + i.ToString("00000") +
                                        "'," + sLoginBy + ",'" + Convert.ToDateTime(meInvoiceDate.Value).ToString("dd/MMM/yyyy") +
                                        "','" + CommonData.LogUserId + "','" + CommonData.LogUserId + "',getdate())";
                        }
                        else
                        {
                            if (sTmpEocde != dt.Rows[0]["SDSOI_EORA_CODE"].ToString())
                                strExists = strExists.TrimEnd(',') + "\n" + dt.Rows[0]["SDSOI_EORA_CODE"].ToString() + " - ";
                            sTmpEocde = dt.Rows[0]["SDSOI_EORA_CODE"].ToString();
                            strExists += dt.Rows[0]["SDSOI_ORDER_NUMBER"].ToString() + ",";
                        }
                    }
                }
                if (strIns != "")
                {
                    int iRetVal = objDA.ExecuteSaveData(strIns);
                }
                if (strExists != "")
                    strExists = "Order sheets already issued  Ecode - Order Nos \n" + strExists;
                MessageBox.Show(strExists + "\n Data saved successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data is not saved\n"+ex.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objDA = null;
            }
            finally
            {
                txtFrNo.Text = "";
                txtToNo.Text = "";
                objDA = null;
                GetDNKNumbers();
            }
        }

        private Int32 GetTotalOrders()
        {
            Int32 nFrmNo = 0, nToNo = 0;

            if (txtFrNo.Text.Length != 0)
            {
                nFrmNo = Convert.ToInt32(txtFrNo.Text);
            }

            if (txtToNo.Text.Length != 0)
            {
                nToNo = Convert.ToInt32(txtToNo.Text);
            }

            nTotOrders = nToNo - nFrmNo;

            return nTotOrders;

        }

        private bool CheckData()
        {
            bool blValue = true;
            if (cbEcode.SelectedIndex == -1)
            {
                MessageBox.Show("Select Ecode!");
                blValue = false;
                cbEcode.Focus();
                return blValue;
            }
            else if ((!IsNumeric(txtFrNo.Text.Trim())) || (!IsNumeric(txtToNo.Text.Trim())))
            {
                txtFrNo.Focus();
                MessageBox.Show("Enter valid Numbers !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                blValue = false;
            }
            else if (Convert.ToInt32(txtFrNo.Text) > Convert.ToInt32(txtToNo.Text))
            {
                MessageBox.Show("From No Must be Less Than To No", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                blValue = false;
            }
            else if (strECode == "0")
            {
                MessageBox.Show("Please Select E Code.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                blValue = false;
            }

            GetTotalOrders();
            if (nTotOrders > 100)
            {
                blValue = false;
                MessageBox.Show("OrderSheet Issues Should Be less than 100 Only At a time", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return blValue;
            }

            //else if ((meInvoiceDate.Text.IndexOf(" ") >= 0) || (meInvoiceDate.Text.Length < 10))
            //{
            //    MessageBox.Show("Enter Order Issue date!");
            //    blValue = false;
            //    meInvoiceDate.Focus();
            //    return blValue;
            //}
            //else
            //{
            if (General.IsDateTime(meInvoiceDate.Text.ToString()))
            {
                if (Convert.ToInt32(Convert.ToDateTime(meInvoiceDate.Text).ToString("yyyy")) < 1950)
                {
                    MessageBox.Show("Enter valid  Date !");
                    blValue = false;
                    meInvoiceDate.CausesValidation = true;
                    //meInvoiceDate.Focus();
                    return blValue;
                }
            }
            else
            {
                MessageBox.Show("Enter valid Order Issue Date!");
                meInvoiceDate.CausesValidation = true;
                blValue = false;
                //meInvoiceDate.Focus();
                return blValue;
            }
            //}
            return blValue;
        }

        public void GetDNKNumbers()
        {
            if (cbEcode.SelectedIndex > -1)
            {
                objDA = new SQLDB();
                DataSet ds = objDA.ExecuteDataSet("SELECT SDSOI_ORDER_NUMBER FROM SALES_DM_SR_ORDSHT_ISSU " +
                                                    "WHERE SDSOI_COMPANY_CODE='" + CommonData.CompanyCode + 
                                                    "' AND SDSOI_BRANCH_CODE='" + CommonData.BranchCode + 
                                                    "' AND SDSOI_DOCUMENT_MONTH='" + CommonData.DocMonth + 
                                                    "' AND SDSOI_EORA_CODE=" + strECode);
                chklNumbers.Items.Clear();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    chklNumbers.Items.Add(dr["SDSOI_ORDER_NUMBER"].ToString(), false);                    
                }
                txtInvAmt.Text = ds.Tables[0].Rows.Count.ToString();
                objDA = null;
            }
            else
                chklNumbers.Items.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            chklNumbers.Items.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want delete this records?", "CRM Application", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string sqlQry = "";
                    foreach (object o in chklNumbers.CheckedItems)
                    {
                        sqlQry += " DELETE FROM SALES_DM_SR_ORDSHT_ISSU WHERE SDSOI_COMPANY_CODE='" + CommonData.CompanyCode + "' AND SDSOI_BRANCH_CODE='" + CommonData.BranchCode + "' AND SDSOI_DOCUMENT_MONTH='" + CommonData.DocMonth + "' AND SDSOI_ORDER_NUMBER=" + o.ToString() + " AND SDSOI_EORA_CODE=" + Convert.ToInt32(strECode);
                    }
                    objDA = new SQLDB();
                    int iRetVal = objDA.ExecuteSaveData(sqlQry);
                    MessageBox.Show("Record deleted successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                GetDNKNumbers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Not deleted this record \n" + ex.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                objDA = null;
            }
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            if (strECode != "")
            {
                Random chlRandom = new Random(strECode);
                chlRandom.objfrmOrderSheetIssue = this;
                chlRandom.ShowDialog();
            }
            else
                MessageBox.Show("Please select E Code.", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            CommonData.ViewReport = "SSCRMREP_ORDERISSUE";
            ReportViewer oRptView = new ReportViewer(Convert.ToInt32(strECode));
            oRptView.Show();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked == true)
                for (int i = 0; i < chklNumbers.Items.Count; i++)
                    chklNumbers.SetItemCheckState(i, CheckState.Checked);
            else
                for (int i = 0; i < chklNumbers.Items.Count; i++)
                    chklNumbers.SetItemCheckState(i, CheckState.Unchecked);
        }
    }
}
