using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using System.Diagnostics;
using System.Configuration;
using SSCRMDB;
using SSAdmin;
using SSTrans;
using SSCRM.App_Code;
using System.IO;

namespace SSCRM
{
    public partial class EmployeeInfo : Form
    {
        HRInfo oHRInfo;
        SQLDB objSQLDB;
        public EmployeeInfo()
        {
            InitializeComponent();
        }
        private void EmployeeInfo_Load(object sender, EventArgs e)
        {

        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (txtEcodeSearch.Text != "")
            {
                oHRInfo = new HRInfo();
                Reports.Employee.DSEmployee oDSEmployee = new SSCRM.Reports.Employee.DSEmployee();
                DataSet ds = oHRInfo.GetEoraCode(Convert.ToInt32(txtEcodeSearch.Text));
                
                CommonData.ViewReport = "SSHR_HRINFORMATION";
                oDSEmployee.MainHead1.Merge(ds.Tables[0]);
                oDSEmployee.Education.Merge(ds.Tables[1]);
                oDSEmployee.ShortCourse.Merge(ds.Tables[2]);
                oDSEmployee.Curricular.Merge(ds.Tables[3]);
                oDSEmployee.Family.Merge(ds.Tables[4]);
                oDSEmployee.Reference.Merge(ds.Tables[5]);
                oDSEmployee.Experience.Merge(ds.Tables[6]);
                oDSEmployee.Documents.Merge(ds.Tables[8]);
                oDSEmployee.Induction.Merge(ds.Tables[9]);
                oDSEmployee.EORA.Merge(ds.Tables[10]);
                DataTable dtD = new DataTable();
                dtD.Columns.Add("CM_COMPANY_NAME");
                dtD.Columns.Add("BRANCH_NAME");
                if (ds.Tables[11].Rows.Count > 0)
                    dtD.Rows.Add(new Object[] { ds.Tables[11].Rows[0]["CM_COMPANY_NAME"], ds.Tables[11].Rows[0]["BRANCH_NAME"] });
                oDSEmployee.MainHead11.Merge(dtD);
                ReportViewer chldRV = new ReportViewer(oDSEmployee);
                chldRV.Show();
            }
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text.ToString().Length > 4)
            {
                if (txtEcodeSearch.Text != "")
                {
                    DataTable dt = new DataTable();
                    objSQLDB = new SQLDB();
                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = objSQLDB.CreateParameter("@EORACODE", DbType.String, txtEcodeSearch.Text.ToString(), ParameterDirection.Input);
                    //dt = objSQLDB.ExecuteDataSet("GetEORADataforEORAMaster", CommandType.StoredProcedure, param).Tables[0];
                    //if (dt.Rows.Count == 0)
                    //{
                    dt = objSQLDB.ExecuteDataSet("GetHRISDataforEORAMas", CommandType.StoredProcedure, param).Tables[0];
                    //}
                    //DataView dvFilter = dsHrisEora.Tables[0].DefaultView;
                    //dvFilter.RowFilter = "ID=" + txtEcodeSearch.Text; 
                    //dt = dvFilter.ToTable();
                    //dt = dsHrisEora.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtEORACode.Text = dt.Rows[0]["EORA_CODE"].ToString();
                        txtMemberName.Text = dt.Rows[0]["MEMBER_NAME"].ToString();
                        txtFatherName.Text = dt.Rows[0]["FATHER_NAME"].ToString();
                        meHRISDataofBirth.Text = Convert.ToDateTime(dt.Rows[0]["DOB"]).ToString("dd/MM/yyyy");
                        meHRISDateOfJoin.Text = Convert.ToDateTime(dt.Rows[0]["DOJ"]).ToString("dd/MM/yyyy");
                        txtHRISDesig.Text = dt.Rows[0]["DESIG_NAME"].ToString();
                        txtHRISBranch.Text = dt.Rows[0]["BRANCH_NAME"].ToString();
                        objSQLDB = new SQLDB();
                        DataSet dsPhoto = objSQLDB.ExecuteDataSet("SELECT HAPS_PHOTO_SIG FROM HR_APPL_PHOTO_SIG WHERE HAPS_APPL_NUMBER=" + txtEcodeSearch.Text);

                        if (dsPhoto.Tables[0].Rows.Count > 0)
                            GetImage((byte[])dsPhoto.Tables[0].Rows[0]["HAPS_PHOTO_SIG"]);
                        else
                            picEmpPhoto.BackgroundImage = global::SSCRM.Properties.Resources.nomale;

                        objSQLDB = null;
                        DateTime zeroTime = new DateTime(1, 1, 1);
                        DateTime a = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy,MM,dd"));
                        DateTime b = Convert.ToDateTime(dt.Rows[0]["DOB"]);
                        TimeSpan ival = a - b;
                        int years = (zeroTime + ival).Year - 1;
                        txtHRISAge.Text = years.ToString();

                    }
                    else
                    {
                        txtEcodeSearch.Focus();
                    }
                    objSQLDB = null;

                }
                else
                {
                    txtEcodeSearch.Focus();
                }
            }
            else
            {
                txtEORACode.Text = "";
                txtMemberName.Text = "";
                txtFatherName.Text = "";
                meHRISDataofBirth.Text = "";
                meHRISDateOfJoin.Text = "";
                txtHRISDesig.Text = "";
                txtHRISBranch.Text = "";
            }
        }
        public void GetImage(byte[] imageData)
        {
            try
            {
                Image newImage;
                using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                {
                    ms.Write(imageData, 0, imageData.Length);
                    newImage = Image.FromStream(ms, true);
                }
                picEmpPhoto.BackgroundImage = newImage;
                this.picEmpPhoto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
            }
        }

    }
}
