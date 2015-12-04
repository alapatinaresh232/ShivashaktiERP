using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SSCRMDB;
using System.Data.SqlClient;
using System.Collections;

namespace SSTrans
{
    public class DnkDetailsDB
    {
        private SQLDB objSQLdb = null;
        
        #region Constructor
        public DnkDetailsDB()
        {          
            
        }
        #endregion
        public Hashtable GetDNKHeadDetailsData(string strSCode, string strBCode, int nInvoiceNo,string DocMonth)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode.ToString(), ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear.ToString(), ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nTNRNo", DbType.Int32, nInvoiceNo, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, DocMonth, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@Imode", DbType.Int32, 101, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetDNKHeadANDDetails", CommandType.StoredProcedure, param);
                ht.Add("DNKHead", ds.Tables[0]);                
                ds = null;
                param = null;
                objSQLdb = null;
                objSQLdb = new SQLDB();
                param = new SqlParameter[6];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode.ToString(), ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear.ToString(), ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nTNRNo", DbType.Int32, nInvoiceNo, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, DocMonth, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@Imode", DbType.Int32, 102, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetDNKHeadANDDetails", CommandType.StoredProcedure, param);
                ht.Add("DNKDetail", ds.Tables[0]);
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
        
        public DataSet DNKSearch_Get(string sCustName, string sVillage, string sMandal, string sState)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@Imode", DbType.Int32, 103, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sfname", DbType.String, sCustName, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sVillage", DbType.String, sVillage, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sMandal", DbType.String, sMandal, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sState", DbType.String, sState, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetDNKHeadANDDetails", CommandType.StoredProcedure, param);
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
