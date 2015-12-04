using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSAdmin;
using SSCRM.App_Code;
using System.IO;
using SSTrans;
using System.Data.SqlClient;
using System.Configuration;
namespace SSCRM
{
    public partial class frmApplication : Form
    {
        public frmFamily objFamily;
        public Security objSecurity;
        public Master objMaster;
        public SQLDB objSQLDB;
        public HRInfo objHrInfo;
        public DataTable dtFamily = new DataTable();
        public DataTable dtEducation = new DataTable();
        public DataTable dtShortCourse = new DataTable();
        public DataTable dtLanguages = new DataTable();
        public DataTable dtECA = new DataTable();
        public DataTable dtExperience = new DataTable();
        public DataTable dtReference = new DataTable();
        public DataTable dtDocuments = new DataTable();
        public int AppliNo = 0, iMaxArE = 0;
        public string CompNo = "", Breanch = "", RecuName = "", TrainedName = "", frmDate = "", toDate = "";

        public frmApplication()
        {
            InitializeComponent();
        }

        string sType = "";

        public frmApplication(string sTypes)
        {
            InitializeComponent();
            sType = sTypes;
        }

        public frmApplication(string Name, string FName, DateTime DOB, string SSCNo, string sType)
        {
            InitializeComponent();
            txtFullName.Text = Name;
            txtfName.Text = FName;
            dtpDOB.Value = DOB;
            txtSSCNumber.Text = SSCNo;
            cmbType_optional.Text = sType;
        }

        public frmApplication(string CompCode, string BranchCode, string ApplNo, string sTypes)
        {
            InitializeComponent();
            CompNo = CompCode;
            Breanch = BranchCode;
            AppliNo = Convert.ToInt32(ApplNo);
            sType = sTypes;
        }

        public frmApplication(string CompCode, string BranchCode, string ApplNo)
        {
            InitializeComponent();
            CompNo = CompCode;
            Breanch = BranchCode;
            AppliNo = Convert.ToInt32(ApplNo);
        }

        private void frmApplication_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            GetPopupdropDown();
            if (cmbType_optional.Text == "A")
            {
                cmbDepartment.SelectedValue = "1200000";
                cmbDepartment_SelectedIndexChanged(null, null);
                cmbDesig.SelectedValue = "987";
                cmbDesig.Enabled = false;
                cmbDepartment.Enabled = false;
                lblHeading.Text = "STANDARD APPLICATION FORM FOR SALES STAFF";
                cbInductionTraining.Enabled = true;
                cbInductionTraining.SelectedIndex = -1;
            }
            else
            {
                cbInductionTraining.SelectedIndex = 0;
                cbInductionTraining.Enabled = false;
                lblHeading.Text = "EMPLOYEE APPLICATION FORM";
                lblHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            }
            grpPage1.Visible = true;
            cmbNationality.SelectedIndex = 1;
            cmbCompany.SelectedIndex = 0;
            cmbF_Flg_optional.SelectedIndex = 0;
            cmbMatrital_optional.SelectedIndex = 0;
            cmbReligion.SelectedIndex = 1;
            cmbSex_optional.SelectedIndex = 0;
            cmbVehicleFlg_optional.SelectedIndex = 0;
            cmbBloodGrp_optional.SelectedIndex = 1;

            cmbCompany.SelectedValue = CommonData.CompanyCode;
            cmbCompany_SelectedIndexChanged(null, null);
            cmbBranch.SelectedValue = CommonData.BranchCode;
            cmbCompany.Enabled = false;
            cmbBranch.Enabled = false;
            dtpDOJ.Value = System.DateTime.Now;
            dtpMarriedDt.Value = System.DateTime.Now;

            #region "FAMILY CREATE TABLE"
            dtFamily.Columns.Add("SLNO");
            dtFamily.Columns.Add("RelationShip");
            dtFamily.Columns.Add("sName");
            dtFamily.Columns.Add("DateofBirth");
            dtFamily.Columns.Add("Residing");
            dtFamily.Columns.Add("Depending");
            dtFamily.Columns.Add("Occupation");
            #endregion

            #region "EDUCATION CREATE TABLE"
            dtEducation.Columns.Add("SLNO_edu");
            dtEducation.Columns.Add("ExamPass");
            dtEducation.Columns.Add("ExamType");
            dtEducation.Columns.Add("YearPass");
            dtEducation.Columns.Add("InstName");
            dtEducation.Columns.Add("InstLocation");
            dtEducation.Columns.Add("Subject");
            dtEducation.Columns.Add("University");
            dtEducation.Columns.Add("PerofPass");
            #endregion

            #region "SHORT COURSE"
            dtShortCourse.Columns.Add("SLNO_sc");
            dtShortCourse.Columns.Add("CourseName");
            dtShortCourse.Columns.Add("YearofPass");
            dtShortCourse.Columns.Add("Insti_name");
            dtShortCourse.Columns.Add("Insti_Location");
            dtShortCourse.Columns.Add("Subject");
            dtShortCourse.Columns.Add("Duration");
            dtShortCourse.Columns.Add("PerofMarks");
            #endregion

            #region "KNOWN LANGUAGES DETAILS"
            dtLanguages.Columns.Add("SLNO_lg");
            dtLanguages.Columns.Add("Language");
            dtLanguages.Columns.Add("ReadFlag");
            dtLanguages.Columns.Add("WriteFlag");
            dtLanguages.Columns.Add("SpeakFlag");            
            #endregion

            #region "ECA COURSE"
            dtECA.Columns.Add("SLNO_eca");
            dtECA.Columns.Add("TypeofECA");
            dtECA.Columns.Add("Remarks");
            #endregion

            #region "EXPERIENCE COURSE"
            dtExperience.Columns.Add("SlNo_ex");
            dtExperience.Columns.Add("Organisation");
            dtExperience.Columns.Add("FromDate");
            dtExperience.Columns.Add("ToDate");
            dtExperience.Columns.Add("HouseNo");
            dtExperience.Columns.Add("LandMark");
            dtExperience.Columns.Add("Village");
            dtExperience.Columns.Add("Mondal");
            dtExperience.Columns.Add("District");
            dtExperience.Columns.Add("State");
            dtExperience.Columns.Add("Pin");
            dtExperience.Columns.Add("JoiningDesg");
            dtExperience.Columns.Add("LeavingDesg");
            dtExperience.Columns.Add("Salary");
            dtExperience.Columns.Add("Remarks_ex");
            dtExperience.Columns.Add("Reasons");
            #endregion

            #region "REFERENCE"
            dtReference.Columns.Add("SlNo_ref");
            dtReference.Columns.Add("refName");
            dtReference.Columns.Add("refOccup");
            dtReference.Columns.Add("refPhoneNo");
            dtReference.Columns.Add("HouseNo");
            dtReference.Columns.Add("LandMark");
            dtReference.Columns.Add("Village");
            dtReference.Columns.Add("Mondal");
            dtReference.Columns.Add("District");
            dtReference.Columns.Add("State");
            dtReference.Columns.Add("Pin");
            dtReference.Columns.Add("PhoneNoref");
            #endregion

            #region "DOCUMENTS"

            dtDocuments.Columns.Add("Checked", typeof(bool));
            dtDocuments.Columns.Add("Head");
            dtDocuments.Columns.Add("Date(dd/mm/yyyy)");
            dtDocuments.Columns.Add("Remarks");

            dtDocuments.Rows.Add(new Object[] { false, "Passport Size PhotoGraphs-6 Nos(2 For Branch & 4 For Head Office)", "", "" });
            dtDocuments.Rows.Add(new Object[] { false, "Blood Group Certificate", "", "" });
            //dtDocuments.Rows.Add(new Object[] { false, "Stamp Size Photographs - 4 Nos.", "", "" });            
            dtDocuments.Rows.Add(new Object[] { false, "Education Certificates", "", "" });
            dtDocuments.Rows.Add(new Object[] { false, "Reference Letters - 2 Nos.", "", "" });
            dtDocuments.Rows.Add(new Object[] { false, "Address Proof(RationCard/VoterID/DrivingLicence/ResidenceCertificate)", "", "" });
            dtDocuments.Rows.Add(new Object[] { false, "Experience Certificate(s), If worked previously", "", "" });
            dtDocuments.Rows.Add(new Object[] { false, "Pan Card copy/Pan Application(Branch Use Only)", "", "" });
            dtDocuments.Rows.Add(new Object[] { false, "ID proof(Driving Licence/Ration Card/Voter ID/Passport etc.)", "", "" });
            this.gvDocuments.DataSource = dtDocuments;
            gvDocuments.Columns["Head"].Width = 450;

            #endregion

