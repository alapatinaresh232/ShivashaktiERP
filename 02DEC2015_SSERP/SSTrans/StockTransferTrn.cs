using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SSCRMDB;
using System.Data.SqlClient;
namespace SSTrans
{
    public class StockTransferTrn
    {
        private SQLDB objSQLdb = null;
        public string GetMaxTransNo(string CompanyCode, string BranchCode, string FinYear, string strType)
        {
            objSQLdb = new SQLDB();
            string sqlSel = "SELECT ISNULL(MAX(SIH_TRN_NUMBER),0)+1 FROM STK_INTR_HEAD WHERE SIH_COMPANY_CODE='" + CompanyCode + "' AND SIH_BRANCH_CODE='" + BranchCode + "' AND SIH_FIN_YEAR='" + FinYear + "' AND SIH_TRN_TYPE='" + strType + "'";
            return objSQLdb.ExecuteDataSet(sqlSel, CommandType.Text).Tables[0].Rows[0][0].ToString();
        }
        public DataSet GetStockTrnsfer(int ECode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xEcode", DbType.String, ECode, ParameterDirection.Input);
                //param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                //param[2] = objSQLdb.CreateParameter("@sStateCode", DbType.String, CommonData.StateCode, ParameterDirection.Input);
                //param[3] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("sscrm_stk_ecode", CommandType.StoredProcedure, param);

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
        public DataSet GetStockTransferDetails(string CompanyCode, string BranchCode, string FinYear, int TrnNo)
        {
            string sqlSel = " SELECT *FROM STK_INTR_HEAD WHERE SIH_COMPANY_CODE='" + CompanyCode + "' and SIH_BRANCH_CODE='" + BranchCode + "' and SIH_FIN_YEAR='" + FinYear + "' and SIH_TRN_NUMBER=" + TrnNo;
            sqlSel += "SELECT A.*,B.PM_PRODUCT_NAME FROM STK_INTR_DETL A INNER JOIN PRODUCT_MAS B ON A.SID_PRODUCT_ID=B.PM_PRODUCT_ID WHERE SID_COMPANY_CODE='" + CompanyCode + "' and SID_BRANCH_CODE='" + BranchCode + "' and SID_FIN_YEAR='" + FinYear + "' and SID_TRN_NUMBER=" + TrnNo;
            objSQLdb = new SQLDB();            
            return objSQLdb.ExecuteDataSet(sqlSel, CommandType.Text);
        }
    }
}
