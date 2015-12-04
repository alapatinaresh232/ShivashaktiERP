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

namespace SSAdmin
{
    public class StaffLevel
    {

        private SQLDB objData = null;
       
        #region Constructor
        public StaffLevel()
        {          
            
        }
        #endregion

        public DataSet GetStatesDS()
        {
            DataSet ds = new DataSet();
            objData = new SQLDB();
            try
            {

                ds = objData.ExecuteDataSet("GetStates_Proc", CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                objData = null;

            }
            return ds;
        }

        public DataSet GetDocuments()
        {
            DataSet ds = new DataSet();
            objData = new SQLDB();
            try
            {

                ds = objData.ExecuteDataSet("GetDocumentsHeads", CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                objData = null;

            }
            return ds;
        }
        public DataSet BranchGLList_Get(string sCompCode, string sBranchCode, string sDocMonth)
        {
            objData = new SQLDB();
            string sqlText = "";
            DataSet ds = new DataSet();
            try
            {
                sqlText = "SELECT DISTINCT LGM_GROUP_ECODE AS ECODE,CAST(LGM_GROUP_ECODE AS VARCHAR)+'--'+MEMBER_NAME+' ('+DESIG+')' AS ENAME FROM LevelGroup_map " +
                            "INNER JOIN EORA_MASTER ON ECODE = LGM_GROUP_ECODE " +
                            "WHERE lgm_branch_code = '" + sBranchCode + "' AND lgm_company_code = '" + sCompCode + "' AND lgm_doc_month = '" + sDocMonth + "';";
                ds = objData.ExecuteDataSet(sqlText);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                objData = null;
            }
            return ds;
        }

        public DataSet GetStaffLevelsDS(string sCompCode,string sBranchType)
        {
            DataSet ds = new DataSet();
            objData = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            try
            {
                param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBRType", DbType.String, sBranchType, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@nBRLevelId", DbType.Int16, CommonData.BranchLevelId, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("GetStaffLevels_Proc", CommandType.StoredProcedure,param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                objData = null;
                param = null;
            }
            return ds;
        }

        public DataSet GetStateBranchesDS(string sCompCode, string sStateCode, string sBranchCode)
        {
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            objData = new SQLDB();
            try
            {

                param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);

                if (sStateCode=="")
                    param[1] = objData.CreateParameter("@sStateCode", DbType.String, DBNull.Value, ParameterDirection.Input);
                else
                    param[1] = objData.CreateParameter("@sStateCode", DbType.String, sStateCode, ParameterDirection.Input);
                
                if (CommonData.BranchType == "HO")
                    param[2] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                else
                    param[2] = objData.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);

                ds = objData.ExecuteDataSet("LevelStateBranches_Proc", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objData = null;

            }
            return ds;
        }
        
        public DataSet LevelCompanyBranches_Proc(string sCompCode,  string sBranchCode)
        {
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            objData = new SQLDB();
            try
            {

                param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);

                if (CommonData.BranchType == "HO")
                    param[1] = objData.CreateParameter("@sBranchCode", DbType.String, "", ParameterDirection.Input);
                else
                    param[1] = objData.CreateParameter("@sBranchCode", DbType.String, "", ParameterDirection.Input);

                ds = objData.ExecuteDataSet("LevelCompanyBranches_Proc", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objData = null;

            }
            return ds;
        }
                
        public DataSet GetMappedSourceDS(string sCompCode, string sStateCode, string sBranchCode, int nLevelId, int nECode, string sLogBranchCode)
        {
            SqlParameter[] param = new SqlParameter[7];
            DataSet ds = new DataSet();
            objData = new SQLDB();
            try
            {

                param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@sStateCode", DbType.String, sStateCode, ParameterDirection.Input);
                param[3] = objData.CreateParameter("@nLDMId", DbType.Int16, nLevelId, ParameterDirection.Input);
                param[4] = objData.CreateParameter("@nDestEcode", DbType.Int32, nECode, ParameterDirection.Input);
                param[5] = objData.CreateParameter("@sDocMonth", DbType.String, CommonData.DocMonth, ParameterDirection.Input);
                param[6] = objData.CreateParameter("@sLogBranchCode", DbType.String, sLogBranchCode, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("GetMappedSourceToDest_Proc", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objData = null;

            }
            return ds;
        }

        public DataSet GetLevelEcodes(string sCompCode, string sStateCode, string sBranchCode, int nLevelId)
        {
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            objData = new SQLDB();
            try
            {
                param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@sStateCode", DbType.String, sStateCode, ParameterDirection.Input);
                param[3] = objData.CreateParameter("@nLDMId", DbType.Int16, nLevelId, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("GetLevelEcodes_Proc", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objData = null;

            }
            return ds;
        }

        public DataSet LevelGroupDestEcodes_Get(string sCompCode, string sStateCode, string sBranchCode, int nLevelId, int nGEcode)
        {
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();
            objData = new SQLDB();
            try
            {
                param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@sStateCode", DbType.String, sStateCode, ParameterDirection.Input);
                param[3] = objData.CreateParameter("@sDocMonth", DbType.String, CommonData.DocMonth, ParameterDirection.Input);
                param[4] = objData.CreateParameter("@nGroupEcode", DbType.Int32, nGEcode, ParameterDirection.Input);
                param[5] = objData.CreateParameter("@nLDMId", DbType.Int16, nLevelId, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("LevelGroupDestEcodes_Proc", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objData = null;

            }
            return ds;
        }

        public DataSet GetMappedStaffToLevelEcodeListDS(string sStateCode, string sBranchCode, int nLevelId)
        {
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            objData = new SQLDB();
            try
            {

                param[0] = objData.CreateParameter("@sCompCode", DbType.String, CommonData.CompanyCode.ToString(), ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@sStateCode", DbType.String, sStateCode, ParameterDirection.Input);
                param[3] = objData.CreateParameter("@nLDMId", DbType.Int16, nLevelId, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("GetMappedStaffToLevelEcodes_Proc", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objData = null;

            }
            return ds;
        }

        public DataSet GetMappedUnmappedListDS(string sCompanyCode, string sBranchCode, string sDocMonth,  char cMapped)
        {
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            objData = new SQLDB();
            try
            {

                param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompanyCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@sDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                param[3] = objData.CreateParameter("@cMapped", DbType.String, cMapped, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("EcodeMapOrUnmapped_Get", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objData = null;

            }
            return ds;
        }

        public DataSet LevelsLogicalBranchList_Get(string sBranchCode)
        {
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            objData = new SQLDB();
            try
            {

                param[0] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("LevelsLogicalBranchList_Get", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objData = null;

            }
            return ds;
        }

        public DataSet LevelGroupEcode_Get(string sCompCode, string sStateCode, string sBranchCode, int nLevelId)
        {
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            objData = new SQLDB();
            try
            {
                param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@sStateCode", DbType.String, sStateCode, ParameterDirection.Input);
                param[3] = objData.CreateParameter("@nLDMId", DbType.Int16, nLevelId, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("LevelGroupEcode_Get", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objData = null;

            }
            return ds;
        }

        public DataSet LevelGroupList_Get(string sCompCode, string sStateCode, string sBranchCode, string sDocMonth)
        {
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            objData = new SQLDB();
            try
            {
                param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@sStateCode", DbType.String, sStateCode, ParameterDirection.Input);
                param[3] = objData.CreateParameter("@sDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("LevelGroupList_Get", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objData = null;

            }
            return ds;
        }

        public DataSet LevelEcodeSearch_Get(string sCompCode, string sStateCode, string sBranchCode, string sDocMonth)
        {
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            objData = new SQLDB();
            try
            {
                param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@sStateCode", DbType.String, sStateCode, ParameterDirection.Input);
                param[3] = objData.CreateParameter("@sDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("LevelEcodeSearch_Get", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objData = null;

            }
            return ds;
        }

        public DataSet LevelGroupEcodeMapped_Get(string sCompCode, string sStateCode, string sBranchCode, int nLevelId, int nECode, string sLogBranchCode)
        {
            SqlParameter[] param = new SqlParameter[7];
            DataSet ds = new DataSet();
            objData = new SQLDB();
            try
            {

                param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@sStateCode", DbType.String, sStateCode, ParameterDirection.Input);
                param[3] = objData.CreateParameter("@nLDMId", DbType.Int16, nLevelId, ParameterDirection.Input);
                param[4] = objData.CreateParameter("@nDestEcode", DbType.Int32, nECode, ParameterDirection.Input);
                param[5] = objData.CreateParameter("@sDocMonth", DbType.String, CommonData.DocMonth, ParameterDirection.Input);
                param[6] = objData.CreateParameter("@sLogBranchCode", DbType.String, sLogBranchCode, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("LevelGroupEcodeMapped_Get1", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objData = null;

            }
            return ds;
        }

        public DataSet LevelGroupMappedList_Get(string sCompCode, string sStateCode, string sBranchCode, int nECode, string sLogBranchCode)
        {
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();
            objData = new SQLDB();
            try
            {

                param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@sStateCode", DbType.String, sStateCode, ParameterDirection.Input);
                param[3] = objData.CreateParameter("@nDestEcode", DbType.Int32, nECode, ParameterDirection.Input);
                param[4] = objData.CreateParameter("@sDocMonth", DbType.String, CommonData.DocMonth, ParameterDirection.Input);
                param[5] = objData.CreateParameter("@sLogBranchCode", DbType.String, sLogBranchCode, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("LevelGroupMappedList_Get", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objData = null;

            }
            return ds;
        }

        public DataSet LevelAboveEcodeGroupMapped_Get(string sCompCode, string sStateCode, string sBranchCode, int nLevelId, int nECode, string sLogBranchCode)
        {
            SqlParameter[] param = new SqlParameter[7];
            DataSet ds = new DataSet();
            objData = new SQLDB();
            try
            {

                param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@sStateCode", DbType.String, sStateCode, ParameterDirection.Input);
                param[3] = objData.CreateParameter("@nLDMId", DbType.Int16, nLevelId, ParameterDirection.Input);
                param[4] = objData.CreateParameter("@nDestEcode", DbType.Int32, nECode, ParameterDirection.Input);
                param[5] = objData.CreateParameter("@sDocMonth", DbType.String, CommonData.DocMonth, ParameterDirection.Input);
                param[6] = objData.CreateParameter("@sLogBranchCode", DbType.String, sLogBranchCode, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("LevelAboveEcodeGroupMapped_Get", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objData = null;

            }
            return ds;
        }

        public Hashtable LevelMappedAboveGroup_Disply(string sCompCode, string sBranchCode)
        {
            DataSet ds = new DataSet();
            objData = new SQLDB();
            string strSQL = string.Empty;
            //SqlParameter[] param = new SqlParameter[3];
            Hashtable ht = new Hashtable();
            try
            {
                strSQL = " SELECT distinct ecode, MEMBER_NAME as ename FROM LevelsSource_Dest_map LSDM " +
                         " Inner Join EORA_MASTER EM on ecode = lsdm_dest_ecode " +
                         " AND company_code = lsdm_company_code AND branch_code = lsdm_branch_code " +
                         " WHERE lsdm_company_code='" + @sCompCode +
                         "' AND lsdm_branch_code='" + @sBranchCode +
                         "' AND lsdm_doc_month='" + CommonData.DocMonth + "'";
                DataTable dt0 = objData.ExecuteDataSet(strSQL, CommandType.Text).Tables[0];

                strSQL = " SELECT distinct ecode, lgm_source_ecode as gecode,  MEMBER_NAME as ename FROM LevelGroup_map LGM " +
                         " Inner Join EORA_MASTER EM on ecode = lgm_group_ecode " +
                         " AND company_code = LGM_company_code AND branch_code = lgm_branch_code " +
                         " WHERE lgm_company_code='" + @sCompCode +
                         "' AND lgm_branch_code='" + @sBranchCode +
                         "' AND lgm_doc_month='" + CommonData.DocMonth + "'";
                DataTable dt1 = objData.ExecuteDataSet(strSQL, CommandType.Text).Tables[0];

                strSQL = " SELECT distinct ecode, lgm_group_ecode as secode,  MEMBER_NAME as ename FROM LevelGroup_map LGM " +
                         " Inner Join EORA_MASTER EM on ecode = lgm_source_ecode " +
                         " AND company_code = LGM_company_code AND branch_code = lgm_branch_code " +
                         " WHERE lgm_company_code='" + @sCompCode +
                         "' AND lgm_branch_code='" + @sBranchCode +
                         "' AND lgm_doc_month='" + CommonData.DocMonth + "'";
                DataTable dt2 = objData.ExecuteDataSet(strSQL, CommandType.Text).Tables[0];

                ht.Add("MainNode", dt0);
                ht.Add("SubNode", dt1);
                ht.Add("SubSubNode", dt2);
                dt0 = null;
                dt1 = null;
                dt2 = null;
                //param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                //param[1] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                //param[2] = objData.CreateParameter("@sDocMonth", DbType.String, CommonData.DocMonth, ParameterDirection.Input);
                //ds = objData.ExecuteDataSet("LevelMappedAboveGroup_Disply", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                objData = null;
                //param = null;
            }
            return ht;
        }

        public DataSet LevelsAboveGroup_Get(string sCompCode, string sBranchCode, string sDocMonth)
        {
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            objData = new SQLDB();
            try
            {

                param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@sDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("LevelsAboveGroup_Get", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objData = null;

            }
            return ds;
        }

        public DataSet LevelEcodeMapped_Get(string sCompCode, string sBranchCode, string sDocMonth)
        {
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            objData = new SQLDB();
            try
            {

                param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@sDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("LevelEcodeMapped_Get", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objData = null;

            }
            return ds;
        }

        public DataSet LevelGroupOtherEcodeMapped_Get(string sCompCode, string sStateCode, string sBranchCode, int nLevelId, int nECode, string sLogBranchCode)
        {
            SqlParameter[] param = new SqlParameter[7];
            DataSet ds = new DataSet();
            objData = new SQLDB();
            try
            {

                param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@sStateCode", DbType.String, sStateCode, ParameterDirection.Input);
                param[3] = objData.CreateParameter("@nLDMId", DbType.Int16, nLevelId, ParameterDirection.Input);
                param[4] = objData.CreateParameter("@nDestEcode", DbType.Int32, nECode, ParameterDirection.Input);
                param[5] = objData.CreateParameter("@sDocMonth", DbType.String, CommonData.DocMonth, ParameterDirection.Input);
                param[6] = objData.CreateParameter("@sLogBranchCode", DbType.String, sLogBranchCode, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("LevelGroupOtherEcodeMapped_Get", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objData = null;

            }
            return ds;
        }

        public DataSet LevelMappedGroupList_Get(string sCompCode, string sStateCode, string sBranchCode)
        {
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            objData = new SQLDB();
            try
            {

                param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@sStateCode", DbType.String, sStateCode, ParameterDirection.Input);
                param[3] = objData.CreateParameter("@sDocMonth", DbType.String, CommonData.DocMonth, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("LevelMappedGroupList_Get", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objData = null;

            }
            return ds;
        }

        public DataSet LevelSourceDestMappedList_Get(string sCompCode, string sStateCode, string sBranchCode)
        {
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            objData = new SQLDB();
            try
            {

                param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@sStateCode", DbType.String, sStateCode, ParameterDirection.Input);
                param[3] = objData.CreateParameter("@sDocMonth", DbType.String, CommonData.DocMonth, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("LevelSourceDestMappedList_Get", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objData = null;

            }
            return ds;
        }

        public DataSet LevelGroupAboveSource_Proc(string sCompCode, string sStateCode, string sBranchCode, int nLevelId, int nECode, string sLogBranchCode)
        {
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            objData = new SQLDB();
            try
            {

                param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@sStateCode", DbType.String, sStateCode, ParameterDirection.Input);
                param[3] = objData.CreateParameter("@sDocMonth", DbType.String, CommonData.DocMonth, ParameterDirection.Input);
                param[4] = objData.CreateParameter("@sLogBranchCode", DbType.String, sLogBranchCode, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("LevelGroupAboveSource_Proc", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objData = null;

            }
            return ds;
        }

        public DataSet LevelGroupAboveDest_Proc(string sCompCode, string sStateCode, string sBranchCode, int nLevelId, int nECode, string sLogBranchCode)
        {
            SqlParameter[] param = new SqlParameter[7];
            DataSet ds = new DataSet();
            objData = new SQLDB();
            try
            {

                param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@sStateCode", DbType.String, sStateCode, ParameterDirection.Input);
                param[3] = objData.CreateParameter("@nLDMId", DbType.Int16, nLevelId, ParameterDirection.Input);
                param[4] = objData.CreateParameter("@nSourceEcode", DbType.Int32, nECode, ParameterDirection.Input);
                param[5] = objData.CreateParameter("@sDocMonth", DbType.String, CommonData.DocMonth, ParameterDirection.Input);
                param[6] = objData.CreateParameter("@sLogBranchCode", DbType.String, sLogBranchCode, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("LevelGroupAboveDest_Proc", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objData = null;

            }
            return ds;
        }

        public DataSet LevelGroupDetailEcodesMapped_Get(string sCompCode, string sStateCode, string sBranchCode, int nLevelId, int nECode, string sLogBranchCode)
        {
            SqlParameter[] param = new SqlParameter[7];
            DataSet ds = new DataSet();
            objData = new SQLDB();
            try
            {

                param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@sStateCode", DbType.String, sStateCode, ParameterDirection.Input);
                param[3] = objData.CreateParameter("@nLevelId", DbType.Int16, nLevelId, ParameterDirection.Input);
                param[4] = objData.CreateParameter("@nGroupEcode", DbType.Int32, nECode, ParameterDirection.Input);
                param[5] = objData.CreateParameter("@sDocMonth", DbType.String, CommonData.DocMonth, ParameterDirection.Input);
                param[6] = objData.CreateParameter("@sLogBranchCode", DbType.String, sLogBranchCode, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("LevelGroupDetailEcodesMapped_Get", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objData = null;

            }
            return ds;
        }

        public DataSet LevelCampBanchAboveEcodesMapped_Get(string sCompCode, string sStateCode, string sBranchCode, int nLevelId, int nECode, string sLogBranchCode)
        {
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();
            try
            {
                nLevelId = GetBranchHeadLevelId(sCompCode, sBranchCode);
                objData = new SQLDB();
                param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@sStateCode", DbType.String, sStateCode, ParameterDirection.Input);
                param[3] = objData.CreateParameter("@nLevelId", DbType.Int16, nLevelId, ParameterDirection.Input);
                param[4] = objData.CreateParameter("@nGroupEcode", DbType.Int32, nECode, ParameterDirection.Input);
                param[5] = objData.CreateParameter("@sDocMonth", DbType.String, CommonData.DocMonth, ParameterDirection.Input);

                ds = objData.ExecuteDataSet("LevelCampBanchAboveEcodesMapped_Get", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objData = null;

            }
            return ds;
        }

        public int GetBranchHeadLevelId(string sCompCode, string sBranchCode)
        {
            Int16 nLevelId = 0;
            objData = new SQLDB();
            try
            {
                DataTable dt = objData.ExecuteDataSet("Select isnull(elevel_id,0) from EORA_MASTER where COMPANY_CODE='" + sCompCode + "' and BRANCH_CODE='" + sBranchCode + "' and ECODE in(SELECT ISNULL(BRANCH_HEAD_ECODE, 0) FROM BRANCH_MAS where COMPANY_CODE='" + sCompCode + "' and BRANCH_CODE='" + sBranchCode + "')").Tables[0];

                if (dt.Rows.Count > 0)
                    nLevelId = Convert.ToInt16(dt.Rows[0][0].ToString());
                else
                    nLevelId = 65;

                if (nLevelId <= 0)
                    nLevelId = 65;

                dt = null;

              
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                
                objData = null;

            }
            return nLevelId;
        }

        public string CheckEcodeUsedInInvoice(string strEcodes)
        {
            string strValue = string.Empty;
            objData = new SQLDB();
            try
            {
                string sqlSel = " SELECT  SIH_EORA_CODE ECODE FROM SALES_INV_HEAD " +
                                " WHERE SIH_COMPANY_CODE ='" + CommonData.CompanyCode + "' AND SIH_BRANCH_CODE='" + CommonData.BranchCode + "' AND SIH_FIN_YEAR='" + CommonData.FinancialYear + "' AND SIH_DOCUMENT_MONTH='" + CommonData.DocMonth + "' AND SIH_EORA_CODE IN (" + strEcodes + ") " +
                                " UNION  " +
                                " SELECT SIBH_EORA_CODE ECODE FROM SALES_INV_BULTIN_HEAD " +
                                " WHERE SIBH_COMPANY_CODE ='" + CommonData.CompanyCode + "' AND SIBH_BRANCH_CODE='" + CommonData.BranchCode + "' AND SIBH_FIN_YEAR='" + CommonData.FinancialYear + "' AND SIBH_DOCUMENT_MONTH='" + CommonData.DocMonth + "' AND SIBH_EORA_CODE IN (" + strEcodes + ")";

               DataTable dt = objData.ExecuteDataSet(sqlSel, CommandType.Text).Tables[0];
               if (dt.Rows.Count > 0)
               {
                   foreach (DataRow dataRow in dt.Rows)
                       strValue += dataRow[0].ToString() + ",";

                   strValue = strValue.Substring(0, strValue.Length - 1);
               }

                dt = null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                objData = null;
            }
            return strValue;
        }

        public DataSet LevelCampList_Get(string sActive)
        {
            DataSet ds = new DataSet();
            objData = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            try
            {
                param[0] = objData.CreateParameter("@sCompCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@sActive", DbType.String, sActive, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("LevelCampList_Get", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                objData = null;
                param = null;

            }
            return ds;
        }

        public DataSet LevelGroupAboveBranchList(string sCompCode, string sStateCode, string sBranchCode)
        {
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                int nLevelId = GetBranchHeadLevelId(sCompCode, sBranchCode);
                objData = new SQLDB();
                param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@sStateCode", DbType.String, sStateCode, ParameterDirection.Input);
                param[3] = objData.CreateParameter("@sDocMonth", DbType.String, CommonData.DocMonth, ParameterDirection.Input);
                
                ds = objData.ExecuteDataSet("LevelGroupAboveBranchList", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objData = null;

            }
            return ds;
        }
        public DataSet LevelGroupAboveBranchListGet(string sCompCode, string sStateCode, string sBranchCode, string br)
        {
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {
                //int nLevelId = GetBranchHeadLevelId(sCompCode, sBranchCode);
                objData = new SQLDB();
                param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@sStateCode", DbType.String, sStateCode, ParameterDirection.Input);
                //param[3] = objData.CreateParameter("@sDocMonth", DbType.String, CommonData.DocMonth, ParameterDirection.Input);
                //param[4] = objData.CreateParameter("@nELevelId", DbType.Int16, nLevelId, ParameterDirection.Input);

                ds = objData.ExecuteDataSet("LevelGroupAboveBranchList", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objData = null;

            }
            return ds;
        }

        public DataSet AboveBranchLeaders_InQ_Get(string sDocMonth, string sEcode)
        {
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            objData = new SQLDB();
            try
            {

                param[0] = objData.CreateParameter("@xDoc_Month", DbType.String, sDocMonth, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@xABLeader", DbType.String, sEcode, ParameterDirection.Input);                
                ds = objData.ExecuteDataSet("AboveBranchLeaders_InQ", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objData = null;

            }
            return ds;
        }
      
}

    public class NewCheckboxListItem
    {
        // define a text and 
        // a tag value

        public string Text;
        public string Tag;

        // override ToString(); this
        // is what the checkbox control
        // displays as text
        public override string ToString()
        {
            return this.Text;
        }
    }

 }

    

    