            if (AppliNo != 0)
            {
                #region "EDITING DATA"
                cmbCompany.SelectedValue = CompNo;
                cmbCompany_SelectedIndexChanged(null, null);
                cmbBranch.SelectedValue = Breanch;
                objHrInfo = new HRInfo();
                DataTable dtMain = objHrInfo.GetHRInformation(101, cmbCompany.SelectedValue.ToString(), cmbBranch.SelectedText.ToString(), AppliNo).Tables[0];
                if (dtMain.Rows.Count > 0)
                {
                    cmbCompany.SelectedValue = dtMain.Rows[0]["company_code"];
                    cmbBranch.SelectedValue = dtMain.Rows[0]["BRANCH_CODE"];
                    cmbDepartment.SelectedValue = Convert.ToInt32(dtMain.Rows[0]["DEPT_ID"]);
                    cmbDesig.SelectedValue = Convert.ToInt32(dtMain.Rows[0]["DESG_ID"]);
                    dtpDOB.Value = Convert.ToDateTime(dtMain.Rows[0]["HAMH_DOB"]);
                    cmbSourceCode.SelectedValue = dtMain.Rows[0]["HAMH_RECRUITMENT_SOURCE_CODE"].ToString();
                    cmbRequtSourDetails.SelectedValue = dtMain.Rows[0]["HAMH_RECRUITMENT_SOURCE_DELT_CODE"].ToString();
                    eCodeCtrlRec.ECode = dtMain.Rows[0]["HAMH_RECRUITMENT_SOURCE_ECODE"].ToString();
                    string sFullName = dtMain.Rows[0]["HAMH_NAME"].ToString();
                    txtFullName.Text = sFullName;
                    cmbF_Flg_optional.Text = dtMain.Rows[0]["HAMH_FORH"].ToString();
                    txtfName.Text = dtMain.Rows[0]["HAMH_FORH_NAME"].ToString();
                    dtpDOJ.Value = Convert.ToDateTime(dtMain.Rows[0]["HAMH_DOJ"]);
                    dtpDOB.Value = Convert.ToDateTime(dtMain.Rows[0]["HAMH_DOB"]);
                    txtNativePlace_optional.Text = dtMain.Rows[0]["HAMH_NATIVE_PLACE"].ToString();
                    cmbSex_optional.Text = dtMain.Rows[0]["HAMH_SEX"].ToString();
                    cmbNationality.Text = dtMain.Rows[0]["HAMH_NAIONALITY"].ToString();
                    if (cmbNationality.SelectedIndex == -1 || dtMain.Rows[0]["HAMH_NAIONALITY"].ToString()=="")
                    {
                        if (cmbCompany.SelectedValue.ToString() == "SBTLNPL")
                            cmbNationality.SelectedIndex = 2;
                        else
                            cmbNationality.SelectedIndex = 1;
                    }
                    if (dtMain.Rows[0]["HAMH_RELIGION"].ToString() == "")                    
                        cmbReligion.SelectedIndex = 1;                    
                    else
                        cmbReligion.Text = dtMain.Rows[0]["HAMH_RELIGION"].ToString();
                    cmbMatrital_optional.Text = dtMain.Rows[0]["HAMH_MATRITAL_STATUS"].ToString();
                    dtpMarriedDt.Value = dtMain.Rows[0]["HAMH_MARRIAGE_DATE"].ToString() == "" ? Convert.ToDateTime(CommonData.CurrentDate) : Convert.ToDateTime(dtMain.Rows[0]["HAMH_MARRIAGE_DATE"]);
                    txtNominee_optional.Text = dtMain.Rows[0]["HAMH_NOMINIEE_NAME"].ToString();
                    cmbNomRelation_optional.Text = dtMain.Rows[0]["HAMH_NOMINIEE_RELATION"].ToString();
                    txtHeight_optional.Text = dtMain.Rows[0]["HAMH_PD_HEIGHT"].ToString();
                    txtWeight_optional.Text = dtMain.Rows[0]["HAMH_PD_WEIGHT"].ToString();
                    cmbBloodGrp_optional.Text = dtMain.Rows[0]["HAMH_PD_BLOOD_GROUP_CODE"].ToString();
                    txtDisabilty_optional.Text = dtMain.Rows[0]["HAMH_PD_PHYSICAL_DISABILITY"].ToString();
                    txtillness_optional.Text = dtMain.Rows[0]["HAMH_PD_PROLONGED_ILLNESS"].ToString();
                    txtProlonged_optional.Text = dtMain.Rows[0]["HAMH_PD_PROLONGED_ILLNESS_PERIOD"].ToString();
                    PresentAddCtr.HouseNo = dtMain.Rows[0]["HAMH_ADD_PRES_ADDR_HNO"].ToString();
                    PresentAddCtr.LandMark = dtMain.Rows[0]["HAMH_ADD_PRES_ADDR_LANDMARK"].ToString();
                    PresentAddCtr.Village = dtMain.Rows[0]["HAMH_ADD_PRES_ADDR_VILL_OR_TOWN"].ToString();
                    PresentAddCtr.Mondal = dtMain.Rows[0]["HAMH_ADD_PRES_ADDR_MANDAL"].ToString();
                    PresentAddCtr.District = dtMain.Rows[0]["HAMH_ADD_PRES_ADDR_DISTRICT"].ToString();
                    PresentAddCtr.State = dtMain.Rows[0]["HAMH_ADD_PRES_ADDR_STATE"].ToString();
                    PresentAddCtr.Pin = dtMain.Rows[0]["HAMH_ADD_PRES_ADDR_PIN"].ToString();
                    PresentPhone_num.Text = dtMain.Rows[0]["HAMH_ADD_PRES_ADDR_PHONE"].ToString();
                    PermentAddCtr.HouseNo = dtMain.Rows[0]["HAMH_ADD_PERM_ADDR_HNO"].ToString();
                    PermentAddCtr.LandMark = dtMain.Rows[0]["HAMH_ADD_PERM_ADDR_LANDMARK"].ToString();
                    PermentAddCtr.Village = dtMain.Rows[0]["HAMH_ADD_PERM_ADDR_VILL_OR_TOWN"].ToString();
                    PermentAddCtr.Mondal = dtMain.Rows[0]["HAMH_ADD_PERM_ADDR_MANDAL"].ToString();
                    PermentAddCtr.District = dtMain.Rows[0]["HAMH_ADD_PERM_ADDR_DISTRICT"].ToString();
                    PermentAddCtr.State = dtMain.Rows[0]["HAMH_ADD_PERM_ADDR_STATE"].ToString();
                    PermentAddCtr.Pin = dtMain.Rows[0]["HAMH_ADD_PERM_ADDR_PIN"].ToString();
                    txtPermentPhNo_num.Text = dtMain.Rows[0]["HAMH_ADD_PERM_ADDR_PHONE"].ToString();
                    txtContactName.Text = dtMain.Rows[0]["HAMH_ADD_CONTPERS_NAME"].ToString();
                    ContactAddCtr.HouseNo = dtMain.Rows[0]["HAMH_ADD_CONTPERS_ADDR_HNO"].ToString();
                    ContactAddCtr.LandMark = dtMain.Rows[0]["HAMH_ADD_CONTPERS_ADDR_LANDMARK"].ToString();
                    ContactAddCtr.Village = dtMain.Rows[0]["HAMH_ADD_CONTPERS_ADDR_VILL_OR_TOWN"].ToString();
                    ContactAddCtr.Mondal = dtMain.Rows[0]["HAMH_ADD_CONTPERS_ADDR_MANDAL"].ToString();
                    ContactAddCtr.District = dtMain.Rows[0]["HAMH_ADD_CONTPERS_ADDR_DISTRICT"].ToString();
                    ContactAddCtr.State = dtMain.Rows[0]["HAMH_ADD_CONTPERS_ADDR_STATE"].ToString();
                    ContactAddCtr.Pin = dtMain.Rows[0]["HAMH_ADD_CONTPERS_ADDR_PIN"].ToString();
                    txtContPhNo_num.Text = dtMain.Rows[0]["HAMH_ADD_CONTPERS_ADDR_PHONE_RES"].ToString();
                    txtContPhNo1_optional.Text = dtMain.Rows[0]["HAMH_ADD_CONTPERS_ADDR_PHONE_OFF"].ToString();
                    if (dtMain.Rows[0]["HAMH_VD_OWN_VEHICLE_FLAG"].ToString() == "")
                        cmbVehicleFlg_optional.Text = "NO";
                    else
                        cmbVehicleFlg_optional.Text = dtMain.Rows[0]["HAMH_VD_OWN_VEHICLE_FLAG"].ToString();
                    txtVehicleNo_optional.Text = dtMain.Rows[0]["HAMH_VD_VEHICLE_REG_NUMBER"].ToString();
                    txtVehicleMake_optional.Text = dtMain.Rows[0]["HAMH_VD_VEHICLE_MAKE"].ToString();
                    txtDlNumber_optional.Text = dtMain.Rows[0]["HAMH_VD_DL_NUMBER"].ToString();
                    txtPassportNumber_optional.Text = dtMain.Rows[0]["HAMH_VD_PASSPORT_NUMBER"].ToString();
                    txtPanCardNumber_optional.Text = dtMain.Rows[0]["HAMH_VD_PAN_CARD_NUMBER"].ToString();

                    eCodeCtrlInterivewed.ECode = dtMain.Rows[0]["HAMH_INTERVIEW_DONE_BY_ECODE"].ToString();
                    if (eCodeCtrlInterivewed.ECode.Length < 5)
                        eCodeCtrlInterivewed.ECode = dtMain.Rows[0]["HAMH_RECRUITMENT_SOURCE_ECODE"].ToString();
                    dtpInterview.Value = dtMain.Rows[0]["HAMH_INTERVIEW_DATE"].ToString() == "" ? Convert.ToDateTime(dtMain.Rows[0]["HAMH_DOJ"]) : Convert.ToDateTime(dtMain.Rows[0]["HAMH_INTERVIEW_DATE"]);
                    txtIVRemarks_optional.Text = dtMain.Rows[0]["HAMH_INTERVIEW_REMARKS"].ToString();

                    if (dtMain.Rows[0]["HAMH_MY_PHOTO"].ToString() != "")
                        GetImage((byte[])dtMain.Rows[0]["HAMH_MY_PHOTO"]);
                    txtSSCNumber.Text = dtMain.Rows[0]["HAED_SSC_NUMBER"].ToString();
                    cmbType_optional.Text = dtMain.Rows[0]["HAMH_EORA_TYPE"].ToString();
                    iMaxArE = Convert.ToInt32(dtMain.Rows[0]["HAMH_EORA_CODE"]);
                    txteoracode_optional.Text = dtMain.Rows[0]["HAMH_EORA_CODE"].ToString();
                    txteoracode_optional.Visible = true;
                    lbleoracode.Visible = true;
                }
                objHrInfo = new HRInfo();
                try
                {
                    dtFamily = objHrInfo.GetHRInformation(102, cmbCompany.SelectedValue.ToString(), cmbBranch.SelectedText.ToString(), AppliNo).Tables[0];
                }
                catch
                {

                }
                try
                {
                    dtEducation = objHrInfo.GetHRInformation(103, cmbCompany.SelectedValue.ToString(), cmbBranch.SelectedText.ToString(), AppliNo).Tables[0];
                }
                catch
                {

                }
                try
                {
                    dtECA = objHrInfo.GetHRInformation(104, cmbCompany.SelectedValue.ToString(), cmbBranch.SelectedText.ToString(), AppliNo).Tables[0];
                }
                catch
                {
                }
                try
                {
                    dtExperience = objHrInfo.GetHRInformation(105, cmbCompany.SelectedValue.ToString(), cmbBranch.SelectedText.ToString(), AppliNo).Tables[0];
                }
                catch
                {
                }
                try
                {
                    dtReference = objHrInfo.GetHRInformation(106, cmbCompany.SelectedValue.ToString(), cmbBranch.SelectedText.ToString(), AppliNo).Tables[0];
                }
                catch
                {

                }
                try
                {
                    dtShortCourse = objHrInfo.GetHRInformation(107, cmbCompany.SelectedValue.ToString(), cmbBranch.SelectedText.ToString(), AppliNo).Tables[0];
                }
                catch
                {
                    
                }
                try
                {
                    dtLanguages = objHrInfo.GetHRInformation(115, cmbCompany.SelectedValue.ToString(), cmbBranch.SelectedText.ToString(), AppliNo).Tables[0];
                }
                catch { }
                //dtDocuments.Rows.Clear();
                DataTable dtDoc = new DataTable();
                try
                {
                    
                    dtDoc = objHrInfo.GetHRInformation(108, cmbCompany.SelectedValue.ToString(), cmbBranch.SelectedText.ToString(), AppliNo).Tables[0];
                }
                catch
                {

                }
                for (int i = 0; i < dtDocuments.Rows.Count; i++)
                {
                    DataRow[] dr = dtDoc.Select("Head='" + dtDocuments.Rows[i][1].ToString() + "'");
                    if (dr.Length > 0)
                    {
                        dtDocuments.Rows[i][0] = dr[0][2].ToString();
                        dtDocuments.Rows[i][2] = dr[0][3].ToString();
                    }
                }
                try
                {
                    DataTable dtIndection = objHrInfo.GetHRInformation(114, cmbCompany.SelectedValue.ToString(), cmbBranch.SelectedText.ToString(), AppliNo).Tables[0];
                
                if (dtIndection.Rows.Count > 0)
                {
                    cbInductionTraining.SelectedIndex = cbInductionTraining.Items.IndexOf(dtIndection.Rows[0][1].ToString());
                    eCodeCtrlTrainer.ECode = dtIndection.Rows[0][2].ToString();
                    dtpTrainingFrom_optional.Value = Convert.ToDateTime(dtIndection.Rows[0][3]);
                    dtpTrainingTo_optional.Value = Convert.ToDateTime(dtIndection.Rows[0][4]);
                }
                else
                {
                    cbInductionTraining.SelectedIndex = 2;
                    cbInductionTraining_SelectedIndexChanged(null, null);
                }
                }
                catch
                {

                }
                objHrInfo = null;
                GetDGVFamily();
                GetDGVEducation();
                GetDGVShortCourse();
                GetDGVECA();
                GetDGVLanguageDetails();
                GetExperience();
                GetReference();
                objSQLDB = null;
                #endregion
            }
            else
            {
                txtFullName.ReadOnly = true;
                txtfName.ReadOnly = true;
                txtSSCNumber.ReadOnly = true;
                //dtpDOB.Enabled = true;
                dtpMarriedDt.Enabled = true;
            }
            if (CommonData.LogUserRole.ToUpper() == "MANAGEMENT" || CommonData.LogUserId.ToUpper() == "ADMIN")
            {
                cmbCompany.Enabled = true;
                cmbBranch.Enabled = true;
                cmbDepartment.Enabled = true;
                cmbDesig.Enabled = true;
                txtSSCNumber.ReadOnly = false;
                dtpDOB.Enabled = true;
                txtFullName.ReadOnly = false;
                cbInductionTraining.Enabled = true;
            }
        }

