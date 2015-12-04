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

namespace SSTrans
{
    public class ServiceDB
    {
        private SQLDB objSQLdb = null;

        public DataSet GetInvocieInfoforService(string BranchCode, string FinYear, string OrderNo, string InvoiceNo, int iMode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@iMode", DbType.Int32, iMode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xFinYear", DbType.String, FinYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xOrderNo", DbType.String, OrderNo, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xInvoiceNo", DbType.String, InvoiceNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetInvoiceInfoforService", CommandType.StoredProcedure, param);
                param = null;
                objSQLdb = null;
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


        public DataSet GetECodesforService(string BranchCode, string DeptCode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xDeptCode", DbType.String, DeptCode, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetEcodeforService", CommandType.StoredProcedure, param);
                param = null;
                objSQLdb = null;
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
        /// <summary>
        /// Master section in Get Free Offer Product details
        /// </summary>
        /// <param name="BranchCode"></param>
        /// <param name="DeptCode"></param>
        /// <returns></returns>
        public DataSet GetFreeOfferProducts(string CompanyCode, string BranchCode, string FinYear, int OfferNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@CompanyCode", DbType.String, CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@BranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@iOfferNo", DbType.Int32, OfferNo, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@FinYear", DbType.String, FinYear, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetFreeOfferProductDetails", CommandType.StoredProcedure, param);
                param = null;
                objSQLdb = null;
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
        /// <summary>
        /// This is used for Get Product Rate Range Details
        /// </summary>
        /// <param name="CompanyCode"></param>
        /// <param name="BranchCode"></param>
        /// <param name="FinYear"></param>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public DataSet GetProductRateRange(string CompanyCode, string BranchCode, string FinYear, string ProductID)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@CompanyCode", DbType.String, CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@BranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@FinYear", DbType.String, FinYear, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@ProductID", DbType.String, ProductID, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetProductRateRange", CommandType.StoredProcedure, param);
                param = null;
                objSQLdb = null;
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
        public DataSet GetSearchServiceData(string sCompany, string sBranch)
        {
            string sqlQry = "SELECT SIH_COMPANY_CODE,SIH_STATE_CODE,SIH_BRANCH_CODE,SIH_FIN_YEAR,SIH_INVOICE_NUMBER,SIH_INVOICE_DATE,SIH_DOCUMENT_MONTH, " +
                " SIH_FARMER_ID,SIH_ORDER_NUMBER,SIH_EORA_CODE,sr_eora_name=(select Member_Name from eora_master where ecode=SIH_EORA_CODE),cm_village," +
                " cm_farmer_name,cm_so_fo,cm_forg_name,cm_mobile_number,cm_mandal,cm_district,cm_state,cm_pin,sam_activity_name,TNA_ACTUAL_DATE,TNA_ATTEND_BY_ECODE," +
                " TNA_FARMER_REMARKS,TNA_PRODUCT_ID, pm_product_name,TNA_TARGET_DATE,SIBD_QTY TNA_QTY,CATEGORY_SHORT_NAME as ShortName " +
                " FROM SERVICES_TNA, SALES_INV_HEAD, CUSTOMER_MAS, SERVICES_ACTIVITIES_MAS, PRODUCT_MAS, CATEGORY_MASTER, SALES_INV_BULTIN_DETL WHERE TNA_company_code = siH_company_code and " +
                " TNA_state_code = siH_state_code and  TNA_branch_code = siH_branch_code and TNA_fin_year = siH_fin_year and TNA_invoice_number = siH_invoice_number and " +
                " TNA_product_id = pm_product_id and TNA_ACTIVITY_ID = SAM_ACTIVITY_ID AND SIH_FARMER_ID = CM_FARMER_ID AND pm_category_id = category_id and category_id = sam_category_id " +
                " AND TNA_product_id=SIBD_PRODUCT_ID AND TNA_company_code=SIBD_COMPANY_CODE AND TNA_branch_code=SIBD_BRANCH_CODE AND TNA_fin_year=SIBD_FIN_YEAR AND SIBD_INVOICE_NUMBER=TNA_invoice_number " +
                " AND TNA_TARGET_DATE < CONVERT(NVARCHAR(10),GETDATE(),102) AND TNA_company_code='" + sCompany + "' AND TNA_branch_code='" + sBranch + "' ORDER BY siH_invoice_number";
            objSQLdb = new SQLDB();
            DataSet ds = objSQLdb.ExecuteDataSet(sqlQry);
            objSQLdb = null;
            return ds;
        }
        public DataSet GetMapOrUnmappedEcodes(string sCompanyCode, string sBranCode, string sDocMonth, char cMapped)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, sCompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@cMapped", DbType.String, cMapped, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("Get_MapOrUnmappedServiceLvlEcodes", CommandType.StoredProcedure, param);

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

        public DataSet GetServiceLevels(string sComCode, string sBranType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();

            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, sComCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBRType", DbType.String, sBranType, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("GetServiceLevels", CommandType.StoredProcedure, param);
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
        //public DataSet EcodesForService_Get(string BranchCode, Int32 DesgId,string sDocMonth)
        //{
        //    objSQLdb = new SQLDB();
        //    SqlParameter[] param = new SqlParameter[3];
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        param[0] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
        //        param[1] = objSQLdb.CreateParameter("@xDesgId", DbType.Int32, DesgId, ParameterDirection.Input);
        //        param[2] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
        //        //param[3] = objSQLdb.CreateParameter("@xDeptCode", DbType.String, DeptCode, ParameterDirection.Input);
        //        ds = objSQLdb.ExecuteDataSet("EcodesForService_Get", CommandType.StoredProcedure, param);

               
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

        //public DataSet LevelGroupServiceEcodeMapped_Get(string CompCode, string BranchCode, string StateCode, Int32 DestEcode, Int32 nLvlId, string LogBranCode)
        //{
        //    objSQLdb = new SQLDB();
        //    SqlParameter[] param = new SqlParameter[7];
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, CompCode, ParameterDirection.Input);
        //        param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
        //        param[2] = objSQLdb.CreateParameter("@sStateCode", DbType.String, StateCode, ParameterDirection.Input);
        //        param[3] = objSQLdb.CreateParameter("@nDestEcode", DbType.Int32, DestEcode, ParameterDirection.Input);
        //        param[4] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, CommonData.DocMonth, ParameterDirection.Input);
        //        param[5] = objSQLdb.CreateParameter("@nLDMId", DbType.Int32, nLvlId, ParameterDirection.Input);
        //        param[6] = objSQLdb.CreateParameter("@sLogBranchCode", DbType.String, LogBranCode, ParameterDirection.Input);
        //        ds = objSQLdb.ExecuteDataSet("LevelGroupServiceEcodeMapped_Get", CommandType.StoredProcedure, param);


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

        //public DataSet LevelServiceMappedGroupList_Get(string CompCode, string BranchCode, string StateCode)
        //{
        //    objSQLdb = new SQLDB();
        //    SqlParameter[] param = new SqlParameter[4];
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, CompCode, ParameterDirection.Input);
        //        param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
        //        param[2] = objSQLdb.CreateParameter("@sStateCode", DbType.String, StateCode, ParameterDirection.Input);
        //        param[3] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, CommonData.DocMonth, ParameterDirection.Input);
                
