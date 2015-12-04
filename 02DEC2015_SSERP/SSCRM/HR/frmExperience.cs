using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRM.App_Code;
using SSCRM;
namespace SSCRM
{
    public partial class frmExperience : Form
    {
        public frmApplication objApplication;
        DataRow[] drs;
        public frmExperience()
        {
            InitializeComponent();
        }
        public frmExperience(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }

        private void frmExperience_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.X + 300, Screen.PrimaryScreen.WorkingArea.Y + 200);
            this.StartPosition = FormStartPosition.CenterScreen;
            if (drs != null)
            {
                txtOrganisation.Text = drs[0][1].ToString();
                dtpFrom.Value = Convert.ToDateTime(drs[0][2]);
                dtpTo.Value = Convert.ToDateTime(drs[0][3]);
                addressCtrl1.HouseNo = drs[0][4].ToString();
                addressCtrl1.LandMark = drs[0][5].ToString();
                addressCtrl1.Village = drs[0][6].ToString();
                addressCtrl1.Mondal = drs[0][7].ToString();
                addressCtrl1.District = drs[0][8].ToString();
                addressCtrl1.State = drs[0][9].ToString();
                addressCtrl1.Pin = drs[0][10].ToString();
                txtJ_Desg.Text = drs[0][11].ToString();
                txtL_Desg.Text = drs[0][12].ToString();
                txtSalary_num.Text = drs[0][13].ToString();
                txtRemarks_optional.Text = drs[0][14].ToString();
                txtReason_optional.Text = drs[0][15].ToString();
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
            //bellow line for delete the row in dtEducation table
            if (drs != null)
                ((frmApplication)objApplication).dtExperience.Rows.Remove(drs[0]);
            //till here
            if (addressCtrl1.Pin == "")
                addressCtrl1.Pin = "0";
            ((frmApplication)objApplication).dtExperience.Rows.Add(new Object[] { "-1", txtOrganisation.Text, dtpFrom.Value, dtpTo.Value, addressCtrl1.HouseNo, addressCtrl1.LandMark, addressCtrl1.Village, addressCtrl1.Mondal, addressCtrl1.District, addressCtrl1.State, addressCtrl1.Pin, txtJ_Desg.Text, txtL_Desg.Text, txtSalary_num.Text, txtRemarks_optional.Text, txtReason_optional.Text });
            ((frmApplication)objApplication).GetExperience();
            this.Close();
        }
    }
}
