using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Diagnostics;
using SSCRMDB;
using System.Xml.Linq;
using System.Net;
using System.Xml;
using System.IO;

namespace SSTrans
{
    public class HRInfo
    {
        private Security objSecurity = null;
        private SQLDB objSQLdb = null;
        string sConnVal = ConfigurationSettings.AppSettings["DBCon"].ToString();
        public int SetImage(int ApplNo, byte[] sMyphoto)
        {
            int iretuval = 0;
            try
            {
                string sConnVal = ConfigurationSettings.AppSettings["DBCon"].ToString();
                objSecurity = new Security();
                SqlConnection CN = new SqlConnection(objSecurity.GetDecodeString(sConnVal));

                string qry = "UPDATE HR_APPL_MASTER_HEAD SET HAMH_MY_PHOTO=@MYPHOTO WHERE HAMH_APPL_NUMBER=@APPLNO";
                SqlCommand SqlCom = new SqlCommand(qry, CN);
                SqlCom.Parameters.Add(new SqlParameter("@APPLNO", (object)ApplNo));
                SqlCom.Parameters.Add(new SqlParameter("@MYPHOTO", (object)sMyphoto));
                CN.Open();
                SqlCom.CommandTimeout = 10;
                SqlCom.ExecuteNonQuery();
                CN.Close();
                return iretuval = 1;
            }
            catch (Exception ex)
            {
                objSecurity = null;
                return iretuval = 0;
            }
            finally
            {
                objSecurity = null;
            }
        }

        public DataSet GetType(string sType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@Type", DbType.String, sType, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("HR_GetEORACode", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }

        public DataSet GetHRInformation(int iMode, string CompanyCode, string BranchCode, int ApplicationNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@imode", DbType.Int32, iMode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@CompanyCode", DbType.String, CompanyCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@BranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@ApplicatioNo", DbType.Int32, ApplicationNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("HR_GetALLInformation", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }

        public DataSet HR_EmpViewDetails(string sCompanyCode, string sBranchCode, string sEoraFlag, string sStatus)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@CompanyCode", DbType.String, sCompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@BranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@EoraFlag", DbType.String, sEoraFlag, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@Status", DbType.String, sStatus, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("HR_EmpViewDetails", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }

        //public string HRMainheadSave(int iMode, object[] MainHead, string[] EoraMast, DataTable dtFamily, DataTable dtEducation, DataTable dtSortCourse, DataTable dtECA, DataTable dtExperience, DataTable dtReference, DataTable dtDocuments, string Ind_Training)
        //{
        //    int iApplNo = 0;
        //    int iECode = 0;
        //    objSecurity = new Security();
        //    SqlConnection CN = new SqlConnection(objSecurity.GetDecodeString(sConnVal));
        //    SqlTransaction transaction;
        //    CN.Open();
        //    transaction = CN.BeginTransaction();
        //    try
        //    {
        //        #region "MAIN HEAD TABLE DATA"
        //        objSecurity = new Security();
        //        SqlCommand cmdUpdate = new SqlCommand("HR_IU_MainHead1", CN, transaction);
        //        cmdUpdate.CommandType = CommandType.StoredProcedure;
        //        //cmdUpdate.CommandTimeout = 1000;
        //        cmdUpdate.Parameters.AddWithValue("@inMode", iMode);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_COMPANY_CODE", MainHead[0].ToString());
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_BRANCH_CODE", MainHead[1].ToString());
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_APPL_NUMBER", Convert.ToInt32(MainHead[2]));
        //        cmdUpdate.Parameters.AddWithValue("@dtHAMH_APPL_DATE", Convert.ToDateTime(MainHead[3]));
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_RECRUITMENT_SOURCE_CODE", MainHead[4].ToString());
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_RECRUITMENT_SOURCE_DELT_CODE", MainHead[5].ToString());
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_RECRUITMENT_SOURCE_ECODE", MainHead[6]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_NAME", MainHead[7]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_FORH", MainHead[8]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_FORH_NAME", MainHead[9]);
        //        cmdUpdate.Parameters.AddWithValue("@dtHAMH_DOJ", Convert.ToDateTime(MainHead[10]));
        //        cmdUpdate.Parameters.AddWithValue("@dtHAMH_DOB", Convert.ToDateTime(MainHead[11]));
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_NATIVE_PLACE", MainHead[12]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_SEX", MainHead[13]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_NAIONALITY", MainHead[14]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_RELIGION", MainHead[15]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_MATRITAL_STATUS", MainHead[16]);
        //        cmdUpdate.Parameters.AddWithValue("@dtHAMH_MARRIAGE_DATE", Convert.ToDateTime(MainHead[17]));
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_NOMINIEE_NAME", MainHead[18]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_NOMINIEE_RELATION", MainHead[19]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_PD_HEIGHT", Convert.ToDouble(MainHead[20]));
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_PD_WEIGHT", Convert.ToDouble(MainHead[21]));
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_PD_BLOOD_GROUP_CODE", MainHead[22]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_PD_PHYSICAL_DISABILITY", MainHead[23]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_PD_PROLONGED_ILLNESS", MainHead[24]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_PD_PROLONGED_ILLNESS_PERIOD", MainHead[25]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PRES_ADDR_HNO", MainHead[26]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PRES_ADDR_LANDMARK", MainHead[27]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PRES_ADDR_VILL_OR_TOWN", MainHead[28]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PRES_ADDR_MANDAL", MainHead[29]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PRES_ADDR_DISTRICT", MainHead[30]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PRES_ADDR_STATE", MainHead[31]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PRES_ADDR_PIN", Convert.ToInt32(MainHead[32]));
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PRES_ADDR_PHONE", MainHead[33]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PERM_ADDR_HNO", MainHead[34]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PERM_ADDR_LANDMARK", MainHead[35]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PERM_ADDR_VILL_OR_TOWN", MainHead[36]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PERM_ADDR_MANDAL", MainHead[37]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PERM_ADDR_DISTRICT", MainHead[38]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PERM_ADDR_STATE", MainHead[39]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PERM_ADDR_PIN", Convert.ToInt32(MainHead[40]));
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PERM_ADDR_PHONE", MainHead[41]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_NAME", MainHead[42]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_ADDR_HNO", MainHead[43]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_ADDR_LANDMARK", MainHead[44]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_ADDR_VILL_OR_TOWN", MainHead[45]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_ADDR_MANDAL", MainHead[46]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_ADDR_DISTRICT", MainHead[47]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_ADDR_STATE", MainHead[48]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_ADDR_PIN", Convert.ToInt32(MainHead[49]));
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_ADDR_PHONE_RES", MainHead[50]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_ADDR_PHONE_OFF", MainHead[51]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_VD_OWN_VEHICLE_FLAG", MainHead[52]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_VD_VEHICLE_REG_NUMBER", MainHead[53]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_VD_VEHICLE_MAKE", MainHead[54]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_VD_DL_NUMBER", MainHead[55]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_VD_PASSPORT_NUMBER", MainHead[56]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_VD_PAN_CARD_NUMBER", MainHead[57]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_INTERVIEW_DONE_BY_ECODE", Convert.ToInt32(MainHead[58]));
        //        cmdUpdate.Parameters.AddWithValue("@dtHAMH_INTERVIEW_DATE", Convert.ToDateTime(MainHead[59]));
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_INTERVIEW_REMARKS", MainHead[60]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_EORA_TYPE", MainHead[61]);
        //        cmdUpdate.Parameters.AddWithValue("@ivHAMH_EORA_CODE", Convert.ToInt32(MainHead[62]));
        //        if (iMode == 101)
        //        {
        //            cmdUpdate.Parameters.AddWithValue("@ivHAMH_CREATED_BY", MainHead[63]);
        //            cmdUpdate.Parameters.AddWithValue("@dtHAMH_CREATED_DATE", Convert.ToDateTime(MainHead[64]));
        //        }
        //        else
        //        {
        //            cmdUpdate.Parameters.AddWithValue("@ivHAMH_MODIFIED_BY", MainHead[63]);
        //            cmdUpdate.Parameters.AddWithValue("@dtHAMH_MODIFIED_DATE", Convert.ToDateTime(MainHead[64]));
        //        }
        //        cmdUpdate.Parameters.AddWithValue("@HAED_SSC_NUMBER", MainHead[65]);

        //        cmdUpdate.Parameters.AddWithValue("@ivApplNo", SqlDbType.Int).Direction = ParameterDirection.Output;
        //        cmdUpdate.Parameters.AddWithValue("@ivRetECode", SqlDbType.Int).Direction = ParameterDirection.Output;

        //        cmdUpdate.ExecuteNonQuery();
        //        iApplNo = (int)(cmdUpdate.Parameters["@ivApplNo"].Value);
        //        iECode = (int)(cmdUpdate.Parameters["@ivRetECode"].Value);
        //        if (iMode == 102)
        //            iApplNo = Convert.ToInt32(MainHead[2]);
        //        #endregion

        //        #region "EORA MASTER DETAILS"
        //        SqlCommand cmdEoraMast = new SqlCommand("HR_IU_EORA_MASTER", CN, transaction);
        //        cmdEoraMast.CommandType = CommandType.StoredProcedure;
        //        if (iMode == 101)
        //            cmdEoraMast.Parameters.AddWithValue("@inMode", iMode);
        //        else
        //            cmdEoraMast.Parameters.AddWithValue("@inMode", iMode);
        //        cmdEoraMast.Parameters.AddWithValue("@COMPANY_CODE", EoraMast[12].ToString());
        //        cmdEoraMast.Parameters.AddWithValue("@BRANCH_CODE", EoraMast[0].ToString());
        //        cmdEoraMast.Parameters.AddWithValue("@DEPT_ID", EoraMast[1].ToString());
        //        cmdEoraMast.Parameters.AddWithValue("@ECODE", iECode);
        //        cmdEoraMast.Parameters.AddWithValue("@MEMBER_NAME", EoraMast[4].ToString());
        //        cmdEoraMast.Parameters.AddWithValue("@EORA", EoraMast[5].ToString());
        //        cmdEoraMast.Parameters.AddWithValue("@DESG_ID", Convert.ToInt32(EoraMast[6]));
        //        cmdEoraMast.Parameters.AddWithValue("@DESIG", EoraMast[7].ToString());
        //        cmdEoraMast.Parameters.AddWithValue("@EMP_DOJ", Convert.ToDateTime(EoraMast[8]));
        //        cmdEoraMast.Parameters.AddWithValue("@EMP_DOB", Convert.ToDateTime(EoraMast[9]));
        //        cmdEoraMast.Parameters.AddWithValue("@FATHER_NAME", EoraMast[10].ToString());
        //        cmdEoraMast.Parameters.AddWithValue("@ELEVEL_ID", Convert.ToInt32(EoraMast[11]));
        //        cmdEoraMast.ExecuteNonQuery();
        //        #endregion

        //        #region "FAMILY DETAILS"
        //        if (iMode == 102)
        //        {
        //            SqlCommand cmdFamilyDel = new SqlCommand("HR_IUD_Family", CN, transaction);
        //            cmdFamilyDel.CommandType = CommandType.StoredProcedure;
        //            cmdFamilyDel.Parameters.AddWithValue("@inMode", 102);
        //            cmdFamilyDel.Parameters.AddWithValue("@ivHAFD_COMPANY_CODE", MainHead[0]);
        //            cmdFamilyDel.Parameters.AddWithValue("@ivHAFD_BRANCH_CODE", MainHead[1]);
        //            cmdFamilyDel.Parameters.AddWithValue("@ivHAFD_APPL_NUMBER", iApplNo);
        //            cmdFamilyDel.ExecuteNonQuery();
        //        }
        //        for (int i = 0; i < dtFamily.Rows.Count; i++)
        //        {
        //            SqlCommand cmdFamily = new SqlCommand("HR_IUD_Family", CN, transaction);
        //            cmdFamily.CommandType = CommandType.StoredProcedure;
        //            cmdFamily.Parameters.AddWithValue("@inMode", 101);
        //            cmdFamily.Parameters.AddWithValue("@ivHAFD_COMPANY_CODE", MainHead[0]);
        //            cmdFamily.Parameters.AddWithValue("@ivHAFD_BRANCH_CODE", MainHead[1]);
        //            cmdFamily.Parameters.AddWithValue("@ivHAFD_APPL_NUMBER", iApplNo);
        //            cmdFamily.Parameters.AddWithValue("@ivHAFD_APPL_SL_NUMBER", Convert.ToInt32(Convert.ToInt32(i + 1)));
        //            cmdFamily.Parameters.AddWithValue("@ivHAFD_FAMILY_MEMBER_RELATIONSHIP", dtFamily.Rows[i][1].ToString());
        //            cmdFamily.Parameters.AddWithValue("@ivHAFD_FAMILY_MEMBER_NAME", dtFamily.Rows[i][2].ToString());
        //            cmdFamily.Parameters.AddWithValue("@dtHAFD_FAMILY_MEMBER_DOB", Convert.ToDateTime(dtFamily.Rows[i][3]));
        //            cmdFamily.Parameters.AddWithValue("@ivHAFD_RESIDING_WITH_APPLICANT", dtFamily.Rows[i][4].ToString());
        //            cmdFamily.Parameters.AddWithValue("@ivHAFD_DEPENDING_ON_APPLICANT", dtFamily.Rows[i][5].ToString());
        //            cmdFamily.Parameters.AddWithValue("@ivHAFD_FAMILY_MEMBER_OCCUPATION", dtFamily.Rows[i][6].ToString());
        //            cmdFamily.ExecuteNonQuery();
        //        }
        //        #endregion

