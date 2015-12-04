using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSAdmin;
using SSTrans;
using SSCRMDB;
using System.Collections;

namespace SSCRM
{
    public partial class frmOrderSheetReturn : Form
    {
        private InvoiceDB objInvoiceDB = null;
        private SQLDB objDA = null;
        private string strECode = string.Empty;
        public int iNullVal = 0, iTest = 0;
        DataSet ds;
        public frmOrderSheetReturn()
        {
            InitializeComponent();
        }

        private void frmOrderSheetReturn_Load(object sender, EventArgs e)
        {
            lblDocMonth.Text = CommonData.DocMonth.ToString().ToUpper();
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
                if(cbGLWiseReconsilation.Checked == true)
                    dsEmp = objInvoiceDB.InvLevelGLEcodeSearch_Get(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth, txtEcodeSearch.Text.ToString());
                else
                    dsEmp = objInvoiceDB.OrdShtReturnEcodeSearch_Get(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth, txtEcodeSearch.Text.ToString());
                
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
                objInvoiceDB = null;
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool iBool = false;
            string sqlQry = "";
            foreach (Control obj in pnlChecks.Controls)
            {
                CheckBox ch = (CheckBox)obj;
                if (ch.Checked == true)
                {
                    string strReturns = "";
                    if (rbtnReturn.Checked == true)
                        strReturns = "RETURNED";
                    else if (rbtnMissplc.Checked == true)
                        strReturns = "MISS PLACE";
                    else if (rbtnCanceled.Checked == true)
                        strReturns = "CANCEL";
                    else if (rbtnSiezed.Checked == true)
                        strReturns = "FIR";
                    else if (rbtnOthers.Checked == true)
                        strReturns = "OTHERS";
                    //else if (chkCanceled.Checked == true)
                    //    strReturns = "CANCEL";
                    else
                        strReturns = "RETURNED";

                    sqlQry += " UPDATE SALES_DM_SR_ORDSHT_ISSU SET SDSOI_STATUS_FLAG='" + strReturns + "', SDSOI_REMARKS='" + txtRemarks.Text +
                        "' WHERE SDSOI_ORDER_NUMBER=" + obj.Name + " AND SDSOI_COMPANY_CODE='" + CommonData.CompanyCode + //"' AND SDSOI_STATE_CODE='" + CommonData.StateCode +
                        "' AND SDSOI_BRANCH_CODE='" + CommonData.BranchCode + "' AND SDSOI_DOCUMENT_MONTH='" + CommonData.DocMonth + "'";
                    iBool = true;
                }
            }
            if (iBool == true)
            {
                objDA = new SQLDB();
                objDA.ExecuteSaveData(sqlQry);
                objDA = null;
                btnCancel_Click(null, null);
                GetDNKNumbers();
                MessageBox.Show("This record updated successfully", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Please select one order sheet", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            iNullVal = 0;
            chkAll.Checked = false;
            txtRemarks.Text = "";
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text.ToString().Trim().Length > 4)
                EcodeSearch();
            else
                FillEmployeeData();
        }

        public void FillEmployeeData()
        {
            objInvoiceDB = new InvoiceDB();
            DataSet dsEmp = null;
            try
            {
                
                if (cbGLWiseReconsilation.Checked == true)
                    dsEmp = objInvoiceDB.GetGLEcodeList(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth);
                else
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

        private void cbEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!e.KeyChar.Equals((char)8))
            {
                SearchItems(cbEcode, ref e);
            }
            else
                e.Handled = false;
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

        private void cbEcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEcode.SelectedIndex > -1)
            {
                strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
                GetDNKNumbers();
            }
            else
                pnlChecks.Controls.Clear();
        }

