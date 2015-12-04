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
    public partial class PrevTurnOver : Form
    {
        public DealarApplicationForm dealerApplication;
        DataRow[] drs;
        public PrevTurnOver()
        {
            InitializeComponent();
        }
        public PrevTurnOver(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }
        private void PrevTurnOver_Load(object sender, EventArgs e)
        {
            if (drs != null)
            {
                txtPrevTurnOverYear.Text = drs[0][1].ToString();
                txtPrevTurnOverProduct.Text = drs[0][2].ToString();
                txtPrevTurnOver.Text = drs[0][3].ToString();
            }
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
                ((DealarApplicationForm)dealerApplication).dtPrevTurnOver.Rows.Remove(drs[0]);
            if (txtPrevTurnOver.Text.Length == 0)
            {
                txtPrevTurnOver.Text = 0 + "";
            }
            if (txtPrevTurnOverYear.Text.Length == 0)
            {
                txtPrevTurnOverYear.Text = 0 + "";
            }
            //till here
            if (txtPrevTurnOver.Text.Length > 0&& txtPrevTurnOverYear.Text.Length > 0)
            {
                ((DealarApplicationForm)dealerApplication).dtPrevTurnOver.Rows.Add(new Object[] { "-1", txtPrevTurnOverYear.Text, txtPrevTurnOverProduct.Text.ToUpper(), txtPrevTurnOver.Text });
                ((DealarApplicationForm)dealerApplication).GetPrevTurnOverDetails();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please Enter Details", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPrevTurnOverYear_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtPrevTurnOver_KeyPress(object sender, KeyPressEventArgs e)
        {
            restrctingToDigits(e);
        }

        private void txtPrevTurnOverProduct_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }
    }
}
