using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SSCRMDB;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

namespace SSTrans
{
   public class LegalInfoDB
    {
        private SQLDB objSQLdb = null;
        public DataTable getLegalCaseDetails(string sCompCode,string sBCode,string sCaseType,string sCaseNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataTable dt = new DataTable();
            DataTable dtDtl = new DataTable();
          
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBCode", DbType.String, sBCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xCaseType", DbType.String, sCaseType, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xCaseNo", DbType.String, sCaseNo, ParameterDirection.Input);
                dt = objSQLdb.ExecuteDataSet("GetLegalCaseDetails", CommandType.StoredProcedure, param).Tables[0];
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
            return dt;
        }
        public DataTable getCaseHearingDetails(string sCompCode, string sBCode, string sCaseType,string sCaseDate, string sCaseNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataTable dtDtl = new DataTable();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBCode", DbType.String, sBCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xCasType", DbType.String, sCaseType, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xCaseDate", DbType.String, sCaseDate, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xCaseNumber", DbType.String, sCaseNo, ParameterDirection.Input);
                dtDtl = objSQLdb.ExecuteDataSet("GetLegalCaseHearingDetails", CommandType.StoredProcedure, param).Tables[0];
              
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
            return dtDtl;
        }
        public DataTable getCaseHearingDetailsMaster(string sCompCode, string sBCode, string sCaseType, string sCaseDate, string sCaseNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataTable dtDtl = new DataTable();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBCode", DbType.String, sBCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xCasType", DbType.String, sCaseType, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xCaseDate", DbType.String, sCaseDate, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xCaseNumber", DbType.String, sCaseNo, ParameterDirection.Input);
                dtDtl = objSQLdb.ExecuteDataSet("GelLegalCaseHearingsForMaster", CommandType.StoredProcedure, param).Tables[0];

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
            return dtDtl;
        }
    }
}
