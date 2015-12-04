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
    public partial class BussinessDetail : Form
    {
        public DealarApplicationForm dealerApplication;
        DataRow[] drs;
        public BussinessDetail()
        {
            InitializeComponent();
        }
        public BussinessDetail(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }
       
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //UtilityLibrary oUtility = new UtilityLibrary();
            //if (!SSCRM.App_Code.UtilityLibrary.CustomValidate(grouper1, toolTip1))
            //    return;

            //bellow line for delete the row in dtEducation table
            if (drs != null)
                ((DealarApplicationForm)dealerApplication).dtBussinessDetails.Rows.Remove(drs[0]);
            if (txtBussinessDetailsYear.Text.Length == 0)
            {
                txtBussinessDetailsYear.Text = 0+"";
            }
            if (txtBussinessDetailsLast2y.Text.Length == 0)
            {
                txtBussinessDetailsLast2y.Text = 0 + "";
                txtBussinessDetailsTotalTurnOver.Text = txtBussinessDetailsLast2y.Text;
            }
            if (txtBussinessDetailsTurnOver.Text.Length == 0)
            {
                txtBussinessDetailsTurnOver.Text = 0 + "";
                txtBussinessDetailsTotalTurnOver.Text = txtBussinessDetailsTurnOver.Text;
            }
            
            //till here
            if (txtBussinessDetailsCompName.Text.Length > 0)
            {
                ((DealarApplicationForm)dealerApplication).dtBussinessDetails.Rows.Add(new Object[] { "-1", txtBussinessDetailsCompName.Text.ToUpper(), txtBussinessDetailsYear.Text, txtBussinessDetailsProd.Text.ToUpper(), txtBussinessDetailsLast2y.Text, txtBussinessDetailsTurnOver.Text, txtBussinessDetailsTotalTurnOver.Text });
                ((DealarApplicationForm)dealerApplication).GetBussinessDetails();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please Enter Details", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BussinessDetail_Load(object sender, EventArgs e)
        {
            if (drs != null)
            {
                txtBussinessDetailsCompName.Text = drs[0][1].ToString();
                txtBussinessDetailsYear.Text = drs[0][2].ToString();
                txtBussinessDetailsProd.Text = drs[0][3].ToString();
                txtBussinessDetailsLast2y.Text = drs[0][4].ToString();
                txtBussinessDetailsTurnOver.Text = drs[0][5].ToString();
                txtBussinessDetailsTotalTurnOver.Text = drs[0][6].ToString();
            }
        }

        private void txtBussinessDetailsYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }
        private void restrctingToDigits(KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtBussinessDetailsLast2y_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }

        private void txtBussinessDetailsTurnOver_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }

        private void txtBussinessDetailsLast2y_Validated(object sender, EventArgs e)
        {
           
            if (txtBussinessDetailsTurnOver.Text.Length == 0)
            {
                txtBussinessDetailsTotalTurnOver.Text = txtBussinessDetailsLast2y.Text;
            }
            if (txtBussinessDetailsLast2y.Text.Length == 0)
            {
                txtBussinessDetailsTotalTurnOver.Text = txtBussinessDetailsTurnOver.Text;
            }
            if (txtBussinessDetailsTurnOver.Text.Length > 0 && txtBussinessDetailsLast2y.Text.Length > 0)
            {
                txtBussinessDetailsTotalTurnOver.Text = (Convert.ToInt32(txtBussinessDetailsTurnOver.Text) + Convert.ToInt32(txtBussinessDetailsLast2y.Text))+"";
            }
            
        }

        private void txtBussinessDetailsTurnOver_Validated(object sender, EventArgs e)
        {
            if (txtBussinessDetailsLast2y.Text.Length == 0)
            {
                txtBussinessDetailsTotalTurnOver.Text = txtBussinessDetailsTurnOver.Text;
            }
            if (txtBussinessDetailsTurnOver.Text.Length == 0)
            {
                txtBussinessDetailsTotalTurnOver.Text = txtBussinessDetailsLast2y.Text;
            }
            if (txtBussinessDetailsTurnOver.Text.Length > 0 && txtBussinessDetailsLast2y.Text.Length > 0)
            {
                txtBussinessDetailsTotalTurnOver.Text = (Convert.ToInt32(txtBussinessDetailsTurnOver.Text) + Convert.ToInt32(txtBussinessDetailsLast2y.Text)) + "";
            }
        }

        private void txtBussinessDetailsCompName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtBussinessDetailsProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }
    }
}
