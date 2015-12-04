using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Diagnostics;
using SSCRMDB;

namespace SSTrans
{

    public class ServiceDeptDB
    {
        private SQLDB objSQLdb = null;

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

        public DataSet GetVillageDataSet(Hashtable htParams)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sVillage", DbType.String, Convert.ToString(htParams["sVillage"]), ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sDistrict", DbType.String, Convert.ToString(htParams["sDistrict"]), ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sCDState", DbType.String, Convert.ToString(htParams["sCDState"]), ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("VillageMaster_Proc", CommandType.StoredProcedure, param);

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


        public DataSet ServiceVillageSearch_Get(string sVillage)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];


            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sVillage", DbType.String, sVillage, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("ServiceVillageSearch_Get", CommandType.StoredProcedure, param);

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



        public DataSet Get_EcodesforService()
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, CommonData.DocMonth, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_EcodesforService", CommandType.StoredProcedure, param);


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

        public DataSet ServiceLevelEcodeSearch_Get(string CompCode, string BranchCode, string DocMon, string EcodeName)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, CompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, DocMon, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sECodeName", DbType.String, EcodeName, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("ServiceLevelEcodeSearch_Get", CommandType.StoredProcedure, param);


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

        public Hashtable GetServiceFarmerMeetingDetails(string strTrnNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sTransactionNo", DbType.String, strTrnNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("ServiceFarmerMeetingHead_Get", CommandType.StoredProcedure, param);
                ht.Add("FarmerMeetingHead", ds.Tables[0]);
                ds = null;
                param = null;
                objSQLdb = null;

                objSQLdb = new SQLDB();
                param = new SqlParameter[1];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@sTransactionNo", DbType.String, strTrnNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("ServiceFarmerMeetProductDetails_Get", CommandType.StoredProcedure, param);
                ht.Add("FarmerMeetingProductDetails", ds.Tables[0]);


                ds = null;
                param = null;
                objSQLdb = null;

                objSQLdb = new SQLDB();
                param = new SqlParameter[1];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@sTransactionNo", DbType.String, strTrnNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("ServiceFarmerMeetingAttendents_Get", CommandType.StoredProcedure, param);
                ht.Add("AttendentEmpDetails", ds.Tables[0]);
                ht.Add("AgriDeptEmpDetails", ds.Tables[1]);
                ht.Add("FarmerDetails", ds.Tables[2]);

                ds = null;
                param = null;
                objSQLdb = null;

                objSQLdb = new SQLDB();
                param = new SqlParameter[1];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@sTransactionNo", DbType.String, strTrnNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("ServiceFarmerMeetImageDetl_Get", CommandType.StoredProcedure, param);
                ht.Add("ImageDetails", ds.Tables[0]);              
                
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

        public Hashtable GetServiceSchoolVisitDetails(string strTrnNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sTransactionNo", DbType.String, strTrnNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SchoolVisitHead_Get", CommandType.StoredProcedure, param);
                ht.Add("SchoolVisitHead", ds.Tables[0]);
                ds = null;
                param = null;
                objSQLdb = null;

                objSQLdb = new SQLDB();
                param = new SqlParameter[1];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@sTransactionNo", DbType.String, strTrnNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SchoolVisitProductDemoDetl_Get", CommandType.StoredProcedure, param);
                ht.Add("SchoolVisitProductDetails", ds.Tables[0]);

                ds = null;
                param = null;
                objSQLdb = null;

