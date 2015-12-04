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
    public partial class TrainingFeedBackForm : Form
    {
        SQLDB objSQLdb = null;
        Int32 EmpApplNo = 0;
        public ActualProgramsForFeedBack objActualProgramsForFeedBack;
        string strPrgNo = "";
        string strPrgName = "";
        string strTrainerName = "";
        DateTime dtPrgDate;
        string sCompCode = "";
        string sBranchCode = "";
        bool flagUpdate = false;
        Int32 nEcode = 0;

        public TrainingFeedBackForm()
        {
            InitializeComponent();
        }
        public TrainingFeedBackForm(string ProgramNo,string PrgName,string TrainerName,DateTime prgDate)
        {
            InitializeComponent();
            strPrgNo = ProgramNo;
            strPrgName = PrgName;
            strTrainerName = TrainerName;
            dtPrgDate = prgDate;
        }

        private void TrainingFeedBackForm_Load(object sender, EventArgs e)
        {
            grouper2.Visible = false;
            grouper1.Visible = true;
            dtpPrgDate.Enabled = false;
            txtPrgName.ReadOnly = true;
            txtTrainerName.ReadOnly = true;
            cbFrmType.SelectedIndex = 0;
            btnDelete.Enabled = false;

            txtPrgName.Text = strPrgName;
            txtPrgName.Tag = strPrgNo;
            txtTrainerName.Text = strTrainerName;
            dtpPrgDate.Value = dtPrgDate;

            FillTrainedEmployees();
            
        }

        private DataSet GetTrainedEmployees(int iMode, string FromDate, string ToDate, string UserId, string sPrgNo, string sEcodeSearch)
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



        private void FillTrainedEmployees()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            //cbEmployees.DataSource = null;
            try
            {
                dt = GetTrainedEmployees(102, "", "", "", strPrgNo, txtEcodeSearch.Text.ToString()).Tables[0];

                if (dt.Rows.Count > 0)
                {                  

                    cbEmployees.DataSource = dt;
                    cbEmployees.DisplayMember = "EmpName";
                    cbEmployees.ValueMember = "Ecode";

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
      
        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {
                grouper1.Visible = false;
                grouper2.Visible = true;
            }
          
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            grouper1.Visible = true;
            grouper2.Visible = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        #region "CHECKING DATA"

        private bool CheckGrouper2Data()
        {
            bool bFlag = true;

            if (cbEmployees.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Employee", "Training Feed Back Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbEmployees.Focus();
                return bFlag;
                grouper1.Visible = true;
                grouper2.Visible = false;
            }

            if (chkCleanExc.Checked == false && chkCleanVG.Checked == false && chkCleanGood.Checked == false && chkCleanFair.Checked == false && chkCleanPoor.Checked == false)
            {
                bFlag = false;
                MessageBox.Show("Select Hall Cleanliness Rating", "Training Feed Back Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return bFlag;
            }

            if (chkProdDisExc.Checked == false && chkProdDisVG.Checked == false && chkProdDisGood.Checked == false && chkProdDisFair.Checked == false && chkProdDisPoor.Checked == false)
            {
                bFlag = false;
                MessageBox.Show("Select Product Display Rating In Program", "Training Feed Back Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return bFlag;
            }

            if (chkAVQuaExc.Checked == false && chkAVQuaVG.Checked == false && chkAVQuaGood.Checked == false && chkAVQuaFair.Checked == false && chkAVQuaPoor.Checked == false)
            {
                bFlag = false;
                MessageBox.Show("Select Audio/Video Quality Rating In Program", "Training Feed Back Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return bFlag;
            }
           
            if (chkOveFacExc.Checked == false && chkOveFacVG.Checked == false && chkOveFacGood.Checked == false && chkOveFacFair.Checked == false && chkOveFacPoor.Checked == false)
            {
                bFlag = false;
                MessageBox.Show("Select Overall Facility Rating In Program", "Training Feed Back Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return bFlag;
            }

            return bFlag;
        }


        private bool CheckData()
        {
            bool flag = true;

            if (cbEmployees.SelectedIndex==-1)
            {
                flag = false;
                MessageBox.Show("Please Select Employee","Training Feed Back Form",MessageBoxButtons.OK,MessageBoxIcon.Information);
                cbEmployees.Focus();
                return flag;
            }
            if (cbFrmType.SelectedIndex == 0)
            {
                if (chkSubExc.Checked == false && chkSubVG.Checked == false && chkSubGood.Checked == false && chkSubFair.Checked == false && chkSubPoor.Checked == false)
                {
                    flag = false;
                    MessageBox.Show("Select Program Subject is Relavant To You Or Not", "Training Feed Back Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return flag;
                }
            }

            if (rdbPrescriptive.Checked == false && rdbExperiental.Checked == false && rdbInteractive.Checked == false)
            {
                flag = false;
                MessageBox.Show("Please Select Trainer Delivery Process", "Training Feed Back Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }

            if (chkCoverageVG.Checked == false && chkCoverageExc.Checked == false && chkCoverageGood.Checked == false && chkCoverageFair.Checked == false && chkCoveragePoor.Checked == false)
            {
                flag = false;
                MessageBox.Show("Please Select Subject Coverage Rating In Program", "Training Feed Back Form", MessageBoxButtons.OK, MessageBoxIcon.Information);              
                return flag;
            }
           
            if (chkClarityExc.Checked == false && chkClarityVG.Checked == false && chkClarityGood.Checked == false && chkClarityFair.Checked == false && chkClarityPoor.Checked == false)
            {
                flag = false;
                MessageBox.Show("Please Select Subject Clarity Rating In Program", "Training Feed Back Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }
            if (chkPracRelExc.Checked == false && chkPracRelVG.Checked == false && chkPracRelGood.Checked == false && chkPracRelFair.Checked == false && chkPracRelPoor.Checked == false)
            {
                flag = false;
                MessageBox.Show("Please Select Subject Practical Relavance Rating In Program", "Training Feed Back Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }
            if (chkCommExc.Checked == false && chkCommVG.Checked == false && chkCommGood.Checked == false && chkCommFair.Checked == false && chkCommPoor.Checked == false)
            {
                flag = false;
                MessageBox.Show("Please Select Trainer Communication Rating In Program", "Training Feed Back Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }
            if (chkEnthusiasmExc.Checked == false && chkEnthusiasmVG.Checked == false && chkEnthusiasmGood.Checked == false && chkEnthusiasmFair.Checked == false && chkEnthusiasmPoor.Checked == false)
            {
                flag = false;
                MessageBox.Show("Please Select Trainer Enthusiasm Rating In Program", "Training Feed Back Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }

            if (chkSubKnowExc.Checked == false && chkSubKnowVG.Checked == false && chkSubKnowGood.Checked == false && chkSubKnowFair.Checked == false && chkSubKnowPoor.Checked == false)
            {
                flag = false;
                MessageBox.Show("Please Select Trainer Subject Knowledge Rating In Program", "Training Feed Back Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }

            if (chkFrndlExc.Checked == false && chkFrndlVG.Checked == false && chkFrndlGood.Checked == false && chkFrndlFair.Checked == false && chkFrndlPoor.Checked == false)
            {
                flag = false;
                MessageBox.Show("Please Select Trainer Friendliness Rating In Program", "Training Feed Back Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }
            if (chkTimeMgntExc.Checked == false && chkTimeMgntVG.Checked == false && chkTimeMgntGood.Checked == false && chkTimeMgntFair.Checked == false && chkTimeMgntPoor.Checked == false)
            {
                flag = false;
                MessageBox.Show("Please Select Trainer Time Management Rating In Program", "Training Feed Back Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }

            if (chkOvePrgExc.Checked == false && chkOvePrgVG.Checked == false && chkOvePrgGood.Checked == false && chkOvePrgFair.Checked == false && chkOvePrgPoor.Checked == false)
            {
                flag = false;
                MessageBox.Show("Please Select Overall Program Rating In Program", "Training Feed Back Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }
                      

            return flag;
        }
        #endregion


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckGrouper2Data() == true)
            {
              
                if (SaveTrainingFeedBackDetails() > 0)
                {
                    MessageBox.Show("Data Saved Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    flagUpdate = false;
                    btnCancel_Click(null, null);
                    txtEcodeSearch.Text = "";
                    FillTrainedEmployees();
                    FillTrainingEmpFeedBackDetails();                    
                    grouper1.Visible = true;
                    grouper2.Visible = false;
                    txtEcodeSearch.Focus();
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private Int32 SaveTrainingFeedBackDetails()
        {
            objSQLdb = new SQLDB();
            string strCommand = "";
            DataTable dt = new DataTable();
            int iRes = 0;
            string strDeliveryProcess = "";
            Int32 SubRating = 0;
            Int32 SubCovgRating = 0;
            Int32 SubClarRating = 0;
            Int32 SubPracRelRating = 0;
            Int32 TrainerCommRating = 0;
            Int32 TrainerEnthusRating = 0;
            Int32 TrainerSubKnowRating = 0;
            Int32 TrainerFrndlinessRating = 0;
            Int32 TrainerTimeMgntRating = 0;
            Int32 OvePrgRate = 0;
            Int32 RoomCleanRating = 0;
            Int32 ProdDispRate = 0;
            Int32 AvQualRate = 0;
            Int32 PrgOveFacRate = 0;
            

            try
            {
                if (rdbPrescriptive.Checked == true)
                {
                    strDeliveryProcess = "PRESCRIPTIVE";
                }
                else if (rdbExperiental.Checked == true)
                {
                    strDeliveryProcess = "EXPERIENTIAL";
                }
                else if (rdbInteractive.Checked == true)
                {
                    strDeliveryProcess = "INTERACTIVE";
                }

                SubRating = GetSubjectRating();
                SubCovgRating = GetSubjectCoverageRating();
                SubClarRating = GetSubjectClarityRatings();
                SubPracRelRating = GetSubPracticalRelvRating();
                TrainerCommRating = GetTrainerCOMMRating();
                TrainerEnthusRating = GetTrainerEnthuRating();
                TrainerSubKnowRating = GetTrainerSubKnowRating();
                TrainerFrndlinessRating = GetTrainerFrndlinessRating();
                TrainerTimeMgntRating = GetTrainerTimeMGNtRating();
                OvePrgRate = GetOverallPrgRating();
                RoomCleanRating = GetRoomCleanRating();
                ProdDispRate = GetProdDisplayRating();
                AvQualRate = GetProdAVRating();
                PrgOveFacRate = GetOvePrgFacilityRating();


                //string strCmd = "SELECT HTAE_EORA_CODE FROM HR_TRAINING_ACTUAL_EMPDETL "+
                //                " WHERE HTAE_EORA_CODE="+ Convert.ToInt32(cbEmployees.SelectedValue) +
                //                " AND HTAE_ACTUAL_PROGRAM_NUMBER='"+ txtPrgName.Tag.ToString() +"' ";
                //dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                //if (dt.Rows.Count == 0)
                //{
                //    string strInsert = "INSERT INTO HR_TRAINING_ACTUAL_EMPDETL(HTAE_ACTUAL_PROGRAM_NUMBER "+
                //                                                            ", HTAE_EORA_CODE "+
                //                                                            ", HTAE_COMPANY_CODE "+
                //                                                            ", HTAE_BRANCH_CODE "+
                //                                                            ")VALUES('"+ txtPrgName.Tag.ToString() +
                //                                                            "',"+ Convert.ToInt32(txtEcodeSearch.Text) +
                //                                                            ",'"+ sCompCode +
                //                                                            "','"+ sBranchCode +"')";

                //    int iRec = objSQLdb.ExecuteSaveData(strInsert);
                //}

                

                if (flagUpdate == true)
                {
                    strCommand = "UPDATE HR_TRAINING_FEEDBACK SET HTF_DELIVERY_PROCESS='"+ strDeliveryProcess +
                                                                "',HTF_PROG_SUBJECT_RATING ="+ SubRating +
                                                                ",HTF_PROG_COVG_RATING="+ SubCovgRating +
                                                                ",HTF_PROG_CLARITY_RATING="+ SubClarRating +
                                                                ",HTF_PROG_PRACT_RATING="+ SubPracRelRating +
                                                                ",HTF_TR_COMM_RATING="+ TrainerCommRating +
                                                                ",HTF_TR_ENTH_RATING="+ TrainerEnthusRating +
                                                                ",HTF_TR_FRIENDLY_RATING="+ TrainerFrndlinessRating +
                                                                ",HTF_TR_SUBKNOW_RATING="+ TrainerSubKnowRating +
                                                                ",HTF_TR_TIMEMNG_RATING="+ TrainerTimeMgntRating +
                                                                ",HTF_OVERALL_PROG_RATING="+ OvePrgRate +
                                                                ",HTF_IMPL_THING1='"+ txtImplement1.Text.ToString() +
                                                                "',HTF_IMPL_THING2='"+ txtImplement2.Text.ToString() +
                                                                "',HTF_IMPL_THING3='"+ txtImplement3.Text.ToString() +
                                                                "',HTF_SUGGESTION1='"+ txtSuggestion1.Text.ToString() +
                                                                "',HTF_SUGGESTION2='"+ txtSuggestion2.Text.ToString() +
                                                                "',HTF_SUGGESTION3='"+ txtSuggestion3.Text.ToString() +
                                                                "',HTF_HALL_CLEAN_RATING="+ RoomCleanRating +
                                                                ",HTF_HALL_DISPLAY_RATING="+ ProdDispRate +
                                                                ",HTF_HALL_AV_RATING="+ AvQualRate +
                                                                ",HTF_HALL_FAC_RATING="+ PrgOveFacRate +
                                                                ",HTF_OVR_PROG_CONTENT='"+ txtPrgContent.Text.ToString() +
                                                                "',HTF_OVR_PROG_INFR='"+ txtInfrastrFeedBack.Text.ToString() +
                                                                "',HTF_OVR_PROG_FACULTY='"+ txtFacultyFeedBack.Text.ToString() +
                                                                "',HTF_OTHER_TOPICS='"+ txtOtherSuggestions.Text.ToString() +
                                                                "',HTF_MODIFIED_BY='"+ CommonData.LogUserId +
                                                                "',HTF_MODIFIED_DATE=getdate() "+
                                                                " WHERE HTF_PROGRAM_NUMBER='" + txtPrgName.Tag.ToString() +
                                                                "' AND HTF_APPL_NUMBER="+ EmpApplNo +"";

                    
                }
                else if(flagUpdate==false)
                {
                    strCommand = "INSERT INTO HR_TRAINING_FEEDBACK(HTF_PROGRAM_NUMBER " +
                                                                ", HTF_PROGRAM_NAME " +
                                                                ", HTF_EORA_CODE " +
                                                                ", HTF_APPL_NUMBER " +
                                                                ", HTF_DELIVERY_PROCESS " +
                                                                ", HTF_PROG_SUBJECT_RATING " +
                                                                ", HTF_PROG_COVG_RATING " +
                                                                ", HTF_PROG_CLARITY_RATING " +
                                                                ", HTF_PROG_PRACT_RATING " +
                                                                ", HTF_TR_COMM_RATING " +
                                                                ", HTF_TR_ENTH_RATING " +
                                                                ", HTF_TR_SUBKNOW_RATING " +
                                                                ", HTF_TR_FRIENDLY_RATING " +
                                                                ", HTF_TR_TIMEMNG_RATING " +
                                                                ", HTF_OVERALL_PROG_RATING " +
                                                                ", HTF_IMPL_THING1 " +
                                                                ", HTF_IMPL_THING2 " +
                                                                ", HTF_IMPL_THING3 " +
                                                                ", HTF_SUGGESTION1 " +
                                                                ", HTF_SUGGESTION2 " +
                                                                ", HTF_SUGGESTION3 " +
                                                                ", HTF_HALL_CLEAN_RATING " +
                                                                ", HTF_HALL_DISPLAY_RATING " +
                                                                ", HTF_HALL_AV_RATING " +
                                                                ", HTF_HALL_FAC_RATING " +
                                                                ", HTF_OVR_PROG_CONTENT " +
                                                                ", HTF_OVR_PROG_FACULTY " +
                                                                ", HTF_OVR_PROG_INFR " +
                                                                ", HTF_OTHER_TOPICS " +
                                                                ", HTF_CREATED_BY " +
                                                                ", HTF_CREATED_DATE " +
                                                                ")VALUES('" + txtPrgName.Tag +
                                                                "','" + txtPrgName.Text.ToString() +
                                                                "'," + nEcode +
                                                                "," + EmpApplNo +
                                                                ",'" + strDeliveryProcess +
                                                                "'," + SubRating +
                                                                "," + SubCovgRating +
                                                                "," + SubClarRating +
                                                                "," + SubPracRelRating +
                                                                "," + TrainerCommRating +
                                                                "," + TrainerEnthusRating +
                                                                "," + TrainerSubKnowRating +
                                                                "," + TrainerFrndlinessRating +
                                                                "," + TrainerTimeMgntRating +
                                                                "," + OvePrgRate +
                                                                ",'" + txtImplement1.Text.ToString() +
                                                                "','" + txtImplement2.Text.ToString() +
                                                                "','" + txtImplement3.Text.ToString() +
                                                                "','" + txtSuggestion1.Text.ToString() +
                                                                "','" + txtSuggestion2.Text.ToString() +
                                                                "','" + txtSuggestion3.Text.ToString() +
                                                                "'," + RoomCleanRating +
                                                                "," + ProdDispRate +
                                                                "," + AvQualRate +
                                                                "," + PrgOveFacRate +
                                                                ",'" + txtPrgContent.Text.ToString() +
                                                                "','" + txtFacultyFeedBack.Text.ToString() +
                                                                "','" + txtInfrastrFeedBack.Text.ToString() +
                                                                "','" + txtOtherSuggestions.Text.ToString() +
                                                                "','" + CommonData.LogUserId +
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

        private Int32 GetSubjectRating()
        {
            Int32 SubRating = 0;
            if (cbFrmType.SelectedIndex == 0)
            {
                if (chkSubExc.Checked == true)
                {
                    SubRating = 5;
                }
                else if (chkSubVG.Checked == true)
                {
                    SubRating = 4;
                }
                else if (chkSubGood.Checked == true)
                {
                    SubRating = 3;
                }
                else if (chkSubFair.Checked == true)
                {
                    SubRating = 2;
                }
                else if (chkSubPoor.Checked == true)
                {
                    SubRating = 1;
                }
            }
            else
            {
                SubRating = 0;
            }

            return SubRating;
        }

        private Int32 GetSubjectCoverageRating()
        {
            Int32 PrgSubCoverageRating = 0;
            

            if (chkCoverageExc.Checked == true)
            {
                PrgSubCoverageRating = 5;
            }
            else if (chkCoverageVG.Checked == true)
            {
                PrgSubCoverageRating = 4;
            }
            else if (chkCoverageGood.Checked == true)
            {
                PrgSubCoverageRating = 3;
            }
            else if (chkCoverageFair.Checked == true)
            {
                PrgSubCoverageRating = 2;
            }
            else if (chkCoveragePoor.Checked == true)
            {
                PrgSubCoverageRating = 1;
            }

            return PrgSubCoverageRating;
        }
     
        private Int32 GetSubjectClarityRatings()
        {            
            Int32 PrgClarityRating = 0;

            if (chkClarityExc.Checked == true)
            {
                PrgClarityRating = 5;
            }
            else if (chkClarityVG.Checked == true)
            {
                PrgClarityRating = 4;
            }
            else if (chkClarityGood.Checked == true)
            {
                PrgClarityRating = 3;
            }
            else if (chkClarityFair.Checked == true)
            {
                PrgClarityRating = 2;
            }
            else if (chkClarityPoor.Checked == true)
            {
                PrgClarityRating = 1;
            }

            return PrgClarityRating;
        }

        private Int32 GetSubPracticalRelvRating()
        {
            Int32 PrgSubPracRelRating = 0;

            if (chkPracRelExc.Checked == true)
            {
                PrgSubPracRelRating = 5;
            }
            else if (chkPracRelVG.Checked == true)
            {
                PrgSubPracRelRating = 4;
            }
            else if (chkPracRelGood.Checked == true)
            {
                PrgSubPracRelRating = 3;
            }
            else if (chkPracRelFair.Checked == true)
            {
                PrgSubPracRelRating = 2;
            }
            else if (chkPracRelPoor.Checked == true)
            {
                PrgSubPracRelRating = 1;
            }

            return PrgSubPracRelRating;
        }

        private Int32 GetTrainerCOMMRating()
        {
            Int32 TrainerCommRating = 0;

            if (chkCommExc.Checked == true)
            {
                TrainerCommRating = 5;
            }
            else if (chkCommVG.Checked == true)
            {
                TrainerCommRating = 4;
            }
            else if (chkCommGood.Checked == true)
            {
                TrainerCommRating = 3;
            }
            else if (chkCommFair.Checked == true)
            {
                TrainerCommRating = 2;
            }
            else if (chkCommPoor.Checked == true)
            {
                TrainerCommRating = 1;
            }

            return TrainerCommRating;
        }

        private Int32 GetTrainerEnthuRating()
        {
            Int32 TrainerEnthusRating = 0;

            if (chkEnthusiasmExc.Checked == true)
            {
                TrainerEnthusRating = 5;
            }
            else if (chkEnthusiasmVG.Checked == true)
            {
                TrainerEnthusRating = 4;
            }
            else if (chkEnthusiasmGood.Checked == true)
            {
                TrainerEnthusRating = 3;
            }
            else if (chkEnthusiasmFair.Checked == true)
            {
                TrainerEnthusRating = 2;
            }
            else if (chkEnthusiasmPoor.Checked == true)
            {
                TrainerEnthusRating = 1;
            }

            return TrainerEnthusRating;
        }

        private Int32 GetTrainerSubKnowRating()
        {
            Int32 TrainerSubKnowRating = 0;

            if (chkSubKnowExc.Checked == true)
            {
                TrainerSubKnowRating = 5;
            }
            else if (chkSubKnowVG.Checked == true)
            {
                TrainerSubKnowRating = 4;
            }
            else if (chkSubKnowGood.Checked == true)
            {
                TrainerSubKnowRating = 3;
            }
            else if (chkSubKnowFair.Checked == true)
            {
                TrainerSubKnowRating = 2;
            }
            else if (chkSubKnowPoor.Checked == true)
            {
                TrainerSubKnowRating = 1;
            }

            return TrainerSubKnowRating;
        }

        private Int32 GetTrainerFrndlinessRating()
        {
            Int32 TrainerFrndlinessRate = 0;

            if (chkFrndlExc.Checked == true)
            {
                TrainerFrndlinessRate = 5;
            }
            else if (chkFrndlVG.Checked == true)
            {
                TrainerFrndlinessRate = 4;
            }
            else if (chkFrndlGood.Checked == true)
            {
                TrainerFrndlinessRate = 3;
            }
            else if (chkFrndlFair.Checked == true)
            {
                TrainerFrndlinessRate = 2;
            }
            else if (chkFrndlPoor.Checked == true)
            {
                TrainerFrndlinessRate = 1;
            }

            return TrainerFrndlinessRate;
        }

        private Int32 GetTrainerTimeMGNtRating()
        {
            Int32 TrainerTimeMgntRate = 0;

            if (chkTimeMgntExc.Checked == true)
            {
                TrainerTimeMgntRate = 5;
            }
            else if (chkTimeMgntVG.Checked == true)
            {
                TrainerTimeMgntRate = 4;
            }
            else if (chkTimeMgntGood.Checked == true)
            {
                TrainerTimeMgntRate = 3;
            }
            else if (chkTimeMgntFair.Checked == true)
            {
                TrainerTimeMgntRate = 2;
            }
            else if (chkTimeMgntPoor.Checked == true)
            {
                TrainerTimeMgntRate = 1;
            }

            return TrainerTimeMgntRate;
        }

        private Int32 GetOverallPrgRating()
        {
            Int32 OvePrgRate = 0;

            if (chkOvePrgExc.Checked == true)
            {
                OvePrgRate = 5;
            }
            else if (chkOvePrgVG.Checked == true)
            {
                OvePrgRate = 4;
            }
            else if (chkOvePrgGood.Checked == true)
            {
                OvePrgRate = 3;
            }
            else if (chkOvePrgFair.Checked == true)
            {
                OvePrgRate = 2;
            }
            else if (chkOvePrgPoor.Checked == true)
            {
                OvePrgRate = 1;
            }

            return OvePrgRate;
        }

        private Int32 GetRoomCleanRating()
        {
            Int32 RoomCleanRate = 0;

            if (chkCleanExc.Checked == true)
            {
                RoomCleanRate = 5;
            }
            else if (chkCleanVG.Checked == true)
            {
                RoomCleanRate = 4;
            }
            else if (chkCleanGood.Checked == true)
            {
                RoomCleanRate = 3;
            }
            else if (chkCleanFair.Checked == true)
            {
                RoomCleanRate = 2;
            }
            else if (chkCleanPoor.Checked == true)
            {
                RoomCleanRate = 1;
            }

            return RoomCleanRate;
        }

        private Int32 GetProdDisplayRating()
        {
            Int32 ProdDisplayRate = 0;

            if (chkProdDisExc.Checked == true)
            {
                ProdDisplayRate = 5;
            }
            else if (chkProdDisVG.Checked == true)
            {
                ProdDisplayRate = 4;
            }
            else if (chkProdDisGood.Checked == true)
            {
                ProdDisplayRate = 3;
            }
            else if (chkProdDisFair.Checked == true)
            {
                ProdDisplayRate = 2;
            }
            else if (chkProdDisPoor.Checked == true)
            {
                ProdDisplayRate = 1;
            }

            return ProdDisplayRate;
        }

        private Int32 GetProdAVRating()
        {
            Int32 ProdAVRate = 0;

            if (chkAVQuaExc.Checked == true)
            {
                ProdAVRate = 5;
            }
            else if (chkAVQuaVG.Checked == true)
            {
                ProdAVRate = 4;
            }
            else if (chkAVQuaGood.Checked == true)
            {
                ProdAVRate = 3;
            }
            else if (chkAVQuaFair.Checked == true)
            {
                ProdAVRate = 2;
            }
            else if (chkAVQuaPoor.Checked == true)
            {
                ProdAVRate = 1;
            }

            return ProdAVRate;
        }

        private Int32 GetOvePrgFacilityRating()
        {
            Int32 OvePrgFacRating = 0;

            if (chkOveFacExc.Checked == true)
            {
                OvePrgFacRating = 5;
            }
            else if (chkOveFacVG.Checked == true)
            {
                OvePrgFacRating = 4;
            }
            else if (chkOveFacGood.Checked == true)
            {
                OvePrgFacRating = 3;
            }
            else if (chkOveFacFair.Checked == true)
            {
                OvePrgFacRating = 2;
            }
            else if (chkOveFacPoor.Checked == true)
            {
                OvePrgFacRating = 1;
            }

            return OvePrgFacRating;
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            chkSubExc.Checked = false;
            chkSubGood.Checked = false;
            chkSubVG.Checked = false;
            chkSubFair.Checked = false;
            chkSubPoor.Checked = false;

            chkOvePrgExc.Checked = false;
            chkOvePrgVG.Checked = false;
            chkOvePrgGood.Checked = false;
            chkOvePrgFair.Checked = false;
            chkOvePrgPoor.Checked = false;

            txtImplement1.Text = "";
            txtImplement2.Text = "";
            txtImplement3.Text = "";

            txtSuggestion1.Text = "";
            txtSuggestion2.Text = "";
            txtSuggestion3.Text = "";
            txtOtherSuggestions.Text = "";

            txtPrgContent.Text = "";
          
            txtInfrastrFeedBack.Text = "";
            //txtEName.Text = "";
            txtEmpDesg.Text = "";

          
            txtFacultyFeedBack.Text = "";
         
            

            chkCoverageVG.Checked = false;
            chkCoverageGood.Checked = false;
            chkCoverageFair.Checked = false;
            chkCoveragePoor.Checked = false;
            chkCoverageExc.Checked = false;

            chkClarityVG.Checked = false;
            chkClarityGood.Checked = false;
            chkClarityFair.Checked = false;
            chkClarityPoor.Checked = false;
            chkClarityExc.Checked = false;

            chkPracRelExc.Checked = false;
            chkPracRelVG.Checked = false;
            chkPracRelGood.Checked = false;
            chkPracRelFair.Checked = false;
            chkPracRelPoor.Checked = false;

            chkCommVG.Checked = false;
            chkCommPoor.Checked = false;
            chkCommGood.Checked = false;
            chkCommFair.Checked = false;
            chkCommExc.Checked = false;

            chkEnthusiasmExc.Checked = false;
            chkEnthusiasmVG.Checked = false;
            chkEnthusiasmGood.Checked = false;
            chkEnthusiasmFair.Checked = false;
            chkEnthusiasmPoor.Checked = false;

            chkSubKnowExc.Checked = false;
            chkSubKnowVG.Checked = false;
            chkSubKnowGood.Checked = false;
            chkSubKnowFair.Checked = false;
            chkSubKnowPoor.Checked = false;

            chkFrndlExc.Checked = false;
            chkFrndlVG.Checked = false;
            chkFrndlGood.Checked = false;
            chkFrndlFair.Checked = false;
            chkFrndlPoor.Checked = false;

            chkTimeMgntExc.Checked = false;
            chkTimeMgntVG.Checked = false;
            chkTimeMgntPoor.Checked = false;
            chkTimeMgntGood.Checked = false;
            chkTimeMgntFair.Checked = false;

            chkCleanExc.Checked = false;
            chkCleanVG.Checked = false;
            chkCleanGood.Checked = false;
            chkCleanFair.Checked = false;
            chkCleanPoor.Checked = false;

            chkProdDisExc.Checked = false;
            chkProdDisVG.Checked = false;
            chkProdDisPoor.Checked = false;
            chkProdDisGood.Checked = false;
            chkProdDisFair.Checked = false;

            chkAVQuaExc.Checked = false;
            chkAVQuaVG.Checked = false;
            chkAVQuaPoor.Checked = false;
            chkAVQuaGood.Checked = false;
            chkAVQuaFair.Checked = false;

            chkOveFacExc.Checked = false;
            chkOveFacVG.Checked = false;
            chkOveFacPoor.Checked = false;
            chkOveFacGood.Checked = false;
            chkOveFacFair.Checked = false;


        }

        private void chkCoverageExc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCoverageExc.Checked == true)
            {
                chkCoverageVG.Checked = false;
                chkCoverageGood.Checked = false;
                chkCoverageFair.Checked = false;
                chkCoveragePoor.Checked = false;
            }

        }

        private void chkCoverageVG_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCoverageVG.Checked == true)
            {
                chkCoverageExc.Checked = false;
                chkCoverageGood.Checked = false;
                chkCoverageFair.Checked = false;
                chkCoveragePoor.Checked = false;
            }
        }

        private void chkCoverageGood_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCoverageGood.Checked == true)
            {
                chkCoverageExc.Checked = false;
                chkCoverageVG.Checked = false;
                chkCoverageFair.Checked = false;
                chkCoveragePoor.Checked = false;
            }
        }

        private void chkCoverageFair_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCoverageFair.Checked == true)
            {
                chkCoverageExc.Checked = false;
                chkCoverageVG.Checked = false;
                chkCoverageGood.Checked = false;
                chkCoveragePoor.Checked = false;
            }
        }

        private void chkCoveragePoor_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCoveragePoor.Checked == true)
            {
                chkCoverageExc.Checked = false;
                chkCoverageVG.Checked = false;
                chkCoverageGood.Checked = false;
                chkCoverageFair.Checked = false;
            }
        }

        private void chkClarityExc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkClarityExc.Checked == true)
            {
                chkClarityVG.Checked = false;
                chkClarityGood.Checked = false;
                chkClarityFair.Checked = false;
                chkClarityPoor.Checked = false;
            }
        }

        private void chkClarityVG_CheckedChanged(object sender, EventArgs e)
        {
            if (chkClarityVG.Checked == true)
            {
                chkClarityExc.Checked = false;
                chkClarityGood.Checked = false;
                chkClarityFair.Checked = false;
                chkClarityPoor.Checked = false;
            }
        }

        private void chkClarityGood_CheckedChanged(object sender, EventArgs e)
        {
            if (chkClarityGood.Checked == true)
            {
                chkClarityExc.Checked = false;
                chkClarityVG.Checked = false;
                chkClarityFair.Checked = false;
                chkClarityPoor.Checked = false;
            }
        }

        private void chkClarityFair_CheckedChanged(object sender, EventArgs e)
        {
            if (chkClarityFair.Checked == true)
            {
                chkClarityExc.Checked = false;
                chkClarityVG.Checked = false;
                chkClarityGood.Checked = false;
                chkClarityPoor.Checked = false;
            }
        }

        private void chkClarityPoor_CheckedChanged(object sender, EventArgs e)
        {
            if (chkClarityPoor.Checked == true)
            {
                chkClarityExc.Checked = false;
                chkClarityVG.Checked = false;
                chkClarityGood.Checked = false;
                chkClarityFair.Checked = false;
            }
        }

        private void chkPracRelExc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPracRelExc.Checked == true)
            {
                chkPracRelVG.Checked = false;
                chkPracRelGood.Checked = false;
                chkPracRelFair.Checked = false;
                chkPracRelPoor.Checked = false;
            }

        }

        private void chkPracRelVG_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPracRelVG.Checked == true)
            {
                chkPracRelExc.Checked = false;
                chkPracRelGood.Checked = false;
                chkPracRelFair.Checked = false;
                chkPracRelPoor.Checked = false;
            }
        }

        private void chkPracRelGood_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPracRelGood.Checked == true)
            {
                chkPracRelExc.Checked = false;
                chkPracRelVG.Checked = false;
                chkPracRelFair.Checked = false;
                chkPracRelPoor.Checked = false;
            }
        }

        private void chkPracRelFair_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPracRelFair.Checked == true)
            {
                chkPracRelExc.Checked = false;
                chkPracRelVG.Checked = false;
                chkPracRelGood.Checked = false;
                chkPracRelPoor.Checked = false;
            }
        }

        private void chkPracRelPoor_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPracRelPoor.Checked == true)
            {
                chkPracRelExc.Checked = false;
                chkPracRelVG.Checked = false;
                chkPracRelGood.Checked = false;
                chkPracRelFair.Checked = false;
            }

        }

        private void chkCommExc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCommExc.Checked == true)
            {
                chkCommVG.Checked = false;
                chkCommPoor.Checked = false;
                chkCommGood.Checked = false;
                chkCommFair.Checked = false;
            }
        }

        private void chkCommVG_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCommVG.Checked == true)
            {
                chkCommExc.Checked = false;
                chkCommPoor.Checked = false;
                chkCommGood.Checked = false;
                chkCommFair.Checked = false;
            }
        }

        private void chkCommGood_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCommGood.Checked == true)
            {
                chkCommExc.Checked = false;
                chkCommPoor.Checked = false;
                chkCommVG.Checked = false;
                chkCommFair.Checked = false;
            }
        }

        private void chkCommFair_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCommFair.Checked == true)
            {
                chkCommExc.Checked = false;
                chkCommPoor.Checked = false;
                chkCommVG.Checked = false;
                chkCommGood.Checked = false;
            }
        }

        private void chkCommPoor_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCommPoor.Checked == true)
            {
                chkCommExc.Checked = false;
                chkCommFair.Checked = false;
                chkCommVG.Checked = false;
                chkCommGood.Checked = false;
            }
        }

        private void chkEnthusiasmExc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnthusiasmExc.Checked == true)
            {
                chkEnthusiasmVG.Checked = false;
                chkEnthusiasmGood.Checked = false;
                chkEnthusiasmFair.Checked = false;
                chkEnthusiasmPoor.Checked = false;
                
            }
        }

        private void chkEnthusiasmVG_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnthusiasmVG.Checked == true)
            {
                chkEnthusiasmExc.Checked = false;
                chkEnthusiasmGood.Checked = false;
                chkEnthusiasmFair.Checked = false;
                chkEnthusiasmPoor.Checked = false;

            }
        }

        private void chkEnthusiasmGood_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnthusiasmGood.Checked == true)
            {
                chkEnthusiasmExc.Checked = false;
                chkEnthusiasmVG.Checked = false;
                chkEnthusiasmFair.Checked = false;
                chkEnthusiasmPoor.Checked = false;

            }
        }

        private void chkEnthusiasmFair_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnthusiasmFair.Checked == true)
            {
                chkEnthusiasmExc.Checked = false;
                chkEnthusiasmVG.Checked = false;
                chkEnthusiasmGood.Checked = false;
                chkEnthusiasmPoor.Checked = false;

            }
        }

        private void chkEnthusiasmPoor_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnthusiasmPoor.Checked == true)
            {
                chkEnthusiasmExc.Checked = false;
                chkEnthusiasmVG.Checked = false;
                chkEnthusiasmGood.Checked = false;
                chkEnthusiasmFair.Checked = false;

            }
        }

        private void chkSubKnowExc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSubKnowExc.Checked == true)
            {
                chkSubKnowVG.Checked = false;
                chkSubKnowGood.Checked = false;
                chkSubKnowFair.Checked = false;
                chkSubKnowPoor.Checked = false;

            }
        }

        private void chkSubKnowVG_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSubKnowVG.Checked == true)
            {
                chkSubKnowExc.Checked = false;
                chkSubKnowGood.Checked = false;
                chkSubKnowFair.Checked = false;
                chkSubKnowPoor.Checked = false;

            }

        }

        private void chkSubKnowGood_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSubKnowGood.Checked == true)
            {
                chkSubKnowExc.Checked = false;
                chkSubKnowVG.Checked = false;
                chkSubKnowFair.Checked = false;
                chkSubKnowPoor.Checked = false;

            }

        }

        private void chkSubKnowFair_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSubKnowFair.Checked == true)
            {
                chkSubKnowExc.Checked = false;
                chkSubKnowVG.Checked = false;
                chkSubKnowGood.Checked = false;
                chkSubKnowPoor.Checked = false;

            }

        }

        private void chkSubKnowPoor_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSubKnowPoor.Checked == true)
            {
                chkSubKnowExc.Checked = false;
                chkSubKnowVG.Checked = false;
                chkSubKnowGood.Checked = false;
                chkSubKnowFair.Checked = false;

            }
        }

        private void chkFrndlExc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFrndlExc.Checked == true)
            {
                chkFrndlVG.Checked = false;
                chkFrndlGood.Checked = false;
                chkFrndlFair.Checked = false;
                chkFrndlPoor.Checked = false;
                
            }
        }

        private void chkFrndlVG_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFrndlVG.Checked == true)
            {
                chkFrndlExc.Checked = false;
                chkFrndlGood.Checked = false;
                chkFrndlFair.Checked = false;
                chkFrndlPoor.Checked = false;

            }
        }

        private void chkFrndlGood_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFrndlGood.Checked == true)
            {
                chkFrndlExc.Checked = false;
                chkFrndlVG.Checked = false;
                chkFrndlFair.Checked = false;
                chkFrndlPoor.Checked = false;

            }
        }

        private void chkFrndlFair_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFrndlFair.Checked == true)
            {
                chkFrndlExc.Checked = false;
                chkFrndlVG.Checked = false;
                chkFrndlGood.Checked = false;
                chkFrndlPoor.Checked = false;

            }

        }

        private void chkFrndlPoor_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFrndlPoor.Checked == true)
            {
                chkFrndlExc.Checked = false;
                chkFrndlVG.Checked = false;
                chkFrndlGood.Checked = false;
                chkFrndlFair.Checked = false;

            }


        }

        private void chkTimeMgntExc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTimeMgntExc.Checked == true)
            {
                chkTimeMgntVG.Checked = false;
                chkTimeMgntPoor.Checked = false;
                chkTimeMgntGood.Checked = false;
                chkTimeMgntFair.Checked = false;

            }

        }

        private void chkTimeMgntVG_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTimeMgntVG.Checked == true)
            {
                chkTimeMgntExc.Checked = false;
                chkTimeMgntPoor.Checked = false;
                chkTimeMgntGood.Checked = false;
                chkTimeMgntFair.Checked = false;

            }
        }

        private void chkTimeMgntGood_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTimeMgntGood.Checked == true)
            {
                chkTimeMgntExc.Checked = false;
                chkTimeMgntPoor.Checked = false;
                chkTimeMgntVG.Checked = false;
                chkTimeMgntFair.Checked = false;

            }
        }

        private void chkTimeMgntFair_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTimeMgntFair.Checked == true)
            {
                chkTimeMgntExc.Checked = false;
                chkTimeMgntPoor.Checked = false;
                chkTimeMgntVG.Checked = false;
                chkTimeMgntGood.Checked = false;

            }
        }

        private void chkTimeMgntPoor_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTimeMgntPoor.Checked == true)
            {
                chkTimeMgntExc.Checked = false;
                chkTimeMgntFair.Checked = false;
                chkTimeMgntVG.Checked = false;
                chkTimeMgntGood.Checked = false;

            }
        }

        private void chkCleanExc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCleanExc.Checked == true)
            {
                chkCleanVG.Checked = false;
                chkCleanGood.Checked = false;
                chkCleanFair.Checked = false;
                chkCleanPoor.Checked = false;

            }

        }

        private void chkCleanVG_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCleanVG.Checked == true)
            {
                chkCleanExc.Checked = false;
                chkCleanGood.Checked = false;
                chkCleanFair.Checked = false;
                chkCleanPoor.Checked = false;

            }
        }

        private void chkCleanGood_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCleanGood.Checked == true)
            {
                chkCleanExc.Checked = false;
                chkCleanVG.Checked = false;
                chkCleanFair.Checked = false;
                chkCleanPoor.Checked = false;

            }
        }

        private void chkCleanFair_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCleanFair.Checked == true)
            {
                chkCleanExc.Checked = false;
                chkCleanVG.Checked = false;
                chkCleanGood.Checked = false;
                chkCleanPoor.Checked = false;

            }
        }

        private void chkCleanPoor_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCleanPoor.Checked == true)
            {
                chkCleanExc.Checked = false;
                chkCleanVG.Checked = false;
                chkCleanGood.Checked = false;
                chkCleanFair.Checked = false;

            }

        }

        private void chkProdDisExc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProdDisExc.Checked == true)
            {
                chkProdDisVG.Checked = false;
                chkProdDisPoor.Checked = false;
                chkProdDisGood.Checked = false;
                chkProdDisFair.Checked = false;

            }
        }

        private void chkProdDisVG_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProdDisVG.Checked == true)
            {
                chkProdDisExc.Checked = false;
                chkProdDisPoor.Checked = false;
                chkProdDisGood.Checked = false;
                chkProdDisFair.Checked = false;

            }

        }

        private void chkProdDisGood_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProdDisGood.Checked == true)
            {
                chkProdDisExc.Checked = false;
                chkProdDisPoor.Checked = false;
                chkProdDisVG.Checked = false;
                chkProdDisFair.Checked = false;

            }
        }

        private void chkProdDisFair_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProdDisFair.Checked == true)
            {
                chkProdDisExc.Checked = false;
                chkProdDisPoor.Checked = false;
                chkProdDisVG.Checked = false;
                chkProdDisGood.Checked = false;

            }
        }

        private void chkProdDisPoor_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProdDisPoor.Checked == true)
            {
                chkProdDisExc.Checked = false;
                chkProdDisFair.Checked = false;
                chkProdDisVG.Checked = false;
                chkProdDisGood.Checked = false;

            }

        }

        private void chkAVQuaExc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAVQuaExc.Checked == true)
            {
                chkAVQuaVG.Checked = false;
                chkAVQuaPoor.Checked = false;
                chkAVQuaGood.Checked = false;
                chkAVQuaFair.Checked = false;

            }

        }

        private void chkAVQuaVG_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAVQuaVG.Checked == true)
            {
                chkAVQuaExc.Checked = false;
                chkAVQuaPoor.Checked = false;
                chkAVQuaGood.Checked = false;
                chkAVQuaFair.Checked = false;

            }
        }

        private void chkAVQuaGood_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAVQuaGood.Checked == true)
            {
                chkAVQuaExc.Checked = false;
                chkAVQuaPoor.Checked = false;
                chkAVQuaVG.Checked = false;
                chkAVQuaFair.Checked = false;

            }
        }

        private void chkAVQuaFair_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAVQuaFair.Checked == true)
            {
                chkAVQuaExc.Checked = false;
                chkAVQuaPoor.Checked = false;
                chkAVQuaVG.Checked = false;
                chkAVQuaGood.Checked = false;

            }
        }

        private void chkAVQuaPoor_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAVQuaPoor.Checked == true)
            {
                chkAVQuaExc.Checked = false;
                chkAVQuaFair.Checked = false;
                chkAVQuaVG.Checked = false;
                chkAVQuaGood.Checked = false;

            }
        }

        private void chkOveFacExc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOveFacExc.Checked == true)
            {
                chkOveFacVG.Checked = false;
                chkOveFacPoor.Checked = false;
                chkOveFacGood.Checked = false;
                chkOveFacFair.Checked = false;

            }
        }

        private void chkOveFacVG_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOveFacVG.Checked == true)
            {
                chkOveFacExc.Checked = false;
                chkOveFacPoor.Checked = false;
                chkOveFacGood.Checked = false;
                chkOveFacFair.Checked = false;

            }
        }

        private void chkOveFacGood_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOveFacGood.Checked == true)
            {
                chkOveFacExc.Checked = false;
                chkOveFacPoor.Checked = false;
                chkOveFacVG.Checked = false;
                chkOveFacFair.Checked = false;

            }
        }

        private void chkOveFacFair_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOveFacFair.Checked == true)
            {
                chkOveFacExc.Checked = false;
                chkOveFacPoor.Checked = false;
                chkOveFacVG.Checked = false;
                chkOveFacGood.Checked = false;

            }
        }

        private void chkOveFacPoor_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOveFacPoor.Checked == true)
            {
                chkOveFacExc.Checked = false;
                chkOveFacFair.Checked = false;
                chkOveFacVG.Checked = false;
                chkOveFacGood.Checked = false;

            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text.Length > 0)
            {
                FillTrainedEmployees();
            }

        //    objSQLdb = new SQLDB();
        //    DataTable dt = new DataTable();
        //    if (txtEcodeSearch.Text != "")
        //    {
        //        try
        //        {
        //            string strCommand = "SELECT MEMBER_NAME,DESIG,HAMH_APPL_NUMBER "+
        //                                " FROM EORA_MASTER "+
        //                                " INNER JOIN HR_APPL_MASTER_HEAD ON HAMH_EORA_CODE=ECODE "+
        //                                " WHERE ECODE=" + Convert.ToInt32(txtEcodeSearch.Text) + "  ";

        //            dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
        //            if (dt.Rows.Count > 0)
        //            {
        //                txtEName.Text = dt.Rows[0]["MEMBER_NAME"].ToString();
        //                txtEmpDesg.Text = dt.Rows[0]["DESIG"].ToString();
        //                EmpApplNo = Convert.ToInt32(dt.Rows[0]["HAMH_APPL_NUMBER"]);
                       

        //            }
        //            else
        //            {
        //                txtEName.Text = "";
        //                txtEmpDesg.Text = "";
        //                EmpApplNo = 0;
        //                ////MessageBox.Show("Please Enter Valid Employee Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                //return;
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.ToString());
        //        }
        //        finally
        //        {
        //            objSQLdb = null;
        //            dt = null;
        //        }
            //}

           
        }

        private void chkSubExc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSubExc.Checked == true)
            {
                chkSubGood.Checked = false;
                chkSubVG.Checked = false;
                chkSubFair.Checked = false;
                chkSubPoor.Checked = false;
            }

        }

        private void chkSubVG_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSubVG.Checked == true)
            {
                chkSubGood.Checked = false;
                chkSubExc.Checked = false;
                chkSubFair.Checked = false;
                chkSubPoor.Checked = false;
            }

        }

        private void chkSubGood_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSubGood.Checked == true)
            {
                chkSubVG.Checked = false;
                chkSubExc.Checked = false;
                chkSubFair.Checked = false;
                chkSubPoor.Checked = false;
            }
        }

        private void chkSubFair_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSubFair.Checked == true)
            {
                chkSubVG.Checked = false;
                chkSubExc.Checked = false;
                chkSubGood.Checked = false;
                chkSubPoor.Checked = false;
            }
        }

        private void chkSubPoor_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSubPoor.Checked == true)
            {
                chkSubVG.Checked = false;
                chkSubExc.Checked = false;
                chkSubGood.Checked = false;
                chkSubFair.Checked = false;
            }

        }

        private void chkOvePrgExc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOvePrgExc.Checked == true)
            {
                chkOvePrgVG.Checked = false;
                chkOvePrgGood.Checked = false;
                chkOvePrgFair.Checked = false;
                chkOvePrgPoor.Checked = false;
            }
        }

        private void chkOvePrgVG_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOvePrgVG.Checked == true)
            {
                chkOvePrgExc.Checked = false;
                chkOvePrgGood.Checked = false;
                chkOvePrgFair.Checked = false;
                chkOvePrgPoor.Checked = false;
            }
        }

        private void chkOvePrgGood_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOvePrgGood.Checked == true)
            {
                chkOvePrgExc.Checked = false;
                chkOvePrgVG.Checked = false;
                chkOvePrgFair.Checked = false;
                chkOvePrgPoor.Checked = false;
            }
        }

        private void chkOvePrgFair_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOvePrgFair.Checked == true)
            {
                chkOvePrgExc.Checked = false;
                chkOvePrgVG.Checked = false;
                chkOvePrgGood.Checked = false;
                chkOvePrgPoor.Checked = false;
            }

        }

        private void chkOvePrgPoor_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOvePrgPoor.Checked == true)
            {
                chkOvePrgExc.Checked = false;
                chkOvePrgVG.Checked = false;
                chkOvePrgGood.Checked = false;
                chkOvePrgFair.Checked = false;
            }

        }
     
        private DataSet GetTrainingEmpFeedBackDetails(Int32 Ecode,string sPrgNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xEcode", DbType.Int32, Ecode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xPrgNumber", DbType.String, sPrgNo, ParameterDirection.Input);


                ds = objSQLdb.ExecuteDataSet("Get_TrainingEmpFeedBackDetails", CommandType.StoredProcedure, param);

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

        private void FillTrainingEmpFeedBackDetails()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            sCompCode = "";
            sBranchCode = "";
            

            if (cbEmployees.SelectedIndex > -1 )
            {
                try
                {
                    nEcode = Convert.ToInt32(((System.Data.DataRowView)(cbEmployees.SelectedItem)).Row.ItemArray[0].ToString().Split('@')[0]);
                    
                    dt = GetTrainingEmpFeedBackDetails(nEcode,txtPrgName.Tag.ToString()).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                       
                        EmpApplNo = Convert.ToInt32(dt.Rows[0]["EmpAppNo"]);
                        //txtEmpDesg.Text = dt.Rows[0]["EmpDesig"].ToString();
                        sCompCode = dt.Rows[0]["CompCode"].ToString();
                        sBranchCode = dt.Rows[0]["BranCode"].ToString();


                        if (dt.Rows[0]["DeliveryProcess"].ToString() != "")
                        {
                            flagUpdate = true;
                            btnDelete.Enabled = true;
                        }

                        if (dt.Rows[0]["DeliveryProcess"].ToString().Equals("PRESCRIPTIVE"))
                        {
                            rdbPrescriptive.Checked = true;
                        }
                        else if (dt.Rows[0]["DeliveryProcess"].ToString().Equals("EXPERIENTIAL"))
                        {
                            rdbExperiental.Checked = true;
                        }
                        else if (dt.Rows[0]["DeliveryProcess"].ToString().Equals("INTERACTIVE"))
                        {
                            rdbInteractive.Checked = true;
                        }

                        // Subject Is useful Or Not Rating
                        if (dt.Rows[0]["PrgSubRating"].ToString().Equals("0"))
                        {
                            cbFrmType.Text = "SR";
                            lblSubRel.Visible = false;
                            chkSubGood.Visible = false;
                            chkSubVG.Visible = false;
                            chkSubFair.Visible = false;
                            chkSubPoor.Visible = false;
                            chkSubExc.Visible = false;

                        }
                        else
                        {
                            cbFrmType.Text = "GL";
                            lblSubRel.Visible = true;
                            chkSubGood.Visible = true;
                            chkSubVG.Visible = true;
                            chkSubFair.Visible = true;
                            chkSubPoor.Visible = true;
                            chkSubExc.Visible = true;
                        }

                        if (dt.Rows[0]["PrgSubRating"].ToString().Equals("5"))
                        {
                            chkSubExc.Checked = true;
                        }
                        else if (dt.Rows[0]["PrgSubRating"].ToString().Equals("4"))
                        {
                            chkSubVG.Checked = true;
                        }
                        else if (dt.Rows[0]["PrgSubRating"].ToString().Equals("3"))
                        {
                            chkSubGood.Checked = true;
                        }
                        else if (dt.Rows[0]["PrgSubRating"].ToString().Equals("2"))
                        {
                            chkSubFair.Checked = true;
                        }
                        else if (dt.Rows[0]["PrgSubRating"].ToString().Equals("1"))
                        {
                            chkSubPoor.Checked = true;
                        }

                        //Program Coverage Rating
                        if (dt.Rows[0]["PrgCovgRating"].ToString().Equals("5"))
                        {
                            chkCoverageExc.Checked = true;
                        }
                        else if (dt.Rows[0]["PrgCovgRating"].ToString().Equals("4"))
                        {
                            chkCoverageVG.Checked = true;
                        }
                        else if (dt.Rows[0]["PrgCovgRating"].ToString().Equals("3"))
                        {
                            chkCoverageGood.Checked = true;
                        }
                        else if (dt.Rows[0]["PrgCovgRating"].ToString().Equals("2"))
                        {
                            chkCoverageFair.Checked = true;
                        }
                        else if (dt.Rows[0]["PrgCovgRating"].ToString().Equals("1"))
                        {
                            chkCoveragePoor.Checked = true;
                        }

                        //Program Clarity Rating

                        if (dt.Rows[0]["PrgClarityRating"].ToString().Equals("5"))
                        {
                            chkClarityExc.Checked = true;
                        }
                        else if (dt.Rows[0]["PrgClarityRating"].ToString().Equals("4"))
                        {
                            chkClarityVG.Checked = true;
                        }
                        else if (dt.Rows[0]["PrgClarityRating"].ToString().Equals("3"))
                        {
                            chkClarityGood.Checked = true;
                        }
                        else if (dt.Rows[0]["PrgClarityRating"].ToString().Equals("2"))
                        {
                            chkClarityFair.Checked = true;
                        }
                        else if (dt.Rows[0]["PrgClarityRating"].ToString().Equals("1"))
                        {
                            chkClarityPoor.Checked = true;
                        }
                        // Program Practical Relavance Rating
                        if (dt.Rows[0]["PrgPracRelrating"].ToString().Equals("5"))
                        {
                            chkPracRelExc.Checked = true;
                        }
                        else if (dt.Rows[0]["PrgPracRelrating"].ToString().Equals("4"))
                        {
                            chkPracRelVG.Checked = true;
                        }
                        else if (dt.Rows[0]["PrgPracRelrating"].ToString().Equals("3"))
                        {
                            chkPracRelGood.Checked = true;
                        }
                        else if (dt.Rows[0]["PrgPracRelrating"].ToString().Equals("2"))
                        {
                            chkPracRelFair.Checked = true;
                        }
                        else if (dt.Rows[0]["PrgPracRelrating"].ToString().Equals("1"))
                        {
                            chkPracRelPoor.Checked = true;
                        }

                        // Trainer Communication Rating

                        if (dt.Rows[0]["TrainerCommRating"].ToString().Equals("5"))
                        {
                            chkCommExc.Checked = true;
                        }
                        else if (dt.Rows[0]["TrainerCommRating"].ToString().Equals("4"))
                        {
                            chkCommVG.Checked = true;
                        }
                        else if (dt.Rows[0]["TrainerCommRating"].ToString().Equals("3"))
                        {
                            chkCommGood.Checked = true;
                        }
                        else if (dt.Rows[0]["TrainerCommRating"].ToString().Equals("2"))
                        {
                            chkCommFair.Checked = true;
                        }
                        else if (dt.Rows[0]["TrainerCommRating"].ToString().Equals("1"))
                        {
                            chkCommPoor.Checked = true;
                        }

                        //Trainer Enthusiasm Rating

                        if (dt.Rows[0]["TrainerEnthusRating"].ToString().Equals("5"))
                        {
                            chkEnthusiasmExc.Checked = true;
                        }
                        else if (dt.Rows[0]["TrainerEnthusRating"].ToString().Equals("4"))
                        {
                            chkEnthusiasmVG.Checked = true;
                        }
                        else if (dt.Rows[0]["TrainerEnthusRating"].ToString().Equals("3"))
                        {
                            chkEnthusiasmGood.Checked = true;
                        }
                        else if (dt.Rows[0]["TrainerEnthusRating"].ToString().Equals("2"))
                        {
                            chkEnthusiasmFair.Checked = true;
                        }
                        else if (dt.Rows[0]["TrainerEnthusRating"].ToString().Equals("1"))
                        {
                            chkEnthusiasmPoor.Checked = true;
                        }

                        // Trainer Friendliness Rating

                        if (dt.Rows[0]["TrainerFrndRating"].ToString().Equals("5"))
                        {
                            chkFrndlExc.Checked = true;
                        }
                        else if (dt.Rows[0]["TrainerFrndRating"].ToString().Equals("4"))
                        {
                            chkFrndlVG.Checked = true;
                        }
                        else if (dt.Rows[0]["TrainerFrndRating"].ToString().Equals("3"))
                        {
                            chkFrndlGood.Checked = true;
                        }
                        else if (dt.Rows[0]["TrainerFrndRating"].ToString().Equals("2"))
                        {
                            chkFrndlFair.Checked = true;
                        }
                        else if (dt.Rows[0]["TrainerFrndRating"].ToString().Equals("1"))
                        {
                            chkFrndlPoor.Checked = true;
                        }
                        // Trainer Subject Knowledge Rating

                        if (dt.Rows[0]["TrainerSubKonwRate"].ToString().Equals("5"))
                        {
                            chkSubKnowExc.Checked = true;
                        }
                        else if (dt.Rows[0]["TrainerSubKonwRate"].ToString().Equals("4"))
                        {
                            chkSubKnowVG.Checked = true;
                        }
                        else if (dt.Rows[0]["TrainerSubKonwRate"].ToString().Equals("3"))
                        {
                            chkSubKnowGood.Checked = true;
                        }
                        else if (dt.Rows[0]["TrainerSubKonwRate"].ToString().Equals("2"))
                        {
                            chkSubKnowFair.Checked = true;
                        }
                        else if (dt.Rows[0]["TrainerSubKonwRate"].ToString().Equals("1"))
                        {
                            chkSubKnowPoor.Checked = true;
                        }

                        // Trainer Time Management Rating 

                        if (dt.Rows[0]["TrainerTimeMgntRate"].ToString().Equals("5"))
                        {
                            chkTimeMgntExc.Checked = true;
                        }
                        else if (dt.Rows[0]["TrainerTimeMgntRate"].ToString().Equals("4"))
                        {
                            chkTimeMgntVG.Checked = true;
                        }
                        else if (dt.Rows[0]["TrainerTimeMgntRate"].ToString().Equals("3"))
                        {
                            chkTimeMgntGood.Checked = true;
                        }
                        else if (dt.Rows[0]["TrainerTimeMgntRate"].ToString().Equals("2"))
                        {
                            chkTimeMgntFair.Checked = true;
                        }
                        else if (dt.Rows[0]["TrainerTimeMgntRate"].ToString().Equals("1"))
                        {
                            chkTimeMgntPoor.Checked = true;
                        }
                        // Overall Program rating  

                        if (dt.Rows[0]["OvePrgRating"].ToString().Equals("5"))
                        {
                            chkOvePrgExc.Checked = true;
                        }
                        else if (dt.Rows[0]["OvePrgRating"].ToString().Equals("4"))
                        {
                            chkOvePrgVG.Checked = true;
                        }
                        else if (dt.Rows[0]["OvePrgRating"].ToString().Equals("3"))
                        {
                            chkOvePrgGood.Checked = true;
                        }
                        else if (dt.Rows[0]["OvePrgRating"].ToString().Equals("2"))
                        {
                            chkOvePrgFair.Checked = true;
                        }
                        else if (dt.Rows[0]["OvePrgRating"].ToString().Equals("1"))
                        {
                            chkOvePrgPoor.Checked = true;
                        }

                        //Hall Clean Ratings
                        if (dt.Rows[0]["HallCleanRate"].ToString().Equals("5"))
                        {
                            chkCleanExc.Checked = true;
                        }
                        else if (dt.Rows[0]["HallCleanRate"].ToString().Equals("4"))
                        {
                            chkCleanVG.Checked = true;
                        }
                        else if (dt.Rows[0]["HallCleanRate"].ToString().Equals("3"))
                        {
                            chkCleanGood.Checked = true;
                        }
                        else if (dt.Rows[0]["HallCleanRate"].ToString().Equals("2"))
                        {
                            chkCleanFair.Checked = true;
                        }
                        else if (dt.Rows[0]["HallCleanRate"].ToString().Equals("1"))
                        {
                            chkCleanPoor.Checked = true;
                        }

                        // Product Display Rating
                        if (dt.Rows[0]["ProdDisRating"].ToString().Equals("5"))
                        {
                            chkProdDisExc.Checked = true;
                        }
                        else if (dt.Rows[0]["ProdDisRating"].ToString().Equals("4"))
                        {
                            chkProdDisVG.Checked = true;
                        }
                        else if (dt.Rows[0]["ProdDisRating"].ToString().Equals("3"))
                        {
                            chkProdDisGood.Checked = true;
                        }
                        else if (dt.Rows[0]["ProdDisRating"].ToString().Equals("2"))
                        {
                            chkProdDisFair.Checked = true;
                        }
                        else if (dt.Rows[0]["ProdDisRating"].ToString().Equals("1"))
                        {
                            chkProdDisPoor.Checked = true;
                        }

                        //Audeo-video Rating
                        if (dt.Rows[0]["AudeoVideoRate"].ToString().Equals("5"))
                        {
                            chkAVQuaExc.Checked = true;
                        }
                        else if (dt.Rows[0]["AudeoVideoRate"].ToString().Equals("4"))
                        {
                            chkAVQuaVG.Checked = true;
                        }
                        else if (dt.Rows[0]["AudeoVideoRate"].ToString().Equals("3"))
                        {
                            chkAVQuaGood.Checked = true;
                        }
                        else if (dt.Rows[0]["AudeoVideoRate"].ToString().Equals("2"))
                        {
                            chkAVQuaFair.Checked = true;
                        }
                        else if (dt.Rows[0]["AudeoVideoRate"].ToString().Equals("1"))
                        {
                            chkAVQuaPoor.Checked = true;
                        }

                        // Hall Facility Rating
                        if (dt.Rows[0]["HallFacRating"].ToString().Equals("5"))
                        {
                            chkOveFacExc.Checked = true;
                        }
                        else if (dt.Rows[0]["HallFacRating"].ToString().Equals("4"))
                        {
                            chkOveFacVG.Checked = true;
                        }
                        else if (dt.Rows[0]["HallFacRating"].ToString().Equals("3"))
                        {
                            chkOveFacGood.Checked = true;
                        }
                        else if (dt.Rows[0]["HallFacRating"].ToString().Equals("2"))
                        {
                            chkOveFacFair.Checked = true;
                        }
                        else if (dt.Rows[0]["HallFacRating"].ToString().Equals("1"))
                        {
                            chkOveFacPoor.Checked = true;
                        }


                        txtImplement1.Text = dt.Rows[0]["ImplThing1"].ToString();
                        txtImplement2.Text = dt.Rows[0]["ImplThing2"].ToString();
                        txtImplement3.Text = dt.Rows[0]["ImplThing3"].ToString();

                        txtSuggestion1.Text = dt.Rows[0]["Suggestion1"].ToString();
                        txtSuggestion2.Text = dt.Rows[0]["Suggestion2"].ToString();
                        txtSuggestion3.Text = dt.Rows[0]["Suggestion3"].ToString();

                        txtPrgContent.Text = dt.Rows[0]["PrgContent"].ToString();
                        txtInfrastrFeedBack.Text = dt.Rows[0]["OvePrgInfr"].ToString();
                        txtFacultyFeedBack.Text = dt.Rows[0]["OvePrgFacultyFeedBack"].ToString();

                        txtOtherSuggestions.Text = dt.Rows[0]["OtherTopics"].ToString();


                    }
                    else
                    {
                        sCompCode = "";
                        sBranchCode = "";
                        rdbExperiental.Checked = false;
                        rdbInteractive.Checked = false;
                        rdbPrescriptive.Checked = false;
                        flagUpdate = false;
                        chkSubExc.Checked = false;
                        chkSubGood.Checked = false;
                        chkSubVG.Checked = false;
                        chkSubFair.Checked = false;
                        chkSubPoor.Checked = false;

                        chkOvePrgExc.Checked = false;
                        chkOvePrgVG.Checked = false;
                        chkOvePrgGood.Checked = false;
                        chkOvePrgFair.Checked = false;
                        chkOvePrgPoor.Checked = false;

                        txtImplement1.Text = "";
                        txtImplement2.Text = "";
                        txtImplement3.Text = "";

                        txtSuggestion1.Text = "";
                        txtSuggestion2.Text = "";
                        txtSuggestion3.Text = "";
                        txtOtherSuggestions.Text = "";

                        txtPrgContent.Text = "";

                        txtInfrastrFeedBack.Text = "";
                                           
                        txtFacultyFeedBack.Text = "";
                        
                        chkCoverageVG.Checked = false;
                        chkCoverageGood.Checked = false;
                        chkCoverageFair.Checked = false;
                        chkCoveragePoor.Checked = false;
                        chkCoverageExc.Checked = false;

                        chkClarityVG.Checked = false;
                        chkClarityGood.Checked = false;
                        chkClarityFair.Checked = false;
                        chkClarityPoor.Checked = false;
                        chkClarityExc.Checked = false;

                        chkPracRelExc.Checked = false;
                        chkPracRelVG.Checked = false;
                        chkPracRelGood.Checked = false;
                        chkPracRelFair.Checked = false;
                        chkPracRelPoor.Checked = false;

                        chkCommVG.Checked = false;
                        chkCommPoor.Checked = false;
                        chkCommGood.Checked = false;
                        chkCommFair.Checked = false;
                        chkCommExc.Checked = false;

                        chkEnthusiasmExc.Checked = false;
                        chkEnthusiasmVG.Checked = false;
                        chkEnthusiasmGood.Checked = false;
                        chkEnthusiasmFair.Checked = false;
                        chkEnthusiasmPoor.Checked = false;

                        chkSubKnowExc.Checked = false;
                        chkSubKnowVG.Checked = false;
                        chkSubKnowGood.Checked = false;
                        chkSubKnowFair.Checked = false;
                        chkSubKnowPoor.Checked = false;

                        chkFrndlExc.Checked = false;
                        chkFrndlVG.Checked = false;
                        chkFrndlGood.Checked = false;
                        chkFrndlFair.Checked = false;
                        chkFrndlPoor.Checked = false;

                        chkTimeMgntExc.Checked = false;
                        chkTimeMgntVG.Checked = false;
                        chkTimeMgntPoor.Checked = false;
                        chkTimeMgntGood.Checked = false;
                        chkTimeMgntFair.Checked = false;

                        chkCleanExc.Checked = false;
                        chkCleanVG.Checked = false;
                        chkCleanGood.Checked = false;
                        chkCleanFair.Checked = false;
                        chkCleanPoor.Checked = false;

                        chkProdDisExc.Checked = false;
                        chkProdDisVG.Checked = false;
                        chkProdDisPoor.Checked = false;
                        chkProdDisGood.Checked = false;
                        chkProdDisFair.Checked = false;

                        chkAVQuaExc.Checked = false;
                        chkAVQuaVG.Checked = false;
                        chkAVQuaPoor.Checked = false;
                        chkAVQuaGood.Checked = false;
                        chkAVQuaFair.Checked = false;

                        chkOveFacExc.Checked = false;
                        chkOveFacVG.Checked = false;
                        chkOveFacPoor.Checked = false;
                        chkOveFacGood.Checked = false;
                        chkOveFacFair.Checked = false;


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

        private void cbFrmType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFrmType.SelectedIndex == 0)
            {
                lblSubRel.Visible = true;
                chkSubGood.Visible = true;
                chkSubVG.Visible = true;
                chkSubFair.Visible = true;
                chkSubPoor.Visible = true;
                chkSubExc.Visible = true;
            }
            else if (cbFrmType.SelectedIndex == 1)
            {
                lblSubRel.Visible = false;
                chkSubGood.Visible = false;
                chkSubVG.Visible = false;
                chkSubFair.Visible = false;
                chkSubPoor.Visible = false;
                chkSubExc.Visible = false;
            }
        }

      
        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "";

            if (cbEmployees.SelectedIndex>-1)
            {

                DialogResult result = MessageBox.Show("Do you want to delete This Record ?",
                                       "Training FeedBack Form", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (flagUpdate == true)
                    {

                        try
                        {
                            strCommand = "DELETE FROM HR_TRAINING_FEEDBACK " +
                                        " WHERE HTF_APPL_NUMBER=" + EmpApplNo +
                                        " AND HTF_PROGRAM_NUMBER='" + txtPrgName.Tag.ToString() + "'";

                            if (strCommand.Length > 5)
                            {
                                iRes = objSQLdb.ExecuteSaveData(strCommand);
                            }

                            if (iRes > 0)
                            {
                                MessageBox.Show("Data Deleted Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                flagUpdate = false;
                                btnCancel_Click(null, null);
                                grouper1.Visible = true;
                                grouper2.Visible = false;
                            }


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }

                    }
                    else
                    {
                        MessageBox.Show("Data Not Deleted", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void txtEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
          
        }

        private void cbEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEmployees.SelectedIndex>-1)
            {
                txtEmpDesg.Text = ((System.Data.DataRowView)(cbEmployees.SelectedItem)).Row.ItemArray[0].ToString().Split('@')[1];
                EmpApplNo = Convert.ToInt32(((System.Data.DataRowView)(cbEmployees.SelectedItem)).Row.ItemArray[0].ToString().Split('@')[2]);

                FillTrainingEmpFeedBackDetails();
            }
            else
            {
               
                txtEmpDesg.Text = "";
            }
        }

     
      
    }
}
