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
    public partial class TrainingPlanningDetails : Form
    {
        SQLDB objSQLdb = null;
        HRInfo objHRdb = null;        

        public DataTable dtTopicMethodDetl = new DataTable();
        public DataTable dtEmpDetl = new DataTable();
        private bool flagUpdate = false;
        private string strFrmType = "";


        public TrainingPlanningDetails()
        {
            InitializeComponent();
        }
        public TrainingPlanningDetails(string sType)
        {
            InitializeComponent();
            strFrmType = sType;
        }


        private void TrainingPlanningDetails_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            cbTrainerType.SelectedIndex = 0;
            chkOurPremises.Checked = true;
            GenerateProgramNumber();
            dtpPrgFrmDate.Value = DateTime.Today;
            dtpPrgToDate.Value = DateTime.Today;

            try
            {
                objSQLdb = new SQLDB();
                DataTable dt = objSQLdb.ExecuteDataSet("SELECT DISTINCT HTPH_PROGRAM_NAME " +
                                                    " FROM HR_TRAINING_PLANNER_HEAD where HTPH_TRN_TYPE='"+ strFrmType +"' " +                                                    
                                                    " ORDER BY HTPH_PROGRAM_NAME ASC").Tables[0];
                UtilityLibrary.AutoCompleteTextBox(txtPrgName, dt, "", "HTPH_PROGRAM_NAME");
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
                String strCommand = "SELECT ISNULL(MAX(cast(HTPH_PROGRAM_NUMBER as numeric)),0)+1  FROM HR_TRAINING_PLANNER_HEAD";
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
                MessageBox.Show("Please Add Training Topic Methodology Details", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }
            //if (gvEmpDetails.Rows.Count == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Add Trained Employee Details", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return flag;
            //}

            return flag;
        }

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
                            SaveTrainedEmpDetails();

                            MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnCancel_Click(null, null);
                            flagUpdate = false;
                            GenerateProgramNumber();
                        }
                        else
                        {
                            MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            flagUpdate = false;
                            strDelete = "DELETE FROM HR_TRAINING_PLANNER_HEAD WHERE HTPH_PROGRAM_NUMBER='" + txtProgramId.Text.ToString() + "'";
                            iRec = objSQLdb.ExecuteSaveData(strDelete);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                strCommand = "DELETE FROM HR_TRAINING_PLANNER_EMPDETL WHERE HTPE_PROGRAM_NUMBER='"+ txtProgramId.Text.ToString() +"'";
               
                
                strCommand += " DELETE FROM HR_TRAINING_PLANNER_DETL WHERE HTPD_PROGRAM_NUMBER='" + txtProgramId.Text.ToString() + "'";              

          

                if (flagUpdate == true)
                {
                    strCommand += " UPDATE HR_TRAINING_PLANNER_HEAD SET HTPH_PROGRAM_FROM_DATE='" + Convert.ToDateTime(dtpPrgFrmDate.Value).ToString("dd/MMM/yyyy") +
                                "',HTPH_PROGRAM_TO_DATE='" + Convert.ToDateTime(dtpPrgToDate.Value).ToString("dd/MMM/yyyy") +
                                "', HTPH_PROGRAM_NAME='" + txtPrgName.Text.ToString().ToUpper() +
                                "' ,HTPH_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                                "',HTPH_LAST_MODIFIED_DATE=getdate(),HTPH_TRN_TYPE='"+ strFrmType +"' ";
                               

                    if (cbTrainerType.SelectedIndex == 0)
                    {

                        strCommand += ", HTPH_TRAINER_ECODE=" + Convert.ToInt32(txtEcodeSearch.Text) +
                                      ",HTPH_TRAINER_FLAG='INTERNAL' ";
                    }
                    if (cbTrainerType.SelectedIndex == 1)
                    {
                        strCommand += ", HTPH_TRAINER_FLAG='EXTERNAL' " +
                                     ",HTPH_EXTERNAL_TRAINER_NAME='" + txtTrainerName.Text.ToString() +
                                     "',HTTH_EXTERNAL_TRAINER_DETAILS='" + txtTrainerDetl.Text.ToString() + "'";
                                     
                    }
                    if (chkOurPremises.Checked == true)
                    {
                        strCommand += ", HTPH_PROGRAM_LOCATION_FLAG='OUR PERMISES' " +
                                       ",HTPH_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                       "',HTPH_BRANCH_CODE='" + cbBranches.SelectedValue.ToString() + "'";
                                       
                    }
                    if (chkOthers.Checked == true)
                    {
                        strCommand += " HTPH_PROGRAM_LOCATION_FLAG='OTHERS' " +
                                       ",HTPH_OTHERS_LOCATION_ADDR1='" + txtAddress1.Text.ToString() +
                                       "',HTPH_OTHERS_LOCATION_ADDR2='" + txtAddress2.Text.ToString() +
                                       "',HTPH_OTHERS_LOCATION_CITY='" + txtCity.Text.ToString() +
                                       "',HTPH_OTHERS_LOCATION_STATE='" + txtState.Text.ToString() + "'";
                                      
                    }

                    strCommand += " WHERE HTPH_PROGRAM_NUMBER='" + txtProgramId.Text.ToString() + "'";

                }
                else if (flagUpdate == false)
                {
                    GenerateProgramNumber();
                    objSQLdb = new SQLDB();

                    strCommand = "INSERT INTO HR_TRAINING_PLANNER_HEAD(HTPH_PROGRAM_NUMBER " +
                                                                    ", HTPH_PROGRAM_FROM_DATE " +
                                                                    ", HTPH_PROGRAM_TO_DATE " +
                                                                    ", HTPH_PROGRAM_NAME "+
                                                                    ", HTPH_TRN_TYPE ";

                    if (cbTrainerType.Text.Equals("INTERNAL"))
                    {
                        strCommand += ", HTPH_TRAINER_FLAG, HTPH_TRAINER_ECODE";

                    }
                    if (cbTrainerType.Text.Equals("EXTERNAL"))
                    {
                        strCommand += ", HTPH_TRAINER_FLAG,HTPH_EXTERNAL_TRAINER_NAME,HTTH_EXTERNAL_TRAINER_DETAILS ";
                    }
                    if (chkOurPremises.Checked == true)
                    {
                        strCommand += ", HTPH_PROGRAM_LOCATION_FLAG, HTPH_COMPANY_CODE, HTPH_BRANCH_CODE";
                    }

                    if (chkOthers.Checked == true)
                    {
                        strCommand += ", HTPH_PROGRAM_LOCATION_FLAG " +
                                        ", HTPH_OTHERS_LOCATION_ADDR1 " +
                                        ", HTPH_OTHERS_LOCATION_ADDR2 " +
                                        ", HTPH_OTHERS_LOCATION_CITY " +
                                        ", HTPH_OTHERS_LOCATION_STATE ";
                    }
                    strCommand += ", HTPH_CREATED_BY " +
                                    ", HTPH_CREATED_DATE)VALUES " +
                                    "('" + txtProgramId.Text.ToString() +
                                    "','" + Convert.ToDateTime(dtpPrgFrmDate.Value).ToString("dd/MMM/yyyy") +
                                    "','" + Convert.ToDateTime(dtpPrgToDate.Value).ToString("dd/MMM/yyyy") +
                                    "', '" + txtPrgName.Text.ToString().ToUpper() + 
                                    "','"+ strFrmType +"'";


                    if (cbTrainerType.SelectedIndex == 0)
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
                        strCommand += "INSERT INTO HR_TRAINING_PLANNER_DETL(HTPD_PROGRAM_NUMBER " +
                                                                        ", HTPD_TOPIC_ID " +
                                                                        ", HTPD_SL_NO " +                                                                        
                                                                        ", HTPD_METHOD_FLAG " +
                                                                        ", HTPD_TOPIC_DETAILS " +
                                                                        ", HTPD_TRAINER_FLAG "+
                                                                        ", HTPD_EXTERNAL_TRAINER_NAME "+
                                                                        ", HTTD_EXTERNAL_TRAINER_DETAILS "+
                                                                        ", HTPD_TRAINER_ECODE "+
                                                                        ", HTPD_START_TIME "+
                                                                        ", HTPD_END_TIME "+
                                                                        ", HTPD_REMARKS "+
                                                                        ", HTPD_TRN_TYPE "+
                                                                        ")VALUES('" + txtProgramId.Text.ToString() +
                                                                        "',"+ Convert.ToInt32(gvTopicsMethodDetl.Rows[i].Cells["TopicId"].Value) +
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
                                                                        "','"+ strFrmType +"')";
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
                        strCommand += "INSERT INTO HR_TRAINING_PLANNER_EMPDETL(HTPE_PROGRAM_NUMBER "+
                                                                            ", HTPE_EORA_CODE "+
                                                                            ", HTPE_COMPANY_CODE "+
                                                                            ", HTPE_BRANCH_CODE "+
                                                                            ", HTPE_TRN_TYPE "+
                                                                            ")VALUES('" + txtProgramId.Text.ToString() +
                                                                            "',"+ Convert.ToInt32(gvEmpDetails.Rows[i].Cells["Ecode"].Value) +
                                                                            ",'"+ gvEmpDetails.Rows[i].Cells["CompanyCode"].Value.ToString() +
                                                                            "','"+ gvEmpDetails.Rows[i].Cells["BranchCode"].Value.ToString() +
                                                                            "','"+ strFrmType +"')";
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

        private void cbTrainerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTrainerType.SelectedIndex == 0)
            {
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

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            if (txtEcodeSearch.Text != "")
            {

                try
                {
                    string strCommand = "SELECT MEMBER_NAME,desig_name FROM EORA_MASTER " +
                                        " INNER JOIN DESIG_MAS ON desig_code=DESG_ID "+
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
            else
            {
                txtEName.Text = "";
                txtEmpDesg.Text = "";
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

                    TrainingTopicMethodDetl TopicMethodDetl = new TrainingTopicMethodDetl(strType,Ecode, sEmpName, Desig);
                    TopicMethodDetl.objTrainingPlanningDetails = this;
                    TopicMethodDetl.ShowDialog();
                }
                else if (cbTrainerType.SelectedIndex == 1)
                {
                    strTrainerName = txtTrainerName.Text.ToString();
                    strTrainerdetl = txtTrainerDetl.Text.ToString();
                    strType = "EXTERNAL";

                    TrainingTopicMethodDetl TopicMethodDetl = new TrainingTopicMethodDetl(strType,strTrainerName, strTrainerdetl);
                    TopicMethodDetl.objTrainingPlanningDetails = this;
                    TopicMethodDetl.ShowDialog();
                }


            }

        }
        

        private void btnAddEmpDetails_Click(object sender, EventArgs e)
        {
            if (txtPrgName.Text != "")
            {
                TrainingPrgEmpDetails EmpDetl = new TrainingPrgEmpDetails();
                EmpDetl.objTrainingPlanningDetails = this;
                EmpDetl.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please Enter Program Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPrgName.Focus();
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
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
                        MessageBox.Show("Selected Data Deleted Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void gvTopicsMethodDetl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == gvTopicsMethodDetl.Columns["Edit"].Index)
                {
                    if (Convert.ToBoolean(gvTopicsMethodDetl.Rows[e.RowIndex].Cells["Edit"].Selected) == true)
                    {
                       
                        int SlNo = Convert.ToInt32(gvTopicsMethodDetl.Rows[e.RowIndex].Cells[gvTopicsMethodDetl.Columns["SLNO"].Index].Value);
                        DataRow[] dr = dtTopicMethodDetl.Select("SLNO=" + SlNo);

                        TrainingTopicMethodDetl TopicMethodDetl = new TrainingTopicMethodDetl(dr);
                        TopicMethodDetl.objTrainingPlanningDetails = this;
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            flagUpdate = false;

            GenerateProgramNumber();
            txtPrgName.Text = "";
            dtpPrgFrmDate.Value = DateTime.Today;
            dtpPrgToDate.Value = DateTime.Today;
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

        private void txtProgramId_Validated(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "", strUserId = "";


            if (txtProgramId.Text.Length > 0)
            {
               
                    strCmd = "SELECT HTPH_CREATED_BY FROM HR_TRAINING_PLANNER_HEAD WHERE HTPH_PROGRAM_NUMBER='" + txtProgramId.Text.ToString() + "'";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        strUserId = dt.Rows[0]["HTPH_CREATED_BY"].ToString();
                    }

                    if (CommonData.LogUserId.Equals(strUserId) || CommonData.LogUserId =="admin")
                    {

                        FillTrainingProgramDetails();
                    }
                    else
                    {
                        MessageBox.Show("Sorry! This Program Id Not Pertaining To You", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(null, null);
                        GenerateProgramNumber();
                        
                    }
               
            }
            else
            {
                btnCancel_Click(null, null);
            }
        }

        private void txtPrgName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtTrainerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtProgramId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }


        #region "GET DATA FOR UPDATE"
        private void FillTrainingProgramDetails()
        {
            objHRdb = new HRInfo();
            DataTable dtTrainingPrgHeadDetl;
            Hashtable ht;
            if (txtProgramId.Text != "")
            {
                try
                {
                    ht = objHRdb.GetTrainingPlanningDetails(txtProgramId.Text.ToString());
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
        }

        private void FillTrainingPrgHeadDetails(DataTable dtHead)
        {
            objHRdb = new HRInfo();
            Hashtable ht;
            DataTable dtTopicsDetl;
            DataTable dtEmpDetails;
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";

            if (txtProgramId.Text != "")
            {
                try
                {
                    ht = objHRdb.GetTrainingPlanningDetails(txtProgramId.Text.ToString());

                    dtTopicsDetl = (DataTable)ht["TrainingPrgTopicDetl"];
                    dtEmpDetails = (DataTable)ht["TrainingEmpDetl"];

                    if (dtHead.Rows.Count > 0)
                    {
                        flagUpdate = true;

                        strCmd = "select HTPH_PROGRAM_NUMBER from HR_TRAINING_PLANNER_HEAD "+
                                " WHERE HTPH_PROGRAM_NUMBER='"+ txtProgramId.Text +
                                "' and HTPH_PROGRAM_NUMBER in (SELECT HTAH_AGAINST_PROGRAM_NUMBER FROM HR_TRAINING_ACTUAL_HEAD)";
                        dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["HTPH_PROGRAM_NUMBER"].ToString() != null)
                            {
                                btnSave.Enabled = false;
                                btnDelete.Enabled = false;
                            }
                            else
                            {
                                btnSave.Enabled = true;
                                btnDelete.Enabled = true;
                            }
                        }



                        strFrmType = "";

                        txtPrgName.Text = dtHead.Rows[0]["ProgramName"].ToString();
                        dtpPrgFrmDate.Value = Convert.ToDateTime(dtHead.Rows[0]["ProgramFromDate"].ToString());
                        dtpPrgToDate.Value = Convert.ToDateTime(dtHead.Rows[0]["PrgToDate"].ToString());
                        strFrmType = dtHead.Rows[0]["Trn_Type"].ToString();

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
                        GenerateProgramNumber();

                        flagUpdate = false;
                        txtPrgName.Text = "";
                        dtpPrgFrmDate.Value = DateTime.Today;
                        dtpPrgToDate.Value = DateTime.Today;
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
            if (txtProgramId.Text!="")
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

        private void FillTrainingEmpDetails(DataTable dtEmp)
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();            
            string strDel = "";
            int iRes = 0;
            DataTable dt = new DataTable();
            if (txtProgramId.Text != "")
            {
                string strCmd = "SELECT * FROM HR_TRAINING_PLANNER_HEAD WHERE HTPH_PROGRAM_NUMBER='"+ txtProgramId.Text.ToString() +"'";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["HTPH_CREATED_BY"].ToString().Equals(CommonData.LogUserId) || CommonData.LogUserId =="admin")
                    {

                        DialogResult result = MessageBox.Show("Do you want to delete This Record ?",
                                            "SSERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                flagUpdate = true;
                                strDel = "DELETE FROM HR_TRAINING_PLANNER_EMPDETL WHERE HTPE_PROGRAM_NUMBER='" + txtProgramId.Text.ToString() + "'";
                                //iRes = objSQLdb.ExecuteSaveData(strDel);

                                strDel += "DELETE FROM HR_TRAINING_PLANNER_DETL WHERE HTPD_PROGRAM_NUMBER='" + txtProgramId.Text.ToString() + "'";
                                //iRes = objSQLdb.ExecuteSaveData(strDel);

                                strDel += "DELETE FROM HR_TRAINING_PLANNER_HEAD WHERE HTPH_PROGRAM_NUMBER='" + txtProgramId.Text.ToString() + "'";
                                iRes = objSQLdb.ExecuteSaveData(strDel);

                                if (iRes > 0)
                                {
                                    MessageBox.Show("Data Deleted Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    btnCancel_Click(null, null);
                                    flagUpdate = false;
                                }

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }

                        }
                        else
                        {
                            MessageBox.Show(" Data Not Deleted", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sorry! This Program Id Not Pertaining To You", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnCancel_Click(null,null);
                        GenerateProgramNumber();
                        txtPrgName.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter Valid Program Number","SSERP",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }

            }
           
        }

        private void txtPrgName_Validated(object sender, EventArgs e)
        {
            //try
            //{
            //    objSQLdb = new SQLDB();
            //    DataTable dt = objSQLdb.ExecuteDataSet("SELECT DISTINCT HTPH_PROGRAM_NAME "+
            //                                        " FROM HR_TRAINING_PLANNER_HEAD "+
            //                                        " WHERE HTPH_PROGRAM_NAME='"+ txtPrgName.Text.ToString() +
            //                                        "' and HTPH_TRN_TYPE='"+ strFrmType +"' ORDER BY HTPH_PROGRAM_NAME ASC").Tables[0];
            //    UtilityLibrary.AutoCompleteTextBox(txtPrgName, dt, "", "HTPH_PROGRAM_NAME");
            //    objSQLdb = null;
            //}
            //catch { }
            //finally { objSQLdb = null; }
        }

      

   

    }
}
