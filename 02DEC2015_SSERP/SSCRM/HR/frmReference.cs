using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRM.App_Code;
using SSCRMDB;

namespace SSCRM
{
    public partial class frmReference : Form
    {
        public frmApplication objApplication;
        DataRow[] drs;
        private SQLDB objSQLDB = null;
        public frmReference()
        {
            InitializeComponent();
        }
        public frmReference(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }
        private void frmReference_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.X + 300, Screen.PrimaryScreen.WorkingArea.Y + 200);
            this.StartPosition = FormStartPosition.CenterScreen;
            objSQLDB = new SQLDB();
            string strSQL1 = "SELECT OM_OCCUPATION,OM_OCCUPATION FROM OCCUPATION_MASTER";
            DataTable dt1 = objSQLDB.ExecuteDataSet(strSQL1, CommandType.Text).Tables[0];
            UtilityLibrary.PopulateControl(cmbOccupation_optional, dt1.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
            if (drs != null)
            {
                txtReference.Text = drs[0][1].ToString();
                //txtrefOccu.Text = drs[0][2].ToString();
                cmbOccupation_optional.Text = drs[0][2].ToString();
                txtRefPhno_optional.Text = drs[0][3].ToString();
                addressCtrl1.HouseNo = drs[0][4].ToString();
                addressCtrl1.LandMark = drs[0][5].ToString();
                addressCtrl1.Village = drs[0][6].ToString();
                addressCtrl1.Mondal = drs[0][7].ToString();
                addressCtrl1.District = drs[0][8].ToString();
                addressCtrl1.State = drs[0][9].ToString();
                addressCtrl1.Pin = drs[0][10].ToString();
                txtPhoneno.Text = drs[0][11].ToString();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UtilityLibrary oUtility = new UtilityLibrary();
            if (!SSCRM.App_Code.UtilityLibrary.CustomValidate(grouper1, toolTip1))
                return;
            //bellow line for delete the row in dtReference table
            if (drs != null)
                ((frmApplication)objApplication).dtReference.Rows.Remove(drs[0]);
            //till here
            if (addressCtrl1.Pin == "")
                addressCtrl1.Pin = "0";
            ((frmApplication)objApplication).dtReference.Rows.Add(new Object[] { "-1", txtReference.Text, cmbOccupation_optional.Text.ToString(), txtRefPhno_optional.Text, addressCtrl1.HouseNo, addressCtrl1.LandMark, addressCtrl1.Village, addressCtrl1.Mondal, addressCtrl1.District, addressCtrl1.State, addressCtrl1.Pin, txtPhoneno.Text });
            ((frmApplication)objApplication).GetReference();
            this.Close();
        }

        private void txtPhoneno_KeyPress(object sender, KeyPressEventArgs e)
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
