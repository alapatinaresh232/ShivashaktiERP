using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;

namespace SSCRM
{
    public partial class frmAgriDeptEmpDetails : Form
    {
        public FarmerMeetingForm objFarmerMeetingForm = null;
        //string strType = "";
        DataRow[] drs;
       

        public frmAgriDeptEmpDetails(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;


        }
        public frmAgriDeptEmpDetails()
        {
            InitializeComponent();



        }
        private void frmAgriDeptEmpDetails_Load(object sender, EventArgs e)
        {
            if (drs != null)
            {
                txtAgriDeptEName.Text = drs[0]["AgriEmpName"].ToString();
                txtEmpDeptName.Text = drs[0]["AgriEmpDept"].ToString();
                txtEmpDesig.Text = drs[0]["AgriEmpDesig"].ToString();
                txtMobileNo.Text = drs[0]["mobileNo1"].ToString();

            }
        }

        private void btnSave1_Click(object sender, EventArgs e)
        {
            if (drs != null)
                ((FarmerMeetingForm)objFarmerMeetingForm).dtEmpDetails.Rows.Remove(drs[0]);
            if (CheckData() == true)
            {
                ((FarmerMeetingForm)objFarmerMeetingForm).dtEmpDetails.Rows.Add(new Object[] { "-1", txtAgriDeptEName.Text.ToString().Replace("'"," "), txtEmpDesig.Text.ToString().Replace("'"," "), 
                            txtEmpDeptName.Text.ToString().Replace("'"," "), txtMobileNo.Text });
                ((FarmerMeetingForm)objFarmerMeetingForm).GetAgriDeptEmpDetails();

                this.Close();
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtAgriDeptEName.Text = "";
            txtEmpDeptName.Text = "";
            txtEmpDesig.Text = "";
            txtMobileNo.Text = "";
           
        }

        private void txtMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && e.KeyChar != ',')
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

        private void txtEmpDeptName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
            if (e.KeyChar != '\b' && !(char.IsWhiteSpace(e.KeyChar)))
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
        private bool CheckData()
        {
            bool flag = true;
            if (txtAgriDeptEName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Name","Emp Details",MessageBoxButtons.OK,MessageBoxIcon.Information);
                txtAgriDeptEName.Focus();
            }
            else if (txtMobileNo.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Mobile Number", "Emp Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMobileNo.Focus();
            }
            else if (txtEmpDesig.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Emp Desig", "Emp Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmpDesig.Focus();
            }
            return flag;
        }

      
       
       
       
       

       
    }
}
