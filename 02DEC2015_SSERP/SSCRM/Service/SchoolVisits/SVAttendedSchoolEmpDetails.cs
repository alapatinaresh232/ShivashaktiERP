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
    public partial class SVAttendedSchoolEmpDetails : Form
    {
        public frmSchoolVisits objfrmSchoolVisits;
        DataRow[] drs;

        public SVAttendedSchoolEmpDetails()
        {
            InitializeComponent();
        }
        public SVAttendedSchoolEmpDetails(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }

       

        private void SVAttendedSchoolEmpDetails_Load(object sender, EventArgs e)
        {
            if (drs != null)
            {
               
                txtStaffName.Text = drs[0]["StaffName"].ToString();
                txtStaffDesig.Text = drs[0]["StaffDesig"].ToString();
                txtMobileNo.Text = drs[0]["StaffMobileNo"].ToString();
                txtStaffRemarks.Text = drs[0]["StaffRemarks"].ToString();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();

        }


        private bool CheckData()
        {
            bool flag = true;
            if (txtStaffName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Staff Name","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
                txtStaffName.Focus();
            }
            else if (txtStaffDesig.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Staff Desig", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtStaffDesig.Focus();
            }
            else if (txtMobileNo.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Staff Mobile Number", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMobileNo.Focus();
            }
            //else if (txtStaffRemarks.Text.Length == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Enter Remarks", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtStaffRemarks.Focus();
            //}

            return flag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {
                if (drs != null)
                {
                    ((frmSchoolVisits)objfrmSchoolVisits).dtSchoolStaffDetl.Rows.Remove(drs[0]);
                }


                ((frmSchoolVisits)objfrmSchoolVisits).dtSchoolStaffDetl.Rows.Add(new object[] {"-1",txtStaffName.Text.ToString(),
                            txtStaffDesig.Text.ToString(),txtMobileNo.Text,txtStaffRemarks.Text.ToString() });

                ((frmSchoolVisits)objfrmSchoolVisits).GetSchoolStaffDetails();

                this.Close();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtStaffName.Text = "";
            txtStaffDesig.Text = "";
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

        private void txtStaffName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtStaffDesig_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

      

       
       

       
    }
}