        public void GetDNKNumbers()
        {
            if (strECode != "0")
            {
                objDA = new SQLDB();
                string SqlSry;
                if (cbGLWiseReconsilation.Checked == true)
                    SqlSry = " SELECT SDSOI_ORDER_NUMBER, SDSOI_STATUS_FLAG, ISNULL(B.SOH_ORDER_NUMBER,'0') AS SOH_ORDER_NUMBER FROM SALES_DM_SR_ORDSHT_ISSU A " +
                                "left outer join SALES_ORD_HEAD B on A.SDSOI_EORA_CODE=B.SOH_EORA_CODE and B.soh_order_number=A.sdsoi_order_number " +
                                "WHERE SDSOI_COMPANY_CODE='" + CommonData.CompanyCode + "' AND SDSOI_BRANCH_CODE='" + CommonData.BranchCode + "' AND SDSOI_DOCUMENT_MONTH='" + CommonData.DocMonth + "' " +
                                "AND  SDSOI_GROUP_LEAD_ECODE=" + Convert.ToInt32(strECode) + " AND NOT EXISTS (SELECT SIBH_ORDER_NUMBER FROM SALES_INV_BULTIN_head " +
                                "WHERE SIBH_COMPANY_CODE=SDSOI_COMPANY_CODE AND SIBH_BRANCH_CODE=SDSOI_BRANCH_CODE AND SIBH_DOCUMENT_MONTH=SDSOI_DOCUMENT_MONTH " +
                                "AND SIBH_ORDER_NUMBER = SDSOI_ORDER_NUMBER UNION SELECT SOH_ORDER_NUMBER FROM SALES_ORD_HEAD " +
                                "WHERE SOH_COMPANY_CODE=SDSOI_COMPANY_CODE  AND SOH_BRANCH_CODE=SDSOI_BRANCH_CODE AND SOH_DOCUMENT_MONTH=SDSOI_DOCUMENT_MONTH " +
                                "AND SOH_ORDER_NUMBER = SDSOI_ORDER_NUMBER UNION SELECT SIH_ORDER_NUMBER FROM SALES_INV_HEAD " +
                                "WHERE SIH_COMPANY_CODE=SDSOI_COMPANY_CODE AND SIH_BRANCH_CODE=SDSOI_BRANCH_CODE AND SIH_DOCUMENT_MONTH=SDSOI_DOCUMENT_MONTH " +
                                "AND SIH_ORDER_NUMBER = SDSOI_ORDER_NUMBER) ORDER BY CAST(SDSOI_ORDER_NUMBER AS NUMERIC)";
                else
                    SqlSry = " SELECT SDSOI_ORDER_NUMBER, SDSOI_STATUS_FLAG, ISNULL(B.SOH_ORDER_NUMBER,'0') AS SOH_ORDER_NUMBER FROM SALES_DM_SR_ORDSHT_ISSU A " +
                                "left outer join SALES_ORD_HEAD B on A.SDSOI_EORA_CODE=B.SOH_EORA_CODE and B.soh_order_number=A.sdsoi_order_number " +
                                "WHERE SDSOI_COMPANY_CODE='" + CommonData.CompanyCode + "' AND SDSOI_BRANCH_CODE='" + CommonData.BranchCode + "' AND SDSOI_DOCUMENT_MONTH='" + CommonData.DocMonth + "' " +
                                "AND SDSOI_EORA_CODE=" + Convert.ToInt32(strECode) + " AND NOT EXISTS (SELECT SIBH_ORDER_NUMBER FROM SALES_INV_BULTIN_head " +
                                "WHERE SIBH_COMPANY_CODE=SDSOI_COMPANY_CODE AND SIBH_BRANCH_CODE=SDSOI_BRANCH_CODE AND SIBH_DOCUMENT_MONTH=SDSOI_DOCUMENT_MONTH " +
                                "AND SIBH_ORDER_NUMBER = SDSOI_ORDER_NUMBER UNION SELECT SOH_ORDER_NUMBER FROM SALES_ORD_HEAD " +
                                "WHERE SOH_COMPANY_CODE=SDSOI_COMPANY_CODE  AND SOH_BRANCH_CODE=SDSOI_BRANCH_CODE AND SOH_DOCUMENT_MONTH=SDSOI_DOCUMENT_MONTH " +
                                "AND SOH_ORDER_NUMBER = SDSOI_ORDER_NUMBER UNION SELECT SIH_ORDER_NUMBER FROM SALES_INV_HEAD " +
                                "WHERE SIH_COMPANY_CODE=SDSOI_COMPANY_CODE AND SIH_BRANCH_CODE=SDSOI_BRANCH_CODE AND SIH_DOCUMENT_MONTH=SDSOI_DOCUMENT_MONTH " +
                                "AND SIH_ORDER_NUMBER = SDSOI_ORDER_NUMBER) ORDER BY CAST(SDSOI_ORDER_NUMBER AS NUMERIC)";
                try
                {
                    ds = objDA.ExecuteDataSet(SqlSry);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                pnlChecks.Controls.Clear();
                int iMps = 0, iCan = 0, iRecei = 0, iFIR = 0, iOthers = 0;
                CheckBox[] chkBox = new CheckBox[ds.Tables[0].Rows.Count + 1];
                int height = 2, padding = 4, ival = 0;
                pnlChecks.Controls.Clear();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    iTest = 1;
                    chkBox[i] = new CheckBox();
                    chkBox[i].Text = ds.Tables[0].Rows[i]["SDSOI_ORDER_NUMBER"].ToString();
                    if (ds.Tables[0].Rows[i]["SDSOI_ORDER_NUMBER"].ToString() != "0")
                    {
                        chkBox[i].BackColor = System.Drawing.Color.MediumTurquoise;
                        if (ds.Tables[0].Rows[i]["SDSOI_STATUS_FLAG"].ToString().Trim() == "CANCEL")
                        {
                            chkBox[i].BackColor = lblCacelColor.BackColor;
                            //System.Drawing.Color.FromArgb(248, 199, 107);
                            iCan++;
                        }

                        else if (ds.Tables[0].Rows[i]["SDSOI_STATUS_FLAG"].ToString() == "MISS PLACE")
                        {
                            chkBox[i].BackColor = lblMisplaceColor.BackColor;
                            //System.Drawing.Color.FromArgb(248, 107, 107);
                            iMps++;
                        }
                        else if (ds.Tables[0].Rows[i]["SDSOI_STATUS_FLAG"].ToString().Trim() == "RETURNED")
                        {
                            chkBox[i].BackColor = lblReturnedColor.BackColor;
                            iRecei++;
                            //System.Drawing.Color.FromArgb(210, 240, 121);
                        }
                        else if (ds.Tables[0].Rows[i]["SDSOI_STATUS_FLAG"].ToString().Trim() == "FIR")
                        {
                            chkBox[i].BackColor = lblFIR.BackColor;
                            iFIR++;
                            //System.Drawing.Color.FromArgb(210, 240, 121);
                        }
                        else if (ds.Tables[0].Rows[i]["SDSOI_STATUS_FLAG"].ToString().Trim() == "OTHERS")
                        {
                            chkBox[i].BackColor = lblOthers.BackColor;
                            iOthers++;
                            //System.Drawing.Color.FromArgb(210, 240, 121);
                        }
                        else
                        {
                            chkBox[i].Text = ds.Tables[0].Rows[i]["SDSOI_ORDER_NUMBER"].ToString();
                            chkBox[i].BackColor = System.Drawing.Color.Transparent;
                            chkBox[i].Checked = true;
                            iNullVal++;
                        }
                    }
                    if (ival == 20)
                    { padding += 100; height = 1; ival = 0; }
                    chkBox[i].Name = ds.Tables[0].Rows[i]["SDSOI_ORDER_NUMBER"].ToString();
                    chkBox[i].TabIndex = ival;
                    chkBox[i].AutoSize = true;
                    chkBox[i].Location = new System.Drawing.Point(padding + 20, height);
                    chkBox[i].CheckedChanged += new System.EventHandler(this.ChkCtrl_CheckedChanged);
                    pnlChecks.Controls.Add(chkBox[i]);
                    height += 18;
                    ival++;
                }
                if (cbGLWiseReconsilation.Checked == true)
                    SqlSry = " SELECT COUNT(SDSOI_ORDER_NUMBER) AS ISSUED_SHEETS FROM SALES_DM_SR_ORDSHT_ISSU A WHERE SDSOI_COMPANY_CODE='" + CommonData.CompanyCode + "' AND SDSOI_BRANCH_CODE='" + CommonData.BranchCode + "' AND SDSOI_DOCUMENT_MONTH='" + CommonData.DocMonth + "' AND  SDSOI_GROUP_LEAD_ECODE='" + Convert.ToInt32(strECode) + "'";
                else
                    SqlSry = " SELECT COUNT(SDSOI_ORDER_NUMBER) AS ISSUED_SHEETS FROM SALES_DM_SR_ORDSHT_ISSU A WHERE SDSOI_COMPANY_CODE='" + CommonData.CompanyCode + "' AND SDSOI_BRANCH_CODE='" + CommonData.BranchCode + "' AND SDSOI_DOCUMENT_MONTH='" + CommonData.DocMonth + "' AND  SDSOI_EORA_CODE='" + Convert.ToInt32(strECode) + "'";
                Int32 totalIssued = Convert.ToInt32(objDA.ExecuteDataSet(SqlSry).Tables[0].Rows[0][0].ToString());
                if (cbGLWiseReconsilation.Checked == true)
                    SqlSry = " SELECT COUNT(DISTINCT(SALED)) AS SALED FROM(SELECT SDSOI_ORDER_NUMBER AS SALED " +
                        "FROM SALES_DM_SR_ORDSHT_ISSU SDSOI INNER JOIN SALES_INV_BULTIN_HEAD SIBH ON SIBH_COMPANY_CODE = SDSOI_COMPANY_CODE " +
                        "AND SIBH_BRANCH_CODE = SDSOI_BRANCH_CODE AND SIBH_DOCUMENT_MONTH = SDSOI_DOCUMENT_MONTH AND SIBH_ORDER_NUMBER = SDSOI_ORDER_NUMBER " +
                        "WHERE SDSOI_COMPANY_CODE = '" + CommonData.CompanyCode + "' AND SDSOI_BRANCH_CODE = '" + CommonData.BranchCode + "' AND SDSOI_DOCUMENT_MONTH = '" + CommonData.DocMonth + "' AND SDSOI_GROUP_LEAD_ECODE = '" + Convert.ToInt32(strECode) + "' " +
                        "UNION SELECT SDSOI_ORDER_NUMBER AS SALED FROM SALES_DM_SR_ORDSHT_ISSU SDSOI INNER JOIN SALES_INV_HEAD SIH "+
                        "ON SIH_COMPANY_CODE = SDSOI_COMPANY_CODE AND SIH_BRANCH_CODE = SDSOI_BRANCH_CODE AND SIH_DOCUMENT_MONTH = SDSOI_DOCUMENT_MONTH AND SIH_ORDER_NUMBER = SDSOI_ORDER_NUMBER "+
                        "WHERE SDSOI_COMPANY_CODE = '" + CommonData.CompanyCode + "' AND SDSOI_BRANCH_CODE = '" + CommonData.BranchCode + "' AND SDSOI_DOCUMENT_MONTH = '" + CommonData.DocMonth + "' AND SDSOI_GROUP_LEAD_ECODE = '" + Convert.ToInt32(strECode) + "' " +
                        "UNION SELECT SDSOI_ORDER_NUMBER AS SALED FROM SALES_DM_SR_ORDSHT_ISSU SDSOI INNER JOIN SALES_ORD_HEAD SOH ON SOH_BRANCH_CODE = SDSOI_BRANCH_CODE " +
                        "AND SOH_COMPANY_CODE = SDSOI_COMPANY_CODE AND SOH_DOCUMENT_MONTH = SDSOI_DOCUMENT_MONTH AND SOH_ORDER_NUMBER = SDSOI_ORDER_NUMBER " +
                        "WHERE SDSOI_COMPANY_CODE = '" + CommonData.CompanyCode + "' AND SDSOI_BRANCH_CODE = '" + CommonData.BranchCode + "' AND SDSOI_DOCUMENT_MONTH = '" + CommonData.DocMonth + "' AND SDSOI_GROUP_LEAD_ECODE = '" + Convert.ToInt32(strECode) + "') AS TABLE1";
                else
                    SqlSry = "SELECT COUNT(DISTINCT(SALED)) AS SALED FROM(SELECT SDSOI_ORDER_NUMBER AS SALED " +
                        "FROM SALES_DM_SR_ORDSHT_ISSU SDSOI INNER JOIN SALES_INV_BULTIN_HEAD SIBH ON SIBH_COMPANY_CODE = SDSOI_COMPANY_CODE " +
                        "AND SIBH_BRANCH_CODE = SDSOI_BRANCH_CODE AND SIBH_DOCUMENT_MONTH = SDSOI_DOCUMENT_MONTH AND SIBH_ORDER_NUMBER = SDSOI_ORDER_NUMBER " +
                        "WHERE SDSOI_COMPANY_CODE = '" + CommonData.CompanyCode + "' AND SDSOI_BRANCH_CODE = '" + CommonData.BranchCode + "' AND SDSOI_DOCUMENT_MONTH = '" + CommonData.DocMonth + "' AND SDSOI_EORA_CODE = '" + Convert.ToInt32(strECode) + "' " +
                                                "UNION SELECT SDSOI_ORDER_NUMBER AS SALED FROM SALES_DM_SR_ORDSHT_ISSU SDSOI INNER JOIN SALES_INV_HEAD SIH " +
                        "ON SIH_COMPANY_CODE = SDSOI_COMPANY_CODE AND SIH_BRANCH_CODE = SDSOI_BRANCH_CODE AND SIH_DOCUMENT_MONTH = SDSOI_DOCUMENT_MONTH AND SIH_ORDER_NUMBER = SDSOI_ORDER_NUMBER " +
                        "WHERE SDSOI_COMPANY_CODE = '" + CommonData.CompanyCode + "' AND SDSOI_BRANCH_CODE = '" + CommonData.BranchCode + "' AND SDSOI_DOCUMENT_MONTH = '" + CommonData.DocMonth + "' AND SDSOI_EORA_CODE = '" + Convert.ToInt32(strECode) + "' " +
                        "UNION SELECT SDSOI_ORDER_NUMBER AS SALED FROM SALES_DM_SR_ORDSHT_ISSU SDSOI INNER JOIN SALES_ORD_HEAD SOH ON SOH_BRANCH_CODE = SDSOI_BRANCH_CODE " +
                        "AND SOH_COMPANY_CODE = SDSOI_COMPANY_CODE AND SOH_DOCUMENT_MONTH = SDSOI_DOCUMENT_MONTH AND SOH_ORDER_NUMBER = SDSOI_ORDER_NUMBER " +
                        "WHERE SDSOI_COMPANY_CODE = '" + CommonData.CompanyCode + "' AND SDSOI_BRANCH_CODE = '" + CommonData.BranchCode + "' AND SDSOI_DOCUMENT_MONTH = '" + CommonData.DocMonth + "' AND SDSOI_EORA_CODE = '" + Convert.ToInt32(strECode) + "') AS TABLE1";
                Int32 totalSaled = Convert.ToInt32(objDA.ExecuteDataSet(SqlSry).Tables[0].Rows[0][0].ToString());
                lblIssued.Text = totalIssued.ToString();
                lblCanceled.Text = iCan.ToString();
                lblMilsplc.Text = iMps.ToString();
                txtSalesEntered.Text = totalSaled.ToString();
                lblFIRSheets.Text = iFIR.ToString();
                lblOthersheets.Text = iOthers.ToString();
                //iRecei = totalIssued - (iCan + iMps);
                lblReceived.Text = iRecei.ToString();
                txtEmptySheets.Text = (totalIssued - (totalSaled + iCan + iMps + iRecei + iFIR + iOthers)).ToString();
                if ((totalIssued - (totalSaled + iCan + iMps + iRecei + iFIR + iOthers)) == 0)
                    iNullVal = 0;
                else
                    iNullVal = 1;
                objDA = null;
            }
            else
            {
                pnlChecks.Controls.Clear();
                lblIssued.Text = "";
                lblCanceled.Text = "";
                lblMilsplc.Text = "";
                lblReceived.Text = "";
                lblFIRSheets.Text = "";
                lblOthers.Text = "";
                txtEmptySheets.Text = "";
                txtSalesEntered.Text = "";
            }

        }