        public void GetPopupdropDown()
        {
            objSecurity = new Security();
            DataTable dtCpy = objSecurity.GetCompanyDataSet().Tables[0];
            UtilityLibrary.PopulateControl(cmbCompany, dtCpy.DefaultView, 1, 0, "--PLEASE SELECT--", 0);

            objSecurity = null;

            objSQLDB = new SQLDB();
            string strSQL1 = "SELECT HRSM_RECRUITMENT_SOURCE_CODE,HRSM_RECRUITMENT_SOURCE_NAME from HR_RECRUITMENT_SOURCE_MASTER";
            DataTable dt1 = objSQLDB.ExecuteDataSet(strSQL1, CommandType.Text).Tables[0];
            UtilityLibrary.PopulateControl(cmbSourceCode, dt1.DefaultView, 1, 0, "--PLEASE SELECT--", 0);

            DataView dvDept = objSQLDB.ExecuteDataSet("SELECT DEPT_CODE,DEPT_DESC FROM DEPT_MAS", CommandType.Text).Tables[0].DefaultView;
            UtilityLibrary.PopulateControl(cmbDepartment, dvDept, 1, 0, "--PLEASE SELECT--", 0);
            objSQLDB = null;          
        }

        private void pg1Next_Click(object sender, EventArgs e)
        {
            UtilityLibrary oUtility = new UtilityLibrary();
            if (!SSCRM.App_Code.UtilityLibrary.CustomValidate(grpPage1, toolTip1))
            {
                //tabMain.SelectedIndex = 0;
                return;
            }
            if (!SSCRM.App_Code.UtilityLibrary.CustomValidate(grouper2, toolTip1))
            {
                //tabMain.SelectedIndex = 0;
                return;
            }
            if (!SSCRM.App_Code.UtilityLibrary.CustomValidate(grouper3, toolTip1))
            {
                TabAddress.SelectedIndex = 0;
                return;
            }
            if (!SSCRM.App_Code.UtilityLibrary.CustomValidate(grouper4, toolTip1))
            {
                TabAddress.SelectedIndex = 1;
                return;
            }
            if (!SSCRM.App_Code.UtilityLibrary.CustomValidate(grouper7, toolTip1))
            {
                TabAddress.SelectedIndex = 2;
                return;
            }
            if (GetAddressCheck() == false)
            {
                MessageBox.Show("Please enter address.", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (eCodeCtrlRec.EName == "")
            {
                MessageBox.Show("Invalid Recruiter E-Code", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //tabMain.SelectedIndex = 1;
            grpPage1.Visible = false;
            grpPage2.Visible = true;
            grpPage3.Visible = false;
            grpPage4.Visible = false;
        }

        private void pg2Next_Click(object sender, EventArgs e)
        {
            if (dtEducation.Rows.Count == 0)
            {
                MessageBox.Show("Please enter education details.", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            grpPage1.Visible = false;
            grpPage2.Visible = false;
            grpPage3.Visible = true;
            grpPage4.Visible = false;
        }

        private void pg3Back_Click(object sender, EventArgs e)
        {
            //tabMain.SelectedIndex = 1;
            grpPage1.Visible = false;
            grpPage2.Visible = true;
            grpPage3.Visible = false;
            grpPage4.Visible = false;
        }

        private void pg2Back_Click(object sender, EventArgs e)
        {
            //tabMain.SelectedIndex = 0;
            grpPage1.Visible = true;
            grpPage2.Visible = false;
            grpPage3.Visible = false;
            grpPage4.Visible = false;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //tabMain.SelectedIndex = 3;
            grpPage1.Visible = false;
            grpPage2.Visible = false;
            grpPage3.Visible = false;
            grpPage4.Visible = true;
            if (cmbType_optional.Text == "A")
            {
                cbInductionTraining.Visible = true;
                eCodeCtrlTrainer.Visible = true;
                dtpTrainingFrom_optional.Visible = true;
                dtpTrainingTo_optional.Visible = true;
                lblIndTraining.Visible = true;
                lblTrainerECode.Visible = true;
                lblTrainingFrom.Visible = true;
                lblTrainingTo.Visible = true;

            }
        }

        private void pg4Back_Click(object sender, EventArgs e)
        {
            //tabMain.SelectedIndex = 2;
            grpPage1.Visible = false;
            grpPage2.Visible = false;
            grpPage3.Visible = true;
            grpPage4.Visible = false;
        }

        private void btnFamily_Click(object sender, EventArgs e)
        {
            frmFamily frmChFamily = new frmFamily();
            frmChFamily.objApplication = this;
            frmChFamily.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (sType == "Edit")
            {
                frmEditingInfo newEditInfo = new frmEditingInfo();
                this.Close();
                newEditInfo.Show();
            }
            else
            {
                if (cmbType_optional.Text == "E")
                {
                    Search newSearch = new Search("E");
                    this.Close();
                    newSearch.Show();
                }
                else if (cmbType_optional.Text == "A")
                {
                    Search newSearch = new Search("A");
                    this.Close();
                    newSearch.Show();
                }
            }
        }

        private void btnEducation_Click(object sender, EventArgs e)
        {
            frmEducation frmChl = new frmEducation();
            frmChl.objApplication = this;
            frmChl.ShowDialog();
        }

        private void btnShortCourses_Click(object sender, EventArgs e)
        {
            frmShortCourse frmSChld = new frmShortCourse();
            frmSChld.objApplication = this;
            frmSChld.ShowDialog();
        }

        private void btnECA_Click(object sender, EventArgs e)
        {
            frmECA frmchld = new frmECA();
            frmchld.objApplication = this;
            frmchld.ShowDialog();
        }

        private void btnReferences_Click(object sender, EventArgs e)
        {
            frmReference objRefChld = new frmReference();
            objRefChld.objApplication = this;
            objRefChld.ShowDialog();
        }

        private void btnExperience_Click(object sender, EventArgs e)
        {
            frmExperience frmChld = new frmExperience();
            frmChld.objApplication = this;
            frmChld.ShowDialog();
        }

        #region "EDITING AND DELETE THE ROWS"
        private void gvFamily_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (gvFamily.Rows[e.RowIndex].Cells["Edit"].Value.ToString().Trim() != "")
                {
                    if (Convert.ToBoolean(gvFamily.Rows[e.RowIndex].Cells["Edit"].Selected) == true)
                    {
                        int SlNo = Convert.ToInt32(gvFamily.Rows[e.RowIndex].Cells[gvFamily.Columns["SlNo"].Index].Value);
                        DataRow[] dr = dtFamily.Select("SlNo=" + SlNo);
                        frmFamily ofamily = new frmFamily(dr);
                        ofamily.objApplication = this;
                        ofamily.ShowDialog();
                    }
                }
                if (e.ColumnIndex == gvFamily.Columns["Delete"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvFamily.Rows[e.RowIndex].Cells[gvFamily.Columns["SlNo"].Index].Value);
                        DataRow[] dr = dtFamily.Select("SlNo=" + SlNo);
                        dtFamily.Rows.Remove(dr[0]);
                        GetDGVFamily();
                        MessageBox.Show("Selected information Has Been Deleted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void gvEducation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (gvEducation.Rows[e.RowIndex].Cells["Edit_edu"].Value.ToString().Trim() != "")
                {
                    if (Convert.ToBoolean(gvEducation.Rows[e.RowIndex].Cells["Edit_edu"].Selected) == true)
                    {
                        int SlNo = Convert.ToInt32(gvEducation.Rows[e.RowIndex].Cells[gvEducation.Columns["SLNO_edu"].Index].Value);
                        DataRow[] dr = dtEducation.Select("SLNO_edu=" + SlNo);
                        frmEducation oEducation = new frmEducation(dr);
                        oEducation.objApplication = this;
                        oEducation.ShowDialog();
                    }
                }
                if (e.ColumnIndex == gvEducation.Columns["Del_edu"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvEducation.Rows[e.RowIndex].Cells[gvEducation.Columns["SLNO_edu"].Index].Value);
                        DataRow[] dr = dtEducation.Select("SLNO_edu=" + SlNo);
                        dtEducation.Rows.Remove(dr[0]);
                        GetDGVEducation();
                        MessageBox.Show("Selected information Has Been Deleted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void gvShortCourse_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (gvShortCourse.Rows[e.RowIndex].Cells["Edit_sc"].Value.ToString().Trim() != "")
                {
                    if (Convert.ToBoolean(gvShortCourse.Rows[e.RowIndex].Cells["Edit_sc"].Selected) == true)
                    {
                        int SlNo = Convert.ToInt32(gvShortCourse.Rows[e.RowIndex].Cells[gvShortCourse.Columns["SLNO_sc"].Index].Value);
                        DataRow[] dr = dtShortCourse.Select("SLNO_sc=" + SlNo);
                        frmShortCourse oShortCourse = new frmShortCourse(dr);
                        oShortCourse.objApplication = this;
                        oShortCourse.ShowDialog();
                    }
                }
                if (e.ColumnIndex == gvShortCourse.Columns["Del_sc"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvShortCourse.Rows[e.RowIndex].Cells[gvShortCourse.Columns["SLNO_sc"].Index].Value);
                        DataRow[] dr = dtShortCourse.Select("SLNO_sc=" + SlNo);
                        dtShortCourse.Rows.Remove(dr[0]);
                        GetDGVShortCourse();
                        MessageBox.Show("Selected information Has Been Deleted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void gvECA_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (gvECA.Rows[e.RowIndex].Cells["Edit_eca"].Value.ToString().Trim() != "")
                {
                    if (Convert.ToBoolean(gvECA.Rows[e.RowIndex].Cells["Edit_eca"].Selected) == true)
                    {
                        int SlNo = Convert.ToInt32(gvECA.Rows[e.RowIndex].Cells[gvECA.Columns["SLNO_eca"].Index].Value);
                        DataRow[] dr = dtECA.Select("SLNO_eca=" + SlNo);
                        frmECA oECA = new frmECA(dr);
                        oECA.objApplication = this;
                        oECA.ShowDialog();
                    }
                }
                if (e.ColumnIndex == gvECA.Columns["Del_eca"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvECA.Rows[e.RowIndex].Cells[gvECA.Columns["SLNO_eca"].Index].Value);
                        DataRow[] dr = dtECA.Select("SLNO_eca=" + SlNo);
                        dtECA.Rows.Remove(dr[0]);
                        GetDGVECA();
                        MessageBox.Show("Selected information Has Been Deleted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void gvExperience_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (gvExperience.Rows[e.RowIndex].Cells["Edit_ex"].Value.ToString().Trim() != "")
                {
                    if (Convert.ToBoolean(gvExperience.Rows[e.RowIndex].Cells["Edit_ex"].Selected) == true)
                    {
                        int SlNo = Convert.ToInt32(gvExperience.Rows[e.RowIndex].Cells[gvExperience.Columns["SlNo_ex"].Index].Value);
                        DataRow[] dr = dtExperience.Select("SlNo_ex=" + SlNo);
                        frmExperience oExpe = new frmExperience(dr);
                        oExpe.objApplication = this;
                        oExpe.ShowDialog();
                    }
                }
                if (e.ColumnIndex == gvExperience.Columns["Del_ex"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvExperience.Rows[e.RowIndex].Cells[gvExperience.Columns["SlNo_ex"].Index].Value);
                        DataRow[] dr = dtExperience.Select("SlNo_ex=" + SlNo);
                        dtExperience.Rows.Remove(dr[0]);
                        GetExperience();
                        MessageBox.Show("Selected information Has Been Deleted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void gvReference_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (gvReference.Rows[e.RowIndex].Cells["Edit_ref"].Value.ToString().Trim() != "")
                {
                    if (Convert.ToBoolean(gvReference.Rows[e.RowIndex].Cells["Edit_ref"].Selected) == true)
                    {
                        int SlNo = Convert.ToInt32(gvReference.Rows[e.RowIndex].Cells[gvReference.Columns["SlNo_ref"].Index].Value);
                        DataRow[] dr = dtReference.Select("SlNo_ref=" + SlNo);
                        frmReference oExpe = new frmReference(dr);
                        oExpe.objApplication = this;
                        oExpe.ShowDialog();
                    }
                }
                if (e.ColumnIndex == gvReference.Columns["Del_ref"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvReference.Rows[e.RowIndex].Cells[gvReference.Columns["SlNo_ref"].Index].Value);
                        DataRow[] dr = dtReference.Select("SlNo_ref=" + SlNo);
                        dtReference.Rows.Remove(dr[0]);
                        GetReference();
                        MessageBox.Show("Selected information Has Been Deleted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void gvDocuments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //if (Convert.ToBoolean(gvDocuments.Rows[e.RowIndex].Cells[0].Value) == true)
                //{
                //    gvDocuments.Rows[e.RowIndex].Cells[2].Value = System.DateTime.Now.ToString("dd/MM/yyyy");
                //}
                //if (gvDocuments.Rows[e.RowIndex].Cells["Edit_doc"].Value.ToString().Trim() != "")
                //{
                //    if (Convert.ToBoolean(gvDocuments.Rows[e.RowIndex].Cells["Edit_doc"].Selected) == true)
                //    {
                //        int SlNo = Convert.ToInt32(gvDocuments.Rows[e.RowIndex].Cells[gvDocuments.Columns["SlNo_Doc"].Index].Value);
                //        DataRow[] dr = dtDocuments.Select("SlNo_Doc=" + SlNo);
                //        frmDocuments oExpe = new frmDocuments(dr);
                //        oExpe.objApplication = this;
                //        oExpe.ShowDialog();
                //    }
                //}
                //if (e.ColumnIndex == gvDocuments.Columns["Del_doc"].Index)
                //{
                //    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //    if (dlgResult == DialogResult.Yes)
                //    {
                //        int SlNo = Convert.ToInt32(gvDocuments.Rows[e.RowIndex].Cells[gvDocuments.Columns["SlNo_Doc"].Index].Value);
                //        DataRow[] dr = dtDocuments.Select("SlNo_Doc=" + SlNo);
                //        dtDocuments.Rows.Remove(dr[0]);
                //        
                //        MessageBox.Show("Selected information Has Been Deleted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    }
                //}
            }
        }
        #endregion

        #region "GRIDVIEW DETAILS"
        public void GetDGVFamily()
        {
            int intRow = 1;
            gvFamily.Rows.Clear();
            for (int i = 0; i < dtFamily.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                dtFamily.Rows[i]["SLNO"] = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellRelationShip = new DataGridViewTextBoxCell();
                cellRelationShip.Value = dtFamily.Rows[i]["RelationShip"];
                tempRow.Cells.Add(cellRelationShip);

                DataGridViewCell cellsName = new DataGridViewTextBoxCell();
                cellsName.Value = dtFamily.Rows[i]["sName"];
                tempRow.Cells.Add(cellsName);

                DataGridViewCell cellDateofBirth = new DataGridViewTextBoxCell();
                cellDateofBirth.Value = Convert.ToDateTime(dtFamily.Rows[i]["DateofBirth"]).ToString("dd/MM/yyyy");
                tempRow.Cells.Add(cellDateofBirth);

                DataGridViewCell cellResiding = new DataGridViewTextBoxCell();
                cellResiding.Value = dtFamily.Rows[i]["Residing"];
                tempRow.Cells.Add(cellResiding);

                DataGridViewCell cellDepending = new DataGridViewTextBoxCell();
                cellDepending.Value = dtFamily.Rows[i]["Depending"];
                tempRow.Cells.Add(cellDepending);

                DataGridViewCell cellOccupation = new DataGridViewTextBoxCell();
                cellOccupation.Value = dtFamily.Rows[i]["Occupation"];
                tempRow.Cells.Add(cellOccupation);
                intRow = intRow + 1;
                gvFamily.Rows.Add(tempRow);
            }
        }

        public void GetDGVEducation()
        {
            int intRow = 1;
            gvEducation.Rows.Clear();
            for (int i = 0; i < dtEducation.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                dtEducation.Rows[i]["SLNO_edu"] = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellExamPass = new DataGridViewTextBoxCell();
                cellExamPass.Value = dtEducation.Rows[i]["ExamPass"];
                tempRow.Cells.Add(cellExamPass);

                DataGridViewCell cellYearPass = new DataGridViewTextBoxCell();
                cellYearPass.Value = dtEducation.Rows[i]["YearPass"];
                tempRow.Cells.Add(cellYearPass);

                DataGridViewCell cellInstName = new DataGridViewTextBoxCell();
                cellInstName.Value = dtEducation.Rows[i]["InstName"];
                tempRow.Cells.Add(cellInstName);

                DataGridViewCell cellInstLocation = new DataGridViewTextBoxCell();
                cellInstLocation.Value = dtEducation.Rows[i]["InstLocation"];
                tempRow.Cells.Add(cellInstLocation);

                DataGridViewCell cellSubject = new DataGridViewTextBoxCell();
                cellSubject.Value = dtEducation.Rows[i]["Subject"];
                tempRow.Cells.Add(cellSubject);

                DataGridViewCell cellUniversity = new DataGridViewTextBoxCell();
                cellUniversity.Value = dtEducation.Rows[i]["University"];
                tempRow.Cells.Add(cellUniversity);

                DataGridViewCell cellPerofPass = new DataGridViewTextBoxCell();
                cellPerofPass.Value = dtEducation.Rows[i]["PerofPass"];
                tempRow.Cells.Add(cellPerofPass);

                intRow = intRow + 1;
                gvEducation.Rows.Add(tempRow);
            }
        }

        public void GetDGVShortCourse()
        {
            int intRow = 1;
            gvShortCourse.Rows.Clear();
            for (int i = 0; i < dtShortCourse.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                dtShortCourse.Rows[i]["SLNO_sc"] = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellCourseName = new DataGridViewTextBoxCell();
                cellCourseName.Value = dtShortCourse.Rows[i]["CourseName"];
                tempRow.Cells.Add(cellCourseName);

                DataGridViewCell cellYearPass = new DataGridViewTextBoxCell();
                cellYearPass.Value = dtShortCourse.Rows[i]["YearofPass"];
                tempRow.Cells.Add(cellYearPass);

                DataGridViewCell cellInstName = new DataGridViewTextBoxCell();
                cellInstName.Value = dtShortCourse.Rows[i]["Insti_name"];
                tempRow.Cells.Add(cellInstName);

                DataGridViewCell cellInstLocation = new DataGridViewTextBoxCell();
                cellInstLocation.Value = dtShortCourse.Rows[i]["Insti_Location"];
                tempRow.Cells.Add(cellInstLocation);

                DataGridViewCell cellSubject = new DataGridViewTextBoxCell();
                cellSubject.Value = dtShortCourse.Rows[i]["Subject"];
                tempRow.Cells.Add(cellSubject);

                DataGridViewCell cellDuration = new DataGridViewTextBoxCell();
                cellDuration.Value = dtShortCourse.Rows[i]["Duration"];
                tempRow.Cells.Add(cellDuration);

                DataGridViewCell cellPerofPass = new DataGridViewTextBoxCell();
                cellPerofPass.Value = dtShortCourse.Rows[i]["PerofMarks"];
                tempRow.Cells.Add(cellPerofPass);

                intRow = intRow + 1;
                gvShortCourse.Rows.Add(tempRow);
            }
        }

        public void GetDGVECA()
        {
            int intRow = 1;
            gvECA.Rows.Clear();
            for (int i = 0; i < dtECA.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                dtECA.Rows[i]["SLNO_eca"] = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellTypeofECA = new DataGridViewTextBoxCell();
                cellTypeofECA.Value = dtECA.Rows[i]["TypeofECA"];
                tempRow.Cells.Add(cellTypeofECA);

                DataGridViewCell cellRemarks = new DataGridViewTextBoxCell();
                cellRemarks.Value = dtECA.Rows[i]["Remarks"];
                tempRow.Cells.Add(cellRemarks);
                intRow = intRow + 1;
                gvECA.Rows.Add(tempRow);
            }
        }

        public void GetDGVLanguageDetails()
        {
            int intRow = 1;
            gvLanguages.Rows.Clear();
            for (int i = 0; i < dtLanguages.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                //dtECA.Rows[i]["SLNO_lg"] = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellLang = new DataGridViewTextBoxCell();
                cellLang.Value = dtLanguages.Rows[i]["Language"];
                tempRow.Cells.Add(cellLang);

                DataGridViewCell cellread = new DataGridViewTextBoxCell();
                cellread.Value = dtLanguages.Rows[i]["ReadFlag"];
                tempRow.Cells.Add(cellread);

                DataGridViewCell cellWrite = new DataGridViewTextBoxCell();
                cellWrite.Value = dtLanguages.Rows[i]["WriteFlag"];
                tempRow.Cells.Add(cellWrite);

                DataGridViewCell cellSpeak = new DataGridViewTextBoxCell();
                cellSpeak.Value = dtLanguages.Rows[i]["SpeakFlag"];
                tempRow.Cells.Add(cellSpeak);

                intRow = intRow + 1;
                gvLanguages.Rows.Add(tempRow);
            }
        }

        public void GetExperience()
        {
            int intRow = 1;
            gvExperience.Rows.Clear();
            for (int i = 0; i < dtExperience.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                dtExperience.Rows[i]["SlNo_ex"] = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellOrg = new DataGridViewTextBoxCell();
                cellOrg.Value = dtExperience.Rows[i]["Organisation"];
                tempRow.Cells.Add(cellOrg);

                DataGridViewCell cellFromDate = new DataGridViewTextBoxCell();
                cellFromDate.Value = dtExperience.Rows[i]["FromDate"];
                tempRow.Cells.Add(cellFromDate);

                DataGridViewCell cellToDate = new DataGridViewTextBoxCell();
                cellToDate.Value = dtExperience.Rows[i]["ToDate"];
                tempRow.Cells.Add(cellToDate);

                DataGridViewCell cellSalary = new DataGridViewTextBoxCell();
                cellSalary.Value = dtExperience.Rows[i]["Salary"];
                tempRow.Cells.Add(cellSalary);

                DataGridViewCell cellRemarks = new DataGridViewTextBoxCell();
                cellRemarks.Value = dtExperience.Rows[i]["Remarks_ex"];
                tempRow.Cells.Add(cellRemarks);

                intRow = intRow + 1;
                gvExperience.Rows.Add(tempRow);
            }
        }

        public void GetReference()
        {
            int intRow = 1;
            gvReference.Rows.Clear();
            for (int i = 0; i < dtReference.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                dtReference.Rows[i]["SlNo_ref"] = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellrefName = new DataGridViewTextBoxCell();
                cellrefName.Value = dtReference.Rows[i]["refName"];
                tempRow.Cells.Add(cellrefName);

                DataGridViewCell cellrefOccup = new DataGridViewTextBoxCell();
                cellrefOccup.Value = dtReference.Rows[i]["refOccup"];
                tempRow.Cells.Add(cellrefOccup);

                DataGridViewCell cellrefPhoneNo = new DataGridViewTextBoxCell();
                cellrefPhoneNo.Value = dtReference.Rows[i]["refPhoneNo"];
                tempRow.Cells.Add(cellrefPhoneNo);

                DataGridViewCell cellVillage = new DataGridViewTextBoxCell();
                cellVillage.Value = dtReference.Rows[i]["Village"];
                tempRow.Cells.Add(cellVillage);

                DataGridViewCell cellPhoneNoref = new DataGridViewTextBoxCell();
                cellPhoneNoref.Value = dtReference.Rows[i]["PhoneNoref"];
                tempRow.Cells.Add(cellPhoneNoref);

                intRow = intRow + 1;
                gvReference.Rows.Add(tempRow);
            }
        }

        #endregion

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            UtilityLibrary oUtility = new UtilityLibrary();
            if (!SSCRM.App_Code.UtilityLibrary.CustomValidate(grpPage1, toolTip1))
            {
                //tabMain.SelectedIndex = 0;
                return;
            }
            if (!SSCRM.App_Code.UtilityLibrary.CustomValidate(grouper2, toolTip1))
            {
                //tabMain.SelectedIndex = 0;
                return;
            }
            if (!SSCRM.App_Code.UtilityLibrary.CustomValidate(grpPage4, toolTip1))
            {
                //tabMain.SelectedIndex = 3;
                return;
            }
            if (!SSCRM.App_Code.UtilityLibrary.CustomValidate(grouper3, toolTip1))
            {
                //tabMain.SelectedIndex = 0;
                TabAddress.SelectedIndex = 0;
                return;
            }
            if (!SSCRM.App_Code.UtilityLibrary.CustomValidate(grouper4, toolTip1))
            {
                //tabMain.SelectedIndex = 0;
                TabAddress.SelectedIndex = 1;
                return;
            }
            if (!SSCRM.App_Code.UtilityLibrary.CustomValidate(grouper7, toolTip1))
            {
                //tabMain.SelectedIndex = 0;
                TabAddress.SelectedIndex = 2;
                return;
            }
            if (dtEducation.Rows.Count == 0)
            {
                MessageBox.Show("Please enter education details.", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool iChk = true;
            int icnt = 0;
            for (int i = 0; i < gvDocuments.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvDocuments.Rows[i].Cells[0].Value) != false)
                {
                    if (gvDocuments.Rows[i].Cells[2].Value.ToString() == "")
                        iChk = false;
                }
                else
                    icnt++;
            }
            if (icnt == 8)
            {
                iChk = false;
            }
            if (iChk == false)
            {
                MessageBox.Show("Please Enter Documents Details", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            objSQLDB = new SQLDB();
            string sConnVal = ConfigurationSettings.AppSettings["DBCon"].ToString();
            objSecurity = new Security();
            SqlConnection CN = new SqlConnection(objSecurity.GetDecodeString(sConnVal));
            //SqlTransaction transaction;
            CN.Open();
            //transaction = CN.BeginTransaction();
            try
            {
                byte[] imageData = { 0 };
                if (lblPath.Text != "")
                    imageData = ReadFile(lblPath.Text);

                if (PermentAddCtr.Pin == "")
                    PermentAddCtr.Pin = "0";
                if (PresentAddCtr.Pin == "")
                    PresentAddCtr.Pin = "0";
                if (ContactAddCtr.Pin == "")
                    ContactAddCtr.Pin = "0";
                int iMaxNo = 0;

                string ReqECode = "", InterViewECode = "";
                ReqECode = eCodeCtrlRec.ECode;
                if (ReqECode == "")
                {
                    MessageBox.Show("Enter Recruiter Ecode", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ReqECode = "0";
                    return;
                    
                }
                if (eCodeCtrlRec.EName == "")
                {
                    MessageBox.Show("Enter Recruiter Ecode", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (eCodeCtrlInterivewed.EName == "")
                {
                    MessageBox.Show("Invalid InterViewed ECode", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    InterViewECode = eCodeCtrlInterivewed.ECode;
                }
                if (cmbType_optional.Text == "A" && cbInductionTraining.SelectedIndex == 0 && eCodeCtrlTrainer.Name == "")
                {
                    MessageBox.Show("Invalid Trainer E-Code", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                dtDocuments = (DataTable)gvDocuments.DataSource;
                string Eora_Mast = "", sReturn = "", Ind_Training = "";
                
                if (AppliNo > 0)
                {
                    iMaxNo = AppliNo;
                    #region "UPDATE THE MAIN DATA "
                    objHrInfo = new HRInfo();
                    var MainHeadrow = new[] {cmbCompany.SelectedValue,cmbBranch.SelectedValue,iMaxNo,System.DateTime.Now,cmbSourceCode.SelectedValue,cmbRequtSourDetails.SelectedValue,ReqECode,
                    txtFullName.Text.Replace(" ", "") ,cmbF_Flg_optional.Text ,txtfName.Text ,dtpDOJ.Value ,dtpDOB.Value ,txtNativePlace_optional.Text ,
                    cmbSex_optional.Text,cmbNationality.Text,cmbReligion.Text,cmbMatrital_optional.Text,dtpMarriedDt.Value,txtNominee_optional.Text,cmbNomRelation_optional.Text,txtHeight_optional.Text,txtWeight_optional.Text,
                    cmbBloodGrp_optional.Text , txtDisabilty_optional.Text , txtillness_optional.Text , txtProlonged_optional.Text ,PresentAddCtr.HouseNo , PresentAddCtr.LandMark , PresentAddCtr.Village , 
                    PresentAddCtr.Mondal , PresentAddCtr.District , PresentAddCtr.State,PresentAddCtr.Pin ,PresentPhone_num.Text , PermentAddCtr.HouseNo , PermentAddCtr.LandMark , PermentAddCtr.Village ,
                    PermentAddCtr.Mondal ,PermentAddCtr.District , PermentAddCtr.State , PermentAddCtr.Pin , txtPermentPhNo_num.Text , txtContactName.Text , ContactAddCtr.HouseNo,ContactAddCtr.LandMark , 
                    ContactAddCtr.Village , ContactAddCtr.Mondal , ContactAddCtr.District , ContactAddCtr.State, ContactAddCtr.Pin ,txtContPhNo_num.Text , txtContPhNo1_optional.Text , cmbVehicleFlg_optional.Text , 
                    txtVehicleNo_optional.Text , txtVehicleMake_optional.Text , txtDlNumber_optional.Text ,txtPassportNumber_optional.Text , txtPanCardNumber_optional.Text ,InterViewECode , dtpInterview.Value , 
                    txtIVRemarks_optional.Text ,cmbType_optional.Text , iMaxArE , CommonData.LogUserId , System.DateTime.Now,txtSSCNumber.Text};

                    if (cmbType_optional.Text.Trim() == "A")
                    {
                        if (cbInductionTraining.Text != "NO")
                            Ind_Training = cbInductionTraining.Text + "," + eCodeCtrlTrainer.ECode + "," + dtpTrainingFrom_optional.Value + "," + dtpTrainingTo_optional.Text;
                        else
                            Ind_Training = "";
                    }
                    else
                        Ind_Training = "";
                    if (cmbType_optional.Text.Trim() == "A")
                        Eora_Mast = cmbBranch.SelectedValue + "," + "1200000" + "," + iMaxNo + "," + txtFullName.Text + "," + txtFullName.Text + "," + "A" + "," + 987 + "," + "AGENT" + "," + dtpDOJ.Value.ToString("dd/MMM/yyyy") + "," + dtpDOB.Value.ToString("dd/MMM/yyyy") + "," + txtfName.Text + "," + 95 + "," + cmbCompany.SelectedValue;
                    else
                        Eora_Mast = cmbBranch.SelectedValue + "," + cmbDepartment.SelectedValue + "," + iMaxNo + "," + txtFullName.Text + "," + txtFullName.Text + "," + "E" + "," + cmbDesig.SelectedValue + "," + cmbDesig.Text + "," + dtpDOJ.Value.ToString("dd/MMM/yyyy") + "," + dtpDOB.Value.ToString("dd/MMM/yyyy") + "," + txtfName.Text + "," + 95 + "," + cmbCompany.SelectedValue;
                    sReturn = objHrInfo.HRMainheadSave(102, MainHeadrow, Eora_Mast.Split(','), dtFamily, dtEducation, dtShortCourse, dtECA, dtExperience, dtReference, dtDocuments, Ind_Training, dtLanguages);

                    #endregion
                }
                else
                {
                    #region "INSERT THE MAIN DATA "
                    //This is Employee and Ajent wise Generated Code                
                    objHrInfo = new HRInfo();
                    var MainHeadrow = new[] {cmbCompany.SelectedValue,cmbBranch.SelectedValue,iMaxNo,System.DateTime.Now,cmbSourceCode.SelectedValue,cmbRequtSourDetails.SelectedValue,ReqECode,
                    /*txtSurName_optional.Text.Replace(" ", "") + " " + */txtFullName.Text.ToString() ,cmbF_Flg_optional.Text ,txtfName.Text ,dtpDOJ.Value ,dtpDOB.Value ,txtNativePlace_optional.Text ,
                    cmbSex_optional.Text,cmbNationality.Text,cmbReligion.Text,cmbMatrital_optional.Text,Convert.ToDateTime(dtpMarriedDt.Value).ToString("dd/MMM/yyyy"),txtNominee_optional.Text,cmbNomRelation_optional.Text,txtHeight_optional.Text,txtWeight_optional.Text,
                    cmbBloodGrp_optional.Text , txtDisabilty_optional.Text , txtillness_optional.Text , txtProlonged_optional.Text ,PresentAddCtr.HouseNo , PresentAddCtr.LandMark , PresentAddCtr.Village , 
                    PresentAddCtr.Mondal , PresentAddCtr.District , PresentAddCtr.State,PresentAddCtr.Pin ,PresentPhone_num.Text , PermentAddCtr.HouseNo , PermentAddCtr.LandMark , PermentAddCtr.Village ,
                    PermentAddCtr.Mondal ,PermentAddCtr.District , PermentAddCtr.State , PermentAddCtr.Pin , txtPermentPhNo_num.Text , txtContactName.Text , ContactAddCtr.HouseNo,ContactAddCtr.LandMark , 
                    ContactAddCtr.Village , ContactAddCtr.Mondal , ContactAddCtr.District , ContactAddCtr.State, ContactAddCtr.Pin ,txtContPhNo_num.Text , txtContPhNo1_optional.Text , cmbVehicleFlg_optional.Text , 
                    txtVehicleNo_optional.Text , txtVehicleMake_optional.Text , txtDlNumber_optional.Text ,txtPassportNumber_optional.Text , txtPanCardNumber_optional.Text ,InterViewECode , dtpInterview.Value , 
                    txtIVRemarks_optional.Text ,cmbType_optional.Text , iMaxArE , CommonData.LogUserId , System.DateTime.Now,txtSSCNumber.Text};
                    #endregion

                    #region "EORA_MASTER table data Insert Statement"
                    if (cmbType_optional.Text.Trim() == "A")
                    {
                        if (cbInductionTraining.Text == "YES")
                        {
                            if (eCodeCtrlTrainer.EName == "")
                            {
                                MessageBox.Show("Invalid E Code ", "SSCRM - Trainer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            Ind_Training = cbInductionTraining.Text + "," + eCodeCtrlTrainer.ECode + "," + dtpTrainingFrom_optional.Value.ToString("dd/MMM/yyyy") + "," + dtpTrainingTo_optional.Value.ToString("dd/MMM/yyyy");
                        }
                        else
                            Ind_Training = "";
                    }
                    else
                        Ind_Training = "";
                    if (cmbType_optional.Text.Trim() == "A")
                        Eora_Mast = cmbBranch.SelectedValue + "," + "1200000" + "," + iMaxNo + "," + txtFullName.Text + "," + txtFullName.Text + "," + "A" + "," + 987 + "," + "ST" + "," + dtpDOJ.Value.ToString("dd/MMM/yyyy") + "," + dtpDOB.Value.ToString("dd/MMM/yyyy") + "," + txtfName.Text + "," + 95 + "," + cmbCompany.SelectedValue;
                    else
                        Eora_Mast = cmbBranch.SelectedValue + "," + cmbDepartment.SelectedValue + "," + iMaxNo + "," + txtFullName.Text + "," + txtFullName.Text + "," + "E" + "," + cmbDesig.SelectedValue + "," + cmbDesig.Text + "," + dtpDOJ.Value.ToString("dd/MMM/yyyy") + "," + dtpDOB.Value.ToString("dd/MMM/yyyy") + "," + txtfName.Text + "," + 95 + "," + cmbCompany.SelectedValue;

                    
                    #endregion
                    sReturn = objHrInfo.HRMainheadSave(101, MainHeadrow, Eora_Mast.Split(','), dtFamily, dtEducation, dtShortCourse, dtECA, dtExperience, dtReference, dtDocuments, Ind_Training, dtLanguages);
                }
                #region "This is used for Image Update"
                if (imageData.Length > 1)
                {
                    objHrInfo = new HRInfo();
                    objHrInfo.UpdatePhoto(iMaxNo, imageData);
                }
                #endregion

                string[] sData = sReturn.Split(',');
                if (sReturn.Contains("Update"))
                {
                    //objHrInfo.SendSMSForEmployeeCodeGeneration(sData[1].ToString(), txtFullName.Text.ToString(), PresentPhone_num.Text, cmbType_optional.Text.ToString(), cmbCompany.Text.ToString());
                    MessageBox.Show(" EORA CODE : " + sData[1].ToString() + "\n The record Updated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (sReturn.Contains("Saved"))
                {
                    //objHrInfo.SendSMSForEmployeeCodeGeneration(sData[1].ToString(), txtFullName.Text.ToString(), PresentPhone_num.Text, cmbType_optional.Text.ToString(), cmbCompany.Text.ToString());
                    MessageBox.Show(" EORA CODE : " + sData[1].ToString() + "\n The record saved successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (cmbType_optional.Text == "E")
                    {
                        objHrInfo.SendSMSForEmployeeCodeGeneration(sData[1].ToString(), txtFullName.Text.ToString(), PresentPhone_num.Text, cmbType_optional.Text.ToString(), cmbCompany.Text.ToString());
                        ////frmAppointment frmChld = new frmAppointment(cmbCompany.SelectedValue.ToString(), cmbBranch.SelectedValue.ToString(), Convert.ToInt32(sData[2]), sData[1].ToString());
                        ////frmChld.Show();
                        ////this.Close();
                        //return;
                    }
                }
                else
                {
                    MessageBox.Show(sReturn, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (sType == "Edit")
                {
                    frmEditingInfo ofrmEdit = new frmEditingInfo();
                    ofrmEdit.Show();
                    this.Close();
                    
                }
                else if (sType != "Approved")
                {
                    Search frmChld = new Search(cmbType_optional.Text);
                    frmChld.Show();
                    this.Close();
                    
                }
                else
                {
                    frmApprovedStatus frmApproved = new frmApprovedStatus();
                    frmApproved.Show();
                    this.Close();                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objHrInfo = null;
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
                pictureBox1.BackgroundImage = newImage;
                this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            }
            catch// (Exception ex)
            {
            }
        }

        private void dtpDOB_ValueChanged(object sender, EventArgs e)
        {
            DateTime zeroTime = new DateTime(1, 1, 1);
            DateTime a = Convert.ToDateTime(System.DateTime.Now.ToString("dd/MMM/yyyy"));
            DateTime b = Convert.ToDateTime(dtpDOB.Value.ToString("dd/MMM/yyyy"));
            if (a > b)
            {
                TimeSpan ival = a - b;
                int years = (zeroTime + ival).Year - 1;
                txtAge.Text = years.ToString();
            }
            else
            {
                dtpDOB.Value = Convert.ToDateTime(CommonData.CurrentDate);
            }
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "Images (*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|" + "All files (*.*)|*.*";
            od.Multiselect = true;
            if (od.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lblPath.Text = od.FileNames[0].ToString();
                Image loadedImage = Image.FromFile(lblPath.Text);
                //if (loadedImage.Height > 600 || loadedImage.Width > 800)
                //{
                //    lblPath.Text = "";
                //    MessageBox.Show("Please select image between size(600 W * 800 H)", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                pictureBox1.BackgroundImage = loadedImage;
            }
        }

        byte[] ReadFile(string sPath)
        {
            //Initialize byte array with a null value initially.           
            byte[] data = null;

            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);

            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader(fStream);

            //When you use BinaryReader, you need to supply number of bytes to read from file.
            //In this case we want to read entire file. So supplying total number of bytes.
            data = br.ReadBytes((int)numBytes);
            return data;
        }

        private void chkPermanent_Click(object sender, EventArgs e)
        {
            if (chkPermanent.Checked == true)
            {
                PermentAddCtr.HouseNo = PresentAddCtr.HouseNo;
                PermentAddCtr.LandMark = PresentAddCtr.LandMark;
                PermentAddCtr.Village = PresentAddCtr.Village;
                PermentAddCtr.Mondal = PresentAddCtr.Mondal;
                PermentAddCtr.District = PresentAddCtr.District;
                PermentAddCtr.State = PresentAddCtr.State;
                PermentAddCtr.Pin = PresentAddCtr.Pin;
                txtPermentPhNo_num.Text = PresentPhone_num.Text;
            }
            else
            {
                PermentAddCtr.HouseNo = "";
                PermentAddCtr.LandMark = "";
                PermentAddCtr.Village = "";
                PermentAddCtr.Mondal = "";
                PermentAddCtr.District = "";
                PermentAddCtr.State = "";
                PermentAddCtr.Pin = "";
                txtPermentPhNo_num.Text = "";
            }
        }

        private void chkContact_Click(object sender, EventArgs e)
        {
            if (chkContact.Checked == true)
            {
                ContactAddCtr.HouseNo = PresentAddCtr.HouseNo;
                ContactAddCtr.LandMark = PresentAddCtr.LandMark;
                ContactAddCtr.Village = PresentAddCtr.Village;
                ContactAddCtr.Mondal = PresentAddCtr.Mondal;
                ContactAddCtr.District = PresentAddCtr.District;
                ContactAddCtr.State = PresentAddCtr.State;
                ContactAddCtr.Pin = PresentAddCtr.Pin;
                txtContPhNo_num.Text = PresentPhone_num.Text;
            }
            else
            {
                ContactAddCtr.HouseNo = "";
                ContactAddCtr.LandMark = "";
                ContactAddCtr.Village = "";
                ContactAddCtr.Mondal = "";
                ContactAddCtr.District = "";
                ContactAddCtr.State = "";
                ContactAddCtr.Pin = "";
                txtContPhNo_num.Text = "";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //ResetFields(this);
            //gvDocuments.Rows.Clear();
            //gvECA.Rows.Clear();
            //gvEducation.Rows.Clear();
            //gvExperience.Rows.Clear();
            //gvFamily.Rows.Clear();
            //gvReference.Rows.Clear();
            //gvShortCourse.Rows.Clear();
            this.Close();
        }

        public static void ResetFields(Control form)
        {
            foreach (Control ctrl in form.Controls)
            {
                if (ctrl.Controls.Count > 0)
                    ResetFields(ctrl);
                Reset(ctrl);
            }
        }

        public static void Reset(Control ctrl)
        {
            if (ctrl is TextBox)
            {
                TextBox tb = (TextBox)ctrl;
                if (tb != null)
                {
                    tb.ResetText();
                }
            }
            else if (ctrl is ComboBox)
            {
                ComboBox dd = (ComboBox)ctrl;
                if (dd != null)
                {
                    dd.SelectedIndex = 0;
                }
            }
        }

        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCompany.SelectedIndex > 0)
            {
                objHrInfo = new HRInfo();
                DataTable dtBranch = objHrInfo.GetAllBranchList(cmbCompany.SelectedValue.ToString(), "", "").Tables[0];
                UtilityLibrary.PopulateControl(cmbBranch, dtBranch.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
                objHrInfo = null;
            }
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDepartment.SelectedIndex > 0)
            {
                objSQLDB = new SQLDB();
                DataView dvDesg = objSQLDB.ExecuteDataSet("SELECT DESIG_CODE,Desig_Name From DESIG_MAS WHERE DEPT_CODE=" + cmbDepartment.SelectedValue, CommandType.Text).Tables[0].DefaultView;
                UtilityLibrary.PopulateControl(cmbDesig, dvDesg, 1, 0, "--PLEASE SELECT--", 0);
            }
        }

        private void cmbSourceCode_optional_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSourceCode.SelectedIndex > 0)
            {
                objSQLDB = new SQLDB();
                string strReq = "SELECT HRSMD_RECRUITMENT_SOURCE_DETL_CODE,HRSMD_RECRUITMENT_SOURCE_DETL_NAME From HR_RECRUITMENT_SOURCE_MASTER_DETL WHERE HRSMD_RECRUITMENT_SOURCE_CODE='" + cmbSourceCode.SelectedValue + "'";
                DataTable dtRQT = objSQLDB.ExecuteDataSet(strReq, CommandType.Text).Tables[0];
                UtilityLibrary.PopulateControl(cmbRequtSourDetails, dtRQT.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
                objSQLDB = null;
            }
        }
        private void cmbRequtSourDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbRequtSourDetails.SelectedValue != null)
            //{
            //    if (cmbRequtSourDetails.SelectedValue.ToString() == "RSD000005")
            //    {
            //        eCodeCtrlRec.Enabled = false;
            //        eCodeCtrlRec.ECode = "0";
            //    }
            //    else
            //        eCodeCtrlRec.Enabled = true;
            //}
        }

        public bool GetAddressCheck()
        {
            bool iretu = true;
            if ((PresentAddCtr.HouseNo == "") && (PresentAddCtr.Village == "") && (PresentAddCtr.Mondal == "") && (PresentAddCtr.District == "") && (PresentAddCtr.State == "") && (PresentAddCtr.Pin == ""))
                iretu = false;
            if ((PermentAddCtr.HouseNo == "") && (PermentAddCtr.Village == "") && (PermentAddCtr.Mondal == "") && (PermentAddCtr.District == "") && (PermentAddCtr.State == "") && (PermentAddCtr.Pin == ""))
                iretu = false;
            if ((ContactAddCtr.HouseNo == "") && (ContactAddCtr.Village == "") && (ContactAddCtr.Mondal == "") && (ContactAddCtr.District == "") && (ContactAddCtr.State == "") && (ContactAddCtr.Pin == ""))
                iretu = false;
            return iretu;
        }

        private void txtFullName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar != '\b') && (e.KeyChar != ' '))
            {
                if (!char.IsLetter((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtSurName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar != '\b') && (e.KeyChar != ' '))
            {
                if (!char.IsLetter((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtfName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar != '\b') && (e.KeyChar != ' '))
            {
                if (!char.IsLetter((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void cmbVehicleFlg_optional_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbVehicleFlg_optional.SelectedIndex == 1)
            {
                txtVehicleNo_optional.Enabled = false;
                txtVehicleMake_optional.Enabled = false;
                txtDlNumber_optional.Enabled = false;
                txtVehicleNo_optional.Text = "";
                txtVehicleMake_optional.Text = "";
                txtDlNumber_optional.Text = "";
            }
            else
            {
                txtVehicleNo_optional.Enabled = true;
                txtVehicleMake_optional.Enabled = true;
                txtDlNumber_optional.Enabled = true;
            }
        }

        private void cmbMatrital_optional_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMatrital_optional.SelectedIndex == 1)
                dtpMarriedDt.Enabled = false;
            else
                dtpMarriedDt.Enabled = true;
        }

        private void txtHeight_num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)) && e.KeyChar!='.')
                {
                    e.Handled = true;
                }
            }
        }

        private void txtWeight_num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void PresentPhone_num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtContPhNo_num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtContPhNo1_num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void cbInductionTraining_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbInductionTraining.SelectedIndex == 2)
            {
                eCodeCtrlTrainer.Enabled = false;
                dtpTrainingFrom_optional.Enabled = false;
                dtpTrainingTo_optional.Enabled = false;
                eCodeCtrlTrainer.ECode = "0";

            }
            else
            {
                eCodeCtrlTrainer.Enabled = true;
                dtpTrainingFrom_optional.Enabled = true;
                dtpTrainingTo_optional.Enabled = true;
            }
        }

        private void txtTrainerEcode_optional_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void gvDocuments_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0)
            {
                DateTime dtVal;

                try
                {
                    if (Convert.ToBoolean(gvDocuments.Rows[e.RowIndex].Cells[0].Value) == true)
                    {
                        if (gvDocuments.Rows[e.RowIndex].Cells[2].Value.ToString() != "")
                        {
                            dtVal = Convert.ToDateTime(gvDocuments.Rows[e.RowIndex].Cells[2].Value);
                        }
                        else
                        {
                            gvDocuments.Rows[e.RowIndex].Cells[2].Value = System.DateTime.Now.ToString("dd/MMM/yyyy");
                        }
                    }
                    else
                    {
                        gvDocuments.Rows[e.RowIndex].Cells[2].Value = "";
                    }
                }
                catch// (Exception ex)
                {
                    gvDocuments.Rows[e.RowIndex].Cells[2].Value = "";
                    return;
                }
            }
        }

        private void btnLangAdd_Click(object sender, EventArgs e)
        {
            frmKnownLanguages childfrmKnownLanguages = new frmKnownLanguages();
            childfrmKnownLanguages.objApplication = this;
            childfrmKnownLanguages.ShowDialog();
        }

        private void gvLanguages_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (gvLanguages.Rows[e.RowIndex].Cells["Edit_lg"].Value.ToString().Trim() != "")
                {
                    if (Convert.ToBoolean(gvLanguages.Rows[e.RowIndex].Cells["Edit_lg"].Selected) == true)
                    {
                        int SlNo = Convert.ToInt32(gvLanguages.Rows[e.RowIndex].Cells[gvLanguages.Columns["SlNo_lg"].Index].Value);
                        DataRow[] dr = dtLanguages.Select("SlNO_lg=" + SlNo);
                        frmKnownLanguages oKnownLanguages = new frmKnownLanguages(dr);
                        oKnownLanguages.objApplication = this;
                        oKnownLanguages.ShowDialog();
                    }
                }
                if (e.ColumnIndex == gvLanguages.Columns["Del_lg"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvLanguages.Rows[e.RowIndex].Cells[gvLanguages.Columns["SlNo_lg"].Index].Value);
                        DataRow[] dr = dtLanguages.Select("SlNO_lg=" + SlNo);
                        dtLanguages.Rows.Remove(dr[0]);
                        GetDGVLanguageDetails();
                        MessageBox.Show("Selected information Has Been Deleted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
