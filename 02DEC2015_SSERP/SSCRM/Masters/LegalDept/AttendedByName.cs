using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSAdmin;

namespace SSCRM
{
    public partial class AttendedByName : Form
    {
       public CaseHearings objCaseHearings;
        public AttendedByName()
        {
            InitializeComponent();
        }

        private void txtEcode_TextChanged(object sender, EventArgs e)
        {
            if(txtEcode.Text.Length>0)
            {
               
              Master  objMstr = new Master();
                DataSet ds = new DataSet();
                try
                {
                    ds = objMstr.GetEmployeeMasterDetl(txtEcode.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objMstr = null;
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtName.Text = ds.Tables[0].Rows[0]["EmpName"].ToString();
                }
                else
                {
                    txtName.Text = "";
                }
                ds = null;
            }
           else
            {
                txtName.Text = "";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtName.Text.Length>0)
            {
                bool flag = false;
                if (((CaseHearings)objCaseHearings).gvAttendedBy.Rows.Count > 0)
                {
                 
                    for (int iRow = 0; iRow < ((CaseHearings)objCaseHearings).gvAttendedBy.Rows.Count; iRow++)
                    {
                        if (((CaseHearings)objCaseHearings).gvAttendedBy.Rows[iRow].Cells["Ecode"].Value.ToString() == txtEcode.Text.ToString())
                        {
                            flag = true;
                            MessageBox.Show("Already Exists");
                            txtName.Text = "";
                            txtEcode.Text = "";
                        }
                    }
                }
                if (flag == false)
                {
                    ((CaseHearings)objCaseHearings).gvAttendedBy.Rows.Add();
                    ((CaseHearings)objCaseHearings).gvAttendedBy.Rows[objCaseHearings.gvAttendedBy.Rows.Count - 1].Cells["SiNO"].Value = objCaseHearings.gvAttendedBy.Rows.Count;
                    ((CaseHearings)objCaseHearings).gvAttendedBy.Rows[objCaseHearings.gvAttendedBy.Rows.Count - 1].Cells["type"].Value = "EMPLOYEE";
                    ((CaseHearings)objCaseHearings).gvAttendedBy.Rows[objCaseHearings.gvAttendedBy.Rows.Count - 1].Cells["Ecode"].Value = txtEcode.Text;
                    ((CaseHearings)objCaseHearings).gvAttendedBy.Rows[objCaseHearings.gvAttendedBy.Rows.Count - 1].Cells["EmpName"].Value = txtName.Text;

                    txtName.Text = "";
                    txtEcode.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Please Enter Correct Ecode");
            }
        }

        private void AttendedByName_Load(object sender, EventArgs e)
        {

        }

        private void txtEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
    }
}

