using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Cryptography;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace SSCRMDB
{
    public class Security
    {
        private SQLDB objSQLdb = null;

        public DataSet GetCompanyDataSet()
        {
            objSQLdb = new SQLDB();
            DataSet ds = new DataSet();
            try
            {

                ds = objSQLdb.ExecuteDataSet("CompanyMaster_Proc", CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                objSQLdb = null;

            }
            return ds;
        }

        public DataSet GetSingleProductsDataSet()
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SingleProductMasAll_Get", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                objSQLdb = null;

            }
            return ds;
        }

        public DataSet UserBranchList(string strCompanCode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, strCompanCode, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("UserBranchMaster_Get", CommandType.StoredProcedure, param);

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

        public DataTable dtCompanyDocumentMonth()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {

               dt = objSQLdb.ExecuteDataSet("SELECT fin_year+'@'+cast(start_date as varchar)+'@'+ cast(end_date as varchar) AS FinYear, document_month AS DocMonth FROM document_month where company_code='" + CommonData.CompanyCode + "' And Status='R' ORDER BY start_date desc", CommandType.Text).Tables[0];
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                objSQLdb = null;

            }
            return dt;
        }

        public Hashtable GetCompanyDocumentMonth()
        {
            objSQLdb = new SQLDB();
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {

                ds = objSQLdb.ExecuteDataSet("select *, getdate() as CurDate from document_month where company_code='" + CommonData.CompanyCode.ToString() + "' And Status='R'", CommandType.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ht.Add("DocMonth", ds.Tables[0].Rows[0]["document_month"]);
                    ht.Add("DocFDate", ds.Tables[0].Rows[0]["start_date"]);
                    ht.Add("DocTDate", ds.Tables[0].Rows[0]["end_date"]);
                    ht.Add("FYear", ds.Tables[0].Rows[0]["fin_year"]);
                    ht.Add("CurDate", ds.Tables[0].Rows[0]["CurDate"]);

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                ds = null;

            }
            return ht;
        }

        #region Code for Security
        public DataTable UserLogin(string sUserId, string sPwd)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            string str = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                string sEcode = GetEncodeString(sPwd);
                //string sDcode = GetDecodeString(sEcode);
                param[0] = objSQLdb.CreateParameter("@sUSERID", DbType.String, sUserId, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sPASSWORD", DbType.String, sEcode, ParameterDirection.Input);
                dt = objSQLdb.ExecuteDataSet("GetUserLogin_Proc", CommandType.StoredProcedure, param).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    CommonData.CompanyCode = dt.Rows[0]["CompanyCode"].ToString();
                    CommonData.CompanyName = dt.Rows[0]["CompanyName"].ToString();
                    CommonData.BranchCode = dt.Rows[0]["BranchCode"].ToString();
                    CommonData.BranchName = dt.Rows[0]["BranchName"].ToString();
                    CommonData.StateCode = dt.Rows[0]["StateCode"].ToString();
                    CommonData.StateName = dt.Rows[0]["StateName"].ToString();
                    CommonData.BranchType = dt.Rows[0]["BranchType"].ToString();
                    CommonData.BranchLevelId = Convert.ToInt32(dt.Rows[0]["BranchLevelId"]+"");
                    CommonData.LogUserId = sUserId;
                    CommonData.LogUserEcode = dt.Rows[0]["UserEcode"] + "";
                    CommonData.LogUserRole = dt.Rows[0]["UserRole"] + "";
                    CommonData.LogUserBackDays = Convert.ToInt16(dt.Rows[0]["UserBackDays"] + "");
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
            return dt;

        }

        public bool CanModifyDataUserAsPerBackDays(DateTime dtDate)
        {
            bool blModf = false;
            if (Convert.ToInt16(CommonData.LogUserBackDays) >= 0)
            {
                dtDate = dtDate.AddDays(Convert.ToInt16(CommonData.LogUserBackDays));
                if (dtDate >= Convert.ToDateTime(Convert.ToDateTime(CommonData.DocTDate).ToString("dd/MM/yyyy")))
                    blModf = true;
            }
            else
                blModf = true;

            return blModf;
        }

        
        /// <summary>
        /// 
        /// </remarks>
        public string GetEncodeString(string sVal)
        {
            byte[] sB = Encoding.Default.GetBytes(sVal);
            string strVal = Convert.ToBase64String(sB);
            return strVal;
        }

        /// <summary>
        /// 
        /// </remarks>
        public string GetDecodeString(string sVal)
        {
            byte[] sB = Convert.FromBase64String(sVal);
            string strVal = Encoding.Default.GetString(sB);
            return strVal;
        }

        #endregion
    }
}
