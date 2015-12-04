using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SSCRMDB;
using System.Data.SqlClient;
using System.Collections;

namespace SSTrans
{
    public class IndentDB
    {
        private SQLDB objSQLdb = null;

        #region Constructor
        public IndentDB()
        {          
            
        }
        #endregion

        public DataSet IndentGroupList_Get(string sCompCode, string sBranchCode, string sDocMonth)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("IndentGroupList_Get", CommandType.StoredProcedure, param);

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
        public DataSet Get_ProductLicenceDetails(string sCompanyCode, string sStateCode, Int32 sDistrict, string sStatus)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompany", DbType.String, sCompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sState", DbType.String, sStateCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDistrict", DbType.Int32, sDistrict, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xStatus", DbType.String, sStatus, ParameterDirection.Input);
                //param[4] = objSQLdb.CreateParameter("@sRepType", DbType.String, ParameterDirection.Output);


                ds = objSQLdb.ExecuteDataSet("Get_ProductLicenceDetails", CommandType.StoredProcedure, param);

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
        public DataSet get_HolidaysList(Int32 intYear)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xYear ", DbType.Int32, intYear, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("get_HolidaysList", CommandType.StoredProcedure, param);

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

        public DataSet IndentStockPointList_Get(string strCompanCode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, strCompanCode, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("IndentStockPointList_Get", CommandType.StoredProcedure, param);

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

        public DataSet DCOtherEmployeeList_Get()
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("DCOtherEmployeeList_Get", CommandType.StoredProcedure, param);

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
                
        public DataSet IndentGroupEcodeList_Get(string sCompCode, string sBranchCode, string sDocMonth, int intEcode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            string ecode = intEcode + "%";
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nGroupEcode", DbType.String, ecode, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("IndentGroupEcodeList_Get", CommandType.StoredProcedure, param);

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

        public DataSet IndentProductCategoryList_GET()
        {
            objSQLdb = new SQLDB();
            DataSet ds = new DataSet();
            try
            {

                ds = objSQLdb.ExecuteDataSet("IndentProductCategoryList_GET", CommandType.StoredProcedure);

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

        public DataSet IndentProductList_Get(string strCompCode,string strBranchCode, string strCategoryId)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, strCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sCategoryId", DbType.String, strCategoryId, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("IndentProductList_Get", CommandType.StoredProcedure, param);

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
        public DataSet IndentDCSTProductList_Get(string strCompCode, string strBranchCode, string strCategoryId)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, strCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sCategoryId", DbType.String, strCategoryId, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("IndentDCSTProductList_Get", CommandType.StoredProcedure, param);

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

        public DataSet IndentProductDetail_Get(string sCompCode, string sBranchCode,  Int32 intIndent)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nIndent", DbType.Int32, intIndent, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("IndentProductDetail_Get", CommandType.StoredProcedure, param);

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

        public DataSet IndentList_Get(string sCompCode, string sBranchCode, Int32 intGLEcode, string sStockPointCode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nGLEcode", DbType.Int32, intGLEcode, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sStockPointCode", DbType.String, sStockPointCode, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("IndentList_Get", CommandType.StoredProcedure, param);

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

        public DataSet DeliveryChallanIndentList_Get(string sCompCode, string sBranchCode, string sIndentFromCode, Int32 intGLEcode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sIndentFromCode", DbType.String, sIndentFromCode, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@nGLEcode", DbType.Int32, intGLEcode, ParameterDirection.Input);
                
                ds = objSQLdb.ExecuteDataSet("DeliveryChallanIndentList_Get", CommandType.StoredProcedure, param);

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

        public DataSet BranchListForDC_Get(string strCompanCode, string strBrType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {
             
                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, strCompanCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchType", DbType.String, strBrType, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("BranchListForDC_Get", CommandType.StoredProcedure, param);

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

        public DataSet IndentFromBrnchList_Get(string sCompCode, string sBranchCode, Int32 intGLEcode, string sStockPointCode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nGLEcode", DbType.Int32, intGLEcode, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sStockPointCode", DbType.String, sStockPointCode, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("IndentFromBrnchList_Get", CommandType.StoredProcedure, param);

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

        public string GenerateNewDCTranNo(string strCompCode, string strBranchCode, string strTrnType)
        {
            objSQLdb = new SQLDB();
            string strNewNo = string.Empty;
            string sCompCode = CommonData.CompanyCode;
            string strStCode = CommonData.StateCode;
            string strBranchName = CommonData.BranchName;
            string strBranchCod = CommonData.BranchCode;
            string strFinYear = CommonData.FinancialYear;
            string sDcNo = string.Empty;
            string[] strArrBr = strBranchName.Split(' ');
            if (strArrBr.Length > 2)
                strBranchName = strArrBr[strArrBr.Length - 2];
            else if (strArrBr.Length > 1)
                strBranchName = strArrBr[strArrBr.Length - 2];
            else if (strArrBr.Length == 1)
                strBranchName = strArrBr[0];

            strBranchName = strBranchName.Replace(".", "");
            try
            {
                DataTable dt = new DataTable();
                if(strTrnType == "DC")
                    strNewNo = sCompCode.Substring(0, 3) + strStCode + strBranchCod.Substring(6, 3) + strFinYear.Substring(2, 2) + strFinYear.Substring(7, 2) + "DC-";
                else
                    strNewNo = sCompCode.Substring(0, 3) + strStCode + strBranchCod.Substring(6, 3) + strFinYear.Substring(2, 2) + strFinYear.Substring(7, 2) + "DCST-";
                if(strTrnType == "DC")
                    dt = objSQLdb.ExecuteDataSet("Select IsNull(Max(Substring(IsNull(SPDH_TRN_NUMBER, '" + strNewNo + "'),16,20)),0) + 1 from SP_DC_HEAD WHERE SPDH_COMPANY_CODE  = '" + strCompCode + "' AND SPDH_BRANCH_CODE  = '" + strBranchCode + "' AND SPDH_FIN_YEAR = '" + CommonData.FinancialYear + "' AND SPDH_TRN_NUMBER LIKE '%DC-%'").Tables[0];
                else
                    dt = objSQLdb.ExecuteDataSet("Select IsNull(Max(Substring(IsNull(SPDH_TRN_NUMBER, '" + strNewNo + "'),18,20)),0) + 1 from SP_DC_HEAD WHERE SPDH_COMPANY_CODE  = '" + strCompCode + "' AND SPDH_BRANCH_CODE  = '" + strBranchCode + "' AND SPDH_FIN_YEAR = '" + CommonData.FinancialYear + "' AND SPDH_TRN_NUMBER LIKE '%DCST-%'").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    sDcNo = Convert.ToInt32(dt.Rows[0][0]).ToString().PadLeft(6, '0');
                }
                strNewNo = strNewNo + sDcNo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
            }
            return strNewNo;
        }

        public DataSet DeliveryChallanIndentDetails_Get(string sCompCode, string sIndFromCode, Int32 intIndent, Int32 nGLCODE)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sIndentFromCode", DbType.String, sIndFromCode, ParameterDirection.Input);
                //param[4] = objSQLdb.CreateParameter("@nGLCode", DbType.Int32, nGLCODE, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@nIndent", DbType.Int32, intIndent, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("DeliveryChallanIndentDetails_Get", CommandType.StoredProcedure, param);

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

        public Hashtable GetDeliveryChallanData(string strToBCode, string sTranNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nTranNo", DbType.String, sTranNo, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sToBranchCode", DbType.String, strToBCode, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("DeliveryChallanHead_Get", CommandType.StoredProcedure, param);
                ht.Add("Head", ds.Tables[0]);
                if (ds.Tables[0].Rows.Count > 0)
                    sTranNo = ds.Tables[0].Rows[0]["TrnNumber"].ToString();
                ds = null;

                param = null;
                objSQLdb = null;
                objSQLdb = new SQLDB();
                param = new SqlParameter[5];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sToBranchCode", DbType.String, strToBCode, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@nTranNo", DbType.String, sTranNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("DeliveryChallanDetl_Get", CommandType.StoredProcedure, param);
                ht.Add("Detail", ds.Tables[0]);
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

        public Int32 GenerateNewIndentNo(string strCompCode, string strBranchCode)
        {
            objSQLdb = new SQLDB();
            Int32 nIndNo = 0;
            try
            {
                DataTable dt = objSQLdb.ExecuteDataSet("Select IsNull(Max(SPIH_INDENT_NUMBER),0) + 1 from SP_INDENT_HEAD WHERE SPIH_COMPANY_CODE  = '" + strCompCode + "' AND SPIH_BRANCH_CODE  = '" + strBranchCode + "' AND SPIH_FIN_YEAR = '" + CommonData.FinancialYear + "' ").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    nIndNo = Convert.ToInt32(dt.Rows[0][0]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
            }
            return nIndNo;
        }

        public Int16 CheckIndentNoDeliver(int intIndNo)
        {
            objSQLdb = new SQLDB();
            Int16 nIndNo = 0;
            try
            {
                DataTable dt = objSQLdb.ExecuteDataSet("SELECT Count(*) RecCnt FROM  SP_DC_DETL SDD WHERE SPDD_COMPANY_CODE  = '" + CommonData.CompanyCode + "' AND SPDD_BRANCH_CODE  = '" + CommonData.BranchCode + "' AND SPDD_FIN_YEAR = '" + CommonData.FinancialYear + "' AND SPDD_INDENT_NUMBER=" + intIndNo).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    nIndNo = Convert.ToInt16(dt.Rows[0][0]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
            }
            return nIndNo;
        }

        //public Int32 GenerateNewDCTranNo(string strCompCode, string strBranchCode)
        //{
        //    objSQLdb = new SQLDB();
        //    Int32 nIndNo = 0;
        //    try
        //    {
        //        DataTable dt = objSQLdb.ExecuteDataSet("Select IsNull(Max(SPDH_TRN_NUMBER),0) + 1 from SP_DC_HEAD WHERE SPDH_COMPANY_CODE  = '" + strCompCode + "' AND SPDH_BRANCH_CODE  = '" + strBranchCode + "' AND SPDH_FIN_YEAR = '" + CommonData.FinancialYear + "' ").Tables[0];
        //        if (dt.Rows.Count > 0)
        //        {
        //            nIndNo = Convert.ToInt32(dt.Rows[0][0]);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.ToString());
        //    }
        //    finally
        //    {
        //        objSQLdb = null;
        //    }
        //    return nIndNo;
        //}

        public Hashtable GetGRNInputBranchData(string strCmpCode, string strBrCode, string strFinYear, string sTarnNo, string strToBranchCode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            string strToBrnCode = string.Empty;
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, strCmpCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBrCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, strFinYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nTranNo", DbType.String, sTarnNo, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sToBranchCode", DbType.String, strToBranchCode, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("DeliveryChallanHead_Get1", CommandType.StoredProcedure, param);
                ht.Add("Head", ds.Tables[0]);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    sTarnNo = ds.Tables[0].Rows[0]["TrnNumber"].ToString();
                    //strToBrnCode = ds.Tables[0].Rows[0]["ToBranchCode"].ToString();
                }
                ds = null;

                param = null;
                objSQLdb = null;
                objSQLdb = new SQLDB();
                param = new SqlParameter[5];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, strCmpCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBrCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, strFinYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sToBranchCode", DbType.String, strToBranchCode, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@nTranNo", DbType.String, sTarnNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("DeliveryChallanDetl_Get", CommandType.StoredProcedure, param);
                ht.Add("Detail", ds.Tables[0]);
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

        public DataSet ProductList_Get(string strCompCode, string strCategoryId)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, strCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sCategoryId", DbType.String, strCategoryId, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("ProductList_Get", CommandType.StoredProcedure, param);

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


        public DataSet Get_CombiHavingProducts(string sSingleProducts)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xSingleProducts", DbType.String, sSingleProducts, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("Get_CombiHavingProducts", CommandType.StoredProcedure, param);

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

        public string GenerateNewDLDCTranNo(string strCompCode, string strBranchCode, string strTrnType)
        {
            objSQLdb = new SQLDB();
            string strNewNo = string.Empty;
            string sCompCode = CommonData.CompanyCode;
            string strStCode = CommonData.StateCode;
            string strBranchName = CommonData.BranchName;
            string strBranchCod = CommonData.BranchCode;
            string strFinYear = CommonData.FinancialYear;
            string sDcNo = string.Empty;
            string[] strArrBr = strBranchName.Split(' ');
            if (strArrBr.Length > 2)
                strBranchName = strArrBr[strArrBr.Length - 2];
            else if (strArrBr.Length > 1)
                strBranchName = strArrBr[strArrBr.Length - 2];
            else if (strArrBr.Length == 1)
                strBranchName = strArrBr[0];

            strBranchName = strBranchName.Replace(".", "");
            try
            {
                DataTable dt = new DataTable();
                //if (strTrnType == "SP2DLSB")
                strNewNo = sCompCode.Substring(0, 3) + strStCode + strBranchCod.Substring(6, 3) + strFinYear.Substring(2, 2) + strFinYear.Substring(7, 2) + "-";
                //else
                //    strNewNo = sCompCode.Substring(0, 3) + strStCode + strBranchCod.Substring(6, 3) + strFinYear.Substring(2, 2) + strFinYear.Substring(7, 2) + "DCST-";
                //if (strTrnType == "SP2DLSB")
                    dt = objSQLdb.ExecuteDataSet("Select IsNull(Max(Substring(IsNull(DLDH_TRN_NUMBER, '" + strNewNo + 
                        "'),14,18)),0) + 1 from DL_DCINV_HEAD WHERE DLDH_COMPANY_CODE  = '" + strCompCode + 
                        "' AND DLDH_BRANCH_CODE = '" + strBranchCode + "' AND DLDH_FIN_YEAR = '" + CommonData.FinancialYear + "'").Tables[0];
                //else
                //    dt = objSQLdb.ExecuteDataSet("Select IsNull(Max(Substring(IsNull(SPDH_TRN_NUMBER, '" + strNewNo + "'),18,20)),0) + 1 from SP_DC_HEAD WHERE SPDH_COMPANY_CODE  = '" + strCompCode + "' AND SPDH_BRANCH_CODE  = '" + strBranchCode + "' AND SPDH_FIN_YEAR = '" + CommonData.FinancialYear + "' AND SPDH_TRN_NUMBER LIKE '%DCST-%'").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    sDcNo = Convert.ToInt32(dt.Rows[0][0]).ToString().PadLeft(6, '0');
                }
                strNewNo = strNewNo + sDcNo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
            }
            return strNewNo;
        }



        public Hashtable GetDLDeliveryChallanData(string sTranNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nTranNo", DbType.String, sTranNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("DLDeliveryChallanHead_Get", CommandType.StoredProcedure, param);
                ht.Add("Head", ds.Tables[0]);
                if (ds.Tables[0].Rows.Count > 0)
                    sTranNo = ds.Tables[0].Rows[0]["TrnNumber"].ToString();
                ds = null;

                param = null;
                objSQLdb = null;
                objSQLdb = new SQLDB();
                param = new SqlParameter[4];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nTranNo", DbType.String, sTranNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("DLDeliveryChallanDetl_Get", CommandType.StoredProcedure, param);
                ht.Add("Detail", ds.Tables[0]);
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


        public string GenerateNewDCRTranNo(string strCompCode, string strBranchCode, string strTrnType)
        {
            objSQLdb = new SQLDB();
            string strNewNo = string.Empty;
            string sCompCode = CommonData.CompanyCode;
            string strStCode = CommonData.StateCode;
            string strBranchName = CommonData.BranchName;
            string strBranchCod = CommonData.BranchCode;
            string strFinYear = CommonData.FinancialYear;
            string sDcNo = string.Empty;
            string[] strArrBr = strBranchName.Split(' ');
            if (strArrBr.Length > 2)
                strBranchName = strArrBr[strArrBr.Length - 2];
            else if (strArrBr.Length > 1)
                strBranchName = strArrBr[strArrBr.Length - 2];
            else if (strArrBr.Length == 1)
                strBranchName = strArrBr[0];

            strBranchName = strBranchName.Replace(".", "");
            try
            {
                DataTable dt = new DataTable();
                if (strTrnType == "SP2BR")
                    strNewNo = sCompCode.Substring(0, 3) + strStCode + strBranchCod.Substring(6, 3) + strFinYear.Substring(2, 2) + strFinYear.Substring(7, 2) + "DCR-";
                else
                    strNewNo = sCompCode.Substring(0, 3) + strStCode + strBranchCod.Substring(6, 3) + strFinYear.Substring(2, 2) + strFinYear.Substring(7, 2) + "DCR-";
                if (strTrnType == "SP2BR")
                    dt = objSQLdb.ExecuteDataSet("Select IsNull(Max(Substring(IsNull(SPDH_TRN_NUMBER, '" + strNewNo + "'),17,20)),0) + 1 from SP_DC_HEAD WHERE SPDH_COMPANY_CODE  = '" + strCompCode + "' AND SPDH_BRANCH_CODE  = '" + strBranchCode + "' AND SPDH_FIN_YEAR = '" + CommonData.FinancialYear + "' AND SPDH_TRN_NUMBER LIKE '%DCR-%'").Tables[0];

                if (dt.Rows.Count > 0)
                {
                    sDcNo = Convert.ToInt32(dt.Rows[0][0]).ToString().PadLeft(6, '0');
                }
                strNewNo = strNewNo + sDcNo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
            }
            return strNewNo;
        }



        
    }
}
