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
    public partial class frmActionTakenForFF : Form
    {
        SQLDB objSQLdb = null;
        string sEmpName = "";
        public frmWrongCommitmentOrFinancialFrauds objWCFF = null;
        DataTable dtDetails = new DataTable();

        public frmActionTakenForFF(DataTable dt)
        {
            InitializeComponent();
            dtDetails = dt;
        }

        private void frmActionTakenForFF_Load(object sender, EventArgs e)
        {
            dtpActionDate.Value = DateTime.Today;
            
            if (dtDetails.Rows.Count>0)
            { 
                txtActionTakenEcode.Text = dtDetails.Rows[0]["ActionTakenEcode"].ToString();
                txtActionTakenName.Text = dtDetails.Rows[0]["EmpName"].ToString();
                txtFineAmt.Text = dtDetails.Rows[0]["FineAmt"].ToString();
                txtJvNo.Text = dtDetails.Rows[0]["JvNo"].ToString();
                txtDescription.Text = dtDetails.Rows[0]["Description"].ToString();
                if (dtDetails.Rows[0]["ActionDate"].ToString()!="")
                dtpActionDate.Value = Convert.ToDateTime(dtDetails.Rows[0]["ActionDate"].ToString());
            }

        }
              

        private string GetEmployeeName(Int32 EmpEcode)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();

            try
            {
                string strCmd = "SELECT MEMBER_NAME+'('+DESIG+')' EName FROM EORA_MASTER WHERE ECODE=" + EmpEcode + "";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    sEmpName = dt.Rows[0]["EName"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
                dt = null;
            }
            return sEmpName;

        }

        private bool CheckData()
        {
            bool flag = true;
            if (txtActionTakenName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Action Taken Ecode","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txtActionTakenEcode.Focus();
                return flag;
            }
            if (txtDescription.Text.Length == 0 || txtDescription.Text.Length < 20)
            {
                flag = false;
                MessageBox.Show("Please Enter Description Of Action Taken", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDescription.Focus();
                return flag;
            }
            if (dtpActionDate.Value > DateTime.Today)
            {
                flag = false;
                MessageBox.Show("Action Date Should Be Less Than Today's Date", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpActionDate.Focus();
                return flag;
            }
            if (txtDescription.Text.Length >500)
            {
                flag = false;
                MessageBox.Show("Description Should be Less than 500 Characters", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDescription.Focus();
                return flag;

            }

            return flag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {
                if (txtFineAmt.Text.Length == 0)
                {
                    txtFineAmt.Text = "0";
                }

                ((frmWrongCommitmentOrFinancialFrauds)objWCFF).dtActionTakenDetails.Rows.Clear();                

                ((frmWrongCommitmentOrFinancialFrauds)objWCFF).dtActionTakenDetails.Rows.Add(new Object[] { "-1",txtActionTakenEcode.Text,
                Convert.ToDateTime(dtpActionDate.Value).ToString("dd/MMM/yyyy"), txtActionTakenName.Text.ToString(),txtJvNo.Text.ToString().Replace("'"," ")
                ,txtFineAmt.Text,txtDescription.Text.ToString()});

                this.Close();
            }
        }

        private void txtActionTakenEcode_Validated(object sender, EventArgs e)
        {
            if (txtActionTakenEcode.Text.Length > 4)
            {
                txtActionTakenName.Text = GetEmployeeName(Convert.ToInt32(txtActionTakenEcode.Text));
            }
            else
            {
                txtActionTakenName.Text = "";
            }
        }

        private void txtActionTakenEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtFineAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtActionTakenEcode.Text = "";
            txtActionTakenName.Text = "";
            txtJvNo.Text = "";
            txtFineAmt.Text = "";
            txtDescription.Text = "";
            dtpActionDate.Value = DateTime.Today;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

      
       
    }
}
