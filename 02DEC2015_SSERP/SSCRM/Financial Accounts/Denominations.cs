using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using System.Data.SqlClient;

namespace SSCRM
{
    public partial class Denominations : Form
    {
        public Denominations()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            gvBillDetails.Rows.Clear();
            FillDenominationDefDetl();
            for (int i = 0; i < gvBillDetails.Rows.Count;i++ )
            {
                gvBillDetails.Rows[i].Cells["Count"].Value = "0";
                gvBillDetails.Rows[i].Cells["AmtReceived"].Value = "0";
            }
            btnSave.Enabled = false;
            CalcTotal();
        }

        private void CalcTotal()
        {
            double total = 0;
            for (int i = 0; i < gvBillDetails.Rows.Count; i++)
            {
                if (Convert.ToDouble(gvBillDetails.Rows[i].Cells["AmtReceived"].Value.ToString()) > 0 &&
                    (Convert.ToString(gvBillDetails.Rows[i].Cells["AmtReceived"].Value) != ""))
                    total += Convert.ToDouble(gvBillDetails.Rows[i].Cells["AmtReceived"].Value);

            }
            txtTotalAmt.Text = total.ToString();
        }

        private void Denominations_Load(object sender, EventArgs e)
        {
            txtComanpanyName.Text = CommonData.CompanyName;
            txtBranch.Text = CommonData.BranchName;
            txtFinYear.Text = CommonData.FinancialYear;
            dtpInvoiceDate.Value = DateTime.Today;
            txtCash.Text = FillDayClosingCash();
            FillDenominationDefDetl();
            CalcTotal();
        }

