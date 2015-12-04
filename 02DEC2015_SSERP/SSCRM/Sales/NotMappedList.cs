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
    public partial class NotMappedList : Form
    {
        SQLDB objSQLDB;
        public NotMappedList()
        {
            InitializeComponent();
        }

        private void NotMappedList_Load(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            UtilityLibrary.PopulateControl(cmbBranch, objSQLDB.ExecuteDataSet("select branch_code,Branch_name From branch_mas where branch_type='BR' and active='T'").Tables[0].DefaultView, 1, 0, "-- ALL --", 0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            CommonData.ViewReport = "NOTMAPPEDLIST";
            string sVal = "";
            if (cmbBranch.Text == "-- ALL --")
                sVal = "ALL";
            else
                //sVal = cmbBranch.SelectedValue.ToString();
                sVal = ((DataRowView)cmbBranch.SelectedItem).Row.ItemArray[0].ToString();
            ReportViewer oReportViewer = new ReportViewer(sVal, Convert.ToDateTime(dtpDate.Value).ToString("15/MMM/yyyy"));
            oReportViewer.Show();
        }
    }
}
