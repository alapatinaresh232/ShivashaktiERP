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
    public partial class frmProblemSolving : Form
    {
        DataRow[] drs;
        public ServiceActivities objServiceActivities;

        public frmProblemSolving()
        {
            InitializeComponent();
        }
        public frmProblemSolving(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }

      
        private void frmProblemSolving_Load(object sender, EventArgs e)
        {
            cbProblemType.SelectedIndex = 0;

            if (drs != null)
            {
                cbProblemType.Text = drs[0]["RelatedWork"].ToString();
                txtRemarks.Text = drs[0]["Remarks"].ToString();
            }

        }
        private bool CheckData()
        {
            bool flag = true;

            if (cbProblemType.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Problem Type", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbProblemType.Focus();
                return flag;
            }
            if (txtRemarks.Text.Length == 0 || txtRemarks.Text.Length < 10)
            {
                flag = false;
                MessageBox.Show("Please Enter Remarks", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRemarks.Focus();
                return flag;
            }

            return flag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                if (drs != null)
                {
                    ((ServiceActivities)objServiceActivities).dtActivityDetails.Rows.Remove(drs[0]);
                }

                ((ServiceActivities)objServiceActivities).dtActivityDetails.Rows.Add(new Object[] { "-1","PROBLEM SOLVING","",
                                "","","","","","","","","PROBLEM SOLVING", 
                               cbProblemType.Text.ToString()+'-'+"Problem Solving", txtRemarks.Text.ToString().Replace("'"," "),
                                0,0,cbProblemType.Text.ToString()});
                ((ServiceActivities)objServiceActivities).GetActivityDetails();


                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbProblemType.SelectedIndex = 0;
            txtRemarks.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

  
    }
}