                objSQLdb = new SQLDB();
                param = new SqlParameter[1];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@sTransactionNo", DbType.String, strTrnNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SchoolVisitGiftDetl_Get", CommandType.StoredProcedure, param);
                ht.Add("GiftDetails", ds.Tables[0]);


                ds = null;
                param = null;
                objSQLdb = null;

                objSQLdb = new SQLDB();
                param = new SqlParameter[1];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@sTransactionNo", DbType.String, strTrnNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("SchoolVisitAttendentDetl_Get", CommandType.StoredProcedure, param);
                ht.Add("StudentDetails", ds.Tables[0]);
                ht.Add("AttendentEmpDetails", ds.Tables[1]);
                ht.Add("SchoolStaffDetails", ds.Tables[2]);



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


        public DataSet CustomersInVillage_Get(string sVillage, string sMandal, string sDistrict, string sState)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSQLdb.CreateParameter("@xVillage", DbType.String, sVillage, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xMandal", DbType.String, sMandal, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDistrict", DbType.String, sDistrict, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xState", DbType.String, sState, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("CustomersInVillage_Get", CommandType.StoredProcedure, param);
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


        public Hashtable GetServiceDailyActivityDetails(string strTrnNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sTrnNo", DbType.String, strTrnNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_ServiceDARHeadDetails", CommandType.StoredProcedure, param);
                ht.Add("ServicesDARHead", ds.Tables[0]);
                ds = null;
                param = null;
                objSQLdb = null;

                objSQLdb = new SQLDB();
                param = new SqlParameter[1];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@sTrnNo", DbType.String, strTrnNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_ServicesDARDetl", CommandType.StoredProcedure, param);
                ht.Add("ServicesDARDetl", ds.Tables[0]);



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





        public Hashtable GetProductPromotionDetails(string sTrnNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sTrnNo", DbType.String, sTrnNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_ProdPromotionHeadDetl", CommandType.StoredProcedure, param);
                ht.Add("ProdPromHead", ds.Tables[0]);
                ds = null;
                param = null;
                objSQLdb = null;

                objSQLdb = new SQLDB();
                param = new SqlParameter[1];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@sTrnNo", DbType.String, sTrnNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_PMProdDemoDetl", CommandType.StoredProcedure, param);
                ht.Add("ProdPromProductDetails", ds.Tables[0]);

                ds = null;
                param = null;
                objSQLdb = null;

                objSQLdb = new SQLDB();
                param = new SqlParameter[1];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@sTrnNo", DbType.String, sTrnNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_ProdPromItemDetails", CommandType.StoredProcedure, param);
                ht.Add("ProdItemDetails", ds.Tables[0]);


                ds = null;
                param = null;
                objSQLdb = null;

                objSQLdb = new SQLDB();
                param = new SqlParameter[1];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@sTrnNo", DbType.String, sTrnNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_ProdPrmAttendentDetails", CommandType.StoredProcedure, param);
                ht.Add("OthersDetails", ds.Tables[0]);
                ht.Add("AttendentEmpDetails", ds.Tables[1]);


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


        public Hashtable GetInvoiceData(string sCompCode, string strSCode, string strBCode, int nInvoiceNo, string sOrderNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear.ToString(), ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nInvoice", DbType.Int32, nInvoiceNo, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sOrderNo", DbType.String, sOrderNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("Get_InvoiceBultinHeadDetails", CommandType.StoredProcedure, param);
                ht.Add("InvHead", ds.Tables[0]);
                if (ds.Tables[0].Rows.Count > 0)
                    nInvoiceNo = Convert.ToInt32(ds.Tables[0].Rows[0]["invoice_number"]);
                ds = null;

                param = null;
                objSQLdb = null;
                objSQLdb = new SQLDB();
                param = new SqlParameter[4];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear.ToString(), ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nInvoice", DbType.Int32, nInvoiceNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("GetInvoiceDetail_Proc", CommandType.StoredProcedure, param);
                ht.Add("InvDetail", ds.Tables[0]);
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

        public Hashtable InvoiceBulitin_Get(string sCompCode, string strSCode, string strBCode, int nInvoiceNo, string sOrderNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear.ToString(), ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nInvoice", DbType.Int32, nInvoiceNo, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@sOrderNo", DbType.String, sOrderNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("Get_InvoiceBultinHeadDetails", CommandType.StoredProcedure, param);
                ht.Add("InvHead", ds.Tables[0]);
                if (ds.Tables[0].Rows.Count > 0)
                    nInvoiceNo = Convert.ToInt32(ds.Tables[0].Rows[0]["invoice_number"]);
                ds = null;

                param = null;
                objSQLdb = null;
                objSQLdb = new SQLDB();

                param = new SqlParameter[4];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, strBCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear.ToString(), ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@nInvoice", DbType.Int32, nInvoiceNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("InvoiceBultinDetail_Get", CommandType.StoredProcedure, param);
                ht.Add("InvDetail", ds.Tables[0]);
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
        public DataSet SalesOrderForBuiltInAndInvoice_Get(string sOrdNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompanyCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, CommonData.FinancialYear.ToString(), ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@sOrderNo", DbType.String, sOrdNo, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("SalesOrderForBuiltInAndInvoice_Get", CommandType.StoredProcedure, param);
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
        public DataSet GetServiceWCorFFDetails(string strTrnNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();

            try
            {
                param[0] = objSQLdb.CreateParameter("@sTransactionNo", DbType.String, strTrnNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("ServiceWCOrFFDetails_Get", CommandType.StoredProcedure, param);



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
        public Hashtable GetDemoPlotsDetails(string strTrnNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sTrnNo", DbType.String, strTrnNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("DemoPlotsHead_Get", CommandType.StoredProcedure, param);
                ht.Add("DemoPlotsHead", ds.Tables[0]);
                
                ds = null;
                param = null;
                objSQLdb = null;
                objSQLdb = new SQLDB();
                param = new SqlParameter[1];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@sTrnNo", DbType.String, strTrnNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("DemoPlotsProductDetails_Get", CommandType.StoredProcedure, param);
                ht.Add("DemoPlotsProductDetails", ds.Tables[0]);

                ds = null;
                param = null;
                objSQLdb = null;
                objSQLdb = new SQLDB();
                param = new SqlParameter[1];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@sTrnNo", DbType.String, strTrnNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("DemoPlotsAttendentDetails_Get", CommandType.StoredProcedure, param);
                ht.Add("DemoPlotsFarmerDetails", ds.Tables[0]);
                ht.Add("DemoPlotsEmpDetails", ds.Tables[1]);
                ht.Add("DemoPlotsAgriDeptEmpDetails", ds.Tables[2]);

                ds = null;
                param = null;
                objSQLdb = null;
                objSQLdb = new SQLDB();
                param = new SqlParameter[1];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@sTrnNo", DbType.String, strTrnNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("DemoPlotsResultDetails_Get", CommandType.StoredProcedure, param);
                ht.Add("DemoPlotsResultDetails", ds.Tables[0]);


                
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

        public DataSet ServiceFarmerMeetingDetails(string CompCode, string BranchCode, string DocMon, string RepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, CompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, DocMon, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xRepType", DbType.String, RepType, ParameterDirection.Input);


                ds = objSQLdb.ExecuteDataSet("SSCRM_REP_SERVICE_FARMER_MEETINGS", CommandType.StoredProcedure, param);


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

        public DataSet ServiceSchoolVisitDetails(string CompCode, string BranchCode, string DocMon, string RepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, CompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, DocMon, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xRepType", DbType.String, RepType, ParameterDirection.Input);


                ds = objSQLdb.ExecuteDataSet("SSCRM_REP_SERVICE_SCHOOL_VISITS", CommandType.StoredProcedure, param);


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

        public DataSet ServiceDemoPlotsDetails(string CompCode, string BranchCode, string DocMon, string RepType)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, CompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, DocMon, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xRepType", DbType.String, RepType, ParameterDirection.Input);


                ds = objSQLdb.ExecuteDataSet("SSCRM_REP_SERVICE_DEMO_PLOTS", CommandType.StoredProcedure, param);


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

        public DataSet GetServiceMappedEcodes(string CompCode, string BranchCode, string DocMon)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, CompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, DocMon, ParameterDirection.Input);


                ds = objSQLdb.ExecuteDataSet("ServiceMappedEcodes_Get", CommandType.StoredProcedure, param);


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

        public DataSet Get_ServiceEmpAllActivityDates(string CompCode, string BranchCode, string DocMon, DateTime FromDate, DateTime ToDate, string strEcode)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[6];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, CompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, DocMon, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xFromDate", DbType.DateTime, FromDate, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xToDate", DbType.DateTime, ToDate, ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xEcode", DbType.String, strEcode, ParameterDirection.Input);


                ds = objSQLdb.ExecuteDataSet("Get_ServiceEmpAllActivityDates", CommandType.StoredProcedure, param);


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

        public DataSet GetServiceEmpActivities(string CompCode, string BranchCode, string DocMon, string strEcode, string ActivityDate)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[5];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, CompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, DocMon, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xEcode", DbType.String, strEcode, ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xActivityDate", DbType.String, ActivityDate, ParameterDirection.Input);



                ds = objSQLdb.ExecuteDataSet("Get_ServiceEmpAllActivitiesByActivityDate", CommandType.StoredProcedure, param);


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

        public Hashtable GetEmpTourBillDetails(string strTrnNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sTransactionNo", DbType.String, strTrnNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_ServicesTourBillHead", CommandType.StoredProcedure, param);
                ht.Add("TourBillHead", ds.Tables[0]);

                ds = null;
                param = null;
                objSQLdb = null;
                objSQLdb = new SQLDB();
                param = new SqlParameter[1];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@sTransactionNo", DbType.String, strTrnNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_ServicesTourBillDetl", CommandType.StoredProcedure, param);
                ht.Add("TourBillDetl", ds.Tables[0]);




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

        public DataSet GetEmpTourActivityDetails(string TrnNo, DateTime ActivityDate)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@sTransactionNo", DbType.String, TrnNo, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xActivityDate", DbType.DateTime, ActivityDate, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_ServiceTourBillActivityDetails", CommandType.StoredProcedure, param);


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
        public DataSet LevelGroupServiceEcodes_Get(string sBranchCode, Int32 nDestEcode, string DocMonth)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@nDestEcode", DbType.Int32, nDestEcode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, DocMonth, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("LevelGroupServiceEcodes_Get", CommandType.StoredProcedure, param);

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
     

        public DataSet Get_ServiceMaporUnmappedEcodes(string sBranchCode,string DocMonth, char cMapped)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, sBranchCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sDocMonth", DbType.String, DocMonth, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@cMapped", DbType.String, cMapped, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("Get_MapOrUnmappedServiceEcodes", CommandType.StoredProcedure, param);
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

        public DataSet LevelCampList_Get(string sCompCode,string sBranCode,string sActive)
        {
            DataSet ds = new DataSet();
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            try
            {
                param[0] = objSQLdb.CreateParameter("@sCompCode", DbType.String, sCompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranchCode", DbType.String, sBranCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sActive", DbType.String, sActive, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("Get_CampListForService", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
                param = null;

            }
            return ds;
        }

        public Hashtable Get_InvoiceVerificationDetl(string CompCode, string BranchCode, string DocMon, string sOrderNo)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[4];
            Hashtable ht = new Hashtable();
            DataSet ds = new DataSet();
            try
            {
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, CompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, DocMon, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xOrderNo", DbType.String, sOrderNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("InvoiceVerificationHead_Get", CommandType.StoredProcedure, param);
                ht.Add("InvVerificationHead", ds.Tables[0]);

                ds = null;
                param = null;
                objSQLdb = null;
                objSQLdb = new SQLDB();
                param = new SqlParameter[4];
                ds = new DataSet();
                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, CompCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xDocMonth", DbType.String, DocMon, ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xOrderNo", DbType.String, sOrderNo, ParameterDirection.Input);

                ds = objSQLdb.ExecuteDataSet("InvoiceVerificationDetl_Get", CommandType.StoredProcedure, param);
                ht.Add("InvVerificationDetl", ds.Tables[0]);


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
    }
}
