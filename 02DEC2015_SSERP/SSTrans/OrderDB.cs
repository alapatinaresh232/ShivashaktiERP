using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SSCRMDB;
using System.Collections;
namespace SSTrans
{
    public class OrderDB
    {
        private SQLDB objSQLdb = null;
        public OrderDB()
        {
        }
        public Hashtable GetSalseOrderData(string strSCode, string strBCode, string DocMonth,string sOrdNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, strSCode.ToString(), ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear.ToString(), ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, DocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sOrder", DbType.String, sOrdNo, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@Imode", DbType.Int32, 101, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetSalseOrderHeadANDDetails", CommandType.StoredProcedure, param);
                ht.Add("OrdHead", ds.Tables[0]);
                ht.Add("REFUND", ds.Tables[1]);
                ds = null;
                param = null;
                objSQLdb = null;
                objSQLdb = new SQLDB();
                param = new SqlParameter[6];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode.ToString(), ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear.ToString(), ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, DocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sOrder", DbType.String, sOrdNo, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@Imode", DbType.Int32, 102, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetSalseOrderHeadANDDetails", CommandType.StoredProcedure, param);
                ht.Add("OrdDetail", ds.Tables[0]);
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

        public Hashtable GetSalseOrderDataByARNumber(string strSCode, string strBCode, string DocMonth, string sARNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, strSCode.ToString(), ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear.ToString(), ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, DocMonth, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sOrder", DbType.String, sARNo, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@Imode", DbType.Int32, 104, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetSalseOrderHeadANDDetails", CommandType.StoredProcedure, param);
                ht.Add("OrdHead", ds.Tables[0]);
                ht.Add("REFUND", ds.Tables[1]);
                ds = null;
                param = null;
                objSQLdb = null;
                objSQLdb = new SQLDB();
                param = new SqlParameter[6];
                ds = new DataSet();
                DataTable dtInvH = (DataTable)ht["OrdHead"];
                if (dtInvH.Rows.Count > 0)
                {
                    param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode.ToString(), ParameterDirection.Input);
                    param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBCode, ParameterDirection.Input);
                    param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear.ToString(), ParameterDirection.Input);
                    param[3] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, DocMonth, ParameterDirection.Input);
                    param[4] = objSQLdb.CreateParameter("@sOrder", DbType.String, dtInvH.Rows[0]["soh_order_number"].ToString(), ParameterDirection.Input);
                    param[5] = objSQLdb.CreateParameter("@Imode", DbType.Int32, 102, ParameterDirection.Input);
                    ds = objSQLdb.ExecuteDataSet("GetSalseOrderHeadANDDetails", CommandType.StoredProcedure, param);
                    ht.Add("OrdDetail", ds.Tables[0]);
                    dtInvH = null;
                }
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

        public DataSet SalseOrderSearch_Get(string sCustName, string sVillage, string sMandal, string sState)
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
                ds = objSQLdb.ExecuteDataSet("GetSalseOrderHeadANDDetails", CommandType.StoredProcedure, param);
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
        public bool CheckOrderNo(string sOrdNo)
        {
            objSQLdb = new SQLDB();
            bool blTrue = false;
            try
            {
                string sqlSel = " SELECT ORDNO  FROM CheckInvoiceOrderNumber_View " +
                                " WHERE COMPANY_CODE ='" + CommonData.CompanyCode +
                                "' AND BRANCH_CODE='" + CommonData.BranchCode +
                                "' AND FIN_YEAR='" + CommonData.FinancialYear +
                                "' AND ORDNO='" + sOrdNo  + "'";

                DataTable dt = objSQLdb.ExecuteDataSet(sqlSel, CommandType.Text).Tables[0];
                if (dt.Rows.Count > 0)
                    blTrue = true;

                dt = null;
            }
            catch (Exception ex)
            {
                blTrue = true;
                throw new Exception(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
            }
            return blTrue;
        }
    }
}
