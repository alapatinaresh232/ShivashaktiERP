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
    public class UserTaskDB
    {
        private SQLDB objData = null;
        #region Constructor
        public UserTaskDB()
        {

        }
        #endregion

        public DataSet UserTasks_Get(string sUserID)
        {
            DataSet ds = new DataSet();
            objData = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            try
            {
                param[0] = objData.CreateParameter("@sUserId", DbType.String, sUserID, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                
                ds = objData.ExecuteDataSet("UserTasks_Get", CommandType.StoredProcedure, param);

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

        public DataSet Get_DMSUserTasks(string sUserID, string sCompCode)
        {
            DataSet ds = new DataSet();
            objData = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            try
            {
                param[0] = objData.CreateParameter("@sUserId", DbType.String, sUserID, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);

                ds = objData.ExecuteDataSet("DMS_UserTasks_Get", CommandType.StoredProcedure, param);

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

        public int GenerateFeedBackTranNo()
        {
            objData = new SQLDB();
            int trnNo = 1;
            try
            {
                string strQry = "SELECT  ISNULL(MAX(CF_TRN_NUMBER),0) + 1 FBNO " +
                                " FROM CRM_FEEDBACK WHERE CF_COMPANY_CODE ='" + CommonData.CompanyCode + "' AND CF_BRANCH_CODE ='" + CommonData.BranchCode + "' AND CF_FIN_YEAR='" + CommonData.FinancialYear + "' ";

                DataTable dt = objData.ExecuteDataSet(strQry, CommandType.Text).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    trnNo = Convert.ToInt32(dt.Rows[0][0].ToString());
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
            return trnNo;
        }

        public DataSet UserFeedBack_Get(string sType)
        {
            DataSet ds = new DataSet();
            objData = new SQLDB();
            try
            {
                string strQry = "SELECT  CF_TRN_TYPE FBTYPE, CF_TRN_NUMBER FBNO, CF_TRN_DATE EntryDate, CF_TRN_REM FeedBack, CF_CREATED_BY USERID " +
                                " FROM CRM_FEEDBACK WHERE CF_COMPANY_CODE ='" + CommonData.CompanyCode + "' AND CF_BRANCH_CODE ='" + CommonData.BranchCode + "' AND CF_FIN_YEAR='" + CommonData.FinancialYear + "' AND CF_TRN_TYPE='" + sType + "' AND CF_CREATED_BY ='" + CommonData.LogUserId + "' " +
                                " ORDER BY CF_TRN_NUMBER";

                ds = objData.ExecuteDataSet(strQry, CommandType.Text);

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
    }
}
