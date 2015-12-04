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
    public partial class Territory_Details : Form
    {
        public DealarApplicationForm dealerApplication;
        DataRow[] drs;
        public Territory_Details()
        {
            InitializeComponent();
        }
        public Territory_Details(DataRow[] dr)
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
                ((DealarApplicationForm)dealerApplication).dtTerritoryDetl.Rows.Remove(drs[0]);
            //till here
            if (cbTerritoryType.SelectedIndex > 0 && txtTerritoryName.Text.Length > 0)
            {
                ((DealarApplicationForm)dealerApplication).dtTerritoryDetl.Rows.Add(new Object[] { "-1", cbTerritoryType.SelectedItem.ToString().ToUpper(), txtTerritoryName.Text.ToUpper() });
                ((DealarApplicationForm)dealerApplication).GetTerritoryDetl();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please Enter Details", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Territory_Details_Load(object sender, EventArgs e)
        {
            if (drs != null)
            {
                cbTerritoryType.SelectedItem = drs[0][1].ToString();
                txtTerritoryName.Text = drs[0][2].ToString();
               
            }
        }

        private void txtTerritoryName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }
    }
}