        //        #region "EDUCATION DETAILS"
        //        if (iMode == 102)
        //        {
        //            SqlCommand cmdEducationDel = new SqlCommand("HR_IUD_Education", CN, transaction);
        //            cmdEducationDel.CommandType = CommandType.StoredProcedure;
        //            cmdEducationDel.Parameters.AddWithValue("@inMode", 102);
        //            cmdEducationDel.Parameters.AddWithValue("@ivHAED_COMPANY_CODE", MainHead[0]);
        //            cmdEducationDel.Parameters.AddWithValue("@ivHAED_BRANCH_CODE", MainHead[1]);
        //            cmdEducationDel.Parameters.AddWithValue("@ivHAED_APPL_NUMBER", iApplNo);
        //            cmdEducationDel.ExecuteNonQuery();
        //        }
        //        for (int i = 0; i < dtEducation.Rows.Count; i++)
        //        {
        //            SqlCommand cmdEducation = new SqlCommand("HR_IUD_Education", CN, transaction);
        //            cmdEducation.CommandType = CommandType.StoredProcedure;
        //            cmdEducation.Parameters.AddWithValue("@inMode", 101);
        //            cmdEducation.Parameters.AddWithValue("@ivHAED_COMPANY_CODE", MainHead[0]);
        //            cmdEducation.Parameters.AddWithValue("@ivHAED_BRANCH_CODE", MainHead[1]);
        //            cmdEducation.Parameters.AddWithValue("@ivHAED_APPL_NUMBER", iApplNo);
        //            cmdEducation.Parameters.AddWithValue("@ivHAED_APPL_SL_NUMBER", Convert.ToInt32(Convert.ToInt32(i + 1)));
        //            cmdEducation.Parameters.AddWithValue("@ivHAED_EXAMINATION_PASSED", dtEducation.Rows[i][1].ToString());
        //            cmdEducation.Parameters.AddWithValue("@ivHAED_EXAMTYPE", dtEducation.Rows[i][2].ToString());
        //            cmdEducation.Parameters.AddWithValue("@ivHAED_YEAR_OF_PASSING", Convert.ToInt32(dtEducation.Rows[i][3]));
        //            cmdEducation.Parameters.AddWithValue("@ivHAED_INSITUTION_NAME", dtEducation.Rows[i][4].ToString());
        //            cmdEducation.Parameters.AddWithValue("@ivHAED_INSITUTION_LOCATION", dtEducation.Rows[i][5].ToString());
        //            cmdEducation.Parameters.AddWithValue("@ivHAED_SUBJECTS", dtEducation.Rows[i][6].ToString());
        //            cmdEducation.Parameters.AddWithValue("@ivHAED_UNIVERSITY", dtEducation.Rows[i][7].ToString());
        //            cmdEducation.Parameters.AddWithValue("@ivHAED_PERC_MARKS", Convert.ToDouble(dtEducation.Rows[i][8]));
        //            cmdEducation.ExecuteNonQuery();
        //        }
        //        #endregion

        //        #region "SHORT COURSE DETAILS"
        //        if (iMode == 102)
        //        {
        //            SqlCommand cmdShortCourseDel = new SqlCommand("HR_IUD_ShortCourse", CN, transaction);
        //            cmdShortCourseDel.CommandType = CommandType.StoredProcedure;
        //            cmdShortCourseDel.Parameters.AddWithValue("@inMode", 102);
        //            cmdShortCourseDel.Parameters.AddWithValue("@ivHASCD_COMPANY_CODE", MainHead[0]);
        //            cmdShortCourseDel.Parameters.AddWithValue("@ivHASCD_BRANCH_CODE", MainHead[1]);
        //            cmdShortCourseDel.Parameters.AddWithValue("@ivHASCD_APPL_NUMBER", iApplNo);
        //            cmdShortCourseDel.ExecuteNonQuery();
        //        }
        //        for (int i = 0; i < dtSortCourse.Rows.Count; i++)
        //        {
        //            SqlCommand cmdShortCourse = new SqlCommand("HR_IUD_ShortCourse", CN, transaction);
        //            cmdShortCourse.CommandType = CommandType.StoredProcedure;
        //            cmdShortCourse.Parameters.AddWithValue("@inMode", 101);
        //            cmdShortCourse.Parameters.AddWithValue("@ivHASCD_COMPANY_CODE", MainHead[0]);
        //            cmdShortCourse.Parameters.AddWithValue("@ivHASCD_BRANCH_CODE", MainHead[1]);
        //            cmdShortCourse.Parameters.AddWithValue("@ivHASCD_APPL_NUMBER", iApplNo);
        //            cmdShortCourse.Parameters.AddWithValue("@ivHASCD_APPL_SL_NUMBER", Convert.ToInt32(Convert.ToInt32(i + 1)));
        //            cmdShortCourse.Parameters.AddWithValue("@ivHASCD_COURSE_NAME", dtSortCourse.Rows[i][1].ToString());
        //            cmdShortCourse.Parameters.AddWithValue("@ivHASCD_YEAR_OF_PASSING", Convert.ToInt32(dtSortCourse.Rows[i][2]));
        //            cmdShortCourse.Parameters.AddWithValue("@ivHASCD_INSITUTE_ORGANISATION", dtSortCourse.Rows[i][3].ToString());
        //            cmdShortCourse.Parameters.AddWithValue("@ivHASCD_INSITUTE_LOCATION", dtSortCourse.Rows[i][4].ToString());
        //            cmdShortCourse.Parameters.AddWithValue("@ivHASCD_SUBJECTS", dtSortCourse.Rows[i][5].ToString());
        //            cmdShortCourse.Parameters.AddWithValue("@ivHASCD_DURATION", dtSortCourse.Rows[i][6].ToString());
        //            cmdShortCourse.Parameters.AddWithValue("@ivHASCD_PERC_MARKS", Convert.ToDouble(dtSortCourse.Rows[i][7]));
        //            cmdShortCourse.ExecuteNonQuery();
        //        }
        //        #endregion

        //        #region "ECA DETAILS"
        //        if (iMode == 102)
        //        {
        //            SqlCommand cmdECADel = new SqlCommand("HR_IUD_ECAInfo", CN, transaction);
        //            cmdECADel.CommandType = CommandType.StoredProcedure;
        //            cmdECADel.Parameters.AddWithValue("@inMode", 102);
        //            cmdECADel.Parameters.AddWithValue("@ivHAECD_COMPANY_CODE", MainHead[0]);
        //            cmdECADel.Parameters.AddWithValue("@ivHAECD_BRANCH_CODE", MainHead[1]);
        //            cmdECADel.Parameters.AddWithValue("@ivHAECD_APPL_NUMBER", iApplNo);
        //            cmdECADel.ExecuteNonQuery();
        //        }
        //        for (int i = 0; i < dtECA.Rows.Count; i++)
        //        {
        //            SqlCommand cmdECA = new SqlCommand("HR_IUD_ECAInfo", CN, transaction);
        //            cmdECA.CommandType = CommandType.StoredProcedure;
        //            cmdECA.Parameters.AddWithValue("@inMode", 101);
        //            cmdECA.Parameters.AddWithValue("@ivHAECD_COMPANY_CODE", MainHead[0]);
        //            cmdECA.Parameters.AddWithValue("@ivHAECD_BRANCH_CODE", MainHead[1]);
        //            cmdECA.Parameters.AddWithValue("@ivHAECD_APPL_NUMBER", iApplNo);
        //            cmdECA.Parameters.AddWithValue("@ivHAECD_APPL_SL_NUMBER", Convert.ToInt32(Convert.ToInt32(i + 1)));
        //            cmdECA.Parameters.AddWithValue("@ivHAECD_ECA_TYPE", dtECA.Rows[i][1].ToString());
        //            cmdECA.Parameters.AddWithValue("@ivHAECD_REMARKS", dtECA.Rows[i][2].ToString());
        //            cmdECA.ExecuteNonQuery();
        //        }
        //        #endregion

        //        #region "EXPERIENCE DETAILS"
        //        if (iMode == 102)
        //        {
        //            SqlCommand cmdExperienceDel = new SqlCommand("HR_IUD_Experience", CN, transaction);
        //            cmdExperienceDel.CommandType = CommandType.StoredProcedure;
        //            cmdExperienceDel.Parameters.AddWithValue("@inMode", 102);
        //            cmdExperienceDel.Parameters.AddWithValue("@ivHAEXD_COMPANY_CODE", MainHead[0]);
        //            cmdExperienceDel.Parameters.AddWithValue("@ivHAEXD_BRANCH_CODE", MainHead[1]);
        //            cmdExperienceDel.Parameters.AddWithValue("@ivHAEXD_APPL_NUMBER", iApplNo);
        //            cmdExperienceDel.ExecuteNonQuery();
        //        }
        //        for (int i = 0; i < dtExperience.Rows.Count; i++)
        //        {
        //            SqlCommand cmdExperience = new SqlCommand("HR_IUD_Experience", CN, transaction);
        //            cmdExperience.CommandType = CommandType.StoredProcedure;
        //            cmdExperience.Parameters.AddWithValue("@inMode", 101);
        //            cmdExperience.Parameters.AddWithValue("@ivHAEXD_COMPANY_CODE", MainHead[0]);
        //            cmdExperience.Parameters.AddWithValue("@ivHAEXD_BRANCH_CODE", MainHead[1]);
        //            cmdExperience.Parameters.AddWithValue("@ivHAEXD_APPL_NUMBER", iApplNo);
        //            cmdExperience.Parameters.AddWithValue("@ivHAEXD_APPL_SL_NUMBER", Convert.ToInt32(Convert.ToInt32(i + 1)));
        //            cmdExperience.Parameters.AddWithValue("@dtHAEXD_FROM_DATE", Convert.ToDateTime(dtExperience.Rows[i][2]));
        //            cmdExperience.Parameters.AddWithValue("@dtHAEXD_TO_DATE", Convert.ToDateTime(dtExperience.Rows[i][3]));
        //            cmdExperience.Parameters.AddWithValue("@ivHAEXD_ORGANISATION_NAME", dtExperience.Rows[i][1].ToString());
        //            cmdExperience.Parameters.AddWithValue("@ivHAEXD_ORGANISATION_HNO", dtExperience.Rows[i][4].ToString());
        //            cmdExperience.Parameters.AddWithValue("@ivHAEXD_ORGANISATION_LANDMARK", dtExperience.Rows[i][5].ToString());
        //            cmdExperience.Parameters.AddWithValue("@ivHAEXD_ORGANISATION_VILL_OR_TOWN", dtExperience.Rows[i][6].ToString());
        //            cmdExperience.Parameters.AddWithValue("@ivHAEXD_ORGANISATION_MANDAL", dtExperience.Rows[i][7].ToString());
        //            cmdExperience.Parameters.AddWithValue("@ivHAEXD_ORGANISATION_DISTRICT", dtExperience.Rows[i][8].ToString());
        //            cmdExperience.Parameters.AddWithValue("@ivHAEXD_ORGANISATION_STATE", dtExperience.Rows[i][9].ToString());
        //            cmdExperience.Parameters.AddWithValue("@ivHAEXD_ORGANISATION_PIN", Convert.ToInt32(dtExperience.Rows[i][10]));
        //            cmdExperience.Parameters.AddWithValue("@ivHAEXD_JOINING_DESIGNATION", dtExperience.Rows[i][11].ToString());
        //            cmdExperience.Parameters.AddWithValue("@ivHAEXD_LEAVING_DESIGNATION", dtExperience.Rows[i][12].ToString());
        //            cmdExperience.Parameters.AddWithValue("@ivHAEXD_GROSS_SALARY", Convert.ToDouble(dtExperience.Rows[i][13]).ToString("0.00"));
        //            cmdExperience.Parameters.AddWithValue("@ivHAEXD_SALARY_REMARKS", dtExperience.Rows[i][14].ToString());
        //            cmdExperience.Parameters.AddWithValue("@ivHAEXD_REASONS_FOR_LEAVING", dtExperience.Rows[i][15].ToString());
        //            cmdExperience.ExecuteNonQuery();
        //        }
        //        #endregion

        //        #region "INDUCTION_TRAINING"
        //        if (Ind_Training != "")
        //        {
        //            string[] IndTrVal = Ind_Training.Split(',');
        //            SqlCommand cmdDocumentsDel = new SqlCommand("HR_IU_INDUCTION_TRAINING", CN, transaction);
        //            cmdDocumentsDel.CommandType = CommandType.StoredProcedure;
        //            cmdDocumentsDel.Parameters.AddWithValue("@inMode", iMode);
        //            cmdDocumentsDel.Parameters.AddWithValue("@ECODE", iECode);
        //            cmdDocumentsDel.Parameters.AddWithValue("@TRAINING", IndTrVal[0].ToString());
        //            cmdDocumentsDel.Parameters.AddWithValue("@TRAINER_CODE", Convert.ToInt32(IndTrVal[1].ToString()));
        //            cmdDocumentsDel.Parameters.AddWithValue("@TRAINING_FROM", Convert.ToDateTime(IndTrVal[2]));
        //            cmdDocumentsDel.Parameters.AddWithValue("@TRAINING_TO", Convert.ToDateTime(IndTrVal[3]));
        //            cmdDocumentsDel.ExecuteNonQuery();
        //        }
        //        #endregion

