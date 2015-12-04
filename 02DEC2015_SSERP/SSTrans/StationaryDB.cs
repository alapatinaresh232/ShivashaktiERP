using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SSCRMDB;
using System.Data.SqlClient;
namespace SSTrans
{
   public  class StationaryDB
    {
       SQLDB objSQLdb;
       public DataSet GetDCsList_ForBranchGRN(string strCompCode, string strBranchCode, string strDocMonth)
       {
           objSQLdb = new SQLDB();
           SqlParameter[] param = new SqlParameter[3];
           DataSet ds = new DataSet();
           try
           {
               param[0] = objSQLdb.CreateParameter("@xCompanyCode", DbType.String, strCompCode, ParameterDirection.Input);
               param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
               param[2] = objSQLdb.CreateParameter("@xFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
               ds = objSQLdb.ExecuteDataSet("ST_DCsList_ForBranchGRN", CommandType.StoredProcedure, param);

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
       public DataSet Get_ST_DC_Data( string strBranchCode, string sFinYear, string DCValue)
       {
           objSQLdb = new SQLDB();
           SqlParameter[] param = new SqlParameter[3];
           DataSet ds = new DataSet();
           try
           {
              
               param[0] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
               param[1] = objSQLdb.CreateParameter("@xFinYear", DbType.String, sFinYear, ParameterDirection.Input);
               param[2] = objSQLdb.CreateParameter("@xDCNo", DbType.String, DCValue, ParameterDirection.Input);
               ds = objSQLdb.ExecuteDataSet("Get_ST_DC_Data", CommandType.StoredProcedure, param);
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

       public DataSet Get_BR_StationaryIssueData_ByTrnNo(string strCompCode, string strBranchCode, string sFinYear, string sTrnNo)
       {
           objSQLdb = new SQLDB();
           SqlParameter[] param = new SqlParameter[4];
           DataSet ds = new DataSet();
           try
           {
               param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, strCompCode, ParameterDirection.Input);
               param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
               param[2] = objSQLdb.CreateParameter("@xFinYear", DbType.String, sFinYear, ParameterDirection.Input);
               param[3] = objSQLdb.CreateParameter("@xTrnNo", DbType.String, sTrnNo, ParameterDirection.Input);
               ds = objSQLdb.ExecuteDataSet("Get_BR_StationaryIssueData_ByTrnNo", CommandType.StoredProcedure, param);
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

       public DataSet GetSelfDCsList_ForBranchGRN(string strCompCode, string strBranchCode, string strDocMonth, Int32 Ecode)
       {
           objSQLdb = new SQLDB();
           SqlParameter[] param = new SqlParameter[4];
           DataSet ds = new DataSet();
           try
           {
               param[0] = objSQLdb.CreateParameter("@xCompanyCode", DbType.String, strCompCode, ParameterDirection.Input);
               param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
               param[2] = objSQLdb.CreateParameter("@xFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
               param[3] = objSQLdb.CreateParameter("@xECode", DbType.Int32, Ecode, ParameterDirection.Input);
               ds = objSQLdb.ExecuteDataSet("ST_SelfDCsList_ForBranchGRN", CommandType.StoredProcedure, param);

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
