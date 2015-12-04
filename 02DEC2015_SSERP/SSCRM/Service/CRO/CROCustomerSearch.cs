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
    public partial class CROCustomerSearch : Form
    {
        SQLDB objDb = null;
        InvoiceDB objInvDb = null;
       
        public CROCustomerSearch()
        {
            InitializeComponent();
        }

        private void CROCustomerSearch_Load(object sender, EventArgs e)
        {
            dgvCRODetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                         System.Drawing.FontStyle.Regular);
        }

        private void FillCROCustomerDetails()
        {
            int Row = 0;
            objInvDb = new InvoiceDB();
            DataTable dt = new DataTable();
            dgvCRODetails.Rows.Clear();
            try
            {
                dt = objInvDb.Get_SalesInvBulletDetails(txtOrderNo.Text.ToString(),txtMobileNo.Text.ToString()).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();

                        DataGridViewCell cellSlNo = new DataGridViewTextBoxCell();
                        cellSlNo.Value = (i + 1).ToString();
                        tempRow.Cells.Add(cellSlNo);

                        DataGridViewCell cellCompanyCode = new DataGridViewTextBoxCell();
                        cellCompanyCode.Value = dt.Rows[i]["CompanyCode"].ToString();
                        tempRow.Cells.Add(cellCompanyCode);

                        DataGridViewCell cellBranchCode = new DataGridViewTextBoxCell();
                        cellBranchCode.Value = dt.Rows[i]["BranchCode"].ToString();
                        tempRow.Cells.Add(cellBranchCode);

                        DataGridViewCell cellOrderNo = new DataGridViewTextBoxCell();
                        cellOrderNo.Value = dt.Rows[i]["OrderNo"].ToString();
                        tempRow.Cells.Add(cellOrderNo);


                        DataGridViewCell cellInvNo = new DataGridViewTextBoxCell();
                        cellInvNo.Value = dt.Rows[i]["InvNo"].ToString();
                        tempRow.Cells.Add(cellInvNo);

                        DataGridViewCell cellFinYear = new DataGridViewTextBoxCell();
                        cellFinYear.Value = dt.Rows[i]["FinYear"].ToString();
                        tempRow.Cells.Add(cellFinYear);


                        DataGridViewCell cellDocMonth = new DataGridViewTextBoxCell();
                        cellDocMonth.Value = dt.Rows[i]["DocMonth"].ToString();
                        tempRow.Cells.Add(cellDocMonth);


                        DataGridViewCell cellState = new DataGridViewTextBoxCell();
                        cellState.Value = dt.Rows[i]["StateName"].ToString();
                        tempRow.Cells.Add(cellState);

                        DataGridViewCell cellCustomerName= new DataGridViewTextBoxCell();
                        cellCustomerName.Value = dt.Rows[i]["CustmerName"].ToString();
                        tempRow.Cells.Add(cellCustomerName);

                        DataGridViewCell cellMobileNo = new DataGridViewTextBoxCell();
                        cellMobileNo.Value = dt.Rows[i]["MobileNo"].ToString();
                        tempRow.Cells.Add(cellMobileNo);

                        Row = Row + 1;
                        dgvCRODetails.Rows.Add(tempRow);
                       



                    }
                  
                }
                else
                {
                    MessageBox.Show("No Customer Details ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objInvDb = null;
                dt = null;
            }

            
            
        }
               
      
        private bool CheckData()
        {
           bool Chkflag = true;

            if (txtOrderNo.Text.Length == 0)
            {
                Chkflag = false;
                MessageBox.Show("Please Enter OrderNO", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtOrderNo.Focus();
                return Chkflag;
            }
        
          
            return Chkflag;

        }

       

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {
                FillCROCustomerDetails();
              
            }
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
               this.Close();
               this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtMobileNo.Text = "";
            txtOrderNo.Text = "";
            dgvCRODetails.Rows.Clear();
            txtOrderNo.Focus();
        }

        private void txtOrderNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

            }
        }

        private void txtMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

            }
        }

        private void dgvCRODetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string strCompCode = "";
            string strBranchCode = "";
            string strFinYear = "";
            int nInvNo = 0;
           

            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == dgvCRODetails.Columns["Print"].Index)
                {
                    strFinYear = dgvCRODetails.Rows[e.RowIndex].Cells["FinYear"].Value.ToString();
                    strCompCode = dgvCRODetails.Rows[e.RowIndex].Cells["CompanyCode"].Value.ToString();
                    strBranchCode = dgvCRODetails.Rows[e.RowIndex].Cells["BranchCode"].Value.ToString();
                    nInvNo = Convert.ToInt32(dgvCRODetails.Rows[e.RowIndex].Cells["InvNo"].Value.ToString());

                    CommonData.ViewReport = "SSCRM_REP_INVOICE_PRINT";
                    ReportViewer childReportViewer = new ReportViewer(strCompCode, strBranchCode,strFinYear, nInvNo);
                    childReportViewer.Show();
                }
            }
        }

     

     
             
    }
}
