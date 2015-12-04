using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRM.App_Code;

namespace SSCRM
{
    public partial class frmECA : Form
    {
        public frmApplication objApplication;
        DataRow[] drs;
        public frmECA()
        {
            InitializeComponent();
        }
        public frmECA(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }

        private void frmECA_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.X + 300, Screen.PrimaryScreen.WorkingArea.Y + 200);
            this.StartPosition = FormStartPosition.CenterScreen;
            cmbTypeEC.SelectedIndex = 0;
            if (drs != null)
            {
                cmbTypeEC.SelectedIndex = cmbTypeEC.Items.IndexOf(drs[0][1].ToString());
                txtRemarks.Text = drs[0][2].ToString();
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
                ((frmApplication)objApplication).dtECA.Rows.Remove(drs[0]);
            //till here
            DataView dv = ((frmApplication)objApplication).dtECA.DefaultView;
            if (dv.Table.Rows.Count > 0)
            {
                dv.RowFilter = "TypeofECA='" + cmbTypeEC.Text + "'";
                DataTable dt;
                dt = dv.ToTable();
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("This ECA Type is already exists", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            ((frmApplication)objApplication).dtECA.Rows.Add(new Object[] { "-1", cmbTypeEC.Text, txtRemarks.Text });
            ((frmApplication)objApplication).GetDGVECA();
            this.Close();
        }
    }
}
