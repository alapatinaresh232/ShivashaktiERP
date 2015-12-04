using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSTrans;

namespace SSCRM
{
    public partial class ServicesDailyActivitiesPrint : Form
    {
        SQLDB objSQLdb = null;
        ServiceDeptDB objServiceDB = null;

        public ServicesDailyActivitiesPrint()
        {
            InitializeComponent();
        }

        private void ServicesDailyActivitiesPrint_Load(object sender, EventArgs e)
        {
            //FillDocMonthsData();
            dtpDocMonth.Value =Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
            FillEmployeeData();
           
        }


        private void FillEmployeeData()
        {
            objServiceDB = new ServiceDeptDB();
            DataTable dt = new DataTable();           
            gvEmpDetails.Rows.Clear();
            try
            {
                dt = objServiceDB.GetServiceMappedEcodes(CommonData.CompanyCode, CommonData.BranchCode, dtpDocMonth.Value.ToString("MMMyyyy")).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        gvEmpDetails.Rows.Add();
                        gvEmpDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                        gvEmpDetails.Rows[i].Cells["Ecode"].Value = dt.Rows[i]["ECODE"].ToString();
                        gvEmpDetails.Rows[i].Cells["EmpName"].Value = dt.Rows[i]["ENAME"].ToString();
                        gvEmpDetails.Rows[i].Cells["Desig"].Value = dt.Rows[i]["desig_name"].ToString();
                        gvEmpDetails.Rows[i].Cells["Dept"].Value = dt.Rows[i]["dept_name"].ToString();
                      
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objServiceDB = null;
                dt = null;
            }
        }

     

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void dtpDocMonth_ValueChanged(object sender, EventArgs e)
        {
            FillEmployeeData();
        }
      

        private void gvEmpDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == gvEmpDetails.Columns["Print"].Index)
                {
                    CommonData.ViewReport = "SSCRM_REP_SERVICES_EMP_DAILY_ACTIVITIES";
                    ReportViewer childReportViewer = new ReportViewer(Convert.ToInt32(gvEmpDetails.Rows[e.RowIndex].Cells["Ecode"].Value), dtpDocMonth.Value.ToString("MMMyyyy").ToUpper());
                    childReportViewer.Show();
                }
            }
        }

       

        //private void FillDocMonthsData()
        //{
        //    objSQLdb = new SQLDB();
        //    DataTable dt = new DataTable();
        //    try
        //    {

        //        string strCmd = "SELECT document_month FROM document_month " +
        //                        " WHERE company_code='" + CommonData.CompanyCode +
        //                        "' ORDER BY start_date ,end_date";
        //        dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];


        //        if (dt.Rows.Count > 0)
        //        {
        //            DataRow row = dt.NewRow();
        //            row[0] = "--Select--";

        //            dt.Rows.InsertAt(row, 0);

        //            cbDocMonth.DataSource = dt;
        //            cbDocMonth.DisplayMember = "document_month";
        //            cbDocMonth.ValueMember = "document_month";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        objSQLdb = null;
        //        dt = null;
        //    }
        //}

    
    }
}
