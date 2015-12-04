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
    
    public partial class frmKnownLanguages : Form
    {
        public frmApplication objApplication = null;
        DataRow[] drs;
        private SQLDB objSQLDB = null;
        public frmKnownLanguages()
        {
            InitializeComponent();
        }
        public frmKnownLanguages(DataRow[] dr)
        {
            InitializeComponent();
            drs = dr;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void frmKnownLanguages_Load(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            string strSQL1 = "select HALM_LANGUAGE DISPLAY,HALM_LANGUAGE VALUE from HR_APPL_LANGUAGE_MASTER ORDER BY HALM_LANGUAGE ASC";
            DataTable dt1 = objSQLDB.ExecuteDataSet(strSQL1, CommandType.Text).Tables[0];
            UtilityLibrary.PopulateControl(cmbLanguage, dt1.DefaultView, 1, 0);
            cmbReadFlag.SelectedIndex = 0;
            cmbSpeakFlag.SelectedIndex = 0;
            cmbWriteFlag.SelectedIndex = 0;
            if (drs != null)
            {
                cmbLanguage.SelectedValue = drs[0][1].ToString();
                cmbReadFlag.SelectedItem = drs[0][2].ToString();
                cmbSpeakFlag.SelectedItem = drs[0][4].ToString();
                cmbWriteFlag.SelectedItem = drs[0][3].ToString();
                
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < ((frmApplication)objApplication).dtLanguages.Rows.Count; i++)
            {
                if (((frmApplication)objApplication).dtLanguages.Rows[i]["Language"].ToString() == cmbLanguage.SelectedValue.ToString())
                {
                    this.Close();
                    return;
                }
            }
            //bellow line for delete the row in dtEducation table
            if (drs != null)
                ((frmApplication)objApplication).dtLanguages.Rows.Remove(drs[0]);
            //till here
            //dtLanguages.Rows.Count
            ((frmApplication)objApplication).dtLanguages.Rows.Add(new Object[] { ((frmApplication)objApplication).dtLanguages.Rows.Count+1, cmbLanguage.SelectedValue, cmbReadFlag.Text, cmbWriteFlag.Text, cmbSpeakFlag.Text });
            ((frmApplication)objApplication).GetDGVLanguageDetails();
            this.Close();
        }
    }
}