        private void ChkCtrl_CheckedChanged(object sender, EventArgs e)
        {
            //if (iTest == 1)
            //{
            //    objDA = new SQLDB();
            //    bool iCan = false, iAdv = false;
            //    string svalues = "";
            //    foreach (Control obj in pnlChecks.Controls)
            //    {
            //        CheckBox ch = (CheckBox)obj;
            //        if (ch.Checked == true)
            //        {
            //            DataView dvData = ds.Tables[0].DefaultView;
            //            dvData.RowFilter = " SDSOI_ORDER_NUMBER=" + obj.Name;
            //            DataTable dt;
            //            dt = dvData.ToTable();
            //            if (dt.Rows[0][2].ToString() == "0")
            //            {
            //                svalues += ",";
            //                iCan = true;
            //            }
            //            else
            //                iAdv = true;
            //        }
            //    }
            //    if (iCan == true)
            //        chkCanceled.Enabled = false;
            //    else
            //        chkCanceled.Enabled = true;
            //    if (iAdv == true)
            //    {
            //        rbtnMissplc.Enabled = false;
            //        rbtnReturn.Enabled = false;
            //    }
            //    else
            //    {
            //        rbtnMissplc.Enabled = true;
            //        rbtnReturn.Enabled = true;
            //    }
            //    if ((iCan == true) && (iAdv == true))
            //        btnSave.Enabled = false;
            //    else
            //        btnSave.Enabled = true;
            //}
            //iTest = 1;
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked == true)
            {
                foreach (Control obj in pnlChecks.Controls)
                {
                    iTest = 0;
                    CheckBox ch = (CheckBox)obj;
                    ch.Checked = true;
                }
                
            }
            else
            {
                foreach (Control obj in pnlChecks.Controls)
                {
                    iTest = 0;
                    CheckBox ch = (CheckBox)obj;
                    ch.Checked = false;
                }
            }
        }

