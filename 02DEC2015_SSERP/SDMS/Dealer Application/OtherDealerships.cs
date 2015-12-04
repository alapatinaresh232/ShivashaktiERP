using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using SDMS.App_Code;
using System.Windows.Forms;

namespace SDMS
{
    public partial class OtherDealerships : Form
    {
        public DealarApplicationForm dealerApplication;
        DataRow[] drs;

        public OtherDealerships()
        {
            InitializeComponent();
        }

        public OtherDealerships(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFromYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void OtherDealerships_Load(object sender, EventArgs e)
        {
            if (drs != null)
            {
                txtCompanyNameOD.Text = drs[0][1].ToString();
                txtFromYear.Text = drs[0][2].ToString();
                txtRemarks.Text = drs[0][3].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //UtilityLibrary oUtility = new UtilityLibrary();
            //if (!SSCRM.App_Code.UtilityLibrary.CustomValidate(grouper1, toolTip1))
            //    return;

            //bellow line for delete the row in dtEducation table
            if (drs != null)
                ((DealarApplicationForm)dealerApplication).dtOtherDealerShips.Rows.Remove(drs[0]);
            if (txtFromYear.Text.Length == 0)
            {
                txtFromYear.Text = 0 + "";
                
            }
            //till here
            if (txtCompanyNameOD.Text.Length > 0 )
            {
                ((DealarApplicationForm)dealerApplication).dtOtherDealerShips.Rows.Add(new Object[] { "-1", txtCompanyNameOD.Text.ToUpper(), txtFromYear.Text, txtRemarks.Text.ToUpper() });
                ((DealarApplicationForm)dealerApplication).GetOtherDealerShips();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please Enter Details", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCompanyNameOD_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtRemarks_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }
    }
}
