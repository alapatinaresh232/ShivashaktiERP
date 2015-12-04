using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using System.Diagnostics;
using System.Configuration;
using SSCRMDB;
using SSAdmin;
using SSTrans;

namespace SSCRM
{
    partial class SalesInvoiceEdit : Form
    {
        private SQLDB objSQLDB = null;
        private InvoiceDB objData = null;
        private bool IsBiltinInvoice = false;
        private bool IsSaleInvoice = false;
        public SalesInvoiceEdit()
        {
            InitializeComponent();
            //this.Text = String.Format("About {0} {0}", AssemblyTitle);
            //this.labelProductName.Text = AssemblyProduct;
            //this.labelVersion.Text = String.Format("Version {0} {0}", AssemblyVersion);
            //this.labelCopyright.Text = AssemblyCopyright;
            //this.labelCompanyName.Text = AssemblyCompany;
            //this.textBoxDescription.Text = AssemblyDescription;
        }

        private void SalesInvoiceEdit_Load(object sender, EventArgs e)
        {

        }

        private void txtPreOrderNo_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (char.IsLetterOrDigit(e.KeyChar) == false)
                e.Handled = true;

            if (e.KeyChar == 8)
                e.Handled = false;

            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtPreInvoiceNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == false)
                e.Handled = true;

            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void txtPresOrderNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetterOrDigit(e.KeyChar) == false)
                e.Handled = true;

            if (e.KeyChar == 8)
                e.Handled = false;

            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtPresInvoiceNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == false)
                e.Handled = true;

            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void txtPreOrderNo_Validated(object sender, EventArgs e)
        {
            if (Convert.ToString(txtPreOrderNo.Text).Trim().Length > 0)
            {                
                FillInvOrBultInData(0, Convert.ToInt32(txtPreOrderNo.Text).ToString("00000"));
            }
        }

        private void FillInvOrBultInData(int nInvNo, string sOrderNo)
        {
            Hashtable ht;
            DataTable InvH;
            DataTable InvD;
            DataSet ds = new DataSet();
            try
            {
                
                objData = new InvoiceDB();
                ht = objData.GetInvoiceData(CommonData.StateCode, CommonData.BranchCode, nInvNo, sOrderNo);

                InvH = (DataTable)ht["InvHead"];
                InvD = (DataTable)ht["InvDetail"];
                if (InvH.Rows.Count == 0)
                {
                    
                    ht = objData.InvoiceBulitin_Get(CommonData.StateCode, CommonData.BranchCode, nInvNo, sOrderNo);
                    InvH = (DataTable)ht["InvHead"];
                    InvD = (DataTable)ht["InvDetail"];
                    if (InvH.Rows.Count == 0)
                    {
                        IsBiltinInvoice = false;
                        IsSaleInvoice = false;
                    }
                    else
                    {
                        IsBiltinInvoice = true;
                        IsSaleInvoice = false;
                    }
                }
                else
                {
                    IsBiltinInvoice = false;
                    IsSaleInvoice = true;
                }

                FillInvoiceData(InvH, InvD);
                

            }
            catch (Exception ex)
            {
                objData = null;
                ht = null;
                InvH = null;
                InvD = null;
                MessageBox.Show(ex.Message, "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objData = null;
                ht = null;
                InvH = null;
                InvD = null;
                ds = null;
            }
        }

        private void FillInvoiceData(DataTable dtInvH, DataTable dtInvD)
        {
            try
            {
                if (dtInvH.Rows.Count > 0)
                {
                    txtPreOrderNo.Text = dtInvH.Rows[0]["order_number"] + "";
                    txtPreInvoiceNo.Text = dtInvH.Rows[0]["invoice_number"] + "";
                    txtPresOrderNo.Text = dtInvH.Rows[0]["order_number"] + "";
                    txtPresInvoiceNo.Text = dtInvH.Rows[0]["invoice_number"] + "";

                }
                else
                {
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                dtInvH = null;
            }
        }

        private void txtPresOrderNo_KeyUp(object sender, KeyEventArgs e)
        {
            GetInvoiceorBulletinDataByOrderNo(Convert.ToInt32(txtPresOrderNo.Text).ToString("00000"));
        }

        private void GetInvoiceorBulletinDataByOrderNo(string sOrderNo)
        {
            objSQLDB = new SQLDB();
            string sqlText = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            string sqlText = " UPDATE SALES_INV_BULTIN_HEAD SET SIBH_ORDER_NUMBER = '" + txtPresOrderNo.Text + "' " +
                            "WHERE SIBH_BRANCH_CODE='" + CommonData.BranchCode + "' AND SIBH_FIN_YEAR ='" + CommonData.FinancialYear + "' " +
                            "AND SIBH_ORDER_NUMBER='" + txtPreOrderNo.Text + "';" +
                            " UPDATE SALES_INV_HEAD SET SIH_ORDER_NUMBER = '" + txtPresOrderNo.Text + "' " +
                            "WHERE SIH_BRANCH_CODE='" + CommonData.BranchCode + "' AND SIH_FIN_YEAR ='" + CommonData.FinancialYear + "' " +
                            "AND SIH_ORDER_NUMBER='" + txtPreOrderNo.Text + "';";
            int iRes = 0;
            try
            {
                iRes = objSQLDB.ExecuteSaveData(sqlText);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLDB = null;
            }
            if (iRes > 0)
            {
                MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPreInvoiceNo.Text = "";
                txtPreOrderNo.Text = "";
                txtPresInvoiceNo.Text = "";
                txtPresOrderNo.Text = "";
            }
            else
            {
                MessageBox.Show("Data not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtPreInvoiceNo.Text = "";
            txtPreOrderNo.Text = "";
            txtPresInvoiceNo.Text = "";
            txtPresOrderNo.Text = "";
        }
    }
}
