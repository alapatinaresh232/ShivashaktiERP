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
using SSTrans;
using SSCRMDB;

namespace SSCRM
{
    public partial class AfterTrainingEmpPerf : Form
    {
        SQLDB objSQLdb = null;
        HRInfo objHRdb = null;
        string strPrograms = "", sFormType="";

        public AfterTrainingEmpPerf()
        {
            InitializeComponent();
        }
        public AfterTrainingEmpPerf(string sType)
        {
            InitializeComponent();
            sFormType = sType;
        }
        private void AfterTrainingEmpPerf_Load(object sender, EventArgs e)
        {
            if (sFormType == "EMP_PERF")
            {

                FillTrainerDetails();

                if (CommonData.LogUserId.ToUpper() == "ADMIN")
                {
                    cbTrainerNames.Enabled = true;
                }
                else
                {
                    cbTrainerNames.SelectedValue = CommonData.LogUserId;
                    cbTrainerNames.Enabled = false;
                }
                chkAllPrgs.Visible = false;
            }
            else if (sFormType == "ACTUAL_PROGRAMS")
            {
                this.Text = "Actual Training Program Details";
                cbTrainerNames.Visible = false;
                lblName.Text = "Trainer Details";
                FillActualTrainerDetails();
                grpMonths.Visible = false;
                lblTrName.Visible = false;
            }
            else if (sFormType == "PLAN_PROGRAMS")
            {
                this.Text = "Planning Program Details";
                cbTrainerNames.Visible = false;
                lblTrName.Visible = false;
                lblName.Text = "Trainer Details";
                FillPlanningTrainerDetails();
                grpMonths.Visible = false;
            }
            dtpFromdate.Value = DateTime.Today;
            dtpToDate.Value = DateTime.Today;
            chkAllPrgs.Visible = false;
            btnReport.Enabled = false;
           
        }

        private void FillTrainerDetails()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
            cbTrainerNames.DataBindings.Clear();
            try
            {

                strCommand = "SELECT  DISTINCT(MEMBER_NAME+'-'+CAST(HTAH_TRAINER_ECODE AS VARCHAR)) EmpName ,HTAH_TRAINER_ECODE EmpEcode " +
                                " FROM HR_TRAINING_ACTUAL_HEAD " +
                                " inner JOIN EORA_MASTER ON ECODE=HTAH_TRAINER_ECODE " +
                                " WHERE HTAH_TRAINER_FLAG='INTERNAL' " +
                                " ORDER BY EmpName asc";
                                 
                if (strCommand.Length > 5)
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                UtilityLibrary.AutoCompleteComboBox(cbTrainerNames, dt, "", "EmpName");

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

        private void FillActualTrainerDetails()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            clbPrograms.Items.Clear();
            string strCommand = "";
            
                try
                {
                    strCommand = "SELECT  DISTINCT(CAST(HTAH_TRAINER_ECODE AS VARCHAR)+'-'+MEMBER_NAME) EmpName "+
                                ", HTAH_TRAINER_ECODE EmpEcode " +
                                " FROM HR_TRAINING_ACTUAL_HEAD " +
                                " inner JOIN EORA_MASTER ON ECODE=HTAH_TRAINER_ECODE " +
                                " WHERE HTAH_TRAINER_FLAG='INTERNAL' " +
                                " and HTAH_ACTUAL_PROGRAM_FROM_DATE BETWEEN '"+ dtpFromdate.Value.ToString("dd/MMM/yyyy") +
                                "' AND '" + dtpToDate.Value.ToString("dd/MMM/yyyy") + "' ORDER BY EmpName asc";

                    if (strCommand.Length > 5)
                        dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        chkAllPrgs.Visible = true;
                        btnReport.Enabled = true;
                        foreach (DataRow dataRow in dt.Rows)
                        {
                            NewCheckboxListItem oclBox = new NewCheckboxListItem();
                            oclBox.Text = dataRow["EmpName"].ToString();
                            oclBox.Tag = dataRow["EmpEcode"].ToString();

                            clbPrograms.Items.Add(oclBox);
                            oclBox = null;
                        }

                    }
                    else
                    {
                        btnReport.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            

        }

        private void FillPlanningTrainerDetails()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            clbPrograms.Items.Clear();
            string strCommand = "";

            try
            {
                strCommand = "SELECT  DISTINCT(CAST(HTPH_TRAINER_ECODE AS VARCHAR)+'-'+MEMBER_NAME) EmpName ,HTPH_TRAINER_ECODE EmpEcode " +
                           " FROM HR_TRAINING_PLANNER_HEAD " +
                           " inner JOIN EORA_MASTER ON ECODE=HTPH_TRAINER_ECODE " +
                           " WHERE HTPH_TRAINER_FLAG='INTERNAL' " +
                           " and HTPH_PROGRAM_FROM_DATE BETWEEN '" + dtpFromdate.Value.ToString("dd/MMM/yyyy") +
                           "' AND '" + dtpToDate.Value.ToString("dd/MMM/yyyy") + "' ORDER BY EmpName asc";

                if (strCommand.Length > 5)
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    chkAllPrgs.Visible = true;
                    btnReport.Enabled = true;
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Text = dataRow["EmpName"].ToString();
                        oclBox.Tag = dataRow["EmpEcode"].ToString();

                        clbPrograms.Items.Add(oclBox);
                        oclBox = null;
                    }

                }
                else
                {
                    btnReport.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }


