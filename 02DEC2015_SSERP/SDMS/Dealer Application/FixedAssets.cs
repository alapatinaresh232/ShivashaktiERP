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
    public partial class FixedAssets : Form
    {
        public DealarApplicationForm dealerApplication;
        DataRow[] drs;
        public FixedAssets()
        {
            InitializeComponent();
        }
        public FixedAssets(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //UtilityLibrary oUtility = new UtilityLibrary();
            //if (!SSCRM.App_Code.UtilityLibrary.CustomValidate(grouper1, toolTip1))
            //    return;

            //bellow line for delete the row in dtEducation table
            if (drs != null)
                ((DealarApplicationForm)dealerApplication).dtFixedAssets.Rows.Remove(drs[0]);
            //till here
            if (txtDetails1.Text.Length > 0 || txtDetails2.Text.Length > 0 || txtDetails3.Text.Length > 0)
            {
                ((DealarApplicationForm)dealerApplication).dtFixedAssets.Rows.Add(new Object[] { "-1", txtDetails1.Text, txtDetails2.Text, txtDetails3.Text, });
                ((DealarApplicationForm)dealerApplication).GetFixedAssets();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please Enter Details", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FixedAssets_Load(object sender, EventArgs e)
        {
            if (drs != null)
            {
                txtDetails1.Text= drs[0][1].ToString();
                txtDetails2.Text = drs[0][2].ToString();
                txtDetails3.Text = drs[0][3].ToString();
            }
        }

        private void txtDetails1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtDetails2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtDetails3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }
    }
}
