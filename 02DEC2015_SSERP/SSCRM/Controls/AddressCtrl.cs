using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SSCRM
{
    public partial class AddressCtrl : UserControl
    {
        public AddressCtrl()
        {
            InitializeComponent();
        }

        private void btnAddress_Click(object sender, EventArgs e)
        {
            VillageSearch VSearch = new VillageSearch("HRAddress");
            VSearch.objFrmAddress = this;
            VSearch.ShowDialog();
        }
        public string HouseNo
        {
            get { return txtHouseNo.Text; }
            set { txtHouseNo.Text = value; }
        }
        public string LandMark
        {
            get { return txtLandMark.Text; }
            set { txtLandMark.Text = value; }
        }
        public string Village
        {
            get { return txtVillage.Text; }
            set { txtVillage.Text = value; }
        }
        public string Mondal
        {
            get { return txtMondal.Text; }
            set { txtMondal.Text = value; }
        }
        public string District
        {
            get { return txtDistrict.Text; }
            set { txtDistrict.Text = value; }
        }
        public string State
        {
            get { return txtState.Text; }
            set { txtState.Text = value; }
        }
        public string Pin
        {
            get { return txtPin_num.Text; }
            set { txtPin_num.Text = value; }
        }

        private void txtPin_num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }
    }
}