        private void FillTrainingProgramDetl(Int32 iEcode)
        {
            objHRdb = new HRInfo();
            DataTable dt = new DataTable();
            clbPrograms.Items.Clear();
            if (cbTrainerNames.SelectedIndex > 0)
            {
                try
                {
                    dt = objHRdb.GetTrainingProgramsByTrainer(iEcode, "", Convert.ToDateTime(dtpFromdate.Value).ToString("dd/MMM/yyyy"), Convert.ToDateTime(dtpToDate.Value).ToString("dd/MMM/yyyy"), "ACTUAL").Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        //chkAllPrgs.Visible = true;
                        btnReport.Enabled = true;
                        foreach (DataRow dataRow in dt.Rows)
                        {
                            NewCheckboxListItem oclBox = new NewCheckboxListItem();
                            oclBox.Text = dataRow["PrgNo"].ToString() + '-' + dataRow["PrgName"].ToString() + '(' + dataRow["Prglocation"].ToString() + ')';
                            oclBox.Tag = dataRow["PrgNo"].ToString();

                            clbPrograms.Items.Add(oclBox);
                            oclBox = null;
                        }

                    }
                    else
                    {
                        btnReport.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }


        private void cbTrainerNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 nEcode = 0;

            if (cbTrainerNames.SelectedIndex > 0)
            {
                nEcode = Convert.ToInt32(((System.Data.DataRowView)(cbTrainerNames.SelectedItem)).Row.ItemArray[1].ToString());

                FillTrainingProgramDetl(Convert.ToInt32(cbTrainerNames.SelectedValue));
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

       
     
        private void btnClear_Click(object sender, EventArgs e)
        {
            //dtpFromdate.Value = DateTime.Today;
            //dtpToDate.Value = DateTime.Today;
            //txtTrBeforeMonths.Text = "";           
            //cbTrainerNames.SelectedIndex = 0;
            //chkAllPrgs.Checked = false;
            //clbPrograms.Items.Clear();
        }

        private void chkAllPrgs_CheckedChanged(object sender, EventArgs e)
        {
            strPrograms = "";
            if (chkAllPrgs.Checked == true)
            {
                for (int i = 0; i < clbPrograms.Items.Count; i++)
                {
                    clbPrograms.SetItemCheckState(i, CheckState.Checked);                    
                    strPrograms += ((NewCheckboxListItem)clbPrograms.CheckedItems[i]).Tag +',';
                }
                strPrograms = strPrograms.TrimEnd(',');
            }
            else
            {
                for (int i = 0; i < clbPrograms.Items.Count; i++)
                {
                    clbPrograms.SetItemCheckState(i, CheckState.Unchecked);
                    strPrograms = "";

                }
            }
        }

        private bool CheckData()
        {
            bool flag = true;
            if (sFormType == "EMP_PERF")
            {
                if (cbTrainerNames.SelectedIndex == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Select Trainer Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbTrainerNames.Focus();
                    return flag;
                }
                if (clbPrograms.Items.Count == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Select Valid From Date and ToDate", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtpFromdate.Focus();
                    return flag;
                }

                if (clbPrograms.CheckedItems.Count == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Select Atleast One Program", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clbPrograms.Focus();
                    return flag;
                }
                if (txtTrBeforeMonths.Text.Length == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Enter Before Training Months", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTrBeforeMonths.Focus();
                    return flag;
                }
                if (txtAftTrMonths.Text.Length == 0 || txtAftTrMonths.Text == "0")
                {
                    flag = false;
                    MessageBox.Show("Please Enter After Training Months", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAftTrMonths.Focus();
                    return flag;
                }
            }
            else
            {
                if (clbPrograms.Items.Count == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Select Valid From Date and ToDate", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtpFromdate.Focus();
                    return flag;
                }

                if (clbPrograms.CheckedItems.Count == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Select Atleast One Trainer", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clbPrograms.Focus();
                    return flag;
                }
            }

            
            return flag;

        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            strPrograms = "";
            if (CheckData() == true)
            {
                GetSelectedPrograms();
                if (sFormType == "EMP_PERF")
                {
                    CommonData.ViewReport = "AFTER_TRAINING_EMP_PERFORMANCE";
                    ReportViewer childReportViewer = new ReportViewer("", "", "", "", "", "", strPrograms, "", Convert.ToInt32(txtTrBeforeMonths.Text), Convert.ToInt32(txtAftTrMonths.Text));
                    childReportViewer.Show();
                }
                else if (sFormType == "ACTUAL_PROGRAMS")
                {
                    CommonData.ViewReport = "ACTUAL_TRAINING_PROGRAM_DETAILS";
                    ReportViewer childReportViewer = new ReportViewer("", "", strPrograms, dtpFromdate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), "ACTUAL_PROGRAMS");
                    childReportViewer.Show();
                }
                else if (sFormType == "PLAN_PROGRAMS")
                {
                    CommonData.ViewReport = "TRAINER_PROGRAM_DETAILS";
                    ReportViewer childReportViewer = new ReportViewer("", "", strPrograms, dtpFromdate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), "PLANNING_PROGRAMS");
                    childReportViewer.Show();
                }
            }
        }

        private void clbPrograms_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSelectedPrograms();
        }

        private void GetSelectedPrograms()
        {
            strPrograms = "";

            if (clbPrograms.Items.Count > 0)
            {
                for (int iVar = 0; iVar < clbPrograms.CheckedItems.Count; iVar++)
                {
                    strPrograms += ((NewCheckboxListItem)clbPrograms.CheckedItems[iVar]).Tag + ',';
                }

                strPrograms = strPrograms.TrimEnd(',');
            }
        }

  

        private void txtTrBeforeMonths_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtAftTrMonths_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void dtpFromdate_ValueChanged(object sender, EventArgs e)
        {
            if (sFormType == "EMP_PERF")
            {
                if (dtpFromdate.Value < dtpToDate.Value && cbTrainerNames.SelectedIndex > 0)
                {
                    FillTrainingProgramDetl(Convert.ToInt32(cbTrainerNames.SelectedValue));
                }
            }
            if (sFormType == "ACTUAL_PROGRAMS")
            {
                FillActualTrainerDetails();
            }
            if (sFormType == "PLAN_PROGRAMS")
            {
                FillPlanningTrainerDetails();
            }
        }

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            if (sFormType == "EMP_PERF")
            {
                if (dtpFromdate.Value < dtpToDate.Value && cbTrainerNames.SelectedIndex > 0)
                {
                    FillTrainingProgramDetl(Convert.ToInt32(cbTrainerNames.SelectedValue));
                }
            }
            if (sFormType == "ACTUAL_PROGRAMS")
            {
                FillActualTrainerDetails();
            }
            if (sFormType == "PLAN_PROGRAMS")
            {
                FillPlanningTrainerDetails();
            }

        }

        private void clbPrograms_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (sFormType == "EMP_PERF")
            {
                for (int i = 0; i < clbPrograms.Items.Count; i++)
                {
                    if (e.Index != i)
                        clbPrograms.SetItemCheckState(i, CheckState.Unchecked);
                }
            }
        }

      

    }
}