        //        ds = objSQLdb.ExecuteDataSet("LevelServiceMappedGroupList_Get", CommandType.StoredProcedure, param);


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



        //public DataSet ServiceLevelGroupEcodeMapped_Get(string CompCode, string BranchCode, string StateCode, Int32 nLvlId, Int32 iDestEcode, string LogBranCode)
        //{
        //    objSQLdb = new SQLDB();
        //    SqlParameter[] param = new SqlParameter[7];
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, CompCode, ParameterDirection.Input);
        //        param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
        //        param[2] = objSQLdb.CreateParameter("@sStateCode", DbType.String, StateCode, ParameterDirection.Input);
        //        param[3] = objSQLdb.CreateParameter("@nLDMId", DbType.Int32, nLvlId, ParameterDirection.Input);
        //        param[4] = objSQLdb.CreateParameter("@nDestEcode", DbType.Int32, iDestEcode, ParameterDirection.Input);
        //        param[5] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, CommonData.DocMonth, ParameterDirection.Input);
        //        param[6] = objSQLdb.CreateParameter("@sLogBranchCode", DbType.String,LogBranCode , ParameterDirection.Input);


        //        ds = objSQLdb.ExecuteDataSet("ServiceLevelGroupEcodeMapped_Get", CommandType.StoredProcedure, param);


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


        public DataSet EcodesForService_Get(string BranchCode, Int32 DesgId, string sDocMonth)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xDesgId", DbType.Int32, DesgId, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, sDocMonth, ParameterDirection.Input);
                //param[3] = objSQLdb.CreateParameter("@xDeptCode", DbType.String, DeptCode, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("EcodesForService_Get", CommandType.StoredProcedure, param);


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

        public DataSet LevelGroupServiceEcodeMapped_Get(string CompCode, string BranchCode, string StateCode, Int32 DestEcode, Int32 nLvlId, string LogBranCode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[7];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, CompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sStateCode", DbType.String, StateCode, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nDestEcode", DbType.Int32, DestEcode, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, CommonData.DocMonth, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@nLDMId", DbType.Int32, nLvlId, ParameterDirection.Input);
                param[6] = objSQLdb.CreateParameter("@sLogBranchCode", DbType.String, LogBranCode, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("LevelGroupServiceEcodeMapped_Get", CommandType.StoredProcedure, param);


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

        public DataSet LevelServiceMappedGroupList_Get(string CompCode, string BranchCode, string StateCode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, CompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sStateCode", DbType.String, StateCode, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, CommonData.DocMonth, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("LevelServiceMappedGroupList_Get", CommandType.StoredProcedure, param);


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



        public DataSet ServiceLevelGroupEcodeMapped_Get(string CompCode, string BranchCode, string StateCode, Int32 nLvlId, Int32 iDestEcode, string LogBranCode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[7];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, CompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sStateCode", DbType.String, StateCode, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nLDMId", DbType.Int32, nLvlId, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@nDestEcode", DbType.Int32, iDestEcode, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, CommonData.DocMonth, ParameterDirection.Input);
                param[6] = objSQLdb.CreateParameter("@sLogBranchCode", DbType.String, LogBranCode, ParameterDirection.Input);


                ds = objSQLdb.ExecuteDataSet("ServiceLevelGroupEcodeMapped_Get", CommandType.StoredProcedure, param);


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

        public DataSet ServiceLevelGroupDetailEcodesMapped_Get(string CompCode, string BranchCode, string StateCode, Int32 nLvlId, Int32 iGroupEcode, string DocMon, string LogBranCode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[7];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, CompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sStateCode", DbType.String, StateCode, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nLevelId", DbType.Int32, nLvlId, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@nGroupEcode", DbType.Int32, iGroupEcode, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, DocMon, ParameterDirection.Input);
                param[6] = objSQLdb.CreateParameter("@sLogBranchCode", DbType.String, LogBranCode, ParameterDirection.Input);


                ds = objSQLdb.ExecuteDataSet("ServiceLevelGroupDetailEcodesMapped_Get", CommandType.StoredProcedure, param);


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
