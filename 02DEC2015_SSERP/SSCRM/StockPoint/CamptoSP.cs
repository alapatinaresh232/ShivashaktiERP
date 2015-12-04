using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSCRM.App_Code;
using System.Data.SqlClient;
using SSAdmin;
namespace SSCRM
{
    public partial class CamptoSP : Form
    {
        SQLDB objSQLDB = new SQLDB();
        public CamptoSP()
        {
            InitializeComponent();
        }

        private void CamptoSP_Load(object sender, EventArgs e)
        {
            dtDocMon.Value = System.DateTime.Now;
            DataView dvBranch = objSQLDB.ExecuteDataSet("SELECT BRANCH_CODE,BRANCH_NAME FROM BRANCH_MAS WHERE BRANCH_TYPE='BR' ORDER BY BRANCH_NAME").Tables[0].DefaultView;
            UtilityLibrary.PopulateControl(cmbBranch, dvBranch, 1, 0, "--PLEASE SELECT--", 0);
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            clbCampLst.Items.Clear();
            chkStockPoints.Items.Clear();
            string sqlQry = "select distinct CM_CAMP_NAME,CM_VILLAGE+','+CM_MANDAL+','+CM_DISTRICT+','+CM_STATE as CodeAdd From camp_mas A INNER JOIN levelgroup_map B ON A.CM_CAMP_NAME=B.LGM_GROUP_NAME where CM_VILLAGE is not null and CM_VILLAGE<>'' " +
                " and cm_branch_code='" + cmbBranch.SelectedValue + "' and lgm_doc_month='" + dtDocMon.Value.ToString("MMMyyyy") + "' order by CM_Camp_Name";
            DataSet dsCmps = objSQLDB.ExecuteDataSet(sqlQry);
            foreach (DataRow dataRow in dsCmps.Tables[0].Rows)
            {
                if (dataRow["cm_camp_name"] + "" != "")
                {
                    NewCheckboxListItem oclBox = new NewCheckboxListItem();
                    oclBox.Tag = dataRow["CodeAdd"].ToString();
                    oclBox.Text = dataRow["cm_camp_name"].ToString();
                    clbCampLst.Items.Add(oclBox);
                    oclBox = null;
                }
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            CommonData.ViewReport = "CAMPTOSPDISTANCE";
            ReportViewer oReportViewer = new ReportViewer(cmbBranch.SelectedValue.ToString(), dtDocMon.Value.ToString("MMMyyyy"));
            oReportViewer.Show();
        }
        public void GetStockPoints()
        {
            string sqlSps = "SELECT DISTINCT BRANCH_NAME,CBD_TO_BRANCH_OR_CAMP_CODE fROM CAMP_MAS A INNER JOIN LEVELGROUP_MAP B ON A.CM_CAMP_NAME=B.LGM_GROUP_NAME " +
                " INNER JOIN CAMP_BRANCH_DISTANCE C ON  A.CM_VILLAGE+','+CM_MANDAL+','+CM_DISTRICT+','+CM_STATE=C.CBD_FROM_CAMP_OR_BRANCH_CODE INNER JOIN " +
                " BRANCH_MAS D ON C.CBD_TO_BRANCH_CODE=D.BRANCH_CODE WHERE CM_CAMP_NAME='" + clbCampLst.SelectedItem.ToString() + "' AND  LGM_DOC_MONTH='" + dtDocMon.Value.ToString("MMMyyyy") + "'" +
                " AND CM_VILLAGE IS NOT NULL AND CM_VILLAGE<>'' AND CBD_DISTANCE<>'' AND CBD_TO_TYPE='SP' ";
            DataSet dssPS = objSQLDB.ExecuteDataSet(sqlSps);
            foreach (DataRow dataRow in dssPS.Tables[0].Rows)
            {
                if (dataRow["CBD_TO_BRANCH_OR_CAMP_CODE"] + "" != "")
                {
                    NewCheckboxListItem oclBox = new NewCheckboxListItem();
                    oclBox.Tag = dataRow["CBD_TO_BRANCH_OR_CAMP_CODE"].ToString();
                    oclBox.Text = dataRow["BRANCH_NAME"].ToString();
                    chkStockPoints.Items.Add(oclBox);
                    oclBox = null;
                }
            }
        }
        private void clbCampLst_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            chkStockPoints.Items.Clear();

            for (int i = 0; i < clbCampLst.Items.Count; i++)
            {
                if (e.Index != i)
                    clbCampLst.SetItemCheckState(i, CheckState.Unchecked);
            }
            if (e.NewValue == CheckState.Checked)
                GetStockPoints();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMap_Click(object sender, EventArgs e)
        {
            string Start = ((SSAdmin.NewCheckboxListItem)(clbCampLst.SelectedItem)).Tag.ToString();
            string end = ((SSAdmin.NewCheckboxListItem)(chkStockPoints.SelectedItem)).Tag.ToString();
            GoogleReport oGoogleMap = new GoogleReport(Start, end);
            oGoogleMap.ShowDialog();
        }

        private void chkStockPoints_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int i = 0; i < chkStockPoints.Items.Count; i++)
            {
                if (e.Index != i)
                    chkStockPoints.SetItemCheckState(i, CheckState.Unchecked);
            }
        }
    }
}