        //        #region "REFERENCE DETAILS"
        //        if (iMode == 102)
        //        {
        //            SqlCommand cmdRefDel = new SqlCommand("HR_IUD_Reference", CN, transaction);
        //            cmdRefDel.CommandType = CommandType.StoredProcedure;
        //            cmdRefDel.Parameters.AddWithValue("@inMode", 102);
        //            cmdRefDel.Parameters.AddWithValue("@ivHARD_COMPANY_CODE", MainHead[0]);
        //            cmdRefDel.Parameters.AddWithValue("@ivHARD_BRANCH_CODE", MainHead[1]);
        //            cmdRefDel.Parameters.AddWithValue("@ivHARD_APPL_NUMBER", iApplNo);
        //            cmdRefDel.ExecuteNonQuery();
        //        }
        //        for (int i = 0; i < dtReference.Rows.Count; i++)
        //        {
        //            SqlCommand cmdRef = new SqlCommand("HR_IUD_Reference", CN, transaction);
        //            cmdRef.CommandType = CommandType.StoredProcedure;
        //            cmdRef.Parameters.AddWithValue("@inMode", 101);
        //            cmdRef.Parameters.AddWithValue("@ivHARD_COMPANY_CODE", MainHead[0]);
        //            cmdRef.Parameters.AddWithValue("@ivHARD_BRANCH_CODE", MainHead[1]);
        //            cmdRef.Parameters.AddWithValue("@ivHARD_APPL_NUMBER", iApplNo);
        //            cmdRef.Parameters.AddWithValue("@ivHARD_APPL_SL_NUMBER", Convert.ToInt32(Convert.ToInt32(i + 1)));
        //            cmdRef.Parameters.AddWithValue("@ivHARD_REF_NAME", dtReference.Rows[i][1].ToString());
        //            cmdRef.Parameters.AddWithValue("@ivHARD_REF_OCCUPATION", dtReference.Rows[i][2].ToString());
        //            cmdRef.Parameters.AddWithValue("@ivHARD_REF_TELEPHONE", dtReference.Rows[i][3].ToString());
        //            cmdRef.Parameters.AddWithValue("@ivHARD_ADDR_HNO", dtReference.Rows[i][4].ToString());
        //            cmdRef.Parameters.AddWithValue("@ivHARD_ADDR_LANDMARK", dtReference.Rows[i][5].ToString());
        //            cmdRef.Parameters.AddWithValue("@ivHARD_ADDR_VILL_OR_TOWN", dtReference.Rows[i][6].ToString());
        //            cmdRef.Parameters.AddWithValue("@ivHARD_ADDR_MANDAL", dtReference.Rows[i][7].ToString());
        //            cmdRef.Parameters.AddWithValue("@ivHARD_ADDR_DISTRICT", dtReference.Rows[i][8].ToString());
        //            cmdRef.Parameters.AddWithValue("@ivHARD_ADDR_STATE", dtReference.Rows[i][9].ToString());
        //            if (dtReference.Rows[i][10].ToString() == "")
        //                dtReference.Rows[i][10] = 0;
        //            cmdRef.Parameters.AddWithValue("@ivHARD_ADDR_PIN", Convert.ToInt64(dtReference.Rows[i][10]));
        //            cmdRef.Parameters.AddWithValue("@ivHARD_ADDR_PHONE", dtReference.Rows[i][11].ToString());
        //            cmdRef.ExecuteNonQuery();
        //        }
        //        #endregion

        //        #region "DOCUMENT DETAILS"
        //        if (iMode == 102)
        //        {
        //            SqlCommand cmdDocumentsDel = new SqlCommand("HR_IUD_Documents", CN, transaction);
        //            cmdDocumentsDel.CommandType = CommandType.StoredProcedure;
        //            cmdDocumentsDel.Parameters.AddWithValue("@inMode", 102);
        //            cmdDocumentsDel.Parameters.AddWithValue("@ivHAAD_COMPANY_CODE", MainHead[0]);
        //            cmdDocumentsDel.Parameters.AddWithValue("@ivHAAD_BRANCH_CODE", MainHead[1]);
        //            cmdDocumentsDel.Parameters.AddWithValue("@ivHAAD_APPL_NUMBER", iApplNo);
        //            cmdDocumentsDel.ExecuteNonQuery();
        //        }
        //        for (int i = 0; i < dtDocuments.Rows.Count; i++)
        //        {
        //            SqlCommand cmdDocuments = new SqlCommand("HR_IUD_Documents", CN, transaction);
        //            cmdDocuments.CommandType = CommandType.StoredProcedure;
        //            cmdDocuments.Parameters.AddWithValue("@inMode", 101);
        //            cmdDocuments.Parameters.AddWithValue("@ivHAAD_COMPANY_CODE", MainHead[0]);
        //            cmdDocuments.Parameters.AddWithValue("@ivHAAD_BRANCH_CODE", MainHead[1]);
        //            cmdDocuments.Parameters.AddWithValue("@ivHAAD_APPL_NUMBER", iApplNo);
        //            cmdDocuments.Parameters.AddWithValue("@ivHAAD_APPL_SL_NUMBER", Convert.ToInt32(Convert.ToInt32(i + 1)));
        //            cmdDocuments.Parameters.AddWithValue("@ivHAAD_CHECK_LIST_HEAD", dtDocuments.Rows[i][1].ToString());
        //            int ibalFlg = 0;
        //            if (dtDocuments.Rows[i][2].ToString() == "True")
        //                ibalFlg = 1;
        //            cmdDocuments.Parameters.AddWithValue("@ivHAAD_CHECK_LIST_RECD_BR_FLAG", ibalFlg);
        //            cmdDocuments.Parameters.AddWithValue("@dtHAAD_CHECK_LIST_RECD_BR_DATE", Convert.ToDateTime(dtDocuments.Rows[i][3]));
        //            cmdDocuments.Parameters.AddWithValue("@ivHAAD_CHECK_LIST_BR_REMARKS", dtDocuments.Rows[i][4].ToString());
        //            cmdDocuments.ExecuteNonQuery();
        //        }
        //        #endregion


        //        transaction.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        transaction.Rollback();
        //        //if (ex.ToString().Contains("duplicate key"))
        //        //    return "Please cannot inserted the duplicate records.";
        //        //else
        //        return ex.ToString();
        //    }
        //    finally
        //    {
        //        CN.Close();
        //        objSecurity = null;
        //    }
        //    if (iMode == 102)
        //        return "Update , " + iECode + "," + iApplNo;
        //    else
        //        return "Saved , " + iECode + "," + iApplNo;
        //}

