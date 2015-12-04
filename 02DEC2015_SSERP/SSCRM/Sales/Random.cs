using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSTrans;
using SSCRMDB;

namespace SSCRM
{
    public partial class Random : Form
    {
        public frmOrderSheetIssue objfrmOrderSheetIssue;
        InvoiceDB objInvoiceDB;
        SQLDB objDA;
        string strECode = "";
        public Random()
        {
            InitializeComponent();
        }
        public Random(string sECode)
        {
            InitializeComponent();
            strECode = sECode;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtOrder_num.Text == "")
            {
                MessageBox.Show("Enter Order Numbers.","SSCRM",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            try
            {                
                string strIns = "";
                objDA = new SQLDB();
                objInvoiceDB = new InvoiceDB();
                string sLoginBy = objDA.ExecuteDataSet("SELECT UM_ECODE From user_master where um_user_id='" + CommonData.LogUserId + "'", CommandType.Text).Tables[0].Rows[0][0].ToString();
                string[] sOrderVal = txtOrder_num.Text.ToString().Replace("\r", "").Split('\n');
                DataSet dsDNKGroups = objInvoiceDB.GetDNKEcodeInfo(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth, Convert.ToInt32(strECode));
                if (dsDNKGroups != null)
                {
                    for (int i = 0; i < sOrderVal.Length; i++)
                    {
                        try
                        {
                            Convert.ToInt32(sOrderVal[i]).ToString("00000");
                        }
                        catch { sOrderVal[i] = "00000"; }

                        int Cnt = Convert.ToInt32(objDA.ExecuteDataSet("select Count(*) from SALES_DM_SR_ORDSHT_ISSU WHERE SDSOI_COMPANY_CODE='" + CommonData.CompanyCode + "' AND SDSOI_BRANCH_CODE='" + CommonData.BranchCode + "' AND SDSOI_DOCUMENT_MONTH='" + CommonData.DocMonth + "' AND SDSOI_ORDER_NUMBER='" + Convert.ToInt32(sOrderVal[i]).ToString("00000") + "'").Tables[0].Rows[0][0]);
                        if (Cnt == 0 && Convert.ToInt32(sOrderVal[i]) != 0)
                        {
                            strIns += " INSERT INTO SALES_DM_SR_ORDSHT_ISSU (SDSOI_COMPANY_CODE,SDSOI_STATE_CODE,SDSOI_BRANCH_CODE,SDSOI_FIN_YEAR,SDSOI_DOCUMENT_MONTH," +
                            "SDSOI_GROUP_NAME,SDSOI_GROUP_LEAD_ECODE,SDSOI_EORA_CODE,SDSOI_ORDER_NUMBER,SDSOI_ISSUED_BY_ECODE,SDSOI_ISSUED_DATETIME," +
                            "SDSOI_CREATED_BY,SDSOI_AUTHORIZED_BY,SDSOI_CREATED_DATE) VALUES ('" + CommonData.CompanyCode + "','" + CommonData.StateCode + 
                            "','" + CommonData.BranchCode + "','" + CommonData.FinancialYear + "','" + CommonData.DocMonth +
                            "','" + dsDNKGroups.Tables[0].Rows[0]["lgm_group_name"].ToString() + "'," + dsDNKGroups.Tables[0].Rows[0]["lgm_group_ecode"].ToString() + 
                            "," + strECode + ",'" + Convert.ToInt32(sOrderVal[i]).ToString("00000") + "'," + sLoginBy + ",'" + CommonData.CurrentDate +
                            "','" + CommonData.LogUserId + "','" + CommonData.LogUserId + "','" + CommonData.CurrentDate + "')";
                        }
                    }
                }
                int iRetVal = objDA.ExecuteSaveData(strIns);
                MessageBox.Show("Data saved successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data is not saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objDA = null;
            }
            finally
            {
                objDA = null;
                ((frmOrderSheetIssue)objfrmOrderSheetIssue).GetDNKNumbers();
                this.Close();
            }
        }

        private void txtOrder_num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar != '\b') && (e.KeyChar != '\r'))
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }
    }
}
