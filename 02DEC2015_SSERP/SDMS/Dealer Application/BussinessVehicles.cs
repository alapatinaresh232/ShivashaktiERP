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
    public partial class BussinessVehicles : Form
    {
        public DealarApplicationForm dealerApplication;
        DataRow[] drs;
        public BussinessVehicles()
        {
            InitializeComponent();
        }
        public BussinessVehicles(DataRow[] dr)
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
                ((DealarApplicationForm)dealerApplication).dtBussnessVehicles.Rows.Remove(drs[0]);
            //till here
            if (cbVehType.SelectedIndex > 0 && txtNoOfVeh.Text.Length > 0)
            {
                ((DealarApplicationForm)dealerApplication).dtBussnessVehicles.Rows.Add(new Object[] { "-1", cbVehType.SelectedItem.ToString().ToUpper(), txtNoOfVeh.Text });
                ((DealarApplicationForm)dealerApplication).GetBusinessVehicles();
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

        private void txtNoOfVeh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void BussinessVehicles_Load(object sender, EventArgs e)
        {
            if (drs != null)
            {
               cbVehType.SelectedItem = drs[0][1].ToString();
               txtNoOfVeh.Text = drs[0][2].ToString();
                
            }
        }

        
    }
}
