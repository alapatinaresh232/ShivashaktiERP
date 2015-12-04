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
    public partial class TrainingReports : Form
    {
        SQLDB objSQLdb = null;
        HRInfo objHRdb = null;

        public TrainingReports()
        {
            InitializeComponent();
        }

        private void TrainingReports_Load(object sender, EventArgs e)
        {
            cbTrainerType.SelectedIndex = 0;
            dtpFromdate.Value = DateTime.Today;
            dtpToDate.Value = DateTime.Today;

            cbPrgType.SelectedIndex = 0;
            FillInternalTrainerDetails();

        }

        private void FillInternalTrainerDetails()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
            cbTrainerNames.DataBindings.Clear();
            try
            {
                if (cbPrgType.SelectedIndex == 0)
                {
                    strCommand = "SELECT  DISTINCT(CAST(HTAH_TRAINER_ECODE AS VARCHAR)+'-'+MEMBER_NAME) EmpName ,HTAH_TRAINER_ECODE EmpEcode " +
                                " FROM HR_TRAINING_ACTUAL_HEAD " +
                                " LEFT JOIN EORA_MASTER ON ECODE=HTAH_TRAINER_ECODE " +
                                " WHERE HTAH_TRAINER_FLAG='INTERNAL' " +
                                " ORDER BY EmpName ";
                }
                else if (cbPrgType.SelectedIndex == 1)
                {
                    strCommand = "SELECT DISTINCT(CAST(HTPH_TRAINER_ECODE AS VARCHAR)+'-'+MEMBER_NAME) EmpName ,HTPH_TRAINER_ECODE EmpEcode "+
                                " FROM HR_TRAINING_PLANNER_HEAD  "+
                                " LEFT JOIN EORA_MASTER ON ECODE=HTPH_TRAINER_ECODE "+
                                " WHERE HTPH_TRAINER_FLAG='INTERNAL' "+
                                " ORDER BY EmpName";
                }

                if(strCommand.Length>5)
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "0";

                    dt.Rows.InsertAt(dr, 0);

                    cbTrainerNames.DataSource = dt;
                    cbTrainerNames.DisplayMember = "EmpName";
                    cbTrainerNames.ValueMember = "EmpEcode";

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

        private void FillExternalTrainerDetails()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
            cbTrainerNames.DataBindings.Clear();
            try
            {
                if (cbPrgType.SelectedIndex == 0)
                {
                    strCommand = "SELECT  DISTINCT(HTAH_EXTERNAL_TRAINER_NAME) TrainerName " +
                                 " FROM HR_TRAINING_ACTUAL_HEAD " +
                                 " WHERE HTAH_TRAINER_FLAG='EXTERNAL' " +
                                 " ORDER BY TrainerName";
                }
                else if (cbPrgType.SelectedIndex == 1)
                {
                    strCommand = "SELECT DISTINCT(HTPH_EXTERNAL_TRAINER_NAME) TrainerName "+
                                 " FROM HR_TRAINING_PLANNER_HEAD "+
                                 " WHERE HTPH_TRAINER_FLAG='EXTERNAL' "+
                                 "ORDER BY TrainerName";
                }


                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbTrainerNames.DataSource = dt;
                    cbTrainerNames.DisplayMember = "TrainerName";
                    cbTrainerNames.ValueMember = "TrainerName";

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


        private void FillTrainingProgramDetl(Int32 iEcode,string sTrainerName)
        {
            objHRdb = new HRInfo();
            DataTable dt = new DataTable();
            gvTrainingPrgDetl.Rows.Clear();

            if (cbTrainerNames.SelectedIndex > 0)
            {
                try
                {
                    if (cbPrgType.SelectedIndex == 0)
                    {
                        dt = objHRdb.GetTrainingProgramsByTrainer(iEcode, sTrainerName, Convert.ToDateTime(dtpFromdate.Value).ToString("dd/MMM/yyyy"), Convert.ToDateTime(dtpToDate.Value).ToString("dd/MMM/yyyy"), "ACTUAL").Tables[0];
                    }
                    else
                    {
                        dt = objHRdb.GetTrainingProgramsByTrainer(iEcode, sTrainerName, Convert.ToDateTime(dtpFromdate.Value).ToString("dd/MMM/yyyy"), Convert.ToDateTime(dtpToDate.Value).ToString("dd/MMM/yyyy"), "PLAN").Tables[0];
                    }

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            gvTrainingPrgDetl.Rows.Add();
                            gvTrainingPrgDetl.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                            gvTrainingPrgDetl.Rows[i].Cells["PrgNo"].Value = dt.Rows[i]["PrgNo"].ToString();
                            gvTrainingPrgDetl.Rows[i].Cells["PrgName"].Value = dt.Rows[i]["PrgName"].ToString();
                            gvTrainingPrgDetl.Rows[i].Cells["Location"].Value = dt.Rows[i]["Prglocation"].ToString();
                            gvTrainingPrgDetl.Rows[i].Cells["EmpCnt"].Value = dt.Rows[i]["EmpCnt"].ToString();
                            gvTrainingPrgDetl.Rows[i].Cells["TopicsCnt"].Value = dt.Rows[i]["TopicsCnt"].ToString();

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }

        private void cbTrainerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTrainerType.Text.Equals("EXTERNAL"))
            {
                FillExternalTrainerDetails();
            }
            else if (cbTrainerType.Text.Equals("INTERNAL"))
            {
                FillInternalTrainerDetails();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void cbTrainerNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTrainerNames.SelectedIndex > 0)
            {

                if (cbTrainerType.Text.Equals("EXTERNAL"))
                {
                    FillTrainingProgramDetl(0, cbTrainerNames.SelectedValue.ToString());
                }
                else
                {
                    //Convert.ToInt32(((System.Data.DataRowView)(cbTrainerNames.SelectedItem)).Row.ItemArray[1].ToString()
                    FillTrainingProgramDetl(Convert.ToInt32(cbTrainerNames.SelectedValue), "");
                }
            }
        }

        private void dtpFromdate_ValueChanged(object sender, EventArgs e)
        {
            if (cbTrainerNames.SelectedIndex > 0)
            {

                if (cbTrainerType.Text.Equals("EXTERNAL"))
                {
                    FillTrainingProgramDetl(0, cbTrainerNames.SelectedValue.ToString());
                }
                else
                {
                    //Convert.ToInt32(((System.Data.DataRowView)(cbTrainerNames.SelectedItem)).Row.ItemArray[1].ToString()
                    FillTrainingProgramDetl(Convert.ToInt32(cbTrainerNames.SelectedValue), "");
                }
            }
        }

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            if (cbTrainerNames.SelectedIndex > 0)
            {

                if (cbTrainerType.Text.Equals("EXTERNAL"))
                {
                    FillTrainingProgramDetl(0, cbTrainerNames.SelectedValue.ToString());
                }
                else
                {
                    //Convert.ToInt32(((System.Data.DataRowView)(cbTrainerNames.SelectedItem)).Row.ItemArray[1].ToString()
                    FillTrainingProgramDetl(Convert.ToInt32(cbTrainerNames.SelectedValue), "");
                }
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (cbTrainerNames.SelectedIndex > 0)
            {
                CommonData.ViewReport = "TRAINING_PLANNING_DETAILS";
                ReportViewer childReportViewer = new ReportViewer("", "", cbTrainerNames.SelectedValue.ToString(), Convert.ToDateTime(dtpFromdate.Value).ToString("dd/MMM/yyyy"), Convert.ToDateTime(dtpToDate.Value).ToString("dd/MMM/yyyy"), "");
                childReportViewer.Show();
            }
            else
            {
                CommonData.ViewReport = "TRAINING_PLANNING_DETAILS";
                ReportViewer childReportViewer = new ReportViewer("", "", "0", Convert.ToDateTime(dtpFromdate.Value).ToString("dd/MMM/yyyy"), Convert.ToDateTime(dtpToDate.Value).ToString("dd/MMM/yyyy"), "");
                childReportViewer.Show();
            }

        }

        private void gvTrainingPrgDetl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == gvTrainingPrgDetl.Columns["Print"].Index)
                {
                    if (cbPrgType.SelectedIndex == 0)
                    {
                        CommonData.ViewReport = "SSCRM_REP_TRAINING_DETL_BY_TRAINER";
                        ReportViewer childReportViewer = new ReportViewer(gvTrainingPrgDetl.Rows[e.RowIndex].Cells["PrgNo"].Value.ToString());
                        childReportViewer.Show();
                    }
                    else
                    {
                        CommonData.ViewReport = "SSCRM_REP_TRAINING_PLAN_DETL_BY_TRAINER";
                        ReportViewer childReportViewer = new ReportViewer(gvTrainingPrgDetl.Rows[e.RowIndex].Cells["PrgNo"].Value.ToString());
                        childReportViewer.Show();
                        
                    }
                }
            }
        }

        private void cbPrgType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTrainerType.SelectedIndex == 0)
            {
                gvTrainingPrgDetl.Rows.Clear();
                FillInternalTrainerDetails();
            }
            else
            {
                gvTrainingPrgDetl.Rows.Clear();
                FillExternalTrainerDetails();
            }
        }



    }
}