        private string FillDayClosingCash()
        {
            String strClosCash="";
            SQLDB objDB = new SQLDB();
            SqlParameter[] param = new SqlParameter[8];
            DataSet ds = new DataSet();

            try
            {
                param[0] = objDB.CreateParameter("@xcmp_cd", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objDB.CreateParameter("@xBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objDB.CreateParameter("@xCashBankAccount", DbType.String, "A020301003", ParameterDirection.Input);
                param[3] = objDB.CreateParameter("@xFromDate", DbType.String, dtpInvoiceDate.Value.ToString("dd/MMM/yyyy"), ParameterDirection.Input);
                param[4] = objDB.CreateParameter("@xToDate", DbType.String, dtpInvoiceDate.Value.ToString("dd/MMM/yyyy"), ParameterDirection.Input);
                param[5] = objDB.CreateParameter("@xRepType", DbType.String, "", ParameterDirection.Input);
                param[6] = objDB.CreateParameter("@xEcode", DbType.String, "0", ParameterDirection.Input);
                param[7] = objDB.CreateParameter("@xFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);

                ds = objDB.ExecuteDataSet("SSCRM_REP_CASH_DFR", CommandType.StoredProcedure, param);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    strClosCash = dt.Rows[dt.Rows.Count - 1]["cbb_closing_balance"].ToString();
                }
                else
                {
                    strClosCash = "0";
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return strClosCash;
        }

        private void FillDenominationDefDetl()
        {
            string strSQL = "SELECT * FROM FA_DENM_PMTR WHERE DP_COMPANY_CODE='" + CommonData.CompanyCode + "' order by DP_SORT_NO ";
            SQLDB objDB=new SQLDB();
            DataTable dt = objDB.ExecuteDataSet(strSQL).Tables[0];
            if(dt.Rows.Count>0)
            {
                gvBillDetails.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count;i++ )
                {
                    gvBillDetails.Rows.Add();
                    gvBillDetails.Rows[i].Cells["SlNo"].Value = dt.Rows[i]["DP_SORT_NO"];
                    gvBillDetails.Rows[i].Cells["Denomination"].Value = dt.Rows[i]["DP_DENOMIN"];
                    gvBillDetails.Rows[i].Cells["Count"].Value = "0";
                    gvBillDetails.Rows[i].Cells["AmtReceived"].Value = "0";
                }
            }
        }

        private void dtpInvoiceDate_ValueChanged(object sender, EventArgs e)
        {
            txtCash.Text = FillDayClosingCash();
            if(dtpInvoiceDate.Value>DateTime.Today)
            {
                MessageBox.Show("Invalid Date","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                dtpInvoiceDate.Value = DateTime.Today;
            }

             SQLDB objDB = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();

            try
            {
                param[0] = objDB.CreateParameter("@xBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[1] = objDB.CreateParameter("@xFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[2] = objDB.CreateParameter("@xToDate", DbType.String, dtpInvoiceDate.Value.ToString("dd/MMM/yyyy"), ParameterDirection.Input);

                 ds = objDB.ExecuteDataSet("GetDenominationDetails", CommandType.StoredProcedure, param);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    gvBillDetails.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        gvBillDetails.Rows.Add();
                        gvBillDetails.Rows[i].Cells["SlNo"].Value = dt.Rows[i]["DPD_SI_NO"];
                        gvBillDetails.Rows[i].Cells["Denomination"].Value = dt.Rows[i]["DPD_DENOMINATIONS"];
                        gvBillDetails.Rows[i].Cells["Count"].Value = dt.Rows[i]["DPD_DENM_QTY"];
                        gvBillDetails.Rows[i].Cells["AmtReceived"].Value = dt.Rows[i]["DPD_AMOUNT"];
                    }
                    CalcTotal();
                }
                else
                {
                    FillDenominationDefDetl();
                    CalcTotal();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void gvBillDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (gvBillDetails.Rows.Count > 0)
            {
                if (e.ColumnIndex == gvBillDetails.Columns["Count"].Index)
                {
                    if (gvBillDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                    {
                        gvBillDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
                    }
                    if ((gvBillDetails.Rows.Count - 1) != e.RowIndex)
                        gvBillDetails.Rows[e.RowIndex].Cells["AmtReceived"].Value =
                            Convert.ToInt64(gvBillDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) *
                            Convert.ToInt64(gvBillDetails.Rows[e.RowIndex].Cells["Denomination"].Value);
                    else
                    {
                        gvBillDetails.Rows[e.RowIndex].Cells["AmtReceived"].Value =
                           Convert.ToInt64(gvBillDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) *
                           Convert.ToInt64("1");
                    }
                    CalcTotal();
                    if (txtCash.Text == txtTotalAmt.Text)
                    {
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        btnSave.Enabled = false;
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string strSQL = "";
            int J = 0;
            strSQL += "DELETE FA_DENM_DETAILS WHERE DPD_BRANCH_CODE='" + CommonData.BranchCode + "' AND DPD_FIN_YEAR='"+CommonData.FinancialYear+
                        "' AND DPD_DATE ='"+dtpInvoiceDate.Value.ToString("dd/MMM/yyyy")+"'";
            for (int i = 0; i < gvBillDetails.Rows.Count;i++ )
            {

                if (Convert.ToDouble(gvBillDetails.Rows[i].Cells["AmtReceived"].Value.ToString()) > 0 &&
                    (Convert.ToString(gvBillDetails.Rows[i].Cells["AmtReceived"].Value) != ""))
                {
                    ++J;
                    strSQL += "insert into FA_DENM_DETAILS(DPD_COMPAYNY_CODE,DPD_BRANCH_CODE,DPD_FIN_YEAR,DPD_DATE"+
                        ",DPD_DENOMINATIONS,DPD_DENM_QTY,DPD_AMOUNT,DPD_CREATED_BY,DPD_CREATED_DATE,DPD_SI_NO) values" +
                        "('"+CommonData.CompanyCode+"','"+CommonData.BranchCode+"','"+CommonData.FinancialYear+
                        "','" + dtpInvoiceDate.Value.ToString("dd/MMM/yyyy") + "','" + gvBillDetails.Rows[i].Cells["Denomination"].Value +
                        "','" + gvBillDetails.Rows[i].Cells["Count"].Value + "','" + gvBillDetails.Rows[i].Cells["AmtReceived"].Value +
                        "','" + CommonData.LogUserId + "',GETDATE(),'" + J + "')";
                }

            }
            SQLDB OBJDB = new SQLDB();
            int iRes = OBJDB.ExecuteSaveData(strSQL);
            if(iRes >0)
            {
                MessageBox.Show("Data Saved Successfully","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                btnClear_Click(null,null);
            }
        }
    }
}
