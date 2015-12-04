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

namespace SSCRM
{
    public partial class SalesReceipt : Form
    {
        InvoiceDB objData = null;
        SQLDB objSQLDB = null;
        string strECode = null;
        bool flagUpdate = false;
        private DateTime dtCreated = DateTime.Now;
        int iID = 0;
        public SalesReceipt()
        {
            InitializeComponent();
        }

        private void Receipt_Load(object sender, EventArgs e)
        {
            dtCreated = Convert.ToDateTime(CommonData.CurrentDate);
            txtDocMonth.Text = CommonData.DocMonth;
            FillReceiptType();
            FillEmployeeData();
            FillDatatoGrid();
            dtpReceiptDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
        }

        private void FillReceiptType()
        {
            
            try
            {
               DataTable dt= dtDCTranType();
                if (dt.Rows.Count > 0)
                {
                    
                    cbRefType.DataSource = dt;
                    cbRefType.DisplayMember = "type";
                    cbRefType.ValueMember = "name";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {

                Cursor.Current = Cursors.Default;
            }

        }

        private void FillEmployeeData()
        {
            objData = new InvoiceDB();
            DataSet dsEmp = null;
            try
            {
                dsEmp = objData.GetEcodeList(CommonData.CompanyCode, CommonData.BranchCode, txtDocMonth.Text);
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
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text.ToString().Trim().Length > 0)
                EcodeSearch();
            else
                FillEmployeeData();
            FillDatatoGrid();
        }

        private void EcodeSearch()
        {
            objData = new InvoiceDB();
            DataSet dsEmp = null;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                cbEcode.DataSource = null;
                cbEcode.Items.Clear();
                dsEmp = objData.InvLevelEcodeSearch_Get(CommonData.CompanyCode, CommonData.BranchCode, txtDocMonth.Text, txtEcodeSearch.Text.ToString());
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
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetterOrDigit(e.KeyChar) == false)
                e.Handled = true;

            if (e.KeyChar == 8)
                e.Handled = false;

            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dtCreated = Convert.ToDateTime(CommonData.CurrentDate);
            txtReceiptNo.Text = "";
            txtEcodeSearch.Text = "";
            txtAmount.Text = "";
            txtRemarks.Text = "";
            gvReceiptList.Rows.Clear();
            txtRefNo.Text = "";
            flagUpdate = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(CheckData())
            {
                if (SaveData() > 0)
                {
                    MessageBox.Show("Data Saved Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillDatatoGrid();
                    dtCreated = Convert.ToDateTime(CommonData.CurrentDate);
                    flagUpdate = false;
                    txtAmount.Text = "";
                    txtReceiptNo.Text = "";
                    txtRemarks.Text = "";
                    txtRefNo.Text = "";
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private int SaveData()
        {
            int iRes = 0;
            string strCmd = "";

            objSQLDB = new SQLDB();
            try
            {
                if (flagUpdate == false)
                {
                    strCmd = "INSERT INTO SALES_RECIEPTS(SSR_COMPANY_CODE," +
                                                        "SSR_BRANCH_CODE" +
                                                        ",SSR_FIN_YEAR" +
                                                        ",SSR_DOC_MONTH" +
                                                        ",SSR_RECIEPT_NO" +
                                                        ",SSR_RECIEPT_DATE" +
                                                        ",SSR_EORA_CODE" +
                                                        ",SSR_RECIEVED_AMT" +
                                                        ",SSR_CREATED_BY" +
                                                        ",SSR_CREATED_DATE"+
                                                        ",SSR_REMARKS,SSR_RECEIPT_TYPE,SSR_DDORCHEQUE_NO) VALUES('" + CommonData.CompanyCode +
                                                        "','" + CommonData.BranchCode +
                                                        "','" + CommonData.FinancialYear +
                                                        "','" + txtDocMonth.Text +
                                                        "','" + txtReceiptNo.Text +
                                                        "','" + dtpReceiptDate.Value.ToString("dd/MMM/yyyy") +
                                                        "','" + cbEcode.SelectedValue +
                                                        "','" + txtAmount.Text +
                                                        "','" + CommonData.LogUserId +
                                                        "',GETDATE(),'"+txtRemarks.Text.Replace("'","")+"','"+cbRefType.SelectedValue.ToString()+"','"+txtRefNo.Text+"')";
                }
                else
                {
                    strCmd = "UPDATE SALES_RECIEPTS SET SSR_RECIEPT_DATE='" + dtpReceiptDate.Value.ToString("dd/MMM/yyyy") +
                                                        "',SSR_EORA_CODE='" + cbEcode.SelectedValue.ToString() +
                                                        "',SSR_RECIEVED_AMT='" + txtAmount.Text +
                                                        "',SSR_MODIFIED_BY='" + CommonData.LogUserId +
                                                        "',SSR_MODIFIED_DATE=getdate()" +
                                                        ",SSR_REMARKS='" + txtRemarks.Text.Replace("'", "") +
                                                        "',SSR_RECEIPT_TYPE='" + cbRefType.SelectedValue.ToString() +
                                                        "',SSR_DDORCHEQUE_NO='" + txtRefNo.Text+
                                                        "' where SSR_BRANCH_CODE='" + CommonData.BranchCode +
                                                        "' and SSR_DOC_MONTH='" + CommonData.DocMonth +
                                                        "' and SSR_ID=" + iID;
                }
                iRes = objSQLDB.ExecuteSaveData(strCmd);
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
        private void FillDatatoGrid( )
        {
            objData = new InvoiceDB();
            DataTable dt=null;
            if (cbEcode.SelectedIndex > -1 && txtDocMonth.Text.Length > 0)
            {
                dt = objData.GetSalesReceiptDataOfEmp(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth,((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString());
            }


            gvReceiptList.Rows.Clear();
            txtRevenue.Text = "";
            txtTotReciepts.Text = "0.00";
            if(dt != null)
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                gvReceiptList.Rows.Add();
                gvReceiptList.Rows[i].Cells["SlNo"].Value = i + 1;
                gvReceiptList.Rows[i].Cells["Ecode"].Value = dt.Rows[i]["EmpEcode"].ToString();
                gvReceiptList.Rows[i].Cells["Names"].Value = dt.Rows[i]["EmpName"].ToString();
                gvReceiptList.Rows[i].Cells["Type"].Value = dt.Rows[i]["Type"].ToString();
                gvReceiptList.Rows[i].Cells["ReceiptNo"].Value = dt.Rows[i]["ReceiptNo"].ToString();
                gvReceiptList.Rows[i].Cells["Date"].Value = Convert.ToDateTime( dt.Rows[i]["ReceiptDate"].ToString()).ToShortDateString();
                gvReceiptList.Rows[i].Cells["Amount"].Value = dt.Rows[i]["Amount"].ToString();
                gvReceiptList.Rows[i].Cells["Remarks"].Value = dt.Rows[i]["Remarks"].ToString();
                gvReceiptList.Rows[i].Cells["id"].Value = dt.Rows[i]["SSR_ID"].ToString();
                gvReceiptList.Rows[i].Cells["RefNo"].Value = dt.Rows[i]["RefNo"].ToString();
                gvReceiptList.Rows[i].Cells["CreatedDate"].Value = dt.Rows[i]["CreatedDate"].ToString();
                txtTotReciepts.Text = dt.Rows[i]["TotRecieved"].ToString();
                txtRevenue.Text = dt.Rows[i]["TotalRev"].ToString();
            }
        }
        private bool CheckData()
        {
            bool flag=true;
            if(txtDocMonth.Text.Length==0)
            {
                MessageBox.Show("Enter Document Month", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                flag = false;
                return flag;
            }
            if (txtReceiptNo.Text.Length == 0)
            {
                MessageBox.Show("Enter Receipt NO", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                flag = false;
                return flag;
            }

            Security objSecur = new Security();
            if (objSecur.CanModifyDataUserAsPerBackDays(Convert.ToDateTime(dtCreated)) == false)
            {
                MessageBox.Show("You cannot manipulate backdays data!\n If you want to modify, Contact to IT-Department", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                flag = false;
                return flag;
            }
            objSecur = null;

            if(cbEcode.Items.Count==0)
            {
                MessageBox.Show("Enter Valid Ecode", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                flag = false;
                return flag;
            }
            if(txtAmount.Text.Length==0)
            {
                MessageBox.Show("Enter Amount", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                flag = false;
                return flag;
            }
            return flag;
        }

        private void cbEcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDatatoGrid();

        }

        private void gvReceiptList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (gvReceiptList.Rows[e.RowIndex].Cells["Edit"].Value.ToString().Trim() != "")
                {
                    if (Convert.ToBoolean(gvReceiptList.Rows[e.RowIndex].Cells["Edit"].Selected) == true)
                    {
                        txtEcodeSearch.Text = gvReceiptList.Rows[e.RowIndex].Cells["Ecode"].Value.ToString();
                        txtReceiptNo.Text = gvReceiptList.Rows[e.RowIndex].Cells["ReceiptNo"].Value.ToString();
                        dtpReceiptDate.Value = Convert.ToDateTime( Convert.ToDateTime(gvReceiptList.Rows[e.RowIndex].Cells["Date"].Value.ToString()).ToString("dd/MM/yyyy"));
                        txtAmount.Text = gvReceiptList.Rows[e.RowIndex].Cells["Amount"].Value.ToString();
                        txtRemarks.Text = gvReceiptList.Rows[e.RowIndex].Cells["Remarks"].Value.ToString();
                        iID = Convert.ToInt32(gvReceiptList.Rows[e.RowIndex].Cells["id"].Value.ToString());
                        cbRefType.SelectedValue =gvReceiptList.Rows[e.RowIndex].Cells["Type"].Value.ToString();
                        txtRefNo.Text= gvReceiptList.Rows[e.RowIndex].Cells["RefNo"].Value.ToString();
                        dtCreated = Convert.ToDateTime(gvReceiptList.Rows[e.RowIndex].Cells["CreatedDate"].Value.ToString());
                        flagUpdate = true;
                        
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(flagUpdate==true)
            {
             DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
             if (dlgResult == DialogResult.Yes)
             {
                 try
                 {
                     string strCmd = "delete from SALES_RECIEPTS where SSR_COMPANY_CODE='" + CommonData.CompanyCode + 
                                                                "' and SSR_BRANCH_CODE='" + CommonData.BranchCode + 
                                                                "' and SSR_DOC_MONTH='"+CommonData.DocMonth+
                                                                "' and SSR_FIN_YEAR='"+CommonData.FinancialYear+
                                                                //"' and SSR_DOC_MONTH='"+CommonData.DocMonth+
                                                                "' and SSR_ID="+iID;

                     objSQLDB = new SQLDB();
                      int iRes = objSQLDB.ExecuteSaveData(strCmd);
                      if (iRes > 0)
                      {
                          MessageBox.Show("Data Deleted Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                          dtCreated = Convert.ToDateTime(CommonData.CurrentDate);
                          flagUpdate = false;
                          txtAmount.Text = "";
                          txtReceiptNo.Text = "";
                          txtRemarks.Text = "";
                          FillDatatoGrid();
                      }
                 }
                 catch(Exception ex)
                 {
                     MessageBox.Show(ex.ToString());
                 }
             }
            }
        }
        public DataTable dtDCTranType()
        {
            DataTable table = new DataTable();
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("name", typeof(string));

            table.Rows.Add("CASH", "CASH");
            table.Rows.Add("BANK", "BANK");
            table.Rows.Add("JV", "JV");
            table.Rows.Add("DD", "DD");
            table.Rows.Add("CHEQUE", "CHEQUE");
            table.Rows.Add("BILLS/VOUCHERS", "BILLS/VOUCHERS");


            return table;
        }
    }
}
