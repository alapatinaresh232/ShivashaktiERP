using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SSCRM
{
    public partial class frmAttendedAgriDeptEmpDetails : Form
    {
        public frmDemoPlots objfrmDemoPlots = null;
        DataRow[] drs;

        public frmAttendedAgriDeptEmpDetails(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }
        public frmAttendedAgriDeptEmpDetails()
        {
            InitializeComponent();
           
        }

        private void frmAttendedAgriDeptEmpDetails_Load(object sender, EventArgs e)
        {
            if (drs != null)
            {
                txtAgriDeptEName.Text = drs[0]["AgriEmpName"].ToString();
                //txtEmpDeptName.Text = drs[0]["AgriEmpDept"].ToString();
                txtEmpDesig.Text = drs[0]["AgriEmpDesig"].ToString();
                txtMobileNo.Text = drs[0]["mobileNo1"].ToString();

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (CheckData() == true)
            //{
            //    if (drs != null)
            //    {
            //        ((frmDemoPlots)objfrmDemoPlots).dtAgriDeptEmpDetails.Rows.Remove(drs[0]);
            //    }

            //    ((frmDemoPlots)objfrmDemoPlots).dtAgriDeptEmpDetails.Rows.Add(new Object[] { "-1", txtAgriDeptEName.Text.ToString().Replace("'"," "), txtEmpDesig.Text.ToString().Replace("'"," "), txtMobileNo.Text });
            //    ((frmDemoPlots)objfrmDemoPlots).GetAgriDeptEmpDetails();

            //    this.Close();
            //}
        }

        private bool CheckData()
        {
            bool flag = true;
            if (txtAgriDeptEName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Name", "Emp Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAgriDeptEName.Focus();
            }
            else if (txtMobileNo.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Mobile Number", "Emp Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMobileNo.Focus();
            }
            //else if (txtEmpDesig.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter Emp Desig", "Emp Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtEmpDesig.Focus();
            //}
            return flag;
        }

        void RestrictingDigits(KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsLetter(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

       

        private void txtEmpDesig_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
            if (e.KeyChar != '\b' && !(char.IsWhiteSpace(e.KeyChar)))
            {
                if (!char.IsLetter(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && e.KeyChar!=',')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtAgriDeptEName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);

            if (e.KeyChar != '\b' && !(char.IsWhiteSpace(e.KeyChar)))
            {
                if (!char.IsLetter(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtAgriDeptEName.Text = "";
            txtEmpDesig.Text = "";
            txtMobileNo.Text = "";
        }

       
       
    }
}