        public string HRMainheadSave(int iMode, object[] MainHead, string[] EoraMast, DataTable dtFamily, DataTable dtEducation, DataTable dtSortCourse, DataTable dtECA, DataTable dtExperience, DataTable dtReference, DataTable dtDocuments, string Ind_Training, DataTable dtLanguages)
        {
            int iApplNo = 0;
            int iECode = 0;
            objSecurity = new Security();
            objSQLdb = new SQLDB();
            string sqlText = "";
            SqlConnection CN = new SqlConnection(objSecurity.GetDecodeString(sConnVal));
            //SqlTransaction transaction;
            CN.Open();
            if (iMode == 101)
            {
                //transaction = CN.BeginTransaction();
                try
                {
                    #region "MAIN HEAD TABLE DATA"
                    objSecurity = new Security();
                    SqlCommand cmdUpdate = new SqlCommand("HR_IU_MainHead1", CN);
                    cmdUpdate.CommandType = CommandType.StoredProcedure;
                    cmdUpdate.Parameters.AddWithValue("@inMode", iMode);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_COMPANY_CODE", MainHead[0].ToString());
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_BRANCH_CODE", MainHead[1].ToString());
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_APPL_NUMBER", Convert.ToInt32(MainHead[2]));
                    cmdUpdate.Parameters.AddWithValue("@dtHAMH_APPL_DATE", Convert.ToDateTime(Convert.ToDateTime(MainHead[3]).ToString("dd/MMM/yyyy")));
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_RECRUITMENT_SOURCE_CODE", MainHead[4].ToString());
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_RECRUITMENT_SOURCE_DELT_CODE", MainHead[5].ToString());
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_RECRUITMENT_SOURCE_ECODE", MainHead[6]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_NAME", MainHead[7].ToString().Replace(" ",""));
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_FORH", MainHead[8].ToString().Replace(" ", ""));
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_FORH_NAME", MainHead[9]);
                    cmdUpdate.Parameters.AddWithValue("@dtHAMH_DOJ", Convert.ToDateTime(Convert.ToDateTime(MainHead[10]).ToString("dd/MMM/yyyy")));
                    cmdUpdate.Parameters.AddWithValue("@dtHAMH_DOB", Convert.ToDateTime(Convert.ToDateTime(MainHead[11]).ToString("dd/MMM/yyyy")));
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_NATIVE_PLACE", MainHead[12]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_SEX", MainHead[13]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_NAIONALITY", MainHead[14]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_RELIGION", MainHead[15]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_MATRITAL_STATUS", MainHead[16]);
                    cmdUpdate.Parameters.AddWithValue("@dtHAMH_MARRIAGE_DATE", Convert.ToDateTime(Convert.ToDateTime(MainHead[17]).ToString("dd/MMM/yyyy")));
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_NOMINIEE_NAME", MainHead[18]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_NOMINIEE_RELATION", MainHead[19]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_PD_HEIGHT", MainHead[20].ToString() == "" ? 0 : Convert.ToDouble(MainHead[20]));
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_PD_WEIGHT", MainHead[21].ToString() == "" ? 0 : Convert.ToDouble(MainHead[21]));
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_PD_BLOOD_GROUP_CODE", MainHead[22].ToString() == "SELECT" ? "" : MainHead[22]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_PD_PHYSICAL_DISABILITY", MainHead[23]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_PD_PROLONGED_ILLNESS", MainHead[24]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_PD_PROLONGED_ILLNESS_PERIOD", MainHead[25]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PRES_ADDR_HNO", MainHead[26]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PRES_ADDR_LANDMARK", MainHead[27]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PRES_ADDR_VILL_OR_TOWN", MainHead[28]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PRES_ADDR_MANDAL", MainHead[29]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PRES_ADDR_DISTRICT", MainHead[30]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PRES_ADDR_STATE", MainHead[31]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PRES_ADDR_PIN", Convert.ToInt32(MainHead[32]));
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PRES_ADDR_PHONE", MainHead[33]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PERM_ADDR_HNO", MainHead[34]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PERM_ADDR_LANDMARK", MainHead[35]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PERM_ADDR_VILL_OR_TOWN", MainHead[36]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PERM_ADDR_MANDAL", MainHead[37]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PERM_ADDR_DISTRICT", MainHead[38]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PERM_ADDR_STATE", MainHead[39]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PERM_ADDR_PIN", Convert.ToInt32(MainHead[40]));
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PERM_ADDR_PHONE", MainHead[41]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_NAME", MainHead[42]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_ADDR_HNO", MainHead[43]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_ADDR_LANDMARK", MainHead[44]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_ADDR_VILL_OR_TOWN", MainHead[45]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_ADDR_MANDAL", MainHead[46]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_ADDR_DISTRICT", MainHead[47]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_ADDR_STATE", MainHead[48]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_ADDR_PIN", Convert.ToInt32(MainHead[49]));
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_ADDR_PHONE_RES", MainHead[50]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_ADDR_PHONE_OFF", MainHead[51]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_VD_OWN_VEHICLE_FLAG", MainHead[52]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_VD_VEHICLE_REG_NUMBER", MainHead[53]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_VD_VEHICLE_MAKE", MainHead[54]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_VD_DL_NUMBER", MainHead[55]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_VD_PASSPORT_NUMBER", MainHead[56]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_VD_PAN_CARD_NUMBER", MainHead[57]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_INTERVIEW_DONE_BY_ECODE", Convert.ToInt32(MainHead[58]));
                    cmdUpdate.Parameters.AddWithValue("@dtHAMH_INTERVIEW_DATE", Convert.ToDateTime(Convert.ToDateTime(MainHead[59]).ToString("dd/MMM/yyyy")));
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_INTERVIEW_REMARKS", MainHead[60]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_EORA_TYPE", MainHead[61]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_EORA_CODE", Convert.ToInt32(MainHead[62]));
                    if (iMode == 101)
                    {
                        cmdUpdate.Parameters.AddWithValue("@ivHAMH_CREATED_BY", MainHead[63]);
                        cmdUpdate.Parameters.AddWithValue("@dtHAMH_CREATED_DATE", Convert.ToDateTime(Convert.ToDateTime(MainHead[64]).ToString("dd/MMM/yyyy")));
                    }
                    else
                    {
                        cmdUpdate.Parameters.AddWithValue("@ivHAMH_MODIFIED_BY", MainHead[63]);
                        cmdUpdate.Parameters.AddWithValue("@dtHAMH_MODIFIED_DATE", Convert.ToDateTime(Convert.ToDateTime(MainHead[64]).ToString("dd/MMM/yyyy")));
                    }
                    cmdUpdate.Parameters.AddWithValue("@HAED_SSC_NUMBER", MainHead[65]);

                    cmdUpdate.Parameters.AddWithValue("@ivApplNo", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmdUpdate.Parameters.AddWithValue("@ivRetECode", SqlDbType.Int).Direction = ParameterDirection.Output;

                    cmdUpdate.ExecuteNonQuery();
                    iApplNo = (int)(cmdUpdate.Parameters["@ivApplNo"].Value);
                    iECode = (int)(cmdUpdate.Parameters["@ivRetECode"].Value);
                    if (iApplNo == 0 || iECode == 0)
                    {                        
                        return "ERROR";
                    }
                    if (iMode == 102)
                        iApplNo = Convert.ToInt32(MainHead[2]);
                    #endregion

                    #region "EORA MASTER DETAILS"
                    if (iMode == 101)
                    {
                        sqlText += "INSERT INTO EORA_MASTER (BRANCH_CODE,DEPT_ID,ECODE,MEMBER_NAME,EORA,HRIS_EMP_NAME,DESG_ID,DESIG,HRIS_DESIG,EMP_DOJ,EMP_DOB,FATHER_NAME,ELEVEL_ID,COMPANY_CODE) " +
                                    "VALUES('" + EoraMast[0].ToString() + "','" + EoraMast[1].ToString() + "','" + iECode + "','" + EoraMast[4].ToString() + "','" + EoraMast[5].ToString() +
                                    "','" + EoraMast[4].ToString() + "','" + Convert.ToInt32(EoraMast[6]) + "','" + EoraMast[7].ToString() + "','" + EoraMast[7].ToString() + "','" + Convert.ToDateTime(EoraMast[8]).ToString("dd/MMM/yyyy") +
                                    "','" + Convert.ToDateTime(EoraMast[9]).ToString("dd/MMM/yyyy") + "','" + EoraMast[10].ToString() + "','" + Convert.ToInt32(EoraMast[11]) + "','" + EoraMast[12].ToString() + "'); ";
                        //if (sqlText.Length > 10)
                        //{
                        //    SqlCommand cmdEoraMas = new SqlCommand(sqlText, CN, transaction);
                        //    cmdEoraMas.CommandType = CommandType.Text;
                        //    cmdEoraMas.ExecuteNonQuery();
                        //}
                    }
                    else
                    {
                        SqlCommand cmdEoraMast = new SqlCommand("HR_IU_EORA_MASTER", CN);
                        cmdEoraMast.CommandType = CommandType.StoredProcedure;
                        cmdEoraMast.Parameters.AddWithValue("@inMode", iMode);
                        cmdEoraMast.Parameters.AddWithValue("@COMPANY_CODE", EoraMast[12].ToString());
                        cmdEoraMast.Parameters.AddWithValue("@BRANCH_CODE", EoraMast[0].ToString());
                        cmdEoraMast.Parameters.AddWithValue("@DEPT_ID", EoraMast[1].ToString());
                        cmdEoraMast.Parameters.AddWithValue("@ECODE", iECode);
                        cmdEoraMast.Parameters.AddWithValue("@MEMBER_NAME", EoraMast[4].ToString());
                        cmdEoraMast.Parameters.AddWithValue("@EORA", EoraMast[5].ToString());
                        cmdEoraMast.Parameters.AddWithValue("@DESG_ID", Convert.ToInt32(EoraMast[6]));
                        cmdEoraMast.Parameters.AddWithValue("@DESIG", EoraMast[7].ToString());
                        cmdEoraMast.Parameters.AddWithValue("@EMP_DOJ", Convert.ToDateTime(Convert.ToDateTime(EoraMast[8]).ToString("dd/MMM/yyyy")));
                        cmdEoraMast.Parameters.AddWithValue("@EMP_DOB", Convert.ToDateTime(Convert.ToDateTime(EoraMast[9]).ToString("dd/MMM/yyyy")));
                        cmdEoraMast.Parameters.AddWithValue("@FATHER_NAME", EoraMast[10].ToString());
                        cmdEoraMast.Parameters.AddWithValue("@ELEVEL_ID", Convert.ToInt32(EoraMast[11]));
                        cmdEoraMast.ExecuteNonQuery();
                    }
                    #endregion

                    #region "FAMILY DETAILS"
                    //sqlText = "";
                    if (dtFamily.Rows.Count > 0)
                    {
                        sqlText += "DELETE FROM HR_APPL_FAMILY_DETL WHERE HAFD_APPL_NUMBER='" + iApplNo + "'; ";
                        for (int i = 0; i < dtFamily.Rows.Count; i++)
                        {
                            sqlText += "INSERT INTO HR_APPL_FAMILY_DETL(HAFD_COMPANY_CODE,HAFD_BRANCH_CODE,HAFD_APPL_NUMBER,HAFD_APPL_SL_NUMBER,HAFD_FAMILY_MEMBER_RELATIONSHIP," +
                                            "HAFD_FAMILY_MEMBER_NAME,HAFD_FAMILY_MEMBER_DOB,HAFD_RESIDING_WITH_APPLICANT,HAFD_DEPENDING_ON_APPLICANT,HAFD_FAMILY_MEMBER_OCCUPATION) " +
                                        "VALUES('" + MainHead[0] + "','" + MainHead[1] + "','" + iApplNo + "','" + Convert.ToInt32(Convert.ToInt32(i + 1)) + "','" + dtFamily.Rows[i][1].ToString() + "'," +
                                            "'" + dtFamily.Rows[i][2].ToString() + "','" + Convert.ToDateTime(dtFamily.Rows[i][3]).ToString("dd/MMM/yyyy") + "','" + dtFamily.Rows[i][4].ToString() + "','" + dtFamily.Rows[i][5].ToString() + "','" + dtFamily.Rows[i][6].ToString() + "'); ";

                        }
                        //if (sqlText.Length > 10)
                        //{
                        //    SqlCommand cmdFamDetl = new SqlCommand(sqlText, CN, transaction);
                        //    cmdFamDetl.CommandType = CommandType.Text;
                        //    cmdFamDetl.ExecuteNonQuery();
                        //}
                    }
                    #endregion

                    #region "EDUCATION DETAILS"
                    //sqlText = "";
                    if (dtEducation.Rows.Count > 0)
                    {
                        sqlText += "DELETE FROM HR_APPL_EDU_DETL WHERE HAED_APPL_NUMBER='" + iApplNo + "'; ";
                        for (int i = 0; i < dtEducation.Rows.Count; i++)
                        {
                            sqlText += "INSERT INTO HR_APPL_EDU_DETL(HAED_COMPANY_CODE,HAED_BRANCH_CODE,HAED_APPL_NUMBER,HAED_APPL_SL_NUMBER,HAED_EXAMINATION_PASSED," +
                                          "HAED_EXAMTYPE,HAED_YEAR_OF_PASSING,HAED_INSITUTION_NAME,HAED_INSITUTION_LOCATION,HAED_SUBJECTS,HAED_UNIVERSITY,HAED_PERC_MARKS) " +
                                      "VALUES('" + MainHead[0] + "','" + MainHead[1] + "','" + iApplNo + "','" + Convert.ToInt32(Convert.ToInt32(i + 1)) + "','" + dtEducation.Rows[i][1].ToString() + "'," +
                                          "'" + dtEducation.Rows[i][2].ToString() + "','" + dtEducation.Rows[i][3].ToString() + "','" + dtEducation.Rows[i][4].ToString() + "','" + dtEducation.Rows[i][5].ToString() + 
                                          "','" + dtEducation.Rows[i][6].ToString() + "','" + dtEducation.Rows[i][7].ToString() + "','" + Convert.ToDouble(dtEducation.Rows[i][8]) + "'); ";
                        }
                        //if (sqlText.Length > 10)
                        //{
                        //    SqlCommand cmdEduDetl = new SqlCommand(sqlText, CN, transaction);
                        //    cmdEduDetl.CommandType = CommandType.Text;
                        //    cmdEduDetl.ExecuteNonQuery();
                        //}
                    }
                    #endregion

                    #region "SHORT COURSE DETAILS"
                    //sqlText = "";
                    if (dtSortCourse.Rows.Count > 0)
                    {
                        sqlText += "DELETE FROM HR_APPL_SHORT_COURSE_DETL WHERE HASCD_APPL_NUMBER='" + iApplNo + "'; ";
                        for (int i = 0; i < dtSortCourse.Rows.Count; i++)
                        {

                            sqlText += "INSERT INTO HR_APPL_SHORT_COURSE_DETL(HASCD_COMPANY_CODE,HASCD_BRANCH_CODE,HASCD_APPL_NUMBER,HASCD_APPL_SL_NUMBER,HASCD_COURSE_NAME," +
                                          "HASCD_YEAR_OF_PASSING,HASCD_INSITUTE_ORGANISATION,HASCD_INSITUTE_LOCATION,HASCD_SUBJECTS,HASCD_DURATION,HASCD_PERC_MARKS) " +
                                     "VALUES('" + MainHead[0] + "','" + MainHead[1] + "','" + iApplNo + "','" + Convert.ToInt32(Convert.ToInt32(i + 1)) + "','" + dtSortCourse.Rows[i][1].ToString() + "','" + Convert.ToInt32(dtSortCourse.Rows[i][2]) + "'," +
                                          "'" + dtSortCourse.Rows[i][3].ToString() + "','" + dtSortCourse.Rows[i][4].ToString() + "','" + dtSortCourse.Rows[i][5].ToString() + "','" + dtSortCourse.Rows[i][6].ToString() + "','" + Convert.ToDouble(dtSortCourse.Rows[i][7]) + "'); ";
                        }
                        //if (sqlText.Length > 10)
                        //{
                        //    SqlCommand cmdShortCourseDetl = new SqlCommand(sqlText, CN, transaction);
                        //    cmdShortCourseDetl.CommandType = CommandType.Text;
                        //    cmdShortCourseDetl.ExecuteNonQuery();
                        //}
                    }
                    #endregion

                    #region "ECA DETAILS"
                    //sqlText = "";
                    if (dtECA.Rows.Count > 0)
                    {
                        sqlText += "DELETE FROM HR_APPL_ECA_DETL WHERE HAECD_APPL_NUMBER='" + iApplNo + "'; ";
                        for (int i = 0; i < dtECA.Rows.Count; i++)
                        {
                            sqlText += "INSERT INTO HR_APPL_ECA_DETL(HAECD_COMPANY_CODE,HAECD_BRANCH_CODE,HAECD_APPL_NUMBER,HAECD_APPL_SL_NUMBER,HAECD_ECA_TYPE,HAECD_REMARKS) " +
                                     "VALUES('" + MainHead[0] + "','" + MainHead[1] + "','" + iApplNo + "','" + Convert.ToInt32(Convert.ToInt32(i + 1)) + "','" + dtECA.Rows[i][1].ToString() + "','" + dtECA.Rows[i][2].ToString() + "'); ";
                        }
                        //if (sqlText.Length > 10)
                        //{
                        //    SqlCommand cmdEduQualDetl = new SqlCommand(sqlText, CN, transaction);
                        //    cmdEduQualDetl.CommandType = CommandType.Text;
                        //    cmdEduQualDetl.ExecuteNonQuery();
                        //}
                    }
                    #endregion

                    #region "EXPERIENCE DETAILS"
                    //sqlText = "";
                    if (dtExperience.Rows.Count > 0)
                    {
                        sqlText += "DELETE FROM HR_APPL_EXP_DETL WHERE HAEXD_APPL_NUMBER='" + iApplNo + "'; ";
                        for (int i = 0; i < dtExperience.Rows.Count; i++)
                        {
                            sqlText += "INSERT INTO HR_APPL_EXP_DETL(HAEXD_COMPANY_CODE,HAEXD_BRANCH_CODE,HAEXD_APPL_NUMBER,HAEXD_APPL_SL_NUMBER,HAEXD_FROM_DATE," +
                                            "HAEXD_TO_DATE,HAEXD_ORGANISATION_NAME,HAEXD_ORGANISATION_HNO,HAEXD_ORGANISATION_LANDMARK,HAEXD_ORGANISATION_VILL_OR_TOWN," +
                                            "HAEXD_ORGANISATION_MANDAL,HAEXD_ORGANISATION_DISTRICT,HAEXD_ORGANISATION_STATE,HAEXD_ORGANISATION_PIN,HAEXD_JOINING_DESIGNATION," +
                                            "HAEXD_LEAVING_DESIGNATION,HAEXD_GROSS_SALARY,HAEXD_SALARY_REMARKS,HAEXD_REASONS_FOR_LEAVING) " +
                                        "VALUES('" + MainHead[0] + "','" + MainHead[1] + "','" + iApplNo + "','" + Convert.ToInt32(Convert.ToInt32(i + 1)) + "','" + Convert.ToDateTime(dtExperience.Rows[i][2]).ToString("dd/MMM/yyyy") + "','" + Convert.ToDateTime(dtExperience.Rows[i][3]).ToString("dd/MMM/yyyy") + "'," +
                                            "'" + dtExperience.Rows[i][1].ToString() + "','" + dtExperience.Rows[i][4].ToString() + "','" + dtExperience.Rows[i][5].ToString() + "','" + dtExperience.Rows[i][6].ToString() + "'," +
                                            "'" + dtExperience.Rows[i][7].ToString() + "','" + dtExperience.Rows[i][8].ToString() + "','" + dtExperience.Rows[i][9].ToString() + "','" + Convert.ToInt32(dtExperience.Rows[i][10]) + "','" + dtExperience.Rows[i][11].ToString() + "'," +
                                            "'" + dtExperience.Rows[i][12].ToString() + "','" + Convert.ToDouble(dtExperience.Rows[i][13]).ToString("0.00") + "','" + dtExperience.Rows[i][14].ToString() + "','" + dtExperience.Rows[i][15].ToString() + "'); ";
                        }
                        ////if (sqlText.Length > 10)
                        ////{
                        ////    SqlCommand cmdExpDetl = new SqlCommand(sqlText, CN, transaction);
                        ////    cmdExpDetl.CommandType = CommandType.Text;
                        ////    cmdExpDetl.ExecuteNonQuery();
                        ////}
                    }
                    #endregion

                    #region "INDUCTION_TRAINING"
                    //sqlText = "";
                    if (Ind_Training != "")
                    {
                        sqlText += "DELETE FROM HR_APPL_INDUCTION_TRAINING WHERE HAIT_EORA_CODE='" + iECode + "'; ";
                        string[] IndTrVal = Ind_Training.Split(',');
                        sqlText += "INSERT INTO HR_APPL_INDUCTION_TRAINING (HAIT_EORA_CODE,HAIT_TRAINING_STATUS,HAIT_TRAINER_ECODE,HAIT_TRAINING_FROM,HAIT_TRAINING_TO) " +
                                        "VALUES('" + iECode + "','" + IndTrVal[0].ToString() + "','" + Convert.ToInt32(IndTrVal[1].ToString()) + "','" + Convert.ToDateTime(IndTrVal[2]).ToString("dd/MMM/yyyy") + "','" + Convert.ToDateTime(Convert.ToDateTime(IndTrVal[3]).ToString("dd/MMM/yyyy")).ToString("dd/MMM/yyyy") + "'); ";
                    }
                    //if (sqlText.Length > 10)
                    //{
                    //    SqlCommand cmdIndTraingDetl = new SqlCommand(sqlText, CN, transaction);
                    //    cmdIndTraingDetl.CommandType = CommandType.Text;
                    //    cmdIndTraingDetl.ExecuteNonQuery();
                    //}
                    #endregion

                    #region "REFERENCE DETAILS"
                    //sqlText = "";
                    if (dtReference.Rows.Count > 0)
                    {
                        sqlText += "DELETE FROM HR_APPL_REF_DETL WHERE HARD_APPL_NUMBER='" + iApplNo + "'; ";
                        for (int i = 0; i < dtReference.Rows.Count; i++)
                        {
                            if (dtReference.Rows[i][10].ToString() == "")
                                dtReference.Rows[i][10] = 0;
                            sqlText += "INSERT INTO HR_APPL_REF_DETL(HARD_COMPANY_CODE,HARD_BRANCH_CODE,HARD_APPL_NUMBER,HARD_APPL_SL_NUMBER,HARD_REF_NAME," +
                                            "HARD_REF_OCCUPATION,HARD_REF_TELEPHONE,HARD_ADDR_HNO,HARD_ADDR_LANDMARK,HARD_ADDR_VILL_OR_TOWN,HARD_ADDR_MANDAL," +
                                            "HARD_ADDR_DISTRICT,HARD_ADDR_STATE,HARD_ADDR_PIN,HARD_ADDR_PHONE) " +
                                        "VALUES('" + MainHead[0] + "','" + MainHead[1] + "','" + iApplNo + "','" + Convert.ToInt32(Convert.ToInt32(i + 1)) + "','" + dtReference.Rows[i][1].ToString() + "','" + dtReference.Rows[i][2].ToString() + "'," +
                                            "'" + dtReference.Rows[i][3].ToString() + "','" + dtReference.Rows[i][4].ToString() + "','" + dtReference.Rows[i][5].ToString() + "','" + dtReference.Rows[i][6].ToString() + "','" + dtReference.Rows[i][7].ToString() + "','" + dtReference.Rows[i][8].ToString() + "'," +
                                            "'" + dtReference.Rows[i][9].ToString() + "','" + Convert.ToInt64(dtReference.Rows[i][10]) + "','" + dtReference.Rows[i][11].ToString() + "'); ";
                        }
                        //if (sqlText.Length > 10)
                        //{
                        //    SqlCommand cmdRefDetl = new SqlCommand(sqlText, CN, transaction);
                        //    cmdRefDetl.CommandType = CommandType.Text;
                        //    cmdRefDetl.ExecuteNonQuery();
                        //}
                    }
                    #endregion

                    #region "DOCUMENT DETAILS"
                    //sqlText = "";
                    if (dtDocuments.Rows.Count > 0)
                    {
                        sqlText += "DELETE FROM HR_APPL_APPROVAL_DETL WHERE HAAD_APPL_NUMBER='" + iApplNo + "'; ";
                        for (int i = 0; i < dtDocuments.Rows.Count; i++)
                        {
                            if (dtDocuments.Rows[i][0].ToString() == "True")
                            {
                                sqlText += "INSERT INTO HR_APPL_APPROVAL_DETL(HAAD_COMPANY_CODE,HAAD_BRANCH_CODE,HAAD_APPL_NUMBER,HAAD_APPL_SL_NUMBER,HAAD_CHECK_LIST_HEAD," +
                                                "HAAD_CHECK_LIST_RECD_BR_FLAG,HAAD_CHECK_LIST_RECD_BR_DATE,HAAD_CHECK_LIST_BR_REMARKS,HAAD_CHECK_LIST_RECD_HO_FLAG,HAAD_CHECK_LIST_RECD_HO_DATE,HAAD_CHECK_LIST_HO_REMARKS) " +
                                            "VALUES('" + MainHead[0] + "','" + MainHead[1] + "','" + iApplNo + "','" + Convert.ToInt32(Convert.ToInt32(i + 1)) + "','" + dtDocuments.Rows[i][1].ToString() + "','1'," +
                                                "'" + Convert.ToDateTime(dtDocuments.Rows[i][2]).ToString("dd/MMM/yyyy") + "','" + dtDocuments.Rows[i][3].ToString() + "','1','" + Convert.ToDateTime("1/1/1900").ToString("dd/MMM/yyyy") + "','" + string.Empty + "'); ";
                            }
                            else
                            {
                                sqlText += "INSERT INTO HR_APPL_APPROVAL_DETL(HAAD_COMPANY_CODE,HAAD_BRANCH_CODE,HAAD_APPL_NUMBER,HAAD_APPL_SL_NUMBER,HAAD_CHECK_LIST_HEAD," +
                                                "HAAD_CHECK_LIST_RECD_BR_FLAG,HAAD_CHECK_LIST_BR_REMARKS,HAAD_CHECK_LIST_RECD_HO_FLAG,HAAD_CHECK_LIST_HO_REMARKS) " +
                                            "VALUES('" + MainHead[0] + "','" + MainHead[1] + "','" + iApplNo + "','" + Convert.ToInt32(Convert.ToInt32(i + 1)) + "','" + dtDocuments.Rows[i][1].ToString() + "','0'," +
                                                "'" + dtDocuments.Rows[i][3].ToString() + "','0','" + string.Empty + "'); ";
                            }
                        }
                        //if (sqlText.Length > 10)
                        //{
                        //    SqlCommand cmdDocDetl = new SqlCommand(sqlText, CN, transaction);
                        //    cmdDocDetl.CommandType = CommandType.Text;
                        //    cmdDocDetl.ExecuteNonQuery();
                        //}
                    }
                    #endregion
                    if (sqlText.Length > 10)
                    {
                        SqlCommand sqlCmd = new SqlCommand(sqlText, CN);
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.ExecuteNonQuery();
                    }
                    //transaction.Commit();
                }
                catch (Exception ex)
                {
                    //transaction.Rollback();
                    return ex.ToString();
                }
                finally
                {
                    CN.Close();
                    objSecurity = null;
                }
            }
            else
            {
                try
                {
                    #region "MAIN HEAD TABLE DATA"
                    objSecurity = new Security();
                    SqlCommand cmdUpdate = new SqlCommand("HR_IU_MainHead1", CN);
                    cmdUpdate.CommandType = CommandType.StoredProcedure;
                    cmdUpdate.Parameters.AddWithValue("@inMode", iMode);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_COMPANY_CODE", MainHead[0].ToString());
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_BRANCH_CODE", MainHead[1].ToString());
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_APPL_NUMBER", Convert.ToInt32(MainHead[2]));
                    cmdUpdate.Parameters.AddWithValue("@dtHAMH_APPL_DATE", Convert.ToDateTime(Convert.ToDateTime(MainHead[3]).ToString("dd/MMM/yyyy")));
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_RECRUITMENT_SOURCE_CODE", MainHead[4].ToString());
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_RECRUITMENT_SOURCE_DELT_CODE", MainHead[5].ToString());
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_RECRUITMENT_SOURCE_ECODE", MainHead[6]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_NAME", MainHead[7]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_FORH", MainHead[8]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_FORH_NAME", MainHead[9]);
                    cmdUpdate.Parameters.AddWithValue("@dtHAMH_DOJ", Convert.ToDateTime(Convert.ToDateTime(MainHead[10]).ToString("dd/MMM/yyyy")));
                    cmdUpdate.Parameters.AddWithValue("@dtHAMH_DOB", Convert.ToDateTime(Convert.ToDateTime(MainHead[11]).ToString("dd/MMM/yyyy")));
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_NATIVE_PLACE", MainHead[12]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_SEX", MainHead[13]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_NAIONALITY", MainHead[14]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_RELIGION", MainHead[15]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_MATRITAL_STATUS", MainHead[16]);
                    cmdUpdate.Parameters.AddWithValue("@dtHAMH_MARRIAGE_DATE", Convert.ToDateTime(Convert.ToDateTime(MainHead[17]).ToString("dd/MMM/yyyy")));
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_NOMINIEE_NAME", MainHead[18]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_NOMINIEE_RELATION", MainHead[19]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_PD_HEIGHT", MainHead[20].ToString() == "" ? 0 : Convert.ToDouble(MainHead[20]));
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_PD_WEIGHT", MainHead[21].ToString() == "" ? 0 : Convert.ToDouble(MainHead[21]));
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_PD_BLOOD_GROUP_CODE", MainHead[22].ToString() == "SELECT" ? "" : MainHead[22]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_PD_PHYSICAL_DISABILITY", MainHead[23]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_PD_PROLONGED_ILLNESS", MainHead[24]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_PD_PROLONGED_ILLNESS_PERIOD", MainHead[25]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PRES_ADDR_HNO", MainHead[26]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PRES_ADDR_LANDMARK", MainHead[27]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PRES_ADDR_VILL_OR_TOWN", MainHead[28]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PRES_ADDR_MANDAL", MainHead[29]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PRES_ADDR_DISTRICT", MainHead[30]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PRES_ADDR_STATE", MainHead[31]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PRES_ADDR_PIN", Convert.ToInt32(MainHead[32]));
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PRES_ADDR_PHONE", MainHead[33]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PERM_ADDR_HNO", MainHead[34]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PERM_ADDR_LANDMARK", MainHead[35]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PERM_ADDR_VILL_OR_TOWN", MainHead[36]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PERM_ADDR_MANDAL", MainHead[37]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PERM_ADDR_DISTRICT", MainHead[38]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PERM_ADDR_STATE", MainHead[39]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PERM_ADDR_PIN", Convert.ToInt32(MainHead[40]));
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_PERM_ADDR_PHONE", MainHead[41]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_NAME", MainHead[42]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_ADDR_HNO", MainHead[43]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_ADDR_LANDMARK", MainHead[44]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_ADDR_VILL_OR_TOWN", MainHead[45]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_ADDR_MANDAL", MainHead[46]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_ADDR_DISTRICT", MainHead[47]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_ADDR_STATE", MainHead[48]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_ADDR_PIN", Convert.ToInt32(MainHead[49]));
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_ADDR_PHONE_RES", MainHead[50]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_ADD_CONTPERS_ADDR_PHONE_OFF", MainHead[51]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_VD_OWN_VEHICLE_FLAG", MainHead[52]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_VD_VEHICLE_REG_NUMBER", MainHead[53]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_VD_VEHICLE_MAKE", MainHead[54]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_VD_DL_NUMBER", MainHead[55]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_VD_PASSPORT_NUMBER", MainHead[56]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_VD_PAN_CARD_NUMBER", MainHead[57]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_INTERVIEW_DONE_BY_ECODE", Convert.ToInt32(MainHead[58]));
                    cmdUpdate.Parameters.AddWithValue("@dtHAMH_INTERVIEW_DATE", Convert.ToDateTime(Convert.ToDateTime(MainHead[59]).ToString("dd/MMM/yyyy")));
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_INTERVIEW_REMARKS", MainHead[60]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_EORA_TYPE", MainHead[61]);
                    cmdUpdate.Parameters.AddWithValue("@ivHAMH_EORA_CODE", Convert.ToInt32(MainHead[62]));
                    if (iMode == 101)
                    {
                        cmdUpdate.Parameters.AddWithValue("@ivHAMH_CREATED_BY", MainHead[63]);
                        cmdUpdate.Parameters.AddWithValue("@dtHAMH_CREATED_DATE", Convert.ToDateTime(Convert.ToDateTime(MainHead[64]).ToString("dd/MMM/yyyy")));
                    }
                    else
                    {
                        cmdUpdate.Parameters.AddWithValue("@ivHAMH_MODIFIED_BY", MainHead[63]);
                        cmdUpdate.Parameters.AddWithValue("@dtHAMH_MODIFIED_DATE", Convert.ToDateTime(Convert.ToDateTime(MainHead[64]).ToString("dd/MMM/yyyy")));
                    }
                    cmdUpdate.Parameters.AddWithValue("@HAED_SSC_NUMBER", MainHead[65]);

                    cmdUpdate.Parameters.AddWithValue("@ivApplNo", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmdUpdate.Parameters.AddWithValue("@ivRetECode", SqlDbType.Int).Direction = ParameterDirection.Output;

                    cmdUpdate.ExecuteNonQuery();
                    iApplNo = (int)(cmdUpdate.Parameters["@ivApplNo"].Value);
                    iECode = (int)(cmdUpdate.Parameters["@ivRetECode"].Value);
                    if (iMode == 102)
                        iApplNo = Convert.ToInt32(MainHead[2]);
                    #endregion

                    #region "EORA MASTER DETAILS"
                    if (iMode == 101)
                    {
                        sqlText += " INSERT INTO EORA_MASTER (BRANCH_CODE,DEPT_ID,ECODE,MEMBER_NAME,EORA,HRIS_EMP_NAME,DESG_ID,DESIG,HRIS_DESIG,HRIS_DESIG_ID,EMP_DOJ,EMP_DOB,FATHER_NAME,ELEVEL_ID,COMPANY_CODE) " +
                                    "VALUES('" + EoraMast[0].ToString() + "','" + EoraMast[1].ToString() + "','" + iECode + "','" + EoraMast[4].ToString() + "','" + EoraMast[5].ToString() +
                                    "','" + EoraMast[4].ToString() + "','" + Convert.ToInt32(EoraMast[6]) + "','" + EoraMast[7].ToString() + "','" + EoraMast[7].ToString() + "','" + Convert.ToInt32(EoraMast[6]) + "','" + Convert.ToDateTime(EoraMast[8]).ToString("dd/MMM/yyyy") +
                                    "','" + Convert.ToDateTime(EoraMast[9]).ToString("dd/MMM/yyyy") + "','" + EoraMast[10].ToString() + "','" + Convert.ToInt32(EoraMast[11]) + "','" + EoraMast[12].ToString() + "'); ";
                        //if (sqlText.Length > 10)
                        //{
                        //    SqlCommand cmdEoraMas = new SqlCommand(sqlText, CN, transaction);
                        //    cmdEoraMas.CommandType = CommandType.Text;
                        //    cmdEoraMas.ExecuteNonQuery();
                        //}
                    }
                    else
                    {
                        SqlCommand cmdEoraMast = new SqlCommand("HR_IU_EORA_MASTER", CN);
                        cmdEoraMast.CommandType = CommandType.StoredProcedure;
                        cmdEoraMast.Parameters.AddWithValue("@inMode", iMode);
                        cmdEoraMast.Parameters.AddWithValue("@COMPANY_CODE", EoraMast[12].ToString());
                        cmdEoraMast.Parameters.AddWithValue("@BRANCH_CODE", EoraMast[0].ToString());
                        cmdEoraMast.Parameters.AddWithValue("@DEPT_ID", EoraMast[1].ToString());
                        cmdEoraMast.Parameters.AddWithValue("@ECODE", iECode);
                        cmdEoraMast.Parameters.AddWithValue("@MEMBER_NAME", EoraMast[4].ToString());
                        cmdEoraMast.Parameters.AddWithValue("@EORA", EoraMast[5].ToString());
                        cmdEoraMast.Parameters.AddWithValue("@DESG_ID", Convert.ToInt32(EoraMast[6]));
                        cmdEoraMast.Parameters.AddWithValue("@DESIG", EoraMast[7].ToString());
                        cmdEoraMast.Parameters.AddWithValue("@EMP_DOJ", Convert.ToDateTime(Convert.ToDateTime(EoraMast[8]).ToString("dd/MMM/yyyy")));
                        cmdEoraMast.Parameters.AddWithValue("@EMP_DOB", Convert.ToDateTime(Convert.ToDateTime(EoraMast[9]).ToString("dd/MMM/yyyy")));
                        cmdEoraMast.Parameters.AddWithValue("@FATHER_NAME", EoraMast[10].ToString());
                        cmdEoraMast.Parameters.AddWithValue("@ELEVEL_ID", Convert.ToInt32(EoraMast[11]));
                        cmdEoraMast.ExecuteNonQuery();
                    }
                    #endregion

                    #region "FAMILY DETAILS"
                    //sqlText = "";
                    if (dtFamily.Rows.Count > 0)
                    {
                        sqlText += " DELETE FROM HR_APPL_FAMILY_DETL WHERE HAFD_APPL_NUMBER='" + iApplNo + "'; ";
                        for (int i = 0; i < dtFamily.Rows.Count; i++)
                        {
                            sqlText += " INSERT INTO HR_APPL_FAMILY_DETL(HAFD_COMPANY_CODE,HAFD_BRANCH_CODE,HAFD_APPL_NUMBER,HAFD_APPL_SL_NUMBER,HAFD_FAMILY_MEMBER_RELATIONSHIP," +
                                            "HAFD_FAMILY_MEMBER_NAME,HAFD_FAMILY_MEMBER_DOB,HAFD_RESIDING_WITH_APPLICANT,HAFD_DEPENDING_ON_APPLICANT,HAFD_FAMILY_MEMBER_OCCUPATION) " +
                                        "VALUES('" + MainHead[0] + "','" + MainHead[1] + "','" + iApplNo + "','" + Convert.ToInt32(Convert.ToInt32(i + 1)) + "','" + dtFamily.Rows[i][1].ToString() + "'," +
                                            "'" + dtFamily.Rows[i][2].ToString() + "','" + Convert.ToDateTime(dtFamily.Rows[i][3]).ToString("dd/MMM/yyyy") + "','" + dtFamily.Rows[i][4].ToString() + "','" + dtFamily.Rows[i][5].ToString() + "','" + dtFamily.Rows[i][6].ToString() + "'); ";

                        }
                        //if (sqlText.Length > 10)
                        //{
                        //    SqlCommand cmdFamDetl = new SqlCommand(sqlText, CN, transaction);
                        //    cmdFamDetl.CommandType = CommandType.Text;
                        //    cmdFamDetl.ExecuteNonQuery();
                        //}
                    }
                    #endregion

                    #region "EDUCATION DETAILS"
                    //sqlText = "";
                    if (dtEducation.Rows.Count > 0)
                    {
                        sqlText += " DELETE FROM HR_APPL_EDU_DETL WHERE HAED_APPL_NUMBER='" + iApplNo + "'; ";
                        for (int i = 0; i < dtEducation.Rows.Count; i++)
                        {
                            sqlText += " INSERT INTO HR_APPL_EDU_DETL(HAED_COMPANY_CODE,HAED_BRANCH_CODE,HAED_APPL_NUMBER,HAED_APPL_SL_NUMBER,HAED_EXAMINATION_PASSED," +
                                          "HAED_EXAMTYPE,HAED_YEAR_OF_PASSING,HAED_INSITUTION_NAME,HAED_INSITUTION_LOCATION,HAED_SUBJECTS,HAED_UNIVERSITY,HAED_PERC_MARKS) " +
                                      "VALUES('" + MainHead[0] + "','" + MainHead[1] + "','" + iApplNo + "','" + Convert.ToInt32(Convert.ToInt32(i + 1)) + "','" + dtEducation.Rows[i][1].ToString() + "'," +
                                          "'" + dtEducation.Rows[i][2].ToString() + "','" + dtEducation.Rows[i][3].ToString() + "','" + dtEducation.Rows[i][4].ToString() + "','" + dtEducation.Rows[i][5].ToString() + "','" + dtEducation.Rows[i][6].ToString() + "','" + dtEducation.Rows[i][7].ToString() + "','" + Convert.ToDouble(dtEducation.Rows[i][8]) + "'); ";
                        }
                        //if (sqlText.Length > 10)
                        //{
                        //    SqlCommand cmdEduDetl = new SqlCommand(sqlText, CN, transaction);
                        //    cmdEduDetl.CommandType = CommandType.Text;
                        //    cmdEduDetl.ExecuteNonQuery();
                        //}
                    }
                    #endregion

                    #region "SHORT COURSE DETAILS"
                    //sqlText = "";
                    if (dtSortCourse.Rows.Count > 0)
                    {
                        sqlText += " DELETE FROM HR_APPL_SHORT_COURSE_DETL WHERE HASCD_APPL_NUMBER='" + iApplNo + "'; ";
                        for (int i = 0; i < dtSortCourse.Rows.Count; i++)
                        {

                            sqlText += " INSERT INTO HR_APPL_SHORT_COURSE_DETL(HASCD_COMPANY_CODE,HASCD_BRANCH_CODE,HASCD_APPL_NUMBER,HASCD_APPL_SL_NUMBER,HASCD_COURSE_NAME," +
                                          "HASCD_YEAR_OF_PASSING,HASCD_INSITUTE_ORGANISATION,HASCD_INSITUTE_LOCATION,HASCD_SUBJECTS,HASCD_DURATION,HASCD_PERC_MARKS) " +
                                     "VALUES('" + MainHead[0] + "','" + MainHead[1] + "','" + iApplNo + "','" + Convert.ToInt32(Convert.ToInt32(i + 1)) + "','" + dtSortCourse.Rows[i][1].ToString() + "','" + Convert.ToInt32(dtSortCourse.Rows[i][2]) + "'," +
                                          "'" + dtSortCourse.Rows[i][3].ToString() + "','" + dtSortCourse.Rows[i][4].ToString() + "','" + dtSortCourse.Rows[i][5].ToString() + "','" + dtSortCourse.Rows[i][6].ToString() + "','" + Convert.ToDouble(dtSortCourse.Rows[i][7]) + "'); ";
                        }
                        //if (sqlText.Length > 10)
                        //{
                        //    SqlCommand cmdShortCourseDetl = new SqlCommand(sqlText, CN, transaction);
                        //    cmdShortCourseDetl.CommandType = CommandType.Text;
                        //    cmdShortCourseDetl.ExecuteNonQuery();
                        //}
                    }
                    #endregion

                    #region "ECA DETAILS"
                    //sqlText = "";
                    if (dtECA.Rows.Count > 0)
                    {
                        sqlText += " DELETE FROM HR_APPL_ECA_DETL WHERE HAECD_APPL_NUMBER='" + iApplNo + "'; ";
                        for (int i = 0; i < dtECA.Rows.Count; i++)
                        {
                            sqlText += " INSERT INTO HR_APPL_ECA_DETL(HAECD_COMPANY_CODE,HAECD_BRANCH_CODE,HAECD_APPL_NUMBER,HAECD_APPL_SL_NUMBER,HAECD_ECA_TYPE,HAECD_REMARKS) " +
                                     "VALUES('" + MainHead[0] + "','" + MainHead[1] + "','" + iApplNo + "','" + Convert.ToInt32(Convert.ToInt32(i + 1)) + "','" + dtECA.Rows[i][1].ToString() + "','" + dtECA.Rows[i][2].ToString() + "'); ";
                        }
                        //if (sqlText.Length > 10)
                        //{
                        //    SqlCommand cmdEduQualDetl = new SqlCommand(sqlText, CN, transaction);
                        //    cmdEduQualDetl.CommandType = CommandType.Text;
                        //    cmdEduQualDetl.ExecuteNonQuery();
                        //}
                    }
                    #endregion

                    #region "LANGUAGE DETAILS"
                    //sqlText = "";
                    if (dtLanguages.Rows.Count > 0)
                    {
                        sqlText += " DELETE FROM HR_APPL_LANGUAGES_KNOWN WHERE HALK_APPL_NUMBER='" + iApplNo + "'; ";
                        for (int i = 0; i < dtLanguages.Rows.Count; i++)
                        {
                            sqlText += " INSERT INTO HR_APPL_LANGUAGES_KNOWN(HALK_APPL_NUMBER,HALK_APPL_SL_NO,HALK_LANGUAGE,HALK_READ_FLAG,HALK_WRITE_FLAG,HALK_SPEAK_FLAG) " +
                                     "VALUES('" + iApplNo + "','" + dtLanguages.Rows[i][0].ToString() + "','" + dtLanguages.Rows[i][1].ToString() + "','" + dtLanguages.Rows[i][2].ToString() + 
                                     "','" + dtLanguages.Rows[i][3].ToString() + "','" + dtLanguages.Rows[i][4].ToString() + "'); ";
                        }
                        //if (sqlText.Length > 10)
                        //{
                        //    SqlCommand cmdEduQualDetl = new SqlCommand(sqlText, CN, transaction);
                        //    cmdEduQualDetl.CommandType = CommandType.Text;
                        //    cmdEduQualDetl.ExecuteNonQuery();
                        //}
                    }
                    #endregion

                    #region "EXPERIENCE DETAILS"
                    //sqlText = "";
                    if (dtExperience.Rows.Count > 0)
                    {
                        sqlText += " DELETE FROM HR_APPL_EXP_DETL WHERE HAEXD_APPL_NUMBER='" + iApplNo + "'; ";
                        for (int i = 0; i < dtExperience.Rows.Count; i++)
                        {
                            sqlText += " INSERT INTO HR_APPL_EXP_DETL(HAEXD_COMPANY_CODE,HAEXD_BRANCH_CODE,HAEXD_APPL_NUMBER,HAEXD_APPL_SL_NUMBER,HAEXD_FROM_DATE," +
                                            "HAEXD_TO_DATE,HAEXD_ORGANISATION_NAME,HAEXD_ORGANISATION_HNO,HAEXD_ORGANISATION_LANDMARK,HAEXD_ORGANISATION_VILL_OR_TOWN," +
                                            "HAEXD_ORGANISATION_MANDAL,HAEXD_ORGANISATION_DISTRICT,HAEXD_ORGANISATION_STATE,HAEXD_ORGANISATION_PIN,HAEXD_JOINING_DESIGNATION," +
                                            "HAEXD_LEAVING_DESIGNATION,HAEXD_GROSS_SALARY,HAEXD_SALARY_REMARKS,HAEXD_REASONS_FOR_LEAVING) " +
                                        "VALUES('" + MainHead[0] + "','" + MainHead[1] + "','" + iApplNo + "','" + Convert.ToInt32(Convert.ToInt32(i + 1)) + "','" + Convert.ToDateTime(dtExperience.Rows[i][2]).ToString("dd/MMM/yyyy") + "','" + Convert.ToDateTime(dtExperience.Rows[i][3]).ToString("dd/MMM/yyyy") + "'," +
                                            "'" + dtExperience.Rows[i][1].ToString() + "','" + dtExperience.Rows[i][4].ToString() + "','" + dtExperience.Rows[i][5].ToString() + "','" + dtExperience.Rows[i][6].ToString() + "'," +
                                            "'" + dtExperience.Rows[i][7].ToString() + "','" + dtExperience.Rows[i][8].ToString() + "','" + dtExperience.Rows[i][9].ToString() + "','" + Convert.ToInt32(dtExperience.Rows[i][10]) + "','" + dtExperience.Rows[i][11].ToString() + "'," +
                                            "'" + dtExperience.Rows[i][12].ToString() + "','" + Convert.ToDouble(dtExperience.Rows[i][13]).ToString("0.00") + "','" + dtExperience.Rows[i][14].ToString() + "','" + dtExperience.Rows[i][15].ToString() + "'); ";
                        }
                        ////if (sqlText.Length > 10)
                        ////{
                        ////    SqlCommand cmdExpDetl = new SqlCommand(sqlText, CN, transaction);
                        ////    cmdExpDetl.CommandType = CommandType.Text;
                        ////    cmdExpDetl.ExecuteNonQuery();
                        ////}
                    }
                    #endregion

                    #region "INDUCTION_TRAINING"
                    //sqlText = "";
                    if (Ind_Training != "")
                    {
                        sqlText += " DELETE FROM HR_APPL_INDUCTION_TRAINING WHERE HAIT_EORA_CODE='" + iECode + "'; ";
                        string[] IndTrVal = Ind_Training.Split(',');
                        sqlText += " INSERT INTO HR_APPL_INDUCTION_TRAINING (HAIT_EORA_CODE,HAIT_TRAINING_STATUS,HAIT_TRAINER_ECODE,HAIT_TRAINING_FROM,HAIT_TRAINING_TO) " +
                                        "VALUES('" + iECode + "','" + IndTrVal[0].ToString() + "','" + Convert.ToInt32(IndTrVal[1].ToString()) + "','" + Convert.ToDateTime(Convert.ToDateTime(IndTrVal[2]).ToString("dd/MMM/yyyy")).ToString("dd/MMM/yyyy") + "','" + Convert.ToDateTime(Convert.ToDateTime(IndTrVal[3]).ToString("dd/MMM/yyyy")).ToString("dd/MMM/yyyy") + "'); ";
                    }
                    //if (sqlText.Length > 10)
                    //{
                    //    SqlCommand cmdIndTraingDetl = new SqlCommand(sqlText, CN, transaction);
                    //    cmdIndTraingDetl.CommandType = CommandType.Text;
                    //    cmdIndTraingDetl.ExecuteNonQuery();
                    //}
                    #endregion

                    #region "REFERENCE DETAILS"
                    //sqlText = "";
                    if (dtReference.Rows.Count > 0)
                    {
                        sqlText += " DELETE FROM HR_APPL_REF_DETL WHERE HARD_APPL_NUMBER='" + iApplNo + "'; ";
                        for (int i = 0; i < dtReference.Rows.Count; i++)
                        {
                            if (dtReference.Rows[i][10].ToString() == "")
                                dtReference.Rows[i][10] = 0;
                            sqlText += " INSERT INTO HR_APPL_REF_DETL(HARD_COMPANY_CODE,HARD_BRANCH_CODE,HARD_APPL_NUMBER,HARD_APPL_SL_NUMBER,HARD_REF_NAME," +
                                            "HARD_REF_OCCUPATION,HARD_REF_TELEPHONE,HARD_ADDR_HNO,HARD_ADDR_LANDMARK,HARD_ADDR_VILL_OR_TOWN,HARD_ADDR_MANDAL," +
                                            "HARD_ADDR_DISTRICT,HARD_ADDR_STATE,HARD_ADDR_PIN,HARD_ADDR_PHONE) " +
                                        "VALUES('" + MainHead[0] + "','" + MainHead[1] + "','" + iApplNo + "','" + Convert.ToInt32(Convert.ToInt32(i + 1)) + "','" + dtReference.Rows[i][1].ToString() + "','" + dtReference.Rows[i][2].ToString() + "'," +
                                            "'" + dtReference.Rows[i][3].ToString() + "','" + dtReference.Rows[i][4].ToString() + "','" + dtReference.Rows[i][5].ToString() + "','" + dtReference.Rows[i][6].ToString() + "','" + dtReference.Rows[i][7].ToString() + "','" + dtReference.Rows[i][8].ToString() + "'," +
                                            "'" + dtReference.Rows[i][9].ToString() + "','" + Convert.ToInt64(dtReference.Rows[i][10]) + "','" + dtReference.Rows[i][11].ToString() + "'); ";
                        }
                        //if (sqlText.Length > 10)
                        //{
                        //    SqlCommand cmdRefDetl = new SqlCommand(sqlText, CN, transaction);
                        //    cmdRefDetl.CommandType = CommandType.Text;
                        //    cmdRefDetl.ExecuteNonQuery();
                        //}
                    }
                    #endregion

                    #region "DOCUMENT DETAILS"
                    //sqlText = "";
                    if (dtDocuments.Rows.Count > 0)
                    {
                        sqlText += " DELETE FROM HR_APPL_APPROVAL_DETL WHERE HAAD_APPL_NUMBER='" + iApplNo + "'; ";
                        for (int i = 0; i < dtDocuments.Rows.Count; i++)
                        {
                            if (dtDocuments.Rows[i][0].ToString() == "True")
                            {
                                sqlText += " INSERT INTO HR_APPL_APPROVAL_DETL(HAAD_COMPANY_CODE,HAAD_BRANCH_CODE,HAAD_APPL_NUMBER,HAAD_APPL_SL_NUMBER,HAAD_CHECK_LIST_HEAD," +
                                                "HAAD_CHECK_LIST_RECD_BR_FLAG,HAAD_CHECK_LIST_RECD_BR_DATE,HAAD_CHECK_LIST_BR_REMARKS,HAAD_CHECK_LIST_RECD_HO_FLAG,HAAD_CHECK_LIST_RECD_HO_DATE,HAAD_CHECK_LIST_HO_REMARKS) " +
                                            "VALUES('" + MainHead[0] + "','" + MainHead[1] + "','" + iApplNo + "','" + Convert.ToInt32(Convert.ToInt32(i + 1)) + "','" + dtDocuments.Rows[i][1].ToString() + "','1'," +
                                                "'" + Convert.ToDateTime(dtDocuments.Rows[i][2]).ToString("dd/MMM/yyyy") + "','" + dtDocuments.Rows[i][3].ToString() + "','1','" + Convert.ToDateTime("1/1/1900").ToString("dd/MMM/yyyy") + "','" + string.Empty + "'); ";
                            }
                        }
                        //if (sqlText.Length > 10)
                        //{
                        //    SqlCommand cmdDocDetl = new SqlCommand(sqlText, CN, transaction);
                        //    cmdDocDetl.CommandType = CommandType.Text;
                        //    cmdDocDetl.ExecuteNonQuery();
                        //}
                    }
                    #endregion

                    if (sqlText.Length > 10)
                    {
                        SqlCommand sqlCmd = new SqlCommand(sqlText, CN);
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.ExecuteNonQuery();
                    }
                    //transaction.Commit();
                }
                catch (Exception ex)
                {
                    //transaction.Rollback();
                    return ex.ToString();
                }
                finally
                {
                    CN.Close();
                    objSecurity = null;
                }
            }
            if (iMode == 102)
                return "Updated , " + iECode + "," + iApplNo;
            else
                return "Saved , " + iECode + "," + iApplNo;
        }

        #region "PHOTO UPDATED"
        public void UpdatePhoto(int ApplicationID, byte[] buffer)
        {
            objSecurity = new Security();
            SqlConnection CN = new SqlConnection(objSecurity.GetDecodeString(sConnVal));
            SqlCommand cmdPhoto = new SqlCommand("HR_IU_MainHead", CN);
            cmdPhoto.CommandType = CommandType.StoredProcedure;
            cmdPhoto.Parameters.AddWithValue("@inMode", 103);
            cmdPhoto.Parameters.AddWithValue("@ivHAMH_APPL_NUMBER", ApplicationID);
            cmdPhoto.Parameters.AddWithValue("@ivHAMH_MY_PHOTO", buffer);
            CN.Open();
            cmdPhoto.ExecuteNonQuery();
            CN.Close();
            objSecurity = null;
        }
        #endregion

        public string SaveAppointment(int iMode, string[] Appointment)
        {
            objSecurity = new Security();
            try
            {
                SqlConnection CN = new SqlConnection(objSecurity.GetDecodeString(sConnVal));
                SqlCommand cmdUpdate = new SqlCommand("HR_IUD_Appointment", CN);
                cmdUpdate.CommandType = CommandType.StoredProcedure;
                cmdUpdate.Parameters.AddWithValue("@inMode", iMode);
                cmdUpdate.Parameters.AddWithValue("@ivHRAP_COMPANY_CODE", Appointment[0].ToString());
                cmdUpdate.Parameters.AddWithValue("@ivHRAP_BRANCH_CODE", Appointment[1].ToString());
                cmdUpdate.Parameters.AddWithValue("@ivHRAP_APPL_NUMBER", Convert.ToInt32(Appointment[2]));
                cmdUpdate.Parameters.AddWithValue("@ivHRAP_EORA_CODE", Convert.ToInt32(Appointment[3]));
                cmdUpdate.Parameters.AddWithValue("@ivHRAP_LTR_REF_NO", Appointment[4].ToString());
                cmdUpdate.Parameters.AddWithValue("@dtHRAP_LTR_REF_DATE", Convert.ToDateTime(Appointment[5]));
                cmdUpdate.Parameters.AddWithValue("@dtHRAP_EFF_DATE", Convert.ToDateTime(Appointment[6]));
                cmdUpdate.Parameters.AddWithValue("@ivHRAP_REPO_TO_ECODE", Convert.ToInt32(Appointment[7]));
                cmdUpdate.Parameters.AddWithValue("@ivHRAP_REPO_TO_BRANCH_CODE", Appointment[8].ToString());
                cmdUpdate.Parameters.AddWithValue("@ivHRAP_BASIC", Convert.ToDecimal(Appointment[9]));
                cmdUpdate.Parameters.AddWithValue("@ivHRAP_HRA", Convert.ToDecimal(Appointment[10]));
                cmdUpdate.Parameters.AddWithValue("@ivHRAP_CCA", Convert.ToDecimal(Appointment[11]));
                cmdUpdate.Parameters.AddWithValue("@ivHRAP_CONV_ALW", Convert.ToDecimal(Appointment[12]));
                cmdUpdate.Parameters.AddWithValue("@ivHRAP_SPL_ALW", Convert.ToDecimal(Appointment[13]));
                cmdUpdate.Parameters.AddWithValue("@ivHRAP_UNF_ALW", Convert.ToDecimal(Appointment[14]));
                cmdUpdate.Parameters.AddWithValue("@ivHRAP_CH_ED_ALW", Convert.ToDecimal(Appointment[15]));
                cmdUpdate.Parameters.AddWithValue("@ivHRAP_MED_REIMB", Convert.ToDecimal(Appointment[16]));
                if (iMode == 101)
                    cmdUpdate.Parameters.AddWithValue("@ivHRAP_CREATED_BY", Appointment[17].ToString());
                else
                    cmdUpdate.Parameters.AddWithValue("@ivHRAP_MODIFIED_BY", Appointment[17].ToString());
                CN.Open();
                cmdUpdate.ExecuteNonQuery();
                CN.Close();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            objSecurity = null;
            if (iMode == 102)
                return "Update";
            else
                return "Saved";
        }

        public DataSet EmployeeLOPDetails_Get(string CompCode, string BranchCode, Int32 DeptId, string Month)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, CompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sDeptId", DbType.Int32, DeptId, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, Month, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("EmployeeLOPDetails_Get", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }

        public DataSet GetAllBranchList(string CompanyCode, string BranchType, string StateCode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@company_code", DbType.String, CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@BranchType", DbType.String, BranchType, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@StateCode", DbType.String, StateCode, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("HR_GetAllBranchList", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
            }
            return ds;
        }

        public DataSet GetUserBranchList(string sUserId, string sCompanyCode, string sBranchType, string sStateCode)
        {

            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@company_code", DbType.String, sCompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@BranchType", DbType.String, sBranchType, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@StateCode", DbType.String, sStateCode, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xUserID", DbType.String, sUserId, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SSCRM_GetUserBranchList", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
            }
            return ds;

        }

        public DataSet GetMaxApplicationNo()
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@imode", DbType.String, 110, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("HR_GetALLInformation", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
            }
            return ds;
        }

        public DataSet GetNameandEcode()
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@imode", DbType.String, 111, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("HR_GetALLInformation", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
            }
            return ds;
        }
        public DataSet GetEoraCode(int Ecode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@ECode", DbType.Int32, Ecode, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SSHR_EmployeeDetails", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
            }
            return ds;
        }
        public DataSet GetDuplicatRecords()
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xiMod", DbType.Int32, 101, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetApplMultipleEntries", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
            }
            return ds;
        }

        public int DelDuplicatRecords(int ApplNumber, int Ecode, int iMode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            int retVal = 0;
            try
            {
                param[0] = objSQLdb.CreateParameter("@xApplNumber", DbType.Int32, ApplNumber, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xEcode", DbType.Int32, Ecode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xiMod", DbType.Int32, iMode, ParameterDirection.Input);
                retVal = objSQLdb.ExecuteSaveData("GetApplMultipleEntries", param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
            }
            return retVal;
        }

        public DataSet GetPromotionsDetlsForApproval(string sComp, string sBranch, string sPbType, string sDetls)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            //int retVal = 0;
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, sComp, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, sBranch, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xPBType", DbType.String, sPbType, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xDetlType", DbType.String, sDetls, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetPromotionsDetlsForApproval", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
            }
            return ds;
        }

        public void SendSMSForEmployeeCodeGeneration(string ECode, string EmpName, string MobileNo,string EmpType,string sCompanyName)
        {
            objSQLdb = new SQLDB();
            string sMessage = "";
            string sSmsType = "";
            decimal iMobileNo = 0;
            try
            {
                iMobileNo = Convert.ToDecimal(MobileNo);
            }
            catch (Exception ex)
            {
                iMobileNo = 0;
            }
            if (EmpType == "E")
            {
                sMessage = "Dear " + EmpName + ", Hearty welcome to " + sCompanyName + ", Your E-Code is " + ECode + ". " +
                            "Please use this code for any future correspondence. We wish you a bright career with us - HR Dept.";
                sSmsType = "EMP_JOIN";
                
            }
            else
            {
                sMessage = "Dear " + EmpName + ", Hearty welcome to " + sCompanyName + ", Where leaders are created. "+
                            "Your A-Code is " + ECode + ". Please use this code for any future correspondence - Management.";
                sSmsType = "AGENT_APPROVAL";
            }
            try
            {
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://dnd.sendsmsindia.org/sendsms.jsp?" +
                //            "user=SBTLAP&password=admin@66&mobiles=" + MobileNo +
                //            "&sms=" + sMessage + "&senderid=SSGCHO&version=3");

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.smsjust.com/blank/sms/user/urlsms.php?username=shivashakti&pass=1234567&senderid=SSGCHO&dest_mobileno="
                                        + MobileNo + "&msgtype=UNI&message=" + sMessage + "&response=Y");

                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://shiva.bulksmshyderabad.co.in/api/smsapi.aspx?" +
                //            "username=shivashakti&password=shiva123&to=" + MobileNo +
                //            "&from=SSGCHO&message=" + sMessage + "");

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                
                //string sqlText = "INSERT INTO SMS_HISTORY("+
                //                    "sms_sender_id,"+
                //                    "sms_mobile_no,"+
                //                    "sms_name,"+
                //                    "sms_type,"+
                //                    "sms_message,"+
                //                    "sms_sent_by,"+
                //                    "sms_sent_date)"+
                //                    " VALUES('SSGCHO'"+
                //                    ",'" + MobileNo + 
                //                    "','" + ECode + "-" + EmpName +
                //                    "','" + sSmsType + 
                //                    "','" + sMessage + 
                //                    "','ERP',getdate());";
                //objSQLdb.ExecuteSaveData(sqlText);

            }
            catch
            {

            }
        }

        public Hashtable GetMisconductDetails(Int32 MisConId)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@MisConId", DbType.Int32, MisConId, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("Get_MisconductHeadDetails", CommandType.StoredProcedure, param);

                ht.Add("MisconductHeadDetails", ds.Tables[0]);
                ds = null;
                param = null;
                objSQLdb = null;

                objSQLdb = new SQLDB();
                param = new SqlParameter[1];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@MisConId", DbType.Int32, MisConId, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_HRMisConductEmpDetails", CommandType.StoredProcedure, param);

                ht.Add("MisconductEmpDetails", ds.Tables[0]);


                ds = null;
                param = null;
                objSQLdb = null;

                objSQLdb = new SQLDB();
                param = new SqlParameter[1];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@MisConId", DbType.Int32, MisConId, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_MisConductDeptDetails", CommandType.StoredProcedure, param);
                ht.Add("MisconductDeptDetails", ds.Tables[0]);

                ds = null;
                param = null;
                objSQLdb = null;

                objSQLdb = new SQLDB();
                param = new SqlParameter[1];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@MisConId", DbType.Int32, MisConId, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_MisCondExplDetails", CommandType.StoredProcedure, param);
                ht.Add("MisconductExplDetails", ds.Tables[0]);

                ds = null;
                param = null;
                objSQLdb = null;

                objSQLdb = new SQLDB();
                param = new SqlParameter[1];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@MisConId", DbType.Int32, MisConId, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_MisConductRecoveryModeDetl", CommandType.StoredProcedure, param);
                ht.Add("MisCondRecoveryDetl", ds.Tables[0]);

                ds = null;
                param = null;
                objSQLdb = null;

                objSQLdb = new SQLDB();
                param = new SqlParameter[1];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@MisConId", DbType.Int32, MisConId, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_MisConductActualRecoveryDetl", CommandType.StoredProcedure, param);
                ht.Add("MisCondActualRecovery", ds.Tables[0]);


                ds = null;
                param = null;
                objSQLdb = null;

                objSQLdb = new SQLDB();
                param = new SqlParameter[1];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@MisConId", DbType.Int32, MisConId, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_MisconductMgntEmpDetl", CommandType.StoredProcedure, param);
                ht.Add("MisCondMgntEmpDetl", ds.Tables[0]);

                ds = null;
                param = null;
                objSQLdb = null;

                objSQLdb = new SQLDB();
                param = new SqlParameter[1];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@MisConId", DbType.Int32, MisConId, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_MisconductImageDetails", CommandType.StoredProcedure, param);
                ht.Add("MisCondImageDetl", ds.Tables[0]);



            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ht;
        }

        public DataSet GetEmpPFUANDetailData1(string eCode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xEcode", DbType.String, eCode, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetEmpPFUANDetailData1", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }
        public DataSet GetEmpPFUANDetailData2(string eCode, string sDocType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xEcode", DbType.Int32, Convert.ToInt32(eCode), ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xDocType", DbType.String, sDocType, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetEmpPFUANDetailData2", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }
        public DataSet GetLogicalBranches(string BranchCode, string LogBranCode, char Active)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sLogBranchCode", DbType.String, LogBranCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sActive", DbType.String, Active, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("LogicalBranchList_Proc", CommandType.StoredProcedure, param);
                param = null;
                objSQLdb = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }

        public DataSet GetPromotionDetlByDate(string sComp, string sBranch, DateTime FromDate, DateTime ToDate)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, sComp, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranCode", DbType.String, sBranch, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFromDate", DbType.DateTime, FromDate, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sToDate", DbType.DateTime, ToDate, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("Get_EmpPromotionDetailsByDate", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
            }
            return ds;
        }

        public DataSet GetPromotionDetlByEcode(Int32 Ecode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@XEcode", DbType.Int32, Ecode, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_EmpPromotionDetailsByEcode", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
            }
            return ds;
        }

        public DataSet GetTrainingTopicDetails(Int32 xTopicId)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@XTopicId", DbType.Int32, xTopicId, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("Get_TrainingTopicDetails", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
            }
            return ds;
        }
        public DataSet UpdatingLeavesCredit(int iECode, int iApplNO,int iYear)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xEcode", DbType.Int32, iECode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xApplNo", DbType.Int32, iApplNO, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xYear", DbType.Int32, iYear, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("LEAVE_CREDITS_UPDATE", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }
        public DataSet UpdatingLeavesCreditAgainstLeaveUpdate(int iECode, int iApplNO,int iYear)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xEcode", DbType.Int32, iECode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xApplNo", DbType.Int32, iApplNO, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xYear", DbType.Int32, iYear, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("LEAVE_CREDITS_UPDATE_AGNST_LEAVE_UPDATE", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }

        public DataSet Get_SalaryStructureDetails(string SalStructType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xSalStructType", DbType.String, SalStructType, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SalaryStructureDetails_Get", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;

        }

        public Hashtable GetTrainingPlanningDetails(string strProgramNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xProgramNo", DbType.Int32, strProgramNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("Get_TrainingProgramHeadDetl", CommandType.StoredProcedure, param);

                ht.Add("TrainingPrgHeadDetails", ds.Tables[0]);
                ds = null;
                param = null;
                objSQLdb = null;

                objSQLdb = new SQLDB();
                param = new SqlParameter[1];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@xProgramNo", DbType.String, strProgramNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_TrainingProgramTopicsDetl", CommandType.StoredProcedure, param);

                ht.Add("TrainingPrgTopicDetl", ds.Tables[0]);


                ds = null;
                param = null;
                objSQLdb = null;

                objSQLdb = new SQLDB();
                param = new SqlParameter[1];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@xProgramNo", DbType.String, strProgramNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_TrainingEmpDetails", CommandType.StoredProcedure, param);
                ht.Add("TrainingEmpDetl", ds.Tables[0]);



            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ht;
        }
        public DataSet EmployeeAttendDetails(string Month)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xWageMonth", DbType.String, Month, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("EmployeeAttendDetails", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }
        public Hashtable GetTrainingProgramDetails(string strProgramNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xProgramNo", DbType.Int32, strProgramNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("Get_ActualTrainingPrgHead", CommandType.StoredProcedure, param);

                ht.Add("ActualTrainingPrgHeadDetails", ds.Tables[0]);
                ds = null;
                param = null;
                objSQLdb = null;

                objSQLdb = new SQLDB();
                param = new SqlParameter[1];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@xProgramNo", DbType.String, strProgramNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_ActualTrainingTopicsDetl", CommandType.StoredProcedure, param);

                ht.Add("ActualTrainingPrgTopicDetl", ds.Tables[0]);


                ds = null;
                param = null;
                objSQLdb = null;

                objSQLdb = new SQLDB();
                param = new SqlParameter[1];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@xProgramNo", DbType.String, strProgramNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_ActualTrainingPrgEmpDetl", CommandType.StoredProcedure, param);
                ht.Add("ActualTrainingEmpDetl", ds.Tables[0]);



            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ht;
        }

        public DataSet GetSalesPromotionDetails(string CompanyCode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompanyCode", DbType.String, CompanyCode, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetSalesPromotionDetails", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }
        public DataSet GetEmployeesForMisconduct(string CompanyCode, string BranCode, string DocMonth, string EmpName)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, BranCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, DocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sECodeName", DbType.String, EmpName, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Misc_BranchLevelEcodeSearch_Get", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }

        public DataSet BranchUsersList_Get(string sCompCode, string sBranchCode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("BranchUsersList_Get", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }
        
        //public DataSet EmployeeAttendDetails(string Month)
        //{
        //    objSQLdb = new SQLDB();
        //    SqlParameter[] param = new SqlParameter[1];
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        param[0] = objSQLdb.CreateParameter("@xWageMonth", DbType.String, Month, ParameterDirection.Input);
        //        ds = objSQLdb.ExecuteDataSet("EmployeeAttendDetails", CommandType.StoredProcedure, param);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.ToString());
        //    }
        //    finally
        //    {
        //        param = null;
        //        objSQLdb = null;
        //    }
        //    return ds;
        //}
        public DataSet GetHOEmployeesForAuditDataBank(string EmpName)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sECodeName", DbType.String, EmpName, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_HO_Employees_For_Misconduct", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }

        public DataSet GetEmpAuditPlanningDetl(Int32 Ecode, string FromDate, string ToDate)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xEcode", DbType.Int32, Ecode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xFromDate", DbType.String, FromDate, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xToDate", DbType.String, ToDate, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_AuditDRPlanningDetails", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }

        public DataSet Get_Adhar_MasterDetails(Int32 ECode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sEcode", DbType.Int32, ECode, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("Get_Adhar_MasterDetails", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }

        public DataSet GetTrainingProgramsByTrainer(Int32 Ecode, string TrainerName, string FromDate, string ToDate, string sRepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xTrainerEcode", DbType.Int32, Ecode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xTrainerName", DbType.String, TrainerName, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xFromDate", DbType.String, FromDate, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xToDate", DbType.String, ToDate, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xRepType", DbType.String, sRepType, ParameterDirection.Input);


                ds = objSQLdb.ExecuteDataSet("Get_TrainingPrograms_By_Trainer", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }

        public DataSet EmployeeDeductionsDetails(string Month)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xWageMonth", DbType.String, Month, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xCompanyCode", DbType.String, Month, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, Month, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xPayRollType", DbType.String, Month, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_PayRollEmpListForDeductions", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return ds;
        }
    }
}
