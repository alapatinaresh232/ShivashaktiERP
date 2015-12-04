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
    public class Master
    {
        private SQLDB objData = null;
        private Security objSecurity = null;
        #region Constructor
        public Master()
        {

        }
        #endregion

        public DataSet GetBranchesDS(string sStateCoe, string sBranchType, string sBranchCode, string sActive, string sCompCode)
        {
            DataSet ds = new DataSet();
            objData = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            try
            {
                param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sStateCode", DbType.String, sStateCoe, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@sBranchType", DbType.String, sBranchType, ParameterDirection.Input);
                param[3] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[4] = objData.CreateParameter("@sActive", DbType.String, sActive, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("GetBranches_Proc", CommandType.StoredProcedure, param);

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

        public DataSet BranchOutlet_Get(string sBranchType, string sBranchCode, string sActive, string sCompCode)
        {
            DataSet ds = new DataSet();
            objData = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            try
            {
                param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBranchType", DbType.String, sBranchType, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[3] = objData.CreateParameter("@sActive", DbType.String, sActive, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("BranchOutlet_Get", CommandType.StoredProcedure, param);

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

        public DataSet BranchCamp_Get(string sActive)
        {
            DataSet ds = new DataSet();
            objData = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            try
            {
                param[0] = objData.CreateParameter("@sCompCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@sActive", DbType.String, sActive, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("BranchCamp_Get", CommandType.StoredProcedure, param);

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
        
        public DataSet GetBranchDataSet(string strCompanCode)
        {
            objData = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objData.CreateParameter("@sCompCode", DbType.String, strCompanCode, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("BranchMaster_Proc", CommandType.StoredProcedure, param);

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

        public DataSet GetLogicalBranchData(string sLogBCode, char cActive)
        {
            objData = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objData.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sLogBranchCode", DbType.String, sLogBCode, ParameterDirection.Input);
                param[2] = objData.CreateParameter("@sActive", DbType.String, cActive, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("LogicalBranchList_Proc", CommandType.StoredProcedure, param);

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

        public DataSet GetProductCategory()
        {
            objData = new SQLDB();
            DataSet ds = new DataSet();
            try
            {

                ds = objData.ExecuteDataSet("Select CATEGORY_ID, CATEGORY_NAME from  CATEGORY_MASTER Order By CATEGORY_NAME", CommandType.Text);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                

            }
            return ds;
        }

        public DataSet GetPromotionTypes()
        {
            objData = new SQLDB();
            DataSet ds = new DataSet();
            try
            {

                ds = objData.ExecuteDataSet("SELECT HPCM_PROMOTION_CATEGORY_CODE,HPCM_PROMOTION_CATEGORY_NAME FROM HR_PROMOTION_CATEGORY_MASTER", CommandType.Text);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {


            }
            return ds;
        }

        public DataSet Get_PUProductCategory(string sComp, string sBran)
        {
            objData = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objData.CreateParameter("@xCompanyCode", DbType.String, sComp, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@xBranchCode", DbType.String, sBran, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("Get_PUProductCategory", CommandType.StoredProcedure, param);

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

        public DataSet GetEmployeeDataSet()
        {
            objData = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objData.CreateParameter("@sCompCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("EmployeeMaster_Proc", CommandType.StoredProcedure, param);

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

        public DataSet UserEcodeList_Get(string sCompCode, string sBranchCode)
        {
            objData = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("UserEcodeMaster_Get", CommandType.StoredProcedure, param);

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

        public DataSet UserBranchList_Get(string strCompanCode)
        {
            objData = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objData.CreateParameter("@sCompCode", DbType.String, strCompanCode, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("UserBranchMaster_Get", CommandType.StoredProcedure, param);

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

        public DataSet UserMasterList_Get(string sCompCode, string sBranchCode)
        {
            objData = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objData.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("UserMasterList_Get", CommandType.StoredProcedure, param);

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

        public DataSet UserUserIdCheck_Get(string sUserId, string sEcode)
        {
            objData = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objData.CreateParameter("@sUserId", DbType.String, sUserId, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@nECODE", DbType.Int32, Convert.ToInt32(sEcode), ParameterDirection.Input);
                ds = objData.ExecuteDataSet("UserUserIdCheck_Get", CommandType.StoredProcedure, param);

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
        public DataSet CustomerData_Get(string sCustomerId)
        {
            objData = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objData.CreateParameter("@sSearch", DbType.String, DBNull.Value, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sCustomerId", DbType.String, sCustomerId, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("CustomerData_Get", CommandType.StoredProcedure, param);

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

        public DataSet CustomerData_Get(string sCustomerId, string sCustomerName)
        {
            objData = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objData.CreateParameter("@sSearch", DbType.String, sCustomerName, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@sCustomerId", DbType.String, sCustomerId, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("CustomerData_Get", CommandType.StoredProcedure, param);

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

        public int SaveCompany(string CompanyCode, string Companyname, string Active, byte[] sLogo)
        {
            int iretuval = 0;
            try
            {
                string sConnVal = ConfigurationSettings.AppSettings["DBCon"].ToString();
                objSecurity = new Security();
                SqlConnection CN = new SqlConnection(objSecurity.GetDecodeString(sConnVal));
                string qry = "";
                objData = new SQLDB();
                DataSet dsCnt = objData.ExecuteDataSet("select * from COMPANY_MAS WHERE CM_COMPANY_CODE='" + CompanyCode + "'");
                if (dsCnt.Tables[0].Rows.Count == 0)
                {
                    qry = "INSERT INTO COMPANY_MAS (CM_COMPANY_CODE,CM_COMPANY_NAME,ACTIVE,CM_LOGO) values ('" + CompanyCode + "','" + Companyname + "','" + Active + "',@Logo)";
                }
                else
                {
                    if (sLogo.Length > 1)
                        qry = "UPDATE COMPANY_MAS SET CM_COMPANY_CODE='" + CompanyCode + "',CM_COMPANY_NAME='" + Companyname + "',ACTIVE='" + Active + "',CM_LOGO=@Logo WHERE CM_COMPANY_CODE='" + CompanyCode + "'";
                    else
                        qry = "UPDATE COMPANY_MAS SET CM_COMPANY_CODE='" + CompanyCode + "',CM_COMPANY_NAME='" + Companyname + "',ACTIVE='" + Active + "' WHERE CM_COMPANY_CODE='" + CompanyCode + "'";
                }
                SqlCommand SqlCom = new SqlCommand(qry, CN);
                if (sLogo.Length > 1)
                    SqlCom.Parameters.Add(new SqlParameter("@Logo", (object)sLogo));
                CN.Open();
                iretuval=SqlCom.ExecuteNonQuery();
                CN.Close();
                return iretuval;
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

        public DataSet GetEmployeeMasterDetl(string sEcode)
        {
            DataSet ds = new DataSet();
            objData = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            try
            {
                param[0] = objData.CreateParameter("@xEcode", DbType.String, sEcode, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("Get_EmployeeMaster_Detl", CommandType.StoredProcedure, param);

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

        public DataSet GetHRSalStructureTypes()
        {
            DataSet ds = new DataSet();
            objData = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            try
            {
                param[0] = objData.CreateParameter("@xSalStrType", DbType.String, "", ParameterDirection.Input);
                param[1] = objData.CreateParameter("@xSalStrCode", DbType.String, "", ParameterDirection.Input);
                ds = objData.ExecuteDataSet("GetHRSalStructureTypes", CommandType.StoredProcedure, param);
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
        public DataSet GetHRSalStructureTypes(string sSalType, string sSalCode)
        {
            DataSet ds = new DataSet();
            objData = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            try
            {
                param[0] = objData.CreateParameter("@xSalStrType", DbType.String, sSalType, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@xSalStrCode", DbType.String, sSalCode, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("GetHRSalStructureTypes", CommandType.StoredProcedure, param);
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

        public DataSet GetSPPUBranchListDataSet(string strCompCode)
        {
            objData = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objData.CreateParameter("@sCompCode", DbType.String, strCompCode, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("SPPUBranchMaster_Proc", CommandType.StoredProcedure, param);

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

        public DataSet GetSPPUBranchDataSet(string strCompCode)
        {
            objData = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objData.CreateParameter("@sCompCode", DbType.String, strCompCode, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("SPPUBranchMaster_Proc", CommandType.StoredProcedure, param);

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
        public DataSet GetComplainantMasterDetl(string sSearch)
        {
            DataSet ds = new DataSet();
            objData = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            try
            {
                param[0] = objData.CreateParameter("@xComplainantName", DbType.String, sSearch, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("Get_ComplainantMaster_Detl", CommandType.StoredProcedure, param);
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
        public DataSet GetLawyerMasterDetl(string sSearch)
        {
            DataSet ds = new DataSet();
            objData = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            try
            {
                param[0] = objData.CreateParameter("@xLawyerName", DbType.String, sSearch, ParameterDirection.Input);
                ds = objData.ExecuteDataSet("Get_LawyerMaster_Detl", CommandType.StoredProcedure, param);
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
    }
}
