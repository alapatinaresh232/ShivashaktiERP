using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRM.App_Code;
using SSAdmin;

namespace SSCRM
{
    public partial class frmDocuments : Form
    {
        public frmApplication objApplication;
        StaffLevel objData;
        DataRow[] drs;
        public frmDocuments()
        {
            InitializeComponent();
        }
        public frmDocuments(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }
        private void frmDocuments_Load(object sender, EventArgs e)
        {
            objData = new StaffLevel();
            UtilityLibrary.PopulateControl(cmbDocument, objData.GetDocuments().Tables[0].DefaultView, 0, 1, "--PLEASE SELECT--", 0);
            objData = null;
            dtpDate.Value = System.DateTime.Now;
            if (drs != null)
            {
                cmbDocument.Text = drs[0][1].ToString();
                if (Convert.ToBoolean(drs[0][2]) == false)
                    cbRecieved.SelectedIndex = 2;
                else
                    cbRecieved.SelectedIndex = 1;
                dtpDate.Value = Convert.ToDateTime(drs[0][3]);
                txtRemarks.Text = drs[0][4].ToString();
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
            //bellow line for delete the row in dtFamily table
            if (drs != null)
                ((frmApplication)objApplication).dtDocuments.Rows.Remove(drs[0]);
            //till here
            DataView dv = ((frmApplication)objApplication).dtDocuments.DefaultView;
            if (dv.Table.Rows.Count > 0)
            {
                dv.RowFilter = "Head='" + cmbDocument.Text + "'";
                DataTable dt;
                dt = dv.ToTable();
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("This Document is already exists", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            bool ivflg;
            if (cbRecieved.SelectedIndex == 1)
                ivflg = true;
            else if (cbRecieved.SelectedIndex == 2)
                ivflg = false;
            else
            {
                MessageBox.Show("Select Recieved Status", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ((frmApplication)objApplication).dtDocuments.Rows.Add(new Object[] { "-1", cmbDocument.Text, ivflg, dtpDate.Value, txtRemarks.Text });
            //((frmApplication)objApplication).GetDocuments();
            this.Close();
        }
    }
}
