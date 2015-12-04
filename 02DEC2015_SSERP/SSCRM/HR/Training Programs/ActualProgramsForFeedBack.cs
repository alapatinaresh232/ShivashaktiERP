using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SSCRMDB;

namespace SSCRM
{
    public partial class ActualProgramsForFeedBack : Form
    {
        SQLDB objSQLdb = null;

        public ActualProgramsForFeedBack()
        {
            InitializeComponent();
        }

        private void ActualProgramsForFeedBack_Load(object sender, EventArgs e)
        {
            dtpPrgFromDate.Value = DateTime.Today;
            dtpPrgToDate.Value = DateTime.Today;
        }

        private DataSet GetTrainingProgramNames(int iMode,string FromDate,string ToDate,string UserId,string sPrgNo,string sEcodeSearch)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@iMode", DbType.Int32, iMode, ParameterDirection.Input);

                param[1] = objSQLdb.CreateParameter("@xFromDate", DbType.String, FromDate, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xToDate", DbType.String, ToDate, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xUserId", DbType.String, UserId, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xPrgNo", DbType.String, sPrgNo, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xEcodeName", DbType.String, sEcodeSearch, ParameterDirection.Input);


                ds = objSQLdb.ExecuteDataSet("Get_TrainingProgramNamesForFeedBack", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }

        private void FillTrainingPrgNames()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbProgramName.DataSource = null;
            try
            {
                dt = GetTrainingProgramNames(101,Convert.ToDateTime(dtpPrgFromDate.Value).ToString("dd/MMM/yyyy"), Convert.ToDateTime(dtpPrgToDate.Value).ToString("dd/MMM/yyyy"),CommonData.LogUserId,"","").Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();

                    row[0] = "--Select--";
                    row[1] = "--Select--";

                    dt.Rows.InsertAt(row, 0);
                    
                    cbProgramName.DataSource = dt;
                    cbProgramName.DisplayMember = "PrgName";
                    cbProgramName.ValueMember = "TrainerName";

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

      

        private void btnAddFeedbackDetails_Click(object sender, EventArgs e)
        {
            string PrgName = "";
            string TrainerName = "";
            string PrgNumber = "";
            DateTime dtpPrgDate;

            if (cbProgramName.SelectedIndex > 0)
            {
                PrgName = cbProgramName.Text.ToString().Split('-')[1];
                PrgNumber = cbProgramName.Text.ToString().Split('-')[0];
                TrainerName = cbProgramName.SelectedValue.ToString().Split('@')[0];
                dtpPrgDate = Convert.ToDateTime(cbProgramName.SelectedValue.ToString().Split('@')[1]);


                TrainingFeedBackForm FeedBackDetl = new TrainingFeedBackForm(PrgNumber, PrgName, TrainerName, dtpPrgDate);
                FeedBackDetl.objActualProgramsForFeedBack = this;
                FeedBackDetl.ShowDialog();
            }
            else
            {
                if (cbProgramName.Items.Count >= 1)
                {
                    MessageBox.Show("Please Select Program Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbProgramName.Focus();
                }
                else if (cbProgramName.Items.Count == 0)
                {
                    MessageBox.Show("Please Select Valid Program Date", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtpPrgFromDate.Focus();

                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void dtpPrgFromDate_ValueChanged(object sender, EventArgs e)
        {
            FillTrainingPrgNames();
        }

        private void dtpPrgToDate_ValueChanged(object sender, EventArgs e)
        {
            FillTrainingPrgNames();
        }

    
      
    }
}