        private void rbtnReturn_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnReturn.Checked == true)
            {
                rbtnReturn.Checked = true;
                rbtnMissplc.Checked = false;
                rbtnCanceled.Checked = false;
                rbtnSiezed.Checked = false;
                rbtnOthers.Checked = false;
            }
        }

        private void rbtnMissplc_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnMissplc.Checked == true)
            {
                rbtnReturn.Checked = false;
                rbtnMissplc.Checked = true;
                rbtnCanceled.Checked = false;
                rbtnSiezed.Checked = false;
                rbtnOthers.Checked = false;
            }
        }

        private void chkCanceled_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnCanceled.Checked == true)
            {
                rbtnReturn.Checked = false;
                rbtnMissplc.Checked = false;
                rbtnCanceled.Checked = true;
                rbtnOthers.Checked = false;
                rbtnSiezed.Checked = false;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (iNullVal != 0)
            {
                MessageBox.Show("Please Update Remaining "+txtEmptySheets.Text+" Order Sheets Status", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cbGLWiseReconsilation.Checked == true)
            {
                CommonData.ViewReport = "SSCRM_REP_SR_OrderForm";
                ReportViewer oChldReportViewer = new ReportViewer(Convert.ToInt32(strECode), "GL WISE");
                oChldReportViewer.Show();
            }
            else
            {
                CommonData.ViewReport = "SSCRM_REP_SR_OrderForm";
                ReportViewer oChldReportViewer = new ReportViewer(Convert.ToInt32(strECode), "SR WISE");
                oChldReportViewer.Show();
            }
        }

        private void rbtnCanceled_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnCanceled.Checked == true)
            {
                rbtnReturn.Checked = false;
                rbtnMissplc.Checked = false;
                rbtnCanceled.Checked = true;
                rbtnSiezed.Checked = false;
                rbtnOthers.Checked = false;
            }
        }

        private void cbGLWiseReconsilation_CheckedChanged(object sender, EventArgs e)
        {
            FillEmployeeData();
        }

        private void rbtnSiezed_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnSiezed.Checked == true)
            {
                rbtnReturn.Checked = false;
                rbtnMissplc.Checked = false;
                rbtnCanceled.Checked = false;
                rbtnSiezed.Checked = true;
                rbtnOthers.Checked = false;
            }
        }

        private void rbtnOthers_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnOthers.Checked == true)
            {
                rbtnReturn.Checked = false;
                rbtnMissplc.Checked = false;
                rbtnCanceled.Checked = false;
                rbtnSiezed.Checked = false;
                rbtnOthers.Checked = true;
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            //SearchEcode(txtSearch.Text.ToString(), pnlChecks.Controls);
        }



    }
}
