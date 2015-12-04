using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;

namespace SSCRM
{
    public partial class EyeSurgeryList : Form
    {
        SQLDB objSQLdb = null;

        public EyeSurgeryPatientDetails objEyeSurgeryPatientDetails = null;

        public EyeSurgeryList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void EyeSurgeryList_Load(object sender, EventArgs e)
        {
            gvSurgeryList.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                     System.Drawing.FontStyle.Regular);
            FillSurgeryList();
        }

        public void FillSurgeryList()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            gvSurgeryList.Rows.Clear();
            try
            {
                dt = objSQLdb.ExecuteDataSet("SurgeryList_Get",CommandType.StoredProcedure).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {                       
                        gvSurgeryList.Rows.Add();

                        gvSurgeryList.Rows[i].Cells["SLNO"].Value=(i+1).ToString();
                        gvSurgeryList.Rows[i].Cells["TrnNo"].Value = dt.Rows[i]["TrnNo"].ToString();
                        gvSurgeryList.Rows[i].Cells["MrdNo"].Value = dt.Rows[i]["MRD_No"].ToString();
                        gvSurgeryList.Rows[i].Cells["PatientName"].Value = dt.Rows[i]["Patient_Name"].ToString();
                        gvSurgeryList.Rows[i].Cells["Eye"].Value = dt.Rows[i]["Eye"].ToString();
                        gvSurgeryList.Rows[i].Cells["Hospital"].Value = dt.Rows[i]["Hosp_Name"].ToString();
                        gvSurgeryList.Rows[i].Cells["SurgeryDate"].Value = Convert.ToDateTime(dt.Rows[i]["Surgery_Date"]).ToShortDateString();
                    }

                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
                dt = null;
            }
        }

        private void gvSurgeryList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 iTrnNo = Convert.ToInt32(gvSurgeryList.Rows[e.RowIndex].Cells["TrnNo"].Value);
            if (e.ColumnIndex == gvSurgeryList.Columns["Edit"].Index)
            {
                EyeSurgeryPatientDetails SurgeryDetails = new EyeSurgeryPatientDetails(iTrnNo);
                SurgeryDetails.objEyeSurgeryList = this;
                SurgeryDetails.Show();
                
            }
            



        }
    }
}
