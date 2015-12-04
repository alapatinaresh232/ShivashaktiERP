using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using SSCRMDB;
using SSTrans;
using SSCRM.App_Code;

namespace SSCRM
{
    public partial class TrainingProgramDetails : Form
    {
        SQLDB objSQLdb=null;
        HRInfo objHRdb = null;
        bool flagUpdate = false;

        public DataTable dtTopicMethodDetl = new DataTable();
        private string sFrmType = "";
       
        public TrainingProgramDetails()
        {
            InitializeComponent();
        }
        public TrainingProgramDetails(string sType)
        {
            InitializeComponent();
            sFrmType = sType;
        }

        private void TrainingProgramDetails_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            cbTrainerType.SelectedIndex = 0;
            chkOurPremises.Checked = true;
            GenerateProgramNumber();
            dtpPrgFrmDate.Value = DateTime.Today;
            dtpPlanningFrmDate.Value = DateTime.Today;
            dtpPlanningFrmDate.Enabled = false;
            dtpPlanToDate.Enabled = false;
            dtpPlanToDate.Value = DateTime.Today;
            dtpPrgToDate.Value = DateTime.Today;
            rdbAgPlanner.Checked = false;
            rdbNew.Checked = true;

            if (cbTrainerType.SelectedIndex == 0)
            {
                FillInternalTrainerDetails();
                //FillPlanningIdByTrainerEcode();
            }

            try
            {
                objSQLdb = new SQLDB();
                DataTable dt = objSQLdb.ExecuteDataSet("SELECT DISTINCT HTAH_ACTUAL_PROGRAM_NAME " +
                                                        " FROM HR_TRAINING_ACTUAL_HEAD " +
                                                        " ORDER BY HTAH_ACTUAL_PROGRAM_NAME ASC").Tables[0];
                UtilityLibrary.AutoCompleteTextBox(txtPrgName, dt, "", "HTAH_ACTUAL_PROGRAM_NAME");
                objSQLdb = null;
            }
            catch { }
            finally { objSQLdb = null; }                    


            #region "CREATE TOPIC_METHOD_DETAILS_TABLE"
            dtTopicMethodDetl.Columns.Add("SLNO");
            dtTopicMethodDetl.Columns.Add("TopicId");
            dtTopicMethodDetl.Columns.Add("TopicType");
            dtTopicMethodDetl.Columns.Add("TopicName");
            dtTopicMethodDetl.Columns.Add("MethodType");
            dtTopicMethodDetl.Columns.Add("TrainerType");
            dtTopicMethodDetl.Columns.Add("TrainerEcode");
            dtTopicMethodDetl.Columns.Add("TrainerName");
            dtTopicMethodDetl.Columns.Add("TrainerDetails");
            dtTopicMethodDetl.Columns.Add("InternalTrName");
            dtTopicMethodDetl.Columns.Add("EmpDesig");
            dtTopicMethodDetl.Columns.Add("StartTime");
            dtTopicMethodDetl.Columns.Add("EndTime");
            dtTopicMethodDetl.Columns.Add("Remarks");

            #endregion

          
        }

        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT DISTINCT CM_COMPANY_NAME ,CM_COMPANY_CODE " +
                                   " FROM USER_BRANCH " +
                                   " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                   " INNER JOIN COMPANY_MAS ON CM_COMPANY_CODE = COMPANY_CODE " +
                                   " WHERE UB_USER_ID ='" + CommonData.LogUserId +
                                   "' ORDER BY CM_COMPANY_NAME";

                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";
                    row[1] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cbCompany.DataSource = dt;
                    cbCompany.DisplayMember = "CM_COMPANY_NAME";
                    cbCompany.ValueMember = "CM_COMPANY_CODE";
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

