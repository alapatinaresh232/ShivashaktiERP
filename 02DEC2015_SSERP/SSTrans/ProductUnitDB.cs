using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SSCRMDB;
using System.Data.SqlClient;
using System.Collections;

namespace SSTrans
{
    public class ProductUnitDB
    {
        private SQLDB objSQLdb = null;

        #region Constructor
        public ProductUnitDB()
        {          
            
        }
        #endregion

        public DataSet BatchProductList_Get(string strCompCode, string strBranchCode, string strProduct)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, strCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sProduct", DbType.String, strProduct, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("BatchProductList_Get", CommandType.StoredProcedure, param);

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

        public DataSet FinishedProductList_Get(string strCompCode, string strBranchCode, string strProduct, string sCatgId)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, strCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sCatgId", DbType.String, sCatgId, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sProduct", DbType.String, strProduct, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("FinishedProductList_Get", CommandType.StoredProcedure, param);

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

        public string GenerateNewDCPUTranNo(string strCompCode, string strBranchCode,string sTrnType)
        {
            objSQLdb = new SQLDB();
            string strNewNo = string.Empty;
            string sCompCode = CommonData.CompanyCode;
            string sBranchCode = CommonData.BranchCode;
            string strStCode = CommonData.StateCode;
            string strBranchName = CommonData.BranchName;
            string strFinYear = CommonData.FinancialYear;
            string sDcNo = string.Empty;
            //string[] strArrBr = strBranchName.Split(' ');
            //if (strArrBr.Length > 2)
            //    strBranchName = strArrBr[strArrBr.Length - 2];
            //else if (strArrBr.Length > 1)
            //    strBranchName = strArrBr[strArrBr.Length - 2];
            //else if (strArrBr.Length == 1)
            //    strBranchName = strArrBr[0];
            string sType = "";
            strBranchName = strBranchName.Replace(".","");
            try
            {
                if (sTrnType == "DC")
                {
                    strNewNo = sCompCode.Substring(0, 3) + strStCode + sBranchCode.Substring(6, 3) + strFinYear.Substring(2, 2) + strFinYear.Substring(7, 2) + "DC-";
                    sType = "DC-";
                }
                else
                {
                    strNewNo = sCompCode.Substring(0, 3) + strStCode + sBranchCode.Substring(6, 3) + strFinYear.Substring(2, 2) + strFinYear.Substring(7, 2) + "DCST-";
                    sType = "DCST-";
                }
                string strSQL = "Select IsNull(Max(Substring(IsNull(PUFDH_TRN_NUMBER, '" + strNewNo + "'),"
                    + (strNewNo.Length + 1) + "," + (strNewNo.Length + 6) + ")),0) + 1 from PU_FG_DC_HEAD WHERE PUFDH_COMPANY_CODE  = '" +
                    CommonData.CompanyCode + "' AND PUFDH_BRANCH_CODE  = '" + CommonData.BranchCode + "' AND PUFDH_FIN_YEAR = '" +
                    CommonData.FinancialYear + "' AND PUFDH_TRN_NUMBER LIKE '%" + sType + "%'";
                DataTable dt = objSQLdb.ExecuteDataSet(strSQL).Tables[0];
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

        public string GenerateNewSPGRNTranNo(string strCompCode, string strBranchCode)
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
                strNewNo = strBranchCod.Substring(0, 5) + strBranchCod.Substring(6, 3) + strFinYear.Substring(2, 2) + strFinYear.Substring(7, 2) + "GRN-";
                DataTable dt = objSQLdb.ExecuteDataSet("Select IsNull(Max(Substring(IsNull(SPGH_GRN_NUMBER, '" + strNewNo + "'),17,21)),0) + 1 from SP_GRN_HEAD WHERE SPGH_COMPANY_CODE  = '" + strCompCode + "' AND SPGH_BRANCH_CODE  = '" + strBranchCode + "' AND SPGH_FIN_YEAR = '" + CommonData.FinancialYear + "' ").Tables[0];
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

        public string GenerateNewSPPKMGRNTranNo(string strCompCode, string strBranchCode)
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
                strNewNo = strBranchCod.Substring(0, 5) + strBranchCod.Substring(6, 3) + strFinYear.Substring(2, 2) + strFinYear.Substring(7, 2) + "GRN-";
                DataTable dt = objSQLdb.ExecuteDataSet("Select IsNull(Max(Substring(IsNull(SPGH_GRN_NUMBER, '" + strNewNo + "'),17,21)),0) + 1 from SP_PKM_GRN_HEAD WHERE SPGH_COMPANY_CODE  = '" + strCompCode + "' AND SPGH_BRANCH_CODE  = '" + strBranchCode + "' AND SPGH_FIN_YEAR = '" + CommonData.FinancialYear + "' ").Tables[0];
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

        public DataSet GRN_DeliveryChallanListFromPU(string sCompCode, string sBranchCode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sDCFromBRCode", DbType.String, sBranchCode, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GRN_DeliveryChallanListFromPU", CommandType.StoredProcedure, param);

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
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nGroupEcode", DbType.Int32, intEcode, ParameterDirection.Input);
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

        public DataSet IndentProductList_Get(string strCompCode, string strBranchCode, string strCategoryId)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, strCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sCategoryId", DbType.String, strCategoryId, ParameterDirection.Input);
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

        public DataSet IndentProductDetail_Get(string sCompCode, string sBranchCode, Int32 intIndent)
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

        //public DataSet DeliveryChallanIndentList_Get(string sCompCode, string sBranchCode, string sIndentFromCode, Int32 intGLEcode)
        //{
        //    objSQLdb = new SQLDB();
        //    SqlParameter[] param = new SqlParameter[5];
        //    DataSet ds = new DataSet();
        //    try
        //    {

        //        param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, sCompCode, ParameterDirection.Input);
        //        param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
        //        param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
        //        param[3] = objSQLdb.CreateParameter("@sIndentFromCode", DbType.String, sIndentFromCode, ParameterDirection.Input);
        //        param[4] = objSQLdb.CreateParameter("@nGLEcode", DbType.Int32, intGLEcode, ParameterDirection.Input);

        //        ds = objSQLdb.ExecuteDataSet("DeliveryChallanIndentList_Get", CommandType.StoredProcedure, param);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.ToString());
        //    }
        //    finally
        //    {
        //        param = null;
        //        objSQLdb = null;
        //    }
        //    return ds;
        //}

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
        public DataSet DRN_PUDeliveryChallanDetl(string sCompCode, string sDCFromCode, string strTrnNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sDCFromBRCode", DbType.String, sDCFromCode, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sTrnNo", DbType.String, strTrnNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("DRN_PUDeliveryChallanDetl", CommandType.StoredProcedure, param);

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

        public DataSet GRN_StockPoint(string sCompCode, string sDCFromCode, string strTrnNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sTranNo", DbType.String, strTrnNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GRN_StockPoint", CommandType.StoredProcedure, param);

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

        public DataSet GRN_PKM_StockPoint(string sCompCode, string sDCFromCode, string strTrnNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sTranNo", DbType.String, strTrnNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GRN_PKM_StockPoint", CommandType.StoredProcedure, param);

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
        
        public DataSet GRN_DeliveryChallanPUHead_Get(string sCompCode, string sDCFromCode, string strTrnNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sDCFromCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sToBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sTranNo", DbType.String, strTrnNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("DeliveryChallanPUHead_Get", CommandType.StoredProcedure, param);

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
                param[3] = objSQLdb.CreateParameter("@sTranNo", DbType.String, sTranNo, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sToBranchCode", DbType.String, strToBCode, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("DeliveryChallanPUHead_Get", CommandType.StoredProcedure, param);
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
                param[4] = objSQLdb.CreateParameter("@sTranNo", DbType.String, sTranNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("DeliveryChallanPUDetl_Get", CommandType.StoredProcedure, param);
                ht.Add("Detail", ds.Tables[0]);

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
                param[4] = objSQLdb.CreateParameter("@sTranNo", DbType.String, sTranNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("DeliveryChallanPUPMDetl_Get", CommandType.StoredProcedure, param);
                ht.Add("PMDetl", ds.Tables[0]);
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

        public Hashtable GetGoodsReceiptNoteData(string strToBCode, string sTranNo)
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
                param[3] = objSQLdb.CreateParameter("@sToBranchCode", DbType.String, strToBCode, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sTranNo", DbType.String, sTranNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GoodsReceiptNotePUHead_Get", CommandType.StoredProcedure, param);
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
                param[4] = objSQLdb.CreateParameter("@sTranNo", DbType.String, sTranNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GoodsReceiptNotePUDetl_Get", CommandType.StoredProcedure, param);
                ht.Add("Detail", ds.Tables[0]);

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
                param[4] = objSQLdb.CreateParameter("@sTranNo", DbType.String, sTranNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GoodsReceiptNotePUPMDetl_Get", CommandType.StoredProcedure, param);
                ht.Add("PMDetl", ds.Tables[0]);
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




        public string GenerateNewGRNTranNo(string strCompCode, string strBranchCode, string sTrnType)
        {
            objSQLdb = new SQLDB();
            string strNewNo = string.Empty;
            string sCompCode = CommonData.CompanyCode;
            string sBranchCode = CommonData.BranchCode;
            string strStCode = CommonData.StateCode;
            string strBranchName = CommonData.BranchName;
            string strFinYear = CommonData.FinancialYear;
            string sDcNo = string.Empty;
            //string[] strArrBr = strBranchName.Split(' ');
            //if (strArrBr.Length > 2)
            //    strBranchName = strArrBr[strArrBr.Length - 2];
            //else if (strArrBr.Length > 1)
            //    strBranchName = strArrBr[strArrBr.Length - 2];
            //else if (strArrBr.Length == 1)
            //    strBranchName = strArrBr[0];
            string sType = "";
            strBranchName = strBranchName.Replace(".", "");
            try
            {               
                strNewNo = sCompCode.Substring(0, 3) + strStCode + sBranchCode.Substring(6, 3) + strFinYear.Substring(2, 2) + strFinYear.Substring(7, 2) + "GRN-";
                sType = "GRN-";
                
                DataTable dt = objSQLdb.ExecuteDataSet("Select IsNull(Max(Substring(IsNull(PUGH_TRN_NUMBER, '" + strNewNo + "')," + (strNewNo.Length + 1) + "," + (strNewNo.Length + 6) + ")),0) + 1 from PU_GRN_HEAD WHERE PUGH_COMPANY_CODE  = '" + CommonData.CompanyCode + "' AND PUGH_BRANCH_CODE  = '" + CommonData.BranchCode + "' AND PUGH_FIN_YEAR = '" + CommonData.FinancialYear + "' AND PUGH_TRN_NUMBER LIKE '%" + sType + "%' ").Tables[0];
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

        public DataSet DcDCSTList_Get(string strCmpCode, string strBrCode, string strFinYear, string strDocMonth)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@COMP", DbType.String, strCmpCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@BranchCode", DbType.String, strBrCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@FinYear", DbType.String, strFinYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@DocMonth", DbType.String, strDocMonth, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetTransitDCorDCSTforDestLocation", CommandType.StoredProcedure, param);

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





        //public object DcDCSTListByRefDCDCSTNo_Get(string p, string p_2, string p_3, string p_4, string p_5)
        //{
        //    //throw new NotImplementedException();
        //}

        public string GenerateNewDCRPUTranNo(string strCompCode, string strBranchCode, string sTrnType)
        {
            objSQLdb = new SQLDB();
            string strNewNo = string.Empty;
            string sCompCode = CommonData.CompanyCode;
            string sBranchCode = CommonData.BranchCode;
            string strStCode = CommonData.StateCode;
            string strBranchName = CommonData.BranchName;
            string strFinYear = CommonData.FinancialYear;
            string sDcNo = string.Empty;
            string sType = "";
            strBranchName = strBranchName.Replace(".", "");

            try
            {
                if (sTrnType == "DCR")
                {
                    strNewNo = sCompCode.Substring(0, 3) + strStCode + sBranchCode.Substring(6, 3) + strFinYear.Substring(2, 2) + strFinYear.Substring(7, 2) + "DCR-";
                    sType = "DCR-";
                }

                DataTable dt = objSQLdb.ExecuteDataSet("Select IsNull(Max(Substring(IsNull(PUFDH_TRN_NUMBER, '" + strNewNo + "')," + (strNewNo.Length + 1) + "," + (strNewNo.Length + 6) + ")),0) + 1 " +
                                                        " from PU_FG_DC_HEAD WHERE PUFDH_COMPANY_CODE  = '" + CommonData.CompanyCode +
                                                        "' AND PUFDH_BRANCH_CODE  = '" + CommonData.BranchCode +
                                                        "' AND PUFDH_FIN_YEAR = '" + CommonData.FinancialYear +
                                                        "' AND PUFDH_TRN_NUMBER LIKE '%" + sType + "%' ").Tables[0];
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