        private void FillBranchData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbBranches.DataSource = null;
            try
            {
                if (cbCompany.SelectedIndex > 0)
                {

                    string strCommand = "SELECT DISTINCT BRANCH_NAME,BRANCH_CODE " +
                                                           " FROM USER_BRANCH " +
                                                           " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                                           " WHERE COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() +
                                                           "' AND UB_USER_ID ='" + CommonData.LogUserId +
                                                           "' ORDER BY BRANCH_NAME ASC";

                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbBranches.DataSource = dt;
                    cbBranches.DisplayMember = "BRANCH_NAME";
                    cbBranches.ValueMember = "BRANCH_CODE";
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

        private void GenerateProgramNumber()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                String strCommand = "SELECT ISNULL(MAX(cast(HTAH_ACTUAL_PROGRAM_NUMBER as numeric)),0)+1  FROM HR_TRAINING_ACTUAL_HEAD";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtProgramId.Text = dt.Rows[0][0] + "";
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
        
        private bool CheckDetails()
        {
            bool bflag = true;

            if (txtPrgName.Text.Length == 0)
            {
                bflag = false;
                MessageBox.Show("Please Enter Program Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPrgName.Focus();
                return bflag;
            }

            if (chkOurPremises.Checked == true)
            {
                if (cbCompany.SelectedIndex == 0)
                {
                    bflag = false;
                    MessageBox.Show("Please Select Company", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbCompany.Focus();
                    return bflag;
                }
                if (cbBranches.SelectedIndex == 0 || cbBranches.SelectedIndex == -1)
                {
                    bflag = false;
                    MessageBox.Show("Please Select Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbBranches.Focus();
                    return bflag;
                }
            }
            else
            {
                if (txtAddress1.Text.Length == 0)
                {
                    bflag = false;
                    MessageBox.Show("Please Enter Program Location Details", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAddress1.Focus();
                    return bflag;
                }
            }

            if (dtpPrgFrmDate.Value > DateTime.Today)
            {
                bflag = false;
                MessageBox.Show("Please Select Valid Program From Date", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpPrgFrmDate.Focus();
                return bflag;
            }
            if (dtpPrgToDate.Value > DateTime.Today)
            {
                bflag = false;
                MessageBox.Show("Please Select Valid Program To Date", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpPrgToDate.Focus();
                return bflag;
            }

            if (rdbAgPlanner.Checked == true)
            {
                if (cbTrainerName.SelectedIndex == 0 || cbTrainerName.SelectedIndex == -1)
                {
                    bflag = false;
                    MessageBox.Show("Please Select Trainer Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbTrainerName.Focus();
                    return bflag;
                }
                if (cbTrainerName.SelectedIndex > 0)
                {
                    if (cbPlannerId.SelectedIndex == 0 || cbPlannerId.SelectedIndex == -1)
                    {
                        bflag = false;
                        MessageBox.Show("Please Select Planner Id", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbPlannerId.Focus();
                        return bflag;
                    }
                }
            }

            if (cbTrainerType.SelectedIndex == 0)
            {
                if (txtEName.Text.Length == 0)
                {
                    bflag = false;
                    MessageBox.Show("Please Enter Trainer Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEcodeSearch.Focus();
                    return bflag;
                }

            }
            if (cbTrainerType.SelectedIndex == 1)
            {
                if (txtTrainerName.Text.Length == 0)
                {
                    bflag = false;
                    MessageBox.Show("Please Enter Trainer Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTrainerName.Focus();
                    return bflag;
                }
            }
           

          
            return bflag;
        }

        private bool CheckData()
        {
            bool flag = true;

            if (gvTopicsMethodDetl.Rows.Count == 0)
            {
                flag = false;
                MessageBox.Show("Please Add Training Topic Methodology Details", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;
            }
            if (gvEmpDetails.Rows.Count == 0)
            {
                flag = false;
                MessageBox.Show("Please Add Attended Employee Details", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;
            }                      

            if (dtpPrgFrmDate.Value > dtpPrgToDate.Value)
            {
                flag = false;
                MessageBox.Show("Please Select Valid Program Conducted Dates", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;
            }

            return flag;
        }
        #region "INSERT AND UPDATE DATA"
        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            string strDelete = "";
            int iRec = 0;

            if (CheckDetails() == true)
            {
                if (CheckData() == true)
                {
                    if (SaveTrainingPrgHeadDetails() > 0)
                    {
                        if (SaveTrainingTopicsDetails() > 0)
                        {
                            if (SaveTrainedEmpDetails() > 0)
                            {
                                MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnCancel_Click(null, null);
                                flagUpdate = false;
                                GenerateProgramNumber();
                            }
                            else
                            {
                                MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                strDelete = "DELETE FROM HR_TRAINING_ACTUAL_DETL WHERE HTAD_ACTUAL_PROGRAM_NUMBER='" + txtProgramId.Text.ToString() + "'";
                               
                                strDelete += " DELETE FROM HR_TRAINING_ACTUAL_HEAD WHERE HTAH_ACTUAL_PROGRAM_NUMBER='" + txtProgramId.Text.ToString() + "'";
                                iRec = objSQLdb.ExecuteSaveData(strDelete);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            strDelete = "DELETE FROM HR_TRAINING_ACTUAL_HEAD WHERE HTAH_ACTUAL_PROGRAM_NUMBER='" + txtProgramId.Text.ToString() + "'";
                            iRec = objSQLdb.ExecuteSaveData(strDelete);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private int SaveTrainingPrgHeadDetails()
        {
            objSQLdb = new SQLDB();
            int iRes = 0;            
            string strCommand = "";
           
            try
            {              

                strCommand = "DELETE FROM HR_TRAINING_ACTUAL_EMPDETL WHERE HTAE_ACTUAL_PROGRAM_NUMBER='" + txtProgramId.Text.ToString() + "'";
               

                strCommand += " DELETE FROM HR_TRAINING_ACTUAL_DETL WHERE HTAD_ACTUAL_PROGRAM_NUMBER='" + txtProgramId.Text.ToString() + "'";               
                               

                if (flagUpdate == true)
                {
                    strCommand += " UPDATE HR_TRAINING_ACTUAL_HEAD SET HTAH_ACTUAL_PROGRAM_FROM_DATE='" + Convert.ToDateTime(dtpPrgFrmDate.Value).ToString("dd/MMM/yyyy") +
                                "',HTAH_ACTUAL_PROGRAM_TO_DATE='" + Convert.ToDateTime(dtpPrgToDate.Value).ToString("dd/MMM/yyyy") +
                                "', HTAH_ACTUAL_PROGRAM_NAME='" + txtPrgName.Text.ToString().ToUpper() +
                                "', HTAH_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                                "',HTAH_LAST_MODIFIED_DATE=getdate(),HTAH_TRN_TYPE='"+ sFrmType +"'";


                    if (cbTrainerType.SelectedIndex == 0)
                    {

                        strCommand += ", HTAH_TRAINER_ECODE=" + Convert.ToInt32(txtEcodeSearch.Text) +
                                      ",HTAH_TRAINER_FLAG='INTERNAL'";
                    }
                    if (cbTrainerType.SelectedIndex == 1)
                    {
                        strCommand += ", HTAH_TRAINER_FLAG='EXTERNAL' " +
                                     ",HTAH_EXTERNAL_TRAINER_NAME='" + txtTrainerName.Text.ToString() +
                                     "',HTAH_EXTERNAL_TRAINER_DETAILS='" + txtTrainerDetl.Text.ToString()+"'";
                                    
                    }
                    if (chkOurPremises.Checked == true)
                    {
                        strCommand += ", HTAH_PROGRAM_LOCATION_FLAG='OUR PERMISES' " +
                                       ",HTAH_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                       "',HTAH_BRANCH_CODE='" + cbBranches.SelectedValue.ToString() +"'";
                                      
                    }
                    if (chkOthers.Checked == true)
                    {
                        strCommand += ", HTAH_PROGRAM_LOCATION_FLAG='OTHERS' " +
                                       ",HTAH_OTHERS_LOCATION_ADDR1='" + txtAddress1.Text.ToString() +
                                       "',HTAH_OTHERS_LOCATION_ADDR2='" + txtAddress2.Text.ToString() +
                                       "',HTAH_OTHERS_LOCATION_CITY='" + txtCity.Text.ToString() +
                                       "',HTAH_OTHERS_LOCATION_STATE='" + txtState.Text.ToString() +"'";
                                      
                    }
                    if (rdbAgPlanner.Checked == true)
                    {
                        strCommand += ", HTAH_AGAINST_PROGRAM_NUMBER='"+cbPlannerId.SelectedValue.ToString()+"'";
                                     
                    }
                        strCommand+=" WHERE HTAH_ACTUAL_PROGRAM_NUMBER='" + txtProgramId.Text.ToString() + "'";

                   

                }
                else if(flagUpdate==false)
                {

                    strCommand = "INSERT INTO HR_TRAINING_ACTUAL_HEAD(HTAH_ACTUAL_PROGRAM_NUMBER " +
                                                                    ", HTAH_ACTUAL_PROGRAM_FROM_DATE " +
                                                                    ", HTAH_ACTUAL_PROGRAM_TO_DATE "+
                                                                    ", HTAH_ACTUAL_PROGRAM_NAME "+
                                                                    ", HTAH_TRN_TYPE ";

                    if (cbTrainerType.Text.Equals("INTERNAL"))
                    {
                        strCommand += ", HTAH_TRAINER_FLAG, HTAH_TRAINER_ECODE";

                    }
                    if (cbTrainerType.Text.Equals("EXTERNAL"))
                    {
                        strCommand += ", HTAH_TRAINER_FLAG,HTAH_EXTERNAL_TRAINER_NAME,HTAH_EXTERNAL_TRAINER_DETAILS ";
                    }
                    if (chkOurPremises.Checked == true)
                    {
                        strCommand += ", HTAH_PROGRAM_LOCATION_FLAG, HTAH_COMPANY_CODE, HTAH_BRANCH_CODE";
                    }

                    if (chkOthers.Checked == true)
                    {
                        strCommand += ", HTAH_PROGRAM_LOCATION_FLAG " +
                                        ", HTAH_OTHERS_LOCATION_ADDR1 " +
                                        ", HTAH_OTHERS_LOCATION_ADDR2 " +
                                        ", HTAH_OTHERS_LOCATION_CITY " +
                                        ", HTAH_OTHERS_LOCATION_STATE ";
                    }
                    if (rdbAgPlanner.Checked == true)
                    {
                        strCommand += ",HTAH_AGAINST_PROGRAM_NUMBER";
                    }

                    strCommand += ", HTAH_CREATED_BY " +
                                    ", HTAH_CREATED_DATE)VALUES " +
                                    "('" + txtProgramId.Text.ToString() +
                                    "','" + Convert.ToDateTime(dtpPrgFrmDate.Value).ToString("dd/MMM/yyyy") +
                                    "','" + Convert.ToDateTime(dtpPrgToDate.Value).ToString("dd/MMM/yyyy") +
                                    "','" + txtPrgName.Text.ToString().ToUpper() + 
                                    "','"+ sFrmType +"'";


                    if (cbTrainerType.Text.Equals("INTERNAL"))
                    {
                        strCommand += ",'INTERNAL'," + Convert.ToInt32(txtEcodeSearch.Text) + "";
                    }
                    else
                    {
                        strCommand += ",'EXTERNAL','" + txtTrainerName.Text.ToString().ToUpper() +
                                                                         "','" + txtTrainerDetl.Text.ToString() + "'";
                    }

                    if (chkOurPremises.Checked == true)
                    {
                        strCommand += ",'OUR PERMISES','" + cbCompany.SelectedValue.ToString() +
                                                                          "','" + cbBranches.SelectedValue.ToString() + "'";
                    }
                    if (chkOthers.Checked == true)
                    {
                        strCommand += ",'OTHERS','" + txtAddress1.Text.ToString() +
                                                                          "','" + txtAddress2.Text.ToString() +
                                                                          "','" + txtCity.Text.ToString().ToUpper() +
                                                                          "','" + txtState.Text.ToString().ToUpper() + "'";
                    }
                    if (rdbAgPlanner.Checked == true)
                    {
                        strCommand += ",'"+cbPlannerId.SelectedValue.ToString()+"'";
                    }

                    strCommand += ",'" + CommonData.LogUserId +
                                    "',getdate())";
                }

                if (strCommand.Length > 10)
                {
                    iRes = objSQLdb.ExecuteSaveData(strCommand);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iRes;
        }

        private int SaveTrainingTopicsDetails()
        {
            objSQLdb = new SQLDB();
            string strCommand = "";
            int irec = 0;
            try
            {
                if (gvTopicsMethodDetl.Rows.Count > 0)
                {
                    for (int i = 0; i < gvTopicsMethodDetl.Rows.Count; i++)
                    {
                        strCommand += "INSERT INTO HR_TRAINING_ACTUAL_DETL(HTAD_ACTUAL_PROGRAM_NUMBER " +
                                                                        ", HTAD_TOPIC_ID " +
                                                                        ", HTAD_SL_NO " +
                                                                        ", HTAD_METHOD_FLAG " +
                                                                        ", HTAD_TOPIC_DETAILS " +
                                                                        ",HTAD_TRAINER_FLAG " +
                                                                        ",HTAD_EXTERNAL_TRAINER_NAME " +
                                                                        ",HTAD_EXTERNAL_TRAINER_DETAILS " +
                                                                        ",HTAD_TRAINER_ECODE " +
                                                                        ",HTAD_START_TIME "+
                                                                        ",HTAD_END_TIME "+
                                                                        ",HTAD_REMARKS "+
                                                                        ",HTAD_TRN_TYPE "+
                                                                        ")VALUES('" + txtProgramId.Text.ToString() +
                                                                        "'," + Convert.ToInt32(gvTopicsMethodDetl.Rows[i].Cells["TopicId"].Value) +
                                                                        "," + Convert.ToInt32(gvTopicsMethodDetl.Rows[i].Cells["SLNO"].Value) +
                                                                        ",'" + gvTopicsMethodDetl.Rows[i].Cells["MethodType"].Value.ToString() +
                                                                        "','" + gvTopicsMethodDetl.Rows[i].Cells["TopicName"].Value.ToString() +
                                                                        "','" + gvTopicsMethodDetl.Rows[i].Cells["TrainerType"].Value.ToString() +
                                                                        "','" + gvTopicsMethodDetl.Rows[i].Cells["TrainerName"].Value.ToString() +
                                                                        "','" + gvTopicsMethodDetl.Rows[i].Cells["TrainerDetails"].Value.ToString() +
                                                                        "'," + Convert.ToInt32(gvTopicsMethodDetl.Rows[i].Cells["TrainerEcode"].Value) +
                                                                        "," + Convert.ToDouble(gvTopicsMethodDetl.Rows[i].Cells["StartTime"].Value) +
                                                                        "," + Convert.ToDouble(gvTopicsMethodDetl.Rows[i].Cells["EndTime"].Value) +
                                                                        ",'" + gvTopicsMethodDetl.Rows[i].Cells["Remarks"].Value.ToString() + 
                                                                        "','"+ sFrmType +"')";
                    }
                }

                if (strCommand.Length > 10)
                {
                    irec = objSQLdb.ExecuteSaveData(strCommand);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return irec;
        }


        private int SaveTrainedEmpDetails()
        {
            objSQLdb = new SQLDB();
            int iRec = 0;
            string strCommand = "";
            try
            {
                if (gvEmpDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < gvEmpDetails.Rows.Count; i++)
                    {
                        strCommand += "INSERT INTO HR_TRAINING_ACTUAL_EMPDETL(HTAE_ACTUAL_PROGRAM_NUMBER " +
                                                                            ", HTAE_EORA_CODE " +
                                                                            ", HTAE_COMPANY_CODE " +
                                                                            ", HTAE_BRANCH_CODE " +
                                                                            ", HTAE_TRN_TYPE "+
                                                                            ")VALUES('" + txtProgramId.Text.ToString() +
                                                                            "'," + Convert.ToInt32(gvEmpDetails.Rows[i].Cells["Ecode"].Value) +
                                                                            ",'" + gvEmpDetails.Rows[i].Cells["CompanyCode"].Value.ToString() +
                                                                            "','" + gvEmpDetails.Rows[i].Cells["BranchCode"].Value.ToString() + 
                                                                            "','"+ sFrmType +"')";
                    }
                }

                if (strCommand.Length > 5)
                {
                    iRec = objSQLdb.ExecuteSaveData(strCommand);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iRec;
        }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            flagUpdate = false;

            GenerateProgramNumber();

            txtPrgName.Text = "";
            dtpPlanningFrmDate.Value = DateTime.Today;
            dtpPlanToDate.Value = DateTime.Today;
            dtpPrgFrmDate.Value = DateTime.Today;
            dtpPrgToDate.Value = DateTime.Today;
            cbTrainerType.SelectedIndex = 0;
            FillInternalTrainerDetails();
            chkOurPremises.Checked = true;
            gvEmpDetails.Rows.Clear();
            dtTopicMethodDetl.Rows.Clear();
            gvEmpDetails.Rows.Clear();
            gvTopicsMethodDetl.Rows.Clear();
            if(cbTrainerName.SelectedIndex>0)            
            cbTrainerName.SelectedIndex = 0;
            cbPlannerId.SelectedIndex = -1;
            rdbNew.Checked = true;
            txtEcodeSearch.Text = "";
            txtEName.Text = "";
            txtEmpDesg.Text = "";
            txtTrainerName.Text = "";
            txtTrainerDetl.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtCity.Text = "";
            txtState.Text = "";

            if (cbCompany.SelectedIndex > 0)
            {
                cbCompany.SelectedIndex = 0;

            }
            if (cbBranches.SelectedIndex > 0)
            {
                cbBranches.SelectedIndex = 0;
            }
        }

        #region "GRIDVIEW DETAILS"
      
        public void GetTrainingMethodDetails()
        {
            int intRow = 1;
            gvTopicsMethodDetl.Rows.Clear();

            try
            {

                if (dtTopicMethodDetl.Rows.Count > 0)
                {
                    for (int i = 0; i < dtTopicMethodDetl.Rows.Count; i++)
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = intRow;
                        dtTopicMethodDetl.Rows[i]["SLNO"] = intRow;
                        tempRow.Cells.Add(cellSLNO);


                        DataGridViewCell cellTopicId = new DataGridViewTextBoxCell();
                        cellTopicId.Value = dtTopicMethodDetl.Rows[i]["TopicId"].ToString();
                        tempRow.Cells.Add(cellTopicId);

                        DataGridViewCell cellTopicType = new DataGridViewTextBoxCell();
                        cellTopicType.Value = dtTopicMethodDetl.Rows[i]["TopicType"].ToString();
                        tempRow.Cells.Add(cellTopicType);

                        DataGridViewCell cellTopicName = new DataGridViewTextBoxCell();
                        cellTopicName.Value = dtTopicMethodDetl.Rows[i]["TopicName"].ToString();
                        tempRow.Cells.Add(cellTopicName);

                        DataGridViewCell cellMethodType = new DataGridViewTextBoxCell();
                        cellMethodType.Value = dtTopicMethodDetl.Rows[i]["MethodType"].ToString();
                        tempRow.Cells.Add(cellMethodType);

                        DataGridViewCell cellTrainerType = new DataGridViewTextBoxCell();
                        cellTrainerType.Value = dtTopicMethodDetl.Rows[i]["TrainerType"].ToString();
                        tempRow.Cells.Add(cellTrainerType);

                        DataGridViewCell cellTrainerEcode = new DataGridViewTextBoxCell();
                        cellTrainerEcode.Value = dtTopicMethodDetl.Rows[i]["TrainerEcode"].ToString();
                        tempRow.Cells.Add(cellTrainerEcode);

                        DataGridViewCell cellTrainerName = new DataGridViewTextBoxCell();
                        cellTrainerName.Value = dtTopicMethodDetl.Rows[i]["TrainerName"].ToString();
                        tempRow.Cells.Add(cellTrainerName);

                        DataGridViewCell cellTrainerDetails = new DataGridViewTextBoxCell();
                        cellTrainerDetails.Value = dtTopicMethodDetl.Rows[i]["TrainerDetails"].ToString();
                        tempRow.Cells.Add(cellTrainerDetails);

                        DataGridViewCell cellInternalTrName = new DataGridViewTextBoxCell();
                        cellInternalTrName.Value = dtTopicMethodDetl.Rows[i]["InternalTrName"].ToString();
                        tempRow.Cells.Add(cellInternalTrName);

                        DataGridViewCell cellEmpDesig = new DataGridViewTextBoxCell();
                        cellEmpDesig.Value = dtTopicMethodDetl.Rows[i]["EmpDesig"].ToString();
                        tempRow.Cells.Add(cellEmpDesig);

                        DataGridViewCell cellStartTime = new DataGridViewTextBoxCell();
                        cellStartTime.Value = dtTopicMethodDetl.Rows[i]["StartTime"].ToString();
                        tempRow.Cells.Add(cellStartTime);

                        DataGridViewCell cellEndTime = new DataGridViewTextBoxCell();
                        cellEndTime.Value = dtTopicMethodDetl.Rows[i]["EndTime"].ToString();
                        tempRow.Cells.Add(cellEndTime);

                        DataGridViewCell cellRemarks = new DataGridViewTextBoxCell();
                        cellRemarks.Value = dtTopicMethodDetl.Rows[i]["Remarks"].ToString();
                        tempRow.Cells.Add(cellRemarks);

                        intRow = intRow + 1;
                        gvTopicsMethodDetl.Rows.Add(tempRow);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        private void btnAddTopicsDetails_Click(object sender, EventArgs e)
        {
            int Ecode = 0;
            string sEmpName = "";
            string Desig = "";
            string strTrainerName = "";
            string strTrainerdetl = "";
            string strType = "";

            if (CheckDetails() == true)
            {
                if (cbTrainerType.SelectedIndex == 0)
                {
                    Ecode = Convert.ToInt32(txtEcodeSearch.Text);
                    sEmpName = txtEName.Text.ToString();
                    Desig = txtEmpDesg.Text.ToString();
                    strType = "INTERNAL";

                    TrainingPrgTopicsDetl TopicMethodDetl = new TrainingPrgTopicsDetl(strType, Ecode, sEmpName, Desig);
                    TopicMethodDetl.objTrainingProgramDetails = this;
                    TopicMethodDetl.ShowDialog();
                }
                else if (cbTrainerType.SelectedIndex == 1)
                {
                    strTrainerName = txtTrainerName.Text.ToString();
                    strTrainerdetl = txtTrainerDetl.Text.ToString();
                    strType = "EXTERNAL";

                    TrainingPrgTopicsDetl TopicMethodDetl = new TrainingPrgTopicsDetl(strType, strTrainerName, strTrainerdetl);
                    TopicMethodDetl.objTrainingProgramDetails = this;
                    TopicMethodDetl.ShowDialog();
                }


            }


        }

        private void btnAddEmpDetails_Click(object sender, EventArgs e)
        {
            if (txtPrgName.Text != "")
            {
                TrainedEmpDetails EmpDetl = new TrainedEmpDetails();
                EmpDetl.objTrainingProgramDetails = this;
                EmpDetl.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please Enter Program Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPrgName.Focus();
            }
        }

        private void gvTopicsMethodDetl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == gvTopicsMethodDetl.Columns["Edit"].Index)
                {
                    if (Convert.ToBoolean(gvTopicsMethodDetl.Rows[e.RowIndex].Cells["Edit"].Selected) == true)
                    {
                        flagUpdate = true;
                        int SlNo = Convert.ToInt32(gvTopicsMethodDetl.Rows[e.RowIndex].Cells[gvTopicsMethodDetl.Columns["SLNO"].Index].Value);
                        DataRow[] dr = dtTopicMethodDetl.Select("SLNO=" + SlNo);

                        TrainingPrgTopicsDetl TopicMethodDetl = new TrainingPrgTopicsDetl(dr);
                        TopicMethodDetl.objTrainingProgramDetails = this;
                        TopicMethodDetl.ShowDialog();
                    }

                }



                if (e.ColumnIndex == gvTopicsMethodDetl.Columns["Delete"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvTopicsMethodDetl.Rows[e.RowIndex].Cells[gvTopicsMethodDetl.Columns["SLNO"].Index].Value);
                        DataRow[] dr = dtTopicMethodDetl.Select("SLNO=" + SlNo);
                        dtTopicMethodDetl.Rows.Remove(dr[0]);
                        GetTrainingMethodDetails();
                        MessageBox.Show("Selected Data Deleted Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
        }

        private void gvEmpDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == gvEmpDetails.Columns["Del_Emp"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        DataGridViewRow dgvr = gvEmpDetails.Rows[e.RowIndex];
                        gvEmpDetails.Rows.Remove(dgvr);

                        if (gvEmpDetails.Rows.Count > 0)
                        {
                            for (int i = 0; i < gvEmpDetails.Rows.Count; i++)
                            {
                                gvEmpDetails.Rows[i].Cells["SLNO_Emp"].Value = (i + 1).ToString();
                            }
                        }

                    }
                }

            }
        }
        #region "GET TRAINER DETAILS"
        private void FillInternalTrainerDetails()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
            cbTrainerName.DataBindings.Clear();
            try
            {
                if (CommonData.LogUserId.ToUpper() == "ADMIN")
                {
                    strCommand = "SELECT  DISTINCT(CAST(HTPH_TRAINER_ECODE AS VARCHAR)+'-'+MEMBER_NAME) EmpName " +
                                ",HTPH_TRAINER_ECODE  " +
                                " FROM HR_TRAINING_PLANNER_HEAD " +
                                " LEFT JOIN EORA_MASTER ON ECODE=HTPH_TRAINER_ECODE " +
                                " WHERE HTPH_TRAINER_FLAG='INTERNAL' and HTPH_TRN_TYPE='"+ sFrmType +"'";
                }
                else
                {

                    strCommand = "SELECT  DISTINCT(CAST(HTPH_TRAINER_ECODE AS VARCHAR)+'-'+MEMBER_NAME) EmpName " +
                                 ",HTPH_TRAINER_ECODE  " +
                                 " FROM HR_TRAINING_PLANNER_HEAD " +
                                 " LEFT JOIN EORA_MASTER ON ECODE=HTPH_TRAINER_ECODE " +
                                 " WHERE HTPH_TRAINER_FLAG='INTERNAL' and HTPH_TRN_TYPE='"+ sFrmType +
                                 "' and HTPH_CREATED_BY='" + CommonData.LogUserId + "'";
                }

                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "0";

                    dt.Rows.InsertAt(dr, 0);

                    cbTrainerName.DataSource = dt;
                    cbTrainerName.DisplayMember = "EmpName";
                    cbTrainerName.ValueMember = "HTPH_TRAINER_ECODE";

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
            cbTrainerName.DataBindings.Clear();
            try
            {
                if (CommonData.LogUserId.ToUpper() == "ADMIN")
                {
                    strCommand = "SELECT  DISTINCT(HTPH_EXTERNAL_TRAINER_NAME) TrainerName " +
                                 "FROM HR_TRAINING_PLANNER_HEAD " +
                                 " LEFT JOIN EORA_MASTER ON ECODE=HTPH_TRAINER_ECODE " +
                                 " WHERE HTPH_TRAINER_FLAG='EXTERNAL' and HTPH_TRN_TYPE='"+ sFrmType +                                
                                 "' ORDER BY TrainerName";
                }
                else
                {
                    strCommand = "SELECT  DISTINCT(HTPH_EXTERNAL_TRAINER_NAME) TrainerName " +
                                 "FROM HR_TRAINING_PLANNER_HEAD " +
                                 " LEFT JOIN EORA_MASTER ON ECODE=HTPH_TRAINER_ECODE " +
                                 " WHERE HTPH_TRAINER_FLAG='EXTERNAL' and HTPH_TRN_TYPE='" + sFrmType + 
                                 "' and HTPH_CREATED_BY='" + CommonData.LogUserId +
                                 "' ORDER BY TrainerName";
                }

                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbTrainerName.DataSource = dt;
                    cbTrainerName.DisplayMember = "TrainerName";
                    cbTrainerName.ValueMember = "TrainerName";

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
        #endregion



        private void FillPlanningIdByTrainerEcode()                       
        {
             objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
            cbPlannerId.DataSource = null;
            try
            {
                if (cbTrainerName.SelectedIndex > 0)
                {
                    if (flagUpdate == true)
                    {
                        strCommand = "SELECT HTPH_PROGRAM_NUMBER+'-'+HTPH_PROGRAM_NAME PrgName " +
                                ", HTPH_PROGRAM_NUMBER FROM HR_TRAINING_PLANNER_HEAD " +
                                "  WHERE  HTPH_TRAINER_ECODE=" + Convert.ToInt32(cbTrainerName.SelectedValue) + 
                                " and HTPH_TRN_TYPE='" + sFrmType + "'";
                    }
                    else
                    {
                        strCommand = "SELECT HTPH_PROGRAM_NUMBER+'-'+HTPH_PROGRAM_NAME PrgName " +
                                    ", HTPH_PROGRAM_NUMBER FROM HR_TRAINING_PLANNER_HEAD " +
                                    "  WHERE  HTPH_PROGRAM_NUMBER not in(SELECT HTAH_AGAINST_PROGRAM_NUMBER " +
                                    " FROM HR_TRAINING_ACTUAL_HEAD WHERE HTPH_PROGRAM_NUMBER=HTAH_AGAINST_PROGRAM_NUMBER) " +
                                    " AND HTPH_TRAINER_ECODE=" + Convert.ToInt32(cbTrainerName.SelectedValue) + 
                                    "and HTPH_TRN_TYPE='" + sFrmType + "'";
                    }

                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbPlannerId.DataSource = dt;
                    cbPlannerId.DisplayMember = "PrgName";
                    cbPlannerId.ValueMember = "HTPH_PROGRAM_NUMBER";

                }
                else
                {
                   
                    //txtPrgName.Text = "";
                    dtpPlanningFrmDate.Value = DateTime.Today;
                    dtpPlanToDate.Value = DateTime.Today;
                    //dtpPrgDate.Value = DateTime.Today;
                    //cbTrainerType.SelectedIndex = 0;
                    chkOurPremises.Checked = true;
                    gvEmpDetails.Rows.Clear();
                    dtTopicMethodDetl.Rows.Clear();
                    gvEmpDetails.Rows.Clear();
                    gvTopicsMethodDetl.Rows.Clear();

                    txtEcodeSearch.Text = "";
                    txtEName.Text = "";
                    txtEmpDesg.Text = "";
                    txtTrainerName.Text = "";
                    txtTrainerDetl.Text = "";
                    txtAddress1.Text = "";
                    txtAddress2.Text = "";
                    txtCity.Text = "";
                    txtState.Text = "";

                    if (cbCompany.SelectedIndex > 0)
                    {
                        cbCompany.SelectedIndex = 0;

                    }
                    if (cbBranches.SelectedIndex > 0)
                    {
                        cbBranches.SelectedIndex = 0;
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
        private void FillPlanningNoByTrainerName()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
            cbPlannerId.DataSource = null;
            try
            {
                if (cbTrainerName.SelectedIndex > 0)
                {
                    if (flagUpdate == true)
                    {
                        strCommand = "SELECT HTPH_PROGRAM_NUMBER+'-'+HTPH_PROGRAM_NAME PrgName " +
                                      ", HTPH_PROGRAM_NUMBER FROM HR_TRAINING_PLANNER_HEAD " +
                                      "  WHERE  HTPH_EXTERNAL_TRAINER_NAME='" + cbTrainerName.SelectedValue.ToString() + 
                                      "'and HTPH_TRN_TYPE='" + sFrmType + "'";
                    }
                    else
                    {
                        strCommand = "SELECT HTPH_PROGRAM_NUMBER+'-'+HTPH_PROGRAM_NAME PrgName " +
                                    ", HTPH_PROGRAM_NUMBER FROM HR_TRAINING_PLANNER_HEAD " +
                                    "  WHERE  HTPH_PROGRAM_NUMBER not in(SELECT HTAH_AGAINST_PROGRAM_NUMBER " +
                                    " FROM HR_TRAINING_ACTUAL_HEAD WHERE HTPH_PROGRAM_NUMBER=HTAH_AGAINST_PROGRAM_NUMBER) " +
                                    " AND HTPH_EXTERNAL_TRAINER_NAME='" + cbTrainerName.SelectedValue.ToString() +
                                    "'and HTPH_TRN_TYPE='" + sFrmType + "'";
                    }

                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbPlannerId.DataSource = dt;
                    cbPlannerId.DisplayMember = "PrgName";
                    cbPlannerId.ValueMember = "HTPH_PROGRAM_NUMBER";

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
    

        private void cbTrainerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (cbTrainerType.SelectedIndex == 0)
            {
                if (rdbAgPlanner.Checked == true)
                {
                    FillInternalTrainerDetails();
                }

                lblName.Visible = false;
                lblTrDetl.Visible = false;
                txtTrainerName.Visible = false;
                txtTrainerDetl.Visible = false;

                lblEcode.Visible = true;
                lblDesg.Visible = true;
                txtEcodeSearch.Visible = true;
                txtEName.Visible = true;
                txtEmpDesg.Visible = true;
               
            }
            else if (cbTrainerType.SelectedIndex == 1)
            {
                lblName.Visible = true;
                lblTrDetl.Visible = true;
                txtTrainerName.Visible = true;
                txtTrainerDetl.Visible = true;

                lblEcode.Visible = false;
                lblDesg.Visible = false;
                txtEcodeSearch.Visible = false;
                txtEName.Visible = false;
                txtEmpDesg.Visible = false;

                if (rdbAgPlanner.Checked == true)
                {
                    FillExternalTrainerDetails();
                }
            }
        }

        private void chkOurPremises_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOurPremises.Checked == true)
            {
                chkOthers.Checked = false;

                lblCompany.Visible = true;
                lblBranch.Visible = true;
                cbCompany.Visible = true;
                cbBranches.Visible = true;

                lblAddress1.Visible = false;
                lblAddress2.Visible = false;
                lblCity.Visible = false;
                lblState.Visible = false;
                txtAddress1.Visible = false;
                txtAddress2.Visible = false;
                txtCity.Visible = false;
                txtState.Visible = false;
                btnAddEmpFrmAttd.Visible = true;
            }
            else
            {
                chkOthers.Checked = true;

                lblCompany.Visible = false;
                lblBranch.Visible = false;
                cbCompany.Visible = false;
                cbBranches.Visible = false;

                lblAddress1.Visible = true;
                lblAddress2.Visible = true;
                lblCity.Visible = true;
                lblState.Visible = true;
                txtAddress1.Visible = true;
                txtAddress2.Visible = true;
                txtCity.Visible = true;
                txtState.Visible = true;

                btnAddEmpFrmAttd.Visible = false;
            }
        }

        private void chkOthers_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOthers.Checked == true)
            {
                chkOurPremises.Checked = false;

                lblCompany.Visible = false;
                lblBranch.Visible = false;
                cbCompany.Visible = false;
                cbBranches.Visible = false;

                lblAddress1.Visible = true;
                lblAddress2.Visible = true;
                lblCity.Visible = true;
                lblState.Visible = true;
                txtAddress1.Visible = true;
                txtAddress2.Visible = true;
                txtCity.Visible = true;
                txtState.Visible = true;
            }
            else
            {
                chkOurPremises.Checked = true;

                lblCompany.Visible = true;
                lblBranch.Visible = true;
                cbCompany.Visible = true;
                cbBranches.Visible = true;

                lblAddress1.Visible = false;
                lblAddress2.Visible = false;
                lblCity.Visible = false;
                lblState.Visible = false;
                txtAddress1.Visible = false;
                txtAddress2.Visible = false;
                txtCity.Visible = false;
                txtState.Visible = false;
            }
        }

        private void txtPrgName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void rdbAgPlanner_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbAgPlanner.Checked == true)
            {


                lblTrainerName.Visible = true;
                lblPlannerId.Visible = true;
                cbTrainerName.Visible = true;
                cbPlannerId.Visible = true;
                lblPlanningFrmDate.Visible = true;
                dtpPlanningFrmDate.Visible = true;
                lblPlanToDate.Visible = true;
                dtpPlanToDate.Visible = true;
                rdbNew.Checked = false;

                if (flagUpdate == false)
                {
                    GenerateProgramNumber();
                }

                txtPrgName.Text = "";
                dtpPlanningFrmDate.Value = DateTime.Today;
                dtpPlanToDate.Value = DateTime.Today;
                dtpPrgFrmDate.Value = DateTime.Today;
                dtpPrgToDate.Value = DateTime.Today;
                cbTrainerType.SelectedIndex = 0;
                FillInternalTrainerDetails();
                chkOurPremises.Checked = true;
                gvEmpDetails.Rows.Clear();
                dtTopicMethodDetl.Rows.Clear();
                gvEmpDetails.Rows.Clear();
                gvTopicsMethodDetl.Rows.Clear();
                if (cbTrainerName.SelectedIndex > 0)
                    cbTrainerName.SelectedIndex = 0;
                //cbPlannerId.SelectedIndex = -1;

                txtEcodeSearch.Text = "";
                txtEName.Text = "";
                txtEmpDesg.Text = "";
                txtTrainerName.Text = "";
                txtTrainerDetl.Text = "";
                txtAddress1.Text = "";
                txtAddress2.Text = "";
                txtCity.Text = "";
                txtState.Text = "";

                if (cbCompany.SelectedIndex > 0)
                {
                    cbCompany.SelectedIndex = 0;

                }
                if (cbBranches.SelectedIndex > 0)
                {
                    cbBranches.SelectedIndex = 0;
                }

            }
            else
            {
                lblTrainerName.Visible = false;
                lblPlannerId.Visible = false;
                cbTrainerName.Visible = false;
                cbPlannerId.Visible = false;
                lblPlanningFrmDate.Visible = false;
                dtpPlanningFrmDate.Visible = false;
                rdbAgPlanner.Checked = false;

                lblPlanToDate.Visible = false;
                dtpPlanToDate.Visible = false;

                rdbNew.Checked = true;
            }

        }

        private void rdbNew_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbNew.Checked == true)
            {
                lblTrainerName.Visible = false;
                lblPlannerId.Visible = false;
                cbTrainerName.Visible = false;
                cbPlannerId.Visible = false;
                lblPlanningFrmDate.Visible = false;
                dtpPlanningFrmDate.Visible = false;

                lblPlanToDate.Visible = false;
                dtpPlanToDate.Visible = false;

                rdbAgPlanner.Checked = false;

                if (flagUpdate == false)
                {
                    GenerateProgramNumber();
                }

                txtPrgName.Text = "";
                dtpPlanningFrmDate.Value = DateTime.Today;
                dtpPlanToDate.Value = DateTime.Today;
                dtpPrgFrmDate.Value = DateTime.Today;
                dtpPrgToDate.Value = DateTime.Today;
                cbTrainerType.SelectedIndex = 0;
                FillInternalTrainerDetails();
                chkOurPremises.Checked = true;
                gvEmpDetails.Rows.Clear();
                dtTopicMethodDetl.Rows.Clear();
                gvEmpDetails.Rows.Clear();
                gvTopicsMethodDetl.Rows.Clear();
                if (cbTrainerName.SelectedIndex > 0)
                    cbTrainerName.SelectedIndex = 0;
                cbPlannerId.SelectedIndex = -1;

                txtEcodeSearch.Text = "";
                txtEName.Text = "";
                txtEmpDesg.Text = "";
                txtTrainerName.Text = "";
                txtTrainerDetl.Text = "";
                txtAddress1.Text = "";
                txtAddress2.Text = "";
                txtCity.Text = "";
                txtState.Text = "";

                if (cbCompany.SelectedIndex > 0)
                {
                    cbCompany.SelectedIndex = 0;

                }
                if (cbBranches.SelectedIndex > 0)
                {
                    cbBranches.SelectedIndex = 0;
                }

            }
            else
            {
                lblTrainerName.Visible = true;
                lblPlannerId.Visible = true;
                cbTrainerName.Visible = true;
                cbPlannerId.Visible = true;
                lblPlanningFrmDate.Visible = true;
                dtpPlanningFrmDate.Visible = true;

                lblPlanToDate.Visible = true;
                dtpPlanToDate.Visible = true;

                rdbNew.Checked = false;
                rdbAgPlanner.Checked = true;
            }

        }

        
        #region "GET DATA BASED ON PLANNER ID"
        private void FillTrainingPlanningDetails()
        {
            objHRdb = new HRInfo();
            DataTable dtTrainingPrgHeadDetl;
            Hashtable ht;
            if (cbPlannerId.SelectedIndex > 0)
            {
                try
                {
                    ht = objHRdb.GetTrainingPlanningDetails(cbPlannerId.SelectedValue.ToString());

                    dtTrainingPrgHeadDetl = (DataTable)ht["TrainingPrgHeadDetails"];

                    FillTrainingPrgHeadDetails(dtTrainingPrgHeadDetl);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objHRdb = null;
                }
            }
            else
            {
                txtPrgName.Text = "";
                dtpPlanningFrmDate.Value = DateTime.Today;
                cbTrainerType.SelectedIndex = 0;
                chkOurPremises.Checked = true;

                
                dtTopicMethodDetl.Rows.Clear();

                gvEmpDetails.Rows.Clear();
                gvTopicsMethodDetl.Rows.Clear();

                txtEcodeSearch.Text = "";
                txtEName.Text = "";
                txtEmpDesg.Text = "";
                txtTrainerName.Text = "";
                txtTrainerDetl.Text = "";
                txtAddress1.Text = "";
                txtAddress2.Text = "";
                txtCity.Text = "";
                txtState.Text = "";

                if (cbCompany.SelectedIndex > 0)
                {
                    cbCompany.SelectedIndex = 0;

                }
                if (cbBranches.SelectedIndex > 0)
                {
                    cbBranches.SelectedIndex = 0;
                }


            }
        }

        private void FillTrainingPrgHeadDetails(DataTable dtHead)
        {
            objHRdb = new HRInfo();
            Hashtable ht;
            DataTable dtTopicsDetl;
            DataTable dtEmpDetails;

            if (cbPlannerId.SelectedIndex>0)
            {
                try
                {
                    ht = objHRdb.GetTrainingPlanningDetails(cbPlannerId.SelectedValue.ToString());

                    dtTopicsDetl = (DataTable)ht["TrainingPrgTopicDetl"];
                    dtEmpDetails = (DataTable)ht["TrainingEmpDetl"];

                    if (dtHead.Rows.Count > 0)
                    {
                        
                        txtPrgName.Text = dtHead.Rows[0]["ProgramName"].ToString();
                        dtpPlanningFrmDate.Enabled = false;
                        dtpPlanningFrmDate.Value = Convert.ToDateTime(dtHead.Rows[0]["ProgramFromDate"].ToString());
                        dtpPlanToDate.Value = Convert.ToDateTime(dtHead.Rows[0]["PrgToDate"].ToString());

                        dtpPrgFrmDate.Value = Convert.ToDateTime(dtHead.Rows[0]["ProgramFromDate"].ToString());
                        dtpPrgToDate.Value = Convert.ToDateTime(dtHead.Rows[0]["PrgToDate"].ToString());

                        if (dtHead.Rows[0]["TrainerFlag"].ToString().Equals("INTERNAL"))
                        {
                            cbTrainerType.SelectedIndex = 0;

                            txtEcodeSearch.Text = dtHead.Rows[0]["TrainerEcode"].ToString();
                            txtEName.Text = dtHead.Rows[0]["EmpName"].ToString();
                            txtEmpDesg.Text = dtHead.Rows[0]["EmpDesig"].ToString();

                        }
                        else
                        {
                            cbTrainerType.SelectedIndex = 1;
                            txtTrainerName.Text = dtHead.Rows[0]["TrainerName"].ToString();
                            txtTrainerDetl.Text = dtHead.Rows[0]["TrainerDetails"].ToString();
                        }

                        if (dtHead.Rows[0]["LocationFlag"].ToString().Equals("OUR PERMISES"))
                        {
                            chkOurPremises.Checked = true;

                            cbCompany.SelectedValue = dtHead.Rows[0]["CompanyCode"].ToString();
                            cbBranches.SelectedValue = dtHead.Rows[0]["BranchCode"].ToString();
                        }
                        else
                        {
                            chkOthers.Checked = true;

                            txtAddress1.Text = dtHead.Rows[0]["LocationAddr1"].ToString();
                            txtAddress2.Text = dtHead.Rows[0]["LocationAddr2"].ToString();
                            txtCity.Text = dtHead.Rows[0]["LocationCity"].ToString();
                            txtState.Text = dtHead.Rows[0]["LocationState"].ToString();
                        }

                        FillTopicDetails(dtTopicsDetl);
                        FillTrainingEmpDetails(dtEmpDetails);
                    }
                    else
                    {
                        

                        txtPrgName.Text = "";
                        dtpPlanningFrmDate.Value = DateTime.Today;
                        cbTrainerType.SelectedIndex = 0;
                        chkOurPremises.Checked = true;

                       
                        dtTopicMethodDetl.Rows.Clear();

                        gvEmpDetails.Rows.Clear();
                        gvTopicsMethodDetl.Rows.Clear();

                        txtEcodeSearch.Text = "";
                        txtEName.Text = "";
                        txtEmpDesg.Text = "";
                        txtTrainerName.Text = "";
                        txtTrainerDetl.Text = "";
                        txtAddress1.Text = "";
                        txtAddress2.Text = "";
                        txtCity.Text = "";
                        txtState.Text = "";

                        if (cbCompany.SelectedIndex > 0)
                        {
                            cbCompany.SelectedIndex = 0;

                        }
                        if (cbBranches.SelectedIndex > 0)
                        {
                            cbBranches.SelectedIndex = 0;
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }



        private void FillTopicDetails(DataTable dtTopicsDetl)
        {
            dtTopicMethodDetl.Rows.Clear();
            if (cbPlannerId.SelectedIndex>0)
            {
                try
                {
                    if (dtTopicsDetl.Rows.Count > 0)
                    {
                        
                        for (int i = 0; i < dtTopicsDetl.Rows.Count; i++)
                        {

                            dtTopicMethodDetl.Rows.Add(new Object[] {"-1", dtTopicsDetl.Rows[i]["TopicId"].ToString(),
                                                                       dtTopicsDetl.Rows[i]["TopicType"].ToString(),                                                                      
                                                                       dtTopicsDetl.Rows[i]["TopicName"].ToString(),
                                                                       dtTopicsDetl.Rows[i]["MethodFlag"].ToString(),
                                                                       dtTopicsDetl.Rows[i]["TrainerFlag"].ToString(),
                                                                       dtTopicsDetl.Rows[i]["TrainerEcode"].ToString(),
                                                                       dtTopicsDetl.Rows[i]["TrainerName"].ToString(),
                                                                       dtTopicsDetl.Rows[i]["TrainerDetails"].ToString(),
                                                                       dtTopicsDetl.Rows[i]["EmpName"].ToString(),
                                                                       dtTopicsDetl.Rows[i]["EmpDesig"].ToString()});
                            GetTrainingMethodDetails();

                        }
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }

        }

        private void FillTrainingEmpDetails(DataTable dtEmp)
        {
            gvEmpDetails.Rows.Clear();
            if (txtProgramId.Text.Length > 0)
            {
                try
                {
                    if (dtEmp.Rows.Count > 0)
                    {                    

                        for (int i = 0; i < dtEmp.Rows.Count; i++)
                        {
                            gvEmpDetails.Rows.Add();

                            gvEmpDetails.Rows[i].Cells["SLNO_Emp"].Value = (i + 1).ToString();
                            gvEmpDetails.Rows[i].Cells["Ecode"].Value = dtEmp.Rows[i]["EmpEcode"].ToString();
                            gvEmpDetails.Rows[i].Cells["CompanyCode"].Value = dtEmp.Rows[i]["CompanyCode"].ToString();
                            gvEmpDetails.Rows[i].Cells["BranchCode"].Value = dtEmp.Rows[i]["BranchCode"].ToString();
                            gvEmpDetails.Rows[i].Cells["EmpName"].Value = dtEmp.Rows[i]["EmpName"].ToString();
                            gvEmpDetails.Rows[i].Cells["Desig"].Value = dtEmp.Rows[i]["EmpDesig"].ToString();
                            gvEmpDetails.Rows[i].Cells["Dept"].Value = dtEmp.Rows[i]["DeptName"].ToString();
                            gvEmpDetails.Rows[i].Cells["Company"].Value = dtEmp.Rows[i]["CompanyName"].ToString();
                            gvEmpDetails.Rows[i].Cells["Branch"].Value = dtEmp.Rows[i]["BranchName"].ToString();


                        }
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }


        }

        #endregion  


        #region "GET DATA FOR UPDATE"

        private void FillTrainingProgramDetails()
        {
            objHRdb = new HRInfo();
            DataTable dtActualTrainingPrgHeadDetl;
            Hashtable ht;
            if (txtProgramId.Text!="")
            {
                try
                {
                    ht = objHRdb.GetTrainingProgramDetails(txtProgramId.Text.ToString());

                    dtActualTrainingPrgHeadDetl = (DataTable)ht["ActualTrainingPrgHeadDetails"];

                    if (dtActualTrainingPrgHeadDetl.Rows.Count > 0)
                    {
                        FillActualTrainingPrgHeadDetails(dtActualTrainingPrgHeadDetl);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objHRdb = null;
                }
            }
        }

        private void FillActualTrainingPrgHeadDetails(DataTable dtActualHead)
        {
            objHRdb = new HRInfo();
            Hashtable ht;
            DataTable dtTopicsDetl;
            DataTable dtEmpDetails;

            if (txtProgramId.Text.Length > 0)
            {
                try
                {
                    ht = objHRdb.GetTrainingProgramDetails(txtProgramId.Text.ToString());

                    dtTopicsDetl = (DataTable)ht["ActualTrainingPrgTopicDetl"];
                    dtEmpDetails = (DataTable)ht["ActualTrainingEmpDetl"];

                    if (dtActualHead.Rows.Count > 0)
                    {
                        flagUpdate = true;

                        sFrmType = "";

                        if (dtActualHead.Rows[0]["TrainerFlag"].ToString().Equals("INTERNAL"))


                        {
                            cbTrainerType.SelectedIndex = 0;

                            FillInternalTrainerDetails();

                            cbTrainerName.Text = dtActualHead.Rows[0]["EmplName"].ToString();

                            txtEcodeSearch.Text = dtActualHead.Rows[0]["TrainerEcode"].ToString();
                            txtEName.Text = dtActualHead.Rows[0]["EmpName"].ToString();
                            txtEmpDesg.Text = dtActualHead.Rows[0]["EmpDesig"].ToString();

                        }
                        else
                        {
                            cbTrainerType.SelectedIndex = 1;
                            FillExternalTrainerDetails();

                            cbTrainerName.Text = dtActualHead.Rows[0]["TrainerName"].ToString();

                            txtTrainerName.Text = dtActualHead.Rows[0]["TrainerName"].ToString();
                            txtTrainerDetl.Text = dtActualHead.Rows[0]["TrainerDetails"].ToString();
                        }
                    
                        if (dtActualHead.Rows[0]["AgainstPrgId"].ToString() != "")
                        {
                            rdbAgPlanner.Checked = true;
                            cbTrainerName.SelectedValue = dtActualHead.Rows[0]["TrainerEcode"].ToString();

                            if (dtActualHead.Rows[0]["AgainstPrgFromdate"].ToString() != "")
                            {
                                dtpPlanningFrmDate.Value = Convert.ToDateTime(dtActualHead.Rows[0]["AgainstPrgFromdate"].ToString());
                            }
                            if (dtActualHead.Rows[0]["AgainstPrgTodate"].ToString() != "")
                            {
                                dtpPlanToDate.Value = Convert.ToDateTime(dtActualHead.Rows[0]["AgainstPrgTodate"].ToString());
                            }
                            
                            cbPlannerId.SelectedValue = dtActualHead.Rows[0]["AgainstPrgId"].ToString();
                            
                        }
                        else
                        {
                            rdbNew.Checked = true;
                        }

                        txtPrgName.Text = dtActualHead.Rows[0]["ProgramName"].ToString();
                        sFrmType = dtActualHead.Rows[0]["TrnType"].ToString(); 

                        dtpPlanningFrmDate.Enabled = false;
                        dtpPlanToDate.Enabled = false;

                        dtpPrgFrmDate.Value = Convert.ToDateTime(dtActualHead.Rows[0]["PrgFromDate"].ToString());
                        dtpPrgToDate.Value = Convert.ToDateTime(dtActualHead.Rows[0]["PrgToDate"].ToString());
                    

                        if (dtActualHead.Rows[0]["LocationFlag"].ToString().Equals("OUR PERMISES"))
                        {
                            chkOurPremises.Checked = true;

                            cbCompany.SelectedValue = dtActualHead.Rows[0]["CompanyCode"].ToString();
                            cbBranches.SelectedValue = dtActualHead.Rows[0]["BranchCode"].ToString();
                        }
                        else
                        {
                            chkOthers.Checked = true;

                            txtAddress1.Text = dtActualHead.Rows[0]["LocAddr1"].ToString();
                            txtAddress2.Text = dtActualHead.Rows[0]["LocAddr2"].ToString();
                            txtCity.Text = dtActualHead.Rows[0]["LocCity"].ToString();
                            txtState.Text = dtActualHead.Rows[0]["LocState"].ToString();
                        }

                        FillActualTrainingTopicsDetl(dtTopicsDetl);
                        FillActualTrainedEmpDetails(dtEmpDetails);
                    }
                    else
                    {
                        flagUpdate = false;
                        GenerateProgramNumber();
                        txtPrgName.Text = "";
                        dtpPlanningFrmDate.Value = DateTime.Today;
                        cbTrainerType.SelectedIndex = 0;
                        FillInternalTrainerDetails();
                        chkOurPremises.Checked = true;

                       
                        dtTopicMethodDetl.Rows.Clear();

                        gvEmpDetails.Rows.Clear();
                        gvTopicsMethodDetl.Rows.Clear();
                        if (rdbAgPlanner.Checked == true)
                        {
                            cbTrainerName.SelectedIndex = 0;
                            cbPlannerId.SelectedIndex = -1;
                        }

                        txtEcodeSearch.Text = "";
                        txtEName.Text = "";
                        txtEmpDesg.Text = "";
                        txtTrainerName.Text = "";
                        txtTrainerDetl.Text = "";
                        txtAddress1.Text = "";
                        txtAddress2.Text = "";
                        txtCity.Text = "";
                        txtState.Text = "";

                        if (cbCompany.SelectedIndex > 0)
                        {
                            cbCompany.SelectedIndex = 0;

                        }
                        if (cbBranches.SelectedIndex > 0)
                        {
                            cbBranches.SelectedIndex = 0;
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                flagUpdate = false;
                GenerateProgramNumber();
                //txtPrgName.Text = "";
                dtpPlanningFrmDate.Value = DateTime.Today;
                cbTrainerType.SelectedIndex = 0;
                chkOurPremises.Checked = true;
                
                dtTopicMethodDetl.Rows.Clear();

                gvEmpDetails.Rows.Clear();
                gvTopicsMethodDetl.Rows.Clear();
                if (rdbAgPlanner.Checked == true)
                {
                    cbTrainerName.SelectedIndex = 0;
                    cbPlannerId.SelectedIndex = -1;
                }

                txtEcodeSearch.Text = "";
                txtEName.Text = "";
                txtEmpDesg.Text = "";
                txtTrainerName.Text = "";
                txtTrainerDetl.Text = "";
                txtAddress1.Text = "";
                txtAddress2.Text = "";
                txtCity.Text = "";
                txtState.Text = "";

                if (cbCompany.SelectedIndex > 0)
                {
                    cbCompany.SelectedIndex = 0;

                }
                if (cbBranches.SelectedIndex > 0)
                {
                    cbBranches.SelectedIndex = 0;
                }
            }

        }
   

        private void FillActualTrainingTopicsDetl(DataTable dtTopicsDetl)
        {
            dtTopicMethodDetl.Rows.Clear();
            if (txtProgramId.Text.Length > 0)
            {
                try
                {
                    if (dtTopicsDetl.Rows.Count > 0)
                    {
                        flagUpdate = true;

                        for (int i = 0; i < dtTopicsDetl.Rows.Count; i++)
                        {

                            dtTopicMethodDetl.Rows.Add(new Object[] {"-1", dtTopicsDetl.Rows[i]["TopicId"].ToString(),
                                                                       dtTopicsDetl.Rows[i]["TopicType"].ToString(),                                                                      
                                                                       dtTopicsDetl.Rows[i]["TopicName"].ToString(),
                                                                       dtTopicsDetl.Rows[i]["MethodType"].ToString(),                                                                      
                                                                       dtTopicsDetl.Rows[i]["TrainerFlag"].ToString(),
                                                                       dtTopicsDetl.Rows[i]["TrainerEcode"].ToString(),
                                                                       dtTopicsDetl.Rows[i]["TrainerName"].ToString(),
                                                                       dtTopicsDetl.Rows[i]["TrainerDetails"].ToString(),
                                                                       dtTopicsDetl.Rows[i]["EmpName"].ToString(),
                                                                       dtTopicsDetl.Rows[i]["EmpDesig"].ToString(),
                                                                       dtTopicsDetl.Rows[i]["StartTime"].ToString(),
                                                                       dtTopicsDetl.Rows[i]["EndTime"].ToString(),
                                                                       dtTopicsDetl.Rows[i]["Remarks"].ToString()});
                            GetTrainingMethodDetails();

                        }
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }

        }

        private void FillActualTrainedEmpDetails(DataTable dtEmp)
        {
            gvEmpDetails.Rows.Clear();
            if (txtProgramId.Text.Length > 0)
            {
                try
                {
                    if (dtEmp.Rows.Count > 0)
                    {
                        flagUpdate = true;

                        for (int i = 0; i < dtEmp.Rows.Count; i++)
                        {
                            gvEmpDetails.Rows.Add();

                            gvEmpDetails.Rows[i].Cells["SLNO_Emp"].Value = (i + 1).ToString();
                            gvEmpDetails.Rows[i].Cells["Ecode"].Value = dtEmp.Rows[i]["EmpEcode"].ToString();
                            gvEmpDetails.Rows[i].Cells["CompanyCode"].Value = dtEmp.Rows[i]["CompCode"].ToString();
                            gvEmpDetails.Rows[i].Cells["BranchCode"].Value = dtEmp.Rows[i]["BranchCode"].ToString();
                            gvEmpDetails.Rows[i].Cells["EmpName"].Value = dtEmp.Rows[i]["EmpName"].ToString();
                            gvEmpDetails.Rows[i].Cells["Desig"].Value = dtEmp.Rows[i]["EmpDesig"].ToString();
                            gvEmpDetails.Rows[i].Cells["Dept"].Value = dtEmp.Rows[i]["DeptName"].ToString();
                            gvEmpDetails.Rows[i].Cells["Company"].Value = dtEmp.Rows[i]["CompanyName"].ToString();
                            gvEmpDetails.Rows[i].Cells["Branch"].Value = dtEmp.Rows[i]["BranchName"].ToString();
                          

                        }
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }

        }

        #endregion


        private void cbPlannerId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPlannerId.SelectedIndex > 0)
            {
                FillTrainingPlanningDetails();
            }
            else
            {
                
                //txtPrgName.Text = "";
                dtpPlanningFrmDate.Value = DateTime.Today;
                //dtpPrgDate.Value = DateTime.Today;
                //cbTrainerType.SelectedIndex = 0;
                chkOurPremises.Checked = true;
               
                dtTopicMethodDetl.Rows.Clear();
                gvEmpDetails.Rows.Clear();
                gvTopicsMethodDetl.Rows.Clear();

                txtEcodeSearch.Text = "";
                txtEName.Text = "";
                txtEmpDesg.Text = "";
                txtTrainerName.Text = "";
                txtTrainerDetl.Text = "";
                txtAddress1.Text = "";
                txtAddress2.Text = "";
                txtCity.Text = "";
                txtState.Text = "";

                if (cbCompany.SelectedIndex > 0)
                {
                    cbCompany.SelectedIndex = 0;

                }
                if (cbBranches.SelectedIndex > 0)
                {
                    cbBranches.SelectedIndex = 0;
                }

            }
        }

        

        private void cbTrainerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTrainerName.SelectedIndex > 0)
            {
                if (cbTrainerType.Text.Equals("INTERNAL"))
                {
                    FillPlanningIdByTrainerEcode();
                }
                else
                {
                    FillPlanningNoByTrainerName();
                }
            }
            //else
            //{
            //    cbPlannerId.SelectedIndex = -1;
            //    //txtPrgName.Text = "";
            //    dtpPlanningDate.Value = DateTime.Today;
            //    //dtpPrgDate.Value = DateTime.Today;
            //    //cbTrainerType.SelectedIndex = 0;
            //    chkOurPremises.Checked = true;
            //    dtEmpDetl.Rows.Clear();
            //    dtTopicMethodDetl.Rows.Clear();
            //    gvEmpDetails.Rows.Clear();
            //    gvTopicsMethodDetl.Rows.Clear();

            //    txtEcodeSearch.Text = "";
            //    txtEName.Text = "";
            //    txtEmpDesg.Text = "";
            //    txtTrainerName.Text = "";
            //    txtTrainerDetl.Text = "";
            //    txtAddress1.Text = "";
            //    txtAddress2.Text = "";
            //    txtCity.Text = "";
            //    txtState.Text = "";

            //    if (cbCompany.SelectedIndex > 0)
            //    {
            //        cbCompany.SelectedIndex = 0;

            //    }
            //    if (cbBranches.SelectedIndex > 0)
            //    {
            //        cbBranches.SelectedIndex = 0;
            //    }

            //}
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbCompany.SelectedIndex > 0)
            {
                FillBranchData();
            }
            else
            {
                cbBranches.DataSource = null;
            }

        }

        private void txtProgramId_Validated(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "", strUserId = "";

            try
            {

                if (txtProgramId.Text != "")
                {
                    
                        strCmd = "SELECT HTAH_CREATED_BY FROM HR_TRAINING_ACTUAL_HEAD WHERE HTAH_ACTUAL_PROGRAM_NUMBER='" + txtProgramId.Text.ToString() + "'";
                        dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            strUserId = dt.Rows[0]["HTAH_CREATED_BY"].ToString();
                        }

                        if (CommonData.LogUserId.Equals(strUserId) || CommonData.LogUserId =="admin")
                        {
                            FillTrainingProgramDetails();
                        }
                        else
                        {
                            MessageBox.Show("Sorry! This Program Id Not Pertaining To You", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnCancel_Click(null, null);
                            txtPrgName.Focus();

                        }
                    


                }
                else
                {
                    btnCancel_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

       

        private void txtEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtTrainerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            if (txtEcodeSearch.Text != "")
            {

                try
                {
                    string strCommand = "SELECT MEMBER_NAME,desig_name FROM EORA_MASTER " +
                                        " INNER JOIN DESIG_MAS ON desig_code=DESG_ID " +
                                        " WHERE ECODE =" + Convert.ToInt32(txtEcodeSearch.Text) + " ";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtEName.Text = dt.Rows[0]["MEMBER_NAME"].ToString();
                        txtEmpDesg.Text = dt.Rows[0]["desig_name"].ToString();

                    }
                    else
                    {
                        txtEName.Text = "";
                        txtEmpDesg.Text = "";
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
        }

        private void txtPrgName_Validated(object sender, EventArgs e)
        {
            //try
            //{
            //    objSQLdb = new SQLDB();
            //    DataTable dt = objSQLdb.ExecuteDataSet("SELECT DISTINCT HTAH_ACTUAL_PROGRAM_NAME "+
            //                                            " FROM HR_TRAINING_ACTUAL_HEAD "+
            //                                            " WHERE HTAH_ACTUAL_PROGRAM_NAME='"+ txtPrgName.Text +
            //                                            "' ORDER BY HTAH_ACTUAL_PROGRAM_NAME ASC").Tables[0];
            //    UtilityLibrary.AutoCompleteTextBox(txtPrgName, dt, "", "HTAH_ACTUAL_PROGRAM_NAME");
            //    objSQLdb = null;
            //}
            //catch { }
            //finally { objSQLdb = null; }

        }

        private void btnAddEmpFrmAttd_Click(object sender, EventArgs e)
        {
            if (cbBranches.SelectedIndex > 0)
            {
                AddEmployeesFromBiometric EmpFrmBioMetric = new AddEmployeesFromBiometric(cbBranches.SelectedValue.ToString(), dtpPrgFrmDate.Value.ToString("dd/MMM/yyyy"), dtpPrgToDate.Value.ToString("dd/MMM/yyyy"));
                EmpFrmBioMetric.objTrainingProgramDetails = this;
                EmpFrmBioMetric.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please Select Branch Location","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Error);
                cbBranches.Focus();
            }
        }


      

      
      
     
    }
}
